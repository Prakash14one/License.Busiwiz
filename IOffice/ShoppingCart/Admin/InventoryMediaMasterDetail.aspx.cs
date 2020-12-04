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

public partial class ShoppingCart_Admin_InventoryMediaMasterDetail : System.Web.UI.Page
{
    SqlConnection con;
    int i;

    int k;
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
        if (!IsPostBack)
        {
            Label1.Text = "";
            lblfilen.Text = "";
            ViewState["sortOrder"] = "";
            Pagecontrol.dypcontrol(Page, page);

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
            //string strwh = "SELECT WareHouseId,Name,Address,CurrencyId FROM WareHouseMaster where comid='" + Session["comid"] + "'and Status='1' order by Name ";
            //SqlCommand cmdwh = new SqlCommand(strwh, con);
            //SqlDataAdapter adpwh = new SqlDataAdapter(cmdwh);
            //DataTable dtwh = new DataTable();
            //adpwh.Fill(dtwh);

            //ddlWarehouse.DataSource = dtwh;
            //ddlWarehouse.DataTextField = "Name";
            //ddlWarehouse.DataValueField = "WareHouseId";
            //ddlWarehouse.DataBind();
            if (ddlWarehouse.SelectedIndex > -1)
            {

                FillddlInvCat();
                ddlInvCat_SelectedIndexChanged(sender, e);
            }
            DropDownList2.DataSource = (DataSet)fillMediaType();
            DropDownList2.DataTextField = "MediaFileType";
            DropDownList2.DataValueField = "MediaFileTypeID";
            DropDownList2.DataBind();
            DropDownList2.Items.Insert(0, "-Select-");
            DropDownList2.Items[0].Value = "0";
        }
    }
    public DataSet fillMediaType()
    {
        SqlCommand cmd = new SqlCommand("SELECT     MediaFileTypeID, MediaFileType FROM   MediaFileType", con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;
    }
    protected void FillddlInvCat()
    {
        ddlInvCat.Items.Clear();

        string str = "SELECT Distinct  InventoryCategoryMaster.InventeroyCatId,InventoryCategoryMaster.InventoryCatName FROM InventoryCategoryMaster inner join InventorySubCategoryMaster on InventorySubCategoryMaster.InventoryCategoryMasterId=InventoryCategoryMaster.InventeroyCatId inner join InventoruSubSubCategory on InventoruSubSubCategory.InventorySubCatID=InventorySubCategoryMaster.InventorySubCatId inner join InventoryMaster on InventoryMaster.InventorySubSubId=InventoruSubSubCategory.InventorySubSubId inner join InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.InventoryMasterId=InventoryMaster.InventoryMasterId inner join WareHouseMaster on WareHouseMaster.WareHouseId=InventoryWarehouseMasterTbl.WareHouseId  WHERE InventoryWarehouseMasterTbl.WareHouseId ='" + ddlWarehouse.SelectedValue + "'  and (InventoryCategoryMaster.CatType IS NULL) order by InventoryCategoryMaster.InventoryCatName"; ;
        SqlDataAdapter adpt = new SqlDataAdapter(str, con);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ddlInvCat.DataSource = dt;
            ddlInvCat.DataTextField = "InventoryCatName";
            ddlInvCat.DataValueField = "InventeroyCatId";
            ddlInvCat.DataBind();
            ddlInvCat.Items.Insert(0, "All");
            ddlInvCat.Items[0].Value = "0";
        }
        else
        {
            ddlInvCat.Items.Insert(0, "All");
            ddlInvCat.Items[0].Value = "0";
        }

    }

    protected void ddlInvCat_SelectedIndexChanged(object sender, EventArgs e)
    {

        ddlInvSubCat.Items.Clear();
        if (ddlInvCat.SelectedIndex > 0)
        {
            SqlCommand Mycommand = new SqlCommand();


            SqlDataAdapter MyDataAdapter = new SqlDataAdapter();
            Mycommand = new SqlCommand(" SELECT     InventorySubCategoryMaster.InventorySubCatId, " +
                //" (InventoryCategoryMaster.InventoryCatName + " +
                // " ':'+  "+
                " InventorySubCategoryMaster.InventorySubCatName  as category FROM      " +
                " InventorySubCategoryMaster INNER JOIN  InventoryCategoryMaster ON InventorySubCategoryMaster. " +
                " InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId  " +
                " Where InventoryCategoryMaster.InventeroyCatId = '" + ddlInvCat.SelectedValue + "' " +
                " ORDER BY InventorySubCategoryMaster.InventorySubCatName  ", con);

            MyDataAdapter = new SqlDataAdapter(Mycommand);
            DataSet ds1 = new DataSet();
            MyDataAdapter.Fill(ds1);
            if (ds1.Tables[0].Rows.Count > 0)
            {

                ddlInvSubCat.DataSource = ds1;
                ddlInvSubCat.DataValueField = "InventorySubCatId";
                ddlInvSubCat.DataTextField = "category";
                ddlInvSubCat.DataBind();
                ddlInvSubCat.Items.Insert(0, "All");
                ddlInvSubCat.SelectedItem.Value = "0";
            }
            else
            {
                ddlInvSubCat.Items.Insert(0, "All");
                ddlInvSubCat.Items[0].Value = "0";
            }
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
        ddlInvSubSubCat.Items.Clear();
        if (ddlInvSubCat.SelectedIndex > 0)
        {
            string strsubsubcat = "SELECT InventorySubSubId ,InventorySubSubName  ,InventorySubCatID  FROM  InventoruSubSubCategory " +
                            " where InventorySubCatID=" + Convert.ToInt32(ddlInvSubCat.SelectedValue) + " order by InventorySubSubName ";
            SqlCommand cmdsubsubcat = new SqlCommand(strsubsubcat, con);
            SqlDataAdapter adpsubsubcat = new SqlDataAdapter(cmdsubsubcat);
            DataTable dtsubsubcat = new DataTable();
            adpsubsubcat.Fill(dtsubsubcat);
            if (dtsubsubcat.Rows.Count > 0)
            {
                ddlInvSubSubCat.DataSource = dtsubsubcat;
                ddlInvSubSubCat.DataTextField = "InventorySubSubName";
                ddlInvSubSubCat.DataValueField = "InventorySubSubId";
                ddlInvSubSubCat.DataBind();
                ddlInvSubSubCat.Items.Insert(0, "All");
                ddlInvSubSubCat.Items[0].Value = "0";
            }
            else
            {
                ddlInvSubSubCat.Items.Insert(0, "All");
                ddlInvSubSubCat.Items[0].Value = "0";
            }
        }
        else
        {
            ddlInvSubSubCat.Items.Insert(0, "All");
            ddlInvSubSubCat.Items[0].Value = "0";
        }
        ddlInvSubSubCat_SelectedIndexChanged(sender, e);
    }
    protected void ddlInvSubSubCat_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlInvName.Items.Clear();
        if (ddlInvSubSubCat.SelectedIndex > 0)
        {
            string strinvname = "SELECT InventoryMasterId,Name,InventoryDetailsId,InventorySubSubId,ProductNo,InventoryTypeId  FROM InventoryMaster " +
                            " where InventorySubSubId= " + Convert.ToInt32(ddlInvSubSubCat.SelectedValue) + " and InventoryMaster.MasterActiveStatus=1 order by InventoryMaster.Name ";
            SqlCommand cmdinvname = new SqlCommand(strinvname, con);
            SqlDataAdapter adpinvname = new SqlDataAdapter(cmdinvname);
            DataTable dtinvname = new DataTable();
            adpinvname.Fill(dtinvname);
            if (dtinvname.Rows.Count > 0)
            {
                ddlInvName.DataSource = dtinvname;
                ddlInvName.DataTextField = "Name";
                ddlInvName.DataValueField = "InventoryMasterId";
                ddlInvName.DataBind();
                ddlInvName.Items.Insert(0, "All");
                ddlInvName.Items[0].Value = "0";
            }
            else
            {
                ddlInvName.Items.Insert(0, "All");
                ddlInvName.Items[0].Value = "0";
            }
        }
        else
        {
            ddlInvName.Items.Insert(0, "All");
            ddlInvName.Items[0].Value = "0";
        }
    }
    protected void imgBtnSearchGo_Click(object sender, EventArgs e)
    {
        Label1.Text = "";
        Panel1.Visible = true;
        DataTable dtGridFill = (DataTable)(FilterInventoryId());
        DataView myDataView = new DataView();
        myDataView = dtGridFill.DefaultView;
        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }
        if (dtGridFill.Rows.Count > 0)
        {
            grdInvMasters.DataSource = myDataView;
            grdInvMasters.DataBind();
        }
        else
        {
            grdInvMasters.DataSource = null;
            grdInvMasters.DataBind();
        }

        //lblcomname.Text = Session["Cname"].ToString();
        // lblbusiness.Text = ddlWarehouse.SelectedItem.Text;
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

                }
            }
            else
            {
                //strInvCatid = "where InventoryCategoryMaster.InventeroyCatId =" + Convert.ToInt32(ddlInvCat.SelectedValue) + " ";
                //strInvCatid = "where InventoryCategoryMaster.InventeroyCatId =" + Convert.ToInt32(ddlInvCat.SelectedValue) + "and InventoryCategoryMaster.compid=" + Session["comid"] + " ";
            }

            // string mainString = strinv + strInvId + strInvsubsubCatId + strInvsubcatid + strInvCatid + "order by SalesChallanMaster.RefSalesOrderId ";
            string mainString = strinv + strInvId + strInvsubsubCatId + strInvsubcatid + strInvCatid + "  and InventoryMaster.MasterActiveStatus=1 ORDER BY InventoryCategoryMaster.InventoryCatName, InventorySubCategoryMaster.InventorySubCatName, InventoruSubSubCategory.InventorySubSubName, InventoryMaster.Name";


            SqlCommand cmdinv = new SqlCommand(mainString, con);
            SqlDataAdapter adpinv = new SqlDataAdapter(cmdinv);
            DataTable dtinv = new DataTable();
            adpinv.Fill(dtinv);

            return dtinv;
        }
        else
        {
            string str23 = " SELECT     InventoryMaster.InventoryMasterId, InventoryMaster.Name, InventoryMaster.ProductNo, InventoruSubSubCategory.InventorySubSubId, " +
                     " InventoruSubSubCategory.InventorySubSubName, InventorySubCategoryMaster.InventorySubCatId, InventorySubCategoryMaster.InventorySubCatName,  " +
                     " InventoryCategoryMaster.InventeroyCatId, InventoryCategoryMaster.InventoryCatName, InventoryBarcodeMaster.Barcode, " +
                     " Left(InventoryCategoryMaster.InventoryCatName,15)    + ' : ' + Left(InventorySubCategoryMaster.InventorySubCatName,15) + ' : ' + Left(InventoruSubSubCategory.InventorySubSubName,15)     AS CatScSsc,InventoryMeasurementUnit.Unit, UnitTypeMaster.Name as unittype " +
                     "  FROM         InventoryBarcodeMaster RIGHT OUTER JOIN " +
                     " InventoryCategoryMaster RIGHT OUTER JOIN " +
                     " InventorySubCategoryMaster ON InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId RIGHT OUTER JOIN " +
                     " InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID RIGHT OUTER JOIN " +
                     " InventoryMaster ON InventoruSubSubCategory.InventorySubSubId = InventoryMaster.InventorySubSubId ON " +
                     " InventoryBarcodeMaster.InventoryMaster_id = InventoryMaster.InventoryMasterId inner join InventoryMeasurementUnit on InventoryMaster.InventoryMasterId = InventoryMeasurementUnit.InventoryMasterId inner join UnitTypeMaster on UnitTypeMaster.UnitTypeId = InventoryMeasurementUnit.UnitType inner join InventoryWarehouseMasterTbl  on InventoryWarehouseMasterTbl.InventoryMasterId=InventoryMaster.InventoryMasterId " +
                    " WHERE     (InventoryMaster.Name like '%" + txtSearchInvName.Text.Replace("'", "''") + "%') and InventoryMaster.MasterActiveStatus=1 ORDER BY InventoryCategoryMaster.InventoryCatName, InventorySubCategoryMaster.InventorySubCatName, InventoruSubSubCategory.InventorySubSubName, InventoryMaster.Name ";

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
            //string mainString = strinv + strInvId + strInvsubsubCatId + strInvsubcatid + strInvCatid + "  and InventoryMaster.MasterActiveStatus=1 ORDER BY InventoryCategoryMaster.InventoryCatName, InventorySubCategoryMaster.InventorySubCatName, InventoruSubSubCategory.InventorySubSubName, InventoryMaster.Name";


            SqlCommand cmdinv = new SqlCommand(str23, con);
            SqlDataAdapter adpinv = new SqlDataAdapter(cmdinv);
            DataTable dtinv = new DataTable();
            adpinv.Fill(dtinv);

            return dtinv;
        }




    }
    protected void grdInvMasters_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select1")
        {
            pnlViewFill.Visible = true;
            ViewState["InvMid"] = null;
            grdInvMasters.SelectedIndex = Convert.ToInt32(e.CommandArgument.ToString());
            int dk = Convert.ToInt32(grdInvMasters.SelectedDataKey.Value);
            //Session["view"] = dk;

            ViewState["InvMid"] = dk;
            GridViewRow gdr = grdInvMasters.SelectedRow;
            Label lblCategory = (Label)gdr.FindControl("lblCategory");
            Label lblInvName = (Label)gdr.FindControl("lblInvName");
            Label lblProductNo = (Label)gdr.FindControl("lblProductNo");
            Label lblInvweight = (Label)gdr.FindControl("lblInvweight");
            Label lblInvunit = (Label)gdr.FindControl("lblInvunit");

            lblInvCScSScName.Text = lblCategory.Text + " : " + lblInvName.Text + " : " + lblProductNo.Text + " : " + lblInvweight.Text + " " + lblInvunit.Text;

            FillGrid2DiffView();

        }
        if (e.CommandName == "name")
        {
            string dk = Convert.ToString(e.CommandArgument);
            string te = "InventoryProfile.aspx?Invmid=" + dk;
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        }
    }
    protected void FillGrid2DiffView()
    {
        //SqlCommand cmdg = new SqlCommand("select MediaFileTypeID, MediaFileType FROM   MediaFileType", con);
        //SqlDataAdapter dtpg = new SqlDataAdapter(cmdg);
        //DataTable dsg = new DataTable();
        //dtpg.Fill(dsg);
        //if (dsg.Rows.Count > 0)
        //{
        //    GridView1.DataSource = dsg;
        //    GridView1.DataBind();
        //}
        //if (GridView1.Rows.Count > 0)
        //{
        //    foreach (GridViewRow ggghhh in GridView1.Rows)
        //    {

        //    }
        //}
        SqlDataAdapter adpt = new SqlDataAdapter("select InventoryMediaMaster.*,MediaFileType.MediaFileType from InventoryMediaMaster inner join MediaFileType on InventoryMediaMaster.MediaFileTypeID =  MediaFileType.MediaFileTypeID  where InventoryMasterId='" + ViewState["InvMid"] + "'", con);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
            

            foreach (GridViewRow grd in GridView1.Rows)
            {
                Label lblfilename = (Label)grd.FindControl("lblfilename");
                Label lblfullname = (Label)grd.FindControl("lblfullname");
                string strfilename = lblfullname.Text;
                string strremove = strfilename.Substring(0, strfilename.LastIndexOf("/") + 1);
                lblfilename.Text = strfilename.Remove(0, strremove.Length);
            }
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
    }
    protected void linktest_Click(object sender, EventArgs e)
    {
        Button lk = (Button)sender;
        //foreach (GridViewRow grd in GridView1.Rows)
        //{
        //HtmlAnchor playaudio = (HtmlAnchor)grd.FindControl("playaudio");
        string lblview = lk.CommandArgument.ToString();
        //GridViewRow gdr = grdInvMasters.SelectedRow;
        //Label lblview = (Label)gdr.FindControl("lblview");
        StringBuilder sb = new StringBuilder();

        if (lblview == "Audio")
        {

            string url = "../PlayAudio.aspx?id=" + ViewState["InvMid"];
            sb.Append("<script type='text/javascript'>");
            sb.Append("window.open('");
            sb.Append(url);
            sb.Append("');");
            sb.Append("</script>");
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + url + "');", true);
        }
        else if (lblview == "Video")
        {
            string url = "../PlayVideo.aspx?id=" + ViewState["InvMid"];
            sb.Append("<script type='text/javascript'>");
            sb.Append("window.open('");
            sb.Append(url);
            sb.Append("');");
            sb.Append("</script>");
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + url + "');", true);
        }

        // ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());

        //}
    }
    public void insert(int inventoryid, string title, string filename, int typeid, string desc, DateTime date)
    {

        string str = "insert into InventoryMediaMaster(InventoryMasterId,MediaTitle,FileName,MediaFileTypeID,MediaFileDesc,EntryDate) " +
                    " values('" + inventoryid + "','" + title + "','" + filename + "','" + typeid + "','" + desc + "','" + date + "')";

        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd.ExecuteNonQuery();
        con.Close();
        Label1.Visible = true;
        Label1.Text = "Record inserted successfully";
    }
    public bool ext(string filename)
    {
        string[] validFileTypes = { "mp3", "mp4", "wmv", "vlc", "mkv", "avi", "mpg", "flv", "wma" };

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
    protected void Button1_Click(object sender, EventArgs e)
    {

        if (DropDownList2.SelectedItem.Text == "Audio")
        {

            if (FileUpload1.HasFile)
            {
                bool valid = ext(FileUpload1.FileName);
                if (valid == true)
                {



                    int l = FileUpload1.PostedFile.ContentLength;
                    k = l / 1048576;
                    if (k < 50)
                    {
                        FileUpload1.PostedFile.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\audio\\") + FileUpload1.FileName);
                        string filename = Session["comid"] + "/audio/" + FileUpload1.FileName;
                        Session["filename"] = filename.ToString();

                        SqlDataAdapter adpt = new SqlDataAdapter("Select * from InventoryMediaMaster where InventoryMasterId='" + ViewState["InvMid"] + "' and MediaFileTypeID='" + DropDownList2.SelectedValue + "'", con);
                        DataTable dt = new DataTable();
                        adpt.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            Label1.Visible = true;
                            ModalPopupExtender3.Show();
                            // Label1.Text = "Record already exists";
                        }
                        else
                        {
                            insert(Convert.ToInt32(ViewState["InvMid"].ToString()), txtfiletitle.Text, filename, Convert.ToInt32(DropDownList2.SelectedValue), txttaskinstruction.Text, System.DateTime.Now);
                            clear();
                            FillGrid2DiffView();
                            pnladdnew.Visible = false;
                            btntest.Visible = true;
                        }
                    }
                    else
                    {
                        Label1.Visible = true;
                        Label1.Text = "Please upload file Less than of size 50 MB";
                    }
                }
                else
                {
                    Label1.Visible = true;
                    Label1.Text = "Invalid File. Please upload a File with extension mp3, mp4, wmv, vlc, mkv, avi, mpg, flv, wma";
                }
            }
            else if (lblfilen.Text != "")
            {
                string filename = Session["comid"] + "/audio/" + lblfilen.Text;

                SqlDataAdapter adpt = new SqlDataAdapter("Select * from InventoryMediaMaster where InventoryMasterId='" + ViewState["InvMid"] + "' and MediaFileTypeID='" + DropDownList2.SelectedValue + "'", con);
                DataTable dt = new DataTable();
                adpt.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    Label1.Visible = true;
                    ModalPopupExtender3.Show();
                    // Label1.Text = "Record already exists";
                }
                else
                {
                    insert(Convert.ToInt32(ViewState["InvMid"].ToString()), txtfiletitle.Text, filename, Convert.ToInt32(DropDownList2.SelectedValue), txttaskinstruction.Text, System.DateTime.Now);
                    clear();
                    FillGrid2DiffView();
                    pnladdnew.Visible = false;
                    btntest.Visible = true;
                }
            }
            //else if (Session["filename"] != null)
            //{
            //    string filename = Session["comid"] + "/audio/" + Session["filename"];
            //    insert(Convert.ToInt32(ViewState["InvMid"].ToString()), txtfiletitle.Text, filename, Convert.ToInt32(DropDownList2.SelectedValue), txttaskinstruction.Text, System.DateTime.Now);
            //    clear();
            //    FillGrid2DiffView();
            //    pnladdnew.Visible = false;
            //    btntest.Visible = true;
            //    Session["filename"] = null;
            //}
            else
            {
                Label1.Visible = true;
                Label1.Text = "File not found.";
            }
            // }

        }
        else if (DropDownList2.SelectedItem.Text == "Video")
        {

            if (FileUpload1.HasFile)
            {
                bool valid = ext(FileUpload1.FileName);
                if (valid == true)
                {

                    int l = FileUpload1.PostedFile.ContentLength;
                    k = l / 1048576;
                    if (k < 50)
                    {
                        FileUpload1.PostedFile.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\vidio\\") + FileUpload1.FileName);
                        string filename = Session["comid"] + "/vidio/" + FileUpload1.FileName;
                        Session["filename1"] = filename.ToString();
                        SqlDataAdapter adpt1 = new SqlDataAdapter("Select * from InventoryMediaMaster where InventoryMasterId='" + ViewState["InvMid"] + "' and MediaFileTypeID='" + DropDownList2.SelectedValue + "'", con);
                        DataTable dt1 = new DataTable();
                        adpt1.Fill(dt1);
                        if (dt1.Rows.Count > 0)
                        {
                            Label1.Visible = true;
                            ModalPopupExtender3.Show();
                            //Label1.Text = "Record already exists";
                        }
                        else
                        {
                            insert(Convert.ToInt32(ViewState["InvMid"].ToString()), txtfiletitle.Text, filename, Convert.ToInt32(DropDownList2.SelectedValue), txttaskinstruction.Text, System.DateTime.Now);
                            clear();
                            FillGrid2DiffView();
                            pnladdnew.Visible = false;
                            btntest.Visible = true;
                        }
                    }
                    else
                    {
                        Label1.Visible = true;
                        Label1.Text = "Please upload file Less than of size 50 MB";
                    }
                }
                else
                {
                    Label1.Visible = true;
                    Label1.Text = "Invalid File. Please upload a File with extension mp3, mp4, wmv, vlc, mkv, avi, mpg, flv, wma";
                }
            }
            else if (lblfilen.Text != "")
            {
                string filename = Session["comid"] + "/vidio/" + lblfilen.Text;
                SqlDataAdapter adpt1 = new SqlDataAdapter("Select * from InventoryMediaMaster where InventoryMasterId='" + ViewState["InvMid"] + "' and MediaFileTypeID='" + DropDownList2.SelectedValue + "'", con);
                DataTable dt1 = new DataTable();
                adpt1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    Label1.Visible = true;
                    ModalPopupExtender3.Show();
                    //Label1.Text = "Record already exists";
                }
                else
                {
                    insert(Convert.ToInt32(ViewState["InvMid"].ToString()), txtfiletitle.Text, filename, Convert.ToInt32(DropDownList2.SelectedValue), txttaskinstruction.Text, System.DateTime.Now);
                    clear();
                    FillGrid2DiffView();
                    pnladdnew.Visible = false;
                    btntest.Visible = true;
                }
            }
            //else if (Session["filename"] != null)
            //{
            //    string filename = Session["comid"] + "/vidio/" + Session["filename"];
            //    insert(Convert.ToInt32(ViewState["InvMid"].ToString()), txtfiletitle.Text, filename, Convert.ToInt32(DropDownList2.SelectedValue), txttaskinstruction.Text, System.DateTime.Now);
            //    clear();
            //    Session["filename"] = null;
            //    FillGrid2DiffView();
            //    pnladdnew.Visible = false;
            //    btntest.Visible = true;
            //}
            else
            {
                Label1.Visible = true;
                Label1.Text = "File not found.";
            }
            // }

        }
        else
        {

            Label1.Visible = true;
            Label1.Text = "Please select Media Type";
        }
    }
    protected void clear()
    {
        Session["filename"] = null;
        lblfilen.Text = "";
        DropDownList2.SelectedIndex = 0;
        txttaskinstruction.Text = "";
        txtfiletitle.Text = "";

    }
    protected void ddlWarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlInvCat.Items.Clear();
        FillddlInvCat();
        ddlInvCat_SelectedIndexChanged(sender, e);
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        Label1.Text = "";
        clear();
        pnladdnew.Visible = false;
        btntest.Visible = true;
    }
    protected void btntest_Click(object sender, EventArgs e)
    {
        Label1.Text = "";
        if (pnladdnew.Visible == false)
        {
            pnladdnew.Visible = true;
        }
        if (btntest.Visible == true)
        {
            btntest.Visible = false;
        }
    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (DropDownList2.SelectedItem.Text == "Audio")
        {
            Label1.Text = "";
            //DateTime dt = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());

            // Label1.Visible = true;
            // Label1.Text = "Record already exist";


            if (FileUpload1.HasFile)
            {
                bool valid = ext(FileUpload1.FileName);
                if (valid == true)
                {
                    int l = FileUpload1.PostedFile.ContentLength;
                    k = l / 1048576;
                    if (k < 50)
                    {
                        FileUpload1.PostedFile.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\audio\\") + FileUpload1.FileName);
                        string filename = Session["comid"] + "/audio/" + FileUpload1.FileName;
                        lblfilen.Text = FileUpload1.FileName;

                        //SqlDataAdapter adpt = new SqlDataAdapter("Select * from InventoryMediaMaster where InventoryMasterId='" + ViewState["InvMid"] + "' and MediaFileTypeID='" + DropDownList2.SelectedValue + "'", con);
                        //DataTable dt = new DataTable();
                        //adpt.Fill(dt);
                        //if (dt.Rows.Count > 0)
                        //{


                        //    Session["filename"] = FileUpload1.FileName;
                        //    ModalPopupExtender3.Show();
                        //}
                    }
                    else
                    {
                        Label1.Visible = true;
                        Label1.Text = "Please upload file Less than of size 50 MB";
                    }


                }
                else
                {
                    Label1.Visible = true;
                    Label1.Text = "Invalid File Type. Please upload a sound file in one of the following formats: mp3, mp4, wmv, vlc, mkv, avi, mpg, flv, wma";
                }
            }
            else
            {
                Label1.Visible = true;
                Label1.Text = "File not found.";
            }

        }
        else if (DropDownList2.SelectedItem.Text == "Video")
        {
            //DateTime dt = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
            if (FileUpload1.HasFile)
            {
                bool valid = ext(FileUpload1.FileName);
                if (valid == true)
                {


                    int l = FileUpload1.PostedFile.ContentLength;
                    k = l / 1048576;
                    if (k < 50)
                    {
                        FileUpload1.PostedFile.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\vidio\\") + FileUpload1.FileName);
                        string filename = Session["comid"] + "/vidio/" + FileUpload1.FileName;
                        lblfilen.Text = FileUpload1.FileName;

                        //SqlDataAdapter adpt1 = new SqlDataAdapter("Select * from InventoryMediaMaster where InventoryMasterId='" + ViewState["InvMid"] + "' and MediaFileTypeID='" + DropDownList2.SelectedValue + "'", con);
                        //DataTable dt1 = new DataTable();
                        //adpt1.Fill(dt1);
                        //if (dt1.Rows.Count > 0)
                        //{
                        //    // Label1.Visible = true;
                        //    // Label1.Text = "Record already exist";


                        //    Session["filename"] = FileUpload1.FileName;
                        //    ModalPopupExtender3.Show();
                        //}
                    }
                    else
                    {
                        Label1.Visible = true;
                        Label1.Text = "Please upload file Less than of size 50 MB";
                    }
                }
                else
                {
                    Label1.Visible = true;
                    Label1.Text = "Invalid File Type. Please upload a video file in one of the following formats: mp3, mp4, wmv, vlc, mkv, avi, mpg, flv, wma";
                }
            }
            else
            {
                Label1.Visible = true;
                Label1.Text = "File not found.";
            }
        }
        else
        {
            Label1.Visible = true;
            Label1.Text = "Please select Media Type";
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ViewState["id"] = GridView1.DataKeys[e.RowIndex].Value.ToString();
        string str = "Delete from InventoryMediaMaster where InventoryMediaMasterID='" + ViewState["id"] + "'";
        SqlCommand cmd = new SqlCommand(str, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd.ExecuteNonQuery();
        con.Close();
        Label1.Visible = true;
        Label1.Text = "Record deleted successfully";
        FillGrid2DiffView();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            object[] dataitems = ((DataRowView)e.Row.DataItem).Row.ItemArray;
            HtmlAnchor playaudio = (HtmlAnchor)e.Row.FindControl("playaudio");
            HtmlAnchor playvideo = (HtmlAnchor)e.Row.FindControl("playvideo");
            Label lblview = (Label)e.Row.FindControl("lblview");
            if (lblview.Text == "Audio")
            {
                if (playaudio != null)
                {
                    if (playaudio.Visible == false)
                    {
                        playaudio.Visible = true;
                    }
                    else
                    {
                        playaudio.Visible = false;
                    }
                }
            }
            if (lblview.Text == "Video")
            {
                if (playvideo != null)
                {
                    if (playvideo.Visible == false)
                    {
                        playvideo.Visible = true;
                    }
                    else
                    {
                        playvideo.Visible = false;
                    }
                }
            }
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        if (DropDownList2.SelectedItem.Text == "Audio")
        {
            Label1.Text = "";
            //DateTime dt = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
            SqlCommand cmd = new SqlCommand("Delete from InventoryMediaMaster where InventoryMasterId='" + ViewState["InvMid"] + "' and MediaFileTypeID='" + DropDownList2.SelectedValue + "'", con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();

            
           
            if (Session["filename"] != null)
            {
                //string filename = Session["comid"] + "/audio/" + Session["filename"];
                insert(Convert.ToInt32(ViewState["InvMid"].ToString()), txtfiletitle.Text, Session["filename"].ToString(), Convert.ToInt32(DropDownList2.SelectedValue), txttaskinstruction.Text, System.DateTime.Now);
                clear();
                FillGrid2DiffView();
                pnladdnew.Visible = false;
                btntest.Visible = true;
                Session["filename"] = null;
            }
            else if (lblfilen.Text != "")
            {
                string filename = Session["comid"] + "/audio/" + lblfilen.Text;
                insert(Convert.ToInt32(ViewState["InvMid"].ToString()), txtfiletitle.Text, filename, Convert.ToInt32(DropDownList2.SelectedValue), txttaskinstruction.Text, System.DateTime.Now);
                clear();
                FillGrid2DiffView();
                pnladdnew.Visible = false;
                btntest.Visible = true;
                Session["filename"] = null;
            }
        }
        else if (DropDownList2.SelectedItem.Text == "Video")
        {
            //DateTime dt = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
            SqlCommand cmd = new SqlCommand("Delete from InventoryMediaMaster where InventoryMasterId='" + ViewState["InvMid"] + "' and MediaFileTypeID='" + DropDownList2.SelectedValue + "'", con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();
            
            
            if (Session["filename1"] != null)
            {
               // string filename = Session["comid"] + "/vidio/" + Session["filename"];
                insert(Convert.ToInt32(ViewState["InvMid"].ToString()), txtfiletitle.Text, Session["filename1"].ToString(), Convert.ToInt32(DropDownList2.SelectedValue), txttaskinstruction.Text, System.DateTime.Now);
                clear();
                Session["filename"] = null;
                FillGrid2DiffView();
                pnladdnew.Visible = false;
                btntest.Visible = true;
            }
            else if (lblfilen.Text != "")
            {
                string filename = Session["comid"] + "/vidio/" + lblfilen.Text;
                insert(Convert.ToInt32(ViewState["InvMid"].ToString()), txtfiletitle.Text, filename, Convert.ToInt32(DropDownList2.SelectedValue), txttaskinstruction.Text, System.DateTime.Now);
                clear();
                FillGrid2DiffView();
                pnladdnew.Visible = false;
                btntest.Visible = true;
                
            }
            
        }
        ModalPopupExtender3.Hide();
    }

    protected void grdInvMasters_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        imgBtnSearchGo_Click(sender, e);
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
    protected void linkinvetory_click(object sender, EventArgs e)
    {

    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        Label1.Text = "";
        clear();
        pnladdnew.Visible = false;
        btntest.Visible = true;
        ModalPopupExtender3.Hide();
    }
}