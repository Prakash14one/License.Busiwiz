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

public partial class ShoppingCart_Admin_FreeShippingRuleAddManage : System.Web.UI.Page
{
    SqlConnection con=new SqlConnection(PageConn.connnn);
    string compid;
    //GridView grdFreeRule = new GridView();


    protected void Page_Load(object sender, EventArgs e)
    {
        //PageConn pgcon = new PageConn();
        //con = pgcon.dynconn;
        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        }

        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();
        compid = Session["Comid"].ToString();

        Page.Title = pg.getPageTitle(page);

        Lblmsg.Visible = false;
        if (!IsPostBack)
        {
            Pagecontrol.dypcontrol(Page, page);
            ViewState["sortOrder"] = "";
            lblCompany.Text =Convert.ToString(Session["Cname"]);
            fillshipname();
           
            object sender1234 = new object();
            EventArgs e1234 = new EventArgs();
            ddlWarehouse_SelectedIndexChanged(sender1234, e1234);
            fillGrid();
            FillddlFooterCoutry();
            
            
        }

    }
    protected void fillshipname()
    {
        ddlWarehouse.Items.Clear();
        SqlCommand cmd = new SqlCommand("SELECT  distinct WarehouseMaster.Name +':'+FreeShippingRule.Name as Name, Id FROM  FreeShippingRule inner join WarehouseMaster on WarehouseMaster.WarehouseId=FreeShippingRule.Whid where WarehouseMaster.Comid='" + Session["comid"] + "' and Status=1 order by Name", con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ddt = new DataTable();
        adp.Fill(ddt);
        ddlWarehouse.DataSource = ddt;
        ddlWarehouse.DataTextField = "Name";
        ddlWarehouse.DataValueField = "Id";
        ddlWarehouse.DataBind();
        ddlWarehouse.Items.Insert(0, "Select");
        ddlWarehouse.Items[0].Value = "0";

    }
    protected void FillddlFooterCoutry()
    {
        GridViewRow ft = (GridViewRow)grdFreeRule.FooterRow;
        DropDownList ddlwareh = (DropDownList)ft.FindControl("ddlware");
        DropDownList ddlcnt = (DropDownList)ft.FindControl("ddlGrdCountry");
        DropDownList ddlstt = (DropDownList)grdFreeRule.FooterRow.FindControl("ddlGrdState");
        DropDownList ddlcty = (DropDownList)grdFreeRule.FooterRow.FindControl("ddlGrdCity");

     
     
        DataTable ds = ClsStore.SelectStorename();
        ddlwareh.DataSource = ds;
        ddlwareh.DataTextField = "Name";
        ddlwareh.DataValueField = "WareHouseId";
        ddlwareh.DataBind();


        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlwareh.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }
           


        SqlCommand cmd = new SqlCommand("SELECT  distinct CountryId, CountryName FROM  CountryMaster", con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ddt = new DataTable();
        adp.Fill(ddt);
        ddlcnt.DataSource = ddt;
        ddlcnt.DataTextField = "CountryName";
        ddlcnt.DataValueField = "CountryId";
        ddlcnt.DataBind();
        
        ddlcnt.Items.Insert(0, "-Select-");
        ddlcnt.Items[0].Value = "0";
        ddlstt.Items.Clear();
        ddlstt.Items.Insert(0, "All");
        ddlstt.Items[0].Value = "0";
        ddlcty.Items.Clear();
        ddlcty.Items.Insert(0, "All");
        ddlcty.Items[0].Value = "0";
        //ddlcnt.SelectedIndex = ddlcntr.Items.IndexOf(ddlcntr.Items.FindByValue(lblcntrid.Text));

    }

    protected void fillGrid()
    {
        //string strFillGrid = "SELECT     FreeShippingRule.Id AS FreeShipingId, FreeShippingRule.Name AS FreeShipingName, "+
        //    " FreeShippingRule.MinOrdSize, FreeShippingRule.MaxOrderSize, " +
        //             " CityMasterTbl.CityId, CityMasterTbl.CityName, StateMasterTbl.StateName, "+
        //             " StateMasterTbl.StateId, StateMasterTbl.State_Code, CountryMaster.CountryId,  " +
        //             " CountryMaster.CountryName, CountryMaster.Country_Code " +
        //             " FROM         CityMasterTbl INNER JOIN " +
        //             " FreeShippingRule ON CityMasterTbl.CityId = FreeShippingRule.CityId INNER JOIN " +
        //             " StateMasterTbl INNER JOIN " +
        //             " CountryMaster ON StateMasterTbl.CountryId = CountryMaster.CountryId ON CityMasterTbl.StateId = StateMasterTbl.StateId  ";
        string strFillGrid = " SELECT   distinct  FreeShippingRule.Id AS FreeShipingId, FreeShippingRule.Name AS FreeShipingName, FreeShippingRule.MinOrdSize, FreeShippingRule.MaxOrderSize,  " +
                     "   CityMasterTbl.CityId, CityMasterTbl.CityName, StateMasterTbl.StateName, StateMasterTbl.StateId, StateMasterTbl.State_Code, CountryMaster.CountryId,  " +
                     "   CountryMaster.CountryName,WareHouseMaster.Name, CountryMaster.Country_Code " +
                     " FROM         CityMasterTbl RIGHT OUTER JOIN " +
                     "  StateMasterTbl RIGHT OUTER JOIN " +
                     "  CountryMaster RIGHT OUTER JOIN " +
                     "  FreeShippingRule ON CountryMaster.CountryId = FreeShippingRule.CountryId ON StateMasterTbl.StateId = FreeShippingRule.StateId ON  " +
                       " CityMasterTbl.CityId = FreeShippingRule.CityId inner Join WareHouseMaster on WareHouseMaster.WareHouseId=FreeShippingRule.Whid where  FreeShippingRule.compid='" + compid + "'";


        SqlCommand cmdFillGrid = new SqlCommand(strFillGrid, con);
        SqlDataAdapter adpFillGrid = new SqlDataAdapter(cmdFillGrid);
        DataTable dtFillGrid = new DataTable();
        adpFillGrid.Fill(dtFillGrid);

        if (dtFillGrid.Rows.Count > 0)
        {
            grdFreeRule.DataSource = dtFillGrid;
            grdFreeRule.DataBind();

            FillddlFooterCoutry();

            foreach (GridViewRow bnm in grdFreeRule.Rows)
            {
                Label cid = (Label)bnm.FindControl("lblCountryId");
                Label sid = (Label)bnm.FindControl("lblStateId");
                Label ctid = (Label)bnm.FindControl("lblCityId");
                Label cidnm = (Label)bnm.FindControl("lblCountry");
                Label sidnm = (Label)bnm.FindControl("lblstate");
                Label ctidnm = (Label)bnm.FindControl("lblcity");



                if (ctid.Text == "" || ctid.Text == null || ctid.Text == "0")
                {
                    ctidnm.Text = "All";
                }
                else
                {

                }
                if (sid.Text == "" || sid.Text == null || sid.Text == "0")
                {
                    sidnm.Text = "All";
                }
                else
                {

                }
            }
        }
        else
        {
            ShowNoResultFound(dtFillGrid, grdFreeRule);
        }
    }
    private void ShowNoResultFound(DataTable source, GridView gv)
    {

        DataRow add = source.NewRow();
        add["FreeShipingId"] = "0";
        add["FreeShipingName"] = "0";
        add["MinOrdSize"] = "0";
        add["MaxOrderSize"] = "0";
        add["CityId"] = "0";
        add["CityName"] = "0";
        add["StateName"] = "0";
        add["StateId"] = "0";
        add["State_Code"] = "0";
        add["CountryId"] = "0";
        add["CountryName"] = "0";
        add["Country_Code"] = "0";


        source.Rows.Add(add); // create a new blank row to the DataTable
        // Bind the DataTable which contain a blank row to the GridView
        gv.DataSource = source;
        gv.DataBind();
       
        // Get the total number of columns in the GridView to know what the Column Span should be
        //int columnsCount = gv.Columns.Count;
        //gv.Rows[0].Cells.Clear();// clear all the cells in the row
        //gv.Rows[0].Cells.Add(new TableCell()); //add a new blank cell
        //gv.Rows[0].Cells[0].ColumnSpan = columnsCount; //set the column span to the new added cell

        ////You can set the styles here
        //gv.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;

        //gv.Rows[0].Cells[0].Text = "NO RESULT FOUND!";
        FillddlFooterCoutry();
       //if( gv.Rows.Count>0)
       // {
       //     gv.Rows[0].Cells[7].Visible = false;
       //   ImageButton imgbt=(ImageButton)  gv.Rows[0].FindControl("Btndele") ;
       //   imgbt.Visible = false;
       //   imgbt.ImageUrl = "";
       // }
        gv.Rows[0].Cells.Clear();
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        //Calendar1.Visible = true;
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1222222.Hide();
        //Calendar2.Visible = true;

    }

    public void clear()
    {


    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {




        int vvv = 0;
        EventArgs e123 = new EventArgs();
        foreach (GridViewRow gdrdd in grdCat.Rows)
        {
            Label sd1 = (Label)gdrdd.FindControl("lblStartDate");
            Label ed1 = (Label)gdrdd.FindControl("lblEndDate");
            Label ma1 = (Label)gdrdd.FindControl("lblminAmt");
            Label mq1 = (Label)gdrdd.FindControl("lblMinQty");


            CheckBox chkaply = (CheckBox)gdrdd.FindControl("chkApply");
            if (chkaply.Checked == true)
            {
                vvv += 1;
                if (grdCat.Rows.Count > 0)
                {
                    DataTable dtinvwh = new DataTable();

                    Label id = (Label)gdrdd.FindControl("lblId");
                    if (Convert.ToString( ViewState["catid"]) == "1")
                    {
                        string subids = "SELECT     InventorySubCategoryMaster.InventorySubCatName, InventoryCategoryMaster.InventoryCatName, InventoruSubSubCategory.InventorySubSubId as Id,  " +
                           "   InventoruSubSubCategory.InventorySubSubName, InventoryCategoryMaster.InventoryCatName+' : '+InventorySubCategoryMaster.InventorySubCatName+' : '+InventoruSubSubCategory.InventorySubSubName AS Name " +
                           " FROM InventoryCategoryMaster inner join InventorySubCategoryMaster on InventorySubCategoryMaster.InventoryCategoryMasterId=InventoryCategoryMaster.InventeroyCatId inner join InventoruSubSubCategory on InventoruSubSubCategory.InventorySubCatID=InventorySubCategoryMaster.InventorySubCatId inner join InventoryMaster on InventoryMaster.InventorySubSubId=InventoruSubSubCategory.InventorySubSubId inner join InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.InventoryMasterId=InventoryMaster.InventoryMasterId inner join WareHouseMaster on WareHouseMaster.WareHouseId=InventoryWarehouseMasterTbl.WareHouseId  WHERE InventoryWarehouseMasterTbl.WareHouseId ='" + ViewState["Whid"] + "' and InventoryCategoryMaster.compid='" + Session["comid"] + "' and InventoryCategoryMaster.InventeroyCatId = '" + id.Text + "' order by Name";

                        SqlCommand cmdids = new SqlCommand(subids, con);
                        SqlDataAdapter adpids = new SqlDataAdapter(cmdids);
                        DataTable dtids = new DataTable();
                        adpids.Fill(dtids);
                        string strInvBySerchIdinvname = "";
                        if (dtids.Rows.Count > 0)
                        {
                            string strIdinvname = "";
                            string strInvAllIdsinvname = "";
                            string strtempinvname = "";


                            if (dtids.Rows.Count > 0)
                            {

                                foreach (DataRow dtrrr in dtids.Rows)
                                {
                                    strIdinvname = dtrrr["Id"].ToString();
                                    strInvAllIdsinvname = strIdinvname + "," + strInvAllIdsinvname;
                                    strtempinvname = strInvAllIdsinvname.Substring(0, (strInvAllIdsinvname.Length - 1));
                                }

                                strInvBySerchIdinvname = " and InventoruSubSubCategory.InventorySubSubId in (" + strtempinvname + ") ";
                                //string mainstring = strinv + "  order by SalesChallanMaster.RefSalesOrderId ";
                            }


                        }
                        if (strInvBySerchIdinvname != "")
                        {
                            string main = (string)(ViewState["mainstringforinvWHMid"].ToString()) + strInvBySerchIdinvname;
                            SqlCommand cmdinvwhid = new SqlCommand(main, con);
                            SqlDataAdapter adpinvwhid = new SqlDataAdapter(cmdinvwhid);
                            adpinvwhid.Fill(dtinvwh);


                        }


                    }
                    else if (Convert.ToString(ViewState["catid"]) == "2")
                    {
                        string subids = "SELECT     InventorySubCategoryMaster.InventorySubCatName, InventoryCategoryMaster.InventoryCatName, InventoruSubSubCategory.InventorySubSubId as Id,  " +
                           "   InventoruSubSubCategory.InventorySubSubName, InventoryCategoryMaster.InventoryCatName+' : '+InventorySubCategoryMaster.InventorySubCatName+' : '+InventoruSubSubCategory.InventorySubSubName AS Name " +
                           " FROM       InventoryCategoryMaster inner join InventorySubCategoryMaster on InventorySubCategoryMaster.InventoryCategoryMasterId=InventoryCategoryMaster.InventeroyCatId inner join InventoruSubSubCategory on InventoruSubSubCategory.InventorySubCatID=InventorySubCategoryMaster.InventorySubCatId inner join InventoryMaster on InventoryMaster.InventorySubSubId=InventoruSubSubCategory.InventorySubSubId inner join InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.InventoryMasterId=InventoryMaster.InventoryMasterId inner join WareHouseMaster on WareHouseMaster.WareHouseId=InventoryWarehouseMasterTbl.WareHouseId  WHERE InventoryWarehouseMasterTbl.WareHouseId ='" + ViewState["Whid"] + "' and InventoryCategoryMaster.compid='" + Session["comid"] + "' and InventorySubCategoryMaster.InventorySubCatId = '" + id.Text + "'  order by Name ";
                        SqlCommand cmdids = new SqlCommand(subids, con);
                        SqlDataAdapter adpids = new SqlDataAdapter(cmdids);
                        DataTable dtids = new DataTable();
                        adpids.Fill(dtids);
                        string strInvBySerchIdinvname = "";
                        if (dtids.Rows.Count > 0)
                        {
                            string strIdinvname = "";
                            string strInvAllIdsinvname = "";
                            string strtempinvname = "";


                            if (dtids.Rows.Count > 0)
                            {

                                foreach (DataRow dtrrr in dtids.Rows)
                                {
                                    strIdinvname = dtrrr["Id"].ToString();
                                    strInvAllIdsinvname = strIdinvname + "," + strInvAllIdsinvname;
                                    strtempinvname = strInvAllIdsinvname.Substring(0, (strInvAllIdsinvname.Length - 1));
                                }

                                strInvBySerchIdinvname = " and InventoruSubSubCategory.InventorySubSubId in (" + strtempinvname + ") ";
                                //string mainstring = strinv + "  order by SalesChallanMaster.RefSalesOrderId ";
                            }


                        }
                        if (strInvBySerchIdinvname != "")
                        {
                            string main = (string)(ViewState["mainstringforinvWHMid"].ToString()) + strInvBySerchIdinvname;
                            SqlCommand cmdinvwhid = new SqlCommand(main, con);
                            SqlDataAdapter adpinvwhid = new SqlDataAdapter(cmdinvwhid);
                            adpinvwhid.Fill(dtinvwh);



                        }


                    }
                    else if (Convert.ToString(ViewState["catid"]) == "3")
                    {
                        string subids = "SELECT     InventorySubCategoryMaster.InventorySubCatName, InventoryCategoryMaster.InventoryCatName, InventoruSubSubCategory.InventorySubSubId as Id,  " +
                           "   InventoruSubSubCategory.InventorySubSubName, InventoryCategoryMaster.InventoryCatName+' : '+InventorySubCategoryMaster.InventorySubCatName+' : '+InventoruSubSubCategory.InventorySubSubName AS Name " +
                           " FROM     InventoryCategoryMaster inner join InventorySubCategoryMaster on InventorySubCategoryMaster.InventoryCategoryMasterId=InventoryCategoryMaster.InventeroyCatId inner join InventoruSubSubCategory on InventoruSubSubCategory.InventorySubCatID=InventorySubCategoryMaster.InventorySubCatId inner join InventoryMaster on InventoryMaster.InventorySubSubId=InventoruSubSubCategory.InventorySubSubId inner join InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.InventoryMasterId=InventoryMaster.InventoryMasterId inner join WareHouseMaster on WareHouseMaster.WareHouseId=InventoryWarehouseMasterTbl.WareHouseId  WHERE InventoryWarehouseMasterTbl.WareHouseId ='" + ViewState["Whid"] + "' and InventoryCategoryMaster.compid='" + Session["comid"] + "' and InventoruSubSubCategory.InventorySubSubId = '" + id.Text + "' order by Name  ";
                        SqlCommand cmdids = new SqlCommand(subids, con);
                        SqlDataAdapter adpids = new SqlDataAdapter(cmdids);
                        DataTable dtids = new DataTable();
                        adpids.Fill(dtids);
                        string strInvBySerchIdinvname = "";
                        if (dtids.Rows.Count > 0)
                        {
                            string strIdinvname = "";
                            string strInvAllIdsinvname = "";
                            string strtempinvname = "";


                            if (dtids.Rows.Count > 0)
                            {

                                foreach (DataRow dtrrr in dtids.Rows)
                                {
                                    strIdinvname = dtrrr["Id"].ToString();
                                    strInvAllIdsinvname = strIdinvname + "," + strInvAllIdsinvname;
                                    strtempinvname = strInvAllIdsinvname.Substring(0, (strInvAllIdsinvname.Length - 1));
                                }

                                strInvBySerchIdinvname = " and InventoruSubSubCategory.InventorySubSubId in (" + strtempinvname + ") ";
                                //string mainstring = strinv + "  order by SalesChallanMaster.RefSalesOrderId ";
                            }


                        }
                        if (strInvBySerchIdinvname != "")
                        {
                            string main = (string)(ViewState["mainstringforinvWHMid"].ToString()) + strInvBySerchIdinvname;
                            SqlCommand cmdinvwhid = new SqlCommand(main, con);
                            SqlDataAdapter adpinvwhid = new SqlDataAdapter(cmdinvwhid);
                            adpinvwhid.Fill(dtinvwh);



                        }
                    }

                    else if (Convert.ToString(ViewState["catid"]) == "4")
                    {
                        string subids = "SELECT    InventoryMaster.InventoryMasterId as Id,  " +
                           "   InventoruSubSubCategory.InventorySubSubName, InventoryCategoryMaster.InventoryCatName+' : '+InventorySubCategoryMaster.InventorySubCatName+' : '+InventoruSubSubCategory.InventorySubSubName AS Name " +
                           " FROM     InventoryCategoryMaster inner join InventorySubCategoryMaster on InventorySubCategoryMaster.InventoryCategoryMasterId=InventoryCategoryMaster.InventeroyCatId inner join InventoruSubSubCategory on InventoruSubSubCategory.InventorySubCatID=InventorySubCategoryMaster.InventorySubCatId inner join InventoryMaster on InventoryMaster.InventorySubSubId=InventoruSubSubCategory.InventorySubSubId inner join InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.InventoryMasterId=InventoryMaster.InventoryMasterId inner join WareHouseMaster on WareHouseMaster.WareHouseId=InventoryWarehouseMasterTbl.WareHouseId  WHERE InventoryWarehouseMasterTbl.WareHouseId ='" + ViewState["Whid"] + "' and InventoryCategoryMaster.compid='" + Session["comid"] + "' and  InventoryMaster.InventoryMasterId = '" + id.Text + "' order by Name  ";
                        SqlCommand cmdids = new SqlCommand(subids, con);
                        SqlDataAdapter adpids = new SqlDataAdapter(cmdids);
                        DataTable dtids = new DataTable();
                        adpids.Fill(dtids);
                        string strInvBySerchIdinvname = "";
                        if (dtids.Rows.Count > 0)
                        {
                            string strIdinvname = "";
                            string strInvAllIdsinvname = "";
                            string strtempinvname = "";


                            if (dtids.Rows.Count > 0)
                            {

                                foreach (DataRow dtrrr in dtids.Rows)
                                {
                                    strIdinvname = dtrrr["Id"].ToString();
                                    strInvAllIdsinvname = strIdinvname + "," + strInvAllIdsinvname;
                                    strtempinvname = strInvAllIdsinvname.Substring(0, (strInvAllIdsinvname.Length - 1));
                                }

                                strInvBySerchIdinvname = " and InventoryMaster.InventoryMasterId in (" + strtempinvname + ") ";
                                //string mainstring = strinv + "  order by SalesChallanMaster.RefSalesOrderId ";
                            }


                        }
                        if (strInvBySerchIdinvname != "")
                        {
                            string main = (string)(ViewState["mainstringforinvWHMid"].ToString()) + strInvBySerchIdinvname;
                            SqlCommand cmdinvwhid = new SqlCommand(main, con);
                            SqlDataAdapter adpinvwhid = new SqlDataAdapter(cmdinvwhid);
                            adpinvwhid.Fill(dtinvwh);



                        }
                    }


                    if (dtinvwh.Rows.Count > 0)
                    {


                        foreach (DataRow dtrr in dtinvwh.Rows)
                        {
                            string sd12 = sd1.Text;
                            string ed12 = ed1.Text;
                            string ma12 = ma1.Text;
                            string mq12 = mq1.Text;
                          

                            int invWHMasterid = Convert.ToInt32(dtrr["InventoryWarehouseMasterId"]);
                            string str = "";

                            string subids = "Select * from FreeShippingToInvs where InventoryWHM_Id='" + invWHMasterid + "' and  FreeShippRuleId='" + ddlWarehouse.SelectedValue + "'";
                            SqlCommand cmdids = new SqlCommand(subids, con);
                            SqlDataAdapter adpids = new SqlDataAdapter(cmdids);
                            DataTable dtids = new DataTable();
                            adpids.Fill(dtids);
                            if (dtids.Rows.Count > 0)
                            {
                                if (rddate.SelectedIndex == 1)
                                {
                                    str = "Update FreeShippingToInvs Set MinAmout= '" + Convert.ToDecimal(ma12) + "', MinQty='" + Convert.ToDecimal(mq12) + "',StartDate= '" + Convert.ToDateTime(sd12) + "', EndDate='" + Convert.ToDateTime(ed12) + "',Effective='" + chkaply.Checked + "' where InventoryWHM_Id='" + invWHMasterid + "' and  FreeShippRuleId='" + ddlWarehouse.SelectedValue + "'";
                                }
                                else
                                {
                                    str = "Update FreeShippingToInvs Set MinAmout= '" + Convert.ToDecimal(ma12) + "', MinQty='" + Convert.ToDecimal(mq12) + "',StartDate= NULL, EndDate=NULL,Effective='" + chkaply.Checked + "' where InventoryWHM_Id='" + invWHMasterid + "' and  FreeShippRuleId='" + ddlWarehouse.SelectedValue + "'";
   
                                }
                            }
                            else
                            {
                                if (rddate.SelectedIndex == 1)
                                {
                                    str = "INSERT INTO FreeShippingToInvs(InventoryWHM_Id,MinAmout ,MinQty,StartDate ,EndDate,Effective,FreeShippRuleId) " +
                                    " VALUES('" + invWHMasterid + "','" + Convert.ToDecimal(ma12) + "', " +
                                    " '" + Convert.ToDecimal(mq12) + "', '" + Convert.ToDateTime(sd12) + "', " +
                                    "   '" + Convert.ToDateTime(ed12) + "' ,'" + chkaply.Checked + "' ,'" + ddlWarehouse.SelectedValue + "'  ) ";
                                }
                                else
                                {
                                    str = "INSERT INTO FreeShippingToInvs(InventoryWHM_Id,MinAmout ,MinQty,Effective,FreeShippRuleId) " +
                                    " VALUES('" + invWHMasterid + "','" + Convert.ToDecimal(ma12) + "', " +
                                    " '" + Convert.ToDecimal(mq12) + "', " +
                                    "   '" + chkaply.Checked + "' ,'" + ddlWarehouse.SelectedValue + "'  ) ";

                                }
                            }
                                SqlCommand cmd = new SqlCommand(str, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmd.ExecuteNonQuery();
                            con.Close();
                            Lblmsg.Visible = true;
                            Lblmsg.Text = "Record inserted successfully";
                        }
                    }

                }
            }
        }
        if (vvv > 0)
        {
           
            panel234.Visible = false;
            ImageButton3.Visible = false;
            FillGridESCFreeShipToInv();
        }
        pnlEsc.Visible = true;
    }
    protected decimal isdecimalornot(string ck)
    {
        decimal ick = 0;
        try
        {
            ick = Convert.ToDecimal(ck);
            return ick;
        }
        catch
        {
            return ick;
        }
        //return ick;
    }
    protected void btnAddFreeShippingRule_Click(object sender, EventArgs e)
    {
        GridViewRow ft = (GridViewRow)grdFreeRule.FooterRow;

        DropDownList ddlgrdcountry = (DropDownList)ft.FindControl("ddlGrdCountry");
        DropDownList ddlware = (DropDownList)grdFreeRule.FooterRow.FindControl("ddlware");
        DropDownList ddlgrdstate = (DropDownList)grdFreeRule.FooterRow.FindControl("ddlGrdState");
        DropDownList ddlgrdcity = (DropDownList)grdFreeRule.FooterRow.FindControl("ddlGrdCity");
        TextBox txtname = (TextBox)grdFreeRule.FooterRow.FindControl("txtNewSchemForFreeShip");
        TextBox minordersize = (TextBox)grdFreeRule.FooterRow.FindControl("txtMinOrdrSize");
        TextBox maxordersize = (TextBox)grdFreeRule.FooterRow.FindControl("txtMaxOrdrSize");
        string str111 = "Select Name from FreeShippingRule where Name='" + txtname.Text + "' and Whid='" + ddlware.SelectedValue + "'";
        SqlCommand cmdstr111 = new SqlCommand(str111, con);
        SqlDataAdapter dastr111 = new SqlDataAdapter(cmdstr111);
        DataTable dtstr111 = new DataTable();
        dastr111.Fill(dtstr111);
        if (dtstr111.Rows.Count == 0)
        {
            decimal c1 = Convert.ToDecimal(isdecimalornot(maxordersize.Text));
            decimal b1 = Convert.ToDecimal(isdecimalornot(minordersize.Text));
            if (b1 > c1)
            {

                Lblmsg.Visible = true;
                Lblmsg.Text = "Minimum order size must be less than Maximum order size";

            }

            else
            {
                try
                {

                    string insertruledata = " INSERT INTO FreeShippingRule    (Name ,MinOrdSize ,MaxOrderSize ,CountryId,StateId,CityId,compid,Whid) " +
                                            " VALUES ('" + txtname.Text + "','" + minordersize.Text + "','" + maxordersize.Text + "'," +
                                            " '" + Convert.ToInt32(ddlgrdcountry.SelectedValue) + "', " +
                                            " '" + Convert.ToInt32(ddlgrdstate.SelectedValue) + "' , " +
                                            "'" + Convert.ToInt32(ddlgrdcity.SelectedValue) + "','" + compid + "','" + ddlware.SelectedValue + "')";
                    SqlCommand cmdinsertruledata = new SqlCommand(insertruledata, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdinsertruledata.ExecuteNonQuery();
                    con.Close();
                    Lblmsg.Visible = true;
                    Lblmsg.Text = "Record inserted successfully ";
                    fillGrid();
                    txtname.Text = "";
                    minordersize.Text = "";
                    maxordersize.Text = "";

                    ddlgrdcountry.SelectedIndex = 0;
                    ddlgrdstate.SelectedIndex = 0;
                    ddlgrdcity.SelectedIndex = 0;
                    fillshipname();
                }
                catch (Exception rt)
                {
                    Lblmsg.Visible = true;
                    Lblmsg.Text = " error : " + rt.Message;
                }
            }
        }
        else
        {
            Lblmsg.Visible = true;
            Lblmsg.Text = "This name is already used";
        }
    }
    protected void ddlGrdCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        //    DropDownList ddlgrdcountry1 = (DropDownList)(ViewState["ddlGC"]);
        //    DropDownList ddlgrdstate1 = (DropDownList)(ViewState["ddlGS"]);
        //    DropDownList ddlgrdcity1 = (DropDownList)(ViewState["ddlGCt"]);

        //    if (ddlcountry.SelectedIndex > 0)
        //    {


        //        SqlCommand cmd = new SqlCommand("SELECT CityId, CityName FROM  CityMasterTbl WHERE  (StateId = '" + ddlgrdcountry1.SelectedValue + "')", con);
        //        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //        DataSet ds = new DataSet();
        //        adp.Fill(ds);
        //        ddlgrdstate1.DataSource = ds;
        //        ddlgrdstate1.DataTextField = "StateName";
        //        ddlgrdstate1.DataValueField = "StateId";
        //        ddlgrdstate1.DataBind();
        //        ViewState["state"] = ds;
        //        ddlgrdstate1.Items.Insert(0, "All");
        //        ddlgrdstate1.Items[0].Value = "0";
        //    }
        //    else
        //    {
        //        ddlgrdstate1.SelectedIndex = -1;
        //    }
        //    //SqlCommand cmd = new SqlCommand("SELECT StateId, StateName FROM  StateMasterTbl WHERE  (CountryId = '" + ddlcountry.SelectedValue + "')", con);
        //    //SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //    //DataSet ds = new DataSet();
        //    //adp.Fill(ds);
        //    //ddlstate.DataSource = ds;
        //    //ddlstate.DataTextField = "StateName";
        //    //ddlstate.DataValueField = "StateId";
        //    //ddlstate.DataBind();
        //    //ViewState["state"] = ds;
        //    //ddlstate.Items.Insert(0, "All");
    }

    protected void grdFreeRule_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.Footer)
        //{

        //    DropDownList ddlgrdcountry = (DropDownList)e.Row.FindControl("ddlGrdCountry");
        //    DropDownList ddlgrdstate = (DropDownList)e.Row.FindControl("ddlGrdState");
        //    DropDownList ddlgrdcity = (DropDownList)e.Row.FindControl("ddlGrdCity");
        //    //DropDownList objDDL = new DropDownList();
        //    //objDDL.TextField = "DisplayFieldName";
        //    //objDDL.ValueField = "ValueFieldName";
        //    //objDDL.datasource = null;
        //    //objDDL.DataBind();

        //    SqlCommand cmd = new SqlCommand("SELECT CountryId, CountryName FROM  CountryMaster", con);
        //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //    DataSet ds = new DataSet();
        //    adp.Fill(ds);
        //    ddlgrdcountry.DataSource = ds;
        //    ddlgrdcountry.DataTextField = "CountryName";
        //    ddlgrdcountry.DataValueField = "CountryId";
        //    ddlgrdcountry.DataBind();
        //    ddlgrdcountry.Items.Insert(0, "-Select-");
        //    ddlgrdcountry.Items[0].Value = "0";
        //    //e.Row.Cells[0].Controls.Add(objDDL);







        //}

    }
    

    protected void ddlGrdCountry_SelectedIndexChanged1(object sender, EventArgs e)
    {

        GridViewRow ft = (GridViewRow)grdFreeRule.FooterRow;

        DropDownList ddlgrdcountry = (DropDownList)ft.FindControl("ddlGrdCountry");
        DropDownList ddlgrdstate = (DropDownList)grdFreeRule.FooterRow.FindControl("ddlGrdState");
        DropDownList ddlgrdcity = (DropDownList)grdFreeRule.FooterRow.FindControl("ddlGrdCity");
        ddlgrdstate.Items.Clear();
        ddlgrdcity.Items.Clear();
        if (ddlgrdcountry.SelectedIndex > 0)
        {
            SqlCommand cmd2 = new SqlCommand("SELECT StateId, StateName FROM  StateMasterTbl WHERE  (CountryId = '" + ddlgrdcountry.SelectedValue + "')", con);
            SqlDataAdapter adp2 = new SqlDataAdapter(cmd2);
            DataSet ds2 = new DataSet();
            adp2.Fill(ds2);
            ddlgrdstate.DataSource = ds2;
            ddlgrdstate.DataTextField = "StateName";
            ddlgrdstate.DataValueField = "StateId";
            ddlgrdstate.DataBind();
            ViewState["state"] = ds2;
            ddlgrdstate.Items.Insert(0, "All");

            ddlgrdstate.Items[0].Value = "0";
            ddlGrdState_SelectedIndexChanged(sender, e);
        }
        else
        {
            ddlgrdstate.Items.Insert(0, "All");

            ddlgrdstate.Items[0].Value = "0";
            //ddlgrdstate.SelectedIndex = -1;

            ddlgrdcity.Items.Insert(0, "All");

            ddlgrdcity.Items[0].Value = "0";
            // ddlgrdcity.SelectedIndex = -1;
            ddlGrdState_SelectedIndexChanged(sender, e);
        }


    }
    protected void ddlGrdState_SelectedIndexChanged(object sender, EventArgs e)
    {

        GridViewRow ft = (GridViewRow)grdFreeRule.FooterRow;

        DropDownList ddlgrdcountry = (DropDownList)ft.FindControl("ddlGrdCountry");
        DropDownList ddlgrdstate = (DropDownList)grdFreeRule.FooterRow.FindControl("ddlGrdState");
        DropDownList ddlgrdcity = (DropDownList)grdFreeRule.FooterRow.FindControl("ddlGrdCity");
        
        ddlgrdcity.Items.Clear();
        if (ddlgrdstate.SelectedIndex > 0)
        {
            SqlCommand cmd4 = new SqlCommand("SELECT CityId,CityName,StateId  FROM CityMasterTbl WHERE  (StateId = '" + ddlgrdstate.SelectedValue + "')", con);
            SqlDataAdapter adp4 = new SqlDataAdapter(cmd4);
            DataSet ds4 = new DataSet();
            adp4.Fill(ds4);
            ddlgrdcity.DataSource = ds4;
            ddlgrdcity.DataTextField = "CityName";
            ddlgrdcity.DataValueField = "CityId";
            ddlgrdcity.DataBind();
            ViewState["state"] = ds4;
            ddlgrdcity.Items.Insert(0, "All");
            ddlgrdcity.Items[0].Value = "0";
        }
        else
        {
            ddlgrdcity.Items.Insert(0, "All");
            ddlgrdcity.Items[0].Value = "0";
            //ddlgrdcity.SelectedIndex = -1;
        }
    }
    protected void grdFreeRule_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            grdFreeRule.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            int dk = Convert.ToInt32(grdFreeRule.SelectedDataKey.Value);
            ViewState["ruleid"] = dk;

            Label nm = (Label)(grdFreeRule.Rows[grdFreeRule.SelectedIndex].FindControl("lblFreeShipRule"));
            Label ware = (Label)(grdFreeRule.Rows[grdFreeRule.SelectedIndex].FindControl("lblwname"));
            ddlWarehouse.SelectedIndex = ddlWarehouse.Items.IndexOf(ddlWarehouse.Items.FindByText(ware.Text));
            ddlWarehouse.Enabled = false;
            lblFreeshippingruleName.Text = nm.Text;
            pnlRuleDetails.Visible = true;
            txtEndDate.Text = "";
            txtStartDate.Text = "";
            txtMinQty.Text = "";
            txtMinAmt.Text = "";
            pnlEsc.Visible = false;

            FillGridESCFreeShipToInv();

        }
        if (e.CommandName == "Delete1")
        {
            //grdFreeRule.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            //fillGrid();
            //int dk = Convert.ToInt32(grdFreeRule.SelectedDataKey.Value);
            //  Label lblid = (Label)grdFreeRule.Rows[e.RowIndex].FindControl("lblShipMangrId");
            
            //grdFreeRule.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            //int dk = Convert.ToInt32(grdFreeRule.SelectedDataKey.Value);

            //string Delsttr = "Delete  FreeShippingRule where Id=" + dk + " ";
            //SqlCommand cmdinsert1 = new SqlCommand(Delsttr, con);
            //con.Open();
            //cmdinsert1.ExecuteNonQuery();
            //con.Close();



            //string Delsttr2 = "Delete  FreeShippingToInvs where FreeShippRuleId=" + dk + " ";
            //SqlCommand cmdinsert12 = new SqlCommand(Delsttr2, con);
            //con.Open();
            //cmdinsert12.ExecuteNonQuery();
            //con.Close();

            ////(,

            //Lblmsg.Visible = true;
            //Lblmsg.Text = "Record Delete Successfully";
            //fillGrid();
        }

        //if (e.CommandName == "Edit1")
        //{
        //    return; 
        //}
    }
    protected void fillgrd()
    {
        if ( txtMinAmt.Text != "" && txtMinQty.Text != "" )
        {
            if (RadioButtonList2.SelectedValue == "0")
            {
                FillGridByCategory();
              
            }
            else if (RadioButtonList2.SelectedValue == "1")
            {
                FillGridBySubCategory();
               
            }
            else if (RadioButtonList2.SelectedValue == "2")
            {
                FillGridBySubSubCategory();
              
            }
            else if (RadioButtonList2.SelectedValue == "3")
            {
                FillGridByProductname();
                //if (grdCat.Rows.Count > 0)
                //{
                //    foreach (GridViewRow gdrrr in grdCat.Rows)
                //    {
                //        Label sd = (Label)gdrrr.FindControl("lblStartDate");
                //        Label ed = (Label)gdrrr.FindControl("lblEndDate");
                //        Label ma = (Label)gdrrr.FindControl("lblminAmt");
                //        Label mq = (Label)gdrrr.FindControl("lblMinQty");

                //        sd.Text = txtStartDate.Text;
                //        ed.Text = txtEndDate.Text;
                //        ma.Text = txtMinAmt.Text;
                //        mq.Text = txtMinQty.Text;


                //    }
                //}
            }
            else
            {
                grdCat.DataSource = null;
                grdCat.DataBind();
                if (ImageButton3.Visible == true)
                {
                    ImageButton3.Visible = false;
                }
            }
            panel234.Visible = true;
        }
    }
    protected void FillGridByCategory()
    {


        string sttGridCat = "SELECT Distinct  InventoryCategoryMaster.InventeroyCatId as Id, InventoryCategoryMaster.InventoryCatName as Cname,InventoryCategoryMaster.InventoryCatName as Subname,InventoryCategoryMaster.InventoryCatName as Subsubname,InventoryCategoryMaster.InventoryCatName as invname FROM InventoryCategoryMaster inner join InventorySubCategoryMaster on InventorySubCategoryMaster.InventoryCategoryMasterId=InventoryCategoryMaster.InventeroyCatId inner join InventoruSubSubCategory on InventoruSubSubCategory.InventorySubCatID=InventorySubCategoryMaster.InventorySubCatId inner join InventoryMaster on InventoryMaster.InventorySubSubId=InventoruSubSubCategory.InventorySubSubId inner join InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.InventoryMasterId=InventoryMaster.InventoryMasterId inner join WareHouseMaster on WareHouseMaster.WareHouseId=InventoryWarehouseMasterTbl.WareHouseId  WHERE InventoryWarehouseMasterTbl.WareHouseId ='" + ViewState["Whid"] + "' and InventoryCategoryMaster.compid='" + Session["comid"] + "' order by Cname";
        // string sttGridCat = " select InventoryCatName as name,InventeroyCatId as Id from InventoryCategoryMaster where compid='"+Session["comid"]+"'";
        SqlCommand cmdGridCat = new SqlCommand(sttGridCat, con);
        SqlDataAdapter adpGridCat = new SqlDataAdapter(cmdGridCat);
        DataTable dtGridCat = new DataTable();
        adpGridCat.Fill(dtGridCat);
        if (dtGridCat.Rows.Count > 0)
        {
            grdCat.DataSource = dtGridCat;
            grdCat.DataBind();

            //int catid = Convert.ToInt32(dtGridCat.Rows[
            ViewState["catid"] = "1";
            if (ImageButton3.Visible == false)
            {
                ImageButton3.Visible = true;
            } 
        }
        else
        {
            grdCat.DataSource = null;
            grdCat.DataBind();
            if (ImageButton3.Visible == true)
            {
                ImageButton3.Visible = false;
            } 
        }
        if (grdCat.Rows.Count > 0)
        {
            grdCat.Columns[1].Visible = false;
            grdCat.Columns[2].Visible = false;
            grdCat.Columns[3].Visible = false;
        }
    }



    
    protected void FillGridBySubCategory()
    {
        string sttGridSCat = "  SELECT Distinct    InventorySubCategoryMaster.InventorySubCatId as Id, InventorySubCategoryMaster.InventorySubCatName, " +
               "InventoryCategoryMaster.InventoryCatName as Subsubname,InventoryCategoryMaster.InventoryCatName as invname, InventoryCategoryMaster.InventoryCatName as Cname, InventorySubCategoryMaster.InventorySubCatName as Subname,  InventoryCategoryMaster.InventoryCatName , " +
               " InventoryCategoryMaster.InventoryCatName + ' : ' + InventorySubCategoryMaster.InventorySubCatName as Name " +
                      "FROM       InventoryCategoryMaster RIGHT OUTER JOIN " +
  "             InventorySubCategoryMaster ON InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId inner join InventoruSubSubCategory on InventoruSubSubCategory.InventorySubCatID=InventorySubCategoryMaster.InventorySubCatId inner join InventoryMaster on InventoryMaster.InventorySubSubId=InventoruSubSubCategory.InventorySubSubId inner join InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.InventoryMasterId=InventoryMaster.InventoryMasterId inner join WareHouseMaster on WareHouseMaster.WareHouseId=InventoryWarehouseMasterTbl.WareHouseId  WHERE InventoryWarehouseMasterTbl.WareHouseId ='" + ViewState["Whid"] + "' and InventoryCategoryMaster.compid='" + Session["comid"] + "' order by InventoryCategoryMaster.InventoryCatName,InventorySubCategoryMaster.InventorySubCatName ";


        //   string sttGridCat = " select InventoryCatName as name,InventeroyCatId as id from InventoryCategoryMaster";
        SqlCommand cmdGridSCat = new SqlCommand(sttGridSCat, con);
        SqlDataAdapter adpGridSCat = new SqlDataAdapter(cmdGridSCat);
        DataTable dtGridSCat = new DataTable();
        adpGridSCat.Fill(dtGridSCat);
        if (dtGridSCat.Rows.Count > 0)
        {
            grdCat.DataSource = dtGridSCat;
            grdCat.DataBind();
            ViewState["catid"] = "2";
            if (ImageButton3.Visible == false)
            {
                ImageButton3.Visible = true;
            }
        }
        else
        {
            grdCat.DataSource = null;
            grdCat.DataBind();
            if (ImageButton3.Visible == true)
            {
                ImageButton3.Visible = false;
            }
        }
        if (grdCat.Rows.Count > 0)
        {
            grdCat.Columns[1].Visible = true;
            grdCat.Columns[2].Visible = false;
            grdCat.Columns[3].Visible = false;
        }
    }
    protected void FillGridByProductname()
    {

        string sttGridSSCat = "  SELECT   Distinct  InventorySubCategoryMaster.InventorySubCatName, InventoryCategoryMaster.InventoryCatName, InventoryMaster.InventoryMasterId as Id, " +
            " InventoryMaster.Name as Invname,InventorySubCategoryMaster.InventorySubCatName as Subname, InventoryCategoryMaster.InventoryCatName as Cname,InventoruSubSubCategory.InventorySubSubName as Subsubname, InventoryCategoryMaster.InventoryCatName+' : '+InventorySubCategoryMaster.InventorySubCatName+' : '+InventoruSubSubCategory.InventorySubSubName+' : '+InventoryMaster.Name AS Name " +
                      " FROM      InventoryCategoryMaster inner join InventorySubCategoryMaster on InventorySubCategoryMaster.InventoryCategoryMasterId=InventoryCategoryMaster.InventeroyCatId inner join InventoruSubSubCategory on InventoruSubSubCategory.InventorySubCatID=InventorySubCategoryMaster.InventorySubCatId inner join InventoryMaster on InventoryMaster.InventorySubSubId=InventoruSubSubCategory.InventorySubSubId inner join InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.InventoryMasterId=InventoryMaster.InventoryMasterId inner join WareHouseMaster on WareHouseMaster.WareHouseId=InventoryWarehouseMasterTbl.WareHouseId  WHERE InventoryWarehouseMasterTbl.WareHouseId ='" + ViewState["Whid"] + "' and InventoryCategoryMaster.compid='" + Session["comid"] + "' order by InventoryCategoryMaster.InventoryCatName,InventorySubCategoryMaster.InventorySubCatName,InventoruSubSubCategory.InventorySubSubName,InventoryMaster.Name ";

        //   string sttGridCat = " select InventoryCatName as name,InventeroyCatId as id from InventoryCategoryMaster";
        SqlCommand cmdGridSSCat = new SqlCommand(sttGridSSCat, con);
        SqlDataAdapter adpGridSSCat = new SqlDataAdapter(cmdGridSSCat);
        DataTable dtGridSSCat = new DataTable();
        adpGridSSCat.Fill(dtGridSSCat);
        if (dtGridSSCat.Rows.Count > 0)
        {
            grdCat.DataSource = dtGridSSCat;
            grdCat.DataBind();
            ViewState["catid"] = "4";
            if (ImageButton3.Visible == false)
            {
                ImageButton3.Visible = true;
            }
        }
        else
        {
            grdCat.DataSource = null;
            grdCat.DataBind();
           
            if (ImageButton3.Visible == true)
            {
                ImageButton3.Visible = false;
            }
        }
        if (grdCat.Rows.Count > 0)
        {
            grdCat.Columns[1].Visible = true;
            grdCat.Columns[2].Visible = true;
            grdCat.Columns[3].Visible = true;
        }
    }
    protected void FillGridBySubSubCategory()
    {

        string sttGridSSCat = "  SELECT Distinct    InventorySubCategoryMaster.InventorySubCatName, InventoryCategoryMaster.InventoryCatName, InventoruSubSubCategory.InventorySubSubId as Id, " +
                      "InventoryCategoryMaster.InventoryCatName as invname, InventorySubCategoryMaster.InventorySubCatName as Subname, InventoryCategoryMaster.InventoryCatName as Cname,InventoruSubSubCategory.InventorySubSubName as Subsubname, InventoruSubSubCategory.InventorySubSubName, InventoryCategoryMaster.InventoryCatName+' : '+InventorySubCategoryMaster.InventorySubCatName+' : '+InventoruSubSubCategory.InventorySubSubName AS Name " +
                      " FROM      InventoryCategoryMaster inner join InventorySubCategoryMaster on InventorySubCategoryMaster.InventoryCategoryMasterId=InventoryCategoryMaster.InventeroyCatId inner join InventoruSubSubCategory on InventoruSubSubCategory.InventorySubCatID=InventorySubCategoryMaster.InventorySubCatId inner join InventoryMaster on InventoryMaster.InventorySubSubId=InventoruSubSubCategory.InventorySubSubId inner join InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.InventoryMasterId=InventoryMaster.InventoryMasterId inner join WareHouseMaster on WareHouseMaster.WareHouseId=InventoryWarehouseMasterTbl.WareHouseId  WHERE InventoryWarehouseMasterTbl.WareHouseId ='" + ViewState["Whid"] + "' and InventoryCategoryMaster.compid='" + Session["comid"] + "'  order by InventoryCategoryMaster.InventoryCatName,InventorySubCategoryMaster.InventorySubCatName,InventoruSubSubCategory.InventorySubSubName";

        //   string sttGridCat = " select InventoryCatName as name,InventeroyCatId as id from InventoryCategoryMaster";
        SqlCommand cmdGridSSCat = new SqlCommand(sttGridSSCat, con);
        SqlDataAdapter adpGridSSCat = new SqlDataAdapter(cmdGridSSCat);
        DataTable dtGridSSCat = new DataTable();
        adpGridSSCat.Fill(dtGridSSCat);
        if (dtGridSSCat.Rows.Count > 0)
        {
            grdCat.DataSource = dtGridSSCat;
            grdCat.DataBind();
            ViewState["catid"] = "3";
            if (ImageButton3.Visible == false)
            {
                ImageButton3.Visible = true;
            }
        }
        else
        {
            grdCat.DataSource = null;
            grdCat.DataBind();
         
            if (ImageButton3.Visible == true)
            {
                ImageButton3.Visible = false;
            }
        }
        if (grdCat.Rows.Count > 0)
        {
            grdCat.Columns[1].Visible = true;
            grdCat.Columns[2].Visible = true;
            grdCat.Columns[3].Visible = false;
        }
    }

    protected void ImgBtnGo_Click(object sender, EventArgs e)
    {

        int dtaallow = 0;
        if (rddate.SelectedIndex == 1)
        {
            string str = "select Convert(nvarchar,StartDate,101)   as StartDate,EndDate from [ReportPeriod] where Compid = '" + compid + "' and Active='1'  and Whid='" + ViewState["Whid"] + "'";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToDateTime(txtStartDate.Text) < Convert.ToDateTime(dt.Rows[0][0].ToString()))
                {
                    dtaallow = 0;
                    lblstartdate.Text = dt.Rows[0][0].ToString();
                    ModalPopupExtender1222222.Show();

                }


                else
                {
                    DateTime dt2 = Convert.ToDateTime(txtStartDate.Text);
                    DateTime dt1 = Convert.ToDateTime(txtEndDate.Text);


                    if (dt1 < dt2)
                    {

                        Lblmsg.Visible = true;
                        Lblmsg.Text = " Start Date must be less than End Date";


                    }
                    else
                    {
                        dtaallow = 1;
                    }
                }
            }
        }
        else
        {
            dtaallow = 1;
        }
        if (dtaallow == 1)
        {
            fillgrd();
            if (grdCat.Rows.Count > 0)
            {
                ImageButton3.Visible = true;
                panel234.Visible = true;
                foreach (GridViewRow gdrrr in grdCat.Rows)
                {
                    Label sd = (Label)gdrrr.FindControl("lblStartDate");
                    Label ed = (Label)gdrrr.FindControl("lblEndDate");
                    Label ma = (Label)gdrrr.FindControl("lblminAmt");
                    Label mq = (Label)gdrrr.FindControl("lblMinQty");
                    if (rddate.SelectedIndex == 1)
                    {
                        sd.Text = txtStartDate.Text;
                        ed.Text = txtEndDate.Text;
                    }
                    else
                    {
                        sd.Text = "All";
                        ed.Text = "All";
                    }
                    ma.Text = txtMinAmt.Text;
                    mq.Text = txtMinQty.Text;
                }
            }

        }
        
        
    }
    protected void chkAll_chachedChanged(object sender, EventArgs e)
    {
        CheckBox chk;
        foreach (GridViewRow rowitem in grdCat.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("chkApply"));
            chk.Checked = ((CheckBox)sender).Checked;

          
        }
    }
    protected void grdCat_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            return;
        }
    }
    protected void ddlWarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {


        txtMinQty.Text = "0";
        txtMinAmt.Text = "0";
        txtStartDate.Text = "";
        txtEndDate.Text = "";
          string strq = "select * from [FreeShippingRule] where Id = '" + ddlWarehouse.SelectedValue + "'";
        SqlCommand cmdq = new SqlCommand(strq, con);
        SqlDataAdapter daq = new SqlDataAdapter(cmdq);
        DataTable dtq = new DataTable();
        daq.Fill(dtq);
        if (dtq.Rows.Count > 0)
        {

            ViewState["Whid"] = Convert.ToString(dtq.Rows[0]["Whid"]);
            string strbySSCat = "SELECT     InventoryWarehouseMasterTbl.InventoryWarehouseMasterId, InventoryWarehouseMasterTbl.WareHouseId, InventoryWarehouseMasterTbl.Active, " +
                         " InventoryWarehouseMasterTbl.QtyOnHand, InventoryWarehouseMasterTbl.Rate, InventoryWarehouseMasterTbl.Weight,  " +
                         " InventoryWarehouseMasterTbl.QtyOnDateStarted, InventoryWarehouseMasterTbl.NormalOrderQuantity, InventoryWarehouseMasterTbl.ReorderQuantiy,  " +
                         " InventoryWarehouseMasterTbl.ReorderLevel, InventoryWarehouseMasterTbl.PreferredVendorId, InventoryMaster.InventoryMasterId,  " +
                         " InventoryMaster.Name AS InventoryName, InventoryMaster.ProductNo, InventoryDetails.Inventory_Details_Id, InventoryDetails.Description,  " +
                         "  InventoruSubSubCategory.InventorySubSubId, InventoruSubSubCategory.InventorySubSubName,  " +
                         " InventorySubCategoryMaster.InventorySubCatId, InventorySubCategoryMaster.InventorySubCatName, InventoryCategoryMaster.InventeroyCatId,  " +
                         " InventoryCategoryMaster.InventoryCatName , InventoryCategoryMaster.InventoryCatName+' : '+InventorySubCategoryMaster.InventorySubCatName+ " +
                         " ' : ' +InventoruSubSubCategory.InventorySubSubName as CScSSc " +
                         "  FROM         InventoryWarehouseMasterTbl LEFT OUTER JOIN " +
                         " InventoryMaster LEFT OUTER JOIN " +
                         " InventoryDetails ON InventoryMaster.InventoryDetailsId = InventoryDetails.Inventory_Details_Id LEFT OUTER JOIN " +
                         " InventoruSubSubCategory ON InventoryMaster.InventorySubSubId = InventoruSubSubCategory.InventorySubSubId ON  " +
                         " InventoryWarehouseMasterTbl.InventoryMasterId = InventoryMaster.InventoryMasterId LEFT OUTER JOIN " +
                         " InventoryCategoryMaster RIGHT OUTER JOIN " +
                         " InventorySubCategoryMaster ON InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId ON  " +
                         " InventoruSubSubCategory.InventorySubCatID = InventorySubCategoryMaster.InventorySubCatId " +
                         " where InventoryWarehouseMasterTbl.WareHouseId='" + Convert.ToInt32(ViewState["Whid"]) + "' ";





            ViewState["mainstringforinvWHMid"] = strbySSCat;
            FillGridESCFreeShipToInv();
        }
    }
    protected void grdFreeRule_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdFreeRule.EditIndex = e.NewEditIndex;
        fillGrid();
        DropDownList ddlc = (DropDownList)grdFreeRule.Rows[grdFreeRule.EditIndex].FindControl("ddlGrdCountry");
        DropDownList ddls = (DropDownList)grdFreeRule.Rows[grdFreeRule.EditIndex].FindControl("ddlGrdState");
        DropDownList ddlct = (DropDownList)grdFreeRule.Rows[grdFreeRule.EditIndex].FindControl("ddlGrdCity");
        Label cid = (Label)grdFreeRule.Rows[grdFreeRule.EditIndex].FindControl("lblCountryId");
        Label sid = (Label)grdFreeRule.Rows[grdFreeRule.EditIndex].FindControl("lblStateId");
        Label ctid = (Label)grdFreeRule.Rows[grdFreeRule.EditIndex].FindControl("lblCityId");
        DropDownList ddlc1 = (DropDownList)grdFreeRule.Rows[grdFreeRule.EditIndex].FindControl("ddlwarename");
        Label lblc = (Label)grdFreeRule.Rows[grdFreeRule.EditIndex].FindControl("lblwarename");

        SqlCommand cmdw = new SqlCommand("SELECT * FROM  WarehouseMaster where comid='" + Session["comid"] + "' and [WareHouseMaster].Status='1' ", con);
        SqlDataAdapter adpw = new SqlDataAdapter(cmdw);
        DataTable ddtw = new DataTable();
        adpw.Fill(ddtw);
        ddlc1.DataSource = ddtw;
        ddlc1.DataTextField = "Name";
        ddlc1.DataValueField = "WareHouseId";
        ddlc1.DataBind();
        ddlc1.Items.Insert(0, "All");
        ddlc1.Items[0].Value = "0";



        ddlc1.SelectedIndex = ddlc1.Items.IndexOf(ddlc1.Items.FindByText(lblc.Text));



        SqlCommand cmd = new SqlCommand("SELECT CountryId, CountryName FROM  CountryMaster", con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ddt = new DataTable();
        adp.Fill(ddt);
        ddlc.DataSource = ddt;
        ddlc.DataTextField = "CountryName";
        ddlc.DataValueField = "CountryId";
        ddlc.DataBind();
        ddlc.Items.Insert(0, "-Select-");
        ddlc.Items[0].Value = "0";
        if (cid.Text != "")
        {
            ddlc.SelectedIndex = ddlc.Items.IndexOf(ddlc.Items.FindByValue(cid.Text));




            SqlCommand cmd1 = new SqlCommand("SELECT StateId, StateName FROM  StateMasterTbl WHERE  (CountryId = '" + cid.Text + "')", con);
            SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
            DataTable dtdt1 = new DataTable();
            adp1.Fill(dtdt1);
            ddls.DataSource = dtdt1;
            ddls.DataTextField = "StateName";
            ddls.DataValueField = "StateId";
            ddls.DataBind();
            //ViewState["state"] = ds;
            ddls.Items.Insert(0, "ALL");
            ddls.Items[0].Value = "0";
            if (sid.Text != "")
            {
                ddls.SelectedIndex = ddls.Items.IndexOf(ddls.Items.FindByValue(sid.Text));
                SqlCommand cmd3 = new SqlCommand("SELECT CityId, CityName FROM  CityMasterTbl WHERE  (StateId = '" + ddls.Text + "')", con);

                SqlDataAdapter adp3 = new SqlDataAdapter(cmd3);
                DataTable dtr = new DataTable();
                adp3.Fill(dtr);
                ddlct.DataSource = dtr;
                ddlct.DataTextField = "CityName";
                ddlct.DataValueField = "CityId";
                ddlct.DataBind();
                //ViewState["state"] = ds;
                ddlct.Items.Insert(0, "All");
                ddlct.Items[0].Value = "0";
                if (ctid.Text != "")
                {
                    ddlct.SelectedIndex = ddlct.Items.IndexOf(ddlct.Items.FindByValue(ctid.Text));

                }
            }
        }
      
    }

    protected void ddlGrdCountry_SelectedIndexChanged12(object sender, EventArgs e)
    {

        GridViewRow ft = (GridViewRow)grdFreeRule.FooterRow;
        DropDownList ddlcnt = (DropDownList)ft.FindControl("ddlGrdCountry");
        DropDownList ddlstt = (DropDownList)grdFreeRule.FooterRow.FindControl("ddlGrdState");
        DropDownList ddlcty = (DropDownList)grdFreeRule.FooterRow.FindControl("ddlGrdCity");
        ddlcty.Items.Clear();
        ddlstt.Items.Clear();
        if (ddlcnt.SelectedIndex > 0)
        {


            SqlCommand cmd1 = new SqlCommand("SELECT StateId, StateName FROM  StateMasterTbl WHERE  (CountryId = '" + ddlcnt.SelectedValue + "')", con);
            SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
            DataTable dtdt1 = new DataTable();
            adp1.Fill(dtdt1);
            ddlstt.DataSource = dtdt1;
            ddlstt.DataTextField = "StateName";
            ddlstt.DataValueField = "StateId";
            ddlstt.DataBind();
            //ViewState["state"] = ds;
            ddlstt.Items.Insert(0, "All");
            ddlstt.Items[0].Value = "0";
        }
        //    else
        //    {
        //        ddlgrdstate1.SelectedIndex = -1;
        //    }
        //SqlCommand cmd = new SqlCommand("SELECT CityId, CityName FROM  CityMasterTbl WHERE  (StateId = '" + ddlcnt.SelectedValue + "')", con);

        //    //SqlCommand cmd = new SqlCommand("SELECT StateId, StateName FROM  StateMasterTbl WHERE  (CountryId = '" + ddlcountry.SelectedValue + "')", con);
        //    //SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //    //DataSet ds = new DataSet();
        //    //adp.Fill(ds);
        //    //ddlstate.DataSource = ds;
        //    //ddlstate.DataTextField = "StateName";
        //    //ddlstate.DataValueField = "StateId";
        //    //ddlstate.DataBind();
        //    //ViewState["state"] = ds;
        //    //ddlstate.Items.Insert(0, "All");
    }

    protected void ddlGrdState_SelectedIndexChanged132(object sender, EventArgs e)
    {
        GridViewRow ft = (GridViewRow)grdFreeRule.FooterRow;
        DropDownList ddlcnt = (DropDownList)ft.FindControl("ddlGrdCountry");
        DropDownList ddlstt = (DropDownList)grdFreeRule.FooterRow.FindControl("ddlGrdState");
        DropDownList ddlcty = (DropDownList)grdFreeRule.FooterRow.FindControl("ddlGrdCity");
        ddlcty.Items.Clear();
        if (ddlstt.SelectedIndex > 0)
        {


            SqlCommand cmd2 = new SqlCommand("SELECT CityId, CityName FROM  CityMasterTbl WHERE  (StateId = '" + ddlstt.SelectedValue + "')", con);

            SqlDataAdapter adp2 = new SqlDataAdapter(cmd2);
            DataTable dtdtdt = new DataTable();
            adp2.Fill(dtdtdt);
            ddlcty.DataSource = dtdtdt;
            ddlcty.DataTextField = "CityName";
            ddlcty.DataValueField = "CityId";
            ddlcty.DataBind();
            //ViewState["state"] = ds;
            ddlcty.Items.Insert(0, "All");
            ddlcty.Items[0].Value = "0";
        }
        else
        {
            ddlcty.Items.Insert(0, "All");
            ddlcty.Items[0].Value = "0";
        }
    }
    protected void ddlGrdState_SelectedIndexChanged1(object sender, EventArgs e)
    {
        DropDownList ddcountry = (DropDownList)grdFreeRule.Rows[grdFreeRule.EditIndex].FindControl("ddlGrdCountry");

        DropDownList ddstate = (DropDownList)grdFreeRule.Rows[grdFreeRule.EditIndex].FindControl("ddlGrdState");
        DropDownList ddcity = (DropDownList)grdFreeRule.Rows[grdFreeRule.EditIndex].FindControl("ddlGrdCity");
        FillgrdSTct(ddstate, ddcity);
        //FillGrdCntST(ddcountry, ddstate);
    }
    protected void ddlGrdCountry_SelectedIndexChanged17772(object sender, EventArgs e)
    {
        int ihk = grdFreeRule.EditIndex;
        DropDownList ddcountry = (DropDownList)grdFreeRule.Rows[grdFreeRule.EditIndex].FindControl("ddlGrdCountry");
                                                                                                  
        DropDownList ddstate = (DropDownList)grdFreeRule.Rows[grdFreeRule.EditIndex].FindControl("ddlGrdState");
        DropDownList ddcity = (DropDownList)grdFreeRule.Rows[grdFreeRule.EditIndex].FindControl("ddlGrdCity");
        FillGrdCntST(ddcountry, ddstate);

    }
    protected void FillGrdCntST(DropDownList ddlCnt, DropDownList ddlSt)
    {
        ddlSt.Items.Clear();
        if (ddlCnt.SelectedIndex > 0)
        {
            string str45 = "SELECT     StateName  ,StateId " +
                   " FROM  StateMasterTbl where CountryId='" + ddlCnt.SelectedValue + "' " +
                   " Order By StateName";

            SqlCommand cmd45 = new SqlCommand(str45, con);

            SqlDataAdapter da = new SqlDataAdapter(cmd45);

            DataTable ds = new DataTable();

            da.Fill(ds);
            ddlSt.DataSource = ds;
            ddlSt.DataTextField = "StateName";
            ddlSt.DataValueField = "StateId";
            ddlSt.DataBind();

        }

        ddlSt.Items.Insert(0, "ALL");
        ddlSt.SelectedItem.Value = "0";


    }
    protected void FillgrdSTct(DropDownList ddlSt, DropDownList ddlCty)
    {
        if (ddlSt.SelectedIndex > 0)
        {
            string str455 = "SELECT     CityName  ,CityId " +
                       " FROM  CityMasterTbl where StateId='" + ddlSt.SelectedValue + "' " +
                       " Order By CityName";


            SqlCommand cmd45555 = new SqlCommand(str455, con);

            SqlDataAdapter da5 = new SqlDataAdapter(cmd45555);

            DataTable ds5 = new DataTable();

            da5.Fill(ds5);
            ddlCty.DataSource = ds5;
            ddlCty.DataTextField = "CityName";
            ddlCty.DataValueField = "CityId";
            ddlCty.DataBind();
        }
        ddlCty.Items.Insert(0, "ALL");
        ddlCty.SelectedItem.Value = "0";


    }

    protected void grdFreeRule_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdFreeRule.EditIndex = -1;
        fillGrid();
    }
    protected void grdFreeRule_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {


        DropDownList ddlc = (DropDownList)grdFreeRule.Rows[grdFreeRule.EditIndex].FindControl("ddlGrdCountry");
        DropDownList ddls = (DropDownList)grdFreeRule.Rows[grdFreeRule.EditIndex].FindControl("ddlGrdState");
        DropDownList ddlct = (DropDownList)grdFreeRule.Rows[grdFreeRule.EditIndex].FindControl("ddlGrdCity");
        DropDownList ddlware = (DropDownList)grdFreeRule.Rows[e.RowIndex].FindControl("ddlwarename");
        Label rulemid = (Label)grdFreeRule.Rows[grdFreeRule.EditIndex].FindControl("lblmid");

        TextBox rulename = (TextBox)grdFreeRule.Rows[grdFreeRule.EditIndex].FindControl("txtGrdInRuleName");
        TextBox minord = (TextBox)grdFreeRule.Rows[grdFreeRule.EditIndex].FindControl("txtGrdInMinOrder");
        TextBox maxord = (TextBox)grdFreeRule.Rows[grdFreeRule.EditIndex].FindControl("txtGrdInMaxOrder");
        string str111 = "Select Name from FreeShippingRule where Name='" + rulename.Text + "' and Whid='" + ddlware.SelectedValue + "' and  Id<>'" + rulemid.Text + "' ";
        SqlCommand cmdstr111 = new SqlCommand(str111, con);
        SqlDataAdapter dastr111 = new SqlDataAdapter(cmdstr111);
        DataTable dtstr111 = new DataTable();
        dastr111.Fill(dtstr111);
        if (dtstr111.Rows.Count == 0)
        {
            decimal c1 = Convert.ToDecimal(isdecimalornot(maxord.Text));
            decimal b1 = Convert.ToDecimal(isdecimalornot(minord.Text));
            if (b1 > c1)
            {

                Lblmsg.Visible = true;
                Lblmsg.Text = "Minimum order size must be less than Maximum order size";

            }

            else
            {
                try
                {
                    string delescid = "UPDATE  [FreeShippingRule] " +
                               "  SET [Name] = '" + rulename.Text + "' " +
           "  ,[MinOrdSize] = '" + minord.Text + "' " +
            " ,[MaxOrderSize] = '" + maxord.Text + "' " +
          "  ,[CountryId] ='" + ddlc.SelectedValue + "' " +
          "  ,[StateId] = '" + ddls.SelectedValue + "' " +
          "  ,[CityId] = '" + ddlct.SelectedValue + "',  " +
               "[Whid]='" + ddlware.SelectedValue + "' WHERE Id='" + rulemid.Text + "'  ";
                    SqlCommand cmdescid = new SqlCommand(delescid, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdescid.ExecuteNonQuery();
                    con.Close();
                    Lblmsg.Visible = true;
                    Lblmsg.Text = "Record updated successfully";

                    fillshipname();


                }
                catch
                {

                }
                grdFreeRule.EditIndex = -1;







                fillGrid();
                //fillGrid();
              //  fillshipname();
                FillGridESCFreeShipToInv();
                pnlEsc.Visible = false;
                txtMinQty.Text = "0";
                txtMinAmt.Text = "0";
                RadioButtonList1.SelectedIndex = 0;
            }

        }
        else
        {
            Label1.Visible = true;
            Label1.Text = "Record already exists";
        }

    }
    protected void FillGridESCFreeShipToInv()
    {
        string stresc = " SELECT     FreeShippingRule.Id, FreeShippingRule.Name, FreeShippingRule.MinOrdSize, FreeShippingRule.MaxOrderSize, FreeShippingRule.CountryId,  " +
                    "  FreeShippingRule.StateId, CountryMaster.CountryName,Case when(StateMasterTbl.StateName IS NULL) then 'All' else StateMasterTbl.StateName End as StateName ,Case when(CityMasterTbl.CityName IS NULL) then 'All' else CityMasterTbl.CityName End as CityName , FreeShippingToInvs.FreeShippingToInvs, FreeShippingToInvs.InventoryWHM_Id,  " +
                    "  FreeShippingToInvs.MinAmout, FreeShippingToInvs.MinQty,Case when(FreeShippingToInvs.StartDate IS NULL) then 'All' else Convert(nvarchar(10),FreeShippingToInvs.StartDate,101) End as StartDate,Case when (FreeShippingToInvs.EndDate IS NULL) then 'All' Else Convert(nvarchar(10), FreeShippingToInvs.EndDate,101)End as EndDate, FreeShippingToInvs.Effective,  " +
                    "  InventoryMaster.Name AS InvName, InventoruSubSubCategory.InventorySubSubName, InventorySubCategoryMaster.InventorySubCatName,  " +
                    "  InventoryCategoryMaster.InventoryCatName,  " +
                    "  InventoryCategoryMaster.InventoryCatName + ' : ' + InventorySubCategoryMaster.InventorySubCatName + ' : ' + InventoruSubSubCategory.InventorySubSubName + ' : ' " +
                    "   + InventoryMaster.Name AS CatSCatSSCatName " +
                      " FROM         InventorySubCategoryMaster LEFT OUTER JOIN " +
                    "  InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId RIGHT OUTER JOIN " +
                   "   InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID RIGHT OUTER JOIN " +
                   "   InventoryMaster ON InventoruSubSubCategory.InventorySubSubId = InventoryMaster.InventorySubSubId RIGHT OUTER JOIN " +
                   "   InventoryWarehouseMasterTbl ON InventoryMaster.InventoryMasterId = InventoryWarehouseMasterTbl.InventoryMasterId RIGHT OUTER JOIN " +
                   "   CountryMaster RIGHT OUTER JOIN " +
                   "   FreeShippingRule RIGHT OUTER JOIN " +
                   "   FreeShippingToInvs ON FreeShippingRule.Id = FreeShippingToInvs.FreeShippRuleId LEFT OUTER JOIN  CityMasterTbl ON FreeShippingRule.CityId = CityMasterTbl.CityId LEFT OUTER JOIN" +
                   "   StateMasterTbl ON FreeShippingRule.StateId = StateMasterTbl.StateId ON CountryMaster.CountryId = FreeShippingRule.CountryId ON  " +
                   "   InventoryWarehouseMasterTbl.InventoryWarehouseMasterId = FreeShippingToInvs.InventoryWHM_Id " +
                     " where FreeShippingRule.Id='" + ddlWarehouse.SelectedValue + "' order by CatSCatSSCatName ";
        SqlCommand cmdesc = new SqlCommand(stresc, con);
        SqlDataAdapter adpesc = new SqlDataAdapter(cmdesc);
        DataTable dtesc = new DataTable();
        adpesc.Fill(dtesc);
        grdCat0123.DataSource = dtesc;
        DataView myDataView = new DataView();
        myDataView = dtesc.DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }

        grdCat0123.DataBind();
        pnlEsc.Visible = true;

    }
    protected void grdCat0123_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
           // grdCat0123.SelectedIndex = Convert.ToInt32(e.CommandArgument);
           // ViewState["id"] = grdCat0123.SelectedDataKey.Value;
            //int dk0123 =Convert.ToInt32(grdCat0123.SelectedDataKey.Value);
            //if (System.Windows.Forms.MessageBox.Show("You Sure, You Want to Delete !", "Confirm Delete", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
            //{

            //string delescid = "DELETE FreeShippingToInvs      WHERE FreeShippingToInvs='"+dk0123+"' ";
            //SqlCommand cmdescid = new SqlCommand(delescid, con);
            //con.Open();
            //cmdescid.ExecuteNonQuery();
            //con.Close();
            //Lblmsg.Visible = true;
            //Lblmsg.Text = "Record Deleted Successfully";
           // FillGridESCFreeShipToInv();
           // ModalPopupExtender1.Show();
            //}

        }
    }
    protected void grdCat0123_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        grdCat0123.SelectedIndex = Convert.ToInt32(grdCat0123.DataKeys[e.RowIndex].Value.ToString());
        ViewState["id"] = grdCat0123.SelectedIndex;
        string delescid = "DELETE FreeShippingToInvs      WHERE FreeShippingToInvs='" + ViewState["id"] + "' ";
        SqlCommand cmdescid = new SqlCommand(delescid, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmdescid.ExecuteNonQuery();
        con.Close();
        Lblmsg.Visible = true;
        Lblmsg.Text = "Record deleted successfully";
        FillGridESCFreeShipToInv();
       
       
       
    }
    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/ShoppingCart/Admin/wzShippingMaster.aspx");
    }
    protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/ShoppingCart/Admin/wzGeneralShipSetting.aspx");
    }
    
    protected void grdFreeRule_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int dk = Convert.ToInt32(grdFreeRule.DataKeys[e.RowIndex].Value.ToString());

        string Delsttr = "Delete  FreeShippingRule where Id=" + dk + " ";
        SqlCommand cmdinsert1 = new SqlCommand(Delsttr, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmdinsert1.ExecuteNonQuery();
        con.Close();



        string Delsttr2 = "Delete  FreeShippingToInvs where FreeShippRuleId=" + dk + " ";
        SqlCommand cmdinsert12 = new SqlCommand(Delsttr2, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmdinsert12.ExecuteNonQuery();
        con.Close();

        

        Lblmsg.Visible = true;
        Lblmsg.Text = "Record deleted successfully";
        fillGrid();
        fillshipname();
        FillGridESCFreeShipToInv();
        //pnlRuleDetails.Visible = false;
        //pnlEsc.Visible = false;
    }
   
    
    protected void grdCat0123_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdCat0123.PageIndex = e.NewPageIndex;
        FillGridESCFreeShipToInv();
    }
    protected void grdCat0123_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        FillGridESCFreeShipToInv();
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
    protected void grdFreeRule_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        fillGrid();
    }
    protected void grdFreeRule_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 2;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Criteria for Free Shipping";
            HeaderCell.ColumnSpan = 5;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.BackColor = System.Drawing.Color.FromArgb(65, 98, 113);
            HeaderCell.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan =2;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;

          
            HeaderGridRow.Cells.Add(HeaderCell);
        
            grdFreeRule.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }

    }
    protected void chkstep1_CheckedChanged(object sender, EventArgs e)
    {
        if (chkstep1.Checked == true)
        {
            Panel2.Visible = true;
        }
        else
        {
            Panel2.Visible = false;
        }
    }

    protected void rddate_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rddate.SelectedIndex == 1)
        {
            pnldate.Visible = true;
        }
        else
        {
            txtStartDate.Text = "";
            txtEndDate.Text = "";
            pnldate.Visible = false;
        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        
        if (Button4.Text == "Printable Version")
        {
            string[] separator1 = new string[] { ":" };
            string[] strSplitArr1 = ddlWarehouse.SelectedItem.Text.Split(separator1, StringSplitOptions.RemoveEmptyEntries);
            // int i111 = Convert.ToInt32(strSplitArr1.Length);
            // if (i111 == 2)

            lblBusiness.Text = strSplitArr1[0].ToString();
            lblgptext.Text = "List of Free Shipping Rule to set Restriction " + strSplitArr1[1].ToString();
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");

            Button4.Text = "Hide Printable Version";
            Button3.Visible = true;
            
            if (grdCat0123.Columns[10].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                grdCat0123.Columns[10].Visible = false;
            }
        }
        else
        {

            Button4.Text = "Printable Version";
            Button3.Visible = false;
           
            if (ViewState["deleHide"] != null)
            {
                grdCat0123.Columns[10].Visible = true;
            }
        }
    }
}

