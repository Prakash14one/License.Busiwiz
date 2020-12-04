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


public partial class ShoppingCart_Admin_InventoryProfile : System.Web.UI.Page
{
    SqlConnection con;
    int k;
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
            if (Request.QueryString["Invmid"] != null)
            {
                Label1.Text = "";
                int id = Convert.ToInt32(Request.QueryString["Invmid"]);

                SqlCommand cmd = new SqlCommand("SELECT InventoryDetails.Description, InventoryBarcodeMaster.Barcode, InventoryMaster.Name,InventoryMaster.ProductNo, InventoryCategoryMaster.InventoryCatName, InventorySubCategoryMaster.InventorySubCatName, InventoruSubSubCategory.InventorySubSubName ," +
                                   " InventoruSubSubCategory.InventorySubSubId " +
                                   " FROM InventoryCategoryMaster INNER JOIN InventorySubCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId inner join  InventoruSubSubCategory ON InventoruSubSubCategory.InventorySubCatID = InventorySubCategoryMaster.InventorySubCatId  inner join InventoryMaster on InventoryMaster.InventorySubSubId= InventoruSubSubCategory.InventorySubSubId inner join InventoryBarcodeMaster on InventoryBarcodeMaster.InventoryMaster_id=InventoryMaster.InventoryMasterId inner join InventoryDetails on  InventoryDetails.Inventory_Details_Id=InventoryMaster.InventoryDetailsId Where InventoryMasterId='" + id + "' and  InventoryCategoryMaster.compid='" + Session["comid"] + "' and InventoryCategoryMaster.CatType IS NULL", con);





                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                adp.Fill(ds);
                if (ds.Rows.Count > 0)
                {
                    lblinvid.Text = Convert.ToString(ds.Rows[0]["Name"]);
                    lblpbarcode.Text = Convert.ToString(ds.Rows[0]["Barcode"]);
                    lblpnumber.Text = Convert.ToString(ds.Rows[0]["ProductNo"]);
                    lblcategory.Text = Convert.ToString(ds.Rows[0]["InventoryCatName"]);
                    lblsubcategory.Text = Convert.ToString(ds.Rows[0]["InventorySubCatName"]);
                    lblsubsubcat.Text = Convert.ToString(ds.Rows[0]["InventorySubSubName"]);
                  //ddlproduct.SelectedValue=(lblinvid.Text+':'+lblpnumber.Text);
                     
                    lbldis.Text = Convert.ToString(ds.Rows[0]["Description"]);
                }

                string strwh = "SELECT InventoryWarehouseMasterTbl.InventoryWarehouseMasterId,Party_Master.Compname, UnitTypeMaster.Name as utype,WarehouseMaster.Name as Wname, InventoryWarehouseMasterTbl.Rate,InventoryWarehouseMasterTbl.OpeningQty as Qty,InventoryWarehouseMasterTbl.Weight FROM WarehouseMaster inner join InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.WareHouseId=WarehouseMaster.WareHouseId inner join Party_Master on Party_Master.PartyId=InventoryWarehouseMasterTbl.PreferredVendorId inner join UnitTypeMaster on UnitTypeMaster.UnitTypeId=InventoryWarehouseMasterTbl.UnitTypeId where  InventoryMasterId='" + id + "'";
                SqlCommand cmdwh = new SqlCommand(strwh, con);
                SqlDataAdapter adpwh = new SqlDataAdapter(cmdwh);
                DataTable dtwh = new DataTable();
                adpwh.Fill(dtwh);
                if (dtwh.Rows.Count > 0)
                {
                    grdd.DataSource = dtwh;
                    grdd.DataBind();

                }
                ddlinventoryitems.DataSource = getall();
                ddlinventoryitems.DataTextField = "Category";
                ddlinventoryitems.DataValueField = "InventorySubSubId";
                ddlinventoryitems.DataBind();
                ddlinventoryitems.SelectedIndex = ddlinventoryitems.Items.IndexOf(ddlinventoryitems.Items.FindByValue(Convert.ToInt32(ds.Rows[0]["InventorySubSubId"]).ToString()));
                ddlinventoryitems_SelectedIndexChanged(sender, e);
               ddlproduct.SelectedValue = id.ToString();
                ddlproduct.SelectedIndex =ddlproduct.Items.IndexOf(ddlproduct.Items.FindByValue(id.ToString()));
                FillGridView1();
                FillGrid2DiffView();
                fillaudio();
                fillvidio();
               
                    



            }
            else
            {

                //fillstore();
                //ddlWarehouse_SelectedIndexChanged(sender, e);
                ddlinventoryitems.DataSource = getall();
                ddlinventoryitems.DataTextField = "Category";
                ddlinventoryitems.DataValueField = "InventorySubSubId";
                ddlinventoryitems.DataBind();
                ddlinventoryitems_SelectedIndexChanged(sender, e);
                imgBtnSearchGo_Click(sender, e);
                Label1.Text = "";
            }
        }
      
    }

    protected void fillstore()
    {
        //ddlWarehouse.Items.Clear();
        //DataTable ds = ClsStore.SelectStorename();
        //ddlWarehouse.DataSource = ds;
        //ddlWarehouse.DataTextField = "Name";
        //ddlWarehouse.DataValueField = "WareHouseId";
        //ddlWarehouse.DataBind();


        //DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        //if (dteeed.Rows.Count > 0)
        //{
        //    ddlWarehouse.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        //}


    }
    public DataSet getall()
    {
        SqlCommand cmd = new SqlCommand("SELECT InventoryCategoryMaster.InventoryCatName + ':' + " +
                     " InventorySubCategoryMaster.InventorySubCatName + ':' + InventoruSubSubCategory.InventorySubSubName   AS Category, " +
                     " InventoruSubSubCategory.InventorySubSubId " +
                     " FROM InventoruSubSubCategory INNER JOIN InventorySubCategoryMaster ON InventoruSubSubCategory.InventorySubCatID = InventorySubCategoryMaster.InventorySubCatId INNER JOIN   InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId where InventoryCategoryMaster.compid='" + Session["comid"] + "' and InventoryCategoryMaster.CatType IS NULL Order by Category", con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;



    }
    protected void ddlinventoryitems_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlproduct.Items.Clear();
        SqlCommand cmdP = new SqlCommand("Select InventoryMaster.Name+':'+ InventoryMaster.ProductNo as Items,InventoryMasterId from InventoryMaster where InventorySubSubId='" + ddlinventoryitems.SelectedValue + "'", con);
        SqlDataAdapter adpP = new SqlDataAdapter(cmdP);
        DataTable dsP = new DataTable();
        adpP.Fill(dsP);
        if (dsP.Rows.Count > 0)
        {
            ddlproduct.DataSource = dsP;
            ddlproduct.DataTextField = "Items";
            ddlproduct.DataValueField = "InventoryMasterId";
            ddlproduct.DataBind();
        }
    }
    protected void imgBtnSearchGo_Click(object sender, EventArgs e)
    {
        Label1.Text = "";
      
       
    
           SqlCommand cmd  = new SqlCommand("SELECT InventoryDetails.Description, InventoryBarcodeMaster.Barcode, InventoryMaster.Name,InventoryMaster.ProductNo, InventoryCategoryMaster.InventoryCatName, InventorySubCategoryMaster.InventorySubCatName, InventoruSubSubCategory.InventorySubSubName ," +
                        " InventoruSubSubCategory.InventorySubSubId " +
                        " FROM InventoryCategoryMaster INNER JOIN InventorySubCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId inner join  InventoruSubSubCategory ON InventoruSubSubCategory.InventorySubCatID = InventorySubCategoryMaster.InventorySubCatId  inner join InventoryMaster on InventoryMaster.InventorySubSubId= InventoruSubSubCategory.InventorySubSubId inner join InventoryBarcodeMaster on InventoryBarcodeMaster.InventoryMaster_id=InventoryMaster.InventoryMasterId inner join InventoryDetails on  InventoryDetails.Inventory_Details_Id=InventoryMaster.InventoryDetailsId Where InventoryMasterId='" +ddlproduct.SelectedValue+ "' and  InventoryCategoryMaster.compid='" + Session["comid"] + "' and InventoryCategoryMaster.CatType IS NULL", con);
      
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        if (ds.Rows.Count > 0)
        {
            lblinvid.Text = Convert.ToString(ds.Rows[0]["Name"]);
            lblpbarcode.Text = Convert.ToString(ds.Rows[0]["Barcode"]);
            lblpnumber.Text = Convert.ToString(ds.Rows[0]["ProductNo"]);
            lblcategory.Text = Convert.ToString(ds.Rows[0]["InventoryCatName"]);
            lblsubcategory.Text = Convert.ToString(ds.Rows[0]["InventorySubCatName"]);
            lblsubsubcat.Text = Convert.ToString(ds.Rows[0]["InventorySubSubName"]);
            lbldis.Text = Convert.ToString(ds.Rows[0]["Description"]);
        }

        string strwh = "SELECT InventoryWarehouseMasterTbl.InventoryWarehouseMasterId,Party_Master.Compname, UnitTypeMaster.Name as utype,WarehouseMaster.Name as Wname, InventoryWarehouseMasterTbl.Rate,InventoryWarehouseMasterTbl.OpeningQty as Qty,InventoryWarehouseMasterTbl.Weight FROM WarehouseMaster inner join InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.WareHouseId=WarehouseMaster.WareHouseId inner join Party_Master on Party_Master.PartyId=InventoryWarehouseMasterTbl.PreferredVendorId inner join UnitTypeMaster on UnitTypeMaster.UnitTypeId=InventoryWarehouseMasterTbl.UnitTypeId where  InventoryMasterId='" + ddlproduct.SelectedValue + "'";
        SqlCommand cmdwh = new SqlCommand(strwh, con);
        SqlDataAdapter adpwh = new SqlDataAdapter(cmdwh);
        DataTable dtwh = new DataTable();
        adpwh.Fill(dtwh);
        if (dtwh.Rows.Count > 0)
        {
            grdd.DataSource = dtwh;
            grdd.DataBind();

        }
        FillGridView1();
        FillGrid2DiffView();
        fillaudio();
        fillvidio();
    }
    protected void fillvidio()
    {
        grdvidio.DataSource = null;
        grdvidio.DataBind();
        if (ddlproduct.SelectedIndex > -1)
        {
            string strg = " Select * from InventoryMediaMaster where InventoryMasterId='" + ddlproduct.SelectedValue + "' and MediaFileTypeID='2' ";
            SqlCommand cmdg = new SqlCommand(strg, con);
            SqlDataAdapter adpg = new SqlDataAdapter(cmdg);
            DataTable dtg = new DataTable();
            adpg.Fill(dtg);

            if (dtg.Rows.Count > 0)
            {

                grdvidio.DataSource = dtg;
                grdvidio.DataBind();



                Label lblSmallImageText = (Label)grdvidio.Rows[0].FindControl("lblSmallImageText");


                if (lblSmallImageText != null)
                {

                    if (dtg.Rows[0]["FileName"].ToString() != "")
                    {
                        lblSmallImageText.Visible = true;
                        lblSmallImageText.Text = dtg.Rows[0]["FileName"].ToString();
                        //smImg.Width = 100;
                        //smImg.Height = 100;

                    }
                    else
                    {
                        lblSmallImageText.Visible = false;
                        lblSmallImageText.Visible = true;




                    }
                }


            }
            else
            {

                dtg = GRDAUDIO();
                DataRow Drow = dtg.NewRow();
                Drow["FileName"] = "";


                dtg.Rows.Add(Drow);
                grdvidio.DataSource = dtg;
                grdvidio.DataBind();





            }


        }
    }
    protected void grdvidio_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdvidio.EditIndex = e.NewEditIndex;

        //GridView1.DataSource = dstable;
        //GridView1.DataBind();
        fillvidio();
    }

    protected void grdvidio_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string fileSmallName = "";
        GridViewRow gdr = grdvidio.Rows[e.RowIndex];
        FileUpload fpdsm = (FileUpload)gdr.FindControl("fileaudio");
        if (fpdsm.HasFile == true)
        {
            string ext = "";
            string[] validFileTypes = { "mp3", "mp4", "wmv", "vlc", "mkv", "avi", "mpg", "flv", "wma" };
            bool isValidFile = false;
            ext = System.IO.Path.GetExtension(fpdsm.PostedFile.FileName);
            for (int i = 0; i < validFileTypes.Length; i++)
            {

                if (ext == "." + validFileTypes[i])
                {

                    isValidFile = true;

                    break;

                }

            }
            if (!isValidFile)
            {
                Label1.Visible = true;
                Label1.Text = "Invalid File. Please upload a File with extension " +

                         string.Join(",", validFileTypes);
            }
            else
            {
                int l = fpdsm.PostedFile.ContentLength;
                k = l / 1048576;
                if (k < 10)
                {
                    fileSmallName = Session["Comid"] + "/Vidio/" + fpdsm.FileName.ToString();
                    fpdsm.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\Vidio\\") + fpdsm.FileName.ToString());
                    string strg = " Select * from InventoryMediaMaster where InventoryMasterId='" + ddlproduct.SelectedValue + "' and MediaFileTypeID='2' ";

                    SqlCommand cmdfind = new SqlCommand(strg, con);
                    SqlDataAdapter dtpfind = new SqlDataAdapter(cmdfind);
                    DataTable dsfind = new DataTable();
                    dtpfind.Fill(dsfind);
                    if (dsfind.Rows.Count > 0)
                    {
                        if (fpdsm.HasFile)
                        {
                            if (fileSmallName.Length > 0)
                            {
                                string st = "Update InventoryMediaMaster set FileName='" + fileSmallName + "' where InventoryMasterId='" + ddlproduct.SelectedValue + "' and MediaFileTypeID='2'";
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
                        }
                    }
                    else
                    {
                        if (fpdsm.HasFile)
                        {
                            if (fileSmallName.Length > 0)
                            {
                                string insertstrinImgDe = "Insert into InventoryMediaMaster(InventoryMasterId,FileName,MediaFileTypeID,EntryDate) values('" + ddlproduct.SelectedValue + "', " +
                                             "  '" + fileSmallName + "','2','" + System.DateTime.Now.ToString("MM/dd/yyyy") + "')";
                                SqlCommand cmdint = new SqlCommand(insertstrinImgDe, con);
                                if (con.State.ToString() != "Open")
                                {
                                    con.Open();
                                }
                                cmdint.ExecuteNonQuery();
                                con.Close();
                                Label1.Text = "Record inserted successfully";
                                Label1.Visible = true;
                            }
                        }
                    }
                }
                else
                {
                    Label1.Visible = true;
                    Label1.Text = "Please upload file Less than of size 10 MB";
                }
            }               
        }
        grdvidio.EditIndex = -1;
        fillvidio();
    }
    protected void grdvidio_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdvidio.EditIndex = -1;
        fillvidio();
    }

    protected void fillaudio()
    {
        grdaudio.DataSource = null;
        grdaudio.DataBind();
        if (ddlproduct.SelectedIndex > -1)
        {
            string strg = " Select * from InventoryMediaMaster where InventoryMasterId='" + ddlproduct.SelectedValue + "' and MediaFileTypeID='1' ";
            SqlCommand cmdg = new SqlCommand(strg, con);
            SqlDataAdapter adpg = new SqlDataAdapter(cmdg);
            DataTable dtg = new DataTable();
            adpg.Fill(dtg);

            if (dtg.Rows.Count > 0)
            {

                grdaudio.DataSource = dtg;
                grdaudio.DataBind();



                Label lblSmallImageText = (Label)grdaudio.Rows[0].FindControl("lblSmallImageText");
              
               

                if (lblSmallImageText != null)
                {

                    if (dtg.Rows[0]["FileName"].ToString() != "")
                    {
                        lblSmallImageText.Visible = true;
                        lblSmallImageText.Text = dtg.Rows[0]["FileName"].ToString();
                       
                        //smImg.Width = 100;
                        //smImg.Height = 100;

                    }
                    else
                    {
                        lblSmallImageText.Visible = false;
                        lblSmallImageText.Visible = true;

                        


                    }
                }


            }
            else
            {

                dtg = GRDAUDIO();
                DataRow Drow = dtg.NewRow();
                Drow["FileName"] ="";
              
               
                dtg.Rows.Add(Drow);
                grdaudio.DataSource = dtg;
                grdaudio.DataBind();

               // HtmlAnchor playaudio = (HtmlAnchor)grdaudio.Rows[0].FindControl("playaudio");
              //  playaudio.Visible = false;
            

            }


        }
    }
    protected void grdaudio_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdaudio.EditIndex = e.NewEditIndex;

        //GridView1.DataSource = dstable;
        //GridView1.DataBind();
        fillaudio();
    }

    protected void grdaudio_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string fileSmallName = "";
        GridViewRow gdr = grdaudio.Rows[e.RowIndex];           
        FileUpload fpdsm = (FileUpload)gdr.FindControl("fileaudio");       
        if (fpdsm.HasFile == true)
        {
            string ext = "";
            string[] validFileTypes = { "mp3", "mp4", "wmv", "vlc", "mkv", "avi", "mpg", "flv", "wma" };
            bool isValidFile = false;
            ext = System.IO.Path.GetExtension(fpdsm.PostedFile.FileName);
            for (int i = 0; i < validFileTypes.Length; i++)
            {

                if (ext == "." + validFileTypes[i])
                {

                    isValidFile = true;

                    break;

                }

            }
            if (!isValidFile)
            {
                Label1.Visible = true;
                Label1.Text = "Invalid File. Please upload a File with extension " +

                         string.Join(",", validFileTypes);
            }
            else
            {
                int l = fpdsm.PostedFile.ContentLength;
                k = l / 1048576;
                if (k < 10)
                {
                    fileSmallName = Session["Comid"] + "/Audio/" + fpdsm.FileName.ToString();
                    fpdsm.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\Audio\\") + fpdsm.FileName.ToString());
                    string strg = " Select * from InventoryMediaMaster where InventoryMasterId='" + ddlproduct.SelectedValue + "' and MediaFileTypeID='1' ";

                    SqlCommand cmdfind = new SqlCommand(strg, con);
                    SqlDataAdapter dtpfind = new SqlDataAdapter(cmdfind);
                    DataTable dsfind = new DataTable();
                    dtpfind.Fill(dsfind);
                    if (dsfind.Rows.Count > 0)
                    {
                        if (fileSmallName.Length > 0)
                        {
                            string st = "Update InventoryMediaMaster set FileName='" + fileSmallName + "' where InventoryMasterId='" + ddlproduct.SelectedValue + "' and MediaFileTypeID='1'";
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
                    }
                    else
                    {                        
                        if (fileSmallName.Length > 0)
                        {
                            string insertstrinImgDe = "Insert into InventoryMediaMaster(InventoryMasterId,FileName,MediaFileTypeID,EntryDate) values('" + ddlproduct.SelectedValue + "', " +
                                         "  '" + fileSmallName + "','1','" + System.DateTime.Now.ToString("MM/dd/yyyy") + "')";
                            SqlCommand cmdint = new SqlCommand(insertstrinImgDe, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmdint.ExecuteNonQuery();
                            con.Close();
                            Label1.Text = "Record inserted successfully";
                            Label1.Visible = true;
                        }                        
                    }

                }
                else
                {
                    Label1.Visible = true;
                    Label1.Text = "Please upload file Less than of size 10 MB";
                }
            }
        }
        grdaudio.EditIndex = -1;
        fillaudio();
    }
    protected void grdaudio_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdaudio.EditIndex = -1;
        fillaudio();
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
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "addsmall")
        {
            String filename = "";
            GridViewRow gdr = (GridViewRow)((Button)e.CommandSource).NamingContainer;
            FileUpload fpdsm = (FileUpload)gdr.FindControl("FileUploadSmallImage");
            Image imgsmall = (Image)gdr.FindControl("imgsmall");
            filename = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "_";
            if (fpdsm.HasFile == true)
            {
                bool valid = ext(fpdsm.FileName);
                if (valid == true)
                {
                    fpdsm.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\Thumbnail\\") + filename + fpdsm.FileName.ToString());
                    string fileSmallName = Session["comid"] + "/Thumbnail/" + filename + fpdsm.FileName.ToString();
                    ViewState["fileSmallName"] = filename + fpdsm.FileName.ToString();
                    imgsmall.Visible = true;
                    imgsmall.ImageUrl = "~/Account/" + fileSmallName;
                }
                else
                {
                    Label1.Visible = true;
                    Label1.Text = "This file is not valid";
                }
                //fpdsm.PostedFile.SaveAs(Server.MapPath("..\\Thumbnail\\") + fpdsm.FileName);
                //FileUpload1.PostedFile.SaveAs(Server.MapPath("..\\Thumbnail\\") + FileUpload1.FileName);

            }
        }
        if (e.CommandName == "addLarge")
        {
            String filename1 = "";
            GridViewRow gdr = (GridViewRow)((Button)e.CommandSource).NamingContainer;
            filename1 = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "_";
            Image imglarge = (Image)gdr.FindControl("imglarge");
            FileUpload fpdlrg = (FileUpload)gdr.FindControl("FileUploadLargeImage");
            if (fpdlrg.HasFile == true)
            {
                bool valid = ext(fpdlrg.FileName);
                if (valid == true)
                {
                    fpdlrg.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\LargeImg\\") + filename1 + fpdlrg.FileName.ToString());
                    string file1LargeName = Session["comid"] + "/LargeImg/" + filename1 + fpdlrg.FileName.ToString();
                    ViewState["file1LargeName"] = filename1 + fpdlrg.FileName.ToString();
                    imglarge.Visible = true;
                    imglarge.ImageUrl = "~/Account/" + file1LargeName;
                }
                else
                {
                    Label1.Visible = true;
                    Label1.Text = "This file is not valid";
                }
            }
        }
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Label1.Text = "";
        GridView1.EditIndex = e.NewEditIndex;

        //GridView1.DataSource = dstable;
        //GridView1.DataBind();
        FillGridView1();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        FillGridView1();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
       // string fileSmallName = "";
       // string file1LargeName = "";
        
       // String filename = "";
       // String filename1 = "";
        GridViewRow gdr = GridView1.Rows[e.RowIndex];
        Label lblinvmid = (Label)gdr.FindControl("lblInvMid");
        Label lblinvImgId = (Label)gdr.FindControl("lblInvImgId");
        ViewState["InvMid"] = lblinvmid.Text;
        ViewState["InvMimgid"] = lblinvImgId.Text;
        FileUpload fpdsm = (FileUpload)gdr.FindControl("FileUploadSmallImage");      
        FileUpload fpdlrg = (FileUpload)gdr.FindControl("FileUploadLargeImage");
        Image imgsmall = (Image)gdr.FindControl("imgsmall");
        Image imglarge = (Image)gdr.FindControl("imglarge");
        // string id = ((Label)gdr.FindControl("lblviewid")).Text;
        //if (fpdsm.HasFile == true)
        //{
        //    ext = System.IO.Path.GetExtension(fpdsm.PostedFile.FileName);
        //        for (int i = 0; i < validFileTypes.Length; i++)
        //        {

        //            if (ext == "." + validFileTypes[i])
        //            {

        //                isValidFile = true;

        //                break;

        //            }

        //        }
        //        if (!isValidFile)
        //        {
        //            Label1.Visible = true;
        //            Label1.Text = "Invalid File. Please upload a File with extension " +

        //                     string.Join(",", validFileTypes);
        //        }
        //        else
        //        {
        //            filename = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "_";

        //            fpdsm.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\Thumbnail\\") + filename + fpdsm.FileName.ToString());
        //             fileSmallName = Session["Comid"] + "/Thumbnail/" + filename + fpdsm.FileName.ToString();
        //        }
        //}
        //if (fpdlrg.HasFile == true)
        //{
        //    ext = System.IO.Path.GetExtension(fpdlrg.PostedFile.FileName);
        //        for (int i = 0; i < validFileTypes.Length; i++)
        //        {

        //            if (ext == "." + validFileTypes[i])
        //            {

        //                isValidFile = true;

        //                break;

        //            }

        //        }
        //        if (!isValidFile)
        //        {
        //            Label1.Visible = true;
        //            Label1.Text = "Invalid File. Please upload a File with extension " +

        //                     string.Join(",", validFileTypes);
        //        }
        //        else
        //        {
        //            filename1 = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Second.ToString() + "_";

        //            fpdlrg.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\LargeImg\\") + filename1 + fpdlrg.FileName.ToString());
        //             file1LargeName = Session["Comid"] + "/LargeImg/" + filename1 + fpdlrg.FileName.ToString();
        //        }
        //}


        string ssssss = " SELECT     InventoryMaster.InventoryMasterId, InventoryMaster.Name, " +
                    " InventoryImgMaster.InventoryImgMasterID, InventoryImgMaster.Thumbnail, " +
                    "  InventoryImgMaster.LargeImg, InventoryImgMaster.EntryDate " +
                     " FROM         InventoryImgMaster LEFT OUTER JOIN " +
                     " InventoryMaster ON InventoryImgMaster.InventoryMasterId = InventoryMaster.InventoryMasterId " +
                " where InventoryImgMaster.InventoryImgMasterID='" + ViewState["InvMimgid"] + "' ";
        SqlCommand cmdfind = new SqlCommand(ssssss, con);
        SqlDataAdapter dtpfind = new SqlDataAdapter(cmdfind);
        DataTable dsfind = new DataTable();
        dtpfind.Fill(dsfind);

        if (dsfind.Rows.Count > 0)
        {
            if (ViewState["fileSmallName"] != null)
            {
                string fileSmallName = Session["comid"] + "/Thumbnail/" + ViewState["fileSmallName"];
                string st = "Update InventoryImgMaster set Thumbnail='" + fileSmallName + "' where InventoryImgMasterID='" + dsfind.Rows[0]["InventoryImgMasterID"].ToString() + "'";
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
            }
            if (ViewState["file1LargeName"] != null)
            {
                string file1LargeName = Session["comid"] + "/LargeImg/" + ViewState["file1LargeName"];
                string st = "Update InventoryImgMaster set LargeImg='" + file1LargeName + "' where InventoryImgMasterID='" + dsfind.Rows[0]["InventoryImgMasterID"].ToString() + "'";
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
            }
            if (fpdsm.HasFile != false)
            {                
                bool valid = ext(fpdsm.FileName);
                if (valid == true)
                {
                    string filename;
                    filename = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "_" + fpdsm.FileName;
                    fpdsm.PostedFile.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\Thumbnail\\") + filename);
                    string fileSmallName = Session["comid"] + "/Thumbnail/" + filename;
                    string st = "Update InventoryImgMaster set Thumbnail='" + fileSmallName + "' where InventoryImgMasterID='" + dsfind.Rows[0]["InventoryImgMasterID"].ToString() + "'";
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
                }
                else
                {
                    Label1.Visible = true;
                    Label1.Text = "This file is not valid";
                }                
            }
            if (fpdlrg.HasFile != false)
            {                
                bool valid = ext(fpdlrg.FileName);
                if (valid == true)
                {
                    string filename1 = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "_" + fpdlrg.FileName;
                    fpdlrg.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\LargeImg\\") + filename1);
                    string file1LargeName = Session["comid"] + "/LargeImg/" + filename1;

                    string st = "Update InventoryImgMaster set LargeImg='" + file1LargeName + "' where InventoryImgMasterID='" + dsfind.Rows[0]["InventoryImgMasterID"].ToString() + "'";
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
                }
                else
                {
                    Label1.Visible = true;
                    Label1.Text = "This file is not valid";
                }                
            } 
        }
        else
        {
            if (imgsmall.ImageUrl != "")
            {
                string fileSmallName = Session["comid"] + "/ViewSmallImg/" + ViewState["filename"];
                if (imglarge.ImageUrl != "")
                {
                    string file1LargeName = Session["comid"] + "/ViewLargeImg/" + ViewState["filename1"];

                    string insertstrinImgDe = "Insert into InventoryImgMaster(InventoryMaster_Id,Thumbnail,LargeImg,EntryDate) values('" + ViewState["InvMid"].ToString() + "', '" + fileSmallName + "','" + file1LargeName + "','" + System.DateTime.Now.ToString("MM/dd/yyyy") + "')";
                    SqlCommand cmdint = new SqlCommand(insertstrinImgDe, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdint.ExecuteNonQuery();
                    con.Close();
                    Label1.Text = "Record inserted successfully";
                    Label1.Visible = true;
                }
                else
                {
                    string insertstrinImgDe = "Insert into InventoryImgMaster(InventoryMaster_Id,Thumbnail,EntryDate) values('" + ViewState["InvMid"].ToString() + "', '" + fileSmallName + "','" + System.DateTime.Now.ToString("MM/dd/yyyy") + "')";
                    SqlCommand cmdint = new SqlCommand(insertstrinImgDe, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdint.ExecuteNonQuery();
                    con.Close();
                    Label1.Text = "Record inserted successfully";
                    Label1.Visible = true;
                }
            }
            if (fpdsm.HasFile != false)
            {
                bool valid = ext(fpdsm.FileName);
                if (valid == true)
                {
                    string filename;
                    filename = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "_" + fpdsm.FileName;
                    fpdsm.PostedFile.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\Thumbnail\\") + filename);
                    string fileSmallName = Session["comid"] + "/Thumbnail/" + filename;
                    if (fpdlrg.HasFile != false)
                    {
                        bool valid1 = ext(fpdlrg.FileName);
                        if (valid1 == true)
                        {
                            string filename1 = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "_" + fpdlrg.FileName;
                            fpdlrg.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\LargeImg\\") + filename1);
                            string file1LargeName = Session["comid"] + "/LargeImg/" + filename1;

                            string insertstrinImgDe = "Insert into InventoryImgMaster(InventoryMaster_Id,Thumbnail,LargeImg,EntryDate) values('" + ViewState["InvMid"].ToString() + "', '" + fileSmallName + "','" + file1LargeName + "','" + System.DateTime.Now.ToString("MM/dd/yyyy") + "')";
                            SqlCommand cmdint = new SqlCommand(insertstrinImgDe, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmdint.ExecuteNonQuery();
                            con.Close();
                            Label1.Text = "Record inserted successfully";
                            Label1.Visible = true;
                        }
                        else
                        {
                            Label1.Visible = true;
                            Label1.Text = "This file is not valid";
                        }
                    }
                    else
                    {
                        string insertstrinImgDe = "Insert into InventoryImgMaster(InventoryMaster_Id,Thumbnail,EntryDate) values('" + ViewState["InvMid"].ToString() + "', '" + fileSmallName + "','" + System.DateTime.Now.ToString("MM/dd/yyyy") + "')";
                        SqlCommand cmdint = new SqlCommand(insertstrinImgDe, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmdint.ExecuteNonQuery();
                        con.Close();
                        Label1.Text = "Record inserted successfully";
                        Label1.Visible = true;
                    }
                }
                else
                {
                    Label1.Visible = true;
                    Label1.Text = "This file is not valid";
                }
            }
            else if (fpdlrg.HasFile != false)
            {
                bool valid1 = ext(fpdlrg.FileName);
                if (valid1 == true)
                {
                    string filename1 = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "_" + fpdlrg.FileName;
                    fpdlrg.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\LargeImg\\") + filename1);
                    string file1LargeName = Session["comid"] + "/LargeImg/" + filename1;
                    string insertstrinImgDe = "Insert into InventoryImgMaster(InventoryMaster_Id,LargeImg,EntryDate) values('" + ViewState["InvMid"].ToString() + "','" + file1LargeName + "','" + System.DateTime.Now.ToString("MM/dd/yyyy") + "')";
                    SqlCommand cmdint = new SqlCommand(insertstrinImgDe, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdint.ExecuteNonQuery();
                    con.Close();
                    Label1.Text = "Record inserted successfully";
                    Label1.Visible = true;
                }
                else
                {
                    Label1.Visible = true;
                    Label1.Text = "This file is not valid";
                }
            }
            //if (filename1.Length > 0 || filename.Length > 0)
            //{

            //   string insertstrinImgDe = "Insert into InventoryImgMaster(InventoryMasterId,Thumbnail,LargeImg,EntryDate) values('" + ViewState["InvMid"].ToString() + "', " +
            //                 "  '" + fileSmallName + "','" + file1LargeName + "','" + System.DateTime.Now.ToString("MM/dd/yyyy") + "')";
            //    SqlCommand cmdint = new SqlCommand(insertstrinImgDe, con);
            //    if (con.State.ToString() != "Open")
            //    {
            //        con.Open();
            //    }
            //    cmdint.ExecuteNonQuery();
            //    con.Close();

            //    Label1.Text = "Record inserted successfully";
            //    Label1.Visible = true;

            //}
        }

        GridView1.EditIndex = -1;

      
        FillGridView1();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {


        GridViewRow ggghhh = GridView1.Rows[e.RowIndex];
        Image smImg = (Image)ggghhh.FindControl("imgsmall");
        Label smLbl = (Label)ggghhh.FindControl("lblSmallImageText");
        Image lrgImg = (Image)ggghhh.FindControl("imglarge");
        Label lrgLbl = (Label)ggghhh.FindControl("lblLargeImageText");
        Label lblimgdid = (Label)ggghhh.FindControl("lblInvImgId");


        if (smImg != null && smLbl != null && lrgLbl != null && lrgImg != null && lblimgdid != null)
        {
            if (smLbl.Text == " No Image Available")
            {

            }
            else
            {
                string dlesm = " Delete    InventoryImgMaster  where InventoryImgMasterID='" + lblimgdid.Text + "'  ";//where  Thumbnail='' 
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


            }
            else
            {
                string dlesm1 = " Delete    InventoryImgMaster  where InventoryImgMasterID='" + lblimgdid.Text + "'  ";//where  LargeImg='' 
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
        FillGridView1();
    }
    public DataTable GRDAUDIO()
    {
        DataTable dt = new DataTable();
        DataColumn Dcom = new DataColumn();
        Dcom.DataType = System.Type.GetType("System.String");
        Dcom.ColumnName = "FileName";
        Dcom.AllowDBNull = true;
        Dcom.Unique = false;
        Dcom.ReadOnly = false;
       

    
        DataColumn Dcom1 = new DataColumn();
        Dcom1.DataType = System.Type.GetType("System.String");
        Dcom1.ColumnName = "InventoryMasterId";
        Dcom1.AllowDBNull = true;
        Dcom1.Unique = false;
        Dcom1.ReadOnly = false;
       


        dt.Columns.Add(Dcom);
        dt.Columns.Add(Dcom1);
        return dt;
    }
    public DataTable GRDIMAGE()
    {
        DataTable dt = new DataTable();
        DataColumn Dcom = new DataColumn();
        Dcom.DataType = System.Type.GetType("System.String");
        Dcom.ColumnName = "InventoryMasterId";
        Dcom.AllowDBNull = true;
        Dcom.Unique = false;
        Dcom.ReadOnly = false;

        DataColumn Dcom1 = new DataColumn();
        Dcom1.DataType = System.Type.GetType("System.String");
        Dcom1.ColumnName = "InventoryImgMasterID";
        Dcom1.AllowDBNull = true;
        Dcom1.Unique = false;
        Dcom1.ReadOnly = false;

        
        dt.Columns.Add(Dcom);
        dt.Columns.Add(Dcom1);
      
      
        return dt;
    }
    protected void FillGridView1()
    {
        GridView1.DataSource = null;
        GridView1.DataBind();
        if (ddlproduct.SelectedIndex > -1)
        {
            string strg = " SELECT     InventoryMaster.InventoryMasterId, InventoryMaster.Name, " +
                " InventoryImgMaster.InventoryImgMasterID, InventoryImgMaster.Thumbnail, " +
                "  InventoryImgMaster.LargeImg, InventoryImgMaster.EntryDate " +
                 " FROM         InventoryImgMaster LEFT OUTER JOIN " +
                 " InventoryMaster ON InventoryImgMaster.InventoryMasterId = InventoryMaster.InventoryMasterId " +
            " where InventoryMaster.InventoryMasterId='" + ddlproduct.SelectedValue + "' ";
            SqlCommand cmdg = new SqlCommand(strg, con);
            SqlDataAdapter adpg = new SqlDataAdapter(cmdg);
            DataTable dtg = new DataTable();
            adpg.Fill(dtg);

            if (dtg.Rows.Count > 0)
            {

                GridView1.DataSource = dtg;
                GridView1.DataBind();
                ViewState["InvMid"] = dtg.Rows[0]["InventoryMasterId"].ToString();

                Image smImg = (Image)GridView1.Rows[0].FindControl("imgsmall");
                Label smLbl = (Label)GridView1.Rows[0].FindControl("lblSmallImageText");
                Image lrgImg = (Image)GridView1.Rows[0].FindControl("imglarge");
                Label lrgLbl = (Label)GridView1.Rows[0].FindControl("lblLargeImageText");



                if (dtg.Rows[0]["Thumbnail"].ToString() != "")
                {
                    smImg.Visible = true;
                    smImg.ImageUrl = "~/Account/" + dtg.Rows[0]["Thumbnail"].ToString();
                    //smImg.Width = 100;
                    //smImg.Height = 100;
                    smLbl.Visible = true;
                    smLbl.Text = dtg.Rows[0]["Thumbnail"].ToString();
                }
                else
                {
                    smImg.Visible = false;
                    smLbl.Visible = true;
                }
                if (dtg.Rows[0]["LargeImg"].ToString() != "")
                {
                    lrgImg.Visible = true;
                    lrgImg.ImageUrl = "~/Account/" + dtg.Rows[0]["LargeImg"].ToString();
                    //lrgImg.Width = 100;
                    //lrgImg.Height = 100;
                    lrgLbl.Visible = true;
                    lrgLbl.Text = dtg.Rows[0]["LargeImg"].ToString();
                }
                else
                {
                    lrgImg.Visible = false;
                    lrgLbl.Visible = true;

                }
            }
            else
            {
                dtg = GRDIMAGE();
                DataRow Drow = dtg.NewRow();
                Drow["InventoryMasterId"] =ddlproduct.SelectedValue;
                Drow["InventoryImgMasterID"] =0;
               
                dtg.Rows.Add(Drow);
                GridView1.DataSource = dtg;
                GridView1.DataBind();
               
               

            }


        }

    }
    protected void grdslide_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Label1.Text = "";
        grdslide.EditIndex = e.NewEditIndex;

        FillGrid2DiffView();

    }
    protected void grdslide_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        FillGrid2DiffView();
      
    }
    protected void grdslide_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridViewRow ggghhh = grdslide.Rows[e.RowIndex];
        Image smImg = (Image)ggghhh.FindControl("imgsmall");
        Label smLbl = (Label)ggghhh.FindControl("lblSmallImageText");
        Image lrgImg = (Image)ggghhh.FindControl("imglarge");
        Label lrgLbl = (Label)ggghhh.FindControl("lblLargeImageText");
        Label lblimgdid = (Label)ggghhh.FindControl("lblinvImgMdid");


        if (smImg != null && smLbl != null && lrgLbl != null && lrgImg != null && lblimgdid != null)
        {
            if (smLbl.Text == " No Image Available")
            {

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

        FillGrid2DiffView();
    }
    protected void grdslide_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //string fileSmallName = "";
        //string file1LargeName = "";
       // string[] validFileTypes = { "bmp", "gif", "png", "jpg", "jpeg", "JPG", "JPEG" };
       // bool isValidFile = false;
       // string ext = "";
        GridViewRow gdr = grdslide.Rows[e.RowIndex];

        FileUpload fpdsm = (FileUpload)gdr.FindControl("FileUploadSmallImage");
        FileUpload fpdlrg = (FileUpload)gdr.FindControl("FileUploadLargeImage");
        Image imgsmalled = (Image)gdr.FindControl("imgsmalled");
        Image imglargeed = (Image)gdr.FindControl("imglargeed");
        string id = ((Label)gdr.FindControl("lblviewid")).Text;       
        //String filename = "";
        //String filename1 = "";
        //if (fpdsm.HasFile)
        //{
        //    ext = System.IO.Path.GetExtension(fpdsm.PostedFile.FileName);
        //        for (int i = 0; i < validFileTypes.Length; i++)
        //        {

        //            if (ext == "." + validFileTypes[i])
        //            {

        //                isValidFile = true;

        //                break;

        //            }

        //        }
        //        if (!isValidFile)
        //        {
        //            Label1.Visible = true;
        //            Label1.Text = "Invalid File. Please upload a File with extension " +

        //                     string.Join(",", validFileTypes);
        //        }
        //        else
        //        {
        //            filename = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + "_" + fpdsm.FileName;
        //             fileSmallName = Session["Comid"] + "/ViewSmallImg/" + filename;

        //            fpdsm.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\ViewSmallImg\\") + filename);
        //        }
        //}
        //if (fpdlrg.HasFile)
        //{
        //    ext = System.IO.Path.GetExtension(fpdlrg.PostedFile.FileName);
        //        for (int i = 0; i < validFileTypes.Length; i++)
        //        {

        //            if (ext == "." + validFileTypes[i])
        //            {

        //                isValidFile = true;

        //                break;

        //            }

        //        }
        //        if (!isValidFile)
        //        {
        //            Label1.Visible = true;
        //            Label1.Text = "Invalid File. Please upload a File with extension " +

        //                     string.Join(",", validFileTypes);
        //        }
        //        else
        //        {
        //            filename1 = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "_" + fpdlrg.FileName;
        //             file1LargeName = Session["Comid"] + "/ViewLargeImg/" + filename1;

                       
        //            fpdlrg.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\ViewLargeImg\\") + filename1);
        //        }
        //}
        string ssssss = "SELECT     InventoryImgViewMaster.ViewID, InventoryImgViewMaster.ViewName, InventoryMaster.InventoryMasterId, InventoryMaster.Name, InventoryMaster.ProductNo, " +
             " InventoryMaster.MasterActiveStatus, InventoryImgMasterDetails.InventoryImgMasterDetails_Id, InventoryImgMasterDetails.SmallImageUrl,  " +
             " InventoryImgMasterDetails.LargeImageUrl " +
             " FROM         InventoryImgMasterDetails LEFT OUTER JOIN " +
             "  InventoryMaster ON InventoryImgMasterDetails.InventoryMaster_Id = InventoryMaster.InventoryMasterId RIGHT OUTER JOIN " +
             "  InventoryImgViewMaster ON InventoryImgMasterDetails.ViewID = InventoryImgViewMaster.ViewID " +
             " where InventoryMaster.InventoryMasterId='" + ddlproduct.SelectedValue + "' and  InventoryImgViewMaster.ViewID='" + id + "'  ";

        SqlCommand cmdfind = new SqlCommand(ssssss, con);
        SqlDataAdapter dtpfind = new SqlDataAdapter(cmdfind);
        DataTable dsfind = new DataTable();
        dtpfind.Fill(dsfind);

        if (dsfind.Rows.Count > 0)
        {

            if (dsfind.Rows[0]["InventoryImgMasterDetails_Id"].ToString() != null)
            {
                if (imgsmalled.ImageUrl != "")
                {

                    string fileSmallName = Session["comid"] + "/ViewSmallImg/" + ViewState["filenamesmall"];

                    string strimg = "select SmallImageUrl from InventoryImgMasterDetails where SmallImageUrl='" + fileSmallName + "' and ViewID='" + id + "'";

                    SqlCommand cmdimg = new SqlCommand(strimg, con);
                    SqlDataAdapter adpimg = new SqlDataAdapter(cmdimg);
                    DataTable dtimg = new DataTable();
                    adpimg.Fill(dtimg);

                    if (dtimg.Rows.Count > 0)
                    {
                        string fileimg = ViewState["filenamesmall"].ToString();
                        string fileSmallName1 = Session["comid"] + "/ViewSmallImg/";

                        string st = "Update InventoryImgMasterDetails set SmallImageUrl='" + fileSmallName1 + "" + 1 + "" + fileimg + "' where InventoryImgMasterDetails_Id='" + dsfind.Rows[0]["InventoryImgMasterDetails_Id"].ToString() + "' and ViewID='" + id + "'";
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
                if (imglargeed.ImageUrl != "")
                {

                    string file1LargeName = Session["comid"] + "/ViewLargeImg/" + ViewState["filenamelarge"];
                    string strimg = "select LargeImageUrl from InventoryImgMasterDetails where LargeImageUrl='" + file1LargeName + "' and ViewID='" + id + "'";
                    SqlCommand cmdimg = new SqlCommand(strimg, con);
                    SqlDataAdapter adpimg = new SqlDataAdapter(cmdimg);
                    DataTable dtimg1 = new DataTable();
                    adpimg.Fill(dtimg1);
                    if (dtimg1.Rows.Count > 0)
                    {
                        string fileimg = ViewState["filenamelarge"].ToString();
                        string file1LargeName1 = Session["comid"] + "/ViewLargeImg/";

                        string st = "Update InventoryImgMasterDetails set LargeImageUrl='" + file1LargeName1 + "" + 1 + "" + fileimg + "' where InventoryImgMasterDetails_Id='" + dsfind.Rows[0]["InventoryImgMasterDetails_Id"].ToString() + "' and ViewID='" + id + "'";

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
                if (fpdsm.HasFile)
                {
                    bool valid = ext(fpdsm.FileName);
                    if (valid == true)
                    {
                        string filename;
                        filename = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + "_" + fpdsm.FileName;
                        fpdsm.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\ViewSmallImg\\") + filename);
                        string fileSmallName = Session["comid"] + "/ViewSmallImg/" + filename;
                        string strimg = "select SmallImageUrl from InventoryImgMasterDetails where SmallImageUrl='" + fileSmallName + "' and ViewID='" + id + "'";

                        SqlCommand cmdimg = new SqlCommand(strimg, con);
                        SqlDataAdapter adpimg = new SqlDataAdapter(cmdimg);
                        DataTable dtimg = new DataTable();
                        adpimg.Fill(dtimg);

                        if (dtimg.Rows.Count > 0)
                        {

                            string fileSmallName1 = Session["comid"] + "/ViewSmallImg/";

                            string st = "Update InventoryImgMasterDetails set SmallImageUrl='" + fileSmallName1 + "" + 1 + "" + filename + "' where InventoryImgMasterDetails_Id='" + dsfind.Rows[0]["InventoryImgMasterDetails_Id"].ToString() + "' and ViewID='" + id + "'";
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

                            string st = "Update InventoryImgMasterDetails set LargeImageUrl='" + file1LargeName1 + "" + 1 + "" + filename1 + "' where InventoryImgMasterDetails_Id='" + dsfind.Rows[0]["InventoryImgMasterDetails_Id"].ToString() + "' and ViewID='" + id + "'";

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
            //if (fpdsm.HasFile != false)
            //{
            //    if (filename.Length > 0)
            //    {                                          
            //        string st = "Update InventoryImgMasterDetails set SmallImageUrl='" + fileSmallName + "' where InventoryImgMasterDetails_Id='" + dsfind.Rows[0]["InventoryImgMasterDetails_Id"].ToString() + "'";
            //        //SqlCommand cmdup = new SqlCommand("Update InventoryImgMaster set Thumbnail='" + fileSmallName1 + "" + 1 + "" + fileimg + "' where InventoryImgMasterID='" + dsfind.Rows[0]["InventoryImgMasterID"].ToString() + "'", con);
            //        SqlCommand cmdup = new SqlCommand(st, con);
            //        if (con.State.ToString() != "Open")
            //        {
            //            con.Open();
            //        }
            //        cmdup.ExecuteNonQuery();
            //        con.Close();
            //        Label1.Visible = true;
            //        Label1.Text = "Record updated successfully";
            //    }                   
            //}
            //if (fpdlrg.HasFile != false)
            //{
            //    if (filename1.Length > 0)
            //    {
            //        //string fileSmallName = "Thumbnail/" + fpdsm.FileName.ToString();
            //        string st = "Update InventoryImgMasterDetails set LargeImageUrl='" + file1LargeName + "' where InventoryImgMasterDetails_Id='" + dsfind.Rows[0]["InventoryImgMasterDetails_Id"].ToString() + "'";
            //        //SqlCommand cmdup = new SqlCommand("Update InventoryImgMaster set Thumbnail='" + fileSmallName1 + "" + 1 + "" + fileimg + "' where InventoryImgMasterID='" + dsfind.Rows[0]["InventoryImgMasterID"].ToString() + "'", con);
            //        SqlCommand cmdup = new SqlCommand(st, con);
            //        if (con.State.ToString() != "Open")
            //        {
            //            con.Open();
            //        }
            //        cmdup.ExecuteNonQuery();
            //        con.Close();
            //        Label1.Visible = true;
            //        Label1.Text = "Record updated successfully";
            //    }
            //}                                               
        }
        else
        {
            if (imgsmalled.ImageUrl != "")
            {
                //if (fpdsm.HasFile != false || fpdlrg.HasFile != false)
                // {
                string fileSmallName = Session["comid"] + "/ViewSmallImg/" + ViewState["filenamesmall"];
                if (imglargeed.ImageUrl != "")
                {
                    string file1LargeName = Session["comid"] + "/ViewLargeImg/" + ViewState["filenamelarge"];

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
                string file1LargeName = Session["comid"] + "/ViewLargeImg/" + ViewState["filenamelarge"];
                string insertstrinImgDet = "Insert into InventoryImgMasterDetails([InventoryMaster_Id],[LargeImageUrl],[ViewID]) values('" + ViewState["InvMid"].ToString() + "','" + file1LargeName + "','" + id + "')";
                SqlCommand cmdintt = new SqlCommand(insertstrinImgDet, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdintt.ExecuteNonQuery();
                con.Close();
            }

            if (fpdsm.HasFile != false)
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





            //if (fpdsm.PostedFile != null && fpdlrg.PostedFile != null)
            //{
            //    if (fpdsm.HasFile != false || fpdlrg.HasFile != false)
            //    {
            //        if (filename.Length > 0 || filename1.Length > 0)
            //        {

            //            string insertstrinImgDe = "Insert into InventoryImgMasterDetails(InventoryMaster_Id,SmallImageUrl,LargeImageUrl,ViewID) values('" + ViewState["InvMid"].ToString() + "', '" + fileSmallName + "','" + file1LargeName + "','" + id + "')";
            //            SqlCommand cmdint = new SqlCommand(insertstrinImgDe, con);
            //            if (con.State.ToString() != "Open")
            //            {
            //                con.Open();
            //            }
            //            cmdint.ExecuteNonQuery();
            //            con.Close();
            //            Label1.Visible = true;
            //            Label1.Text = "Record inserted successfully";
            //        }
            //    }

            //}
        }

        grdslide.EditIndex = -1;

        //GridView1.DataSource = dstable;
        //GridView1.DataBind();
        FillGrid2DiffView();
    }

    protected void FillGrid2DiffView()
    {
        SqlCommand cmdg = new SqlCommand("select ViewName,ViewID from InventoryImgViewMaster", con);
        SqlDataAdapter dtpg = new SqlDataAdapter(cmdg);
        DataTable dsg = new DataTable();
        dtpg.Fill(dsg);

        if (dsg.Rows.Count > 0)
        {
            grdslide.DataSource = dsg;
            grdslide.DataBind();


        }

        if (grdslide.Rows.Count > 0)
        {
            foreach (GridViewRow ggghhh in grdslide.Rows)
            {
                Label invVWid = (Label)ggghhh.FindControl("lblviewid");


                string strimg = "SELECT     InventoryImgViewMaster.ViewID, InventoryImgViewMaster.ViewName, InventoryMaster.InventoryMasterId, InventoryMaster.Name, InventoryMaster.ProductNo, " +
                     " InventoryMaster.MasterActiveStatus, InventoryImgMasterDetails.InventoryImgMasterDetails_Id, InventoryImgMasterDetails.SmallImageUrl,  " +
                     " InventoryImgMasterDetails.LargeImageUrl " +
                     " FROM         InventoryImgMasterDetails LEFT OUTER JOIN " +
                     "  InventoryMaster ON InventoryImgMasterDetails.InventoryMaster_Id = InventoryMaster.InventoryMasterId RIGHT OUTER JOIN " +
                     "  InventoryImgViewMaster ON InventoryImgMasterDetails.ViewID = InventoryImgViewMaster.ViewID " +
                     " where InventoryMaster.InventoryMasterId='" + ddlproduct.SelectedValue + "' and  InventoryImgViewMaster.ViewID='" + invVWid.Text + "'  ";
                SqlCommand cmd = new SqlCommand(strimg, con);
                SqlDataAdapter adpimg = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adpimg.Fill(dt);

                Image smImg = (Image)ggghhh.FindControl("imgsmall");
                Label smLbl = (Label)ggghhh.FindControl("lblSmallImageText");
                Image lrgImg = (Image)ggghhh.FindControl("imglarge");
                Label lrgLbl = (Label)ggghhh.FindControl("lblLargeImageText");
                Label imgdid = (Label)ggghhh.FindControl("lblinvImgMdid");
                ImageButton delesmallimg = (ImageButton)ggghhh.FindControl("delesmallimg");
                ImageButton delelargeimg = (ImageButton)ggghhh.FindControl("delelargeimg");

                if (smImg != null && smLbl != null && lrgLbl != null && lrgImg != null && imgdid != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["SmallImageUrl"].ToString() != "")
                        {
                            smImg.Visible = true;
                            smImg.ImageUrl = "~/Account/" + dt.Rows[0]["SmallImageUrl"].ToString();
                           
                            smLbl.Visible = true;
                            smLbl.Text = dt.Rows[0]["SmallImageUrl"].ToString();
                            imgdid.Text = dt.Rows[0]["InventoryImgMasterDetails_Id"].ToString();
                        }
                        else
                        {
                            smLbl.Visible = true;

                            smLbl.Text = " No Image Available";
                            delesmallimg.Visible = false;


                        }
                        if (dt.Rows[0]["LargeImageUrl"].ToString() != "")
                        {
                            lrgImg.Visible = true;
                            lrgImg.ImageUrl = "~/Account/" + dt.Rows[0]["LargeImageUrl"].ToString();

                            lrgLbl.Visible = true;
                            lrgLbl.Text = dt.Rows[0]["LargeImageUrl"].ToString();
                            imgdid.Text = dt.Rows[0]["InventoryImgMasterDetails_Id"].ToString();
                        }
                        else
                        {
                            lrgLbl.Visible = true;
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
        }


    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void grdslide_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void grdslide_RowCommand(object sender, GridViewCommandEventArgs e)
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
        if (e.CommandName == "adddetailsmall")
        {
            GridViewRow gdr = (GridViewRow)((Button)e.CommandSource).NamingContainer;

            FileUpload fpdsm = (FileUpload)gdr.FindControl("FileUploadSmallImage");

            Image imgsmalled = (Image)gdr.FindControl("imgsmalled");
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
                    ViewState["filenamesmall"] = filename;
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
                ViewState["filenamesmall"] = "";
            }
        }

        if (e.CommandName == "adddetailLarge")
        {
            GridViewRow gdr = (GridViewRow)((Button)e.CommandSource).NamingContainer;
            FileUpload fpdlrg = (FileUpload)gdr.FindControl("FileUploadLargeImage");
            Image imglargeed = (Image)gdr.FindControl("imglargeed");
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
                    ViewState["filenamelarge"] = filename1;
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
                ViewState["filenamelarge"] = "";
            }
        }
    }
}

