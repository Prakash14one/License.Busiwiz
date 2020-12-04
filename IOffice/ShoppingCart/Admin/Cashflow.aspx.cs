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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using iTextSharp.text.html.simpleparser;

public partial class Cashflow : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(PageConn.connnn);
    string compid;
    public static string str = "";
    public static string acid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //PageConn pgcon = new PageConn();
        //con = pgcon.dynconn; 
        lblmsg.Visible = false;
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();
        compid = Session["Comid"].ToString();
        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();


        Page.Title = pg.getPageTitle(page);
        if (!IsPostBack)
        {
            lblcompname.Text = Convert.ToString(Session["cname"]);

           // pageMailAccess();

            DataTable ds = ClsStore.SelectStorename();
            ddwarehouse.DataSource = ds;
            ddwarehouse.DataTextField = "Name";
            ddwarehouse.DataValueField = "WareHouseId";
            ddwarehouse.DataBind();
            if (Request.QueryString["wid"] != null)
            {
                if (Convert.ToString(Session["edate"]) != "")
                {
                    txtdateto.Text = Convert.ToString(Session["edate"]);
                }
                else
                {
                    txtdateto.Text = DateTime.Now.ToShortDateString();
                }
                ddwarehouse.SelectedValue = Request.QueryString["wid"];
                imgbtngo_Click(sender, e);
            }
            else
            {
                txtdateto.Text = DateTime.Now.ToShortDateString();

                DataTable dteeed = ClsStore.SelectEmployeewithIdwise();
                if (dteeed.Rows.Count > 0)
                {
                    ddwarehouse.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
                }
            }
        }
    }
    protected void pageMailAccess()
    {
        ddlExport.Items.Insert(0, "Export Type");
        ddlExport.Items[0].Value = "0";
        //ddlExport.Items.Insert(1, "Export to PDF");
        //ddlExport.Items[1].Value = "1";
        ddlExport.Items.Insert(1, "Export to Excel");
        ddlExport.Items[1].Value = "2";
        ddlExport.Items.Insert(2, "Export to Word");
        ddlExport.Items[2].Value = "3";
        DataTable drt = select("SELECT distinct RoleMenuAccessRightTbl.MenuId,PageMaster.PageName FROM MainMenuMaster inner join RoleMenuAccessRightTbl on RoleMenuAccessRightTbl.MenuId=MainMenuMaster.MainMenuId inner join PageMaster on PageMaster.MainMenuId=RoleMenuAccessRightTbl.MenuId  inner join pageplaneaccesstbl on pageplaneaccesstbl.Pageid=PageMaster.PageId  INNER JOIN  User_Role ON RoleMenuAccessRightTbl.RoleId = User_Role.Role_id INNER JOIN User_master ON User_Role.User_id = User_master.UserID where pageplaneaccesstbl.Priceplanid='" + Session["PriceId"] + "' and PageMaster.PageName='MessageCompose.aspx' and PageMaster.VersionInfoMasterId='" + Session["verId"] + "' and  User_master.UserID ='" + Session["userid"] + "'");
        if (drt.Rows.Count <= 0)
        {

            drt = select("SELECT PageMaster.PageName FROM PageMaster inner join RolePageAccessRightTbl on RolePageAccessRightTbl.PageId=PageMaster.PageId inner join pageplaneaccesstbl on pageplaneaccesstbl.Pageid=PageMaster.PageId INNER JOIN User_Role ON RolePageAccessRightTbl.RoleId = User_Role.Role_id INNER JOIN User_master ON User_Role.User_id = User_master.UserID where pageplaneaccesstbl.Priceplanid='" + Session["PriceId"] + "' and PageMaster.PageName='MessageCompose.aspx' and PageMaster.VersionInfoMasterId='" + Session["verId"] + "' and  User_master.UserID ='" + Session["userid"] + "'");
            if (drt.Rows.Count <= 0)
            {
                drt = select("SELECT distinct PageMaster.PageName FROM MainMenuMaster inner join  SubMenuMaster on SubMenuMaster.MainMenuId=MainMenuMaster.MainMenuId inner join RoleSubMenuAccessRightTbl on RoleSubMenuAccessRightTbl.SubMenuId=SubMenuMaster.SubMenuId inner join PageMaster on PageMaster.SubMenuId=RoleSubMenuAccessRightTbl.SubMenuId  inner join pageplaneaccesstbl on pageplaneaccesstbl.Pageid=PageMaster.PageId  INNER JOIN  User_Role ON RoleSubMenuAccessRightTbl.RoleId = User_Role.Role_id INNER JOIN User_master ON User_Role.User_id = User_master.UserID where pageplaneaccesstbl.Priceplanid='" + Session["PriceId"] + "' and PageMaster.PageName='MessageCompose.aspx' and PageMaster.VersionInfoMasterId='" + Session["verId"] + "' and  User_master.UserID ='" + Session["userid"] + "'");
                if (drt.Rows.Count <= 0)
                {
                    //ddlExport.Items.Insert(3, "Email with Word");
                    //ddlExport.Items[3].Value = "4";

                }
                else
                {
                    ddlExport.Items.Insert(3, "Email with Word");
                    ddlExport.Items[3].Value = "4";
                }

            }
            else
            {
                ddlExport.Items.Insert(3, "Email with Word");
                ddlExport.Items[3].Value = "4";

            }


        }
        else
        {

            ddlExport.Items.Insert(3, "Email with Word");
            ddlExport.Items[3].Value = "4";

        }
    }

    protected void fillcostgood(string groupid, Label currentlbl, Label lastlbl)
    {
        Decimal asonbal = 0;
        //DataTable dtod = (DataTable)select("select StartDate,Enddate from [ReportPeriod] where Whid='" + ddwarehouse.SelectedValue + "' and Active='1'");
        //if (dtod.Rows.Count > 0)
        //{
        //    Session["date"] = lblstartdate.Text;
        //    Session["edate"] = lblenddate.Text;

        //}
        DataTable dtprodex = new DataTable();
        Decimal deb = 0;
        Decimal crd = 0;
        Decimal debcreamt = 0;
        Decimal salballst = 0;


        DataTable dtamtd = new DataTable();
        dtamtd = (DataTable)select("Select sum(AmountDebit) as amtb from AccountMaster inner join Tranction_Details on Tranction_Details.AccountDebit=AccountMaster.AccountId  inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=Tranction_Details.Tranction_Master_Id where GroupId = '" + groupid + "' and AccountMaster.Whid='" + ddwarehouse.SelectedValue + "' and TranctionMaster.Whid='" + ddwarehouse.SelectedValue + "' and  Tranction_Details.whid='" + ddwarehouse.SelectedValue + "'" + str);
        if (dtamtd.Rows.Count > 0)
        {
            if (dtamtd.Rows[0]["amtb"].ToString() != "")
            {
                Decimal d = 0;
                d = Convert.ToDecimal(dtamtd.Rows[0]["amtb"].ToString());
                deb = deb + d;
            }
        }

        DataTable dtamtc = new DataTable();
        dtamtc = (DataTable)select("Select sum(AmountCredit) as amtc from AccountMaster inner join Tranction_Details on Tranction_Details.AccountCredit=AccountMaster.AccountId inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=Tranction_Details.Tranction_Master_Id where GroupId = '" + groupid + "' and TranctionMaster.Whid='" + ddwarehouse.SelectedValue + "' and  AccountMaster.Whid='" + ddwarehouse.SelectedValue + "' and Tranction_Details.whid='" + ddwarehouse.SelectedValue + "' " + str);
        if (dtamtc.Rows.Count > 0)
        {
            if (dtamtc.Rows[0]["amtc"].ToString() != "")
            {
                Decimal c = 0;
                c = Convert.ToDecimal(dtamtc.Rows[0]["amtc"].ToString());
                crd = crd + c;
            }
        }

        DataTable dtbal = new DataTable();
        dtbal = (DataTable)select("Select sum(Cast(Balance as Decimal(18,2))) as Balance  from AccountBalance_InStatment where AccountMasterId in(select (Id) as Id from AccountMaster where GroupId = '" + groupid + "' and   Whid='" + ddwarehouse.SelectedValue + "' ) and Report_Period_Id='" + ViewState["period"] + "'");
        if (dtbal.Rows.Count > 0)
        {
            if (dtbal.Rows[0]["Balance"].ToString() != "")
            {
                Decimal bal = 0;
                bal = Convert.ToDecimal(dtbal.Rows[0]["Balance"].ToString());
                salballst = salballst + bal;
            }
        }


        debcreamt = asonbal + (deb - crd);
        if (debcreamt >= 0)
        {
            //debcreamt = debcreamt * -1;
            currentlbl.Text = Convert.ToString(debcreamt);
        }
        else
        {
            //debcreamt = debcreamt - (2 * debcreamt);
            currentlbl.Text = Convert.ToString(debcreamt);
        }
        tw(currentlbl);
        if (salballst >= 0)
        {
            //salballst = salballst * -1;
            lastlbl.Text = Convert.ToString(salballst);
        }
        else
        {
            //salballst = salballst - (2 * salballst);
            lastlbl.Text = Convert.ToString(salballst);
        }
        tw(lastlbl);
    }
    protected void filldatagroup(string groupid, Label currentlbl, Label lastlbl)
    {
        DataTable dtprodex = new DataTable();

        double deb = 0;
        double crd = 0;
        double debcreamt = 0;
        double salballst = 0;


        DataTable dtamtd = new DataTable();
        dtamtd = (DataTable)select("Select sum(AmountDebit) as amtb from AccountMaster inner join Tranction_Details on Tranction_Details.AccountDebit=AccountMaster.AccountId  inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=Tranction_Details.Tranction_Master_Id where GroupId = '" + groupid + "' and AccountMaster.Whid='" + ddwarehouse.SelectedValue + "' and TranctionMaster.Whid='" + ddwarehouse.SelectedValue + "' and  Tranction_Details.whid='" + ddwarehouse.SelectedValue + "'" + str);
        if (dtamtd.Rows.Count > 0)
        {
            if (dtamtd.Rows[0]["amtb"].ToString() != "")
            {
                double d = 0;
                d = Convert.ToDouble(dtamtd.Rows[0]["amtb"].ToString());
                deb = deb + d;
            }
        }

        DataTable dtamtc = new DataTable();
        dtamtc = (DataTable)select("Select sum(AmountCredit) as amtc from AccountMaster inner join Tranction_Details on Tranction_Details.AccountCredit=AccountMaster.AccountId inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=Tranction_Details.Tranction_Master_Id where GroupId = '" + groupid + "' and TranctionMaster.Whid='" + ddwarehouse.SelectedValue + "' and  AccountMaster.Whid='" + ddwarehouse.SelectedValue + "' and Tranction_Details.whid='" + ddwarehouse.SelectedValue + "' " + str);
        if (dtamtc.Rows.Count > 0)
        {
            if (dtamtc.Rows[0]["amtc"].ToString() != "")
            {
                double c = 0;
                c = Convert.ToDouble(dtamtc.Rows[0]["amtc"].ToString());
                crd = crd + c;
            }
        }

        DataTable dtbal = new DataTable();
        dtbal = (DataTable)select("Select sum(Cast(Balance as Decimal(18,2))) as Balance  from AccountBalance_InStatment where AccountMasterId in(select (Id) as Id from AccountMaster where GroupId = '" + groupid + "' and   Whid='" + ddwarehouse.SelectedValue + "' ) and Report_Period_Id='" + ViewState["period"] + "'");
        if (dtbal.Rows.Count > 0)
        {
            if (dtbal.Rows[0]["Balance"].ToString() != "")
            {
                double bal = 0;
                bal = Convert.ToDouble(dtbal.Rows[0]["Balance"].ToString());
                salballst = salballst + bal;
            }
        }


        debcreamt = deb - crd;
        if (debcreamt >= 0)
        {
            debcreamt = debcreamt * -1;
            currentlbl.Text = Convert.ToString(debcreamt);
        }
        else
        {
            debcreamt = debcreamt - (2 * debcreamt);
            currentlbl.Text = Convert.ToString(debcreamt);
        }
        tw(currentlbl);
        if (salballst >= 0)
        {
            salballst = salballst * -1;
            lastlbl.Text = Convert.ToString(salballst);
        }
        else
        {
            salballst = salballst - (2 * salballst);
            lastlbl.Text = Convert.ToString(salballst);
        }
        tw(lastlbl);
    }
    protected void incomestatement()
    {

        DataTable dtdaopb = new DataTable();

        dtdaopb = (DataTable)select("Select Report_Period_Id from [ReportPeriod] where ReportPeriod.EndDate<(select EndDate from [ReportPeriod] where Whid='" + ddwarehouse.SelectedValue + "' and Active='1') and  Whid='" + ddwarehouse.SelectedValue + "' order by EndDate Desc");
        ViewState["period1"] = "0";
        if (dtdaopb.Rows.Count > 0)
        {
            ViewState["period"] = dtdaopb.Rows[0]["Report_Period_Id"].ToString();
            if (dtdaopb.Rows.Count > 1)
            {
                ViewState["period1"] = dtdaopb.Rows[1]["Report_Period_Id"].ToString();

            }
        }

        ////// Code for Sales(net) and costofgoodssold currentyear amt and lastyear amt calculation /////////////
        //start with line no 119 and ended at 287 //////
        DataTable dtfillgrid = new DataTable();
        dtfillgrid = (DataTable)select("SELECT     GroupId, groupdisplayname " +
        " FROM         GroupCompanyMaster where GroupId in ('33','38','39') and Whid='" + ddwarehouse.SelectedValue + "' order by GroupId");
        if (dtfillgrid.Rows.Count > 0)
        {
            lblsalesnet.Text = dtfillgrid.Rows[0]["groupdisplayname"].ToString();
            lblcstofgoodsold.Text = dtfillgrid.Rows[1]["groupdisplayname"].ToString();
            lblproductionexpenses.Text = dtfillgrid.Rows[2]["groupdisplayname"].ToString();

        }
        filldatagroup(Convert.ToString(dtfillgrid.Rows[0]["GroupId"]), lblsalamt, lblsalamtlst);
        pnldisplaycost(lblsalamt, lblsalamtlst, pnlsalesnet);
        tw(lblsalamt);
        tw(lblsalamtlst);


        fillcostgood(Convert.ToString(dtfillgrid.Rows[1]["GroupId"]), lblcstofgoodsoldamt, lblcofglst);

        pnldisplaycost(lblcstofgoodsoldamt, lblcofglst, pnlcostofgoods);
        tw(lblcstofgoodsoldamt);
        tw(lblcofglst);
        //fillcost();




        filldatagroup(Convert.ToString(dtfillgrid.Rows[2]["GroupId"]), lblproductionexpensesamt, lblproductionexplst);
        pnldisplaycost(lblproductionexpensesamt, lblproductionexplst, pnlproductexp);
        tw(lblproductionexpensesamt);
        tw(lblproductionexplst);
        if (Convert.ToDecimal(lblproductionexpensesamt.Text) < 0)
        {
            lblproductionexpensesamt.Text = Convert.ToString(Convert.ToDecimal(lblproductionexpensesamt.Text) * -1);
        }
        if (Convert.ToDecimal(lblproductionexplst.Text) < 0)
        {
            lblproductionexplst.Text = Convert.ToString(Convert.ToDecimal(lblproductionexplst.Text) * -1);
        }

        if (lblsalamt.Text != "" && lblcstofgoodsoldamt.Text != "")
        {
            Decimal c = 0;
            c = Convert.ToDecimal(lblsalamt.Text) - Convert.ToDecimal(lblcstofgoodsoldamt.Text);
            lblgrossamt.Text = Convert.ToString(Math.Round(c, 2));
            lblgrossprofit.Text = "Gross Profit";
            tw(lblgrossamt);
        }

        if (lblcofglst.Text != "" && lblsalamtlst.Text != "")
        {
            Decimal c1 = 0;
            c1 = Convert.ToDecimal(lblsalamtlst.Text) - Convert.ToDecimal(lblcofglst.Text);
            lblgrossamtlst.Text = Convert.ToString(Math.Round(c1, 2));
            tw(lblgrossamtlst);
        }
        ////// Code for Sales(net) and costofgoodssold currentyear amt and lastyear amt calculation ended  /////////////
        //start with line no 119  and ended at 287 //////

        //////code start for Salexp,gen& admexp,Depriciation currentyear amt and lastyear amt calculation /////////////
        //start with line no 294  and ended at 467 //////
        DataTable dtexp = new DataTable();
        dtexp = (DataTable)select("SELECT     GroupId, groupdisplayname " +
        " FROM         GroupCompanyMaster where GroupId in ('34','35','36','43')and Whid='" + ddwarehouse.SelectedValue + "' order by GroupId");
        if (dtexp.Rows.Count > 0)
        {
            lblotheropratingexandloss.Text = dtexp.Rows[0]["groupdisplayname"].ToString();
            lblsalexp.Text = dtexp.Rows[1]["groupdisplayname"].ToString();
            lblgenadmexp.Text = dtexp.Rows[2]["groupdisplayname"].ToString();
            lbldep.Text = dtexp.Rows[3]["groupdisplayname"].ToString();

            DataTable dtaccid = new DataTable();
            dtaccid = (DataTable)select("Select Distinct AccountId from AccountMaster where GroupId = '" + dtexp.Rows[1]["GroupId"].ToString() + "' " + acid.ToString() + "");
            Decimal sedr = 0;
            Decimal secr = 0;
            Decimal sedrcr = 0;
            Decimal seballst = 0;
            foreach (DataRow dtr in dtaccid.Rows)
            {
                DataTable dtgr35d = new DataTable();
                dtgr35d = (DataTable)select("Select sum(AmountDebit) as amtb from Tranction_Details inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=Tranction_Details.Tranction_Master_Id where TranctionMaster.Whid='" + ddwarehouse.SelectedValue + "' and  Tranction_Details.whid='" + ddwarehouse.SelectedValue + "'  and AccountDebit='" + dtr["AccountId"].ToString() + "'" + str);
                if (dtgr35d.Rows.Count > 0)
                {
                    if (dtgr35d.Rows[0]["amtb"].ToString() != "")
                    {
                        Decimal d = 0;
                        d = Convert.ToDecimal(dtgr35d.Rows[0]["amtb"].ToString());
                        sedr = sedr + d;
                    }
                }
                //Decimal deb = 0;
                DataTable dtgr35c = new DataTable();
                dtgr35c = (DataTable)select("Select sum(AmountCredit) as amtc from Tranction_Details inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=Tranction_Details.Tranction_Master_Id where TranctionMaster.Whid='" + ddwarehouse.SelectedValue + "' and  Tranction_Details.whid='" + ddwarehouse.SelectedValue + "'  and AccountCredit='" + dtr["AccountId"].ToString() + "'" + str);
                if (dtgr35c.Rows.Count > 0)
                {
                    if (dtgr35c.Rows[0]["amtc"].ToString() != "")
                    {
                        Decimal c = 0;
                        c = Convert.ToDecimal(dtgr35c.Rows[0]["amtc"].ToString());
                        secr = secr + c;
                    }
                }

                DataTable dtselst = new DataTable();
                dtselst = (DataTable)select("Select Balance from AccountBalance_InStatment where AccountMasterId=(select Max(Id) as Id from AccountMaster where AccountId='" + dtr["AccountId"].ToString() + "' and Whid='" + ddwarehouse.SelectedValue + "' ) and Report_Period_Id='" + ViewState["period"] + "'");
                if (dtselst.Rows.Count > 0)
                {
                    if (dtselst.Rows[0]["Balance"].ToString() != "")
                    {
                        Decimal bal = 0;
                        bal = Convert.ToDecimal(dtselst.Rows[0]["Balance"].ToString());
                        seballst = seballst + bal;
                    }
                }

            }
            sedrcr = sedr - secr;
            lblsalexpamt.Text = Convert.ToString(sedrcr);
            lblsalexplst.Text = Convert.ToString(seballst);
            tw(lblsalexpamt);
            tw(lblsalexplst);
            pnldisplaycost(lblsalexpamt, lblsalexplst, pnlsalesmarketexp);
            DataTable dtaccid1 = new DataTable();
            dtaccid1 = (DataTable)select("Select Distinct AccountId from AccountMaster where GroupId = '" + dtexp.Rows[2]["GroupId"].ToString() + "' " + acid.ToString() + "");
            Decimal admdr = 0;
            Decimal admcr = 0;
            Decimal admdrcr = 0;
            Decimal admballst = 0;
            foreach (DataRow dtr in dtaccid1.Rows)
            {
                DataTable dtgr36d = new DataTable();
                dtgr36d = (DataTable)select("Select sum(AmountDebit) as amtb from Tranction_Details inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=Tranction_Details.Tranction_Master_Id where TranctionMaster.Whid='" + ddwarehouse.SelectedValue + "' and  Tranction_Details.whid='" + ddwarehouse.SelectedValue + "'  and AccountDebit='" + dtr["AccountId"].ToString() + "'" + str);
                if (dtgr36d.Rows.Count > 0)
                {
                    if (dtgr36d.Rows[0]["amtb"].ToString() != "")
                    {
                        Decimal d = 0;
                        d = Convert.ToDecimal(dtgr36d.Rows[0]["amtb"].ToString());
                        admdr = admdr + d;
                    }
                }
                //Decimal deb = 0;
                DataTable dtgr36c = new DataTable();
                dtgr36c = (DataTable)select("Select sum(AmountCredit) as amtc from Tranction_Details inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=Tranction_Details.Tranction_Master_Id where TranctionMaster.Whid='" + ddwarehouse.SelectedValue + "' and  Tranction_Details.whid='" + ddwarehouse.SelectedValue + "'  and AccountCredit='" + dtr["AccountId"].ToString() + "'" + str);
                if (dtgr36c.Rows.Count > 0)
                {
                    if (dtgr36c.Rows[0]["amtc"].ToString() != "")
                    {
                        Decimal c = 0;
                        c = Convert.ToDecimal(dtgr36c.Rows[0]["amtc"].ToString());
                        admcr = admcr + c;
                    }
                }

                DataTable dtadmlst = new DataTable();
                dtadmlst = (DataTable)select("Select Balance from AccountBalance_InStatment where AccountMasterId=(select Max(Id) as Id from AccountMaster where AccountId='" + dtr["AccountId"].ToString() + "' and Whid='" + ddwarehouse.SelectedValue + "' ) and Report_Period_Id='" + ViewState["period"] + "'");
                if (dtadmlst.Rows.Count > 0)
                {
                    if (dtadmlst.Rows[0]["Balance"].ToString() != "")
                    {
                        Decimal bal = 0;
                        bal = Convert.ToDecimal(dtadmlst.Rows[0]["Balance"].ToString());
                        admballst = admballst + bal;
                    }
                }

            }
            admdrcr = admdr - admcr;
            lblgenadmamt.Text = Convert.ToString(admdrcr);
            lblgenadmlst.Text = Convert.ToString(admballst);
            tw(lblgenadmamt);
            tw(lblgenadmlst);
            pnldisplaycost(lblgenadmamt, lblgenadmlst, pnlgenadminexp);



            DataTable dtaccid12 = new DataTable();
            dtaccid12 = (DataTable)select("Select Distinct AccountId from AccountMaster where GroupId = '" + dtexp.Rows[3]["GroupId"].ToString() + "' " + acid.ToString() + "");
            Decimal depdr = 0;
            Decimal depcr = 0;
            Decimal depdrcr = 0;
            Decimal depballst = 0;
            foreach (DataRow dtr in dtaccid12.Rows)
            {
                DataTable dtgr43d = new DataTable();
                dtgr43d = (DataTable)select("Select sum(AmountDebit) as amtb from Tranction_Details inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=Tranction_Details.Tranction_Master_Id where TranctionMaster.Whid='" + ddwarehouse.SelectedValue + "' and  Tranction_Details.whid='" + ddwarehouse.SelectedValue + "'  and AccountDebit='" + dtr["AccountId"].ToString() + "'" + str);
                if (dtgr43d.Rows.Count > 0)
                {
                    if (dtgr43d.Rows[0]["amtb"].ToString() != "")
                    {
                        Decimal d = 0;
                        d = Convert.ToDecimal(dtgr43d.Rows[0]["amtb"].ToString());
                        depdr = depdr + d;
                    }
                }
                //Decimal deb = 0;
                DataTable dtgr43c = new DataTable();
                dtgr43c = (DataTable)select("Select sum(AmountCredit) as amtc from Tranction_Details inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=Tranction_Details.Tranction_Master_Id where TranctionMaster.Whid='" + ddwarehouse.SelectedValue + "' and  Tranction_Details.whid='" + ddwarehouse.SelectedValue + "'  and AccountCredit='" + dtr["AccountId"].ToString() + "'" + str);
                if (dtgr43c.Rows.Count > 0)
                {
                    if (dtgr43c.Rows[0]["amtc"].ToString() != "")
                    {
                        Decimal c = 0;
                        c = Convert.ToDecimal(dtgr43c.Rows[0]["amtc"].ToString());
                        depcr = depcr + c;
                    }
                }

                DataTable dtdeplst = new DataTable();
                dtdeplst = (DataTable)select("Select Balance from AccountBalance_InStatment where AccountMasterId=(select Max(Id) as Id from AccountMaster where AccountId='" + dtr["AccountId"].ToString() + "' and Whid='" + ddwarehouse.SelectedValue + "' ) and Report_Period_Id='" + ViewState["period"] + "'");
                if (dtdeplst.Rows.Count > 0)
                {
                    if (dtdeplst.Rows[0]["Balance"].ToString() != "")
                    {
                        Decimal bal = 0;
                        bal = Convert.ToDecimal(dtdeplst.Rows[0]["Balance"].ToString());
                        depballst = depballst + bal;
                    }
                }

            }
            depdrcr = depdr - depcr;
            lbldepamt.Text = Convert.ToString(depdrcr);
            lbldeplst.Text = Convert.ToString(depballst);
            tw(lbldepamt);
            tw(lbldeplst);
            pnldisplaycost(lbldepamt, lbldeplst, pnldepr);

            filldatagroup(Convert.ToString(dtexp.Rows[0]["GroupId"]), lblotheropratingexandlossamt, lblotheropratingexandlosslst);
            tw(lblotheropratingexandlossamt);
            tw(lblotheropratingexandlosslst);
            pnldisplaycost(lblotheropratingexandlossamt, lblotheropratingexandlosslst, pnlotherexp);

            if (Convert.ToDecimal(lblotheropratingexandlossamt.Text) < 0)
            {
                lblotheropratingexandlossamt.Text = Convert.ToString(Convert.ToDecimal(lblotheropratingexandlossamt.Text) * -1);
            }
            if (Convert.ToDecimal(lblotheropratingexandlosslst.Text) < 0)
            {
                lblotheropratingexandlosslst.Text = Convert.ToString(Convert.ToDecimal(lblotheropratingexandlosslst.Text) * -1);
            }

            Decimal totalexpcurr = 0;
            Decimal totalexplst = 0;
            totalexpcurr = sedrcr + admdrcr + depdrcr + Convert.ToDecimal(lblotheropratingexandlossamt.Text);
            totalexplst = seballst + admballst + depballst + Convert.ToDecimal(lblotheropratingexandlosslst.Text); ;

            lbltotexpamt.Text = Convert.ToString(totalexpcurr);
            lbltotexplst.Text = Convert.ToString(totalexplst);
            tw(lbltotexpamt);
            tw(lbltotexplst);
            pnldisplaycost(lbltotexpamt, lbltotexplst, pnlgrossline);

            Decimal opincomeamt = 0;
            opincomeamt = Convert.ToDecimal(lblgrossamt.Text) + Convert.ToDecimal(lblproductionexpensesamt.Text) - Convert.ToDecimal(lbltotexpamt.Text);
            lblopeamt.Text = Convert.ToString(opincomeamt);
            lblopeincome.Text = "Operating Income";

            Decimal opincomelst = 0;
            opincomelst = Convert.ToDecimal(lblgrossamtlst.Text) + Convert.ToDecimal(lblproductionexplst.Text) - Convert.ToDecimal(lbltotexplst.Text);
            lblopelst.Text = Convert.ToString(opincomelst);
            tw(lblopeamt);
            tw(lblopelst);

            //////code for Salexp,gen& admexp,Depriciation currentyear amt and lastyear amt calculation ended /////////////
            //start with line no 294  and ended at 468 //////


        }

        ////// Code for otherincome&revenue and otherexpenses&losses currentyear amt and lastyear amt calculation /////////////
        //start with line no 478 and ended at 669 //////
        DataTable dtotrev = new DataTable();
        dtotrev = (DataTable)select("SELECT     GroupId, groupdisplayname " +
        " FROM         GroupCompanyMaster where GroupId in ('37','44','45') and Whid='" + ddwarehouse.SelectedValue + "' order by GroupId");
        if (dtotrev.Rows.Count > 0)
        {
            lblintrestexp.Text = dtotrev.Rows[0]["groupdisplayname"].ToString();
            lblotherrevn.Text = dtotrev.Rows[1]["groupdisplayname"].ToString();
            lblothexpen.Text = dtotrev.Rows[2]["groupdisplayname"].ToString();
            filldatagroup(Convert.ToString(dtotrev.Rows[0]["GroupId"]), lblintrestexpamt, lblintrestexplst);
            //if (Convert.ToDecimal(lblintrestexpamt.Text) < 0)
            //{
              //  lblintrestexpamt.Text = Convert.ToString(Convert.ToDecimal(lblintrestexpamt.Text) * -1);
           // }
            //if (Convert.ToDecimal(lblintrestexplst.Text) < 0)
            //{
              //  lblintrestexplst.Text = Convert.ToString(Convert.ToDecimal(lblintrestexplst.Text) * -1);
           // }
            pnldisplaycost(lblintrestexpamt, lblintrestexplst, pnlintexp);

            DataTable dtothdr = new DataTable();
            dtothdr = (DataTable)select("Select Distinct AccountId from AccountMaster where GroupId = '" + dtotrev.Rows[1]["GroupId"].ToString() + "' " + acid.ToString() + "");
            if (dtothdr.Rows.Count > 0)
            {
                Decimal othdeb = 0;
                Decimal othcrd = 0;
                Decimal otdebcreamt = 0;
                Decimal othballst = 0;

                foreach (DataRow dtr in dtothdr.Rows)
                {
                    DataTable dtamtd = new DataTable();
                    dtamtd = (DataTable)select("Select sum(AmountDebit) as amtb from Tranction_Details inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=Tranction_Details.Tranction_Master_Id where TranctionMaster.Whid='" + ddwarehouse.SelectedValue + "' and  Tranction_Details.whid='" + ddwarehouse.SelectedValue + "'  and AccountDebit='" + dtr["AccountId"].ToString() + "'" + str);
                    if (dtamtd.Rows.Count > 0)
                    {
                        if (dtamtd.Rows[0]["amtb"].ToString() != "")
                        {
                            Decimal d = 0;
                            d = Convert.ToDecimal(dtamtd.Rows[0]["amtb"].ToString());
                            othdeb = othdeb + d;
                        }
                    }
                    //Decimal deb = 0;
                    DataTable dtamtc = new DataTable();
                    dtamtc = (DataTable)select("Select sum(AmountCredit) as amtc from Tranction_Details inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=Tranction_Details.Tranction_Master_Id where TranctionMaster.Whid='" + ddwarehouse.SelectedValue + "' and  Tranction_Details.whid='" + ddwarehouse.SelectedValue + "'  and AccountCredit='" + dtr["AccountId"].ToString() + "'" + str);
                    if (dtamtc.Rows.Count > 0)
                    {
                        if (dtamtc.Rows[0]["amtc"].ToString() != "")
                        {
                            Decimal c = 0;
                            c = Convert.ToDecimal(dtamtc.Rows[0]["amtc"].ToString());
                            othcrd = othcrd + c;
                        }
                    }

                    DataTable dtothbal = new DataTable();
                    dtothbal = (DataTable)select("Select Balance from AccountBalance_InStatment where AccountMasterId=(select Max(Id) as Id from AccountMaster where AccountId='" + dtr["AccountId"].ToString() + "' and Whid='" + ddwarehouse.SelectedValue + "' ) and Report_Period_Id='" + ViewState["period"] + "'");
                    if (dtothbal.Rows.Count > 0)
                    {
                        if (dtothbal.Rows[0]["Balance"].ToString() != "")
                        {
                            Decimal bal = 0;
                            bal = Convert.ToDecimal(dtothbal.Rows[0]["Balance"].ToString());
                            othballst = othballst + bal;
                        }
                    }
                }

                otdebcreamt = othdeb - othcrd;
                if (otdebcreamt >= 0)
                {
                    otdebcreamt = otdebcreamt * -1;
                    lblothrevamt.Text = Convert.ToString(otdebcreamt);
                }
                else
                {
                    otdebcreamt = otdebcreamt - (2 * otdebcreamt);
                    lblothrevamt.Text = Convert.ToString(otdebcreamt);
                }

                if (othballst >= 0)
                {
                    othballst = othballst * -1;
                    lblothrevlst.Text = Convert.ToString(othballst);
                }
                else
                {
                    othballst = othballst - (2 * othballst);
                    lblothrevlst.Text = Convert.ToString(othballst);
                }

            }

            tw(lblothrevamt);
            tw(lblothrevlst);
            pnldisplaycost(lblothrevamt, lblothrevlst, pnlotherrevandgain);


            DataTable dtothexp = new DataTable();
            dtothexp = (DataTable)select("Select Distinct AccountId from AccountMaster where GroupId = '" + dtotrev.Rows[2]["GroupId"].ToString() + "' " + acid.ToString() + "");
            if (dtothexp.Rows.Count > 0)
            {
                Decimal othexdeb = 0;
                Decimal othexcrd = 0;
                Decimal othexdebcreamt = 0;
                Decimal othexballst = 0;
                foreach (DataRow othdtr in dtothexp.Rows)
                {
                    DataTable dtothexpd = new DataTable();
                    dtothexpd = (DataTable)select("Select sum(AmountDebit) as amtb from Tranction_Details inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=Tranction_Details.Tranction_Master_Id where TranctionMaster.Whid='" + ddwarehouse.SelectedValue + "' and  Tranction_Details.whid='" + ddwarehouse.SelectedValue + "'  and AccountDebit='" + othdtr["AccountId"].ToString() + "'" + str);
                    if (dtothexpd.Rows.Count > 0)
                    {
                        if (dtothexpd.Rows[0]["amtb"].ToString() != "")
                        {
                            Decimal d = 0;
                            d = Convert.ToDecimal(dtothexpd.Rows[0]["amtb"].ToString());
                            othexdeb = othexdeb + d;
                        }
                    }
                    //Decimal deb = 0;
                    DataTable dtothexpc = new DataTable();
                    dtothexpc = (DataTable)select("Select sum(AmountCredit) as amtc from Tranction_Details inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=Tranction_Details.Tranction_Master_Id where TranctionMaster.Whid='" + ddwarehouse.SelectedValue + "' and  Tranction_Details.whid='" + ddwarehouse.SelectedValue + "'  and AccountCredit='" + othdtr["AccountId"].ToString() + "'" + str);
                    if (dtothexpc.Rows.Count > 0)
                    {
                        if (dtothexpc.Rows[0]["amtc"].ToString() != "")
                        {
                            Decimal c = 0;
                            c = Convert.ToDecimal(dtothexpc.Rows[0]["amtc"].ToString());
                            othexcrd = othexcrd + c;
                        }
                    }

                    DataTable dtbalothexp = new DataTable();
                    dtbalothexp = (DataTable)select("Select Balance from AccountBalance_InStatment where AccountMasterId=(select Max(Id) as Id from AccountMaster where AccountId='" + othdtr["AccountId"].ToString() + "' and Whid='" + ddwarehouse.SelectedValue + "' ) and Report_Period_Id='" + ViewState["period"] + "'");
                    if (dtbalothexp.Rows.Count > 0)
                    {
                        if (dtbalothexp.Rows[0]["Balance"].ToString() != "")
                        {
                            Decimal bal = 0;
                            bal = Convert.ToDecimal(dtbalothexp.Rows[0]["Balance"].ToString());
                            othexballst = othexballst + bal;
                        }
                    }

                }

                othexdebcreamt = othexdeb - othexcrd;
                othexdebcreamt = othexdebcreamt * (-1);
                if (othexdebcreamt >= 0)
                {
                    //cstdebcreamt = cstdebcreamt * -1;
                    lblothexpamt.Text = Convert.ToString(othexdebcreamt);
                }
                else
                {
                    //cstdebcreamt = cstdebcreamt - (2 * cstdebcreamt);
                    lblothexpamt.Text = Convert.ToString(othexdebcreamt);
                }
                othexballst = othexballst * (-1);
                if (othexballst >= 0)
                {
                    //salballst = salballst * -1;
                    lblothexplst.Text = Convert.ToString(othexballst);
                }
                else
                {
                    // salballst = salballst - (2 * salballst);
                    lblothexplst.Text = Convert.ToString(othexballst);
                }

            }

            tw(lblothexpamt);
            tw(lblothexplst);
            pnldisplaycost(lblothexpamt, lblothexplst, pnlotheexpandloses);


            Decimal gh = 0;
            if (lblopeamt.Text != "" && lblothrevamt.Text != "")
            {
                gh = Convert.ToDecimal(lblopeamt.Text) + Convert.ToDecimal(lblothrevamt.Text);
            }
            Decimal ghl = 0;
            if (lblopelst.Text != "" && lblothrevlst.Text != "")
            {
                ghl = Convert.ToDecimal(lblopelst.Text) + Convert.ToDecimal(lblothrevlst.Text);
            }

            Decimal j = 0;
            if (lblothrevamt.Text != "")
            {
                j = gh + Convert.ToDecimal(lblothexpamt.Text) + Convert.ToDecimal(lblintrestexpamt.Text);
            }

            Decimal j1 = 0;
            lblincbefamt.Text = Convert.ToString(j);
            lblincomebefore.Text = "Income Before Unusual Items and Income Tax";
            if (lblothrevlst.Text != "")
            {
                j1 = ghl + Convert.ToDecimal(lblothexplst.Text) + Convert.ToDecimal(lblintrestexplst.Text);
            }
            lblincberlst.Text = Convert.ToString(j1);
            tw(lblincbefamt);
            tw(lblincberlst);


        }

        ////// Code for otherincome&revenue and otherexpenses&losses currentyear amt and lastyear amt calculation ended  /////////////
        //start with line no 478  and ended at 669 //////


        ////// Code started for unusual or infrequent item  currentyear amt and lastyear amt calculation /////////////
        //start with line no 679 and ended at 777 //////

        DataTable dtincitem = new DataTable();
        dtincitem = (DataTable)select("SELECT     GroupId, groupdisplayname " +
        " FROM         GroupCompanyMaster where GroupId = 46 and Whid='" + ddwarehouse.SelectedValue + "'");

        if (dtincitem.Rows.Count > 0)
        {
            lblusualinfrequent.Text = dtincitem.Rows[0]["groupdisplayname"].ToString();

            DataTable dtinfredr = new DataTable();
            dtinfredr = (DataTable)select("Select Distinct AccountId from AccountMaster where GroupId = '" + dtincitem.Rows[0]["GroupId"].ToString() + "' " + acid.ToString() + "");
            if (dtinfredr.Rows.Count > 0)
            {
                Decimal infredeb = 0;
                Decimal infrecrd = 0;
                Decimal infredebcreamt = 0;
                Decimal infreballst = 0;

                foreach (DataRow dtr in dtinfredr.Rows)
                {
                    DataTable dtamtinfred = new DataTable();
                    dtamtinfred = (DataTable)select("Select sum(AmountDebit) as amtb from Tranction_Details inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=Tranction_Details.Tranction_Master_Id where TranctionMaster.Whid='" + ddwarehouse.SelectedValue + "' and  Tranction_Details.whid='" + ddwarehouse.SelectedValue + "'  and AccountDebit='" + dtr["AccountId"].ToString() + "'" + str);
                    if (dtamtinfred.Rows.Count > 0)
                    {
                        if (dtamtinfred.Rows[0]["amtb"].ToString() != "")
                        {
                            Decimal d = 0;
                            d = Convert.ToDecimal(dtamtinfred.Rows[0]["amtb"].ToString());
                            infredeb = infredeb + d;
                        }
                    }
                    //Decimal deb = 0;
                    DataTable dtamtinfrec = new DataTable();
                    dtamtinfrec = (DataTable)select("Select sum(AmountCredit) as amtc from Tranction_Details inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=Tranction_Details.Tranction_Master_Id where TranctionMaster.Whid='" + ddwarehouse.SelectedValue + "' and  Tranction_Details.whid='" + ddwarehouse.SelectedValue + "'  and AccountCredit='" + dtr["AccountId"].ToString() + "'" + str);
                    if (dtamtinfrec.Rows.Count > 0)
                    {
                        if (dtamtinfrec.Rows[0]["amtc"].ToString() != "")
                        {
                            Decimal c = 0;
                            c = Convert.ToDecimal(dtamtinfrec.Rows[0]["amtc"].ToString());
                            infrecrd = infrecrd + c;
                        }
                    }

                    DataTable dtinfrebal = new DataTable();
                    dtinfrebal = (DataTable)select("Select Balance from AccountBalance_InStatment where AccountMasterId=(select Max(Id) as Id from AccountMaster where AccountId='" + dtr["AccountId"].ToString() + "' and Whid='" + ddwarehouse.SelectedValue + "' ) and Report_Period_Id='" + ViewState["period"] + "'");
                    if (dtinfrebal.Rows.Count > 0)
                    {
                        if (dtinfrebal.Rows[0]["Balance"].ToString() != "")
                        {
                            Decimal bal = 0;
                            bal = Convert.ToDecimal(dtinfrebal.Rows[0]["Balance"].ToString());
                            infreballst = infreballst + bal;
                        }
                    }
                }

                infredebcreamt = infredeb - infrecrd;
                if (infredebcreamt >= 0)
                {
                    infredebcreamt = infredebcreamt * -1;
                    lblusualinfrqamt.Text = Convert.ToString(infredebcreamt);
                }
                else
                {
                    infredebcreamt = infredebcreamt - (2 * infredebcreamt);
                    lblusualinfrqamt.Text = Convert.ToString(infredebcreamt);
                }

                if (infreballst >= 0)
                {
                    infreballst = infreballst * -1;
                    lblusfrelst.Text = Convert.ToString(infreballst);
                }
                else
                {
                    infreballst = infreballst - (2 * infreballst);
                    lblusfrelst.Text = Convert.ToString(infreballst);
                }
            }

            tw(lblusualinfrqamt);
            tw(lblusfrelst);
            pnldisplaycost(lblusualinfrqamt, lblusfrelst, pnlunusealor);

            lblincomebeforeit.Text = "Income Before Income Tax";
            Decimal i = 0;
            if (lblincbefamt.Text != "" && lblusualinfrqamt.Text != "")
            {
                i = Convert.ToDecimal(lblincbefamt.Text) + Convert.ToDecimal(lblusualinfrqamt.Text);
            }
            Decimal i1 = 0;
            if (lblincberlst.Text != "" && lblusfrelst.Text != "")
            {
                i1 = Convert.ToDecimal(lblincberlst.Text) + Convert.ToDecimal(lblusfrelst.Text);
            }

            lblincbeforitamt.Text = Convert.ToString(i);
            lblincbeforitlst.Text = Convert.ToString(i1);
            tw(lblincbeforitamt);
            tw(lblincbeforitlst);
        }


        ////// Code ended for Income from Discontinued Operations(LessTax) currentyear amt and lastyear amt calculation /////////////
        //start with line no 929 and ended at 1135 //////
    }

    protected void pnldisplay(Label lblamtdata, Panel pnl)
    {
        if (lblamtdata.Text != "0.00" && lblamtdata.Text != "0")
        {
            pnl.Visible = true;
        }
        else
        {
            pnl.Visible = false;
        }

    }
    protected void pnldisplaycost(Label lblamtdata, Label lblamtlst, Panel pnl)
    {
        if (lblamtdata.Text != "0.00" || lblamtlst.Text != "0.00")
        {
            pnl.Visible = true;
        }
        else
        {
            pnl.Visible = false;
        }

    }
    protected void tw(Label lblamtdata)
    {
        if (lblamtdata.Text != "")
        {
            lblamtdata.Text = Math.Round(Convert.ToDecimal(lblamtdata.Text), 2).ToString("###,###.##");
        }
        if (lblamtdata.Text != "")
        {
            lblamtdata.Text = String.Format("{0:n}", Convert.ToDecimal(lblamtdata.Text));
        }
        else
        {
            lblamtdata.Text = "0.00";
        }
        //  lblamtdata.ForeColor = System.Drawing.Color.Black;
    }

    protected DataTable select(string str)
    {
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);

        return dt;

    }

    public void createPDFDoc(String strhtml)
    {
        strhtml = strhtml.Replace("font-size:Medium;", "font-size:12pt;");
        string strfilename = HttpContext.Current.Server.MapPath("TempDoc/GridViewExport.pdf");

        Document doc = new Document(PageSize.A2, 30f, 30f, 30f, 30f);
        PdfWriter.GetInstance(doc, new FileStream(strfilename, FileMode.Create));
        System.IO.StringReader se = new StringReader(strhtml.ToString());
        HTMLWorker obj = new HTMLWorker(doc);

        doc.Open();
        obj.Parse(se);
        doc.Close();
        Showpdf(strfilename);
    }
    public void Showpdf(string strFileName)
    {
        Response.ClearContent();
        Response.ClearHeaders();
        //Response.AddHeader("Content-Disposition", "attachment;filename=" + strFileName);
        //Response.ContentType = "application/pdf";
        Response.WriteFile(strFileName);

        Response.Flush();
        Response.Clear();
    }
    protected void ddlExport_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (pnlreport.Visible == true)
        {

            this.EnableViewState = false;
            Response.Charset = string.Empty;
            if (ddlExport.SelectedValue == "1")
            {


            }
            else if (ddlExport.SelectedValue == "2")
            {
                Response.Clear();

                Response.Buffer = true;

                Response.AddHeader("content-disposition",

                "attachment;filename=GridViewExport.xls");

                Response.Charset = "";

                Response.ContentType = "application/vnd.ms-excel";

                StringWriter sw = new StringWriter();

                HtmlTextWriter hw = new HtmlTextWriter(sw);


                pnlreport.RenderControl(hw);

                //style to format numbers to string

                string style = @"<style> .textmode { mso-number-format:\@; } </style>";

                Response.Write(style);

                Response.Output.Write(sw.ToString());

                Response.Flush();

                Response.End();
            }
            else if (ddlExport.SelectedValue == "3")
            {
                Response.Clear();

                Response.Buffer = true;

                Response.AddHeader("content-disposition",

                "attachment;filename=GridViewExport.doc");

                Response.Charset = "";

                Response.ContentType = "application/vnd.ms-word ";

                StringWriter sw = new StringWriter();

                HtmlTextWriter hw = new HtmlTextWriter(sw);


                pnlreport.RenderControl(hw);
                string style = @"<style> .textmode {mso-number-format:\@; } </style>";

                Response.Write(style);
                Response.Output.Write(sw.ToString());

                Response.Flush();

                Response.End();

            }
            else if (ddlExport.SelectedValue == "4")
            {

                Response.Cache.SetCacheability(HttpCacheability.NoCache);

                ////////

                MemoryStream mem = new MemoryStream();
                StreamWriter twr = new StreamWriter(mem);
                HtmlTextWriter myWriter = new HtmlTextWriter(twr);
                pnlreport.RenderControl(myWriter);
                //base.Render(myWriter);
                myWriter.Flush();
                myWriter.Dispose();
                StreamReader strread = new StreamReader(mem);
                strread.BaseStream.Position = 0;
                string pagecontent = strread.ReadToEnd();
                strread.Dispose();
                mem.Dispose();
                //myWriter.Write(pagecontent);
                createPDFDoc(pagecontent);
                Response.Clear();

                Response.Buffer = true;



                Response.Charset = "";

                Response.ContentType = "application/vnd.ms-word ";

                StringWriter sw = new StringWriter();

                HtmlTextWriter hw = new HtmlTextWriter(sw);

                string filename = "GrdM_" + System.DateTime.Today.Day + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second;
                Session["Emfile"] = filename + ".Doc";
                Session["GrdmailA"] = null;


                pnlreport.RenderControl(hw);
                string style = @"<style> .textmode { mso-number-format:\@; } </style>";

                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.WriteFile(HttpContext.Current.Server.MapPath("TempDoc/" + filename + ".Doc"));
                string path = HttpContext.Current.Server.MapPath("TempDoc/" + filename + ".Doc");
                System.IO.File.WriteAllText(path, style + sw.ToString());

                Response.Flush();

                Response.End();
                string te = "MessageComposeExt.aspx?ema=Azxcvyute";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);


            }

        }

    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void imgbtngo_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = (DataTable)select("select Convert(nvarchar,StartDate,101) as StartDate, Convert(nvarchar,EndDate,101) as EndDate from [ReportPeriod] where Compid = '" + compid + "' and Active='1' and Whid='" + ddwarehouse.SelectedValue + "'");

        if (dt.Rows.Count > 0)
        {
            if (Convert.ToDateTime(txtdateto.Text) < Convert.ToDateTime(dt.Rows[0][0].ToString()) || Convert.ToDateTime(txtdateto.Text) > Convert.ToDateTime(dt.Rows[0][1].ToString()))
            {
                lblstartdate0.Text = dt.Rows[0][0].ToString();
                ModalPopupExtender1.Show();

            }
            else
            {
                str = " and TranctionMaster.Date Between '" + Convert.ToDateTime(dt.Rows[0]["StartDate"]) + "' and '" + Convert.ToDateTime(txtdateto.Text) + "'";
                acid = " and AccountMaster.Whid = '" + ddwarehouse.SelectedValue + "'";
                incomestatement();

                fillcashflow();
                pnlreport.Visible = true;
                lblstore.Text = ddwarehouse.SelectedItem.Text;
                lblcasondate.Text = txtdateto.Text;
                DataTable dtw = select("Select CurrencyName from [CurrencyMaster] inner join WareHouseMaster on [CurrencyMaster].CurrencyId=WareHouseMaster.CurrencyId where WareHouseMaster.WareHouseId='" + ddwarehouse.SelectedValue + "'");
                if (dtw.Rows.Count > 0)
                {
                    lblcurr.Text = dtw.Rows[0][0].ToString();
                }
            }
        }

    }

    protected void balancsheetass(int i, LinkButton lblhead, Label lblamt, Label lbllst)
    {
        DataTable dt = new DataTable();
        dt = (DataTable)select("SELECT    id, GroupId,groupdisplayname" +
        " FROM         GroupCompanyMaster where GroupId = '" + i + "' and Whid='" + ddwarehouse.SelectedValue + "'");
        if (dt.Rows.Count > 0)
        {
            lblhead.Text = dt.Rows[0]["groupdisplayname"].ToString();
            DataTable dt1 = new DataTable();
            //dt1 = (DataTable)select("Select Distinct AccountId from AccountMaster where Whid='"+ddwarehouse.SelectedValue+"' and GroupId= '" + dt.Rows[0]["GroupId"].ToString() + "' and AccountMaster.compid = '" + compid + "'");
            //dt1 = (DataTable)select("Select Distinct AccountId from AccountMaster where Whid='"+ddwarehouse.SelectedValue+"' and GroupId= '" + dt.Rows[0]["GroupId"].ToString() + "' and AccountMaster.Whid = '" + ddwarehouse.SelectedValue + "'");
            dt1 = (DataTable)select("Select Distinct AccountId from AccountMaster where  Whid='" + ddwarehouse.SelectedValue + "' and GroupId = '" + dt.Rows[0]["GroupId"].ToString() + "' " + acid.ToString() + "");
            if (dt1.Rows.Count > 0)
            {
                Decimal deb = 0;
                Decimal crd = 0;
                Decimal debcreamt = 0;
                Decimal ballst = 0;

                foreach (DataRow dtr in dt1.Rows)
                {
                    string accountid1 = dtr["AccountId"].ToString();
                    DataTable dtdeamt = new DataTable();
                    dtdeamt = (DataTable)select("Select sum(AmountDebit) as amtb from Tranction_Details inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=Tranction_Details.Tranction_Master_Id where Tranction_Details.Whid='" + ddwarehouse.SelectedValue + "'  and TranctionMaster.Whid='" + ddwarehouse.SelectedValue + "' and Tranction_Details.AccountDebit='" + dtr["AccountId"].ToString() + "'" + str);
                    if (dtdeamt.Rows.Count > 0)
                    {
                        if (dtdeamt.Rows[0]["amtb"].ToString() != "")
                        {
                            Decimal d = 0;
                            d = Convert.ToDecimal(dtdeamt.Rows[0]["amtb"].ToString());
                            deb = deb + d;
                        }
                    }
                    //Decimal deb = 0;
                    DataTable dtcreamt = new DataTable();
                    dtcreamt = (DataTable)select("Select sum(AmountCredit) as amtc from Tranction_Details inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=Tranction_Details.Tranction_Master_Id where Tranction_Details.Whid='" + ddwarehouse.SelectedValue + "'  and TranctionMaster.Whid='" + ddwarehouse.SelectedValue + "' and AccountCredit='" + dtr["AccountId"].ToString() + "'" + str);
                    if (dtcreamt.Rows.Count > 0)
                    {
                        if (dtcreamt.Rows[0]["amtc"].ToString() != "")
                        {
                            Decimal c = 0;
                            c = Convert.ToDecimal(dtcreamt.Rows[0]["amtc"].ToString());
                            crd = crd + c;
                        }
                    }

                    DataTable dtbal = new DataTable();
                    //dtbal = (DataTable)select("Select BalanceOfLastYear from AccountMaster where Whid='"+ddwarehouse.SelectedValue+"' and AccountId='" + dtr["AccountId"].ToString() + "' and AccountMaster.compid = '" + compid + "'");
                    //dtbal = (DataTable)select("Select BalanceOfLastYear from AccountMaster where Whid='"+ddwarehouse.SelectedValue+"' and AccountId='" + dtr["AccountId"].ToString() + "' and AccountMaster.Whid = '" + ddwarehouse.SelectedValue + "'");
                    dtbal = select("Select Balance from AccountBalance where AccountMasterId=(select Max(Id) as Id from AccountMaster where AccountId='" + dtr["AccountId"].ToString() + "' and Whid='" + ddwarehouse.SelectedValue + "' ) and Report_Period_Id='" + ViewState["period"] + "'");
                    if (dtbal.Rows.Count > 0)
                    {
                        if (dtbal.Rows[0]["Balance"].ToString() != "")
                        {
                            Decimal bal = 0;
                            bal = Convert.ToDecimal(dtbal.Rows[0]["Balance"].ToString());
                            ballst = ballst + bal;
                        }
                    }
                }

                debcreamt = (ballst + deb) - crd;
                //debcreamt = deb - crd;
                if (debcreamt >= 0)
                {
                    //cbankdebcreamt = cbankdebcreamt * -1;
                    lblamt.Text = Convert.ToString(debcreamt);
                }
                else
                {
                    //cbankdebcreamt = cbankdebcreamt - (2 * cbankdebcreamt);
                    lblamt.Text = Convert.ToString(debcreamt);
                }

                if (ballst >= 0)
                {
                    //cbankballst = cbankballst * -1;
                    lbllst.Text = Convert.ToString(ballst);
                }
                else
                {
                    //cbankballst = cbankballst - (2 * cbankballst);
                    lbllst.Text = Convert.ToString(ballst);
                }
            }
            else
            {
                lblamt.Text = Convert.ToString(0);
                lbllst.Text = Convert.ToString(0);
            }

            tw(lblamt);
            tw(lbllst);
        }
    }
    protected void balancesheetlia(int i, LinkButton lblhead, Label lblamt, Label lbllst)
    {
        DataTable dt = new DataTable();
        dt = (DataTable)select("SELECT     id,GroupId, groupdisplayname " +
        " FROM         GroupCompanyMaster where Whid='" + ddwarehouse.SelectedValue + "' and GroupId = '" + i + "' ");
        if (dt.Rows.Count > 0)
        {
            lblhead.Text = dt.Rows[0]["groupdisplayname"].ToString();
            DataTable dt1 = new DataTable();
            //            dt1 = (DataTable)select("Select Distinct AccountId from AccountMaster where Whid='"+ddwarehouse.SelectedValue+"' and GroupId= '" + dt.Rows[0]["GroupId"].ToString() + "' and AccountMaster.compid = '" + compid + "'");
            //dt1 = (DataTable)select("Select Distinct AccountId from AccountMaster where Whid='"+ddwarehouse.SelectedValue+"' and GroupId= '" + dt.Rows[0]["GroupId"].ToString() + "' and AccountMaster.Whid = '" + ddwarehouse.SelectedValue + "'");
            dt1 = (DataTable)select("Select Distinct AccountId from AccountMaster where  Whid='" + ddwarehouse.SelectedValue + "' and GroupId = '" + dt.Rows[0]["GroupId"].ToString() + "' " + acid.ToString() + "");
            if (dt1.Rows.Count > 0)
            {
                Decimal deb = 0;
                Decimal crd = 0;
                Decimal debcreamt = 0;
                Decimal ballst = 0;

                foreach (DataRow dtr in dt1.Rows)
                {
                    DataTable dtdeamt = new DataTable();
                    dtdeamt = (DataTable)select("Select sum(AmountDebit) as amtb from Tranction_Details inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=Tranction_Details.Tranction_Master_Id where Tranction_Details.Whid='" + ddwarehouse.SelectedValue + "'  and TranctionMaster.Whid='" + ddwarehouse.SelectedValue + "' and AccountDebit='" + dtr["AccountId"].ToString() + "'" + str);
                    if (dtdeamt.Rows.Count > 0)
                    {
                        if (dtdeamt.Rows[0]["amtb"].ToString() != "")
                        {
                            Decimal d = 0;
                            d = Convert.ToDecimal(dtdeamt.Rows[0]["amtb"].ToString());
                            deb = deb + d;
                        }
                    }
                    //Decimal deb = 0;




                    DataTable dtcreamt = new DataTable();
                    dtcreamt = (DataTable)select("Select sum(AmountCredit) as amtc from Tranction_Details inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=Tranction_Details.Tranction_Master_Id where Tranction_Details.Whid='" + ddwarehouse.SelectedValue + "'  and TranctionMaster.Whid='" + ddwarehouse.SelectedValue + "' and AccountCredit='" + dtr["AccountId"].ToString() + "'" + str);
                    if (dtcreamt.Rows.Count > 0)
                    {
                        if (dtcreamt.Rows[0]["amtc"].ToString() != "")
                        {
                            Decimal c = 0;
                            c = Convert.ToDecimal(dtcreamt.Rows[0]["amtc"].ToString());
                            crd = crd + c;
                        }
                    }

                    DataTable dtbal = new DataTable();
                    //  dtbal = (DataTable)select("Select BalanceOfLastYear from AccountMaster where Whid='"+ddwarehouse.SelectedValue+"' and AccountId='" + dtr["AccountId"].ToString() + "' and AccountMaster.compid = '" + compid + "'");
                    //dtbal = (DataTable)select("Select BalanceOfLastYear from AccountMaster where Whid='"+ddwarehouse.SelectedValue+"' and AccountId='" + dtr["AccountId"].ToString() + "' and AccountMaster.Whid = '" + ddwarehouse.SelectedValue + "'");
                    dtbal = select("Select Balance from AccountBalance where AccountMasterId=(select Max(Id) as Id from AccountMaster where AccountId='" + dtr["AccountId"].ToString() + "' and Whid='" + ddwarehouse.SelectedValue + "' ) and Report_Period_Id='" + ViewState["period"] + "'");
                    if (dtbal.Rows.Count > 0)
                    {
                        if (dtbal.Rows[0]["Balance"].ToString() != "")
                        {
                            Decimal bal = 0;
                            bal = Convert.ToDecimal(dtbal.Rows[0]["Balance"].ToString());
                            ballst = ballst + bal;
                        }
                    }
                }
                debcreamt = deb - crd;

                if (ballst >= 0)
                {
                    ballst = ballst * -1;
                    lbllst.Text = Convert.ToString(ballst);
                }
                else
                {
                    ballst = ballst - (2 * ballst);
                    lbllst.Text = Convert.ToString(ballst);
                }

                if (debcreamt >= 0)
                {
                    debcreamt = debcreamt * -1;
                    lblamt.Text = Convert.ToString(debcreamt + ballst);
                    // lblamt.Text = Convert.ToString(debcreamt);
                }
                else
                {
                    debcreamt = debcreamt - (2 * debcreamt);
                    lblamt.Text = Convert.ToString(debcreamt + ballst);
                    // lblamt.Text = Convert.ToString(debcreamt);
                }






            }
            else
            {
                lblamt.Text = Convert.ToString(0);
                lbllst.Text = Convert.ToString(0);
            }

            tw(lblamt);
            tw(lbllst);
        }
    }
    protected Decimal revandgain(string groupid, DataList lblb, Panel pnlavv)
    {
        Decimal totsal = 0;
        DataTable dt = new DataTable();
        DataColumn Dcom = new DataColumn();
        Dcom.DataType = System.Type.GetType("System.String");
        Dcom.ColumnName = "AccountName";
        Dcom.AllowDBNull = true;
        Dcom.Unique = false;
        Dcom.ReadOnly = false;

        DataColumn Dcom1 = new DataColumn();
        Dcom1.DataType = System.Type.GetType("System.Decimal");
        Dcom1.ColumnName = "Amount";
        Dcom1.AllowDBNull = true;
        Dcom1.Unique = false;
        Dcom1.ReadOnly = false;

        DataColumn Dcom2 = new DataColumn();
        Dcom2.DataType = System.Type.GetType("System.String");
        Dcom2.ColumnName = "AccountId";
        Dcom2.AllowDBNull = true;
        Dcom2.Unique = false;
        Dcom2.ReadOnly = false;
        dt.Columns.Add(Dcom);
        dt.Columns.Add(Dcom1);
        dt.Columns.Add(Dcom2);
        DataTable datata = select("Select Distinct AccountId,AccountName from AccountMaster where GroupId = '" + groupid + "' " + acid.ToString() + "");
        foreach (DataRow item in datata.Rows)
        {
            decimal cra = 0;
            decimal dra = 0;
            
            DataTable dtamtd = (DataTable)select("Select sum(Tranction_Details.Amountcredit) as Amount from AccountMaster inner join Tranction_Details on Tranction_Details.AccountCredit=AccountMaster.AccountId   inner join " +
        " TranctionMaster on TranctionMaster.Tranction_Master_Id=Tranction_Details.Tranction_Master_Id  where  AccountMaster.GroupId = '1'  " +
         " and AccountMaster.Whid='" + ddwarehouse.SelectedValue + "' and Tranction_Details.Whid='" + ddwarehouse.SelectedValue + "'  and Tranction_Details.Tranction_Master_Id in(Select Distinct Tranction_Details.Tranction_Master_Id  "+
        " from AccountMaster inner join Tranction_Details on Tranction_Details.AccountDebit=AccountMaster.AccountId  inner join TranctionMaster on TranctionMaster.Tranction_Master_Id="+
         " Tranction_Details.Tranction_Master_Id  where   Tranction_Details.AccountDebit='" + item["AccountId"] + "'  and AccountMaster.Whid='" + ddwarehouse.SelectedValue + "' and TranctionMaster.Whid='" + ddwarehouse.SelectedValue + "' and  Tranction_Details.whid='" + ddwarehouse.SelectedValue + "'" + str + ")" + str);
            if (dtamtd.Rows.Count > 0)
            {
                if (Convert.ToString(dtamtd.Rows[0]["Amount"]) != "")
                {
                    cra = Convert.ToDecimal(dtamtd.Rows[0]["Amount"]);
                }
            }
            DataTable dtamn = (DataTable)select("Select sum(Tranction_Details.AmountDebit) as Amount from AccountMaster inner join Tranction_Details on Tranction_Details.AccountDebit=AccountMaster.AccountId   inner join " +
       " TranctionMaster on TranctionMaster.Tranction_Master_Id=Tranction_Details.Tranction_Master_Id  where  AccountMaster.GroupId = '1'  " +
        " and AccountMaster.Whid='" + ddwarehouse.SelectedValue + "' and Tranction_Details.Whid='" + ddwarehouse.SelectedValue + "'  and Tranction_Details.Tranction_Master_Id in(Select Distinct Tranction_Details.Tranction_Master_Id  " +
       " from AccountMaster inner join Tranction_Details on Tranction_Details.AccountCredit=AccountMaster.AccountId  inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=" +
        " Tranction_Details.Tranction_Master_Id  where   Tranction_Details.AccountCredit='" + item["AccountId"] + "'  and AccountMaster.Whid='" + ddwarehouse.SelectedValue + "' and TranctionMaster.Whid='" + ddwarehouse.SelectedValue + "' and  Tranction_Details.whid='" + ddwarehouse.SelectedValue + "'" + str + ")" + str);
            if (dtamn.Rows.Count > 0)
            {
                if (Convert.ToString(dtamn.Rows[0]["Amount"]) != "")
                {
                    dra = Convert.ToDecimal(dtamn.Rows[0]["Amount"]);
                }
            }
            decimal totam=dra-cra;
            if (totam != 0)
            {
                DataRow Drow = dt.NewRow();
                Drow["AccountName"] = item["AccountName"].ToString();
                Drow["Amount"] = totam;
                Drow["AccountId"] = item["AccountId"].ToString();
                dt.Rows.Add(Drow);
                totsal += totam;
            }

            
        }
        lblb.DataSource = dt;
        lblb.DataBind();
        if (totsal != 0)
        {
            pnlavv.Visible = true;
        }
        else
        {
            pnlavv.Visible = false;
        }
        return totsal;

    }
    //protected void revandgain(int k1, DataList DList, string gid)
    //{
    //    DataTable dtothdr = new DataTable();
    //    dtothdr = (DataTable)select("Select Distinct AccountId,AccountName from AccountMaster where GroupId = '" + gid + "' " + acid.ToString() + "");



    //    DataTable dt = new DataTable();
    //    DataColumn Dcom = new DataColumn();
    //    Dcom.DataType = System.Type.GetType("System.String");
    //    Dcom.ColumnName = "AccountName";
    //    Dcom.AllowDBNull = true;
    //    Dcom.Unique = false;
    //    Dcom.ReadOnly = false;

    //    DataColumn Dcom1 = new DataColumn();
    //    Dcom1.DataType = System.Type.GetType("System.Decimal");
    //    Dcom1.ColumnName = "Amount";
    //    Dcom1.AllowDBNull = true;
    //    Dcom1.Unique = false;
    //    Dcom1.ReadOnly = false;

    //    DataColumn Dcom2 = new DataColumn();
    //    Dcom2.DataType = System.Type.GetType("System.String");
    //    Dcom2.ColumnName = "AccountId";
    //    Dcom2.AllowDBNull = true;
    //    Dcom2.Unique = false;
    //    Dcom2.ReadOnly = false;
    //    dt.Columns.Add(Dcom);
    //    dt.Columns.Add(Dcom1);
    //    dt.Columns.Add(Dcom2);
    //    foreach (DataRow dtr in dtothdr.Rows)
    //    {
    //        Decimal othdeb = 0;
    //        Decimal othcrd = 0;
    //        Decimal Totam = 0;

    //        DataTable dtamtd = new DataTable();
    //        dtamtd = (DataTable)select("Select sum(AmountDebit) as amtb from Tranction_Details inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=Tranction_Details.Tranction_Master_Id where TranctionMaster.Whid='" + ddwarehouse.SelectedValue + "' and  Tranction_Details.whid='" + ddwarehouse.SelectedValue + "'  and AccountDebit='" + dtr["AccountId"].ToString() + "'" + str);
    //        if (dtamtd.Rows.Count > 0)
    //        {
    //            if (dtamtd.Rows[0]["amtb"].ToString() != "")
    //            {
    //                Decimal d = 0;
    //                d = Convert.ToDecimal(dtamtd.Rows[0]["amtb"].ToString());
    //                othdeb = othdeb + d;
    //            }
    //        }
    //        //Decimal deb = 0;
    //        DataTable dtamtc = new DataTable();
    //        dtamtc = (DataTable)select("Select sum(AmountCredit) as amtc from Tranction_Details inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=Tranction_Details.Tranction_Master_Id where TranctionMaster.Whid='" + ddwarehouse.SelectedValue + "' and  Tranction_Details.whid='" + ddwarehouse.SelectedValue + "'  and AccountCredit='" + dtr["AccountId"].ToString() + "'" + str);
    //        if (dtamtc.Rows.Count > 0)
    //        {
    //            if (dtamtc.Rows[0]["amtc"].ToString() != "")
    //            {
    //                Decimal c = 0;
    //                c = Convert.ToDecimal(dtamtc.Rows[0]["amtc"].ToString());
    //                othcrd = othcrd + c;
    //            }
    //        }
    //        Totam = othdeb - othcrd;
    //        if (Totam != 0)
    //        {
    //            Totam = Math.Round(Totam, 2) * (k1);

    //            DataRow Drow = dt.NewRow();
    //            Drow["AccountName"] = dtr["AccountName"].ToString();
    //            Drow["Amount"] = Totam;
    //            Drow["AccountId"] = dtr["AccountId"].ToString();
    //            dt.Rows.Add(Drow);
    //        }
    //    }
    //    DList.DataSource = dt;
    //    DList.DataBind();

    //}
    protected void currentacces()
    {
        balancsheetass(1, lblcash, lblcashamt, lblcashlast);
        balancsheetass(2, lblaccreceivable, lblaccrecamt, lblaccreclst);
        balancsheetass(53, lblnotrec, lblnotrecamt, lblnotreclst);
        balancsheetass(3, lblinv, lblinvamt, lblinvlst);
        balancsheetass(4, lblprepaidexp, lblprepaidexpamt, lblprepaidexplst);
        balancsheetass(5, lblorhecurrasset, lblorhecurrassetamt, lblorhecurrassetlst);



        if (Convert.ToDecimal(lblaccrecamt.Text) > 0)
        {
            lblaccreceivable.Text = "Increase in Receivables";
        }
        else
        {
            lblaccreceivable.Text = "Decrease in Receivables";
        }
        lblaccrecamt.Text = Convert.ToString((-1) * (Convert.ToDecimal(lblaccrecamt.Text) - Convert.ToDecimal(lblaccreclst.Text)));

        lblaccrecamt.Text = String.Format("{0:n}", Convert.ToDecimal(lblaccrecamt.Text));

        if (Convert.ToDecimal(lblnotrecamt.Text) > 0)
        {
            lblnotrec.Text = "Increase in Notes Receivable";
        }
        else
        {
            lblnotrec.Text = "Decrease in Notes Receivable";
        }
        lblnotrecamt.Text = Convert.ToString((-1) * (Convert.ToDecimal(lblnotrecamt.Text) - Convert.ToDecimal(lblnotreclst.Text)));

        lblnotrecamt.Text = String.Format("{0:n}", Convert.ToDecimal(lblnotrecamt.Text));


        if (Convert.ToDecimal(lblinvamt.Text) > 0)
        {
            lblinv.Text = "Increase in Inventory";
        }
        else
        {
            lblinv.Text = "Decrease in Inventory";
        }
        lblinvamt.Text = Convert.ToString((-1) * (Convert.ToDecimal(lblinvamt.Text) - Convert.ToDecimal(lblinvlst.Text)));

        lblinvamt.Text = String.Format("{0:n}", Convert.ToDecimal(lblinvamt.Text));


        if (Convert.ToDecimal(lblprepaidexpamt.Text) > 0)
        {
            lblprepaidexp.Text = "Increase in Prepaid expenses";
        }
        else
        {
            lblprepaidexp.Text = "Decrease in Prepaid expenses";
        }
        lblprepaidexpamt.Text = Convert.ToString((-1) * (Convert.ToDecimal(lblprepaidexpamt.Text) - Convert.ToDecimal(lblprepaidexplst.Text)));
        lblprepaidexpamt.Text = String.Format("{0:n}", Convert.ToDecimal(lblprepaidexpamt.Text));

        if (Convert.ToDecimal(lblorhecurrassetamt.Text) > 0)
        {
            lblorhecurrasset.Text = "Increase in Other current assets";
        }
        else
        {
            lblorhecurrasset.Text = "Decrease in Other current assets";
        }
        lblorhecurrassetamt.Text = Convert.ToString((-1) * (Convert.ToDecimal(lblorhecurrassetamt.Text) - Convert.ToDecimal(lblorhecurrassetlst.Text)));

        lblorhecurrassetamt.Text = String.Format("{0:n}", Convert.ToDecimal(lblorhecurrassetamt.Text));
        pnldisplay(lblinvamt, pnlinv);
        pnldisplay(lblaccrecamt, pnlaccrecivable);
        pnldisplay(lblnotrecamt, pnlnotesreceived);
        pnldisplay(lblprepaidexpamt, pnlprepaidexe);
        pnldisplay(lblorhecurrassetamt, pnlothercurrasset);

    }
    protected void currentliab()
    {
        balancesheetlia(15, lblaccpay, lblaccpayamt, lblaccpaylst);
        balancesheetlia(54, lblnotepay, lblnotepayamt, lblnotepaylst);
        balancesheetlia(16, lblintpay, lblintpayamt, lblintpaylst);
        balancesheetlia(17, lbltaxpay, lbltaxpayamt, lbltxtpaylst);
        balancesheetlia(20, lblothcurr, lblothcurramt, lblothcurrlst);
        balancesheetlia(18, lnkspart12mtext, lblspart12mamt, lblspart12mlist);
        balancesheetlia(19, lnkcpartlongtext, lblcpartlongamt, lblcpartlonglist);



        lblaccpayamt.Text = Convert.ToString((1) * (Convert.ToDecimal(lblaccpayamt.Text) - Convert.ToDecimal(lblaccpaylst.Text)));
        if (Convert.ToDecimal(lblaccpayamt.Text) > 0)
        {
            lblaccpay.Text = "Increase in Accounts payable";
        }
        else
        {
            lblaccpay.Text = "Decrease in Accounts payable";
        }
        lblaccpayamt.Text = String.Format("{0:n}", Convert.ToDecimal(lblaccpayamt.Text));


        lblnotepayamt.Text = Convert.ToString((1) * (Convert.ToDecimal(lblnotepayamt.Text) - Convert.ToDecimal(lblnotepaylst.Text)));
        if (Convert.ToDecimal(lblnotepayamt.Text) > 0)
        {
            lblnotepay.Text = "Increase in Notes Payable";
        }
        else
        {
            lblnotepay.Text = "Decrease in Notes Payable";
        }
        lblnotepayamt.Text = String.Format("{0:n}", Convert.ToDecimal(lblnotepayamt.Text));



        lblintpayamt.Text = Convert.ToString((1) * (Convert.ToDecimal(lblintpayamt.Text) - Convert.ToDecimal(lblintpaylst.Text)));
        if (Convert.ToDecimal(lblintpayamt.Text) > 0)
        {
            lblintpay.Text = "Increase in Interest payable";
        }
        else
        {
            lblintpay.Text = "Decrease in Interest payable";
        }
        lblintpayamt.Text = String.Format("{0:n}", Convert.ToDecimal(lblintpayamt.Text));


        lbltaxpayamt.Text = Convert.ToString((1) * (Convert.ToDecimal(lbltaxpayamt.Text) - Convert.ToDecimal(lbltxtpaylst.Text)));
        if (Convert.ToDecimal(lbltaxpayamt.Text) > 0)
        {
            lbltaxpay.Text = "Increase in Taxes payable";
        }
        else
        {
            lbltaxpay.Text = "Decrease in Taxes payable";
        }
        lbltaxpayamt.Text = String.Format("{0:n}", Convert.ToDecimal(lbltaxpayamt.Text));


        lblothcurramt.Text = Convert.ToString((1) * (Convert.ToDecimal(lblothcurramt.Text) - Convert.ToDecimal(lblothcurrlst.Text)));
        if (Convert.ToDecimal(lblothcurramt.Text) > 0)
        {
            lblothcurr.Text = "Increase in Other current liabilities";
        }
        else
        {
            lblothcurr.Text = "Decrease in Other current liabilities";
        }
        lblothcurramt.Text = String.Format("{0:n}", Convert.ToDecimal(lblothcurramt.Text));


        lblspart12mamt.Text = Convert.ToString((1) * (Convert.ToDecimal(lblspart12mamt.Text) - Convert.ToDecimal(lblspart12mlist.Text)));
        if (Convert.ToDecimal(lblspart12mamt.Text) > 0)
        {
            lnkspart12mtext.Text = "Increase in Short-term (due within 12 months)";
        }
        else
        {
            lnkspart12mtext.Text = "Decrease in Short-term (due within 12 months)";
        }
        lblspart12mamt.Text = String.Format("{0:n}", Convert.ToDecimal(lblspart12mamt.Text));

        lblcpartlongamt.Text = Convert.ToString((1) * (Convert.ToDecimal(lblcpartlongamt.Text) - Convert.ToDecimal(lblcpartlonglist.Text)));
        if (Convert.ToDecimal(lblcpartlongamt.Text) > 0)
        {
            lnkcpartlongtext.Text = "Increase in Current part ( long-term debt)";
        }
        else
        {
            lnkcpartlongtext.Text = "Decrease in Current part ( long-term debt)";
        }
        lblcpartlongamt.Text = String.Format("{0:n}", Convert.ToDecimal(lblcpartlongamt.Text));

        pnldisplay(lblnotepayamt, pnlnotespay);
        pnldisplay(lblintpayamt, pnlintrestpay);
        pnldisplay(lbltaxpayamt, pnltaxpay);
        pnldisplay(lblothcurramt, pnlothercurrentlia);
        pnldisplay(lblaccpayamt, pnlaccpay);
        pnldisplay(lblspart12mamt, pnlspart12m);
        pnldisplay(lblcpartlongamt, pnlcpartltermdept);

    }
    protected void intrestandTaxPaid(string groupid, Label lblb)
    {
        decimal Ant = 0;
        DataTable dtamtd = new DataTable();

        dtamtd = (DataTable)select("Select sum(Tranction_Details.Amountdebit) as amtc from AccountMaster inner join Tranction_Details on Tranction_Details.AccountDebit=AccountMaster.AccountId  " +
 " inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=Tranction_Details.Tranction_Master_Id  where   AccountMaster.GroupId = '" + groupid + "' " +
 " and AccountMaster.Whid='" + ddwarehouse.SelectedValue + "' and Tranction_Details.Whid='" + ddwarehouse.SelectedValue + "'  and AccountMaster.AccountId in(Select Distinct AccountMaster.AccountId from AccountMaster inner join Tranction_Details on Tranction_Details.AccountDebit=AccountMaster.AccountId " +
" inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=Tranction_Details.Tranction_Master_Id " +
  " where   (AccountMaster.GroupId = '" + groupid + "' or AccountMaster.GroupId = '1') and AccountMaster.Whid='" + ddwarehouse.SelectedValue + "' and TranctionMaster.Whid='" + ddwarehouse.SelectedValue + "' and  Tranction_Details.whid='" + ddwarehouse.SelectedValue + "'" + str + ")" + str);
        if (dtamtd.Rows.Count > 0)
        {
            if (Convert.ToString(dtamtd.Rows[0]["amtc"]) != "")
            {

                Ant = Convert.ToDecimal(dtamtd.Rows[0]["amtc"].ToString());

            }
        }
        lblb.Text = Convert.ToString(Ant);
    }
    protected Decimal investigationpurchas(string groupid, DataList lblb, Panel pnlavv)
    {
        Decimal totpur = 0;
        DataTable dt = new DataTable();
        DataColumn Dcom = new DataColumn();
        Dcom.DataType = System.Type.GetType("System.String");
        Dcom.ColumnName = "GroupName";
        Dcom.AllowDBNull = true;
        Dcom.Unique = false;
        Dcom.ReadOnly = false;

        DataColumn Dcom1 = new DataColumn();
        Dcom1.DataType = System.Type.GetType("System.Decimal");
        Dcom1.ColumnName = "Amount";
        Dcom1.AllowDBNull = true;
        Dcom1.Unique = false;
        Dcom1.ReadOnly = false;

        DataColumn Dcom2 = new DataColumn();
        Dcom2.DataType = System.Type.GetType("System.String");
        Dcom2.ColumnName = "GroupId";
        Dcom2.AllowDBNull = true;
        Dcom2.Unique = false;
        Dcom2.ReadOnly = false;
        dt.Columns.Add(Dcom);
        dt.Columns.Add(Dcom1);
        dt.Columns.Add(Dcom2);
        DataTable datata = select("Select Distinct GroupCompanyMaster.GroupId,groupdisplayname as GroupName from GroupCompanyMaster inner Join AccountMaster on AccountMaster.GroupId=GroupCompanyMaster.groupid where AccountMaster.GroupId IN(" + groupid + ") and GroupCompanyMaster.whid='" + ddwarehouse.SelectedValue + "'   and AccountMaster.Whid='" + ddwarehouse.SelectedValue + "' Order by groupdisplayname");
        foreach (DataRow item in datata.Rows)
        {
            DataTable dtamtd = (DataTable)select("Select sum(Tranction_Details.Amountdebit) as Amount from AccountMaster inner join Tranction_Details on Tranction_Details.AccountDebit=AccountMaster.AccountId  " +
 " inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=Tranction_Details.Tranction_Master_Id  where   AccountMaster.GroupId = '" + item["GroupId"] + "' " +
 " and AccountMaster.Whid='" + ddwarehouse.SelectedValue + "' and Tranction_Details.Whid='" + ddwarehouse.SelectedValue + "'  and AccountMaster.AccountId in(Select Distinct AccountMaster.AccountId from AccountMaster inner join Tranction_Details on Tranction_Details.AccountDebit=AccountMaster.AccountId " +
    " inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=Tranction_Details.Tranction_Master_Id " +
      " where   (AccountMaster.GroupId = '" + item["GroupId"] + "' or AccountMaster.GroupId = '1') and AccountMaster.Whid='" + ddwarehouse.SelectedValue + "' and TranctionMaster.Whid='" + ddwarehouse.SelectedValue + "' and  Tranction_Details.whid='" + ddwarehouse.SelectedValue + "'" + str + ")" + str);
            if (dtamtd.Rows.Count > 0)
            {
                if (Convert.ToString(dtamtd.Rows[0]["Amount"]) != "")
                {
                    DataRow Drow = dt.NewRow();
                    Drow["GroupName"] = item["GroupName"].ToString();
                    Drow["Amount"] = (-1) * Convert.ToDecimal(dtamtd.Rows[0]["Amount"]);
                    Drow["GroupId"] = item["GroupId"].ToString();
                    dt.Rows.Add(Drow);
                    totpur += (-1) * Convert.ToDecimal(dtamtd.Rows[0]["Amount"]);
                }

            }
        }
        lblb.DataSource = dt;
        lblb.DataBind();
        if (totpur != 0)
        {
            pnlavv.Visible = true;
        }
        else
        {
            pnlavv.Visible = false;
        }
        return totpur;
    }
    protected Decimal investigationpursal(string groupid, DataList lblb, Panel pnlavv)
    {
        Decimal totsal = 0;
        DataTable dt = new DataTable();
        DataColumn Dcom = new DataColumn();
        Dcom.DataType = System.Type.GetType("System.String");
        Dcom.ColumnName = "GroupName";
        Dcom.AllowDBNull = true;
        Dcom.Unique = false;
        Dcom.ReadOnly = false;

        DataColumn Dcom1 = new DataColumn();
        Dcom1.DataType = System.Type.GetType("System.Decimal");
        Dcom1.ColumnName = "Amount";
        Dcom1.AllowDBNull = true;
        Dcom1.Unique = false;
        Dcom1.ReadOnly = false;

        DataColumn Dcom2 = new DataColumn();
        Dcom2.DataType = System.Type.GetType("System.String");
        Dcom2.ColumnName = "GroupId";
        Dcom2.AllowDBNull = true;
        Dcom2.Unique = false;
        Dcom2.ReadOnly = false;
        dt.Columns.Add(Dcom);
        dt.Columns.Add(Dcom1);
        dt.Columns.Add(Dcom2);
        DataTable datata = select("Select Distinct GroupCompanyMaster.GroupId,groupdisplayname as GroupName from GroupCompanyMaster inner Join AccountMaster on AccountMaster.GroupId=GroupCompanyMaster.groupid where AccountMaster.GroupId IN(" + groupid + ") and GroupCompanyMaster.whid='" + ddwarehouse.SelectedValue + "'   and AccountMaster.Whid='" + ddwarehouse.SelectedValue + "' Order by groupdisplayname");
        foreach (DataRow item in datata.Rows)
        {
            DataTable dtamtd = (DataTable)select("Select sum(Tranction_Details.Amountcredit) as Amount from AccountMaster inner join Tranction_Details on Tranction_Details.AccountCredit=AccountMaster.AccountId  " +
 " inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=Tranction_Details.Tranction_Master_Id  where   AccountMaster.GroupId = '" + item["GroupId"] + "' " +
 " and AccountMaster.Whid='" + ddwarehouse.SelectedValue + "' and Tranction_Details.Whid='" + ddwarehouse.SelectedValue + "'  and AccountMaster.AccountId in(Select Distinct AccountMaster.AccountId from AccountMaster inner join Tranction_Details on Tranction_Details.AccountCredit=AccountMaster.AccountId " +
             " inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=Tranction_Details.Tranction_Master_Id " +
             " where   (AccountMaster.GroupId = '" + item["GroupId"] + "' or AccountMaster.GroupId = '1') and AccountMaster.Whid='" + ddwarehouse.SelectedValue + "' and TranctionMaster.Whid='" + ddwarehouse.SelectedValue + "' and  Tranction_Details.whid='" + ddwarehouse.SelectedValue + "'" + str + ")" + str);
            if (dtamtd.Rows.Count > 0)
            {


                if (Convert.ToString(dtamtd.Rows[0]["Amount"]) != "")
                {
                    DataRow Drow = dt.NewRow();
                    Drow["GroupName"] = item["GroupName"].ToString();
                    Drow["Amount"] = Convert.ToDecimal(dtamtd.Rows[0]["Amount"]);
                    Drow["GroupId"] = item["GroupId"].ToString();
                    dt.Rows.Add(Drow);
                    totsal += Convert.ToDecimal(dtamtd.Rows[0]["Amount"]);
                }

            }
        }
        lblb.DataSource = dt;
        lblb.DataBind();
        if (totsal != 0)
        {
            pnlavv.Visible = true;
        }
        else
        {
            pnlavv.Visible = false;
        }
        return totsal;

    }
    protected void fillcashflow()
    {
        lblincomamt.Text = lblincbeforitamt.Text;
        lbltextdepamor.Text = lbldep.Text;
        lbldeprmortize.Text = lbldepamt.Text;
        //revandgain(1, datalistrevgain, "44");
        Decimal totga = revandgain("44", datalistrevgain, pnlgain);
        Decimal totex = revandgain("45", Datalistexploss, pnlexp);
        //revandgain(1, Datalistexploss, "45");
        lblintexamt.Text = lblintrestexpamt.Text;
        lblincbef.Text = Math.Round(Convert.ToDecimal(lbldeprmortize.Text) - totga + totex + Convert.ToDecimal(lblintrestexpamt.Text), 2).ToString();
        //////CurrentAsset
        currentacces();
        currentliab();

        Decimal totalcashop = Convert.ToDecimal(lblincomamt.Text) + Convert.ToDecimal(lblincbef.Text) + Convert.ToDecimal(lblaccrecamt.Text) + Convert.ToDecimal(lblnotrecamt.Text) + Convert.ToDecimal(lblinvamt.Text) + Convert.ToDecimal(lblprepaidexpamt.Text) + Convert.ToDecimal(lblorhecurrassetamt.Text) + Convert.ToDecimal(lblaccpayamt.Text) + Convert.ToDecimal(lblnotepayamt.Text) + Convert.ToDecimal(lblintpayamt.Text) + Convert.ToDecimal(lbltaxpayamt.Text) + Convert.ToDecimal(lblothcurramt.Text) + Convert.ToDecimal(lblcpartlongamt.Text) + Convert.ToDecimal(lblspart12mamt.Text);
        lblcashgeneopration.Text = String.Format("{0:n}", Math.Round(totalcashop, 2));
        intrestandTaxPaid("37", lblinrestpaidcash);

        lblinrestpaidcash.Text = Convert.ToString(Convert.ToDecimal(lblinrestpaidcash.Text) * -1);
        lblinrestpaidcash.Text = String.Format("{0:n}", Math.Round(Convert.ToDecimal(lblinrestpaidcash.Text), 2));

        intrestandTaxPaid("47", lblinctaxpaidcash);

        lblinctaxpaidcash.Text = Convert.ToString(Convert.ToDecimal(lblinctaxpaidcash.Text) * -1);
        lblinctaxpaidcash.Text = String.Format("{0:n}", Math.Round(Convert.ToDecimal(lblinctaxpaidcash.Text), 2));
        Decimal totnetcash = totalcashop + Convert.ToDecimal(lblinrestpaidcash.Text) + Convert.ToDecimal(lblinctaxpaidcash.Text);
        if (totnetcash > 0)
        {
            lblnetcashpoeration.Text = "Net cash from operating activities";
        }
        else
        {
            lblnetcashpoeration.Text = "Net cash used in operating activities";
        }
        lblnetcashpoerationamt.Text = String.Format("{0:n}", Math.Round(totnetcash, 2));
        string gin = "'9','6','7','10','8','58','60','59','13','11','61','12','14'";
        Decimal Totperc = investigationpurchas(gin, datainvestpur, pnlproccashpurinvestact);
        lblpurcamt.Text = String.Format("{0:n}", Math.Round(Totperc, 2));
        Decimal Totsalesa = investigationpursal(gin, datasale, pnlsaleof);
        lblsaleamt.Text = String.Format("{0:n}", Math.Round(Totsalesa, 2));

        //Decimal cashinsvesttot = Convert.ToDecimal(lblpurcamt.Text) + Convert.ToDecimal(lblsaleamt.Text) + Convert.ToDecimal(lblaccrecamt.Text) + Convert.ToDecimal(lblinstreciva.Text) + Convert.ToDecimal(lbldivirecv.Text);
        Decimal cashinsvesttot = Convert.ToDecimal(lblpurcamt.Text) + Convert.ToDecimal(lblsaleamt.Text) + Convert.ToDecimal(lblaccrecamt.Text);
        lnlnetinvestcashamt.Text = String.Format("{0:n}", Math.Round(cashinsvesttot, 2));
        if (totnetcash > 0)
        {
            lnlnetinvestcash.Text = "Net cash from investing activities";
        }
        else
        {
            lnlnetinvestcash.Text = "Net cash used in investing activities";
        }
        ///////Cash Flows From Financing Activities
        string ginfin = "'24','25','26','65','66'";
        Decimal Totfinacashdr = investigationpursal(ginfin, DataIssue, pnlissue);
        lblnetissue.Text = String.Format("{0:n}", Math.Round(Totfinacashdr, 2));

        Decimal Totfinacashcr = investigationpurchas(ginfin, datapayment, pnlpaysa);
        lblnetpayment.Text = String.Format("{0:n}", Math.Round(Totfinacashcr, 2));

        string ginlongbr = "'62','63','64','21','22','23'";
        Decimal Totlongbrow = investigationpursal(ginlongbr, datalongbro,pnlborrow );
        lblnetbrowser.Text = String.Format("{0:n}", Math.Round(Totlongbrow, 2));

        intrestandTaxPaid("52", lbldividpaid);

        lbldividpaid.Text = Convert.ToString(Convert.ToDecimal(lbldividpaid.Text) * -1);
        lbldividpaid.Text = String.Format("{0:n}", Math.Round(Convert.ToDecimal(lbldividpaid.Text), 2));


        Decimal netcashfinan = Convert.ToDecimal(lblnetissue.Text) + Convert.ToDecimal(lblnetpayment.Text) + Convert.ToDecimal(lblnetbrowser.Text) + Convert.ToDecimal(lbldividpaid.Text);
        lblnetcashfinanact.Text = String.Format("{0:n}", Math.Round(netcashfinan, 2));
        if (netcashfinan > 0)
        {
            lblnettextfi.Text = "Net cash from financing activities";
        }
        else
        {
            lblnettextfi.Text = "Net cash used in financing activities";
        }

        /////////////Opening Balance of Cash and Bank

        Decimal casheque = Convert.ToDecimal(lblnetcashpoerationamt.Text) + Convert.ToDecimal(lnlnetinvestcashamt.Text) + Convert.ToDecimal(lblnetcashfinanact.Text);
        lblcashequivalents.Text = String.Format("{0:n}", Math.Round(casheque, 2));
        lblopcashbal.Text = lblcashlast.Text;
        Decimal cashclo = Convert.ToDecimal(lblopcashbal.Text) + Convert.ToDecimal(lblcashequivalents.Text);
        lblcloscashbank.Text = String.Format("{0:n}", Math.Round(cashclo, 2));

        //decimal icwdworkcap = 0;
        //icwdworkcap =(-1)*(Convert.ToDecimal(lbltotnetasstamt.Text)  - Convert.ToDecimal(lblcashamt.Text)) -  (Convert.ToDecimal(lbltotnetasstlst.Text)- Convert.ToDecimal(lblcashlast.Text));
        //lblincworkingcap.Text = String.Format("{0:n}", icwdworkcap);
        //decimal cashopein = Convert.ToDecimal(lblincomamt.Text) + Convert.ToDecimal(lbldeprmortize.Text) + icwdworkcap;
        /////////////Cash Flows From Operating Activities
        //lblcashfloworeratingact.Text = String.Format("{0:n}", cashopein);

        //decimal totfixass = (-1) * Convert.ToDecimal(lbltotfixassamt.Text) - Convert.ToDecimal(lbltotfixasslst.Text);
        //lbltotfixasset.Text = String.Format("{0:n}", totfixass);
        //decimal totfinvesment = (-1) * Convert.ToDecimal(lbltotinveamt.Text) - Convert.ToDecimal(lbltotinvelst.Text);
        //lbltotinvestment.Text = String.Format("{0:n}", totfinvesment);
        //decimal totintangibleAsset = (-1) * Convert.ToDecimal(lblothintengibletotamt.Text) - Convert.ToDecimal(lblotnintengibletotlst.Text);
        //lblintangibleAsset.Text = String.Format("{0:n}", totintangibleAsset);
        //decimal otheasse = Convert.ToDecimal(lblbondisucostamt.Text) + Convert.ToDecimal(lbldepositeamt.Text) + Convert.ToDecimal(lblothernonassetamt.Text);
        //otheasse = otheasse - Convert.ToDecimal(lblbondisucostlst.Text) + Convert.ToDecimal(lbldepositelst.Text) + Convert.ToDecimal(lblothernonassetlst.Text);
        //otheasse = otheasse * (-1);
        //lblotheassetincd.Text = String.Format("{0:n}", otheasse);
        /////////////Total Cash Flows From Investing Activities

        //decimal invActivitys = Convert.ToDecimal(lbltotfixasset.Text) + Convert.ToDecimal(lbltotinvestment.Text) + Convert.ToDecimal(lblintangibleAsset.Text) + otheasse;
        //lblcashflowinvact.Text = String.Format("{0:n}", invActivitys);

        //decimal totlongte = Convert.ToDecimal(lbltotliaamt.Text) - Convert.ToDecimal(lbltotlialst.Text);
        //lbllongtermlia.Text = String.Format("{0:n}", totlongte);

        //decimal totequ = Convert.ToDecimal(lbltotequityamt.Text) - Convert.ToDecimal(lbltotequitylst.Text) - Convert.ToDecimal(lblretearningamt.Text) + Convert.ToDecimal(lblretearninglst.Text);
        //lbltotequi.Text = String.Format("{0:n}", totequ);


        /////////////Cash Flows From Financing Activities
        //decimal totfinaacti = Convert.ToDecimal(lbllongtermlia.Text) + Convert.ToDecimal(lbltotequi.Text);
        //lbltotfinancactivity.Text = String.Format("{0:n}", totfinaacti);
        /////////////Net Cash Flows for the Business
        //lblnetcashflowinbus.Text = String.Format("{0:n}", (cashopein + invActivitys + totfinaacti));
        /////////////Opening Balance of Cash and Bank
        lblopcashbal.Text = lblcashlast.Text;
        /////////////Closing Balance of Cash and Bank
        //decimal totcloscashbank = Convert.ToDecimal(lblnetcashflowinbus.Text) + Convert.ToDecimal(lblopcashbal.Text);
        //lblcloscashbank.Text = String.Format("{0:n}", totcloscashbank);
    }

}
