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

public partial class ShoppingCart_Admin_SalesOrderAll : System.Web.UI.Page
{
 //   SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString"].ConnectionString);
 //   SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString"].ConnectionString);

    SqlConnection con;
    SqlConnection connection;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        }
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        connection = pgcon.dynconn;

        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();


        Page.Title = pg.getPageTitle(page);

        //Session["pnl1"] = "8";
       // Session["pagename"] = "SalesOrderAll.aspx";
        if (!IsPostBack)
        {
            Pagecontrol.dypcontrol(Page, page);

            lblCompany.Text = Session["Cname"].ToString();
         //   ViewState["ret"] = Request.UrlReferrer.ToString();
            fillwarehouse();
            if (Request.QueryString["SaleOrderNo"] != null)
            {
                Session["OrId"] = Convert.ToInt32(Request.QueryString["SaleOrderNo"]);
              
                getsalesorder();
                panel2.Visible = true;
                panel1.Visible = false;
            }
            else
            {
                txtfromdate.Text = System.DateTime.Now.Month.ToString() + "/01/" + System.DateTime.Now.Year.ToString();
                txtTodate.Text = System.DateTime.Now.ToShortDateString();
                bindGrid();
                panel2.Visible = false;
            }

        }
    }
    protected void fillwarehouse()
    {
        string str1 = "select * from WarehouseMaster where comid='" + Session["comid"].ToString() + "' order by Name";

        DataTable ds1 = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(str1, con);
        da.Fill(ds1);
        if (ds1.Rows.Count > 0)
        {
            ddlwarehouse.DataSource = ds1;
            ddlwarehouse.DataTextField = "Name";
            ddlwarehouse.DataValueField = "WarehouseId";
            ddlwarehouse.DataBind();
            ddlwarehouse.Items.Insert(0, "All");
            ddlwarehouse.Items[0].Value = "0";
        }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        //Calendar1.Visible = true;
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        //Calendar2.Visible = true;
    }
    //protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    //{
    //    txtfromdate.Text = Calendar1.SelectedDate.ToShortDateString();
    //    Calendar1.Visible = false;
    //}
    //protected void Calendar2_SelectionChanged(object sender, EventArgs e)
    //{
    //    txtTodate.Text = Calendar2.SelectedDate.ToShortDateString();
    //    Calendar2.Visible = false;
    //}
    protected void Button1_Click(object sender, EventArgs e)
    {
        //bindGrid();
    }
    public void bindGrid()
    {
        string str = "";
        if (ddlwarehouse.SelectedIndex == 0)
        {
            str = "SELECT     SalesOrderMaster.PartyId, SalesOrderMaster.SalesOrderDate, SalesOrderMaster.ShippingCharges, SalesOrderMaster.HandlingCharges,  " +
                          " SalesOrderMasterDetail.PartyDiscountAmount, SalesOrderMasterDetail.OrderValueDiscountAmount, SalesOrderMaster.GrossAmount,  " +
                          " SalesOrderMaster.SalesOrderId,case when (Party_master.Compname is null) then '--' else   Party_master.Compname end as Compname " +
                        " FROM SalesOrderMaster INNER JOIN " +
                          " SalesOrderMasterDetail ON SalesOrderMaster.SalesOrderId = SalesOrderMasterDetail.SalesOrderId LEFT OUTER JOIN " +
                          " Party_master ON SalesOrderMaster.PartyId = Party_master.PartyID " +
                        " where (SalesOrderMaster.SalesOrderDate between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "')  and (Party_master.id='" + Session["comid"].ToString()+ "')" +
                        " ORDER BY SalesOrderMaster.SalesOrderId Desc";
        }
        else
        {
            str = "SELECT     SalesOrderMaster.PartyId, SalesOrderMaster.SalesOrderDate, SalesOrderMaster.ShippingCharges, SalesOrderMaster.HandlingCharges,  " +
                          " SalesOrderMasterDetail.PartyDiscountAmount, SalesOrderMasterDetail.OrderValueDiscountAmount, SalesOrderMaster.GrossAmount,  " +
                          " SalesOrderMaster.SalesOrderId,case when (Party_master.Compname is null) then '--' else   Party_master.Compname end as Compname " +
                        " FROM SalesOrderMaster INNER JOIN " +
                          " SalesOrderMasterDetail ON SalesOrderMaster.SalesOrderId = SalesOrderMasterDetail.SalesOrderId LEFT OUTER JOIN " +
                          " Party_master ON SalesOrderMaster.PartyId = Party_master.PartyID " +
                        " where (SalesOrderMaster.SalesOrderDate between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "') and (Party_master.Whid='"+ddlwarehouse.SelectedValue+"')" +
                        " ORDER BY SalesOrderMaster.SalesOrderId Desc";
        }
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);

        GridView1.DataSource = ds;
        GridView1.DataBind();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "detail")
        {
            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            //getPruduct(Convert.ToInt32(GridView1.SelectedDataKey.Value));
            //GetOrder(Convert.ToInt32(GridView1.SelectedDataKey.Value));
            //getShippingAddress(Convert.ToInt32(GridView1.SelectedDataKey.Value));
            Session["OrId"] = Convert.ToInt32(GridView1.SelectedDataKey.Value);
            getsalesorder();
            panel2.Visible = true;
            panel1.Visible = false;

        }

    }
    //public void getPruduct(int id)
    //{

    //        string str = "SELECT     InventoryMaster.Name, SalesOrderDetail.Qty, SalesOrderDetail.Rate, SalesOrderDetail.Amount, SalesOrderMaster.SalesOrderId, " +
    //                      " SalesOrderDetailDetail.InventoryVolumeDiscountAmount, SalesOrderDetailDetail.PromotionDiscountAmount, " +
    //                      " SalesOrderDetailDetail.PackHandlingAmount " +
    //                    " FROM  SalesOrderMaster INNER JOIN " +
    //                      " SalesOrderDetail ON SalesOrderMaster.SalesOrderId = SalesOrderDetail.SalesOrderMasterId INNER JOIN " +
    //                      " SalesOrderDetailDetail ON SalesOrderDetail.SalesOrderId = SalesOrderDetailDetail.SalesOrderDetailId INNER JOIN " +
    //                      " InventoryMaster ON SalesOrderDetail.InventoryMasterId = InventoryMaster.InventoryMasterId " +
    //                        " WHERE     (SalesOrderMaster.SalesOrderId = '" + id + "')";

    //        SqlCommand cmd = new SqlCommand(str, con);
    //        SqlDataAdapter adp = new SqlDataAdapter(cmd);
    //        DataSet ds = new DataSet();
    //        adp.Fill(ds);
    //        GridView2.DataSource = ds;
    //        GridView2.DataBind();


    //}

    //public void GetOrder(int id)
    //{
    //    string str = "SELECT     SalesOrderMaster.PartyId, SalesOrderMaster.SalesOrderDate, SalesOrderMaster.ShippingCharges, SalesOrderMaster.HandlingCharges, " +
    //                      " SalesOrderMasterDetail.PartyDiscountAmount, SalesOrderMasterDetail.OrderValueDiscountAmount, SalesOrderMaster.GrossAmount, " +
    //                     "  SalesOrderMaster.SalesOrderId " +
    //                    " FROM         SalesOrderMaster INNER JOIN " +
    //                      " SalesOrderMasterDetail ON SalesOrderMaster.SalesOrderId = SalesOrderMasterDetail.SalesOrderId " +
    //                    " WHERE     (SalesOrderMaster.SalesOrderId = '" +id + "')";

    //    SqlCommand cmd = new SqlCommand(str, con);
    //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
    //    DataSet ds = new DataSet();
    //    adp.Fill(ds);


    //    lblOrderNo.Text = id.ToString();
    //        lblDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["SalesOrderDate"]).ToShortDateString();
    //        lblPartyDisc.Text = ds.Tables[0].Rows[0]["PartyDiscountAmount"].ToString();
    //        lblOrderDisc.Text = ds.Tables[0].Rows[0]["OrderValueDiscountAmount"].ToString();
    //        lblShippingChrg.Text = ds.Tables[0].Rows[0]["ShippingCharges"].ToString();
    //        lblHandlingCharg.Text = ds.Tables[0].Rows[0]["HandlingCharges"].ToString();
    //        lblTotal.Text = ds.Tables[0].Rows[0]["GrossAmount"].ToString();



    //}
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        panel2.Visible = false;
        panel1.Visible = true;
    }
    //public void getShippingAddress(int id)
    //{

    //    string str = "SELECT     ShippingAddressID , SalesOrderId, Name, Address, City, State, Country, Phone " +
    //                " FROM         ShippingAddress " +
    //                " WHERE     (SalesOrderId = '" + id + "') ";
    //    SqlCommand cmd = new SqlCommand(str, con);
    //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
    //    DataSet ds = new DataSet();
    //    adp.Fill(ds);

    //    string st = "Name:" + ds.Tables[0].Rows[0]["Name"].ToString() + " <br>Address:" + ds.Tables[0].Rows[0]["Address"].ToString() + " <br>Country:" + ds.Tables[0].Rows[0]["Country"].ToString() + "<br>State:" + ds.Tables[0].Rows[0]["State"].ToString() + "<br> City:" + ds.Tables[0].Rows[0]["City"].ToString() + "<br>Phone:" + ds.Tables[0].Rows[0]["Phone"].ToString() + "";
    //    lblAdress.Text = st.ToString();

    //}

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        //string str = "SELECT     SalesOrderMaster.PartyId, SalesOrderMaster.SalesOrderDate, SalesOrderMaster.ShippingCharges, SalesOrderMaster.HandlingCharges,  " +
        //             " SalesOrderMasterDetail.PartyDiscountAmount, SalesOrderMasterDetail.OrderValueDiscountAmount, SalesOrderMaster.GrossAmount,  " +
        //             " SalesOrderMaster.SalesOrderId,case when (Party_master.Compname is null) then '--' else   Party_master.Compname end as Compname " +
        //           " FROM SalesOrderMaster INNER JOIN " +
        //             " SalesOrderMasterDetail ON SalesOrderMaster.SalesOrderId = SalesOrderMasterDetail.SalesOrderId LEFT OUTER JOIN " +
        //             " Party_master ON SalesOrderMaster.PartyId = Party_master.PartyID " +
        //           " where (SalesOrderMaster.SalesOrderDate between '" + Convert.ToDateTime(txtfromdate.Text) + "' and '" + Convert.ToDateTime(txtTodate.Text) + "')" +
        //           "ORDER BY SalesOrderMaster.SalesOrderId Desc";
        //SqlCommand cmd = new SqlCommand(str, con);
        //SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //DataSet ds = new DataSet();
        //adp.Fill(ds);

        //GridView1.DataSource = ds;
        //GridView1.DataBind();
        bindGrid();
    }
    public void getsalesorder()
    {

        if (Convert.ToString(Session["OrId"]) != "")
        {
            DataSet ds1 = (DataSet)GetMasterDetail();

            if (ds1.Tables[0].Rows.Count > 0)
            {
                lblOrderNo.Text = ds1.Tables[0].Rows[0]["SalesOrderId"].ToString();
                lblDate.Text = ds1.Tables[0].Rows[0]["SalesOrderDate"].ToString();

                lblCDisc.Text = ds1.Tables[0].Rows[0]["Discounts"].ToString();
                lblOVHCharge.Text = ds1.Tables[0].Rows[0]["HandlingCharges"].ToString();
                lblShipCharge.Text = ds1.Tables[0].Rows[0]["ShippingCharges"].ToString();
                lblTax.Text = ds1.Tables[0].Rows[0]["OtherCharges"].ToString();
                lblTotal.Text = ds1.Tables[0].Rows[0]["GrossAmount"].ToString();

            }
            DataSet ds2 = (DataSet)getBillingAddress();
            if (ds2.Tables[0].Rows.Count > 0)
            {
                lblBAddress.Text = ds2.Tables[0].Rows[0]["Address"].ToString();
                LblBCity.Text = ds2.Tables[0].Rows[0]["City"].ToString();
                lblBCountry.Text = ds2.Tables[0].Rows[0]["Country"].ToString();
                lblBName.Text = ds2.Tables[0].Rows[0]["Name"].ToString();
                lblBPhone.Text = ds2.Tables[0].Rows[0]["Phone"].ToString();
                lblBState.Text = ds2.Tables[0].Rows[0]["State"].ToString();
                lblBZip.Text = ds2.Tables[0].Rows[0]["Zipcode"].ToString();



            }
            DataSet ds3 = (DataSet)getShippingAddress();
            if (ds3.Tables[0].Rows.Count > 0)
            {
                lblSAddress.Text = ds3.Tables[0].Rows[0]["Address"].ToString();
                LblSCity.Text = ds3.Tables[0].Rows[0]["City"].ToString();
                lblSCountry.Text = ds3.Tables[0].Rows[0]["Country"].ToString();
                lblSName.Text = ds3.Tables[0].Rows[0]["Name"].ToString();
                lblSPhone.Text = ds3.Tables[0].Rows[0]["Phone"].ToString();
                lblSState.Text = ds3.Tables[0].Rows[0]["State"].ToString();
                lblSZip.Text = ds3.Tables[0].Rows[0]["Zipcode"].ToString();
            }

            DataSet ds4 = (DataSet)getPayment();
            if (ds4.Tables[0].Rows.Count > 0)
            {
                lblPayBy.Text = ds4.Tables[0].Rows[0]["PaymentMethodName"].ToString();
                //lblPayStatus.Text = ds4.Tables[0].Rows[0]["OrderPaymentStatus"].ToString();
                lblPayStatus.Text = ds4.Tables[0].Rows[0]["StatusName"].ToString();
            
            }
            DataSet ds5 = (DataSet)getProdcutDetail();
            GridView2.DataSource = ds5;
            GridView2.DataBind();
            decimal d = 0;
            foreach (GridViewRow gdr in GridView2.Rows)
            {

                d = d + Convert.ToDecimal(gdr.Cells[6].Text);
            }
            lblSubTotal.Text = d.ToString();


        }
        DataSet ds78 = (DataSet)getCustInfo();
        if (ds78.Tables[0].Rows.Count > 0)
        {
            lblemail.Text = ds78.Tables[0].Rows[0]["EmailID"].ToString();

        }
        StringBuilder HeadingTable = new StringBuilder();
        HeadingTable = (StringBuilder)getSiteAddress();
        lblHeading.Text = HeadingTable.ToString();
    }
    public DataSet GetMasterDetail()
    {
        string s1 = " SELECT     SalesOrderId, SalesOrderNo, SalesManId, PartyId, SalesOrderDate, BuyersPOno, ShippersId, ExpextedDeliveryDate, PaymentsTerms, OtherTerms, " +
                           " ShippingCharges, HandlingCharges, OtherCharges, Discounts, GrossAmount  " +
                           " FROM         SalesOrderMaster " +
                           " Where SalesOrderId = '" + Convert.ToString(Session["OrId"]) + "'";

        SqlCommand cmd = new SqlCommand(s1, connection);
        cmd.CommandType = CommandType.Text;
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        sda.Fill(ds);
        return ds;
    }
    public DataSet getBillingAddress()
    {
        SqlCommand cmd = new SqlCommand("SELECT  Name, Address, City, State, Country, Phone, Zipcode FROM BillingAddress WHERE (SalesOrderId = '" + Convert.ToInt32(Session["OrId"]) + "')", connection);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;
    }

    public DataSet getShippingAddress()
    {
        SqlCommand cmd = new SqlCommand("SELECT  Name, Address, City, State, Country, Phone, Zipcode FROM ShippingAddress WHERE (SalesOrderId = '" + Convert.ToInt32(Session["OrId"]) + "')", connection);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;
    }

    public DataSet getPayment()
    {
        //string s = " SELECT     SalesOrderPaymentOption.Id, SalesOrderPaymentOption.SalesOrderId, PaymentMethodMaster.PaymentMethodName, " +
        //            "    SalesOrderPaymentStatus.OrderPaymentStatus " +
        //            " FROM         SalesOrderPaymentOption INNER JOIN " +
        //            "  PaymentMethodMaster ON SalesOrderPaymentOption.PaymentMethodID = PaymentMethodMaster.PaymentMethodID INNER JOIN " +
        //            " SalesOrderPaymentStatus ON SalesOrderPaymentOption.SalesOrderId = SalesOrderPaymentStatus.SalesOrderId " +
        //            " WHERE     (SalesOrderPaymentOption.SalesOrderId = '" + Convert.ToString(Session["OrId"]) + "')  ";
        //SqlCommand cmd = new SqlCommand(s, connection);
        //SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //DataSet ds = new DataSet();
        //adp.Fill(ds);
        //return ds;


        string s = "                SELECT     SalesOrderPaymentOption.Id, SalesOrderPaymentOption.SalesOrderId, PaymentMethodMaster.PaymentMethodName, StatusControl.note,  " +
            " StatusControl.StatusMasterId, StatusMaster.StatusName, StatusControl.StatusControlId " +
           " FROM         SalesOrderPaymentOption INNER JOIN " +
           "  PaymentMethodMaster ON SalesOrderPaymentOption.PaymentMethodID = PaymentMethodMaster.PaymentMethodID INNER JOIN " +
           "  StatusControl ON SalesOrderPaymentOption.SalesOrderId = StatusControl.SalesOrderId INNER JOIN " +
           "  StatusMaster ON StatusControl.StatusMasterId = StatusMaster.StatusId " +
           " WHERE     (SalesOrderPaymentOption.SalesOrderId = '" + Convert.ToString(Session["OrId"]) + "') " +
           " ORDER BY StatusControl.StatusControlId DESC";


        SqlCommand cmd = new SqlCommand(s, connection);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds123 = new DataSet();
        adp.Fill(ds123);
        return ds123;
    }

    public DataSet getProdcutDetail()
    {
        ////////string s = " SELECT     SalesOrderDetail.SalesOrderId, SalesOrderDetail.SalesOrderMasterId, SalesOrderDetail.categorySubSubId, SalesOrderDetail.InventoryWHM_Id, " +
        ////////           "    SalesOrderDetail.Qty, SalesOrderDetail.Rate, SalesOrderDetail.Amount, SalesOrderDetail.Quality, SalesOrderDetail.Notes, InventoryMaster.Name, " +
        ////////           "   SalesOrderDetailDetail.InventoryVolumeDiscountAmount, SalesOrderDetailDetail.PromotionDiscountAmount,  " +
        ////////           "   SalesOrderDetailDetail.PackHandlingAmount " +
        ////////           " FROM         SalesOrderDetail INNER JOIN " +
        ////////           "   InventoryMaster ON SalesOrderDetail.InventoryWHM_Id = InventoryMaster.InventoryMasterId INNER JOIN " +
        ////////           "   SalesOrderDetailDetail ON SalesOrderDetail.SalesOrderId = SalesOrderDetailDetail.SalesOrderDetailId " +
        ////////           " Where SalesOrderDetail.SalesOrderMasterId = '" + Convert.ToString(Session["OrId"]) + "'";


        string s = " SELECT     SalesOrderDetail.SalesOrderId, SalesOrderDetail.SalesOrderMasterId, SalesOrderDetail.categorySubSubId, SalesOrderDetail.InventoryWHM_Id, " +
                 " SalesOrderDetail.Qty, SalesOrderDetail.Rate, SalesOrderDetail.Amount, SalesOrderDetail.Quality, SalesOrderDetail.Notes, InventoryMaster.Name,  " +
                 " SalesOrderDetailDetail.InventoryVolumeDiscountAmount, SalesOrderDetailDetail.PromotionDiscountAmount, SalesOrderDetailDetail.PackHandlingAmount, " +
                 " InventoryWarehouseMasterTbl.InventoryWarehouseMasterId, InventoryWarehouseMasterTbl.WareHouseId, WareHouseMaster.Name AS WarehouseName " +
                 " FROM         InventoryWarehouseMasterTbl INNER JOIN " +
                 " InventoryMaster ON InventoryWarehouseMasterTbl.InventoryMasterId = InventoryMaster.InventoryMasterId INNER JOIN " +
                 " SalesOrderDetail INNER JOIN " +
                 " SalesOrderDetailDetail ON SalesOrderDetail.SalesOrderId = SalesOrderDetailDetail.SalesOrderDetailId ON  " +
                 " InventoryWarehouseMasterTbl.InventoryWarehouseMasterId = SalesOrderDetail.InventoryWHM_Id INNER JOIN " +
                 " WareHouseMaster ON InventoryWarehouseMasterTbl.WareHouseId = WareHouseMaster.WareHouseId " +
        "    Where SalesOrderDetail.SalesOrderMasterId = '" + Convert.ToString(Session["OrId"]) + "'";



        SqlCommand cmd = new SqlCommand(s, connection);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);


        if (ds.Tables[0].Rows.Count > 0)
        {

            Session["warehouseid"] = ds.Tables[0].Rows[0]["WareHouseId"].ToString();
        }
        else
        {
        }

        return ds;
    }
    public StringBuilder getSiteAddress()
    {

        //SqlCommand cmd = new SqlCommand("Sp_select_Siteaddress", con);
        //cmd.CommandType = CommandType.StoredProcedure;
        //SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //DataTable ds = new DataTable();
        //adp.Fill(ds);
        ////return ds;
        //// string path = Server.MapPath(@"../ShoppingCart/images/logo.gif"); 
        //StringBuilder strAddress = new StringBuilder();
        //strAddress.Append("<table width=\"100%\"> ");

        //strAddress.Append("<tr><td> <img src=\"http://www.indianmall.com/ShoppingCart/images/logo.gif\" \"border=\"0\"  /> </td><td align=\"center\"><b><span style=\"color: #996600\">" + ds.Rows[1]["Sitename"].ToString() + "</span></b><Br><b>" + ds.Rows[1]["CompanyName"].ToString() + "</b><Br>" + ds.Rows[1]["Address1"].ToString() + "<Br><b>TollFree:</b>" + ds.Rows[1]["TollFree1"].ToString() + "," + ds.Rows[0]["TollFree2"].ToString() + "<Br><b>Phone:</b>" + ds.Rows[1]["Phone1"].ToString() + "," + ds.Rows[1]["Phone2"].ToString() + "<Br><b>Fax:</b>" + ds.Rows[1]["Fax"].ToString() + "<Br><b>Email:</b>" + ds.Rows[1]["Email"].ToString() + "<Br><b>Website:</b>" + ds.Rows[1]["SiteUrl"].ToString() + " </td></tr>  ");


        //strAddress.Append("</table> ");
        //ViewState["sitename"] = ds.Rows[1]["Sitename"].ToString();
        //return strAddress;






        string warename = Session["warehouseid"].ToString();
        string ADDRESSEX = "select  distinct CompanyWebsitMaster.Sitename,CompanyWebsitMaster.SiteUrl,  CompanyMaster.CompanyName,CompanyMaster.CompanyId,  CompanyWebsiteAddressMaster.Address1,CompanyWebsiteAddressMaster.Phone1,  CompanyWebsiteAddressMaster.Phone2,CompanyWebsiteAddressMaster.Fax,CompanyWebsiteAddressMaster.Email,  CompanyWebsiteAddressMaster.Zip,CompanyWebsiteAddressMaster.TollFree1,CompanyWebsiteAddressMaster.TollFree2,  CompanyWebsiteAddressMaster.State,CompanyWebsiteAddressMaster.City,CompanyWebsiteAddressMaster.Country ,  CountryMaster.CountryName,CityMasterTbl.CityName,StateMasterTbl.StateName,CityMasterTbl.StateId,StateMasterTbl.CountryId  from   CompanyWebsitMaster , CompanyMaster,CompanyWebsiteAddressMaster,StateMasterTbl, CityMasterTbl,CountryMaster  WHERE  CompanyWebsitMaster.CompanyId=CompanyMaster.CompanyId  AND   CompanyWebsiteAddressMaster.CompanyWebsiteMasterId=CompanyWebsitMaster.CompanyWebsiteMasterId  and   CompanyWebsiteAddressMaster.City= CityMasterTbl.CityId and CityMasterTbl.StateId=StateMasterTbl.StateId  and StateMasterTbl.CountryId=CountryMaster.CountryId and   CompanyWebsitMaster.WHId=1";
        SqlCommand cmd = new SqlCommand(ADDRESSEX, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);


        StringBuilder strAddress = new StringBuilder();
        strAddress.Append("<table width=\"100%\"> ");

        strAddress.Append("<tr><td align=\"right\" > <img src=\"http://www.indianmall.com/ShoppingCart/images/logo.gif\" \"border=\"0\"  /> </td><td align=\"center\"><b><span style=\"color: #996600\">" + ds.Rows[0]["CompanyName"].ToString() + "</span></b><Br><b>" + ds.Rows[0]["Sitename"].ToString() + "</b><Br>" + ds.Rows[0]["Address1"].ToString() + "");

        if (ds.Rows[0]["Zip"].ToString() != "")
        {
            strAddress.Append("<BR>" + ds.Rows[0]["CityName"].ToString() + "," + ds.Rows[0]["StateName"].ToString() + "," + ds.Rows[0]["CountryName"].ToString() + "," + ds.Rows[0]["Zip"].ToString() + "<Br>");
        }
        else
        {
            strAddress.Append("<BR>" + ds.Rows[0]["CityName"].ToString() + "," + ds.Rows[0]["StateName"].ToString() + "," + ds.Rows[0]["CountryName"].ToString() + "<Br>");
        }
        if (ds.Rows[0]["TollFree2"].ToString() == "" && ds.Rows[0]["TollFree1"].ToString() == "")
        {

        }

        else if (ds.Rows[0]["TollFree2"].ToString() != "")
        {
            strAddress.Append("<b>TollFree:</b>" + ds.Rows[0]["TollFree1"].ToString() + "," + ds.Rows[0]["TollFree2"].ToString() + "<br>");

        }
        else
        {
            strAddress.Append("<b>TollFree:</b>" + ds.Rows[0]["TollFree1"].ToString() + "<br>");
        }

        if (ds.Rows[0]["Phone2"].ToString() == "" && ds.Rows[0]["Phone2"].ToString() == "")
        {
        }
        else if (ds.Rows[0]["Phone2"].ToString() != "")
        {
            strAddress.Append("<b>Phone:</b>" + ds.Rows[0]["Phone1"].ToString() + "," + ds.Rows[0]["Phone2"].ToString() + "<Br>");
        }
        else
        {
            strAddress.Append("<b>Phone:</b>" + ds.Rows[0]["Phone1"].ToString() + "<br>");
        }

        if (ds.Rows[0]["Fax"].ToString() != "")
        {
            strAddress.Append("<b>Fax:</b>" + ds.Rows[0]["Fax"].ToString() + "<Br>");
        }
        else
        {
        }
        if (ds.Rows[0]["Email"].ToString() != "")
        {
            strAddress.Append("<b>Email:</b>" + ds.Rows[0]["Email"].ToString() + "<Br>");
        }
        else
        {
        }
        if (ds.Rows[0]["SiteUrl"].ToString() != "")
        {
            strAddress.Append("<b>Website:</b>" + ds.Rows[0]["SiteUrl"].ToString() + " </td></tr>  ");
        }
        else
        {
        }

        strAddress.Append("</table> ");

        ViewState["sitename"] = ds.Rows[0]["Sitename"].ToString();
        return strAddress;

    }

    public DataSet getCustInfo()
    {

        string str = "SELECT     SalesOrderMaster.PartyId, SalesOrderMaster.SalesOrderId, User_master.UserID, User_master.Name, User_master.EmailID " +
                    " FROM         SalesOrderMaster INNER JOIN " +
        " Party_master ON SalesOrderMaster.PartyId = Party_master.PartyID INNER JOIN " +
         " User_master ON Party_master.PartyID = User_master.PartyID " +
                        " WHERE (SalesOrderMaster.SalesOrderId = '" + Convert.ToInt32(Session["OrId"]) + "')";
        SqlCommand cmd = new SqlCommand(str, connection);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;
    }

    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        //Response.Redirect(ViewState["ret"].ToString());
        panel2.Visible = false;
        panel1.Visible = true;
    }
    protected void Button101_Click(object sender, EventArgs e)
    {
        bindGrid();
    }

    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");

            Button1.Text = "Hide Printable Version";
            Button2.Visible = true;

            if (GridView1.Columns[4].Visible == true)
            {
                ViewState["detailHide"] = "tt";
                GridView1.Columns[4].Visible = false;
            }

            //ImageButton3.Visible = false;
            //ImageButton4.Visible = false;
           
        }
        else
        {

            Button1.Text = "Printable Version";
            Button2.Visible = false;

            if (ViewState["detailHide"] != null)
            {
                GridView1.Columns[4].Visible = true;
            }

            //ImageButton3.Visible = true;
            //ImageButton4.Visible = true;
          
        }
    }

}

