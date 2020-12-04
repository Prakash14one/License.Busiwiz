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


public partial class ShoppingCart_Admin_Fixedtaxdependingonstate : System.Web.UI.Page
{
    //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString"].ConnectionString);
    SqlConnection con=new SqlConnection(PageConn.connnn);
    string compid;
    DBCommands1 dbss1 = new DBCommands1();
    Boolean only;
    Boolean allapp;
    public int puraccid = 0;
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

        compid = Session["Comid"].ToString();
        Page.Title = pg.getPageTitle(page);
        Label1.Visible = false;
        ViewState["Wid"] = Request.QueryString["wid"].ToString();
        if (!Page.IsPostBack)
        {
            string warename = "select Name from WarehouseMaster where WarehouseId='" + ViewState["Wid"] + "'";
            SqlDataAdapter adp = new SqlDataAdapter(warename, con);
            DataTable dtp = new DataTable();
            adp.Fill(dtp);
            if (dtp.Rows.Count > 0)
            {
                storename.Text = dtp.Rows[0]["Name"].ToString();
            }
            ViewState["sortOrder"] = "";
            fillcountry();
            //FillTax();

            fillgrid();

            //chkCharge_CheckedChanged(sender, e);
            lblCompany.Text = Session["Cname"].ToString();


        }
    }



    protected bool isboolornot(string ck)
    {

        try
        {
            Convert.ToBoolean(ck);
            return true;
        }
        catch
        {
            return false;
        }
    }
    protected void fillcountry()
    {
        string strfillgrid = "SELECT  distinct CountryId, CountryName FROM CountryMaster order by CountryName";
        dbss1.FillDDL1(ddlselectcountry, strfillgrid, "CountryId", "CountryName");
        ddlselectcountry.Items.Insert(0, "ALL");
        ddlselectcountry.SelectedItem.Value = "0";

        dbss1.FillDDL1(ddlfiltercountary, strfillgrid, "CountryId", "CountryName");
        ddlfiltercountary.Items.Insert(0, "ALL");
        ddlfiltercountary.SelectedItem.Value = "0";

        object s = new object();
        EventArgs gh = new EventArgs();

        ddlselectcountry_SelectedIndexChanged(s, gh);
        fillfistate();
    }

    protected void fillgrid()
    {
        string fill = "";
        lblBusiness.Text = storename.Text;
        if (ddlfiltercountary.SelectedIndex > 0)
        {
            fill = " and TaxTypeMasterMoreInfo.CountryId='" + ddlfiltercountary.SelectedValue + "'";
        }
        if (ddlfilterstate.SelectedIndex > 0)
        {
            fill += " and TaxTypeMasterMoreInfo.StateId='" + ddlfilterstate.SelectedValue + "'";
        }
        string str = " SELECT Distinct Taxshortname, TaxTypeMasterMoreInfo.Active, TaxTypeMasterMoreInfo.Id,TaxTypeMasterMoreInfo.TaxMAccountMasterID,TaxTypeMasterMoreInfo.StateId,TaxTypeMasterMoreInfo.CountryId,  TaxTypeMasterMoreInfo.TaxName, TaxTypeMasterMoreInfo.ApplyToallOnlineSales, TaxTypeMasterMoreInfo.ApplyToAllSales, TaxTypeMasterMoreInfo.TaxAmt, TaxTypeMasterMoreInfo.TaxAmtInPerc,   case when   StateMasterTbl.StateName IS null then 'ALL' ELSE StateMasterTbl.StateName END AS StateName  , case when   CountryMaster.CountryName is null then 'ALL' else  CountryMaster.CountryName end as   CountryName, TaxTypeMasterMoreInfo.TaxAmt,Case when(Active=1)then 'Active' else 'Inactive' end as Status   FROM  StateMasterTbl RIGHT OUTER JOIN   TaxTypeMasterMoreInfo ON StateMasterTbl.StateId = TaxTypeMasterMoreInfo.StateId LEFT OUTER JOIN   CountryMaster ON TaxTypeMasterMoreInfo.CountryId = CountryMaster.CountryId where TaxTypeMasterMoreInfo.Whid='" + ViewState["Wid"] + "'" + fill;

        DataTable dtg = dbss1.cmdSelect(str + " Order by CountryName,StateName,TaxName,Taxshortname");
        GridView1.DataSource = dtg;

        DataView myDataView = new DataView();
        myDataView = dtg.DefaultView;
        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }
        GridView1.DataBind();
        foreach (GridViewRow item in GridView1.Rows)
        {
            CheckBox CheckBox2 = (CheckBox)item.FindControl("CheckBox2");
            Label lblsingamt = (Label)item.FindControl("lblsingamt");
            Label lblsignpers = (Label)item.FindControl("lblsignpers");
            if (CheckBox2.Checked == true)
            {
                lblsingamt.Visible = false;
                lblsignpers.Visible = true;
            }
            else
            {
                lblsingamt.Visible = true;
                lblsignpers.Visible = false;
            }

        }

    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Sort")
        {
            return;
        }

        if (e.CommandName == "Edit")
        {


        }

        else if (e.CommandName == "Delete")
        {
            // GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            // ViewState["id"] = GridView1.SelectedDataKey.Value;
        }
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //fillgrid();
        //FillTax();
        // ModalPopupExtender1222.Show();

        GridView1.SelectedIndex = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
        ViewState["id"] = GridView1.SelectedIndex;

        yes_Click(sender, e);
    }
    protected void edit_Click(object sender, EventArgs e)
    {
        ImageButton9.Visible = true;
        Button3.Visible = false;

        ImageButton lnk = (ImageButton)sender;
        int i = Convert.ToInt32(lnk.CommandArgument);
        Session["ttid"] = i;
        String selectStr = "select * from TaxTypeMasterMoreInfo where Id='" + i + "' ";
        SqlDataAdapter ad = new SqlDataAdapter(selectStr, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        DataTable dt = new DataTable();
        ad.Fill(dt);
        Session["Acid"] = dt.Rows[0]["TaxMAccountMasterID"].ToString();

        tbTaxName.Text = dt.Rows[0]["TaxName"].ToString();
        txtAmt.Text = dt.Rows[0]["TaxAmt"].ToString();
        txtshortname.Text = Convert.ToString(dt.Rows[0]["Taxshortname"]);
        bool per = Convert.ToBoolean(dt.Rows[0]["TaxAmtInPerc"].ToString());
        if (per == false)
        {
            rdlist.SelectedIndex = 1;


        }
        else
        {
            rdlist.SelectedIndex = 0;

        }

        string onsales = dt.Rows[0]["ApplyToallOnlineSales"].ToString();
        if (onsales == "True")
        {
            ddlapply.SelectedIndex = 0;
            // RadioButtonList4.SelectedIndex = 0;

        }
        else
        {
            ddlapply.SelectedIndex = 1;
            // RadioButtonList4.SelectedIndex= 1;
        }
        string act = dt.Rows[0]["Active"].ToString();

        if (act == "True")
        {
            ddlstatus.SelectedIndex = 0;
            // chkactive.Checked = true;
        }
        else
        {
            ddlstatus.SelectedIndex = 1;
            //chkactive.Checked = false;
        }
        ddlselectcountry.SelectedIndex = ddlselectcountry.Items.IndexOf(ddlselectcountry.Items.FindByValue(dt.Rows[0]["CountryId"].ToString()));
        ddlselectcountry_SelectedIndexChanged(sender, e);
        ddlstate.SelectedIndex = ddlstate.Items.IndexOf(ddlstate.Items.FindByValue(dt.Rows[0]["StateId"].ToString()));

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {






    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    protected double isdoubleornot(string ck)
    {
        double i = 0;
        try
        {
            i = Convert.ToInt32(ck);
            return i;

        }
        catch
        {
            return i;
        }

    }


    protected void Button2_Click(object sender, EventArgs e)
    {
        ImageButton9.Visible = false;
        Button3.Visible = true;
        Label1.Text = "";

        clearnch();
    }
    protected void clearnch()
    {
        ddlselectcountry.SelectedIndex = 0;
        object sen = new object();
        EventArgs ee = new EventArgs();
        ddlselectcountry_SelectedIndexChanged(sen, ee);
        txtAmt.Text = "";
        tbTaxName.Text = "";
        txtshortname.Text = "";
        ddlstatus.SelectedIndex = 0;
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;

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




    protected void yes_Click(object sender, EventArgs e)
    {
        string chkid = "select * from SalesOrderTempTax where TaxTypeMasterMoreInfoID=" + ViewState["id"] + "";
        DataTable dtchkid = dbss1.cmdSelect(chkid);
        if (dtchkid.Rows.Count > 0)
        {
            Label1.Visible = true;
            Label1.Text = "You can not delete this Tax entry as some order /invoice exist for this tax entry. However you can make this entry inactive to stop using this tax entry for your sales or edit the entry for change in name or tax rates etc.";
        }
        else
        {



            string saa = "select * from TaxTypeMasterMoreInfo where  Id=" + ViewState["id"] + "";
            DataTable dtty = dbss1.cmdSelect(saa);
            if (dtty.Rows.Count > 0)
            {
                string gh = "delete from TaxTypeMasterMoreInfo where Id=" + ViewState["id"] + "";
                SqlCommand cmvv = new SqlCommand(gh, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmvv.ExecuteNonQuery();
                con.Close();

                string str1113 = "select Id  from AccountMaster where Whid='" + ViewState["Wid"] + "' and AccountId='" + dtty.Rows[0]["PurchaseTaxAccountMasterID"] + "'";
                SqlCommand cmd1113 = new SqlCommand(str1113, con);
                SqlDataAdapter adp1113 = new SqlDataAdapter(cmd1113);
                DataTable ds1113 = new DataTable();
                adp1113.Fill(ds1113);
                if (ds1113.Rows.Count > 0)
                {
                    string strpqq = "delete  from AccountBalance where AccountMasterId='" + ds1113.Rows[0]["Id"] + "'";
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    SqlCommand dcmpqer = new SqlCommand(strpqq, con);
                    dcmpqer.ExecuteNonQuery();
                    con.Close();
                }

                string str1w = "select Id  from AccountMaster where Whid='" + ViewState["Wid"] + "' and AccountId='" + dtty.Rows[0]["TaxMAccountMasterID"] + "'";

                SqlDataAdapter adpzx = new SqlDataAdapter(str1w, con);
                DataTable dfg = new DataTable();
                adpzx.Fill(dfg);
                if (dfg.Rows.Count > 0)
                {
                    string strpqq = "delete  from AccountBalance where AccountMasterId='" + dfg.Rows[0]["Id"] + "'";
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    SqlCommand dcmpqer = new SqlCommand(strpqq, con);
                    dcmpqer.ExecuteNonQuery();
                    con.Close();
                }

                string strperd = "delete  from AccountMaster where Whid='" + dtty.Rows[0]["Whid"] + "' and AccountId='" + dtty.Rows[0]["PurchaseTaxAccountMasterID"] + "'";
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                SqlCommand dcmper = new SqlCommand(strperd, con);
                dcmper.ExecuteNonQuery();
                con.Close();
                string strperds = "delete  from AccountMaster where Whid='" + dtty.Rows[0]["Whid"] + "' and AccountId='" + dtty.Rows[0]["TaxMAccountMasterID"] + "'";
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                SqlCommand dcmpers = new SqlCommand(strperds, con);
                dcmpers.ExecuteNonQuery();
                con.Close();
                Label1.Visible = true;
                Label1.Text = "Record deleted successfully";
            }

            fillgrid();


            // ModalPopupExtender1222.Hide();
        }
    }

    protected void ImageButton6_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1222.Hide();
    }

    protected void ddlSearchByTax_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void ddlselectcountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlselectcountry.SelectedIndex > 0)
        {
            ddlstate.Items.Clear();
            string str45 = "SELECT  distinct StateId, StateName  " +
                    " FROM  StateMasterTbl where CountryId='" + ddlselectcountry.SelectedValue + "' " +
                    " Order By StateName";

            dbss1.FillDDL1(ddlstate, str45, "StateId", "StateName");
            ddlstate.Items.Insert(0, "ALL");
            ddlstate.SelectedItem.Value = "0";


        }
        else
        {//country have to be 0
            ddlstate.Items.Clear();
            ddlstate.Items.Insert(0, "ALL");
            ddlstate.SelectedItem.Value = "0";
        }
    }
    protected DataTable select(string qu)
    {
        SqlCommand cmd = new SqlCommand(qu, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        return dt;

    }
    protected string acccc(string accgenid)
    {

        DataTable dtrs = select("select AccountId from AccountMaster where AccountId='" + accgenid + "' and Whid='" + ViewState["Wid"] + "'");
        if (dtrs.Rows.Count > 0)
        {
            accgenid = Convert.ToString(Convert.ToInt32(accgenid) + 1);
            acccc(accgenid);
        }
        return accgenid;
    }
    protected void imgbtnAdd_Click(object sender, EventArgs e)
    {
        if (ddlapply.SelectedValue == "0")
        {
            only = true;
            allapp = false;
        }
        else if (ddlapply.SelectedValue == "1")
        {
            only = false;
            allapp = true;
        }
        int accountid;

        string st153 = "select Report_Period_Id  from ReportPeriod where Compid='" + compid + "' and Whid='" + ViewState["Wid"].ToString() + "' and Active='1'";
        SqlCommand cmd153 = new SqlCommand(st153, con);
        SqlDataAdapter adp153 = new SqlDataAdapter(cmd153);
        DataTable ds153 = new DataTable();
        adp153.Fill(ds153);
        Session["reportid"] = ds153.Rows[0]["Report_Period_Id"].ToString();

        string accper = "  select max(AccountId) as AccountId from AccountMaster where Whid='" + ViewState["Wid"] + "' and GroupId=5";
        SqlDataAdapter aqla = new SqlDataAdapter(accper, con);

        DataTable asper = new DataTable();
        aqla.Fill(asper);
        if (Convert.ToString(asper.Rows[0]["AccountId"]) != "")
        {
            puraccid = Convert.ToInt32(asper.Rows[0]["AccountId"]) + 1;
            puraccid = Convert.ToInt32(acccc(puraccid.ToString()));
        }
        else
        {
            if (puraccid >= 1999)
            {
                if (puraccid > 17000)
                {
                    puraccid += 1;
                    puraccid = Convert.ToInt32(acccc(puraccid.ToString()));
                }
                else
                {
                    puraccid = 17000;
                    puraccid = Convert.ToInt32(acccc(puraccid.ToString()));
                }
            }
            else
            {
                if (puraccid == 0)
                {
                    puraccid = 1700;
                    puraccid = Convert.ToInt32(acccc(puraccid.ToString()));
                }
                else
                {
                    puraccid = puraccid + 1;
                    puraccid = Convert.ToInt32(acccc(puraccid.ToString()));
                }
            }
        }
        string peracname = "" + tbTaxName.Text + " Purchases";

        string chekrcd14 = "SELECT     TaxName FROM  TaxTypeMasterMoreInfo WHERE TaxName='" + tbTaxName.Text + "' and Whid='" + ViewState["Wid"] + "'";

        DataTable dtchekrcd11 = dbss1.cmdSelect(chekrcd14);
        if (dtchekrcd11.Rows.Count == 0)
        {

            string strcln = " SELECT   TaxTypeMasterMoreInfo.Id,TaxTypeMasterMoreInfo.CountryId, TaxTypeMasterMoreInfo.StateId, TaxTypeMasterMoreInfo.ApplyToallOnlineSales, TaxTypeMasterMoreInfo.ApplyToAllSales, TaxTypeMasterMoreInfo.TaxAmt, TaxTypeMasterMoreInfo.TaxAmtInPerc,TaxTypeMasterMoreInfo.TaxName " +
                " FROM         TaxTypeMasterMoreInfo where (CountryId='" + ddlselectcountry.SelectedValue + "' or CountryId='0') and  (StateId='" + ddlstate.SelectedValue + "' or stateId='0') and Whid='" + ViewState["Wid"] + "'";
            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);
            if (dtcln.Rows.Count >= 3)
            {
                Label1.Visible = true;
                Label1.Text = "You can only have three active taxes for that particular country/state";


            }
            else
            {
                int flag = 0;

                if (rdlist.SelectedIndex == 0)
                {
                    if (Convert.ToDecimal(txtAmt.Text) > Convert.ToDecimal(100))
                    {
                        flag = 1;
                    }
                }

                if (flag == 0)
                {
                    ///////////////////////////////////////////////////////////////////////////////

                    string acid = "  select max(AccountId) as AccountId from AccountMaster where Whid='" + ViewState["Wid"] + "' and GroupId=17";
                    SqlDataAdapter aacid = new SqlDataAdapter(acid, con);

                    DataTable adtc = new DataTable();
                    aacid.Fill(adtc);
                    if (adtc.Rows.Count > 0)
                    {
                        accountid = (Convert.ToInt32(adtc.Rows[0]["AccountId"].ToString()) + 1);
                        accountid = Convert.ToInt32(acccc(accountid.ToString()));
                        string accname = "" + tbTaxName.Text + " payable";
                        string str22 = "Insert into AccountMaster(ClassId,GroupId,AccountId,AccountName,Description,Balance,Balanceoflastyear,DateTimeOfLastUpdatedBalance,Status,compid,Whid)" +
                          "values('5','17','" + accountid + "','" + accname + "','" + accname + "','0','0','" + Convert.ToDateTime(DateTime.Now.ToString()) + "','1','" + compid + "','" + ViewState["Wid"] + "')";

                        SqlCommand cmd = new SqlCommand(str22, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmd.ExecuteNonQuery();
                        con.Close();

                    }
                    else
                    {
                        accountid = 3000;
                        accountid = Convert.ToInt32(acccc(accountid.ToString()));
                        string accname = "'" + tbTaxName.Text + "' payable";
                        string str22 = "Insert into AccountMaster(ClassId,GroupId,AccountId,AccountName,Description,Balance,Balanceoflastyear,DateTimeOfLastUpdatedBalance,Status,compid,Whid)" +
                          "values('5','17','" + accountid + "','" + accname + "','" + accname + "','0','0','" + Convert.ToDateTime(DateTime.Now.ToString()) + "','1','" + compid + "','" + ViewState["Wid"] + "')";

                        SqlCommand cmd = new SqlCommand(str22, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmd.ExecuteNonQuery();
                        con.Close();


                    }
                    string str1113 = "select max(Id) as Aid from AccountMaster where Whid='" + ViewState["Wid"] + "'";
                    SqlCommand cmd1113 = new SqlCommand(str1113, con);
                    SqlDataAdapter adp1113 = new SqlDataAdapter(cmd1113);
                    DataTable ds1113 = new DataTable();
                    adp1113.Fill(ds1113);
                    Session["maxaid"] = ds1113.Rows[0]["Aid"].ToString();

                    string str456 = "insert into AccountBalance(AccountMasterId,Balance,Report_Period_Id) values ('" + Session["maxaid"].ToString() + "','" + 0 + "','" + Session["reportid"].ToString() + "')";
                    SqlCommand cmd456 = new SqlCommand(str456, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd456.ExecuteNonQuery();
                    con.Close();
                    string stper = "Insert into AccountMaster(ClassId,GroupId,AccountId,AccountName,Description,Balance,Balanceoflastyear,DateTimeOfLastUpdatedBalance,Status,compid,Whid)" +
                "values('1','5','" + puraccid + "','" + peracname + "','" + peracname + "','0','0','" + Convert.ToDateTime(DateTime.Now.ToString()) + "','1','" + compid + "','" + ViewState["Wid"] + "')";

                    SqlCommand cmdper = new SqlCommand(stper, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdper.ExecuteNonQuery();
                    con.Close();

                    string strz = "select max(Id) as Aid from AccountMaster where Whid='" + ViewState["Wid"] + "'";
                    SqlCommand cmd1z = new SqlCommand(strz, con);
                    SqlDataAdapter adpz = new SqlDataAdapter(cmd1z);
                    DataTable dsz = new DataTable();
                    adpz.Fill(dsz);
                    Session["maxaid"] = dsz.Rows[0]["Aid"].ToString();

                    string str4z = "insert into AccountBalance(AccountMasterId,Balance,Report_Period_Id) values ('" + Session["maxaid"].ToString() + "','" + 0 + "','" + Session["reportid"].ToString() + "')";
                    SqlCommand cmd456z = new SqlCommand(str4z, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd456z.ExecuteNonQuery();
                    con.Close();
                    /////////////////////////////////////////////////////////////////////////////////
                    string str = "Insert into TaxTypeMasterMoreInfo values('" + tbTaxName.Text + "', " +
                   " '" + ddlselectcountry.SelectedValue + "', " +
                   " '" + ddlstate.SelectedValue + "','" + only + "', " +
                   " '" + allapp + "','" + txtAmt.Text + "','" + rdlist.SelectedValue + "','" + ddlstatus.SelectedValue + "','" + ViewState["Wid"] + "','" + accountid + "','" + puraccid + "','" + txtshortname.Text + "') ";
                    SqlCommand cmvv = new SqlCommand(str, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmvv.ExecuteNonQuery();
                    con.Close();

                    Label1.Visible = true;
                    Label1.Text = "Record inserted successfully";
                    clearnch();
                    fillgrid();



                }
                else
                {
                    Label1.Visible = true;
                    Label1.Text = "You cannot input a tax percentage greater than 100";
                }

            }


        }
        else
        {
            Label1.Visible = true;
            Label1.Text = "Record already exists";
        }

       


    }









    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["wz"] != null)
        {
            Response.Redirect("StoreTaxmethodtbl.aspx");
        }
        else
        {
            Response.Redirect("wzStoreTaxmethodtbl.aspx");
        }
    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (Button4.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");

            Button4.Text = "Hide Printable Version";
            Button1.Visible = true;
            if (GridView1.Columns[9].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[9].Visible = false;
            }
            if (GridView1.Columns[10].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                GridView1.Columns[10].Visible = false;
            }
        }
        else
        {

            //pnlgrid.ScrollBars = ScrollBars.Vertical;
            //pnlgrid.Height = new Unit(300);

            Button4.Text = "Printable Version";
            Button1.Visible = false;
            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[9].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                GridView1.Columns[10].Visible = true;
            }
        }
    }

    protected void btnback_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["wz"] != null)
        {
            Response.Redirect("StoreTaxmethodtbl.aspx");
        }
        else
        {
            Response.Redirect("wzStoreTaxmethodtbl.aspx");
        }
    }
    protected void ImageButton9_Click(object sender, EventArgs e)
    {
        if (ddlapply.SelectedValue == "0")
        {
            only = true;
            allapp = false;
        }
        else if (ddlapply.SelectedValue == "1")
        {
            only = false;
            allapp = true;
        }
        int f = 0;
        string chekrcd14 = "SELECT     TaxName FROM  TaxTypeMasterMoreInfo WHERE TaxName='" + tbTaxName.Text + "' and Whid='" + ViewState["Wid"] + "' and Id<>'" + Session["ttid"] + "'";
        SqlCommand cmdclnas = new SqlCommand(chekrcd14, con);
        DataTable dtchekrcd11 = new DataTable();
        SqlDataAdapter sqldae = new SqlDataAdapter(cmdclnas);
        sqldae.Fill(dtchekrcd11);
        if (dtchekrcd11.Rows.Count == 0)
        {
            string strcln = " SELECT   TaxTypeMasterMoreInfo.Id,TaxTypeMasterMoreInfo.CountryId, TaxTypeMasterMoreInfo.StateId, TaxTypeMasterMoreInfo.ApplyToallOnlineSales, TaxTypeMasterMoreInfo.ApplyToAllSales, TaxTypeMasterMoreInfo.TaxAmt, TaxTypeMasterMoreInfo.TaxAmtInPerc,TaxTypeMasterMoreInfo.TaxName " +
               " FROM         TaxTypeMasterMoreInfo where (CountryId='" + ddlselectcountry.SelectedValue + "' or CountryId='0') and  (StateId='" + ddlstate.SelectedValue + "' or stateId='0') and Whid='" + ViewState["Wid"] + "' And Id<>'" + Session["ttid"] + "'";
            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);
            if (dtcln.Rows.Count >= 3)
            {
                Label1.Visible = true;
                Label1.Text = "You can only have three active taxes for that particular country/state";


            }
            else
            {
                int flag = 0;

                if (rdlist.SelectedIndex == 0)
                {
                    if (Convert.ToDecimal(txtAmt.Text) > Convert.ToDecimal(100))
                    {
                        flag = 1;
                    }
                }

                if (flag == 0)
                {
                    string chekrcd142 = "SELECT     * FROM  TaxTypeMasterMoreInfo WHERE  Id='" + Session["ttid"] + "'";
                    SqlCommand cmdclnas2 = new SqlCommand(chekrcd142, con);
                    DataTable dtchekrcdAc = new DataTable();
                    SqlDataAdapter sqldae2 = new SqlDataAdapter(cmdclnas2);
                    sqldae2.Fill(dtchekrcdAc);
                    if (dtchekrcdAc.Rows.Count > 0)
                    {
                    string accname = "" + tbTaxName.Text + " payable";
                    string accountidedit = "update AccountMaster set AccountName='" + accname + "' where Whid='" + ViewState["Wid"] + "' and [AccountId]='" + Convert.ToInt32(dtchekrcdAc.Rows[0]["TaxMAccountMasterID"].ToString()) + "'";
                    SqlCommand cmvv = new SqlCommand(accountidedit, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmvv.ExecuteNonQuery();
                    con.Close();

                    string purchase = "" + tbTaxName.Text + " Purchases";
                    string stper = "update AccountMaster set AccountName='" + purchase + "' where Whid='" + ViewState["Wid"] + "' and [AccountId]='" + Convert.ToString(dtchekrcdAc.Rows[0]["PurchaseTaxAccountMasterID"]) + "'";
                    SqlCommand cdper = new SqlCommand(stper, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cdper.ExecuteNonQuery();
                    con.Close();

                    string str11 = "Update TaxTypeMasterMoreInfo set Taxshortname='" + txtshortname.Text + "', TaxName='" + tbTaxName.Text + "', " +
                     "CountryId='" + ddlselectcountry.SelectedValue + "',StateId='" + ddlstate.SelectedValue + "' ," +
                     " ApplyToallOnlineSales='" + only + "',ApplyToAllSales='" + allapp + "' " +
                     ", TaxAmt='" + txtAmt.Text + "',TaxAmtInPerc='" + rdlist.SelectedValue + "',Active='" + ddlstatus.SelectedValue + "' " +
                     " where  Id='" + Session["ttid"] + "'";
                    SqlCommand cmvv1 = new SqlCommand(str11, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmvv1.ExecuteNonQuery();
                    con.Close();

                    Label1.Visible = true;
                    Label1.Text = "Record updated successfully";
                    ImageButton9.Visible = false;
                    Button3.Visible = true;
                    clearnch();
                }
                }
                else
                {
                    Label1.Visible = true;
                    Label1.Text = "You cannot input a tax percentage greater than 100";
                }
            }


            fillgrid();

        }
        else
        {
            Label1.Visible = true;
            Label1.Text = "Tax name alredy exists";
        }

    }
    protected void ddlfiltercountary_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillfistate();
        // fillgrid();
    }
    protected void fillfistate()
    {
        ddlfilterstate.Items.Clear();
        if (ddlfiltercountary.SelectedIndex > 0)
        {

            string str45 = "SELECT  distinct StateId, StateName  " +
                    " FROM  StateMasterTbl where CountryId='" + ddlfiltercountary.SelectedValue + "' " +
                    " Order By StateName";

            dbss1.FillDDL1(ddlfilterstate, str45, "StateId", "StateName");



        }
        ddlfilterstate.Items.Insert(0, "ALL");
        ddlfilterstate.SelectedItem.Value = "0";
    }

    protected void btngo_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void tbTaxName_TextChanged(object sender, EventArgs e)
    {
        if (tbTaxName.Text.Length > 0)
        {
            if (tbTaxName.Text.Length == 1)
            {
                txtshortname.Text = tbTaxName.Text.Substring(0, 1);
            }
            else if (tbTaxName.Text.Length == 2)
            {
                txtshortname.Text = tbTaxName.Text.Substring(0, 2);
            }
            else if (tbTaxName.Text.Length >= 3)
            {
                txtshortname.Text = tbTaxName.Text.Substring(0, 3);
            }

        }
        else
        {
            txtshortname.Text = "";
        }
    }
}