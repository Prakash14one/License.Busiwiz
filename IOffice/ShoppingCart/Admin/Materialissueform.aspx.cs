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

public partial class ShoppingCart_Admin_Materialissueform : System.Web.UI.Page
{
  //  SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    SqlConnection con=new SqlConnection(PageConn.connnn);
    public static string invtype="0";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        }
        //PageConn pgcon = new PageConn();
        //con = pgcon.dynconn;

        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };
      //  compid = Session["comid"].ToString();
        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();


        Page.Title = pg.getPageTitle(page);




        if (!Page.IsPostBack)
        {
            //Pagecontrol.dypcontrol(Page, page);

            ViewState["sortOrder"] = "";
            lblCompany.Text = Session["Cname"].ToString();
            lblBusiness.Text = "ALL";
            txtissuedate.Text = System.DateTime.Now.ToShortDateString();
            fillstore();
            filljob();
            fillref();
            Fillitem();
            FillGrid();
       
          
        }

       
    }

    protected void fillstore()
    {
        DataTable ds = ClsStore.SelectStorename();
        ddlStoreName.DataSource = ds;
        ddlStoreName.DataTextField = "Name";
        ddlStoreName.DataValueField = "WareHouseId";
        ddlStoreName.DataBind();
     
        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlStoreName.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }

        DropDownList1.DataSource = ds;
        DropDownList1.DataTextField = "Name";
        DropDownList1.DataValueField = "WareHouseId";
        DropDownList1.DataBind();
        DropDownList1.Items.Insert(0, "All");
 
    }
    protected void filljob()
    {
        string str = "select * from JobMaster where Whid='" + ddlStoreName.SelectedValue + "' order by JobName ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        ddlSelectJob.DataSource = ds;
        ddlSelectJob.DataTextField = "JobName";
        ddlSelectJob.DataValueField = "Id";
        ddlSelectJob.DataBind();
 
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        ViewState["tid"] = "00";
                  decimal WIPAvgcost = 0;
                    decimal WIPTotqty = 0;
                    int counter = 0;
        int fl = calc();
        if (fl != 1)
        {
            string strSelectMasterId1 = "select * from MaterialIssueMasterTbl where Whid='" + ddlStoreName.SelectedValue + "' and compid='" + Session["Comid"] + "' and JobMasterId='" + ddlSelectJob.SelectedValue + "' and ReferenceNo='" + txtRefernceNo.Text + "' ";
            SqlCommand cmdSelectMasterId1 = new SqlCommand(strSelectMasterId1, con);
            SqlDataAdapter adpSelectMasterId1 = new SqlDataAdapter(cmdSelectMasterId1);
            DataTable dtSelectMasterId1 = new DataTable();
            adpSelectMasterId1.Fill(dtSelectMasterId1);
            if (dtSelectMasterId1.Rows.Count > 0)
            {

                Label1.Text = "Record already exist";

            }
            else
            {
                string MasterIns = "Insert Into MaterialIssueMasterTbl(JobMasterId,ReferenceNo,Date,Note,Whid,compid) Values('" + ddlSelectJob.SelectedValue + "','" + txtRefernceNo.Text + "','" + txtissuedate.Text + "','" + txtNote.Text + "','" + ddlStoreName.SelectedValue + "','" + Session["Comid"].ToString() + "') ";
                SqlCommand cmdmaster = new SqlCommand(MasterIns, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdmaster.ExecuteNonQuery();
                con.Close();

                string str = "select max(Id) as Id from MaterialIssueMasterTbl where JobMasterId='" + ddlSelectJob.SelectedValue + "'";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                adp.Fill(ds);

                string str12 = "select * from JobMaster where Id='" + ddlSelectJob.SelectedValue + "'";
                SqlCommand cmd12 = new SqlCommand(str12, con);
                SqlDataAdapter adp12 = new SqlDataAdapter(cmd12);
                DataTable ds12 = new DataTable();
                adp12.Fill(ds12);

                Int32 JobWmasterId = Convert.ToInt32(ds12.Rows[0]["InvWMasterId"].ToString());

                Genetrans();

                foreach (GridViewRow gdr in GridView2.Rows)
                {
                  
                    
                    double FinalQtySub = 0;
                    Label lblqtyonhand = (Label)gdr.FindControl("lblqtyonhand");
                    Label lblcost123 = (Label)gdr.FindControl("lblcost123");
                    Label lbltotal123 = (Label)gdr.FindControl("lbltotal123");
                    TextBox txtqty123 = (TextBox)gdr.FindControl("txtqty123");
                    DropDownList ddlitem123 = (DropDownList)gdr.FindControl("ddlitem123");

                    if (ddlitem123.SelectedIndex > 0 && txtqty123.Text != "")
                    {
                        string MasterdetailIns = "Insert Into MaterialIssueDetail(InvWMasterId,Qty,MaterialMasterId,Rate) Values('" + ddlitem123.SelectedValue + "','" + txtqty123.Text + "','" + Convert.ToInt32(ds.Rows[0]["Id"].ToString()) + "','" + lblcost123.Text + "') ";
                        SqlCommand cmdmasterdetail = new SqlCommand(MasterdetailIns, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmdmasterdetail.ExecuteNonQuery();
                        con.Close();

                        FinalQtySub = Convert.ToDouble(txtqty123.Text);
                       // FinalQty = -(FinalQtySub);
                        WIPTotqty += Convert.ToDecimal(txtqty123.Text);
                      //  WIPAvgcost += Convert.ToDecimal(lblcost123.Text);
                        counter += 1;
                        inserSaleInv(FinalQtySub, ddlitem123.SelectedValue.ToString(), lblcost123, lblqtyonhand,ds.Rows[0]["Id"].ToString());
                      

                        //string AvgInsert1 = "Insert Into InventoryWarehouseMasterAvgCostTbl(InvWMasterId,AvgCost,DateUpdated,Qty,MaterialIssueMasterTblId,Rate) Values('" + JobWmasterId + "','" + 0 + "','" + txtissuedate.Text + "','" + txtqty123.Text + "','" + Convert.ToInt32(ds.Rows[0]["Id"].ToString()) + "','" + lblcost123.Text + "' )";
                        //SqlCommand cmdavd1 = new SqlCommand(AvgInsert1, con);
                        //if (con.State.ToString() != "Open")
                        //{
                        //    con.Open();
                        //}
                        //cmdavd1.ExecuteNonQuery();
                        //con.Close();
                    }

                }
                if (counter > 0)
                {
                    if (GridView2.Rows.Count > 0)
                    {
                        GridViewRow ft = (GridViewRow)GridView2.FooterRow;
                        Label lbltotalfooter = (Label)(ft.FindControl("lbltotalfooter"));

                        WIPAvgcost =Convert.ToDecimal(lbltotalfooter.Text)/WIPTotqty;
                        WIPAvgcost = Math.Round(WIPAvgcost, 2);
                        insertpurinv(Convert.ToDouble(WIPTotqty), JobWmasterId.ToString(), WIPAvgcost, ds.Rows[0]["Id"].ToString());
                    }
                }
                Label1.Text = "Record inserted successfully";
                FillGrid();

                fillref();
                clearall();
                lbllegend.Text = "New Issue of Material for Work Order";

                pnluper.Visible = false;
                pnldown.Visible = false;
                btnadd.Visible = true;
                lbllegend.Visible = false;

                clearlist();
            }
        }
        
    }
    protected void Genetrans()
    {
                SqlCommand cd3 = new SqlCommand("Sp_Insert_TranctionMasterRetIdentity", con);

                cd3.CommandType = CommandType.StoredProcedure;
                cd3.Parameters.AddWithValue("@Date", Convert.ToDateTime(txtissuedate.Text).ToShortDateString());
                cd3.Parameters.AddWithValue("@EntryNumber", Convert.ToInt32(0));
                cd3.Parameters.AddWithValue("@EntryTypeId", "0");
                cd3.Parameters.AddWithValue("@UserId", Session["userid"].ToString());
                cd3.Parameters.AddWithValue("@Tranction_Amount", Convert.ToDecimal(0));
                cd3.Parameters.AddWithValue("@whid", 0);

                cd3.Parameters.AddWithValue("@compid", Session["comid"]);


                cd3.Parameters.Add(new SqlParameter("@Tranction_Master_Id", SqlDbType.Int));
                cd3.Parameters["@Tranction_Master_Id"].Direction = ParameterDirection.Output;
                cd3.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
                cd3.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

             if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                int Id1 = (int)cd3.ExecuteNonQuery();
                Id1 = Convert.ToInt32(cd3.Parameters["@Tranction_Master_Id"].Value);
                con.Close();
                ViewState["tid"] = Id1;
                SqlCommand csd = new SqlCommand("Delete from TranctionMaster where Tranction_Master_Id='" + ViewState["tid"] + "'", con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                csd.ExecuteNonQuery();
                con.Close();
    }
    protected void inserSaleInv(Double FinalQtySub, string invwid, Label lblavgrate,Label lblqtyonhand,string materialissueid)
    {
        double FinalQty = 0;
        FinalQty = -(FinalQtySub);
        decimal Totalavgcost = Convert.ToDecimal(lblavgrate.Text);
        decimal Newqtyonhand = Convert.ToDecimal(lblqtyonhand.Text) - Convert.ToDecimal(FinalQtySub);
       // amtavgcost += Convert.ToDouble(lblavgrate.Text) * FinalQtySub;
        string insAvgCost = "INSERT INTO InventoryWarehouseMasterAvgCostTbl(InvWMasterId, AvgCost,DateUpdated,Qty,Tranction_Master_Id,QtyonHand,MaterialIssueMasterTblId) VALUES ('" + Convert.ToInt32(invwid) + "','" + Totalavgcost + "','" + txtissuedate.Text + "','" + FinalQty + "','" + ViewState["tid"] + "','" + Newqtyonhand + "','" + materialissueid + "')";

        SqlCommand cmd123123 = new SqlCommand(insAvgCost, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd123123.ExecuteNonQuery();
        con.Close();

        DataTable Dataupval = select("SELECT  QtyonHand,Rate,AvgCost,Qty,Tranction_Master_Id,IWMAvgCostId,DateUpdated FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + invwid + "' and DateUpdated>'" + txtissuedate.Text + "' order by DateUpdated Asc,Tranction_Master_Id Asc,IWMAvgCostId Asc");
        decimal changeTotalavgcost = Totalavgcost;
        decimal changeTotalonhand = Newqtyonhand;
        decimal Finalqtyhand = Newqtyonhand;
        foreach (DataRow itm in Dataupval.Rows)
        {
            string gupd = "";
            string gupd1 = "";
            string manul = "";
            if (Convert.ToString(itm["Rate"]) == "" && Convert.ToDecimal(itm["Qty"]) < 0)
            {

                decimal newamt = 0;
                DataTable drsv = select("Select AmountDebit from Tranction_Details where AccountDebit='8003' and Tranction_Master_Id='" + itm["Tranction_Master_Id"] + "' ");
                if (drsv.Rows.Count > 0)
                {
                    newamt = Convert.ToDecimal(drsv.Rows[0]["AmountDebit"]);
                }

                changeTotalavgcost = Math.Round(changeTotalavgcost, 2);
                changeTotalonhand = changeTotalonhand + Convert.ToDecimal(itm["Qty"]);
                Finalqtyhand = changeTotalonhand;
                decimal appn = (changeTotalavgcost) * ((-1) * Convert.ToDecimal(itm["Qty"]));
                decimal appold = Convert.ToDecimal(itm["AvgCost"]) * ((-1) * Convert.ToDecimal(itm["Qty"]));
                newamt = newamt + appn - (appold);
                newamt = Math.Round(newamt, 2);
                gupd = "Update Tranction_Details Set AmountDebit='" + newamt + "' where AccountDebit='8003' and Tranction_Master_Id='" + itm["Tranction_Master_Id"] + "'";
                gupd1 = "Update Tranction_Details Set AmountCredit='" + newamt + "' where AccountCredit='8000' and Tranction_Master_Id='" + itm["Tranction_Master_Id"] + "'";
                manul = "Update InventoryWarehouseMasterAvgCostTbl Set QtyonHand='" + changeTotalonhand + "',AvgCost='" + changeTotalavgcost + "' where IWMAvgCostId='" + itm["IWMAvgCostId"] + "'";
                SqlCommand cmdupcugs = new SqlCommand(gupd, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdupcugs.ExecuteNonQuery();
                con.Close();
                SqlCommand cmdupcugsin = new SqlCommand(gupd1, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdupcugsin.ExecuteNonQuery();
                con.Close();

            }
            else if (Convert.ToString(itm["Tranction_Master_Id"]) != "")
            {
                Finalqtyhand = changeTotalonhand + Convert.ToDecimal(itm["Qty"]);
                if (Finalqtyhand > 0)
                {
                    changeTotalavgcost = ((changeTotalavgcost * changeTotalonhand) + (Convert.ToDecimal(itm["Qty"]) * Convert.ToDecimal(itm["Rate"]))) / Finalqtyhand;
                }
                changeTotalonhand = changeTotalonhand + Convert.ToDecimal(itm["Qty"]);
                changeTotalavgcost = Math.Round(changeTotalavgcost, 2);
                manul = "Update InventoryWarehouseMasterAvgCostTbl Set QtyonHand='" + changeTotalonhand + "',AvgCost='" + changeTotalavgcost + "' where IWMAvgCostId='" + itm["IWMAvgCostId"] + "'";

            }
            if (manul.Length > 0)
            {
                SqlCommand cmdupinv = new SqlCommand(manul, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdupinv.ExecuteNonQuery();
                con.Close();
            }

        }

    }

    protected void insertpurinv(Double FinalQtySub, string invwid, decimal invrate, string materialissueid)
    {
        string id12 = invwid;

        string updateavgcos = "";
        decimal OLDavgcost = 0;
        decimal oLDqtyONHAND = 0;
        decimal Totalavgcost = 0;
        decimal Newqtyonhand = 0;
        DataTable drtinvdata = select("SELECT top(1) QtyonHand,Rate,AvgCost,Qty,DateUpdated,IWMAvgCostId FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + id12 + "' and DateUpdated<='" + txtissuedate.Text + "' order by DateUpdated Desc,Tranction_Master_Id Desc,IWMAvgCostId Desc ");

        if (drtinvdata.Rows.Count > 0)
        {
            if (Convert.ToString(drtinvdata.Rows[0]["AvgCost"]) != "")
            {
                OLDavgcost = Convert.ToDecimal(drtinvdata.Rows[0]["AvgCost"]);
            }
            if (Convert.ToString(drtinvdata.Rows[0]["QtyonHand"]) != "")
            {
                oLDqtyONHAND = Convert.ToDecimal(drtinvdata.Rows[0]["QtyonHand"]);
            }
        }
        decimal Finalqtyhand = 0;
        invrate = Math.Round(invrate, 2);
        Finalqtyhand = Convert.ToDecimal(FinalQtySub) + oLDqtyONHAND;
        if (Finalqtyhand > 0)
        {
            Totalavgcost = ((invrate * Convert.ToDecimal(FinalQtySub)) + (OLDavgcost * oLDqtyONHAND)) / Finalqtyhand;
        }
        Totalavgcost = Math.Round(Totalavgcost, 2);
        Newqtyonhand = Convert.ToDecimal(FinalQtySub) + oLDqtyONHAND;
        updateavgcos = "Insert into InventoryWarehouseMasterAvgCostTbl(InvWMasterId,Tranction_Master_Id,Qty,Rate,DateUpdated,AvgCost,QtyonHand,MaterialIssueMasterTblId)values('" + id12 + "','" + ViewState["tid"] + "','" + FinalQtySub + "','" + invrate + "','" + txtissuedate.Text + "','" + Totalavgcost + "','" + Newqtyonhand + "','" + materialissueid + "')";
        SqlCommand cmavgcost = new SqlCommand(updateavgcos, con);
    
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmavgcost.ExecuteNonQuery();
        con.Close();

        DataTable Dataupval = select("SELECT  QtyonHand,Rate,AvgCost,Qty,Tranction_Master_Id,IWMAvgCostId,DateUpdated FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + id12 + "' and DateUpdated>'" + txtissuedate.Text + "' order by DateUpdated Asc,Tranction_Master_Id Asc,IWMAvgCostId Asc");
        decimal changeTotalavgcost = Totalavgcost;
        decimal changeTotalonhand = Newqtyonhand;

        foreach (DataRow itm in Dataupval.Rows)
        {
            string gupd = "";
            string gupd1 = "";
            string manul = "";
           
            if (Convert.ToString(itm["Rate"]) == "" && Convert.ToDecimal(itm["Qty"]) < 0)
            {
                decimal newamt = 0;
                DataTable drsv = select("Select AmountDebit from Tranction_Details where AccountDebit='8003' and Tranction_Master_Id='" + itm["Tranction_Master_Id"] + "' ");
                if (drsv.Rows.Count > 0)
                {
                    newamt = Convert.ToDecimal(drsv.Rows[0]["AmountDebit"]);
                }

                changeTotalavgcost = Math.Round(changeTotalavgcost, 2);
                changeTotalonhand = changeTotalonhand + Convert.ToDecimal(itm["Qty"]);
                Finalqtyhand = changeTotalonhand;
                decimal appn = (changeTotalavgcost) * ((-1) * Convert.ToDecimal(itm["Qty"]));
                decimal appold = Convert.ToDecimal(itm["AvgCost"]) * ((-1) * Convert.ToDecimal(itm["Qty"]));
                newamt = newamt + appn - (appold);
                newamt = Math.Round(newamt, 2);

                gupd = "Update Tranction_Details Set AmountDebit='" + newamt + "' where AccountDebit='8003' and Tranction_Master_Id='" + itm["Tranction_Master_Id"] + "'";
                gupd1 = "Update Tranction_Details Set AmountCredit='" + newamt + "' where AccountCredit='8000' and Tranction_Master_Id='" + itm["Tranction_Master_Id"] + "'";
                manul = "Update InventoryWarehouseMasterAvgCostTbl Set QtyonHand='" + changeTotalonhand + "',AvgCost='" + changeTotalavgcost + "' where IWMAvgCostId='" + itm["IWMAvgCostId"] + "'";
                SqlCommand cmdupcugs = new SqlCommand(gupd, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdupcugs.ExecuteNonQuery();
                con.Close();
                SqlCommand cmdupcugsin = new SqlCommand(gupd1, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdupcugsin.ExecuteNonQuery();
                con.Close();

            }
            else if (Convert.ToString(itm["Tranction_Master_Id"]) != "")
            {
                Finalqtyhand = changeTotalonhand + Convert.ToDecimal(itm["Qty"]);
                if (Finalqtyhand > 0)
                {
                    changeTotalavgcost = ((changeTotalavgcost * changeTotalonhand) + (Convert.ToDecimal(itm["Qty"]) * Convert.ToDecimal(itm["Rate"]))) / Finalqtyhand;
                }
                changeTotalonhand = changeTotalonhand + Convert.ToDecimal(itm["Qty"]);
                changeTotalavgcost = Math.Round(changeTotalavgcost, 2);
                manul = "Update InventoryWarehouseMasterAvgCostTbl Set QtyonHand='" + changeTotalonhand + "',AvgCost='" + changeTotalavgcost + "' where IWMAvgCostId='" + itm["IWMAvgCostId"] + "'";

            }
            if (manul.Length > 0)
            {
                SqlCommand cmdupinv = new SqlCommand(manul, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdupinv.ExecuteNonQuery();
                con.Close();
            }
            //transaction.Commit();
            //con.Close();
        }
    }
    public DataTable CreateDatatable()
    {
        DataTable dtTemp = new DataTable();
        DataColumn prd1 = new DataColumn();
        prd1.ColumnName = "Id";
        prd1.DataType = System.Type.GetType("System.String");
        prd1.AllowDBNull = true;
        dtTemp.Columns.Add(prd1);

        DataColumn prd12 = new DataColumn();
        prd12.ColumnName = "InvWMasterId";
        prd12.DataType = System.Type.GetType("System.String");
        prd12.AllowDBNull = true;
        dtTemp.Columns.Add(prd12);

        DataColumn prd122 = new DataColumn();
        prd122.ColumnName = "Qty";
        prd122.DataType = System.Type.GetType("System.String");
        prd122.AllowDBNull = true;
        dtTemp.Columns.Add(prd122);

        DataColumn prd123 = new DataColumn();
        prd123.ColumnName = "MaterialMasterId";
        prd123.DataType = System.Type.GetType("System.String");
        prd123.AllowDBNull = true;
        dtTemp.Columns.Add(prd123);

        DataColumn prd124 = new DataColumn();
        prd124.ColumnName = "Rate";
        prd124.DataType = System.Type.GetType("System.String");
        prd124.AllowDBNull = true;
        dtTemp.Columns.Add(prd124);


        DataColumn prd1241 = new DataColumn();
        prd1241.ColumnName = "Total";
        prd1241.DataType = System.Type.GetType("System.String");
        prd1241.AllowDBNull = true;
        dtTemp.Columns.Add(prd1241);
        return dtTemp;
    }
    protected void Fillitem()
    {


        DataTable dtTemp = new DataTable();
        dtTemp = CreateDatatable();


        for (int i = 1; i <= 10; i++)
        {
            DataRow dtadd = dtTemp.NewRow();
            dtadd["Id"] = i.ToString();
            dtadd["InvWMasterId"] = i.ToString();
            dtadd["Qty"] = "";
            dtadd["MaterialMasterId"] = "";
            dtadd["Rate"] = "";
            dtadd["Total"] = "";
            dtTemp.Rows.Add(dtadd);
        }
        GridView2.DataSource = dtTemp;
        GridView2.DataBind();
       
       DataTable ds =select( "SELECT   InventoryWarehouseMasterTbl.InventoryWarehouseMasterId ,  LEFT(InventoryCategoryMaster.InventoryCatName, 8) + ' : ' + LEFT(InventorySubCategoryMaster.InventorySubCatName, 8) " +
                "     + ' : ' + LEFT(InventoruSubSubCategory.InventorySubSubName, 8) + ' : ' + InventoryMaster.Name + ' : '+InventoryMaster.ProductNo + ' : '+Cast(InventoryWarehouseMasterTbl.Weight as nvarchar)+ ' : '+UnitTypeMaster.Name AS Name, InventoryMaster.InventoryMasterId " +
             "  FROM         InventoryCategoryMaster RIGHT OUTER JOIN " +
              "      InventorySubCategoryMaster ON InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId LEFT OUTER JOIN " +
              "      InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID RIGHT OUTER JOIN " +
              "      InventoryMaster ON InventoruSubSubCategory.InventorySubSubId = InventoryMaster.InventorySubSubId inner join InventoryWarehouseMasterTbl  on InventoryWarehouseMasterTbl.InventoryMasterId=InventoryMaster.InventoryMasterId  Inner join  UnitTypeMaster on " +
                        " UnitTypeMaster.UnitTypeId =InventoryWarehouseMasterTbl.UnitTypeId  " +
            " WHERE  (InventoryWarehouseMasterTbl.WareHouseId='" + ddlStoreName.SelectedValue + "') and (InventoryCategoryMaster.CatType IS NULL)  and  (InventoryMaster.MasterActiveStatus = 1) " +
            " and (InventoryWarehouseMasterTbl.InventoryWarehouseMasterId Not in(select InvWMasterId from JobMaster where Id='" + ddlSelectJob.SelectedValue + "')) order   by InventoryCategoryMaster.InventoryCatName,InventorySubCategoryMaster.InventorySubCatName,InventoruSubSubCategory.InventorySubSubName");
     
        foreach (GridViewRow item in GridView2.Rows)
        {

            DropDownList ddlitem123 = (DropDownList)item.FindControl("ddlitem123");


            ddlitem123.DataSource = ds;
            ddlitem123.DataTextField = "Name";
            ddlitem123.DataValueField = "InventoryWarehouseMasterId";
            ddlitem123.DataBind();
            ddlitem123.Items.Insert(0, "-Select-");
            ddlitem123.Items[0].Value = "0";
            ddlitem123.Enabled = true;
        }



       
        
    }


    protected void Button7_Click(object sender, EventArgs e)
    {
        int fl = calc();
    }
    protected int calc()
    {
        int fl = 0;
        double temp = 0;

        foreach (GridViewRow gdr in GridView2.Rows)
        {
           
            Label lblqtyonhand = (Label)gdr.FindControl("lblqtyonhand");
            Label lblredmask = (Label)gdr.FindControl("lblredmask");
            Label lblcost123 = (Label)gdr.FindControl("lblcost123");
            TextBox txtqty123 = (TextBox)gdr.FindControl("txtqty123");
            DropDownList ddlitem123 = (DropDownList)gdr.FindControl("ddlitem123");

            if (ddlitem123.SelectedIndex > 0)
            {

                avgcost(ddlitem123, lblcost123, lblqtyonhand);
            }

            if (ddlitem123.SelectedIndex > 0 && lblcost123.Text != "" && txtqty123.Text != "")
            {
                if (lblqtyonhand.Text == "")
                {
                    lblqtyonhand.Text = "0";
                }
                if (Convert.ToDouble(lblqtyonhand.Text) < Convert.ToDouble(txtqty123.Text))
                {
                    fl = 1;
                    lblredmask.Visible = true;
                }
                else
                {
                    lblredmask.Visible = false;
                }
            }
        }
        if (fl != 1)
        {

            foreach (GridViewRow gdr in GridView2.Rows)
            {
                double item1 = 0;
                Label InvWMasterId123 = (Label)gdr.FindControl("InvWMasterId123");
                Label lblcost123 = (Label)gdr.FindControl("lblcost123");
                Label lbltotal123 = (Label)gdr.FindControl("lbltotal123");
                Label lblqtyonhand = (Label)gdr.FindControl("lblqtyonhand");
                TextBox txtqty123 = (TextBox)gdr.FindControl("txtqty123");
                DropDownList ddlitem123 = (DropDownList)gdr.FindControl("ddlitem123");

                if (ddlitem123.SelectedIndex > 0 && lblcost123.Text != "" && txtqty123.Text != "")
                {
                    item1 = Convert.ToDouble(lblcost123.Text) * Convert.ToDouble(txtqty123.Text);
                    if (Convert.ToString(ViewState["Id"]) != "")
                    {
                        Button3.Visible = false;
                        btn_update.Visible = true;
                    }
                    else
                    {
                        Button3.Visible = true;
                        btn_update.Visible = false;
                    }
                }
                lbltotal123.Text = item1.ToString();
                temp += item1;

            }
            if (GridView2.Rows.Count > 0)
            {
                GridViewRow ft = (GridViewRow)GridView2.FooterRow;
                Label lbltotalfooter = (Label)(ft.FindControl("lbltotalfooter"));
                lbltotalfooter.Text = temp.ToString();


            }
            //Label23.Text = temp.ToString();
        }
        else
        {
            Label1.Text = "Issued Qty must be less than Qty on hand.";
        }
        return fl;
    }

    protected void ddlitem123_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((DropDownList)sender).Parent.Parent as GridViewRow;

        int rinrow = row.RowIndex;

        DropDownList ddlitem123 = (DropDownList)(GridView2.Rows[rinrow].FindControl("ddlitem123"));
        Label lblqtyonhand = (Label)(GridView2.Rows[rinrow].FindControl("lblqtyonhand"));
        Label lblcost123 = (Label)(GridView2.Rows[rinrow].FindControl("lblcost123"));


        avgcost(ddlitem123, lblcost123, lblqtyonhand);
    }
   
    protected DataTable select(string qu)
    {
        SqlCommand cmd = new SqlCommand(qu, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        return dt;

    }
   
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
       // avgcost(DropDownList2, Label5);
    }
    protected void ddlStoreName_SelectedIndexChanged(object sender, EventArgs e)
    {
        Fillitem();
        filljob();
    }
    protected void fillref()
    {
        string strSelectMasterId1 = "select top(1) ReferenceNo from MaterialIssueMasterTbl order by Id Desc";
        SqlCommand cmdSelectMasterId1 = new SqlCommand(strSelectMasterId1, con);
        SqlDataAdapter adpSelectMasterId1 = new SqlDataAdapter(cmdSelectMasterId1);
        DataTable dtSelectMasterId1 = new DataTable();
        adpSelectMasterId1.Fill(dtSelectMasterId1);
        if (dtSelectMasterId1.Rows.Count > 0)
        {
            try
            {
                txtRefernceNo.Text =( Convert.ToInt32(dtSelectMasterId1.Rows[0]["ReferenceNo"]) + 1).ToString();
            }
            catch
            {
                txtRefernceNo.Text = "1234";
            }

        }
        else 
        {
            txtRefernceNo.Text = "123";
        }
    }
    protected void FillGrid()
    {
        string str11="";
        lblBusiness.Text = DropDownList1.SelectedItem.Text;
        if (DropDownList1.SelectedIndex > 0)
        {
           
            str11 = "select MaterialIssueMasterTbl.*,WareHouseMaster.Name as WName,JobMaster.JobName  from MaterialIssueMasterTbl inner join JobMaster on JobMaster.Id=MaterialIssueMasterTbl.JobMasterId inner join WareHouseMaster on WareHouseMaster.WareHouseId=MaterialIssueMasterTbl.Whid where MaterialIssueMasterTbl.compid='" + Session["Comid"].ToString() + "'  and MaterialIssueMasterTbl.Whid='"+DropDownList1.SelectedValue+"'";
        }
        else
        {
            str11 = "select MaterialIssueMasterTbl.*,WareHouseMaster.Name as WName,JobMaster.JobName  from MaterialIssueMasterTbl inner join JobMaster on JobMaster.Id=MaterialIssueMasterTbl.JobMasterId inner join WareHouseMaster on WareHouseMaster.WareHouseId=MaterialIssueMasterTbl.Whid where MaterialIssueMasterTbl.compid='" + Session["Comid"].ToString() + "' ";
        }
        SqlCommand cmd1451 = new SqlCommand(str11, con);
        SqlDataAdapter adp1451 = new SqlDataAdapter(cmd1451);
        DataTable ds1451 = new DataTable();
        adp1451.Fill(ds1451);
        if (ds1451.Rows.Count > 0)
        {

            DataView myDataView = new DataView();
            myDataView = ds1451.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }



            GridView1.DataSource = myDataView;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();

        }
 
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
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable dtsx = select("Select * from InventoryWarehouseMasterAvgCostTbl where MaterialIssueMasterTblId='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "'");
        if (dtsx.Rows.Count > 0)
        {
            ViewState["sdo1"] = Convert.ToDateTime(dtsx.Rows[0]["DateUpdated"]).ToShortDateString();
            ViewState["tra"] = Convert.ToString(dtsx.Rows[0]["Tranction_Master_Id"]);
        }
        string st2 = "Delete from MaterialIssueMasterTbl where Id='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "' ";
        SqlCommand cmd2 = new SqlCommand(st2, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd2.ExecuteNonQuery();
        con.Close();

        string st212 = "Delete from InventoryWarehouseMasterAvgCostTbl where MaterialIssueMasterTblId='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "' ";
        SqlCommand cmd212 = new SqlCommand(st212, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd212.ExecuteNonQuery();
        con.Close();


        string stmaterialdetail = "Delete from MaterialIssueDetail where MaterialMasterId='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "' ";
        SqlCommand cmdmaterialdetail = new SqlCommand(stmaterialdetail, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmdmaterialdetail.ExecuteNonQuery();
        con.Close();
        if (Convert.ToString(ViewState["sdo1"]) != "")
        {
            DELAVGCOST(ViewState["tra"].ToString());
        }
        GridView1.EditIndex = -1;
        FillGrid();

      
        Label1.Text = "Record deleted successfully";
    }
    protected void DELAVGCOST(string traid)
    {
        DataTable Datfi = select("SELECT IWMAvgCostId,InvWMasterId  FROM  InventoryWarehouseMasterAvgCostTbl where    Tranction_Master_Id='" + traid + "'");
        foreach (DataRow intvay in Datfi.Rows)
        {
            string updel = "Delete from  InventoryWarehouseMasterAvgCostTbl where IWMAvgCostId='" + intvay["IWMAvgCostId"] + "'";
            SqlCommand cmdels = new SqlCommand(updel, con);

            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdels.ExecuteNonQuery();
            con.Close();

            decimal OLDavgcost = 0;
            decimal oLDqtyONHAND = 0;
            decimal Totalavgcost = 0;
            decimal Newqtyonhand = 0;
            DataTable drtinvdata = new DataTable();
            drtinvdata = select("SELECT top(1) QtyonHand,Rate,AvgCost,Qty,DateUpdated,IWMAvgCostId FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + intvay["InvWMasterId"] + "' and DateUpdated<='" + ViewState["sdo1"] + "' and Tranction_Master_Id<'" + traid + "'  order by DateUpdated Desc,Tranction_Master_Id Desc,IWMAvgCostId Desc ");
            if (drtinvdata.Rows.Count == 0)
            {
                drtinvdata = select("SELECT top(1) QtyonHand,Rate,AvgCost,Qty,DateUpdated,IWMAvgCostId FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + intvay["InvWMasterId"] + "' and DateUpdated<'" + ViewState["sdo1"] + "'   order by DateUpdated Desc,Tranction_Master_Id Desc,IWMAvgCostId Desc ");

            }
            if (drtinvdata.Rows.Count > 0)
            {
                if (Convert.ToString(drtinvdata.Rows[0]["AvgCost"]) != "")
                {
                    OLDavgcost = Convert.ToDecimal(drtinvdata.Rows[0]["AvgCost"]);
                }
                if (Convert.ToString(drtinvdata.Rows[0]["QtyonHand"]) != "")
                {
                    oLDqtyONHAND = Convert.ToDecimal(drtinvdata.Rows[0]["QtyonHand"]);
                }
            }
            decimal Finalqtyhand = 0;

            Finalqtyhand = oLDqtyONHAND;
            Totalavgcost = OLDavgcost;
            Totalavgcost = Math.Round(Totalavgcost, 2);
            Newqtyonhand = oLDqtyONHAND;

            DataTable Dataupval = select("SELECT  QtyonHand,Rate,AvgCost,Qty,Tranction_Master_Id,IWMAvgCostId,DateUpdated FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + intvay["InvWMasterId"] + "' and DateUpdated>='" + ViewState["sdo1"] + "' order by DateUpdated Asc,Tranction_Master_Id Asc,IWMAvgCostId Asc");
            decimal changeTotalavgcost = Totalavgcost;
            decimal changeTotalonhand = Newqtyonhand;
            string ABC = "";
            foreach (DataRow itm in Dataupval.Rows)
            {
                string gupd = "";
                string gupd1 = "";
                string manul = "";

                if (ABC == "")
                {
                    if (Convert.ToDateTime(itm["DateUpdated"]) == Convert.ToDateTime(ViewState["sdo1"]))
                    {
                        if (Convert.ToDecimal(itm["Tranction_Master_Id"]) > Convert.ToDecimal(traid))
                        {
                            ABC = "13";
                        }
                    }
                    else
                    {
                        ABC = "12";
                    }
                }
                if (ABC != "")
                {


                    if (Convert.ToString(itm["Rate"]) == "" && Convert.ToDecimal(itm["Qty"]) < 0)
                    {



                        decimal newamt = 0;
                        DataTable drsv = select("Select AmountDebit from Tranction_Details where AccountDebit='8003' and Tranction_Master_Id='" + itm["Tranction_Master_Id"] + "' ");
                        if (drsv.Rows.Count > 0)
                        {
                            newamt = Convert.ToDecimal(drsv.Rows[0]["AmountDebit"]);
                        }

                        changeTotalavgcost = Math.Round(changeTotalavgcost, 2);
                        changeTotalonhand = changeTotalonhand + Convert.ToDecimal(itm["Qty"]);
                        Finalqtyhand = changeTotalonhand;
                        decimal appn = (changeTotalavgcost) * ((-1) * Convert.ToDecimal(itm["Qty"]));
                        decimal appold = Convert.ToDecimal(itm["AvgCost"]) * ((-1) * Convert.ToDecimal(itm["Qty"]));
                        newamt = newamt + appn - (appold);
                        newamt = Math.Round(newamt, 2);

                        gupd = "Update Tranction_Details Set AmountDebit='" + newamt + "' where AccountDebit='8003' and Tranction_Master_Id='" + itm["Tranction_Master_Id"] + "'";
                        gupd1 = "Update Tranction_Details Set AmountCredit='" + newamt + "' where AccountCredit='8000' and Tranction_Master_Id='" + itm["Tranction_Master_Id"] + "'";
                        manul = "Update InventoryWarehouseMasterAvgCostTbl Set QtyonHand='" + changeTotalonhand + "',AvgCost='" + changeTotalavgcost + "' where IWMAvgCostId='" + itm["IWMAvgCostId"] + "'";
                        SqlCommand cmdupcugs = new SqlCommand(gupd, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmdupcugs.ExecuteNonQuery();
                        con.Close();
                        SqlCommand cmdupcugsin = new SqlCommand(gupd1, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmdupcugsin.ExecuteNonQuery();
                        con.Close();

                    }
                    else if (Convert.ToString(itm["Tranction_Master_Id"]) != "")
                    {
                        Finalqtyhand = changeTotalonhand + Convert.ToDecimal(itm["Qty"]);
                        if (Finalqtyhand > 0)
                        {
                            changeTotalavgcost = ((changeTotalavgcost * changeTotalonhand) + (Convert.ToDecimal(itm["Qty"]) * Convert.ToDecimal(itm["Rate"]))) / Finalqtyhand;
                        }
                        changeTotalonhand = changeTotalonhand + Convert.ToDecimal(itm["Qty"]);
                        changeTotalavgcost = Math.Round(changeTotalavgcost, 2);
                        manul = "Update InventoryWarehouseMasterAvgCostTbl Set QtyonHand='" + changeTotalonhand + "',AvgCost='" + changeTotalavgcost + "' where IWMAvgCostId='" + itm["IWMAvgCostId"] + "'";

                    }
                    if (manul.Length > 0)
                    {
                        SqlCommand cmdupinv = new SqlCommand(manul, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmdupinv.ExecuteNonQuery();
                        con.Close();
                    }
                   
                }
            }
        }
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }
    protected void LinkButton4_Click(object sender, ImageClickEventArgs e)
    {

      //  pnladdmaster.Visible = false;
       // pnldown.Visible = true;

        ImageButton lk = (ImageButton)sender;
        int j = Convert.ToInt32(lk.CommandArgument);
        ViewState["Id"] = j;
        Session["TimeId"] = j;

        string str = "select * from MaterialIssueMasterTbl where Id='" + j + "'";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        adp.Fill(dt);
        ViewState["tid"]="00";
        DataTable dts=select("select * from InventoryWarehouseMasterAvgCostTbl where MaterialIssueMasterTblId='"+j +"'");
        if(dts.Rows.Count>0)
        {
            ViewState["tid"] = Convert.ToString(dts.Rows[0]["Tranction_Master_Id"]);
        }

       // fillstore();
        ddlStoreName.SelectedIndex = ddlStoreName.Items.IndexOf(ddlStoreName.Items.FindByValue(dt.Rows[0]["Whid"].ToString()));
        Fillitem();
        filljob();
        ddlSelectJob.SelectedIndex = ddlSelectJob.Items.IndexOf(ddlSelectJob.Items.FindByValue(dt.Rows[0]["JobMasterId"].ToString()));
        
       
        txtRefernceNo.Text = dt.Rows[0]["ReferenceNo"].ToString();
        txtNote.Text = dt.Rows[0]["Note"].ToString();
        txtissuedate.Text = Convert.ToDateTime(dt.Rows[0]["Date"]).ToShortDateString();
        ViewState["sdo"] = Convert.ToDateTime(dt.Rows[0]["Date"]).ToShortDateString();
        ViewState["sdo1"] = Convert.ToDateTime(dt.Rows[0]["Date"]).ToShortDateString();
        string str12 = "select * from MaterialIssueDetail where MaterialMasterId='" + j + "'";
        SqlCommand cmd12 = new SqlCommand(str12, con);
        SqlDataAdapter adp12 = new SqlDataAdapter(cmd12);
        DataSet ds12 = new DataSet();
        DataTable dt12 = new DataTable();
        adp12.Fill(dt12);

        GridView2.DataSource = dt12;
        GridView2.DataBind();

        double temp = 0;
        string str123123 = "SELECT   InventoryWarehouseMasterTbl.InventoryWarehouseMasterId ,  LEFT(InventoryCategoryMaster.InventoryCatName, 8) + ' : ' + LEFT(InventorySubCategoryMaster.InventorySubCatName, 8) " +
                 "     + ' : ' + LEFT(InventoruSubSubCategory.InventorySubSubName, 8) + ' : ' + InventoryMaster.Name AS Name, InventoryMaster.InventoryMasterId " +
              "  FROM         InventoryCategoryMaster RIGHT OUTER JOIN " +
               "      InventorySubCategoryMaster ON InventoryCategoryMaster.InventeroyCatId = InventorySubCategoryMaster.InventoryCategoryMasterId Inner join " +
               "      InventoruSubSubCategory ON InventorySubCategoryMaster.InventorySubCatId = InventoruSubSubCategory.InventorySubCatID Inner join " +
               "      InventoryMaster ON InventoruSubSubCategory.InventorySubSubId = InventoryMaster.InventorySubSubId inner join InventoryWarehouseMasterTbl  on InventoryWarehouseMasterTbl.InventoryMasterId=InventoryMaster.InventoryMasterId " +
             " WHERE  (InventoryWarehouseMasterTbl.WareHouseId='" + ddlStoreName.SelectedValue + "') and (InventoryCategoryMaster.CatType IS NULL) and  (InventoryMaster.MasterActiveStatus = 1) and (InventoryWarehouseMasterTbl.InventoryWarehouseMasterId Not in(select InvWMasterId from JobMaster where Id='" + ddlSelectJob.SelectedValue + "')) order by InventoryCategoryMaster.InventoryCatName,InventorySubCategoryMaster.InventorySubCatName,InventoruSubSubCategory.InventorySubSubName";
        SqlCommand cmd123123 = new SqlCommand(str123123, con);
        SqlDataAdapter adp123123 = new SqlDataAdapter(cmd123123);
        DataSet ds123123 = new DataSet();
        adp123123.Fill(ds123123);
        foreach (GridViewRow gdr in GridView2.Rows)
        {
            Label InvWMasterId123 = (Label)gdr.FindControl("InvWMasterId123");
            Label lblcost123 = (Label)gdr.FindControl("lblcost123");
            Label lbltotal123 = (Label)gdr.FindControl("lbltotal123");
            Label lblsrno = (Label)gdr.FindControl("lblsrno");
            Label lblqtyonhand = (Label)gdr.FindControl("lblqtyonhand");
         
            TextBox txtqty123 = (TextBox)gdr.FindControl("txtqty123");
            DropDownList ddlitem123 = (DropDownList)gdr.FindControl("ddlitem123");
            lblsrno.Text = (gdr.RowIndex + 1).ToString();
            
            


            ddlitem123.DataSource = ds123123;
            ddlitem123.DataTextField = "Name";
            ddlitem123.DataValueField = "InventoryWarehouseMasterId";
            ddlitem123.DataBind();
            ddlitem123.Enabled = false;
            ddlitem123.SelectedIndex = ddlitem123.Items.IndexOf(ddlitem123.Items.FindByValue(InvWMasterId123.Text));
            avgcost(ddlitem123, lblcost123, lblqtyonhand);
            //lblqtyonhand.Text =(Convert.ToDouble(lblqtyonhand.Text) + Convert.ToDouble(txtqty123.Text)).ToString();
            double item1 = Convert.ToDouble(lblcost123.Text) * Convert.ToDouble(txtqty123.Text);
            temp += item1;
            lbltotal123.Text = item1.ToString();
        }

      
       if (GridView2.Rows.Count > 0)
       {
           GridViewRow ft = (GridViewRow)GridView2.FooterRow;
           Label lbltotalfooter = (Label)(ft.FindControl("lbltotalfooter"));
           lbltotalfooter.Text = temp.ToString();
       }
        Button3.Visible = false;
        pnldown.Visible = true;
      
        pnluper.Visible = true;
        lbllegend.Visible = true;
        btnadd.Visible = false;

        lbllegend.Text = "Edit Issue of Material for Work Order";


    }
    protected void avgcost(DropDownList abc,Label avgcost,Label onhand)
    {


        DataTable drtinvdata = select("SELECT top(1) QtyonHand,Rate,AvgCost,Qty,DateUpdated,IWMAvgCostId FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + abc.SelectedValue + "' and DateUpdated<='" + txtissuedate.Text + "' order by DateUpdated Desc,Tranction_Master_Id Desc,IWMAvgCostId Desc ");

        if (drtinvdata.Rows.Count > 0)
        {
            avgcost.Text = Convert.ToString(drtinvdata.Rows[0]["AvgCost"]);
           
            onhand.Text = Convert.ToString(drtinvdata.Rows[0]["QtyonHand"]);
            if (onhand.Text == "")
            {
                onhand.Text = "0";
            }
        }
        else
        {
            avgcost.Text = "0";
            onhand.Text = "0";
        }
        
    }
    protected void btn_update_Click(object sender, EventArgs e)
    {
        int abcd = 0;
        int fl = calc();
         invtype = "0";
        decimal WIPAvgcost = 0;
        decimal WIPTotqty = 0;
        int counter = 0;
        if (fl != 1)
        {
            if (Convert.ToDateTime(ViewState["sdo"]) == Convert.ToDateTime(txtissuedate.Text))
            {
                invtype = "1";
                ViewState["sdo"] = txtissuedate.Text;
            }
            else if (Convert.ToDateTime(ViewState["sdo"]) >= Convert.ToDateTime(txtissuedate.Text))
            {
                invtype = "2";
                ViewState["sdo"] = txtissuedate.Text;
            }
            else
            {
                invtype = "3";

            }
            string strSelectMasterId1 = "select * from MaterialIssueMasterTbl where Whid='" + ddlStoreName.SelectedValue + "' and compid='" + Session["Comid"] + "' and JobMasterId='" + ddlSelectJob.SelectedValue + "' and ReferenceNo='" + txtRefernceNo.Text + "' and Id<>'" + ViewState["Id"] + "' ";
            SqlCommand cmdSelectMasterId1 = new SqlCommand(strSelectMasterId1, con);
            SqlDataAdapter adpSelectMasterId1 = new SqlDataAdapter(cmdSelectMasterId1);
            DataTable dtSelectMasterId1 = new DataTable();
            adpSelectMasterId1.Fill(dtSelectMasterId1);
            if (dtSelectMasterId1.Rows.Count > 0)
            {

                Label1.Text = "Record already exist";

            }
            else
            {


                foreach (GridViewRow gdr in GridView2.Rows)
                {
                    double FinalQtySub = 0;
                    double FinalQty = 0;
                    Label InvWMasterId123 = (Label)gdr.FindControl("InvWMasterId123");
                    Label lblcost123 = (Label)gdr.FindControl("lblcost123");
                    Label lbltotal123 = (Label)gdr.FindControl("lbltotal123");
                    TextBox txtqty123 = (TextBox)gdr.FindControl("txtqty123");
                    DropDownList ddlitem123 = (DropDownList)gdr.FindControl("ddlitem123");
                    Label lblmasterid = (Label)gdr.FindControl("lblmasterid");

                    FinalQtySub = Convert.ToDouble(txtqty123.Text);
                    FinalQty = -(FinalQtySub);

                    WIPTotqty += Convert.ToDecimal(txtqty123.Text);

                    abcd = FillCountQtyUp(ViewState["tid"].ToString(), InvWMasterId123.Text, txtqty123.Text, invtype);
                    if (abcd == 0)
                    {
                        if (abcd == 1)
                        {
                            lbllegend.Visible = true;
                            lbllegend.Text = "You cannot delete this entry as it may result into negative quantity in this item because of sale entry made after the date of this invoice";
                            break;
                        }
                    }

                }
                string str12 = "select * from JobMaster  where Id='" + ddlSelectJob.SelectedValue + "'";
                SqlCommand cmd12 = new SqlCommand(str12, con);
                SqlDataAdapter adp12 = new SqlDataAdapter(cmd12);
                DataTable ds12 = new DataTable();
                adp12.Fill(ds12);

                Int32 JobWmasterId = Convert.ToInt32(ds12.Rows[0]["InvWMasterId"].ToString());


                abcd = FillCountQtyUp(ViewState["tid"].ToString(), JobWmasterId.ToString(), WIPTotqty.ToString(), invtype);
         
                if (abcd == 0)
                {
                    if (abcd == 1)
                    {
                        lbllegend.Visible = true;
                        lbllegend.Text = "You cannot delete this entry as it may result into negative quantity in this item because of sale entry made after the date of this invoice";

                    }
                }
                 if (abcd == 0)
                 {
                     string Update = "Update MaterialIssueMasterTbl set JobMasterId='" + ddlSelectJob.SelectedValue + "',ReferenceNo='" + txtRefernceNo.Text + "',Date='" + txtissuedate.Text + "',Note='" + txtNote.Text + "',Whid='" + ddlStoreName.SelectedValue + "' Where Id='" + ViewState["Id"] + "'";

                     SqlCommand cmdupdate = new SqlCommand(Update, con);
                     if (con.State.ToString() != "Open")
                     {
                         con.Open();
                     }
                     cmdupdate.ExecuteNonQuery();
                     con.Close();


                    
                     WIPAvgcost = 0;
                     WIPTotqty = 0;
                     counter = 0;
                     foreach (GridViewRow gdr in GridView2.Rows)
                     {
                         double FinalQtySub = 0;
                         double FinalQty = 0;
                         Label InvWMasterId123 = (Label)gdr.FindControl("InvWMasterId123");
                         Label lblcost123 = (Label)gdr.FindControl("lblcost123");
                         Label lbltotal123 = (Label)gdr.FindControl("lbltotal123");
                         TextBox txtqty123 = (TextBox)gdr.FindControl("txtqty123");
                         DropDownList ddlitem123 = (DropDownList)gdr.FindControl("ddlitem123");
                         Label lblmasterid = (Label)gdr.FindControl("lblmasterid");

                         string MasterdetailIns = "Update  MaterialIssueDetail set InvWMasterId='" + ddlitem123.SelectedValue + "',Qty='" + txtqty123.Text + "',Rate='" + lblcost123.Text + "' where Id='" + lblmasterid.Text + "'";
                         SqlCommand cmdmasterdetail = new SqlCommand(MasterdetailIns, con);
                         if (con.State.ToString() != "Open")
                         {
                             con.Open();
                         }
                         cmdmasterdetail.ExecuteNonQuery();
                         con.Close();

                         FinalQtySub = Convert.ToDouble(txtqty123.Text);
                         FinalQty = -(FinalQtySub);



                         // FinalQty = -(FinalQtySub);
                         WIPTotqty += Convert.ToDecimal(txtqty123.Text);
                        // WIPAvgcost += Convert.ToDecimal(lblcost123.Text);
                         //counter += 1;
                         string AvgInsert = "Update  InventoryWarehouseMasterAvgCostTbl set DateUpdated='" + txtissuedate.Text + "',Qty='" + FinalQty + "' where MaterialIssueMasterTblId='" + ViewState["Id"] + "' and InvWMasterId='" + InvWMasterId123.Text + "'";
                         SqlCommand cmdavd = new SqlCommand(AvgInsert, con);
                         if (con.State.ToString() != "Open")
                         {
                             con.Open();
                         }
                         cmdavd.ExecuteNonQuery();
                         con.Close();

                         if (invtype == "3" || invtype == "2")
                         {
                             string updateavgcos = "Delete from  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + InvWMasterId123.Text + "' and Tranction_Master_Id='" + ViewState["tid"] + "'";
                             SqlCommand cmavgcost = new SqlCommand(updateavgcos, con);

                             if (con.State.ToString() != "Open")
                             {
                                 con.Open();
                             }
                             cmavgcost.ExecuteNonQuery();
                             con.Close();
                             if (invtype == "3")
                             {
                                 string upproc = "Insert into InventoryWarehouseMasterAvgCostTbl(InvWMasterId,Tranction_Master_Id,Qty,DateUpdated,AvgCost,QtyonHand)values('" + InvWMasterId123.Text + "','" + ViewState["tid"] + "','" + FinalQty + "','" + txtissuedate.Text + "','0','0')";
                                 SqlCommand cmdpro = new SqlCommand(upproc, con);
                                 if (con.State.ToString() != "Open")
                                 {
                                     con.Open();
                                 }
                                 cmdpro.ExecuteNonQuery();
                                 con.Close();
                             }
                         }
                         fillAVGCOSTSALES(ViewState["tid"].ToString(), InvWMasterId123.Text.ToString(), FinalQtySub.ToString(), invtype);

                     }
                     //// UPDATE PURCHASE MATERIAL
                     if (GridView2.Rows.Count > 0)
                     {
                         GridViewRow ft = (GridViewRow)GridView2.FooterRow;
                         Label lbltotalfooter = (Label)(ft.FindControl("lbltotalfooter"));

                         WIPAvgcost = Convert.ToDecimal(lbltotalfooter.Text) / WIPTotqty;
                         WIPAvgcost = Math.Round(WIPAvgcost, 2);
                         if (invtype == "3" || invtype == "2")
                         {
                             string updateavgcos = "Delete from  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + JobWmasterId + "' and Tranction_Master_Id='" + ViewState["tid"] + "'";
                             SqlCommand cmavgcost = new SqlCommand(updateavgcos, con);

                             if (con.State.ToString() != "Open")
                             {
                                 con.Open();
                             }
                             cmavgcost.ExecuteNonQuery();
                             con.Close();
                             if (invtype == "3")
                             {
                                 string upproc = "Insert into InventoryWarehouseMasterAvgCostTbl(InvWMasterId,Rate,Tranction_Master_Id,Qty,DateUpdated,AvgCost,QtyonHand)values('" + JobWmasterId + "','" + WIPAvgcost + "','" + ViewState["tid"] + "','" + WIPTotqty + "','" + txtissuedate.Text + "','0','0')";
                                 SqlCommand cmdpro = new SqlCommand(upproc, con);
                                 if (con.State.ToString() != "Open")
                                 {
                                     con.Open();
                                 }
                                 cmdpro.ExecuteNonQuery();
                                 con.Close();
                             }
                         }
                         fillAVGCOSTPurc(ViewState["tid"].ToString(), JobWmasterId.ToString(), WIPAvgcost, WIPTotqty.ToString(), invtype);
                     }


                     Label1.Text = "Record updated successfully";

                     pnluper.Visible = false;


                     pnldown.Visible = false;
                     btnadd.Visible = true;
                     lbllegend.Visible = false;
                     lbllegend.Text = "New Issue of Material for Work Order";

                     fillref();

                     clearall();
                     Button3.Visible = true;
                     FillGrid();
                 }
            }
        }

    }
    protected void fillAVGCOSTSALES(string traid, string invid, string recqty, string invtype)
    {
        decimal inwavgid = 0;
        string id12 = invid;
        string updateavgcos = "";
        decimal OLDavgcost = 0;
        decimal oLDqtyONHAND = 0;
        decimal Totalavgcost = 0;
        decimal Newqtyonhand = 0;
        decimal Finalqtyhand = 0;

        if (invtype == "1")
        {
            DataTable Datfi = select("SELECT IWMAvgCostId  FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + id12 + "' and Tranction_Master_Id='" + traid + "'");
            if (Datfi.Rows.Count > 0)
            {
                inwavgid = Convert.ToDecimal(Datfi.Rows[0]["IWMAvgCostId"]);
            }
            else
            {
                string ABCD = "Insert into InventoryWarehouseMasterAvgCostTbl(InvWMasterId,Tranction_Master_Id,Qty,DateUpdated,AvgCost,QtyonHand)values('" + id12 + "','" + traid + "','" + (Convert.ToDouble(recqty) * (-1)) + "','" + ViewState["sdo"] + "','0','0')";
                SqlCommand cmdadd = new SqlCommand(ABCD, con);

                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdadd.ExecuteNonQuery();
                con.Close();
            }
        }
        if (invtype == "1" || invtype == "2")
        {
            DataTable drtinvdata = new DataTable();
            if (invtype == "1")
            {
                drtinvdata = select("SELECT top(1) QtyonHand,Rate,AvgCost,Qty,DateUpdated,IWMAvgCostId FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + id12 + "' and DateUpdated<='" + ViewState["sdo"] + "' and Tranction_Master_Id<'" + traid + "' order by DateUpdated Desc,Tranction_Master_Id Desc,IWMAvgCostId Desc ");
                if (drtinvdata.Rows.Count == 0)
                {
                    drtinvdata = select("SELECT top(1) QtyonHand,Rate,AvgCost,Qty,DateUpdated,IWMAvgCostId FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + id12 + "' and DateUpdated<'" + ViewState["sdo"] + "'  order by DateUpdated Desc,Tranction_Master_Id Desc,IWMAvgCostId Desc ");

                }
            }
            else
            {
                drtinvdata = select("SELECT top(1) QtyonHand,Rate,AvgCost,Qty,DateUpdated,IWMAvgCostId FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + id12 + "' and DateUpdated<='" + ViewState["sdo"] + "' order by DateUpdated Desc,Tranction_Master_Id Desc,IWMAvgCostId Desc ");
            }
            if (drtinvdata.Rows.Count > 0)
            {
                if (Convert.ToString(drtinvdata.Rows[0]["AvgCost"]) != "")
                {
                    OLDavgcost = Convert.ToDecimal(drtinvdata.Rows[0]["AvgCost"]);
                }
                if (Convert.ToString(drtinvdata.Rows[0]["QtyonHand"]) != "")
                {
                    oLDqtyONHAND = Convert.ToDecimal(drtinvdata.Rows[0]["QtyonHand"]);
                }

            }
            Totalavgcost = OLDavgcost;
            Newqtyonhand = oLDqtyONHAND - Convert.ToDecimal(recqty);

            if (invtype == "1")
            {
                updateavgcos = "Update InventoryWarehouseMasterAvgCostTbl Set QtyonHand='" + Newqtyonhand + "',AvgCost='" + Totalavgcost + "' where InvWMasterId='" + id12 + "' and Tranction_Master_Id='" + traid + "'";

            }
            else
            {
                updateavgcos = "Insert into InventoryWarehouseMasterAvgCostTbl(InvWMasterId,Tranction_Master_Id,Qty,DateUpdated,AvgCost,QtyonHand)values('" + id12 + "','" + traid + "','" + (Convert.ToDouble(recqty) * (-1)) + "','" + ViewState["sdo"] + "','" + Totalavgcost + "','" + Newqtyonhand + "')";
            }
            SqlCommand cmavgcost = new SqlCommand(updateavgcos, con);

            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmavgcost.ExecuteNonQuery();
            con.Close();
        }
        string pera = "";
        if (invtype == "1")
        {
            pera = "  and DateUpdated>='" + ViewState["sdo"] + "' ";
        }
        else
        {
            pera = "  and DateUpdated>'" + ViewState["sdo"] + "'";
        }


        DataTable Dataupval = select("SELECT  QtyonHand,Rate,AvgCost,Qty,Tranction_Master_Id,IWMAvgCostId,DateUpdated FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + id12 + "' " + pera + " order by DateUpdated Asc,Tranction_Master_Id Asc,IWMAvgCostId Asc");
        decimal changeTotalavgcost = Totalavgcost;
        decimal changeTotalonhand = Newqtyonhand;
        string ABC = "";
        foreach (DataRow itm in Dataupval.Rows)
        {
            string gupd = "";
            string gupd1 = "";
            string manul = "";
            if (ABC == "")
            {
                if (invtype == "1")
                {
                    if ((Convert.ToDateTime(ViewState["sdo"]) == Convert.ToDateTime(itm["DateUpdated"])) && (Convert.ToDecimal(itm["Tranction_Master_Id"]) > Convert.ToDecimal(traid)))
                    {
                        ABC = "13";
                    }
                    else if (Convert.ToDateTime(ViewState["sdo"]) < Convert.ToDateTime(itm["DateUpdated"]))
                    {
                        ABC = "13";
                    }
                   
                }
                else
                {
                    ABC = "12";
                }
            }
            if (ABC != "")
            {
                if (Convert.ToString(itm["Rate"]) == "" && Convert.ToDecimal(itm["Qty"]) < 0)
                {
                    decimal newamt = 0;
                    DataTable drsv = select("Select AmountDebit from Tranction_Details where AccountDebit='8003' and Tranction_Master_Id='" + itm["Tranction_Master_Id"] + "' ");
                    if (drsv.Rows.Count > 0)
                    {
                        newamt = Convert.ToDecimal(drsv.Rows[0]["AmountDebit"]);
                    }

                    changeTotalavgcost = Math.Round(changeTotalavgcost, 2);
                    changeTotalonhand = changeTotalonhand + Convert.ToDecimal(itm["Qty"]);
                    Finalqtyhand = changeTotalonhand;
                    decimal appn = (changeTotalavgcost) * ((-1) * Convert.ToDecimal(itm["Qty"]));
                    decimal appold = Convert.ToDecimal(itm["AvgCost"]) * ((-1) * Convert.ToDecimal(itm["Qty"]));
                    newamt = newamt + appn - (appold);
                    newamt = Math.Round(newamt, 2);

                    gupd = "Update Tranction_Details Set AmountDebit='" + newamt + "' where AccountDebit='8003' and Tranction_Master_Id='" + itm["Tranction_Master_Id"] + "'";
                    gupd1 = "Update Tranction_Details Set AmountCredit='" + newamt + "' where AccountCredit='8000' and Tranction_Master_Id='" + itm["Tranction_Master_Id"] + "'";
                    manul = "Update InventoryWarehouseMasterAvgCostTbl Set QtyonHand='" + changeTotalonhand + "',AvgCost='" + changeTotalavgcost + "' where IWMAvgCostId='" + itm["IWMAvgCostId"] + "'";
                    SqlCommand cmdupcugs = new SqlCommand(gupd, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdupcugs.ExecuteNonQuery();
                    con.Close();
                    SqlCommand cmdupcugsin = new SqlCommand(gupd1, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdupcugsin.ExecuteNonQuery();
                    con.Close();

                }
                else if (Convert.ToString(itm["Tranction_Master_Id"]) != "")
                {
                    Finalqtyhand = changeTotalonhand + Convert.ToDecimal(itm["Qty"]);
                    if (Finalqtyhand > 0)
                    {
                        changeTotalavgcost = ((changeTotalavgcost * changeTotalonhand) + (Convert.ToDecimal(itm["Qty"]) * Convert.ToDecimal(itm["Rate"]))) / Finalqtyhand;
                    }
                    changeTotalonhand = changeTotalonhand + Convert.ToDecimal(itm["Qty"]);
                    changeTotalavgcost = Math.Round(changeTotalavgcost, 2);
                    manul = "Update InventoryWarehouseMasterAvgCostTbl Set QtyonHand='" + changeTotalonhand + "',AvgCost='" + changeTotalavgcost + "' where IWMAvgCostId='" + itm["IWMAvgCostId"] + "'";

                }
                if (manul.Length > 0)
                {
                    SqlCommand cmdupinv = new SqlCommand(manul, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdupinv.ExecuteNonQuery();
                    con.Close();
                }
            }

        }

    }
    protected void fillAVGCOSTPurc(string traid, string invid, decimal invrate, string recqty, string invtype)
    {
        decimal inwavgid = 0;
        string id12 = invid;
        string updateavgcos = "";
        decimal OLDavgcost = 0;
        decimal oLDqtyONHAND = 0;
        decimal Totalavgcost = 0;
        decimal Newqtyonhand = 0;
        decimal Finalqtyhand = 0;
        if (invtype == "1")
        {
            DataTable Datfi = select("SELECT IWMAvgCostId  FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + id12 + "' and Tranction_Master_Id='" + traid + "'");
            if (Datfi.Rows.Count > 0)
            {
                inwavgid = Convert.ToDecimal(Datfi.Rows[0]["IWMAvgCostId"]);
            }
            else
            {
                string ABCD = "Insert into InventoryWarehouseMasterAvgCostTbl(InvWMasterId,Tranction_Master_Id,Qty,Rate,DateUpdated,AvgCost,QtyonHand)values('" + id12 + "','" + traid + "','" + recqty + "','" + invrate + "','" + ViewState["sdo"] + "','" + Totalavgcost + "','" + Newqtyonhand + "')";
                SqlCommand cmdadd = new SqlCommand(ABCD, con);

                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdadd.ExecuteNonQuery();
                con.Close();
            }
        }
        if (invtype == "1" || invtype == "2")
        {
            DataTable drtinvdata = new DataTable();
            if (invtype == "1")
            {
                drtinvdata = select("SELECT top(1) QtyonHand,Rate,AvgCost,Qty,DateUpdated,IWMAvgCostId FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + id12 + "' and DateUpdated<='" + ViewState["sdo"] + "' and Tranction_Master_Id<'" + traid + "' order by DateUpdated Desc,Tranction_Master_Id Desc,IWMAvgCostId Desc ");
                if (drtinvdata.Rows.Count == 0)
                {
                    drtinvdata = select("SELECT top(1) QtyonHand,Rate,AvgCost,Qty,DateUpdated,IWMAvgCostId FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + id12 + "' and DateUpdated<'" + ViewState["sdo"] + "'  order by DateUpdated Desc,Tranction_Master_Id Desc,IWMAvgCostId Desc ");

                }
            }
            else
            {
                drtinvdata = select("SELECT top(1) QtyonHand,Rate,AvgCost,Qty,DateUpdated,IWMAvgCostId FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + id12 + "' and DateUpdated<='" + ViewState["sdo"] + "' order by DateUpdated Desc,Tranction_Master_Id Desc,IWMAvgCostId Desc ");
            }
            if (drtinvdata.Rows.Count > 0)
            {
                if (Convert.ToString(drtinvdata.Rows[0]["AvgCost"]) != "")
                {
                    OLDavgcost = Convert.ToDecimal(drtinvdata.Rows[0]["AvgCost"]);
                }
                if (Convert.ToString(drtinvdata.Rows[0]["QtyonHand"]) != "")
                {
                    oLDqtyONHAND = Convert.ToDecimal(drtinvdata.Rows[0]["QtyonHand"]);
                }

            }
            invrate = Math.Round(invrate, 2);
            Finalqtyhand = Convert.ToDecimal(recqty) + oLDqtyONHAND;
            if (Finalqtyhand > 0)
            {
                Totalavgcost = ((invrate * Convert.ToDecimal(recqty)) + (OLDavgcost * oLDqtyONHAND)) / Finalqtyhand;
            }
            Totalavgcost = Math.Round(Totalavgcost, 2);
            Newqtyonhand = Convert.ToDecimal(recqty) + oLDqtyONHAND;
            if (invtype == "1")
            {
                updateavgcos = "Update InventoryWarehouseMasterAvgCostTbl Set Rate='" + invrate + "', QtyonHand='" + Newqtyonhand + "',AvgCost='" + Totalavgcost + "' where InvWMasterId='" + id12 + "' and Tranction_Master_Id='" + traid + "'";

            }
            else
            {
                updateavgcos = "Insert into InventoryWarehouseMasterAvgCostTbl(InvWMasterId,Tranction_Master_Id,Qty,Rate,DateUpdated,AvgCost,QtyonHand)values('" + id12 + "','" + traid + "','" + recqty + "','" + invrate + "','" + ViewState["sdo"] + "','" + Totalavgcost + "','" + Newqtyonhand + "')";
            }
            SqlCommand cmavgcost = new SqlCommand(updateavgcos, con);
            //cmavgcost.CommandType = CommandType.Text;
            //cmavgcost.Transaction = transaction;
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmavgcost.ExecuteNonQuery();
            con.Close();
        }
        string pera = "";
        if (invtype == "1")
        {
            pera = "  and DateUpdated>='" + ViewState["sdo"] + "' ";
        }
        else
        {
            pera = "  and DateUpdated>'" + ViewState["sdo"] + "'";
        }


        DataTable Dataupval = select("SELECT  QtyonHand,Rate,AvgCost,Qty,Tranction_Master_Id,IWMAvgCostId,DateUpdated FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + id12 + "' " + pera + " order by DateUpdated Asc,Tranction_Master_Id Asc,IWMAvgCostId Asc");
        decimal changeTotalavgcost = Totalavgcost;
        decimal changeTotalonhand = Newqtyonhand;
        string ABC = "";
        foreach (DataRow itm in Dataupval.Rows)
        {
            string gupd = "";
            string gupd1 = "";
            string manul = "";
            if (ABC == "")
            {
                if (invtype == "1")
                {
                    if ((Convert.ToDateTime(ViewState["sdo"]) == Convert.ToDateTime(itm["DateUpdated"])) && (Convert.ToDecimal(itm["Tranction_Master_Id"]) > Convert.ToDecimal(traid)))
                    {
                        ABC = "13";
                    }
                    else if (Convert.ToDateTime(ViewState["sdo"]) < Convert.ToDateTime(itm["DateUpdated"]))
                    {
                        ABC = "13";
                    }
                    //if (Convert.ToDecimal(itm["Tranction_Master_Id"]) >Convert.ToDecimal(traid))
                    //{
                    //    ABC = "13";
                    //}
                }
                else
                {
                    ABC = "12";
                }
            }
            if (ABC != "")
            {
                if (Convert.ToString(itm["Rate"]) == "" && Convert.ToDecimal(itm["Qty"]) < 0)
                {
                    decimal newamt = 0;
                    DataTable drsv = select("Select AmountDebit from Tranction_Details where AccountDebit='8003' and Tranction_Master_Id='" + itm["Tranction_Master_Id"] + "' ");
                    if (drsv.Rows.Count > 0)
                    {
                        newamt = Convert.ToDecimal(drsv.Rows[0]["AmountDebit"]);
                    }

                    changeTotalavgcost = Math.Round(changeTotalavgcost, 2);
                    changeTotalonhand = changeTotalonhand + Convert.ToDecimal(itm["Qty"]);
                    Finalqtyhand = changeTotalonhand;
                    decimal appn = (changeTotalavgcost) * ((-1) * Convert.ToDecimal(itm["Qty"]));
                    decimal appold = Convert.ToDecimal(itm["AvgCost"]) * ((-1) * Convert.ToDecimal(itm["Qty"]));
                    newamt = newamt + appn - (appold);
                    newamt = Math.Round(newamt, 2);

                    gupd = "Update Tranction_Details Set AmountDebit='" + newamt + "' where AccountDebit='8003' and Tranction_Master_Id='" + itm["Tranction_Master_Id"] + "'";
                    gupd1 = "Update Tranction_Details Set AmountCredit='" + newamt + "' where AccountCredit='8000' and Tranction_Master_Id='" + itm["Tranction_Master_Id"] + "'";
                    manul = "Update InventoryWarehouseMasterAvgCostTbl Set QtyonHand='" + changeTotalonhand + "',AvgCost='" + changeTotalavgcost + "' where IWMAvgCostId='" + itm["IWMAvgCostId"] + "'";
                    SqlCommand cmdupcugs = new SqlCommand(gupd, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdupcugs.ExecuteNonQuery();
                    con.Close();
                    SqlCommand cmdupcugsin = new SqlCommand(gupd1, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdupcugsin.ExecuteNonQuery();
                    con.Close();

                }
                else if (Convert.ToString(itm["Tranction_Master_Id"]) != "")
                {
                    Finalqtyhand = changeTotalonhand + Convert.ToDecimal(itm["Qty"]);
                    if (Finalqtyhand > 0)
                    {
                        changeTotalavgcost = ((changeTotalavgcost * changeTotalonhand) + (Convert.ToDecimal(itm["Qty"]) * Convert.ToDecimal(itm["Rate"]))) / Finalqtyhand;
                    }
                    changeTotalonhand = changeTotalonhand + Convert.ToDecimal(itm["Qty"]);
                    changeTotalavgcost = Math.Round(changeTotalavgcost, 2);
                    manul = "Update InventoryWarehouseMasterAvgCostTbl Set QtyonHand='" + changeTotalonhand + "',AvgCost='" + changeTotalavgcost + "' where IWMAvgCostId='" + itm["IWMAvgCostId"] + "'";

                }
                if (manul.Length > 0)
                {
                    SqlCommand cmdupinv = new SqlCommand(manul, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdupinv.ExecuteNonQuery();
                    con.Close();
                }
            }

        }

    }

    protected int FillCountQtyUp(string traid, string invid, string recqty, string invtype)
    {
        int Anr = 0;
        decimal inwavgid = 0;
        string id12 = invid;

        decimal oLDqtyONHAND = 0;
        decimal Finalqtyhand = 0;
        if (invtype == "1")
        {
            DataTable Datfi = select("SELECT IWMAvgCostId  FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + id12 + "' and Tranction_Master_Id='" + traid + "'");
            if (Datfi.Rows.Count > 0)
            {
                inwavgid = Convert.ToDecimal(Datfi.Rows[0]["IWMAvgCostId"]);
            }

        }
        if (invtype == "1" || invtype == "2")
        {
            DataTable drtinvdata = new DataTable();
            if (invtype == "1")
            {
                drtinvdata = select("SELECT top(1) QtyonHand,Rate,AvgCost,Qty,DateUpdated,IWMAvgCostId FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + id12 + "' and DateUpdated<='" + ViewState["sdo"] + "' and Tranction_Master_Id<'" + traid + "' order by DateUpdated Desc,Tranction_Master_Id Desc,IWMAvgCostId Desc ");
                if (drtinvdata.Rows.Count == 0)
                {
                    drtinvdata = select("SELECT top(1) QtyonHand,Rate,AvgCost,Qty,DateUpdated,IWMAvgCostId FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + id12 + "' and DateUpdated<'" + ViewState["sdo"] + "'  order by DateUpdated Desc,Tranction_Master_Id Desc,IWMAvgCostId Desc ");

                }
            }
            else
            {
                drtinvdata = select("SELECT top(1) QtyonHand,Rate,AvgCost,Qty,DateUpdated,IWMAvgCostId FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + id12 + "' and DateUpdated<='" + ViewState["sdo"] + "' order by DateUpdated Desc,Tranction_Master_Id Desc,IWMAvgCostId Desc ");
            }
            if (drtinvdata.Rows.Count > 0)
            {

                if (Convert.ToString(drtinvdata.Rows[0]["QtyonHand"]) != "")
                {
                    oLDqtyONHAND = Convert.ToDecimal(drtinvdata.Rows[0]["QtyonHand"]);
                }

            }

            Finalqtyhand = Convert.ToDecimal(recqty) + oLDqtyONHAND;

        }
        string pera = "";
        if (invtype == "1")
        {
            pera = "  and DateUpdated>='" + ViewState["sdo"] + "' ";
        }
        else
        {
            pera = "  and DateUpdated>'" + ViewState["sdo"] + "'";
        }

        DataTable Dataupval = select("SELECT  QtyonHand,Rate,AvgCost,Qty,Tranction_Master_Id,IWMAvgCostId,DateUpdated FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + id12 + "' " + pera + " order by DateUpdated Asc,Tranction_Master_Id Asc,IWMAvgCostId Asc");

        string ABC = "";
        foreach (DataRow itm in Dataupval.Rows)
        {
            if (ABC == "")
            {
                if (invtype == "1")
                {
                    if ((Convert.ToDateTime(ViewState["sdo"]) == Convert.ToDateTime(itm["DateUpdated"])) && (Convert.ToDecimal(itm["Tranction_Master_Id"]) > Convert.ToDecimal(traid)))
                    {
                        ABC = "13";
                    }
                    else if (Convert.ToDateTime(ViewState["sdo"]) < Convert.ToDateTime(itm["DateUpdated"]))
                    {
                        ABC = "13";
                    }
                }
                else
                {
                    ABC = "12";
                }
            }
            if (ABC != "")
            {
                if (Convert.ToDateTime(ViewState["sdo"]) <= Convert.ToDateTime(itm["DateUpdated"]))
                {
                    if ((Convert.ToDateTime(ViewState["sdo"]) == Convert.ToDateTime(itm["DateUpdated"])) && (Convert.ToDecimal(traid) < Convert.ToDecimal(itm["Tranction_Master_Id"])))
                    {
                        Finalqtyhand += Convert.ToDecimal(recqty);
                    }
                    else
                    {
                        Finalqtyhand += Convert.ToDecimal(recqty);
                    }
                }
                Finalqtyhand = Finalqtyhand + Convert.ToDecimal(itm["Qty"]);
                if (Finalqtyhand < 0)
                {
                    Anr = 1;
                    break;
                }
            }

        }
        return Anr;
    }
    
    protected void Button4_Click(object sender, EventArgs e)
    {
        lbllegend.Text = "New Issue of Material for Work Order";

      
        pnluper.Visible = false;
        pnldown.Visible = false;
        btnadd.Visible = true;
        lbllegend.Visible = false;


        ddlStoreName.SelectedIndex = 0;
        ddlSelectJob.SelectedIndex = 0;
        txtissuedate.Text = "";
        txtNote.Text = "";
        txtRefernceNo.Text = "";
     
        clearall();
    }

    protected void clearall()
    {

        txtRefernceNo.Text = "";
        txtNote.Text = "";
        txtissuedate.Text = "";
        ddlSelectJob.SelectedIndex = 0;
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        FillGrid();
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");

            Button1.Text = "Hide Printable Version";
            Button2.Visible = true;
            if (GridView1.Columns[5].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[5].Visible = false;
            }
            if (GridView1.Columns[6].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                GridView1.Columns[6].Visible = false;
            }
        }
        else
        {

         

            Button1.Text = "Printable Version";
            Button2.Visible = false;
            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[5].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                GridView1.Columns[6].Visible = true;
            }
        }
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        Label1.Text = "";
        pnluper.Visible = true;
        btn_update.Visible = false;
        btnadd.Visible = false;
        lbllegend.Visible = true;
        ViewState["Id"] = "";
        //Button3.Visible = true;
        lbllegend.Text = "New Issue of Material for Work Order";
        pnldown.Visible = true;
        Fillitem();
    }
    protected void clearlist()
    {
        fillref();
       
       
        
    }

    protected void Button9_Click(object sender, EventArgs e)
    {
      
        pnluper.Visible = false;
        ViewState["Id"] = "";
        Label1.Text = "";
        pnldown.Visible = false;
        btnadd.Visible = true;
        lbllegend.Visible = false;
        lbllegend.Text = "New Issue of Material for Work Order";

    }
    protected void ddlSelectJob_SelectedIndexChanged(object sender, EventArgs e)
    {
        Fillitem();
    }

    //protected void txtissuedate_TextChanged(object sender, EventArgs e)
    //{
    //    foreach (GridViewRow item in GridView2.Rows)
    //    {
    //        DropDownList ddlitem123 = (DropDownList)item.FindControl("ddlitem123");
    //        Label lblqtyonhand = (Label)(item.FindControl("lblqtyonhand"));
    //        Label lblcost123 = (Label)(item.FindControl("lblcost123"));

    //        if (ddlitem123.SelectedIndex > 0)
    //        {

    //            avgcost(ddlitem123, lblcost123, lblqtyonhand);
    //        }
    //        else
    //        {
    //            lblqtyonhand.Text = "0.00";
    //            lblcost123.Text="0.00";
    //        }
           

    //    }
    //    Button7_Click(sender, e);
        
 
    //}
   
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        FillGrid();
    }
   
}
