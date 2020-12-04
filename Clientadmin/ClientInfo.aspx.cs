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
using System.Security.Cryptography;

public partial class ClientInfo : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    SqlCommand cmd1;
    DataTable dt;
    string FranCompanyname = "jobcenter";
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["Clientid"]==null)
        //{
        //    btnSubmit.Visible = true;
        //    btnUpdate.Visible = false;
        //}
        //else
        //{
        //    btnSubmit.Visible =false;
        //    btnUpdate.Visible = true;
        //}
        string pass = txtLoginPassword.Text;
        //lbllogin.Visible = false;
        txtLoginPassword.Attributes.Add("Value", pass);
        txtConfirmPassword.Attributes.Add("Value", pass);
        //lblimg.Text = "";
        FranCompanyname = "jobcenter";
        if (Request.QueryString["Franchisee"] != "" || Request.QueryString["Franchisee"] != "WWW" || Request.QueryString["Franchisee"] != "www")
        {
            FranCompanyname = Request.QueryString["Franchisee"];
        }
        else
        {
            FranCompanyname = "jobcenter";
        }
        if (!IsPostBack)
        {
            //----------  //Ijobcenter/ Ifile  Company Registretion-----------------------------------------------------------------------------
           
              Session["Pid"] = 9435;//Price Plan ID
              string sele = "select ProductMaster.ClientMasterId from ProductMaster inner join PricePlanMaster on PricePlanMaster.ProductId=ProductMaster.ProductId where PricePlanMaster.PricePlanId='"+ Session["Pid"] +"'";
            SqlCommand cmdsele = new SqlCommand(sele, con);
            con.Open();
            object clientid = cmdsele.ExecuteScalar();
            con.Close();
            string str221 = "SELECT  BusiwizMasterInfoTbl.*,ClientMaster.ClientURL from BusiwizMasterInfoTbl inner join ClientMaster on BusiwizMasterInfoTbl.ClientMasterId=ClientMaster.ClientMasterId  where BusiwizMasterInfoTbl.ClientMasterId='" + clientid.ToString() + "' ";
            SqlCommand cmd21 = new SqlCommand(str221, con);
            SqlDataAdapter adp11 = new SqlDataAdapter(cmd21);
            DataSet dt11 = new DataSet();
            adp11.Fill(dt11);
            if (dt11.Tables[0].Rows.Count > 0)
            {
                Textpaypal.Text = dt11.Tables[0].Rows[0]["PaypalID"].ToString();
                Textcancel.Text = dt11.Tables[0].Rows[0]["PaypalCancelURL"].ToString();
                Textreturn.Text = dt11.Tables[0].Rows[0]["PaypalReturnURL"].ToString();
                Textnotify.Text = dt11.Tables[0].Rows[0]["PaypalNotifyURL"].ToString();
                txtpaymentnotifyurl.Text = dt11.Tables[0].Rows[0]["PaymentNotifyURL"].ToString();
            }

            if (Session["Pid"] != null)
            {
                GetClientId();

                hdnProductId.Value = ViewState["ProductId"].ToString(); // Request.QueryString["PId"].ToString();
             //   fillterms();
               
            }
            else
            {

            }


            ViewState["upfile"] = "DefaultLogo.png";
            //------------------------------///////-----------///////---------/////----------------///////////////----------------------------------------------------------

            //---------------///////-----///////////////////////-----------------------///////////---------------------------------------------------------------------
            fillplan();

            FillddlCountry();
            //if (Session["ClientId"].ToString() != "")
            //{
            //    Request.QueryString["ClientId"] = Session["ClientId"].ToString();
            //}
            if (Session["ClientIdqqqq"] != null)
            {
                lblHead.Text = "Edit New IT Company Registration";
                FillInfo();
                btnSubmit.Visible = false;
                btnUpdate.Visible = true;
                string str22 = "SELECT * from BusiwizMasterInfoTbl where ClientMasterId='" + Session["ClientId"] + "' ";
                SqlCommand cmd2 = new SqlCommand(str22, con);
                SqlDataAdapter adp1 = new SqlDataAdapter(cmd2);
                DataSet dt1 = new DataSet();
                adp1.Fill(dt1);
                if (dt1.Tables[0].Rows.Count > 0)
                {
                    Textpaypalid.Text = dt1.Tables[0].Rows[0]["PaypalID"].ToString();
                    Textcancelurl.Text = dt1.Tables[0].Rows[0]["PaypalCancelURL"].ToString();
                    Textreturnurl.Text = dt1.Tables[0].Rows[0]["PaypalReturnURL"].ToString();
                    Textnotifyurl.Text = dt1.Tables[0].Rows[0]["PaypalNotifyURL"].ToString();
                    Textpaymentnotifyurl.Text = dt1.Tables[0].Rows[0]["PaymentNotifyURL"].ToString();
                }

            }
            else
            {
                lblHead.Text = "New IT Company Registration";
                btnUpdate.Visible = false;
                btnSubmit.Visible = true;

                Textcancelurl.Text = "http://paymentgateway.eplaza.us";
                Textreturnurl.Text = "http://paymentgateway.eplaza.us";
                Textnotifyurl.Text = "https://paymentgateway.eplaza.us";
                Textpaymentnotifyurl.Text = "http://licence.busiwiz.com/busiwizlicensekeygeneration.aspx?";

            }

            pass = txtLoginPassword.Text;
            //lbllogin.Visible = false;
            txtLoginPassword.Attributes.Add("Value", pass);
            txtConfirmPassword.Attributes.Add("Value", pass);
            string outgoing = outgoingserverpassword.Text;
            string ingoing = incomingserverpassword.Text;
            outgoingserverpassword.Attributes.Add("Value", outgoing);
            incomingserverpassword.Attributes.Add("Value", ingoing);
        }
    }
    protected void fillplan()
    {
        string str = " select * , PriceplanName + ' - ' + PricePlanAmount + ' $ ' as PriceplanName1 from  ClientPricePlanMaster";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        ddlsubscriptionplan.DataSource = ds;

        ddlsubscriptionplan.DataTextField = "PriceplanName1";
        ddlsubscriptionplan.DataValueField = "ClientPricePlanId";


        ddlsubscriptionplan.DataBind();
        ddlsubscriptionplan.Items.Insert(0, "-Select-");
        ddlsubscriptionplan.Items[0].Value = "0";
    }
    protected void FillInfo()
    {
        string str = " SELECT     ClientMaster.ClientMasterId, ClientMaster.CompanyName, ClientMaster.Address1, ClientMaster.Address2, ClientMaster.CountryId, ClientMaster.StateId, " +
                     " ClientMaster.City, ClientMaster.Zipcode,ClientMaster.OutgoingServerPort, ClientMaster.ContactPersonName, ClientMaster.Fax1, ClientMaster.Fax2, ClientMaster.Email1, ClientMaster.Email2, " +
                     " ClientMaster.Phone1, ClientMaster.Phone2, ClientMaster.ClientURL, ClientMaster.CustomerSupportURL, ClientMaster.SalesCustomerSupportURL, " +
                     " ClientMaster.SalesPhoneNo,ClientMaster.ServerName, ClientMaster.SalesFaxNo, ClientMaster.SalesEmail, ClientMaster.AfterSalesSupportPhoneNo, ClientMaster.AfterSalesSupportFaxNo, " +
                     " ClientMaster.AfterSalesSupportEmail, ClientMaster.TechSupportPhoneNo, ClientMaster.TechSupportFaxNo, ClientMaster.TechSupportEmail, ClientMaster.FTP, " +
                     " ClientMaster.FTPUserName, ClientMaster.FTPPassword, ClientMaster.LoginName, ClientMaster.LoginPassword, CountryMaster.CountryId AS Expr1, " +
                     " CountryMaster.CountryName, CountryMaster.Country_Code, StateMasterTbl.StateId AS Expr2, StateMasterTbl.StateName, StateMasterTbl.CountryId AS Expr3, " +
                     " StateMasterTbl.State_Code ,ClientMaster.ClientPricePlanId,ClientMaster.CompanyLogo,ClientMaster.EmailID,ClientMaster.IncomingServerPOP,ClientMaster.IncomingServerUSerID,ClientMaster.IncolingServerPasword,ClientMaster.OurgoingServerSMTP,ClientMaster.OutgoingServerUserID,ClientMaster.OutgoingServerPassword  FROM   StateMasterTbl INNER JOIN " +
                      " CountryMaster ON StateMasterTbl.CountryId = CountryMaster.CountryId RIGHT OUTER JOIN " +
                      " ClientMaster ON StateMasterTbl.StateId = ClientMaster.StateId AND CountryMaster.CountryId = ClientMaster.CountryId " +
                             " WHERE (ClientMaster.ClientMasterId = '" + Convert.ToInt32(Session["ClientId"].ToString()) + "')";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtCompanyName.Enabled = false;
            txtCompanyName.Text = ds.Tables[0].Rows[0]["CompanyName"].ToString();
            txtAdrs1.Text = ds.Tables[0].Rows[0]["Address1"].ToString();
            txtAdrs2.Text = ds.Tables[0].Rows[0]["Address2"].ToString();
            ddlCountry.SelectedIndex = ddlCountry.Items.IndexOf(ddlCountry.Items.FindByValue(ds.Tables[0].Rows[0]["CountryId"].ToString()));
            FillddlState();
            ddlState.SelectedIndex = ddlState.Items.IndexOf(ddlState.Items.FindByValue(ds.Tables[0].Rows[0]["StateId"].ToString()));
            //txtCountry.Text = ds.Tables[0].Rows[0]["CountryName"].ToString();
            //txtState.Text = ds.Tables[0].Rows[0]["StateName"].ToString();
            txtCity.Text = ds.Tables[0].Rows[0]["City"].ToString();
            txtZipcode.Text = ds.Tables[0].Rows[0]["Zipcode"].ToString();
            txtContactPerson.Text = ds.Tables[0].Rows[0]["ContactPersonName"].ToString();
            txtFax1.Text = ds.Tables[0].Rows[0]["Fax1"].ToString();
            txtFax2.Text = ds.Tables[0].Rows[0]["Fax2"].ToString();
            txtEmail1.Enabled = false;
            txtEmail1.Text = ds.Tables[0].Rows[0]["Email1"].ToString();
            txtEmail2.Text = ds.Tables[0].Rows[0]["Email2"].ToString();
            txtPhone1.Text = ds.Tables[0].Rows[0]["Phone1"].ToString();
            txtPhone2.Text = ds.Tables[0].Rows[0]["Phone2"].ToString();
            txtClientUrl.Text = ds.Tables[0].Rows[0]["ClientURL"].ToString();
            txtLoginName.Text = ds.Tables[0].Rows[0]["LoginName"].ToString();
            ViewState["empuserid"] = txtLoginName.Text;
            txtLoginPassword.Text = ds.Tables[0].Rows[0]["LoginPassword"].ToString();
            txtContactPerson.Text = ds.Tables[0].Rows[0]["ContactPersonName"].ToString();
            ddlsubscriptionplan.Enabled = false;
            ddlsubscriptionplan.SelectedIndex = ddlsubscriptionplan.Items.IndexOf(ddlsubscriptionplan.Items.FindByValue(ds.Tables[0].Rows[0]["ClientPricePlanId"].ToString()));
            txtConfirmPassword.Text = ds.Tables[0].Rows[0]["LoginPassword"].ToString();
            txtservername.Text = ds.Tables[0].Rows[0]["ServerName"].ToString();

            Txtmastereid.Text = ds.Tables[0].Rows[0]["EmailID"].ToString();
            txtincomingserver.Text = ds.Tables[0].Rows[0]["IncomingServerPOP"].ToString();
            txtincomingserveruserid.Text = ds.Tables[0].Rows[0]["IncomingServerUSerID"].ToString();
            incomingserverpassword.Text = ds.Tables[0].Rows[0]["IncolingServerPasword"].ToString();
            txtoutgoingserver.Text = ds.Tables[0].Rows[0]["OurgoingServerSMTP"].ToString();
            txtoutgoingserveruserid.Text = ds.Tables[0].Rows[0]["OutgoingServerUserID"].ToString();
            outgoingserverpassword.Text = ds.Tables[0].Rows[0]["OutgoingServerPassword"].ToString();
            
            ViewState["upfile"] = ds.Tables[0].Rows[0]["CompanyLogo"].ToString();
            txtoutgoingserverport.Text = ds.Tables[0].Rows[0]["OutgoingServerPort"].ToString();
        }
    }
    protected void FillddlCountry()
    {
        string strcountry = "SELECT CountryId,CountryName,Country_Code FROM CountryMaster";
        SqlCommand cmdcountry = new SqlCommand(strcountry, con);
        DataTable dtcountry = new DataTable();
        SqlDataAdapter adpcountry = new SqlDataAdapter(cmdcountry);
        adpcountry.Fill(dtcountry);
        ddlCountry.DataSource = dtcountry;
        ddlCountry.DataTextField = "CountryName";
        ddlCountry.DataValueField = "CountryId";
        ddlCountry.DataBind();
        ddlCountry.Items.Insert(0, "-Select-");
        ddlCountry.Items[0].Value = "0";
    }
    protected void FillddlState()
    {
        string strcountry = "SELECT Country_Code FROM CountryMaster where CountryId ='" + Convert.ToInt32(ddlCountry.SelectedValue) + "'";
        SqlCommand cmdcountry = new SqlCommand(strcountry, con);
        con.Open();
        object hh = cmdcountry.ExecuteScalar();
        ViewState["contry_code"] = hh.ToString();
        con.Close();
        string strstate = "SELECT StateId,StateName,CountryId,State_Code  FROM StateMasterTbl where CountryId =" + Convert.ToInt32(ddlCountry.SelectedValue) + " ";
        SqlCommand cmdstate = new SqlCommand(strstate, con);
        DataTable dtstate = new DataTable();
        SqlDataAdapter adpstate = new SqlDataAdapter(cmdstate);
        adpstate.Fill(dtstate);
        ddlState.DataSource = dtstate;
        ddlState.DataTextField = "StateName";
        ddlState.DataValueField = "StateId";
        ddlState.DataBind();
        ddlState.Items.Insert(0, "-Select-");
        ddlState.Items[0].Value = "0";
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string status;
        try
        {

            if (Convert.ToInt32(lblamt.Text) == 0)
            {
                status = "Active";
            }
            else
            {
                status = "Inactive";
            }
            ViewState["comname"] = txtCompanyName.Text;
            ViewState["logourl"] = txtClientUrl.Text;

            string str12 = "";
            str12 = "Select ClientMaster.* from ClientMaster where  LoginName='" + txtLoginName.Text + "'  and CompanyName='" + txtCompanyName.Text + " '";
            SqlCommand cmd12 = new SqlCommand(str12, con);
            SqlDataAdapter adp12 = new SqlDataAdapter(cmd12);
            DataTable ds12 = new DataTable();
            adp12.Fill(ds12);
            if (ds12.Rows.Count > 0)
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Client is with same User name and Company Name is already exists ,So Please select another name...";
            }
            else
            {
                string str = "INSERT INTO ClientMaster " +
                   "(CompanyName,Address1,Address2,CountryId,StateId,City,Zipcode,ContactPersonName,Fax1,Fax2,Email1,Email2,Phone1,Phone2 ," +
                   " ClientURL,CustomerSupportURL,SalesCustomerSupportURL,SalesPhoneNo,SalesFaxNo,SalesEmail,AfterSalesSupportPhoneNo,AfterSalesSupportFaxNo,AfterSalesSupportEmail " +
                   ",TechSupportPhoneNo,TechSupportFaxNo,TechSupportEmail,FTP,FTPUserName,FTPPassword, LoginName,LoginPassword , ClientPricePlanId,Status,ServerName,CompanyLogo,EmailID,IncomingServerPOP,IncomingServerUSerID,IncolingServerPasword,OurgoingServerSMTP,OutgoingServerUserID,OutgoingServerPassword,OutgoingServerPort)" +
                   "VALUES('" + txtCompanyName.Text + "','" + txtAdrs1.Text + "','" + txtAdrs2.Text + "'," + Convert.ToInt32(ddlCountry.SelectedValue) + "," + Convert.ToInt32(ddlState.SelectedValue) + ",'" + txtCity.Text + "','" + txtZipcode.Text + "','" + txtContactPerson.Text + "','" + txtFax1.Text + "','" + txtFax2.Text + "','" + txtEmail1.Text + "','" + txtEmail2.Text + "','" + txtPhone1.Text + "','" + txtPhone2.Text + "'," +
                   "'" + txtClientUrl.Text + "','','','','' , '', '' ,'', ''," +
                   "'' , '','','','','','" + txtLoginName.Text + "','" + PageMgmt.Encrypted(txtLoginPassword.Text) + "','" + ddlsubscriptionplan.SelectedItem.Value.ToString() + "',' " + status + "','" + txtservername.Text + "','" + ViewState["upfile"] + "','" + Txtmastereid.Text + "','" + txtincomingserver.Text + "','" + txtincomingserveruserid.Text + "','" + incomingserverpassword.Text + "','" + txtoutgoingserver.Text + "','" + txtoutgoingserveruserid.Text + "','" + outgoingserverpassword.Text + "','" + txtoutgoingserverport.Text + "')";
                SqlCommand cmd = new SqlCommand(str, con);
                DataTable dt = new DataTable();
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                insertbuz();
                lblmsg.Visible = true;
                lblmsg.Text = "Client is created Successfully.";


                str = "SELECT   top 1 ClientMasterId from ClientMaster  order by ClientMasterId desc";
                cmd = new SqlCommand(str, con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ViewState["ClientmasterId"] = ds.Tables[0].Rows[0]["ClientmasterId"].ToString();
                    txtcompanyid.Text = ds.Tables[0].Rows[0]["ClientmasterId"].ToString();
                    string strpaypalAtmp1x = "insert into clientLoginMaster(clientId,UserId,Password)values('" + ds.Tables[0].Rows[0]["ClientmasterId"].ToString() + "','" + txtLoginName.Text + "','" + PageMgmt.Encrypted(txtLoginPassword.Text) + "')";
                    SqlCommand cmdpapalatmp1x = new SqlCommand(strpaypalAtmp1x, con);
                    con.Open();
                    cmdpapalatmp1x.ExecuteNonQuery();
                    con.Close();

                    string strpaypalAtmp1 = "insert into paymentinfo(PaypalEmailId,ClientMasterId,websitename,amount,contarycode)values('" + Textpaypalid.Text + "', '" + ds.Tables[0].Rows[0]["ClientmasterId"].ToString() + "' ," +
                        " '" + ViewState["logourl"] + "','" + String.Format("{0:0.00} ", lblamt.Text) + "','" + ViewState["contry_code"] + "')";
                    SqlCommand cmdpapalatmp1 = new SqlCommand(strpaypalAtmp1, con);
                    con.Open();
                    cmdpapalatmp1.ExecuteNonQuery();
                    con.Close();
                    if (Convert.ToInt32(lblamt.Text) <= 0)
                    {
                        fillmail();
                      //  Response.Redirect("busiwizlicensekeygeneration.aspx?orz=" + ds.Tables[0].Rows[0]["ClientmasterId"].ToString() + "");

                        btncontinue_Click(sender, e);


                    }
                    string str5 = "SELECT paymentinfoid FROM  paymentinfo where ClientMasterId='" + ds.Tables[0].Rows[0]["ClientmasterId"].ToString() + "'";
                    SqlCommand cmd5 = new SqlCommand(str5, con);
                    con.Open();
                    int i = Convert.ToInt32(cmd5.ExecuteScalar());
                    con.Close();
                    ClearAll();
                    Response.Redirect("http://paymentgateway.safestserver.com/paymentnow.aspx?payid=" + i + "");
                    fillmail();
                    //Response.Redirect("OrderClientInfo.aspx?ClientId="+ ds.Tables[0].Rows[0]["ClientmasterId"].ToString() ) ;
                }
            }
        }
        catch (Exception err)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Error ;" + err.Message;
        }
    }

    //protected void insertbuz()
    //{  
    //    string str22 = "SELECT * from BusiwizMasterInfoTbl ";
    //    SqlCommand cmd2 = new SqlCommand(str22, con);
    //    SqlDataAdapter adp1 = new SqlDataAdapter(cmd2);
    //    DataSet dt1 = new DataSet();
    //    adp1.Fill(dt1);
    //    if (dt1.Tables[0].Rows.Count ==0)
    //    {
    //        string buz = "insert into BusiwizMasterInfoTbl(Name,LogoURL,PaypalID,PaypalNotifyURL,PaypalCancelURL,PaypalReturnURL,PaymentNotifyURL)values('" + ViewState["comname"] + "','" + ViewState["logourl"] + "','" + Textpaypalid.Text.Trim() + "','" + Textnotifyurl.Text.Trim() + "','" + Textcancelurl.Text.Trim() + "','" + Textreturnurl.Text.Trim() + "','" + Textpaymentnotifyurl.Text.Trim() + "')";
    //        SqlCommand cmd1 = new SqlCommand(buz, con);
    //        con.Open();
    //        cmd1.ExecuteNonQuery();
    //        con.Close();
    //    }
    //    else
    //    {

    //        string buz = "update BusiwizMasterInfoTbl set Name='" + ViewState["comname"] + "',LogoURL='" + ViewState["logourl"] + "',PaypalID='" + Textpaypalid.Text.Trim() + "',PaypalNotifyURL='" + Textnotifyurl.Text.Trim() + "',PaypalCancelURL='" + Textcancelurl.Text.Trim() + "',PaypalReturnURL='" + Textreturnurl.Text.Trim() + "',PaymentNotifyURL='" + Textpaymentnotifyurl.Text.Trim() + "' where ID= '" + dt1.Tables[0].Rows[0]["ID"] + "'";
    //        SqlCommand cmd1 = new SqlCommand(buz, con);
    //        con.Open();
    //        cmd1.ExecuteNonQuery();
    //        con.Close();
    //    }
    //}
    protected void insertbuz()
    {
        string sele = "Select ClientMasterId  from ClientMaster where LoginName='" + txtLoginName.Text + "' and LoginPassword='" + PageMgmt.Encrypted(txtLoginPassword.Text) + "'";
        SqlCommand cmdsele = new SqlCommand(sele, con);
        con.Open();
        object clientid = cmdsele.ExecuteScalar();
        con.Close();
        string buz = "insert into BusiwizMasterInfoTbl(ClientMasterId,Name,LogoURL,PaypalID,PaypalNotifyURL,PaypalCancelURL,PaypalReturnURL,PaymentNotifyURL)values('" + clientid.ToString() + "','" + ViewState["comname"] + "','" + ViewState["logourl"] + "','" + Textpaypalid.Text.Trim() + "','" + Textnotifyurl.Text.Trim() + "','" + Textcancelurl.Text.Trim() + "','" + Textreturnurl.Text.Trim() + "','" + Textpaymentnotifyurl.Text.Trim() + "')";
        SqlCommand cmd1 = new SqlCommand(buz, con);
        con.Open();
        cmd1.ExecuteNonQuery();
        con.Close();
    }

    protected void updatebiz()
    {
        string buz = "update BusiwizMasterInfoTbl set Name='" + ViewState["comname"] + "',LogoURL='" + ViewState["logourl"] + "',PaypalID='" + Textpaypalid.Text.Trim() + "',PaypalNotifyURL='" + Textnotifyurl.Text.Trim() + "',PaypalCancelURL='" + Textcancelurl.Text.Trim() + "',PaypalReturnURL='" + Textreturnurl.Text.Trim() + "',PaymentNotifyURL='" + Textpaymentnotifyurl.Text.Trim() + "' where ClientMasterId= '" + Session["ClientId"].ToString() + "'";
        SqlCommand cmd1 = new SqlCommand(buz, con);
        con.Open();
        cmd1.ExecuteNonQuery();
        con.Close();

    }

    protected void ClearAll()
    {
        txtCompanyName.Text = "";
        txtAdrs1.Text = "";
        txtAdrs2.Text = " ";
        ddlCountry.SelectedIndex = 0;
        //ddlState.SelectedIndex = 0 ;
        ddlState.DataSource = null;
        ddlState.DataBind();
        txtCity.Text = "";
        txtZipcode.Text = "";
        txtContactPerson.Text = "";
        txtFax1.Text = "";
        txtFax2.Text = "";
        txtEmail1.Text = "";
        txtEmail2.Text = "";
        txtPhone1.Text = "";
        txtPhone2.Text = "";
        txtClientUrl.Text = "";
        //txtCustSupportURL.Text  = "" ;
        //txtSalesSupportURL.Text = "";
        //txtSalesPhoneNO.Text = "";
        //txtSalesFaxNo.Text = "";
        //txtSalesEmail.Text = "";
        //txtafterSalesSupPhoneNO.Text = "";
        //txtAfterSalesSupFaxNo.Text = "";
        //txtAfterSalesSupEmail.Text = "";
        //txtTechSupportPhoneNo.Text = "";
        //txtTechSupportFaxNo.Text = "";
        //txtTechSupEmail.Text = "";
        //txtFTP.Text = "";
        //txtFTPUserName.Text = "";
        //txtFTPPassword.Text ="";                          
    }
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillddlState();
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string status = "";

        lblamt.Text = "0";
        try
        {
            if (Convert.ToInt32(lblamt.Text) == 0)
            {
                status = "Active";
            }
            else
            {
                status = "Inactive";
            }
            ViewState["comname"] = txtCompanyName.Text;
            ViewState["logourl"] = txtClientUrl.Text;


            string str12 = "";
            str12 = "Select ClientMaster.* from ClientMaster where  LoginName='" + txtLoginName.Text + "'  and CompanyName='" + txtCompanyName.Text + " ' and ClientMasterId <> '" + Session["ClientId"].ToString() + "'";
            SqlCommand cmd12 = new SqlCommand(str12, con);
            SqlDataAdapter adp12 = new SqlDataAdapter(cmd12);
            DataTable ds12 = new DataTable();
            adp12.Fill(ds12);
            if (ds12.Rows.Count > 0)
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Client is with same User name and Company Name is already exists ,So Please select another name...";
            }
            else
            {

                string str = "update   ClientMaster  set " +
                   "CompanyName='" + txtCompanyName.Text + "',Address1='" + txtAdrs1.Text + "',Address2 ='" + txtAdrs2.Text + "', CountryId=" + Convert.ToInt32(ddlCountry.SelectedValue) + ",StateId=" + Convert.ToInt32(ddlState.SelectedValue) + ",City ='" + txtCity.Text + "' ,Zipcode='" + txtZipcode.Text + "',ContactPersonName ='" + txtContactPerson.Text + "',Fax1='" + txtFax1.Text + "',Fax2='" + txtFax2.Text + "',Email1='" + txtEmail1.Text + "',Email2='" + txtEmail2.Text + "',Phone1='" + txtPhone1.Text + "',Phone2='" + txtPhone2.Text + "' ," +
                   " ClientURL='" + txtClientUrl.Text + "',CustomerSupportURL ='',SalesCustomerSupportURL ='' ,SalesPhoneNo ='', SalesFaxNo =''  ,SalesEmail ='', AfterSalesSupportPhoneNo ='' ,AfterSalesSupportFaxNo ='',AfterSalesSupportEmail =''  " +
                   ",TechSupportPhoneNo='',TechSupportFaxNo='',TechSupportEmail ='',FTP='',FTPUserName='',FTPPassword='', LoginName='" + txtLoginName.Text + "',LoginPassword ='" + txtLoginPassword.Text + "' ,ClientPricePlanId= '" + ddlsubscriptionplan.SelectedItem.Value.ToString() + "',Status='" + status + "',ServerName='" + txtservername.Text + "',CompanyLogo='" + ViewState["upfile"] + "',EmailID='" + Txtmastereid.Text + "',IncomingServerPOP='" + txtincomingserver.Text + "', IncomingServerUSerID='" + txtincomingserveruserid.Text + "',IncolingServerPasword='" + incomingserverpassword.Text + "',OurgoingServerSMTP='" + txtoutgoingserver.Text + "',OutgoingServerUserID='" + txtoutgoingserveruserid.Text + "',OutgoingServerPassword='" + outgoingserverpassword.Text + "' , OutgoingServerPort = ' " + txtoutgoingserverport.Text + "' " +
                   "where ClientMasterId =" + Session["ClientId"].ToString();
                SqlCommand cmd = new SqlCommand(str, con);
                DataTable dt = new DataTable();
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                string strpaypalAtmp1xx = "update clientLoginMaster Set UserId='" + txtLoginName.Text + "',Password='" + txtLoginPassword.Text + "' where clientId='" + Session["ClientId"].ToString() + "' and UserId='" + ViewState["empuserid"] + "'";
                SqlCommand cmdpapalatmp1xx = new SqlCommand(strpaypalAtmp1xx, con);
                con.Open();
                cmdpapalatmp1xx.ExecuteNonQuery();
                con.Close();
                //insertbuz();
                updatebiz();
                lblmsg.Visible = true;
                lblmsg.Text = "Client is Updated Successfully.";

                str = "SELECT  ClientMasterId from ClientMaster where ClientMasterId ='" + Session["ClientId"].ToString() + "' order by ClientMasterId desc";
                cmd = new SqlCommand(str, con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string strpaypalAtmp1 = "update paymentinfo set PaypalEmailId='" + Textpaypalid.Text + "',websitename='" + ViewState["logourl"] + "',amount='" + String.Format("{0:0.00} ", lblamt.Text) + "',contarycode='" + ViewState["contry_code"] + "' where ClientMasterId='" + ds.Tables[0].Rows[0]["ClientmasterId"].ToString() + "' ";
                    SqlCommand cmdpapalatmp1 = new SqlCommand(strpaypalAtmp1, con);
                    con.Open();
                    cmdpapalatmp1.ExecuteNonQuery();
                    con.Close();
                    if (Convert.ToInt32(lblamt.Text) <= 0)
                    {

                        Response.Redirect("busiwizlicensekeygeneration.aspx?orz=" + ds.Tables[0].Rows[0]["ClientmasterId"].ToString() + "");


                    }
                    string str5 = "SELECT paymentinfoid FROM  paymentinfo where ClientMasterId='" + ds.Tables[0].Rows[0]["ClientmasterId"].ToString() + "'";
                    SqlCommand cmd5 = new SqlCommand(str5, con);
                    con.Open();
                    int i = Convert.ToInt32(cmd5.ExecuteScalar());
                    con.Close();
                    Response.Redirect("http://paymentgateway.safestserver.com/paymentnow.aspx?payid=" + i + "");

                    // Response.Redirect("OrderClientInfo.aspx?ClientId="+ ds.Tables[0].Rows[0]["ClientmasterId"].ToString() ) ;
                }

                //Response.Redirect("OrderClientInfo.aspx?ClientId=" + Request.QueryString["ClientId"].ToString());
            }
        }
        catch (Exception err)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Error ;" + err.Message;
        }
    }

    protected void ddlsubscriptionplan_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlsubscriptionplan.SelectedIndex > 0)
        {
            string str = "SELECT *  FROM  ClientPricePlanMaster " +
                         " WHERE (ClientPricePlanId = '" + Convert.ToInt32(ddlsubscriptionplan.SelectedItem.Value.ToString()) + "')";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblamt.Text = ds.Tables[0].Rows[0]["PricePlanAmount"].ToString();
            }
        }
        else
        {
            lblamt.Text = "";
        }
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox1.Checked == true)
        {
            pnlservername.Visible = true;
        }
        else
        {
            pnlservername.Visible = false;
        }
    }
    protected void Butsubmit_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            //  FileUpload1.PostedFile.SaveAs(Server.MapPath("ftp://192.168.1.219:29/images/") + FileUpload1.FileName);
            FileUpload1.PostedFile.SaveAs(Server.MapPath("images\\") + FileUpload1.FileName);
            ViewState["upfile"] = FileUpload1.FileName;
            lblimg.Text = "Upload Image Successfully";
        }
    }

      public void sendmail(string To)
    {


        //  string str = "select distinct  ClientMaster.*,PricePlanMaster.PricePlanName,CompanyMaster.PlanId,CompanyMaster.Adminid,CompanyMaster.Password,CompanyMaster.CompanyLoginId,CompanyMaster.CompanyName as cname,CompanyMaster.ContactPerson as cperson, CompanyMaster.Email from  CompanyMaster inner join PricePlanMaster  on CompanyMaster.PricePlanId=PricePlanMaster.PricePlanId inner join ProductMaster on PricePlanMaster.ProductId=ProductMaster.ProductId inner join ClientMaster on ProductMaster.ClientMasterId=ClientMaster.ClientMasterId WHERE(CompanyLoginId = '" + Request.QueryString["cid"] + "') ";
        string str = " select distinct ClientMaster.*,ClientPricePlanMaster.PricePlanName,StateMasterTbl.StateName,CountryMaster.CountryName from  ClientMaster inner join ClientPricePlanMaster  on ClientPricePlanMaster.ClientPricePlanId=ClientMaster.ClientPricePlanID inner join StateMasterTbl on StateMasterTbl.StateId=ClientMaster.StateId inner join CountryMaster on ClientMaster.CountryId=CountryMaster.CountryId WHERE ClientMaster.ClientMasterId='"+ViewState["ClientmasterId"].ToString()+"' ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        if (dt.Rows.Count > 0)
        {
           // string struu = "insert into ClientOrderPaymentStatus(ClientMasterId,TransactionID)values('11','" + dt.Rows[0]["Email"].ToString() + "')";
           // SqlCommand cmduu = new SqlCommand(struu, con);
           // con.Open();
            //cmduu.ExecuteNonQuery();
           // con.Close();
            StringBuilder strhead = new StringBuilder();
            strhead.Append("<table width=\"100%\"> ");
            strhead.Append("<tr><td align=\"left\"> <img src=\"http://license.busiwiz.com/images/" + dt.Rows[0]["CompanyLogo"].ToString() + "\" \"border=\"0\" Width=\"200px\" Height=\"125px\" / > </td ><td Width=\"200px\"></td><td align=\"Centre\"><br><span style=\"color: #996600\">" + dt.Rows[0]["CompanyName"].ToString() + "</span></b><Br>" + dt.Rows[0]["Address1"].ToString() + "<Br>" + dt.Rows[0]["Address2"].ToString() + "<Br><Br>" + dt.Rows[0]["city"].ToString() + "<Br>" + dt.Rows[0]["Statename"].ToString() + "<Br>" + dt.Rows[0]["CountryName"].ToString() + "<Br>" + dt.Rows[0]["Phone1"].ToString() + "," + dt.Rows[0]["Phone2"].ToString() + "<Br>" + dt.Rows[0]["Fax1"].ToString() + "," + dt.Rows[0]["Fax2"].ToString() + "<Br>" + dt.Rows[0]["Email1"].ToString() + "," + dt.Rows[0]["Email2"].ToString() + "<Br>" + dt.Rows[0]["ClientURL"].ToString() + " </td></tr>  ");
            strhead.Append("<br></table> ");

            strhead.Append("<br><table width=\"100%\"> ");
            strhead.Append("<tr><td align=\"left\">You have successfully configured your account.<br></td></tr>");
            strhead.Append("</table> ");

          
            string body = "The following is your site login information.";

            //string stplan = "SELECT * from PricePlanMaster WHERE(PricePlanId = '" + dt.Rows[0]["PlanId"].ToString() + "')";
            //string stplan = "select VersionInfoMaster.VersionInfoName,ProductMaster.ProductName,ProductMaster.ClientMasterId,PricePlanMaster.* from VersionInfoMaster inner join ProductMaster  on VersionInfoMaster.productid=ProductMaster.productId join PricePlanMaster on PricePlanMaster.VersionInfoMasterId=VersioninfoMaster.VersionInfoId where PricePlanId='" + dt.Rows[0]["PlanId"].ToString() + "'";

            //SqlCommand cmdplan = new SqlCommand(stplan, con);
            //SqlDataAdapter adpplan = new SqlDataAdapter(cmdplan);
            //DataTable dtplan = new DataTable();
            //adpplan.Fill(dtplan);
            //StringBuilder strplan = new StringBuilder();
            //if (dtplan.Rows.Count > 0)
            //{

            //    double amt = 0;
            //    string stplan11 = "select * from Payperorderplansubscriptiontbl where priceplanid='" + dt.Rows[0]["PlanId"].ToString() + "' and CustomerID='" + Request.QueryString["cid"] + "'";

            //    SqlCommand cmdplan11 = new SqlCommand(stplan11, con);
            //    SqlDataAdapter adpplan11 = new SqlDataAdapter(cmdplan11);
            //    DataTable dtplan11 = new DataTable();
            //    adpplan11.Fill(dtplan11);
            //    if (dtplan11.Rows.Count > 0)
            //    {
            //        amt = Convert.ToDouble(dtplan11.Rows[0]["AmountPaid"].ToString());
            //    }
            //    else
            //    {
            //        amt = Convert.ToDouble(dtplan.Rows[0]["PricePlanAmount"].ToString());
            //    }
            //   // strplan.Append("<table width=\"100%\"> ");
            //    //strplan.Append("<tr><td align=\"left\"><b>Product & Version Name:</b><b><span style=\"color: #996600\">" + dtplan.Rows[0]["ProductName"].ToString() + " & " + " " + dtplan.Rows[0]["VersionInfoName"].ToString() + "</span></b><br><b>Plan Name:</b>" + dtplan.Rows[0]["PricePlanName"].ToString() + "</b><Br><b>Plan Description:</b>" + dtplan.Rows[0]["PricePlanDesc"].ToString() + "<Br><b>Plan Start Date:</b>" + DateTime.Now.ToShortDateString() + "<Br><b>Validity Period(Month):</b>" + dtplan.Rows[0]["DurationMonth"].ToString() + "<Br><b>Plan Amount:</b>" + amt + "</td></tr> ");
            //    //strplan.Append("</table> ");
            //}
            //string license = "select CompanyMaster.CompanyId,LicenseMaster.LicenseKey from CompanyMaster inner join LicenseMaster on LicenseMaster.CompanyId=CompanyMaster.CompanyId where CompanyLoginId='" + ViewState["comid"].ToString() + "'";

            //SqlCommand cmdl = new SqlCommand(license, con);
            //SqlDataAdapter adl = new SqlDataAdapter(cmdl);
            //DataTable dtl = new DataTable();
            //adl.Fill(dtl);
            string bodytext = "<b> Your License.Busiwiz Login Url : </b> <a href=http://license.Busiwiz.com target=_blank > Login now<br></a>";


            //string bodytext1 = "&nbsp;You need to activate your license key now  <a href=http://busiwiz.eplaza.us/License.aspx target=_blank > configure now<br></a>";
            //string que = "<br><br><centre>&nbsp;If you have any technical issue with the license or usage of the site , Please Email <a href=mailto:techsupport@busiwiz.com >techsupport@busiwiz.com </a></centre><br><br>&nbsp;Thanking you.<br><br>&nbsp;Sincerly";

            StringBuilder support = new StringBuilder();
            support.Append("<table width=\"100%\"> ");
            support.Append("<tr><td align=\"left\"><b>Client Company Name: </b><b><span style=\"color: #996600\">" + dt.Rows[0]["CompanyName"].ToString() + "</span></b><br><b>Login User Name :</b>" + dt.Rows[0]["LoginName"].ToString() + "<Br><b>Login Password :</b>" + dt.Rows[0]["LoginPassword"].ToString() + "<Br></td></tr>  ");
            support.Append("</table> ");

            string bodyformate = "" + strhead + "" + "<br>" + body + "<br>" + "" + "<br>" + bodytext + "<br>" + "" + support + "";
          
            //string struu3 = "insert into ClientOrderPaymentStatus(ClientMasterId,TransactionID)values('12','Welcomemessage')";
            //SqlCommand cmduu3 = new SqlCommand(struu3, con);
            //con.Open();
            //cmduu3.ExecuteNonQuery();
            //con.Close();
            try
            {

                MailAddress to = new MailAddress(To);
                MailAddress from = new MailAddress(dt.Rows[0]["OutgoingServerUserID"].ToString());
                MailMessage objEmail = new MailMessage(from, to);
                objEmail.Subject = "Welcome " + dt.Rows[0]["CompanyName"].ToString() + "  - Registration";

                objEmail.Body = bodyformate.ToString();
                objEmail.IsBodyHtml = true;
                objEmail.Priority = MailPriority.High;

                SmtpClient client = new SmtpClient();
                client.Credentials = new NetworkCredential(dt.Rows[0]["OutgoingServerUserID"].ToString(), dt.Rows[0]["OutgoingServerPassword"].ToString());
                client.Host = dt.Rows[0]["OurgoingServerSMTP"].ToString();
                client.Send(objEmail);

            }
            catch (Exception e)
            {
                //string struu1 = "insert into ClientOrderPaymentStatus(ClientMasterId,TransactionID)values('67','" + e.ToString() + "')";
                //SqlCommand cmduu1 = new SqlCommand(struu1, con);
                //con.Open();
                //cmduu.ExecuteNonQuery();
                //con.Close();
            }
        }


    }
      protected void fillmail()
      {
          
                //order = "SELECT   top 1 ClientMasterId from ClientMaster  order by ClientMasterId desc";
                //cmd = new SqlCommand(str, con);
                //SqlDataAdapter adp = new SqlDataAdapter(cmd);
                //DataSet ds = new DataSet();
                //adp.Fill(ds);
                //if (ds.Tables[0].Rows.Count > 0)
                //{
          
          //string order = "select * from paymentinfo where ordermasterid='" + ViewState["saleorderold"] + "' and  ClientMasterId IS NULL";
          //string order = "select * from OrderMaster where CompanyLoginId='" + ViewState["comid"].ToString() + "' ";

          string order = "SELECT   top 1 ClientMasterId,Email1 from ClientMaster  order by ClientMasterId desc";
          SqlCommand cmdor = new SqlCommand(order, con);
          SqlDataAdapter adpor = new SqlDataAdapter(cmdor);
          DataTable dtorder = new DataTable();
          adpor.Fill(dtorder);
          if (dtorder.Rows.Count > 0)
          {
              sendmail(dtorder.Rows[0]["Email1"].ToString());
          }
      }



      protected void btncontinue_Click(object sender, EventArgs e)
      {

                      string sele = "select ProductMaster.ProductId from ProductMaster inner join PricePlanMaster on PricePlanMaster.ProductId=ProductMaster.ProductId where PricePlanMaster.PricePlanId='" + Session["Pid"] + "'";
                      SqlCommand cmdsele = new SqlCommand(sele, con);
                      con.Open();
                      object productid = cmdsele.ExecuteScalar();
                      con.Close();
                      string sele1 = "select companymaster.email from  CompanyMaster inner join ProductMaster on ProductMaster.ProductId=CompanyMaster.ProductId where CompanyMaster.Email='" + txtEmail1.Text + "' and ProductMaster.ProductId='" + productid + "'";

                      SqlDataAdapter adpp = new SqlDataAdapter(sele1, con);
                      DataSet dsp = new DataSet();
                      adpp.Fill(dsp);
                      if (dsp.Tables[0].Rows.Count > 10)
                      {
                          lblmsg.Visible = true;

                          lblmsg.Text = "Sorry, This Email Is Already Registerd";
                      }
                      else
                      {
                          lblmsg.Text = "";
                          string str1 = "SELECT  * FROM   CompanyMaster WHERE     (CompanyLoginId = '" + txtcompanyid.Text + "')";
                          cmd1 = new SqlCommand(str1, con);
                          SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                          DataSet ds1 = new DataSet();
                          adp1.Fill(ds1);
                          if (ds1.Tables[0].Rows.Count > 0)
                          {
                              
                              //lblCompanyIDAVl.Text = "Sorry This Company ID  is already exist. Please try another.";
                              //lblCompanyIDAVl.ForeColor = System.Drawing.Color.Red;
                              //lblCompanyIDAVl.Focus();
                              //return;
                          }

                      //    int j = checkCompId();
                          int j = 0;
                          if (j == 0)
                          {
                              string str = "";
                              ViewState["email"] = txtEmail1.Text;

                              str = "INSERT INTO OrderMaster " +
                                    " (CompanyName,ContactPerson, ContactPersonDesignation, Address, Email,  phone, fax,PlanId, CompanyLoginId,AdminId, Password, Domain, HostId,Status,companylogo,paypalid) " +
                                    " VALUES     ('" + txtCompanyName.Text + "','" + txtContactPerson.Text + "','','" + txtAdrs1.Text + "','" + txtEmail1.Text + "','" + txtPhone1.Text + "','" + txtFax1.Text + "','" + Convert.ToInt32(lblPricePlanId.Text) + "','" + txtcompanyid.Text + "','" + txtLoginName.Text + "','" + txtLoginPassword.Text + "','" + txtcompanyid.Text + "','" + lblPricePlanId.Text + "','True','" + ViewState["upfile"] + "','" + txtEmail1.Text + "')";

                              lblmsg.Visible = true;

                              SqlCommand cmd = new SqlCommand(str, con);
                              con.Open();
                              cmd.ExecuteNonQuery();
                              con.Close();
                              ViewState["comid"] = txtcompanyid.Text;
                              string s = " select  OrderID  from OrderMaster where  CompanyLoginId= '" + txtcompanyid.Text + "' and AdminId = '" + txtLoginName.Text + "' and Password= '" + txtLoginPassword.Text + "'";//CompanyName='" + txtcompanyname.Text + "'" + " and
                              SqlCommand c = new SqlCommand(s, con);
                              SqlDataAdapter a = new SqlDataAdapter(c);
                              DataTable d = new DataTable();
                              a.Fill(d);
                              int orderid = 0;
                              if (d.Rows.Count > 0)
                              {
                                  orderid = Convert.ToInt32(d.Rows[0]["orderid"]);
                                  ViewState["orderid"] = Convert.ToInt32(d.Rows[0]["orderid"]);
                              }
                              if (orderid.ToString().Length > 0)
                              {
                                  lblmsg.Visible = true;
                                  //lblmsg.Text = "Thank You for register.";
                                  int compid = insertCompanyMaster(orderid.ToString());
                                  insertstatus();
                                  InsertFrancDetail();
                                  //========== select unalloted domain with database and connection string =================

                                  Session["CI"] = txtcompanyid.Text;
                                  Session["UN"] = txtLoginName.Text;
                                  Session["UP"] = txtLoginPassword.Text;
                                  if ((lblamt.Text.Length > 0) && (lblamt.Text == "0.00" || lblamt.Text == "00.00" || lblamt.Text == "0"))
                                  {

                                  }
                                  else
                                  {

                                      insertpay();

                                  }
                             //     btnclear_Click(sender, e);
                              }
                          }
                          else
                          {
                              return;
                          }
                          insertpay();

                      }            
             
        
      }

      public int insertCompanyMaster(string OrderId)
      {
          string str = "";

          str = "INSERT INTO CompanyMaster " +
                       " (CompanyName, ContactPerson, ContactPersonDesignation, CompanyWebsite, date, PlanId, Address, " +
                       " Email, pincode, Phonecc,phone,Mobilecc,MobileNo, fax, CompanyLoginId, AdminId,  " +
                       " Password, redirect, active, deactiveReason,HostId,OrderId,ProductId,PricePlanId,Websiteurl,companylogo,paypalid,returnurl,cancelurl,paymentreturnurl,countryId,StateId,city,carrierid) " +
                     " VALUES     ('" + txtCompanyName.Text + "','" + txtContactPerson.Text + "','',''," +
                     " '" + System.DateTime.Now.Date + "','" + Convert.ToInt32(lblPricePlanId.Text) + "','" + txtAdrs1.Text + "'," +
                     " '" + txtEmail1.Text + "','" + txtZipcode.Text + "','" + txtPhone1.Text + "','" + txtPhone2.Text + "','','','" + txtFax1.Text + "', " +
                     " '" + txtcompanyid.Text + "' ,'" + txtLoginName.Text + "','" + txtLoginPassword.Text + "','" + txtCompanyName.Text + "','0','','0','" + OrderId + "'," + hdnProductId.Value.ToString() + ",'" + Convert.ToInt32(lblPricePlanId.Text) + "','" + txtCompanyName.Text + "','" + ViewState["upfile"] + "','" + ViewState["email"] + "','" + Textreturnurl.Text + "','" + Textcancelurl.Text + "','" + Textnotifyurl.Text + "','" + ddlCountry.SelectedValue + "','" + ddlState.SelectedValue + "','" + txtCity.Text + "','') ";

          SqlCommand cmd = new SqlCommand(str, con);
          con.Open();
          cmd.ExecuteNonQuery();
          con.Close();
          string str1 = "select MAX(CompanyId ) as CompanyId from CompanyMaster ";
          cmd1 = new SqlCommand(str1, con);
          SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
          dt = new DataTable();
          adp1.Fill(dt);
          int id = Convert.ToInt32(dt.Rows[0]["CompanyId"]);
          string str2 = "INSERT INTO CompanyUserMaster " +
                   " (CompanyId, Username, Password) " +
                     " VALUES     ('" + id + "' ,'" + txtLoginName.Text + "','" + txtLoginPassword.Text + "')";
          SqlCommand cmd2 = new SqlCommand(str2, con);
          con.Open();
          cmd2.ExecuteNonQuery();
          con.Close();



          return id;
      }


      protected void GetClientId()
      {
          string str = "select PricePlanMaster.*,PortalMasterTbl.PortalName,Priceplancategory.CategoryName , case when  PricePlanMaster.AllowIPTrack IS NULL     then 'No' else 'Yes' end as GBUSage1 , ProductMaster.* " +
                          " FROM         PricePlanMaster LEFT OUTER JOIN " +
                          " ProductMaster ON PricePlanMaster.ProductId = ProductMaster.ProductId Left outer join Priceplancategory on Priceplancategory.ID=PricePlanMaster.PriceplancatId left outer join PortalMasterTbl on PortalMasterTbl.Id=Priceplancategory.PortalId" +
            " where   PricePlanId='" + Session["Pid"]  + "'";
          SqlCommand cmd = new SqlCommand(str, con);
          SqlDataAdapter adp = new SqlDataAdapter(cmd);
          dt = new DataTable();
          adp.Fill(dt);
          if (dt.Rows.Count > 0)
          {
              //if (Convert.ToBoolean(dt.Rows[0]["active"].ToString()) == true)
              //{
              //    btncontinue.Enabled = true;
              //}
              //else
              //{
              //    btncontinue.Enabled = false;
              //}

              Session["ClientId"] = dt.Rows[0]["ClientMasterId"].ToString();
              ViewState["ProductId"] = dt.Rows[0]["ProductId"].ToString();
            //  ibtnPlanINfo.HRef = "http://" + dt.Rows[0]["PricePlanURL"].ToString();
              lblPricePlanId.Text = dt.Rows[0]["PricePlanId"].ToString();
              Label4.Text = dt.Rows[0]["PortalName"].ToString();
           //   LinkButton1.Text = dt.Rows[0]["CategoryName"].ToString() + " - " + dt.Rows[0]["PricePlanName"].ToString();
              lblamt.Text = dt.Rows[0]["PricePlanAmount"].ToString();
             // pnlpayo.Visible = false;
              if (dt.Rows[0]["PayperOrderPlan"].ToString() == "True" && dt.Rows[0]["amountperOrder"].ToString() != "")
              {

               //   pnlpayo.Visible = true;
                  // lblpayporder.Text = dt.Rows[0]["PayperOrderPlan"].ToString();
                  lblfreeiorder.Text = dt.Rows[0]["FreeIntialOrders"].ToString();
                  lblminidep.Text = dt.Rows[0]["MinimumDeposite"].ToString();
                  // lblminamount.Text = dt.Rows[0]["Maxamount"].ToString();
                  lblamt.Text = dt.Rows[0]["MinimumDeposite"].ToString();
              }
              else
              {
               //   pnlpayo.Visible = false;
              }
          }
      }

      protected void insertstatus()
      {
          string strmaincmp = " SELECT * from Companycreationstatustbl where StatusMasterId='1' and Status='1' and CompanyID='" + txtcompanyid.Text + "'   ";
          SqlCommand cmdmaincmp = new SqlCommand(strmaincmp, con);
          SqlDataAdapter adpmaincmp = new SqlDataAdapter(cmdmaincmp);
          DataTable dsmaincmp = new DataTable();
          adpmaincmp.Fill(dsmaincmp);

          if (dsmaincmp.Rows.Count == 0)
          {

              string str = " insert into Companycreationstatustbl(StatusMasterId,Status,DateTime,CompanyID) values ('1','1','" + DateTime.Now.ToString() + "','" + txtcompanyid.Text + "') ";
              SqlCommand cmd = new SqlCommand(str, con);
              if (con.State.ToString() != "Open")
              {
                  con.Open();
              }
              cmd.ExecuteNonQuery();
              con.Close();
          }
      }

      protected void InsertFrancDetail()
      {
          try
          {
              SqlCommand cmdsq = new SqlCommand("Insert_CompanyRegistrationbyFranchiseeTBL", con);
              cmdsq.CommandType = CommandType.StoredProcedure;
              cmdsq.Parameters.AddWithValue("@CompanyMasterLoginID", txtcompanyid.Text);
              cmdsq.Parameters.AddWithValue("@RegisteringFranchiseeLogID", FranCompanyname);
              cmdsq.Parameters.AddWithValue("@CompanysPrimaryFranchiseeLogID", FranCompanyname);
              cmdsq.Parameters.AddWithValue("@DateandTime", DateTime.Now);
              cmdsq.Parameters.AddWithValue("@Active", true);


              if (con.State.ToString() != "Open")
              {
                  con.Open();
              }
              cmdsq.ExecuteNonQuery();
              con.Close();
          }
          catch (Exception ex)
          {
          }
          
      }

      protected void insertpay()
      {
          //if (txtdepo.Text.Length > 0)
          //{
          //    if (Convert.ToDecimal(txtdepo.Text) > Convert.ToDecimal(lblamt.Text))
          //    {
          //        lblamt.Text = txtdepo.Text;
          //    }
          //}
          lblamt.Text ="00.00";
          con.Close();
          string strpaypalAtmp1 = "insert into paymentinfo(PaypalEmailId,ordermasterid,websitename,amount,returnurl,calcelurl,notifyurl,contarycode,compid)values('" + Textpaypal.Text + "', '" + ViewState["orderid"].ToString() + "'," +
        " '" + txthostedsite.Text + "','" + String.Format("{0:0.00} ", lblamt.Text) + "'," +
        " '" + Textreturn.Text + "','" + Textcancel.Text + "','" + Textnotify.Text.Trim() + "','" + ViewState["contry_code"] + "','" + ViewState["comid"] + "')";
          SqlCommand cmdpapalatmp1 = new SqlCommand(strpaypalAtmp1, con);
          con.Open();
          cmdpapalatmp1.ExecuteNonQuery();
          con.Close();
          createzeroAmount();
          string str5 = "SELECT paymentinfoid FROM  paymentinfo where ordermasterid='" + ViewState["orderid"].ToString() + "' and compid= '" + ViewState["comid"] + "'";
          SqlCommand cmd5 = new SqlCommand(str5, con);
          con.Open();
          int i = Convert.ToInt32(cmd5.ExecuteScalar());
          con.Close();
          Response.Redirect("https://paymentgateway.safestserver.com/paymentnow.aspx?payid=" + i + "");
      }

      protected void createzeroAmount()
      {

          if (Convert.ToDecimal(lblamt.Text) <= Convert.ToDecimal(0))
          {
              string zero1 = "select PricePlanAmount from PricePlanMaster where PricePlanId='" + Session["Pid"] + "' and PayperOrderPlan='False' and(PricePlanAmount is Null or PricePlanAmount='0' or PricePlanAmount='')";
              SqlCommand cmdzero1 = new SqlCommand(zero1, con);
              SqlDataAdapter adpzero1 = new SqlDataAdapter(cmdzero1);
              DataTable dszero1 = new DataTable();
              adpzero1.Fill(dszero1);
              if (dszero1.Rows.Count > 0)
              {
                  Response.Redirect("busiwizlicensekeygeneration.aspx?orz=" + ViewState["orderid"].ToString() + "&cid=" + ViewState["comid"] + "&Franchisee=" + FranCompanyname + "");
              }
              string zero = "select PayperOrderPlan,amountperOrder,FreeIntialOrders,MinimumDeposite,Maxamount from PricePlanMaster where PricePlanId='" + Session["Pid"] + "' and PayperOrderPlan='True' and(MinimumDeposite is Null or MinimumDeposite='0' or MinimumDeposite='')";
              SqlCommand cmdzero = new SqlCommand(zero, con);
              SqlDataAdapter adpzero = new SqlDataAdapter(cmdzero);
              DataTable dszero = new DataTable();
              adpzero.Fill(dszero);

              if (dszero.Rows.Count > 0)
              {
                  Response.Redirect("busiwizlicensekeygeneration.aspx?orz=" + ViewState["orderid"].ToString() + "&cid=" + ViewState["comid"] + "&Franchisee=''");
              }
              else
              {
                  Response.Redirect("busiwizlicensekeygeneration.aspx?orz=" + ViewState["orderid"].ToString() + "&cid=" + ViewState["comid"] + "&Franchisee=''");
              }

              //else if (Convert.ToString(dszero.Rows[0]["MinimumDeposite"].ToString()) == "" || Convert.ToString(dszero.Rows[0]["MinimumDeposite"].ToString()) == "0")
              //{
              //    
              //}
          }

      }
}
