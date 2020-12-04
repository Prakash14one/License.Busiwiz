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
using System.Net.Mime;

public partial class ShoppingCart_Admin_Default4 : System.Web.UI.Page
{
    SqlConnection con;
    string compid;
    int uid;

    int whid;

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

        compid = Session["Comid"].ToString();
        Page.Title = pg.getPageTitle(page);

        uid = Convert.ToInt32(Session["userid"]);
        getwhid();

        if (!IsPostBack)
        {         
         lblalphanum.Text = alphanum();
         lblnum.Text = number(8).ToString();

               
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string te = "giftcertdetail.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("insert into GiftCertificateMasterTbl values('" + compid + "','" + whid + "','" + uid + "','" + lblnum.Text + "','" + lblalphanum.Text + "','" + TextBox2.Text + "',0,'" + TextBox1.Text + "','" + TextBox1.Text + "','','"+DateTime.Now.ToString()+"',0)", con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd.ExecuteNonQuery();
        con.Close();



        if (CheckBox1.Checked == true)
        {

            bool success = sendmail(TextBox2.Text);
            if (success == true)
            {
                lblmessage.Visible = true;
                lblmessage.Text = "Mail Sent";
            }
            else
            {
                lblmessage.Visible = true;
                lblmessage.Text = "Please Enter the Email Address";
            }
        }
        else
        { 
        
        }

        cancel();
    }

    public string  alphanum()
    {
        Guid g = Guid.NewGuid();
        string r = g.ToString();
        string alpha=r.Substring(0, 8);
        return alpha;
    }
    //public int num(double l, double u)
    //{
    //    Random rn = new Random();
    //    return rn.NextDouble(l,u);
    //}
    public static Int64 number(int s)
    {
        Random rn = new Random((int)DateTime.Now.Ticks);
        StringBuilder bl = new StringBuilder();
        string ass;
        for (int i = 0; i < s;i++ )
        {
            ass = Convert.ToString(Convert.ToInt32(Math.Floor(26 * rn.NextDouble() + 65)));
            bl.Append(ass);
        }
        return Convert.ToInt64((bl.ToString()));
    }

    protected void getwhid()
    {
        SqlCommand cmd = new SqlCommand("select WareHouseMaster.WareHouseId from User_Master inner join Party_Master on Party_Master.PartyID=User_Master.PartyID inner join WareHouseMaster on WareHouseMaster.WareHouseId=Party_Master.whid where User_Master.UserID='"+ uid +"'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        whid=Convert.ToInt32(dt.Rows[0]["WareHouseId"]);
    }

    protected void cancel()
    {
        TextBox1.Text = "";
        TextBox2.Text = "";
    }

    public string getWelcometext()
    {
       // SqlConnection conn = new SqlConnection(strconn);

        string str = "SELECT EmailContentMaster.EmailContent, EmailContentMaster.EntryDate, CompanyWebsitMaster.SiteUrl, EmailTypeMaster.EmailTypeId " +
                    " FROM CompanyWebsitMaster INNER JOIN " +
                      " EmailContentMaster ON CompanyWebsitMaster.CompanyWebsiteMasterId = EmailContentMaster.CompanyWebsiteMasterId INNER JOIN " +
                      " EmailTypeMaster ON EmailContentMaster.EmailTypeId = EmailTypeMaster.EmailTypeId " +
                    " WHERE     (EmailTypeMaster.EmailTypeId = 1) AND (CompanyWebsitMaster.SiteUrl = 'www.IndianMall.com')  and (EmailTypeMaster.Compid='" + compid + "')" +
                    " ORDER BY EmailContentMaster.EntryDate DESC ";
        SqlCommand cmd = new SqlCommand(str, con);
        //cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        string welcometext = "";
        if (ds.Rows.Count > 0)
        {
            welcometext = ds.Rows[0]["EmailContent"].ToString();
        }
        return welcometext;

    }

    public Boolean sendmail(string To)
    {

        string ADDRESSEX = "select CompanyMaster.CompanyLogo,CompanyWebsitMaster.EmailMasterLoginPassword,CompanyWebsitMaster.EmailSentDisplayName,CompanyWebsitMaster.Sitename,CompanyWebsitMaster.MasterEmailId,CompanyWebsitMaster.OutGoingMailServer,warehousemaster.name as Wname,user_master.name as Uname,CompanyMaster.CompanyName,GiftCertificateMasterTbl.* from [GiftCertificateMasterTbl] inner join CompanyMaster on CompanyMaster.Compid=GiftCertificateMasterTbl.CompanyId inner join CompanyWebsitMaster on CompanyWebsitMaster.companyid=CompanyMaster.companyid inner join user_master on user_master.userid=[GiftCertificateMasterTbl].userid inner join party_master on Party_Master.PartyID=User_Master.PartyID inner join WareHouseMaster on WareHouseMaster.WareHouseId=Party_Master.whid where GiftCertificateMasterTbl.companyid='" + compid + "' and GiftCertificateMasterTbl.BusinessID='" + whid + "'";
        SqlCommand cmd = new SqlCommand(ADDRESSEX, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        StringBuilder HeadingTable = new StringBuilder();
        HeadingTable.Append("<table width=\"100%\"> ");


        HeadingTable.Append("<tr><td width=\"50%\" style=\"padding-left:10px\" align=\"left\" > <img src=\"../images/" + ds.Rows[0]["CompanyLogo"].ToString() + "\" \"border=\"0\" Width=\"150px\" Height=\"125px\" / > </td><td style=\"padding-left:100px\" width=\"50%\" align=\"left\"><table><tr><td colspan=\"2\"><b><span style=\"color: #996600\">" + ds.Rows[0]["CompanyName"].ToString() + "</span></b></td></tr><tr><td colspan=\"2\"><b>" + ds.Rows[0]["Wname"].ToString() + "</b></td></tr><tr><td colspan=\"2\">" + ds.Rows[0]["Uname"].ToString() + "</td></tr><tr><td><b>Card No. </b></td><td>" + ds.Rows[0]["CardNo"].ToString() + "</td></tr><tr><td><b>Card Passcode </b></td><td>" + ds.Rows[0]["CardPasscode"].ToString() + " </td></tr><tr><td><b>Max Amount </b></td><td> " + ds.Rows[0]["MaxAmount"].ToString() + "</td></tr><tr><td><b>Balance Amount </b></td><td>" + ds.Rows[0]["BalanceAmount"].ToString() + "</td></tr><tr><td><b>Date and Time </b></td><td>" + ds.Rows[0]["DateandTime"].ToString() + "</td></tr><tr><td><b>PayPalID </b></td><td>" + ds.Rows[0]["PayPalID"].ToString() + "</td></tr></table></td></tr>  ");


        HeadingTable.Append("</table> ");

     

       string welcometext = getWelcometext();


        
        string body = "<br>" + HeadingTable + "<br><br> Dear <strong><span style=\"color: #996600\"> " + ds.Rows[0]["Uname"].ToString() + " </span></strong> ,<br><br> <br><br><br><br><br> <span style=\"font-size: 10pt; color: #000000; font-family: Arial\"><br>Thanking you <br>Sincerely yours</span><br><strong><span style=\"color: #996600\"> " + ViewState["sitename"] + "<br>Admin Team<br>" + ds.Rows[0]["CompanyName"].ToString() + "/" + ds.Rows[0]["Sitename"].ToString() + "</span></strong>";
        if (ds.Rows[0]["MasterEmailId"].ToString() != "" && ds.Rows[0]["EmailSentDisplayName"].ToString() != "")
        {
            MailAddress to = new MailAddress(To);

            MailAddress from = new MailAddress("" + ds.Rows[0]["MasterEmailId"] + "", "" + ds.Rows[0]["EmailSentDisplayName"] + "");

          

            MailMessage objEmail = new MailMessage(from, to);
           
            objEmail.Subject = "Gift Cart - " + ds.Rows[0]["CompanyName"].ToString() + "/" + ds.Rows[0]["Sitename"].ToString() + " ";
           

            objEmail.Body = body.ToString();
            objEmail.IsBodyHtml = true;


            objEmail.Priority = MailPriority.Normal;

      
            SmtpClient client = new SmtpClient();

            client.Credentials = new NetworkCredential("" + ds.Rows[0]["MasterEmailId"] + "", "" + ds.Rows[0]["EmailMasterLoginPassword"] + "");
            client.Host = ds.Rows[0]["OutGoingMailServer"].ToString();


            client.Send(objEmail);
            return true;
        }
        else
        {
           
            return false;
        }
    }
}
