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

public partial class EditProduct : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        if (!IsPostBack)
        {
            Filldetail();
            //if (Session["Login"] != null)
            //{
            //    if (Session["Login"].ToString() == null)
            //    {
            //        Response.Redirect("Login.aspx");
            //    }
            //}
            //else
            //{
            //    Response.Redirect("Login.aspx");
            //}
         //  FillProduct ();
         //   FillddlClientname();
        }
    }
    protected void Filldetail()
    {
        
        string  i =  Request.QueryString["productDetailId"].ToString() ; //)   Convert.ToInt32(GridView1.DataKeys[GridView1.SelectedIndex].Value.ToString()); //.SelectedDataKey.Value);
        //string strcln = "SELECT ProductMaster.*, ProductDetail.* " +
        //            " FROM   ProductDetail RIGHT OUTER JOIN " +
        //            " ProductMaster ON ProductDetail.ProductId = ProductMaster.ProductId " +
        //            " where   ProductId= " + ddlProductList.SelectedItem.Value.ToString();
        string strcln = "SELECT     ProductMaster.ProductId, ProductMaster.ClientMasterId, ProductMaster.ProductName, ProductMaster.ProductURL, ProductMaster.PricePlanURL, " +
                 " ProductDetail.ProductDetailId, ProductDetail.ProductId AS Expr1, ProductDetail.VersionNo, ProductDetail.Active, ProductDetail.Startdate, ProductDetail.EndDate, " +
                 " ProductDetail.ProductSetup, ProductDetail.ProductDb, ProductDetail.ProductExtra " +
                 " FROM         ProductDetail RIGHT OUTER JOIN ProductMaster ON ProductDetail.ProductId = ProductMaster.ProductId" +
                 " where   ProductDetail.ProductDetailId= " + i.ToString();
        hdnProductDetailId.Value = i.ToString();

        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        if (dtcln.Rows.Count > 0)
        {
            hdnProductId.Value = dtcln.Rows[0]["ProductId"].ToString();
            txtPricePlanURL.Text = dtcln.Rows[0]["PricePlanURL"].ToString();
        txtProductName.Text   = dtcln.Rows[0]["ProductName"].ToString();
            txtUrl.Text = dtcln.Rows[0]["ProductURL"].ToString();
            txtStartdate.Text = Convert.ToDateTime(dtcln.Rows[0]["Startdate"].ToString()).ToShortDateString();
            txtEndDate.Text = Convert.ToDateTime(dtcln.Rows[0]["EndDate"].ToString()).ToShortDateString();
            txtVersionNo.Text = dtcln.Rows[0]["VersionNo"].ToString();
            txtUrl.Enabled = true;
          //  txtProductName.Enabled = true;
            txtPricePlanURL.Enabled = true;
            btnSubmit.Text = "Update";
        }
        // Response.Redirect("Company
    }
    //protected void FillProduct()
    //{
    //    string strcln = " SELECT * from  ProductMaster where ClientMasterId= " + Session["ClientId"].ToString();
    //    SqlCommand cmdcln = new SqlCommand(strcln, con);
    //    DataTable dtcln = new DataTable();
    //    SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
    //    adpcln.Fill(dtcln);
    //    ddlProductList.DataSource  = dtcln;
    //    ddlProductList.DataBind();
    //    ddlProductList.Items.Insert(0, "-Select-");
    //    ddlProductList.Items[0].Value = "0";
    //    ddlProductList.SelectedIndex = 0;
    //}

    //protected void FillGrid()
    //{
    //    string strcln = " SELECT ProductMaster.*, ProductDetail.* " +
    //                    " FROM   ProductDetail RIGHT OUTER JOIN " +
    //                    " ProductMaster ON ProductDetail.ProductId = ProductMaster.ProductId " +
    //                    " where ClientMasterId= " + Session["ClientId"].ToString();
         
    //    SqlCommand cmdcln = new SqlCommand(strcln, con);
    //    DataTable dtcln = new DataTable();
    //    SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
    //    adpcln.Fill(dtcln);
    //    GridView1.DataSource = dtcln;
    //    GridView1.DataBind();
    //}
    protected void FillddlClientname()
    {
        string strcln = "SELECT ClientMasterId,CompanyName FROM ClientMaster";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);

        //ddlClientname.DataSource = dtcln;
        //ddlClientname.DataTextField = "CompanyName";
        //ddlClientname.DataValueField = "ClientMasterId";
        //ddlClientname.DataBind();

        //ddlClientname.Items.Insert(0, "-Select-");
        //ddlClientname.Items[0].Value = "0";



 
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSubmit.Text == "Update")
            {
                string productSetup = "";
                string productdb = "";
                string productextra = "";
                SqlCommand cmdcln;
                DataTable dtcln;
                SqlDataAdapter adpcln;
                string strcln;
                SqlCommand cmd;
                string str;

                str = "update   ProductMaster set ProductName='" + txtProductName.Text + "',ProductURL='" + txtUrl.Text + "', PricePlanURL='" + txtPricePlanURL.Text + "' where ProductId=" + hdnProductId.Value.ToString();

                cmd = new SqlCommand(str, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                lblmsg.Visible = true;

                string thisDir = "";
                string thisDirMainsetup = "";
                //  thisDir = "W:\\websites\\members.busiwiz.com\\Clients\\" + Session["ClientId"].ToString() + ""; // Server.MapPath(".");
                thisDir = "W:\\websites\\members.busiwiz.com\\Clients\\" + Session["ClientId"].ToString() + ""; // Server.MapPath(".");


                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(thisDir);
                if (!di.Exists)
                {
                    di.Create();
                }
                thisDirMainsetup = "W:\\websites\\members.busiwiz.com\\Clients\\" + Session["ClientId"].ToString() + "\\MainSetup"; // Server.MapPath(".");
                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(thisDirMainsetup);
                if (!dir.Exists)
                {
                    dir.Create();
                }

                productSetup = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + "_" + fuploadProjectSetup.FileName;
                productdb = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + "_" + FuploadDbFile.FileName;
                if (FUploadExtra.HasFile == true)
                {
                    productextra = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + "_" + FUploadExtra.FileName;
                }
                //fileuploadocurl.PostedFile.SaveAs(Server.MapPath("..\\Account\\UploadedDocuments\\") + filename);
                //fileuploadocurl.PostedFile.SaveAs(Server.MapPath("..\\Account\\DocumentImageTemp\\") + filename);
                String Dest = "";
                thisDir = "";
                //  Dest = "W:\\websites\\members.busiwiz.com\\Clients\\" + Session["ClientId"].ToString() + "\\Database.zip";
                // thisDir = "W:\\websites\\members.busiwiz.com\\Clients\\" + Session["ClientId"].ToString() + "\\MainSetup\\Database.zip";
                // System.IO.File.Copy(thisDir, Dest);
                //
                string strcln1 = "SELECT     ProductMaster.ProductId, ProductMaster.ClientMasterId, ProductMaster.ProductName, ProductMaster.ProductURL, ProductMaster.PricePlanURL, " +
                    " ProductDetail.ProductDetailId, ProductDetail.ProductId AS Expr1, ProductDetail.VersionNo, ProductDetail.Active, ProductDetail.Startdate, ProductDetail.EndDate, " +
                    " ProductDetail.ProductSetup, ProductDetail.ProductDb, ProductDetail.ProductExtra " +
                    " FROM         ProductDetail RIGHT OUTER JOIN ProductMaster ON ProductDetail.ProductId = ProductMaster.ProductId" +
                    " where   ProductDetail.ProductDetailId= " + hdnProductDetailId.Value.ToString();
                //hdnProductDetailId.Value = i.ToString();

                SqlCommand cmdcln1 = new SqlCommand(strcln1, con);
                DataTable dtcln1 = new DataTable();
                SqlDataAdapter adpcln1 = new SqlDataAdapter(cmdcln1);
                adpcln1.Fill(dtcln1);
                if (dtcln1.Rows.Count > 0)
                {
                    if (fuploadProjectSetup.HasFile == true)
                    {
                        fuploadProjectSetup.SaveAs("W:\\websites\\members.busiwiz.com\\Clients\\" + Session["ClientId"].ToString() + "\\MainSetup\\" + productSetup);
                    }
                    else
                    {
                        //        ,='" + productSetup + "', ProductDb='" + productdb + "', ProductExtra='" + productextra + "') " +
                        productSetup = dtcln1.Rows[0]["ProductSetup"].ToString();

                    }
                    if (FuploadDbFile.HasFile == true)
                    {
                        FuploadDbFile.SaveAs("W:\\websites\\members.busiwiz.com\\Clients\\" + Session["ClientId"].ToString() + "\\MainSetup\\" + productdb);
                    }
                    else
                    {

                        productdb = dtcln1.Rows[0]["productdb"].ToString();
                    }
                    if (FUploadExtra.HasFile == true)
                    {
                        FUploadExtra.SaveAs("W:\\websites\\members.busiwiz.com\\Clients\\" + Session["ClientId"].ToString() + "\\MainSetup\\" + productextra);
                    }
                    else
                    {

                        productextra = dtcln1.Rows[0]["productextra"].ToString();
                    }
                    //

                    str = "update   ProductDetail set  VersionNo ='" + txtVersionNo.Text + "', Active= '" + chkboxActiveDeactive.Checked.ToString() + "' ,startDate='" + txtStartdate.Text + "', EndDate='" + txtEndDate.Text + "',ProductSetup='" + productSetup + "', ProductDb='" + productdb + "', ProductExtra='" + productextra + "' where ProductDetailId=" + hdnProductDetailId.Value.ToString();

                    cmd = new SqlCommand(str, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    lblmsg.Visible = true;
                    lblmsg.Text = "Record Updated successfully.";
                    Response.Redirect("ClientProductList.aspx");
                }
                // 
            }
        }
        catch (Exception eerr)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Error : " + eerr.Message;

        }



    }
    //protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    if (e.CommandName == "edit1")
    //    {
    //        GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
    //        int i = Convert.ToInt32(GridView1.DataKeys[GridView1.SelectedIndex].Value.ToString()) ; //.SelectedDataKey.Value);
    //        //string strcln = "SELECT ProductMaster.*, ProductDetail.* " +
    //        //            " FROM   ProductDetail RIGHT OUTER JOIN " +
    //        //            " ProductMaster ON ProductDetail.ProductId = ProductMaster.ProductId " +
    //        //            " where   ProductId= " + ddlProductList.SelectedItem.Value.ToString();
    //        string strcln = "SELECT     ProductMaster.ProductId, ProductMaster.ClientMasterId, ProductMaster.ProductName, ProductMaster.ProductURL, ProductMaster.PricePlanURL, " + 
    //                 " ProductDetail.ProductDetailId, ProductDetail.ProductId AS Expr1, ProductDetail.VersionNo, ProductDetail.Active, ProductDetail.Startdate, ProductDetail.EndDate, " +  
    //                 " ProductDetail.ProductSetup, ProductDetail.ProductDb, ProductDetail.ProductExtra " + 
    //                 " FROM         ProductDetail RIGHT OUTER JOIN ProductMaster ON ProductDetail.ProductId = ProductMaster.ProductId" +
    //                 " where   ProductDetail.ProductDetailId= " + i.ToString();
    //        hdnProductDetailId.Value = i.ToString();
         
    //        SqlCommand cmdcln = new SqlCommand(strcln, con);
    //        DataTable dtcln = new DataTable();
    //        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
    //        adpcln.Fill(dtcln);
    //        if (dtcln.Rows.Count > 0)
    //        {
    //            hdnProductId.Value = dtcln.Rows[0]["ProductId"].ToString();
    //            txtPricePlanURL.Text = dtcln.Rows[0]["PricePlanURL"].ToString();            
    //            txtProductName.Text = dtcln.Rows[0]["ProductName"].ToString();
    //            txtUrl.Text = dtcln.Rows[0]["ProductURL"].ToString();
    //            txtStartdate.Text = Convert.ToDateTime( dtcln.Rows[0]["Startdate"].ToString()).ToShortDateString();
    //            txtEndDate.Text = Convert.ToDateTime(dtcln.Rows[0]["EndDate"].ToString()).ToShortDateString();
    //            txtVersionNo.Text = dtcln.Rows[0]["VersionNo"].ToString();
    //            txtUrl.Enabled = true;
    //            txtProductName.Enabled = true;
    //            txtPricePlanURL.Enabled = true;
    //            btnSubmit.Text = "Update";
    //        }
    //       // Response.Redirect("CompanySetupEdit.aspx?id=" + i + "");
    //    }
    //}
    //protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (RadioButtonList1.SelectedIndex == 0)
    //    {
    //           pnlProduct.Visible = false;
    //           btnSubmit.Text = "Submit";
    //           txtUrl.Enabled = true;
    //           txtProductName.Enabled = true;
    //           txtPricePlanURL.Enabled = true;
    //    }
    //    else
    //    {
    //           pnlProduct.Visible = true;
    //           btnSubmit.Text = "Add";
    //           txtUrl.Enabled = false;
    //           txtProductName.Enabled = false;
    //           txtPricePlanURL.Enabled = false;
    //    }
    //}
    //protected void ddlProductList_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlProductList.SelectedIndex > 0)
    //    {
    //        btnSubmit.Text = "Add";
    //        string strcln = "SELECT     ProductMaster.ProductId, ProductMaster.ClientMasterId, ProductMaster.ProductName, ProductMaster.ProductURL, ProductMaster.PricePlanURL, " +
    //                  " ProductDetail.ProductDetailId, ProductDetail.ProductId AS Expr1, ProductDetail.VersionNo, ProductDetail.Active, ProductDetail.Startdate, ProductDetail.EndDate, " +
    //                  " ProductDetail.ProductSetup, ProductDetail.ProductDb, ProductDetail.ProductExtra " +
    //                  " FROM         ProductDetail RIGHT OUTER JOIN ProductMaster ON ProductDetail.ProductId = ProductMaster.ProductId" +
    //                  " where   ProductMaster.ProductId= " + ddlProductList.SelectedItem.Value.ToString(); SqlCommand cmdcln = new SqlCommand(strcln, con);
    //        DataTable dtcln = new DataTable();
    //        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
    //        adpcln.Fill(dtcln);
    //        if (dtcln.Rows.Count > 0)
    //        {                
    //            txtUrl.Enabled = false;
    //            txtProductName.Enabled = false;
    //            txtPricePlanURL.Enabled = false;
    //            txtPricePlanURL.Text = dtcln.Rows[0]["PricePlanURL"].ToString();
    //            txtProductName.Text = dtcln.Rows[0]["ProductName"].ToString();
    //            txtUrl.Text = dtcln.Rows[0]["ProductURL"].ToString();
    //        }
    //    }
    //}
}
