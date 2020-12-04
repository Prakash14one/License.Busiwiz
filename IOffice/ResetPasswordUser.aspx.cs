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
    SqlConnection connection = new SqlConnection(@"Data Source =C3\C3SERVERMASTER,30000; Initial Catalog = jobcenter.OADB; User ID=sa; Password=06De1963++; Persist Security Info=true;");
    SqlConnection licenseconn = new SqlConnection(@"Data Source=tcp:192.168.1.219,2810;Initial Catalog=License.Busiwiz; User ID=BuzRead; Password=Busiwiz2013++; Persist Security Info=true; ");

    //SqlConnection connection = new SqlConnection(@"Data Source =TCP:72.38.84.230,30000; Initial Catalog = jobcenter.OADB; User ID=sa; Password=06De1963++; Persist Security Info=true;");
    //SqlConnection licenseconn = new SqlConnection(@"Data Source=192.168.40.120,30000;Initial Catalog=License.BusiwizDeveloper; User ID=TVMDeveloper; Password=Om2015++; Persist Security Info=true; ");
   


    protected void Page_Load(object sender, EventArgs e)
    {
        lblVersion.Text = "V5-01-Apr-2016-updated by nithya";
        //PageConn pgcon = new PageConn();
        PageConn.strEnc = "3d70f5cff23ed17ip8H9";
        //connection = pgcon.dynconn;
       
        //txtnewpassword.Attributes("value") = txtnewpassword.Text;
        if (!IsPostBack)
        {
            if (Request.QueryString["to"] != null)
            {
                string str = "SELECT CompanyLoginId from CompanyMaster where Websiteurl='" + Request.Url.Host.ToString() + "' and active='1'";

                SqlCommand cmd = new SqlCommand(str, licenseconn);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    Session["Comid"] = Convert.ToString(dt.Rows[0]["CompanyLoginId"]);
                }

                string strmaxid = "Select UserId from PasswordResetRequestTbl where ID='" + ClsEncDesc.Decrypted(Request.QueryString["to"]) + "'";
                SqlCommand cmdmax = new SqlCommand(strmaxid, connection);
                SqlDataAdapter adptmax = new SqlDataAdapter(cmdmax);
                DataSet dsmax = new DataSet();
                adptmax.Fill(dsmax);

                if (dsmax.Tables[0].Rows.Count > 0)
                {
                    ViewState["maxuserid"] = Convert.ToInt32(dsmax.Tables[0].Rows[0]["UserId"]);
                }
                string str1 = " Select  Distinct CompanyMaster.Compid,CompanyWebsitMaster.MasterEmailId, CompanyWebsitMaster.OutGoingMailServer,CompanyWebsitMaster.WebMasterEmail,CompanyWebsitMaster.EmailMasterLoginPassword, User_master.EmailID,User_master.UserID,CompanyMaster.CompanyLogo,CompanyMaster.CompanyName,CompanyAddressMaster.Email,CompanyAddressMaster.Fax,CompanyAddressMaster.Phone from User_master inner join Party_master on  Party_master.PartyID=User_master.PartyID inner join WareHouseMaster on WareHouseMaster.WareHouseId=Party_master.Whid inner join CompanyMaster on CompanyMaster.Compid=WareHouseMaster.comid inner join CompanyAddressMaster on CompanyAddressMaster.CompanyMasterId=CompanyMaster.CompanyId inner join CompanyWebsitMaster on CompanyWebsitMaster.WHId=WareHouseMaster.WareHouseId where User_master.UserID='" + ViewState["maxuserid"] + "'";
                SqlCommand cmd1 = new SqlCommand(str1, connection);
                SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                adp1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    ViewState["UserID"] = Convert.ToString(dt1.Rows[0]["UserID"]);
                    Session["Comid"] = Convert.ToString(dt1.Rows[0]["Compid"]);
                    if (Convert.ToString(dt1.Rows[0]["CompanyLogo"]) != "")
                    {
                        img1.ImageUrl = "ShoppingCart/images/" + Convert.ToString(dt1.Rows[0]["CompanyLogo"]);
                    }
                }

                pnlsecurityquestion.Visible = true;
                Panel2.Visible = false;

                fillquestion1();
                string strquestion = "Select SecurityQuestionMaster.*,SecurityQuestion.Id as ansid,SecurityQuestion.SequrityQueId,SecurityQuestion.UserId,SecurityQuestion.Answer from SecurityQuestionMaster inner join SecurityQuestion on SecurityQuestionMaster.id = SecurityQuestion.SequrityQueId where SecurityQuestion.UserId = '" + Session["userid"] + "' and SecurityQuestion.QuestionNo='1'";
                DataSet dsquestion = (DataSet)fillddl(strquestion);
                if (dsquestion.Tables[0].Rows.Count > 0)
                {
                    ddlquestion1.SelectedIndex = ddlquestion1.Items.IndexOf(ddlquestion1.Items.FindByValue(dsquestion.Tables[0].Rows[0]["SequrityQueId"].ToString()));

                }

                fillquestion2();

                string strquestion2 = "Select SecurityQuestionMaster.*,SecurityQuestion.Id as ansid,SecurityQuestion.SequrityQueId,SecurityQuestion.UserId,SecurityQuestion.Answer from SecurityQuestionMaster inner join SecurityQuestion on SecurityQuestionMaster.id = SecurityQuestion.SequrityQueId where SecurityQuestion.UserId = '" + Session["userid"] + "' and SecurityQuestion.QuestionNo='2'";
                DataSet dsquestion2 = (DataSet)fillddl(strquestion2);

                if (dsquestion2.Tables[0].Rows.Count > 0)
                {
                    ddlquestion2.SelectedIndex = ddlquestion2.Items.IndexOf(ddlquestion2.Items.FindByValue(dsquestion2.Tables[0].Rows[0]["SequrityQueId"].ToString()));


                }

                fillquestion3();
                string strquestion3 = "Select SecurityQuestionMaster.*,SecurityQuestion.Id as ansid,SecurityQuestion.SequrityQueId,SecurityQuestion.UserId,SecurityQuestion.Answer from SecurityQuestionMaster inner join SecurityQuestion on SecurityQuestionMaster.id = SecurityQuestion.SequrityQueId where SecurityQuestion.UserId = '" + Session["userid"] + "' and SecurityQuestion.QuestionNo='3'";
                DataSet dsquestion3 = (DataSet)fillddl(strquestion3);


                if (dsquestion3.Tables[0].Rows.Count > 0)
                {
                    ddlquestion3.SelectedIndex = ddlquestion3.Items.IndexOf(ddlquestion3.Items.FindByValue(dsquestion3.Tables[0].Rows[0]["SequrityQueId"].ToString()));


                }
            }
            else if (Request.QueryString["cid"] != null && Request.QueryString["uid"] != null && Request.QueryString["pwd"] != null)
            {
                string cid = Convert.ToString(Request.QueryString["cid"]);
                string uid = Convert.ToString(Request.QueryString["uid"]);
                string pwd = Convert.ToString(Request.QueryString["pwd"]);

                Session["Comid"] = cid;

                //PageConn pgcon1 = new PageConn();
                //connection1 = pgcon1.dynconn;                         

                pwd = ClsEncDesc.Decrypted(pwd);

                SqlCommand cmdlogin = new SqlCommand("SelectUserLogin", connection);
                cmdlogin.CommandType = CommandType.StoredProcedure;
                cmdlogin.Parameters.Add(new SqlParameter("@UID", SqlDbType.NVarChar));
                cmdlogin.Parameters["@UID"].Value = uid;
                cmdlogin.Parameters.Add(new SqlParameter("@CD", SqlDbType.NVarChar));
                cmdlogin.Parameters["@CD"].Value = cid;
                cmdlogin.Parameters.Add(new SqlParameter("@Pas", SqlDbType.NVarChar));
                cmdlogin.Parameters["@Pas"].Value = ClsEncDesc.Encrypted(pwd);

                SqlDataAdapter adplogin = new SqlDataAdapter(cmdlogin);
                DataTable dtloginuser = new DataTable();
                adplogin.Fill(dtloginuser);

                if (dtloginuser.Rows.Count > 0)
                {
                    panelforcandidate.Visible = false;
                    pnlsecurityquestion.Visible = true;
                    Panel2.Visible = false;
                    Session["userid"] = dtloginuser.Rows[0]["UserID"].ToString();
                    lblmsg.Text = "";

                    fillquestion111();

                    string strquestion = "Select SecurityQuestionMaster.*,SecurityQuestion.Id as ansid,SecurityQuestion.SequrityQueId,SecurityQuestion.UserId,SecurityQuestion.Answer from SecurityQuestionMaster inner join SecurityQuestion on SecurityQuestionMaster.id = SecurityQuestion.SequrityQueId where SecurityQuestion.UserId = '" + Session["userid"] + "' and SecurityQuestion.QuestionNo='1'";
                    DataSet dsquestion = (DataSet)fillddl123(strquestion);
                    if (dsquestion.Tables[0].Rows.Count > 0)
                    {
                        ddlquestion1.SelectedIndex = ddlquestion1.Items.IndexOf(ddlquestion1.Items.FindByValue(dsquestion.Tables[0].Rows[0]["SequrityQueId"].ToString()));
                    }

                    fillquestion222();

                    string strquestion2 = "Select SecurityQuestionMaster.*,SecurityQuestion.Id as ansid,SecurityQuestion.SequrityQueId,SecurityQuestion.UserId,SecurityQuestion.Answer from SecurityQuestionMaster inner join SecurityQuestion on SecurityQuestionMaster.id = SecurityQuestion.SequrityQueId where SecurityQuestion.UserId = '" + Session["userid"] + "' and SecurityQuestion.QuestionNo='2'";
                    DataSet dsquestion2 = (DataSet)fillddl123(strquestion2);

                    if (dsquestion2.Tables[0].Rows.Count > 0)
                    {
                        ddlquestion2.SelectedIndex = ddlquestion2.Items.IndexOf(ddlquestion2.Items.FindByValue(dsquestion2.Tables[0].Rows[0]["SequrityQueId"].ToString()));
                    }

                    fillquestion333();

                    string strquestion3 = "Select SecurityQuestionMaster.*,SecurityQuestion.Id as ansid,SecurityQuestion.SequrityQueId,SecurityQuestion.UserId,SecurityQuestion.Answer from SecurityQuestionMaster inner join SecurityQuestion on SecurityQuestionMaster.id = SecurityQuestion.SequrityQueId where SecurityQuestion.UserId = '" + Session["userid"] + "' and SecurityQuestion.QuestionNo='3'";
                    DataSet dsquestion3 = (DataSet)fillddl123(strquestion3);

                    if (dsquestion3.Tables[0].Rows.Count > 0)
                    {
                        ddlquestion3.SelectedIndex = ddlquestion3.Items.IndexOf(ddlquestion3.Items.FindByValue(dsquestion3.Tables[0].Rows[0]["SequrityQueId"].ToString()));
                    }
                }
                else
                {
                    pnlsecurityquestion.Visible = false;
                    lblmsg.Text = "Incorrect username or password. Please try again.";
                }
            }
            else
            {
                pnlsecurityquestion.Visible = false;
                Panel2.Visible = true;
            }
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlCommand cmdlogin = new SqlCommand("SelectUserLogin", connection);
        cmdlogin.CommandType = CommandType.StoredProcedure;
        cmdlogin.Parameters.Add(new SqlParameter("@UID", SqlDbType.NVarChar));
        cmdlogin.Parameters["@UID"].Value = txtuname.Text;
        cmdlogin.Parameters.Add(new SqlParameter("@CD", SqlDbType.NVarChar));
        cmdlogin.Parameters["@CD"].Value = txtcompanyid.Text;

        cmdlogin.Parameters.Add(new SqlParameter("@Pas", SqlDbType.NVarChar));
        cmdlogin.Parameters["@Pas"].Value = ClsEncDesc.Encrypted(txtpass.Text);

        SqlDataAdapter adplogin = new SqlDataAdapter(cmdlogin);
        DataTable dtloginuser = new DataTable();
        adplogin.Fill(dtloginuser);
        
        if (dtloginuser.Rows.Count > 0)
        {
            pnlsecurityquestion.Visible = true;
            Panel2.Visible = false;
            Session["userid"] = dtloginuser.Rows[0]["UserID"].ToString();
            lblmsg.Text = "";

            fillquestion1();
            string strquestion = "Select SecurityQuestionMaster.*,SecurityQuestion.Id as ansid,SecurityQuestion.SequrityQueId,SecurityQuestion.UserId,SecurityQuestion.Answer from SecurityQuestionMaster inner join SecurityQuestion on SecurityQuestionMaster.id = SecurityQuestion.SequrityQueId where SecurityQuestion.UserId = '" + Session["userid"] + "' and SecurityQuestion.QuestionNo='1'";
            DataSet dsquestion = (DataSet)fillddl(strquestion);
            if (dsquestion.Tables[0].Rows.Count > 0)
            {
                ddlquestion1.SelectedIndex = ddlquestion1.Items.IndexOf(ddlquestion1.Items.FindByValue(dsquestion.Tables[0].Rows[0]["SequrityQueId"].ToString()));

            }

            fillquestion2();

            string strquestion2 = "Select SecurityQuestionMaster.*,SecurityQuestion.Id as ansid,SecurityQuestion.SequrityQueId,SecurityQuestion.UserId,SecurityQuestion.Answer from SecurityQuestionMaster inner join SecurityQuestion on SecurityQuestionMaster.id = SecurityQuestion.SequrityQueId where SecurityQuestion.UserId = '" + Session["userid"] + "' and SecurityQuestion.QuestionNo='2'";
            DataSet dsquestion2 = (DataSet)fillddl(strquestion2);

            if (dsquestion2.Tables[0].Rows.Count > 0)
            {
                ddlquestion2.SelectedIndex = ddlquestion2.Items.IndexOf(ddlquestion2.Items.FindByValue(dsquestion2.Tables[0].Rows[0]["SequrityQueId"].ToString()));
            }

            fillquestion3();
            string strquestion3 = "Select SecurityQuestionMaster.*,SecurityQuestion.Id as ansid,SecurityQuestion.SequrityQueId,SecurityQuestion.UserId,SecurityQuestion.Answer from SecurityQuestionMaster inner join SecurityQuestion on SecurityQuestionMaster.id = SecurityQuestion.SequrityQueId where SecurityQuestion.UserId = '" + Session["userid"] + "' and SecurityQuestion.QuestionNo='3'";
            DataSet dsquestion3 = (DataSet)fillddl(strquestion3);


            if (dsquestion3.Tables[0].Rows.Count > 0)
            {
                ddlquestion3.SelectedIndex = ddlquestion3.Items.IndexOf(ddlquestion3.Items.FindByValue(dsquestion3.Tables[0].Rows[0]["SequrityQueId"].ToString()));
            }
        }
        else
        {
            pnlsecurityquestion.Visible = false;
            lblmsg.Text = "Incorrect username or password. Please try again.";
        }
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {

    }

    public DataSet fillddl(String qry)
    {
        SqlCommand cmd = new SqlCommand(qry, connection);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;
    }

    public DataSet fillddl123(String qry)
    {
        SqlCommand cmd = new SqlCommand(qry, connection);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;
    }

    public void fillddlOther(DropDownList ddl, String dtf, String dvf)
    {
        ddl.DataTextField = dtf;
        ddl.DataValueField = dvf;
        ddl.DataBind();

    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        updateinsertsecurityquestion();

        if (ViewState["Duplication"] == "0")
        {

            if (CheckBox1.Checked == true)
            {

                string updateusermaster = "Update User_master set Username='" + txtnewuserid.Text + "' where UserID='" + Session["userid"] + "'";
                SqlCommand cmdusermaster = new SqlCommand(updateusermaster, connection);
                if (connection.State.ToString() != "Open")
                {
                    connection.Open();
                }
                cmdusermaster.ExecuteNonQuery();
                connection.Close();

                string upd = "Update Login_master Set username='" + txtnewuserid.Text + "' where UserId='" + Session["userid"] + "'";
                SqlCommand cmdupd = new SqlCommand(upd, connection);
                if (connection.State.ToString() != "Open")
                {
                    connection.Open();
                }
                cmdupd.ExecuteNonQuery();
                connection.Close();
            }
            if (CheckBox2.Checked == true)
            {
                string upd = "Update Login_master Set password='" + ClsEncDesc.Encrypted(txtnewpassword.Text) + "' where UserId='" + Session["userid"] + "'";
                SqlCommand cmdupd = new SqlCommand(upd, connection);
                if (connection.State.ToString() != "Open")
                {
                    connection.Open();
                }
                cmdupd.ExecuteNonQuery();
                connection.Close();

            }

            if (CheckBox3.Checked == true)
            {

                string strempcodeid = "select EmployeeMaster.EmployeeMasterID from User_master inner join EmployeeMaster on EmployeeMaster.PartyID=User_master.PartyID  where User_master.UserID = '" + Session["userid"] + "'";
                DataSet dsempcode = (DataSet)fillddl(strempcodeid);

                if (dsempcode.Tables[0].Rows.Count > 0)
                {
                    ViewState["employeeid"] = dsempcode.Tables[0].Rows[0]["EmployeeMasterID"].ToString();

                    string updateempcode = "Update EmployeeBarcodeMaster set Employeecode='" + txtempcode.Text + "' where Employee_Id='" + ViewState["employeeid"] + "'";
                    SqlCommand cmduserempcode = new SqlCommand(updateempcode, connection);
                    if (connection.State.ToString() != "Open")
                    {
                        connection.Open();
                    }
                    cmduserempcode.ExecuteNonQuery();
                    connection.Close();

                }
            }
            sendmail();
            txtanswer1.Text = "";
            txtanswer2.Text = "";
            txtanswer3.Text = "";

            lblmsg.Visible = true;
            lblmsg.Text = " Information was successfully reset";
            pnlsecurityquestion.Visible = false;
            Panel2.Visible = true;

            string te = "http://members.ijobcenter.com/";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        }
        else
        {
            Label4.Visible = true;
            Label4.Text = "Duplicate question is selected.";
            pnlsecurityquestion.Visible = true;
            Panel2.Visible = false;
        }
    }
    public void sendmail()
    {

        string str = " Select  Distinct CompanyMaster.Compid,CompanyMaster.CompanyName,CompanyWebsitMaster.logourl,CompanyAddressMaster.Website,WareHouseMaster.Name as Wname,CompanyWebsitMaster.MasterEmailId, CompanyWebsitMaster.OutGoingMailServer,CompanyWebsitMaster.WebMasterEmail,CompanyWebsitMaster.EmailMasterLoginPassword, User_master.EmailID,User_master.UserID,CompanyMaster.CompanyLogo,CompanyMaster.CompanyName,CompanyAddressMaster.Address1,CompanyAddressMaster.Address2,CompanyAddressMaster.Email,CompanyAddressMaster.Fax,CompanyAddressMaster.Phone from User_master inner join Party_master on  Party_master.PartyID=User_master.PartyID inner join WareHouseMaster on WareHouseMaster.WareHouseId=Party_master.Whid inner join CompanyMaster on CompanyMaster.Compid=WareHouseMaster.comid inner join CompanyAddressMaster on CompanyAddressMaster.CompanyMasterId=CompanyMaster.CompanyId inner join CompanyWebsitMaster on CompanyWebsitMaster.WHId=WareHouseMaster.WareHouseId where User_master.UserID='" + Session["userid"].ToString() + "'";
        SqlCommand cmd = new SqlCommand(str, connection);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            string companyid = dt.Rows[0]["Compid"].ToString();

            SqlDataAdapter daas = new SqlDataAdapter("select * from Party_master inner join User_master on User_master.PartyID=Party_master.PartyID inner join Login_master on User_master.UserID=Login_master.UserID where User_master.UserID='" + Session["userid"].ToString() + "'", connection);
            DataTable dtss = new DataTable();
            daas.Fill(dtss);
            string employeenamee = "";
            if (dtss.Rows.Count > 0)
            {
                employeenamee = Convert.ToString(dtss.Rows[0]["Compname"]);
            }


            try
            {
                string strdy = " select distinct  PortalMasterTbl.* from  CompanyMaster inner join PricePlanMaster  on CompanyMaster.PricePlanId=PricePlanMaster.PricePlanId inner join VersionInfoMaster on VersionInfoMaster.VersionInfoId=PricePlanMaster.VersionInfoMasterId inner join ProductMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId inner join ClientMaster on ProductMaster.ClientMasterId=ClientMaster.ClientMasterId inner join Priceplancategory on Priceplancategory.ID=PricePlanMaster.PriceplancatId inner join PortalMasterTbl on PortalMasterTbl.Id=Priceplancategory.PortalId   inner join OrderMaster on CompanyMaster.CompanyLoginId=OrderMaster.CompanyLoginId inner join  OrderPaymentSatus on OrderMaster.OrderId=OrderPaymentSatus.OrderId inner join StateMasterTbl on StateMasterTbl.StateId=PortalMasterTbl.StateId inner join CountryMaster on StateMasterTbl.CountryId=CountryMaster.CountryId  WHERE(PortalMasterTbl.Id=7)  ";
                SqlCommand cmd45 = new SqlCommand(strdy, licenseconn);
                SqlDataAdapter adpdy = new SqlDataAdapter(cmd45);
                DataTable dtdy = new DataTable();
                adpdy.Fill(dtdy);
                string aa = "";
                string bb = "";
                string cc = "";
                string ff = "";
                string ee = "";
                string dd = "";
                string ext = "";
                string tollfree = "";
                string tollfreeext = "";
                if (dtdy.Rows.Count > 0)
                {

                    if (Convert.ToString(dtdy.Rows[0]["Supportteamphonenoext"].ToString()) != "" && Convert.ToString(dtdy.Rows[0]["Supportteamphonenoext"].ToString()) != null)
                    {
                        ext = "ext " + dtdy.Rows[0]["Supportteamphonenoext"].ToString();
                    }

                    if (Convert.ToString(dtdy.Rows[0]["Tollfree"].ToString()) != "" && Convert.ToString(dtdy.Rows[0]["Tollfree"].ToString()) != null)
                    {
                        tollfree = dtdy.Rows[0]["Tollfree"].ToString();
                    }

                    if (Convert.ToString(dtdy.Rows[0]["Tollfree"].ToString()) != "" && Convert.ToString(dtdy.Rows[0]["Tollfree"].ToString()) != null)
                    {
                        tollfreeext = "ext " + dtdy.Rows[0]["Tollfreeext"].ToString();
                    }


                    aa = "" + dtdy.Rows[0]["Supportteammanagername"].ToString() + "- Support Manager";
                    bb = "" + dtdy.Rows[0]["PortalName"].ToString() + " ";
                    cc = "" + dtdy.Rows[0]["Supportteamphoneno"].ToString() + "  " + ext + " ";
                    dd = "" + tollfree + " " + tollfreeext + " ";
                    ee = "" + dtdy.Rows[0]["Portalmarketingwebsitename"].ToString() + "";
                    // ff = "" + dt21.Rows[0]["City"].ToString() + " " + dt21.Rows[0]["StateName"].ToString() + " " + dt21.Rows[0]["CountryName"].ToString() + " " + dt21.Rows[0]["Zip"].ToString() + " ";
                }
                string tomail = dtss.Rows[0]["Email"].ToString();
                string file = "job-center-logo.jpg";
                string bodyformate = "<br>  <img src=\"http://members.ijobcenter.com/images/" + file + "\" \"border=\"0\" Width=\"90\" Height=\"80\" / > <br>Dear " + dtss.Rows[0]["Compname"].ToString() + ", <br><br>The account information for your ijobcenter.com account has been successfully changed.<br><br><br>Thank you,</span><br>IJobCenter Support Team<br>" + aa + "<br>" + bb + "<br>" + cc + "<br>" + dd + "<br>" + ee + "";
                if (dtdy.Rows.Count > 0)
                {
                    string email = Convert.ToString(dtdy.Rows[0]["UserIdtosendmail"]);
                    string displayname = Convert.ToString("IJobCenter");
                    string password = Convert.ToString(dtdy.Rows[0]["Password"]);
                    string outgo = Convert.ToString(dtdy.Rows[0]["Mailserverurl"]);
                    string body1 = bodyformate;
                    string Subject = "Account is successfully reseted";


                    MailAddress to = new MailAddress(tomail);//info@ijobcenter.com("company12@safestmail.net");//
                    MailAddress from = new MailAddress(email, displayname);
                    MailMessage objEmail = new MailMessage(from, to);
                    objEmail.Subject = Subject.ToString();
                    objEmail.Body = body1.ToString();

                    objEmail.IsBodyHtml = true;
                    objEmail.Priority = MailPriority.High;
                    SmtpClient client = new SmtpClient();
                    client.Credentials = new NetworkCredential(email, password);
                    client.Host = outgo;
                    client.Send(objEmail);
                    lblmsg.Visible = true;


                    lblmsg.Text = ViewState["Confirmationby"].ToString() + " accepted. A confirmation email has been sent to your email ID.";


                   
                }

            }
            catch (Exception e)
            {
                lblmsg.Visible = true;
                //lblmsg.Text = "Error:" + e.ToString();

            }
        }

    }

    protected void updateinsertsecurityquestion()
    {
        ViewState["Duplication"] = "0";

        int ques1 = 0;
        ques1 = Convert.ToInt32(ddlquestion1.SelectedValue);
        int ques2 = 0;
        ques2 = Convert.ToInt32(ddlquestion2.SelectedValue);
        int ques3 = 0;
        ques3 = Convert.ToInt32(ddlquestion3.SelectedValue);

        if (ques1 == ques2)
        {
            ViewState["Duplication"] = "1";
            Label4.Text = "Duplicate question is selected";
        }
        else if (ques2 == ques3)
        {
            ViewState["Duplication"] = "1";
            Label4.Text = "Duplicate question is selected";
        }
        else if (ques3 == ques1)
        {
            ViewState["Duplication"] = "1";

            Label4.Text = "Duplicate question is selected";
        }
        else
        {


            // for question 1
            Label4.Text = " ";

            string strquestion = "Select SecurityQuestionMaster.*,SecurityQuestion.Id as ansid,SecurityQuestion.SequrityQueId,SecurityQuestion.UserId,SecurityQuestion.Answer from SecurityQuestionMaster inner join SecurityQuestion on SecurityQuestionMaster.id = SecurityQuestion.SequrityQueId where SecurityQuestion.UserId = '" + Session["userid"] + "' and SecurityQuestion.QuestionNo='1'";
            DataSet dsquestion = (DataSet)fillddl(strquestion);
            string updateque = "";
            int cout = 0;
            if (dsquestion.Tables[0].Rows.Count > 0)
            {


                updateque = "update SecurityQuestion set SequrityQueId='" + ddlquestion1.SelectedValue + "',Answer='" + txtanswer1.Text + "' where UserId='" + Session["userid"] + "' and Id='" + dsquestion.Tables[0].Rows[0]["ansid"] + "' and QuestionNo='1' ";
                cout = 0;
            }
            else
            {
                updateque = "insert into SecurityQuestion(SequrityQueId,UserId,Answer,QuestionNo) values ('" + ddlquestion1.SelectedValue + "','" + Session["userid"] + "','" + txtanswer1.Text + "','1')";
               // updateque = "insertSecurityQuestion";
                cout = 1;
            }
            SqlCommand cmdupdateque = new SqlCommand(updateque, connection);
            if (connection.State.ToString() != "Open")
            {
                connection.Open();
            }

            //if(cout==1)
            //{
            //cmdupdateque.CommandType = CommandType.StoredProcedure;
            //cmdupdateque.Parameters.AddWithValue("@SequrityQueId", ddlquestion1.SelectedValue);
            //cmdupdateque.Parameters.AddWithValue("@UserId",  Session["userid"] );
            //cmdupdateque.Parameters.AddWithValue("@Answer", txtanswer1.Text);
            //cmdupdateque.Parameters.AddWithValue("@QuestionNo", 1);
            //}
            cmdupdateque.ExecuteNonQuery();
            connection.Close();

            // end  for question 1


            // for question 2

            string strquestion2 = "Select SecurityQuestionMaster.*,SecurityQuestion.Id as ansid,SecurityQuestion.SequrityQueId,SecurityQuestion.UserId,SecurityQuestion.Answer from SecurityQuestionMaster inner join SecurityQuestion on SecurityQuestionMaster.id = SecurityQuestion.SequrityQueId where SecurityQuestion.UserId = '" + Session["userid"] + "' and SecurityQuestion.QuestionNo='2'";
            DataSet dsquestion2 = (DataSet)fillddl(strquestion2);

            string updateque1 = "";

            if (dsquestion2.Tables[0].Rows.Count > 0)
            {


                updateque1 = "update SecurityQuestion set SequrityQueId='" + ddlquestion2.SelectedValue + "',Answer='" + txtanswer2.Text + "' where UserId='" + Session["userid"] + "' and Id='" + dsquestion2.Tables[0].Rows[0]["ansid"] + "' and QuestionNo='2' ";
                cout = 0;
            }
            else
            {
               updateque1 = "insert into SecurityQuestion(SequrityQueId,UserId,Answer,QuestionNo) values ('" + ddlquestion2.SelectedValue + "','" + Session["userid"] + "','" + txtanswer2.Text + "','2')";
              
                cout = 1;
            }
            SqlCommand cmdupdateque1 = new SqlCommand(updateque1, connection);
            if (connection.State.ToString() != "Open")
            {
                connection.Open();
            }
            //if (cout == 1)
            //{
            //    cmdupdateque1.CommandType = CommandType.StoredProcedure;
            //    cmdupdateque1.Parameters.AddWithValue("@SequrityQueId", ddlquestion2.SelectedValue);
            //    cmdupdateque1.Parameters.AddWithValue("@UserId", Session["userid"]);
            //    cmdupdateque1.Parameters.AddWithValue("@Answer", txtanswer2.Text);
            //    cmdupdateque1.Parameters.AddWithValue("@QuestionNo", 2);
            //}
            cmdupdateque1.ExecuteNonQuery();
            connection.Close();

            // end for question 2

            //for question 3

            string strquestion3 = "Select SecurityQuestionMaster.*,SecurityQuestion.Id as ansid,SecurityQuestion.SequrityQueId,SecurityQuestion.UserId,SecurityQuestion.Answer from SecurityQuestionMaster inner join SecurityQuestion on SecurityQuestionMaster.id = SecurityQuestion.SequrityQueId where SecurityQuestion.UserId = '" + Session["userid"] + "' and SecurityQuestion.QuestionNo='3'";
            DataSet dsquestion3 = (DataSet)fillddl(strquestion3);

            string updateque2 = "";
            if (dsquestion3.Tables[0].Rows.Count > 0)
            {


                updateque2 = "update SecurityQuestion set SequrityQueId='" + ddlquestion3.SelectedValue + "',Answer='" + txtanswer3.Text + "' where UserId='" + Session["userid"] + "'  and Id='" + dsquestion3.Tables[0].Rows[0]["ansid"] + "'  and QuestionNo='3' ";
                cout = 0;
            }
            else
            {
                updateque2 = "insert into SecurityQuestion(SequrityQueId,UserId,Answer,QuestionNo) values ('" + ddlquestion3.SelectedValue + "','" + Session["userid"] + "','" + txtanswer3.Text + "','3')";
               
                cout = 1;
            }
            SqlCommand cmdupdateque2 = new SqlCommand(updateque2, connection);
            if (connection.State.ToString() != "Open")
            {
                connection.Open();
            }
            //if (cout == 1)
            //{
            //    cmdupdateque2.CommandType = CommandType.StoredProcedure;
            //    cmdupdateque2.Parameters.AddWithValue("@SequrityQueId", ddlquestion3.SelectedValue);
            //    cmdupdateque2.Parameters.AddWithValue("@UserId", Session["userid"]);
            //    cmdupdateque2.Parameters.AddWithValue("@Answer", txtanswer3.Text);
            //    cmdupdateque2.Parameters.AddWithValue("@QuestionNo", 3);
            //}
            cmdupdateque2.ExecuteNonQuery();
            connection.Close();
            //end for question 3

            ViewState["Duplication"] = "0";

        }
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox1.Checked == true)
        {
            pnlchangeuserid.Visible = true;
        }
        if (CheckBox1.Checked == false)
        {
            pnlchangeuserid.Visible = false;
        }
    }
    protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox2.Checked == true)
        {
            pnlchangepassword.Visible = true;
        }
        if (CheckBox2.Checked == false)
        {
            pnlchangepassword.Visible = false;
        }
    }
    protected void CheckBox3_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox3.Checked == true)
        {
            pnlchangeempcode.Visible = true;
        }
        if (CheckBox3.Checked == false)
        {
            pnlchangeempcode.Visible = false;
        }
    }

    protected void fillquestion1()
    {
        string str = "Select * from SecurityQuestionMaster where Active='1'";
        ddlquestion1.DataSource = (DataSet)fillddl(str);
        fillddlOther(ddlquestion1, "QueName", "id");
        ddlquestion1.Items.Insert(0, "-Select any question-");
        ddlquestion1.Items[0].Value = "0";
    }
    protected void fillquestion2()
    {
        string str = "Select * from SecurityQuestionMaster where Active='1'";
        ddlquestion2.DataSource = (DataSet)fillddl(str);
        fillddlOther(ddlquestion2, "QueName", "id");
        ddlquestion2.Items.Insert(0, "-Select any question-");
        ddlquestion2.Items[0].Value = "0";
    }
    protected void fillquestion3()
    {
        string str = "Select * from SecurityQuestionMaster where Active='1'";

        ddlquestion3.DataSource = (DataSet)fillddl(str);
        fillddlOther(ddlquestion3, "QueName", "id");
        ddlquestion3.Items.Insert(0, "-Select any questions-");
        ddlquestion3.Items[0].Value = "0";
    }


    protected void fillquestion111()
    {
        string str = "Select * from SecurityQuestionMaster where Active='1'";
        ddlquestion1.DataSource = (DataSet)fillddl123(str);
        fillddlOther(ddlquestion1, "QueName", "id");
        ddlquestion1.Items.Insert(0, "-Select any question-");
        ddlquestion1.Items[0].Value = "0";
    }
    protected void fillquestion222()
    {
        string str = "Select * from SecurityQuestionMaster where Active='1'";
        ddlquestion2.DataSource = (DataSet)fillddl123(str);
        fillddlOther(ddlquestion2, "QueName", "id");
        ddlquestion2.Items.Insert(0, "-Select any question-");
        ddlquestion2.Items[0].Value = "0";
    }
    protected void fillquestion333()
    {
        string str = "Select * from SecurityQuestionMaster where Active='1'";

        ddlquestion3.DataSource = (DataSet)fillddl123(str);
        fillddlOther(ddlquestion3, "QueName", "id");
        ddlquestion3.Items.Insert(0, "-Select any questions-");
        ddlquestion3.Items[0].Value = "0";
    }
}
