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
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;

public partial class shortlistingofcanddidatesbeforelogin : System.Web.UI.Page
{
    //jobcenter.OADBConnectionString
    SqlConnection connection1 = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ToString());
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["jobcenter.OADBConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //string vac = Request.QueryString["vacid"];
            //string can = Request.QueryString["canid"];
            //string vacid = ClsEncDesc.Decrypted(vac.ToString());//Convert.ToInt32(Request.QueryString["id"]);
            //string candid = ClsEncDesc.Decrypted(can.ToString());
            if (Request.QueryString["vacid"] != null && Request.QueryString["canid"] != null)
            {

                Session["vacanyid"] = Convert.ToInt32(Request.QueryString["vacid"]);
                Session["candid"] = Convert.ToInt32(Request.QueryString["canid"]);
              
               shortlist();
            }
        }
    }
    public void shortlist()
    {
        DataTable dt12 = select1("select vacancypositiontitleid,comid from VacancyMasterTbl where ID = " + Session["vacanyid"] + "");//vacancy id,contact name,id,compid
        DataTable dt14 = select1("select CandidateMaster.FirstName +' '+ CandidateMaster.LastName as name from CandidateMaster where  CandidateId = " + Session["candid"] + "");
        DataTable dt13 = select1("select VacancyPositionTitle from VacancyPositionTitleMaster where ID = " + dt12.Rows[0]["vacancypositiontitleid"].ToString() + "");//vacancy name

        DataTable dt = select1("select * from ShortListcandidates where Vacancy_id=" + dt12.Rows[0]["vacancypositiontitleid"].ToString() + " and Candidate_id=" + Session["candid"] + "");
        if (dt.Rows.Count > 0)
        {
            lblmsg.Text = "Sorry, this link is no longer valid";
            Label1.Text = "";
            Label2.Text = "";
        }
        else
        {
            SqlCommand code = new SqlCommand("SELECT max(candidate_code) as candidate_code  FROM ShortListcandidates ", con);
            SqlDataAdapter ada1 = new SqlDataAdapter();
            ada1.SelectCommand = code;
            DataTable adt = new DataTable();
            ada1.Fill(adt);
            //int cod = Convert.ToInt32(adt.Rows[0][0].ToString());
            if (adt.Rows.Count > 0 && adt.Rows[0][0].ToString() != null)
            {
                Session["candidate_code"] = adt.Rows[0]["candidate_code"].ToString();
            }
            else
            {
                Session["candidate_code"] = 11111111;      //frist time this no and than add 1 number each
            }
            int SyncroniceMax;
            try
            {
                SyncroniceMax = Convert.ToInt32(Session["candidate_code"]) + 1;
            }
            catch
            {
                SyncroniceMax = 11111111;
            }
            SqlCommand code1 = new SqlCommand("SELECT max(test_center_code) as test_center_code  FROM ShortListcandidates", con);
            SqlDataAdapter ada2 = new SqlDataAdapter();
            ada2.SelectCommand = code1;
            DataTable dt1 = new DataTable();
            ada2.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                Session["test_center_code"] = dt1.Rows[0]["test_center_code"].ToString();
            }
            else
            {
                Session["test_center_code"] = 11111100;      //frist time this no and than add 1 number each
            }
            int SyncroniceMax1;
            try
            {
                SyncroniceMax1 = Convert.ToInt32(Session["test_center_code"]) + 1;
            }
            catch
            {
                SyncroniceMax1 = 11111100;
            }

            con.Open();
            // string str = @"insert into ShortListcandidates(Vacancy_id,Candidate_id,User_id,Note,Active) values(" + dt_s.Rows[j][0].ToString() + "," + dt_s.Rows[j][1].ToString() + ",'" + dt_s.Rows[j][2].ToString() + "','" + dt_s.Rows[j][3].ToString() + "'," + dt_s.Rows[j][4].ToString() + ") ";
            SqlCommand cmd = new SqlCommand("insertShortListcandidates", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Vacancy_id", dt12.Rows[0]["vacancypositiontitleid"].ToString());
            cmd.Parameters.AddWithValue("@Candidate_id", Session["candid"]);
            cmd.Parameters.AddWithValue("@User_id", dt12.Rows[0]["comid"].ToString());
            cmd.Parameters.AddWithValue("@candidate_code", SyncroniceMax);//  SyncroniceMax
            cmd.Parameters.AddWithValue("@test_center_code", SyncroniceMax1);
            cmd.Parameters.AddWithValue("@Note", "");
            cmd.Parameters.AddWithValue("@Active", "Yes");
            cmd.ExecuteNonQuery();
            con.Close();
            Session["candidate_code"] = "";
            Session["test_center_code"] = "";
            lblmsg.Text = "The following candidate has been shortlisted for following vacancy";
            Label1.Text = "Mr/Mrs " + dt14.Rows[0]["name"].ToString() + " has been shortlisted for " + dt13.Rows[0]["VacancyPositionTitle"].ToString() + " vacancy";
            Label2.Text = "Thank you";
        }
    }
    protected DataTable select1(string str)
    {
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);
        return dt;
    }
}