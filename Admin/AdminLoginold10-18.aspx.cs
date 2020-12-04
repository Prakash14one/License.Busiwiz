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


public partial class AdminLogin : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Clear();
        if (!IsPostBack)
        {
            //if (Session["Login"] != null)
            //{
            //    if (Session["Lonin"].ToString() == null)
            //    {
            //        Response.Redirect("Login.aspx");
            //    }
            //}
            //else
            //{
            //    Response.Redirect("Login.aspx");
            //}

            lblmsg.Visible = false;
        }

    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
        //string str = "  select * from  ClientMaster where  LoginName='"+ txtUser.Text  +"' and LoginPassword ='"+ txtPassword.Text  +"' ";
        //SqlCommand cmd = new SqlCommand(str, con);
        //SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //DataTable dt = new DataTable();
        //adp.Fill(dt);
        //if (dt.Rows.Count > 0)
        //{
        //    Session["Login"] = "admin";
        //    Session["ClientId"] = dt.Rows[0]["ClientMasterId"].ToString();           
        //    Response.Redirect("afterLoginforClient.aspx");       
        //}
        //else
        //{
        //    lblmsg.Visible = true;
        //    lblmsg.Text = "Invalid UserName/Password";
        //}
        if (txtUser.Text == "BusiAdmin" && txtPassword.Text == "NorthernMarch97++")
        {
            Response.Redirect("ClientList.aspx");       
        }
        else
        {

        }
        
    }


   
   
 

}
