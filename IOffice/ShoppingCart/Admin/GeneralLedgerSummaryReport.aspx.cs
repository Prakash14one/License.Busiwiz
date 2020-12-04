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

public partial class GeneralLedgerSummaryReport : System.Web.UI.Page
{
    //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    SqlConnection con=new SqlConnection(PageConn.connnn);
    string str = "";
    string compid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Comid"] == null)
        {
            Response.Redirect("~/ShoppingCart/Admin/ShoppingCartLogin.aspx");
        }
        //PageConn pgcon = new PageConn();
        //con = pgcon.dynconn; 
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();
        compid = Session["Comid"].ToString();
        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();


        Page.Title = pg.getPageTitle(page); 
        lblmsg.Text = "";
        lblmsg.Visible = false;
        if (!IsPostBack)
        {
            Pagecontrol.dypcontrol(Page, page);
            //fillddlclass();
            if (txtdateto.Text.Length <= 0)
            {
                txtdateto.Text = DateTime.Now.ToShortDateString();
            }
            if (txtdatefrom.Text.Length <= 0)
            {
                txtdatefrom.Text = DateTime.Now.ToShortDateString();
            }
            ViewState["sortOrder"] = "";
            fillstore();
            lblcompname.Text = Session["Cname"].ToString();
        
            if (Request.QueryString["Gd"] != null && Request.QueryString["Wh"] != null)
            {
                       
                        Panel1.Visible = true;
                       
                        ddlSearchByStore.SelectedValue = Request.QueryString["Wh"];
                        ddlSearchByStore_SelectedIndexChanged(sender, e);
                        txtdatefrom.Text = Session["ddf"].ToString();
                        txtdateto.Text = Session["dde"].ToString();

                        DataTable drt = select("select [ClassCompanyMaster].[id],[GroupCompanyMaster].[id] as gid from [GroupCompanyMaster] inner join [ClassCompanyMaster] on [ClassCompanyMaster].id = [GroupCompanyMaster].classcompanymasterid  where [GroupCompanyMaster].GroupId = '" + Request.QueryString["Gd"] + "' and [GroupCompanyMaster].Whid='" + ddlSearchByStore.SelectedValue + "'  ");

                      if (drt.Rows.Count > 0)
                        {
                            ddlclass.SelectedIndex = ddlclass.Items.IndexOf(ddlclass.Items.FindByValue(drt.Rows[0]["id"].ToString()));

                        }
                      ddlclass_SelectedIndexChanged(sender, e);
                      ddlgroup.SelectedIndex = ddlgroup.Items.IndexOf(ddlgroup.Items.FindByValue(drt.Rows[0]["gid"].ToString()));
                      imgbtngo_Click(sender,e);
               
            }
            else
            {
               
                ddlSearchByStore_SelectedIndexChanged(sender, e);
            }
        }
    }

    protected void fillstore()
    {  
        ddlSearchByStore.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlSearchByStore.DataSource = ds;
        ddlSearchByStore.DataTextField = "Name";
        ddlSearchByStore.DataValueField = "WareHouseId";
        ddlSearchByStore.DataBind();



        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlSearchByStore.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }

       
    
           
           
    }


    protected void fillddlclass()
    {
        ddlclass.Items.Clear();
        string str="";

        str = "select [ClassCompanyMaster].[id],[ClassTypeCompanyMaster].displayname,[ClassCompanyMaster].[classtypecompanymasterid],[ClassCompanyMaster].[displayname],[ClassTypeCompanyMaster].[displayname] + ':' + [ClassCompanyMaster].[displayname] as classtype from [ClassTypeCompanyMaster] inner join [ClassCompanyMaster] on [ClassCompanyMaster].[classtypecompanymasterid] = [ClassTypeCompanyMaster].[id] where [ClassTypeCompanyMaster].Whid = '" + ddlSearchByStore.SelectedValue + "' and [ClassTypeCompanyMaster].Whid='" + ddlSearchByStore.SelectedValue + "'  ";
        SqlCommand cmd = new SqlCommand(str,con);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        dtp.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlclass.DataSource = ds;
            ddlclass.DataTextField = "classtype";
            ddlclass.DataValueField = "id";
          
        }
        ddlclass.DataBind();
        ddlclass.Items.Insert(0, "-Select-");
        ddlclass.Items[0].Value = "0";
       ddlgroup.Items.Clear();
        ddlgroup.Items.Insert(0, "-Select-");
        ddlgroup.Items[0].Value = "0";
    }
   

    protected void ddlclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        string str = "";
        Panel1.Visible = false;
        ddlgroup.Items.Clear();
        if (ddlclass.SelectedIndex > 0)
        {
           

            str = "select GroupCompanyMaster.GroupId,[GroupCompanyMaster].id,[GroupCompanyMaster].groupdisplayname,[ClassCompanyMaster].id,[ClassCompanyMaster].displayname,[GroupCompanyMaster].groupdisplayname as Groupname from [GroupCompanyMaster] inner join [ClassCompanyMaster] on [ClassCompanyMaster].id = [GroupCompanyMaster].classcompanymasterid where [GroupCompanyMaster].whid = '" + ddlSearchByStore.SelectedValue + "' and [GroupCompanyMaster].classcompanymasterid = '" + ddlclass.SelectedValue + "' ";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter dtp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            dtp.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlgroup.DataSource = ds;
                ddlgroup.DataTextField = "Groupname";
                ddlgroup.DataValueField = "id";
                ddlgroup.DataBind();
              
            }

            DataTable dlt = select("select classtypeid from [ClassTypeCompanyMaster] inner join [ClassCompanyMaster] on [ClassCompanyMaster].[classtypecompanymasterid] = [ClassTypeCompanyMaster].[id] where [ClassTypeCompanyMaster].Whid = '" + ddlSearchByStore.SelectedValue + "' and [ClassCompanyMaster].[id]='" + ddlclass.SelectedValue + "'");
            if (dlt.Rows.Count > 0)
            {
                if (Convert.ToString( Request.QueryString["genl"])=="asd")
                {
                    pnlds.Visible = true;
                    lbldatefrom.Text = "From Date";
                    txtdatefrom.Visible = true;

                    lbldateto.Visible = true;


                }
                else
                {
                    pnlds.Visible = false;
                    lbldatefrom.Text = "Date as On ";
                    txtdatefrom.Visible = false;
                    lbldateto.Visible = false;

                }
                Panel1.Visible = true;
            }
        }
        ddlgroup.Items.Insert(0, "-Select-");
        ddlgroup.Items[0].Value = "0";
       
    
    }
    protected void fillgriddata()
    {  DataTable dtseldaq = new DataTable();
    string stsfd = "";
    ViewState["startdate"] = "";
        if (txtdatefrom.Visible == true)
        {
            stsfd = " Startdate<='" + txtdatefrom.Text + "'";
        }
        else
        {
            stsfd = "  Startdate<='" + txtdateto.Text + "'";
        }
        dtseldaq = (DataTable)select("select Top(2) Report_Period_Id,Convert(nvarchar,StartDate,101) as StartDate,Convert(nvarchar,Enddate,101) as Enddate from [ReportPeriod] where" + stsfd + "   and  Whid='" + ddlSearchByStore.SelectedValue + "' order by Enddate Desc");
        if (dtseldaq.Rows.Count > 0)
        {
            lbldatelast.Text = "" + dtseldaq.Rows[1]["StartDate"] + " - " + dtseldaq.Rows[1]["EndDate"] + "";
            ViewState["lastid"] = Convert.ToString(dtseldaq.Rows[1]["Report_Period_Id"]);


            string str = "";
            string strr = "";
            strr = "select Distinct GroupCompanyMaster.GroupId, WareHouseMaster.WareHouseId,WareHouseMaster.[Name],ClassTypeId,[ClassCompanyMaster].[displayname],[GroupCompanyMaster].[id],[GroupCompanyMaster].[groupdisplayname],[AccountMaster].id,AccountMaster.AccountName,AccountMaster.AccountId,[AccountMaster].[Date] from [AccountMaster] inner join [GroupCompanyMaster] on [GroupCompanyMaster].groupid = [AccountMaster].GroupId inner join [ClassCompanyMaster] on [ClassCompanyMaster].id = [GroupCompanyMaster].[classcompanymasterid] inner join ClassTypeCompanyMaster on ClassTypeCompanyMaster.Id=ClassCompanyMaster.classtypecompanymasterid inner join WareHouseMaster on WareHouseMaster.WareHouseId = [AccountMaster].Whid where [AccountMaster].Whid = '" + ddlSearchByStore.SelectedValue + "' and [GroupCompanyMaster].Whid = '" + ddlSearchByStore.SelectedValue + "' and [GroupCompanyMaster].[id] = '" + ddlgroup.SelectedValue + "' ";

            lblstore.Text = ddlSearchByStore.SelectedItem.Text;
            SqlCommand cmd = new SqlCommand(strr, con);
            SqlDataAdapter dtp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dtp.Fill(dt);
            if (dt.Rows.Count > 0)
            {

                //if (txtdatefrom.Text != "" && txtdateto.Text != "")
                //{

                //    ViewState["startdate"] = txtdatefrom.Text;
                //}



                if (dtseldaq.Rows[0]["StartDate"].ToString() != "")
                    {
                        string amte = "";
                        if (Convert.ToString(ViewState["startdate"]) == "")
                        {
                            ViewState["startdate"] = dtseldaq.Rows[0]["StartDate"].ToString();
                        }
                       
                            if (txtdatefrom.Visible == false)
                            {
                                txtdatefrom.Text = Convert.ToDateTime(dtseldaq.Rows[0]["StartDate"]).ToShortDateString();
                            }
                            if (Convert.ToString(dt.Rows[0]["GroupId"]) == "3")
                            {
                                amte = "ABCDR";
                            }
                            else if (Convert.ToString(dt.Rows[0]["GroupId"]) == "38")
                            {
                                amte = "";
                            }
                            // CostGroup.fillcost(ddlSearchByStore.SelectedValue, Convert.ToString(txtdatefrom.Text), Convert.ToString(txtdateto.Text), Convert.ToString(dtseldaq.Rows[0]["Report_Period_Id"]), true, amte);

                            lbldate.Text = "" + Convert.ToDateTime(ViewState["startdate"]).ToShortDateString() + " - " + Convert.ToDateTime(txtdateto.Text).ToShortDateString() + "";
                            str = " and TranctionMaster.Date between '" + Convert.ToDateTime(txtdatefrom.Text) + "' and '" + Convert.ToDateTime(txtdateto.Text) + "'";
                            Session["Sdate"] = ViewState["startdate"].ToString();
                            GridView1.DataSource = dt;
                            DataView myDataView = new DataView();
                            myDataView = dt.DefaultView; ;

                            if (hdnsortExp.Value != string.Empty)
                            {
                                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
                            }


                            GridView1.DataBind();
                       
                    }

                




                double debitlist1 = 0;
                double totcredit = 0;
                double totdebit = 0;
                //  double creditlist1 = 0;
                double debitlist = 0;

                // double creditlist = 0;
                GridView1.FooterRow.Cells[6].Text = "0.00";
                GridView1.FooterRow.Cells[5].Text = "0.00";
                //GridView1.FooterRow.Cells[7].Text = "0.00";
                //GridView1.FooterRow.Cells[8].Text = "0.00";

                foreach (GridViewRow fff1 in GridView1.Rows)
                {
                    Label lblaccountID = (Label)fff1.FindControl("lblaccid");
                    Label lblbaldr = (Label)fff1.FindControl("lblcurbaldr");
                    Label lbldr = (Label)fff1.FindControl("lbldr");
                    Label lblcr = (Label)fff1.FindControl("lblcr");

                    Label lblballstdr = (Label)fff1.FindControl("lbllstbaldr");

                    Label lblctid = (Label)fff1.FindControl("lblctid");

                    double debit = 0;
                    double credit = 0;
                    double oppbal = 0;
                    double oppdeb = 0;
                    double oppcre = 0;
                    if (Convert.ToDateTime(ViewState["startdate"]) != Convert.ToDateTime(txtdatefrom.Text))
                    {
                        string balstr = " and TranctionMaster.Date between '" + Convert.ToDateTime(ViewState["startdate"]).ToShortDateString() + "' and '" + Convert.ToDateTime(txtdatefrom.Text).AddDays(-1).ToShortDateString() + "'";


                        DataTable dtopp1 = (DataTable)select("Select sum(AmountCredit) as AmountCredit from Tranction_Details inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=Tranction_Details.Tranction_Master_Id where Tranction_Details.Whid='" + ddlSearchByStore.SelectedValue + "' and TranctionMaster.Whid='" + ddlSearchByStore.SelectedValue + "' and AccountCredit='" + lblaccountID.Text + "'  " + balstr + "");
                        if (dtopp1.Rows.Count > 0)
                        {
                            if (dtopp1.Rows[0]["AmountCredit"].ToString() != "")
                            {

                                oppcre = Convert.ToDouble(dtopp1.Rows[0]["AmountCredit"].ToString());

                            }
                        }


                        DataTable dtopp2 = (DataTable)select("Select sum(AmountDebit) as AmountDebit from Tranction_Details inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=Tranction_Details.Tranction_Master_Id where Tranction_Details.Whid='" + ddlSearchByStore.SelectedValue + "' and TranctionMaster.Whid='" + ddlSearchByStore.SelectedValue + "' and AccountDebit='" + lblaccountID.Text + "'  " + balstr + "");
                        if (dtopp2.Rows.Count > 0)
                        {
                            if (dtopp2.Rows[0]["AmountDebit"].ToString() != "")
                            {

                                oppdeb = Convert.ToDouble(dtopp2.Rows[0]["AmountDebit"].ToString());

                            }
                        }
                        oppbal = oppdeb - oppcre;

                    }



                    DataTable dtcredit = new DataTable();
                    dtcredit = (DataTable)select("Select sum(AmountCredit) as AmountCredit from Tranction_Details inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=Tranction_Details.Tranction_Master_Id where Tranction_Details.Whid='" + ddlSearchByStore.SelectedValue + "' and TranctionMaster.Whid='" + ddlSearchByStore.SelectedValue + "' and AccountCredit='" + lblaccountID.Text + "'  " + str + "");
                    if (dtcredit.Rows.Count > 0)
                    {
                        if (dtcredit.Rows[0]["AmountCredit"].ToString() != "")
                        {

                            credit = Convert.ToDouble(dtcredit.Rows[0]["AmountCredit"].ToString());
                            totcredit += credit;
                        }
                    }
                    lblcr.Text = String.Format("{0:n}", credit);
                    DataTable dtamtd1 = new DataTable();
                    dtamtd1 = (DataTable)select("Select sum(AmountDebit) as AmountDebit from Tranction_Details inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=Tranction_Details.Tranction_Master_Id where Tranction_Details.Whid='" + ddlSearchByStore.SelectedValue + "' and TranctionMaster.Whid='" + ddlSearchByStore.SelectedValue + "' and AccountDebit='" + lblaccountID.Text + "'  " + str + "");
                    if (dtamtd1.Rows.Count > 0)
                    {
                        if (dtamtd1.Rows[0]["AmountDebit"].ToString() != "")
                        {

                            debit = Convert.ToDouble(dtamtd1.Rows[0]["AmountDebit"].ToString());
                            totdebit += debit;
                        }
                    }

                    lbldr.Text = String.Format("{0:n}", debit);



                    double amtfi = Lastfig(lblaccountID, lblctid);
                    double latestamt = 0;
                    if (lblctid.Text == "15" || lblctid.Text == "19")
                    {
                        latestamt = ((oppbal) + (debit - credit));
                    }
                    else
                    {
                        latestamt = ((amtfi + oppbal) + (debit - credit));
                    }
                    lblbaldr.Text = String.Format("{0:n}", Convert.ToDecimal(latestamt));
                    debitlist += latestamt;
                    //if (amtfi > 0)
                    //{
                    lblballstdr.Text = String.Format("{0:n}", Convert.ToDecimal(amtfi + oppbal));
                    debitlist1 += (amtfi + oppbal);
                    //}
                    //else
                    //{
                    //    amtfi = (-1) * amtfi;
                    //    lbllstbalcr.Text = String.Format("{0:n}", Convert.ToDecimal(amtfi));
                    //    creditlist1 += amtfi;
                    //}




                }

                if (GridView1.Rows.Count > 0)
                {

                    GridView1.FooterRow.Cells[5].Text = String.Format("{0:n}", Convert.ToDecimal(debitlist1));
                    GridView1.FooterRow.Cells[6].Text = String.Format("{0:n}", Convert.ToDecimal(totdebit));
                    GridView1.FooterRow.Cells[7].Text = String.Format("{0:n}", Convert.ToDecimal(totcredit));
                    GridView1.FooterRow.Cells[8].Text = String.Format("{0:n}", Convert.ToDecimal(debitlist));
                    // GridView1.FooterRow.Cells[8].Text = String.Format("{0:n}", Convert.ToDecimal(creditlist1));
                }
            }
        }
    }
    protected double Lastfig(Label lblaccountID, Label lblctid)
    {
        double salballst = 0;
        DataTable dtbal = new DataTable();
        if (Convert.ToString(lblctid.Text) == "15" || Convert.ToString(lblctid.Text) == "19")
        {
            dtbal = (DataTable)select("Select Balance  from AccountBalance_InStatment where AccountMasterId=(select (Id) as Id from AccountMaster where AccountMaster.AccountId='" + lblaccountID.Text + "' and   Whid='" + ddlSearchByStore.SelectedValue + "' ) and Report_Period_Id='" + ViewState["lastid"] + "'");
        }
        else
        {
            dtbal = (DataTable)select("Select Balance  from AccountBalance where AccountMasterId=(select  Top(1)(Id) as Id from AccountMaster where AccountMaster.AccountId='" + lblaccountID.Text + "' and   Whid='" + ddlSearchByStore.SelectedValue + "' ) and Report_Period_Id='" + ViewState["lastid"] + "'");

        }
            if (dtbal.Rows.Count > 0)
        {
            if (dtbal.Rows[0]["Balance"].ToString() != "")
            {
               
                salballst = Convert.ToDouble(dtbal.Rows[0]["Balance"].ToString());
              
            }
        }
        return salballst;
    }
     protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        fillgriddata();
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
    protected void imgbtngo_Click(object sender, EventArgs e)
    {
        //if (ddlgroup.SelectedIndex > 0)
        //{
          
        //        Session["date"] = txtdatefrom.Text;
        //        Session["Sdate"] = txtdatefrom.Text;
        //        Session["Edate"] = txtdateto.Text;
        //        GridView1.DataSource = null;
        //        GridView1.DataBind();
        //        lbldate.Text = "";
        //        fillgriddata();
           
            
            
        //}
        if (ddlgroup.SelectedIndex > 0)
        {

            Session["date"] = txtdatefrom.Text;
            Session["Sdate"] = txtdatefrom.Text;
            Session["Edate"] = txtdateto.Text;
            GridView1.DataSource = null;
            GridView1.DataBind();
            lblmsg.Text = "";
            if (txtdatefrom.Visible == true)
            {

                DateTime dt2 = Convert.ToDateTime(txtdatefrom.Text);
                DateTime dt1 = Convert.ToDateTime(txtdateto.Text);
                if (dt1 < dt2)
                {

                    lblmsg.Visible = true;
                    lblmsg.Text = " Start Date Must Be Less than End Date";


                }
                else
                {
                    fillgriddata();
                }
            }
            else
            {
                fillgriddata();
            }


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
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    double dbdramt = 0;
        //    double dbcramt = 0;
        //    double dbdrcrsum = 0;
        //    Label lblaccid = (Label)e.Row.FindControl("lblaccid");
        //    Label lblbaldr = (Label)e.Row.FindControl("lblcurbaldr");
        //    Label lblbalcr = (Label)e.Row.FindControl("lblcurbalcr");
        //    Label lbllstdr = (Label)e.Row.FindControl("lbllstbaldr");
        //    Label lbllstcr = (Label)e.Row.FindControl("lbllstbalcr");
        //    DataTable dtdr = new DataTable();
            
        //    //dtdr = (DataTable)select("Select Sum(AmountDebit) as Drsum from Tranction_Details where AccountDebit='" + lblaccid.Text + "'" + str);
        //    dtdr = (DataTable)select("Select Sum(AmountCredit) as Drsum from Tranction_Details inner join [TranctionMaster] on [Tranction_Details].Tranction_Details_Id = [TranctionMaster].Tranction_Master_Id where AccountDebit='" + lblaccid.Text + "'" + str);

        //    if (dtdr.Rows.Count > 0)
        //    {
        //        if (dtdr.Rows[0]["Drsum"].ToString() != "")
        //        {
        //            dbdramt = Convert.ToDouble(dtdr.Rows[0]["Drsum"].ToString());
        //        }
               
        //    }
        //    DataTable dtcr = new DataTable();
        //    //dtcr = (DataTable)select("Select Sum(AmountCredit) as Crsum from Tranction_Details where AccountCredit='" + lblaccid.Text + "'" + str);
        //    dtdr = (DataTable)select("Select Sum(AmountCredit) as Crsum from Tranction_Details inner join [TranctionMaster] on [Tranction_Details].Tranction_Details_Id = [TranctionMaster].Tranction_Master_Id where AccountCredit ='" + lblaccid.Text + "'" + str);
        //    if (dtcr.Rows.Count > 0)
        //    {
        //        if (dtcr.Rows[0]["Crsum"].ToString() != "")
        //        {
        //            dbcramt = Convert.ToDouble(dtcr.Rows[0]["Crsum"].ToString());
        //        }                
        //    }

        //    dbdrcrsum = dbdramt - dbcramt;
        //    if (dbdrcrsum >= 0)
        //    {
        //        lblbaldr.Text = Convert.ToString(dbdrcrsum);
        //    }
        //    else
        //    {
        //        lblbalcr.Text = Convert.ToString(dbdrcrsum);
        //    }

        //    DataTable dtballast = new DataTable();
        //    //dtballast = (DataTable)select("Select BalanceOfLastYear from AccountMaster where AccountId='" + lblaccid.Text + "' and AccountMaster.compid = '" + compid + "'");
        //    dtballast = (DataTable)select("Select BalanceOfLastYear from AccountMaster where Id='" + lblaccid.Text + "' and AccountMaster.Whid = '" + ddlSearchByStore.SelectedValue + "'");

        //    if (dtballast.Rows.Count > 0)
        //    {
        //        if (dtballast.Rows[0]["BalanceOfLastYear"].ToString() != "")
        //        {
        //            //Label lbllastbal = (Label)e.Row.FindControl("lbllstbaldr");
        //            if (Convert.ToDouble(dtballast.Rows[0]["BalanceOfLastYear"].ToString()) > 0)
        //            {
        //                lbllstdr.Text = dtballast.Rows[0]["BalanceOfLastYear"].ToString();
        //            }
        //            else if (Convert.ToDouble(dtballast.Rows[0]["BalanceOfLastYear"].ToString()) < 0)
        //            {
        //                lbllstcr.Text = dtballast.Rows[0]["BalanceOfLastYear"].ToString();
        //            }
                    
        //        }
        //    }
        //}
    }
 

    protected void txtdatefrom_TextChanged(object sender, EventArgs e)
    {

    }
    protected void ddlperiod_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlSearchByStore_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillddlclass();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (Button2.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
           // pnlgrid.Height = new Unit("100%");

            Button2.Text = "Hide Printable Version";
            Button3.Visible = true;



        }
        else
        {
            pnlgrid.ScrollBars = ScrollBars.Both;
            //pnlgrid.Height = new Unit(250);

            Button2.Text = "Printable Version";
            Button3.Visible = false;

           
        }
    }


    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.Header)
        //{
        //    GridView HeaderGrid = (GridView)sender;
        //    GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
        //    TableCell HeaderCell = new TableCell();
        //    HeaderCell.Text = "";
        //    HeaderCell.ColumnSpan = 4;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
           
        //        HeaderCell.Text = "Current Year<br />" + lbldate.Text;
          
        //    HeaderCell.ColumnSpan = 1;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.BackColor = System.Drawing.Color.FromArgb(65, 98, 113);
        //    HeaderCell.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "Last Year ended on<br />" + lblcurrentdate.Text+lbldatelast.Text;
        //    HeaderCell.ColumnSpan = 1;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.BackColor = System.Drawing.Color.FromArgb(65, 98, 113);
        //    HeaderCell.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
        //    HeaderGridRow.Cells.Add(HeaderCell);


        

        //    GridView1.Controls[0].Controls.AddAt(0, HeaderGridRow);

        //}
    }
   
}
