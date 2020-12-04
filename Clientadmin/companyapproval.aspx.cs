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
using System.IO;
using System.Text;
using System.Net;
using System.Data.SqlClient;
using System.Globalization;
using System.Net.Mail;
using System.Security.Cryptography;

public partial class companyapproval : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    string FranName; 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["comid"] != null && Request.QueryString["id"] != null)
        {
            if (Request.QueryString["orderpaystatus"] != null && Request.QueryString["paymentinfo"] != null)
            {
                Session["conf_pay_id"] = Request.QueryString["orderpaystatus"];
                Session["paymentinfo"] = Request.QueryString["paymentinfo"];
                con.Open();
                fillplanAndProduct();
                SqlCommand cmdu = new SqlCommand("update Payment_Order_Conform SET   Status ='Approve' Where conf_pay_id=" + Session["conf_pay_id"] + "", con);
                cmdu.ExecuteNonQuery();
                SqlCommand cmd1 = new SqlCommand("update OrderPaymentSatus SET   PaymentStatus ='Approve' Where OrderId=" + Session["conf_pay_id"] + "", con);
                cmd1.ExecuteNonQuery();
                //wait
                //SqlCommand cmd2 = new SqlCommand("update OrderPaymentSatus SET   salesorderid=" + lbl_orderno.Text + " Where OrderId=" + lbl_orderno.Text + "", con);
                //cmd2.ExecuteNonQuery();

                con.Close();
                ViewState["custom"] = "H1";
                Response.Redirect("https://paymentgateway.safestserver.com/PlaceOrderNotify_OtherthanPaypal.aspx?comp_id=" + Session["com_id"] + "&Status=VERIFIED&Amount=" + lbl_amount.Text + "&orderid=" + lbl_orderno.Text + "&paymode=" + Request.QueryString["paymode"] + "");
            }
         
           
           

            //******
            con.Close();
            con.Open();
            string cid = PageMgmt.Decrypted(Request.QueryString["comid"].ToString().Replace(" ","+"));
            try
            {
                if (Request.QueryString["Franchisee"] != null)
                {
                    FranName = Request.QueryString["Franchisee"].ToString();
                }
            }
            catch (Exception ex)
            {

            }
            if (Request.QueryString["comidauto"] != null)

            {
                cid = Request.QueryString["comidauto"].ToString();
            }
            string id = Request.QueryString["id"].ToString();

            string str = "select * from CompanyActivationRequestTbl where emailgeneratecompanyid='" + cid + "' and Id='" + id + "' and companyactive='0'  ";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                string stractivation = " update  CompanyActivationRequestTbl set companyactive='1' where emailgeneratecompanyid='" + cid + "' and Id='" + id + "' ";
                SqlCommand cmdactivation = new SqlCommand(stractivation, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdactivation.ExecuteNonQuery();
                con.Close();

                string strcmp = " update  CompanyMaster set CompanyActiveByAdmin='1' where CompanyLoginId='" + cid + "'  ";
                SqlCommand cmdcmp = new SqlCommand(strcmp, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdcmp.ExecuteNonQuery();
                con.Close();

                sendmailconfiguration(cid);

                Label1.Text = " The company " + cid + " is successfully approved and an email has been sent to the client with configuration link";
            }

           // sendmailconfiguration(cid);
        }

    }
    public void fillplanAndProduct()
    {
        try
        {
                      
            string str = "SELECT    CompanyMaster.CompanyLoginId ,   paymentinfo.paymentinfoid, paymentinfo.PaypalEmailId, paymentinfo.salesorderid, paymentinfo.websitename, paymentinfo.amount, paymentinfo.currencycode, " +
                        " paymentinfo.returnurl, paymentinfo.calcelurl, paymentinfo.notifyurl, paymentinfo.contarycode, paymentinfo.DateTime, paymentinfo.compid, paymentinfo.wid, " +
                      "   paymentinfo.ordermasterid, paymentinfo.ClientMasterId, CompanyMaster.CompanyName, PricePlanMaster.PricePlanName, CompanyMaster.PricePlanId, " +
                     "    PricePlanMaster.PricePlanAmount, CompanyMaster.Websiteurl, OrderMaster.OrderId, OrderMaster.CompanyName AS Order_compName, CompanyMaster.CompanyId " +
                "FROM      paymentinfo INNER JOIN " +
               "          CompanyMaster ON paymentinfo.compid = CompanyMaster.CompanyLoginId INNER JOIN " +
              "           PricePlanMaster ON CompanyMaster.PricePlanId = PricePlanMaster.PricePlanId INNER JOIN " +
             "            OrderMaster ON CompanyMaster.OrderId = OrderMaster.OrderId " +
            "WHERE  (paymentinfo.paymentinfoid = " + Session["paymentinfo"] + ")";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Session["com_id"] = dt.Rows[0]["CompanyId"].ToString();
                Session["com_logid"] = dt.Rows[0]["CompanyLoginId"].ToString();
                lbl_orderno.Text = dt.Rows[0]["OrderId"].ToString();
                lbl_planname.Text = dt.Rows[0]["PricePlanName"].ToString();
                lbl_website.Text = dt.Rows[0]["websitename"].ToString();
                lbl_amount.Text = dt.Rows[0]["PricePlanAmount"].ToString();
            }
            else
            {
                //my page here i can change
                return; 
            }
        }
        catch (Exception ex)
        {
        }
    }

    public void sendmailconfiguration(string cid)
    {
        string str = "select distinct Priceplancategory.CategoryName,CompanyMaster.CompanyLoginId,CompanyMaster.AdminId,CompanyMaster.Password as Pwd,CompanyMaster.Email,CompanyMaster.CompanyName,CompanyMaster.ContactPerson,CompanyMaster.Address,StateMasterTbl.StateName,CountryMaster.CountryName, PortalMasterTbl.*,PricePlanMaster.PricePlanAmount,PricePlanMaster.PricePlanName,PricePlanMaster.PricePlanId,PricePlanMaster.PricePlanDesc from  CompanyMaster inner join PricePlanMaster  on CompanyMaster.PricePlanId=PricePlanMaster.PricePlanId inner join VersionInfoMaster on VersionInfoMaster.VersionInfoId=PricePlanMaster.VersionInfoMasterId inner join ProductMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId inner join ClientMaster on ProductMaster.ClientMasterId=ClientMaster.ClientMasterId inner join Priceplancategory on Priceplancategory.ID=PricePlanMaster.PriceplancatId inner join PortalMasterTbl on PortalMasterTbl.Id=Priceplancategory.PortalId   inner join OrderMaster on CompanyMaster.CompanyLoginId=OrderMaster.CompanyLoginId inner join  OrderPaymentSatus on OrderMaster.OrderId=OrderPaymentSatus.OrderId  inner join StateMasterTbl on StateMasterTbl.StateId=PortalMasterTbl.StateId inner join CountryMaster on StateMasterTbl.CountryId=CountryMaster.CountryId  WHERE(CompanyMaster.CompanyLoginId = '" + cid + "') ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);

        if (Request.QueryString["comidauto"] != null)
        {
            string stepsFive = "http://members.busiwiz.com/Companyconfigureinfo.aspx?comid=" + PageMgmt.Encrypted(dt.Rows[0]["CompanyLoginId"].ToString()) + "&comidauto=" + dt.Rows[0]["CompanyLoginId"].ToString() +"&Franchisee="+ Request.QueryString["Franchisee"] +"&Autocompa=" + dt.Rows[0]["CompanyLoginId"].ToString() + "";
            // stepsFive= "http://license.busiwiz.com/companyapproval.aspx?comid=" + ClsEncDesc.Encrypted(Label7.Text) + "&id=" + dsmaxid.Rows[0]["Id"].ToString() + "comidauto=" + Label7.Text + "" ;
            //" + ClsEncDesc.Encrypted(Label7.Text) + "&id=" + ClsEncDesc.Encrypted(dsmaxid.Rows[0]["Id"].ToString()) + "

            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + stepsFive + "');", true);
        }
        else
        {
            string s = " select  OrderID as OrderID  from OrderMaster where CompanyLoginId='" + cid + "'";
            SqlCommand c = new SqlCommand(s, con);
            SqlDataAdapter a = new SqlDataAdapter(c);
            DataTable d = new DataTable();
            a.Fill(d);
            int orderid = 0;
            if (d.Rows.Count > 0)
            {
                //orderid = Convert.ToInt32(d.Rows[0]["orderid"]);
                ViewState["OrderID"] = Convert.ToInt32(d.Rows[0]["orderid"]);
            }
            string str1 = " select Payment_mode.paymentmode_name as paymentmethod ,Payment_Order_Conform.Payment_date as Date,Payment_Order_Conform.Pay_amount as amountpaid,OrderPaymentSatus.TransactionID " +
                          " FROM OrderPaymentSatus LEFT OUTER JOIN Payment_Order_Conform on Payment_Order_Conform.order_id=OrderPaymentSatus.OrderId " +
                          " LEFT OUTER JOIN Payment_mode on Payment_mode.paymentmode_id=Payment_Order_Conform.payment_mode_id " +
                          " where  OrderPaymentSatus.OrderId=" + ViewState["OrderID"] + "";

            SqlCommand cmd1 = new SqlCommand(str1, con);
            SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            adp1.Fill(dt1);

            string Pay_amount = "";
            string TransactionID = "";
            string Payment_date = "";
            string paymentmode_name = "";
            if (dt1.Rows.Count > 0)
            {
                Pay_amount = "" + dt1.Rows[0]["amountpaid"].ToString() + "";
                TransactionID = "" + dt1.Rows[0]["TransactionID"].ToString() + "";
                paymentmode_name = " " + dt1.Rows[0]["paymentmethod"].ToString() + "";
                Payment_date = "" + dt1.Rows[0]["Date"].ToString() + "";
            }

            if (dt.Rows.Count > 0)
            {
                StringBuilder strplan = new StringBuilder();

                strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
                strplan.Append("<tr><td align=\"left\"> <img src=\"http://license.busiwiz.com/images/" + dt.Rows[0]["LogoPath"].ToString() + "\" \"border=\"0\" Width=\"90\" Height=\"80\" / > </td ></tr>  ");
                strplan.Append("<br></table> ");

                strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
                strplan.Append("<tr><td align=\"left\">Dear " + dt.Rows[0]["CompanyName"].ToString() + ",</td></tr>  ");
                strplan.Append("<tr><td align=\"left\"></td></tr>  ");
                strplan.Append("</table> ");

                //strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
                //strplan.Append("<tr><td align=\"left\">Thank you for buying/subscribing our product.</td></tr> ");
                //strplan.Append("</table> ");

                //							
                strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
                strplan.Append("<tr><td align=\"left\">We are pleased to inform you that your product is ready for configuration. Please note that you must configure your account before you can start using this product. Please click <a href=http://members.busiwiz.com/Companyconfigureinfo.aspx?comid=" + PageMgmt.Encrypted(dt.Rows[0]["CompanyLoginId"].ToString()) + "&Franchisee=" + FranName + " "
                    + " target =_blank >here </a> to begin the configuration process now, or copy and paste the following URL into your internet browser.<br><br>http://members.busiwiz.com/Companyconfigureinfo.aspx?comid=" + PageMgmt.Encrypted(dt.Rows[0]["CompanyLoginId"].ToString()) + "&Franchisee=" + FranName + "<br><br><br></td></tr> ");
                strplan.Append("</table> ");

                //strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
                //strplan.Append("<tr><td align=\"left\">We are pleased to inform you that your product is ready for configuration.</td></tr> ");
                //strplan.Append("</table> ");

                //strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
                //strplan.Append("<tr><td align=\"left\">Please note that you must configure your account before you can start using this product. </td></tr> ");
                //strplan.Append("</table> ");

                //strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
                //strplan.Append("<tr><td align=\"left\">Please click <a href=http://members.busiwiz.com/Companyconfigureinfo.aspx?comid=" + PageMgmt.Encrypted(dt.Rows[0]["CompanyLoginId"].ToString()) + " target =_blank >here </a> to begin the configuration process now, or copy and paste the following URL into your internet browser.<br><br></td></tr> ");
                //strplan.Append("<tr><td align=\"left\">http://members.busiwiz.com/Companyconfigureinfo.aspx?comid=" + PageMgmt.Encrypted(dt.Rows[0]["CompanyLoginId"].ToString()) + "<br><br></td></tr> ");
                //strplan.Append("</table> ");

                //strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
                //strplan.Append("<tr><td align=\"left\">Please note keep the following information in a safe place for future reference.<br><br></td></tr> ");
                //strplan.Append("</table> ");

                strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
                strplan.Append("<tr><td align=\"left\">Please keep the following information in a safe place for future reference.<br><br></td></tr> ");
                strplan.Append("</table> ");
                

                strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; font-weight:bold; color: #000000; font-family: Arial\"> ");
                strplan.Append("<tr><td align=\"left\"><b> Product Details</b> </td></tr>  ");
                // strplan.Append("<tr><td align=\"left\"><br></td></tr>  ");
                strplan.Append("</table> ");

                strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
                strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Product Name: " + dt.Rows[0]["PortalName"].ToString() + "</td></tr>");
                strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Price Plan Name: " + dt.Rows[0]["PricePlanName"].ToString() + "</td></tr>");
                strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Amount Paid: " + Pay_amount + "</td></tr>");
                strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Transaction ID:" + TransactionID + "</td></tr>");
                strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Payment Method: " + paymentmode_name + "</td></tr>");
                strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Transaction Date:" + Payment_date + "</td></tr>");
                strplan.Append("<tr><td align=\"left\"><br></td></tr>  ");
                strplan.Append("</table> ");

                strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; font-weight:bold; color: #000000; font-family: Arial\"> ");
                strplan.Append("<tr><td align=\"left\"><b>Account Information</b></td></tr>  ");
                //strplan.Append("<tr><td align=\"left\"><br></td></tr>  ");
                strplan.Append("</table> ");

                strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
                strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Company ID: " + dt.Rows[0]["CompanyLoginId"].ToString() + "</td></tr>");
                strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">User ID: " + dt.Rows[0]["AdminId"].ToString() + "</td></tr>");
                strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Password: " + dt.Rows[0]["Pwd"].ToString() + "</td></tr>");
                strplan.Append("</table> ");

                strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
                strplan.Append("<tr><td align=\"left\"><br><br></td></tr> ");
                strplan.Append("<tr><td align=\"left\">If you have any questions regarding this information <br> <a href=mailto:" + dt.Rows[0]["Supportteamemailid"].ToString() + " >" + dt.Rows[0]["Supportteamemailid"].ToString() + " </a> </td></tr> ");
                strplan.Append("<tr><td align=\"left\"><br><br></td></tr> ");
                strplan.Append("</table> ");


                //strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
                //strplan.Append("<tr><td align=\"left\">Thank you,</td></tr>  ");
                //strplan.Append("<tr><td align=\"left\">Sincerely ,</td></tr>  ");
                //strplan.Append("<tr><td align=\"left\">Support Team -" + dt.Rows[0]["PortalName"].ToString() + "</td></tr>  ");
                //strplan.Append("</table> ");

                string ext = "";
                string tollfree = "";
                string tollfreeext = "";

                if (Convert.ToString(dt.Rows[0]["Supportteamphonenoext"].ToString()) != "" && Convert.ToString(dt.Rows[0]["Supportteamphonenoext"].ToString()) != null)
                {
                    ext = "ext " + dt.Rows[0]["Supportteamphonenoext"].ToString();
                }

                if (Convert.ToString(dt.Rows[0]["Tollfree"].ToString()) != "" && Convert.ToString(dt.Rows[0]["Tollfree"].ToString()) != null)
                {
                    tollfree = dt.Rows[0]["Tollfree"].ToString();
                }

                if (Convert.ToString(dt.Rows[0]["Tollfree"].ToString()) != "" && Convert.ToString(dt.Rows[0]["Tollfree"].ToString()) != null)
                {
                    tollfreeext = "ext " + dt.Rows[0]["Tollfreeext"].ToString();
                }


                strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
                strplan.Append("<tr><td align=\"left\">Thank you,</td></tr> ");
                //strplan.Append("<tr><td align=\"left\">Sincerely, </td></tr> ");
                //strplan.Append("<tr><td align=\"left\"><br> </td></tr> ");
                strplan.Append("<tr><td align=\"left\">" + dt.Rows[0]["Supportteammanagername"].ToString() + "- Support Manager</td></tr> ");
                strplan.Append("<tr><td align=\"left\">" + dt.Rows[0]["PortalName"].ToString() + " </td></tr> ");
                strplan.Append("<tr><td align=\"left\">" + dt.Rows[0]["Supportteamphoneno"].ToString() + "  " + ext + "  </td></tr> ");
                strplan.Append("<tr><td align=\"left\">" + tollfree + " " + tollfreeext + " </td></tr> ");
                strplan.Append("<tr><td align=\"left\">" + dt.Rows[0]["Portalmarketingwebsitename"].ToString() + " </td></tr> ");
                strplan.Append("<tr><td align=\"left\">" + dt.Rows[0]["Address1"].ToString() + " </td></tr> ");
                strplan.Append("<tr><td align=\"left\">" + dt.Rows[0]["City"].ToString() + " " + dt.Rows[0]["StateName"].ToString() + " " + dt.Rows[0]["CountryName"].ToString() + " " + dt.Rows[0]["Zip"].ToString() + " </td></tr> ");
                strplan.Append("</table> ");

                string bodyformate = "" + strplan + "";


                try
                {

                    MailAddress to = new MailAddress(dt.Rows[0]["Email"].ToString());
                    MailAddress from = new MailAddress(dt.Rows[0]["UserIdtosendmail"].ToString(), dt.Rows[0]["EmailDisplayname"].ToString());
                    MailMessage objEmail = new MailMessage(from, to);
                    objEmail.Subject = " Your product  " + dt.Rows[0]["PortalName"].ToString() + " is ready for configuration now";

                    objEmail.Body = bodyformate.ToString();
                    objEmail.IsBodyHtml = true;
                    objEmail.Priority = MailPriority.High;
                    SmtpClient client = new SmtpClient();
                    client.Credentials = new NetworkCredential(dt.Rows[0]["UserIdtosendmail"].ToString(), dt.Rows[0]["Password"].ToString());
                    client.Host = dt.Rows[0]["Mailserverurl"].ToString();
                    //client.Port = 587;
                    client.Send(objEmail);
                }
                catch
                {
                }
        }

      
        }
    }
  //public void sendmailconfiguration(string cid)
  //  {
  //      string str = "select distinct Priceplancategory.CategoryName,CompanyMaster.CompanyLoginId,CompanyMaster.AdminId,CompanyMaster.Password as Pwd,CompanyMaster.Email,CompanyMaster.CompanyName,CompanyMaster.ContactPerson,CompanyMaster.Address,StateMasterTbl.StateName,CountryMaster.CountryName, PortalMasterTbl.*,PricePlanMaster.PricePlanAmount,PricePlanMaster.PricePlanName,PricePlanMaster.PricePlanId,PricePlanMaster.PricePlanDesc from  CompanyMaster inner join PricePlanMaster  on CompanyMaster.PricePlanId=PricePlanMaster.PricePlanId inner join VersionInfoMaster on VersionInfoMaster.VersionInfoId=PricePlanMaster.VersionInfoMasterId inner join ProductMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId inner join ClientMaster on ProductMaster.ClientMasterId=ClientMaster.ClientMasterId inner join Priceplancategory on Priceplancategory.ID=PricePlanMaster.PriceplancatId inner join PortalMasterTbl on PortalMasterTbl.Id=Priceplancategory.PortalId   inner join OrderMaster on CompanyMaster.CompanyLoginId=OrderMaster.CompanyLoginId inner join  OrderPaymentSatus on OrderMaster.OrderId=OrderPaymentSatus.OrderId  inner join StateMasterTbl on StateMasterTbl.StateId=PortalMasterTbl.StateId inner join CountryMaster on StateMasterTbl.CountryId=CountryMaster.CountryId  WHERE(CompanyMaster.CompanyLoginId = '" + cid + "') ";
  //      SqlCommand cmd = new SqlCommand(str, con);
  //      SqlDataAdapter adp = new SqlDataAdapter(cmd);
  //      DataTable dt = new DataTable();
  //      adp.Fill(dt);
  //      if (dt.Rows.Count > 0)
  //      {
  //      //http://members.busiwiz.com/Companyconfigureinfo.aspx?comid=" + PageMgmt.Encrypted(dt.Rows[0]["CompanyLoginId"].ToString())

  //          if (Request.QueryString["comidauto"] != null)
  //          {
  //              string stepsFive = "http://members.busiwiz.com/Companyconfigureinfo.aspx?comid=" + PageMgmt.Encrypted(dt.Rows[0]["CompanyLoginId"].ToString()) + "&comidauto=" + dt.Rows[0]["CompanyLoginId"].ToString();// "&Autocompa=" + dt.Rows[0]["CompanyLoginId"].ToString() + "";
  //            // stepsFive= "http://license.busiwiz.com/companyapproval.aspx?comid=" + ClsEncDesc.Encrypted(Label7.Text) + "&id=" + dsmaxid.Rows[0]["Id"].ToString() + "comidauto=" + Label7.Text + "" ;
  //                                                                                           //" + ClsEncDesc.Encrypted(Label7.Text) + "&id=" + ClsEncDesc.Encrypted(dsmaxid.Rows[0]["Id"].ToString()) + "
                                                                                                
  //              ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + stepsFive + "');", true);
  //          }
  //          StringBuilder strplan = new StringBuilder();


  //          strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
  //          strplan.Append("<tr><td align=\"left\"> <img src=\"http://license.busiwiz.com/images/" + dt.Rows[0]["LogoPath"].ToString() + "\" \"border=\"0\" Width=\"90\" Height=\"80\" / > </td ></tr>  ");
  //          strplan.Append("<br></table> ");

  //          strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
  //          strplan.Append("<tr><td align=\"left\">Dear " + dt.Rows[0]["ContactPerson"].ToString() + ",</td></tr>  ");
  //          strplan.Append("<tr><td align=\"left\"><br></td></tr>  ");
  //          strplan.Append("</table> ");

  //          strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
  //          strplan.Append("<tr><td align=\"left\">Thank you for buying/subscribing our product.</td></tr> ");
  //          strplan.Append("</table> ");

  //          strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; font-weight:bold; color: #000000; font-family: Arial\"> ");
  //          strplan.Append("<tr><td align=\"left\">Product Details :</td></tr>  ");
  //          strplan.Append("<tr><td align=\"left\"><br></td></tr>  ");
  //          strplan.Append("</table> ");

  //          strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
  //          strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Product Name :</td><td align=\"left\" style=\"width: 80%\">" + dt.Rows[0]["PortalName"].ToString() + "</td></tr>");
  //          strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Plan ID  :</td><td align=\"left\" style=\"width: 80%\">" + dt.Rows[0]["PricePlanId"].ToString() + "</td></tr>");
  //          strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Plan Name :</td><td align=\"left\" style=\"width: 80%\">" + dt.Rows[0]["PricePlanName"].ToString() + "</td></tr>");
  //          strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Plan Description :</td><td align=\"left\" style=\"width: 80%\">" + dt.Rows[0]["PricePlanDesc"].ToString() + "</td></tr>");
  //          strplan.Append("</table> ");

  //          strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; font-weight:bold; color: #000000; font-family: Arial\"> ");
  //          strplan.Append("<tr><td align=\"left\">Login Details :</td></tr>  ");
  //          strplan.Append("<tr><td align=\"left\"><br></td></tr>  ");
  //          strplan.Append("</table> ");

  //          strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
  //          strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Company ID :</td><td align=\"left\" style=\"width: 80%\">" + dt.Rows[0]["CompanyLoginId"].ToString() + "</td></tr>");
  //          strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">User ID  :</td><td align=\"left\" style=\"width: 80%\">" + dt.Rows[0]["AdminId"].ToString() + "</td></tr>");
  //          strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Password :</td><td align=\"left\" style=\"width: 80%\">" + dt.Rows[0]["Pwd"].ToString() + "</td></tr>");            
  //          strplan.Append("</table> ");

  //          strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
  //          strplan.Append("<tr><td align=\"left\">We are pleased to inform you that your product is ready for configuratgion.</td></tr> ");
  //          strplan.Append("</table> ");

  //          strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
  //          strplan.Append("<tr><td align=\"left\">Please note that you have to configure your account before you can start using this product.</td></tr> ");
  //          strplan.Append("</table> ");

  //          strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
  //          strplan.Append("<tr><td align=\"left\">Please click <a href=http://members.busiwiz.com/Companyconfigureinfo.aspx?comid=" + PageMgmt.Encrypted(dt.Rows[0]["CompanyLoginId"].ToString()) + " target=_blank >here </a> to start your configuration now.if the the link does not open please copy and paste the following link: </td></tr> ");
  //          strplan.Append("<tr><td align=\"left\">http://members.busiwiz.com/Companyconfigureinfo.aspx?comid=" + PageMgmt.Encrypted(dt.Rows[0]["CompanyLoginId"].ToString()) + "</td></tr> ");
  //          strplan.Append("</table> ");

  //          strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
  //          strplan.Append("<tr><td align=\"left\"><br></td></tr> ");
  //          strplan.Append("<tr><td align=\"left\">If you have any questions regarding this information, please contact us at <a href=mailto:" + dt.Rows[0]["Supportteamemailid"].ToString() + " >" + dt.Rows[0]["Supportteamemailid"].ToString() + " </a> </td></tr> ");
  //          strplan.Append("<tr><td align=\"left\"><br></td></tr> ");
  //          strplan.Append("</table> ");


  //          strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
  //          strplan.Append("<tr><td align=\"left\">Thanking you </td></tr>  ");
  //          strplan.Append("<tr><td align=\"left\">Sincerely ,</td></tr>  ");
  //          strplan.Append("<tr><td align=\"left\">Support Team -" + dt.Rows[0]["PortalName"].ToString() + "</td></tr>  ");
  //          strplan.Append("</table> ");

  //          string ext = "";
  //          string tollfree = "";
  //          string tollfreeext = "";

  //          if (Convert.ToString(dt.Rows[0]["Supportteamphonenoext"].ToString()) != "" && Convert.ToString(dt.Rows[0]["Supportteamphonenoext"].ToString()) != null)
  //          {
  //              ext = "ext " + dt.Rows[0]["Supportteamphonenoext"].ToString();
  //          }

  //          if (Convert.ToString(dt.Rows[0]["Tollfree"].ToString()) != "" && Convert.ToString(dt.Rows[0]["Tollfree"].ToString()) != null)
  //          {
  //              tollfree = dt.Rows[0]["Tollfree"].ToString();
  //          }

  //          if (Convert.ToString(dt.Rows[0]["Tollfree"].ToString()) != "" && Convert.ToString(dt.Rows[0]["Tollfree"].ToString()) != null)
  //          {
  //              tollfreeext = "ext " + dt.Rows[0]["Tollfreeext"].ToString();
  //          }


  //          strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
  //          strplan.Append("<tr><td align=\"left\">Thanking you </td></tr> ");
  //          strplan.Append("<tr><td align=\"left\">Sincerely, </td></tr> ");
  //          strplan.Append("<tr><td align=\"left\"><br> </td></tr> ");

  //          strplan.Append("<tr><td align=\"left\">" + dt.Rows[0]["Supportteammanagername"].ToString() + "- Support Manager</td></tr> ");
  //          strplan.Append("<tr><td align=\"left\">" + dt.Rows[0]["PortalName"].ToString() + " </td></tr> ");
  //          strplan.Append("<tr><td align=\"left\">" + dt.Rows[0]["Supportteamphoneno"].ToString() + "  " + ext + "  </td></tr> ");
  //          strplan.Append("<tr><td align=\"left\">" + tollfree + " " + tollfreeext + " </td></tr> ");
  //          strplan.Append("<tr><td align=\"left\">" + dt.Rows[0]["Portalmarketingwebsitename"].ToString() + " </td></tr> ");
  //          strplan.Append("<tr><td align=\"left\">" + dt.Rows[0]["Address1"].ToString() + " </td></tr> ");
  //          strplan.Append("<tr><td align=\"left\">" + dt.Rows[0]["City"].ToString() + " " + dt.Rows[0]["StateName"].ToString() + " " + dt.Rows[0]["CountryName"].ToString() + " " + dt.Rows[0]["Zip"].ToString() + " </td></tr> ");
  //          strplan.Append("</table> ");

  //          string bodyformate = "" + strplan + "";


  //          try
  //          {

  //              MailAddress to = new MailAddress(dt.Rows[0]["Email"].ToString());
  //              MailAddress from = new MailAddress(dt.Rows[0]["UserIdtosendmail"].ToString(), dt.Rows[0]["EmailDisplayname"].ToString());
  //              MailMessage objEmail = new MailMessage(from, to);
  //              objEmail.Subject = " Your product  " + dt.Rows[0]["PortalName"].ToString() + " is ready for configuration now";

  //              objEmail.Body = bodyformate.ToString();
  //              objEmail.IsBodyHtml = true;
  //              objEmail.Priority = MailPriority.High;
  //              SmtpClient client = new SmtpClient();
  //              client.Credentials = new NetworkCredential(dt.Rows[0]["UserIdtosendmail"].ToString(), dt.Rows[0]["Password"].ToString());
  //              client.Host = dt.Rows[0]["Mailserverurl"].ToString();
  //              //client.Port = 587;
  //              client.Send(objEmail);
  //          }
  //          catch
  //          {
  //          }
  //      }
  //  }  
}
