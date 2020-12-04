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

public partial class ShoppingCart_Admin_UpdateImageOfItem : System.Web.UI.Page
{
    SqlConnection con;
    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
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
        Page.Title = pg.getPageTitle(page);
       
        if (!IsPostBack)
        {
            ViewState["sortOrder"] = "";
            lblmsg.Text = "";
            fillstore();
            ddlcategory.Items.Insert(0, "All");
            ddlcategory.Items[0].Value = "0";

            ddlSubCategory.Items.Insert(0, "All");
            ddlSubCategory.Items[0].Value = "0";

            ddlSubSubCategory.Items.Insert(0, "All");
            ddlSubSubCategory.Items[0].Value = "0";
            
            ddlwarehouse_SelectedIndexChanged(sender, e);
            ImageButton1_Click(sender, e);

        }

    }

    public DataTable fillddl2()
    {

        {
            
            string str = "select distinct InventoryCategoryMaster.InventeroyCatId,  InventoryCategoryMaster.InventoryCatName from InventoryCategoryMaster inner join InventorySubCategoryMaster on InventorySubCategoryMaster.InventoryCategoryMasterId=InventoryCategoryMaster.InventeroyCatId inner join InventoruSubSubCategory on InventoruSubSubCategory.InventorySubCatID=InventorySubCategoryMaster.InventorySubCatId inner join InventoryMaster on InventoryMaster.InventorySubSubId=InventoruSubSubCategory.InventorySubSubId inner join InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.InventoryMasterId=InventoryMaster.InventoryMasterId inner join WareHouseMaster on WareHouseMaster.WareHouseId=InventoryWarehouseMasterTbl.WareHouseId  WHERE InventoryWarehouseMasterTbl.WareHouseId ='" + ddlwarehouse.SelectedValue + "' order by InventoryCatName";
            //cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adp = new SqlDataAdapter(str, con);
            DataTable ds = new DataTable();
            adp.Fill(ds);

            return ds;
        }
    }
    protected void ddlcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcategory.SelectedIndex > 0)
        {
            ddlSubCategory.Items.Clear();
            SqlCommand Mycommand = new SqlCommand();


            SqlDataAdapter MyDataAdapter = new SqlDataAdapter();
            Mycommand = new SqlCommand(" SELECT     InventorySubCategoryMaster.InventorySubCatId, " +
                //" (InventoryCategoryMaster.InventoryCatName + " +
                // " ':'+  "+
              " InventorySubCategoryMaster.InventorySubCatName  as category FROM      " +
              "     InventorySubCategoryMaster INNER JOIN  InventoryCategoryMaster ON InventorySubCategoryMaster. " +
              "         InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId  " +
             " Where InventoryCategoryMaster.InventeroyCatId = '" + ddlcategory.SelectedValue + "' " +
           " ORDER BY InventoryCategoryMaster.InventoryCatName, InventorySubCategoryMaster.InventorySubCatName  ", con);

            MyDataAdapter = new SqlDataAdapter(Mycommand);
            DataSet ds1 = new DataSet();
            MyDataAdapter.Fill(ds1);
            con.Close();

            ddlSubCategory.DataSource = ds1;
            ddlSubCategory.DataValueField = "InventorySubCatId";
            ddlSubCategory.DataTextField = "category";
            ddlSubCategory.DataBind();
            ddlSubCategory.Items.Insert(0, "All");
            ddlSubCategory.SelectedItem.Value = "0";

        }
        else
        {

            ddlSubCategory.Items.Clear();
            ddlSubCategory.Items.Insert(0, "All");
            ddlSubCategory.SelectedItem.Value = "0";

            ddlSubSubCategory.Items.Clear();
            ddlSubSubCategory.Items.Insert(0, "All");
            ddlSubSubCategory.SelectedItem.Value = "0";
            //  fillgrid();
        }
    }

    public DataTable fillddl3()
    {

        SqlCommand cmd = new SqlCommand("Sp_Select_SubCategory", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@InventoryCategoryMasterId", ddlcategory.SelectedValue);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        return ds;
    }
    protected void ddlSubCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSubSubCategory.Items.Clear();
        if (ddlSubCategory.SelectedIndex == 0)
        {

        }

        else
        {
            ddlSubSubCategory.DataSource = (DataTable)fillddl4();
            ddlSubSubCategory.DataTextField = "InventorySubSubName";
            ddlSubSubCategory.DataValueField = "InventorySubSubId";
            ddlSubSubCategory.DataBind();

        }
        ddlSubSubCategory.Items.Insert(0, "All");
        ddlSubSubCategory.Items[0].Value = "0";
    }

    public DataTable fillddl4()
    {


        string str1 = "SELECT InventorySubSubId,InventorySubSubName,InventorySubCatID " +
            "  FROM InventoruSubSubCategory where InventorySubCatID=" + ddlSubCategory.SelectedValue + " order by InventorySubSubName";
        SqlCommand cmd = new SqlCommand(str1, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds1 = new DataTable();
        adp.Fill(ds1);
        return ds1;
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {

        string str = "  SELECT distinct    InventoryMaster.InventoryMasterId, InventoryMaster.ProductNo, InventoryMaster.Name, InventoryImgMaster.Thumbnail, InventoryImgMaster.InventoryImgMasterID, " +
            "   InventoryImgMaster.LargeImg,  " +
            "  InventoryCategoryMaster.InventoryCatName + ':' + InventorySubCategoryMaster.InventorySubCatName + ':' + InventoruSubSubCategory.InventorySubSubName AS cat, " +
            "   InventoruSubSubCategory.InventorySubSubId, InventoruSubSubCategory.InventorySubSubName, InventorySubCategoryMaster.InventorySubCatId, " +
            "  InventorySubCategoryMaster.InventorySubCatName, InventoryCategoryMaster.InventeroyCatId, InventoryCategoryMaster.InventoryCatName " +
            " FROM         InventoryCategoryMaster inner JOIN " +
            "  InventorySubCategoryMaster ON InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId inner JOIN " +
            "  InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID inner JOIN " +
            "  InventoryMaster ON InventoruSubSubCategory.InventorySubSubId = InventoryMaster.InventorySubSubId left outer JOIN " +
            "  InventoryImgMaster ON InventoryMaster.InventoryMasterId = InventoryImgMaster.InventoryMasterId inner join InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.InventoryMasterId=InventoryMaster.InventoryMasterId where InventoryWarehouseMasterTbl.WareHouseId='" + ddlwarehouse.SelectedValue + "' " +
            "   and     InventoryMaster.MasterActiveStatus=1 ";

        string str1 = "";
        string str2 = "";
        string str3 = "";
        //string strInvId = "";
        string strInvsubsubCatId = "";
        string strInvsubcatid = "";
        string strInvCatid = "";
        //string strInvBySerchId = "";
        if (ddlcategory.SelectedIndex > 0)
        {
            if (ddlSubCategory.SelectedIndex > 0)
            {
                if (ddlSubSubCategory.SelectedIndex > 0)
                {
                    strInvsubsubCatId = " and  InventoruSubSubCategory.InventorySubSubId=" + Convert.ToInt32(ddlSubSubCategory.SelectedValue) + "";
                    str = str + strInvsubsubCatId;
                }
                else
                {
                    strInvsubcatid = " and  InventorySubCategoryMaster.InventorySubCatId = " + Convert.ToInt32(ddlSubCategory.SelectedValue) + " ";
                    str = str + strInvsubcatid;
                }
            }
            else
            {
                strInvCatid = " and  InventoryCategoryMaster.InventeroyCatId =" + Convert.ToInt32(ddlcategory.SelectedValue) + " ";
                str = str + strInvCatid;
            }
        }

        if (ddlImgAvai.SelectedIndex == 0)
        {
            str1 = " and (InventoryImgMaster.Thumbnail IS NULL) AND (InventoryImgMaster.LargeImg IS NULL)";

        }
        else if (ddlImgAvai.SelectedIndex == 1)
        {
            str1 = "and (InventoryImgMaster.Thumbnail IS not NULL) AND (InventoryImgMaster.LargeImg IS not NULL)";
        }
        else if (ddlImgAvai.SelectedIndex == 2)
        {
            str1 = " and (InventoryImgMaster.Thumbnail IS not NULL) AND (InventoryImgMaster.LargeImg IS  NULL)";
        }
        else
        {
            str1 = " and (InventoryImgMaster.Thumbnail IS  NULL) AND (InventoryImgMaster.LargeImg IS not NULL)";
        }

        if (ddlSort.SelectedIndex == 0)
        {
            str2 = "ORDER BY InventoryMaster.Name ";
        }
        else
        {
            str2 = "ORDER BY InventoryMaster.ProductNo ";
        }

        if (ddlSortType.SelectedIndex == 0)
        {
            str3 = " asc";
        }
        else
        {
            str3 = " desc";
        }

        string strmain = str + str1 + str2 + str3;
        SqlCommand cmd1 = new SqlCommand(strmain, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable ds5 = new DataTable();
        adp1.Fill(ds5);
        if (ds5.Rows.Count > 0)
        {
            DataView myDataView = new DataView();
            myDataView = ds5.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }
            GridView1.DataSource = myDataView;
            GridView1.DataBind();
           
            Session["data"] = ds5;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                Image smImg = (Image)GridView1.Rows[i].FindControl("imgsmall");
                Image lrgImg = (Image)GridView1.Rows[i].FindControl("imglarge");
                if (ds5.Rows[i]["Thumbnail"].ToString() != "")
                {
                    smImg.ImageUrl = "~/Account/" + ds5.Rows[i]["Thumbnail"].ToString();
                    lrgImg.ImageUrl = "~/Account/" + ds5.Rows[i]["LargeImg"].ToString();
                }
            }
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }

    }

    


    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "addsmall")
        {
            GridViewRow gdr = (GridViewRow)((Button)e.CommandSource).NamingContainer;

            FileUpload fpdsm = (FileUpload)gdr.FindControl("FileUploadThumbNail");

            Image imgsmalled = (Image)gdr.FindControl("imgsmall");

            String filename = "";

            if (fpdsm.HasFile)
            {
                bool valid = ext(fpdsm.FileName);
                if (valid == true)
                {
                    filename = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "_" + fpdsm.FileName;
                    fpdsm.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\ViewSmallImg\\") + filename);
                    string fileSmallName = Session["comid"] + "/ViewSmallImg/" + filename;
                    ViewState["filename"] = fileSmallName;
                    imgsmalled.Visible = true;
                    imgsmalled.ImageUrl = "~/Account/" + fileSmallName;
                }
                else
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "This is not Valid File";
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

            String filename1 = "";
            if (fpdlrg.HasFile)
            {
                bool valid = ext(fpdlrg.FileName);
                if (valid == true)
                {
                    filename1 = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "_" + fpdlrg.FileName;
                    fpdlrg.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\ViewLargeImg\\") + filename1);
                    string file1LargeName = Session["comid"] + "/ViewLargeImg/" + filename1;
                    ViewState["filename1"] = file1LargeName;
                    imglargeed.Visible = true;
                    imglargeed.ImageUrl = "~/Account/" + file1LargeName;
                }
                else
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "This is not Valid File";
                }
            }
            else
            {
                ViewState["filename1"] = "";
            }
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
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {

        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        ImageButton1_Click(sender, e);
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        lblmsg.Text = "";
        GridView1.EditIndex = e.NewEditIndex;
        ImageClickEventArgs eh = new ImageClickEventArgs(0, 0);
        ImageButton1_Click(sender, eh);
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        ImageClickEventArgs eh = new ImageClickEventArgs(0, 0);
        ImageButton1_Click(sender, eh);
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        FileUpload ImgThumbnail = (FileUpload)GridView1.Rows[GridView1.EditIndex].FindControl("FileUploadThumbNail");
        FileUpload ImgLarge = (FileUpload)GridView1.Rows[GridView1.EditIndex].FindControl("FileUploadLargeImage");
        string ImgMasterId_li = ((Label)GridView1.Rows[GridView1.EditIndex].FindControl("lblImgMasterIdLI")).Text;
        string ImgMasterId_tn = ((Label)GridView1.Rows[GridView1.EditIndex].FindControl("lblImgMasterIdTN")).Text;
        Image imgsmalled = (Image)GridView1.Rows[GridView1.EditIndex].FindControl("imgsmall");
        Image imglargeed = (Image)GridView1.Rows[GridView1.EditIndex].FindControl("imglarge");

        String filename = "";
        String filename1 = "";
        bool valid = ext(ImgThumbnail.FileName);
        if (valid == true)
        {
            if (ImgThumbnail.HasFile != false)
            {
                filename = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "_" + ImgThumbnail.FileName;
                string fileThumbnail = Session["Comid"] + "/Thumbnail/" + filename;
                SqlCommand cmdup = new SqlCommand("Update InventoryImgMaster set Thumbnail='" + fileThumbnail + "' where InventoryImgMasterID='" + ImgMasterId_tn + "'", con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdup.ExecuteNonQuery();
                con.Close();
                ImgThumbnail.PostedFile.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\Thumbnail\\") + filename);

            }
        }
        bool valid1 = ext(ImgLarge.FileName);
        if (valid1 == true)
        {
            if (ImgLarge.HasFile != false)
            {
                filename1 = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "_" + ImgLarge.FileName;
                string fileLarge = Session["Comid"] + "/LargeImg/" + filename1;
                SqlCommand cmdup = new SqlCommand("Update InventoryImgMaster set LargeImg='" + fileLarge + "' where InventoryImgMasterID='" + ImgMasterId_li + "'", con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdup.ExecuteNonQuery();
                con.Close();
                ImgLarge.PostedFile.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\LargeImg\\") + filename1);

            }
        }
        if (imgsmalled.ImageUrl != "")
        {
            if (ViewState["filename"] != null && ViewState["filename"] != "")
            {
                SqlCommand cmdup = new SqlCommand("Update InventoryImgMaster set Thumbnail='" + ViewState["filename"] + "' where InventoryImgMasterID='" + ImgMasterId_tn + "'", con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdup.ExecuteNonQuery();
                con.Close();
            }
        }
        if (imglargeed.ImageUrl != "")
        {
            if (ViewState["filename1"] != "" && ViewState["filename1"] != null)
            {
                SqlCommand cmdup = new SqlCommand("Update InventoryImgMaster set LargeImg='" + ViewState["filename1"] + "' where InventoryImgMasterID='" + ImgMasterId_li + "'", con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdup.ExecuteNonQuery();
                con.Close();
            }
        }
        GridView1.EditIndex = -1;
        ImageClickEventArgs eh = new ImageClickEventArgs(0, 0);
        lblmsg.Visible = true;
        lblmsg.Text = "Record updated successfully";
        ImageButton1_Click(sender, eh);
        ViewState["filename"] = "";
        ViewState["filename1"] = "";
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
    protected void ddlImgAvai_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlwarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlcategory.Items.Clear();


        ddlcategory.DataSource = (DataTable)fillddl2();
        ddlcategory.DataTextField = "InventoryCatName";
        ddlcategory.DataValueField = "InventeroyCatId";
        ddlcategory.DataBind();
        ddlcategory.Items.Insert(0, "All");
        ddlcategory.Items[0].Value = "0";

        ddlcategory_SelectedIndexChanged(sender, e);
    }


    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        ImageButton1_Click(sender, e);
    }
    protected void fillstore()
    {
        ddlwarehouse.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlwarehouse.DataSource = ds;
        ddlwarehouse.DataTextField = "Name";
        ddlwarehouse.DataValueField = "WareHouseId";
        ddlwarehouse.DataBind();

        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlwarehouse.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }


    }
}
