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

public partial class New_Account_SignUp_Form : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    DataTable dt;
    SqlCommand cmd1;
    string FranCompanyname;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblchapmsg.Text = "";
        lblVersion.Text = "";
        lblCompanyIDAVl.Text = "";
        
        string pass = txtpassword.Text;
      
        txtpassword.Attributes.Add("Value", pass);
        txtcnfrmpassword.Attributes.Add("Value", pass);


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

            if ((Request.QueryString["orderid"]) != null)
            {
                ViewState["orderid"] = Request.QueryString["orderid"].ToString();
                fillddldataPopup();
            }
            string sele = "select ProductMaster.ClientMasterId from ProductMaster inner join PricePlanMaster on PricePlanMaster.ProductId=ProductMaster.ProductId where PricePlanMaster.PricePlanId='" + Request.QueryString["pid"] + "'";
            SqlCommand cmdsele = new SqlCommand(sele, con);
            con.Open();
            object clientid = cmdsele.ExecuteScalar();
            con.Close();
            string str22 = "SELECT  BusiwizMasterInfoTbl.*,ClientMaster.ClientURL from BusiwizMasterInfoTbl inner join ClientMaster on BusiwizMasterInfoTbl.ClientMasterId=ClientMaster.ClientMasterId  where BusiwizMasterInfoTbl.ClientMasterId='" + clientid.ToString() + "' ";
            SqlCommand cmd2 = new SqlCommand(str22, con);
            SqlDataAdapter adp1 = new SqlDataAdapter(cmd2);
            DataSet dt1 = new DataSet();
            adp1.Fill(dt1);
            if (dt1.Tables[0].Rows.Count > 0)
            {
                Textpaypal.Text = dt1.Tables[0].Rows[0]["PaypalID"].ToString();
                Textcancel.Text = dt1.Tables[0].Rows[0]["PaypalCancelURL"].ToString();
                Textreturn.Text = dt1.Tables[0].Rows[0]["PaypalReturnURL"].ToString();
                Textnotify.Text = dt1.Tables[0].Rows[0]["PaypalNotifyURL"].ToString();
                txtpaymentnotifyurl.Text = dt1.Tables[0].Rows[0]["PaymentNotifyURL"].ToString();
            }
            if (Request.QueryString["PId"] != null)
            {
                GetClientId();
                hdnProductId.Value = ViewState["ProductId"].ToString(); // Request.QueryString["PId"].ToString();
                fillterms();
                //2-10   pnlProduct.Visible = false;
            }
            else
            {                
            }
            if (Request.QueryString["spid"] != null)
            {
                string str = "select PricePlanMaster.*,PortalMasterTbl.PortalName,Priceplancategory.CategoryName , case when  PricePlanMaster.AllowIPTrack IS NULL     then 'No' else 'Yes' end as GBUSage1 , ProductMaster.* " +
                        " FROM         PricePlanMaster LEFT OUTER JOIN ProductMaster ON PricePlanMaster.ProductId = ProductMaster.ProductId Left outer join Priceplancategory on Priceplancategory.ID=PricePlanMaster.PriceplancatId left outer join PortalMasterTbl on PortalMasterTbl.Id=Priceplancategory.PortalId" +
                        " where   PricePlanId='" + Request.QueryString["spid"].ToString() + "'";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {                 
                    lblserverplndetailid.Text = dt.Rows[0]["PricePlanId"].ToString();                    
                    linkHostingplan.Text =dt.Rows[0]["PortalName"].ToString() +" - "+ dt.Rows[0]["CategoryName"].ToString() + " - " + dt.Rows[0]["PricePlanName"].ToString();
                    lblHostAmt.Text = dt.Rows[0]["PricePlanAmount"].ToString();

                    Decimal totalamt1 = Convert.ToDecimal(lblplanamt.Text);
                    Decimal totalamt2 = Convert.ToDecimal(lblHostAmt.Text);
                    Decimal totalamt = totalamt1 + totalamt2;
                   lblamt.Text =Convert.ToString(totalamt); 
                }
            }
            Fillpaymentmode();
            Fillddlcountry();
            carrirfill();
            Fillddlstatelist();

            DataTable ds = new DataTable();
            ds = FillVersion();

            Session["ProductId"] = ds.Rows[0]["ProductName"].ToString();

            Session["ClientId"] = ds.Rows[0]["ClientMasterId"].ToString();
            lblproductId.Text = Session["ProductId"].ToString();
            Session["ProductId"] = lblproductId.Text;
            string productid = lblproductId.Text;
            ddlVersion.Text = ds.Rows[0]["VersionNo"].ToString();

            ViewState["upfile"] = "DefaultLogo.png";

        }
        //  lblamt.Text = "0";
        lblmsg.Visible = false;
    }
    protected void GetClientId()
    {
        string str = "select PricePlanMaster.*,PortalMasterTbl.PortalName,Priceplancategory.CategoryName , case when  PricePlanMaster.AllowIPTrack IS NULL     then 'No' else 'Yes' end as GBUSage1 , ProductMaster.* " +
                        " FROM         PricePlanMaster LEFT OUTER JOIN " +
                        " ProductMaster ON PricePlanMaster.ProductId = ProductMaster.ProductId Left outer join Priceplancategory on Priceplancategory.ID=PricePlanMaster.PriceplancatId left outer join PortalMasterTbl on PortalMasterTbl.Id=Priceplancategory.PortalId" +
          " where   PricePlanId='" + Request.QueryString["PId"].ToString() + "'";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        dt = new DataTable();
        adp.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            if (Convert.ToBoolean(dt.Rows[0]["active"].ToString()) == true)
            {
                btncontinue.Enabled = true;
            }
            else
            {
                btncontinue.Enabled = false;
            }

            Session["ClientId"] = dt.Rows[0]["ClientMasterId"].ToString();
            ViewState["ProductId"] = dt.Rows[0]["ProductId"].ToString();
            ibtnPlanINfo.HRef = "http://" + dt.Rows[0]["PricePlanURL"].ToString();
            lblPricePlanId.Text = dt.Rows[0]["PricePlanId"].ToString();
            Label4.Text = dt.Rows[0]["PortalName"].ToString();
           
            LinkButton1.Text = dt.Rows[0]["CategoryName"].ToString() + " - " + dt.Rows[0]["PricePlanName"].ToString();


            lblplanamt.Text = dt.Rows[0]["PricePlanAmount"].ToString();
            pnlpayo.Visible = false;
            if (dt.Rows[0]["PayperOrderPlan"].ToString() == "True" && dt.Rows[0]["amountperOrder"].ToString() != "")
            {

                pnlpayo.Visible = true;
                // lblpayporder.Text = dt.Rows[0]["PayperOrderPlan"].ToString();
                lblfreeiorder.Text = dt.Rows[0]["FreeIntialOrders"].ToString();
                lblminidep.Text = dt.Rows[0]["MinimumDeposite"].ToString();
                // lblminamount.Text = dt.Rows[0]["Maxamount"].ToString();
                lblplanamt.Text = dt.Rows[0]["MinimumDeposite"].ToString();
            }
            else
            {
                pnlpayo.Visible = false;
            }
        }
    }
    public DataTable FillVersion()
    {

        string str = "  SELECT     ProductMaster.*, ProductDetail.* fROM  ProductDetail RIGHT OUTER JOIN " +
                     " ProductMaster ON ProductDetail.ProductId = ProductMaster.ProductId where ProductMaster.Productid= " + hdnProductId.Value.ToString();
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        return ds;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string sele = "select ProductMaster.ProductId from ProductMaster inner join PricePlanMaster on PricePlanMaster.ProductId=ProductMaster.ProductId where PricePlanMaster.PricePlanId='" + Request.QueryString["pid"] + "'";
        SqlCommand cmdsele = new SqlCommand(sele, con);
        con.Open();
        object productid = cmdsele.ExecuteScalar();
        con.Close();
        string sele1 = "select companymaster.email from  CompanyMaster inner join ProductMaster on ProductMaster.ProductId=CompanyMaster.ProductId where CompanyMaster.Email='" + txtemail.Text + "' and ProductMaster.ProductId='" + productid + "'";

        SqlDataAdapter adpp = new SqlDataAdapter(sele1, con);
        DataSet dsp = new DataSet();
        adpp.Fill(dsp);
        if (dsp.Tables[0].Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.ForeColor = System.Drawing.Color.Red;

            lblmsg.Text = "Email Already Registered";
        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.ForeColor = System.Drawing.Color.Green;
            lblmsg.Text = "Email Available";

        }
    }

    protected void txtemail_TextChanged(object sender, EventArgs e)
    {
        string sele = "select ProductMaster.ProductId from ProductMaster inner join PricePlanMaster on PricePlanMaster.ProductId=ProductMaster.ProductId where PricePlanMaster.PricePlanId='" + Request.QueryString["pid"] + "'";
        SqlCommand cmdsele = new SqlCommand(sele, con);
        con.Open();
        object productid = cmdsele.ExecuteScalar();
        con.Close();
        string sele1 = "select companymaster.email from  CompanyMaster inner join ProductMaster on ProductMaster.ProductId=CompanyMaster.ProductId where CompanyMaster.Email='" + txtemail.Text + "' and ProductMaster.ProductId='" + productid + "'";

        SqlDataAdapter adpp = new SqlDataAdapter(sele1, con);
        DataSet dsp = new DataSet();
        adpp.Fill(dsp);
        if (dsp.Tables[0].Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.ForeColor = System.Drawing.Color.Red;

            lblmsg.Text = "Already Registered";
        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.ForeColor = System.Drawing.Color.Green;
            lblmsg.Text = "Available";

        }
    }
    protected void txtdepo_TextChanged(object sender, EventArgs e)
    {
        if (txtdepo.Text.Length > 0)
        {
            if (Convert.ToDecimal(txtdepo.Text) > Convert.ToDecimal(lblminidep.Text))
            {
                lblamt.Text = "";
                lblamt.Text = txtdepo.Text;
            }
        }
    }
    protected void txtcompanyid_TextChanged(object sender, EventArgs e)
    {
        // txtdomain.Text = txtcompanyid.Text;


        if (txtdepo.Text.Length > 0)
        {
            if (Convert.ToDecimal(txtdepo.Text) > Convert.ToDecimal(lblamt.Text))
            {
                lblamt.Text = "";
                lblamt.Text = txtdepo.Text;
            }
        }
        if (txtcompanyid.Text.Length <= 0)
        {
            lblCompanyIDAVl.Text = "Please enter company id.";
            return;
        }
        int i = 0;

        string str = "SELECT  * FROM   CompanyMaster WHERE     (CompanyLoginId = '" + txtcompanyid.Text + "')";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            // lblmsg.Visible = true;
            lblCompanyIDAVl.Text = "Sorry, this company ID is already in existance. Please try another.";
            lblCompanyIDAVl.ForeColor = System.Drawing.Color.Red;
            lblCompanyIDAVl.Focus();

        }
        else
        {
            lblCompanyIDAVl.Text = "Available for you.";
            lblCompanyIDAVl.ForeColor = System.Drawing.Color.Green;
        }

    }
    protected void btnCheckCompany_Click(object sender, EventArgs e)
    {
        //  txtdomain.Text = txtcompanyid.Text;


        if (txtdepo.Text.Length > 0)
        {
            if (Convert.ToInt32(txtdepo.Text) > Convert.ToInt32(lblamt.Text))
            {
                lblamt.Text = "";
                lblamt.Text = txtdepo.Text;
            }
        }
        if (txtcompanyid.Text.Length <= 0)
        {
            lblCompanyIDAVl.Text = "Please enter Company Id.";
            return;
        }
        int i = 0;

        string str = "SELECT  * FROM   CompanyMaster WHERE     (CompanyLoginId = '" + txtcompanyid.Text + "')";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            // lblmsg.Visible = true;
            lblCompanyIDAVl.Text = "Sorry,This Company Id is already exist.Please try another.";
            lblCompanyIDAVl.ForeColor = System.Drawing.Color.Red;
            lblCompanyIDAVl.Focus();

        }
        else
        {
            lblCompanyIDAVl.Text = "Available for you.";
            lblCompanyIDAVl.ForeColor = System.Drawing.Color.Green;
        }
    }

    protected void btncontinue_Click(object sender, EventArgs e)
    {
        if (Session["Captcha"] != null)
        {

            if (Chk.Checked)
            {
                if (txtCaptcha.Text == Session["Captcha"].ToString())
                {
                    string sele = "select ProductMaster.ProductId from ProductMaster inner join PricePlanMaster on PricePlanMaster.ProductId=ProductMaster.ProductId where PricePlanMaster.PricePlanId='" + Request.QueryString["pid"] + "'";
                    SqlCommand cmdsele = new SqlCommand(sele, con);
                    con.Open();
                    object productid = cmdsele.ExecuteScalar();
                    con.Close();
                    string sele1 = "select companymaster.email from  CompanyMaster inner join ProductMaster on ProductMaster.ProductId=CompanyMaster.ProductId where CompanyMaster.Email='" + txtemail.Text + "' and ProductMaster.ProductId='" + productid + "'";

                    SqlDataAdapter adpp = new SqlDataAdapter(sele1, con);
                    DataSet dsp = new DataSet();
                    adpp.Fill(dsp);
                    if (dsp.Tables[0].Rows.Count > 0)
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
                            // lblmsg.Visible = true;
                            lblCompanyIDAVl.Text = "Sorry This Company ID  is already exist. Please try another.";
                            lblCompanyIDAVl.ForeColor = System.Drawing.Color.Red;
                            lblCompanyIDAVl.Focus();
                            return;
                        }
                        
                        int j = checkCompId();
                        if (j == 0)
                        {
                            string str = "";
                            ViewState["email"] = txtemail.Text;
                            
                            //str = " INSERT INTO OrderMaster " +
                            //      " (CompanyName,ContactPerson, ContactPersonDesignation, Address, Email,  phone, fax,PlanId, CompanyLoginId,AdminId, Password, Domain, HostId,Status,companylogo,paypalid) " +
                            //      " VALUES     ('" + txtcompanyname.Text + "','" + txtcontactprsn.Text + "','" + txtcontactprsndsg.Text + "','" + txtadd.Text + "','" + txtemail.Text + "','" + txtphn.Text + "','" + txtfax.Text + "','" + Convert.ToInt32(lblPricePlanId.Text) + "','" + txtcompanyid.Text + "','" + txtadminuserid.Text + "','" + txtpassword.Text + "','" + txtcompanyid.Text + "','" + lblPricePlanId.Text + "','True','" + ViewState["upfile"] + "','" + txtemail.Text + "')";
                            
                            //lblmsg.Visible = true;
                            
                            //SqlCommand cmd = new SqlCommand(str, con);
                            //con.Open();
                            //cmd.ExecuteNonQuery();
                            //con.Close();
                            try
                            {
                                SqlCommand cmdsq = new SqlCommand("OrderMaster_AddDelUpdtSelect", con);
                                cmdsq.CommandType = CommandType.StoredProcedure;
                                cmdsq.Parameters.AddWithValue("@StatementType", "Update");
                                cmdsq.Parameters.AddWithValue("@OrderId", ViewState["orderid"]);                                
                                cmdsq.Parameters.AddWithValue("@CompanyName", txtcompanyname.Text);
                                cmdsq.Parameters.AddWithValue("@ContactPerson", txtcontactprsn.Text);
                                cmdsq.Parameters.AddWithValue("@ContactPersonDesignation", txtcontactprsndsg.Text);
                                cmdsq.Parameters.AddWithValue("@Address", txtadd.Text);
                                cmdsq.Parameters.AddWithValue("@Email", txtemail.Text);
                                cmdsq.Parameters.AddWithValue("@phone", txtphn.Text);
                                cmdsq.Parameters.AddWithValue("@fax", txtfax.Text);
                                cmdsq.Parameters.AddWithValue("@PlanId", Convert.ToInt32(lblPricePlanId.Text));
                                cmdsq.Parameters.AddWithValue("@CompanyLoginId", txtcompanyid.Text );
                                cmdsq.Parameters.AddWithValue("@AdminId", txtadminuserid.Text);
                                cmdsq.Parameters.AddWithValue("@Password", txtpassword.Text);
                                cmdsq.Parameters.AddWithValue("@Domain", txtcompanyid.Text);
                                cmdsq.Parameters.AddWithValue("@HostId",lblPricePlanId.Text);
                                cmdsq.Parameters.AddWithValue("@Status", true);
                                cmdsq.Parameters.AddWithValue("@companylogo",ViewState["upfile"] );
                                cmdsq.Parameters.AddWithValue("@paypalid",txtemail.Text );
                                if (con.State.ToString() != "Open")
                                {
                                    con.Open();
                                }
                                cmdsq.ExecuteNonQuery();
                                con.Close();
                            }
                            catch (Exception ex)
                            {
                                con.Close();
                            }

                            /*
                             
                             
                             */


                            ViewState["comid"] = txtcompanyid.Text;
                            int orderid = 0;
                            orderid =Convert.ToInt32(ViewState["orderid"]);

                            Insert_CompanyMasterPlanDetal(ViewState["orderid"].ToString(), ViewState["comid"].ToString());
                            //string s = " select  OrderID  from OrderMaster where CompanyName='" + txtcompanyname.Text + "'" + " and CompanyLoginId= '" + txtcompanyid.Text + "' and AdminId = '" + txtadminuserid.Text + "' and Password= '" + txtpassword.Text + "'";
                            //SqlCommand c = new SqlCommand(s, con);
                            //SqlDataAdapter a = new SqlDataAdapter(c);
                            //DataTable d = new DataTable();
                            //a.Fill(d);
                            //
                            //if (d.Rows.Count > 0)
                            //{
                            //    orderid = Convert.ToInt32(d.Rows[0]["orderid"]);
                            //    ViewState["orderid"] = Convert.ToInt32(d.Rows[0]["orderid"]);
                            //}
                            if (orderid.ToString().Length > 0)
                            {
                                lblmsg.Visible = true;
                                //lblmsg.Text = "Thank You for register.";
                             

                                int compid = insertCompanyMaster(orderid.ToString());
                                insertstatus();
                                InsertFrancDetail();
                                //========== select unalloted domain with database and connection string =================
                                
                                Session["CI"] = txtcompanyid.Text;
                                Session["UN"] = txtadminuserid.Text;
                                Session["UP"] = txtpassword.Text;
                                if ((lblamt.Text.Length > 0) && (lblamt.Text == "0.00" || lblamt.Text == "00.00" || lblamt.Text == "0"))
                                {

                                }
                                else
                                {                                    
                                    insertpay();                                    
                                }
                                btnclear_Click(sender, e);
                            }
                        }
                        else
                        {
                            return;
                        }
                        insertpay();

                    }
                }
                else
                {
                    lblchapmsg.Visible = true;
                    lblchapmsg.Text = "Please Enter Valid Captcha code";
                    txtCaptcha.Text = "";
                    txtCaptcha.Focus();
                }
            }
            else
            {
                Response.Write("<script language='javascript'>alert('Sorry , You can't register without accepting the terms & conditions.');</script>");
            }
        }
        else
        {
            lblchapmsg.Visible = true;
            lblmsg.Text = "Session Expired, please re-enter Captcha.";
        }
    }

    protected void Insert_CompanyMasterPlanDetal(string maxorderid, string compid)
    {
        //------------------
        try
        {
            DataTable dtsedata = select("Select * From OrderMasterDetail Where ordermasterid='" + maxorderid + "'");
              if (dtsedata.Rows.Count > 0)
              {
                  foreach (DataRow item in dtsedata.Rows)
                  {
                      string Priceplanid = Convert.ToString(item["Priceplanid"]);
                      string Amt = Convert.ToString(item["Amt"]);
                      Boolean hostserv = Convert.ToBoolean(item["IsHostingServer"]);
                      Boolean service = Convert.ToBoolean(item["IsServices"]);
                      Boolean webapppli = Convert.ToBoolean(item["IsWebBasedApplications"]);
                      try
                      {
                          SqlCommand cmdsq = new SqlCommand("CompanyMasterPlanDetal_AddDelUpdtSelect", con);
                          cmdsq.CommandType = CommandType.StoredProcedure;
                          cmdsq.Parameters.AddWithValue("@StatementType", "Insert");                          
                          cmdsq.Parameters.AddWithValue("@Priceplanid", Priceplanid);
                          cmdsq.Parameters.AddWithValue("@CompanyLoginID", compid);                          
                          cmdsq.Parameters.AddWithValue("@Amt", Amt);
                          cmdsq.Parameters.AddWithValue("@Date", DateTime.Now);
                          cmdsq.Parameters.AddWithValue("@IsWebplan", webapppli);
                          cmdsq.Parameters.AddWithValue("@IsServerplan", hostserv);
                          cmdsq.Parameters.AddWithValue("@IsServiceplan", service);

                          if (con.State.ToString() != "Open")
                          {
                              con.Open();
                          }
                          cmdsq.ExecuteNonQuery();
                          con.Close();
                      }
                      catch (Exception ex)
                      {
                          con.Close();
                      }
                     

                  }
              }
        }
        catch (Exception ex)
        {
            con.Close();
        }

    }
    public int checkCompId()
    {
        int i = 0;
        string str = "";

        str = "SELECT  * FROM   CompanyMaster WHERE     (CompanyLoginId = '" + txtcompanyid.Text + "')";


        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Sorry This Company Id  is already exist. Please try another.";
            i = 1;
        }
        return i;
    }

    protected void btnclear_Click(object sender, EventArgs e)
    {
        txtadd.Text = "";
        txtadminuserid.Text = "";
        txtcnfrmpassword.Text = "";
        txtcompanyid.Text = "";
        txtcompanyname.Text = "";
        txtcontactprsn.Text = "";
        txtcontactprsndsg.Text = "";
        //txtdomain.Text = "";
        txtemail.Text = "";
        txtfax.Text = "";
        txtpassword.Text = "";
        txtphn.Text = "";

    }
    public int insertCompanyMaster(string OrderId)
    {
        string str = "";
        
        str = " INSERT INTO CompanyMaster " +
                     " (CompanyName, ContactPerson, ContactPersonDesignation, CompanyWebsite, date, PlanId, Address, " +
                     " Email, pincode, Phonecc,phone,Mobilecc,MobileNo, fax, CompanyLoginId, AdminId,  " +
                     " Password, redirect, active, deactiveReason,HostId,OrderId,ProductId,PricePlanId,Websiteurl,companylogo,paypalid,returnurl,cancelurl,paymentreturnurl,countryId,StateId,city,carrierid) " +
                   " VALUES     ('" + txtcompanyname.Text + "','" + txtcontactprsn.Text + "','" + txtcontactprsndsg.Text + "',''," +
                   " '" + System.DateTime.Now.Date + "','" + Convert.ToInt32(lblPricePlanId.Text) + "','" + txtadd.Text + "'," +
                   " '" + txtemail.Text + "','" + txtzipcode.Text + "','" + txtcccode.Text + "','" + txtphn.Text + "','" + txtcccode.Text + "','" + txtmobileno.Text + "','" + txtfax.Text + "', " +
                   " '" + txtcompanyid.Text + "' ,'" + txtadminuserid.Text + "','" + txtpassword.Text + "','" + txtcompanyname.Text + "','0','','0','" + OrderId + "'," + hdnProductId.Value.ToString() + ",'" + Convert.ToInt32(lblPricePlanId.Text) + "','" + txtcompanyname.Text + "','" + ViewState["upfile"] + "','" + ViewState["email"] + "','" + Textreturn.Text + "','" + Textcancel.Text + "','" + txtpaymentnotifyurl.Text + "','" + ddlcountry.SelectedValue + "','" + ddlstate.SelectedValue + "','" + txtcity.Text + "','" + ddlcarriername.SelectedValue + "') ";
       
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
                   " VALUES     ('" + id + "' ,'" + txtadminuserid.Text + "','" + txtpassword.Text + "')";
        SqlCommand cmd2 = new SqlCommand(str2, con);
        con.Open();
        cmd2.ExecuteNonQuery();
        con.Close();
        return id;
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
            con.Close();
        }        
    }


    protected void insertpay()
    {
        if (txtdepo.Text.Length > 0)
        {
            if (Convert.ToDecimal(txtdepo.Text) > Convert.ToDecimal(lblamt.Text))
            {
                lblamt.Text = txtdepo.Text;
            }
        }
        string strpaypalAtmp1 = " insert into paymentinfo(PaypalEmailId,ordermasterid,websitename,amount,returnurl,calcelurl,notifyurl,contarycode,compid)values('" + Textpaypal.Text + "', '" + ViewState["orderid"].ToString() + "'," +
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
            string zero1 = "select PricePlanAmount from PricePlanMaster where PricePlanId='" + Request.QueryString["pid"] + "' and PayperOrderPlan='False' and(PricePlanAmount is Null or PricePlanAmount='0' or PricePlanAmount='')";
            SqlCommand cmdzero1 = new SqlCommand(zero1, con);
            SqlDataAdapter adpzero1 = new SqlDataAdapter(cmdzero1);
            DataTable dszero1 = new DataTable();
            adpzero1.Fill(dszero1);
            if (dszero1.Rows.Count > 0)
            {
                Response.Redirect("busiwizlicensekeygeneration.aspx?orz=" + ViewState["orderid"].ToString() + "&cid=" + ViewState["comid"] + "&Franchisee="+ FranCompanyname +"");
            }
            string zero = "select PayperOrderPlan,amountperOrder,FreeIntialOrders,MinimumDeposite,Maxamount from PricePlanMaster where PricePlanId='" + Request.QueryString["pid"] + "' and PayperOrderPlan='True' and(MinimumDeposite is Null or MinimumDeposite='0' or MinimumDeposite='')";
            SqlCommand cmdzero = new SqlCommand(zero, con);
            SqlDataAdapter adpzero = new SqlDataAdapter(cmdzero);
            DataTable dszero = new DataTable();
            adpzero.Fill(dszero);

            if (dszero.Rows.Count > 0)
            {
                Response.Redirect("busiwizlicensekeygeneration.aspx?orz=" + ViewState["orderid"].ToString() + "&cid=" + ViewState["comid"] + "&Franchisee=" + FranCompanyname + "");
            }
            else
            {
                Response.Redirect("busiwizlicensekeygeneration.aspx?orz=" + ViewState["orderid"].ToString() + "&cid=" + ViewState["comid"] + "&Franchisee=" + FranCompanyname + "");
            }
            //else if (Convert.ToString(dszero.Rows[0]["MinimumDeposite"].ToString()) == "" || Convert.ToString(dszero.Rows[0]["MinimumDeposite"].ToString()) == "0")
            //{
            //    
            //}
        }
    }
    protected void ddlcountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        carrirfill();
        Fillddlstatelist();
    }
    protected void Fillddlcountry()
    {
        dt = new DataTable();
        dt = SelectCountryMaster();
        ddlcountry.DataSource = dt;
        ddlcountry.DataBind();
        ddlcountry.Items.Insert(0, "-Select-");
        ddlcountry.SelectedIndex = 0;
        ddlcountry.SelectedItem.Value = "0";
    }
    protected DataTable SelectCountryMaster()
    {
        SqlCommand cmd = new SqlCommand();
        dt = new DataTable();
        cmd.Connection = con;
        cmd.CommandText = "SelectCountryMaster";
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        adp.Fill(dt);
        return dt;
    }
    protected void Fillddlstatelist()
    {
        
        SqlCommand cmd = new SqlCommand();
        if (ddlcountry.SelectedIndex > 0)
        {
            cmd.CommandText = "SELECT StateMasterTbl.StateId, StateMasterTbl.StateName, StateMasterTbl.State_Code, CountryMaster.CountryId AS CountryId, CountryMaster.CountryName, CountryMaster.Country_Code FROM StateMasterTbl LEFT OUTER JOIN  CountryMaster ON StateMasterTbl.CountryId = CountryMaster.CountryId WHERE (CountryMaster.CountryId =" + ddlcountry.SelectedValue + ") order by StateMasterTbl.StateName ";

        }
        else
        {
            cmd.CommandText = "SELECT StateMasterTbl.StateId, StateMasterTbl.StateName, StateMasterTbl.State_Code, CountryMaster.CountryId AS CountryId, CountryMaster.CountryName, CountryMaster.Country_Code FROM StateMasterTbl LEFT OUTER JOIN  CountryMaster ON StateMasterTbl.CountryId = CountryMaster.CountryId order by StateMasterTbl.StateName";

        }
        cmd.CommandType = CommandType.Text;
        DataTable dt = new DataTable();
        cmd.Connection = con;
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        adp.Fill(dt);
        ddlstate.DataSource = dt;
        ddlstate.DataValueField = "StateId";
        ddlstate.DataTextField = "StateName";
        ddlstate.DataBind();
        ddlstate.Items.Insert(0, "-Select-");

        ddlstate.Items[0].Value = "0";
    }
    protected DataTable SelectStateMasterWithCountry(Int32 CountryId)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "SELECT StateMasterTbl.StateId, StateMasterTbl.StateName, StateMasterTbl.State_Code, CountryMaster.CountryId AS CountryId, CountryMaster.CountryName, CountryMaster.Country_Code FROM         StateMasterTbl LEFT OUTER JOIN  CountryMaster ON StateMasterTbl.CountryId = CountryMaster.CountryId WHERE (CountryMaster.CountryId =" + CountryId + ")";
        cmd.CommandType = CommandType.Text;
        DataTable dt = new DataTable();
        cmd.Connection = con;
        //  cmd.Parameters.Add(new SqlParameter("@CountryId", SqlDbType.Int));
        //  cmd.Parameters["@CountryId"].Value = CountryId;
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        adp.Fill(dt);
        //   dt = DatabaseCls.FillAdapter(cmd);
        return dt;
    }
    protected void fillterms()
    {
        string zero1 = " select *  from TermsOfUsetbl where priceplanid='" + Request.QueryString["pid"] + "' ";
        SqlCommand cmdzero1 = new SqlCommand(zero1, con);
        SqlDataAdapter adpzero1 = new SqlDataAdapter(cmdzero1);
        DataTable dszero1 = new DataTable();
        adpzero1.Fill(dszero1);

        if (dszero1.Rows.Count > 0)
        {
            lbltermsofuse.Text = dszero1.Rows[0]["termsofuse"].ToString();
        }

    }
    //protected void HyperLink2_Click(object sender, EventArgs e)
    //{

    //    string te = "ViewPricePlan.aspx?id=" + ViewState["ProductId"].ToString() + " ";

    //    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    //}
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        string te = "Priceplancomparision.aspx?id=" + ViewState["ProductId"].ToString() + " ";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }

    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        Response.Redirect("Priceplancomparision.aspx?id=" + ViewState["ProductId"].ToString() + " ");
    }

    protected void carrirfill()
    {
        ddlcarriername.Items.Clear();

        string strcarrier = "select * from SMSCarrirMaster where Country='" + ddlcountry.SelectedValue + "' ";
        SqlCommand cmdcarrier = new SqlCommand(strcarrier, con);
        SqlDataAdapter adpcarrier = new SqlDataAdapter(cmdcarrier);
        DataTable dscarrier = new DataTable();
        adpcarrier.Fill(dscarrier);

        if (dscarrier.Rows.Count > 0)
        {

            ddlcarriername.DataSource = dscarrier;
            ddlcarriername.DataTextField = "CarrirName";
            ddlcarriername.DataValueField = "ID";
            ddlcarriername.DataBind();
        }
        else
        {
            ddlcarriername.Items.Insert(0, "-Select-");
            ddlcarriername.Items[0].Value = "0";

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

    protected void Fillpaymentmode()
    {

        SqlCommand cmd = new SqlCommand();    
       cmd.CommandText = "SELECT * from Payment_mode Where Active='True'"; 
       // cmd.CommandType = CommandType.Text;
        DataTable dt = new DataTable();
        cmd.Connection = con;
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        adp.Fill(dt);
        drppaymode.DataSource = dt;
        drppaymode.DataValueField = "paymentmode_id";
        drppaymode.DataTextField = "paymentmode_name";

        drppaymode.DataBind();
        drppaymode.Items.FindByText("Paypal/OnlineDebit/Credit").Selected = true;
       // drppaymode.Text = "Paypal/OnlineDebit/Credit";

        //drppaymode.Items.Insert(0, "-Select-");
        //.Items[0].Value = "0";
    }
    protected DataTable select(string str)
    {
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);
        return dt;
    }
    //-----------Total Calculation and Display grid
    protected void fillddldataPopup()
    {

        gv_orderdetail.DataSource = null;
        gv_orderdetail.DataBind();
        DataTable dtcln = select(" SELECT  dbo.OrderMasterDetail.ID, dbo.PricePlanMaster.PricePlanId, dbo.Priceplancategory.CategoryName + ' : ' + dbo.PricePlanMaster.PricePlanName AS PricePlanName, dbo.PricePlanMaster.PricePlanAmount,dbo.PortalMasterTbl.PortalName FROM dbo.PricePlanMaster INNER JOIN dbo.OrderMasterDetail ON dbo.PricePlanMaster.PricePlanId = dbo.OrderMasterDetail.Priceplanid INNER JOIN dbo.Priceplancategory ON dbo.PricePlanMaster.PriceplancatId = dbo.Priceplancategory.ID INNER JOIN dbo.PortalMasterTbl ON dbo.PricePlanMaster.PortalMasterId1 = dbo.PortalMasterTbl.Id  WHERE (dbo.OrderMasterDetail.ordermasterid = '" + ViewState["orderid"] + "')");        
        gv_orderdetail.DataSource = dtcln;
        gv_orderdetail.DataBind();
        gv_orderdetail.Visible = true;
        Decimal totalamt = 0;
        gv_orderdetail.GridLines = GridLines.None;
        foreach (GridViewRow gdr in gv_orderdetail.Rows)
        {
            GridViewRow ft = (GridViewRow)(gv_orderdetail.FooterRow);

            string lblgvamt = ((Label)gdr.FindControl("lblgvamt")).Text;
            Decimal strdesimal1 = Convert.ToDecimal(lblgvamt);
            totalamt = totalamt + strdesimal1;
            lblamt.Text = Convert.ToString(totalamt);
            Label lblgv_total = (Label)ft.FindControl("lblgv_total");
            lblgv_total.Text = Convert.ToString(lblamt.Text);
        }
       
    }
    protected void gvpopup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
    }
    protected void gvpopup_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "GetRow")
        {
            //GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
            ////Here i am assuming you are having label inside your gridview
            //Label lbl = (Label)row.FindControl("lblid");
            //Label Label1 = (Label)row.FindControl("Label1");
            //Label lblcid = (Label)row.FindControl("lblcid");

            //lbl.Text = lbl.Text;
            //gvpopup.Visible = false;
            //gvallrest.Visible = true;
            //gvallrest.DataSource = null;
            //gvallrest.DataBind();
            //// DataTable dtcln = select(" SELECT DISTINCT TOP (100) PERCENT dbo.PricePlanMaster.PricePlanName, CONVERT(int, dbo.Priceplanrestrecordallowtbl.RecordsAllowed) AS RecordsAllowed  ,dbo.PricePlanMaster.PricePlanId as pid, dbo.PricePlanMaster.PricePlanAmount, dbo.PriceplanrestrictionTbl.Id, dbo.Priceplancategory.ID AS cid,  dbo.PriceplanrestrictionTbl.NameofRest, dbo.PriceplanrestrictionTbl.TextofQueinSelection, dbo.PriceplanrestrictionTbl.Restingroup, dbo.PriceplanrestrictionTbl.PortalId, dbo.PricePlanMaster.PricePlanId, dbo.Priceplanrestrecordallowtbl.PriceplanRestrictiontblId, dbo.PricePlanMaster.PriceplancatId, dbo.PricePlanMaster.PortalMasterId1, dbo.Priceplancategory.CategoryName FROM  dbo.PriceplanrestrictionTbl LEFT OUTER JOIN dbo.Priceplanrestrecordallowtbl ON dbo.Priceplanrestrecordallowtbl.PriceplanRestrictiontblId = dbo.PriceplanrestrictionTbl.Id INNER JOIN dbo.PricePlanMaster ON dbo.PricePlanMaster.PricePlanId = dbo.Priceplanrestrecordallowtbl.PricePlanId INNER JOIN dbo.Priceplancategory ON dbo.PricePlanMaster.PriceplancatId = dbo.Priceplancategory.ID WHERE    (dbo.PriceplanrestrictionTbl.PortalId = '" + ddlportal.SelectedValue + "') AND dbo.PriceplanrestrictionTbl.Id='" + lbl.Text + "' AND dbo.Priceplancategory.CategoryType='" + Request.QueryString["type"] + "' AND (dbo.PricePlanMaster.active = '1') AND (dbo.Priceplancategory.Status = '1') AND dbo.Priceplanrestrecordallowtbl.RecordsAllowed > " + Label1.Text + "  ORDER BY dbo.Priceplancategory.CategoryName DESC, dbo.PricePlanMaster.PricePlanName DESC, RecordsAllowed asc");
            //fillpriceplancateNew();

            //string category = "";
            //if (ddlpriceplancatagory.SelectedIndex > 0)
            //{
            //    category = " AND ID='" + ddlpriceplancatagory.SelectedValue + "'";
            //}

            //DataTable dtcln = select(" SELECT CategoryName, ID FROM  dbo.Priceplancategory WHERE PortalID='" + ddlportal.SelectedValue + "' and (CategoryType = '" + Request.QueryString["type"] + "') AND (Status = '1') " + category + " ORDER BY CategoryName DESC ");

            //gvallrest.DataSource = dtcln;
            //gvallrest.DataBind();
            //ImageButton1.Visible = true;
            //pnl1search.Visible = true;

            //****

        }
    }


}
