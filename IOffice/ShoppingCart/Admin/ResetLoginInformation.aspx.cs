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
    string pwd1;
    protected void Page_Load(object sender, EventArgs e)
    {
        //Session["pnl1"] = "8";
        PageConn pgcon = new PageConn();
        connection = pgcon.dynconn;
        if (!IsPostBack)
        {
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
        if (txtuserid.Text != "" || txtuname.Text != "")
        {
            if (txtuserid.Text != "")
            {
                str = "select User_master.EmailID,User_master.UserID,User_master.Username,Party_master.Whid from User_master inner join Party_master on User_master.PartyID=Party_master.PartyID where Username='" + txtuserid.Text + "'";
            }
            else if (txtuname.Text != "")
            {
                str = "select User_master.EmailID,User_master.UserID,User_master.Username,Party_master.Whid from User_master inner join Party_master on User_master.PartyID=Party_master.PartyID where EmailID='" + txtuname.Text + "'";

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
                img1.Visible = true;
                Panel1.Visible = true;
                fillquestion();
                lblmsg.Text = "";
            }
            else
            {
                ImageButton6.Visible = false;
                img1.Visible = false;
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

        //string str = "SELECT EmailId,Username from User_master where EmailId='" + txtuname.Text + "'";
        //SqlCommand cmd = new SqlCommand(str, connection);
        //SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //DataTable dt = new DataTable();
        //adp.Fill(dt);
        //if (dt.Rows.Count == 0)
        //{

        //    lblmsg.Text = "User name possibly incorrect";
        //    txtuname.Text = "";

        //}
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

            if (txtans1.Visible == true)
            {
                if (ans1 == txtans1.Text)
                {
                    Panel1.Visible = true;
                    img1.Visible = true;
                    
                    string maxid = "select MAX(ID) as ID from PasswordResetRequestTbl where UserId='" + ViewState["Userid"] + "'";
                    SqlCommand cmd = new SqlCommand(maxid, connection);
                    SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adpt.Fill(ds);
                    ViewState["maxid"] = ds.Tables[0].Rows[0]["ID"].ToString();
                    sendmail(email);
                }
                else
                {
                    lblmsg.Text = "Your answer is incorrect kindly try to answer again";
                }
            }
            if (txtans2.Visible == true)
            {
                if (ans2 == txtans2.Text)
                {
                    Panel1.Visible = true;
                    img1.Visible = true;
                    
                    string maxid = "select MAX(ID) as ID from PasswordResetRequestTbl where UserId='" + ViewState["Userid"] + "'";
                    SqlCommand cmd = new SqlCommand(maxid, connection);
                    SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adpt.Fill(ds);
                    ViewState["maxid"] = ds.Tables[0].Rows[0]["ID"].ToString();
                    sendmail(email);
                }
                else
                {
                    lblmsg.Text = "Your answer is incorrect kindly try to answer again";
                }
            }
            if (txtans3.Visible == true)
            {
                if (ans3 == txtans3.Text)
                {
                    Panel1.Visible = true;
                    img1.Visible = true;
                    
                    string maxid = "select MAX(ID) as ID from PasswordResetRequestTbl where UserId='" + ViewState["Userid"] + "'";
                    SqlCommand cmd = new SqlCommand(maxid, connection);
                    SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adpt.Fill(ds);
                    ViewState["maxid"] = ds.Tables[0].Rows[0]["ID"].ToString();
                    sendmail(email);
                }
                else
                {
                    lblmsg.Text = "Your answer is incorrect kindly try to answer again";
                }
            }
            else
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
            }
        }
        else
        {
            Label2.Text = "";
            string maxid = "select MAX(ID) as ID from PasswordResetRequestTbl where UserId='" + ViewState["Userid"] + "'";
            SqlCommand cmd = new SqlCommand(maxid, connection);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            ViewState["maxid"] = ds.Tables[0].Rows[0]["ID"].ToString();
            sendmail(email);
        }
        // connection1.Open();
        // string passcommandstring;
        // passcommandstring = "SELECT  dbo.Login_master.username, dbo.Login_master.password, dbo.User_master.EmailID FROM   dbo.Login_master INNER JOIN    dbo.User_master ON dbo.Login_master.username = dbo.User_master.Username WHERE     (dbo.Login_master.username = '" + txtuname.Text + "')";


        // System.Data.SqlClient.SqlCommand command1 = new System.Data.SqlClient.SqlCommand(passcommandstring1);
        // command1.Connection = connection1;
        // command1.CommandType = new System.Data.CommandType();
        // command1.CommandText = passcommandstring;

        // SqlDataReader passrd;
        // passrd = command1.ExecuteReader();
        // passrd.Read();

        // //---------------------sending mail--------------//

        // string mailbody = "<table border=1 width=70% bordercolor=#eeeeee bordercolordark=#ffffff ><tr><td><b>User Name :</b></td><td>" + passrd[0].ToString() + "</td></tr><tr><td><b>Password:</b></td><td>" + passrd[1].ToString() + "</td></tr></table>";
        // mailbody = mailbody + "<table width=70% ><tr><td>HerryKem Cybernet LTD.</td></tr><tr><td>1/Giribuag Banglow,Behind Center Point,Alkapuri</td><tr><td>Vadodara</td><tr><td></td><tr><td></td></table>";

        // MailAddress fromAddress = new MailAddress("mp@onlinemanagers.com");


        // MailMessage message = new MailMessage();
        // SmtpClient client = new SmtpClient();

        //// smtpClient.Host = "127.0.0.1";


        // //smtpClient.Port = 25;
        // client.Credentials = new NetworkCredential("mp@onlinemanagers.com", "Mehul++");
        // client.Host = "mail.onlinemanagers.com";


        // message.From = fromAddress;

        // // To address collection of MailAddress
        // message.To.Add(passrd[2].ToString());
        // message.Subject = "Forgot Password";

        // message.IsBodyHtml = true;

        // // Message body content
        // message.Body = mailbody;

        // // Send SMTP mail
        // client.Send(message);

        // passrd.Close();
        // connection1.Close();


        //connection.Close();
    }
    //public string getWelcometext()
    //{


    //    string str = "SELECT EmailContentMaster.EmailContent, EmailContentMaster.EntryDate, CompanyWebsitMaster.SiteUrl, EmailTypeMaster.EmailTypeId " +
    //                " FROM CompanyWebsitMaster INNER JOIN " +
    //                  " EmailContentMaster ON CompanyWebsitMaster.CompanyWebsiteMasterId = EmailContentMaster.CompanyWebsiteMasterId INNER JOIN " +
    //                  " EmailTypeMaster ON EmailContentMaster.EmailTypeId = EmailTypeMaster.EmailTypeId " +
    //                " WHERE     (EmailTypeMaster.EmailTypeId = 1)  and (EmailTypeMaster.Compid='" + Session["Comid"].ToString() + "')" +
    //                " ORDER BY EmailContentMaster.EntryDate DESC ";
    //    SqlCommand cmd = new SqlCommand(str, connection);

    //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
    //    DataTable ds = new DataTable();
    //    adp.Fill(ds);
    //    string welcometext = "";
    //    if (ds.Rows.Count > 0)
    //    {
    //        welcometext = ds.Rows[0]["EmailContent"].ToString();

    //    } return welcometext;

    //}
    public void sendmail(string To)
    {

        string str = " Select  Distinct CompanyMaster.Compid,CompanyWebsitMaster.MasterEmailId,CompanyWebsitMaster.SiteUrl,CompanyWebsitMaster.Sitename, CompanyWebsitMaster.OutGoingMailServer,CompanyWebsitMaster.WebMasterEmail,CompanyWebsitMaster.EmailMasterLoginPassword, User_master.EmailID,User_master.UserID,CompanyMaster.CompanyLogo,CompanyMaster.CompanyName,CompanyAddressMaster.Email,CompanyAddressMaster.Fax,CompanyAddressMaster.Phone from User_master inner join Party_master on  Party_master.PartyID=User_master.PartyID inner join WareHouseMaster on WareHouseMaster.WareHouseId=Party_master.Whid inner join CompanyMaster on CompanyMaster.Compid=WareHouseMaster.comid inner join CompanyAddressMaster on CompanyAddressMaster.CompanyMasterId=CompanyMaster.CompanyId inner join CompanyWebsitMaster on CompanyWebsitMaster.WHId=WareHouseMaster.WareHouseId where User_master.EmailID='" + To + "'";
        SqlCommand cmd = new SqlCommand(str, connection);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        if (dt.Rows.Count > 0)
        {

            StringBuilder strhead = new StringBuilder();
            strhead.Append("<table style=\"font-size:14px; font-family:Calibri\" width=\"100%\"> ");
            strhead.Append("<tr><td align=\"left\"> <img src=\"ShoppingCart/images/" + Convert.ToString(dt.Rows[0]["CompanyLogo"]) + "\" \"border=\"0\" Width=\"176px\" Height=\"106px\" / > </td><td align=\"right\"><b><span style=\"color: #996600\">" + Convert.ToString(dt.Rows[0]["CompanyName"]) + "</span></b><Br><b>" + dt.Rows[0]["Sitename"].ToString() + "</b><BR><b>Phone Number:</b>" + Convert.ToString(dt.Rows[0]["Phone"]) + "<Br><b>Fax Number:</b>" + Convert.ToString(dt.Rows[0]["Fax"]) + "<Br><b>Email:</b>" + Convert.ToString(dt.Rows[0]["Email"]) + "<Br><b>Website:</b>" + dt.Rows[0]["SiteUrl"].ToString() + " </td></tr>  ");
            strhead.Append("<br><br></table> ");
            // StringBuilder strAddress = new StringBuilder();

            //string body = " <br><br><left>Did you Request a password reset for your account ("+To+")?</left><br><br> ";
            //body =body + "if you requested this password reset, go here:<br>";
            //body = body + "<a href=http://" + Request.Url.Host.ToString() + "/Shoppingcart/admin/Resetpassword.aspx?to="+To+">http://" +Request.Url.Host.ToString()+"/Shoppingcart/admin/Resetpassword.aspx?to="+To+"</a><br>";

            SqlDataAdapter daas = new SqlDataAdapter("select employeename from employeemaster inner join User_master on User_master.PartyID=EmployeeMaster.PartyID where username='" + txtuserid.Text + "'", connection);
            DataTable dtss = new DataTable();
            daas.Fill(dtss);

            string employeenameee = "";
            if (dtss.Rows.Count > 0)
            {

                employeenameee = Convert.ToString(dtss.Rows[0]["employeename"]);
            }
            else
            {
                employeenameee = "";
            }

            string sel = "select max(PartyID) as PartyID from Party_master where Id='" + Convert.ToInt32(Session["Comid"]) + "'";
            SqlCommand cmd5 = new SqlCommand(sel, connection);
            SqlDataAdapter da5 = new SqlDataAdapter(cmd5);
            DataSet ds5 = new DataSet();
            da5.Fill(ds5);

            SqlDataAdapter daas3 = new SqlDataAdapter("select EmployeeNo from EmployeePayrollMaster inner join employeemaster on  employeemaster.EmployeeMasterID=EmployeePayrollMaster.EmpId inner join User_master  on User_master.PartyID=EmployeeMaster.PartyID where username='" + txtuserid.Text + "'", connection);
            DataTable dtss3 = new DataTable();
            daas3.Fill(dtss3);

            string employeenum = "";
            if (dtss3.Rows.Count > 0)
            {

                employeenum = Convert.ToString(dtss3.Rows[0]["EmployeeNo"]);
            }
            else
            {
                employeenum = "";
            }

            SqlDataAdapter daas4 = new SqlDataAdapter("select Employeecode from EmployeeBarcodeMaster inner join employeemaster on employeemaster.EmployeeMasterID=EmployeeBarcodeMaster.Employee_Id inner join User_master on User_master.PartyID=EmployeeMaster.PartyID where username='" + txtuserid.Text + "'", connection);
            DataTable dtss4 = new DataTable();
            daas4.Fill(dtss4);

            string employeecodee = "";
            if (dtss4.Rows.Count > 0)
            {

                employeecodee = Convert.ToString(dtss4.Rows[0]["Employeecode"]);
            }
            else
            {
                employeecodee = "";
            }

            ViewState["partyidforemail"] = Convert.ToInt32(ds5.Tables[0].Rows[0]["PartyId"].ToString());

            string accountdetailofparty = "select Party_master.*,CountryMaster.CountryName,StateMasterTbl.StateName,CityMasterTbl.CityName from Party_master  left outer join CountryMaster on CountryMaster.CountryId=Party_master.Country left outer join StateMasterTbl on StateMasterTbl.StateId=Party_master.State left outer join CityMasterTbl on CityMasterTbl.CityId=Party_master.City inner join User_master on User_master.PartyID=Party_master.PartyID where Party_master.id='" + Convert.ToInt32(Session["Comid"]) + "' and Party_master.PartyID='" + ViewState["partyidforemail"] + "' ";
            SqlCommand cmdpartydetail = new SqlCommand(accountdetailofparty, connection);
            SqlDataAdapter adppartydetail = new SqlDataAdapter(cmdpartydetail);
            DataTable dspartydetail = new DataTable();
            string Accountdetail12 = "";
            adppartydetail.Fill(dspartydetail);

            string country = Convert.ToString(dspartydetail.Rows[0]["CountryName"]);
            string state = Convert.ToString(dspartydetail.Rows[0]["StateName"]);
            string city = Convert.ToString(dspartydetail.Rows[0]["CityName"]);
            string zipcode = Convert.ToString(dspartydetail.Rows[0]["Zipcode"]);
            string address = Convert.ToString(dspartydetail.Rows[0]["Address"]);
            string phonnum = Convert.ToString(dspartydetail.Rows[0]["Phoneno"]);
            string emaail = Convert.ToString(dspartydetail.Rows[0]["Email"]);


            //if (dspartydetail.Rows.Count > 0)
            //{
            Accountdetail12 = "<br><br>Below are your contact details. If you find any information that is incorrect, please change them from My Personal Profile page.<br> <br>Employee Name: " + employeenameee + "<br>Employee Number: " + employeenum + "<br>Employee Code: " + employeecodee + " <br>Address: " + address + " <br>City, State/Province, Country : " + country + "," + state + "," + city + "<br>ZIP/Postal Code: " + zipcode + "<br>Phone Number: " + phonnum + "<br>Email: " + emaail + " ";
            //<strong><span style=\"color: #996600\"></span></strong>
            //}

            string loginurl = Request.Url.Host.ToString() + "/Shoppingcart/Admin/ResetPasswordUser.aspx";

            SqlDataAdapter daas1 = new SqlDataAdapter("select username,password from Login_master where username='" + txtuserid.Text + "'", connection);
            DataTable dtss1 = new DataTable();
            daas1.Fill(dtss1);

            

            if (dtss1.Rows.Count > 0)
            {
                string pwd = dtss1.Rows[0]["password"].ToString();
                pwd1 = ClsEncDesc.Decrypted(pwd);
            }

            string body = " <br><br><span style=\"font-size:14px; font-family:Calibri; text-align:left\">Dear " + employeenameee + ", </span><br><br> ";
            body = body + "<br><br><span style=\"font-size:14px; font-family:Calibri; text-align:left\">You have requested to change your login information. Please click <a href=http://itimekeeper.com/shoppingcart/admin/resetpassworduser.aspx target=_blank > here</a> to change your information now, or click on the link below.</span><br> ";
            body = body + "<br><br><span style=\"font-size:14px; font-family:Calibri; text-align:left\">http://" + loginurl + " </span><br><br> ";
            body = body + "<table style=\"font-size:14px; font-family:Calibri\"><tr><td><left>Company Name : " + Convert.ToString(dt.Rows[0]["CompanyName"]) + "</left><br></td></tr>";
            //body = body + "<tr><td><left>Login URL: </left></td><td><left>: " + loginurl + "</left><br></td></tr>";
            body = body + "<tr><td><left>Company ID : " + Session["Comid"] + "</left><br></td></tr>";
            body = body + "<tr><td><left>Temporary User ID : " + dtss1.Rows[0]["username"].ToString() + "</left><br></td></tr>";
            body = body + "<tr><td><left>Temporary Password : " + pwd1 + "</left><br><br></td></tr>";
            body = body + "<tr><td><left>Please ensure that you change your user ID and password as soon as possible, for your own account security.</left><br><br></td></tr>";
            body = body + "<tr><td><left>" + Accountdetail12 + "</left></td></tr>";            
            //body = body + "<span style=\"font-size:14px; font-family:Calibri; text-align:left\">To change the password, please click on the link below</span><br>";
            //body = body + "<a href=http://" + Request.Url.Host.ToString() + "/Shoppingcart/admin/Resetpassword.aspx?to=" + ClsEncDesc.Encrypted(ViewState["maxid"].ToString()) + ">http://" + Request.Url.Host.ToString() + "/Shoppingcart/admin/Resetpassword.aspx?to=" + ClsEncDesc.Encrypted(ViewState["maxid"].ToString()) + "</a><br><br>";
            //body = body + "<span style=\"font-size:14px; font-family:Calibri; text-align:left\">If you have not sent any such password change request,just ignore this email.</span><br><br>";
            //body = body + "<span style=\"font-size:14px; font-family:Calibri; text-align:left\">If you have any other technical question, Please do not hesitate to contact by email our technical</span><br>";
            //body = body + "<span style=\"font-size:14px; font-family:Calibri; text-align:left\">Support team at techsupport@busiwiz.com</span><br><br>";



            string bodytext = "<br><span style=\"font-size:14px; font-family:Calibri; text-align:left\">Thank you and have a great day.<br><br>Sincerely,<br><br>The Admin Team at " + Convert.ToString(dt.Rows[0]["CompanyName"]) + "</span>";
            // " + dt.Rows[0]["Sitename"].ToString() + "
            StringBuilder support = new StringBuilder();
            support.Append("<table style=\"font-size:14px; font-family:Calibri; text-align:left\" width=\"100%\"> ");
            support.Append("<tr><td align=\"left\"><b><span style=\"color: #996600\">" + Session["productname"] + "</span></b>");
            support.Append("<br></table> ");
            string bodyformate = "" + strhead + "<br>" + body + "<br>" + bodytext + "<br>" + support + "";
            try
            {
                string strdy = " SELECT ClientMaster.OutgoingServerUserID,ClientMaster.OurgoingServerSMTP,ClientMaster.OutgoingServerPassword from CompanyMaster inner join ProductMaster on ProductMaster.ProductId=CompanyMaster.ProductId inner join ClientMaster on ClientMaster.ClientMasterId=ProductMaster.ClientMasterId where CompanyMaster.CompanyLoginId='" + Convert.ToString(dt.Rows[0]["Compid"]) + "'";
                SqlCommand cmddy = new SqlCommand(strdy, PageConn.licenseconn());
                SqlDataAdapter adpdy = new SqlDataAdapter(cmddy);
                DataTable dtdy = new DataTable();
                adpdy.Fill(dtdy);
                if (dtdy.Rows.Count > 0)
                {
                    MailAddress to = new MailAddress(To);
                    MailAddress from = new MailAddress(dtdy.Rows[0]["OutgoingServerUserID"].ToString());
                    MailMessage objEmail = new MailMessage(from, to);
                    objEmail.Subject = "" + Session["productname"] + " - " + Convert.ToString(dt.Rows[0]["CompanyName"]) + " - Request for Password Reset";

                    objEmail.Body = bodyformate.ToString();
                    objEmail.IsBodyHtml = true;
                    objEmail.Priority = MailPriority.High;

                    SmtpClient client = new SmtpClient();
                    client.Credentials = new NetworkCredential(dtdy.Rows[0]["OutgoingServerUserID"].ToString(), dtdy.Rows[0]["OutgoingServerPassword"].ToString());
                    client.Host = dtdy.Rows[0]["OurgoingServerSMTP"].ToString();
                    client.Send(objEmail);
                    lblmsg.Visible = true;
                    lblmsg.Text = "You will get your password by Email";
                    txtuname.Text = "";
                    Panel2.Visible = false;
                    ImageButton6.Visible = false;
                }

            }
            catch (Exception e)
            {
                lblmsg.Visible = true;
                //lblmsg.Text = e.ToString();
                lblmsg.Text = "Email Id or User Name is invalid for this Company";
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
