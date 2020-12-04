using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Drawing;

public partial class ShoppingCart_Admin_PaymnentOption : System.Web.UI.Page
{
    //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString"].ConnectionString);
    SqlConnection con;
    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();


        Page.Title = pg.getPageTitle(page);
        Session["pnlM"] = "5";
        Session["pnl5"] = "51";


        Label1.Visible = false;
        if (!IsPostBack)
        {

            string strwh = "SELECT WareHouseId,Name,Address,CurrencyId FROM WareHouseMaster where comid='" + Session["comid"] + "' and WarehouseMaster.Status='1' order by Name";
            SqlCommand cmdwh = new SqlCommand(strwh, con);
            SqlDataAdapter adpwh = new SqlDataAdapter(cmdwh);
            DataTable dtwh = new DataTable();
            adpwh.Fill(dtwh);

            ddlSearchByStore.DataSource = dtwh;
            ddlSearchByStore.DataTextField = "Name";
            ddlSearchByStore.DataValueField = "WareHouseId";

            ddlSearchByStore.DataBind();
            ddlSearchByStore.Items.Insert(0, "-Select-");
        }
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        if (ddlSearchByStore.SelectedItem.Text != "-Select-")
        {
            if (ImageButton3.Text == "Change")
            {
                ImageButton3.Text = "Update";
           
                onpaypal.Enabled = true;
                retpaypal.Enabled = true;
                oncreditcart.Enabled = true;
                retcreditcart.Enabled = true;
                oncheque.Enabled = true;
                retcheque.Enabled = true;
                oncredit.Enabled = true;
                retcredit.Enabled = true;
                oncreditcartoff.Enabled = true;
                retcreditcartoff.Enabled = true;
                ondo.Enabled = true;
                retdo.Enabled = true;
                oncash.Enabled = true;
                retcash.Enabled = true;
                ddlpayacc.Enabled = true;
                ddlcreditcardacc.Enabled = true;
                ddlcheque.Enabled = true;
                ddlcredit.Enabled = true;
                ddlcreditoffacc.Enabled = true;
                ddlcashondeli.Enabled = true;
                ddlcash.Enabled = true;
                ddldebitcreditcardacc.Enabled = true;
                ddlgiftcardacc.Enabled = true;
               // chkdebitcreditcardonline.Enabled = true;
                chkdebitcreditcardretail.Enabled = true;
                chkgiftcardonline.Enabled = true;
                chkgiftcardretail.Enabled = true;


            }
            else if (ImageButton3.Text == "Update")
            {
                ImageButton3.Text = "Change";
                // ImageButton3.ImageUrl = "~/ShoppingCart/images/Change.png";
                onpaypal.Enabled = false;
                retpaypal.Enabled = false;
                oncreditcart.Enabled = false;
                retcreditcart.Enabled = false;
                oncheque.Enabled = false;
                retcheque.Enabled = false;
                oncredit.Enabled = false;
                retcredit.Enabled = false;
                oncreditcartoff.Enabled = false;
                retcreditcartoff.Enabled = false;
                ondo.Enabled = false;
                retdo.Enabled = false;
                oncash.Enabled = false;
                retcash.Enabled = false;

              //  chkdebitcreditcardonline.Enabled = false;
                chkdebitcreditcardretail.Enabled = false;
                chkgiftcardonline.Enabled = false;
                chkgiftcardretail.Enabled = false;


                string stringonpaypal = "False";
                string stringretpaypal = "False";
                string stringoncreditcart = "False";
                string stringretcreditcart = "False";
                string stringonchuque = "False";
                string stringretchuque = "False";
                string stringoncredit = "False";
                string stringretcredit = "False";
                string stringoncreditcart_off = "False";
                string stringretcreditcart_of = "False";
                string stringondo = "False";
                string stringretdo = "False";
                string stringoncash = "False";
                string stringretcash = "False";

                string stringondebitcreditcard = "False";
                string stringretaildebitcreditcard = "False";

                string stringongiftcard = "False";
                string stringretailgiftcard = "False";



                ddlpayacc.Enabled = false;
                ddlcreditcardacc.Enabled = false;
                ddlcheque.Enabled = false;
                ddlcredit.Enabled = false;
                ddlcreditoffacc.Enabled = false;
                ddlcashondeli.Enabled = false;
                ddlcash.Enabled = false;
                ddldebitcreditcardacc.Enabled = false;
                ddlgiftcardacc.Enabled = false;

                if (onpaypal.Checked == true)
                {
                    stringonpaypal = "Paypal";
                }
                if (retpaypal.Checked == true)
                {
                    stringretpaypal = "Paypal";
                }
                if (oncreditcart.Checked == true)
                {
                    stringoncreditcart = "CreditCard";
                }
                if (retcreditcart.Checked == true)
                {
                    stringretcreditcart = "CreditCard";
                }
                if (oncheque.Checked == true)
                {
                    stringonchuque = "Cheque";
                }
                if (retcheque.Checked == true)
                {
                    stringretchuque = "Cheque";
                }
                if (oncredit.Checked == true)
                {
                    stringoncredit = "Credit";
                }
                if (retcredit.Checked == true)
                {
                    stringretcredit = "Credit";
                }
                if (oncreditcartoff.Checked == true)
                {
                    stringoncreditcart_off = "CreditCard(Offline)";
                }
                if (retcreditcartoff.Checked == true)
                {
                    stringretcreditcart_of = "CreditCard(Offline)";
                }
                if (ondo.Checked == true)
                {
                    stringondo = "Dc/Cr(RealTime)";
                }
                if (retdo.Checked == true)
                {
                    stringretdo = "Dc/Cr(RealTime)";
                }
                if (oncash.Checked == true)
                {
                    stringoncash = "Cash";
                }
                if (retcash.Checked == true)
                {
                    stringretcash = "Cash";
                }
                if (chkdebitcreditcardonline.Checked == true)
                {
                    stringondebitcreditcard = "Debit/CreditCard";
                }
                if (chkdebitcreditcardretail.Checked == true)
                {
                    stringretaildebitcreditcard = "Debit/CreditCard";
                }

                if (chkgiftcardonline.Checked == true)
                {
                     stringongiftcard = "GiftCard";
                   
                }
                if (chkgiftcardretail.Checked == true)
                {
                    stringretailgiftcard ="GiftCard";
                }
                


                SqlCommand cmds = new SqlCommand("sp_Update_Storepaymentmethod", con);
                cmds.CommandType = CommandType.StoredProcedure;
                cmds.Parameters.AddWithValue("@Paypal", stringonpaypal);
                cmds.Parameters.AddWithValue("@Craditcard", stringoncreditcart);
                cmds.Parameters.AddWithValue("@Whid", ddlSearchByStore.SelectedValue);
                cmds.Parameters.AddWithValue("@Cheque", stringonchuque);
                cmds.Parameters.AddWithValue("@Credit", stringoncredit);
                cmds.Parameters.AddWithValue("@Creditcart_offline", stringoncreditcart_off);
                cmds.Parameters.AddWithValue("@Do_Co_RealTime", stringondo);
                cmds.Parameters.AddWithValue("@Cash", stringoncash);
                cmds.Parameters.AddWithValue("@onlineCheck", '1');

                cmds.Parameters.AddWithValue("@DebitCreditCard", stringondebitcreditcard);
                cmds.Parameters.AddWithValue("@GiftCard", stringongiftcard);
               


                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmds.ExecuteNonQuery();
                con.Close();
                SqlCommand cmds1 = new SqlCommand("sp_Update_Storepaymentmethod1", con);
                cmds1.CommandType = CommandType.StoredProcedure;
                cmds1.Parameters.AddWithValue("@Paypal", stringretpaypal);
                cmds1.Parameters.AddWithValue("@Craditcard", stringretcreditcart);
                cmds1.Parameters.AddWithValue("@Whid", ddlSearchByStore.SelectedValue);
                cmds1.Parameters.AddWithValue("@Cheque", stringretchuque);
                cmds1.Parameters.AddWithValue("@Credit", stringretcredit);
                cmds1.Parameters.AddWithValue("@Creditcart_offline", stringretcreditcart_of);
                cmds1.Parameters.AddWithValue("@Do_Co_RealTime", stringretdo);
                cmds1.Parameters.AddWithValue("@Cash", stringretcash);
                cmds1.Parameters.AddWithValue("@RetailCheck", '1');
                cmds1.Parameters.AddWithValue("@DebitCreditCard", stringretaildebitcreditcard);
                cmds1.Parameters.AddWithValue("@GiftCard", stringretailgiftcard);

                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmds1.ExecuteNonQuery();
                con.Close();
                String upaccid = "";

               
                string strvg = "Select * from PaymentMethodAccountTbl where Whid='" + ddlSearchByStore.SelectedValue + "'";

                SqlCommand cmdas = new SqlCommand(strvg, con);
                SqlDataAdapter asas = new SqlDataAdapter(cmdas);
                DataTable dsas = new DataTable();
                asas.Fill(dsas);
                if (dsas.Rows.Count > 0)
                {

                    upaccid = "Update PaymentMethodAccountTbl Set PaypalAccId='" + ddlpayacc.SelectedValue + "',CreditCardAccId='" + ddlcreditcardacc.SelectedValue + "',ChequeAccId='" + ddlcheque.SelectedValue + "',CreditAccId='" + ddlcredit.SelectedValue + "',CreditCardoffAccId='" + ddlcreditoffacc.SelectedValue + "',DcCrAccId='" + ddlcashondeli.SelectedValue + "',CashAccId='" + ddlcash.SelectedValue + "',DebitCreditAccId='"+ddldebitcreditcardacc.SelectedValue+"',GiftCardAccId='"+ddlgiftcardacc.SelectedValue+"'  where Whid='" + ddlSearchByStore.SelectedValue + "'";

                }
                else
                {
                    upaccid = "insert into PaymentMethodAccountTbl(PaypalAccId,CreditCardAccId,ChequeAccId,CreditAccId,CreditCardoffAccId,DcCrAccId,CashAccId,Whid,DebitCreditAccId,GiftCardAccId)values('" + ddlpayacc.SelectedValue + "','" + ddlcreditcardacc.SelectedValue + "','" + ddlcheque.SelectedValue + "','" + ddlcredit.SelectedValue + "','" + ddlcreditoffacc.SelectedValue + "','" + ddlcashondeli.SelectedValue + "','" + ddlcash.SelectedValue + "' ,'" + ddlSearchByStore.SelectedValue + "','" + ddldebitcreditcardacc.SelectedValue + "','" + ddlgiftcardacc.SelectedValue + "')";

                }
                SqlCommand cms = new SqlCommand(upaccid, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cms.ExecuteNonQuery();
                con.Close();
                
                Label1.Visible = true;
                Label1.Text = "Record updated successfully";
            }
        }
        else
        {
            Label1.Visible = true;
            Label1.Text = "Select Business Name";
        }
    }
    protected void ddlSearchByStore_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchByStore.SelectedItem.Text != "-Select-")
        {
            fillvluechk();
            Fillaccount();
            fillaccountgiftcard();
            filaacc();
        }
        else
        {
            ImageButton3.Text = "Change";

        }
    }
    public void fillvluechk()
    {
              onpaypal.Checked = false;
              oncreditcart.Checked = false;
              oncheque.Checked = false;
              oncredit.Checked = false;
              oncreditcartoff.Checked = false;
              ondo.Checked = false;
              oncash.Checked = false;

              chkdebitcreditcardonline.Checked = false;
              chkgiftcardonline.Checked = false;
           
             

        string opt = "Select * from Storepaymentmethod where Whid='" + ddlSearchByStore.SelectedValue + "' and onlineCheck='1'";
        SqlDataAdapter adp = new SqlDataAdapter(opt, con);
        DataTable dtp = new DataTable();
        adp.Fill(dtp);
        if (dtp.Rows.Count > 0)
        {
            if (dtp.Rows[0]["Paypal"].ToString() != "False")
                {
                    onpaypal.Checked = true;
                }
            if (dtp.Rows[0]["Craditcard"].ToString() != "False")
                {
                    oncreditcart.Checked = true;
                }
            if (dtp.Rows[0]["Cheque"].ToString() != "False")
                {
                    oncheque.Checked = true;
                }
            if (dtp.Rows[0]["Credit"].ToString() != "False")
                {
                    oncredit.Checked = true;
                }
            if (dtp.Rows[0]["Creditcart_offline"].ToString() != "False")
                {
                    oncreditcartoff.Checked = true;
                }
            if (dtp.Rows[0]["Do_Co_RealTime"].ToString() != "False")
                {
                    ondo.Checked = true;
                }
            if (dtp.Rows[0]["Cash"].ToString() != "False")
                {
                    oncash.Checked = true;

                }
            if (dtp.Rows[0]["DebitCreditCard"].ToString() != "False")
            {
               // chkdebitcreditcardonline.Checked = true;
               

            }
            if (dtp.Rows[0]["GiftCard"].ToString() != "False")
            {
                chkgiftcardonline.Checked = true;

            }
        }
            retpaypal.Checked = false;
            retcreditcart.Checked = false;
            retcheque.Checked = false;
            //retcreditcart.Checked = false;
            retcreditcartoff.Checked = false;
            retcreditcart.Checked = false;
            retdo.Checked = false;
            retcredit.Checked = false;
            retcash.Checked = false;

            chkdebitcreditcardretail.Checked = false;
            chkgiftcardretail.Checked = false;

            string opt1 = "Select * from Storepaymentmethod where Whid='" + ddlSearchByStore.SelectedValue + "' and RetailCheck='1'";
                SqlDataAdapter adp1 = new SqlDataAdapter(opt1, con);
                DataTable dtp1 = new DataTable();
                adp1.Fill(dtp1);
                if (dtp1.Rows.Count > 0)
                {
                        if (dtp1.Rows[0]["Paypal"].ToString() != "False")
                    {
                        retpaypal.Checked = true;
                    }

                        if (dtp1.Rows[0]["Craditcard"].ToString() != "False")
                    {
                        retcreditcart.Checked = true;
                    }

                        if (dtp1.Rows[0]["Cheque"].ToString() != "False")
                    {
                        retcheque.Checked = true;
                    }

                        if (dtp1.Rows[0]["Credit"].ToString() != "False")
                    {
                        retcredit.Checked = true;
                    }

                        if (dtp1.Rows[0]["Creditcart_offline"].ToString() != "False")
                    {
                        retcreditcartoff.Checked = true;
                    }

                        if (dtp1.Rows[0]["Do_Co_RealTime"].ToString() != "False")
                    {
                        retdo.Checked = true;
                    }

                        if (dtp1.Rows[0]["Cash"].ToString() != "False")
                    {
                        retcash.Checked = true;
                    }
                        if (dtp1.Rows[0]["DebitCreditCard"].ToString() != "False")
                        {
                            chkdebitcreditcardretail.Checked = true;
                           
                          
                        }
                        if (dtp1.Rows[0]["GiftCard"].ToString() != "False")
                        {
                            chkgiftcardretail.Checked = true;
                        }
               
        }
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        Label1.Text = "";
        ddlSearchByStore.SelectedIndex = 0;

        onpaypal.Checked = false;
        retpaypal.Checked = false;
        oncreditcart.Checked = false;
        retcreditcart.Checked = false;
        oncheque.Checked = false;
        retcheque.Checked = false;
        oncredit.Checked = false;
        retcredit.Checked = false;
        oncreditcartoff.Checked = false;
        retcreditcartoff.Checked = false;
        ondo.Checked = false;
        retdo.Checked = false;
        oncash.Checked = false;
        retcash.Checked = false;
        chkdebitcreditcardretail.Checked = false;
        chkgiftcardretail.Checked = false;
        chkgiftcardonline.Checked = false;


        onpaypal.Enabled = false;
        retpaypal.Enabled = false;
        oncreditcart.Enabled = false;
        retcreditcart.Enabled = false;
        oncheque.Enabled = false;
        retcheque.Enabled = false;
        oncredit.Enabled = false;
        retcredit.Enabled = false;
        oncreditcartoff.Enabled = false;
        retcreditcartoff.Enabled = false;
        ondo.Enabled = false;
        retdo.Enabled = false;
        oncash.Enabled = false;
        retcash.Enabled = false;

       
        chkdebitcreditcardretail.Enabled = false;
        chkgiftcardonline.Enabled = false;
        chkgiftcardretail.Enabled = false;


        ddlpayacc.Enabled = false;
        ddlcreditcardacc.Enabled = false;
        ddlcheque.Enabled = false;
        ddlcredit.Enabled = false;
        ddlcreditoffacc.Enabled = false;
        ddlcashondeli.Enabled = false;
        ddlcash.Enabled = false;
        ddldebitcreditcardacc.Enabled = false;
        ddlgiftcardacc.Enabled = false;


        ImageButton3.Text = "Change";

        ddlpayacc.Items.Clear();
        ddlcreditcardacc.Items.Clear();
        ddlcheque.Items.Clear();
        ddlcredit.Items.Clear();
        ddlcreditoffacc.Items.Clear();
        ddlcashondeli.Items.Clear();
        ddlcash.Items.Clear();
        ddldebitcreditcardacc.Items.Clear();
        ddlgiftcardacc.Items.Clear();

    }
    protected void LinkButton13_Click(object sender, ImageClickEventArgs e)
    {
       
           // Fillaccount();
            DataTable dt1 = dt();

            ddlpayacc.DataSource = dt1;
            ddlpayacc.DataTextField = "accountname";
            ddlpayacc.DataValueField = "AccountId";
            ddlpayacc.DataBind();
      
    }
    protected void LinkButton97666667_Click(object sender, ImageClickEventArgs e)
    {
        string te = "AccountMaster.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void LinkButtonImageButton14_Click(object sender, ImageClickEventArgs e)
    {
        string te = "AccountMaster.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void LinkButtonImageButton16_Click(object sender, ImageClickEventArgs e)
    {
        string te = "AccountMaster.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    private DataTable dt()
    {
        string str1 = "";


        str1 = "SELECT  AccountMaster.AccountId,GroupCompanyMaster.groupdisplayname,AccountMaster.AccountName as accname, cast(AccountMaster.AccountId as nvarchar) as accid, AccountMaster.AccountName as accountname    FROM         AccountMaster LEFT OUTER JOIN  " +
               "   ClassTypeCompanyMaster RIGHT OUTER JOIN  " +
               "   ClassCompanyMaster ON ClassTypeCompanyMaster.id = ClassCompanyMaster.classtypecompanymasterid RIGHT OUTER JOIN  " +
               "   GroupCompanyMaster ON ClassCompanyMaster.id = GroupCompanyMaster.classcompanymasterid ON AccountMaster.GroupId = GroupCompanyMaster.GroupId where (GroupCompanyMaster.GroupId = 1)and AccountMaster.Status=1 and AccountMaster.compid = '" + Session["comid"] + "' " +
               "   and AccountMaster.Whid='" + ddlSearchByStore.SelectedValue + "' and GroupCompanyMaster.whid='" + ddlSearchByStore.SelectedValue + "'   order by AccountMaster.accountname  ";

        SqlCommand cmd1 = new SqlCommand(str1, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable ds1 = new DataTable();
        adp1.Fill(ds1);
        return ds1;
    }
    private DataTable dtmaster()
    {
        string str1 = "";


        str1 = "SELECT  AccountMaster.AccountId,GroupCompanyMaster.groupdisplayname,AccountMaster.AccountName as accname, cast(AccountMaster.AccountId as nvarchar) as accid, AccountMaster.AccountName as accountname    FROM         AccountMaster LEFT OUTER JOIN  " +
               "   ClassTypeCompanyMaster RIGHT OUTER JOIN  " +
               "   ClassCompanyMaster ON ClassTypeCompanyMaster.id = ClassCompanyMaster.classtypecompanymasterid RIGHT OUTER JOIN  " +
               "   GroupCompanyMaster ON ClassCompanyMaster.id = GroupCompanyMaster.classcompanymasterid ON AccountMaster.GroupId = GroupCompanyMaster.GroupId where (GroupCompanyMaster.GroupId = 1)and AccountMaster.Status=1 and AccountMaster.compid = '" + Session["comid"] + "' " +
               "   and AccountMaster.Whid='" + ddlSearchByStore.SelectedValue + "' and GroupCompanyMaster.whid='" + ddlSearchByStore.SelectedValue + "'   order by AccountMaster.accountname  ";

        SqlCommand cmd1 = new SqlCommand(str1, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable ds1 = new DataTable();
        adp1.Fill(ds1);
        return ds1;
    }
    
    protected void Fillaccount()
    {
        //string str1 = " select Id,AccountName,AccountId from AccountMaster where  AccountMaster.Status=1 and  ClassId=13 and AccountMaster.compid = '" + Session["Comid"].ToString() + "' and AccountMaster.Whid='" + ddlstrname.SelectedValue + "'  order by AccountName asc";
        string str1  ="";
       

         str1 = "SELECT  AccountMaster.AccountId,GroupCompanyMaster.groupdisplayname,AccountMaster.AccountName as accname, cast(AccountMaster.AccountId as nvarchar) as accid, AccountMaster.AccountName as accountname    FROM         AccountMaster LEFT OUTER JOIN  " +
                "   ClassTypeCompanyMaster RIGHT OUTER JOIN  " +
                "   ClassCompanyMaster ON ClassTypeCompanyMaster.id = ClassCompanyMaster.classtypecompanymasterid RIGHT OUTER JOIN  " +
                "   GroupCompanyMaster ON ClassCompanyMaster.id = GroupCompanyMaster.classcompanymasterid ON AccountMaster.GroupId = GroupCompanyMaster.GroupId where (GroupCompanyMaster.GroupId = 1)and AccountMaster.Status=1 and AccountMaster.compid = '" + Session["comid"] + "' " +
                "   and AccountMaster.Whid='" + ddlSearchByStore.SelectedValue + "' and GroupCompanyMaster.whid='" + ddlSearchByStore.SelectedValue + "'   order by AccountMaster.accountname  ";

        SqlCommand cmd1 = new SqlCommand(str1, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataSet ds1 = new DataSet();
        adp1.Fill(ds1);
        ddlpayacc.DataSource = ds1;
        ddlpayacc.DataTextField = "accountname";
        ddlpayacc.DataValueField = "AccountId";
        ddlpayacc.DataBind();
        
        ddlcreditcardacc.DataSource = ds1;
        ddlcreditcardacc.DataTextField = "accountname";
        ddlcreditcardacc.DataValueField = "AccountId";
        ddlcreditcardacc.DataBind();
        
        ddlcheque.DataSource = ds1;
        ddlcheque.DataTextField = "accountname";
        ddlcheque.DataValueField = "AccountId";
        ddlcheque.DataBind();

        ddlcredit.DataSource = ds1;
        ddlcredit.DataTextField = "accountname";
        ddlcredit.DataValueField = "AccountId";
        ddlcredit.DataBind();
        
        ddlcreditoffacc.DataSource = ds1;
        ddlcreditoffacc.DataTextField = "accountname";
        ddlcreditoffacc.DataValueField = "AccountId";
        ddlcreditoffacc.DataBind();
        
        ddlcashondeli.DataSource = ds1;
        ddlcashondeli.DataTextField = "accountname";
        ddlcashondeli.DataValueField = "AccountId";
        ddlcashondeli.DataBind();
        
        ddlcash.DataSource = ds1;
        ddlcash.DataTextField = "accountname";
        ddlcash.DataValueField = "AccountId";
        ddlcash.DataBind();

        ddldebitcreditcardacc.DataSource = ds1;
        ddldebitcreditcardacc.DataTextField = "accountname";
        ddldebitcreditcardacc.DataValueField = "AccountId";
        ddldebitcreditcardacc.DataBind();

    }
    protected void filaacc()
    {
        string strvg = "Select * from PaymentMethodAccountTbl where Whid='" + ddlSearchByStore.SelectedValue + "'";

        SqlCommand cmdas = new SqlCommand(strvg, con);
        SqlDataAdapter asas = new SqlDataAdapter(cmdas);
        DataTable dsas = new DataTable();
        asas.Fill(dsas);
        if (dsas.Rows.Count > 0)
        {

            ddlpayacc.SelectedIndex = ddlpayacc.Items.IndexOf(ddlpayacc.Items.FindByValue(Convert.ToString(dsas.Rows[0]["PaypalAccId"])));

          
            ddlcreditcardacc.Items.IndexOf(ddlcreditcardacc.Items.FindByValue(Convert.ToString(dsas.Rows[0]["CreditCardAccId"])));

           
            ddlcheque.SelectedIndex = ddlcheque.Items.IndexOf(ddlcheque.Items.FindByValue(Convert.ToString(dsas.Rows[0]["ChequeAccId"])));


           
            ddlcredit.SelectedIndex = ddlcredit.Items.IndexOf(ddlcredit.Items.FindByValue(Convert.ToString(dsas.Rows[0]["CreditAccId"])));

          
            ddlcreditoffacc.SelectedIndex = ddlcreditoffacc.Items.IndexOf(ddlcreditoffacc.Items.FindByValue(Convert.ToString(dsas.Rows[0]["CreditCardoffAccId"])));

           
            ddlcashondeli.SelectedIndex = ddlcashondeli.Items.IndexOf(ddlcashondeli.Items.FindByValue(Convert.ToString(dsas.Rows[0]["DcCrAccId"])));

            
            ddlcash.SelectedIndex = ddlcash.Items.IndexOf(ddlcash.Items.FindByValue(Convert.ToString(dsas.Rows[0]["CashAccId"])));

            ddldebitcreditcardacc.SelectedIndex = ddldebitcreditcardacc.Items.IndexOf(ddldebitcreditcardacc.Items.FindByValue(Convert.ToString(dsas.Rows[0]["DebitCreditAccId"])));


            ddlgiftcardacc.SelectedIndex = ddlgiftcardacc.Items.IndexOf(ddlgiftcardacc.Items.FindByValue(Convert.ToString(dsas.Rows[0]["GiftCardAccId"])));

        }
    }
    protected void ImageButton1_Click1(object sender, ImageClickEventArgs e)
    {
        DataTable dt1 = dt();
        ddlcreditcardacc.DataSource = dt1;
        ddlcreditcardacc.DataTextField = "accountname";
        ddlcreditcardacc.DataValueField = "AccountId";
        ddlcreditcardacc.DataBind();
    }
    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dt1 = dt();
        ddlcheque.DataSource = dt1;
        ddlcheque.DataTextField = "accountname";
        ddlcheque.DataValueField = "AccountId";
        ddlcheque.DataBind();
    }
    protected void ImageButton6_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dt1 = dt();
        ddlcredit.DataSource = dt1;
        ddlcredit.DataTextField = "accountname";
        ddlcredit.DataValueField = "AccountId";
        ddlcredit.DataBind();
    }
    protected void ImageButton8_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dt1 = dt();
        ddlcreditoffacc.DataSource = dt1;
        ddlcreditoffacc.DataTextField = "accountname";
        ddlcreditoffacc.DataValueField = "AccountId";
        ddlcreditoffacc.DataBind();
    }
    protected void ImageButton10_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dt1 = dt();
        ddlcashondeli.DataSource = dt1;
        ddlcashondeli.DataTextField = "accountname";
        ddlcashondeli.DataValueField = "AccountId";
        ddlcashondeli.DataBind();

    }
    protected void ImageButton12_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dt1 = dt();
        ddlcash.DataSource = dt1;
        ddlcash.DataTextField = "accountname";
        ddlcash.DataValueField = "AccountId";
        ddlcash.DataBind();
    }
    protected void ImageButtonImageButton15_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dt1 = dt();
        ddldebitcreditcardacc.DataSource = dt1;
        ddldebitcreditcardacc.DataTextField = "accountname";
        ddldebitcreditcardacc.DataValueField = "AccountId";
        ddldebitcreditcardacc.DataBind();
    }
    protected void ImageButtonImageButton17_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dt1 = dtmaster();
        ddlgiftcardacc.DataSource = dt1;
        ddlgiftcardacc.DataTextField = "accountname";
        ddlgiftcardacc.DataValueField = "AccountId";
        ddlgiftcardacc.DataBind();

        ddlgiftcardacc.Items.Insert(0, "-Select-");
        ddlgiftcardacc.Items[0].Value = "0";
    }

    protected void fillaccountgiftcard()
    {
        string str1 = "";


        str1 = "SELECT  AccountMaster.AccountId,GroupCompanyMaster.groupdisplayname,AccountMaster.AccountName as accname, cast(AccountMaster.AccountId as nvarchar) as accid, AccountMaster.AccountName as accountname    FROM         AccountMaster LEFT OUTER JOIN  " +
               "   ClassTypeCompanyMaster RIGHT OUTER JOIN  " +
               "   ClassCompanyMaster ON ClassTypeCompanyMaster.id = ClassCompanyMaster.classtypecompanymasterid RIGHT OUTER JOIN  " +
               "   GroupCompanyMaster ON ClassCompanyMaster.id = GroupCompanyMaster.classcompanymasterid ON AccountMaster.GroupId = GroupCompanyMaster.GroupId where (GroupCompanyMaster.GroupId = 20)and AccountMaster.Status=1 and AccountMaster.compid = '" + Session["comid"] + "' " +
               "   and AccountMaster.Whid='" + ddlSearchByStore.SelectedValue + "' and GroupCompanyMaster.whid='" + ddlSearchByStore.SelectedValue + "'   order by AccountMaster.accountname  ";

        SqlCommand cmd1 = new SqlCommand(str1, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataSet ds1 = new DataSet();
        adp1.Fill(ds1);

        ddlgiftcardacc.DataSource = ds1;
        ddlgiftcardacc.DataTextField = "accountname";
        ddlgiftcardacc.DataValueField = "AccountId";
        ddlgiftcardacc.DataBind();

        ddlgiftcardacc.Items.Insert(0, "-Select-");
        ddlgiftcardacc.Items[0].Value = "0";
       
        
    }
}
