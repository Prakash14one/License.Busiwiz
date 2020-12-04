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

public partial class ShoppingCart_Admin_giftcertdetail : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    DataTable dt;
    DataSet ds;
    SqlDataAdapter da;
    string comid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        }
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        comid = Session["comid"].ToString();
       
        if (!IsPostBack)
        {
            if (Session["Comid"] == null)
            {
                Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
            }

            lblBusiness.Text = "All";
            lblCompany.Text = comid;
            ViewState["sortOrder"] = "";
            fillstore();
            ddlboughtuserid.Items.Insert(0, "All");
          
            fillgrid();
            Fillddlperiod();
        }
    }

    protected void fillstore()
    {
        ddlfilterbusiness.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlfilterbusiness.DataSource = ds;
        ddlfilterbusiness.DataTextField = "Name";
        ddlfilterbusiness.DataValueField = "WareHouseId";
        ddlfilterbusiness.DataBind();

        ddlfilterbusiness.Items.Insert(0, "All");
        ddlfilterbusiness.Items[0].Value = "0";

        

    }
    protected void filluser()
    {
       
        string str = "";    


        if (ddlfilterbusiness.SelectedIndex > 0)
        {
            str = "select User_master.Name,User_master.UserID  from [User_master] inner join party_master on user_master.partyid=party_master.partyid inner join warehousemaster on party_master.whid=warehousemaster.warehouseid where warehousemaster.comid='" + comid + "' and warehousemaster.warehouseid='" + ddlfilterbusiness.SelectedValue + "' ";
        }
        else
        {
            str = "select User_master.Name,User_master.UserID from [User_master] inner join party_master on user_master.partyid=party_master.partyid inner join warehousemaster on party_master.whid=warehousemaster.warehouseid where warehousemaster.comid='" + comid + "' ";
        }


        cmd = new SqlCommand(str, con);
        da = new SqlDataAdapter(cmd);
        dt = new DataTable();
        da.Fill(dt);
       
        ddlboughtuserid.DataSource = dt;
        ddlboughtuserid.DataTextField = "Name";
         ddlboughtuserid.DataValueField = "UserID";
         ddlboughtuserid.DataBind();
         ddlboughtuserid.Items.Insert(0, "All");
        
         ddlboughtuserid.Items[0].Value = "0";


        
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
    private string BetweenByPeriod()
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
        string thismonthenddate = Today.ToString();
        //------------------this month period end................



        //-----------------last month period start ---------------

        string lastmonthNumber = "12";
        int yearforlastmont12 = Convert.ToInt32(ThisYear);
        int lastmonthno = Convert.ToInt32(thismonthstart.Month.ToString()) - 1;
        if (lastmonthno.ToString() == "0")
        {
            yearforlastmont12 = Convert.ToInt32(ThisYear) - 1;
        }
        else
        {
            lastmonthNumber = Convert.ToString(lastmonthno.ToString());
        }

        //int lastmonthno = Convert.ToInt32(thismonthstart.Month.ToString()) - 1;
        //string lastmonthNumber = Convert.ToString(lastmonthno.ToString());
        DateTime lastmonth = Convert.ToDateTime(lastmonthNumber.ToString() + "/1/" + yearforlastmont12.ToString());
        string lastmonthstart = lastmonth.ToShortDateString();
        string lastmonthend = "";

        if (lastmonthNumber == "1" || lastmonthNumber == "3" || lastmonthNumber == "5" || lastmonthNumber == "7" || lastmonthNumber == "8" || lastmonthNumber == "10" || lastmonthNumber == "12")
        {
            lastmonthend = lastmonthNumber + "/31/" + yearforlastmont12.ToString();
        }
        else if (lastmonthNumber == "4" || lastmonthNumber == "6" || lastmonthNumber == "9" || lastmonthNumber == "11")
        {
            lastmonthend = lastmonthNumber + "/30/" + yearforlastmont12.ToString();
        }
        else
        {
            if (System.DateTime.IsLeapYear(Convert.ToInt32(ThisYear.ToString())))
            {
                lastmonthend = lastmonthNumber + "/29/" + yearforlastmont12.ToString();
            }
            else
            {
                lastmonthend = lastmonthNumber + "/28/" + yearforlastmont12.ToString();
            }
        }

        string lastmonthstartdate = lastmonthstart.ToString();
        string lastmonthenddate = lastmonthend.ToString();


        //-----------------last month period end -----------------------

        //--------------------this quater period start ----------------

        int thisqtr = Convert.ToInt32(thismonthstart.AddMonths(-2).Month.ToString());
        string thisqtrNumber = Convert.ToString(thisqtr.ToString());
        int lstyear = Convert.ToInt32(ThisYear);
        if (thisqtrNumber.ToString() == "11" || thisqtrNumber.ToString() == "12")
        {
            lstyear = Convert.ToInt32(ThisYear) - 1;

        }
        DateTime thisquater = Convert.ToDateTime(thisqtrNumber.ToString() + "/1/" + lstyear.ToString());
        string thisquaterstart = thisquater.ToShortDateString();
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
        string thisquaterenddate = thismonthenddate.ToString();

        // --------------this quater period end ------------------------

        // --------------last quater period start----------------------

        int lastqtr = Convert.ToInt32(thismonthstart.AddMonths(-5).Month.ToString());// -5;
        string lastqtrNumber = Convert.ToString(lastqtr.ToString());
        int lastqater3 = Convert.ToInt32(thismonthstart.AddMonths(-3).Month.ToString());
        //DateTime lastqater3 = Convert.ToDateTime(System.DateTime.Now.AddMonths(-3).Month.ToString());
        string lasterquater3 = lastqater3.ToString();
        int lstyreee = Convert.ToInt32(ThisYear);
        if (lasterquater3.ToString() == "10" || lasterquater3.ToString() == "11" || lasterquater3.ToString() == "12")
        {
            lstyreee = Convert.ToInt32(ThisYear) - 1;
        }
        DateTime lastquater = Convert.ToDateTime(lastqtrNumber.ToString() + "/1/" + lstyreee.ToString());
        string lastquaterstart = lastquater.ToShortDateString();
        string lastquaterend = "";

        if (lasterquater3 == "1" || lasterquater3 == "3" || lasterquater3 == "5" || lasterquater3 == "7" || lasterquater3 == "8" || lasterquater3 == "10" || lasterquater3 == "12")
        {
            lastquaterend = lasterquater3 + "/31/" + lstyreee.ToString();
        }
        else if (lasterquater3 == "4" || lasterquater3 == "6" || lasterquater3 == "9" || lasterquater3 == "11")
        {
            lastquaterend = lasterquater3 + "/30/" + lstyreee.ToString();
        }
        else
        {
            if (System.DateTime.IsLeapYear(Convert.ToInt32(lstyreee.ToString())))
            {
                lastquaterend = lasterquater3 + "/29/" + lstyreee.ToString();
            }
            else
            {
                lastquaterend = lasterquater3 + "/28/" + lstyreee.ToString();
            }
        }

        string lastquaterstartdate = lastquaterstart.ToString();
        string lastquaterenddate = lastquaterend.ToString();

        //--------------last quater period end-------------------------

        //--------------this year period start----------------------
        DateTime thisyearstart = Convert.ToDateTime("1/1/" + ThisYear.ToString());
        DateTime thisyearend = Convert.ToDateTime("12/31/" + ThisYear.ToString());

        string thisyearstartdate = thisyearstart.ToShortDateString();
        string thisyearenddate = thisyearend.ToShortDateString();

        //---------------this year period end-------------------
        //--------------this year period start----------------------
        DateTime lastyearstart = Convert.ToDateTime("1/1/" + System.DateTime.Today.AddYears(-1).Year.ToString());
        DateTime lastyearend = Convert.ToDateTime("12/31/" + System.DateTime.Today.AddYears(-1).Year.ToString());

        string lastyearstartdate = lastyearstart.ToShortDateString();
        string lastyearenddate = lastyearend.ToShortDateString();



        //---------------this year period end-------------------

        string periodstartdate = "";
        string periodenddate = "";

        if (ddlperiod.SelectedItem.Text == "Today")
        {

            periodstartdate = Today.ToString();
            periodenddate = Today.ToString();


        }
        else if (ddlperiod.SelectedItem.Text == "Yesterday")
        {

            periodstartdate = Yesterday.ToString();
            periodenddate = Yesterday.ToString();
            //   }
            //}
        }
        else if (ddlperiod.SelectedItem.Text == "CurrentWeek")
        {

            periodstartdate = thisweekstart.ToString();
            periodenddate = thisweekend.ToString();
            // }
            // }
        }
        else if (ddlperiod.SelectedItem.Text == "LastWeek")
        {

            periodstartdate = lastweekstartdate.ToString();
            periodenddate = Today.ToString();

        }
        else if (ddlperiod.SelectedItem.Text == "CurrentMonth")
        {

            periodstartdate = thismonthstart.ToString();
            periodenddate = Today.ToString();
            // }
            //  }
        }
        else if (ddlperiod.SelectedItem.Text == "LastMonth")
        {

            periodstartdate = lastmonthstartdate.ToString();
            periodenddate = lastmonthenddate.ToString();
            // }
            //  }


        }
        else if (ddlperiod.SelectedItem.Text == "CurrentQuater")
        {

            periodstartdate = thisquaterstartdate.ToString();
            periodenddate = thisquaterenddate.ToString();
            //}
            //  }


        }
        else if (ddlperiod.SelectedItem.Text == "LastQuater")
        {

            periodstartdate = lastquaterstartdate.ToString();
            periodenddate = lastquaterenddate.ToString();
            // }
            //  }


        }

        else if (ddlperiod.SelectedItem.Text == "CurrentYear")
        {

            periodstartdate = thisyearstartdate.ToString();
            periodenddate = thisyearenddate.ToString();



        }
        else if (ddlperiod.SelectedItem.Text == "LastYear")
        {

            periodstartdate = lastyearstartdate.ToString();
            periodenddate = lastyearenddate.ToString();



        }
        else if (ddlperiod.SelectedItem.Text == "All")
        {

            periodstartdate = "1/1/1900";
            periodenddate = Today.ToString();


        }


        DateTime sd = Convert.ToDateTime(periodstartdate.ToString());
        DateTime ed = Convert.ToDateTime(periodenddate.ToString());
        string str = " and dateandtime Between '" + sd.ToShortDateString() + "' AND '" + ed.ToShortDateString() + "'"; // + //2009-4-30' " +



        return str;
    }



       
    
    protected void fillgrid()
    {
        string str1 = "select Dateandtime,GiftCertificateMasterTbl.Active,BalanceAmount,whid,MaxAmount,GiftCertificateMasterTbl.UserID,User_master.PartyID,GiftCertificateMasterID from GiftCertificateMasterTbl inner join User_master on User_master.UserID=GiftCertificateMasterTbl.UserID inner join party_master  on party_master.partyid=User_master.PartyID";

        if (ddlfilterbusiness.SelectedIndex > 0)
        {

            lblBusiness.Text = ddlfilterbusiness.SelectedItem.Text;
            str1 += "  where Party_master.whid='" + ddlfilterbusiness.SelectedValue + "'";
        }
        if (ddlboughtuserid.SelectedIndex > 0)
        {
            str1 += " and GiftCertificateMasterTbl.UserID='" + ddlboughtuserid.SelectedValue + "'";
        }

        if (ddlperiod.SelectedIndex > 0)
        {

            str1 += BetweenByPeriod();
        }
        //if (ddlperiod.SelectedIndex > 0)

        //{
        //    if (ddlperiod.SelectedItem.Text == "Today")
        //    { 
        //      string today=DateTime.Now.ToShortDateString();
        //        str +=" and dateandtime='"+today+"'";
        //    }
        //    if (ddlperiod.SelectedItem.Text == "Yesterday")
        //    {
        //        string ystrday = DateTime.Now.AddDays(-1).ToShortDateString();
        //        str += " and dateandtime='" + ystrday + "'";
        //    }
        //    if (ddlperiod.SelectedItem.Text == "LastWeek")
        //    {

        //        string lastweekstart = DateTime.Now.ToShortDateString();
        //        string lastweekend = DateTime.Now.AddDays(-7).ToShortDateString();
        //        str += " and dateandtime between '" + lastweekstart + "' AND '" + lastweekend + "'";
        //    }
        //    if (ddlperiod.SelectedItem.Text == "Lastmonth")
        //    {
        //        string mnth = DateTime.Now.Month.ToString();
        //        string now = DateTime.Now.ToShortDateString();
        //        DateTime thismonthstart = Convert.ToDateTime(System.DateTime.Now.Month.ToString() + "/1/" + System.DateTime.Now.Year.ToString());
        //        string today1 = DateTime.Now.Day.ToString();
        //        string lmnth=DateTime.Now.AddMonths(-1).ToShortDateString();
        //        str += " and dateandtime between '" + mnth + "/"+today1+"/"+DateTime.Now.Year.ToString()+"' AND '" + lmnth + "'";
        //    }
        //    if (ddlperiod.SelectedItem.Text == "CurrentMonth")
        //    {
        //        string mnth = DateTime.Now.Month.ToString();
        //        string now = DateTime.Now.ToShortDateString();
        //        DateTime thismonthstart = Convert.ToDateTime(System.DateTime.Now.Month.ToString() + "/1/" + System.DateTime.Now.Year.ToString());
        //        string today1 = DateTime.Now.Day.ToString();
        //        string lmnth = DateTime.Now.AddMonths(-1).ToShortDateString();
        //        str += " and dateandtime between '" + mnth + "/1/" + DateTime.Now.Year.ToString() + "' AND '" +DateTime.Now.ToShortDateString()+"'";
        //    }
        //    if (ddlperiod.SelectedItem.Text == "CurrentYear")
        //    {
              
        //        DateTime  cyear=Convert.ToDateTime("1/1/"+DateTime.Now.Year.ToString());

        //        str += " and dateandtime between ' "+cyear+"' AND '" + DateTime.Now.ToShortDateString() + "'";
        //    }
        //    if (ddlperiod.SelectedItem.Text == "LastYear")

        //    {
        //        DateTime lastyear = Convert.ToDateTime(DateTime.Now.AddYears(-1).ToShortDateString());
        //        str += " and dateandtime between ' " + lastyear + "' AND '" + DateTime.Now.ToShortDateString() + "'";
        //    }
        

     //   }
        if (txtfrom.Text.Length > 0 && txttodate.Text.Length > 0)
        {
            DateTime sd = Convert.ToDateTime(txtfrom.Text);
            DateTime ed = Convert.ToDateTime(txttodate.Text);
            str1 += " and dateandtime between '" + sd + "' AND '" + ed + "'";

        }
         

         
     

        cmd = new SqlCommand(str1, con);
        da = new SqlDataAdapter(cmd);
        dt = new DataTable();
        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {

            grdviewlist.DataSource = dt;
            DataView myDataView = new DataView();
            myDataView = dt.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }
            grdviewlist.DataBind();
        }
        else
        {
            grdviewlist.EmptyDataText = "No Record Found.";
            grdviewlist.DataBind();
        }
    
    
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");

            Button1.Text = "Hide Printable Version";
            Button7.Visible = true;
            if (grdviewlist.Columns[6].Visible == true)
            {
                ViewState["editHide"] = "tt";
                grdviewlist.Columns[6].Visible = false;
            }
            if (grdviewlist.Columns[7].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                grdviewlist.Columns[7].Visible = false;
            }
           

        }
        else
        {
            //pnlgrid.ScrollBars = ScrollBars.Vertical;
            //pnlgrid.Height = new Unit(100);

            Button1.Text = "Printable Version";
            Button7.Visible = false;
            if (ViewState["editHide"] != null)
            {
                grdviewlist.Columns[6].Visible = true;
            }

            if (ViewState["deleHide"] != null)
            {
                grdviewlist.Columns[7].Visible = true;
            }
           
        }
    }
    protected void btnaddcard_Click(object sender, EventArgs e)
    {
        string te = "giftcertmastertbl.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void rdchanged(object sender, EventArgs e)
    {
        pnldate.Visible = true;
        pnlperiod.Visible = false;

    }
    protected void rdperiod_CheckedChanged(object sender, EventArgs e)
    {
        pnldate.Visible = false;
        pnlperiod.Visible = true ;
        txttodate.Text = "";
        txtfrom.Text = "";
        Fillddlperiod();
    }
    protected void grdviewlist_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        fillgrid();

    }
    protected void grdviewlist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            ViewState["deleteid"] = Convert.ToInt32(e.CommandArgument);
            ImageButton2_Click(sender, e);
          
        }
        if (e.CommandName == "Edit")

        { 
            ViewState["id"]=Convert.ToInt32(e.CommandArgument);
            cmd = new SqlCommand("select BalanceAmount,RecipientEmail from GiftCertificateMasterTbl where GiftCertificateMasterID='" + ViewState["id"] + "' ", con);
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);

            Session["balance"] = dt.Rows[0]["BalanceAmount"].ToString();
            Session["mailid"] = dt.Rows[0]["RecipientEmail"].ToString();
            Session["giftid"] = ViewState["id"].ToString();

            Response.Redirect("giftcertmastertbl.aspx?Giftid=" + Session["giftid"]);
        
        }

    }
    protected void grdviewlist_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    protected void grdviewlist_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        cmd = new SqlCommand("delete from GiftCertificateMasterTbl where GiftCertificateMasterID='" + ViewState["deleteid"] + "'",con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd.ExecuteNonQuery();
        con.Close();
        fillgrid();
 Label1.Visible = true;
      Label1.Text = "Record deleted successfully";
       // fillgrid();

        grdviewlist.SelectedIndex = -1;

       
    }
    protected void ddlfilterbusiness_SelectedIndexChanged(object sender, EventArgs e)
    {
        filluser();
       // fillgrid();
    }
    protected void Fillddlperiod()
    {

        String str = "SELECT  * FROM PeriodMaster";
        SqlCommand cmd = new SqlCommand(str, con);
        cmd.CommandType = CommandType.Text;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        ddlperiod.DataSource = dt;
        ddlperiod.DataTextField = "PeriodName";
        ddlperiod.DataValueField = "PeriodId";
        ddlperiod.DataBind();
        ddlperiod.Items.Insert(0, "All");

        ddlperiod.Items[0].Value = "0";
        ddlperiod.SelectedIndex = 0;

    }
   
   
    protected void btngo_Click(object sender, EventArgs e)
    {
        fillgrid();
        txtfrom.Text = "";
        txttodate.Text = "";
       // ddlperiod.SelectedIndex = 0;
    }
}
