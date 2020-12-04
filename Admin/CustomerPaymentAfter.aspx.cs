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

public partial class CustomerPaymentAfter : System.Web.UI.Page
{
     
    public string FillInfo()
    {
        string str = "";
        str = Session["BodyText"].ToString();
        return str;
    }
   //  SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ifilecabinateConnectionString"].ConnectionString);
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
         if (!Page.IsPostBack)
         {
             //string str = "SELECT     CompanyMaster.*, CompanyUserMaster.* " +
             //                 " FROM         CompanyMaster INNER JOIN " +
             //                 "  CompanyUserMaster ON CompanyMaster.CompanyId = CompanyUserMaster.CompanyId " +
             //                 " WHERE     (CompanyMaster.CompanyLoginId = '" + Session["CI"].ToString() + "') and (CompanyUserMaster.Username='" + Session["UN"].ToString() + "') and (CompanyUserMaster.Password='" + Session["UP"].ToString() + "')  ";

             //SqlCommand cmd = new SqlCommand(str, con);
             //SqlDataAdapter adp = new SqlDataAdapter(cmd);
             //DataTable dt = new DataTable();
             //adp.Fill(dt);
             //if (dt.Rows.Count > 0)
             //{
             //    if (dt.Rows[0]["HostId"].ToString() == "False")
             //    {
             //     //   btnLogin.Visible = true;
             //    }
             //    else                 
             //    //    btnLogin.Visible = false;
             //    }
             }
             btnLogin.Visible = false;
    }
 
    protected int checkDemo(DateTime setupdate, int plan, int CompanyId)
    {
        int i = 0;
        if (plan == 2)
        {
            
            DateTime dt = setupdate.AddMonths(6);
            if (dt < System.DateTime.Now.Date)
            {
                i = 1;
                string str = "update CompanyMaster set active=0 where CompanyId='"+ CompanyId +"'";
                SqlCommand cmd = new SqlCommand(str, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
          
        }
        return i;

    }
    //protected void btnLoginNow_Click(object sender, EventArgs e)
    //{
    
        
    //}
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string str = "SELECT     CompanyMaster.*, CompanyUserMaster.* " +
                           " FROM         CompanyMaster INNER JOIN " +
                           "  CompanyUserMaster ON CompanyMaster.CompanyId = CompanyUserMaster.CompanyId " +
                           " WHERE     (CompanyMaster.CompanyLoginId = '" + Session["CI"].ToString() + "') and (CompanyUserMaster.Username='" + Session["UN"].ToString() + "') and (CompanyUserMaster.Password='" + Session["UP"].ToString() + "')  ";

        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            if (Convert.ToBoolean(dt.Rows[0]["active"]) == true)
            {
                int i = checkDemo(Convert.ToDateTime(dt.Rows[0]["date"]), Convert.ToInt32(dt.Rows[0]["PlanId"]), Convert.ToInt32(dt.Rows[0]["CompanyId"]));
                if (i == 0)
                {
                    string url = "http://" + dt.Rows[0]["redirect"].ToString() + ".ifilecabinet.com/Default.aspx?uname=" + Session["UN"].ToString() + "&pwd=" + Session["UP"].ToString() + "";
                    Session["username"] = Session["UN"].ToString(); // txtUserId.Text;
                    Session["password"] = Session["UP"].ToString(); //txtPassword.Text;
                    Response.Redirect(url);
                }
                else
                {
                    // lblmsg.Text = "Your Free Account is Deactive.";
                }
            }
            else
            {
                string str1 = "SELECT     TempDomainId, TempDomainName, Alloted, Active, DatabaseName, CompanyId " +
                        " FROM         TempDomainMaster " +
                        " WHERE     (Alloted = 1) AND  (CompanyId = '" + Convert.ToInt32(dt.Rows[0]["CompanyId"]) + "') ";

                SqlCommand cmd1 = new SqlCommand(str1, con);
                SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                adp1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                  //  if (Convert.ToBoolean(dt1.Rows[0]["Active"]) == true)
                 //   {
                        string tempurl = "http://" + dt1.Rows[0]["TempDomainName"].ToString() + ".ifilecabinet.com/Default.aspx?uname=" + Session["UN"].ToString() + "&pwd=" + Session["UP"].ToString() + "";
                        Session["username"] = Session["UN"].ToString(); // txtUserId.Text;
                        Session["password"] = Session["UP"].ToString(); //txtPassword.Text;
                        Response.Redirect(tempurl);
                  //  }
                  //  else
                  //  {
                        //    lblmsg.Text = "Your Company Account is Deactive.";
                  //  }
                }
                else
                {
                    //  lblmsg.Text = "Sorry we are unable to redirect at this time. Your company account will setup after 24 Hours.";
                }
            }
        }
    }

}