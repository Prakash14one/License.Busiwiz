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

public partial class ShoppingCart_Admin_Ipaddressverification : System.Web.UI.Page
{

    SqlConnection conn;
    SqlConnection connection;

    protected void Page_Load(object sender, EventArgs e)
    {


        PageConn pgcon = new PageConn();
        conn = pgcon.dynconn;
        connection = pgcon.dynconn;




        string pass1 = txtlicensekeyverificationpwd.Text;
        txtlicensekeyverificationpwd.Attributes.Add("Value", pass1);

        if (!IsPostBack)
        {


            if (Request.QueryString["cid"] != null && Request.QueryString["uid"] != null && Request.QueryString["ip"] != null)
            {
                string cid = Convert.ToString(Request.QueryString["cid"]);
                ViewState["cid"] = cid.ToString();
                string uid = Convert.ToString(Request.QueryString["uid"]);

                string stripa = "select Login_master.*,Party_master.Compname from  User_master inner join  Login_master on Login_master.UserID=User_master.UserID inner join Party_master on Party_master.PartyID=User_master.PartyID where Login_master.username='" + uid.ToString() + "'  ";
                SqlCommand cmdipa = new SqlCommand(stripa, conn);
                SqlDataAdapter adpipa = new SqlDataAdapter(cmdipa);
                DataTable dsipa = new DataTable();
                adpipa.Fill(dsipa);

                if (dsipa.Rows.Count > 0)
                {
                    ViewState["uid"] = dsipa.Rows[0]["UserID"].ToString();
                    ViewState["partyname"] = dsipa.Rows[0]["Compname"].ToString();

                }



                string ip = Convert.ToString(Request.QueryString["ip"]);
                ViewState["ip"] = ip.ToString();

                string strfinal = "select * from CompanyMaster where Compid='" + cid + "'";
                SqlDataAdapter adp = new SqlDataAdapter(strfinal, conn);
                DataTable ds = new DataTable();
                adp.Fill(ds);
                if (ds.Rows.Count > 0)
                {
                    ViewState["CompanyName"] = ds.Rows[0]["CompanyName"].ToString();

                    fillradio1();
                    RadioButtonList1_SelectedIndexChanged(sender, e);

                }


                lblzerocounter.Text = "0";

            }
        }

    }
    protected void fillradio1()
    {
        RadioButtonList1.Items[0].Text = "Would you like to add the IP address " + ViewState["ip"] + "   to the list of allowed IP addresses for the user name " + ViewState["partyname"].ToString() + " ?";
        RadioButtonList1.Items[1].Text = " Would you like to add the IP address " + ViewState["ip"] + " to the list of allowed IP addresses for the all the users of the company " + ViewState["CompanyName"].ToString() + " ?";
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedValue == "1")
        {
            RadioButtonList2.SelectedIndex = -1;
            pnloption2.Visible = true;
            pnlradio2option1.Visible = false;
            Panel2.Visible = false;
            pnlradio2option3.Visible = false;
            pnlradio2option2.Visible = false;

        }
        else
        {
            RadioButtonList2.SelectedIndex = -1;
            pnloption2.Visible = false;
            pnlradio2option1.Visible = true;
            Panel2.Visible = false;
            pnlradio2option3.Visible = false;
            pnlradio2option2.Visible = false;
        }
    }
    protected void RadioButtonList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList2.SelectedValue == "0")
        {
            pnlradio2option2.Visible = false;
            pnlradio2option3.Visible = false;
            pnlradio2option1.Visible = true;
            Panel2.Visible = false;

        }
        else if (RadioButtonList2.SelectedValue == "1")
        {
            pnlradio2option2.Visible = true;
            pnlradio2option3.Visible = false;
            pnlradio2option1.Visible = false;
            lblsecondoptindynamicipaddress.Text = ViewState["ip"].ToString();

            Panel2.Visible = true;


        }
        else
        {
            pnlradio2option2.Visible = false;
            pnlradio2option3.Visible = true;
            pnlradio2option1.Visible = false;
            Panel2.Visible = true;
            lbllicensekeyipaddress.Text = ViewState["ip"].ToString();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {


        int flag2 = attemptcount();
        if (flag2 == 1)
        {
            lblmsg.Text = "You have unsuccessfully tried to log in five times. Your account is now locked out for ten minutes.<br/>Please try again after ten minutes.";

        }
        else
        {




            SqlCommand cmdlogin = new SqlCommand("SelectUserLogin", conn);
            cmdlogin.CommandType = CommandType.StoredProcedure;
            cmdlogin.Parameters.Add(new SqlParameter("@UID", SqlDbType.NVarChar));
            cmdlogin.Parameters["@UID"].Value = txtuserid.Text;
            cmdlogin.Parameters.Add(new SqlParameter("@CD", SqlDbType.NVarChar));
            cmdlogin.Parameters["@CD"].Value = ViewState["cid"].ToString();

            cmdlogin.Parameters.Add(new SqlParameter("@Pas", SqlDbType.NVarChar));
            cmdlogin.Parameters["@Pas"].Value = ClsEncDesc.Encrypted(txtpassword.Text);
            SqlDataAdapter adplogin = new SqlDataAdapter(cmdlogin);
            DataTable dtloginuser = new DataTable();
            adplogin.Fill(dtloginuser);


            if (dtloginuser.Rows.Count > 0)
            {

                int flag = attemptcount();
                if (flag == 1)
                {
                    lblmsg.Text = "You have unsuccessfully tried to log in five times. Your account is now locked out for ten minutes.<br/>Please try again after ten minutes.";

                }
                else
                {

                    Session["userid"] = dtloginuser.Rows[0]["UserID"].ToString();

                    string strfinal = "select PartytTypeMaster.PartType from User_master inner join Party_master on Party_master.PartyID=User_master.PartyID inner join PartytTypeMaster on PartytTypeMaster.PartyTypeId=Party_master.PartyTypeId  where User_master.UserID='" + Session["userid"] + "' ";
                    SqlDataAdapter adp = new SqlDataAdapter(strfinal, conn);
                    DataTable ds = new DataTable();
                    adp.Fill(ds);

                    if (ds.Rows.Count > 0)
                    {
                        string stradmin = "Admin";
                        if (stradmin == ds.Rows[0]["PartType"].ToString())
                        {

                            lblmsg.Text = "";

                            string strquestion1 = "select SecurityQuestionMaster.* from SecurityQuestion inner join SecurityQuestionMaster on SecurityQuestionMaster.id=SecurityQuestion.SequrityQueId where SecurityQuestion.UserId='" + Session["userid"] + "' and SecurityQuestion.QuestionNo='1' ";
                            SqlDataAdapter adpquestion1 = new SqlDataAdapter(strquestion1, conn);
                            DataTable dsquestion1 = new DataTable();
                            adpquestion1.Fill(dsquestion1);

                            if (dsquestion1.Rows.Count > 0)
                            {



                                pnlsecurityquestion.Visible = true;
                                lblquestion1.Text = dsquestion1.Rows[0]["QueName"].ToString();
                                string successfull = "1";
                                InsertIpLog(successfull);

                                lblzerocounter.Text = "0";
                                lblmsg.Text = "";
                                Label21.Text = "";



                            }
                            else
                            {

                                pnlsecurityquestion.Visible = false;
                                lblmsg.Text = "Sorry You have set no Security questions /answers , So you can not use this option. Please try other option.";
                                string successfull = "0";
                                InsertIpLog(successfull);
                                counter();


                            }


                        }
                        else
                        {
                            pnlsecurityquestion.Visible = false;
                            lblmsg.Text = "The user ID and password that you have entered is not correct. Please verify the information you have entered and try again.";
                            string successfull = "0";
                            InsertIpLog(successfull);
                            counter();

                        }

                    }


                    else
                    {
                        pnlsecurityquestion.Visible = false;
                        lblmsg.Text = "The user ID and password that you have entered is not correct. Please verify the information you have entered and try again.";
                        string successfull = "0";
                        InsertIpLog(successfull);
                        counter();
                    }

                }
            }
            else
            {
                int flag3 = attemptcount();
                if (flag3 == 1)
                {
                    lblmsg.Text = "You have unsuccessfully tried to log in five times. Your account is now locked out for ten minutes.<br/>Please try again after ten minutes."; ;

                }
                else
                {
                    pnlsecurityquestion.Visible = false;
                    lblmsg.Text = "The user ID and password that you have entered is not correct. Please verify the information you have entered and try again.";

                    string successfull = "0";
                    InsertIpLog(successfull);
                    counter();
                }

            }
        }

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        int flag2 = attemptcount();
        if (flag2 == 1)
        {
            lblsecuritylblmsg.Text = "You have unsuccessfully tried to log in five times. Your account is now locked out for ten minutes.<br/>Please try again after ten minutes.";

        }
        else
        {


            string strquestion1 = "select SecurityQuestionMaster.* from SecurityQuestion inner join SecurityQuestionMaster on SecurityQuestionMaster.id=SecurityQuestion.SequrityQueId where SecurityQuestion.UserId='" + Session["userid"] + "' and SecurityQuestion.QuestionNo='1' and Answer='" + txtanswer1.Text + "' ";
            SqlDataAdapter adpquestion1 = new SqlDataAdapter(strquestion1, conn);
            DataTable dsquestion1 = new DataTable();
            adpquestion1.Fill(dsquestion1);

            if (dsquestion1.Rows.Count > 0)
            {
                int flag = attemptcount();
                if (flag == 1)
                {
                    lblsecuritylblmsg.Text = "You have unsuccessfully tried to log in five times. Your account is now locked out for ten minutes.<br/>Please try again after ten minutes.";
                }
                else
                {

                    string strquestion2 = "select SecurityQuestionMaster.* from SecurityQuestion inner join SecurityQuestionMaster on SecurityQuestionMaster.id=SecurityQuestion.SequrityQueId where SecurityQuestion.UserId='" + Session["userid"] + "' and SecurityQuestion.QuestionNo='2' ";
                    SqlDataAdapter adpquestion2 = new SqlDataAdapter(strquestion2, conn);
                    DataTable dsquestion2 = new DataTable();
                    adpquestion2.Fill(dsquestion2);

                    if (dsquestion2.Rows.Count > 0)
                    {
                        pnlsecurityquestion2.Visible = true;
                        lblquestion2.Text = dsquestion2.Rows[0]["QueName"].ToString();
                        string successfull = "1";
                        InsertIpLog(successfull);
                        lblzerocounter.Text = "0";
                        lblsecuritylblmsg.Text = "";
                        Label21.Text = "";


                    }
                    else
                    {
                        lblsecuritylblmsg.Text = "Sorry have not set other Security questions /answers , So you can not use this option. Please try other option";
                        string successfull = "0";
                        InsertIpLog(successfull);
                        counter();
                    }


                }
            }
            else
            {
                lblsecuritylblmsg.Text = "Wrong Answer";
                string successfull = "0";
                InsertIpLog(successfull);
                counter();

            }


        }


    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        int flag2 = attemptcount();
        if (flag2 == 1)
        {
            lblsecuritylblmsg.Text = "You have unsuccessfully tried to log in five times. Your account is now locked out for ten minutes.<br/>Please try again after ten minutes.";

        }
        else
        {


            string strquestion1 = "select SecurityQuestionMaster.* from SecurityQuestion inner join SecurityQuestionMaster on SecurityQuestionMaster.id=SecurityQuestion.SequrityQueId where SecurityQuestion.UserId='" + Session["userid"] + "' and SecurityQuestion.QuestionNo='2' and Answer='" + txtanswer2.Text + "' ";
            SqlDataAdapter adpquestion1 = new SqlDataAdapter(strquestion1, conn);
            DataTable dsquestion1 = new DataTable();
            adpquestion1.Fill(dsquestion1);

            if (dsquestion1.Rows.Count > 0)
            {
                int flag = attemptcount();
                if (flag == 1)
                {
                    lblsecuritylblmsg.Text = "You have unsuccessfully tried to log in five times. Your account is now locked out for ten minutes.<br/>Please try again after ten minutes.";
                }
                else
                {

                    string strquestion2 = "select SecurityQuestionMaster.* from SecurityQuestion inner join SecurityQuestionMaster on SecurityQuestionMaster.id=SecurityQuestion.SequrityQueId where SecurityQuestion.UserId='" + Session["userid"] + "' and SecurityQuestion.QuestionNo='3' ";
                    SqlDataAdapter adpquestion2 = new SqlDataAdapter(strquestion2, conn);
                    DataTable dsquestion2 = new DataTable();
                    adpquestion2.Fill(dsquestion2);

                    if (dsquestion2.Rows.Count > 0)
                    {
                        pnlsecurityquestion3.Visible = true;
                        lblquestion3.Text = dsquestion2.Rows[0]["QueName"].ToString();
                        string successfull = "1";
                        InsertIpLog(successfull);
                        lblzerocounter.Text = "0";
                        lblsecuritylblmsg.Text = "";
                        Label21.Text = "";


                    }
                    else
                    {
                        lblsecuritylblmsg.Text = "Sorry have not set other Security questions /answers , So you can not use this option. Please try other option";
                        string successfull = "0";
                        InsertIpLog(successfull);
                        counter();
                    }

                }

            }
            else
            {
                lblsecuritylblmsg.Text = "Wrong Answer";
                string successfull = "0";
                InsertIpLog(successfull);
                counter();
            }

        }

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string stripmasterid = " select * from IpControlMastertbl where CID='" + ViewState["cid"] + "' ";
        SqlDataAdapter adpipmasterid = new SqlDataAdapter(stripmasterid, conn);
        DataTable dsipmasterid = new DataTable();
        adpipmasterid.Fill(dsipmasterid);

        if (dsipmasterid.Rows.Count > 0)
        {

            string insertip = "insert into IpControldetailtbl (IpcontrolId,Cidwise,Userwise,UserId,Ipaddress) values ('" + dsipmasterid.Rows[0]["IpcontrolId"].ToString() + "','1','0','0','" + ViewState["ip"] + "')  ";

            SqlCommand cmdinsertip = new SqlCommand(insertip, conn);
            if (conn.State.ToString() != "Open")
            {
                conn.Open();
            }
            cmdinsertip.ExecuteNonQuery();
            conn.Close();
            pnlconfirmationmessage.Visible = true;
            lblsecurityquestionconfirmation.Text = "You have successfully answered all the secutiy questions correctly.";
            lblsecurityquestionconfirmationipname.Text = ViewState["ip"].ToString();
            lblsecurityquestionconfirmation2.Text = "has now been added to the list of allowed IP addresses for the whole company.";

            string successfull = "1";
            InsertIpLog(successfull);


        }


    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        int flag2 = attemptcount();
        if (flag2 == 1)
        {
            lblsecuritylblmsg.Text = "You have unsuccessfully tried to log in five times. Your account is now locked out for ten minutes.<br/>Please try again after ten minutes.";

        }
        else
        {

            string strquestion1 = "select SecurityQuestionMaster.* from SecurityQuestion inner join SecurityQuestionMaster on SecurityQuestionMaster.id=SecurityQuestion.SequrityQueId where SecurityQuestion.UserId='" + Session["userid"] + "' and SecurityQuestion.QuestionNo='3' and Answer='" + txtanswer3.Text + "' ";
            SqlDataAdapter adpquestion1 = new SqlDataAdapter(strquestion1, conn);
            DataTable dsquestion1 = new DataTable();
            adpquestion1.Fill(dsquestion1);

            if (dsquestion1.Rows.Count > 0)
            {
                int flag = attemptcount();

                if (flag == 1)
                {
                    lblsecuritylblmsg.Text = "You have unsuccessfully tried to log in five times. Your account is now locked out for ten minutes.<br/>Please try again after ten minutes.";

                }
                else
                {
                    pnlfinalsubmit.Visible = true;
                    string successfull = "1";
                    InsertIpLog(successfull);
                    lblzerocounter.Text = "0";

                    lblsecuritylblmsg.Text = "";
                    Label21.Text = "";
                }
            }
            else
            {
                lblsecuritylblmsg.Text = "Wrong Answer";
                pnlfinalsubmit.Visible = false;
                string successfull = "0";
                InsertIpLog(successfull);
                counter();
            }
        }
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        int flag2 = attemptcount();
        if (flag2 == 1)
        {
            lbllicensekeyverification.Text = "You have unsuccessfully tried to log in five times. Your account is now locked out for ten minutes.<br/>Please try again after ten minutes.";

        }
        else
        {

            SqlCommand cmdlogin = new SqlCommand("SelectUserLogin", conn);
            cmdlogin.CommandType = CommandType.StoredProcedure;
            cmdlogin.Parameters.Add(new SqlParameter("@UID", SqlDbType.NVarChar));
            cmdlogin.Parameters["@UID"].Value = txtlicensekeyverificationuid.Text;
            cmdlogin.Parameters.Add(new SqlParameter("@CD", SqlDbType.NVarChar));
            cmdlogin.Parameters["@CD"].Value = txtlicensekeyverificationcid.Text;
            cmdlogin.Parameters.Add(new SqlParameter("@Pas", SqlDbType.NVarChar));
            cmdlogin.Parameters["@Pas"].Value = ClsEncDesc.Encrypted(txtlicensekeyverificationpwd.Text);
            SqlDataAdapter adplogin = new SqlDataAdapter(cmdlogin);
            DataTable dtloginuser = new DataTable();
            adplogin.Fill(dtloginuser);



            if (dtloginuser.Rows.Count > 0)
            {
                int flag = attemptcount();
                if (flag == 1)
                {
                    lbllicensekeyverification.Text = "You have unsuccessfully tried to log in five times. Your account is now locked out for ten minutes.<br/>Please try again after ten minutes.";

                }
                else
                {

                    Session["userid"] = dtloginuser.Rows[0]["UserID"].ToString();

                    string strfinal = "select PartytTypeMaster.PartType from User_master inner join Party_master on Party_master.PartyID=User_master.PartyID inner join PartytTypeMaster on PartytTypeMaster.PartyTypeId=Party_master.PartyTypeId  where User_master.UserID='" + Session["userid"] + "' ";
                    SqlDataAdapter adp = new SqlDataAdapter(strfinal, conn);
                    DataTable ds = new DataTable();
                    adp.Fill(ds);

                    if (ds.Rows.Count > 0)
                    {
                        string stradmin = "Admin";
                        if (stradmin == ds.Rows[0]["PartType"].ToString())
                        {
                            pnllicensekeynameoption.Visible = true;
                            lbllicensekeyipaddress.Text = ViewState["ip"].ToString();

                            string successfull = "1";
                            InsertIpLog(successfull);
                            lblzerocounter.Text = "0";
                            lbllicensekeyverification.Text = "";
                            Label21.Text = "";


                        }
                        else
                        {
                            pnllicensekeynameoption.Visible = false;
                            lbllicensekeyverification.Text = "The user ID and password that you have entered is not correct. Please verify the information you have entered and try again.";
                            string successfull = "0";
                            InsertIpLog(successfull);

                            counter();
                        }

                    }
                    else
                    {
                        pnllicensekeynameoption.Visible = false;
                        lbllicensekeyverification.Text = "The user ID and password that you have entered is not correct. Please verify the information you have entered and try again.";
                        string successfull = "0";
                        InsertIpLog(successfull);
                        counter();

                    }


                }
            }
            else
            {
                pnllicensekeynameoption.Visible = false;
                lbllicensekeyverification.Text = "The user ID and password that you have entered is not correct. Please verify the information you have entered and try again.";
                string successfull = "0";
                InsertIpLog(successfull);
                counter();

            }
        }
    }
    protected void Button7_Click(object sender, EventArgs e)
    {
        int flag2 = attemptcount();
        if (flag2 == 1)
        {
            lbllicensekeyverification.Text = "You have unsuccessfully tried to log in five times. Your account is now locked out for ten minutes.<br/>Please try again after ten minutes.";

        }
        else
        {


            SqlCommand cmd = new SqlCommand("SELECT LicenseMaster.CompanyId, LicenseMaster.LicenseKey,LicenseMaster.LicenseDate,CompanyMaster.CompanyName, CompanyMaster.AdminId, CompanyMaster.Password,CompanyMaster.Websiteurl,CompanyMaster.active, " +
                             " HostDetail.SqlServerName, HostDetail.SqlServerUName, HostDetail.SqlServerUPassword, HostDetail.DatabaseName " +
                             " FROM CompanyMaster LEFT OUTER JOIN " +
                             " HostDetail ON CompanyMaster.CompanyId = HostDetail.CompanyId LEFT OUTER JOIN " +
                             " LicenseMaster ON CompanyMaster.CompanyId = LicenseMaster.CompanyId where LicenseMaster.LicenseKey='" + txtlicensekey.Text + "' and CompanyMaster.AdminId ='" + txtlicensekeyverificationuid.Text + "' and CompanyMaster.Password='" + txtlicensekeyverificationpwd.Text + "' and CompanyMaster.CompanyLoginId='" + ViewState["cid"].ToString() + "'  and CompanyMaster.active='1'", PageConn.licenseconn());
            SqlDataAdapter dtp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dtp.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                int flag = attemptcount();

                if (flag == 1)
                {
                    lbllicensekeyverification.Text = "You have unsuccessfully tried to log in five times. Your account is now locked out for ten minutes.<br/>Please try again after ten minutes.";

                }
                else
                {
                    string stripmasterid = " select * from IpControlMastertbl where CID='" + ViewState["cid"] + "' ";
                    SqlDataAdapter adpipmasterid = new SqlDataAdapter(stripmasterid, conn);
                    DataTable dsipmasterid = new DataTable();
                    adpipmasterid.Fill(dsipmasterid);

                    if (dsipmasterid.Rows.Count > 0)
                    {

                        string insertip = "insert into IpControldetailtbl (IpcontrolId,Cidwise,Userwise,UserId,Ipaddress) values ('" + dsipmasterid.Rows[0]["IpcontrolId"].ToString() + "','1','0','0','" + ViewState["ip"] + "')  ";

                        SqlCommand cmdinsertip = new SqlCommand(insertip, conn);
                        if (conn.State.ToString() != "Open")
                        {
                            conn.Open();
                        }
                        cmdinsertip.ExecuteNonQuery();
                        conn.Close();
                        pnlconfirmationmessagelicense.Visible = true;

                        lbllicenseconfirmation123.Text = "You have successfully added the license key.";
                        lbllicenseconfirmation123456.Text = ViewState["ip"].ToString();
                        lbllicenseconfirmation123456789.Text = " has now been added to the list of allowed IP addresses for the whole company.";
                        string successfull = "1";
                        InsertIpLog(successfull);
                        lblzerocounter.Text = "0";
                        LinkButton1.Visible = true;

                    }

                }
            }
            else
            {
                pnlconfirmationmessagelicense.Visible = true;

                lbllicenseconfirmation123.Text = " The Licensing information provided by you does not match our records. Kindly contact techsupport@busiwiz.com";

                lbllicenseconfirmation123456.Text = "";
                lbllicenseconfirmation123456789.Text = "";
                string successfull = "0";
                InsertIpLog(successfull);
                counter();
                LinkButton1.Visible = false;


            }

        }
    }

    protected void Button8_Click(object sender, EventArgs e)
    {
        string strinsert = "insert into Ipaddresschangerequesttbl(datetimeemailgenerated,emailgenerateduserid,emailgenerated,ipaddresssuceessfullyadded)" +
                                     " values('" + System.DateTime.Now.ToString() + "','" + ViewState["uid"] + "','1','0')";
        SqlCommand cmdinsert = new SqlCommand(strinsert, conn);
        if (conn.State.ToString() != "Open")
        {
            connection.Open();
        }
        cmdinsert.ExecuteNonQuery();
        conn.Close();


        string stripmasterid = " select EmployeeMaster.* from EmployeeMaster inner join Party_master on Party_master.PartyID=EmployeeMaster.PartyID inner join PartytTypeMaster on PartytTypeMaster.PartyTypeId=Party_master.PartyTypeId  where Party_master.id='" + ViewState["cid"] + "' and PartytTypeMaster.PartType='Admin' ";
        SqlDataAdapter adpipmasterid = new SqlDataAdapter(stripmasterid, conn);
        DataTable dsipmasterid = new DataTable();
        adpipmasterid.Fill(dsipmasterid);

        if (dsipmasterid.Rows.Count > 0)
        {

            foreach (DataRow dtope in dsipmasterid.Rows)
            {

                string To = dtope["Email"].ToString();
                string whid = dtope["Whid"].ToString();

                if (To != "" && whid != "")
                {
                    string strmasteremailid = "select * from CompanyWebsitMaster  where WHId='" + whid + "' ";
                    SqlDataAdapter adpmasteremailid = new SqlDataAdapter(strmasteremailid, conn);
                    DataTable dsmasteremailid = new DataTable();
                    adpmasteremailid.Fill(dsmasteremailid);

                    if (dsmasteremailid.Rows.Count > 0)
                    {
                        string maxid = "select MAX(Id) as Id from Ipaddresschangerequesttbl where emailgenerateduserid='" + ViewState["uid"] + "'";
                        SqlCommand cmd = new SqlCommand(maxid, connection);
                        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                        DataTable ds = new DataTable();
                        adpt.Fill(ds);

                        ViewState["maxid"] = ds.Rows[0]["Id"].ToString();

                        string MasterEmailId = dsmasteremailid.Rows[0]["MasterEmailId"].ToString();
                        string MasterEmailIdpassword = dsmasteremailid.Rows[0]["EmailMasterLoginPassword"].ToString();
                        string OutGoingMailServer = dsmasteremailid.Rows[0]["OutGoingMailServer"].ToString();
                        string Displayname = dsmasteremailid.Rows[0]["EmailSentDisplayName"].ToString();

                        if (MasterEmailId != "" && MasterEmailIdpassword != "" && OutGoingMailServer != "")
                        {
                            sendmail(To, whid, MasterEmailId, MasterEmailIdpassword, OutGoingMailServer, Displayname, ViewState["maxid"].ToString());

                            lblemailconfirmation.Text = "An email has been successfully sent to admin.";
                        }
                    }

                }


            }


        }
    }

    public void sendmail(string To, string whid, string MasterEmailId, string MasterEmailIdpassword, string OutGoingMailServer, string Displayname, string maxid)
    {        
        string strbus = "select Name from WarehouseMaster where WarehouseID='" + whid.ToString() + "'";
        SqlDataAdapter da = new SqlDataAdapter(strbus, conn);
        DataTable dt = new DataTable();
        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            if (Convert.ToString(dt.Rows[0]["Name"]) != "")
            {
                ViewState["buss"] = Convert.ToString(dt.Rows[0]["Name"]);
            }
            else
            {
                ViewState["buss"] = "";
            }
        }

        string ADDRESSEX = "SELECT distinct CompanyMaster.CompanyLogo, CompanyMaster.CompanyName,CompanyWebsitMaster.Sitename,CompanyWebsitMaster.MasterEmailId,CompanyWebsitMaster.EmailMasterLoginPassword,CompanyWebsitMaster.OutGoingMailServer, CompanyWebsitMaster.EmailSentDisplayName,CompanyWebsitMaster.SiteUrl,CompanyWebsiteAddressMaster.Address1,CompanyWebsiteAddressMaster.Address2,CompanyWebsiteAddressMaster.Phone1, CompanyWebsiteAddressMaster.Phone2, CompanyWebsiteAddressMaster.TollFree1, CompanyWebsiteAddressMaster.Fax,CompanyWebsiteAddressMaster.Email,CompanyMaster.CompanyId,CompanyWebsitMaster.WHid FROM  CompanyMaster LEFT OUTER JOIN AddressTypeMaster RIGHT OUTER JOIN CompanyWebsiteAddressMaster ON AddressTypeMaster.AddressTypeMasterId = CompanyWebsiteAddressMaster.AddressTypeMasterId RIGHT OUTER JOIN CompanyWebsitMaster ON CompanyWebsiteAddressMaster.CompanyWebsiteMasterId = CompanyWebsitMaster.CompanyWebsiteMasterId ON CompanyMaster.CompanyId = CompanyWebsitMaster.CompanyId where CompanyMaster.Compid='" + ViewState["cid"] + "' and WHId='" + whid.ToString() + "'";
        SqlCommand cmd = new SqlCommand(ADDRESSEX, conn);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);

        StringBuilder HeadingTable = new StringBuilder();
        HeadingTable.Append("<table width=\"100%\"> ");

        SqlDataAdapter dalogo = new SqlDataAdapter("select logourl from CompanyWebsitMaster where whid='" + whid.ToString() + "'", conn);
        DataTable dtlogo = new DataTable();
        dalogo.Fill(dtlogo);

        HeadingTable.Append("<tr><td width=\"50%\" style=\"padding-left:10px\" align=\"left\" > <img src=\"../images/" + dtlogo.Rows[0]["logourl"].ToString() + "\" \"border=\"0\" Width=\"200px\" Height=\"125px\" / > </td><td style=\"padding-left:100px\" width=\"50%\" align=\"left\"><b><span style=\"color: #996600\">" + ds.Rows[0]["CompanyName"].ToString() + "</span></b><Br>" + ds.Rows[0]["Address1"].ToString() + "<Br>" + ds.Rows[0]["Address2"].ToString() + "<Br>Toll Free : " + ds.Rows[0]["TollFree1"].ToString() + "<Br>Phone : " + ds.Rows[0]["Phone1"].ToString() + "<Br>Fax :" + ds.Rows[0]["Fax"].ToString() + "<Br>Email :" + ds.Rows[0]["Email"].ToString() + "<Br>Website :" + ds.Rows[0]["SiteUrl"].ToString() + " </td></tr>  ");
        HeadingTable.Append("</table> ");

        string welcometext = getWelcometext();
        string radiooption = "0";
        string AccountInfo = "";
        if (RadioButtonList1.SelectedValue == "0")
        {
            radiooption = "0";
            AccountInfo = " <span style=\"font-size: 10pt; color: #000000; font-family: Arial\"> The user " + ViewState["partyname"].ToString() + "  would like to add the following IP address " + ViewState["ip"] + " to the list of allowed IP addresses for the user only " + ViewState["partyname"].ToString() + ".</span>";
        }
        else
        {
            radiooption = "1";
            AccountInfo = " <span style=\"font-size: 10pt; color: #000000; font-family: Arial\"> The user " + ViewState["partyname"].ToString() + "  would like to add the following IP address " + ViewState["ip"] + " to the list of allowed IP addresses for the whole company " + ViewState["CompanyName"] + ".</span>";
        }
        string currentdate = " <span style=\"font-size: 10pt; color: #000000; font-family: Arial\">" + System.DateTime.Now.ToShortDateString() + " </span>";



        string str1 = " <span style=\"font-size: 10pt; color: #000000; font-family: Arial\"><br><br>If you would like to grant website access from this IP address please click <a href=http://" + Request.Url.Host.ToString() + "/Emailverification.aspx?cid=" + ClsEncDesc.Encrypted(ViewState["cid"].ToString()) + "&uid=" + ViewState["uid"].ToString() + "&ip=" + ViewState["ip"].ToString() + "&option=" + radiooption.ToString() + "&maxid=" + maxid.ToString() + " > here </a></span>.";
        string str2 = "<span style=\"font-size: 10pt; color: #000000; font-family: Arial\"><br><br>Or, copy and paste the below link, into your browser: <br><br>http://" + Request.Url.Host.ToString() + "/Emailverification.aspx?cid=" + ClsEncDesc.Encrypted(ViewState["cid"].ToString()) + "&uid=" + ViewState["uid"].ToString() + "&ip=" + ViewState["ip"].ToString() + "&option=" + radiooption.ToString() + "&maxid=" + maxid.ToString() + " </span>";
        string str3 = "<span style=\"font-size: 10pt; color: #000000; font-family: Arial\"><br><br>If you do not want to proceed with this access, please disregard this email. ";
        string body = "<br>" + HeadingTable + "<br>Dear <strong><span style=\"color: #996600\"> " + ViewState["buss"].ToString() + " </span></strong> ,<br><br>" + welcometext.ToString() + " <br>" + AccountInfo.ToString() + "<br> " + str1 + " <br>" + str2 + " <br><br>" + str3 + " <br> <br> <span style=\"font-size: 10pt; color: #000000; font-family: Arial\"><br>Thank you,<br><br>Security Department<br> " + ViewState["CompanyName"] + "<Br>" + ds.Rows[0]["Address1"].ToString() + "<Br>" + ds.Rows[0]["Address2"].ToString() + "<Br>Toll Free : " + ds.Rows[0]["TollFree1"].ToString() + "<Br>Phone : " + ds.Rows[0]["Phone1"].ToString() + "<Br>Fax :" + ds.Rows[0]["Fax"].ToString() + "<Br>Email :" + ds.Rows[0]["Email"].ToString() + "<Br>Website :" + ds.Rows[0]["SiteUrl"].ToString() + "</span><br><strong><span style=\"color: #996600\"> ";

        MailAddress to = new MailAddress(To);
        MailAddress from = new MailAddress("" + MasterEmailId + "", "" + Displayname + "");
        MailMessage objEmail = new MailMessage(from, to);
        objEmail.Subject = "Request To Add IP Address To Allowed List - " + ViewState["buss"].ToString() + " ";
        objEmail.Body = body.ToString();
        objEmail.IsBodyHtml = true;
        objEmail.Priority = MailPriority.Normal;
        SmtpClient client = new SmtpClient();
        client.Credentials = new NetworkCredential("" + MasterEmailId + "", "" + MasterEmailIdpassword + "");
        client.Host = OutGoingMailServer;
        client.Send(objEmail);


    }

    public string getWelcometext()
    {


        string str = "SELECT EmailContentMaster.EmailContent, EmailContentMaster.EntryDate, CompanyWebsitMaster.SiteUrl, EmailTypeMaster.EmailTypeId " +
                    " FROM CompanyWebsitMaster INNER JOIN " +
                      " EmailContentMaster ON CompanyWebsitMaster.CompanyWebsiteMasterId = EmailContentMaster.CompanyWebsiteMasterId INNER JOIN " +
                      " EmailTypeMaster ON EmailContentMaster.EmailTypeId = EmailTypeMaster.EmailTypeId " +
                    " WHERE     (EmailTypeMaster.EmailTypeId = 1)  and (EmailTypeMaster.Compid='" + ViewState["cid"] + "')" +
                    " ORDER BY EmailContentMaster.EntryDate DESC ";
        SqlCommand cmd = new SqlCommand(str, conn);

        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        string welcometext = "";
        if (ds.Rows.Count > 0)
        {
            welcometext = ds.Rows[0]["EmailContent"].ToString();

        } return welcometext;

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        string te = "http://" + Request.Url.Host.ToString() + "/Shoppingcart/Admin/Shoppingcartlogin.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        string te = "http://" + Request.Url.Host.ToString() + "/Shoppingcart/Admin/Shoppingcartlogin.aspx";

        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }

    public void InsertIpLog(string successful)
    {
        string str3 = "insert into IpChangeAttemptLog(UserId,Datetime,Ipaddress,Successful)values('" + ViewState["uid"].ToString() + "','" + System.DateTime.Now.ToString() + "','" + ViewState["ip"].ToString() + "','" + successful + "')";
        if (conn.State.ToString() == "Open")
        {
            conn.Close();
        }
        conn.Open();
        SqlCommand cmd1 = new SqlCommand(str3, conn);
        cmd1.ExecuteNonQuery();
        conn.Close();

    }

    protected int attemptcount()
    {

        int flag = 0;

        string stripa = "select Top(5)* from  IpChangeAttemptLog where  UserId='" + ViewState["uid"].ToString() + "' order by Id desc  ";
        SqlCommand cmdipa = new SqlCommand(stripa, conn);
        SqlDataAdapter adpipa = new SqlDataAdapter(cmdipa);
        DataTable dsipa = new DataTable();
        adpipa.Fill(dsipa);

        int successfullcounter = 0;

        foreach (DataRow dtope in dsipa.Rows)
        {


            if (Convert.ToBoolean(dtope["Successful"].ToString()) == true)
            {
                successfullcounter = 1;

            }

        }

        if (successfullcounter == 0)
        {
            if (dsipa.Rows.Count == 4)
            {
                Label21.Text = "Please note that after 5 unsuccessful attempts, you will be locked out for 10 minutes before you can try again. ";

            }
            else
            {

                if (dsipa.Rows.Count >= 5)
                {

                    string time1 = Convert.ToDateTime(dsipa.Rows[0]["Datetime"].ToString()).ToString("HH:mm");
                    string time2 = Convert.ToDateTime(dsipa.Rows[4]["Datetime"].ToString()).ToString("HH:mm");

                    TimeSpan t1 = TimeSpan.Parse(time1.ToString());
                    TimeSpan t2 = TimeSpan.Parse(time2.ToString());

                    string time3 = t1.Subtract(t2).ToString();

                    string str = "00:10";

                    if (Convert.ToDateTime(dsipa.Rows[0]["Datetime"].ToString()).ToShortDateString() == Convert.ToDateTime(dsipa.Rows[4]["Datetime"].ToString()).ToShortDateString())
                    {

                        TimeSpan master1 = TimeSpan.Parse(time3.ToString());
                        TimeSpan master2 = TimeSpan.Parse(str.ToString());


                        if (master1 < master2)
                        {


                            if (Convert.ToDateTime(dsipa.Rows[0]["Datetime"].ToString()).ToShortDateString() == Convert.ToDateTime(System.DateTime.Now.ToString()).ToShortDateString())
                            {

                                string time4 = Convert.ToDateTime(System.DateTime.Now.ToString()).ToString("HH:mm");
                                TimeSpan t3 = TimeSpan.Parse(time4.ToString());

                                string time5 = t3.Subtract(t1).ToString();
                                string str1 = "00:10";

                                TimeSpan master3 = TimeSpan.Parse(time5.ToString());
                                TimeSpan master4 = TimeSpan.Parse(str1.ToString());

                                if (master3 < master4)
                                {

                                    flag = 1;

                                }
                                else
                                {
                                    flag = 0;
                                }

                            }
                            else
                            {
                                flag = 0;
                            }

                        }

                        else
                        {
                            flag = 0;

                        }
                    }
                    else
                    {
                        flag = 0;

                    }




                }
            }

        }

        return flag;

    }
    protected void counter()
    {
        if (lblzerocounter.Text != "5")
        {

            string stripa = "select * from  IpChangeAttemptLog where Successful='0' and UserId='" + ViewState["uid"].ToString() + "' order by Id desc  ";
            SqlCommand cmdipa = new SqlCommand(stripa, conn);
            SqlDataAdapter adpipa = new SqlDataAdapter(cmdipa);
            DataTable dsipa = new DataTable();
            adpipa.Fill(dsipa);


            if (dsipa.Rows.Count > 0)
            {
                int count = Convert.ToInt32(lblzerocounter.Text);

                count = count + 1;

                lblzerocounter.Text = count.ToString();



            }
            else
            {
                lblzerocounter.Text = "1";

            }
        }


    }
}
