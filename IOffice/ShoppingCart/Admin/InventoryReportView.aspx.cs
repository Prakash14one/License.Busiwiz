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

public partial class ShoppingCart_Admin_InventoryReportView : System.Web.UI.Page
{
    // SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString"].ConnectionString);
    SqlConnection con;
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
        Label1.Visible = false;
        if (!IsPostBack)
        {
            Pagecontrol.dypcontrol(Page, page);
            FillddlStoreLocationDE();

           

            ViewState["sortOrder"] = "";
           
            if (RadioButtonList1.SelectedValue == "0")
            {
                pnlInvCat.Visible = true;
                pnlInvName.Visible = false;

                FillddlInvCat();
                ddlInvCat_SelectedIndexChanged(sender, e);

                btnSearchGo_Click(sender, e);
            }
            else if (RadioButtonList1.SelectedValue == "1")
            {
                pnlInvCat.Visible = false;
                pnlInvName.Visible = true;

            }

            else
            {
                pnlInvCat.Visible = false; ;
                pnlInvName.Visible = false;
               
            }

        }

    }
    protected void FillddlStoreLocationDE()
    {

        string stSL = "SELECT  WareHouseId,Name ,Address ,CurrencyId FROM  WareHouseMaster where comid='" + Session["comid"] + "' order by Name";
        SqlCommand cmdSL = new SqlCommand(stSL, con);
        SqlDataAdapter adpSL = new SqlDataAdapter(cmdSL);
        DataTable dtSL = new DataTable();
        adpSL.Fill(dtSL);

        ddlStoreLocationDe.DataSource = dtSL;
        ddlStoreLocationDe.DataTextField = "Name";
        ddlStoreLocationDe.DataValueField = "WareHouseId";

        ddlStoreLocationDe.DataBind();

        ddlStoreLocationDe.Items.Insert(0, "Select");
        ddlStoreLocationDe.Items[0].Value = "0";

        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlStoreLocationDe.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }



    }
    protected void FillddlInvCat()
    {
        

        ddlInvCat.Items.Clear();

        string str = "SELECT Distinct  InventoryCategoryMaster.InventeroyCatId,InventoryCategoryMaster.InventoryCatName FROM InventoryCategoryMaster inner join InventorySubCategoryMaster on InventorySubCategoryMaster.InventoryCategoryMasterId=InventoryCategoryMaster.InventeroyCatId inner join InventoruSubSubCategory on InventoruSubSubCategory.InventorySubCatID=InventorySubCategoryMaster.InventorySubCatId inner join InventoryMaster on InventoryMaster.InventorySubSubId=InventoruSubSubCategory.InventorySubSubId inner join InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.InventoryMasterId=InventoryMaster.InventoryMasterId inner join WareHouseMaster on WareHouseMaster.WareHouseId=InventoryWarehouseMasterTbl.WareHouseId  WHERE InventoryWarehouseMasterTbl.WareHouseId ='" + ddlStoreLocationDe.SelectedValue + "' and InventoryCategoryMaster.compid='" + Session["comid"] + "' and InventoryCategoryMaster.CatType IS Null  order by InventoryCatName";

        SqlCommand cmdcat = new SqlCommand(str, con);
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
    protected void ddlInvCat_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        //ddlInvSubCat.DataSource = null;
        //ddlInvSubCat.DataBind();
        //ddlInvSubCat.Items.Clear();
        //ddlInvSubSubCat.DataSource = null;
        //ddlInvSubSubCat.DataBind();
        //ddlInvSubSubCat.Items.Clear();
        //ddlInvName.DataSource = null;
        //ddlInvName.DataBind();
        //ddlInvName.Items.Clear();

        //if (Convert.ToInt32(ddlInvCat.SelectedIndex) > 0)
        //{
           
        //    {

        //        string strsubcat = "SELECT InventorySubCatId  ,InventorySubCatName ,InventoryCategoryMasterId  FROM InventorySubCategoryMaster " +
        //                        " where InventoryCategoryMasterId = " + Convert.ToInt32(ddlInvCat.SelectedValue) + " order by InventorySubCatName ";
        //        SqlCommand cmdsubcat = new SqlCommand(strsubcat, con);
        //        SqlDataAdapter adpsubcat = new SqlDataAdapter(cmdsubcat);
        //        DataTable dtsubcat = new DataTable();
        //        adpsubcat.Fill(dtsubcat);

        //        ddlInvSubCat.DataSource = dtsubcat;

        //        ddlInvSubCat.DataTextField = "InventorySubCatName";
        //        ddlInvSubCat.DataValueField = "InventorySubCatId";
        //        ddlInvSubCat.DataBind();
        //    }
        //}
        //else
        //{
        //    if (ddlInvCat.SelectedItem.ToString() == "All")
        //    {
        //        string strsubcat12 = "SELECT InventorySubCatId  ,InventorySubCatName ,InventoryCategoryMasterId  FROM InventorySubCategoryMaster order by InventorySubCatName ";
        //        //  " where InventoryCategoryMasterId = " + Convert.ToInt32(ddlInvCat.SelectedValue) + " ";
        //        SqlCommand cmdsubcat12 = new SqlCommand(strsubcat12, con);
        //        SqlDataAdapter adpsubcat12 = new SqlDataAdapter(cmdsubcat12);
        //        DataTable dtsubcat12 = new DataTable();
        //        adpsubcat12.Fill(dtsubcat12);

        //        ddlInvSubCat.DataSource = dtsubcat12;

        //        ddlInvSubCat.DataTextField = "InventorySubCatName";
        //        ddlInvSubCat.DataValueField = "InventorySubCatId";
        //        ddlInvSubCat.DataBind();

        //    }

        //    else
        //    {


        //        ddlInvSubCat.DataSource = null;
        //        ddlInvSubCat.DataBind();
        //    }
        //}
        //ddlInvSubCat.Items.Insert(0, "All");
        //ddlInvSubCat.Items[0].Value = "0";
        //ddlInvSubSubCat.DataSource = null;
        //ddlInvSubSubCat.DataBind();
        ////ddlInvSubCat.AutoPostBack = true;
        //ddlInvSubCat_SelectedIndexChanged(sender, e);


        ddlInvSubCat.Items.Clear();


        if (Convert.ToInt32(ddlInvCat.SelectedIndex) > 0)
        {


            string strsubcat = "SELECT InventorySubCatId  ,InventorySubCatName ,InventoryCategoryMasterId  FROM InventorySubCategoryMaster " +
                            " where InventoryCategoryMasterId = " + Convert.ToInt32(ddlInvCat.SelectedValue) + " order by InventorySubCatName ";
            SqlCommand cmdsubcat = new SqlCommand(strsubcat, con);
            SqlDataAdapter adpsubcat = new SqlDataAdapter(cmdsubcat);
            DataTable dtsubcat = new DataTable();
            adpsubcat.Fill(dtsubcat);

            ddlInvSubCat.DataSource = dtsubcat;
            ddlInvSubCat.DataTextField = "InventorySubCatName";
            ddlInvSubCat.DataValueField = "InventorySubCatId";
            ddlInvSubCat.DataBind();

            ddlInvSubCat.Items.Insert(0, "All");
            ddlInvSubCat.Items[0].Value = "0";


        }
        else
        {
            ddlInvSubCat.Items.Insert(0, "All");
            ddlInvSubCat.Items[0].Value = "0";

        }

        ddlInvSubCat_SelectedIndexChanged(sender, e);

        
    }
    protected void ddlInvSubCat_SelectedIndexChanged(object sender, EventArgs e)
    {
       
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


        // ddlInvSubSubCat.AutoPostBack = true;
        ddlInvSubSubCat_SelectedIndexChanged(sender, e);

    }
    protected void ddlInvSubSubCat_SelectedIndexChanged(object sender, EventArgs e)
    {


        ddlInvName.DataSource = null;
        ddlInvName.DataBind();
        ddlInvName.Items.Clear();
        if (Convert.ToInt32(ddlInvSubSubCat.SelectedIndex) > 0)
        {
            string strinvname = "SELECT InventoryMasterId ,Name ,InventoryDetailsId ,InventorySubSubId   ,ProductNo ,InventoryTypeId  FROM InventoryMaster " +
                            " where InventorySubSubId= " + Convert.ToInt32(ddlInvSubSubCat.SelectedValue) + " and MasterActiveStatus=1 and InventoryMaster.CatType IS Null  order by Name  ";
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

    //public DataSet fillddl2()
    //{
    //    SqlCommand cmd = new SqlCommand("Sp_Select_InventoruyCategory", con);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
    //    DataSet ds = new DataSet();
    //    adp.Fill(ds);

    //    return ds;

    //}
    //protected void ddlcategory_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //    if (ddlcategory.SelectedIndex == 0)
    //    {
    //        ddlSubSubCategory.Items.Clear();
    //        ddlSubCategory.Items.Clear();

    //        ddlSubCategory.Items.Insert(0, "--Select--");
    //        ddlSubCategory.SelectedIndex = 0;
    //        ddlSubSubCategory.Items.Insert(0, "--Select--");
    //        ddlSubSubCategory.SelectedIndex = 0;

    //    }

    //    else
    //    {

    //        ddlSubCategory.DataSource = (DataSet)fillddl3();
    //        ddlSubCategory.DataTextField = "InventorySubCatName";
    //        ddlSubCategory.DataValueField = "InventorySubCatId";
    //        ddlSubCategory.DataBind();

    //    }
    //}

    //public DataSet fillddl3()
    //{

    //    {
    //        SqlCommand cmd = new SqlCommand("Sp_Select_SubCategory", con);
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.Parameters.AddWithValue("@InventoryCategoryMasterId", ddlcategory.SelectedValue);
    //        SqlDataAdapter adp = new SqlDataAdapter(cmd);
    //        DataSet ds = new DataSet();
    //        adp.Fill(ds);

    //        int h = ds.Tables[0].Rows.Count;
    //        int i = 0;
    //        string a, b;

    //        DataSet ds1 = new DataSet();
    //        //  for (int i1 = 0; i1 < h; i1++)
    //        //{
    //        foreach (DataRow row in ds.Tables[0].Rows)
    //        {
    //            ViewState["a"] = row["InventorySubCatId"].ToString();
    //            SqlDataAdapter nilesh = new SqlDataAdapter("SELECT InventorySubSubId,InventorySubSubName,InventorySubCatID      FROM InventoruSubSubCategory where InventorySubCatID=" + ViewState["a"] + "", con);
    //            nilesh.Fill(ds1, "Nilesh");
    //            ds1 = (DataSet)ds1;

    //        }


    //        ddlSubSubCategory.DataSource = ds1;
    //        ddlSubSubCategory.DataTextField = "InventorySubSubName";
    //        ddlSubSubCategory.DataValueField = "InventorySubSubId";
    //        ddlSubSubCategory.DataBind();

    //        DataTable ds5 = new DataTable();
    //        foreach (DataRow row in ds1.Tables[0].Rows)
    //        {
    //            ViewState["a1"] = row["InventorySubSubId"].ToString();
    //            SqlDataAdapter nil = new SqlDataAdapter("SELECT     InventoryMaster.Name, InventoryMaster.ProductNo, InventoryMaster.InventoryMasterId, InventoryMaster.InventorySubSubId, "+
    //                  " InventoryImgMaster.Active, InventoryImgMaster.Newarrival, InventoryImgMaster.Promotion, InventoryImgMaster.FutureProduct "+
    //                " FROM InventoryMaster INNER JOIN "+
    //                "  InventoryImgMaster ON InventoryMaster.InventoryMasterId = InventoryImgMaster.InventoryMasterId " +
    //            " WHERE (InventoryMaster.InventorySubSubId = " + Convert.ToInt32(ViewState["a1"]) + ")", con);
    //            nil.Fill(ds5);

    //        }
    //        GridView1.DataSource = ds5;
    //        GridView1.DataBind();
    //        Session["data"] = ds5;
    //        return ds;
    //    }
    //}
    //protected void ddlSubCategory_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlcategory.SelectedIndex == 0)
    //    {
    //        ddlSubSubCategory.Items.Insert(0, "--Select--");



    //    }

    //    else
    //    {
    //        ddlSubSubCategory.DataSource = (DataSet)fillddl4();
    //        ddlSubSubCategory.DataTextField = "InventorySubSubName";
    //        ddlSubSubCategory.DataValueField = "InventorySubSubId";
    //        ddlSubSubCategory.DataBind();
    //    }
    //}

    //public DataSet fillddl4()
    //{
    //    {

    //        string str1 = "SELECT InventorySubSubId,InventorySubSubName,InventorySubCatID " +
    //            "  FROM InventoruSubSubCategory where InventorySubCatID=" + ddlSubCategory.SelectedValue + " ";
    //        SqlCommand cmd = new SqlCommand(str1, con);
    //        SqlDataAdapter adp = new SqlDataAdapter(cmd);
    //        DataSet ds1 = new DataSet();
    //        adp.Fill(ds1);

    //        DataTable ds99 = new DataTable();
    //        foreach (DataRow row in ds1.Tables[0].Rows)
    //        {
    //            ViewState["a11"] = row["InventorySubSubId"].ToString();
    //            SqlDataAdapter nil = new SqlDataAdapter("SELECT     InventoryMaster.Name, InventoryMaster.ProductNo, InventoryMaster.InventoryMasterId, InventoryMaster.InventorySubSubId, "+
    //                  " InventoryImgMaster.Active, InventoryImgMaster.Newarrival, InventoryImgMaster.Promotion, InventoryImgMaster.FutureProduct "+
    //                " FROM InventoryMaster INNER JOIN "+
    //                "  InventoryImgMaster ON InventoryMaster.InventoryMasterId = InventoryImgMaster.InventoryMasterId " +
    //            " WHERE (InventoryMaster.InventorySubSubId = " + ViewState["a11"] + ")", con);
    //            nil.Fill(ds99);
    //            ds99 = (DataTable)ds99;
    //        }
    //        GridView1.DataSource = ds99;
    //        GridView1.DataBind();
    //        Session["data"] = ds99;

    //        return ds1;

    //    }


    //}
    //protected void ddlSubSubCategory_SelectedIndexChanged1(object sender, EventArgs e)
    //{
    //    fill5();
    //}
    //public DataTable fill5()
    //{
    //    SqlCommand cmd = new SqlCommand("SELECT     InventoryMaster.Name, InventoryMaster.ProductNo, InventoryMaster.InventoryMasterId, InventoryMaster.InventorySubSubId, " +
    //                  " InventoryImgMaster.Active, InventoryImgMaster.Newarrival, InventoryImgMaster.Promotion, InventoryImgMaster.FutureProduct " +
    //                " FROM InventoryMaster INNER JOIN " +
    //                "  InventoryImgMaster ON InventoryMaster.InventoryMasterId = InventoryImgMaster.InventoryMasterId " +
    //            " WHERE (InventoryMaster.InventorySubSubId = '" + ddlSubSubCategory.SelectedValue + "') ", con);

    //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
    //    DataTable ds = new DataTable();
    //    adp.Fill(ds);

    //    GridView1.DataSource = ds;
    //    GridView1.DataBind();
    //    Session["data"] = ds;
    //    return ds;


    //}

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "Sort")
        {
            return;
        }
        if (e.CommandName == "ed")
        {
            //**************chnages codes

           int indx = Convert.ToInt32(e.CommandArgument.ToString());

          Label iwhmid = (Label)(GridView1.Rows[indx].FindControl("lblInventoryWarehouseMasterId"));
          int InvWhMid = Convert.ToInt32(iwhmid.Text);
          Response.Redirect("Inupdate.aspx?InventoryWarehouseMasterId=" + InvWhMid + "");
        //  string te = "Inupdate.aspx?InventoryWarehouseMasterId=" + InvWhMid + "";
          // ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);



//*************************
          //  int indx = Convert.ToInt32(e.CommandArgument.ToString());

          //  Label iwhmid = (Label)(GridView1.Rows[indx].FindControl("lblInventoryWarehouseMasterId"));
          //  int InvWhMid = Convert.ToInt32(iwhmid.Text);
          ////  Response.Redirect("InventoryUpdate.aspx?InventoryWarehouseMasterId=" + InvWhMid + "");
          //  ////****************************

           // ModalPopupExtender142422.Show();


        }
       





      
     



    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataSource = Session["data"];
        GridView1.DataBind();
    }
  
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedValue == "0")
        {
            pnlInvCat.Visible = true;
            pnlInvName.Visible = false;

            FillddlInvCat();
            ddlInvCat_SelectedIndexChanged(sender, e);
            //btnSearchGo_Click(sender, e);
        }
        else if (RadioButtonList1.SelectedValue == "1")
        {
            pnlInvCat.Visible = false;
            pnlInvName.Visible = true;

        }

        else
        {
            pnlInvCat.Visible = false; ;
            pnlInvName.Visible = false;
            //pnlInvBarcode.Visible = false;
        }
    }
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        lblcomname.Text = Session["Cname"].ToString();
        lblBusiness.Text = ddlStoreLocationDe.SelectedItem.Text;

        lblInvName.Visible = false;
        DataTable dtinvids = new DataTable();

        if (RadioButtonList1.SelectedValue == "0")
        {
            lblinvcateg.Text = ddlInvCat.SelectedItem.Text;
            lblinvsubcate.Text = ddlInvSubCat.SelectedItem.Text;
            lblinvsubsubcate.Text = ddlInvSubSubCat.SelectedItem.Text;
            lblinvprod.Text = ddlInvName.SelectedItem.Text;
            lblinvstat.Text = ddlstatus.SelectedItem.Text;

            dtinvids = (DataTable)(SeachByCat());
            Panel4.Visible = false;
            Panel3.Visible = true;
        }
        else if (RadioButtonList1.SelectedValue == "1")
        {
            Label15.Text = txtSearchInvName.Text;
            lblinvstatus123.Text = ddlstatus.SelectedItem.Text;

            if (txtSearchInvName.Text.Length > 0 )
            {
                dtinvids = (DataTable)(SearchByName());
                
                    Panel4.Visible=true;
                    Panel3.Visible = false;
            }
            else
            {
                lblInvName.Visible = true;
                lblInvName.Text = "plese input inventory name ";
            }
        }

        else
        {

        }
        if (dtinvids.Rows.Count > 0)
        {

            GridView1.DataSource = dtinvids;
            DataView myDataView = new DataView();
            myDataView = dtinvids.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }


            GridView1.DataBind();

            string openingdate = "select StartDate,EndDate from ReportPeriod where Compid='" + Session["Comid"].ToString() + "' and Whid='" + Convert.ToInt32(ddlStoreLocationDe.SelectedValue) + "' and Active='1'";
            SqlCommand cmd22221 = new SqlCommand(openingdate, con);
            SqlDataAdapter adp22221 = new SqlDataAdapter(cmd22221);
            DataTable ds112221 = new DataTable();
            adp22221.Fill(ds112221);
            if (ds112221.Rows.Count > 0)
            {
              

                ViewState["StartDate"] = Convert.ToDateTime(ds112221.Rows[0]["StartDate"].ToString());
                ViewState["EndDate"] = Convert.ToDateTime(ds112221.Rows[0]["EndDate"].ToString());

                foreach (GridViewRow gdr in GridView1.Rows)
                {


                    Label lblInvWHMasterId = (Label)gdr.FindControl("lblInvWHMasterId");
                    Label lblQtyOnHand = (Label)gdr.FindControl("lblQtyOnHand");


                    double TotalAvgBal = 0;
                    double AvgQtyAvail = 0;
                    double AvgCostFinal = 0;
                    double finaltotalqty = 0;


                    string Avgcost = "select * from InventoryWarehouseMasterAvgCostTbl   where InventoryWarehouseMasterAvgCostTbl.DateUpdated between '" + ViewState["StartDate"].ToString() + "' and '" + ViewState["EndDate"].ToString() + "' and InventoryWarehouseMasterAvgCostTbl.InvWMasterId='" + Convert.ToInt32(lblInvWHMasterId.Text) + "' order by DateUpdated,IWMAvgCostId  ";


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



                            }
                            if (Convert.ToString(dtr["Qty"]) != "")
                            {
                                totalqtycount = Convert.ToDouble(dtr["Qty"].ToString());
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
                            finaltotalqty += totalqtycount;

                            if (finaltotalqty == 0)
                            {
                                avgqty = 0;
                                avgrate = 0;
                                AvgQtyAvail = 0;
                                TotalAvgBalsub = 0;
                                TotalAvgBal = 0;
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
                            AvgCostFinal = TotalAvgBal / AvgQtyAvail;
                            AvgCostFinal = Math.Round(AvgCostFinal, 2);
                        }
                    }


                    lblQtyOnHand.Text = finaltotalqty.ToString();


                }
            }
            Session["data"] = dtinvids;

        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
           
        }
    }


    public DataTable SeachByCat()
    {
        
        string mainStringCat = "";



        string strinv = "SELECT InventoryWarehouseMasterTbl.InventoryWarehouseMasterId, Convert(nvarchar(50),InventoryWarehouseMasterTbl.Weight )+ ' / ' + UnitTypeMaster.Name as Weight ,UnitTypeMaster.Name, WareHouseMaster.WareHouseId,Left(WareHouseMaster.Name,30) as wname ,  InventoryMaster.InventoryMasterId, InventoryDetails.Inventory_Details_Id, InventoruSubSubCategory.InventorySubSubId,      InventoruSubSubCategory.InventorySubSubName, InventorySubCategoryMaster.InventorySubCatId, InventorySubCategoryMaster.InventorySubCatName,       InventoryCategoryMaster.InventeroyCatId, InventoryCategoryMaster.InventoryCatName, Left(InventoryMaster.Name,30) as InventoryMasterName, InventoryMaster.MasterActiveStatus,case when (InventoryMaster.MasterActiveStatus='1') then 'Active' else 'Inactive' End as Statuslabel,      InventoryMaster.ProductNo,       Left(InventoryCategoryMaster.InventoryCatName,15) + ' : ' + Left(InventorySubCategoryMaster.InventorySubCatName,15) + ' :  ' + Left(InventoruSubSubCategory.InventorySubSubName,15)      AS CateAndName, InventoryBarcodeMaster.Barcode, InventoryDetails.Description, InventoryImgMaster.Newarrival, InventoryImgMaster.Promotion,       InventoryImgMaster.FutureProduct  FROM         InventoryMaster LEFT OUTER JOIN    InventoryImgMaster ON InventoryMaster.InventoryMasterId = InventoryImgMaster.InventoryMasterId LEFT OUTER JOIN      InventoryDetails ON InventoryMaster.InventoryDetailsId = InventoryDetails.Inventory_Details_Id    LEFT OUTER JOIN      InventoryBarcodeMaster ON InventoryMaster.InventoryMasterId = InventoryBarcodeMaster.InventoryMaster_id LEFT OUTER JOIN      InventorySubCategoryMaster LEFT OUTER JOIN      InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId RIGHT OUTER JOIN      InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID ON InventoryMaster.InventorySubSubId = InventoruSubSubCategory.InventorySubSubId Left Outer Join InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.InventoryMasterId=InventoryMaster.InventoryMasterId inner join WareHouseMaster on WareHouseMaster.WareHouseId=InventoryWarehouseMasterTbl.WareHouseId  inner join UnitTypeMaster on UnitTypeMaster.UnitTypeId = InventoryDetails.UnitTypeId where  " + "  InventoryCategoryMaster.compid='" + Session["comid"] + "' and  InventoryWarehouseMasterTbl.WareHouseId='" + ddlStoreLocationDe.SelectedValue + "' and InventoryMaster.CatType IS Null ";
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
                        strInvId = "and InventoryMaster.InventoryMasterId=" + Convert.ToInt32(ddlInvName.SelectedValue) + " ";
                        // strInvId = "AND  InventoryMaster.InventoryMasterId=" + Convert.ToInt32(ddlInvName.SelectedValue) + " ";

                    }
                    else
                    {
                        // strInvsubsubCatId = "where InventoruSubSubCategory.InventorySubSubId=" + Convert.ToInt32(ddlInvSubSubCat.SelectedValue) + "";
                        strInvsubsubCatId = "and InventoruSubSubCategory.InventorySubSubId=" + Convert.ToInt32(ddlInvSubSubCat.SelectedValue) + "";
                    }
                }
                else
                {
                    // strInvsubcatid = "where InventorySubCategoryMaster.InventorySubCatId = " + Convert.ToInt32(ddlInvSubCat.SelectedValue) + " ";
                    strInvsubcatid = "and InventorySubCategoryMaster.InventorySubCatId = " + Convert.ToInt32(ddlInvSubCat.SelectedValue) + " ";

                }

            }
            else
            {
                //strInvCatid = "where InventoryCategoryMaster.InventeroyCatId =" + Convert.ToInt32(ddlInvCat.SelectedValue) + " ";
                strInvCatid = " and InventoryCategoryMaster.InventeroyCatId =" + Convert.ToInt32(ddlInvCat.SelectedValue) + " ";

                //strInvId = "where  InventoryMaster.InventoryMasterId=" + Convert.ToInt32(ddlInvName.SelectedValue) + " ";

            }
        }
        else
        {
            //strInvCatid = "where InventoryCategoryMaster.InventeroyCatId =" + Convert.ToInt32(ddlInvCat.SelectedValue) + " ";

            //    string mainStringCat = "";



        }


        if (ddlstatus.SelectedItem.Text == "All")
        {
            mainStringCat = strinv + strInvId + strInvsubsubCatId + strInvsubcatid + strInvCatid + "  order by InventoryCategoryMaster.InventoryCatName, InventorySubCategoryMaster.InventorySubCatName,InventoruSubSubCategory.InventorySubSubName, InventoryMaster.Name";
        }
        else if (ddlstatus.SelectedItem.Text == "Active")
        {
            mainStringCat = strinv + strInvId + strInvsubsubCatId + strInvsubcatid + strInvCatid + " and InventoryMaster.MasterActiveStatus=1 order by InventoryCategoryMaster.InventoryCatName, InventorySubCategoryMaster.InventorySubCatName,InventoruSubSubCategory.InventorySubSubName, InventoryMaster.Name";
            //mainStringCat = strinv + strInvId + strInvsubsubCatId + strInvsubcatid + strInvCatid + " and InventoryMaster.MasterActiveStatus=1 order by InventoryMaster.InventoryMasterId";//+ strInvBySerchId // InventoryMaster.Name ";
        }
        else if (ddlstatus.SelectedItem.Text == "Inactive")
        {
            mainStringCat = strinv + strInvId + strInvsubsubCatId + strInvsubcatid + strInvCatid + " and InventoryMaster.MasterActiveStatus=0 order by InventoryCategoryMaster.InventoryCatName, InventorySubCategoryMaster.InventorySubCatName,InventoruSubSubCategory.InventorySubSubName, InventoryMaster.Name";
        }
        //********** changes codes

        if (ddlInvCat.SelectedIndex == 0 && ddlstatus.SelectedItem.Text == "All")
        {
            mainStringCat = strinv + strInvId + strInvsubsubCatId + strInvsubcatid + strInvCatid + "  order by InventoryCategoryMaster.InventoryCatName, InventorySubCategoryMaster.InventorySubCatName,InventoruSubSubCategory.InventorySubSubName, InventoryMaster.Name";
        }
        else if (ddlInvCat.SelectedIndex == 0 && ddlstatus.SelectedItem.Text == "Active")
        {
            mainStringCat = strinv + strInvId + strInvsubsubCatId + strInvsubcatid + strInvCatid + " and InventoryMaster.MasterActiveStatus=1 order by InventoryCategoryMaster.InventoryCatName, InventorySubCategoryMaster.InventorySubCatName,InventoruSubSubCategory.InventorySubSubName, InventoryMaster.Name";
        }
        else if (ddlInvCat.SelectedIndex == 0 && ddlstatus.SelectedItem.Text == "Inactive")
        {
            mainStringCat = strinv + strInvId + strInvsubsubCatId + strInvsubcatid + strInvCatid + " and InventoryMaster.MasterActiveStatus=0 order by InventoryCategoryMaster.InventoryCatName, InventorySubCategoryMaster.InventorySubCatName,InventoruSubSubCategory.InventorySubSubName, InventoryMaster.Name";
        }




        SqlCommand cmdcat = new SqlCommand(mainStringCat, con);
        SqlDataAdapter adpcat = new SqlDataAdapter(cmdcat);
        DataTable dtcat = new DataTable();
        adpcat.Fill(dtcat);



        return dtcat;

    }
    public DataTable SearchByName()
    {
       

       
        
        string str = "";
        if (ddlstatus.SelectedItem.Text == "All")
        {
            str = "";
        }
        else if (ddlstatus.SelectedItem.Text == "Active")
        {
            str = "and InventoryMaster.MasterActiveStatus=1";
        }
        else if (ddlstatus.SelectedItem.Text == "Inactive")
        {
            str = "and InventoryMaster.MasterActiveStatus=0";
        }

        DataTable dtinvname = new DataTable();

        string str23invname = " SELECT    InventoryWarehouseMasterTbl.InventoryWarehouseMasterId, InventoryMaster.InventoryMasterId,WareHouseMaster.WareHouseId,Left(WareHouseMaster.Name,25) as wname , InventoryDetails.Inventory_Details_Id,Convert(nvarchar(50),InventoryWarehouseMasterTbl.Weight )+ ' / ' + UnitTypeMaster.Name as Weight,UnitTypeMaster.Name, InventoruSubSubCategory.InventorySubSubId, InventoruSubSubCategory.InventorySubSubName, InventorySubCategoryMaster.InventorySubCatId, InventorySubCategoryMaster.InventorySubCatName,       InventoryCategoryMaster.InventeroyCatId, InventoryCategoryMaster.InventoryCatName, Left(InventoryMaster.Name,30) as InventoyrName,InventoryMaster.Name as InventoryMasterName, InventoryMaster.MasterActiveStatus,case when (InventoryMaster.MasterActiveStatus='1') then 'Active' else 'Inactive' End as Statuslabel,      InventoryMaster.ProductNo,       Left(InventoryCategoryMaster.InventoryCatName,15) + ' : ' + Left(InventorySubCategoryMaster.InventorySubCatName,15) + ' :  ' + Left(InventoruSubSubCategory.InventorySubSubName,15 ) AS CateAndName, InventoryBarcodeMaster.Barcode, InventoryDetails.Description, InventoryImgMaster.Newarrival, InventoryImgMaster.Promotion,       InventoryImgMaster.FutureProduct  FROM         InventoryMaster LEFT OUTER JOIN    InventoryImgMaster ON InventoryMaster.InventoryMasterId = InventoryImgMaster.InventoryMasterId LEFT OUTER JOIN      InventoryDetails ON InventoryMaster.InventoryDetailsId = InventoryDetails.Inventory_Details_Id Left Outer Join InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.InventoryMasterId=InventoryMaster.InventoryMasterId inner join WareHouseMaster on WareHouseMaster.WareHouseId=InventoryWarehouseMasterTbl.WareHouseId      LEFT OUTER JOIN   UnitTypeMaster on UnitTypeMaster.UnitTypeId= InventoryDetails.UnitTypeId   LEFT OUTER JOIN      InventoryBarcodeMaster ON InventoryMaster.InventoryMasterId = InventoryBarcodeMaster.InventoryMaster_id LEFT OUTER JOIN      InventorySubCategoryMaster LEFT OUTER JOIN      InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId RIGHT OUTER JOIN      InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID ON       InventoryMaster.InventorySubSubId = InventoruSubSubCategory.InventorySubSubId " +
             "   WHERE    ( (InventoryMaster.Name like '%" + txtSearchInvName.Text.Replace("'", "''") + "%') " +
                     " or (InventoryBarcodeMaster.Barcode= '" + txtSearchInvName.Text + "') or (InventoryMaster.ProductNo='" + txtSearchInvName.Text + "') ) and  WareHouseMaster.WareHouseId='" + ddlStoreLocationDe.SelectedValue + "' and  InventoryCategoryMaster.compid='" + Session["comid"] + "' " + str + " and InventoryMaster.CatType IS Null order by InventoryCategoryMaster.InventoryCatName, InventorySubCategoryMaster.InventorySubCatName,InventoruSubSubCategory.InventorySubSubName, InventoryMaster.Name  ";
        SqlCommand cmdinvname = new SqlCommand(str23invname, con);
        SqlDataAdapter adpinvname = new SqlDataAdapter(cmdinvname);

        adpinvname.Fill(dtinvname);



        return dtinvname;



    }

   
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        //hdnsortExp.Value = e.SortExpression.ToString();
        //hdnsortDir.Value = sortOrder; // sortOrder;
        ////SelectDocumentforApproval();
        //EventArgs e34 = new EventArgs();
        //btnSearchGo_Click(sender, e34);



        {
            ViewState["sortexpression"] = e.SortExpression;
            if (ViewState["sortdirection"] == null)
            {
                ViewState["sortdirection"] = "asc";
            }
            else
            {
                if (ViewState["sortdirection"].ToString() == "asc")
                {
                    ViewState["sortdirection"] = "desc";
                }
                else
                {
                    ViewState["sortdirection"] = "asc";
                }
            }
            DataTable dtable = new DataTable();
            dtable = (DataTable)Session["data"];
            DataView dview = dtable.DefaultView;

            if (ViewState["sortexpression"] != null)
            {
                dview.Sort = ViewState["sortexpression"].ToString() + " " + ViewState["sortdirection"].ToString();
            }
            GridView1.DataSource = dview;
            GridView1.DataBind();
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


    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dtinvids = new DataTable();

        if (RadioButtonList1.SelectedValue == "0")
        {
            dtinvids = (DataTable)(SeachByCat());
        }
        else if (RadioButtonList1.SelectedValue == "1")
        {
            if (txtSearchInvName.Text.Length > 1 || txtBarcode.Text.Length > 0 || txtProductNo.Text.Length > 0)
            {
                dtinvids = (DataTable)(SearchByName());
            }
            else
            {
                lblInvName.Visible = true;
                lblInvName.Text = "plese input InvntoryName atleast";
            }
        }

        else
        {

        }
        if (dtinvids.Rows.Count > 0)
        {

            GridView1.DataSource = dtinvids;
            DataView myDataView = new DataView();
            myDataView = dtinvids.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }


            GridView1.DataBind();
            Session["data"] = dtinvids;

        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
           
        }
    }
    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {
        txtBarcode.Text = "";
        // txtCountReason.Text = "";
        ddlInvCat.SelectedIndex = 0;
        ddlInvSubCat.SelectedIndex = -1;
        ddlInvSubSubCat.SelectedIndex = -1;
        ddlInvName.SelectedIndex = -1;
        //
        //
        //t//txtDate
        //txtlastdcno.Text = "";
        //txtLastGoodReNo.Text = "";
        //txtname.Text = "";
        txtProductNo.Text = "";
        txtSearchInvName.Text = "";
        //ddlInvCat.SelectedIndex = 0;
        //ddl
        //Panel1.Visible = false;
        //grdInvMasters.DataSource = null;
        //grdInvMasters.DataBind();
        //object sender = new object();
        //EventArgs e = new EventArgs();
        ddlInvCat_SelectedIndexChanged(sender, e);
        //FillddlInvCat();
        GridView1.DataSource = null;
        GridView1.DataBind();
       
    }
    protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
protected void  ddlInvName_SelectedIndexChanged(object sender, EventArgs e)
{

}

protected void ddlinventorysubsubid_SelectedIndexChanged(object sender, EventArgs e)
{

}
protected void ddllbs_SelectedIndexChanged(object sender, EventArgs e)
{

}
protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
{

}
protected void imgbtnSubmit_Click(object sender, ImageClickEventArgs e)
{

}
protected void imgbtnCancel_Click(object sender, ImageClickEventArgs e)
{

}
protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
{

}
protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
{
 //   Response.Redirect("Inupdate.asp");
}
protected void ddlStoreLocationDe_SelectedIndexChanged(object sender, EventArgs e)
{
    if (RadioButtonList1.SelectedValue == "0")
    {
        FillddlInvCat();
        ddlInvCat_SelectedIndexChanged(sender, e);
    }
}
protected void Button1_Click(object sender, EventArgs e)
{
    if (Button1.Text == "Printable Version")
    {
        Button1.Text = "Hide Printable Version";
        Button2.Visible = true;

        if (GridView1.Columns[10].Visible == true)
        {
            ViewState["editHide"] = "tt";
            GridView1.Columns[10].Visible = false;
        }
    }
    else
    {
        Button1.Text = "Printable Version";
        Button2.Visible = false;

        if (ViewState["editHide"] != null)
        {
            GridView1.Columns[10].Visible = true;
        }
    }
}
}







