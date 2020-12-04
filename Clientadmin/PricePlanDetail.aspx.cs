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
public partial class PricePlanDetail : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    SqlConnection conn;
    public static int intch = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblVersion.Text = "";
       
        pnlperorder.Visible = false;
        lblmsg.Text = "";
        if (!Page.IsPostBack)
        {
            if (Session["Login"] != null)
            {
                if (Session["Login"].ToString() == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
            txtStartdate.Text = System.DateTime.Now.ToShortDateString();
            txtEndDate.Text = System.DateTime.Now.AddYears(2).ToShortDateString();
            btnSubmit.Visible = true;
            FillProduct();
            ddlProductname1_SelectedIndexChanged(sender, e);
            serverdd();
            ViewState["sortOrder"] = "";
            DropDownList1_SelectedIndexChanged(sender, e);


            //--          
        }


    }
    protected void chkupload_CheckedChanged(object sender, EventArgs e)
    {
       
            FillProduct();
            ddlProductname1_SelectedIndexChanged(sender, e);
            serverdd();
            ViewState["sortOrder"] = "";
            DropDownList1_SelectedIndexChanged(sender, e);
       
    }
    protected void fillpriceplancate()
    {
        ddlpriceplancatagory.Items.Clear();
        string strcln = " SELECT distinct * FROM Priceplancategory where PortalId='" + ddlportal.SelectedValue + "' order  by CategoryName";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        ddlpriceplancatagory.DataSource = dtcln;
        ddlpriceplancatagory.DataTextField = "CategoryName";
        ddlpriceplancatagory.DataValueField = "Id";
        ddlpriceplancatagory.DataBind();

    }


    protected void btnprint_Click(object sender, EventArgs e)
    {
        if (btnprint.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");

            btnprint.Text = "Hide Printable Version";
            btnin.Visible = true;
            if (GridView1.Columns[7].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[7].Visible = false;
            }
            if (GridView1.Columns[8].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                GridView1.Columns[8].Visible = false;
            }
        }
        else
        {
            //pnlgrid.ScrollBars = ScrollBars.Vertical;
            //pnlgrid.Height = new Unit(100);
            btnprint.Text = "Printable Version";
            btnin.Visible = false;
            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[7].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                GridView1.Columns[8].Visible = true;
            }
        }
    }
    protected void FillProduct()
    {
        //string strcln = " SELECT distinct ProductMaster.ProductId, VersionInfoMaster.VersionInfoId,ProductMaster.ProductName + ' : ' + VersionInfoMaster.VersionInfoName as productversion FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.VersionNo=VersionInfoMaster.VersionInfoName    where ClientMasterId=" + Session["ClientId"].ToString() + " and ProductDetail.Active='1' and ProductDetail.Active ='True' order  by productversion";
        string activestr = "";
        if (chkupload.Checked == true)
        {
            activestr =  " and VersionInfoMaster.Active=1";
        }

        string strcln = " SELECT distinct ProductMaster.ProductId,ProductDetail.Active,VersionInfoMaster.VersionInfoId,ProductMaster.ProductName + ' : ' + VersionInfoMaster.VersionInfoName as productversion FROM ProductMaster inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId where ClientMasterId=" + Session["ClientId"].ToString() + " and ProductDetail.Active='1'  order  by productversion";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        ddlProductname1.DataSource = dtcln;

        ddlProductname1.DataValueField = "VersionInfoId";
        ddlProductname1.DataTextField = "productversion";
        ddlProductname1.DataBind();
        ddlProductname1.Items.Insert(0, "-Select-");
        ddlProductname1.Items[0].Value = "0";

        DropDownList1.DataSource = dtcln;
        DropDownList1.DataTextField = "productversion";
        DropDownList1.DataValueField = "VersionInfoId";
        DropDownList1.DataBind();
        DropDownList1.Items.Insert(0, "All");
        DropDownList1.Items[0].Value = "0";

    }

    protected void FillGrid()
    {
        string strcln = " SELECT distinct PricePlanMaster.*,PortalMasterTbl.PortalName,  case when  (PricePlanMaster.active='1') then 'Active' else 'Inactive' end as Active1,  case when  PricePlanMaster.AllowIPTrack IS NULL     then 'No' else 'Yes' end as GBUSage1 , ProductMaster.ProductName + ' : ' + VersionInfoMaster.VersionInfoName as ProductName " +
                        " FROM ProductMaster inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join PricePlanMaster  " +
                        "  ON PricePlanMaster.VersionInfoMasterId= VersionInfoMaster.VersionInfoId inner join PortalMasterTbl on PortalMasterTbl.Id=PricePlanMaster.PortalMasterId1 " +
                        " where ClientMasterId= " + Session["ClientId"].ToString();
        if (DropDownList1.SelectedIndex > 0)
        {
            strcln = strcln + " and PricePlanMaster.VersionInfoMasterId='" + DropDownList1.SelectedItem.Value.ToString() + "'";
        }
        if (ddlfilterportal.SelectedIndex > 0)
        {

            strcln = strcln + " and PricePlanMaster.PortalMasterId1='" + ddlfilterportal.SelectedValue.ToString() + "'";

        }
        if (ddlfilproductcategory.SelectedIndex > 0)
        {

            strcln = strcln + " and PricePlanMaster.PriceplancatId='" + ddlfilproductcategory.SelectedValue.ToString() + "'";

        }


        strcln = strcln + " and PricePlanMaster.active='" + ddlstfilter.SelectedValue + "'";


        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);

        if (dtcln.Rows.Count > 0)
        {

            DataView myDataView = new DataView();
            myDataView = dtcln.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }

            GridView1.DataSource = dtcln;
            GridView1.DataBind();
        }

        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();

        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
       
        Session["ProductId"] = ddlProductname1.SelectedValue;
        string strss = "select productid from VersionInfoMaster where VersionInfoId='" + ddlProductname1.SelectedValue + "' ";
        SqlCommand cmdcln = new SqlCommand(strss, con);
        con.Open();
        Session["ProductId"] = cmdcln.ExecuteScalar();
        con.Close();

        if (btnSubmit.Text == "Update")
        {
            addnewpanel.Visible = true;
            pnladdnew.Visible = false;
            lbladdlabel.Text = "";
            TextBox lblprp1 = (TextBox)grdmulti.HeaderRow.FindControl("lblprp1");

            TextBox lblpamt = (TextBox)grdmulti.HeaderRow.FindControl("lblfo1");
            TextBox txtbaseprice = (TextBox)grdmulti.HeaderRow.FindControl("txtbaseprice1");
            CheckBox chkplc = (CheckBox)grdmulti.HeaderRow.FindControl("chkplc1");
            intch = 0;
            if (chkplc.Checked == true)
            {
                string otherup = " update PricePlanMaster set Plancatedefault='0' where PortalMasterId1='" + ddlportal.SelectedValue + "' and VersionInfoMasterId='" + ddlProductname1.SelectedValue + "' ";
                SqlCommand cmdotherup = new SqlCommand(otherup, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdotherup.ExecuteNonQuery();
                con.Close();
            }

            if (CheckBox1.Checked == true)
            {
                string otherup = " update PricePlanMaster set IsItFreeTryOutPlan='0' where PortalMasterId1='" + ddlportal.SelectedValue + "' and VersionInfoMasterId='" + ddlProductname1.SelectedValue + "' ";
                SqlCommand cmdotherup = new SqlCommand(otherup, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdotherup.ExecuteNonQuery();
                con.Close();
            }
            int itfreepl = 0;
            if (CheckBox1.Checked == true)
            {
                itfreepl = 1;
            }
            string str = "update   PricePlanMaster set Plancatedefault='" + chkplc.Checked + "',BasePrice='" + txtbaseprice.Text + "',IsItFreeTryOutPlan='" + itfreepl + "', Producthostclientserver='" + Rdownclient.SelectedValue + "', PortalMasterId1='" + ddlportal.SelectedValue + "',  PricePlanName='" + lblprp1.Text + "' ,PricePlanDesc='" + txtPlanDesc.Text + "', Active = '" + chkboxActiveDeactive.Checked.ToString() + "' ,StartDate='" + txtStartdate.Text + "',EndDate='" + txtEndDate.Text + "', pricePlanAmount='" + lblpamt.Text + "',PayperOrderPlan='" + chkporder.Checked + "',amountperOrder='" + txtperorder.Text + "',FreeIntialOrders='" + txtfreeinitialorder.Text + "',MinimumDeposite='" + txtmindepre.Text + "',Maxamount='" + txtminamount.Text + "', ProductId=" + Session["ProductId"] + ",DurationMonth= '" + txtMonth.Text + "' , AllowIPTrack = '" + Convert.ToBoolean(RadioButtonList1.SelectedItem.Value) + "' ,GBUsage = '" + txtGBUsage.Text + "',  TrafficinGB = '" + txtTrafficinGB.Text + "' , TotalMail='" + txtTotalMail.Text + "',CheckIntervalDays='" + txtcheintvaldays.Text + "',GraceDays='" + txtgracedays.Text + "',VersionInfoMasterId='" + ddlProductname1.SelectedValue + "',PriceplancatId='" + ddlpriceplancatagory.SelectedValue + "' where PricePlanId= '" + hdnPricePlanId.Value.ToString() + "'";
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand(str, con);

            cmd.ExecuteNonQuery();
            con.Close();
            string delstv = "Delete from   Priceplanrestrecordallowtbl Where PricePlanId='" + hdnPricePlanId.Value.ToString() + "'";
            SqlCommand cmdvdel = new SqlCommand(delstv, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdvdel.ExecuteNonQuery();
            con.Close();
            foreach (GridViewRow item in grdmulti.Rows)
            {
                string restId = grdmulti.DataKeys[item.RowIndex].Value.ToString();
                Label lblupId = (Label)item.FindControl("lblupId");
                //Label lblpriceaddgroup = (Label)item.FindControl("lblpriceaddgroup");
                TextBox lblprprest1 = (TextBox)item.FindControl("lblprprest1");
                string strv = "";
                //if (lblupId.Text != "")
                //{
                //    strv = "update   Priceplanrestrecordallowtbl set RecordsAllowed='" + lblprprest1.Text + "' Where Id='" + lblupId.Text + "'";
                //}
                //else
                //{
                strv = "INSERT INTO Priceplanrestrecordallowtbl (PriceplanRestrictiontblId,PricePlanId,RecordsAllowed) Values('" + restId + "','" + hdnPricePlanId.Value.ToString() + "','" + lblprprest1.Text + "')";

                //}
                if (strv.Length > 0)
                {
                    SqlCommand cmdv = new SqlCommand(strv, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdv.ExecuteNonQuery();
                    con.Close();
                }

            }
            if (Rdownclient.SelectedValue == "0")
            {
                DataTable dtserass = select("Select * from ServerAssignmentMasterTbl where   PricePlanId='" + hdnPricePlanId.Value.ToString() + "'");
                if (dtserass.Rows.Count == 0)
                {
                    string stradd = "Insert into ServerAssignmentMasterTbl(ClientId,ProductId,VersionId,PricePlanId,ServerId,Active,Activatedatetime)Values('" + Session["ClientId"] + "','" + Session["ProductId"] + "','" + ddlProductname1.SelectedValue + "','" + hdnPricePlanId.Value.ToString() + "','" + ddlserverMas.SelectedValue + "','1','" + DateTime.Now + "')";
                    SqlCommand cmdc = new SqlCommand(stradd, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdc.ExecuteNonQuery();
                    con.Close();
                }
                else
                {
                    string stradd = "Update ServerAssignmentMasterTbl Set ServerId='" + ddlserverMas.SelectedValue + "',Activatedatetime='" + DateTime.Now + "' where PricePlanId='" + hdnPricePlanId.Value.ToString() + "'";
                    SqlCommand cmdc = new SqlCommand(stradd, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdc.ExecuteNonQuery();
                    con.Close();
                }
            }
            else
            {
                DataTable dtserass = select("Select * from  ServerAssignmentMasterTbl where   PricePlanId='" + hdnPricePlanId.Value.ToString() + "'");
                if (dtserass.Rows.Count > 0)
                {
                    string stradd = "Update ServerAssignmentMasterTbl Set Active='0',Activatedatetime='" + DateTime.Now + "' where PricePlanId='" + hdnPricePlanId.Value.ToString() + "'";
                    SqlCommand cmdc = new SqlCommand(stradd, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdc.ExecuteNonQuery();
                    con.Close();
                }
            }
            //--Update Delete PRODUCT MODUL----------------------
            //DeleteProductModulePriceplan(hdnPricePlanId.Value.ToString());  
            //InsertProductModulePriceplan(hdnPricePlanId.Value.ToString());  
            //--------------
            lblmsg.Visible = true;
            lblmsg.Text = "Record updated successfully";
            FillGrid();
            ClearAll();
            ModernpopSync.Show();
        }
        else
        {

            try
            {

                if (intch == 0)
                {
                    btncalsugprice_Click(sender, e);
                }
                int k1 = 0;
                addnewpanel.Visible = true;
                pnladdnew.Visible = false;
                lbladdlabel.Text = "";
                TextBox lblprp1 = (TextBox)grdmulti.HeaderRow.FindControl("lblprp1");

                intch = 0;

                if (lblprp1.Text.Length > 0)
                {
                    k1 = 1;
                    insertpp(1, lblprp1);

                }
                if (rdmultip.SelectedValue == "2")
                {
                    TextBox lblprp2 = (TextBox)grdmulti.HeaderRow.FindControl("lblprp2");

                    if (lblprp2.Text.Length > 0)
                    {
                        k1 = 1;
                        insertpp(2, lblprp2);

                    }
                    TextBox lblprp3 = (TextBox)grdmulti.HeaderRow.FindControl("lblprp3");

                    if (lblprp3.Text.Length > 0)
                    {
                        k1 = 1;
                        insertpp(3, lblprp3);

                    }
                    TextBox lblprp4 = (TextBox)grdmulti.HeaderRow.FindControl("lblprp4");

                    if (lblprp4.Text.Length > 0)
                    {
                        k1 = 1;
                        insertpp(4, lblprp4);

                    }
                    TextBox lblprp5 = (TextBox)grdmulti.HeaderRow.FindControl("lblprp5");

                    if (lblprp5.Text.Length > 0)
                    {
                        k1 = 1;
                        insertpp(5, lblprp5);

                    }
                    TextBox lblprp6 = (TextBox)grdmulti.HeaderRow.FindControl("lblprp6");

                    if (lblprp6.Text.Length > 0)
                    {
                        k1 = 1;
                        insertpp(6, lblprp6);

                    }
                    TextBox lblprp7 = (TextBox)grdmulti.HeaderRow.FindControl("lblprp7");

                    if (lblprp7.Text.Length > 0)
                    {
                        k1 = 1;
                        insertpp(7, lblprp7);

                    }
                    TextBox lblprp8 = (TextBox)grdmulti.HeaderRow.FindControl("lblprp8");

                    if (lblprp8.Text.Length > 0)
                    {
                        k1 = 1;
                        insertpp(8, lblprp8);

                    }
                    TextBox lblprp9 = (TextBox)grdmulti.HeaderRow.FindControl("lblprp9");

                    if (lblprp9.Text.Length > 0)
                    {
                        k1 = 1;
                        insertpp(9, lblprp9);

                    }
                    TextBox lblprp10 = (TextBox)grdmulti.HeaderRow.FindControl("lblprp10");

                    if (lblprp10.Text.Length > 0)
                    {
                        k1 = 1;
                        insertpp(10, lblprp10);

                    }
                    TextBox lblprp11 = (TextBox)grdmulti.HeaderRow.FindControl("lblprp11");

                    if (lblprp11.Text.Length > 0)
                    {
                        k1 = 1;
                        insertpp(11, lblprp11);

                    }
                    TextBox lblprp12 = (TextBox)grdmulti.HeaderRow.FindControl("lblprp12");

                    if (lblprp12.Text.Length > 0)
                    {
                        k1 = 1;
                        insertpp(12, lblprp12);

                    }
                    TextBox lblprp13 = (TextBox)grdmulti.HeaderRow.FindControl("lblprp13");

                    if (lblprp13.Text.Length > 0)
                    {
                        k1 = 1;
                        insertpp(13, lblprp13);

                    }
                    TextBox lblprp14 = (TextBox)grdmulti.HeaderRow.FindControl("lblprp14");

                    if (lblprp14.Text.Length > 0)
                    {
                        k1 = 1;
                        insertpp(14, lblprp14);

                    }
                    TextBox lblprp15 = (TextBox)grdmulti.HeaderRow.FindControl("lblprp15");

                    if (lblprp15.Text.Length > 0)
                    {
                        k1 = 1;
                        insertpp(15, lblprp15);

                    }
                    TextBox lblprp16 = (TextBox)grdmulti.HeaderRow.FindControl("lblprp16");

                    if (lblprp16.Text.Length > 0)
                    {
                        k1 = 1;
                        insertpp(16, lblprp16);

                    }
                    TextBox lblprp17 = (TextBox)grdmulti.HeaderRow.FindControl("lblprp17");

                    if (lblprp17.Text.Length > 0)
                    {
                        k1 = 1;
                        insertpp(17, lblprp17);

                    }
                    TextBox lblprp18 = (TextBox)grdmulti.HeaderRow.FindControl("lblprp18");

                    if (lblprp18.Text.Length > 0)
                    {
                        k1 = 1;
                        insertpp(18, lblprp18);

                    }
                    TextBox lblprp19 = (TextBox)grdmulti.HeaderRow.FindControl("lblprp19");

                    if (lblprp19.Text.Length > 0)
                    {
                        k1 = 1;
                        insertpp(19, lblprp19);

                    }
                    TextBox lblprp20 = (TextBox)grdmulti.HeaderRow.FindControl("lblprp20");

                    if (lblprp20.Text.Length > 0)
                    {
                        k1 = 1;
                        insertpp(20, lblprp20);

                    }
                }
                if (k1 == 1)
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "Record inserted successfully";
                    FillGrid();

                    ClearAll();
                    ModernpopSync.Show();
                }
                else
                {
                    lblmsg.Text = "Record does not inserted successfully";
                }
            }
            catch (Exception eerr)
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Error : " + eerr.Message;
            }

            FillGrid();
        }
    }
    protected void insertpp(int rno, TextBox lblppname)
    {
        TextBox lblpamt = (TextBox)grdmulti.HeaderRow.FindControl("lblfo" + rno);
        TextBox txtbaseprice = (TextBox)grdmulti.HeaderRow.FindControl("txtbaseprice" + rno);
        CheckBox chkplc = (CheckBox)grdmulti.HeaderRow.FindControl("chkplc" + rno);
        string str = "";
        int ind = 0;
        if (chkplc.Checked == true)
        {
            string otherup = " update PricePlanMaster set Plancatedefault='0' where PortalMasterId1='" + ddlportal.SelectedValue + "' and VersionInfoMasterId='" + ddlProductname1.SelectedValue + "' ";
            SqlCommand cmdotherup = new SqlCommand(otherup, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdotherup.ExecuteNonQuery();
            con.Close();
        }

        if (CheckBox1.Checked == true)
        {
            string otherup = " update PricePlanMaster set IsItFreeTryOutPlan='0' where PortalMasterId1='" + ddlportal.SelectedValue + "' and VersionInfoMasterId='" + ddlProductname1.SelectedValue + "' ";
            SqlCommand cmdotherup = new SqlCommand(otherup, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdotherup.ExecuteNonQuery();
            con.Close();
            str = "INSERT INTO PricePlanMaster(BasePrice,Plancatedefault,PricePlanName,PricePlanDesc, Active ,StartDate,EndDate, pricePlanAmount,PayperOrderPlan,amountperOrder,FreeIntialOrders,MinimumDeposite,Maxamount, ProductId,DurationMonth ,AllowIPTrack ,GBUsage , TrafficinGB , TotalMail,CheckIntervalDays,GraceDays,VersionInfoMasterId,PriceplancatId,PortalMasterId1,Producthostclientserver,IsItFreeTryOutPlan,CompanyPrefix,ConfigurationText,EmailContent) " +
                               "VALUES('" + txtbaseprice.Text + "','" + chkplc.Checked + "','" + lblppname.Text + "','" + txtPlanDesc.Text + "',1,'" + txtStartdate.Text + "','" + txtEndDate.Text + "','" + lblpamt.Text + "','" + chkporder.Checked + "','" + txtperorder.Text + "','" + txtfreeinitialorder.Text + "','" + txtmindepre.Text + "','" + txtminamount.Text + "'," + Session["ProductId"] + ", '" + txtMonth.Text + "','" + Convert.ToBoolean(RadioButtonList1.SelectedItem.Value) + "','" + txtGBUsage.Text + "', '" + txtTrafficinGB.Text + "','" + txtTotalMail.Text + "','" + txtcheintvaldays.Text + "','" + txtgracedays.Text + "','" + ddlProductname1.SelectedValue + "','" + ddlpriceplancatagory.SelectedValue + "','" + ddlportal.Text + "','" + Rdownclient.SelectedValue + "','1','" + txtprefix.Text + "','" + TextBox2.Text + "','" + TextBox1.Text + "')";
            SqlCommand cmd = new SqlCommand(str, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            ind = Convert.ToInt32(cmd.ExecuteNonQuery());
            con.Close();
        }
        else
        {
            str = "INSERT INTO PricePlanMaster(BasePrice,Plancatedefault,PricePlanName,PricePlanDesc, Active ,StartDate,EndDate, pricePlanAmount,PayperOrderPlan,amountperOrder,FreeIntialOrders,MinimumDeposite,Maxamount, ProductId,DurationMonth ,AllowIPTrack ,GBUsage , TrafficinGB , TotalMail,CheckIntervalDays,GraceDays,VersionInfoMasterId,PriceplancatId,PortalMasterId1,Producthostclientserver) " +
                              "VALUES('" + txtbaseprice.Text + "','" + chkplc.Checked + "','" + lblppname.Text + "','" + txtPlanDesc.Text + "',1,'" + txtStartdate.Text + "','" + txtEndDate.Text + "','" + lblpamt.Text + "','" + chkporder.Checked + "','" + txtperorder.Text + "','" + txtfreeinitialorder.Text + "','" + txtmindepre.Text + "','" + txtminamount.Text + "'," + Session["ProductId"] + ", '" + txtMonth.Text + "','" + Convert.ToBoolean(RadioButtonList1.SelectedItem.Value) + "','" + txtGBUsage.Text + "', '" + txtTrafficinGB.Text + "','" + txtTotalMail.Text + "','" + txtcheintvaldays.Text + "','" + txtgracedays.Text + "','" + ddlProductname1.SelectedValue + "','" + ddlpriceplancatagory.SelectedValue + "','" + ddlportal.Text + "','" + Rdownclient.SelectedValue + "')";
            SqlCommand cmd = new SqlCommand(str, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            ind = Convert.ToInt32(cmd.ExecuteNonQuery());
            con.Close();
        }


        if (ind > 0)
        {
            DataTable dtp = select("Select Max(PricePlanId) as PricePlanId from PricePlanMaster where VersionInfoMasterId='" + ddlProductname1.SelectedValue + "'");
            if (dtp.Rows.Count > 0)
            {
                string Temp1 = "";
                string Temp1val = "";
                Temp1 = "INSERT INTO Priceplanrestrecordallowtbl(PriceplanRestrictiontblId,PricePlanId,RecordsAllowed)Values";

                foreach (GridViewRow item in grdmulti.Rows)
                {
                    string restId = grdmulti.DataKeys[item.RowIndex].Value.ToString();
                    //Label lblrestgroup = (Label)item.FindControl("lblrestgroup");
                    //Label lblpriceaddgroup = (Label)item.FindControl("lblpriceaddgroup");
                    TextBox lblprprest1 = (TextBox)item.FindControl("lblprprest" + rno);
                    if (Temp1val.Length > 0)
                    {
                        Temp1val += ",";
                    }
                    Temp1val += "('" + restId + "','" + dtp.Rows[0]["PricePlanId"] + "','" + lblprprest1.Text + "')";

                }
                if (Temp1val.Length > 0)
                {
                    Temp1 += Temp1val;
                    SqlCommand cmdb = new SqlCommand(Temp1, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdb.ExecuteNonQuery();
                    con.Close();
                }
                if (Rdownclient.SelectedValue == "0")
                {
                    string stradd = "Insert into ServerAssignmentMasterTbl(ClientId,ProductId,VersionId,PricePlanId,ServerId,Active,Activatedatetime)Values('" + Session["ClientId"] + "','" + Session["ProductId"] + "','" + ddlProductname1.SelectedValue + "','" + dtp.Rows[0]["PricePlanId"] + "','" + ddlserverMas.SelectedValue + "','1','" + DateTime.Now + "')";
                    SqlCommand cmdc = new SqlCommand(stradd, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdc.ExecuteNonQuery();
                    con.Close();
                }



                //-------------Insert Product Modul-----------
                //InsertProductModulePriceplan(dtp.Rows[0]["PricePlanId"].ToString());                
                //---------------------------
                fillpageaccess(dtp.Rows[0]["PricePlanId"].ToString());
            }

        }

    }
    protected void ClearAll()
    {
        btnSubmit.Visible = true;
        txtEndDate.Text = "";
        txtPlanAmount.Text = "";
        txtPlanDesc.Text = "";
        txtPlanName.Text = "";
        txtStartdate.Text = "";
        txtTotalMail.Text = "";
        txtTrafficinGB.Text = "";
        //  txtMaxUser.Text = "";
        RadioButtonList1.SelectedIndex = 1;
        // ddlProductname1.SelectedIndex = 0;
        btnSubmit.Text = "Submit";
        txtGBUsage.Text = "";
        txtMonth.Text = "";
        txtcheintvaldays.Text = "";
        txtgracedays.Text = "";
        chkporder.Checked = false;
        txtminamount.Text = "";
        txtperorder.Text = "";
        txtfreeinitialorder.Text = "";
        txtmindepre.Text = "";
        intch = 0;
        EventArgs e = new EventArgs();
        object sender = new object();
        // ddlProductname1_SelectedIndexChanged(sender, e);
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "edit1")
        {
            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            int i = Convert.ToInt32(GridView1.SelectedDataKey.Value);

            lblmsg.Text = "";
            addnewpanel.Visible = false;
            pnladdnew.Visible = true;
            lbladdlabel.Text = "Edit Priceplan";

            pnlnoofmulti.Visible = false;
            lblmultipres.Text = "Set Price Plan Restrictions ";
            ViewState["PricePlanId"] = Convert.ToInt32(GridView1.SelectedDataKey.Value);
            btnSubmit.Visible = true;
            fillvalue(i);
            btnSubmit.Text = "Update";
            // BtnSubmitall.Text = "Update";
            // BtnRecord.Text = "Update";



        }
        else if (e.CommandName == "view")
        {
            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            int i = Convert.ToInt32(GridView1.SelectedDataKey.Value);

            lblmsg.Text = "";
            addnewpanel.Visible = false;
            pnladdnew.Visible = true;
            lbladdlabel.Text = "View Priceplan";

            btnSubmit.Visible = false;
            fillvalue(i);

        }
        else if (e.CommandName == "delete")
        {
            int i = Convert.ToInt32(e.CommandArgument);



            string delstv = "Delete from   Priceplanrestrecordallowtbl Where PricePlanId='" + i + "'";
            SqlCommand cmdvdel = new SqlCommand(delstv, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdvdel.ExecuteNonQuery();
            con.Close();

            string stradd = "Delete from  ServerAssignmentMasterTbl where PricePlanId='" + i + "'";
            SqlCommand cmdc = new SqlCommand(stradd, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdc.ExecuteNonQuery();
            con.Close();

            string straddF = "Delete from  pageplaneaccesstbl where priceplanid='" + i + "'";
            SqlCommand cmdcF = new SqlCommand(straddF, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdcF.ExecuteNonQuery();
            con.Close();

            SqlCommand cd4 = new SqlCommand("Delete from  PricePlanMaster  where PricePlanId='" + i + "'", con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cd4.ExecuteNonQuery();
            con.Close();
            lblmsg.Visible = true;
            lblmsg.Text = "Record deleted successfully.";
            FillGrid();

        }
        //  Response.Redirect("CustomerEdit.aspx?CompanyId=" + i + "");
    }

    public void fillvalue(int i)
    {
        string strcln = " SELECT     PricePlanMaster.*, ProductMaster.* " +
           " FROM         PricePlanMaster LEFT OUTER JOIN " +
                      " ProductMaster ON PricePlanMaster.ProductId = ProductMaster.ProductId " +
                        " where PricePlanId= " + i.ToString();

        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        if (dtcln.Rows.Count > 0)
        {
            hdnPricePlanId.Value = i.ToString();
            txtPlanName.Text = dtcln.Rows[0]["PricePlanName"].ToString();
            txtPlanDesc.Text = dtcln.Rows[0]["PricePlanDesc"].ToString();
            chkboxActiveDeactive.Checked = Convert.ToBoolean(dtcln.Rows[0]["Active"].ToString());
            txtStartdate.Text = dtcln.Rows[0]["StartDate"].ToString();
            txtEndDate.Text = dtcln.Rows[0]["EndDate"].ToString();
            ddlProductname1.SelectedIndex = ddlProductname1.Items.IndexOf(ddlProductname1.Items.FindByValue(dtcln.Rows[0]["VersionInfoMasterId"].ToString()));
            fillportal();
            if (Convert.ToString(dtcln.Rows[0]["PortalMasterId1"]) != "")
            {
                //ddlportal.SelectedValue = Convert.ToString(dtcln.Rows[0]["PortalMasterId1"]);
                ddlportal.SelectedIndex = ddlportal.Items.IndexOf(ddlportal.Items.FindByValue(dtcln.Rows[0]["PortalMasterId1"].ToString()));
            }
            fillpriceplancate();
            if (Convert.ToString(dtcln.Rows[0]["PriceplancatId"]) != "")
            {
                ddlpriceplancatagory.SelectedIndex = ddlpriceplancatagory.Items.IndexOf(ddlpriceplancatagory.Items.FindByValue(dtcln.Rows[0]["PriceplancatId"].ToString()));

                //  ddlpriceplancatagory.SelectedValue = Convert.ToString(dtcln.Rows[0]["PriceplancatId"]);
            }
            txtPlanAmount.Text = dtcln.Rows[0]["pricePlanAmount"].ToString();
            txtMonth.Text = dtcln.Rows[0]["DurationMonth"].ToString();
            if (dtcln.Rows[0]["PayperOrderPlan"].ToString() != "")
            {
                chkporder.Checked = Convert.ToBoolean(dtcln.Rows[0]["PayperOrderPlan"].ToString());
                if (chkporder.Checked == true)
                {
                    pnlperorder.Visible = true;
                }
            }
            Session["ProductId"] = dtcln.Rows[0]["ProductId"].ToString();
            txtperorder.Text = dtcln.Rows[0]["amountperOrder"].ToString();
            txtfreeinitialorder.Text = dtcln.Rows[0]["FreeIntialOrders"].ToString();
            txtmindepre.Text = dtcln.Rows[0]["MinimumDeposite"].ToString();
            txtminamount.Text = dtcln.Rows[0]["Maxamount"].ToString();
            txtcheintvaldays.Text = dtcln.Rows[0]["CheckIntervalDays"].ToString();
            txtgracedays.Text = dtcln.Rows[0]["GraceDays"].ToString();
            //GridView1.DataSource = dtcln;
            //GridView1.DataBind();

            //
            //, , , ,   , 
            //    Response.Write(dtcln.Rows[0]["AllowIPTrack"]);
            if (dtcln.Rows[0]["AllowIPTrack"].ToString() == "True")
            {
                RadioButtonList1.SelectedIndex = 0;
            }
            else
            {
                RadioButtonList1.SelectedIndex = 1;
            }
            txtGBUsage.Text = dtcln.Rows[0]["GBUsage"].ToString();
            // txtMaxUser.Text = dtcln.Rows[0]["MaxUser"].ToString();
            txtTrafficinGB.Text = dtcln.Rows[0]["TrafficinGB"].ToString();
            txtTotalMail.Text = dtcln.Rows[0]["TotalMail"].ToString();

            if (dtcln.Rows[0]["Producthostclientserver"].ToString() != "")
            {
                Rdownclient.SelectedValue = Convert.ToInt32(dtcln.Rows[0]["Producthostclientserver"]).ToString();
                if (Rdownclient.SelectedValue == "1")
                {
                    pnlserver.Visible = false;
                }
                else
                {
                    pnlserver.Visible = true;
                    DataTable dtserass = select("Select * from  ServerAssignmentMasterTbl where   PricePlanId='" + i.ToString() + "'");
                    if (dtserass.Rows.Count > 0)
                    {
                        ddlserverMas.SelectedIndex = ddlserverMas.Items.IndexOf(ddlserverMas.Items.FindByValue(dtserass.Rows[0]["ServerId"].ToString()));

                    }
                }
            }
            EventArgs e = new EventArgs();
            object sender = new object();
            rdmultip.SelectedIndex = 0;
            for (int ik = 3; ik <= 21; ik++)
            {
                grdmulti.Columns[ik].Visible = false;
            }
            DataTable dtv = select("Select distinct Priceplanrestrecordallowtbl.Id as DTI,RecordsAllowed, PriceplanrestrictionTbl.NameofRest,PriceplanrestrictionTbl.Id,Priceaddingroup, Restingroup  from PriceplanrestrictionTbl Left join Priceplanrestrecordallowtbl on Priceplanrestrecordallowtbl.PriceplanRestrictiontblId=PriceplanrestrictionTbl.Id and PricePlanId='" + i.ToString() + "' where PriceplanrestrictionTbl.ProductversionId='" + ddlProductname1.SelectedValue + "' and PortalId='" + ddlportal.SelectedValue + "' ");
            grdmulti.DataSource = dtv;
            grdmulti.DataBind();
            if (grdmulti.Rows.Count > 0)
            {
                TextBox lblfo = (TextBox)grdmulti.HeaderRow.FindControl("lblfo1");
                TextBox txtbaseprice = (TextBox)grdmulti.HeaderRow.FindControl("txtbaseprice1");
                CheckBox chkplc = (CheckBox)grdmulti.HeaderRow.FindControl("chkplc1");
                CheckBox chkfreetryplan = (CheckBox)grdmulti.HeaderRow.FindControl("chkfreetryplan1");
                if (Convert.ToString(dtcln.Rows[0]["BasePrice"]) != "")
                {
                    txtbaseprice.Text = Convert.ToString(dtcln.Rows[0]["BasePrice"]);
                }
                if (Convert.ToString(dtcln.Rows[0]["Plancatedefault"]) != "")
                {
                    chkplc.Checked = Convert.ToBoolean(dtcln.Rows[0]["Plancatedefault"]);
                }

                if (Convert.ToString(dtcln.Rows[0]["IsItFreeTryOutPlan"]) != "")
                {
                    chkfreetryplan.Checked = Convert.ToBoolean(dtcln.Rows[0]["IsItFreeTryOutPlan"]);
                    CheckBox1.Checked = Convert.ToBoolean(dtcln.Rows[0]["IsItFreeTryOutPlan"]);

                }


                lblfo.Text = txtPlanAmount.Text;
                fillfiltertxt(1);

            }
            //EditProductModulePriceplan(hdnPricePlanId.Value);
            fillgd();
            CheckBox1_CheckedChanged(sender, e);
            chkfreetryplan1_chackedChanged(sender, e);
        }
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillfilterportal();
        ddlfilterportal_SelectedIndexChanged(sender, e);
    }

    protected void fillfilterportal()
    {
        ddlfilterportal.Items.Clear();
        string strcln22v = "Select * from PortalMasterTbl where ProductId In( Select distinct ProductMaster.ProductId from  ProductMaster  inner join VersionInfoMaster on VersionInfoMaster.productId=ProductMaster.ProductId where VersionInfoId = '" + DropDownList1.SelectedValue + "' ) and PortalMasterTbl.Status = '1'  order by PortalName"; //and PortalMasterTbl.Status = '1' add on 32615
        //string strcln22v = "Select * from PortalMasterTbl where ProductId = '" + DropDownList1.SelectedValue + "'  order by PortalName";
        SqlCommand cmdcln22v = new SqlCommand(strcln22v, con);
        DataTable dtcln22v = new DataTable();
        SqlDataAdapter adpcln22v = new SqlDataAdapter(cmdcln22v);
        adpcln22v.Fill(dtcln22v);
        ddlfilterportal.DataSource = dtcln22v;
        ddlfilterportal.DataValueField = "Id";
        ddlfilterportal.DataTextField = "PortalName";
        ddlfilterportal.DataBind();
        ddlfilterportal.Items.Insert(0, "All");
        ddlfilterportal.Items[0].Value = "0";



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


    protected void chkporder_CheckedChanged(object sender, EventArgs e)
    {
        if (chkporder.Checked == true)
        {
            pnlperorder.Visible = true;
        }
    }



    protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        ClearAll();
        lblmsg.Text = "";
        addnewpanel.Visible = true;
        pnladdnew.Visible = false;
        lbladdlabel.Text = "";
    }
    protected void addnewpanel_Click(object sender, EventArgs e)
    {
        addnewpanel.Visible = false;
        pnladdnew.Visible = true;
        lbladdlabel.Text = "Add New Priceplan";
        lblmsg.Text = "";
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        FillGrid();
    }

    protected void ddlProductname1_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillportal();
        //rdmultip_SelectedIndexChanged(sender, e);
        //lblprp1_TextChanged(sender, e);
        ddlportal_SelectedIndexChanged(sender, e);
    }
    protected void fillportal()
    {

        string activestr = "";
        if (chkupload.Checked == true)
        {
            activestr = " and PortalMasterTbl.Status=1";
        }
        ddlportal.Items.Clear();
        string strcln22v = "Select * from PortalMasterTbl where ProductId In( Select distinct ProductMaster.ProductId from  ProductMaster  inner join VersionInfoMaster on VersionInfoMaster.productId=ProductMaster.ProductId where VersionInfoId = '" + ddlProductname1.SelectedValue + "' ) " + activestr + " order by PortalName";
        SqlCommand cmdcln22v = new SqlCommand(strcln22v, con);
        DataTable dtcln22v = new DataTable();
        SqlDataAdapter adpcln22v = new SqlDataAdapter(cmdcln22v);
        adpcln22v.Fill(dtcln22v);
        ddlportal.DataSource = dtcln22v;

        ddlportal.DataValueField = "Id";
        ddlportal.DataTextField = "PortalName";
        ddlportal.DataBind();


        ddlportal.Items.Insert(0, "-Select-");
        ddlportal.Items[0].Value = "0";
    }
    protected DataTable select(string str)
    {
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);

        return dt;

    }
    protected void rdmultip_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (rdmultip.SelectedValue == "1")
        {
            lblmultipres.Text = "Set Price Plan Restrictions";
            pnlnoofmulti.Visible = false;
            pnlmultoplan.ScrollBars = ScrollBars.None;
            lblmd.Text = "";
            pnlprname.Visible = true;
            pnlmultoplan.Width = new Unit();

            grdmulti.Columns[2].Visible = true;
            grdmulti.Columns[3].Visible = false;
            grdmulti.Columns[4].Visible = false;
            grdmulti.Columns[5].Visible = false;
            grdmulti.Columns[6].Visible = false;
            grdmulti.Columns[7].Visible = false;
            grdmulti.Columns[8].Visible = false;
            grdmulti.Columns[9].Visible = false;
            grdmulti.Columns[10].Visible = false;
            grdmulti.Columns[11].Visible = false;
            grdmulti.Columns[12].Visible = false;
            grdmulti.Columns[13].Visible = false;
            grdmulti.Columns[14].Visible = false;
            grdmulti.Columns[15].Visible = false;
            grdmulti.Columns[16].Visible = false;
            grdmulti.Columns[17].Visible = false;
            grdmulti.Columns[18].Visible = false;
            grdmulti.Columns[19].Visible = false;
            grdmulti.Columns[20].Visible = false;
            grdmulti.Columns[21].Visible = false;
        }
        else
        {
            lblmultipres.Text = "Create multiple priceplans and set restrictions for each price plan. ";
            pnlmultoplan.ScrollBars = ScrollBars.Horizontal;
            lblmd.Text = "Common Price Plan Information";
            pnlprname.Visible = false;
            pnlnoofmulti.Visible = true;
            pnlmultoplan.Width = new Unit("1100");

            if (txtnoofalloed.Text.Length > 0)
            {
                if (Convert.ToInt32(txtnoofalloed.Text) > 20)
                {
                    txtnoofalloed.Text = "20";
                }
                for (int i = 2; i <= (20 + 1); i++)
                {
                    if (Convert.ToInt32(txtnoofalloed.Text) < (i - 1))
                    {
                        grdmulti.Columns[i].Visible = false;
                    }
                    else
                    {
                        grdmulti.Columns[i].Visible = true;
                    }
                }

            }
        }
        pnlmultoplan.Visible = true;


        DataTable dtv = select("Select PriceplanrestrictionTbl.NameofRest,PriceplanrestrictionTbl.Id,Priceaddingroup, Restingroup, ' ' as RecordsAllowed,'' as DTI  from ProductMaster inner join VersionInfoMaster on VersionInfoMaster.ProductId=  ProductMaster.ProductId  inner join ClientProductTableMaster on ClientProductTableMaster.VersionInfoId=VersionInfoMaster.VersionInfoId inner join PriceplanrestrictionTbl on PriceplanrestrictionTbl.TableId=ClientProductTableMaster.Id where PriceplanrestrictionTbl.ProductversionId='" + ddlProductname1.SelectedValue + "' and PriceplanrestrictionTbl.PortalId='" + ddlportal.SelectedValue + "'");
        grdmulti.DataSource = dtv;
        grdmulti.DataBind();
        fillgd();

        //if (rdmultip.SelectedValue == "2")
        //{
        //    pnlsingleplan.Visible = false;
        //}
        //if (rdmultip.SelectedValue == "1")
        //{
        //    pnlsingleplan.Visible = true;

        //}

    }
    protected void chkplc1_chackedChanged(object sender, EventArgs e)
    {
        if (rdmultip.SelectedValue == "1")
        {
            CheckBox chkplc = (CheckBox)grdmulti.HeaderRow.FindControl("chkplc1");
            if (chkplc.ID == ((CheckBox)sender).ID)
            {
            }
            else
            {
                chkplc.Checked = false;
            }
        }
        else
        {
            int Mn = Convert.ToInt32(txtnoofalloed.Text);
            for (int i = 1; i <= Mn; i++)
            {
                CheckBox chkplc = (CheckBox)grdmulti.HeaderRow.FindControl("chkplc" + i);
                if (chkplc.ID == ((CheckBox)sender).ID)
                {
                }
                else
                {
                    chkplc.Checked = false;
                }
            }
        }

    }
    protected void chkfreetryplan1_chackedChanged(object sender, EventArgs e)
    {

        if (rdmultip.SelectedValue == "1")
        {
            TextBox lblprp = (TextBox)grdmulti.HeaderRow.FindControl("lblprp1");

            CheckBox chkfreetryplan = (CheckBox)grdmulti.HeaderRow.FindControl("chkfreetryplan1");
            lbldisplanname.Text = "";
            if (chkfreetryplan.Checked == true)
            {
                lbldisplanname.Text = " " + lblprp.Text + " ";
                pnlfreetryout.Visible = true;
                CheckBox1.Checked = true;
            }
            else
            {
                pnlfreetryout.Visible = false;
                CheckBox1.Checked = false;
            }
            CheckBox1_CheckedChanged(sender, e);
        }
        else
        {
            int Mn = Convert.ToInt32(txtnoofalloed.Text);
            for (int i = 1; i <= Mn; i++)
            {
                TextBox lblprp = (TextBox)grdmulti.HeaderRow.FindControl("lblprp" + i);
                CheckBox chkfreetryplan = (CheckBox)grdmulti.HeaderRow.FindControl("chkfreetryplan" + i);
                if (chkfreetryplan.ID == ((CheckBox)sender).ID)
                {
                    if (chkfreetryplan.Checked == true)
                    {
                        lbldisplanname.Text = " " + lblprp.Text + " ";
                        pnlfreetryout.Visible = true;
                        CheckBox1.Checked = true;
                    }
                    else
                    {
                        pnlfreetryout.Visible = false;
                        CheckBox1.Checked = false;
                    }
                    CheckBox1_CheckedChanged(sender, e);
                }
                else
                {
                    chkfreetryplan.Checked = false;
                }
            }
        }

    }
    protected void chkAll_chackedChanged(object sender, EventArgs e)
    {
        CheckBox chk;
        foreach (GridViewRow rowitem in grdmulti.Rows)
        {
            chk = (CheckBox)(rowitem.FindControl("chkchield"));
            chk.Checked = ((CheckBox)sender).Checked;
            if (rdmultip.SelectedValue == "1")
            {
                RequiredFieldValidator ReqF1 = (RequiredFieldValidator)rowitem.FindControl("ReqF1");

                TextBox lblprprest1 = (TextBox)rowitem.FindControl("lblprprest1");
                lblprprest1.Enabled = Convert.ToBoolean(((CheckBox)sender).Checked);
                if (lblprprest1.Enabled == true)
                {
                    ReqF1.Visible = true;
                }
                else
                {
                    lblprprest1.Text = "";
                    ReqF1.Visible = false;
                }
            }
            else
            {
                int Mn = Convert.ToInt32(txtnoofalloed.Text);
                for (int i = 1; i <= Mn; i++)
                {
                    RequiredFieldValidator ReqF1 = (RequiredFieldValidator)rowitem.FindControl("ReqF" + i);

                    TextBox lblprprest1 = (TextBox)rowitem.FindControl("lblprprest" + i);
                    lblprprest1.Enabled = Convert.ToBoolean(((CheckBox)sender).Checked);
                    if (lblprprest1.Enabled == true)
                    {
                        ReqF1.Visible = true;
                    }
                    else
                    {
                        lblprprest1.Text = "";
                        ReqF1.Visible = false;
                    }
                }
            }
        }
    }
    protected void chkchield_chackedChanged(object sender, EventArgs e)
    {
        GridViewRow rd = ((CheckBox)sender).Parent.Parent as GridViewRow;
        int rimin = rd.RowIndex;
        if (rdmultip.SelectedValue == "1")
        {
            TextBox lblprprest1 = (TextBox)grdmulti.Rows[rimin].FindControl("lblprprest1");
            RequiredFieldValidator ReqF1 = (RequiredFieldValidator)grdmulti.Rows[rimin].FindControl("ReqF1");

            lblprprest1.Enabled = Convert.ToBoolean(((CheckBox)sender).Checked);

            if (lblprprest1.Enabled == true)
            {
                ReqF1.Visible = true;
            }
            else
            {
                lblprprest1.Text = "";
                ReqF1.Visible = false;
            }
        }
        else
        {
            int Mn = Convert.ToInt32(txtnoofalloed.Text);
            for (int i = 1; i <= Mn; i++)
            {
                TextBox lblprprest1 = (TextBox)grdmulti.Rows[rimin].FindControl("lblprprest" + i);
                lblprprest1.Enabled = Convert.ToBoolean(((CheckBox)sender).Checked);
                RequiredFieldValidator ReqF1 = (RequiredFieldValidator)grdmulti.Rows[rimin].FindControl("ReqF" + i);
                if (lblprprest1.Enabled == true)
                {
                    ReqF1.Visible = true;
                }
                else
                {
                    lblprprest1.Text = "";
                    ReqF1.Visible = false;
                }

            }
        }

    }
    protected void txtPlanName_TextChanged(object sender, EventArgs e)
    {
        fillgd();
    }
    protected void fillgd()
    {
        if (grdmulti.Rows.Count > 0)
        {
            TextBox lblprp1 = (TextBox)grdmulti.HeaderRow.FindControl("lblprp1");
            lblprp1.Text = txtPlanName.Text;
            fillfiltertxt(1);
        }

    }
    protected void btncalsugprice_Click(object sender, EventArgs e)
    {
        intch = 1;
        TextBox lblprp1 = (TextBox)grdmulti.HeaderRow.FindControl("lblprp1");

        if (lblprp1.Text.Length > 0)
        {
            fillgddata(1);

        }
        TextBox lblprp2 = (TextBox)grdmulti.HeaderRow.FindControl("lblprp2");

        if (lblprp2.Text.Length > 0)
        {
            fillgddata(2);

        }
        TextBox lblprp3 = (TextBox)grdmulti.HeaderRow.FindControl("lblprp3");

        if (lblprp3.Text.Length > 0)
        {
            fillgddata(3);

        }
        TextBox lblprp4 = (TextBox)grdmulti.HeaderRow.FindControl("lblprp4");

        if (lblprp4.Text.Length > 0)
        {
            fillgddata(4);

        }
        TextBox lblprp5 = (TextBox)grdmulti.HeaderRow.FindControl("lblprp5");

        if (lblprp5.Text.Length > 0)
        {
            fillgddata(5);

        }
        TextBox lblprp6 = (TextBox)grdmulti.HeaderRow.FindControl("lblprp6");

        if (lblprp6.Text.Length > 0)
        {
            fillgddata(6);

        }
        TextBox lblprp7 = (TextBox)grdmulti.HeaderRow.FindControl("lblprp7");

        if (lblprp7.Text.Length > 0)
        {
            fillgddata(7);

        }
        TextBox lblprp8 = (TextBox)grdmulti.HeaderRow.FindControl("lblprp8");

        if (lblprp3.Text.Length > 0)
        {
            fillgddata(8);

        }
        TextBox lblprp9 = (TextBox)grdmulti.HeaderRow.FindControl("lblprp9");

        if (lblprp9.Text.Length > 0)
        {
            fillgddata(9);

        }
        TextBox lblprp10 = (TextBox)grdmulti.HeaderRow.FindControl("lblprp10");

        if (lblprp10.Text.Length > 0)
        {
            fillgddata(10);

        }
        TextBox lblprp11 = (TextBox)grdmulti.HeaderRow.FindControl("lblprp11");

        if (lblprp11.Text.Length > 0)
        {
            fillgddata(11);

        }
        TextBox lblprp12 = (TextBox)grdmulti.HeaderRow.FindControl("lblprp12");

        if (lblprp12.Text.Length > 0)
        {
            fillgddata(12);

        }
        TextBox lblprp13 = (TextBox)grdmulti.HeaderRow.FindControl("lblprp13");

        if (lblprp13.Text.Length > 0)
        {
            fillgddata(13);

        }
        TextBox lblprp14 = (TextBox)grdmulti.HeaderRow.FindControl("lblprp14");

        if (lblprp14.Text.Length > 0)
        {
            fillgddata(14);

        }
        TextBox lblprp15 = (TextBox)grdmulti.HeaderRow.FindControl("lblprp15");

        if (lblprp15.Text.Length > 0)
        {
            fillgddata(15);

        }
        TextBox lblprp16 = (TextBox)grdmulti.HeaderRow.FindControl("lblprp16");

        if (lblprp16.Text.Length > 0)
        {
            fillgddata(16);

        }
        TextBox lblprp17 = (TextBox)grdmulti.HeaderRow.FindControl("lblprp17");

        if (lblprp17.Text.Length > 0)
        {
            fillgddata(17);

        }
        TextBox lblprp18 = (TextBox)grdmulti.HeaderRow.FindControl("lblprp18");

        if (lblprp18.Text.Length > 0)
        {
            fillgddata(18);

        }
        TextBox lblprp19 = (TextBox)grdmulti.HeaderRow.FindControl("lblprp19");

        if (lblprp19.Text.Length > 0)
        {
            fillgddata(19);

        }
        TextBox lblprp20 = (TextBox)grdmulti.HeaderRow.FindControl("lblprp20");

        if (lblprp20.Text.Length > 0)
        {
            fillgddata(20);

        }
        Label lblfo1vc = (Label)grdmulti.FooterRow.FindControl("lblfo1vc");
        lblfo1vc.Text = "Suggested Price";
    }
    protected void fillpageaccess(string PPID)
    {
        string Temp1 = "";
        string Temp1val = "";
        Temp1 = "insert into pageplaneaccesstbl(pageid,pagename,priceplanid,pageaccess)values ";
        DataTable dtxc = select("Select Distinct pageplaneaccesstbl.Pageid,PageMaster.PageName from PageMaster inner join pageplaneaccesstbl on pageplaneaccesstbl.PageId= PageMaster.PageId where pageaccess='1' and Priceplanid " +
        " in(select top(1) PricePlanMaster.PricePlanId from PricePlanMaster inner join pageplaneaccesstbl on pageplaneaccesstbl.PricePlanId= PricePlanMaster.PricePlanId where PricePlanMaster.PriceplancatId='" + ddlpriceplancatagory.SelectedValue + "')");
        foreach (DataRow item in dtxc.Rows)
        {
            if (Temp1val.Length > 0)
            {
                Temp1val += ",";
            }
            Temp1val += "('" + item["PageId"] + "','" + item["PageName"] + "','" + PPID + "','1')";
        }
        if (Temp1val.Length > 0)
        {            
            Temp1 += Temp1val;
            SqlCommand cd4 = new SqlCommand(Temp1, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cd4.ExecuteNonQuery();
            con.Close();
        }
    }

    protected void fillgddata(int rown)
    {
        decimal totpricegdrows = 0;
        foreach (GridViewRow item in grdmulti.Rows)
        {
            string restId = grdmulti.DataKeys[item.RowIndex].Value.ToString();

            Label lblrestgroup = (Label)item.FindControl("lblrestgroup");
            Label lblpriceaddgroup = (Label)item.FindControl("lblpriceaddgroup");

            TextBox lblprprest1 = (TextBox)item.FindControl("lblprprest" + rown);

            if (lblprprest1.Text != "")
            {
                if (Convert.ToDecimal(lblprprest1.Text) <= Convert.ToDecimal(lblrestgroup.Text))
                {
                    lblprprest1.Text = lblrestgroup.Text;
                }
                else if (Convert.ToDecimal(lblprprest1.Text) > Convert.ToDecimal(lblrestgroup.Text))
                {
                    Int32 Totno = Convert.ToInt32(lblprprest1.Text) / Convert.ToInt32(lblrestgroup.Text);
                    if (Convert.ToDecimal(Totno * Convert.ToInt32(lblrestgroup.Text)) != Convert.ToDecimal(lblprprest1.Text))
                    {
                        decimal totpairamt = (Convert.ToInt32(lblrestgroup.Text) * Totno) + Convert.ToInt32(lblrestgroup.Text);
                        lblprprest1.Text = totpairamt.ToString();
                    }
                }
                Int32 Totorgn = Convert.ToInt32(lblprprest1.Text) / Convert.ToInt32(lblrestgroup.Text);
                totpricegdrows += Convert.ToDecimal(lblpriceaddgroup.Text) * Convert.ToDecimal(Totorgn);
            }
        }
        TextBox txtbaseprice = (TextBox)grdmulti.HeaderRow.FindControl("txtbaseprice" + rown);
        TextBox lblfo = (TextBox)grdmulti.HeaderRow.FindControl("lblfo" + rown);
        if (txtbaseprice.Text.Length > 0)
        {
            lblfo.Text = (Convert.ToDecimal(txtbaseprice.Text) + totpricegdrows).ToString();
        }

        //lblfo.Text = totpricegdrows.ToString();
    }
    protected void lblprp1_TextChanged(object sender, EventArgs e)
    {
        fillfiltertxt(1);
    }
    protected void lblprp2_TextChanged(object sender, EventArgs e)
    {
        fillfiltertxt(2);
    }
    protected void lblprp3_TextChanged(object sender, EventArgs e)
    {
        fillfiltertxt(3);
    }
    protected void lblprp4_TextChanged(object sender, EventArgs e)
    {
        fillfiltertxt(4);
    }
    protected void lblprp5_TextChanged(object sender, EventArgs e)
    {
        fillfiltertxt(5);
    }
    protected void lblprp6_TextChanged(object sender, EventArgs e)
    {
        fillfiltertxt(6);
    }
    protected void lblprp7_TextChanged(object sender, EventArgs e)
    {
        fillfiltertxt(7);
    }
    protected void lblprp8_TextChanged(object sender, EventArgs e)
    {
        fillfiltertxt(8);
    }
    protected void lblprp9_TextChanged(object sender, EventArgs e)
    {
        fillfiltertxt(9);
    }
    protected void lblprp10_TextChanged(object sender, EventArgs e)
    {
        fillfiltertxt(10);
    }
    protected void lblprp11_TextChanged(object sender, EventArgs e)
    {
        fillfiltertxt(11);
    }
    protected void lblprp12_TextChanged(object sender, EventArgs e)
    {
        fillfiltertxt(12);
    }
    protected void lblprp13_TextChanged(object sender, EventArgs e)
    {
        fillfiltertxt(13);
    }
    protected void lblprp14_TextChanged(object sender, EventArgs e)
    {
        fillfiltertxt(14);
    }
    protected void lblprp15_TextChanged(object sender, EventArgs e)
    {
        fillfiltertxt(15);
    }
    protected void lblprp16_TextChanged(object sender, EventArgs e)
    {
        fillfiltertxt(16);
    }
    protected void lblprp17_TextChanged(object sender, EventArgs e)
    {
        fillfiltertxt(17);
    }
    protected void lblprp18_TextChanged(object sender, EventArgs e)
    {
        fillfiltertxt(18);
    }
    protected void lblprp19_TextChanged(object sender, EventArgs e)
    {
        fillfiltertxt(19);
    }
    protected void lblprp20_TextChanged(object sender, EventArgs e)
    {
        fillfiltertxt(20);
    }
    protected void fillfiltertxt(int no)
    {
        if (grdmulti.Rows.Count > 0)
        {
            TextBox lblprp1 = (TextBox)grdmulti.HeaderRow.FindControl("lblprp" + no);

            foreach (GridViewRow item in grdmulti.Rows)
            {
                CheckBox chkchield = (CheckBox)item.FindControl("chkchield");
                RequiredFieldValidator ReqF1 = (RequiredFieldValidator)item.FindControl("ReqF" + no);
                if (chkchield.Checked == true)
                {
                    if (lblprp1.Text.Length > 0)
                    {
                        ReqF1.Visible = true;
                    }
                    else
                    {
                        ReqF1.Visible = false;
                    }
                }
            }
        }
    }
    protected void ddlfilproductcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
    }
    protected void ddlportal_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillpriceplancate();

        rdmultip_SelectedIndexChanged(sender, e);
        lblprp1_TextChanged(sender, e);
    }
    protected void ddlfilterportal_SelectedIndexChanged(object sender, EventArgs e)
    {

            string activestr = "";
        if (chkupload.Checked == true)
        {
            activestr = " and         Priceplancategory.Status=1";
        }

        string strcln = " SELECT distinct * FROM Priceplancategory where PortalId='" + ddlfilterportal.SelectedValue + "' " + activestr + " order  by CategoryName";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);

        ddlfilproductcategory.DataSource = dtcln;
        ddlfilproductcategory.DataTextField = "CategoryName";
        ddlfilproductcategory.DataValueField = "Id";
        ddlfilproductcategory.DataBind();
        ddlfilproductcategory.Items.Insert(0, "All");
        ddlfilproductcategory.Items[0].Value = "0";
        ddlfilproductcategory_SelectedIndexChanged(sender, e);
    }


    protected void Rdownclient_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Rdownclient.SelectedValue == "1")
        {
            pnlserver.Visible = false;
        }
        else
        {
            pnlserver.Visible = true;

            //FillProductModulePriceplan();
        }
    }
    protected void serverdd()
    {
        string activestr = "";
        if (chkupload.Checked == true)
        {
            activestr = " and VersionInfoMaster.Active=1";
        }


        string strcln = " SELECT distinct ServerName,Id FROM ServerMasterTbl where Status='1' order  by ServerName";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);

        ddlserverMas.DataSource = dtcln;
        ddlserverMas.DataTextField = "ServerName";
        ddlserverMas.DataValueField = "Id";
        ddlserverMas.DataBind();
        ddlserverMas.Items.Insert(0, "-Select-");
        ddlserverMas.Items[0].Value = "0";

    }
   
    protected void btndosyncro_Click(object sender, EventArgs e)
    {

        if (rdsync.SelectedValue == "1")
        {
            int transf = 0;
            DataTable dt1 = select(" SELECT DISTINCT SatelliteSyncronisationrequiringTablesMaster.Id FROM ClientProductTableMaster INNER JOIN SatelliteSyncronisationrequiringTablesMaster ON ClientProductTableMaster.Id = SatelliteSyncronisationrequiringTablesMaster.TableID where SatelliteSyncronisationrequiringTablesMaster.Status='1' and ClientProductTableMaster.TableName='Priceplanrestrecordallowtbl' and ClientProductTableMaster.VersionInfoId='" + ddlProductname1.SelectedValue + "' and SatelliteSyncronisationrequiringTablesMaster.ProductVersionID='" + ddlProductname1.SelectedValue + "' ");
            if (dt1.Rows.Count > 0)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    string datetim = DateTime.Now.ToString();
                    string arqid = dt1.Rows[i]["Id"].ToString();

                    string str22 = "Insert Into SyncronisationrequiredTbl(SatelliteSyncronisationrequiringTablesMasterID,DateandTime)Values('" + arqid + "','" + Convert.ToDateTime(datetim) + "')";
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    SqlCommand cmn = new SqlCommand(str22, con);
                    cmn.ExecuteNonQuery();
                    con.Close();

                    DataTable dt121 = select("SELECT Max(ID) as ID from SyncronisationrequiredTbl where SatelliteSyncronisationrequiringTablesMasterID='" + arqid + "'");

                    if (Convert.ToString(dt121.Rows[0]["ID"]) != "")
                    {
                        DataTable dtcln = select("SELECT Distinct ServerMasterTbl.Id FROM ServerMasterTbl inner join ServerAssignmentMasterTbl on ServerAssignmentMasterTbl.ServerId=ServerMasterTbl.Id inner join  PricePlanMaster on PricePlanMaster.PricePlanId=ServerAssignmentMasterTbl.PricePlanId    where ServerMasterTbl.Status='1' and ServerAssignmentMasterTbl.Active='1' and PricePlanMaster.active='1' and  PricePlanMaster.VersionInfoMasterId='" + ddlProductname1.SelectedValue + "'");

                        for (int j = 0; j < dtcln.Rows.Count; j++)
                        {
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            transf = Convert.ToInt32(ddlProductname1.SelectedValue);
                            string str223 = "Insert Into SateliteServerRequiringSynchronisationMasterTbl(SyncronisationrequiredTBlID,[servermasterID],[SynchronisationSuccessful],[SynchronisationSuccessfulDatetime])Values('" + dt121.Rows[0]["ID"] + "','" + dtcln.Rows[j]["Id"] + "','0','" + DateTime.Now.ToString() + "')";
                            SqlCommand cmn3 = new SqlCommand(str223, con);
                            cmn3.ExecuteNonQuery();
                            con.Close();
                        }
                    }


                }

            }


            if (transf > 0)
            {
                string te = "SyncData.aspx?verId=" + transf;
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

            }
        }
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox1.Checked == true)
        {
            pnlfreetryout.Visible = true;
        }
        else
        {
            pnlfreetryout.Visible = false;
        }
    }
    protected void LinkButton97666667_Click(object sender, ImageClickEventArgs e)
    {
        string te = "Priceplanrestriction.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void LinkButton13_Click(object sender, ImageClickEventArgs e)
    {
        rdmultip_SelectedIndexChanged(sender, e);
    }
    protected void btnlinkpop_Click(object sender, EventArgs e)
    {
        string ANT = "There can be only one price plan for a portal which can be selected as a free and<br>" +
                                                   " try out plan offered by default all new client for the portal, so this price plan<br>" +
                                                   " would replace any other price plan already selected for being offered as free try<br>" +
                                                   " out plan previously.";
        lbllllv.Text = ANT;
        ModalPopupExtender4.Show();
    }
    protected void lnkpop_Click(object sender, EventArgs e)
    {
        string ANT = "(For Eg. If xyz is mentioned here; and when new company for any other portal with<br>" +
                                                          " company ID 'capman' is created; the second company ID 'xyzcapman' would be created<br>" +
                                                          " for being used for this free try out plan)";

        lbllllv.Text = ANT;
        ModalPopupExtender4.Show();
    }
    protected void lnkpopmm_Click(object sender, EventArgs e)
    {
        string ANT = "For Eg. You can mention following text would you like to subscribe to 90 days free<br>" +
                                                  "  plan of BusiDirectory.com - an online business directory ?";

        lbllllv.Text = ANT;
        ModalPopupExtender4.Show();
    }
    protected void ddlstfilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void grdmulti_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 1;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Price Plan Name";
            HeaderCell.ColumnSpan = 20;
            HeaderCell.HorizontalAlign = HorizontalAlign.Left;
            HeaderGridRow.BackColor = System.Drawing.Color.FromArgb(65, 98, 113);
            HeaderCell.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            HeaderGridRow.Cells.Add(HeaderCell);
            grdmulti.Controls[0].Controls.AddAt(0, HeaderGridRow);
        }
    }
    protected void txtnoofalloed_TextChanged(object sender, EventArgs e)
    {
        rdmultip_SelectedIndexChanged(sender, e);
    }



  

   
    
  
}

