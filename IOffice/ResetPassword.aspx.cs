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
using System.Text;
using System.Data.SqlClient;

public partial class ShoppingCart_Admin_ResetPassword : System.Web.UI.Page
{
    SqlConnection connection = new SqlConnection(@"Data Source =C3\C3SERVERMASTER,30000; Initial Catalog = jobcenter.OADB; User ID=sa; Password=06De1963++; Persist Security Info=true;");
    SqlConnection licenseconn = new SqlConnection(@"Data Source=tcp:192.168.1.219,2810;Initial Catalog=License.Busiwiz; User ID=BuzRead; Password=Busiwiz2013++; Persist Security Info=true; ");
   
    protected void Page_Load(object sender, EventArgs e)
    {

        //PageConn pgcon = new PageConn();
        //connection = pgcon.dynconn;
        PageConn.strEnc = "3d70f5cff23ed17ip8H9";
        if (!IsPostBack)
        {
            lblmsg.Text = "";
            string str = "SELECT CompanyLoginId from CompanyMaster where Websiteurl='" + Request.Url.Host.ToString() + "' and active='1'";

            SqlCommand cmd = new SqlCommand(str, licenseconn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Session["Comid"] = Convert.ToString(dt.Rows[0]["CompanyLoginId"]);
            }

            string id = Request.QueryString["to"];
            string id1 = ClsEncDesc.Decrypted(id.ToString().Replace(" ", "+"));
            ViewState["id1"] = id1.ToString();
            string strmaxid = "Select UserId from PasswordResetRequestTbl where ID='" + id1.ToString() + "'";
            SqlCommand cmdmax = new SqlCommand(strmaxid, connection);
            SqlDataAdapter adptmax = new SqlDataAdapter(cmdmax);
            DataSet dsmax = new DataSet();
            adptmax.Fill(dsmax);
            ViewState["maxuserid"] = dsmax.Tables[0].Rows[0]["UserId"].ToString();
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
            fillquestion();

        }


    }
    protected void fillquestion()
    {
        lblque1.Text = "No Question set yet";
        lblque2.Text = "No Question set yet";
        lblque3.Text = "No Question set yet";

        string strquestion = "Select SecurityQuestionMaster.*,SecurityQuestion.UserId,SecurityQuestion.Answer from SecurityQuestionMaster inner join SecurityQuestion on SecurityQuestionMaster.id = SecurityQuestion.SequrityQueId where SecurityQuestion.UserId = '" + ViewState["UserID"] + "'";
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
            }
            else
            {
                lblque2.Visible = false;
                txtans2.Visible = false;
                lblque2.Text = "No question set yet";

            }
            if (dsquestion.Tables[0].Rows[2]["QueName"].ToString() != null)
            {
                lblque3.Visible = true;
                txtans3.Visible = true;
                lblque3.Text = dsquestion.Tables[0].Rows[2]["QueName"].ToString();
            }
            else
            {
                lblque3.Visible = false;
                txtans3.Visible = false;
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
            Panel2.Visible = true;
            Button1.Visible = false;
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {

        string chekeit = "Select * from PasswordResetRequestTbl where ID='" + ViewState["id1"] + "' and passwordchanged='1'";
        SqlCommand cmdchek = new SqlCommand(chekeit, connection);
        SqlDataAdapter adptchek = new SqlDataAdapter(cmdchek);
        DataTable dtchek = new DataTable();
        adptchek.Fill(dtchek);
        if (dtchek.Rows.Count > 0)
        {
            lblmsg.Text = "You have already changed your password.";
        }
        else
        {
            string strhour = "Select * from PasswordResetRequestTbl where ID='" + ViewState["id1"] + "'";
            SqlCommand cmdhour = new SqlCommand(strhour, connection);
            SqlDataAdapter adphour = new SqlDataAdapter(cmdhour);
            DataTable dthour = new DataTable();
            adphour.Fill(dthour);


            // DateTime datet = DateTime.Now.AddHours(-48);

            TimeSpan t1 = (Convert.ToDateTime(dthour.Rows[0]["RequestTimeanddate"].ToString()) - Convert.ToDateTime((System.DateTime.Now.ToString())));
            if (t1.Hours == -48)
            {
                lblmsg.Text = "You have already changed password last 48 hours so you can not change your password now, try after sometime.";
            }
            else
            {
                if (txtpass.Text.ToString() == txtresetpass.Text.ToString())
                {
                  

                    string upd = "Update Login_master Set password='" + ClsEncDesc.Encrypted(txtpass.Text) + "' where UserId='" + Convert.ToString(ViewState["UserID"]) + "'";
                    SqlCommand cmdupd = new SqlCommand(upd, connection);
                    if (connection.State.ToString() != "Open")
                    {
                        connection.Open();
                    }
                    int success = cmdupd.ExecuteNonQuery();
                    if (Convert.ToBoolean(success) == true)
                    {
                        string strinsert = "update PasswordResetRequestTbl set passwordchanged='1' where ID ='" + ViewState["id1"]+ "'";
                        SqlCommand cmdinsert = new SqlCommand(strinsert, connection);
                        if (connection.State.ToString() != "Open")
                        {
                            connection.Open();
                        }
                        cmdinsert.ExecuteNonQuery();
                        connection.Close();
                        lblmsg.Visible = true;
                        lblmsg.Text = "Password updated successfully";
                        Panel2.Visible = false;
                        Panel1.Visible = false;

                        
                    }
                    else
                    {

                        lblmsg.Visible = true;
                        lblmsg.Text = " Password not updated successfully";
                    }
                    connection.Close();
                }
                else
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "Confirm password not match";
                }
                txtpass.Text = "";
                txtresetpass.Text = "";
            }
        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        txtpass.Text = "";
        txtresetpass.Text = "";
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string strquestion = "Select SecurityQuestionMaster.*,SecurityQuestion.UserId,SecurityQuestion.Answer from SecurityQuestionMaster inner join SecurityQuestion on SecurityQuestionMaster.id = SecurityQuestion.SequrityQueId where SecurityQuestion.UserId = '" + ViewState["UserID"] + "'";
        SqlCommand cmdquestion = new SqlCommand(strquestion, connection);
        SqlDataAdapter adptquestion = new SqlDataAdapter(cmdquestion);
        DataSet dsquestion = new DataSet();
        adptquestion.Fill(dsquestion);
        if (dsquestion.Tables[0].Rows.Count > 0)
        {
            string ans1 = dsquestion.Tables[0].Rows[0]["Answer"].ToString();
            string ans2 = dsquestion.Tables[0].Rows[1]["Answer"].ToString();
            string ans3 = dsquestion.Tables[0].Rows[2]["Answer"].ToString();

            

            if (ans1 == txtans1.Text && ans2 == txtans2.Text && ans3 == txtans3.Text)
            {
                
                Panel2.Visible = true;
                lblmsg.Text = "";
                img1.Visible = true;
                Panel1.Visible = false;
                
            }
            else
            {
                lblmsg.Text = "your answers were not correct kindly answer them again";
                Panel2.Visible = false;
                img1.Visible = false;
                Panel1.Visible = true;
               
                
            }

            txtans1.Text = "";
            txtans2.Text = "";
            txtans3.Text = "";
        }
    }
}
