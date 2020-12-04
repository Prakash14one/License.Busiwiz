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
public partial class ShoppingCart_Admin_Inupdate : System.Web.UI.Page
{
    SqlConnection con;
    int a;
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
            txtqtyondatestarted.Text = System.DateTime.Now.ToShortDateString();

            ddllbs.DataSource = uinittype();
            ddllbs.DataTextField = "Name";
            ddllbs.DataValueField = "UnitTypeId";
            ddllbs.DataBind();



            ddlinventorysubsubid.DataSource = getall();
            ddlinventorysubsubid.DataTextField = "Category";
            ddlinventorysubsubid.DataValueField = "InventorySubSubId";
            ddlinventorysubsubid.DataBind();
          

            ViewState["inventoryWHid"] = Convert.ToInt32(Request.QueryString["InventoryWarehouseMasterId"].ToString());
            lblInvId.Text = ViewState["inventoryWHid"].ToString(); //= Convert.ToInt32(Request.QueryString["id"].ToString());





          

            string str = "select  InventoryMaster.Name,InventoryMaster.MasterActiveStatus, InventoryMaster.ProductNo, InventoryMaster.InventoryMasterId, InventoryMaster.InventorySubSubId, InventoryMaster.InventoryDetailsId, " +
                       " InventoryBarcodeMaster.Barcode ,InventoryDetails.Description, InventoryDetails.Weight,InventoryDetails.DateStarted, InventoryDetails.Inventory_Details_Id,UnitTypeMaster.Name as Name1,UnitTypeMaster.UnitTypeId  " +
                       " FROM     InventoryMaster INNER JOIN    InventoryBarcodeMaster ON InventoryBarcodeMaster.InventoryMaster_id=InventoryMaster.InventoryMasterId    INNER JOIN  InventoryDetails on  InventoryDetails.Inventory_Details_Id=InventoryMaster.InventoryDetailsId    INNER JOIN UnitTypeMaster ON  UnitTypeMaster.UnitTypeId=InventoryDetails.UnitTypeId   where  InventoryMaster.InventoryMasterId= " + lblInvId.Text + " ";

            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblINvName.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                txtname.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                txtproductno.Text = ds.Tables[0].Rows[0]["ProductNo"].ToString();
                ddlinventorysubsubid.SelectedIndex = ddlinventorysubsubid.Items.IndexOf(ddlinventorysubsubid.Items.FindByValue(ds.Tables[0].Rows[0]["InventorySubSubId"].ToString()));
                txtBarcode.Text = ds.Tables[0].Rows[0]["Barcode"].ToString();
                txtWeight.Text = ds.Tables[0].Rows[0]["Weight"].ToString();
                ddllbs.SelectedIndex = ddllbs.Items.IndexOf(ddllbs.Items.FindByValue(ds.Tables[0].Rows[0]["UnitTypeId"].ToString()));
                // ddllbs.SelectedIndex = ddllbs.Items.IndexOf(ddllbs.Items.FindByValue(ds.Tables[0].Rows[0]["UnitType"].ToString()));

                txtdescription.Text = ds.Tables[0].Rows[0]["Description"].ToString();

                chkActive.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["MasterActiveStatus"].ToString());
                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["MasterActiveStatus"].ToString()) == true)
                {
                    ddlstatus.SelectedValue = "1";

                }
                else
                {
                    ddlstatus.SelectedValue = "0";
                }
               // txtqtyondatestarted.Text = ds.Tables[0].Rows[0]["DateStarted"].ToString();
                ViewState["detailid"] = ds.Tables[0].Rows[0]["InventoryDetailsId"].ToString();
            }
        }
    }

    public DataSet uinittype()
    {
        SqlCommand cmd1 = new SqlCommand("SELECT [UnitTypeId], [Name] FROM [UnitTypeMaster] where UnitTypeId In ('1','2','3','4')", con);
        //cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataSet ds1 = new DataSet();
        adp1.Fill(ds1);
        return ds1;


    }
    public DataSet getall()
    {
        SqlCommand cmd = new SqlCommand("SELECT     InventoryCategoryMaster.InventoryCatName + ':' + InventorySubCategoryMaster.InventorySubCatName + ':' + InventoruSubSubCategory.InventorySubSubName   AS Category, InventoruSubSubCategory.InventorySubSubId FROM         InventoruSubSubCategory INNER JOIN          InventorySubCategoryMaster ON InventoruSubSubCategory.InventorySubCatID = InventorySubCategoryMaster.InventorySubCatId INNER JOIN   InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId where InventoryCategoryMaster.compid='"+Session["comid"]+"' Order by Category", con);
        //cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;
    }
    protected void imgbtnSubmit_Click(object sender, EventArgs e)
    {

        {

            lblmsg.Visible = false;

            statuslable.Visible = false;

            string pr = "select * from InventoryMaster where ProductNo='" + txtproductno.Text + "' and (InventoryMasterId <> '" + lblInvId.Text + "')";

            SqlDataAdapter pr1 = new SqlDataAdapter(pr, con);
            DataSet dspr = new DataSet();
            pr1.Fill(dspr);
            if (dspr.Tables[0].Rows.Count > 0)
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Product No. is already exist";

            }
            else
            {

                if (txtBarcode.Text == "")
                {

                    int chk;

                    //if (chkActive.Checked == true)
                    if (ddlstatus.SelectedValue == "1")
                    {
                        chk = 1;
                    }
                    else
                    {
                        chk = 0;
                    }

                    //if (chk == 0)
                    //{

                    //    string invenwarehiuse = "select * from InventoryWarehouseMasterTbl where InventoryMasterId='" + lblInvId.Text + "' and Active=1";
                    //    SqlDataAdapter dr = new SqlDataAdapter(invenwarehiuse, con);
                    //    DataSet dr1 = new DataSet();
                    //    dr.Fill(dr1);
                    //    if (dr1.Tables[0].Rows.Count > 0)
                    //    {
                    //        lblmsg.Visible = true;
                    //        lblmsg.Text = "First Inactivate this Item from Inventory Warehouse Master";
                    //    }
                    //    else
                    //    {




                    //        decimal weigth = Convert.ToDecimal(txtWeight.Text);

                    //        string updateweight = "   update   InventoryDetails set " +
                    //               "Description='" + txtdescription.Text + "',   DateStarted='" + txtqtyondatestarted.Text + "', " +
                    //                 "QtyTypeMasterId=25,Weight='" + weigth + "',   UnitTypeId='" + ddllbs.SelectedValue + "'  " +
                    //                 "where  Inventory_Details_Id= " + Convert.ToInt32(ViewState["detailid"].ToString()) + "";
                    //        SqlCommand cn = new SqlCommand(updateweight, con);
                    //        if (con.State.ToString() != "Open")
                    //        {
                    //            con.Open();
                    //        }
                    //        cn.ExecuteNonQuery();
                    //        con.Close();


                    //        string updateinventory = "update InventoryMaster set Name='" + txtname.Text + "',InventoryDetailsId='" + ViewState["detailid"] + "',InventorySubSubId=" + Convert.ToInt32(ddlinventorysubsubid.SelectedValue) + ",ProductNo='" + txtproductno.Text + "',MasterActiveStatus= " + chk + " where InventoryMasterId='" + lblInvId.Text + "' ";
                    //        SqlCommand cn0 = new SqlCommand(updateinventory, con);


                    //        if (con.State.ToString() != "Open")
                    //        {
                    //            con.Open();
                    //        }
                    //        cn0.ExecuteNonQuery();
                    //        con.Close();


                    //        string updatebarcode = "update  InventoryBarcodeMaster set Barcode='" + txtBarcode.Text + "' where InventoryMaster_id='" + lblInvId.Text + "'";
                    //        SqlCommand cn01 = new SqlCommand(updatebarcode, con);


                    //        if (con.State.ToString() != "Open")
                    //        {
                    //            con.Open();
                    //        }
                    //        cn01.ExecuteNonQuery();
                    //        con.Close();
                    //        Label1.Text = "Record updated successfully";
                    //        //  imgbtnCancel_Click(sender, e);
                    //        Label1.Visible = true;
                    //        Response.Redirect("InventoryReportView.aspx");
                    //    }
                    //}
                    //else
                    //{

                        decimal weigth = Convert.ToDecimal(txtWeight.Text);

                        string updateweight = "   update   InventoryDetails set " +
                               "Description='" + txtdescription.Text + "',   DateStarted='" + txtqtyondatestarted.Text + "', " +
                                 "QtyTypeMasterId=25,Weight='" + weigth + "',   UnitTypeId='" + ddllbs.SelectedValue + "'  " +
                                 "where  Inventory_Details_Id= " + Convert.ToInt32(ViewState["detailid"].ToString()) + "";
                        SqlCommand cn = new SqlCommand(updateweight, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cn.ExecuteNonQuery();
                        con.Close();


                        string updateinventory = "update InventoryMaster set Name='" + txtname.Text + "',InventoryDetailsId='" + ViewState["detailid"] + "',InventorySubSubId=" + Convert.ToInt32(ddlinventorysubsubid.SelectedValue) + ",ProductNo='" + txtproductno.Text + "',MasterActiveStatus= " + chk + " where InventoryMasterId='" + lblInvId.Text + "' ";
                        SqlCommand cn0 = new SqlCommand(updateinventory, con);


                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cn0.ExecuteNonQuery();
                        con.Close();


                        string updatebarcode = "update  InventoryBarcodeMaster set Barcode='" + txtBarcode.Text + "' where InventoryMaster_id='" + lblInvId.Text + "'";
                        SqlCommand cn01 = new SqlCommand(updatebarcode, con);


                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cn01.ExecuteNonQuery();
                        con.Close();
                        Label1.Text = "Record updated successfully";
                        //imgbtnCancel_Click(sender, e);
                        Label1.Visible = true;
                        Response.Redirect("InventoryReportView.aspx");



                   // }
                }





                else
                {



                    string br = "select * from InventoryBarcodeMaster where Barcode='" + txtBarcode.Text + "' and (InventoryMaster_id <> '" + lblInvId.Text + "')";

                    SqlDataAdapter br1 = new SqlDataAdapter(br, con);
                    DataSet brd = new DataSet();
                    br1.Fill(brd);
                    if (brd.Tables[0].Rows.Count > 0)
                    {
                        lblmsg.Visible = true;
                        lblmsg.Text = "Barcode is already exist";
                    }

                    else
                    {
                        string Name12 = "select * from InventoryMaster where Name='" + txtname.Text + "' and (InventoryMasterId <> '" + lblInvId.Text + "')";

                        SqlDataAdapter br12 = new SqlDataAdapter(Name12, con);
                        DataSet brd2 = new DataSet();
                        br12.Fill(brd2);
                        if (brd2.Tables[0].Rows.Count > 0)
                        {
                            lblmsg.Visible = true;
                            lblmsg.Text = "Product Name is already exist";

                        }
                        else
                        {

                            int chk;

                           // if (chkActive.Checked == true)
                            if (ddlstatus.SelectedValue == "1")
                            {
                                chk = 1;
                            }
                            else
                            {
                                chk = 0;
                            }

                            //if (chk == 0)
                            //{

                                //string invenwarehiuse = "select * from InventoryWarehouseMasterTbl where InventoryMasterId='" + lblInvId.Text + "' and Active=1";
                                //SqlDataAdapter dr = new SqlDataAdapter(invenwarehiuse, con);
                                //DataSet dr1 = new DataSet();
                                //dr.Fill(dr1);
                                //if (dr1.Tables[0].Rows.Count > 0)
                                //{
                                //    lblmsg.Visible = true;
                                //    lblmsg.Text = "First Inactivate this Item from Inventory Warehouse Master";
                                //}
                                //else
                                //{




                                //    decimal weigth = Convert.ToDecimal(txtWeight.Text);

                                //    string updateweight = "   update   InventoryDetails set " +
                                //           "Description='" + txtdescription.Text + "',   DateStarted='" + txtqtyondatestarted.Text + "', " +
                                //             "QtyTypeMasterId=25,Weight='" + weigth + "',   UnitTypeId='" + ddllbs.SelectedValue + "'  " +
                                //             "where  Inventory_Details_Id= " + Convert.ToInt32(ViewState["detailid"].ToString()) + "";
                                //    SqlCommand cn = new SqlCommand(updateweight, con);
                                //    if (con.State.ToString() != "Open")
                                //    {
                                //        con.Open();
                                //    }
                                //    cn.ExecuteNonQuery();
                                //    con.Close();


                                //    string updateinventory = "update InventoryMaster set Name='" + txtname.Text + "',InventoryDetailsId='" + ViewState["detailid"] + "',InventorySubSubId=" + Convert.ToInt32(ddlinventorysubsubid.SelectedValue) + ",ProductNo='" + txtproductno.Text + "',MasterActiveStatus= " + chk + " where InventoryMasterId='" + lblInvId.Text + "' ";
                                //    SqlCommand cn0 = new SqlCommand(updateinventory, con);


                                //    if (con.State.ToString() != "Open")
                                //    {
                                //        con.Open();
                                //    }
                                //    cn0.ExecuteNonQuery();
                                //    con.Close();


                                //    string updatebarcode = "update  InventoryBarcodeMaster set Barcode='" + txtBarcode.Text + "' where InventoryMaster_id='" + lblInvId.Text + "'";
                                //    SqlCommand cn01 = new SqlCommand(updatebarcode, con);


                                //    if (con.State.ToString() != "Open")
                                //    {
                                //        con.Open();
                                //    }
                                //    cn01.ExecuteNonQuery();
                                //    con.Close();
                                //    Label1.Text = "Record updated successfully";
                                //    //imgbtnCancel_Click(sender, e);
                                //    Label1.Visible = true;
                                //    Response.Redirect("InventoryReportView.aspx");
                                //}
                           // }
                            //else
                            //{

                                decimal weigth = Convert.ToDecimal(txtWeight.Text);

                                string updateweight = "   update   InventoryDetails set " +
                                       "Description='" + txtdescription.Text + "',   DateStarted='" + txtqtyondatestarted.Text + "', " +
                                         "QtyTypeMasterId=25,Weight='" + weigth + "',   UnitTypeId='" + ddllbs.SelectedValue + "'  " +
                                         "where  Inventory_Details_Id= " + Convert.ToInt32(ViewState["detailid"].ToString()) + "";
                                SqlCommand cn = new SqlCommand(updateweight, con);
                                if (con.State.ToString() != "Open")
                                {
                                    con.Open();
                                }
                                cn.ExecuteNonQuery();
                                con.Close();


                                string updateinventory = "update InventoryMaster set Name='" + txtname.Text + "',InventoryDetailsId='" + ViewState["detailid"] + "',InventorySubSubId=" + Convert.ToInt32(ddlinventorysubsubid.SelectedValue) + ",ProductNo='" + txtproductno.Text + "',MasterActiveStatus= " + chk + " where InventoryMasterId='" + lblInvId.Text + "' ";
                                SqlCommand cn0 = new SqlCommand(updateinventory, con);


                                if (con.State.ToString() != "Open")
                                {
                                    con.Open();
                                }
                                cn0.ExecuteNonQuery();
                                con.Close();


                                string updatebarcode = "update  InventoryBarcodeMaster set Barcode='" + txtBarcode.Text + "' where InventoryMaster_id='" + lblInvId.Text + "'";
                                SqlCommand cn01 = new SqlCommand(updatebarcode, con);


                                if (con.State.ToString() != "Open")
                                {
                                    con.Open();
                                }
                                cn01.ExecuteNonQuery();
                                con.Close();
                                Label1.Text = "Record updated successfully";
                                //imgbtnCancel_Click(sender, e);
                                Label1.Visible = true;
                                Response.Redirect("InventoryReportView.aspx");



                          //  }
                        }

                    }
                }
            }
        }
    }
    protected void imgbtnCancel_Click(object sender, EventArgs e)
    {

        {


            txtdescription.Text = "";
            txtname.Text = "";

            txtproductno.Text = "";
           // txtqtyondatestarted.Text = "";


            ddlinventorysubsubid.SelectedIndex = 0;
            ddlqtytypemasterid.SelectedIndex = 0;
            txtBarcode.Text = "";

            txtWeight.Text = "";
            lblmsg.Visible = false;
            Label1.Visible = false;
            Response.Redirect("InventoryReportView.aspx");


        }
    }
    protected void ddlqtytypemasterid_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
   
}
