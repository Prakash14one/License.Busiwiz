
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

public partial class Shoppingcart_Admin_TaxMasterAndMoreInfo_New : System.Web.UI.Page
{
  //  SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString"].ConnectionString);
    SqlConnection con;
    string compid;
    Boolean only;
    Boolean allapp;
    DBCommands1 dbss1 = new DBCommands1();
    public int puraccid = 0;
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

        compid = Session["Comid"].ToString();
        Page.Title = pg.getPageTitle(page);
        //Label1.Visible = false;
        ViewState["Wid"] = Request.QueryString["wid"].ToString();

        if (!Page.IsPostBack)
        {
           ViewState["sortOrder"] = "";
           lblCompany.Text = Session["Cname"].ToString();
            string warename = "select WareHouseId,Name from WarehouseMaster where WarehouseId='" + ViewState["Wid"] + "'";
            SqlDataAdapter adp = new SqlDataAdapter(warename, con);
            DataTable dtp = new DataTable();
            adp.Fill(dtp);
            if (dtp.Rows.Count > 0)
            {
                
                lblstorename.Text = dtp.Rows[0]["Name"].ToString();
            }
            //FillTax();
          //  ShowRecordincontrols();
            fillgrid();
           // RadioButtonList1_SelectedIndexChanged(sender, e);
            //chkCharge_CheckedChanged(sender, e);

          //  RadioButtonList2_SelectedIndexChanged(sender, e);
          //  RadioButtonList3_SelectedIndexChanged(sender, e);
            lblCompany.Text = Session["Cname"].ToString();
        }
    }


    

    
   


    protected void fillgrid()
    {
        lblBusiness.Text = lblstorename.Text;
        string strgrd = "select distinct Taxshortname, TaxTypeMasterId,Name,Percentage,Amount,Applytoonlinesales,Whid,ApplyAllsalesandretail,Active,Case when(Percentage<>0)then Percentage else Amount end as TaxRate,Case when(Active=1)then 'Active' else 'Inactive' end as Status  from TaxTypeMaster where Whid='" + ViewState["Wid"] + "'";
        DataTable dtg = dbss1.cmdSelect(strgrd);
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
            Label lbltaxpers = (Label)item.FindControl("lbltaxpers");
            Label lblsingamt = (Label)item.FindControl("lblsingamt");
            Label lblsignpers = (Label)item.FindControl("lblsignpers");
            if (Convert.ToDecimal(lbltaxpers.Text) > 0)
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        Label1.Text = "";
        string st153 = "select Report_Period_Id  from ReportPeriod where Compid='" + compid + "' and Whid='" + ViewState["Wid"].ToString() + "' and Active='1'";
        SqlCommand cmd153 = new SqlCommand(st153, con);
        SqlDataAdapter adp153 = new SqlDataAdapter(cmd153);
        DataTable ds153 = new DataTable();
        adp153.Fill(ds153);
        if (ds153.Rows.Count > 0)
        {
            Session["reportid"] = ds153.Rows[0]["Report_Period_Id"].ToString();
        }
        else
        {

            Session["reportid"] = "0";
        }

        //if (tbTaxName.Text.Length > 1 && tbPer.Text.Length > 1)
        //{
        if (tbTaxName.Text != "" && tbPer.Text != "")
        {
            int accountid;
            string compare = "select Name from TaxTypeMaster where Name='" + tbTaxName.Text + "' and Whid='" + ViewState["Wid"] + "'";
            SqlDataAdapter adc = new SqlDataAdapter(compare, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            DataTable dtc = new DataTable();
            adc.Fill(dtc);

            if (dtc.Rows.Count > 0)
            {
                Label1.Text = "Tax Name already exists";
                Label1.Visible = true;
            }

            else
            {
                int flag = 0;
                if (rdlist.SelectedIndex == 0)
                {
                    if (rdlist.SelectedIndex == 0)
                    {
                        if (Convert.ToDecimal(tbPer.Text) > Convert.ToDecimal(100))
                        {
                            flag = 1;
                        }
                    }
                }
                if (flag == 0)
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

                    if (rdlist.SelectedIndex == 0)
                    {
                        string three = "select * from TaxTypeMaster where Whid='" + ViewState["Wid"] + "' ";
                        SqlDataAdapter da3 = new SqlDataAdapter(three, con);
                        DataTable dt3 = new DataTable();
                        da3.Fill(dt3);

                        if (dt3.Rows.Count > 2)
                        {
                            Label1.Text = "You are not allowed to add more than three taxes";
                            Label1.Visible = true;
                        }
                        else
                        {


                            string acid = "  select max(AccountId) as AccountId from AccountMaster where Whid='" + ViewState["Wid"] + "' and GroupId=17";
                            SqlDataAdapter aacid = new SqlDataAdapter(acid, con);

                            DataTable adtc = new DataTable();
                            aacid.Fill(adtc);
                            if (adtc.Rows[0]["AccountId"].ToString() != "")
                            {
                                accountid = (Convert.ToInt32(adtc.Rows[0]["AccountId"].ToString()) + 1);
                                accountid =Convert.ToInt32(acccc(accountid.ToString()));
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

                            string str = "insert into  TaxTypeMaster(name,Percentage,Amount,Applytoonlinesales,Whid,ApplyAllsalesandretail,Active,TaxAccountMasterID,PurchaseTaxAccountMasterID,Taxshortname)values('" + tbTaxName.Text + "', " +
                               " '" + tbPer.Text + "','0', " +
                               " '" + only + "' ,' " + ViewState["Wid"] + "','" + allapp + "','" + ddlstatus.SelectedValue + "','" + accountid + "','" + puraccid + "','" + txtshortname.Text + "')";
                            bool ik = dbss1.cmdInsUpdateDelete(str);
                            if (ik == true)
                            {

                                Label1.Visible = true;
                                Label1.Text = "Record inserted successfully";
                                cle();
                                fillgrid();

                            }
                            else
                            {
                                Label1.Visible = true;
                                Label1.Text = "Error : ";
                            }
                        }
                    }
                    else
                    {
                        string three = "select * from TaxTypeMaster where Whid='" + ViewState["Wid"] + "' ";
                        SqlDataAdapter da3 = new SqlDataAdapter(three, con);
                        DataTable dt3 = new DataTable();
                        da3.Fill(dt3);

                        if (dt3.Rows.Count > 2)
                        {
                            Label1.Text = "You are not allowed to add more than three taxes";
                            Label1.Visible = true;
                        }

                        else
                        {
                            string acid = "  select max(AccountId) as AccountId from AccountMaster where Whid='" + ViewState["Wid"] + "' and GroupId=17";
                            SqlDataAdapter aacid = new SqlDataAdapter(acid, con);

                            DataTable adtc = new DataTable();
                            aacid.Fill(adtc);
                            if (adtc.Rows[0]["AccountId"].ToString() != "")
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

                            string accper = "  select max(AccountId) as AccountId from AccountMaster where Whid='" + ViewState["Wid"] + "' and GroupId=5";
                            SqlDataAdapter aqla = new SqlDataAdapter(accper, con);

                            DataTable asper = new DataTable();
                            aqla.Fill(asper);
                            if (Convert.ToString(asper.Rows[0]["AccountId"]) != "")
                            {
                                puraccid = Convert.ToInt32(asper.Rows[0]["AccountId"]) + 1;

                            }
                            else
                            {
                                if (puraccid >= 1999)
                                {
                                    if (puraccid > 17000)
                                    {
                                        puraccid += 1;
                                    }
                                    else
                                    {
                                        puraccid = 17000;
                                    }
                                }
                                else
                                {
                                    if (puraccid == 0)
                                    {
                                        puraccid = 1700;
                                    }
                                    else
                                    {
                                        puraccid = puraccid + 1;
                                    }
                                }
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


                            string peracname = "" + tbTaxName.Text + " Purchases";
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


                            string str = "insert into    TaxTypeMaster(name,Percentage,Amount,Applytoonlinesales,Whid,ApplyAllsalesandretail,Active,TaxAccountMasterID,PurchaseTaxAccountMasterID,Taxshortname)values('" + tbTaxName.Text + "', " +
                              " '0','" + tbPer.Text + "', " +
                              " '" + only + "' ,' " + ViewState["Wid"] + "','" + allapp + "','" + ddlstatus.SelectedValue + "','" + accountid + "','" + puraccid + "','" + txtshortname.Text + "')";
                            bool ik = dbss1.cmdInsUpdateDelete(str);
                            if (ik == true)
                            {

                                Label1.Visible = true;
                                Label1.Text = "Record inserted successfully";
                                fillgrid();
                                cle();
                            }
                            else
                            {
                                Label1.Visible = true;
                                Label1.Text = "Error : ";
                            }
                        }
                    }
                    cle();

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
            Label1.Text = "please insert something";
        }
    }
    public void cle()
    {
            tbTaxName.Text = "";
            tbPer.Text = "";
            txtshortname.Text = "";
           // chkper.Checked = false;
           // RadioButtonList4.SelectedIndex = 0;
           // chkactive.Checked = true;
    }
     
       
   
    protected void edit_Click(object sender, EventArgs e)
    {
        Label1.Text = "";
        ImageButton9.Visible = true;
        ImageButton1.Visible = false;
       // LinkButton lnk = (LinkButton)sender;
        ImageButton lnk = (ImageButton)sender;
        int i = Convert.ToInt32(lnk.CommandArgument);
        Session["tid"] = i;
        String selectStr = "select Taxshortname, PurchaseTaxAccountMasterID, TaxTypeMasterId,TaxAccountMasterID,Name,Percentage,Amount,Applytoonlinesales,Whid,ApplyAllsalesandretail,Active from TaxTypeMaster where TaxTypeMasterId='" + i + "' ";
        SqlDataAdapter ad = new SqlDataAdapter(selectStr, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        DataTable dt = new DataTable();
        ad.Fill(dt);
        Session["Acid"] =Convert.ToString(dt.Rows[0]["TaxAccountMasterID"]);
        ViewState["Pacid"] = Convert.ToString(dt.Rows[0]["PurchaseTaxAccountMasterID"]);
        tbTaxName.Text =Convert.ToString( dt.Rows[0]["Name"]);
        txtshortname.Text = Convert.ToString(dt.Rows[0]["Taxshortname"]);
        //int per =Convert.ToInt32(dt.Rows[0]["Percentage"].ToString());
        double per = Convert.ToDouble(dt.Rows[0]["Percentage"].ToString());
        if (per == 0)
        {
            rdlist.SelectedIndex = 1;
           // chkper.Checked = false;
            tbPer.Text = dt.Rows[0]["Amount"].ToString();
        }
        else
        {
            rdlist.SelectedIndex = 0;
           // chkper.Checked = true;
            tbPer.Text = dt.Rows[0]["Percentage"].ToString();
        }

        string  onsales = dt.Rows[0]["Applytoonlinesales"].ToString();
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
        string  act =dt.Rows[0]["Active"].ToString();

        if (act == "True")
        {
            ddlstatus.SelectedIndex = 0;
           // chkactive.Checked = true;
        }
        else
        {
            ddlstatus.SelectedIndex =1;
            //chkactive.Checked = false;
        }
        
    }

    protected void ImageButton9_Click(object sender, EventArgs e)
    {
        Label1.Text = "";
        string compare = "select Name from TaxTypeMaster where Name='" + tbTaxName.Text + "'and Whid='" + ViewState["Wid"] + "' and  TaxTypeMasterId<>'" + Session["tid"].ToString() + "' ";
        SqlDataAdapter adc = new SqlDataAdapter(compare, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        DataTable dtc = new DataTable();
        adc.Fill(dtc);

        if (dtc.Rows.Count > 0)
        {
            Label1.Text = "Record already exists";
            Label1.Visible = true;
        }

        else
        {

            int flag = 0;
            if (rdlist.SelectedIndex == 0)
            {
                if (rdlist.SelectedIndex == 0)
                {
                    if (Convert.ToDecimal(tbPer.Text) > Convert.ToDecimal(100))
                    {
                        flag = 1;
                    }
                }
            }
            if (flag == 0)
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

                if (rdlist.SelectedIndex == 0)
                {
                    string accname = "" + tbTaxName.Text + " payable";
                    string accountidedit = "update AccountMaster set AccountName='" + accname + "' where Whid='" + ViewState["Wid"] + "' and [AccountId]='" + Convert.ToString(Session["Acid"]) + "'";
                    bool ik1 = dbss1.cmdInsUpdateDelete(accountidedit);

                    string purch = "" + tbTaxName.Text + " Purchases";
                    string accperc = "update AccountMaster set AccountName='" + purch + "' where Whid='" + ViewState["Wid"] + "' and [AccountId]='" + Convert.ToString(ViewState["Pacid"]) + "'";
                    bool ik1per = dbss1.cmdInsUpdateDelete(accperc);

                    int ttid = Convert.ToInt32(Session["tid"].ToString());
                    string str = "Update TaxTypeMaster set name='" + tbTaxName.Text + "',Percentage='" + tbPer.Text + "',Amount='0',Applytoonlinesales='" + only + "',ApplyAllsalesandretail='" + allapp + "',Active='" + ddlstatus.SelectedValue + "',PurchaseTaxAccountMasterID='" + ViewState["Pacid"] + "',Taxshortname='" + txtshortname.Text + "' where TaxTypeMasterId='" + ttid + "'";



                    //string str = "insert into    TaxTypeMaster(name,Percentage,Amount,Applytoonlinesales,Whid,ApplyAllsalesandretail,Active)values('" + tbTaxName.Text + "', " +
                    //   " '" + tbPer.Text + "','0', " +
                    //   " '" + only + "' ,' " + compid + "','" + allapp + "','" + chkactive.Checked + "')";
                    bool ik = dbss1.cmdInsUpdateDelete(str);
                    if (ik == true)
                    {

                        Label1.Visible = true;
                        Label1.Text = "Record updated successfully";

                        fillgrid();
                        cle();
                        ImageButton9.Visible = false;
                        ImageButton1.Visible = true;
                    }
                    else
                    {
                        Label1.Visible = true;
                        Label1.Text = "Error : ";
                    }
                }
                else
                {
                    string accname = "" + tbTaxName.Text + " payable";
                    string accountidedit = "update AccountMaster set AccountName='" + accname + "' where Whid='" + ViewState["Wid"] + "' and [AccountId]='" + Convert.ToString(Session["Acid"]) + "'";
                    bool ik1 = dbss1.cmdInsUpdateDelete(accountidedit);

                    string purch = "" + tbTaxName.Text + " Purchases";
                    string accperc = "update AccountMaster set AccountName='" + purch + "' where Whid='" + ViewState["Wid"] + "' and [AccountId]='" + Convert.ToString(ViewState["Pacid"]) + "'";
                    bool ik1per = dbss1.cmdInsUpdateDelete(accperc);

                    int ttid = Convert.ToInt32(Session["tid"].ToString());
                    string str = "Update TaxTypeMaster set name='" + tbTaxName.Text + "',Percentage='0',Amount='" + tbPer.Text + "',Applytoonlinesales='" + only + "',ApplyAllsalesandretail='" + allapp + "',Active='" + ddlstatus.SelectedValue + "',PurchaseTaxAccountMasterID='" + ViewState["Pacid"] + "',Taxshortname='" + txtshortname.Text + "' where TaxTypeMasterId='" + ttid + "'";
                    //string str = "insert into    TaxTypeMaster(name,Percentage,Amount,Applytoonlinesales,Whid,ApplyAllsalesandretail,Active)values('" + tbTaxName.Text + "', " +
                    //  " '0','" + tbPer.Text + "', " +
                    //  " '" + only + "' ,' " + compid + "','" + allapp + "','" + chkactive.Checked + "')";
                    bool ik = dbss1.cmdInsUpdateDelete(str);
                    if (ik == true)
                    {

                        Label1.Visible = true;
                        Label1.Text = "Record updated successfully";
                        String selectStr = "select TaxTypeMasterId,Name,Percentage,Amount,Applytoonlinesales from TaxTypeMaster where Name='" + tbTaxName.Text + "' ";
                        SqlDataAdapter ad = new SqlDataAdapter(selectStr, con);
                        //  con.Open();
                        DataTable dt = new DataTable();
                        ad.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            ViewState["id"] = dt.Rows[0]["TaxTypeMasterId"].ToString();

                        }
                        con.Close();
                        fillgrid();
                        cle();
                        ImageButton9.Visible = false;
                        ImageButton1.Visible = true;
                    }
                    else
                    {
                        Label1.Visible = true;
                        Label1.Text = "Error : ";
                    }
                }


            }

            else
            {
                Label1.Visible = true;
                Label1.Text = "You cannot input a tax percentage greater than 100";
            }
        }

    }
    //protected void LinkButton97666667_Click(object sender, EventArgs e)
    //{
    //    con.Close();
    //    LinkButton del = (LinkButton)sender;
    //    int deli = Convert.ToInt32(del.CommandArgument);
    //    string chkid = "select * from SalesOrderTempTax where TaxTypeMasterID=" + deli + "";
    //    DataTable dtchkid = dbss1.cmdSelect(chkid);
    //    if (dtchkid.Rows.Count > 0)
    //    {
    //        Label1.Visible = true;
    //        Label1.Text = "You can not delete this Tax entry as some order /invoice exist for this tax entry. However you can make this entry inactive to stop using this tax entry for your sales or edit the entry for change in name or tax rates etc.";
    //    }
    //    else
    //    {

           
    //        string str = "delete  from TaxTypeMaster where TaxTypeMasterId='" + deli + "'";
    //        if (con.State.ToString() != "Open")
    //        {
    //            con.Open();
    //        }
    //        SqlCommand dcmd = new SqlCommand(str, con);
    //        dcmd.ExecuteNonQuery();
    //        fillgrid();
    //        Label1.Visible = true;
    //        Label1.Text = "Record Successfully Deleted";
    //    }

    //}
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        ImageButton9.Visible = false;
        ImageButton1.Visible = true;
        cle();
       
        Label1.Text = "";
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
    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (Button4.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");

            Button4.Text = "Hide Printable Version";
            Button3.Visible = true;
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
            Button3.Visible = false;
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
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //con.Close();
        //LinkButton del = (LinkButton)sender;
        //int deli = Convert.ToInt32(del.CommandArgument);

       int deli= Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());

        string chkid = "select * from SalesOrderTempTax where TaxTypeMasterID=" + deli + "";
        DataTable dtchkid = dbss1.cmdSelect(chkid);
        if (dtchkid.Rows.Count > 0)
        {
            Label1.Visible = true;
            Label1.Text = "You can not delete this Tax entry as some order /invoice exist for this tax entry. However you can make this entry inactive to stop using this tax entry for your sales or edit the entry for change in name or tax rates etc.";
        }
        else
        {

            string saa = "select * from TaxTypeMaster where TaxTypeMasterID=" + deli + "";
             DataTable dtty = dbss1.cmdSelect(saa);
             if (dtty.Rows.Count > 0)
             {
                 string str = "delete  from TaxTypeMaster where TaxTypeMasterId='" + deli + "'";
                 if (con.State.ToString() != "Open")
                 {
                     con.Open();
                 }
                 SqlCommand dcmd = new SqlCommand(str, con);
                 dcmd.ExecuteNonQuery();
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

                 string str1w = "select Id  from AccountMaster where Whid='" + ViewState["Wid"] + "' and AccountId='" + dtty.Rows[0]["TaxAccountMasterID"] + "'";

                 SqlDataAdapter adpzx = new SqlDataAdapter(str1w,con);
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
                 string strperds = "delete  from AccountMaster where Whid='" + dtty.Rows[0]["Whid"] + "' and AccountId='" + dtty.Rows[0]["TaxAccountMasterID"] + "'";
                 if (con.State.ToString() != "Open")
                 {
                     con.Open();
                 }
                 SqlCommand dcmpers = new SqlCommand(strperds, con);
                 dcmpers.ExecuteNonQuery();
                 con.Close();
                 fillgrid();
                 Label1.Visible = true;
                 Label1.Text = "Record deleted successfully";
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


