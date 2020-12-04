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

public partial class ShoppingCart_SalesRateDeterminatation : System.Web.UI.Page
{
    //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString"].ConnectionString);
    SqlConnection con;
    DataSet ds = new DataSet();
    double total = 0, Rate = 0, Mar = 0, FlatRate = 0, VolumeFactor = 0, Weight = 0, overhead = 0;
    string compid;
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
        lblMsg.Visible = false;
        if (!IsPostBack)
        {

            lblCompany.Text = Session["Cname"].ToString();
            pnlCal.Visible = true;



            //string strwh = "SELECT WareHouseId,Name,Address,CurrencyId FROM WareHouseMaster where comid='" + compid + "' and [WareHouseMaster].status = '1'";
            //SqlCommand cmdwh = new SqlCommand(strwh, con);
            //SqlDataAdapter adpwh = new SqlDataAdapter(cmdwh);
            //DataTable dtwh = new DataTable();
            //adpwh.Fill(dtwh);

            //ddlWarehouse.DataSource = dtwh;
            //ddlWarehouse.DataTextField = "Name";
            //ddlWarehouse.DataValueField = "WareHouseId";
            //ddlWarehouse.DataBind();

            //ddlWarehouse.Items.Insert(0, "Select");
            //ddlWarehouse.Items[0].Value = "0";
            fillstore();
            ddlWarehouse_SelectedIndexChanged(sender, e);
            ddlItemName_SelectedIndexChanged(sender, e);
            fillUnittypes();
            FillVolumeunitsDDL();
            fillstorefilter();
            Fillgrid();
            lbldate.Text = System.DateTime.Now.ToShortDateString();


        }
    }
    public DataSet fiillcategory()
    {
        

        string strcat = " SELECT Distinct InventoryCategoryMaster.InventeroyCatId,InventoruSubSubCategory.InventorySubSubId,InventoryCategoryMaster.InventoryCatName+'-'+ InventorySubCategoryMaster.InventorySubCatName+'-'+InventoruSubSubCategory.InventorySubSubName as expr FROM InventoryCategoryMaster inner join InventorySubCategoryMaster on InventorySubCategoryMaster.InventoryCategoryMasterId=InventoryCategoryMaster.InventeroyCatId inner join InventoruSubSubCategory on InventoruSubSubCategory.InventorySubCatID=InventorySubCategoryMaster.InventorySubCatId inner join InventoryMaster on InventoryMaster.InventorySubSubId=InventoruSubSubCategory.InventorySubSubId inner join InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.InventoryMasterId=InventoryMaster.InventoryMasterId inner join WareHouseMaster on WareHouseMaster.WareHouseId=InventoryWarehouseMasterTbl.WareHouseId  WHERE InventoryWarehouseMasterTbl.WareHouseId ='" + ddlWarehouse.SelectedValue + "'and [InventoryCategoryMaster].[Activestatus]='1' ";

        SqlCommand cmd = new SqlCommand(strcat, con);

    

        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;

    }
    protected void fillUnittypes()
    {
        string strwh1 = "SELECT  [UnitTypeId] ,[Name],[Status]  FROM [UnitTypeMaster] where Status=1";
        SqlCommand cmdwh1 = new SqlCommand(strwh1, con);
        SqlDataAdapter adpwh1 = new SqlDataAdapter(cmdwh1);
        DataTable dtwh1 = new DataTable();
        adpwh1.Fill(dtwh1);
        if (dtwh1.Rows.Count > 0)
        {
            DropDownList1.DataSource = dtwh1;
            DropDownList1.DataTextField = "Name";
            DropDownList1.DataValueField = "UnitTypeId";
            DropDownList1.DataBind();

            DropDownList3.DataSource = dtwh1;
            DropDownList3.DataTextField = "Name";
            DropDownList3.DataValueField = "UnitTypeId";
            DropDownList3.DataBind();
        }

    }
    protected void FillVolumeunitsDDL()
    {
        string strwh11 = "SELECT   [VolumeUnitId]      ,[VolumeUnitName]  FROM [VolumeUnitMaster]";
        SqlCommand cmdwh11 = new SqlCommand(strwh11, con);
        SqlDataAdapter adpwh11 = new SqlDataAdapter(cmdwh11);
        DataTable dtwh11 = new DataTable();
        adpwh11.Fill(dtwh11);
        if (dtwh11.Rows.Count > 0)
        {
            DropDownList2.DataSource = dtwh11;
            DropDownList2.DataTextField = "VolumeUnitName";
            DropDownList2.DataValueField = "VolumeUnitId";
            DropDownList2.DataBind();


        }

    }
    protected void btCal_Click(object sender, EventArgs e)
    {
        ////////if (lblRate.Text == "")
        ////////{
        ////////    lblMsg.Visible = true;
        ////////    lblMsg.Text = " Select the Rate then Caculate";

        ////////}
        ////////else
        ////////{
        ////////    DataTable ds1 = new DataTable();
        ////////    ds1 = (DataTable)ViewState["ds"];
        ////////    txtItemNo.Text = ds1.Rows[0]["ProductNo"].ToString();
        ////////    txtname.Text = ds1.Rows[0]["Name"].ToString();
        ////////    txtPrice.Text = lblRate.Text;

        ////////    Mar = (Convert.ToDecimal(txtmar.Text) * 100) / Convert.ToDecimal(lblRate.Text);

        ////////    txtmargin.Text = Mar.ToString();

        ////////    FlatRate = Convert.ToDecimal(txtFlat.Text);
        ////////    // int j = FlatRate.ToString().IndexOf('.') + 3;
        ////////    txtFlatAmt.Text = FlatRate.ToString();

        ////////    Weight = Convert.ToDecimal(txtItemWeight.Text) * Convert.ToDecimal(ds1.Rows[0]["Weight"].ToString());
        ////////    //int k = Weight.ToString().IndexOf('.') + 3;
        ////////    txtWeight.Text = Weight.ToString();

        ////////    if (ds1.Rows[0]["Volume"].ToString()  != "" )
        ////////    {
        ////////        VolumeFactor = Convert.ToDecimal(txtItemVolume.Text) * Convert.ToDecimal(ds1.Rows[0]["Volume"].ToString());
        ////////    }
        ////////    // int l = VolumeFactor.ToString().IndexOf('.') + 3;
        ////////    txtVolume.Text = VolumeFactor.ToString();

        ////////    if (ds1.Rows[0]["Weight"].ToString() != "")
        ////////    {
        ////////        overhead = Convert.ToDecimal(txtOverhead1.Text) * Convert.ToDecimal(ds1.Rows[0]["Weight"].ToString());
        ////////    }
        ////////    //   int m = overhead.ToString().IndexOf('.') + 3;
        ////////    txtOverhead.Text = overhead.ToString();


        ////////    Rate = Convert.ToDecimal(lblRate.Text);

        ////////    total = Rate + Mar + FlatRate + Weight + VolumeFactor + overhead;
        ////////    //int n = total.ToString().IndexOf('.') + 3;
        ////////    txtTotalAmt.Text = total.ToString();
        ////////    pnlCal.Visible = false;
        ////////    pnlConfirm.Visible = true;
        ////////}
    }
    protected void txtmargin_TextChanged(object sender, EventArgs e)
    {

    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = "";

        if (ddlCategory.SelectedIndex > -1)
        {
            string strs = "SELECT  distinct   InventoryMaster.Name, InventoruSubSubCategory.InventorySubSubId, InventoryMaster.InventoryMasterId, InventoryMaster.ProductNo,  " +
                          " InventoryWarehouseMasterTbl.InventoryWarehouseMasterId, InventoryWarehouseMasterTbl.WareHouseId, WareHouseMaster.Name AS WarehouseName " +
                           " FROM         InventoryMaster INNER JOIN " +
                          " InventoruSubSubCategory ON InventoryMaster.InventorySubSubId = InventoruSubSubCategory.InventorySubSubId INNER JOIN " +
                          " InventoryWarehouseMasterTbl ON InventoryMaster.InventoryMasterId = InventoryWarehouseMasterTbl.InventoryMasterId INNER JOIN " +
                           " WareHouseMaster ON InventoryWarehouseMasterTbl.WareHouseId = WareHouseMaster.WareHouseId left outer join InventoryMasterMNC on  InventoryMasterMNC.Inventorymasterid=InventoryMaster.InventoryMasterId  " +
                           " WHERE InventoruSubSubCategory.InventorySubSubId ='" + ddlCategory.SelectedValue + "' and InventoryWarehouseMasterTbl.WareHouseId='" + ddlWarehouse.SelectedValue + "' and WareHouseMaster.Status='" + 1 + "' ";
            // (InventoruSubSubCategory.InventorySubSubId ='" + Convert.ToInt32(ddlCategory.SelectedValue) + "') " +
            //" and (InventoryWarehouseMasterTbl.WareHouseId='" + Convert.ToInt32(ddlWarehouse.SelectedValue) + "' and InventoryMasterMNC.copid='" + compid + "') ";


            SqlCommand cmd = new SqlCommand(strs, con);
            cmd.Parameters.AddWithValue("@InventorySubSubId", ddlCategory.SelectedValue);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);

            adp.Fill(ds);

            ddlItemName.DataSource = ds;
            ddlItemName.DataTextField = "Name";
            ddlItemName.DataValueField = "InventoryWarehouseMasterId";
            ddlItemName.DataBind();

            //ddlItemName.Items.Insert(0, "--Select--");
            //ddlItemName.Items[0].Value = "0";
        }
        RadioButtonList2_SelectedIndexChanged(sender, e);

    }

    protected void RadioButtonList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        //lblMsg.Text = "";

        DataTable ds1;


        string str = "SELECT     InventoryMaster.Name, InventoryMaster.ProductNo, MAX(PurchaseMaster.RatePerUnit) AS Highest, MIN(PurchaseMaster.RatePerUnit) AS Lowest,  " +
                 " InventorySizeMaster.Volume, InventoryDetails.Weight,InventoryDetails.UnitTypeId, InventoryWarehouseMasterTbl.InventoryWarehouseMasterId,InventoryWarehouseMasterTbl.WareHouseId, InventoryWarehouseMasterTbl.Rate " +
                 " FROM         InventoryDetails LEFT OUTER JOIN " +
                 " InventorySizeMaster RIGHT OUTER JOIN " +
                 " PurchaseDetails LEFT OUTER JOIN " +
                 " InventoryWarehouseMasterTbl LEFT OUTER JOIN " +
                 " PurchaseMaster ON InventoryWarehouseMasterTbl.InventoryWarehouseMasterId = PurchaseMaster.InventoryWHM_Id LEFT OUTER JOIN " +
                 " InventoryMaster ON InventoryWarehouseMasterTbl.InventoryMasterId = InventoryMaster.InventoryMasterId ON " +
                 " PurchaseDetails.Purchase_Details_Id = PurchaseMaster.Purchase_Details_Id ON InventorySizeMaster.InventoryMasterId = InventoryMaster.InventoryMasterId ON  " +
                 " InventoryDetails.Inventory_Details_Id = InventoryMaster.InventoryDetailsId left outer join InventoryMasterMNC on  InventoryMasterMNC.Inventorymasterid=InventoryMaster.InventoryMasterId  " +
                 " WHERE     (InventoryWarehouseMasterTbl.InventoryWarehouseMasterId= '" + ddlItemName.SelectedValue + "') and InventoryWarehouseMasterTbl.WareHouseId='" + Convert.ToInt32(ddlWarehouse.SelectedValue) + "' " +
                 " GROUP BY InventoryMaster.Name, InventoryMaster.ProductNo, InventorySizeMaster.Volume, InventoryDetails.Weight,InventoryDetails.UnitTypeId, " +
                 " InventoryWarehouseMasterTbl.InventoryWarehouseMasterId, InventoryWarehouseMasterTbl.Rate,InventoryWarehouseMasterTbl.WareHouseId ";

        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        ds1 = new DataTable();
        adp.Fill(ds1);
        ViewState["ds"] = ds1;

       
        if (RadioButtonList2.SelectedIndex == 0)
        {
            lblRate.Text = "";


            string str11 = "select * from InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + ddlItemName.SelectedValue + "' and  InventoryWarehouseMasterAvgCostTbl.Tranction_Master_Id Is Not Null and InventoryWarehouseMasterAvgCostTbl.Rate Is Not Null and InventoryWarehouseMasterAvgCostTbl.AdjustMasterId Is Null  and DateUpdated<='" + System.DateTime.Now + "'  order by IWMAvgCostId  ";
            SqlCommand cmd11 = new SqlCommand(str11, con);
            SqlDataAdapter adp11 = new SqlDataAdapter(cmd11);
            DataTable ds111 = new DataTable();
            adp11.Fill(ds111);
            if (ds111.Rows.Count > 0)
            {
                //lblRate.Text = ds111.Rows[0]["Rate"].ToString();


                double max = 0;
                foreach (DataRow dr in ds111.Rows)
                {
                    double j = Convert.ToDouble(dr["Rate"].ToString());


                    if (j > max)
                    {
                        max = j;
                    }


                }
                lblRate.Text = max.ToString();

            }

            
        }
        else if (RadioButtonList2.SelectedIndex == 1)
        {
            lblRate.Text = "";
            string str11 = "select * from InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + ddlItemName.SelectedValue + "' and  InventoryWarehouseMasterAvgCostTbl.Tranction_Master_Id Is Not Null and InventoryWarehouseMasterAvgCostTbl.Rate Is Not Null and InventoryWarehouseMasterAvgCostTbl.AdjustMasterId Is Null  and DateUpdated<='" + System.DateTime.Now + "'  order by IWMAvgCostId  ";
            SqlCommand cmd11 = new SqlCommand(str11, con);
            SqlDataAdapter adp11 = new SqlDataAdapter(cmd11);
            DataTable ds111 = new DataTable();
            adp11.Fill(ds111);
            if (ds111.Rows.Count > 0)
            {

                double j = Convert.ToDouble(ds111.Rows[0]["Rate"].ToString());

                foreach (DataRow dr in ds111.Rows)
                {
                    double min = Convert.ToDouble(dr["Rate"].ToString());


                    if (j < min)
                    {
                        min = j;
                    }
                    else
                    {
                        j = min;
                    }


                }
                lblRate.Text = j.ToString();
            }
            
        }
        else if (RadioButtonList2.SelectedIndex == 2)
        {
            lblRate.Text = "";
            string str11 = "select *  from InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + ddlItemName.SelectedValue + "' and  InventoryWarehouseMasterAvgCostTbl.Tranction_Master_Id Is Not Null and InventoryWarehouseMasterAvgCostTbl.Rate Is Not Null and InventoryWarehouseMasterAvgCostTbl.AdjustMasterId Is Null and DateUpdated<='" + System.DateTime.Now + "'  order by IWMAvgCostId Desc ";
            SqlCommand cmd11 = new SqlCommand(str11, con);
            SqlDataAdapter adp11 = new SqlDataAdapter(cmd11);
            DataTable ds111 = new DataTable();
            adp11.Fill(ds111);
            if (ds111.Rows.Count > 0)
            {
                lblRate.Text = ds111.Rows[0]["Rate"].ToString();
            }
            

        }
        else if (RadioButtonList2.SelectedIndex == 3)
        {
            lblRate.Text = "";
            double avgqty = 0;
            double avgrate = 0;
            double TotalAvgBalsub = 0;
            double TotalAvgBal = 0;
            double AvgQtyAvail = 0;
            
            double AvgCostFinal = 0;

            string str11 = "select *  from InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + ddlItemName.SelectedValue + "' and DateUpdated<='" + System.DateTime.Now + "' order by IWMAvgCostId ";
            SqlCommand cmd1451 = new SqlCommand(str11, con);
            SqlDataAdapter adp1451 = new SqlDataAdapter(cmd1451);
            DataTable ds1451 = new DataTable();
            adp1451.Fill(ds1451);


            if (ds1451.Rows.Count > 0)
            {
                foreach (DataRow dtr in ds1451.Rows)
                {
                    if (Convert.ToString(dtr["Qty"]) != "")
                    {
                        avgqty = Convert.ToDouble(dtr["Qty"].ToString());
                    }
                    if (Convert.ToString(dtr["Rate"]) != "")
                    {
                        if (avgqty < 0)
                        {

                            if (TotalAvgBal == 0 && AvgQtyAvail == 0)
                            {
                                avgrate = 0;

                            }
                            else
                            {
                                avgrate = TotalAvgBal / AvgQtyAvail;
                            }

                        }
                        else
                        {
                            avgrate = Convert.ToDouble(dtr["Rate"].ToString());
                        }

                    }


                    AvgQtyAvail += avgqty;

                    if (AvgQtyAvail == 0)
                    {
                        avgqty = 0;
                        avgrate = 0;
                        AvgQtyAvail = 0;
                        TotalAvgBalsub = 0;
                        TotalAvgBal = 0;

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
                    AvgCostFinal = TotalAvgBal / AvgQtyAvail;
                    AvgCostFinal = Math.Round(AvgCostFinal, 2);
                }
            }

            lblRate.Text = AvgCostFinal.ToString();
        }
        else
        {
            lblRate.Text = "Please Select Rate Option.....";
        }
        //}
        //else
        //{
        //    lblRate.Text = "";
        //}
        //}


        //string Avgcost = "select Max(InventoryWarehouseMasterAvgCostTbl.Rate) from InventoryWarehouseMasterAvgCostTbl inner join InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.InventoryWarehouseMasterId= InventoryWarehouseMasterAvgCostTbl.InvWMasterId  where  InventoryWarehouseMasterTbl.InventoryWarehouseMasterId='" + ddlItemName.SelectedValue + "' and InventoryWarehouseMasterAvgCostTbl.Tranction_Master_Id Is Not Null and InventoryWarehouseMasterAvgCostTbl.Rate Is Not Null and InventoryWarehouseMasterAvgCostTbl.AdjustMasterId Is Null and InventoryWarehouseMasterAvgCostTbl.Qty>0 ";
        //SqlCommand cmd1451 = new SqlCommand(Avgcost, con);
        //SqlDataAdapter adp1451 = new SqlDataAdapter(cmd1451);
        //DataTable ds1451 = new DataTable();
        //adp1451.Fill(ds1451);


    }
    protected void RadioButtonList1_SelectedIndexChanged1(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        //if (RadioButtonList1.SelectedIndex == 0)
        //{
        //    //////string str = " SELECT   InventoryDetails.Rate as Existing, InventoryMaster.Name, InventoryMaster.ProductNo, Max(PurchaseMaster.RatePerUnit) as Highest,Min(PurchaseMaster.RatePerUnit) as Lowest," +
        //    //////         " (Min(PurchaseMaster.RatePerUnit)+ InventoryDetails.Rate +Max(PurchaseMaster.RatePerUnit))/3 as Average " +
        //    //////         " FROM         PurchaseMaster INNER JOIN " +
        //    //////         " PurchaseDetails ON PurchaseMaster.Purchase_Details_Id = PurchaseDetails.Purchase_Details_Id INNER JOIN " +
        //    //////         " InventoryMaster ON PurchaseMaster.InventoryWHM_Id = InventoryMaster.InventoryMasterId INNER JOIN " +
        //    //////         " InventoryDetails ON InventoryMaster.InventoryDetailsId = InventoryDetails.Inventory_Details_Id " +
        //    //////         " Where InventoryMaster.InventoryMasterId = '" + ddlItemName.SelectedValue + "'" +
        //    //////         " Group by  InventoryDetails.Rate , InventoryMaster.Name, InventoryMaster.ProductNo ";

        //    string str = "SELECT     InventoryMaster.Name, InventoryMaster.ProductNo, MAX(PurchaseMaster.RatePerUnit) AS Highest, MIN(PurchaseMaster.RatePerUnit) AS Lowest,  " +
        //             " InventorySizeMaster.Volume, InventoryDetails.Weight, InventoryWarehouseMasterTbl.InventoryWarehouseMasterId,InventoryWarehouseMasterTbl.WareHouseId, InventoryWarehouseMasterTbl.Rate " +
        //             " FROM         InventoryDetails LEFT OUTER JOIN " +
        //             " InventorySizeMaster RIGHT OUTER JOIN " +
        //             " PurchaseDetails LEFT OUTER JOIN " +
        //             " InventoryWarehouseMasterTbl LEFT OUTER JOIN " +
        //             " PurchaseMaster ON InventoryWarehouseMasterTbl.InventoryWarehouseMasterId = PurchaseMaster.InventoryWHM_Id LEFT OUTER JOIN " +
        //             " InventoryMaster ON InventoryWarehouseMasterTbl.InventoryMasterId = InventoryMaster.InventoryMasterId ON " +
        //             " PurchaseDetails.Purchase_Details_Id = PurchaseMaster.Purchase_Details_Id ON InventorySizeMaster.InventoryMasterId = InventoryMaster.InventoryMasterId ON  " +
        //             " InventoryDetails.Inventory_Details_Id = InventoryMaster.InventoryDetailsId " +
        //             " WHERE     (InventoryWarehouseMasterTbl.InventoryWarehouseMasterId= '" + ddlItemName.SelectedValue + "') and (InventoryWarehouseMasterTbl.WareHouseId='" + Convert.ToInt32(ddlWarehouse.SelectedValue) + "' )" +
        //             " GROUP BY InventoryMaster.Name, InventoryMaster.ProductNo, InventorySizeMaster.Volume, InventoryDetails.Weight, " +
        //             " InventoryWarehouseMasterTbl.InventoryWarehouseMasterId, InventoryWarehouseMasterTbl.Rate, InventoryWarehouseMasterTbl.WareHouseId ";



        //    SqlCommand cmd = new SqlCommand(str, con);
        //    SqlDataAdapter adp = new SqlDataAdapter(cmd);

        //    adp.Fill(ds);

        //}
        //else if (RadioButtonList1.SelectedIndex == 1)
        //{
        //////string str = " SELECT   InventoryDetails.Rate as Existing, InventoryMaster.Name, InventoryMaster.ProductNo, Max(PurchaseMaster.RatePerUnit) as Highest,Min(PurchaseMaster.RatePerUnit) as Lowest," +
        //////         " (Min(PurchaseMaster.RatePerUnit)+ InventoryDetails.Rate +Max(PurchaseMaster.RatePerUnit))/3 as Average " +
        //////         " FROM         PurchaseMaster INNER JOIN " +
        //////         " PurchaseDetails ON PurchaseMaster.Purchase_Details_Id = PurchaseDetails.Purchase_Details_Id INNER JOIN " +
        //////         " InventoryMaster ON PurchaseMaster.InventoryWHM_Id = InventoryMaster.InventoryMasterId INNER JOIN " +
        //////         " InventoryDetails ON InventoryMaster.InventoryDetailsId = InventoryDetails.Inventory_Details_Id " +
        //////         " Where InventoryMaster.InventoryMasterId = '" + ddlItemName.SelectedValue + "'" +
        //////         " Group by  InventoryDetails.Rate , InventoryMaster.Name, InventoryMaster.ProductNo ";


        string str = "SELECT     InventoryMaster.Name, InventoryMaster.ProductNo, MAX(PurchaseMaster.RatePerUnit) AS Highest, MIN(PurchaseMaster.RatePerUnit) AS Lowest,  " +
                 " InventorySizeMaster.Volume, InventoryDetails.Weight,InventoryDetails.UnitTypeId, InventoryWarehouseMasterTbl.InventoryWarehouseMasterId,InventoryWarehouseMasterTbl.WareHouseId, InventoryWarehouseMasterTbl.Rate " +
                 " FROM         InventoryDetails LEFT OUTER JOIN " +
                 " InventorySizeMaster RIGHT OUTER JOIN " +
                 " PurchaseDetails LEFT OUTER JOIN " +
                 " InventoryWarehouseMasterTbl LEFT OUTER JOIN " +
                 " PurchaseMaster ON InventoryWarehouseMasterTbl.InventoryWarehouseMasterId = PurchaseMaster.InventoryWHM_Id LEFT OUTER JOIN " +
                 " InventoryMaster ON InventoryWarehouseMasterTbl.InventoryMasterId = InventoryMaster.InventoryMasterId ON " +
                 " PurchaseDetails.Purchase_Details_Id = PurchaseMaster.Purchase_Details_Id ON InventorySizeMaster.InventoryMasterId = InventoryMaster.InventoryMasterId ON  " +
                 " InventoryDetails.Inventory_Details_Id = InventoryMaster.InventoryDetailsId left outer join InventoryMasterMNC on  InventoryMasterMNC.Inventorymasterid=InventoryMaster.InventoryMasterId   " +
                 " WHERE     (InventoryWarehouseMasterTbl.InventoryWarehouseMasterId= '" + ddlItemName.SelectedValue + "') and InventoryWarehouseMasterTbl.WareHouseId='" + Convert.ToInt32(ddlWarehouse.SelectedValue) + "' " +
                 " GROUP BY InventoryMaster.Name, InventoryMaster.ProductNo, InventorySizeMaster.Volume, InventoryDetails.Weight,InventoryDetails.UnitTypeId, " +
                 " InventoryWarehouseMasterTbl.InventoryWarehouseMasterId, InventoryWarehouseMasterTbl.Rate,InventoryWarehouseMasterTbl.WareHouseId ";

        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);

        //}
    }
    protected void btSubmit_Click(object sender, EventArgs e)
    {


        ////string st = "INSERT INTO dbo.InventoryRuleMaster(RuleName,AppliedDate,Description)VALUES " +
        ////           " ('" + txtRuleName.Text + "','" + System.DateTime.Now + "','" + txtNote.Text + "')";
        ////SqlCommand cmd1 = new SqlCommand(st, con);
        ////cmd1.CommandType = CommandType.Text;

        ////string st1 = "";
        ////SqlCommand cmd2 = new SqlCommand(st1, con);
        ////cmd2.CommandType = CommandType.Text;










        ////string str = " UPDATE InventoryWarehouseMasterTbl SET Rate ='" + Convert.ToDecimal(txtTotalAmt.Text) + "'" +
        ////            " Where InventoryWarehouseMasterId = '" + ddlItemName.SelectedValue + "'";

        ////SqlCommand cmd = new SqlCommand(str, con);
        ////cmd.CommandType = CommandType.Text;

        ////try
        ////{
        ////    con.Open();
        ////    cmd.ExecuteNonQuery();
        ////    con.Close();
        ////    pnlCal.Visible =true ;
        ////    pnlConfirm.Visible = false;
        ////    lblMsg.Visible = true;
        ////    lblMsg.Text = "Price Updated."; // +ex.Message;
        ////}


        ////catch (Exception ex)
        ////{
        ////    lblMsg.Visible = true;
        ////    lblMsg.Text = "Error :" + ex.Message;
        ////}
    }
    protected void ddlWarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlCal.Visible = true;
        ddlCategory.DataSource = (DataSet)fiillcategory();
       
        ddlCategory.DataTextField = "expr";
        ddlCategory.DataValueField = "InventorySubSubId";
        ddlCategory.DataBind();

        ddlCategory_SelectedIndexChanged(sender, e);
        RadioButtonList2_SelectedIndexChanged(sender, e);
        //ddlCategory.Items.Insert(0, "--Select--");
        //ddlCategory.Items[0].Value = "0";

        
    }
    public decimal isnumSelf(string ck)
    {
        decimal i = 0;
        try
        {
            i = Convert.ToDecimal(ck);
        }
        catch
        {
            i = 0;
        }
        return i;
    }
    public Double isdoubleornot(string ck)
    {
        Double i = 0;
        try
        {
            i = Convert.ToDouble(ck);
        }
        catch
        {
            i = 0;
        }
        return i;
    }
    public double gmtoLBS()
    {
        // consider 1 gm to lbs
        double i = 0;
        try
        {
            //i = 1 / 453.59237;
            i = 453.59237;
            return i;
        }
        catch
        {

        }

        return i;
    }
    public double gmtoOZ()
    {
        // consider 1 gm to oz
        double i = 0;
        try
        {
            //i = 1 / 28.3492;
            i = 28.3492;
            return i;
        }
        catch
        {

        }
        return i;
    }
    protected Double convertto1GM(string tx, string unit, string forUNITYPE)
    {
        // unit = from unit, forUNITYPE = tounit
        double igm = 1;
        double lbsfrom1gm = gmtoLBS();
        double ozfrom1gm = gmtoOZ();

        try
        {

            if (unit == "lbs" && forUNITYPE == "gm")
            {
                //            lbs
                igm = Convert.ToDouble(isdoubleornot(tx) / 453.59237);
                // return igm;


            }
            else if (unit == "gram" && forUNITYPE == "gm")
            {
                //gram
                igm = isdoubleornot(tx);
                // return igm;

            }
            else if (unit == "kg" && forUNITYPE == "gm")
            {
                //kg
                igm = Convert.ToDouble(isdoubleornot(tx) / 1000);
                // return igm;
            }
            else if (unit == "oz" && forUNITYPE == "gm")
            {
                //ohs
                igm = Convert.ToDouble(isdoubleornot(tx) / 28.3492);
                // return igm;
            }
            else if (unit == "ltr" && forUNITYPE == "gm")
            {//ltr
                igm = Convert.ToDouble(isdoubleornot(tx) / 1000);
                // return igm;
            }
            else if (unit == "ml" && forUNITYPE == "gm")
            { //ml
                igm = isdoubleornot(tx);
                // return igm;
            }
            else if (unit == "mili gram" && forUNITYPE == "gm")
            { //mili gram
                igm = Convert.ToDouble(isdoubleornot(tx) / 0.001);
                return igm;
            }
            else if (unit == "lbs" && forUNITYPE == "lbs")
            {
                //            lbs
                igm = Convert.ToDouble((isdoubleornot(tx) / 453.59237) * lbsfrom1gm);
                return igm;


            }
            else if (unit == "gram" && forUNITYPE == "lbs")
            {
                //gram
                igm = Convert.ToDouble(lbsfrom1gm * isdoubleornot(tx));
                return igm;

            }
            else if (unit == "kg" && forUNITYPE == "lbs")
            {
                //kg
                igm = Convert.ToDouble((isdoubleornot(tx) / 1000) * lbsfrom1gm);
                return igm;
            }
            else if (unit == "oz" && forUNITYPE == "lbs")
            {
                //ohs
                igm = Convert.ToDouble((isdoubleornot(tx) / 28.3492) * lbsfrom1gm);
                return igm;
            }
            else if (unit == "ltr" && forUNITYPE == "lbs")
            {//ltr
                igm = Convert.ToDouble((isdoubleornot(tx) / 1000) * lbsfrom1gm);
                return igm;
            }
            else if (unit == "ml" && forUNITYPE == "lbs")
            { //ml
                igm = Convert.ToDouble(isdoubleornot(tx) * lbsfrom1gm);
                return igm;
            }
            else if (unit == "mili gram" && forUNITYPE == "lbs")
            { //mili gram
                igm = Convert.ToDouble((isdoubleornot(tx) / 0.001) * lbsfrom1gm);
                return igm;
            }
            else if (unit == "lbs" && forUNITYPE == "oz")
            {
                //            lbs
                igm = Convert.ToDouble((isdoubleornot(tx) / 453.59237) * ozfrom1gm);
                return igm;


            }
            else if (unit == "gram" && forUNITYPE == "oz")
            {
                //gram
                igm = Convert.ToDouble(ozfrom1gm * isdoubleornot(tx));
                return igm;

            }
            else if (unit == "kg" && forUNITYPE == "oz")
            {
                //kg
                igm = Convert.ToDouble((isdoubleornot(tx) / 1000) * ozfrom1gm);
                return igm;
            }
            else if (unit == "oz" && forUNITYPE == "oz")
            {
                //ohs
                igm = Convert.ToDouble((isdoubleornot(tx) / 28.3492) * ozfrom1gm);
                return igm;
            }
            else if (unit == "ltr" && forUNITYPE == "oz")
            {//ltr
                igm = Convert.ToDouble((isdoubleornot(tx) / 1000) * ozfrom1gm);
                return igm;
            }
            else if (unit == "ml" && forUNITYPE == "oz")
            { //ml
                igm = Convert.ToDouble(isdoubleornot(tx) * ozfrom1gm);
                return igm;
            }
            else if (unit == "mili gram" && forUNITYPE == "oz")
            { //mili gram
                igm = Convert.ToDouble((isdoubleornot(tx) / 0.001) * ozfrom1gm);
                return igm;
            }
            else
            {
                igm = 0;
                return igm;
            }

            ////////////////////////////if (forUNITYPE == "kg" || forUNITYPE == "ltr")
            ////////////////////////////{
            ////////////////////////////    igm = convertto1GM(tx, stdd, unit, "gm");
            ////////////////////////////    igm *= 1000;
            ////////////////////////////}
            ////////////////////////////else if (forUNITYPE == "mili gram")
            ////////////////////////////{
            ////////////////////////////    igm = convertto1GM(tx, stdd, unit, "gm");
            ////////////////////////////    igm *= 0.001;
            ////////////////////////////}
            ////////////////////////////else
            ////////////////////////////{

            ////////////////////////////}




            //if (forUNITYPE == "gm" || forUNITYPE=="ml")
            //{

            //}
            //else if (forUNITYPE == "oz")
            //{

            //}
            //else if (forUNITYPE == "lbs")
            //{

            //}
            //else 




        }
        catch
        {

        }
        return igm;

    }
    protected double CovenvrtDBtoAPPLunitForWGTfactore(double DB_wg, string DB_wg_unit,
        double APPL_wg, string APPL_wg_unit, double Selectd_rate)
    {
        double A = 0;
        //        lbs
        //gram
        //kg
        //oz
        //ltr
        //ml
        //mili gram
        double cnv_with_A = 1;

        if (DB_wg_unit == "lbs")
        {
            cnv_with_A = 453.59237;
        }
        else if (DB_wg_unit == "gram")
        {
            cnv_with_A = 1;
        }
        else if (DB_wg_unit == "kg")
        {
            cnv_with_A = 1000;
        }
        else if (DB_wg_unit == "oz")
        {
            cnv_with_A = 28.35;
        }
        else if (DB_wg_unit == "ltr")
        {
            cnv_with_A = 1000;
        }
        else if (DB_wg_unit == "ml")
        {
            cnv_with_A = 1;
        }
        else if (DB_wg_unit == "mili gram")
        {
            cnv_with_A = 0.001;
        }
        else
        {

        }

        double B = 0;

        double cnv_with_B = 1;

        if (APPL_wg_unit == "lbs")
        {
            cnv_with_B = 1 / 453.59237;
        }
        else if (APPL_wg_unit == "grBm")
        {
            cnv_with_B = 1 / 1;
        }
        else if (APPL_wg_unit == "kg")
        {
            cnv_with_B = 1 / 1000;
        }
        else if (APPL_wg_unit == "oz")
        {
            cnv_with_B = 1 / 28.35;
        }
        else if (APPL_wg_unit == "ltr")
        {
            cnv_with_B = 1 / 1000;
        }
        else if (APPL_wg_unit == "ml")
        {
            cnv_with_B = 1 / 1;
        }
        else if (APPL_wg_unit == "mili grBm")
        {
            cnv_with_B = 1 / 0.001;
        }
        else
        {

        }
        // this value gives single unit, selected rate in form of grams.... from DB
        ////////  double DB_Rate_Per_Unit = Convert.ToDouble(Selectd_rate / DB_wg);
        ////////  double DB_Cnvrted_gm = cnv_with_A;

        ////////  // this value gives sigle unit,selected rate in form of grams... applied for wght factor
        ////////  double APPL_rate_Per_unit = Convert.ToDouble(Selectd_rate / APPL_wg);
        ////////  double APPL_Cnvrted_gm = cnv_with_B;


        ////////double wg_fct_for_single_unit = Convert.ToDouble((APPL_rate_Per_unit * DB_Cnvrted_gm) / APPL_Cnvrted_gm);

        ////////  double wg_fact = Convert.ToDouble(DB_wg * wg_fct_for_single_unit);

        //string n = wg_fact.ToString().IndexOf('.') + 3;

        double DB_wg_into_GM = Convert.ToDouble(DB_wg * cnv_with_A * cnv_with_B);

        double wg_fact = Convert.ToDouble(APPL_wg * DB_wg_into_GM);
        return wg_fact;
    }


    public double FactoreCalculation()
    {
        double i = 0;
        try
        {

        }
        catch
        {

        }
        return i;

    }

    protected void btCal_Click(object sender, ImageClickEventArgs e)
    {
        if (lblRate.Text == "")
        {
            lblMsg.Visible = true;
            lblMsg.Text = " Select the Rate then Caculate";

        }
        else
        {
            DataTable ds1 = new DataTable();
            ds1 = (DataTable)ViewState["ds"];
            txtItemNo.Text = ds1.Rows[0]["ProductNo"].ToString();
            txtname.Text = ds1.Rows[0]["Name"].ToString();

            txtPrice.Text = lblRate.Text;






            string unittype_frm_db = "lbs";
            int unitidfmDT = Convert.ToInt32(ds1.Rows[0]["UnitTypeId"]);

            double whtofproduct = Convert.ToDouble(isdoubleornot(ds1.Rows[0]["Weight"].ToString()));


            string strserUNITNAME = " SELECT  [UnitTypeId] ,[Name]  FROM  [UnitTypeMaster] where  [UnitTypeId]='" + unitidfmDT + "'";
            SqlCommand cmdUNtnm = new SqlCommand(strserUNITNAME, con);
            SqlDataAdapter adpUNTnm = new SqlDataAdapter(cmdUNtnm);
            DataTable dtunnm = new DataTable();
            adpUNTnm.Fill(dtunnm);
            if (dtunnm.Rows.Count > 0)
            {
                unittype_frm_db = dtunnm.Rows[0]["Name"].ToString();
            }



            Mar = (Convert.ToDouble(txtmar.Text) * Convert.ToDouble(lblRate.Text)) / 100;//) / Convert.ToDecimal(lblRate.Text);

            txtmargin.Text = Mar.ToString();

            FlatRate = Convert.ToDouble(isdoubleornot(txtFlat.Text));
            // int j = FlatRate.ToString().IndexOf('.') + 3;
            txtFlatAmt.Text = FlatRate.ToString();




            //weight factore calculation......

            string forUNITYPE = DropDownList1.SelectedItem.Text;
            ////////////////////double igm = convertto1GM(txtItemWeight.Text, Unitnametocconvert, forUNITYPE);

            //////////////////// if (forUNITYPE == "kg" || forUNITYPE == "ltr")
            ////////////////////{
            ////////////////////    igm = convertto1GM(txtItemWeight.Text, Unitnametocconvert, "gm");
            ////////////////////    igm *= 1000;
            ////////////////////}
            ////////////////////else if (forUNITYPE == "mili gram")
            ////////////////////{
            ////////////////////    igm = convertto1GM(txtItemWeight.Text, Unitnametocconvert, "gm");
            ////////////////////    igm *= 0.001;
            ////////////////////}
            ////////////////////else
            ////////////////////{
            ////////////////////    igm = convertto1GM(txtItemWeight.Text, Unitnametocconvert, forUNITYPE);

            ////////////////////}
            //////////////////// txtWeight.Text = Convert.ToDouble(whtofproduct * igm).ToString();

            //////////////////// //weight factore calculation...... END

            if (txtItemWeight.Text != "0")
            {
                Weight = Convert.ToDouble(CovenvrtDBtoAPPLunitForWGTfactore(whtofproduct,
                    unittype_frm_db,
                    isdoubleornot(txtItemWeight.Text),
                    DropDownList1.SelectedItem.Text,
                    isdoubleornot(lblRate.Text)));

            }
            else
            {
                Weight = 0;
            }










            //if (DropDownList1.SelectedValue == "1")
            //{
            //    //decimal ohf = Convert.ToDecimal(isnumSelf(txtItemWeight.Text)) * Convert.ToDecimal(isnumSelf("0.4535"));
            //   // overhead = Convert.ToDecimal(isnumSelf(ohf.ToString())) * Convert.ToDecimal(isnumSelf(ds1.Rows[0]["Weight"].ToString()));
            //    Weight = Convert.ToDecimal(isnumSelf(txtItemWeight.Text)) * Convert.ToDecimal(isnumSelf(ds1.Rows[0]["Weight"].ToString()));

            //}
            //else
            //{
            //    //overhead = Convert.ToDecimal(isnumSelf(txtOverhead1.Text)) * Convert.ToDecimal(isnumSelf(ds1.Rows[0]["Weight"].ToString()));

            //    Weight = Convert.ToDecimal(isnumSelf(txtItemWeight.Text)) * Convert.ToDecimal((ds1.Rows[0]["Weight"].ToString()));

            //}

            //Weight = Convert.ToDecimal(isnumSelf(txtItemWeight.Text)) * Convert.ToDecimal((ds1.Rows[0]["Weight"].ToString()));
            int k = Weight.ToString().IndexOf('.') + 3;
            if (Weight.ToString().Length > k)
            {

                txtWeight.Text = Weight.ToString().Substring(0, k);
            }
            else
            {
                txtWeight.Text = Weight.ToString();
            }



            if (ds1.Rows[0]["Volume"].ToString() != "")
            {
                VolumeFactor = Convert.ToDouble(isdoubleornot(txtItemVolume.Text)) * Convert.ToDouble(isdoubleornot(ds1.Rows[0]["Volume"].ToString()));
            }
            // int l = VolumeFactor.ToString().IndexOf('.') + 3;
            txtVolume.Text = VolumeFactor.ToString();








            //overhead factore calculation......

            //////////////////////////////////string forUNITYPE1 = DropDownList3.SelectedItem.Text;
            //////////////////////////////////double igm1 = convertto1GM(txtOverhead1.Text, Unitnametocconvert, forUNITYPE1);

            //////////////////////////////////if (forUNITYPE1 == "kg" || forUNITYPE1 == "ltr")
            //////////////////////////////////{
            //////////////////////////////////    igm1 = convertto1GM(txtOverhead1.Text, Unitnametocconvert, "gm");
            //////////////////////////////////    igm1 *= 1000;
            //////////////////////////////////}
            //////////////////////////////////else if (forUNITYPE == "mili gram")
            //////////////////////////////////{
            //////////////////////////////////    igm1 = convertto1GM(txtOverhead1.Text, Unitnametocconvert, "gm");
            //////////////////////////////////    igm1 *= 0.001;
            //////////////////////////////////}
            //////////////////////////////////else
            //////////////////////////////////{
            //////////////////////////////////    igm1 = convertto1GM(txtOverhead1.Text, Unitnametocconvert, forUNITYPE1);

            //////////////////////////////////}
            //////////////////////////////////txtOverhead.Text = Convert.ToDouble(whtofproduct * igm1).ToString();

            ////////////////////////////////////overhead factore calculation...... END

            //////////////////////////////////overhead = Convert.ToDecimal(isnumSelf(txtOverhead.Text));

            if (txtOverhead1.Text != "0")
            {
                overhead = Convert.ToDouble(CovenvrtDBtoAPPLunitForWGTfactore(whtofproduct,
                   unittype_frm_db,
                   isdoubleornot(txtOverhead1.Text),
                   DropDownList3.SelectedItem.Text,
                   isdoubleornot(lblRate.Text)));
            }
            else
            {
                overhead = 0;
            }










            //////////if (ds1.Rows[0]["Weight"].ToString() != "")
            //////////{
            //////////    if (DropDownList3.SelectedValue == "1")
            //////////    {
            //////////        decimal ohf = Convert.ToDecimal(isnumSelf(txtOverhead1.Text)) * Convert.ToDecimal(isnumSelf("0.4535"));
            //////////        overhead = Convert.ToDecimal(isnumSelf(ohf.ToString())) * Convert.ToDecimal(isnumSelf(ds1.Rows[0]["Weight"].ToString()));
            //////////    }
            //////////    else
            //////////    {
            //////////        overhead = Convert.ToDecimal(isnumSelf(txtOverhead1.Text)) * Convert.ToDecimal(isnumSelf(ds1.Rows[0]["Weight"].ToString()));


            //////////    }


            //////////}
            int m = overhead.ToString().IndexOf('.') + 3;
            if (overhead.ToString().Length > m)
            {
                txtOverhead.Text = overhead.ToString().Substring(0, m);
            }
            else
            {
                txtOverhead.Text = overhead.ToString();
            }



            Rate = Convert.ToDouble(isnumSelf(lblRate.Text));
            Double selectedprice = Convert.ToDouble(txtPrice.Text);
            total = Rate + Mar + FlatRate + Weight + VolumeFactor + overhead;// +selectedprice;
            int n = total.ToString().IndexOf('.') + 3;
            if (total.ToString().Length > n)
            {
                txtTotalAmt.Text = total.ToString().Substring(0, n);
            }
            else
            {
                txtTotalAmt.Text = total.ToString();
            }

            pnlCal.Visible = false;
            pnlConfirm.Visible = true;
            Label1.Text = txtname.Text;
            Label2.Text = txtItemNo.Text;
            Label3.Text = txtPrice.Text;
        }
    }
    protected void btSubmit_Click(object sender, ImageClickEventArgs e)
    {
        string st = "INSERT INTO dbo.InventoryRuleMaster(RuleName,AppliedDate,Description,compid)VALUES " +
                  " ('" + txtRuleName.Text + "','" + System.DateTime.Now + "','" + txtNote.Text + "','" + compid + "')";
        SqlCommand cmd1 = new SqlCommand(st, con);
        cmd1.CommandType = CommandType.Text;

        string st1 = "";
        SqlCommand cmd2 = new SqlCommand(st1, con);
        cmd2.CommandType = CommandType.Text;


        string str = " UPDATE InventoryWarehouseMasterTbl SET Rate ='" + Convert.ToDecimal(txtTotalAmt.Text) + "'" +
                    " Where InventoryWarehouseMasterId = '" + ddlItemName.SelectedValue + "'";

        SqlCommand cmd = new SqlCommand(str, con);
        cmd.CommandType = CommandType.Text;

        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            pnlCal.Visible = true;
            pnlConfirm.Visible = false;
            lblMsg.Visible = true;
            lblMsg.Text = "Price Updated."; // +ex.Message;
        }


        catch (Exception ex)
        {
            lblMsg.Visible = true;
            lblMsg.Text = "Error :" + ex.Message;
        }







    }
    protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblRate.Text = "";

        string Str = "select Rate from InventoryWarehouseMasterTbl where InventoryWarehouseMasterId='" + ddlItemName .SelectedValue+ "'";
        SqlCommand cmd = new SqlCommand(Str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        if (ds.Rows.Count > 0)
        {
            ExistingSalesRate.Text = ds.Rows[0]["Rate"].ToString();
            ExistRate.Text = ds.Rows[0]["Rate"].ToString();
        }
        RadioButtonList2_SelectedIndexChanged(sender, e);
    }
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void txtOverhead1_TextChanged(object sender, EventArgs e)
    {

    }



    protected void Btn_Submit_Click(object sender, EventArgs e)
    {
        Btn_Calculate_Click(sender, e);

        String selectStr = "Select * from InventoryRuleMaster where Whid = '" + ddlWarehouse.SelectedValue + "' and RuleName='" +txtRuleName.Text+ "'";
        SqlCommand selectCmd = new SqlCommand(selectStr, con);
        SqlDataAdapter dtp = new SqlDataAdapter(selectCmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            Label1.Visible = true;
            Label1.Text = "Record already exist";
            
        }
        else
        {

            string str = "Insert Into InventoryRuleMaster (RuleName,AppliedDate,compid,Margin,FlatRateAmount,WeightFactor,WeightUnitId,VolumeFactor,VolumeUnitId,OverHeadFactor,OverHeadUnitId,RateApplied,InvWMasterId,Whid,Highest,Lowest,Recent,Average,ExistingSalesRate,SelectedRate,InventorySubSubId) values ('" + txtRuleName.Text + "','" + System.DateTime.Now + "','" + Session["Comid"].ToString() + "','" + txtmar.Text + "','" + txtFlat.Text + "','" + txtItemWeight.Text + "','" + DropDownList1.SelectedValue + "','" + txtItemVolume.Text + "','" + DropDownList2.SelectedValue + "','" + txtOverhead1.Text + "','" + DropDownList3.SelectedValue + "','" + lblTotalAmt.Text + "','" + ddlItemName.SelectedValue + "','" + ddlWarehouse.SelectedValue + "','" + RadioButtonList2.SelectedValue + "','" + RadioButtonList2.SelectedValue + "','" + RadioButtonList2.SelectedValue + "','" + RadioButtonList2.SelectedValue + "','" + ExistingSalesRate.Text + "','" + lblRate.Text + "','" + ddlCategory.SelectedValue + "')";
        SqlCommand cmd1 = new SqlCommand(str, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd1.ExecuteNonQuery();
        con.Close();

        string str12 = " UPDATE InventoryWarehouseMasterTbl SET Rate ='" + Convert.ToDecimal(lblTotalAmt.Text) + "'" +
                    " Where InventoryWarehouseMasterId = '" + ddlItemName.SelectedValue + "'";

        SqlCommand cmd123 = new SqlCommand(str12, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd123.ExecuteNonQuery();
        con.Close();


        lblMsg.Visible = true;
        lblMsg.Text = "Price updated sucessfully";
        Fillgrid();
        clearall();

        pnladd.Visible = false;
        lbllegend.Visible = false;
        btnadd.Visible = true;
        lbllegend.Text = "Add Sales Rate Determination";
        }


    }
    protected void Btn_Calculate_Click(object sender, EventArgs e)
    {
        if (lblRate.Text == "")
        {
            lblMsg.Visible = true;
            lblMsg.Text = " Select the Rate then Caculate";

        }
        else
        {
            DataTable ds1 = new DataTable();
            ds1 = (DataTable)ViewState["ds"];
            txtItemNo.Text = ds1.Rows[0]["ProductNo"].ToString();
            txtname.Text = ds1.Rows[0]["Name"].ToString();

            txtPrice.Text = lblRate.Text;






            string unittype_frm_db = "lbs";
            int unitidfmDT = Convert.ToInt32(ds1.Rows[0]["UnitTypeId"]);

            double whtofproduct = Convert.ToDouble(isdoubleornot(ds1.Rows[0]["Weight"].ToString()));


            string strserUNITNAME = " SELECT  [UnitTypeId] ,[Name]  FROM  [UnitTypeMaster] where  [UnitTypeId]='" + unitidfmDT + "'";
            SqlCommand cmdUNtnm = new SqlCommand(strserUNITNAME, con);
            SqlDataAdapter adpUNTnm = new SqlDataAdapter(cmdUNtnm);
            DataTable dtunnm = new DataTable();
            adpUNTnm.Fill(dtunnm);
            if (dtunnm.Rows.Count > 0)
            {
                unittype_frm_db = dtunnm.Rows[0]["Name"].ToString();
            }



            Mar = (Convert.ToDouble(txtmar.Text) * Convert.ToDouble(lblRate.Text)) / 100;//) / Convert.ToDecimal(lblRate.Text);

            txtmargin.Text = Mar.ToString();

            FlatRate = Convert.ToDouble(isdoubleornot(txtFlat.Text));
            // int j = FlatRate.ToString().IndexOf('.') + 3;
            txtFlatAmt.Text = FlatRate.ToString();




            //weight factore calculation......

            string forUNITYPE = DropDownList1.SelectedItem.Text;
            ////////////////////double igm = convertto1GM(txtItemWeight.Text, Unitnametocconvert, forUNITYPE);

            //////////////////// if (forUNITYPE == "kg" || forUNITYPE == "ltr")
            ////////////////////{
            ////////////////////    igm = convertto1GM(txtItemWeight.Text, Unitnametocconvert, "gm");
            ////////////////////    igm *= 1000;
            ////////////////////}
            ////////////////////else if (forUNITYPE == "mili gram")
            ////////////////////{
            ////////////////////    igm = convertto1GM(txtItemWeight.Text, Unitnametocconvert, "gm");
            ////////////////////    igm *= 0.001;
            ////////////////////}
            ////////////////////else
            ////////////////////{
            ////////////////////    igm = convertto1GM(txtItemWeight.Text, Unitnametocconvert, forUNITYPE);

            ////////////////////}
            //////////////////// txtWeight.Text = Convert.ToDouble(whtofproduct * igm).ToString();

            //////////////////// //weight factore calculation...... END

            if (txtItemWeight.Text != "0")
            {
                Weight = Convert.ToDouble(CovenvrtDBtoAPPLunitForWGTfactore(whtofproduct,
                    unittype_frm_db,
                    isdoubleornot(txtItemWeight.Text),
                    DropDownList1.SelectedItem.Text,
                    isdoubleornot(lblRate.Text)));

            }
            else
            {
                Weight = 0;
            }










            //if (DropDownList1.SelectedValue == "1")
            //{
            //    //decimal ohf = Convert.ToDecimal(isnumSelf(txtItemWeight.Text)) * Convert.ToDecimal(isnumSelf("0.4535"));
            //   // overhead = Convert.ToDecimal(isnumSelf(ohf.ToString())) * Convert.ToDecimal(isnumSelf(ds1.Rows[0]["Weight"].ToString()));
            //    Weight = Convert.ToDecimal(isnumSelf(txtItemWeight.Text)) * Convert.ToDecimal(isnumSelf(ds1.Rows[0]["Weight"].ToString()));

            //}
            //else
            //{
            //    //overhead = Convert.ToDecimal(isnumSelf(txtOverhead1.Text)) * Convert.ToDecimal(isnumSelf(ds1.Rows[0]["Weight"].ToString()));

            //    Weight = Convert.ToDecimal(isnumSelf(txtItemWeight.Text)) * Convert.ToDecimal((ds1.Rows[0]["Weight"].ToString()));

            //}

            //Weight = Convert.ToDecimal(isnumSelf(txtItemWeight.Text)) * Convert.ToDecimal((ds1.Rows[0]["Weight"].ToString()));
            int k = Weight.ToString().IndexOf('.') + 3;
            if (Weight.ToString().Length > k)
            {

                txtWeight.Text = Weight.ToString().Substring(0, k);
            }
            else
            {
                txtWeight.Text = Weight.ToString();
            }



            if (ds1.Rows[0]["Volume"].ToString() != "")
            {
                VolumeFactor = Convert.ToDouble(isdoubleornot(txtItemVolume.Text)) * Convert.ToDouble(isdoubleornot(ds1.Rows[0]["Volume"].ToString()));
            }
            // int l = VolumeFactor.ToString().IndexOf('.') + 3;
            txtVolume.Text = VolumeFactor.ToString();








            //overhead factore calculation......

            //////////////////////////////////string forUNITYPE1 = DropDownList3.SelectedItem.Text;
            //////////////////////////////////double igm1 = convertto1GM(txtOverhead1.Text, Unitnametocconvert, forUNITYPE1);

            //////////////////////////////////if (forUNITYPE1 == "kg" || forUNITYPE1 == "ltr")
            //////////////////////////////////{
            //////////////////////////////////    igm1 = convertto1GM(txtOverhead1.Text, Unitnametocconvert, "gm");
            //////////////////////////////////    igm1 *= 1000;
            //////////////////////////////////}
            //////////////////////////////////else if (forUNITYPE == "mili gram")
            //////////////////////////////////{
            //////////////////////////////////    igm1 = convertto1GM(txtOverhead1.Text, Unitnametocconvert, "gm");
            //////////////////////////////////    igm1 *= 0.001;
            //////////////////////////////////}
            //////////////////////////////////else
            //////////////////////////////////{
            //////////////////////////////////    igm1 = convertto1GM(txtOverhead1.Text, Unitnametocconvert, forUNITYPE1);

            //////////////////////////////////}
            //////////////////////////////////txtOverhead.Text = Convert.ToDouble(whtofproduct * igm1).ToString();

            ////////////////////////////////////overhead factore calculation...... END

            //////////////////////////////////overhead = Convert.ToDecimal(isnumSelf(txtOverhead.Text));

            if (txtOverhead1.Text != "0")
            {
                overhead = Convert.ToDouble(CovenvrtDBtoAPPLunitForWGTfactore(whtofproduct,
                   unittype_frm_db,
                   isdoubleornot(txtOverhead1.Text),
                   DropDownList3.SelectedItem.Text,
                   isdoubleornot(lblRate.Text)));
            }
            else
            {
                overhead = 0;
            }










            //////////if (ds1.Rows[0]["Weight"].ToString() != "")
            //////////{
            //////////    if (DropDownList3.SelectedValue == "1")
            //////////    {
            //////////        decimal ohf = Convert.ToDecimal(isnumSelf(txtOverhead1.Text)) * Convert.ToDecimal(isnumSelf("0.4535"));
            //////////        overhead = Convert.ToDecimal(isnumSelf(ohf.ToString())) * Convert.ToDecimal(isnumSelf(ds1.Rows[0]["Weight"].ToString()));
            //////////    }
            //////////    else
            //////////    {
            //////////        overhead = Convert.ToDecimal(isnumSelf(txtOverhead1.Text)) * Convert.ToDecimal(isnumSelf(ds1.Rows[0]["Weight"].ToString()));


            //////////    }


            //////////}
            int m = overhead.ToString().IndexOf('.') + 3;
            if (overhead.ToString().Length > m)
            {
                txtOverhead.Text = overhead.ToString().Substring(0, m);
            }
            else
            {
                txtOverhead.Text = overhead.ToString();
            }



            Rate = Convert.ToDouble(isnumSelf(lblRate.Text));
            Double selectedprice = Convert.ToDouble(txtPrice.Text);
            total = Rate + Mar + FlatRate + Weight + VolumeFactor + overhead;// +selectedprice;
            int n = total.ToString().IndexOf('.') + 3;
            if (total.ToString().Length > n)
            {
                txtTotalAmt.Text = total.ToString().Substring(0, n);
                lblTotalAmt.Text = total.ToString().Substring(0, n);
            }
            else
            {
                txtTotalAmt.Text = total.ToString();
                lblTotalAmt.Text = total.ToString();
            }

            pnlCal.Visible = true;
            pnlConfirm.Visible = false;
            Label1.Text = txtname.Text;
            Label2.Text = txtItemNo.Text;
            Label3.Text = txtPrice.Text;
        }
    }


    protected void Fillgrid()
    {
        string str1= "";
        string str2 = "";

         str1 = " select InventoryRuleMaster.*,InventoryMaster.Name,WareHouseMaster.Name as WName from InventoryRuleMaster inner join InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.InventoryWarehouseMasterId=InventoryRuleMaster.InvWMasterId inner join InventoryMaster on InventoryWarehouseMasterTbl.InventoryMasterId=InventoryMaster.InventoryMasterId inner join WareHouseMaster on WareHouseMaster.WareHouseId=InventoryRuleMaster.Whid where InventoryRuleMaster.compid='" + Session["comid"] + "' ";

        if (ddlfilterbusiness.SelectedIndex > 0)
        {
            str2 = "and InventoryRuleMaster.Whid='"+ddlfilterbusiness.SelectedValue+"' ";
        }
        string str = str1 + str2;

        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        if (ds.Rows.Count > 0)
        {
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
        


    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string st2 = "Delete from InventoryRuleMaster where RuleMasterId='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "' ";
        SqlCommand cmd2 = new SqlCommand(st2, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd2.ExecuteNonQuery();
        con.Close();
        GridView1.EditIndex = -1;
        Fillgrid();

      //  fillgrid();
      //  lblmsg.Visible = true;
      //  lblmsg.Text = "Record Succesfully Deleted";
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        Fillgrid();
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void LinkButton4_Click(object sender, ImageClickEventArgs e)
    {
        Button3.Visible = true;
        Btn_Submit.Visible = false;

        pnladd.Visible = true;
        lbllegend.Visible = true;
        btnadd.Visible = false;
        lbllegend.Text = "Edit Sales Rate Determination";

        ImageButton lk = (ImageButton)sender;
        int j = Convert.ToInt32(lk.CommandArgument);
        ViewState["Id"] = j;
        Session["TimeId"] = j;

        string str = "select * from InventoryRuleMaster where RuleMasterId='" + j + "'";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        adp.Fill(dt);

       

        fillwarehouse();

       // ddlWarehouse.SelectedValue = dt.Rows[0]["Whid"].ToString();

        ddlWarehouse.SelectedIndex = ddlWarehouse.Items.IndexOf(ddlWarehouse.Items.FindByValue(dt.Rows[0]["Whid"].ToString()));


        Selectedindexchange();
        ddlCategory.SelectedIndex = ddlCategory.Items.IndexOf(ddlCategory.Items.FindByValue(dt.Rows[0]["InventorySubSubId"].ToString()));
        mainitemindexchange();
        ddlItemName.SelectedIndex = ddlItemName.Items.IndexOf(ddlItemName.Items.FindByValue(dt.Rows[0]["InvWMasterId"].ToString()));

        existingsalesrate();

        lblRate.Text = dt.Rows[0]["SelectedRate"].ToString();

        txtmar.Text = dt.Rows[0]["Margin"].ToString();
        txtFlat.Text = dt.Rows[0]["FlatRateAmount"].ToString();
        txtItemWeight.Text = dt.Rows[0]["WeightFactor"].ToString();
        txtItemVolume.Text = dt.Rows[0]["VolumeFactor"].ToString();
        txtOverhead1.Text = dt.Rows[0]["OverHeadFactor"].ToString();
       // ExistRate.Text = dt.Rows[0]["ExistingSalesRate"].ToString();
        lblTotalAmt.Text = dt.Rows[0]["RateApplied"].ToString();
        txtRuleName.Text = dt.Rows[0]["RuleName"].ToString();
        lbldate.Text = dt.Rows[0]["AppliedDate"].ToString();

        fillUnittypes();

        FillVolumeunitsDDL();

        RadioButtonList2.SelectedIndex = RadioButtonList2.Items.IndexOf(RadioButtonList2.Items.FindByValue(dt.Rows[0]["Highest"].ToString()));
       
        DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByValue(dt.Rows[0]["WeightUnitId"].ToString()));
        DropDownList2.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByValue(dt.Rows[0]["VolumeUnitId"].ToString()));
        DropDownList3.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByValue(dt.Rows[0]["OverHeadUnitId"].ToString()));
       
       


       

    }
    protected void fillwarehouse()
    {
        string strwh = "SELECT WareHouseId,Name,Address,CurrencyId FROM WareHouseMaster where comid='" + compid + "' and [WareHouseMaster].status = '1'";
        SqlCommand cmdwh = new SqlCommand(strwh, con);
        SqlDataAdapter adpwh = new SqlDataAdapter(cmdwh);
        DataTable dtwh = new DataTable();
        adpwh.Fill(dtwh);

        ddlWarehouse.DataSource = dtwh;
        ddlWarehouse.DataTextField = "Name";
        ddlWarehouse.DataValueField = "WareHouseId";
        ddlWarehouse.DataBind();
    }

    protected void Selectedindexchange()
    {
        pnlCal.Visible = true;
        ddlCategory.DataSource = (DataSet)fiillcategory();
       
        ddlCategory.DataTextField = "expr";
        ddlCategory.DataValueField = "InventorySubSubId";
        ddlCategory.DataBind();

        //ddlCategory.Items.Insert(0, "--Select--");
        //ddlCategory.Items[0].Value = "0";
    }
    protected void clearall()
    {
        txtRuleName.Text = "";
        ddlWarehouse.SelectedIndex = 0;
      
       // ExistingSalesRate.Text = "0";
        lblRate.Text = "0";
        txtmar.Text = "0";
        txtFlat.Text = "0";
        txtItemWeight.Text = "0";
        txtItemVolume.Text = "0";
        txtOverhead1.Text = "0";
        DropDownList1.SelectedIndex = 0;
        DropDownList2.SelectedIndex = 0;
        DropDownList3.SelectedIndex = 0;
        //ExistRate.Text = "0";
        lblTotalAmt.Text = "0";
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Button3.Visible = false;
        Btn_Submit.Visible = true;
        Btn_Calculate_Click(sender, e);
        String selectStr = "select * from InventoryRuleMaster where Whid = '" + ddlWarehouse.SelectedValue + "' and RuleName='" + txtRuleName.Text + "' and  RuleMasterId<>'" + ViewState["Id"] + "'";
        SqlCommand cmd1 = new SqlCommand(selectStr, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd1);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            Label1.Visible = true;
            Label1.Text = "Record already exist";

        }
        else
        {
            string strupd = "Update  InventoryRuleMaster set RuleName='" + txtRuleName.Text + "',AppliedDate='" + System.DateTime.Now + "',compid='" + Session["Comid"].ToString() + "',Margin='" + txtmar.Text + "',FlatRateAmount='" + txtFlat.Text + "',WeightFactor='" + txtItemWeight.Text + "',WeightUnitId='" + DropDownList1.SelectedValue + "',VolumeFactor='" + txtItemVolume.Text + "',VolumeUnitId='" + DropDownList2.SelectedValue + "',OverHeadFactor='" + txtOverhead1.Text + "',OverHeadUnitId='" + DropDownList3.SelectedValue + "',RateApplied='" + lblTotalAmt.Text + "',InvWMasterId='" + ddlItemName.SelectedValue + "',Whid='" + ddlWarehouse.SelectedValue + "',Highest='" + RadioButtonList2.SelectedValue + "',Lowest='" + RadioButtonList2.SelectedValue + "',Recent='" + RadioButtonList2.SelectedValue + "',Average='" + RadioButtonList2.SelectedValue + "',ExistingSalesRate='" + ExistingSalesRate.Text + "',SelectedRate='" + lblRate.Text + "',InventorySubSubId='" + ddlCategory.SelectedValue + "' where  RuleMasterId='" + ViewState["Id"] + "' ";
            
            SqlCommand cmdupd = new SqlCommand(strupd, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdupd.ExecuteNonQuery();
            con.Close();


            string str12 = " UPDATE InventoryWarehouseMasterTbl SET Rate ='" + Convert.ToDecimal(lblTotalAmt.Text) + "'" +
                    " Where InventoryWarehouseMasterId = '" + ddlItemName.SelectedValue + "'";

            SqlCommand cmd123 = new SqlCommand(str12, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd123.ExecuteNonQuery();
            con.Close();


            lblMsg.Visible = true;
            lblMsg.Text = "Price updated sucessfully";
            Fillgrid();
            clearall();
            pnladd.Visible = false;
            lbllegend.Visible = false;
            btnadd.Visible = true;
            lbllegend.Text = "Add Sales Rate Determination";
 
        }

    }
    protected void mainitemindexchange()
    {
        lblMsg.Text = "";

        if (ddlCategory.SelectedIndex > 0)
        {
            string strs = "SELECT  distinct   InventoryMaster.Name, InventoruSubSubCategory.InventorySubSubId, InventoryMaster.InventoryMasterId, InventoryMaster.ProductNo,  " +
                          " InventoryWarehouseMasterTbl.InventoryWarehouseMasterId, InventoryWarehouseMasterTbl.WareHouseId, WareHouseMaster.Name AS WarehouseName " +
                           " FROM         InventoryMaster INNER JOIN " +
                          " InventoruSubSubCategory ON InventoryMaster.InventorySubSubId = InventoruSubSubCategory.InventorySubSubId INNER JOIN " +
                          " InventoryWarehouseMasterTbl ON InventoryMaster.InventoryMasterId = InventoryWarehouseMasterTbl.InventoryMasterId INNER JOIN " +
                           " WareHouseMaster ON InventoryWarehouseMasterTbl.WareHouseId = WareHouseMaster.WareHouseId left outer join InventoryMasterMNC on  InventoryMasterMNC.Inventorymasterid=InventoryMaster.InventoryMasterId  " +
                           " WHERE InventoruSubSubCategory.InventorySubSubId ='" + ddlCategory.SelectedValue + "' and InventoryWarehouseMasterTbl.WareHouseId='" + ddlWarehouse.SelectedValue + "' and WareHouseMaster.Status='" + 1 + "' ";
            // (InventoruSubSubCategory.InventorySubSubId ='" + Convert.ToInt32(ddlCategory.SelectedValue) + "') " +
            //" and (InventoryWarehouseMasterTbl.WareHouseId='" + Convert.ToInt32(ddlWarehouse.SelectedValue) + "' and InventoryMasterMNC.copid='" + compid + "') ";


            SqlCommand cmd = new SqlCommand(strs, con);
            cmd.Parameters.AddWithValue("@InventorySubSubId", ddlCategory.SelectedValue);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);

            adp.Fill(ds);

            ddlItemName.DataSource = ds;
            ddlItemName.DataTextField = "Name";
            ddlItemName.DataValueField = "InventoryWarehouseMasterId";
            ddlItemName.DataBind();

            ddlItemName.Items.Insert(0, "--Select--");
            ddlItemName.Items[0].Value = "0";
        }
        else
        {

        }
 
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        if (pnladd.Visible == false)
        {
            pnladd.Visible = true;
            lbllegend.Visible = true;
        }
        else
        {
            pnladd.Visible = false;
            lbllegend.Visible = false;
        }
        btnadd.Visible = false;
        lblMsg.Text = "";
    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");

            Button1.Text = "Hide Printable Version";
            Button7.Visible = true;
            if (GridView1.Columns[6].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[6].Visible = false;
            }
            if (GridView1.Columns[7].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                GridView1.Columns[7].Visible = false;
            }

        }
        else
        {

            //pnlgrid.ScrollBars = ScrollBars.Vertical;
            //pnlgrid.Height = new Unit(250);

            Button1.Text = "Printable Version";
            Button7.Visible = false;
            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[6].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                GridView1.Columns[7].Visible = true;
            }

        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        clearall();

        pnladd.Visible = false;
        lbllegend.Visible = false;
        btnadd.Visible = true;
        lbllegend.Text = "Add Sales Rate Determination";
    }
    protected void fillstorefilter()
    {
        ddlfilterbusiness.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlfilterbusiness.DataSource = ds;
        ddlfilterbusiness.DataTextField = "Name";
        ddlfilterbusiness.DataValueField = "WareHouseId";
        ddlfilterbusiness.DataBind();
        ddlfilterbusiness.Items.Insert(0, "All");
        ddlfilterbusiness.Items[0].Value = "0";


       


    }
    protected void ddlfilterbusiness_SelectedIndexChanged(object sender, EventArgs e)
    {
        Fillgrid();
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

    protected void existingsalesrate()
    {
        string Str = "select Rate from InventoryWarehouseMasterTbl where InventoryWarehouseMasterId='" + ddlItemName.SelectedValue + "'";
        SqlCommand cmd = new SqlCommand(Str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        if (ds.Rows.Count > 0)
        {
            ExistingSalesRate.Text = ds.Rows[0]["Rate"].ToString();
            ExistRate.Text = ds.Rows[0]["Rate"].ToString();
        }
    }
}
