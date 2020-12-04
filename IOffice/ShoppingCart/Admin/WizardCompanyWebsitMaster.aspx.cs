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
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Text;


public partial class Add_Wizard_Company_Website_Master : System.Web.UI.Page
{
    SqlConnection con;
    public static int flat = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblVersion.Text = "This is Version ** Updated on 10/10/2015 by Manan";
        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Login.aspx");
        }
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();


     //   Page.Title = pg.getPageTitle(page);

        Session["pnlM"] = "1";
        Session["pnl1"] = "12";

        if (!IsPostBack)
        {
            //DataTable datcand = select("select candidatemaster.LastName + ' ' + candidatemaster.FirstName + ' ' + candidatemaster.MiddleName as name,candidatemaster.CandidateId from candidatemaster where partyid='" + Convert.ToInt32(Session["PartyId"].ToString()) + "'");

            //if (datcand.Rows.Count > 0)
            //{
            //    Response.Redirect("http://members.ijobcenter.com/");

            //}
            //else
            //{
                
            //}


            ViewState["sortOrder"] = "";
            //Session["pagename"] = "WizardCompanyWebsitMaster.aspx";
            //Session["pnl1"] = "2";
            lblcomname.Text = Convert.ToString(Session["Cname"]);
            Fillddlcomapnyname();
            fillddlcurrency();
            fillgriddata();
            filltimezone();
            //Loaddata();
            //ddlCompanyName  
            accountdate();
            if (Request.Url.Host.ToString() == "itimekeeper.com")
            {
                pnlcurrency.Visible = false;
                Panel1.Visible = false;
                pnlaccy.Visible = false;
                GridView1.Columns[4].Visible = false;
            }
            else if (Request.Url.Host.ToString() == "itimekeeper.us")
            {
                pnlcurrency.Visible = false;
                Panel1.Visible = false;
                pnlaccy.Visible = false;
                GridView1.Columns[4].Visible = false;
            }
            SqlCommand cmdwid = new SqlCommand("select WareHouseMaster.Name+':'+EmployeeMaster.EmployeeName as EmpName,EmployeeMaster.EmployeeMasterId,WareHouseMaster.WareHouseId from WareHouseMaster inner join EmployeeMaster on EmployeeMaster.Whid=WareHouseMaster.WareHouseId inner join Party_Master on Party_Master.PartyId=EmployeeMaster.PartyId inner join PartytTypeMaster on PartytTypeMaster.PartyTypeId=Party_Master.PartyTypeId  where PartType !='1Admin' and PartytTypeMaster.compid='" + Session["comid"] + "' Order by EmpName ", con);
            SqlDataAdapter dtpwid = new SqlDataAdapter(cmdwid);
            DataTable dtwid = new DataTable();
            dtpwid.Fill(dtwid);
            if (dtwid.Rows.Count > 0)
            {
                gridAccess.DataSource = dtwid;
                gridAccess.DataBind();
            }
            foreach (GridViewRow grd in gridAccess.Rows)
            {
                Label lblwid = (Label)grd.FindControl("lblwid");
                Label lblEmployeeMasterId = (Label)grd.FindControl("lblEmployeeMasterId");
                CheckBox ChkAess = (CheckBox)grd.FindControl("ChkAess");
                CheckBox chka = (CheckBox)grd.FindControl("chka");
                string str = "select  distinct * from EmployeeWarehouseRights where  Whid='" + lblwid.Text + "' and EmployeeId='" + lblEmployeeMasterId.Text + "' ";
                SqlDataAdapter dtpwida = new SqlDataAdapter(str, con);
                DataTable dtwida = new DataTable();
                dtpwida.Fill(dtwida);
                if (dtwida.Rows.Count > 0)
                {
                    chka.Checked = true;
                    ChkAess.Checked = true;
                }


            }
        }
        Label3.Text = "";

        //string strqa = TextEmailMasterLoginPassword.Text;
        //TextEmailMasterLoginPassword.Attributes.Add("Value", strqa);
    }
    protected void filltimezone()
    {
        string str1 = "SELECT ID,[Name]+':'+ShortName+':'+gmt AS TimeZone FROM [TimeZoneMaster] where  TimeZoneMaster.Status ='1'";
        SqlCommand cmd1 = new SqlCommand(str1, con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataTable ds1 = new DataTable();
        da1.Fill(ds1);

        ddlwztimezone.DataSource = ds1;
        ddlwztimezone.DataTextField = "TimeZone";
        ddlwztimezone.DataValueField = "ID";
        ddlwztimezone.DataBind();

        string bus = "select top(1) * from WHTimeZone where compid = '" + Session["Comid"] + "'  order by Id desc ";
        SqlCommand cmdbus = new SqlCommand(bus, con);
        SqlDataAdapter adpbus = new SqlDataAdapter(cmdbus);
        DataTable dtbus = new DataTable();
        adpbus.Fill(dtbus);
        if (dtbus.Rows.Count > 0)
        {
            ddlwztimezone.SelectedIndex = ddlwztimezone.Items.IndexOf(ddlwztimezone.Items.FindByValue(dtbus.Rows[0]["TimeZone"].ToString()));
        }
    }

    protected void accountdate()
    {

        //lblyearcacc.Text = "Select your First Accounting year for your business";
        //lblopenaccy.Visible = false;
        string fday = "1";
        string fmonth = "4";
        string fyear = DateTime.Now.Year.ToString();
        lblfysdate.Text = "4/1/" + fyear;

        lblfyedate.Text = Convert.ToDateTime(lblfysdate.Text).AddYears(1).ToShortDateString();
        lblfyedate.Text = Convert.ToDateTime(lblfyedate.Text).AddDays(-1).ToShortDateString();

        lblfirstyear.Text = "April 1," + fyear;
        lblendyear.Text = Convert.ToDateTime(lblfirstyear.Text).AddYears(1).ToString("MMMM dd, yyyy");
        lblendyear.Text = Convert.ToDateTime(lblendyear.Text).AddDays(-1).ToString("MMMM dd, yyyy");
        for (int k = 1; k <= 31; k++)
        {
            ddlacday.Items.Insert(k - 1, k.ToString());
        }
        int index = 0;
        ddlacday.SelectedIndex = ddlacday.Items.IndexOf(ddlacday.Items.FindByText(fday));
        EventArgs e = new EventArgs();
        object sender = new object();
        ddlacday_SelectedIndexChanged(sender, e);
        ddlacmonth.SelectedIndex = ddlacmonth.Items.IndexOf(ddlacmonth.Items.FindByValue(fmonth));
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


    }
    protected void Button1_Click1(object sender, ImageClickEventArgs e)
    {

        Response.Redirect("~/ShoppingCart/Admin/WizardCompanyWebsiteAddressMaster.aspx");

    }

    protected void SqlDataSource2_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {

    }
    protected void websiteurl(string masterid)
    {

        string finalcompid = Session["Comid"].ToString();


        SqlCommand cmdsel = new SqlCommand("select WebSite from CompanyAddressMaster where CompanyMasterId ='" + masterid + "' ", con);

        // SqlCommand cmdsel = new SqlCommand("select WebSite from CompanyAddressMaster where CompanyMasterId ='" + ddlCompanyName.Text + "' and CompanyMasterId=(select * from CompanyMaster where Compid='" + finalcompid + "') ", con);
        SqlDataAdapter dtpsel = new SqlDataAdapter(cmdsel);
        DataTable dtsel = new DataTable();
        dtpsel.Fill(dtsel);
        if (dtsel.Rows.Count > 0)
        {
            lblsiteurl.Text = dtsel.Rows[0]["WebSite"].ToString();
        }
        SqlCommand cmdwid = new SqlCommand("select Max(WareHouseId) as wid from WareHouseMaster", con);
        SqlDataAdapter dtpwid = new SqlDataAdapter(cmdwid);
        DataTable dtwid = new DataTable();
        dtpwid.Fill(dtwid);
        if (dtwid.Rows.Count > 0)
        {
            if (dtwid.Rows[0]["wid"].ToString() != "")
            {
                int i = 0;
                i = Convert.ToInt32(dtwid.Rows[0]["wid"].ToString()) + 1;
                ViewState["wid"] = i;

                lblshop.Text = "";
                lblshop.Text = "/ShoppingCart/default.aspx?WHid=" + i;
            }
            else
            {
                lblshop.Text = "";
                lblshop.Text = "/ShoppingCart/default.aspx?WHid=1";
                ViewState["wid"] = 1;
            }

        }

    }
    protected void Fillddlcomapnyname()
    {
        string finalcompid1 = Session["Comid"].ToString();
        string strcom = "SELECT [CompanyId], [CompanyName] FROM [CompanyMaster] where Compid='" + finalcompid1 + "'";
        SqlCommand cmdcom = new SqlCommand(strcom, con);
        SqlDataAdapter adpcom = new SqlDataAdapter(cmdcom);
        DataTable dtcom = new DataTable();

        adpcom.Fill(dtcom);
        if (dtcom.Rows.Count > 0)
        {
            ViewState["cid"] = dtcom.Rows[0]["CompanyId"].ToString();
            ddlCompanyName.Text = dtcom.Rows[0]["CompanyName"].ToString();
            websiteurl(dtcom.Rows[0]["CompanyId"].ToString());
            //ddlCompanyName.Items[0].Value = "0";
        }
        else
        {

        }

    }
    protected void fillgriddata()
    {
        //        SqlCommand cmdgrid = new SqlCommand("SELECT     CompanyWebsitMaster.WHId, CompanyWebsitMaster.CompanyWebsiteMasterId, CompanyWebsitMaster.CompanyId,  " +
        //                      " CompanyWebsitMaster.Sitename, CompanyWebsitMaster.SiteUrl, CompanyWebsitMaster.Active, WareHouseMaster.Name, " +
        //                      " CurrencyMaster.CurrencyName " +
        //" FROM         WareHouseMaster LEFT OUTER JOIN " +
        //                      " CurrencyMaster ON WareHouseMaster.CurrencyId = CurrencyMaster.CurrencyId RIGHT OUTER JOIN " +
        //                      " CompanyWebsitMaster ON WareHouseMaster.WareHouseId = CompanyWebsitMaster.WHId ", con);
        //        SqlCommand cmdgrid = new SqlCommand("SELECT     CompanyWebsitMaster.WHId, CompanyWebsitMaster.CompanyWebsiteMasterId, CompanyWebsitMaster.CompanyId, " +
        //                      " CompanyWebsitMaster.Sitename, CompanyWebsitMaster.SiteUrl, CompanyWebsitMaster.Active, WareHouseMaster.Name, " +
        //                      " CurrencyMaster.CurrencyName, CompanyMaster.CompanyName " +
        //" FROM         CompanyMaster RIGHT OUTER JOIN " +
        //                      " CompanyWebsitMaster ON CompanyMaster.CompanyId = CompanyWebsitMaster.CompanyId LEFT OUTER JOIN " +
        //                      " WareHouseMaster LEFT OUTER JOIN " +
        //                      " CurrencyMaster ON WareHouseMaster.CurrencyId = CurrencyMaster.CurrencyId ON CompanyWebsitMaster.WHId = WareHouseMaster.WareHouseId",con);
        //**********Radhika Chnages.

        //string sgggg = "SELECT     CompanyWebsitMaster.WHId, CompanyWebsitMaster.CompanyWebsiteMasterId, CompanyWebsitMaster.CompanyId, CompanyWebsitMaster.SiteUrl, " +
        //           "   CompanyWebsitMaster.Active, WareHouseMaster.Name AS Sitename, CurrencyMaster.CurrencyName, CompanyMaster.CompanyName, " +
        //           "   CompanyAddressMaster.WebSite " +
        //           " FROM         CurrencyMaster RIGHT OUTER JOIN " +
        //           "   CompanyMaster LEFT OUTER JOIN " +
        //           "   CompanyAddressMaster ON CompanyMaster.CompanyId = CompanyAddressMaster.CompanyMasterId RIGHT OUTER JOIN " +
        //           "   WareHouseMaster LEFT OUTER JOIN " +
        //           "   CompanyWebsitMaster ON WareHouseMaster.WareHouseId = CompanyWebsitMaster.WHId ON CompanyMaster.CompanyId = CompanyWebsitMaster.CompanyId ON " +
        //           "    CurrencyMaster.CurrencyId = WareHouseMaster.CurrencyId ";

        //********************
        string strfil = "";
        if (ddlactfil.SelectedIndex > 0)
        {
            strfil = " and CompanyWebsitMaster.Active='" + ddlactfil.SelectedValue + "'";

        }
        string finalcompid1 = Session["Comid"].ToString();
        string sgggg = " CompanyWebsitMaster.WHId, CompanyWebsitMaster.CompanyWebsiteMasterId, CompanyWebsitMaster.CompanyId, CompanyWebsitMaster.SiteUrl, " +
                 "  case  when(CompanyWebsitMaster.Active='1') then 'Active' else 'Inactive' end as Active, WareHouseMaster.Name AS Sitename, CurrencyMaster.CurrencyName, CompanyMaster.CompanyName, " +
                 "  CompanyAddressMaster.WebSite " +
                 "  FROM         CurrencyMaster RIGHT OUTER JOIN " +
                 "  CompanyMaster LEFT OUTER JOIN " +
                 "  CompanyAddressMaster ON CompanyMaster.CompanyId = CompanyAddressMaster.CompanyMasterId RIGHT OUTER JOIN " +
                 "  WareHouseMaster LEFT OUTER JOIN " +
                 "  CompanyWebsitMaster ON WareHouseMaster.WareHouseId = CompanyWebsitMaster.WHId ON CompanyMaster.CompanyId = CompanyWebsitMaster.CompanyId ON " +
                 "  CurrencyMaster.CurrencyId = WareHouseMaster.CurrencyId   where WareHouseMaster.comid='" + finalcompid1 + "' " + strfil + "";

        string str2 = " select count(WarehouseMaster.WarehouseID) as ci " +
                     "  FROM         CurrencyMaster RIGHT OUTER JOIN " +
                 "  CompanyMaster LEFT OUTER JOIN " +
                 "  CompanyAddressMaster ON CompanyMaster.CompanyId = CompanyAddressMaster.CompanyMasterId RIGHT OUTER JOIN " +
                 "  WareHouseMaster LEFT OUTER JOIN " +
                 "  CompanyWebsitMaster ON WareHouseMaster.WareHouseId = CompanyWebsitMaster.WHId ON CompanyMaster.CompanyId = CompanyWebsitMaster.CompanyId ON " +
                 "  CurrencyMaster.CurrencyId = WareHouseMaster.CurrencyId   where WareHouseMaster.comid='" + finalcompid1 + "' " + strfil + "";
        //order by Sitename";

        GridView1.VirtualItemCount = GetRowCount(str2);

        string sortExpression = " WareHouseMaster.Name asc";

        if (Convert.ToInt32(ViewState["count"]) > 0)
        {
            DataTable dtgrid = GetDataPage(GridView1.PageIndex, GridView1.PageSize, sortExpression, sgggg);

            GridView1.DataSource = dtgrid;
            DataView myDataView = new DataView();
            myDataView = dtgrid.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }

            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }

        //SqlCommand cmdgrid = new SqlCommand(sgggg, con);
        //SqlDataAdapter dtpgrid = new SqlDataAdapter(cmdgrid);
        //DataTable dtgrid = new DataTable();
        //dtpgrid.Fill(dtgrid);      

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

    protected DataTable select(string qu)
    {
        SqlCommand cmd = new SqlCommand(qu, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        return dt;
    }

    protected void fillddlcurrency()
    {
        SqlCommand cmdcurr = new SqlCommand("Select * from CurrencyMaster order by CurrencyName", con);
        SqlDataAdapter dtpcurr = new SqlDataAdapter(cmdcurr);
        DataTable dtcurr = new DataTable();
        dtpcurr.Fill(dtcurr);
        if (dtcurr.Rows.Count > 0)
        {
            ddlcurrency.DataSource = dtcurr;
            ddlcurrency.DataTextField = "CurrencyName";
            ddlcurrency.DataValueField = "CurrencyId";
            ddlcurrency.DataBind();
            ddlcurrency.Items.Insert(0, "-Select-");
            ddlcurrency.Items[0].Value = "0";
        }
    }
    protected void ImgBtnSubmit_Click(object sender, EventArgs e)
    {

        string finalcompid2 = Session["Comid"].ToString();
        SqlCommand cmdcheck = new SqlCommand("select Name from WarehouseMaster where Name ='" + TextSiteName.Text.Replace("'", "''") + "' and comid='" + finalcompid2 + "'", con);
        SqlDataAdapter dtpcheck = new SqlDataAdapter(cmdcheck);
        DataTable dtcheck = new DataTable();
        dtpcheck.Fill(dtcheck);
        if (dtcheck.Rows.Count > 0)
        {
            Label3.Text = "";
            Label3.Text = "Record already exists";
        }
        else
        {
            //if (flat == 5)
            // {
            lblfinal.Text = lblfysdate.Text + " - " + lblfyedate.Text + ".";
            TextEmailMasterLoginPassword.Attributes.Add("Value", TextEmailMasterLoginPassword.Text);
            ModalPopupExtender1222.Show();


            // }
            // else
            // {
            //ModapupExtende22.Show();
            //    Label3.Text = "Please first confirm Account Year";
            // }
        }


        //string str = "SELECT     CompanyWebsiteMasterId FROM  CompanyWebsitMaster";
        //SqlCommand cmd = new SqlCommand(str, con);
        //SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //DataTable dt = new DataTable();
        //adp.Fill(dt);

        //if (dt.Rows.Count == 0)
        //{

        //    SqlCommand mycmd = new SqlCommand("Sp_Insert_CompanyWebsitMaster", con);
        //    mycmd.CommandType = CommandType.StoredProcedure;
        //    mycmd.Parameters.AddWithValue("@CompanyId", ddlCompanyName.Text);
        //    mycmd.Parameters.AddWithValue("@Sitename", TextSiteName.Text);
        //    mycmd.Parameters.AddWithValue("@SiteUrl", TextSiteUrl.Text);
        //    mycmd.Parameters.AddWithValue("@SupportEmail", TextSupportEmail.Text);
        //    mycmd.Parameters.AddWithValue("@WebMasterEmail", TextWebMasterEmail.Text);
        //    mycmd.Parameters.AddWithValue("@AdminEmail", TextAdminEmail.Text);
        //    mycmd.Parameters.AddWithValue("@IncomingMailServer", TextIncomingMailServer.Text);
        //    mycmd.Parameters.AddWithValue("@OutGoingMailServer", TextOutGoingMailServer.Text);
        //    mycmd.Parameters.AddWithValue("@EmailMasterLoginPassword", TextEmailMasterLoginPassword.Text);

        //    con.Open();
        //    mycmd.ExecuteNonQuery();
        //    con.Close();
        //    Label3.Visible = true;
        //    Label3.Text = "Record Inserted Successfully";
        //    //GridView1.DataBind();
        //    //TextSiteName.Text = "";
        //    //TextSiteUrl.Text = "";
        //    //TextSupportEmail.Text = "";
        //    //TextWebMasterEmail.Text = "";
        //    //TextAdminEmail.Text = "";
        //    //TextIncomingMailServer.Text = "";
        //    //TextOutGoingMailServer.Text = "";
        //    //TextEmailMasterLoginPassword.Text = "";
        //}
        //else
        //{
        //    string sst = "UPDATE    CompanyWebsitMaster " +
        //            " SET   Sitename ='" + TextSiteName.Text + "', SiteUrl ='" + TextSiteUrl.Text + "', SupportEmail ='" + TextSupportEmail.Text + "', WebMasterEmail ='" + TextWebMasterEmail.Text + "', AdminEmail ='" + TextAdminEmail.Text + "', OutGoingMailServer ='" + TextOutGoingMailServer.Text + "', EmailMasterLoginPassword ='" + TextEmailMasterLoginPassword.Text + "',  " +
        //              " IncomingMailServer = '" + TextIncomingMailServer.Text + "' where CompanyWebsiteMasterId='" + Convert.ToInt32(dt.Rows[0]["CompanyWebsiteMasterId"]) + "'";
        //    SqlCommand cmd11 = new SqlCommand(sst, con);
        //    con.Open();
        //    cmd11.ExecuteNonQuery();
        //    con.Close();
        //    Label3.Visible = true;
        //    Label3.Text = "Record Updated Successfully";

        //}
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        ControlenableTEstmail(true);
        if (txtmemailname.Enabled != false)
        {
            try
            {


                string AdminEmail = txtmemailid.Text;


                string outgoinmailserver = TextOutGoingMailServer.Text;
                string incommingserver = TextIncomingMailServer.Text;



                String Password = TextEmailMasterLoginPassword.Text;



                string body = "Please ignore this message.<br> This email was generated to test the email server account configuration by " + Request.Url.Host + ".<br> The test was successful.<br> Thank you for using " + Request.Url.Host + ".";

                MailAddress to = new MailAddress(AdminEmail);
                MailAddress from = new MailAddress(AdminEmail);
                //MailAddress fromdes = new MailAddr
                MailMessage objEmail = new MailMessage(from, to);
                objEmail.Subject = "Test of mail server by " + Request.Url.Host + " account configuration ";
                objEmail.Body = body.ToString();
                objEmail.IsBodyHtml = true;
                //objEmail.From.DisplayName = txtmemailname.Text;

                objEmail.Priority = MailPriority.High;


                SmtpClient client = new SmtpClient();

                client.Credentials = new NetworkCredential(AdminEmail, Password);
                client.Host = outgoinmailserver;


                client.Send(objEmail);

                MailAddress to1 = new MailAddress(AdminEmail);
                MailAddress from1 = new MailAddress(AdminEmail);

                MailMessage objEmail1 = new MailMessage(from1, to1);
                objEmail1.Subject = "Test Mail Server";
                objEmail1.Body = body.ToString();
                objEmail1.IsBodyHtml = true;


                objEmail1.Priority = MailPriority.High;


                SmtpClient client1 = new SmtpClient();

                client1.Credentials = new NetworkCredential(AdminEmail, Password);
                client1.Host = incommingserver;
                Label3.Text = "Test Connection Successful";
                Label3.Visible = true;
                TextEmailMasterLoginPassword.Attributes.Add("Value", Password);
            }
            catch (Exception ert)
            {

                Label3.Text = "Error Connecting Mail Server :  " + ert.Message;
                Label3.Visible = true;
            }
        }
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {

    }
    protected void ImageButton5_Click(object sender, EventArgs e)
    {


        string finalcompid2 = Session["Comid"].ToString();

        SqlDataAdapter adwarehousecheck = new SqlDataAdapter("select * from WarehouseMaster where Name='" + TextSiteName.Text.Replace("'", "''") + "' and comid='" + Session["Comid"].ToString() + "' ", con);
        DataTable dswarehousecheck = new DataTable();
        adwarehousecheck.Fill(dswarehousecheck);

        if (dswarehousecheck.Rows.Count > 0)
        {
            Label3.Visible = true;
            Label3.Text = "Record already exists";
        }
        else
        {

            SqlCommand cmdins = new SqlCommand("insert into WarehouseMaster(Name,Address,CurrencyId,Status,comid,OnlineStoreSetup) values('" + TextSiteName.Text.Replace("'", "''") + "','" + TextSiteName.Text.Replace("'", "''") + "','" + ddlcurrency.SelectedValue + "','" + ddlstatus.SelectedValue + "','" + finalcompid2 + "','" + chkbusisetup.Checked + "')", con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdins.ExecuteNonQuery();
            con.Close();

            SqlDataAdapter ad = new SqlDataAdapter("select max(WareHouseId) as WareHouseId from WarehouseMaster ", con);
            DataSet ds112 = new DataSet();
            ad.Fill(ds112);

            int whidmax = Convert.ToInt32(ds112.Tables[0].Rows[0]["WareHouseId"]);
            ViewState["whid"] = ds112.Tables[0].Rows[0]["WareHouseId"];

            SqlCommand cmdinsweb = new SqlCommand("insert into CompanyWebsitMaster(CompanyId,Sitename,SiteUrl,SupportEmail,WebMasterEmail, " +
                " AdminEmail,IncomingMailServer,OutGoingMailServer,EmailMasterLoginPassword,EmailSentDisplayName,MasterEmailId,UNameForSendingMail,Active,WHId) " +
                " values ('" + ViewState["cid"] + "','" + TextSiteName.Text.Replace("'", "''") + "','" + lblsiteurl.Text + "','" + TextSupportEmail.Text + "','" + TextWebMasterEmail.Text + "', " +
                " '" + TextAdminEmail.Text + "','" + TextIncomingMailServer.Text + "','" + TextOutGoingMailServer.Text + "','" + TextEmailMasterLoginPassword.Text + "','" + txtmemailname.Text + "', " +
                " '" + txtmemailid.Text + "','" + txtusmail.Text + "','" + ddlstatus.SelectedValue + "','" + whidmax + "')", con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdinsweb.ExecuteNonQuery();
            con.Close();

            deptdesi();

            Label3.Text = "";
            Label3.Text = "Record inserted successfully";

            accountentry();

            ddlcurrency.SelectedIndex = 0;
            TextSiteName.Text = "";
            lblsiteurl.Text = "";
            txtmemailname.Text = "";
            txtmemailid.Text = "";
            txtusmail.Text = "";
            TextEmailMasterLoginPassword.Attributes.Clear();
            TextEmailMasterLoginPassword.Text = "";
            TextIncomingMailServer.Text = "";
            TextAdminEmail.Text = "";
            TextSupportEmail.Text = "";
            TextWebMasterEmail.Text = "";
            TextOutGoingMailServer.Text = "";
            ddlstatus.SelectedIndex = 0;
            lblshop.Text = "";
            chkbusisetup.Checked = true;
            lblurrr.Visible = false;
            lblhttp.Visible = false;
            lblsiteurl.Visible = false;
            lblshop.Visible = false;
            chkmasteremail.Checked = false;
            pnlmasteremail.Visible = false;
            fillgriddata();
            pnladmin.Visible = false;
            chkad.Checked = false;
            CheckBox1.Checked = false;
            pnlaccy.Visible = false;
            btnadd.Visible = true;
            pnladd.Visible = false;
            lbladd.Text = "";
            filltimezone();
            ModalPopupExtender1222.Hide();

        }
    }
    //protected DataTable select(string str)
    //{
    //    SqlCommand cmd = new SqlCommand(str, con);
    //    SqlDataAdapter dtp = new SqlDataAdapter(cmd);
    //    DataTable dt = new DataTable();
    //    dtp.Fill(dt);

    //    return dt;

    //}
    protected void BatchWorhingDay(string Whid, string batchtimId, string BatchId, string comid)
    {
        bool chk = false;
        int days = 0;
        String day = "";
        DataTable ds11 = select("Select ReportPeriod.StartDate from ReportPeriod where  ReportPeriod.Whid='" + Whid + "' and ReportPeriod.Active='1'");
        if (ds11.Rows.Count > 0)
        {
            DateTime date = Convert.ToDateTime(ds11.Rows[0]["StartDate"]);
            int year = Convert.ToDateTime(ds11.Rows[0]["StartDate"]).Year;
            day = date.DayOfWeek.ToString();
            if (ds11.Rows.Count > 0)
            {
                if (year % 4 == 0)
                {
                    days = 366;

                }
                else
                {
                    days = 365;

                }

                for (int i = 0; i < days; i++)
                {

                    day = date.DayOfWeek.ToString();
                    if (day == "Monday" || day == "Tuesday" || day == "Wednesday" || day == "Thursday" || day == "Friday")
                    {
                        chk = true;

                    }
                    if (chk == true)
                    {
                        DataTable ds1222 = select("Select DateMasterTbl.Date , DateMasterTbl.DateId,DateMasterTbl.day from DateMasterTbl where DateMasterTbl.Date='" + date + "' and DateMasterTbl.day='" + day + "'");
                        if (ds1222.Rows.Count == 1)
                        {
                            string strbatch = "Insert  Into BatchWorkingDays(DateMasterID,BatchID,Monday,MondayScheduleId,Tuesday,TuesdayscheduleId,Wednesday,WednesdayscheduleId,Thursday,ThursdayscheduleId,Friday,FridayscheduleId,Saturday,SaturdayscheduleId,Sunday,SundayscheduleId)values('" + ds1222.Rows[0]["DateId"] + "','" + BatchId + "','1','" + batchtimId + "','1','" + batchtimId + "','1','" + batchtimId + "','1','" + batchtimId + "','1','" + batchtimId + "','0','0','0','0')";
                            SqlCommand cmd12 = new SqlCommand(strbatch, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();

                            }

                            cmd12.ExecuteNonQuery();
                            con.Close();

                        }
                    }

                    date = date.AddDays(1);
                    chk = false;


                }
                string str = "Insert  Into BatchWorkingDay(Whid,BatchMasterId,Monday,MondayScheduleId,Tuesday,TuesdayscheduleId,Wednesday,WednesdayscheduleId,Thursday,ThursdayscheduleId,Friday,FridayscheduleId,Saturday,SaturdayscheduleId,Sunday,SundayscheduleId,compid,LastDayOftheWeek)  Values('" + Whid + "','" + BatchId + "','1','" + batchtimId + "','1','" + batchtimId + "','1','" + batchtimId + "','1','" + batchtimId + "','1','" + batchtimId + "','0','0','0','0','" + comid + "','Friday')";
                SqlCommand cmd = new SqlCommand(str, con);

                if (con.State.ToString() != "Open")
                {
                    con.Open();

                }

                cmd.ExecuteNonQuery();
                con.Close();

            }
        }
    }
    protected object inBatchTimingMaster(string BatchMasterId, string StartTime, string EndTime, string FirstBreakStartTime, string FirstBreakEndTime, string TimeScheduleMasterId, string Active, string Whid, string compid, string totalhours)
    {
        object dept;
        SqlCommand cmddept = new SqlCommand("BatchTimingRetIdentity", con);
        cmddept.CommandType = CommandType.StoredProcedure;
        cmddept.Parameters.AddWithValue("@BatchMasterId", BatchMasterId);
        cmddept.Parameters.AddWithValue("@StartTime", StartTime);
        cmddept.Parameters.AddWithValue("@EndTime", EndTime);
        cmddept.Parameters.AddWithValue("@FirstBreakStartTime", FirstBreakStartTime);
        cmddept.Parameters.AddWithValue("@FirstBreakEndTime", FirstBreakEndTime);
        cmddept.Parameters.AddWithValue("@TimeScheduleMasterId", TimeScheduleMasterId);
        cmddept.Parameters.AddWithValue("@Active", Active);
        cmddept.Parameters.AddWithValue("@Whid", Whid);
        cmddept.Parameters.AddWithValue("@compid", compid);
        cmddept.Parameters.AddWithValue("@totalhours", totalhours);

        cmddept.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
        cmddept.Parameters["@Id"].Direction = ParameterDirection.Output;
        cmddept.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmddept.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        dept = (object)cmddept.ExecuteNonQuery();
        dept = cmddept.Parameters["@Id"].Value;
        con.Close();
        return dept;
    }

    protected object inBatchMaster(string Batchname, string Whid, string status, string BatchZone, string Cid, string DefaultBatchId)
    {

        object dept;
        SqlCommand cmddept = new SqlCommand("BatchMasterRetIdentity", con);
        cmddept.CommandType = CommandType.StoredProcedure;
        cmddept.Parameters.AddWithValue("@Name", Batchname);
        cmddept.Parameters.AddWithValue("@WHID", Whid);
        cmddept.Parameters.AddWithValue("@Status", status);
        cmddept.Parameters.AddWithValue("@BatchTimeZone", BatchZone);
        cmddept.Parameters.AddWithValue("@CompanyId", Cid);
        cmddept.Parameters.AddWithValue("@DefaultBatchId", DefaultBatchId);
        cmddept.Parameters.AddWithValue("@EffectiveStartDate", System.DateTime.Now.ToShortDateString());
        cmddept.Parameters.AddWithValue("@EffectiveEndDate", System.DateTime.Now.AddYears(5).ToShortDateString());

        cmddept.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
        cmddept.Parameters["@Id"].Direction = ParameterDirection.Output;
        cmddept.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmddept.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        dept = (object)cmddept.ExecuteNonQuery();
        dept = cmddept.Parameters["@Id"].Value;
        con.Close();
        return dept;
    }
    protected object inWHTimeZone(string Whid, string Est, string Cid)
    {
        object dept;
        SqlCommand cmddept = new SqlCommand("WHTimeZoneRetIdentity", con);
        cmddept.CommandType = CommandType.StoredProcedure;
        cmddept.Parameters.AddWithValue("@WHID", Whid);
        cmddept.Parameters.AddWithValue("@TimeZone", Est);
        cmddept.Parameters.AddWithValue("@compid", Cid);
        cmddept.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
        cmddept.Parameters["@Id"].Direction = ParameterDirection.Output;
        cmddept.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmddept.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        dept = (object)cmddept.ExecuteNonQuery();
        dept = cmddept.Parameters["@Id"].Value;
        con.Close();
        return dept;
    }

    protected object indept(string deptname, string cname, string wname)
    {
        //SqlConnection liceco = new SqlConnection();
        //liceco = PageConn.licenseconn();
        object dept = "";
        string strcomid = "SELECT Distinct DeptId, DeptName,CompanyMaster.PricePlanId  from CompanyMaster inner join DefaultDeptIdforPriceplan on DefaultDeptIdforPriceplan.PriceplanId=CompanyMaster.PricePlanId inner join DefaultDept on DefaultDept.DeptId=DefaultDeptIdforPriceplan.DefaultDeptId  WHERE CompanyMaster.CompanyLoginId = '" + cname + "'";

        SqlDataAdapter adcheck1 = new SqlDataAdapter(strcomid, PageConn.licenseconn());
        DataTable dtcheck1 = new DataTable();
        adcheck1.Fill(dtcheck1);
        foreach (DataRow item in dtcheck1.Rows)
        {
            SqlCommand cmddept = new SqlCommand("DeptRetIdentity", con);
            cmddept.CommandType = CommandType.StoredProcedure;
            cmddept.Parameters.AddWithValue("@Departmentname", item["DeptName"]);
            cmddept.Parameters.AddWithValue("@Companyid", cname);
            cmddept.Parameters.AddWithValue("@Whid", wname);
            cmddept.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
            cmddept.Parameters["@Id"].Direction = ParameterDirection.Output;
            cmddept.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
            cmddept.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            dept = (object)cmddept.ExecuteNonQuery();
            dept = cmddept.Parameters["@Id"].Value;
            con.Close();
        }
        return dept;
    }
    protected void indesignation(string desname, string cid, string wid)
    {
        //SqlConnection liceco = new SqlConnection();
        //liceco = PageConn.licenseconn();
        string strcomid = "SELECT Distinct designationname, DefaultDesignationTbl.RoleId, RoleName,CompanyMaster.PricePlanId,DefaultDesignationTbl.DeptId,DeptName   from CompanyMaster inner join DefaultRolIdforePriceplan on DefaultRolIdforePriceplan.PriceplanId=CompanyMaster.PricePlanId inner join DefaultRole on DefaultRole.RoleId=DefaultRolIdforePriceplan.DefaultRoleId inner join DefaultDesignationTbl on DefaultDesignationTbl.RoleId=DefaultRole.RoleId inner join DefaultDept on DefaultDept.DeptId=DefaultDesignationTbl.DeptId WHERE CompanyMaster.CompanyLoginId = '" + cid + "'";

        SqlDataAdapter adcheck1 = new SqlDataAdapter(strcomid, PageConn.licenseconn());
        DataTable dtcheck1 = new DataTable();
        adcheck1.Fill(dtcheck1);
        foreach (DataRow item in dtcheck1.Rows)
        {
            string strmdept = "SELECT Distinct id from DepartmentmasterMNC WHERE DepartmentmasterMNC.Departmentname='" + Convert.ToString(item["DeptName"]) + "' and DepartmentmasterMNC.Whid = '" + wid + "'";

            SqlDataAdapter adsdep = new SqlDataAdapter(strmdept, con);
            DataTable dtsdep = new DataTable();
            adsdep.Fill(dtsdep);

            string strmdeptrol = "SELECT Distinct Role_id from RoleMaster WHERE RoleMaster.Role_name='" + Convert.ToString(item["RoleName"]) + "' and RoleMaster.compid = '" + cid + "'";

            SqlDataAdapter adsdeprol = new SqlDataAdapter(strmdeptrol, con);
            DataTable dtsdeprol = new DataTable();
            adsdeprol.Fill(dtsdeprol);

            if (dtsdeprol.Rows.Count > 0)
            {

                SqlCommand cmddegdes = new SqlCommand("Insert into DesignationMaster(DesignationName,DeptID,RoleId) values ('" + Convert.ToString(item["designationname"]) + "','" + dtsdep.Rows[0]["id"] + "','" + dtsdeprol.Rows[0]["Role_id"] + "')", con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmddegdes.ExecuteNonQuery();
                con.Close();
            }
        }
    }


    protected void accountentry()
    {


        SqlCommand cmd777 = new SqlCommand("select max(WarehouseId) from Warehousemaster where comid='" + Session["comid"] + "'", con);

        con.Open();
        object yy = cmd777.ExecuteScalar();
        con.Close();


        string strcompnaywebsiteaddress = " select CompanyAddressMaster.* from CompanyAddressMaster inner join CompanyMaster on CompanyMaster.CompanyId=CompanyAddressMaster.CompanyMasterId where CompanyMaster.Compid='" + Session["comid"] + "'";
        SqlCommand cmdcompnaywebsiteaddress = new SqlCommand(strcompnaywebsiteaddress, con);
        SqlDataAdapter adpcompnaywebsiteaddress = new SqlDataAdapter(cmdcompnaywebsiteaddress);
        DataTable dtcompnaywebsiteaddress = new DataTable();
        adpcompnaywebsiteaddress.Fill(dtcompnaywebsiteaddress);



        string eplazacityid = "";
        string eplazastateid = "";
        string eplazacountryid = "";
        string eplazaaddress = "";
        string eplazaaddress2 = "";
        string eplazaphone = "";
        string eplazafax = "";
        string eplazaemail = "";
        string eplazapincode = "";
        string eplazacontact1 = "";
        string eplazacontact2 = "";
        string eplazacontactpersonname = "";
        string eplazacontactpersondesignation = "";
        string companyname = "";


        if (dtcompnaywebsiteaddress.Rows.Count > 0)
        {
            eplazacityid = dtcompnaywebsiteaddress.Rows[0]["City"].ToString();
            eplazastateid = dtcompnaywebsiteaddress.Rows[0]["State"].ToString();
            eplazacountryid = dtcompnaywebsiteaddress.Rows[0]["Country"].ToString();
            eplazaaddress = dtcompnaywebsiteaddress.Rows[0]["Address1"].ToString();
            eplazaaddress2 = dtcompnaywebsiteaddress.Rows[0]["Address2"].ToString();
            eplazaphone = dtcompnaywebsiteaddress.Rows[0]["Phone"].ToString();
            eplazafax = dtcompnaywebsiteaddress.Rows[0]["Fax"].ToString();
            eplazaemail = dtcompnaywebsiteaddress.Rows[0]["Email"].ToString();
            eplazapincode = dtcompnaywebsiteaddress.Rows[0]["PinCode"].ToString();
            eplazacontact1 = dtcompnaywebsiteaddress.Rows[0]["ContactNo1"].ToString();
            eplazacontact2 = dtcompnaywebsiteaddress.Rows[0]["ContactNo2"].ToString();
            eplazacontactpersonname = dtcompnaywebsiteaddress.Rows[0]["ContactPersonNAme"].ToString();
            eplazacontactpersondesignation = dtcompnaywebsiteaddress.Rows[0]["ContactPersonDesignation"].ToString();

        }


        string strmaxaddresstype1 = "SELECT * from AddressTypeMaster where Name='Business Address' and compid='" + Session["comid"] + "'";
        SqlCommand cmdmaxaddresstype1 = new SqlCommand(strmaxaddresstype1, con);
        SqlDataAdapter adpmaxaddresstype1 = new SqlDataAdapter(cmdmaxaddresstype1);
        DataTable dtmaxaddresstype1 = new DataTable();
        adpmaxaddresstype1.Fill(dtmaxaddresstype1);
        if (dtmaxaddresstype1.Rows.Count > 0)
        {
            ViewState["AddressTypeMasterId1"] = dtmaxaddresstype1.Rows[0]["AddressTypeMasterId"].ToString();

            SqlCommand cmd4businessadress1 = new SqlCommand("Insert into CompanyWebsiteAddressMaster(CompanyWebsiteMasterId,AddressTypeMasterId,Address1,Address2,City,State,Country,Phone1,TollFree1,TollFree2,Fax,Zip,Email,ContactPersonName,ContactPersonDesignation) values ('" + yy.ToString() + "','" + ViewState["AddressTypeMasterId1"] + "','" + eplazaaddress + "','" + eplazaaddress2 + "','" + eplazacityid + "','" + eplazastateid + "','" + eplazacountryid + "','" + eplazaphone + "','" + eplazacontact1 + "','" + eplazacontact2 + "','" + eplazafax + "','" + eplazapincode + "','" + eplazaemail + "','" + eplazacontactpersonname + "','" + eplazacontactpersondesignation + "')", con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd4businessadress1.ExecuteNonQuery();
            con.Close();
        }
        string strmaxaddresstype2 = "SELECT * from AddressTypeMaster where Name='Shipping Address' and compid='" + Session["comid"] + "' ";
        SqlCommand cmdmaxaddresstype2 = new SqlCommand(strmaxaddresstype2, con);
        SqlDataAdapter adpmaxaddresstype2 = new SqlDataAdapter(cmdmaxaddresstype2);
        DataTable dtmaxaddresstype2 = new DataTable();
        adpmaxaddresstype2.Fill(dtmaxaddresstype2);
        if (dtmaxaddresstype2.Rows.Count > 0)
        {
            ViewState["AddressTypeMasterId2"] = dtmaxaddresstype2.Rows[0]["AddressTypeMasterId"].ToString();

            SqlCommand cmd4businessadress2 = new SqlCommand("Insert into CompanyWebsiteAddressMaster(CompanyWebsiteMasterId,AddressTypeMasterId,Address1,Address2,City,State,Country,Phone1,TollFree1,TollFree2,Fax,Zip,Email,ContactPersonName,ContactPersonDesignation) values ('" + yy.ToString() + "','" + ViewState["AddressTypeMasterId2"] + "','" + eplazaaddress + "','" + eplazaaddress2 + "','" + eplazacityid + "','" + eplazastateid + "','" + eplazacountryid + "','" + eplazaphone + "','" + eplazacontact1 + "','" + eplazacontact2 + "','" + eplazafax + "','" + eplazapincode + "','" + eplazaemail + "','" + eplazacontactpersonname + "','" + eplazacontactpersondesignation + "')", con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd4businessadress2.ExecuteNonQuery();
            con.Close();

        }


        object dept = indept("Admin", Session["comid"].ToString(), yy.ToString());

        SqlCommand cmddept = new SqlCommand("Select id from DepartmentmasterMNC where  Departmentname='Admin' and Whid='" + yy.ToString() + "'", con);
        con.Open();
        dept = cmddept.ExecuteScalar();
        con.Close();

        SqlCommand cmdseleminwz = new SqlCommand("Select min(WarehouseId) from Warehousemaster where comid='" + Session["comid"] + "'", con);
        con.Open();
        object minwz = cmdseleminwz.ExecuteScalar();
        con.Close();


        indesignation("Admin", Session["comid"].ToString(), yy.ToString());
        SqlCommand cmddesig = new SqlCommand("Select DesignationMasterId from DesignationMaster where DesignationName='Admin' and DeptID='" + dept + "'", con);
        con.Open();
        object desig = cmddesig.ExecuteScalar();
        con.Close();

        SqlCommand cmd2g = new SqlCommand("Insert into Storepaymentmethod(Paypal,Craditcard,Cheque,Credit,Creditcart_offline,Do_Co_RealTime,Cash,onlineCheck,Whid,DebitCreditCard,GiftCard) values ('Paypal','CreditCard','False','False','False','False','False','1','" + yy.ToString() + "','False','False')", con);
        con.Open();
        cmd2g.ExecuteNonQuery();
        con.Close();
        SqlCommand cmd2g1 = new SqlCommand("Insert into Storepaymentmethod(Paypal,Craditcard,Cheque,Credit,Creditcart_offline,Do_Co_RealTime,Cash,RetailCheck,Whid,DebitCreditCard,GiftCard) values ('False','CreditCard','Cheque','False','False','False','Cash','1','" + yy.ToString() + "','False','False')", con);
        con.Open();
        cmd2g1.ExecuteNonQuery();
        con.Close();
        SqlCommand cmdpayaccid = new SqlCommand("Insert into PaymentMethodAccountTbl(PaypalAccId,CreditCardAccId,ChequeAccId,CreditAccId,CreditCardoffAccId,DcCrAccId,CashAccId,Whid,DebitCreditAccId,GiftCardAccId) values ('1001','1001','1001','1001','1001','1000','1000','" + yy.ToString() + "','1000','3300')", con);
        con.Open();
        cmdpayaccid.ExecuteNonQuery();
        con.Close();

        string stctm = "SELECT * from ClassTypeMaster";
        SqlCommand cmdctm = new SqlCommand(stctm, con);
        DataTable dtctm = new DataTable();
        SqlDataAdapter adpctm = new SqlDataAdapter(cmdctm);
        adpctm.Fill(dtctm);
        for (int i = 0; i < dtctm.Rows.Count; i++)
        {

            SqlCommand mycmd = new SqlCommand("Insertclasstypemaster", con);
            mycmd.CommandType = CommandType.StoredProcedure;
            mycmd.Parameters.AddWithValue("@classtypeid", dtctm.Rows[i]["ClassTypeId"].ToString());
            mycmd.Parameters.AddWithValue("@displayname", dtctm.Rows[i]["ClassType"].ToString());
            mycmd.Parameters.AddWithValue("@description", dtctm.Rows[i]["Description"].ToString());
            mycmd.Parameters.AddWithValue("@status", dtctm.Rows[i]["Status"].ToString());
            mycmd.Parameters.AddWithValue("@cid", Session["comid"]);
            mycmd.Parameters.AddWithValue("@whid", yy.ToString());
            con.Open();
            mycmd.ExecuteNonQuery();
            con.Close();
        }

        string stctmn = "SELECT ClassMaster.*,ClassTypeCompanyMaster.id as ctid from ClassMaster inner join ClassTypeCompanyMaster on ClassTypeCompanyMaster.classtypeid=ClassMaster.ClassTypeId  where  ClassTypeCompanyMaster.Whid='" + yy.ToString() + "'";
        SqlCommand cmdctmn = new SqlCommand(stctmn, con);
        DataTable dtctmn = new DataTable();
        SqlDataAdapter adpctmn = new SqlDataAdapter(cmdctmn);
        adpctmn.Fill(dtctmn);
        for (int i = 0; i < dtctmn.Rows.Count; i++)
        {
            SqlCommand cmctm = new SqlCommand("Insert into ClassCompanyMaster(classid,displayname,description,active,cid,classtypecompanymasterid,whid) values ('" + dtctmn.Rows[i]["ClassId"].ToString() + "','" + dtctmn.Rows[i]["ClassName"].ToString() + "','" + dtctmn.Rows[i]["Description"].ToString() + "','" + dtctmn.Rows[i]["Status"].ToString() + "','" + Session["comid"] + "','" + dtctmn.Rows[i]["ctid"].ToString() + "','" + yy.ToString() + "')", con);
            con.Open();
            cmctm.ExecuteNonQuery();
            con.Close();
        }

        string stctmnn = "SELECT GroupMaster.*,ClassCompanyMaster.id as ccmid from GroupMaster inner join ClassCompanyMaster on ClassCompanyMaster.classid=GroupMaster.ClassId  where ClassCompanyMaster.Whid='" + yy.ToString() + "'";
        SqlCommand cmdctmnn = new SqlCommand(stctmnn, con);
        DataTable dtctmnn = new DataTable();
        SqlDataAdapter adpctmnn = new SqlDataAdapter(cmdctmnn);
        adpctmnn.Fill(dtctmnn);
        for (int i = 0; i < dtctmnn.Rows.Count; i++)
        {
            SqlCommand cmctm = new SqlCommand("Insert into GroupCompanyMaster(groupid,groupdisplayname,description,active,cid,classcompanymasterid,whid) values ('" + dtctmnn.Rows[i]["GroupId"].ToString() + "','" + dtctmnn.Rows[i]["GroupName"].ToString() + "','" + dtctmnn.Rows[i]["Description"].ToString() + "','" + dtctmnn.Rows[i]["Status"].ToString() + "','" + Session["comid"] + "','" + dtctmnn.Rows[i]["ccmid"].ToString() + "','" + yy.ToString() + "')", con);
            con.Open();
            cmctm.ExecuteNonQuery();
            con.Close();
        }






        SqlCommand cmd5 = new SqlCommand("Insert into AccountMaster(AccountId,ClassId,GroupId,AccountName,Description,Balance,Date,InventoryFlag,BalanceOfLastYear,DateTimeOfLastUpdatedBalance,Status,compid,Whid) values ('30000','5','15','admin','admin','0', '2011-12-31','False','0','2011-12-31','True','" + Session["comid"] + "','" + yy.ToString() + "')", con);
        con.Open();
        cmd5.ExecuteNonQuery();
        con.Close();
        SqlCommand cmdcash = new SqlCommand("Insert into AccountMaster(AccountId,ClassId,GroupId,AccountName,Description,Balance,Date,InventoryFlag,BalanceOfLastYear,DateTimeOfLastUpdatedBalance,Status,compid,Whid) values ('1000','1','1','Cash','admin','0', '" + DateTime.Now.ToShortDateString() + "','False','0','" + DateTime.Now.ToShortDateString() + "','True','" + Session["comid"] + "','" + yy.ToString() + "')", con);
        con.Open();
        cmdcash.ExecuteNonQuery();
        con.Close();

        SqlCommand cmdPaypal = new SqlCommand("Insert into AccountMaster(AccountId,ClassId,GroupId,AccountName,Description,Balance,Date,InventoryFlag,BalanceOfLastYear,DateTimeOfLastUpdatedBalance,Status,compid,Whid) values ('1001','1','1','Paypal','Paypal','0', '" + DateTime.Now.ToShortDateString() + "','False','0','" + DateTime.Now.ToShortDateString() + "','True','" + Session["comid"] + "','" + yy.ToString() + "')", con);
        con.Open();
        cmdPaypal.ExecuteNonQuery();
        con.Close();

        SqlCommand cmdcap = new SqlCommand("Insert into AccountMaster(AccountId,ClassId,GroupId,AccountName,Description,Balance,Date,InventoryFlag,BalanceOfLastYear,DateTimeOfLastUpdatedBalance,Status,compid,Whid) values ('4500','7','24','Capital','admin','0', '" + DateTime.Now.ToShortDateString() + "','False','0','" + DateTime.Now.ToShortDateString() + "','True','" + Session["comid"] + "','" + yy.ToString() + "')", con);
        con.Open();
        cmdcap.ExecuteNonQuery();
        con.Close();
        SqlCommand cmdsale = new SqlCommand("Insert into AccountMaster(AccountId,ClassId,GroupId,AccountName,Description,Balance,Date,InventoryFlag,BalanceOfLastYear,DateTimeOfLastUpdatedBalance,Status,compid,Whid) values ('5000','26','33','Sales','admin','0', '" + DateTime.Now.ToShortDateString() + "','False','0','" + DateTime.Now.ToShortDateString() + "','True','" + Session["comid"] + "','" + yy.ToString() + "')", con);
        con.Open();
        cmdsale.ExecuteNonQuery();
        con.Close();
        //SqlCommand cmdgen = new SqlCommand("Insert into AccountMaster(AccountId,ClassId,GroupId,AccountName,Description,Balance,Date,InventoryFlag,BalanceOfLastYear,DateTimeOfLastUpdatedBalance,Status,compid,Whid) values ('8001','13','38','Purchase Tax1','admin','0', '" + DateTime.Now.ToShortDateString() + "','False','0','" + DateTime.Now.ToShortDateString() + "','True','" + Session["comid"] + "','" + yy.ToString() + "')", con);
        //con.Open();
        //cmdgen.ExecuteNonQuery();
        //con.Close();
        //SqlCommand cmdgen2 = new SqlCommand("Insert into AccountMaster(AccountId,ClassId,GroupId,AccountName,Description,Balance,Date,InventoryFlag,BalanceOfLastYear,DateTimeOfLastUpdatedBalance,Status,compid,Whid) values ('8002','13','38','Purchase Tax2','admin','0', '" + DateTime.Now.ToShortDateString() + "','False','0','" + DateTime.Now.ToShortDateString() + "','True','" + Session["comid"] + "','" + yy.ToString() + "')", con);
        //con.Open();
        //cmdgen2.ExecuteNonQuery();
        //con.Close();

        SqlCommand cmdgen = new SqlCommand("Insert into AccountMaster(AccountId,ClassId,GroupId,AccountName,Description,Balance,Date,InventoryFlag,BalanceOfLastYear,DateTimeOfLastUpdatedBalance,Status,compid,Whid) values ('9900','7','52','Dividend on Equity Share','admin','0', '" + DateTime.Now.ToShortDateString() + "','False','0','" + DateTime.Now.ToShortDateString() + "','True','" + Session["comid"] + "','" + yy.ToString() + "')", con);

        con.Open();

        cmdgen.ExecuteNonQuery();
        con.Close();

        SqlCommand cmdgen2 = new SqlCommand("Insert into AccountMaster(AccountId,ClassId,GroupId,AccountName,Description,Balance,Date,InventoryFlag,BalanceOfLastYear,DateTimeOfLastUpdatedBalance,Status,compid,Whid) values ('9901','7','52','Dividend on Preference Share','admin','0', '" + DateTime.Now.ToShortDateString() + "','False','0','" + DateTime.Now.ToShortDateString() + "','True','" + Session["comid"] + "','" + yy.ToString() + "')", con);

        con.Open();

        cmdgen2.ExecuteNonQuery();
        con.Close();

        SqlCommand cmdgenpadevi = new SqlCommand("Insert into AccountMaster(AccountId,ClassId,GroupId,AccountName,Description,Balance,Date,InventoryFlag,BalanceOfLastYear,DateTimeOfLastUpdatedBalance,Status,compid,Whid) values ('3300','5','20','Dividend Payable on Equity Share','admin','0', '" + DateTime.Now.ToShortDateString() + "','False','0','" + DateTime.Now.ToShortDateString() + "','True','" + Session["comid"] + "','" + yy.ToString() + "')", con);

        con.Open();

        cmdgenpadevi.ExecuteNonQuery();
        con.Close();

        SqlCommand cmdgen2paydi = new SqlCommand("Insert into AccountMaster(AccountId,ClassId,GroupId,AccountName,Description,Balance,Date,InventoryFlag,BalanceOfLastYear,DateTimeOfLastUpdatedBalance,Status,compid,Whid) values ('3301','5','20','Dividend Payable on Preference Share','admin','0', '" + DateTime.Now.ToShortDateString() + "','False','0','" + DateTime.Now.ToShortDateString() + "','True','" + Session["comid"] + "','" + yy.ToString() + "')", con);

        con.Open();

        cmdgen2paydi.ExecuteNonQuery();
        con.Close();

        SqlCommand cmdgen3 = new SqlCommand("Insert into AccountMaster(AccountId,ClassId,GroupId,AccountName,Description,Balance,Date,InventoryFlag,BalanceOfLastYear,DateTimeOfLastUpdatedBalance,Status,compid,Whid) values ('8003','13','38','Cost of goods sold','admin','0', '" + DateTime.Now.ToShortDateString() + "','False','0','" + DateTime.Now.ToShortDateString() + "','True','" + Session["comid"] + "','" + yy.ToString() + "')", con);
        con.Open();
        cmdgen3.ExecuteNonQuery();
        con.Close();
        SqlCommand cmdsales = new SqlCommand("Insert into AccountMaster(AccountId,ClassId,GroupId,AccountName,Description,Balance,Date,InventoryFlag,BalanceOfLastYear,DateTimeOfLastUpdatedBalance,Status,compid,Whid) values ('6003','13','35','Sales Man1','admin','0', '" + DateTime.Now.ToShortDateString() + "','False','0','" + DateTime.Now.ToShortDateString() + "','True','" + Session["comid"] + "','" + yy.ToString() + "')", con);
        con.Open();
        cmdsales.ExecuteNonQuery();
        con.Close();
        SqlCommand cmdsales04 = new SqlCommand("Insert into AccountMaster(AccountId,ClassId,GroupId,AccountName,Description,Balance,Date,InventoryFlag,BalanceOfLastYear,DateTimeOfLastUpdatedBalance,Status,compid,Whid) values ('6004','13','35','Sales Man2','admin','0', '" + DateTime.Now.ToShortDateString() + "','False','0','" + DateTime.Now.ToShortDateString() + "','True','" + Session["comid"] + "','" + yy.ToString() + "')", con);
        con.Open();
        cmdsales04.ExecuteNonQuery();
        con.Close();
        SqlCommand cmdsales00 = new SqlCommand("Insert into AccountMaster(AccountId,ClassId,GroupId,AccountName,Description,Balance,Date,InventoryFlag,BalanceOfLastYear,DateTimeOfLastUpdatedBalance,Status,compid,Whid) values ('6000','13','35','Sales Man3','admin','0', '" + DateTime.Now.ToShortDateString() + "','False','0','" + DateTime.Now.ToShortDateString() + "','True','" + Session["comid"] + "','" + yy.ToString() + "')", con);
        con.Open();
        cmdsales00.ExecuteNonQuery();
        con.Close();
        SqlCommand cmdsales01 = new SqlCommand("Insert into AccountMaster(AccountId,ClassId,GroupId,AccountName,Description,Balance,Date,InventoryFlag,BalanceOfLastYear,DateTimeOfLastUpdatedBalance,Status,compid,Whid) values ('6001','13','35','Sales Man4','admin','0', '" + DateTime.Now.ToShortDateString() + "','False','0','" + DateTime.Now.ToShortDateString() + "','True','" + Session["comid"] + "','" + yy.ToString() + "')", con);
        con.Open();
        cmdsales01.ExecuteNonQuery();
        con.Close();
        SqlCommand cmdsales8004 = new SqlCommand("Insert into AccountMaster(AccountId,ClassId,GroupId,AccountName,Description,Balance,Date,InventoryFlag,BalanceOfLastYear,DateTimeOfLastUpdatedBalance,Status,compid,Whid) values ('8004','13','38','Shipping Charges paid','admin','0', '" + DateTime.Now.ToShortDateString() + "','False','0','" + DateTime.Now.ToShortDateString() + "','True','" + Session["comid"] + "','" + yy.ToString() + "')", con);
        con.Open();
        cmdsales8004.ExecuteNonQuery();
        con.Close();
        SqlCommand cmdsales8000 = new SqlCommand("Insert into AccountMaster(AccountId,ClassId,GroupId,AccountName,Description,Balance,Date,InventoryFlag,BalanceOfLastYear,DateTimeOfLastUpdatedBalance,Status,compid,Whid) values ('8000','1','3','Inventory','admin','0', '" + DateTime.Now.ToShortDateString() + "','False','0','" + DateTime.Now.ToShortDateString() + "','True','" + Session["comid"] + "','" + yy.ToString() + "')", con);
        con.Open();
        cmdsales8000.ExecuteNonQuery();
        con.Close();
        SqlCommand cmds5001 = new SqlCommand("Insert into AccountMaster(AccountId,ClassId,GroupId,AccountName,Description,Balance,Date,InventoryFlag,BalanceOfLastYear,DateTimeOfLastUpdatedBalance,Status,compid,Whid) values ('5001','26','33','Over All Handing Charges Collected','admin','0', '" + DateTime.Now.ToShortDateString() + "','False','0','" + DateTime.Now.ToShortDateString() + "','True','" + Session["comid"] + "','" + yy.ToString() + "')", con);
        con.Open();
        cmds5001.ExecuteNonQuery();
        con.Close();
        SqlCommand cmds5002 = new SqlCommand("Insert into AccountMaster(AccountId,ClassId,GroupId,AccountName,Description,Balance,Date,InventoryFlag,BalanceOfLastYear,DateTimeOfLastUpdatedBalance,Status,compid,Whid) values ('5002','26','33','Over all Shipping Charges Collected','admin','0', '" + DateTime.Now.ToShortDateString() + "','False','0','" + DateTime.Now.ToShortDateString() + "','True','" + Session["comid"] + "','" + yy.ToString() + "')", con);
        con.Open();
        cmds5002.ExecuteNonQuery();
        con.Close();
        SqlCommand cmds9000 = new SqlCommand("Insert into AccountMaster(AccountId,ClassId,GroupId,AccountName,Description,Balance,Date,InventoryFlag,BalanceOfLastYear,DateTimeOfLastUpdatedBalance,Status,compid,Whid) values ('8010','13','38','factory o/h','admin','0', '" + DateTime.Now.ToShortDateString() + "','False','0','" + DateTime.Now.ToShortDateString() + "','True','" + Session["comid"] + "','" + yy.ToString() + "')", con);
        con.Open();
        cmds9000.ExecuteNonQuery();
        con.Close();
        SqlCommand cmds4700 = new SqlCommand("Insert into AccountMaster(AccountId,ClassId,GroupId,AccountName,Description,Balance,Date,InventoryFlag,BalanceOfLastYear,DateTimeOfLastUpdatedBalance,Status,compid,Whid) values ('4700','7','51','Retained earnings','admin','0', '" + DateTime.Now.ToShortDateString() + "','False','0','" + DateTime.Now.ToShortDateString() + "','True','" + Session["comid"] + "','" + yy.ToString() + "')", con);
        con.Open();
        cmds4700.ExecuteNonQuery();
        con.Close();
        SqlCommand cmdsper = new SqlCommand("Insert into AccountMaster(AccountId,ClassId,GroupId,AccountName,Description,Balance,Date,InventoryFlag,BalanceOfLastYear,DateTimeOfLastUpdatedBalance,Status,compid,Whid) values ('8005','13','38','Purchases A/c','admin','0', '" + DateTime.Now.ToShortDateString() + "','False','0','" + DateTime.Now.ToShortDateString() + "','True','" + Session["comid"] + "','" + yy.ToString() + "')", con);
        con.Open();
        cmdsper.ExecuteNonQuery();
        con.Close();



        SqlCommand cmdspertp = new SqlCommand("Insert into AccountMaster(AccountId,ClassId,GroupId,AccountName,Description,Balance,Date,InventoryFlag,BalanceOfLastYear,DateTimeOfLastUpdatedBalance,Status,compid,Whid) values ('8006','13','38','Production Wages','admin','0', '" + DateTime.Now.ToShortDateString() + "','False','0','" + DateTime.Now.ToShortDateString() + "','True','" + Session["comid"] + "','" + yy.ToString() + "')", con);
        con.Open();
        cmdspertp.ExecuteNonQuery();
        con.Close();

        SqlCommand cmdspertpov = new SqlCommand("Insert into AccountMaster(AccountId,ClassId,GroupId,AccountName,Description,Balance,Date,InventoryFlag,BalanceOfLastYear,DateTimeOfLastUpdatedBalance,Status,compid,Whid) values ('8007','13','38','Production Wages-Overtime','admin','0', '" + DateTime.Now.ToShortDateString() + "','False','0','" + DateTime.Now.ToShortDateString() + "','True','" + Session["comid"] + "','" + yy.ToString() + "')", con);
        con.Open();
        cmdspertpov.ExecuteNonQuery();
        con.Close();
        SqlCommand cmdspertpovAs = new SqlCommand("Insert into AccountMaster(AccountId,ClassId,GroupId,AccountName,Description,Balance,Date,InventoryFlag,BalanceOfLastYear,DateTimeOfLastUpdatedBalance,Status,compid,Whid) values ('8008','13','38','Reduction in Inventory on A/c of Abnormal Loss','admin','0', '" + DateTime.Now.ToShortDateString() + "','False','0','" + DateTime.Now.ToShortDateString() + "','True','" + Session["comid"] + "','" + yy.ToString() + "')", con);

        con.Open();

        cmdspertpovAs.ExecuteNonQuery();
        con.Close();
        SqlCommand cmdgainlossacc = new SqlCommand("Insert into AccountMaster(AccountId,ClassId,GroupId,AccountName,Description,Balance,Date,InventoryFlag,BalanceOfLastYear,DateTimeOfLastUpdatedBalance,Status,compid,Whid) values ('5500','13','34','Abnormal Inventory Loss','admin','0', '" + DateTime.Now.ToShortDateString() + "','False','0','" + DateTime.Now.ToShortDateString() + "','True','" + Session["comid"] + "','" + yy.ToString() + "')", con);

        con.Open();

        cmdgainlossacc.ExecuteNonQuery();
        con.Close();

        SqlCommand cmdsofficesalary = new SqlCommand("Insert into AccountMaster(AccountId,ClassId,GroupId,AccountName,Description,Balance,Date,InventoryFlag,BalanceOfLastYear,DateTimeOfLastUpdatedBalance,Status,compid,Whid) values ('7000','13','36','Office Salary','admin','0', '" + DateTime.Now.ToShortDateString() + "','False','0','" + DateTime.Now.ToShortDateString() + "','True','" + Session["comid"] + "','" + yy.ToString() + "')", con);
        con.Open();
        cmdsofficesalary.ExecuteNonQuery();
        con.Close();

        SqlCommand cmdsofficesalaryov = new SqlCommand("Insert into AccountMaster(AccountId,ClassId,GroupId,AccountName,Description,Balance,Date,InventoryFlag,BalanceOfLastYear,DateTimeOfLastUpdatedBalance,Status,compid,Whid) values ('7001','13','36','Office Salary-Overtime','admin','0', '" + DateTime.Now.ToShortDateString() + "','False','0','" + DateTime.Now.ToShortDateString() + "','True','" + Session["comid"] + "','" + yy.ToString() + "')", con);
        con.Open();
        cmdsofficesalaryov.ExecuteNonQuery();
        con.Close();


        SqlCommand cmdsalesstaffsalary = new SqlCommand("Insert into AccountMaster(AccountId,ClassId,GroupId,AccountName,Description,Balance,Date,InventoryFlag,BalanceOfLastYear,DateTimeOfLastUpdatedBalance,Status,compid,Whid) values ('6005','13','35','Sales Staff Salary','admin','0', '" + DateTime.Now.ToShortDateString() + "','False','0','" + DateTime.Now.ToShortDateString() + "','True','" + Session["comid"] + "','" + yy.ToString() + "')", con);
        con.Open();
        cmdsalesstaffsalary.ExecuteNonQuery();
        con.Close();

        SqlCommand cmdsalesstaffsalaryov = new SqlCommand("Insert into AccountMaster(AccountId,ClassId,GroupId,AccountName,Description,Balance,Date,InventoryFlag,BalanceOfLastYear,DateTimeOfLastUpdatedBalance,Status,compid,Whid) values ('6006','13','35','Sales Staff Salary-Overtime','admin','0', '" + DateTime.Now.ToShortDateString() + "','False','0','" + DateTime.Now.ToShortDateString() + "','True','" + Session["comid"] + "','" + yy.ToString() + "')", con);
        con.Open();
        cmdsalesstaffsalaryov.ExecuteNonQuery();
        con.Close();


        SqlCommand cmssalescom = new SqlCommand("Insert into AccountMaster(AccountId,ClassId,GroupId,AccountName,Description,Balance,Date,InventoryFlag,BalanceOfLastYear,DateTimeOfLastUpdatedBalance,Status,compid,Whid) values ('6007','13','35','Sales Comission','admin','0', '" + DateTime.Now.ToShortDateString() + "','False','0','" + DateTime.Now.ToShortDateString() + "','True','" + Session["comid"] + "','" + yy.ToString() + "')", con);
        con.Open();
        cmssalescom.ExecuteNonQuery();
        con.Close();

        SqlCommand cmsgiftcard = new SqlCommand("Insert into AccountMaster(AccountId,ClassId,GroupId,AccountName,Description,Balance,Date,InventoryFlag,BalanceOfLastYear,DateTimeOfLastUpdatedBalance,Status,compid,Whid) values ('3300','5','20','Gift Cards Sold but not used','admin','0', '" + DateTime.Now.ToShortDateString() + "','False','0','" + DateTime.Now.ToShortDateString() + "','True','" + Session["comid"] + "','" + yy.ToString() + "')", con);

        con.Open();
        cmsgiftcard.ExecuteNonQuery();
        con.Close();

        SqlCommand cmotherevn = new SqlCommand("Insert into AccountMaster(AccountId,ClassId,GroupId,AccountName,Description,Balance,Date,InventoryFlag,BalanceOfLastYear,DateTimeOfLastUpdatedBalance,Status,compid,Whid) values ('9200','26','44','Discount or Rebate Received','admin','0', '" + DateTime.Now.ToShortDateString() + "','False','0','" + DateTime.Now.ToShortDateString() + "','True','" + Session["comid"] + "','" + yy.ToString() + "')", con);

        con.Open();

        cmotherevn.ExecuteNonQuery();
        con.Close();
        //////
        SqlCommand cmdOFFINV = new SqlCommand("Insert into AccountMaster(AccountId,ClassId,GroupId,AccountName,Description,Balance,Date,InventoryFlag,BalanceOfLastYear,DateTimeOfLastUpdatedBalance,Status,compid,Whid) values ('7002','13','36','Office Salary Allotted to Inventory(Work in progress) A/c','admin','0', '" + DateTime.Now.ToShortDateString() + "','False','0','" + DateTime.Now.ToShortDateString() + "','True','" + Session["comid"] + "','" + yy.ToString() + "')", con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmdOFFINV.ExecuteNonQuery();
        con.Close();

        SqlCommand cmpvinv = new SqlCommand("Insert into AccountMaster(AccountId,ClassId,GroupId,AccountName,Description,Balance,Date,InventoryFlag,BalanceOfLastYear,DateTimeOfLastUpdatedBalance,Status,compid,Whid) values ('8009','13','38','Production Wages Allotted to Inventory(Work in progress) A/c','admin','0', '" + DateTime.Now.ToShortDateString() + "','False','0','" + DateTime.Now.ToShortDateString() + "','True','" + Session["comid"] + "','" + yy.ToString() + "')", con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmpvinv.ExecuteNonQuery();
        con.Close();

        SqlCommand cmdlossss = new SqlCommand("Insert into AccountMaster(AccountId,ClassId,GroupId,AccountName,Description,Balance,Date,InventoryFlag,BalanceOfLastYear,DateTimeOfLastUpdatedBalance,Status,compid,Whid) values ('5501','13','34','Loss from Reducing Inventory to Lower of Cost or Market Value','admin','0', '" + DateTime.Now.ToShortDateString() + "','False','0','" + DateTime.Now.ToShortDateString() + "','True','" + Session["comid"] + "','" + yy.ToString() + "')", con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmdlossss.ExecuteNonQuery();
        con.Close();

        SqlCommand cmdrecgain = new SqlCommand("Insert into AccountMaster(AccountId,ClassId,GroupId,AccountName,Description,Balance,Date,InventoryFlag,BalanceOfLastYear,DateTimeOfLastUpdatedBalance,Status,compid,Whid) values ('5502','13','34','Recovery against Loss from Reducing Inventory to Lower of Cost or Market Value','admin','0', '" + DateTime.Now.ToShortDateString() + "','False','0','" + DateTime.Now.ToShortDateString() + "','True','" + Session["comid"] + "','" + yy.ToString() + "')", con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmdrecgain.ExecuteNonQuery();
        con.Close();

        SqlCommand cmdinvalllo = new SqlCommand("Insert into AccountMaster(AccountId,ClassId,GroupId,AccountName,Description,Balance,Date,InventoryFlag,BalanceOfLastYear,DateTimeOfLastUpdatedBalance,Status,compid,Whid) values ('11000','1','3','Allowance to reduce inventory to Lower of Cost or Market Value','admin','0', '" + DateTime.Now.ToShortDateString() + "','False','0','" + DateTime.Now.ToShortDateString() + "','True','" + Session["comid"] + "','" + yy.ToString() + "')", con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmdinvalllo.ExecuteNonQuery();
        con.Close();

        SqlCommand cmabngam = new SqlCommand("Insert into AccountMaster(AccountId,ClassId,GroupId,AccountName,Description,Balance,Date,InventoryFlag,BalanceOfLastYear,DateTimeOfLastUpdatedBalance,Status,compid,Whid) values ('9201','26','44','Abnormal Inventory Gain','admin','0', '" + DateTime.Now.ToShortDateString() + "','False','0','" + DateTime.Now.ToShortDateString() + "','True','" + Session["comid"] + "','" + yy.ToString() + "')", con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmabngam.ExecuteNonQuery();
        con.Close();
        SqlCommand cmdoverprod = new SqlCommand("Insert into AccountMaster(AccountId,ClassId,GroupId,AccountName,Description,Balance,Date,InventoryFlag,BalanceOfLastYear,DateTimeOfLastUpdatedBalance,Status,compid,Whid) values ('8011','13','38','Production Expenses','admin','0', '" + DateTime.Now.ToShortDateString() + "','False','0','" + DateTime.Now.ToShortDateString() + "','True','" + Session["comid"] + "','" + yy.ToString() + "')", con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmdoverprod.ExecuteNonQuery();
        con.Close();


        int Currentyearorg = 0;
        int Currentyear = 0;
        int yem1 = Convert.ToInt32(ddlacyear.SelectedItem.Text) - 1;
        string yname1 = yem1.ToString() + " TO " + ddlacyear.SelectedItem.Text;
        SqlCommand cmdd = new SqlCommand("Insert into ReportPeriod(Name,StartDate,EndDate,FistStartDate,Active,Compid,Whid)Values('" + yname1 + "','" + Convert.ToDateTime(lblfysdate.Text).AddYears(-1).ToString() + "','" + Convert.ToDateTime(lblfyedate.Text).AddYears(-1).ToString() + "','" + Convert.ToDateTime(lblfysdate.Text) + "','0','" + Session["comid"] + "','" + Convert.ToInt32(yy) + "') ", con);

        con.Open();
        cmdd.ExecuteNonQuery();
        con.Close();

        SqlCommand cmsa11 = new SqlCommand("SELECT max(Report_Period_Id) from ReportPeriod where Compid='" + Session["comid"] + "' and Whid='" + Convert.ToInt32(yy) + "'", con);
        con.Open();
        object xde21 = cmsa11.ExecuteScalar();
        con.Close();
        string str5t1 = "select Id from AccountMaster where Whid='" + yy.ToString() + "'";
        SqlCommand cmd32t1 = new SqlCommand(str5t1, con);
        SqlDataAdapter adp15t1 = new SqlDataAdapter(cmd32t1);
        DataTable dtlogin14t1 = new DataTable();
        adp15t1.Fill(dtlogin14t1);
        foreach (DataRow dr in dtlogin14t1.Rows)
        {
            SqlCommand cmdtr13 = new SqlCommand("Insert into AccountBalance(AccountMasterId,Balance,Report_Period_Id)Values('" + dr["Id"] + "','0','" + xde21 + "') ", con);

            con.Open();
            cmdtr13.ExecuteNonQuery();
            con.Close();
        }

        string strdate1 = "";
        string lastdate = "";
        string strdate2 = "";
        string lasttwodata = "";

        strdate1 = ddlacyear.SelectedItem.Text.ToString();
        lastdate = strdate1.Substring(2);

        int yem = Convert.ToInt32(ddlacyear.SelectedItem.Text) + 1;
        string yname = ddlacyear.SelectedItem.Text + " TO " + yem.ToString();

        strdate2 = yem.ToString();

        lasttwodata = strdate2.Substring(2);

        SqlCommand cmdtr2 = new SqlCommand("Insert into ReportPeriod(Name,StartDate,EndDate,FistStartDate,Active,Compid,Whid)Values('" + yname + "','" + Convert.ToDateTime(lblfysdate.Text) + "','" + Convert.ToDateTime(lblfyedate.Text) + "','" + Convert.ToDateTime(lblfysdate.Text) + "','1','" + Session["comid"] + "','" + Convert.ToInt32(yy) + "') ", con);

        con.Open();
        cmdtr2.ExecuteNonQuery();
        con.Close();
        SqlCommand cmsa1 = new SqlCommand("SELECT max(Report_Period_Id) from ReportPeriod where Compid='" + Session["comid"] + "' and Whid='" + Convert.ToInt32(yy) + "'", con);
        con.Open();
        object xde2 = cmsa1.ExecuteScalar();
        con.Close();

        string inse = "Insert into Report_period_confirm(Report_Period_Id,conform)values('" + xde2 + "','1')";
        SqlCommand cmd = new SqlCommand(inse, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd.ExecuteNonQuery();
        con.Close();

        foreach (DataRow dr in dtlogin14t1.Rows)
        {
            SqlCommand cmdtr13t = new SqlCommand("Insert into AccountBalance(AccountMasterId,Balance,Report_Period_Id)Values('" + dr["Id"] + "','0','" + xde2 + "') ", con);

            con.Open();
            cmdtr13t.ExecuteNonQuery();
            con.Close();
        }
        Currentyear = Convert.ToInt32(ddlacyear.SelectedItem.Text) + 1;
        Currentyearorg = DateTime.Now.Year + 1;
        int k = 1;
        for (int i = Currentyear; i < Currentyearorg; i++)
        {
            DateTime fye = Convert.ToDateTime(lblfysdate.Text).AddYears(k);
            DateTime eye = Convert.ToDateTime(lblfyedate.Text).AddYears(k);
            int sy = Convert.ToInt32(ddlacyear.SelectedItem.Text) + k;
            int ey = Convert.ToInt32(ddlacyear.SelectedItem.Text) + (k + 1);
            string nam = sy.ToString() + " TO " + ey.ToString();
            SqlCommand cmdged = new SqlCommand("Insert into ReportPeriod(Name,StartDate,EndDate,FistStartDate,Active,Compid,Whid)Values('" + nam + "','" + fye + "','" + eye + "','" + fye + "','0','" + Session["Comid"] + "','" + yy.ToString() + "') ", con);

            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdged.ExecuteNonQuery();
            con.Close();
            k += 1;
            SqlCommand cmsa = new SqlCommand("SELECT max(Report_Period_Id) from ReportPeriod where Compid='" + Session["comid"] + "' and Whid='" + Convert.ToInt32(yy) + "'", con);
            con.Open();
            object rig = cmsa.ExecuteScalar();
            con.Close();
            foreach (DataRow dr in dtlogin14t1.Rows)
            {
                SqlCommand cmdtr13t = new SqlCommand("Insert into AccountBalance(AccountMasterId,Balance,Report_Period_Id)Values('" + dr["Id"] + "','0','" + rig + "') ", con);

                con.Open();
                cmdtr13t.ExecuteNonQuery();
                con.Close();
            }
        }


        string strrempw = "select AccountId from AccountMaster where AccountName='Production Wages' and Whid='" + yy.ToString() + "'";
        SqlCommand cmrempv = new SqlCommand(strrempw, con);
        SqlDataAdapter adrempv = new SqlDataAdapter(cmrempv);
        DataTable dtrempv = new DataTable();
        adrempv.Fill(dtrempv);
        if (dtrempv.Rows.Count > 0)
        {
            String strrem = "Insert Into RemunerationMaster (RemunerationName,AccountMasterId,Whid,Compid,Commitiontyperem,IsPensionIncome,IsInsurableIncome)values('Production Wages','" + dtrempv.Rows[0]["AccountId"] + "','" + yy.ToString() + "','" + Session["comid"] + "','" + false + "','" + false + "','" + false + "')";
            SqlCommand cmremm = new SqlCommand(strrem, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmremm.ExecuteNonQuery();
            con.Close();
        }

        string strrselst = "select AccountId from AccountMaster where AccountName='Sales Staff Salary' and Whid='" + yy.ToString() + "'";
        SqlCommand cmdselsta = new SqlCommand(strrselst, con);
        SqlDataAdapter adselsta = new SqlDataAdapter(cmdselsta);
        DataTable dtselstaff = new DataTable();
        adselsta.Fill(dtselstaff);
        if (dtselstaff.Rows.Count > 0)
        {
            String strrem = "Insert Into RemunerationMaster (RemunerationName,AccountMasterId,Whid,Compid,Commitiontyperem,IsPensionIncome,IsInsurableIncome)values('Sales Staff Salary','" + dtselstaff.Rows[0]["AccountId"] + "','" + yy.ToString() + "','" + Session["comid"] + "','" + false + "','" + false + "','" + false + "')";
            SqlCommand cmremm = new SqlCommand(strrem, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmremm.ExecuteNonQuery();
            con.Close();
        }
        string stoffisal = "select AccountId from AccountMaster where AccountName='Office Salary' and Whid='" + yy.ToString() + "'";
        SqlCommand cmofsal = new SqlCommand(stoffisal, con);
        SqlDataAdapter adoffsel = new SqlDataAdapter(cmofsal);
        DataTable dtoffsel = new DataTable();
        adoffsel.Fill(dtoffsel);
        if (dtoffsel.Rows.Count > 0)
        {
            String strrem = "Insert Into RemunerationMaster (RemunerationName,AccountMasterId,Whid,Compid,Commitiontyperem,IsPensionIncome,IsInsurableIncome)values('Office Salary','" + dtoffsel.Rows[0]["AccountId"] + "','" + yy.ToString() + "','" + Session["comid"] + "','" + false + "','" + false + "','" + false + "')";
            SqlCommand cmremm = new SqlCommand(strrem, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmremm.ExecuteNonQuery();
            con.Close();
        }
        string RemunrationsalaryId = "";
        string strremsalid = "select Id from RemunerationMaster where RemunerationName='Office Salary' and Whid='" + yy.ToString() + "'";
        SqlCommand cmdremsid = new SqlCommand(strremsalid, con);
        SqlDataAdapter adremsalid = new SqlDataAdapter(cmdremsid);
        DataTable dtreselid = new DataTable();
        adremsalid.Fill(dtreselid);
        if (dtreselid.Rows.Count > 0)
        {
            RemunrationsalaryId = Convert.ToString(dtreselid.Rows[0]["Id"]);
        }






        string str4 = "select PartyTypeId from PartytTypeMaster where PartType='Admin' and compid='" + Session["comid"] + "' ";
        SqlCommand cmd31 = new SqlCommand(str4, con);
        SqlDataAdapter adp14 = new SqlDataAdapter(cmd31);
        DataTable dtlogin13 = new DataTable();
        adp14.Fill(dtlogin13);
        if (dtlogin13.Rows.Count > 0)
        {
            string partytypeid = dtlogin13.Rows[0]["PartyTypeId"].ToString();

            SqlCommand cmd1r0 = new SqlCommand("Insert into Party_master (Account,Compname,Contactperson,Address,City,State,Country,Website,GSTno," +
                                " Incometaxno,Email,Phoneno,DataopID,PartyTypeId,AssignedAccountManagerId,AssignedRecevingDepartmentInchargeId,AssignedPurchaseDepartmentInchargeId," +
                                " AssignedShippingDepartmentInchargeId,AssignedSalesDepartmentIncharge,StatusMasterId,Fax,AccountnameID,id,AccountBalanceLimitId,Whid,Zipcode,PartyTypeCategoryNo) " +
                                " values ('30000','" + companyname + "','" + eplazacontactpersonname + "','" + eplazaaddress + "','" + eplazacityid + "','" + eplazastateid + "','" + eplazacountryid + "','','1','1'," +
                                " '" + eplazaemail + "','" + eplazaphone + "','1','" + partytypeid + "','1','1','1','1','1','42','" + eplazafax + "','1','" + Session["comid"] + "','0','" + yy.ToString() + "','" + eplazapincode + "','1')", con);
            con.Open();
            cmd1r0.ExecuteNonQuery();
            con.Close();


            string str9 = "select PartyID from Party_master where Whid='" + yy.ToString() + "'";
            SqlCommand cmd35 = new SqlCommand(str9, con);
            SqlDataAdapter adp19 = new SqlDataAdapter(cmd35);
            DataTable dtlogin19 = new DataTable();
            adp19.Fill(dtlogin19);

            if (dtlogin19.Rows.Count > 0)
            {
                string partyid = dtlogin19.Rows[0]["PartyID"].ToString();



                SqlCommand cmd6s = new SqlCommand("Insert into User_master(Name,Address,City,State,Country,Phoneno,EmailID,Username,Department,Accesslevel,PartyID,DesigantionMasterId,photo,Active,Extention,zipcode) values " +
                               " ('" + companyname + "','" + eplazaaddress + "','" + eplazacityid + "','" + eplazastateid + "','" + eplazacountryid + "','" + eplazaphone + "','" + eplazaemail + "', " +
                               " 'Admin','" + dept + "','1','" + partyid + "','3','','" + 1 + "','','" + eplazapincode + "')", con);

                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd6s.ExecuteNonQuery();
                con.Close();

                string strmaxemptype = "select * from EmployeeType where CID='" + Session["comid"] + "' order by EmployeeTypeName  ";
                SqlCommand cmdmaxemptype = new SqlCommand(strmaxemptype, con);
                SqlDataAdapter adpmaxemptype = new SqlDataAdapter(cmdmaxemptype);
                DataTable dsmaxemptype = new DataTable();
                adpmaxemptype.Fill(dsmaxemptype);


                ViewState["EmpTypeIdMax"] = dsmaxemptype.Rows[0]["EmployeeTypeId"].ToString();




                string strempadd = "Insert into EmployeeMaster(PartyID,DeptID,DesignationMasterId,StatusMasterId,EmployeeTypeId, " +
                                         " DateOfJoin,Address,CountryId,StateId,City,ContactNo,Email,AccountId,AccountNo,EmployeeName,Whid,Description,Active) values('" + partyid + "','" + dept + "', " +
                                          " '" + desig + "','0','" + ViewState["EmpTypeIdMax"].ToString() + "','" + System.DateTime.Now.ToShortDateString() + "','" + eplazaaddress + "', " +
                                         " '" + eplazacountryid + "','" + eplazastateid + "','" + eplazacityid + "','" + eplazaphone + "','" + eplazaemail + "','" + Convert.ToString(dtlogin14t1.Rows[0]["Id"]) + "','30000','Admin','" + yy.ToString() + "','Admin','1')";
                SqlCommand cmdempadd = new SqlCommand(strempadd, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdempadd.ExecuteNonQuery();
                con.Close();

                string str121maxemp = "select max(EmployeeMasterID) as EmployeeMasterID from EmployeeMaster";
                SqlCommand cmd121maxemp = new SqlCommand(str121maxemp, con);
                SqlDataAdapter adp121maxuser = new SqlDataAdapter(cmd121maxemp);
                DataTable ds121maxuser = new DataTable();
                adp121maxuser.Fill(ds121maxuser);

                ViewState["MailIDD"] = ds121maxuser.Rows[0]["EmployeeMasterID"].ToString();

                if (chkmasteremail.Checked == true)
                {
                    if (txtusmail.Text != "")
                    {

                        SqlCommand cmdinout = new SqlCommand("insert into inoutcompanyemail values('" + TextOutGoingMailServer.Text + "','" + txtusmail.Text + "','" + TextEmailMasterLoginPassword.Text + "','" + TextIncomingMailServer.Text + "','" + txtusmail.Text + "','" + TextEmailMasterLoginPassword.Text + "','" + Convert.ToDateTime("01/01/1980") + "','" + 0 + "','" + yy.ToString() + "','" + 1 + "','" + 0 + "','" + txtusmail.Text + "','','')", con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmdinout.ExecuteNonQuery();
                        con.Close();

                        SqlDataAdapter dainout = new SqlDataAdapter("select max(ID) as ID from inoutcompanyemail", con);
                        DataTable dtinout = new DataTable();
                        dainout.Fill(dtinout);

                        if (dtinout.Rows.Count > 0)
                        {
                            SqlCommand cmdinout1 = new SqlCommand("insert into CompanyEmailAssignAccessRights values('" + Convert.ToString(dtinout.Rows[0]["ID"]) + "','" + desig + "','" + ViewState["MailIDD"] + "','1','" + Session["Comid"].ToString() + "','1','1')", con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmdinout1.ExecuteNonQuery();
                            con.Close();

                            SqlCommand cmdinout2 = new SqlCommand("insert into EmailSignatureMaster values('" + Convert.ToString(dtinout.Rows[0]["ID"]) + "','" + TextSiteName.Text + "')", con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmdinout2.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }

                string strpayrolldd = "Insert into EmployeePayrollMaster(EmpId,PaymentMethodId,PaymentReceivedNameOf,PaypalId,PaymentEmailId,DirectDepositBankName,DirectDepositBankCode,DirectDepositBankBranchName,DirectDepositBankBranchNumber,DirectDepositTransitNumber,DirectDepositAccountHolderName,DirectDepositBankAccountType,DirectDepositBankAccountNumber,DirectDepositBankBranchAddress,DirectDepositBankBranchcity,DirectDepositBankBranchstate,DirectDepositBankBranchcountry,DirectDepositBankBranchzipcode,DirectDepositBankIFCNumber,DirectDepositBankSwiftNumber,DirectDepositBankEmployeeEmail,Whid,Compid,PayPeriodMasterId,LastName,FirstName,Intials,EmployeeNo,DateOfBirth,SocialSecurityNo,EmployeePaidAsPerDesignation)values('" + ds121maxuser.Rows[0]["EmployeeMasterID"].ToString() + "',2,'',0,'','','','','','','','','','','','','','','','','','" + yy.ToString() + "','" + Session["comid"] + "',0,'Ad','mi','n','','','',0)";
                SqlCommand cmdpayrolldd = new SqlCommand(strpayrolldd, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdpayrolldd.ExecuteNonQuery();
                con.Close();


                string statem = "select EmployeeMasterID from EmployeeMaster where Whid='" + yy.ToString() + "'";
                SqlCommand cmatem = new SqlCommand(statem, con);
                SqlDataAdapter adatem = new SqlDataAdapter(cmatem);
                DataTable dtemat = new DataTable();
                adatem.Fill(dtemat);

                if (dtemat.Rows.Count > 0)
                {
                    string strattev = "Insert Into AttandanceRule(StoreId,AcceptableInTimeDeviationMinutes,AcceptableOutTimeDeviationMinutes,Considerwithinrangedeviationasstandardtime,ShowtheFieldtorecordthereasonfordeviation,Showreasonafterinstance,TakeapprovaloftheseniorEmployee,SeniorEmployeeID,Takeapprovalafterinstance,Donotallowemployeetomakeentryinregister,Donotallowemployeeinstance,Overtimepara,overtimeruleno,CompId,Generalapprovalrule,Considerinoutrangeintance,rulegreatertime,rulegreatertimeinstance,Maxuserhours" +
                    ",nooverflunc,withoutverification,allowedmultipleentry,generatenotificemail,notificemailallowedhours,notificmailsuper,notificmailattendanceadmin,blockattendance,blackattendanceminit,op2graceperiod)values " +
                                           " ('" + yy.ToString() + "','5','5','1','1','5','1','" + dtemat.Rows[0]["EmployeeMasterID"] + "','5','1','7','0','1','" + Session["comid"] + "','0','3','1','0','6'" +
                    ",'1','1','0','0','0','1','1','1','20','1')";
                    SqlCommand cmdatts = new SqlCommand(strattev, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }

                    cmdatts.ExecuteNonQuery();
                    con.Close();

                    string strtyr2 = "select UserID  from User_master inner join Party_master on Party_master.PartyID=User_master.PartyID where Party_master.Whid='" + yy.ToString() + "'";
                    SqlCommand cmd1j = new SqlCommand(strtyr2, con);
                    SqlDataAdapter adp13j = new SqlDataAdapter(cmd1j);
                    DataTable dtlogin11j = new DataTable();
                    adp13j.Fill(dtlogin11j);
                    if (dtlogin11j.Rows.Count > 0)
                    {
                        string userid = dtlogin11j.Rows[0]["UserID"].ToString();

                        SqlCommand cmd45 = new SqlCommand("Insert into Login_master(UserID,username,password,department,accesslevel,deptid,accessid) values ('" + userid + "','0','000','" + dept + "','1','3','1')", con);
                        con.Open();
                        cmd45.ExecuteNonQuery();
                        con.Close();

                        string str3 = "select Role_id from RoleMaster where Role_name='Admin' and compid='" + Session["comid"] + "' ";
                        SqlCommand cmd34 = new SqlCommand(str3, con);
                        SqlDataAdapter adp12 = new SqlDataAdapter(cmd34);
                        DataTable dtlogin12 = new DataTable();
                        adp12.Fill(dtlogin12);

                        if (dtlogin12.Rows.Count > 0)
                        {
                            string roleid = dtlogin12.Rows[0]["Role_id"].ToString();


                            SqlCommand cmd8 = new SqlCommand("Insert into User_Role (User_id,Role_id,ActiveDeactive) values ('" + userid + "','" + roleid + "','true ')", con);
                            con.Open();
                            cmd8.ExecuteNonQuery();
                            con.Close();

                        }

                    }
                    for (int i = 0; i < gridAccess.Rows.Count; i++)
                    {
                        Label lblEmployeeMasterId = (Label)gridAccess.Rows[i].FindControl("lblEmployeeMasterId");
                        Label lblwid = (Label)gridAccess.Rows[i].FindControl("lblwid");
                        CheckBox chk = (CheckBox)(gridAccess.Rows[i].FindControl("ChkAess"));
                        int ch = 0;
                        if (chk.Checked)
                        {
                            ch = 1;
                        }
                        else
                        {
                            ch = 0;
                        }
                        string str = "Insert  into EmployeeWarehouseRights (EmployeeId,Whid,AccessAllowed)values('" + lblEmployeeMasterId.Text + "','" + yy.ToString() + "','" + ch + "')";
                        SqlCommand cmd1 = new SqlCommand(str, con);
                        con.Open();
                        cmd1.ExecuteNonQuery();
                        con.Close();
                    }


                    //object WhtimeEST = inWHTimeZone(yy.ToString(), "271", Session["comid"].ToString());
                    //object WhtimePST = inWHTimeZone(yy.ToString(), "280", Session["comid"].ToString());
                    //object WhtimeNST = inWHTimeZone(yy.ToString(), "264", Session["comid"].ToString());
                    //object WhtimeAST = inWHTimeZone(yy.ToString(), "265", Session["comid"].ToString());
                    //object WhtimeCST = inWHTimeZone(yy.ToString(), "273", Session["comid"].ToString());
                    //object WhtimeMST = inWHTimeZone(yy.ToString(), "279", Session["comid"].ToString());
                    object ddzone = inWHTimeZone(yy.ToString(), ddlwztimezone.SelectedValue, Session["comid"].ToString());
                    //  object BatchEST = inBatchMaster("Reg 9 to 17:30 EST", yy.ToString(), "1", WhtimeEST.ToString(), Session["comid"].ToString(), "0");



                    string StrInsertemp = "Insert Into EmployeeSalaryMaster (EmployeeId,Remuneration_Id,IsPercent_IsAmount,Amount,PayablePer_PeriodMasterId,EffectiveStartDate,EffectiveEndDate,Whid,compid) values ('" + dtemat.Rows[0]["EmployeeMasterID"] + "','" + RemunrationsalaryId + "','0','0','2','" + DateTime.Now.ToShortDateString() + "','" + DateTime.Now.AddYears(3).ToShortDateString() + "','" + yy.ToString() + "','" + Session["comid"] + "') ";
                    SqlCommand cmemppayrolsal = new SqlCommand(StrInsertemp, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }

                    cmemppayrolsal.ExecuteNonQuery();
                    con.Close();

                    string strtempavg = "Insert Into EmployeeAvgSalaryMaster (EmployeeId,AvgRate,PayPeriodMasterId,Whid,compid) values ('" + dtemat.Rows[0]["EmployeeMasterID"] + "','0','0','" + yy.ToString() + "','" + Session["comid"] + "') ";
                    SqlCommand cmdavg = new SqlCommand(strtempavg, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }

                    cmdavg.ExecuteNonQuery();
                    con.Close();

                    object BatchMST = inBatchMaster("Reg 07:00 to 15:00", yy.ToString(), "1", ddzone.ToString(), Session["comid"].ToString(), "0");
                    object BatchTimingMST = inBatchTimingMaster(BatchMST.ToString(), "07:00", "15:00", "12:00", "12:30", "33", "1", yy.ToString(), Session["comid"].ToString(), "07:30");
                    BatchWorhingDay(yy.ToString(), BatchTimingMST.ToString(), BatchMST.ToString(), Session["comid"].ToString());

                    object BatchEST = inBatchMaster("Reg 09:00 to 17:30", yy.ToString(), "1", ddzone.ToString(), Session["comid"].ToString(), "0");
                    object BatchTimingEST = inBatchTimingMaster(BatchEST.ToString(), "09:00", "17:30", "12:00", "12:30", "33", "1", yy.ToString(), Session["comid"].ToString(), "08:00");
                    BatchWorhingDay(yy.ToString(), BatchTimingEST.ToString(), BatchEST.ToString(), Session["comid"].ToString());

                    object BatchPST = inBatchMaster("Reg 09:00 to 17:00", yy.ToString(), "1", ddzone.ToString(), Session["comid"].ToString(), "0");
                    object BatchTimingPST = inBatchTimingMaster(BatchPST.ToString(), "09:00", "17:00", "12:00", "12:30", "33", "1", yy.ToString(), Session["comid"].ToString(), "07:30");
                    BatchWorhingDay(yy.ToString(), BatchTimingPST.ToString(), BatchPST.ToString(), Session["comid"].ToString());


                    object BatchNST = inBatchMaster("Reg 08:30 to 17:00", yy.ToString(), "1", ddzone.ToString(), Session["comid"].ToString(), "0");
                    object BatchTimingNST = inBatchTimingMaster(BatchNST.ToString(), "08:30", "17:00", "12:00", "12:30", "33", "1", yy.ToString(), Session["comid"].ToString(), "08:00");
                    BatchWorhingDay(yy.ToString(), BatchTimingNST.ToString(), BatchNST.ToString(), Session["comid"].ToString());

                    object BatchAST = inBatchMaster("Reg 08:00 to 16:30", yy.ToString(), "1", ddzone.ToString(), Session["comid"].ToString(), "0");
                    object BatchTimingAST = inBatchTimingMaster(BatchAST.ToString(), "08:00", "16:30", "12:00", "12:30", "33", "1", yy.ToString(), Session["comid"].ToString(), "08:00");
                    BatchWorhingDay(yy.ToString(), BatchTimingAST.ToString(), BatchAST.ToString(), Session["comid"].ToString());

                    object BatchCST = inBatchMaster("Reg 07:00 to 15:30", yy.ToString(), "1", ddzone.ToString(), Session["comid"].ToString(), "0");
                    object BatchTimingCST = inBatchTimingMaster(BatchCST.ToString(), "07:00", "15:30", "12:00", "12:30", "33", "1", yy.ToString(), Session["comid"].ToString(), "08:00");
                    BatchWorhingDay(yy.ToString(), BatchTimingCST.ToString(), BatchCST.ToString(), Session["comid"].ToString());




                    string strbatchempassign = "Insert  into EmployeeBatchMaster (Whid,Employeeid,Batchmasterid)values('" + yy.ToString() + "','" + dtemat.Rows[0]["EmployeeMasterID"] + "','" + BatchNST.ToString() + "')";
                    SqlCommand cmdbatchempassign = new SqlCommand(strbatchempassign, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }

                    cmdbatchempassign.ExecuteNonQuery();
                    con.Close();
                }

            }

            DocumentCls1 clsDocument = new DocumentCls1();

            Int32 rst = clsDocument.InsertDocumentMainType("General", yy.ToString());
            string strtydmain = "select DocumentMainTypeId  from DocumentMainType where Whid='" + yy.ToString() + "'";
            SqlCommand cmddmain = new SqlCommand(strtydmain, con);
            SqlDataAdapter adpdmain = new SqlDataAdapter(cmddmain);
            DataTable dtdmain = new DataTable();
            adpdmain.Fill(dtdmain);
            if (dtdmain.Rows.Count > 0)
            {
                Int32 rst1 = clsDocument.InsertDocumentSubType(Convert.ToInt32(dtdmain.Rows[0]["DocumentMainTypeId"]), "General");
                string strtydmainsub = "select DocumentSubTypeId  from DocumentSubType where DocumentMainTypeId='" + Convert.ToInt32(dtdmain.Rows[0]["DocumentMainTypeId"]) + "'";
                SqlCommand cmddmainsub = new SqlCommand(strtydmainsub, con);
                SqlDataAdapter adpdmainsub = new SqlDataAdapter(cmddmainsub);
                DataTable dtdmainsub = new DataTable();
                adpdmainsub.Fill(dtdmainsub);
                if (dtdmainsub.Rows.Count > 0)
                {
                    Int32 rst2 = clsDocument.InsertDocumentType1(Convert.ToInt32(dtdmainsub.Rows[0]["DocumentSubTypeId"]), "General");
                }
            }
            Int32 mainyearcabinet = clsDocument.InsertDocumentMainType("Accounting", yy.ToString());

            string maincabinetmax = "select Max(DocumentMainTypeId) as DocumentMainTypeId  from DocumentMainType ";
            SqlCommand cmdmaincabinetmax = new SqlCommand(maincabinetmax, con);
            SqlDataAdapter adpmaincabinetmax = new SqlDataAdapter(cmdmaincabinetmax);
            DataTable dtmaincabinetmax = new DataTable();
            adpmaincabinetmax.Fill(dtmaincabinetmax);

            if (dtmaincabinetmax.Rows.Count > 0)
            {
                Int32 mainyeardrawer = clsDocument.InsertDocumentSubType(Convert.ToInt32(dtmaincabinetmax.Rows[0]["DocumentMainTypeId"]), "" + ddlacyear.SelectedItem.Text + " TO " + yem.ToString() + "");

                string maindrawermax = "select Max(DocumentSubTypeId) as DocumentSubTypeId  from DocumentSubType ";
                SqlCommand just = new SqlCommand(maindrawermax, con);
                SqlDataAdapter adpmaindrawermax = new SqlDataAdapter(just);
                DataTable dtmaindrawermax = new DataTable();
                adpmaindrawermax.Fill(dtmaindrawermax);

                if (dtmaindrawermax.Rows.Count > 0)
                {
                    Int32 mainyearfolder = clsDocument.InsertDocumentType1(Convert.ToInt32(dtmaindrawermax.Rows[0]["DocumentSubTypeId"]), "Purchase Invoice" + " " + lastdate + "-" + lasttwodata + "");
                    Int32 mainyearfolder1 = clsDocument.InsertDocumentType1(Convert.ToInt32(dtmaindrawermax.Rows[0]["DocumentSubTypeId"]), "Cash Payments" + " " + lastdate + "-" + lasttwodata + "");
                    Int32 mainyearfolder2 = clsDocument.InsertDocumentType1(Convert.ToInt32(dtmaindrawermax.Rows[0]["DocumentSubTypeId"]), "Cash Receipts" + " " + lastdate + "-" + lasttwodata + "");
                    Int32 mainyearfolder3 = clsDocument.InsertDocumentType1(Convert.ToInt32(dtmaindrawermax.Rows[0]["DocumentSubTypeId"]), "Journal Entry" + " " + lastdate + "-" + lasttwodata + "");
                    Int32 mainyearfolder4 = clsDocument.InsertDocumentType1(Convert.ToInt32(dtmaindrawermax.Rows[0]["DocumentSubTypeId"]), "Sales Invoice" + " " + lastdate + "-" + lasttwodata + "");
                    Int32 mainyearfolder5 = clsDocument.InsertDocumentType1(Convert.ToInt32(dtmaindrawermax.Rows[0]["DocumentSubTypeId"]), "Retail Sales Invoice" + " " + lastdate + "-" + lasttwodata + "");
                    Int32 mainyearfolder6 = clsDocument.InsertDocumentType1(Convert.ToInt32(dtmaindrawermax.Rows[0]["DocumentSubTypeId"]), "Debit-Credit Note" + " " + lastdate + "-" + lasttwodata + "");
                    Int32 mainyearfolder7 = clsDocument.InsertDocumentType1(Convert.ToInt32(dtmaindrawermax.Rows[0]["DocumentSubTypeId"]), "Quick Cash Entry" + " " + lastdate + "-" + lasttwodata + "");

                }

            }
        }
    }
    protected void ImageButton6_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1222.Hide();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "editview")
        {
            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);

            ViewState["sid"] = GridView1.SelectedIndex;
            string finalcompid23 = Session["Comid"].ToString();
            pnlaccy.Visible = false;
            //lblacchead.Visible = false;
            SqlCommand cmdedit = new SqlCommand(" SELECT   WareHouseMaster.OnlineStoreSetup,  CompanyWebsitMaster.WHId, CompanyWebsitMaster.CompanyWebsiteMasterId, CompanyWebsitMaster.CompanyId, CompanyWebsitMaster.Sitename, " +
                    "  CompanyWebsitMaster.SiteUrl, CompanyWebsitMaster.Active, WareHouseMaster.Name, WareHouseMaster.CurrencyId, WareHouseMaster.Status, " +
                    "  CurrencyMaster.CurrencyName, CompanyWebsitMaster.SupportEmail, CompanyWebsitMaster.WebMasterEmail, CompanyWebsitMaster.AdminEmail, " +
                    "  CompanyWebsitMaster.IncomingMailServer, CompanyWebsitMaster.OutGoingMailServer, CompanyWebsitMaster.EmailMasterLoginPassword, " +
                    "  CompanyWebsitMaster.EmailSentDisplayName, CompanyWebsitMaster.MasterEmailId, CompanyWebsitMaster.UNameForSendingMail, " +
                     " CompanyMaster.CompanyName, CompanyAddressMaster.WebSite " +
                    " FROM         CompanyMaster LEFT OUTER JOIN " +
                    "  CompanyAddressMaster ON CompanyMaster.CompanyId = CompanyAddressMaster.CompanyMasterId RIGHT OUTER JOIN " +
                    "  CompanyWebsitMaster ON CompanyMaster.CompanyId = CompanyWebsitMaster.CompanyId LEFT OUTER JOIN " +
                    "  WareHouseMaster LEFT OUTER JOIN " +
                   "   CurrencyMaster ON WareHouseMaster.CurrencyId = CurrencyMaster.CurrencyId ON CompanyWebsitMaster.WHId = WareHouseMaster.WareHouseId  where CompanyWebsitMaster.CompanyWebsiteMasterId='" + ViewState["sid"] + "' and CompanyMaster.Compid='" + finalcompid23 + "' and WareHouseMaster.comid= '" + finalcompid23 + "'", con);


            SqlDataAdapter dtpedit = new SqlDataAdapter(cmdedit);
            DataTable dtedit = new DataTable();
            dtpedit.Fill(dtedit);
            if (dtedit.Rows.Count > 0)
            {
                //  Controlenable(false);
                ImgBtnSubmit.Visible = false;
                imgbtnupdate.Visible = true;
                //imgBtnCancelMainUpdate.Visible = false;
                //    imgbtnedit.Visible = true;
                ddlCompanyName.Text = dtedit.Rows[0]["CompanyName"].ToString();
                ddlcurrency.SelectedIndex = ddlcurrency.Items.IndexOf(ddlcurrency.Items.FindByValue(dtedit.Rows[0]["CurrencyId"].ToString()));
                TextSiteName.Text = dtedit.Rows[0]["Name"].ToString();
                lblsiteurl.Text = dtedit.Rows[0]["WebSite"].ToString();
                lblshop.Text = "/Shoppingcart/default.aspx?WHid=" + dtedit.Rows[0]["WHId"].ToString();
                if (Convert.ToString(dtedit.Rows[0]["MasterEmailId"]) != "")
                {

                    txtmemailname.Text = dtedit.Rows[0]["EmailSentDisplayName"].ToString();
                    txtmemailid.Text = dtedit.Rows[0]["MasterEmailId"].ToString();
                    txtusmail.Text = dtedit.Rows[0]["UNameForSendingMail"].ToString();
                    TextBox2.Text = dtedit.Rows[0]["UNameForSendingMail"].ToString();
                    TextEmailMasterLoginPassword.Attributes.Add("Value", dtedit.Rows[0]["EmailMasterLoginPassword"].ToString());
                    TextBox3.Attributes.Add("Value", dtedit.Rows[0]["EmailMasterLoginPassword"].ToString());
                    //string strqa = TextEmailMasterLoginPassword.Text;
                    //TextEmailMasterLoginPassword.Attributes.Add("Value", strqa);
                    TextIncomingMailServer.Text = dtedit.Rows[0]["IncomingMailServer"].ToString();
                    TextAdminEmail.Text = dtedit.Rows[0]["AdminEmail"].ToString();
                    TextSupportEmail.Text = dtedit.Rows[0]["SupportEmail"].ToString();
                    TextWebMasterEmail.Text = dtedit.Rows[0]["WebMasterEmail"].ToString();
                    TextOutGoingMailServer.Text = dtedit.Rows[0]["OutGoingMailServer"].ToString();
                    pnlmasteremail.Visible = true;
                    chkmasteremail.Checked = true;
                }
                else
                {
                    chkmasteremail.Checked = false;
                    pnlmasteremail.Visible = false;
                }

                lblurrr.Visible = true;
                lblhttp.Visible = true;
                lblsiteurl.Visible = true;
                lblshop.Visible = true;

                ViewState["wid"] = dtedit.Rows[0]["WHId"].ToString();
                string bus = "select Top(1) * from WHTimeZone where compid = '" + Session["comid"] + "' and WHID = '" + ViewState["wid"] + "' order by Id desc";
                SqlCommand cmdbus = new SqlCommand(bus, con);
                SqlDataAdapter adpbus = new SqlDataAdapter(cmdbus);
                DataTable dtbus = new DataTable();
                adpbus.Fill(dtbus);
                if (dtbus.Rows.Count > 0)
                {
                    ddlwztimezone.SelectedIndex = ddlwztimezone.Items.IndexOf(ddlwztimezone.Items.FindByValue(dtbus.Rows[0]["TimeZone"].ToString()));
                }
                if (dtedit.Rows[0]["Active"].ToString() == "True")
                {
                    ddlstatus.SelectedIndex = ddlstatus.Items.IndexOf(ddlstatus.Items.FindByText("Active"));
                    //chkstatus.Checked = true;
                }
                else
                {
                    ddlstatus.SelectedIndex = ddlstatus.Items.IndexOf(ddlstatus.Items.FindByText("Inactive"));
                    //chkstatus.Checked = false;
                }
                if (dtedit.Rows[0]["OnlineStoreSetup"].ToString() == "True")
                {
                    chkbusisetup.Checked = true;
                }
                else
                {
                    chkbusisetup.Checked = false;
                }
                //TextSiteName.Text = dtedit.Rows[0]["Sitename"].ToString();
                //TextSiteName.Text = dtedit.Rows[0]["Sitename"].ToString();
                if (Request.Url.Host.ToString() == "itimekeeper.com")
                {
                    pnlcurrency.Visible = false;
                    Panel1.Visible = false;
                    pnlaccy.Visible = false;
                    GridView1.Columns[4].Visible = false;
                }
                else if (Request.Url.Host.ToString() == "itimekeeper.us")
                {
                    pnlcurrency.Visible = false;
                    Panel1.Visible = false;
                    pnlaccy.Visible = false;
                    GridView1.Columns[4].Visible = false;
                }
                btnadd.Visible = false;
                pnladd.Visible = true;
                lbladd.Text = "Edit Business";
            }

        }
        if (e.CommandName == "View")
        {
            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);

            ViewState["sid"] = GridView1.SelectedIndex;
            string finalcompid23 = Session["Comid"].ToString();
            pnlaccy.Visible = false;
            //lblacchead.Visible = false;
            SqlCommand cmdedit = new SqlCommand(" SELECT   WareHouseMaster.OnlineStoreSetup,  CompanyWebsitMaster.WHId, CompanyWebsitMaster.CompanyWebsiteMasterId, CompanyWebsitMaster.CompanyId, CompanyWebsitMaster.Sitename, " +
                    "  CompanyWebsitMaster.SiteUrl, CompanyWebsitMaster.Active, WareHouseMaster.Name, WareHouseMaster.CurrencyId, WareHouseMaster.Status, " +
                    "  CurrencyMaster.CurrencyName, CompanyWebsitMaster.SupportEmail, CompanyWebsitMaster.WebMasterEmail, CompanyWebsitMaster.AdminEmail, " +
                    "  CompanyWebsitMaster.IncomingMailServer, CompanyWebsitMaster.OutGoingMailServer, CompanyWebsitMaster.EmailMasterLoginPassword, " +
                    "  CompanyWebsitMaster.EmailSentDisplayName, CompanyWebsitMaster.MasterEmailId, CompanyWebsitMaster.UNameForSendingMail, " +
                     " CompanyMaster.CompanyName, CompanyAddressMaster.WebSite " +
                    " FROM         CompanyMaster LEFT OUTER JOIN " +
                    "  CompanyAddressMaster ON CompanyMaster.CompanyId = CompanyAddressMaster.CompanyMasterId RIGHT OUTER JOIN " +
                    "  CompanyWebsitMaster ON CompanyMaster.CompanyId = CompanyWebsitMaster.CompanyId LEFT OUTER JOIN " +
                    "  WareHouseMaster LEFT OUTER JOIN " +
                   "   CurrencyMaster ON WareHouseMaster.CurrencyId = CurrencyMaster.CurrencyId ON CompanyWebsitMaster.WHId = WareHouseMaster.WareHouseId  where CompanyWebsitMaster.CompanyWebsiteMasterId='" + ViewState["sid"] + "' and CompanyMaster.Compid='" + finalcompid23 + "' and WareHouseMaster.comid= '" + finalcompid23 + "'", con);


            SqlDataAdapter dtpedit = new SqlDataAdapter(cmdedit);
            DataTable dtedit = new DataTable();
            dtpedit.Fill(dtedit);
            if (dtedit.Rows.Count > 0)
            {
                Controlenable(false);
                ImgBtnSubmit.Visible = false;
                //imgbtnupdate.Visible = true;
                //imgBtnCancelMainUpdate.Visible = false;
                //    imgbtnedit.Visible = true;
                ddlCompanyName.Text = dtedit.Rows[0]["CompanyName"].ToString();
                ddlcurrency.SelectedIndex = ddlcurrency.Items.IndexOf(ddlcurrency.Items.FindByValue(dtedit.Rows[0]["CurrencyId"].ToString()));
                TextSiteName.Text = dtedit.Rows[0]["Name"].ToString();
                lblsiteurl.Text = dtedit.Rows[0]["WebSite"].ToString();
                lblshop.Text = "/Shoppingcart/default.aspx?WHid=" + dtedit.Rows[0]["WHId"].ToString();
                if (Convert.ToString(dtedit.Rows[0]["MasterEmailId"]) != "")
                {

                    txtmemailname.Text = dtedit.Rows[0]["EmailSentDisplayName"].ToString();
                    txtmemailid.Text = dtedit.Rows[0]["MasterEmailId"].ToString();
                    txtusmail.Text = dtedit.Rows[0]["UNameForSendingMail"].ToString();
                    TextBox2.Text = dtedit.Rows[0]["UNameForSendingMail"].ToString();
                    TextEmailMasterLoginPassword.Attributes.Add("Value", dtedit.Rows[0]["EmailMasterLoginPassword"].ToString());
                    TextBox3.Attributes.Add("Value", dtedit.Rows[0]["EmailMasterLoginPassword"].ToString());
                    //string strqa = TextEmailMasterLoginPassword.Text;
                    //TextEmailMasterLoginPassword.Attributes.Add("Value", strqa);
                    TextIncomingMailServer.Text = dtedit.Rows[0]["IncomingMailServer"].ToString();
                    TextAdminEmail.Text = dtedit.Rows[0]["AdminEmail"].ToString();
                    TextSupportEmail.Text = dtedit.Rows[0]["SupportEmail"].ToString();
                    TextWebMasterEmail.Text = dtedit.Rows[0]["WebMasterEmail"].ToString();
                    TextOutGoingMailServer.Text = dtedit.Rows[0]["OutGoingMailServer"].ToString();
                    pnlmasteremail.Visible = true;
                    chkmasteremail.Checked = true;
                }
                else
                {
                    chkmasteremail.Checked = false;
                    pnlmasteremail.Visible = false;
                }

                lblurrr.Visible = true;
                lblhttp.Visible = true;
                lblsiteurl.Visible = true;
                lblshop.Visible = true;

                ViewState["wid"] = dtedit.Rows[0]["WHId"].ToString();
                string bus = "select * from WHTimeZone where compid = '" + Session["comid"] + "' and WHID = '" + ViewState["wid"] + "'";
                SqlCommand cmdbus = new SqlCommand(bus, con);
                SqlDataAdapter adpbus = new SqlDataAdapter(cmdbus);
                DataTable dtbus = new DataTable();
                adpbus.Fill(dtbus);
                if (dtbus.Rows.Count > 0)
                {
                    ddlwztimezone.SelectedIndex = ddlwztimezone.Items.IndexOf(ddlwztimezone.Items.FindByValue(dtbus.Rows[0]["TimeZone"].ToString()));
                }
                if (dtedit.Rows[0]["Active"].ToString() == "True")
                {
                    ddlstatus.SelectedIndex = ddlstatus.Items.IndexOf(ddlstatus.Items.FindByText("Active"));
                    //chkstatus.Checked = true;
                }
                else
                {
                    ddlstatus.SelectedIndex = ddlstatus.Items.IndexOf(ddlstatus.Items.FindByText("Inactive"));
                    //chkstatus.Checked = false;
                }
                if (dtedit.Rows[0]["OnlineStoreSetup"].ToString() == "True")
                {
                    chkbusisetup.Checked = true;
                }
                else
                {
                    chkbusisetup.Checked = false;
                }
                //TextSiteName.Text = dtedit.Rows[0]["Sitename"].ToString();
                //TextSiteName.Text = dtedit.Rows[0]["Sitename"].ToString();
                if (Request.Url.Host.ToString() == "itimekeeper.com")
                {
                    pnlcurrency.Visible = false;
                    Panel1.Visible = false;
                    pnlaccy.Visible = false;
                    GridView1.Columns[4].Visible = false;
                }
                else if (Request.Url.Host.ToString() == "itimekeeper.us")
                {
                    pnlcurrency.Visible = false;
                    Panel1.Visible = false;
                    pnlaccy.Visible = false;
                    GridView1.Columns[4].Visible = false;
                }
                btnadd.Visible = false;
                pnladd.Visible = true;
                lbladd.Text = "View Business";
            }
        }
    }
    protected void Controlenable(bool t)
    {
        ddlCompanyName.Enabled = t;
        ddlcurrency.Enabled = t;
        TextSiteName.Enabled = t;
        lblsiteurl.Enabled = t;
        txtmemailname.Enabled = t;
        txtmemailid.Enabled = t;
        txtusmail.Enabled = t;
        TextEmailMasterLoginPassword.Enabled = t;
        TextIncomingMailServer.Enabled = t;
        TextAdminEmail.Enabled = t;
        TextSupportEmail.Enabled = t;
        TextWebMasterEmail.Enabled = t;
        TextOutGoingMailServer.Enabled = t;
        ddlstatus.Enabled = t;
        chkmasteremail.Enabled = t;
        chkad.Enabled = t;
        CheckBox1.Enabled = t;
        TextBox2.Enabled = t;
        TextBox3.Enabled = t;
        //chkstatus.Enabled = t;
        chkbusisetup.Enabled = t;
        ddlwztimezone.Enabled = t;
    }

    protected void ControlenableTEstmail(bool t)
    {

        //txtmemailname.Enabled = t;
        txtmemailid.Enabled = t;
        txtusmail.Enabled = t;
        TextEmailMasterLoginPassword.Enabled = t;

    }

    protected void imgbtnedit_Click(object sender, EventArgs e)
    {
        Controlenable(true);
        imgbtnedit.Visible = false;
        imgbtnupdate.Visible = true;
        //imgBtnCancelMainUpdate.Visible = true;

    }
    protected void imgbtnupdate_Click(object sender, EventArgs e)
    {
        string strid = ViewState["sid"].ToString();
        string finalcompid267 = Session["Comid"].ToString();

        if (strid != "")
        {//****Radhika Chnages
            // SqlCommand cmdwid = new SqlCommand("select WHId from CompanyWebsitMaster where CompanyWebsiteMasterId='" + strid + "'", con);
            //*************
            string finalcompid2 = Session["Comid"].ToString();
            SqlCommand cmdcheck = new SqlCommand("select Name from WarehouseMaster where WarehouseId<>'" + ViewState["wid"] + "' and Name ='" + TextSiteName.Text.Replace("'", "''") + "' and comid='" + finalcompid2 + "'", con);
            SqlDataAdapter dtpcheck = new SqlDataAdapter(cmdcheck);
            DataTable dtcheck = new DataTable();
            dtpcheck.Fill(dtcheck);
            if (dtcheck.Rows.Count > 0)
            {
                Label3.Text = "";
                Label3.Text = "Record already exists";
            }
            else
            {
                SqlCommand cmdwid = new SqlCommand("select WHId from CompanyWebsitMaster where CompanyWebsiteMasterId='" + strid + "' and  CompanyId=(select  CompanyId from CompanyMaster where  Compid ='" + finalcompid267 + "')", con);
                SqlDataAdapter dtpwid = new SqlDataAdapter(cmdwid);
                DataTable dtwid = new DataTable();
                dtpwid.Fill(dtwid);
                ViewState["whid"]=dtwid.Rows[0][0].ToString();
               
                if (dtwid.Rows.Count > 0)
                {
                    //  Radhika Chnages
                    //SqlCommand cmdwsta = new SqlCommand("Update WareHouseMaster set Status='" + chkstatus.Checked + "', Name='" + TextSiteName.Text + "' where WareHouseId='" + dtwid.Rows[0]["WHId"].ToString() + "'", con);
                    //************************
                    SqlCommand cmdwsta = new SqlCommand("Update WareHouseMaster set Status='" + ddlstatus.SelectedValue + "',CurrencyId='" + ddlcurrency.SelectedValue + "', Name='" + TextSiteName.Text.Replace("'", "''") + "',OnlineStoreSetup='" + chkbusisetup.Checked + "' where WareHouseId='" + dtwid.Rows[0]["WHId"].ToString() + "' and  comid='" + finalcompid267 + "'", con);

                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdwsta.ExecuteNonQuery();
                    con.Close();


                    //*********   Radhika Chnages
                    //string sst = "UPDATE    CompanyWebsitMaster " +
                    //            " SET   Sitename ='" + TextSiteName.Text + "', SiteUrl ='" + lblsiteurl.Text + "', SupportEmail ='" + TextSupportEmail.Text + "',EmailSentDisplayName='" + txtmemailname.Text + "',MasterEmailId='" + txtmemailid.Text + "',UNameForSendingMail='" + txtmemailname.Text + "', WebMasterEmail ='" + TextWebMasterEmail.Text + "', AdminEmail ='" + TextAdminEmail.Text + "', OutGoingMailServer ='" + TextOutGoingMailServer.Text + "', EmailMasterLoginPassword ='" + TextEmailMasterLoginPassword.Text + "',  " +
                    //              " IncomingMailServer = '" + TextIncomingMailServer.Text + "',Active='" + chkstatus.Checked + "' where CompanyWebsiteMasterId='" + strid + "'";

                    //*********************      


                    string sst = "UPDATE    CompanyWebsitMaster " +
                                      " SET   Sitename ='" + TextSiteName.Text.Replace("'", "''") + "', SiteUrl ='" + lblsiteurl.Text + "', SupportEmail ='" + TextSupportEmail.Text + "',EmailSentDisplayName='" + txtmemailname.Text + "',MasterEmailId='" + txtmemailid.Text + "',UNameForSendingMail='" + txtusmail.Text + "', WebMasterEmail ='" + TextWebMasterEmail.Text + "', AdminEmail ='" + TextAdminEmail.Text + "', OutGoingMailServer ='" + TextOutGoingMailServer.Text + "', EmailMasterLoginPassword ='" + TextEmailMasterLoginPassword.Text + "',  " +
                                        " IncomingMailServer = '" + TextIncomingMailServer.Text + "',Active='" + ddlstatus.SelectedValue + "' where CompanyWebsiteMasterId='" + strid + "' ";


                    SqlCommand cmd11 = new SqlCommand(sst, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd11.ExecuteNonQuery();
                    con.Close();


                    for (int i = 0; i < gridAccess.Rows.Count; i++)
                    {
                        CheckBox chka = (CheckBox)(gridAccess.Rows[i].FindControl("chka"));
                        CheckBox ChkAess = (CheckBox)(gridAccess.Rows[i].FindControl("ChkAess"));
                        Label lblEmployeeMasterId = (Label)gridAccess.Rows[i].FindControl("lblEmployeeMasterId");
                        string str = "";
                        if (chka.Checked == false)
                        {
                            if (ChkAess.Checked == true)
                            {
                                str = "Insert  into EmployeeWarehouseRights (EmployeeId,Whid,AccessAllowed)values('" + lblEmployeeMasterId.Text + "','" + dtwid.Rows[0]["WHId"].ToString() + "','" + ChkAess.Checked + "')";

                                SqlCommand cmd1 = new SqlCommand(str, con);
                                if (con.State.ToString() != "Open")
                                {
                                    con.Open();
                                }
                                cmd1.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                        else
                        {
                            //    if (chk.Checked == false)
                            //    {
                            str = "Update  EmployeeWarehouseRights Set AccessAllowed='" + ChkAess.Checked + "' where EmployeeId='" + lblEmployeeMasterId.Text + "' AND Whid='" + dtwid.Rows[0]["WHId"].ToString() + "'";

                            SqlCommand cmd1 = new SqlCommand(str, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmd1.ExecuteNonQuery();
                            con.Close();
                            // }

                        }

                    }
                    if (ddlwztimezone.SelectedIndex > 0)
                    {
                        string strupwz = "Update  WHTimeZone Set TimeZone='" + ddlwztimezone.SelectedValue + "' where WHID='" + dtwid.Rows[0]["WHId"].ToString() + "' and compid='" + Session["Comid"].ToString() + "'";

                        SqlCommand cmd1 = new SqlCommand(strupwz, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmd1.ExecuteNonQuery();
                        con.Close();
                    }
                    Label3.Visible = true;
                    pnlaccy.Visible = true;
                    //  lblacchead.Visible = true;
                    Label3.Text = "Record updated successfully";

                    imgbtnupdate.Visible = false;
                    //imgBtnCancelMainUpdate.Visible = false;
                    Controlenable(true);
                    imgbtnedit.Visible = false;
                    ImgBtnSubmit.Visible = true;
                    fillgriddata();


                    ddlcurrency.SelectedIndex = 0;
                    TextSiteName.Text = "";
                    lblsiteurl.Text = "";
                    txtmemailname.Text = "";
                    txtmemailid.Text = "";
                    txtusmail.Text = "";

                    TextEmailMasterLoginPassword.Attributes.Clear();
                    TextEmailMasterLoginPassword.Text = "";
                    TextIncomingMailServer.Text = "";
                    TextAdminEmail.Text = "";
                    TextSupportEmail.Text = "";
                    TextWebMasterEmail.Text = "";
                    TextOutGoingMailServer.Text = "";
                    ddlstatus.SelectedIndex = 0;
                    chkbusisetup.Checked = true;
                    lblurrr.Visible = false;
                    lblhttp.Visible = false;
                    lblsiteurl.Visible = false;
                    lblshop.Visible = false;
                    chkmasteremail.Checked = false;
                    pnlmasteremail.Visible = false;
                    pnladmin.Visible = false;
                    chkad.Checked = false;
                    CheckBox1.Checked = false;
                    pnlaccy.Visible = false;
                    //imgBtnCancelMainUpdate.Visible = false;
                    if (Request.Url.Host.ToString() == "itimekeeper.com")
                    {
                        pnlcurrency.Visible = false;
                        Panel1.Visible = false;
                        pnlaccy.Visible = false;
                        GridView1.Columns[4].Visible = false;
                    }
                    else if (Request.Url.Host.ToString() == "itimekeeper.us")
                    {
                        pnlcurrency.Visible = false;
                        Panel1.Visible = false;
                        pnlaccy.Visible = false;
                        GridView1.Columns[4].Visible = false;
                    }
                    btnadd.Visible = true;
                    pnladd.Visible = false;
                    lbladd.Text = "";
                    filltimezone();
                }
            }

        }

    }
    protected void imgBtnCancelMainUpdate_Click(object sender, EventArgs e)
    {
        imgbtnupdate.Visible = false;
        //imgBtnCancelMainUpdate.Visible = false;
        Controlenable(true);
        imgbtnedit.Visible = false;
        ImgBtnSubmit.Visible = true;
        fillgriddata();
        TextEmailMasterLoginPassword.Attributes.Clear();

        ddlcurrency.SelectedIndex = 0;
        TextSiteName.Text = "";
        lblsiteurl.Text = "";
        txtmemailname.Text = "";
        txtmemailid.Text = "";
        txtusmail.Text = "";
        TextEmailMasterLoginPassword.Text = "";
        TextIncomingMailServer.Text = "";
        TextAdminEmail.Text = "";
        TextSupportEmail.Text = "";
        TextWebMasterEmail.Text = "";
        TextOutGoingMailServer.Text = "";
        ddlstatus.SelectedIndex = 0;
        chkbusisetup.Checked = true;
        lblurrr.Visible = false;
        lblhttp.Visible = false;
        lblsiteurl.Visible = false;
        lblshop.Visible = false;
        chkmasteremail.Checked = false;
        pnlmasteremail.Visible = false;
        CheckBox1.Checked = false;
        chkad.Checked = false;
        pnladmin.Visible = false;
        pnlaccy.Visible = false;
        if (Request.Url.Host.ToString() == "itimekeeper.com")
        {
            pnlcurrency.Visible = false;
            Panel1.Visible = false;
            pnlaccy.Visible = false;
            GridView1.Columns[4].Visible = false;
        }
        else if (Request.Url.Host.ToString() == "itimekeeper.us")
        {
            pnlcurrency.Visible = false;
            Panel1.Visible = false;
            pnlaccy.Visible = false;
            GridView1.Columns[4].Visible = false;
        }
        btnadd.Visible = true;
        pnladd.Visible = false;
        lbladd.Text = "";
        TextBox3.Text = "";
        TextBox2.Text = "";
        filltimezone();
        //lblacchead.Visible = true;
        //imgBtnCancelMainUpdate.Visible = false;
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgriddata();
    }

    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
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
    protected void chkmasteremail_CheckedChanged(object sender, EventArgs e)
    {
        if (chkmasteremail.Checked == true)
        {
            pnlmasteremail.Visible = true;
        }
        else
        {
            pnlmasteremail.Visible = false;
        }
    }
    protected void ddlacday_SelectedIndexChanged(object sender, EventArgs e)
    {
        string se = "";
        if (ddlacmonth.SelectedIndex != -1)
        {
            se = ddlacmonth.SelectedItem.Value;
        }
        ddlacmonth.Items.Clear();
        if (Convert.ToInt32(ddlacday.SelectedItem.Text) <= 29)
        {
            ddlacmonth.Items.Insert(0, "January");
            ddlacmonth.Items[0].Value = "1";
            ddlacmonth.Items.Insert(1, "February");
            ddlacmonth.Items[1].Value = "2";
            ddlacmonth.Items.Insert(2, "March");
            ddlacmonth.Items[2].Value = "3";
            ddlacmonth.Items.Insert(3, "April");
            ddlacmonth.Items[3].Value = "4";
            ddlacmonth.Items.Insert(4, "May");
            ddlacmonth.Items[4].Value = "5";
            ddlacmonth.Items.Insert(5, "June");
            ddlacmonth.Items[5].Value = "6";
            ddlacmonth.Items.Insert(6, "July");
            ddlacmonth.Items[6].Value = "7";
            ddlacmonth.Items.Insert(7, "August");
            ddlacmonth.Items[7].Value = "8";
            ddlacmonth.Items.Insert(8, "September");
            ddlacmonth.Items[8].Value = "9";
            ddlacmonth.Items.Insert(9, "October");
            ddlacmonth.Items[9].Value = "10";
            ddlacmonth.Items.Insert(10, "November");
            ddlacmonth.Items[10].Value = "11";
            ddlacmonth.Items.Insert(11, "December");
            ddlacmonth.Items[11].Value = "12";
        }
        else if (Convert.ToInt32(ddlacday.SelectedItem.Text) <= 30)
        {
            ddlacmonth.Items.Insert(0, "January");
            ddlacmonth.Items[0].Value = "1";
            // ddlacmonth.Items.Insert(0, "2");
            ddlacmonth.Items.Insert(1, "March");
            ddlacmonth.Items[1].Value = "3";
            ddlacmonth.Items.Insert(2, "April");
            ddlacmonth.Items[2].Value = "4";
            ddlacmonth.Items.Insert(3, "May");
            ddlacmonth.Items[3].Value = "5";
            ddlacmonth.Items.Insert(4, "June");
            ddlacmonth.Items[4].Value = "6";
            ddlacmonth.Items.Insert(5, "July");
            ddlacmonth.Items[5].Value = "7";
            ddlacmonth.Items.Insert(6, "August");
            ddlacmonth.Items[6].Value = "8";
            ddlacmonth.Items.Insert(7, "September");
            ddlacmonth.Items[7].Value = "9";
            ddlacmonth.Items.Insert(8, "October");
            ddlacmonth.Items[8].Value = "10";
            ddlacmonth.Items.Insert(9, "November");
            ddlacmonth.Items[9].Value = "11";
            ddlacmonth.Items.Insert(10, "December");
            ddlacmonth.Items[10].Value = "12";

        }

        else if (Convert.ToInt32(ddlacday.SelectedItem.Text) <= 31)
        {
            ddlacmonth.Items.Insert(0, "January");
            ddlacmonth.Items[0].Value = "1";

            ddlacmonth.Items.Insert(1, "March");
            ddlacmonth.Items[1].Value = "3";


            ddlacmonth.Items.Insert(2, "May");
            ddlacmonth.Items[2].Value = "5";

            ddlacmonth.Items.Insert(3, "July");
            ddlacmonth.Items[3].Value = "7";
            ddlacmonth.Items.Insert(4, "August");
            ddlacmonth.Items[4].Value = "8";

            ddlacmonth.Items.Insert(5, "October");
            ddlacmonth.Items[5].Value = "10";

            ddlacmonth.Items.Insert(6, "December");
            ddlacmonth.Items[6].Value = "12";
        }
        if (se != "")
        {
            ddlacmonth.SelectedIndex = ddlacmonth.Items.IndexOf(ddlacmonth.Items.FindByValue(se));
            lblfysdate.Text = ddlacmonth.SelectedItem.Value + "/" + ddlacday.SelectedItem.Text + "/" + ddlacyear.SelectedItem.Text;
            lblfyedate.Text = Convert.ToDateTime(lblfysdate.Text).AddYears(1).ToShortDateString();
            lblfyedate.Text = Convert.ToDateTime(lblfyedate.Text).AddDays(-1).ToShortDateString();

            lblfirstyear.Text = ddlacmonth.SelectedItem.Text + " " + ddlacday.SelectedItem.Text + "," + ddlacyear.SelectedItem.Text;
            lblendyear.Text = Convert.ToDateTime(lblendyear.Text).AddYears(1).ToString("MMMM dd, yyyy");


        }

    }
    protected void ddlacmonth_SelectedIndexChanged(object sender, EventArgs e)
    {

        lblfysdate.Text = ddlacmonth.SelectedItem.Value + "/" + ddlacday.SelectedItem.Text + "/" + ddlacyear.SelectedItem.Text;


        lblfyedate.Text = Convert.ToDateTime(lblfysdate.Text).AddYears(1).ToShortDateString();
        lblfyedate.Text = Convert.ToDateTime(lblfyedate.Text).AddDays(-1).ToShortDateString();

        lblfirstyear.Text = ddlacmonth.SelectedItem.Text + " " + ddlacday.SelectedItem.Text + "," + ddlacyear.SelectedItem.Text;
        lblendyear.Text = Convert.ToDateTime(lblfirstyear.Text).AddYears(1).ToString("MMMM dd, yyyy");
        lblendyear.Text = Convert.ToDateTime(lblendyear.Text).AddDays(-1).ToString("MMMM dd, yyyy");
    }
    protected void ddlacyear_SelectedIndexChanged(object sender, EventArgs e)
    {

        lblfysdate.Text = ddlacmonth.SelectedItem.Value + "/" + ddlacday.SelectedItem.Text + "/" + ddlacyear.SelectedItem.Text;


        lblfyedate.Text = Convert.ToDateTime(lblfysdate.Text).AddYears(1).ToShortDateString();
        lblfyedate.Text = Convert.ToDateTime(lblfyedate.Text).AddDays(-1).ToShortDateString();

        lblfirstyear.Text = ddlacmonth.SelectedItem.Text + " " + ddlacday.SelectedItem.Text + "," + ddlacyear.SelectedItem.Text;
        lblendyear.Text = Convert.ToDateTime(lblfirstyear.Text).AddYears(1).ToString("MMMM dd, yyyy");
        lblendyear.Text = Convert.ToDateTime(lblendyear.Text).AddDays(-1).ToString("MMMM dd, yyyy");
    }
    protected void btnconf_Click(object sender, EventArgs e)
    {
        flat = 5;
        Label3.Text = "Account Year confirmed";
        //ModapupExtende22.Show();
        lblfinal.Text = lblfysdate.Text + " - " + lblfyedate.Text + ".";
    }
    protected void btnapd_Click(object sender, EventArgs e)
    {
        flat = 5;
        //ModalPopupExtender1222.Show();
    }
    protected void Button1_Click2(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            //pnlgrid.ScrollBars = ScrollBars.None;
            //pnlgrid.Height = new Unit("100%");

            GridView1.AllowPaging = false;
            GridView1.PageSize = 1000;
            fillgriddata();

            Button1.Text = "Hide Printable Version";
            Button3.Visible = true;
            if (GridView1.Columns[6].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[6].Visible = false;
            }
            if (GridView1.Columns[7].Visible == true)
            {
                ViewState["editHide11"] = "tt";
                GridView1.Columns[7].Visible = false;
            }
        }
        else
        {
            Button1.Text = "Printable Version";
            Button3.Visible = false;


            GridView1.AllowPaging = true;
            GridView1.PageSize = 20;
            fillgriddata();

            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[6].Visible = true;
            }
            if (ViewState["editHide11"] != null)
            {
                GridView1.Columns[7].Visible = true;
            }

        }
    }
    protected void chkad_CheckedChanged(object sender, EventArgs e)
    {
        if (chkad.Checked == true)
        {
            pnladmin.Visible = true;
        }
        else
        {
            pnladmin.Visible = false;
        }
    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        btnadd.Visible = false;
        pnladd.Visible = true;
        lbladd.Text = "Add New Business";
        Label3.Text = "";
        imgbtnupdate.Visible = false;
        ImgBtnSubmit.Visible = true;
    }

    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox1.Checked == true)
        {
            pnlaccy.Visible = true;
        }
        else
        {
            pnlaccy.Visible = false;
        }
    }

    protected void ddlactfil_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgriddata();
    }


    public void deptdesi()
    {
        DataTable dt_s = select("Select * From DepartmentmasterMNC   where Companyid='" + Session["comid"] + "' and Whid='" + Session["whid"] + "'  order by Departmentname");
        for (int i = 0; i < dt_s.Rows.Count; i++)
        {
            con.Open();
            SqlCommand da = new SqlCommand("insert into DepartmentmasterMNC(Companyid,Departmentname,Whid) values('" + Session["comid"] + "','" + dt_s.Rows[i]["Departmentname"].ToString() + "','" + ViewState["whid"] + "')",con);
            da.ExecuteNonQuery();

            DataTable da1 = select("select max(id) from DepartmentmasterMNC ");

            DataTable dty = select("select * from DesignationMaster Where DeptID='" + dt_s.Rows[i]["id"].ToString() + "'");
            for (int j = 0; j < dty.Rows.Count; j++)
            {
                SqlCommand daa = new SqlCommand("insert into DesignationMaster(DesignationName,DeptID,RoleId) values('" + dty.Rows[j]["DesignationName"].ToString() + "','" + da1.Rows[0][0].ToString() + "','" + dty.Rows[j]["RoleId"].ToString() + "')", con);
                daa.ExecuteNonQuery();
            }
            con.Close();    
        }
    }
}