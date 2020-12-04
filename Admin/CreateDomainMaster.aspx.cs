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
using System.Text;
using System.Security.Cryptography;
public partial class Admin_CreateDomainMaster : System.Web.UI.Page
{
  //  SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["ifilecabinateConnectionString"].ConnectionString);
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
   
    
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["Login"] != null)
        //{
        //    if (Session["Login"].ToString() == null)
        //    {
        //        Response.Redirect("Login.aspx");
        //    }
        //}
        //else
        //{
        //    Response.Redirect("Login.aspx");
        //}
        if (!IsPostBack)
        {
          //  FillClient();
            FillddlClientname();
            FillddlCompany();
            fillgrid();
           
            btnSave.Visible = false;
        }


    }
 

    protected void FillddlClientname()
    {
        string strcln = "SELECT ClientMasterId,CompanyName FROM ClientMaster";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);

         ddlClientname.DataSource = dtcln;
        ddlClientname.DataTextField = "CompanyName";
        ddlClientname.DataValueField = "ClientMasterId";
        ddlClientname.DataBind();

        ddlClientname.Items.Insert(0, "-Select-");
        ddlClientname.Items[0].Value = "0";

    }
    protected void FillddlCompany()
    {
        string str = " Select CompanyId,CompanyName from CompanyMaster";
        SqlCommand cmd = new SqlCommand(str, con);
        DataTable dt = new DataTable();
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        adp.Fill(dt);

        ddlCompany.DataSource = dt;
        ddlCompany.DataTextField = "CompanyName";
        ddlCompany.DataValueField = "CompanyId";
        ddlCompany.DataBind();

        ddlCompany.Items.Insert(0, "-Select-");
        ddlCompany.Items[0].Value = "0";

    }

    protected void txtdomainName_TextChanged(object sender, EventArgs e)
    {
     //   txtdatabaseName.Text = txtdomainName.Text; // txtDatabaseName.Text = txtdomainName.Text;
        

    }
    protected void InsertDATA()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO TempDomainMaster (TempDomainName,DatabaseName,ClientMasterId) " +
                                 "VALUES ('" + txtdomainName.Text + "','" + txtdomainName.Text + ViewState["URLDB"].ToString() + "','"+ ddlClientname.SelectedItem.Value.ToString() +"')", con);
            cmd.Parameters.AddWithValue("@TempDomainName", txtdomainName.Text);
            cmd.Parameters.AddWithValue("@DatabaseName", txtdomainName.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            lblmsg.Visible = true;
            lblmsg.Text = "Your Domain Name is - http://" + txtdomainName.Text + "" + lblDomain.Text + "  and database name is - " + txtdomainName.Text + "" + ViewState["URLDB"].ToString() + "";
        }
        catch (Exception exxx)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Error : " + exxx.Message;
        }



    }
    //protected void Button1_Click(object sender, EventArgs e)
    //{
     
        
    //}
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
    protected void BntKeyG_Click(object sender, EventArgs e)
    {
        if ( txtdomainName.Text.Length > 0)
        {
            string str = "";
            string hashkey = "";
            str = CreateLicenceKey(out hashkey);
            lblLKey.Text = str.ToUpper();
            lblHKey.Text = hashkey.ToUpper();
            btnSave.Visible = true;
        }
        else
        {
            lblLKey.Text = "";
            lblHKey.Text = "";
        }
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
       // InsertDATA();
        InsertLicenseKey();
    }
    protected void InsertLicenseKey()
    {
        SqlConnection conLic = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
        string str11 = "INSERT INTO LicenseMaster " +
                   " (SiteMasterId, CompanyId,LicenseKey, HashKey, LicenseDate,DatabaseName) " +
                   " VALUES     ('1','" + ddlCompany.SelectedValue +"','" + lblLKey.Text + "','" + lblHKey.Text + "','" + DateTime.Now.ToShortDateString() + "','infinal')";
        SqlCommand cmd = new SqlCommand(str11, con);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
    }
    protected void btnCheckDomain_Click(object sender, EventArgs e)
    {
        if (txtdomainName.Text.Length <= 0)
        {
           
            lblDomainAVl1.Text = "Please enter Domain Name.";
            return;
        }
        int i = 0;
        string str = "SELECT  * FROM   tempdomainmaster WHERE  upper(tempdomainname)= '" + txtdomainName.Text.ToUpper().Trim() +"'";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
              lblmsg.Visible = true;
            lblDomainAVl1.Text = "Sorry This Domain  is already exist. Please try another.";
            lblDomainAVl1.ForeColor = System.Drawing.Color.Red;
            lblDomainAVl1.Focus();

        }
        else
        {
            lblDomainAVl1.Text = "Available for you.";
            lblDomainAVl1.ForeColor = System.Drawing.Color.Green;
        }
    }
   
    protected void ddlClientname_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlClientname.SelectedIndex > 0)
        {
            FillProduct();
            fillgrid();
           
        }
    }
    
    
    protected void GetProductURL()
    {
        SqlConnection conLic = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
        string str = "SELECT * from ProductMaster where  ProductId = '" + ddlProductList.SelectedItem.Value.ToString() + "'"; // and SiteMasterId='" + Session["SiteId"].ToString() + "' ";
        SqlCommand cmd = new SqlCommand(str, conLic);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        conLic.Open();
        adp.Fill(dt);
        conLic.Close();
        if (dt.Rows.Count > 0)
        {
            String strurl = dt.Rows[0]["ProductURL"].ToString(); // lblDomain.Visible = true;
            char[] separator1 = new char[] { '.' };
            string[] strSplitArr1 = new string[1];
            strSplitArr1 = strurl.Split(separator1);
            //int s = Convert.ToInt32(strSplitArr1.Length);
            int i = 0;
            string sitename = "";
            for (i = 1; i < strSplitArr1.Length; i++)
            {
                if (i == 1)
                { }
                else
                {
                    sitename = sitename + ".";
                }
                sitename = sitename + strSplitArr1[i].ToString();
            }
            ViewState["URL"] = "." + sitename.ToString();
            lblDomain.Text = ViewState["URL"].ToString();
            sitename = "";
            for (i = 1; i < strSplitArr1.Length -1 ; i++)
            {
                if (i == 1)
                { }
                else
                {
                    sitename = sitename + ".";
                }
                sitename = sitename + strSplitArr1[i].ToString();
                ViewState["URLDB"] = sitename;
            }
        }
    }
    protected void FillProduct()
    {
        string strcln = " SELECT * from  ProductMaster where ClientMasterId= " +  ddlClientname.SelectedItem.Value.ToString();
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        ddlProductList.DataSource = dtcln;
        ddlProductList.DataBind();
        ddlProductList.Items.Insert(0, "-Select-");
        ddlProductList.Items[0].Value = "0";
        ddlProductList.SelectedIndex = 0;
    }
    protected void ddlProductList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProductList.SelectedIndex > 0)
        {
            GetProductURL();
        }
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedIndex > 0)
        {
            fillgrid();
        }
    }
    //protected void FillClient()
    //{
    //    string str = " SELECT   * from   ClientMaster ";
    //    SqlCommand cmd = new SqlCommand(str, con);
    //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
    //    DataTable dt = new DataTable();
    //    adp.Fill(dt);
    //  ddl   ddlClientList.DataSource = dt;
    //    ddlClientList.DataBind();
    //    ddlClientList.Items.Insert(0, "- All -");
    //    ddlClientList.Items[0].Value = "0";
    //    ddlClientList.SelectedIndex = 0;
    //}

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    public void fillgrid()
    {
        DataTable dt = new DataTable();
        dt = getCompanyMaster();
        GridView1.DataSource = dt;
        GridView1.DataBind();
        foreach (GridViewRow gdr in GridView1.Rows)
        {
            if (gdr.Cells[4].Text == "Active")
            {
                //   GridView1.Rows[gdr.RowIndex].BackColor = System.Drawing.Color.LightGreen;

            }
            else
            {
                //   GridView1.Rows[gdr.RowIndex].BackColor = System.Drawing.Color.LightPink;
                //   GridView1.Rows[gdr.RowIndex].ForeColor = System.Drawing.Color.White;
            }
        }

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "edit1")
        {
            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            int i = Convert.ToInt32(GridView1.SelectedDataKey.Value);
            Response.Redirect("CustomerEdit.aspx?CompanyId=" + i + "");
        }
    }
   
    public DataTable getCompanyMaster()
    {
        //string str = "SELECT     CompanyMaster.CompanyId, CompanyMaster.CompanyName, CompanyMaster.ContactPerson, CompanyMaster.ContactPersonDesignation,  "+
        //              " CompanyMaster.CompanyWebsite, CompanyMaster.date, CompanyMaster.PlanId, CompanyMaster.Address, CompanyMaster.Email, CompanyMaster.pincode,  "+
        //              " CompanyMaster.phone, CompanyMaster.fax, CompanyMaster.CompanyLoginId, CompanyMaster.AdminId, CompanyMaster.Password, CompanyMaster.redirect + '.ifilecabinet.com '  as url, "+
        //              " case CompanyMaster.active when 1 then 'Active' else 'Deactive' end as active , CompanyMaster.deactiveReason, PricePlanMaster.PlanName, PricePlanMaster.MaxNoOfUser, PricePlanMaster.MaxStorage, "+
        //              " PricePlanMaster.PricePerMonth "+
        //                " FROM         CompanyMaster INNER JOIN "+
        //             " PricePlanMaster ON CompanyMaster.PlanId = PricePlanMaster.PlanId ";
        //string str = "  SELECT     CompanyMaster.CompanyId, CompanyMaster.CompanyName, CompanyMaster.ContactPerson, CompanyMaster.ContactPersonDesignation, " +
        //              " CompanyMaster.CompanyWebsite, CompanyMaster.date, CompanyMaster.PlanId, CompanyMaster.Address, CompanyMaster.Email AS Expr1, " +
        //              "  CompanyMaster.pincode, CompanyMaster.phone, CompanyMaster.fax AS Expr2, CompanyMaster.CompanyLoginId, CompanyMaster.AdminId, " +
        //               " CompanyMaster.Password, CompanyMaster.redirect + '.ifilecabinet.com ' AS url, CASE CompanyMaster.active WHEN 1 THEN 'Active' ELSE 'Deactive' END AS active, " +
        //               " CompanyMaster.deactiveReason, PricePlanView.*  FROM         CompanyMaster LEFT OUTER JOIN " +
        //                " PricePlanView ON CompanyMaster.PlanId = PricePlanView.PricePlanDetailId ";
        //string str = " SELECT     ClientMaster.ClientMasterId, ClientMaster.CompanyName, ClientMaster.Address1, ClientMaster.Address2, ClientMaster.CountryId, ClientMaster.StateId, " +
        //         " ClientMaster.City, ClientMaster.Zipcode, ClientMaster.ContactPersonName, ClientMaster.Fax1, ClientMaster.Fax2, ClientMaster.Email1, ClientMaster.Email2, " +
        //         " ClientMaster.Phone1, ClientMaster.Phone2, ClientMaster.ClientURL, ClientMaster.CustomerSupportURL, ClientMaster.SalesCustomerSupportURL, " +
        //         " ClientMaster.SalesPhoneNo, ClientMaster.SalesFaxNo, ClientMaster.SalesEmail, ClientMaster.AfterSalesSupportPhoneNo, ClientMaster.AfterSalesSupportFaxNo, " +
        //         " ClientMaster.AfterSalesSupportEmail, ClientMaster.TechSupportPhoneNo, ClientMaster.TechSupportFaxNo, ClientMaster.TechSupportEmail, ClientMaster.FTP, " +
        //         " ClientMaster.FTPUserName, ClientMaster.FTPPassword, ClientMaster.LoginName, ClientMaster.LoginPassword, CountryMaster.CountryId AS Expr1, " +
        //         " CountryMaster.CountryName, CountryMaster.Country_Code, StateMasterTbl.StateId AS Expr2, StateMasterTbl.StateName, StateMasterTbl.CountryId AS Expr3, " +
        //         " StateMasterTbl.State_Code FROM         StateMasterTbl INNER JOIN " +
        //          " CountryMaster ON StateMasterTbl.CountryId = CountryMaster.CountryId RIGHT OUTER JOIN " +
        //          " ClientMaster ON StateMasterTbl.StateId = ClientMaster.StateId AND CountryMaster.CountryId = ClientMaster.CountryId " +
        //                 " WHERE (ClientMaster.ClientMasterId = '" + Convert.ToInt32(Request.QueryString["ClientId"].ToString()) + "')";
        //string str = " SELECT     OrderMaster.AdminId, OrderMaster.Password, OrderMaster.Domain, OrderMaster.HostId, CompanyMaster.CompanyId, CompanyMaster.CompanyLoginId, " +
        //           "  PricePlanMaster.PricePlanId, PricePlanMaster.PricePlanName, PricePlanMaster.PricePlanDesc, PricePlanMaster.active AS Expr15, PricePlanMaster.StartDate, " +
        //           "  PricePlanMaster.EndDate, PricePlanMaster.PricePlanAmount, PricePlanMaster.ProductId, CompanyMaster.CompanyId AS Expr1, CompanyMaster.CompanyName, " +
        //           "  CompanyMaster.ContactPerson, CompanyMaster.ContactPersonDesignation, CompanyMaster.CompanyWebsite, CompanyMaster.date, CompanyMaster.PlanId, " +
        //           "  CompanyMaster.Address, CompanyMaster.Email, CompanyMaster.pincode, CompanyMaster.phone, CompanyMaster.fax, CompanyMaster.CompanyLoginId AS Expr2," +
        //           "  CompanyMaster.AdminId AS Expr3, CompanyMaster.Password AS Expr4, CompanyMaster.redirect, CompanyMaster.active, CompanyMaster.deactiveReason, " +
        //           "  CompanyMaster.PlanActive, CompanyMaster.HostId AS Expr5, CompanyMaster.StateId, CompanyMaster.OrderId, CompanyMaster.ProductId AS Expr6, " +
        //           "  ProductMaster.ClientMasterId, OrderMaster.PlanId AS Expr7, OrderMaster.OrderId AS Expr8 ,ProductMaster.ProductURL" +
        //           " FROM         ProductMaster LEFT OUTER JOIN " +
        //             " PricePlanMaster ON ProductMaster.ProductId = PricePlanMaster.ProductId RIGHT OUTER JOIN " +
        //             " CompanyMaster ON ProductMaster.ProductId = CompanyMaster.ProductId FULL OUTER JOIN " +
        //             " OrderMaster ON CompanyMaster.OrderId = OrderMaster.OrderId " +
        //            " WHERE     (ProductMaster.ClientMasterId = '" + Session["ClientId"].ToString() + "')";
        //string str = "SELECT     OrderMaster.OrderId, CompanyMaster.CompanyName, CompanyMaster.ContactPerson, CompanyMaster.ContactPersonDesignation, CompanyMaster.Address, CompanyMaster.Email, " + 
        //              " OrderMaster.phone, OrderMaster.fax, OrderMaster.PlanId, OrderMaster.AdminId, OrderMaster.Password, OrderMaster.Domain, OrderMaster.HostId, " + 
        //            " CompanyMaster.CompanyId ,  "+ 
        //             "  CompanyMaster.CompanyWebsite, CompanyMaster.date, CompanyMaster.CompanyLoginId, CompanyMaster.redirect, CompanyMaster.active, " + 
        //             " CompanyMaster.deactiveReason, CompanyMaster.PlanActive, CompanyMaster.StateId, OrderPaymentSatus.OrderPaymentId, OrderPaymentSatus.PaymentStatus, " + 
        //             " OrderPaymentSatus.TransactionID, CASE CompanyMaster.HostId WHEN 1 THEN 'User Server' ELSE 'busiwiz's Server' END AS HostName, " + 
        //             " TempDomainMaster.TempDomainId, TempDomainMaster.TempDomainName, OrderMaster.Status, TempDomainMaster.DatabaseName, " + 
        //             " PricePlanMaster.PricePlanDesc FROM         PricePlanMaster RIGHT OUTER JOIN " + 
        //              " CompanyMaster ON PricePlanMaster.ProductId = CompanyMaster.ProductId LEFT OUTER JOIN " + 
        //              " TempDomainMaster ON CompanyMaster.CompanyId = TempDomainMaster.CompanyId RIGHT OUTER JOIN " + 
        //              " OrderMaster LEFT OUTER JOIN OrderPaymentSatus ON OrderMaster.OrderId = OrderPaymentSatus.OrderId ON CompanyMaster.OrderId = OrderMaster.OrderId" +
        //              " WHERE     (ProductMaster.ClientMasterId = '" + Session["ClientId"].ToString() + "')";
        //        string str = "SELECT     OrderMaster.OrderId, CompanyMaster.CompanyName, CompanyMaster.ContactPerson, CompanyMaster.ContactPersonDesignation, CompanyMaster.Address, " +
        //        " CompanyMaster.Email, OrderMaster.phone, OrderMaster.fax, OrderMaster.PlanId, OrderMaster.AdminId, OrderMaster.Password, OrderMaster.Domain, " +
        //         "             OrderMaster.HostId, CompanyMaster.CompanyId, CompanyMaster.CompanyWebsite, CompanyMaster.date, CompanyMaster.CompanyLoginId, " +
        //          "            CompanyMaster.redirect, CompanyMaster.active, CompanyMaster.deactiveReason, CompanyMaster.PlanActive, CompanyMaster.StateId, " +
        //           "           OrderPaymentSatus.OrderPaymentId, OrderPaymentSatus.PaymentStatus, OrderPaymentSatus.TransactionID, " +
        //            "          CASE CompanyMaster.HostId WHEN 1 THEN 'User Server' ELSE 'busiwiz Server' END AS HostName, TempDomainMaster.TempDomainId, " +
        //             "         TempDomainMaster.TempDomainName, OrderMaster.Status, TempDomainMaster.DatabaseName, PricePlanMaster.PricePlanDesc, PricePlanMaster.PricePlanName,PricePlanMaster.PricePlanAmount, ProductMaster.*" +
        //" FROM        PricePlanMaster RIGHT OUTER JOIN ProductMaster ON PricePlanMaster.ProductId = ProductMaster.ProductId RIGHT OUTER JOIN " +
        //"                      CompanyMaster ON ProductMaster.ProductId = CompanyMaster.ProductId AND PricePlanMaster.ProductId = CompanyMaster.ProductId LEFT OUTER JOIN " +
        // "                     TempDomainMaster ON CompanyMaster.CompanyId = TempDomainMaster.CompanyId RIGHT OUTER JOIN " +
        //  "                    OrderMaster LEFT OUTER JOIN OrderPaymentSatus ON OrderMaster.OrderId = OrderPaymentSatus.OrderId ON CompanyMaster.OrderId = OrderMaster.OrderId " +
        //         " WHERE     (ProductMaster.ClientMasterId = '" + Session["ClientId"].ToString() + "')";
        //string str ="SELECT     OrderMaster.OrderId AS Expr1, CompanyMaster.CompanyName, CompanyMaster.ContactPerson, CompanyMaster.ContactPersonDesignation, " + 
        //" CompanyMaster.Address, CompanyMaster.Email, OrderMaster.phone, OrderMaster.fax, OrderMaster.PlanId, OrderMaster.AdminId, OrderMaster.Password, " + 
        // "                     OrderMaster.Domain, OrderMaster.HostId, CompanyMaster.CompanyId, CompanyMaster.CompanyWebsite, CompanyMaster.date, CompanyMaster.CompanyLoginId, " + 
        //  "                    CompanyMaster.redirect, CompanyMaster.active, CompanyMaster.deactiveReason, CompanyMaster.PlanActive, CompanyMaster.StateId, " + 
        //   "                   CASE CompanyMaster.HostId WHEN 1 THEN 'User Server' ELSE 'busiwiz Server' END AS HostName, TempDomainMaster.TempDomainId, " + 
        //    "                  TempDomainMaster.TempDomainName, OrderMaster.Status, TempDomainMaster.DatabaseName, PricePlanMaster.PricePlanDesc, PricePlanMaster.PricePlanName, " + 
        //     "                 PricePlanMaster.PricePlanAmount, ProductMaster.ProductId, ProductMaster.ClientMasterId, ProductMaster.ProductName, ProductMaster.ProductURL, " + 
        //      "                ProductMaster.PricePlanURL, OrderPaymentSatus.* " + 
        //" FROM         OrderPaymentSatus RIGHT OUTER JOIN " + 
        //                      " OrderMaster ON OrderPaymentSatus.OrderId = OrderMaster.OrderId LEFT OUTER JOIN " + 
        //                      " PricePlanMaster RIGHT OUTER JOIN " +
        //                      " CompanyMaster ON PricePlanMaster.PricePlanId = CompanyMaster.PricePlanId LEFT OUTER JOIN " + 
        //                      " TempDomainMaster ON CompanyMaster.CompanyId = TempDomainMaster.CompanyId ON OrderMaster.OrderId = CompanyMaster.OrderId RIGHT OUTER JOIN " +
        //"                      ProductMaster ON PricePlanMaster.ProductId = ProductMaster.ProductId AND CompanyMaster.ProductId = ProductMaster.ProductId " +
        // " WHERE     (ProductMaster.ClientMasterId = '" + Session["ClientId"].ToString() + "')";
        string str = " SELECT     TempDomainMaster.TempDomainId, TempDomainMaster.TempDomainName, " +
                     " CASE TempDomainMaster.Alloted WHEN 1 THEN 'Yes' ELSE 'No' END AS Allocated, " +
                     " CASE TempDomainMaster.Active WHEN '1' THEN 'Yes' ELSE 'No' END AS Active, TempDomainMaster.DatabaseName AS Expr9, " +
                     " TempDomainMaster.CompanyId AS Expr10, TempDomainMaster.ClientMasterId, ClientMaster.ClientMasterId AS Expr1, ClientMaster.CompanyName, " +
                     " ClientMaster.Address1, ClientMaster.Address2, ClientMaster.CountryId, ClientMaster.StateId, ClientMaster.City, ClientMaster.Zipcode, " +
                     " ClientMaster.ContactPersonName, ClientMaster.Fax1, ClientMaster.Fax2, ClientMaster.Email1, ClientMaster.Email2, ClientMaster.Phone1, " +
                     " ClientMaster.Phone2, ClientMaster.ClientURL, ClientMaster.CustomerSupportURL, ClientMaster.SalesCustomerSupportURL, " +
                     " ClientMaster.SalesPhoneNo, ClientMaster.SalesFaxNo, ClientMaster.SalesEmail, ClientMaster.AfterSalesSupportPhoneNo," +
                     " ClientMaster.AfterSalesSupportFaxNo, ClientMaster.AfterSalesSupportEmail, ClientMaster.TechSupportPhoneNo, ClientMaster.TechSupportFaxNo, " +
                     " ClientMaster.TechSupportEmail, ClientMaster.FTP, ClientMaster.FTPUserName, ClientMaster.FTPPassword, ClientMaster.LoginName, " +
                     " ClientMaster.LoginPassword, ClientMaster.ClientPricePlanID, CompanyMaster.CompanyId AS Expr2, CompanyMaster.CompanyName AS Expr3," +
                     " CompanyMaster.ContactPerson, CompanyMaster.ContactPersonDesignation, CompanyMaster.CompanyWebsite, CompanyMaster.date, " +
                     " CompanyMaster.PlanId, CompanyMaster.Address, CompanyMaster.Email, CompanyMaster.pincode, CompanyMaster.phone, CompanyMaster.fax,  " +
                     " CompanyMaster.CompanyLoginId, CompanyMaster.AdminId, CompanyMaster.Password, CompanyMaster.redirect, CompanyMaster.active AS Expr4, " +
                     " CompanyMaster.deactiveReason, CompanyMaster.PlanActive, CompanyMaster.HostId, CompanyMaster.StateId AS Expr5, CompanyMaster.OrderId, " +
                     " CompanyMaster.ProductId AS Expr6, CompanyMaster.PricePlanId AS Expr7, PricePlanMaster.PricePlanId, PricePlanMaster.PricePlanName, " +
                     " PricePlanMaster.PricePlanDesc, PricePlanMaster.active AS Expr8, PricePlanMaster.StartDate, PricePlanMaster.EndDate, " +
                     " PricePlanMaster.PricePlanAmount, PricePlanMaster.ProductId, PricePlanMaster.DurationMonth, PricePlanMaster.AllowIPTrack," +
                     " PricePlanMaster.GBUsage, PricePlanMaster.MaxUser, PricePlanMaster.TrafficinGB, PricePlanMaster.TotalMail, " +
                     " LicenseMaster.SiteMasterId AS Expr11, LicenseMaster.* " +
                     " FROM         LicenseMaster RIGHT OUTER JOIN " +
                     " CompanyMaster ON LicenseMaster.CompanyId = CompanyMaster.CompanyId LEFT OUTER JOIN " +
                     " PricePlanMaster ON CompanyMaster.PricePlanId = PricePlanMaster.PricePlanId RIGHT OUTER JOIN " +
                     " TempDomainMaster ON CompanyMaster.CompanyId = TempDomainMaster.ClientMasterId LEFT OUTER JOIN " +
                     " ClientMaster ON TempDomainMaster.ClientMasterId = ClientMaster.ClientMasterId "; 
        if ( ddlClientname.SelectedIndex > 0)
        {
            str = str + " WHERE     (TempDomainMaster.ClientMasterId = '" + ddlClientname.SelectedItem.Value.ToString() + "')";
        }
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        return dt;
    }
    //protected void ddlClientList_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlClientList.SelectedIndex > 0)
    //    {
    //        fillgrid();
    //    }
    //}
}
