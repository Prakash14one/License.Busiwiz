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

public partial class ShoppingCart_Admin_TimeKeeperCompanyLogo : System.Web.UI.Page
{
    SqlConnection con;
    CompanyWizard clsCompany = new CompanyWizard();

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
            //LoadData();
            fillgrid();
        }

    }
    protected void LoadData()
    {
        DataTable dt = new DataTable();
        string cmpid = Session["Comid"].ToString();
        dt = clsCompany.SelectCompanyInfo(cmpid);
        if (dt.Rows.Count > 0)
        {
            ViewState["comid"] = dt.Rows[0]["CompanyId"].ToString();
            imgLogo.ImageUrl = "~/ShoppingCart/images/" + dt.Rows[0]["CompanyLogo"].ToString();
        }
        else
        {
            Panel1.Visible = true;
            pnllogo.Visible = false;
        }

    }

    protected void btnChange_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        lblmsg.Text = "";
    }
    public bool ext(string filename)
    {
        string[] validFileTypes = { "bmp", "gif", "png", "jpg", "jpeg", "ipg", "BMP", "GIF", "PNG", "JPG", "JPEG", "IPG","JFIF","jfif","TIFF","tiff","WEBP","webp" };

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
    protected void imgBtnImageUpdate_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {

            bool valid = ext(FileUpload1.FileName);
            if (valid == true)
            {
                FileUpload1.PostedFile.SaveAs(Server.MapPath("~\\ShoppingCart\\images\\") + FileUpload1.FileName);
                string logoname = FileUpload1.FileName.ToString();


                //LoadData();
                //************CHNAGES codes
                con.Close();
                con.Open();

                //*************Radhika Chnages
                //  string updatelogoname = "update CompanyMaster set CompanyLogo='" + logoname + "' where CompanyId='" + ViewState["comid"].ToString() + "' ";
                //*************************
                //string updatelogoname = "update CompanyMaster set CompanyLogo='" + logoname + "' where CompanyId='" + ViewState["comid"].ToString() + "' and Compid='" + Session["Comid"].ToString() + "' ";
                string updatelogoname = "update CompanyWebsitMaster set LogoUrl='" + logoname + "' where whid='" + ViewState["IMG"] + "'";

                SqlCommand cmdlogo = new SqlCommand(updatelogoname, con);
                cmdlogo.ExecuteNonQuery();
                con.Close();


                //*************


                imgLogo.ImageUrl = "~/ShoppingCart/images/" + FileUpload1.FileName.ToString();
                pnllogo.Visible = true;
                Panel1.Visible = false;
                lblmsg.Text = "Record updated successfully";

                fillgrid();

                lblLogo.Visible = false;
                imgLogo.Visible = false;
                btnChange.Visible = false;
                Label2.Visible = false;
            }
            else
            {
                lblmsg.Text = "Invalid File Type. Please upload an image file in one of the following formats: bmp, gif, png, jpg, jpeg, ipg, jfif, tiff, webp";
            }

        }

    }

    protected void ImgBtncancel_Click(object sender, EventArgs e)
    {
        pnllogo.Visible = true;
        Panel1.Visible = false;
        lblmsg.Text = "";
    }

    protected void fillgrid()
    {
        string str = "select WareHouseMaster.Name,CompanyWebsitMaster.whid,CompanyWebsitMaster.LogoUrl from WareHouseMaster inner join CompanyWebsitMaster on CompanyWebsitMaster.WHId=WareHouseMaster.WareHouseId where WareHouseMaster.comid='" + Session["Comid"] + "' and WareHouseMaster.status='1'";

        SqlDataAdapter da = new SqlDataAdapter(str, con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Button change = (Button)e.Row.FindControl("change");
            Image Image1 = (Image)e.Row.FindControl("Image1");

            if (Image1.ImageUrl != "")
            {
                Image1.ImageUrl = "~/ShoppingCart/images/" + Image1.ImageUrl;
                change.Text = "Change";
            }
            else
            {
                change.Text = "Add New";
            }
            //Label change = (Label)e.Row.FindControl("change");
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            int i = Convert.ToInt32(e.CommandArgument);
            lblmsg.Text = "";
            ViewState["IMG"] = i;

            SqlDataAdapter daf = new SqlDataAdapter("select CompanyWebsitMaster.LogoUrl,warehousemaster.name from CompanyWebsitMaster inner join warehousemaster on warehousemaster.warehouseid=CompanyWebsitMaster.whid where CompanyWebsitMaster.whid='" + i + "'", con);
            DataTable dt = new DataTable();
            daf.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                if (Convert.ToString(dt.Rows[0]["LogoUrl"]) != "")
                {
                    lblLogo.Visible = true;
                    lblLogo.Text = "Existing Logo of Business - ";
                    btnChange.Visible = true;
                    btnChange.Text = "Change Logo";
                    pnllogo.Visible = true;
                    Label2.Visible = true;
                    Label2.Text = dt.Rows[0]["name"].ToString();
                    imgLogo.Visible = true;
                    imgLogo.ImageUrl = "~/ShoppingCart/images/" + dt.Rows[0]["LogoUrl"].ToString();
                }
                else
                {
                    lblLogo.Visible = true;
                    lblLogo.Text = "Set a Logo for your Business - ";
                    btnChange.Visible = true;
                    btnChange.Text = "Add New Logo";
                    imgLogo.Visible = false;
                    Label2.Visible = true;
                    Label2.Text = dt.Rows[0]["name"].ToString();
                }
            }
        }
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
}

