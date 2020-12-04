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

public partial class Add_Account_Year : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(PageConn.connnn);
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

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();


        Page.Title = pg.getPageTitle(page);
        if (!IsPostBack)
        {
            Pagecontrol.dypcontrol(Page, page);
            ViewState["sortOrder"] = "";
            fillstore();
            ddlbusiness_SelectedIndexChanged(sender, e);
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
    protected void fillstore()
    {
        DataTable ds = ClsStore.SelectStorename();
        ddlbusiness.DataSource = ds;
        ddlbusiness.DataTextField = "Name";
        ddlbusiness.DataValueField = "WareHouseId";
        ddlbusiness.DataBind();


        if (Request.QueryString["Wid"] != null)
        {
            ddlbusiness.SelectedValue = Convert.ToString(Request.QueryString["Wid"]);
        }
        else
        {
            DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

            if (dteeed.Rows.Count > 0)
            {
                ddlbusiness.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
            }
        }

    }

    public void fillgrid()
    {
        string ws = "";
        if (ddlbusiness.SelectedIndex > -1)
        {
            ws = " and Whid='" + ddlbusiness.SelectedValue + "'";
        }
        DataTable dtda = (DataTable)select("select Distinct WarehouseMaster.Name as Wname,ReportPeriod.StartDate,ReportPeriod.EndDate,ReportPeriod.Whid, ReportPeriod.Active, ReportPeriod.Report_Period_Id,Convert(nvarchar,StartDate,101) as StartDate, Convert(nvarchar,EndDate,101) as EndDate from [ReportPeriod] inner join WarehouseMaster on WarehouseMaster.WarehouseId=ReportPeriod.Whid   where Compid = '" + Session["comid"] + "'" + ws + " Order by  WarehouseMaster.Name, ReportPeriod.StartDate,ReportPeriod.EndDate ");

        lblBusiness.Text = Session["Cname"].ToString();
        lblBusinessN.Text = ddlbusiness.SelectedItem.Text;

        if (dtda.Rows.Count > 0)
        {
            DataView myDataView = new DataView();
            myDataView = dtda.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }

            grd.DataSource = myDataView;
            grd.DataBind();

            string openingdate = "select Distinct Report_period_confirm.Id as conid, ReportPeriod.Report_Period_Id, StartDate,EndDate from ReportPeriod left join Report_period_confirm on Report_period_confirm.Report_Period_Id=ReportPeriod.Report_Period_Id where Compid='" + Session["Comid"].ToString() + "' and Whid='" + ddlbusiness.SelectedValue + "' and Active='1'";
            SqlCommand cmd22221 = new SqlCommand(openingdate, con);
            SqlDataAdapter adp22221 = new SqlDataAdapter(cmd22221);
            DataTable ds112221 = new DataTable();
            adp22221.Fill(ds112221);
            if (ds112221.Rows.Count > 0)
            {
                ViewState["CurrentActiveStartDate"] = ds112221.Rows[0]["StartDate"].ToString();
                ViewState["CurrentActiveEndDate"] = ds112221.Rows[0]["EndDate"].ToString();
            }

            foreach (GridViewRow gdr in grd.Rows)
            {


                Label lblreportmasterid = (Label)gdr.FindControl("lblreportmasterid");
                CheckBox chkactive = (CheckBox)gdr.FindControl("chkactive");



                string str1 = "select Name,Report_Period_Id,StartDate,EndDate from ReportPeriod where Report_Period_Id='" + lblreportmasterid.Text + "' ";
                SqlCommand cmd1 = new SqlCommand(str1);
                cmd1.Connection = con;
                SqlDataAdapter da1 = new SqlDataAdapter();
                da1.SelectCommand = cmd1;
                DataTable ds1 = new DataTable();
                da1.Fill(ds1);
                if (ds1.Rows.Count > 0)
                {
                    ViewState["SelectedYearStartDate"] = ds1.Rows[0]["StartDate"].ToString();
                    ViewState["SelectedYearEndDate"] = ds1.Rows[0]["EndDate"].ToString();
                }

                DateTime dt = Convert.ToDateTime(ViewState["CurrentActiveEndDate"]);
                DateTime dt2 = dt.AddYears(1);
                DateTime dt3 = dt.AddYears(-1);

                DateTime dtnewdate = Convert.ToDateTime(ViewState["SelectedYearEndDate"]);

                if (dtnewdate == dt2 || dtnewdate == dt3)
                {
                    chkactive.Enabled = true;
                }
                else
                {
                    chkactive.Enabled = false;
                }


            }
            if (grd.Rows.Count > 0)
            {
                CheckBox chkactive = (CheckBox)grd.Rows[0].FindControl("chkactive");
                if (Convert.ToString(ds112221.Rows[0]["conid"]) != "")
                {

                    chkactive.Enabled = false;

                }
                else
                {
                    chkactive.Enabled = true;

                }
            }
        

        }
        else
        {
            grd.DataSource = null;
            grd.DataBind();

        }
    }

    protected void grid_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        fillgrid();
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
    protected void ddlbusiness_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        fillgrid();
    }
    protected void chkactive_chachedChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((CheckBox)sender).Parent.Parent as GridViewRow;

        int rinrow = row.RowIndex;

        ViewState["id"] = Convert.ToString(grd.DataKeys[rinrow].Value);
        Label lblWhid = (Label)grd.Rows[rinrow].FindControl("lblWhid");
        Label lblEndd = (Label)grd.Rows[rinrow].FindControl("lbledate");
        ViewState["Whid"] = lblWhid.Text;

        string openingdate = "select StartDate,EndDate,Report_Period_Id from ReportPeriod where Compid='" + Session["Comid"].ToString() + "' and Whid='" + ViewState["Whid"] + "' and Active='1'";
        SqlCommand cmd22221 = new SqlCommand(openingdate, con);
        SqlDataAdapter adp22221 = new SqlDataAdapter(cmd22221);
        DataTable ds112221 = new DataTable();
        adp22221.Fill(ds112221);
        if (ds112221.Rows.Count > 0)
        {
            ViewState["CurrentActiveStartDate"] = ds112221.Rows[0]["StartDate"].ToString();
            ViewState["CurrentActiveEndDate"] = ds112221.Rows[0]["EndDate"].ToString();
            ViewState["RPACT"] = ds112221.Rows[0]["EndDate"].ToString();
        }


        string str1 = "select Name,Report_Period_Id,StartDate,EndDate from ReportPeriod where EndDate='" + lblEndd.Text + "' and Whid='" + ViewState["Whid"] + "'";
        SqlCommand cmd1 = new SqlCommand(str1);
        cmd1.Connection = con;
        SqlDataAdapter da1 = new SqlDataAdapter();
        da1.SelectCommand = cmd1;
        DataTable ds1 = new DataTable();
        da1.Fill(ds1);

        if (ds1.Rows.Count > 0)
        {
            ViewState["SelectedYearStartDate"] = ds1.Rows[0]["StartDate"].ToString();
            ViewState["SelectedYearEndDate"] = ds1.Rows[0]["EndDate"].ToString();
            ViewState["id"] = ds1.Rows[0]["Report_Period_Id"].ToString();
        }

        DateTime dt = Convert.ToDateTime(ViewState["CurrentActiveEndDate"]);
        DateTime dt2 = dt.AddYears(1);
        DateTime dt3 = dt.AddYears(-1);

        DateTime dtnewdate = Convert.ToDateTime(ViewState["SelectedYearEndDate"]);

        if (dtnewdate == dt2 || dtnewdate == dt3)
        {
            ModalPopupExtender122.Show();
        }
        else
        {
            ModalPopupExtender1.Show();
        }




    }
    protected void activelastacc()
    {
        string str1 = "select Name,Report_Period_Id,StartDate,EndDate from ReportPeriod where Report_Period_Id='" + ViewState["id"] + "' and Whid='" + ViewState["Whid"] + "'";
        SqlCommand cmd1 = new SqlCommand(str1);
        cmd1.Connection = con;
        SqlDataAdapter da1 = new SqlDataAdapter();
        da1.SelectCommand = cmd1;
        DataTable ds1 = new DataTable();
        da1.Fill(ds1);

        if (ds1.Rows.Count >= 1)
        {
            string strdate1 = "";
            string lastdate = "";
            string strdate2 = "";
            string lasttwodata = "";
            string[] separator1 = new string[] { " TO " };
            string[] strSplitArr1 = ds1.Rows[0]["Name"].ToString().Split(separator1, StringSplitOptions.RemoveEmptyEntries);
            int i111 = Convert.ToInt32(strSplitArr1.Length);
            if (i111 == 2 || i111 == 1)
            {
                int fr = Convert.ToInt32(strSplitArr1[0].ToString());
                int sec = 0;
                if (i111 > 1)
                {
                     sec = Convert.ToInt32(strSplitArr1[1].ToString());

                }
                int ddt1 = fr - 1;
                int dde1 = sec - 1;
                string peram = "";
                strdate1 = ddt1.ToString();
                lastdate = strdate1.Substring(2);
                if (i111 == 2)
                {
                    strdate2 = dde1.ToString();
                    lasttwodata = strdate2.Substring(2);
                    peram = ddt1 + " TO " + dde1;
                }
                else
                {
                    peram = ddt1.ToString();
                }
                string addsdate = Convert.ToDateTime(ds1.Rows[0]["StartDate"]).AddMonths(-12).ToShortDateString();
                string addedate = Convert.ToDateTime(ds1.Rows[0]["EndDate"]).AddMonths(-12).ToShortDateString();
                SqlCommand cmdtr = new SqlCommand("Insert into ReportPeriod(Name,StartDate,EndDate,FistStartDate,Active,Compid,Whid)Values('" + peram + "','" + addsdate + "','" + addedate + "','" + DateTime.Now.ToShortDateString() + "','0','" + Session["comid"] + "','" + ViewState["Whid"] + "') ", con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdtr.ExecuteNonQuery();
                con.Close();
                SqlCommand cmsa = new SqlCommand("SELECT max(Report_Period_Id) from ReportPeriod where Compid='" + Session["comid"] + "' and Whid='" + ViewState["Whid"] + "'", con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                object xde = cmsa.ExecuteScalar();
                con.Close();
                string str5t = "select Id from AccountMaster where Whid='" + ViewState["Whid"] + "'";
                SqlCommand cmd32t = new SqlCommand(str5t, con);
                SqlDataAdapter adp15t = new SqlDataAdapter(cmd32t);
                DataTable dtlogin14t = new DataTable();
                adp15t.Fill(dtlogin14t);
                foreach (DataRow dr in dtlogin14t.Rows)
                {
                    SqlCommand cmdtr1 = new SqlCommand("Insert into AccountBalance(AccountMasterId,Balance,Report_Period_Id)Values('" + dr["Id"] + "','0','" + xde + "') ", con);

                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdtr1.ExecuteNonQuery();
                    con.Close();
                }
                DocumentCls1 clsDocument = new DocumentCls1();

                //  Int32 mainyearcabinet = clsDocument.InsertDocumentMainType("Accounting", ddlbusiness.SelectedValue);

                string maincabinetmax = "select *  from DocumentMainType where CID='" + Session["comid"].ToString() + "' and Whid='" + ddlbusiness.SelectedValue + "' and DocumentMainType='Accounting' ";
                SqlCommand cmdmaincabinetmax = new SqlCommand(maincabinetmax, con);
                SqlDataAdapter adpmaincabinetmax = new SqlDataAdapter(cmdmaincabinetmax);
                DataTable dtmaincabinetmax = new DataTable();
                adpmaincabinetmax.Fill(dtmaincabinetmax);

                if (dtmaincabinetmax.Rows.Count > 0)
                {
                    Int32 mainyeardrawer = clsDocument.InsertDocumentSubType(Convert.ToInt32(dtmaincabinetmax.Rows[0]["DocumentMainTypeId"]), "" + peram + "");

                    string maindrawermax = "select Max(DocumentSubTypeId) as DocumentSubTypeId  from DocumentSubType ";
                    SqlCommand just = new SqlCommand(maindrawermax, con);
                    SqlDataAdapter adpmaindrawermax = new SqlDataAdapter(just);
                    DataTable dtmaindrawermax = new DataTable();
                    adpmaindrawermax.Fill(dtmaindrawermax);

                    if (dtmaindrawermax.Rows.Count > 0)
                    {
                        string perment = "";
                        if (i111 == 2)
                        {
                            perment = lastdate + "-" + lasttwodata;
                        }
                        else
                        {
                            perment = peram;
                        }
                        Int32 mainyearfolder = clsDocument.InsertDocumentType1(Convert.ToInt32(dtmaindrawermax.Rows[0]["DocumentSubTypeId"]), "Purchase Invoice" + " " + perment);
                        Int32 mainyearfolder1 = clsDocument.InsertDocumentType1(Convert.ToInt32(dtmaindrawermax.Rows[0]["DocumentSubTypeId"]), "Cash Payments" + " " + perment);
                        Int32 mainyearfolder2 = clsDocument.InsertDocumentType1(Convert.ToInt32(dtmaindrawermax.Rows[0]["DocumentSubTypeId"]), "Cash Receipts" + " " + perment);
                        Int32 mainyearfolder3 = clsDocument.InsertDocumentType1(Convert.ToInt32(dtmaindrawermax.Rows[0]["DocumentSubTypeId"]), "Journal Entry" + " " + perment);
                        Int32 mainyearfolder4 = clsDocument.InsertDocumentType1(Convert.ToInt32(dtmaindrawermax.Rows[0]["DocumentSubTypeId"]), "Sales Invoice" + " " + perment);
                        Int32 mainyearfolder5 = clsDocument.InsertDocumentType1(Convert.ToInt32(dtmaindrawermax.Rows[0]["DocumentSubTypeId"]), "Retail Sales Invoice" + " " + perment);
                        Int32 mainyearfolder6 = clsDocument.InsertDocumentType1(Convert.ToInt32(dtmaindrawermax.Rows[0]["DocumentSubTypeId"]), "Debit-Credit Note" + " " + perment);
                        Int32 mainyearfolder7 = clsDocument.InsertDocumentType1(Convert.ToInt32(dtmaindrawermax.Rows[0]["DocumentSubTypeId"]), "Quick Cash Entry" + " " + perment);
                        Int32 mainyearfolder8 = clsDocument.InsertDocumentType1(Convert.ToInt32(dtmaindrawermax.Rows[0]["DocumentSubTypeId"]), "Paypal Cash/Bank Receipts" + " " + perment);
                        Int32 mainyearfolder9 = clsDocument.InsertDocumentType1(Convert.ToInt32(dtmaindrawermax.Rows[0]["DocumentSubTypeId"]), "Paypal Cash/Bank Payments" + " " + perment);
                        Int32 mainyearfolder10 = clsDocument.InsertDocumentType1(Convert.ToInt32(dtmaindrawermax.Rows[0]["DocumentSubTypeId"]), "Cash Statement" + " " + perment);
                        Int32 mainyearfolder11 = clsDocument.InsertDocumentType1(Convert.ToInt32(dtmaindrawermax.Rows[0]["DocumentSubTypeId"]), "Paypal Statement" + " " + perment);

                    }

                }
                lblmsg.Text = "New accounting year created sucessfully";
                lblmsg.Visible = true;
                fillgrid();
            }
        }
    }
    protected void btnapd_Click(object sender, EventArgs e)
    {

        DataTable dtsg = select("select Whid, Name,Report_Period_Id,Cast(StartDate as Date) as StartDate,Cast(EndDate as Date)  as EndDate,FistStartDate,Name from ReportPeriod where Report_Period_Id='" + ViewState["id"] + "' and Whid='" + ViewState["Whid"] + "'");

        if (dtsg.Rows.Count > 0)
        {
            dtsg = select("select ReportPeriod.Report_Period_Id from ReportPeriod  where Compid = '" + Session["comid"] + "' and Whid='" + ViewState["Whid"] + "' and EndDate <'" + dtsg.Rows[0]["EndDate"] + "' order by EndDate Desc ");
        }
        if (dtsg.Rows.Count <= 0)
        {
            activelastacc();
        }



        fillamount();
        SqlCommand cmdtr = new SqlCommand("Update ReportPeriod set Active='1' where  Report_Period_Id='" + ViewState["id"] + "'  and Compid= '" + Session["comid"] + "' and Whid='" + ViewState["Whid"] + "'  ", con);

        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmdtr.ExecuteNonQuery();
        con.Close();

        fillgrid();

        lblmsg.Text = "Current active accounting year changed successfully";
        lblmsg.Visible = true;
    }
    protected void fillamount()
    {

        Decimal Totalretailbal = 0;
        //  Decimal opnbal1 = 0;
        string str1 = "select Whid, Name,Report_Period_Id,Cast(StartDate as Date) as StartDate,Cast(EndDate as Date)  as EndDate,FistStartDate,Name from ReportPeriod where Compid = '" + Session["comid"] + "' and Whid='" + ViewState["Whid"] + "' and Active='1'";
        SqlCommand cmd12 = new SqlCommand(str1);
        cmd12.Connection = con;
        SqlDataAdapter da12 = new SqlDataAdapter();
        da12.SelectCommand = cmd12;
        DataTable ds12 = new DataTable();
        da12.Fill(ds12);
        //string bal_cal = "";
        //string str145 = "select ReportPeriod.Report_Period_Id from ReportPeriod  where Compid = '" + Session["comid"] + "' and Whid='" + ViewState["Whid"] + "' and ReportPeriod.Report_Period_Id<'" + ds12.Rows[0]["Report_Period_Id"] + "' order by ReportPeriod.Report_Period_Id Desc ";
        //SqlCommand cmd121 = new SqlCommand(str145);
        //cmd121.Connection = con;
        //SqlDataAdapter da121 = new SqlDataAdapter();
        //da121.SelectCommand = cmd121;
        DataTable ds121 = new DataTable();
        //da121.Fill(ds121);

        //if (ds121.Rows.Count <= 0)
        //{
        ds121 = select("select  Report_Period_Id from ReportPeriod  where Compid = '" + Session["comid"] + "' and Whid='" + ViewState["Whid"] + "' and EndDate<'" + ds12.Rows[0]["EndDate"] + "' order by EndDate Desc");
        //}
        int k = 0;
        if (ds121.Rows.Count > 1)
        {
            k += 1;
            // opnbal1 = Convert.ToDecimal(opnbal1) + (Convert.ToDecimal(ds121.Rows[0]["Balance"].ToString()));
            // bal_cal = opnbal1.ToString();
        }
        else
        {

        }

        // CostGroup.fillcost(Convert.ToString(ViewState["Whid"]), Convert.ToString(ds12.Rows[0]["StartDate"]), Convert.ToString(ds12.Rows[0]["StartDate"]), Convert.ToString(ds12.Rows[0]["Report_Period_Id"]), false, "");

        DataTable dtaa = new DataTable();
        dtaa = (DataTable)select("Select Distinct Id from AccountMaster where Id not in(select AccountMasterId  from AccountBalance where Report_Period_Id='" + ds12.Rows[0]["Report_Period_Id"] + "')  and Whid='" + ViewState["Whid"] + "'");
        foreach (DataRow dr in dtaa.Rows)
        {
            SqlCommand cmdtr11 = new SqlCommand("Insert into AccountBalance(AccountMasterId,Balance,Report_Period_Id)Values('" + dr["Id"] + "','0','" + ds121.Rows[0]["Report_Period_Id"] + "') ", con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdtr11.ExecuteNonQuery();
            con.Close();

            SqlCommand cmdtr1 = new SqlCommand("Insert into AccountBalance(AccountMasterId,Balance,Report_Period_Id)Values('" + dr["Id"] + "','0','" + ds12.Rows[0]["Report_Period_Id"] + "') ", con);

            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdtr1.ExecuteNonQuery();
            con.Close();
        }
        DataTable drst = select("Select Distinct Id from ClassMaster inner join GroupMaster on   GroupMaster.ClassId=ClassMaster.ClassId Inner join  AccountMaster    ON AccountMaster.GroupId=GroupMaster.GroupId where Id not in( SELECT     AccountBalance_InStatment.AccountMasterId " +
               "  FROM  ClassMaster inner join GroupMaster on  GroupMaster.ClassId=ClassMaster.ClassId Inner join  AccountMaster    ON AccountMaster.GroupId=GroupMaster.GroupId inner join AccountBalance_InStatment on AccountBalance_InStatment.AccountMasterId=AccountMaster.Id where ClassMaster.ClassTypeId in('15','19') and  Report_Period_Id='" + ds12.Rows[0]["Report_Period_Id"] + "' and  AccountMaster.whid='" + ViewState["Whid"] + "') and  AccountMaster.whid='" + ViewState["Whid"] + "' and ClassMaster.ClassTypeId in('15','19')");

        string acr = "Insert into AccountBalance_InStatment(AccountMasterId,Balance,Report_Period_Id,CrBalance,DrBalance,Cr)Values";

        int cc = 0;
        foreach (DataRow dr in drst.Rows)
        {
            if (cc > 0)
            {
                acr = acr + ",";

            }

            acr = acr + "('" + dr["Id"] + "','0','" + ds12.Rows[0]["Report_Period_Id"] + "','0','0','1') ";


            cc += 1;
        }
        if (cc > 0)
        {
            SqlCommand cmdtr11fs = new SqlCommand(acr, con);

            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdtr11fs.ExecuteNonQuery();
            con.Close();
        }

        if (ds12.Rows.Count > 0)
        {


            DataTable dtamtc = new DataTable();
            dtamtc = (DataTable)select("Select ClassMaster.ClassTypeId, AccountMaster.AccountId,AccountBalance.Balance,AccountBalance.Account_Balance_Id from ReportPeriod inner join AccountBalance on AccountBalance.Report_Period_Id=ReportPeriod.Report_Period_Id inner join AccountMaster on AccountMaster.Id=AccountBalance.AccountMasterId  inner join  GroupMaster on AccountMaster.GroupId=GroupMaster.GroupId inner join ClassMaster on GroupMaster.ClassId=ClassMaster.ClassId  where  ClassMaster.ClassTypeId NOT In('15','19') and ReportPeriod.Report_Period_Id='" + ds12.Rows[0]["Report_Period_Id"] + "'");

            foreach (DataRow gdr5 in dtamtc.Rows)
            {
                Decimal totcrdit = 0;
                Decimal totdebit = 0;
                Decimal balance = 0;
                DataTable dtcredit = new DataTable();
                dtcredit = (DataTable)select("Select sum(AmountCredit) as AmountCredit from Tranction_Details inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=Tranction_Details.Tranction_Master_Id where Tranction_Details.Whid='" + ds12.Rows[0]["Whid"] + "' and TranctionMaster.Whid='" + ds12.Rows[0]["Whid"] + "' and AccountCredit='" + gdr5["AccountId"] + "' and Date between '" + Convert.ToDateTime(ds12.Rows[0]["StartDate"]) + "' and '" + Convert.ToDateTime(ds12.Rows[0]["EndDate"]) + "'");
                if (dtcredit.Rows.Count > 0)
                {
                    if (dtcredit.Rows[0]["AmountCredit"].ToString() != "")
                    {

                        totcrdit = Convert.ToDecimal(dtcredit.Rows[0]["AmountCredit"].ToString());

                    }
                }
                DataTable dtamtd1 = new DataTable();
                dtamtd1 = (DataTable)select("Select sum(AmountDebit) as AmountDebit from Tranction_Details inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=Tranction_Details.Tranction_Master_Id where Tranction_Details.Whid='" + ds12.Rows[0]["Whid"] + "' and TranctionMaster.Whid='" + ds12.Rows[0]["Whid"] + "' and AccountDebit='" + gdr5["AccountId"] + "' and Date between '" + Convert.ToDateTime(ds12.Rows[0]["StartDate"]) + "' and '" + Convert.ToDateTime(ds12.Rows[0]["EndDate"]) + "' ");
                if (dtamtd1.Rows.Count > 0)
                {
                    if (dtamtd1.Rows[0]["AmountDebit"].ToString() != "")
                    {

                        totdebit = Convert.ToDecimal(dtamtd1.Rows[0]["AmountDebit"].ToString());
                    }
                }
                //if (k > 0)
                //{
                    DataTable dta = new DataTable();
                    dta = (DataTable)select("Select AccountMaster.AccountId,AccountBalance.Balance,AccountBalance.Account_Balance_Id from ReportPeriod inner join AccountBalance on AccountBalance.Report_Period_Id=ReportPeriod.Report_Period_Id inner join AccountMaster on AccountMaster.Id=AccountBalance.AccountMasterId  where AccountId='" + gdr5["AccountId"] + "' and ReportPeriod.Report_Period_Id='" + ds121.Rows[0]["Report_Period_Id"] + "'");
                    if (dta.Rows.Count > 0)
                    {

                        if (dta.Rows[0]["Balance"].ToString() == "")
                        {
                            balance = totdebit - totcrdit;
                        }
                        else
                        {
                            balance = (Convert.ToDecimal(dta.Rows[0]["Balance"]) + totdebit - totcrdit);
                        }
                    }
                //}
                //else
                //{
                //    balance = totdebit - totcrdit;
                //}

                SqlCommand cmdtr1 = new SqlCommand("Update AccountBalance Set Balance='" + balance + "' where Account_Balance_Id='" + gdr5["Account_Balance_Id"] + "' ", con);

                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdtr1.ExecuteNonQuery();
                con.Close();
            }



            DataTable dtamtca = new DataTable();
            dtamtca = (DataTable)select("Select ClassMaster.ClassTypeId, AccountMaster.AccountId,AccountBalance_InStatment.Balance,AccountBalance_InStatment.Account_Balance_Id from ReportPeriod inner join AccountBalance_InStatment on AccountBalance_InStatment.Report_Period_Id=ReportPeriod.Report_Period_Id inner join AccountMaster on AccountMaster.Id=AccountBalance_InStatment.AccountMasterId  inner join  GroupMaster on AccountMaster.GroupId=GroupMaster.GroupId inner join ClassMaster on GroupMaster.ClassId=ClassMaster.ClassId  where  ClassMaster.ClassTypeId  In('15','19') and ReportPeriod.Report_Period_Id='" + ds12.Rows[0]["Report_Period_Id"] + "'");
            string opg = " and InventoryWarehouseMasterAvgCostTbl.DateUpdated Between '" + Convert.ToDateTime(ds12.Rows[0]["StartDate"]).ToShortDateString() + "' and'" + Convert.ToDateTime(ds12.Rows[0]["EndDate"]).ToShortDateString() + "'";

            // fillcost(opg, Convert.ToDateTime(ds12.Rows[0]["EndDate"]).ToShortDateString(), Convert.ToDateTime(ds12.Rows[0]["StartDate"]).ToShortDateString());

            foreach (DataRow gdr5 in dtamtca.Rows)
            {
                Decimal totcrdit = 0;
                Decimal totdebit = 0;
                Decimal balance = 0;
                DataTable dtcredit = new DataTable();
                dtcredit = (DataTable)select("Select sum(AmountCredit) as AmountCredit from Tranction_Details inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=Tranction_Details.Tranction_Master_Id where Tranction_Details.Whid='" + ds12.Rows[0]["Whid"] + "' and TranctionMaster.Whid='" + ds12.Rows[0]["Whid"] + "' and AccountCredit='" + gdr5["AccountId"] + "' and TranctionMaster.Date between '" + Convert.ToDateTime(ds12.Rows[0]["StartDate"]) + "' and '" + Convert.ToDateTime(ds12.Rows[0]["EndDate"]) + "'");
                if (dtcredit.Rows.Count > 0)
                {
                    if (dtcredit.Rows[0]["AmountCredit"].ToString() != "")
                    {

                        totcrdit = Convert.ToDecimal(dtcredit.Rows[0]["AmountCredit"].ToString());

                    }
                }
                DataTable dtamtd1 = new DataTable();
                dtamtd1 = (DataTable)select("Select sum(AmountDebit) as AmountDebit from Tranction_Details inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=Tranction_Details.Tranction_Master_Id where Tranction_Details.Whid='" + ds12.Rows[0]["Whid"] + "' and TranctionMaster.Whid='" + ds12.Rows[0]["Whid"] + "' and AccountDebit='" + gdr5["AccountId"] + "' and TranctionMaster.Date between '" + Convert.ToDateTime(ds12.Rows[0]["StartDate"]) + "' and '" + Convert.ToDateTime(ds12.Rows[0]["EndDate"]) + "' ");
                if (dtamtd1.Rows.Count > 0)
                {
                    if (dtamtd1.Rows[0]["AmountDebit"].ToString() != "")
                    {

                        totdebit = Convert.ToDecimal(dtamtd1.Rows[0]["AmountDebit"].ToString());
                    }
                }

                balance = totdebit - totcrdit;
                ////////////////////////////Cost of good ACCC

                DataTable dtcbal = new DataTable();

                //if (Convert.ToString(gdr5["AccountId"]) == "8003")
                //{
                //    dtcbal = (DataTable)select("Select AccountMaster.AccountId,AccountBalance_InStatment.Balance,AccountBalance_InStatment.Account_Balance_Id from ReportPeriod inner join AccountBalance_InStatment on AccountBalance_InStatment.Report_Period_Id=ReportPeriod.Report_Period_Id inner join AccountMaster on AccountMaster.Id=AccountBalance_InStatment.AccountMasterId  where AccountId='" + gdr5["AccountId"] + "' and ReportPeriod.Report_Period_Id='" + ds121.Rows[0]["Report_Period_Id"] + "'");
                //    if (dtcbal.Rows.Count > 0)
                //    {

                //        if (dtcbal.Rows[0]["Balance"].ToString() != "")
                //        {
                //            balance = (Convert.ToDecimal(dtcbal.Rows[0]["Balance"]) + balance);
                //        }
                //    }
                //}

                bool acd = true;
                if (balance > 0)
                {
                    acd = false;
                }
                else
                {
                    acd = true;
                }
                Totalretailbal += balance;
                SqlCommand cmdtr1 = new SqlCommand("Update AccountBalance_InStatment Set CrBalance='" + totcrdit + "', DrBalance='" + totdebit + "', Cr='" + acd + "', Balance='" + balance + "' where Account_Balance_Id='" + gdr5["Account_Balance_Id"] + "' ", con);

                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdtr1.ExecuteNonQuery();
                con.Close();
            }
            Decimal revdif = 0;

            DataTable dttotac = new DataTable();
            dttotac = (DataTable)select("select distinct AccountId,AccountMaster.Id from ClassMaster inner join GroupMaster on GroupMaster.ClassId=ClassMaster.ClassId inner join  AccountMaster on AccountMaster.GroupId=GroupMaster.GroupId inner join Tranction_Details on " +
            "(Tranction_Details.AccountDebit=AccountMaster.AccountId or Tranction_Details.AccountCredit=AccountMaster.AccountId ) where  Tranction_Details.whid='" + ds12.Rows[0]["Whid"] + "' " +
            "and  AccountMaster.Whid='" + ds12.Rows[0]["Whid"] + "' and   AccountMaster.compid='" + Session["comid"] + "'  and ClassMaster.ClassTypeId In('15','19')");
            if (dttotac.Rows.Count > 0)
            {


                decimal actotcr = 0;
                decimal actotde = 0;


                DataTable dtsdebit = new DataTable();
                dtsdebit = (DataTable)select(" select sum(AmountDebit) as AmountDebit from ClassMaster inner join GroupMaster on GroupMaster.ClassId=ClassMaster.ClassId inner join  AccountMaster on AccountMaster.GroupId=GroupMaster.GroupId inner join Tranction_Details on " +
                "Tranction_Details.AccountDebit=AccountMaster.AccountId inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=Tranction_Details.Tranction_Master_Id  where  Tranction_Details.whid='" + ds12.Rows[0]["Whid"] + "' " +
               " and  AccountMaster.Whid='" + ds12.Rows[0]["Whid"] + "' and   AccountMaster.compid='" + Session["comid"] + "'  and ClassMaster.ClassTypeId In('15','19') and TranctionMaster.Date between '" + Convert.ToDateTime(ds12.Rows[0]["StartDate"]) + "' and '" + Convert.ToDateTime(ds12.Rows[0]["EndDate"]) + "'");

                if (dtsdebit.Rows[0]["AmountDebit"].ToString() != "")
                {

                    actotde = Convert.ToDecimal(dtsdebit.Rows[0]["AmountDebit"].ToString());

                }

                DataTable dtscredit = new DataTable();
                dtscredit = (DataTable)select(" select sum(AmountCredit) as AmountCredit from ClassMaster inner join GroupMaster on GroupMaster.ClassId=ClassMaster.ClassId inner join  AccountMaster on AccountMaster.GroupId=GroupMaster.GroupId inner join Tranction_Details on " +
                "Tranction_Details.AccountCredit=AccountMaster.AccountId inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=Tranction_Details.Tranction_Master_Id  where  Tranction_Details.whid='" + ds12.Rows[0]["Whid"] + "' " +
               " and  AccountMaster.Whid='" + ds12.Rows[0]["Whid"] + "' and   AccountMaster.compid='" + Session["comid"] + "'  and ClassMaster.ClassTypeId In('15','19') and  TranctionMaster.Date between '" + Convert.ToDateTime(ds12.Rows[0]["StartDate"]) + "' and '" + Convert.ToDateTime(ds12.Rows[0]["EndDate"]) + "'");


                if (dtscredit.Rows[0]["AmountCredit"].ToString() != "")
                {

                    actotcr = Convert.ToDecimal(dtscredit.Rows[0]["AmountCredit"]);
                }




                DataTable dtbal = new DataTable();
                dtbal = (DataTable)select("Select AccountBalance.Balance,Account_Balance_Id from AccountBalance  inner join AccountMaster on AccountMaster.Id=AccountBalance.AccountMasterId where AccountId='4700' and Report_Period_Id='" + ds12.Rows[0]["Report_Period_Id"] + "'  and Whid='" + ds12.Rows[0]["Whid"] + "'");
                if (dtbal.Rows.Count > 0)
                {
                    revdif = Convert.ToDecimal(dtbal.Rows[0]["Balance"]) - actotcr + actotde;
                    SqlCommand csd = new SqlCommand("Update AccountBalance Set Balance='" + Math.Round(revdif, 2) + "' where Account_Balance_Id='" + dtbal.Rows[0]["Account_Balance_Id"] + "' ", con);

                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                 int cd=Convert.ToInt32(csd.ExecuteNonQuery());
                    con.Close();
                }
            }
        }
        SqlCommand cmdtr = new SqlCommand("Update ReportPeriod set Active='0'  where  Report_Period_Id='" + ds12.Rows[0]["Report_Period_Id"] + "' ", con);
        ViewState["rpt"] = ds12.Rows[0]["Report_Period_Id"].ToString();
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmdtr.ExecuteNonQuery();
        con.Close();
         Decimal totamt = 0;
        Decimal cramr = 0;
        Decimal dramt = 0;

       

        DataTable dtg = new DataTable();
        dtg = (DataTable)select("Select sum(AmountCredit) as AmountCredit from Tranction_Details inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=Tranction_Details.Tranction_Master_Id where Tranction_Details.Whid='" + ddlbusiness.SelectedValue + "' and TranctionMaster.Whid='" + ddlbusiness.SelectedValue + "' and AccountCredit='4700' and TranctionMaster.Date between '" + Convert.ToDateTime(ds12.Rows[0]["StartDate"]).ToShortDateString() + "' and'" + Convert.ToDateTime(ds12.Rows[0]["EndDate"]).ToShortDateString() + "'");
        if (dtg.Rows.Count > 0)
        {
            if (dtg.Rows[0]["AmountCredit"].ToString() != "")
            {

                cramr = Convert.ToDecimal(dtg.Rows[0]["AmountCredit"].ToString());

            }
        }
        DataTable dtdc = new DataTable();
        dtdc = (DataTable)select("Select sum(AmountDebit) as AmountDebit from Tranction_Details inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=Tranction_Details.Tranction_Master_Id where Tranction_Details.Whid='" + ddlbusiness.SelectedValue + "' and TranctionMaster.Whid='" + ddlbusiness.SelectedValue + "' and AccountDebit='4700' and TranctionMaster.Date between '" + Convert.ToDateTime(ds12.Rows[0]["StartDate"]).ToShortDateString() + "' and'" + Convert.ToDateTime(ds12.Rows[0]["EndDate"]).ToShortDateString() + "'");
        if (dtdc.Rows.Count > 0)
        {
            if (dtdc.Rows[0]["AmountDebit"].ToString() != "")
            {

                dramt = Convert.ToDecimal(dtdc.Rows[0]["AmountDebit"].ToString());
            }
        }
        Decimal retaopp = 0;
        DataTable dtret = (DataTable)select("Select AccountMaster.AccountId,AccountBalance.Balance,AccountBalance.Account_Balance_Id from ReportPeriod inner join AccountBalance on AccountBalance.Report_Period_Id=ReportPeriod.Report_Period_Id inner join AccountMaster on AccountMaster.Id=AccountBalance.AccountMasterId  where AccountId='4700' and ReportPeriod.Report_Period_Id='" + ds121.Rows[0]["Report_Period_Id"] + "'");
        if (dtret.Rows.Count > 0)
        {
            if (Convert.ToString(dtret.Rows[0]["Balance"]) != "")
            {
                retaopp = Convert.ToDecimal(dtret.Rows[0]["Balance"]);
            }
        }
        //if (retaopp != 0)
        //{
        //    retaopp = (-1) * retaopp;
        //}
        totamt = totamt + retaopp+(dramt - cramr);
        // Decimal finalretamt = Totalretailbal + invopst - totamt;
        Decimal finalretamt =totamt+ Totalretailbal ;
       
        SqlCommand CVD = new SqlCommand(" Update AccountBalance set Balance='" + Math.Round(finalretamt, 2) + "' where Account_Balance_Id In(select Account_Balance_Id from AccountBalance  inner join AccountMaster on AccountMaster.Id=AccountBalance.AccountMasterId where AccountId='4700' and Report_Period_Id='" + ViewState["rpt"] + "'  and Whid='" + ddlbusiness.SelectedValue + "')", con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        CVD.ExecuteNonQuery();
        con.Close();
    }
   
    protected void addtxformula(string emp, string year)
    {
        string str = "Insert Into TaxFormulaVariableTbl(EmployeeID,TaxYearId,A,A1,B,B1,C,D,D1,E,E1,F,F1,F2,F3,F4,G,HD,I,I1,IE,K,KP,K1,K1P,K2,K2P,K2Q,K3,K3P,K4,K4P,L,LCF,LCP,M,M1,P,P1,PR,R,S,S1,T,T1,T2,T3,T4,TB,TC,TCP,U1,V,V1,V2,Y,YTD,T)values('" + emp + "','" + year + "','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0')";
        SqlCommand cmd = new SqlCommand(str, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd.ExecuteNonQuery();
        con.Close();
    }
    protected void btnaddnewaccount_Click(object sender, EventArgs e)
    {
        if (ddlbusiness.SelectedIndex > -1)
        {
            string str1 = "select Name,Report_Period_Id,StartDate,EndDate from ReportPeriod where Compid = '" + Session["comid"] + "' and Whid='" + ddlbusiness.SelectedValue + "' order by EndDate Desc";
            SqlCommand cmd1 = new SqlCommand(str1);
            cmd1.Connection = con;
            SqlDataAdapter da1 = new SqlDataAdapter();
            da1.SelectCommand = cmd1;
            DataTable ds1 = new DataTable();
            da1.Fill(ds1);

            if (ds1.Rows.Count >= 1)
            {

                string[] separator1 = new string[] { " TO " };
                string[] strSplitArr1 = ds1.Rows[0]["Name"].ToString().Split(separator1, StringSplitOptions.RemoveEmptyEntries);
                int i111 = Convert.ToInt32(strSplitArr1.Length);
                if (i111 == 2 ||i111 == 1)
                {
                    int fr = Convert.ToInt32(strSplitArr1[0].ToString());
                     int sec =0;
                     if (i111 >1)
                     {
                         sec = Convert.ToInt32(strSplitArr1[1].ToString());
                     }
                    string strdate1 = "";
                    string lastdate = "";
                    string strdate2 = "";
                    string lasttwodata = "";

                    int ddt1 = fr + 1;
                    int dde1 = sec + 1;

                    strdate1 = ddt1.ToString();
                    lastdate = strdate1.Substring(2);
                    string peram = "";
                    if (i111 == 2)
                    {
                       
                        strdate2 = dde1.ToString();
                        lasttwodata = strdate2.Substring(2);
                        peram = ddt1 + " TO " + dde1;
                    }
                    else
                    {
                        peram = ddt1.ToString();
                    }

                    string addsdate = Convert.ToDateTime(ds1.Rows[0]["StartDate"]).AddMonths(12).ToShortDateString();
                    string addedate = Convert.ToDateTime(ds1.Rows[0]["EndDate"]).AddMonths(12).ToShortDateString();
                    SqlCommand cmdtr = new SqlCommand("Insert into ReportPeriod(Name,StartDate,EndDate,FistStartDate,Active,Compid,Whid)Values('" + peram + "','" + addsdate + "','" + addedate + "','" + DateTime.Now.ToShortDateString() + "','0','" + Session["comid"] + "','" + ddlbusiness.SelectedValue + "') ", con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdtr.ExecuteNonQuery();
                    con.Close();
                    SqlCommand cmsa = new SqlCommand("SELECT max(Report_Period_Id) from ReportPeriod where Compid='" + Session["comid"] + "' and Whid='" + ddlbusiness.SelectedValue + "'", con);
                    con.Open();
                    object xde = cmsa.ExecuteScalar();
                    con.Close();
                    string str5t = "select Id from AccountMaster where Whid='" + ddlbusiness.SelectedValue + "'";
                    SqlCommand cmd32t = new SqlCommand(str5t, con);
                    SqlDataAdapter adp15t = new SqlDataAdapter(cmd32t);
                    DataTable dtlogin14t = new DataTable();
                    adp15t.Fill(dtlogin14t);
                    foreach (DataRow dr in dtlogin14t.Rows)
                    {
                        SqlCommand cmdtr1 = new SqlCommand("Insert into AccountBalance(AccountMasterId,Balance,Report_Period_Id)Values('" + dr["Id"] + "','0','" + xde + "') ", con);

                        con.Open();
                        cmdtr1.ExecuteNonQuery();
                        con.Close();
                    }

                    lblmsg.Text = "New accounting year created successfully";
                    lblmsg.Visible = true;


                    DocumentCls1 clsDocument = new DocumentCls1();

                    //  Int32 mainyearcabinet = clsDocument.InsertDocumentMainType("Accounting", ddlbusiness.SelectedValue);

                    string maincabinetmax = "select *  from DocumentMainType where CID='" + Session["comid"].ToString() + "' and Whid='" + ddlbusiness.SelectedValue + "' and DocumentMainType='Accounting' ";
                    SqlCommand cmdmaincabinetmax = new SqlCommand(maincabinetmax, con);
                    SqlDataAdapter adpmaincabinetmax = new SqlDataAdapter(cmdmaincabinetmax);
                    DataTable dtmaincabinetmax = new DataTable();
                    adpmaincabinetmax.Fill(dtmaincabinetmax);

                    if (dtmaincabinetmax.Rows.Count > 0)
                    {
                        Int32 mainyeardrawer = clsDocument.InsertDocumentSubType(Convert.ToInt32(dtmaincabinetmax.Rows[0]["DocumentMainTypeId"]), "" + peram + "");

                        string maindrawermax = "select Max(DocumentSubTypeId) as DocumentSubTypeId  from DocumentSubType ";
                        SqlCommand just = new SqlCommand(maindrawermax, con);
                        SqlDataAdapter adpmaindrawermax = new SqlDataAdapter(just);
                        DataTable dtmaindrawermax = new DataTable();
                        adpmaindrawermax.Fill(dtmaindrawermax);

                        if (dtmaindrawermax.Rows.Count > 0)
                        {string perment = "";
                            if (i111 == 2)
                            {
                                perment = lastdate + "-" + lasttwodata;
                            }
                            else
                            {
                                perment = peram;
                            }
                            Int32 mainyearfolder = clsDocument.InsertDocumentType1(Convert.ToInt32(dtmaindrawermax.Rows[0]["DocumentSubTypeId"]), "Purchase Invoice" + " " + perment);
                            Int32 mainyearfolder1 = clsDocument.InsertDocumentType1(Convert.ToInt32(dtmaindrawermax.Rows[0]["DocumentSubTypeId"]), "Cash Payments" + " " + perment);
                            Int32 mainyearfolder2 = clsDocument.InsertDocumentType1(Convert.ToInt32(dtmaindrawermax.Rows[0]["DocumentSubTypeId"]), "Cash Receipts" + " " + perment);
                            Int32 mainyearfolder3 = clsDocument.InsertDocumentType1(Convert.ToInt32(dtmaindrawermax.Rows[0]["DocumentSubTypeId"]), "Journal Entry" + " " + perment);
                            Int32 mainyearfolder4 = clsDocument.InsertDocumentType1(Convert.ToInt32(dtmaindrawermax.Rows[0]["DocumentSubTypeId"]), "Sales Invoice" + " " + perment);
                            Int32 mainyearfolder5 = clsDocument.InsertDocumentType1(Convert.ToInt32(dtmaindrawermax.Rows[0]["DocumentSubTypeId"]), "Retail Sales Invoice" + " " + perment);
                            Int32 mainyearfolder6 = clsDocument.InsertDocumentType1(Convert.ToInt32(dtmaindrawermax.Rows[0]["DocumentSubTypeId"]), "Debit-Credit Note" + " " + perment);
                            Int32 mainyearfolder7 = clsDocument.InsertDocumentType1(Convert.ToInt32(dtmaindrawermax.Rows[0]["DocumentSubTypeId"]), "Quick Cash Entry" + " " + perment);
                            Int32 mainyearfolder8 = clsDocument.InsertDocumentType1(Convert.ToInt32(dtmaindrawermax.Rows[0]["DocumentSubTypeId"]), "Paypal Cash/Bank Receipts" + " " + perment);
                            Int32 mainyearfolder9 = clsDocument.InsertDocumentType1(Convert.ToInt32(dtmaindrawermax.Rows[0]["DocumentSubTypeId"]), "Paypal Cash/Bank Payments" + " " + perment);
                            Int32 mainyearfolder10 = clsDocument.InsertDocumentType1(Convert.ToInt32(dtmaindrawermax.Rows[0]["DocumentSubTypeId"]), "Cash Statement" + " " + perment);
                            Int32 mainyearfolder11 = clsDocument.InsertDocumentType1(Convert.ToInt32(dtmaindrawermax.Rows[0]["DocumentSubTypeId"]), "Paypal Statement" + " " + perment);

                        }

                    }



                    fillgrid();
                }
            }

        }
    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");

            Button1.Text = "Hide Printable Version";
            Button7.Visible = true;
            lblmsg.Text = "";
            grd.Enabled = false;

            //foreach (GridViewRow gdr in grd.Rows)
            //{
            //    CheckBox chkactive = (CheckBox)(gdr.FindControl("chkactive"));
            //    chkactive.Enabled = false;


            //}

        }
        else
        {
            //foreach (GridViewRow gdr in grd.Rows)
            //{
            //    CheckBox chkactive = (CheckBox)(gdr.FindControl("chkactive"));
            //    chkactive.Enabled = true;


            //}
            lblmsg.Text = "";
            grd.Enabled = true;
            Button1.Text = "Printable Version";
            Button7.Visible = false;

        }
    }
    protected void btns_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        if (ddlbusiness.SelectedIndex > -1)
        {
            btnaddnewaccount0.Visible = true;
        }
        else
        {
            btnaddnewaccount0.Visible = false;
        }
        fillgrid();
        ModalPopupExtender122.Hide();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        activelastacc();
        fillgrid();
        ModalPopupExtender1.Hide();
    }
}
