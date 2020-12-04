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
    SqlConnection connection1;

    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        connection = pgcon.dynconn;

        if (!IsPostBack)
        {
            if (Request.QueryString["to"] != null)
            {
                string str = "SELECT CompanyLoginId from CompanyMaster where Websiteurl='" + Request.Url.Host.ToString() + "' and active='1'";

                SqlCommand cmd = new SqlCommand(str, PageConn.licenseconn());
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

                PageConn pgcon1 = new PageConn();
                connection1 = pgcon1.dynconn;                         

                pwd = ClsEncDesc.Decrypted(pwd);

                SqlCommand cmdlogin = new SqlCommand("SelectUserLogin", connection1);
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
        SqlCommand cmd = new SqlCommand(qry, connection1);
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
                string upd = "Update Login_master Set password='" + ClsEncDesc.Encrypted(txtnewpassword.Text) + "',username='" + txtnewuserid.Text + "' where UserId='" + Session["userid"] + "'";
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
                string updateusermaster = "Update User_master set Username='" + txtnewuserid.Text + "' where UserID='" + Session["userid"] + "'";
                SqlCommand cmdusermaster = new SqlCommand(updateusermaster, connection);
                if (connection.State.ToString() != "Open")
                {
                    connection.Open();
                }
                cmdusermaster.ExecuteNonQuery();
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

            txtanswer1.Text = "";
            txtanswer2.Text = "";
            txtanswer3.Text = "";

            lblmsg.Visible = true;
            lblmsg.Text = "Login information updated successfully.";
            pnlsecurityquestion.Visible = false;
            Panel2.Visible = true;

            Response.Redirect("Shoppingcartlogin.aspx");
        }
        else
        {
            Label4.Visible = true;
            Label4.Text = "Duplicate question is selected.";
            pnlsecurityquestion.Visible = true;
            Panel2.Visible = false;
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

            if (dsquestion.Tables[0].Rows.Count > 0)
            {


                updateque = "update SecurityQuestion set SequrityQueId='" + ddlquestion1.SelectedValue + "',Answer='" + txtanswer1.Text + "' where UserId='" + Session["userid"] + "' and Id='" + dsquestion.Tables[0].Rows[0]["ansid"] + "' and QuestionNo='1' ";
            }
            else
            {
                updateque = "insert into SecurityQuestion(SequrityQueId,UserId,Answer,QuestionNo) values ('" + ddlquestion1.SelectedValue + "','" + Session["userid"] + "','" + txtanswer1.Text + "','1')";
            }
            SqlCommand cmdupdateque = new SqlCommand(updateque, connection);
            if (connection.State.ToString() != "Open")
            {
                connection.Open();
            }
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
            }
            else
            {
                updateque1 = "insert into SecurityQuestion(SequrityQueId,UserId,Answer,QuestionNo) values ('" + ddlquestion2.SelectedValue + "','" + Session["userid"] + "','" + txtanswer2.Text + "','2')";
            }
            SqlCommand cmdupdateque1 = new SqlCommand(updateque1, connection);
            if (connection.State.ToString() != "Open")
            {
                connection.Open();
            }
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
            }
            else
            {
                updateque2 = "insert into SecurityQuestion(SequrityQueId,UserId,Answer,QuestionNo) values ('" + ddlquestion3.SelectedValue + "','" + Session["userid"] + "','" + txtanswer3.Text + "','3')";
            }
            SqlCommand cmdupdateque2 = new SqlCommand(updateque2, connection);
            if (connection.State.ToString() != "Open")
            {
                connection.Open();
            }
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
