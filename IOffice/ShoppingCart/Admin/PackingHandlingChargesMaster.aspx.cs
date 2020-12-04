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

public partial class PackingHandlingChargesMaster : System.Web.UI.Page
{
    string compid;
    int status;
    int status1;
    int status2;

    //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString"].ConnectionString);
    SqlConnection con=new SqlConnection(PageConn.connnn);
    int i=0;

    protected void Page_Load(object sender, EventArgs e)
    {
        //PageConn pgcon = new PageConn();
        //con = pgcon.dynconn;
        if (Session["Comid"] == null)
        {
            Response.Redirect("~/ShoppingCart/Admin/ShoppingCartLogin.aspx");
        }
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();

        compid = Session["comid"].ToString();
        Page.Title = pg.getPageTitle(page);
      
        Label1.Visible = false;
        if (!IsPostBack)
        {
            ViewState["sortOrder"] = "";
            fillstore();
            fillunittype();
           
            //ddlsubcategory.Items.Insert(0, "All");
            //ddlcategory.Items.Insert(0, "All");
            //ddlsubsubcategory.Items.Insert(0, "All");
            //ddlcategory.Items[0].Value = "0";
            //ddlsubcategory.Items[0].Value = "0";
            //ddlsubsubcategory.Items[0].Value = "0";
            ddlWarehouse_SelectedIndexChanged(sender, e);
            gridbind();
        }



    }
    protected void fillstore()
    {
        ddlWarehouse.Items.Clear();
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


    }
    protected void fillunittype()
    {
        string strunit = " SELECT UnitTypeId,Name  FROM UnitTypeMaster where UnitTypeId in (1,2,3,4) order by Name";
        SqlCommand cmdunit = new SqlCommand(strunit, con);
        SqlDataAdapter adpunit = new SqlDataAdapter(cmdunit);
        DataTable dtunit = new DataTable();
        adpunit.Fill(dtunit);

        ddllbs.DataSource = dtunit;
        ddllbs.DataTextField = "Name";
        ddllbs.DataValueField = "UnitTypeId";
        ddllbs.DataBind();
    }
    public void gridbind()
    {

        string str = "";
        if (ddlWarehouse.SelectedIndex > 0)
        {
            if (ddlcategory.SelectedIndex != 0)
            {
                str = " SELECT Distinct UnitTypeMaster.Name as Unitname, [PackhandMasterId],WareHouseMaster.Name as Wname,ApplyOnlineSales,ApplyRetailSales, PackingHandlingChargesMaster.[Name], [Amount], [IsPercent], [IsPerUnit], [IsPerFlatOrder], [MinOrderValue], [MaxOrderValue], [MinOrderWeight], [MaxOrderWeight], [MinitemNo], [MaxItemNo],InventoryCategoryMaster.InventoryCatName,InventorySubCategoryMaster.InventorySubCatName,InventoruSubSubCategory.InventorySubSubName, [EntryDate]  FROM WareHouseMaster inner join  [PackingHandlingChargesMaster] on PackingHandlingChargesMaster.Whid=WareHouseMaster.WareHouseId  LEFT OUTER JOIN  InventoruSubSubCategory  on InventoruSubSubCategory.InventorySubSubId=PackingHandlingChargesMaster.InvSubSubCategoryId LEFT OUTER JOIN  InventorySubCategoryMaster ON InventorySubCategoryMaster.InventorySubCatId= PackingHandlingChargesMaster.InvSubCategoryId Left OUTER JOIN InventoryCategoryMaster  ON PackingHandlingChargesMaster.InvCategoryId = InventoryCategoryMaster.InventeroyCatId Left join UnitTypeMaster on UnitTypeMaster.UnitTypeId= PackingHandlingChargesMaster.UnitType where [PackingHandlingChargesMaster].Whid = '" + ddlWarehouse.SelectedValue + "' order by Wname,InventoryCatName,InventorySubCatName,InventorySubSubName,PackingHandlingChargesMaster.[Name]";

            }
            else
            {
                str = "SELECT Distinct UnitTypeMaster.Name as Unitname, [PackhandMasterId],WareHouseMaster.Name as Wname,ApplyOnlineSales,ApplyRetailSales, PackingHandlingChargesMaster.[Name], [Amount], [IsPercent], [IsPerUnit], [IsPerFlatOrder], [MinOrderValue], [MaxOrderValue], [MinOrderWeight], [MaxOrderWeight], [MinitemNo], [MaxItemNo],InventoryCategoryMaster.InventoryCatName,InventorySubCategoryMaster.InventorySubCatName,InventoruSubSubCategory.InventorySubSubName,Convert(nvarchar, [EntryDate],101) as EntryDate FROM WareHouseMaster inner join  [PackingHandlingChargesMaster] on PackingHandlingChargesMaster.Whid=WareHouseMaster.WareHouseId left join InventoruSubSubCategory on InventoruSubSubCategory.InventorySubSubId=PackingHandlingChargesMaster.InvSubSubCategoryId left join InventorySubCategoryMaster on InventorySubCategoryMaster.InventorySubCatId =InventoruSubSubCategory.InventorySubCatID left join InventoryCategoryMaster on InventoryCategoryMaster.InventeroyCatId=InventorySubCategoryMaster.InventoryCategoryMasterId Left join UnitTypeMaster on UnitTypeMaster.UnitTypeId= PackingHandlingChargesMaster.UnitType where [PackingHandlingChargesMaster].Whid = '" + ddlWarehouse.SelectedValue + "' order by Wname,InventoryCatName,InventorySubCatName,InventorySubSubName,PackingHandlingChargesMaster.[Name]";
            }
        }
        else
        {
            str = "SELECT Distinct UnitTypeMaster.Name as Unitname, [PackhandMasterId],WareHouseMaster.Name as Wname,ApplyOnlineSales,ApplyRetailSales, PackingHandlingChargesMaster.[Name], [Amount], [IsPercent], [IsPerUnit], [IsPerFlatOrder], [MinOrderValue], [MaxOrderValue], [MinOrderWeight], [MaxOrderWeight], [MinitemNo], [MaxItemNo],InventoryCategoryMaster.InventoryCatName,InventorySubCategoryMaster.InventorySubCatName,InventoruSubSubCategory.InventorySubSubName,Convert(nvarchar, [EntryDate],101) as EntryDate  FROM WareHouseMaster inner join  [PackingHandlingChargesMaster] on PackingHandlingChargesMaster.Whid=WareHouseMaster.WareHouseId left join InventoruSubSubCategory on InventoruSubSubCategory.InventorySubSubId=PackingHandlingChargesMaster.InvSubSubCategoryId left join InventorySubCategoryMaster on InventorySubCategoryMaster.InventorySubCatId =InventoruSubSubCategory.InventorySubCatID left join InventoryCategoryMaster on InventoryCategoryMaster.InventeroyCatId=InventorySubCategoryMaster.InventoryCategoryMasterId Left join UnitTypeMaster on UnitTypeMaster.UnitTypeId= PackingHandlingChargesMaster.UnitType where WareHouseMaster.comid = '" + Session["comid"] + "' and WareHouseMaster.Status='" + 1 + "' order by Wname,InventoryCatName,InventorySubCatName,InventorySubSubName,PackingHandlingChargesMaster.[Name]";
        }
        lblCompany.Text = Session["Cname"].ToString();
        lblbusiness.Text = ddlWarehouse.SelectedItem.Text;
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        da.Fill(ds);
        GridView2.DataSource = ds.DefaultView;
        DataView myDataView = new DataView();
        myDataView = ds.DefaultView;
        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }


        GridView2.DataSource = myDataView;
        GridView2.DataBind();
        if (GridView2.Rows.Count > 0)
        {
            foreach (GridViewRow rd in GridView2.Rows)
            {
                Label invcat = (Label)rd.FindControl("lblcat");
                if (invcat.Text == "")
                {
                    invcat.Text = "All";
                }
                Label invsubcat = (Label)rd.FindControl("lblsub");
                if (invsubcat.Text == "")
                {
                    invsubcat.Text = "All";
                }
                Label invsubsubcat = (Label)rd.FindControl("lblsubsub");
                if (invsubsubcat.Text == "")
                {
                    invsubsubcat.Text = "All";
                }

                CheckBox chkperflat = (CheckBox)rd.FindControl("chkperflat");
                CheckBox chkperunit = (CheckBox)rd.FindControl("chkperunit");
                CheckBox chkpercent = (CheckBox)rd.FindControl("chkpercent");

                Label lblperorder = (Label)rd.FindControl("lblperorder");
                Label lblsingamt = (Label)rd.FindControl("lblsingamt");
                Label lblsignpers = (Label)rd.FindControl("lblsignpers");
                if (chkpercent.Checked==true)
                    {
                        lblsingamt.Visible = false;
                        lblsignpers.Visible = true;
                        lblperorder.Visible = false;
                    }
                else if (chkperunit.Checked == true)
                    {
                        lblsingamt.Visible = true;
                        lblsignpers.Visible = false;
                        lblperorder.Visible = false;
                    }
                else if (chkperflat.Checked == true)
                {
                    lblsingamt.Visible = false;
                    lblsignpers.Visible = false;
                    lblperorder.Visible = true;
                }
                
            }

        }
    }

    public DataSet fillddl1()
    {

        SqlCommand Mycommand = new SqlCommand();
        DataSet ds = new DataSet();
        SqlDataAdapter MyDataAdapter = new SqlDataAdapter();
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        string str = "SELECT distinct InventoryCategoryMaster.* from InventoryCategoryMaster inner join InventorySubCategoryMaster on InventorySubCategoryMaster.InventoryCategoryMasterId=InventoryCategoryMaster.InventeroyCatId inner join InventoruSubSubCategory on InventoruSubSubCategory.InventorySubCatID=InventorySubCategoryMaster.InventorySubCatId inner join InventoryMaster on InventoryMaster.InventorySubSubId=InventoruSubSubCategory.InventorySubSubId inner join InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.InventoryMasterId=InventoryMaster.InventoryMasterId  where WareHouseId='" + ddlWarehouse.SelectedValue + "' order by InventoryCategoryMaster.InventoryCatName";

        MyDataAdapter = new SqlDataAdapter(str, con);
        MyDataAdapter.Fill(ds);
        con.Close();
        return ds;

    }
    public DataSet fillddl2()
    {
        //SqlCommand cmd = new SqlCommand("Sp_Selct_Subcategory", con);
        //cmd.CommandType = CommandType.StoredProcedure;
        //cmd.Parameters.AddWithValue("@InventoryCategoryMasterId", ddlcategory.SelectedValue);
        //SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //DataSet ds = new DataSet();
        //adp.Fill(ds);
        string strsubcat = "SELECT InventorySubCatId  ,InventorySubCatName ,InventoryCategoryMasterId  FROM InventorySubCategoryMaster " +
                           " where InventoryCategoryMasterId = " + Convert.ToInt32(ddlcategory.SelectedValue) + " order by InventorySubCatName";
        SqlCommand cmdsubcat = new SqlCommand(strsubcat, con);
        SqlDataAdapter adpsubcat = new SqlDataAdapter(cmdsubcat);
        DataSet ds = new DataSet();
        adpsubcat.Fill(ds);

        
        return ds;

    }
    public DataSet fillddl3()
    {
        //SqlCommand cmd = new SqlCommand("Sp_Selct_SubSubcategory", con);
        //cmd.CommandType = CommandType.StoredProcedure;

        //cmd.Parameters.AddWithValue("@InventorySubCatID", ddlsubcategory.SelectedValue);
        //SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //DataSet ds = new DataSet();
        //adp.Fill(ds);
        string strsubsubcat = "SELECT InventorySubSubId ,InventorySubSubName  ,InventorySubCatID  FROM  InventoruSubSubCategory " +
                           " where InventorySubCatID=" + Convert.ToInt32(ddlsubcategory.SelectedValue) + "  order by InventorySubSubName";
        SqlCommand cmdsubsubcat = new SqlCommand(strsubsubcat, con);
        SqlDataAdapter adpsubsubcat = new SqlDataAdapter(cmdsubsubcat);
        DataSet ds = new DataSet();
        adpsubsubcat.Fill(ds);
        return ds;

    }
    protected void ddlcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlsubcategory.Items.Clear();
        if (ddlcategory.SelectedIndex != 0)
        {
            ddlsubcategory.DataSource = (DataSet)fillddl2();
            ddlsubcategory.DataTextField = "InventorySubCatName";
            ddlsubcategory.DataValueField = "InventorySubCatId";
            ddlsubcategory.DataBind();
            ddlsubcategory.Items.Insert(0, "All");
            ddlsubcategory.Items[0].Value = "0";
            //ddlsubsubcategory.Items[0].Value = "0";


        }
        else
        {

            ddlsubcategory.Items.Insert(0, "All");
            ddlsubcategory.Items[0].Value = "0";
            //ddlsubsubcategory.Items[0].Value = "0";


        }
        ddlsubcategory_SelectedIndexChanged(sender, e);

    }
    protected void ddlsubcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlsubsubcategory.Items.Clear();
        if (ddlsubcategory.SelectedIndex != 0)
        {
            ddlsubsubcategory.DataSource = (DataSet)fillddl3();
            ddlsubsubcategory.DataTextField = "InventorySubSubName";
            ddlsubsubcategory.DataValueField = "InventorySubSubId";
            ddlsubsubcategory.DataBind();
            ddlsubsubcategory.Items.Insert(0, "All");
            //ddlsubcategory.Items[0].Value = "0";
            ddlsubsubcategory.Items[0].Value = "0";


        }
        else
        {

            ddlsubsubcategory.Items.Insert(0, "All");
            // ddlsubcategory.Items[0].Value = "0";
            ddlsubsubcategory.Items[0].Value = "0";


        }


    }
    protected decimal isdecimalornot(string ck)
    {
        decimal ick = 0;
        try
        {
            ick = Convert.ToDecimal(ck);
            return ick;
        }
        catch
        {
            return ick;
        }
        //return ick;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        int c1 = Convert.ToInt32(isdecimalornot(TxtMaxOrderValue.Text));
        int b1 = Convert.ToInt32(isdecimalornot(TxtMinOrderValue.Text));
        if (b1 > c1)
        {

            Label1.Visible = true;
            Label1.Text = "Minimum Order Value must be less than Maximum Order Value";

        }

        else
        {
            int c11 = Convert.ToInt32(isdecimalornot(txtMaxOrderWeight.Text));
            int b11 = Convert.ToInt32(isdecimalornot(txtMinOrderWeight.Text));
            if (b11 > c11)
            {

                Label1.Visible = true;
                Label1.Text = "Minimum Order Weight must be less than Maximum Order Weight";

            }
            else
            {
                int c111 = Convert.ToInt32(isdecimalornot(txtMaxOrederNo.Text));
                int b111 = Convert.ToInt32(isdecimalornot(txtMinOrederNo.Text));
                if (b111 > c111)
                {

                    Label1.Visible = true;
                    Label1.Text = "Minimum No. of Items must be less than Maximum No. of Items";

                }
                else
                {

                    if (i == 1)
                    {

                        return;


                    }
                    else
                    {
                        DateTime dt = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());

                        try
                        {
                            string check = "select * from PackingHandlingChargesMaster where [Name]='" + txtName.Text + "' and  Whid='" + ddlWarehouse.SelectedValue + "'";
                            SqlDataAdapter spa = new SqlDataAdapter(check, con);
                            DataTable dts = new DataTable();
                            spa.Fill(dts);
                            if (dts.Rows.Count == 0)
                            {
                                int flag = 0;

                                if (rbPercentage.Checked == true)
                                {
                                    if (Convert.ToDecimal(txtAmount.Text) > Convert.ToDecimal(100))
                                    {
                                        flag = 1;
                                    }
                                }
                                if (flag == 0)
                                {
                                    SqlCommand cmd = new SqlCommand("Sp_Insert_PackingHandlingCharges", con);
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                                    cmd.Parameters.AddWithValue("@Amount", txtAmount.Text);
                                    cmd.Parameters.AddWithValue("@Whid", ddlWarehouse.SelectedValue);
                                    cmd.Parameters.AddWithValue("@ApplyOnlineSales", chkonline.Checked);
                                    cmd.Parameters.AddWithValue("@ApplyRetailSales", chkretail.Checked);
                                    if (rbPercentage.Checked == true)
                                    {
                                        cmd.Parameters.AddWithValue("@IsPercent", 1);
                                    }

                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@IsPercent", 0);
                                    }

                                    if (rbPerUnit.Checked == true)
                                    {

                                        cmd.Parameters.AddWithValue("@IsPerUnit", 1);
                                    }

                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@IsPerUnit", 0);
                                    }

                                    if (rbPerFlat.Checked == true)
                                    {
                                        cmd.Parameters.AddWithValue("@IsPerFlatOrder", 1);
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@IsPerFlatOrder", 0);

                                    }
                                    cmd.Parameters.AddWithValue("@MinOrderValue", TxtMinOrderValue.Text);
                                    cmd.Parameters.AddWithValue("@MaxOrderValue", TxtMaxOrderValue.Text);
                                    cmd.Parameters.AddWithValue("@MinOrderWeight", txtMinOrderWeight.Text);
                                    cmd.Parameters.AddWithValue("@MaxOrderWeight", txtMaxOrderWeight.Text);
                                    cmd.Parameters.AddWithValue("@MinitemNo", txtMinOrederNo.Text);
                                    cmd.Parameters.AddWithValue("@MaxItemNo", txtMaxOrederNo.Text);
                                    cmd.Parameters.AddWithValue("@InvCategoryId", ddlcategory.SelectedValue);
                                    cmd.Parameters.AddWithValue("@InvSubCategoryId", ddlsubcategory.SelectedValue);
                                    cmd.Parameters.AddWithValue("@UnitType", ddllbs.SelectedValue);

                                    //if (ddlsubsubcategory.SelectedIndex == 0)
                                    //{ cmd.Parameters.AddWithValue("@InvSubSubCategoryId", DBNull.Value); }
                                    //else
                                    //{
                                    cmd.Parameters.AddWithValue("@InvSubSubCategoryId", ddlsubsubcategory.SelectedValue);
                                    //}
                                    cmd.Parameters.AddWithValue("@EntryDate", Convert.ToDateTime(dt));
                                    if (con.State.ToString() != "Open")
                                    {
                                        con.Open();
                                    }
                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                    Label1.Visible = true; Label1.Text = "Record inserted successfully";
                                    Button2_Click(sender, e);
                                    gridbind();
                                    pnladd.Visible = false;
                                    btnadd.Visible = true;

                                    //GridView1.DataBind();
                                }
                                else
                                {
                                    Label1.Visible = true;
                                    Label1.Text = "You cannot input a amount percentage greater then 100";
                                }
                            }
                            else
                            {
                                Label1.Visible = true; Label1.Text = "Record already exists";
                            }
                        }


                        catch (Exception)

                        { Label1.Visible = true; Label1.Text = "Error"; }

                        finally { }
                    }
                }
            }
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        txtAmount.Text = "";
        TxtMaxOrderValue.Text = "";
        txtMaxOrderWeight.Text = "";
        txtMaxOrederNo.Text = "";
        TxtMinOrderValue.Text = "";
        txtMinOrderWeight.Text = "";
        txtName.Text = "";
        ddlcategory.SelectedIndex = -1;
        ddlsubcategory.SelectedIndex = -1;
        ddlsubsubcategory.SelectedIndex = -1;
        txtMinOrederNo.Text = "";

        lblleg.Text = "";
        imgupdate.Visible = false;
        ImageButton1.Visible = true;
        ddlWarehouse.Enabled = true;
        ddlWarehouse.SelectedIndex = 0;
        chkonline.Checked = false;
        chkretail.Checked = false;
        pnlv.Enabled = true;
        pnladd.Visible = false;
        btnadd.Visible = true;
        
    }
    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/ShoppingCart/Admin/wzOrderValueDiscountMaster.aspx");
    }
    protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/ShoppingCart/Admin/wzPromotionDiscountDetail.aspx");
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //GridView1.PageIndex = e.NewPageIndex;
        //GridView1.DataBind();
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {




    }
    protected void GridView2_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {

    }
    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView2_RowCommand1(object sender, GridViewCommandEventArgs e)
    {




        if (e.CommandName == "Edit" || e.CommandName == "View")
        {
            if (e.CommandName == "Edit")
            {
                imgupdate.Visible = true;
                ImageButton1.Visible = false;
                pnlv.Enabled = true;
                lblleg.Text = "Edit Packing and Handling Charge";
            }
            else if (e.CommandName == "View")
            {
                imgupdate.Visible = false;
                ImageButton1.Visible = false;
                pnlv.Enabled = false;
                lblleg.Text = "View Packing and Handling Charge";
            }
            Label1.Text = "";
            pnladd.Visible = true;
            btnadd.Visible = false;
           
           int key = Convert.ToInt32(e.CommandArgument);
           string s = "select * from PackingHandlingChargesMaster where PackhandMasterId = '" + key + "' ";

            SqlCommand cmd = new SqlCommand(s, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            ddlWarehouse.SelectedIndex = ddlWarehouse.Items.IndexOf(ddlWarehouse.Items.FindByValue(ds.Tables[0].Rows[0]["Whid"].ToString()));
            ddlWarehouse.Enabled = false;
            chkonline.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["ApplyOnlineSales"].ToString());
            chkretail.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["ApplyRetailSales"].ToString());
            Session["status"] = key.ToString();

            txtName.Text = ds.Tables[0].Rows[0]["Name"].ToString();
            txtAmount.Text = ds.Tables[0].Rows[0]["Amount"].ToString();


            if (ds.Tables[0].Rows[0]["IsPercent"].ToString() == "True")
            {
                rbPercentage.Checked = true;
            }
            else if (ds.Tables[0].Rows[0]["IsPerUnit"].ToString() == "True")
            {
                rbPerUnit.Checked = true;
            }
            else if (ds.Tables[0].Rows[0]["IsPerFlatOrder"].ToString() == "True")
            {
                rbPerFlat.Checked = true;

            }

            TxtMinOrderValue.Text = ds.Tables[0].Rows[0]["MinOrderValue"].ToString();
            TxtMaxOrderValue.Text = ds.Tables[0].Rows[0]["MaxOrderValue"].ToString();

            txtMinOrderWeight.Text = ds.Tables[0].Rows[0]["MinOrderWeight"].ToString();
            txtMaxOrderWeight.Text = ds.Tables[0].Rows[0]["MaxOrderWeight"].ToString();

            txtMinOrederNo.Text = ds.Tables[0].Rows[0]["MinitemNo"].ToString();
            txtMaxOrederNo.Text = ds.Tables[0].Rows[0]["MaxItemNo"].ToString();

            ddlcategory.SelectedIndex = ddlcategory.Items.IndexOf(ddlcategory.Items.FindByValue(ds.Tables[0].Rows[0]["InvCategoryId"].ToString()));
            ddllbs.SelectedIndex = ddllbs.Items.IndexOf(ddllbs.Items.FindByValue(Convert.ToString(ds.Tables[0].Rows[0]["UnitType"])));


            string s1 = "select InventorySubCatId,InventorySubCatName from  InventorySubCategoryMaster where InventoryCategoryMasterId = '" + ddlcategory.SelectedValue + "'";
            SqlDataAdapter da1 = new SqlDataAdapter(s1, con);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);
            ddlsubcategory.DataSource = ds1;
            ddlsubcategory.DataTextField = "InventorySubCatName";
            ddlsubcategory.DataValueField = "InventorySubCatId";
            ddlsubcategory.DataBind();

            ddlsubcategory.Items.Insert(0, "All");
            ddlsubcategory.Items[0].Value = "0";


            ddlsubcategory.SelectedIndex = ddlsubcategory.Items.IndexOf(ddlsubcategory.Items.FindByValue(ds.Tables[0].Rows[0]["InvSubCategoryId"].ToString()));



            string s2 = "select InventorySubSubId,InventorySubSubName from InventoruSubSubCategory where InventorySubCatID = '" + ddlsubcategory.SelectedValue + "'";
            SqlDataAdapter ddd = new SqlDataAdapter(s2, con);
            DataSet dtas = new DataSet();
            ddd.Fill(dtas);
            ddlsubsubcategory.DataSource = dtas;
            ddlsubsubcategory.DataTextField = "InventorySubSubName";
            ddlsubsubcategory.DataValueField = "InventorySubSubId";
            ddlsubsubcategory.DataBind();

            ddlsubsubcategory.Items.Insert(0, "All");
            ddlsubsubcategory.Items[0].Value = "0";


            ddlsubsubcategory.SelectedIndex = ddlsubsubcategory.Items.IndexOf(ddlsubsubcategory.Items.FindByValue(ds.Tables[0].Rows[0]["InvSubSubCategoryId"].ToString()));

        }
       



    }


    protected void imgupdate_Click(object sender, EventArgs e)
    {
        int c1 = Convert.ToInt32(isdecimalornot(TxtMaxOrderValue.Text));
        int b1 = Convert.ToInt32(isdecimalornot(TxtMinOrderValue.Text));
        if (b1 > c1)
        {

            Label1.Visible = true;
            Label1.Text = "Minimum Order Value must be less than Maximum Order Value";

        }

        else
        {
            int c11 = Convert.ToInt32(isdecimalornot(txtMaxOrderWeight.Text));
            int b11 = Convert.ToInt32(isdecimalornot(txtMinOrderWeight.Text));
            if (b11 > c11)
            {

                Label1.Visible = true;
                Label1.Text = "Minimum Order Weight must be less than Maximum Order Weight";

            }
            else
            {
                int c111 = Convert.ToInt32(isdecimalornot(txtMaxOrederNo.Text));
                int b111 = Convert.ToInt32(isdecimalornot(txtMinOrederNo.Text));
                if (b111 > c111)
                {

                    Label1.Visible = true;
                    Label1.Text = "Minimum No. of Items must be less than Maximum No. of Items";

                }
                else
                {

                    if (i == 1)
                    {

                        return;


                    }
                    else
                    {
                        if (rbPercentage.Checked == true)
                        {
                            status = 1;
                        }
                        else if (rbPerUnit.Checked == true)
                        {
                            status1 = 1;
                        }

                        else if (rbPerFlat.Checked == true)
                        {
                            status2 = 1;

                        }
                        string check = "select * from PackingHandlingChargesMaster where [Name]='" + txtName.Text + "' and  Whid='" + ddlWarehouse.SelectedValue + "' and PackhandMasterId <>'" + Session["status"] + "' ";
                        SqlDataAdapter spa = new SqlDataAdapter(check, con);
                        DataTable dts = new DataTable();
                        spa.Fill(dts);
                        if (dts.Rows.Count == 0)
                        {
                            int flag = 0;
                            if (rbPercentage.Checked == true)
                            {

                                if (Convert.ToDecimal(txtAmount.Text) > Convert.ToDecimal(100))
                                {
                                    flag = 1;
                                }
                            }
                            if (flag == 0)
                            {
                                string s2 = "update PackingHandlingChargesMaster set  Name = '" + txtName.Text + "' , Amount = '" + txtAmount.Text + "' , IsPercent = '" + status + "' , IsPerUnit = '" + status1 + "' , IsPerFlatOrder = '" + status2 + "', MinOrderValue = '" + TxtMinOrderValue.Text + "' , MaxOrderValue = '" + TxtMaxOrderValue.Text + "' , MinOrderWeight = '" + txtMinOrderWeight.Text + "' , MaxOrderWeight = '" + txtMaxOrderWeight.Text + "' , MinitemNo = '" + txtMinOrederNo.Text + "' , MaxItemNo = '" + txtMaxOrederNo.Text + "' , InvCategoryId = '" + ddlcategory.SelectedValue + "', InvSubCategoryId = '" + ddlsubcategory.SelectedValue + "' , InvSubSubCategoryId = '" + ddlsubsubcategory.SelectedValue + "',ApplyRetailSales='" + chkretail.Checked + "',ApplyOnlineSales='" + chkonline.Checked + "',UnitType='" + ddllbs.SelectedValue + "'  where PackhandMasterId = '" + Session["status"] + "'   ";

                                SqlCommand cmd = new SqlCommand(s2, con);
                                if (con.State.ToString() != "Open")
                                {
                                    con.Open();
                                }
                                cmd.ExecuteNonQuery();
                                con.Close();
                                // SqlDataAdapter da = new SqlDataAdapter(cmd);
                                //DataSet ds = new DataSet();
                                //da.Fill(ds);

                                gridbind();
                                Label1.Visible = true;
                                Label1.Text = "Record updated successfully";
                                lblleg.Text = "";
                                imgupdate.Visible = false;
                                ImageButton1.Visible = true;
                                pnladd.Visible = false;
                                btnadd.Visible = true;
                            }
                            else
                            {
                                Label1.Visible = true;
                                Label1.Text = "You cannot input a amount percentage greater then 100";
                            }
                        }
                        else
                        {
                            Label1.Visible = true;
                            Label1.Text = "Record already exists";
                            imgupdate.Visible = false;
                            ImageButton1.Visible = true;

                        }
                        ddlWarehouse.Enabled = true;
                        ddlWarehouse.SelectedIndex = 0;
                        chkonline.Checked = false;
                        chkretail.Checked = false;
                        txtName.Text = "";
                        txtAmount.Text = "";
                        TxtMinOrderValue.Text = "";
                        TxtMaxOrderValue.Text = "";
                        txtMinOrderWeight.Text = "";
                        txtMaxOrderWeight.Text = "";
                        txtMinOrederNo.Text = "";
                        txtMaxOrederNo.Text = "";

                        ddlsubcategory.SelectedIndex = 0;
                        ddlsubsubcategory.SelectedIndex = 0;
                        ddlcategory.SelectedIndex = 0;

                        ddlsubcategory.Items.Clear();
                        ddlsubsubcategory.Items.Clear();

                    }
                }
            }
        }

    }
    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string s = "Delete From PackingHandlingChargesMaster where PackhandMasterId ='" + GridView2.DataKeys[e.RowIndex].Value + "'";
        SqlCommand cmd = new SqlCommand(s, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd.ExecuteNonQuery();
        gridbind();
        Label1.Visible = true;
        Label1.Text = "Record deleted successfully";
    }
    protected void ddlWarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {

        gridbind();
        ddlcategory.DataSource = (DataSet)fillddl1();
        ddlcategory.DataTextField = "InventoryCatName";
        ddlcategory.DataValueField = "InventeroyCatId";
        ddlcategory.DataBind();
        ddlsubcategory.Items.Insert(0, "All");
        ddlcategory.Items.Insert(0, "All");
        ddlsubsubcategory.Items.Insert(0, "All");

        ddlcategory.Items[0].Value = "0";
        ddlsubcategory.Items[0].Value = "0";
        ddlsubsubcategory.Items[0].Value = "0";

        ddlcategory_SelectedIndexChanged(sender, e);

    }
   
    protected void btnadd_Click(object sender, EventArgs e)
    {
        if (pnladd.Visible == false)
        {
            lblleg.Text = "Add Packing and Handling Charge";
            pnladd.Visible = true;
        }
        else
        {
            lblleg.Text = "";
            pnladd.Visible = false;
        }
        btnadd.Visible = false;
        Label1.Text = "";
    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
          //  pnlgrid.ScrollBars = ScrollBars.None;
          //  pnlgrid.Height = new Unit("100%");

            Button1.Text = "Hide Printable Version";
            Button7.Visible = true;
            if (GridView2.Columns[20].Visible == true)
            {
                ViewState["viewhide"] = "tt";
                GridView2.Columns[20].Visible = false;
            }
            if (GridView2.Columns[21].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView2.Columns[21].Visible = false;
            }
            if (GridView2.Columns[22].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                GridView2.Columns[22].Visible = false;
            }

        }
        else
        {

            //pnlgrid.ScrollBars = ScrollBars.Both;
            //pnlgrid.Height = new Unit(250);

            Button1.Text = "Printable Version";
            Button7.Visible = false;
            if (ViewState["viewhide"] != null)
            {
                GridView2.Columns[20].Visible = true;
            }
            if (ViewState["editHide"] != null)
            {
                GridView2.Columns[21].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                GridView2.Columns[22].Visible = true;
            }

        }
    }
    protected void GridView2_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        gridbind();
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
    protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
}

