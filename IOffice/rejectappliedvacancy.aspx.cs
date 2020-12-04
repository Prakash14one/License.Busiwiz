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

public partial class rejectappliedvacancy : System.Web.UI.Page
{
    //SqlConnection connection1 = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ToString());
    //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["jobcenter.OADBConnectionString"].ToString());
    SqlConnection con;
    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
       // PageConn.strEnc = "3d70f5cff23ed17ip8H9";
        con = pgcon.dynconn;
        if (!IsPostBack)
        {


            
            if (Request.QueryString["canid"] != null && Request.QueryString["vacid"] != "")
            {
               
               // string vacid = ClsEncDesc.Decrypted(Convert.ToString(Request.QueryString["vacid"]));//Convert.ToInt32(Request.QueryString["id"]);
               // string candid = ClsEncDesc.Decrypted(Convert.ToString(Request.QueryString["canid"]));

                Session["vacanyid"] = Convert.ToString(Request.QueryString["vacid"]);
                Session["candid"] = Convert.ToString(Request.QueryString["canid"]);

                reject();
            }
        }
    }
    public void reject()
    {
        DataTable dt12 = select1("select vacancypositiontitleid,comid from VacancyMasterTbl where ID = " + Session["vacanyid"] + "");//vacancy id,contact name,id,compid
        DataTable dt14 = select1("select CandidateMaster.FirstName +' '+ CandidateMaster.LastName as name from CandidateMaster where  CandidateId = " + Session["candid"] + "");
        DataTable dt13 = select1("select VacancyPositionTitle from VacancyPositionTitleMaster where ID = " + dt12.Rows[0]["vacancypositiontitleid"].ToString() + "");//vacancy name
        DataTable dt = select1("select * from ShortListcandidates where Vacancy_id=" + dt12.Rows[0]["vacancypositiontitleid"].ToString() + " and Candidate_id=" + Session["candid"] + "");
       ViewState["comid"]=dt12.Rows[0][1].ToString();
        ViewState["vacancyname"]=dt13.Rows[0][0].ToString();
        
        if (dt.Rows.Count > 0)
        {
            lblmsg.Text = "Sorry, this link is no longer valid";
            
        }
        else
        {
            con.Open();
            string doc = "delete from VacancyDocuments where CandidateID=" + Session["candid"] + " and Vacancyid=" + Session["vacanyid"] + " ";
            SqlCommand cmd1 = new SqlCommand(doc, con);
            cmd1.ExecuteNonQuery();
            string app = "delete from VacancyAppReceive where CandidateID=" + Session["candid"] + " and Vacancyid=" + Session["vacanyid"] + "";
            SqlCommand cmd2 = new SqlCommand(app, con);
            cmd2.ExecuteNonQuery();
            string status = "delete from CandidateVacancyStatus where VacancyID=" + Session["vacanyid"] + " and CandidateID=" + Session["candid"] + "";
            SqlCommand cmd3 = new SqlCommand(status, con);
            cmd3.ExecuteNonQuery();
            con.Close();
            lblmsg.Text = " " + dt14.Rows[0]["name"].ToString() + "   has been rejected for  vacancy " + dt13.Rows[0]["VacancyPositionTitle"].ToString() + "";
            
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
    public void sendmail()
    {
        string ss = "  select CompanyName from CompanyMaster where Compid='" + ViewState["comid"] + "'";
        SqlDataAdapter da34 = new SqlDataAdapter(ss, con);
        DataTable dt34 = new DataTable();
        da34.Fill(dt34);




        string cand = "SELECT CandidateMaster.FirstName, Party_master.Email  FROM  CandidateMaster INNER JOIN Party_master ON CandidateMaster.PartyID = Party_master.PartyID  WHERE CandidateMaster.CandidateId =" + Session["candid"] + " ";
        SqlDataAdapter da1 = new SqlDataAdapter(cand, con);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);
        ViewState["Name"]=dt1.Rows[0][0].ToString();
        string mailto = dt1.Rows[0][1].ToString();//"company17@safestmail.net"; //



        try
        {
            string str21 = "  select distinct  PortalMasterTbl.* from  CompanyMaster inner join PricePlanMaster  on CompanyMaster.PricePlanId=PricePlanMaster.PricePlanId inner join VersionInfoMaster on VersionInfoMaster.VersionInfoId=PricePlanMaster.VersionInfoMasterId inner join ProductMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId inner join ClientMaster on ProductMaster.ClientMasterId=ClientMaster.ClientMasterId inner join Priceplancategory on Priceplancategory.ID=PricePlanMaster.PriceplancatId inner join PortalMasterTbl on PortalMasterTbl.Id=Priceplancategory.PortalId   inner join OrderMaster on CompanyMaster.CompanyLoginId=OrderMaster.CompanyLoginId inner join  OrderPaymentSatus on OrderMaster.OrderId=OrderPaymentSatus.OrderId  inner join StateMasterTbl on StateMasterTbl.StateId=PortalMasterTbl.StateId inner join CountryMaster on StateMasterTbl.CountryId=CountryMaster.CountryId  WHERE(PortalMasterTbl.Id=7)  ";
            SqlCommand cmd45 = new SqlCommand(str21, PageConn.licenseconn());
            SqlDataAdapter adp45 = new SqlDataAdapter(cmd45);
            DataTable dt21 = new DataTable();
            adp45.Fill(dt21);

            string aa = "";
            string bb = "";
            string cc = "";
            string ff = "";
            string ee = "";
            string dd = "";
            string ext = "";
            string tollfree = "";
            string tollfreeext = "";


            if (Convert.ToString(dt21.Rows[0]["Supportteamphonenoext"].ToString()) != "" && Convert.ToString(dt21.Rows[0]["Supportteamphonenoext"].ToString()) != null)
            {
                ext = "ext " + dt21.Rows[0]["Supportteamphonenoext"].ToString();
            }

            if (Convert.ToString(dt21.Rows[0]["Tollfree"].ToString()) != "" && Convert.ToString(dt21.Rows[0]["Tollfree"].ToString()) != null)
            {
                tollfree = dt21.Rows[0]["Tollfree"].ToString();
            }

            if (Convert.ToString(dt21.Rows[0]["Tollfree"].ToString()) != "" && Convert.ToString(dt21.Rows[0]["Tollfree"].ToString()) != null)
            {
                tollfreeext = "ext " + dt21.Rows[0]["Tollfreeext"].ToString();
            }
            if (dt21.Rows.Count > 0)
            {

                aa = "" + dt21.Rows[0]["Supportteammanagername"].ToString() + "- Support Manager";
                bb = "" + dt21.Rows[0]["PortalName"].ToString() + " ";
                cc = "" + dt21.Rows[0]["Supportteamphoneno"].ToString() + "  " + ext + " ";
                dd = "" + tollfree + " " + tollfreeext + " ";
                ee = "" + dt21.Rows[0]["Portalmarketingwebsitename"].ToString() + "";
            }
            if (dt21.Rows.Count > 0)
            {
                string file = "jobcenterlogo.jpg";
                string body1 = "<br><img src=\"http://http://www.ijobcenter.com/Images/" + file + "\" \"border=\"0\" Width=\"90\" Height=\"80\" / >  <br>  Dear " + ViewState["Name"] + " <br><br>" + dt34.Rows[0][0].ToString() + " has rejected your applied vacancy " + ViewState["vacancyname"] + ". <br><br>Thank you,</span><br><br>IJobCenter Support Team<br>" + aa + "<br>" + bb + "<br>" + cc + "<br>" + ee + "";

                string email = Convert.ToString(dt21.Rows[0]["UserIdtosendmail"]);
                string displayname = Convert.ToString("IJobCenter");
                string password = Convert.ToString(dt21.Rows[0]["Password"]);
                string outgo = Convert.ToString(dt21.Rows[0]["Mailserverurl"]);
                string body = body1;
                string Subject = "Reject your applied vacancy";

                MailAddress to = new MailAddress(mailto);
                MailAddress from = new MailAddress(email, displayname);
                MailMessage objEmail = new MailMessage(from, to);
                objEmail.Subject = Subject.ToString();
                objEmail.Body = body.ToString();
                objEmail.IsBodyHtml = true;
                objEmail.Priority = MailPriority.High;
                SmtpClient client = new SmtpClient();
                client.Credentials = new NetworkCredential(email, password);
                client.Host = outgo;
                client.Send(objEmail);
            }

        }


        catch
        {
            lblmsg.Text = "Error..... ";

        }
    }
}