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

public partial class InventoryImgMaster : System.Web.UI.Page
{
    SqlConnection con;
    int i;
    string compid;
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

        compid = Session["Comid"].ToString();
        Page.Title = pg.getPageTitle(page);
      
        Label1.Visible = false;
        Panel2.Visible = false;
        if (!IsPostBack)
        {
            Pagecontrol.dypcontrol(Page, page);
           
           
            
            lblcomname.Text = Session["Cname"].ToString();

            storefill();
          
          
            ddlWarehouse_SelectedIndexChanged(sender, e);
            FillGridView1();
            lblbusiness.Text = ddlWarehouse.SelectedItem.Text;

         
        }

    }

    public DataSet fillddl()
    {

              
        string str1234 = "SELECT     LEFT(InventoryCategoryMaster.InventoryCatName, 8) + ' : ' + LEFT(InventorySubCategoryMaster.InventorySubCatName, 8) " +
                  "     + ' : ' + LEFT(InventoruSubSubCategory.InventorySubSubName, 8) + ' : ' + InventoryMaster.ProductNo + ' : ' + InventoryMaster.Name AS Name,InventoryMaster.ProductNo,InventoryMaster.InventoryMasterId " +
               "  FROM         InventoryCategoryMaster RIGHT OUTER JOIN " +
                "      InventorySubCategoryMaster ON InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId LEFT OUTER JOIN " +
                "      InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID RIGHT OUTER JOIN " +
                "      InventoryMaster ON InventoruSubSubCategory.InventorySubSubId = InventoryMaster.InventorySubSubId inner join InventoryWarehouseMasterTbl  on InventoryWarehouseMasterTbl.InventoryMasterId=InventoryMaster.InventoryMasterId Left OUTER JOIN InventoryImgMaster ON InventoryMaster.InventoryMasterId = InventoryImgMaster.InventoryMasterId " +
              " WHERE  (InventoryWarehouseMasterTbl.WareHouseId='" + ddlWarehouse.SelectedValue + "') and (InventoryCategoryMaster.CatType IS NULL) and  (InventoryMaster.MasterActiveStatus = 1) and ( InventoryCategoryMaster.compid='" + Session["comid"] + "') ";


       
        string str2 = "";
        string str3 = "";
        string str4 = "";

       
        if (ddlimageavaibility.SelectedValue == "1")
        {
            str2 = " and (InventoryImgMaster.Thumbnail IS not NULL) AND (InventoryImgMaster.LargeImg IS not NULL) ";
        }
        if (ddlimageavaibility.SelectedValue == "2")
        {
            str3 = " and ( (InventoryImgMaster.Thumbnail IS not NULL) AND (InventoryImgMaster.LargeImg IS  NULL) or (InventoryImgMaster.Thumbnail IS NULL) AND (InventoryImgMaster.LargeImg IS not  NULL) ) ";

        }
        if (ddlimageavaibility.SelectedValue == "3")
        {
            str4 = " and (InventoryImgMaster.Thumbnail IS  NULL) AND (InventoryImgMaster.LargeImg IS  NULL) ";
        }

        string sorting = " order by InventoryCategoryMaster.InventoryCatName,InventorySubCategoryMaster.InventorySubCatName,InventoruSubSubCategory.InventorySubSubName, InventoryMaster.Name";

        string finalstr = str1234 + str2 + str3 + str4 + sorting;
        SqlCommand cmd = new SqlCommand(finalstr, con);
       
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;

    }
  
    protected void Button2_Click(object sender, EventArgs e)
    {
        clean();

    }

    public void clean()
    {

       
        DropDownList1.SelectedIndex = 0;
        ddlWarehouse.SelectedIndex = 0;


    }
    protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (DropDownList1.SelectedIndex == 0)
        {
            i = 1;
            args.IsValid = false;


        }
        else
        {
            i = 0;
        }
    }
    
    protected void btnsubmit_Click(object sender, EventArgs e)
    {

        int flag = 0;
        if (i == 1)
        {


            return;

        }
        else
        {


            DateTime dt = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
            try
            {
                String filename = "";
                String filename1 = "";
                if (FileUpload1.HasFile)
                {

                   
                   filename = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "_" + FileUpload1.FileName;
                   bool valid = ext(FileUpload1.FileName);
                   if (valid == true)
                   {
                       // FileUpload1.PostedFile.SaveAs(Server.MapPath("..\\Thumbnail\\") + filename);
                       FileUpload1.PostedFile.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\Thumbnail\\") + filename);
                       flag = 2;
                   }
                   else
                   {
                       Label1.Visible = true;
                       Panel2.Visible = true;
                       Label1.Text = "Invalid File Type. Please upload an image file in one of the following formats: bmp, gif, png, jpg, jpeg, JPG, JPEG";
                   }
                   



                }
                if (FileUpload2.HasFile)
                {
                    filename1 = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString()  + "_" + FileUpload2.FileName;
                    //FileUpload2.PostedFile.SaveAs(Server.MapPath("..\\ViewLargeImg\\") + filename1);
                    bool valid = ext(FileUpload2.FileName);
                    if (valid == true)
                    {
                        FileUpload2.PostedFile.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\LargeImg\\") + filename1);
                        flag = 2;
                    }
                    else
                    {
                        Label1.Visible = true;
                        Panel2.Visible = true;
                        Label1.Text = "Invalid File Type. Please upload an image file in one of the following formats: bmp, gif, png, jpg, jpeg, JPG, JPEG";
                    }
                    


                }


                if (flag == 2)
                {
                    string inserrtimgmaster = "INSERT INTO InventoryImgMaster           (InventoryMasterId," +
                " Thumbnail,           LargeImg,             EntryDate) " +
                 " VALUES(  '" + DropDownList1.SelectedValue + "','" + Session["comid"] + "/Thumbnail/" + filename + "' " +
                 " ,'" + Session["comid"] + "/LargeImg/" + filename1 + "',   '" + Convert.ToDateTime(dt) + "' )";
                    SqlCommand cmd = new SqlCommand(inserrtimgmaster, con);

                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }

                    
                    cmd.ExecuteNonQuery();
                    con.Close();

                    Label1.Visible = true;
                    Label1.Text = "Record inserted successfully";

                    Panel2.Visible = false;
                   
                    FillGridView1();

                }
               // clean();
            }


            catch (Exception ex)
            {

                Label1.Visible = true;
                Label1.Text = "Error : " + ex.Message;

            }
            finally { }
        }
    }

    public bool ext(string filename)
    {
        string[] validFileTypes = { "bmp", "gif", "png", "jpg", "jpeg","JPG","JPEG" };

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
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

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
                    Label1.Text = "Invalid File Type. Please upload an image file in one of the following formats: bmp, gif, png, jpg, jpeg, JPG, JPEG";
                }
                //fpdsm.PostedFile.SaveAs(Server.MapPath("..\\Thumbnail\\") + fpdsm.FileName);
                //FileUpload1.PostedFile.SaveAs(Server.MapPath("..\\Thumbnail\\") + FileUpload1.FileName);

            }
        }
        if (e.CommandName == "addLarge")
        {
            String filename1 = "";
            GridViewRow gdr = (GridViewRow)((Button)e.CommandSource).NamingContainer;
            filename1 = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() +  "_";
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
                    Label1.Text = "Invalid File Type. Please upload an image file in one of the following formats: bmp, gif, png, jpg, jpeg, JPG, JPEG";
                }
            }
        }
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
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
        
       // String  filename = "";
       // String filename1 = "";
       
        GridViewRow gdr = GridView1.Rows[e.RowIndex];
        Label lblinvmid = (Label)gdr.FindControl("lblInvMid");
        Label lblinvImgId = (Label)gdr.FindControl("lblInvImgId");
        ViewState["InvMid"] = lblinvmid.Text;
        ViewState["InvMimgid"] = lblinvImgId.Text;
        
      // filename = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "_" ;
       FileUpload fpdsm = (FileUpload)gdr.FindControl("FileUploadSmallImage");
      // filename1 = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Second.ToString() + "_";

       FileUpload fpdlrg = (FileUpload)gdr.FindControl("FileUploadLargeImage");
        Image imgsmall = (Image)gdr.FindControl("imgsmall");
        Image imglarge = (Image)gdr.FindControl("imglarge");

        //if (fpdsm.HasFile == true)
        //{
        //    bool valid = ext(fpdsm.FileName);
        //    if (valid == true)
        //    {
        //        fpdsm.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\Thumbnail\\") + filename + fpdsm.FileName.ToString());
        //        string fileSmallName = Session["comid"] + "/Thumbnail/" + filename + fpdsm.FileName.ToString();
        //    }
        //    else
        //    {
        //        Label1.Visible = true;
        //        Label1.Text = "This is not Valid File";
        //    }
            
            
        //}

        //if (fpdlrg.HasFile == true)
        //{
        //    bool valid = ext(fpdlrg.FileName);
        //    if (valid == true)
        //    {
        //        fpdlrg.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\LargeImg\\") + filename1 + fpdlrg.FileName.ToString());
                

        //        string file1LargeName = Session["comid"] + "/LargeImg/" + filename + fpdlrg.FileName.ToString();
                
        //    }
        //    else
        //    {
        //        Label1.Visible = true;
        //        Label1.Text = "This is not Valid File";
        //    }
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
            //dtpfind
            if (dsfind.Rows[0]["InventoryImgMasterID"].ToString() != "")
            {
                if (ViewState["fileSmallName"] != null)
                {
                    //string fileSmallName;
                    //if (ViewState["fileSmallName"] != null)
                    //{
                        string fileSmallName = Session["comid"] + "/Thumbnail/" + ViewState["fileSmallName"];
                    //}
                    string strimg = "select Thumbnail from InventoryImgMaster where Thumbnail='"+ fileSmallName +"'" ;

                    SqlCommand cmdimg = new SqlCommand(strimg, con);
                    SqlDataAdapter adpimg = new SqlDataAdapter(cmdimg);
                    DataTable dtimg = new DataTable();
                    adpimg.Fill(dtimg);

                    if (dtimg.Rows.Count > 0)
                    {
                        //string fileimg = filename + fpdsm.FileName.ToString();
                        string fileSmallName1 = Session["comid"] + "/Thumbnail/";
                        //string fileSmallName = "Thumbnail/" + fpdsm.FileName.ToString();                       
                        //bool valid = ext(fpdsm.FileName);
                        //if (valid == true)
                        //{
                            //fpdsm.PostedFile.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\Thumbnail\\") + 1 + fpdsm.FileName);
                       
                            //string st = "Update InventoryImgMaster set Thumbnail='" + fileSmallName1 + "" + 1 + "" + ViewState["fileSmallName"] + "' where InventoryImgMasterID='" + dsfind.Rows[0]["InventoryImgMasterID"].ToString() + "'";
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
                        //}
                        //else
                        //{
                        //     Label1.Visible = true;
                        //     Label1.Text = "This is not Valid File";
                        //}
                    }
                    else
                    {
                        //string fileSmallName = "Thumbnail/" + fpdsm.FileName.ToString();
                        
                       // bool valid = ext(fpdsm.FileName);
                        //if (valid == true)
                       // {
                            //fpdsm.PostedFile.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\Thumbnail\\") + filename + fpdsm.FileName);
                            SqlCommand cmdup = new SqlCommand("Update InventoryImgMaster set Thumbnail='" + fileSmallName + "' where InventoryImgMasterID='" + dsfind.Rows[0]["InventoryImgMasterID"].ToString() + "'", con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmdup.ExecuteNonQuery();
                            con.Close();
                            Label1.Text = "Record updated successfully";
                            Label1.Visible = true;
                       // }
                       // else
                       // {
                            //Label1.Visible = true;
                            //Label1.Text = "This is not Valid File";
                       // }
                    }
                }

                if (ViewState["file1LargeName"] != null)
                {
                    //string file1LargeName;
                    //if (ViewState["file1LargeName"] != null)
                    //{
                        string file1LargeName = Session["comid"] + "/LargeImg/" + ViewState["file1LargeName"];
                   // }
                    
                    string strimg1 = "select LargeImg from InventoryImgMaster where LargeImg='" + file1LargeName + "'";

                    SqlCommand cmdimg = new SqlCommand(strimg1, con);
                    SqlDataAdapter adpimg = new SqlDataAdapter(cmdimg);
                    DataTable dtimg = new DataTable();
                    adpimg.Fill(dtimg);

                    if (dtimg.Rows.Count > 0)
                    {
                        //string fileimg1 = filename1 + fpdlrg.FileName.ToString();
                        string file1LargeName1 = Session["comid"] + "/LargeImg/";
                        //string fileSmallName = "Thumbnail/" + fpdsm.FileName.ToString();
                        
                        //bool valid = ext(fpdlrg.FileName);
                        //if (valid == true)
                        //{
                            //fpdlrg.PostedFile.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\LargeImg\\") + 1 + fpdlrg.FileName);
                            //string st = "Update InventoryImgMaster set LargeImg='" + file1LargeName1 + "" + 1 + "" + ViewState["file1LargeName"] + "' where InventoryImgMasterID='" + dsfind.Rows[0]["InventoryImgMasterID"].ToString() + "'";
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
                        //}
                        //else
                        //{
                        //    Label1.Visible = true;
                        //    Label1.Text = "This is not Valid File";
                        //}
                    }
                    else
                    {
                       // string file1LargeName = "LargeImg/" + fpdlrg.FileName.ToString();
                        
                       // bool valid = ext(fpdlrg.FileName);
                        //if (valid == true)
                        //{
                            //fpdlrg.PostedFile.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\LargeImg\\") + fpdlrg.FileName);
                            SqlCommand cmdup = new SqlCommand("Update InventoryImgMaster set LargeImg='" + file1LargeName + "' where InventoryImgMasterID='" + dsfind.Rows[0]["InventoryImgMasterID"].ToString() + "'", con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmdup.ExecuteNonQuery();
                            con.Close();
                            Label1.Text = "Record updated successfully";
                            Label1.Visible = true;
                        //}
                       // else
                       // {
                        //    Label1.Visible = true;
                        //    Label1.Text = "This is not Valid File";
                        //}
                    }
                }
                if (fpdsm.HasFile)
                {
                    bool valid = ext(fpdsm.FileName);
                    if (valid == true)
                    {
                        string filename;
                        filename = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "_" + fpdsm.FileName;
                        fpdsm.PostedFile.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\Thumbnail\\") + filename);
                        string fileSmallName = Session["comid"] + "/Thumbnail/" + filename;
                        string strimg = "select Thumbnail from InventoryImgMaster where Thumbnail='" + fileSmallName + "'";

                        SqlCommand cmdimg = new SqlCommand(strimg, con);
                        SqlDataAdapter adpimg = new SqlDataAdapter(cmdimg);
                        DataTable dtimg = new DataTable();
                        adpimg.Fill(dtimg);

                        if (dtimg.Rows.Count > 0)
                        {

                            string fileSmallName1 = Session["comid"] + "/Thumbnail/";

                            string st = "Update InventoryImgMaster set Thumbnail='" + fileSmallName + "' where InventoryImgMasterID='" + dsfind.Rows[0]["InventoryImgMasterID"].ToString() + "'";
                            
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

                            SqlCommand cmdup = new SqlCommand("Update InventoryImgMaster set Thumbnail='" + fileSmallName + "' where InventoryImgMasterID='" + dsfind.Rows[0]["InventoryImgMasterID"].ToString() + "'", con);
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
                        Label1.Text = "Invalid File Type. Please upload an image file in one of the following formats: bmp, gif, png, jpg, jpeg, JPG, JPEG";
                    }
                }
                if (fpdlrg.HasFile)
                {
                    bool valid = ext(fpdlrg.FileName);
                    if (valid == true)
                    {
                        string filename1 = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "_" + fpdlrg.FileName;
                        fpdlrg.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\LargeImg\\") + filename1);
                        string file1LargeName = Session["comid"] + "/LargeImg/" + filename1;

                        string strimg1 = "select LargeImg from InventoryImgMaster where LargeImg='" + file1LargeName + "'";

                        SqlCommand cmdimg = new SqlCommand(strimg1, con);
                        SqlDataAdapter adpimg = new SqlDataAdapter(cmdimg);
                        DataTable dtimg = new DataTable();
                        adpimg.Fill(dtimg);

                        if (dtimg.Rows.Count > 0)
                        {

                            string file1LargeName1 = Session["comid"] + "/LargeImg/";

                            string st = "Update InventoryImgMaster set LargeImg='" + file1LargeName + "' where InventoryImgMasterID='" + dsfind.Rows[0]["InventoryImgMasterID"].ToString() + "'";
                           
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
                            SqlCommand cmdup = new SqlCommand("Update InventoryImgMaster set LargeImg='" + file1LargeName + "' where InventoryImgMasterID='" + dsfind.Rows[0]["InventoryImgMasterID"].ToString() + "'", con);
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
                        Label1.Text = "Invalid File Type. Please upload an image file in one of the following formats: bmp, gif, png, jpg, jpeg, JPG, JPEG";
                    }
                }
            }
        }
        else
        {
           // if (fpdsm.PostedFile != null && fpdlrg.PostedFile != null)
           // {
                if (imgsmall.ImageUrl != "" || imglarge.ImageUrl != "")
                {

                    string fileSmallName = Session["comid"] + "/Thumbnail/" + ViewState["fileSmallName"];
                    string file1LargeName = Session["comid"] + "/LargeImg/" + ViewState["file1LargeName"];


                    
                    //bool valid = ext(fpdsm.FileName);
                    //if (valid == true)
                    //{
                      //  bool valid1 = ext(fpdlrg.FileName);
                       // if (valid1 == true)
                       // {
                            string insertstrinImgDe = "Insert into InventoryImgMaster(InventoryMasterId,Thumbnail,LargeImg,EntryDate) values('" + ViewState["InvMid"].ToString() + "', " +
                                 "  '" + fileSmallName + "','" + file1LargeName + "','" + System.DateTime.Now.ToString("MM/dd/yyyy") + "')";
                            SqlCommand cmdint = new SqlCommand(insertstrinImgDe, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmdint.ExecuteNonQuery();
                            con.Close();
                           // fpdsm.PostedFile.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\Thumbnail\\") + fpdsm.FileName);
                          //  fpdlrg.PostedFile.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\LargeImg\\") + fpdlrg.FileName);

                       // }
                       // else
                      //  {
                       //     Label1.Visible = true;
                       //     Label1.Text = "This is not Valid File";
                       // }
                   // }
                   // else
                   // {
                   //     Label1.Visible = true;
                   //     Label1.Text = "This is not Valid File";
                   // }

                    
                    
                }
                //if (fpdlrg.HasFile != null)
                //{
                //    string insertstrinImgDe = "Insert into InventoryImgMasterDetails values('" + ViewState["InvMid"].ToString() + "', " +
                //                 "  '" + fpdsm.FileName + "','" + fpdlrg.FileName + "','" + id + "')";
                //    SqlCommand cmdint = new SqlCommand(insertstrinImgDe, con);
                //    con.Open();
                //    cmdint.ExecuteNonQuery();
                //    con.Close();

                //}
                
            }
       // }
        
        GridView1.EditIndex = -1;

        //GridView1.DataSource = dstable;
        //GridView1.DataBind();
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
 
    protected void FillGridView1()
    {
        try
        {

            if (DropDownList1.SelectedIndex > 0)
            {
                string strg = " SELECT     InventoryMaster.InventoryMasterId, InventoryMaster.Name, " +
                    " InventoryImgMaster.InventoryImgMasterID, InventoryImgMaster.Thumbnail, " +
                    "  InventoryImgMaster.LargeImg, InventoryImgMaster.EntryDate " +
                     " FROM         InventoryImgMaster LEFT OUTER JOIN " +
                     " InventoryMaster ON InventoryImgMaster.InventoryMasterId = InventoryMaster.InventoryMasterId " +
                " where InventoryMaster.InventoryMasterId='" + DropDownList1.SelectedValue + "' ";
                SqlCommand cmdg = new SqlCommand(strg, con);
                SqlDataAdapter adpg = new SqlDataAdapter(cmdg);
                DataTable dtg = new DataTable();
                adpg.Fill(dtg);

                if (dtg.Rows.Count > 0)
                {
                    if (dtg.Rows[0]["InventoryMasterId"].ToString() != "")
                    {
                        GridView1.DataSource = dtg;
                        GridView1.DataBind();


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
                            lrgImg.ImageUrl = "~/Account/" +  dtg.Rows[0]["LargeImg"].ToString();
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
                        Panel2.Visible = false;

                    }

                    else
                    {

                        GridView1.DataSource = null;
                        GridView1.DataBind();


                        
                    }


                }
                else
                {

                    GridView1.DataSource = null;
                    GridView1.DataBind();

                    Panel2.Visible = true;
                   

                }
                lblbusiness.Text = ddlWarehouse.SelectedItem.Text;
                if (DropDownList1.SelectedIndex > 0)
                {
                    lblitemname.Text = DropDownList1.SelectedItem.Text;
                    lblitemdd.Visible = true;
                }
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }
        catch (Exception ty)
        {
            Label1.Visible = true;
            Label1.Text = "Error " + ty.Message;

        }
    }


 
    protected void ddlWarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        DropDownList1.DataSource = (DataSet)fillddl();
        DropDownList1.DataTextField = "Name";
        DropDownList1.DataValueField = "InventoryMasterId";
        DropDownList1.DataBind();
        DropDownList1.Items.Insert(0, "-Select-");
        DropDownList1.Items[0].Value = "0";
        

    }
   
    protected void imgBtnSearchGo_Click(object sender, EventArgs e)
    {

        GridView1.EditIndex = -1;

        FillGridView1();
     
        
    }
    protected void btncancel0_Click(object sender, EventArgs e)
    {
        if (btncancel0.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");

            btncancel0.Text = "Hide Printable Version";
            Button2.Visible = true;
            if (GridView1.Columns[2].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[2].Visible = false;
            }
            if (GridView1.Columns[3].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                GridView1.Columns[3].Visible = false;
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
                GridView1.Columns[2].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                GridView1.Columns[3].Visible = true;
            }

        }
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
       // pnladd.Visible = true;
       // btnadd.Visible = false;
    }
    protected void btnmassentry_Click(object sender, EventArgs e)
    {
        string te = "TransferImages.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void ddlimageavaibility_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlWarehouse_SelectedIndexChanged(sender, e);

    }

    protected void storefill()
    {
        DataTable dtwh = ClsStore.SelectStorename();
        ddlWarehouse.DataSource = dtwh;
        ddlWarehouse.DataTextField = "Name";
        ddlWarehouse.DataValueField = "WareHouseId";
        ddlWarehouse.DataBind();
        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();
        if (dteeed.Rows.Count > 0)
        {
            ddlWarehouse.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }
    }
   
}
