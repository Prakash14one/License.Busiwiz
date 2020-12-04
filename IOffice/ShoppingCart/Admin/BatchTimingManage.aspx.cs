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

public partial class Add_Batch_Timing : System.Web.UI.Page
{
    SqlConnection conn;
    string compid;
    // SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        }
        pagetitleclass pg = new pagetitleclass();


        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };
        compid = Session["Comid"].ToString();
        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();


        Page.Title = pg.getPageTitle(page);

        PageConn pgcon = new PageConn();
        conn = pgcon.dynconn;
        if (!Page.IsPostBack)
        {
            Pagecontrol.dypcontrol(Page, page);
            ViewState["sortOrder"] = "";
            lblCompany.Text = Session["Cname"].ToString();
            lblBusiness.Text = "All";

            //string str = "select WareHouseId,Name from WareHouseMaster WHERE comid='" + Session["Comid"].ToString() + "' and [WareHouseMaster].Status='1' order by Name";
            //SqlCommand cmd = new SqlCommand(str, conn);
            //SqlDataAdapter adp = new SqlDataAdapter(cmd);
            //DataSet ds = new DataSet();
            //adp.Fill(ds);
            //ddlstorename.DataSource = ds;
            //ddlstorename.DataTextField = "Name";
            //ddlstorename.DataValueField = "WareHouseId";
            //ddlstorename.DataBind();


            //DropDownList3.DataSource = ds;
            //DropDownList3.DataTextField = "Name";
            //DropDownList3.DataValueField = "WareHouseId";
            //DropDownList3.DataBind();
            //DropDownList3.Items.Insert(0, "ALL");

            fillstore();

            lblCompany.Text = Session["Cname"].ToString();

            Fillddlbatch();
            Fillddltime();
            Fillgridbatchtiming();
            if (Request.QueryString["id"] != null && Request.QueryString["id"] != "")
            {
                SqlDataAdapter adpt = new SqlDataAdapter("select BatchMaster.ID,BatchMaster.WHID,WareHouseMaster.WareHouseId from BatchMaster inner join WareHouseMaster on BatchMaster.WHID = WareHouseMaster.WareHouseId where BatchMaster.ID ='" + Request.QueryString["id"] + "'", conn);
                DataTable dt = new DataTable();
                adpt.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    ddlstorename.SelectedIndex = ddlstorename.Items.IndexOf(ddlstorename.Items.FindByValue(dt.Rows[0]["WareHouseId"].ToString()));
                    Fillddlbatch();
                    ddlbatchmaster.SelectedIndex = ddlbatchmaster.Items.IndexOf(ddlbatchmaster.Items.FindByValue(dt.Rows[0]["ID"].ToString()));
                    Fillddltime();

                    Label1.Text = "";
                    lblmsg.Text = "";
                    pnladd.Visible = true;
                    lbladd.Text = "Add New Batch Time";
                    btnadd.Visible = false;
                }
            }
            //ddlstorename.Items.Insert(0, "--Select--");
            ImageButton49.Visible = false;
            ImageButton48.Visible = true;
            lblmsg.Text = "";
            // txteffectstart.Text = Convert.ToString(System.DateTime.Now);
            // txteffectend.Text = Convert.ToString(System.DateTime.Now.AddYears(5));
        }


    }

    protected void Fillddlbatch()
    {
        string str = "select ID,Name from BatchMaster where WHID='" + ddlstorename.SelectedValue + "' and Status='" + 1 + "' order by Name";
        SqlCommand cmd = new SqlCommand(str, conn);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        ddlbatchmaster.DataSource = ds;
        ddlbatchmaster.DataTextField = "Name";
        ddlbatchmaster.DataValueField = "ID";
        ddlbatchmaster.DataBind();


        //ddlbatchmaster.Items.Insert(0, "--Select--");
        //lblmsg.Text = "";
    }
    protected void Fillddltime()
    {

        string str = "select id,SchedulName from TimeSchedulMaster where  id not in (select TimeScheduleMasterId from BatchTiming where BatchMasterId = '" + ddlbatchmaster.SelectedValue + "' )";
        SqlCommand cmd = new SqlCommand(str, conn);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddltimeschedule.DataSource = ds;
            ddltimeschedule.DataTextField = "SchedulName";
            ddltimeschedule.DataValueField = "ID";
            ddltimeschedule.DataBind();
        }
        else
        {
            ddltimeschedule.DataSource = ds;
            ddltimeschedule.DataBind();
        }
        //ddltimeschedule.Items.Insert(0, "--Select--");

        //lblmsg.Text = "";
    }



    //protected void ImageButton48_Click(object sender, ImageClickEventArgs e)
    //{

    //}
    protected void Fillgridbatchtiming()
    {
        string str = "";
        string str1 = "";

        if (DropDownList3.SelectedIndex > 0)
        {
            str1 += " and BatchTiming.Whid='" + DropDownList3.SelectedValue + "'";
        }
        if (ddlfilterstatus.SelectedIndex > 0)
        {
            str1 += " and BatchTiming.Active='" + ddlfilterstatus.SelectedValue + "'";
        }
        if (chkallbatch.Checked == true)
        {
            str1 += " and (getdate() between BatchMaster.EffectiveStartDate and BatchMaster.EffectiveEndDate)";
        }

        str = "  BatchTiming.*,left(BatchTiming.FirstBreakStartTime,5) as FirstBreakStart,left(BatchTiming.totalhours,5) as total,case when (BatchTiming.Active = '1') then 'Active' else 'Inactive' end as status,BatchMaster.ID as BId,BatchMaster.Name as BName,WareHouseMaster.WareHouseId as WId,WareHouseMaster.Name as WName,TimeSchedulMaster.id as Tid,TimeSchedulMaster.SchedulName as SName from BatchTiming " +
                      " INNER JOIN BatchMaster ON BatchMaster.ID=BatchTiming.BatchMasterId " +
                      " INNER JOIN WareHouseMaster ON WareHouseMaster.WareHouseId=BatchTiming.Whid " +
                      " INNER JOIN TimeSchedulMaster ON TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where [WareHouseMaster].Status='1' and BatchTiming.compid='" + Session["Comid"].ToString() + "' " + str1 + "";

        string str2 = "select count(BatchTiming.ID) as ci from BatchTiming " +
                    " INNER JOIN BatchMaster ON BatchMaster.ID=BatchTiming.BatchMasterId " +
                      " INNER JOIN WareHouseMaster ON WareHouseMaster.WareHouseId=BatchTiming.Whid " +
                      " INNER JOIN TimeSchedulMaster ON TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where [WareHouseMaster].Status='1' and BatchTiming.compid='" + Session["Comid"].ToString() + "' " + str1 + "";

        grdbatchtiming.VirtualItemCount = GetRowCount(str2);

        string sortExpression = " WareHouseMaster.Name,BatchMaster.Name,TimeSchedulMaster.SchedulName asc";

        lblBusiness.Text = DropDownList3.SelectedItem.Text;
        lblfilstatus.Text = ddlfilterstatus.SelectedItem.Text;

        if (Convert.ToInt32(ViewState["count"]) > 0)
        {
            DataTable dt1 = GetDataPage(grdbatchtiming.PageIndex, grdbatchtiming.PageSize, sortExpression, str);

            DataView myDataView = new DataView();
            myDataView = dt1.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }
            grdbatchtiming.DataSource = myDataView;
            grdbatchtiming.DataBind();
        }

        else
        {
            grdbatchtiming.DataSource = null;
            grdbatchtiming.DataBind();
        }         
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
        SqlCommand cmd = new SqlCommand(qu, conn);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        return dt;
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
    protected void grdbatchtiming_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //grdbatchtiming.EditIndex = e.NewEditIndex;
        //Fillgridbatchtiming();
    }
    protected void grdbatchtiming_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdbatchtiming.EditIndex = -1;
        Fillgridbatchtiming();
    }


    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        LinkButton lk = (LinkButton)sender;
        int j = Convert.ToInt32(lk.CommandArgument);
        ViewState["Id"] = j;
        Session["TimeId"] = j;
        //String st = "select * from AssociateAdminLoginInfoTbl where ID='" + j + "' ";
        string str = "SELECT * from BatchTiming where ID='" + j + "'  ";

        SqlCommand cmd = new SqlCommand(str, conn);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        adp.Fill(dt);
        if (dt.Rows[0]["Whid"].ToString() != "")
        {
            ddlstorename.SelectedIndex = ddlstorename.Items.IndexOf(ddlstorename.Items.FindByValue(dt.Rows[0]["Whid"].ToString()));
        }


        ddlbatchmaster.SelectedValue = dt.Rows[0]["BatchMasterId"].ToString();
        //txtdiaplayname.Text = dt.Rows[0]["DisplayName"].ToString();
        txtstarttime.Text = dt.Rows[0]["StartTime"].ToString();
        txtendtime.Text = dt.Rows[0]["EndTime"].ToString();
        //txteffectstart.Text = dt.Rows[0]["EffectiveStartDate"].ToString();
        // txteffectend.Text = dt.Rows[0]["EffectiveEndDate"].ToString();
        txtbrakestart.Text = dt.Rows[0]["FirstBreakStartTime"].ToString();
        txtbrakeend.Text = dt.Rows[0]["FirstBreakEndTime"].ToString();
        txtsecondbrakestart.Text = dt.Rows[0]["SecondBreakStartTime"].ToString();
        txtsecondbrakeend.Text = dt.Rows[0]["SecondBreakEndTime"].ToString();
        //  ddltimeschedule.SelectedValue = dt.Rows[0]["TimeScheduleMasterId"].ToString();
        ddltimeschedule.SelectedIndex = ddltimeschedule.Items.IndexOf(ddltimeschedule.Items.FindByValue(dt.Rows[0]["TimeScheduleMasterId"].ToString()));
        ddlstatus.SelectedIndex = ddlstatus.Items.IndexOf(ddlstatus.Items.FindByValue(dt.Rows[0]["Active"].ToString()));

        //string ck = dt.Rows[0]["Active"].ToString();
        //if (ck == "True")
        //{
        //    chkactive.Checked = true;

        //}
        //else
        //{
        //    chkactive.Checked = false;
        //}
        ImageButton49.Visible = true;
        ImageButton48.Visible = false;
        //cleartxt();


    }
    //protected void ImageButton49_Click(object sender, ImageClickEventArgs e)
    //{

    //}
    protected void grdbatchtiming_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        Label batchid = (Label)grdbatchtiming.Rows[e.RowIndex].FindControl("lblBatchId");
        ViewState["BatchId"] = batchid.Text;
        string str11 = "";
        str11 = "Select EmployeeBatchMaster.BatchMasterId,EmployeeBatchMaster.Employeeid from BatchTiming inner join EmployeeBatchMaster on BatchTiming.BatchMasterId=EmployeeBatchMaster.Batchmasterid where  BatchTiming.BatchMasterId='" + ViewState["BatchId"].ToString() + "'";

        SqlCommand cmd = new SqlCommand(str11, conn);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);


        if (ds.Tables[0].Rows.Count == 0)
        {

            string st2 = "Delete from BatchTiming where BatchTiming.ID='" + grdbatchtiming.DataKeys[e.RowIndex].Value.ToString() + "' ";
            SqlCommand cmd2 = new SqlCommand(st2, conn);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            cmd2.ExecuteNonQuery();
            conn.Close();
            grdbatchtiming.EditIndex = -1;
            Fillgridbatchtiming();
            lblmsg.Visible = true;
            lblmsg.Text = "Record deleted successfully";
            cleartxt();
        }
        else
        {
            lblmsg.Visible = true;
            //lblmsg.Text = "Sorry, you can not delete this Batch Timing, Because this batch has employees";
            // lblmsg.Text = "You cannot delete this batch timing as there are employees listed in this batch. Please go to " + "<a href=\"EmployeeBatchManage.aspx\" style=\"font-size:14px; color:red; \" target=\"_blank\">" + "Employee Batch: Manage " + "</a>in order to remove employees from this batch.";
            CheckBox3.Checked = false;
            ModalPopupExtender1.Show();
        }

    }
    //protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    //{

    //}
    protected void cleartxt()
    {

        ImageButton49.Visible = false;
        ImageButton48.Visible = true;
        ddlstorename.SelectedIndex = 0;
        //ddlbatchmaster.SelectedIndex = 0;
        //ddltimeschedule.SelectedIndex = 0;
        txtstarttime.Text = "";
        txtendtime.Text = "";
        txtbrakestart.Text = "";
        txtbrakeend.Text = "";
        txtsecondbrakestart.Text = "";
        txtsecondbrakeend.Text = "";
        // txteffectstart.Text = "";
        // txteffectend.Text = "";
        //chkactive.Checked = false;
        ddlstatus.SelectedIndex = 0;
        Fillddlbatch();
        Fillddltime();


    }

    protected void ddlstorename_SelectedIndexChanged(object sender, EventArgs e)
    {
        string str = "select ID,Name from BatchMaster where WHID='" + ddlstorename.SelectedValue + "' ";
        SqlCommand cmd = new SqlCommand(str, conn);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        ddlbatchmaster.DataSource = ds;
        ddlbatchmaster.DataTextField = "Name";
        ddlbatchmaster.DataValueField = "ID";

        ddlbatchmaster.DataBind();

        Fillddltime();
        //ddlbatchmaster.Items.Insert(0, "--Select--");
        //lblmsg.Text = "";

    }
    protected void ImageButton48_Click(object sender, EventArgs e)
    {
        if (ddltimeschedule.SelectedIndex > -1)
        {
            string st1 = "select * from BatchTiming where BatchMasterId='" + ddlbatchmaster.SelectedValue + "' and Whid='" + ddlstorename.SelectedValue + "' and TimeScheduleMasterId='" + ddltimeschedule.SelectedValue + "'";
            SqlCommand cmd1 = new SqlCommand(st1, conn);
            SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
            DataSet ds1 = new DataSet();
            DataTable dt1 = new DataTable();
            adp1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {

                lblmsg.Visible = true;
                lblmsg.Text = "Please change end time";



            }
            else
            {
                //string batchid = "select BatchTiming.StartTime,BatchTiming.EndTime,BatchTiming.EffectiveStartDate,BatchTiming.EffectiveEndDate,BatchTiming.FirstBreakStartTime,BatchTiming.FirstBreakEndTime,BatchTiming.SecondBreakStartTime,BatchTiming.SecondBreakEndTime from EmployeeBatchMaster inner join  BatchTiming on BatchTiming.BatchMasterId=EmployeeBatchMaster.Batchmasterid inner join BatchWorkingDay on BatchWorkingDay.BatchMasterId=EmployeeBatchMaster.Batchmasterid where EmployeeBatchMaster.Employeeid='" + ddlemployee.SelectedValue + "'  ";
                //SqlCommand cmdbatch = new SqlCommand(batchid, conn);
                //SqlDataAdapter adpbatch = new SqlDataAdapter(cmdbatch);
                //DataTable dsbatch = new DataTable();
                //adpbatch.Fill(dsbatch);

                //if (dsbatch.Rows.Count > 0)
                //{
                string time1 = "";
                string outdifftime1 = "";

                string time2 = "";
                string outdifftime2 = "";

                string time3 = "";
                string outdifftime3 = "";


                string temp1 = "";
                string temp2 = "";

                string temp3 = "";
                string temp4 = "";

                string temp5 = "";
                string temp6 = "";
                TimeSpan t3;
                TimeSpan t4;
                TimeSpan t5;
                TimeSpan t6;
                //string timeStartTime = txtstarttime.Text; ;
                //string timeEndTime = dsbatch.Rows[0]["EndTime"].ToString();

                //string timeEffectiveStartDate = dsbatch.Rows[0]["EffectiveStartDate"].ToString();
                //string timeEffectiveEndDate = dsbatch.Rows[0]["EffectiveEndDate"].ToString();

                //string timeFirstBreakStartTime = dsbatch.Rows[0]["FirstBreakStartTime"].ToString();
                //string timeFirstBreakEndTime = dsbatch.Rows[0]["FirstBreakEndTime"].ToString();

                //string timeSecondBreakStartTime = dsbatch.Rows[0]["SecondBreakStartTime"].ToString();
                //string timeSecondBreakEndTime = dsbatch.Rows[0]["SecondBreakEndTime"].ToString();

                TimeSpan t1 = TimeSpan.Parse(txtstarttime.Text);
                TimeSpan t2 = TimeSpan.Parse(txtendtime.Text);
                if (txtbrakestart.Text != "" && txtbrakeend.Text != "")
                {
                    t3 = TimeSpan.Parse(txtbrakestart.Text);
                    t4 = TimeSpan.Parse(txtbrakeend.Text);
                }
                else
                {
                    t3 = TimeSpan.Parse("00:00");
                    t4 = TimeSpan.Parse("00:00");
                }
                if (txtsecondbrakestart.Text != "" && txtsecondbrakeend.Text != "")
                {
                    t5 = TimeSpan.Parse(txtsecondbrakestart.Text);
                    t6 = TimeSpan.Parse(txtsecondbrakeend.Text);
                }
                else
                {
                    t5 = TimeSpan.Parse("00:00");
                    t6 = TimeSpan.Parse("00:00");
                }
                time1 = t2.Subtract(t1).ToString();

                outdifftime1 = Convert.ToDateTime(time1).ToString("HH:MM");

                temp1 = Convert.ToDateTime(time1).ToString("HH");
                temp2 = Convert.ToDateTime(time1).ToString("mm");

                double main1 = Convert.ToDouble(temp1);
                double main2 = Convert.ToDouble(temp2);


                time2 = t4.Subtract(t3).ToString();
                outdifftime2 = Convert.ToDateTime(time2).ToString("HH:mm");

                temp3 = Convert.ToDateTime(time2).ToString("HH");
                temp4 = Convert.ToDateTime(time2).ToString("mm");
                double main3 = Convert.ToDouble(temp3);
                double main4 = Convert.ToDouble(temp4);

                time3 = t6.Subtract(t5).ToString();

                outdifftime3 = Convert.ToDateTime(time3).ToString("HH:mm");



                temp5 = Convert.ToDateTime(time3).ToString("HH");
                temp6 = Convert.ToDateTime(time3).ToString("mm");

                double main5 = Convert.ToDouble(temp5);
                double main6 = Convert.ToDouble(temp6);


                TimeSpan C1 = TimeSpan.Parse(time1);
                TimeSpan C2 = TimeSpan.Parse(time2);
                TimeSpan C3 = TimeSpan.Parse(time3);

                string diff1 = C3.Add(C2).ToString();

                TimeSpan C4 = TimeSpan.Parse(diff1);

                string Finalcal = C1.Subtract(C4).ToString();
                string finalvalue = Convert.ToDateTime(Finalcal).ToString("HH:mm");
                string lblnoofhour = finalvalue;



                lblmsg.Text = "";
                string str = "Insert  Into BatchTiming (BatchMasterId,StartTime,EndTime,EffectiveStartDate,EffectiveEndDate,FirstBreakStartTime,FirstBreakEndTime,SecondBreakStartTime,SecondBreakEndTime,TimeScheduleMasterId,Active,Whid,compid,totalhours)  Values('" + ddlbatchmaster.SelectedValue + "','" + txtstarttime.Text + "','" + txtendtime.Text + "','','','" + txtbrakestart.Text + "','" + txtbrakeend.Text + "','" + txtsecondbrakestart.Text + "','" + txtsecondbrakeend.Text + "','" + ddltimeschedule.SelectedValue + "','" + ddlstatus.SelectedValue + "','" + ddlstorename.SelectedValue + "','" + Session["Comid"].ToString() + "','" + lblnoofhour.ToString() + "')";
                SqlCommand cmd = new SqlCommand(str, conn);
                if (conn.State.ToString() != "Open")
                {
                    conn.Open();
                }
                cmd.ExecuteNonQuery();
                conn.Close();
                //SqlDataAdapter adp = new SqlDataAdapter(cmd);
                //DataSet ds = new DataSet();
                //adp.Fill(ds);
                lblmsg.Visible = true;
                lblmsg.Text = "Record inserted successfully";
                Fillgridbatchtiming();


                if (CheckBox1.Checked == true)
                {
                    string te = "BatchworkingDay.aspx?id=" + ddlbatchmaster.SelectedValue;
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
                }
                cleartxt();
                pnladd.Visible = false;
                btnadd.Visible = true;
                lbladd.Text = "";
            }
        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.Text = "You don't have any more schedules available for this batch.";
        }
    }
    protected void ImageButton49_Click(object sender, EventArgs e)
    {
        string st1 = "select * from BatchTiming where BatchMasterId='" + ddlbatchmaster.SelectedValue + "' and Whid='" + ddlstorename.SelectedValue + "' and TimeScheduleMasterId='" + ddltimeschedule.SelectedValue + "' and ID!='" + ViewState["Id"] + "'";
        SqlCommand cmd1 = new SqlCommand(st1, conn);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataSet ds1 = new DataSet();
        DataTable dt1 = new DataTable();
        adp1.Fill(dt1);
        if (dt1.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Record already exists";
        }
        else
        {
            string time1 = "";
            string outdifftime1 = "";

            string time2 = "";
            string outdifftime2 = "";

            string time3 = "";
            string outdifftime3 = "";


            string temp1 = "";
            string temp2 = "";

            string temp3 = "";
            string temp4 = "";

            string temp5 = "";
            string temp6 = "";
            TimeSpan t3;
            TimeSpan t4;
            TimeSpan t5;
            TimeSpan t6;
            TimeSpan t1 = TimeSpan.Parse(txtstarttime.Text);
            TimeSpan t2 = TimeSpan.Parse(txtendtime.Text);
            if (txtbrakestart.Text != "" && txtbrakeend.Text != "")
            {
                t3 = TimeSpan.Parse(txtbrakestart.Text);
                t4 = TimeSpan.Parse(txtbrakeend.Text);
            }
            else
            {
                t3 = TimeSpan.Parse("00:00");
                t4 = TimeSpan.Parse("00:00");
            }
            if (txtsecondbrakestart.Text != "" && txtsecondbrakeend.Text != "")
            {
                t5 = TimeSpan.Parse(txtsecondbrakestart.Text);
                t6 = TimeSpan.Parse(txtsecondbrakeend.Text);
            }
            else
            {
                t5 = TimeSpan.Parse("00:00");
                t6 = TimeSpan.Parse("00:00");
            }
            time1 = t2.Subtract(t1).ToString();

            outdifftime1 = Convert.ToDateTime(time1).ToString("HH:MM");

            temp1 = Convert.ToDateTime(time1).ToString("HH");
            temp2 = Convert.ToDateTime(time1).ToString("mm");

            double main1 = Convert.ToDouble(temp1);
            double main2 = Convert.ToDouble(temp2);


            time2 = t4.Subtract(t3).ToString();
            outdifftime2 = Convert.ToDateTime(time2).ToString("HH:mm");

            temp3 = Convert.ToDateTime(time2).ToString("HH");
            temp4 = Convert.ToDateTime(time2).ToString("mm");
            double main3 = Convert.ToDouble(temp3);
            double main4 = Convert.ToDouble(temp4);

            time3 = t6.Subtract(t5).ToString();

            outdifftime3 = Convert.ToDateTime(time3).ToString("HH:mm");



            temp5 = Convert.ToDateTime(time3).ToString("HH");
            temp6 = Convert.ToDateTime(time3).ToString("mm");

            double main5 = Convert.ToDouble(temp5);
            double main6 = Convert.ToDouble(temp6);


            TimeSpan C1 = TimeSpan.Parse(time1);
            TimeSpan C2 = TimeSpan.Parse(time2);
            TimeSpan C3 = TimeSpan.Parse(time3);

            string diff1 = C3.Add(C2).ToString();

            TimeSpan C4 = TimeSpan.Parse(diff1);

            string Finalcal = C1.Subtract(C4).ToString();
            string finalvalue = Convert.ToDateTime(Finalcal).ToString("HH:mm");
            string lblnoofhour = finalvalue;



            string str = "UPDATE BatchTiming set BatchMasterId='" + ddlbatchmaster.SelectedValue + "',StartTime='" + txtstarttime.Text + "',EndTime='" + txtendtime.Text + "',FirstBreakStartTime='" + txtbrakestart.Text + "',FirstBreakEndTime='" + txtbrakeend.Text + "',SecondBreakStartTime='" + txtsecondbrakestart.Text + "',SecondBreakEndTime='" + txtsecondbrakeend.Text + "',TimeScheduleMasterId='" + ddltimeschedule.SelectedValue + "',Active='" + ddlstatus.SelectedValue + "',Whid='" + ddlstorename.SelectedValue + "',totalhours='" + lblnoofhour.ToString() + "' WHERE ID='" + ViewState["Id"] + "'";

            SqlCommand cmd = new SqlCommand(str, conn);
            if (conn.State.ToString() != "Open")
            {
                conn.Open();

            }

            cmd.ExecuteNonQuery();
            conn.Close();

            Fillgridbatchtiming();
            lblmsg.Visible = true;
            lblmsg.Text = "Record updated successfully";
            cleartxt();
            ImageButton49.Visible = false;
            ImageButton48.Visible = true;
            pnladd.Visible = false;
            btnadd.Visible = true;
            lbladd.Text = "";
            CheckBox1.Visible = true;
        }
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        cleartxt();
        pnladd.Visible = false;
        btnadd.Visible = true;
        lbladd.Text = "";
        CheckBox1.Visible = true;
    }

    protected void grdbatchtiming_RowCommand(object sender, GridViewCommandEventArgs e)
    {


    }

    public void edit()
    {
        string str2 = "Select EmployeeBatchMaster.BatchMasterId,EmployeeBatchMaster.Employeeid from BatchTiming inner join EmployeeBatchMaster on BatchTiming.BatchMasterId=EmployeeBatchMaster.Batchmasterid where  BatchTiming.BatchMasterId='" + ViewState["BatchId"].ToString() + "'";
        SqlCommand cmd2 = new SqlCommand(str2, conn);
        SqlDataAdapter adp2 = new SqlDataAdapter(cmd2);
        DataSet ds2 = new DataSet();
        adp2.Fill(ds2);


        if (ds2.Tables[0].Rows.Count == 0)
        {

            pnladd.Visible = true;
            btnadd.Visible = false;
            lbladd.Text = "Edit Batch Time";

            string str = "SELECT * from BatchTiming where ID='" + ViewState["Id"] + "'  ";

            SqlCommand cmd = new SqlCommand(str, conn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            adp.Fill(dt);

            string str11 = "select WareHouseId,Name from WareHouseMaster WHERE comid='" + Session["Comid"].ToString() + "' and [WareHouseMaster].Status='1' order by Name";
            SqlCommand cmd11 = new SqlCommand(str11, conn);
            SqlDataAdapter adp11 = new SqlDataAdapter(cmd11);
            DataSet ds11 = new DataSet();
            adp11.Fill(ds11);
            ddlstorename.DataSource = ds11;
            ddlstorename.DataTextField = "Name";
            ddlstorename.DataValueField = "WareHouseId";
            ddlstorename.DataBind();

            if (dt.Rows[0]["Whid"].ToString() != "")
            {
                ddlstorename.SelectedIndex = ddlstorename.Items.IndexOf(ddlstorename.Items.FindByValue(dt.Rows[0]["Whid"].ToString()));
            }

            Fillddlbatch();
            ddlbatchmaster.SelectedValue = dt.Rows[0]["BatchMasterId"].ToString();
            //txtdiaplayname.Text = dt.Rows[0]["DisplayName"].ToString();
            txtstarttime.Text = dt.Rows[0]["StartTime"].ToString();
            txtendtime.Text = dt.Rows[0]["EndTime"].ToString();
            //txteffectstart.Text = Convert.ToDateTime(dt.Rows[0]["EffectiveStartDate"].ToString()).ToShortDateString();
            //txteffectend.Text = Convert.ToDateTime(dt.Rows[0]["EffectiveEndDate"].ToString()).ToShortDateString();
            txtbrakestart.Text = dt.Rows[0]["FirstBreakStartTime"].ToString();
            txtbrakeend.Text = dt.Rows[0]["FirstBreakEndTime"].ToString();
            txtsecondbrakestart.Text = dt.Rows[0]["SecondBreakStartTime"].ToString();
            txtsecondbrakeend.Text = dt.Rows[0]["SecondBreakEndTime"].ToString();
            //  ddltimeschedule.SelectedValue = dt.Rows[0]["TimeScheduleMasterId"].ToString();

            string str1 = "select id,SchedulName from TimeSchedulMaster where id not in (select TimeScheduleMasterId from BatchTiming where BatchMasterId = '" + ddlbatchmaster.SelectedValue + "' ) or  id = '" + dt.Rows[0]["TimeScheduleMasterId"].ToString() + "' ";
            SqlCommand cmd1 = new SqlCommand(str1, conn);
            SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
            DataSet ds1 = new DataSet();
            adp1.Fill(ds1);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                ddltimeschedule.DataSource = ds1;
                ddltimeschedule.DataTextField = "SchedulName";
                ddltimeschedule.DataValueField = "ID";
                ddltimeschedule.DataBind();
            }
            ddltimeschedule.SelectedIndex = ddltimeschedule.Items.IndexOf(ddltimeschedule.Items.FindByValue(dt.Rows[0]["TimeScheduleMasterId"].ToString()));
            ddlstatus.SelectedIndex = ddlstatus.Items.IndexOf(ddlstatus.Items.FindByValue(dt.Rows[0]["Active"].ToString()));

            //string ck = dt.Rows[0]["Active"].ToString();
            //if (ck == "True")
            //{
            //    chkactive.Checked = true;

            //}
            //else
            //{
            //    chkactive.Checked = false;
            //}
            ImageButton49.Visible = true;
            ImageButton48.Visible = false;
            CheckBox1.Visible = false;
            //cleartxt();
        }
        else
        {
            //lblmsg.Visible = true;
            //lblmsg.Text = "You cannot edit this batch timing as there are employees listed in this batch. Please go to " + "<a href=\"EmployeeBatchManage.aspx\" style=\"font-size:14px; color:red; \" target=\"_blank\">" + "Employee Batch: Manage " + "</a>in order to remove employees from this batch.";
            CheckBox2.Checked = false;
            ModalPopupExtender145.Show();
        }
    }

    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        Fillgridbatchtiming();
    }
    protected void grdbatchtiming_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;

        Fillgridbatchtiming();
    }
    protected void grdbatchtiming_RowEditing1(object sender, GridViewEditEventArgs e)
    {

        lblmsg.Text = "";
        //grdbatchtiming.SelectedIndex = Convert.ToInt32(e.CommandArgument);
        ViewState["Id"] = grdbatchtiming.DataKeys[e.NewEditIndex].Value.ToString();
        //string index = grdbatchtiming.SelectedDataKey.Value.ToString();

        Label batchid = (Label)grdbatchtiming.Rows[e.NewEditIndex].FindControl("lblBatchId");
        ViewState["BatchId"] = batchid.Text;
        edit();



    }
    protected void fillstore()
    {


        DataTable ds = ClsStore.SelectStorename();
        ddlstorename.DataSource = ds;
        ddlstorename.DataTextField = "Name";
        ddlstorename.DataValueField = "WareHouseId";
        ddlstorename.DataBind();

        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlstorename.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }

        DropDownList3.DataSource = ds;
        DropDownList3.DataTextField = "Name";
        DropDownList3.DataValueField = "WareHouseId";
        DropDownList3.DataBind();

        DropDownList3.Items.Insert(0, "All");
        DropDownList3.Items[0].Value = "0";

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (Button2.Text == "Printable Version")
        {
            //pnlgrid.ScrollBars = ScrollBars.None;
            //pnlgrid.Height = new Unit("100%");

            grdbatchtiming.AllowPaging = false;
            grdbatchtiming.PageSize = 1000;
            Fillgridbatchtiming();

            Button2.Text = "Hide Printable Version";
            Button1.Visible = true;

            if (grdbatchtiming.Columns[12].Visible == true)
            {
                ViewState["editHide"] = "tt";
                grdbatchtiming.Columns[12].Visible = false;
            }
            if (grdbatchtiming.Columns[13].Visible == true)
            {
                ViewState["deleteHide"] = "tt";
                grdbatchtiming.Columns[13].Visible = false;
            }
        }
        else
        {
            //pnlgrid.ScrollBars = ScrollBars.Vertical;
            //pnlgrid.Height = new Unit(200);

            grdbatchtiming.AllowPaging = true;
            grdbatchtiming.PageSize = 20;
            Fillgridbatchtiming();

            Button2.Text = "Printable Version";
            Button1.Visible = false;

            if (ViewState["editHide"] != null)
            {
                grdbatchtiming.Columns[12].Visible = true;
            }
            if (ViewState["deleteHide"] != null)
            {
                grdbatchtiming.Columns[13].Visible = true;
            }
        }
    }
    protected void grdbatchtiming_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdbatchtiming.PageIndex = e.NewPageIndex;
        Fillgridbatchtiming();
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        Label1.Text = "";
        lblmsg.Text = "";
        pnladd.Visible = true;
        lbladd.Text = "Add New Batch Time";
        btnadd.Visible = false;
        CheckBox1.Visible = true;
    }
    protected void imgadd_Click(object sender, ImageClickEventArgs e)
    {
        string te = "BatchMaster.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void imgref_Click(object sender, ImageClickEventArgs e)
    {
        Fillddlbatch();
    }
    protected void ImageButton5_Click(object sender, EventArgs e)
    {
        ModalPopupExtender145.Hide();
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        if (CheckBox2.Checked == true)
        {
            string te = "BatchMaster.aspx";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        }
        ModalPopupExtender145.Hide();
    }
    protected void ddlfilterstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        Fillgridbatchtiming();
    }
    protected void chkallbatch_CheckedChanged(object sender, EventArgs e)
    {
        Fillgridbatchtiming();
    }
    protected void ddlbatchmaster_SelectedIndexChanged(object sender, EventArgs e)
    {
        Fillddltime();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        if (CheckBox3.Checked == true)
        {
            string te = "BatchMaster.aspx";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        }
        ModalPopupExtender1.Hide();
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1.Hide();
    }
    protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
    {

    }
}
