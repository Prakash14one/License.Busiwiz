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

public partial class ApplyVolumeDiscount : System.Web.UI.Page
{
    string compid;
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

        compid = Session["comid"].ToString();
        Page.Title = pg.getPageTitle(page);
     
        Label1.Visible = false;
        if (!IsPostBack)
        {
            Pagecontrol.dypcontrol(Page, page);
            FillddlInvCat();
            DateTime fristdayofmonth = new DateTime(DateTime.Now.Year,1, 1);
            txtfilsdate.Text = fristdayofmonth.ToShortDateString();
            DateTime lastdaymonth = fristdayofmonth.AddMonths(12).AddDays(-1);
            txtfiledate.Text = lastdaymonth.ToShortDateString();
            string strwh = "SELECT WareHouseId,Name,Address,CurrencyId FROM WareHouseMaster where comid='" + compid + "' Order by Name";
            SqlCommand cmdwh = new SqlCommand(strwh, con);
            SqlDataAdapter adpwh = new SqlDataAdapter(cmdwh);
            DataTable dtwh = new DataTable();
            adpwh.Fill(dtwh);

            ddlWarehouse.DataSource = dtwh;
            ddlWarehouse.DataTextField = "Name";
            ddlWarehouse.DataValueField = "WareHouseId";

            ddlWarehouse.DataBind();
            //object sender12=new object();
            //EventArgs e12 = new EventArgs();
            ddlWarehouse_SelectedIndexChanged(sender, e);
            ddlInvSubCat.Items.Insert(0, "All");
            ddlInvSubCat.Items[0].Value = "0";
            ddlInvSubSubCat.Items.Insert(0, "All");
            ddlInvSubSubCat.Items[0].Value = "0";
            ddlInvName.Items.Insert(0, "All");
            ddlInvName.Items[0].Value = "0";
            FillGridVolume("");
           // imgbtnGo_Click(sender, e);
            //DataTable dtivs = (DataTable)SeachByCat();
            //if (dtivs.Rows.Count > 0)
            //{
            //    GridView1.DataSource = dtivs;
            //    GridView1.DataBind();
            //    if (ImageButton1.Visible == false)
            //    {
            //        ImageButton1.Visible = true;
            //    }

            //}
            //else
            //{
            //    if (ImageButton1.Visible == true)
            //    {
            //        ImageButton1.Visible = false;
            //    }
            //}
            
            

        }
    }



    protected void Button1_Click(object sender, EventArgs e)
    {
        int i = 0;
        Label1.Text = "";
        lblerrormsg.Text = "";
        if (ViewState["Schemaid"] != null)
        {
            int addc = 0;
            int upc = 0;
            int del = 0;
            int all = 0;
            foreach (GridViewRow dgi in this.GridView1.Rows)
            {
                Label lblnetsales = (Label)dgi.FindControl("lblnetsales");
                Label lblsalerror = (Label)dgi.FindControl("lblsalerror");
                CheckBox chkapl = (CheckBox)dgi.FindControl("CheckBox1");
                if (chkapl.Checked == true)
                {
                    if (Convert.ToDecimal(lblnetsales.Text) > 0)
                    {
                        lblsalerror.Visible = false;
                    }
                    else
                    {
                        all += 1;
                        lblsalerror.Visible = true;
                    }
                }
            }
            if (all == 0)
            {
                foreach (GridViewRow dgi in this.GridView1.Rows)
                {

                    CheckBox chkvoldish = (CheckBox)dgi.FindControl("chkvoldish");

                    CheckBox CheckBox1 = (CheckBox)dgi.FindControl("CheckBox1");

                    if (CheckBox1.Checked == true || chkvoldish.Checked == true)
                    {


                        if (((CheckBox)dgi.FindControl("CheckBox1")).Enabled == true)
                        {
                            ViewState["ProcessingId"] = (int)GridView1.DataKeys[i].Value;
                            CheckBox chkonline = (CheckBox)dgi.FindControl("chkonapp");
                            CheckBox chkretail = (CheckBox)dgi.FindControl("chkretailap");

                            Label lblvoldesdetail = (Label)dgi.FindControl("lblvoldesdetail");
                            Label lblpromdetailid = (Label)dgi.FindControl("lblpromdetailid");
                            // CheckBox chkvoldis = (CheckBox)dgi.FindControl("chkvoldis");

                            CheckBox chkpromoremove = (CheckBox)dgi.FindControl("chkpromoremove");
                            CheckBox chkpromoh = (CheckBox)dgi.FindControl("chkpromoh");

                            string str5 = "select * from InventoryVolumeSchemeDetail where SchemeID='" + Convert.ToInt32(ViewState["Schemaid"]) + "'  and InventoryWHM_Id='" + ViewState["ProcessingId"] + "'";
                            SqlCommand cmd5 = new SqlCommand(str5, con);
                            SqlDataAdapter nil = new SqlDataAdapter(cmd5);
                            DataTable ds5 = new DataTable();
                            nil.Fill(ds5);
                            string str4 = "";

                            if (ds5.Rows.Count == 0)
                            {
                                str4 = "Insert Into InventoryVolumeSchemeDetail(SchemeID,InventoryWHM_Id,ApplyonlineSales,ApplyRetailSales) values  (" + Convert.ToInt32(ViewState["Schemaid"]) + "," + ViewState["ProcessingId"] + ",'" + chkonline.Checked + "','" + chkretail.Checked + "')";

                                addc = addc + 1;


                            }
                            else
                            {
                                if (chkvoldish.Checked == true)
                                {
                                    if (CheckBox1.Checked == false)
                                    {
                                        str4 = "Delete from  InventoryVolumeSchemeDetail where SchemeDetailID='" + lblvoldesdetail.Text + "'";
                                        del = del + 1;
                                    }
                                    else
                                    {
                                        str4 = "Update InventoryVolumeSchemeDetail Set SchemeID='" + Convert.ToInt32(ViewState["Schemaid"]) + "',ApplyonlineSales='" + chkonline.Checked + "',ApplyRetailSales='" + chkretail.Checked + "' where InventoryWHM_Id='" + ViewState["ProcessingId"] + "'";
                                        upc = upc + 1;
                                    }
                                }

                            }
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }

                            SqlCommand cmd33 = new SqlCommand(str4, con);
                            cmd33.ExecuteNonQuery();
                            con.Close();

                            if (chkpromoh.Checked == true)
                            {
                                if (chkpromoremove.Checked == false)
                                {
                                    str4 = "Delete from  PromotionDiscountDetail where PromotionDiscountDetail='" + lblpromdetailid.Text + "'";
                                    if (con.State.ToString() != "Open")
                                    {
                                        con.Open();
                                    }

                                    SqlCommand cmd332 = new SqlCommand(str4, con);
                                    cmd332.ExecuteNonQuery();
                                    con.Close();
                                }
                            }

                            Label1.Visible = true;

                            //Label1.Text = upc.ToString() + " Record Updated," + addc.ToString() + " Record Inserted  and " + del.ToString() + " Record Deleted";

                        }
                    }
                    else
                    {

                       
                    }
                    i++;

                }
                if (addc == 0 && upc == 0 && del==0)
                {
                    Label1.Visible = true;
                    Label1.Text = "Please Check the Checkbox";
                }
                else
                {
                    //ddlWarehouse.SelectedIndex = 0;
                    //ddlWarehouse_SelectedIndexChanged(sender, e);
                    Panel1.Visible = false;

                }
                if (upc > 0)
                {
                    Label1.Text = upc.ToString() + " records have been updated";
                }
                if (addc > 0)
                {
                    if (upc > 0)
                    {
                        Label1.Text = Label1.Text + ", ";
                    }
                    Label1.Text = Label1.Text + addc.ToString() + " records have been inserted";
                }
                if (del > 0)
                {
                    if (upc > 0 || addc > 0)
                    {
                        Label1.Text = Label1.Text + ", ";
                    }
                    Label1.Text = Label1.Text + del.ToString() + " records have been deleted";

                }
            }
            else
            {
                lblerrormsg.Text = "Applying the discount you selected will cause your inventory sales price to be 0 or less than 0.<br> Please remove all items marked with a * from the list.";

            }
        }

    }
    protected void FillGridVolume(String Ed)
    {
        txtFooterSchemaName.DataSource = null;

        txtFooterSchemaName.DataBind();
        string fill = "";
        if (ddlfilst.SelectedIndex > 0)
        {
            fill=" and Active='"+ddlfilst.SelectedValue+"'";
        }
        if (txtfilsdate.Text.Length > 0 && txtfiledate.Text.Length > 0)
        {
            fill += " and (EffectiveStartDate>='" + txtfilsdate.Text + "' and EndDate<='" + txtfiledate.Text + "')";
        }
        string scema = "   SELECT SchemeID      ,SchemeName      ,MinDiscountQty      ,MaxDiscountQty      ,SchemeDiscount    " +
             " , convert(nvarchar(10),EffectiveStartDate,101) as EffectiveStartDate " +
                        ",convert(nvarchar(10),EndDate,101) as  EndDate     ,EntryDate      ,Active ,Case When(Active='1') then 'Active' else 'Inactive' End as status  " +
                        " ,IsPercentage  FROM InventoryVolumeSchemeMaster where compid='" + compid + "' "+fill+" Order by SchemeID Desc ";

        SqlCommand cmdscema = new SqlCommand(scema, con);
        SqlDataAdapter adpscema = new SqlDataAdapter(cmdscema);
        DataTable dtscema = new DataTable();
        adpscema.Fill(dtscema);
        if (dtscema.Rows.Count > 0)
        {

            txtFooterSchemaName.DataSource = dtscema;

            txtFooterSchemaName.DataBind();
        
            foreach (GridViewRow item in txtFooterSchemaName.Rows)
            {
                CheckBox chkIsprs1 = new CheckBox();
                chkIsprs1 = (CheckBox)item.FindControl("chkIsprs1");
               

                if (chkIsprs1 != null)
                {
                    Label Label22 = (Label)item.FindControl("Label22");
                    Label lblsign = (Label)item.FindControl("lblsign");
                    Label lblIsPercent = (Label)item.FindControl("lblIsPercent");
                    if (chkIsprs1.Checked == true)
                    {
                        Label22.Visible = true;
                        lblsign.Visible = false;
                    }
                    else
                    {
                        Label22.Visible = false;
                        lblsign.Visible = true;
                    }
                }
                if (chkIsprs1 == null)
                {
                    chkIsprs1 = (CheckBox)item.FindControl("chkIsprs");
                    RadioButtonList rdperamtedit = (RadioButtonList)item.FindControl("rdperamtedit");
                    if (chkIsprs1.Checked == true)
                    {
                        rdperamtedit.SelectedValue = "1";
                    }
                    else
                    {
                        rdperamtedit.SelectedValue = "2";
                    }
                }


            }
        }
        else
        {

            //ShowNoResultFound(dtscema, txtFooterSchemaName);
        }

    }


    protected void FillddlInvCat()
    {
        ddlInvCat.Items.Clear();
        string strcat = "  SELECT Distinct  InventoryCategoryMaster.InventeroyCatId,InventoryCategoryMaster.InventoryCatName FROM InventoryCategoryMaster inner join InventorySubCategoryMaster on InventorySubCategoryMaster.InventoryCategoryMasterId=InventoryCategoryMaster.InventeroyCatId inner join InventoruSubSubCategory on InventoruSubSubCategory.InventorySubCatID=InventorySubCategoryMaster.InventorySubCatId inner join InventoryMaster on InventoryMaster.InventorySubSubId=InventoruSubSubCategory.InventorySubSubId inner join InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.InventoryMasterId=InventoryMaster.InventoryMasterId inner join WareHouseMaster on WareHouseMaster.WareHouseId=InventoryWarehouseMasterTbl.WareHouseId  WHERE InventoryCategoryMaster.CatType IS NULL and InventoryWarehouseMasterTbl.WareHouseId ='" + ViewState["WH"] + "'";
        //string strcat = "SELECT InventeroyCatId,InventoryCatName  FROM  InventoryCategoryMaster  where compid='" + Session["comid"] + "'";
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

    }

    protected void ddlInvSubCat_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        ddlInvSubSubCat.Items.Clear();
       

        if (Convert.ToInt32(ddlInvSubCat.SelectedIndex) > 0)
        {
            string strsubsubcat = "SELECT InventorySubSubId ,InventorySubSubName  ,InventorySubCatID  FROM  InventoruSubSubCategory " +
                            " where InventorySubCatID=" + Convert.ToInt32(ddlInvSubCat.SelectedValue) + " ";
            SqlCommand cmdsubsubcat = new SqlCommand(strsubsubcat, con);
            SqlDataAdapter adpsubsubcat = new SqlDataAdapter(cmdsubsubcat);
            DataTable dtsubsubcat = new DataTable();
            adpsubsubcat.Fill(dtsubsubcat);

            ddlInvSubSubCat.DataSource = dtsubsubcat;
            ddlInvSubSubCat.DataTextField = "InventorySubSubName";
            ddlInvSubSubCat.DataValueField = "InventorySubSubId";
            ddlInvSubSubCat.DataBind();

        }
       

        ddlInvSubSubCat.Items.Insert(0, "All");
        ddlInvSubSubCat.Items[0].Value = "0";


        ddlInvSubSubCat_SelectedIndexChanged(sender, e);


        // ddlInvSubSubCat.AutoPostBack = true;


    }
    protected void ddlInvSubSubCat_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        ddlInvName.Items.Clear();
        if (Convert.ToInt32(ddlInvSubSubCat.SelectedIndex) > 0)
        {
            string strinvname = "SELECT InventoryMasterId ,Name ,InventoryDetailsId ,InventorySubSubId   ,ProductNo ,InventoryTypeId  FROM InventoryMaster " +
                            " where InventorySubSubId= " + Convert.ToInt32(ddlInvSubSubCat.SelectedValue) + "  and MasterActiveStatus=1 ";
            SqlCommand cmdinvname = new SqlCommand(strinvname, con);
            SqlDataAdapter adpinvname = new SqlDataAdapter(cmdinvname);
            DataTable dtinvname = new DataTable();
            adpinvname.Fill(dtinvname);

            ddlInvName.DataSource = dtinvname;

            ddlInvName.DataTextField = "Name";
            ddlInvName.DataValueField = "InventoryMasterId";
            ddlInvName.DataBind();

        }
       
        ddlInvName.Items.Insert(0, "All");
        ddlInvName.Items[0].Value = "0";
    }

    protected void ddlWarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {

        ViewState["WH"] = ddlWarehouse.SelectedValue;
        FillddlInvCat();
        //ImageClickEventArgs e12345 = new ImageClickEventArgs();
        //imgbtnGo_Click(sender, e12345);
        ddlInvCat_SelectedIndexChanged(sender, e);
        GridView1.DataSource = null;
        GridView1.DataBind();

    }
    protected void imgbtnGo_Click(object sender, EventArgs e)
    {
        lblerrormsg.Text = "";
        DataTable dtivs = (DataTable)SeachByCat();
        if (dtivs.Rows.Count > 0)
        {
            GridView1.DataSource = dtivs;
            GridView1.DataBind();
            if (ImageButton1.Visible == false)
            {
                ImageButton1.Visible = true;
            }
        }
        else
        {
            if (ImageButton1.Visible == true)
            {
                ImageButton1.Visible = false;
            }

        }
        calavgcost();
    }
     protected DataTable select(string str)
    {
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);

        return dt;

    }
     public double GetVolumeDiscount(int InventorywhID, int Qty, decimal rate, Label lblvoldesdetail)
     {
         string str11 = "SELECT    SchemeDetailID, InventoryVolumeSchemeMaster.IsPercentage, InventoryVolumeSchemeDetail.InventoryWHM_Id " +
                   " FROM         InventoryVolumeSchemeMaster INNER JOIN " +
                   "  InventoryVolumeSchemeDetail ON InventoryVolumeSchemeMaster.SchemeID = InventoryVolumeSchemeDetail.SchemeID " +
                   " WHERE (InventoryVolumeSchemeDetail.SchemeID='" + ViewState["Schemaid"] + "') and (InventoryVolumeSchemeDetail.InventoryWHM_Id = '" + InventorywhID + "') AND " +
                   " (InventoryVolumeSchemeMaster.Active = 1)" +
                   " ORDER BY InventoryVolumeSchemeMaster.EffectiveStartDate DESC ";

         SqlCommand cmd11 = new SqlCommand(str11, con);
         SqlDataAdapter adp11 = new SqlDataAdapter(cmd11);
         DataSet ds11 = new DataSet();
         adp11.Fill(ds11);
         if (ds11.Tables[0].Rows.Count > 0)
         {
             lblvoldesdetail.Text = Convert.ToString(ds11.Tables[0].Rows[0]["SchemeDetailID"]);
             int i = Convert.ToInt32(ds11.Tables[0].Rows[0]["IsPercentage"]);

             if (i == 1)
             {
                 string str1 = "SELECT     InventoryVolumeSchemeMaster.SchemeDiscount, InventoryVolumeSchemeDetail.InventoryWHM_Id, InventoryVolumeSchemeMaster.Active,  " +
                                           "InventoryVolumeSchemeMaster.MinDiscountQty, InventoryVolumeSchemeMaster.MaxDiscountQty,  " +
                                            " InventoryVolumeSchemeMaster.EffectiveStartDate,InventoryVolumeSchemeDetail.SchemeDetailID, InventoryVolumeSchemeMaster.IsPercentage " +
                               " FROM         InventoryVolumeSchemeMaster INNER JOIN " +
                                           " InventoryVolumeSchemeDetail ON InventoryVolumeSchemeMaster.SchemeID = InventoryVolumeSchemeDetail.SchemeID " +
                               " WHERE  (InventoryVolumeSchemeDetail.SchemeID='" + ViewState["Schemaid"] + "') and  (InventoryVolumeSchemeDetail.InventoryWHM_Id = '" + InventorywhID + "') AND (InventoryVolumeSchemeMaster.Active = 1)" +
                                " ORDER BY InventoryVolumeSchemeMaster.EffectiveStartDate DESC ";
                 SqlCommand cmd1 = new SqlCommand(str1, con);
                 SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                 DataSet ds1 = new DataSet();
                 adp1.Fill(ds1);
                 double first = Convert.ToDouble(Qty) * Convert.ToDouble(rate);
                 double volumeDis = (first * Convert.ToDouble(ds1.Tables[0].Rows[0]["SchemeDiscount"])) / 100;
              
                 return volumeDis;

             }
             else
             {
                 string str2 = "SELECT     InventoryVolumeSchemeMaster.SchemeDiscount, InventoryVolumeSchemeDetail.InventoryWHM_Id, InventoryVolumeSchemeMaster.Active,  " +
                                         "InventoryVolumeSchemeMaster.MinDiscountQty, InventoryVolumeSchemeMaster.MaxDiscountQty,  " +
                                          " InventoryVolumeSchemeMaster.EffectiveStartDate, InventoryVolumeSchemeDetail.SchemeDetailID, InventoryVolumeSchemeMaster.IsPercentage " +
                             " FROM         InventoryVolumeSchemeMaster INNER JOIN " +
                                         " InventoryVolumeSchemeDetail ON InventoryVolumeSchemeMaster.SchemeID = InventoryVolumeSchemeDetail.SchemeID " +
                             " WHERE  (InventoryVolumeSchemeDetail.SchemeID='" + ViewState["Schemaid"] + "') and  (InventoryVolumeSchemeDetail.InventoryWHM_Id = '" + InventorywhID + "') AND (InventoryVolumeSchemeMaster.Active = 1) " +
                              " ORDER BY InventoryVolumeSchemeMaster.EffectiveStartDate DESC ";
                 SqlCommand cmd2 = new SqlCommand(str2, con);
                 SqlDataAdapter adp2 = new SqlDataAdapter(cmd2);
                 DataSet ds2 = new DataSet();
                 adp2.Fill(ds2);

                 double volumeDis = Convert.ToDouble(ds2.Tables[0].Rows[0]["SchemeDiscount"]);
               
                 return volumeDis;
             }
         }
         else
         {



             string str = "SELECT  InventoryVolumeSchemeMaster.IsPercentage, InventoryVolumeSchemeMaster.SchemeDiscount " +
                         " FROM    InventoryVolumeSchemeMaster where SchemeID='" + ViewState["Schemaid"] + "' ";

             SqlCommand cmd = new SqlCommand(str, con);
             SqlDataAdapter adp = new SqlDataAdapter(cmd);
             DataSet ds = new DataSet();
             adp.Fill(ds);
             if (ds.Tables[0].Rows.Count > 0)
             {
                 int i = Convert.ToInt32(ds.Tables[0].Rows[0]["IsPercentage"]);

                 if (i == 1)
                 {

                     double first = Convert.ToDouble(Qty) * Convert.ToDouble(rate);
                     double volumeDis = (first * Convert.ToDouble(ds.Tables[0].Rows[0]["SchemeDiscount"])) / 100;

                     return volumeDis;

                 }
                 else
                 {

                     double volumeDis = Convert.ToDouble(ds.Tables[0].Rows[0]["SchemeDiscount"]);

                     return volumeDis;
                 }
             }
             else
             {

                 return 0;
             }
         }
     }
     protected void calavgcost()
     {
         foreach (GridViewRow item in GridView1.Rows)
         {
             string invwareid = GridView1.DataKeys[item.RowIndex].Value.ToString();
             Label lblavgcost = (Label)item.FindControl("lblavgcost");
             Label lblsalesrate = (Label)item.FindControl("lblsalesrate");
             Label lblvald = (Label)item.FindControl("lblvald");
             Label lblpromp = (Label)item.FindControl("lblpromp");
            // Label lblfeatureamt = (Label)item.FindControl("lblfeatureamt");
             Label lblmaxorder = (Label)item.FindControl("lblmaxorder");
             Label lbltotdiscamt = (Label)item.FindControl("lbltotdiscamt");
             Label lblnetsales = (Label)item.FindControl("lblnetsales");
             Label lblmarkup = (Label)item.FindControl("lblmarkup");

             Label lblpromdetailid = (Label)item.FindControl("lblpromdetailid");
             Label lblvoldesdetail = (Label)item.FindControl("lblvoldesdetail");

             CheckBox chkonapp = (CheckBox)item.FindControl("chkonapp");
             CheckBox chkretailap = (CheckBox)item.FindControl("chkretailap");
           

             CheckBox CheckBox1 = (CheckBox)item.FindControl("CheckBox1");
             CheckBox chkvoldish = (CheckBox)item.FindControl("chkvoldish");
             CheckBox chkpromoremove = (CheckBox)item.FindControl("chkpromoremove");
             CheckBox chkpromoh = (CheckBox)item.FindControl("chkpromoh");
             //string sk1 = " and InventoryWarehouseMasterAvgCostTbl.DateUpdated<='" +Convert.toda + "'";
             double opto = opstockoth(invwareid);
             lblavgcost.Text = opto.ToString();
             double Discount = GetVolumeDiscount(Convert.ToInt32(invwareid), Convert.ToInt32("1"), Convert.ToDecimal(lblsalesrate.Text), lblvoldesdetail);
             lblvald.Text = String.Format("{0:n}", Discount);
             if (Convert.ToString(lblvoldesdetail.Text) != "")
             {
                  CheckBox1.Checked = true;
                  chkvoldish.Checked = true;

                  chkonapp.Checked = true;
                  chkonapp.Enabled = true;
                  chkretailap.Checked = true;
                  chkretailap.Enabled = true;
             }
             
             else
             {
                 chkonapp.Checked = false;
                 chkonapp.Enabled = false;
                 chkretailap.Checked = false;
                 chkretailap.Enabled = false;

                 CheckBox1.Checked = false;
                 chkvoldish.Checked = false;
             }
             double PromotionDiscount = GetPromotionDiscount(Convert.ToInt32(1), Convert.ToDecimal(lblsalesrate.Text), Convert.ToInt32(invwareid), lblpromdetailid);
             lblpromp.Text = String.Format("{0:n}", PromotionDiscount);
             if (Convert.ToString(lblpromdetailid.Text) != "")
             {
                 chkpromoremove.Checked = true;
                 chkpromoh.Checked = true;
             }

             else
             {
                 chkpromoremove.Checked = false;
                 chkpromoh.Checked = false;
             }
             //double FeatureDiscount = GetFeatureProdDiscountMaster(Convert.ToInt32(1), Convert.ToDecimal(lblsalesrate.Text), Convert.ToInt32(invwareid));
             //lblfeatureamt.Text = String.Format("{0:n}", FeatureDiscount);
             double amt = Convert.ToDouble(lblsalesrate.Text) * 1;
             amt = amt - Discount - PromotionDiscount;
             double Ordervalue=  CountOrderValue(amt);
             lblmaxorder.Text = String.Format("{0:n}", Ordervalue); 

             double totaldisc = (Discount + PromotionDiscount+Ordervalue);

             lbltotdiscamt.Text = String.Format("{0:n}", totaldisc);
             lblnetsales.Text = String.Format("{0:n}", (Convert.ToDouble(lblsalesrate.Text) - totaldisc));
        double maxrate=Convert.ToDouble(lblsalesrate.Text)-((Convert.ToDouble(lblavgcost.Text)+totaldisc))/(Convert.ToDouble(lblsalesrate.Text)*100);
        lblmarkup.Text = String.Format("{0:n}", maxrate);
         }
     }
     public double CountOrderValue(double amt)
     {

         double Value = amt;
             string str = "SELECT     IsPercentage, Active, OrderValueDiscountMasterId, ValueDiscount FROM   OrderValueDiscountMaster " +
                         " where (Whid='" +ddlWarehouse.SelectedValue + "') AND(active =1)  " +
                         " ORDER BY StartDate DESC ";
             SqlCommand cmd = new SqlCommand(str, con);
             SqlDataAdapter adp = new SqlDataAdapter(cmd);
             DataSet ds = new DataSet();
             adp.Fill(ds);
             if (ds.Tables[0].Rows.Count > 0)
             {
                 int i = Convert.ToInt32(ds.Tables[0].Rows[0]["IsPercentage"]);

                 if (i == 1)
                 {
                     string str1 = " SELECT  IsPercentage, Active, StartDate, ValueDiscount FROM OrderValueDiscountMaster " +
                                     " WHERE  (Whid='" + ddlWarehouse.SelectedValue + "') AND (Active = 1) " +
                                      " ORDER BY StartDate DESC ";
                     SqlCommand cmd1 = new SqlCommand(str1, con);
                     SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                     DataSet ds1 = new DataSet();
                     adp1.Fill(ds1);
                     double first = Value;
                     double OrderDisc = (first * Convert.ToDouble(ds1.Tables[0].Rows[0]["ValueDiscount"])) / 100;

                    string Order = String.Format("{0:n}", OrderDisc);
                    return Convert.ToDouble(Order);
                 }
                 else
                 {
                     string str2 = " SELECT  IsPercentage, Active, StartDate, ValueDiscount FROM OrderValueDiscountMaster " +
                                    " WHERE  (Whid='" + ddlWarehouse.SelectedValue + "')AND (Active = 1)" +
                                     " ORDER BY StartDate DESC ";
                     SqlCommand cmd2 = new SqlCommand(str2, con);
                     SqlDataAdapter adp2 = new SqlDataAdapter(cmd2);
                     DataSet ds2 = new DataSet();
                     adp2.Fill(ds2);

                     double OrderDisc = Convert.ToDouble(ds2.Tables[0].Rows[0]["ValueDiscount"]);
                    return OrderDisc;
                 }
               
             }
             else
             {

                 return 0;
             }
       

     }
     public double GetPromotionDiscount(int Qty, decimal rate, int invemtorywhId,Label lblpromdetailid)
     {

         string str = " SELECT   PromotionDiscountDetail,  PromotionDiscountMaster.IsPercentage " +
                     " FROM         PromotionDiscountMaster INNER JOIN " +
                   " PromotionDiscountDetail ON PromotionDiscountMaster.PromotionDiscountMasterID = PromotionDiscountDetail.PromotionDiscountMasterID " +
                     " WHERE ((PromotionDiscountMaster.StartDate<='" + lblenddate.Text + "') and (PromotionDiscountMaster.EndDate >= '" + lblsdate.Text + "')) and    (PromotionDiscountMaster.Active = 1) AND (PromotionDiscountDetail.InventoryWHM_Id = '" + invemtorywhId + "') " +
                 "  ORDER BY PromotionDiscountMaster.EntryDate DESC ";
         SqlCommand cmd = new SqlCommand(str, con);
         SqlDataAdapter adp = new SqlDataAdapter(cmd);
         DataSet ds = new DataSet();
         adp.Fill(ds);
         if (ds.Tables[0].Rows.Count > 0)
         {
             lblpromdetailid.Text = Convert.ToString(ds.Tables[0].Rows[0]["PromotionDiscountDetail"]);
             int i = Convert.ToInt32(ds.Tables[0].Rows[0]["IsPercentage"]);

             if (i == 1)
             {
                 string str1 = " SELECT     PromotionDiscountMaster.IsPercentage, PromotionDiscountMaster.DiscountAmount, PromotionDiscountDetail.PromotionDiscountDetail " +
                     " FROM         PromotionDiscountMaster INNER JOIN " +
                   " PromotionDiscountDetail ON PromotionDiscountMaster.PromotionDiscountMasterID = PromotionDiscountDetail.PromotionDiscountMasterID " +
                     " WHERE ((PromotionDiscountMaster.StartDate<='" + lblenddate.Text + "') and (PromotionDiscountMaster.EndDate >= '" + lblsdate.Text + "')) and   (PromotionDiscountMaster.Active = 1) AND (PromotionDiscountDetail.InventoryWHM_Id = '" + invemtorywhId + "') " +
                 "  ORDER BY PromotionDiscountMaster.EntryDate DESC ";
                 SqlCommand cmd1 = new SqlCommand(str1, con);
                 SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                 DataSet ds1 = new DataSet();
                 adp1.Fill(ds1);
                 double first = Convert.ToDouble(Qty) * Convert.ToDouble(rate);
                 double CustDis = (first * Convert.ToDouble(ds1.Tables[0].Rows[0]["DiscountAmount"])) / 100;
             
                 return CustDis;


             }
             else
             {
                 string str2 = " SELECT     PromotionDiscountMaster.IsPercentage, PromotionDiscountMaster.DiscountAmount, PromotionDiscountDetail.PromotionDiscountDetail " +
                    " FROM         PromotionDiscountMaster INNER JOIN " +
                  " PromotionDiscountDetail ON PromotionDiscountMaster.PromotionDiscountMasterID = PromotionDiscountDetail.PromotionDiscountMasterID " +
                    " WHERE  ((PromotionDiscountMaster.StartDate<='" + lblenddate.Text + "') and (PromotionDiscountMaster.EndDate >= '" + lblsdate.Text + "')) and  (PromotionDiscountMaster.Active = 1) AND (PromotionDiscountDetail.InventoryWHM_Id = '" + invemtorywhId + "') " +
                "  ORDER BY PromotionDiscountMaster.EntryDate DESC ";
                 SqlCommand cmd2 = new SqlCommand(str2, con);
                 SqlDataAdapter adp2 = new SqlDataAdapter(cmd2);
                 DataSet ds2 = new DataSet();
                 adp2.Fill(ds2);

                 double CustDis = Convert.ToDouble(ds2.Tables[0].Rows[0]["DiscountAmount"]);
                
                 return CustDis;
             }
         }
         else
         {
             
             return 0;
         }


     }
     public double GetFeatureProdDiscountMaster(int Qty, decimal rate, int invemtorywhId)
     {

         string str = " SELECT     FeatureProdDiscountMaster.IsPercentage " +
                   " FROM         FeatureProdDiscountMaster INNER JOIN " +
                 " FeatureProdDiscountDetail ON FeatureProdDiscountMaster.FeatureProdDiscountMasterID = FeatureProdDiscountDetail.FeatureProdDiscountDetailId " +
                   " WHERE ((FeatureProdDiscountMaster.StartDate between '" + lblsdate.Text + "' and '" + lblenddate.Text + "') and (FeatureProdDiscountMaster.EndDate between '" + lblsdate.Text + "' and '" + lblenddate.Text + "')) and    (FeatureProdDiscountMaster.Active = 1) AND (FeatureProdDiscountDetail.InventoryWHM_Id = '" + invemtorywhId + "') " +
               "  ORDER BY FeatureProdDiscountMaster.FeatureProdDiscountMasterID DESC ";
         SqlCommand cmd = new SqlCommand(str, con);
         SqlDataAdapter adp = new SqlDataAdapter(cmd);
         DataSet ds = new DataSet();
         adp.Fill(ds);
         if (ds.Tables[0].Rows.Count > 0)
         {
             int i = Convert.ToInt32(ds.Tables[0].Rows[0]["IsPercentage"]);

             if (i == 1)
             {
                 string str1 = " SELECT     FeatureProdDiscountMaster.IsPercentage, FeatureProdDiscountMaster.DiscountAmount" +
                     " FROM        FeatureProdDiscountDetail ON FeatureProdDiscountMaster.FeatureProdDiscountMasterID = FeatureProdDiscountDetail.FeatureProdDiscountDetailId  " +
                     " WHERE ((FeatureProdDiscountMaster.StartDate between '" + lblsdate.Text + "' and '" + lblenddate.Text + "') and (FeatureProdDiscountMaster.EndDate between '" + lblsdate.Text + "' and '" + lblenddate.Text + "')) and   (FeatureProdDiscountMaster.Active = 1) AND (FeatureProdDiscountDetail.InventoryWHM_Id = '" + invemtorywhId + "') " +
                 "  ORDER BY PromotionDiscountMaster.FeatureProdDiscountMasterID DESC ";
                 SqlCommand cmd1 = new SqlCommand(str1, con);
                 SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                 DataSet ds1 = new DataSet();
                 adp1.Fill(ds1);
                 double first = Convert.ToDouble(Qty) * Convert.ToDouble(rate);
                 double fitdis = (first * Convert.ToDouble(ds1.Tables[0].Rows[0]["DiscountAmount"])) / 100;

                 return fitdis;


             }
             else
             {
                 string str2 = " SELECT     FeatureProdDiscountMaster.IsPercentage, FeatureProdDiscountMaster.DiscountAmount " +
                    " FROM       FeatureProdDiscountDetail ON FeatureProdDiscountMaster.FeatureProdDiscountMasterID = FeatureProdDiscountDetail.FeatureProdDiscountDetailId  " +
                     " WHERE ((FeatureProdDiscountMaster.StartDate between '" + lblsdate.Text + "' and '" + lblenddate.Text + "') and (FeatureProdDiscountMaster.EndDate between '" + lblsdate.Text + "' and '" + lblenddate.Text + "')) and   (FeatureProdDiscountMaster.Active = 1) AND (FeatureProdDiscountDetail.InventoryWHM_Id = '" + invemtorywhId + "') " +
                 "  ORDER BYPromotionDiscountMaster.FeatureProdDiscountMasterID DESC ";
                 SqlCommand cmd2 = new SqlCommand(str2, con);
                 SqlDataAdapter adp2 = new SqlDataAdapter(cmd2);
                 DataSet ds2 = new DataSet();
                 adp2.Fill(ds2);

                 double fitdis = Convert.ToDouble(ds2.Tables[0].Rows[0]["DiscountAmount"]);

                 return fitdis;
             }
         }
         else
         {

             return 0;
         }


     }

     protected double opstockoth( string invwareid)
     {
         double avgqty = 0;


         double avgrate = 0;
         double TotalAvgBalsub = 0;
         double TotalAvgBal = 0;
         double AvgQtyAvail = 0;

         double AvgCostFinal = 0;

         double totavgcost = 0;
         double Avgtotqty = 0;
         DataTable dtodop = new DataTable();
         DataTable dtodop1 = new DataTable();
         //dtodop1 = (DataTable)select("Select Sum(cast(Qty as decimal(20,2))) as qty from InventoryWarehouseMasterAvgCostTbl  inner join InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.InventoryWarehouseMasterId=InventoryWarehouseMasterAvgCostTbl.InvWMasterId where InventoryWarehouseMasterTbl.InventoryWarehouseMasterId='" + invwareid + "' and InventoryWarehouseMasterTbl.WareHouseId='" + ddlWarehouse.SelectedValue + "' " + op);
         //if (dtodop1.Rows.Count > 0)
         //{
         //    if (Convert.ToString(dtodop1.Rows[0][0]) != "")
         //        Avgtotqty = Convert.ToDouble(dtodop1.Rows[0][0]);
         //}
         //else
         //{
         //    Avgtotqty = 0;
         //}
         dtodop = (DataTable)select("Select InventoryWarehouseMasterAvgCostTbl.* from  InventoryWarehouseMasterAvgCostTbl  inner join InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.InventoryWarehouseMasterId=InventoryWarehouseMasterAvgCostTbl.InvWMasterId where InventoryWarehouseMasterTbl.InventoryWarehouseMasterId='" + invwareid + "' and InventoryWarehouseMasterTbl.WareHouseId='" + ddlWarehouse.SelectedValue + "'"+" order by IWMAvgCostId");
         if (dtodop.Rows.Count > 0)
         {
             foreach (DataRow dtr in dtodop.Rows)
             {
                 if (Convert.ToString(dtr["Qty"]) != "")
                 {
                     avgqty = Convert.ToDouble(dtr["Qty"].ToString());
                     if (Convert.ToString(avgqty) != "")
                     {
                         if (avgqty < 0)
                         {
                             avgqty = avgqty * (-1);
                         }
                     }
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
             }
             //totavgcost = Convert.ToDouble(Avgtotqty) * Convert.ToDouble(AvgCostFinal);
           //  totavgcost = Math.Round(totavgcost, 2);
             totavgcost = Math.Round(AvgCostFinal, 2);
         }
         return totavgcost;

     }
       
    public DataTable SeachByCat()
    {



        lblMainCat.Text = "All";
        lblSubCat.Text = "All";
        lblSubSub.Text = "All";
        lblInv.Text = "All";
        string strinv = " SELECT  InventoryWarehouseMasterTbl.Weight,  UnitTypeMaster.Name as UnitName, InventoryMaster.Name, InventoryMaster.ProductNo, InventoryMaster.InventoryMasterId, InventoryWarehouseMasterTbl.Rate,  " +
                     " InventoryWarehouseMasterTbl.WareHouseId, InventoryCategoryMaster.InventeroyCatId, InventoryCategoryMaster.InventoryCatName, " +
                     " InventorySubCategoryMaster.InventorySubCatId, InventorySubCategoryMaster.InventorySubCatName, InventoruSubSubCategory.InventorySubSubId,  " +
                     " InventoruSubSubCategory.InventorySubSubName,InventoryWarehouseMasterTbl.InventoryWarehouseMasterId, " +
                      " InventoryCategoryMaster.InventoryCatName +' : '+ InventorySubCategoryMaster.InventorySubCatName+' : '+ " +
                      "  InventoruSubSubCategory.InventorySubSubName+' : '+InventoryMaster.Name as catandname    " +
                     " FROM         InventoryMaster INNER JOIN " +
                     " InventoruSubSubCategory ON InventoryMaster.InventorySubSubId = InventoruSubSubCategory.InventorySubSubId INNER JOIN " +
                     " InventorySubCategoryMaster ON InventoruSubSubCategory.InventorySubCatID = InventorySubCategoryMaster.InventorySubCatId INNER JOIN " +
                     " InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId RIGHT OUTER JOIN " +
                     " InventoryWarehouseMasterTbl ON InventoryMaster.InventoryMasterId = InventoryWarehouseMasterTbl.InventoryMasterId  left outer join UnitTypeMaster on UnitTypeMaster.UnitTypeId=InventoryWarehouseMasterTbl.UnitTypeId " +
                     " WHERE  InventoryMaster.CatType IS NULL And   (InventoryWarehouseMasterTbl.WareHouseId = '" + Convert.ToInt32(ViewState["WH"].ToString()) + "')   and InventoryMaster.MasterActiveStatus=1   ";
        //and (InventoryMasterMNC.copid='"+compid+"')

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
                        strInvId = "and  InventoryMaster.InventoryMasterId=" + Convert.ToInt32(ddlInvName.SelectedValue) + " ";

                    }
                    else
                    {
                        strInvsubsubCatId = " and  InventoruSubSubCategory.InventorySubSubId=" + Convert.ToInt32(ddlInvSubSubCat.SelectedValue) + "";
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
        lblcomname.Text = Session["Cname"].ToString();
        lblBusiness.Text ="Business : "+ ddlWarehouse.SelectedItem.Text;
        lblMainCat.Text = ddlInvCat.SelectedItem.Text;
        lblSubCat.Text = ddlInvSubCat.SelectedItem.Text;
        lblSubSub.Text = ddlInvSubSubCat.SelectedItem.Text;
        lblInv.Text = ddlInvName.SelectedItem.Text;

        string mainStringCat = strinv + strInvId + strInvsubsubCatId + strInvsubcatid + strInvCatid + "order by InventoryMaster.InventoryMasterId";//+ strInvBySerchId // InventoryMaster.Name ";


        SqlCommand cmdcat = new SqlCommand(mainStringCat, con);
        SqlDataAdapter adpcat = new SqlDataAdapter(cmdcat);
        DataTable dtcat = new DataTable();
        adpcat.Fill(dtcat);



        return dtcat;

    }
 
    protected void GrdSchemaVolume_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        ViewState["Schemaid"] = null;
        if (e.CommandName == "Select")
        {

            //Panel1.Visible = true;
            //txtFooterSchemaName.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            //int dk = Convert.ToInt32(txtFooterSchemaName.SelectedDataKey.Value);
            //ViewState["Schemaid"] = dk;

            //LinkButton nm = (LinkButton)(txtFooterSchemaName.Rows[txtFooterSchemaName.SelectedIndex].FindControl("lblScemaName"));
            //LinkButton nm1 = (LinkButton)(txtFooterSchemaName.Rows[txtFooterSchemaName.SelectedIndex].FindControl("lblMinDiscountQty"));
            //LinkButton nm2 = (LinkButton)(txtFooterSchemaName.Rows[txtFooterSchemaName.SelectedIndex].FindControl("lblMaxDiscountQty"));
            //LinkButton nm3 = (LinkButton)(txtFooterSchemaName.Rows[txtFooterSchemaName.SelectedIndex].FindControl("lblEffectiveStartDate"));
            //LinkButton nm4 = (LinkButton)(txtFooterSchemaName.Rows[txtFooterSchemaName.SelectedIndex].FindControl("lblEndDate"));
            //Label nm5 = (Label)(txtFooterSchemaName.Rows[txtFooterSchemaName.SelectedIndex].FindControl("lblSchemeDiscount"));
            //CheckBox nm6 = (CheckBox)(txtFooterSchemaName.Rows[txtFooterSchemaName.SelectedIndex].FindControl("chkIsprs1"));
            //lblScemaNamefromGrd.Text = nm.Text;
            //lblminqty.Text = nm1.Text;
            //lblmaxqty.Text = nm2.Text;
            //lbldis.Text = nm5.Text;
            
            //chkisper.Checked = nm6.Checked;
            //if(chkisper.Checked == true)
            //{
            //    lblisper.Text = "True";
            //}
            //else if (chkisper.Checked == false)
            //{
            //    lblisper.Text = "False";
            //}

            int key = Convert.ToInt32(e.CommandArgument);
            ViewState["Schemaid"] = key.ToString();

            string s = "select * from InventoryVolumeSchemeMaster where SchemeID = '" + ViewState["Schemaid"] + "' ";

            SqlCommand cmd = new SqlCommand(s, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            da.Fill(ds);
            if (ds.Rows.Count > 0)
            {

                Panel2.Visible = true;


                lblScemaNamefromGrd.Text = "'" + Convert.ToString(ds.Rows[0]["SchemeName"]) + "'";
                if (Convert.ToBoolean(ds.Rows[0]["IsPercentage"]) == true)
                {
                    lblmd.Text = "(Min Disc " + Convert.ToString(ds.Rows[0]["MinDiscountQty"]) + ", Max Disc " +Convert.ToString(ds.Rows[0]["MaxDiscountQty"]) +", Disc=" + Convert.ToString(ds.Rows[0]["SchemeDiscount"]) + "%, ";
                }
                else
                {
                    lblmd.Text = "(Disc=$" + Convert.ToString(ds.Rows[0]["SchemeDiscount"]) + ", ";
                }
                lblmd.Text = lblmd.Text + Convert.ToDateTime(ds.Rows[0]["EffectiveStartDate"]).ToShortDateString() + " - " + Convert.ToDateTime(ds.Rows[0]["EndDate"]).ToShortDateString() + ")";
                //lbldis.Text = Convert.ToString(ds.Rows[0]["DiscountAmount"]);

                //chkisper.Checked = Convert.ToBoolean(ds.Rows[0]["IsPercentage"]);
                //if (chkisper.Checked == true)
                //{
                //    lblisper.Text = "True";
                //}
                //else if (chkisper.Checked == false)
                //{
                //    lblisper.Text = "False";
                //}
                lblsdate.Text = Convert.ToDateTime(ds.Rows[0]["EffectiveStartDate"]).ToShortDateString();
                lblenddate.Text = Convert.ToDateTime(ds.Rows[0]["EndDate"]).ToShortDateString();
                //ImageButton1.Visible = false;
                pnlgrid.Visible = true;
                Panel1.Visible = true;
                if (Convert.ToString(ds.Rows[0]["Active"]) == "True")
                {
                  
                    pnlgrid.Enabled = true;
                    ImageButton1.Enabled = true;
                }
                else
                {
                 
                    pnlgrid.Enabled = false;
                    ImageButton1.Enabled = false;
                }
                if (Convert.ToDateTime(ds.Rows[0]["EndDate"]) < Convert.ToDateTime(DateTime.Now.ToShortDateString()))
                {
                    pnlgrid.Enabled = false;
                    ImageButton1.Enabled = false;
                }

            }
           
            imgbtnGo_Click(sender, e);
        }
        if (e.CommandName == "Edit")
        {
            //txtFooterSchemaName.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            //int dk = Convert.ToInt32(txtFooterSchemaName.SelectedDataKey.Value);
            //ViewState["Schemaid"] = dk;

            //LinkButton nm4 = (LinkButton)(txtFooterSchemaName.Rows[txtFooterSchemaName.SelectedIndex].FindControl("lblEndDate"));

            //ViewState["ED"] = nm4.Text;
   
            //GridViewEditEventArgs e12 = new GridViewEditEventArgs(txtFooterSchemaName.SelectedIndex);
            //GrdSchemaVolume_RowEditing(sender, e12);

            imgupdate.Visible = true;
            Button1.Visible = false;
            pnlv.Enabled = true;
            lblleg.Text = "Edit Volume Discount";

            Panel1.Visible = false;
            Label1.Text = "";
            pnladd.Visible = true;
            btnadd.Visible = false;

            int key = Convert.ToInt32(e.CommandArgument);
            ViewState["Schemaid"] = key.ToString();
            string s = "select * from InventoryVolumeSchemeMaster where SchemeID = '" + key + "' ";

            SqlCommand cmd = new SqlCommand(s, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            da.Fill(ds);
            if (ds.Rows.Count > 0)
            {


                txtdosname.Text = Convert.ToString(ds.Rows[0]["SchemeName"]);
                txtEndDate.Text = Convert.ToString(ds.Rows[0]["EndDate"]);

                txtFooterDescount.Text = Convert.ToString(ds.Rows[0]["SchemeDiscount"]);
                ViewState["ED"] = Convert.ToString(ds.Rows[0]["EndDate"]);
                txtStartDate.Text = Convert.ToString(ds.Rows[0]["EffectiveStartDate"]);
                txtFooterMaxQty.Text = Convert.ToString(ds.Rows[0]["MaxDiscountQty"]);
                txtFooterMinQty.Text = Convert.ToString(ds.Rows[0]["MinDiscountQty"]);
                if (Convert.ToString(ds.Rows[0]["Active"]) == "True")
                {
                   
                    ddlstatus.SelectedIndex = 0;
                }
                else
                {
                    ddlstatus.SelectedIndex = 1;
                }
                if (Convert.ToString(ds.Rows[0]["IsPercentage"]) == "True")
                {
                  
                    rdperamt.SelectedValue = "1";
                }
                else
                {
                    rdperamt.SelectedValue = "2";
                }
            }
        }
        if (e.CommandName == "Delete1")
        {
            //GrdSchemaVolume.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            //int dk = Convert.ToInt32(GrdSchemaVolume.SelectedDataKey.Value);

            //// string Delsttr = "Delete  InventoryVolumeSchemeMaster where SchemeID=" + dk + " ";

            //string Delsttr = "Delete  InventoryVolumeSchemeMaster where SchemeID=" + dk + " and compid='" + compid + "' ";
            //SqlCommand cmdinsert1 = new SqlCommand(Delsttr, con);
            //con.Open();
            //cmdinsert1.ExecuteNonQuery();
            //con.Close();


            //string delstrdetail1 = "Delete InventoryVolumeSchemeDetail where SchemeID=" + dk + " ";
            //SqlCommand cmdinsert11 = new SqlCommand(delstrdetail1, con);
            //con.Open();
            //cmdinsert11.ExecuteNonQuery();
            //con.Close();

            //Label1.Visible = true;
            //Label1.Text = "Record deleted successfully";
            //FillGridVolume();
            //Panel1.Visible = false;

        }


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
   
    protected void GrdSchemaVolume_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //txtFooterSchemaName.EditIndex = e.NewEditIndex;
        //FillGridVolume("Ed");

    }
   
   
   
  
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1222.Hide();
    }
    protected void ddlInvCat_SelectedIndexChanged(object sender, EventArgs e)
    {
        {


            ddlInvSubCat.Items.Clear();

            if (Convert.ToInt32(ddlInvCat.SelectedIndex) > 0)
            {
                string strsubcat = "SELECT InventorySubCatId  ,InventorySubCatName ,InventoryCategoryMasterId  FROM InventorySubCategoryMaster " +
                                " where InventoryCategoryMasterId = " + Convert.ToInt32(ddlInvCat.SelectedValue) + " ";
                SqlCommand cmdsubcat = new SqlCommand(strsubcat, con);
                SqlDataAdapter adpsubcat = new SqlDataAdapter(cmdsubcat);
                DataTable dtsubcat = new DataTable();
                adpsubcat.Fill(dtsubcat);

                ddlInvSubCat.DataSource = dtsubcat;

                ddlInvSubCat.DataTextField = "InventorySubCatName";
                ddlInvSubCat.DataValueField = "InventorySubCatId";
                ddlInvSubCat.DataBind();

            }
           
            ddlInvSubCat.Items.Insert(0, "All");
            ddlInvSubCat.Items[0].Value = "0";
            ddlInvSubCat_SelectedIndexChanged(sender, e);
        }
    }
    protected void GrdSchemaVolume_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        txtFooterSchemaName.PageIndex = e.NewPageIndex;
        FillGridVolume("");
    }
    protected void chktemp_chachedChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((CheckBox)sender).Parent.Parent as GridViewRow;

        int rinrow = row.RowIndex;
        CheckBox chk = (CheckBox)GridView1.Rows[rinrow].FindControl("CheckBox1");
        CheckBox chkonapp = (CheckBox)GridView1.Rows[rinrow].FindControl("chkonapp");

        CheckBox chkretailap = (CheckBox)GridView1.Rows[rinrow].FindControl("chkretailap");
        if (chk.Checked == true)
        {
            chkretailap.Enabled = true;
            chkretailap.Checked = true;
            chkonapp.Enabled = true;
            chkonapp.Checked = true;
        }
        else
        {
            chkretailap.Enabled = false;
            chkretailap.Checked = false;
            chkonapp.Enabled = false;
            chkonapp.Checked = false;
        }
    }
    protected void chkAll_chachedChanged(object sender, EventArgs e)
    {
        CheckBox chk;
        foreach (GridViewRow rowitem in GridView1.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBox1"));
            chk.Checked = ((CheckBox)sender).Checked;

            CheckBox chkretailap = (CheckBox)(rowitem.FindControl("chkretailap"));
            CheckBox chkonapp = (CheckBox)(rowitem.FindControl("chkonapp"));
            if (chk.Checked == true)
            {
                chkretailap.Enabled = true;
                chkretailap.Checked = true;
                chkonapp.Enabled = true;
                chkonapp.Checked = true;
            }
            else
            {
                chkretailap.Enabled = false;
                chkretailap.Checked = false;
                chkonapp.Enabled = false;
                chkonapp.Checked = false;
            }
        }
    }
    protected void GrdSchemaVolume_Sorting(object sender, GridViewSortEventArgs e)
    {

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {

    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (Buttonp1.Text == "Printable Version")
        {
            //pnlgrid.ScrollBars = ScrollBars.None;
            //pnlgrid.Height = new Unit("100%");

            Buttonp1.Text = "Hide Printable Version";
            Button2.Visible = true;
            if (GridView1.Columns[4].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[4].Visible = false;
            }
            CheckBox chkAll = (CheckBox)GridView1.HeaderRow.FindControl("chkAll");

            chkAll.Enabled = false;
            foreach (GridViewRow item in GridView1.Rows)
            {
                CheckBox chk = (CheckBox)item.FindControl("CheckBox1");
                CheckBox chkpromoremove = (CheckBox)item.FindControl("chkpromoremove");
                CheckBox chkonapp = (CheckBox)item.FindControl("chkonapp");
                CheckBox chkretailap = (CheckBox)item.FindControl("chkretailap");
                chkonapp.Enabled = false;
                chkretailap.Enabled = false;
                chk.Enabled = false;
                chkpromoremove.Enabled = false;
            }
           
        }
        else
        {

            //pnlgrid.ScrollBars = ScrollBars.Vertical;
            //pnlgrid.Height = new Unit(300);

            Buttonp1.Text = "Printable Version";
            Button2.Visible = false;
            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[4].Visible = true;
            }
            CheckBox chkAll = (CheckBox)GridView1.HeaderRow.FindControl("chkAll");

            chkAll.Enabled = true;
            foreach (GridViewRow item in GridView1.Rows)
            {
                CheckBox chk = (CheckBox)item.FindControl("CheckBox1");
                CheckBox chkpromoremove = (CheckBox)item.FindControl("chkpromoremove");
                CheckBox chkonapp = (CheckBox)item.FindControl("chkonapp");
                CheckBox chkretailap = (CheckBox)item.FindControl("chkretailap");
                chkonapp.Enabled = false;
                chkretailap.Enabled = false;
                chk.Enabled = true;
                chkpromoremove.Enabled = true;
            }
           
        }
    }
    protected void GrdSchemaVolume_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        txtFooterSchemaName.SelectedIndex = Convert.ToInt32(txtFooterSchemaName.DataKeys[e.RowIndex].Value.ToString());
        int dk = txtFooterSchemaName.SelectedIndex;

        // string Delsttr = "Delete  InventoryVolumeSchemeMaster where SchemeID=" + dk + " ";

        string Delsttr = "Delete  InventoryVolumeSchemeMaster where SchemeID=" + dk + " and compid='" + compid + "' ";
        SqlCommand cmdinsert1 = new SqlCommand(Delsttr, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmdinsert1.ExecuteNonQuery();
        con.Close();


        string delstrdetail1 = "Delete InventoryVolumeSchemeDetail where SchemeID=" + dk + " ";
        SqlCommand cmdinsert11 = new SqlCommand(delstrdetail1, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmdinsert11.ExecuteNonQuery();
        con.Close();

        Label1.Visible = true;
        Label1.Text = "Record deleted successfully";
        ddlWarehouse.SelectedIndex = 0;
        ddlWarehouse_SelectedIndexChanged(sender, e);
        FillGridVolume("");
        Panel1.Visible = false;

    }
  
    protected void btncan_Click1(object sender, EventArgs e)
    {
        Panel1.Visible = false;
    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        if (pnladd.Visible == false)
        {
            lblleg.Text = "Add New Volume Discount";
            pnladd.Visible = true;
            Panel1.Visible = false;
        }
        else
        {
            lblleg.Text = "";
            pnladd.Visible = false;
        }
        btnadd.Visible = false;
        Label1.Text = "";
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
       
        string str111 = "Select SchemeName from InventoryVolumeSchemeMaster where SchemeName='" + txtdosname.Text + "' and compid='" + Session["comid"] + "' ";
        SqlCommand cmdstr111 = new SqlCommand(str111, con);
        SqlDataAdapter dastr111 = new SqlDataAdapter(cmdstr111);
        DataTable dtstr111 = new DataTable();
        dastr111.Fill(dtstr111);
        if (dtstr111.Rows.Count == 0)
        {



            //int a, b;
            DateTime date = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());



            bool ispercn = true;
            decimal c1 = Convert.ToDecimal(isdecimalornot(txtFooterMaxQty.Text));
            decimal b1 = Convert.ToDecimal(isdecimalornot(txtFooterMinQty.Text));
            if (b1 > c1)
            {

                Label1.Visible = true;
                Label1.Text = "Minimum Quantity must be less than Maximum Quantity";
               

            }

            else
            {
                DateTime dt = Convert.ToDateTime(txtStartDate.Text);
                DateTime dt1 = Convert.ToDateTime(txtEndDate.Text);
                if (dt < Convert.ToDateTime(DateTime.Now.ToShortDateString()))
                {
                    Label1.Visible = true;
                    Label1.Text = "The start date must be the current date, or greater than the current date.";
                   
                }

                else if (dt1 < dt)
                {

                    Label1.Visible = true;
                    Label1.Text = "Start Date must be less than End Date";
                   
                }
                else
                {

                    int flag = 0;

                    if (rdperamt.SelectedIndex == 0)
                    {
                        if (Convert.ToDecimal(txtFooterDescount.Text) > Convert.ToDecimal(100))
                        {
                            flag = 1;
                        }
                    }
                    if (flag == 0)
                    {


                        try
                        {
                            if (rdperamt.SelectedValue == "1")
                            {
                                ispercn = true;
                            }
                            else
                            {
                                ispercn = false;

                            }

                            String strinsert = "Insert Into InventoryVolumeSchemeMaster(SchemeName,MinDiscountQty,MaxDiscountQty,SchemeDiscount " +
                                               " ,EffectiveStartDate,EndDate,EntryDate,Active,IsPercentage,compid) " +
                                               " values ( '" + txtdosname.Text + "', " +
                                                   "'" + txtFooterMinQty.Text + "', " +
                                                   "'" + txtFooterMaxQty.Text + "', " +
                                                        "'" + txtFooterDescount.Text + "', " +
                                                             "'" + txtStartDate.Text + "', " +
                                                                  "'" + txtEndDate.Text + "', " +
                                                                       "'" + System.DateTime.Now.ToShortDateString() + "', " +
                                                                            "'" + ddlstatus.SelectedValue + "', " +
                                                                                 "'" + ispercn + "','" + compid + "' )";
                            SqlCommand cmdinsert = new SqlCommand(strinsert, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmdinsert.ExecuteNonQuery();
                            con.Close();
                            Label1.Visible = true;
                            Label1.Text = "Record inserted successfully";
                            FillGridVolume("");
                            clear();
                        }
                        catch (Exception ex)
                        {
                            Label1.Visible = true;
                            Label1.Text = "Error";
                        }
                    }
                    else
                    {
                        Label1.Visible = true;
                        Label1.Text = "Volume discount percentage cannot be greater than 100%.";
                    }
                }

            }


        }
        else
        {
            Label1.Visible = true;
            Label1.Text = "Record already exists";
        }

    }
    protected void clear()
    {
        txtdosname.Text = "";
        txtFooterDescount.Text = "";
        txtEndDate.Text = "";
        ddlstatus.SelectedIndex = 0;
        txtStartDate.Text = "";
        lblleg.Text = "";
        imgupdate.Visible = false;
        Button1.Visible = true;
        pnladd.Visible = false;
        btnadd.Visible = true;
        rdperamt.SelectedValue = "1";
        txtFooterMinQty.Text = "";
        txtFooterMaxQty.Text = "";
    }
    protected void txtFooterSchemaName_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lblGrdStartDate = (Label)e.Row.FindControl("lblEffectiveStartDate");
            Label lblGrdEndDate = (Label)e.Row.FindControl("lblEndDate");
            Label lblch = (Label) e.Row.FindControl("lblch");
            LinkButton img11a = (LinkButton) e.Row.FindControl("img11a");
            if (Convert.ToDateTime(lblGrdEndDate.Text) < Convert.ToDateTime(DateTime.Now.ToShortDateString()))
            {
                img11a.Text = "View Discount";
                img11a.ToolTip = "View Discount";
            }
            else if (lblch.Text == "Inactive")
            {
                img11a.Text = "View Discount";
                img11a.ToolTip = "View Discount";
            }
            else
            {
                img11a.Text = "Apply Discount";
                img11a.ToolTip = "Apply Discount";
            }

        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Label1.Text = "";
        clear();
    }
    protected void imgupdate_Click(object sender, EventArgs e)
    {
        bool isper = true;
        string str111 = "Select SchemeName from InventoryVolumeSchemeMaster where SchemeName='" + txtdosname.Text + "' and compid='" + Session["comid"] + "' and SchemeID<>'" + ViewState["Schemaid"] + "' ";
        SqlCommand cmdstr111 = new SqlCommand(str111, con);
        SqlDataAdapter dastr111 = new SqlDataAdapter(cmdstr111);
        DataTable dtstr111 = new DataTable();
        dastr111.Fill(dtstr111);
        if (dtstr111.Rows.Count == 0)
        {

            DateTime date = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
            decimal c1 = Convert.ToDecimal(isdecimalornot(txtFooterMaxQty.Text));
            decimal b1 = Convert.ToDecimal(isdecimalornot(txtFooterMinQty.Text));
            if (b1 > c1)
            {

                Label1.Visible = true;
                Label1.Text = "Minimum Quantity must be less than Maximum Quantity";

            }

            else
            {
                int ct = 0;
                DateTime dt = Convert.ToDateTime(txtStartDate.Text);
                DateTime dt1 = Convert.ToDateTime(txtEndDate.Text);
                if (Convert.ToDateTime(ViewState["ED"]).ToShortDateString() == dt1.ToShortDateString())
                {

                }
                else if (dt1 < Convert.ToDateTime(DateTime.Now.ToShortDateString()))
                {
                    Label1.Visible = true;
                    Label1.Text = "The start date must be the current date, or greater than the current date.";
                    ct = 1;
                }

                if (dt1 < dt)
                {

                    Label1.Visible = true;
                    Label1.Text = "Start Date must be less than End Date";

                    ct = 1;
                }
                else
                {
                    if (ct == 0)
                    {
                        int flag = 0;

                        if (rdperamt.SelectedIndex == 0)
                        {
                            if (Convert.ToDecimal(txtFooterDescount.Text) > Convert.ToDecimal(100))
                            {
                                flag = 1;
                            }
                        }
                        if (flag == 0)
                        {
                            try
                            {
                                if (rdperamt.SelectedValue == "1")
                                {
                                    isper = true;
                                }
                                else
                                {
                                    isper = false;

                                }

                                string strupdate = " update InventoryVolumeSchemeMaster set SchemeName='" + txtdosname.Text + "', " +
                                                     " MinDiscountQty='" + txtFooterMinQty.Text + "',MaxDiscountQty='" + txtFooterMaxQty.Text + "',  " +
                                                     "    SchemeDiscount='" + txtFooterDescount.Text + "' " +
                                                   " ,EndDate='" + txtEndDate.Text + "',      " +
                                                   "  Active='" + ddlstatus.SelectedValue + "',IsPercentage='" + isper+ "'  " +
                                                   " where  SchemeID='" + ViewState["Schemaid"] + "'  ";

                                SqlCommand cmdupdate = new SqlCommand(strupdate, con);
                                if (con.State.ToString() != "Open")
                                {
                                    con.Open();
                                }
                                cmdupdate.ExecuteNonQuery();
                                con.Close();
                                Label1.Visible = true;
                                Label1.Text = " Record updated successfully";

                                txtFooterSchemaName.EditIndex = -1;
                                FillGridVolume("");
                                clear();
                            }
                            catch (Exception ex)
                            {
                                Label1.Visible = true;
                                Label1.Text = "Error";
                            }
                        }
                        else
                        {
                            Label1.Visible = true;
                            Label1.Text = "Volume discount percentage cannot be greater than 100%.";
                        }
                    }
                }
            }
        }
        else
        {
            Label1.Visible = true;
            Label1.Text = "Record already exists";
        }
    }
    protected void btng_Click(object sender, EventArgs e)
    {
        FillGridVolume("");
    }
}