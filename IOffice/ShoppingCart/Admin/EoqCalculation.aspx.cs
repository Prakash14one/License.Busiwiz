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

public partial class EoqCalculation : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(PageConn.connnn);

    string compid;

    protected void Page_Load(object sender, EventArgs e)
    {

        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();
        compid = Session["Comid"].ToString();
        char[] separator = new char[] { '/' };
        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();

        Page.Title = pg.getPageTitle(page);


        if (!IsPostBack)
        {
            ViewState["sortOrder"] = "";

            fillstore();
            datefill();
            fillgrid();
            savecheck();

        }
    }
    protected void fillstore()
    {
        ddlWarehouse.Items.Clear();
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


    }
    protected void fillgrid()
    {

        lblstore.Text = ddlWarehouse.SelectedItem.Text;

        string str = "SELECT   InventoryMaster.InventoryMasterId, Left(InventoryMaster.Name,30) as Name,Left(InventoryCategoryMaster.InventoryCatName,15) AS CategoryName,case when (InventoryMaster.MasterActiveStatus='1') then 'Active' else 'Inactive' End as Statuslabel, " +
                                     "  Left(InventoruSubSubCategory.InventorySubSubName,15) AS SubSubCategoryName, Left(InventorySubCategoryMaster.InventorySubCatName,15) AS SubCategoryName,  " +
                                     "  InventoryMaster.ProductNo, InventoryCategoryMaster.InventeroyCatId,UnitTypeMaster.Name as unitname,InventoryBarcodeMaster.Barcode, InventorySubCategoryMaster.InventorySubCatId, " +
                                     "  InventoruSubSubCategory.InventorySubSubId, InventoryWarehouseMasterTbl.Active, InventoryWarehouseMasterTbl.InventoryWarehouseMasterId,  " +
                                     "  InventoryWarehouseMasterTbl.Rate, InventoryWarehouseMasterTbl.Weight,Cast(InventoryWarehouseMasterTbl.Weight as nvarchar(50))+':'+UnitTypeMaster.Name as weightunitname ,InventoryWarehouseMasterTbl.QtyOnHand, InventoryWarehouseMasterTbl.WareHouseId,InventorySizeMaster.Volume,InventoryWarehouseMasterTbl.PreferredVendorId " +
                                     " FROM         InventoryMaster Inner JOIN " +
                                     "  InventoryWarehouseMasterTbl ON InventoryMaster.InventoryMasterId = InventoryWarehouseMasterTbl.InventoryMasterId inner JOIN " +
                                     "  InventoruSubSubCategory INNER JOIN " +
                                     "  InventoryCategoryMaster INNER JOIN " +
                                     "  InventorySubCategoryMaster ON InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId ON  " +
                                     "  InventoruSubSubCategory.InventorySubCatID = InventorySubCategoryMaster.InventorySubCatId ON  " +
                                     "  InventoryMaster.InventorySubSubId = InventoruSubSubCategory.InventorySubSubId left outer join InventoryMasterMNC on InventoryMaster.InventoryMasterId = InventoryMasterMNC.Inventorymasterid LEFT OUTER JOIN      InventoryDetails ON InventoryMaster.InventoryDetailsId = InventoryDetails.Inventory_Details_Id LEFT OUTER JOIN      InventoryBarcodeMaster ON InventoryMaster.InventoryMasterId = InventoryBarcodeMaster.InventoryMaster_id inner join UnitTypeMaster on UnitTypeMaster.UnitTypeId = InventoryDetails.UnitTypeId " +
                                     " left outer join InventorySizeMaster on InventorySizeMaster.InventoryMasterId=InventoryMaster.InventoryMasterId where  InventoryWarehouseMasterTbl.WareHouseId='" + Convert.ToInt32(ddlWarehouse.SelectedValue) + "' and InventoryMaster.CatType IS Null ";

        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);

        GridView1.DataSource = dt;
        GridView1.DataBind();





    }
    protected void datefill()
    {
        string openingdate = "select Report_Period_Id,StartDate,EndDate from ReportPeriod where Compid='" + Session["Comid"].ToString() + "' and Whid='" + Convert.ToInt32(ddlWarehouse.SelectedValue) + "' and Active='1'";
        SqlCommand cmd22221 = new SqlCommand(openingdate, con);
        SqlDataAdapter adp22221 = new SqlDataAdapter(cmd22221);
        DataTable ds112221 = new DataTable();
        adp22221.Fill(ds112221);

        if (ds112221.Rows.Count > 0)
        {
            DateTime t1;
            DateTime t2;

            t1 = Convert.ToDateTime(ds112221.Rows[0]["StartDate"].ToString());
            t2 = Convert.ToDateTime(ds112221.Rows[0]["EndDate"].ToString());

            ViewState["CurrentYearStartdate"] = t1.ToShortDateString();
            ViewState["CurrentYearEnddate"] = t2.ToShortDateString();
            ViewState["Id"] = ds112221.Rows[0]["Report_Period_Id"].ToString();

            lblcurrentaccountstart.Text = t1.ToShortDateString();
            lblcurrentaccountend.Text = t2.ToShortDateString();



        }

    }
    protected DataTable select(string str)
    {
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);
        return dt;


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
    protected void ddlWarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        datefill();
        fillgrid();
        savecheck();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlsitename = (DropDownList)e.Row.FindControl("ddlsitename");
            Label lblsitevolumecapacity = (Label)e.Row.FindControl("lblsitevolumecapacity");
            Label lblsitevolumecapacityfordisplay = (Label)e.Row.FindControl("lblsitevolumecapacityfordisplay");


            Label lblsitestoragecostperyear = (Label)e.Row.FindControl("lblsitestoragecostperyear");
            Label lblinvwarehouseid = (Label)e.Row.FindControl("lblinvwarehouseid");
            Label lblyearlyavgstock = (Label)e.Row.FindControl("lblyearlyavgstock");

            Label lblproductvoulme = (Label)e.Row.FindControl("lblproductvoulme");

            Label lblavgstockvolume = (Label)e.Row.FindControl("lblavgstockvolume");
            Label lblavgstockvolumefordisplay = (Label)e.Row.FindControl("lblavgstockvolumefordisplay");

            Label lblproductusageofsitevolumepercent = (Label)e.Row.FindControl("lblproductusageofsitevolumepercent");
            Label lblcarringcostproductperyear = (Label)e.Row.FindControl("lblcarringcostproductperyear");
            Label lblcarringcostperunit = (Label)e.Row.FindControl("lblcarringcostperunit");
            Label lblprefferedvendorid = (Label)e.Row.FindControl("lblprefferedvendorid");
            Label lblorderingcostlabel = (Label)e.Row.FindControl("lblorderingcostlabel");
            Label lblorderingcost = (Label)e.Row.FindControl("lblorderingcost");

            Label lbleoq = (Label)e.Row.FindControl("lbleoq");
            Label lbleoqtext = (Label)e.Row.FindControl("lbleoqtext");


            string streoqchk = "select * from EoqCalculation where invwhid='" + lblinvwarehouseid.Text + "' and Whid='" + ddlWarehouse.SelectedValue + "' and Accountyearid='" + ViewState["Id"].ToString() + "'";
            SqlCommand cmdeoqchk = new SqlCommand(streoqchk, con);
            SqlDataAdapter adpeoqchk = new SqlDataAdapter(cmdeoqchk);
            DataTable dteoqchk = new DataTable();
            adpeoqchk.Fill(dteoqchk);

            if (dteoqchk.Rows.Count > 0)
            {
                lblyearlyavgstock.Text = dteoqchk.Rows[0]["yearlyavgstock"].ToString();
                lblavgstockvolume.Text = dteoqchk.Rows[0]["avgstockvolume"].ToString();

                //lblavgstockvolumefordisplay.Text = dteoqchk.Rows[0]["avgstockvolume"].ToString();
                if (Convert.ToDouble(dteoqchk.Rows[0]["avgstockvolume"].ToString()) == 0)
                {
                    lblavgstockvolumefordisplay.Text = Convert.ToDouble(dteoqchk.Rows[0]["avgstockvolume"].ToString()).ToString();
                }
                else
                {
                    lblavgstockvolumefordisplay.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(dteoqchk.Rows[0]["avgstockvolume"].ToString())), 2).ToString("###,###.##");
                }

                string strsite = "select * from InventorySiteMasterTbl where WarehouseID='" + ddlWarehouse.SelectedValue + "' order by InventorySiteName ";
                SqlCommand cmdsite = new SqlCommand(strsite, con);
                SqlDataAdapter adpsite = new SqlDataAdapter(cmdsite);
                DataTable dtsite = new DataTable();
                adpsite.Fill(dtsite);

                ddlsitename.DataSource = dtsite;
                ddlsitename.DataTextField = "InventorySiteName";
                ddlsitename.DataValueField = "InventorySiteID";
                ddlsitename.DataBind();

                if (dtsite.Rows.Count > 0)
                {

                    ddlsitename.SelectedIndex = ddlsitename.Items.IndexOf(ddlsitename.Items.FindByValue(dteoqchk.Rows[0]["Sitemasterid"].ToString()));
                }
                lblsitevolumecapacity.Text = dteoqchk.Rows[0]["sitevolumecapacity"].ToString();
                //  lblsitevolumecapacityfordisplay
                if (Convert.ToDecimal(dteoqchk.Rows[0]["sitevolumecapacity"].ToString()) == 0)
                {
                    lblsitevolumecapacityfordisplay.Text = Convert.ToDecimal(dteoqchk.Rows[0]["sitevolumecapacity"].ToString()).ToString();
                }
                else
                {
                    lblsitevolumecapacityfordisplay.Text = Math.Round(Convert.ToDouble(Convert.ToDecimal(dteoqchk.Rows[0]["sitevolumecapacity"].ToString()).ToString()), 2).ToString("###,###.##");
                }


                lblproductusageofsitevolumepercent.Text = dteoqchk.Rows[0]["productusageofsitevolumepercent"].ToString();
                lblsitestoragecostperyear.Text = dteoqchk.Rows[0]["sitestoragecostperyear"].ToString();
                lblcarringcostproductperyear.Text = dteoqchk.Rows[0]["carringcostproductperyear"].ToString();
                lblcarringcostperunit.Text = dteoqchk.Rows[0]["carringcostperunit"].ToString();
                lblorderingcost.Text = dteoqchk.Rows[0]["orderingcost"].ToString();
                lbleoq.Text = dteoqchk.Rows[0]["Eoq"].ToString();

            }
            else
            {




                // yearly avg stock
                DateTime d1 = Convert.ToDateTime(ViewState["CurrentYearStartdate"].ToString());
                DateTime d2 = Convert.ToDateTime(ViewState["CurrentYearEnddate"].ToString());
                DateTime d3 = Convert.ToDateTime(ViewState["CurrentYearStartdate"].ToString());
                DateTime d4 = Convert.ToDateTime(ViewState["CurrentYearStartdate"].ToString());

                int day = d2.Subtract(d1).Days;
                int i = 0;
                double total = 0;
                for (i = 0; i < day - 1; i++)
                {
                    DateTime d5 = d4.AddDays(i);
                    double avgrate = avgcostcalculation(lblinvwarehouseid.Text, d3, d5);
                    total += avgrate;

                }
                int yearlyavgstock;

                if (total != 0 && day != 0)
                {
                    yearlyavgstock = Convert.ToInt32(total) / day;
                }
                else
                {
                    yearlyavgstock = 0;
                }

                lblyearlyavgstock.Text = yearlyavgstock.ToString();
                //End  yearly avg stock





                // avg stock volume

                double avgstockvolume = 0;
                if (lblproductvoulme.Text != "")
                {
                    avgstockvolume = Math.Round(yearlyavgstock * Convert.ToDouble(lblproductvoulme.Text), 2);
                }
                else
                {
                    avgstockvolume = 0;

                }
                lblavgstockvolume.Text = avgstockvolume.ToString();
                // lblavgstockvolumefordisplay.Text = avgstockvolume.ToString();

                if (avgstockvolume == 0)
                {
                    lblavgstockvolumefordisplay.Text = avgstockvolume.ToString();
                }
                else
                {
                    lblavgstockvolumefordisplay.Text = Math.Round(Convert.ToDouble(avgstockvolume.ToString()), 2).ToString("###,###.##");
                }


                // end avg stock volume


                // site storage cost
                string strsite = "select * from InventorySiteMasterTbl where WarehouseID='" + ddlWarehouse.SelectedValue + "' order by InventorySiteName";
                SqlCommand cmdsite = new SqlCommand(strsite, con);
                SqlDataAdapter adpsite = new SqlDataAdapter(cmdsite);
                DataTable dtsite = new DataTable();
                adpsite.Fill(dtsite);

                ddlsitename.DataSource = dtsite;
                ddlsitename.DataTextField = "InventorySiteName";
                ddlsitename.DataValueField = "InventorySiteID";
                ddlsitename.DataBind();

                if (dtsite.Rows.Count > 0)
                {
                    lblsitevolumecapacity.Text = Convert.ToString(dtsite.Rows[0]["TotalUsableWarehousecapacityinvolume"].ToString());

                    if (Convert.ToDecimal(dtsite.Rows[0]["TotalUsableWarehousecapacityinvolume"].ToString()) == 0)
                    {
                        lblsitevolumecapacityfordisplay.Text = Convert.ToDecimal(dtsite.Rows[0]["TotalUsableWarehousecapacityinvolume"].ToString()).ToString();
                    }
                    else
                    {
                        lblsitevolumecapacityfordisplay.Text = Math.Round(Convert.ToDouble(Convert.ToDecimal(dtsite.Rows[0]["TotalUsableWarehousecapacityinvolume"].ToString()).ToString()), 2).ToString("###,###.##");
                    }

                    if (dtsite.Rows[0]["Totalwarehousecost"].ToString() != null && dtsite.Rows[0]["TotalUsableWarehousecapacityinvolume"].ToString() != null)
                    {

                        double Totalwarehousecost = Convert.ToDouble(dtsite.Rows[0]["Totalwarehousecost"].ToString());
                        double TotalUsableWarehousecapacityinvolume = Convert.ToDouble(dtsite.Rows[0]["TotalUsableWarehousecapacityinvolume"].ToString());
                        if (TotalUsableWarehousecapacityinvolume != 0)
                        {
                            lblsitestoragecostperyear.Text = (Totalwarehousecost / TotalUsableWarehousecapacityinvolume).ToString();
                        }
                        else
                        {
                            lblsitestoragecostperyear.Text = "0";
                        }
                    }
                    else
                    {
                        lblsitestoragecostperyear.Text = "0";

                    }
                }
                // end site storage cost





                //Product Usage of Site Volume %
                double productusagesitevoumepercent = 0;
                if (lblsitevolumecapacity.Text != "")
                {
                    if (avgstockvolume != 0 && Convert.ToDouble(lblsitevolumecapacity.Text) != 0)
                    {
                        productusagesitevoumepercent = Math.Round((avgstockvolume / Convert.ToDouble(lblsitevolumecapacity.Text)) * 100, 2);
                    }
                    else
                    {
                        productusagesitevoumepercent = 0;
                    }
                }
                else
                {
                    productusagesitevoumepercent = 0;
                }

                lblproductusageofsitevolumepercent.Text = productusagesitevoumepercent.ToString();
                // end Product Usage of Site Volume %




                //Carring Cost of Product per year

                lblcarringcostproductperyear.Text = (Convert.ToDouble(lblproductusageofsitevolumepercent.Text) * Convert.ToDouble(lblsitestoragecostperyear.Text)).ToString();
                //End Carring Cost of Product per year




                //Carring Cost Per Unit


                if (Convert.ToDouble(lblcarringcostproductperyear.Text) != 0 && Convert.ToDouble(lblyearlyavgstock.Text) != 0)
                {
                    lblcarringcostperunit.Text = ((Convert.ToDouble(lblcarringcostproductperyear.Text)) / (Convert.ToDouble(lblyearlyavgstock.Text))).ToString();
                }
                else
                {
                    lblcarringcostperunit.Text = "0";
                }

                //end Carring Cost Per Unit





                // Order cost calculation


                if (dtsite.Rows.Count > 0)
                {

                    if (lblprefferedvendorid.Text == "0" || lblprefferedvendorid.Text == null)
                    {
                        lblorderingcostlabel.Visible = true;
                        lblorderingcostlabel.Text = "No Prefered vendor set";

                        lblorderingcost.Text = "";
                    }
                    else
                    {
                        string strordercost = "select  TotalCostperProduct from EOQMaster where Whid='" + ddlWarehouse.SelectedValue + "' and VendorId='" + lblprefferedvendorid.Text + "' ";
                        SqlCommand cmdordercost = new SqlCommand(strordercost, con);
                        SqlDataAdapter adpordercost = new SqlDataAdapter(cmdordercost);
                        DataTable dtordercost = new DataTable();
                        adpordercost.Fill(dtordercost);

                        if (dtordercost.Rows.Count > 0)
                        {

                            lblorderingcost.Text = dtordercost.Rows[0]["TotalCostperProduct"].ToString();
                        }
                        else
                        {
                            lblorderingcostlabel.Visible = true;
                            lblorderingcostlabel.Text = "No Prefered vendor set";

                            lblorderingcost.Text = "";
                        }
                    }

                }
                // end Order cost calculation





                // Eoq calaculation


                if (lblorderingcost.Text == "" || lblorderingcost.Text == "0")
                {
                    lbleoq.Text = "";
                    lbleoqtext.Text = "EOQ not possible";
                }
                else
                {


                    string Avgcost = "select Replace(sum(cast(Qty as Decimal)),'-','') as Qty from InventoryWarehouseMasterAvgCostTbl   where InventoryWarehouseMasterAvgCostTbl.DateUpdated between '" + ViewState["CurrentYearStartdate"].ToString() + "' and '" + ViewState["CurrentYearEnddate"].ToString() + "' and InventoryWarehouseMasterAvgCostTbl.InvWMasterId='" + lblinvwarehouseid.Text.ToString() + "' and (Tranction_Master_Id Not In('00000','--')  ) and Tranction_Master_Id is Not Null and cast(Qty as Decimal) <0   order by Qty  ";
                    SqlCommand cmd1451 = new SqlCommand(Avgcost, con);
                    SqlDataAdapter adp1451 = new SqlDataAdapter(cmd1451);
                    DataTable ds1451 = new DataTable();
                    adp1451.Fill(ds1451);

                    if (ds1451.Rows.Count > 0)
                    {
                        if (ds1451.Rows[0]["Qty"].ToString() != "")
                        {

                            double eoqwithoutsqrt = Math.Round(Math.Sqrt(Math.Round((2 * Convert.ToDouble(ds1451.Rows[0]["Qty"].ToString()) * Convert.ToDouble(lblorderingcost.Text)) / (Convert.ToDouble(lblcarringcostperunit.Text)), 2)), 2);
                            lbleoq.Text = eoqwithoutsqrt.ToString();
                            lbleoqtext.Text = "";

                        }
                        else
                        {
                            lbleoq.Text = "";
                            lbleoqtext.Text = "EOQ not possible";

                        }
                    }
                    else
                    {
                        lbleoq.Text = "";
                        lbleoqtext.Text = "EOQ not possible";
                    }
                    // End Total sales
                }

                // End Eoq calculation



            }
        }
    }
    protected void ddlsitename_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((DropDownList)sender).Parent.Parent as GridViewRow;
        int rinrow = row.RowIndex;


        DropDownList ddlsitename = (DropDownList)(GridView1.Rows[rinrow].FindControl("ddlsitename"));

        Label lblsitevolumecapacity = (Label)(GridView1.Rows[rinrow].FindControl("lblsitevolumecapacity"));
        Label lblsitevolumecapacityfordisplay = (Label)(GridView1.Rows[rinrow].FindControl("lblsitevolumecapacityfordisplay"));
        

        Label lblsitestoragecostperyear = (Label)(GridView1.Rows[rinrow].FindControl("lblsitestoragecostperyear"));



        string strsite = "select * from InventorySiteMasterTbl where WarehouseID='" + ddlWarehouse.SelectedValue + "' and InventorySiteID='" + ddlsitename.SelectedValue + "'";
        SqlCommand cmdsite = new SqlCommand(strsite, con);
        SqlDataAdapter adpsite = new SqlDataAdapter(cmdsite);
        DataTable dtsite = new DataTable();
        adpsite.Fill(dtsite);

        if (dtsite.Rows.Count > 0)
        {
            lblsitevolumecapacity.Text = Convert.ToString(dtsite.Rows[0]["TotalUsableWarehousecapacityinvolume"].ToString());

            if (Convert.ToDecimal(dtsite.Rows[0]["TotalUsableWarehousecapacityinvolume"].ToString()) == 0)
            {
                lblsitevolumecapacityfordisplay.Text = Convert.ToDecimal(dtsite.Rows[0]["TotalUsableWarehousecapacityinvolume"].ToString()).ToString();
            }
            else
            {
                lblsitevolumecapacityfordisplay.Text = Math.Round(Convert.ToDouble(Convert.ToDecimal(dtsite.Rows[0]["TotalUsableWarehousecapacityinvolume"].ToString()).ToString()), 2).ToString("###,###.##");
            }


            if (dtsite.Rows[0]["Totalwarehousecost"].ToString() != null && dtsite.Rows[0]["TotalUsableWarehousecapacityinvolume"].ToString() != null)
            {

                double Totalwarehousecost = Convert.ToDouble(dtsite.Rows[0]["Totalwarehousecost"].ToString());
                double TotalUsableWarehousecapacityinvolume = Convert.ToDouble(dtsite.Rows[0]["TotalUsableWarehousecapacityinvolume"].ToString());
                if (TotalUsableWarehousecapacityinvolume != 0)
                {
                    lblsitestoragecostperyear.Text = (Totalwarehousecost / TotalUsableWarehousecapacityinvolume).ToString();
                }
                else
                {
                    lblsitestoragecostperyear.Text = "0";
                }
            }
            else
            {
                lblsitestoragecostperyear.Text = "0";

            }
        }

        //Product Usage of Site Volume %

        Label lblproductusageofsitevolumepercent = (Label)(GridView1.Rows[rinrow].FindControl("lblproductusageofsitevolumepercent"));
        Label lblavgstockvolume = (Label)(GridView1.Rows[rinrow].FindControl("lblavgstockvolume"));

        double avgstockvolume = Convert.ToDouble(lblavgstockvolume.Text);

        double productusagesitevoumepercent = 0;
        if (lblsitevolumecapacity.Text != "")
        {
            if (avgstockvolume != 0 && Convert.ToDouble(lblsitevolumecapacity.Text) != 0)
            {
                productusagesitevoumepercent = Math.Round((avgstockvolume / Convert.ToDouble(lblsitevolumecapacity.Text)) * 100, 2);
            }
            else
            {
                productusagesitevoumepercent = 0;
            }
        }
        else
        {
            productusagesitevoumepercent = 0;
        }

        lblproductusageofsitevolumepercent.Text = productusagesitevoumepercent.ToString();


        // end Product Usage of Site Volume %

        //Carring Cost of Product per year


        Label lblcarringcostproductperyear = (Label)(GridView1.Rows[rinrow].FindControl("lblcarringcostproductperyear"));


        lblcarringcostproductperyear.Text = (Convert.ToDouble(lblproductusageofsitevolumepercent.Text) * Convert.ToDouble(lblsitestoragecostperyear.Text)).ToString();


        //End Carring Cost of Product per year


        //Carring Cost Per Unit


        Label lblcarringcostperunit = (Label)(GridView1.Rows[rinrow].FindControl("lblcarringcostperunit"));

        Label lblyearlyavgstock = (Label)(GridView1.Rows[rinrow].FindControl("lblyearlyavgstock"));

        if (Convert.ToDouble(lblcarringcostproductperyear.Text) != 0 && Convert.ToDouble(lblyearlyavgstock.Text) != 0)
        {
            lblcarringcostperunit.Text = ((Convert.ToDouble(lblcarringcostproductperyear.Text)) / (Convert.ToDouble(lblyearlyavgstock.Text))).ToString();
        }
        else
        {
            lblcarringcostperunit.Text = "0";
        }

        //end Carring Cost Per Unit

        // Order cost calculation

        Label lblprefferedvendorid = (Label)(GridView1.Rows[rinrow].FindControl("lblprefferedvendorid"));
        Label lblorderingcostlabel = (Label)(GridView1.Rows[rinrow].FindControl("lblorderingcostlabel"));
        Label lblorderingcost = (Label)(GridView1.Rows[rinrow].FindControl("lblorderingcost"));

        if (dtsite.Rows.Count > 0)
        {








            if (lblprefferedvendorid.Text == "0" || lblprefferedvendorid.Text == null)
            {
                lblorderingcostlabel.Visible = true;
                lblorderingcostlabel.Text = "No Prefered vendor set";

                lblorderingcost.Text = "";
            }
            else
            {
                string strordercost = "select  TotalCostperProduct from EOQMaster where Whid='" + ddlWarehouse.SelectedValue + "' and VendorId='" + lblprefferedvendorid.Text + "' ";
                SqlCommand cmdordercost = new SqlCommand(strordercost, con);
                SqlDataAdapter adpordercost = new SqlDataAdapter(cmdordercost);
                DataTable dtordercost = new DataTable();
                adpordercost.Fill(dtordercost);

                if (dtordercost.Rows.Count > 0)
                {

                    lblorderingcost.Text = dtordercost.Rows[0]["TotalCostperProduct"].ToString();
                }
                else
                {
                    lblorderingcost.Text = "";
                    lblorderingcostlabel.Visible = true;
                    lblorderingcostlabel.Text = "No Prefered vendor set";
                }
            }

        }

        // end Order cost calculation

        // Eoq calaculation

        //Label lbleoq = (Label)e.Row.FindControl("lbleoq");

        Label lbleoq = (Label)(GridView1.Rows[rinrow].FindControl("lbleoq"));
        Label lblinvwarehouseid = (Label)(GridView1.Rows[rinrow].FindControl("lblinvwarehouseid"));
        Label lbleoqtext = (Label)(GridView1.Rows[rinrow].FindControl("lbleoqtext"));
        if (lblorderingcost.Text == "" || lblorderingcost.Text == "0")
        {
            lbleoq.Text = "";
            lbleoqtext.Text = "EOQ not possible";
        }
        else
        {


            string Avgcost = "select Replace(sum(cast(Qty as Decimal)),'-','') as Qty from InventoryWarehouseMasterAvgCostTbl   where InventoryWarehouseMasterAvgCostTbl.DateUpdated between '" + ViewState["CurrentYearStartdate"].ToString() + "' and '" + ViewState["CurrentYearEnddate"].ToString() + "' and InventoryWarehouseMasterAvgCostTbl.InvWMasterId='" + lblinvwarehouseid.Text.ToString() + "' and (Tranction_Master_Id Not In('00000','--')  ) and Tranction_Master_Id is Not Null and cast(Qty as Decimal) <0   order by Qty  ";
            SqlCommand cmd1451 = new SqlCommand(Avgcost, con);
            SqlDataAdapter adp1451 = new SqlDataAdapter(cmd1451);
            DataTable ds1451 = new DataTable();
            adp1451.Fill(ds1451);

            if (ds1451.Rows.Count > 0)
            {
                if (ds1451.Rows[0]["Qty"].ToString() != "")
                {

                    double eoqwithoutsqrt = Math.Round(Math.Sqrt(Math.Round((2 * Convert.ToDouble(ds1451.Rows[0]["Qty"].ToString()) * Convert.ToDouble(lblorderingcost.Text)) / (Convert.ToDouble(lblcarringcostperunit.Text)), 2)), 2);
                    lbleoq.Text = eoqwithoutsqrt.ToString();
                    lbleoqtext.Text = "";

                }
                else
                {
                    lbleoq.Text = "";
                    lbleoqtext.Text = "EOQ not possible";

                }
            }
            else
            {
                lbleoq.Text = "";
                lbleoqtext.Text = "EOQ not possible";
            }


            // End Total sales
        }

        // End Eoq calculation






    }
    protected double avgcostcalculation(string inwhid, DateTime fromdate, DateTime todate)
    {

        //For Average Cost calculation

        double TotalAvgBal = 0;
        double AvgQtyAvail = 0;
        double AvgCostFinal = 0;
        double finaltotalqty = 0;


        string Avgcost = "select * from InventoryWarehouseMasterAvgCostTbl   where InventoryWarehouseMasterAvgCostTbl.DateUpdated between '" + fromdate.ToString() + "' and '" + todate.ToShortDateString() + "' and InventoryWarehouseMasterAvgCostTbl.InvWMasterId='" + inwhid.ToString() + "' order by DateUpdated,IWMAvgCostId  ";
        SqlCommand cmd1451 = new SqlCommand(Avgcost, con);
        SqlDataAdapter adp1451 = new SqlDataAdapter(cmd1451);
        DataTable ds1451 = new DataTable();
        adp1451.Fill(ds1451);

        if (ds1451.Rows.Count > 0)
        {
            foreach (DataRow dtr in ds1451.Rows)
            {
                double avgqty = 0;
                double avgrate = 0;
                double TotalAvgBalsub = 0;
                double totalqtycount = 0;


                if (Convert.ToString(dtr["Qty"]) != "" && Convert.ToString(dtr["Rate"]) != "")
                {

                    avgqty = Convert.ToDouble(dtr["Qty"].ToString());

                    avgrate = Convert.ToDouble(dtr["Rate"].ToString());

                }
                if (Convert.ToString(dtr["Qty"]) != "")
                {
                    totalqtycount = Convert.ToDouble(dtr["Qty"].ToString());
                }

                AvgQtyAvail += avgqty;
                finaltotalqty += totalqtycount;

                if (finaltotalqty == 0)
                {

                    TotalAvgBal = 0;
                    AvgQtyAvail = 0;
                    AvgCostFinal = 0;
                    finaltotalqty = 0;
                    avgqty = 0;
                    avgrate = 0;
                    TotalAvgBalsub = 0;
                    totalqtycount = 0;

                }

                TotalAvgBalsub = avgqty * avgrate;
                TotalAvgBal += TotalAvgBalsub;
            }
            if (TotalAvgBal == 0 && AvgQtyAvail == 0)
            {
                AvgCostFinal = 0;

            }
            else
            {
                AvgCostFinal = Math.Round(TotalAvgBal / AvgQtyAvail, 2);

            }
        }

        return finaltotalqty;

        //End Average Cost calculation
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        datasave();
        datefill();
        fillgrid();
        savecheck();

    }

    protected void savecheck()
    {
        string str = "select * from EoqCalculation where Whid='" + ddlWarehouse.SelectedValue + "' and compid='" + Session["comid"].ToString() + "' and Accountyearid='" + ViewState["Id"].ToString() + "'";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            Button1.Visible = false;
            Label3.Visible = true;
            lbllasteogdate.Visible = true;
            lbllasteogdate.Text = dt.Rows[0]["LastEoqDate"].ToString();

            CheckBox1.Visible = true;


        }
        else
        {
            Label3.Visible = false;
            lbllasteogdate.Visible = false;
            Button1.Visible = true;
            CheckBox1.Visible = false;

        }

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        if (Button3.Text == "Printable Version")
        {


            Button3.Text = "Hide Printable Version";
            btnPrint.Visible = true;


        }
        else
        {


            Button3.Text = "Printable Version";
            btnPrint.Visible = false;


        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        recalculate();
        datasave();
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox1.Checked == true)
        {
            Button2.Visible = true;

        }
        else
        {
            Button2.Visible = false;

        }
    }

    protected void datasave()
    {
        foreach (GridViewRow gdr in GridView1.Rows)
        {


            Label lblinvwarehouseid = (Label)(gdr.FindControl("lblinvwarehouseid"));
            Label lblprefferedvendorid = (Label)(gdr.FindControl("lblprefferedvendorid"));
            Label lblyearlyavgstock = (Label)(gdr.FindControl("lblyearlyavgstock"));
            Label lblavgstockvolume = (Label)(gdr.FindControl("lblavgstockvolume"));
            Label lblavgstockvolumefordisplay = (Label)(gdr.FindControl("lblavgstockvolumefordisplay"));


            DropDownList ddlsitename = (DropDownList)(gdr.FindControl("ddlsitename"));
            Label lblsitevolumecapacity = (Label)(gdr.FindControl("lblsitevolumecapacity"));
            Label lblsitevolumecapacityfordisplay = (Label)(gdr.FindControl("lblsitevolumecapacityfordisplay"));


            Label lblproductusageofsitevolumepercent = (Label)(gdr.FindControl("lblproductusageofsitevolumepercent"));
            Label lblsitestoragecostperyear = (Label)(gdr.FindControl("lblsitestoragecostperyear"));
            Label lblcarringcostproductperyear = (Label)(gdr.FindControl("lblcarringcostproductperyear"));
            Label lblcarringcostperunit = (Label)(gdr.FindControl("lblcarringcostperunit"));
            Label lblorderingcost = (Label)(gdr.FindControl("lblorderingcost"));
            Label lbleoq = (Label)(gdr.FindControl("lbleoq"));

            if (lbleoq.Text != "" && lblorderingcost.Text != "" && lbleoq.Text != "Infinity")
            {
                string str = "select * from EoqCalculation where Whid='" + ddlWarehouse.SelectedValue + "' and compid='" + Session["comid"].ToString() + "' and Accountyearid='" + ViewState["Id"].ToString() + "' and invwhid='" + lblinvwarehouseid.Text + "'";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    string str1 = "Update EoqCalculation set yearlyavgstock='" + lblyearlyavgstock.Text + "',avgstockvolume='" + lblavgstockvolume.Text + "',Sitemasterid='" + ddlsitename.SelectedValue + "',sitevolumecapacity='" + lblsitevolumecapacity.Text + "',productusageofsitevolumepercent='" + lblproductusageofsitevolumepercent.Text + "',sitestoragecostperyear='" + lblsitestoragecostperyear.Text + "',carringcostperunit='" + lblcarringcostperunit.Text + "',orderingcost='" + lblorderingcost.Text + "',Eoq='" + lbleoq.Text + "',preferedvendorid='" + lblprefferedvendorid.Text + "',Accountyearid='" + ViewState["Id"].ToString() + "',Whid='" + ddlWarehouse.SelectedValue + "',compid='" + Session["comid"].ToString() + "',carringcostproductperyear='" + lblcarringcostproductperyear.Text + "',LastEoqDate='" + DateTime.Now.ToString() + "' where invwhid='" + lblinvwarehouseid.Text + "' and Whid='" + ddlWarehouse.SelectedValue + "' and Accountyearid='" + ViewState["Id"].ToString() + "'";

                    SqlCommand cmd1 = new SqlCommand(str1, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd1.ExecuteNonQuery();
                    con.Close();


                }
                else
                {

                    string str1 = "Insert into EoqCalculation(invwhid,yearlyavgstock,avgstockvolume,Sitemasterid,sitevolumecapacity,productusageofsitevolumepercent,sitestoragecostperyear,carringcostperunit,orderingcost,Eoq,preferedvendorid,Accountyearid,Whid,compid,carringcostproductperyear,LastEoqDate) " +
                    " values ('" + lblinvwarehouseid.Text + "','" + lblyearlyavgstock.Text + "','" + lblavgstockvolume.Text + "','" + ddlsitename.SelectedValue + "','" + lblsitevolumecapacity.Text + "','" + lblproductusageofsitevolumepercent.Text + "','" + lblsitestoragecostperyear.Text + "','" + lblcarringcostperunit.Text + "','" + lblorderingcost.Text + "','" + lbleoq.Text + "','" + lblprefferedvendorid.Text + "','" + ViewState["Id"].ToString() + "','" + ddlWarehouse.SelectedValue + "','" + Session["comid"].ToString() + "','" + lblcarringcostproductperyear.Text + "','" + DateTime.Now.ToString() + "')";
                    SqlCommand cmd1 = new SqlCommand(str1, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd1.ExecuteNonQuery();
                    con.Close();

                }

            }


        }
        lblMsg.Text = "Record updated successfully.";

    }

    protected void recalculate()
    {
        foreach (GridViewRow gdr in GridView1.Rows)
        {
            DropDownList ddlsitename = (DropDownList)(gdr.FindControl("ddlsitename"));
            Label lblsitevolumecapacity = (Label)(gdr.FindControl("lblsitevolumecapacity"));
            Label lblsitevolumecapacityfordisplay = (Label)(gdr.FindControl("lblsitevolumecapacityfordisplay"));
            

            Label lblsitestoragecostperyear = (Label)(gdr.FindControl("lblsitestoragecostperyear"));
            Label lblinvwarehouseid = (Label)(gdr.FindControl("lblinvwarehouseid"));
            Label lblyearlyavgstock = (Label)(gdr.FindControl("lblyearlyavgstock"));

            Label lblproductvoulme = (Label)(gdr.FindControl("lblproductvoulme"));
            Label lblavgstockvolume = (Label)(gdr.FindControl("lblavgstockvolume"));
            Label lblavgstockvolumefordisplay = (Label)(gdr.FindControl("lblavgstockvolumefordisplay"));


            Label lblproductusageofsitevolumepercent = (Label)(gdr.FindControl("lblproductusageofsitevolumepercent"));
            Label lblcarringcostproductperyear = (Label)(gdr.FindControl("lblcarringcostproductperyear"));
            Label lblcarringcostperunit = (Label)(gdr.FindControl("lblcarringcostperunit"));
            Label lblprefferedvendorid = (Label)(gdr.FindControl("lblprefferedvendorid"));
            Label lblorderingcostlabel = (Label)(gdr.FindControl("lblorderingcostlabel"));
            Label lblorderingcost = (Label)(gdr.FindControl("lblorderingcost"));

            Label lbleoq = (Label)(gdr.FindControl("lbleoq"));
            Label lbleoqtext = (Label)(gdr.FindControl("lbleoqtext"));



            // yearly avg stock
            DateTime d1 = Convert.ToDateTime(ViewState["CurrentYearStartdate"].ToString());
            DateTime d2 = Convert.ToDateTime(ViewState["CurrentYearEnddate"].ToString());
            DateTime d3 = Convert.ToDateTime(ViewState["CurrentYearStartdate"].ToString());
            DateTime d4 = Convert.ToDateTime(ViewState["CurrentYearStartdate"].ToString());

            int day = d2.Subtract(d1).Days;
            int i = 0;
            double total = 0;
            for (i = 0; i < day - 1; i++)
            {
                DateTime d5 = d4.AddDays(i);
                double avgrate = avgcostcalculation(lblinvwarehouseid.Text, d3, d5);
                total += avgrate;

            }
            int yearlyavgstock;

            if (total != 0 && day != 0)
            {
                yearlyavgstock = Convert.ToInt32(total) / day;
            }
            else
            {
                yearlyavgstock = 0;
            }

            lblyearlyavgstock.Text = yearlyavgstock.ToString();
            //End  yearly avg stock





            // avg stock volume

            double avgstockvolume = 0;
            if (lblproductvoulme.Text != "")
            {
                avgstockvolume = Math.Round(yearlyavgstock * Convert.ToDouble(lblproductvoulme.Text), 2);
            }
            else
            {
                avgstockvolume = 0;

            }
            lblavgstockvolume.Text = avgstockvolume.ToString();

            if (avgstockvolume == 0)
            {
                lblavgstockvolumefordisplay.Text = avgstockvolume.ToString();
            }
            else
            {
                lblavgstockvolumefordisplay.Text = Math.Round(Convert.ToDouble(avgstockvolume.ToString()), 2).ToString("###,###.##");
            }



            // end avg stock volume


            // site storage cost


            string strsite = "select * from InventorySiteMasterTbl where WarehouseID='" + ddlWarehouse.SelectedValue + "'  ";
            SqlCommand cmdsite = new SqlCommand(strsite, con);
            SqlDataAdapter adpsite = new SqlDataAdapter(cmdsite);
            DataTable dtsite = new DataTable();
            adpsite.Fill(dtsite);

            ddlsitename.DataSource = dtsite;
            ddlsitename.DataTextField = "InventorySiteName";
            ddlsitename.DataValueField = "InventorySiteID";
            ddlsitename.DataBind();

            if (dtsite.Rows.Count > 0)
            {
                string str = "select * from EoqCalculation where Whid='" + ddlWarehouse.SelectedValue + "' and compid='" + Session["comid"].ToString() + "' and Accountyearid='" + ViewState["Id"].ToString() + "' and invwhid='" + lblinvwarehouseid.Text + "'";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    ddlsitename.SelectedIndex = ddlsitename.Items.IndexOf(ddlsitename.Items.FindByValue(dt.Rows[0]["Sitemasterid"].ToString()));
                }


                lblsitevolumecapacity.Text = Convert.ToString(dtsite.Rows[0]["TotalUsableWarehousecapacityinvolume"].ToString());


                if (Convert.ToDecimal(dtsite.Rows[0]["TotalUsableWarehousecapacityinvolume"].ToString()) == 0)
                {
                    lblsitevolumecapacityfordisplay.Text = Convert.ToDecimal(dtsite.Rows[0]["TotalUsableWarehousecapacityinvolume"].ToString()).ToString();
                }
                else
                {
                    lblsitevolumecapacityfordisplay.Text = Math.Round(Convert.ToDouble(Convert.ToDecimal(dtsite.Rows[0]["TotalUsableWarehousecapacityinvolume"].ToString()).ToString()), 2).ToString("###,###.##");
                }


                if (dtsite.Rows[0]["Totalwarehousecost"].ToString() != null && dtsite.Rows[0]["TotalUsableWarehousecapacityinvolume"].ToString() != null)
                {

                    double Totalwarehousecost = Convert.ToDouble(dtsite.Rows[0]["Totalwarehousecost"].ToString());
                    double TotalUsableWarehousecapacityinvolume = Convert.ToDouble(dtsite.Rows[0]["TotalUsableWarehousecapacityinvolume"].ToString());
                    if (TotalUsableWarehousecapacityinvolume != 0)
                    {
                        lblsitestoragecostperyear.Text = (Totalwarehousecost / TotalUsableWarehousecapacityinvolume).ToString();
                    }
                    else
                    {
                        lblsitestoragecostperyear.Text = "0";
                    }
                }
                else
                {
                    lblsitestoragecostperyear.Text = "0";

                }
            }
            // end site storage cost





            //Product Usage of Site Volume %
            double productusagesitevoumepercent = 0;
            if (lblsitevolumecapacity.Text != "")
            {
                if (avgstockvolume != 0 && Convert.ToDouble(lblsitevolumecapacity.Text) != 0)
                {
                    productusagesitevoumepercent = Math.Round((avgstockvolume / Convert.ToDouble(lblsitevolumecapacity.Text)) * 100, 2);
                }
                else
                {
                    productusagesitevoumepercent = 0;
                }
            }
            else
            {
                productusagesitevoumepercent = 0;
            }

            lblproductusageofsitevolumepercent.Text = productusagesitevoumepercent.ToString();
            // end Product Usage of Site Volume %




            //Carring Cost of Product per year

            lblcarringcostproductperyear.Text = (Convert.ToDouble(lblproductusageofsitevolumepercent.Text) * Convert.ToDouble(lblsitestoragecostperyear.Text)).ToString();
            //End Carring Cost of Product per year




            //Carring Cost Per Unit


            if (Convert.ToDouble(lblcarringcostproductperyear.Text) != 0 && Convert.ToDouble(lblyearlyavgstock.Text) != 0)
            {
                lblcarringcostperunit.Text = ((Convert.ToDouble(lblcarringcostproductperyear.Text)) / (Convert.ToDouble(lblyearlyavgstock.Text))).ToString();
            }
            else
            {
                lblcarringcostperunit.Text = "0";
            }

            //end Carring Cost Per Unit





            // Order cost calculation


            if (dtsite.Rows.Count > 0)
            {

                if (lblprefferedvendorid.Text == "0" || lblprefferedvendorid.Text == null)
                {
                    lblorderingcostlabel.Visible = true;
                    lblorderingcostlabel.Text = "No Prefered vendor set";

                    lblorderingcost.Text = "";
                }
                else
                {
                    string strordercost = "select  TotalCostperProduct from EOQMaster where Whid='" + ddlWarehouse.SelectedValue + "' and VendorId='" + lblprefferedvendorid.Text + "' ";
                    SqlCommand cmdordercost = new SqlCommand(strordercost, con);
                    SqlDataAdapter adpordercost = new SqlDataAdapter(cmdordercost);
                    DataTable dtordercost = new DataTable();
                    adpordercost.Fill(dtordercost);

                    if (dtordercost.Rows.Count > 0)
                    {

                        lblorderingcost.Text = dtordercost.Rows[0]["TotalCostperProduct"].ToString();
                    }
                    else
                    {
                        lblorderingcostlabel.Visible = true;
                        lblorderingcostlabel.Text = "No Prefered vendor set";

                        lblorderingcost.Text = "";
                    }
                }

            }
            // end Order cost calculation





            // Eoq calaculation


            if (lblorderingcost.Text == "" || lblorderingcost.Text == "0")
            {
                lbleoq.Text = "";
                lbleoqtext.Text = "EOQ not possible";
            }
            else
            {


                string Avgcost = "select Replace(sum(cast(Qty as Decimal)),'-','') as Qty from InventoryWarehouseMasterAvgCostTbl   where InventoryWarehouseMasterAvgCostTbl.DateUpdated between '" + ViewState["CurrentYearStartdate"].ToString() + "' and '" + ViewState["CurrentYearEnddate"].ToString() + "' and InventoryWarehouseMasterAvgCostTbl.InvWMasterId='" + lblinvwarehouseid.Text.ToString() + "' and (Tranction_Master_Id Not In('00000','--')  ) and Tranction_Master_Id is Not Null and cast(Qty as Decimal) <0   order by Qty  ";
                SqlCommand cmd1451 = new SqlCommand(Avgcost, con);
                SqlDataAdapter adp1451 = new SqlDataAdapter(cmd1451);
                DataTable ds1451 = new DataTable();
                adp1451.Fill(ds1451);

                if (ds1451.Rows.Count > 0)
                {
                    if (ds1451.Rows[0]["Qty"].ToString() != "")
                    {

                        double eoqwithoutsqrt = Math.Round(Math.Sqrt(Math.Round((2 * Convert.ToDouble(ds1451.Rows[0]["Qty"].ToString()) * Convert.ToDouble(lblorderingcost.Text)) / (Convert.ToDouble(lblcarringcostperunit.Text)), 2)), 2);
                        lbleoq.Text = eoqwithoutsqrt.ToString();
                        lbleoqtext.Text = "";

                    }
                    else
                    {
                        lbleoq.Text = "";
                        lbleoqtext.Text = "EOQ not possible";

                    }
                }
                else
                {
                    lbleoq.Text = "";
                    lbleoqtext.Text = "EOQ not possible";
                }
                // End Total sales
            }

            // End Eoq calculation


        }
    }

}
