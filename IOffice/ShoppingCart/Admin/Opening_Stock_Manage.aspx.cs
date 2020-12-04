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

public partial class ShoppingCart_Admin_Opening_Balance : System.Web.UI.Page
{

    

    SqlConnection con=new SqlConnection(PageConn.connnn);
   
    string compid;
    protected void Page_Load(object sender, EventArgs e)
    {
        //PageConn pgcon = new PageConn();
        //con = pgcon.dynconn;
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();

        compid = Session["Comid"].ToString();

        Page.Title = pg.getPageTitle(page);


        lblerror.Text = "";
        lblerror0.Text = "";

        if (!IsPostBack)
        {
            ViewState["sortOrder"] = "";
            fillstore();
            fillaccountyearofbusiness();

            BtnChange.Visible = true;
            imgsubmitrate.Visible = false;
            if (Request.QueryString["Whid"] != null &&Convert.ToString(Request.QueryString["invma"]) == "inv")
            {
                ddlSearchByStore.SelectedValue = Convert.ToString(Request.QueryString["Whid"]);
            }
            ddlSearchByStore_SelectedIndexChanged(sender, e);
            DataTable dtda = new DataTable();


            dtda = (DataTable)select("select Report_Period_Id,Convert(nvarchar,StartDate,101) as StartDate, Convert(nvarchar,EndDate,101) as EndDate from [ReportPeriod] where Compid = '" + Session["comid"] + "' and Active='1' ");


          //  lbldate.Text = Convert.ToDateTime(dtda.Rows[0]["StartDate"]).ToShortDateString().ToString();
            lblcompname.Text = Session["Cname"].ToString();
            

        }
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
    protected void fillstore()
    {
        string str1 = "select * from WarehouseMaster where comid='" + Session["comid"] + "' and [WareHouseMaster].status = '1' Order by Name Desc";

        DataTable ds1 = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(str1, con);
        da.Fill(ds1);
        if (ds1.Rows.Count > 0)
        {
            ddlSearchByStore.DataSource = ds1;
            ddlSearchByStore.DataTextField = "Name";
            ddlSearchByStore.DataValueField = "WarehouseId";
            ddlSearchByStore.DataBind();

        }


    }

    protected void fillaccountyearofbusiness()
    {
        DataTable dtda = new DataTable();

        dtda = (DataTable)select("select Report_Period_Id,Convert(nvarchar,StartDate,101) as StartDate, Convert(nvarchar,EndDate,101) as EndDate from [ReportPeriod] where Compid = '" + Session["comid"] + "' and Whid='" + ddlSearchByStore.SelectedValue + "' and Active='1' ");


        

        ViewState["CurrentStartdate"] = Convert.ToDateTime(dtda.Rows[0]["StartDate"]).ToShortDateString().ToString();
        ViewState["Currentenddate"] = Convert.ToDateTime(dtda.Rows[0]["EndDate"]).ToShortDateString().ToString();

       

        DataTable dtpreviousyear = new DataTable();

        dtpreviousyear = (DataTable)select(" select Top(1) * from ReportPeriod where (EndDate<>(select Top(1) EndDate from  ReportPeriod where Compid='" + Session["comid"] + "' and Whid='" + ddlSearchByStore.SelectedValue + "'  ) ) and Compid='" + Session["comid"] + "' and Whid='" + ddlSearchByStore.SelectedValue + "' ");
        
        if (dtpreviousyear.Rows.Count > 0)
        {


            ViewState["StartDateofyear"] = Convert.ToDateTime(dtpreviousyear.Rows[0]["StartDate"]).ToShortDateString().ToString();
            ViewState["Enddateofyear"] = Convert.ToDateTime(dtpreviousyear.Rows[0]["EndDate"]).ToShortDateString().ToString();

            lbldate.Text = Convert.ToDateTime(dtpreviousyear.Rows[0]["StartDate"]).ToShortDateString().ToString();
            lblenddate.Text = Convert.ToDateTime(dtpreviousyear.Rows[0]["EndDate"]).ToShortDateString().ToString();

        }

        DateTime dtcurrentstart = Convert.ToDateTime(ViewState["CurrentStartdate"]);
        DateTime dtcurrentend = Convert.ToDateTime(ViewState["Currentenddate"]);

        DateTime dtstartyeardate = Convert.ToDateTime(ViewState["StartDateofyear"]);
        DateTime dtyearenddate = Convert.ToDateTime(ViewState["Enddateofyear"]);

        if (dtcurrentstart == dtstartyeardate && dtcurrentend == dtyearenddate)
        {
            BtnChange.Visible = true;
            Button3.Visible = true;
            Button4.Visible = true;
        }
        else
        {
            BtnChange.Visible = false;
            Button3.Visible = false;
            Button4.Visible = false;
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





    protected void ddlSearchByStore_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        lblcompname.Text = Session["Cname"].ToString();

        fillaccountyearofbusiness();

        lblstore.Text = ddlSearchByStore.SelectedItem.Text.ToString();


        fillgrd();
      
       

    }

    protected void fillgrd()
    {
        DataTable dtda = new DataTable();






        string strservice = "SELECT InventoryWarehouseMasterTbl.InventoryWarehouseMasterId, InventoryWarehouseMasterTbl.Weight,UnitTypeMaster.Name,Convert(nvarchar(50),InventoryWarehouseMasterTbl.Weight )+ ' / ' + UnitTypeMaster.Name as UnitTypeName , WareHouseMaster.WareHouseId,Left(WareHouseMaster.Name,30) as wname ,  InventoryMaster.InventoryMasterId, InventoryDetails.Inventory_Details_Id, InventoruSubSubCategory.InventorySubSubId,      InventoruSubSubCategory.InventorySubSubName, InventorySubCategoryMaster.InventorySubCatId, InventorySubCategoryMaster.InventorySubCatName,       InventoryCategoryMaster.InventeroyCatId, InventoryCategoryMaster.InventoryCatName, Left(InventoryMaster.Name,30) as InventoryMasterName, InventoryMaster.MasterActiveStatus,case when (InventoryMaster.MasterActiveStatus='1') then 'Active' else 'Inactive' End as Statuslabel,      InventoryMaster.ProductNo,       Left(InventoryCategoryMaster.InventoryCatName,15) + ' : ' + Left(InventorySubCategoryMaster.InventorySubCatName,15) + ' :  ' + Left(InventoruSubSubCategory.InventorySubSubName,15)      AS CateAndName, InventoryBarcodeMaster.Barcode, InventoryDetails.Description, InventoryImgMaster.Newarrival, InventoryImgMaster.Promotion,       InventoryImgMaster.FutureProduct  FROM         InventoryMaster LEFT OUTER JOIN    InventoryImgMaster ON InventoryMaster.InventoryMasterId = InventoryImgMaster.InventoryMasterId LEFT OUTER JOIN      InventoryDetails ON InventoryMaster.InventoryDetailsId = InventoryDetails.Inventory_Details_Id    LEFT OUTER JOIN      InventoryBarcodeMaster ON InventoryMaster.InventoryMasterId = InventoryBarcodeMaster.InventoryMaster_id LEFT OUTER JOIN      InventorySubCategoryMaster LEFT OUTER JOIN      InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId RIGHT OUTER JOIN      InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID ON InventoryMaster.InventorySubSubId = InventoruSubSubCategory.InventorySubSubId Left Outer Join InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.InventoryMasterId=InventoryMaster.InventoryMasterId inner join WareHouseMaster on WareHouseMaster.WareHouseId=InventoryWarehouseMasterTbl.WareHouseId  inner join UnitTypeMaster on UnitTypeMaster.UnitTypeId = InventoryDetails.UnitTypeId where  " + "  InventoryCategoryMaster.compid='" + Session["comid"] + "' and  InventoryWarehouseMasterTbl.WareHouseId='" + ddlSearchByStore.SelectedValue + "' and  InventoryMaster.CatType IS NULL  and InventoryCategoryMaster.CatType IS NULL order by WareHouseMaster.Name,CateAndName,InventoryMaster.Name,UnitTypeName ";

        SqlCommand cmdservice = new SqlCommand(strservice, con);
        SqlDataAdapter adpservice = new SqlDataAdapter(cmdservice);
        DataTable dsservice = new DataTable();
        adpservice.Fill(dsservice);

        if (dsservice.Rows.Count > 0)
        {
            grdservicestore.DataSource = dsservice;

            DataView myDataView = new DataView();
            myDataView = dsservice.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }

           
            grdservicestore.DataBind();
        }
        else
        {
            grdservicestore.DataSource = null;
            grdservicestore.DataBind();
        }
        foreach (GridViewRow gdr in grdservicestore.Rows)
        {
            TextBox txtopeningqty = (TextBox)gdr.FindControl("txtopeningqty");
            TextBox txtopeningrate123 = (TextBox)gdr.FindControl("txtopeningrate123");
            Label lblinvwhmasterid = (Label)gdr.FindControl("lblinvwhmasterid");

            Label lblopqty = (Label)gdr.FindControl("lblopqty");
            Label lbloprate = (Label)gdr.FindControl("lbloprate");


            string stropening = "select * from InventoryWarehouseMasterAvgCostTbl   where  InventoryWarehouseMasterAvgCostTbl.InvWMasterId='" + lblinvwhmasterid.Text + "' and Tranction_Master_Id  In('00000','--') and InventoryWarehouseMasterAvgCostTbl.DateUpdated between '" + ViewState["StartDateofyear"] + "' and '" + ViewState["Enddateofyear"] + "' order by IWMAvgCostId  ";
            SqlCommand cmdopening = new SqlCommand(stropening, con);
            SqlDataAdapter adpopening = new SqlDataAdapter(cmdopening);
            DataTable dsopening = new DataTable();
            adpopening.Fill(dsopening);


            if (dsopening.Rows.Count > 0)
            {

                txtopeningqty.Text = dsopening.Rows[0]["Qty"].ToString();
                txtopeningrate123.Text = dsopening.Rows[0]["Rate"].ToString();
                lblopqty.Text = dsopening.Rows[0]["Qty"].ToString();
                lbloprate.Text = dsopening.Rows[0]["Rate"].ToString();
            }
            else
            {
                txtopeningqty.Text = "0";
                txtopeningrate123.Text = "0";
                lblopqty.Text = "0";
                lbloprate.Text = "0";
            }
        }
    }

    //protected void imgsubmitrate_Click(object sender, EventArgs e)
    //{
    //    double total = 0;
    //    double invpri = 0;
    //    double invbal = 0;

    //        int i = Convert.ToInt32(ViewState["InvMid123"]);
    //        ViewState["CSSCNm"] = i;


    //        foreach (GridViewRow gdr in grdservicestore.Rows)
    //        {


    //            Label storename = (Label)gdr.FindControl("lblstoreid");
    //            CheckBox chkActive = ((CheckBox)gdr.FindControl("chkinvMasterStatus"));


    //            Label lblinvMId = (Label)gdr.FindControl("lblInvMasterId");
    //            ViewState["InvMid123"] = Convert.ToInt32(lblinvMId.Text);

    //            Label lbldeteilid = (Label)gdr.FindControl("lbldeteilid");
    //            ViewState["invDi"] = Convert.ToInt32(lbldeteilid.Text);


    //            TextBox txtopeningqty = (TextBox)gdr.FindControl("txtopeningqty");
    //            txtopeningqty.Enabled = false;

    //            TextBox txtopeningrate123 = (TextBox)gdr.FindControl("txtopeningrate123");
    //            txtopeningrate123.Enabled = false;


    //            total = Convert.ToDouble(txtopeningqty.Text) * Convert.ToDouble(txtopeningrate123.Text);



    //            string strwhi = " select QtyOnHand, OpeningQty, OpeningRate,Total, InventoryWarehouseMasterId from InventoryWarehouseMasterTbl where InventoryMasterId='" + Convert.ToInt32(ViewState["InvMid123"]).ToString() + "' and WareHouseId='" + ddlSearchByStore.SelectedValue + "'";
    //            SqlCommand cmdwhi = new SqlCommand(strwhi, con);
    //            SqlDataAdapter dawhi = new SqlDataAdapter(cmdwhi);
    //            DataTable dtwhi = new DataTable();
    //            dawhi.Fill(dtwhi);
    //            if(dtwhi.Rows.Count>0)
    //            {
    //                decimal oldQtyonHand = 0;
    //                decimal oldOpqty = 0;
    //                decimal oldOpRt = 0;

    //                if (dtwhi.Rows[0]["QtyOnHand"].ToString() != "")
    //                {
    //                     oldQtyonHand = Convert.ToDecimal(dtwhi.Rows[0]["QtyOnHand"]);

    //                }

    //                if (dtwhi.Rows[0]["OpeningQty"].ToString() != "")
    //                {
    //                     oldOpqty = Convert.ToDecimal(dtwhi.Rows[0]["OpeningQty"]);   

    //                }
    //                if (dtwhi.Rows[0]["OpeningRate"].ToString() != "")
    //                {
    //                     oldOpRt = Convert.ToDecimal(dtwhi.Rows[0]["OpeningRate"]);

    //                }
    //                if (dtwhi.Rows[0]["Total"].ToString() != "")
    //                {
    //                    invpri = Convert.ToDouble(dtwhi.Rows[0]["Total"]);
    //                }
    //                string invwhid = dtwhi.Rows[0]["InventoryWarehouseMasterId"].ToString();





    //            decimal newOpqty = Convert.ToDecimal(txtopeningqty.Text);
    //            decimal newOpRt = Convert.ToDecimal(txtopeningrate123.Text);
    //            decimal newQtyonHand = (oldQtyonHand + newOpqty) - oldOpqty;
    //                invbal = total;


    //                DataTable dtdaopb = new DataTable();

    //                dtdaopb = (DataTable)select("select Report_Period_Id from [ReportPeriod] where ReportPeriod.Report_Period_Id<( select Report_Period_Id from [ReportPeriod] where Whid='" + ddlSearchByStore.SelectedValue + "' and Active='1') and  Whid='" + ddlSearchByStore.SelectedValue + "' order by Report_Period_Id Desc");

    //                    DataTable dtdaopb1 = new DataTable();
    //                    dtdaopb1 = (DataTable)select("select AccountBalance.Account_Balance_Id,AccountBalance.Balance from ReportPeriod inner join AccountBalance on AccountBalance.Report_Period_Id= ReportPeriod.Report_Period_Id inner join AccountMaster on AccountMaster.Id=AccountBalance.AccountMasterId where ReportPeriod.Report_Period_Id='" + dtdaopb.Rows[0]["Report_Period_Id"] + "' and AccountMaster.AccountId='8000' and ReportPeriod.Whid='" + ddlSearchByStore.SelectedValue + "'");
    //                    double balan = 0;
    //                    if ((oldOpqty != newOpqty) || (oldOpRt != newOpRt))
    //                    //if ((oldOpqty != newOpqty) && (oldOpRt != newOpRt))
    //                    {
    //                        invpri = (invbal) - Convert.ToDouble((oldOpRt * oldOpqty));
    //                        balan = (Convert.ToDouble(dtdaopb1.Rows[0]["Balance"]) + invpri);


    //                        string insertintoaccount = "update AccountBalance set Balance=" + balan + " where Account_Balance_Id='" + dtdaopb1.Rows[0]["Account_Balance_Id"] + "'";
    //                        SqlCommand cmbau = new SqlCommand(insertintoaccount, con);
    //                        if (con.State.ToString() != "Open")
    //                        {
    //                            con.Open();
    //                        }
    //                        cmbau.ExecuteNonQuery();
    //                        con.Close();


    //                        string str11 = "update InventoryWarehouseMasterAvgCostTbl set Qty ='" + txtopeningqty.Text + "',Rate='" + txtopeningrate123.Text + "' where InvWMasterId='" + invwhid + "' AND  Tranction_Master_Id='00000' ";
    //                        SqlCommand cmdins = new SqlCommand(str11, con);
    //                        if (con.State.ToString() != "Open")
    //                        {
    //                            con.Open();
    //                        }
    //                        cmdins.ExecuteNonQuery();
    //                        con.Close();


    //                        string invupdate = " Update InventoryWarehouseMasterTbl set QtyOnHand=" + newQtyonHand + ", OpeningQty='" + txtopeningqty.Text + "',OpeningRate='" + txtopeningrate123.Text + "',Active='" + chkActive.Checked + "',Total='" + total + "' where InventoryWarehouseMasterId='" + invwhid + "' ";
    //                        SqlCommand cmdinvupdate = new SqlCommand(invupdate, con);
    //                        if (con.State.ToString() != "Open")
    //                        {
    //                            con.Open();
    //                        }
    //                        cmdinvupdate.ExecuteNonQuery();
    //                        con.Close();

    //                        lblerror.Visible = true;
    //                        lblerror.Text = "Inventory record updated successfully";




    //                    }
    //        }


    //        }
    //        BtnChange.Visible = true;
    //        imgsubmitrate.Visible = false;
    //        fillgrd();
    //    //}
    //    //catch(Exception ex)
    //    //{
    //    //    lblerror.Visible = true;
    //    //    lblerror.Text = ex.ToString();

    //    //}
    //}

    protected void imgsubmitrate_Click(object sender, EventArgs e)
    {
        int flag = 0;
        int abcd = 0;
        decimal accamt = 0;
        string invty = "";
        foreach (GridViewRow gdr in grdservicestore.Rows)
        {
            TextBox txtopeningqty = (TextBox)gdr.FindControl("txtopeningqty");
            TextBox txtopeningrate123 = (TextBox)gdr.FindControl("txtopeningrate123");
            Label lblinvwhmasterid = (Label)gdr.FindControl("lblinvwhmasterid");
            Label lblInvName = (Label)gdr.FindControl("lblInvName");
            Label lblopqty = (Label)gdr.FindControl("lblopqty");
            Label lbloprate = (Label)gdr.FindControl("lbloprate");

            if ((Convert.ToDecimal(lblopqty.Text) != Convert.ToDecimal(txtopeningqty.Text)) || Convert.ToDecimal(lbloprate.Text) != Convert.ToDecimal(txtopeningrate123.Text))
            {
                if ((Convert.ToDecimal(lblopqty.Text) >Convert.ToDecimal(txtopeningqty.Text)))
                {
                    abcd = FillCountQty(lblinvwhmasterid.Text.ToString(), txtopeningqty.Text.ToString());
                    if (abcd == 1)
                    {
                        flag = 1;
                        if (invty.Length > 0)
                        {
                            invty = "," + lblInvName.Text;
                        }
                        else
                        {
                            invty = lblInvName.Text;
                        }

                    }
                }
                else
                {
                    abcd = 0;
                }
                if (abcd == 0)
                {
                    string stropening = "select * from InventoryWarehouseMasterAvgCostTbl   where  InventoryWarehouseMasterAvgCostTbl.InvWMasterId='" + lblinvwhmasterid.Text + "' and Tranction_Master_Id  In('00000','--') and InventoryWarehouseMasterAvgCostTbl.DateUpdated Between '" + ViewState["StartDateofyear"] + "' and  '" + ViewState["Enddateofyear"] + "' ";
                    SqlCommand cmdopening = new SqlCommand(stropening, con);
                    SqlDataAdapter adpopening = new SqlDataAdapter(cmdopening);
                    DataTable dsopening = new DataTable();
                    adpopening.Fill(dsopening);

                    if (dsopening.Rows.Count > 0)
                    {
                        string str11 = "update InventoryWarehouseMasterAvgCostTbl set Qty ='" + txtopeningqty.Text + "',Rate='" + txtopeningrate123.Text + "',AvgCost='" + txtopeningrate123.Text + "',QtyonHand='" + txtopeningqty.Text + "',DateUpdated='" + ViewState["StartDateofyear"] + "' where IWMAvgCostId='" + dsopening.Rows[0]["IWMAvgCostId"] + "' ";
                        SqlCommand cmdins = new SqlCommand(str11, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmdins.ExecuteNonQuery();
                        con.Close();

                    }
                    else
                    {
                        string aFD = "DELETE from InventoryWarehouseMasterAvgCostTbl where  InventoryWarehouseMasterAvgCostTbl.InvWMasterId='" + lblinvwhmasterid.Text + "' and Tranction_Master_Id  In('00000','--')";
                        SqlCommand CMSA = new SqlCommand(aFD, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        CMSA.ExecuteNonQuery();
                        con.Close();
                        string str = "00000";

                        string str11 = "Insert into  InventoryWarehouseMasterAvgCostTbl(InvWMasterId,AvgCost,DateUpdated,Qty,Rate,Tranction_Master_Id,QtyonHand) values ('" + lblinvwhmasterid.Text + "','" + txtopeningrate123.Text + "','" + ViewState["StartDateofyear"] + "','" + txtopeningqty.Text + "','" + txtopeningrate123.Text + "','" + str + "','" + txtopeningqty.Text + "') ";
                        SqlCommand cmdins = new SqlCommand(str11, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmdins.ExecuteNonQuery();
                        con.Close();

                    }
                    fillAVGCOST(lblinvwhmasterid.Text.ToString(), Convert.ToDecimal(txtopeningrate123.Text), txtopeningqty.Text.ToString());
                    accamt += Convert.ToDecimal(txtopeningqty.Text) * Convert.ToDecimal(txtopeningrate123.Text);
                }
                else
                {
                    accamt += Convert.ToDecimal(lbloprate.Text) * Convert.ToDecimal(lblopqty.Text);
                }
            }
            else
            {
                accamt += Convert.ToDecimal(lbloprate.Text) * Convert.ToDecimal(lblopqty.Text);

            }
        }
        if (flag == 1)
        {
            lblerror.Text = "You cannot edit inventory(" + invty + ") quantity as it may result into negative quantity in this item because of sale entry made after the change of this opening quantity";

        }
        if (Request.QueryString["rp"] != null && Request.QueryString["Ad"] != null)
        {
            accamt = Math.Round(accamt, 2);
            String AERF = "update AccountBalance set Balance='" + accamt + "' where [Report_Period_Id]='" + Request.QueryString["rp"] + "' and [AccountMasterId]=" + Request.QueryString["Ad"] + "";
            SqlCommand cmd11a = new SqlCommand(AERF, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd11a.ExecuteNonQuery();
            con.Close();
            if (flag == 0)
            {
                Response.Redirect("Opening_Balance.aspx?Whid=" + ddlSearchByStore.SelectedValue);
            }
            //}
        }
        if (flag == 0)
        {
            foreach (GridViewRow gdr in grdservicestore.Rows)
            {
                TextBox txtopeningqty = (TextBox)(gdr.FindControl("txtopeningqty"));
                txtopeningqty.Enabled = false;

                TextBox txtopeningrate123 = (TextBox)(gdr.FindControl("txtopeningrate123"));
                txtopeningrate123.Enabled = false;
            }
            imgsubmitrate.Visible = false;
            BtnChange.Visible = true;
            btncancel.Visible = false;
            fillgrd();
            lblerror.Visible = true;
            lblerror.Text = "Inventory record updated successfully";
        }


       

    }
    protected void fillAVGCOST(string invid, decimal invrate, string recqty)
    {
        string id12 = invid;
      
        decimal Finalqtyhand = 0;

        DataTable Dataupval = select("SELECT  QtyonHand,Rate,AvgCost,Qty,Tranction_Master_Id,IWMAvgCostId,DateUpdated FROM  InventoryWarehouseMasterAvgCostTbl where DateUpdated >= '" + ViewState["StartDateofyear"] + "' and InvWMasterId='" + id12 + "'  and Tranction_Master_Id not in('00000') order by DateUpdated Asc,Tranction_Master_Id Asc,IWMAvgCostId Asc");
        decimal changeTotalavgcost = invrate;
        decimal changeTotalonhand = Convert.ToDecimal(recqty);
        
        foreach (DataRow itm in Dataupval.Rows)
        {
            string gupd = "";
            string gupd1 = "";
            string manul = "";
         
                if (Convert.ToString(itm["Rate"]) == "" && Convert.ToDecimal(itm["Qty"]) < 0)
                {
                    decimal newamt = 0;
                    DataTable drsv = select("Select AmountDebit from Tranction_Details where AccountDebit='8003' and Tranction_Master_Id='" + itm["Tranction_Master_Id"] + "' ");
                    if (drsv.Rows.Count > 0)
                    {
                        newamt = Convert.ToDecimal(drsv.Rows[0]["AmountDebit"]);
                    }

                    changeTotalavgcost = Math.Round(changeTotalavgcost, 2);
                    changeTotalonhand = changeTotalonhand + Convert.ToDecimal(itm["Qty"]);
                    Finalqtyhand = changeTotalonhand;
                    decimal appn = (changeTotalavgcost) * ((-1) * Convert.ToDecimal(itm["Qty"]));
                    decimal appold = Convert.ToDecimal(itm["AvgCost"]) * ((-1) * Convert.ToDecimal(itm["Qty"]));
                    newamt = newamt + appn - (appold);
                    newamt = Math.Round(newamt, 2);

                    gupd = "Update Tranction_Details Set AmountDebit='" + newamt + "' where AccountDebit='8003' and Tranction_Master_Id='" + itm["Tranction_Master_Id"] + "'";
                    gupd1 = "Update Tranction_Details Set AmountCredit='" + newamt + "' where AccountCredit='8000' and Tranction_Master_Id='" + itm["Tranction_Master_Id"] + "'";
                    manul = "Update InventoryWarehouseMasterAvgCostTbl Set QtyonHand='" + changeTotalonhand + "',AvgCost='" + changeTotalavgcost + "' where IWMAvgCostId='" + itm["IWMAvgCostId"] + "'";
                    SqlCommand cmdupcugs = new SqlCommand(gupd, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdupcugs.ExecuteNonQuery();
                    con.Close();
                    SqlCommand cmdupcugsin = new SqlCommand(gupd1, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdupcugsin.ExecuteNonQuery();
                    con.Close();

                }
                else if (Convert.ToString(itm["Tranction_Master_Id"]) != "")
                {
                    Finalqtyhand = changeTotalonhand + Convert.ToDecimal(itm["Qty"]);
                    if (Finalqtyhand > 0)
                    { 
                        
                        changeTotalavgcost = ((changeTotalavgcost * changeTotalonhand) + (Convert.ToDecimal(itm["Qty"]) * Convert.ToDecimal(itm["Rate"]))) / Finalqtyhand;
                    }
                    changeTotalonhand = changeTotalonhand + Convert.ToDecimal(itm["Qty"]);
                    changeTotalavgcost = Math.Round(changeTotalavgcost, 2);
                    manul = "Update InventoryWarehouseMasterAvgCostTbl Set QtyonHand='" + changeTotalonhand + "',AvgCost='" + changeTotalavgcost + "' where IWMAvgCostId='" + itm["IWMAvgCostId"] + "'";

                }
                if (manul.Length > 0)
                {
                    SqlCommand cmdupinv = new SqlCommand(manul, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdupinv.ExecuteNonQuery();
                    con.Close();
                }
        
        }

    }

    protected int FillCountQty(string invid, string recqty)
    {
        int  Anr=0;
        string id12 = invid;

        decimal Finalqtyhand = 0;

        Finalqtyhand = Convert.ToDecimal(recqty);

            DataTable Dataupval = select("SELECT  QtyonHand,Rate,AvgCost,Qty,Tranction_Master_Id,IWMAvgCostId,DateUpdated FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + id12 + "' and DateUpdated >= '" + ViewState["StartDateofyear"] + "' and Tranction_Master_Id not in('00000') order by DateUpdated Asc,Tranction_Master_Id Asc,IWMAvgCostId Asc");
        foreach (DataRow itm in Dataupval.Rows)
        {
            Finalqtyhand = Finalqtyhand + Convert.ToDecimal(itm["Qty"]);
            if (Finalqtyhand < 0)
            {
                Anr = 1;
                break;
            }
        }
        return Anr;
    }
    protected void BtnChange_Click(object sender, EventArgs e)
    {
        imgsubmitrate.Visible = true;
        BtnChange.Visible = false;
        btncancel.Visible = true;



        foreach (GridViewRow gdr in grdservicestore.Rows)
        {
            TextBox txtopeningqty = (TextBox)(gdr.FindControl("txtopeningqty"));
            txtopeningqty.Enabled = true;

            TextBox txtopeningrate123 = (TextBox)(gdr.FindControl("txtopeningrate123"));
            txtopeningrate123.Enabled = true;
        }
    }
    protected void grdservicestore_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        fillgrd();
    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");

            Button1.Text = "Hide Printable Version";
            Button2.Visible = true;
          

        }
        else
        {


            Button1.Text = "Printable Version";
            Button2.Visible = false;
           

        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        string te = "InventoryMasterAdd.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        fillgrd();
        foreach (GridViewRow gdr in grdservicestore.Rows)
        {
            TextBox txtopeningqty = (TextBox)(gdr.FindControl("txtopeningqty"));
            txtopeningqty.Enabled = false;

            TextBox txtopeningrate123 = (TextBox)(gdr.FindControl("txtopeningrate123"));
            txtopeningrate123.Enabled = false;
        }
        imgsubmitrate.Visible = false;
        BtnChange.Visible = true;
        btncancel.Visible = false;
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        fillgrd();
    }
    protected void btnGoback_Click(object sender, EventArgs e)
    {
        Response.Redirect("Opening_Balance.aspx?Whid=" + ddlSearchByStore.SelectedValue);
    }
}
