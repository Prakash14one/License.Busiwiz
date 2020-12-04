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

public partial class ShoppingCart_Admin_OrderQtyRestrictionDetail : System.Web.UI.Page
{
    SqlConnection con;
    DateTime dt = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
    //string str4;
    string compid;
    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        } 
       compid = Session["comid"].ToString();
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();

        //compid = Session["Comid"].ToString();
        Page.Title = pg.getPageTitle(page);
       
    
        Label1.Visible = false;
        

        if (!IsPostBack)
        {
            Pagecontrol.dypcontrol(Page, page);

            DataTable dswh = ClsStore.SelectStorename();
            //ddlWarehouse.DataSource = dswh;
            //ddlWarehouse.DataTextField = "Name";
            //ddlWarehouse.DataValueField = "WareHouseId";

            //ddlWarehouse.DataBind();
            ddlfilterbusness.DataSource = dswh;
            ddlfilterbusness.DataTextField = "Name";
            ddlfilterbusness.DataValueField = "WareHouseId";

            ddlfilterbusness.DataBind();
            ddlfilterbusness.Items.Insert(0, "All");
            ddlfilterbusness.Items[0].Value = "0";

            //DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

            //if (dteeed.Rows.Count > 0)
            //{
            //     ViewState["Whid"] = Convert.ToString(dteeed.Rows[0]["Whid"]);
            //}


          
           
            EventArgs e123 = new EventArgs();
          //  ddlWarehouse_SelectedIndexChanged(sender, e123);
           GridOrdrQtyMasterFill();
         
            //ViewState["WH"] = Convert.ToInt32( ViewState["Whid"]);
        }

    }
    protected void FillddlFooterCoutry()
    {
        GridViewRow ft = (GridViewRow)GrdOrdrQtyMaster.FooterRow;
        DropDownList ddlwareh = (DropDownList)ft.FindControl("ddlware");
      


        DataTable ds = ClsStore.SelectStorename();
        ddlwareh.DataSource = ds;
        ddlwareh.DataTextField = "Name";
        ddlwareh.DataValueField = "WareHouseId";
        ddlwareh.DataBind();


        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlwareh.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }


    }
    protected void GridOrdrQtyMasterFill()
    {
        string pera = "";
        if (ddlfilterbusness.SelectedIndex > 0)
        {
            pera = " and OrderQtyRestrictionMaster.Whid='" + ddlfilterbusness.SelectedValue+ "'";
        }
        string strmaster = "SELECT distinct Minorder,Maxorder, OrderQtyRestrictionMaster.Name, OrderQtyRestrictionMasterId ,Whid,WarehouseMaster.Name as wname,convert(nvarchar(10),StartDate,101) as StartDate , " +
            " convert(nvarchar(10),EndDate,101) as EndDate ,Active,Case when(Active=1)then 'Active' else 'Deactive' end status, case when(Ruleapplysales='1')then 'Online Sales' else 'Retail Sales' end Ruleapp  FROM OrderQtyRestrictionMaster inner join WarehouseMaster on OrderQtyRestrictionMaster.Whid=WarehouseMaster.WarehouseId where WarehouseMaster.comid='" + compid + "'  " + pera;
        SqlCommand cmdmaster = new SqlCommand(strmaster, con);
        SqlDataAdapter adpMaster = new SqlDataAdapter(cmdmaster);
        DataTable dtmaster = new DataTable();
        adpMaster.Fill(dtmaster);
        if (dtmaster.Rows.Count > 0)
        {

            GrdOrdrQtyMaster.DataSource = dtmaster;
            GrdOrdrQtyMaster.DataBind();
          
        }
        else
        {
            ShowNoResultFound(dtmaster, GrdOrdrQtyMaster);
        }
        FillddlFooterCoutry();
    }
    private void ShowNoResultFound(DataTable source, GridView gv)
    {
        //"SELECT  , ,convert(nvarchar(10),StartDate,101) as  , " +
        //   " convert(nvarchar(10),EndDate,101) as  ,  FROM OrderQtyRestrictionMaster ";
        DataRow add = source.NewRow();
        add["OrderQtyRestrictionMasterId"] = "0";
        add["Name"] = "0";
        add["StartDate"] = "0";
        add["StartDate"] = "0";
        add["EndDate"] = System.DateTime.Now.ToShortDateString();
        add["Active"] = false;

        add["EndDate"] = System.DateTime.Now.ToShortDateString();

        source.Rows.Add(add); // create a new blank row to the DataTable
        // Bind the DataTable which contain a blank row to the GridView
        gv.DataSource = source;
        gv.DataBind();
        // Get the total number of columns in the GridView to know what the Column Span should be
        int columnsCount = gv.Columns.Count;
        gv.Rows[0].Cells.Clear();// clear all the cells in the row
        gv.Rows[0].Cells.Add(new TableCell()); //add a new blank cell
        gv.Rows[0].Cells[0].ColumnSpan = columnsCount; //set the column span to the new added cell

        //You can set the styles here
        gv.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;

        gv.Rows[0].Cells[0].Text = "NO RESULT FOUND!";
    }
    protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            //DropDownList1.DataSource = (DataSet)fillddl2();
            //DropDownList1.DataTextField = "InventoryCatName";
            //DropDownList1.DataValueField = "InventeroyCatId";
            //DropDownList1.DataBind();
            //DropDownList1.Items.Insert(0, "--Select--");


            //ddlSubCategory1.Items.Clear();
            //ddlsubsub.Items.Clear();
            //DropDownList1.Visible = true;
            //ddlSubCategory1.Visible = false;
            //ddlsubsub.Visible = false;
            //lbCategory.Visible = true;
            //lblSubSubCategory.Visible = false;
            //lbSubCategory.Visible = false;
            //ddlSubCategory1.SelectedIndex = 0;
            //ddlsubsub.SelectedIndex = 0;
        }
        catch (Exception ex2)
        { Label1.Visible = true; Label1.Text = "Error" + ex2.Message; }
        finally { }


    }

    protected void RadioButton4_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            //lbCategory.Visible = false;
            //lblSubSubCategory.Visible = true;
            //lbSubCategory.Visible = false;
            //SqlDataAdapter nilesh = new SqlDataAdapter("SELECT InventorySubSubName,InventorySubSubId      FROM InventoruSubSubCategory ", con);
            //DataSet ds1 = new DataSet();
            //nilesh.Fill(ds1, "Nilesh");
            //ds1 = (DataSet)ds1;


            //ddlsubsub.DataSource = ds1;
            //ddlsubsub.DataTextField = "InventorySubSubName";
            //ddlsubsub.DataValueField = "InventorySubSubId";
            //ddlsubsub.DataBind();
            //ddlSubCategory1.Items.Clear();
            //DropDownList1.Items.Clear();
            //DropDownList1.Visible = false;
            //ddlSubCategory1.Visible = false;
            //ddlsubsub.Visible = true;
        }
        catch (Exception ex3)
        { Label1.Visible = true; Label1.Text = "Error" + ex3.Message; }
        finally { }


    }

 
  
    public void clear()
    {

       

    }
    protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    {
        //if (ddlName.SelectedIndex == 0)
        //{

        //    args.IsValid = false;
        //    ViewState["a"] = "a";

        //}
        //else { ViewState["a"] = "b"; }
    }
    protected decimal isdecimalornot(string ck)
    {
        decimal i = 0;
        try
        {
            i = Convert.ToDecimal(ck);
            return i;
        }
        catch
        {
            return i;
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        int i = 0;
        int addc = 0;
        int upc = 0;
        int del = 0;


        Label1.Text = "";
        if (ViewState["MastrIdofOrdrQtyMastr"] != null)
        {
            if (GridView1.Rows.Count > 0)
            {

                foreach (GridViewRow gdrtt in GridView1.Rows)
                {
                    //int dkin = gdrtt.   
                    Label invwhm = (Label)gdrtt.FindControl("lblInvWHMid");
                    CheckBox chkApply123 = (CheckBox)gdrtt.FindControl("CheckBox1");

                    Label lblOrderQtyRestrictionDetailID = (Label)gdrtt.FindControl("lblOrderQtyRestrictionDetailID");


                    try
                    {



                        //string str5 = "select * from OrderQtyRestrictionDetail where OrderQtyRestrictionMasterId='" + ViewState["MastrIdofOrdrQtyMastr"].ToString() + "'  and InventoryWHM_Id='" + invwhm.Text + "'";
                        //SqlCommand cmd5 = new SqlCommand(str5, con);
                        //SqlDataAdapter nil = new SqlDataAdapter(cmd5);
                        //DataTable ds5 = new DataTable();
                        //nil.Fill(ds5);


                        //if (ds5.Rows.Count == 0)
                        //{
                        if (Convert.ToString(lblOrderQtyRestrictionDetailID.Text) == "")
                        {
                            if (chkApply123.Checked == true)
                            {
                                string strinsertDetail = "INSERT INTO OrderQtyRestrictionDetail " +
                                    " (OrderQtyRestrictionMasterId,InventoryWHM_Id,MinQty,MaxQty,MinOrderAmount,MaxOrderAmount) " +
                                    "  VALUES (" + ViewState["MastrIdofOrdrQtyMastr"].ToString() + "," + invwhm.Text + "," + txtMinQtyOut.Text + ", " +
                                    " " + txtMaxQtyOut.Text + ",'0','0') ";
                                addc = addc + 1;
                                SqlCommand cmdinsertDetail = new SqlCommand(strinsertDetail, con);
                                if (con.State.ToString() != "Open")
                                {

                                    con.Open();
                                }
                                cmdinsertDetail.ExecuteNonQuery();
                                con.Close();

                            }
                        }
                        else if (Convert.ToString(lblOrderQtyRestrictionDetailID.Text) != "")
                        {
                            string strinsertDetail = "";
                            if (chkApply123.Checked == true)
                            {
                                strinsertDetail = "Update OrderQtyRestrictionDetail set" +
                    "MinQty='" + txtMinQtyOut.Text + "',MaxQty='" + txtMaxQtyOut.Text + "' where OrderQtyRestrictionDetailID='" + lblOrderQtyRestrictionDetailID.Text + "' ";
                                upc = upc + 1;
                            }
                            else
                            {
                                strinsertDetail = "Delete from  OrderQtyRestrictionDetail  where OrderQtyRestrictionDetailID='" + lblOrderQtyRestrictionDetailID.Text + "'";
                                del = del + 1;
                            }

                            SqlCommand cmdinsertDetail = new SqlCommand(strinsertDetail, con);
                            if (con.State.ToString() != "Open")
                            {

                                con.Open();
                            }


                            cmdinsertDetail.ExecuteNonQuery();
                            con.Close();


                        }
                     
                    }

                    catch (Exception errrr)
                    {
                        Label1.Visible = true;
                        Label1.Text = " Error :" + errrr.Message;
                    }

                }
                if (addc == 0 && upc == 0 && del == 0)
                {
                    Label1.Visible = true;
                    Label1.Text = "Please Check The Checkbox";
                }
                else
                {
                    Label1.Visible = true;
                    if (upc > 0)
                    {
                        Label1.Text = upc.ToString() + " Record Updated";
                    }
                    else if(addc > 0)
                    {
                        if (Label1.Text != "")
                        {
                            Label1.Text += "'";
                        }
                        Label1.Text +=  addc.ToString() + " Record Inserted";
                    }
                    else if (del > 0)
                    {
                        if (Label1.Text != "")
                        {
                            Label1.Text += "'";
                        }
                        Label1.Text += del.ToString() + " Record Deleted";
                   
                    }
                   // RadioButtonList2_SelectedIndexChanged(sender, e);
                    string main1 = (string)(ViewState["upd"].ToString());
                    SqlCommand cmdinvwhid1 = new SqlCommand(main1, con);
                    SqlDataAdapter adpinvwhid1 = new SqlDataAdapter(cmdinvwhid1);
                    DataTable dtinvvvv = new DataTable();
                    adpinvwhid1.Fill(dtinvvvv);
                    GridView1.DataSource = dtinvvvv;
                    GridView1.DataBind();
               
                }



            }




        }
    }
   
    protected void RadioButtonList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView2.DataSource = null;
        GridView2.DataBind();
        GridView1.DataSource = null;
        GridView1.DataBind();
        ImgBtnMove.Visible = false;
        ViewState["WH"] = Convert.ToInt32(ViewState["Whid"]);
        string strbySSCat = "SELECT  distinct OrderQtyRestrictionDetail.OrderQtyRestrictionDetailID,Case when(OrderQtyRestrictionDetail.OrderQtyRestrictionDetailID IS NULL) then cast('0' as bit) else cast('1' as bit) end as chk,  InventoryWarehouseMasterTbl.InventoryWarehouseMasterId, InventoryWarehouseMasterTbl.WareHouseId, InventoryWarehouseMasterTbl.Active, " +
                     " InventoryWarehouseMasterTbl.QtyOnHand, InventoryWarehouseMasterTbl.Rate, InventoryWarehouseMasterTbl.Weight,  " +
                     " InventoryWarehouseMasterTbl.QtyOnDateStarted, InventoryWarehouseMasterTbl.NormalOrderQuantity, InventoryWarehouseMasterTbl.ReorderQuantiy,  " +
                     " InventoryWarehouseMasterTbl.ReorderLevel, InventoryWarehouseMasterTbl.PreferredVendorId, InventoryMaster.InventoryMasterId,  " +
                     " InventoryMaster.Name AS InventoryName, InventoryMaster.ProductNo, InventoryDetails.Inventory_Details_Id, InventoryDetails.Description,  " +
                     "  InventoruSubSubCategory.InventorySubSubId, InventoruSubSubCategory.InventorySubSubName,  " +
                     " InventorySubCategoryMaster.InventorySubCatId, InventorySubCategoryMaster.InventorySubCatName, InventoryCategoryMaster.InventeroyCatId,  " +
                     " InventoryCategoryMaster.InventoryCatName , InventoryCategoryMaster.InventoryCatName+' : '+InventorySubCategoryMaster.InventorySubCatName+ " +
                     " ' : ' +InventoruSubSubCategory.InventorySubSubName+' : '+InventoryMaster.Name as CScSScName " +
                     "  FROM     OrderQtyRestrictionDetail inner join OrderQtyRestrictionMaster on OrderQtyRestrictionMaster.OrderQtyRestrictionMasterId=OrderQtyRestrictionDetail.OrderQtyRestrictionMasterId and OrderQtyRestrictionDetail.OrderQtyRestrictionMasterId='" + ViewState["MastrIdofOrdrQtyMastr"] + "'  Right Join    InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.InventoryWarehouseMasterId=OrderQtyRestrictionDetail.InventoryWHM_Id Inner join " +
                     " InventoryMaster Inner join " +
                     " InventoryDetails ON InventoryMaster.InventoryDetailsId = InventoryDetails.Inventory_Details_Id Inner join " +
                     " InventoruSubSubCategory ON InventoryMaster.InventorySubSubId = InventoruSubSubCategory.InventorySubSubId ON  " +
                     " InventoryWarehouseMasterTbl.InventoryMasterId = InventoryMaster.InventoryMasterId Inner join " +
                     " InventoryCategoryMaster inner join " +
                     " InventorySubCategoryMaster ON InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId ON  " +
                     " InventoruSubSubCategory.InventorySubCatID = InventorySubCategoryMaster.InventorySubCatId left outer join InventoryMasterMNC on  InventoryMasterMNC.Inventorymasterid=InventoryMaster.InventoryMasterId" +
                     " where InventoryWarehouseMasterTbl.WareHouseId='" + Convert.ToInt32(ViewState["WH"]) + "'  and InventoryMaster.MasterActiveStatus=1 ";//and InventoryMasterMNC.copid='" + compid + "'
        ViewState["mainstringforinvWHMid"] = strbySSCat;
        Panel2.Visible = true;
        if (RadioButtonList2.SelectedValue == "0")
        {
                FillGridByCategory();
            
         
        }
        else if (RadioButtonList2.SelectedValue == "1")
        {
          
                FillGridBySubCategory();
                
             
        }
        else if (RadioButtonList2.SelectedValue == "2")
        {
           
                FillGridBySubSubCategory();
        
        }
        else if (RadioButtonList2.SelectedValue == "3")
        {
           
            ImgBtnMove.Visible = false;
            Panel2.Visible = false;
                DataTable dtinvvvv = new DataTable();
                string main1 = (string)(ViewState["mainstringforinvWHMid"].ToString());
                SqlCommand cmdinvwhid1 = new SqlCommand(main1, con);
                SqlDataAdapter adpinvwhid1 = new SqlDataAdapter(cmdinvwhid1);
                adpinvwhid1.Fill(dtinvvvv);
                GridView1.DataSource = dtinvvvv;
                GridView1.DataBind();
                ViewState["upd"] = main1;
                    foreach (GridViewRow item in GridView1.Rows)
                    {
                        string invwareid = GridView1.DataKeys[item.RowIndex].Value.ToString();
                        Label lblavgcost = (Label)item.FindControl("lblavgcost");

                        double opto = opstockoth(invwareid);
                        lblavgcost.Text = opto.ToString();
                    }
                
        }
        else
        {

        }
        if (GridView2.Rows.Count > 0)
        {
            ImgBtnMove.Visible = true;
        }
        else
        {
            ImgBtnMove.Visible = false;
        }
        if (GridView1.Rows.Count > 0)
        {

            ImgeButt1.Visible = true;
        }
        else
        {

            ImgeButt1.Visible = false;

        }
    }
    protected void GrdOrdrQtyMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void GrdOrdrQtyMaster_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GrdOrdrQtyMaster.EditIndex = e.RowIndex;
        int dk = Convert.ToInt32(GrdOrdrQtyMaster.DataKeys[GrdOrdrQtyMaster.EditIndex].Value);

        DropDownList ddlwarename = (DropDownList)GrdOrdrQtyMaster.Rows[GrdOrdrQtyMaster.EditIndex].FindControl("ddlwarename");
        DropDownList ddleditrule = (DropDownList)GrdOrdrQtyMaster.Rows[GrdOrdrQtyMaster.EditIndex].FindControl("ddleditrule");
        DropDownList ddleditstatus = (DropDownList)GrdOrdrQtyMaster.Rows[GrdOrdrQtyMaster.EditIndex].FindControl("ddleditstatus");

      //  Label lblwid = (Label)GrdOrdrQtyMaster.Rows[GrdOrdrQtyMaster.EditIndex].FindControl("lblwid");
        //Label lblrulapp = (Label)GrdOrdrQtyMaster.Rows[GrdOrdrQtyMaster.EditIndex].FindControl("lblrulapp");
        ////Label lblch = (Label)GrdOrdrQtyMaster.Rows[GrdOrdrQtyMaster.EditIndex].FindControl("lblch");
        TextBox std1 = (TextBox)(GrdOrdrQtyMaster.Rows[GrdOrdrQtyMaster.EditIndex].FindControl("txtGrdStartDate"));
        TextBox edd1 = (TextBox)(GrdOrdrQtyMaster.Rows[GrdOrdrQtyMaster.EditIndex].FindControl("txtGrdEndDate"));
   

        TextBox scemname1 = (TextBox)(GrdOrdrQtyMaster.Rows[GrdOrdrQtyMaster.EditIndex].FindControl("txtGridInScmName"));
        TextBox txtGrdInMinQty = (TextBox)(GrdOrdrQtyMaster.Rows[GrdOrdrQtyMaster.EditIndex].FindControl("txtGrdInMinQty"));
        TextBox txtGrdInMaxQty = (TextBox)(GrdOrdrQtyMaster.Rows[GrdOrdrQtyMaster.EditIndex].FindControl("txtGrdInMaxQty"));
       // CheckBox act1 = (CheckBox)(GrdOrdrQtyMaster.Rows[GrdOrdrQtyMaster.EditIndex].FindControl("CheckBoxEdit"));
        string str111 = "Select Name from OrderQtyRestrictionMaster where Name='" + scemname1.Text + "' and Whid='" + ddlwarename.SelectedValue + "' and compid='" + Session["comid"] + "' and  OrderQtyRestrictionMasterId<>'" + dk.ToString() + "' ";
        SqlCommand cmdstr111 = new SqlCommand(str111, con);
        SqlDataAdapter dastr111 = new SqlDataAdapter(cmdstr111);
        DataTable dtstr111 = new DataTable();
        dastr111.Fill(dtstr111);
        if (dtstr111.Rows.Count == 0)
        {
            string str = "select Convert(nvarchar,StartDate,101)   as StartDate,EndDate from [ReportPeriod] where Compid = '" + compid + "' and Active='1'  and Whid='" + ddlwarename.SelectedValue + "'";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToDateTime(std1.Text) < Convert.ToDateTime(dt.Rows[0][0].ToString()))
                {
                    lblstartdate.Text = dt.Rows[0][0].ToString();
                    ModalPopupExtender12222.Show();

                }


                else
                {
                    DateTime dt2 = Convert.ToDateTime(std1.Text);
                    DateTime dt1 = Convert.ToDateTime(edd1.Text);


                    if (dt1 < dt2)
                    {

                        Label1.Visible = true;
                        Label1.Text = " Start Date must be less than End Date";


                    }
                    else
                    {
                        string strupdate = " update OrderQtyRestrictionMaster  set Name='" + scemname1.Text + "' ,StartDate='" + std1.Text + "' " +
                                           " ,EndDate='" + edd1.Text + "' ,Active='" + ddleditstatus.SelectedValue + "',Ruleapplysales='" + ddleditrule.SelectedValue + "',Whid='" + ddlwarename.SelectedValue + "',Minorder='" + txtGrdInMinQty.Text + "',Maxorder='"+txtGrdInMaxQty.Text+"' " +
                                           " where OrderQtyRestrictionMasterId='" + dk.ToString() + "'  ";
                        SqlCommand cmdupdate = new SqlCommand(strupdate, con);
                        if (con.State.ToString() != "Open")
                        {

                            con.Open();
                        }
                        cmdupdate.ExecuteNonQuery();
                        con.Close();

                        Label1.Visible = true;
                        Label1.Text = "Record updated successfully";
                        GrdOrdrQtyMaster.EditIndex = -1;
                        GridOrdrQtyMasterFill();
                    }
                }
            }
        }
        else
        {
            Label1.Visible = true;
            Label1.Text = "Record already exist";
        }
    }
    protected void GrdOrdrQtyMaster_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GrdOrdrQtyMaster.EditIndex = e.NewEditIndex;
        GridOrdrQtyMasterFill();
        DropDownList ddlwarename = (DropDownList)GrdOrdrQtyMaster.Rows[GrdOrdrQtyMaster.EditIndex].FindControl("ddlwarename");
        DropDownList ddleditrule = (DropDownList)GrdOrdrQtyMaster.Rows[GrdOrdrQtyMaster.EditIndex].FindControl("ddleditrule");
        DropDownList ddleditstatus = (DropDownList)GrdOrdrQtyMaster.Rows[GrdOrdrQtyMaster.EditIndex].FindControl("ddleditstatus");

        Label lblwid = (Label)GrdOrdrQtyMaster.Rows[GrdOrdrQtyMaster.EditIndex].FindControl("lblwid1");
        Label lblrulapp = (Label)GrdOrdrQtyMaster.Rows[GrdOrdrQtyMaster.EditIndex].FindControl("lblrulapp1");
        Label lblch = (Label)GrdOrdrQtyMaster.Rows[GrdOrdrQtyMaster.EditIndex].FindControl("lblch1");
        DataTable dswh = ClsStore.SelectStorename();
        ddlwarename.DataSource = dswh;
        ddlwarename.DataTextField = "Name";
        ddlwarename.DataValueField = "WareHouseId";

        ddlwarename.DataBind();



        ddlwarename.SelectedIndex = ddlwarename.Items.IndexOf(ddlwarename.Items.FindByValue(lblwid.Text));
        ddleditrule.SelectedIndex = ddleditrule.Items.IndexOf(ddleditrule.Items.FindByText(ddleditrule.Text));
        ddleditstatus.SelectedIndex = ddleditstatus.Items.IndexOf(ddleditstatus.Items.FindByText(lblch.Text));


      
    }
    protected void GrdOrdrQtyMaster_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GrdOrdrQtyMaster.EditIndex = -1;
        GridOrdrQtyMasterFill();
    }
    protected void lblScemaName_Click(object sender, ImageClickEventArgs e)
    {

      
        GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
        int rinrow = row.RowIndex;



        int dk = Convert.ToInt32(GrdOrdrQtyMaster.DataKeys[rinrow].Value);
        ViewState["MastrIdofOrdrQtyMastr"] = dk;
        Label lblwid = (Label)(GrdOrdrQtyMaster.Rows[rinrow].FindControl("lblwid"));
        Label lblwname = (Label)(GrdOrdrQtyMaster.Rows[rinrow].FindControl("lblwname"));
        Label lblSchemaame = (Label)(GrdOrdrQtyMaster.Rows[rinrow].FindControl("lblSchemaame"));
        Label lblrulapp = (Label)(GrdOrdrQtyMaster.Rows[rinrow].FindControl("lblrulapp"));
        Label lblGrdEndDate = (Label)(GrdOrdrQtyMaster.Rows[rinrow].FindControl("lblGrdEndDate"));
        Label lblch = (Label)(GrdOrdrQtyMaster.Rows[rinrow].FindControl("lblch"));

        Label lblMinDiscountQty = (Label)(GrdOrdrQtyMaster.Rows[rinrow].FindControl("lblMinDiscountQty"));
        Label lblMaxDiscountQty = (Label)(GrdOrdrQtyMaster.Rows[rinrow].FindControl("lblMaxDiscountQty"));


        ViewState["Whid"] = lblwid.Text;
        lblap.Text = lblSchemaame.Text;
        lblbname.Text = lblwname.Text;
        lblrulea.Text = lblrulapp.Text;
        lblsdate.Text = lblGrdEndDate.Text;
        lblenddate.Text = lblGrdEndDate.Text;
        pnlApplySchema.Visible = true;
        lblst.Text = lblch.Text; 
        lblSchemaName.Text = lblSchemaame.Text;
        lblminorder.Text = lblMinDiscountQty.Text;
        lblmaxorder.Text = lblMaxDiscountQty.Text;


        pnlQtyRestriction.Visible = true;
      
        pnlqty.Visible = true;
        Panel2.Visible = true;
        txtMaxQtyOut.Text = "";
      
        txtMinQtyOut.Text = "";
        GridView1.DataSource = null;
        GridView1.DataBind();
      
    }
    protected void GrdOrdrQtyMaster_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Manage")
        {
           
        }
        if (e.CommandName == "Delete1")
        {
            
        }
    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        GridViewRow ft = (GridViewRow)GrdOrdrQtyMaster.FooterRow;
        TextBox scemname = (TextBox)(ft.FindControl("txtFooterScmName"));
        DropDownList ddlware = (DropDownList)(ft.FindControl("ddlware"));
        DropDownList ddlrule = (DropDownList)(ft.FindControl("ddlrule"));
        TextBox std = (TextBox)(ft.FindControl("txtFooterStartDate"));
        TextBox edd = (TextBox)(ft.FindControl("txtFooterEndDate"));

        TextBox txtFooterMinQty = (TextBox)(ft.FindControl("txtFooterMinQty"));
        TextBox txtFooterMaxQty = (TextBox)(ft.FindControl("txtFooterMaxQty"));
        //CheckBox act = (CheckBox)(ft.FindControl("CheckBoxFootr"));
        DropDownList ddlstatus = (DropDownList)(ft.FindControl("ddlstatus"));
        string str111 = "Select Name from OrderQtyRestrictionMaster where Name='" + scemname.Text + "' and compid='" + Session["comid"] + "' and Whid='" +ddlware.SelectedValue+ "' ";
        SqlCommand cmdstr111 = new SqlCommand(str111, con);
        SqlDataAdapter dastr111 = new SqlDataAdapter(cmdstr111);
        DataTable dtstr111 = new DataTable();
        dastr111.Fill(dtstr111);
        if (dtstr111.Rows.Count == 0)
        {
            string str = "select Convert(nvarchar,StartDate,101)   as StartDate,EndDate from [ReportPeriod] where Compid = '" + compid + "' and Active='1'  and Whid='" + ddlware.SelectedValue + "'";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToDateTime(std.Text) < Convert.ToDateTime(dt.Rows[0][0].ToString()))
                {
                    lblstartdate.Text = dt.Rows[0][0].ToString();
                    ModalPopupExtender12222.Show();

                }


                else
                {
                    DateTime dt2 = Convert.ToDateTime(std.Text);
                    DateTime dt1 = Convert.ToDateTime(edd.Text);


                    if (dt1 < dt2)
                    {

                        Label1.Visible = true;
                        Label1.Text = " Start Date must be less than End Date";


                    }
                    else
                    {
                        string strinsert = "insert into OrderQtyRestrictionMaster(Name ,StartDate ,EndDate ,Active,compid,Whid,Ruleapplysales,Minorder,Maxorder ) " +
                            "Values ('" + scemname.Text + "','" + std.Text + "','" + edd.Text + "','" + ddlstatus.SelectedValue + "','" + compid + "','" + ddlware.SelectedValue + "','" + ddlrule.SelectedValue + "','" + txtFooterMinQty.Text + "','" + txtFooterMaxQty.Text + "') ";
                        SqlCommand cmdinsert = new SqlCommand(strinsert, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmdinsert.ExecuteNonQuery();
                        con.Close();
                        Label1.Visible = true;
                        Label1.Text = "Record inserted successfully";

                        GridOrdrQtyMasterFill();
                    }
                }
            }
        }
        else
        {
            Label1.Visible = true;
            Label1.Text = "Record already exist";
        }
    }
    protected void imgbtnGo_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void FillGridByCategory()
    {


             string sttGridCat = "SELECT Distinct  InventoryCategoryMaster.InventeroyCatId as Id,InventoryCategoryMaster.InventoryCatName as name FROM InventoryCategoryMaster inner join InventorySubCategoryMaster on InventorySubCategoryMaster.InventoryCategoryMasterId=InventoryCategoryMaster.InventeroyCatId inner join InventoruSubSubCategory on InventoruSubSubCategory.InventorySubCatID=InventorySubCategoryMaster.InventorySubCatId inner join InventoryMaster on InventoryMaster.InventorySubSubId=InventoruSubSubCategory.InventorySubSubId inner join InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.InventoryMasterId=InventoryMaster.InventoryMasterId inner join WareHouseMaster on WareHouseMaster.WareHouseId=InventoryWarehouseMasterTbl.WareHouseId  WHERE InventoryWarehouseMasterTbl.WareHouseId ='" +  ViewState["Whid"] + "' and  [InventoryCategoryMaster].[Activestatus]='1'";//" select InventoryCatName as name,InventeroyCatId as Id from InventoryCategoryMaster";
     
        SqlCommand cmdGridCat = new SqlCommand(sttGridCat, con);
        SqlDataAdapter adpGridCat = new SqlDataAdapter(cmdGridCat);
        DataTable dtGridCat = new DataTable();
        adpGridCat.Fill(dtGridCat);
        if (dtGridCat.Rows.Count > 0)
        {
            GridView2.DataSource = dtGridCat;
            GridView2.DataBind();

        
            ViewState["Scatid"] = 1;
        }

    }

    protected void FillGridBySubCategory()
    {
             string sttGridSCat = "SELECT  Distinct   InventorySubCategoryMaster.InventorySubCatId as Id, InventorySubCategoryMaster.InventorySubCatName, " +
               "InventoryCategoryMaster.InventoryCatName , " +
               " InventoryCategoryMaster.InventoryCatName + ' : ' + InventorySubCategoryMaster.InventorySubCatName as Name " +
               "FROM InventoryCategoryMaster inner join InventorySubCategoryMaster on InventorySubCategoryMaster.InventoryCategoryMasterId=InventoryCategoryMaster.InventeroyCatId inner join InventoruSubSubCategory on InventoruSubSubCategory.InventorySubCatID=InventorySubCategoryMaster.InventorySubCatId inner join InventoryMaster on InventoryMaster.InventorySubSubId=InventoruSubSubCategory.InventorySubSubId inner join InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.InventoryMasterId=InventoryMaster.InventoryMasterId inner join WareHouseMaster on WareHouseMaster.WareHouseId=InventoryWarehouseMasterTbl.WareHouseId  WHERE InventoryWarehouseMasterTbl.WareHouseId ='" +  ViewState["Whid"] + "' and [InventorySubCategoryMaster].[Activestatus]='1'";
               SqlCommand cmdGridSCat = new SqlCommand(sttGridSCat, con);
        SqlDataAdapter adpGridSCat = new SqlDataAdapter(cmdGridSCat);
        DataTable dtGridSCat = new DataTable();
        adpGridSCat.Fill(dtGridSCat);
        if (dtGridSCat.Rows.Count > 0)
        {
            GridView2.DataSource = dtGridSCat;
            GridView2.DataBind();
            ViewState["Scatid"] = 2;

        }
    }
    protected void FillGridBySubSubCategory()
    {

        string sttGridSSCat = "SELECT  Distinct   InventorySubCategoryMaster.InventorySubCatName, InventoryCategoryMaster.InventoryCatName, InventoruSubSubCategory.InventorySubSubId as Id, " +
                      " InventoruSubSubCategory.InventorySubSubName, InventoryCategoryMaster.InventoryCatName+' : '+InventorySubCategoryMaster.InventorySubCatName+' : '+InventoruSubSubCategory.InventorySubSubName AS Name " +
                      " FROM  InventoryCategoryMaster inner join InventorySubCategoryMaster on InventorySubCategoryMaster.InventoryCategoryMasterId=InventoryCategoryMaster.InventeroyCatId inner join InventoruSubSubCategory on InventoruSubSubCategory.InventorySubCatID=InventorySubCategoryMaster.InventorySubCatId inner join InventoryMaster on InventoryMaster.InventorySubSubId=InventoruSubSubCategory.InventorySubSubId inner join InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.InventoryMasterId=InventoryMaster.InventoryMasterId inner join WareHouseMaster on WareHouseMaster.WareHouseId=InventoryWarehouseMasterTbl.WareHouseId  WHERE InventoryWarehouseMasterTbl.WareHouseId ='" +  ViewState["Whid"] + "' and [InventoruSubSubCategory].[Activestatus]='1'";
             SqlCommand cmdGridSSCat = new SqlCommand(sttGridSSCat, con);
        SqlDataAdapter adpGridSSCat = new SqlDataAdapter(cmdGridSSCat);
        DataTable dtGridSSCat = new DataTable();
        adpGridSSCat.Fill(dtGridSSCat);

        GridView2.DataSource = dtGridSSCat;
        GridView2.DataBind();
        ViewState["Scatid"] = 3;

    }

    protected void ImgBtnMove_Click(object sender, EventArgs e)
    {

        string inviid = "";
        foreach (GridViewRow gdrdd in GridView2.Rows)
        {
            Label id = (Label)gdrdd.FindControl("lblId");


            CheckBox chkaply = (CheckBox)gdrdd.FindControl("chkApplyto");
            if (chkaply.Checked == true)
            {
                inviid += id.Text + ",";
            }
           
        }
        if (inviid.Length > 0)
        {
            inviid = inviid.Substring(0, (inviid.Length - 1));


        }
                        DataTable dtinvwh = new DataTable();

                        if (inviid.Length > 0)
                        {
                            if (ViewState["Scatid"].ToString() == "1")
                            {

                                string subids = "SELECT     InventorySubCategoryMaster.InventorySubCatName, InventoryCategoryMaster.InventoryCatName, InventoruSubSubCategory.InventorySubSubId as Id,  " +
                                   "   InventoruSubSubCategory.InventorySubSubName, InventoryCategoryMaster.InventoryCatName+' : '+InventorySubCategoryMaster.InventorySubCatName+' : '+InventoruSubSubCategory.InventorySubSubName AS Name " +
                                   " FROM         InventoruSubSubCategory LEFT OUTER JOIN " +
                                   "   InventorySubCategoryMaster ON InventoruSubSubCategory.InventorySubCatID = InventorySubCategoryMaster.InventorySubCatId LEFT OUTER JOIN " +
                                   "   InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId " +
                                   "   where InventoryCategoryMaster.InventeroyCatId In( " + inviid + ")";

                                SqlCommand cmdids = new SqlCommand(subids, con);
                                SqlDataAdapter adpids = new SqlDataAdapter(cmdids);
                                DataTable dtids = new DataTable();
                                adpids.Fill(dtids);
                                string strInvBySerchIdinvname = "";
                                if (dtids.Rows.Count > 0)
                                {
                                    string strIdinvname = "";
                                    string strInvAllIdsinvname = "";
                                    string strtempinvname = "";


                                    if (dtids.Rows.Count > 0)
                                    {

                                        foreach (DataRow dtrrr in dtids.Rows)
                                        {
                                            strIdinvname = dtrrr["Id"].ToString();
                                            strInvAllIdsinvname = strIdinvname + "," + strInvAllIdsinvname;
                                            strtempinvname = strInvAllIdsinvname.Substring(0, (strInvAllIdsinvname.Length - 1));
                                        }

                                        strInvBySerchIdinvname = " and InventoruSubSubCategory.InventorySubSubId in (" + strtempinvname + ") ";
                                        //string mainstring = strinv + "  order by SalesChallanMaster.RefSalesOrderId ";
                                    }


                                }
                                if (strInvBySerchIdinvname != "")
                                {
                                    string main = (string)(ViewState["mainstringforinvWHMid"].ToString()) + strInvBySerchIdinvname;
                                    SqlCommand cmdinvwhid = new SqlCommand(main, con);
                                    SqlDataAdapter adpinvwhid = new SqlDataAdapter(cmdinvwhid);
                                    adpinvwhid.Fill(dtinvwh);
                                    ViewState["upd"] = main;

                                }


                            }
                            else if (ViewState["Scatid"].ToString() == "2")
                            {
                                string subids = "SELECT     InventorySubCategoryMaster.InventorySubCatName, InventoryCategoryMaster.InventoryCatName, InventoruSubSubCategory.InventorySubSubId as Id,  " +
                                   "   InventoruSubSubCategory.InventorySubSubName, InventoryCategoryMaster.InventoryCatName+' : '+InventorySubCategoryMaster.InventorySubCatName+' : '+InventoruSubSubCategory.InventorySubSubName AS Name " +
                                   " FROM         InventoruSubSubCategory LEFT OUTER JOIN " +
                                   "   InventorySubCategoryMaster ON InventoruSubSubCategory.InventorySubCatID = InventorySubCategoryMaster.InventorySubCatId LEFT OUTER JOIN " +
                                   "   InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId " +
                                   "   where InventorySubCategoryMaster.InventorySubCatId In("+inviid+")  ";
                                SqlCommand cmdids = new SqlCommand(subids, con);
                                SqlDataAdapter adpids = new SqlDataAdapter(cmdids);
                                DataTable dtids = new DataTable();
                                adpids.Fill(dtids);
                                string strInvBySerchIdinvname = "";
                                if (dtids.Rows.Count > 0)
                                {
                                    string strIdinvname = "";
                                    string strInvAllIdsinvname = "";
                                    string strtempinvname = "";


                                    if (dtids.Rows.Count > 0)
                                    {

                                        foreach (DataRow dtrrr in dtids.Rows)
                                        {
                                            strIdinvname = dtrrr["Id"].ToString();
                                            strInvAllIdsinvname = strIdinvname + "," + strInvAllIdsinvname;
                                            strtempinvname = strInvAllIdsinvname.Substring(0, (strInvAllIdsinvname.Length - 1));
                                        }

                                        strInvBySerchIdinvname = " and InventoruSubSubCategory.InventorySubSubId in (" + strtempinvname + ") ";
                                        //string mainstring = strinv + "  order by SalesChallanMaster.RefSalesOrderId ";
                                    }


                                }
                                if (strInvBySerchIdinvname != "")
                                {
                                    string main = (string)(ViewState["mainstringforinvWHMid"].ToString()) + strInvBySerchIdinvname;
                                    SqlCommand cmdinvwhid = new SqlCommand(main, con);
                                    SqlDataAdapter adpinvwhid = new SqlDataAdapter(cmdinvwhid);
                                    adpinvwhid.Fill(dtinvwh);
                                    ViewState["upd"] = main;


                                }


                            }
                            else if (ViewState["Scatid"].ToString() == "3")
                            {
                                string subids = "SELECT     InventorySubCategoryMaster.InventorySubCatName, InventoryCategoryMaster.InventoryCatName, InventoruSubSubCategory.InventorySubSubId as Id,  " +
                                   "   InventoruSubSubCategory.InventorySubSubName, InventoryCategoryMaster.InventoryCatName+' : '+InventorySubCategoryMaster.InventorySubCatName+' : '+InventoruSubSubCategory.InventorySubSubName AS Name " +
                                   " FROM         InventoruSubSubCategory LEFT OUTER JOIN " +
                                   "   InventorySubCategoryMaster ON InventoruSubSubCategory.InventorySubCatID = InventorySubCategoryMaster.InventorySubCatId LEFT OUTER JOIN " +
                                   "   InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId " +
                                   "   where InventoruSubSubCategory.InventorySubSubId In("+inviid+")  ";  
                                SqlCommand cmdids = new SqlCommand(subids, con);
                                SqlDataAdapter adpids = new SqlDataAdapter(cmdids);
                                DataTable dtids = new DataTable();
                                adpids.Fill(dtids);
                                string strInvBySerchIdinvname = "";
                                if (dtids.Rows.Count > 0)
                                {
                                    string strIdinvname = "";
                                    string strInvAllIdsinvname = "";
                                    string strtempinvname = "";


                                    if (dtids.Rows.Count > 0)
                                    {

                                        foreach (DataRow dtrrr in dtids.Rows)
                                        {
                                            strIdinvname = dtrrr["Id"].ToString();
                                            strInvAllIdsinvname = strIdinvname + "," + strInvAllIdsinvname;
                                            strtempinvname = strInvAllIdsinvname.Substring(0, (strInvAllIdsinvname.Length - 1));
                                        }

                                        strInvBySerchIdinvname = " and InventoruSubSubCategory.InventorySubSubId in (" + strtempinvname + ") ";
                                        //string mainstring = strinv + "  order by SalesChallanMaster.RefSalesOrderId ";
                                    }


                                }
                                if (strInvBySerchIdinvname != "")
                                {




                                    string main = (string)(ViewState["mainstringforinvWHMid"].ToString()) + strInvBySerchIdinvname;
                                    SqlCommand cmdinvwhid = new SqlCommand(main, con);
                                    SqlDataAdapter adpinvwhid = new SqlDataAdapter(cmdinvwhid);
                                    adpinvwhid.Fill(dtinvwh);
                                    ViewState["upd"] = main;


                                }
                            }
                            else
                            {

                            }


                        }
                        if (dtinvwh.Rows.Count > 0)
                        {

                            GridView1.DataSource = dtinvwh;
                            GridView1.DataBind();
                           
                            

                            ViewState["minqtyFromPopup"] = txtMinQtyOut.Text;
                            ViewState["maxqtyFromPopup"] = txtMaxQtyOut.Text;

                        }
                    

            //    }
            //}

            if (ViewState["minqtyFromPopup"] != null)
            {
                txtMinQtyOut.Text = ViewState["minqtyFromPopup"].ToString();
                //lblMinQtyFromPopup.Visible = true;
            }
            else
            {
                txtMinQtyOut.Visible = true;
            }
            if (ViewState["maxqtyFromPopup"] != null)
            {
                txtMaxQtyOut.Text = ViewState["maxqtyFromPopup"].ToString();

            }
            else
            {
                txtMinQtyOut.Visible = true;
            }
            if (GridView1.Rows.Count > 0)
            {
                ImgeButt1.Visible = true;
                foreach (GridViewRow item in GridView1.Rows)
                {
                    string invwareid = GridView1.DataKeys[item.RowIndex].Value.ToString();
                    Label lblavgcost = (Label)item.FindControl("lblavgcost");

                    double opto = opstockoth(invwareid);
                    lblavgcost.Text = opto.ToString();
                }
            }
            else
            {
               
                    ImgeButt1.Visible = false;
               
            }


        
    }
    protected DataTable select(string str)
    {
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);

        return dt;

    }
    protected double opstockoth(string invwareid)
    {
        double avgqty = 0;


        double avgrate = 0;
        double TotalAvgBalsub = 0;
        double TotalAvgBal = 0;
        double AvgQtyAvail = 0;

        double AvgCostFinal = 0;

        double totavgcost = 0;
        double Avgtotqty = 0;
        DataTable dtodop = new DataTable();
        DataTable dtodop1 = new DataTable();

        dtodop = (DataTable)select("Select InventoryWarehouseMasterAvgCostTbl.* from  InventoryWarehouseMasterAvgCostTbl  inner join InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.InventoryWarehouseMasterId=InventoryWarehouseMasterAvgCostTbl.InvWMasterId where InventoryWarehouseMasterTbl.InventoryWarehouseMasterId='" + invwareid + "' and InventoryWarehouseMasterTbl.WareHouseId='" + ViewState["Whid"] + "'" + " order by IWMAvgCostId");
        if (dtodop.Rows.Count > 0)
        {
            foreach (DataRow dtr in dtodop.Rows)
            {
                if (Convert.ToString(dtr["Qty"]) != "")
                {
                    avgqty = Convert.ToDouble(dtr["Qty"].ToString());
                    if (Convert.ToString(avgqty) != "")
                    {
                        if (avgqty < 0)
                        {
                            avgqty = avgqty * (-1);
                        }
                    }
                }
                if (Convert.ToString(dtr["Rate"]) != "")
                {
                    if (avgqty < 0)
                    {

                        if (TotalAvgBal == 0 && AvgQtyAvail == 0)
                        {
                            avgrate = 0;

                        }
                        else
                        {
                            avgrate = TotalAvgBal / AvgQtyAvail;
                        }

                    }
                    else
                    {
                        avgrate = Convert.ToDouble(dtr["Rate"].ToString());
                    }

                }


                AvgQtyAvail += avgqty;

                if (AvgQtyAvail == 0)
                {
                    avgqty = 0;
                    avgrate = 0;
                    AvgQtyAvail = 0;
                    TotalAvgBalsub = 0;
                    TotalAvgBal = 0;

                }

                TotalAvgBalsub = avgqty * avgrate;
                TotalAvgBal += TotalAvgBalsub;
            }
            if (TotalAvgBal == 0 && AvgQtyAvail == 0)
            {
                AvgCostFinal = 0;

            }
            else
            {
                AvgCostFinal = TotalAvgBal / AvgQtyAvail;
            }
            //totavgcost = Convert.ToDouble(Avgtotqty) * Convert.ToDouble(AvgCostFinal);
            //  totavgcost = Math.Round(totavgcost, 2);
            totavgcost = Math.Round(AvgCostFinal, 2);
        }
        return totavgcost;

    }
   
    protected void HeaderChkbox11_CheckedChanged1(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)GridView2.HeaderRow.Cells[1].Controls[1];
        for (int i = 0; i < GridView2.Rows.Count; i++)
        {
            CheckBox ch = (CheckBox)GridView2.Rows[i].Cells[1].Controls[1];
            ch.Checked = chk.Checked;
        }
     
    }
    protected void HeaderChkbox1_CheckedChanged1(object sender, EventArgs e)
    {
        //CheckBox chk = (CheckBox)GridView1.HeaderRow.Cells[3].Controls[1];
        //for (int i = 0; i < GridView1.Rows.Count; i++)
        //{
        //    CheckBox ch = (CheckBox)GridView1.Rows[i].Cells[4].Controls[1];
        //    ch.Checked = chk.Checked;
        //}

        CheckBox chk;
        foreach (GridViewRow rowitem in GridView1.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("CheckBox1"));
            chk.Checked = ((CheckBox)sender).Checked;

          
        }
    }
    protected void GrdOrdrQtyMaster_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GrdOrdrQtyMaster.SelectedIndex = Convert.ToInt32(GrdOrdrQtyMaster.DataKeys[e.RowIndex].Value.ToString());
        int dk = Convert.ToInt32(GrdOrdrQtyMaster.SelectedIndex);

        string Delsttr = "Delete  OrderQtyRestrictionMaster where OrderQtyRestrictionMasterId=" + dk + " ";
        SqlCommand cmdinsert1 = new SqlCommand(Delsttr, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        
        cmdinsert1.ExecuteNonQuery();
        con.Close();

        
        string delstrdetail1 = "Delete OrderQtyRestrictionDetail where OrderQtyRestrictionMasterId=" + dk + " ";
        SqlCommand cmdinsert11 = new SqlCommand(delstrdetail1, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmdinsert11.ExecuteNonQuery();
        con.Close();

        Label1.Visible = true;
        Label1.Text = "Record deleted successfully";
        GridOrdrQtyMasterFill();
    }
    protected void ddlfilterbusness_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridOrdrQtyMasterFill();
    }
}
