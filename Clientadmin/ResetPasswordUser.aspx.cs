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
           
            string str = "Select * from SecurityQuestionMaster where Active='1'";
            ddlquestion1.DataSource = (DataSet)fillddl(str);
            fillddlOther(ddlquestion1, "QueName", "id");
            ddlquestion1.Items.Insert(0, "-Select any question-");
            ddlquestion1.Items[0].Value = "0";

            ddlquestion2.DataSource = (DataSet)fillddl(str);
            fillddlOther(ddlquestion2, "QueName", "id");
            ddlquestion2.Items.Insert(0, "-Select any question-");
            ddlquestion2.Items[0].Value = "0";

            ddlquestion3.DataSource = (DataSet)fillddl(str);
            fillddlOther(ddlquestion3, "QueName", "id");
            ddlquestion3.Items.Insert(0, "-Select any questions-");
            ddlquestion3.Items[0].Value = "0";

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

            Session["userid"] = dtloginuser.Rows[0]["UserID"].ToString();
           
        }
        else
        {
            pnlsecurityquestion.Visible = false;
            lblmsg.Text = "wrong username or password.";
 
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
    public void fillddlOther(DropDownList ddl, String dtf, String dvf)
    {
        ddl.DataTextField = dtf;
        ddl.DataValueField = dvf;
        ddl.DataBind();

    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        string upd = "Update Login_master Set password='" + ClsEncDesc.Encrypted(txtnewpassword.Text) + "',username='" + txtnewuserid.Text + "' where UserId='" + Session["userid"] + "'";
        SqlCommand cmdupd = new SqlCommand(upd, connection);
        if (connection.State.ToString() != "Open")
        {
            connection.Open();
        }
         cmdupd.ExecuteNonQuery();
         connection.Close();

        string updateusermaster = "Update User_master set Username='" + txtnewuserid.Text + "' where UserID='" + Session["userid"] + "'";
        SqlCommand cmdusermaster = new SqlCommand(updateusermaster, connection);
        if (connection.State.ToString() != "Open")
        {
            connection.Open();
        }
        cmdusermaster.ExecuteNonQuery();
        connection.Close();

        updateinsertsecurityquestion();


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

        lblmsg.Visible = true;
        lblmsg.Text = "Password updated succesfully";
        pnlsecurityquestion.Visible = false;

    }

    protected void updateinsertsecurityquestion()
    {
        int ques1 = 0;
        ques1 = Convert.ToInt32(ddlquestion1.SelectedValue);
        int ques2 = 0;
        ques2 = Convert.ToInt32(ddlquestion2.SelectedValue);
        int ques3 = 0;
        ques3 = Convert.ToInt32(ddlquestion3.SelectedValue);
        if (ques1 == ques2)
        {
            Label2.Text = "This question is already selected";
        }
        else if (ques2 == ques3)
        {
            Label2.Text = "This question is already selected";
        }
        else if (ques3 == ques1)
        {
            Label2.Text = "This question is already selected";
        }
        else
        {
            Label2.Text = " ";

            string strquestion = "Select SecurityQuestionMaster.*,SecurityQuestion.Id as ansid,SecurityQuestion.UserId,SecurityQuestion.Answer from SecurityQuestionMaster inner join SecurityQuestion on SecurityQuestionMaster.id = SecurityQuestion.SequrityQueId where SecurityQuestion.UserId = '" + Session["userid"] + "'";
            DataSet dsquestion = (DataSet)fillddl(strquestion);
            string updateque = "";
            Int32 row = 0;
            if (row < dsquestion.Tables[0].Rows.Count)
            {
                // if (dsquestion.Tables[0].Rows[0][""].ToString())
                updateque = "update SecurityQuestion set SequrityQueId='" + ddlquestion1.SelectedValue + "',Answer='" + txtanswer1.Text + "' where UserId='" + Session["userid"] + "' and Id='" + dsquestion.Tables[0].Rows[0]["ansid"] + "'";
            }
            else
            {
                updateque = "insert into SecurityQuestion(SequrityQueId,UserId,Answer) values ('" + ddlquestion1.SelectedValue + "','" + Session["userid"] + "','" + txtanswer1.Text + "')";
            }
            SqlCommand cmdupdateque = new SqlCommand(updateque, connection);
            if (connection.State.ToString() != "Open")
            {
                connection.Open();
            }
            cmdupdateque.ExecuteNonQuery();
            connection.Close();
            row = row + 1;
            string updateque1 = "";
            if (row < dsquestion.Tables[0].Rows.Count)
            {
                updateque1 = "update SecurityQuestion set SequrityQueId='" + ddlquestion2.SelectedValue + "',Answer='" + txtanswer2.Text + "' where UserId='" + Session["userid"] + "' and Id='" + dsquestion.Tables[0].Rows[1]["ansid"] + "'";
            }
            else
            {
                updateque1 = "insert into SecurityQuestion(SequrityQueId,UserId,Answer) values ('" + ddlquestion2.SelectedValue + "','" + Session["userid"] + "','" + txtanswer2.Text + "')";
            }
            SqlCommand cmdupdateque1 = new SqlCommand(updateque1, connection);
            if (connection.State.ToString() != "Open")
            {
                connection.Open();
            }
            cmdupdateque1.ExecuteNonQuery();
            connection.Close();
            row = row + 1;
            string updateque2 = "";
            if (row < dsquestion.Tables[0].Rows.Count)
            {
                updateque2 = "update SecurityQuestion set SequrityQueId='" + ddlquestion3.SelectedValue + "',Answer='" + txtanswer3.Text + "' where UserId='" + Session["userid"] + "'  and Id='" + dsquestion.Tables[0].Rows[2]["ansid"] + "'";
            }
            else
            {
                updateque2 = "insert into SecurityQuestion(SequrityQueId,UserId,Answer) values ('" + ddlquestion3.SelectedValue + "','" + Session["userid"] + "','" + txtanswer3.Text + "')";
            }
            SqlCommand cmdupdateque2 = new SqlCommand(updateque2, connection);
            if (connection.State.ToString() != "Open")
            {
                connection.Open();
            }
            cmdupdateque2.ExecuteNonQuery();
            connection.Close();

          
        }
    }
}
