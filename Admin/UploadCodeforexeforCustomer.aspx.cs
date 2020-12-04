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
public partial class UploadCodeforexeforCustomer : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        if (!Page.IsPostBack)
        {
            FillClient();
            FillProduct();
            FillGrid();
        }
    }
    protected void FillClient()
    {
        string str = " SELECT   * from   ClientMaster ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        ddlClientList.DataSource = dt;
        ddlClientList.DataBind();
        ddlClientList.Items.Insert(0, "- All -");
        ddlClientList.Items[0].Value = "0";
        ddlClientList.SelectedIndex = 0;
    }
    protected void FillProduct()
    {
        string strcln = "SELECT *  FROM ProductMaster  where ClientMasterId=" + ddlClientList.SelectedItem.Value.ToString();
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        ddlProductname.DataSource = dtcln;
        ddlProductname.DataBind();
        ddlProductname.Items.Insert(0, "-Select-");
        ddlProductname.Items[0].Value = "0";
    }
    //protected void FillGrid()
    //{
    //    string strcln = "SELECT     PricePlanMaster.PricePlanId AS Expr6, PricePlanMaster.PricePlanName, PricePlanMaster.PricePlanDesc, PricePlanMaster.active AS Expr2, " + 
    //                    " PricePlanMaster.StartDate AS Expr3, PricePlanMaster.EndDate AS Expr4, PricePlanMaster.PricePlanAmount, PricePlanMaster.ProductId AS Expr5, " + 
    //                    " PricePlanMaster.DurationMonth, PricePlanMaster.AllowIPTrack, PricePlanMaster.GBUsage, PricePlanMaster.MaxUser, PricePlanMaster.TrafficinGB, " +
    //                    " PricePlanMaster.TotalMail, CASE WHEN PricePlanMaster.AllowIPTrack IS NULL THEN 'No' ELSE 'Yes' END AS GBUSage1, " + 
    //                    " ProductMaster.ProductId AS Expr1, ProductMaster.ClientMasterId, ProductMaster.ProductName, ProductMaster.ProductURL, " + 
    //                    " ProductMaster.PricePlanURL, ProductDetail.ProductDetailId, ProductDetail.ProductId, ProductDetail.VersionNo, ProductDetail.Active,  " + 
    //                    " ProductDetail.Startdate, ProductDetail.EndDate, SetupMaster.SetupId, SetupMaster.PricePlanId, SetupMaster.ProductDetailId AS Expr7, " +
    //                    " SetupMaster.ProductSetup, SetupMaster.ProductDB, SetupMaster.ProductExtra " +
    //                    "  FROM         PricePlanMaster RIGHT OUTER JOIN " +
    //                    " SetupMaster ON PricePlanMaster.PricePlanId = SetupMaster.PricePlanId RIGHT OUTER JOIN " + 
    //                    " ProductMaster ON PricePlanMaster.ProductId = ProductMaster.ProductId FULL OUTER JOIN " + 
    //                    " ProductDetail ON ProductMaster.ProductId = ProductDetail.ProductId AND SetupMaster.ProductDetailId = ProductDetail.ProductDetailId "  +
    //                    " where ClientMasterId= " + Session["ClientId"].ToString() + " AND (SetupMaster.ProductSetup IS NOT NULL)";
    //        //if (ddlPricePlanName.SelectedIndex > 0)
    //        //{
    //        //strcln = strcln + " and SetupMaster.PricePlanId= '"+ ddlPricePlanName.SelectedItem.Value.ToString() +"'"  ;
    //        //}

    //        //if (ddlVersion.SelectedIndex > 0)
    //        //{
    //        //strcln = strcln + " and ProductDetail.ProductId= '"+ ddlVersion.SelectedItem.Value.ToString() +"'"  ;
    //        //}

    //        //if ( ddlProductname.SelectedIndex > 0)
    //        //{
    //        //strcln = strcln + " and SetupMaster.PricePlanId= '"+ ddlPricePlanName.SelectedItem.Value.ToString() +"'"  ;
    //        //}

         
    //    if (ddlProductname.SelectedIndex > 0)
    //    { 
    //        strcln = strcln + "and ProductMaster.ProductId= " + ddlProductname.SelectedItem.Value.ToString() ; //+
    //    }
    //    if (ddlVersion.SelectedIndex > 0)
    //    {
    //        strcln = strcln + "and SetupMaster.ProductDetailId= " + ddlVersion.SelectedItem.Value.ToString(); //+
    //    }

    //    if ( ddlPricePlanName.SelectedIndex > 0)
    //    {
    //        strcln = strcln + "and SetupMaster.PricePlanId= " + ddlPricePlanName.SelectedItem.Value.ToString(); //+
    //    }

    //    SqlCommand cmdcln = new SqlCommand(strcln, con);
    //    DataTable dtcln = new DataTable();
    //    SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
    //    adpcln.Fill(dtcln);
    //    GridView1.DataSource = dtcln;
    //    GridView1.DataBind();
    //}
    protected void FillGrid()
    {
        //string strcln = " SELECT ProductMaster.*, ProductDetail.* " +
        //                " FROM   ProductDetail RIGHT OUTER JOIN " +
        //                " ProductMaster ON ProductDetail.ProductId = ProductMaster.ProductId " +
        //                " where ClientMasterId= " + Session["ClientId"].ToString();

        string strcln = "  SELECT     ProductDetailExe.ProductDetailExeId, ProductDetailExe.ProductDetailId, ProductDetailExe.Location, ProductDetailExe.UploadDate, " +
                     " ProductDetailExe.Status, ProductDetailExe.PricePlanId AS Expr4, ProductDetail.ProductDetailId AS Expr1, ProductDetail.ProductId AS Expr5, " +
                     " ProductDetail.VersionNo, ProductDetail.Active AS Expr6, ProductDetail.Startdate AS Expr7, ProductDetail.EndDate AS Expr8," +
                     " ProductDetail.ProductSetup, ProductDetail.ProductDb, ProductDetail.ProductExtra, ProductMaster.ProductId  , ProductMaster.ClientMasterId,  " +
                     " ProductMaster.ProductName, ProductMaster.ProductURL, ProductMaster.PricePlanURL, ClientMaster.ClientMasterId  ,  " +
                     " ClientMaster.CompanyName, ClientMaster.Address1, ClientMaster.Address2, ClientMaster.CountryId, ClientMaster.StateId, ClientMaster.City,  " +
                     " ClientMaster.Zipcode, ClientMaster.ContactPersonName, ClientMaster.Fax1, ClientMaster.Fax2, ClientMaster.Email1, ClientMaster.Email2, " +
                     " ClientMaster.Phone1, ClientMaster.Phone2, ClientMaster.ClientURL, ClientMaster.CustomerSupportURL, ClientMaster.SalesCustomerSupportURL, " +
                     " ClientMaster.SalesPhoneNo, ClientMaster.SalesFaxNo, ClientMaster.SalesEmail, ClientMaster.AfterSalesSupportPhoneNo,  " +
                     " ClientMaster.AfterSalesSupportFaxNo, ClientMaster.AfterSalesSupportEmail, ClientMaster.TechSupportPhoneNo, ClientMaster.TechSupportFaxNo,  " +
                     " ClientMaster.TechSupportEmail, ClientMaster.FTP, ClientMaster.FTPUserName, ClientMaster.FTPPassword, ClientMaster.LoginName, " +
                     " ClientMaster.LoginPassword, ClientMaster.ClientPricePlanID, PricePlanMaster.PricePlanName AS Expr9, PricePlanMaster.PricePlanAmount AS Expr10,  " +
                     " PricePlanMaster.* FROM         PricePlanMaster RIGHT OUTER JOIN " +
                     " ProductDetailExe ON PricePlanMaster.PricePlanId = ProductDetailExe.PricePlanId LEFT OUTER JOIN " +
                     " ProductMaster LEFT OUTER JOIN " +
                     " ClientMaster ON ProductMaster.ClientMasterId = ClientMaster.ClientMasterId RIGHT OUTER JOIN " +
                     " ProductDetail ON ProductMaster.ProductId = ProductDetail.ProductId ON ProductDetailExe.ProductDetailId = ProductDetail.ProductDetailId " +
                      " WHERE     (ProductDetailExe.Status IS NULL) OR  (ProductDetailExe.Status = 0) ";
                    strcln = strcln + "   and ClientMaster.ClientMasterId = '" + Session["ClientId"].ToString() + "'";
        if( ddlProductname.SelectedIndex > 0)
        {
        strcln = strcln + "   and ProductMaster.ProductId = '" + ddlProductname.SelectedItem.Value.ToString() + "'";
        }

        if(ddlVersion.SelectedIndex > 0)
        {
           strcln = strcln + "   and ProductDetailExe.ProductDetailId = '" + ddlVersion.SelectedItem.Value.ToString() + "'";
        }
        if(ddlPricePlanName.SelectedIndex > 0)
        {
        strcln = strcln + "   and ProductDetailExe.PricePlanId = '" + ddlPricePlanName.SelectedItem.Value.ToString() + "'";
          }
       //  strcln = strcln + " and "
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        GridView1.DataSource = dtcln;
        GridView1.DataBind();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (ddlPricePlanName.SelectedIndex == 0)
        {
            if (ddlPricePlanName.Items.Count == 0)
            {
                lblmsg.Visible = true;
                lblmsg.Text = "You have to create atleast one price plan for this product. Go to Add/Update Price Plan page.";
                return;
            }
            else
            {
                lblmsg.Visible = true;
                lblmsg.Text = "You have to select atleast one price plan for this product.";
                return;
            
            }
        }

      //  if (btnSubmit.Text == "Update")
      //  {
      //      string SetupId = "";
      //   string     str = "SELECT     PricePlanMaster.PricePlanId, PricePlanMaster.PricePlanName, PricePlanMaster.PricePlanDesc, PricePlanMaster.active, PricePlanMaster.StartDate, " + 
      //                " PricePlanMaster.EndDate, PricePlanMaster.PricePlanAmount, PricePlanMaster.ProductId, PricePlanMaster.DurationMonth, " + 
      //                " PricePlanMaster.AllowIPTrack, PricePlanMaster.GBUsage, PricePlanMaster.MaxUser, PricePlanMaster.TrafficinGB, PricePlanMaster.TotalMail, " + 
      //                " SetupMaster.SetupId, SetupMaster.PricePlanId AS Expr1, SetupMaster.ProductDetailId, SetupMaster.ProductSetup, SetupMaster.ProductDB, " + 
      //                " SetupMaster.ProductExtra FROM         SetupMaster LEFT OUTER JOIN " +
      //                " PricePlanMaster ON SetupMaster.PricePlanId = PricePlanMaster.PricePlanId where PricePlanMaster.productId= " + ddlProductname.SelectedItem.Value.ToString() + "order by Setupid desc";
        

      //  SqlCommand cmd = new SqlCommand(str, con);
      //  SqlDataAdapter adp = new SqlDataAdapter(cmd);
      //  DataSet ds = new DataSet();
      //  adp.Fill(ds);
      //  if (ds.Tables[0].Rows.Count > 0)
      //  {
      //      SetupId = ds.Tables[0].Rows[0]["SetupId"].ToString();
      //  }
      //  string productId = "";
      //  productId = ddlProductname.SelectedItem.Value.ToString();
      ////  String SetupId = "";
      //  string productSetup = "";
      ////  string productdb = "";
      ////  string productextra = "";
      //  productSetup = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + "_" + fuploadProjectSetup.FileName;
      //  //productdb = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + "_" + FuploadDbFile.FileName;
      //  //if (FUploadExtra.HasFile == true)
      //  //{
      //  //    productextra = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + "_" + FUploadExtra.FileName;
      //  //}
      //  string thisDir = "";
      //  string thisDirMainsetup = "";
      //  string thisDirProduct = "";

      //  thisDir = "W:\\websites\\members.busiwiz.com\\Clients\\" + Session["ClientId"].ToString() + ""; // Server.MapPath(".");
      //  System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(thisDir);
      //  if (!di.Exists)
      //  {
      //      di.Create();
      //  }
      //  thisDirMainsetup = "W:\\websites\\members.busiwiz.com\\Clients\\" + Session["ClientId"].ToString() + "\\SetupExe"; // Server.MapPath(".");
      //  System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(thisDirMainsetup);
      //  if (!dir.Exists)
      //  {
      //      dir.Create();
      //  }

      //  thisDirProduct = "W:\\websites\\members.busiwiz.com\\Clients\\" + Session["ClientId"].ToString() + "\\SetupExe\\" + productId.ToString() + ""; // Server.MapPath(".");
      //  dir = new System.IO.DirectoryInfo(thisDirProduct);
      //  if (!dir.Exists)
      //  {
      //      dir.Create();
      //  }
      //  //thisDirProduct = "W:\\websites\\members.busiwiz.com\\Clients\\" + Session["ClientId"].ToString() + "\\MainSetup\\" + productId.ToString() + "\\" + SetupId.ToString(); // Server.MapPath(".");
      //  //dir = new System.IO.DirectoryInfo(thisDirProduct);
      //  //if (!dir.Exists)
      //  //{
      //  //    dir.Create();
      //  //}

      //  fuploadProjectSetup.SaveAs("W:\\websites\\members.busiwiz.com\\Clients\\" + Session["ClientId"].ToString() + "\\SetupExe\\" + productId.ToString() + "\\" + productSetup);
      //  //FuploadDbFile.SaveAs("W:\\websites\\members.busiwiz.com\\Clients\\" + Session["ClientId"].ToString() + "\\MainSetup\\" + productId.ToString() + "\\" + SetupId.ToString() + "\\" + productdb);
      //  //FUploadExtra.SaveAs("W:\\websites\\members.busiwiz.com\\Clients\\" + Session["ClientId"].ToString() + "\\MainSetup\\" + productId.ToString() + "\\" + SetupId.ToString() + "\\" + productextra);
      //  //
      //  //str = "update   SetupMaster set  PricePlanId='" + ddlPricePlanName.SelectedItem.Value.ToString() + "' ,ProductDetailId='" + ddlVersion.SelectedItem.Value.ToString() + "', ProductSetup = '" + productSetup + "' ,ProductDB='" + productdb + "',ProductExtra='" + productextra    + "'where setupId= '" +  SetupId.ToString() + "'";

      //  //  cmd = new SqlCommand(str, con);
      //  //con.Open();
      //  //cmd.ExecuteNonQuery();
      //  //con.Close();
      //  str = "update   ProductDetailExe set  Location ='" + productSetup + "' , UploadDate = '"+ DateTime.Now.ToShortDateString() +"' where ProductDetailId=" + hdnProductDetailId.Value.ToString();
      //  cmd = new SqlCommand(str, con);
      //  con.Open();
      //  cmd.ExecuteNonQuery();
      //  con.Close();
      //  //
      //  lblmsg.Visible = true;
      //  lblmsg.Text = "Record updated sucessfully";
      //  FillGrid();
      //  ClearAll();
      //  }
      //  else
      //  {

            try
            {                
                string productSetup = "";
              //  string productdb = "";
              //  string productextra = "";
                productSetup = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + "_" + fuploadProjectSetup.FileName;
              //  productdb = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + "_" + FuploadDbFile.FileName;
                //if (FUploadExtra.HasFile == true)
                //{
                //    productextra = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + "_" + FUploadExtra.FileName;
                //}
 //               string str = "INSERT INTO SetupMaster(PricePlanId,ProductDetailId,ProductSetup , ProductDB, ProductExtra) " +
 //                      "VALUES('" + ddlPricePlanName.SelectedItem.Value.ToString() + "','" + ddlVersion.SelectedItem.Value.ToString() + "','" + productSetup + "','" + productdb + "','" + productextra + "')";
 //               SqlCommand cmd = new SqlCommand(str, con);
 //               con.Open();
 //               cmd.ExecuteNonQuery();
 //               con.Close();
 //string productId = "";
 //               productId = ddlProductname.SelectedItem.Value.ToString();
 //               String SetupId = "";

 //               string strcln = "SELECT * FROM SetupMaster order by SetupId desc";
 //                   cmd = new SqlCommand(strcln, con);
 //                DataTable    dtcln = new DataTable();
 //               SqlDataAdapter     adpcln = new SqlDataAdapter(cmd);
 //                   adpcln.Fill(dtcln);
                
 //               if (dtcln.Rows.Count > 0)
 //               {
 //                   SetupId = dtcln.Rows[0]["SetupId"].ToString();
 //               }
                //
                string thisDir = "";
                string thisDirMainsetup = "";
                string thisDirProduct = "";
                string productId = "";
                productId = ddlProductname.SelectedItem.Value.ToString();
                string PricePlanId = "";
                PricePlanId = ddlPricePlanName.SelectedItem.Value.ToString();
                thisDir = "W:\\websites\\members.busiwiz.com\\Clients\\" + Session["ClientId"].ToString() + ""; // Server.MapPath(".");
                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(thisDir);
                if (!di.Exists)
                {
                    di.Create();
                }
                thisDirMainsetup = "W:\\websites\\members.busiwiz.com\\Clients\\" + Session["ClientId"].ToString() + "\\SetupExe"; // Server.MapPath(".");
                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(thisDirMainsetup);
                if (!dir.Exists)
                {
                    dir.Create();
                }

                thisDirProduct = "W:\\websites\\members.busiwiz.com\\Clients\\" + Session["ClientId"].ToString() + "\\SetupExe\\" + productId.ToString() + ""; // Server.MapPath(".");
                dir = new System.IO.DirectoryInfo(thisDirProduct);
                if (!dir.Exists)
                {
                    dir.Create();
                }
                //  thisDir = "W:\\websites\\members.busiwiz.com\\Clients\\" + Session["ClientId"].ToString() + ""; // Server.MapPath(".");
              //  thisDir = "W:\\websites\\members.busiwiz.com\\Clients\\" + Session["ClientId"].ToString() + ""; // Server.MapPath(".");
              //  System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(thisDir);
              //  if (!di.Exists)
              //  {
              //      di.Create();
              //  }
              //  thisDirMainsetup = "W:\\websites\\members.busiwiz.com\\Clients\\" + Session["ClientId"].ToString() + "\\MainSetup"; // Server.MapPath(".");
              //  System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(thisDirMainsetup);
              //  if (!dir.Exists)
              //  {
              //      dir.Create();
              //  }

              //  thisDirProduct = "W:\\websites\\members.busiwiz.com\\Clients\\" + Session["ClientId"].ToString() + "\\MainSetup\\" +   productId.ToString() + ""; // Server.MapPath(".");
              //dir = new System.IO.DirectoryInfo(thisDirProduct);
              //  if (!dir.Exists)
              //  {
              //      dir.Create();
              //  }
              //  thisDirProduct = "W:\\websites\\members.busiwiz.com\\Clients\\" + Session["ClientId"].ToString() + "\\MainSetup\\" + productId.ToString() + "\\" + SetupId.ToString(); // Server.MapPath(".");
              //  dir = new System.IO.DirectoryInfo(thisDirProduct);
              //  if (!dir.Exists)
              //  {
              //      dir.Create();
              //  }
              // PricePlanId = Convert.ToInt32(dtcln.Rows[0]["PricePlanId"].ToString());
              //
                fuploadProjectSetup.SaveAs("W:\\websites\\members.busiwiz.com\\Clients\\" + Session["ClientId"].ToString() + "\\SetupExe\\" + productId.ToString() + "\\" + productSetup);
              // fuploadProjectSetup.SaveAs("W:\\websites\\members.busiwiz.com\\Clients\\" + Session["ClientId"].ToString() + "\\MainSetup\\" + productId.ToString() + "\\" + SetupId.ToString()  + "\\" + productSetup);
              // FuploadDbFile.SaveAs("W:\\websites\\members.busiwiz.com\\Clients\\" + Session["ClientId"].ToString() + "\\MainSetup\\" + productId.ToString() + "\\" + SetupId.ToString()  + "\\" +  productdb);
              // FUploadExtra.SaveAs("W:\\websites\\members.busiwiz.com\\Clients\\" + Session["ClientId"].ToString() + "\\MainSetup\\" + productId.ToString() + "\\" + SetupId.ToString()  + "\\" +  productextra);
              //              
                //
                string str = "";
              str = " INSERT INTO ProductDetailExe(ProductDetailId,Location,UploadDate, PricePlanId) " +
                          " VALUES('" + hdnProductDetailId.Value.ToString() + "','" + productSetup + "','" + DateTime.Now.ToShortDateString() + "'," + Convert.ToInt32( PricePlanId.ToString()) + ")";
           SqlCommand    cmd = new SqlCommand(str, con);
              con.Open();
              cmd.ExecuteNonQuery();
              con.Close();
                lblmsg.Text = "Record Inserted Sucessfully";
                FillGrid();
                ClearAll();
            }
            catch (Exception eerr)
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Error : " + eerr.Message;
            }
         //   FillGrid();
        //}
    }
    protected void ClearAll()
    {
       
        ddlProductname.SelectedIndex = 0;
        btnSubmit.Text = "Submit";
        hdnProductId.Value = "";
        hdnProductDetailId.Value = "";
         
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete1")
        {
            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            int i = Convert.ToInt32(GridView1.SelectedDataKey.Value);
            string strcln = "Delete ProductDetailExe   where productDetailExeId=" + i.ToString(); // +

            SqlCommand cmd = new SqlCommand(strcln, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            FillGrid();
            lblmsg.Text = "Data is Deleted.";
            lblmsg.Visible = true;
        }
    }
    protected void ddlProductname_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        if (ddlProductname.SelectedIndex > 0)
        {
            hdnProductId.Value = ddlProductname.SelectedItem.Value.ToString();
            
            ds = FillVersion();
            ddlVersion.DataSource = ds;
            ddlVersion.DataBind();
            
            ddlVersion.Items.Insert(0, "-Select-");
            ddlVersion.SelectedIndex = 0;
            ddlVersion.SelectedItem.Value = "0";
            ds = new DataSet();
            ds = FillPricePlan();
            ddlPricePlanName.DataSource = ds;
            ddlPricePlanName.DataBind();
            
            ddlPricePlanName.Items.Insert(0, "-Select-");
            ddlPricePlanName.SelectedIndex = 0;
            ddlPricePlanName.SelectedItem.Value = "0";
          //  hdnProductId.Value = "";
            FillGrid();
        }
        else
        {
            ds = null;
            ddlVersion.DataSource = ds ;
            ddlVersion.DataBind();
            ddlVersion.Items.Clear();
            ddlVersion.Items.Insert(0, "-Select-");
            ddlVersion.SelectedIndex = 0;
            ddlVersion.SelectedItem.Value = "0";
            ddlPricePlanName.DataSource = ds;
            ddlPricePlanName.DataBind();
            ddlPricePlanName.Items.Clear();
            ddlPricePlanName.Items.Insert(0, "-Select-");
            ddlPricePlanName.SelectedIndex = 0;
            ddlPricePlanName.SelectedItem.Value = "0";
            FillGrid();
        }
       
    }
    public DataSet FillVersion()
    {
        string str = "  SELECT     ProductMaster.*, ProductDetail.* fROM  ProductDetail RIGHT OUTER JOIN " +
                     " ProductMaster ON ProductDetail.productId = ProductMaster.productId where ProductMaster.productId= " + ddlProductname.SelectedItem.Value.ToString();
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;
    }
    public DataSet FillPricePlan()
    {
         string str = "" ;
        if (RadioButtonList1.SelectedIndex == 0)
        { 
                       str = "  SELECT     ProductMaster.*, PricePlanMaster.* " + 
" FROM         ProductMaster LEFT OUTER JOIN PricePlanMaster ON ProductMaster.productId = PricePlanMaster.productId where ProductMaster.productId= " + ddlProductname.SelectedItem.Value.ToString();

        }
        else
        {

    str = "SELECT     PricePlanMaster.PricePlanId, PricePlanMaster.PricePlanName, PricePlanMaster.PricePlanDesc, PricePlanMaster.active, PricePlanMaster.StartDate, " + 
                      " PricePlanMaster.EndDate, PricePlanMaster.PricePlanAmount, PricePlanMaster.ProductId, PricePlanMaster.DurationMonth, " + 
                      " PricePlanMaster.AllowIPTrack, PricePlanMaster.GBUsage, PricePlanMaster.MaxUser, PricePlanMaster.TrafficinGB, PricePlanMaster.TotalMail, " + 
                      " SetupMaster.SetupId, SetupMaster.PricePlanId AS Expr1, SetupMaster.ProductDetailId, SetupMaster.ProductSetup, SetupMaster.ProductDB, " + 
                      " SetupMaster.ProductExtra FROM         SetupMaster LEFT OUTER JOIN " +
                      " PricePlanMaster ON SetupMaster.PricePlanId = PricePlanMaster.PricePlanId where PricePlanMaster.productId= " + ddlProductname.SelectedItem.Value.ToString();
        }

               SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;
    }

    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedIndex == 0)
        {
            btnSubmit.Text = "Submit";
        }
        else
        {
            btnSubmit.Text = "Update";
        }
    }
    protected void ddlVersion_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        if (ddlVersion.SelectedIndex > 0)
        {
            hdnProductDetailId.Value = ddlVersion.SelectedItem.Value.ToString();
            FillGrid();
        }
    }
    protected void ddlPricePlanName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPricePlanName.SelectedIndex > 0)
        {
            FillGrid();
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        FillGrid();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void ddlClientList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlClientList.SelectedIndex > 0)
        {
            FillProduct();
        }

        else
        {
            ddlProductname.DataSource = null;
            ddlProductname.DataBind();
        }

    }
}
