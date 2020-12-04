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

public partial class ShoppingCart_Admin_Register_SalesOrder : System.Web.UI.Page
{
    SqlConnection ifile=new SqlConnection(PageConn.connnn) ;
    
    SqlConnection connection=new SqlConnection(PageConn.connnn);
    SqlConnection con = new SqlConnection(PageConn.connnn);
    string compid;
    protected void Page_Load(object sender, EventArgs e)
    {
        //PageConn pgcon = new PageConn();
        //con = pgcon.dynconn;
        //connection = pgcon.dynconn;
        //ifile = pgcon.dynconn; 
        if (!IsPostBack)
        {
            compid = Session["comid"].ToString();
        }
        
            pagetitleclass pg = new pagetitleclass();
            string strData = Request.Url.ToString();
            compid = Session["Comid"].ToString();
            char[] separator = new char[] { '/' };

            string[] strSplitArr = strData.Split(separator);
            int i = Convert.ToInt32(strSplitArr.Length);
            string page = strSplitArr[i - 1].ToString();


            Page.Title = pg.getPageTitle(page);
            ViewState["sortOrder"] = "";


            Label1.Visible = false;
            compid = Session["Comid"].ToString();
            pnlColumnSelect.Visible = false;
            ModalPopupExtender1.Hide();
            //Session["pnl1"] = "8";
            //Session["pagename"] = "Register_SalesOrder.aspx";
       // }
        if (!IsPostBack)
        {
            Fillddlwarehouse();
            ddlwarehouse.SelectedIndex = ddlwarehouse.Items.IndexOf(ddlwarehouse.Items.FindByValue(Session["WH"].ToString()));

            rbtnlist_SelectedIndexChanged(sender, e);
            FillddlInvCat();
            FillddlParty();
            FillddlPaymentType();
            ddlInvCat_SelectedIndexChanged(sender, e);
            gridtax();
           // FillddlInvCat();
            FillddlTypesOfSale();
            FillddlSalePerson();
            //FillddlOrderStatusForGrid();
           // FillddlPaymentTypeForGrid();
            filldelivary();
            fillpaystatus();
            if (Request.QueryString["docid"] != null)
            {

                if (!IsPostBack)
                {
                    compid = Session["comid"].ToString();
                  
                    string sssx11 = "SELECT WarehouseMaster.WarehouseId, DocumentMainType.DocumentMainType + '/'+DocumentSubType.DocumentSubType +'/'+ DocumentType.DocumentType as DocumentType,DocumentDate,DocumentTitle,DocumentId FROM WarehouseMaster inner join DocumentMainType on DocumentMainType.Whid=WarehouseId inner join DocumentSubType on DocumentSubType.DocumentMainTypeId=DocumentMainType.DocumentMainTypeId inner join DocumentType on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId inner join DocumentMaster on DocumentMaster.DocumentTypeId=DocumentType.DocumentTypeId where DocumentId='" + Request.QueryString["docid"] + "'";
                    SqlDataAdapter adpt11 = new SqlDataAdapter(sssx11, con);
                    DataTable dtpt11 = new DataTable();
                    adpt11.Fill(dtpt11);
                    if (dtpt11.Rows.Count > 0)
                    {
                        pnnl.Visible = true;

                        Label4.Text = dtpt11.Rows[0]["DocumentId"].ToString();
                        Label10.Text = dtpt11.Rows[0]["DocumentTitle"].ToString();

                        Label11.Text = Convert.ToDateTime(dtpt11.Rows[0]["DocumentDate"]).ToShortDateString(); ;

                        Label12.Text = dtpt11.Rows[0]["DocumentType"].ToString();

                        ddlwarehouse.SelectedValue = dtpt11.Rows[0]["WarehouseId"].ToString();
                        ddlwarehouse.Enabled = false;
                        
                        //  btnGo.Click += new EventHandler(btnGo_Click);

                    }
                }
                imgin.Visible = true;

            }
            else
            {
                string eeed = " Select distinct EmployeeMaster.Whid from  EmployeeMaster where EmployeeMasterId='" + Session["EmployeeId"] + "'";
                SqlCommand cmdeeed = new SqlCommand(eeed, con);
                SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
                DataTable dteeed = new DataTable();
                adpeeed.Fill(dteeed);
                if (dteeed.Rows.Count > 0)
                {
                    ddlwarehouse.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);

                }
            }
            ddlwarehouse_SelectedIndexChanged(sender, e);


           

            if (Request.QueryString["SaleOrderNo"] != null)
            {
                Session["OrId"] = Convert.ToInt32(Request.QueryString["SaleOrderNo"]);

                getsalesorder();
                panel2.Visible = true;
               // panel1.Visible = false;
            }
            else
            {
                txtfromdate.Text = System.DateTime.Now.Month.ToString() + "/01/" + System.DateTime.Now.Year.ToString();
                txttodate.Text = System.DateTime.Now.ToShortDateString();
                //bindGrid();
                panel2.Visible = false;
            }
            Button1_Click(sender, e);

        }
    }
    protected void Fillddlmonth()
    {
        String str = "SELECT  * FROM MonthMaster";

        SqlCommand cmd = new SqlCommand(str, con);
        cmd.CommandType = CommandType.Text;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        ddlmonth.DataSource = dt;
        ddlmonth.DataTextField = "MonthName";
        ddlmonth.DataValueField = "MonthMasterId";
        ddlmonth.DataBind();
        ddlmonth.Items.Insert(0, "All");

        ddlmonth.Items[0].Value = "0";
        ddlmonth.SelectedIndex = 0;
    }
    protected void Fillddlyear()
    {
        String str = "SELECT  * FROM Year_master order by Year desc";

        SqlCommand cmd = new SqlCommand(str, con);
        cmd.CommandType = CommandType.Text;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        ddlyear.DataSource = dt;
        ddlyear.DataTextField = "Year";
        ddlyear.DataValueField = "YearID";
        ddlyear.DataBind();
        ddlyear.Items.Insert(0, "All");

        ddlyear.Items[0].Value = "0";
        ddlyear.SelectedIndex = 0;
    }
    protected void Fillddlperiod()
    {

        String str = "SELECT  * FROM PeriodMaster";
        SqlCommand cmd = new SqlCommand(str, con);
        cmd.CommandType = CommandType.Text;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        ddlperiod.DataSource = dt;
        ddlperiod.DataTextField = "PeriodName";
        ddlperiod.DataValueField = "PeriodId";
        ddlperiod.DataBind();
        ddlperiod.Items.Insert(0, "All");

        ddlperiod.Items[0].Value = "0";
        ddlperiod.SelectedIndex = 0;

    }
    protected void FillddlParty()
    {
       string strparty = "";
        if (ddlwarehouse.SelectedIndex > 0)
        {
            strparty = "SELECT  Party_master.PartyID,Convert(nvarchar(10),Party_master.PartyID)+':'+Party_master.Compname+':'+Party_master.Contactperson  as Compname,  Party_master.PartyTypeId  FROM  Party_master left outer join PartytTypeMaster on  Party_master.PartyTypeId = PartytTypeMaster.PartyTypeId where  Party_master.id = '" + compid + "' and Party_master.Whid='" + ddlwarehouse.SelectedValue + "'"; //partytype2 = Customer
        }
        else
        {
            strparty = "SELECT  Party_master.PartyID,Convert(nvarchar(10),Party_master.PartyID)+':'+Party_master.Compname+':'+Party_master.Contactperson  as Compname,  Party_master.PartyTypeId  FROM  Party_master left outer join PartytTypeMaster on  Party_master.PartyTypeId = PartytTypeMaster.PartyTypeId where  Party_master.id = '" + compid + "'"; //partytype2 = Customer

        }
        SqlCommand cmdparty = new SqlCommand(strparty, con);
        SqlDataAdapter adpparty = new SqlDataAdapter(cmdparty);
        DataTable dtparty = new DataTable();
        adpparty.Fill(dtparty);

        ddlParty.DataSource = dtparty;

        ddlParty.DataTextField = "Compname";
        ddlParty.DataValueField = "PartyID";
        ddlParty.DataBind();
        ddlParty.Items.Insert(0, "All");
        ddlParty.Items[0].Value = "0";
    }
    protected void FillddlPaymentType()
    {
        string strpaytype = "SELECT    PaymentMethodID     ,PaymentMethodName  FROM PaymentMethodMaster";
        SqlCommand cmdpaytype = new SqlCommand(strpaytype, con);
        SqlDataAdapter adppaytype = new SqlDataAdapter(cmdpaytype);
        DataTable dtpaytype = new DataTable();
        adppaytype.Fill(dtpaytype);

        ddlPaymentType.DataSource = dtpaytype;

        ddlPaymentType.DataTextField = "PaymentMethodName";
        ddlPaymentType.DataValueField = "PaymentMethodID";
        ddlPaymentType.DataBind();
        ddlPaymentType.Items.Insert(0, "All");
        ddlPaymentType.Items[0].Value = "0";
    }
    protected void FillddlTypesOfSale()
    {
        //string strTypeOfSale = "SELECT  Entry_Type_Id  ,Entry_Type_Name FROM EntryTypeMaster   where Entry_Type_Id in (26,30) and compid = '"+compid+"'";
        string strTypeOfSale = "SELECT  Entry_Type_Id  ,Entry_Type_Name FROM EntryTypeMaster where Entry_Type_Id In('1','2','3','6','7','30','26')";
        SqlCommand cmdTypeOfSale = new SqlCommand(strTypeOfSale, con);
        SqlDataAdapter adpTypeOfSale = new SqlDataAdapter(cmdTypeOfSale);
        DataTable dtTypeOfSale = new DataTable();
        adpTypeOfSale.Fill(dtTypeOfSale);

        ddlTypeOfSale.DataSource = dtTypeOfSale;

        ddlTypeOfSale.DataTextField = "Entry_Type_Name";
        ddlTypeOfSale.DataValueField = "Entry_Type_Id";
        ddlTypeOfSale.DataBind();
        ddlTypeOfSale.Items.Insert(0, "All");
        ddlTypeOfSale.Items[0].Value = "0";


    }
    protected void fillpaystatus()
    {

        DataTable dtTypeOfSale = select("Select StatusName,StatusId from StatusMaster where StatusId IN('14','15','20','21','28','29','30')  Order by StatusName");

        ddlpaymentstatus.DataSource = dtTypeOfSale;

        ddlpaymentstatus.DataTextField = "StatusName";
        ddlpaymentstatus.DataValueField = "StatusId";
        ddlpaymentstatus.DataBind();
        ddlpaymentstatus.Items.Insert(0, "All");
        ddlpaymentstatus.Items[0].Value = "0";


    }
    protected void filldelivary()
    {

        DataTable dtTypeOfSale = select("Select StatusName,StatusId from StatusMaster where StatusCategoryMasterId='31'  Order by StatusName");

        ddldeliverystatus.DataSource = dtTypeOfSale;

        ddldeliverystatus.DataTextField = "StatusName";
        ddldeliverystatus.DataValueField = "StatusId";
        ddldeliverystatus.DataBind();
        ddldeliverystatus.Items.Insert(0, "All");
        ddldeliverystatus.Items[0].Value = "0";


    }
 
    protected void FillddlSalePerson()
    {
           string strsaleperson = "";
        if (ddlwarehouse.SelectedIndex > 0)
        {
            strsaleperson = "Select distinct AccountMaster.AccountName,AccountMaster.AccountID from AccountMaster inner join Party_Master on  AccountMaster.AccountID=party_master.Account where " +
                                    " compid ='" + compid + "' and AccountMaster.Whid='" + ddlwarehouse.SelectedValue + "'";
        }
        else
        {
            strsaleperson = "Select distinct AccountMaster.AccountName,AccountMaster.AccountID from AccountMaster inner join Party_Master on AccountMaster.AccountID=party_master.Account where " +
                                    " compid ='" + compid + "'";
        }
      
        SqlCommand cmdsaleperson = new SqlCommand(strsaleperson, con);
        SqlDataAdapter adpsaleperson = new SqlDataAdapter(cmdsaleperson);
        DataTable dtsaleperson = new DataTable();
        adpsaleperson.Fill(dtsaleperson);

        ddlSalesPerson.DataSource = dtsaleperson;

        ddlSalesPerson.DataTextField = "AccountName";
        ddlSalesPerson.DataValueField = "AccountID";
        ddlSalesPerson.DataBind();
        ddlSalesPerson.Items.Insert(0, "All");
        ddlSalesPerson.Items[0].Value = "0";


    }
    protected void Fillddlwarehouse()
    {
        string strwh = "SELECT Distinct WareHouseId,Name  FROM WareHouseMaster inner join EmployeeWarehouseRights on EmployeeWarehouseRights.Whid=WareHouseMaster.WareHouseId where comid = '" + Session["comid"] + "'and WareHouseMaster.Status='" + 1 + "' and EmployeeWarehouseRights.AccessAllowed='True' order by name";

        SqlCommand cmd = new SqlCommand(strwh, con);
        cmd.CommandType = CommandType.Text;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        ddlwarehouse.DataSource = dt;
        ddlwarehouse.DataTextField = "Name";
        ddlwarehouse.DataValueField = "WareHouseId";
        ddlwarehouse.DataBind();
        ddlwarehouse.Items.Insert(0, "Select");
        ddlwarehouse.Items[0].Value = "0";
    
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        //Calendar1.Visible = true;
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        ModalPopupExtender1.Hide();
    }
   
    protected void Button1_Click(object sender, EventArgs e)
    {
        lbltypeofsalesprint.Text = ddlTypeOfSale.SelectedItem.Text;
        lblsalesperson.Text = ddlSalesPerson.SelectedItem.Text;
        lblpaymenttype.Text = ddlPaymentType.SelectedItem.Text;
        lblpartyprint.Text = ddlParty.SelectedItem.Text;

        bindGrid();
    }
   
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Sort")
        {
            return;
        }
        else if (e.CommandName == "AddDoc")
        {
            //// GridView2.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            //int dk = Convert.ToInt32(e.CommandArgument);// Convert.ToInt32(GridView2.DataKeys[GridView2.SelectedIndex].Value);
            //ViewState["Dk"] = dk;
            //string scpt = "select * from AttachmentMaster where RelatedTableId='" + ViewState["Dk"] + "'";

            //SqlDataAdapter adp58 = new SqlDataAdapter(scpt, con);
            //DataTable ds58 = new DataTable();
            //adp58.Fill(ds58);
            //if (ds58.Rows.Count > 0)
            //{
            //    grd.DataSource = ds58;
            //    grd.DataBind();
            //    ModalPopupExtender4.Show();
            //}
            // GridView2.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            int dk = Convert.ToInt32(e.CommandArgument);// Convert.ToInt32(GridView2.DataKeys[GridView2.SelectedIndex].Value);
            ViewState["Dk"] = dk;

            string entryno = "select TranctionMaster.EntryNumber ,EntryTypeMaster.SortName from TranctionMaster inner join EntryTypeMaster on EntryTypeMaster.Entry_Type_Id=TranctionMaster.EntryTypeId where Tranction_Master_Id='" + ViewState["Dk"] + "'";

            SqlDataAdapter adpentno = new SqlDataAdapter(entryno, con);
            DataTable dsentno = new DataTable();
            adpentno.Fill(dsentno);
            if (dsentno.Rows.Count > 0)
            {
                lbldocentrytype.Text = dsentno.Rows[0]["SortName"].ToString();
                lbldocentryno.Text = dsentno.Rows[0]["EntryNumber"].ToString();

            }


            // string scpt = "select * from AttachmentMaster where RelatedTableId='" + ViewState["Dk"] + "'";
            string scpt = " select AttachmentMaster.Id,DocumentMaster.DocumentId as IfilecabinetDocId,DocumentMaster.DocumentTitle as Titlename,CONVERT (Nvarchar, DocumentMaster.DocumentDate,101) as Datetime   ,DocumentMainType.DocumentMainType+':'+ DocumentSubType.DocumentSubType+':'+DocumentType.DocumentType as Filename from AttachmentMaster inner join DocumentMaster on DocumentMaster.DocumentId=AttachmentMaster.IfilecabinetDocId inner join DocumentType on DocumentMaster.DocumentTypeId=DocumentType.DocumentTypeId  inner join DocumentSubType on DocumentSubType.DocumentSubTypeId=DocumentType.DocumentSubTypeId inner join DocumentMainType on DocumentMainType.DocumentMainTypeId=DocumentSubType.DocumentMainTypeId where AttachmentMaster.RelatedTableId='" + ViewState["Dk"] + "' ";
            SqlDataAdapter adp58 = new SqlDataAdapter(scpt, con);
            DataTable ds58 = new DataTable();
            adp58.Fill(ds58);


            if (ds58.Rows.Count > 0)
            {
                grd.DataSource = ds58;
                grd.DataBind();
                ModalPopupExtender4.Show();
            }



            if (e.CommandName == "detail")
            {
                GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            Session["OrId"] = Convert.ToInt32(GridView1.SelectedDataKey.Value);
             
                Response.Redirect("SalesOrderDetailThankyou.aspx?id=" + Session["OrId"].ToString() + " ");


            }
            if (e.CommandName == "AddPayInfo")
            {

                GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
                ViewState["OdrIdForPay"] = Convert.ToInt32(GridView1.SelectedDataKey.Value);
                lblSalesOrderNoFromGrid.Text = ViewState["OdrIdForPay"].ToString();
                Label lblgsamt = (Label)(GridView1.Rows[GridView1.SelectedIndex].FindControl("lblgrdgrossAmt"));
             string sg2w = "SELECT PartyId FROM SalesOrderMaster where SalesOrderId='" + ViewState["OdrIdForPay"].ToString() + "' ";
                SqlCommand cmdddd2w = new SqlCommand(sg2w, con);
                SqlDataAdapter dg2w = new SqlDataAdapter(cmdddd2w);
                DataTable dgh2w = new DataTable();
                dg2w.Fill(dgh2w);

                string sg = "SELECT  UserID ,PartyID FROM User_master left outer join DepartmentmasterMNC on User_master.Department = DepartmentmasterMNC.Departmentid where PartyID='" + dgh2w.Rows[0]["PartyId"].ToString() + "'";
                SqlCommand cmdddd = new SqlCommand(sg, con);
                SqlDataAdapter dg = new SqlDataAdapter(cmdddd);
                DataTable dgh = new DataTable();
                dg.Fill(dgh);
                object userid = dgh.Rows[0]["UserID"];
                ViewState["UseridFromGrid"] = userid.ToString();
                txtPayAmtForGrid.Text = lblgsamt.Text;

                ModalPopupExtender3.Show();
            }
            if (e.CommandName == "AddNote")
            {
                GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
                ViewState["FormNo1"] = Convert.ToInt32(GridView1.SelectedDataKey.Value);
                lblSalesOrderNoFromGrid0.Text = Convert.ToString(ViewState["FormNo1"]);


                Label lblInvNom = (Label)(GridView1.Rows[GridView1.SelectedIndex].FindControl("lblentryno"));
                Label lblPackSlNom = (Label)(GridView1.Rows[GridView1.SelectedIndex].FindControl("lblPackingSlipNo"));
                if (isnumSelf(lblInvNom.Text) != 0)
                {
                    ViewState["InNom"] = lblInvNom.Text;
                }
                if (isnumSelf(lblPackSlNom.Text) != 0)
                {
                    ViewState["PackSl"] = lblPackSlNom.Text;
                }
                FillFormTypeforAddNotes();

                ModalPopupExtender2.Show();

            }
            
        }
        else if (e.CommandName == "Delete")
        {
            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            Label chn = (Label)(GridView1.Rows[GridView1.SelectedIndex].FindControl("lblPackingSlipNo"));
            string sln = Convert.ToString(GridView1.SelectedDataKey.Value);
            Label invn = (Label)(GridView1.Rows[GridView1.SelectedIndex].FindControl("lblentryno"));
            ImageButton tra = (ImageButton)(GridView1.Rows[GridView1.SelectedIndex].FindControl("img1"));
            ViewState["tra"] = tra.CommandArgument.ToString();
                ViewState["ChNo"] = Convert.ToInt32(chn.Text);
           
            
                ViewState["SONob"] = Convert.ToInt32(sln.ToString());
           
           
                ViewState["InNob"] = Convert.ToInt32(invn.Text);
            

         
                Label25.Text = "You Sure! Delete,This Will Delete <br/>Delivery Challan# : " + Convert.ToInt32(ViewState["ChNo"]) + "<br/>";
           
           
                Label25.Text += "Invoice# : " + Convert.ToInt32(ViewState["InNob"]) + " ";

           
          

            ModalPopupExtender1222.Show();
        }


    }
    public int isnumSelf(string ck)
    {
        int i = 0;
        try
        {
            i = Convert.ToInt32(ck);
        }
        catch (Exception)
        {
            i = 0;
        }
        return i;
    }
    protected void FillFormTypeforAddNotes()
    {
        string strfill = " SELECT StatusCategoryMasterId  ,StatusCategory FROM StatusCategory where StatusCategoryMasterId in (13,15,18,29) and compid ='"+compid+"'";
        SqlCommand cmdFill1 = new SqlCommand(strfill, con);
        SqlDataAdapter adpfill1 = new SqlDataAdapter(cmdFill1);
        DataTable dtFill1 = new DataTable();
        adpfill1.Fill(dtFill1);
        if (dtFill1.Rows.Count > 0)
        {
            ddlFormTypeForAddNote.DataSource = dtFill1;
            ddlFormTypeForAddNote.DataTextField = "StatusCategory";
            ddlFormTypeForAddNote.DataValueField= "StatusCategoryMasterId";
            ddlFormTypeForAddNote.DataBind();
            object f = new object();
            EventArgs g = new EventArgs();
            ddlFormTypeForAddNote_SelectedIndexChanged(f,g);
        }


    }
  
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        panel2.Visible = false;
       // panel1.Visible = true;
    }


    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
       
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
     
       string s = " SELECT     SalesOrderPaymentOption.Id, SalesOrderPaymentOption.SalesOrderId, PaymentMethodMaster.PaymentMethodName, StatusControl.note, "+
                  "    StatusControl.StatusMasterId, StatusMaster.StatusName "+
                 " FROM         SalesOrderPaymentOption INNER JOIN "+
                 "     PaymentMethodMaster ON SalesOrderPaymentOption.PaymentMethodID = PaymentMethodMaster.PaymentMethodID INNER JOIN "+
                 "     StatusControl ON SalesOrderPaymentOption.SalesOrderId = StatusControl.SalesOrderId INNER JOIN "+
                 "     StatusMaster ON StatusControl.StatusMasterId = StatusMaster.StatusId left outer join StatusCategory on  StatusMaster.StatusCategoryMasterId = StatusCategory.StatusCategoryMasterId" +
                  " WHERE     (SalesOrderPaymentOption.SalesOrderId = '" + Convert.ToString(Session["OrId"]) + "')and StatusCategory.compid = '"+compid+"'  ";
                SqlCommand cmd = new SqlCommand(s, connection);

        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;
    }

    public DataSet getProdcutDetail()
    {
       

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
        "    Where SalesOrderDetail.SalesOrderMasterId = '" + Convert.ToString(Session["OrId"]) + "' and comid = '"+compid+"'";



        SqlCommand cmd = new SqlCommand(s, connection);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;
    }
    public StringBuilder getSiteAddress()
    {

        SqlCommand cmd = new SqlCommand("Sp_select_Siteaddress", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        //return ds;
        // string path = Server.MapPath(@"../ShoppingCart/images/logo.gif"); 
        StringBuilder strAddress = new StringBuilder();
        strAddress.Append("<table width=\"100%\"> ");

        strAddress.Append("<tr><td> <img src=\"http://www.indianmall.com/ShoppingCart/images/logo.gif\" \"border=\"0\"  /> </td><td align=\"center\"><b><span style=\"color: #996600\">" + ds.Rows[1]["Sitename"].ToString() + "</span></b><Br><b>" + ds.Rows[1]["CompanyName"].ToString() + "</b><Br>" + ds.Rows[1]["Address1"].ToString() + "<Br><b>TollFree:</b>" + ds.Rows[1]["TollFree1"].ToString() + "," + ds.Rows[0]["TollFree2"].ToString() + "<Br><b>Phone:</b>" + ds.Rows[1]["Phone1"].ToString() + "," + ds.Rows[1]["Phone2"].ToString() + "<Br><b>Fax:</b>" + ds.Rows[1]["Fax"].ToString() + "<Br><b>Email:</b>" + ds.Rows[1]["Email"].ToString() + "<Br><b>Website:</b>" + ds.Rows[1]["SiteUrl"].ToString() + " </td></tr>  ");


        strAddress.Append("</table> ");
        ViewState["sitename"] = ds.Rows[1]["Sitename"].ToString();
        return strAddress;

    }

    public DataSet getCustInfo()
    {

        string str = "SELECT     SalesOrderMaster.PartyId, SalesOrderMaster.SalesOrderId, User_master.UserID, User_master.Name, User_master.EmailID " +
                    " FROM         SalesOrderMaster INNER JOIN " +
        " Party_master ON SalesOrderMaster.PartyId = Party_master.PartyID INNER JOIN " +
         " User_master ON Party_master.PartyID = User_master.PartyID left outer join DepartmentmasterMNC on User_master.Department = DepartmentmasterMNC.Departmentid " +
                        " WHERE (SalesOrderMaster.SalesOrderId = '" + Convert.ToInt32(Session["OrId"]) + "') and DepartmentmasterMNC.Companyid = '"+compid+"'";
        SqlCommand cmd = new SqlCommand(str, connection);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;
    }

    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {
        panel2.Visible = false;
        //panel1.Visible = true;
    }
    protected void Button1_Click(object sender, ImageClickEventArgs e)
    {
        lbltypeofsalesprint.Text=ddlTypeOfSale.SelectedItem.Text;
            lblsalesperson.Text=ddlSalesPerson.SelectedItem.Text;
            lblpaymenttype.Text=ddlPaymentType.SelectedItem.Text;
            lblpartyprint.Text = ddlParty.SelectedItem.Text;

          string strc = "select CompanyName from CompanyMaster where [Compid]='" + compid.ToString() + "'";
        SqlCommand cmdc = new SqlCommand(strc, con);
        SqlDataAdapter dac = new SqlDataAdapter(cmdc);
        DataTable dtc = new DataTable();
        dac.Fill(dtc);
        lblcompname.Text = dtc.Rows[0][0].ToString();

        if (ddlwarehouse.SelectedIndex > 0)
        {
            string strw = "Select CurrencyName from [CurrencyMaster] inner join WareHouseMaster on [CurrencyMaster].CurrencyId=WareHouseMaster.CurrencyId where WareHouseMaster.WareHouseId='" + ddlwarehouse.SelectedValue + "'";
            SqlCommand cmdw = new SqlCommand(strw, con);
            SqlDataAdapter daw = new SqlDataAdapter(cmdw);
            DataTable dtw = new DataTable();
            daw.Fill(dtw);
            lblcompname.Text = compid.ToString();
            lblstore.Text = ddlwarehouse.SelectedItem.Text.ToString();

        }
        bindGrid();
       }
    protected void fillhede()
    {
        if (chkboxSelectGridColm.Items[0].Selected == true)
        {
            GridView1.Columns[2].Visible = true;

        }
        else
        {
            GridView1.Columns[2].Visible = false;
        }

        if (chkboxSelectGridColm.Items[1].Selected == true)
        {
            GridView1.Columns[1].Visible = true;
        }
        else
        {
            GridView1.Columns[1].Visible = false;
        }

        if (chkboxSelectGridColm.Items[2].Selected == true)
        {
            GridView1.Columns[3].Visible = true;
        }
        else
        {
            GridView1.Columns[3].Visible = false;
        }

        if (chkboxSelectGridColm.Items[3].Selected == true)
        {
            GridView1.Columns[5].Visible = true;
        }
        else
        {
            GridView1.Columns[5].Visible = false;
        }

        if (chkboxSelectGridColm.Items[4].Selected == true)
        {
            GridView1.Columns[4].Visible = true;
        }
        else
        {
            GridView1.Columns[4].Visible = false;
        }

        if (chkboxSelectGridColm.Items[5].Selected == true)
        {
            GridView1.Columns[6].Visible = true;
        }
        else
        {
            GridView1.Columns[6].Visible = false;
        }

        if (chkboxSelectGridColm.Items[6].Selected == true)
        {
            GridView1.Columns[7].Visible = true;
        }
        else
        {
            GridView1.Columns[7].Visible = false;
        }

        if (chkboxSelectGridColm.Items[7].Selected == true)
        {
            GridView1.Columns[8].HeaderText = chkboxSelectGridColm.Items[7].Text;
            GridView1.Columns[8].Visible = true;
        }
        else
        {
            GridView1.Columns[8].Visible = false;
        }

        if (chkboxSelectGridColm.Items[8].Selected == true)
        {
            GridView1.Columns[9].HeaderText = chkboxSelectGridColm.Items[8].Text;
            GridView1.Columns[9].Visible = true;
        }
        else
        {
            GridView1.Columns[9].Visible = false;
        }

        if (chkboxSelectGridColm.Items[9].Selected == true)
        {
            GridView1.Columns[10].HeaderText = chkboxSelectGridColm.Items[9].Text;
            GridView1.Columns[10].Visible = true;
        }
        else
        {
            GridView1.Columns[10].Visible = false;
        }

        if (chkboxSelectGridColm.Items[10].Selected == true)
        {
            GridView1.Columns[11].Visible = true;
        }
        else
        {
            GridView1.Columns[11].Visible = false;
        }

        if (chkboxSelectGridColm.Items[11].Selected == true)
        {
            GridView1.Columns[12].Visible = true;
        }
        else
        {
            GridView1.Columns[12].Visible = false;
        }
        if (chkboxSelectGridColm.Items[12].Selected == true)
        {
            GridView1.Columns[13].Visible = true;
        }
        else
        {
            GridView1.Columns[13].Visible = false;
        }
        if (chkboxSelectGridColm.Items[13].Selected == true)
        {
            GridView1.Columns[14].Visible = true;
        }
        else
        {
            GridView1.Columns[14].Visible = false;
        }
        if (chkboxSelectGridColm.Items[14].Selected == true)
        {
            GridView1.Columns[15].Visible = true;
        }
        else
        {
            GridView1.Columns[15].Visible = false;
        }
    
    }
    protected void rbtnlist_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtnlist.SelectedValue == "1")
        {
            pnlfromdatetodate.Visible = true;
            pnlmonthyear.Visible = false;
            pnlperiod.Visible = false;
        }
        else if (rbtnlist.SelectedValue == "2")
        {
            pnlfromdatetodate.Visible = false;
            pnlmonthyear.Visible = false;
            pnlperiod.Visible = true;
            Fillddlperiod();
        }
        else
        {
            pnlfromdatetodate.Visible = false;
            pnlmonthyear.Visible = true;
            pnlperiod.Visible = false;
            Fillddlmonth();
            Fillddlyear();
        }
    }

    public void bindGrid()
    {
      

        DataTable dtGrid = (DataTable)(GridFiltersDatatable());
        if (dtGrid.Rows.Count > 0)
        {
          
            fillhede();
            GridView1.DataSource = dtGrid;

            DataView myDataView = new DataView();
            myDataView = dtGrid.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }


            GridView1.DataBind();


            int h = 0;
           

            double tax22 = 0;
            double tax33 = 0;
            double tax11 = 0;
            double tax21 = 0;
            double netamt11 = 0;
            double grossamt11 = 0;
            double shipcost11 = 0;

            foreach (GridViewRow gdrw in GridView1.Rows)
            {
                //int tid = Convert.ToInt32(gridDCregister.DataKeys[h].Value);
                ImageButton img = (ImageButton)gdrw.FindControl("img1");
                Label lblSalesOrder = (Label)gdrw.FindControl("lblSalesOrder");
                Label lblSalesChallan = (Label)gdrw.FindControl("lblSalesChallan");
                  Label lblentryno = (Label)gdrw.FindControl("lblentryno");
                  Label lblEntryTypeId = (Label)gdrw.FindControl("lblEntryTypeId");
                  Label lblpaysta = (Label)gdrw.FindControl("lblpaysta");
                  Label lbldesatat = (Label)gdrw.FindControl("lbldesatat");
                  Label lbldocno = (Label)gdrw.FindControl("lbldocno");
                  //Label lblattach = (Label)gdrw.FindControl("lblattach");
                  int salesid = Convert.ToInt32(GridView1.DataKeys[gdrw.RowIndex].Value);
                  DataTable mixstr =select( "select distinct top(1) [StatusControlId],StatusName,StatusId  from StatusControl inner join StatusMaster on StatusMaster.StatusId=StatusControl.StatusMasterId inner join StatusCategory on StatusCategory.StatusCategoryMasterId=StatusMaster.StatusCategoryMasterId " +
               " where [SalesOrderId]='" + salesid + "' and ([StatusCategory].[StatusCategoryMasterId]='36' or [StatusCategory].[StatusCategoryMasterId]='29') order by [StatusControlId] Desc");
                  if (mixstr.Rows.Count > 0)
                  {
                      lblpaysta.Text = Convert.ToString(mixstr.Rows[0]["StatusName"]);
                  }
                  DataTable mixstr1 = select("select distinct top(1) [StatusControlId],StatusName,StatusId  from StatusControl inner join StatusMaster on StatusMaster.StatusId=StatusControl.StatusMasterId inner join StatusCategory on StatusCategory.StatusCategoryMasterId=StatusMaster.StatusCategoryMasterId " +
               " where [SalesOrderId]='" + salesid + "' and [StatusCategory].[StatusCategoryMasterId]='31'  order by [StatusControlId] Desc");
                  if (mixstr1.Rows.Count > 0)
                  {
                      lbldesatat.Text = Convert.ToString(mixstr1.Rows[0]["StatusName"]);
                  }

                //HtmlAnchor anchor = (HtmlAnchor)gdrw.FindControl("sales");
                //  HtmlAnchor anchor1 = (HtmlAnchor)gdrw.FindControl("ptik");
                //if (lblEntryTypeId.Text.ToString() == "26")
                //{
                //    anchor.HRef = "SalesInvoiceShow.aspx?id=" + lblSalesOrder.Text + "&id2=" + lblentryno.Text + "&id3=" + lblSalesChallan.Text + "";
                //    anchor1.HRef = "SalesRelatedReport2.aspx?id=" + lblSalesOrder.Text + "&id2=" + lblSalesChallan.Text + "&id3=" + lblentryno.Text + "";
                //}
                //else if (lblEntryTypeId.Text.ToString() == "30")
                //{
                //    anchor.HRef = "RetailCustomerDeliveryChallanPrint.aspx?id=" + Convert.ToInt32(lblSalesChallan.Text) + "&wareid=" + Convert.ToInt32(ddlwarehouse.SelectedValue) + "";
                //    //anchor.Disabled =true;
                //   // anchor1.Disabled = true;
                //}

                  string tid = img.CommandArgument;
                  h = h + 1;
                  string scpt = "select * from AttachmentMaster where RelatedTableId='" + tid + "'";

                  SqlDataAdapter adp58 = new SqlDataAdapter(scpt, con);
                  DataTable ds58 = new DataTable();
                  adp58.Fill(ds58);
                  ImageButton link1 = (ImageButton)gdrw.FindControl("img1");
                  if (ds58.Rows.Count == 0)
                  //if(Convert.ToString(lblattach.Text)=="")
                  {
                      lbldocno.Text = "0";
                      link1.ToolTip = "0";
                     // link1.ImageUrl = "~/ShoppingCart/images/Red.jpg";
                      link1.Enabled = false;
                  }
                  else
                  {
                      link1.Enabled = true;
                      link1.ToolTip = ds58.Rows.Count.ToString();
                      lbldocno.Text = ds58.Rows.Count.ToString();


                  }
                if (Request.QueryString["docid"] != null)
                {
                    GridView1.Columns[0].Visible = true;
                }
                else
                {
                    GridView1.Columns[0].Visible = false;
                }

                Label lblgrdgrossAm = (Label)(gdrw.FindControl("lblgrdgrossAmt"));
                Label lblship = (Label)(gdrw.FindControl("lblshipcharge"));

                Label lbltx1 = (Label)(gdrw.FindControl("lbltax1"));
                Label lbltx2 = (Label)(gdrw.FindControl("lbltax2"));
                Label lbltxx3 = (Label)(gdrw.FindControl("lbltax3"));
                Label lbltxx2 = (Label)(gdrw.FindControl("lbltaxx2"));
                Label lblnetamt = (Label)(gdrw.FindControl("lblnetamont"));

                //string saleamt = lblgrdgrossAm.Text;

                double tx1 = 0;
                if (lbltx1.Text == "")
                {
                    tx1 = 0;
                }
                else
                {
                    tx1 = Convert.ToDouble(lbltx1.Text);
                }
                tax11 = tax11 + tx1;
                double tx22 = 0;
                if (lbltxx2.Text == "")
                {
                    tx22 = 0;
                }
                else
                {
                    tx22 = Convert.ToDouble(lbltxx2.Text);
                }
                tax22 = tax22 + tx22;

                double tx33 = 0;
                if (lbltxx3.Text == "")
                {
                    tx33 = 0;
                }
                else
                {
                    tx33 = Convert.ToDouble(lbltxx3.Text);
                }
                tax33 = tax33 + tx33;


                double tx2 = 0;
                if (lbltx2.Text == "--")
                {
                    tx2 = 0;
                }
                else
                {
                    tx2 = Convert.ToDouble(lbltx2.Text);
                }
                tax21 = tax21 + tx2;


                double namt1 = 0;
                if (lblnetamt.Text == "--")
                {
                    namt1 = 0;
                }
                else
                {
                    namt1 = Convert.ToDouble(lblnetamt.Text);
                }
                netamt11 = namt1 + netamt11;



                double gsam1 = 0;
                if (lblgrdgrossAm.Text == "--")
                {
                    gsam1 = 0;
                }
                else
                {
                    gsam1 = Convert.ToDouble(lblgrdgrossAm.Text);
                }
                grossamt11 = grossamt11 + gsam1;



                double shcst1 = 0;
                if (lblship.Text == "")
                {
                    shcst1 = 0;
                }
                else
                {
                    shcst1 = Convert.ToDouble(lblship.Text);
                }
                shipcost11 = shipcost11 + shcst1;



            }

            GridViewRow ft = (GridViewRow)(GridView1.FooterRow);
            Label lbltxx2Footr = (Label)(ft.FindControl("lbltaxx2footer"));
            Label lbltxx3Footr = (Label)(ft.FindControl("lbltax3footer"));
            Label lblgrdgrossAmFootr = (Label)(ft.FindControl("lblgrdgrossAmtfooter"));
            Label lblshipFootr = (Label)(ft.FindControl("lblshipchargefooter"));

            Label lbltx1Footr = (Label)(ft.FindControl("lbltax1footer"));
            Label lbltx2Footr = (Label)(ft.FindControl("lbltax2footer"));
            Label lblnetamtFootr = (Label)(ft.FindControl("lblnetamontfooter"));

            lblgrdgrossAmFootr.Text = "Total :" + grossamt11.ToString();
            lblshipFootr.Text = "Total :" + shipcost11.ToString();
            lbltx1Footr.Text = "Total :" + tax11.ToString();
            lbltx2Footr.Text = "Total :" + tax21.ToString();
            lbltxx2Footr.Text = "Total :" + tax22.ToString();
            lbltxx3Footr.Text = "Total :" + tax33.ToString();
            lblnetamtFootr.Text = "Total :" + netamt11.ToString();


        }
        else
        {
            GridView1.DataSource = null;

           
            GridView1.DataBind();
        }
    }


    public void gridtax()
    {
         chkboxSelectGridColm.Items[7].Enabled=false;
        chkboxSelectGridColm.Items[8].Enabled=false;
        chkboxSelectGridColm.Items[9].Enabled = false;
        DataTable dttxt = select("select * from StorTaxmethodtbl where Storeid='" + ddlwarehouse.SelectedValue + "'");


        if (dttxt.Rows.Count > 0)
        {
            if (dttxt.Rows[0]["Variabletax"].ToString() == "1")
            {
               
                DataTable dtstate = select("Select State,Country,StateName,CountryName from CountryMaster inner join CompanyWebsiteAddressMaster on CompanyWebsiteAddressMaster.Country=CountryMaster.CountryId inner join StateMasterTbl on StateMasterTbl.StateId=CompanyWebsiteAddressMaster.State  inner join CompanyWebsitMaster on CompanyWebsitMaster.CompanyWebsiteMasterId=CompanyWebsiteAddressMaster.CompanyWebsiteMasterId  inner join WarehouseMaster on WarehouseMaster.WarehouseId=CompanyWebsitMaster.Whid where CompanyWebsitMaster.Whid='" + ddlwarehouse.SelectedValue + "'");
                if (dtstate.Rows.Count > 0)
                {
                    //Convert.ToString(dtstate.Rows[0]["Country"]);

                    DataTable dtwhid = select("select Top(3) Taxshortname,PurchaseTaxAccountMasterID,[Tax%],[SalesTaxOption3TaxNameTbl].[Taxname],[SalesTaxOption3TaxNameTbl].[StateId] from [InventoryTaxability] Left join [SalesTaxOption3TaxNameTbl] on [SalesTaxOption3TaxNameTbl].[Id]=[InventoryTaxability].[Taxoption3id] " +
                                         " where Active='1' and [Whid]='" + ddlwarehouse.SelectedValue + "' and ([SalesTaxOption3TaxNameTbl].[StateId]='" + Convert.ToString(dtstate.Rows[0]["State"]) + "' or [SalesTaxOption3TaxNameTbl].[StateId]='0')   order by SalesTaxOption3TaxNameTbl.Id Desc");

                    if (dtwhid.Rows.Count > 0)
                    {

                        if (dtwhid.Rows.Count == 3)
                        {
                            chkboxSelectGridColm.Items[7].Text = Convert.ToString(dtwhid.Rows[0]["Taxshortname"]);
                            chkboxSelectGridColm.Items[8].Text = Convert.ToString(dtwhid.Rows[1]["Taxshortname"]);
                            chkboxSelectGridColm.Items[9].Text = Convert.ToString(dtwhid.Rows[2]["Taxshortname"]);
                            chkboxSelectGridColm.Items[7].Enabled = true;
                            chkboxSelectGridColm.Items[8].Enabled = true;
                            chkboxSelectGridColm.Items[9].Enabled = true;
                        }
                        else if (dtwhid.Rows.Count == 2)
                        {
                            chkboxSelectGridColm.Items[7].Text = Convert.ToString(dtwhid.Rows[0]["Taxshortname"]);
                            chkboxSelectGridColm.Items[8].Text = Convert.ToString(dtwhid.Rows[1]["Taxshortname"]);
                            chkboxSelectGridColm.Items[7].Enabled = true;
                            chkboxSelectGridColm.Items[8].Enabled = true;
                        }
                        else if (dtwhid.Rows.Count == 1)
                        {
                            chkboxSelectGridColm.Items[7].Text = Convert.ToString(dtwhid.Rows[0]["Taxshortname"]);
                            chkboxSelectGridColm.Items[7].Enabled = true;
                        }

                    }
                    else
                    {
                       
                    }
                }
            }
            else if (dttxt.Rows[0]["Fixedtaxdependingonstate"].ToString() == "True")
            {

                DataTable dtstate = select("Select State,Country,StateName,CountryName from CountryMaster inner join CompanyWebsiteAddressMaster on CompanyWebsiteAddressMaster.Country=CountryMaster.CountryId inner join StateMasterTbl on StateMasterTbl.StateId=CompanyWebsiteAddressMaster.State  inner join CompanyWebsitMaster on CompanyWebsitMaster.CompanyWebsiteMasterId=CompanyWebsiteAddressMaster.CompanyWebsiteMasterId  inner join WarehouseMaster on WarehouseMaster.WarehouseId=CompanyWebsitMaster.Whid where CompanyWebsitMaster.Whid='" + ddlwarehouse.SelectedValue + "'");
                if (dtstate.Rows.Count > 0)
                {
              
                    DataTable dtwhid = select("select Top(3) Taxshortname,PurchaseTaxAccountMasterID from [TaxTypeMasterMoreInfo]  where Active='1' and [Whid]='" + ddlwarehouse.SelectedValue + "' and ([TaxTypeMasterMoreInfo].[StateId]='" + Convert.ToString(dtstate.Rows[0]["State"]) + "' or [TaxTypeMasterMoreInfo].[StateId]='0')   order by TaxTypeMasterMoreInfo.Id Desc");

                    if (dtwhid.Rows.Count > 0)
                    {

                        if (dtwhid.Rows.Count == 3)
                        {
                            chkboxSelectGridColm.Items[7].Text = Convert.ToString(dtwhid.Rows[0]["Taxshortname"]);
                            chkboxSelectGridColm.Items[8].Text = Convert.ToString(dtwhid.Rows[1]["Taxshortname"]);
                            chkboxSelectGridColm.Items[9].Text = Convert.ToString(dtwhid.Rows[2]["Taxshortname"]);
                            chkboxSelectGridColm.Items[7].Enabled = true;
                            chkboxSelectGridColm.Items[8].Enabled = true;
                            chkboxSelectGridColm.Items[9].Enabled = true;
                        }
                        else if (dtwhid.Rows.Count == 2)
                        {
                            chkboxSelectGridColm.Items[7].Text = Convert.ToString(dtwhid.Rows[0]["Taxshortname"]);
                            chkboxSelectGridColm.Items[8].Text = Convert.ToString(dtwhid.Rows[1]["Taxshortname"]);
                            chkboxSelectGridColm.Items[7].Enabled = true;
                            chkboxSelectGridColm.Items[8].Enabled = true;
                        }
                        else if (dtwhid.Rows.Count == 1)
                        {
                            chkboxSelectGridColm.Items[7].Text = Convert.ToString(dtwhid.Rows[0]["Taxshortname"]);
                            chkboxSelectGridColm.Items[7].Enabled = true;
                        }
                    }
                  
                }
            }
            else if (dttxt.Rows[0]["Fixedtaxforall"].ToString() == "True")
            {

                DataTable dtwhid = select("select Top(3) Taxshortname,PurchaseTaxAccountMasterID from [TaxTypeMaster]  where Active='1' and [Whid]='" + ddlwarehouse.SelectedValue + "'   order by TaxTypeMaster.TaxTypeMasterId Desc");

                if (dtwhid.Rows.Count > 0)
                {

                    if (dtwhid.Rows.Count == 3)
                    {
                        chkboxSelectGridColm.Items[7].Text = Convert.ToString(dtwhid.Rows[0]["Taxshortname"]);
                        chkboxSelectGridColm.Items[8].Text = Convert.ToString(dtwhid.Rows[1]["Taxshortname"]);
                        chkboxSelectGridColm.Items[9].Text = Convert.ToString(dtwhid.Rows[2]["Taxshortname"]);
                        chkboxSelectGridColm.Items[7].Enabled = true;
                        chkboxSelectGridColm.Items[8].Enabled = true;
                        chkboxSelectGridColm.Items[9].Enabled = true;
                    }
                    else if (dtwhid.Rows.Count == 2)
                    {
                        chkboxSelectGridColm.Items[7].Text = Convert.ToString(dtwhid.Rows[0]["Taxshortname"]);
                        chkboxSelectGridColm.Items[8].Text = Convert.ToString(dtwhid.Rows[1]["Taxshortname"]);
                        chkboxSelectGridColm.Items[7].Enabled = true;
                        chkboxSelectGridColm.Items[8].Enabled = true;
                    }
                    else if (dtwhid.Rows.Count == 1)
                    {
                        chkboxSelectGridColm.Items[7].Text = Convert.ToString(dtwhid.Rows[0]["Taxshortname"]);
                        chkboxSelectGridColm.Items[7].Enabled = true;
                    }
                }
                
            }
        }


    }
    private DataTable GridFiltersDatatable()
    {

        

        DataTable dt = new DataTable();
        ViewState["StrMMM"] = null;




        string str = "SELECT  distinct SalesChallanMaster.SalesChallanMasterId, SalesChallanMaster.PartyID, CONVERT(nvarchar(10), SalesChallanMaster.SalesChallanDatetime, 101)" +
                      "AS SalesChallanDatetime, SalesChallanMaster.ShipToAddress, SalesChallanMaster.BillToAddress, SalesChallanMaster.EntryTypeId, "+
                      "TranctionMaster.Tranction_Master_Id, TranctionMaster.Date, TranctionMaster.EntryNumber, TranctionMaster.UserId, SalesChallanMoreInfo.InvoiceNo, "+
                      "SalesChallanMoreInfo.RecieptNo, CASE WHEN (Party_master.Compname IS NULL) THEN '--' ELSE Party_master.Compname END AS PartyName, "+
                      "CASE WHEN (SalesOrderMaster.GrossAmount IS NULL) THEN '--' ELSE SalesOrderMaster.GrossAmount END AS GrossAmount, "+
                      "CASE WHEN (SalesOrderMaster.OtherCharges IS NULL) THEN '--' ELSE SalesOrderMaster.OtherCharges END AS Tax1, "+
                      "CASE WHEN (SalesOrderMaster.HandlingCharges IS NULL) THEN '--' ELSE SalesOrderMaster.HandlingCharges END AS HandlingCharges, "+
                      "SalesChallanMoreInfo.ShippingCost, CASE WHEN (SalesOrderMasterTamp.subTotal IS NULL) THEN '--' ELSE SalesOrderMasterTamp.subTotal END AS Amount, " +
                      "EntryTypeMaster.Entry_Type_Name,AccountMaster.AccountName, SalesOrderMaster.SalesManId, SalesOrderMaster.PaymentsTerms, SalesOrderMaster.SalesOrderNo, " +
                      "SalesOrderMaster.SalesOrderId, CONVERT(nvarchar(10), SalesOrderMaster.SalesOrderDate, 101) AS SalesOrderDate, PaymentMethodMaster.PaymentMethodName,"+
                      "PaymentMethodMaster.PaymentMethodID,SalesOrderMaster.Tax1Amt,SalesOrderMaster.Tax2Amt,SalesOrderMaster.Tax3Amt " +
                      "FROM  AccountMaster inner join Party_master ON party_master.Account=AccountMaster.AccountID  RIGHT OUTER JOIN "+
                      "SalesChallanMaster ON Party_master.PartyID = SalesChallanMaster.PartyID LEFT OUTER JOIN "+
                      "SalesChallanMoreInfo ON SalesChallanMaster.SalesChallanMasterId = SalesChallanMoreInfo.SalesChallanMasterId RIGHT OUTER JOIN "+
                      "SalesOrderMasterTamp RIGHT OUTER JOIN " +
                      "TransactionMasterMoreInfo RIGHT OUTER JOIN "+
                      "PaymentMethodMaster INNER JOIN "+
                      "SalesOrderMaster ON PaymentMethodMaster.PaymentMethodID = SalesOrderMaster.PaymentsTerms ON "+
                      "TransactionMasterMoreInfo.SalesOrderId = SalesOrderMaster.SalesOrderId ON SalesOrderMasterTamp.SalesOrderTempId = SalesOrderMaster.SalesOrderId ON " +
                      "SalesChallanMaster.RefSalesOrderId = SalesOrderMaster.SalesOrderId LEFT OUTER JOIN StatusControl on StatusControl.SalesOrderId=SalesOrderMaster.SalesOrderId LEFT OUTER JOIN StatusMaster on StatusMaster.StatusId=StatusControl.StatusMasterId LEFT OUTER JOIN " +
                      "EntryTypeMaster RIGHT OUTER JOIN "+
                      "TranctionMaster ON EntryTypeMaster.Entry_Type_Id = TranctionMaster.EntryTypeId ON "+
                      "TransactionMasterMoreInfo.Tranction_Master_Id = TranctionMaster.Tranction_Master_Id  where  AccountMaster.compid='" + Session["comid"] + "' and AccountMaster.Whid = '" + ddlwarehouse.SelectedValue + "' and TranctionMaster.Tranction_Master_Id IS NOT NULL and Party_master.Whid='" + ddlwarehouse.SelectedValue + "'";


        String between = "and SalesOrderMaster.SalesOrderDate " + (String)GetDateBetweens();
        String FilterByInv = "";
        String FilterByPartyId = "";
       // String strMMM = "";


        ViewState["StrMMM"] = str + between;

        if (ddlInvCat.SelectedIndex > 0 || txtSearchInvName.Text != "")
        {
            FilterByInv = (string)(SearchInventoryIds());
            if (FilterByInv == "")
            {
                ViewState["StrMMM"] = null;
                return dt;
            }
            else
            {
                ViewState["StrMMM"] = ViewState["StrMMM"].ToString() + FilterByInv;
            }
        }
        if (ddlParty.SelectedIndex > 0)
        {

            FilterByPartyId = " and SalesChallanMaster.PartyID='" + Convert.ToString(ddlParty.SelectedValue) + "'";
            if (FilterByPartyId == "")
            {
                ViewState["StrMMM"] = null;
                return dt;
            }
            else
            {
                ViewState["StrMMM"] = ViewState["StrMMM"].ToString() + FilterByPartyId;
            }
        }
        if (ddlPaymentType.SelectedIndex > 0)
        {

            FilterByPartyId = " and  PaymentMethodMaster.PaymentMethodID='" + Convert.ToString(ddlPaymentType.SelectedValue) + "'";
            if (FilterByPartyId == "")
            {
                ViewState["StrMMM"] = null;
                return dt;
            }
            else
            {
                ViewState["StrMMM"] = ViewState["StrMMM"].ToString() + FilterByPartyId;
            }
        }

        if (ddlTypeOfSale.SelectedIndex > 0)
        {
            String typefsale = " and EntryTypeMaster.Entry_Type_Name='" + ddlTypeOfSale.SelectedItem.Text + "' ";
            if (typefsale == "")
            {

                ViewState["StrMMM"] = null;
                return dt;
            }
            else
            {
                ViewState["StrMMM"] = ViewState["StrMMM"].ToString() + typefsale.ToString();
            }
        }


        if (ddlwarehouse.SelectedIndex > 0)
        {
            String FilterByWH = (string)(FilterByStoreLocation());
            if (FilterByWH == "")
            {

                ViewState["StrMMM"] = null;
                return dt;
            }
            else
            {
                ViewState["StrMMM"] = ViewState["StrMMM"].ToString() + FilterByWH;
            }
        }


        if (ddlSalesPerson.SelectedIndex > 0)
        {
            String FilterBySP = (string)(FilterBySalesPerson());
            if (FilterBySP == "")
            {

                ViewState["StrMMM"] = null;
                return dt;
            }
            else
            {
                ViewState["StrMMM"] = ViewState["StrMMM"].ToString() + FilterBySP;
            }
        }


        if (ddlpaymentstatus.SelectedIndex > 0)
        {
            String typefsale = "";
            if (ddlpaymentstatus.SelectedValue == "30")
            {
                typefsale = " and (StatusMaster.StatusId='" + ddlpaymentstatus.SelectedValue + "' or StatusMaster.StatusId='35')";
            }
            else
            {
                typefsale = "  and StatusMaster.StatusId='" + ddlpaymentstatus.SelectedValue + "'";
            }
            ViewState["StrMMM"] = ViewState["StrMMM"].ToString() + typefsale.ToString();
           
        }
        if (ddldeliverystatus.SelectedIndex > 0)
        {
            String typefsale = "";

            typefsale = "  and StatusMaster.StatusId='" + ddldeliverystatus.SelectedValue + "'";
          
            ViewState["StrMMM"] = ViewState["StrMMM"].ToString() + typefsale.ToString();

        }
        //if (ddlInvCat.SelectedIndex > 0)
        //{
        //    FilterByInv = (string)(SearchInventoryIds());


        //}
        //if (ddlParty.SelectedIndex > 0)
        //{
        //    FilterByPartyId = " and SalesOrderMaster.PartyId='" + Convert.ToInt32(ddlParty.SelectedValue) + "'";

        //}

        ////strMMM = str + between + FilterByInv + FilterByPartyId + " order by SalesOrderMaster.SalesOrderId";

        ////SqlCommand cmd = new SqlCommand(strMMM, con);
        ////SqlDataAdapter da = new SqlDataAdapter(cmd);

        ////da.Fill(dt);


        ////return dt;
        if (ViewState["StrMMM"] != null)
        {
            SqlCommand cmd = new SqlCommand(ViewState["StrMMM"].ToString() + " order by SalesOrderMaster.SalesOrderId", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
        }
        else
        {
            dt = null;
        }

        return dt;

    }





    private string GetDateBetweens()
    {
        String DtBtn = "";
        String DateBetween = "";
        if (rbtnlist.SelectedValue == "1")
        {
           
           // txttodate.Text = Convert.ToString(System.DateTime.Now.Date.ToShortDateString());

            DateBetween = (string)BetweenByFromdateTodate();

        }
        else if (rbtnlist.SelectedValue == "2")
        {
            DateBetween = (string)BetweenByPeriod();
        }
        else
        {
            DateBetween = (string)BetweenByYearMonth();
        }


        if (DtBtn.Length > 0)
        {
            DateBetween = DtBtn.ToString();
        }
        return DateBetween;


    }
    private string BetweenByFromdateTodate()
    {

        string str = "  between '" + txtfromdate.Text + "' AND '" + txttodate.Text + "'";// + //2009-4-30' " +

        return str;
    }
    private string BetweenByPeriod()
    {
        //date between you should use  date first and earlier date lateafter
        string Today, Yesterday, ThisYear;
        Today = Convert.ToString(System.DateTime.Today.ToShortDateString());
        Yesterday = Convert.ToString(System.DateTime.Today.AddDays(-1).ToShortDateString());
        ThisYear = Convert.ToString(System.DateTime.Today.Year.ToString());

        //-------------------this week start...............
        DateTime d1, d2, d3, d4, d5, d6, d7;
        DateTime weekstart, weekend;
        d1 = Convert.ToDateTime(System.DateTime.Today.ToShortDateString());
        d2 = Convert.ToDateTime(System.DateTime.Today.AddDays(-1).ToShortDateString());
        d3 = Convert.ToDateTime(System.DateTime.Today.AddDays(-2).ToShortDateString());
        d4 = Convert.ToDateTime(System.DateTime.Today.AddDays(-3).ToShortDateString());
        d5 = Convert.ToDateTime(System.DateTime.Today.AddDays(-4).ToShortDateString());
        d6 = Convert.ToDateTime(System.DateTime.Today.AddDays(-5).ToShortDateString());
        d7 = Convert.ToDateTime(System.DateTime.Today.AddDays(-6).ToShortDateString());
        string ThisWeek = (System.DateTime.Today.DayOfWeek.ToString());
        if (ThisWeek.ToString() == "Monday")
        {
            weekstart = d1;
            weekend = weekstart.Date.AddDays(+6);
        }
        else if (Convert.ToString(ThisWeek) == "Tuesday")
        {
            weekstart = d2;
            weekend = weekstart.Date.AddDays(+6);
        }
        else if (ThisWeek.ToString() == "Wednesday")
        {
            weekstart = d3;
            weekend = weekstart.Date.AddDays(+6);
        }
        else if (ThisWeek.ToString() == "Thursday")
        {
            weekstart = d4;
            weekend = weekstart.Date.AddDays(+6);
        }
        else if (ThisWeek.ToString() == "Friday")
        {
            weekstart = d5;
            weekend = weekstart.Date.AddDays(+6);
        }
        else if (ThisWeek.ToString() == "Saturday")
        {
            weekstart = d6;
            weekend = weekstart.Date.AddDays(+6);

        }
        else
        {
            weekstart = d7;
            weekend = weekstart.Date.AddDays(+6);
        }
        string thisweekstart = weekstart.ToString();
        string thisweekend = weekend.ToString();

        //.................this week duration end.....................

        ///--------------------last week duration ....

        DateTime d17, d8, d9, d10, d11, d12, d13;
        DateTime lastweekstart, lastweekend;
        d17 = Convert.ToDateTime(System.DateTime.Today.AddDays(-7).ToShortDateString());
        d8 = Convert.ToDateTime(System.DateTime.Today.AddDays(-8).ToShortDateString());
        d9 = Convert.ToDateTime(System.DateTime.Today.AddDays(-9).ToShortDateString());
        d10 = Convert.ToDateTime(System.DateTime.Today.AddDays(-10).ToShortDateString());
        d11 = Convert.ToDateTime(System.DateTime.Today.AddDays(-11).ToShortDateString());
        d12 = Convert.ToDateTime(System.DateTime.Today.AddDays(-12).ToShortDateString());
        d13 = Convert.ToDateTime(System.DateTime.Today.AddDays(-13).ToShortDateString());
        string thisday = (System.DateTime.Today.DayOfWeek.ToString());
        if (thisday.ToString() == "Monday")
        {
            lastweekstart = d17;
            lastweekend = lastweekstart.Date.AddDays(+6);
        }
        else if (Convert.ToString(thisday) == "Tuesday")
        {
            lastweekstart = d8;
            lastweekend = lastweekstart.Date.AddDays(+6);
        }
        else if (thisday.ToString() == "Wednesday")
        {
            lastweekstart = d9;
            lastweekend = lastweekstart.Date.AddDays(+6);
        }
        else if (thisday.ToString() == "Thursday")
        {
            lastweekstart = d10;
            lastweekend = lastweekstart.Date.AddDays(+6);
        }
        else if (thisday.ToString() == "Friday")
        {
            lastweekstart = d11;
            lastweekend = lastweekstart.Date.AddDays(+6);
        }
        else if (thisday.ToString() == "Saturday")
        {
            lastweekstart = d12;
            lastweekend = lastweekstart.Date.AddDays(+6);

        }
        else
        {
            lastweekstart = d13;
            lastweekend = lastweekstart.Date.AddDays(+6);
        }
        string lastweekstartdate = lastweekstart.ToString();
        string lastweekenddate = lastweekend.ToString();
        //---------------lastweek duration end.................

        //        Today
        //2	Yesterday
        //3	ThisWeek
        //4	LastWeek
        //5	ThisMonth
        //6	LastMonth
        //7	ThisQuarter
        //8	LastQuarter
        //9	ThisYear
        //10Last Year
        //------------------this month period-----------------

        DateTime thismonthstart = Convert.ToDateTime(System.DateTime.Now.Month.ToString() + "/1/" + System.DateTime.Now.Year.ToString());
        string thismonthstartdate = thismonthstart.ToShortDateString();
        string thismonthenddate = Today.ToString();
        //------------------this month period end................



        //-----------------last month period start ---------------

        string lastmonthNumber = "12";
        int yearforlastmont12 = Convert.ToInt32(ThisYear);
        int lastmonthno = Convert.ToInt32(thismonthstart.Month.ToString()) - 1;
        if (lastmonthno.ToString() == "0")
        {
            yearforlastmont12 = Convert.ToInt32(ThisYear) - 1;
        }
        else
        {
            lastmonthNumber = Convert.ToString(lastmonthno.ToString());
        }

        //int lastmonthno = Convert.ToInt32(thismonthstart.Month.ToString()) - 1;
        //string lastmonthNumber = Convert.ToString(lastmonthno.ToString());
        DateTime lastmonth = Convert.ToDateTime(lastmonthNumber.ToString() + "/1/" + yearforlastmont12.ToString());
        string lastmonthstart = lastmonth.ToShortDateString();
        string lastmonthend = "";

        if (lastmonthNumber == "1" || lastmonthNumber == "3" || lastmonthNumber == "5" || lastmonthNumber == "7" || lastmonthNumber == "8" || lastmonthNumber == "10" || lastmonthNumber == "12")
        {
            lastmonthend = lastmonthNumber + "/31/" + yearforlastmont12.ToString();
        }
        else if (lastmonthNumber == "4" || lastmonthNumber == "6" || lastmonthNumber == "9" || lastmonthNumber == "11")
        {
            lastmonthend = lastmonthNumber + "/30/" + yearforlastmont12.ToString();
        }
        else
        {
            if (System.DateTime.IsLeapYear(Convert.ToInt32(ThisYear.ToString())))
            {
                lastmonthend = lastmonthNumber + "/29/" + yearforlastmont12.ToString();
            }
            else
            {
                lastmonthend = lastmonthNumber + "/28/" + yearforlastmont12.ToString();
            }
        }

        string lastmonthstartdate = lastmonthstart.ToString();
        string lastmonthenddate = lastmonthend.ToString();


        //-----------------last month period end -----------------------

        //--------------------this quater period start ----------------

        int thisqtr = Convert.ToInt32(thismonthstart.AddMonths(-2).Month.ToString());
        string thisqtrNumber = Convert.ToString(thisqtr.ToString());
        int lstyear = Convert.ToInt32(ThisYear);
        if (thisqtrNumber.ToString() == "11" || thisqtrNumber.ToString() == "12")
        {
            lstyear = Convert.ToInt32(ThisYear) - 1;

        }
        DateTime thisquater = Convert.ToDateTime(thisqtrNumber.ToString() + "/1/" + lstyear.ToString());
        string thisquaterstart = thisquater.ToShortDateString();
        string thisquaterend = "";

        if (thisqtrNumber == "1" || thisqtrNumber == "3" || thisqtrNumber == "5" || thisqtrNumber == "7" || thisqtrNumber == "8" || thisqtrNumber == "10" || thisqtrNumber == "12")
        {
            thisquaterend = thisqtrNumber + "/31/" + ThisYear.ToString();
        }
        else if (thisqtrNumber == "4" || thisqtrNumber == "6" || thisqtrNumber == "9" || thisqtrNumber == "11")
        {
            thisquaterend = thisqtrNumber + "/30/" + ThisYear.ToString();
        }
        else
        {
            if (System.DateTime.IsLeapYear(Convert.ToInt32(ThisYear.ToString())))
            {
                thisquaterend = thisqtrNumber + "/29/" + ThisYear.ToString();
            }
            else
            {
                thisquaterend = thisqtrNumber + "/28/" + ThisYear.ToString();
            }
        }

        string thisquaterstartdate = thisquaterstart.ToString();
        string thisquaterenddate = thismonthenddate.ToString();

        // --------------this quater period end ------------------------

        // --------------last quater period start----------------------

        int lastqtr = Convert.ToInt32(thismonthstart.AddMonths(-5).Month.ToString());// -5;
        string lastqtrNumber = Convert.ToString(lastqtr.ToString());
        int lastqater3 = Convert.ToInt32(thismonthstart.AddMonths(-3).Month.ToString());
        //DateTime lastqater3 = Convert.ToDateTime(System.DateTime.Now.AddMonths(-3).Month.ToString());
        string lasterquater3 = lastqater3.ToString();
        int lstyreee = Convert.ToInt32(ThisYear);
        if (lasterquater3.ToString() == "10" || lasterquater3.ToString() == "11" || lasterquater3.ToString() == "12")
        {
            lstyreee = Convert.ToInt32(ThisYear) - 1;
        }
        DateTime lastquater = Convert.ToDateTime(lastqtrNumber.ToString() + "/1/" + lstyreee.ToString());
        string lastquaterstart = lastquater.ToShortDateString();
        string lastquaterend = "";

        if (lasterquater3 == "1" || lasterquater3 == "3" || lasterquater3 == "5" || lasterquater3 == "7" || lasterquater3 == "8" || lasterquater3 == "10" || lasterquater3 == "12")
        {
            lastquaterend = lasterquater3 + "/31/" + lstyreee.ToString();
        }
        else if (lasterquater3 == "4" || lasterquater3 == "6" || lasterquater3 == "9" || lasterquater3 == "11")
        {
            lastquaterend = lasterquater3 + "/30/" + lstyreee.ToString();
        }
        else
        {
            if (System.DateTime.IsLeapYear(Convert.ToInt32(lstyreee.ToString())))
            {
                lastquaterend = lasterquater3 + "/29/" + lstyreee.ToString();
            }
            else
            {
                lastquaterend = lasterquater3 + "/28/" + lstyreee.ToString();
            }
        }

        string lastquaterstartdate = lastquaterstart.ToString();
        string lastquaterenddate = lastquaterend.ToString();

        //--------------last quater period end-------------------------

        //--------------this year period start----------------------
        DateTime thisyearstart = Convert.ToDateTime("1/1/" + ThisYear.ToString());
        DateTime thisyearend = Convert.ToDateTime("12/31/" + ThisYear.ToString());

        string thisyearstartdate = thisyearstart.ToShortDateString();
        string thisyearenddate = thisyearend.ToShortDateString();

        //---------------this year period end-------------------
        //--------------this year period start----------------------
        DateTime lastyearstart = Convert.ToDateTime("1/1/" + System.DateTime.Today.AddYears(-1).Year.ToString());
        DateTime lastyearend = Convert.ToDateTime("12/31/" + System.DateTime.Today.AddYears(-1).Year.ToString());

        string lastyearstartdate = lastyearstart.ToShortDateString();
        string lastyearenddate = lastyearend.ToShortDateString();



        //---------------this year period end-------------------

        string periodstartdate = "";
        string periodenddate = "";

        if (ddlperiod.SelectedItem.Text == "Today")
        {
            periodstartdate = Today.ToString();
            string str1 = "Select  Convert(varchar,FirstYearStartDate,101)  from CompanyMaster where Compid = '"+compid+"'";
            SqlCommand cmd = new SqlCommand(str1, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToDateTime(periodstartdate) < Convert.ToDateTime(dt.Rows[0][0].ToString()))
                {
                    lblstartdate.Text = dt.Rows[0][0].ToString();
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    ModalPopupExtender3.Show();
                    //txtfromdate.Text = dt.Rows[0][0].ToString();
                    periodenddate = Today.ToString();

                }


                else
                {
                    periodstartdate = Today.ToString();
                    periodenddate = Today.ToString();
                }
            }
        }
        else if (ddlperiod.SelectedItem.Text == "Yesterday")
        {
            periodstartdate = Yesterday.ToString();
            string str1 = "select Convert(nvarchar,StartDate,101)   as StartDate,EndDate from [ReportPeriod] where Compid = '" + compid + "' and Active='1'  and Whid='"+ddlwarehouse.SelectedValue+"'";
            SqlCommand cmd = new SqlCommand(str1, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToDateTime(periodstartdate) < Convert.ToDateTime(dt.Rows[0][0].ToString()))
                {
                    lblstartdate.Text = dt.Rows[0][0].ToString();
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                   ModalPopupExtender3.Show();
                    //txtfromdate.Text = dt.Rows[0][0].ToString();
                    periodenddate = Today.ToString();

                }


                else
                {
                    periodstartdate = Yesterday.ToString();
                    periodenddate = Yesterday.ToString();
                }
            }
        }
        else if (ddlperiod.SelectedItem.Text == "CurrentWeek")
        {
            periodstartdate = thisweekstart.ToString();
            string str1 = "select Convert(nvarchar,StartDate,101)   as StartDate,EndDate from [ReportPeriod] where Compid = '" + compid + "' and Active='1'  and Whid='"+ddlwarehouse.SelectedValue+"'";
            SqlCommand cmd = new SqlCommand(str1, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToDateTime(periodstartdate) < Convert.ToDateTime(dt.Rows[0][0].ToString()))
                {
                    lblstartdate.Text = dt.Rows[0][0].ToString();
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    ModalPopupExtender3.Show();
                    //txtfromdate.Text = dt.Rows[0][0].ToString();
                    periodenddate = Today.ToString();

                }


                else
                {
                    periodstartdate = thisweekstart.ToString();
                    periodenddate = thisweekend.ToString();
                }
            }
        }
        else if (ddlperiod.SelectedItem.Text == "LastWeek")
        {
            periodstartdate = lastweekstartdate.ToString();
            string str1 = "select Convert(nvarchar,StartDate,101)   as StartDate,EndDate from [ReportPeriod] where Compid = '" + compid + "' and Active='1'  and Whid='"+ddlwarehouse.SelectedValue+"'";
            SqlCommand cmd = new SqlCommand(str1, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToDateTime(periodstartdate) < Convert.ToDateTime(dt.Rows[0][0].ToString()))
                {
                    lblstartdate.Text = dt.Rows[0][0].ToString();
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    ModalPopupExtender3.Show();

                    //txtfromdate.Text = dt.Rows[0][0].ToString();
                    periodenddate = Today.ToString();
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                }


                else
                {
                    periodstartdate = lastweekstartdate.ToString();
                    periodenddate = Today.ToString();
                }
            }
        }
        else if (ddlperiod.SelectedItem.Text == "CurrentMonth")
        {
            periodstartdate = thismonthstart.ToString();
            string str1 = "select Convert(nvarchar,StartDate,101)   as StartDate,EndDate from [ReportPeriod] where Compid = '" + compid + "' and Active='1'  and Whid='"+ddlwarehouse.SelectedValue+"'";
            SqlCommand cmd = new SqlCommand(str1, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToDateTime(periodstartdate) < Convert.ToDateTime(dt.Rows[0][0].ToString()))
                {
                    lblstartdate.Text = dt.Rows[0][0].ToString();
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    ModalPopupExtender3.Show();
                    //txtfromdate.Text = dt.Rows[0][0].ToString();
                    periodenddate = Today.ToString();

                }


                else
                {
                    periodstartdate = thismonthstart.ToString();
                    periodenddate = Today.ToString();
                }
            }
        }
        else if (ddlperiod.SelectedItem.Text == "LastMonth")
        {
            periodstartdate = lastmonthstartdate.ToString();
            string str1 = "select Convert(nvarchar,StartDate,101)   as StartDate,EndDate from [ReportPeriod] where Compid = '" + compid + "' and Active='1'  and Whid='"+ddlwarehouse.SelectedValue+"'";
            SqlCommand cmd = new SqlCommand(str1, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToDateTime(periodstartdate) < Convert.ToDateTime(dt.Rows[0][0].ToString()))
                {
                    lblstartdate.Text = dt.Rows[0][0].ToString();
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    ModalPopupExtender3.Show();
                    //txtfromdate.Text = dt.Rows[0][0].ToString();
                    periodenddate = Today.ToString();

                }


                else
                {
                    periodstartdate = lastmonthstartdate.ToString();
                    periodenddate = lastmonthenddate.ToString();
                }
            }


        }
        else if (ddlperiod.SelectedItem.Text == "CurrentQuater")
        {
            periodstartdate = thisquaterstartdate.ToString();
            string str1 = "select Convert(nvarchar,StartDate,101)   as StartDate,EndDate from [ReportPeriod] where Compid = '" + compid + "' and Active='1'  and Whid='"+ddlwarehouse.SelectedValue+"'";
            SqlCommand cmd = new SqlCommand(str1, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToDateTime(periodstartdate) < Convert.ToDateTime(dt.Rows[0][0].ToString()))
                {
                    lblstartdate.Text = dt.Rows[0][0].ToString();
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    ModalPopupExtender3.Show();
                    //txtfromdate.Text = dt.Rows[0][0].ToString();
                    periodenddate = Today.ToString();

                }


                else
                {
                    periodstartdate = thisquaterstartdate.ToString();
                    periodenddate = thisquaterenddate.ToString();
                }
            }


        }
        else if (ddlperiod.SelectedItem.Text == "LastQuater")
        {
            periodstartdate = lastquaterstartdate.ToString();
            string str1 = "select Convert(nvarchar,StartDate,101)   as StartDate,EndDate from [ReportPeriod] where Compid = '" + compid + "' and Active='1'  and Whid='"+ddlwarehouse.SelectedValue+"'";
            SqlCommand cmd = new SqlCommand(str1, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToDateTime(periodstartdate) < Convert.ToDateTime(dt.Rows[0][0].ToString()))
                {
                    lblstartdate.Text = dt.Rows[0][0].ToString();
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    ModalPopupExtender3.Show();
                    //txtfromdate.Text = dt.Rows[0][0].ToString();
                    periodenddate = Today.ToString();

                }


                else
                {
                    periodstartdate = lastquaterstartdate.ToString();
                    periodenddate = lastquaterenddate.ToString();
                }
            }


        }

        else if (ddlperiod.SelectedItem.Text == "CurrentYear")
        {
            periodstartdate = thisyearstartdate.ToString();
            string str1 = "select Convert(nvarchar,StartDate,101)   as StartDate,EndDate from [ReportPeriod] where Compid = '" + compid + "' and Active='1'  and Whid='"+ddlwarehouse.SelectedValue+"'";
            SqlCommand cmd = new SqlCommand(str1, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToDateTime(periodstartdate) < Convert.ToDateTime(dt.Rows[0][0].ToString()))
                {
                    lblstartdate.Text = dt.Rows[0][0].ToString();
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    ModalPopupExtender3.Show();
                    //txtfromdate.Text = dt.Rows[0][0].ToString();
                    periodenddate = Today.ToString();

                }


                else
                {
                    periodstartdate = thisyearstartdate.ToString();
                    periodenddate = thisyearenddate.ToString();
                }
            }


        }
        else if (ddlperiod.SelectedItem.Text == "LastYear")
        {
            periodstartdate = lastyearstartdate.ToString();
            string str1 = "select Convert(nvarchar,StartDate,101)   as StartDate,EndDate from [ReportPeriod] where Compid = '" + compid + "' and Active='1'  and Whid='"+ddlwarehouse.SelectedValue+"'";
            SqlCommand cmd = new SqlCommand(str1, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToDateTime(periodstartdate) < Convert.ToDateTime(dt.Rows[0][0].ToString()))
                {
                    lblstartdate.Text = dt.Rows[0][0].ToString();
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    ModalPopupExtender3.Show();
                    //txtfromdate.Text = dt.Rows[0][0].ToString();
                    periodenddate = Today.ToString();

                }


                else
                {
                    periodstartdate = lastyearstartdate.ToString();
                    periodenddate = lastyearenddate.ToString();
                }
            }


        }
        else if (ddlperiod.SelectedItem.Text == "All")
        {
            string str1 = "select Convert(nvarchar,StartDate,101)   as StartDate,EndDate from [ReportPeriod] where Compid = '" + compid + "' and Active='1'  and Whid='"+ddlwarehouse.SelectedValue+"'";
            SqlCommand cmd = new SqlCommand(str1, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                periodstartdate = dt.Rows[0][0].ToString();
            }
            string str122 = "select Convert(nvarchar,StartDate,101)   as StartDate,EndDate from [ReportPeriod] where Compid = '" + compid + "' and Active='1'  and Whid='"+ddlwarehouse.SelectedValue+"'";
            SqlCommand cmd2 = new SqlCommand(str122, con);
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            if (dt2.Rows.Count > 0)
            {
                if (Convert.ToDateTime(periodstartdate) < Convert.ToDateTime(dt2.Rows[0][0].ToString()))
                {
                    lblstartdate.Text = dt2.Rows[0][0].ToString();
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    ModalPopupExtender3.Show();
                    periodenddate = Today.ToString();

                    //txtfromdate.Text = dt.Rows[0][0].ToString();

                }


                else
                {
                    periodstartdate = "1/1/1900";
                    periodenddate = Today.ToString();

                }
            }
        }


        DateTime sd = Convert.ToDateTime(periodstartdate.ToString());
        DateTime ed = Convert.ToDateTime(periodenddate.ToString());
        string str = "  Between '" + sd.ToShortDateString() + "' AND '" + ed.ToShortDateString() + "'"; // + //2009-4-30' " +



        return str;
        //string periodstartdate = "";
        //string periodenddate = "";

        //if (ddlperiod.SelectedItem.Text == "Today")
        //{
        //    periodstartdate = Today.ToString();
        //    periodenddate = Today.ToString();
        //}
        //else if (ddlperiod.SelectedItem.Text == "Yesterday")
        //{
        //    periodstartdate = Yesterday.ToString();
        //    periodenddate = Yesterday.ToString();
        //}
        //else if (ddlperiod.SelectedItem.Text == "CurrentWeek")
        //{
        //    periodstartdate = thisweekstart.ToString();
        //    periodenddate = thisweekend.ToString();
        //}
        //else if (ddlperiod.SelectedItem.Text == "LastWeek")
        //{

        //    periodstartdate = lastweekstartdate.ToString();
        //    periodenddate = Today.ToString();
        //}
        //else if (ddlperiod.SelectedItem.Text == "CurrentMonth")
        //{

        //    periodstartdate = thismonthstart.ToString();
        //    periodenddate = Today.ToString();
        //}
        //else if (ddlperiod.SelectedItem.Text == "LastMonth")
        //{

        //    periodstartdate = lastmonthstartdate.ToString();
        //    periodenddate = lastmonthenddate.ToString();


        //}
        //else if (ddlperiod.SelectedItem.Text == "CurrentQuater")
        //{

        //    periodstartdate = thisquaterstartdate.ToString();
        //    periodenddate = thisquaterenddate.ToString();


        //}
        //else if (ddlperiod.SelectedItem.Text == "LastQuater")
        //{

        //    periodstartdate = lastquaterstartdate.ToString();
        //    periodenddate = lastquaterenddate.ToString();


        //}

        //else if (ddlperiod.SelectedItem.Text == "CurrentYear")
        //{

        //    periodstartdate = thisyearstartdate.ToString();
        //    periodenddate = thisyearenddate.ToString();


        //}
        //else if (ddlperiod.SelectedItem.Text == "LastYear")
        //{

        //    periodstartdate = lastyearstartdate.ToString();
        //    periodenddate = lastyearenddate.ToString();


        //}
        //else
        //{
        //    periodstartdate = "1/1/1900";
        //    periodenddate = System.DateTime.Today.AddDays(1).ToShortDateString();
        //}


        //DateTime sd = Convert.ToDateTime(periodstartdate.ToString());
        //DateTime ed = Convert.ToDateTime(periodenddate.ToString());
        //string str = "  Between '" + sd.ToShortDateString() + "' AND '" + ed.ToShortDateString() + "'"; // + //2009-4-30' " +



        //return str;
        //string str = "  Between '" + periodstartdate.ToString() + "' AND '" + periodenddate.ToString() + "'"; // + //2009-4-30' " +



        //return str;
    }
    private string BetweenByYearMonth()
    {
        string currentyear = System.DateTime.Today.Year.ToString();
        string currentmonth = System.DateTime.Today.Month.ToString();

        string selectedyear = Convert.ToString(ddlyear.SelectedItem.Text);
        string selectedmonth = Convert.ToString(ddlmonth.SelectedItem.Text);
        string selectedmonthNumber = "";
        string ThisYear = Convert.ToString(System.DateTime.Today.Year.ToString());

        //DateTime datebyyearmonth = Convert.ToDateTime(selectedmonthNumber.ToString() + "/1/" + selectedyear.ToString()).ToShortDateString();

        //string datebyyearmonthdate = datebyyearmonth.ToShortDateString();

        string yearmonthstartdate = "";
        string yearmonthenddate = "";

        string month = "";
        string year = "";


        if (ddlmonth.SelectedItem.Text == "All" && ddlyear.SelectedItem.Text == "All")
        {
            //month = "1";
            //year = System.DateTime.Now.Year.ToString();

            yearmonthstartdate = "1/1/1900";
            yearmonthenddate = System.DateTime.Today.ToShortDateString();
        }
        else if (ddlmonth.SelectedItem.Text != "All" && ddlyear.SelectedItem.Text == "All")
        {
            switch (selectedmonth)
            {
                case "January":
                    selectedmonthNumber = "1";
                    break;

                case "Feb":
                    selectedmonthNumber = "2";
                    break;
                case "March":
                    selectedmonthNumber = "3";
                    break;
                case "April":
                    selectedmonthNumber = "4";
                    break;
                case "May":
                    selectedmonthNumber = "5";
                    break;
                case "June":
                    selectedmonthNumber = "6";
                    break;
                case "July":
                    selectedmonthNumber = "7";
                    break;
                case "August":
                    selectedmonthNumber = "8";
                    break;
                case "September":
                    selectedmonthNumber = "9";
                    break;
                case "Octomber":
                    selectedmonthNumber = "10";
                    break;
                case "November":
                    selectedmonthNumber = "11";
                    break;
                case "December":
                    selectedmonthNumber = "12";
                    break;
            }
            string days = "";
            if (selectedmonthNumber == "1" || selectedmonthNumber == "3" || selectedmonthNumber == "5" || selectedmonthNumber == "7" || selectedmonthNumber == "9" || selectedmonthNumber == "11")
            {
                days = "31";
            }
            else if (selectedmonthNumber == "4" || selectedmonthNumber == "6" || selectedmonthNumber == "8" || selectedmonthNumber == "10" || selectedmonthNumber == "12")
            {
                days = "30";
            }
            else
            {
                if (System.DateTime.IsLeapYear(Convert.ToInt32(ThisYear.ToString())))
                {
                    days = "29";
                }
                else
                {
                    days = "28";
                }
            }
            //DateTime selectedmonthonly = Convert.ToDateTime(selectedmonthNumber.ToString() + "/1/" + "1900").ToShortDateString();

            month = selectedmonthNumber.ToString();
            year = System.DateTime.Now.Year.ToString();
            //day = days;


            yearmonthstartdate = Convert.ToDateTime(month + "/1/" + currentyear.ToString()).ToShortDateString();
            yearmonthenddate = Convert.ToDateTime(month + "/" + days + "/" + currentyear.ToString()).ToShortDateString();


        }
        else if (ddlmonth.SelectedItem.Text == "All" && ddlyear.SelectedItem.Text != "All")
        {
            //month = Convert.ToString(System.DateTime.Now.Month);
            year = selectedyear.ToString();


            yearmonthstartdate = Convert.ToDateTime("1" + "/1/" + year.ToString()).ToShortDateString();
            yearmonthenddate = Convert.ToDateTime("12" + "/31/" + year.ToString()).ToShortDateString();



        }
        else
        {


            switch (selectedmonth)
            {
                case "January":
                    selectedmonthNumber = "1";
                    break;

                case "Feb":
                    selectedmonthNumber = "2";
                    break;
                case "March":
                    selectedmonthNumber = "3";
                    break;
                case "April":
                    selectedmonthNumber = "4";
                    break;
                case "May":
                    selectedmonthNumber = "5";
                    break;
                case "June":
                    selectedmonthNumber = "6";
                    break;
                case "July":
                    selectedmonthNumber = "7";
                    break;
                case "August":
                    selectedmonthNumber = "8";
                    break;
                case "September":
                    selectedmonthNumber = "9";
                    break;
                case "Octomber":
                    selectedmonthNumber = "10";
                    break;
                case "November":
                    selectedmonthNumber = "11";
                    break;
                case "December":
                    selectedmonthNumber = "12";
                    break;
            }
            string days = "";
            if (selectedmonthNumber == "1" || selectedmonthNumber == "3" || selectedmonthNumber == "5" || selectedmonthNumber == "7" || selectedmonthNumber == "8" || selectedmonthNumber == "10" || selectedmonthNumber == "12")
            {
                days = "31";
            }
            else if (selectedmonthNumber == "4" || selectedmonthNumber == "6" || selectedmonthNumber == "9" || selectedmonthNumber == "11")
            {
                days = "30";
            }
            else
            {
                if (System.DateTime.IsLeapYear(Convert.ToInt32(ThisYear.ToString())))
                {
                    days = "29";
                }
                else
                {
                    days = "28";
                }
            }
            //DateTime selectedmonthonly = Convert.ToDateTime(selectedmonthNumber.ToString() + "/1/" + "1900").ToShortDateString();

            month = selectedmonthNumber.ToString();
            year = selectedyear.ToString();

            yearmonthstartdate = Convert.ToDateTime(month + "/1/" + year.ToString()).ToShortDateString();
            yearmonthenddate = Convert.ToDateTime(month + "/" + days + "/" + year.ToString()).ToShortDateString();


        }



        //DateTime yearmonth = Convert.ToDateTime(month.ToString() + "/1/" + year.ToString()).ToShortDateString();
        string YearMonthStartfrom = yearmonthstartdate.ToString();
        string YearMonthEndTo = yearmonthenddate.ToString();

        string str = "  Between '" + YearMonthStartfrom.ToString() + "' AND '" + YearMonthEndTo.ToString() + "'"; // + //2009-4-30' " +



        return str;
    }
    private string FilterByStoreLocation()
    {
          string strForStorelocation = " SELECT     InventoryWarehouseMasterTbl.WareHouseId, SalesOrderDetail.SalesOrderMasterId "+
             " FROM         SalesOrderDetail LEFT OUTER JOIN "+
                     " InventoryWarehouseMasterTbl ON SalesOrderDetail.InventoryWHM_Id = InventoryWarehouseMasterTbl.InventoryWarehouseMasterId left outer join WareHouseMaster on InventoryWarehouseMasterTbl.WareHouseId =  WareHouseMaster.WareHouseId where WareHouseMaster.comid = '"+compid+"' ";



        string wherestorelocation = "";

        if (ddlwarehouse.SelectedIndex > 0)
        {
            wherestorelocation = " and InventoryWarehouseMasterTbl.WareHouseId = '" + Convert.ToInt32(ddlwarehouse.SelectedValue) + "' ORDER BY SalesOrderDetail.SalesOrderMasterId DESC ";

        }

        string mixstr = strForStorelocation + wherestorelocation;

        SqlCommand cmdSL = new SqlCommand(mixstr, con);
        SqlDataAdapter adpSL = new SqlDataAdapter(cmdSL);
        DataTable dtSL = new DataTable();
        adpSL.Fill(dtSL);

        //DataTable dtDCids = new DataTable();
        string strSLsearchId = "";
        if (dtSL.Rows.Count > 0)
        {

            string strSLId = "";
            string strAllSLIds = "";
            string strtempSL = "";
            foreach (DataRow dtrrr12 in dtSL.Rows)
            {
                strSLId = dtrrr12["SalesOrderMasterId"].ToString();
                strAllSLIds = strSLId + "," + strAllSLIds;
                strtempSL = strAllSLIds.Substring(0, (strAllSLIds.Length - 1));
            }

            strSLsearchId = " and SalesChallanMaster.RefSalesOrderId in (" + strtempSL + ") ";

        }
        String SLIds = "";
        if (strSLsearchId.Length > 0)
        {
            SLIds = strSLsearchId.ToString();
        }
        else
        {
            SLIds = "";
        }
        return SLIds;


    }



    private string FilterBySalesPerson()
    {


        string strForSalesPerson = "SELECT     SalesChallanMaster.SalesChallanMasterId, SalesChallanMaster.PartyID, CONVERT(nvarchar(10), SalesChallanMaster.SalesChallanDatetime, 101)AS SalesChallanDatetime, SalesChallanMaster.ShipToAddress, SalesChallanMaster.BillToAddress, SalesChallanMaster.EntryTypeId, TranctionMaster.Tranction_Master_Id, TranctionMaster.Date, TranctionMaster.EntryNumber, TranctionMaster.UserId, SalesChallanMoreInfo.InvoiceNo, SalesChallanMoreInfo.RecieptNo, CASE WHEN (Party_master.Compname IS NULL) THEN '--' ELSE Party_master.Compname END AS PartyName, CASE WHEN (SalesOrderMaster.GrossAmount IS NULL) THEN '--' ELSE SalesOrderMaster.GrossAmount END AS GrossAmount, CASE WHEN (SalesOrderMaster.OtherCharges IS NULL) THEN '--' ELSE SalesOrderMaster.OtherCharges END AS Tax1, CASE WHEN (SalesOrderMaster.HandlingCharges IS NULL) THEN '--' ELSE SalesOrderMaster.HandlingCharges END AS HandlingCharges, SalesChallanMoreInfo.ShippingCost, CASE WHEN (SalesOrderDetail.Amount IS NULL) THEN '--' ELSE SalesOrderDetail.Amount END AS Amount, EntryTypeMaster.Entry_Type_Name, SalesOrderMaster.SalesManId, SalesOrderMaster.PaymentsTerms, SalesOrderMaster.SalesOrderNo, SalesOrderMaster.SalesOrderId, CONVERT(nvarchar(10), SalesOrderMaster.SalesOrderDate, 101) AS SalesOrderDate, PaymentMethodMaster.PaymentMethodName,PaymentMethodMaster.PaymentMethodID FROM         Party_master RIGHT OUTER JOIN SalesChallanMaster ON Party_master.PartyID = SalesChallanMaster.PartyID LEFT OUTER JOIN SalesChallanMoreInfo ON SalesChallanMaster.SalesChallanMasterId = SalesChallanMoreInfo.SalesChallanMasterId RIGHT OUTER JOIN SalesOrderDetail RIGHT OUTER JOIN TransactionMasterMoreInfo RIGHT OUTER JOIN PaymentMethodMaster INNER JOIN SalesOrderMaster ON PaymentMethodMaster.PaymentMethodID = SalesOrderMaster.PaymentsTerms ON TransactionMasterMoreInfo.SalesOrderId = SalesOrderMaster.SalesOrderId ON SalesOrderDetail.SalesOrderId = SalesOrderMaster.SalesOrderId ON SalesChallanMaster.RefSalesOrderId = SalesOrderMaster.SalesOrderId LEFT OUTER JOIN EntryTypeMaster RIGHT OUTER JOIN TranctionMaster ON EntryTypeMaster.Entry_Type_Id = TranctionMaster.EntryTypeId ON TransactionMasterMoreInfo.Tranction_Master_Id = TranctionMaster.Tranction_Master_Id where TranctionMaster.Whid = '" + ddlwarehouse.SelectedValue + "'";
        // "WHERE     (InventoryWarehouseMasterTbl.WareHouseId = '1') ";



        string whereSalesPerson = "";

        if (ddlSalesPerson.SelectedIndex > 0)
        {
            whereSalesPerson = " and SalesManID='" + Convert.ToInt32(ddlSalesPerson.SelectedValue) + "' ";

        }

        string mixstr = strForSalesPerson + whereSalesPerson;

        SqlCommand cmdSL = new SqlCommand(mixstr, con);
        SqlDataAdapter adpSL = new SqlDataAdapter(cmdSL);
        DataTable dtSL = new DataTable();
        adpSL.Fill(dtSL);

        //DataTable dtDCids = new DataTable();
        string strSLsearchId = "";
        if (dtSL.Rows.Count > 0)
        {

            string strSLId = "";
            string strAllSLIds = "";
            string strtempSL = "";
            foreach (DataRow dtrrr12 in dtSL.Rows)
            {
                strSLId = dtrrr12["SalesOrderNo"].ToString();
                strAllSLIds = strSLId + "," + strAllSLIds;
                strtempSL = strAllSLIds.Substring(0, (strAllSLIds.Length - 1));
            }

            strSLsearchId = " and SalesOrderMaster.SalesOrderNo in (" + strtempSL + ") ";

        }
        String SLIds = "";
        if (strSLsearchId.Length > 0)
        {
            SLIds = strSLsearchId.ToString();
        }
        else
        {
            SLIds = "";
        }
        return SLIds;


    }

    private string SearchInventoryIds()
    {
       
        string strinv = " SELECT     SalesOrderMaster.PartyId, SalesOrderMaster.SalesOrderDate, SalesOrderMaster.ShippingCharges, SalesOrderMaster.HandlingCharges, " +
                   "   SalesOrderMasterDetail.PartyDiscountAmount, SalesOrderMasterDetail.OrderValueDiscountAmount, SalesOrderMaster.GrossAmount, SalesOrderMaster.SalesOrderId,  " +
                   "   CASE WHEN (Party_master.Compname IS NULL) THEN '--' ELSE Party_master.Compname END AS Compname,  " +
                   "   InventoryWarehouseMasterTbl.InventoryWarehouseMasterId, InventoryMaster.InventoryMasterId, InventoryMaster.Name,  " +
                   "   InventorySubCategoryMaster.InventorySubCatId, InventorySubCategoryMaster.InventorySubCatName, InventoruSubSubCategory.InventorySubSubId,  " +
                   "   InventoruSubSubCategory.InventorySubSubName, InventoryCategoryMaster.InventeroyCatId, InventoryCategoryMaster.InventoryCatName " +
                   " FROM         Party_master RIGHT OUTER JOIN " +
                   "   InventoryMaster INNER JOIN " +
                   "   InventoruSubSubCategory ON InventoryMaster.InventorySubSubId = InventoruSubSubCategory.InventorySubSubId INNER JOIN " +
                   "   InventorySubCategoryMaster ON InventoruSubSubCategory.InventorySubCatID = InventorySubCategoryMaster.InventorySubCatId INNER JOIN " +
                   "   InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId INNER JOIN " +
                   "   InventoryWarehouseMasterTbl ON InventoryMaster.InventoryMasterId = InventoryWarehouseMasterTbl.InventoryMasterId RIGHT OUTER JOIN " +
                   "   SalesOrderDetail ON InventoryWarehouseMasterTbl.InventoryWarehouseMasterId = SalesOrderDetail.InventoryWHM_Id RIGHT OUTER JOIN " +
                   "   SalesOrderMaster INNER JOIN " +
                   "   SalesOrderMasterDetail ON SalesOrderMaster.SalesOrderId = SalesOrderMasterDetail.SalesOrderId ON  " +
                   "   SalesOrderDetail.SalesOrderId = SalesOrderMaster.SalesOrderId ON Party_master.PartyID = SalesOrderMaster.PartyId left outer join InventoryMasterMNC on InventoryMaster.InventoryMasterId = InventoryMasterMNC.Inventorymasterid where  InventoryMasterMNC.copid = '" + Session["comid"] + "' ";



        string strInvId = "";
        string strInvsubsubCatId = "";
        string strInvsubcatid = "";
        string strInvCatid = "";
        string strInvBySerchId = "";
        if (txtSearchInvName.Text.Length <= 0)
        {
            if (ddlInvCat.SelectedIndex > 0)
            {
                if (ddlInvSubCat.SelectedIndex > 0)
                {
                    if (ddlInvSubSubCat.SelectedIndex > 0)
                    {
                        if (ddlInvName.SelectedIndex > 0)
                        {
                            strInvId = "and  InventoryMaster.InventoryMasterId=" + Convert.ToInt32(ddlInvName.SelectedValue) + " ";

                        }
                        else
                        {
                            strInvsubsubCatId = "and InventoruSubSubCategory.InventorySubSubId=" + Convert.ToInt32(ddlInvSubSubCat.SelectedValue) + "";
                        }
                    }
                    else
                    {
                        strInvsubcatid = "and InventorySubCategoryMaster.InventorySubCatId = " + Convert.ToInt32(ddlInvSubCat.SelectedValue) + " ";

                    }

                }
                else
                {
                    strInvCatid = "and InventoryCategoryMaster.InventeroyCatId =" + Convert.ToInt32(ddlInvCat.SelectedValue) + " ";

                    //strInvId = "where  InventoryMaster.InventoryMasterId=" + Convert.ToInt32(ddlInvName.SelectedValue) + " ";

                }
            }
            else
            {
                //strInvCatid = "where InventoryCategoryMaster.InventeroyCatId =" + Convert.ToInt32(ddlInvCat.SelectedValue) + " ";

            }

            // string mainString = strinv + strInvId + strInvsubsubCatId + strInvsubcatid + strInvCatid + "order by SalesChallanMaster.RefSalesOrderId ";

        }
        else
        {
            string str23 = " SELECT     InventoryMaster.InventoryMasterId, InventoryMaster.InventorySubSubId as InventorySubSubId, InventoryMaster.Name, InventoryMaster.ProductNo, InventoruSubSubCategory.InventorySubSubName, " +
       " InventoryDetails.Description, InventoryDetails.QtyOnHand, InventoryDetails.Rate, InventoryDetails.Weight, InventoruSubSubCategory.InventorySubSubId,  " +
       " InventorySizeMaster.Width, InventorySizeMaster.Height, InventorySizeMaster.length AS Length, InventoryBarcodeMaster.Barcode,  " +
       " InventoryLocationTbl.InventortyRackID, InventoryLocationTbl.ShelfNumber, InventoryLocationTbl.Position, InventoryMeasurementUnit.Unit,  " +
       " case when InventoryMeasurementUnit.UnitType is null then '1'  else InventoryMeasurementUnit.UnitType  end as  UnitType  " +
         " FROM      [InventoryWarehouseMasterTbl] inner join   InventoryMaster on InventoryMaster.InventoryMasterId= InventoryWarehouseMasterTbl.InventoryMasterId INNER JOIN " +
       " InventoryDetails ON InventoryMaster.InventoryDetailsId = InventoryDetails.Inventory_Details_Id INNER JOIN " +
       " InventoruSubSubCategory ON InventoryMaster.InventorySubSubId = InventoruSubSubCategory.InventorySubSubId LEFT OUTER JOIN " +
       " InventoryMeasurementUnit ON InventoryMaster.InventoryMasterId = InventoryMeasurementUnit.InventoryMasterId LEFT OUTER JOIN " +
       " InventoryLocationTbl ON InventoryMaster.InventoryMasterId = InventoryLocationTbl.InventoryWHM_Id LEFT OUTER JOIN " +
       " InventorySizeMaster ON InventoryMaster.InventoryMasterId = InventorySizeMaster.InventoryMasterId LEFT OUTER JOIN " +
      "  InventoryBarcodeMaster ON InventoryMaster.InventoryMasterId = InventoryBarcodeMaster.InventoryMaster_id left outer join InventoryMasterMNC on InventoryMaster.InventoryMasterId = InventoryMasterMNC.Inventorymasterid  " +
      " where     (InventoryMaster.Name like '%" + txtSearchInvName.Text.Replace("'", "''") + "%') and InventoryWarehouseMasterTbl.WareHouseId='" + ddlwarehouse.SelectedValue + "'";

            SqlCommand cmd23 = new SqlCommand(str23, con);
            SqlDataAdapter adp23 = new SqlDataAdapter(cmd23);
            DataTable dt23 = new DataTable();
            adp23.Fill(dt23);
            if (dt23.Rows.Count > 0)
            {
                string strId = "";
                string strInvAllIds = "";
                string strtemp = "";
                foreach (DataRow dtrrr in dt23.Rows)
                {
                    strId = dtrrr["InventoryMasterId"].ToString();
                    strInvAllIds = strId + "," + strInvAllIds;
                    strtemp = strInvAllIds.Substring(0, (strInvAllIds.Length - 1));
                }

                strInvBySerchId = " and InventoryMaster.InventoryMasterId in (" + strtemp + ") " + "order by SalesOrderMaster.SalesOrderId  ";
                //string mainstring = strinv + "  order by SalesChallanMaster.RefSalesOrderId ";
            }
            else
            {
                strinv = "";
            }
        }

        string mainString = strinv + strInvId + strInvsubsubCatId + strInvsubcatid + strInvCatid + strInvBySerchId;
          DataTable dtinv = new DataTable();
        if (mainString != "")
        {
            SqlCommand cmdinv = new SqlCommand(mainString, con);
            SqlDataAdapter adpinv = new SqlDataAdapter(cmdinv);
            
            adpinv.Fill(dtinv);
        }
        //DataTable dtDCids = new DataTable();
        string strDCsearchId = "";
        if (dtinv.Rows.Count > 0)
        {

            string strDCId = "";
            string strAllDCIds = "";
            string strtempDC = "";
            foreach (DataRow dtrrr12 in dtinv.Rows)
            {
                strDCId = dtrrr12["SalesOrderId"].ToString();
                strAllDCIds = strDCId + "," + strAllDCIds;
                strtempDC = strAllDCIds.Substring(0, (strAllDCIds.Length - 1));
            }

            strDCsearchId = " and SalesOrderMaster.SalesOrderId in (" + strtempDC + ") ";

        }
        String DCIds = "";
        if (strDCsearchId.Length > 0)
        {
            DCIds = strDCsearchId.ToString();
        }
        else
        {
            DCIds = "";
        }
        return DCIds;
    }
     protected void FillddlInvCat()
    {
        ddlInvCat.Items.Clear();
        if (ddlwarehouse.SelectedIndex > 0)
        {
            string strcat = "SELECT Distinct  InventoryCategoryMaster.InventeroyCatId,InventoryCategoryMaster.InventoryCatName FROM InventoryCategoryMaster inner join InventorySubCategoryMaster on InventorySubCategoryMaster.InventoryCategoryMasterId=InventoryCategoryMaster.InventeroyCatId inner join InventoruSubSubCategory on InventoruSubSubCategory.InventorySubCatID=InventorySubCategoryMaster.InventorySubCatId inner join InventoryMaster on InventoryMaster.InventorySubSubId=InventoruSubSubCategory.InventorySubSubId inner join InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.InventoryMasterId=InventoryMaster.InventoryMasterId inner join WareHouseMaster on WareHouseMaster.WareHouseId=InventoryWarehouseMasterTbl.WareHouseId  WHERE InventoryWarehouseMasterTbl.WareHouseId ='" + ddlwarehouse.SelectedValue + "' and [InventoryCategoryMaster].[Activestatus]='1' order by InventoryCatName";
            //string strcat = "SELECT InventeroyCatId,InventoryCatName  FROM  InventoryCategoryMaster where "+
            //    " compid='"+compid +"' order by InventoryCatName";
            SqlCommand cmdcat = new SqlCommand(strcat, con);
            SqlDataAdapter adpcat = new SqlDataAdapter(cmdcat);
            DataTable dtcat = new DataTable();
            adpcat.Fill(dtcat);

            ddlInvCat.DataSource = dtcat;
            ddlInvCat.DataTextField = "InventoryCatName";
            ddlInvCat.DataValueField = "InventeroyCatId";
            ddlInvCat.DataBind();
            ddlInvCat.Items.Insert(0, "All");
            ddlInvCat.Items[0].Value = "0";
        }
        else
        {
            ddlInvCat.Items.Insert(0, "All");
            ddlInvCat.Items[0].Value = "0";
        }
        

    }
    protected void ddlInvCat_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlInvCat.DataSource = null;
        //ddlInvCat.DataBind();
        ddlInvSubCat.DataSource = null;
        ddlInvSubCat.DataBind();
        ddlInvSubCat.Items.Clear();
        ddlInvSubSubCat.DataSource = null;
        ddlInvSubSubCat.DataBind();
        ddlInvSubSubCat.Items.Clear();
        ddlInvName.DataSource = null;
        ddlInvName.DataBind();
        ddlInvName.Items.Clear();

        if (Convert.ToInt32(ddlInvCat.SelectedIndex) > 0)
        {
            string strsubcat = "SELECT InventorySubCatId  ,InventorySubCatName ,InventoryCategoryMasterId  FROM InventorySubCategoryMaster " +
                            " where InventoryCategoryMasterId = " + Convert.ToInt32(ddlInvCat.SelectedValue) + " and InventorySubCategoryMaster.[Activestatus]='1' order by InventorySubCatName ";
            SqlCommand cmdsubcat = new SqlCommand(strsubcat, con);
            SqlDataAdapter adpsubcat = new SqlDataAdapter(cmdsubcat);
            DataTable dtsubcat = new DataTable();
            adpsubcat.Fill(dtsubcat);

            ddlInvSubCat.DataSource = dtsubcat;

            ddlInvSubCat.DataTextField = "InventorySubCatName";
            ddlInvSubCat.DataValueField = "InventorySubCatId";
            ddlInvSubCat.DataBind();

        }
        else
        {
            ddlInvSubCat.DataSource = null;


            ddlInvSubCat.DataBind();
        }
        ddlInvSubCat.Items.Insert(0, "All");
        ddlInvSubCat.Items[0].Value = "0";
        ddlInvSubSubCat.DataSource = null;
        ddlInvSubSubCat.DataBind();
        ddlInvSubCat_SelectedIndexChanged(sender, e);
        //ddlInvSubCat.AutoPostBack = true;
    }
    protected void ddlInvSubCat_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlInvSubCat.DataSource = null;
        //ddlInvSubCat.DataBind();
        ddlInvSubSubCat.DataSource = null;
        ddlInvSubSubCat.DataBind();
        ddlInvSubSubCat.Items.Clear();
        ddlInvName.DataSource = null;
        ddlInvName.DataBind();
        ddlInvName.Items.Clear();


        if (Convert.ToInt32(ddlInvSubCat.SelectedIndex) > 0)
        {
            string strsubsubcat = "SELECT InventorySubSubId ,InventorySubSubName  ,InventorySubCatID  FROM  InventoruSubSubCategory " +
                            " where InventorySubCatID=" + Convert.ToInt32(ddlInvSubCat.SelectedValue) + " [Activestatus]='1' order by InventorySubSubName ";
            SqlCommand cmdsubsubcat = new SqlCommand(strsubsubcat, con);
            SqlDataAdapter adpsubsubcat = new SqlDataAdapter(cmdsubsubcat);
            DataTable dtsubsubcat = new DataTable();
            adpsubsubcat.Fill(dtsubsubcat);

            ddlInvSubSubCat.DataSource = dtsubsubcat;
            ddlInvSubSubCat.DataTextField = "InventorySubSubName";
            ddlInvSubSubCat.DataValueField = "InventorySubSubId";
            ddlInvSubSubCat.DataBind();

        }
        else
        {
            ddlInvSubSubCat.DataSource = null;
            ddlInvSubSubCat.DataBind();
        }

        ddlInvSubSubCat.Items.Insert(0, "All");
        ddlInvSubSubCat.Items[0].Value = "0";

        ddlInvName.DataSource = null;
        ddlInvName.DataBind();

        ddlInvSubSubCat_SelectedIndexChanged(sender, e);
        // ddlInvSubSubCat.AutoPostBack = true;


    }
    protected void ddlInvSubSubCat_SelectedIndexChanged(object sender, EventArgs e)
    {

        //ddlInvSubSubCat.DataSource = null;
        //ddlInvSubSubCat.DataBind();
        ddlInvName.DataSource = null;
        ddlInvName.DataBind();
        ddlInvName.Items.Clear();
        if (Convert.ToInt32(ddlInvSubSubCat.SelectedIndex) > 0)
        {
            //string strinvname = "SELECT InventoryMaster.InventoryMasterId ,InventoryMaster.Name ,InventoryMaster.InventoryDetailsId ,InventoryMaster.InventorySubSubId   ,InventoryMaster.ProductNo ,InventoryMaster.InventoryTypeId  FROM InventoryMaster left outer join InventoryMasterMNC on InventoryMaster.InventoryMasterId = InventoryMasterMNC.Inventorymasterid   " +
            //                " where InventorySubSubId= " + Convert.ToInt32(ddlInvSubSubCat.SelectedValue) + " and InventoryMasterMNC.copid = '" + compid + "' order by Name ";
            string strinvname = "SELECT InventoryMaster.InventoryMasterId ,InventoryMaster.Name ,InventoryMaster.InventoryDetailsId ,InventoryMaster.InventorySubSubId   ,InventoryMaster.ProductNo ,InventoryMaster.InventoryTypeId  FROM InventoryMaster left outer join InventoryMasterMNC on InventoryMaster.InventoryMasterId = InventoryMasterMNC.Inventorymasterid " +
                            " where InventorySubSubId= " + Convert.ToInt32(ddlInvSubSubCat.SelectedValue) + " and [InventoryMaster].[MasterActiveStatus]='1' order by Name ";
            SqlCommand cmdinvname = new SqlCommand(strinvname, con);
            SqlDataAdapter adpinvname = new SqlDataAdapter(cmdinvname);
            DataTable dtinvname = new DataTable();
            adpinvname.Fill(dtinvname);

            ddlInvName.DataSource = dtinvname;

            ddlInvName.DataTextField = "Name";
            ddlInvName.DataValueField = "InventoryMasterId";
            ddlInvName.DataBind();

        }
        else
        {
            ddlInvName.DataSource = null;
            ddlInvName.DataBind();
        }
        ddlInvName.Items.Insert(0, "All");
        ddlInvName.Items[0].Value = "0";
    }
    //protected void FillddlParty()
    protected void btnSelectColm_Click(object sender, EventArgs e)
    {
        pnlColumnSelect.Visible = true;
        
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
       // SelectDocumentforApproval();
        bindGrid();
    }


    public string sortOrder
    {
        get
        {
            if (ViewState["sortOrder"].ToString() == "desc")
            {
                ViewState["sortOrder"] = "asc";
            }
            else
            {
                ViewState["sortOrder"] = "desc";
            }

            return ViewState["sortOrder"].ToString();
        }
        set
        {
            ViewState["sortOrder"] = value;
        }
    }
    protected void HiddenButton_Click(object sender, EventArgs e)
    {

    }

    protected void ImgBtnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
             ViewState["orderid"] = lblSalesOrderNoFromGrid.Text;

             SqlCommand cmd12 = new SqlCommand("SELECT Tranction_Master_Temp_Id,Date, EntryNumber, EntryTypeId, UserId, Tranction_Amount FROM TranctionMaster_Temp WHERE salesorderid = '" + lblSalesOrderNoFromGrid.Text + "'and EntryTypeId='25'", con);
             SqlDataAdapter adp12 = new SqlDataAdapter(cmd12);
             DataSet ds12 = new DataSet();
             adp12.Fill(ds12);

             foreach (DataRow dr in ds12.Tables[0].Rows)
             {

                 string str123 = "insert into TranctionMaster(Date,EntryNumber,EntryTypeId,UserId,Tranction_Amount) " +
                " values('" + Convert.ToDateTime(dr["Date"]) + "','" + Convert.ToInt32(dr["EntryNumber"]) + "','" + Convert.ToInt32(dr["EntryTypeId"]) + "','" + ViewState["UseridFromGrid"].ToString() + "','" + Convert.ToDecimal(dr["Tranction_Amount"]) + "')";
                 SqlCommand cmd123 = new SqlCommand(str123, con);
                 con.Open();
                 cmd123.ExecuteNonQuery();
                 con.Close();

                 SqlCommand cmdd = new SqlCommand("select max(Tranction_Master_Id)as masterid from TranctionMaster", con);
                 SqlDataAdapter add = new SqlDataAdapter(cmdd);
                 DataSet dsdd = new DataSet();
                 add.Fill(dsdd);
                 int masterid = Convert.ToInt32(dsdd.Tables[0].Rows[0]["masterid"]);

                 //insert transaction master more info table

                 SqlCommand cmMore = new SqlCommand("insert into TransactionMasterMoreInfo(SalesOrderId,Tranction_Master_Id) values('" + lblSalesOrderNoFromGrid.Text + "','" + masterid + "')", con);
                 con.Open();
                 cmMore.ExecuteNonQuery();
                 con.Close();


                 SqlCommand cmds = new SqlCommand("select * from Tranction_Details_Temp where Tranction_Master_Temp_Id='" + Convert.ToInt32(dr["Tranction_Master_Temp_Id"]) + "'", con);
                 SqlDataAdapter ads = new SqlDataAdapter(cmds);
                 DataSet dss = new DataSet();
                 ads.Fill(dss);

                 foreach (DataRow dr2 in dss.Tables[0].Rows)
                 {
                     string str2 = "insert into Tranction_Details(AccountDebit,AccountCredit,AmountDebit,AmountCredit,Tranction_Master_Id,Memo,DateTimeOfTransaction,DiscEarn,DiscPaid)" +
                                   " values('" + Convert.ToInt32(dr2["AccountDebit"]) + "','" + Convert.ToInt32(dr2["AccountCredit"]) + "','" + Convert.ToDecimal(dr2["AmountDebit"]) + "','" + Convert.ToDecimal(dr2["AmountCredit"]) + "','" + masterid + "','" + dr2["Memo"] + "','" + Convert.ToDateTime(dr2["DateTimeOfTransaction"]) + "','0','0')";
                     SqlCommand cmd2 = new SqlCommand(str2, con);
                     con.Open();
                     cmd2.ExecuteNonQuery();
                     con.Close();
                 }
             }

           
                       







            
        }
        catch (Exception tr)
        {
            Label1.Visible = true;
            Label1.Text = "Error : " + tr.Message;

        }
    }
    protected void ImgBtnCancel_Click(object sender, ImageClickEventArgs e)
    {
        ddlOrderStatusForGrid.SelectedIndex = 0;
        ddlPaymentTypeForGrid.SelectedIndex = 0;
        txtPayAmtForGrid.Text = "";
        lblSalesOrderNoFromGrid.Text = "";
        lblSalesOrderNoFromGrid0.Text = "";
        ModalPopupExtender1.Hide();
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        ModalPopupExtender1.Hide();
    }
    protected void FillddlPaymentTypeForGrid()
    {
        string str1f = "SELECT     PaymentMethodID, PaymentMethodName, status "+
                      " FROM         PaymentMethodMaster where status=1";
        SqlCommand cmd1f = new SqlCommand(str1f, con);
        SqlDataAdapter adp1f = new SqlDataAdapter(cmd1f);
        DataTable dt1f = new DataTable();
        adp1f.Fill(dt1f);

        if (dt1f.Rows.Count > 0)
        {
            ddlPaymentTypeForGrid.DataSource = dt1f;
            ddlPaymentTypeForGrid.DataTextField = "PaymentMethodName";
            ddlPaymentTypeForGrid.DataValueField = "PaymentMethodID";
            ddlPaymentTypeForGrid.DataBind();

            ddlPaymentTypeForGrid.Items.Insert(0, "-Select-");
            ddlPaymentTypeForGrid.Items[0].Value = "0";

        }
        else
        {
            ddlPaymentTypeForGrid.Items.Insert(0, "-Select-");
            ddlPaymentTypeForGrid.Items[0].Value = "0";
        }
    }
    protected void FillddlOrderStatusForGrid()
    {
        //string str2f = "SELECT     StatusMaster.StatusName, StatusMaster.StatusId, StatusCategory.StatusCategoryMasterId, StatusCategory.StatusCategory " +
        //               " FROM         StatusMaster LEFT OUTER JOIN " +
        //              " StatusCategory ON StatusMaster.StatusCategoryMasterId = StatusCategory.StatusCategoryMasterId where StatusCategory='Sales Order - Payment' and StatusCategory.compid = '"+compid+"' ";
        string str2f = "SELECT     StatusMaster.StatusName, StatusMaster.StatusId, StatusCategory.StatusCategoryMasterId, StatusCategory.StatusCategory " +
                       " FROM         StatusMaster LEFT OUTER JOIN " +
                      " StatusCategory ON StatusMaster.StatusCategoryMasterId = StatusCategory.StatusCategoryMasterId where StatusCategory.compid = '" + compid + "' ";
        SqlCommand cmd2f = new SqlCommand(str2f, con);
        SqlDataAdapter adp2f = new SqlDataAdapter(cmd2f);
        DataTable dt2f = new DataTable();
        adp2f.Fill(dt2f);

        if (dt2f.Rows.Count > 0)
        {
            ddlOrderStatusForGrid.DataSource = dt2f;
            ddlOrderStatusForGrid.DataTextField = "StatusName";
            ddlOrderStatusForGrid.DataValueField = "StatusId";
            ddlOrderStatusForGrid.DataBind();

            ddlOrderStatusForGrid.Items.Insert(0, "-Select-");
            ddlOrderStatusForGrid.Items[0].Value = "0";

        }
        else
        {
            ddlOrderStatusForGrid.Items.Insert(0, "-Select-");
            ddlOrderStatusForGrid.Items[0].Value = "0";
        }
    }
    protected void ImgBtnSu5_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string statusmid = "";
            string str587 = "        SELECT    StatusId, StatusName, StatusCategoryMasterId FROM         StatusMaster left outer join StatusCategory on StatusMaster.StatusCategoryMasterId = StatusCategory.StatusCategoryMasterId " +
                            " WHERE     (StatusName = '" + ddlOrderStatusForGrid.SelectedItem.Text + "') and StatusCategory.compid = '" + compid + "' ";
            SqlCommand cmd587 = new SqlCommand(str587, con);
            SqlDataAdapter adp587 = new SqlDataAdapter(cmd587);
            DataTable ds587 = new DataTable();
            adp587.Fill(ds587);
            if (ds587.Rows.Count > 0)
            {
                statusmid = ds587.Rows[0]["StatusId"].ToString();
            }




            string str95 = "    INSERT INTO StatusControl (StatusMasterId,Datetime  ,UserMasterId ,SalesOrderId, note) " +
                            " VALUES ('" + statusmid + "','" + System.DateTime.Now.ToShortDateString() + "','" + Session["userid"] + "','" + lblSalesOrderNoFromGrid.Text + "','Register SalesOrder Entry')";
            SqlCommand cmd95 = new SqlCommand(str95, con);
            con.Open();
            cmd95.ExecuteNonQuery();
            con.Close();

            Label1.Visible = true;
            Label1.Text = "";
        }
        catch (Exception )
        {

        }
    }
    protected void ImageButton6_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void HiddenButton1_Click(object sender, EventArgs e)
    {

    }
    protected void ImgBtnSu6AddNote_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void FillGridForNotes()
    {

    }


    public string ChekRecord(string Str, string Delid)
    {
        SqlCommand cmdChek1 = new SqlCommand(Str, connection);
        SqlDataAdapter adpChek1 = new SqlDataAdapter(cmdChek1);
        DataTable dtChek1 = new DataTable();
        adpChek1.Fill(dtChek1);
        string i = "0";
        if (dtChek1.Rows.Count > 0)
        {
            i = dtChek1.Rows[0][Delid].ToString();
            return i;
        }
        return i;
    }
    protected DataTable select(string str)
    {
        SqlCommand cmd = new SqlCommand(str, connection);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);
        return dt;

    }
    protected void yes_Click(object sender, EventArgs e)
    {


        SqlCommand cmdsel = new SqlCommand("select * from SalesChallanTransaction where SalesChallanMasterId= '" + Convert.ToInt32(ViewState["ChNo"]) + "'", connection);

        SqlDataAdapter dtpsel = new SqlDataAdapter(cmdsel);
        DataTable dtsel = new DataTable();
        dtpsel.Fill(dtsel);
        con.Open();
        SqlTransaction transaction = con.BeginTransaction();
        try
        {
            //Delete Delivery Challan...
            string DelSalesChallanMster = "";
            string DelSalesChallanDetail = "";
            string DelSalesChallanMoreInf = "";
            string DelSalesChallanTranction = "";
         
            string TransactionMasterSalesChallanTbl = "";
            if (ViewState["ChNo"] != null)
            {


                foreach (DataRow dr in dtsel.Rows)
                {
                    DataTable dtoldqty = new DataTable();
                    dtoldqty = (DataTable)select("Select QtyOnHand from InventoryWarehouseMasterTbl where InventoryWarehouseMasterId='" + dr["inventoryWHM_Id"].ToString() + "' ");
                    if (dtoldqty.Rows.Count > 0)
                    {

                        double newqty = (Convert.ToDouble(dtoldqty.Rows[0]["QtyOnHand"].ToString()) + Convert.ToDouble(dr["Quantity"].ToString()));

                            SqlCommand cmdup = new SqlCommand("Update InventoryWarehouseMasterTbl set QtyOnHand='" + newqty + "' where InventoryWarehouseMasterId='" + dr["InventoryWHM_Id"].ToString() + "'", con);
                            cmdup.Transaction = transaction;
                          
                            cmdup.ExecuteNonQuery();
                          
                    }
                }
                DelSalesChallanMster = " delete SalesChallanMaster where SalesChallanMasterId='" + Convert.ToInt32(ViewState["ChNo"]) + "' ";


             
                    DelSalesChallanDetail = " Delete SalesChallanDetail where SalesChallanMasterId='" + Convert.ToInt32(ViewState["ChNo"]) + "' ";
              
              DelSalesChallanMoreInf = " Delete SalesChallanMoreInfo where SalesChallanMasterId='" + Convert.ToInt32(ViewState["ChNo"]) + "' ";
              
              DelSalesChallanTranction = " Delete SalesChallanTransaction where SalesChallanMasterId='" + Convert.ToInt32(ViewState["ChNo"]) + "' ";
              TransactionMasterSalesChallanTbl = " Delete TransactionMasterSalesChallanTbl where SalesChallanMasterId='" + Convert.ToInt32(ViewState["ChNo"]) + "' ";
              if (TransactionMasterSalesChallanTbl != "")
              {
                  SqlCommand cmd21 = new SqlCommand(TransactionMasterSalesChallanTbl, con);
                  cmd21.Transaction = transaction;
                  //con.Open();
                  cmd21.ExecuteNonQuery();
                  // con.Close();
              }
           if (DelSalesChallanMster != "")
                {
                    SqlCommand cmd2 = new SqlCommand(DelSalesChallanMster, con);
                    cmd2.Transaction = transaction;
                    //con.Open();
                    cmd2.ExecuteNonQuery();
                    // con.Close();
                }
                if (DelSalesChallanDetail != "")
                {
                    SqlCommand cmd3 = new SqlCommand(DelSalesChallanDetail, con);
                    cmd3.Transaction = transaction;
                    // con.Open();
                    cmd3.ExecuteNonQuery();
                    //con.Close();
                }
                if (DelSalesChallanMoreInf != "")
                {
                    SqlCommand cmd4 = new SqlCommand(DelSalesChallanMoreInf, con);
                    cmd4.Transaction = transaction;
                    // con.Open();
                    cmd4.ExecuteNonQuery();
                    // con.Close();
                }
                if (DelSalesChallanTranction != "")
                {
                    SqlCommand cmd5 = new SqlCommand(DelSalesChallanTranction, con);
                    cmd5.Transaction = transaction;
                    //con.Open();
                    cmd5.ExecuteNonQuery();
                    //con.Close();
                }
              
            }
            //delete Delivery Challna end

            //Delete Sales Invoice Start

            string DelTrasMasterForInv = "";
            string DelTrasDetailForInv = "";
            string DelPayAppln = "";
            string DelStatusControlForInv = "";
            string TransactionMasterMoreInfo = "";
            string TranctionMasterSuppliment = "";
            string ChekRec8 = "";
            string DelTrasMasterForInvAvgcost = "";
            if (ViewState["SONob"] != null)
            {
                if (ViewState["InNob"] != null)
                {
                    string strinb = "SELECT     TranctionMaster.EntryTypeId, TransactionMasterMoreInfo.SalesOrderId, TranctionMaster.Tranction_Master_Id, TranctionMaster.EntryNumber " +
                    "  FROM         TranctionMaster RIGHT OUTER JOIN " +
                     " TransactionMasterMoreInfo ON TranctionMaster.Tranction_Master_Id = TransactionMasterMoreInfo.Tranction_Master_Id " +
                     " WHERE (TranctionMaster.Tranction_Master_Id='"+ ViewState["tra"]+"') And (TranctionMaster.EntryNumber = '" + Convert.ToInt32(ViewState["InNob"]) + "') AND (TranctionMaster.Whid='" + ddlwarehouse.SelectedValue + "') ";
                    SqlCommand cmdinb = new SqlCommand(strinb, connection);
                    SqlDataAdapter adpinb = new SqlDataAdapter(cmdinb);
                    DataTable dtinb = new DataTable();
                    adpinb.Fill(dtinb);
                    if (dtinb.Rows.Count > 0)
                    {
                        DelTrasMasterForInv = " delete TranctionMaster where Tranction_Master_Id='" + Convert.ToInt32(dtinb.Rows[0]["Tranction_Master_Id"]) + "' ";
                        DelTrasDetailForInv = " delete Tranction_Details where Tranction_Master_Id='" + Convert.ToInt32(dtinb.Rows[0]["Tranction_Master_Id"]) + "' ";
                        TransactionMasterMoreInfo = " delete TransactionMasterMoreInfo where Tranction_Master_Id='" + Convert.ToInt32(dtinb.Rows[0]["Tranction_Master_Id"]) + "' ";
                        TranctionMasterSuppliment = " delete TranctionMasterSuppliment where Tranction_Master_Id='" + Convert.ToInt32(dtinb.Rows[0]["Tranction_Master_Id"]) + "' ";
                        DelTrasMasterForInvAvgcost = " delete InventoryWarehouseMasterAvgCostTbl where Tranction_Master_Id='" + Convert.ToInt32(dtinb.Rows[0]["Tranction_Master_Id"]) + "' ";
                      
                        string ChekPayAppnTb = " SELECT PaymentAppTblId,TranMIdAmtApplied,SalesOrderId  FROM PaymentApplicationTbl " +
                            " where TranMIdAmtApplied='" + Convert.ToInt32(dtinb.Rows[0]["Tranction_Master_Id"]) + "' and SalesOrderId='" + Convert.ToInt32(ViewState["SONob"]) + "' ";
                        SqlCommand cmdPayAppnTb = new SqlCommand(ChekPayAppnTb, connection);
                        SqlDataAdapter adpPayAppnTb = new SqlDataAdapter(cmdPayAppnTb);
                        DataTable dtPayAppnTb = new DataTable();
                        adpPayAppnTb.Fill(dtPayAppnTb);
                        if (dtPayAppnTb.Rows.Count > 0)
                        {
                            DelPayAppln = " Delete PaymentApplicationTbl where PaymentAppTblId='" + Convert.ToInt32(dtPayAppnTb.Rows[0]["PaymentAppTblId"]) + "'";
                        }

                      
                            DelStatusControlForInv = " Delete StatusControl where  TranctionMasterId='" + Convert.ToInt32(dtinb.Rows[0]["Tranction_Master_Id"]) + "' or  SalesChallanMasterId='" + Convert.ToInt32(ViewState["ChNo"]) + "' ";
                      

                        if (Convert.ToString(dtinb.Rows[0]["EntryTypeId"]) == "30")
                        {
                            //Delete Sales Order Main

                            string DelSalesOrdrMaster = "";
                            string DelSalesOrderDetail = "";
                            string DelSalesOrdrSuppliment = "";
                            string DelSalesOrderBillAdds = "";
                            string DelSalesOrderShipAdds = "";
                            string DelSalesOrderMasterDetail = "";
                      
                            string DelSaleOrdMasterPayOptn = "";


                          
                                DelSalesOrdrMaster = "  Delete   FROM SalesOrderMaster where SalesOrderId='" + Convert.ToInt32(ViewState["SONob"]) + "' ";
                              DelSalesOrdrSuppliment = " Delete SalesOrderSuppliment where SalesOrderMasterId='" + Convert.ToInt32(ViewState["SONob"]) + "' ";
                            DelSalesOrderDetail = " Delete SalesOrderDetail where SalesOrderMasterId='" + Convert.ToInt32(ViewState["SONob"]) + "' ";
                            DelSalesOrderBillAdds = " Delete BillingAddress where SalesOrderId='" + Convert.ToInt32(ViewState["SONob"]) + "' ";
                               DelSalesOrderShipAdds = " Delete ShippingAddress where SalesOrderId='" + Convert.ToInt32(ViewState["SONob"]) + "' ";
                           
                                DelSalesOrderMasterDetail = " Delete SalesOrderMasterDetail where SalesOrderId='" + Convert.ToInt32(ViewState["SONob"]) + "' ";
                           


                            ChekRec8 = " or SalesOrderId='" + Convert.ToInt32(ViewState["SONob"]) + "'";
                           
                          
                                DelSaleOrdMasterPayOptn = " Delete SalesOrderPaymentOption where SalesOrderId='" + Convert.ToInt32(ViewState["SONob"]) + "' ";
                              string  SalesOrderMasterTamp = " Delete SalesOrderMasterTamp where SalesOrderTempId='" + Convert.ToInt32(ViewState["SONob"]) + "' ";
                              string Salesorderbyavcost = " Delete Salesorderbyavcost where SalesOrderId='" + Convert.ToInt32(ViewState["SONob"]) + "' ";
                              string SalesOrderDetailTemp = " Delete SalesOrderDetailTemp where SalesOrderTempId='" + Convert.ToInt32(ViewState["SONob"]) + "' ";
                            //string DelSalesOrdrMaster = "";
                            if (DelSalesOrdrMaster != "")
                            {
                                SqlCommand cmd2qq1 = new SqlCommand(DelSalesOrdrMaster, con);
                                cmd2qq1.Transaction = transaction;
                                cmd2qq1.ExecuteNonQuery();
                             
                            }
                            //string DelSalesOrderDetail = "";
                            if (DelSalesOrderDetail != "")
                            {
                                SqlCommand cmd2qq2 = new SqlCommand(DelSalesOrderDetail, con);
                                cmd2qq2.Transaction = transaction;
                                cmd2qq2.ExecuteNonQuery();
                              
                            }

                            // string DelSalesOrdrSuppliment = "";
                            if (DelSalesOrdrSuppliment != "")
                            {
                                SqlCommand cmd2qq3 = new SqlCommand(DelSalesOrdrSuppliment, con);
                                cmd2qq3.Transaction = transaction;
                                cmd2qq3.ExecuteNonQuery();
                               
                            }


                            //string DelSalesOrderBillAdds = "";
                            if (DelSalesOrderBillAdds != "")
                            {
                                SqlCommand cmd2qq4 = new SqlCommand(DelSalesOrderBillAdds, con);
                                cmd2qq4.Transaction = transaction;
                                cmd2qq4.ExecuteNonQuery();
                             
                            }


                            //string DelSalesOrderShipAdds = "";
                            if (DelSalesOrderShipAdds != "")
                            {
                                SqlCommand cmd2qq5 = new SqlCommand(DelSalesOrderShipAdds, con);
                                cmd2qq5.Transaction = transaction;
                                cmd2qq5.ExecuteNonQuery();
                           
                            }

                            //string DelSalesOrderMasterDetail = "";
                            if (DelSalesOrderMasterDetail != "")
                            {
                                SqlCommand cmd2qq6 = new SqlCommand(DelSalesOrderMasterDetail, con);
                                cmd2qq6.Transaction = transaction;
                                cmd2qq6.ExecuteNonQuery();
                             
                            }

                          
                           
                            if (DelSaleOrdMasterPayOptn != "")
                            {
                                SqlCommand cmd2qq8 = new SqlCommand(DelSaleOrdMasterPayOptn, con);
                                cmd2qq8.Transaction = transaction;
                                cmd2qq8.ExecuteNonQuery();
                             
                            }
                            if (SalesOrderMasterTamp != "")
                            {
                                SqlCommand cm2qq8 = new SqlCommand(SalesOrderMasterTamp, con);
                                cm2qq8.Transaction = transaction;
                                cm2qq8.ExecuteNonQuery();

                            }
                            if (Salesorderbyavcost != "")
                            {
                                SqlCommand cm2qq87 = new SqlCommand(Salesorderbyavcost, con);
                                cm2qq87.Transaction = transaction;
                                cm2qq87.ExecuteNonQuery();

                            }
                            if (SalesOrderDetailTemp != "")
                            {
                                SqlCommand cm2qq87 = new SqlCommand(SalesOrderDetailTemp, con);
                                cm2qq87.Transaction = transaction;
                                cm2qq87.ExecuteNonQuery();

                            }
                        }
                    }
                    if (TranctionMasterSuppliment != "")
                    {
                        SqlCommand cmd491 = new SqlCommand(TranctionMasterSuppliment, con);
                        cmd491.Transaction = transaction;

                        cmd491.ExecuteNonQuery();

                    }
                    if (DelTrasMasterForInv != "")
                    {
                        SqlCommand cmd401 = new SqlCommand(DelTrasMasterForInv, con);
                        cmd401.Transaction = transaction;

                        cmd401.ExecuteNonQuery();

                    }

                    if (TransactionMasterMoreInfo != "")
                    {
                        SqlCommand cmd402 = new SqlCommand(TransactionMasterMoreInfo, con);
                        cmd402.Transaction = transaction;

                        cmd402.ExecuteNonQuery();

                    }
                    if (DelTrasDetailForInv != "")
                    {
                        SqlCommand cmd40 = new SqlCommand(DelTrasDetailForInv, con);
                        cmd40.Transaction = transaction;
                     
                        cmd40.ExecuteNonQuery();
                     
                    }
                    if (DelPayAppln != "")
                    {
                        SqlCommand cmd50 = new SqlCommand(DelPayAppln, con);
                        cmd50.Transaction = transaction;
                       
                        cmd50.ExecuteNonQuery();
                       
                    }
                    if (DelStatusControlForInv != "")
                    {
                        string ss=DelStatusControlForInv + ChekRec8;
                        SqlCommand cmd60 = new SqlCommand(ss, con);
                        cmd60.Transaction = transaction;
                       
                        cmd60.ExecuteNonQuery();
                       
                    }

                    if (DelTrasMasterForInvAvgcost != "")
                    {
                        string ssv = DelTrasMasterForInvAvgcost;
                        SqlCommand cmd60v = new SqlCommand(ssv, con);
                        cmd60v.Transaction = transaction;

                        cmd60v.ExecuteNonQuery();

                    }
                }



            }

           
            transaction.Commit();
           
          
        }
        catch (Exception jh)
        {
            transaction.Rollback();
            Label1.Visible = true;
            Label1.Text = " Error : " + jh.Message;
        }
        finally
        {
            con.Close();
            ModalPopupExtender1222.Hide();
            Button1_Click(sender, e);
            
            Label1.Visible = true;
            Label1.Text = "Record deleted sucessfully";
        }
    }
    protected void ddlFormTypeForAddNote_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        string strFillStatusCon = " SELECT     StatusCategory.StatusCategoryMasterId, StatusCategory.StatusCategory, StatusMaster.StatusName, StatusMaster.StatusId "+
                   "  FROM         StatusCategory RIGHT OUTER JOIN "+
                    "   StatusMaster ON StatusCategory.StatusCategoryMasterId = StatusMaster.StatusCategoryMasterId "+
                   "  WHERE     (StatusCategory.StatusCategoryMasterId IN (13,  15, 18, 29)) "+
                   " and StatusCategory.StatusCategoryMasterId='" + Convert.ToInt32(ddlFormTypeForAddNote.SelectedValue) + "' and compid = '"+compid+"' ";
        SqlCommand cmdStcn1 = new SqlCommand(strFillStatusCon, con);
        SqlDataAdapter adpStcn1 = new SqlDataAdapter(cmdStcn1);
        DataTable dtStcn1 = new DataTable();
        adpStcn1.Fill(dtStcn1);
        if (dtStcn1.Rows.Count > 0)
        {
            ddlStatusMasterForAddNotes.DataSource = dtStcn1;
            ddlStatusMasterForAddNotes.DataTextField = "StatusName";
            ddlStatusMasterForAddNotes.DataValueField = "StatusId";
            ddlStatusMasterForAddNotes.DataBind();
            //  ViewState["InNom"]   ViewState["PackSl"]
        }


        if (ddlFormTypeForAddNote.SelectedValue == "13")
        {
            lblFormNoForAddNotes.Text = "Sales Ord# " + lblSalesOrderNoFromGrid0.Text;
        }
     
        else if (ddlFormTypeForAddNote.SelectedValue == "15")
        {
            if (ViewState["InNom"] != null)
            {
                lblFormNoForAddNotes.Text = "Sales Inv# " + Convert.ToInt32(ViewState["InNom"]);
            }
        }
        else if (ddlFormTypeForAddNote.SelectedValue == "18")
        {
            if (ViewState["PackSl"] != null)
            {
                lblFormNoForAddNotes.Text = "Packin Slip# " + Convert.ToInt32(ViewState["PackSl"]);

            }
        }
        else if (ddlFormTypeForAddNote.SelectedValue == "29")
        {
            lblFormNoForAddNotes.Text = "Sales Ord# " + lblSalesOrderNoFromGrid0.Text;

        }
        else
        {
            lblFormNoForAddNotes.Text = "Not Available";
        }
        ModalPopupExtender2.Show();
    }
    protected void Button661_Click(object sender, EventArgs e)
    {

    }
    protected void ImageButton4123_Click(object sender, EventArgs e)
    {
        ModalPopupExtender4.Hide();
    }
    protected void ImageButtondsfdsfdsf123_Click(object sender, ImageClickEventArgs e)
    {
        ModalPopupExtender4.Hide();
    }


    protected void grd_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            grd.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            int docid = Convert.ToInt32(grd.Rows[grd.SelectedIndex].Cells[1].Text);
        }
    }
    protected void imgin_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["docid"] != null)
        {
            inserdocatt();
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
            int k = 0;
            ViewState["ttt"] = 0;
            foreach (GridViewRow gdr in GridView1.Rows)
            {
                CheckBox chk1 = (CheckBox)gdr.FindControl("chk");
                //int tid = Convert.ToInt32(gridDCregister.DataKeys[k].Value);
                ImageButton img = (ImageButton)gdr.FindControl("img1");
                int tid = Convert.ToInt32(img.CommandArgument);

                k = k + 1;
                if (chk1.Checked == true)
                {
                    string sqlselectr = "select * from AttachmentMaster where RelatedTableId='" + tid + "' and IfilecabinetDocId='" + Request.QueryString["docid"] + "'";
                    SqlDataAdapter adptr = new SqlDataAdapter(sqlselectr, con);
                    DataTable dtptr = new DataTable();
                    adptr.Fill(dtptr);
                    if (dtptr.Rows.Count <= 0)
                    {
                        if (Convert.ToInt32(ViewState["ttt"]) != tid)
                        {
                            ViewState["ttt"] = tid.ToString();
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
                            cmdi.Parameters["@RelatedTableId"].Value = tid.ToString();
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
                }
            }
            if (k > 0)
            {
                Label1.Text = "Record Inserted Successfully";
                Label1.Visible = true;
                bindGrid();
            }

        }
    }
    protected void ddlwarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillddlParty();
        FillddlSalePerson();
        FillddlInvCat();
        ddlInvCat_SelectedIndexChanged(sender,e);
        gridtax();
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (Button2.Text == "Printable Version")
        {
            //pnlgrid.ScrollBars = ScrollBars.None;
            //pnlgrid.Height = new Unit("100%");

            Button2.Text = "Hide Printable Version";
            btnPrint.Visible = true;

            if (GridView1.Columns[14].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[14].Visible = false;
            }
            if (GridView1.Columns[15].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                GridView1.Columns[15].Visible = false;
            }
            if (GridView1.Columns[16].Visible == true)
            {
                ViewState["Viewdd"] = "tt";
                GridView1.Columns[16].Visible = false;
            }

        }
        else
        {

            //pnlgrid.ScrollBars = ScrollBars.Vertical;
            //pnlgrid.Height = new Unit(250);

            Button2.Text = "Printable Version";
            btnPrint.Visible = false;
            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[14].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                GridView1.Columns[15].Visible = true;
            }
            if (ViewState["Viewdd"] != null)
            {
                GridView1.Columns[16].Visible = true;
            }


        }
    }
    protected void ImageButton12_Click(object sender, ImageClickEventArgs e)
    {
        ModalPopupExtender1222.Hide();
    }
}

