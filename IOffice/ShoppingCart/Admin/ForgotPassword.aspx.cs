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
using System.Net;
using System.Net.Mail;
using System.Text;

public partial class ShoppingCart_Admin_ForgotPassword : System.Web.UI.Page
{
    SqlConnection connection;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Session["pnl1"] = "8";
        PageConn pgcon = new PageConn();
        connection = pgcon.dynconn;
        if (!IsPostBack)
        {
            Button1.Enabled = true;
            txtuname.Enabled = true;
            txtuserid.Enabled = true;

            Session["pagename"] = "ForgotPassword.aspx";

            string str = "SELECT CompanyLoginId,CompanyName from CompanyMaster where Websiteurl='" + Request.Url.Host.ToString() + "' and active='1'";

            SqlCommand cmd = new SqlCommand(str, PageConn.licenseconn());
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Session["Comid"] = Convert.ToString(dt.Rows[0]["CompanyLoginId"]);
                ViewState["companyname"] = dt.Rows[0]["CompanyName"].ToString();
            }
            string str1 = "Select ProductMaster.ProductName from ProductMaster inner join CompanyMaster on CompanyMaster.ProductId = ProductMaster.ProductId where CompanyMaster.CompanyLoginId='" + Session["Comid"] + "'";
            SqlCommand cmd1 = new SqlCommand(str1, PageConn.licenseconn());
            SqlDataAdapter adpt = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            adpt.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                Session["productname"] = dt1.Rows[0]["ProductName"].ToString();
            }

        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string str = "";
        ViewState["Confirmationby"] = "";
        if (txtuserid.Text != "" || txtuname.Text != "")
        {
            if (txtuserid.Text != "")
            {
                str = "select User_master.EmailID,User_master.UserID,User_master.Username,Party_master.Whid from User_master inner join Party_master on User_master.PartyID=Party_master.PartyID where Username='" + txtuserid.Text + "'";

                ViewState["Confirmationby"] = "User ID";
            }
            else if (txtuname.Text != "")
            {
                str = "select User_master.EmailID,User_master.UserID,User_master.Username,Party_master.Whid from User_master inner join Party_master on User_master.PartyID=Party_master.PartyID where EmailID='" + txtuname.Text + "'";

                ViewState["Confirmationby"] = "Email ID";
            }
            SqlCommand cmd = new SqlCommand(str, connection);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                ViewState["Userid"] = dt.Rows[0]["UserID"].ToString();
                ViewState["username"] = dt.Rows[0]["Username"].ToString();
                ViewState["Whid"] = dt.Rows[0]["Whid"].ToString();
                ViewState["emailid"] = dt.Rows[0]["EmailID"].ToString();
                if (txtuserid.Text != "")
                {
                    img1.Visible = true;
                    img2.Visible = false;
                }
                if (txtuname.Text != "")
                {
                    img1.Visible = false;
                    img2.Visible = true;
                }
                Panel1.Visible = true;
                fillquestion();
                lblmsg.Text = "";

                Button1.Enabled = false;
                txtuname.Enabled = false;
                txtuserid.Enabled = false;

            }
            else
            {

                Button1.Enabled = true;
                txtuname.Enabled = true;
                txtuserid.Enabled = true;

                // ImageButton6.Visible = false;
                img1.Visible = false;
                img2.Visible = false;
                lblmsg.Text = "User name is possibly incorrect";
                txtuname.Text = "";
                Panel1.Visible = false;
            }
        }
        else
        {
            lblmsg.Text = "Please enter User Id or Email";
        }
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {

        string strinsert = "insert into PasswordResetRequestTbl(Whid,UserId,RequestTimeanddate,Successful,EmailGenerated,passwordchanged)" +
                                      " values('" + ViewState["Whid"] + "','" + ViewState["Userid"] + "','" + System.DateTime.Now.ToString() + "','1','1','0')";
        SqlCommand cmdinsert = new SqlCommand(strinsert, connection);
        if (connection.State.ToString() != "Open")
        {
            connection.Open();
        }
        cmdinsert.ExecuteNonQuery();
        connection.Close();

        string email = ViewState["emailid"].ToString();

        string strquestion = "Select SecurityQuestionMaster.*,SecurityQuestion.UserId,SecurityQuestion.Answer from SecurityQuestionMaster inner join SecurityQuestion on SecurityQuestionMaster.id = SecurityQuestion.SequrityQueId where SecurityQuestion.UserId = '" + ViewState["Userid"] + "'";
        SqlCommand cmdquestion = new SqlCommand(strquestion, connection);
        SqlDataAdapter adptquestion = new SqlDataAdapter(cmdquestion);
        DataSet dsquestion = new DataSet();
        adptquestion.Fill(dsquestion);

        if (dsquestion.Tables[0].Rows.Count > 0)
        {
            string ans1 = dsquestion.Tables[0].Rows[0]["Answer"].ToString();
            string ans2 = dsquestion.Tables[0].Rows[1]["Answer"].ToString();
            string ans3 = dsquestion.Tables[0].Rows[2]["Answer"].ToString();

            //if (txtans1.Visible == true)
            //{
            //    if (ans1 == txtans1.Text)
            //    {
            //        Panel1.Visible = true;
            //        img1.Visible = true;

            //        string maxid = "select MAX(ID) as ID from PasswordResetRequestTbl where UserId='" + ViewState["Userid"] + "'";
            //        SqlCommand cmd = new SqlCommand(maxid, connection);
            //        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            //        DataSet ds = new DataSet();
            //        adpt.Fill(ds);
            //        ViewState["maxid"] = ds.Tables[0].Rows[0]["ID"].ToString();
            //        sendmail(email);
            //    }
            //    else
            //    {
            //        lblmsg.Text = "Your answer is incorrect kindly try to answer again";
            //    }
            //}
            //if (txtans2.Visible == true)
            //{
            //    if (ans2 == txtans2.Text)
            //    {
            //        Panel1.Visible = true;
            //        img1.Visible = true;

            //        string maxid = "select MAX(ID) as ID from PasswordResetRequestTbl where UserId='" + ViewState["Userid"] + "'";
            //        SqlCommand cmd = new SqlCommand(maxid, connection);
            //        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            //        DataSet ds = new DataSet();
            //        adpt.Fill(ds);
            //        ViewState["maxid"] = ds.Tables[0].Rows[0]["ID"].ToString();
            //        sendmail(email);
            //    }
            //    else
            //    {
            //        lblmsg.Text = "Your answer is incorrect kindly try to answer again";
            //    }
            //}
            //if (txtans3.Visible == true)
            //{
            //    if (ans3 == txtans3.Text)
            //    {
            //        Panel1.Visible = true;
            //        img1.Visible = true;

            //        string maxid = "select MAX(ID) as ID from PasswordResetRequestTbl where UserId='" + ViewState["Userid"] + "'";
            //        SqlCommand cmd = new SqlCommand(maxid, connection);
            //        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            //        DataSet ds = new DataSet();
            //        adpt.Fill(ds);
            //        ViewState["maxid"] = ds.Tables[0].Rows[0]["ID"].ToString();
            //        sendmail(email);
            //    }
            //    else
            //    {
            //        lblmsg.Text = "Your answer is incorrect kindly try to answer again";
            //    }
            //}

            if (ans1 == txtans1.Text && ans2 == txtans2.Text && ans3 == txtans3.Text)
            {
                DateTime datet = DateTime.Now.AddMinutes(-10);
                string chekeit = "Select * from PasswordResetRequestTbl where UserId='" + ViewState["Userid"] + "' and   RequestTimeanddate Between   CAST('" + datet + "'as Datetime) and  CAST('" + DateTime.Now.ToString() + "' as Datetime)";
                SqlCommand cmdchek = new SqlCommand(chekeit, connection);
                SqlDataAdapter adptchek = new SqlDataAdapter(cmdchek);
                DataTable dtchek = new DataTable();
                adptchek.Fill(dtchek);
                if (dtchek.Rows.Count > 5)
                {
                    lblmsg.Text = "You have attempted 5 times in last 10 minutes so this facility is blocked for 15 minutes,you can try after sometime";
                }
                else
                {

                    string maxid = "select MAX(ID) as ID from PasswordResetRequestTbl where UserId='" + ViewState["Userid"] + "'";
                    SqlCommand cmd = new SqlCommand(maxid, connection);
                    SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adpt.Fill(ds);

                    ViewState["maxid"] = ds.Tables[0].Rows[0]["ID"].ToString();
                    sendmail(email);

                    Panel2.Visible = false;
                    Panel1.Visible = false;
                    ImageButton6.Visible = false;

                }

            }



        }
        else
        {

            string maxid = "select MAX(ID) as ID from PasswordResetRequestTbl where UserId='" + ViewState["Userid"] + "'";
            SqlCommand cmd = new SqlCommand(maxid, connection);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            ViewState["maxid"] = ds.Tables[0].Rows[0]["ID"].ToString();
            sendmail(email);

            Panel2.Visible = false;
            Panel1.Visible = false;
            ImageButton6.Visible = false;

        }

    }

    public void sendmail(string To)
    {

        string str = " Select  Distinct CompanyMaster.Compid,CompanyWebsitMaster.logourl,CompanyAddressMaster.Website,WareHouseMaster.Name as Wname,CompanyWebsitMaster.MasterEmailId, CompanyWebsitMaster.OutGoingMailServer,CompanyWebsitMaster.WebMasterEmail,CompanyWebsitMaster.EmailMasterLoginPassword, User_master.EmailID,User_master.UserID,CompanyMaster.CompanyLogo,CompanyMaster.CompanyName,CompanyAddressMaster.Address1,CompanyAddressMaster.Address2,CompanyAddressMaster.Email,CompanyAddressMaster.Fax,CompanyAddressMaster.Phone from User_master inner join Party_master on  Party_master.PartyID=User_master.PartyID inner join WareHouseMaster on WareHouseMaster.WareHouseId=Party_master.Whid inner join CompanyMaster on CompanyMaster.Compid=WareHouseMaster.comid inner join CompanyAddressMaster on CompanyAddressMaster.CompanyMasterId=CompanyMaster.CompanyId inner join CompanyWebsitMaster on CompanyWebsitMaster.WHId=WareHouseMaster.WareHouseId where User_master.UserID='" + ViewState["Userid"].ToString() + "'";
        SqlCommand cmd = new SqlCommand(str, connection);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            string companyid = dt.Rows[0]["Compid"].ToString();

            StringBuilder strhead = new StringBuilder();
            strhead.Append("<table style=\"font-size:14px; font-family:Calibri\" width=\"100%\"> ");
            strhead.Append("<tr><td align=\"left\"> <img src=\"~/ShoppingCart/images/" + Convert.ToString(dt.Rows[0]["logourl"]) + "\" \"border=\"0\" Width=\"176px\" Height=\"106px\" / > </td><td align=\"right\"><b><span style=\"color: #996600\">" + Convert.ToString(dt.Rows[0]["CompanyName"]) + "</span></b><BR>" + Convert.ToString(dt.Rows[0]["Address1"]) + "<BR>" + Convert.ToString(dt.Rows[0]["Address2"]) + "<BR><b>Phone:</b>" + Convert.ToString(dt.Rows[0]["Phone"]) + "<Br><b>Fax:</b>" + Convert.ToString(dt.Rows[0]["Fax"]) + "<Br><b>Email:</b>" + Convert.ToString(dt.Rows[0]["Email"]) + "<Br><b>Website:</b>" + Convert.ToString(dt.Rows[0]["Website"]) + "</td></tr>  ");
            strhead.Append("<br><br></table> ");


            SqlDataAdapter daas = new SqlDataAdapter("select * from Party_master inner join User_master on User_master.PartyID=Party_master.PartyID where User_master.UserID='" + ViewState["Userid"].ToString() + "'", connection);
            DataTable dtss = new DataTable();
            daas.Fill(dtss);
            string employeenamee = "";
            if (dtss.Rows.Count > 0)
            {
                employeenamee = Convert.ToString(dtss.Rows[0]["Compname"]);
            }

            string body = " <br><br><span style=\"font-size:14px; font-family:Calibri; text-align:left\">Dear " + dt.Rows[0]["Wname"].ToString() + ", </span><br><br> ";
            body = body + "<br><span style=\"font-size:14px; font-family:Calibri; text-align:left\">You have requested to change your password on the following account:</span><br> ";
            body = body + "<table style=\"font-size:14px; font-family:Calibri\"><tr><td><left>Company ID </left></td><td><left>: " + companyid + "</left><br></td></tr>";
            body = body + "<tr><td><left>Username </left></td><td><left>: " + ViewState["username"] + "</left><br></td></tr>";
            body = body + "<tr><td><left>Email ID </left></td><td><left>: " + To + "</left><br></td></tr></table><br>";
            body = body + "<span style=\"font-size:14px; font-family:Calibri; text-align:left\">To change your password please click <a href=http://" + Request.Url.Host.ToString() + "/Shoppingcart/admin/Resetpassword.aspx?to=" + ClsEncDesc.Encrypted(ViewState["maxid"].ToString()) + " target=_blank> here</a>, or copy and paste the following link into your browser:</span><br><br>";
            body = body + "<a href=http://" + Request.Url.Host.ToString() + "/Shoppingcart/admin/Resetpassword.aspx?to=" + ClsEncDesc.Encrypted(ViewState["maxid"].ToString()) + ">http://" + Request.Url.Host.ToString() + "/Shoppingcart/admin/Resetpassword.aspx?to=" + ClsEncDesc.Encrypted(ViewState["maxid"].ToString()) + "</a><br><br>";
            body = body + "<span style=\"font-size:14px; font-family:Calibri; text-align:left\">If you have not requested a password change, please disregard this email.</span><br><br>";
            body = body + "<span style=\"font-size:14px; font-family:Calibri; text-align:left\">If you have any questions, please contact our technical support team at: techsupport@busiwiz.com</span><br>";


            string bodytext = "<br><span style=\"font-size:14px; font-family:Calibri; text-align:left\">Thank you</span><br>";

            StringBuilder support = new StringBuilder();
            support.Append("<table style=\"font-size:14px; font-family:Calibri; text-align:left\" width=\"100%\"> ");
            support.Append("<tr><td align=\"left\"><b><span style=\"color: #996600\">Technical Support Team</span></b><br>" + Convert.ToString(dt.Rows[0]["CompanyName"]) + "<br>" + Convert.ToString(dt.Rows[0]["Address1"]) + "<BR>" + Convert.ToString(dt.Rows[0]["Address2"]) + "<BR><b>Phone:</b>" + Convert.ToString(dt.Rows[0]["Phone"]) + "<Br><b>Fax:</b>" + Convert.ToString(dt.Rows[0]["Fax"]) + "<Br><b>Email:</b>" + Convert.ToString(dt.Rows[0]["Email"]) + "<Br><b>Website:</b>" + Convert.ToString(dt.Rows[0]["Website"]) + "");
            support.Append("<br></table> ");
            string bodyformate = "" + strhead + "<br>" + body + "<br>" + bodytext + "<br>" + support + "";
            try
            {
                string strdy = " SELECT ClientMaster.OutgoingServerUserID,ClientMaster.OurgoingServerSMTP,ClientMaster.OutgoingServerPassword,ProductMaster.ProductName from CompanyMaster inner join ProductMaster on ProductMaster.ProductId=CompanyMaster.ProductId inner join ClientMaster on ClientMaster.ClientMasterId=ProductMaster.ClientMasterId where CompanyMaster.CompanyLoginId='" + Convert.ToString(dt.Rows[0]["Compid"]) + "'";
                SqlCommand cmddy = new SqlCommand(strdy, PageConn.licenseconn());
                SqlDataAdapter adpdy = new SqlDataAdapter(cmddy);
                DataTable dtdy = new DataTable();
                adpdy.Fill(dtdy);

                if (dtdy.Rows.Count > 0)
                {
                    MailAddress to = new MailAddress(To);
                    MailAddress from = new MailAddress(dtdy.Rows[0]["OutgoingServerUserID"].ToString());
                    MailMessage objEmail = new MailMessage(from, to);
                    objEmail.Subject = "Request For Password Reset - " + dt.Rows[0]["Wname"].ToString();

                    objEmail.Body = bodyformate.ToString();
                    objEmail.IsBodyHtml = true;
                    objEmail.Priority = MailPriority.High;

                    SmtpClient client = new SmtpClient();
                    client.Credentials = new NetworkCredential(dtdy.Rows[0]["OutgoingServerUserID"].ToString(), dtdy.Rows[0]["OutgoingServerPassword"].ToString());
                    client.Host = dtdy.Rows[0]["OurgoingServerSMTP"].ToString();
                    client.Send(objEmail);
                    lblmsg.Visible = true;


                    lblmsg.Text = ViewState["Confirmationby"].ToString() + " accepted. A confirmation email has been sent to your email address.";


                    txtuname.Text = "";
                    ImageButton6.Visible = false;
                }

            }
            catch (Exception e)
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Error:" + e.ToString();

            }
        }

    }

    protected void fillquestion()
    {
        lblque1.Text = "No Question set yet";
        lblque2.Text = "No Question set yet";
        lblque3.Text = "No Question set yet";

        string strquestion = "Select SecurityQuestionMaster.*,SecurityQuestion.UserId,SecurityQuestion.Answer from SecurityQuestionMaster inner join SecurityQuestion on SecurityQuestionMaster.id = SecurityQuestion.SequrityQueId where SecurityQuestion.UserId = '" + ViewState["Userid"] + "'";
        SqlCommand cmdquestion = new SqlCommand(strquestion, connection);
        SqlDataAdapter adptquestion = new SqlDataAdapter(cmdquestion);
        DataSet dsquestion = new DataSet();
        adptquestion.Fill(dsquestion);
        if (dsquestion.Tables[0].Rows.Count > 0)
        {
            if (dsquestion.Tables[0].Rows[0]["QueName"].ToString() != null)
            {
                lblque1.Visible = true;
                txtans1.Visible = true;
                lblque1.Text = dsquestion.Tables[0].Rows[0]["QueName"].ToString();
                ImageButton6.Visible = true;
            }
            else
            {
                lblque1.Visible = false;
                txtans1.Visible = false;

                lblque1.Text = "No question set yet";
            }
            if (dsquestion.Tables[0].Rows[1]["QueName"].ToString() != null)
            {
                lblque2.Visible = true;
                txtans2.Visible = true;
                lblque2.Text = dsquestion.Tables[0].Rows[1]["QueName"].ToString();
                ImageButton6.Visible = true;
            }
            else
            {
                lblque2.Visible = false;
                txtans2.Visible = false;
                ImageButton6.Visible = true;
                lblque2.Text = "No question set yet";

            }
            if (dsquestion.Tables[0].Rows[2]["QueName"].ToString() != null)
            {
                lblque3.Visible = true;
                txtans3.Visible = true;
                lblque3.Text = dsquestion.Tables[0].Rows[2]["QueName"].ToString();
                ImageButton6.Visible = true;
            }
            else
            {
                lblque3.Visible = false;
                txtans3.Visible = false;
                ImageButton6.Visible = true;
                lblque3.Text = "No question set yet";
            }
        }
        else
        {
            lblque1.Visible = false;
            lblque2.Visible = false;
            lblque3.Visible = false;
            txtans1.Visible = false;
            txtans2.Visible = false;
            txtans3.Visible = false;
            Panel1.Visible = false;
            ImageButton6.Visible = true;
        }
    }
}
