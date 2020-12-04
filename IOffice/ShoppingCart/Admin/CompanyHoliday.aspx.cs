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

public partial class Add_Company_Holiday : System.Web.UI.Page
{
    // SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString"].ConnectionString);
    SqlConnection con=new SqlConnection(PageConn.connnn);
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        }
        //PageConn pgcon = new PageConn();
        //con = pgcon.dynconn;
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };
        // compid = Session["comid"].ToString();
        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();


        Page.Title = pg.getPageTitle(page);

        // lblmsg.Visible = false;
        if (!IsPostBack)
        {
            Pagecontrol.dypcontrol(Page, page);
         

            ViewState["sortOrder"] = "";
            lblCompany.Text = Session["Cname"].ToString();


            fillstore();
            datefill();
            fillgrdholiday();
           
            fillgrdspecialholiday();
            fillschedule();
            gridfun();
            
            txtdate.Text = System.DateTime.Now.ToShortDateString();
        }
    }
    public void gridfun()
    {
        string st1 = "";
        string st2 = "";
        lblBusiness.Text = "All";
        lblholiday.Text = "All";
       


        string strL = " SELECT Company_Holiday.Id as Company_Holiday_Id,case when(Company_Holiday.HolidayMethod='1') then 'Yes' else 'No' end as holiday, Company_Holiday.Whid, WareHouseMaster.Name, Company_Holiday.HolidayName,Company_Holiday.ScheduleName ,convert(nvarchar,Company_Holiday.Date,101) as Date  FROM Company_Holiday  INNER JOIN  WareHouseMaster ON Company_Holiday.Whid = WareHouseMaster.WareHouseId   where Company_Holiday.Company_Id='" + Session["comid"] + "'";
        if (DropDownList1.SelectedIndex > 0)
        {
            st1 = " and Company_Holiday.Whid='" + DropDownList1.SelectedValue + "'";
            lblBusiness.Text = DropDownList1.SelectedItem.Text;
        }
        if (txtstartdate1.Text.Length > 0 && txttodate.Text.Length > 0)
        {
            st2 = "and (Date between '" + txtstartdate1.Text + "' and '" + txttodate.Text + "')";

        }
        string sorting = " order by WareHouseMaster.Name,Company_Holiday.HolidayName,Company_Holiday.ScheduleName,Company_Holiday.Date ";


        strL = strL + st1 + st2 + sorting;

        SqlCommand cmd3 = new SqlCommand(strL, con);
        SqlDataAdapter adap3 = new SqlDataAdapter(cmd3);
        DataSet ds3 = new DataSet();
        adap3.Fill(ds3);
        DataView myDataView = new DataView();
        myDataView = ds3.Tables[0].DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }

        GridView2.DataSource = myDataView;
        GridView2.DataBind();

    }

    public void fillschedule()
    {
        SqlCommand cmd = new SqlCommand("Select Distinct id,SchedulName from TimeSchedulMaster where Status = '1' order by SchedulName", con);
        SqlDataAdapter adap = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adap.Fill(ds);
        ddlschedule.DataSource = ds;
        ddlschedule.DataValueField = "id";
        ddlschedule.DataTextField = "SchedulName";
        ddlschedule.DataBind();
        ddlschedule.Items.Insert(0, "-Select-");
        ddlschedule.Items[0].Value = "0";

    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        string st1 = "select * from Company_Holiday where Whid='" + ddlStore.SelectedValue + "' and HolidayName='" + txtHoliday.Text + "' ";
        SqlCommand cmd1 = new SqlCommand(st1, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable dt = new DataTable();
        adp1.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            Label1.Visible = true;
            Label1.Text = "Record already exists";
        }
        else
        {
            if (Convert.ToDateTime(txtdate.Text) >= System.DateTime.Now.Date)
            {


               
                if (RdSchedule.SelectedItem.Value == "0")
                {
                    SqlDataAdapter adpt = new SqlDataAdapter("select * from Company_Holiday where  Whid='" + ddlStore.SelectedValue + "' and Date='" + txtdate.Text + "' ", con);
                    DataTable dt1 = new DataTable();
                    adpt.Fill(dt1);
                    if (dt1.Rows.Count > 0)
                    {
                        Label1.Text = "Sorry, Holiday or Special Schedule is already set at that day";
                    }
                    else
                    {


                        string strbatch = "Insert  Into Company_Holiday(Whid,ScheduleName,Date,Company_Id,HolidayMethod)values('" + ddlStore.SelectedValue + "','" + txtHoliday.Text + "','" + txtdate.Text + "','" + Session["comid"] + "','" + RdSchedule.SelectedValue + "')";
                            SqlCommand cmd12 = new SqlCommand(strbatch, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmd12.ExecuteNonQuery();
                            con.Close();

                            string str123 = "select Max(Id) as CompanyHolidayMasterID from Company_Holiday where Whid='" + ddlStore.SelectedValue + "'";
                            DataTable ds123 = new DataTable();
                            SqlDataAdapter da123 = new SqlDataAdapter(str123, con);
                            da123.Fill(ds123);

                            ViewState["CompanyHolidayMasterID"] = ds123.Rows[0]["CompanyHolidayMasterID"].ToString();

                            SqlDataAdapter adptdateid = new SqlDataAdapter("select * from DateMasterTbl where Date='" + txtdate.Text + "'", con);
                            DataTable dtdateid = new DataTable();
                            adptdateid.Fill(dtdateid);

                            if (dtdateid.Rows.Count > 0)
                            {
                                ViewState["DateID"] = dtdateid.Rows[0]["DateId"].ToString();
                            }

                            foreach (GridViewRow gdr in grdscheduleholiday.Rows)
                            {
                                Label lblbatchmasterid = (Label)gdr.FindControl("lblbatchmasterid");
                                CheckBox chkactivedesignspecialschedule123 = (CheckBox)(gdr.FindControl("chkactivedesignspecialschedule123"));
                                DropDownList ddlbatchschedule = (DropDownList)(gdr.FindControl("ddlbatchschedule"));


                              
                                if (chkactivedesignspecialschedule123.Checked == true)
                                {
                                    string strholidaydetail = "Insert  Into CompanyHolidayBatchDetailTbl(CompanyHolidayId,BatchId,BatchScheduleId,Active)values('" + ViewState["CompanyHolidayMasterID"].ToString() + "','" + lblbatchmasterid.Text + "','" + ddlbatchschedule.SelectedValue + "','" + chkactivedesignspecialschedule123.Checked + "')";
                                    SqlCommand cmdholidaydetail = new SqlCommand(strholidaydetail, con);
                                    if (con.State.ToString() != "Open")
                                    {
                                        con.Open();
                                    }
                                    cmdholidaydetail.ExecuteNonQuery();
                                    con.Close();

                                    DateTime date = Convert.ToDateTime(txtdate.Text);
                                   string day = date.DayOfWeek.ToString();
                                   string upd = "";

                                   if (day == "Monday")
                                   {
                                       upd = " Monday='1',MondayScheduleId='" + ddlbatchschedule.SelectedValue + "'";
                                   }
                                   
                                   else if (day == "Tuesday")
                                   {
                                       upd = " Tuesday='1',TuesdayScheduleId='" + ddlbatchschedule.SelectedValue + "'";
                            

                                   }
                                   else if (day == "Wednesday")
                                   {
                                       upd = " Wednesday='1', WednesdayScheduleId='" + ddlbatchschedule.SelectedValue + "'";
                            
                                   }
                                   else if (day == "Thursday")
                                   {
                                       upd = " Thursday='1',ThursdayScheduleId='" + ddlbatchschedule.SelectedValue + "'";
                            
                                   }
                                   else if (day == "Friday")
                                   {
                                       upd = " Friday='1',FridayScheduleId='" + ddlbatchschedule.SelectedValue + "'";
                            
                                   }
                                   else if (day == "Saturday")
                                   {
                                       upd = " Saturday='1',SaturdayScheduleId='" + ddlbatchschedule.SelectedValue + "'";
                            
                                   }
                                   else if (day == "Sunday")
                                   {
                                       upd = " Sunday='1',SundayScheduleId='" + ddlbatchschedule.SelectedValue + "'";
                            
                                   }
                                    string BatchDateDelete = " Update  BatchWorkingDays set "+upd+" where DateMasterID='" + ViewState["DateID"] + "' and BatchID='" + lblbatchmasterid.Text + "'  ";
                                    SqlCommand cmdDatedelete = new SqlCommand(BatchDateDelete, con);
                                    if (con.State.ToString() != "Open")
                                    {
                                        con.Open();
                                    }

                                    cmdDatedelete.ExecuteNonQuery();
                                    con.Close();
                                }

                            }


                           
                            Label1.Text = "Record inserted successfully";
                            txtHoliday.Text = "";
                            btnadd.Visible = true;
                            pnladd.Visible = false;
                            lbladd.Text = "";
                            gridfun();

                           
                       
                    }
                }
                else
                {

                    string strbatch = "Insert  Into Company_Holiday(Whid,HolidayName,Date,Company_Id,HolidayMethod)values('" + ddlStore.SelectedValue + "','" + txtHoliday.Text + "','" + txtdate.Text + "','" + Session["comid"] + "','" + RdSchedule.SelectedValue + "')";
                    SqlCommand cmd12 = new SqlCommand(strbatch, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd12.ExecuteNonQuery();
                    con.Close();


                    string str123 = "select Max(Id) as CompanyHolidayMasterID from Company_Holiday ";
                    DataTable ds123 = new DataTable();
                    SqlDataAdapter da123 = new SqlDataAdapter(str123, con);
                    da123.Fill(ds123);

                    ViewState["CompanyHolidayMasterID"] = ds123.Rows[0]["CompanyHolidayMasterID"].ToString();

                    SqlDataAdapter adptdateid = new SqlDataAdapter("select * from DateMasterTbl where Date='" + txtdate.Text + "'", con);
                    DataTable dtdateid = new DataTable();
                    adptdateid.Fill(dtdateid);

                    if (dtdateid.Rows.Count > 0)
                    {
                        ViewState["DateID"] = dtdateid.Rows[0]["DateId"].ToString();
                    }

                    foreach (GridViewRow gdr in grdfullholiday.Rows)
                    {
                        Label lblbatchmasteridfullholidayid = (Label)gdr.FindControl("lblbatchmasteridfullholidayid");
                        CheckBox chkactivedesign123 = (CheckBox)(gdr.FindControl("chkactivedesign123"));


                        if (chkactivedesign123.Checked == true)
                        {

                            string strholidaydetail = "Insert  Into CompanyHolidayBatchDetailTbl(CompanyHolidayId,BatchId,Active)values('" + ViewState["CompanyHolidayMasterID"].ToString() + "','" + lblbatchmasteridfullholidayid.Text + "','" + chkactivedesign123.Checked + "')";
                            SqlCommand cmdholidaydetail = new SqlCommand(strholidaydetail, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmdholidaydetail.ExecuteNonQuery();
                            con.Close();
                            string BatchDateDelete = " Delete from BatchWorkingDays where DateMasterID='" + ViewState["DateID"] + "' and BatchID='" + lblbatchmasteridfullholidayid.Text + "'  ";
                            SqlCommand cmdDatedelete = new SqlCommand(BatchDateDelete, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }

                            cmdDatedelete.ExecuteNonQuery();
                            con.Close();
                        }
                        
                    }

                    Label1.Text = "Record inserted successfully";
                    gridfun();
                    pnladd.Visible = true;
                    ddlStore.SelectedIndex = 0;
                    txtdate.Text = "";
                    txtHoliday.Text = "";
                    btnadd.Visible = true;
                    pnladd.Visible = false;
                    lbladd.Text = "";
                }


            }
            else
            {
                Label1.Text = "Start Date must be today or greater then today";
            }
        }
    }

   
    protected void RdSchedule_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label1.Text = "";
        if (RdSchedule.SelectedItem.Value == "1")
        {
           

            pnlgrdholiday.Visible = true;
            pnlgrdspecialholiday.Visible = false;
        }
        else
        {
           

            pnlgrdholiday.Visible = false;
            pnlgrdspecialholiday.Visible = true;
        }
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        gridfun();
        
    }
  

    protected void edit()
    {



        SqlCommand cmd3 = new SqlCommand("SELECT * FROM Company_Holiday  INNER JOIN  WareHouseMaster ON Company_Holiday.Whid = WareHouseMaster.WareHouseId where Company_Holiday.Company_Id='" + Session["comid"] + "' and Company_Holiday.Id='" + ViewState["Id"] + "'", con); ;
        SqlDataAdapter adp = new SqlDataAdapter(cmd3);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        if (Convert.ToDateTime(ds.Tables[0].Rows[0]["Date"]) >= Convert.ToDateTime(DateTime.Now.ToShortDateString()))
        {
        fillstore();
        ddlStore.SelectedIndex = ddlStore.Items.IndexOf(ddlStore.Items.FindByValue(ds.Tables[0].Rows[0]["Whid"].ToString()));
        txtdate.Enabled = false;
        ddlStore.Enabled = false;
        imgbtncal.Enabled = false;
        if (ds.Tables[0].Rows[0]["HolidayMethod"].ToString() == "1")
        {
            txtHoliday.Text = ds.Tables[0].Rows[0]["HolidayName"].ToString();
        }
        else
        {
            txtHoliday.Text = ds.Tables[0].Rows[0]["ScheduleName"].ToString();
        }
        
        
        txtdate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["Date"].ToString()).ToShortDateString();

        ViewState["Previousdate"] = ds.Tables[0].Rows[0]["Date"].ToString();

        

        if (ds.Tables[0].Rows[0]["HolidayMethod"].ToString() == "1")
        {
            ViewState["st"] ="1";
            RdSchedule.SelectedValue = "1";
            pnlgrdholiday.Visible = true;
            pnlgrdspecialholiday.Visible = false;
            fillgrdholiday();
            RdSchedule.Enabled = false;
            foreach (GridViewRow gdr in grdfullholiday.Rows)
            {
              
                Label lblbatchmasteridfullholidayid = (Label)gdr.FindControl("lblbatchmasteridfullholidayid");

                CheckBox chkactivedesign123 = (CheckBox)gdr.FindControl("chkactivedesign123");
              
                SqlCommand cmdholidaybatch = new SqlCommand("select * from CompanyHolidayBatchDetailTbl where BatchId='" + lblbatchmasteridfullholidayid.Text + "' and CompanyHolidayId='" + ViewState["Id"] + "' and BatchScheduleId Is Null and Active='1'", con); ;
                SqlDataAdapter adpholidaybatch = new SqlDataAdapter(cmdholidaybatch);
                DataTable dsholidaybatch = new DataTable();
                adpholidaybatch.Fill(dsholidaybatch);

                if (dsholidaybatch.Rows.Count > 0)
                {
                    chkactivedesign123.Checked = true;
                    chkactivedesign123.Enabled = false;
                }
                else
                {
                    chkactivedesign123.Enabled = true;
                    chkactivedesign123.Checked = false;
                }
                           
            }

        }
        else
        {
            ViewState["st"] = "0";
            RdSchedule.SelectedValue = "0";
            pnlgrdholiday.Visible = false;
            pnlgrdspecialholiday.Visible = true;
            RdSchedule.Enabled = false;
            fillgrdspecialholiday();

            foreach (GridViewRow gdr in grdscheduleholiday.Rows)
            {
                Label lblbatchmasterid = (Label)gdr.FindControl("lblbatchmasterid");

                CheckBox chkactivedesignspecialschedule123 = (CheckBox)gdr.FindControl("chkactivedesignspecialschedule123");
                DropDownList ddlbatchschedule = (DropDownList)gdr.FindControl("ddlbatchschedule");




                SqlCommand cmdholidaybatch = new SqlCommand("select * from CompanyHolidayBatchDetailTbl where BatchId='" + lblbatchmasterid.Text + "' and CompanyHolidayId='" + ViewState["Id"] + "' and BatchScheduleId Is Not Null and Active='1'", con); ;
                SqlDataAdapter adpholidaybatch = new SqlDataAdapter(cmdholidaybatch);
                DataTable dsholidaybatch = new DataTable();
                adpholidaybatch.Fill(dsholidaybatch);

                if (dsholidaybatch.Rows.Count > 0)
                {
                    chkactivedesignspecialschedule123.Checked = true;
                    chkactivedesignspecialschedule123.Enabled = false;
                    ddlbatchschedule.SelectedIndex = ddlbatchschedule.Items.IndexOf(ddlbatchschedule.Items.FindByValue(dsholidaybatch.Rows[0]["BatchScheduleId"].ToString()));

                }
                else
                {
                    chkactivedesignspecialschedule123.Enabled = true;
                    chkactivedesignspecialschedule123.Checked = false;
                }

            }


        }

       
        btnupdate.Visible = true;
        btnsubmit.Visible = false;
        btnadd.Visible = false;
        pnladd.Visible = true;
        GridView2.EditIndex = -1;
        lbladd.Text = "Edit Company Holiday";
        }
        else
        {
            btnupdate.Visible = false;
            btnsubmit.Visible = true;
            btnadd.Visible = true;
            pnladd.Visible = false;
            GridView2.EditIndex = -1;

            Label1.Text = "You cannot edit a holiday with a date that has already passed.";
        }
    }
    protected void ImageButton7_Click(object sender, EventArgs e)
    {
        lbladd.Text = "";
        lblschedule.Visible = false;
        ddlschedule.Visible = false;
        RdSchedule.Enabled = true;
        RdSchedule.SelectedIndex = 0;
        ddlschedule.SelectedIndex = 0;
        //ddlStore.SelectedIndex = 0;
        txtdate.Text = "";
        txtHoliday.Text = "";
        btnupdate.Visible = false;
        btnsubmit.Visible = true;
        Label1.Text = "";
        imgbtncal.Enabled = true;
        txtdate.Enabled = true;
        ddlStore.Enabled = true;
        btnadd.Visible = true;
        pnladd.Visible = false;
        lbladd.Text = "";
        GridView2.EditIndex = -1;
    }
   
    protected void btnupdate_Click(object sender, EventArgs e)
    {

        string st1 = "select * from Company_Holiday where Whid='" + ddlStore.SelectedValue + "' and HolidayName='" + txtHoliday.Text + "'and Id != '" + ViewState["Id"] + "' ";
        SqlCommand cmd1 = new SqlCommand(st1, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable dt = new DataTable();
        adp1.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            Label1.Visible = true;
            Label1.Text = "Record already exists";
        }
        else
        {
            if (Convert.ToDateTime(txtdate.Text) >= System.DateTime.Now.Date)
            {
                
                SqlDataAdapter adptdateid = new SqlDataAdapter("select * from DateMasterTbl where Date='" + txtdate.Text + "'", con);
                DataTable dtdateid = new DataTable();
                adptdateid.Fill(dtdateid);

                if (dtdateid.Rows.Count > 0)
                {
                    ViewState["DateID"] = dtdateid.Rows[0]["DateId"].ToString();
                }


                if (RdSchedule.SelectedItem.Value == "0")
                {

                    string strbatch = "Update  Company_Holiday Set ScheduleName='" + txtHoliday.Text + "',HolidayMethod='" + RdSchedule.SelectedValue + "',Whid='" + ddlStore.SelectedValue + "' where Id = '" + ViewState["Id"] + "' ";
                    SqlCommand cmd12 = new SqlCommand(strbatch, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd12.ExecuteNonQuery();
                    con.Close();
                        foreach (GridViewRow gdr in grdscheduleholiday.Rows)
                        {
                            Label lblbatchmasterid = (Label)gdr.FindControl("lblbatchmasterid");
                            CheckBox chkactivedesignspecialschedule123 = (CheckBox)(gdr.FindControl("chkactivedesignspecialschedule123"));
                            DropDownList ddlbatchschedule = (DropDownList)(gdr.FindControl("ddlbatchschedule"));

                            string strholidaydetail = "";
                            if (chkactivedesignspecialschedule123.Enabled == false)
                            {
                                strholidaydetail = "Update   CompanyHolidayBatchDetailTbl Set Active='" + chkactivedesignspecialschedule123.Checked + "', BatchScheduleId='" + ddlbatchschedule.SelectedValue + "' where BatchId='" + lblbatchmasterid.Text + "' AND CompanyHolidayId='" + ViewState["Id"] + "'";
                            }
                            else
                            {
                                 strholidaydetail = "Insert  Into CompanyHolidayBatchDetailTbl(CompanyHolidayId,BatchId,BatchScheduleId,Active)values('" + ViewState["Id"] + "','" + lblbatchmasterid.Text + "','" + ddlbatchschedule.SelectedValue + "','" + chkactivedesignspecialschedule123.Checked + "')";
                           
                            }
                                SqlCommand cmdholidaydetail = new SqlCommand(strholidaydetail, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmdholidaydetail.ExecuteNonQuery();
                            con.Close();

                            if (chkactivedesignspecialschedule123.Checked == true)
                            {
                                DateTime date = Convert.ToDateTime(txtdate.Text);
                                string day = date.DayOfWeek.ToString();
                                string upd = "";

                                if (day == "Monday")
                                {
                                    upd = " Monday='1',MondayScheduleId='" + ddlbatchschedule.SelectedValue + "'";
                                }

                                else if (day == "Tuesday")
                                {
                                    upd = " Tuesday='1',TuesdayScheduleId='" + ddlbatchschedule.SelectedValue + "'";


                                }
                                else if (day == "Wednesday")
                                {
                                    upd = " Wednesday='1' and WednesdayScheduleId='" + ddlbatchschedule.SelectedValue + "'";

                                }
                                else if (day == "Thursday")
                                {
                                    upd = " Thursday='1',ThursdayScheduleId='" + ddlbatchschedule.SelectedValue + "'";

                                }
                                else if (day == "Friday")
                                {
                                    upd = " Friday='1',FridayScheduleId='" + ddlbatchschedule.SelectedValue + "'";

                                }
                                else if (day == "Saturday")
                                {
                                    upd = " Saturday='1',SaturdayScheduleId='" + ddlbatchschedule.SelectedValue + "'";

                                }
                                else if (day == "Sunday")
                                {
                                    upd = " Sunday='1',SundayScheduleId='" + ddlbatchschedule.SelectedValue + "'";

                                }
                                string BatchDateDelete = " Update  BatchWorkingDays set " + upd + " where DateMasterID='" + ViewState["DateID"] + "' and BatchID='" + lblbatchmasterid.Text + "'  ";
                                SqlCommand cmdDatedelete = new SqlCommand(BatchDateDelete, con);
                                if (con.State.ToString() != "Open")
                                {
                                    con.Open();
                                }

                                cmdDatedelete.ExecuteNonQuery();
                                con.Close();
                            }

                        }
                        imgbtncal.Enabled =true;
                        ddlStore.Enabled = true;
                        txtdate.Enabled = true;
                        RdSchedule.Enabled = true;
                        Label1.Text = "Record updated successfully";
                        txtHoliday.Text = "";
                        btnadd.Visible = true;
                        pnladd.Visible = false;
     
                        lbladd.Text = "";
                        gridfun();
                    
                }
                else
                {
                    string strbatch = "Update  Company_Holiday Set HolidayName='" + txtHoliday.Text + "',HolidayMethod='" + RdSchedule.SelectedValue + "',Whid='" + ddlStore.SelectedValue + "' where Id = '" + ViewState["Id"] + "' ";
                    SqlCommand cmd12 = new SqlCommand(strbatch, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd12.ExecuteNonQuery();
                    con.Close();
                    foreach (GridViewRow gdr in grdfullholiday.Rows)
                    {
                        Label lblbatchmasteridfullholidayid = (Label)gdr.FindControl("lblbatchmasteridfullholidayid");
                        CheckBox chkactivedesign123 = (CheckBox)(gdr.FindControl("chkactivedesign123"));
                        string strholidaydetail = "";
                        if (chkactivedesign123.Checked == false)
                        {
                            strholidaydetail = "Update CompanyHolidayBatchDetailTbl set Active='" + chkactivedesign123.Checked + "' where BatchId='" + lblbatchmasteridfullholidayid.Text + "' AND CompanyHolidayId='" + ViewState["Id"] + "'";
                        }
                        else
                        {
                            strholidaydetail = "Insert  Into CompanyHolidayBatchDetailTbl(CompanyHolidayId,BatchId,Active)values('" + ViewState["Id"] + "','" + lblbatchmasteridfullholidayid.Text + "','" + chkactivedesign123.Checked + "')";
                           
                        }
                            SqlCommand cmdholidaydetail = new SqlCommand(strholidaydetail, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmdholidaydetail.ExecuteNonQuery();
                        con.Close();

                        if (chkactivedesign123.Checked == true)
                        {

                            string BatchDateDelete = " Delete from BatchWorkingDays where DateMasterID='" + ViewState["DateID"] + "' and BatchID='" + lblbatchmasteridfullholidayid.Text + "'  ";
                            SqlCommand cmdDatedelete = new SqlCommand(BatchDateDelete, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }

                            cmdDatedelete.ExecuteNonQuery();
                            con.Close();
                        }

                    }
                    ddlStore.Enabled = true;
                    imgbtncal.Enabled = true;
                    txtdate.Enabled = true;
                    RdSchedule.Enabled = true;
                    Label1.Text = "Record updated successfully";
                    gridfun();
                    btnadd.Visible = true;
                    pnladd.Visible = false;
                    lbladd.Text = "";
                    txtdate.Text = "";
                    txtHoliday.Text = "";
                }


            }
            else
            {
                Label1.Text = "Start Date must be today or greater then today";
            }
        }
        GridView2.EditIndex = -1;
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        gridfun();
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
    protected void GridView2_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        gridfun();
    }
    protected void fillstore()
    {
        ddlStore.Items.Clear();

        DataTable ds = ClsStore.SelectStorename();
        ddlStore.DataSource = ds;
        ddlStore.DataTextField = "Name";
        ddlStore.DataValueField = "WareHouseId";
        ddlStore.DataBind();

        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlStore.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }

        DropDownList1.DataSource = ds;
        DropDownList1.DataTextField = "Name";
        DropDownList1.DataValueField = "WareHouseId";
        DropDownList1.DataBind();
        DropDownList1.Items.Insert(0, "All");
        DropDownList1.Items[0].Value = "0";

    }


    protected void fillgrdholiday()
    {

        string str = "Select BatchMaster.* from BatchMaster inner join BatchWorkingDay on BatchWorkingDay.BatchMasterId=BatchMaster.ID where BatchMaster.WHID='" + ddlStore.SelectedValue + "' ";
        SqlCommand cmd3 = new SqlCommand(str, con);
        SqlDataAdapter adap3 = new SqlDataAdapter(cmd3);
        DataTable ds3 = new DataTable();
        adap3.Fill(ds3);

        if (ds3.Rows.Count > 0)
        {
            grdfullholiday.DataSource = ds3;
            grdfullholiday.DataBind();
        }
        else
        {
            grdfullholiday.DataSource = null;
            grdfullholiday.DataBind();
        }
        
    }
    protected void fillgrdspecialholiday()
    {

        string str = "Select BatchMaster.* from BatchMaster inner join BatchWorkingDay on BatchWorkingDay.BatchMasterId=BatchMaster.ID where BatchMaster.WHID='" + ddlStore.SelectedValue + "' ";
        SqlCommand cmd3 = new SqlCommand(str, con);
        SqlDataAdapter adap3 = new SqlDataAdapter(cmd3);
        DataTable ds3 = new DataTable();
        adap3.Fill(ds3);

        if (ds3.Rows.Count > 0)
        {
            grdscheduleholiday.DataSource = ds3;
            grdscheduleholiday.DataBind();


        }
        else
        {
            grdscheduleholiday.DataSource = null;
            grdscheduleholiday.DataBind();
        }
        foreach (GridViewRow gdr in grdscheduleholiday.Rows)
        {
            Label lblbatchmasterid = (Label)gdr.FindControl("lblbatchmasterid");
            Label lblwhid = (Label)gdr.FindControl("lblwhid");

            DropDownList ddlbatchschedule = (DropDownList)gdr.FindControl("ddlbatchschedule");

            CheckBox chkactivedesignspecialschedule123 = (CheckBox)(gdr.FindControl("chkactivedesignspecialschedule123"));

            ddlbatchschedule.Items.Clear();
            string strmethod = "select distinct BatchTiming.Id, TimeSchedulMaster.SchedulName +' : '+Left(cast(BatchTiming.StartTime as nvarchar(50)),5) +' : '+Left(cast(BatchTiming.EndTime as nvarchar(50)),5)  as SchedulName from TimeSchedulMaster  inner join [BatchTiming] ON [BatchTiming].TimeScheduleMasterId=TimeSchedulMaster.id where [BatchTiming].Active=1 and BatchTiming.compid='" + Session["Comid"].ToString() + "' and BatchTiming.BatchMasterId='" + lblbatchmasterid.Text + "'  and BatchTiming.Whid='" + lblwhid.Text + "' and BatchTiming.Active='1'";
            SqlCommand cmdallocate = new SqlCommand(strmethod, con);
            SqlDataAdapter adpallocate = new SqlDataAdapter(cmdallocate);
            DataTable dtallocate = new DataTable();
            adpallocate.Fill(dtallocate);
            if (dtallocate.Rows.Count > 0)
            {

                ddlbatchschedule.DataSource = dtallocate;
                ddlbatchschedule.DataTextField = "SchedulName";
                ddlbatchschedule.DataValueField = "Id";
                ddlbatchschedule.DataBind();
            }
            else
            {
                ddlbatchschedule.DataSource = null;
                ddlbatchschedule.DataBind();
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Label1.Text = "";
        if (Button1.Text == "Printable Version")
        {


            Button1.Text = "Hide Printable Version";
            Button7.Visible = true;
            if (GridView2.Columns[5].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView2.Columns[5].Visible = false;
            }
            if (GridView2.Columns[6].Visible == true)
            {
                ViewState["deletehide"] = "tt";
                GridView2.Columns[6].Visible = false;
            }

        }
        else
        {


            Button1.Text = "Printable Version";
            Button7.Visible = false;
            if (ViewState["editHide"] != null)
            {
                GridView2.Columns[5].Visible = true;
            }
            if (ViewState["deletehide"] != null)
            {
                GridView2.Columns[6].Visible = true;
            }
           

        }
    }
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        gridfun();
    }


    protected void edit_Click(object sender, EventArgs e)
    {
        Label1.Text = "";
      
        // LinkButton lnk = (LinkButton)sender;
        ImageButton lnk = (ImageButton)sender;
        int i = Convert.ToInt32(lnk.CommandArgument);
        Label1.Text = "";
        ViewState["Id"] = i.ToString();
        edit();
       
    }
    protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //Label1.Text = "";
        //ViewState["Id"] = GridView2.DataKeys[e.NewEditIndex].Value.ToString();
        //edit();
        //btnadd.Visible = false;
        //pnladd.Visible = true;
        //lbladd.Text = "Edit Company Holiday";
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        fillgrdholiday();
        ddlschedule.Enabled = true;
        imgbtncal.Enabled = true;
        ddlStore.Enabled = true;
        txtdate.Enabled =true;
        imgbtncal.Enabled = true;
        fillgrdspecialholiday();
        Label1.Text = "";
        btnadd.Visible = false;
        pnladd.Visible = true;
        lbladd.Text = "Add New Company Holiday";
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Label1.Text = "";
        gridfun();
    }
    protected void datefill()
    {
        string openingdate = "select StartDate,EndDate from ReportPeriod where Compid='" + Session["Comid"].ToString() + "' and Whid='" + Convert.ToInt32(ddlStore.SelectedValue) + "' and Active='1'";
        SqlCommand cmd22221 = new SqlCommand(openingdate, con);
        SqlDataAdapter adp22221 = new SqlDataAdapter(cmd22221);
        DataTable ds112221 = new DataTable();
        adp22221.Fill(ds112221);

        if (ds112221.Rows.Count > 0)
        {
            DateTime t1;
            DateTime t2;

            t1 = Convert.ToDateTime(ds112221.Rows[0]["StartDate"].ToString());
            t2 = Convert.ToDateTime(ds112221.Rows[0]["EndDate"].ToString());
            txtstartdate1.Text = t1.ToShortDateString();

            txttodate.Text = t2.ToShortDateString();


        }

    }
    protected void grdscheduleholiday_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlStore_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label1.Text = "";
        fillgrdholiday();

        fillgrdspecialholiday();
    }
    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        String chid = GridView2.DataKeys[e.RowIndex].Value.ToString();

        SqlDataAdapter adfd = new SqlDataAdapter("select * from Company_Holiday where Id='" + chid + "'", con);
        DataTable dtfd = new DataTable();
        adfd.Fill(dtfd);
        if (Convert.ToDateTime(dtfd.Rows[0]["Date"]) >=Convert.ToDateTime(DateTime.Now.ToShortDateString()))
        {
            SqlDataAdapter adptdateid = new SqlDataAdapter("select * from DateMasterTbl where Date='" + Convert.ToDateTime(dtfd.Rows[0]["Date"]) + "'", con);
            DataTable dtdateid = new DataTable();
            adptdateid.Fill(dtdateid);

            if (dtdateid.Rows.Count > 0)
            {
                ViewState["DateID"] = dtdateid.Rows[0]["DateId"].ToString();
            }



            SqlConnection conn = new SqlConnection(PageConn.connnn);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            SqlTransaction transaction = con.BeginTransaction();
            try
            {

                SqlDataAdapter adadd = new SqlDataAdapter("select BatchId from CompanyHolidayBatchDetailTbl where CompanyHolidayId='" + chid + "'", conn);
                DataTable dta = new DataTable();
                adadd.Fill(dta);
               foreach (DataRow dr in dta.Rows)
                {
                    SqlDataAdapter adf = new SqlDataAdapter("select * from BatchWorkingDay  where BatchMasterId='" + dr["BatchId"] + "'", conn);
                    DataTable dtf = new DataTable();
                    adf.Fill(dtf);
                    if (dtf.Rows.Count > 0)
                    {
                        string strbatch = "Insert  Into BatchWorkingDays(DateMasterID,BatchID,Monday,MondayScheduleId,Tuesday,TuesdayscheduleId,Wednesday,WednesdayscheduleId,Thursday,ThursdayscheduleId,Friday,FridayscheduleId,Saturday,SaturdayscheduleId,Sunday,SundayscheduleId)values('" + ViewState["DateID"] + "','" + dr["BatchId"] + "','" + dtf.Rows[0]["Monday"] + "','" + dtf.Rows[0]["MondayScheduleId"] + "','" + dtf.Rows[0]["Tuesday"] + "','" + dtf.Rows[0]["TuesdayScheduleId"] + "','" + dtf.Rows[0]["Wednesday"] + "','" + dtf.Rows[0]["WednesdayScheduleId"] + "','" + dtf.Rows[0]["Thursday"] + "','" + dtf.Rows[0]["ThursdayScheduleId"] + "','" + dtf.Rows[0]["Friday"] + "','" + dtf.Rows[0]["FridayScheduleId"] + "','" + dtf.Rows[0]["Saturday"] + "','" + dtf.Rows[0]["SaturdayScheduleId"] + "','" + dtf.Rows[0]["Sunday"] + "','" + dtf.Rows[0]["SundayScheduleId"] + "')";

                        SqlCommand cmddd = new SqlCommand(strbatch, con);
                        cmddd.Transaction = transaction;
                        cmddd.ExecuteNonQuery();
                    }
                }

                string str = "delete from Company_Holiday where Id='" + Convert.ToString(chid) + "' ";

                SqlCommand cmdd = new SqlCommand(str, con);
                cmdd.Transaction = transaction;
                cmdd.ExecuteNonQuery();

                string str1 = "delete from CompanyHolidayBatchDetailTbl where CompanyHolidayId='" + Convert.ToString(chid) + "' ";

                SqlCommand cmdd1 = new SqlCommand(str1, con);
                cmdd1.Transaction = transaction;
                cmdd1.ExecuteNonQuery();




                transaction.Commit();


            }
            catch (Exception)
            {
                transaction.Rollback();
                Label1.Text = "Record not deleted successfully";
            }
            finally
            {
                con.Close();
                gridfun();
                GridView2.EditIndex = -1;
                Label1.Visible = true;

                Label1.Text = "Record deleted successfully";

            }
        }
        else
        {
            Label1.Text = "You cannot delete a holiday that has already passed.";
        }
    }
 
}
