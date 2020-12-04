using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Security.Cryptography;
using System.Configuration;
using System.Data;

public partial class Moborder_now : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    DataTable dt;
    SqlCommand cmd1;
    protected void Page_Load(object sender, EventArgs e)
    {
        txtpassword.Attributes.Add("value", txtpassword.Text);
        txtcnfrmpassword.Attributes.Add("value", txtcnfrmpassword.Text);
        if (!IsPostBack)
        {
            if (Request.QueryString["Pid"] != null)
            {
                GetClientId();

                hdnProductId.Value = Request.QueryString["Pid"].ToString();
                fillterms();
                //2-10   pnlProduct.Visible = false;
            }
            else
            {

            }
            Fillddlcountry();
            Fillddlstatelist();

            DataTable ds = new DataTable();
            ds = FillVersion();

            //lblproductId.Text = Session["ProductId"].ToString();
            lblproductId.Text = "online account product ASSPL 1";
            if (ds.Rows.Count > 0)
            {
                ddlVersion.Text = ds.Rows[0]["VersionNo"].ToString();
            }
        }
        lblmsg.Visible = false;
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
    //protected void ValidateCaptcha(object sender, ServerValidateEventArgs e)
    //{
    //    Captcha1.ValidateCaptcha(txtCaptcha.Text.Trim());
    //    e.IsValid = Captcha1.UserValidated;
    //    if (e.IsValid)
    //    {
    //        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Valid Captcha!');", true);
    //    }
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
    protected void Fillddlcountry()
    {
        string qryStr = "select CountryId,CountryName from CountryMaster order by CountryName";
        ddlcountry.DataSource = (DataTable)select(qryStr);
        fillddlOther(ddlcountry, "CountryName", "CountryId");
        ddlcountry.Items.Insert(0, "-Select-");
        ddlcountry.Items[0].Value = "0";
    }
    public void fillddlOther(DropDownList ddl, String dtf, String dvf)
    {
        ddl.DataTextField = dtf;
        ddl.DataValueField = dvf;
        ddl.DataBind();

    }
    protected DataTable select(string qu)
    {
        SqlCommand cmd = new SqlCommand(qu, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        return dt;
    }

    //private DataTable SelectCountryMaster()
    //{
    //    SqlCommand cmd = new SqlCommand();
    //    dt = new DataTable();
    //    cmd.Connection = con;
    //    cmd.CommandText = "SelectCountryMaster";
    //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
    //    adp.Fill(dt);
    //    return dt;
    //}

    private void Fillddlstatelist()
    {
        try
        {

            ddlstate.Items.Clear();
            if (ddlcountry.SelectedIndex > 0)
            {
                string qryStr = "select StateId,StateName from StateMasterTbl where CountryId=" + ddlcountry.SelectedValue + " order by StateName";
                ddlstate.DataSource = (DataTable)select(qryStr);
                fillddlOther(ddlstate, "StateName", "StateId");
                ddlstate.Items.Insert(0, "-Select-");
                ddlstate.Items[0].Value = "0";
            }
            else
            {
                ddlstate.Items.Insert(0, "-Select-");
                ddlstate.Items[0].Value = "0";
            }

        }
        catch (Exception)
        { }
    }
   
    protected void ddlcountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        Fillddlstatelist();
    }
    protected void GetClientId()
    {
        string str = "select PricePlanMaster.*,PortalMasterTbl.PortalName,Priceplancategory.CategoryName , case when  PricePlanMaster.AllowIPTrack IS NULL     then 'No' else 'Yes' end as GBUSage1 , ProductMaster.* " +
                        " FROM         PricePlanMaster LEFT OUTER JOIN " +
                        " ProductMaster ON PricePlanMaster.ProductId = ProductMaster.ProductId Left outer join Priceplancategory on Priceplancategory.ID=PricePlanMaster.PriceplancatId left outer join PortalMasterTbl on PortalMasterTbl.Id=Priceplancategory.PortalId" +
          " where   PricePlanId='" + Request.QueryString["Pid"].ToString() + "'";
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
            //ibtnPlanINfo.HRef = "http://" + dt.Rows[0]["PricePlanURL"].ToString();
            lblPricePlanId.Text = dt.Rows[0]["PricePlanId"].ToString();
            Label4.Text = dt.Rows[0]["PortalName"].ToString();

            LinkButton1.Text = dt.Rows[0]["CategoryName"].ToString() + " - " + dt.Rows[0]["PricePlanName"].ToString();


            lblamt.Text = dt.Rows[0]["PricePlanAmount"].ToString();
            pnlpayo.Visible = false;
            if (dt.Rows[0]["PayperOrderPlan"].ToString() == "True" && dt.Rows[0]["amountperOrder"].ToString() != "")
            {

                pnlpayo.Visible = true;
                //lblpayporder.Text = dt.Rows[0]["PayperOrderPlan"].ToString();
                lblfreeiorder.Text = dt.Rows[0]["FreeIntialOrders"].ToString();
                lblminidep.Text = dt.Rows[0]["MinimumDeposite"].ToString();
                // lblminamount.Text = dt.Rows[0]["Maxamount"].ToString();
                lblamt.Text = dt.Rows[0]["MinimumDeposite"].ToString();
            }
            else
            {
                //pnlpayo.Visible = false;
            }
        }
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
    protected void btncontinue_Click(object sender, EventArgs e)
    {


        if (Chk.Checked)
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
            //if (dsp.Tables[0].Rows.Count > 0)
            //{
            //    lblmsg.Visible = true;

            //    lblmsg.Text = "Sorry, This Email Is Already Registerd";
            //}
            //else
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
                    //ViewState["email"] = txtemail.Text;

                    str = "INSERT INTO OrderMaster " +
                          " (CompanyName,ContactPerson, ContactPersonDesignation, Address, Email,  phone,PlanId, CompanyLoginId,AdminId, Password, Domain, HostId,Status,companylogo,paypalid) " +
                          " VALUES     ('" + txtcompanyname.Text + "','" + txtcontactprsn.Text + "','" + txtcontactprsndsg.Text + "','" + txtadd.Text + "','" + txtemail.Text + "','" + txtphn.Text + "','" + Convert.ToInt32(lblPricePlanId.Text) + "','" + txtcompanyid.Text + "','" + txtadminuserid.Text + "','" + txtpassword.Text + "','" + txtcompanyid.Text + "','" + lblPricePlanId.Text + "','True','" + ViewState["upfile"] + "','" + txtemail.Text + "')";

                    lblmsg.Visible = true;

                    SqlCommand cmd = new SqlCommand(str, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    ViewState["comid"] = txtcompanyid.Text;
                    string s = " select  OrderID  from OrderMaster where CompanyName='" + txtcompanyname.Text + "'" + " and CompanyLoginId= '" + txtcompanyid.Text + "' and AdminId = '" + txtadminuserid.Text + "' and Password= '" + txtpassword.Text + "'";
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
            Response.Write("<script language='javascript'>alert('Sorry , You can't register without accepting the terms & conditions.');</script>");
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
            string zero1 = "select PricePlanAmount from PricePlanMaster where PricePlanId='" + Request.QueryString["pid"] + "' and PayperOrderPlan='False' and(PricePlanAmount is Null or PricePlanAmount='0' or PricePlanAmount='')";
            SqlCommand cmdzero1 = new SqlCommand(zero1, con);
            SqlDataAdapter adpzero1 = new SqlDataAdapter(cmdzero1);
            DataTable dszero1 = new DataTable();
            adpzero1.Fill(dszero1);
            if (dszero1.Rows.Count > 0)
            {
                Response.Redirect("busiwizlicensekeygeneration.aspx?orz=" + ViewState["orderid"].ToString() + "&cid=" + ViewState["comid"] + "");
            }
            string zero = "select PayperOrderPlan,amountperOrder,FreeIntialOrders,MinimumDeposite,Maxamount from PricePlanMaster where PricePlanId='" + Request.QueryString["pid"] + "' and PayperOrderPlan='True' and(MinimumDeposite is Null or MinimumDeposite='0' or MinimumDeposite='')";
            SqlCommand cmdzero = new SqlCommand(zero, con);
            SqlDataAdapter adpzero = new SqlDataAdapter(cmdzero);
            DataTable dszero = new DataTable();
            adpzero.Fill(dszero);

            if (dszero.Rows.Count > 0)
            {
                Response.Redirect("busiwizlicensekeygeneration.aspx?orz=" + ViewState["orderid"].ToString() + "&cid=" + ViewState["comid"] + "");
            }
            else
            {
                Response.Redirect("busiwizlicensekeygeneration.aspx?orz=" + ViewState["orderid"].ToString() + "&cid=" + ViewState["comid"] + "");
            }

            //else if (Convert.ToString(dszero.Rows[0]["MinimumDeposite"].ToString()) == "" || Convert.ToString(dszero.Rows[0]["MinimumDeposite"].ToString()) == "0")
            //{
            //    
            //}
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
    public int insertCompanyMaster(string OrderId)
    {
        string str = "";

        str = "INSERT INTO CompanyMaster " +
                     " (CompanyName, ContactPerson, ContactPersonDesignation, CompanyWebsite, date, PlanId, Address,Email, " +
                     "  pincode,phone,MobileNo, CompanyLoginId, AdminId,  " +
                     " Password, redirect, active, deactiveReason,HostId,OrderId,ProductId,PricePlanId,Websiteurl,companylogo,paypalid,returnurl,cancelurl,paymentreturnurl,countryId,StateId,city) " +
                   " VALUES     ('" + txtcompanyname.Text + "','" + txtcontactprsn.Text + "','" + txtcontactprsndsg.Text + "',''," +
                   " '" + System.DateTime.Now.Date + "','" + Convert.ToInt32(lblPricePlanId.Text) + "','" + txtadd.Text + "'," +
                   " '" + txtemail.Text + "','" + txtzipcode.Text + "','" + txtphn.Text + "','" + txtmobileno.Text + "'," +
                   " '" + txtcompanyid.Text + "' ,'" + txtadminuserid.Text + "','" + txtpassword.Text + "','" + txtcompanyname.Text + "','0','','0','" + OrderId + "'," + hdnProductId.Value.ToString() + ",'" + Convert.ToInt32(lblPricePlanId.Text) + "','" + txtcompanyname.Text + "','" + ViewState["upfile"] + "','" + ViewState["email"] + "','" + Textreturn.Text + "','" + Textcancel.Text + "','" + txtpaymentnotifyurl.Text + "','" + ddlcountry.SelectedValue + "','" + ddlstate.SelectedValue + "','" + txtcity.Text + "') ";

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

        txtpassword.Text = "";
        txtphn.Text = "";

    }
    protected void txtcompanyid_TextChanged(object sender, EventArgs e)
    {

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
        //DataSet df = new DataSet();
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
}