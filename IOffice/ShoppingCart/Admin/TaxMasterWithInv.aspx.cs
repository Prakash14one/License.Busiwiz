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
public partial class Shoppingcart_Admin_TaxMasterWithInv : System.Web.UI.Page
{
    // SqlConnection con123 = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString"].ConnectionString);
    // SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    DBCommands1 dbss1 = new DBCommands1();
    public DataTable dtinvids = new DataTable();
    string compid;
    SqlConnection con;
    public int puraccid = 0;
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

        compid = Session["Comid"].ToString();
        Page.Title = pg.getPageTitle(page);
        /// lblBarcode.Visible = false;
        lblmsg.Visible = false;
    
        if (!IsPostBack)
        {
            ViewState["sortOrder"] = "";
            ViewState["CSSCNm"] = "";

            if (Request.QueryString["wid"] != null)
            {


                string strwh = "SELECT WareHouseId,Name,Address,CurrencyId FROM WareHouseMaster where comid='" + compid + "'";
                SqlCommand cmdwh = new SqlCommand(strwh, con);
                SqlDataAdapter adpwh = new SqlDataAdapter(cmdwh);
                DataTable dtwh = new DataTable();
                adpwh.Fill(dtwh);

                ddlWarehouse.DataSource = dtwh;
                ddlWarehouse.DataTextField = "Name";
                ddlWarehouse.DataValueField = "WareHouseId";
                ddlWarehouse.DataBind();
                ddlWarehouse.SelectedIndex = ddlWarehouse.Items.IndexOf(ddlWarehouse.Items.FindByValue(Request.QueryString["wid"]));
                //ddlWarehouse.Items.Insert(0, "-Select-");
                //ddlWarehouse.Items[0].Value = "0";
                filltaxgrid();

                lblcompany.Text = Session["Cname"].ToString();
              




                ViewState["sortOrder"] = "";

                //FilterInventoryId();
                //FillWarehouseinGrid();
                //FilterInventoryId();

                if (RadioButtonList1.SelectedValue == "0")
                {
                    //    pnlInvCat.Visible = true;
                    //    pnlInvName.Visible = false;
                    //    pnlInvDDLname.Visible = false;
                    FillddlInvCat();
                }
                else if (RadioButtonList1.SelectedValue == "1")
                {
                    //pnlInvCat.Visible = false;
                    //pnlInvName.Visible = true;
                    //pnlInvDDLname.Visible = false;
                }

                else
                {
                    pnlInvCat.Visible = false;
                    pnlInvName.Visible = false;
                  
                }
                FillCountry();
                //RadioButtonList1_SelectedIndexChanged(sender, e);
            }

        }
    }




    protected void FillddlInvCat()
    {
        //string strcat = "SELECT InventeroyCatId,InventoryCatName  FROM  InventoryCategoryMaster where compid='" + compid + "' ";
        string strcat = "SELECT Distinct InventeroyCatId,InventoryCatName  FROM InventoryCategoryMaster inner join InventorySubCategoryMaster on InventorySubCategoryMaster.InventoryCategoryMasterId=InventoryCategoryMaster.InventeroyCatId inner join InventoruSubSubCategory on InventoruSubSubCategory.InventorySubCatID=InventorySubCategoryMaster.InventorySubCatId inner Join InventoryMaster on InventoryMaster.InventorySubSubId=InventoruSubSubCategory.InventorySubSubId inner join InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.InventoryMasterId=InventoryMaster.InventoryMasterId where InventoryWarehouseMasterTbl.WareHouseId='" + ddlWarehouse.SelectedValue + "' order by InventoryCatName";
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
        //ddlInvCat.AutoPostBack = true;
        object se = new object();
        EventArgs er = new EventArgs();
        ddlInvCat_SelectedIndexChanged(se, er);

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
                            " where InventoryCategoryMasterId = " + Convert.ToInt32(ddlInvCat.SelectedValue) + " order by InventorySubCatName";
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
                            " where InventorySubCatID=" + Convert.ToInt32(ddlInvSubCat.SelectedValue) + " order by InventorySubSubName ";
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


        ddlInvName.DataSource = null;
        ddlInvName.DataBind();
        ddlInvName.Items.Clear();
        if (Convert.ToInt32(ddlInvSubSubCat.SelectedIndex) > 0)
        {
            string strinvname = "SELECT Name,InventoryMaster.InventoryMasterId ,InventorySubSubId   ,ProductNo  FROM InventoryMaster   " +
                            " where InventoryMaster.InventorySubSubId= '" + Convert.ToInt32(ddlInvSubSubCat.SelectedValue) + "' and  InventoryMaster.MasterActiveStatus='True' Order by Name";
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

    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        //if(ddlWarehouse.SelectedIndex>0)
        //{

        

        Panel1.Visible = true;


        if (RadioButtonList1.SelectedValue == "0")
        {
            dtinvids = (DataTable)(SeachByCat());
            // Panel2.Visible = true;
        }
        else if (RadioButtonList1.SelectedValue == "1")
        {
            if (txtSearchInvName.Text.Length > 0 )
            {
                dtinvids = (DataTable)(SearchByName());
                // Panel2.Visible = true;
            }
            else
            {
               
            }
        }

        else
        {

        }
        
    }



    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnSubmit.Visible = false;
        if (RadioButtonList1.SelectedValue == "0")
        {
            pnlInvCat.Visible = true;
            pnlInvName.Visible = false;
        
            FillddlInvCat();
            //btnSearchGo_Click(sender, e);
            clearcontrolsdetail();
            Panel1.Visible = false;
        }
        else if (RadioButtonList1.SelectedValue == "1")
        {
            txtSearchInvName.Text = "";
            pnlInvCat.Visible = false;
            pnlInvName.Visible = true;
         
            // btnSearchGo_Click(sender, e);
            clearcontrolsdetail();
            Panel1.Visible = false;
        }
        else if (RadioButtonList1.SelectedValue == "2")
        {

        }
        else
        {
            pnlInvCat.Visible = false;
            pnlInvName.Visible = false;
          
            Panel1.Visible = false;
            //btnSearchGo_Click(sender, e);

        }
    }
  


    public DataTable SeachByCat()
    {
        //string strinv =" SELECT     InventoryMaster.InventoryMasterId, InventoryDetails.Inventory_Details_Id, InventoryDetails.Weight, InventoruSubSubCategory.InventorySubSubId, "+
        //           "    InventoruSubSubCategory.InventorySubSubName, InventorySubCategoryMaster.InventorySubCatId, InventorySubCategoryMaster.InventorySubCatName, "+
        //           "   InventoryCategoryMaster.InventeroyCatId, InventoryCategoryMaster.InventoryCatName, InventoryMaster.Name, InventoryMaster.ProductNo, "+
        //           "   InventoryCategoryMaster.InventoryCatName + ' : <br/>' + InventorySubCategoryMaster.InventorySubCatName + ' : <br/>' + InventoruSubSubCategory.InventorySubSubName "+
        //           "    AS CateAndName, InventoryBarcodeMaster.Barcode, InventoryDetails.Inventory_Details_Id AS Expr1, InventoryDetails.Description, "+
        //           "   UnitTypeMaster.Name AS UnitTypeName, InventoryDetails.UnitTypeId, InventoryWarehouseMasterTbl.InventoryWarehouseMasterId,  "+
        //           "   InventoryWarehouseMasterTbl.WareHouseId "+
        //            " FROM         InventoryWarehouseMasterTbl LEFT OUTER JOIN "+
        //            "  InventoryMaster ON InventoryWarehouseMasterTbl.InventoryMasterId = InventoryMaster.InventoryMasterId LEFT OUTER JOIN "+
        //            "  InventoryDetails LEFT OUTER JOIN "+
        //            "  UnitTypeMaster ON InventoryDetails.UnitTypeId = UnitTypeMaster.UnitTypeId ON  "+
        //            "  InventoryMaster.InventoryDetailsId = InventoryDetails.Inventory_Details_Id LEFT OUTER JOIN "+
        //            "  InventoryBarcodeMaster ON InventoryMaster.InventoryMasterId = InventoryBarcodeMaster.InventoryMaster_id LEFT OUTER JOIN "+
        //            "  InventorySubCategoryMaster LEFT OUTER JOIN "+
        //            "  InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId RIGHT OUTER JOIN "+
        //            "  InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID ON "+
        //            "  InventoryMaster.InventorySubSubId = InventoruSubSubCategory.InventorySubSubId ";

        string strinv = "SELECT     InventoryMaster.InventoryMasterId, InventoruSubSubCategory.InventorySubSubId, InventoruSubSubCategory.InventorySubSubName,  " +
                  "     InventorySubCategoryMaster.InventorySubCatId, InventorySubCategoryMaster.InventorySubCatName, InventoryCategoryMaster.InventeroyCatId,  " +
                  "    InventoryCategoryMaster.InventoryCatName, InventoryMaster.Name, InventoryMaster.ProductNo,  " +
                  "    Left( InventoryCategoryMaster.InventoryCatName,10) + ' : ' +Left(  InventorySubCategoryMaster.InventorySubCatName,10) + ' : ' + Left( InventoruSubSubCategory.InventorySubSubName,15) " +
                  "     AS CateAndName, InventoryWarehouseMasterTbl.InventoryWarehouseMasterId, InventoryWarehouseMasterTbl.WareHouseId " +
                  "  FROM         InventoryWarehouseMasterTbl LEFT OUTER JOIN " +
                   "   InventoryMaster ON InventoryWarehouseMasterTbl.InventoryMasterId = InventoryMaster.InventoryMasterId LEFT OUTER JOIN " +
                   "   InventorySubCategoryMaster LEFT OUTER JOIN " +
                   "   InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId RIGHT OUTER JOIN " +
                   "   InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID ON " +
                   "   InventoryMaster.InventorySubSubId = InventoruSubSubCategory.InventorySubSubId left outer join InventoryMasterMNC on  InventoryMasterMNC.Inventorymasterid=InventoryMaster.InventoryMasterId " +
                   " WHERE     (InventoryWarehouseMasterTbl.WareHouseId = '" + ddlWarehouse.SelectedValue + "')";
        string strInvId = "";
        string strInvsubsubCatId = "";
        string strInvsubcatid = "";
        string strInvCatid = "";
        // string strInvBySerchId = "";
        //if (txtSearchInvName.Text.Length <= 0)
        //{
        if (ddlInvCat.SelectedIndex > 0)
        {
            if (ddlInvSubCat.SelectedIndex > 0)
            {
                if (ddlInvSubSubCat.SelectedIndex > 0)
                {
                    if (ddlInvName.SelectedIndex > 0)
                    {
                        strInvId = " and  InventoryMaster.InventoryMasterId=" + Convert.ToInt32(ddlInvName.SelectedValue) + " ";

                    }
                    else
                    {
                        strInvsubsubCatId = " and InventoruSubSubCategory.InventorySubSubId=" + Convert.ToInt32(ddlInvSubSubCat.SelectedValue) + "";
                    }
                }
                else
                {
                    strInvsubcatid = " and InventorySubCategoryMaster.InventorySubCatId = " + Convert.ToInt32(ddlInvSubCat.SelectedValue) + " ";

                }

            }
            else
            {
                strInvCatid = " and InventoryCategoryMaster.InventeroyCatId =" + Convert.ToInt32(ddlInvCat.SelectedValue) + " ";

                //strInvId = "where  InventoryMaster.InventoryMasterId=" + Convert.ToInt32(ddlInvName.SelectedValue) + " ";

            }
        }
        else
        {
            //strInvCatid = "where InventoryCategoryMaster.InventeroyCatId =" + Convert.ToInt32(ddlInvCat.SelectedValue) + " ";

        }


        string mainStringCat = strinv + strInvId + strInvsubsubCatId + strInvsubcatid + strInvCatid + " and InventoryMaster.MasterActiveStatus=1  order by CateAndName";//+ strInvBySerchId // InventoryMaster.Name ";


        SqlCommand cmdcat = new SqlCommand(mainStringCat, con);
        SqlDataAdapter adpcat = new SqlDataAdapter(cmdcat);
        DataTable dtcat = new DataTable();
        adpcat.Fill(dtcat);



        return dtcat;

    }

    public DataTable SearchByName()
    {
        string strinvMainName = "SELECT     InventoryMaster.InventoryMasterId, InventoruSubSubCategory.InventorySubSubId, InventoruSubSubCategory.InventorySubSubName,  " +
                  "     InventorySubCategoryMaster.InventorySubCatId, InventorySubCategoryMaster.InventorySubCatName, InventoryCategoryMaster.InventeroyCatId,  " +
                  "    InventoryCategoryMaster.InventoryCatName, InventoryMaster.Name, InventoryMaster.ProductNo,  " +
                  "   Left(InventoryCategoryMaster.InventoryCatName,10) + ' : ' +Left( InventorySubCategoryMaster.InventorySubCatName,10) + ' : ' + Left(InventoruSubSubCategory.InventorySubSubName,15) " +
                  "     AS CateAndName, InventoryWarehouseMasterTbl.InventoryWarehouseMasterId, InventoryWarehouseMasterTbl.WareHouseId " +
                  "  FROM         InventoryWarehouseMasterTbl INNER JOIN " +
                   "   InventoryMaster ON InventoryWarehouseMasterTbl.InventoryMasterId = InventoryMaster.InventoryMasterId INNER JOIN " +
                   "   InventorySubCategoryMaster INNER JOIN " +
                   "   InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId INNER JOIN " +
                   "   InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID ON " +
                   "   InventoryMaster.InventorySubSubId = InventoruSubSubCategory.InventorySubSubId    Left Join InventoryBarcodeMaster on InventoryBarcodeMaster.InventoryMaster_id=InventoryMaster.InventoryMasterId  " +
                   " WHERE     (InventoryWarehouseMasterTbl.WareHouseId = '" + ddlWarehouse.SelectedValue + "') and  (InventoryMaster.ProductNo = '%" + txtSearchInvName.Text + "%' or InventoryMaster.Name like '%" + txtSearchInvName.Text.Replace("'", "''") + "%' or InventoryBarcodeMaster.Barcode='%" + txtSearchInvName.Text + "%')  and InventoryMaster.MasterActiveStatus=1 order by CateAndName";






        string strmaininvname = strinvMainName;
        SqlCommand cmdinvname = new SqlCommand(strmaininvname, con);
        SqlDataAdapter adpinvname = new SqlDataAdapter(cmdinvname);
        DataTable dtinvname = new DataTable();
        adpinvname.Fill(dtinvname);

        return dtinvname;

    }

    protected void grdInvMasters_RowCommand(object sender, GridViewCommandEventArgs e)
    {
       
        if (e.CommandName == "Select1")
        {
            int indx = Convert.ToInt32(e.CommandArgument.ToString());

        }
    }
    protected void ClearDetails()
    {

    }
    //protected void btnSubmit_Click(object sender, ImageClickEventArgs e)
    //{


    //}

    protected void ddlWarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        RadioButtonList1_SelectedIndexChanged(sender, e);
        Panel1.Visible = false;
        //btnSearchGo_Click(sender, e);
    }

    protected void clearcontrolsdetail()
    {

    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {

    }
    //protected void ImgBtnSearchGo_Click(object sender, ImageClickEventArgs e)
    //{

    //}
    protected bool isboolornot(string ck)
    {
        try
        {
            Convert.ToBoolean(ck);
            return true;
        }
        catch
        {
            return false;
        }


    }
    protected void ImgBtnAdd1_Click(object sender, ImageClickEventArgs e)
    {
        FillBelowGrid();
    }
    protected DataTable CreateTempDT()
    {

        DataTable dtTemp = new DataTable();


        DataColumn id = new DataColumn();
        id.ColumnName = "InventoryWarehouseMasterId";
        id.DataType = System.Type.GetType("System.Int32");
        id.AllowDBNull = true;
        dtTemp.Columns.Add(id);


        DataColumn prd = new DataColumn();
        prd.ColumnName = "ProductNo";
        prd.DataType = System.Type.GetType("System.String");
        prd.AllowDBNull = true;
        dtTemp.Columns.Add(prd);

        DataColumn ssCatId = new DataColumn();
        ssCatId.ColumnName = "CateAndName";
        ssCatId.DataType = System.Type.GetType("System.String");
        ssCatId.AllowDBNull = true;
        dtTemp.Columns.Add(ssCatId);

        DataColumn InvName = new DataColumn();
        InvName.ColumnName = "Name";
        InvName.DataType = System.Type.GetType("System.String");
        InvName.AllowDBNull = true;
        dtTemp.Columns.Add(InvName);

        DataColumn taxname = new DataColumn();
        taxname.ColumnName = "TaxtName";
        taxname.DataType = System.Type.GetType("System.String");
        taxname.AllowDBNull = true;
        dtTemp.Columns.Add(taxname);

        DataColumn Taxable = new DataColumn();
        Taxable.ColumnName = "Taxable";
        Taxable.DataType = System.Type.GetType("System.Boolean");
        Taxable.AllowDBNull = true;
        dtTemp.Columns.Add(Taxable);


        DataColumn Tax = new DataColumn();
        Tax.ColumnName = "Tax";
        Tax.DataType = System.Type.GetType("System.String");
        Tax.AllowDBNull = true;
        dtTemp.Columns.Add(Tax);




        DataColumn InvTaxabilityId = new DataColumn();
        InvTaxabilityId.ColumnName = "InvTaxabilityId";
        InvTaxabilityId.DataType = System.Type.GetType("System.String");
        InvTaxabilityId.AllowDBNull = true;
        dtTemp.Columns.Add(InvTaxabilityId);

        DataColumn appyallsales = new DataColumn();
        appyallsales.ColumnName = "ApplyToAllSales";
        appyallsales.DataType = System.Type.GetType("System.Boolean");
        appyallsales.AllowDBNull = true;
        dtTemp.Columns.Add(appyallsales);


        DataColumn appyonlinesales = new DataColumn();
        appyonlinesales.ColumnName = "ApplyToallOnlineSales";
        appyonlinesales.DataType = System.Type.GetType("System.Boolean");
        appyonlinesales.AllowDBNull = true;
        dtTemp.Columns.Add(appyonlinesales);

        DataColumn appystatus = new DataColumn();
        appystatus.ColumnName = "Active";
        appystatus.DataType = System.Type.GetType("System.Boolean");
        appystatus.AllowDBNull = true;
        dtTemp.Columns.Add(appystatus);


        DataColumn taxper = new DataColumn();
        taxper.ColumnName = "taxper";
        taxper.DataType = System.Type.GetType("System.String");
        taxper.AllowDBNull = true;
        dtTemp.Columns.Add(taxper);

        return dtTemp;

    }
    protected void FillCountry()
    {
        string strcountryfill = "SELECT CountryId   ,CountryName  ,Country_Code  FROM CountryMaster";
        dbss1.FillDDL1(ddlcountry, strcountryfill, "CountryId", "CountryName");
        ddlcountry.Items.Insert(0, "ALL");
        ddlcountry.Items[0].Value = "0";

        dbss1.FillDDL1(ddlfiltercountary, strcountryfill, "CountryId", "CountryName");
        ddlfiltercountary.Items.Insert(0, "ALL");
        ddlfiltercountary.SelectedItem.Value = "0";
        object aa = new object();
        EventArgs bb = new EventArgs();
        ddlcountry_SelectedIndexChanged1(aa, bb);
        //ddlState.Items.Insert(0, "ALL");
        //ddlState.Items[0].Value = "0";
        fillfistate();
    }
    protected void fillfistate()
    {
        ddlfilterstate.Items.Clear();
        if (ddlfiltercountary.SelectedIndex > 0)
        {

            string str45 = "SELECT  distinct StateId, StateName  " +
                    " FROM  StateMasterTbl where CountryId='" + ddlfiltercountary.SelectedValue + "' " +
                    " Order By StateName";

            dbss1.FillDDL1(ddlfilterstate, str45, "StateId", "StateName");



        }
        ddlfilterstate.Items.Insert(0, "ALL");
        ddlfilterstate.SelectedItem.Value = "0";
    }
    protected void ddlcountry_SelectedIndexChanged1(object sender, EventArgs e)
    {
        ddlState.Items.Clear();
        if (ddlcountry.SelectedIndex > 0)
        {
            string sgr = "SELECT StateId,StateName  ,CountryId    FROM StateMasterTbl where CountryId='" + ddlcountry.SelectedValue + "' ";
            dbss1.FillDDL1(ddlState, sgr, "StateId", "StateName");
        }
              ddlState.Items.Insert(0, "ALL");
        ddlState.Items[0].Value = "0";
    }
    protected void FillBelowGrid()
    {
        Panel1.Visible = true;
        //try
        //{
            DataTable dtn = (DataTable)(CreateTempDT());
            if (RadioButtonList1.SelectedValue == "0")
            {
                dtinvids = (DataTable)(SeachByCat());
                // Panel2.Visible = true;
            }
            else if (RadioButtonList1.SelectedValue == "1")
            {
                if (txtSearchInvName.Text.Length > 0 )
                {
                    dtinvids = (DataTable)(SearchByName());
                    // Panel2.Visible = true;
                }
                else
                {
                   
                }
            }

            else
            {

            }



            if (dtinvids.Rows.Count > 0)
            {
                

                for (int i = 0; i < dtinvids.Rows.Count; i++)
                {


                    string iwhmid = dtinvids.Rows[i]["InventoryWarehouseMasterId"].ToString();
                    string prodno = dtinvids.Rows[i]["ProductNo"].ToString();
                    string Cat = dtinvids.Rows[i]["CateAndName"].ToString();
                    string Name = dtinvids.Rows[i]["Name"].ToString();

                    DataRow addn = dtn.NewRow();
                    addn["InventoryWarehouseMasterId"] = iwhmid;
                    addn["ProductNo"] = prodno;
                    addn["CateAndName"] = Cat;
                    addn["Name"] = Name;
                    string gh = "SELECT  InvTaxabilityId ,InventoryWHM_Id ,StateId ,Taxable,countaryId,StateId,[Tax%] ,ApplyToallOnlineSales,ApplyToAllSales,InventoryTaxability.Active,TaxtName " +
                              " FROM  InventoryTaxability left outer join InventoryWarehouseMasterTbl on  InventoryTaxability.InventoryWHM_Id=InventoryWarehouseMasterTbl.InventoryWarehouseMasterId left outer join WareHouseMaster on  InventoryWarehouseMasterTbl.WareHouseId=WareHouseMaster.WareHouseId  " +
                              "where InventoryWHM_Id= '" + iwhmid + "' and WareHouseMaster.WarehouseId='" + ddlWarehouse.SelectedValue + "' and InventoryTaxability.Taxoption3id='" + ViewState["op3id"] + "' ";
                    DataTable dt3 = dbss1.cmdSelect(gh);
                    if (dt3.Rows.Count > 0)
                    {
                        ddlcountry.SelectedIndex = ddlcountry.Items.IndexOf(ddlcountry.Items.FindByValue(dt3.Rows[0]["countaryId"].ToString()));
                        ddlState.SelectedIndex = ddlState.Items.IndexOf(ddlState.Items.FindByValue(dt3.Rows[0]["StateId"].ToString()));
                        if (rdmasterrate.SelectedIndex == 0)
                        {
                            addn["Tax"] = txttrate.Text;
                         
                            if (chkonline.Checked == true)
                            {
                              
                                addn["ApplyToallOnlineSales"] = true;
                            }
                            else
                            {
                             
                                addn["ApplyToallOnlineSales"] = false;
                            }
                            if (chkboth.Checked == false)
                            {
                                addn["ApplyToAllSales"] = false;
                              
                            }
                            else
                            {
                                addn["ApplyToAllSales"] = true;
                               
                            }
                            addn["taxper"] = rdamtapp.SelectedValue;
                            
                        }
                        else
                        {
                            addn["Tax"] = dt3.Rows[0]["Tax%"].ToString();
                            addn["ApplyToAllSales"] =Convert.ToBoolean( dt3.Rows[0]["ApplyToAllSales"]);
                            addn["ApplyToallOnlineSales"] =Convert.ToBoolean(dt3.Rows[0]["ApplyToallOnlineSales"]);
                            addn["taxper"] =Convert.ToInt32(dt3.Rows[0]["Taxable"]);
                        }
                        
                        addn["Taxable"] = dt3.Rows[0]["Taxable"].ToString();
                     
                        addn["InvTaxabilityId"] = dt3.Rows[0]["InvTaxabilityId"].ToString();
                        addn["TaxtName"] = lbltname.Text;
                           addn["Active"] = dt3.Rows[0]["Active"].ToString();
                    }
                    else
                    {
                        if (rdmasterrate.SelectedIndex == 0)
                        {
                            addn["Tax"] = txttrate.Text;
                            if (chkonline.Checked == true)
                            {

                                addn["ApplyToallOnlineSales"] = true;
                            }
                            else
                            {

                                addn["ApplyToallOnlineSales"] = false;
                            }
                            if (chkboth.Checked == false)
                            {
                                addn["ApplyToAllSales"] = false;

                            }
                            else
                            {
                                addn["ApplyToAllSales"] = true;

                            }
                           
                               
                           
                        }
                        else
                        {
                            addn["Tax"] = "0.00";
                            addn["ApplyToAllSales"] = true;
                            addn["ApplyToallOnlineSales"] = false;
                        }
                        addn["taxper"] = rdamtapp.SelectedValue;
                        ddlcountry.SelectedIndex = 0;
                        ddlState.SelectedIndex = 0;
                        addn["Taxable"] = true;
                      
                        addn["InvTaxabilityId"] = "0";
                        addn["TaxtName"] = lbltname.Text;
                      
                        addn["Active"] = false;
                    }






                    dtn.Rows.Add(addn);


                }

            }
            if (dtn.Rows.Count > 0)
            {
               
                grdInvMasters0.DataSource = dtn;



                DataView myDataView = new DataView();
                myDataView = dtn.DefaultView;
                if (hdnsortExp.Value != string.Empty)
                {
                    myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
                }
                grdInvMasters0.DataBind();
               
                grdInvMasters0.Visible = true;
                
            }
            else
            {
                grdInvMasters0.DataSource = null;
                grdInvMasters0.DataBind();
            }
            if (grdInvMasters0.Rows.Count > 0)
            {
                btnSubmit.Visible = true;
            }
            else
            {
                btnSubmit.Visible = false;
            }
        //}
        //catch (Exception ett)
        //{
        //    lblmsg.Text = "error :" + ett.Message;
        //    lblmsg.Visible = true;
        //}
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        //FillBelowGrid();
    }
    protected void ddlInvName_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    //protected void imgsubmittax_Click(object sender, ImageClickEventArgs e)
    //{

    //}


    protected void filltaxgrid()
    { string fill = "";
         lblbusiness.Text ="Business : "+ ddlWarehouse.SelectedItem.Text;
        if (ddlfiltercountary.SelectedIndex > 0)
        {
            fill = " and SalesTaxOption3TaxNameTbl.CountryId='" + ddlfiltercountary.SelectedValue + "'";
        }
        if (ddlfilterstate.SelectedIndex > 0)
        {
            fill += " and SalesTaxOption3TaxNameTbl.StateId='" + ddlfilterstate.SelectedValue + "'";
        }

        string strcln = " select Distinct SalesTaxOption3TaxNameTbl.Id,SalesTaxOption3TaxNameTbl.Taxshortname,SalesTaxOption3TaxNameTbl.Taxname,Case When(CountryMaster.CountryName IS NULL) Then 'All' else CountryName End as CountryName ,Case When(StateMasterTbl.StateName IS NULL) then 'All' else StateName End as StateName from SalesTaxOption3TaxNameTbl Left join  StateMasterTbl on StateMasterTbl.StateId=SalesTaxOption3TaxNameTbl.StateId Left join CountryMaster on CountryMaster.CountryId= SalesTaxOption3TaxNameTbl.CountryId where SalesTaxOption3TaxNameTbl.Whid='" + ddlWarehouse.SelectedValue + "'" + fill + " order by CountryName,StateName, SalesTaxOption3TaxNameTbl.Taxname,SalesTaxOption3TaxNameTbl.Taxshortname";
        //string strcln = " select * from SalesTaxOption3TaxNameTbl where Whid='" + ddlWarehouse.SelectedValue + "' order by SalesTaxOption3TaxNameTbl.Taxname";
        //if (ddlProductname.SelectedIndex > 0)
        //{
        //    strcln = strcln + " and ProductMaster.ProductId='" + ddlProductname.SelectedItem.Value.ToString() + "'";
        // }

        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        gridtaxname.DataSource = dtcln;

        DataView myDataView = new DataView();
        myDataView = dtcln.DefaultView;
        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }
        gridtaxname.DataBind();
        //if (gridtaxname.Rows.Count > 0)
        //{

            //foreach (GridViewRow gv1 in gridtaxname.Rows)
            //{
            //    Label lbltid = (Label)(gv1.FindControl("lbltid"));
            //    Label lblamount = (Label)(gv1.FindControl("lblcon"));
            //    string strcln9 = " select Distinct CountryMaster.CountryName from SalesTaxOption3TaxNameTbl Left join CountryMaster on CountryMaster.CountryId= SalesTaxOption3TaxNameTbl.CountryId where SalesTaxOption3TaxNameTbl.Id='" + lbltid.Text + "'";
            //    SqlCommand cmdcln9 = new SqlCommand(strcln9, con);
            //    DataTable dtcln9 = new DataTable();
            //    SqlDataAdapter adpcln9 = new SqlDataAdapter(cmdcln9);
            //    adpcln9.Fill(dtcln9);
            //    if (dtcln9.Rows.Count > 0)
            //    {
            //        lblamount.Text = dtcln9.Rows[0]["CountryName"].ToString();
            //    }

            //    if (lblamount.Text == "")
            //    {
            //        lblamount.Text = "All";
            //    }
            //    Label lblstate = (Label)(gv1.FindControl("lblstate"));


            //    if (lblstate.Text == "")
            //    {
            //        lblstate.Text = "All";
            //    }
            //}
        //}
    }
    protected void gridtaxname_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gridtaxname.SelectedIndex = Convert.ToInt32(gridtaxname.DataKeys[e.NewEditIndex].Value.ToString());
        int i = Convert.ToInt32(gridtaxname.SelectedIndex);
        ViewState["op3id"] = i.ToString();
        string strcln = " SELECT  * from   SalesTaxOption3TaxNameTbl where Id= " + i.ToString();

        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        if (dtcln.Rows.Count > 0)
        {

            txttax.Text = dtcln.Rows[0]["Taxname"].ToString();
            txtshortname.Text = Convert.ToString(dtcln.Rows[0]["Taxshortname"]);
            ddlcountry.SelectedIndex = ddlcountry.Items.IndexOf(ddlcountry.Items.FindByValue(dtcln.Rows[0]["CountryId"].ToString()));

            ddlState.SelectedIndex = ddlState.Items.IndexOf(ddlState.Items.FindByValue(dtcln.Rows[0]["StateId"].ToString()));
            //imgsubmittax.ImageUrl = "~/ShoppingCart/images/Update.png";
            imgsubmittax.Text = "Update";
        }

    }
    protected void gridtaxname_RowCommand(object sender, GridViewCommandEventArgs e)
    {


       
        if  (e.CommandName == "delete")
        {
            

        }
        else if (e.CommandName == "setrates")
        {
            filltaxgrid();
            gridtaxname.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            int i = Convert.ToInt32(gridtaxname.SelectedDataKey.Value);
            ViewState["op3id"] = i.ToString();
            lblbn.Text = ddlWarehouse.SelectedItem.Text;
            
            gridtaxname.Rows[gridtaxname.SelectedIndex].BackColor = System.Drawing.Color.Yellow;
            string strcl = "select Distinct SalesTaxOption3TaxNameTbl.Id,SalesTaxOption3TaxNameTbl.Taxname,CountryMaster.CountryName,StateMasterTbl.StateName from SalesTaxOption3TaxNameTbl Left join  StateMasterTbl on StateMasterTbl.StateId=SalesTaxOption3TaxNameTbl.StateId Left join CountryMaster on CountryMaster.CountryId= StateMasterTbl.CountryId where SalesTaxOption3TaxNameTbl. Id= " + i.ToString();

            SqlCommand cmdcl = new SqlCommand(strcl, con);
            DataTable dtcl = new DataTable();
            SqlDataAdapter adpcl = new SqlDataAdapter(cmdcl);
            adpcl.Fill(dtcl);
            if (dtcl.Rows.Count > 0)
            {
                lbltname.Text = dtcl.Rows[0]["Taxname"].ToString();

                lblcouname.Text = dtcl.Rows[0]["CountryName"].ToString();
                if (lblcouname.Text == "")
                {
                    lblcouname.Text = "All";
                }
                lblstatename.Text = dtcl.Rows[0]["StateName"].ToString();
                if (lblstatename.Text == "")
                {
                    lblstatename.Text = "All";
                }
                Panel3.Visible = true;
                pnlInvCat.Visible = true;
                ImgBtnSearchGo.Visible = true;
                Panel1.Visible = false;
                grdInvMasters0.DataSource = null;
                grdInvMasters0.DataBind();
                btnSubmit.Visible = false;
                rdmasterrate.SelectedIndex = 1;
                rdmasterrate_SelectedIndexChanged(sender, e);
            }
        }

    }
    protected void gridtaxname_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        gridtaxname.SelectedIndex = Convert.ToInt32(gridtaxname.DataKeys[e.RowIndex].Value.ToString());
        int i = Convert.ToInt32(gridtaxname.SelectedIndex);
        ViewState["op3id"] = i.ToString();
        string chkid = "  select SalesOrderTempTax.* from SalesOrderTempTax inner join InventoryTaxability on InventoryTaxability.InvTaxabilityId=SalesOrderTempTax.InvTaxabilityID inner join SalesTaxOption3TaxNameTbl on SalesTaxOption3TaxNameTbl.Id=InventoryTaxability.Taxoption3id where SalesTaxOption3TaxNameTbl.Id=" + i.ToString() + "";

        SqlCommand cmchkid = new SqlCommand(chkid, con);
        DataTable dtchkid = new DataTable();
        SqlDataAdapter adchkid = new SqlDataAdapter(cmchkid);
        adchkid.Fill(dtchkid);
        if (dtchkid.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "You can not delete this Tax entry as some order /invoice exist for this tax entry. However you can make this entry inactive to stop using this tax entry for your sales or edit the entry for change in name or tax rates etc.";
        }
        else
        {
          
          
            string saa = "select * from SalesTaxOption3TaxNameTbl where  Id=" + i.ToString() + "";
            DataTable dtty = dbss1.cmdSelect(saa);
            if (dtty.Rows.Count > 0)
            {
                string str22 = "delete from SalesTaxOption3TaxNameTbl where Id=" + i.ToString() + "";
                SqlCommand cmd = new SqlCommand(str22, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                con.Close();

                string str1113 = "select Id  from AccountMaster where Whid='" + ddlWarehouse.SelectedValue + "' and AccountId='" + dtty.Rows[0]["PurchaseTaxAccountMasterID"] + "'";
                SqlCommand cmd1113 = new SqlCommand(str1113, con);
                SqlDataAdapter adp1113 = new SqlDataAdapter(cmd1113);
                DataTable ds1113 = new DataTable();
                adp1113.Fill(ds1113);
                if (ds1113.Rows.Count > 0)
                {
                    string strpqq = "delete  from AccountBalance where AccountMasterId='" + ds1113.Rows[0]["Id"] + "'";
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    SqlCommand dcmpqer = new SqlCommand(strpqq, con);
                    dcmpqer.ExecuteNonQuery();
                    con.Close();
                }

                string str1w = "select Id  from AccountMaster where Whid='" + ddlWarehouse.SelectedValue + "' and AccountId='" + dtty.Rows[0]["TaxInvAccountMasterID"] + "'";

                SqlDataAdapter adpzx = new SqlDataAdapter(str1w, con);
                DataTable dfg = new DataTable();
                adpzx.Fill(dfg);
                if (dfg.Rows.Count > 0)
                {
                    string strpqq = "delete  from AccountBalance where AccountMasterId='" + dfg.Rows[0]["Id"] + "'";
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    SqlCommand dcmpqer = new SqlCommand(strpqq, con);
                    dcmpqer.ExecuteNonQuery();
                    con.Close();
                }

                string strperd = "delete  from AccountMaster where Whid='" + dtty.Rows[0]["Whid"] + "' and AccountId='" + dtty.Rows[0]["PurchaseTaxAccountMasterID"] + "'";
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                SqlCommand dcmper = new SqlCommand(strperd, con);
                dcmper.ExecuteNonQuery();
                con.Close();
                string strperds = "delete  from AccountMaster where Whid='" + dtty.Rows[0]["Whid"] + "' and AccountId='" + dtty.Rows[0]["TaxInvAccountMasterID"] + "'";
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                SqlCommand dcmpers = new SqlCommand(strperds, con);
                dcmpers.ExecuteNonQuery();
                con.Close();
                lblmsg.Visible = true;
                lblmsg.Text = "Record deleted successfully";

            }
            filltaxgrid();
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["wz"] != null)
        {
            Response.Redirect("StoreTaxmethodtbl.aspx");
        }
        else
        {
            Response.Redirect("wzStoreTaxmethodtbl.aspx");
        }
    }
    protected void chkAll_chachedChanged(object sender, EventArgs e)
    {
        CheckBox chk;
        foreach (GridViewRow rowitem in grdInvMasters0.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBox1"));
            chk.Checked = ((CheckBox)sender).Checked;

        }
    }
    protected void chkAll1_chachedChanged(object sender, EventArgs e)
    {
        CheckBox chk;
        foreach (GridViewRow rowitem in grdInvMasters0.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("Chec"));
            chk.Checked = ((CheckBox)sender).Checked;

        }
    }

    protected void chkAll2_chachedChanged(object sender, EventArgs e)
    {
        CheckBox chk;
        foreach (GridViewRow rowitem in grdInvMasters0.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckB"));
            chk.Checked = ((CheckBox)sender).Checked;

        }
    }


    protected void chkAll3_chachedChanged(object sender, EventArgs e)
    {
        CheckBox chk;
        foreach (GridViewRow rowitem in grdInvMasters0.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("chtstasus1"));
            chk.Checked = ((CheckBox)sender).Checked;

        }
    }


    protected DataTable select(string qu)
    {
        SqlCommand cmd = new SqlCommand(qu, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        return dt;

    }
    protected string acccc(string accgenid)
    {

        DataTable dtrs = select("select AccountId from AccountMaster where AccountId='" + accgenid + "' and Whid='" + ViewState["Wid"] + "'");
        if (dtrs.Rows.Count > 0)
        {
            accgenid = Convert.ToString(Convert.ToInt32(accgenid) + 1);
            acccc(accgenid);
        }
        return accgenid;
    }
    protected void imgsubmittax_Click(object sender, EventArgs e)
    {
        string st153 = "select Report_Period_Id  from ReportPeriod where Compid='" + Session["comid"] + "' and Whid='" +ddlWarehouse.SelectedValue + "' and Active='1'";
        SqlCommand cmd153 = new SqlCommand(st153, con);
        SqlDataAdapter adp153 = new SqlDataAdapter(cmd153);
        DataTable ds153 = new DataTable();
        adp153.Fill(ds153);
        Session["reportid"] = ds153.Rows[0]["Report_Period_Id"].ToString();

        string accper = "  select max(AccountId) as AccountId from AccountMaster where Whid='" + ddlWarehouse.SelectedValue + "' and GroupId=5";
        SqlDataAdapter aqla = new SqlDataAdapter(accper, con);

        DataTable asper = new DataTable();
        aqla.Fill(asper);
        if (Convert.ToString(asper.Rows[0]["AccountId"]) != "")
        {
            puraccid = Convert.ToInt32(asper.Rows[0]["AccountId"])+1;
            puraccid = Convert.ToInt32(acccc(puraccid.ToString()));
        }
        else
        {
            if (puraccid >= 1999)
            {
                if (puraccid > 17000)
                {
                    puraccid += 1;
                    puraccid = Convert.ToInt32(acccc(puraccid.ToString()));
                }
                else
                {
                    puraccid = 17000;
                    puraccid = Convert.ToInt32(acccc(puraccid.ToString()));
                }
            }
            else
            {
                if (puraccid == 0)
                {
                    puraccid = 1700;
                    puraccid = Convert.ToInt32(acccc(puraccid.ToString()));
                }
                else
                {
                    puraccid = puraccid + 1;
                    puraccid = Convert.ToInt32(acccc(puraccid.ToString()));
                }
            }
        }
        string peracname = "" + txttax.Text + " Purchases";
        string svm = "And Id<>'" + ViewState["op3id"] + "'";
        if (imgsubmittax.Text != "Update")
        {
        }
        int accountid = 0;

        string strclnm = " select * from SalesTaxOption3TaxNameTbl where (CountryId='0')  and Whid='" + ddlWarehouse.SelectedValue + "'" + " " + svm + "";
        SqlCommand cmdclnm = new SqlCommand(strclnm, con);
        DataTable dtclnm = new DataTable();
        SqlDataAdapter adpclnm = new SqlDataAdapter(cmdclnm);
        adpclnm.Fill(dtclnm);
        if (dtclnm.Rows.Count < 3)
        {
            string st = " select * from SalesTaxOption3TaxNameTbl where (CountryId='" + ddlcountry.SelectedValue + "') and Whid='" + ddlWarehouse.SelectedValue + "'" + " " + svm + "";
            SqlCommand cm = new SqlCommand(st, con);
            DataTable dtc = new DataTable();
            SqlDataAdapter ad11 = new SqlDataAdapter(cm);
            ad11.Fill(dtc);
            if (dtc.Rows.Count < 3)
            {



                if (imgsubmittax.Text != "Update")
                {
                    string strcln2 = " select * from SalesTaxOption3TaxNameTbl where Whid='" + ddlWarehouse.SelectedValue + "' and TaxName='" + txttax.Text + "'";
                    SqlCommand cmdcln2 = new SqlCommand(strcln2, con);
                    DataTable dtcln2 = new DataTable();
                    SqlDataAdapter adpcln2 = new SqlDataAdapter(cmdcln2);
                    adpcln2.Fill(dtcln2);
                    if (dtcln2.Rows.Count <= 0)
                    {

                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        string strcln = " select * from SalesTaxOption3TaxNameTbl where (CountryId='" + ddlcountry.SelectedValue + "' or CountryId='0') and  (StateId='" + ddlState.SelectedValue + "' or stateId='0') and Whid='" + ddlWarehouse.SelectedValue + "'";
                        SqlCommand cmdcln = new SqlCommand(strcln, con);
                        DataTable dtcln = new DataTable();
                        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
                        adpcln.Fill(dtcln);
                        if (dtcln.Rows.Count >= 3)
                        {
                            lblmsg.Visible = true;
                            lblmsg.Text = "You can only have three active taxes for that particular country/state";


                        }
                        else
                        {
                            string acid = "  select max(AccountId) as AccountId from AccountMaster where Whid='" + ddlWarehouse.SelectedValue + "' and GroupId=17";
                            SqlDataAdapter aacid = new SqlDataAdapter(acid, con);

                            DataTable adtc = new DataTable();
                            aacid.Fill(adtc);
                            if (adtc.Rows.Count > 0)
                            {
                                if (adtc.Rows[0]["AccountId"].ToString() == "")
                                {
                                    accountid = 3000;
                                    accountid = Convert.ToInt32(acccc(accountid.ToString()));
                                }
                                else
                                {
                                    accountid = (Convert.ToInt32(adtc.Rows[0]["AccountId"].ToString()) + 1);
                                    accountid = Convert.ToInt32(acccc(accountid.ToString()));
                                }
                                string accname = "" + txttax.Text + " payable";
                                string str222 = "Insert into AccountMaster(ClassId,GroupId,AccountId,AccountName,Description,Balance,Balanceoflastyear,DateTimeOfLastUpdatedBalance,Status,compid,Whid)" +
                                  "values('5','17','" + accountid + "','" + accname + "','" + accname + "','0','0','" + Convert.ToDateTime(DateTime.Now.ToString()) + "','1','" + compid + "','" + ddlWarehouse.SelectedValue + "')";

                                SqlCommand cmd1 = new SqlCommand(str222, con);
                                if (con.State.ToString() != "Open")
                                {
                                    con.Open();
                                }
                                cmd1.ExecuteNonQuery();
                                con.Close();

                            }
                            else
                            {

                                accountid = 3000;
                                accountid = Convert.ToInt32(acccc(accountid.ToString()));
                                string accname = "'" + txttax.Text + "' payable";
                                string str222 = "Insert into AccountMaster(ClassId,GroupId,AccountId,AccountName,Description,Balance,Balanceoflastyear,Status,compid,Whid)" +
                                  "values('5','17','" + accountid + "','" + accname + "','" + accname + "','0','0','1','" + compid + "','" + ddlWarehouse.SelectedValue + "')";

                                SqlCommand cmd1 = new SqlCommand(str222, con);
                                if (con.State.ToString() != "Open")
                                {
                                    con.Open();
                                }
                                cmd1.ExecuteNonQuery();
                                con.Close();


                            }
                            string str1113 = "select max(Id) as Aid from AccountMaster where Whid='" + ddlWarehouse.SelectedValue + "'";
                            SqlCommand cmd1113 = new SqlCommand(str1113, con);
                            SqlDataAdapter adp1113 = new SqlDataAdapter(cmd1113);
                            DataTable ds1113 = new DataTable();
                            adp1113.Fill(ds1113);
                            Session["maxaid"] = ds1113.Rows[0]["Aid"].ToString();

                            string str456 = "insert into AccountBalance(AccountMasterId,Balance,Report_Period_Id) values ('" + Session["maxaid"].ToString() + "','" + 0 + "','" + Session["reportid"].ToString() + "')";
                            SqlCommand cmd456 = new SqlCommand(str456, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmd456.ExecuteNonQuery();
                            con.Close();
                            string stper = "Insert into AccountMaster(ClassId,GroupId,AccountId,AccountName,Description,Balance,Balanceoflastyear,DateTimeOfLastUpdatedBalance,Status,compid,Whid)" +
                        "values('1','5','" + puraccid + "','" + peracname + "','" + peracname + "','0','0','" + Convert.ToDateTime(DateTime.Now.ToString()) + "','1','" + compid + "','" + ddlWarehouse.SelectedValue + "')";

                            SqlCommand cmdper = new SqlCommand(stper, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmdper.ExecuteNonQuery();
                            con.Close();

                            string strz = "select max(Id) as Aid from AccountMaster where Whid='" + ddlWarehouse.SelectedValue + "'";
                            SqlCommand cmd1z = new SqlCommand(strz, con);
                            SqlDataAdapter adpz = new SqlDataAdapter(cmd1z);
                            DataTable dsz = new DataTable();
                            adpz.Fill(dsz);
                            Session["maxaid"] = dsz.Rows[0]["Aid"].ToString();

                            string str4z = "insert into AccountBalance(AccountMasterId,Balance,Report_Period_Id) values ('" + Session["maxaid"].ToString() + "','" + 0 + "','" + Session["reportid"].ToString() + "')";
                            SqlCommand cmd456z = new SqlCommand(str4z, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmd456z.ExecuteNonQuery();
                            con.Close();

                            string str22 = "Insert into SalesTaxOption3TaxNameTbl(CountryId,StateId,Taxname,Whid,TaxInvAccountMasterID,PurchaseTaxAccountMasterID,Taxshortname)" +
                            "values('" + ddlcountry.SelectedValue + "','" + ddlState.SelectedValue + "','" + txttax.Text + "','" + ddlWarehouse.SelectedValue + "','" + accountid + "','" + puraccid + "','" + txtshortname.Text + "')";

                            SqlCommand cmd = new SqlCommand(str22, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmd.ExecuteNonQuery();
                            con.Close();
                            lblmsg.Visible = true;
                            lblmsg.Text = "Record inserted successfully ";


                        }
                    }
                    else
                    {
                        //imgsubmittax.ImageUrl = "~/ShoppingCart/images/Submit.png";
                        imgsubmittax.Text = "Submit";
                        lblmsg.Visible = true;
                        lblmsg.Text = "Record already exists";
                    }
                }
                else
                {
                    string strcln = " select * from SalesTaxOption3TaxNameTbl where (CountryId='" + ddlcountry.SelectedValue + "' or CountryId='0' or CountryId is null) and  (StateId='" + ddlState.SelectedValue + "' or stateId='0' or stateId is null) and Whid='" + ddlWarehouse.SelectedValue + "' And Id<>'" + ViewState["op3id"] + "'";
                    SqlCommand cmdcln = new SqlCommand(strcln, con);
                    DataTable dtcln = new DataTable();
                    SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
                    adpcln.Fill(dtcln);
                    if (dtcln.Rows.Count >= 3)
                    {
                       
                        lblmsg.Visible = true;
                        lblmsg.Text = "You can only have three active taxes for that particular country/state";

                    }
                    else
                    {
                        string strcln2 = " select * from SalesTaxOption3TaxNameTbl where Whid='" + ddlWarehouse.SelectedValue + "' and TaxName='" + txttax.Text + "' and Id<>'" + ViewState["op3id"] + "'";
                        SqlCommand cmdcln2 = new SqlCommand(strcln2, con);
                        DataTable dtcln2 = new DataTable();
                        SqlDataAdapter adpcln2 = new SqlDataAdapter(cmdcln2);
                        adpcln2.Fill(dtcln2);
                        if (dtcln2.Rows.Count == 0)
                        {

                            string selectAcid = "select TaxInvAccountMasterID,PurchaseTaxAccountMasterID from   SalesTaxOption3TaxNameTbl where Id ='" + ViewState["op3id"] + "'";
                            DataTable dtchekrcdAc = dbss1.cmdSelect(selectAcid);
                            string accname = "" + txttax.Text + " payable";
                            string accountidedit = "update AccountMaster set AccountName='" + accname + "' where Whid='"+ddlWarehouse.SelectedValue+"' and [AccountId]='" + Convert.ToInt32(dtchekrcdAc.Rows[0]["TaxInvAccountMasterID"].ToString()) + "'";
                            bool ik1 = dbss1.cmdInsUpdateDelete(accountidedit);
                            string peracc = "" + txttax.Text + " Purchases";
                            string acpe = "update AccountMaster set AccountName='" + peracc + "' where Whid='" + ddlWarehouse.SelectedValue + "' and  [AccountId]='" + Convert.ToString(dtchekrcdAc.Rows[0]["PurchaseTaxAccountMasterID"]) + "'";
                            bool ikper = dbss1.cmdInsUpdateDelete(acpe);


                            string str22 = "Update SalesTaxOption3TaxNameTbl Set Taxshortname='"+txtshortname.Text+"', CountryId='" + ddlcountry.SelectedValue + "',StateId='" + ddlState.SelectedValue + "',Taxname='" + txttax.Text + "' where Id='" + ViewState["op3id"] + "'";

                            SqlCommand cmd = new SqlCommand(str22, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmd.ExecuteNonQuery();
                            con.Close();
                            lblmsg.Visible = true;
                            //  imgsubmittax.ImageUrl = "~/ShoppingCart/images/Submit.png";
                            imgsubmittax.Text = "Submit";
                            lblmsg.Text = "Record updated successfully";
                        }
                        else
                        {
                            // imgsubmittax.ImageUrl = "~/ShoppingCart/images/Submit.png";
                            imgsubmittax.Text = "Submit";
                            lblmsg.Visible = true;
                            lblmsg.Text = "Record already exists";
                        }
                    }
                }
                txttax.Text = "";
                txtshortname.Text = "";
                ddlState.SelectedIndex = 0;
                ddlcountry.SelectedIndex = 0;
                filltaxgrid();


                //    }
                //    else
                //    {
                //        imgsubmittax.ImageUrl = "~/ShoppingCart/images/Submit.png";
                //        imgsubmittax.AlternateText = "Submit";
                //        lblmsg.Visible = true;
                //        lblmsg.Text = "You have only three tax use in perticular country/state..";
                //    }
                //}
                //else
                //{
                //    imgsubmittax.ImageUrl = "~/ShoppingCart/images/Submit.png";
                //    imgsubmittax.AlternateText = "Submit";
                //    lblmsg.Visible = true;
                //    lblmsg.Text = "You have only three tax use in perticular country/state..";
                //}
            }


            else
            {
                lblmsg.Visible = true;
                lblmsg.Text = "You have only three tax use in perticular country/state";

            }

        }

        else
        {
            lblmsg.Visible = true;
            lblmsg.Text = "You have only three tax use in perticular country/state";

        }
    }
    protected void ImgBtnSearchGo_Click(object sender, EventArgs e)
    {
        lblmsgrange.Text = "";
        int flag = 0;
        if (rdmasterrate.SelectedIndex == 0)
        {
            if (rdamtapp.SelectedIndex == 0)
            {
                if (Convert.ToDecimal(txttrate.Text) > Convert.ToDecimal(100))
                {
                    flag = 1;
                }
            }
        }
        if (flag == 0)
        {
            FillBelowGrid();
        }
        else
        {
            lblmsgrange.Text = "Up to 100%";
        }
        // btnSearchGo_Click(sender, ee);
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (grdInvMasters0.Rows.Count > 0)
        {
            int flag = 0;
            foreach (GridViewRow g2 in grdInvMasters0.Rows)
            {

                TextBox tax = (TextBox)(g2.FindControl("txtTaxinper"));
                Label lblgtrate = (Label)g2.FindControl("lblgtrate");
                RadioButtonList rdamtappgd = (RadioButtonList)(g2.FindControl("rdamtappgd"));
                lblgtrate.Text = "";
                if (rdamtappgd.SelectedIndex == 0)
                {

                    if (Convert.ToDecimal(tax.Text) > Convert.ToDecimal(100))
                    {
                        flag = 1;
                        lblgtrate.Text = "Up to 100%";
                    }
                }
            }

            if (flag == 0)
            {
                int y = 0;
                int l = 0;

                //try
                //{
                foreach (GridViewRow g2 in grdInvMasters0.Rows)
                {
                    Label iwhmid = (Label)g2.FindControl("lblinvWHMid");
                    CheckBox chk = (CheckBox)(g2.FindControl("CheckBox1"));
                    CheckBox cksales = (CheckBox)(g2.FindControl("Chec"));

                    TextBox tax = (TextBox)(g2.FindControl("txtTaxinper"));
                    Label taxabilityid = (Label)g2.FindControl("lbltaxabilityid");
                    RadioButtonList rdamtappgd = (RadioButtonList)(g2.FindControl("rdamtappgd"));
                    CheckBox ckonline = (CheckBox)(g2.FindControl("CheckB"));
                    CheckBox chactive = (CheckBox)(g2.FindControl("chtstasus1"));
                    Label taxname = (Label)g2.FindControl("txtname");
                    if (rdamtappgd.SelectedIndex == 0)
                    {
                        chk.Checked = true;
                    }
                    else
                    {
                        chk.Checked = false;
                    }
                    //string gh = "SELECT  InvTaxabilityId ,InventoryWHM_Id ,StateId ,Taxable      ,[Tax%] " +
                    //           " FROM  InventoryTaxability  where InventoryWHM_Id= '" + iwhmid.Text + "' and StateId='"+ddlState.SelectedValue+"'";

                    string gh = "SELECT  InvTaxabilityId,InventoryWHM_Id ,StateId ,Taxable      ,[Tax%] " +
                               " FROM  InventoryTaxability left outer join InventoryWarehouseMasterTbl on  InventoryTaxability.InventoryWHM_Id=InventoryWarehouseMasterTbl.InventoryWarehouseMasterId left outer join WareHouseMaster on  InventoryWarehouseMasterTbl.WareHouseId=WareHouseMaster.WareHouseId " +
                               "where InventoryWHM_Id= '" + iwhmid.Text + "'  and WareHouseMaster.WarehouseId='" + ddlWarehouse.SelectedValue + "' and InventoryTaxability.Taxoption3id='" + ViewState["op3id"] + "'";
                    DataTable dt3 = dbss1.cmdSelect(gh);
                    if (dt3.Rows.Count > 0)
                    {
                       
                        string updaterec = " update InventoryTaxability set StateId='" + ddlState.SelectedItem.Value + "',countaryId='" + ddlcountry.SelectedItem.Value + "',Taxable='" + chk.Checked + "' ,[Tax%]='" + tax.Text + "',ApplyToallOnlineSales='" + ckonline.Checked + "',ApplyToAllSales='" + cksales.Checked + "',Active='" + chactive.Checked + "',TaxtName='" + taxname.Text + "' " +
                            " where InvTaxabilityId='" + dt3.Rows[0]["InvTaxabilityId"].ToString() + "' ";
                        bool i = dbss1.cmdInsUpdateDelete(updaterec);
                        if (i == true)
                        {
                            y += 1;
                        }

                    }
                    else
                    {


                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        if (chactive.Checked == true)
                        {
                            string insertrec = " insert InventoryTaxability values('" + iwhmid.Text + "', " +
                            " '" + ddlState.SelectedValue + "','" + chk.Checked + "','" + tax.Text + "','" + ddlcountry.SelectedValue + "','" + ckonline.Checked + "','" + cksales.Checked + "','" + chactive.Checked + "','" + taxname.Text + "','" + ViewState["op3id"] + "') ";
                            bool i = dbss1.cmdInsUpdateDelete(insertrec);
                            if (i == true)
                            {
                                l += 1;
                            }
                        }

                    }


                }
                lblmsg.Visible = true;
               // lblmsg.Text = "'" + y + "'" + " : Record Updated, " + "'" + l + "'" + " : Record Inserted ";
               lblmsg.Text = "Record updated successfully";
          
                
                //}
                //catch (Exception et)
                //{
                //    lblmsg.Visible = true;
                //    lblmsg.Text = "Error " + et.Message;
                //}
                grdInvMasters0.DataSource = null;
                grdInvMasters0.DataBind();
                RadioButtonList1_SelectedIndexChanged(sender, e);
                Panel3.Visible = false;
                filltaxgrid();
            }
        }

    }


    protected void gridtaxname_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        filltaxgrid();

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
    protected void Button2_Click1(object sender, EventArgs e)
    {
        if (Button2.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");

            Button2.Text = "Hide Printable Version";
            Button1.Visible = true;
            if (gridtaxname.Columns[5].Visible == true)
            {
                ViewState["editHide"] = "tt";
                gridtaxname.Columns[5].Visible = false;
            }
            if (gridtaxname.Columns[6].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                gridtaxname.Columns[6].Visible = false;
            }
            if (gridtaxname.Columns[7].Visible == true)
            {
                ViewState["viewHide"] = "tt";
                gridtaxname.Columns[7].Visible = false;
            }
        }
        else
        {

            //pnlgrid.ScrollBars = ScrollBars.Vertical;
            //pnlgrid.Height = new Unit(250);

            Button2.Text = "Printable Version";
            Button1.Visible = false;
            if (ViewState["editHide"] != null)
            {
                gridtaxname.Columns[5].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                gridtaxname.Columns[6].Visible = true;
            }
            if (ViewState["viewHide"] != null)
            {
                gridtaxname.Columns[7].Visible = true;
            }
        }
    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["wz"] != null)
        {
            Response.Redirect("StoreTaxmethodtbl.aspx");
        }
        else
        {
            Response.Redirect("wzStoreTaxmethodtbl.aspx");
        }
    }
    protected void ddlfiltercountary_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillfistate();
    }
    protected void btngo_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        Panel3.Visible = false;
        filltaxgrid();
    }
    protected void rdmasterrate_SelectedIndexChanged(object sender, EventArgs e)
    {
        txttrate.Text = "0";
        rdamtapp.SelectedIndex = 0;
        chkonline.Checked = false;
        chkboth.Checked = false;
        if (rdmasterrate.SelectedIndex == 0)
        {
            pnltaxex.Visible = true;
        }
        else
        {
            pnltaxex.Visible = false;
        }
    }

    protected void btncan_Click(object sender, EventArgs e)
    {
        txtshortname.Text = "";
        txttax.Text = "";
        imgsubmittax.Text = "Submit";
        ddlcountry.SelectedIndex = 0;
        ddlState.SelectedIndex = 0;
    }
    protected void txttax_TextChanged(object sender, EventArgs e)
    {
        if (txttax.Text.Length > 0)
        {
            if (txttax.Text.Length ==1)
            {
            txtshortname.Text = txttax.Text.Substring(0,1);
            }
            else if (txttax.Text.Length == 2)
            {
                txtshortname.Text = txttax.Text.Substring(0, 2);
            }
            else if (txttax.Text.Length >= 3)
            {
                txtshortname.Text = txttax.Text.Substring(0, 3);
            }

        }
        else
        {
            txtshortname.Text = "";
        }
    }
    protected void btngcanc_Click(object sender, EventArgs e)
    {
        grdInvMasters0.DataSource = null;
        grdInvMasters0.DataBind();
        RadioButtonList1_SelectedIndexChanged(sender, e);
        Panel3.Visible = false;
        filltaxgrid();
    }
    protected void grdInvMasters0_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        FillBelowGrid();
    }
}