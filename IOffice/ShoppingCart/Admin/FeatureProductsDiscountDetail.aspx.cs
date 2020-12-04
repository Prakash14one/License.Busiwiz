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

public partial class FeatureProductsDiscountDetail : System.Web.UI.Page
{
    SqlConnection con;
    // SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString"].ConnectionString);
  //  SqlConnection con = new SqlConnection(PageConn.connnn);
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
        if (!IsPostBack)
        {
            Session["sortOrder"] = "";
            Session["WH"] = "";
            Session["MastrIdofOrdrQtyMastr"] = "";
            Session["dk"] = "";
            Session["ED"] = "";
            Session["editHide"] = "";
            //Pagecontrol.dypcontrol(Page, page);

            FillddlInvCat();

            string strwh = "SELECT WareHouseId,Name,Address,CurrencyId FROM WareHouseMaster where comid='" + compid + "'and [WareHouseMaster].status = '1' Order by Name ";
            SqlCommand cmdwh = new SqlCommand(strwh, con);
            SqlDataAdapter adpwh = new SqlDataAdapter(cmdwh);
            DataTable dtwh = new DataTable();
            adpwh.Fill(dtwh);

            ddlWarehouse.DataSource = dtwh;
            ddlWarehouse.DataTextField = "Name";
            ddlWarehouse.DataValueField = "WareHouseId";
            //ddlWarehouse.Items.Insert(0, "--Select--");
            ddlWarehouse.DataBind();

            EventArgs e8 = new EventArgs();
            ddlWarehouse_SelectedIndexChanged(sender, e8);
            ddlInvSubCat.Items.Insert(0, "All");
            ddlInvSubCat.Items[0].Value = "0";
            ddlInvSubSubCat.Items.Insert(0, "All");
            ddlInvSubSubCat.Items[0].Value = "0";
            ddlInvName.Items.Insert(0, "All");
            ddlInvName.Items[0].Value = "0";
            PromotionSchemaFill();

        }
    }

    protected void PromotionSchemaFill()
    {
        GrdOrdrQtyMaster.DataSource = null;
        GrdOrdrQtyMaster.DataBind();
        string strmaster = "SELECT FeatureProdDiscountMasterID  ,FeatureProdDiscountSchemeName , " +//DiscountAmount,
                              "Convert(nvarchar(10),StartDate,101) as StartDate, " +
                             "convert(nvarchar(10),EndDate,101) as EndDate,Active " +
                           " ,case when (Active = 1) then 'Active' else 'Inactive' end as status,IsPercentage,EntryDate,Case when (Active = 1) then 'Apply' else 'View' end as app  FROM FeatureProdDiscountMaster where compid='" + compid + "'";
        SqlCommand cmdmaster = new SqlCommand(strmaster, con);
        SqlDataAdapter adpMaster = new SqlDataAdapter(cmdmaster);
        DataTable dtmaster = new DataTable();
        adpMaster.Fill(dtmaster);
        //if (dtmaster.Rows.Count > 0)
        //{
            GrdOrdrQtyMaster.DataSource = dtmaster;
            DataView myDataView = new DataView();
            myDataView = dtmaster.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }

           
            
           
            GrdOrdrQtyMaster.DataBind();
            foreach (GridViewRow item in GrdOrdrQtyMaster.Rows)
            {
                Label lblGrdStartDate = (Label)item.FindControl("lblGrdStartDate");
                Label lblGrdEndDate = (Label)item.FindControl("lblGrdEndDate");
                Label Label9 = (Label)item.FindControl("Label9");
                LinkButton img11a = (LinkButton)item.FindControl("img11a");
                if ( Convert.ToDateTime(lblGrdEndDate.Text) < Convert.ToDateTime(DateTime.Now.ToShortDateString()))
                {
                    img11a.Text = "View";
                    img11a.ToolTip = "View";
                }
                else if (Label9.Text == "Inactive")
                {
                    img11a.Text = "View";
                    img11a.ToolTip = "View";
                }
                else
                {
                    img11a.Text = "Apply";
                    img11a.ToolTip = "Apply";
                }
                
            }
        //}
        //else
        //{
        //    //ShowNoResultFound(dtmaster, GrdOrdrQtyMaster);
        //}
    }

    private void ShowNoResultFound(DataTable source, GridView gv)
    {

        DataRow add = source.NewRow();
        add["FeatureProdDiscountMasterID"] = "0";
        add["FeatureProdDiscountSchemeName"] = "0";
        //add["DiscountAmount"] = "0";
        add["StartDate"] = "0";
        add["EndDate"] = "0";
        add["Active"] = false;
        //add["IsPercentage"] = false;
        add["EntryDate"] = System.DateTime.Now.ToShortDateString();

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

        gv.Rows[0].Cells[0].Text = "No Record Found.";
    }

    protected void FillddlInvCat()
    {
        string strcat = "  SELECT Distinct  InventoryCategoryMaster.InventeroyCatId,InventoryCategoryMaster.InventoryCatName FROM InventoryCategoryMaster inner join InventorySubCategoryMaster on InventorySubCategoryMaster.InventoryCategoryMasterId=InventoryCategoryMaster.InventeroyCatId inner join InventoruSubSubCategory on InventoruSubSubCategory.InventorySubCatID=InventorySubCategoryMaster.InventorySubCatId inner join InventoryMaster on InventoryMaster.InventorySubSubId=InventoruSubSubCategory.InventorySubSubId inner join InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.InventoryMasterId=InventoryMaster.InventoryMasterId inner join WareHouseMaster on WareHouseMaster.WareHouseId=InventoryWarehouseMasterTbl.WareHouseId  WHERE InventoryCategoryMaster.CatType IS NULL and InventoryWarehouseMasterTbl.WareHouseId ='" + Convert.ToString(Session["WH"]) + "' and [InventoryCategoryMaster].[Activestatus]='1'";
        //   string strcat = "SELECT InventeroyCatId,InventoryCatName  FROM  InventoryCategoryMaster where compid='" +compid +"'";
        SqlCommand cmdcat = new SqlCommand(strcat, con);
        SqlDataAdapter adpcat = new SqlDataAdapter(cmdcat);
        DataTable dtcat = new DataTable();
        adpcat.Fill(dtcat);

        ddlInvCat.DataSource = dtcat;
        ddlInvCat.DataTextField = "InventoryCatName";
        ddlInvCat.DataValueField = "InventeroyCatId";
        ddlInvCat.DataBind();
        ddlInvCat.Items.Insert(0, "All");
        ddlInvCat.Items[0].Value = "0";
        //ddlInvCat.AutoPostBack = true;


    }
    protected void ddlInvCat_SelectedIndexChanged(object sender, EventArgs e)
    {

        ddlInvSubCat.Items.Clear();
        if (Convert.ToInt32(ddlInvCat.SelectedIndex) > 0)
        {
            string strsubcat = "SELECT InventorySubCatId  ,InventorySubCatName ,InventoryCategoryMasterId  FROM InventorySubCategoryMaster " +
                            " where InventoryCategoryMasterId = " + Convert.ToInt32(ddlInvCat.SelectedValue) + " and [InventorySubCategoryMaster].[Activestatus]='1'";
            SqlCommand cmdsubcat = new SqlCommand(strsubcat, con);
            SqlDataAdapter adpsubcat = new SqlDataAdapter(cmdsubcat);
            DataTable dtsubcat = new DataTable();
            adpsubcat.Fill(dtsubcat);

            ddlInvSubCat.DataSource = dtsubcat;

            ddlInvSubCat.DataTextField = "InventorySubCatName";
            ddlInvSubCat.DataValueField = "InventorySubCatId";
            ddlInvSubCat.DataBind();

        }

        ddlInvSubCat.Items.Insert(0, "All");
        ddlInvSubCat.Items[0].Value = "0";

        ddlInvSubCat_SelectedIndexChanged(sender, e);
    }
    protected void ddlInvSubCat_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlInvSubSubCat.Items.Clear();
        if (Convert.ToInt32(ddlInvSubCat.SelectedIndex) > 0)
        {
            string strsubsubcat = "SELECT InventorySubSubId ,InventorySubSubName  ,InventorySubCatID  FROM  InventoruSubSubCategory " +
                            " where InventorySubCatID=" + Convert.ToInt32(ddlInvSubCat.SelectedValue) + " and [InventoruSubSubCategory].[Activestatus]='1' ";
            SqlCommand cmdsubsubcat = new SqlCommand(strsubsubcat, con);
            SqlDataAdapter adpsubsubcat = new SqlDataAdapter(cmdsubsubcat);
            DataTable dtsubsubcat = new DataTable();
            adpsubsubcat.Fill(dtsubsubcat);

            ddlInvSubSubCat.DataSource = dtsubsubcat;
            ddlInvSubSubCat.DataTextField = "InventorySubSubName";
            ddlInvSubSubCat.DataValueField = "InventorySubSubId";
            ddlInvSubSubCat.DataBind();

        }


        ddlInvSubSubCat.Items.Insert(0, "All");
        ddlInvSubSubCat.Items[0].Value = "0";

        ddlInvSubSubCat_SelectedIndexChanged(sender, e);

        // ddlInvSubSubCat.AutoPostBack = true;


    }
    protected void ddlInvSubSubCat_SelectedIndexChanged(object sender, EventArgs e)
    {

        ddlInvName.Items.Clear();
        if (Convert.ToInt32(ddlInvSubSubCat.SelectedIndex) > 0)
        {
            //    string strinvname = " SELECT InventoryMaster.InventoryMasterId, Name ,InventoryDetailsId ,InventorySubSubId   ,ProductNo ,InventoryTypeId  FROM InventoryMaster left outer join InventoryMasterMNC on InventoryMaster.InventoryMasterId = InventoryMasterMNC.Inventorymasterid  " +
            //                        " where InventorySubSubId= " + Convert.ToInt32(ddlInvSubSubCat.SelectedValue) + " and InventoryMaster.MasterActiveStatus=1 and InventoryMasterMNC.copid = '" + compid + "'  ";
            string strinvname = "SELECT InventoryMasterId ,Name ,InventoryDetailsId ,InventorySubSubId   ,ProductNo ,InventoryTypeId  FROM InventoryMaster " +
                               " where InventorySubSubId= " + Convert.ToInt32(ddlInvSubSubCat.SelectedValue) + "  and MasterActiveStatus=1 ";
            SqlCommand cmdinvname = new SqlCommand(strinvname, con);
            SqlDataAdapter adpinvname = new SqlDataAdapter(cmdinvname);
            DataTable dtinvname = new DataTable();
            adpinvname.Fill(dtinvname);
            ddlInvName.DataSource = dtinvname;
            ddlInvName.DataTextField = "Name";
            ddlInvName.DataValueField = "InventoryMasterId";
            ddlInvName.DataBind();
        }

        ddlInvName.Items.Insert(0, "All");
        ddlInvName.Items[0].Value = "0";


    }
    protected void Button1_Click(object sender, EventArgs e)
    {


        int i = 0;
        int addc = 0;
        int upc = 0;
        foreach (GridViewRow dgi in this.GridView1.Rows)
        {


            Label invwhmid = (Label)dgi.FindControl("invWHMid");
            CheckBox chkapl = (CheckBox)dgi.FindControl("CheckBox1");
            if (chkapl.Checked.ToString() == "True")
            {

                if (Session["MastrIdofOrdrQtyMastr"] != null)
                {
                    string str5 = "select * from FeatureProdDiscountDetail where FeatureProdDiscountMasterID='" + Convert.ToInt32(Session["MastrIdofOrdrQtyMastr"]) + "'  and InventoryWHM_Id='" + invwhmid.Text + "'";
                    SqlCommand cmd5 = new SqlCommand(str5, con);
                    SqlDataAdapter nil = new SqlDataAdapter(cmd5);
                    DataTable ds5 = new DataTable();
                    nil.Fill(ds5);
                    string str4 = "";

                    if (ds5.Rows.Count == 0)
                    {
                        str4 = "Insert Into FeatureProdDiscountDetail(FeatureProdDiscountMasterID,InventoryWHM_Id) values " +
                         " ('" + Session["MastrIdofOrdrQtyMastr"].ToString() + "','" + invwhmid.Text + "')";
                        addc = addc + 1;


                    }
                    else
                    {
                        str4 = "Update FeatureProdDiscountDetail Set FeatureProdDiscountMasterID='" + Convert.ToInt32(Session["MastrIdofOrdrQtyMastr"]) + "' where  InventoryWHM_Id='" + invwhmid.Text + "'";

                        upc = upc + 1;


                    }
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    SqlCommand cmd33 = new SqlCommand(str4, con);
                    cmd33.ExecuteNonQuery();
                    con.Close();
                   
                }
            }

            else
            {

                if (addc == 0 && upc == 0)
                {
                    Label1.Visible = true;
                    Label1.Text = "Please Check The Checkbox";
                }
                else
                {
                    ddlWarehouse.SelectedIndex = 0;
                    ddlWarehouse_SelectedIndexChanged(sender, e);
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    Panel2.Visible = true;
                }

            }
            i++;

        }
        Label1.Text = "";
        Label1.Visible = true;
        if (upc > 0)
        {
            Label1.Text = upc.ToString() + " records have been updated";
        }
        if (addc > 0)
        {
            if (upc > 0)
            {
                Label1.Text =Label1.Text+ ",";
            }
            Label1.Text =Label1.Text + addc.ToString() + " records have been inserted";
        }

    }

    protected void imgbtnGo_Click(object sender, EventArgs e)
    {

        DataTable dtivs = (DataTable)SeachByCat();
        GridView1.DataSource = dtivs;
  
        DataView myDataView = new DataView();
        myDataView = dtivs.DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }

           
        GridView1.DataBind();

    }
    public DataTable SeachByCat()
    {
        string strinv = " SELECT     InventoryMaster.Name, InventoryMaster.ProductNo, InventoryMaster.InventoryMasterId, InventoryWarehouseMasterTbl.Rate,  " +
                      " InventoryWarehouseMasterTbl.WareHouseId, InventoryCategoryMaster.InventeroyCatId, InventoryCategoryMaster.InventoryCatName, " +
                      " InventorySubCategoryMaster.InventorySubCatId, InventorySubCategoryMaster.InventorySubCatName, InventoruSubSubCategory.InventorySubSubId,  " +
                      " InventoruSubSubCategory.InventorySubSubName,InventoryWarehouseMasterTbl.InventoryWarehouseMasterId " +
                      " FROM         InventoryMaster INNER JOIN " +
                      " InventoruSubSubCategory ON InventoryMaster.InventorySubSubId = InventoruSubSubCategory.InventorySubSubId INNER JOIN " +
                      " InventorySubCategoryMaster ON InventoruSubSubCategory.InventorySubCatID = InventorySubCategoryMaster.InventorySubCatId INNER JOIN " +
                      " InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId RIGHT OUTER JOIN " +
                      " InventoryWarehouseMasterTbl ON InventoryMaster.InventoryMasterId = InventoryWarehouseMasterTbl.InventoryMasterId left outer join InventoryMasterMNC on  InventoryMasterMNC.Inventorymasterid=InventoryMaster.InventoryMasterId " +
                      " WHERE   InventoryMaster.CatType IS NULL and   (InventoryWarehouseMasterTbl.WareHouseId = '" + Convert.ToInt32(Session["WH"].ToString()) + "' ) ";
        //and InventoryMasterMNC.copid='" + compid + "'
        string strInvId = "";
        string strInvsubsubCatId = "";
        string strInvsubcatid = "";
        string strInvCatid = "";
        // string strInvBySerchId = "";
        //if (txtSearchInvName.Text.Length <= 0)
        //{
        if (ddlInvCat.SelectedIndex > 0)
        {
            if (ddlInvSubCat.SelectedIndex > 0)
            {
                if (ddlInvSubSubCat.SelectedIndex > 0)
                {
                    if (ddlInvName.SelectedIndex > 0)
                    {
                        strInvId = "and  InventoryMaster.InventoryMasterId=" + Convert.ToInt32(ddlInvName.SelectedValue) + " ";

                    }
                    else
                    {
                        strInvsubsubCatId = " and  InventoruSubSubCategory.InventorySubSubId=" + Convert.ToInt32(ddlInvSubSubCat.SelectedValue) + "";
                    }
                }
                else
                {
                    strInvsubcatid = " and InventorySubCategoryMaster.InventorySubCatId = " + Convert.ToInt32(ddlInvSubCat.SelectedValue) + " ";

                }

            }
            else
            {
                strInvCatid = " and InventoryCategoryMaster.InventeroyCatId =" + Convert.ToInt32(ddlInvCat.SelectedValue) + " ";

                //strInvId = "where  InventoryMaster.InventoryMasterId=" + Convert.ToInt32(ddlInvName.SelectedValue) + " ";

            }
        }
        else
        {
            //strInvCatid = "where InventoryCategoryMaster.InventeroyCatId =" + Convert.ToInt32(ddlInvCat.SelectedValue) + " ";

        }

        lblCompany.Text = Session["Cname"].ToString();
        lblBusiness.Text = ddlWarehouse.SelectedItem.Text;
        lblMainCat.Text = ddlInvCat.SelectedItem.Text;
        lblSubCat.Text = ddlInvSubCat.SelectedItem.Text;
        lblSubSub.Text = ddlInvSubSubCat.SelectedItem.Text;
        lblInv.Text = ddlInvName.SelectedItem.Text;
        string mainStringCat = strinv + strInvId + strInvsubsubCatId + strInvsubcatid + strInvCatid + "order by InventoryMaster.InventoryMasterId";//+ strInvBySerchId // InventoryMaster.Name ";


        SqlCommand cmdcat = new SqlCommand(mainStringCat, con);
        SqlDataAdapter adpcat = new SqlDataAdapter(cmdcat);
        DataTable dtcat = new DataTable();
        adpcat.Fill(dtcat);



        return dtcat;

    }
    protected void ddlWarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {

        Session["WH"] = Convert.ToInt32(ddlWarehouse.SelectedValue);
        FillddlInvCat();
        ddlInvCat_SelectedIndexChanged(sender, e);
    }

       protected void GrdOrdrQtyMaster_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //GrdOrdrQtyMaster.EditIndex = e.NewEditIndex;
        //PromotionSchemaFill();
    }
    protected void GrdOrdrQtyMaster_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GrdOrdrQtyMaster.EditIndex = -1;
        PromotionSchemaFill();
    }
    protected void GrdOrdrQtyMaster_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            //GrdOrdrQtyMaster.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            //int dk = Convert.ToInt32(GrdOrdrQtyMaster.DataKeys[GrdOrdrQtyMaster.SelectedIndex].Value);
            //Session["MastrIdofOrdrQtyMastr"] = dk;

            //Label scemname = (Label)(GrdOrdrQtyMaster.Rows[GrdOrdrQtyMaster.SelectedIndex].FindControl("lblSchemaName"));
            //Label lblstatut = (Label)(GrdOrdrQtyMaster.Rows[GrdOrdrQtyMaster.SelectedIndex].FindControl("Label9"));
            //Label lblGrdEndDate = (Label)(GrdOrdrQtyMaster.Rows[GrdOrdrQtyMaster.SelectedIndex].FindControl("lblGrdEndDate"));
            //Label lblGrdStartDate = (Label)(GrdOrdrQtyMaster.Rows[GrdOrdrQtyMaster.SelectedIndex].FindControl("lblGrdStartDate"));

            //Panel2.Visible = true;
            //lblPromotionScemaName.Text = "(" + scemname.Text;
            //lblsdate.Text = lblGrdStartDate.Text;
            //lblenddate.Text = lblGrdEndDate.Text;
            //lblsta.Text = lblstatut.Text;
          
            int key = Convert.ToInt32(e.CommandArgument);
           Session["MastrIdofOrdrQtyMastr"] = key.ToString();

             string s = "select * from FeatureProdDiscountMaster where FeatureProdDiscountMasterID = '" +  Session["MastrIdofOrdrQtyMastr"] + "' ";

            SqlCommand cmd = new SqlCommand(s, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            da.Fill(ds);
            if (ds.Rows.Count > 0)
            {

                Panel2.Visible = true;


                lblPromotionScemaName.Text = Convert.ToString(ds.Rows[0]["FeatureProdDiscountSchemeName"])+",";
                //lblPromotionScemaName.Text = "(" + Convert.ToString(ds.Rows[0]["FeatureProdDiscountSchemeName"]);
                lblenddate.Text = Convert.ToDateTime(ds.Rows[0]["EndDate"]).ToShortDateString();


                Session["ED"] = Convert.ToDateTime(ds.Rows[0]["EndDate"]).ToShortDateString();
                lblsdate.Text = Convert.ToDateTime(ds.Rows[0]["StartDate"]).ToShortDateString();
                if (Convert.ToString(ds.Rows[0]["Active"]) == "True")
                {
                    lblsta.Text = "Active";
                    Panel1.Enabled = true;
                    ImageButton1.Enabled = true;
                }
                else
                {
                    lblsta.Text = "Inactive";
                    Panel1.Enabled = false;
                    ImageButton1.Enabled = false;
                }
                if (Convert.ToDateTime(ds.Rows[0]["EndDate"]) < Convert.ToDateTime(DateTime.Now.ToShortDateString()))
                {
                    Panel1.Enabled = false;
                    ImageButton1.Enabled = false;
                }
                //lblsta.Text = Convert.ToString(ds.Rows[0]["Active"]);
                imgbtnGo_Click(sender, e);
                
            }

           
        }
        else if (e.CommandName == "Delete1")
        {

        }
        else if (e.CommandName == "Edit")
        {
          
            imgupdate.Visible = true;
            Button2.Visible = false;
            pnlv.Enabled = true;
            lblleg.Text = "Edit Feature Product Plan";

            Panel2.Visible = false;
            Label1.Text = "";
            pnladd.Visible = true;
            btnadd.Visible = false;
           
           int key = Convert.ToInt32(e.CommandArgument);
           Session["dk"] = key.ToString();
           string s = "select * from FeatureProdDiscountMaster where FeatureProdDiscountMasterID = '" + key + "' ";

            SqlCommand cmd = new SqlCommand(s, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            da.Fill(ds);
            if (ds.Rows.Count > 0)
            {


                txtfeatureplan.Text = Convert.ToString(ds.Rows[0]["FeatureProdDiscountSchemeName"]);
                txtEndDate.Text = Convert.ToString(ds.Rows[0]["EndDate"]);


                Session["ED"] = Convert.ToString(ds.Rows[0]["EndDate"]);
                txtStartDate.Text = Convert.ToString(ds.Rows[0]["StartDate"]);
                if (Convert.ToString(ds.Rows[0]["Active"]) == "True")
                {
                    // ddlstatus.SelectedIndex = ddlstatus.Items.IndexOf(ddlstatus.Items.FindByValue(ds.Rows[0]["Active"].ToString()));
                    ddlstatus.SelectedIndex = 0;
                }
                else
                {
                    ddlstatus.SelectedIndex = 1;
                }
            }
        }

    }

    protected void Button1_Click1(object sender, EventArgs e)
    {
        GridViewRow ft = (GridViewRow)GrdOrdrQtyMaster.FooterRow;
        TextBox scemname = (TextBox)(ft.FindControl("txtFooterScmName"));
        TextBox std = (TextBox)(ft.FindControl("txtFooterStartDate"));
        TextBox edd = (TextBox)(ft.FindControl("txtFooterEndDate"));
        CheckBox act = (CheckBox)(ft.FindControl("CheckBoxFootr"));
        CheckBox isper21 = (CheckBox)(ft.FindControl("chkFooterPercent"));
        TextBox disc21 = (TextBox)(ft.FindControl("txtFooterDescount"));
        string str111 = "Select FeatureProdDiscountSchemeName from FeatureProdDiscountMaster where FeatureProdDiscountSchemeName='" + scemname.Text + "' and compid='" + Session["comid"] + "'";
        SqlCommand cmdstr111 = new SqlCommand(str111, con);
        SqlDataAdapter dastr111 = new SqlDataAdapter(cmdstr111);
        DataTable dtstr111 = new DataTable();
        dastr111.Fill(dtstr111);
        if (dtstr111.Rows.Count == 0)
        {
            string str = "Select Convert(varchar,FirstYearStartDate,101) from CompanyMaster where Compid='" + compid + "'";
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

                    if (dt2 < Convert.ToDateTime(DateTime.Now.ToShortDateString()))
                    {
                        Label1.Visible = true;
                        Label1.Text = " The start date must be the current date, or greater than the current date.";
                    }


                    else if (dt1 < dt2)
                    {

                        Label1.Visible = true;
                        Label1.Text = " Start Date must be less than End Date";


                    }
                    else
                    {
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        string strinsert = "insert into FeatureProdDiscountMaster(FeatureProdDiscountSchemeName,   " +
                            " StartDate ,EndDate,Active,compid  ) " +
                            "Values ('" + scemname.Text + "','" + std.Text + "','" + edd.Text + "','" + act.Checked + "','" + compid + "') ";
                        SqlCommand cmdinsert = new SqlCommand(strinsert, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmdinsert.ExecuteNonQuery();
                        con.Close();
                        Label1.Visible = true;
                        Label1.Text = "Record inserted successfully";

                        PromotionSchemaFill();
                    }
                }
            }
        }
        else
        {
            Label1.Visible = true;
            Label1.Text = "This name is already used ";
        }

    }
    protected void ImageButton9_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/ShoppingCart/Admin/wzPromotionDiscountDetail.aspx");
    }
    protected void ImageButton10_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/ShoppingCart/Admin/wzSalesRateDeterminatation.aspx");
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        ModalPopupExtender12222.Hide();
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        imgbtnGo_Click(sender, e);
    }
    protected void GrdOrdrQtyMaster_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Panel2.Visible = false;
        GrdOrdrQtyMaster.SelectedIndex = Convert.ToInt32(GrdOrdrQtyMaster.DataKeys[e.RowIndex].Value.ToString());
        int dk = Convert.ToInt32(GrdOrdrQtyMaster.SelectedIndex);

        string Delsttr = "Delete  FeatureProdDiscountMaster where FeatureProdDiscountMasterID=" + dk + " and compid='" + compid + "'";
        SqlCommand cmdinsert1 = new SqlCommand(Delsttr, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmdinsert1.ExecuteNonQuery();
        con.Close();

        //(,(
        string delstrdetail1 = "Delete FeatureProdDiscountDetail where FeatureProdDiscountMasterID=" + dk + " ";
        SqlCommand cmdinsert11 = new SqlCommand(delstrdetail1, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmdinsert11.ExecuteNonQuery();
        con.Close();

        Label1.Visible = true;
        Label1.Text = "Record deleted successfully";
        PromotionSchemaFill();
        ddlWarehouse.SelectedIndex = 0;
        ddlWarehouse_SelectedIndexChanged(sender, e);
        GridView1.DataSource = null;
        GridView1.DataBind();
        ImageButton1.Visible = false;
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        if (Button4.Text == "Printable Version")
        {
            Panel1.ScrollBars = ScrollBars.None;
            Panel1.Height = new Unit("100%");

            Button4.Text = "Hide Printable Version";
            Button7.Visible = true;
            if (GridView1.Columns[5].Visible == true)
            {
                Session["editHide"] = "tt";
                GridView1.Columns[5].Visible = false;
            }

        }
        else
        {

            //Panel2.ScrollBars = ScrollBars.Vertical;
            //Panel2.Height = new Unit(500);

            Button4.Text = "Printable Version";
            Button7.Visible = false;
            if (Session["editHide"] != null)
            {
                GridView1.Columns[5].Visible = true;
            }

        }
    }

    protected void btncance_Click(object sender, EventArgs e)
    {
        Panel2.Visible = false;
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        if (pnladd.Visible == false)
        {
            lblleg.Text = "Add New Feature Product Plan";
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
    protected void Button2_Click(object sender, EventArgs e)
    {

        string str111 = "Select FeatureProdDiscountSchemeName from FeatureProdDiscountMaster where FeatureProdDiscountSchemeName='" + txtfeatureplan.Text + "' and compid='" + Session["comid"] + "'";
        SqlCommand cmdstr111 = new SqlCommand(str111, con);
        SqlDataAdapter dastr111 = new SqlDataAdapter(cmdstr111);
        DataTable dtstr111 = new DataTable();
        dastr111.Fill(dtstr111);
        if (dtstr111.Rows.Count == 0)
        {

            DateTime dt2 = Convert.ToDateTime(txtStartDate.Text);
            DateTime dt1 = Convert.ToDateTime(txtEndDate.Text);

            if (dt2 < Convert.ToDateTime(DateTime.Now.ToShortDateString()))
            {
                Label1.Visible = true;
                Label1.Text = " The start date must be the current date, or greater than the current date.";
            }
            else if (dt1 < dt2)
            {

                Label1.Visible = true;
                Label1.Text = " Start Date must be less than End Date";

            }
            else
            {
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                string strinsert = "insert into FeatureProdDiscountMaster(FeatureProdDiscountSchemeName,   " +
                    " StartDate ,EndDate,Active,compid  ) " +
                    "Values ('" + txtfeatureplan.Text + "','" + txtStartDate.Text + "','" + txtEndDate.Text + "','" + ddlstatus.SelectedValue + "','" + compid + "') ";
                SqlCommand cmdinsert = new SqlCommand(strinsert, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdinsert.ExecuteNonQuery();
                con.Close();
                Label1.Visible = true;
                Label1.Text = "Record inserted successfully";

                PromotionSchemaFill();
            }


        }
        else
        {
            Label1.Visible = true;
            Label1.Text = "Record already exists";
        }

    }

    protected void imgupdate_Click(object sender, EventArgs e)
    {
     
         string str111 = "Select FeatureProdDiscountSchemeName from FeatureProdDiscountMaster where FeatureProdDiscountSchemeName='" + txtfeatureplan.Text + "' and compid='" + Session["comid"] + "' and  FeatureProdDiscountMasterID<>'" +Session["dk"] + "' ";
        SqlCommand cmdstr111 = new SqlCommand(str111, con);
        SqlDataAdapter dastr111 = new SqlDataAdapter(cmdstr111);
        DataTable dtstr111 = new DataTable();
        dastr111.Fill(dtstr111);
        if (dtstr111.Rows.Count == 0)
        {
            int ct = 0;
                    DateTime dt2 = Convert.ToDateTime(txtStartDate.Text);
                    DateTime dt1 = Convert.ToDateTime(txtEndDate.Text);

                    if (Convert.ToDateTime(Session["ED"]).ToShortDateString() == Convert.ToDateTime(txtEndDate.Text).ToShortDateString())
                    {

                    }
                    else if (dt1 < Convert.ToDateTime(DateTime.Now.ToShortDateString()))
                    {
                        Label1.Visible = true;
                        Label1.Text = "The end date must be the current date, or greater than the current date.";
                        ct = 1;
                    }

                    if (dt1 < dt2)
                    {

                        Label1.Visible = true;
                        Label1.Text = " Start Date must be less than End Date";

                        ct = 1;
                    }
                    else
                    {
                        if (ct == 0)
                        {


                            string strupdate = " update FeatureProdDiscountMaster  set FeatureProdDiscountSchemeName='" + txtfeatureplan.Text + "'  " +

                                               " ,EndDate='" + txtEndDate.Text + "' ,Active='" + ddlstatus.SelectedValue + "' " +
                                               " where FeatureProdDiscountMasterID='" + Session["dk"] + "' ";
                            SqlCommand cmdupdate = new SqlCommand(strupdate, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmdupdate.ExecuteNonQuery();
                            con.Close();

                            Label1.Visible = true;
                            Label1.Text = "Record updated successfully";
                            clear();
                            imgupdate.Visible = false;
                            Button2.Visible = true;
                            pnladd.Visible = false;
                            btnadd.Visible = true;
                            PromotionSchemaFill();
                        }
                    }
                    
              
        }
        else
        {
            Label1.Visible = true;
            Label1.Text = "Record already exists";
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Label1.Text = "";
        clear();
    }
    protected void clear()
    {
        txtfeatureplan.Text = "";
        txtStartDate.Text = "";
        txtEndDate.Text = "";
        ddlstatus.SelectedIndex = 0;
        lblleg.Text = "";
        imgupdate.Visible = false;
        Button2.Visible = true;
        pnladd.Visible = false;
        btnadd.Visible = true;
    }
    protected void GrdOrdrQtyMaster_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        PromotionSchemaFill();
    }
    public string sortOrder
    {
        get
        {
            if ( Session["sortOrder"].ToString() == "desc")
            {
                 Session["sortOrder"] = "asc";
            }
            else
            {
                 Session["sortOrder"] = "desc";
            }

            return  Session["sortOrder"].ToString();
        }
        set
        {
             Session["sortOrder"] = value;
        }
    }
}