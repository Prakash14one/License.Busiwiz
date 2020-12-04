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
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text;
using System.Text;
using System.Net;
using System.Net.Mail;


public partial class Register_purchase : System.Web.UI.Page
{
    // SqlConnection ifile = new SqlConnection(ConfigurationManager.ConnectionStrings["ifilecabinet"].ConnectionString);
    // SqlConnection con;
    // SqlConnection conn;

    SqlConnection con = new SqlConnection(PageConn.connnn);
    SqlConnection conn = new SqlConnection(PageConn.connnn);
    SqlConnection ifile = new SqlConnection(PageConn.connnn);
    string compid;
    object paramMissing = Type.Missing;
    public string errormessage;
    private bool wordavailable = false;
    private bool checkedword = false;
    public static string abcdy = "";
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

            // fillDefaultValueLabel();
            if (Request.QueryString["cty"] == "3435" && Request.QueryString["ty"] != null)
            {
                ddlwarehouse.SelectedValue = Request.QueryString["wid"];
                ddlwarehouse_SelectedIndexChanged(sender, e);
                rbtnlist.SelectedValue = "2";
                rbtnlist_SelectedIndexChanged(sender, e);
                ddlperiod.SelectedValue = Request.QueryString["ty"];
                btnSearchGo_Click(sender, e);
            }
            else
            {
                ddlwarehouse_SelectedIndexChanged(sender, e);


           ////////bool blperiod= ClsPeriod.rdperido(ddlwarehouse.SelectedValue);
                if (rbtnlist.SelectedValue == "1")
                {
                    pnlfromdatetodate.Visible = true;
                    pnlmonthyear.Visible = false;
                    pnlperiod.Visible = false;
                    String frmdt = System.DateTime.Now.Month.ToString() + "/1/" + System.DateTime.Now.Year.ToString();
                    txtfromdate.Text = Convert.ToDateTime(frmdt).ToShortDateString();
                    txttodate.Text = Convert.ToString(System.DateTime.Now.Date.ToShortDateString());

                }
                else if (rbtnlist.SelectedValue == "2")
                {
                    pnlfromdatetodate.Visible = false;
                    pnlmonthyear.Visible = false;
                    pnlperiod.Visible = true;
                    Fillddlperiod();
                }
                else
                {
                    pnlfromdatetodate.Visible = false;
                    pnlmonthyear.Visible = true;
                    pnlperiod.Visible = false;
                    Fillddlmonth();
                    Fillddlyear();
                }
            }
            Panel1.Visible = true;
            pageMailAccess();
        }
    }

    protected void alloperiod()
    {
        DataTable dt1111 = MainAcocount.SelectReportPeriodwithWhid(ddlwarehouse.SelectedValue);
        if (dt1111.Rows.Count > 0)
        {
            string datti = DateTime.Now.ToShortDateString();
            if (Convert.ToDateTime(datti) >= Convert.ToDateTime(dt1111.Rows[0]["StartDate"]) && Convert.ToDateTime(datti) <= Convert.ToDateTime(dt1111.Rows[0]["EndDate"]))
            {
                
                rbtnlist.Items[1].Enabled = true;
              
            }
            else
            {
                String frmdt = System.DateTime.Now.Month.ToString() + "/1/" + System.DateTime.Now.Year.ToString();
                txtfromdate.Text = Convert.ToDateTime(frmdt).ToShortDateString();
                txttodate.Text = Convert.ToString(System.DateTime.Now.Date.ToShortDateString());

                rbtnlist.SelectedValue = "1";
                rbtnlist.Items[1].Enabled = false;
            }
        }
    }
    protected void btnsubmit_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void rbtnlist_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtnlist.SelectedValue == "1")
        {
            pnlfromdatetodate.Visible = true;
            pnlmonthyear.Visible = false;
            pnlperiod.Visible = false;
            if (txtfromdate.Text.Length <= 0)
            {
                String frmdt = System.DateTime.Now.Month.ToString() + "/1/" + System.DateTime.Now.Year.ToString();
                txtfromdate.Text = Convert.ToDateTime(frmdt).ToShortDateString();
                txttodate.Text = Convert.ToString(System.DateTime.Now.Date.ToShortDateString());
            }
        }
        else if (rbtnlist.SelectedValue == "2")
        {
            pnlfromdatetodate.Visible = false;
            pnlmonthyear.Visible = false;
            pnlperiod.Visible = true;
            Fillddlperiod();
        }
        else
        {
            pnlfromdatetodate.Visible = false;
            pnlmonthyear.Visible = true;
            pnlperiod.Visible = false;
            Fillddlmonth();
            Fillddlyear();
        }

    }
    protected void Fillddlmonth()
    {
        String str = "SELECT  * FROM MonthMaster";

        SqlCommand cmd = new SqlCommand(str, con);
        cmd.CommandType = CommandType.Text;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        ddlmonth.DataSource = dt;
        ddlmonth.DataTextField = "MonthName";
        ddlmonth.DataValueField = "MonthMasterId";
        ddlmonth.DataBind();
        ddlmonth.Items.Insert(0, "All");

        ddlmonth.Items[0].Value = "0";
        ddlmonth.SelectedIndex = 0;
    }
    protected void Fillddlyear()
    {
        String str = "SELECT  * FROM Year_master order by Year desc";

        SqlCommand cmd = new SqlCommand(str, con);
        cmd.CommandType = CommandType.Text;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        ddlyear.DataSource = dt;
        ddlyear.DataTextField = "Year";
        ddlyear.DataValueField = "YearID";
        ddlyear.DataBind();
        ddlyear.Items.Insert(0, "All");

        ddlyear.Items[0].Value = "0";
        ddlyear.SelectedIndex = 0;
    }
    protected void Fillddlperiod()
    {

        String str = "SELECT  * FROM PeriodMaster";
        SqlCommand cmd = new SqlCommand(str, con);
        cmd.CommandType = CommandType.Text;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        ddlperiod.DataSource = dt;
        ddlperiod.DataTextField = "PeriodName";
        ddlperiod.DataValueField = "PeriodId";
        ddlperiod.DataBind();
        //ddlperiod.Items.Insert(0, "All");

        //ddlperiod.Items[0].Value = "0";
        ddlperiod.SelectedIndex = 0;
        object sender=new object();
        EventArgs e=new EventArgs();
        ddlperiod_SelectedIndexChanged(sender, e);
    }

    protected void Fillddlparty()
    {
        ddlparty.Items.Clear();
        if (ddlwarehouse.SelectedIndex > -1)
        {
            String str = "SELECT Party_master.PartyID , Convert(nvarchar(10),Party_master.PartyID)+':'+Party_master.Compname+':'+Party_master.Contactperson  as Compname , Party_master.PartyTypeId FROM Party_master inner join PartytTypeMaster on PartytTypeMaster.[PartyTypeId]=Party_master.PartyTypeId WHERE  Party_master.Whid='" + ddlwarehouse.SelectedValue + "' and PartType='Vendor'";

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
    protected void gridpurchaseregistser_RowCommand(object sender, GridViewCommandEventArgs e)
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
        if (e.CommandName == "remove")
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
            string te = "EditPurchaseInvoice.aspx?Purchase_Details_Id=" + dk;
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

        }

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
            gridpurchaseregister.Columns[5].HeaderText = CheckBoxList1.Items[5].Text;
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



    }
    protected void FillGrid()
    {

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
                DataTable dtrs = select(abcdy);
                if (dtrs.Rows.Count > 0)
                {

                  
                    ViewState["10"] = Convert.ToString(dtrs.Rows[0]["shippingandHandlingCharges"]);
                    ViewState["8"] = Convert.ToString(dtrs.Rows[0]["TexAmount1"]);
                    ViewState["9"] = Convert.ToString(dtrs.Rows[0]["TexAmount2"]);

                    ViewState["ta3"] = Convert.ToString(dtrs.Rows[0]["TexAmount3"]);
                    ViewState["11"] =Convert.ToString(dtrs.Rows[0]["TotalAmt"]);

                    ViewState["7"] = Convert.ToDecimal(ViewState["11"]) - Convert.ToDecimal(ViewState["ta3"]) - Convert.ToDecimal(ViewState["8"]) - Convert.ToDecimal(ViewState["9"]) - Convert.ToDecimal(ViewState["10"]);
                    //ViewState["7"] = nettotal.ToString();
                    //ViewState["10"] = shipchargetotal.ToString();
                    //ViewState["8"] = tax1total.ToString();
                    //ViewState["9"] = tax2total.ToString();

                    //ViewState["ta3"] = tax3total.ToString();
                    //ViewState["11"] = gsttltotal.ToString();
                }
                // GridViewRow ft = (GridViewRow)gridpurchaseregister.FooterRow;
                //gridpurchaseregister.FooterRow.Cells[3].ForeColor = System.Drawing.Color.White;
                //gridpurchaseregister.FooterRow.Cells[4].ForeColor = System.Drawing.Color.White;
                //gridpurchaseregister.FooterRow.Cells[5].ForeColor = System.Drawing.Color.White;
                //gridpurchaseregister.FooterRow.Cells[6].ForeColor = System.Drawing.Color.White;
                //gridpurchaseregister.FooterRow.Cells[7].ForeColor = System.Drawing.Color.White;
                //gridpurchaseregister.FooterRow.Cells[8].ForeColor = System.Drawing.Color.White;
                //gridpurchaseregister.FooterRow.Cells[9].ForeColor = System.Drawing.Color.White;

                gridpurchaseregister.FooterRow.Cells[3].Text = "Total :";
                gridpurchaseregister.FooterRow.Cells[4].Text = String.Format("{0:n}", Math.Round(Convert.ToDecimal(ViewState["7"]), 2));
                gridpurchaseregister.FooterRow.Cells[5].Text = String.Format("{0:n}", Math.Round(Convert.ToDecimal(ViewState["8"]), 2));
                gridpurchaseregister.FooterRow.Cells[6].Text = String.Format("{0:n}", Math.Round(Convert.ToDecimal(ViewState["9"]), 2));
                gridpurchaseregister.FooterRow.Cells[7].Text = String.Format("{0:n}", Math.Round(Convert.ToDecimal(ViewState["ta3"]), 2));
                gridpurchaseregister.FooterRow.Cells[8].Text = String.Format("{0:n}", Math.Round(Convert.ToDecimal(ViewState["10"]), 2));
                gridpurchaseregister.FooterRow.Cells[9].Text = String.Format("{0:n}", Math.Round(Convert.ToDecimal(ViewState["11"]), 2));
            }


        }
        else
        {
            gridpurchaseregister.DataSource = null;


            gridpurchaseregister.DataBind();
        }

    }

    private int GetRowCount(string str)
    {
        int count = 0;
        DataTable dte = new DataTable();
        dte = select(str);
        if (dte.Rows.Count > 0)
        {
            count += Convert.ToInt32(dte.Rows[0]["ci"]);
        }
        ViewState["count"] = count;
        return count;
    }

    private DataTable GetDataPage(int pageIndex, int pageSize, string sortExpression, string query)
    {
        DataTable dt = select(string.Format("SELECT Distinct * FROM (select TOP {0} ROW_NUMBER() OVER (ORDER BY {1}) as ROW_NUM,   " + " {2} ORDER BY ROW_NUM) innerSelect WHERE ROW_NUM > {3}", ((pageIndex + 1) * pageSize), sortExpression, query, (pageIndex * pageSize)));
        dt.Columns.Remove("ROW_NUM");
        return dt;

       // ViewState["dt"] = dt;
    }


    private DataTable GridFiltersDatatable()
    {

        DataTable dt = new DataTable();
        ViewState["StrMMM"] = null;
        string str = "";
        string str2 = "";

        if (chkitem.Checked == false)
        {
            str = "   TranctionMaster.Tranction_Master_Id,  PurchaseDetails.Purchase_Details_Id, PurchaseDetails.PartyName as Pid,Party_master.Compname as  PartyName,Convert(nvarchar(10),PurchaseDetails.Date,101) as Date, PurchaseDetails.TotalAmt," +
                          " PurchaseDetails.PurchaseInvoiceNumber, PurchaseDetails.TexAmount1, PurchaseDetails.texAmount2, PurchaseDetails.texAmount3, PurchaseDetails.shippingandHandlingCharges," +
                          " PurchaseDetails.EntryNumer, PurchaseDetails.WareHouseMasterId,TranctionMaster.EntryTypeId," +
                          " TranctionMaster.EntryNumber  FROM PurchaseDetails INNER JOIN " +
                            " TranctionMaster ON PurchaseDetails.TransId = TranctionMaster.Tranction_Master_Id INNER JOIN " +
                           "  Party_master ON PurchaseDetails.PartyName=Party_master.PartyID " +
                      " where PurchaseDetails.compid = '" + compid + "' and TranctionMaster.Whid='" + ddlwarehouse.SelectedValue + "' and PurchaseDetails.WareHouseMasterId='" + ddlwarehouse.SelectedValue + "'  and EntryTypeId='27' ";

            str2 = " select Count(PurchaseDetails.Purchase_Details_Id) as ci  FROM PurchaseDetails INNER JOIN " +
                            " TranctionMaster ON PurchaseDetails.TransId = TranctionMaster.Tranction_Master_Id INNER JOIN " +
                           "  Party_master ON PurchaseDetails.PartyName=Party_master.PartyID " +
                      " where PurchaseDetails.compid = '" + compid + "' and TranctionMaster.Whid='" + ddlwarehouse.SelectedValue + "' and PurchaseDetails.WareHouseMasterId='" + ddlwarehouse.SelectedValue + "'  and EntryTypeId='27' ";

            abcdy = "Select Sum(PurchaseDetails.TotalAmt) as TotalAmt,Sum(Cast(PurchaseDetails.TexAmount1 as decimal(18,2))) as TexAmount1,Sum(Cast(PurchaseDetails.texAmount2 as decimal(18,2))) as texAmount2,Sum(Cast(PurchaseDetails.TexAmount3 as decimal(18,2)))as texAmount3 ,Sum(Cast(PurchaseDetails.shippingandHandlingCharges as decimal(18,2)))as shippingandHandlingCharges " +
                          "  FROM PurchaseDetails INNER JOIN " +
                            " TranctionMaster ON PurchaseDetails.TransId = TranctionMaster.Tranction_Master_Id INNER JOIN " +
                           "  Party_master ON PurchaseDetails.PartyName=Party_master.PartyID " +
                      " where PurchaseDetails.compid = '" + compid + "' and TranctionMaster.Whid='" + ddlwarehouse.SelectedValue + "' and PurchaseDetails.WareHouseMasterId='" + ddlwarehouse.SelectedValue + "'  and EntryTypeId='27' ";

        }
        else
        {
            str = "   TranctionMaster.Tranction_Master_Id,  PurchaseDetails.Purchase_Details_Id, PurchaseDetails.PartyName as Pid,Party_master.Compname as  PartyName,Convert(nvarchar(10),PurchaseDetails.Date,101) as Date, PurchaseDetails.TotalAmt," +
                         " PurchaseDetails.PurchaseInvoiceNumber, PurchaseDetails.TexAmount1, PurchaseDetails.texAmount2, PurchaseDetails.texAmount3, PurchaseDetails.shippingandHandlingCharges," +
                         " PurchaseDetails.EntryNumer, PurchaseDetails.WareHouseMasterId,TranctionMaster.EntryTypeId," +
                         " TranctionMaster.EntryNumber FROM PurchaseMaster inner join PurchaseDetails on PurchaseDetails.Purchase_Details_Id=" +
                " PurchaseMaster.Purchase_Details_Id INNER JOIN  TranctionMaster ON PurchaseDetails.TransId = TranctionMaster.Tranction_Master_Id    inner join" +
                " Party_master ON PurchaseDetails.PartyName=Party_master.PartyID   " +
                  " where PurchaseMaster.InventoryWHM_Id = '" + ddlInvName.SelectedValue + "' and TranctionMaster.Whid='" + ddlwarehouse.SelectedValue + "' and PurchaseDetails.WareHouseMasterId='" + ddlwarehouse.SelectedValue + "' and EntryTypeId='27'";

            abcdy = "Select Sum(PurchaseDetails.TotalAmt) as TotalAmt,Sum(Cast(PurchaseDetails.TexAmount1 as decimal(18,2))) as TexAmount1,Sum(Cast(PurchaseDetails.texAmount2 as decimal(18,2))) as texAmount2,Sum(Cast(PurchaseDetails.TexAmount3 as decimal(18,2)))as texAmount3 ,Sum(Cast(PurchaseDetails.shippingandHandlingCharges as decimal(18,2)))as shippingandHandlingCharges " +
      "  FROM PurchaseMaster inner join PurchaseDetails on PurchaseDetails.Purchase_Details_Id=" +
" PurchaseMaster.Purchase_Details_Id INNER JOIN  TranctionMaster ON PurchaseDetails.TransId = TranctionMaster.Tranction_Master_Id    inner join" +
 " Party_master ON PurchaseDetails.PartyName=Party_master.PartyID   " +
                " where PurchaseMaster.InventoryWHM_Id = '" + ddlInvName.SelectedValue + "' and TranctionMaster.Whid='" + ddlwarehouse.SelectedValue + "' and PurchaseDetails.WareHouseMasterId='" + ddlwarehouse.SelectedValue + "' and EntryTypeId='27'";

            str2 = " select Count(PurchaseDetails.Purchase_Details_Id) as ci  FROM PurchaseMaster inner join PurchaseDetails on PurchaseDetails.Purchase_Details_Id=" +
" PurchaseMaster.Purchase_Details_Id INNER JOIN  TranctionMaster ON PurchaseDetails.TransId = TranctionMaster.Tranction_Master_Id    inner join" +
" Party_master ON PurchaseDetails.PartyName=Party_master.PartyID   " +
               " where PurchaseMaster.InventoryWHM_Id = '" + ddlInvName.SelectedValue + "' and TranctionMaster.Whid='" + ddlwarehouse.SelectedValue + "' and PurchaseDetails.WareHouseMasterId='" + ddlwarehouse.SelectedValue + "' and EntryTypeId='27'";

        }



        string sortExpression = "TranctionMaster.Date Desc";
        String strMMM = "";
        string between = "";
        if (rbtnlist.SelectedValue == "1")
        {
            between = " PurchaseDetails.Date  between '" + txtfromdate.Text + "' AND '" + txttodate.Text + "'";// + //2009-4-30' " +
        }
        else
        {
            between = " PurchaseDetails.Date  between '" + lblpfdate.Text + "' AND '" + lblptdate.Text + "'";// + //2009-4-30' " +

        }
        if (between != "")
        {
             strMMM = " and  " + between;

            if (ddlparty.SelectedIndex > 0)
            {
                 strMMM = strMMM+" and PartyID ='" + ddlparty.SelectedValue + "'";
               
            }
            str = str + strMMM;
            str2 = str2 + strMMM;
            abcdy = abcdy + strMMM;
            if (str != "")
            {
                gridpurchaseregister.VirtualItemCount = GetRowCount(str2);
                dt = GetDataPage(gridpurchaseregister.PageIndex, gridpurchaseregister.PageSize, sortExpression, str);
                return dt;
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
    private string GetDateBetweens()
    {
        String DtBtn = "";
        String DateBetween = "";
        if (rbtnlist.SelectedValue == "1")
        {

            // txttodate.Text = Convert.ToString(System.DateTime.Now.Date.ToShortDateString());

            DateBetween = (string)BetweenByFromdateTodate();

        }
        else if (rbtnlist.SelectedValue == "2")
        {
            DateBetween = (string)BetweenByPeriod();
        }
        else
        {
            DateBetween = (string)BetweenByYearMonth();
        }


        if (DtBtn.Length > 0)
        {
            DateBetween = DtBtn.ToString();
        }
        return DateBetween;


    }
    private string BetweenByFromdateTodate()
    {

        string str = " TranctionMaster.Date  between '" + txtfromdate.Text + "' AND '" + txttodate.Text + "'";// + //2009-4-30' " +

        return str;
    }
    private string BetweenByPeriod()
    {
        //date between you should use  date first and earlier date lateafter
        string Today, Yesterday, ThisYear;
        Today = Convert.ToString(System.DateTime.Today.ToShortDateString());
        Yesterday = Convert.ToString(System.DateTime.Today.AddDays(-1).ToShortDateString());
        ThisYear = Convert.ToString(System.DateTime.Today.Year.ToString());

        //-------------------this week start...............
        DateTime d1, d2, d3, d4, d5, d6, d7;
        DateTime weekstart, weekend;
        d1 = Convert.ToDateTime(System.DateTime.Today.ToShortDateString());
        d2 = Convert.ToDateTime(System.DateTime.Today.AddDays(-1).ToShortDateString());
        d3 = Convert.ToDateTime(System.DateTime.Today.AddDays(-2).ToShortDateString());
        d4 = Convert.ToDateTime(System.DateTime.Today.AddDays(-3).ToShortDateString());
        d5 = Convert.ToDateTime(System.DateTime.Today.AddDays(-4).ToShortDateString());
        d6 = Convert.ToDateTime(System.DateTime.Today.AddDays(-5).ToShortDateString());
        d7 = Convert.ToDateTime(System.DateTime.Today.AddDays(-6).ToShortDateString());
        string ThisWeek = (System.DateTime.Today.DayOfWeek.ToString());
        if (ThisWeek.ToString() == "Monday")
        {
            weekstart = d1;
            weekend = weekstart.Date.AddDays(+6);
        }
        else if (Convert.ToString(ThisWeek) == "Tuesday")
        {
            weekstart = d2;
            weekend = weekstart.Date.AddDays(+6);
        }
        else if (ThisWeek.ToString() == "Wednesday")
        {
            weekstart = d3;
            weekend = weekstart.Date.AddDays(+6);
        }
        else if (ThisWeek.ToString() == "Thursday")
        {
            weekstart = d4;
            weekend = weekstart.Date.AddDays(+6);
        }
        else if (ThisWeek.ToString() == "Friday")
        {
            weekstart = d5;
            weekend = weekstart.Date.AddDays(+6);
        }
        else if (ThisWeek.ToString() == "Saturday")
        {
            weekstart = d6;
            weekend = weekstart.Date.AddDays(+6);

        }
        else
        {
            weekstart = d7;
            weekend = weekstart.Date.AddDays(+6);
        }


        string thisweekstart = weekstart.ToString();
        //ViewState["SDate"] = thisweekstart;
        string thisweekend = weekend.ToString();

        //.................this week duration end.....................

        ///--------------------last week duration ....

        DateTime d17, d8, d9, d10, d11, d12, d13;
        DateTime lastweekstart, lastweekend;
        d17 = Convert.ToDateTime(System.DateTime.Today.AddDays(-7).ToShortDateString());
        d8 = Convert.ToDateTime(System.DateTime.Today.AddDays(-8).ToShortDateString());
        d9 = Convert.ToDateTime(System.DateTime.Today.AddDays(-9).ToShortDateString());
        d10 = Convert.ToDateTime(System.DateTime.Today.AddDays(-10).ToShortDateString());
        d11 = Convert.ToDateTime(System.DateTime.Today.AddDays(-11).ToShortDateString());
        d12 = Convert.ToDateTime(System.DateTime.Today.AddDays(-12).ToShortDateString());
        d13 = Convert.ToDateTime(System.DateTime.Today.AddDays(-13).ToShortDateString());
        string thisday = (System.DateTime.Today.DayOfWeek.ToString());
        if (thisday.ToString() == "Monday")
        {
            lastweekstart = d17;
            lastweekend = lastweekstart.Date.AddDays(+6);
        }
        else if (Convert.ToString(thisday) == "Tuesday")
        {
            lastweekstart = d8;
            lastweekend = lastweekstart.Date.AddDays(+6);
        }
        else if (thisday.ToString() == "Wednesday")
        {
            lastweekstart = d9;
            lastweekend = lastweekstart.Date.AddDays(+6);
        }
        else if (thisday.ToString() == "Thursday")
        {
            lastweekstart = d10;
            lastweekend = lastweekstart.Date.AddDays(+6);
        }
        else if (thisday.ToString() == "Friday")
        {
            lastweekstart = d11;
            lastweekend = lastweekstart.Date.AddDays(+6);
        }
        else if (thisday.ToString() == "Saturday")
        {
            lastweekstart = d12;
            lastweekend = lastweekstart.Date.AddDays(+6);

        }
        else
        {
            lastweekstart = d13;
            lastweekend = lastweekstart.Date.AddDays(+6);
        }
        string lastweekstartdate = lastweekstart.ToString();
        //ViewState["SDate"] = lastweekstartdate;
        string lastweekenddate = lastweekend.ToString();
        //---------------lastweek duration end.................

        //        Today
        //2	Yesterday
        //3	ThisWeek
        //4	LastWeek
        //5	ThisMonth
        //6	LastMonth
        //7	ThisQuarter
        //8	LastQuarter
        //9	ThisYear
        //10Last Year
        //------------------this month period-----------------

        DateTime thismonthstart = Convert.ToDateTime(System.DateTime.Now.Month.ToString() + "/1/" + System.DateTime.Now.Year.ToString());
        string thismonthstartdate = thismonthstart.ToShortDateString();
        //ViewState["SDate"] = thismonthstartdate;

        DateTime lastdate = new DateTime(thismonthstart.Year, thismonthstart.Month, 1).AddMonths(1).AddDays(-1);
        string thismonthenddate = lastdate.ToShortDateString();
        //------------------this month period end................



        //-----------------last month period start ---------------

        string lastmonthNumber = "12";
        int lastmonthno = Convert.ToInt32(thismonthstart.Month.ToString()) - 1;
        if (lastmonthno.ToString() == "0")
        {

        }
        else
        {
            lastmonthNumber = Convert.ToString(lastmonthno.ToString());
        }

        //int lastmonthno = Convert.ToInt32(thismonthstart.Month.ToString()) - 1;
        //string lastmonthNumber = Convert.ToString(lastmonthno.ToString());
        DateTime lastmonth = Convert.ToDateTime(lastmonthNumber.ToString() + "/1/" + ThisYear.ToString());
        string lastmonthstart = lastmonth.ToShortDateString();

        string lastmonthend = "";

        if (lastmonthNumber == "1" || lastmonthNumber == "3" || lastmonthNumber == "5" || lastmonthNumber == "7" || lastmonthNumber == "8" || lastmonthNumber == "10" || lastmonthNumber == "12")
        {
            lastmonthend = lastmonthNumber + "/31/" + ThisYear.ToString();
        }
        else if (lastmonthNumber == "4" || lastmonthNumber == "6" || lastmonthNumber == "9" || lastmonthNumber == "11")
        {
            lastmonthend = lastmonthNumber + "/30/" + ThisYear.ToString();
        }
        else
        {
            if (System.DateTime.IsLeapYear(Convert.ToInt32(ThisYear.ToString())))
            {
                lastmonthend = lastmonthNumber + "/29/" + ThisYear.ToString();
            }
            else
            {
                lastmonthend = lastmonthNumber + "/28/" + ThisYear.ToString();
            }
        }

        string lastmonthstartdate = lastmonthstart.ToString();
        //ViewState["SDate"] = lastmonthstartdate;
        string lastmonthenddate = lastmonthend.ToString();


        //-----------------last month period end -----------------------

        //--------------------this quater period start ----------------

        int thisqtr = 0;
        string thisqtrNumber = "";
        int mon = Convert.ToInt32(DateTime.Now.Month.ToString());
        if (mon >= 1 && mon <= 3)
        {
            thisqtr = 1;
            thisqtrNumber = "3";

        }
        else if (mon >= 4 && mon <= 6)
        {
            thisqtr = 4;
            thisqtrNumber = "6";
        }
        else if (mon >= 7 && mon <= 9)
        {
            thisqtr = 7;
            thisqtrNumber = "9";
        }
        else if (mon >= 10 && mon <= 12)
        {
            thisqtr = 10;
            thisqtrNumber = "12";
        }
        // int thisqtr = Convert.ToInt32(thismonthstart.AddMonths(-2).Month.ToString());
        // string thisqtrNumber = Convert.ToString(DateTime.Now.Month.ToString());

        DateTime thisquater = Convert.ToDateTime(thisqtr.ToString() + "/1/" + ThisYear.ToString());
        string thisquaterstart = thisquater.ToShortDateString();
        // string thisqtrNumber = Convert.ToString(thisqtr + 3);
        string thisquaterend = "";

        if (thisqtrNumber == "1" || thisqtrNumber == "3" || thisqtrNumber == "5" || thisqtrNumber == "7" || thisqtrNumber == "8" || thisqtrNumber == "10" || thisqtrNumber == "12")
        {
            thisquaterend = thisqtrNumber + "/31/" + ThisYear.ToString();
        }
        else if (thisqtrNumber == "4" || thisqtrNumber == "6" || thisqtrNumber == "9" || thisqtrNumber == "11")
        {
            thisquaterend = thisqtrNumber + "/30/" + ThisYear.ToString();
        }
        else
        {
            if (System.DateTime.IsLeapYear(Convert.ToInt32(ThisYear.ToString())))
            {
                thisquaterend = thisqtrNumber + "/29/" + ThisYear.ToString();
            }
            else
            {
                thisquaterend = thisqtrNumber + "/28/" + ThisYear.ToString();
            }
        }

        string thisquaterstartdate = thisquaterstart.ToString();
        // ViewState["SDate"] = thisquaterstartdate;
        string thisquaterenddate = thisquaterend.ToString();

        // --------------this quater period end ------------------------

        // --------------last quater period start----------------------

        //int lastqtr = Convert.ToInt32(thismonthstart.AddMonths(-5).Month.ToString());// -5;
        //string lastqtrNumber = Convert.ToString(lastqtr.ToString());
        //int lastqater3 = Convert.ToInt32(thismonthstart.AddMonths(-3).Month.ToString());
        int lastqtr = Convert.ToInt32(Convert.ToDateTime(thisquaterstartdate).AddMonths(-3).Month.ToString());// -5;
        string lastqtrNumber = Convert.ToString(lastqtr.ToString());
        int lastqater3 = Convert.ToInt32(Convert.ToDateTime(thisquaterenddate).AddMonths(-3).Month.ToString());

        //DateTime lastqater3 = Convert.ToDateTime(System.DateTime.Now.AddMonths(-3).Month.ToString());
        string lasterquater3 = lastqater3.ToString();
        DateTime lastquater = Convert.ToDateTime(lastqtrNumber.ToString() + "/1/" + ThisYear.ToString());
        string lastquaterstart = lastquater.ToShortDateString();
        string lastquaterend = "";

        if (lasterquater3 == "1" || lasterquater3 == "3" || lasterquater3 == "5" || lasterquater3 == "7" || lasterquater3 == "8" || lasterquater3 == "10" || lasterquater3 == "12")
        {
            lastquaterend = lasterquater3 + "/31/" + ThisYear.ToString();
        }
        else if (lasterquater3 == "4" || lasterquater3 == "6" || lasterquater3 == "9" || lasterquater3 == "11")
        {
            lastquaterend = lasterquater3 + "/30/" + ThisYear.ToString();
        }
        else
        {
            if (System.DateTime.IsLeapYear(Convert.ToInt32(ThisYear.ToString())))
            {
                lastquaterend = lasterquater3 + "/29/" + ThisYear.ToString();
            }
            else
            {
                lastquaterend = lasterquater3 + "/28/" + ThisYear.ToString();
            }
        }

        string lastquaterstartdate = lastquaterstart.ToString();
        //ViewState["SDate"] = lastquaterstartdate;
        string lastquaterenddate = lastquaterend.ToString();

        //--------------last quater period end-------------------------

        //--------------this year period start----------------------
        DateTime thisyearstart = Convert.ToDateTime("1/1/" + ThisYear.ToString());
        DateTime thisyearend = Convert.ToDateTime("12/31/" + ThisYear.ToString());

        string thisyearstartdate = thisyearstart.ToShortDateString();
        //ViewState["SDate"] = thisyearstartdate;
        string thisyearenddate = thisyearend.ToShortDateString();
        DataTable dt = MainAcocount.SelectReportPeriodwithWhid(ddlwarehouse.SelectedValue);

        if (dt.Rows.Count > 0)
        {
            thisyearstartdate = Convert.ToDateTime(dt.Rows[0]["StartDate"]).ToShortDateString();
            thisyearenddate = Convert.ToDateTime(dt.Rows[0]["EndDate"]).ToShortDateString();
        }
        //---------------this year period end-------------------
        //--------------this year period start----------------------
        //DateTime lastyearstart = Convert.ToDateTime("1/1/" + System.DateTime.Today.AddYears(-1).Year.ToString());
        //DateTime lastyearend = Convert.ToDateTime("12/31/" + System.DateTime.Today.AddYears(-1).Year.ToString());
        DateTime lastyearstart = Convert.ToDateTime("1/1/" + System.DateTime.Today.AddYears(-1).Year.ToString());
        DateTime lastyearend = Convert.ToDateTime("12/31/" + System.DateTime.Today.AddYears(-1).Year.ToString());

        lastyearstart = Convert.ToDateTime(thisyearstartdate).AddYears(-1);
        lastyearend = Convert.ToDateTime(thisyearenddate).AddYears(-1);



        string lastyearstartdate = lastyearstart.ToShortDateString();
        // ViewState["SDate"] = lastyearstartdate;
        string lastyearenddate = lastyearend.ToShortDateString();



        //---------------this year period end-------------------


        string periodstartdate = "";
        string periodenddate = "";

        if (ddlperiod.SelectedItem.Text == "Today")
        {

            periodstartdate = Today.ToString();
            periodenddate = Today.ToString();

        }
        else if (ddlperiod.SelectedItem.Text == "Yesterday")
        {
            periodstartdate = Yesterday.ToString();
            periodenddate = Yesterday.ToString();

        }
        else if (ddlperiod.SelectedItem.Text == "Current Week")
        {

            periodstartdate = thisweekstart.ToString();
            periodenddate = thisweekend.ToString();

        }
        else if (ddlperiod.SelectedItem.Text == "Last Week")
        {

            periodstartdate = lastweekstartdate.ToString();
            //periodenddate = Today.ToString();
            periodenddate = lastweekenddate;

        }
        else if (ddlperiod.SelectedItem.Text == "Current Month")
        {


            periodstartdate = thismonthstart.ToString();
            periodenddate = thismonthenddate.ToString();
        }
        else if (ddlperiod.SelectedItem.Text == "Last Month")
        {

            periodstartdate = lastmonthstartdate.ToString();
            periodenddate = lastmonthenddate.ToString();

        }
        else if (ddlperiod.SelectedItem.Text == "Current Quarter")
        {
            periodstartdate = thisquaterstartdate.ToString();
            periodenddate = thisquaterenddate.ToString();

        }
        else if (ddlperiod.SelectedItem.Text == "Last Quarter")
        {

            periodstartdate = lastquaterstartdate.ToString();
            periodenddate = lastquaterenddate.ToString();

        }

        else if (ddlperiod.SelectedItem.Text == "Current Year")
        {


            if (dt.Rows.Count > 0)
            {
                periodstartdate = dt.Rows[0]["StartDate"].ToString();
                periodenddate = dt.Rows[0]["EndDate"].ToString();

            }


        }
        else if (ddlperiod.SelectedItem.Text == "Last Year")
        {
            DataTable ds121 = select("select top(1) StartDate,Enddate from ReportPeriod  where EndDate<(Select Distinct EndDate from ReportPeriod  where Whid='" + ddlwarehouse.SelectedValue + "' and Active='1') and Whid='" + ddlwarehouse.SelectedValue + "' order by EndDate Desc");

            if (ds121.Rows.Count > 0)
            {
                periodstartdate = ds121.Rows[0]["StartDate"].ToString();
                periodenddate = ds121.Rows[0]["EndDate"].ToString();

            }
        }
        else if (ddlperiod.SelectedItem.Text == "All")
        {
            if (dt.Rows.Count > 0)
            {

                periodstartdate = dt.Rows[0]["StartDate"].ToString();
                periodenddate = dt.Rows[0]["EndDate"].ToString();

            }

        }

        DateTime sd = Convert.ToDateTime(periodstartdate.ToString());
        DateTime ed = Convert.ToDateTime(periodenddate.ToString());


        //string str = "  TranctionMaster.Date between '" + sd.ToShortDateString() + "' AND '" + ed.ToShortDateString() + "'"; // + //2009-4-30' " +
        string str = "  PurchaseDetails.Date between '" + sd.ToShortDateString() + "' AND '" + ed.ToShortDateString() + "' "; // + //2009-4-30' " +
        ViewState["startdateOfOpeningBalance"] = sd.ToShortDateString();
        ViewState["SDate"] = sd.ToShortDateString();
        ViewState["endDate"] = ed.ToShortDateString();
        lblpfdate.Text = ViewState["SDate"].ToString();
        lblptdate.Text = ViewState["endDate"].ToString();
        return str;
    }
    private string BetweenByYearMonth()
    {
        string currentyear = System.DateTime.Today.Year.ToString();
        string currentmonth = System.DateTime.Today.Month.ToString();

        string selectedyear = Convert.ToString(ddlyear.SelectedItem.Text);
        string selectedmonth = Convert.ToString(ddlmonth.SelectedItem.Text);
        string selectedmonthNumber = "";
        string ThisYear = Convert.ToString(System.DateTime.Today.Year.ToString());

        //DateTime datebyyearmonth = Convert.ToDateTime(selectedmonthNumber.ToString() + "/1/" + selectedyear.ToString()).ToShortDateString();

        //string datebyyearmonthdate = datebyyearmonth.ToShortDateString();

        string yearmonthstartdate = "";
        string yearmonthenddate = "";

        string month = "";
        string year = "";

        string str = "";
        DataTable dt = MainAcocount.SelectReportPeriodwithWhid(ddlwarehouse.SelectedValue);

        if (dt.Rows.Count > 0)
        {

            if (ddlmonth.SelectedItem.Text == "All" && ddlyear.SelectedItem.Text == "All")
            {

                yearmonthstartdate = Convert.ToDateTime(dt.Rows[0][0]).ToShortDateString();
                yearmonthenddate = Convert.ToDateTime(dt.Rows[0][1]).ToShortDateString();
            }
            else if (ddlmonth.SelectedItem.Text != "All" && ddlyear.SelectedItem.Text == "All")
            {
                switch (selectedmonth)
                {
                    case "January":
                        selectedmonthNumber = "1";
                        break;

                    case "Feb":
                        selectedmonthNumber = "2";
                        break;
                    case "March":
                        selectedmonthNumber = "3";
                        break;
                    case "April":
                        selectedmonthNumber = "4";
                        break;
                    case "May":
                        selectedmonthNumber = "5";
                        break;
                    case "June":
                        selectedmonthNumber = "6";
                        break;
                    case "July":
                        selectedmonthNumber = "7";
                        break;
                    case "August":
                        selectedmonthNumber = "8";
                        break;
                    case "September":
                        selectedmonthNumber = "9";
                        break;
                    case "Octomber":
                        selectedmonthNumber = "10";
                        break;
                    case "November":
                        selectedmonthNumber = "11";
                        break;
                    case "December":
                        selectedmonthNumber = "12";
                        break;
                }
                string days = "";
                if (selectedmonthNumber == "1" || selectedmonthNumber == "3" || selectedmonthNumber == "5" || selectedmonthNumber == "7" || selectedmonthNumber == "9" || selectedmonthNumber == "11")
                {
                    days = "31";
                }
                else if (selectedmonthNumber == "4" || selectedmonthNumber == "6" || selectedmonthNumber == "8" || selectedmonthNumber == "10" || selectedmonthNumber == "12")
                {
                    days = "30";
                }
                else
                {
                    if (System.DateTime.IsLeapYear(Convert.ToInt32(ThisYear.ToString())))
                    {
                        days = "29";
                    }
                    else
                    {
                        days = "28";
                    }
                }
                //DateTime selectedmonthonly = Convert.ToDateTime(selectedmonthNumber.ToString() + "/1/" + "1900").ToShortDateString();

                month = selectedmonthNumber.ToString();
                year = System.DateTime.Now.Year.ToString();
                //day = days;


                yearmonthstartdate = Convert.ToDateTime(month + "/1/" + currentyear.ToString()).ToShortDateString();
                yearmonthenddate = Convert.ToDateTime(month + "/" + days + "/" + currentyear.ToString()).ToShortDateString();


            }
            else if (ddlmonth.SelectedItem.Text == "All" && ddlyear.SelectedItem.Text != "All")
            {
                //month = Convert.ToString(System.DateTime.Now.Month);
                year = selectedyear.ToString();


                yearmonthstartdate = Convert.ToDateTime("1" + "/1/" + year.ToString()).ToShortDateString();
                yearmonthenddate = Convert.ToDateTime("12" + "/31/" + year.ToString()).ToShortDateString();



            }
            else
            {


                switch (selectedmonth)
                {
                    case "January":
                        selectedmonthNumber = "1";
                        break;

                    case "Feb":
                        selectedmonthNumber = "2";
                        break;
                    case "March":
                        selectedmonthNumber = "3";
                        break;
                    case "April":
                        selectedmonthNumber = "4";
                        break;
                    case "May":
                        selectedmonthNumber = "5";
                        break;
                    case "June":
                        selectedmonthNumber = "6";
                        break;
                    case "July":
                        selectedmonthNumber = "7";
                        break;
                    case "August":
                        selectedmonthNumber = "8";
                        break;
                    case "September":
                        selectedmonthNumber = "9";
                        break;
                    case "Octomber":
                        selectedmonthNumber = "10";
                        break;
                    case "November":
                        selectedmonthNumber = "11";
                        break;
                    case "December":
                        selectedmonthNumber = "12";
                        break;
                }
                string days = "";
                if (selectedmonthNumber == "1" || selectedmonthNumber == "3" || selectedmonthNumber == "5" || selectedmonthNumber == "7" || selectedmonthNumber == "8" || selectedmonthNumber == "10" || selectedmonthNumber == "12")
                {
                    days = "31";
                }
                else if (selectedmonthNumber == "4" || selectedmonthNumber == "6" || selectedmonthNumber == "9" || selectedmonthNumber == "11")
                {
                    days = "30";
                }
                else
                {
                    if (System.DateTime.IsLeapYear(Convert.ToInt32(ThisYear.ToString())))
                    {
                        days = "29";
                    }
                    else
                    {
                        days = "28";
                    }
                }
                //DateTime selectedmonthonly = Convert.ToDateTime(selectedmonthNumber.ToString() + "/1/" + "1900").ToShortDateString();

                month = selectedmonthNumber.ToString();
                year = selectedyear.ToString();

                yearmonthstartdate = Convert.ToDateTime(month + "/1/" + year.ToString()).ToShortDateString();
                yearmonthenddate = Convert.ToDateTime(month + "/" + days + "/" + year.ToString()).ToShortDateString();


            }



            //DateTime yearmonth = Convert.ToDateTime(month.ToString() + "/1/" + year.ToString()).ToShortDateString();
            string YearMonthStartfrom = yearmonthstartdate.ToString();
            string YearMonthEndTo = yearmonthenddate.ToString();
            if (Convert.ToDateTime(YearMonthStartfrom) < Convert.ToDateTime(dt.Rows[0][0].ToString()))
            {
               // lblstartdate.Text = Convert.ToDateTime(dt.Rows[0][0]).ToShortDateString();
               // ModalPopupExtender1.Show();

            }
            else
            {
                str = " TranctionMaster.Date between   '" + YearMonthStartfrom.ToString() + "' AND '" + YearMonthEndTo.ToString() + "'"; // + //2009-4-30' " +

            }



        }

        return str;
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
        alloperiod();
        rbtnlist_SelectedIndexChanged(sender, e);
        DataTable dtdaopb = (DataTable)select("select Name from [ReportPeriod] where  Whid='" + ddlwarehouse.SelectedValue + "' and Active='1'");
        if (dtdaopb.Rows.Count > 0)
        {
            string[] separator1 = new string[] { " TO " };
            string[] strSplitArr1 = dtdaopb.Rows[0]["Name"].ToString().Split(separator1, StringSplitOptions.RemoveEmptyEntries);


            ddlyear.Items.Insert(0, strSplitArr1[0]);
            ddlyear.Items[0].Value = strSplitArr1[0];
            ddlyear.Items.Insert(1, strSplitArr1[1]);
            ddlyear.Items[1].Value = strSplitArr1[1];
        }
       
        Fillddlparty();
        ITEM();
        gridtax();

    }



    protected void ITEM()
    {
        DataTable ds = select("SELECT   distinct  InventoryMaster.Name + ' : ' + Case when( InventoryMaster.ProductNo IS NULL) then '-' else InventoryMaster.ProductNo End+ ' : ' + cast(InventoryWarehouseMasterTbl.Weight as nvarchar(50))+ ' : ' + UnitTypeMaster.Name AS Category, InventoryCategoryMaster.InventoryCatName, InventorySubCategoryMaster.InventorySubCatName, InventoruSubSubCategory.InventorySubSubName, InventoryMaster.Name, InventoryMaster.InventoryMasterId, InventoryWarehouseMasterTbl.InventoryWarehouseMasterId,InventoryWarehouseMasterTbl.WareHouseId, InventoryWarehouseMasterTbl.Active FROM         InventoryMaster INNER JOIN InventoruSubSubCategory ON InventoryMaster.InventorySubSubId = InventoruSubSubCategory.InventorySubSubId INNER JOIN InventorySubCategoryMaster ON InventoruSubSubCategory.InventorySubCatID = InventorySubCategoryMaster.InventorySubCatId INNER JOIN InventoryCategoryMaster ON InventorySubCategoryMaster.InventoryCategoryMasterId = InventoryCategoryMaster.InventeroyCatId INNER JOIN InventoryWarehouseMasterTbl ON InventoryMaster.InventoryMasterId = InventoryWarehouseMasterTbl.InventoryMasterId Inner Join UnitTypeMaster on UnitTypeMaster.UnitTypeId=InventoryWarehouseMasterTbl.UnitTypeId WHERE  InventoryCategoryMaster.CatType IS NULL and   (InventoryWarehouseMasterTbl.Active = 1) and InventoryMaster.MasterActiveStatus=1 and (InventoryWarehouseMasterTbl.WareHouseId='" + ddlwarehouse.SelectedValue + "') ORDER BY Category ");
        if (ds.Rows.Count > 0)
        {
            ddlInvName.DataSource = ds;
            ddlInvName.DataTextField = "Category";
            ddlInvName.DataValueField = "InventoryWarehouseMasterId";
            ddlInvName.DataBind();
        }


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

        if (chkitem.Checked == true)
        {
            lblinv.Text = "Item Name : " + ddlInvName.SelectedItem.Text;
        }
        else
        {
            lblinv.Text = "";
        }
        lblparty.Text = ddlparty.SelectedItem.Text;

        gridpurchaseregister.DataSource = null;
        gridpurchaseregister.DataBind();
        if (Button5.Text == "Print and Export")
        {
            if (Convert.ToString(Session["aledi"]) != "Nn")
            {
                gridpurchaseregister.Columns[11].Visible = true;
            }
            if (Convert.ToString(Session["aldel"]) != "Nn")
            {
                gridpurchaseregister.Columns[12].Visible = true;
            }
        }
        if (ddlwarehouse.SelectedIndex > -1)
        {
            lblstore.Text = ddlwarehouse.SelectedItem.Text.ToString();
            DataTable dt1111 = select("select Convert(nvarchar,StartDate,101) as StartDate, Convert(nvarchar,EndDate,101) as EndDate from [ReportPeriod] where whid='" + ddlwarehouse.SelectedValue + "'  and Active='1'");
            if (dt1111.Rows.Count > 0)
            {
                if (rbtnlist.SelectedValue == "1")
                {
                    lblddf.Text = "From Date : " + txtfromdate.Text + "  To Date : " + txttodate.Text;
                    if (Convert.ToDateTime(txtfromdate.Text) >= Convert.ToDateTime(dt1111.Rows[0]["StartDate"]) && Convert.ToDateTime(txttodate.Text) <= Convert.ToDateTime(dt1111.Rows[0]["EndDate"]))
                    {

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
                    }
                    else
                    {
                        // lblstartdate.Text = dt1111.Rows[0][0].ToString();
                        ModalPopupExtenderAcy.Show();
                    }

                }
                else if (rbtnlist.SelectedValue == "2")
                {
                    lblddf.Text = "From Date : " + lblpfdate.Text + "  To Date : " + lblptdate.Text;
       
                    if (Convert.ToDateTime(lblpfdate.Text) >= Convert.ToDateTime(dt1111.Rows[0]["StartDate"]) && Convert.ToDateTime(lblptdate.Text) <= Convert.ToDateTime(dt1111.Rows[0]["EndDate"]))
                    {

                        Panel1.Visible = true;
                        btngo_Click(sender, e);

                    }
                    else
                    {
                        // lblstartdate.Text = dt1111.Rows[0][0].ToString();
                        ModalPopupExtenderAcy.Show();

                    }
                }



            }
        }
        else
        {
            Label1.Visible = true;
            Label1.Text = "Your account year not activated";

        }
    }
    protected void btnok_Click(object sender, EventArgs e)
    {
        ModalPopupExtenderAcy.Hide();
        Panel1.Visible = true;
        gridpurchaseregister.Columns[11].Visible = false;
        gridpurchaseregister.Columns[12].Visible = false;
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
    protected int delqtycount(string maininv, string traid)
    {
        int Anr = 0;
        DataTable Datfi = select("SELECT IWMAvgCostId,InvWMasterId  FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId  IN(" + maininv + ") and Tranction_Master_Id='" + traid + "'");
        foreach (DataRow intvay in Datfi.Rows)
        {

            decimal oLDqtyONHAND = 0;

            DataTable drtinvdata = new DataTable();
            drtinvdata = select("SELECT top(1) QtyonHand,Rate,AvgCost,Qty,DateUpdated,IWMAvgCostId FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + intvay["InvWMasterId"] + "' and DateUpdated<='" + ViewState["sdo1"] + "' and Tranction_Master_Id<'" + traid + "'  order by DateUpdated Desc,Tranction_Master_Id Desc,IWMAvgCostId Desc ");
            if (drtinvdata.Rows.Count == 0)
            {
                drtinvdata = select("SELECT top(1) QtyonHand,Rate,AvgCost,Qty,DateUpdated,IWMAvgCostId FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + intvay["InvWMasterId"] + "' and DateUpdated<'" + ViewState["sdo1"] + "'  order by DateUpdated Desc,Tranction_Master_Id Desc,IWMAvgCostId Desc ");

            }
            if (drtinvdata.Rows.Count > 0)
            {

                if (Convert.ToString(drtinvdata.Rows[0]["QtyonHand"]) != "")
                {
                    oLDqtyONHAND = Convert.ToDecimal(drtinvdata.Rows[0]["QtyonHand"]);
                }
            }
            decimal Finalqtyhand = 0;

            Finalqtyhand = oLDqtyONHAND;

            DataTable Dataupval = select("SELECT  QtyonHand,Rate,AvgCost,Qty,Tranction_Master_Id,IWMAvgCostId,DateUpdated FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + intvay["InvWMasterId"] + "' and DateUpdated>='" + ViewState["sdo1"] + "' order by DateUpdated Asc,Tranction_Master_Id Asc,IWMAvgCostId Asc");
            string ABC = "";
            foreach (DataRow itm in Dataupval.Rows)
            {
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

                    Finalqtyhand = Finalqtyhand + Convert.ToDecimal(itm["Qty"]);
                    if (Finalqtyhand < 0)
                    {
                        Anr = 1;
                        break;

                    }
                }
            }
        }
        return Anr;
    }
    protected void yes_Click(object sender, EventArgs e)
    {
        string maininv1 = "";
        int ly1 = 0;
        SqlDataAdapter dtpsel = new SqlDataAdapter("select PurchaseDetails.EntryNumer, PurchaseMaster.*,TranctionMaster.Tranction_Master_Id, TranctionMaster.EntryTypeId,PurchaseDetails.Purchase_Details_Id,TranctionMaster.Date as Td from PurchaseMaster Inner join PurchaseDetails on PurchaseMaster.Purchase_Details_Id = PurchaseDetails.Purchase_Details_Id  inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=PurchaseDetails.TransId  where PurchaseDetails.Purchase_Details_Id='" + Convert.ToInt32(ViewState["Dk"]) + "' and  TranctionMaster.EntryTypeId ='27'  and TranctionMaster.Whid='" + ddlwarehouse.SelectedValue + "'", con);

        DataTable dtsel = new DataTable();
        dtpsel.Fill(dtsel);
        ViewState["sdo1"] = Convert.ToDateTime(dtsel.Rows[0]["Td"]).ToShortDateString();
        int trMstId = Convert.ToInt32(dtsel.Rows[0]["Tranction_Master_Id"]);
        if (dtsel.Rows.Count > 0)
        {
            foreach (DataRow dr in dtsel.Rows)
            {
                if (ly1 > 0)
                {
                    maininv1 = maininv1 + ",";
                }
                else
                {
                    ly1 += 1;
                }

                maininv1 = maininv1 + dr["InventoryWHM_Id"].ToString();
            }
        }
        int abcd = delqtycount(maininv1, trMstId.ToString());
        if (abcd == 0)
        {

            if (conn.State.ToString() != "Open")
            {
                conn.Open();

            }


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
                    if (trMstId.ToString() != "0")
                    {
                        if (trMstId.ToString() != "")
                        {
                            srdelTrasnm = " delete  TranctionMaster where Tranction_Master_Id='" + trMstId + "' ";

                            stdeltrasD = " delete Tranction_Details where Tranction_Master_Id='" + trMstId + "' ";

                            DelTrsSupl = " delete TranctionMasterSuppliment where Tranction_Master_Id='" + Convert.ToInt32(trMstId) + "' ";


                            DelPayAppln = " delete PaymentApplicationTbl where TranMIdPayReceived='" + Convert.ToInt32(trMstId) + "' ";


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
                con.Close();
                string maininv = "";
                int ly = 0;
                if (dtsel.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtsel.Rows)
                    {
                        if (ly > 0)
                        {
                            maininv = maininv + ",";
                        }
                        else
                        {
                            ly += 1;
                        }

                        maininv = maininv + dr["InventoryWHM_Id"].ToString();

                        //SqlCommand cmdup1 = new SqlCommand("Delete InventoryWarehouseMasterAvgCostTbl where InvWMasterId='" + dr["InventoryWHM_Id"].ToString() + "'and Tranction_Master_Id='" + dr["Tranction_Master_Id"].ToString() + "'", conn);
                        //cmdup1.Transaction = transaction;

                        //cmdup1.ExecuteNonQuery();

                    }
                    DELAVGCOST(maininv, trMstId.ToString());
                }


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
        else
        {
            Label1.Visible = true;
            Label1.Text = "You cannot delete this entry as it may result into negative quantity in this item because of sale entry made after the date of this invoice";
        }
    }
    protected void DELAVGCOST(string maininv, string traid)
    {
        DataTable Datfi = select("SELECT IWMAvgCostId,InvWMasterId  FROM  InventoryWarehouseMasterAvgCostTbl where InvWMasterId  IN(" + maininv + ") and Tranction_Master_Id='" + traid + "'");
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
                    //transaction.Commit();
                    //con.Close();
                }
            }
        }
    }
    protected void Button661_Click(object sender, ImageClickEventArgs e)
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
        if (Button5.Text == "Print and Export")
        {
            //Panel1.ScrollBars = ScrollBars.None;
            //Panel1.Height = new Unit("100%");

            Button5.Text = "Hide Print and Export";
            Button3.Visible = true;

            ddlExport.Visible = true;

            gridpurchaseregister.AllowPaging = false;
            gridpurchaseregister.PageSize = 100000;
            FillGrid();            

            if (gridpurchaseregister.Columns[10].Visible == true)
            {
                ViewState["viewd"] = "tt";
                gridpurchaseregister.Columns[10].Visible = false;
            }
            if (gridpurchaseregister.Columns[11].Visible == true)
            {
                ViewState["editHide"] = "tt";
                gridpurchaseregister.Columns[11].Visible = false;
            }
            if (gridpurchaseregister.Columns[12].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                gridpurchaseregister.Columns[12].Visible = false;
            }
            if (gridpurchaseregister.Columns[13].Visible == true)
            {
                ViewState["Viewdd"] = "tt";
                gridpurchaseregister.Columns[13].Visible = false;
            }


        }
        else
        {

            //Panel1.ScrollBars = ScrollBars.Vertical;
            //Panel1.Height = new Unit(250);

            Button5.Text = "Print and Export";
            Button3.Visible = false;

            ddlExport.Visible = false;

            gridpurchaseregister.AllowPaging = true;
            gridpurchaseregister.PageSize = 25;
            FillGrid();

            if (ViewState["viewd"] != null)
            {
                gridpurchaseregister.Columns[10].Visible = true;
            }
            if (ViewState["editHide"] != null)
            {
                gridpurchaseregister.Columns[11].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                gridpurchaseregister.Columns[12].Visible = true;
            }
            if (ViewState["Viewdd"] != null)
            {
                gridpurchaseregister.Columns[13].Visible = true;
            }
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
            string te = "EditPurchaseInvoice.aspx?Purchase_Details_Id=" + dk;
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);


        }

    }

    protected void gridpurchaseregister_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void chkitem_CheckedChanged(object sender, EventArgs e)
    {
        if (chkitem.Checked == true)
        {
            pnlitem.Visible = true;
        }
        else
        {
            pnlitem.Visible = false;
        }
    }
    protected void ddlExport_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (gridpurchaseregister.Rows.Count > 0)
        {
            
           // Button1_Click1(sender, e);

            if (ddlExport.SelectedValue == "1" || ddlExport.SelectedValue == "4")
            {   
                Response.Buffer = true;
                Response.Charset = "";
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                string filename = "GrdM_" + System.DateTime.Today.Day + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second;
                Panel1.RenderControl(hw);
                string style = "";
                string path = HttpContext.Current.Server.MapPath("TempDoc/" + filename + ".Doc");
                System.IO.File.WriteAllText(path, style + sw.ToString());

                //set exportformat to pdf
                Microsoft.Office.Interop.Word.WdExportFormat paramExportFormat = Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF;
                bool paramOpenAfterExport = false;
                Microsoft.Office.Interop.Word.WdExportOptimizeFor paramExportOptimizeFor = Microsoft.Office.Interop.Word.WdExportOptimizeFor.wdExportOptimizeForPrint;
                Microsoft.Office.Interop.Word.WdExportRange paramExportRange = Microsoft.Office.Interop.Word.WdExportRange.wdExportAllDocument;
                int paramStartPage = 0;
                int paramEndPage = 0;

                Microsoft.Office.Interop.Word.WdExportItem paramExportItem = Microsoft.Office.Interop.Word.WdExportItem.wdExportDocumentContent;
                bool paramIncludeDocProps = true;
                bool paramKeepIRM = true;
                Microsoft.Office.Interop.Word.WdExportCreateBookmarks paramCreateBookmarks = Microsoft.Office.Interop.Word.WdExportCreateBookmarks.wdExportCreateWordBookmarks;

                bool paramDocStructureTags = true;
                bool paramBitmapMissingFonts = true;
                bool paramUseISO19005_1 = true;
                object paramSourceDocPath = HttpContext.Current.Server.MapPath("TempDoc/" + filename + ".Doc");
                string paramExportFilePath = HttpContext.Current.Server.MapPath("TempDoc/" + filename + ".pdf");
                Session["Emfile"] = filename + ".pdf";
                Session["GrdmailA"] = null;

                Microsoft.Office.Interop.Word.Application wordApp = null;
                wordApp = new Microsoft.Office.Interop.Word.Application();

                wordApp.Documents.Open(ref paramSourceDocPath, ref paramMissing, ref paramMissing,
                                            ref paramMissing, ref paramMissing, ref paramMissing,
                                            ref paramMissing, ref paramMissing, ref paramMissing,
                                            ref paramMissing, ref paramMissing, ref paramMissing,
                                            ref paramMissing, ref paramMissing, ref paramMissing,
                                            ref paramMissing);

                wordApp.ActiveDocument.ExportAsFixedFormat(paramExportFilePath,
                                                                        paramExportFormat, paramOpenAfterExport,
                                                                        paramExportOptimizeFor, paramExportRange, paramStartPage,
                                                                        paramEndPage, paramExportItem, paramIncludeDocProps,
                                                                        paramKeepIRM, paramCreateBookmarks, paramDocStructureTags,
                                                                        paramBitmapMissingFonts, paramUseISO19005_1,
                                                                        ref paramMissing);


                if (wordApp != null)
                {
                    wordApp.Quit(ref paramMissing, ref paramMissing, ref paramMissing);

                    wordApp = null;
                }
                if (ddlExport.SelectedValue == "4")
                {

                    string te = "MessageComposeExt.aspx?ema=Azxcvyute";
                    try
                    {
                        System.Threading.Thread.Sleep(100);
                        File.Delete(path);
                    }
                    catch
                    {
                    }
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

                }
                else
                {
                    Response.Clear();
                    Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.pdf");
                    Response.ContentType = "application/pdf";
                    Response.TransmitFile(paramExportFilePath);
                    Response.End();
                }


            }
            else if (ddlExport.SelectedValue == "2")
            {
                Response.Clear();

                Response.Buffer = true;

                Response.AddHeader("content-disposition",

                "attachment;filename=GridViewExport.xls");

                Response.Charset = "";

                Response.ContentType = "application/vnd.ms-excel";

                StringWriter sw = new StringWriter();

                HtmlTextWriter hw = new HtmlTextWriter(sw);


                //Change the Header Row back to white color

                gridpurchaseregister.HeaderRow.Style.Add("background-color", "#FFFFFF");


                for (int i = 0; i < gridpurchaseregister.Rows.Count; i++)
                {

                    GridViewRow row = gridpurchaseregister.Rows[i];
                    row.BackColor = System.Drawing.Color.White;
                    row.Attributes.Add("class", "textmode");

                }

                Panel1.RenderControl(hw);

                //style to format numbers to string

                string style = @"<style> .textmode { mso-number-format:\@; } </style>";

                Response.Write(style);

                Response.Output.Write(sw.ToString());

                Response.Flush();

                Response.End();
            }
            else if (ddlExport.SelectedValue == "3")
            {
                Response.Clear();

                Response.Buffer = true;

                Response.AddHeader("content-disposition",

                "attachment;filename=GridViewExport.doc");

                Response.Charset = "";

                Response.ContentType = "application/vnd.ms-word ";

                StringWriter sw = new StringWriter();

                HtmlTextWriter hw = new HtmlTextWriter(sw);


                Panel1.RenderControl(hw);

                Response.Output.Write(sw.ToString());

                Response.Flush();

                Response.End();

            }
           

        }

    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }


    protected DataTable selectbcon(string str)
    {
        SqlCommand cmd = new SqlCommand(str, PageConn.busclient());
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);
        return dt;

    }
    protected void pageMailAccess()
    {
        ddlExport.Items.Insert(0, "Export Type");
        ddlExport.Items[0].Value = "0";
        ddlExport.Items.Insert(1, "Export to PDF");
        ddlExport.Items[1].Value = "1";
        ddlExport.Items.Insert(2, "Export to Excel");
        ddlExport.Items[2].Value = "2";
        ddlExport.Items.Insert(3, "Export to Word");
        ddlExport.Items[3].Value = "3";


        string avfr = "  and PageMaster.PageName='" + ClsEncDesc.EncDyn("MessageCompose.aspx") + "'";
        DataTable drt = selectbcon("SELECT distinct " + PageConn.busdatabase + ".dbo.RoleMenuAccessRightTbl.MenuId,PageMaster.PageName FROM MainMenuMaster inner join " + PageConn.busdatabase + ".dbo.RoleMenuAccessRightTbl on " + PageConn.busdatabase + ".dbo.RoleMenuAccessRightTbl.MenuId=MainMenuMaster.MainMenuId inner join PageMaster on PageMaster.MainMenuId=" + PageConn.busdatabase + ".dbo.RoleMenuAccessRightTbl.MenuId  inner join pageplaneaccesstbl on pageplaneaccesstbl.Pageid=PageMaster.PageId  INNER JOIN  " + PageConn.busdatabase + ".dbo.User_Role ON " + PageConn.busdatabase + ".dbo.RoleMenuAccessRightTbl.RoleId = " + PageConn.busdatabase + ".dbo.User_Role.Role_id INNER JOIN " + PageConn.busdatabase + ".dbo.User_master ON " + PageConn.busdatabase + ".dbo.User_Role.User_id = " + PageConn.busdatabase + ".dbo.User_master.UserID where pageplaneaccesstbl.Priceplanid='" + ClsEncDesc.EncDyn(Session["PriceId"].ToString()) + "'" + avfr + " and PageMaster.VersionInfoMasterId='" + ClsEncDesc.EncDyn(Session["verId"].ToString()) + "' and  " + PageConn.busdatabase + ".dbo.User_master.UserID ='" + Session["userid"] + "'");
        if (drt.Rows.Count <= 0)
        {

            drt = selectbcon("SELECT PageMaster.PageName FROM PageMaster inner join " + PageConn.busdatabase + ".dbo.RolePageAccessRightTbl on " + PageConn.busdatabase + ".dbo.RolePageAccessRightTbl.PageId=PageMaster.PageId inner join pageplaneaccesstbl on pageplaneaccesstbl.Pageid=PageMaster.PageId INNER JOIN " + PageConn.busdatabase + ".dbo.User_Role ON " + PageConn.busdatabase + ".dbo.RolePageAccessRightTbl.RoleId = " + PageConn.busdatabase + ".dbo.User_Role.Role_id INNER JOIN " + PageConn.busdatabase + ".dbo.User_master ON " + PageConn.busdatabase + ".dbo.User_Role.User_id = " + PageConn.busdatabase + ".dbo.User_master.UserID where pageplaneaccesstbl.Priceplanid='" + ClsEncDesc.EncDyn(Session["PriceId"].ToString()) + "' " + avfr + " and PageMaster.VersionInfoMasterId='" + ClsEncDesc.EncDyn(Session["verId"].ToString()) + "' and  " + PageConn.busdatabase + ".dbo.User_master.UserID ='" + Session["userid"] + "'");
            if (drt.Rows.Count <= 0)
            {
                drt = selectbcon("SELECT distinct PageMaster.PageName FROM MainMenuMaster inner join  SubMenuMaster on SubMenuMaster.MainMenuId=MainMenuMaster.MainMenuId inner join " + PageConn.busdatabase + ".dbo.RoleSubMenuAccessRightTbl on " + PageConn.busdatabase + ".dbo.RoleSubMenuAccessRightTbl.SubMenuId=SubMenuMaster.SubMenuId inner join PageMaster on PageMaster.SubMenuId=" + PageConn.busdatabase + ".dbo.RoleSubMenuAccessRightTbl.SubMenuId  inner join pageplaneaccesstbl on pageplaneaccesstbl.Pageid=PageMaster.PageId  INNER JOIN  " + PageConn.busdatabase + ".dbo.User_Role ON " + PageConn.busdatabase + ".dbo.RoleSubMenuAccessRightTbl.RoleId = " + PageConn.busdatabase + ".dbo.User_Role.Role_id INNER JOIN " + PageConn.busdatabase + ".dbo.User_master ON " + PageConn.busdatabase + ".dbo.User_Role.User_id = " + PageConn.busdatabase + ".dbo.User_master.UserID where pageplaneaccesstbl.Priceplanid='" + ClsEncDesc.EncDyn(Session["PriceId"].ToString()) + "'" + avfr + " and PageMaster.VersionInfoMasterId='" + ClsEncDesc.EncDyn(Session["verId"].ToString()) + "' and  " + PageConn.busdatabase + ".dbo.User_master.UserID ='" + Session["userid"] + "'");


                if (drt.Rows.Count <= 0)
                {


                }
                else
                {
                    ddlExport.Items.Insert(4, "Email with PDF");
                    ddlExport.Items[4].Value = "4";
                }

            }
            else
            {
                ddlExport.Items.Insert(4, "Email with PDF");
                ddlExport.Items[4].Value = "4";

            }


        }
        else
        {

            ddlExport.Items.Insert(4, "Email with PDF");
            ddlExport.Items[4].Value = "4";

        }
    }

    public void createPDFDoc(String strhtml)
    {
        string strfilename = HttpContext.Current.Server.MapPath("TempDoc/GridViewExport.pdf");

        Document doc = new Document(PageSize.A2, 30f, 30f, 30f, 30f);
        PdfWriter.GetInstance(doc, new FileStream(strfilename, FileMode.Create));
        System.IO.StringReader se = new StringReader(strhtml.ToString());
        HTMLWorker obj = new HTMLWorker(doc);

        doc.Open();
        obj.Parse(se);
        doc.Close();
        Showpdf(strfilename);
    }
    public void Showpdf(string strFileName)
    {
        Response.ClearContent();
        Response.ClearHeaders();
        Response.AddHeader("Content-Disposition", "inline;filename=" + strFileName);
        Response.ContentType = "application/pdf";
        Response.WriteFile(strFileName);
        Response.Flush();
        Response.Clear();
    }
    protected void gridpurchaseregister_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridpurchaseregister.PageIndex = e.NewPageIndex;
        FillGrid();
    }

    protected void ddlperiod_SelectedIndexChanged(object sender, EventArgs e)
    {
      string  DateBetween = (string)BetweenByPeriod();
    }
}

