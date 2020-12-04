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

public partial class ShoppingCart_Admin_Default2 : System.Web.UI.Page
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
        if (!IsPostBack)
        {
            ware();
            DataTable dtda = (DataTable)select("select Distinct Report_period_confirm.Id as conid, ReportPeriod.Report_Period_Id,Convert(nvarchar,StartDate,101) as StartDate, Convert(nvarchar,EndDate,101) as EndDate from [ReportPeriod] left join Report_period_confirm on Report_period_confirm.Report_Period_Id=ReportPeriod.Report_Period_Id where Compid = '" + Session["comid"] + "' and Whid='" + ddlbus.SelectedValue + "' and Active='1' ");
            lblfysdate.Text = Convert.ToString(dtda.Rows[0]["StartDate"]);
            lblfyedate.Text = Convert.ToString(dtda.Rows[0]["EndDate"]);

            if (dtda.Rows.Count > 0)
            {

                string fday = Convert.ToDateTime(dtda.Rows[0]["StartDate"]).Day.ToString();
                string fmonth = Convert.ToDateTime(dtda.Rows[0]["StartDate"]).Month.ToString();
                string fyear = Convert.ToDateTime(dtda.Rows[0]["StartDate"]).Year.ToString();
                               for (int k = 1; k <= 31; k++)
                {
                    ddlacday.Items.Insert(k - 1, k.ToString());
                }
                int index = 0;
                ddlacday.SelectedIndex = ddlacday.Items.IndexOf(ddlacday.Items.FindByText(fday));
                ddlacday_SelectedIndexChanged(sender, e);
                ddlacmonth.SelectedIndex = ddlacmonth.Items.IndexOf(ddlacmonth.Items.FindByText(fmonth));
                for (int k = 1; k <= 50; k++)
                {
                    int cye = (DateTime.Now.Year) + 50;
                    ddlacyear.Items.Insert(index, (cye - k).ToString());

                }
                for (int k = 1; k <= 15; k++)
                {
                    int cye = DateTime.Now.Year - 15;
                    ddlacyear.Items.Insert(index, (DateTime.Now.Year - k).ToString());
                }

                ddlacyear.SelectedIndex = ddlacyear.Items.IndexOf(ddlacyear.Items.FindByText(fyear));
                if (Convert.ToString(dtda.Rows[0]["conid"]) == "")
                {
                    lblyearcacc.Text = "Select your first accounting year for your business";
                                      pnlaccy.Visible = true;
                    //lblyearcacc.Text = "Select your First Accounting year for your business";
                    lblopenaccy.Visible = false;
                    contr(true);
                    Panel1.Visible = true;
                    btnconf.Visible = true;
                    Label9.Visible = false;
                    Label10.Visible = false;
                    AHREF.Visible = false;
                }
                else
                {

                    //ddlacmonth.SelectedIndex = ddlacmonth.Items.IndexOf(ddlacmonth.Items.FindByText(DateTime.Now.Year.ToString()));
                    contr(false);
                    pnlaccy.Visible = true;
                    //      lblyearcacc.Text = "Accounting year for your business ";
                    //      lblopenaccy.Text = Convert.ToString(dtda.Rows[0]["StartDate"]) + "-" + Convert.ToString(dtda.Rows[0]["EndDate"]);
                    lblopenaccy.Visible = true;
                    Panel1.Visible = false;
                    btnconf.Visible = false;
                    Label9.Visible = true;
                    Label10.Visible = true;
                    AHREF.Visible = true;
                    AHREF.HRef = "AccountYearchange.aspx?Wid=" + ddlbus.SelectedValue + "";

                    //lblopenaccy.NavigateUrl = "~/ShoppingCart/Admin/AccountYearChange.aspx?Whid=" + ddlbus.SelectedValue + "";
                }
            }
        }
    }
    protected void ware()
    {
        //string str1 = "select * from WarehouseMaster where comid='" + Session["comid"] + "' and WareHouseMaster.Status='1' order by Name";

        //DataTable ds1 = new DataTable();
        //SqlDataAdapter da = new SqlDataAdapter(str1, con);
        //da.Fill(ds1);
        DataTable ds = ClsStore.SelectStorename();
        if (ds.Rows.Count > 0)
        {


            ddlbus.DataSource = ds;
            ddlbus.DataTextField = "Name";
            ddlbus.DataValueField = "WarehouseId";
            ddlbus.DataBind();

        }
        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlbus.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
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
    
    protected void fillcloseentry()
    {

    }
    public double isnumSelf(string ck)
    {
        double i = 0;
        try
        {
            i = Convert.ToDouble(ck);
        }
        catch (Exception)
        {
            i = 0;
        }
        return i;
    }
    protected void ddlacday_SelectedIndexChanged(object sender, EventArgs e)
    {
        string se = "";
        if (ddlacmonth.SelectedIndex != -1)
        {
            se = ddlacmonth.SelectedItem.Text;
        }
        ddlacmonth.Items.Clear();
        if (Convert.ToInt32(ddlacday.SelectedItem.Text) <= 29)
        {
            ddlacmonth.Items.Insert(0, "1");
            ddlacmonth.Items.Insert(1, "2");
            ddlacmonth.Items.Insert(2, "3");
            ddlacmonth.Items.Insert(3, "4");
            ddlacmonth.Items.Insert(4, "5");
            ddlacmonth.Items.Insert(5, "6");
            ddlacmonth.Items.Insert(6, "7");
            ddlacmonth.Items.Insert(7, "8");
            ddlacmonth.Items.Insert(8, "9");
            ddlacmonth.Items.Insert(9, "10");
            ddlacmonth.Items.Insert(10, "11");
            ddlacmonth.Items.Insert(11, "12");
        }
        else if (Convert.ToInt32(ddlacday.SelectedItem.Text) <= 30)
        {
            ddlacmonth.Items.Insert(0, "1");
            // ddlacmonth.Items.Insert(0, "2");
            ddlacmonth.Items.Insert(1, "3");
            ddlacmonth.Items.Insert(2, "4");
            ddlacmonth.Items.Insert(3, "5");
            ddlacmonth.Items.Insert(4, "6");
            ddlacmonth.Items.Insert(5, "7");
            ddlacmonth.Items.Insert(6, "8");
            ddlacmonth.Items.Insert(7, "9");
            ddlacmonth.Items.Insert(8, "10");
            ddlacmonth.Items.Insert(9, "11");
            ddlacmonth.Items.Insert(10, "12");

        }

        else if (Convert.ToInt32(ddlacday.SelectedItem.Text) <= 31)
        {
            ddlacmonth.Items.Insert(0, "1");

            ddlacmonth.Items.Insert(1, "3");

            ddlacmonth.Items.Insert(2, "5");

            ddlacmonth.Items.Insert(3, "7");
            ddlacmonth.Items.Insert(4, "8");

            ddlacmonth.Items.Insert(5, "10");

            ddlacmonth.Items.Insert(6, "12");
        }
        if (se != "")
        {
            ddlacmonth.SelectedIndex = ddlacmonth.Items.IndexOf(ddlacmonth.Items.FindByText(se));
            lblfysdate.Text = ddlacmonth.SelectedItem.Text + "/" + ddlacday.SelectedItem.Text + "/" + ddlacyear.SelectedItem.Text;
            lblfyedate.Text = Convert.ToDateTime(lblfysdate.Text).AddYears(1).ToShortDateString();
            lblfyedate.Text = Convert.ToDateTime(lblfyedate.Text).AddDays(-1).ToShortDateString();
        }

    }
    protected void ddlbus_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtda = (DataTable)select("select Distinct Report_period_confirm.Id as conid, ReportPeriod.Report_Period_Id,Convert(nvarchar,StartDate,101) as StartDate, Convert(nvarchar,EndDate,101) as EndDate from [ReportPeriod] left join Report_period_confirm on Report_period_confirm.Report_Period_Id=ReportPeriod.Report_Period_Id where Compid = '" + Session["comid"] + "' and Whid='" + ddlbus.SelectedValue + "' and Active='1' ");
        if (dtda.Rows.Count > 0)
        {
            lblyearcacc.Text = "Select your first accounting year for your business";
            lblfysdate.Text = Convert.ToString(dtda.Rows[0]["StartDate"]);
            lblfyedate.Text = Convert.ToString(dtda.Rows[0]["EndDate"]);
         
            if (Convert.ToString(dtda.Rows[0]["conid"]) == "")
            {
                pnlaccy.Visible = true;
                //lblyearcacc.Text = "Select your First Accounting year for your business";
                lblopenaccy.Visible = false;
                string fday = Convert.ToDateTime(dtda.Rows[0]["StartDate"]).Day.ToString();
                string fmonth = Convert.ToDateTime(dtda.Rows[0]["StartDate"]).Month.ToString();
                string fyear = Convert.ToDateTime(dtda.Rows[0]["StartDate"]).Year.ToString();
                   
                ddlacday.SelectedIndex = ddlacday.Items.IndexOf(ddlacday.Items.FindByText(fday));
                ddlacyear.SelectedIndex = ddlacyear.Items.IndexOf(ddlacyear.Items.FindByText(fyear));
                ddlacday_SelectedIndexChanged(sender, e);
                ddlacmonth.SelectedIndex = ddlacmonth.Items.IndexOf(ddlacmonth.Items.FindByText(fmonth));
              
                contr(true);
                Panel1.Visible = true;
                btnconf.Visible = true;
                Label9.Visible = false;
                Label10.Visible = false;
                AHREF.Visible = false;
            }
            else
            {
                lblopenaccy.NavigateUrl = "~/ShoppingCart/Admin/AccountYearChange.aspx?Whid=" + ddlbus.SelectedValue + "";
                AHREF.HRef = "AccountYearchange.aspx?Wid=" + ddlbus.SelectedValue + "";
                pnlaccy.Visible = true;
                contr(false);
                Panel1.Visible = false;
                btnconf.Visible = false;
                Label9.Visible = true;
                Label10.Visible = true;
                AHREF.Visible = true;
                //lblyearcacc.Text = "Accounting year for your business ";
                // lblopenaccy.Text = Convert.ToString(dtda.Rows[0]["StartDate"]) + "-" + Convert.ToString(dtda.Rows[0]["EndDate"]);
                //lblopenaccy.Visible = true;
            }

        }
    }
    protected void contr(Boolean t)
    {
        ddlacday.Enabled = t;
        ddlacmonth.Enabled = t;
        ddlacyear.Enabled = t;
        ddlacmonth.Enabled = t;
        btnconf.Enabled = t;
       
    }
    protected void ddlacmonth_SelectedIndexChanged(object sender, EventArgs e)
    {

        lblfysdate.Text = ddlacmonth.SelectedItem.Text + "/" + ddlacday.SelectedItem.Text + "/" + ddlacyear.SelectedItem.Text;


        lblfyedate.Text = Convert.ToDateTime(lblfysdate.Text).AddYears(1).ToShortDateString(); ;
        lblfyedate.Text = Convert.ToDateTime(lblfyedate.Text).AddDays(-1).ToShortDateString();
    }
    protected void ddlacyear_SelectedIndexChanged(object sender, EventArgs e)
    {

        lblfysdate.Text = ddlacmonth.SelectedItem.Text + "/" + ddlacday.SelectedItem.Text + "/" + ddlacyear.SelectedItem.Text;


        lblfyedate.Text = Convert.ToDateTime(lblfysdate.Text).AddYears(1).ToShortDateString();
        lblfyedate.Text = Convert.ToDateTime(lblfyedate.Text).AddDays(-1).ToShortDateString();
    }
    protected void btnapd_Click(object sender, EventArgs e)
    {
        int Currentyearorg = 0;
        int Currentyear = 0;
        DataTable dtda = (DataTable)select("select Report_Period_Id,Convert(nvarchar,StartDate,101) as StartDate, Convert(nvarchar,EndDate,101) as EndDate from [ReportPeriod] where Compid = '" + Session["comid"] + "' and Whid='" + ddlbus.SelectedValue + "' and Active='1' ");
        if (dtda.Rows.Count > 0)
        {
            DataTable dtdaopb = new DataTable();
            dtdaopb = (DataTable)select("select top(1)Report_Period_Id from [ReportPeriod] where ReportPeriod.Report_Period_Id<'" + dtda.Rows[0]["Report_Period_Id"] + "' and  Whid='" + ddlbus.SelectedValue + "'  order by Report_Period_Id Desc");
            if (dtdaopb.Rows.Count > 0)
            {
                string inse = "Insert into Report_period_confirm(Report_Period_Id,conform)values('" + dtda.Rows[0]["Report_Period_Id"] + "','1')";
                SqlCommand cmd = new SqlCommand(inse, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                con.Close();
                Currentyear = Convert.ToInt32(ddlacyear.SelectedItem.Text) + 1;
                Currentyearorg = DateTime.Now.Year + 1;
                int yem = Convert.ToInt32(ddlacyear.SelectedItem.Text) + 1;
                string yname = ddlacyear.SelectedItem.Text + " TO " + yem.ToString();
                string upd = "Update ReportPeriod set Name='" + yname + "',StartDate='" + Convert.ToDateTime(lblfysdate.Text) + "',EndDate='" + Convert.ToDateTime(lblfyedate.Text) + "',FistStartDate='" + Convert.ToDateTime(lblfysdate.Text) + "' where Report_Period_Id='" + dtda.Rows[0]["Report_Period_Id"] + "'";
                SqlCommand cmd1 = new SqlCommand(upd, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd1.ExecuteNonQuery();
                con.Close();


                int yem1 = Convert.ToInt32(ddlacyear.SelectedItem.Text) - 1;
                string yname1 = yem1.ToString() + " TO " + ddlacyear.SelectedItem.Text;
                string upd1 = "Update ReportPeriod set Name='" + yname1 + "',StartDate='" + Convert.ToDateTime(lblfysdate.Text).AddYears(-1).ToString() + "',EndDate='" + Convert.ToDateTime(lblfyedate.Text).AddYears(-1).ToString() + "',FistStartDate='" + Convert.ToDateTime(lblfysdate.Text) + "' where Report_Period_Id='" + dtdaopb.Rows[0]["Report_Period_Id"] + "'";
                SqlCommand cmd11 = new SqlCommand(upd1, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd11.ExecuteNonQuery();
                con.Close();
                int k = 1;
                for (int i = Currentyear; i < Currentyearorg; i++)
                {
                    DateTime fye = Convert.ToDateTime(lblfysdate.Text).AddYears(k);
                    DateTime eye = Convert.ToDateTime(lblfyedate.Text).AddYears(k);
                    int sy = Convert.ToInt32(ddlacyear.SelectedItem.Text) + k;
                    int ey = Convert.ToInt32(ddlacyear.SelectedItem.Text) + (k + 1);
                    string nam = sy.ToString() + " TO " + ey.ToString();
                    SqlCommand cmdtr2 = new SqlCommand("Insert into ReportPeriod(Name,StartDate,EndDate,FistStartDate,Active,Compid,Whid)Values('" + nam + "','" + fye + "','" + eye + "','" + fye + "','0','" + Session["Comid"] + "','" + ddlbus.SelectedValue + "') ", con);

                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdtr2.ExecuteNonQuery();
                    con.Close();
                    k += 1;
                    SqlCommand cmsa = new SqlCommand("SELECT max(Report_Period_Id) from ReportPeriod where Compid='" + Session["comid"] + "' and Whid='" + ddlbus.SelectedValue + "'", con);
                    con.Open();
                    object rig = cmsa.ExecuteScalar();
                    con.Close();
                    string str5t1 = "select Id from AccountMaster where Whid='" + ddlbus.SelectedValue + "'";
                    SqlCommand cmd32t1 = new SqlCommand(str5t1, con);
                    SqlDataAdapter adp15t1 = new SqlDataAdapter(cmd32t1);
                    DataTable dtlogin14t1 = new DataTable();
                    adp15t1.Fill(dtlogin14t1);
                    foreach (DataRow dr in dtlogin14t1.Rows)
                    {
                        SqlCommand cmdtr13 = new SqlCommand("Insert into AccountBalance(AccountMasterId,Balance,Report_Period_Id)Values('" + dr["Id"] + "','0','" + rig + "') ", con);

                        con.Open();
                        cmdtr13.ExecuteNonQuery();
                        con.Close();
                    }
                }
                contr(false);
                ModalPopupExtender122.Hide();
            }
        }
    }
    protected void btnconf_Click(object sender, EventArgs e)
    {
        ModalPopupExtender122.Show();
    }
}
