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

public partial class Add_Address_Master : System.Web.UI.Page
{

    SqlConnection con;
    Int32 mas;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        }
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();


        Page.Title = pg.getPageTitle(page);
        Label1.Text = "";
        if (!IsPostBack)
        {
            Pagecontrol.dypcontrol(Page, page);
            ViewState["sortOrder"] = "";
            Session["pnlM"] = "1";
            Session["pnl1"] = "13";

            //Session["pagename"] = "WizardCompanyWebsiteAddressMaster.aspx";
            //Session["pnl"] = "3";
            Label1.Visible = false;
            Fillddlcomapnyname();
            fillDdlAddressType();

            fillfilterstore();
            fillddlcountry();
            fillgriddata();
            ddlCompanyWebsiteMasterName.Enabled = true;
        }
    }
    protected void Fillddlcomapnyname()
    {
        string finalcompid = Session["Comid"].ToString();

        //string strcom = "select companymaster.CompanyName,CompanyWebsitMaster.Sitename,CompanyWebsitMaster.CompanyWebsiteMasterId from companymaster inner join CompanyWebsitMaster on CompanyWebsitMaster.companyid=companymaster.CompanyId  where  Compid='" + finalcompid + "' and CompanyWebsitMaster.Active='" + 1 + "' order by Sitename";

        DataTable ds = ClsStore.SelectStorename();
        ddlCompanyWebsiteMasterName.DataSource = ds;
        ddlCompanyWebsiteMasterName.DataTextField = "Name";
        ddlCompanyWebsiteMasterName.DataValueField = "WareHouseId";
        ddlCompanyWebsiteMasterName.DataBind();


        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlCompanyWebsiteMasterName.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }


    }
    protected void fillfilterstore()
    {

        DataTable ds = ClsStore.SelectStorename();
        ddlfilterstore.DataSource = ds;
        ddlfilterstore.DataTextField = "Name";
        ddlfilterstore.DataValueField = "WareHouseId";
        ddlfilterstore.DataBind();
        //ddlStore.Items.Insert(0, "Select");
        ddlfilterstore.Items.Insert(0, "All");
        ddlfilterstore.Items[0].Value = "0";




    }
    protected void fillddlcountry()
    {
        SqlCommand cmd = new SqlCommand("Select distinct CountryId,CountryName from CountryMaster order by CountryName", con);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ddlcountry.DataSource = dt;
            ddlcountry.DataTextField = "CountryName";
            ddlcountry.DataValueField = "CountryId";
            ddlcountry.DataBind();

        }
        ddlcountry.Items.Insert(0, "-Select-");
        ddlcountry.Items[0].Value = "0";
        ddlstate.Items.Insert(0, "-Select-");
        ddlstate.Items[0].Value = "0";
        ddlcity.Items.Insert(0, "-Select-");
        ddlcity.Items[0].Value = "0";
    }
    protected void fillddlstate()
    {
        ddlstate.Items.Clear();
        ddlcity.Items.Clear();
        SqlCommand cmd = new SqlCommand("Select * from StateMasterTbl where CountryId='" + ddlcountry.SelectedValue + "' order by StateName", con);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ddlstate.DataSource = dt;
            ddlstate.DataTextField = "StateName";
            ddlstate.DataValueField = "StateId";
            ddlstate.DataBind();
            // ddlstate.Items.Insert(0, "--Select--");
        }
        ddlstate.Items.Insert(0, "-Select-");
        ddlstate.Items[0].Value = "0";
        ddlcity.Items.Insert(0, "-Select-");
        ddlcity.Items[0].Value = "0";
    }
    protected void fillddlcity()
    {
        ddlcity.Items.Clear();
        SqlCommand cmd = new SqlCommand("Select * from CityMasterTbl where StateId='" + ddlstate.SelectedValue + "' order by CityName", con);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ddlcity.DataSource = dt;
            ddlcity.DataTextField = "CityName";
            ddlcity.DataValueField = "CityId";
            ddlcity.DataBind();
            // ddlcity.Items.Insert(0, "--Select--");
        }

        ddlcity.Items.Insert(0, "-Select-");
        ddlcity.Items[0].Value = "0";
    }
    protected void fillgriddata()
    {
        //    Radhika Chnages
        //        SqlCommand cmdgrid = new SqlCommand("SELECT  distinct   CompanyWebsiteAddressMaster.CompanyWebsiteAddressMasterId, CompanyWebsiteAddressMaster.Address1, " +
        //                      " CompanyWebsiteAddressMaster.Address2, CompanyWebsiteAddressMaster.City, CompanyWebsiteAddressMaster.State, " +
        //                      " CompanyWebsiteAddressMaster.Country, AddressTypeMaster.Name, CompanyWebsitMaster.Sitename, CountryMaster.CountryName, " +
        //                      " StateMasterTbl.StateName, CityMasterTbl.CityName " +
        //" FROM         CompanyWebsitMaster RIGHT OUTER JOIN " +
        //                      " StateMasterTbl RIGHT OUTER JOIN " +
        //                      " CompanyWebsiteAddressMaster LEFT OUTER JOIN " +
        //                      " CityMasterTbl ON CompanyWebsiteAddressMaster.City = CityMasterTbl.CityId ON " +
        //                      " StateMasterTbl.StateId = CompanyWebsiteAddressMaster.State LEFT OUTER JOIN " +
        //                      " CountryMaster ON CompanyWebsiteAddressMaster.Country = CountryMaster.CountryId ON " +
        //                      " CompanyWebsitMaster.CompanyWebsiteMasterId = CompanyWebsiteAddressMaster.CompanyWebsiteMasterId LEFT OUTER JOIN " +
        //                      " AddressTypeMaster ON CompanyWebsiteAddressMaster.AddressTypeMasterId = AddressTypeMaster.AddressTypeMasterId", con);
        //*************************

        //***************
        lblCompany.Text = Session["Cname"].ToString();
        //
        string finalcompid12 = Session["Comid"].ToString();
        string st1web = "";
        st1web = "SELECT  distinct   CompanyWebsiteAddressMaster.CompanyWebsiteAddressMasterId, CompanyWebsiteAddressMaster.Address1, " +
                     " CompanyWebsiteAddressMaster.Address2, CompanyWebsiteAddressMaster.City, CompanyWebsiteAddressMaster.State, " +
                     " CompanyWebsiteAddressMaster.Country, AddressTypeMaster.Name, WareHouseMaster.Name as Sitename, CountryMaster.CountryName, " +
                     " StateMasterTbl.StateName, CityMasterTbl.CityName " +
                     " FROM  WareHouseMaster RIGHT OUTER JOIN " +
                     " StateMasterTbl RIGHT OUTER JOIN " +
                     " CompanyWebsiteAddressMaster LEFT OUTER JOIN " +
                     " CityMasterTbl ON CompanyWebsiteAddressMaster.City = CityMasterTbl.CityId ON " +
                     " StateMasterTbl.StateId = CompanyWebsiteAddressMaster.State LEFT OUTER JOIN " +
                     " CountryMaster ON CompanyWebsiteAddressMaster.Country = CountryMaster.CountryId ON " +
                     " WareHouseMaster.WareHouseId = CompanyWebsiteAddressMaster.CompanyWebsiteMasterId LEFT OUTER JOIN " +
                     " AddressTypeMaster ON CompanyWebsiteAddressMaster.AddressTypeMasterId = AddressTypeMaster.AddressTypeMasterId  where  WareHouseMaster.Comid='" + finalcompid12 + "' and WareHouseMaster.Status='1'";
        if (ddlfilterstore.SelectedIndex > 0)
        {
            st1web += "and CompanyWebsiteAddressMaster.CompanyWebsiteMasterId = '" + ddlfilterstore.SelectedValue + "'";
        }
        st1web += " Order by Sitename, Name";
        lblBusiness.Text = ddlfilterstore.SelectedItem.Text;
        SqlCommand cmdgrid = new SqlCommand(st1web, con);
        SqlDataAdapter dtpgrid = new SqlDataAdapter(cmdgrid);
        DataTable dtgrid = new DataTable();
        dtpgrid.Fill(dtgrid);
        if (dtgrid.Rows.Count > 0)
        {
            GridView2.DataSource = dtgrid.DefaultView;
            DataView myDataView = new DataView();
            myDataView = dtgrid.DefaultView;
            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }
            GridView2.DataSource = myDataView;
            GridView2.DataBind();
        }
        else
        {
            GridView2.DataSource = null;
            GridView2.DataBind();
        }

    }
    protected void fillDdlAddressType()
    {
        string strcom23 = "SELECT  AddressTypeMasterId ,Name  FROM AddressTypeMaster where compid='" + Session["comid"] + "' order by Name";
        SqlCommand cmdcom23 = new SqlCommand(strcom23, con);
        SqlDataAdapter adpcom23 = new SqlDataAdapter(cmdcom23);
        DataTable dtcom23 = new DataTable();

        adpcom23.Fill(dtcom23);
        if (dtcom23.Rows.Count > 0)
        {

            ddlAddressTypeMasterName.DataSource = dtcom23;
            ddlAddressTypeMasterName.DataTextField = "Name";
            ddlAddressTypeMasterName.DataValueField = "AddressTypeMasterId";
            ddlAddressTypeMasterName.DataBind();

        }
        else
        {

        }
        // ddlAddressTypeMasterName.Items.Insert(0, "-Select-");
        // ddlAddressTypeMasterName.Items[0].Value = "0";
    }

    protected void fillgrid()
    {

        //  Radhika Chnages
        //string strgrd = "select  [CompanyWebsiteMasterId], [AddressTypeMasterId], [Address1], [Address2], [City], "+
        //    " [State], [Country], [Phone1], [Phone2], [TollFree1], [TollFree2], [Fax], [Zip], [Email], [LiveChatUrl],"+
        //    " [LogoUrl], [ContactPersonName], [ContactPersonDesignation] from CompanyWebsiteAddressMaster";


        //***************
        lblCompany.Text = Session["Cname"].ToString();
        //lblBusiness.Text = ddlfilterstore.SelectedItem.Text;

        string finalcompid123 = Session["Comid"].ToString();
        string strgrd = "";

        strgrd = "select  CompanyWebsiteAddressMasterId,CompanyWebsiteAddressMaster .CompanyWebsiteMasterId, [AddressTypeMasterId], [Address1], [Address2], [City], " +
                 " [State], [Country], [Phone1], [Phone2], [TollFree1], [TollFree2], [Fax], [Zip], [Email], [LiveChatUrl]," +
                 " [LogoUrl], [ContactPersonName], [ContactPersonDesignation] from CompanyWebsiteAddressMaster " +
                   "   inner join  CompanyWebsitMaster on CompanyWebsitMaster.CompanyWebsiteMasterId=CompanyWebsiteAddressMaster.CompanyWebsiteMasterId " +
                 " inner join  CompanyMaster on  CompanyMaster.CompanyId=CompanyWebsitMaster.CompanyId where  CompanyMaster.Compid='" + finalcompid123 + "' ";


        SqlCommand cmdgrd = new SqlCommand(strgrd, con);
        SqlDataAdapter adpgrd = new SqlDataAdapter(cmdgrd);
        DataTable dtgrd = new DataTable();
        adpgrd.Fill(dtgrd);
        if (dtgrd.Rows.Count > 0)
        {
            GridView1.DataSource = dtgrd.DefaultView;
            DataView myDataView = new DataView();
            myDataView = dtgrd.DefaultView;
            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }


            GridView1.DataSource = myDataView;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            GridView1.EmptyDataText = "No Record Found";
        }

    }

    protected void loadData()
    {
        //Radhika Chnages**
        //string strgrd1 = "select [CompanyWebsiteAddressMasterId], [CompanyWebsiteMasterId], [AddressTypeMasterId], [Address1], [Address2], [City], " +
        //   " [State], [Country], [Phone1], [Phone2], [TollFree1], [TollFree2], [Fax], [Zip], [Email], [LiveChatUrl]," +
        //   " [LogoUrl], [ContactPersonName], [ContactPersonDesignation] from CompanyWebsiteAddressMaster where CompanyWebsiteMasterId='" + ddlCompanyWebsiteMasterName.SelectedValue + "'   ";
        ////**************************


        string strgrd1 = "select [CompanyWebsiteAddressMasterId], [CompanyWebsiteMasterId], [AddressTypeMasterId], [Address1], [Address2], [City], " +
         " [State], [Country], [Phone1], [Phone2], [TollFree1], [TollFree2], [Fax], [Zip], [Email], [LiveChatUrl]," +
         " [LogoUrl], [ContactPersonName], [ContactPersonDesignation] from CompanyWebsiteAddressMaster " +
         "  where CompanyWebsiteMasterId='" + ddlCompanyWebsiteMasterName.SelectedValue + "' ";




        //string strgrd = "select  CompanyWebsiteAddressMaster .CompanyWebsiteMasterId, [AddressTypeMasterId], [Address1], [Address2], [City], " +
        //  " [State], [Country], [Phone1], [Phone2], [TollFree1], [TollFree2], [Fax], [Zip], [Email], [LiveChatUrl]," +
        //  " [LogoUrl], [ContactPersonName], [ContactPersonDesignation] from CompanyWebsiteAddressMaster    where CompanyWebsiteAddressMaster.CompanyWebsiteMasterId=CompanyWebsitMaster.CompanyWebsiteMasterId  " +
        // "    and     CompanyMaster.CompanyId=CompanyWebsitMaster.CompanyId and  CompanyMaster.Compid='" + finalcompid123 + "'";



        SqlCommand cmdgrd1 = new SqlCommand(strgrd1, con);
        SqlDataAdapter adpgrd1 = new SqlDataAdapter(cmdgrd1);
        DataTable dtgrd1 = new DataTable();
        adpgrd1.Fill(dtgrd1);
        if (dtgrd1.Rows.Count > 0)
        {
            //ViewState["DAta"] = dtgrd1;

            ddlAddressTypeMasterName.SelectedIndex = ddlAddressTypeMasterName.Items.IndexOf(ddlAddressTypeMasterName.Items.FindByValue(dtgrd1.Rows[0]["CompanyWebsiteMasterId"].ToString()));
            txtAddress1.Text = dtgrd1.Rows[0]["Address1"].ToString();
            txtAddress2.Text = dtgrd1.Rows[0]["Address2"].ToString();
            //txtCity.Text = dtgrd1.Rows[0]["City"].ToString();
            //tXtState.Text = dtgrd1.Rows[0]["State"].ToString();

            //TextCountry.Text = dtgrd1.Rows[0]["Country"].ToString();
            TextPhone1.Text = dtgrd1.Rows[0]["Phone1"].ToString();
            TextPhone2.Text = dtgrd1.Rows[0]["Phone2"].ToString();
            TextTollFree1.Text = dtgrd1.Rows[0]["TollFree1"].ToString();
            TextTollFree2.Text = dtgrd1.Rows[0]["TollFree2"].ToString();
            TextFax.Text = dtgrd1.Rows[0]["Fax"].ToString();
            TextZip.Text = dtgrd1.Rows[0]["Zip"].ToString();
            TextEmail.Text = dtgrd1.Rows[0]["Email"].ToString();
            TextLiveChatUrl.Text = dtgrd1.Rows[0]["LiveCharUrl"].ToString();
            TextContactPersonName.Text = dtgrd1.Rows[0]["ContactPersonName"].ToString();
            TextContactPersonDesignation.Text = dtgrd1.Rows[0]["ContactPersonDesignation"].ToString();

        }


    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        fillgrid();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        fillgrid();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        //DropDownList ddl1 = (DropDownList)(GridView1.Rows[GridView1.EditIndex].FindControl("DropDownList2"));
        //DropDownList ddl2 = (DropDownList)(GridView1.Rows[GridView1.EditIndex].FindControl("DropDownList4"));
        //TextBox t1=(TextBox)(GridView1.Rows[GridView1.EditIndex].FindControl("txtadd1"));
        //TextBox t11 = (TextBox)(GridView1.Rows[GridView1.EditIndex].FindControl("txtadd1"));
        //TextBox t12 = (TextBox)(GridView1.Rows[GridView1.EditIndex].FindControl("txtCity"));
        //TextBox t13 = (TextBox)(GridView1.Rows[GridView1.EditIndex].FindControl("txtState"));
        //TextBox t14 = (TextBox)(GridView1.Rows[GridView1.EditIndex].FindControl("txtPhone1"));
        //TextBox t15 = (TextBox)(GridView1.Rows[GridView1.EditIndex].FindControl("txtTollFree1"));
        //TextBox t16 = (TextBox)(GridView1.Rows[GridView1.EditIndex].FindControl("txtTollFree2"));
        //TextBox t17 = (TextBox)(GridView1.Rows[GridView1.EditIndex].FindControl("txtFax"));
        //TextBox t18 = (TextBox)(GridView1.Rows[GridView1.EditIndex].FindControl("txtZip"));
        //TextBox t19 = (TextBox)(GridView1.Rows[GridView1.EditIndex].FindControl("LiveChatUrl"));
        //TextBox t110 = (TextBox)(GridView1.Rows[GridView1.EditIndex].FindControl("txtContactPersonName"));
        //TextBox t111 = (TextBox)(GridView1.Rows[GridView1.EditIndex].FindControl("txtContactPersonDesignation"));


        ////
        ////    txtadd2
        ////    txtCity
        ////        txtState
        ////txtPhone1
        ////txtTollFree1
        ////    txtTollFree2
        ////txtFax
        ////    txtZip
        ////    txtEmail
        ////LiveChatUrl
        ////fileupload
        ////txtContactPersonName
        ////txtContactPersonDesignation


        //GridView1.EditIndex = -1;
        //fillgrid();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {


        GridView1.EditIndex = -1;
        fillgrid();
    }
    protected void ddlCompanyWebsiteMasterName_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlCompanyWebsiteMasterName.SelectedIndex > 0)
        //{

        //    string sg = "SELECT     CompanyMaster.CompanyId, CompanyMaster.CompanyName, CompanyMaster.AdminName, CompanyMaster.IRSNumber, CompanyMaster.StateTaxNumber, " +
        //            "      CompanyMaster.YearEndingDate, CompanyMaster.FirstYearStartDate, CompanyMaster.StartDateOfAccountYear, CompanyMaster.SaleReciept, " +
        //            "      CompanyMaster.AccountStatement, CompanyMaster.EstimentAndQuotation, CompanyMaster.VendorCentre, CompanyMaster.CustomerCentre, " +
        //            "      CompanyMaster.DocumentSystem, CompanyMaster.PayInvoicingPaymentTracking, CompanyMaster.CompanyLogo, CompanyMaster.BankAccount,  " +
        //            "      CompanyMaster.BusinessRevenueTypeId, CompanyMaster.OrganisedTypeId, CompanyMaster.IndustryTypeMasterId, CompanyMaster.PaypalEmailId,  " +
        //            "      CompanyMaster.PurchaseInvoiceTracking, CompanyMaster.TrackInventory, CompanyMaster.BusinessPlanningModule, CompanyMaster.EmployeeTaskAllocation,  " +
        //            "      CompanyMaster.EmployeeTimeTracking, CompanyWebsitMaster.Sitename, CompanyWebsitMaster.SiteUrl, CompanyWebsitMaster.AdminEmail,  " +
        //            "      CompanyWebsitMaster.EmailMasterLoginPassword, CompanyWebsiteAddressMaster.CompanyWebsiteAddressMasterId,  " +
        //            "      CompanyWebsiteAddressMaster.AddressTypeMasterId, CompanyWebsiteAddressMaster.Address1, CompanyWebsiteAddressMaster.Address2,  " +
        //            "      CompanyWebsiteAddressMaster.City, CompanyWebsiteAddressMaster.State, CompanyWebsiteAddressMaster.Country, CompanyWebsiteAddressMaster.Phone1,  " +
        //            "      CompanyWebsiteAddressMaster.Phone2, CompanyWebsiteAddressMaster.TollFree1, CompanyWebsiteAddressMaster.TollFree2,  " +
        //            "      CompanyWebsiteAddressMaster.Fax, CompanyWebsiteAddressMaster.Zip, CompanyWebsiteAddressMaster.Email, CompanyWebsiteAddressMaster.LiveChatUrl,  " +
        //            "      CompanyWebsiteAddressMaster.ContactPersonName, CompanyWebsiteAddressMaster.ContactPersonDesignation, CompanyWebsiteAddressMaster.LogoUrl,  " +
        //            "      CompanyWebsitMaster.CompanyWebsiteMasterId " +
        //            "  FROM         AddressTypeMaster RIGHT OUTER JOIN " +
        //            "      CompanyWebsiteAddressMaster ON AddressTypeMaster.AddressTypeMasterId = CompanyWebsiteAddressMaster.AddressTypeMasterId LEFT OUTER JOIN " +
        //            "      CompanyMaster RIGHT OUTER JOIN " +
        //            "      CompanyWebsitMaster ON CompanyMaster.CompanyId = CompanyWebsitMaster.CompanyId ON   " +
        //            "          CompanyWebsiteAddressMaster.CompanyWebsiteMasterId = CompanyWebsitMaster.CompanyWebsiteMasterId " +
        //            "   WHERE     (CompanyWebsitMaster.CompanyWebsiteMasterId ='" + ddlCompanyWebsiteMasterName.SelectedValue + "'  ) ";
        //    SqlCommand d = new SqlCommand(sg, con);
        //    SqlDataAdapter ad = new SqlDataAdapter(d);
        //    DataTable dtad = new DataTable();
        //    ad.Fill(dtad);
        //    if (dtad.Rows.Count > 0)
        //    {

        //        ddlAddressTypeMasterName.SelectedIndex = ddlAddressTypeMasterName.Items.IndexOf(ddlAddressTypeMasterName.Items.FindByValue(dtad.Rows[0]["CompanyWebsiteMasterId"].ToString()));
        //        //ddlAddressTypeMasterName.SelectedIndex
        //        txtAddress1.Text = dtad.Rows[0]["Address1"].ToString();
        //        txtAddress2.Text = dtad.Rows[0]["Address2"].ToString();
        //        //txtCity.Text = dtad.Rows[0]["City"].ToString();
        //        //tXtState.Text = dtad.Rows[0]["State"].ToString();

        //        //TextCountry.Text = dtad.Rows[0]["Country"].ToString();
        //        TextPhone1.Text = dtad.Rows[0]["Phone1"].ToString();
        //        TextPhone2.Text = dtad.Rows[0]["Phone2"].ToString();
        //        TextTollFree1.Text = dtad.Rows[0]["TollFree1"].ToString();
        //        TextTollFree2.Text = dtad.Rows[0]["TollFree2"].ToString();
        //        TextFax.Text = dtad.Rows[0]["Fax"].ToString();
        //        TextZip.Text = dtad.Rows[0]["Zip"].ToString();
        //        TextEmail.Text = dtad.Rows[0]["Email"].ToString();
        //        TextLiveChatUrl.Text = dtad.Rows[0]["LiveChatUrl"].ToString();
        //        TextContactPersonName.Text = dtad.Rows[0]["ContactPersonName"].ToString();
        //        TextContactPersonDesignation.Text = dtad.Rows[0]["ContactPersonDesignation"].ToString();
        //        ViewState["MainId"] = dtad.Rows[0]["CompanyWebsiteAddressMasterId"].ToString();

        //        ViewState["DAta"] = dtad;
        //    }
        //    else
        //    {

        //    }
        //}
        //mycmd.Parameters.AddWithValue("@CompanyWebsiteMasterId", ddlCompanyWebsiteMasterName.SelectedValue);
        //mycmd.Parameters.AddWithValue("@AddressTypeMasterId", ddlAddressTypeMasterName.SelectedValue);
        //mycmd.Parameters.AddWithValue("@Address1", txtAddress1.Text);
        //mycmd.Parameters.AddWithValue("@Address2", txtAddress2.Text);
        //mycmd.Parameters.AddWithValue("@City", txtCity.Text);
        //mycmd.Parameters.AddWithValue("@State", tXtState.Text);
        //mycmd.Parameters.AddWithValue("@Country", TextCountry.Text);
        //mycmd.Parameters.AddWithValue("@Phone1", TextPhone1.Text);
        //mycmd.Parameters.AddWithValue("@Phone2", TextPhone2.Text);
        //mycmd.Parameters.AddWithValue("@TollFree1", TextTollFree1.Text);
        //mycmd.Parameters.AddWithValue("@TollFree2", TextTollFree2.Text);
        //mycmd.Parameters.AddWithValue("@Fax", TextFax.Text);
        //mycmd.Parameters.AddWithValue("@Zip", TextZip.Text);
        //mycmd.Parameters.AddWithValue("@Email", TextEmail.Text);
        //mycmd.Parameters.AddWithValue("@LiveChatUrl", TextLiveChatUrl.Text);
        //mycmd.Parameters.AddWithValue("@LogoUrl", DBNull.Value);
        //mycmd.Parameters.AddWithValue("@ContactPersonName", TextContactPersonName.Text);
        //mycmd.Parameters.AddWithValue("@ContactPersonDesignation", TextContactPersonDesignation.Text);
        //*********chnages codes


        imgbtnupdate.Visible = false;
        //**************

        Button1.Visible = true;
        txtAddress1.Text = "";
        txtAddress2.Text = "";
        TextPhone1.Text = "";
        TextPhone2.Text = "";
        TextTollFree1.Text = "";
        TextTollFree2.Text = "";
        TextFax.Text = "";
        TextZip.Text = "";
        TextEmail.Text = "";
        TextLiveChatUrl.Text = "";
        TextContactPersonName.Text = "";
        TextContactPersonDesignation.Text = "";
        ddlcountry.SelectedIndex = 0;
        ddlstate.SelectedIndex = 0;
        ddlcity.SelectedIndex = 0;

    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "editview")
        {

            GridView2.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            ViewState["cwid"] = GridView2.SelectedIndex;

            SqlCommand cmdview = new SqlCommand("SELECT * from CompanyWebsiteAddressMaster where CompanyWebsiteAddressMasterId='" + ViewState["cwid"] + "'", con);
            SqlDataAdapter dtpview = new SqlDataAdapter(cmdview);
            DataTable dtview = new DataTable();
            dtpview.Fill(dtview);
            if (dtview.Rows.Count > 0)
            {
                Label1.Text = "";
                btnadd.Visible = false;
                Pnladdnew.Visible = true;
                lbllegend.Text = "Edit Business Address";
                Button1.Visible = false;
                imgbtnupdate.Visible = false;
                imgbtnedit.Visible = true;
                //controlenable(false);
                ddlCompanyWebsiteMasterName.SelectedIndex = ddlCompanyWebsiteMasterName.Items.IndexOf(ddlCompanyWebsiteMasterName.Items.FindByValue(dtview.Rows[0]["CompanyWebsiteMasterId"].ToString()));
                //  ddlCompanyWebsiteMasterName.Enabled = true;
                ddlAddressTypeMasterName.SelectedIndex = ddlAddressTypeMasterName.Items.IndexOf(ddlAddressTypeMasterName.Items.FindByValue(dtview.Rows[0]["AddressTypeMasterId"].ToString()));
                ddlcountry.SelectedIndex = ddlcountry.Items.IndexOf(ddlcountry.Items.FindByValue(dtview.Rows[0]["Country"].ToString()));
                EventArgs eg = new EventArgs();
                object ob = new object();
                ddlcountry_SelectedIndexChanged(ob, eg);
                ddlstate.SelectedIndex = ddlstate.Items.IndexOf(ddlstate.Items.FindByValue(dtview.Rows[0]["State"].ToString()));
                EventArgs eg1 = new EventArgs();
                object ob1 = new object();
                ddlstate_SelectedIndexChanged(ob1, eg1);
                ddlcity.SelectedIndex = ddlcity.Items.IndexOf(ddlcity.Items.FindByValue(dtview.Rows[0]["City"].ToString()));
                txtAddress1.Text = dtview.Rows[0]["Address1"].ToString();
                txtAddress2.Text = dtview.Rows[0]["Address2"].ToString();
                //txtCity.Text = dtview.Rows[0]["City"].ToString();
                //tXtState.Text = dtview.Rows[0]["State"].ToString();
                //TextCountry.Text = dtview.Rows[0]["Country"].ToString();
                TextEmail.Text = dtview.Rows[0]["Email"].ToString();
                TextPhone1.Text = dtview.Rows[0]["Phone1"].ToString();
                TextPhone2.Text = dtview.Rows[0]["Phone2"].ToString();
                TextTollFree1.Text = dtview.Rows[0]["TollFree1"].ToString();
                TextTollFree2.Text = dtview.Rows[0]["TollFree2"].ToString();
                TextFax.Text = dtview.Rows[0]["Fax"].ToString();
                TextZip.Text = dtview.Rows[0]["Zip"].ToString();
                TextContactPersonName.Text = dtview.Rows[0]["ContactPersonName"].ToString();
                TextContactPersonDesignation.Text = dtview.Rows[0]["ContactPersonDesignation"].ToString();
                if (dtview.Rows[0]["LiveChatUrl"].ToString() != "" && dtview.Rows[0]["LiveChatUrl"].ToString() != null)
                {
                    chkboldchat.Checked = true;
                    chkboldchat_CheckedChanged(sender, e);

                    TextLiveChatUrl.Text = dtview.Rows[0]["LiveChatUrl"].ToString();
                }
                else
                {
                    chkboldchat.Checked = false;
                    chkboldchat_CheckedChanged(sender, e);

                    TextLiveChatUrl.Text = "";
                }
                controlenable(false);
                //txtAddress1.Text = dtview.Rows[0][""].ToString();
                //txtAddress1.Text = dtview.Rows[0][""].ToString();

            }
        }
        if (e.CommandName == "del")
        {
            GridView2.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            ViewState["delid"] = GridView2.SelectedDataKey.Value;
            //ModalPopupExtender1222.Show();

        }
    }
    protected void controlenable(bool t)
    {
        ddlCompanyWebsiteMasterName.Enabled = t;
        ddlAddressTypeMasterName.Enabled = t;
        txtAddress1.Enabled = t;
        txtAddress2.Enabled = t;
        //txtCity.Enabled = t;
        //tXtState.Enabled = t;
        //TextCountry.Enabled = t;
        ddlcountry.Enabled = t;
        ddlstate.Enabled = t;
        ddlcity.Enabled = t;
        TextEmail.Enabled = t;
        TextPhone1.Enabled = t;
        TextPhone2.Enabled = t;
        TextTollFree1.Enabled = t;
        TextTollFree2.Enabled = t;
        TextFax.Enabled = t;
        TextZip.Enabled = t;
        TextContactPersonName.Enabled = t;
        TextContactPersonDesignation.Enabled = t;
        TextLiveChatUrl.Enabled = t;
        chkboldchat.Enabled = t;
    }




    protected void ddlcountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcountry.SelectedIndex > 0)
        {
            fillddlstate();
        }
    }
    protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlstate.SelectedIndex > 0)
        {
            fillddlcity();
        }
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/ShoppingCart/Admin/wzFaqCategoryMaster.aspx");
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/ShoppingCart/Admin/WizardCompanyWebsitMaster.aspx");
    }

    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgriddata();
    }

    protected void ImageButton61_Click(object sender, ImageClickEventArgs e)
    {
        ModalPopupExtender1.Hide();
    }
    //protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    fillgriddata();
    //}


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
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        fillgrid();
    }

    protected void ImageButton51_Click(object sender, EventArgs e)
    {
        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString"].ConnectionString);



        SqlCommand mycmd = new SqlCommand("Sp_Insert_CompanyWebsiteAddressMaster", con);



        mycmd.CommandType = CommandType.StoredProcedure;
        mycmd.Parameters.AddWithValue("@CompanyWebsiteMasterId", ddlCompanyWebsiteMasterName.SelectedValue);
        mycmd.Parameters.AddWithValue("@AddressTypeMasterId", ddlAddressTypeMasterName.SelectedValue);
        mycmd.Parameters.AddWithValue("@Address1", txtAddress1.Text);
        mycmd.Parameters.AddWithValue("@Address2", txtAddress2.Text);
        mycmd.Parameters.AddWithValue("@City", ddlcity.SelectedValue);
        mycmd.Parameters.AddWithValue("@State", ddlstate.SelectedValue);
        mycmd.Parameters.AddWithValue("@Country", ddlcountry.SelectedValue);
        mycmd.Parameters.AddWithValue("@Phone1", TextPhone1.Text);
        mycmd.Parameters.AddWithValue("@Phone2", TextPhone2.Text);
        mycmd.Parameters.AddWithValue("@TollFree1", TextTollFree1.Text);
        mycmd.Parameters.AddWithValue("@TollFree2", TextTollFree2.Text);
        mycmd.Parameters.AddWithValue("@Fax", TextFax.Text);
        mycmd.Parameters.AddWithValue("@Zip", TextZip.Text);
        mycmd.Parameters.AddWithValue("@Email", TextEmail.Text);
        mycmd.Parameters.AddWithValue("@LiveChatUrl", TextLiveChatUrl.Text);
        mycmd.Parameters.AddWithValue("@LogoUrl", DBNull.Value);
        mycmd.Parameters.AddWithValue("@ContactPersonName", TextContactPersonName.Text);
        mycmd.Parameters.AddWithValue("@ContactPersonDesignation", TextContactPersonDesignation.Text);

        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        mycmd.ExecuteNonQuery();
        con.Close();
        string stw = "select distinct WareHouseId as WHID from WareHouseMaster where WareHouseId='" + ddlCompanyWebsiteMasterName.SelectedValue + "'";
        SqlDataAdapter adcw = new SqlDataAdapter(stw, con);
        DataTable dtstw = new DataTable();
        adcw.Fill(dtstw);
        if (dtstw.Rows.Count > 0)
        {

            string st = "select distinct CarrierMethod,TrackingURL,Protocol,ShippingManager.ShippingManagerID from ShippingmanagerStateDetail inner join ShippingManager on ShippingmanagerStateDetail.ShippingManagerID=ShippingManager.ShippingManagerID where ShippingmanagerStateDetail.StateID='" + ddlstate.SelectedValue + "'";
            SqlDataAdapter adc = new SqlDataAdapter(st, con);
            DataTable dtst = new DataTable();
            adc.Fill(dtst);
            string scc = "";
            foreach (DataRow item in dtst.Rows)
            {
                string idhttp = "1";
                if ("http://" == Convert.ToString(item["Protocol"]))
                {
                    idhttp = "0";
                }
                string strsh = "Insert Into ShippersMaster(ShippersName,shippingDocNo,compid,Whid,DocumentName,TrackingUrl,Protocol,ShippingMethodId)values('" + item["CarrierMethod"] + "','0','" + Session["Comid"] + "','" + dtstw.Rows[0]["WHID"] + "','','" + item["TrackingURL"] + "','" + idhttp + "','1')";
                SqlCommand cmdsh = new SqlCommand(strsh, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdsh.ExecuteNonQuery();
                con.Close();
                scc += item["ShippingManagerID"] + ",";

                //if (scc.Length > 0)
                //{
                //    scc += ",";
                //}
                //else
                //{
                //    scc += item["ShippingManagerID"] + ",";
                //}
            }
            string st2 = "";
            if (scc.Length > 0)
            {

                scc = scc.Remove(scc.Length - 1);
            }
            if (scc.Length > 0)
            {
                st2 = "select distinct CarrierMethod,TrackingURL,Protocol,ShippingManagerID from ShippingManager  where ShippingManagerID NOT IN(" + scc + ") and Country_ID='" + ddlcountry.SelectedValue + "'";
            }
            else
            {
                st2 = "select distinct CarrierMethod,TrackingURL,Protocol,ShippingManagerID from ShippingManager where  Country_ID='" + ddlcountry.SelectedValue + "'";
            }
            SqlDataAdapter adc2 = new SqlDataAdapter(st2, con);
            DataTable dtst2 = new DataTable();
            adc2.Fill(dtst2);

            foreach (DataRow item in dtst2.Rows)
            {
                string idhttp = "1";
                if ("http://" == Convert.ToString(item["Protocol"]))
                {
                    idhttp = "0";
                }

                string strsh = "Insert Into ShippersMaster(ShippersName,shippingDocNo,compid,Whid,DocumentName,TrackingUrl,Protocol,ShippingMethodId)values('" + item["CarrierMethod"] + "','0','" + Session["Comid"] + "','" + dtstw.Rows[0]["WHID"] + "','','" + item["TrackingURL"] + "','" + idhttp + "','1')";
                SqlCommand cmdsh = new SqlCommand(strsh, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdsh.ExecuteNonQuery();
                con.Close();

            }
        }
        Label1.Visible = true;
        Label1.Text = "Record inserted successfully";
        GridView1.DataBind();
        txtAddress1.Text = "";
        txtAddress2.Text = "";

        ddlCompanyWebsiteMasterName.SelectedIndex = 0;
        ddlAddressTypeMasterName.SelectedIndex = 0;
        ddlcountry.SelectedIndex = 0;
        ddlstate.SelectedIndex = 0;
        ddlcity.SelectedIndex = 0;
        TextPhone1.Text = "";
        TextPhone2.Text = "";
        TextTollFree1.Text = "";
        TextTollFree2.Text = "";
        TextFax.Text = "";
        TextZip.Text = "";
        TextEmail.Text = "";
        TextLiveChatUrl.Text = "";
        chkboldchat.Checked = false;
        chkboldchat_CheckedChanged(sender, e);
        TextContactPersonName.Text = "";
        TextContactPersonDesignation.Text = "";
        fillgriddata();
        Pnladdnew.Visible = false;
        btnadd.Visible = true;
        lbllegend.Text = "";

    }
    protected void GridView2_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        fillgriddata();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string findmasterid = "select * from  CompanyWebsiteAddressMaster where CompanyWebsiteMasterId='" + ddlCompanyWebsiteMasterName.SelectedValue + "' and AddressTypeMasterId = '" + ddlAddressTypeMasterName.SelectedValue + "'";
        SqlDataAdapter adp12 = new SqlDataAdapter(findmasterid, con);
        DataTable ds34 = new DataTable();
        adp12.Fill(ds34);
        if (ds34.Rows.Count > 0)
        {
            Label1.Visible = true;
            Label1.Text = "Record already exists";

        }
        else
        {
            ModalPopupExtender1.Show();
        }
        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString"].ConnectionString);

        ////DataTable dtdata = (DataTable)(ViewState["DAta"]);
        ////if (dtdata.Rows.Count > 0)
        ////{
        ////    SqlCommand mycmd = new SqlCommand("Sp_Update_CompanyWebsiteAddressMaster", con);

        ////    mycmd.CommandType = CommandType.StoredProcedure;

        ////    mycmd.Parameters.AddWithValue("@CompanyWebsiteMasterId", ddlCompanyWebsiteMasterName.SelectedValue);
        ////    mycmd.Parameters.AddWithValue("@AddressTypeMasterId", ddlAddressTypeMasterName.SelectedValue);
        ////    mycmd.Parameters.AddWithValue("@Address1", txtAddress1.Text);
        ////    mycmd.Parameters.AddWithValue("@Address2", txtAddress2.Text);
        ////    mycmd.Parameters.AddWithValue("@City", txtCity.Text);
        ////    mycmd.Parameters.AddWithValue("@State", tXtState.Text);
        ////    mycmd.Parameters.AddWithValue("@Country", TextCountry.Text);
        ////    mycmd.Parameters.AddWithValue("@Phone1", TextPhone1.Text);
        ////    mycmd.Parameters.AddWithValue("@Phone2", TextPhone2.Text);
        ////    mycmd.Parameters.AddWithValue("@TollFree1", TextTollFree1.Text);
        ////    mycmd.Parameters.AddWithValue("@TollFree2", TextTollFree2.Text);
        ////    mycmd.Parameters.AddWithValue("@Fax", TextFax.Text);
        ////    mycmd.Parameters.AddWithValue("@Zip", TextZip.Text);
        ////    mycmd.Parameters.AddWithValue("@Email", TextEmail.Text);
        ////    mycmd.Parameters.AddWithValue("@LiveChatUrl", TextLiveChatUrl.Text);
        ////    mycmd.Parameters.AddWithValue("@LogoUrl", DBNull.Value);
        ////    mycmd.Parameters.AddWithValue("@ContactPersonName", TextContactPersonName.Text);
        ////    mycmd.Parameters.AddWithValue("@ContactPersonDesignation", TextContactPersonDesignation.Text);
        ////    mycmd.Parameters.AddWithValue("@CompanyWebsiteAddressMasterId", ViewState["MainId"].ToString());


        ////}
        ////else
        ////{


        //    SqlCommand mycmd = new SqlCommand("Sp_Insert_CompanyWebsiteAddressMaster", con);



        //    mycmd.CommandType = CommandType.StoredProcedure;
        //    mycmd.Parameters.AddWithValue("@CompanyWebsiteMasterId", ddlCompanyWebsiteMasterName.SelectedValue);
        //    mycmd.Parameters.AddWithValue("@AddressTypeMasterId", ddlAddressTypeMasterName.SelectedValue);
        //    mycmd.Parameters.AddWithValue("@Address1", txtAddress1.Text);
        //    mycmd.Parameters.AddWithValue("@Address2", txtAddress2.Text);
        //    mycmd.Parameters.AddWithValue("@City", ddlcity.SelectedValue);
        //    mycmd.Parameters.AddWithValue("@State", ddlstate.SelectedValue);
        //    mycmd.Parameters.AddWithValue("@Country", ddlcountry.SelectedValue);
        //    mycmd.Parameters.AddWithValue("@Phone1", TextPhone1.Text);
        //    mycmd.Parameters.AddWithValue("@Phone2", TextPhone2.Text);
        //    mycmd.Parameters.AddWithValue("@TollFree1", TextTollFree1.Text);
        //    mycmd.Parameters.AddWithValue("@TollFree2", TextTollFree2.Text);
        //    mycmd.Parameters.AddWithValue("@Fax", TextFax.Text);
        //    mycmd.Parameters.AddWithValue("@Zip", TextZip.Text);
        //    mycmd.Parameters.AddWithValue("@Email", TextEmail.Text);
        //    mycmd.Parameters.AddWithValue("@LiveChatUrl", TextLiveChatUrl.Text);
        //    mycmd.Parameters.AddWithValue("@LogoUrl", DBNull.Value);
        //    mycmd.Parameters.AddWithValue("@ContactPersonName", TextContactPersonName.Text);
        //    mycmd.Parameters.AddWithValue("@ContactPersonDesignation", TextContactPersonDesignation.Text);


        //    con.Open();
        //    mycmd.ExecuteNonQuery();
        //    con.Close();
        //    Label1.Visible = true;
        //    Label1.Text = "Record Inserted Successfully";
        //    GridView1.DataBind();
        //    txtAddress1.Text = "";
        //    txtAddress2.Text = "";
        //    //txtCity.Text = "";
        //    //tXtState.Text = "";
        //    //TextCountry.Text = "";
        //    ddlcountry.SelectedIndex = 0;
        //    ddlstate.SelectedIndex = 0;
        //    ddlcity.SelectedIndex = 0;
        //    TextPhone1.Text = "";
        //    TextPhone2.Text = "";
        //    TextTollFree1.Text = "";
        //    TextTollFree2.Text = "";
        //    TextFax.Text = "";
        //    TextZip.Text = "";
        //    TextEmail.Text = "";
        //    TextLiveChatUrl.Text = "";
        //    //TextLogoUrl.Text = "";
        //    TextContactPersonName.Text = "";
        //    TextContactPersonDesignation.Text = "";
        //    fillgriddata();
        ////}

    }
    protected void imgbtnedit_Click(object sender, EventArgs e)
    {
        imgbtnupdate.Visible = true;
        imgbtnedit.Visible = false;
        controlenable(true);
        ddlCompanyWebsiteMasterName.Enabled = false;
    }
    protected void imgbtnupdate_Click(object sender, EventArgs e)
    {
        string findmasterid = "select * from  CompanyWebsiteAddressMaster where CompanyWebsiteMasterId='" + ddlCompanyWebsiteMasterName.SelectedValue + "' and AddressTypeMasterId = '" + ddlAddressTypeMasterName.SelectedValue + "' and CompanyWebsiteAddressMasterId <> '" + ViewState["cwid"] + "'";
        //string findmasterid = "select * from  CompanyWebsitMaster where CompanyWebsiteMasterId='" + ddlCompanyWebsiteMasterName.SelectedValue.ToString() + "'";
        SqlDataAdapter adp12 = new SqlDataAdapter(findmasterid, con);
        DataSet ds34 = new DataSet();
        adp12.Fill(ds34);
        if (ds34.Tables[0].Rows.Count > 0)
        {
            Label1.Visible = true;
            Label1.Text = "Record already exists";
        }
        else
        {
            //if (ds34.Tables[0].Rows.Count > 0)
            //{
            //    mas = Convert.ToInt32(ds34.Tables[0].Rows[0]["CompanyWebsiteMasterId"].ToString());

            //}
            //**********************



            string strweb = "update CompanyWebsiteAddressMaster set AddressTypeMasterId='" + ddlAddressTypeMasterName.SelectedValue + "', " +
                " Address1='" + txtAddress1.Text + "',Address2='" + txtAddress2.Text + "',City='" + ddlcity.SelectedValue + "',State='" + ddlstate.SelectedValue + "',Country='" + ddlcountry.SelectedValue + "',Phone1='" + TextPhone1.Text + "', " +
                " Phone2='" + TextPhone2.Text + "',TollFree1='" + TextTollFree1.Text + "',TollFree2='" + TextTollFree2.Text + "',Fax='" + TextFax.Text + "',Zip='" + TextZip.Text + "',Email='" + TextEmail.Text + "', " +
                " ContactPersonName='" + TextContactPersonName.Text + "',ContactPersonDesignation='" + TextContactPersonDesignation.Text + "' where CompanyWebsiteAddressMasterId='" + ViewState["cwid"] + "'";

            //SqlCommand cmdup = new SqlCommand("update CompanyWebsiteAddressMaster set CompanyWebsiteMasterId=" + mas + ",AddressTypeMasterId='" + ddlAddressTypeMasterName.SelectedValue + "', " +
            //    " Address1='" + txtAddress1.Text + "',Address2='" + txtAddress2.Text + "',City='" + ddlcity.SelectedValue + "',State='" + ddlstate.SelectedValue  + "',Country='" + ddlcountry.SelectedValue + "',Phone1='" + TextPhone1.Text + "', " +
            //    " Phone2='" + TextPhone2.Text + "',TollFree1='" + TextTollFree1.Text + "',TollFree2='" + TextTollFree2.Text + "',Fax='" + TextFax.Text + "',Zip='" + TextZip.Text + "',Email='" + TextEmail.Text + "', " +
            //    " ContactPersonName='" + TextContactPersonName.Text + "',ContactPersonDesignation='" + TextContactPersonDesignation.Text + "' where CompanyWebsiteAddressMasterId='" + ViewState["cwid"] + "'", con);
            //// LiveChatUrl='" + TextLiveChatUrl.Text + "'
            SqlCommand cmdup = new SqlCommand(strweb, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdup.ExecuteNonQuery();
            con.Close();
            Button1.Visible = true;
            imgbtnedit.Visible = false;
            imgbtnupdate.Visible = false;

            fillgriddata();

            Label1.Visible = true;
            Label1.Text = "";
            Label1.Text = "Record updated successfully";
            btnadd.Visible = true;
            Pnladdnew.Visible = false;
            lbllegend.Text = "";
            //GridView1.DataBind();
            ddlAddressTypeMasterName.SelectedIndex = 0;
            ddlCompanyWebsiteMasterName.SelectedIndex = 0;

            txtAddress1.Text = "";
            txtAddress2.Text = "";
            //txtCity.Text = "";
            //tXtState.Text = "";
            //TextCountry.Text = "";
            ddlcountry.SelectedIndex = 0;
            ddlstate.SelectedIndex = 0;
            ddlcity.SelectedIndex = 0;
            TextPhone1.Text = "";
            TextPhone2.Text = "";
            TextTollFree1.Text = "";
            TextTollFree2.Text = "";
            TextFax.Text = "";
            TextZip.Text = "";
            TextEmail.Text = "";
            TextLiveChatUrl.Text = "";
            controlenable(true);
            //TextLogoUrl.Text = "";
            TextContactPersonName.Text = "";
            TextContactPersonDesignation.Text = "";
            chkboldchat.Checked = false;
            chkboldchat_CheckedChanged(sender, e);
        }

    }
    protected void imgBtnCancelMainUpdate_Click(object sender, EventArgs e)
    {
        controlenable(true);
        ddlAddressTypeMasterName.SelectedIndex = 0;
        ddlCompanyWebsiteMasterName.SelectedIndex = 0;
        btnadd.Visible = true;
        Pnladdnew.Visible = false;
        lbllegend.Text = "";
        txtAddress1.Text = "";
        txtAddress2.Text = "";
        //txtCity.Text = "";
        //tXtState.Text = "";
        //TextCountry.Text = "";
        ddlcountry.SelectedIndex = 0;
        ddlstate.SelectedIndex = 0;
        ddlcity.SelectedIndex = 0;
        TextPhone1.Text = "";
        TextPhone2.Text = "";
        TextTollFree1.Text = "";
        TextTollFree2.Text = "";
        TextFax.Text = "";
        TextZip.Text = "";
        TextEmail.Text = "";
        TextLiveChatUrl.Text = "";
        //TextLogoUrl.Text = "";
        TextContactPersonName.Text = "";
        TextContactPersonDesignation.Text = "";
        imgbtnedit.Visible = false;
        imgbtnupdate.Visible = false;
        Button1.Visible = true;
        chkboldchat.Checked = false;
        chkboldchat_CheckedChanged(sender, e);
        //fillgriddata();
    }
    protected void ImageButton5_Click(object sender, EventArgs e)
    {
        SqlCommand cmddel = new SqlCommand("Delete from CompanyWebsiteAddressMaster where CompanyWebsiteAddressMasterId='" + ViewState["delid"] + "'", con);
        con.Open();
        cmddel.ExecuteNonQuery();
        con.Close();
        //ModalPopupExtender1222.Hide();
        fillgriddata();
        Label1.Visible = true;
        Label1.Text = "";
        Label1.Text = "Record Deleted Successfully";
    }
    protected void ImageButton6_Click(object sender, EventArgs e)
    {
        //ModalPopupExtender1222.Hide();
        Pnladdnew.Visible = false;
        btnadd.Visible = true;
        lbllegend.Text = "";
        imgBtnCancelMainUpdate_Click(sender, e);

    }

    protected void ddlfilterstore_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgriddata();
    }
    protected void btncancel0_Click(object sender, EventArgs e)
    {
        if (btncancel0.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");
            btncancel0.Text = "Hide Printable Version";
            Button7.Visible = true;

            if (GridView2.Columns[9].Visible == true)
            {
                ViewState["edith"] = "tt";
                GridView2.Columns[9].Visible = false;
            }

        }
        else
        {
            btncancel0.Text = "Printable Version";
            Button7.Visible = false;

            if (ViewState["edith"] != null)
            {
                GridView2.Columns[9].Visible = true;
            }

        }
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        btnadd.Visible = false;
        Pnladdnew.Visible = true;
        lbllegend.Text = "Add New Business Address";
    }

    protected void chkboldchat_CheckedChanged(object sender, EventArgs e)
    {
        if (chkboldchat.Checked == true)
        {
            Panel4.Visible = true;
        }
        else
        {
            Panel4.Visible = false;
        }
    }
    protected void ImageButton51_Click1(object sender, ImageClickEventArgs e)
    {
        fillDdlAddressType();
    }
    protected void ImageButton50_Click(object sender, ImageClickEventArgs e)
    {
        string te = "AddressTypeMaster.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
}

