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
using System.Data.OleDb;
using System.Text;
using System.Reflection;
using System.Data;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;

public partial class ShoppingCart_Admin_xcelltransferdata : System.Web.UI.Page
{

    Int32 TyId;
    Int32 maxdetailid12;
    Int32 maxinvenid;
    string FilePath;
    string ConnectionString;

    OleDbConnection oledbConn;
  //  SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    SqlConnection con;
    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
         pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();




    }

  
    protected void btnfileupload_Click(object sender, EventArgs e)
    {
       // Label1.Visible = false;
        if (File12.HasFile)
        {
            try
            {
                string FileName = Path.GetFileName(File12.PostedFile.FileName);
                string Extension = Path.GetExtension(File12.PostedFile.FileName);
                string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                FilePath = Server.MapPath(FolderPath + "ExelFile\\" + FileName);
                File12.SaveAs(FilePath);



                string FolderPath12 = ConfigurationManager.AppSettings["FolderPath"];
                string FilePath12 = Server.MapPath(FolderPath12 + "ExelFile\\" + File12.FileName);
                string SourceFilePath = FilePath12;
                ViewState["fn"] = File12.FileName;
                Session["storesourcefilepath"] = SourceFilePath;

                //  ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + SourceFilePath + ";Extended Properties=Excel 8.0;";
              
                ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + SourceFilePath + ";Extended Properties=Excel 12.0;";
                OleDbConnection oledbConn = new OleDbConnection(ConnectionString);
                if (oledbConn.State.ToString() != "Open")
                {
                    oledbConn.Open();
                }
                //****************

                //Get the Sheets in Excel WorkBoo

                ConnectionString = String.Format(ConnectionString, FilePath);

                OleDbConnection connExcel = new OleDbConnection(ConnectionString);

                OleDbCommand cmdExcel = new OleDbCommand();

                OleDbDataAdapter oda = new OleDbDataAdapter();

                cmdExcel.Connection = connExcel;
                if (connExcel.State.ToString() != "Open")
                {
                    connExcel.Open();

                }

                //Bind the Sheets to DropDownList

                ddlSheets.Items.Clear();

                ddlSheets.Items.Add(new ListItem("--Select Sheet--", ""));

                ddlSheets.DataSource = connExcel

                         .GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                ddlSheets.DataTextField = "TABLE_NAME";

                ddlSheets.DataValueField = "TABLE_NAME";

                ddlSheets.DataBind();

                connExcel.Close();

                oledbConn.Close();
            }
            catch(Exception exp)
            {
                Label1.Visible = true;
                Label1.Text = exp.ToString();
            }
        }
        else
        {
            Label1.Visible = true;
            Label1.Text = "Please Select the Excel file....";
        }
      
   }

    protected DataTable dydata()
    {

        DataTable dt = new DataTable();
        DataColumn Dcom = new DataColumn();
        Dcom.DataType = System.Type.GetType("System.String");
        Dcom.ColumnName = "No";
        Dcom.AllowDBNull = true;
        Dcom.Unique = false;
        Dcom.ReadOnly = false;

        DataColumn Dcom1 = new DataColumn();
        Dcom1.DataType = System.Type.GetType("System.String");
        Dcom1.ColumnName = "Business";
        Dcom1.AllowDBNull = true;
        Dcom1.Unique = false;
        Dcom1.ReadOnly = false;

        DataColumn Dcom2 = new DataColumn();
        Dcom2.DataType = System.Type.GetType("System.String");
        Dcom2.ColumnName = "ProductName";
        Dcom2.AllowDBNull = true;
        Dcom2.Unique = false;
        Dcom2.ReadOnly = false;
        DataColumn Dcom3 = new DataColumn();
        Dcom3.DataType = System.Type.GetType("System.String");
        Dcom3.ColumnName = "Datestart";
        Dcom3.AllowDBNull = true;
        Dcom3.Unique = false;
        Dcom3.ReadOnly = false;

        DataColumn Dcom4 = new DataColumn();
        Dcom4.DataType = System.Type.GetType("System.String");
        Dcom4.ColumnName = "ProductNo";
        Dcom4.AllowDBNull = true;
        Dcom4.Unique = false;
        Dcom4.ReadOnly = false;


        DataColumn Dcom5 = new DataColumn();
        Dcom5.DataType = System.Type.GetType("System.String");
        Dcom5.ColumnName = "Barcode";
        Dcom5.AllowDBNull = true;
        Dcom5.Unique = false;
        Dcom5.Unique = false;
        Dcom5.ReadOnly = false;


        DataColumn Dcom6 = new DataColumn();
        Dcom6.DataType = System.Type.GetType("System.String");
        Dcom6.ColumnName = "SubSubCatName";
        Dcom6.AllowDBNull = true;
        Dcom6.Unique = false;
        Dcom6.ReadOnly = false;


        DataColumn Dcom7 = new DataColumn();
        Dcom7.DataType = System.Type.GetType("System.String");
        Dcom7.ColumnName = "Description";
        Dcom7.AllowDBNull = true;
        Dcom7.Unique = false;
        Dcom7.ReadOnly = false;

        DataColumn Dcom8 = new DataColumn();
        Dcom8.DataType = System.Type.GetType("System.String");
        Dcom8.ColumnName = "Status";
        Dcom8.AllowDBNull = true;
        Dcom8.Unique = false;
        Dcom8.ReadOnly = false;

        DataColumn Dcom9 = new DataColumn();
        Dcom9.DataType = System.Type.GetType("System.String");
        Dcom9.ColumnName = "Weight";
        Dcom9.AllowDBNull = true;
        Dcom9.Unique = false;
        Dcom9.ReadOnly = false;


        DataColumn Dcom10 = new DataColumn();
        Dcom10.DataType = System.Type.GetType("System.String");
        Dcom10.ColumnName = "Unit";
        Dcom10.AllowDBNull = true;
        Dcom10.Unique = false;
        Dcom10.ReadOnly = false;

        DataColumn Dcom11 = new DataColumn();
        Dcom11.DataType = System.Type.GetType("System.String");
        Dcom11.ColumnName = "Rate";
        Dcom11.AllowDBNull = true;
        Dcom11.Unique = false;
        Dcom11.ReadOnly = false;
        dt.Columns.Add(Dcom);
        dt.Columns.Add(Dcom1);
        dt.Columns.Add(Dcom2);
        dt.Columns.Add(Dcom3);
        dt.Columns.Add(Dcom4);
        dt.Columns.Add(Dcom5);
        dt.Columns.Add(Dcom6);
        dt.Columns.Add(Dcom7);
        dt.Columns.Add(Dcom8);
        dt.Columns.Add(Dcom9);
        dt.Columns.Add(Dcom10);
        dt.Columns.Add(Dcom11);
        return dt;

    }
    


    protected void bnt123_Click(object sender, EventArgs e)
    {
        if (ddlSheets.Items.Count > 0)
        {
            DataTable dt = new DataTable();
            int totrecprd = 0;
            int rej = 0;
            string sheetname = ddlSheets.SelectedItem.ToString();
            try
            {
                // oledbConn.Close();
                string finalpath12 = Session["storesourcefilepath"].ToString();
                //ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + finalpath12 + ";Extended Properties=Excel 8.0;";
                ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + finalpath12 + ";Extended Properties=Excel 12.0;";
                oledbConn = new OleDbConnection(ConnectionString);

                //   oledbConn.Open();
                //OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Sheet1$]", oledbConn);
                OleDbCommand cmd = new OleDbCommand("SELECT * FROM [" + sheetname + "]", oledbConn);
                OleDbDataAdapter oleda = new OleDbDataAdapter();
                oleda.SelectCommand = cmd;
                DataSet ds = new DataSet();
                DataTable ds1 = new DataTable();
                oleda.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string ProductName = ds.Tables[0].Rows[i]["ProductName"].ToString();
                    string Business = ds.Tables[0].Rows[i]["Business"].ToString();
                    string Datestart = "";
                    if (Convert.ToString(ds.Tables[0].Rows[i]["Datestart"])!="")
                    {
                        Datestart = Convert.ToDateTime(ds.Tables[0].Rows[i]["Datestart"]).ToShortDateString();
                    }
                    string ProductNo = ds.Tables[0].Rows[i]["ProductNo"].ToString();
                    string Barcode = ds.Tables[0].Rows[i]["Barcode"].ToString();
                    string SubSubCatName = ds.Tables[0].Rows[i]["SubSubCatName"].ToString();
                    string Description = ds.Tables[0].Rows[i]["Description"].ToString();
                    string Status = ds.Tables[0].Rows[i]["Status"].ToString();
                    string Weight = ds.Tables[0].Rows[i]["Weight"].ToString();
                    string Unit = ds.Tables[0].Rows[i]["Unit"].ToString();
                    string Rate = ds.Tables[0].Rows[i]["Rate"].ToString();

                    if (Business == "" && ProductName == "" && Datestart == "" && ProductNo == "" && Barcode == "" && Weight == "" && Unit == "" && Rate == "")
                    {
                     
                    }
                    else
                    {
                        int kk = 0;
                        string checkproductno = " Select InventoryMaster.ProductNo,InventoryMaster.Name,InventoryCategoryMaster.compid,InventoryBarcodeMaster.Barcode from InventoryBarcodeMaster inner join InventoryMaster on InventoryMaster.InventoryMasterId=InventoryBarcodeMaster.InventoryMaster_id inner join InventoruSubSubCategory on InventoryMaster.InventorySubSubId=InventoruSubSubCategory.InventorySubSubId INNER JOIN " +
                         " InventorySubCategoryMaster ON InventoruSubSubCategory.InventorySubCatID = " +
                         " InventorySubCategoryMaster.InventorySubCatId INNER JOIN   InventoryCategoryMaster ON " +
                         " InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId " +
                         " where  InventoryCategoryMaster.compid='" + Session["comid"] + "' and InventoryCategoryMaster.CatType IS NULL" +
                         " and  (ProductNo='" + ProductNo + "'  or InventoryBarcodeMaster.Barcode='" + Barcode + "' or InventoryMaster.Name='" + ProductName + "') ";

                        DataTable ds34 = new DataTable();
                        SqlDataAdapter ad = new SqlDataAdapter(checkproductno, con);
                        ad.Fill(ds34);
                        if (ds34.Rows.Count > 0)
                        {
                            if (Convert.ToString(ds34.Rows[0]["Name"]) == ProductName)
                            {
                                kk += 1;

                            }
                            else if (Convert.ToString(ds34.Rows[0]["ProductNo"]) == ProductNo)
                            {
                                kk += 1;

                            }
                            else if (Convert.ToString(ds34.Rows[0]["Barcode"]) == Barcode)
                            {
                                kk += 1;

                            }
                        }
                        string unitid = " Select UnitTypeId from UnitTypeMaster where Name='" + Unit + "'";

                        DataTable dsuid = new DataTable();
                        SqlDataAdapter auid = new SqlDataAdapter(unitid, con);
                        auid.Fill(dsuid);
                        if (dsuid.Rows.Count > 0)
                        {
                           

                        }
                        else
                        {
                            kk += 1;
                        }
                        string warid = " Select WareHouseId from WareHouseMaster where Name='" + Business + "' and comid='"+Session["Comid"]+"'";
                        string wid = "";
                        DataTable dtw = new DataTable();
                        SqlDataAdapter adw = new SqlDataAdapter(warid, con);
                        adw.Fill(dtw);
                        if (dtw.Rows.Count > 0)
                        {
                            wid = Convert.ToString(dtw.Rows[0]["WareHouseId"]);
                        }
                        else
                        {
                            kk += 1;

                        }
                        string subsubcat = " Select InventoruSubSubCategory.InventorySubSubId  from InventoruSubSubCategory INNER JOIN " +
                         " InventorySubCategoryMaster ON InventoruSubSubCategory.InventorySubCatID = " +
                         " InventorySubCategoryMaster.InventorySubCatId INNER JOIN   InventoryCategoryMaster ON " +
                         " InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId " +
                         " where  InventoryCategoryMaster.compid='" + Session["comid"] + "' and InventorySubSubName='" + SubSubCatName + "'";

                        DataTable dsub = new DataTable();
                        SqlDataAdapter asub = new SqlDataAdapter(subsubcat, con);
                        asub.Fill(dsub);
                        if (dsub.Rows.Count > 0)
                        {


                        }
                        else
                        {
                            kk += 1;
                        }
                        if (kk > 0)
                        {
                            rej += 1;
                            if (Convert.ToString(ViewState["data"]) == "" && dt.Rows.Count == 0)
                            {
                                dt = dydata();
                            }
                            else
                            {
                                dt = (DataTable)ViewState["data"];
                            }


                            DataRow Drow = dt.NewRow();
                            Drow["No"] = (i + 1).ToString();
                            Drow["Business"] = Business;
                            Drow["ProductName"] = ProductName;
                            Drow["Datestart"] = Datestart;

                            Drow["ProductNo"] = ProductNo;
                            Drow["Barcode"] = Barcode;
                            Drow["SubSubCatName"] = SubSubCatName;
                            Drow["Description"] = Description;
                            Drow["Status"] = Status;
                            Drow["Weight"] = Weight;
                            Drow["Unit"] = Unit;
                            Drow["Rate"] = Rate;
                            dt.Rows.Add(Drow);

                            ViewState["data"] = dt;
                        }
                        else if (kk == 0)
                        {

                            SqlCommand cmd12 = new SqlCommand("InsertInvDetailExcelTransferDataTbl", con);
                            cmd12.CommandType = CommandType.StoredProcedure;
                            cmd12.Parameters.Add("@Description", Description);
                            cmd12.Parameters.Add("@DateStarted", Datestart);

                            cmd12.Parameters.Add("@QtyTypeMasterId", 1);

                            cmd12.Parameters.Add("@UnitTypeId", Convert.ToInt32(dsuid.Rows[0]["UnitTypeId"]));

                            cmd12.Parameters.Add("@Weight", Weight);
                            cmd12.Parameters.Add(new SqlParameter("@Inventory_Details_Id", SqlDbType.Int));
                            cmd12.Parameters["@Inventory_Details_Id"].Direction = ParameterDirection.Output;
                            cmd12.Parameters.Add(new SqlParameter("@ReturnValues", SqlDbType.Int));
                            cmd12.Parameters["@ReturnValues"].Direction = ParameterDirection.ReturnValue;
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            Int32 result = cmd12.ExecuteNonQuery();
                            result = Convert.ToInt32(cmd12.Parameters["@Inventory_Details_Id"].Value);
                            con.Close();

                            SqlCommand cmd121 = new SqlCommand("InsertInvMasterExcelTransferDataTbl", con);
                            cmd121.CommandType = CommandType.StoredProcedure;
                            cmd121.Parameters.Add("@Name", ProductName);
                            cmd121.Parameters.Add("@InventoryDetailsId", result);
                            cmd121.Parameters.Add("@InventorySubSubId", Convert.ToInt32(dsub.Rows[0]["InventorySubSubId"]));

                            cmd121.Parameters.Add("@ProductNo", ProductNo);
                            cmd121.Parameters.Add("@MasterActiveStatus", Status);
                            cmd121.Parameters.Add(new SqlParameter("@InventoryMasterId", SqlDbType.Int));
                            cmd121.Parameters["@InventoryMasterId"].Direction = ParameterDirection.Output;
                            cmd121.Parameters.Add(new SqlParameter("@ReturnValues", SqlDbType.Int));
                            cmd121.Parameters["@ReturnValues"].Direction = ParameterDirection.ReturnValue;
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            Int32 result1 = cmd121.ExecuteNonQuery();
                            result1 = Convert.ToInt32(cmd121.Parameters["@InventoryMasterId"].Value);
                            con.Close();

                            string s23New = "INSERT INTO InventoryMeasurementUnit(InventoryMasterId,Unit,UnitType) " +
                                                         " VALUES   ('" + Convert.ToInt32(result1) + "' ,'" + Convert.ToDecimal(Weight) + "','" + Convert.ToInt32(dsub.Rows[0]["InventorySubSubId"]) + "')";


                            SqlCommand cmd3New = new SqlCommand(s23New, con);

                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmd3New.ExecuteNonQuery();
                            con.Close();
                            string s23Newleng = "INSERT INTO InventorySizeMaster(InventoryMasterId,Width,Height,length,Volume) " +
                                             " VALUES   ('" + Convert.ToInt32(result1) + "','" + Weight + "' ,'0','0','0')";


                            SqlCommand cmd3Newlengh = new SqlCommand(s23Newleng, con);

                            if (con.State.ToString() != "Open")
                            {
                                con.Open();

                            }

                            cmd3Newlengh.ExecuteNonQuery();
                            con.Close();
                            string insertBarcode = "Insert Into InventoryBarcodeMaster (InventoryMaster_id,Barcode)values(" + Convert.ToInt32(result1) + ",'" + Barcode + "')";
                            SqlCommand cmdBarcode = new SqlCommand(insertBarcode, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmdBarcode.ExecuteNonQuery();
                            con.Close();

                            SqlCommand cmd1 = new SqlCommand("select Max(InventoryWarehouseMasterId) as finaltotal from     InventoryWarehouseMasterTbl", con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();

                            }
                            int k = Convert.ToInt32(cmd1.ExecuteScalar());
                            k = k + 1;
                            con.Close();
                            string insertdetail = "INSERT INTO InventoryWarehouseMasterTbl  " +
                                "(InventoryWarehouseMasterId,InventoryMasterId ,InventoryDetailsId,WareHouseId,Active,PreferredVendorId, " +
                                " ReorderQuantiy,NormalOrderQuantity,ReorderLevel ,QtyOnDateStarted ,QtyOnHand,Rate,OpeningQty,OpeningRate,Total,Weight,UnitTypeId) " +
                                " VALUES ('" + k + "'," + Convert.ToInt32(result1) + "," + Convert.ToInt32(result) + ", " +
                                " " + Convert.ToInt32(wid) + ",'" + Status + "', " +
                                " '0','0','0' " +
                                ",'0','" + Datestart + "'   " +
                                "  ,'0','"+Rate+"','0','0','0','" + Weight + "','" + dsuid.Rows[0]["UnitTypeId"] + "')";

                            SqlCommand insertDetailss = new SqlCommand(insertdetail, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();

                            }
                            insertDetailss.ExecuteNonQuery();
                            con.Close();


                            totrecprd += 1;

                        }

                    }


                }
                lblnoofrecord.Text = totrecprd.ToString();
                lblnotimport.Text = rej.ToString();
                lblsucmsg.Text = ViewState["fn"].ToString() + " file and sheet name " + ddlSheets.SelectedItem.Text;
                ddlSheets.Items.Clear();
                File.Delete(Server.MapPath("~\\ShoppingCart\\Admin\\ExelFile\\") + ViewState["fn"].ToString());
                ViewState["fn"] = "";
                pnlsucedata.Visible = true;

                if (rej > 0)
                {
                    Button2.Visible = true;

                }
                else
                {

                    Button2.Visible = false;
                }


            }
            catch (Exception ex)
            {

                Label1.Visible = true;
                Label1.Text = "Error : " + ex.Message;
            }
            finally
            {

                oledbConn.Close();
            }
        }
    }
    protected void ImageButtondsfdsfdsf123_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void ImageButton4123_Click(object sender, ImageClickEventArgs e)
    {
        ModalPopupExtender1.Hide();
    }
    protected void btlnote_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1.Show();
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        ModalPopupExtender1.Hide();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        pnlgrd.Visible = true;
        DataTable dtr = (DataTable)ViewState["data"];

        grderrorlist.DataSource = dtr;
        grderrorlist.DataBind();
        pnldis.Visible = false;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        ViewState["data"] = "";
        grderrorlist.DataSource = null;
        grderrorlist.DataBind();
        pnldis.Visible = true;
        pnlsucedata.Visible = false;
        pnlgrd.Visible = false;
    }
    protected void grderrorlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grderrorlist.PageIndex = e.NewPageIndex;
        DataTable dtr = (DataTable)ViewState["data"];

        grderrorlist.DataSource = dtr;
        grderrorlist.DataBind();
       
    }
}