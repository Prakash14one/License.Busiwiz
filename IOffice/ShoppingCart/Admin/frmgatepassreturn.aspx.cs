using System;
using System.Web;
using System.Windows;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;

public partial class Manage_frmgatepassreturn : System.Web.UI.Page
{
    SqlConnection con;
    TimeSpan t1;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        }
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();
        ViewState["Compid"] = Session["Comid"].ToString();
        ViewState["UserName"] = Session["userid"].ToString();

        Page.Title = pg.getPageTitle(page);
        if (!IsPostBack)
        {
            Pagecontrol.dypcontrol(Page, page);
            if (Request.QueryString["gatepass"] != null)
            {
                Session["Gatepass"] = Request.QueryString["gatepass"].ToString();
            }

            lblDateTime.Text = System.DateTime.Now.ToShortDateString();
            string gtpsrq = "select Gatepassdetails.ID,Gatepassdetails.TimeReached,Gatepassdetails.TimeLeft,Gatepassdetails.TaskId,Gatepassdetails.GatePassId,GatepassDetails.ID,GatepassDetails.PurposeofVisit,GatePassTBL.ExpectedOutTime,GatepassTBL.ExpectedInTime,GatepassDetails.PartyID,EmployeeMaster.EmployeeName,Party_master.Compname as 'PartyName' from GatepassTBL  inner join GatepassDetails  on GatepassDetails.GatePassID=GatepassTBL.Id  inner join Party_master on GatepassDetails.PartyID = Party_master.PartyID inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID=GatepassTBL.EmployeeID where GatepassDetails.GatePassID = '" + Session["Gatepass"].ToString() + "' ";//GatePassNumber NOT NULL
            SqlCommand cmdrq = new SqlCommand(gtpsrq, con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmdrq);
            DataSet dsrq = new DataSet();
            adpt.Fill(dsrq);
            if (dsrq.Tables[0].Rows.Count > 0)
            {

                SqlDataAdapter daaa = new SqlDataAdapter("select Gatepassdetails.* from GatepassTBL inner join GatepassDetails  on GatepassDetails.GatePassID=GatepassTBL.Id  inner join Party_master on GatepassDetails.PartyID = Party_master.PartyID inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID=GatepassTBL.EmployeeID where GatepassDetails.GatePassID = '" + Session["Gatepass"].ToString() + "' and GatepassDetails.Detail IS NULL ", con);
                DataTable dtaaa = new DataTable();
                daaa.Fill(dtaaa);

                if (dtaaa.Rows.Count > 0)
                {
                    btnSubmit.Visible = true;
                    lblExpIn.Text = dsrq.Tables[0].Rows[0]["ExpectedInTime"].ToString();
                    lblExpOut.Text = dsrq.Tables[0].Rows[0]["ExpectedOutTime"].ToString();

                    lblactualouttime.Text = dsrq.Tables[0].Rows[0]["TimeReached"].ToString();
                    lblactualintime.Text = dsrq.Tables[0].Rows[0]["TimeLeft"].ToString();

                    lblEmployeeName.Text = dsrq.Tables[0].Rows[0]["EmployeeName"].ToString();
                    lblgatepass.Text = dsrq.Tables[0].Rows[0]["GatePassId"].ToString();
                    grdreturn.DataSource = dsrq;
                    grdreturn.DataBind();

                    DateTime tme1 = Convert.ToDateTime(lblExpIn.Text);
                    DateTime tme2 = Convert.ToDateTime(lblExpOut.Text);
                    TimeSpan t11 = ((Convert.ToDateTime(tme1)) - Convert.ToDateTime(tme2));
                    string a1 = t11.Hours.ToString();
                    string a2 = t11.Minutes.ToString();
                    lblTotalTime.Text = a1 + "  :  " + a2;


                    if (lblactualouttime.Text != "" && lblactualintime.Text != "")
                    {
                        DateTime tme11 = Convert.ToDateTime(lblactualintime.Text);
                        DateTime tme22 = Convert.ToDateTime(lblactualouttime.Text);
                        TimeSpan t111 = ((Convert.ToDateTime(tme11)) - Convert.ToDateTime(tme22));
                        string a11 = t111.Hours.ToString();
                        string a22 = t111.Minutes.ToString();
                        lblactualduration.Text = a11 + "  :  " + a22;
                    }
                    else
                    {
                        lblactualouttime.Text = "0";
                        lblactualintime.Text = "0";
                        lblactualduration.Text = "0";
                    }
                }
                else
                {
                    grdreturn.DataSource = null;
                    grdreturn.DataBind();
                    btnSubmit.Visible = false;
                }
            }
        }
    }



    protected void btnCalculate_Click(object sender, EventArgs e)
    {
        calculatebtn();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //calculatebtn();

        //if (lblMsg.Visible != true)
        //{
        //    if (Convert.ToInt32(ViewState["databasehour"]) > Convert.ToInt32(ViewState["tothour"]))
        //    {

        foreach (GridViewRow gdr in grdreturn.Rows)
        {
            Label lblMasterId = (Label)gdr.FindControl("lblId");
            TextBox txtTimeReached1 = (TextBox)gdr.FindControl("txtTimeReached");
            TextBox txtTimeLeft1 = (TextBox)gdr.FindControl("txtTimeLeft");
            Label lblDuration1 = (Label)gdr.FindControl("lblDuration");
            TextBox txtDetails1 = (TextBox)gdr.FindControl("txtDetails");
            CheckBox chk = (CheckBox)gdr.FindControl("chkCompleted");
            Label lblTask = (Label)gdr.FindControl("lblTaskID");
            //FileUpload fu1 = (FileUpload)gdr.FindControl("fu1control");

            //if (lblDuration1.Text != "")
            //{
            string returnupdate = "update GatepassDetails set Detail = '" + txtDetails1.Text + "',TotalDuration='" + lblactualduration.Text + "' where ID = '" + lblMasterId.Text + "'";
            //, TimeReached = '" + txtTimeReached1.Text + "', TimeLeft = '" + txtTimeLeft1.Text + "',TotalDuration = '" + lblDuration1.Text + "'
            SqlCommand cmdupdate = new SqlCommand(returnupdate, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdupdate.ExecuteNonQuery();
            con.Close();
            //if (fu1.HasFile)
            //{

            //}
            if (chk.Checked == true)
            {
                string updatetask = "update TaskMaster set Status = '" + 193 + "' where TaskId = '" + lblTask.Text + "'";
                SqlCommand cmdupdatetask = new SqlCommand(updatetask, con);

                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdupdatetask.ExecuteNonQuery();
            }
            con.Close();
            txtTimeReached1.Text = "";
            txtTimeLeft1.Text = "";
            lblDuration1.Text = "";
            txtDetails1.Text = "";
            txtTimeReached1.Text = "";
            txtTimeLeft1.Text = "";
            lblDuration1.Text = "";
            txtDetails1.Text = "";
            //}
            //else
            //{
            //    lblMsg.Visible = true;
            //    lblMsg.Text = "Timing is not proper";
            //    //btnSubmit.Visible = false;
            //    //btnCalculate.Visible = true;
            //}

        }
        lblMsg.Visible = true;
        lblMsg.Text = "Record inserted successfully";

        grdreturn.DataSource = null;
        grdreturn.DataBind();

        pnlmainentry.Visible = false;

        if (CheckBox1.Checked == true)
        {
            string te = "ReminderMaster.aspx";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        }
        //btnSubmit.Visible = false;

        //}
        //else if (Convert.ToInt32(ViewState["databasehour"]) >= Convert.ToInt32(ViewState["tothour"]))
        //{
        //    if (Convert.ToInt32(ViewState["databasemin"]) >= Convert.ToInt32(ViewState["totmin"]))
        //    {

        //        foreach (GridViewRow gdr in grdreturn.Rows)
        //        {

        //            Label lblMasterId = (Label)gdr.FindControl("lblId");
        //            TextBox txtTimeReached1 = (TextBox)gdr.FindControl("txtTimeReached");
        //            TextBox txtTimeLeft1 = (TextBox)gdr.FindControl("txtTimeLeft");
        //            Label lblDuration1 = (Label)gdr.FindControl("lblDuration");
        //            TextBox txtDetails1 = (TextBox)gdr.FindControl("txtDetails");
        //            CheckBox chk = (CheckBox)gdr.FindControl("chkCompleted");
        //            Label lblTask = (Label)gdr.FindControl("lblTaskID");


        //            string returnupdate = "update GatepassDetails set Detail = '" + txtDetails1.Text + "', TimeReached = '" + txtTimeReached1.Text + "', TimeLeft = '" + txtTimeLeft1.Text + "',TotalDuration = '" + lblDuration1.Text + "' where ID = '" + lblMasterId.Text + "'";
        //            SqlCommand cmdupdate = new SqlCommand(returnupdate, con);

        //            if (con.State.ToString() != "Open")
        //            {
        //                con.Open();
        //            }
        //            cmdupdate.ExecuteNonQuery();
        //            con.Close();
        //            if (chk.Checked == true)
        //            {
        //                string updatetask = "update TaskMaster set Status = '" + 193 + "' where TaskId = '" + lblTask.Text + "'";
        //                SqlCommand cmdupdatetask = new SqlCommand(updatetask, con);

        //                if (con.State.ToString() != "Open")
        //                {
        //                    con.Open();
        //                }
        //                cmdupdatetask.ExecuteNonQuery();
        //            }
        //            con.Close();
        //            txtTimeReached1.Text = "";
        //            txtTimeLeft1.Text = "";
        //            lblDuration1.Text = "";
        //            txtDetails1.Text = "";
        //            txtTimeReached1.Text = "";
        //            txtTimeLeft1.Text = "";
        //            lblDuration1.Text = "";
        //            txtDetails1.Text = "";
        //        }
        //        lblMsg.Visible = true;
        //        lblMsg.Text = "Record inserted successfully";
        //        grdreturn.DataSource = null;
        //        grdreturn.DataBind();
        //        pnlmainentry.Visible = false;
        //        //btnSubmit.Visible = false;
        //    }
        //}
        //else
        //{
        //    lblMsg.Visible = true;
        //    lblMsg.Text = "Timing is not proper";
        //    //btnSubmit.Visible = false;
        //    //btnCalculate.Visible = true;
        //}
        //}
    }
    protected void calculatebtn()
    {
        TimeInputted();
        int ach = 0;
        int tothour = 0;
        int totmin = 0;
        foreach (GridViewRow gtr in grdreturn.Rows)
        {
            FileUpload fu1control1 = (FileUpload)gtr.FindControl("fu1control");
            TextBox txtintime = (TextBox)(gtr.FindControl("txtTimeReached"));
            TextBox txtouttime = (TextBox)(gtr.FindControl("txtTimeLeft"));
            Label lbldiff = (Label)(gtr.FindControl("lblDuration"));
            TimeSpan t = ((Convert.ToDateTime(txtouttime.Text)) - Convert.ToDateTime(txtintime.Text));
            if (((Convert.ToDateTime(ViewState["OutTime"])) <= (Convert.ToDateTime(txtintime.Text))) && ((Convert.ToDateTime(ViewState["InTime"]) >= (Convert.ToDateTime((txtouttime.Text))))))
            {
                int a = t.Hours;
                int b = t.Minutes;
                string remain = a + " : " + b;
                lbldiff.Text = remain;
                tothour = tothour + a;
                totmin = totmin + b;
                btnSubmit.Visible = true;
                if (totmin > 59)
                {
                    tothour = tothour + (totmin / 60);
                    totmin = (totmin % 60);
                }
                ViewState["tothour"] = tothour;
                ViewState["totmin"] = totmin;
                lblMsg.Visible = false;
                btnCalculate.Visible = false;
                btnSubmit.Visible = true;
            }
            else
            {
                lblMsg.Text = "Timing is not proper";
                lblMsg.Visible = true;
                ach = ach + 1;
            }



        }
        if (ach > 0)
        {
            lblMsg.Text = "Timing is not proper";
            lblMsg.Visible = true;
            //btnSubmit.Visible = false;
            //btnCalculate.Visible = true;
        }
        else
        {
            btnSubmit.Visible = true;
        }
    }
    protected void TimeInputted()
    {
        string check = "select ExpectedOutTime,ExpectedInTime from gatepassTBL where Id = '" + Session["Gatepass"].ToString() + "'"; //Gatepassnumber
        SqlDataAdapter adptcheck = new SqlDataAdapter(check, con);
        DataSet ds = new DataSet();
        adptcheck.Fill(ds);
        string time1 = "";
        string time2 = "";
        if (ds.Tables[0].Rows.Count > 0)
        {
            time1 = ds.Tables[0].Rows[0]["ExpectedOutTime"].ToString();
            time2 = ds.Tables[0].Rows[0]["ExpectedInTime"].ToString();
            ViewState["OutTime"] = time1;
            ViewState["InTime"] = time2;

        }
        t1 = ((Convert.ToDateTime(time2)) - Convert.ToDateTime(time1));

        ViewState["databasehour"] = t1.Hours;
        ViewState["databasemin"] = t1.Minutes;
    }
}
