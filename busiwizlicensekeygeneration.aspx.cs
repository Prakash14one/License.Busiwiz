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
    string strkey;

    //NEW------------------------------------------------------------
    SqlConnection conn;
    SqlConnection coninfinal = new SqlConnection();
    public static string encstr = "";
    public static string Serverencstr = "";

    public static string verid = "";
    public static string macaddresss = "";
    string ComputerName = "";

    #region Variables
    private string sDomain = "TheSafestserver.com";
    private string sDefaultRootOU = "DC=TheSafestserver,DC=com";
    #endregion

    SqlConnection condefaultinstance = new SqlConnection();
    SqlConnection concompanyinstance = new SqlConnection();
    SqlConnection condefaultdatabase = new SqlConnection();

    public static string companyencript = "";
    //--------------------------------------------------
    public StringBuilder strplan1 = new StringBuilder();

    protected void Page_Load(object sender, EventArgs e)
    {

        //string aa = PageConnXYZX.GetC("aswathy");

       

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

                Response.Redirect("http://paymentgateway.safestserver.com/Thankyou.aspx?cid=" + Request.QueryString["cid"] + "");
            }
            else
            {
                Response.Redirect("http://paymentgateway.safestserver.com/Thankyou.aspx?id=" + ViewState["saleorderold"] + "");
            }
        }
        else if (Request.QueryString["id"] != null)
        {
            sendmailemail();
            Response.Redirect("http://paymentgateway.safestserver.com/Thankyou.aspx?cid=" + Request.QueryString["cid"] + "");
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
                insertStatus(Convert.ToInt32(Orderid), payment_status.ToString(), txn_id.ToString(), "before in Complete");
                try
                {
                    insertStatustesting(Orderid, txn_id, "", payment_status, "", "5");
                    //string orderid,  string txnid, string comp, string status, string emai, string step
                }
                catch (Exception ex)
                {
                }
                if (payment_status == "Completed")
                {
                    filldatas(Orderid, payment_status, txn_id);
                }
            }
        }
    }
    public void sendmailemail()
    {
        DataTable dt3 = select1("SELECT TOP(1) [ID]  ,[OriginalEmailID] ,[FirstName] ,[LastName] ,[PortalName] ,[Mailboxname]  ,[password] ,[domain] ,[doneor_failed],[Redirectionid] FROM [License.Busiwiz].[dbo].[MailBox_MailEnableCreation] Where ID='" + Request.QueryString["id"] + "' ");

        string str21 = "  select distinct  PortalMasterTbl.* from  CompanyMaster inner join PricePlanMaster  on CompanyMaster.PricePlanId=PricePlanMaster.PricePlanId inner join VersionInfoMaster on VersionInfoMaster.VersionInfoId=PricePlanMaster.VersionInfoMasterId inner join ProductMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId inner join ClientMaster on ProductMaster.ClientMasterId=ClientMaster.ClientMasterId inner join Priceplancategory on Priceplancategory.ID=PricePlanMaster.PriceplancatId inner join PortalMasterTbl on PortalMasterTbl.Id=Priceplancategory.PortalId   inner join OrderMaster on CompanyMaster.CompanyLoginId=OrderMaster.CompanyLoginId inner join  OrderPaymentSatus on OrderMaster.OrderId=OrderPaymentSatus.OrderId  inner join StateMasterTbl on StateMasterTbl.StateId=PortalMasterTbl.StateId inner join CountryMaster on StateMasterTbl.CountryId=CountryMaster.CountryId  WHERE(PortalMasterTbl.Id=7)  ";
        SqlCommand cmd45 = new SqlCommand(str21, con);
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
        if (dt21.Rows.Count > 0)
        {

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


            aa = "" + dt21.Rows[0]["Supportteammanagername"].ToString() + "- Support Manager";
            bb = "" + dt21.Rows[0]["PortalName"].ToString() + " ";
            cc = "" + dt21.Rows[0]["Supportteamphoneno"].ToString() + "  " + ext + " ";
            dd = "" + tollfree + " " + tollfreeext + " ";
            ee = "" + dt21.Rows[0]["Portalmarketingwebsitename"].ToString() + "";
            // ff = "" + dt21.Rows[0]["City"].ToString() + " " + dt21.Rows[0]["StateName"].ToString() + " " + dt21.Rows[0]["CountryName"].ToString() + " " + dt21.Rows[0]["Zip"].ToString() + " ";
        }

        string file = "job-center-logo-changes 33.png";
        string tomail = dt3.Rows[0]["OriginalEmailID"].ToString();
       // Label46.Text = dt3.Rows[0]["OriginalEmailID"].ToString();
        string username = "" + dt3.Rows[0]["Mailboxname"].ToString() + "@" + dt3.Rows[0]["domain"].ToString() + "";
        string canname = "" + dt3.Rows[0]["FirstName"].ToString() + " " + dt3.Rows[0]["LastName"].ToString() + "";
        string body1 = "<br>  <img src=\"http://members.ijobcenter.com/images/" + file + "\" \"border=\"0\" Width=\"90\" Height=\"80\" / > <br> Dear " + canname.ToString() + " <br><br>" +
        "You have successfully registered with ijobcenter.com Mail Account. Your login information is as follows <br><br>User ID: " + username.ToString() + " <br>Password: " + dt3.Rows[0]["password"].ToString() + " " +
         " <br><br>Please use this email as your default email for all operations from ijobcenter<br><br>If you would like to log into your account now you may do so by clicking  <a href=http://safestmail.net/Mondo/lang/sys/login.aspx target=_blank > here </a> ,or you may copy and paste the link below into your internet browser.<br><br>http://safestmail.net/Mondo/lang/sys/login.aspx <br><br><br>Thank you,<br><br>" + aa + "<br>" + bb + "<br>" + cc + "<br>" + ee + "";
        if (dt21.Rows.Count > 0)
        {
            try
            {
                string email = Convert.ToString(dt21.Rows[0]["UserIdtosendmail"]);
                string displayname = Convert.ToString("IJobCenter");
                string password = Convert.ToString(dt21.Rows[0]["Password"]);
                string outgo = Convert.ToString(dt21.Rows[0]["Mailserverurl"]);
                string body = body1;
                string Subject = "IJobCenter.com Email Registration";


                MailAddress to = new MailAddress(tomail);//(tomail);//info@ijobcenter.com("company12@safestmail.net");//
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
            catch
            {
            }
        }
    }
    public void insertStatustesting(string orderid, string txnid, string comp, string status, string emai, string step)
    {

        string str = "insert into PayPaltest(Orderid,TransactionId,CompID,status, email , step)values('" + orderid + "','" + txnid.ToString() + "','" + comp + "','" + status + "','" + emai + "', '" + step + "')";
        SqlCommand cmd = new SqlCommand(str, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd.ExecuteNonQuery();
        con.Close();
    }
    public void filldatas(string Orderid, string payment_status, string txn_id)
    {
        string Key = "";
        string HashKey = "";
        Key = CreateLicenceKey(out HashKey);

        string Encrrptionkey = "";
        //Encrrptionkey = CreateEncreptionKey(out HashKey);
         Encrrptionkey =MyCommonfile.RandomeIntnumber(20);
        ViewState["licensekey"] = Key;
        double ii = Convert.ToDouble(ViewState["mc_gross"]);

        string stri = "select * from paymentinfo where ClientMasterId='" + ViewState["saleorderold"] + "' and notifyurl IS NULL  ";//and amount='" + ii + "' 
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
            string str11 = " INSERT INTO LicenseMaster " +
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
            //NEW Plane --FreePlan 
            int flag = 0;
            string order = " select * from paymentinfo where ordermasterid='" + ViewState["saleorderold"] + "' and  ClientMasterId IS NULL";
            SqlCommand cmdor = new SqlCommand(order, con);
            SqlDataAdapter adpor = new SqlDataAdapter(cmdor);
            DataTable dtorder = new DataTable();
            adpor.Fill(dtorder);

            if (dtorder.Rows.Count > 0)
            {
                Session["CompId"] = dtorder.Rows[0]["compid"].ToString();
                string strpriceid = "SELECT PlanId from OrderMaster WHERE (OrderId = '" + ViewState["saleorderold"].ToString() + "')";
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
                string selli = " SELECT dbo.CompanyMaster.Encryptkeycomp,dbo.CompanyMaster.ServerId, dbo.ServerMasterTbl.Enckey FROM dbo.ServerMasterTbl INNER JOIN dbo.CompanyMaster ON dbo.ServerMasterTbl.Id = dbo.CompanyMaster.ServerId where CompanyLoginId='" + Session["CompId"].ToString()  + "' and (Encryptkeycomp='' OR Encryptkeycomp IS  NULL) ";
                selli = "SELECT dbo.CompanyMaster.Encryptkeycomp, dbo.CompanyMaster.ServerId FROM dbo.CompanyMaster WHERE  CompanyLoginId='" + Session["CompId"].ToString() + "' and (Encryptkeycomp = '' OR Encryptkeycomp IS NULL)";
                SqlDataAdapter adlic = new SqlDataAdapter(selli, con);
                DataTable dtlic = new DataTable();
                adlic.Fill(dtlic);
                if (dtlic.Rows.Count > 0)
                {
                    string update = " update CompanyMaster set PricePlanId='" + ViewState["priceplanid"] + "', active='True',OrderId='" + ViewState["saleorderold"] + "' ,Encryptkeycomp='" + Encrrptionkey + "' where CompanyLoginId='" + Session["CompId"] + "'";//,
                    SqlCommand upcom = new SqlCommand(update, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    upcom.ExecuteNonQuery();
                    con.Close();

                    string C1 = Encrrptionkey.Substring(0, 4) + MyCommonfile.RandomeIntnumber(16);
                    string C2 = Encrrptionkey.Substring(4, 4) + MyCommonfile.RandomeIntnumber(16);
                    string C3 = Encrrptionkey.Substring(8, 4) + MyCommonfile.RandomeIntnumber(16);
                    string C4 = Encrrptionkey.Substring(12, 4) + MyCommonfile.RandomeIntnumber(16);
                    string C5 = Encrrptionkey.Substring(16, 4) + MyCommonfile.RandomeIntnumber(16);
                    string SerId = "";
                    Boolean CABCM = CompKeyIns.Insert_CompanyABCMaster(Session["CompId"].ToString(), Encrrptionkey, C1, C2, C3, C4, C5, SerId);
                

                    //String SerKey = Encrrptionkey;
                    //string S1 = SerKey.Substring(0, 4) + MyCommonfile.RandomeIntnumber(16);
                    //string S2 = SerKey.Substring(4, 4) + MyCommonfile.RandomeIntnumber(16);
                    //string S3 = SerKey.Substring(8, 4) + MyCommonfile.RandomeIntnumber(16);
                    //string S4 = SerKey.Substring(12, 4) + MyCommonfile.RandomeIntnumber(16);
                    //string S5 = SerKey.Substring(16, 4) + MyCommonfile.RandomeIntnumber(16);
                    //Boolean SerCABCMM = CompKeyIns.Insert_CompanyABCMaster(Session["CompId"].ToString(), SerKey, S1, S2, S3, S4, S5, dtlic.Rows[0]["ServerId"].ToString());

                  
                }
                else
                {
                    //License Key Not Require-- 
                    string update = "update CompanyMaster set PricePlanId='" + ViewState["priceplanid"] + "', active='True',OrderId='" + ViewState["saleorderold"] + "'   where CompanyLoginId='" + Session["CompId"] + "'";//,
                    SqlCommand upcom = new SqlCommand(update, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    upcom.ExecuteNonQuery();
                    con.Close();
                 
                }

                string comid = " SELECT OrderMaster.OrderId,OrderMaster.OrderType,CompanyMaster.CompanyId,CompanyMaster.CompanyLoginId,OrderMasterDetail.PricePlanId from CompanyMaster inner join OrderMaster on OrderMaster.CompanyLoginId=CompanyMaster.CompanyLoginId  WHERE (CompanyMaster.CompanyLoginId = '" + Session["CompId"].ToString() + "') and (OrderMaster.OrderId = '" + ViewState["saleorderold"].ToString() + "')";
                comid = " SELECT dbo.OrderMaster.OrderId, dbo.OrderMaster.OrderType, dbo.CompanyMaster.CompanyId, dbo.CompanyMaster.CompanyLoginId, dbo.OrderMasterDetail.PricePlanId, dbo.Priceplancategory.ID FROM dbo.Priceplancategory INNER JOIN dbo.PricePlanMaster ON dbo.Priceplancategory.ID = dbo.PricePlanMaster.PriceplancatId INNER JOIN dbo.CompanyMaster INNER JOIN dbo.OrderMaster ON dbo.OrderMaster.CompanyLoginId = dbo.CompanyMaster.CompanyLoginId INNER JOIN dbo.OrderMasterDetail ON dbo.OrderMaster.OrderId = dbo.OrderMasterDetail.ordermasterid ON dbo.PricePlanMaster.PricePlanId = dbo.OrderMasterDetail.Priceplanid WHERE (CompanyMaster.CompanyLoginId = '" + Session["CompId"].ToString() + "') and (OrderMaster.OrderId = '" + ViewState["saleorderold"].ToString() + "') ";
                SqlDataAdapter adc = new SqlDataAdapter(comid, con);
                DataTable dtcc = new DataTable();
                adc.Fill(dtcc);
                if (dtcc.Rows.Count > 0)
                {
                    foreach (DataRow uyt in dtcc.Rows)
                    {
                        string sellii = "SELECT * from LicenseMaster where CompanyId='" + uyt["CompanyId"] + "' and PricePlanId='" + uyt["PricePlanId"] + "' ";
                        SqlDataAdapter adlici = new SqlDataAdapter(sellii, con);
                        DataTable dtlici = new DataTable();
                        adlici.Fill(dtlici);
                        if (dtlici.Rows.Count <= 0)
                        {
                            #region
                            //uyt["CodeVersion"].ToString() 
                            string str11 = " INSERT INTO LicenseMaster (SiteMasterId, CompanyId, LicenseKey, HashKey, LIcenseDate,LicensePeriod,PricePlanId) " +
                                       " VALUES ('1','" + uyt["CompanyId"] + "','" + Key + "','" + HashKey + "','" + DateTime.Now.ToShortDateString() + "'," + ViewState["month"] + ",'" + uyt["PricePlanId"] + "')";
                            SqlCommand cmd = new SqlCommand(str11, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmd.ExecuteNonQuery();
                            con.Close();
                            #endregion
                        }
                        else
                        {
                            #region
                            if (Convert.ToString(uyt["OrderType"]) == "Renue")
                            {
                                DateTime lastredate = Convert.ToDateTime(dtlic.Rows[0]["LIcenseDate"]).AddDays(Convert.ToInt32(dtlic.Rows[0]["LicensePeriod"]));
                                int remainrday = lastredate.Subtract(DateTime.Now).Days;
                                if (remainrday > 0)
                                {
                                    ViewState["month"] = (Convert.ToInt32(ViewState["month"]) + remainrday).ToString();
                                }
                            }
                            flag = 2;
                            string str11 = " Update LicenseMaster set LIcenseDate='" + DateTime.Now.ToShortDateString() + "',LicensePeriod=" + ViewState["month"] + " where CompanyId='" + uyt["CompanyId"] + "'";
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
                            //New For Lmaster
                            #region
                            SqlConnection servermasterconn = new SqlConnection();
                            Session["comid"] = uyt["CompanyLoginId"].ToString();
                            string strcomp = "select dbo.CompanyMaster.CompanyLoginId, ServerMasterTbl.*,CompanyMaster.Encryptkeycomp from CompanyMaster inner join ServerMasterTbl on ServerMasterTbl.Id=CompanyMaster.ServerId where CompanyMaster.CompanyLoginId='" + Convert.ToString(HttpContext.Current.Session["comid"]) + "'  ";
                            SqlCommand cmdcomp = new SqlCommand(strcomp, con);
                            DataTable dtcomp = new DataTable();
                            SqlDataAdapter adpcomp = new SqlDataAdapter(cmdcomp);
                            adpcomp.Fill(dtcomp);
                            if (dtcomp.Rows.Count > 0)
                            {
                                //  datasource = @"Data Source =" + dtcomp.Rows[0]["sqlurl"].ToString() + "\\" + dtcomp.Rows[0]["DefaultsqlInstance"].ToString() + "," + dtcomp.Rows[0]["Port"].ToString();
                                servermasterconn.ConnectionString = @"Data Source =" + dtcomp.Rows[0]["sqlurl"].ToString() + "\\" + dtcomp.Rows[0]["DefaultsqlInstance"].ToString() + "," + dtcomp.Rows[0]["Port"].ToString() + "; Initial Catalog = " + dtcomp.Rows[0]["DefaultDatabaseName"].ToString() + "; User ID=sa; Password=" + PageMgmt.Decrypted(dtcomp.Rows[0]["Sapassword"].ToString()) + "; Persist Security Info=true; ";
                                strkey = Convert.ToString(dtcomp.Rows[0]["Encryptkeycomp"]);
                                //Data Source =C3\C3SERVERMASTER,30000; Initial Catalog = C3SATELLITESERVER; User ID=sa; Password=06De1963++; Persist Security Info=true; 
                                string PricePlanIddd = encryptstrring(uyt["PricePlanId"].ToString());
                                //     string compiddd = encryptstrring(dtc.Rows[0]["CompanyId"].ToString());
                                string compiddd = encryptstrring(uyt["CompanyLoginId"].ToString());
                                try
                                {
                                    string str11orlm = "Update LMaster set LUPD='" + PricePlanIddd + "',AT='True' where  CID = '" + compiddd + "'";
                                    SqlCommand cmdordlm = new SqlCommand(str11orlm, servermasterconn);
                                    servermasterconn.Open();
                                    cmdordlm.ExecuteNonQuery();
                                    servermasterconn.Close();
                                }
                                catch (Exception ex)
                                {
                                }
                                //NEW CODE-------------------------------
                                string cid = Session["comid"].ToString();
                                string sid = dtcomp.Rows[0]["id"].ToString();
                                createwebsiteandattach(cid.ToString(), sid.ToString());
                                //--------------------------------------                           
                            }
                            #endregion
                            #endregion
                        }



                        string chkaper = "select * FROM PricePlanMaster WHERE PricePlanId='" + uyt["PricePlanId"] + "' and amountperOrder is NOT NULL and amountperOrder NOT IN('') and PayperOrderPlan='True'";
                        SqlDataAdapter adcheck = new SqlDataAdapter(chkaper, con);
                        DataTable dtcheck = new DataTable();
                        adcheck.Fill(dtcheck);
                        if (dtcheck.Rows.Count > 0)
                        {
                            #region
                            string chkse = "select * FROM PaypalordercustomerbalanceTbl WHERE CustomerID='" + uyt["CompanyLoginId"] + "' and PricePlanID='" + uyt["PricePlanId"] + "'";
                            SqlDataAdapter adse = new SqlDataAdapter(chkse, con);
                            DataTable dtse = new DataTable();
                            adse.Fill(dtse);
                            if (dtse.Rows.Count <= 0)
                            {
                                #region
                                totabala = Convert.ToInt32(dtcheck.Rows[0]["FreeIntialOrders"]);
                                netdo = (Convert.ToDecimal(ViewState["mc_gross"])) + totabala;
                                string inpayper = "INSERT INTO Payperorderplansubscriptiontbl " +
                                               " (priceplanid, CustomerID, AmountPaid, orderID) " +
                                               " VALUES     ('" + uyt["PricePlanId"] + "','" + uyt["CompanyLoginId"] + "','" + netdo + "','" + uyt["OrderId"] + "')";
                                SqlCommand cmdinpa = new SqlCommand(inpayper, con);
                                if (con.State.ToString() != "Open")
                                {
                                    con.Open();
                                }
                                cmdinpa.ExecuteNonQuery();
                                con.Close();
                                string inpaocb = "INSERT INTO PaypalordercustomerbalanceTbl " +
                                           " (priceplanid,CustomerID,Balance,Subdate) " +
                                           " VALUES('" + uyt["PricePlanId"] + "','" + uyt["CompanyLoginId"] + "','" + netdo + "','" + DateTime.Now.ToShortDateString() + "')";
                                SqlCommand cmpaocb = new SqlCommand(inpaocb, con);
                                if (con.State.ToString() != "Open")
                                {
                                    con.Open();
                                }
                                cmpaocb.ExecuteNonQuery();
                                con.Close();
                                #endregion
                            }
                            else
                            {
                                #region
                                double totalamount = 0;
                                double netamount = 0;
                                totalamount = Convert.ToDouble(ViewState["mc_gross"]);
                                netamount = Convert.ToDouble(dtse.Rows[0]["Balance"]);
                                netamount = netamount + totalamount;
                                string inpayper = "Update Payperorderplansubscriptiontbl Set AmountPaid='" + ViewState["mc_gross"] + "' where CustomerID='" + uyt["CompanyLoginId"] + "'";
                                SqlCommand cmdinpa = new SqlCommand(inpayper, con);
                                if (con.State.ToString() != "Open")
                                {
                                    con.Open();
                                }
                                cmdinpa.ExecuteNonQuery();
                                con.Close();
                                string inpaocb = "Update PaypalordercustomerbalanceTbl Set Balance='" + netamount + "',Subdate='" + DateTime.Now.ToShortDateString() + "' where CustomerID='" + uyt["CompanyLoginId"] + "'";
                                SqlCommand cmpaocb = new SqlCommand(inpaocb, con);
                                con.Open();
                                cmpaocb.ExecuteNonQuery();
                                con.Close();
                                #endregion
                            }
                            #endregion
                        }
                    }



                    insertStatus(Convert.ToInt32(Orderid), payment_status.ToString(), txn_id.ToString(), ViewState["mc_gross"].ToString());

                    if (flag == 0)
                    {
                        if (Request.QueryString["mail"] != null)
                        {
                            // sendmail(dtorder.Rows[0]["PaypalEmailId"].ToString());
                        }
                        else
                        {
                            sendmail(dtorder.Rows[0]["PaypalEmailId"].ToString());
                        }
                        try
                        {
                            if (Request.QueryString["comidauto"] != null)
                            {
                                if (Request.QueryString["mailid"] != null)
                                {
                                    // Response.Redirect("http://Mailmanager.busiwiz.com/WebEnable.aspx?ID=" + Request.QueryString["mailid"].ToString() + "&cid=" + Request.QueryString["cid"] + "");
                                }
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
            else
            {
                //Order Id not match
                Response.Redirect("http://paymentgateway.safestserver.com/Thankyou.aspx?Oid=Order Id not match");
                lbl_msg.Text = "Order Id not match"; 
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
        return Encrypt(strText, strkey);
    }

    public string Encrypted(string strText)
    {
        return Encrypt(strText, strkey);
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
        string str = "select distinct  ProductMaster.ProductId, ProductMaster.loginurlforuser, ClientMaster.*,PricePlanMaster.PricePlanName,CompanyMaster.PlanId,CompanyMaster.Adminid,CompanyMaster.Password,CompanyMaster.CompanyLoginId,StateMasterTbl.StateName,CountryMaster.CountryName,CompanyMaster.Email,CompanyMaster.CompanyName as cname  from  CompanyMaster inner join PricePlanMaster  on CompanyMaster.PricePlanId=PricePlanMaster.PricePlanId inner join VersionInfoMaster on VersionInfoMaster.VersionInfoId=PricePlanMaster.VersionInfoMasterId inner join ProductMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId inner join ClientMaster on ProductMaster.ClientMasterId=ClientMaster.ClientMasterId left join StateMasterTbl on StateMasterTbl.StateId=ClientMaster.StateId left join CountryMaster on StateMasterTbl.CountryId=CountryMaster.CountryId  WHERE(CompanyLoginId = '" + Session["CompId"].ToString() + "') ";
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

            string str1 = "select distinct PortalMasterTbl.EmailDisplayname,PortalMasterTbl.UserIdtosendmail,PortalMasterTbl.Password,PortalMasterTbl.Mailserverurl, PortalMasterTbl.LogoPath,PortalMasterTbl.Supportteammanagername,PortalMasterTbl.City,PortalMasterTbl.Zip,PortalMasterTbl.Supportteamemailid,PortalMasterTbl.PortalName,PortalMasterTbl.Portalmarketingwebsitename,PortalMasterTbl.Address1,PortalMasterTbl.Supportteamphoneno,PortalMasterTbl.Supportteamphonenoext,PortalMasterTbl.Tollfree,PortalMasterTbl.Tollfreeext,PortalMasterTbl.Fax, ProductMaster.ProductURL, ProductMaster.loginurlforuser, ClientMaster.*,PricePlanMaster.PricePlanName,CompanyMaster.PlanId,CompanyMaster.Adminid,CompanyMaster.Password,CompanyMaster.CompanyLoginId,StateMasterTbl.StateName,CountryMaster.CountryName  from  CompanyMaster inner join PricePlanMaster  on CompanyMaster.PricePlanId=PricePlanMaster.PricePlanId inner join VersionInfoMaster on VersionInfoMaster.VersionInfoId=PricePlanMaster.VersionInfoMasterId inner join ProductMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId inner join ClientMaster on ProductMaster.ClientMasterId=ClientMaster.ClientMasterId  inner join Priceplancategory on Priceplancategory.ID=PricePlanMaster.PriceplancatId inner join PortalMasterTbl on PortalMasterTbl.Id=Priceplancategory.PortalId left join StateMasterTbl on StateMasterTbl.StateId=PortalMasterTbl.StateId left join CountryMaster on StateMasterTbl.CountryId=CountryMaster.CountryId WHERE (CompanyLoginId = '" + Session["CompId"].ToString() + "') ";
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

                string stprder = "Select OrderPaymentSatus.* from OrderPaymentSatus inner join OrderMaster on OrderMaster.OrderId=OrderPaymentSatus.OrderId inner join CompanyMaster on CompanyMaster.CompanyLoginId=OrderMaster.CompanyLoginId where CompanyMaster.CompanyLoginId= '" + Session["CompId"].ToString() + "'";
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
                    string stplan11 = "select * from Payperorderplansubscriptiontbl where priceplanid='" + dt.Rows[0]["PlanId"].ToString() + "' and CustomerID='" + Session["CompId"].ToString() + "'";

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
                    strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Product Name: </td><td align=\"left\" style=\"width: 80%\"><a href=http://" + dt1.Rows[0]["Portalmarketingwebsitename"].ToString() + "  target=_blank >" + dt1.Rows[0]["PortalName"].ToString() + "</a></td></tr>");
                    strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Plan ID: </td><td align=\"left\" style=\"width: 80%\">" + dtplan.Rows[0]["PricePlanId"].ToString() + "</td></tr>");
                    strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Plan Name: </td><td align=\"left\" style=\"width: 80%\"><a href=http://license.busiwiz.com/Priceplancomparision.aspx?Id=2056&PN=" + dt1.Rows[0]["PortalName"].ToString() + "&type=Customer  target=_blank >" + dtplan.Rows[0]["CategoryName"].ToString() + " - " + dtplan.Rows[0]["PricePlanName"].ToString() + "</a></td></tr>");
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
                        client.Host = dt1.Rows[0]["Mailserverurl"].ToString(); //
                      //  client.Port = 587;
                        client.Send(objEmail);

                    }
                    catch (Exception e)
                    {
                        string struu1 = " insert into ClientOrderPaymentStatus(ClientMasterId,TransactionID)values('35','" + e.ToString() + "')";
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
        string str = "select distinct ProductMaster.loginurlforuser, ClientMaster.*,PricePlanMaster.PricePlanName,CompanyMaster.PlanId,CompanyMaster.Adminid,CompanyMaster.Password,CompanyMaster.CompanyLoginId,StateMasterTbl.StateName,CountryMaster.CountryName,CompanyMaster.Email  from  CompanyMaster inner join PricePlanMaster  on CompanyMaster.PricePlanId=PricePlanMaster.PricePlanId inner join VersionInfoMaster on VersionInfoMaster.VersionInfoId=PricePlanMaster.VersionInfoMasterId inner join ProductMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId inner join ClientMaster on ProductMaster.ClientMasterId=ClientMaster.ClientMasterId left join StateMasterTbl on StateMasterTbl.StateId=ClientMaster.StateId left join CountryMaster on StateMasterTbl.CountryId=CountryMaster.CountryId WHERE(CompanyLoginId = '" + Session["CompId"].ToString() + "') ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            string str1 = "select distinct ProductMaster.ProductURL, ProductMaster.loginurlforuser, ClientMaster.*,PricePlanMaster.PricePlanName,CompanyMaster.PlanId,CompanyMaster.Adminid,CompanyMaster.Password,CompanyMaster.CompanyLoginId,StateMasterTbl.StateName,CountryMaster.CountryName  from  CompanyMaster inner join PricePlanMaster  on CompanyMaster.PricePlanId=PricePlanMaster.PricePlanId inner join VersionInfoMaster on VersionInfoMaster.VersionInfoId=PricePlanMaster.VersionInfoMasterId inner join ProductMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId inner join ClientMaster on ProductMaster.ClientMasterId=ClientMaster.ClientMasterId left join StateMasterTbl on StateMasterTbl.StateId=ClientMaster.StateId left join CountryMaster on StateMasterTbl.CountryId=CountryMaster.CountryId WHERE(CompanyLoginId = '" + Session["CompId"].ToString() + "') ";
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

                string stprder = "Select OrderPaymentSatus.* from OrderPaymentSatus inner join OrderMaster on OrderMaster.OrderId=OrderPaymentSatus.OrderId inner join CompanyMaster on CompanyMaster.CompanyLoginId=OrderMaster.CompanyLoginId where CompanyMaster.CompanyLoginId= '" + Session["CompId"].ToString() + "'";

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
                    string stplan11 = "select * from Payperorderplansubscriptiontbl where priceplanid='" + dt.Rows[0]["PlanId"].ToString() + "' and CustomerID='" + Session["CompId"].ToString() + "'";

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
                string license = "select CompanyMaster.CompanyId,LicenseMaster.LicenseKey from CompanyMaster inner join LicenseMaster on LicenseMaster.CompanyId=CompanyMaster.CompanyId where CompanyLoginId='" + Session["CompId"].ToString() + "'";

                SqlCommand cmdl = new SqlCommand(license, con);
                SqlDataAdapter adl = new SqlDataAdapter(cmdl);
                DataTable dtl = new DataTable();
                adl.Fill(dtl);
                // &nbsp;&nbsp;&nbsp;<b>Your license number is :</b>" + dtl.Rows[0]["LicenseKey"].ToString() + "<br><br>
                string bodytext = "<span style=\"color: #F2618D\"><b>PLEASE NOTE :: You will have to configure your account before you start using the product. </span><a href=http://members.busiwiz.com/Companyconfigureinfo.aspx?comid=" + dt.Rows[0]["CompanyLoginId"].ToString() + " target=_blank >Please configure now by clicking here.</b> </a>";
                string sprk = "SELECT distinct CompanyId,PricePlanId,ProductMaster.ProductURL,BusiControllerMasterTBl.BusiControllerApplicationURL, BusiControllerMasterTBl.returnurlbisicontrol,BusiControllerMasterTBl.DatabaseName,BusiControllerMasterTBl.UserID,BusiControllerMasterTBl.Password,BusiControllerMasterTBl.Port,BusiControllerMasterTBl.DatabaseServerNameOrIp from CompanyMaster inner join  ProductMaster on CompanyMaster.ProductId=ProductMaster.ProductId inner join VersionInfoMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId inner join BusiControllerMasterTBl on BusiControllerMasterTBl.versioninfomasterid=VersionInfoMaster.VersionInfoId WHERE CompanyLoginId ='" + Session["CompId"].ToString() + "'";
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


                MailAddress to = new MailAddress(dt.Rows[0]["Email"].ToString());
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

        //New Code for select  randome int key (Above Code no need)
       
        HashKey = hashcode.ToUpper();
        return str;
    }
    private string RNGCharacterMaskENC()
    {
        int maxSize = 10;
        int minSize = 12;
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

            string stplan11 = "select * from Payperorderplansubscriptiontbl where priceplanid='" + dt.Rows[0]["PricePlanId"].ToString() + "' and CustomerID='" + Session["CompId"].ToString() + "'";
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
    protected void createwebsiteandattach(string compid, string sid)
    {

     
        string str = " SELECT CompanyMaster.*,PortalMasterTbl.PortalName,PricePlanMaster.VersionInfoMasterId,PricePlanMaster.Producthostclientserver from CompanyMaster inner join PricePlanMaster on PricePlanMaster.PricePlanId=CompanyMaster.PricePlanId inner join PortalMasterTbl on PricePlanMaster.PortalMasterId1 = PortalMasterTbl.id where CompanyLoginId='" + compid + "' ";

        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);

        if (ds.Rows.Count > 0)
        {

            string websitename = compid;
            verid = ds.Rows[0]["VersionInfoMasterId"].ToString();
            string priceplanid = ds.Rows[0]["PricePlanId"].ToString();
            string productid = ds.Rows[0]["ProductId"].ToString();
            string versionid = ds.Rows[0]["VersionInfoMasterId"].ToString();
            string portalname = ds.Rows[0]["PortalName"].ToString();

            ViewState["ownserver"] = ds.Rows[0]["Producthostclientserver"].ToString();

            string strserver = "";
            if (ViewState["ownserver"].ToString() == "True")
            {
                strserver = "select ServerMasterTbl.* from ServerMasterTbl inner join  CompanyMaster on CompanyMaster.CompanyLoginId=ServerMasterTbl.compid where CompanyMaster.CompanyLoginId='" + compid + "' and ServerMasterTbl.compid='" + compid + "'";
            }
            else
            {
                strserver = " SELECT ServerMasterTbl.* from ServerAssignmentMasterTbl inner join ServerMasterTbl on ServerMasterTbl.Id=ServerAssignmentMasterTbl.ServerId where ProductId='" + productid + "' and VersionId='" + versionid + "' and PricePlanId='" + priceplanid + "' and Active='1' ";

            }
            SqlCommand cmdserver = new SqlCommand(strserver, con);
            SqlDataAdapter adpserver = new SqlDataAdapter(cmdserver);
            DataTable dsserver = new DataTable();
            adpserver.Fill(dsserver);

            if (dsserver.Rows.Count > 0)
            {
                // folder creation for company

                // copmany default path
                sDomain = dsserver.Rows[0]["DomainName"].ToString() + "." + dsserver.Rows[0]["DomainGroupName"].ToString();
                sDefaultRootOU = "DC=" + dsserver.Rows[0]["DomainName"].ToString() + ", DC=" + dsserver.Rows[0]["DomainGroupName"].ToString();

                string defaultinstancename = dsserver.Rows[0]["DefaultsqlInstance"].ToString();
                string sqlserveurl = dsserver.Rows[0]["sqlurl"].ToString();
                string sqlservername = dsserver.Rows[0]["PublicIp"].ToString();
                string sqlinstancename = dsserver.Rows[0]["Sqlinstancename"].ToString();
                string sqlserverport = dsserver.Rows[0]["port"].ToString();
                string defaultdatabasename = dsserver.Rows[0]["DefaultDatabaseName"].ToString();
                ComputerName = dsserver.Rows[0]["ComputerName"].ToString();
                string Companymastersqlistance = dsserver.Rows[0]["PortforCompanymastersqlistance"].ToString();
                string iiswebsitepath = dsserver.Rows[0]["serverdefaultpathforiis"].ToString();
                string iismdfpath = dsserver.Rows[0]["serverdefaultpathformdf"].ToString();
                string iisldfpath = dsserver.Rows[0]["serverdefaultpathforfdf"].ToString();
                string domainuser = dsserver.Rows[0]["InDomain"].ToString();
                Serverencstr = dsserver.Rows[0]["Enckey"].ToString();

                // string sapassword = dsserver.Rows[0]["Sapassword"].ToString();
                string sapassword = PageMgmt.Decrypted(dsserver.Rows[0]["Sapassword"].ToString());

                condefaultinstance = new SqlConnection();
                condefaultinstance.ConnectionString = @"Data Source =" + sqlserveurl + "\\" + defaultinstancename + "," + sqlserverport + "; Initial Catalog = master; User ID=sa; Password=" + sapassword + "; Persist Security Info=true; ";

                if (condefaultinstance.State.ToString() != "Open")
                {
                    condefaultinstance.Open();//Settelite C3
                }
                condefaultinstance.Close();


                concompanyinstance = new SqlConnection();
                concompanyinstance.ConnectionString = @"Data Source =" + ComputerName + "\\" + sqlinstancename + "; Initial Catalog = master; User ID=sa; Password=" + sapassword + "; Persist Security Info=true; ";
                if (concompanyinstance.State.ToString() != "Open")
                {
                    concompanyinstance.Open();//Licemse
                }
                concompanyinstance.Close();


                condefaultdatabase = new SqlConnection();
                condefaultdatabase.ConnectionString = @"Data Source =" + sqlserveurl + "\\" + defaultinstancename + "," + sqlserverport + "; Initial Catalog = " + defaultdatabasename + "; User ID=sa; Password=" + sapassword + "; Persist Security Info=true; ";

                if (condefaultdatabase.State.ToString() != "Open")
                {
                    condefaultdatabase.Open();//sattelite
                }
                condefaultdatabase.Close();


          

                try
                {


                    // database attach
                    // Find All Oroduct code and versionno here ie. OADB , USERLOG, EXTMSGDB Etc
                    string strmaxiddatabase = "select ProductMasterCodeTbl.ProductVerID,ProductMasterCodeTbl.CodeTypeID,Max(ProductMasterCodeTbl.codeversionnumber) as codeversionnumber,ProductCodeDetailTbl.CodeTypeName  from ProductMasterCodeTbl inner join ProductMasterCodeonsatelliteserverTbl on ProductMasterCodeonsatelliteserverTbl.ProductMastercodeID=ProductMasterCodeTbl.ID inner join CodeTypeTbl on CodeTypeTbl.ID=ProductMasterCodeTbl.CodeTypeID inner join CodeTypeCategory on CodeTypeCategory.CodeMasterNo=CodeTypeTbl.CodeTypeCategoryId inner join ProductCodeDetailTbl on ProductCodeDetailTbl.Id=CodeTypeTbl.ProductCodeDetailId  where ProductMasterCodeTbl.ProductVerID='" + versionid + "' and ProductMasterCodeonsatelliteserverTbl.ServerID='" + sid + "' and ProductMasterCodeonsatelliteserverTbl.Successfullyuploadedtoserver='1' and CodeTypeCategory.CodeMasterNo In ('2') group by ProductMasterCodeTbl.ProductVerID,ProductMasterCodeTbl.CodeTypeID,ProductCodeDetailTbl.CodeTypeName";
                    SqlCommand cmdmaxiddatabase = new SqlCommand(strmaxiddatabase, con);
                    SqlDataAdapter adpmaxiddatabase = new SqlDataAdapter(cmdmaxiddatabase);
                    DataTable dsmaxiddatabase = new DataTable();
                    adpmaxiddatabase.Fill(dsmaxiddatabase);

                    if (dsmaxiddatabase.Rows.Count > 0)
                    {
                        foreach (DataRow drmaxdb in dsmaxiddatabase.Rows)
                        {

                            string strmastersourcepath = " select ProductMasterCodeTbl.ProductVerID,ProductMasterCodeTbl.CodeTypeID,ProductMasterCodeTbl.codeversionnumber,ProductMasterCodeonsatelliteserverTbl.Physicalpath,ProductMasterCodeonsatelliteserverTbl.filename,ProductCodeDetailTbl.CodeTypeName,CodeTypeTbl.CodeTypeCategoryId ,ProductCodeDetailTbl.AdditionalPageInserted,ProductCodeDetailTbl.BusiwizSynchronization,ProductCodeDetailTbl.CompanyDefaultData from ProductMasterCodeTbl inner join ProductMasterCodeonsatelliteserverTbl on ProductMasterCodeonsatelliteserverTbl.ProductMastercodeID=ProductMasterCodeTbl.ID inner join CodeTypeTbl on CodeTypeTbl.ID=ProductMasterCodeTbl.CodeTypeID inner join ProductCodeDetailTbl on ProductCodeDetailTbl.Id=CodeTypeTbl.ProductCodeDetailId where ProductMasterCodeTbl.ProductVerID='" + drmaxdb["ProductVerID"].ToString() + "' and ProductMasterCodeTbl.CodeTypeID='" + drmaxdb["CodeTypeID"].ToString() + "' and ProductMasterCodeTbl.codeversionnumber='" + drmaxdb["codeversionnumber"].ToString() + "' and ProductMasterCodeonsatelliteserverTbl.ServerID='" + sid + "' and ProductMasterCodeonsatelliteserverTbl.Successfullyuploadedtoserver='1'";
                            SqlCommand cmdmastersourcepath = new SqlCommand(strmastersourcepath, con);
                            SqlDataAdapter adpmastersourcepath = new SqlDataAdapter(cmdmastersourcepath);
                            DataTable dsmastersourcepath = new DataTable();
                            adpmastersourcepath.Fill(dsmastersourcepath);


                            if (dsmastersourcepath.Rows.Count > 0)
                            {

                                string mastersourcepath = dsmastersourcepath.Rows[0]["Physicalpath"].ToString();
                                string filename = dsmastersourcepath.Rows[0]["filename"].ToString();
                                string filexten = Path.GetExtension(filename);
                                string CodeTypeName = dsmastersourcepath.Rows[0]["CodeTypeName"].ToString();
                                string ldffilename = "";
                                string ldfmastersourcepath = "";
                                string databasename = compid + "." + CodeTypeName;
                                string busiwizsyncronise = dsmastersourcepath.Rows[0]["BusiwizSynchronization"].ToString();
                                string OAdefaultvaluse = dsmastersourcepath.Rows[0]["CompanyDefaultData"].ToString();
                                string codetypeid = dsmastersourcepath.Rows[0]["CodeTypeID"].ToString();
                                string codeversionno = dsmastersourcepath.Rows[0]["codeversionnumber"].ToString();

                                if (filexten == ".MDF" || filexten == ".mdf")
                                {
                                    // get ldf file name
                                    string strfindldf = " select ProductMasterCodeTbl.ProductVerID,ProductMasterCodeTbl.CodeTypeID,Max(ProductMasterCodeTbl.codeversionnumber) as codeversionnumber,ProductCodeDetailTbl.CodeTypeName  from ProductMasterCodeTbl inner join ProductMasterCodeonsatelliteserverTbl on ProductMasterCodeonsatelliteserverTbl.ProductMastercodeID=ProductMasterCodeTbl.ID inner join CodeTypeTbl on CodeTypeTbl.ID=ProductMasterCodeTbl.CodeTypeID inner join CodeTypeCategory on CodeTypeCategory.CodeMasterNo=CodeTypeTbl.CodeTypeCategoryId inner join ProductCodeDetailTbl on ProductCodeDetailTbl.Id=CodeTypeTbl.ProductCodeDetailId  where ProductMasterCodeTbl.ProductVerID='" + versionid + "' and ProductMasterCodeonsatelliteserverTbl.ServerID='" + sid + "' and ProductMasterCodeonsatelliteserverTbl.Successfullyuploadedtoserver='1' and CodeTypeCategory.CodeMasterNo In ('2') and ProductCodeDetailTbl.CodeTypeName='" + CodeTypeName + "' group by ProductMasterCodeTbl.ProductVerID,ProductMasterCodeTbl.CodeTypeID,ProductCodeDetailTbl.CodeTypeName";
                                    SqlCommand cmdfindldf = new SqlCommand(strfindldf, con);
                                    SqlDataAdapter adpfindldf = new SqlDataAdapter(cmdfindldf);
                                    DataTable dsfindldf = new DataTable();
                                    adpfindldf.Fill(dsfindldf);

                                    if (dsfindldf.Rows.Count > 0)
                                    {

                                        foreach (DataRow drldffile in dsfindldf.Rows)
                                        {

                                            string strldffilecomp = " select ProductMasterCodeTbl.ProductVerID,ProductMasterCodeTbl.CodeTypeID,ProductMasterCodeTbl.codeversionnumber,ProductMasterCodeonsatelliteserverTbl.Physicalpath,ProductMasterCodeonsatelliteserverTbl.filename,ProductCodeDetailTbl.CodeTypeName,CodeTypeTbl.CodeTypeCategoryId ,ProductCodeDetailTbl.AdditionalPageInserted from ProductMasterCodeTbl inner join ProductMasterCodeonsatelliteserverTbl on ProductMasterCodeonsatelliteserverTbl.ProductMastercodeID=ProductMasterCodeTbl.ID inner join CodeTypeTbl on CodeTypeTbl.ID=ProductMasterCodeTbl.CodeTypeID inner join ProductCodeDetailTbl on ProductCodeDetailTbl.Id=CodeTypeTbl.ProductCodeDetailId where ProductMasterCodeTbl.ProductVerID='" + drldffile["ProductVerID"].ToString() + "' and ProductMasterCodeTbl.CodeTypeID='" + drldffile["CodeTypeID"].ToString() + "' and ProductMasterCodeTbl.codeversionnumber='" + drldffile["codeversionnumber"].ToString() + "' and ProductMasterCodeonsatelliteserverTbl.ServerID='" + sid + "' and ProductMasterCodeonsatelliteserverTbl.Successfullyuploadedtoserver='1'";
                                            SqlCommand cmdldffilecomp = new SqlCommand(strldffilecomp, con);
                                            SqlDataAdapter adpldffilecomp = new SqlDataAdapter(cmdldffilecomp);
                                            DataTable dsldffilecomp = new DataTable();
                                            adpldffilecomp.Fill(dsldffilecomp);

                                            if (dsldffilecomp.Rows.Count > 0)
                                            {

                                                string getldffilename = dsldffilecomp.Rows[0]["filename"].ToString();
                                                string ldffilexten = Path.GetExtension(getldffilename);

                                                if (ldffilexten == ".LDF" || ldffilexten == ".ldf")
                                                {
                                                    ldffilename = dsldffilecomp.Rows[0]["filename"].ToString();
                                                    ldfmastersourcepath = dsldffilecomp.Rows[0]["Physicalpath"].ToString();
                                                }


                                            }


                                        }
                                    }
                                    // end get ldf file name

                                    if (filename.ToString() != "" && ldffilename != "")
                                    {

                                        string tocopymdffile = iiswebsitepath + "\\" + websitename + "\\" + filename;
                                        string tocopyldffile = iiswebsitepath + "\\" + websitename + "\\" + ldffilename;


                                        if (busiwizsyncronise == "True")
                                        {
                                            //create table in Databases
                                            insert(databasename, sqlinstancename, sqlserverport, "", sapassword, databasename, compid, macaddresss, "", sqlserveurl);
                                        }

                                        if (OAdefaultvaluse == "True")
                                        {
                                            insertcompanymaster(compid, sqlserveurl, sqlinstancename, sqlserverport, databasename, sapassword);
                                        }



                                    }
                                }


                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                }
                // end database attach


                


               
               
            

            }


        }

    }


    protected void insert(string Url, string instancename, string port, string userid, string password, string Databasename, string compid, string macaddress, string computername, string sqlservername)
    {

        conn = new SqlConnection();
        conn.ConnectionString = @"Data Source =" + ComputerName + "\\" + instancename + "; Initial Catalog = " + Databasename + "; User ID=Sa; password=" + password + ";  Persist Security Info=true; ";

        SqlCommand cmdsel = new SqlCommand("SELECT CompanyMaster.CompanyId,CompanyMaster.AdminId,CompanyMaster.Password, CompanyMaster.Websiteurl,CompanyMaster.CompanyLoginId,CompanyMaster.active, PricePlanMaster.CheckIntervalDays, PricePlanMaster.GraceDays, ProductMaster.ProductURL,ProductMaster.ProductName,  " +
                      "CompanyMaster.ProductId, CompanyMaster.PricePlanId, PricePlanMaster.StartDate, PricePlanMaster.EndDate, PricePlanMaster.TrafficinGB,  " +
                      "PricePlanMaster.MaxUser, ProductDetail.VersionNo,ProductMaster.ProductURL,PricePlanMaster.TotalMail, PricePlanMaster.GBUsage,VersionInfoMaster.VersionInfoId ,dbo.PricePlanMaster.PortalMasterId1 " +
                      "FROM         ProductDetail INNER JOIN " +
                      "ProductMaster ON ProductDetail.ProductId = ProductMaster.ProductId RIGHT OUTER JOIN " +
                      "CompanyMaster ON ProductMaster.ProductId = CompanyMaster.ProductId LEFT OUTER JOIN  " +
                      "PricePlanMaster ON CompanyMaster.PricePlanId = PricePlanMaster.PricePlanId inner join VersionInfoMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId  " +
                      " WHERE     (CompanyMaster.CompanyLoginId = '" + compid + "')", con);

        SqlDataAdapter dtpsel = new SqlDataAdapter(cmdsel);
        DataTable dt1 = new DataTable();
        dtpsel.Fill(dt1);

        if (dt1.Rows.Count > 0)
        {

            string uid = Encrypted(dt1.Rows[0]["AdminId"].ToString());
            string pass = Encrypted(dt1.Rows[0]["Password"].ToString());
            string compidlmaster = Encrypted(dt1.Rows[0]["CompanyLoginId"].ToString());
            string prodid = Encrypted(dt1.Rows[0]["ProductId"].ToString());
            string proname = Encrypted(dt1.Rows[0]["ProductName"].ToString());
            string chdint = Encrypted(dt1.Rows[0]["CheckIntervalDays"].ToString());
            string gradays = Encrypted(dt1.Rows[0]["GraceDays"].ToString());
            string runcou = Encrypted(Convert.ToString(1));
            string StartDate = Encrypted(dt1.Rows[0]["StartDate"].ToString());
            string EndDate = Encrypted(dt1.Rows[0]["EndDate"].ToString());
            string MaxUser = Encrypted(dt1.Rows[0]["MaxUser"].ToString());
            string VersionNo = Encrypted(dt1.Rows[0]["VersionNo"].ToString());
            string TotalMail = Encrypted(dt1.Rows[0]["TotalMail"].ToString());
            string GBUsage = Encrypted(dt1.Rows[0]["GBUsage"].ToString());
            string TrafficinGB = Encrypted(dt1.Rows[0]["TrafficinGB"].ToString());
            string PricePlanId = Encrypted(dt1.Rows[0]["PricePlanId"].ToString());
            string VersionInfoId = dt1.Rows[0]["VersionInfoId"].ToString();
            string PortalMasterId1 = Encrypted(dt1.Rows[0]["PortalMasterId1"].ToString());


            SqlCommand cmdnew = new SqlCommand("SELECT LicenseMaster.CompanyId, LicenseMaster.LicenseKey,LicenseMaster.LicenseDate,LicenseMaster.LicensePeriod,CompanyMaster.CompanyName, CompanyMaster.AdminId, CompanyMaster.Password,CompanyMaster.Websiteurl,CompanyMaster.active, " +
                        " HostDetail.SqlServerName, HostDetail.SqlServerUName, HostDetail.SqlServerUPassword, HostDetail.DatabaseName " +
                        " FROM CompanyMaster LEFT OUTER JOIN " +
                        " HostDetail ON CompanyMaster.CompanyId = HostDetail.CompanyId LEFT OUTER JOIN " +
                        " LicenseMaster ON CompanyMaster.CompanyId = LicenseMaster.CompanyId where  CompanyMaster.AdminId ='" + dt1.Rows[0]["AdminId"].ToString() + "' and CompanyMaster.Password='" + dt1.Rows[0]["Password"].ToString() + "' and CompanyMaster.CompanyLoginId='" + compid + "'  and CompanyMaster.active='1'", con);
            SqlDataAdapter dtp = new SqlDataAdapter(cmdnew);
            DataTable dtnew = new DataTable();
            dtp.Fill(dtnew);

            if (dtnew.Rows.Count > 0)
            {
                string LicenseDate = Encrypted(dtnew.Rows[0]["LicenseDate"].ToString());
                string LicensePeriod = Encrypted(dtnew.Rows[0]["LicensePeriod"].ToString());
                string lickey = Encrypted(dtnew.Rows[0]["LicenseKey"].ToString());
                try
                {
                    SqlCommand cmdins = new SqlCommand(" Update LMaster SET  LK='" + lickey + "',UID='" + uid + "',PWD='" + pass + "',AT='" + dt1.Rows[0]["active"].ToString() + "' ,PID='" + prodid + "',PN='" + proname + "',CHKINTDAYS='" + chdint + "',GPRDDAYS='" + gradays + "',LCHKDT='" + System.DateTime.Now.ToShortDateString() + "',RUNTOT='" + runcou + "',TRSD='" + StartDate + "' ,TRED='" + EndDate + "',ATINGB='" + GBUsage + "',TRGBPRD='" + TrafficinGB + "',TU='" + MaxUser + "',V='" + VersionNo + "',MP='" + TotalMail + "',LUPD='" + PricePlanId + "', LSD='" + LicenseDate + "', LP='" + LicensePeriod + "', VN='" + VersionInfoId + "' ,PORTMT='" + PortalMasterId1 + "' WHERE CID='" + compidlmaster + "'  ", condefaultdatabase);
                    if (conn.State.ToString() != "Open")
                    {
                        conn.Open();
                    }
                    cmdins.ExecuteNonQuery();
                    conn.Close();
                }
                catch (Exception ex)
                {
                } 
               

                //Insert into Satellight Server


                SqlCommand cmdinsserverdb = new SqlCommand(" Update LMaster SET  LK='" + lickey + "',UID='" + uid + "',PWD='" + pass + "',AT='" + dt1.Rows[0]["active"].ToString() + "' ,PID='" + prodid + "',PN='" + proname + "', CHKINTDAYS='" + chdint + "',GPRDDAYS='" + gradays + "',LCHKDT='" + System.DateTime.Now.ToShortDateString() + "',RUNTOT='" + runcou + "',TRSD='" + StartDate + "' ,TRED='" + EndDate + "',ATINGB='" + GBUsage + "',TRGBPRD='" + TrafficinGB + "',TU='" + MaxUser + "',V='" + VersionNo + "',MP='" + TotalMail + "',LUPD='" + PricePlanId + "', LSD='" + LicenseDate + "', LP='" + LicensePeriod + "', VN='" + VersionInfoId + "' ,PORTMT='" + PortalMasterId1 + "' WHERE CID='" + compidlmaster + "'  ", condefaultdatabase);
                if (condefaultdatabase.State.ToString() != "Open")
                {
                    condefaultdatabase.Open();
                }
                cmdinsserverdb.ExecuteNonQuery();
                condefaultdatabase.Close();


                
               


            }

        }


        string CID = compid;

        tableins("WebsiteMaster");
        WEBSITE(verid, CID);
        tableins("WebsiteSection");
        Section(verid, CID);
        tableins("MasterPageMaster");
        Masterpage(verid, CID);
        tableins("MainMenuMaster");
        MainManu(verid, CID);
        tableins("SubMenuMaster");
        SubManu(verid, CID);
        tableins("PageMaster");
        Pagename(verid, CID);
        tableins("pageplaneaccesstbl");
        Pageaccess(verid, CID);
        tableins("Control_type_Master");
        Controltype(verid, CID);
        tableins("PageControlMaster");
        PageControltype(verid, CID);
        tableins("ClientProductTableMaster");
        ClientProductTableMaster(verid, CID);
        tableins("ClientProductRecordsAllowed");
        ClientProductRecordsAllowed(verid, CID);
        tableins("SetupWizardMaster");
        SetupWizardMaster(verid, CID);
        tableins("SetupWizardMasterwithPortal");
        SetupWizardMasterwithPortal(verid, CID);
        tableins("SetupwizardDetail");
        SetupwizardDetail(verid, CID);
        tableins("SetupWizardDetailwithPortal");
        SetupWizardDetailwithPortal(verid, CID);
        tableins("setupwizardquestion");
        setupwizardquestion(verid, CID);
        tableins("setupwizardquestionPortal");
        setupwizardquestionPortal(verid, CID);
        tableins("SateliteServerRequiringSynchronisationMasterTbl");
        SatelliteSynchronise(verid);
        tableins("tablefielddetail");
        tablefielddetail(verid, CID);
        tableins("PriceplanrestrictionTbl");
        PriceplanrestrictionTbl(verid, CID);
        tableins("Priceplanrestrecordallowtbl");
        Priceplanrestrecordallowtbl(verid, CID);
        tableins("PortaldesignationTbl");
        PortaldesignationTbl(verid, CID);
        tableins("PortaldesignationDetailTbl");
        PortaldesignationDetailTbl(verid, CID);

    }

    protected void insertcompanymaster(string compid, string servername, string sqlinctancename, string sqlserverport, string databasename, string sapassword)
    {


        string strclnd = "Select *  from CompanyMaster where CompanyLoginId='" + compid + "' ";
        SqlCommand cmdclnd = new SqlCommand(strclnd, con);
        DataTable dtclnd = new DataTable();
        SqlDataAdapter adpclnd = new SqlDataAdapter(cmdclnd);
        adpclnd.Fill(dtclnd);

        if (dtclnd.Rows.Count > 0)
        {
            // coninfinal.ConnectionString = @"Data Source =" + servername + "\\" + sqlinctancename + "," + sqlserverport + "; Initial Catalog = " + databasename + "; Integrated Security = true";
            coninfinal.ConnectionString = @"Data Source =" + ComputerName + "\\" + sqlinctancename + "; Initial Catalog = " + databasename + "; User ID=Sa; password=" + sapassword + "; Persist Security Info=true; ";

            if (coninfinal.State.ToString() != "Open")
            {
                coninfinal.Open();
            }
            coninfinal.Close();
        }



        string strcitistate = "SELECT * from CompanyMaster where CompanyLoginId='" + compid + "'";
        SqlCommand cmdcitystate = new SqlCommand(strcitistate, con);
        DataTable dtcitystate = new DataTable();
        SqlDataAdapter adpcitystate = new SqlDataAdapter(cmdcitystate);
        adpcitystate.Fill(dtcitystate);



        string onlinecountyid = "";
        string onlinestateid = "";
        string onlinecityid = "";
        string licencestatename = "";
        string licensestateid = "";
        string eplazastateid = "";
        string licensecity = "";
        string eplazacityid = "";

        if (dtcitystate.Rows.Count > 0)
        {




            string strcountry = dtcitystate.Rows[0]["countryId"].ToString(); //licence country id
            licensecity = dtcitystate.Rows[0]["city"].ToString(); //licence city name
            if (strcountry != "")
            {
                string strcounrty = "SELECT * from CountryMaster where CountryId='" + strcountry + "' ";
                SqlCommand cmdcountry = new SqlCommand(strcounrty, con);
                DataTable dtcountry = new DataTable();
                SqlDataAdapter adpcountry = new SqlDataAdapter(cmdcountry);
                adpcountry.Fill(dtcountry);
                if (dtcountry.Rows.Count > 0)
                {

                    if (dtcountry.Rows[0]["CountryName"].ToString() != "")
                    {
                        string stronlinecounrty = "SELECT * from CountryMaster where CountryName='" + dtcountry.Rows[0]["CountryName"].ToString() + "' ";

                        SqlCommand cmdonlinecountry = new SqlCommand(stronlinecounrty, coninfinal);
                        DataTable dtonlinecountry = new DataTable();
                        SqlDataAdapter adponlinecountry = new SqlDataAdapter(cmdonlinecountry);
                        adponlinecountry.Fill(dtonlinecountry);

                        if (dtonlinecountry.Rows.Count > 0)
                        {
                            if (dtonlinecountry.Rows[0]["CountryId"].ToString() != "")
                            {
                                onlinecountyid = dtonlinecountry.Rows[0]["CountryId"].ToString(); //eplaza country id


                                if (dtcitystate.Rows[0]["countryId"].ToString() != "")
                                {
                                    licensestateid = dtcitystate.Rows[0]["StateId"].ToString();



                                    string strstate12 = "SELECT * from StateMasterTbl where CountryId='" + strcountry + "' and StateId='" + licensestateid + "' ";
                                    SqlCommand cmdstate12 = new SqlCommand(strstate12, con);
                                    DataTable dtstate12 = new DataTable();
                                    SqlDataAdapter adpstate12 = new SqlDataAdapter(cmdstate12);
                                    adpstate12.Fill(dtstate12);

                                    if (dtstate12.Rows.Count > 0)
                                    {
                                        licencestatename = dtstate12.Rows[0]["StateName"].ToString();

                                        string strstate12121 = "SELECT * from StateMasterTbl where CountryId='" + onlinecountyid + "' and StateName='" + licencestatename + "' ";
                                        SqlCommand cmdstate12121 = new SqlCommand(strstate12121, coninfinal);
                                        DataTable dtstate12121 = new DataTable();
                                        SqlDataAdapter adpstate12121 = new SqlDataAdapter(cmdstate12121);
                                        adpstate12121.Fill(dtstate12121);

                                        if (dtstate12121.Rows.Count > 0)
                                        {
                                            eplazastateid = dtstate12121.Rows[0]["StateId"].ToString();// Eplaza State Id

                                            string strstate12121789 = "SELECT * from CityMasterTbl where StateId='" + eplazastateid + "' and CityName='" + licensecity + "' ";
                                            SqlCommand cmdstate12121789 = new SqlCommand(strstate12121789, coninfinal);
                                            DataTable dtstate12121789 = new DataTable();
                                            SqlDataAdapter adpstate12121789 = new SqlDataAdapter(cmdstate12121789);
                                            adpstate12121789.Fill(dtstate12121789);

                                            if (dtstate12121789.Rows.Count > 0)
                                            {
                                                eplazacityid = dtstate12121789.Rows[0]["CityId"].ToString();

                                            }

                                        }

                                    }

                                }



                            }

                        }


                    }
                }



            }

        }


        //SELECT * from CompanyMaster where CompanyLoginId='" + compid + "'


        string strcln111 = "SELECT * from CompanyMaster where CompanyLoginId='" + compid + "'";
        SqlCommand cmdcln111 = new SqlCommand(strcln111, con);
        DataTable dtcln111 = new DataTable();
        SqlDataAdapter adpcln111 = new SqlDataAdapter(cmdcln111);
        adpcln111.Fill(dtcln111);

        if (dtcln111.Rows.Count > 0)
        {
            string compnayname = dtcln111.Rows[0]["CompanyName"].ToString();
            string AdminName = dtcln111.Rows[0]["ContactPerson"].ToString();
            string ContactPersonDesignation = dtcln111.Rows[0]["ContactPersonDesignation"].ToString();
            string PaypalEmailId = dtcln111.Rows[0]["paypalid"].ToString();
            string CompanyMainWebsite = dtcln111.Rows[0]["CompanyWebsite"].ToString();

            string Address = dtcln111.Rows[0]["Address"].ToString();
            string phone = dtcln111.Rows[0]["phone"].ToString();

            string Email = dtcln111.Rows[0]["Email"].ToString();
            string fax = dtcln111.Rows[0]["fax"].ToString();
            string pincode = dtcln111.Rows[0]["pincode"].ToString();



            int ddt = Convert.ToInt32(DateTime.Now.Year.ToString());
            int dde = ddt + 1;

            string strin = "insert into CompanyMaster(CompanyName,AdminName,PaypalEmailId,Compid,YearEndingDate,StartDateOfAccountYear,CompanyMainWebsite)values('" + compnayname + "','" + AdminName + "','" + PaypalEmailId + "', '" + compid + "','3/31/" + dde + "','4/1/" + ddt + "','" + CompanyMainWebsite + "')";
            SqlCommand cmdoin = new SqlCommand(strin, coninfinal);
            coninfinal.Open();
            cmdoin.ExecuteNonQuery();
            coninfinal.Close();
            string strs = "Select CompanyId from CompanyMaster where   Compid='" + compid + "'";
            SqlCommand cmds = new SqlCommand(strs, coninfinal);
            coninfinal.Open();
            object cid = cmds.ExecuteScalar();
            coninfinal.Close();

            string strinn = "insert into CompanyAddressMaster(CompanyName,ContactPersonNAme,ContactPersonDesignation,WebSite,CompanyMasterId,Address1,Phone,Country,State,Email,Fax,City,PinCode)values('" + compnayname + "','" + AdminName + "','" + ContactPersonDesignation + "','" + CompanyMainWebsite + "', '" + cid.ToString() + "','" + Address + "','" + phone + "','" + onlinecountyid + "','" + eplazastateid + "','" + Email + "','" + fax + "','" + eplazacityid + "','" + pincode + "')";
            SqlCommand cmdoinn = new SqlCommand(strinn, coninfinal);
            coninfinal.Open();
            cmdoinn.ExecuteNonQuery();
            coninfinal.Close();

        }

    }


    protected void tableins(string tablename)
    {
        string st1 = "CREATE TABLE " + tablename + "(";

        DataTable dts1 = selectBZ("select column_name,data_type,CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.COLUMNS where table_name='" + tablename + "'");
        for (int k = 0; k < dts1.Rows.Count; k++)
        {
            if (k == 0)
            {
                //st1 += ("" + dts1.Rows[k]["column_name"] + " int Identity(1,1),");
                st1 += ("" + dts1.Rows[k]["column_name"] + " nvarchar(500),");
            }
            else
            {
                st1 += ("" + dts1.Rows[k]["column_name"] + " " + dts1.Rows[k]["data_type"] + "(" + dts1.Rows[k]["CHARACTER_MAXIMUM_LENGTH"] + "),");

            }
        }
        st1 = st1.Remove(st1.Length - 1);
        st1 += ")";
        //st1 = st1.Replace("int()", "int");
        st1 = st1.Replace("bigint()", "nvarchar(500)");
        st1 = st1.Replace("int()", "nvarchar(500)");
        st1 = st1.Replace("(-1)", "(MAX)");
        st1 = st1.Replace("datetime()", "nvarchar(500)");
        st1 = st1.Replace("nvarchar(50)", "nvarchar(500)");
        st1 = st1.Replace("decimal()", "nvarchar(500)");
        st1 = st1.Replace("decimal", "nvarchar(500)");
        st1 = st1.Replace("bit()", "bit");
        DataTable dts = select("select column_name,data_type,CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.COLUMNS where table_name='" + tablename + "'");
        if (dts.Rows.Count <= 0)
        {
            SqlCommand cmdr = new SqlCommand(st1, conn);
            conn.Open();
            cmdr.ExecuteNonQuery();
            conn.Close();
        }
        else
        {
            string strBC = "CREATE TABLE " + tablename + "(";
            DataTable DTBC = select("select column_name,data_type,CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.COLUMNS where table_name='" + tablename + "'");
            for (int k = 0; k < DTBC.Rows.Count; k++)
            {
                if (k == 0)
                {
                    //  strBC += ("" + DTBC.Rows[k]["column_name"] + " int Identity(1,1),");
                    strBC += ("" + DTBC.Rows[k]["column_name"] + " nvarchar(500),");
                }
                else
                {
                    strBC += ("" + DTBC.Rows[k]["column_name"] + " " + DTBC.Rows[k]["data_type"] + "(" + DTBC.Rows[k]["CHARACTER_MAXIMUM_LENGTH"] + "),");

                }
            }
            strBC = strBC.Remove(strBC.Length - 1);
            strBC += ")";
            strBC = strBC.Replace("bigint()", "nvarchar(500)");
            strBC = strBC.Replace("int()", "nvarchar(500)");
            strBC = strBC.Replace("(-1)", "(MAX)");
            strBC = strBC.Replace("datetime()", "nvarchar(500)");
            st1 = st1.Replace("nvarchar(50)", "nvarchar(500)");
            st1 = st1.Replace("decimal()", "nvarchar(500)");
            st1 = st1.Replace("decimal", "nvarchar(500)");
            strBC = strBC.Replace("bit()", "bit");
            if (strBC != st1)
            {
                SqlCommand cmdrX = new SqlCommand("Drop table " + tablename, conn);
                conn.Open();
                cmdrX.ExecuteNonQuery();
                conn.Close();
                SqlCommand cmdr = new SqlCommand(st1, conn);
                conn.Open();
                cmdr.ExecuteNonQuery();
                conn.Close();
            }
            else
            {
                SqlCommand cmdrX = new SqlCommand("Delete  from  " + tablename, conn);
                conn.Open();
                cmdrX.ExecuteNonQuery();
                conn.Close();
            }
        }

    }


    protected void WEBSITE(string verid, string cid)
    {
        string Temp3val = "";
        string Temp2 = " INSERT INTO WebsiteMaster(ID,WebsiteName,WebsiteUrl,WebsitePort,IISServerIpUrl,IISServerUserId,IISServerPassWord,DatabaseName," +
                " DatabaseServerIpUrl,DatabaseUserId,DatabasePassword,BusiControllerName,BusiControllerDatabaseName,BusiControllerSqlServerIpUrl,BusiControllerUserId,BusiControllerPassword,BusiControllerConnectionString," +
                " FTP_Url,FTP_Port,FTP_UserId,FTP_Password,VersionInfoId,IISAccessPort,DatabaseAccessPort,BusiControllerPort,compid,FTPWorkGuideUrl,FTPWorkGuidePort,FTPWorkGuideUserId,FTPWorkGuidePW)Values ";
        DataTable dtr = selectBZ(" Select * from  WebsiteMaster  where [VersionInfoId]='" + verid + "' ");
        foreach (DataRow itm in dtr.Rows)
        {
            if (Temp3val.Length > 0)
            {
                Temp3val += ",";
            }

            Temp3val += "('" + Encrypted(Convert.ToString(itm["ID"])) + "','" + Encrypted(Convert.ToString(itm["WebsiteName"])) + "','" + Encrypted(Convert.ToString(itm["WebsiteUrl"])) + "','" + Encrypted(Convert.ToString(itm["WebsitePort"])) + "','" + Encrypted(Convert.ToString(itm["IISServerIpUrl"])) + "','" + Encrypted(Convert.ToString(itm["IISServerUserId"])) + "','" + Encrypted(Convert.ToString(itm["IISServerPassWord"])) + "'," +
       " '" + Encrypted(Convert.ToString(itm["DatabaseName"])) + "','" + Encrypted(Convert.ToString(itm["DatabaseServerIpUrl"])) + "','" + Encrypted(Convert.ToString(itm["DatabaseUserId"])) + "','" + Encrypted(Convert.ToString(itm["DatabasePassword"])) + "','" + Encrypted(Convert.ToString(itm["BusiControllerName"])) + "','" + Encrypted(Convert.ToString(itm["BusiControllerDatabaseName"])) + "','" + Encrypted(Convert.ToString(itm["BusiControllerSqlServerIpUrl"])) + "','" + Encrypted(Convert.ToString(itm["BusiControllerUserId"])) + "', " +
       " '" + Encrypted(Convert.ToString(itm["BusiControllerPassword"])) + "','" + Encrypted(Convert.ToString(itm["BusiControllerConnectionString"])) + "','" + Encrypted(Convert.ToString(itm["FTP_Url"])) + "','" + Encrypted(Convert.ToString(itm["FTP_Port"])) + "','" + Encrypted(Convert.ToString(itm["FTP_UserId"])) + "'," +
       " '" + Encrypted(Convert.ToString(itm["FTP_Password"])) + "','" + Encrypted(Convert.ToString(itm["VersionInfoId"])) + "','" + Encrypted(Convert.ToString(itm["IISAccessPort"])) + "','" + Encrypted(Convert.ToString(itm["DatabaseAccessPort"])) + "','" + Encrypted(Convert.ToString(itm["BusiControllerPort"])) + "','" + Encrypted(Convert.ToString(itm["compid"])) + "','" + Encrypted(Convert.ToString(itm["FTPWorkGuideUrl"])) + "','" + Encrypted(Convert.ToString(itm["FTPWorkGuidePort"])) + "','" + Encrypted(Convert.ToString(itm["FTPWorkGuideUserId"])) + "','" + Encrypted(Convert.ToString(itm["FTPWorkGuidePW"])) + "')";


        }
        if (Temp3val.Length > 0)
        {
            Temp2 += Temp3val;
            SqlCommand ccm = new SqlCommand(Temp2, conn);
            conn.Open();
            ccm.ExecuteNonQuery();
            conn.Close();
        }
    }
    protected void Section(string verid, string cid)
    {
        string Temp3val = "";
        string Temp2 = "INSERT INTO WebsiteSection(WebsiteSectionId,WebsiteMasterId,SectionName,AfterLoginDefaultPageId,LoginUrl,NormalUrl)Values ";
        DataTable dtr = selectBZ("Select Distinct WebsiteSection.*   from  WebsiteMaster inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.Id where WebsiteMaster.VersionInfoId = '" + verid + "' ");
        foreach (DataRow itm in dtr.Rows)
        {
            if (Temp3val.Length > 0)
            {
                Temp3val += ",";
            }

            Temp3val += "('" + Encrypted(Convert.ToString(itm["WebsiteSectionId"])) + "','" + Encrypted(Convert.ToString(itm["WebsiteMasterId"])) + "','" + Encrypted(Convert.ToString(itm["SectionName"])) + "','" + Encrypted(Convert.ToString(itm["AfterLoginDefaultPageId"])) + "','" + Encrypted(Convert.ToString(itm["LoginUrl"])) + "','" + Encrypted(Convert.ToString(itm["NormalUrl"])) + "')";


        }
        if (Temp3val.Length > 0)
        {
            Temp2 += Temp3val;
            SqlCommand ccm = new SqlCommand(Temp2, conn);
            conn.Open();
            ccm.ExecuteNonQuery();
            conn.Close();
        }
    }
    protected void Masterpage(string verid, string cid)
    {
        string Temp3val = "";
        string Temp2 = "INSERT INTO MasterPageMaster(MasterPageId,MasterPageName,MasterPageDescription,WebsiteSectionId)Values ";
        DataTable dtr = selectBZ("Select Distinct MasterPageMaster.*   from  WebsiteMaster inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.Id inner join MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId where WebsiteMaster.VersionInfoId = '" + verid + "' ");
        foreach (DataRow itm in dtr.Rows)
        {
            if (Temp3val.Length > 0)
            {
                Temp3val += ",";
            }

            Temp3val += "('" + Encrypted(Convert.ToString(itm["MasterPageId"])) + "','" + Encrypted(Convert.ToString(itm["MasterPageName"])) + "','" + Encrypted(Convert.ToString(itm["MasterPageDescription"])) + "','" + Encrypted(Convert.ToString(itm["WebsiteSectionId"])) + "')";


        }
        if (Temp3val.Length > 0)
        {
            Temp2 += Temp3val;
            SqlCommand ccm = new SqlCommand(Temp2, conn);
            conn.Open();
            ccm.ExecuteNonQuery();
            conn.Close();
        }
    }
    protected void MainManu(string verid, string cid)
    {
        string Temp3val = "";
        string Temp2 = "INSERT INTO MainMenuMaster(MainMenuId,MainMenuName,BackColour,MainMenuTitle,MasterPage_Id,MainMenuIndex,Active,LanguageId)Values ";
        DataTable dtr = selectBZ("Select Distinct MainMenuMaster.*   from  WebsiteMaster inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.Id inner join MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId inner join MainMenuMaster on MainMenuMaster.MasterPage_Id=MasterPageMaster.MasterPageId where WebsiteMaster.VersionInfoId = '" + verid + "' ");
        foreach (DataRow itm in dtr.Rows)
        {
            if (Temp3val.Length > 0)
            {
                Temp3val += ",";
            }

            Temp3val += "('" + Encrypted(Convert.ToString(itm["MainMenuId"])) + "','" + Encrypted(Convert.ToString(itm["MainMenuName"])) + "','" + Encrypted(Convert.ToString(itm["BackColour"])) + "','" + Encrypted(Convert.ToString(itm["MainMenuTitle"])) + "','" + Encrypted(Convert.ToString(itm["MasterPage_Id"])) + "','" + (itm["MainMenuIndex"]) + "','" + Convert.ToString(itm["Active"]) + "','" + Encrypted(Convert.ToString(itm["LanguageId"])) + "')";


        }
        if (Temp3val.Length > 0)
        {
            Temp2 += Temp3val;
            SqlCommand ccm = new SqlCommand(Temp2, conn);
            conn.Open();
            ccm.ExecuteNonQuery();
            conn.Close();
        }
    }
    protected void SubManu(string verid, string cid)
    {
        string Temp3val = "";
        string Temp2 = "INSERT INTO SubMenuMaster(SubMenuId,MainMenuId,SubMenuName,SubMenuIndex,Active,LanguageId)Values ";
        DataTable dtr = selectBZ("Select Distinct SubMenuMaster.*   from  WebsiteMaster inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.Id inner join MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId inner join MainMenuMaster on MainMenuMaster.MasterPage_Id=MasterPageMaster.MasterPageId inner join SubMenuMaster on SubMenuMaster.MainMenuId=MainMenuMaster.MainMenuId where WebsiteMaster.VersionInfoId = '" + verid + "' ");
        foreach (DataRow itm in dtr.Rows)
        {
            if (Temp3val.Length > 0)
            {
                Temp3val += ",";
            }

            Temp3val += "('" + Encrypted(Convert.ToString(itm["SubMenuId"])) + "','" + Encrypted(Convert.ToString(itm["MainMenuId"])) + "','" + Encrypted(Convert.ToString(itm["SubMenuName"])) + "','" + (Convert.ToString(itm["SubMenuIndex"])) + "','" + Convert.ToString(itm["Active"]) + "','" + Encrypted(Convert.ToString(itm["LanguageId"])) + "')";


        }
        if (Temp3val.Length > 0)
        {
            Temp2 += Temp3val;
            SqlCommand ccm = new SqlCommand(Temp2, conn);
            conn.Open();
            ccm.ExecuteNonQuery();
            conn.Close();
        }
    }
    protected void Pagename(string verid, string cid)
    {

        string Temp3val = "";
        string temp = "";
        string Temp2 = "INSERT INTO PageMaster(PageId,PageTypeId,PageName,PageTitle,PageDescription,PageIndex,VersionInfoMasterId,MainMenuId,FolderName,Active,SubMenuId,LanguageId,ManuAccess)Values ";
        DataTable dtr = selectBZ("Select Distinct PageMaster.*   from  WebsiteMaster inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.Id inner join MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId inner join MainMenuMaster on MainMenuMaster.MasterPage_Id=MasterPageMaster.MasterPageId inner join SubMenuMaster on SubMenuMaster.MainMenuId=MainMenuMaster.MainMenuId inner join PageMaster on PageMaster.MainMenuId=MainMenuMaster.MainMenuId where PageMaster.VersionInfoMasterId = '" + verid + "' ");
        temp = Temp3val;
        int jk = 0;
        foreach (DataRow itm in dtr.Rows)
        {

            if (Temp3val.Length > 0)
            {
                Temp3val += ",";
            }


            //pkchange29716 Temp3val += "('" + Encrypted(Convert.ToString(itm["PageId"])) + "','" + Encrypted(Convert.ToString(itm["PageTypeId"])) + "','" + Encrypted(Convert.ToString(itm["PageName"])) + "','" + Encrypted(Convert.ToString(itm["PageTitle"])) + "','" + Encrypted(Convert.ToString(itm["PageDescription"])) + "','" + Convert.ToString(itm["PageIndex"]) + "','" + Encrypted(Convert.ToString(itm["VersionInfoMasterId"])) + "','" + Encrypted(Convert.ToString(itm["MainMenuId"])) + "','" + Encrypted(Convert.ToString(itm["FolderName"])) + "','" + Convert.ToString(itm["Active"]) + "','" + Encrypted(Convert.ToString(itm["SubMenuId"])) + "','" + Encrypted(Convert.ToString(itm["LanguageId"])) + "','" + Convert.ToString(itm["ManuAccess"]) + "')";
            Temp3val += "('" + Encrypted(Convert.ToString(itm["PageId"])) + "','" + Encrypted(Convert.ToString(itm["PageTypeId"])) + "','" + Encrypted(Convert.ToString(itm["PageName"])) + "','" + Encrypted(Convert.ToString(itm["PageTitle"])) + "','" + Encrypted(Convert.ToString(itm["PageDescription"])) + "','" + Convert.ToString(itm["PageIndex"]) + "','" + Encrypted(Convert.ToString(itm["VersionInfoMasterId"])) + "','" + Encrypted(Convert.ToString(itm["MainMenuId"])) + "','" + Encrypted(Convert.ToString(itm["FolderName"])) + "','" + Convert.ToString(itm["Active"]) + "','" + Encrypted(Convert.ToString(itm["SubMenuId"])) + "','" + Encrypted(Convert.ToString(itm["LanguageId"])) + "','" + Convert.ToString(itm["ManuAccess"]) + "')";

            if (jk > 700)
            {

                temp = Temp2 + Temp3val;
                SqlCommand ccm = new SqlCommand(temp, conn);
                conn.Open();
                ccm.ExecuteNonQuery();
                conn.Close();
                Temp3val = "";
                temp = "";
                jk = 0;

            }
            else
            {
                jk += 1;
            }


        }
        if (Temp3val.Length > 0)
        {
            temp = Temp2 + Temp3val;
            SqlCommand ccm = new SqlCommand(temp, conn);
            conn.Open();
            ccm.ExecuteNonQuery();
            conn.Close();
        }
    }
    protected void Pageaccess(string verid, string cid)
    {

        string Temp3val = "";
        string temp = "";
        string Temp2 = "INSERT INTO pageplaneaccesstbl(Id,Pageid,Pagename,Priceplanid,pageaccess)Values ";
        DataTable dtr = selectBZ("Select Distinct pageplaneaccesstbl.*   from  WebsiteMaster inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.Id inner join MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId inner join MainMenuMaster on MainMenuMaster.MasterPage_Id=MasterPageMaster.MasterPageId inner join SubMenuMaster on SubMenuMaster.MainMenuId=MainMenuMaster.MainMenuId inner join PageMaster on PageMaster.MainMenuId=MainMenuMaster.MainMenuId inner join pageplaneaccesstbl on pageplaneaccesstbl.Pageid=PageMaster.PageId where  PageMaster.VersionInfoMasterId = '" + verid + "' ");
        temp = Temp3val;
        int jk = 0;
        foreach (DataRow itm in dtr.Rows)
        {

            if (Temp3val.Length > 0)
            {
                Temp3val += ",";
            }

            Temp3val += "('" + Encrypted(Convert.ToString(itm["Id"])) + "','" + Encrypted(Convert.ToString(itm["Pageid"])) + "','" + Encrypted(Convert.ToString(itm["Pagename"])) + "','" + Encrypted(Convert.ToString(itm["Priceplanid"])) + "','" + Convert.ToString(itm["pageaccess"]) + "')";

            string test = Encrypted(Convert.ToString(itm["Id"]));
            if (jk > 700)
            {

                temp = Temp2 + Temp3val;
                SqlCommand ccm = new SqlCommand(temp, conn);
                conn.Open();
                ccm.ExecuteNonQuery();
                conn.Close();
                Temp3val = "";
                temp = "";
                jk = 0;

            }
            else
            {
                jk += 1;
            }


        }
        if (Temp3val.Length > 0)
        {
            temp = Temp2 + Temp3val;
            SqlCommand ccm = new SqlCommand(temp, conn);
            conn.Open();
            ccm.ExecuteNonQuery();
            conn.Close();
        }

    }
    protected void Controltype(string verid, string cid)
    {
        string Temp3val = "";
        string Temp2 = "INSERT INTO Control_type_Master(Type_id,Type_name)Values ";
        DataTable dtr = selectBZ("Select Distinct *   from  Control_type_Master ");
        foreach (DataRow itm in dtr.Rows)
        {
            if (Temp3val.Length > 0)
            {
                Temp3val += ",";
            }

            Temp3val += "('" + Encrypted(Convert.ToString(itm["Type_id"])) + "','" + Encrypted(Convert.ToString(itm["Type_name"])) + "')";


        }
        if (Temp3val.Length > 0)
        {
            Temp2 += Temp3val;
            SqlCommand ccm = new SqlCommand(Temp2, conn);
            conn.Open();
            ccm.ExecuteNonQuery();
            conn.Close();
        }
    }
    protected void PageControltype(string verid, string cid)
    {

        string Temp3val = "";
        string temp = "";
        string Temp2 = "INSERT INTO PageControlMaster(PageControl_id,Page_id,ControlTitle,ControlName,ControlType_id,ActiveDeactive,DefaultLabel)Values ";
        DataTable dtr = selectBZ("Select Distinct PageControlMaster.* from PageControlMaster inner join pageplaneaccesstbl on pageplaneaccesstbl.pageid=PageControlMaster.Page_id inner join PageMaster on PageMaster.PageId=PageControlMaster.Page_id where  PageMaster.VersionInfoMasterId = '" + verid + "'");
        temp = Temp3val;
        int jk = 0;
        foreach (DataRow itm in dtr.Rows)
        {

            if (Temp3val.Length > 0)
            {
                Temp3val += ",";
            }

            Temp3val += "('" + Encrypted(Convert.ToString(itm["PageControl_id"])) + "','" + Encrypted(Convert.ToString(itm["Page_id"])) + "','" + Encrypted(Convert.ToString(itm["ControlTitle"])) + "','" + Encrypted(Convert.ToString(itm["ControlName"])) + "','" + Encrypted(Convert.ToString(itm["ControlType_id"])) + "','" + Convert.ToString(itm["ActiveDeactive"]) + "','" + Encrypted(Convert.ToString(itm["DefaultLabel"])) + "')";
            if (jk > 700)
            {

                temp = Temp2 + Temp3val;
                SqlCommand ccm = new SqlCommand(temp, conn);
                conn.Open();
                ccm.ExecuteNonQuery();
                conn.Close();
                Temp3val = "";
                temp = "";
                jk = 0;

            }
            else
            {
                jk += 1;
            }


        }
        if (Temp3val.Length > 0)
        {
            temp = Temp2 + Temp3val;
            SqlCommand ccm = new SqlCommand(temp, conn);
            conn.Open();
            ccm.ExecuteNonQuery();
            conn.Close();
        }
    }
    protected void ClientProductTableMaster(string verid, string cid)
    {
        string Temp3val = "";
        string Temp2 = "INSERT INTO ClientProductTableMaster(Id,VersionInfoId,TableName,TableTitle)Values ";
        DataTable dtr = selectBZ("Select Distinct *   from  ClientProductTableMaster where VersionInfoId='" + verid + "'");
        foreach (DataRow itm in dtr.Rows)
        {
            if (Temp3val.Length > 0)
            {
                Temp3val += ",";
            }

            Temp3val += "('" + Encrypted(Convert.ToString(itm["Id"])) + "','" + Encrypted(Convert.ToString(itm["VersionInfoId"])) + "','" + Encrypted(Convert.ToString(itm["TableName"])) + "','" + Encrypted(Convert.ToString(itm["TableTitle"])) + "')";


        }
        if (Temp3val.Length > 0)
        {
            Temp2 += Temp3val;
            SqlCommand ccm = new SqlCommand(Temp2, conn);
            conn.Open();
            ccm.ExecuteNonQuery();
            conn.Close();
        }
    }
    protected void ClientProductRecordsAllowed(string verid, string cid)
    {
        string Temp3val = "";
        string Temp2 = "INSERT INTO ClientProductRecordsAllowed(Id,ClientProductTblId,PricePlanId,RecordsAllowed,ForeignKeyrecordId,ForeignKeyrecordName)Values ";
        DataTable dtr = selectBZ("Select Distinct ClientProductRecordsAllowed.*   from ClientProductRecordsAllowed inner join  ClientProductTableMaster on ClientProductTableMaster.Id=ClientProductRecordsAllowed.ClientProductTblId where VersionInfoId='" + verid + "'");
        foreach (DataRow itm in dtr.Rows)
        {
            if (Temp3val.Length > 0)
            {
                Temp3val += ",";
            }

            Temp3val += "('" + Encrypted(Convert.ToString(itm["Id"])) + "','" + Encrypted(Convert.ToString(itm["ClientProductTblId"])) + "','" + Encrypted(Convert.ToString(itm["PricePlanId"])) + "','" + Encrypted(Convert.ToString(itm["RecordsAllowed"])) + "','" + Encrypted(Convert.ToString(itm["ForeignKeyrecordId"])) + "','" + Encrypted(Convert.ToString(itm["ForeignKeyrecordName"])) + "')";


        }
        if (Temp3val.Length > 0)
        {
            Temp2 += Temp3val;
            SqlCommand ccm = new SqlCommand(Temp2, conn);
            conn.Open();
            ccm.ExecuteNonQuery();
            conn.Close();
        }
    }

    protected void setupwizardquestion(string verid, string cid)
    {

        string Temp3val = "";
        string temp = "";


        string Temp2 = "INSERT INTO setupwizardquestion(id,SetupDetailID,questionname,answerhint,PageID,showanswerhint,Showanswertetbox,Showddwithhyperlink,YesHyperlinkdisplayPageID,screenshotURL,videoURL,powerpointURL,SetupQindex)Values ";
        DataTable dtr = selectBZ("Select Distinct setupwizardquestion.*   from setupwizardquestion inner join SetupwizardDetail on SetupwizardDetail.Id=setupwizardquestion.SetupDetailID inner join SetupWizardMaster on SetupWizardMaster.Id=SetupwizardDetail.SetupwizardMasterID   where SetupWizardMaster.ProductVersionId='" + verid + "'  ");
        temp = Temp3val;
        int jk = 0;
        foreach (DataRow itm in dtr.Rows)
        {

            if (Temp3val.Length > 0)
            {
                Temp3val += ",";
            }

            Temp3val += "('" + (Convert.ToString(itm["id"])) + "','" + (Convert.ToString(itm["SetupDetailID"])) + "','" + (Convert.ToString(itm["questionname"])) + "','" + (Convert.ToString(itm["answerhint"])) + "','" + (Convert.ToString(itm["PageID"])) + "','" + Convert.ToString(itm["showanswerhint"]) + "','" + (Convert.ToString(itm["Showanswertetbox"])) + "','" + (Convert.ToString(itm["Showddwithhyperlink"])) + "','" + (Convert.ToString(itm["YesHyperlinkdisplayPageID"])) + "','" + Convert.ToString(itm["screenshotURL"]) + "','" + (Convert.ToString(itm["videoURL"])) + "','" + (Convert.ToString(itm["powerpointURL"])) + "','" + (Convert.ToString(itm["SetupQindex"])) + "')";
            if (jk > 700)
            {

                temp = Temp2 + Temp3val;
                SqlCommand ccm = new SqlCommand(temp, conn);
                conn.Open();
                ccm.ExecuteNonQuery();
                conn.Close();
                Temp3val = "";
                temp = "";
                jk = 0;

            }
            else
            {
                jk += 1;
            }


        }
        if (Temp3val.Length > 0)
        {
            temp = Temp2 + Temp3val;
            SqlCommand ccm = new SqlCommand(temp, conn);
            conn.Open();
            ccm.ExecuteNonQuery();
            conn.Close();
        }
    }
    protected void setupwizardquestionPortal(string verid, string cid)
    {

        string Temp3val = "";
        string temp = "";


        string Temp2 = "INSERT INTO setupwizardquestionPortal(setupwizardquestionId,PortalId)Values ";
        //DataTable dtr = selectBZ("Select Distinct PageMaster.*   from  WebsiteMaster inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.Id inner join MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId inner join MainMenuMaster on MainMenuMaster.MasterPage_Id=MasterPageMaster.MasterPageId inner join SubMenuMaster on SubMenuMaster.MainMenuId=MainMenuMaster.MainMenuId inner join PageMaster on PageMaster.MainMenuId=MainMenuMaster.MainMenuId where PageMaster.VersionInfoMasterId = '" + verid + "' ");
        DataTable dtr = selectBZ("Select Distinct setupwizardquestionPortal.*   from SetupwizardMaster inner join  SetupwizardDetail on SetupwizardDetail.setupwizardmasterid=SetupwizardMaster.ID inner join setupwizardquestion on setupwizardquestion.SetupDetailID=SetupwizardDetail.Id  inner join setupwizardquestionPortal on setupwizardquestionPortal.setupwizardquestionId=setupwizardquestion.Id where ProductVersionId In( select ProductId from  VersionInfoMaster where VersionInfoId='" + verid + "')");
        temp = Temp3val;
        int jk = 0;
        foreach (DataRow itm in dtr.Rows)
        {

            if (Temp3val.Length > 0)
            {
                Temp3val += ",";
            }

            Temp3val += "('" + (Convert.ToString(itm["setupwizardquestionId"])) + "','" + (Convert.ToString(itm["PortalId"])) + "')";
            if (jk > 700)
            {

                temp = Temp2 + Temp3val;
                SqlCommand ccm = new SqlCommand(temp, conn);
                conn.Open();
                ccm.ExecuteNonQuery();
                conn.Close();
                Temp3val = "";
                temp = "";
                jk = 0;

            }
            else
            {
                jk += 1;
            }


        }
        if (Temp3val.Length > 0)
        {
            temp = Temp2 + Temp3val;
            SqlCommand ccm = new SqlCommand(temp, conn);
            conn.Open();
            ccm.ExecuteNonQuery();
            conn.Close();
        }
    }
    protected void SetupWizardMaster(string verid, string cid)
    {

        string Temp3val = "";
        string temp = "";


        string Temp2 = "INSERT INTO SetupWizardMaster(Id,Name,ProductVersionId,SetupMindex)Values ";
        //DataTable dtr = selectBZ("Select Distinct PageMaster.*   from  WebsiteMaster inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.Id inner join MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId inner join MainMenuMaster on MainMenuMaster.MasterPage_Id=MasterPageMaster.MasterPageId inner join SubMenuMaster on SubMenuMaster.MainMenuId=MainMenuMaster.MainMenuId inner join PageMaster on PageMaster.MainMenuId=MainMenuMaster.MainMenuId where PageMaster.VersionInfoMasterId = '" + verid + "' ");
        DataTable dtr = selectBZ("Select Distinct SetupWizardMaster.*   from SetupWizardMaster where ProductVersionId='" + verid + "'  ");
        temp = Temp3val;
        int jk = 0;
        foreach (DataRow itm in dtr.Rows)
        {

            if (Temp3val.Length > 0)
            {
                Temp3val += ",";
            }

            Temp3val += "('" + (Convert.ToString(itm["Id"])) + "','" + (Convert.ToString(itm["Name"])) + "','" + (Convert.ToString(itm["ProductVersionId"])) + "','" + (Convert.ToString(itm["SetupMindex"])) + "')";
            if (jk > 700)
            {

                temp = Temp2 + Temp3val;
                SqlCommand ccm = new SqlCommand(temp, conn);
                conn.Open();
                ccm.ExecuteNonQuery();
                conn.Close();
                Temp3val = "";
                temp = "";
                jk = 0;

            }
            else
            {
                jk += 1;
            }


        }
        if (Temp3val.Length > 0)
        {
            temp = Temp2 + Temp3val;
            SqlCommand ccm = new SqlCommand(temp, conn);
            conn.Open();
            ccm.ExecuteNonQuery();
            conn.Close();
        }
    }
    protected void SetupWizardMasterwithPortal(string verid, string cid)
    {

        string Temp3val = "";
        string temp = "";


        string Temp2 = "INSERT INTO SetupWizardMasterwithPortal(SetupWizardMasterId,PortalId)Values ";
        //DataTable dtr = selectBZ("Select Distinct PageMaster.*   from  WebsiteMaster inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.Id inner join MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId inner join MainMenuMaster on MainMenuMaster.MasterPage_Id=MasterPageMaster.MasterPageId inner join SubMenuMaster on SubMenuMaster.MainMenuId=MainMenuMaster.MainMenuId inner join PageMaster on PageMaster.MainMenuId=MainMenuMaster.MainMenuId where PageMaster.VersionInfoMasterId = '" + verid + "' ");
        DataTable dtr = selectBZ("Select Distinct SetupWizardMasterwithPortal.*   from SetupwizardMaster  inner join SetupWizardMasterwithPortal on SetupWizardMasterwithPortal.SetupWizardMasterId=SetupwizardMaster.Id  where ProductVersionId In( select ProductId from  VersionInfoMaster where VersionInfoId='" + verid + "')");
        temp = Temp3val;
        int jk = 0;
        foreach (DataRow itm in dtr.Rows)
        {

            if (Temp3val.Length > 0)
            {
                Temp3val += ",";
            }

            Temp3val += "('" + (Convert.ToString(itm["SetupWizardMasterId"])) + "','" + (Convert.ToString(itm["PortalId"])) + "')";
            if (jk > 700)
            {

                temp = Temp2 + Temp3val;
                SqlCommand ccm = new SqlCommand(temp, conn);
                conn.Open();
                ccm.ExecuteNonQuery();
                conn.Close();
                Temp3val = "";
                temp = "";
                jk = 0;

            }
            else
            {
                jk += 1;
            }


        }
        if (Temp3val.Length > 0)
        {
            temp = Temp2 + Temp3val;
            SqlCommand ccm = new SqlCommand(temp, conn);
            conn.Open();
            ccm.ExecuteNonQuery();
            conn.Close();
        }
    }
    protected void SetupwizardDetail(string verid, string cid)
    {

        string Temp3val = "";
        string temp = "";


        string Temp2 = "INSERT INTO SetupwizardDetail(Id,SetupwizardMasterID,SetupSubwizardTitle,SetupDMindex)Values ";
        DataTable dtr = selectBZ("Select Distinct SetupwizardDetail.*   from SetupwizardDetail inner join SetupWizardMaster on SetupWizardMaster.Id=SetupwizardDetail.SetupwizardMasterID   where SetupWizardMaster.ProductVersionId='" + verid + "'  ");
        temp = Temp3val;
        int jk = 0;
        foreach (DataRow itm in dtr.Rows)
        {

            if (Temp3val.Length > 0)
            {
                Temp3val += ",";
            }

            Temp3val += "('" + (Convert.ToString(itm["Id"])) + "','" + (Convert.ToString(itm["SetupwizardMasterID"])) + "','" + (Convert.ToString(itm["SetupSubwizardTitle"])) + "','" + (Convert.ToString(itm["SetupDMindex"])) + "')";
            if (jk > 700)
            {

                temp = Temp2 + Temp3val;
                SqlCommand ccm = new SqlCommand(temp, conn);
                conn.Open();
                ccm.ExecuteNonQuery();
                conn.Close();
                Temp3val = "";
                temp = "";
                jk = 0;

            }
            else
            {
                jk += 1;
            }


        }
        if (Temp3val.Length > 0)
        {
            temp = Temp2 + Temp3val;
            SqlCommand ccm = new SqlCommand(temp, conn);
            conn.Open();
            ccm.ExecuteNonQuery();
            conn.Close();
        }
    }
    protected void SetupWizardDetailwithPortal(string verid, string cid)
    {

        string Temp3val = "";
        string temp = "";


        string Temp2 = "INSERT INTO SetupWizardDetailwithPortal(SetupWizardDetailId,PortalId)Values ";
        //DataTable dtr = selectBZ("Select Distinct PageMaster.*   from  WebsiteMaster inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.Id inner join MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId inner join MainMenuMaster on MainMenuMaster.MasterPage_Id=MasterPageMaster.MasterPageId inner join SubMenuMaster on SubMenuMaster.MainMenuId=MainMenuMaster.MainMenuId inner join PageMaster on PageMaster.MainMenuId=MainMenuMaster.MainMenuId where PageMaster.VersionInfoMasterId = '" + verid + "' ");
        DataTable dtr = selectBZ("Select Distinct SetupWizardDetailwithPortal.*   from SetupwizardMaster inner join  SetupwizardDetail on SetupwizardDetail.setupwizardmasterid=SetupwizardMaster.ID   inner join SetupWizardDetailwithPortal on SetupWizardDetailwithPortal.SetupWizardDetailId=SetupwizardDetail.Id where ProductVersionId In( select ProductId from  VersionInfoMaster where VersionInfoId='" + verid + "')");
        temp = Temp3val;
        int jk = 0;
        foreach (DataRow itm in dtr.Rows)
        {

            if (Temp3val.Length > 0)
            {
                Temp3val += ",";
            }

            Temp3val += "('" + (Convert.ToString(itm["SetupWizardDetailId"])) + "','" + (Convert.ToString(itm["PortalId"])) + "')";
            if (jk > 700)
            {

                temp = Temp2 + Temp3val;
                SqlCommand ccm = new SqlCommand(temp, conn);
                conn.Open();
                ccm.ExecuteNonQuery();
                conn.Close();
                Temp3val = "";
                temp = "";
                jk = 0;

            }
            else
            {
                jk += 1;
            }


        }
        if (Temp3val.Length > 0)
        {
            temp = Temp2 + Temp3val;
            SqlCommand ccm = new SqlCommand(temp, conn);
            conn.Open();
            ccm.ExecuteNonQuery();
            conn.Close();
        }
    }
    protected void SatelliteSynchronise(string verid)
    {
        string Temp3val = "";
        string Temp2 = "INSERT INTO SateliteServerRequiringSynchronisationMasterTbl (Id,SyncronisationrequiredTBlID,servermasterID,SynchronisationSuccessful,SynchronisationSuccessfulDatetime) Values";

        DataTable dtr = selectserverconn("select SateliteServerRequiringSynchronisationMasterTbl.* from SateliteServerRequiringSynchronisationMasterTbl inner join SyncronisationrequiredTbl on SyncronisationrequiredTbl.Id=SateliteServerRequiringSynchronisationMasterTbl.SyncronisationrequiredTBlID inner join SatelliteSyncronisationrequiringTablesMaster on SatelliteSyncronisationrequiringTablesMaster.Id=SyncronisationrequiredTbl.SatelliteSyncronisationrequiringTablesMasterID  where SatelliteSyncronisationrequiringTablesMaster.ProductVersionID='" + verid + "' order by Id ");
        foreach (DataRow itm in dtr.Rows)
        {
            if (Temp3val.Length > 0)
            {
                Temp3val += ",";
            }

            Temp3val += "('" + (Convert.ToString(itm["Id"])) + "','" + (Convert.ToString(itm["SyncronisationrequiredTBlID"])) + "','" + (Convert.ToString(itm["servermasterID"])) + "','" + (Convert.ToString(itm["SynchronisationSuccessful"])) + "','" + (Convert.ToString(itm["SynchronisationSuccessfulDatetime"])) + "')";

        }
        if (Temp3val.Length > 0)
        {
            Temp2 += Temp3val;
            SqlCommand ccm = new SqlCommand(Temp2, conn);
            conn.Open();
            ccm.ExecuteNonQuery();
            conn.Close();
        }
    }

    protected void PriceplanrestrictionTbl(string verid, string cid)
    {
        string Temp3val = "";
        string Temp2 = "INSERT INTO PriceplanrestrictionTbl(Id,ProductversionId,TableId,NameofRest,TextofQueinSelection,Restingroup,Priceaddingroup,PortalId,FieldrestrictionSet,RestrictfieldId)Values ";
        DataTable dtr = selectBZ("Select Distinct *   from  PriceplanrestrictionTbl where ProductversionId='" + verid + "'");
        foreach (DataRow itm in dtr.Rows)
        {
            if (Temp3val.Length > 0)
            {
                Temp3val += ",";
            }

            Temp3val += "('" + Encrypted(Convert.ToString(itm["Id"])) + "','" + Encrypted(Convert.ToString(itm["ProductversionId"])) + "','" + Encrypted(Convert.ToString(itm["TableId"])) + "','" + Encrypted(Convert.ToString(itm["NameofRest"])) + "'," +
            " '" + Encrypted(Convert.ToString(itm["TextofQueinSelection"])) + "','" + Encrypted(Convert.ToString(itm["Restingroup"])) + "','" + Encrypted(Convert.ToString(itm["Priceaddingroup"])) + "','" + Encrypted(Convert.ToString(itm["PortalId"])) + "'," +
            " '" + Encrypted(Convert.ToString(itm["FieldrestrictionSet"])) + "','" + Encrypted(Convert.ToString(itm["RestrictfieldId"])) + "' )";


        }
        if (Temp3val.Length > 0)
        {
            Temp2 += Temp3val;
            SqlCommand ccm = new SqlCommand(Temp2, conn);
            conn.Open();
            ccm.ExecuteNonQuery();
            conn.Close();
        }
    }
    protected void Priceplanrestrecordallowtbl(string verid, string cid)
    {

        string Temp3val = "";
        string temp = "";

        string Temp2 = "INSERT INTO Priceplanrestrecordallowtbl(Id,PriceplanRestrictiontblId,PricePlanId,RecordsAllowed)Values ";
        DataTable dtr = selectBZ("Select Distinct Priceplanrestrecordallowtbl.*   from Priceplanrestrecordallowtbl inner join  PriceplanrestrictionTbl on PriceplanrestrictionTbl.Id=Priceplanrestrecordallowtbl.PriceplanRestrictiontblId where PriceplanrestrictionTbl.ProductversionId='" + verid + "'");
        temp = Temp3val;
        int jk = 0;
        foreach (DataRow itm in dtr.Rows)
        {

            if (Temp3val.Length > 0)
            {
                Temp3val += ",";
            }

            Temp3val += "('" + Encrypted(Convert.ToString(itm["Id"])) + "','" + Encrypted(Convert.ToString(itm["PriceplanRestrictiontblId"])) + "','" + Encrypted(Convert.ToString(itm["PricePlanId"])) + "','" + Encrypted(Convert.ToString(itm["RecordsAllowed"])) + "')";
            if (jk > 700)
            {

                temp = Temp2 + Temp3val;
                SqlCommand ccm = new SqlCommand(temp, conn);
                conn.Open();
                ccm.ExecuteNonQuery();
                conn.Close();
                Temp3val = "";
                temp = "";
                jk = 0;

            }
            else
            {
                jk += 1;
            }


        }
        if (Temp3val.Length > 0)
        {
            temp = Temp2 + Temp3val;
            SqlCommand ccm = new SqlCommand(temp, conn);
            conn.Open();
            ccm.ExecuteNonQuery();
            conn.Close();
        }
    }
    protected void tablefielddetail(string verid, string cid)
    {

        string Temp3val = "";
        string temp = "";

        string Temp2 = "INSERT INTO tablefielddetail(Id,tableId,feildname,fieldtype,size,Isforeignkey,foreignkeytblid,foreignkeyfieldId)Values ";
        DataTable dtr = selectBZ("Select Distinct tablefielddetail.*   from tablefielddetail inner join  ClientProductTableMaster on ClientProductTableMaster.Id=tablefielddetail.tableId where ClientProductTableMaster.VersionInfoId='" + verid + "'");
        temp = Temp3val;
        int jk = 0;
        foreach (DataRow itm in dtr.Rows)
        {

            if (Temp3val.Length > 0)
            {
                Temp3val += ",";
            }

            Temp3val += "('" + Encrypted(Convert.ToString(itm["Id"])) + "','" + Encrypted(Convert.ToString(itm["tableId"])) + "','" + Encrypted(Convert.ToString(itm["feildname"])) + "','" + Encrypted(Convert.ToString(itm["fieldtype"])) + "'," +
            "'" + Encrypted(Convert.ToString(itm["size"])) + "','" + (Convert.ToString(itm["Isforeignkey"])) + "','" + Encrypted(Convert.ToString(itm["foreignkeytblid"])) + "','" + Encrypted(Convert.ToString(itm["foreignkeyfieldId"])) + "')";
            if (jk > 700)
            {

                temp = Temp2 + Temp3val;
                SqlCommand ccm = new SqlCommand(temp, conn);
                conn.Open();
                ccm.ExecuteNonQuery();
                conn.Close();
                Temp3val = "";
                temp = "";
                jk = 0;

            }
            else
            {
                jk += 1;
            }


        }
        if (Temp3val.Length > 0)
        {
            temp = Temp2 + Temp3val;
            SqlCommand ccm = new SqlCommand(temp, conn);
            conn.Open();
            ccm.ExecuteNonQuery();
            conn.Close();
        }
    }
    protected void PortaldesignationTbl(string verid, string cid)
    {

        string Temp3val = "";
        string temp = "";


        string Temp2 = "INSERT INTO PortaldesignationTbl(Id,ProductId,PortalId,Portalname,DefaultPageName)Values ";
        DataTable dtr = selectBZ("Select Distinct PortaldesignationTbl.*   from PortaldesignationTbl where ProductId In( Select Distinct ProductId from VersionInfoMaster where VersionInfoId='" + verid + "')");
        temp = Temp3val;
        int jk = 0;
        foreach (DataRow itm in dtr.Rows)
        {

            if (Temp3val.Length > 0)
            {
                Temp3val += ",";
            }

            Temp3val += "('" + (Convert.ToString(itm["Id"])) + "','" + (Convert.ToString(itm["ProductId"])) + "','" + (Convert.ToString(itm["PortalId"])) + "','" + (Convert.ToString(itm["Portalname"])) + "','" + (Convert.ToString(itm["DefaultPageName"])) + "')";
            if (jk > 700)
            {

                temp = Temp2 + Temp3val;
                SqlCommand ccm = new SqlCommand(temp, conn);
                conn.Open();
                ccm.ExecuteNonQuery();
                conn.Close();
                Temp3val = "";
                temp = "";
                jk = 0;

            }
            else
            {
                jk += 1;
            }


        }
        if (Temp3val.Length > 0)
        {
            temp = Temp2 + Temp3val;
            SqlCommand ccm = new SqlCommand(temp, conn);
            conn.Open();
            ccm.ExecuteNonQuery();
            conn.Close();
        }
    }
    protected void PortaldesignationDetailTbl(string verid, string cid)
    {

        string Temp3val = "";
        string temp = "";


        string Temp2 = "INSERT INTO PortaldesignationDetailTbl(PortaldesignationTblId,DesignationName,PageName)Values ";
        DataTable dtr = selectBZ("Select Distinct PortaldesignationDetailTbl.*   from PortaldesignationDetailTbl inner join PortaldesignationTbl on PortaldesignationTbl.Id=PortaldesignationDetailTbl.PortaldesignationTblId where PortaldesignationTbl.ProductId In( Select Distinct ProductId from VersionInfoMaster where VersionInfoId='" + verid + "')");
        temp = Temp3val;
        int jk = 0;
        foreach (DataRow itm in dtr.Rows)
        {

            if (Temp3val.Length > 0)
            {
                Temp3val += ",";
            }

            Temp3val += "('" + (Convert.ToString(itm["PortaldesignationTblId"])) + "','" + (Convert.ToString(itm["DesignationName"])) + "','" + (Convert.ToString(itm["PageName"])) + "')";
            if (jk > 700)
            {

                temp = Temp2 + Temp3val;
                SqlCommand ccm = new SqlCommand(temp, conn);
                conn.Open();
                ccm.ExecuteNonQuery();
                conn.Close();
                Temp3val = "";
                temp = "";
                jk = 0;

            }
            else
            {
                jk += 1;
            }


        }
        if (Temp3val.Length > 0)
        {
            temp = Temp2 + Temp3val;
            SqlCommand ccm = new SqlCommand(temp, conn);
            conn.Open();
            ccm.ExecuteNonQuery();
            conn.Close();
        }
    }

    protected DataTable selectserverconn(string str)
    {
        SqlCommand cmdclnccdweb = new SqlCommand(str, condefaultdatabase);
        DataTable dtclnccdweb = new DataTable();
        SqlDataAdapter adpclnccdweb = new SqlDataAdapter(cmdclnccdweb);
        adpclnccdweb.Fill(dtclnccdweb);
        return dtclnccdweb;
    }

    protected DataTable selectBZ(string str)
    {
        SqlCommand cmdclnccdweb = new SqlCommand(str, con);
        DataTable dtclnccdweb = new DataTable();
        SqlDataAdapter adpclnccdweb = new SqlDataAdapter(cmdclnccdweb);
        adpclnccdweb.Fill(dtclnccdweb);
        return dtclnccdweb;
    }
    protected DataTable select(string str)
    {
        SqlCommand cmdclnccdweb = new SqlCommand(str, conn);//BUSICONTROLDB
        DataTable dtclnccdweb = new DataTable();
        SqlDataAdapter adpclnccdweb = new SqlDataAdapter(cmdclnccdweb);
        adpclnccdweb.Fill(dtclnccdweb);
        return dtclnccdweb;
    }
    protected DataTable select1(string str)
    {
        SqlCommand cmdclnccdweb = new SqlCommand(str, con);//BUSICONTROLDB
        DataTable dtclnccdweb = new DataTable();
        SqlDataAdapter adpclnccdweb = new SqlDataAdapter(cmdclnccdweb);
        adpclnccdweb.Fill(dtclnccdweb);
        return dtclnccdweb;
    }


    //----------------------------------
    //-----------------------
    protected void BtnABCKey(string CompanyLoginId,string ServerId)
    {
      
        //CompanyLoginId = "aswathy";

        //Stopwatch watch = new Stopwatch();
        //watch.Start();
        // Int64 C = 0;
        Int64 X = 0;
        Int64 Y = 0;
        Int64 Z = 0;      

        Int64 C1 = 0;
        Int64 C2 = 0;
        Int64 C3 = 0;
        Int64 C4 = 0;
        Int64 C5 = 0;

        Int64 A1 = 0;
        Int64 A2 = 0;
        Int64 A3 = 0;
        Int64 A4 = 0;
        Int64 A5 = 0;       
        string result = "";       
        DataTable dt_getCompanyABCMaster = MyCommonfile.selectBZ(" Select * From CompanyABCMaster where CompanyLoginId='" + CompanyLoginId + "' ");
        if (dt_getCompanyABCMaster.Rows.Count > 0)
        {
            //txt_c1.Text = txt_c.Text.Substring(0, 4) + MyCommonfile.RandomeIntnumber(16);
            //txt_c2.Text = txt_c.Text.Substring(4, 4) + MyCommonfile.RandomeIntnumber(16);
            //txt_c3.Text = txt_c.Text.Substring(8, 4) + MyCommonfile.RandomeIntnumber(16);
            //txt_c4.Text = txt_c.Text.Substring(12, 4) + MyCommonfile.RandomeIntnumber(16);
            //txt_c5.Text = txt_c.Text.Substring(16, 4) + MyCommonfile.RandomeIntnumber(16);
            txt_c.Text = dt_getCompanyABCMaster.Rows[0]["C"].ToString();

            txt_c1.Text = dt_getCompanyABCMaster.Rows[0]["C1"].ToString();
            txt_c2.Text = dt_getCompanyABCMaster.Rows[0]["C2"].ToString();
            txt_c3.Text = dt_getCompanyABCMaster.Rows[0]["C3"].ToString();
            txt_c4.Text = dt_getCompanyABCMaster.Rows[0]["C4"].ToString();
            txt_c5.Text = dt_getCompanyABCMaster.Rows[0]["C5"].ToString();   

            C1 = Convert.ToInt64(txt_c1.Text.Substring(0, 4));
            C2 = Convert.ToInt64(txt_c2.Text.Substring(0, 4));
            C3 = Convert.ToInt64(txt_c3.Text.Substring(0, 4));
            C4 = Convert.ToInt64(txt_c4.Text.Substring(0, 4));
            C5 = Convert.ToInt64(txt_c5.Text.Substring(0, 4));

            DateTime todaydatefull = DateTime.Now;
            string strdt = todaydatefull.ToString("MM-dd-yyyy");
            string ndays = "1";

            DataTable dtfunction = selectBZ(" Select * From FunctionMaster ");
            DateTime startDate = DateTime.Parse(strdt);
            DateTime expiryDate = startDate.AddYears(30);
            int DayInterval = 1;
            while (startDate <= expiryDate)
            {
                Random random = new Random();
                int randomNumberA1 = random.Next(1, 6);
                int randomNumberA2 = random.Next(1, 6);
                int randomNumberA3 = random.Next(1, 6);
                int randomNumberA4 = random.Next(1, 6);
                int randomNumberA5 = random.Next(1, 6);
                //randomNumberA1 = 3;
                //randomNumberA2 = 4;
                //randomNumberA3= 1;
                //randomNumberA4 = 1;
                //randomNumberA5 = 1;
                txt_F1.Text = Convert.ToString(randomNumberA1);
                txt_F2.Text = Convert.ToString(randomNumberA2);
                txt_F3.Text = Convert.ToString(randomNumberA3);
                txt_F4.Text = Convert.ToString(randomNumberA4);
                txt_F5.Text = Convert.ToString(randomNumberA5);

                DateTime datevalue = (Convert.ToDateTime(startDate.ToString("MM-dd-yyyy")));
                String dy = datevalue.Day.ToString();
                String mn = datevalue.Month.ToString();
                String yy = datevalue.Year.ToString();
                txt_date.Text = dy + "" + mn + "" + yy;
                txt_x.Text = dy;
                txt_y.Text = mn;
                txt_z.Text = yy;

                X = Convert.ToInt64(txt_x.Text);
                Y = Convert.ToInt64(txt_y.Text);
                Z = Convert.ToInt64(txt_z.Text);

                //--GET A1---C1--------------------------------------------------------------------
                if (randomNumberA1 == 1)
                {
                    A1 = C1 - (X + Y + Z);
                }
                if (randomNumberA1 == 1)
                {
                    C1 = A1 + (X + Y + Z);
                }
                //-------------------------
                if (randomNumberA1 == 2)
                {
                    A1 = C1 - (X + Y - Z);
                }
                if (randomNumberA1 == 2)
                {
                    C1 = A1 + (X + Y - Z);
                }
                //--------------------------
                if (randomNumberA1 == 3)
                {
                    A1 = C1 - (X - Y + Z);
                }
                if (randomNumberA1 == 3)
                {
                    C1 = A1 + (X - Y + Z);
                }
                //--------------------------
                if (randomNumberA1 == 4)
                {
                    A1 = C1 + (X + Y + Z);
                }
                if (randomNumberA1 == 4)
                {
                    C1 = A1 - (X + Y + Z);
                }
                //--------------------------
                if (randomNumberA1 == 5)
                {
                    A1 = C1 + (X - Y - Z);
                }
                if (randomNumberA1 == 5)
                {
                    C1 = A1 - (X - Y - Z);
                }
                //--------------------------          
                if (randomNumberA1 == 6)
                {
                    A1 = C1 - (X - Y - Z);
                }
                if (randomNumberA1 == 6)
                {
                    C1 = A1 + (X - Y - Z);
                }
                //---------------------------------
                if (C1 != Convert.ToInt64(txt_c1.Text.Substring(0, 4)))
                {
                }
                string a1valu = Convert.ToString(A1);
                txt_a1.Text = a1valu;
                txt_d1.Text = txt_a1.Text.Substring(0, 1);
                txt_e1.Text = txt_a1.Text.Substring(1, a1valu.Length - 1);

                txt_ansc1.Text = Convert.ToString(C1);
                //----------------------------------------------------------------------------------------------------------------
                //--GET C2-A2----------------------------------------------------------------------          
                if (randomNumberA2 == 1)
                {
                    A2 = C2 - (X + Y + Z);
                }
                if (randomNumberA2 == 1)
                {
                    C2 = A2 + (X + Y + Z);
                }
                //----------------------------------         
                if (randomNumberA2 == 2)
                {
                    A2 = C2 - (X + Y - Z);
                }
                if (randomNumberA2 == 2)
                {
                    C2 = A2 + (X + Y - Z);
                }
                //--------------------------------------         
                if (randomNumberA2 == 3)
                {
                    A2 = C2 - (X - Y + Z);
                }
                if (randomNumberA2 == 3)
                {
                    C2 = A2 + (X - Y + Z);
                }
                //---------------------------------------         
                if (randomNumberA2 == 4)
                {
                    A2 = C2 + (X + Y + Z);
                }
                if (randomNumberA2 == 4)
                {
                    C2 = A2 - (X + Y + Z);
                }
                //-----------------------------------------          
                if (randomNumberA2 == 5)
                {
                    A2 = C2 + (X - Y - Z);
                }
                if (randomNumberA2 == 5)
                {
                    C2 = A2 - (X - Y - Z);
                }
                //------------------------------------        
                if (randomNumberA2 == 6)
                {
                    A2 = C2 - (X - Y - Z);
                }
                if (randomNumberA2 == 6)
                {
                    C2 = A2 + (X - Y - Z);
                }
                //--------------------------------

                if (C2 != Convert.ToInt64(txt_c2.Text.Substring(0, 4)))
                {

                }

                string a2valu = Convert.ToString(A2);
                txt_a2.Text = a2valu;
                txt_d2.Text = txt_a2.Text.Substring(0, 1);
                txt_e2.Text = txt_a2.Text.Substring(1, a2valu.Length - 1);
                txt_ansc2.Text = Convert.ToString(C2);

                //-------GET A3---C3----------------------------------------------------------------------------------------------------
                if (randomNumberA3 == 1)
                {
                    A3 = C3 - (X + Y + Z);
                }
                if (randomNumberA3 == 1)
                {
                    C3 = A3 + (X + Y + Z);
                }

                //----------------------------------------
                if (randomNumberA3 == 2)
                {
                    A3 = C3 - (X + Y - Z);
                }
                if (randomNumberA3 == 2)
                {
                    C3 = A3 + (X + Y - Z);
                }
                //--------------------------------
                if (randomNumberA3 == 3)
                {
                    A3 = C3 - (X - Y + Z);
                }
                if (randomNumberA3 == 3)
                {
                    C3 = A3 + (X - Y + Z);
                }
                //---------------------------------
                if (randomNumberA3 == 4)
                {
                    A3 = C3 + (X + Y + Z);
                }
                if (randomNumberA3 == 4)
                {
                    C3 = A3 - (X + Y + Z);
                }
                //------------------------------------
                if (randomNumberA3 == 5)
                {
                    A3 = C3 + (X - Y - Z);
                }
                if (randomNumberA3 == 5)
                {
                    C3 = A3 - (X - Y - Z);
                }
                //----------------------------------
                if (randomNumberA3 == 6)
                {
                    A3 = C3 - (X - Y - Z);
                }
                if (randomNumberA3 == 6)
                {
                    C3 = A3 + (X - Y - Z);
                }
                //-------------------------------
                if (C3 != Convert.ToInt64(txt_c3.Text.Substring(0, 4)))
                {

                }
                string a3valu = Convert.ToString(A3);
                txt_a3.Text = a3valu;
                txt_d3.Text = txt_a3.Text.Substring(0, 1);
                txt_e3.Text = txt_a3.Text.Substring(1, a3valu.Length - 1);

                txt_ansc3.Text = Convert.ToString(C3);

                //---------Get A4--C4------------------------------------------------
                if (randomNumberA4 == 1)
                {
                    A4 = C4 - (X + Y + Z);
                }
                if (randomNumberA4 == 1)
                {
                    C4 = A4 + (X + Y + Z);
                }
                //-----------------------
                if (randomNumberA4 == 2)
                {
                    A4 = C4 - (X + Y - Z);
                }
                if (randomNumberA4 == 2)
                {
                    C4 = A4 + (X + Y - Z);
                }
                //-----------------------------
                if (randomNumberA4 == 3)
                {
                    A4 = C4 - (X - Y + Z);
                }
                if (randomNumberA4 == 3)
                {
                    C4 = A4 + (X - Y + Z);
                }
                //---------------------------------
                if (randomNumberA4 == 4)
                {
                    A4 = C4 + (X + Y + Z);
                }
                if (randomNumberA4 == 4)
                {
                    C4 = A4 - (X + Y + Z);
                }
                //-----------------------
                if (randomNumberA4 == 5)
                {
                    A4 = C4 + (X - Y - Z);
                }
                if (randomNumberA4 == 5)
                {
                    C4 = A4 - (X - Y - Z);
                }
                //-------------------------------
                if (randomNumberA4 == 6)
                {
                    A4 = C4 - (X - Y - Z);
                }
                if (randomNumberA4 == 6)
                {
                    C4 = A4 + (X - Y - Z);
                }
                //--------------------
                if (C4 != Convert.ToInt64(txt_c4.Text.Substring(0, 4)))
                {

                }
                string a4valu = Convert.ToString(A4);
                txt_a4.Text = a4valu;
                txt_d4.Text = txt_a4.Text.Substring(0, 1);
                txt_e4.Text = txt_a4.Text.Substring(1, a4valu.Length - 1);

                txt_ansc4.Text = Convert.ToString(C4);
                //----------GET A5-------------------------------------------------
                if (randomNumberA5 == 1)
                {
                    A5 = C5 - (X + Y + Z);
                }
                if (randomNumberA5 == 1)
                {
                    C5 = A5 + (X + Y + Z);
                }
                //--------------------------------------
                if (randomNumberA5 == 2)
                {
                    A5 = C5 - (X + Y - Z);
                }
                if (randomNumberA5 == 2)
                {
                    C5 = A5 + (X + Y - Z);
                }
                //-------------------------------------------
                if (randomNumberA5 == 3)
                {
                    A5 = C5 - (X - Y + Z);
                }
                if (randomNumberA5 == 3)
                {
                    C4 = A5 + (X - Y + Z);
                }
                //----------------------------------
                if (randomNumberA5 == 4)
                {
                    A5 = C5 + (X + Y + Z);
                }
                if (randomNumberA5 == 4)
                {
                    C5 = A5 - (X + Y + Z);
                }
                //----------------------------
                if (randomNumberA5 == 5)
                {
                    A5 = C5 + (X - Y - Z);
                }
                if (randomNumberA5 == 5)
                {
                    C5 = A5 - (X - Y - Z);
                }
                //--------------------------------
                if (randomNumberA5 == 6)
                {
                    A5 = C5 - (X - Y - Z);
                }
                if (randomNumberA5 == 6)
                {
                    C5 = A5 + (X - Y - Z);
                }
                //-----------------------
                if (C5 != Convert.ToInt64(txt_c5.Text.Substring(0, 4)))
                {

                }
                string a5valu = Convert.ToString(A5);
                txt_a5.Text = a5valu;
                txt_d5.Text = txt_a5.Text.Substring(0, 1);
                txt_e5.Text = txt_a5.Text.Substring(1, a5valu.Length - 1);

                txt_ansc5.Text = Convert.ToString(C5);
                //-----------------------------------------------------------------  
                txt_ansc.Text = txt_ansc1.Text + txt_ansc2.Text + txt_ansc3.Text + txt_ansc4.Text + txt_ansc5.Text;



                string D1 = txt_d1.Text;
                string D2 = txt_d2.Text;
                string D3 = txt_d3.Text;
                string D4 = txt_d4.Text;
                string D5 = txt_d5.Text;

                string E1 = txt_e1.Text;
                string E2 = txt_e2.Text;
                string E3 = txt_e3.Text;
                string E4 = txt_e4.Text;
                string E5 = txt_e5.Text;

                string F1 = Convert.ToString(randomNumberA1);
                string F2 = Convert.ToString(randomNumberA2);
                string F3 = Convert.ToString(randomNumberA3);
                string F4 = Convert.ToString(randomNumberA4);
                string F5 = Convert.ToString(randomNumberA5);
                Boolean CABCD = CompKeyIns.Insert_CompanyABCDetail(CompanyLoginId, txt_date.Text, txt_a1.Text, txt_a2.Text, txt_a3.Text, txt_a4.Text, txt_a5.Text, D1, D2, D3, D4, D5, E1, E2, E3, E4, E5, F1, F2, F3, F4, F5, txt_ansc1.Text, txt_ansc2.Text, txt_ansc3.Text, txt_ansc4.Text, txt_ansc5.Text, txt_ansc.Text, ServerId);
                startDate = startDate.AddDays(DayInterval);
                if (txt_ansc.Text == txt_c.Text)
                {
                }
                else
                {
                    //watch.Stop();
                    //lbl_msg.Text = watch.Elapsed.TotalSeconds.ToString();
                }
            }
        }
        else
        {
            lbl_msg.Text = "no data found in CompanyABCMaster";
        }
       
    }

    //protected void Insert_CompanyABCDetail(string CompanyLoginId, string Z, string A1, string A2, string A3, string A4, string A5, string D1, string D2, string D3, string D4, string D5, string E1, string E2, string E3, string E4, string E5, string F1, string F2, string F3, string F4, string F5, string txt_ansc1, string txt_ansc2, string txt_ansc3, string txt_ansc4, string txt_ansc5, string txt_ansc)
    //{
    //    if (con.State.ToString() != "Open")
    //    {
    //        con.Open();
    //    }
    //    SqlCommand cmd = new SqlCommand("CompanyABCDetail_AddDelUpdtSelect", con);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cmd.Parameters.AddWithValue("@StatementType", "Insert");
    //    cmd.Parameters.AddWithValue("@CompanyLoginId", CompanyLoginId);
    //    cmd.Parameters.AddWithValue("@Z", Z);
    //    cmd.Parameters.AddWithValue("@A1", A1);
    //    cmd.Parameters.AddWithValue("@A2", A2);
    //    cmd.Parameters.AddWithValue("@A3", A3);
    //    cmd.Parameters.AddWithValue("@A4", A4);
    //    cmd.Parameters.AddWithValue("@A5", A5);
    //    cmd.Parameters.AddWithValue("@D1", D1);
    //    cmd.Parameters.AddWithValue("@D2", D1);
    //    cmd.Parameters.AddWithValue("@D3", D3);
    //    cmd.Parameters.AddWithValue("@D4", D4);
    //    cmd.Parameters.AddWithValue("@D5", D5);
    //    cmd.Parameters.AddWithValue("@E1", E1);
    //    cmd.Parameters.AddWithValue("@E2", E2);
    //    cmd.Parameters.AddWithValue("@E3", E3);
    //    cmd.Parameters.AddWithValue("@E4", E4);
    //    cmd.Parameters.AddWithValue("@E5", E5);
    //    cmd.Parameters.AddWithValue("@F1", F1);
    //    cmd.Parameters.AddWithValue("@F2", F2);
    //    cmd.Parameters.AddWithValue("@F3", F3);
    //    cmd.Parameters.AddWithValue("@F4", F4);
    //    cmd.Parameters.AddWithValue("@F5", F5);

    //    cmd.Parameters.AddWithValue("@ansc1", txt_ansc1);
    //    cmd.Parameters.AddWithValue("@ansc2", txt_ansc2);
    //    cmd.Parameters.AddWithValue("@ansc3", txt_ansc3);
    //    cmd.Parameters.AddWithValue("@ansc4", txt_ansc4);
    //    cmd.Parameters.AddWithValue("@ansc5", txt_ansc5);

    //    cmd.Parameters.AddWithValue("@ansc", txt_ansc);

    //    cmd.ExecuteNonQuery();
    //    con.Close();
    //}
    //protected void Insert_CompanyABCMaster(string CompanyLoginId, string Z, string A1, string A2, string A3, string A4, string A5, string D1, string D2, string D3, string D4, string D5, string E1, string E2, string E3, string E4, string E5, string F1, string F2, string F3, string F4, string F5, string txt_ansc1, string txt_ansc2, string txt_ansc3, string txt_ansc4, string txt_ansc5, string txt_ansc)
    //{
        


    //    if (con.State.ToString() != "Open")
    //    {
    //        con.Open();
    //    }
    //    SqlCommand cmd = new SqlCommand("CompanyABCMaster_AddDelUpdtSelect", con);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cmd.Parameters.AddWithValue("@StatementType", "Insert");
    //    cmd.Parameters.AddWithValue("@CompanyLoginId", CompanyLoginId);
    //    cmd.Parameters.AddWithValue("@C", Z);
    //    cmd.Parameters.AddWithValue("@C1", A1);
    //    cmd.Parameters.AddWithValue("@C2", A2);
    //    cmd.Parameters.AddWithValue("@C3", A3);
    //    cmd.Parameters.AddWithValue("@C4", A2);
    //    cmd.Parameters.AddWithValue("@C5", A3);
    //    cmd.ExecuteNonQuery();
    //    con.Close();
    //}
    protected void addnewpanel_Click(object sender, EventArgs e)
    {
    }
    protected void addnewpanelC_Click(object sender, EventArgs e)
    {
    }
}
