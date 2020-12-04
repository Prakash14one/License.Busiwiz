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
public partial class busiwizlicensekeygeneration : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    SqlConnection dynconn;
    decimal totabala = 0;
    decimal netdo = 0;
    string FranCompanyname;


    public StringBuilder strplan1 = new StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {
        string mc_gross = "";
        string txn_id = "";

        string Orderid = "";
        if (Request.QueryString["Franchisee"] != "")
        {
            FranCompanyname = Request.QueryString["Franchisee"];
        }
        else
        {
            FranCompanyname = "jobcenter";
        }

        if (Request.QueryString["orz"] != null)
        {
            ViewState["saleorderold"] = Request.QueryString["orz"].ToString();
            ViewState["mc_gross"] = "0";
            ViewState["paymentstatus"] = "NA";
            ViewState["txtid"] = "NA";
            filldatas(Request.QueryString["orz"].ToString(), ViewState["paymentstatus"].ToString(), ViewState["txtid"].ToString());

            if (Request.QueryString["cid"] != null)
            {

                Response.Redirect("https://paymentgateway.safestserver.com/Thankyou.aspx?cid=" + Request.QueryString["cid"] + "");
            }
            else
            {
                Response.Redirect("https://paymentgateway.safestserver.com/Thankyou.aspx?id=" + ViewState["saleorderold"] + "");
            }
        }
        else
        {
            if (Request.QueryString["txn_id"] != null)
            {
                txn_id = Request.QueryString["txn_id"];
                ViewState["txtid"] = txn_id;
                mc_gross = Request.QueryString["mc_gross"];
                ViewState["mc_gross"] = mc_gross;
                string payment_status = Request.QueryString["payment_status"];
                ViewState["paymentstatus"] = payment_status;
                string payer_email = Request.QueryString["payer_email"];

                Orderid = Request.QueryString["item_number"];
                ViewState["saleorderold"] = Orderid.ToString();
                // insertStatus(Convert.ToInt32(Orderid), payment_status.ToString(), txn_id.ToString(), "before in Complete");

                if (payment_status == "Completed")
                {
                    filldatas(Orderid, payment_status, txn_id);
                }
            }
        }
    }
    public void filldatas(string Orderid, string payment_status, string txn_id)
    {
        string Key = "";
        string HashKey = "";
        Key = CreateLicenceKey(out HashKey);

        string Encrrptionkey = "";
        Encrrptionkey = CreateEncreptionKey(out HashKey);

        ViewState["licensekey"] = Key;
        double ii = Convert.ToDouble(ViewState["mc_gross"]);

        string stri = "select * from paymentinfo where ClientMasterId='" + ViewState["saleorderold"] + "' and amount='" + ii + "' and notifyurl IS NULL  ";

        SqlCommand cmd1 = new SqlCommand(stri, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        adp1.Fill(dt1);
        if (dt1.Rows.Count > 0)
        {
            //Renue
            insertStatusclient(Convert.ToInt32(Orderid), payment_status.ToString(), txn_id.ToString(), ViewState["mc_gross"].ToString());
            string strpriceid = "SELECT ClientPricePlanId from ClientMaster WHERE     (ClientMasterId = '" + ViewState["saleorderold"].ToString() + "')";
            SqlCommand cmdprice = new SqlCommand(strpriceid, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            object priceid = cmdprice.ExecuteScalar();
            ViewState["clientpriceplanid"] = priceid;
            con.Close();
            string period = "SELECT * from ClientPricePlanMaster WHERE (ClientPricePlanId = '" + ViewState["clientpriceplanid"] + "')";
            SqlDataAdapter adperiod = new SqlDataAdapter(period, con);
            DataTable dtperiod = new DataTable();
            adperiod.Fill(dtperiod);

            if (dtperiod.Rows.Count > 0)
            {
                string month = "SELECT  DATEDIFF(day,'" + dtperiod.Rows[0]["StartDate"] + "','" + dtperiod.Rows[0]["EndDate"] + "')";
                SqlCommand cmdmonth = new SqlCommand(month, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                object periodmont = cmdmonth.ExecuteScalar();
                ViewState["month"] = periodmont;
                con.Close();

            }
            string str11 = "INSERT INTO LicenseMaster " +
                           " (SiteMasterId, LicenseKey, HashKey, LIcenseDate,Clientid,LicensePeriod) " +
                           " VALUES     ('1','" + Key + "','" + HashKey + "','" + DateTime.Now.ToShortDateString() + "','" + dt1.Rows[0]["ClientMasterId"] + "'," + ViewState["month"] + " )";
            SqlCommand cmd = new SqlCommand(str11, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();

            sendmailclient(dt1.Rows[0]["PaypalEmailId"].ToString());

        }
        else
        {
            //FreePlan or new plane
            int flag = 0;
            string order = "select * from paymentinfo where ordermasterid='" + ViewState["saleorderold"] + "' and  ClientMasterId IS NULL";
            SqlCommand cmdor = new SqlCommand(order, con);
            SqlDataAdapter adpor = new SqlDataAdapter(cmdor);
            DataTable dtorder = new DataTable();
            adpor.Fill(dtorder);
           

            if (dtorder.Rows.Count > 0)
            {
                Session["CompId"] = dtorder.Rows[0]["compid"].ToString();
                string strpriceid = "SELECT PlanId from OrderMaster WHERE(OrderId = '" + ViewState["saleorderold"].ToString() + "')";
                SqlCommand cmdprice = new SqlCommand(strpriceid, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                object priceid = cmdprice.ExecuteScalar();
                ViewState["priceplanid"] = priceid;
                con.Close();
                string period = "SELECT * from PricePlanMaster WHERE (PricePlanId = '" + ViewState["priceplanid"] + "')";
                SqlDataAdapter adperiod = new SqlDataAdapter(period, con);
                DataTable dtperiod = new DataTable();
                adperiod.Fill(dtperiod);

                if (dtperiod.Rows.Count > 0)
                {

                    ViewState["month"] = dtperiod.Rows[0]["DurationMonth"];

                }
                string update = "update CompanyMaster set PricePlanId='" + ViewState["priceplanid"] + "', active='True',OrderId='" + ViewState["saleorderold"] + "',Encryptkeycomp='" + Encrrptionkey + "' where CompanyLoginId='" + Session["CompId"] + "'";
                SqlCommand upcom = new SqlCommand(update, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                upcom.ExecuteNonQuery();
                con.Close();

                string comid = "SELECT OrderMaster.OrderId,OrderMaster.OrderType,CompanyMaster.CompanyId,CompanyMaster.CompanyLoginId,CompanyMaster.PricePlanId from CompanyMaster inner join OrderMaster on OrderMaster.CompanyLoginId=CompanyMaster.CompanyLoginId  WHERE (CompanyMaster.CompanyLoginId = '" + Session["CompId"].ToString() + "') and (OrderMaster.OrderId = '" + ViewState["saleorderold"].ToString() + "')";
                SqlDataAdapter adc = new SqlDataAdapter(comid, con);
                DataTable dtc = new DataTable();
                adc.Fill(dtc);

                if (dtc.Rows.Count > 0)
                {
                    string selli = "SELECT * from LicenseMaster where CompanyId='" + dtc.Rows[0]["CompanyId"] + "'";
                    SqlDataAdapter adlic = new SqlDataAdapter(selli, con);
                    DataTable dtlic = new DataTable();
                    adlic.Fill(dtlic);

                    if (dtlic.Rows.Count <= 0)
                    {
                        string str11 = "INSERT INTO LicenseMaster " +
                                       " (SiteMasterId, CompanyId, LicenseKey, HashKey, LIcenseDate,LicensePeriod) " +
                                       " VALUES ('1','" + dtc.Rows[0]["CompanyId"] + "','" + Key + "','" + HashKey + "','" + DateTime.Now.ToShortDateString() + "'," + ViewState["month"] + ")";
                        SqlCommand cmd = new SqlCommand(str11, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    else
                    {

                        if (Convert.ToString(dtc.Rows[0]["OrderType"]) == "Renue")
                        {
                            DateTime lastredate = Convert.ToDateTime(dtlic.Rows[0]["LIcenseDate"]).AddDays(Convert.ToInt32(dtlic.Rows[0]["LicensePeriod"]));
                            int remainrday = lastredate.Subtract(DateTime.Now).Days;
                            if (remainrday > 0)
                            {
                                ViewState["month"] = (Convert.ToInt32(ViewState["month"]) + remainrday).ToString();
                            }

                        }
                        flag = 2;
                        string str11 = "Update LicenseMaster set LIcenseDate='" + DateTime.Now.ToShortDateString() + "',LicensePeriod=" + ViewState["month"] + " where CompanyId='" + dtc.Rows[0]["CompanyId"] + "'";
                        SqlCommand cmd = new SqlCommand(str11, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmd.ExecuteNonQuery();
                        con.Close();
                        string str11or = "Update OrderMaster set Status='1' where  OrderId = '" + ViewState["saleorderold"].ToString() + "'";
                        SqlCommand cmdord = new SqlCommand(str11or, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmdord.ExecuteNonQuery();
                        con.Close();
                        dyconnlmaster();
                        string PricePlanIddd = encryptstrring(dtc.Rows[0]["PricePlanId"].ToString());
                        string compiddd = encryptstrring(dtc.Rows[0]["CompanyId"].ToString());

                        string str11orlm = "Update LMaster set LUPD='" + PricePlanIddd + "',AT='True' where  CID = '" + compiddd + "'";
                        SqlCommand cmdordlm = new SqlCommand(str11orlm, dynconn);

                        dynconn.Open();
                        cmdordlm.ExecuteNonQuery();
                        dynconn.Close();

                    }

                    string chkaper = "select * FROM PricePlanMaster WHERE PricePlanId='" + dtc.Rows[0]["PricePlanId"] + "' and amountperOrder is NOT NULL and amountperOrder NOT IN('') and PayperOrderPlan='True'";
                    SqlDataAdapter adcheck = new SqlDataAdapter(chkaper, con);
                    DataTable dtcheck = new DataTable();
                    adcheck.Fill(dtcheck);
                    if (dtcheck.Rows.Count > 0)
                    {

                        string chkse = "select * FROM PaypalordercustomerbalanceTbl WHERE CustomerID='" + dtc.Rows[0]["CompanyLoginId"] + "' and PricePlanID='" + dtc.Rows[0]["PricePlanId"] + "'";
                        SqlDataAdapter adse = new SqlDataAdapter(chkse, con);
                        DataTable dtse = new DataTable();
                        adse.Fill(dtse);
                        if (dtse.Rows.Count <= 0)
                        {
                            totabala = Convert.ToInt32(dtcheck.Rows[0]["FreeIntialOrders"]);
                            netdo = (Convert.ToDecimal(ViewState["mc_gross"])) + totabala;
                            string inpayper = "INSERT INTO Payperorderplansubscriptiontbl " +
                                           " (priceplanid, CustomerID, AmountPaid, orderID) " +
                                           " VALUES     ('" + dtc.Rows[0]["PricePlanId"] + "','" + dtc.Rows[0]["CompanyLoginId"] + "','" + netdo + "','" + dtc.Rows[0]["OrderId"] + "')";
                            SqlCommand cmdinpa = new SqlCommand(inpayper, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmdinpa.ExecuteNonQuery();
                            con.Close();
                            string inpaocb = "INSERT INTO PaypalordercustomerbalanceTbl " +
                                       " (priceplanid,CustomerID,Balance,Subdate) " +
                                       " VALUES('" + dtc.Rows[0]["PricePlanId"] + "','" + dtc.Rows[0]["CompanyLoginId"] + "','" + netdo + "','" + DateTime.Now.ToShortDateString() + "')";
                            SqlCommand cmpaocb = new SqlCommand(inpaocb, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmpaocb.ExecuteNonQuery();
                            con.Close();
                        }
                        else
                        {
                            double totalamount = 0;
                            double netamount = 0;
                            totalamount = Convert.ToDouble(ViewState["mc_gross"]);
                            netamount = Convert.ToDouble(dtse.Rows[0]["Balance"]);
                            netamount = netamount + totalamount;
                            string inpayper = "Update Payperorderplansubscriptiontbl Set AmountPaid='" + ViewState["mc_gross"] + "' where CustomerID='" + dtc.Rows[0]["CompanyLoginId"] + "'";
                            SqlCommand cmdinpa = new SqlCommand(inpayper, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmdinpa.ExecuteNonQuery();
                            con.Close();
                            string inpaocb = "Update PaypalordercustomerbalanceTbl Set Balance='" + netamount + "',Subdate='" + DateTime.Now.ToShortDateString() + "' where CustomerID='" + dtc.Rows[0]["CompanyLoginId"] + "'";
                            SqlCommand cmpaocb = new SqlCommand(inpaocb, con);
                            con.Open();
                            cmpaocb.ExecuteNonQuery();
                            con.Close();
                        }
                    }

                    insertStatus(Convert.ToInt32(Orderid), payment_status.ToString(), txn_id.ToString(), ViewState["mc_gross"].ToString());
                   
                    if (flag == 0)
                    {
                        sendmail(dtorder.Rows[0]["PaypalEmailId"].ToString());
                        
                        try
                        {
                            if (Request.QueryString["comidauto"] != null)
                            {

                            }
                            else
                            {
                                sendmailtoclientforcompanyactivation();
                            }
                        }
                        catch (Exception ex)
                        {
                            sendmailtoclientforcompanyactivation();
                        }

                        
                       // sendmailclient1();
                    }
                    else
                    {
                    }

                }
            }
        }
    }
    private string Encrypt(string strtxt, string strtoencrypt)
    {
        byte[] bykey = new byte[20];
        byte[] dv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        try
        {
            bykey = System.Text.Encoding.UTF8.GetBytes(strtoencrypt.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputArray = System.Text.Encoding.UTF8.GetBytes(strtxt);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(bykey, dv), CryptoStreamMode.Write);
            cs.Write(inputArray, 0, inputArray.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    public string encryptstrring(string strText)
    {
        return Encrypt(strText, "&%#@?,:*");
    }
    protected void dyconnlmaster()
    {
        dynconn = new SqlConnection();
        string strcln = "Select BusiControllerMasterTBl.* from BusiControllerMasterTBl inner join CompanyMaster on CompanyMaster.CompanyLoginId= BusiControllerMasterTBl.compnaymasterid inner join PricePlanMaster on PricePlanMaster.PricePlanId=CompanyMaster.PricePlanId inner join ProductMaster on ProductMaster.ProductId= PricePlanMaster.ProductId where CompanyMaster.CompanyLoginId='" + Session["CompId"] + "' and ProductMaster.Download='1'";
        SqlCommand cmdclnbc = new SqlCommand(strcln, con);
        DataTable dtclnbc = new DataTable();
        SqlDataAdapter adpclnbc = new SqlDataAdapter(cmdclnbc);
        adpclnbc.Fill(dtclnbc);
        if (dtclnbc.Rows.Count > 0)
        {
            string scot2 = dtclnbc.Rows[0]["DatabaseServerNameOrIp"].ToString().Substring(0, 4);
            string scot12 = dtclnbc.Rows[0]["DatabaseServerNameOrIp"].ToString().Substring(0, 2);
            if (scot2 != "tcp:" || scot12 != "19")
            {
                dynconn.ConnectionString = @"Data Source=" + dtclnbc.Rows[0]["DatabaseServerNameOrIp"].ToString() + ";Initial Catalog=" + dtclnbc.Rows[0]["DatabaseName"].ToString() + ";Persist Security Info=True;User ID=" + dtclnbc.Rows[0]["UserId"].ToString() + ";Password=" + dtclnbc.Rows[0]["Password"].ToString() + "";

            }
            else
            {
                dynconn.ConnectionString = @"Data Source=" + dtclnbc.Rows[0]["DatabaseServerNameOrIp"].ToString() + "," + dtclnbc.Rows[0]["Port"].ToString() + ";Initial Catalog=" + dtclnbc.Rows[0]["DatabaseName"].ToString() + ";Persist Security Info=True;User ID=" + dtclnbc.Rows[0]["UserID"].ToString() + ";Password=" + dtclnbc.Rows[0]["Password"].ToString() + "";

            }
        }
        else
        {
            dynconn.ConnectionString = @"Data Source=tcp:192.168.1.219,2810;Initial Catalog=busiwizclient;Integrated Security=True";

        }
    }
    public void insertStatusclient(int orderid, string status, string txnid, string pamt)
    {
        string str = "insert into OrderPaymentSatus(OrderId,Clientid,PaymentStatus,TransactionID,Paidamount,orderdate)values('0','" + orderid + "','" + status.ToString() + "','" + txnid.ToString() + "','" + pamt + "','" + DateTime.Now.ToShortDateString() + "')";
        SqlCommand cmd = new SqlCommand(str, con);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
    }
    public void insertStatus(int orderid, string status, string txnid, string pamt)
    {

        string str = "insert into OrderPaymentSatus(OrderId,PaymentStatus,TransactionID,Paidamount,orderdate)values('" + orderid + "','" + status.ToString() + "','" + txnid.ToString() + "','" + pamt + "','" + DateTime.Now.ToShortDateString() + "')";
        SqlCommand cmd = new SqlCommand(str, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd.ExecuteNonQuery();
        con.Close();
    }


    public void sendmail(string To)
    {



        string str = "select distinct  ProductMaster.ProductId, ProductMaster.loginurlforuser, ClientMaster.*,PricePlanMaster.PricePlanName,CompanyMaster.PlanId,CompanyMaster.Adminid,CompanyMaster.Password,CompanyMaster.CompanyLoginId,StateMasterTbl.StateName,CountryMaster.CountryName,CompanyMaster.Email,CompanyMaster.CompanyName as cname  from  CompanyMaster inner join PricePlanMaster  on CompanyMaster.PricePlanId=PricePlanMaster.PricePlanId inner join VersionInfoMaster on VersionInfoMaster.VersionInfoId=PricePlanMaster.VersionInfoMasterId inner join ProductMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId inner join ClientMaster on ProductMaster.ClientMasterId=ClientMaster.ClientMasterId inner join StateMasterTbl on StateMasterTbl.StateId=ClientMaster.StateId inner join CountryMaster on StateMasterTbl.CountryId=CountryMaster.CountryId  WHERE(CompanyLoginId = '" + Session["CompId"].ToString() + "') ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            string struu = "insert into ClientOrderPaymentStatus(ClientMasterId,TransactionID)values('35','" + dt.Rows[0]["Email"].ToString() + "')";
            SqlCommand cmduu = new SqlCommand(struu, con);
            con.Open();
            cmduu.ExecuteNonQuery();
            con.Close();

            string str1 = "select distinct PortalMasterTbl.EmailDisplayname,PortalMasterTbl.UserIdtosendmail,PortalMasterTbl.Password,PortalMasterTbl.Mailserverurl, PortalMasterTbl.LogoPath,PortalMasterTbl.Supportteammanagername,PortalMasterTbl.City,PortalMasterTbl.Zip,PortalMasterTbl.Supportteamemailid,PortalMasterTbl.PortalName,PortalMasterTbl.Portalmarketingwebsitename,PortalMasterTbl.Address1,PortalMasterTbl.Supportteamphoneno,PortalMasterTbl.Supportteamphonenoext,PortalMasterTbl.Tollfree,PortalMasterTbl.Tollfreeext,PortalMasterTbl.Fax, ProductMaster.ProductURL, ProductMaster.loginurlforuser, ClientMaster.*,PricePlanMaster.PricePlanName,CompanyMaster.PlanId,CompanyMaster.Adminid,CompanyMaster.Password,CompanyMaster.CompanyLoginId,StateMasterTbl.StateName,CountryMaster.CountryName  from  CompanyMaster inner join PricePlanMaster  on CompanyMaster.PricePlanId=PricePlanMaster.PricePlanId inner join VersionInfoMaster on VersionInfoMaster.VersionInfoId=PricePlanMaster.VersionInfoMasterId inner join ProductMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId inner join ClientMaster on ProductMaster.ClientMasterId=ClientMaster.ClientMasterId  inner join Priceplancategory on Priceplancategory.ID=PricePlanMaster.PriceplancatId inner join PortalMasterTbl on PortalMasterTbl.Id=Priceplancategory.PortalId inner join StateMasterTbl on StateMasterTbl.StateId=PortalMasterTbl.StateId inner join CountryMaster on StateMasterTbl.CountryId=CountryMaster.CountryId WHERE (CompanyLoginId = '" + Request.QueryString["cid"] + "') ";
            SqlCommand cmd1 = new SqlCommand(str1, con);
            SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            adp1.Fill(dt1);

            if (dt1.Rows.Count > 0)
            {

                StringBuilder strhead = new StringBuilder();
                StringBuilder dear = new StringBuilder();
                StringBuilder strorder = new StringBuilder();
                StringBuilder strplan = new StringBuilder();

                strhead.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
                strhead.Append("<tr><td align=\"left\"> <img src=\"http://license.busiwiz.com/images/" + dt1.Rows[0]["LogoPath"].ToString() + "\" \"border=\"0\" Width=\"90\" Height=\"80\" / > </td ></tr>  ");
                strhead.Append("<br></table> ");

                dear.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
                dear.Append("<tr><td align=\"left\">Dear " + dt.Rows[0]["cname"].ToString() + ",</td></tr>  ");
                dear.Append("<tr><td align=\"left\"><br></td></tr>  ");
                dear.Append("</table> ");

                string stprder = "Select OrderPaymentSatus.* from OrderPaymentSatus inner join OrderMaster on OrderMaster.OrderId=OrderPaymentSatus.OrderId inner join CompanyMaster on CompanyMaster.CompanyLoginId=OrderMaster.CompanyLoginId where CompanyMaster.CompanyLoginId= '" + Request.QueryString["cid"] + "'";
                SqlCommand cmdorder = new SqlCommand(stprder, con);
                SqlDataAdapter adorder = new SqlDataAdapter(cmdorder);
                DataTable dtorder = new DataTable();
                adorder.Fill(dtorder);

                if (dtorder.Rows.Count > 0)
                {
                    strorder.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
                    strorder.Append("<tr><td  align=\"left\">Thank you for your order and welcome to ijobcenter.com. Please keep the details below in a safe place for future reference.<br><br></td></tr>");
                    strorder.Append("</table> ");
                    strorder.Append("<table width=\"100%\" style=\"font-size: 10pt;font-weight:bold;color: #000000; font-family: Arial\"> ");
                    strorder.Append("<tr><td  align=\"left\">Order Information</td></tr>");
                    strorder.Append("</table> ");
                    strorder.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
                    strorder.Append("<tr><td align=\"left\" style=\"width: 20%\">Order Number: </td><td align=\"left\" style=\"width: 80%\">" + dtorder.Rows[0]["OrderId"].ToString() + "</td></tr>");
                    strorder.Append("<tr><td align=\"left\" style=\"width: 20%\">Order Date: </td><td align=\"left\" style=\"width: 80%\">" + dtorder.Rows[0]["orderdate"].ToString() + "</td></tr>");
                    strorder.Append("<tr><td align=\"left\" style=\"width: 20%\">Amount Paid: </td><td align=\"left\" style=\"width: 80%\">$ " + dtorder.Rows[0]["Paidamount"].ToString() + "</td></tr>");
                    strorder.Append("<tr><td align=\"left\" style=\"width: 20%\"> Transaction ID: </td><td align=\"left\" style=\"width: 80%\">" + dtorder.Rows[0]["TransactionID"].ToString() + "<br><br></td></tr>");
                    strorder.Append("</table> ");
                }

                string stplan = "select Priceplancategory.CategoryName,VersionInfoMaster.VersionInfoName,ProductMaster.ProductName,ProductMaster.ClientMasterId,PricePlanMaster.* from VersionInfoMaster inner join ProductMaster  on VersionInfoMaster.productid=ProductMaster.productId join PricePlanMaster on PricePlanMaster.VersionInfoMasterId=VersioninfoMaster.VersionInfoId inner join Priceplancategory on Priceplancategory.ID=PricePlanMaster.PriceplancatId inner join PortalMasterTbl on PortalMasterTbl.Id=Priceplancategory.PortalId where PricePlanId='" + dt.Rows[0]["PlanId"].ToString() + "'";
                SqlCommand cmdplan = new SqlCommand(stplan, con);
                SqlDataAdapter adpplan = new SqlDataAdapter(cmdplan);
                DataTable dtplan = new DataTable();
                adpplan.Fill(dtplan);

                if (dtplan.Rows.Count > 0)
                {
                    double amt = 0;
                    string stplan11 = "select * from Payperorderplansubscriptiontbl where priceplanid='" + dt.Rows[0]["PlanId"].ToString() + "' and CustomerID='" + Request.QueryString["cid"] + "'";

                    SqlCommand cmdplan11 = new SqlCommand(stplan11, con);
                    SqlDataAdapter adpplan11 = new SqlDataAdapter(cmdplan11);
                    DataTable dtplan11 = new DataTable();
                    adpplan11.Fill(dtplan11);

                    if (dtplan11.Rows.Count > 0)
                    {
                        amt = Convert.ToDouble(dtplan11.Rows[0]["AmountPaid"].ToString());
                    }
                    else
                    {
                        amt = Convert.ToDouble(dtplan.Rows[0]["PricePlanAmount"].ToString());
                    }


                    strplan.Append("<table width=\"100%\" style=\"font-size: 10pt;font-weight:bold;color: #000000; font-family: Arial\"> ");
                    strplan.Append("<tr><td align=\"left\">Product/Service Details</td></tr> ");
                    strplan.Append("</table> ");

                    strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
                    strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Product Name: </td><td align=\"left\" style=\"width: 80%\">" + dt1.Rows[0]["PortalName"].ToString() + "</td></tr>");
                    strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Plan ID: </td><td align=\"left\" style=\"width: 80%\">" + dtplan.Rows[0]["PricePlanId"].ToString() + "</td></tr>");
                    strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Plan Name: </td><td align=\"left\" style=\"width: 80%\">" + dtplan.Rows[0]["CategoryName"].ToString() + " - " + dtplan.Rows[0]["PricePlanName"].ToString() + "</td></tr>");
                    strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Plan Description: </td><td align=\"left\" style=\"width: 80%\">" + dtplan.Rows[0]["PricePlanDesc"].ToString() + "</td></tr>");
                    strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Plan Start Date: </td><td align=\"left\" style=\"width: 80%\">" + DateTime.Now.ToShortDateString() + "</td></tr>");
                    strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Validity Period (Months): </td><td align=\"left\" style=\"width: 80%\">" + dtplan.Rows[0]["DurationMonth"].ToString() + "</td></tr>");
                    strplan.Append("</table> ");

                    strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
                    strplan.Append("<tr><td align=\"left\"><br><br><br><br></td></tr> ");
                    strplan.Append("</table> ");

                    strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
                    strplan.Append("<tr><td align=\"left\">Your order is being processed and you will receive an email from our support team with information about configuring your account shortly. In the meantime, if you have any questions regarding this information,please contact us any time at <a href=mailto:" + dt1.Rows[0]["Supportteamemailid"].ToString() + " >" + dt1.Rows[0]["Supportteamemailid"].ToString() + "<br><br></td></tr> ");
                    strplan.Append("</table> ");

                 

                    //strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
                    //strplan.Append("<tr><td align=\"left\"><br><br></td></tr> ");
                    //strplan.Append("<tr><td align=\"left\">If you have any questions regarding this information, please contact us at <a href=mailto:" + dt1.Rows[0]["Supportteamemailid"].ToString() + " >" + dt1.Rows[0]["Supportteamemailid"].ToString() + " </a> </td></tr> ");
                    //strplan.Append("<tr><td align=\"left\"><br></td></tr> ");
                    //strplan.Append("</table> ");


                    string ext = "";
                    string tollfree = "";
                    string tollfreeext = "";

                    if (Convert.ToString(dt1.Rows[0]["Supportteamphonenoext"].ToString()) != "" && Convert.ToString(dt1.Rows[0]["Supportteamphonenoext"].ToString()) != null)
                    {
                        ext = "ext " + dt1.Rows[0]["Supportteamphonenoext"].ToString();
                    }

                    if (Convert.ToString(dt1.Rows[0]["Tollfree"].ToString()) != "" && Convert.ToString(dt1.Rows[0]["Tollfree"].ToString()) != null)
                    {
                        tollfree = dt1.Rows[0]["Tollfree"].ToString();
                    }

                    if (Convert.ToString(dt1.Rows[0]["Tollfree"].ToString()) != "" && Convert.ToString(dt1.Rows[0]["Tollfree"].ToString()) != null)
                    {
                        tollfreeext = "ext " + dt1.Rows[0]["Tollfreeext"].ToString();
                    }


                    strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
                    strplan.Append("<tr><td align=\"left\">Thank you,</td></tr> ");

                    //strplan.Append("<tr><td align=\"left\">Sincerely, </td></tr> ");
                    //strplan.Append("<tr><td align=\"left\"><br> </td></tr> ");

                    strplan.Append("<tr><td align=\"left\">" + dt1.Rows[0]["Supportteammanagername"].ToString() + "- Support Manager</td></tr> ");
                    strplan.Append("<tr><td align=\"left\">" + dt1.Rows[0]["PortalName"].ToString() + " </td></tr> ");
                    strplan.Append("<tr><td align=\"left\">" + dt1.Rows[0]["Supportteamphoneno"].ToString() + "  " + ext + "  </td></tr> ");
                    strplan.Append("<tr><td align=\"left\">" + tollfree + " " + tollfreeext + " </td></tr> ");
                    strplan.Append("<tr><td align=\"left\">" + dt1.Rows[0]["Portalmarketingwebsitename"].ToString() + " </td></tr> ");
                    strplan.Append("<tr><td align=\"left\">" + dt1.Rows[0]["Address1"].ToString() + " </td></tr> ");
                    strplan.Append("<tr><td align=\"left\">" + dt1.Rows[0]["City"].ToString() + " " + dt1.Rows[0]["StateName"].ToString() + " " + dt1.Rows[0]["CountryName"].ToString() + " " + dt1.Rows[0]["Zip"].ToString() + " </td></tr> ");
                    strplan.Append("</table> ");



                    string bodyformate = "" + strhead + "" + dear + "" + strorder + "<br>" + strplan + "";

                    string struu3 = "insert into ClientOrderPaymentStatus(ClientMasterId,TransactionID)values('35','Welcomemessage')";
                    SqlCommand cmduu3 = new SqlCommand(struu3, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmduu3.ExecuteNonQuery();
                    con.Close();


                    try
                    {

                        MailAddress to = new MailAddress(dt.Rows[0]["Email"].ToString());
                        MailAddress from = new MailAddress(dt1.Rows[0]["UserIdtosendmail"].ToString(), dt1.Rows[0]["EmailDisplayname"].ToString());//donot_reply@ijobcenter.com **Sales Team IJobCenter
                        MailMessage objEmail = new MailMessage(from, to);
                        objEmail.Subject = "Welcome to " + dt1.Rows[0]["PortalName"].ToString();
                        objEmail.Body = bodyformate.ToString();
                        objEmail.IsBodyHtml = true;
                        objEmail.Priority = MailPriority.High;
                        SmtpClient client = new SmtpClient();
                        client.Credentials = new NetworkCredential(dt1.Rows[0]["UserIdtosendmail"].ToString(), dt1.Rows[0]["Password"].ToString());
                        client.Host = dt1.Rows[0]["Mailserverurl"].ToString(); //72.38.84.230
                      //  client.Port = 587;
                        client.Send(objEmail);



                    }
                    catch (Exception e)
                    {
                        string struu1 = "insert into ClientOrderPaymentStatus(ClientMasterId,TransactionID)values('35','" + e.ToString() + "')";
                        SqlCommand cmduu1 = new SqlCommand(struu1, con);
                        con.Open();
                        cmduu.ExecuteNonQuery();
                        con.Close();
                    }

                }
            }


        }
    }
    public void sendmailclient1()
    {


        //  string str = "select distinct  ClientMaster.*,PricePlanMaster.PricePlanName,CompanyMaster.PlanId,CompanyMaster.Adminid,CompanyMaster.Password,CompanyMaster.CompanyLoginId,CompanyMaster.CompanyName as cname,CompanyMaster.ContactPerson as cperson, CompanyMaster.Email from  CompanyMaster inner join PricePlanMaster  on CompanyMaster.PricePlanId=PricePlanMaster.PricePlanId inner join ProductMaster on PricePlanMaster.ProductId=ProductMaster.ProductId inner join ClientMaster on ProductMaster.ClientMasterId=ClientMaster.ClientMasterId WHERE(CompanyLoginId = '" + Request.QueryString["cid"] + "') ";
        string str = "select distinct ProductMaster.loginurlforuser, ClientMaster.*,PricePlanMaster.PricePlanName,CompanyMaster.PlanId,CompanyMaster.Adminid,CompanyMaster.Password,CompanyMaster.CompanyLoginId,StateMasterTbl.StateName,CountryMaster.CountryName,CompanyMaster.Email  from  CompanyMaster inner join PricePlanMaster  on CompanyMaster.PricePlanId=PricePlanMaster.PricePlanId inner join VersionInfoMaster on VersionInfoMaster.VersionInfoId=PricePlanMaster.VersionInfoMasterId inner join ProductMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId inner join ClientMaster on ProductMaster.ClientMasterId=ClientMaster.ClientMasterId inner join StateMasterTbl on StateMasterTbl.StateId=ClientMaster.StateId inner join CountryMaster on StateMasterTbl.CountryId=CountryMaster.CountryId WHERE(CompanyLoginId = '" + Session["CompId"].ToString() + "') ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            string str1 = "select distinct ProductMaster.ProductURL, ProductMaster.loginurlforuser, ClientMaster.*,PricePlanMaster.PricePlanName,CompanyMaster.PlanId,CompanyMaster.Adminid,CompanyMaster.Password,CompanyMaster.CompanyLoginId,StateMasterTbl.StateName,CountryMaster.CountryName  from  CompanyMaster inner join PricePlanMaster  on CompanyMaster.PricePlanId=PricePlanMaster.PricePlanId inner join VersionInfoMaster on VersionInfoMaster.VersionInfoId=PricePlanMaster.VersionInfoMasterId inner join ProductMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId inner join ClientMaster on ProductMaster.ClientMasterId=ClientMaster.ClientMasterId inner join StateMasterTbl on StateMasterTbl.StateId=ClientMaster.StateId inner join CountryMaster on StateMasterTbl.CountryId=CountryMaster.CountryId WHERE(CompanyLoginId = '" + Request.QueryString["cid"] + "') ";
            SqlCommand cmd1 = new SqlCommand(str1, con);
            SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            adp.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {

                StringBuilder strhead = new StringBuilder();
                strhead.Append("<table width=\"100%\"> ");
                strhead.Append("<tr><td align=\"left\"> <img src=\"http://license.busiwiz.com/images/" + dt.Rows[0]["CompanyLogo"].ToString() + "\" \"border=\"0\" Width=\"200px\" Height=\"125px\" / > </td ><td Width=\"200px\"></td><td align=\"Centre\"><br><span style=\"color: #996600\">" + dt.Rows[0]["CompanyName"].ToString() + "</span></b><Br>" + dt.Rows[0]["Address1"].ToString() + "<Br><Br>" + dt.Rows[0]["city"].ToString() + "<Br>" + dt.Rows[0]["Statename"].ToString() + "<Br>" + dt.Rows[0]["CountryName"].ToString() + "<Br>" + dt.Rows[0]["Phone1"].ToString() + "," + dt.Rows[0]["Phone2"].ToString() + "<Br>" + dt.Rows[0]["Fax1"].ToString() + "<Br>" + dt.Rows[0]["Email1"].ToString() + "<Br>" + dt.Rows[0]["ClientURL"].ToString() + " </td></tr>  ");
                strhead.Append("<br></table> ");

                string stprder = "Select OrderPaymentSatus.* from OrderPaymentSatus inner join OrderMaster on OrderMaster.OrderId=OrderPaymentSatus.OrderId inner join CompanyMaster on CompanyMaster.CompanyLoginId=OrderMaster.CompanyLoginId where CompanyMaster.CompanyLoginId= '" + Request.QueryString["cid"] + "'";

                SqlCommand cmdorder = new SqlCommand(stprder, con);
                SqlDataAdapter adorder = new SqlDataAdapter(cmdorder);
                DataTable dtorder = new DataTable();
                adorder.Fill(dtorder);
                StringBuilder strorder = new StringBuilder();
                if (dtorder.Rows.Count > 0)
                {
                    // You have successfully paid the following order<br>
                    strorder.Append("<br><table width=\"100%\"> ");
                    strorder.Append("<tr><td align=\"left\">Thanks for buying our products/subscribing our services.<br><br><b>Order No:</b>" + dtorder.Rows[0]["OrderId"].ToString() + "<br><b>Order Date:</b>" + dtorder.Rows[0]["orderdate"].ToString() + "<br><b>Amount Paid:</b>" + dtorder.Rows[0]["Paidamount"].ToString() + "</b><Br><b>Transaction ID:</b>" + dtorder.Rows[0]["TransactionID"].ToString() + "<b></td></br></tr>");
                    strorder.Append("</table> ");
                }
                string body = "&nbsp;&nbsp;&nbsp;Thanks for subscribing our following product/plan.";

                //string stplan = "SELECT * from PricePlanMaster WHERE(PricePlanId = '" + dt.Rows[0]["PlanId"].ToString() + "')";
                string stplan = "select VersionInfoMaster.VersionInfoName,ProductMaster.ProductName,ProductMaster.ClientMasterId,PricePlanMaster.* from VersionInfoMaster inner join ProductMaster  on VersionInfoMaster.productid=ProductMaster.productId join PricePlanMaster on PricePlanMaster.VersionInfoMasterId=VersioninfoMaster.VersionInfoId where PricePlanId='" + dt.Rows[0]["PlanId"].ToString() + "'";

                SqlCommand cmdplan = new SqlCommand(stplan, con);
                SqlDataAdapter adpplan = new SqlDataAdapter(cmdplan);
                DataTable dtplan = new DataTable();
                adpplan.Fill(dtplan);
                StringBuilder strplan = new StringBuilder();
                if (dtplan.Rows.Count > 0)
                {

                    double amt = 0;
                    string stplan11 = "select * from Payperorderplansubscriptiontbl where priceplanid='" + dt.Rows[0]["PlanId"].ToString() + "' and CustomerID='" + Request.QueryString["cid"] + "'";

                    SqlCommand cmdplan11 = new SqlCommand(stplan11, con);
                    SqlDataAdapter adpplan11 = new SqlDataAdapter(cmdplan11);
                    DataTable dtplan11 = new DataTable();
                    adpplan11.Fill(dtplan11);
                    if (dtplan11.Rows.Count > 0)
                    {
                        amt = Convert.ToDouble(dtplan11.Rows[0]["AmountPaid"].ToString());
                    }
                    else
                    {
                        amt = Convert.ToDouble(dtplan.Rows[0]["PricePlanAmount"].ToString());
                    }
                    strplan.Append("<table width=\"100%\"> ");
                    strplan.Append("<tr><td align=\"left\"><b>Product & Version Name:</b><b><span style=\"color: #996600\">" + dtplan.Rows[0]["ProductName"].ToString() + " & " + " " + dtplan.Rows[0]["VersionInfoName"].ToString() + "</span></b><br><b>Plan Name:</b>" + dtplan.Rows[0]["PricePlanName"].ToString() + "</b><Br><b>Plan Description:</b>" + dtplan.Rows[0]["PricePlanDesc"].ToString() + "<Br><b>Plan Start Date:</b>" + DateTime.Now.ToShortDateString() + "<Br><b>Validity Period(Month):</b>" + dtplan.Rows[0]["DurationMonth"].ToString() + "<Br><b>Plan Amount:</b>" + amt + "</td></tr> ");
                    strplan.Append("</table> ");

                }
                string license = "select CompanyMaster.CompanyId,LicenseMaster.LicenseKey from CompanyMaster inner join LicenseMaster on LicenseMaster.CompanyId=CompanyMaster.CompanyId where CompanyLoginId='" + Request.QueryString["cid"] + "'";

                SqlCommand cmdl = new SqlCommand(license, con);
                SqlDataAdapter adl = new SqlDataAdapter(cmdl);
                DataTable dtl = new DataTable();
                adl.Fill(dtl);
                // &nbsp;&nbsp;&nbsp;<b>Your license number is :</b>" + dtl.Rows[0]["LicenseKey"].ToString() + "<br><br>
                string bodytext = "<span style=\"color: #F2618D\"><b>PLEASE NOTE :: You will have to configure your account before you start using the product. </span><a href=http://members.busiwiz.com/Companyconfigureinfo.aspx?comid=" + dt.Rows[0]["CompanyLoginId"].ToString() + " target=_blank >Please configure now by clicking here.</b> </a>";
                string sprk = "SELECT distinct CompanyId,PricePlanId,ProductMaster.ProductURL,BusiControllerMasterTBl.BusiControllerApplicationURL, BusiControllerMasterTBl.returnurlbisicontrol,BusiControllerMasterTBl.DatabaseName,BusiControllerMasterTBl.UserID,BusiControllerMasterTBl.Password,BusiControllerMasterTBl.Port,BusiControllerMasterTBl.DatabaseServerNameOrIp from CompanyMaster inner join  ProductMaster on CompanyMaster.ProductId=ProductMaster.ProductId inner join VersionInfoMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId inner join BusiControllerMasterTBl on BusiControllerMasterTBl.versioninfomasterid=VersionInfoMaster.VersionInfoId WHERE CompanyLoginId ='" + Request.QueryString["cid"] + "'";
                SqlCommand cmdsprk = new SqlCommand(sprk, con);
                SqlDataAdapter adsprk = new SqlDataAdapter(cmdsprk);
                DataTable dtsprk = new DataTable();
                adsprk.Fill(dtsprk);
                string bodytext1 = "";
                if (dtsprk.Rows.Count > 0)
                {
                    bodytext1 = " ";
                }

                string que = "<br><br><centre>&nbsp;&nbsp;If you have any technical question related to license or your order,Please contact us by email at <a href=mailto:techsupport@busiwiz.com >techsupport@busiwiz.com </a></centre><br><br>&nbsp;&nbsp;Thanking you.<br><br>&nbsp;&nbsp;Sincerly";
                StringBuilder support = new StringBuilder();
                support.Append("<table width=\"100%\"> ");
                support.Append("<tr><td align=\"left\"><b>Support Team: </b><b><span style=\"color: #996600\">" + dt.Rows[0]["CompanyName"].ToString() + "</span></b><br><b>Phone :</b>" + dt.Rows[0]["Phone1"].ToString() + "<Br><b>Email :</b>" + dt.Rows[0]["Email1"].ToString() + "<Br></td></tr>  ");
                support.Append("</table> ");
                string bodyformate = "" + strhead + "" + strorder + "<br>" + body + "<br>" + strplan + "" + bodytext + "<br>" + bodytext1 + "<br>" + que + "" + support + "";
              

                    MailAddress to = new MailAddress(dt.Rows[0]["Email1"].ToString());
                    MailAddress from = new MailAddress(dt.Rows[0]["OutgoingServerUserID"].ToString());
                    MailMessage objEmail = new MailMessage(from, to);
                    objEmail.Subject = "Welcome " + dt.Rows[0]["CompanyName"].ToString() + "";

                    objEmail.Body = bodyformate.ToString();
                    objEmail.IsBodyHtml = true;
                    objEmail.Priority = MailPriority.High;

                    SmtpClient client = new SmtpClient();
                    client.Credentials = new NetworkCredential(dt.Rows[0]["OutgoingServerUserID"].ToString(), dt.Rows[0]["OutgoingServerPassword"].ToString());
                    client.Host = dt.Rows[0]["OurgoingServerSMTP"].ToString();
                    //client.Port = 587;
                    client.Send(objEmail);

               
            }
        }
    }
    public string CreateLicenceKey(out string HashKey)
    {
        string str = "";
        string s1 = "";
        string s2 = "";
        string s3 = "";
        string s4 = "";
        s1 = DateTime.Now.ToString().GetHashCode().ToString("x").ToString();
        if (s1.Length > 5)
        {
            s1 = s1.Substring(0, 5); //
        }
        else
        {
            s1 = s1 + "1";
        }
        s2 = DateTime.Now.Ticks.ToString("x").ToString();  //DateTime.Now.ToString().GetHashCode().ToString("x");
        if (s2.Length > 9)
        {
            s2 = s2.Substring(4, 5); //
        }
        s3 = Guid.NewGuid().ToString().GetHashCode().ToString("x").ToString(); //DateTime.Now.ToString().GetHashCode().ToString("x");
        if (s3.Length > 5)
        {
            s3 = s3.Substring(0, 5); //
        }
        s4 = RNGCharacterMask().ToString().Substring(0, 5); // DateTime.Now.ToString().GetHashCode().ToString("x");
        if (s4.Length > 5)
        {
            s4 = s4.Substring(0, 5); //
        }
        string hashcode = "";
        string s11 = "";
        string s22 = "";
        string s33 = "";
        string s44 = "";
        string s55 = "";
        s11 = DateTime.Now.ToString().GetHashCode().ToString("x").ToString();
        s22 = DateTime.Now.Ticks.ToString("x").ToString();  //DateTime.Now.ToString().GetHashCode().ToString("x");
        s33 = Guid.NewGuid().ToString().GetHashCode().ToString("x").ToString(); //DateTime.Now.ToString().GetHashCode().ToString("x");
        s44 = RNGCharacterMask().ToString(); // DateTime.Now.ToString().GetHashCode().ToString("x
        s55 = RNGCharacterMask().ToString(); // DateTime.Now.ToString().GetHashCode().ToString("x
        s11 = s11.Substring(s11.Length - 1, 1);
        s22 = s22.Substring(s22.Length - 1, 1);
        s33 = s33.Substring(s33.Length - 1, 1);
        s44 = s44.Substring(s44.Length - 1, 1);
        s55 = s55.Substring(s55.Length - 2, 1);
        hashcode = s11 + s22 + s33 + s44 + s55;
        str = s3.ToString() + " - " + s2.ToString() + " - " + s1.ToString() + " - " + s4.ToString();
        HashKey = hashcode.ToUpper();
        return str;
    }
    private string RNGCharacterMask()
    {
        int maxSize = 12;
        int minSize = 10;
        char[] chars = new char[62];
        string a;
        a = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        chars = a.ToCharArray();
        int size = maxSize;
        byte[] data = new byte[1];
        RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
        crypto.GetNonZeroBytes(data);
        size = maxSize;
        data = new byte[size];
        crypto.GetNonZeroBytes(data);
        StringBuilder result = new StringBuilder(size);
        foreach (byte b in data)
        { result.Append(chars[b % (chars.Length - 1)]); }
        return result.ToString();
    }
    public string CreateEncreptionKey(out string HashKey)
    {

        string str = "";
        string s1 = "";
        string s2 = "";
        string s3 = "";
        string s4 = "";
        s1 = DateTime.Now.ToString().GetHashCode().ToString("x").ToString();
        if (s1.Length > 5)
        {
            s1 = s1.Substring(0, 5); //
        }
        else
        {
            s1 = s1 + "1";
        }
        s2 = DateTime.Now.Ticks.ToString("x").ToString();  //DateTime.Now.ToString().GetHashCode().ToString("x");
        if (s2.Length > 9)
        {
            s2 = s2.Substring(4, 5); //
        }
        s3 = Guid.NewGuid().ToString().GetHashCode().ToString("x").ToString(); //DateTime.Now.ToString().GetHashCode().ToString("x");
        if (s3.Length > 5)
        {
            s3 = s3.Substring(0, 5); //
        }
        s4 = RNGCharacterMaskENC().ToString().Substring(0, 5); // DateTime.Now.ToString().GetHashCode().ToString("x");
        if (s4.Length > 5)
        {
            s4 = s4.Substring(0, 5); //
        }
        string hashcode = "";
        string s11 = "";
        string s22 = "";
        string s33 = "";
        string s44 = "";
        string s55 = "";
        s11 = DateTime.Now.ToString().GetHashCode().ToString("x").ToString();
        s22 = DateTime.Now.Ticks.ToString("x").ToString();  //DateTime.Now.ToString().GetHashCode().ToString("x");
        s33 = Guid.NewGuid().ToString().GetHashCode().ToString("x").ToString(); //DateTime.Now.ToString().GetHashCode().ToString("x");
        s44 = RNGCharacterMaskENC().ToString(); // DateTime.Now.ToString().GetHashCode().ToString("x
        s55 = RNGCharacterMaskENC().ToString(); // DateTime.Now.ToString().GetHashCode().ToString("x
        s11 = s11.Substring(s11.Length - 1, 1);
        s22 = s22.Substring(s22.Length - 1, 1);
        s33 = s33.Substring(s33.Length - 1, 1);
        s44 = s44.Substring(s44.Length - 1, 1);
        s55 = s55.Substring(s55.Length - 2, 1);
        hashcode = s11 + s22 + s33 + s44 + s55;
        str = s3.ToString() + "" + s2.ToString() + "" + s1.ToString() + "" + s4.ToString();
        HashKey = hashcode.ToUpper();
        return str;
    }
    private string RNGCharacterMaskENC()
    {
        int maxSize = 12;
        int minSize = 10;
        char[] chars = new char[62];
        string a;
        a = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        chars = a.ToCharArray();
        int size = maxSize;
        byte[] data = new byte[1];
        RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
        crypto.GetNonZeroBytes(data);
        size = maxSize;
        data = new byte[size];
        crypto.GetNonZeroBytes(data);
        StringBuilder result = new StringBuilder(size);
        foreach (byte b in data)
        { result.Append(chars[b % (chars.Length - 1)]); }
        return result.ToString();
    }
    public void sendmailclient(string To)
    {
        string str = "SELECT * from ClientMaster WHERE     (ClientMasterId = '" + ViewState["saleorderold"].ToString() + "')";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            StringBuilder strhead = new StringBuilder();
            strhead.Append("<table width=\"100%\"> ");
            strhead.Append("<tr><td align=\"left\"> <img src=\"images/" + dt.Rows[0]["CompanyLogo"].ToString() + "\" \"border=\"0\" Width=\"176px\" Height=\"106px\" / > </td><td align=\"right\"><b><span style=\"color: #996600\">" + dt.Rows[0]["CompanyName"].ToString() + "</span></b><Br>" + dt.Rows[0]["Address1"].ToString() + "<Br><BR><b>Customer Support Phone:</b>" + dt.Rows[0]["Phone1"].ToString() + "," + dt.Rows[0]["Phone2"].ToString() + "<Br><b>Fax:</b>" + dt.Rows[0]["Fax1"].ToString() + "<Br><b>Email:</b>" + dt.Rows[0]["Email1"].ToString() + "<Br><b>Website:</b>" + dt.Rows[0]["ClientURL"].ToString() + " </td></tr>  ");
            strhead.Append("<br><br></table> ");
            StringBuilder strAddress = new StringBuilder();

            strAddress.Append("<table width=\"100%\"> ");
            strAddress.Append("<tr><td align=\"left\"><b>To:</b><br><b>Company Name :</b><b><span style=\"color: #996600\">" + dt.Rows[0]["CompanyName"].ToString() + "</span></b><Br><b>Contact Name :</b>" + dt.Rows[0]["ContactPersonName"].ToString() + "<Br><BR></td></tr> ");
            strAddress.Append("<br></table> ");
            string body = "<br><br><centre>Thanks for subscribing our following product/Plan.Your Login information and Licensekey is mentioned he below.</centre><br><br> ";

            string stplan = "SELECT * from ClientPricePlanMaster WHERE (ClientPricePlanId = '" + dt.Rows[0]["ClientPricePlanId"].ToString() + "')";

            SqlCommand cmdplan = new SqlCommand(stplan, con);
            SqlDataAdapter adpplan = new SqlDataAdapter(cmdplan);
            DataTable dtplan = new DataTable();
            adpplan.Fill(dtplan);
            StringBuilder strplan = new StringBuilder();
            if (dtplan.Rows.Count > 0)
            {


                strplan.Append("<table width=\"100%\"> ");
                strplan.Append("<tr><td align=\"left\"><b>Plan Name:</b><b><span style=\"color: #996600\">" + dtplan.Rows[0]["PricePlanName"].ToString() + "</span></b><Br><b>Plan Description:</b>" + dtplan.Rows[0]["PricePlanDesc"].ToString() + "<br><br> </td></tr>  ");
                strplan.Append("<br></table> ");
            }

            StringBuilder strpay = new StringBuilder();
            strpay.Append("<table width=\"100%\"> ");
            strpay.Append("<tr><td align=\"left\"><b>Plan Amount:</b><b><span style=\"color: #996600\">" + ViewState["mc_gross"] + "</span></b><Br><b>Payment Date:</b>" + DateTime.Now.ToShortDateString() + "<Br><b> Payment By: </b> Paypal<br><b>Conformation ID:</b>" + ViewState["txtid"] + "<Br><b>License Key:</b>" + ViewState["licensekey"] + "<Br><b>License Validity Period:</b>" + ViewState["month"] + "<Br></td></tr>  ");
            strpay.Append("<br></table> ");



            string bodytext = "<br>Your login info are as follows:<br>&nbsp; Client ID:" + dt.Rows[0]["ClientMasterId"].ToString() + " <br>&nbsp; User Name: " + dt.Rows[0]["LoginName"].ToString() + " <br>&nbsp; Password: " + dt.Rows[0]["LoginPassword"].ToString() + " <br>" +
                "<br><br><br><br><centre>If you have any question  regarding this license Pelase Email <a href=mailto:Support@busiwiz.com >Support@busiwiz.com</a></centre>" +
                "<br>Thanks.";
            StringBuilder support = new StringBuilder();
            support.Append("<table width=\"100%\"> ");
            support.Append("<tr><td align=\"left\"><b>Support Team</b><b><span style=\"color: #996600\">" + dt.Rows[0]["CompanyName"].ToString() + "</span></b><br>" + dt.Rows[0]["Phone1"].ToString() + "<Br>" + dt.Rows[0]["Email1"].ToString() + "<Br></td></tr>  ");
            support.Append("<br></table> ");
            string bodyformate = "" + strhead + "<br>" + strAddress + "<br>" + body + "<br>" + strplan + "<br>" + strpay + "<br>" + bodytext + "<br>" + support + "";
            try
            {
                string strmal = "SELECT * from ClientMaster WHERE ClientMasterId = '" + ViewState["saleorderold"].ToString() + "'";
                SqlCommand cmdma = new SqlCommand(strmal, con);
                SqlDataAdapter adpma = new SqlDataAdapter(cmdma);
                DataTable dtma = new DataTable();
                adpma.Fill(dtma);
                if (dtma.Rows.Count > 0)
                {
                    MailAddress to = new MailAddress(To);
                    MailAddress from = new MailAddress(dtma.Rows[0]["OutgoingServerUserID"].ToString());
                    MailMessage objEmail = new MailMessage(from, to);
                    objEmail.Subject = "Welcome " + dt.Rows[0]["CompanyName"].ToString() + " Busiwiz.com - Registration";

                    objEmail.Body = bodyformate.ToString();
                    objEmail.IsBodyHtml = true;
                    objEmail.Priority = MailPriority.High;

                    SmtpClient client = new SmtpClient();
                    client.Credentials = new NetworkCredential(dtma.Rows[0]["OutgoingServerUserID"].ToString(), dtma.Rows[0]["OutgoingServerPassword"].ToString());
                    client.Host = dtma.Rows[0]["OurgoingServerSMTP"].ToString();
                    //client.Port = 587;
                    client.Send(objEmail);
                }
            }
            catch
            {
            }
        }
        //string body = "" + HeadingTable + "<br><br> Dear " + txtlastname.Text + "&nbsp;" + txtfirstname.Text + " ,<br><br>" + welcometext.ToString() + " <br>" + AccountInfo.ToString() + "<br><br><strong><span style=\"color: #996600\"> " + ViewState["sitename"] + " Team</span></strong>";

    }



    public void sendmailtoclientforcompanyactivation()
    {
        string str = "select distinct Priceplancategory.CategoryName,CompanyMaster.CompanyLoginId,CompanyMaster.phone,CompanyMaster.Email,CompanyMaster.pincode,CompanyMaster.CompanyName,CompanyMaster.ContactPerson,CompanyMaster.city,CompanyMaster.Address,StateMasterTbl.StateName,CountryMaster.CountryName, PortalMasterTbl.*,PricePlanMaster.PricePlanAmount,PricePlanMaster.PricePlanName,PricePlanMaster.PricePlanId,OrderPaymentSatus.orderdate,OrderPaymentSatus.TransactionID from  CompanyMaster inner join PricePlanMaster  on CompanyMaster.PricePlanId=PricePlanMaster.PricePlanId inner join VersionInfoMaster on VersionInfoMaster.VersionInfoId=PricePlanMaster.VersionInfoMasterId inner join ProductMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId inner join ClientMaster on ProductMaster.ClientMasterId=ClientMaster.ClientMasterId inner join Priceplancategory on Priceplancategory.ID=PricePlanMaster.PriceplancatId inner join PortalMasterTbl on PortalMasterTbl.Id=Priceplancategory.PortalId   inner join OrderMaster on CompanyMaster.CompanyLoginId=OrderMaster.CompanyLoginId inner join  OrderPaymentSatus on OrderMaster.OrderId=OrderPaymentSatus.OrderId   inner join StateMasterTbl on StateMasterTbl.StateId=CompanyMaster.StateId inner join CountryMaster on StateMasterTbl.CountryId=CountryMaster.CountryId  WHERE(CompanyMaster.CompanyLoginId = '" + Session["CompId"].ToString() + "') ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            StringBuilder strplan = new StringBuilder();

            double amt = 0;

            string stplan11 = "select * from Payperorderplansubscriptiontbl where priceplanid='" + dt.Rows[0]["PricePlanId"].ToString() + "' and CustomerID='" + Request.QueryString["cid"] + "'";
            SqlCommand cmdplan11 = new SqlCommand(stplan11, con);
            SqlDataAdapter adpplan11 = new SqlDataAdapter(cmdplan11);
            DataTable dtplan11 = new DataTable();
            adpplan11.Fill(dtplan11);
           

            if (dtplan11.Rows.Count > 0)
            {
                amt = Convert.ToDouble(dtplan11.Rows[0]["AmountPaid"].ToString());
            }
            else
            {
                amt = Convert.ToDouble(dt.Rows[0]["PricePlanAmount"].ToString());
            }

            string strcompanyactivation = "insert into CompanyActivationRequestTbl (datetimeemailgenerate,emailgeneratecompanyid,emailgeneratecompanyemail,companyactive) values ('" + DateTime.Now.ToShortDateString() + "','" + Session["CompId"].ToString() + "','" + dt.Rows[0]["Email"].ToString() + "','0')";
            SqlCommand cmdcompanyactivation = new SqlCommand(strcompanyactivation, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdcompanyactivation.ExecuteNonQuery();
            con.Close();

            string maxid = "select MAX(Id) as Id from CompanyActivationRequestTbl where emailgeneratecompanyid='" + Session["CompId"].ToString() + "'";
            SqlCommand cmdmaxid = new SqlCommand(maxid, con);
            SqlDataAdapter adptmaxid = new SqlDataAdapter(cmdmaxid);
            DataTable dsmaxid = new DataTable();
            adptmaxid.Fill(dsmaxid);

            if (dsmaxid.Rows.Count > 0)
            {


                strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
                strplan.Append("<tr><td align=\"left\"> <img src=\"http://license.busiwiz.com/images/" + dt.Rows[0]["LogoPath"].ToString() + "\" \"border=\"0\" Width=\"90\" Height=\"80\" / > </td ></tr>  ");
                strplan.Append("<br></table> ");

                strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
                strplan.Append("<tr><td align=\"left\">Dear " + dt.Rows[0]["Supportteammanagername"].ToString() + ",</td></tr>  ");
                strplan.Append("<tr><td align=\"left\"><br></td></tr>  ");
                strplan.Append("</table> ");

                strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
                strplan.Append("<tr><td align=\"left\">The following company has subscribed to the following price plan. Please review this information to determine whether or not to authorize and activate this account.<br><br><br><br></td></tr> "); //" + dt.Rows[0]["CategoryName"].ToString() + " of " + dt.Rows[0]["PortalName"].ToString() + ".</td></tr> ");
                strplan.Append("</table> ");

                strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; font-weight:bold; color:#000000; font-family: Arial\"> ");
                strplan.Append("<tr><td align=\"left\">Company Details</td></tr>  ");
               // strplan.Append("<tr><td align=\"left\"><br></td></tr>  ");
                strplan.Append("</table> ");

                strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
                strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Company ID: </td><td align=\"left\" style=\"width: 80%\">" + dt.Rows[0]["CompanyLoginId"].ToString() + "</td></tr>");
                strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Company Name: </td><td align=\"left\" style=\"width: 80%\">" + dt.Rows[0]["CompanyName"].ToString() + "</td></tr>");
                strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Contact Name: </td><td align=\"left\" style=\"width: 80%\">" + dt.Rows[0]["ContactPerson"].ToString() + "</td></tr>");
                strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Contact Address: </td><td align=\"left\" style=\"width: 80%\">" + dt.Rows[0]["Address"].ToString() + ", " + dt.Rows[0]["city"].ToString() + ", " + dt.Rows[0]["StateName"].ToString() + ", " + dt.Rows[0]["CountryName"].ToString() + ", " + dt.Rows[0]["pincode"].ToString() + "</td></tr>");
                strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Phone No: </td><td align=\"left\" style=\"width: 80%\">" + dt.Rows[0]["phone"].ToString() + "</td></tr>");
                strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Email ID: </td><td align=\"left\" style=\"width: 80%\">" + dt.Rows[0]["Email"].ToString() + "</td></tr>");
                strplan.Append("</table> ");

                strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; font-weight:bold; color: #000000; font-family: Arial\"> ");
                strplan.Append("<tr><td align=\"left\"><br><br></td></tr>  ");
                strplan.Append("<tr><td align=\"left\">Product Details</td></tr>  ");
                //strplan.Append("<tr><td align=\"left\"><br></td></tr>  ");
                strplan.Append("</table> ");

                strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
                strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Product Name: </td><td align=\"left\" style=\"width: 80%\">" + dt.Rows[0]["PortalName"].ToString() + "</td></tr>");
                strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Price Plan Name: </td><td align=\"left\" style=\"width: 80%\">" + dt.Rows[0]["CategoryName"].ToString() + " - " + dt.Rows[0]["PricePlanName"].ToString() + "</td></tr>");
                strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Amount Paid: </td><td align=\"left\" style=\"width: 80%\">" + amt.ToString() + "</td></tr>");
                strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Transaction ID: </td><td align=\"left\" style=\"width: 80%\">" + dt.Rows[0]["TransactionID"].ToString() + "</td></tr>");
                if (Request.QueryString["paymode"] != "")
                {
                    strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Payment Method:</td><td align=\"left\" style=\"width: 80%\">" + Request.QueryString["paymode"] + "</td></tr>");
                }


                strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Transaction Date: </td><td align=\"left\" style=\"width: 80%\">" + dt.Rows[0]["orderdate"].ToString() + "<br><br></td></tr>");
                strplan.Append("</table> ");

              

                string stservername = " select ServerMasterTbl.* from ServerAssignmentMasterTbl inner join ServerMasterTbl on ServerMasterTbl.Id=ServerAssignmentMasterTbl.ServerId where ServerAssignmentMasterTbl.PricePlanId='" + dt.Rows[0]["PricePlanId"].ToString() + "' ";
                SqlCommand cmdservername = new SqlCommand(stservername, con);
                SqlDataAdapter adpservername = new SqlDataAdapter(cmdservername);
                DataTable dtservername = new DataTable();
                adpservername.Fill(dtservername);

                if (dtservername.Rows.Count > 0)
                {
                    strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; font-weight:bold; color: #000000; font-family: Arial\"> ");
                    strplan.Append("<tr><td align=\"left\"><br></td></tr>  ");
                    strplan.Append("<tr><td align=\"left\">Server Details</td></tr>  ");
                    //strplan.Append("<tr><td align=\"left\"><br></td></tr>  ");
                    strplan.Append("</table> ");

                    strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
                    strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Hosted on: </td><td align=\"left\" style=\"width: 80%\">" + dtservername.Rows[0]["ServerName"].ToString() + "<br><br></td></tr>");
                    strplan.Append("</table> ");

                }




                strplan.Append("<table width=\"100%\" style=\"font-size: 10pt;  color: #000000; font-family: Arial\"> ");
                strplan.Append("<tr><td align=\"left\"><br></td></tr>  ");
                strplan.Append("<tr><td align=\"left\">If you wish to authorize and activate this account click <a href=http://license.busiwiz.com/companyapproval.aspx?comid=" + PageMgmt.Encrypted(dt.Rows[0]["CompanyLoginId"].ToString()) + "&id=" + dsmaxid.Rows[0]["Id"].ToString() + "&Franchisee=" + FranCompanyname + " target=_blank >here </a>.or copy and paste the following URL into your internet browser.<br><br></td></tr>  ");
                strplan.Append("<tr><td align=\"left\">http://license.busiwiz.com/companyapproval.aspx?comid=" + PageMgmt.Encrypted(dt.Rows[0]["CompanyLoginId"].ToString()) + "&id=" + dsmaxid.Rows[0]["Id"].ToString() + "&Franchisee=" + FranCompanyname + "</td></tr> ");
                strplan.Append("<tr><td align=\"left\"><br><br></td></tr>  ");
                strplan.Append("</table> ");

                strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
                strplan.Append("<tr><td align=\"left\">Thank you,</td></tr>  ");
                //strplan.Append("<tr><td align=\"left\">Sincerely ,</td></tr>  ");
                strplan.Append("<tr><td align=\"left\">Support Team -" + dt.Rows[0]["PortalName"].ToString() + "</td></tr>  ");
                strplan.Append("</table> ");

                string bodyformate = "" + strplan + "";
                               // try
                //{

                    MailAddress to = new MailAddress(dt.Rows[0]["Supportteamemailid"].ToString());//support@ijobcenter.com
                    MailAddress from = new MailAddress(dt.Rows[0]["UserIdtosendmail"].ToString(), dt.Rows[0]["EmailDisplayname"].ToString());//donot_reply@ijobcenter.com  **Sales Team IJobCenter 
                    MailMessage objEmail = new MailMessage(from, to);
                    objEmail.Subject = " " + dt.Rows[0]["PortalName"].ToString() + " " + dt.Rows[0]["CategoryName"].ToString() + " " + amt.ToString() + " New company approval required " + dt.Rows[0]["CompanyLoginId"].ToString() + " " + dt.Rows[0]["city"].ToString() + "  " + dt.Rows[0]["StateName"].ToString() + "  " + dt.Rows[0]["CountryName"].ToString() + " ";
                    objEmail.Body = bodyformate.ToString();
                    objEmail.IsBodyHtml = true;
                    objEmail.Priority = MailPriority.High;
                    SmtpClient client = new SmtpClient();
                    client.Credentials = new NetworkCredential(dt.Rows[0]["UserIdtosendmail"].ToString(), dt.Rows[0]["Password"].ToString()); //donot_reply@ijobcenter.com  **Om2012++
                    client.Host = dt.Rows[0]["Mailserverurl"].ToString();
                    //client.Port = 587;
                    client.Send(objEmail);

                    /*
                     MailAddress to = new MailAddress(To);
                    MailAddress from = new MailAddress(dtma.Rows[0]["OutgoingServerUserID"].ToString());
                    MailMessage objEmail = new MailMessage(from, to);
                    objEmail.Subject = "Welcome " + dt.Rows[0]["CompanyName"].ToString() + " Busiwiz.com - Registration";

                    objEmail.Body = bodyformate.ToString();
                    objEmail.IsBodyHtml = true;
                    objEmail.Priority = MailPriority.High;

                    SmtpClient client = new SmtpClient();
                    client.Credentials = new NetworkCredential(dtma.Rows[0]["OutgoingServerUserID"].ToString(), dtma.Rows[0]["OutgoingServerPassword"].ToString());
                    client.Host = dtma.Rows[0]["OurgoingServerSMTP"].ToString();
                    client.Send(objEmail);*/

                //}
                //catch (Exception e)
                //{

                //}
            }

        }
    }

    



}
