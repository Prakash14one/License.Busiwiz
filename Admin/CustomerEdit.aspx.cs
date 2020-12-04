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

public partial class CustomerEditAdmin : System.Web.UI.Page
{
  //  SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ifilecabinateConnectionString"].ConnectionString);
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string i = Request.QueryString["CompanyId"];
             
            Session["id"] = i.ToString();
            txtdate.Text = System.DateTime.Now.ToShortDateString();

            string str = " select * from Companymaster where Companymaster.CompanyId= " + i.ToString();
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds1 = new DataSet();
        adp.Fill(ds1);
        if (ds1.Tables[0].Rows.Count > 0)
        {
            hdnProductId.Value = ds1.Tables[0].Rows[0]["ProductId"].ToString();
            ViewState["OrderId"] = ds1.Tables[0].Rows[0]["OrderId"].ToString();
        }

        DataSet ds = new DataSet();
            ds = fillplan();
            ddlsubscriptionplan.DataSource = ds;
            ddlsubscriptionplan.DataTextField = "PricePlanName";
            ddlsubscriptionplan.DataValueField = "PricePlanId";
            ddlsubscriptionplan.DataBind();
            loaddate(Convert.ToInt32(i));
        }
        lblmsg.Visible = false;

    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string str = "";
       // if (RadioButtonList1.SelectedIndex == 0)
     //   {
            str = "update   CompanyMaster set " +
                         " CompanyName='" + txtcompanyname.Text + "', ContactPerson='" + txtcontactprsn.Text + "', ContactPersonDesignation='" + txtcontactprsndsg.Text + "', CompanyWebsite='', date='" + System.DateTime.Now.Date + "', PlanId='" + Convert.ToInt32(ddlsubscriptionplan.SelectedValue) + "', Address ='" + txtadd.Text + "', " +
                         " Email='" + txtemail.Text + "', pincode='', phone='" + Convert.ToInt32(txtphn.Text) + "', fax='" + Convert.ToInt32(txtfax.Text) + "', CompanyLoginId='" + lblCompanyId.Text + "',  " +
                         " active='" + RadioButtonList1.SelectedItem.Value.ToString() + "'" +
                         "where CompanyId = " + Session["id"].ToString();
                       
       // }
        //else
        //{
        //    str = "INSERT INTO CompanyMaster " +
        //                 " (CompanyName, ContactPerson, ContactPersonDesignation, CompanyWebsite, date, PlanId, Address, " +
        //                 " Email, pincode, phone, fax, CompanyLoginId, AdminId,  " +
        //                 " Password, redirect, active, deactiveReason,HostId,OrderId,StateId,ProductId) " +
        //               " VALUES     ('" + txtcompanyname.Text + "','" + txtcontactprsn.Text + "','" + txtcontactprsndsg.Text + "',''," +
        //               " '" + System.DateTime.Now.Date + "','" + Convert.ToInt32(ddlsubscriptionplan.SelectedValue) + "','" + txtadd.Text + "'," +
        //               " '" + txtemail.Text + "','','" + Convert.ToInt32(txtphn.Text) + "','" + Convert.ToInt32(txtfax.Text) + "', " +
        //               " '" + txtcompanyid.Text + "' ,'" + txtadminuserid.Text + "','" + txtpassword.Text + "','" + txtdomain.Text + "','0','',0,'" + OrderId + "','" + ddlstate.SelectedItem.Value.ToString() + "'," + hdnProductId.Value.ToString() + ") ";
        //}
            SqlCommand cmd = new SqlCommand(str, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            str = "update OrderMaster set CompanyName='" + txtcompanyname.Text + "',ContactPerson='" + txtcontactprsn.Text + "',ContactPersonDesignation='" + txtcontactprsndsg.Text + "',Address='" + txtadd.Text + "', Email='" + txtemail.Text + "',phone='" + txtphn.Text + "',fax='" + txtfax.Text + "',Status='" + RadioButtonList1.SelectedValue + "' where OrderId='" + Convert.ToInt32(ViewState["OrderId"].ToString()) + "'";

              cmd = new SqlCommand(str, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            lblmsg.Visible = true;
            //ViewState["CompanyId"] 
            string dbnm = lblRedirectUrl.Text + "ifilecabinet";
            str = "update TempDomainMaster set TempDomainName='" + lblRedirectUrl.Text + "',DatabaseName='" + dbnm + "' where CompanyId='"+ ViewState["CompanyId"].ToString() +"'";
            cmd = new SqlCommand(str, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            lblmsg.Visible = true;

            SqlConnection conLic = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);

           String  str11 = "update  LicenseMaster  set LicenseDate= '" + DateTime.Now.ToShortDateString() + "',DatabaseName='" + dbnm + "'  where CompanyId='" + ViewState["CompanyId"].ToString() + "'";
            cmd = new SqlCommand(str11, conLic);
            conLic.Open();
            cmd.ExecuteNonQuery();
            conLic.Close();
            //Response .Write ( " Order Updated successfully.");
          //  sendmail(txtemail.Text );
            lblmessage.Text = " Order Updated successfully.";
            //Response.Redirect("afterloginforClient.aspx");
           // btnreset_Click(sender, e);
    }
    public void sendmail(string To)
    {
        //string body = "" + HeadingTable + "<br><br> Dear " + txtlastname.Text + "&nbsp;" + txtfirstname.Text + " ,<br><br>" + welcometext.ToString() + " <br>" + AccountInfo.ToString() + "<br><br><strong><span style=\"color: #996600\"> " + ViewState["sitename"] + " Team</span></strong>";
        string logo = "<img src=\"http://www.ifilecabinet.com/images/logo.jpg\" \"border=\"0\"  />";
        string body = logo.ToString() + "<br><br> Thank you for registering with <a href=\"http://www.ifilecabinet.com \">Ifilecabinet.com</a><br><br> Your login info is as follows:<br>&nbsp; Company ID:" + lblCompanyId.Text  + " <br>&nbsp; Admin UserID: " +  lblAUserId.Text  + " <br>&nbsp; Password: " + lblAPassword.Text  + "<br><br><br><br>" +
        "Please login to Members.Busiwiz.com" +
        "Sincerely,<br>Ifilecabinet Team ";
        string bodytext = "Thank you for registering with <a href=\"http://www.ifilecabinet.com \">Ifilecabinet.com</a><br><br> Your login info is as follows:<br>&nbsp; Company ID:" + lblCompanyId.Text + " <br>&nbsp; Admin UserID: " + lblAUserId.Text + " <br>&nbsp; Password: " + lblAPassword.Text + "<br><br><br><br>Sincerely,<br>Ifilecabinet Team ";
        MailAddress to = new MailAddress(To);
        MailAddress from = new MailAddress("admin@ifilecabinet.com");
        MailMessage objEmail = new MailMessage(from, to);
        objEmail.Subject = "Welcome to Ifilecabinet.com - Registration";
        objEmail.Body = body.ToString();
        objEmail.IsBodyHtml = true;
        objEmail.Priority = MailPriority.High;
        Session["BodyText"] = bodytext;
        SmtpClient client = new SmtpClient();
        client.Credentials = new NetworkCredential("admin@ifilecabinet.com", "26De1966");
        client.Host = "mail.ifilecabinet.com";
        client.Send(objEmail);
    }
    //public DataSet fillplan()
    //{
    //    string str = "SELECT     PlanId, PlanName + ' - ' + CONVERT(nvarchar, PricePerMonth) +'$'  as planname " +
    //        " FROM         PricePlanMaster";
    //    SqlCommand cmd = new SqlCommand(str, con);
    //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
    //    DataSet ds = new DataSet();
    //    adp.Fill(ds);
    //    return ds;
    //}

    public DataSet fillplan()
    {
        string str = "SELECT     PricePlanMaster.*, ProductMaster.* FROM         PricePlanMaster LEFT OUTER JOIN " +
                     " ProductMaster ON PricePlanMaster.ProductId = ProductMaster.ProductId where ProductMaster.Productid= " + hdnProductId.Value.ToString();
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;


    }
    protected void btnreset_Click(object sender, EventArgs e)
    {
        lblRedirectUrl.Text = "";
      //  txtpin.Text = "";
        txtphn.Text = "";
        //txtpassword.Text = "";
        txtfax.Text = "";
        txtemail.Text = "";
        txtcontactprsndsg.Text = "";
        txtcontactprsn.Text = "";
        txtcompanyname.Text = "";
      //  txtcompanyid.Text = "";
        //txtcnfrmpassword.Text = "";
        //txtadminloginid.Text = "";
        txtadd.Text = "";
       // txtweb.Text = "";

    }
    //public int checkCompId()
    //{
    //    int i = 0;
    //    string str = "SELECT  * FROM   CompanyMaster WHERE     (CompanyLoginId = '" + txtcompanyid.Text + "') or (redirect= '"+ txtredirecturl.Text +"')";
    //    SqlCommand cmd = new SqlCommand(str, con);
    //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
    //    DataSet ds = new DataSet();
    //    adp.Fill(ds);
    //    if(ds.Tables[0].Rows.Count >0)
    //    {
    //        lblmsg.Visible=true;
    //        lblmsg.Text ="Sorry This Company ID or Redirect Url is already exist. Please try another.";
    //        i=1;
    //    }
    //    return i;
    //}
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        checkRadioButtons();
    }
    public void checkRadioButtons()
    {
        if (RadioButtonList1.SelectedIndex == 1)
        {
           // lblDeactive.Visible = true;
           // txtDeactiveReason.Visible = true;
        }
        else
        {
          //  lblDeactive.Visible = false;
           // txtDeactiveReason.Visible = false;

        }
    }

    public DataTable getCompanyMaster(int id)
    {
        //string str = "SELECT     CompanyMaster.CompanyId, CompanyMaster.CompanyName, CompanyMaster.ContactPerson, CompanyMaster.ContactPersonDesignation,  " +
        //              " CompanyMaster.CompanyWebsite, CompanyMaster.date, CompanyMaster.PlanId, CompanyMaster.Address, CompanyMaster.Email, CompanyMaster.pincode,  " +
        //              " CompanyMaster.phone, CompanyMaster.fax, CompanyMaster.CompanyLoginId, CompanyMaster.AdminId, CompanyMaster.Password, CompanyMaster.redirect  as url, " +
        //              " CompanyMaster.active, CompanyMaster.deactiveReason, PricePlanMaster.PlanName, PricePlanMaster.MaxNoOfUser, PricePlanMaster.MaxStorage, " +
        //              " PricePlanMaster.PricePerMonth " +
        //                " FROM         CompanyMaster INNER JOIN " +
        //             " PricePlanMaster ON CompanyMaster.PlanId = PricePlanMaster.PlanId " +
        //             " where CompanyId='" + id +"'";
        //string str = "  SELECT     CompanyMaster.CompanyId, CompanyMaster.CompanyName, CompanyMaster.ContactPerson, CompanyMaster.ContactPersonDesignation, " +
        //              " CompanyMaster.CompanyWebsite, CompanyMaster.date, CompanyMaster.PlanId, CompanyMaster.Address, CompanyMaster.Email  , " +
        //              "  CompanyMaster.pincode, CompanyMaster.phone, CompanyMaster.fax  , CompanyMaster.CompanyLoginId, CompanyMaster.AdminId, " +
        //               " CompanyMaster.Password, CompanyMaster.redirect  , CASE CompanyMaster.active WHEN 1 THEN 'Active' ELSE 'Deactive' END AS active, " +
        //               " CompanyMaster.deactiveReason, PricePlanView.* ,CompanyMaster.active as active1  FROM         CompanyMaster LEFT OUTER JOIN " +
        //                " PricePlanView ON CompanyMaster.PlanId = PricePlanView.PricePlanDetailId " +
        //                 " where CompanyId='" + id + "'";
//        string str = "  SELECT     OrderMaster.OrderId  , OrderMaster.CompanyName, OrderMaster.ContactPerson, OrderMaster.ContactPersonDesignation, OrderMaster.Address, " +
//                    " OrderMaster.Email, OrderMaster.phone, OrderMaster.fax, OrderMaster.PlanId, OrderMaster.AdminId, OrderMaster.Password, OrderMaster.Domain, " +
//                    " OrderMaster.HostId, PricePlanView.PricePlanDetailId, PricePlanView.PricePlanId, PricePlanView.PricePlanTypeId, PricePlanView.Amount, " +
//                    " PricePlanView.PlanTypeName, PricePlanView.PricePlanName, PricePlanView.PlanName, PricePlanView.PlanName1, CompanyMaster.CompanyId, " +
//                    " PricePlanView.PlanDetail,   CompanyMaster.CompanyWebsite, CompanyMaster.date ,           " +
//                     " CompanyMaster.CompanyLoginId,  CompanyMaster.redirect, CompanyMaster.active, " +
//                     " CompanyMaster.deactiveReason, CompanyMaster.PlanActive,   CompanyMaster.StateId , " +
//                    "  OrderPaymentSatus.OrderPaymentId , OrderPaymentSatus.PaymentStatus, OrderPaymentSatus.TransactionID, " +
//                    "  CASE CompanyMaster.HostId WHEN 1 THEN 'User Server' ELSE 'IfileCabinet Server' END AS HostName, TempDomainMaster.TempDomainId, " +
//                    "  TempDomainMaster.TempDomainName, OrderMaster.Status , TempDomainMaster.DatabaseName " +
//" FROM         TempDomainMaster RIGHT OUTER JOIN " +
// "                    CompanyMaster ON TempDomainMaster.CompanyId = CompanyMaster.CompanyId RIGHT OUTER JOIN " +
//  "                   OrderMaster LEFT OUTER JOIN " +
//   "                  OrderPaymentSatus ON OrderMaster.OrderId = OrderPaymentSatus.OrderId LEFT OUTER JOIN " +
//    "                 PricePlanView ON OrderMaster.PlanId = PricePlanView.PricePlanDetailId ON CompanyMaster.OrderId = OrderMaster.OrderId " +
//" WHERE     OrderMaster.OrderId = '" + id + "'" ;  
        //string str = "SELECT     OrderMaster.OrderId, CompanyMaster.CompanyName, CompanyMaster.ContactPerson, OrderMaster.ContactPersonDesignation, CompanyMaster.Address, CompanyMaster.Email, " +
        //             " OrderMaster.phone, OrderMaster.fax, OrderMaster.PlanId, OrderMaster.AdminId, OrderMaster.Password, OrderMaster.Domain, OrderMaster.HostId, " +
        //           " CompanyMaster.CompanyId ,  " +
        //            "  CompanyMaster.CompanyWebsite, CompanyMaster.date, CompanyMaster.CompanyLoginId, CompanyMaster.redirect, CompanyMaster.active, " +
        //            " CompanyMaster.deactiveReason, CompanyMaster.PlanActive, CompanyMaster.StateId, OrderPaymentSatus.OrderPaymentId, OrderPaymentSatus.PaymentStatus, " +
        //            " OrderPaymentSatus.TransactionID, CASE CompanyMaster.HostId WHEN 1 THEN 'User Server' ELSE 'busiwiz's Server' END AS HostName, " +
        //            " TempDomainMaster.TempDomainId, TempDomainMaster.TempDomainName, OrderMaster.Status, TempDomainMaster.DatabaseName, " +
        //            " PricePlanMaster.PricePlanDesc FROM         PricePlanMaster RIGHT OUTER JOIN " +
        //             " CompanyMaster ON PricePlanMaster.ProductId = CompanyMaster.ProductId LEFT OUTER JOIN " +
        //             " TempDomainMaster ON CompanyMaster.CompanyId = TempDomainMaster.CompanyId RIGHT OUTER JOIN " +
        //             " OrderMaster LEFT OUTER JOIN OrderPaymentSatus ON OrderMaster.OrderId = OrderPaymentSatus.OrderId ON CompanyMaster.OrderId = OrderMaster.OrderId" +
        //             " WHERE       OrderMaster.OrderId = '" + id + "'";
        string str = "SELECT     OrderMaster.OrderId, CompanyMaster.CompanyName, CompanyMaster.ContactPerson, CompanyMaster.ContactPersonDesignation, CompanyMaster.Address, " +
       " CompanyMaster.Email, OrderMaster.phone, OrderMaster.fax, OrderMaster.PlanId, OrderMaster.AdminId, OrderMaster.Password, OrderMaster.Domain, " +
        " OrderMaster.HostId, CompanyMaster.CompanyId, CompanyMaster.CompanyWebsite, CompanyMaster.date, CompanyMaster.CompanyLoginId, " +
         " CompanyMaster.redirect, CompanyMaster.active, CompanyMaster.deactiveReason, CompanyMaster.PlanActive, CompanyMaster.StateId, " +
          " OrderPaymentSatus.OrderPaymentId, OrderPaymentSatus.PaymentStatus, OrderPaymentSatus.TransactionID, " +
           "  CASE CompanyMaster.HostId WHEN 1 THEN 'User Server' ELSE 'busiwiz Server' END AS HostName, TempDomainMaster.TempDomainId, " +
            " TempDomainMaster.TempDomainName, OrderMaster.Status, TempDomainMaster.DatabaseName, PricePlanMaster.PricePlanDesc, ProductMaster.*" +
" FROM PricePlanMaster RIGHT OUTER JOIN ProductMaster ON PricePlanMaster.ProductId = ProductMaster.ProductId RIGHT OUTER JOIN " +
" CompanyMaster ON ProductMaster.ProductId = CompanyMaster.ProductId AND PricePlanMaster.ProductId = CompanyMaster.ProductId LEFT OUTER JOIN " +
"  TempDomainMaster ON CompanyMaster.CompanyId = TempDomainMaster.CompanyId RIGHT OUTER JOIN " +
 " OrderMaster LEFT OUTER JOIN OrderPaymentSatus ON OrderMaster.OrderId = OrderPaymentSatus.OrderId ON CompanyMaster.OrderId = OrderMaster.OrderId " +
        " WHERE     (CompanyMaster.CompanyId = '" + Request.QueryString["CompanyId"].ToString() + "')";

        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        return dt;
    }
    public void loaddate(int id)
    {
        DataTable dt = new DataTable();
        dt = getCompanyMaster(id);
        if (dt.Rows.Count > 0)
        {
            ViewState["CompanyId"] = dt.Rows[0]["CompanyId"].ToString();
            txtcompanyname.Text = dt.Rows[0]["CompanyName"].ToString();
            txtadd.Text = dt.Rows[0]["Address"].ToString();
            lblCompanyId.Text  = dt.Rows[0]["CompanyLoginId"].ToString();
            lblAUserId.Text = dt.Rows[0]["AdminId"].ToString();
            lblAPassword.Text = dt.Rows[0]["Password"].ToString(); 
            txtcontactprsn.Text = dt.Rows[0]["ContactPerson"].ToString();
            txtcontactprsndsg.Text = dt.Rows[0]["ContactPersonDesignation"].ToString();
            txtdate.Text = dt.Rows[0]["date"].ToString();
            txtemail.Text = dt.Rows[0]["Email"].ToString();
            txtfax.Text = dt.Rows[0]["fax"].ToString();
            txtphn.Text = dt.Rows[0]["phone"].ToString();
            //txtpin.Text = dt.Rows[0]["pincode"].ToString();
            hdnProductId.Value = dt.Rows[0]["ProductId"].ToString();
            lblRedirectUrl.Text = dt.Rows[0]["Redirect"].ToString();
          //  ViewState["Redirect"]
            //txtweb.Text = dt.Rows[0]["CompanyWebsite"].ToString();
            ddlsubscriptionplan.SelectedIndex  = ddlsubscriptionplan.Items.IndexOf(ddlsubscriptionplan.Items.FindByValue(dt.Rows[0]["PlanId"].ToString()));
            ddlsubscriptionplan.Enabled = false;
            RadioButtonList1.SelectedIndex = RadioButtonList1.Items.IndexOf(RadioButtonList1.Items.FindByValue(Convert.ToBoolean(dt.Rows[0]["status"].ToString()).ToString()));
   
         //   RadioButtonList1.SelectedValue =dt.Rows[0]["active"].ToString();
            checkRadioButtons();
        }
    }
}
