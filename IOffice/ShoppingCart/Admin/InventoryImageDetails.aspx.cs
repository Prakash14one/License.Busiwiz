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
using System.IO;
using System.Text;

public partial class InventoryImageDetails : System.Web.UI.Page
{
    SqlConnection con;
    int i;
    DataSet dstable = new DataSet();
    string file;
    string file1;
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
        Session["pnlM"] = "4";
        Session["pnl4"] = "410";
        Label1.Visible = false;
        if (!IsPostBack)
        {
            Pagecontrol.dypcontrol(Page, page);
            string strwh = "SELECT WareHouseId,Name,Address,CurrencyId FROM WareHouseMaster where comid='" + Session["comid"] + "'and Status='1'";
            SqlCommand cmdwh = new SqlCommand(strwh, con);
            SqlDataAdapter adpwh = new SqlDataAdapter(cmdwh);
            DataTable dtwh = new DataTable();
            adpwh.Fill(dtwh);

            ddlWarehouse.DataSource = dtwh;
            ddlWarehouse.DataTextField = "Name";
            ddlWarehouse.DataValueField = "WareHouseId";
            ddlWarehouse.DataBind();
           // ddlWarehouse.Items.Insert(0, "Select");
            ddlWarehouse.SelectedIndex = 0;
            //FillddlInvCat();
            if (ddlWarehouse.SelectedIndex >= 0)
            {
                ddlInvCat.Items.Clear();
                FillddlInvCat();
                ddlInvCat_SelectedIndexChanged(sender, e);
            }
            ViewState["filename"] = "";
            ViewState["filename1"] = "";
        }
    }
    public DataSet fillView()
    {
        SqlCommand cmd = new SqlCommand("Sp_Select_InventoryImgViewMaster1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
        }
        catch (Exception ex)
        {

            Label1.Visible = true;
            Label1.Text = "Error";

        }
        finally { }
    }
    public void clean()
    {
        ddlInvCat.SelectedItem.Value = "0";
        ddlInvName.SelectedItem.Value = "0";
        ddlInvSubCat.SelectedItem.Value = "0";
        ddlInvSubSubCat.SelectedItem.Value = "0";
        txtSearchInvName.Text = "";

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        clean();
    }
    protected void FillddlInvCat()
    {
        ddlInvCat.DataSource = getall1();
        ddlInvCat.DataTextField = "InventoryCatName";
        ddlInvCat.DataValueField = "InventeroyCatId";
        ddlInvCat.DataBind();
        ddlInvCat.Items.Insert(0, "All");
        ddlInvCat.Items[0].Value = "0";
        //ddlInvCat.AutoPostBack = true;

    }
    public DataSet getall1()
    {
        SqlCommand Mycommand = new SqlCommand();
        DataSet ds = new DataSet();
        SqlDataAdapter MyDataAdapter = new SqlDataAdapter();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        con.Open();
        string str = "SELECT Distinct  InventoryCategoryMaster.InventeroyCatId,InventoryCategoryMaster.InventoryCatName FROM InventoryCategoryMaster inner join InventorySubCategoryMaster on InventorySubCategoryMaster.InventoryCategoryMasterId=InventoryCategoryMaster.InventeroyCatId inner join InventoruSubSubCategory on InventoruSubSubCategory.InventorySubCatID=InventorySubCategoryMaster.InventorySubCatId inner join InventoryMaster on InventoryMaster.InventorySubSubId=InventoruSubSubCategory.InventorySubSubId inner join InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.InventoryMasterId=InventoryMaster.InventoryMasterId inner join WareHouseMaster on WareHouseMaster.WareHouseId=InventoryWarehouseMasterTbl.WareHouseId  WHERE InventoryWarehouseMasterTbl.WareHouseId ='" + ddlWarehouse.SelectedValue + "'  and (InventoryCategoryMaster.CatType IS NULL)"; ;

        MyDataAdapter = new SqlDataAdapter(str, con);
        MyDataAdapter.Fill(ds);
        con.Close();
        return ds;
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
            //string strsubcat = "SELECT InventorySubCatId  ,InventorySubCatName ,InventoryCategoryMasterId  FROM InventorySubCategoryMaster " +
            //                " where InventoryCategoryMasterId = " + Convert.ToInt32(ddlInvCat.SelectedValue) + " order by InventorySubCatName";
            //SqlCommand cmdsubcat = new SqlCommand(strsubcat, con);
            //SqlDataAdapter adpsubcat = new SqlDataAdapter(cmdsubcat);
            //DataTable dtsubcat = new DataTable();
            //adpsubcat.Fill(dtsubcat);

            //ddlInvSubCat.DataSource = dtsubcat;

            //ddlInvSubCat.DataTextField = "InventorySubCatName";
            //ddlInvSubCat.DataValueField = "InventorySubCatId";
            //ddlInvSubCat.DataBind();

            ddlInvSubCat.Items.Clear();
            SqlCommand Mycommand = new SqlCommand();


            SqlDataAdapter MyDataAdapter = new SqlDataAdapter();
            Mycommand = new SqlCommand(" SELECT     InventorySubCategoryMaster.InventorySubCatId, " +
                //" (InventoryCategoryMaster.InventoryCatName + " +
                // " ':'+  "+
              " InventorySubCategoryMaster.InventorySubCatName  as category FROM      " +
              "     InventorySubCategoryMaster INNER JOIN  InventoryCategoryMaster ON InventorySubCategoryMaster. " +
              "         InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId  " +
             " Where InventoryCategoryMaster.InventeroyCatId = '" + ddlInvCat.SelectedValue + "' " +
           " ORDER BY InventoryCategoryMaster.InventoryCatName, InventorySubCategoryMaster.InventorySubCatName  ", con);

            MyDataAdapter = new SqlDataAdapter(Mycommand);
            DataSet ds1 = new DataSet();
            MyDataAdapter.Fill(ds1);
            con.Close();


            ddlInvSubCat.DataSource = ds1;
            ddlInvSubCat.DataValueField = "InventorySubCatId";
            ddlInvSubCat.DataTextField = "category";
            ddlInvSubCat.DataBind();
            ddlInvSubCat.Items.Insert(0, "All");
            ddlInvSubCat.SelectedItem.Value = "0";
        }
        else
        {
            ddlInvSubCat.DataSource = null;
            ddlInvSubCat.Items.Insert(0, "All");
            ddlInvSubCat.Items[0].Value = "0";

            ddlInvSubCat.DataBind();
        }
        
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

        //ddlInvSubSubCat.DataSource = null;
        //ddlInvSubSubCat.DataBind();
        ddlInvName.DataSource = null;
        ddlInvName.DataBind();
        ddlInvName.Items.Clear();
        if (Convert.ToInt32(ddlInvSubSubCat.SelectedIndex) > 0)
        {
            string strinvname = "SELECT InventoryMasterId ,Name ,InventoryDetailsId ,InventorySubSubId   ,ProductNo ,InventoryTypeId  FROM InventoryMaster " +
                            " where InventorySubSubId= " + Convert.ToInt32(ddlInvSubSubCat.SelectedValue) + " and InventoryMaster.MasterActiveStatus=1 order by Name ";
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
    



    public DataTable FilterInventoryId()
    {
        string strinv = " SELECT     InventoryMaster.InventoryMasterId, InventoryMaster.Name, InventoryMaster.ProductNo, InventoruSubSubCategory.InventorySubSubId, " +
                     " InventoruSubSubCategory.InventorySubSubName, InventorySubCategoryMaster.InventorySubCatId, InventorySubCategoryMaster.InventorySubCatName,  " +
                     " InventoryCategoryMaster.InventeroyCatId, InventoryCategoryMaster.InventoryCatName, InventoryBarcodeMaster.Barcode, " +
                     " Left(InventoryCategoryMaster.InventoryCatName,15)    + ' : ' + Left(InventorySubCategoryMaster.InventorySubCatName,15) + ' : ' + Left(InventoruSubSubCategory.InventorySubSubName,15)     AS CatScSsc,InventoryMeasurementUnit.Unit, UnitTypeMaster.Name as unittype " +
                     "  FROM         InventoryBarcodeMaster RIGHT OUTER JOIN " +
                     " InventoryCategoryMaster RIGHT OUTER JOIN " +
                     " InventorySubCategoryMaster ON InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId RIGHT OUTER JOIN " +
                     " InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID RIGHT OUTER JOIN " +
                     " InventoryMaster ON InventoruSubSubCategory.InventorySubSubId = InventoryMaster.InventorySubSubId ON " +
                     " InventoryBarcodeMaster.InventoryMaster_id = InventoryMaster.InventoryMasterId inner join InventoryMeasurementUnit on InventoryMaster.InventoryMasterId = InventoryMeasurementUnit.InventoryMasterId inner join UnitTypeMaster on UnitTypeMaster.UnitTypeId = InventoryMeasurementUnit.UnitType inner join InventoryWarehouseMasterTbl  on InventoryWarehouseMasterTbl.InventoryMasterId=InventoryMaster.InventoryMasterId  where InventoryWarehouseMasterTbl.WareHouseId='" + ddlWarehouse.SelectedValue + "' and InventoryCategoryMaster.compid='" + Session["comid"] + "'";
        //where InventoryCategoryMaster.compid='" + Session["comid"] + "'
        string strInvId = "";
        string cmpid = "";
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
                            strInvId = "and  InventoryMaster.InventoryMasterId=" + Convert.ToInt32(ddlInvName.SelectedValue) + "  ";

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
                    strInvCatid = " and InventoryCategoryMaster.InventeroyCatId =" + Convert.ToInt32(ddlInvCat.SelectedValue) + " ";
                    //and InventoryCategoryMaster.compid=" + Session["comid"] + "
                    //strInvId = "where  InventoryMaster.InventoryMasterId=" + Convert.ToInt32(ddlInvName.SelectedValue) + " ";

                }
            }
            else
            {
                //strInvCatid = "where InventoryCategoryMaster.InventeroyCatId =" + Convert.ToInt32(ddlInvCat.SelectedValue) + " ";
                //strInvCatid = "where InventoryCategoryMaster.InventeroyCatId =" + Convert.ToInt32(ddlInvCat.SelectedValue) + "and InventoryCategoryMaster.compid=" + Session["comid"] + " ";
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
         " FROM         InventoryMaster INNER JOIN " +
       " InventoryDetails ON InventoryMaster.InventoryDetailsId = InventoryDetails.Inventory_Details_Id INNER JOIN " +
       " InventoruSubSubCategory ON InventoryMaster.InventorySubSubId = InventoruSubSubCategory.InventorySubSubId LEFT OUTER JOIN " +
       " InventoryMeasurementUnit ON InventoryMaster.InventoryMasterId = InventoryMeasurementUnit.InventoryMasterId LEFT OUTER JOIN " +
       " InventoryLocationTbl ON InventoryMaster.InventoryMasterId = InventoryLocationTbl.InventoryWHM_Id LEFT OUTER JOIN " +
       " InventorySizeMaster ON InventoryMaster.InventoryMasterId = InventorySizeMaster.InventoryMasterId LEFT OUTER JOIN " +
      "  InventoryBarcodeMaster ON InventoryMaster.InventoryMasterId = InventoryBarcodeMaster.InventoryMaster_id " +
      " WHERE     (InventoryMaster.Name like '%" + txtSearchInvName.Text.Replace("'", "''") + "%') and InventoryMaster.MasterActiveStatus=1 ";

            SqlCommand cmd23 = new SqlCommand(str23, con);
            SqlDataAdapter adp23 = new SqlDataAdapter(cmd23);
            DataTable dt23 = new DataTable();
            adp23.Fill(dt23);

            string strId = "";
            string strInvAllIds = "";
            string strtemp = "";
            foreach (DataRow dtrrr in dt23.Rows)
            {
                strId = dtrrr["InventoryMasterId"].ToString();
                strInvAllIds = strId + "," + strInvAllIds;
                strtemp = strInvAllIds.Substring(0, (strInvAllIds.Length - 1));



            }

            strInvBySerchId = " and InventoryMaster.InventoryMasterId in (" + strtemp + ") ";
            //string mainstring = strinv + "  order by SalesChallanMaster.RefSalesOrderId ";

        }

        string mainString = strinv + strInvId + strInvsubsubCatId + strInvsubcatid + strInvCatid + strInvBySerchId + "  and InventoryMaster.MasterActiveStatus=1 ORDER BY InventoryCategoryMaster.InventoryCatName, InventorySubCategoryMaster.InventorySubCatName, InventoruSubSubCategory.InventorySubSubName, InventoryMaster.Name";


        SqlCommand cmdinv = new SqlCommand(mainString, con);
        SqlDataAdapter adpinv = new SqlDataAdapter(cmdinv);
        DataTable dtinv = new DataTable();
        adpinv.Fill(dtinv);

        return dtinv;


    }

    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        //Panel1.Visible = true;
        //DataTable dtGridFill = (DataTable)(FilterInventoryId());
        //if (dtGridFill.Rows.Count > 0)
        //{

        //    grdInvMasters.DataSource = dtGridFill;
        //    grdInvMasters.DataBind();
        //}
        //else
        //{
        //    grdInvMasters.DataSource = null;
        //    grdInvMasters.DataBind();
        //}
    }

    protected void grdInvMasters_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select1")
        {
            ViewState["InvMid"] = null;
            grdInvMasters.SelectedIndex = Convert.ToInt32(e.CommandArgument.ToString());
            int dk = Convert.ToInt32(grdInvMasters.SelectedDataKey.Value);
            //Session["view"] = dk;

            ViewState["InvMid"] = dk;
            FillGrid2DiffView();


        }
    }
    protected void FillGrid2DiffView()
    {
        SqlCommand cmdg = new SqlCommand("select ViewName,ViewID from InventoryImgViewMaster", con);
        SqlDataAdapter dtpg = new SqlDataAdapter(cmdg);
        DataTable dsg = new DataTable();
        dtpg.Fill(dsg);

        //Session["dataset"] = dsg;
        //dstable = (DataSet)Session["dataset"];
        if (dsg.Rows.Count > 0)
        {
            GridView1.DataSource = dsg;
            GridView1.DataBind();


        }
        Label lblcscssc = (Label)(grdInvMasters.Rows[grdInvMasters.SelectedIndex].FindControl("lblCategory"));
        Label lblname = (Label)(grdInvMasters.Rows[grdInvMasters.SelectedIndex].FindControl("lblInvName"));

        lblInvCScSScName.Text = lblcscssc.Text + " : " + lblname.Text;

        lblitemname.Text = lblcscssc.Text + " : " + lblname.Text;
        //BindGrid();
        pnlViewFill.Visible = true;
        int countsmall = 0;
        int countlarge = 0;
        if (GridView1.Rows.Count > 0)
        {
            foreach (GridViewRow ggghhh in GridView1.Rows)
            {
                Label invVWid = (Label)ggghhh.FindControl("lblviewid");


                string strimg = "SELECT     InventoryImgViewMaster.ViewID, InventoryImgViewMaster.ViewName, InventoryMaster.InventoryMasterId, InventoryMaster.Name, InventoryMaster.ProductNo, " +
                 " InventoryMaster.MasterActiveStatus, InventoryImgMasterDetails.InventoryImgMasterDetails_Id, InventoryImgMasterDetails.SmallImageUrl,  " +
                 " InventoryImgMasterDetails.LargeImageUrl " +
                 " FROM         InventoryImgMasterDetails LEFT OUTER JOIN " +
                 " InventoryMaster ON InventoryImgMasterDetails.InventoryMaster_Id = InventoryMaster.InventoryMasterId RIGHT OUTER JOIN " +
                 " InventoryImgViewMaster ON InventoryImgMasterDetails.ViewID = InventoryImgViewMaster.ViewID " +
                 " where InventoryMaster.InventoryMasterId='" + ViewState["InvMid"].ToString() + "' and  InventoryImgViewMaster.ViewID='" + invVWid.Text + "'  ";
                SqlCommand cmd = new SqlCommand(strimg, con);
                SqlDataAdapter adpimg = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adpimg.Fill(dt);

                Image smImg = (Image)ggghhh.FindControl("imgsmall");
                Label smLbl = (Label)ggghhh.FindControl("lblSmallImageText");
                Image lrgImg = (Image)ggghhh.FindControl("imglarge");
                Label lrgLbl = (Label)ggghhh.FindControl("lblLargeImageText");
                Label imgdid = (Label)ggghhh.FindControl("lblinvImgMdid");
                Label lblsize = (Label)ggghhh.FindControl("lblsize");
              
                Label lbllargesize = (Label)ggghhh.FindControl("lbllargesize");
                ImageButton delesmallimg = (ImageButton)ggghhh.FindControl("delesmallimg");
                ImageButton delelargeimg = (ImageButton)ggghhh.FindControl("delelargeimg");
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["SmallImageUrl"].ToString() != "")
                    {
                        smImg.Visible = true;
                        smImg.ImageUrl = "~/Account/" + dt.Rows[0]["SmallImageUrl"].ToString();
                    }
                    if (dt.Rows[0]["LargeImageUrl"].ToString() != "")
                    {
                        lrgImg.Visible = true;
                        lrgImg.ImageUrl = "~/Account/" + dt.Rows[0]["LargeImageUrl"].ToString();
                    }
                }
                if (smImg != null && smLbl != null && lrgLbl != null && lrgImg != null && imgdid != null )
                {
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["SmallImageUrl"].ToString() != "")
                        {
                            smImg.Visible = true;
                            smImg.ImageUrl = "~/Account/" + dt.Rows[0]["SmallImageUrl"].ToString();
                            
                            //smImg.Width = 100;
                            //smImg.Height = 100;
                            smLbl.Visible = true;
                            
                            smLbl.Text = dt.Rows[0]["SmallImageUrl"].ToString();
                            imgdid.Text = dt.Rows[0]["InventoryImgMasterDetails_Id"].ToString();
                            int filelength = 0;
                            int width = 0;
                            int height = 0;
                            FileInfo imgfile = new FileInfo(MapPath(smImg.ImageUrl));
                            bool smimgexist = imgfile.Exists;
                            if (smimgexist == true)
                            {
                                FileStream fs = new FileStream(MapPath(smImg.ImageUrl), FileMode.Open, FileAccess.Read, FileShare.Read);
                                System.Drawing.Image img = System.Drawing.Image.FromStream(fs);

                                filelength = Convert.ToInt32(fs.Length / 1024);
                                lblsize.Text = filelength.ToString();
                                width = img.Width;
                                height = img.Height;
                            }
                            else
                            {
                                lblsize.Text = filelength.ToString();
                            }
                        }
                        else
                        {
                            smLbl.Visible = true;
                            //smImg.Visible = true;
                            // smImg.ImageUrl = "~/ShoppingCart/ViewSmallImg/" + dt.Rows[0]["SmallImageUrl"].ToString();
                            //smLbl.Text = dt.Rows[0]["SmallImageUrl"].ToString();
                            smLbl.Text = " No Image Available";
                            delesmallimg.Visible = false;
                        }
                        if (dt.Rows[0]["LargeImageUrl"].ToString() != "")
                        {
                            lrgImg.Visible = true;
                            lrgImg.ImageUrl = "~/Account/" + dt.Rows[0]["LargeImageUrl"].ToString();
                           
                            //lrgImg.Width = 100;
                            //lrgImg.Height = 100;
                            lrgLbl.Visible = true;
                            lrgLbl.Text = dt.Rows[0]["LargeImageUrl"].ToString();
                            imgdid.Text = dt.Rows[0]["InventoryImgMasterDetails_Id"].ToString();
                            int filelength1 = 0;
                            int width1 = 0;
                            int height1 = 0;
                            FileInfo imgfile1 = new FileInfo(MapPath(lrgImg.ImageUrl));
                            bool lrgimgexist = imgfile1.Exists;
                            if (lrgimgexist == true)
                            {
                                FileStream fs = new FileStream(MapPath(lrgImg.ImageUrl), FileMode.Open, FileAccess.Read, FileShare.Read);
                                System.Drawing.Image img1 = System.Drawing.Image.FromStream(fs);

                                filelength1 = Convert.ToInt32(fs.Length / 1024);
                                lbllargesize.Text = filelength1.ToString();
                                width1 = img1.Width;
                                height1 = img1.Height;
                            }
                            else
                            {
                                lbllargesize.Text = filelength1.ToString();
                            }
                        }
                        else
                        {
                            lrgLbl.Visible = true;
                            //lrgImg.Visible = true;
                            // lrgImg.ImageUrl = "~/ShoppingCart/ViewLargeImg/" + dt.Rows[0]["LargeImageUrl"].ToString();
                            lrgLbl.Text = " No Image Available";
                            delelargeimg.Visible = false;
                        }
                    }
                    else
                    {
                        smImg.Visible = false;
                        lrgImg.Visible = false;
                        smLbl.Visible = true;
                        lrgLbl.Visible = true;
                        smLbl.Text = " No Image Available";
                        lrgLbl.Text = " No Image Available";
                        delesmallimg.Visible = false;
                        delelargeimg.Visible = false;
                    }
                }

              
                
            }
            foreach (GridViewRow ggh in GridView1.Rows)
            {
                Label lblsize = (Label)ggh.FindControl("lblsize");
                Label lbllargesize = (Label)ggh.FindControl("lbllargesize");
               
                countsmall = countsmall + Convert.ToInt32(lblsize.Text);
                countlarge = countlarge + Convert.ToInt32(lbllargesize.Text);   
            }
            Label lbltotalsmall = (Label)GridView1.FooterRow.FindControl("lbltotalsmall");
            Label lbltotallarge = (Label)GridView1.FooterRow.FindControl("lbltotallarge");
           
            lbltotalsmall.Text = countsmall.ToString() + " KB";
            lbltotallarge.Text = countlarge.ToString() + " KB";
        }
    }

    public void BindGrid()
    {
        ////if (ViewState["InvMid"] != null)
        ////{
        ////    if (ViewState["InvMid"].ToString() != "")
        ////    {
        ////        pnlViewFill.Visible = true;
        ////        string str = "SELECT     InventoryImgMasterDetails.SmallImageUrl, InventoryImgMasterDetails.LargeImageUrl, InventoryImgViewMaster.ViewName,  " +
        ////                      " InventoryImgMasterDetails.InventoryImgMasterDetails_Id " +
        ////                        " FROM         InventoryImgMasterDetails INNER JOIN " +
        ////                      " InventoryImgViewMaster ON InventoryImgMasterDetails.ViewID = InventoryImgViewMaster.ViewID " +
        ////                        " WHERE     (InventoryImgMasterDetails.InventoryMaster_Id = '" + ViewState["InvMid"].ToString() + "')";
        ////        SqlCommand cmd = new SqlCommand(str, con);

        ////        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        ////        DataSet ds = new DataSet();
        ////        adp.Fill(ds);

        ////        GridView1.DataSource = ds;
        ////        GridView1.DataBind();

        SqlCommand cmdgtab = new SqlCommand("select ViewName,ViewID from InventoryImgViewMaster", con);
        SqlDataAdapter dtpgtab = new SqlDataAdapter(cmdgtab);
        DataSet dsgtab = new DataSet();
        dtpgtab.Fill(dsgtab);

        GridView1.DataSource = dsgtab;
        GridView1.DataBind();


        ////    }
        //foreach(GridViewRow gdr in GridView1.Rows)
        //{
        //    GridView gd = (GridView)gdr.Cells[0].FindControl("gridchk");
        //    gd.DataSource = (DataSet)fillView();
        //    gd.DataBind();
        //}


        //GridView1.DataSource = (DataSet)fillView();
        //GridView1.DataBind();

    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{

        //    Label lbl = ((Label)(e.Row.FindControl("lblviewid")));

        //    SqlCommand cmdview = new SqlCommand("Select SmallImageUrl,LargeImageUrl from InventoryImgMasterDetails where InventoryMaster_Id='" + Session["view"].ToString() + "' and ViewID='" + lbl.Text + "'", con);
        //    SqlDataAdapter dtpview = new SqlDataAdapter(cmdview);
        //    DataSet dsimg = new DataSet();

        //    dtpview.Fill(dsimg);

        //    Image img = ((Image)(e.Row.FindControl("imgsmall")));
        //    Image img1 = ((Image)(e.Row.FindControl("imglarge")));
        //    if (dsimg.Tables[0].Rows.Count > 0)
        //    {                
        //        img.ImageUrl = "~/ShoppingCart/" + dsimg.Tables[0].Rows[0]["SmallImageUrl"].ToString();
        //        img1.ImageUrl = "~/ShoppingCart/" + dsimg.Tables[0].Rows[0]["LargeImageUrl"].ToString();
        //    }
        //    else
        //    {
        //        img.Visible = false;
        //        img1.Visible = false;
        //        Label lbsmallimage = ((Label)(e.Row.FindControl("lblSmallImageText")));
        //        Label lbsmallimage1 = ((Label)(e.Row.FindControl("lblLargeImageText")));
        //        lbsmallimage.Visible = true;
        //        lbsmallimage1.Visible = true;
        //    }   
        //}
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;

        //GridView1.DataSource = dstable;
        //GridView1.DataBind();
        FillGrid2DiffView();

    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        FillGrid2DiffView();
        //GridView1.DataSource = dstable;
        //GridView1.DataBind();
        //BindGrid();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridViewRow ggghhh = GridView1.Rows[e.RowIndex];
        Image smImg = (Image)ggghhh.FindControl("imgsmall");
        Label smLbl = (Label)ggghhh.FindControl("lblSmallImageText");
        Image lrgImg = (Image)ggghhh.FindControl("imglarge");
        Label lrgLbl = (Label)ggghhh.FindControl("lblLargeImageText");
        Label lblimgdid = (Label)ggghhh.FindControl("lblinvImgMdid");


        if (smImg != null && smLbl != null && lrgLbl != null && lrgImg != null && lblimgdid != null)
        {
            if (smLbl.Text == " No Image Available")
            {
                Label1.Text = "There are no records to delete";
                Label1.Visible = true;
            }
            else
            {
                string dlesm = " update    InventoryImgMasterDetails set SmallImageUrl=''  where InventoryImgMasterDetails_Id='" + lblimgdid.Text + "'  ";
                SqlCommand cmdsmupddd = new SqlCommand(dlesm, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdsmupddd.ExecuteNonQuery();
                con.Close();
                Label1.Text = "Record deleted successfully";
                Label1.Visible = true;

            }


            if (lrgLbl.Text == " No Image Available")
            {
                Label1.Text = "There are no records to delete";
                Label1.Visible = true;

            }
            else
            {
                string dlesm1 = " update    InventoryImgMasterDetails set LargeImageUrl=''  where InventoryImgMasterDetails_Id='" + lblimgdid.Text + "'  ";
                //string dlesm = " update    InventoryImgMasterDetails set SmallImageUrl=''  where InventoryImgMasterDetails_Id='" + lblimgdid.Text + "'  ";
                SqlCommand cmdsmupddd1 = new SqlCommand(dlesm1, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdsmupddd1.ExecuteNonQuery();
                con.Close();
                Label1.Visible = true;
                Label1.Text = "Record deleted successfully";
            }

        }

        GridView1.EditIndex = -1;

        //GridView1.DataSource = dstable;
        //GridView1.DataBind();
        FillGrid2DiffView();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        
        GridViewRow gdr = GridView1.Rows[e.RowIndex];

        FileUpload fpdsm = (FileUpload)gdr.FindControl("FileUploadSmallImage");
        FileUpload fpdlrg = (FileUpload)gdr.FindControl("FileUploadLargeImage");
        Image imgsmalled = (Image)gdr.FindControl("imgsmall");
        Image imglargeed = (Image)gdr.FindControl("imglarge");
        string id = ((Label)gdr.FindControl("lblviewid")).Text;
        //String filename = "";
        //String filename1 = "";
        //if (fpdsm.HasFile)
        //{
        //    bool valid = ext(fpdsm.FileName);
        //    if (valid == true)
        //    {
        //        filename = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + "_" + fpdsm.FileName;
        //        fpdsm.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\ViewSmallImg\\") + filename);
        //        string fileSmallName = Session["comid"] + "/ViewSmallImg/" + filename;
        //    }
        //    else
        //    {
        //        Label1.Visible = true;
        //        Label1.Text = "This is not Valid File";
        //    }
        //}
        //if (fpdlrg.HasFile)
        //{
        //    bool valid = ext(fpdlrg.FileName);
        //    if (valid == true)
        //    {
        //        filename1 = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "_" + fpdlrg.FileName;
        //        fpdlrg.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\ViewLargeImg\\") + filename1);
        //        string file1LargeName = Session["comid"] + "/ViewLargeImg/" + filename1;

        //    }
        //    else
        //    {
        //        Label1.Visible = true;
        //        Label1.Text = "This is not Valid File";
        //    }
        //}

        string ssssss = "SELECT     InventoryImgViewMaster.ViewID, InventoryImgViewMaster.ViewName, InventoryMaster.InventoryMasterId, InventoryMaster.Name, InventoryMaster.ProductNo, " +
             " InventoryMaster.MasterActiveStatus, InventoryImgMasterDetails.InventoryImgMasterDetails_Id, InventoryImgMasterDetails.SmallImageUrl,  " +
             " InventoryImgMasterDetails.LargeImageUrl " +
             " FROM         InventoryImgMasterDetails LEFT OUTER JOIN " +
             "  InventoryMaster ON InventoryImgMasterDetails.InventoryMaster_Id = InventoryMaster.InventoryMasterId RIGHT OUTER JOIN " +
             "  InventoryImgViewMaster ON InventoryImgMasterDetails.ViewID = InventoryImgViewMaster.ViewID " +
             " where InventoryMaster.InventoryMasterId='" + ViewState["InvMid"].ToString() + "' and  InventoryImgViewMaster.ViewID='" + id + "'  ";

        SqlCommand cmdfind = new SqlCommand(ssssss, con);
        SqlDataAdapter dtpfind = new SqlDataAdapter(cmdfind);
        DataTable dsfind = new DataTable();
        dtpfind.Fill(dsfind);

        if (dsfind.Rows.Count > 0)
        {
            //dtpfind
            if (dsfind.Rows[0]["InventoryImgMasterDetails_Id"].ToString() != null)
            {
                if (imgsmalled.ImageUrl != "")
                {
                    if (ViewState["filename"] != "" && ViewState["filename"] != null)
                    {
                        string fileSmallName = ViewState["filename"].ToString();

                        string strimg = "select SmallImageUrl from InventoryImgMasterDetails where SmallImageUrl='" + fileSmallName + "' and ViewID='" + id + "'";

                        SqlCommand cmdimg = new SqlCommand(strimg, con);
                        SqlDataAdapter adpimg = new SqlDataAdapter(cmdimg);
                        DataTable dtimg = new DataTable();
                        adpimg.Fill(dtimg);

                        if (dtimg.Rows.Count > 0)
                        {
                            string fileimg = ViewState["filename"].ToString();
                            string fileSmallName1 = Session["comid"] + "/ViewSmallImg/";
                            //string fileSmallName = "Thumbnail/" + fpdsm.FileName.ToString();

                            //bool valid = ext(fpdsm.FileName);
                            //if (valid == true)
                            // {
                            string st = "Update InventoryImgMasterDetails set SmallImageUrl='" + fileSmallName + "' where InventoryImgMasterDetails_Id='" + dsfind.Rows[0]["InventoryImgMasterDetails_Id"].ToString() + "' and ViewID='" + id + "'";
                            //SqlCommand cmdup = new SqlCommand("Update InventoryImgMaster set Thumbnail='" + fileSmallName1 + "" + 1 + "" + fileimg + "' where InventoryImgMasterID='" + dsfind.Rows[0]["InventoryImgMasterID"].ToString() + "'", con);
                            SqlCommand cmdup = new SqlCommand(st, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmdup.ExecuteNonQuery();
                            con.Close();
                            Label1.Text = "Record updated successfully";
                            Label1.Visible = true;
                            ViewState["filename"] = "";
                            // fpdsm.PostedFile.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\ViewSmallImg\\") + 1 + filename);
                            //}
                            //else
                            //{
                            //    Label1.Visible = true;
                            //    Label1.Text = "This is not Valid File";
                            //}
                        }
                        else
                        {
                            //string fileSmallName = "ViewSmallImg/" + fpdsm.FileName.ToString();

                            // bool valid = ext(fpdsm.FileName);
                            //if (valid == true)
                            //{
                            SqlCommand cmdup = new SqlCommand("Update InventoryImgMasterDetails set SmallImageUrl='" + fileSmallName + "' where InventoryImgMasterDetails_Id='" + dsfind.Rows[0]["InventoryImgMasterDetails_Id"].ToString() + "' and ViewID='" + id + "'", con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmdup.ExecuteNonQuery();
                            con.Close();
                            Label1.Text = "Record updated successfully";
                            Label1.Visible = true;
                            ViewState["filename"] = "";
                            // fpdsm.PostedFile.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\ViewSmallImg\\") + filename);

                            //}
                            //else
                            //{
                            //    Label1.Visible = true;
                            //    Label1.Text = "This is not Valid File";
                            //}
                        }
                    }
                }
                if (imglargeed.ImageUrl != "")
                {
                    if (ViewState["filename1"] != "" && ViewState["filename1"] != null)
                    {
                        string file1LargeName = ViewState["filename1"].ToString();
                        string strimg = "select LargeImageUrl from InventoryImgMasterDetails where LargeImageUrl='" + file1LargeName + "' and ViewID='" + id + "'";
                        SqlCommand cmdimg = new SqlCommand(strimg, con);
                        SqlDataAdapter adpimg = new SqlDataAdapter(cmdimg);
                        DataTable dtimg1 = new DataTable();
                        adpimg.Fill(dtimg1);
                        if (dtimg1.Rows.Count > 0)
                        {
                            string fileimg = ViewState["filename1"].ToString();
                            string file1LargeName1 = Session["comid"] + "/ViewLargeImg/";
                            //string fileSmallName = "Thumbnail/" + fpdsm.FileName.ToString();

                            //bool valid = ext(fpdlrg.FileName);
                            //if (valid == true)
                            //{
                            string st = "Update InventoryImgMasterDetails set LargeImageUrl='" + file1LargeName +"' where InventoryImgMasterDetails_Id='" + dsfind.Rows[0]["InventoryImgMasterDetails_Id"].ToString() + "' and ViewID='" + id + "'";

                            SqlCommand cmdup = new SqlCommand(st, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmdup.ExecuteNonQuery();
                            con.Close();
                            Label1.Text = "Record updated successfully";
                            Label1.Visible = true;
                            ViewState["filename1"] = "";
                            // fpdlrg.PostedFile.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\ViewLargeImg\\") + 1 + filename1);

                            //}
                            //else
                            //{
                            //    Label1.Visible = true;
                            //    Label1.Text = "This is not Valid File";
                            //}
                        }
                        else
                        {
                            // string file1LargeName = "ViewLargeImg/" + fpdlrg.FileName.ToString();

                            //bool valid = ext(fpdlrg.FileName);
                            // if (valid == true)
                            //{
                            SqlCommand cmdup = new SqlCommand("Update InventoryImgMasterDetails set LargeImageUrl='" + file1LargeName + "' where InventoryImgMasterDetails_Id='" + dsfind.Rows[0]["InventoryImgMasterDetails_Id"].ToString() + "' and ViewID='" + id + "'", con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmdup.ExecuteNonQuery();
                            con.Close();
                            Label1.Text = "Record updated successfully";
                            Label1.Visible = true;
                            ViewState["filename1"] = "";
                            // fpdlrg.PostedFile.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\ViewLargeImg\\") + filename1);
                            //  }
                            //else
                            //{
                            //    Label1.Visible = true;
                            //    Label1.Text = "This is not Valid File";
                            //}
                        }
                    }
                }
                if (fpdsm.HasFile)
                {
                    bool valid = ext(fpdsm.FileName);
                    if (valid == true)
                    {
                        string filename;
                        filename = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + "_" + fpdsm.FileName;
                        fpdsm.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\ViewSmallImg\\") + filename);
                        string fileSmallName = Session["comid"] + "/ViewSmallImg/" + filename;
                        string strimg = "select SmallImageUrl from InventoryImgMasterDetails where SmallImageUrl='" + fileSmallName + "' and ViewID='"+ id +"'";

                        SqlCommand cmdimg = new SqlCommand(strimg, con);
                        SqlDataAdapter adpimg = new SqlDataAdapter(cmdimg);
                        DataTable dtimg = new DataTable();
                        adpimg.Fill(dtimg);

                        if (dtimg.Rows.Count > 0)
                        {
                            
                            string fileSmallName1 = Session["comid"] + "/ViewSmallImg/";

                            string st = "Update InventoryImgMasterDetails set SmallImageUrl='" + fileSmallName1 + "" + filename + "' where InventoryImgMasterDetails_Id='" + dsfind.Rows[0]["InventoryImgMasterDetails_Id"].ToString() + "' and ViewID='" + id + "'";                            
                            SqlCommand cmdup = new SqlCommand(st, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmdup.ExecuteNonQuery();
                            con.Close();
                            Label1.Text = "Record updated successfully";
                            Label1.Visible = true;
                           
                        }
                        else
                        {
                            
                            SqlCommand cmdup = new SqlCommand("Update InventoryImgMasterDetails set SmallImageUrl='" + fileSmallName + "' where InventoryImgMasterDetails_Id='" + dsfind.Rows[0]["InventoryImgMasterDetails_Id"].ToString() + "' and ViewID='" + id + "'", con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmdup.ExecuteNonQuery();
                            con.Close();
                            Label1.Text = "Record updated successfully";
                            Label1.Visible = true;
                           
                        }
                    }
                    else
                    {
                        Label1.Visible = true;
                        Label1.Text = "This is not Valid File";
                    }
                }
                if (fpdlrg.HasFile)
                {
                    bool valid = ext(fpdlrg.FileName);
                    if (valid == true)
                    {   
                        string filename1 = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "_" + fpdlrg.FileName;
                        fpdlrg.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\ViewLargeImg\\") + filename1);
                        string file1LargeName = Session["comid"] + "/ViewLargeImg/" + filename1;
                        
                        string strimg = "select LargeImageUrl from InventoryImgMasterDetails where LargeImageUrl='" + file1LargeName + "' and ViewID='" + id + "'";
                        SqlCommand cmdimg = new SqlCommand(strimg, con);
                        SqlDataAdapter adpimg = new SqlDataAdapter(cmdimg);
                        DataTable dtimg1 = new DataTable();
                        adpimg.Fill(dtimg1);
                        if (dtimg1.Rows.Count > 0)
                        {
                            
                            string file1LargeName1 = Session["comid"] + "/ViewLargeImg/";

                            string st = "Update InventoryImgMasterDetails set LargeImageUrl='" + file1LargeName1 + "" + filename1 + "' where InventoryImgMasterDetails_Id='" + dsfind.Rows[0]["InventoryImgMasterDetails_Id"].ToString() + "' and ViewID='" + id + "'";

                            SqlCommand cmdup = new SqlCommand(st, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmdup.ExecuteNonQuery();
                            con.Close();
                            Label1.Text = "Record updated successfully";
                            Label1.Visible = true;
                            
                        }
                        else
                        {
                           
                            SqlCommand cmdup = new SqlCommand("Update InventoryImgMasterDetails set LargeImageUrl='" + file1LargeName + "' where InventoryImgMasterDetails_Id='" + dsfind.Rows[0]["InventoryImgMasterDetails_Id"].ToString() + "' and ViewID='" + id + "'", con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmdup.ExecuteNonQuery();
                            con.Close();
                            Label1.Text = "Record updated successfully";
                            Label1.Visible = true;
                            
                        }

                    }
                    else
                    {
                        Label1.Visible = true;
                        Label1.Text = "This is not Valid File";
                    }
                }
            }
        }
        else
        {
            if (imgsmalled.ImageUrl != "")
            {
                //if (fpdsm.HasFile != false || fpdlrg.HasFile != false)
                // {
                string fileSmallName = Session["comid"] + "/ViewSmallImg/" + ViewState["filename"];
                if (imglargeed.ImageUrl != "")
                {
                    string file1LargeName = Session["comid"] + "/ViewLargeImg/" + ViewState["filename1"];
                    
                    string insertstrinImgDe = "Insert into InventoryImgMasterDetails(InventoryMaster_Id,SmallImageUrl,LargeImageUrl,ViewID) values('" + ViewState["InvMid"].ToString() + "', '" + fileSmallName + "','" + file1LargeName + "','" + id + "')";
                    SqlCommand cmdint = new SqlCommand(insertstrinImgDe, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdint.ExecuteNonQuery();
                    con.Close();
                }
                else
                {
                    string insertstrinImgDe = "Insert into InventoryImgMasterDetails(InventoryMaster_Id,SmallImageUrl,ViewID) values('" + ViewState["InvMid"].ToString() + "', '" + fileSmallName + "','" + id + "')";
                    SqlCommand cmdint = new SqlCommand(insertstrinImgDe, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdint.ExecuteNonQuery();
                    con.Close();
                }
            }
            else if (imglargeed.ImageUrl != "")                            
            {
                string file1LargeName = Session["comid"] + "/ViewLargeImg/" + ViewState["filename1"];
                string insertstrinImgDet = "Insert into InventoryImgMasterDetails([InventoryMaster_Id],[LargeImageUrl],[ViewID]) values('" + ViewState["InvMid"].ToString() + "','" + file1LargeName + "','" + id + "')";
                SqlCommand cmdintt = new SqlCommand(insertstrinImgDet, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdintt.ExecuteNonQuery();
                con.Close();
            }
            
            if (fpdsm.HasFile != false )
            {
                string filename;
                filename = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + "_" + fpdsm.FileName;
                fpdsm.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\ViewSmallImg\\") + filename);
                string fileSmallName = Session["comid"] + "/ViewSmallImg/" + filename;
                if (fpdlrg.HasFile != false)
                {
                    string filename1 = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "_" + fpdlrg.FileName;
                    fpdlrg.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\ViewLargeImg\\") + filename1);
                    string file1LargeName = Session["comid"] + "/ViewLargeImg/" + filename1;
                    string insertstrinImgDe = "Insert into InventoryImgMasterDetails(InventoryMaster_Id,SmallImageUrl,LargeImageUrl,ViewID) values('" + ViewState["InvMid"].ToString() + "', '" + fileSmallName + "','" + file1LargeName + "','" + id + "')";
                    SqlCommand cmdint = new SqlCommand(insertstrinImgDe, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdint.ExecuteNonQuery();
                    con.Close();
                }
                else
                {
                    string insertstrinImgDe = "Insert into InventoryImgMasterDetails(InventoryMaster_Id,SmallImageUrl,ViewID) values('" + ViewState["InvMid"].ToString() + "', '" + fileSmallName + "','" + id + "')";
                    SqlCommand cmdint = new SqlCommand(insertstrinImgDe, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdint.ExecuteNonQuery();
                    con.Close();
                }
            }
            else if (fpdlrg.HasFile != false)
            {
                string filename1 = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "_" + fpdlrg.FileName;
                fpdlrg.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\ViewLargeImg\\") + filename1);
                string file1LargeName = Session["comid"] + "/ViewLargeImg/" + filename1;
                string insertstrinImgDe = "Insert into InventoryImgMasterDetails(InventoryMaster_Id,LargeImageUrl,ViewID) values('" + ViewState["InvMid"].ToString() + "','" + file1LargeName + "','" + id + "')";
                SqlCommand cmdint = new SqlCommand(insertstrinImgDe, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdint.ExecuteNonQuery();
                con.Close();
            }
            

                // bool valid = ext(fpdsm.FileName);
                // if (valid == true)
                // {
                // bool valid1 = ext(fpdlrg.FileName);
                // if (valid1 == true)
                // {
          
               
                   
                   
                    // fpdsm.PostedFile.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\ViewSmallImg\\") + filename);
                    //  fpdlrg.PostedFile.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\ViewLargeImg\\") + filename1);
                    // }
                    // else
                    // {
                    //  Label1.Visible = true;
                    //   Label1.Text = "This is not Valid File";

                    // }
                    //}
                    //else
                    //{
                    // Label1.Visible = true;
                    // Label1.Text = "This is not Valid File";
                    // }
                
               
            }

            GridView1.EditIndex = -1;            
            FillGrid2DiffView();
    }
            
    public bool ext(string filename)
    {
        string[] validFileTypes = { "gif", "png", "jpg", "jpeg", "JPG", "JPEG" };

        string ext = System.IO.Path.GetExtension(filename);

        bool isValidFile = false;

        for (int i = 0; i < validFileTypes.Length; i++)
        {

            if (ext == "." + validFileTypes[i])
            {

                isValidFile = true;

                break;

            }

        }
        return isValidFile;
    }
    protected void ddlInvName_SelectedIndexChanged(object sender, EventArgs e)
    {


    }

    protected void imgBtnSearchGo_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        DataTable dtGridFill = (DataTable)(FilterInventoryId());
        if (dtGridFill.Rows.Count > 0)
        {

            grdInvMasters.DataSource = dtGridFill;
            grdInvMasters.DataBind();
        }
        else
        {
            grdInvMasters.DataSource = null;
            grdInvMasters.DataBind();
        }
        lblcomname.Text = Session["Cname"].ToString();
        lblbusiness.Text = ddlWarehouse.SelectedItem.Text;
       
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/ShoppingCart/Admin/wzInventoryImgMaster.aspx");
    }
    protected void ddlInvName_SelectedIndexChanged1(object sender, EventArgs e)
    {

    }
    protected void ddlWarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlInvCat.Items.Clear();
        FillddlInvCat();
        ddlInvCat_SelectedIndexChanged(sender, e);
    }
    protected void btncancel0_Click(object sender, EventArgs e)
    {
        Button btnslide = (Button)GridView1.HeaderRow.Cells[1].FindControl("Buttonsmallslideshow");
        Button btnslidelarge = (Button)GridView1.HeaderRow.Cells[1].FindControl("Buttonlargeslideshow");
        if (btncancel0.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");

            btncancel0.Text = "Hide Printable Version";
            Button2.Visible = true;
            
            if (GridView1.Columns[5].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[5].Visible = false;
            }
            if (GridView1.Columns[6].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                GridView1.Columns[6].Visible = false;
            }
            btnslide.Visible = false;
            btnslidelarge.Visible = false;
            foreach (GridViewRow row in GridView1.Rows)
            {
                
                ImageButton delesmallimg = (ImageButton)row.FindControl("delesmallimg");
                delesmallimg.Visible = false;
                ImageButton delelargeimg = (ImageButton)row.FindControl("delelargeimg");
                delelargeimg.Visible = false;
            }
        }
        else
        {

            //pnlgrid.ScrollBars = ScrollBars.Vertical;
            //pnlgrid.Height = new Unit(300);

            btncancel0.Text = "Printable Version";
            Button2.Visible = false;
            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[5].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                GridView1.Columns[6].Visible = true;
            }
            foreach (GridViewRow row in GridView1.Rows)
            {
                
                ImageButton delesmallimg = (ImageButton)row.FindControl("delesmallimg");
                delesmallimg.Visible = true;
                ImageButton delelargeimg = (ImageButton)row.FindControl("delelargeimg");
                delelargeimg.Visible = true;
            }
            btnslide.Visible = true;
            btnslidelarge.Visible = true;
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
       
        if (e.CommandName == "DeleteSmall")
        {
            GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
            Image smImg = (Image)row.FindControl("imgsmall");
            Label smLbl = (Label)row.FindControl("lblSmallImageText");
           
            Label lblimgdid = (Label)row.FindControl("lblinvImgMdid");
            if (smImg != null && smLbl != null && lblimgdid != null)
            {
                if (smLbl.Text == " No Image Available")
                {

                }
                else
                {
                    string dlesm = " update InventoryImgMasterDetails set SmallImageUrl=''  where InventoryImgMasterDetails_Id='" + lblimgdid.Text + "'  ";
                    SqlCommand cmdsmupddd = new SqlCommand(dlesm, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdsmupddd.ExecuteNonQuery();
                    con.Close();
                    Label1.Text = "Record deleted successfully";
                    Label1.Visible = true;

                }
               
            }
            FillGrid2DiffView();
        }
        if (e.CommandName == "DeleteLarge")
        {
            GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
            Image lrgImg = (Image)row.FindControl("imglarge");
            Label lrgLbl = (Label)row.FindControl("lblLargeImageText");
            Label lblimgdid = (Label)row.FindControl("lblinvImgMdid");
            if (lrgLbl != null && lrgImg != null && lblimgdid != null)
            {
                if (lrgLbl.Text == " No Image Available")
                {


                }
                else
                {
                    string dlesm1 = " update InventoryImgMasterDetails set LargeImageUrl=''  where InventoryImgMasterDetails_Id='" + lblimgdid.Text + "'  ";
                    //string dlesm = " update    InventoryImgMasterDetails set SmallImageUrl=''  where InventoryImgMasterDetails_Id='" + lblimgdid.Text + "'  ";
                    SqlCommand cmdsmupddd1 = new SqlCommand(dlesm1, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdsmupddd1.ExecuteNonQuery();
                    con.Close();
                    Label1.Visible = true;
                    Label1.Text = "Record deleted successfully";
                }
            }
            FillGrid2DiffView();
        }
        if (e.CommandName == "addsmall")
        {
            GridViewRow gdr = (GridViewRow)((Button)e.CommandSource).NamingContainer;

            FileUpload fpdsm = (FileUpload)gdr.FindControl("FileUploadSmallImage");

            Image imgsmalled = (Image)gdr.FindControl("imgsmall");
            string id = ((Label)gdr.FindControl("lblviewid")).Text;
            String filename = "";

            if (fpdsm.HasFile)
            {
                bool valid = ext(fpdsm.FileName);
                if (valid == true)
                {
                    filename = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + "_" + fpdsm.FileName;
                    fpdsm.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\ViewSmallImg\\") + filename);
                    string fileSmallName = Session["comid"] + "/ViewSmallImg/" + filename;
                    ViewState["filename"] = fileSmallName;
                    imgsmalled.Visible = true;
                    imgsmalled.ImageUrl = "~/Account/" + fileSmallName;
                }
                else
                {
                    Label1.Visible = true;
                    Label1.Text = "This is not Valid File";
                }
            }
            else
            {
                ViewState["filename"] = "";
            }         
        }
        
        if (e.CommandName == "addLarge")
        {
            GridViewRow gdr = (GridViewRow)((Button)e.CommandSource).NamingContainer;
            FileUpload fpdlrg = (FileUpload)gdr.FindControl("FileUploadLargeImage");
            Image imglargeed = (Image)gdr.FindControl("imglarge");
            string id = ((Label)gdr.FindControl("lblviewid")).Text;
            String filename1 = "";
            if (fpdlrg.HasFile)
            {
                bool valid = ext(fpdlrg.FileName);
                if (valid == true)
                {
                    filename1 = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "_" + fpdlrg.FileName;
                    fpdlrg.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\ViewLargeImg\\") + filename1);
                    string file1LargeName = Session["comid"] + "/ViewLargeImg/" + filename1;
                    ViewState["filename1"] = file1LargeName;
                    imglargeed.Visible = true;
                    imglargeed.ImageUrl = "~/Account/" + file1LargeName;
                }
                else
                {
                    Label1.Visible = true;
                    Label1.Text = "This is not Valid File";
                }
            }
            else
            {
                ViewState["filename1"] = "";
            }
        }
       
        
    }
   
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        Button lk = (Button)sender;
        //int j = Convert.ToInt32(lk.CommandArgument);
        string url = "../NormalView.aspx?ProductID=" + ViewState["InvMid"];
        StringBuilder sb = new StringBuilder();
        sb.Append("<script type='text/javascript'>");
        sb.Append("window.open('");
        sb.Append(url);
        sb.Append("');");
        sb.Append("</script>");
        ClientScript.RegisterStartupScript(this.GetType(),"script",sb.ToString());
        //Response.Redirect(",);
        //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void LinkButton5_Click(object sender, EventArgs e)
    {
        Button lk = (Button)sender;
        //int j = Convert.ToInt32(lk.CommandArgument);
        
        string url = "../EnhanceView.aspx?ProductID=" + ViewState["InvMid"];
        StringBuilder sb = new StringBuilder();
        sb.Append("<script type='text/javascript'>");
        sb.Append("window.open('");
        sb.Append(url);
        sb.Append("');");
        sb.Append("</script>");
        ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
        // ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    
}



