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

public partial class ShoppingCart_Admin_InventoryMediaMaster : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString"].ConnectionString);
    int i;

    int k;
    protected void Page_Load(object sender, EventArgs e)
    {
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
       // Session["pnl1"] = "8";
       // Session["pagename"] = "InventoryMediaMaster.aspx";
        Label1.Visible = false;

        if (!IsPostBack)
        {
            Pagecontrol.dypcontrol(Page, page);
            string strwh = "SELECT WareHouseId,Name,Address,CurrencyId FROM WareHouseMaster where comid='" + Session["comid"] + "' and Status='" + 1 + "' order by name";
            SqlCommand cmdwh = new SqlCommand(strwh, con);
            SqlDataAdapter adpwh = new SqlDataAdapter(cmdwh);
            DataTable dtwh = new DataTable();
            adpwh.Fill(dtwh);

            ddlwarehouse.DataSource = dtwh;
            ddlwarehouse.DataTextField = "Name";
            ddlwarehouse.DataValueField = "WareHouseId";
            ddlwarehouse.DataBind();

            ddlwarehouse.Items.Insert(0, "Select");
            ddlwarehouse.Items[0].Value = "0";
            ddlwarehouse_SelectedIndexChanged(sender, e);
            DropDownList2.DataSource=(DataSet)fillMediaType();
            DropDownList2.DataTextField="MediaFileType";
            DropDownList2.DataValueField="MediaFileTypeID";
            DropDownList2.DataBind();
            DropDownList2.Items.Insert(0,"Select");
            DropDownList2.Items[0].Value = "0";
        }

    }

    public DataSet fillddl()
    {
        SqlCommand cmd = new SqlCommand("SELECT Distinct InventoryMaster.InventoryMasterId,InventoryMaster.Name   FROM InventoryCategoryMaster inner join InventorySubCategoryMaster on InventorySubCategoryMaster.InventoryCategoryMasterId=InventoryCategoryMaster.InventeroyCatId inner join InventoruSubSubCategory on InventoruSubSubCategory.InventorySubCatID=InventorySubCategoryMaster.InventorySubCatId inner join InventoryMaster on InventoryMaster.InventorySubSubId=InventoruSubSubCategory.InventorySubSubId inner join InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.InventoryMasterId=InventoryMaster.InventoryMasterId inner join WareHouseMaster on WareHouseMaster.WareHouseId=InventoryWarehouseMasterTbl.WareHouseId  WHERE InventoryWarehouseMasterTbl.WareHouseId ='" + ddlwarehouse.SelectedValue + "' order by Name", con);
        cmd.CommandType = CommandType.Text;
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;

    }
    public DataSet fillMediaType()
    {
        SqlCommand cmd=new SqlCommand("SELECT     MediaFileTypeID, MediaFileType FROM   MediaFileType",con);
        SqlDataAdapter adp=new SqlDataAdapter(cmd);
        DataSet ds=new DataSet();
        adp.Fill(ds);
        return ds;
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
    protected void CustomValidator2_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (DropDownList2.SelectedIndex == 0)
        {
            i = 1;
            args.IsValid = false;


        }
        else
        {
            i = 0;
        }
    }

    public void insert(int inventoryid, string title, string filename, int typeid, string desc, DateTime date)
    {
        if (k > 10)
        {
            Label1.Visible = true;
            Label1.Text = "Please upload file Less than of size 10 MB";
        }
        if (k < 10)
        {
            string str = "insert into InventoryMediaMaster(InventoryMasterId,MediaTitle,FileName,MediaFileTypeID,MediaFileDesc,EntryDate) " +
                        " values('" + inventoryid + "','" + title + "','" + filename + "','" + typeid + "','" + desc + "','" + date + "')";

            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Label1.Visible = true;
            Label1.Text = "File uploaded successfully";
        }
    }
    protected void Button1_Click(object sender,EventArgs e)
    {
        string[] validFileTypes = { "mp3", "mp4", "wmv", "vlc", "mkv", "avi", "mpg", "flv" };

        string ext = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);

        bool isValidFile = false;

        for (int k = 0; k < validFileTypes.Length; k++)
        {

            if (ext == "." + validFileTypes[k])
            {

                isValidFile = true;

                break;

            }

        }

        if (!isValidFile)
        {

            Label1.Visible = true;

            Label1.Text = "Invalid File. Please upload a File with extension " + string.Join(",", validFileTypes);

        }

        else
        {


            if (i == 1)
            {


                return;

            }
            else
            {

                //SqlCommand cmd = new SqlCommand("SELECT InventoryMediaMasterID  FROM InventoryMediaMaster where InventoryMasterId='" + DropDownList1.SelectedValue + "' and MediaFileTypeID='"+DropDownList2.SelectedValue+"'", con);
                //cmd.CommandType = CommandType.StoredProcedure;
                //SqlDataAdapter adp = new SqlDataAdapter(cmd);
                //DataSet ds = new DataSet();
                //adp.Fill(ds);
                if (DropDownList2.SelectedIndex == 1)
                {
                    //DateTime dt = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());

                    if (FileUpload1.HasFile)
                    {
                        FileUpload1.PostedFile.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\audio\\") + FileUpload1.FileName);


                        // Jaydeep Tried for checking content type
                        //if (FileUpload1.PostedFile. == "mp3" || FileUpload1.PostedFile.ContentType == "wma")
                        //{
                        //    insert(Convert.ToInt32(DropDownList1.SelectedValue), TextBox1.Text, FileUpload1.FileName, Convert.ToInt32(DropDownList2.SelectedValue), txttaskinstruction.Text, System.DateTime.Now);
                        //}
                        //else
                        //{
                        //    Label1.Visible = true;
                        //    Label1.Text = "Please Insert Mp3 file..";
                        //}
                       int l = FileUpload1.PostedFile.ContentLength;
                        k = l / 1048576;
                        insert(Convert.ToInt32(DropDownList1.SelectedValue), TextBox1.Text, FileUpload1.FileName, Convert.ToInt32(DropDownList2.SelectedValue), txttaskinstruction.Text, System.DateTime.Now);
                        clear();
                    }
                    else
                    {
                        Label1.Visible = true;
                        Label1.Text = "File not found..";
                    }

                }
                else
                {
                    //DateTime dt = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());

                    if (FileUpload1.HasFile)
                    {
                        FileUpload1.PostedFile.SaveAs(Server.MapPath("~\\Account\\" + Session["comid"] + "\\video\\") + FileUpload1.FileName);
                        int l = FileUpload1.PostedFile.ContentLength;
                        k = l / 1048576;
                        insert(Convert.ToInt32(DropDownList1.SelectedValue), TextBox1.Text, FileUpload1.FileName, Convert.ToInt32(DropDownList2.SelectedValue), txttaskinstruction.Text, System.DateTime.Now);
                        clear();
                    }
                    else
                    {
                        Label1.Visible = true;
                        Label1.Text = "File not found..";
                    }
                }
            }
        }
    }
    protected void clear()
    {
        ddlwarehouse.SelectedIndex = 0;
        DropDownList1.SelectedIndex = 0;
        DropDownList2.SelectedIndex = 0;
        txttaskinstruction.Text = "";
        TextBox1.Text = "";
      
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        clear();
        Label1.Text = "";
    }
    protected void ddlwarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList1.Items.Clear();
        if (ddlwarehouse.SelectedIndex > 0)
        {
            DropDownList1.DataSource = (DataSet)fillddl();
            DropDownList1.DataTextField = "Name";
            DropDownList1.DataValueField = "InventoryMasterId";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, "Select");
            DropDownList1.Items[0].Value = "0";
        }
        else
        {
            DropDownList1.Items.Insert(0, "Select");
            DropDownList1.Items[0].Value = "0";
        }
    }
   
}
