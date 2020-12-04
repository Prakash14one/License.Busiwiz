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
using System.Data.SqlClient;


public partial class ExpenseInvoice : System.Web.UI.Page
{
    public Int32 InventoryWarehouseMasterId;
    public Int32 InventoryWarehouseMasterIdB;
    public Int32 InventoryWarehouseMasterIdP;
    public Int32 InventoryWarehouseMasterIdN;
    Int32 warehousefinalid;
    // Int32 cashid;
    string a6;
    object com;
    string page;
   
    SqlConnection con = new SqlConnection(PageConn.connnn);
    SqlConnection conn = new SqlConnection(PageConn.connnn);
  
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        }
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        //  lblcash.Visible = false;
        // ddlcashtype.Visible = false;
        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        page = strSplitArr[i - 1].ToString();
        //try
        //{ 
        //   ImageButton1.Attributes.Add("onclick", "window.open('Default6.aspx', ''," + "'width=590,height=630,top=240,left=220,right=40,bottom=20,toolbar=no fullscreen=0,titlebar=no,resizable=0')");
        Label8.Text = "";
        lblmmssgg.Text = "";
        Label2.Text = "";
      



        

        if (!IsPostBack)
        {
            txtpayduedate.Text = DateTime.Now.ToShortDateString();
            DataTable ds = ClsStore.SelectStorename();
            ddlWarehouse.DataSource = ds;
            ddlWarehouse.DataTextField = "Name";
            ddlWarehouse.DataValueField = "WareHouseId";
            ddlWarehouse.DataBind();


             DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

                if (dteeed.Rows.Count > 0)
                {
                    ddlWarehouse.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
                }



                RadioButtonList3_SelectedIndexChanged(sender, e);



                ddlWarehouse_SelectedIndexChanged(sender, e);


                txtDate.Text = System.DateTime.Today.ToShortDateString();
              
            
                CheckBox1.Checked = true;
                CheckBox1.Enabled = false;
                filltrans();
                fillpartyddl();



               
        }
      
    }
    protected void filletype()
    {
        string s121 = " SELECT     EntryTypeId, Max(EntryNumber) as Number FROM     TranctionMaster " +
                              " Where EntryTypeId = 4 and Whid='" + ddlWarehouse.SelectedValue + "' Group by EntryTypeId ";
        SqlCommand cm131 = new SqlCommand(s121, con);
        cm131.CommandType = CommandType.Text;
        SqlDataAdapter da131 = new SqlDataAdapter(cm131);
        DataSet ds131 = new DataSet();
        da131.Fill(ds131);

        if (ds131.Tables[0].Rows.Count > 0)
        {

            int q = Convert.ToInt32(ds131.Tables[0].Rows[0]["Number"]) + 1;
            lblEntryNo.Text = q.ToString();
        }
        else
        {
            lblEntryNo.Text = "1";
        }
    }



    protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
    {
        //Calendar2.Visible = true;
    }

    protected void ddlpartyName_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string str123 = " SELECT     PartyID, Account,City ,State      ,Country, Compname, PartyTypeId, Address FROM         Party_master " +
        //                 " Where PartyID = '" + ddlpartyName.SelectedValue + "'";
        //SqlCommand cm133 = new SqlCommand(str123, con);
        //cm133.CommandType = CommandType.Text;
        //SqlDataAdapter da133 = new SqlDataAdapter(cm133);
        //DataSet ds133 = new DataSet();
        //da133.Fill(ds133);

        //if (ds133.Tables[0].Rows.Count > 0)
        //{
        //    txtAddress.Text = ds133.Tables[0].Rows[0]["Address"].ToString();

        //    string strconu = " SELECT  CountryId       ,CountryName      ,Country_Code  FROM CountryMaster where CountryId=" + Convert.ToInt32(isintornot(ds133.Tables[0].Rows[0]["Country"].ToString())) + " ";
        //    string Countr = returnCSCt(strconu, "CountryName");
        //    string ct3 = " SELECT  CityId      ,CityName      ,StateId  FROM CityMasterTbl where CityId=" + Convert.ToInt32(isintornot(ds133.Tables[0].Rows[0]["City"].ToString()));
        //    string STatecin = "SELECT  StateId      ,StateName,CountryId  ,State_Code  FROM StateMasterTbl where StateId=" + Convert.ToInt32(isintornot(ds133.Tables[0].Rows[0]["State"].ToString()));
        //    string st = returnCSCt(STatecin, "StateName");
        //    string ct = returnCSCt(ct3, "CityName");

        //    txtAddress.Text += "<br/>" + Countr + "<br/>" + st + "<br/>" + ct + "<br/>";


        //}
    }
    protected string returnCSCt(string strrr, string rtSTT)
    {
        SqlCommand cmdstrr = new SqlCommand(strrr, con);
        SqlDataAdapter adprrr = new SqlDataAdapter(cmdstrr);
        DataTable dtrrr = new DataTable();
        adprrr.Fill(dtrrr);
        string SCTST = "";
        if (dtrrr.Rows.Count > 0)
        {
            SCTST = dtrrr.Rows[0][rtSTT].ToString();
        }
        return SCTST;

    }

    protected int isintornot(string ck)
    {
        int ihk = 0;
        try
        {
            ihk = Convert.ToInt32(ck);
            return ihk;
        }
        catch
        {
            return ihk;
        }
        //return ihk;

    }
    protected void recal()
    {
        if (grdinvoice.Rows.Count > 0)
        {
            btSubmit.Visible = false;
            btCal.Visible = true;

            Decimal grdcalamt = 0;
            Decimal grdtax1amt = 0;
              Decimal grdtax2amt = 0;
              Decimal grdtax3amt = 0;
              foreach (GridViewRow gdr in grdinvoice.Rows)
              {
                  TextBox txtamount = (TextBox)gdr.FindControl("txtamount");

                  if (txtamount.Text.Length > 0)
                  {
                      TextBox txtgt1 = (TextBox)gdr.FindControl("txtgt1");
                      TextBox txtgt2 = (TextBox)gdr.FindControl("txtgt2");
                      TextBox txtgt3 = (TextBox)gdr.FindControl("txtgt3");
                      grdcalamt += Convert.ToDecimal(txtamount.Text);
                      if (txtgt1.Text.Length > 0)
                      {
                          grdtax1amt += Convert.ToDecimal(txtgt1.Text);
                      }
                      if (txtgt2.Text.Length > 0)
                      {
                          grdtax2amt += Convert.ToDecimal(txtgt2.Text);
                      }
                      if (txtgt3.Text.Length > 0)
                      {
                          grdtax3amt += Convert.ToDecimal(txtgt3.Text);
                      }
                  }
              }
              if (txtTax1.Enabled == false)
              {
                  txtTax1.Text = Math.Round(grdtax1amt, 2).ToString();
              }
              if (txtTax2.Enabled == false)
              {
                  txtTax2.Text = Math.Round(grdtax2amt, 2).ToString();
              }
              if (txtTax3.Enabled == false)
              {
                  txtTax3.Text = Math.Round(grdtax3amt, 2).ToString();
              }
            if (txtTax1.Text.Length <= 0)
            {
                txtTax1.Text = "0";
            }
            if (txtTax2.Text.Length <= 0)
            {
                txtTax2.Text = "0";
            }
            if (txtTax3.Text.Length <= 0)
            {
                txtTax3.Text = "0";
            }
            if (txtShippCharge.Text.Length <= 0)
            {
                txtShippCharge.Text = "0";
            }




            txtValurRecd.Text = Math.Round(grdcalamt, 2).ToString();

            decimal d5 = 0;
            if (txtincoperation.Text.Length < 0)
            {
                txtincoperation.Text = "0";
            }
            if (CheckBox2.SelectedIndex == 0)
            {
            //    d5 = grdcalamt + Convert.ToDecimal(txtShippCharge.Text) - Convert.ToDecimal(txtincoperation.Text);
                d5 = grdcalamt + Convert.ToDecimal(txtTax1.Text) + Convert.ToDecimal(txtTax2.Text) + Convert.ToDecimal(txtTax3.Text) + Convert.ToDecimal(txtShippCharge.Text) - Convert.ToDecimal(txtincoperation.Text);

            }
            else
            {

                d5 = grdcalamt + Convert.ToDecimal(txtTax1.Text) + Convert.ToDecimal(txtTax2.Text) + Convert.ToDecimal(txtTax3.Text) + Convert.ToDecimal(txtShippCharge.Text) - Convert.ToDecimal(txtincoperation.Text);

            }
           
            txtNetAmount.Text = Math.Round(Convert.ToDecimal(d5), 2).ToString();

        }
        else
        {
            txtNetAmount.Text = "0";
           
            txtValurRecd.Text = "0";
            txtincoperation.Text = "0";
            txtTax1.Text = "0";
            txtTax2.Text = "0";
            txtTax3.Text = "0";
            txtShippCharge.Text = "0";
           
        }
    }
    protected void btCal_Click(object sender, EventArgs e)
    {
       
                recal();
                Label8.Text = "";
                if (Convert.ToDecimal(txtNetAmount.Text) > 0)
                {
                    pnlconf.Enabled = false;
                    btCal.Visible = false;

                    btSubmit.Visible = true;

                    btCal.Visible = false;
                }

            
    }

    protected void btSubmit_Click(object sender, EventArgs e)
    {
        int accesin = 0;
        DataTable dtdr = select("Select  AccountAddDataRequireTbl.* from AccountAddDataRequireTbl  where  Compid='" + Session["Comid"] + "' and Access='1'");

        if (dtdr.Rows.Count > 0)
        {
            DataTable dtd = select("Select Distinct AccountPageDataAddDesignationRight.* from AccountPageDataAddDesignationRight  where   DesignationId='" + Convert.ToString(Session["DesignationId"]) + "'");

            if (dtd.Rows.Count > 0)
            {
                if (Convert.ToInt32(dtd.Rows[0]["Accessbus"]) == 0)
                {
                    accesin = 1;
                }
                else if (Convert.ToInt32(dtd.Rows[0]["Accessbus"]) == Convert.ToInt32(ddlWarehouse.SelectedValue))
                {
                    accesin = 1;
                }
            }
        }
        else
        {
            accesin = 1;
        }
        //int accesin = ClsAccountAppr.AccessInsert(ddlWarehouse.SelectedValue);

        if (accesin == 1)
        {

            DataTable dtss = (DataTable)select("select Convert(nvarchar,StartDate,101) as StartDate, Convert(nvarchar,EndDate,101) as EndDate from [ReportPeriod] where Active='1' and Whid='" + ddlWarehouse.SelectedValue + "'");

            if (dtss.Rows.Count > 0)
            {
                if (Convert.ToDateTime(txtDate.Text) >= Convert.ToDateTime(dtss.Rows[0]["StartDate"]) && Convert.ToDateTime(txtDate.Text) <= Convert.ToDateTime(dtss.Rows[0]["EndDate"]))
                {


                    bool access = UserAccess.Usercon("TranctionMaster", "", "Tranction_Master_Id", "", "", "compid", "TranctionMaster");
                    if (access == true)
                    {
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }

                        SqlTransaction transaction = con.BeginTransaction();

                        try
                        {
                            SqlCommand cd3 = new SqlCommand("Sp_Insert_TranctionMasterRetIdentity", con);


                            cd3.CommandType = CommandType.StoredProcedure;
                            cd3.Parameters.AddWithValue("@Date", Convert.ToDateTime(txtDate.Text).ToShortDateString());
                            cd3.Parameters.AddWithValue("@EntryNumber", Convert.ToInt32(lblEntryNo.Text));
                            cd3.Parameters.AddWithValue("@EntryTypeId", "4");
                            cd3.Parameters.AddWithValue("@UserId", Session["userid"].ToString());
                            cd3.Parameters.AddWithValue("@Tranction_Amount", Convert.ToDecimal(txtNetAmount.Text));
                            cd3.Parameters.AddWithValue("@whid", ddlWarehouse.SelectedValue);

                            cd3.Parameters.AddWithValue("@compid", Session["comid"]);


                            cd3.Parameters.Add(new SqlParameter("@Tranction_Master_Id", SqlDbType.Int));
                            cd3.Parameters["@Tranction_Master_Id"].Direction = ParameterDirection.Output;
                            cd3.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
                            cd3.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

                            cd3.Transaction = transaction;
                            int Id1 = (int)cd3.ExecuteNonQuery();
                            Id1 = Convert.ToInt32(cd3.Parameters["@Tranction_Master_Id"].Value);

                            ViewState["tid"] = Id1;
                            SqlCommand cd = new SqlCommand("Sp_Insert_PurchaseDetailsRetIdentity", con);


                            cd.CommandType = CommandType.StoredProcedure;
                            cd.Parameters.AddWithValue("@PartyName", ddlpartyName.SelectedValue.ToString());
                            cd.Parameters.AddWithValue("@Date", Convert.ToDateTime(txtDate.Text));
                            cd.Parameters.AddWithValue("@EntryNumer", Convert.ToInt32(lblEntryNo.Text));
                            cd.Parameters.AddWithValue("@PurchaseInvoiceNumber", Convert.ToString(txtInvNo.Text));
                            cd.Parameters.AddWithValue("@ShippingDocumentNumber", 10);
                            cd.Parameters.AddWithValue("@ShippingId", "");
                            cd.Parameters.AddWithValue("@ShippingTrackingNumber", txtTrackingNo.Text.ToString());
                            cd.Parameters.AddWithValue("@NetInvoiceNumber", "0");
                            cd.Parameters.AddWithValue("@TexAmount1", txtTax1.Text);
                            cd.Parameters.AddWithValue("@texAmount2", txtTax2.Text);
                            cd.Parameters.AddWithValue("@Invpertax", Convert.ToBoolean(CheckBox2.SelectedIndex));
                            cd.Parameters.AddWithValue("@shippingandHandlingCharg", txtShippCharge.Text);
                            cd.Parameters.AddWithValue("@TotalAmt", Convert.ToDecimal(txtNetAmount.Text));
                            cd.Parameters.AddWithValue("@WareHouseMasterId", ddlWarehouse.SelectedValue);
                            cd.Parameters.AddWithValue("@compid", Session["comid"]);


                            cd.Parameters.AddWithValue("@TexAmount3", Convert.ToDecimal(txtTax3.Text));
                            cd.Parameters.AddWithValue("@AcseprateTax", Convert.ToBoolean(rdl1.SelectedIndex));
                            cd.Parameters.AddWithValue("@TaxoptionType", lbltxtop.Text);
                            cd.Parameters.AddWithValue("@Pertax1Id", lbltaxper1.Text);
                            cd.Parameters.AddWithValue("@Pertax2Id", lbltaxper2.Text);
                            cd.Parameters.AddWithValue("@Pertax3Id", lbltaxper3.Text);
                            cd.Parameters.AddWithValue("@Transporterid", ddlTransporter.SelectedValue);

                            cd.Parameters.AddWithValue("@TransId", Id1);
                            cd.Parameters.AddWithValue("@InvType", RadioButtonList1.SelectedIndex);
                            cd.Parameters.AddWithValue("@discountorrebet", txtincoperation.Text);

                            cd.Parameters.Add(new SqlParameter("@Purchase_Details_Id", SqlDbType.Int));
                            cd.Parameters["@Purchase_Details_Id"].Direction = ParameterDirection.Output;
                            cd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
                            cd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

                            cd.Transaction = transaction;
                            int Id = (int)cd.ExecuteNonQuery();
                            Id = Convert.ToInt32(cd.Parameters["@Purchase_Details_Id"].Value);


                            DataTable dt1 = new DataTable();
                            dt1 = (DataTable)ViewState["dt"];



                            if (RadioButtonList1.SelectedIndex == 0)
                            {
                                string ggg = " INSERT INTO  TranctionMasterSuppliment  (Tranction_Master_Id  ,AmountDue " +
                                             " ,Memo  ,Party_MasterId,GrnMaster_Id) " +
                                            "  VALUES('" + Convert.ToInt32(Id1) + "','" + Convert.ToDecimal(txtNetAmount.Text) + "', " +
                                            "   '" + txtdesc.Text + "' , '" + Convert.ToInt32(ddlpartyName.SelectedValue) + "' ,'" + txtpayduedate.Text + "')";
                                SqlCommand cd45r = new SqlCommand(ggg, con);
                                cd45r.CommandType = CommandType.Text;
                                cd45r.Transaction = transaction;
                                cd45r.ExecuteNonQuery();
                            }
                            else
                            {
                                string ggg = " INSERT INTO  TranctionMasterSuppliment  (Tranction_Master_Id  ,AmountDue " +
                                            " ,Memo  ,Party_MasterId,GrnMaster_Id) " +
                                           "  VALUES('" + Convert.ToInt32(Id1) + "','" + Convert.ToDecimal(0) + "', " +
                                           "   '" + txtdesc.Text + "', '" + Convert.ToInt32(ddlpartyName.SelectedValue) + "','" + txtpayduedate.Text + "' )";
                                SqlCommand cd45r = new SqlCommand(ggg, con);
                                cd45r.CommandType = CommandType.Text;
                                cd45r.Transaction = transaction;
                                cd45r.ExecuteNonQuery();
                            }




                            string acFormPjid = " SELECT Account FROM  Party_master where PartyID='" + ddlpartyName.SelectedValue + "' and id='" + Session["comid"] + "' and Whid='" + ddlWarehouse.SelectedValue + "' ";
                            SqlCommand cmdacFrompjid = new SqlCommand(acFormPjid, conn);
                            SqlDataAdapter adpAcfrompjid = new SqlDataAdapter(cmdacFrompjid);
                            DataTable dtacfrompjid = new DataTable();
                            adpAcfrompjid.Fill(dtacfrompjid);
                            int accntidd = 0;
                            if (dtacfrompjid.Rows.Count > 0)
                            {
                                if (dtacfrompjid.Rows[0]["Account"].ToString() != "")
                                {
                                    accntidd = Convert.ToInt32(dtacfrompjid.Rows[0]["Account"]);
                                }
                            }


                            if (RadioButtonList1.SelectedIndex == 0)
                            {
                                a6 = "INSERT INTO dbo.Tranction_Details(AccountCredit,AmountCredit,Tranction_Master_Id" +
                                     " ,DateTimeOfTransaction,compid,whid)" +
                                     " VALUES('" + Convert.ToInt32(accntidd) + "','" + Convert.ToDecimal(txtNetAmount.Text) + "'" +
                                     " ,'" + Id1 + "','" + Convert.ToDateTime(txtDate.Text).ToShortDateString() + "','" + Session["comid"].ToString() + "','" + ddlWarehouse.SelectedValue + "')";
                            }
                            else
                            {
                                a6 = "INSERT INTO dbo.Tranction_Details(AccountCredit,AmountCredit,Tranction_Master_Id" +
                                    " ,DateTimeOfTransaction,compid,whid)" +
                                    " VALUES('" + ddlcashtype.SelectedValue + "','" + Convert.ToDecimal(txtNetAmount.Text) + "'" +
                                    " ,'" + Id1 + "','" + Convert.ToDateTime(txtDate.Text).ToShortDateString() + "','" + Session["comid"].ToString() + "','" + ddlWarehouse.SelectedValue + "')";

                            }

                            SqlCommand cd4 = new SqlCommand(a6, con);
                            cd4.CommandType = CommandType.Text;
                            cd4.Transaction = transaction;
                            cd4.ExecuteNonQuery();

                            if (txtincoperation.Text.Length > 0)
                            {
                                string acrt = "INSERT INTO dbo.Tranction_Details(AccountCredit,AmountCredit,Tranction_Master_Id" +
                                      " ,DateTimeOfTransaction,compid,whid)" +
                                      " VALUES('9200','" + Convert.ToDecimal(txtincoperation.Text) + "'" +
                                      " ,'" + Id1 + "','" + Convert.ToDateTime(txtDate.Text).ToShortDateString() + "','" + Session["comid"].ToString() + "','" + ddlWarehouse.SelectedValue + "')";

                                SqlCommand cdgt = new SqlCommand(acrt, con);
                                cdgt.CommandType = CommandType.Text;
                                cdgt.Transaction = transaction;
                                cdgt.ExecuteNonQuery();

                            }


                            if (rdl1.SelectedIndex == 0)
                            {
                                string a8 = "INSERT INTO dbo.Tranction_Details(AccountDebit,AmountDebit,Tranction_Master_Id" +
                                          " ,DateTimeOfTransaction,compid,whid)" +
                                          " VALUES('" + lbltaxper1.Text + "','" + Convert.ToDecimal(txtTax1.Text) + "'" +
                                          "  , '" + Id1 + "','" + Convert.ToDateTime(txtDate.Text).ToShortDateString() + "','" + Session["comid"].ToString() + "','" + ddlWarehouse.SelectedValue + "')";
                                SqlCommand cd6 = new SqlCommand(a8, con);
                                cd6.CommandType = CommandType.Text;
                                cd6.Transaction = transaction;
                                cd6.ExecuteNonQuery();


                                string a9 = "INSERT INTO dbo.Tranction_Details(AccountDebit,AmountDebit,Tranction_Master_Id" +
                                          " ,DateTimeOfTransaction,compid,whid )" +
                                          " VALUES('" + lbltaxper2.Text + "','" + Convert.ToDecimal(txtTax2.Text) + "'" +
                                          "  , '" + Id1 + "','" + System.DateTime.Now.ToShortDateString() + "','" + Session["comid"].ToString() + "','" + ddlWarehouse.SelectedValue + "')";
                                SqlCommand cd7 = new SqlCommand(a9, con);
                                cd7.CommandType = CommandType.Text;
                                cd7.Transaction = transaction;
                                cd7.ExecuteNonQuery();

                                string a9t = "INSERT INTO dbo.Tranction_Details(AccountDebit,AmountDebit,Tranction_Master_Id" +
                                         " ,DateTimeOfTransaction,compid,whid )" +
                                         " VALUES('" + lbltaxper3.Text + "','" + Convert.ToDecimal(txtTax3.Text) + "'" +
                                         "  , '" + Id1 + "','" + Convert.ToDateTime(txtDate.Text).ToShortDateString() + "','" + Session["comid"].ToString() + "','" + ddlWarehouse.SelectedValue + "')";
                                SqlCommand cd7t = new SqlCommand(a9t, con);
                                cd7t.CommandType = CommandType.Text;
                                cd7t.Transaction = transaction;
                                cd7t.ExecuteNonQuery();
                            }

                            string a10 = "INSERT INTO dbo.Tranction_Details(AccountDebit,AmountDebit,Tranction_Master_Id" +
                                        " ,DateTimeOfTransaction,compid,whid )" +
                                        " VALUES('8004','" + Convert.ToDecimal(txtShippCharge.Text) + "'" +
                                        "  , '" + Id1 + "','" + Convert.ToDateTime(txtDate.Text).ToShortDateString() + "','" + Session["comid"].ToString() + "','" + ddlWarehouse.SelectedValue + "')";
                            SqlCommand cd8 = new SqlCommand(a10, con);
                            cd8.CommandType = CommandType.Text;
                            cd8.Transaction = transaction;
                            cd8.ExecuteNonQuery();


                            foreach (GridViewRow dr in grdinvoice.Rows)
                            {

                                TextBox txtamount = (TextBox)dr.FindControl("txtamount");
                                if (txtamount.Text.Length > 0)
                                {
                                    DropDownList ddlaccgroup = (DropDownList)dr.FindControl("ddlaccgroup");
                                    TextBox txtgt1 = (TextBox)dr.FindControl("txtgt1");
                                    TextBox txtgt2 = (TextBox)dr.FindControl("txtgt2");
                                    TextBox txtgt3 = (TextBox)dr.FindControl("txtgt3");

                                    TextBox txtmemo = (TextBox)dr.FindControl("txtmemo");
                                    decimal divtxttt1;
                                    decimal divtxttt2;
                                    decimal divtxttt3;

                                    if (CheckBox2.SelectedIndex == 1)
                                    {
                                        divtxttt1 = ((Convert.ToDecimal(txtamount.Text) * Convert.ToDecimal(txtTax1.Text)) / Convert.ToDecimal(txtValurRecd.Text));
                                        divtxttt2 = ((Convert.ToDecimal(txtamount.Text) * Convert.ToDecimal(txtTax2.Text)) / Convert.ToDecimal(txtValurRecd.Text));
                                        divtxttt3 = ((Convert.ToDecimal(txtamount.Text) * Convert.ToDecimal(txtTax3.Text)) / Convert.ToDecimal(txtValurRecd.Text));

                                        divtxttt1 = Math.Round(divtxttt1, 2);
                                        divtxttt2 = Math.Round(divtxttt2, 2);
                                        divtxttt3 = Math.Round(divtxttt3, 2);
                                    }
                                    else
                                    {
                                        if (Convert.ToString(txtgt1.Text) != "")
                                        {
                                            divtxttt1 = Convert.ToDecimal(txtgt1.Text);
                                            divtxttt1 = Math.Round(divtxttt1, 2);
                                        }
                                        else
                                        {
                                            divtxttt1 = 0;
                                        }
                                        if (Convert.ToString(txtgt2.Text) != "")
                                        {
                                            divtxttt2 = Convert.ToDecimal(txtgt2.Text);
                                            divtxttt2 = Math.Round(divtxttt2, 2);
                                        }
                                        else
                                        {
                                            divtxttt2 = 0;
                                        }
                                        if (Convert.ToString(txtgt3.Text) != "")
                                        {
                                            divtxttt3 = Convert.ToDecimal(txtgt3.Text);
                                            divtxttt3 = Math.Round(divtxttt3, 2);
                                        }
                                        else
                                        {
                                            divtxttt3 = 0;
                                        }

                                    }

                                    decimal totalInvamt = 0;
                                    if (rdl1.SelectedIndex == 0)
                                    {
                                        totalInvamt = Convert.ToDecimal(txtamount.Text);

                                    }
                                    else
                                    {
                                        totalInvamt = Convert.ToDecimal(txtamount.Text) + divtxttt1 + divtxttt2 + divtxttt3;

                                    }

                                    string a7 = "INSERT INTO dbo.Tranction_Details(AccountDebit,AmountDebit,Tranction_Master_Id" +
                                                        " ,DateTimeOfTransaction,compid,whid,Memo)" +
                                                        " VALUES('" + ddlaccgroup.SelectedValue + "','" + totalInvamt + "'" +
                                                        " , '" + Id1 + "','" + Convert.ToDateTime(txtDate.Text).ToShortDateString() + "','" + Session["comid"].ToString() + "','" + ddlWarehouse.SelectedValue + "','" + txtmemo.Text + "')";

                                    SqlCommand cd5 = new SqlCommand(a7, con);
                                    cd5.CommandType = CommandType.Text;
                                    cd5.Transaction = transaction;
                                    cd5.ExecuteNonQuery();
                                }


                            }

                            transaction.Commit();
                            DataTable dtapprequirment = ClsAccountAppr.Apprreuqired();
                            if (dtapprequirment.Rows.Count > 0)
                            {

                                ClsAccountAppr.AccountAppMaster(ddlWarehouse.SelectedValue, Id1.ToString(), chkappentry.Checked);
                            }
                            filletype();

                            ClearAll();
                            Label8.Visible = true;
                            Label8.Text = "Record inserted successfully";
                            if (Request.QueryString["docid"] != null)
                            {
                                inserdocatt();
                            }
                            else
                            {
                                if (chkdoc.Checked == true)
                                {
                                    entry();
                                    //  ModalPopupExtender1.Show();
                                    //filldoc();
                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            Label8.Visible = true;
                            Label8.Text = "Error :" + ex.Message;
                        }
                        finally
                        {
                            con.Close();
                        }


                    }
                    else
                    {
                        Label8.Visible = true;
                        Label8.Text = "Sorry, You are not permitted for greater record to Priceplan";
                    }
                }
                else
                {
                    Label8.Visible = true;
                    Label8.Text = "Please check your date. You cannot select any date earlier/later than start/end date of the year";
                }

            }
        }
        else
        {
            Label8.Visible = true;
            Label8.Text = "You are not permited to insert record for this page.";

        }
    }
    public void inserdocatt()
    {

        string sqlselect = "select * from DocumentMaster where DocumentId='" + Request.QueryString["docid"] + "'";
        SqlDataAdapter adpt = new SqlDataAdapter(sqlselect, con);
        DataTable dtpt = new DataTable();
        adpt.Fill(dtpt);
        if (dtpt.Rows.Count > 0)
        {

            SqlCommand cmdi = new SqlCommand("InsertAttachmentMaster", con);

            cmdi.CommandType = CommandType.StoredProcedure;
            cmdi.Parameters.Add(new SqlParameter("@Titlename", SqlDbType.NVarChar));
            cmdi.Parameters["@Titlename"].Value = dtpt.Rows[0]["DocumentTitle"].ToString();
            cmdi.Parameters.Add(new SqlParameter("@Filename", SqlDbType.NVarChar));
            cmdi.Parameters["@Filename"].Value = dtpt.Rows[0]["DocumentName"].ToString();

            cmdi.Parameters.Add(new SqlParameter("@Datetime", SqlDbType.DateTime));
            cmdi.Parameters["@Datetime"].Value = dtpt.Rows[0]["DocumentUploadDate"].ToString(); ;
            cmdi.Parameters.Add(new SqlParameter("@RelatedtablemasterId", SqlDbType.NVarChar));
            cmdi.Parameters["@RelatedtablemasterId"].Value = "5";
            cmdi.Parameters.Add(new SqlParameter("@RelatedTableId", SqlDbType.NVarChar));
            cmdi.Parameters["@RelatedTableId"].Value = ViewState["tid"].ToString();
            cmdi.Parameters.Add(new SqlParameter("@IfilecabinetDocId", SqlDbType.NVarChar));
            cmdi.Parameters["@IfilecabinetDocId"].Value = dtpt.Rows[0]["DocumentId"].ToString();
            cmdi.Parameters.Add(new SqlParameter("@Attachment", SqlDbType.Int));
            cmdi.Parameters["@Attachment"].Direction = ParameterDirection.Output;

            cmdi.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
            cmdi.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            Int32 result = cmdi.ExecuteNonQuery();
            result = Convert.ToInt32(cmdi.Parameters["@Attachment"].Value);
            con.Close();
        }
    }



   
   
    protected void ddlWarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearAll();
        filletype();
        filltrans();
        fillpartyddl();
      
        chkappentry.Visible = false;
        DataTable appri = ClsAccountAppr.Apprreuqired();
        if (appri.Rows.Count > 0)
        {
            ViewState["AccRS"] = "ACC";
            int kn = ClsAccountAppr.Allowchkappr(ddlWarehouse.SelectedValue);
            if (kn == 1)
            {
                chkappentry.Visible = true;
            }
        }
        else
        {
            ViewState["AccRS"] = "";
        }
        btSubmit.Enabled = true;
        Label8.Text = "";
        DataTable dtpaacc = select("Select * from AccountPageRightAccess where cid='" + Session["Comid"] + "' and Access='1' and AccType='0'");
        if (dtpaacc.Rows.Count > 0)
        {
            btSubmit.Enabled = false;

            DataTable dtrc = select(" select Accountingpagerightwithdesignation.*  from Accountingpagerightwithdesignation inner join DesignationMaster on Accountingpagerightwithdesignation.DesignationId=DesignationMaster.DesignationMasterId " +
           " inner join DepartmentmasterMNC on DepartmentmasterMNC.Id=DesignationMaster.DeptId where DesignationId='" + Session["DesignationId"] + "' and  DepartmentmasterMNC.whid='" + ddlWarehouse.SelectedValue + "'");
            if (dtrc.Rows.Count > 0)
            {
                if (Convert.ToInt32(dtrc.Rows[0]["AccessRight"]) == 0)
                {

                    Label8.Text = "Sorry,you are not access right for this business";
                }
                else if (Convert.ToInt32(dtrc.Rows[0]["AccessRight"]) == 2)
                {
                    if (Convert.ToInt32(dtrc.Rows[0]["Insert_Right"]) == 1)
                    {
                        btSubmit.Enabled = true;
                    }
                }
                else
                {
                    btSubmit.Enabled = true;

                }
            }
            else
            {
                Label8.Text = "Sorry,you are not access right for this business";

            }
        }
        if (grdinvoice.Rows.Count > 0)
        {
            gridtax();
        }
    }
    protected void ClearAll()
    {
        rdl1.Enabled = true;
        pnlitem.Visible = false;
        pnlmain.Visible = false;
        rdl1.SelectedIndex = -1;
        CheckBox2.SelectedIndex = -1;
        
        txtInvNo.Text = "0";
      
        txtNetAmount.Text = "0";
       
        txtShippCharge.Text = "0";
      
        txtTax1.Text = "0";
        txtTax2.Text = "0";
        txtTax3.Text = "0";
        txtTrackingNo.Text = "0";
       
        txtValurRecd.Text = "0";
        txtincoperation.Text = "0";
        lblmmssgg.Text = "";
        txtdesc.Text = "";
       
        if (ddlTransporter.SelectedIndex >= 0)
        {
            ddlTransporter.SelectedIndex = 0;
        }
        
        //ddlWarehouse.SelectedIndex = 0;
        btSubmit.Visible = false;
       
        ViewState["dt"] = null;
    }
   

    protected void RadioButtonList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedIndex == 1)
        {
                string cashtype = "select AccountName,AccountId from AccountMaster where GroupId=1 and ClassId=1 and compid='" + Session["comid"] + "' and Whid='" + ddlWarehouse.SelectedValue + "' order by AccountName";
            SqlDataAdapter d1 = new SqlDataAdapter(cashtype, con);
            DataSet d12 = new DataSet();
            d1.Fill(d12);
            ddlcashtype.DataSource = d12;
            ddlcashtype.DataTextField = "AccountName";
            ddlcashtype.DataValueField = "AccountId";
            ddlcashtype.Items.Insert(0, "-Select-");
            ddlcashtype.SelectedItem.Value = "0";
            ddlcashtype.DataBind();
            pnlcash.Visible = true;
          
        }
        else
        {

          
            pnlcash.Visible = false;
        
        }
    }
   

    public void entry()
    {
        String te = "AccEntryDocUp.aspx?Tid=" + ViewState["tid"];


        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void fillpartyddl()
    {
        //string s12 = " SELECT     Party_master.PartyID, Party_master.Account, Party_master.Contactperson+':'+Party_master.Compname as Compname, Party_master.PartyTypeId, User_master.Active " +
        string s12 = " SELECT     Party_master.PartyID, Party_master.Account,Party_master.Compname+':'+Party_master.Contactperson  as Compname, Party_master.PartyTypeId, User_master.Active " +
     " FROM     [PartytTypeMaster] inner join    Party_master on Party_master.PartyTypeId= [PartytTypeMaster].[PartyTypeId] INNER JOIN " +
     "                  User_master ON Party_master.PartyID = User_master.PartyID " +
    " WHERE     (User_master.Active = 1) and [PartytTypeMaster].compid='" + Session["comid"] + "' and Party_master.Whid='" + ddlWarehouse.SelectedValue + "' and [PartType]='Vendor' order by Compname "; // + //" Where PartytypeId = 1 and Compname <> 'yyyyy'";
        SqlCommand cm13 = new SqlCommand(s12, con);
        cm13.CommandType = CommandType.Text;
        SqlDataAdapter da13 = new SqlDataAdapter(cm13);
        DataSet ds13 = new DataSet();
        da13.Fill(ds13);


        ddlpartyName.DataSource = ds13;
        ddlpartyName.DataTextField = "Compname";
        ddlpartyName.DataValueField = "PartyID";

        ddlpartyName.DataBind();
        ddlpartyName.Items.Insert(0, "-Select-");

        ddlpartyName.Items[0].Value = "0";




    }

    protected void filltrans()
    {
        ddlTransporter.Items.Clear();
        //string s12 = " SELECT     Party_master.PartyID, Party_master.Account, Party_master.Contactperson+':'+Party_master.Compname as Compname, Party_master.PartyTypeId, User_master.Active " +
        string s12 = " SELECT     Party_master.PartyID, Party_master.Account,Party_master.Compname+':'+Party_master.Contactperson  as Compname, Party_master.PartyTypeId, User_master.Active " +
     " FROM     [PartytTypeMaster] inner join    Party_master on Party_master.PartyTypeId= [PartytTypeMaster].[PartyTypeId] INNER JOIN " +
     "                  User_master ON Party_master.PartyID = User_master.PartyID " +
    " WHERE     (User_master.Active = 1) and [PartytTypeMaster].compid='" + Session["comid"] + "' and Party_master.Whid='" + ddlWarehouse.SelectedValue + "' and [PartType]='Vendor' order by Compname "; // + //" Where PartytypeId = 1 and Compname <> 'yyyyy'";
        SqlCommand cm13 = new SqlCommand(s12, con);
        cm13.CommandType = CommandType.Text;
        SqlDataAdapter da13 = new SqlDataAdapter(cm13);
        DataTable ds13 = new DataTable();
        da13.Fill(ds13);
        if (ds13.Rows.Count > 0)
        {



            ddlTransporter.DataSource = ds13;
            ddlTransporter.DataTextField = "Compname";
            ddlTransporter.DataValueField = "PartyID";

            ddlTransporter.DataBind();
        }
        ddlTransporter.Items.Insert(0, "Select");
        ddlTransporter.Items[0].Value = "0";

    }

    protected void CheckBox2_SelectedIndexChanged(object sender, EventArgs e)
    {
        grdinvoice.Columns[2].Visible = false;
        grdinvoice.Columns[3].Visible = false;
        grdinvoice.Columns[4].Visible = false;
        gridtax();
        if (CheckBox2.SelectedIndex == 0)
        {
            
            txtTax1.Enabled = false;
            txtTax2.Enabled = false;
            txtTax3.Enabled = false;
          
            //if (Panel1.Visible == true)
            //{
          
            // }
            if (lbltxtop.Text == "0")
            {
               
                ModalPopupExtender12222.Show();
            }
            else
            {
               
            }
        }
        else
        {

            txtTax1.Enabled = true;
            txtTax2.Enabled = true;
            txtTax3.Enabled = true;
          
        }
        pnlmain.Visible = true;
        pnlconf.Enabled = true;
        btCal.Visible = true;
    }

   

  
    protected DataTable select(string str)
    {
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);

        DataTable dt = new DataTable();
        adp.Fill(dt);
        return dt;


    }
    public void gridtax()
    {
        pnltax1.Visible = false;
        pnltax2.Visible = false;
        pnltax3.Visible = false;
        txtTax1.Enabled = true;
        txtTax2.Enabled = true;
        txtTax3.Enabled = true;
        grdinvoice.Columns[2].Visible = false;
        grdinvoice.Columns[3].Visible = false;
        grdinvoice.Columns[4].Visible = false;
        lbltaxper1.Text = "";
        lbltaxper2.Text = "";
        lbltaxper3.Text = "";
        lbltxtop.Text = "0";
        txttinfo1.Text = "";
        txttinfo2.Text = "";
        txttinfo3.Text = "";
        DataTable dttxt = select("select * from StorTaxmethodtbl where Storeid='" + ddlWarehouse.SelectedValue + "'");


        if (dttxt.Rows.Count > 0)
        {
             if (dttxt.Rows[0]["Fixedtaxdependingonstate"].ToString() == "True")
            {
                DataTable dtstate = select("Select State,Country,StateName,CountryName from CountryMaster inner join CompanyWebsiteAddressMaster on CompanyWebsiteAddressMaster.Country=CountryMaster.CountryId inner join StateMasterTbl on StateMasterTbl.StateId=CompanyWebsiteAddressMaster.State  inner join CompanyWebsitMaster on CompanyWebsitMaster.CompanyWebsiteMasterId=CompanyWebsiteAddressMaster.CompanyWebsiteMasterId  inner join WarehouseMaster on WarehouseMaster.WarehouseId=CompanyWebsitMaster.Whid where CompanyWebsitMaster.Whid='" + ddlWarehouse.SelectedValue + "'");
                if (dtstate.Rows.Count > 0)
                {
                    //Convert.ToString(dtstate.Rows[0]["Country"]);

                    DataTable dtwhid = select("select Top(3) Taxshortname,PurchaseTaxAccountMasterID,TaxName from [TaxTypeMasterMoreInfo]  where Active='1' and [Whid]='" + ddlWarehouse.SelectedValue + "' and ([TaxTypeMasterMoreInfo].[StateId]='" + Convert.ToString(dtstate.Rows[0]["State"]) + "' or [TaxTypeMasterMoreInfo].[StateId]='0')   order by TaxTypeMasterMoreInfo.Id Desc");

                    if (dtwhid.Rows.Count > 0)
                    {

                        lbltxtop.Text = "2";
                        if (dtwhid.Rows.Count == 3)
                        {
                            ViewState["Taxallo"] = "3";
                           
                            txttinfo1.Text = Convert.ToString(dtwhid.Rows[0]["Taxshortname"]) + " = (" + Convert.ToString(dtwhid.Rows[0]["TaxName"]) + ")";
                            txttinfo2.Text = Convert.ToString(dtwhid.Rows[1]["Taxshortname"]) + " = (" + Convert.ToString(dtwhid.Rows[1]["TaxName"]) + ")";
                            txttinfo3.Text = Convert.ToString(dtwhid.Rows[2]["Taxshortname"]) + " = (" + Convert.ToString(dtwhid.Rows[2]["TaxName"]) + ")";
                            Label23.Text = Convert.ToString(dtwhid.Rows[0]["Taxshortname"]) + " Tax";
                            Label24.Text = Convert.ToString(dtwhid.Rows[1]["Taxshortname"]) + " Tax";
                            Label60.Text = Convert.ToString(dtwhid.Rows[2]["Taxshortname"]) + " Tax";
                            lbltaxper1.Text = Convert.ToString(dtwhid.Rows[0]["PurchaseTaxAccountMasterID"]);
                            lbltaxper2.Text = Convert.ToString(dtwhid.Rows[1]["PurchaseTaxAccountMasterID"]);
                            lbltaxper3.Text = Convert.ToString(dtwhid.Rows[2]["PurchaseTaxAccountMasterID"]);
                            pnltax1.Visible = true;
                            pnltax2.Visible = true;
                            pnltax3.Visible = true;
                           
                        }
                        else if (dtwhid.Rows.Count == 2)
                        {
                            ViewState["Taxallo"] = "2";
                           
                            txttinfo1.Text = Convert.ToString(dtwhid.Rows[0]["Taxshortname"]) + " = (" + Convert.ToString(dtwhid.Rows[0]["TaxName"]) + ")";
                            txttinfo2.Text = Convert.ToString(dtwhid.Rows[1]["Taxshortname"]) + " = (" + Convert.ToString(dtwhid.Rows[1]["TaxName"]) + ")";
                            
                            Label23.Text = Convert.ToString(dtwhid.Rows[0]["Taxshortname"]) + " Tax";
                            Label24.Text = Convert.ToString(dtwhid.Rows[1]["Taxshortname"]) + " Tax";
                            lbltaxper1.Text = Convert.ToString(dtwhid.Rows[0]["PurchaseTaxAccountMasterID"]);
                            lbltaxper2.Text = Convert.ToString(dtwhid.Rows[1]["PurchaseTaxAccountMasterID"]);
                            pnltax1.Visible = true;
                            pnltax2.Visible = true;
                           
                        }
                        else if (dtwhid.Rows.Count == 1)
                        {
                            ViewState["Taxallo"] = "1";
                            
                            txttinfo1.Text = Convert.ToString(dtwhid.Rows[0]["Taxshortname"]) + " = (" + Convert.ToString(dtwhid.Rows[0]["TaxName"]) + ")";
                            Label23.Text = Convert.ToString(dtwhid.Rows[0]["Taxshortname"]) + " Tax";
                            lbltaxper1.Text = Convert.ToString(dtwhid.Rows[0]["PurchaseTaxAccountMasterID"]);
                            pnltax1.Visible = true;
                           
                        }

                    }
                    else
                    {
                        ViewState["Taxallo"] = "0";
                    }
                }
            }
            else if (dttxt.Rows[0]["Fixedtaxforall"].ToString() == "True")
            {

                DataTable dtwhid = select("select Top(3) Taxshortname,Name,PurchaseTaxAccountMasterID from [TaxTypeMaster]  where Active='1' and [Whid]='" + ddlWarehouse.SelectedValue + "'   order by TaxTypeMaster.TaxTypeMasterId Desc");

                if (dtwhid.Rows.Count > 0)
                {
                    lbltxtop.Text = "1";
                    if (dtwhid.Rows.Count == 3)
                    {
                       
                        ViewState["Taxallo"] = "3";
                        
                        txttinfo1.Text = Convert.ToString(dtwhid.Rows[0]["Taxshortname"]) + " = (" + Convert.ToString(dtwhid.Rows[0]["Name"]) + ")";
                        txttinfo2.Text = Convert.ToString(dtwhid.Rows[1]["Taxshortname"]) + " = (" + Convert.ToString(dtwhid.Rows[1]["Name"]) + ")";
                        txttinfo3.Text = Convert.ToString(dtwhid.Rows[2]["Taxshortname"]) + " = (" + Convert.ToString(dtwhid.Rows[2]["Name"]) + ")";
                        Label23.Text = Convert.ToString(dtwhid.Rows[0]["Taxshortname"]) + " Tax";
                        Label24.Text = Convert.ToString(dtwhid.Rows[1]["Taxshortname"]) + " Tax";

                        Label60.Text = Convert.ToString(dtwhid.Rows[2]["Taxshortname"]) + " Tax";
                        lbltaxper1.Text = Convert.ToString(dtwhid.Rows[0]["PurchaseTaxAccountMasterID"]);
                        lbltaxper2.Text = Convert.ToString(dtwhid.Rows[1]["PurchaseTaxAccountMasterID"]);
                        lbltaxper3.Text = Convert.ToString(dtwhid.Rows[2]["PurchaseTaxAccountMasterID"]);
                        pnltax1.Visible = true;
                        pnltax2.Visible = true;
                        pnltax3.Visible = true;
                      
                    }
                    else if (dtwhid.Rows.Count == 2)
                    {
                        ViewState["Taxallo"] = "2";
                       
                        txttinfo1.Text = Convert.ToString(dtwhid.Rows[0]["Taxshortname"]) + " = (" + Convert.ToString(dtwhid.Rows[0]["Name"]) + ")";
                        txttinfo2.Text = Convert.ToString(dtwhid.Rows[1]["Taxshortname"]) + " = (" + Convert.ToString(dtwhid.Rows[1]["Name"]) + ")";
                        Label23.Text = Convert.ToString(dtwhid.Rows[0]["Taxshortname"]) + " Tax";
                        Label24.Text = Convert.ToString(dtwhid.Rows[1]["Taxshortname"]) + " Tax";

                        lbltaxper1.Text = Convert.ToString(dtwhid.Rows[0]["PurchaseTaxAccountMasterID"]);
                        lbltaxper2.Text = Convert.ToString(dtwhid.Rows[1]["PurchaseTaxAccountMasterID"]);
                        pnltax1.Visible = true;
                        pnltax2.Visible = true;
                     
                    }
                    else if (dtwhid.Rows.Count == 1)
                    {
                        ViewState["Taxallo"] = "1";
                        
                        txttinfo1.Text = Convert.ToString(dtwhid.Rows[0]["Taxshortname"]) + " = (" + Convert.ToString(dtwhid.Rows[0]["Name"]) + ")";
                        Label23.Text = Convert.ToString(dtwhid.Rows[0]["Taxshortname"]) + " Tax";
                        lbltaxper1.Text = Convert.ToString(dtwhid.Rows[0]["PurchaseTaxAccountMasterID"]);
                        pnltax1.Visible = true;
                       
                    }

                }
                else
                {
                    ViewState["Taxallo"] = "0";
                }
            }
        }

      
     Label lblheadtax1=(Label)grdinvoice.HeaderRow.FindControl("lblheadtax1");
     Label lblheadtax2 = (Label)grdinvoice.HeaderRow.FindControl("lblheadtax2");
     Label lblheadtax3 = (Label)grdinvoice.HeaderRow.FindControl("lblheadtax3");
        if (CheckBox2.SelectedIndex == 0)
        {
            if (Convert.ToString(ViewState["Taxallo"]) == "3")
            {
                lblheadtax1.Text = Label23.Text;
                lblheadtax2.Text = Label24.Text;
                lblheadtax3.Text = Label60.Text;
                grdinvoice.Columns[2].Visible = true;
                grdinvoice.Columns[3].Visible = true;
                grdinvoice.Columns[4].Visible = true;
            }
            else if (Convert.ToString(ViewState["Taxallo"]) == "2")
            {
                lblheadtax1.Text = Label23.Text;
                lblheadtax2.Text = Label24.Text;
                grdinvoice.Columns[2].Visible = true;
                grdinvoice.Columns[3].Visible = true;
            }
            else if (Convert.ToString(ViewState["Taxallo"]) == "1")
            {
                lblheadtax1.Text = Label23.Text;

                grdinvoice.Columns[2].Visible = true;

            }
        }

    }

    protected void ddlexasset_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((DropDownList)sender).Parent.Parent as GridViewRow;
        int rinrow = row.RowIndex;

        DropDownList ddlaccgroup = (DropDownList)grdinvoice.Rows[rinrow].FindControl("ddlaccgroup");
        DropDownList ddlexasset = (DropDownList)grdinvoice.Rows[rinrow].FindControl("ddlexasset");

        

        ddlaccgroup.Items.Clear();

        DataTable dt = select("SELECT (AccountMaster.AccountName) +':'+(GroupCompanyMaster.groupdisplayname) as Classgroup,AccountMaster.AccountId  FROM ClassTypeCompanyMaster inner join  ClassCompanyMaster on ClassCompanyMaster.classtypecompanymasterid=ClassTypeCompanyMaster.Id  inner join GroupCompanyMaster on GroupCompanyMaster.classcompanymasterid=ClassCompanyMaster.Id  inner join AccountMaster  on AccountMaster.GroupId=GroupCompanyMaster.GroupId  where ClassTypeCompanyMaster.classtypeid='" + ddlexasset.SelectedValue + "' and AccountMaster.Whid='" + ddlWarehouse.SelectedValue + "' and GroupCompanyMaster.Whid='" + ddlWarehouse.SelectedValue + "' order by Classgroup");
       
            ddlaccgroup.DataSource = dt;
            ddlaccgroup.DataTextField = "Classgroup";
            ddlaccgroup.DataValueField = "AccountId";
            ddlaccgroup.DataBind();
       
    }
    protected void fillaccount()
    {
            
        foreach (GridViewRow item in grdinvoice.Rows)
        {
            DropDownList ddlaccgroup = (DropDownList)item.FindControl("ddlaccgroup");
            DropDownList ddlexasset = (DropDownList)item.FindControl("ddlexasset");
            DataTable dt = select("SELECT (AccountMaster.AccountName) +':'+(GroupCompanyMaster.groupdisplayname) as Classgroup,AccountMaster.AccountId  FROM ClassTypeCompanyMaster inner join  ClassCompanyMaster on ClassCompanyMaster.classtypecompanymasterid=ClassTypeCompanyMaster.Id  inner join GroupCompanyMaster on GroupCompanyMaster.classcompanymasterid=ClassCompanyMaster.Id  inner join AccountMaster  on AccountMaster.GroupId=GroupCompanyMaster.GroupId  where ClassTypeCompanyMaster.classtypeid ='" + ddlexasset.SelectedValue+ "' and AccountMaster.Whid='" + ddlWarehouse.SelectedValue + "' and GroupCompanyMaster.Whid='" + ddlWarehouse.SelectedValue + "' order by Classgroup");
      
             ddlaccgroup.Items.Clear();
             grdinvoice.Visible = true;
                  
                ddlaccgroup.DataSource = dt;
                ddlaccgroup.DataTextField = "Classgroup";
                ddlaccgroup.DataValueField = "AccountId";
                ddlaccgroup.DataBind();
            
        }
    }
    protected void btnyes_Click(object sender, EventArgs e)
    {
        String te = "StoreTaxmethodtbl.aspx?Wid=" + ddlWarehouse.SelectedValue;


        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void imgadddivision_Click(object sender, ImageClickEventArgs e)
    {
        string te = "VendorPartyRegister.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void LinkButton3_Click(object sender, ImageClickEventArgs e)
    {

        fillpartyddl();

    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        filltrans();
    }
  




    protected void lnkmore_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1.Show();
    }
    protected void btnnewinv_Click(object sender, EventArgs e)
    {
        ClearAll();
    }
    protected void btnk_Click(object sender, EventArgs e)
    {

        rdl1.ToolTip = "If you wish to make changes in the selection of this option, you must refresh this page from the top right corner. Please note, all information that you added will be deleted. You will have to re-enter this information again by hitting the create new invoice button. ";
        rdl1.Enabled = false;
        if (rdl1.SelectedIndex == 0)
        {
            pnlitem.Visible = true;
            pnlmain.Visible = false;
        }
        else
        {
            
            pnlmain.Visible = true;
            pnlconf.Enabled = true;
            btCal.Visible = true;
            gridtax();
        }
        ModalPopupExtender2.Hide();
    }
    protected void btnccc_Click(object sender, EventArgs e)
    {
        rdl1.SelectedIndex = -1;
    }
    protected void rdl1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlitemnoinv_SelectedIndexChanged(sender, e);
        grdinvoice.Columns[2].Visible = false;
        grdinvoice.Columns[3].Visible = false;
        grdinvoice.Columns[4].Visible = false;
        ModalPopupExtender2.Show();
    }

    protected void chksinfo_CheckedChanged(object sender, EventArgs e)
    {
        if (chksinfo.Checked == true)
        {
            pnlshipin.Visible = true;
        }
        else
        {
            pnlshipin.Visible = false;
        }
    }
    public DataTable CreateDatatable()
    {
        DataTable dtTemp = new DataTable();
        DataColumn prd1 = new DataColumn();
        prd1.ColumnName = "Id";
        prd1.DataType = System.Type.GetType("System.String");
        prd1.AllowDBNull = true;
        dtTemp.Columns.Add(prd1);



        return dtTemp;
    }
    protected void ddlitemnoinv_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtTemp = new DataTable();
        dtTemp = CreateDatatable();


        for (int i = 1; i <=Convert.ToInt32(ddlitemnoinv.SelectedItem.Text); i++)
        {
            DataRow dtadd = dtTemp.NewRow();
            dtadd["Id"] = i.ToString();
            dtTemp.Rows.Add(dtadd);
        }
      
        grdinvoice.DataSource = dtTemp;
        grdinvoice.DataBind();
        fillaccount();
    }

}
