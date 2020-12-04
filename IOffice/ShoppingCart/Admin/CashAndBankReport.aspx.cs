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

public partial class account_CashAndBankReport : System.Web.UI.Page
{
    string compid;
    SqlConnection con=new SqlConnection(PageConn.connnn);
   
    
    protected void Page_Load(object sender, EventArgs e)
    {
        //PageConn pgcon = new PageConn();
        //con = pgcon.dynconn; 
        
       
            pagetitleclass pg = new pagetitleclass();
            string strData = Request.Url.ToString();

            char[] separator = new char[] { '/' };
            compid = Session["Comid"].ToString();
            string[] strSplitArr = strData.Split(separator);
            int i = Convert.ToInt32(strSplitArr.Length);
            string page = strSplitArr[i - 1].ToString();


            Page.Title = pg.getPageTitle(page);
       
        if (!IsPostBack)
        {
            txtdatefrom.Text = System.DateTime.Now.ToShortDateString();
            txtdateto.Text = System.DateTime.Now.ToShortDateString();
            ViewState["BalOfLastYr"] = null;
            //lbl1.Visible = false;
            //lbl2.Visible = false;
            //lblOpningBalStartDate.Visible = false;
            //lblOpeningBal.Visible = false;
            ViewState["startdateOfOpeningBalance"] = "";
           
            ViewState["sortOrder"] = "";

            if (Request.QueryString["Aid"] != null && Request.QueryString["Wid"]!=null)
            {
               
               // Fillddlwarehouse();
                string strwh = "SELECT Distinct WareHouseId,Name  FROM WareHouseMaster inner join EmployeeWarehouseRights on EmployeeWarehouseRights.Whid=WareHouseMaster.WareHouseId where comid = '" + Session["comid"] + "'and WareHouseMaster.Status='" + 1 + "' and EmployeeWarehouseRights.AccessAllowed='True' order by name";

                SqlCommand cmd1 = new SqlCommand(strwh, con);
                cmd1.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                DataTable dt = new DataTable();
                da.Fill(dt);

                ddlwarehouse.DataSource = dt;
                ddlwarehouse.DataTextField = "Name";
                ddlwarehouse.DataValueField = "WareHouseId";
                ddlwarehouse.DataBind();
                ddlwarehouse.Items.Insert(0, "Select");
               // ddlwarehouse.SelectedIndex = ddlwarehouse.Items.IndexOf(ddlwarehouse.Items.FindByValue(Request.QueryString["Wid"]));
                ddlwarehouse.SelectedValue = Request.QueryString["Wid"];
              
                ddlwarehouse_SelectedIndexChanged(sender, e);
                acccfill();
                ddlcashbank.SelectedIndex = ddlcashbank.Items.IndexOf(ddlcashbank.Items.FindByValue(Request.QueryString["Aid"]));
               
                RadioButtonList1.SelectedIndex = 0;
               
                    Panel1.Visible = true;
                    Panel12.Visible = false;
                    if (Request.QueryString["Aid"] != null)
                    {
                        if (Convert.ToString(Session["date"]) != "")
                        {
                            txtdatefrom.Text = Session["date"].ToString();
                            txtdateto.Text = Session["edate"].ToString();
                        }
                    }
                    else
                    {
                        DateTime fristdayofmonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                        txtdatefrom.Text = fristdayofmonth.ToShortDateString();
                        DateTime lastdaymonth = fristdayofmonth.AddMonths(1).AddDays(-1);
                        txtdateto.Text = lastdaymonth.ToShortDateString();
                    }
                btnGo_Click(sender, e);
               
            }

            
               if (Request.QueryString["Aid"] == null && Request.QueryString["Wid"] == null)
            {
                Fillddlwarehouse();
                if (RadioButtonList1.SelectedIndex == 0)
                {
                    Panel1.Visible = true;
                    Panel12.Visible = false;
                    //ddlperiod.Visible = false;

                    txtdatefrom.Text = System.DateTime.Now.ToShortDateString();
                    txtdateto.Text = System.DateTime.Now.ToShortDateString();
                }
                else if (RadioButtonList1.SelectedIndex == 1)
                {
                    Panel1.Visible = false;
                    Panel12.Visible = true;
                    SqlCommand cmd = new SqlCommand("select PeriodId,PeriodName from PeriodMaster", con);
                    SqlDataAdapter dtp = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    dtp.Fill(ds);

                    ddlperiod.DataSource = ds;
                    ddlperiod.DataTextField = "PeriodName";
                    ddlperiod.DataValueField = "PeriodId";
                    ddlperiod.DataBind();
                    ddlperiod.Items.Insert(0, "All");

                    ddlperiod.Items[0].Value = "0";
                    ddlperiod.SelectedIndex = 0;

                    ddlperiod.SelectedIndex = ddlperiod.Items.IndexOf(ddlperiod.Items.FindByValue("5"));


                }
         
            }
            
            
        }

    }
    protected void fillddl(DropDownList ddl, string qry, string field, string field1)
    {
        SqlCommand cmdfill = new SqlCommand(qry, con);
        SqlDataAdapter dtpfill = new SqlDataAdapter(cmdfill);
        DataSet dsfill = new DataSet();

        dtpfill.Fill(dsfill);

        ddl.DataSource = dsfill;
        ddl.DataTextField = field;
        ddl.DataValueField = field1;
        ddl.DataBind();
    }
    protected void imgbtn1_Click(object sender, ImageClickEventArgs e)
    {
      
    }
    
    protected void imgbtn2_Click(object sender, ImageClickEventArgs e)
    {
       
    }
    protected void Calendar2_SelectionChanged(object sender, EventArgs e)
    {
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedIndex == 0)
        {
            Panel1.Visible = true;
            Panel12.Visible = false;
            
           
        }
        else if (RadioButtonList1.SelectedIndex == 1)
        {
            Panel1.Visible = false;
            Panel12.Visible = true;
            SqlCommand cmd = new SqlCommand("select PeriodId,PeriodName from PeriodMaster", con);
            SqlDataAdapter dtp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            dtp.Fill(ds);

            ddlperiod.DataSource = ds;
            ddlperiod.DataTextField = "PeriodName";
            ddlperiod.DataValueField = "PeriodId";
            ddlperiod.DataBind();

            ddlperiod.SelectedIndex = ddlperiod.Items.IndexOf(ddlperiod.Items.FindByValue("5"));
            
            
        }
    }
  

    protected void FillGrid()
    {
        //try
        //{
            string strfromto = "";
            string strperiod = "";
            string strAll = "";
            if (ddlcashbank.SelectedIndex > 0)
            {
                strAll = " select distinct    convert(nvarchar(10), TranctionMaster.Date,101)as Date,TranctionMaster.Tranction_Master_Id, TranctionMaster.EntryNumber,EntryTypeMaster.Entry_Type_Id,EntryTypeMaster.Entry_Type_Name, Tranction_Details.AccountDebit, Tranction_Details.AccountCredit,    Case when (Tranction_Details.AmountDebit is Null) Then ('0') Else Tranction_Details.AmountDebit End as AmountDebit,    Case when (Tranction_Details.AmountCredit IS null) then ('0') else Tranction_Details.AmountCredit End as AmountCredit,    Tranction_Details.Memo AS Detail_Memo, Tranction_Details.DateTimeOfTransaction, Tranction_Details.DiscEarn,     Tranction_Details.DiscPaid, Tranction_Details.Tranction_Details_Id from   EntryTypeMaster inner join TranctionMaster ON TranctionMaster.EntryTypeId = EntryTypeMaster.Entry_Type_Id " +
                  " inner join Tranction_Details on Tranction_Details.Tranction_Master_Id   = TranctionMaster.Tranction_Master_Id and TranctionMaster.Whid='" + ddlwarehouse.SelectedValue + "'  join " +
                   "AccountMaster on  (AccountMaster.AccountId=Tranction_Details.AccountDebit " +
                   "or  AccountMaster.AccountId=Tranction_Details.AccountCredit)  and  Tranction_Details.whid='" + ddlwarehouse.SelectedValue + "' and AccountMaster.Whid IS NOT NULL  and  AccountMaster.Whid='" + ddlwarehouse.SelectedValue + "' and   AccountMaster.compid='" + Session["comid"] + "' and AccountMaster.AccountId='" + ddlcashbank.SelectedValue + "'";
               
            }
            else if (ddlcashbank.SelectedIndex == 0)
            {
               finctoin();
            }
            if (ddlcashbank.SelectedIndex > 0)
            {
                if (RadioButtonList1.SelectedValue == "0")
                {

                    DateTime endofdate = new DateTime();
                    endofdate = Convert.ToDateTime(txtdateto.Text);
                   
                    string thisdateupdate = Convert.ToString(endofdate);

                    strfromto = " and (TranctionMaster.Date between '" + Convert.ToDateTime(txtdatefrom.Text.ToString()).ToShortDateString() + "' and '" + Convert.ToDateTime(thisdateupdate).ToShortDateString() + "') order by Date ";

                    ViewState["OpnBalDt"] = Convert.ToDateTime(txtdatefrom.Text.ToString()).ToShortDateString();
                    ViewState["SDate"] = Convert.ToDateTime(txtdateto.Text.ToString()).ToShortDateString();

                }
                else
                {
                    ViewState["OpnBalDt"] = Convert.ToDateTime(txtdatefrom.Text.ToString()).ToShortDateString();
                    string diffper = (string)(PeriodDiff());

                    strperiod = " and (" + diffper.ToString() + ") order by TranctionMaster.Tranction_Master_Id ";
                }

                string strmain = strAll + strfromto + strperiod;

                string entrytype1 = "";
                string partyname1 = "";
                string deptname1 = "";
                string location1 = "";
                string costcentre1 = "";
                string profitecentre1 = "";
                string cashbank1 = "";



                string strupdatewithallFilter = strmain + cashbank1 + entrytype1 + partyname1 + deptname1 + location1 + costcentre1 + profitecentre1;

                SqlCommand cmdcr = new SqlCommand(strupdatewithallFilter, con);
                SqlDataAdapter adpcr = new SqlDataAdapter(cmdcr);
                DataTable dtcr = new DataTable();
                adpcr.Fill(dtcr);


                string strwithAllCrDB = strupdatewithallFilter;// +"or TranctionMaster.Tranction_Master_Id in" + Or + " ";

                string ddd = "";
                string dating = "";
                DateTime dating1;
                if (RadioButtonList1.SelectedIndex == 0)
                {
                    DateTime endadatecount = Convert.ToDateTime(txtdatefrom.Text);
                    ddd = endadatecount.ToShortDateString().ToString();
                    endadatecount = endadatecount.AddDays(-1);
                    dating = Convert.ToString(endadatecount.ToShortDateString());
                    dating1 = Convert.ToDateTime(txtdatefrom.Text);
                }
                else
                {
                    DateTime endadatecount = Convert.ToDateTime(ViewState["SDate"]);
                    ddd = endadatecount.ToShortDateString().ToString();
                    endadatecount = endadatecount.AddDays(-1);
                    dating = Convert.ToString(endadatecount.ToShortDateString());
                    dating1 = Convert.ToDateTime(ViewState["SDate"]);
                }


                SqlCommand cmdcrNew = new SqlCommand(strwithAllCrDB, con);
                SqlDataAdapter adpcrNew = new SqlDataAdapter(cmdcrNew);
                DataTable dtcrNew = new DataTable();
                adpcrNew.Fill(dtcrNew);

                DataTable dtdaopb = new DataTable();
                dtdaopb = (DataTable)select("select Report_Period_Id from [ReportPeriod] where ReportPeriod.Report_Period_Id<( select Report_Period_Id from [ReportPeriod] where Whid='" + ddlwarehouse.SelectedValue + "' and Active='1') and  Whid='" + ddlwarehouse.SelectedValue + "' order by Report_Period_Id Desc");
                if (dtdaopb.Rows.Count > 0)
                {
                    ViewState["period"] = dtdaopb.Rows[0]["Report_Period_Id"].ToString();
                   
                }
                DataTable dtcrNew4 = new DataTable();
                 dtcrNew4 = select("Select Balance from AccountBalance where AccountMasterId=(select Max(Id) as Id from AccountMaster where AccountId='" + ddlcashbank.SelectedValue + "' and Whid='" + ddlwarehouse.SelectedValue + "' ) and Report_Period_Id='" + ViewState["period"] + "'");
                 

                        
                         ViewState["BalOfLastYr"] = "";
                         ViewState["OpnBalDt"] = ddd;
                         double opnbal1 = 0;
                         double totaodebi = 0;
                         double totaocredit = 0;
                         DataTable dtcrNe1 = new DataTable();
                         dtcrNe1 = select("select Distinct Sum(Tranction_Details.AmountDebit)   from   EntryTypeMaster inner join TranctionMaster ON TranctionMaster.EntryTypeId = EntryTypeMaster.Entry_Type_Id " +
                         " inner join Tranction_Details on Tranction_Details.Tranction_Master_Id   = TranctionMaster.Tranction_Master_Id and TranctionMaster.Whid='" + ddlwarehouse.SelectedValue + "'  join " +
                         "AccountMaster on  AccountMaster.AccountId=Tranction_Details.AccountDebit " +
                         "  and  Tranction_Details.whid='" + ddlwarehouse.SelectedValue + "' and AccountMaster.Whid IS NOT NULL  and  AccountMaster.Whid='" + ddlwarehouse.SelectedValue + "' and   AccountMaster.compid='" + Session["comid"] + "' and AccountMaster.AccountId='" + ddlcashbank.SelectedValue + "' and (TranctionMaster.Date<= '" + dating + "')");
                          if (Convert.ToString( dtcrNe1.Rows[0][0]) !="")
                          {
                              totaodebi=Convert.ToDouble(dtcrNe1.Rows[0][0]);
                          }
                         DataTable dtcrNe11 = new DataTable();
                         dtcrNe11 = select("select Distinct Sum(Tranction_Details.AmountCredit)   from   EntryTypeMaster inner join TranctionMaster ON TranctionMaster.EntryTypeId = EntryTypeMaster.Entry_Type_Id " +
                         " inner join Tranction_Details on Tranction_Details.Tranction_Master_Id   = TranctionMaster.Tranction_Master_Id and TranctionMaster.Whid='" + ddlwarehouse.SelectedValue + "'  join " +
                         "AccountMaster on  AccountMaster.AccountId=Tranction_Details.AccountCredit " +
                         "  and  Tranction_Details.whid='" + ddlwarehouse.SelectedValue + "' and AccountMaster.Whid IS NOT NULL  and  AccountMaster.Whid='" + ddlwarehouse.SelectedValue + "' and   AccountMaster.compid='" + Session["comid"] + "' and AccountMaster.AccountId='" + ddlcashbank.SelectedValue + "' and (TranctionMaster.Date<= '" + dating + "')");
                          if (Convert.ToString( dtcrNe11.Rows[0][0]) !="")
                          {
                               totaocredit=Convert.ToDouble(dtcrNe11.Rows[0][0]);
                          }
                         
                        // opnbal1=totaocredit-totaodebi;
                         opnbal1 = totaodebi - totaocredit;
                         if (dtcrNew4.Rows.Count > 0)
                         {
                             if (dtcrNew4.Rows[0]["Balance"].ToString() != null)
                             {
                                 ViewState["BalOfLastYr"] = (Convert.ToDouble(dtcrNew4.Rows[0]["Balance"]) + opnbal1).ToString();

                             }
                             else
                             {
                                 ViewState["BalOfLastYr"] =  opnbal1.ToString();

                             }
                         }
                         else
                         {
                             ViewState["BalOfLastYr"] = opnbal1.ToString();

                         }
                         //   ViewState["BalOfLastYr"] = dtcrNew4.Rows[0]["BalanceOfLastYear"].ToString();
                         ViewState["OpnBalDt"] = ddd;
                         //lbl1.Visible = true;
                         //lbl2.Visible = true;
                         //lblOpningBalStartDate.Visible = true;
                         //lblOpeningBal.Visible = true;
                         lblOpningBalStartDate.Text = Convert.ToString(ViewState["OpnBalDt"].ToString());
                         lblOpningBalStartDate.Text = dating1.ToShortDateString();
                         lblOpeningBal.Text = Math.Round(Convert.ToDecimal(ViewState["BalOfLastYr"]), 2).ToString("###,###.##");
                         if (Convert.ToString(lblOpeningBal.Text) == "")
                         {
                             lblOpeningBal.Text = "0.00";
                         }
                         else
                         {
                             lblOpeningBal.Text = String.Format("{0:n}", Convert.ToDecimal(lblOpeningBal.Text));
                         }
                         //ViewState["OBD"] = 
                  
                DataTable dt123 = (DataTable)CreateTempDTforGrid();
                foreach (DataRow dtrNew in dtcrNew.Rows)
                {
                    DataRow dradd = dt123.NewRow();
                    dradd["Tranction_Master_Id"] = dtrNew["Tranction_Master_Id"].ToString();
                    dradd["Date"] = dtrNew["Date"].ToString();
                    dradd["EntryType"] = dtrNew["Entry_Type_Name"].ToString();
                    dradd["EntryNo"] = dtrNew["EntryNumber"].ToString();


                   if ((Convert.ToString(dtrNew["AccountCredit"]) != "") && (Convert.ToInt32(dtrNew["AccountCredit"]) != 0))
                    {

                        string jh = "Select Distinct Tranction_Details.AmountDebit,[AccountName],AccountId From TranctionMaster inner join Tranction_Details on Tranction_Details.Tranction_Master_Id=TranctionMaster.Tranction_Master_Id inner join AccountMaster on AccountMaster.AccountId=Tranction_Details.AccountDebit where TranctionMaster.Tranction_Master_Id='" + Convert.ToString(dtrNew["Tranction_Master_Id"]) + "'  and AccountMaster.Whid='" + ddlwarehouse.SelectedValue + "' and Tranction_Details.Whid='" + ddlwarehouse.SelectedValue + "' and (Tranction_Details.AmountDebit>0 or Tranction_Details.AmountCredit>0 )and AccountId>0 order by Tranction_Details.AmountDebit Desc";

                        //string jh = "Select [AccountName],Tranction_Details.AmountDebit From TranctionMaster inner join Tranction_Details on Tranction_Details.Tranction_Master_Id=TranctionMaster.Tranction_Master_Id inner join AccountMaster on AccountMaster.AccountId=Tranction_Details.AccountDebit where TranctionMaster.Tranction_Master_Id='" + Convert.ToString(dtrNew["Tranction_Master_Id"]) + "'  and AccountMaster.Whid='" + ddlwarehouse.SelectedValue + "' order by Tranction_Details.AmountDebit Desc";
                        SqlCommand cmdcrNew11 = new SqlCommand(jh, con);
                        SqlDataAdapter adpcrNew11 = new SqlDataAdapter(cmdcrNew11);
                        DataTable dtcrNew11 = new DataTable();
                        adpcrNew11.Fill(dtcrNew11);
                        if (dtcrNew11.Rows.Count > 0)
                        {
                            if (Convert.ToString(dtcrNew11.Rows[0]["AccountName"]) != "")
                            {
                                dradd["Accountr"] = Convert.ToString(dtcrNew11.Rows[0]["AccountName"]);
                            }
                        }


                    }
                    else if ((Convert.ToString(dtrNew["AccountDebit"]) != "") && (Convert.ToInt32(dtrNew["AccountDebit"]) != 0))
                    {

                        string jh = "Select Distinct Tranction_Details.AmountCredit,  [AccountName],AccountId From TranctionMaster inner join Tranction_Details on Tranction_Details.Tranction_Master_Id=TranctionMaster.Tranction_Master_Id inner join AccountMaster on AccountMaster.AccountId=Tranction_Details.AccountCredit where TranctionMaster.Tranction_Master_Id='" + Convert.ToString(dtrNew["Tranction_Master_Id"]) + "'  and AccountMaster.Whid='" + ddlwarehouse.SelectedValue + "' and Tranction_Details.Whid='" + ddlwarehouse.SelectedValue + "' and (Tranction_Details.AmountDebit>0 or Tranction_Details.AmountCredit>0 )and AccountId>0 order by Tranction_Details.AmountCredit Desc";
                  
                       // string jh = "Select [AccountName],Tranction_Details.AmountCredit From TranctionMaster inner join Tranction_Details on Tranction_Details.Tranction_Master_Id=TranctionMaster.Tranction_Master_Id inner join AccountMaster on AccountMaster.AccountId=Tranction_Details.AccountCredit where TranctionMaster.Tranction_Master_Id='" + Convert.ToString(dtrNew["Tranction_Master_Id"]) + "'  and AccountMaster.Whid='" + ddlwarehouse.SelectedValue + "' order by Tranction_Details.AmountCredit Desc";
                        SqlCommand cmdcrNew11 = new SqlCommand(jh, con);
                        SqlDataAdapter adpcrNew11 = new SqlDataAdapter(cmdcrNew11);
                        DataTable dtcrNew11 = new DataTable();
                        adpcrNew11.Fill(dtcrNew11);
                        if (dtcrNew11.Rows.Count > 0)
                        {
                            if (Convert.ToString(dtcrNew11.Rows[0]["AccountName"]) != "")
                            {
                                dradd["Accountr"] = Convert.ToString(dtcrNew11.Rows[0]["AccountName"]);
                            }
                        }
                    }
                    
                    else
                    {
                        dradd["Accountr"] = "";
                    }
                   dradd["Credit"] = Math.Round(Convert.ToDecimal(dtrNew["AmountCredit"]), 2).ToString("###,###.##");
                   if (Convert.ToString(dradd["Credit"]) == "")
                   {
                       dradd["Credit"] = "0.00";
                   }
                   else
                   {
                       dradd["Credit"] = String.Format("{0:n}", Convert.ToDecimal(dradd["Credit"]));
                   }
                   dradd["Debit"] = Math.Round(Convert.ToDecimal(dtrNew["AmountDebit"]), 2).ToString("###,###.##");
                   if (Convert.ToString(dradd["Debit"]) == "")
                   {
                       dradd["Debit"] = "0.00";
                   }
                   else
                   {
                       dradd["Debit"] = String.Format("{0:n}", Convert.ToDecimal(dradd["Debit"]));
                   }
                    //dradd["AccountDebit"] = dtrNew["AccDbtName"].ToString();
                    dradd["Tranction_Details_Id"] = dtrNew["Tranction_Details_Id"].ToString();
                    dradd["DetMemo"] = dtrNew["Detail_Memo"].ToString();
                    //dradd["Credit"] = dtrNew["AmountCredit"].ToString();
                    //dradd["Debit"] = dtrNew["AmountDebit"].ToString();
                    dradd["Balance"] = "";// dtrNew["Date"].ToString();
                    //dradd["AccountId"] = dtrNew["AccountId"].ToString();
                    // dradd["ProfitCentre"] = dtrNew["ProfitCenterName"].ToString();
                    //  dradd["Party"] = dtrNew["Compname"].ToString();
                    // dradd["Dept"] = dtrNew["Deptname"].ToString();
                    //dradd["Location"] = dtrNew["WareHouseName"].ToString();
                    dt123.Rows.Add(dradd);
                }


                if (dt123.Rows.Count > 0)
                {
                    grdCashBankReport.DataSource = dt123;

                    DataView myDataView = new DataView();
                    myDataView = dt123.DefaultView;

                    if (hdnsortExp.Value != string.Empty)
                    {
                        myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
                    }

                    grdCashBankReport.DataBind();

                    gaccname.Text = "Cash and bank:" + ddlcashbank.SelectedItem.Text;
                    if (RadioButtonList1.SelectedValue == "0")
                    {
                        gdate.Text = "From : " + txtdatefrom.Text + " To : " + txtdateto.Text;
                    }
                    else
                    {
                        gdate.Text = ddlperiod.SelectedItem.Text;
                    }
                   
                    gpanel.Visible = true;
                    double opnbal = Convert.ToDouble(Session["opbal"]);
                    if (ViewState["BalOfLastYr"] == null || ViewState["BalOfLastYr"].ToString() == "")
                    {

                    }
                    else
                    {
                        //opnbal = Convert.ToDouble(ViewState["CalculatedOpBal"]);
                        opnbal = Convert.ToDouble(ViewState["BalOfLastYr"].ToString());

                    }
                    grdCashBankReport.Columns[1].Visible = false;
                    int h = 0;
                    foreach (GridViewRow gdr5 in grdCashBankReport.Rows)
                    {
                        Label lbldocno = (Label)(gdr5.FindControl("lbldocno"));
                        Label crd = (Label)(gdr5.FindControl("lblAmtCredit"));
                        Label dbt = (Label)(gdr5.FindControl("lblAmtDetail"));
                        LinkButton link1 = (LinkButton)(gdr5.FindControl("link1"));
                        Label bal_cal = (Label)(gdr5.FindControl("lblBalance"));
                        Label lblAcc1 = (Label)(gdr5.FindControl("lblAcc1"));
                          lblAcc1.Visible = true;
                       
                          Label lbltid = (Label)(gdr5.FindControl("lblTransactionMasterId"));
                          string scptx = "Select Distinct Tranction_Details.AmountDebit,Tranction_Details.Tranction_Details_Id From TranctionMaster inner join Tranction_Details on Tranction_Details.Tranction_Master_Id=TranctionMaster.Tranction_Master_Id  where TranctionMaster.Tranction_Master_Id='" + lbltid.Text + "' and (Tranction_Details.AmountDebit>0 or Tranction_Details.AmountCredit>0) order by Tranction_Details.AmountDebit Desc";

                          SqlDataAdapter adp58x = new SqlDataAdapter(scptx, con);
                          DataTable ds58x = new DataTable();
                          adp58x.Fill(ds58x);

                          if (ds58x.Rows.Count > 2)
                          {
                              link1.Visible = true;
                          }
                          else
                          {
                              link1.Visible = false;
                          }
                        if (crd.Text != "" && dbt.Text != "")
                        {

                            double crdV = Convert.ToDouble(crd.Text);
                            double dbtV = Convert.ToDouble(dbt.Text);

                            if (crdV.ToString() != "0")
                            {
                                opnbal = opnbal - crdV;


                            }
                            else if (dbtV.ToString() != "0")
                            {
                                opnbal = opnbal + dbtV;
                            }

                            bal_cal.Text = (Math.Round(opnbal, 2)).ToString("###,###.##");

                        }
                        else if (crd.Text == "" && dbt.Text != "")
                        {
                            //bal_cal.Text = dbt.Text;
                            bal_cal.Text = Convert.ToString(Convert.ToDecimal(bal_cal.Text) + Convert.ToDecimal(dbt.Text) - Convert.ToDecimal(crd.Text));
                            bal_cal.Text = Math.Round(Convert.ToDecimal(bal_cal.Text), 2).ToString("###,###.##");

                        }
                        else if (crd.Text != "" && dbt.Text != "")
                        {
                            //bal_cal.Text = crd.Text;
                            bal_cal.Text = Convert.ToString(Convert.ToDecimal(bal_cal.Text) + Convert.ToDecimal(dbt.Text) - Convert.ToDecimal(crd.Text));
                            bal_cal.Text = Math.Round(Convert.ToDecimal(bal_cal.Text), 2).ToString("###,###.##");

                        }
                        else
                        {
                            bal_cal.Text = "0.00";
                        }
                        if (bal_cal.Text.Length <= 0)
                        {
                            bal_cal.Text = "0.00";
                        }
                        else
                        {
                            bal_cal.Text = String.Format("{0:n}", Convert.ToDecimal(bal_cal.Text));
                        }
                        bal_cal.Text = String.Format("{0:n}", Convert.ToDecimal(bal_cal.Text));
                      
                        ImageButton img = (ImageButton)gdr5.FindControl("img1");
                        // Label lbldocno = (Label)gr.FindControl("lbldocno");

                        string tid1 = img.CommandArgument;
                        h = h + 1;
                        string scpt = "select * from AttachmentMaster where RelatedTableId='" + tid1 + "'";

                        SqlDataAdapter adp58 = new SqlDataAdapter(scpt, con);
                        DataTable ds58 = new DataTable();
                        adp58.Fill(ds58);
  
                        if (ds58.Rows.Count == 0)
                        {

                            img.ImageUrl = "~/ShoppingCart/images/Docimg.png";
                            img.Enabled = false;
                            lbldocno.Text = "0";
                            img.ToolTip = "0";
                        }
                        else
                        {
                            img.ImageUrl = "~/ShoppingCart/images/Docimggreen.jpg";
                            img.Enabled = true;
                            img.ToolTip = ds58.Rows.Count.ToString();
                            lbldocno.Text = ds58.Rows.Count.ToString();



                        }
                        if (Request.QueryString["docid"] != null)
                        {
                            grdCashBankReport.Columns[0].Visible = true;
                        }
                        else
                        {
                            grdCashBankReport.Columns[0].Visible = false;
                        }


                    }


                    //lblOpeningBal.Text = opnbal.ToString();
                }
                else
                {

                    grdCashBankReport.DataSource = null;



                    grdCashBankReport.DataBind();

                }

            }

        //}
        //catch (Exception err)
        //{
        //    //Response.Write("error " + err.Message);

        //}

    }
    protected void link1_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
        lblbnp.Text = "";
        lbletp.Text = "";
        lblenp.Text = "";
        lbldatep.Text = "";
        int rinrow = row.RowIndex;
        // Label AccountId = (Label)grdCashBankReport.Rows[rinrow].FindControl("AccountId");
        Label lbltradeid = (Label)grdCashBankReport.Rows[rinrow].FindControl("lbltradeid");
        int i = Convert.ToInt32(grdCashBankReport.DataKeys[rinrow].Value);
        DataTable dt = new DataTable();
        dt = (DataTable)select("Select WarehouseMaster.Name, [Whid],Entry_Type_Name,EntryNumber,Convert(nvarchar,Date,101) as Date From WarehouseMaster inner join TranctionMaster on TranctionMaster.Whid=WarehouseMaster.WarehouseId inner join EntryTypeMaster on EntryTypeMaster.Entry_Type_Id=TranctionMaster.EntryTypeId where Tranction_Master_Id='" + Convert.ToString(i) + "' ");
        if (dt.Rows.Count > 0)
        {
            lblbnp.Text = Convert.ToString(dt.Rows[0]["Name"]);
            lbletp.Text = Convert.ToString(dt.Rows[0]["Entry_Type_Name"]);
            lblenp.Text = Convert.ToString(dt.Rows[0]["EntryNumber"]);
            lbldatep.Text = Convert.ToString(dt.Rows[0]["Date"]);
            DataTable dt1 = new DataTable();
            dt1 = (DataTable)select("Select distinct AccountDebit, AccountCredit,Case when (Tranction_Details.AmountDebit is Null) Then ('0') Else Tranction_Details.AmountDebit End as AmountDebit,    Case when (Tranction_Details.AmountCredit IS null) then ('0') else Tranction_Details.AmountCredit End as AmountCredit,Tranction_Details.Memo " +
                "from TranctionMaster inner join  Tranction_Details on Tranction_Details.Tranction_Master_Id   = TranctionMaster.Tranction_Master_Id where  Tranction_Details.Tranction_Master_Id ='" + i + "'  and (Tranction_Details.AmountDebit>0 or Tranction_Details.AmountCredit>0 ) ");
            if (dt1.Rows.Count > 0)
            {
                grdPartyList.DataSource = dt1;
                grdPartyList.DataBind();

                int m = 0;
                foreach (GridViewRow grd in grdPartyList.Rows)
                {
                    Label lblacccc = (Label)(grd.FindControl("lblacccc"));
                    Label lblDebit = (Label)(grd.FindControl("lblDebit"));
                    Label lblCredit = (Label)(grd.FindControl("lblCredit"));

                    lblDebit.Text = Math.Round(Convert.ToDecimal(lblDebit.Text), 2).ToString("###,###.##");
                    if (lblDebit.Text == "")
                    {
                        lblDebit.Text = "0.00";
                    }
                    else
                    {

                        lblDebit.Text = String.Format("{0:n}", Convert.ToDecimal(lblDebit.Text));
                    }
                    lblCredit.Text = Math.Round(Convert.ToDecimal(lblCredit.Text), 2).ToString("###,###.##");

                    if (lblCredit.Text == "")
                    {
                        lblCredit.Text = "0.00";
                    }
                    else
                    {
                        lblCredit.Text = String.Format("{0:n}", Convert.ToDecimal(lblCredit.Text));
                    }

                    if ((Convert.ToString(dt1.Rows[m]["AccountCredit"]) != "") && (Convert.ToInt32(dt1.Rows[m]["AccountCredit"]) != 0))
                    {

                        string jh = "Select distinct [AccountName],AccountId From  AccountMaster where  AccountMaster.Whid='" + ddlwarehouse.SelectedValue + "' and AccountMaster.AccountId='" + dt1.Rows[m]["AccountCredit"] + "' ";
                        SqlCommand cmdcrNew11 = new SqlCommand(jh, con);
                        SqlDataAdapter adpcrNew11 = new SqlDataAdapter(cmdcrNew11);
                        DataTable dtcrNew11 = new DataTable();
                        adpcrNew11.Fill(dtcrNew11);
                        if (dtcrNew11.Rows.Count > 0)
                        {
                            if (Convert.ToString(dtcrNew11.Rows[0]["AccountName"]) != "")
                            {
                                lblacccc.Text = Convert.ToString(dtcrNew11.Rows[0]["AccountId"]) + ":" + Convert.ToString(dtcrNew11.Rows[0]["AccountName"]);
                            }
                        }


                    }
                    else if ((Convert.ToString(dt1.Rows[m]["AccountDebit"]) != "") && (Convert.ToInt32(dt1.Rows[m]["AccountDebit"]) != 0))
                    {

                        string jh = "Select distinct [AccountName],AccountId From  AccountMaster where  AccountMaster.Whid='" + ddlwarehouse.SelectedValue + "' and AccountMaster.AccountId='" + dt1.Rows[m]["AccountDebit"] + "' ";
                        SqlCommand cmdcrNew11 = new SqlCommand(jh, con);
                        SqlDataAdapter adpcrNew11 = new SqlDataAdapter(cmdcrNew11);
                        DataTable dtcrNew11 = new DataTable();
                        adpcrNew11.Fill(dtcrNew11);
                        if (dtcrNew11.Rows.Count > 0)
                        {
                            if (Convert.ToString(dtcrNew11.Rows[0]["AccountName"]) != "")
                            {
                                lblacccc.Text = Convert.ToString(dtcrNew11.Rows[0]["AccountId"]) + ":" + Convert.ToString(dtcrNew11.Rows[0]["AccountName"]);

                            }
                        }


                    }
                    m += 1;
                }
                ModalPopupExtender2.Show();
            }
        }
    }
    protected void finctoin()
    {
        gpanel.Visible = false;
        string strfromto = "";
        string strperiod = "";
        string strAll = "";
        if (RadioButtonList1.SelectedValue == "0")
        {

            DateTime endofdate = new DateTime();
            endofdate = Convert.ToDateTime(txtdateto.Text);
            // endofdate = endofdate.AddDays(1);

            string thisdateupdate = Convert.ToString(endofdate);

            strfromto = " and (TranctionMaster.Date between '" + Convert.ToDateTime(txtdatefrom.Text.ToString()).ToShortDateString() + "' and '" + Convert.ToDateTime(thisdateupdate).ToShortDateString() + "') order by Date ";

            ViewState["OpnBalDt"] = Convert.ToDateTime(txtdatefrom.Text.ToString()).ToShortDateString();
            ViewState["SDate"] = Convert.ToDateTime(txtdateto.Text.ToString()).ToShortDateString();

        }
        else
        {
            ViewState["OpnBalDt"] = Convert.ToDateTime(txtdatefrom.Text.ToString()).ToShortDateString();
            string diffper = (string)(PeriodDiff());

            strperiod = " and (" + diffper.ToString() + ") order by TranctionMaster.Tranction_Master_Id ";
        }
        DataTable dt123 = (DataTable)CreateTempDTforGrid();
        for (int k = 0; k <ddlcashbank.Items.Count; k++)
        {
           
            if (k >= 1)
            {
                strAll = " select distinct    convert(nvarchar(10), TranctionMaster.Date,101)as Date,TranctionMaster.Tranction_Master_Id, TranctionMaster.EntryNumber,EntryTypeMaster.Entry_Type_Id,EntryTypeMaster.Entry_Type_Name, Tranction_Details.AccountDebit, Tranction_Details.AccountCredit,    Case when (Tranction_Details.AmountDebit is Null) Then ('0') Else Tranction_Details.AmountDebit End as AmountDebit,    Case when (Tranction_Details.AmountCredit IS null) then ('0') else Tranction_Details.AmountCredit End as AmountCredit,    Tranction_Details.Memo AS Detail_Memo, Tranction_Details.DateTimeOfTransaction, Tranction_Details.DiscEarn,     Tranction_Details.DiscPaid, Tranction_Details.Tranction_Details_Id from   EntryTypeMaster inner join TranctionMaster ON TranctionMaster.EntryTypeId = EntryTypeMaster.Entry_Type_Id " +
                        " inner join Tranction_Details on Tranction_Details.Tranction_Master_Id   = TranctionMaster.Tranction_Master_Id and TranctionMaster.Whid='" + ddlwarehouse.SelectedValue + "'  join " +
                         "AccountMaster on  (AccountMaster.AccountId=Tranction_Details.AccountDebit " +
                         "or  AccountMaster.AccountId=Tranction_Details.AccountCredit)  and  Tranction_Details.whid='" + ddlwarehouse.SelectedValue + "' and AccountMaster.Whid IS NOT NULL  and  AccountMaster.Whid='" + ddlwarehouse.SelectedValue + "' and   AccountMaster.compid='" + Session["comid"] + "' and  AccountMaster.AccountId='" + ddlcashbank.Items[k].Value + "'";



                string strmain = strAll + strfromto + strperiod;

                string entrytype1 = "";
                string partyname1 = "";
                string deptname1 = "";
                string location1 = "";
                string costcentre1 = "";
                string profitecentre1 = "";
                string cashbank1 = "";



                string strupdatewithallFilter = strmain + cashbank1 + entrytype1 + partyname1 + deptname1 + location1 + costcentre1 + profitecentre1;

                SqlCommand cmdcr = new SqlCommand(strupdatewithallFilter, con);
                SqlDataAdapter adpcr = new SqlDataAdapter(cmdcr);
                DataTable dtcr = new DataTable();
                adpcr.Fill(dtcr);


                string strwithAllCrDB = strupdatewithallFilter;// +"or TranctionMaster.Tranction_Master_Id in" + Or + " ";

                string ddd = "";
                string dating = "";
                DateTime dating1;
                if (RadioButtonList1.SelectedIndex == 0)
                {
                    DateTime endadatecount = Convert.ToDateTime(txtdatefrom.Text);
                    ddd = endadatecount.ToShortDateString().ToString();
                    endadatecount = endadatecount.AddDays(-1);
                    dating = Convert.ToString(endadatecount.ToShortDateString());
                    dating1 = Convert.ToDateTime(txtdatefrom.Text);
                }
                else
                {
                    DateTime endadatecount = Convert.ToDateTime(ViewState["SDate"]);
                    ddd = endadatecount.ToShortDateString().ToString();
                    endadatecount = endadatecount.AddDays(-1);
                    dating = Convert.ToString(endadatecount.ToShortDateString());
                    dating1 = Convert.ToDateTime(ViewState["SDate"]);
                }


                SqlCommand cmdcrNew = new SqlCommand(strwithAllCrDB, con);
                SqlDataAdapter adpcrNew = new SqlDataAdapter(cmdcrNew);
                DataTable dtcrNew = new DataTable();
                adpcrNew.Fill(dtcrNew);

                DataTable dtdaopb = new DataTable();
                dtdaopb = (DataTable)select("  select Report_Period_Id from [ReportPeriod] where ReportPeriod.Report_Period_Id<( select Report_Period_Id from [ReportPeriod] where Whid='" + ddlwarehouse.SelectedValue + "' and Active='1') and  Whid='" + ddlwarehouse.SelectedValue + "' order by Report_Period_Id Desc");
                if (dtdaopb.Rows.Count > 0)
                {
                    ViewState["period"] = dtdaopb.Rows[0]["Report_Period_Id"].ToString();
                }
                DataTable dtcrNew4 = new DataTable();
                dtcrNew4 = select("Select Balance from AccountBalance where AccountMasterId=(select Max(Id) as Id from AccountMaster where AccountId='" + ddlcashbank.Items[k].Value + "' and Whid='" + ddlwarehouse.SelectedValue + "' ) and Report_Period_Id='" + ViewState["period"] + "'");
                double totaodebi = 0;
                double totaocredit = 0;
                double opnbal1 = 0;
                DataTable dtcrNe1 = new DataTable();
                dtcrNe1 = select("select Distinct Sum(Tranction_Details.AmountDebit)   from   EntryTypeMaster inner join TranctionMaster ON TranctionMaster.EntryTypeId = EntryTypeMaster.Entry_Type_Id " +
                " inner join Tranction_Details on Tranction_Details.Tranction_Master_Id   = TranctionMaster.Tranction_Master_Id and TranctionMaster.Whid='" + ddlwarehouse.SelectedValue + "'  join " +
                "AccountMaster on  AccountMaster.AccountId=Tranction_Details.AccountDebit " +
                "  and  Tranction_Details.whid='" + ddlwarehouse.SelectedValue + "' and AccountMaster.Whid IS NOT NULL  and  AccountMaster.Whid='" + ddlwarehouse.SelectedValue + "' and   AccountMaster.compid='" + Session["comid"] + "' and AccountMaster.AccountId='" + ddlcashbank.Items[k].Value + "' and (TranctionMaster.Date<= '" + dating + "')");
                if (Convert.ToString(dtcrNe1.Rows[0][0]) != "")
                {
                    totaodebi = Convert.ToDouble(dtcrNe1.Rows[0][0]);
                }
                DataTable dtcrNe11 = new DataTable();
                dtcrNe11 = select("select Distinct Sum(Tranction_Details.AmountCredit)   from   EntryTypeMaster inner join TranctionMaster ON TranctionMaster.EntryTypeId = EntryTypeMaster.Entry_Type_Id " +
                " inner join Tranction_Details on Tranction_Details.Tranction_Master_Id   = TranctionMaster.Tranction_Master_Id and TranctionMaster.Whid='" + ddlwarehouse.SelectedValue + "'  join " +
                "AccountMaster on  AccountMaster.AccountId=Tranction_Details.AccountCredit " +
                "  and  Tranction_Details.whid='" + ddlwarehouse.SelectedValue + "' and AccountMaster.Whid IS NOT NULL  and  AccountMaster.Whid='" + ddlwarehouse.SelectedValue + "' and   AccountMaster.compid='" + Session["comid"] + "' and AccountMaster.AccountId='" + ddlcashbank.Items[k].Value + "' and (TranctionMaster.Date<= '" + dating + "')");
                if (Convert.ToString(dtcrNe11.Rows[0][0]) != "")
                {
                    totaocredit = Convert.ToDouble(dtcrNe11.Rows[0][0]);
                }

               // opnbal1 = totaocredit - totaodebi;
                opnbal1 = totaodebi - totaocredit;
                        if (dtcrNew4.Rows.Count > 0)
                        {

                            if (dtcrNew4.Rows[0]["Balance"].ToString() != null)
                            {

                                ViewState["BalOfLastYr"] = (Convert.ToDouble(dtcrNew4.Rows[0]["Balance"]) + opnbal1).ToString();
                            }
                            else
                            {
                                ViewState["BalOfLastYr"] =  opnbal1.ToString();
                          
                            }
                        }
                        else
                        {
                            ViewState["BalOfLastYr"] = opnbal1.ToString();

                        }

                        //   ViewState["BalOfLastYr"] = dtcrNew4.Rows[0]["BalanceOfLastYear"].ToString();
                        ViewState["OpnBalDt"] = ddd;
                        //lbl1.Visible = true;
                        //lbl2.Visible = true;
                        //lblOpningBalStartDate.Visible = true;
                        //lblOpeningBal.Visible = true;
                        lblOpningBalStartDate.Text = Convert.ToString(ViewState["OpnBalDt"].ToString());
                        lblOpningBalStartDate.Text = dating1.ToShortDateString();
                        lblOpeningBal.Text = Math.Round(Convert.ToDecimal(ViewState["BalOfLastYr"]), 2).ToString("###,###.##");
                        if (Convert.ToString(lblOpeningBal.Text) == "")
                        {
                            lblOpeningBal.Text = "0.00";
                        }
                        else
                        {
                            lblOpeningBal.Text = String.Format("{0:n}", Convert.ToDecimal(lblOpeningBal.Text));
                        }
                        //ViewState["OBD"] = 
                     
                if (dtcrNew.Rows.Count > 0)
                {

                    DataRow dradd1 = dt123.NewRow();
                    string[] separator1 = new string[] { ":" };
                    string[] strSplitArr1 = ddlcashbank.Items[k].Text.Split(separator1, StringSplitOptions.RemoveEmptyEntries);
                    string Accname = strSplitArr1[3].ToString();
                    if (Accname.Length >= 16)
                    {
                        Accname = Accname.Substring(1, 15);
                    }
                    dradd1["Account"] = Accname;
                    dradd1["Date"] = lblOpningBalStartDate.Text;
                    dradd1["Balance"] = lblOpeningBal.Text;
                    dradd1["EntryType"] = "OPENING BALANCE";
                    dt123.Rows.Add(dradd1);
                }
               
                
                foreach (DataRow dtrNew in dtcrNew.Rows)
                {
                    DataRow dradd = dt123.NewRow();
                    dradd["Tranction_Master_Id"] = dtrNew["Tranction_Master_Id"].ToString();
                    dradd["Date"] = dtrNew["Date"].ToString();
                    dradd["EntryType"] = dtrNew["Entry_Type_Name"].ToString();
                    dradd["EntryNo"] = dtrNew["EntryNumber"].ToString();


                    if ((Convert.ToString(dtrNew["AccountCredit"]) != "") && (Convert.ToInt32(dtrNew["AccountCredit"]) != 0))
                    {

                        string jh = "Select Distinct Tranction_Details.AmountDebit,[AccountName],AccountId From TranctionMaster inner join Tranction_Details on Tranction_Details.Tranction_Master_Id=TranctionMaster.Tranction_Master_Id inner join AccountMaster on AccountMaster.AccountId=Tranction_Details.AccountDebit where TranctionMaster.Tranction_Master_Id='" + Convert.ToString(dtrNew["Tranction_Master_Id"]) + "'  and AccountMaster.Whid='" + ddlwarehouse.SelectedValue + "' and Tranction_Details.Whid='" + ddlwarehouse.SelectedValue + "' and (Tranction_Details.AmountDebit>0 or Tranction_Details.AmountCredit>0 )and AccountId>0 order by Tranction_Details.AmountDebit Desc";
                 

                        //string jh = "Select [AccountName],Tranction_Details.AmountDebit From TranctionMaster inner join Tranction_Details on Tranction_Details.Tranction_Master_Id=TranctionMaster.Tranction_Master_Id inner join AccountMaster on AccountMaster.AccountId=Tranction_Details.AccountDebit where TranctionMaster.Tranction_Master_Id='" + Convert.ToString(dtrNew["Tranction_Master_Id"]) + "'  and AccountMaster.Whid='" + ddlwarehouse.SelectedValue + "' order by Tranction_Details.AmountDebit Desc";
                        SqlCommand cmdcrNew11 = new SqlCommand(jh, con);
                        SqlDataAdapter adpcrNew11 = new SqlDataAdapter(cmdcrNew11);
                        DataTable dtcrNew11 = new DataTable();
                        adpcrNew11.Fill(dtcrNew11);
                        if (dtcrNew11.Rows.Count > 0)
                        {
                            if (Convert.ToString(dtcrNew11.Rows[0]["AccountName"]) != "")
                            {
                                dradd["Accountr"] = Convert.ToString(dtcrNew11.Rows[0]["AccountName"]);
                            }
                        }
                       
                    }
                    else if ((Convert.ToString(dtrNew["AccountDebit"]) != "") && (Convert.ToInt32(dtrNew["AccountDebit"]) != 0))
                    {
                        string jh = "Select Distinct Tranction_Details.AmountCredit,  [AccountName],AccountId From TranctionMaster inner join Tranction_Details on Tranction_Details.Tranction_Master_Id=TranctionMaster.Tranction_Master_Id inner join AccountMaster on AccountMaster.AccountId=Tranction_Details.AccountCredit where TranctionMaster.Tranction_Master_Id='" + Convert.ToString(dtrNew["Tranction_Master_Id"]) + "'  and AccountMaster.Whid='" + ddlwarehouse.SelectedValue + "' and Tranction_Details.Whid='" + ddlwarehouse.SelectedValue + "' and (Tranction_Details.AmountDebit>0 or Tranction_Details.AmountCredit>0 )and AccountId>0 order by Tranction_Details.AmountCredit Desc";
                  
                        //string jh = "Select [AccountName],Tranction_Details.AmountCredit From TranctionMaster inner join Tranction_Details on Tranction_Details.Tranction_Master_Id=TranctionMaster.Tranction_Master_Id inner join AccountMaster on AccountMaster.AccountId=Tranction_Details.AccountCredit where TranctionMaster.Tranction_Master_Id='" + Convert.ToString(dtrNew["Tranction_Master_Id"]) + "'  and AccountMaster.Whid='" + ddlwarehouse.SelectedValue + "' order by Tranction_Details.AmountCredit Desc";
                                 SqlCommand cmdcrNew11 = new SqlCommand(jh, con);
                        SqlDataAdapter adpcrNew11 = new SqlDataAdapter(cmdcrNew11);
                        DataTable dtcrNew11 = new DataTable();
                        adpcrNew11.Fill(dtcrNew11);
                        if (dtcrNew11.Rows.Count > 0)
                        {
                            if (Convert.ToString(dtcrNew11.Rows[0]["AccountName"]) != "")
                            {
                                dradd["Accountr"] = Convert.ToString(dtcrNew11.Rows[0]["AccountName"]);
                            }
                        }
                    }
                    else
                    {
                        dradd["Accountr"] = "";
                    }
                    dradd["Credit"] = Math.Round(Convert.ToDecimal(dtrNew["AmountCredit"]), 2).ToString("###,###.##");
      
                    if (Convert.ToString(dradd["Credit"]) == "")
                    {
                        dradd["Credit"] = "0.00";
                    }
                    else
                    {
                        dradd["Credit"] = String.Format("{0:n}", Convert.ToDecimal(dradd["Credit"]));
                    }
                    dradd["Debit"] = Math.Round(Convert.ToDecimal(dtrNew["AmountDebit"]), 2).ToString("###,###.##");
                    if (Convert.ToString(dradd["Debit"]) == "")
                    {
                        dradd["Debit"] = "0.00";
                    }
                    else
                    {
                        dradd["Debit"] = String.Format("{0:n}", Convert.ToDecimal(dradd["Debit"]));
                    }
                    //dradd["AccountDebit"] = dtrNew["AccDbtName"].ToString();
                    dradd["Tranction_Details_Id"] = dtrNew["Tranction_Details_Id"].ToString();
                    dradd["DetMemo"] = dtrNew["Detail_Memo"].ToString();
                    //dradd["Credit"] = dtrNew["AmountCredit"].ToString();
                    //dradd["Debit"] = dtrNew["AmountDebit"].ToString();
                    dradd["Balance"] = "";// dtrNew["Date"].ToString();
                   // dradd["AccountId"] = dtrNew["AccountId"].ToString();
                    // dradd["ProfitCentre"] = dtrNew["ProfitCenterName"].ToString();
                    //  dradd["Party"] = dtrNew["Compname"].ToString();
                    // dradd["Dept"] = dtrNew["Deptname"].ToString();
                    //dradd["Location"] = dtrNew["WareHouseName"].ToString();
                    dt123.Rows.Add(dradd);
                }
                if (dtcrNew.Rows.Count > 0)
                {

                    DataRow dradd1 = dt123.NewRow();
                    dradd1["Account"] = "";
                    dradd1["Date"] = "";
                    dradd1["Balance"] = "";
                    dradd1["EntryType"] = "";
                    dt123.Rows.Add(dradd1);
                    DataRow dradd2 = dt123.NewRow();
                    dradd2["Account"] = "";
                    dradd2["Date"] = "";
                    dradd2["Balance"] = "";
                    dradd2["EntryType"] = "";
                    dt123.Rows.Add(dradd2);
                }
                     
                }
               
            }
        

        if (dt123.Rows.Count > 0)
        {
            grdCashBankReport.DataSource = dt123;

            DataView myDataView = new DataView();
            myDataView = dt123.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }

            grdCashBankReport.DataBind();

            gaccname.Text = "Cash and bank:" + ddlcashbank.SelectedItem.Text;
            if (RadioButtonList1.SelectedValue == "0")
            {
                gdate.Text = "From : " + txtdatefrom.Text + " To : " + txtdateto.Text;
            }
            else
            {
                gdate.Text = ddlperiod.SelectedItem.Text;
            }
      
            //gpanel.Visible = true;
            double opnbal = Convert.ToDouble(Session["opbal"]);
            if (ViewState["BalOfLastYr"] == null || ViewState["BalOfLastYr"].ToString() == "")
            {

            }
            else
            {
                //opnbal = Convert.ToDouble(ViewState["CalculatedOpBal"]);
                opnbal = Convert.ToDouble(ViewState["BalOfLastYr"].ToString());

            }
            //lbl1.Visible = false;
            //lbl2.Visible = false;
            //lblOpeningBal.Visible = false;
            //lblOpningBalStartDate.Visible=false;
            grdCashBankReport.Columns[1].Visible = true;
            int h = 0;
            foreach (GridViewRow gdr5 in grdCashBankReport.Rows)
            {
                Label crd = (Label)(gdr5.FindControl("lblAmtCredit"));
                Label dbt = (Label)(gdr5.FindControl("lblAmtDetail"));
                Label bal_cal = (Label)(gdr5.FindControl("lblBalance"));
                LinkButton link1 = (LinkButton)(gdr5.FindControl("link1"));
                Label lblDate = (Label)(gdr5.FindControl("lblDate"));
                Label lblentrytype = (Label)(gdr5.FindControl("lblentrytype"));
                Label lblAcc1 = (Label)(gdr5.FindControl("lblAcc1"));
                Label lblAcc = (Label)(gdr5.FindControl("lblAcc"));
                ImageButton lbladdd = (ImageButton)(gdr5.FindControl("lbladdd"));
                ImageButton link11 = (ImageButton)gdr5.FindControl("img1");
                Label lbldocno = (Label)gdr5.FindControl("lbldocno");
                if (lblDate.Text.ToString() == "" && lblentrytype.Text.ToString() == "")
                {
                    lbldocno.Visible = false;
                    bal_cal.Text = "";
                    link1.Visible = false;
                    opnbal = 0;
                    link11.Visible = false;
                    lbladdd.Visible = false;
                    bal_cal.Visible = false;
                }
                if (bal_cal.Text != "" && lblentrytype.Text.ToString() == "OPENING BALANCE")
                {
                    lblAcc.Font.Bold = true;
                    lblAcc.Text = lblAcc1.Text;
                    lblentrytype.Font.Bold = true;
                    opnbal = Convert.ToDouble(lblOpeningBal.Text);
                    //opnbal =Convert.ToDouble(bal_cal.Text);
                    link1.Visible = false;
                    lblAcc1.Visible = true;
                    link11.Visible = false;
                    lbladdd.Visible = false;
                    bal_cal.Visible = true;
                    lbldocno.Visible = false;
                }
                if (link1.Visible == true)
                {
                    Label lbltid = (Label)(gdr5.FindControl("lblTransactionMasterId"));
                    string scptx = "Select Distinct Tranction_Details.AmountDebit,Tranction_Details.Tranction_Details_Id From TranctionMaster inner join Tranction_Details on Tranction_Details.Tranction_Master_Id=TranctionMaster.Tranction_Master_Id  where TranctionMaster.Tranction_Master_Id='" + lbltid.Text + "' and (Tranction_Details.AmountDebit>0 or Tranction_Details.AmountCredit>0) order by Tranction_Details.AmountDebit Desc";

                    SqlDataAdapter adp58x = new SqlDataAdapter(scptx, con);
                    DataTable ds58x = new DataTable();
                    adp58x.Fill(ds58x);

                    if (ds58x.Rows.Count > 2)

                   
                    {
                        link1.Visible = true;
                    }
                    else
                    {
                        link1.Visible = false;
                    }
                }
                if (crd.Text != "" && dbt.Text != "")
                {

                    double crdV = Convert.ToDouble(crd.Text);
                    double dbtV = Convert.ToDouble(dbt.Text);

                    if (crdV.ToString() != "0")
                    {
                        opnbal = opnbal - crdV;


                    }
                    else if (dbtV.ToString() != "0")
                    {
                        opnbal = opnbal + dbtV;
                    }


                    bal_cal.Text = (Math.Round(opnbal, 2)).ToString("###,###.##");

                }
                else if (crd.Text == "" && dbt.Text != "")
                {
                    //bal_cal.Text = dbt.Text;
                    bal_cal.Text = Convert.ToString(Convert.ToDecimal(bal_cal.Text) + Convert.ToDecimal(dbt.Text) - Convert.ToDecimal(crd.Text));
                    bal_cal.Text = Math.Round(Convert.ToDecimal(bal_cal.Text), 2).ToString("###,###.##");

                }
                else if (crd.Text != "" && dbt.Text != "")
                {
                    //bal_cal.Text = crd.Text;
                    bal_cal.Text = Convert.ToString(Convert.ToDecimal(bal_cal.Text) + Convert.ToDecimal(dbt.Text) - Convert.ToDecimal(crd.Text));
                    bal_cal.Text = Math.Round(Convert.ToDecimal(bal_cal.Text), 2).ToString("###,###.##");

                }
                //else
                //{
                //    bal_cal.Text = "0.00";
                //}
                if (bal_cal.Text.Length <= 0)
                {
                    bal_cal.Text = "0.00";
                }
                else
                {
                    bal_cal.Text = String.Format("{0:n}", Convert.ToDecimal(bal_cal.Text));
                }
                bal_cal.Text = String.Format("{0:n}", Convert.ToDecimal(bal_cal.Text));

                ImageButton img = (ImageButton)gdr5.FindControl("img1");
                // Label lbldocno = (Label)gr.FindControl("lbldocno");
         
                string tid1 = img.CommandArgument;
                h = h + 1;
                string scpt = "select * from AttachmentMaster where RelatedTableId='" + tid1 + "'";

                SqlDataAdapter adp58 = new SqlDataAdapter(scpt, con);
                DataTable ds58 = new DataTable();
                adp58.Fill(ds58);
                //ImageButton link1 = (ImageButton)gdr5.FindControl("img1");
                if (ds58.Rows.Count == 0)
                {

                    img.ImageUrl = "~/ShoppingCart/images/Docimg.png";
                    img.Enabled = false;
                    lbldocno.Text = "0";
                    img.ToolTip = "0";
                }
                else
                {
                    img.ImageUrl = "~/ShoppingCart/images/Docimggreen.jpg";
                    img.Enabled = true;
                    img.ToolTip = ds58.Rows.Count.ToString();
                    lbldocno.Text = ds58.Rows.Count.ToString();



                }
                if (Request.QueryString["docid"] != null)
                {
                    grdCashBankReport.Columns[0].Visible = true;
                }
                else
                {
                    grdCashBankReport.Columns[0].Visible = false;
                }


            }
           
        }
               
    }
    public double isnumSelf(string ck)
    {
        double i = 0;
        try
        {
            i = Convert.ToDouble(ck);
        }
        catch (Exception )
        {
            i = 0;
        }
        return i;
    }
   
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

        //---------------this year period end-------------------
        //--------------this year period start----------------------
        DateTime lastyearstart = Convert.ToDateTime("1/1/" + System.DateTime.Today.AddYears(-1).Year.ToString());
        DateTime lastyearend = Convert.ToDateTime("12/31/" + System.DateTime.Today.AddYears(-1).Year.ToString());

        string lastyearstartdate = lastyearstart.ToShortDateString();
        // ViewState["SDate"] = lastyearstartdate;
        string lastyearenddate = lastyearend.ToShortDateString();



        //---------------this year period end-------------------


        string periodstartdate = "";
        string periodenddate = "";

        if (ddlperiod.SelectedItem.Text == "Today")
        {
            periodstartdate = Today.ToString();
            DataTable dt = new DataTable();
            dt = (DataTable)select("select Convert(nvarchar,StartDate,101)   as StartDate,EndDate from [ReportPeriod] where Compid = '" + compid + "' and Active='1'  and Whid='" + ddlwarehouse.SelectedValue + "'");

            if (dt.Rows.Count > 0)
            {
                if (Convert.ToDateTime(periodstartdate) < Convert.ToDateTime(dt.Rows[0][0].ToString()))
                {
                    lblstartdate.Text = dt.Rows[0][0].ToString();
                    grdCashBankReport.DataSource = null;
                    grdCashBankReport.DataBind();
                    ModalPopupExtender1.Show();
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
            DataTable dt = new DataTable();
            dt = (DataTable)select("select Convert(nvarchar,StartDate,101)   as StartDate,EndDate from [ReportPeriod] where Compid = '" + compid + "' and Active='1'  and Whid='" + ddlwarehouse.SelectedValue + "'");

            if (dt.Rows.Count > 0)
            {
                if (Convert.ToDateTime(periodstartdate) < Convert.ToDateTime(dt.Rows[0][0].ToString()))
                {
                    lblstartdate.Text = dt.Rows[0][0].ToString();
                    grdCashBankReport.DataSource = null;
                    grdCashBankReport.DataBind();
                    ModalPopupExtender1.Show();
                    //txtfromdate.Text = dt.Rows[0][0].ToString();
                    periodenddate = Today.ToString();

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
            DataTable dt = new DataTable();
            dt = (DataTable)select("select Convert(nvarchar,StartDate,101)   as StartDate,EndDate from [ReportPeriod] where Compid = '" + compid + "' and Active='1'  and Whid='" + ddlwarehouse.SelectedValue + "'");

            if (dt.Rows.Count > 0)
            {
                if (Convert.ToDateTime(periodstartdate) < Convert.ToDateTime(dt.Rows[0][0].ToString()))
                {
                    lblstartdate.Text = dt.Rows[0][0].ToString();
                    grdCashBankReport.DataSource = null;
                    grdCashBankReport.DataBind();
                    ModalPopupExtender1.Show();
                    //txtfromdate.Text = dt.Rows[0][0].ToString();
                    periodenddate = Today.ToString();

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
            DataTable dt = new DataTable();
            dt = (DataTable)select("select Convert(nvarchar,StartDate,101)   as StartDate,EndDate from [ReportPeriod] where Compid = '" + compid + "' and Active='1'  and Whid='" + ddlwarehouse.SelectedValue + "'");

            if (dt.Rows.Count > 0)
            {
                if (Convert.ToDateTime(periodstartdate) < Convert.ToDateTime(dt.Rows[0][0].ToString()))
                {
                    lblstartdate.Text = dt.Rows[0][0].ToString();
                    grdCashBankReport.DataSource = null;
                    grdCashBankReport.DataBind();
                    ModalPopupExtender1.Show();

                    //txtfromdate.Text = dt.Rows[0][0].ToString();
                    periodenddate = Today.ToString();
                    //gridpurchaseregister.DataSource = null;
                    //gridpurchaseregister.DataBind();
                }


                else
                {
                    periodstartdate = lastweekstartdate.ToString();
                    periodenddate = Today.ToString();
                }
            }
        }
        else if (ddlperiod.SelectedItem.Text == "Current Month")
        {

            periodstartdate = thismonthstart.ToString();
            periodenddate = thismonthenddate.ToString();
            DataTable dt = new DataTable();
            dt = (DataTable)select("select Convert(nvarchar,StartDate,101)   as StartDate,EndDate from [ReportPeriod] where Compid = '" + compid + "' and Active='1'  and Whid='" + ddlwarehouse.SelectedValue + "'");

            if (dt.Rows.Count > 0)
            {
                if (Convert.ToDateTime(periodstartdate) < Convert.ToDateTime(dt.Rows[0][0].ToString()))
                {
                    lblstartdate.Text = dt.Rows[0][0].ToString();
                    grdCashBankReport.DataSource = null;
                    grdCashBankReport.DataBind();
                    ModalPopupExtender1.Show();
                    //txtfromdate.Text = dt.Rows[0][0].ToString();


                }


                else
                {
                    periodstartdate = thismonthstart.ToString();

                }
            }
        }
        else if (ddlperiod.SelectedItem.Text == "Last Month")
        {

            periodstartdate = lastmonthstartdate.ToString();
            DataTable dt = new DataTable();
            dt = (DataTable)select("select Convert(nvarchar,StartDate,101)   as StartDate,EndDate from [ReportPeriod] where Compid = '" + compid + "' and Active='1'  and Whid='" + ddlwarehouse.SelectedValue + "'");

            if (dt.Rows.Count > 0)
            {
                if (Convert.ToDateTime(periodstartdate) < Convert.ToDateTime(dt.Rows[0][0].ToString()))
                {
                    lblstartdate.Text = dt.Rows[0][0].ToString();
                    grdCashBankReport.DataSource = null;
                    grdCashBankReport.DataBind();
                    ModalPopupExtender1.Show();
                    //txtfromdate.Text = dt.Rows[0][0].ToString();
                    periodenddate = Today.ToString();

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
            DataTable dt = new DataTable();
            dt = (DataTable)select("select Convert(nvarchar,StartDate,101)   as StartDate,EndDate from [ReportPeriod] where Compid = '" + compid + "' and Active='1'  and Whid='" + ddlwarehouse.SelectedValue + "'");

            if (dt.Rows.Count > 0)
            {
                if (Convert.ToDateTime(periodstartdate) < Convert.ToDateTime(dt.Rows[0][0].ToString()))
                {
                    lblstartdate.Text = dt.Rows[0][0].ToString();
                    grdCashBankReport.DataSource = null;
                    grdCashBankReport.DataBind();
                    ModalPopupExtender1.Show();
                    //txtfromdate.Text = dt.Rows[0][0].ToString();
                    periodenddate = Today.ToString();

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
            DataTable dt = new DataTable();
            dt = (DataTable)select("select Convert(nvarchar,StartDate,101)   as StartDate,EndDate from [ReportPeriod] where Compid = '" + compid + "' and Active='1'  and Whid='" + ddlwarehouse.SelectedValue + "'");

            if (dt.Rows.Count > 0)
            {
                if (Convert.ToDateTime(periodstartdate) < Convert.ToDateTime(dt.Rows[0][0].ToString()))
                {
                    lblstartdate.Text = dt.Rows[0][0].ToString();
                    grdCashBankReport.DataSource = null;
                    grdCashBankReport.DataBind();
                    ModalPopupExtender1.Show();
                    //txtfromdate.Text = dt.Rows[0][0].ToString();
                    periodenddate = Today.ToString();

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
            DataTable dt = new DataTable();
            dt = (DataTable)select("select Convert(nvarchar,StartDate,101)   as StartDate,EndDate from [ReportPeriod] where Compid = '" + compid + "' and Active='1'  and Whid='" + ddlwarehouse.SelectedValue + "'");

            if (dt.Rows.Count > 0)
            {
                if (Convert.ToDateTime(periodstartdate) < Convert.ToDateTime(dt.Rows[0][0].ToString()))
                {
                    lblstartdate.Text = dt.Rows[0][0].ToString();
                    grdCashBankReport.DataSource = null;
                    grdCashBankReport.DataBind();
                    ModalPopupExtender1.Show();
                    //txtfromdate.Text = dt.Rows[0][0].ToString();
                    periodenddate = Today.ToString();

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
            DataTable dt = new DataTable();
            dt = (DataTable)select("select Convert(nvarchar,StartDate,101)   as StartDate,EndDate from [ReportPeriod] where Compid = '" + compid + "' and Active='1'  and Whid='" + ddlwarehouse.SelectedValue + "'");

            if (dt.Rows.Count > 0)
            {
                if (Convert.ToDateTime(periodstartdate) < Convert.ToDateTime(dt.Rows[0][0].ToString()))
                {
                    lblstartdate.Text = dt.Rows[0][0].ToString();
                    grdCashBankReport.DataSource = null;
                    grdCashBankReport.DataBind();
                    ModalPopupExtender1.Show();
                    //txtfromdate.Text = dt.Rows[0][0].ToString();
                    periodenddate = Today.ToString();

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
            DataTable dt4 = new DataTable();
            dt4 = (DataTable)select("select Convert(nvarchar,StartDate,101)   as StartDate,EndDate from [ReportPeriod] where Compid = '" + compid + "' and Active='1'  and Whid='" + ddlwarehouse.SelectedValue + "'");

            if (dt4.Rows.Count > 0)
            {
                periodstartdate = dt4.Rows[0]["StartDate"].ToString();
                periodenddate = dt4.Rows[0]["EndDate"].ToString();
            }
           
         
           
             
        }



        DateTime sd = Convert.ToDateTime(periodstartdate.ToString());
        DateTime ed = Convert.ToDateTime(periodenddate.ToString());
        ed = ed.AddDays(1);

         //string str = "  TranctionMaster.Date between '" + sd.ToShortDateString() + "' AND '" + ed.ToShortDateString() + "'"; // + //2009-4-30' " +
        string str = "  TranctionMaster.Date between '" + sd.ToShortDateString() + "' AND '" + ed.ToShortDateString() + "' "; // + //2009-4-30' " +
        ViewState["startdateOfOpeningBalance"] = sd.ToShortDateString();
        ViewState["SDate"] = sd.ToShortDateString();
        return str;
    }
     protected DataTable select(string str)
     {
         SqlCommand cmd = new SqlCommand(str, con);
         SqlDataAdapter dtp = new SqlDataAdapter(cmd);
         DataTable dt = new DataTable();
         dtp.Fill(dt);

         return dt;

     }    
     protected void btnGo_Click(object sender, EventArgs e)
     {
         gpanel.Visible = false;
         if (RadioButtonList1.SelectedValue == "0")
         {
             DataTable dt = new DataTable();
             dt = (DataTable)select("select Convert(nvarchar,StartDate,101)   as StartDate,EndDate from [ReportPeriod] where Compid = '" + compid + "' and Active='1'  and Whid='"+ddlwarehouse.SelectedValue+"'");
            
            
             if (dt.Rows.Count > 0)
             {
                 if (Convert.ToDateTime(txtdatefrom.Text) < Convert.ToDateTime(dt.Rows[0]["StartDate"].ToString()))
                 {
                     lblstartdate.Text = dt.Rows[0][0].ToString();
                     ModalPopupExtender1.Show();
                     txtdatefrom.Text = dt.Rows[0][0].ToString();

                 }


                 else
                 {
                     DateTime dt2 = Convert.ToDateTime(txtdatefrom.Text);
                     DateTime dt1 = Convert.ToDateTime(txtdateto.Text);


                     if (dt1 < dt2)
                     {

                         Label1.Visible = true;
                         Label1.Text = " Start Date Must Be Less than End Date";


                     }
                     else
                     {
                         FillGrid();
                        
                         

                        



                     }
                 }
             }
         }
         else if (RadioButtonList1.SelectedValue == "1")
         {
            
             FillGrid();
           
         }
        
     }
    protected void grdCashBankReport_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        FillGrid();
    }
    protected void grdCashBankReport_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Sort")
        {
            return;
        }

        else if (e.CommandName == "AddDoc")
        {
            // GridView2.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            int dk = Convert.ToInt32(e.CommandArgument);// Convert.ToInt32(GridView2.DataKeys[GridView2.SelectedIndex].Value);
            ViewState["Dk"] = dk;

            string entryno = "select TranctionMaster.EntryNumber ,EntryTypeMaster.Entry_Type_Name from TranctionMaster inner join EntryTypeMaster on EntryTypeMaster.Entry_Type_Id=TranctionMaster.EntryTypeId where Tranction_Master_Id='" + ViewState["Dk"] + "'";

            SqlDataAdapter adpentno = new SqlDataAdapter(entryno, con);
            DataTable dsentno = new DataTable();
            adpentno.Fill(dsentno);
            if (dsentno.Rows.Count > 0)
            {
                lbldocentrytype.Text = dsentno.Rows[0]["Entry_Type_Name"].ToString();
                lbldocentryno.Text = dsentno.Rows[0]["EntryNumber"].ToString();

            }


            // string scpt = "select * from AttachmentMaster where RelatedTableId='" + ViewState["Dk"] + "'";
            string scpt = " select AttachmentMaster.Id,DocumentMaster.DocumentId as IfilecabinetDocId,DocumentMaster.DocumentTitle as Titlename,CONVERT (Nvarchar, DocumentMaster.DocumentDate,101) as Datetime   ,DocumentMainType.DocumentMainType+':'+ DocumentSubType.DocumentSubType+':'+DocumentType.DocumentType as Filename from AttachmentMaster inner join DocumentMaster on DocumentMaster.DocumentId=AttachmentMaster.IfilecabinetDocId inner join DocumentType on DocumentMaster.DocumentTypeId=DocumentType.DocumentTypeId  inner join DocumentSubType on DocumentSubType.DocumentSubTypeId=DocumentType.DocumentSubTypeId inner join DocumentMainType on DocumentMainType.DocumentMainTypeId=DocumentSubType.DocumentMainTypeId where AttachmentMaster.RelatedTableId='" + ViewState["Dk"] + "' ";
            SqlDataAdapter adp58 = new SqlDataAdapter(scpt, con);
            DataTable ds58 = new DataTable();
            adp58.Fill(ds58);


            if (ds58.Rows.Count > 0)
            {
                grd.DataSource = ds58;
                grd.DataBind();
                if (grd.Rows.Count > 1)
                {
                    lbldoclab.Text = "List of Documents";
                    lblheadoc.Text = "List of documents attached to ";
                }
                else
                {
                    lbldoclab.Text = "List of Document";
                    lblheadoc.Text = "List of document attached to ";
                }
                ModalPopupExtender3.Show();
            }
        }
        else if (e.CommandName == "Docadd")
        {
            int dk = Convert.ToInt32(e.CommandArgument);
            string te = "AccEntryDocUp.aspx?Tid=" + dk;
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

            //<a href="AccEntryDocUp.aspx?Tid=<%#DataBinder.Eval(Container.DataItem, "Tranction_Master_Id")%>" target="_blank" >

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

   
    protected void grdCashBankReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    public DataTable CreateTempDTforGrid()
    {
        DataTable dtGRD = new DataTable();

        DataColumn dcTMI = new DataColumn();
        dcTMI.ColumnName = "Tranction_Master_Id";
        dcTMI.DataType = System.Type.GetType("System.String");
        dcTMI.AllowDBNull = true;
        dtGRD.Columns.Add(dcTMI);

        DataColumn dcDate = new DataColumn();
        dcDate.ColumnName = "Date";
        dcDate.DataType = System.Type.GetType("System.String");
        dcDate.AllowDBNull = true;
        dtGRD.Columns.Add(dcDate);

        DataColumn dcEntryType = new DataColumn();
        dcEntryType.ColumnName = "EntryType";
        dcEntryType.DataType = System.Type.GetType("System.String");
        dcEntryType.AllowDBNull = true;
        dtGRD.Columns.Add(dcEntryType);

        DataColumn dcEntryNo = new DataColumn();
        dcEntryNo.ColumnName = "EntryNo";
        dcEntryNo.DataType = System.Type.GetType("System.String");
        dcEntryNo.AllowDBNull = true;
        dtGRD.Columns.Add(dcEntryNo);

       
        DataColumn dcRelaDoc = new DataColumn();
        dcRelaDoc.ColumnName = "RelaDoc";
        dcRelaDoc.DataType = System.Type.GetType("System.String");
        dcRelaDoc.AllowDBNull = true;
        dtGRD.Columns.Add(dcRelaDoc);

        DataColumn dcDetMemo = new DataColumn();
        dcDetMemo.ColumnName = "DetMemo";
        dcDetMemo.DataType = System.Type.GetType("System.String");
        dcDetMemo.AllowDBNull = true;
        dtGRD.Columns.Add(dcDetMemo);

        DataColumn dcAccount = new DataColumn();
        dcAccount.ColumnName = "Account";
        dcAccount.DataType = System.Type.GetType("System.String");
        dcAccount.AllowDBNull = true;
        dtGRD.Columns.Add(dcAccount);

        DataColumn dcCredit = new DataColumn();
        dcCredit.ColumnName = "Credit";
        dcCredit.DataType = System.Type.GetType("System.String");
        dcCredit.AllowDBNull = true;
        dtGRD.Columns.Add(dcCredit);

        //DataColumn dcAccountDebit = new DataColumn();
        //dcAccountDebit.ColumnName = "AccountDebit";
        //dcAccountDebit.DataType = System.Type.GetType("System.String");
        //dtGRD.Columns.Add(dcAccountDebit);

        DataColumn dcDebit = new DataColumn();
        dcDebit.ColumnName = "Debit";
        dcDebit.DataType = System.Type.GetType("System.String");
        dcDebit.AllowDBNull = true;
        dtGRD.Columns.Add(dcDebit);

        DataColumn dcBalance = new DataColumn();
        dcBalance.ColumnName = "Balance";
        dcBalance.DataType = System.Type.GetType("System.String");
        dcBalance.AllowDBNull = true;
        dtGRD.Columns.Add(dcBalance);

        DataColumn dcCostCtr = new DataColumn();
        dcCostCtr.ColumnName = "CostCentre";
        dcCostCtr.DataType = System.Type.GetType("System.String");
        dcCostCtr.AllowDBNull = true;
        dtGRD.Columns.Add(dcCostCtr);

        DataColumn dcProfitCtr = new DataColumn();
        dcProfitCtr.ColumnName = "ProfitCentre";
        dcProfitCtr.DataType = System.Type.GetType("System.String");
        dcProfitCtr.AllowDBNull = true;
        dtGRD.Columns.Add(dcProfitCtr);

        DataColumn dcParty = new DataColumn();
        dcParty.ColumnName = "Party";
        dcParty.DataType = System.Type.GetType("System.String");
        dcParty.AllowDBNull = true;
        dtGRD.Columns.Add(dcParty);

        DataColumn dcDept = new DataColumn();
        dcDept.ColumnName = "Tranction_Details_Id";
        dcDept.DataType = System.Type.GetType("System.String");
        dcDept.AllowDBNull = true;
        dtGRD.Columns.Add(dcDept);

        DataColumn dcLocation = new DataColumn();
        dcLocation.ColumnName = "Location";
        dcLocation.DataType = System.Type.GetType("System.String");
        dcLocation.AllowDBNull = true;
        dtGRD.Columns.Add(dcLocation);

        DataColumn AccountId = new DataColumn();
        //AccountId.ColumnName = "AccountId";
        //AccountId.DataType = System.Type.GetType("System.String");
        //AccountId.AllowDBNull = true;
        //dtGRD.Columns.Add(AccountId);
       
        DataColumn Accountr = new DataColumn();
        Accountr.ColumnName = "Accountr";
        Accountr.DataType = System.Type.GetType("System.String");
        Accountr.AllowDBNull = true;
        dtGRD.Columns.Add(Accountr);
        return dtGRD;



    }

    //protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    //{
       
        
    //}
    protected void ddlperiod_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Fillddlwarehouse()
    {
        string strwh = "SELECT Distinct WareHouseId,Name  FROM WareHouseMaster inner join EmployeeWarehouseRights on EmployeeWarehouseRights.Whid=WareHouseMaster.WareHouseId where comid = '" + Session["comid"] + "'and WareHouseMaster.Status='" + 1 + "' and EmployeeWarehouseRights.AccessAllowed='True' order by name";

        SqlCommand cmd = new SqlCommand(strwh, con);
        cmd.CommandType = CommandType.Text;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ddlwarehouse.DataSource = dt;
            ddlwarehouse.DataTextField = "Name";
            ddlwarehouse.DataValueField = "WareHouseId";
            ddlwarehouse.DataBind();
            ddlwarehouse.Items.Insert(0, "-Select-");

            ddlwarehouse.Items[0].Value = "0";
        }
        string eeed = " Select distinct EmployeeMaster.Whid from  EmployeeMaster where EmployeeMasterId='" + Session["EmployeeId"] + "'";
        SqlCommand cmdeeed = new SqlCommand(eeed, con);
        SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
        DataTable dteeed = new DataTable();
        adpeeed.Fill(dteeed);
        if (dteeed.Rows.Count > 0)
        {
            ddlwarehouse.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);

        }
        EventArgs e = new EventArgs();
        object sender = new object();
        ddlwarehouse_SelectedIndexChanged(sender, e);
       
    }
    protected void ddlwarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        acccfill();
    }
    protected void acccfill()
    {
        ddlcashbank.Items.Clear();
        if (ddlwarehouse.SelectedIndex > 0)
        {
            fillddl(ddlcashbank, "SELECT     LEFT(ClassTypeCompanyMaster.displayname, 10) + ' : ' + LEFT(ClassCompanyMaster.displayname, 10) + ' : ' + LEFT(GroupCompanyMaster.groupdisplayname, 10) " +
                    "   + ' : ' + AccountMaster.AccountName AS AccountName, AccountMaster.AccountId  " +
                  "  FROM         AccountMaster LEFT OUTER JOIN  " +
                "    ClassTypeCompanyMaster RIGHT OUTER JOIN  " +
               "   ClassCompanyMaster ON ClassTypeCompanyMaster.id = ClassCompanyMaster.classtypecompanymasterid RIGHT OUTER JOIN  " +
                      "  GroupCompanyMaster ON ClassCompanyMaster.id = GroupCompanyMaster.classcompanymasterid ON AccountMaster.GroupId = GroupCompanyMaster.GroupId where AccountMaster.Status=1 and AccountMaster.compid = '" + Session["comid"] + "' " +
               "  and AccountMaster.Whid='" + ddlwarehouse.SelectedValue + "' and GroupCompanyMaster.whid='" + ddlwarehouse.SelectedValue + "' order by  ClassTypeCompanyMaster.displayname, ClassCompanyMaster.displayname, GroupCompanyMaster.groupdisplayname, AccountMaster.AccountName asc", "AccountName", "AccountId");// where GroupId=1
            ddlcashbank.Items.Insert(0, "All");
            ddlcashbank.Items[0].Value = "0";
        }
        else
        {
            ddlcashbank.Items.Insert(0, "All");
            ddlcashbank.Items[0].Value = "0";
        }

        DataTable dt = new DataTable();
        dt = (DataTable)select("Select CompanyMaster.CompanyName,WareHouseMaster.Name from CompanyMaster inner join CompanyWebsitMaster on CompanyWebsitMaster.CompanyId=CompanyMaster.CompanyId " +
"inner join WareHouseMaster on WareHouseMaster.WareHouseId=CompanyWebsitMaster.WHId where WareHouseMaster.WareHouseId='" + ddlwarehouse.SelectedValue + "'");


        if (dt.Rows.Count > 0)
        {
            lblcompname.Text = dt.Rows[0]["CompanyName"].ToString();
            lblstore.Text = dt.Rows[0]["Name"].ToString();
        }

    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        ModalPopupExtender2.Hide();
    }
    protected void ImageButtondsfdsfdsf123_Click(object sender, ImageClickEventArgs e)
    {
        ModalPopupExtender3.Hide();
    }
    //protected void ImageButton4123_Click(object sender, ImageClickEventArgs e)
    //{
       
    //}
    //protected void imgin_Click(object sender, ImageClickEventArgs e)
    //{
       

    //}
   

    protected void grd_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            grd.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            int docid = Convert.ToInt32(grd.Rows[grd.SelectedIndex].Cells[1].Text);
        }
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        grdCashBankReport.DataSource = null;
        grdCashBankReport.DataBind();
        ModalPopupExtender1.Hide();
    }
   
   


   
    protected void ImageButton4123_Click(object sender, ImageClickEventArgs e)
    {
        ModalPopupExtender3.Hide();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (Button2.Text == "Printable Version")
        {
            //pnlgrid.ScrollBars = ScrollBars.None;
            //pnlgrid.Height = new Unit("100%");

            Button2.Text = "Hide Printable Version";
            Button3.Visible = true;




            if (grdCashBankReport.Columns[10].Visible == true)
            {
                ViewState["docHide1"] = "tt";
                grdCashBankReport.Columns[10].Visible = false;
            }
            if (grdCashBankReport.Columns[11].Visible == true)
            {
                ViewState["docHide"] = "tt";
                grdCashBankReport.Columns[11].Visible = false;
            }
        }
        else
        {
            //pnlgrid.ScrollBars = ScrollBars.Vertical;
            //pnlgrid.Height = new Unit(250);

            Button2.Text = "Printable Version";
            Button3.Visible = false;


            if (ViewState["docHide1"] != null)
            {
                grdCashBankReport.Columns[10].Visible = true;
            }
            if (ViewState["docHide"] != null)
            {
                grdCashBankReport.Columns[11].Visible = true;
            }
        }
    }
}
