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


public partial class PaymentDueReport : System.Web.UI.Page
{
   // SqlConnection ifile = new SqlConnection(ConfigurationManager.ConnectionStrings["ifilecabinet"].ConnectionString);
    // SqlConnection con;
   // SqlConnection conn;

    SqlConnection con = new SqlConnection(PageConn.connnn);
    SqlConnection conn = new SqlConnection(PageConn.connnn);
    SqlConnection ifile = new SqlConnection(PageConn.connnn);
    string compid;

    protected void Page_Load(object sender, EventArgs e)
    {
        //PageConn pgcon = new PageConn();
        //con = pgcon.dynconn;
        //conn = pgcon.dynconn; 
        if (!IsPostBack)
        {
            compid = Session["comid"].ToString();
        }

        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();
        compid = Session["Comid"].ToString();
        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();


        Page.Title = pg.getPageTitle(page);

        // ModalPopupExtender1222.Hide();
        Label1.Visible = false;
        Panel2.Visible = false;

        if (!IsPostBack)
        {
            ViewState["sortOrder"] = "";


            Fillddlwarehouse();

            ddlparty.Items.Insert(0, "All");

            if (Convert.ToString(Request.QueryString["GRT"]) == "fgr" && Request.QueryString["Wh"] != null)
            {
                ddlwarehouse.SelectedValue = Request.QueryString["Wh"];
                ddlwarehouse_SelectedIndexChanged(sender, e);

                txtfromdate.Text = Session["ddf"].ToString();
                txttodate.Text = Session["dde"].ToString();
                btnSearchGo_Click(sender, e);
            }
            else
            {
                ddlwarehouse_SelectedIndexChanged(sender, e);


                String frmdt = System.DateTime.Now.Month.ToString() + "/1/" + System.DateTime.Now.Year.ToString();
                txtfromdate.Text = Convert.ToDateTime(frmdt).ToShortDateString();
                txttodate.Text = Convert.ToString(System.DateTime.Now.Date.ToShortDateString());

            }


        }
    }


   
    protected void Fillddlparty()
    {
        ddlparty.Items.Clear();
        if (ddlwarehouse.SelectedIndex >-1)
        {
            String str = "SELECT Party_master.PartyID , Party_master.Compname+':'+Party_master.Contactperson  as Compname , Party_master.PartyTypeId FROM Party_master inner join PartytTypeMaster on PartytTypeMaster.[PartyTypeId]=Party_master.PartyTypeId WHERE  Party_master.Whid='" + ddlwarehouse.SelectedValue + "' and PartytTypeMaster.PartType='Vendor'";

            SqlCommand cmd = new SqlCommand(str, con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlparty.DataSource = dt;
            ddlparty.DataTextField = "Compname";
            ddlparty.DataValueField = "PartyID";
            ddlparty.DataBind();
            ddlparty.Items.Insert(0, "All");

            ddlparty.Items[0].Value = "0";
        }
        else
        {
            ddlparty.Items.Insert(0, "All");

            ddlparty.Items[0].Value = "0";
        }
      
       
    }
 
    protected void Fillddlwarehouse()
    {
        DataTable dtwh = ClsStore.SelectStorename();
        if (dtwh.Rows.Count > 0)
        {
            ddlwarehouse.DataSource = dtwh;
            ddlwarehouse.DataValueField = "WareHouseId";
            ddlwarehouse.DataTextField = "Name";

            ddlwarehouse.DataBind();
            DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

            if (dteeed.Rows.Count > 0)
            {
                ddlwarehouse.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
            }

        }
     
    }
    protected void gridpurchaseregister_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ////int i = Convert.ToInt32(e.Row.RowIndex.ToString());
        ////int datakey = Convert.ToInt32(gridpurchaseregister.DataKeys[i].Value.ToString());

       
        //gridpurchaseregister.f

        
    }
      public void gridtax()
    {
        CheckBoxList1.Items[5].Enabled = false;
        CheckBoxList1.Items[6].Enabled = false;
        CheckBoxList1.Items[7].Enabled = false;
        DataTable dttxt = select("select * from StorTaxmethodtbl where Storeid='" + ddlwarehouse.SelectedValue + "'");


        if (dttxt.Rows.Count > 0)
        {
            if (dttxt.Rows[0]["Variabletax"].ToString() == "1")
            {

                DataTable dtstate = select("Select State,Country,StateName,CountryName from CountryMaster inner join CompanyWebsiteAddressMaster on CompanyWebsiteAddressMaster.Country=CountryMaster.CountryId inner join StateMasterTbl on StateMasterTbl.StateId=CompanyWebsiteAddressMaster.State  inner join CompanyWebsitMaster on CompanyWebsitMaster.CompanyWebsiteMasterId=CompanyWebsiteAddressMaster.CompanyWebsiteMasterId  inner join WarehouseMaster on WarehouseMaster.WarehouseId=CompanyWebsitMaster.Whid where CompanyWebsitMaster.Whid='" + ddlwarehouse.SelectedValue + "'");
                if (dtstate.Rows.Count > 0)
                {
                    //Convert.ToString(dtstate.Rows[0]["Country"]);

                    DataTable dtwhid = select("select Top(3) Taxshortname,PurchaseTaxAccountMasterID,[Tax%],[SalesTaxOption3TaxNameTbl].[Taxname],[SalesTaxOption3TaxNameTbl].[StateId] from [InventoryTaxability] Left join [SalesTaxOption3TaxNameTbl] on [SalesTaxOption3TaxNameTbl].[Id]=[InventoryTaxability].[Taxoption3id] " +
                                         " where Active='1' and [Whid]='" + ddlwarehouse.SelectedValue + "' and ([SalesTaxOption3TaxNameTbl].[StateId]='" + Convert.ToString(dtstate.Rows[0]["State"]) + "' or [SalesTaxOption3TaxNameTbl].[StateId]='0')   order by SalesTaxOption3TaxNameTbl.Id Desc");

                    if (dtwhid.Rows.Count > 0)
                    {

                        if (dtwhid.Rows.Count == 3)
                        {
                            CheckBoxList1.Items[5].Text = Convert.ToString(dtwhid.Rows[0]["Taxshortname"]);
                            CheckBoxList1.Items[6].Text = Convert.ToString(dtwhid.Rows[1]["Taxshortname"]);
                            CheckBoxList1.Items[7].Text = Convert.ToString(dtwhid.Rows[2]["Taxshortname"]);
                            CheckBoxList1.Items[5].Enabled = true;
                            CheckBoxList1.Items[6].Enabled = true;
                            CheckBoxList1.Items[7].Enabled = true;
                        }
                        else if (dtwhid.Rows.Count == 2)
                        {
                            CheckBoxList1.Items[5].Text = Convert.ToString(dtwhid.Rows[0]["Taxshortname"]);
                            CheckBoxList1.Items[6].Text = Convert.ToString(dtwhid.Rows[1]["Taxshortname"]);
                            CheckBoxList1.Items[5].Enabled = true;
                            CheckBoxList1.Items[6].Enabled = true;
                        }
                        else if (dtwhid.Rows.Count == 1)
                        {
                            CheckBoxList1.Items[5].Text = Convert.ToString(dtwhid.Rows[0]["Taxshortname"]);
                            CheckBoxList1.Items[5].Enabled = true;
                        }

                    }
                    else
                    {

                    }
                }
            }
            else if (dttxt.Rows[0]["Fixedtaxdependingonstate"].ToString() == "True")
            {

                DataTable dtstate = select("Select State,Country,StateName,CountryName from CountryMaster inner join CompanyWebsiteAddressMaster on CompanyWebsiteAddressMaster.Country=CountryMaster.CountryId inner join StateMasterTbl on StateMasterTbl.StateId=CompanyWebsiteAddressMaster.State  inner join CompanyWebsitMaster on CompanyWebsitMaster.CompanyWebsiteMasterId=CompanyWebsiteAddressMaster.CompanyWebsiteMasterId  inner join WarehouseMaster on WarehouseMaster.WarehouseId=CompanyWebsitMaster.Whid where CompanyWebsitMaster.Whid='" + ddlwarehouse.SelectedValue + "'");
                if (dtstate.Rows.Count > 0)
                {

                    DataTable dtwhid = select("select Top(3) Taxshortname,PurchaseTaxAccountMasterID from [TaxTypeMasterMoreInfo]  where Active='1' and [Whid]='" + ddlwarehouse.SelectedValue + "' and ([TaxTypeMasterMoreInfo].[StateId]='" + Convert.ToString(dtstate.Rows[0]["State"]) + "' or [TaxTypeMasterMoreInfo].[StateId]='0')   order by TaxTypeMasterMoreInfo.Id Desc");

                    if (dtwhid.Rows.Count > 0)
                    {

                        if (dtwhid.Rows.Count == 3)
                        {
                            CheckBoxList1.Items[5].Text = Convert.ToString(dtwhid.Rows[0]["Taxshortname"]);
                            CheckBoxList1.Items[6].Text = Convert.ToString(dtwhid.Rows[1]["Taxshortname"]);
                            CheckBoxList1.Items[7].Text = Convert.ToString(dtwhid.Rows[2]["Taxshortname"]);
                            CheckBoxList1.Items[5].Enabled = true;
                            CheckBoxList1.Items[6].Enabled = true;
                            CheckBoxList1.Items[7].Enabled = true;
                        }
                        else if (dtwhid.Rows.Count == 2)
                        {
                            CheckBoxList1.Items[5].Text = Convert.ToString(dtwhid.Rows[0]["Taxshortname"]);
                            CheckBoxList1.Items[6].Text = Convert.ToString(dtwhid.Rows[1]["Taxshortname"]);
                            CheckBoxList1.Items[5].Enabled = true;
                            CheckBoxList1.Items[6].Enabled = true;
                        }
                        else if (dtwhid.Rows.Count == 1)
                        {
                            CheckBoxList1.Items[5].Text = Convert.ToString(dtwhid.Rows[0]["Taxshortname"]);
                            CheckBoxList1.Items[5].Enabled = true;
                        }
                    }

                }
            }
            else if (dttxt.Rows[0]["Fixedtaxforall"].ToString() == "True")
            {

                DataTable dtwhid = select("select Top(3) Taxshortname,PurchaseTaxAccountMasterID from [TaxTypeMaster]  where Active='1' and [Whid]='" + ddlwarehouse.SelectedValue + "'   order by TaxTypeMaster.TaxTypeMasterId Desc");

                if (dtwhid.Rows.Count > 0)
                {

                    if (dtwhid.Rows.Count == 3)
                    {
                        CheckBoxList1.Items[5].Text = Convert.ToString(dtwhid.Rows[0]["Taxshortname"]);
                        CheckBoxList1.Items[6].Text = Convert.ToString(dtwhid.Rows[1]["Taxshortname"]);
                        CheckBoxList1.Items[7].Text = Convert.ToString(dtwhid.Rows[2]["Taxshortname"]);
                        CheckBoxList1.Items[5].Enabled = true;
                        CheckBoxList1.Items[6].Enabled = true;
                        CheckBoxList1.Items[7].Enabled = true;
                    }
                    else if (dtwhid.Rows.Count == 2)
                    {
                        CheckBoxList1.Items[5].Text = Convert.ToString(dtwhid.Rows[0]["Taxshortname"]);
                        CheckBoxList1.Items[6].Text = Convert.ToString(dtwhid.Rows[1]["Taxshortname"]);
                        CheckBoxList1.Items[5].Enabled = true;
                        CheckBoxList1.Items[6].Enabled = true;
                    }
                    else if (dtwhid.Rows.Count == 1)
                    {
                        CheckBoxList1.Items[5].Text = Convert.ToString(dtwhid.Rows[0]["Taxshortname"]);
                        CheckBoxList1.Items[5].Enabled = true;
                    }
                }

            }
        }


    }
    protected void btngo_Click(object sender, EventArgs e)
    {
        taxop();
        FillGrid();
       
    }
    protected void taxop()
    {
        if (CheckBoxList1.Items[0].Selected)
        {
            gridpurchaseregister.Columns[0].Visible = true;
        }
        else
        {
            gridpurchaseregister.Columns[0].Visible = false;
        }
        if (CheckBoxList1.Items[1].Selected)
        {
            gridpurchaseregister.Columns[1].Visible = true;
        }
        else
        {
            gridpurchaseregister.Columns[1].Visible = false;
        }
        if (CheckBoxList1.Items[2].Selected)
        {
            gridpurchaseregister.Columns[2].Visible = true;
        }
        else
        {
            gridpurchaseregister.Columns[2].Visible = false;
        }
        if (CheckBoxList1.Items[3].Selected)
        {
            gridpurchaseregister.Columns[3].Visible = true;
        }
        else
        {
            gridpurchaseregister.Columns[3].Visible = false;
        }

        if (CheckBoxList1.Items[4].Selected)
        {
            gridpurchaseregister.Columns[4].Visible = true;
        }
        else
        {
            gridpurchaseregister.Columns[4].Visible = false;
        }

        if (CheckBoxList1.Items[5].Selected)
        {
            gridpurchaseregister.Columns[5].Visible = true;
        }
        else
        {
            gridpurchaseregister.Columns[5].Visible = false;
        }

        if (CheckBoxList1.Items[6].Selected)
        {
            gridpurchaseregister.Columns[6].HeaderText = CheckBoxList1.Items[6].Text;
            gridpurchaseregister.Columns[6].Visible = true;
        }
        else
        {
            gridpurchaseregister.Columns[6].Visible = false;
        }

        if (CheckBoxList1.Items[7].Selected)
        {
            gridpurchaseregister.Columns[7].HeaderText = CheckBoxList1.Items[7].Text;
            gridpurchaseregister.Columns[7].Visible = true;
        }
        else
        {

            gridpurchaseregister.Columns[7].Visible = false;
        }

        if (CheckBoxList1.Items[8].Selected)
        {
            gridpurchaseregister.Columns[8].HeaderText = CheckBoxList1.Items[8].Text;
            gridpurchaseregister.Columns[8].Visible = true;
        }
        else
        {
            gridpurchaseregister.Columns[8].Visible = false;
        }

        if (CheckBoxList1.Items[9].Selected)
        {
            gridpurchaseregister.Columns[9].Visible = true;
        }
        else
        {
            gridpurchaseregister.Columns[9].Visible = false;
        }
        if (CheckBoxList1.Items[10].Selected)
        {
            gridpurchaseregister.Columns[10].Visible = true;
        }
        else
        {
            gridpurchaseregister.Columns[10].Visible = false;
        }


      
    }
    protected void FillGrid()
    {
        lblparty.Text = ddlparty.SelectedItem.Text;
        DataTable dtGrid = (DataTable)(GridFiltersDatatable());
        if (dtGrid != null)
        {
            if (dtGrid.Rows.Count > 0)
            {

                gridpurchaseregister.DataSource = dtGrid;

                DataView myDataView = new DataView();
                myDataView = dtGrid.DefaultView;

                if (hdnsortExp.Value != string.Empty)
                {
                    myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
                }

                gridpurchaseregister.DataBind();
                
            }
            if (gridpurchaseregister.Rows.Count > 0)
            {
                Double tax1total = 0;
                Double tax2total = 0;
                Double tax3total = 0;
                Double shipchargetotal = 0;
                Double gsttltotal = 0;
                Double nettotal = 0;

                foreach (GridViewRow gv in gridpurchaseregister.Rows)
                {

                    int tid = Convert.ToInt32(gridpurchaseregister.DataKeys[gv.RowIndex].Value);
                    ImageButton img = (ImageButton)gv.FindControl("img1");
                    Label lbldocno = (Label)gv.FindControl("lbldocno");
                    string tid1 = img.CommandArgument;

                    string scpt = "select * from AttachmentMaster where RelatedTableId='" + tid1 + "'";

                    SqlDataAdapter adp58 = new SqlDataAdapter(scpt, con);
                    DataTable ds58 = new DataTable();
                    adp58.Fill(ds58);

                    if (ds58.Rows.Count == 0)
                    {

                        img.ImageUrl = "~/ShoppingCart/images/Docimg.png";
                        img.Enabled = false;

                        lbldocno.Text = "0";
                        img.ToolTip = "0";
                    }
                    else
                    {
                        img.ImageUrl = "~/ShoppingCart/images/Docimggreen.jpg";
                        img.Enabled = true;
                        img.ToolTip = ds58.Rows.Count.ToString();
                        lbldocno.Text = ds58.Rows.Count.ToString();

                    }

                    double t1, t2, t3, shipcharge, gsttl, net = 0;
                    DataSet ds = new DataSet();
                    Label tax1 = (Label)(gv.FindControl("lblgrdtax1"));
                    if (tax1.Text != "")
                    {
                        if (isnumSelf(tax1.Text) != 0)
                        {
                            //tax1.Text 
                            t1 = Convert.ToDouble(tax1.Text);
                        }
                        else
                        {
                            t1 = 0;
                        }
                    }
                    else
                    {
                        t1 = 0;
                    }
                    Label tax2 = (Label)(gv.FindControl("lblgrdtax2"));
                    if (tax2.Text != "")
                    {
                        if (isnumSelf(tax2.Text) != 0)
                        {

                            t2 = Convert.ToDouble(tax2.Text);
                        }
                        else
                        {
                            t2 = 0;
                        }
                    }
                    else
                    {
                        t2 = 0;
                    }
                    Label tax3 = (Label)(gv.FindControl("lblgrdtax3"));
                    if (tax3.Text != "")
                    {
                        if (isnumSelf(tax3.Text) != 0)
                        {
                            //tax1.Text 
                            t3 = Convert.ToDouble(tax3.Text);
                        }
                        else
                        {
                            t3 = 0;
                        }
                    }
                    else
                    {
                        t3 = 0;
                    }
                    Label shipingcharge = (Label)(gv.FindControl("lblgrdshippingcharg"));
                    if (shipingcharge.Text != "")
                    {
                        if (isnumSelf(shipingcharge.Text) != 0)
                        {

                            shipcharge = Convert.ToDouble(shipingcharge.Text);
                        }
                        else
                        {
                            shipcharge = 0;
                        }
                    }
                    else
                    {
                        shipcharge = 0;
                    }
                    double total = (t1 + t2 + t3 + shipcharge);
                    Label grisstotal = (Label)(gv.FindControl("lblgrdgrosstotal"));
                    if (grisstotal.Text != "")
                    {
                        gsttl = Convert.ToDouble(grisstotal.Text);
                    }
                    else
                    {
                        gsttl = 0;
                    }
                    net = gsttl - total;
                    Label netamt = (Label)(gv.FindControl("lblgrdnetamt"));
      
                    netamt.Text = Convert.ToString(net);
                    Label lblgrdentrytype = (Label)(gv.FindControl("lblgrdentrytype"));
                    tax1total = tax1total + t1;
                    tax2total = tax2total + t2;
                    tax3total = tax3total + t3;
                    shipchargetotal = shipchargetotal + shipcharge;
                    gsttltotal = gsttltotal + gsttl;
                    nettotal = nettotal + net;
                    ds.Reset();


                }


                ViewState["7"] = nettotal.ToString();
                ViewState["10"] = shipchargetotal.ToString();
                ViewState["8"] = tax1total.ToString();
                ViewState["9"] = tax2total.ToString();

                ViewState["ta3"] = tax3total.ToString();
                ViewState["11"] = gsttltotal.ToString();

                // GridViewRow ft = (GridViewRow)gridpurchaseregister.FooterRow;
                gridpurchaseregister.FooterRow.Cells[4].ForeColor = System.Drawing.Color.White;
                gridpurchaseregister.FooterRow.Cells[5].ForeColor = System.Drawing.Color.White;
                gridpurchaseregister.FooterRow.Cells[6].ForeColor = System.Drawing.Color.White;
                gridpurchaseregister.FooterRow.Cells[7].ForeColor = System.Drawing.Color.White;
                gridpurchaseregister.FooterRow.Cells[8].ForeColor = System.Drawing.Color.White;
                gridpurchaseregister.FooterRow.Cells[9].ForeColor = System.Drawing.Color.White;
                gridpurchaseregister.FooterRow.Cells[10].ForeColor = System.Drawing.Color.White;

                gridpurchaseregister.FooterRow.Cells[4].Text = "Total :";
                gridpurchaseregister.FooterRow.Cells[5].Text = String.Format("{0:n}", Math.Round(Convert.ToDecimal(ViewState["7"]), 2));
                gridpurchaseregister.FooterRow.Cells[6].Text = String.Format("{0:n}", Math.Round(Convert.ToDecimal(ViewState["8"]), 2));
                gridpurchaseregister.FooterRow.Cells[7].Text = String.Format("{0:n}", Math.Round(Convert.ToDecimal(ViewState["9"]), 2));
                gridpurchaseregister.FooterRow.Cells[8].Text = String.Format("{0:n}", Math.Round(Convert.ToDecimal(ViewState["ta3"]), 2));
                gridpurchaseregister.FooterRow.Cells[9].Text = String.Format("{0:n}", Math.Round(Convert.ToDecimal(ViewState["10"]), 2));
                gridpurchaseregister.FooterRow.Cells[10].Text = String.Format("{0:n}", Math.Round(Convert.ToDecimal(ViewState["11"]), 2));
            }


        }
        else
        {
            gridpurchaseregister.DataSource = null;


            gridpurchaseregister.DataBind();
        }

    }
    
    private DataTable GridFiltersDatatable()
    {
        
        DataTable dt = new DataTable();
        ViewState["StrMMM"] = null;
        string str = "";

        str = " SELECT  distinct  EntryTypeMaster.Entry_Type_Name as SortName, TranctionMaster.Tranction_Master_Id, GrnMaster_Id, PurchaseDetails.Purchase_Details_Id, PurchaseDetails.PartyName as Pid,Party_master.Compname as  PartyName,Convert(nvarchar(10),PurchaseDetails.Date,101) as Date, PurchaseDetails.TotalAmt," +
                          " PurchaseDetails.PurchaseInvoiceNumber, PurchaseDetails.TexAmount1, PurchaseDetails.texAmount2, PurchaseDetails.texAmount3, PurchaseDetails.shippingandHandlingCharges," +
                          " PurchaseDetails.EntryNumer, PurchaseDetails.WareHouseMasterId,TranctionMaster.EntryTypeId," +
                          " TranctionMaster.EntryNumber  FROM Party_master inner join  PurchaseDetails ON PurchaseDetails.PartyName=Party_master.PartyID  INNER JOIN   TranctionMaster ON PurchaseDetails.TransId =  TranctionMaster.Tranction_Master_Id inner join   " +
                            " TranctionMasterSuppliment ON TranctionMasterSuppliment.Tranction_Master_Id=TranctionMaster.Tranction_Master_Id   inner join EntryTypeMaster on EntryTypeMaster.Entry_Type_Id=TranctionMaster.EntryTypeId " +
                           
                                     " where PurchaseDetails.compid = '" + compid + "' and TranctionMaster.Whid='" + ddlwarehouse.SelectedValue + "' and PurchaseDetails.WareHouseMasterId='" + ddlwarehouse.SelectedValue + "' ";




        string between = " Cast(TranctionMasterSuppliment.GrnMaster_Id as datetime)  between '" + txtfromdate.Text + "' AND '" + txttodate.Text + "' and AmountDue>0";
        if (between != "")
        {
            String strMMM = str + " and  " + between;

            ViewState["StrMMM"] = strMMM;

         
           
            if (ddlparty.SelectedIndex > 0)
            {

                String FilterByPartyId = " and PartyID ='" + ddlparty.SelectedValue + "'";
                if (FilterByPartyId == "")
                {
                    ViewState["StrMMM"] = null;
                    return dt;
                }
                else
                {
                    ViewState["StrMMM"] = ViewState["StrMMM"].ToString() + FilterByPartyId;
                }
            }
           

            if (ViewState["StrMMM"] != null)
            {
                SqlCommand cmd = new SqlCommand(ViewState["StrMMM"].ToString() + " Order by GrnMaster_Id Desc", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            else
            {
                dt = null;
            }
        }
        else
        {
            dt = null;
        }
        return dt;
    }
   
   
    protected void gridpurchaseregister_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        FillGrid();
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
    protected void ddlwarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        Fillddlparty();
       
        gridtax();
     
    }
    
 

   
    public decimal isnumSelf(string ck)
    {
        decimal i;
        try
        {
            i = Convert.ToDecimal(ck);
        }
        catch (Exception)
        {
            i = 0;
        }
        return i;
    }
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {


        gridpurchaseregister.DataSource = null;

        gridpurchaseregister.DataBind();
       

        if (ddlwarehouse.SelectedIndex > -1)
        {


            lblstore.Text = ddlwarehouse.SelectedItem.Text.ToString();


            //DataTable dt1111 = select("select Convert(nvarchar,StartDate,101) as StartDate, Convert(nvarchar,EndDate,101) as EndDate from [ReportPeriod] where whid='" + ddlwarehouse.SelectedValue + "'  and Active='1'");
            //if (dt1111.Rows.Count > 0)
            //{

                //lblddf.Text = "From Date : " + txtfromdate.Text + "  To Date : " + txttodate.Text;
                //if (Convert.ToDateTime(txtfromdate.Text) >= Convert.ToDateTime(dt1111.Rows[0]["StartDate"]) && Convert.ToDateTime(txttodate.Text) <= Convert.ToDateTime(dt1111.Rows[0]["EndDate"]))
                //{

                    DateTime dt2 = Convert.ToDateTime(txtfromdate.Text);
                    DateTime dt1 = Convert.ToDateTime(txttodate.Text);
                    if (dt1 < dt2)
                    {

                        Label1.Visible = true;
                        Label1.Text = " Start Date Must Be Less than End Date";


                    }
                    else
                    {
                        Panel1.Visible = true;
                        btngo_Click(sender, e);

                    }
                //}
                //else
                //{
                //    // lblstartdate.Text = dt1111.Rows[0][0].ToString();
                //    ModalPopupExtenderAcy.Show();
                //}

           // }


        }
    }
    protected void btnok_Click(object sender, EventArgs e)
    {
        ModalPopupExtenderAcy.Hide();
        Panel1.Visible = true;
       
        btngo_Click(sender, e);


    }

 
    protected void btnSelectColums_Click(object sender, EventArgs e)
    {
        Panel2.Visible = true;
    }
    protected DataTable select(string str)
    {
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);
        return dt;

    }
    protected void yes_Click(object sender, EventArgs e)
    {
      
        if (conn.State.ToString() != "Open")
        {
            conn.Open();
           
        }

        SqlDataAdapter dtpsel = new SqlDataAdapter("select PurchaseDetails.EntryNumer, TranctionMaster.Tranction_Master_Id, TranctionMaster.EntryTypeId,PurchaseDetails.Purchase_Details_Id from  PurchaseDetails  inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=PurchaseDetails.TransId  where PurchaseDetails.Purchase_Details_Id='" + Convert.ToInt32(ViewState["Dk"]) + "' and  TranctionMaster.EntryTypeId ='4'  and TranctionMaster.Whid='" + ddlwarehouse.SelectedValue + "'", con);
        //SqlDataAdapter dtpsel = new SqlDataAdapter(cmdsel);


        DataTable dtsel = new DataTable();
        dtpsel.Fill(dtsel);
     
        SqlTransaction transaction = conn.BeginTransaction();
        try
        {
           // SqlCommand cmdsel = new SqlCommand("select PurchaseDetails.EntryNumer, PurchaseMaster.*,TranctionMaster.Tranction_Master_Id, TranctionMaster.EntryTypeId,PurchaseDetails.Purchase_Details_Id from PurchaseMaster Inner join PurchaseDetails on PurchaseMaster.Purchase_Details_Id = PurchaseDetails.Purchase_Details_Id  inner join TranctionMaster on TranctionMaster.EntryNumber=PurchaseDetails.EntryNumer  where PurchaseDetails.Purchase_Details_Id='" + Convert.ToInt32(ViewState["Dk"]) + "' and  TranctionMaster.EntryTypeId ='27'  and TranctionMaster.Whid='" + ddlwarehouse.SelectedValue + "'", con);
            
        string srdelTrasnm = "";
        string stdeltrasD = "";
      
        string DelTrsSupl = "";
       
        string DelPayAppln = "";

        if (dtsel.Rows.Count > 0)
        {
            int trMstId = Convert.ToInt32(dtsel.Rows[0]["Tranction_Master_Id"]);
            if (trMstId.ToString() != "0")
            {
                if (trMstId.ToString() != "")
                {
                    srdelTrasnm = " delete  TranctionMaster where Tranction_Master_Id='" + trMstId + "' ";

                    stdeltrasD = " delete Tranction_Details where Tranction_Master_Id='" + trMstId + "' ";

                    DelTrsSupl = " delete TranctionMasterSuppliment where Tranction_Master_Id='" + trMstId + "' ";



                    DelPayAppln = " delete PaymentApplicationTbl where PaymentAppTblId='" + trMstId + "' ";
                   
                }
            }

        }






        string strdelePurD = " delete PurchaseDetails where Purchase_Details_Id = '" + Convert.ToInt32(ViewState["Dk"]) + "' ";
        string strdelePurM = " delete PurchaseMaster where Purchase_Details_Id='" + Convert.ToInt32(ViewState["Dk"]) + "' ";





        SqlCommand strPurM = new SqlCommand(strdelePurM, conn);
        SqlCommand strPurD = new SqlCommand(strdelePurD, conn);
        strPurM.Transaction = transaction;
        strPurD.Transaction = transaction;
        if (srdelTrasnm != "")
        {
            SqlCommand cmdDeleTrasM = new SqlCommand(srdelTrasnm, conn);
            SqlCommand cmdDeleTrsD = new SqlCommand(stdeltrasD, conn);
            cmdDeleTrasM.Transaction = transaction;
            cmdDeleTrsD.Transaction = transaction;

            //  con.Open();
            cmdDeleTrasM.ExecuteNonQuery();
            cmdDeleTrsD.ExecuteNonQuery();
            // con.Close();
        }
        if (DelTrsSupl != "")
        {
            SqlCommand cmdDelTrSupl = new SqlCommand(DelTrsSupl, conn);
            cmdDelTrSupl.Transaction = transaction;


            //con.Open();
            cmdDelTrSupl.ExecuteNonQuery();
            // con.Close();
        }
        if (DelPayAppln != "")
        {
            SqlCommand cmdPayAccpn = new SqlCommand(DelPayAppln, conn);
            cmdPayAccpn.Transaction = transaction;
            // con.Open();
            cmdPayAccpn.ExecuteNonQuery();
            //con.Close();
        }


        // con.Open();
        strPurD.ExecuteNonQuery();
        strPurM.ExecuteNonQuery();
        // con.Close();
       
        
       
           
            transaction.Commit();
            Label1.Text = "Record deleted successfully";
        }
        catch (Exception)
        {
            transaction.Rollback();
        }
        finally
        {
            conn.Close();
            Label1.Visible = true;
            EventArgs ee = new EventArgs();
            btnSearchGo_Click(sender, e);
          
        }
    }
    protected void Button661_Click(object sender, ImageClickEventArgs e)
    {

    }
   
    protected void ddlperiod_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
   
    protected void grd_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            grd.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            int docid = Convert.ToInt32(grd.Rows[grd.SelectedIndex].Cells[1].Text);
        }
    }
    protected void imgin_Click(object sender, ImageClickEventArgs e)
    {
        if (Request.QueryString["docid"] != null)
        {
            inserdocatt();
        }
    }
    public void inserdocatt()
    {

        string sqlselect = "select * from DocumentMaster where DocumentId='" + Request.QueryString["docid"] + "'";
        SqlDataAdapter adpt = new SqlDataAdapter(sqlselect, con);
        DataTable dtpt = new DataTable();
        adpt.Fill(dtpt);
        if (dtpt.Rows.Count > 0)
        {
            int k = 0;
            ViewState["ttt"] = 0;
            foreach (GridViewRow gdr in gridpurchaseregister.Rows)
            {
                CheckBox chk1 = (CheckBox)gdr.FindControl("chk");
                //int tid = Convert.ToInt32(gridDCregister.DataKeys[k].Value);
                ImageButton img = (ImageButton)gdr.FindControl("img1");
                int tid = Convert.ToInt32(img.CommandArgument);

                k = k + 1;
                if (chk1.Checked == true)
                {
                    string sqlselectr = "select * from AttachmentMaster where RelatedTableId='" + tid + "' and IfilecabinetDocId='" + Request.QueryString["docid"] + "'";
                    SqlDataAdapter adptr = new SqlDataAdapter(sqlselectr, con);
                    DataTable dtptr = new DataTable();
                    adptr.Fill(dtptr);
                    if (dtptr.Rows.Count <= 0)
                    {
                        if (Convert.ToInt32(ViewState["ttt"]) != tid)
                        {
                            ViewState["ttt"] = tid.ToString();
                            SqlCommand cmdi = new SqlCommand("InsertAttachmentMaster", con);

                            cmdi.CommandType = CommandType.StoredProcedure;
                            cmdi.Parameters.Add(new SqlParameter("@Titlename", SqlDbType.NVarChar));
                            cmdi.Parameters["@Titlename"].Value = dtpt.Rows[0]["DocumentTitle"].ToString();
                            cmdi.Parameters.Add(new SqlParameter("@Filename", SqlDbType.NVarChar));
                            cmdi.Parameters["@Filename"].Value = dtpt.Rows[0]["DocumentName"].ToString();

                            cmdi.Parameters.Add(new SqlParameter("@Datetime", SqlDbType.DateTime));
                            cmdi.Parameters["@Datetime"].Value = dtpt.Rows[0]["DocumentUploadDate"].ToString(); ;
                            cmdi.Parameters.Add(new SqlParameter("@RelatedtablemasterId", SqlDbType.NVarChar));
                            cmdi.Parameters["@RelatedtablemasterId"].Value = "5";
                            cmdi.Parameters.Add(new SqlParameter("@RelatedTableId", SqlDbType.NVarChar));
                            cmdi.Parameters["@RelatedTableId"].Value = tid.ToString();
                            cmdi.Parameters.Add(new SqlParameter("@IfilecabinetDocId", SqlDbType.NVarChar));
                            cmdi.Parameters["@IfilecabinetDocId"].Value = dtpt.Rows[0]["DocumentId"].ToString();
                            cmdi.Parameters.Add(new SqlParameter("@Attachment", SqlDbType.Int));
                            cmdi.Parameters["@Attachment"].Direction = ParameterDirection.Output;

                            cmdi.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
                            cmdi.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            Int32 result = cmdi.ExecuteNonQuery();
                            result = Convert.ToInt32(cmdi.Parameters["@Attachment"].Value);
                            con.Close();
                        }
                    }
                }
            }
            if (k > 0)
            {
                Label1.Text = "Record Inserted Successfully";
                Label1.Visible = true;
                FillGrid();
            }

        }
    }
 
    protected void gridpurchaseregister_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //gridpurchaseregister.SelectedIndex = Convert.ToInt32(gridpurchaseregister.DataKeys[e.RowIndex].Value.ToString());
        //ViewState["Dk"] = gridpurchaseregister.SelectedIndex;

        //yes_Click(sender, e);

    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (Button5.Text == "Printable Version")
        {
            //Panel1.ScrollBars = ScrollBars.None;
            //Panel1.Height = new Unit("100%");

            Button5.Text = "Hide Printable Version";
            Button3.Visible = true;
            //if (gridpurchaseregister.Columns[11].Visible == true)
            //{
            //    ViewState["viewd"] = "tt";
            //    gridpurchaseregister.Columns[11].Visible = false;
            //}
            //if (gridpurchaseregister.Columns[12].Visible == true)
            //{
            //    ViewState["editHide"] = "tt";
            //    gridpurchaseregister.Columns[12].Visible = false;
            //}
            //if (gridpurchaseregister.Columns[13].Visible == true)
            //{
            //    ViewState["deleHide"] = "tt";
            //    gridpurchaseregister.Columns[13].Visible = false;
            //}
            //if (gridpurchaseregister.Columns[14].Visible == true)
            //{
            //    ViewState["Viewdd"] = "tt";
            //    gridpurchaseregister.Columns[14].Visible = false;
            //}


        }
        else
        {

            //Panel1.ScrollBars = ScrollBars.Vertical;
            //Panel1.Height = new Unit(250);

            Button5.Text = "Printable Version";
            Button3.Visible = false;
            //if (ViewState["viewd"] != null)
            //{
            //    gridpurchaseregister.Columns[11].Visible = true;
            //}
            //if (ViewState["editHide"] != null)
            //{
            //    gridpurchaseregister.Columns[12].Visible = true;
            //}
            //if (ViewState["deleHide"] != null)
            //{
            //    gridpurchaseregister.Columns[13].Visible = true;
            //}
            //if (ViewState["Viewdd"] != null)
            //{
            //    gridpurchaseregister.Columns[14].Visible = true;
            //}


        }
    }
    protected void gridpurchaseregister_RowCommand(object sender, GridViewCommandEventArgs e)
    {              

        if (e.CommandName == "Sort")
        {
            return;
        }
        else if (e.CommandName == "AddDoc")
        {
            int dk = Convert.ToInt32(e.CommandArgument);// Convert.ToInt32(GridView2.DataKeys[GridView2.SelectedIndex].Value);
            ViewState["Dk"] = dk;

            string entryno = "select TranctionMaster.EntryNumber ,EntryTypeMaster.Entry_Type_Name from TranctionMaster inner join EntryTypeMaster on EntryTypeMaster.Entry_Type_Id=TranctionMaster.EntryTypeId where Tranction_Master_Id='" + ViewState["Dk"] + "'";

            SqlDataAdapter adpentno = new SqlDataAdapter(entryno, con);
            DataTable dsentno = new DataTable();
            adpentno.Fill(dsentno);
            if (dsentno.Rows.Count > 0)
            {
                lbldocentrytype.Text = dsentno.Rows[0]["Entry_Type_Name"].ToString();
                lbldocentryno.Text = dsentno.Rows[0]["EntryNumber"].ToString();

            }


            // string scpt = "select * from AttachmentMaster where RelatedTableId='" + ViewState["Dk"] + "'";
            string scpt = " select AttachmentMaster.Id,DocumentMaster.DocumentId as IfilecabinetDocId,DocumentMaster.DocumentTitle as Titlename,CONVERT (Nvarchar, DocumentMaster.DocumentDate,101) as Datetime   ,DocumentMainType.DocumentMainType+':'+ DocumentSubType.DocumentSubType+':'+DocumentType.DocumentType as Filename from AttachmentMaster inner join DocumentMaster on DocumentMaster.DocumentId=AttachmentMaster.IfilecabinetDocId inner join DocumentType on DocumentMaster.DocumentTypeId=DocumentType.DocumentTypeId  inner join DocumentSubType on DocumentSubType.DocumentSubTypeId=DocumentType.DocumentSubTypeId inner join DocumentMainType on DocumentMainType.DocumentMainTypeId=DocumentSubType.DocumentMainTypeId where AttachmentMaster.RelatedTableId='" + ViewState["Dk"] + "' ";
            SqlDataAdapter adp58 = new SqlDataAdapter(scpt, con);
            DataTable ds58 = new DataTable();
            adp58.Fill(ds58);


            if (ds58.Rows.Count > 0)
            {
                grd.DataSource = ds58;
                grd.DataBind();
                if (grd.Rows.Count > 1)
                {
                    lbldoclab.Text = "List of Documents";
                    lblheadoc.Text = "List of documents attached to ";
                }
                else
                {
                    lbldoclab.Text = "List of Document";
                    lblheadoc.Text = "List of document attached to ";
                }

            }
            ModalPopupExtender4.Show();

        }
        else if (e.CommandName == "Delete")
        {

            ViewState["Dk"] = e.CommandArgument.ToString();
            //ModalPopupExtender1222.Show();
            yes_Click(sender, e);
        }
        else if (e.CommandName == "remove")
        {
            gridpurchaseregister.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            int dk = Convert.ToInt32(gridpurchaseregister.DataKeys[gridpurchaseregister.SelectedIndex].Value);
            ViewState["Dk"] = dk;
            //ModalPopupExtender1222.Show();
        }
        else if (e.CommandName == "Docadd")
        {
            int dk = Convert.ToInt32(e.CommandArgument);
            string te = "AccEntryDocUp.aspx?Tid=" + dk;
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);


        }
        else if (e.CommandName == "Edit")
        {
            int dk = Convert.ToInt32(e.CommandArgument);
            string te = "ExpenseInvoice.aspx?Purchase_Details_Id=" + dk;
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
          

        }

    }

    protected void gridpurchaseregister_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
 
 
   
}

