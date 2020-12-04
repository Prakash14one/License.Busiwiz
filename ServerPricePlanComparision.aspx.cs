using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;

public partial class PricePlanComparision : System.Web.UI.Page
{
    string strcln;
    string id11;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    public static string VerId = "";
    public static string Cond = "0";
    public static int btnstep = 0;
    public static int btnfistsele = 0;
    public static string catid = "";
    public static string tempdata = "";
    public static string selcol = "";
    public static int discat = 0;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        if (!IsPostBack)
        {

            if ((Request.QueryString["orderid"]) != null)
            {
                Session["orderid"] = Request.QueryString["orderid"].ToString();
                if ((Request.QueryString["PN"]) != null)
                {
                    Session["PortalName"] = Request.QueryString["PN"].ToString();
                }
                if (Request.QueryString["id"] != null)
                {

                    Session["ProductId"] = Request.QueryString["id"];
                    string strcln = "SELECT Top(1) ProductMaster.ClientMasterId,VersionInfoMaster.VersionInfoId from  ProductMaster inner join VersionInfoMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId where ProductMaster.ProductId='" + Session["ProductId"] + "' ";
                    SqlCommand cmdcln = new SqlCommand(strcln, con);
                    DataTable dtcln = new DataTable();
                    SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
                    adpcln.Fill(dtcln);



                    Session["ClientId"] = dtcln.Rows[0]["ClientMasterId"];
                    string strclnxx = " SELECT distinct ProductMaster.ProductId,ProductDetail.Active,VersionInfoMaster.VersionInfoId,ProductMaster.ProductName + ' : ' + VersionInfoMaster.VersionInfoName as productversion FROM ProductMaster inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId where VersionInfoMaster.ProductId='" + Session["ProductId"] + "' and ClientMasterId=" + Session["ClientId"].ToString() + " and ProductDetail.Active='1'  order  by productversion";

                    SqlCommand cmd = new SqlCommand(strclnxx, con);
                    DataTable dtc = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dtc);
                    ddlproduct.DataSource = dtc;

                    ddlproduct.DataValueField = "VersionInfoId";
                    ddlproduct.DataTextField = "productversion";
                    ddlproduct.DataBind();
                    VerId = Convert.ToString(ddlproduct.SelectedValue);
                    ddlproduct_SelectedIndexChanged(sender, e);
                    //filldefaultprice();

                }
            }
        }
    }
   
    protected void ddlproduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        VerId = Convert.ToString(ddlproduct.SelectedValue);
        fillportal();
        if (Request.QueryString["PN"] != null)
        {
            string Portalname = Request.QueryString["PN"].ToUpper();
            Session["PortalName"] = Request.QueryString["PN"].ToString();
            Session["VD"] = Request.QueryString["Id"];
            for (int i = 0; i < ddlportal.Items.Count; i++)
            {
                if (Portalname == ddlportal.Items[i].Text.ToUpper())
                {
                    ddlportal.Items[i].Selected = true;
                    break;
                }
            }
        }
        ddlportal_SelectedIndexChanged(sender, e);
        // filldefaultprice();

    }
    protected void ddlportal_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
        fillpriceplancate();
    }
    protected void FillGrid()
    {
        gvCustomers.DataSource = null;
        gvCustomers.DataBind();
        DataTable dtclnv = select(" SELECT Top(7) * FROM Priceplancategory where PortalId='" + ddlportal.SelectedValue + "' and CategoryTypeSubID=" + Request.QueryString["subTypeid"] + " order  by Id ");
        if (dtclnv.Rows.Count > 0)
        {
            discat = dtclnv.Rows.Count;
            Cond = "0";
            DataTable dtTemp = new DataTable();
            dtTemp = CreateData();
            //        string strcln = "";
            strcln = "SELECT Distinct productfeature,Productfeaturetbl.Id FROM Productfeaturetbl inner join Priceplanfeatureproduct on Priceplanfeatureproduct.productfeatureid=Productfeaturetbl.Id  where portalid='" + ddlportal.SelectedValue + "' and  Productfeaturetbl.productversionid='" + VerId + "' ";

            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);
            int kb = 0;
            for (int i = 0; i < dtcln.Rows.Count; i++)
            {
                DataRow dtr2 = dtTemp.NewRow();
                dtr2["Id"] = kb;
                dtr2["Fid"] = Convert.ToString(dtcln.Rows[i]["Id"]);
                dtr2["Name"] = Convert.ToString(dtcln.Rows[i]["productfeature"]);
                //dtr2["Rate"] = item["Qty"];
                dtTemp.Rows.Add(dtr2);
                kb += 1;
            }
            if (kb > 0)
            {
                DataRow dtr2 = dtTemp.NewRow();
                dtr2["Id"] = kb;
                dtr2["Fid"] = "P2";
                dtr2["Name"] = "Validity Period(Days)";

                dtTemp.Rows.Add(dtr2);
                kb += 1;
            }
            if (kb > 0)
            {
                DataRow dtr2 = dtTemp.NewRow();
                dtr2["Id"] = kb;
                dtr2["Fid"] = "P1";
                dtr2["Name"] = "Amount To pay";

                dtTemp.Rows.Add(dtr2);
                kb += 1;
            }
            if (kb > 0)
            {
                //DataRow dtr2 = dtTemp.NewRow();
                //dtr2["Id"] = kb;
                //dtr2["Fid"] = "PA2";
                //dtr2["Name"] = "One Time Setup fees";

                //dtTemp.Rows.Add(dtr2);
                //kb += 1;
            }
            if (kb > 0)
            {
                //DataRow dtr2 = dtTemp.NewRow();
                //dtr2["Id"] = kb;
                //dtr2["Fid"] = "PA3";
                //dtr2["Name"] = "Total Amt";

                //dtTemp.Rows.Add(dtr2);
                //kb += 1;
            }


            for (int i = 1; i <= 3; i++)
            {

                DataRow dtr2 = dtTemp.NewRow();
                dtr2["Id"] = kb;

                if (i == 3)
                {
                    dtr2["Fid"] = "f3";
                    dtr2["Name"] = "View Details";
                }
                else
                {
                    if (i == 1)
                    {
                        dtr2["Fid"] = "f1";
                        dtr2["Name"] = "Subscribe Now";
                    }
                    else if (i == 2)
                    {
                        dtr2["Fid"] = "f2";
                        dtr2["Name"] = "";//Customise Order
                    }
                }
                //dtr2["Rate"] = item["Qty"];
                dtTemp.Rows.Add(dtr2);
                kb += 1;
            }
            gvCustomers.DataSource = dtTemp;
            gvCustomers.DataBind();
        }
    }
    protected void fillpriceplancate()
    {

        //if (gvCustomers.Rows.Count > 0)
        //{
        discat = 0;
        gvCustomers.Columns[2].Visible = false;
        gvCustomers.Columns[3].Visible = false;
        gvCustomers.Columns[4].Visible = false;
        gvCustomers.Columns[5].Visible = false;
        gvCustomers.Columns[6].Visible = false;
        gvCustomers.Columns[7].Visible = false;
        gvCustomers.Columns[8].Visible = false;

        DataTable dtcln = select(" SELECT Top(7)* FROM Priceplancategory where status=1 and  PortalId='" + ddlportal.SelectedValue + "' and CategoryTypeSubID=" + Request.QueryString["subTypeid"] + " order  by Id ");
        if (dtcln.Rows.Count > 0)
        {
            discat = dtcln.Rows.Count;
            if (dtcln.Rows.Count > 0)
            {
                Label lblhead1 = (Label)gvCustomers.HeaderRow.FindControl("lblhead1");
                Label lblhead1id = (Label)gvCustomers.HeaderRow.FindControl("lblhead1id");
                lblhead1.Text = Convert.ToString(dtcln.Rows[0]["CategoryName"]);
                lblhead1id.Text = Convert.ToString(dtcln.Rows[0]["Id"]);
                gvCustomers.Columns[2].Visible = true;
            }
            if (dtcln.Rows.Count > 1)
            {
                Label lblhead1 = (Label)gvCustomers.HeaderRow.FindControl("lblhead2");
                Label lblhead1id = (Label)gvCustomers.HeaderRow.FindControl("lblhead2id");
                lblhead1.Text = Convert.ToString(dtcln.Rows[1]["CategoryName"]);
                lblhead1id.Text = Convert.ToString(dtcln.Rows[1]["Id"]);
                gvCustomers.Columns[3].Visible = true;
            }
            if (dtcln.Rows.Count > 2)
            {
                Label lblhead1 = (Label)gvCustomers.HeaderRow.FindControl("lblhead3");
                Label lblhead1id = (Label)gvCustomers.HeaderRow.FindControl("lblhead3id");
                lblhead1.Text = Convert.ToString(dtcln.Rows[2]["CategoryName"]);
                lblhead1id.Text = Convert.ToString(dtcln.Rows[2]["Id"]);
                gvCustomers.Columns[4].Visible = true;
            }
            if (dtcln.Rows.Count > 3)
            {
                Label lblhead1 = (Label)gvCustomers.HeaderRow.FindControl("lblhead4");
                Label lblhead1id = (Label)gvCustomers.HeaderRow.FindControl("lblhead4id");
                lblhead1.Text = Convert.ToString(dtcln.Rows[3]["CategoryName"]);
                lblhead1id.Text = Convert.ToString(dtcln.Rows[3]["Id"]);
                gvCustomers.Columns[5].Visible = true;
            }
            if (dtcln.Rows.Count > 4)
            {
                Label lblhead1 = (Label)gvCustomers.HeaderRow.FindControl("lblhead5");
                Label lblhead1id = (Label)gvCustomers.HeaderRow.FindControl("lblhead5id");
                lblhead1.Text = Convert.ToString(dtcln.Rows[4]["CategoryName"]);
                lblhead1id.Text = Convert.ToString(dtcln.Rows[4]["Id"]);
                gvCustomers.Columns[6].Visible = true;
            }
            if (dtcln.Rows.Count > 5)
            {
                Label lblhead1 = (Label)gvCustomers.HeaderRow.FindControl("lblhead6");
                Label lblhead1id = (Label)gvCustomers.HeaderRow.FindControl("lblhead6id");
                lblhead1.Text = Convert.ToString(dtcln.Rows[5]["CategoryName"]);
                lblhead1id.Text = Convert.ToString(dtcln.Rows[5]["Id"]);
                gvCustomers.Columns[7].Visible = true;
            }
            if (dtcln.Rows.Count > 6)
            {
                Label lblhead1 = (Label)gvCustomers.HeaderRow.FindControl("lblhead7");
                Label lblhead1id = (Label)gvCustomers.HeaderRow.FindControl("lblhead7id");
                lblhead1.Text = Convert.ToString(dtcln.Rows[6]["CategoryName"]);
                lblhead1id.Text = Convert.ToString(dtcln.Rows[6]["Id"]);
                gvCustomers.Columns[8].Visible = true;
            }
        }
        //}

    }
    protected void fillportal()
    {
        ddlportal.Items.Clear();
        string strcln22v = "Select * from PortalMasterTbl where ProductId In( Select distinct ProductMaster.ProductId from  ProductMaster  inner join VersionInfoMaster on VersionInfoMaster.productId=ProductMaster.ProductId where VersionInfoId = '" + ddlproduct.SelectedValue + "' ) order by PortalName";
        SqlCommand cmdcln22v = new SqlCommand(strcln22v, con);
        DataTable dtcln22v = new DataTable();
        SqlDataAdapter adpcln22v = new SqlDataAdapter(cmdcln22v);
        adpcln22v.Fill(dtcln22v);
        ddlportal.DataSource = dtcln22v;

        ddlportal.DataValueField = "Id";
        ddlportal.DataTextField = "PortalName";
        ddlportal.DataBind();

    }
  
    public DataTable CreateData()
    {
        DataTable dtTemp = new DataTable();
        DataColumn prd1 = new DataColumn();
        prd1.ColumnName = "Fid";
        prd1.DataType = System.Type.GetType("System.String");
        prd1.AllowDBNull = true;
        dtTemp.Columns.Add(prd1);

        DataColumn prd11 = new DataColumn();
        prd11.ColumnName = "Id";
        prd11.DataType = System.Type.GetType("System.String");
        prd11.AllowDBNull = true;
        dtTemp.Columns.Add(prd11);

        DataColumn prd111 = new DataColumn();
        prd111.ColumnName = "Name";
        prd111.DataType = System.Type.GetType("System.String");
        prd111.AllowDBNull = true;
        dtTemp.Columns.Add(prd111);


        return dtTemp;
    }
    protected DataTable select(string str)
    {
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);
        return dt;
    }
    public DataTable itemdy()
    {
        DataTable dt = new DataTable();
        DataColumn Dcom = new DataColumn();
        Dcom.DataType = System.Type.GetType("System.String");
        Dcom.ColumnName = "Id";
        Dcom.AllowDBNull = true;
        Dcom.Unique = false;
        Dcom.ReadOnly = false;

        DataColumn Dcom1 = new DataColumn();
        Dcom1.DataType = System.Type.GetType("System.String");
        Dcom1.ColumnName = "productfeature";
        Dcom1.AllowDBNull = true;
        Dcom1.Unique = false;
        Dcom1.ReadOnly = false;

        //DataColumn Dcom2 = new DataColumn();
        //Dcom2.DataType = System.Type.GetType("System.String");
        //Dcom2.ColumnName = "passGrade";
        //Dcom2.AllowDBNull = true;
        //Dcom2.Unique = false;
        //Dcom2.ReadOnly = false;
        //DataColumn Dcom3 = new DataColumn();
        //Dcom3.DataType = System.Type.GetType("System.String");
        //Dcom3.ColumnName = "spsubject";
        //Dcom3.AllowDBNull = true;
        //Dcom3.Unique = false;
        //Dcom3.ReadOnly = false;

        dt.Columns.Add(Dcom);
        dt.Columns.Add(Dcom1);
        //dt.Columns.Add(Dcom2);
        //dt.Columns.Add(Dcom3);


        return dt;
    }
    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Cond == "0")
            {
                fillpriceplancate();
                Cond = "2";
            }
            int customerId = Convert.ToInt32(gvCustomers.DataKeys[e.Row.RowIndex].Value.ToString());
            Label lblcb = (Label)e.Row.FindControl("lblcb");
            Label lblfeid = (Label)e.Row.FindControl("lblfeid");
            Panel pnlA = (Panel)e.Row.FindControl("pnlA");
            Image imghead1 = (Image)e.Row.FindControl("imghead1");


            if (lblfeid.Text == "P1")
            {
                lblcb.Text = "Price";
                for (int i = 1; i <= discat; i++)
                {
                    Label lblhead1id = (Label)gvCustomers.HeaderRow.FindControl("lblhead" + i + "id");
                    DataTable dtcln = select("Select Distinct Top(7) PricePlanMaster.PricePlanId, PricePlanMaster.PricePlanAmount,PricePlanMaster.PricePlanId,Plancatedefault from PriceplanrestrictionTbl inner join Priceplanrestrecordallowtbl on Priceplanrestrecordallowtbl.PriceplanRestrictiontblId=PriceplanrestrictionTbl.Id inner join PricePlanMaster on PricePlanMaster.PricePlanId=Priceplanrestrecordallowtbl.PricePlanId where  PriceplancatId='" + lblhead1id.Text + "' and  PricePlanMaster.VersionInfoMasterId='" + ddlproduct.SelectedValue + "' and  PriceplanrestrictionTbl.portalid='" + ddlportal.SelectedValue + "' order by Plancatedefault Desc");

                    HtmlAnchor HR1 = (HtmlAnchor)e.Row.FindControl("HR" + i);
                    Label lbla1 = (Label)e.Row.FindControl("lbla" + i);
                    Label lbla11 = (Label)e.Row.FindControl("lbla" + i);
                    Label lblpid = (Label)e.Row.FindControl("lblpid" + i);
                    HR1.Visible = true;
                    lbla1.Visible = true;
                    lbla1.Enabled = true;
                    lbla1.Text = "0";
                    if (dtcln.Rows.Count > 0)
                    {
                        lblpid.Text = dtcln.Rows[0]["PricePlanId"].ToString(); 
                        lbla1.Text = Convert.ToString(dtcln.Rows[0]["PricePlanAmount"]);
                        lbla11.Text = Convert.ToString(dtcln.Rows[0]["PricePlanAmount"]);
                    }
                }
            }
            else if (lblfeid.Text == "P2")
            {
                lblcb.Text = "Validity Period(Days)";
                for (int i = 1; i <= discat; i++)
                {
                    Label lblhead1id = (Label)gvCustomers.HeaderRow.FindControl("lblhead" + i + "id");
                    DataTable dtcln = select("Select Distinct Top(1) PricePlanMaster.DurationMonth,PricePlanMaster.PricePlanId,Plancatedefault from PriceplanrestrictionTbl inner join Priceplanrestrecordallowtbl on Priceplanrestrecordallowtbl.PriceplanRestrictiontblId=PriceplanrestrictionTbl.Id inner join PricePlanMaster on PricePlanMaster.PricePlanId=Priceplanrestrecordallowtbl.PricePlanId where PriceplancatId='" + lblhead1id.Text + "' and  PricePlanMaster.VersionInfoMasterId='" + ddlproduct.SelectedValue + "' and  PriceplanrestrictionTbl.portalid='" + ddlportal.SelectedValue + "' order by Plancatedefault Desc");

                    HtmlAnchor HR1 = (HtmlAnchor)e.Row.FindControl("HR" + i);
                    Label lbla1 = (Label)e.Row.FindControl("lbla" + i);
                  
                    HR1.Visible = true;
                    lbla1.Visible = true;
                    lbla1.Enabled = true;
                    lbla1.Text = "0";
                    if (dtcln.Rows.Count > 0)
                    {
                        lbla1.Text = Convert.ToString(dtcln.Rows[0]["DurationMonth"]);
                    }

                }
            }
            else if (lblfeid.Text == "f1")
            {
                lblcb.Text = "";
                for (int i = 1; i <= discat; i++)
                {
                    Label lblhead1id = (Label)gvCustomers.HeaderRow.FindControl("lblhead" + i + "id");
                    HtmlAnchor HR1 = (HtmlAnchor)e.Row.FindControl("HR" + i);
                    HtmlAnchor HR11 = (HtmlAnchor)e.Row.FindControl("HR" + i);
                    Button btnBuySelectSer = (Button)e.Row.FindControl("btnBuySelectSer" + i);
                    Label lbla1 = (Label)e.Row.FindControl("lbla" + i);
                    btnBuySelectSer.Visible = true;
                    lbla1.CssClass = "BTNTorder";
                    HR1.Visible = false;
                    lbla1.Visible = false;
                    HR1.HRef = "";

                    DataTable dtcln = select("Select Distinct Top(1) PricePlanMaster.PricePlanAmount,PricePlanMaster.PricePlanId,Plancatedefault from PriceplanrestrictionTbl inner join Priceplanrestrecordallowtbl on Priceplanrestrecordallowtbl.PriceplanRestrictiontblId=PriceplanrestrictionTbl.Id inner join PricePlanMaster on PricePlanMaster.PricePlanId=Priceplanrestrecordallowtbl.PricePlanId where PriceplancatId='" + lblhead1id.Text + "' and  PricePlanMaster.VersionInfoMasterId='" + ddlproduct.SelectedValue + "' and  PriceplanrestrictionTbl.portalid='" + ddlportal.SelectedValue + "' order by Plancatedefault Desc");
                    if (dtcln.Rows.Count > 0)
                    {

                        HR1.HRef = "Ordernow.aspx?Pid=" + Request.QueryString["PPid"] + "&spid="+Convert.ToString(dtcln.Rows[0]["PricePlanId"])+"";
                        HR1.Name = Convert.ToString(dtcln.Rows[0]["PricePlanId"]);
                        HR11.Name = Convert.ToString(dtcln.Rows[0]["PricePlanId"]);


                    }

                }
            }
            else if (lblfeid.Text == "f2")
            {
               
            }
            else if (lblfeid.Text == "f3")
            {
                pnlA.Visible = false;
                lblcb.Visible = false;
                //pnlA.Visible = true;
                // lblcb.Text = "";
                //GridView grdnoof = e.Row.FindControl("grdnoof") as GridView;
                //DataTable dtcln = select("Select Distinct PriceplanrestrictionTbl.Id,NameofRest,TextofQueinSelection,Priceaddingroup,Restingroup from PriceplanrestrictionTbl inner join Priceplanrestrecordallowtbl on Priceplanrestrecordallowtbl.PriceplanRestrictiontblId=PriceplanrestrictionTbl.Id inner join PricePlanMaster on PricePlanMaster.PricePlanId=Priceplanrestrecordallowtbl.PricePlanId where PricePlanMaster.VersionInfoMasterId='" + ddlproduct.SelectedValue + "' and  PriceplanrestrictionTbl.portalid='" + ddlportal.SelectedValue + "'");
                //grdnoof.DataSource = dtcln;
                //grdnoof.DataBind();

            }
           
            else
            {
                pnlA.Visible = true;
                GridView gvOrders = e.Row.FindControl("gvOrders") as GridView;

                if (lblfeid.Text != "f1" && lblfeid.Text != "f2" && lblfeid.Text != "f3" && lblfeid.Text != "P1")
                {
                    gvOrders.Columns[1].Visible = false;
                    gvOrders.Columns[2].Visible = false;
                    gvOrders.Columns[3].Visible = false;
                    gvOrders.Columns[4].Visible = false;
                    gvOrders.Columns[5].Visible = false;
                    gvOrders.Columns[6].Visible = false;
                    gvOrders.Columns[7].Visible = false;
                    for (int mn = 1; mn <= discat; mn++)
                    {
                        gvOrders.Columns[mn].Visible = true;
                    }
                    DataTable dtsd = select("select  * from Priceplanfeatureproduct where productfeatureid='" + lblfeid.Text + "' ");
                    gvOrders.DataSource = dtsd;
                    gvOrders.DataBind();
                    

                    foreach (GridViewRow itn in gvOrders.Rows)
                    {
                        String Temp3 = " and PriceplancategoryId In( ";
                        string Temp3val = "";
                        string cgvid = gvOrders.DataKeys[itn.RowIndex].Value.ToString();

                        for (int i = 1; i <= discat; i++)
                        {
                            Label lblhead1id = (Label)gvCustomers.HeaderRow.FindControl("lblhead" + i + "id");
                            if (lblhead1id.Text.Length > 0)
                            {
                                if (Temp3val.Length > 0)
                                {
                                    Temp3val += ",";
                                }
                                Temp3val += "'" + lblhead1id.Text + "'";
                            }
                        }
                        if (Temp3val.Length > 0)
                        {
                            Temp3 += Temp3val + ")";
                            DataTable dtfi = select("Select * from PriceplanfeatureproductwithPLcategory where PriceplanfeatureproductId='" + cgvid + "' " + Temp3);
                            foreach (DataRow dtr in dtfi.Rows)
                            {
                                for (int hj = 1; hj <= discat; hj++)
                                {
                                    Label lblhead1id = (Label)gvCustomers.HeaderRow.FindControl("lblhead" + hj + "id");
                                    Image imgfree = (Image)itn.FindControl("imgfree" + hj);
                                    if (Convert.ToString(lblhead1id.Text) == Convert.ToString(dtr["PriceplancategoryId"]))
                                    {
                                        imgfree.ImageUrl = "images/Right.jpg";
                                        break;
                                    }
                                }


                            }
                        }

                    }
                    for (int mn = 1; mn <= discat; mn++)
                    {
                        int ccou = 0;
                        Image imgmain = (Image)e.Row.FindControl("imghead" + mn);
                        foreach (GridViewRow itn in gvOrders.Rows)
                        {
                            Image imgfree = (Image)itn.FindControl("imgfree" + mn);
                            if (imgfree.ImageUrl == "images/Right.jpg")
                            {
                                ccou += 1;
                            }

                        }
                        if (gvOrders.Rows.Count == ccou)
                        {
                            imgmain.ImageUrl = "images/Right.jpg";
                        }
                        else if (ccou == 0)
                        {
                            imgmain.ImageUrl = "images/Wrong.gif";
                        }
                        imgmain.Visible = true;
                    }
                }
            }



        }
    }
    protected void cellcolourchange(int gn)
    {
        foreach (GridViewRow item in gvCustomers.Rows)
        {


            for (int mn = 1; mn <= discat; mn++)
            {
                Label lbla = (Label)gvCustomers.Rows[item.RowIndex].FindControl("lbla" + mn);
                Button btndata = (Button)gvCustomers.Rows[item.RowIndex].FindControl("ddlhead" + mn);
                if (mn == gn)
                {
                    lbla.Enabled = true;
                    gvCustomers.Rows[item.RowIndex].Cells[mn+1].BackColor = System.Drawing.Color.Aquamarine;
                  
                }
                else
                {
                    lbla.Enabled = false;
                    gvCustomers.Rows[item.RowIndex].Cells[mn+1].BackColor = new System.Drawing.Color();
                }
            }
        }
    }

    protected void ddlhead1_Click(object sender, EventArgs e)
    {      
    }
    protected void ddlhead2_Click(object sender, EventArgs e)
    {      
    }
    protected void ddlhead3_Click(object sender, EventArgs e)
    {       
    }
    protected void ddlhead4_Click(object sender, EventArgs e)
    {       
    }
    protected void ddlhead5_Click(object sender, EventArgs e)
    {        
    }
    protected void ddlhead6_Click(object sender, EventArgs e)
    {       
    }
   


    protected void btnmoreinfo1_Click(object sender, EventArgs e)
    {              
    }
    protected void btnmoreinfo2_Click(object sender, EventArgs e)
    { 
    }
    protected void btnmoreinfo3_Click(object sender, EventArgs e)
    {             
    }
    protected void btnmoreinfo4_Click(object sender, EventArgs e)
    {              
    }
    protected void btnmoreinfo5_Click(object sender, EventArgs e)
    {   
    }
    protected void btnmoreinfo6_Click(object sender, EventArgs e)
    {   
    }
  
   
   
    protected void btnBuySelectSer1_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((Button)sender).Parent.Parent as GridViewRow;
        tempdata = "";
        int rinrow = row.RowIndex;
        selcol = "1";
        Label lblpid = (Label)gvCustomers.Rows[(rinrow - 1)].FindControl("lblpid" + (selcol));
       string serverppid = lblpid.Text;
        //   lblpopupPPID.Text = "8433";
       ServercheckboxAllow(serverppid);
    }
    protected void btnBuySelectSer2_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((Button)sender).Parent.Parent as GridViewRow;
        tempdata = "";
        int rinrow = row.RowIndex;
        selcol = "2";
        Label lblpid = (Label)gvCustomers.Rows[(rinrow - 1)].FindControl("lblpid" + (selcol));
        string serverppid = lblpid.Text;
        //   lblpopupPPID.Text = "8433";
        ServercheckboxAllow(serverppid);
    }
    protected void btnBuySelectSer3_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((Button)sender).Parent.Parent as GridViewRow;
        tempdata = "";
        int rinrow = row.RowIndex;
        selcol = "3";
        Label lblpid = (Label)gvCustomers.Rows[(rinrow - 1)].FindControl("lblpid" + (selcol));
        string serverppid = lblpid.Text;
        //   lblpopupPPID.Text = "8433";
        ServercheckboxAllow(serverppid);
    }
    protected void btnBuySelectSer4_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((Button)sender).Parent.Parent as GridViewRow;
        tempdata = "";
        int rinrow = row.RowIndex;
        selcol = "4";
        Label lblpid = (Label)gvCustomers.Rows[(rinrow - 1)].FindControl("lblpid" + (selcol));
        string serverppid = lblpid.Text;
        //   lblpopupPPID.Text = "8433";
        ServercheckboxAllow(serverppid);
    }
    protected void btnBuySelectSer5_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((Button)sender).Parent.Parent as GridViewRow;
        tempdata = "";
        int rinrow = row.RowIndex;
        selcol = "5";
        Label lblpid = (Label)gvCustomers.Rows[(rinrow - 1)].FindControl("lblpid" + (selcol));
        string serverppid = lblpid.Text;
        //   lblpopupPPID.Text = "8433";
        ServercheckboxAllow(serverppid);
    }
    protected void btnBuySelectSer6_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((Button)sender).Parent.Parent as GridViewRow;
        tempdata = "";
        int rinrow = row.RowIndex;
        selcol = "6";
        Label lblpid = (Label)gvCustomers.Rows[(rinrow - 1)].FindControl("lblpid" + (selcol));
        string serverppid = lblpid.Text;
        //   lblpopupPPID.Text = "8433";
        ServercheckboxAllow(serverppid);
    }
    protected void btnBuySelectSer7_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((Button)sender).Parent.Parent as GridViewRow;
        tempdata = "";
        int rinrow = row.RowIndex;
        selcol = "7";
        Label lblpid = (Label)gvCustomers.Rows[(rinrow - 1)].FindControl("lblpid" + (selcol));
        string serverppid = lblpid.Text;
        //   lblpopupPPID.Text = "8433";
        ServercheckboxAllow(serverppid);

    }
    protected void btnBuySelectSer8_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((Button)sender).Parent.Parent as GridViewRow;
        tempdata = "";
        int rinrow = row.RowIndex;
        selcol = "8";
        Label lblpid = (Label)gvCustomers.Rows[(rinrow - 1)].FindControl("lblpid" + (selcol));
        string serverppid = lblpid.Text;
        //   lblpopupPPID.Text = "8433";
        ServercheckboxAllow(serverppid);
    }



    public void ServercheckboxAllow(string serverppid)
    {

        DataTable dtcln = select(" Select  dbo.PortalMasterTbl.PortalName,dbo.PortalMasterTbl.ProductId , dbo.PricePlanMaster.PricePlanAmount FROM            dbo.PricePlanMaster INNER JOIN dbo.PortalMasterTbl ON dbo.PricePlanMaster.PortalMasterId1 = dbo.PortalMasterTbl.Id Where PricePlanId='" + serverppid + "'  ");//and Producthostclientserver='0'
         if (dtcln.Rows.Count > 0)
         {   //----------------------------------------------------------
             //Insertas a OrderMaster-----Main PP ID-------------------------------
             string portal = dtcln.Rows[0]["PortalName"].ToString();
             string strProductId = dtcln.Rows[0]["ProductId"].ToString();

             InsertOrderMasterDetail(Session["orderid"].ToString(), serverppid, dtcln.Rows[0]["PricePlanAmount"].ToString(),true,false,false);
             //-------------------------------------------------------------------------
                 

             DataTable dtservice = select("SELECT dbo.PriceplanCategorySubType.UniqueID, dbo.PriceplanCategorySubType.Name as CategorySubTypeName, dbo.PriceplanCategorySubType.Description FROM dbo.Priceplancategory INNER JOIN dbo.PriceplanCategoryType ON dbo.Priceplancategory.CategoryTypeID = dbo.PriceplanCategoryType.ID INNER JOIN dbo.PriceplanCategorySubType ON dbo.Priceplancategory.CategoryTypeSubID = dbo.PriceplanCategorySubType.UniqueID INNER JOIN dbo.PricePlanMaster ON dbo.Priceplancategory.ID = dbo.PricePlanMaster.PriceplancatId where PortalId='" + ddlportal.SelectedValue + "' and CategoryTypeID='14' order  by dbo.PriceplanCategorySubType.Name ");            
                 if (dtservice.Rows.Count > 0)
                 {
                     string link = "ServicePricePlanComparision.aspx?Id=" + strProductId + "&PN=" + portal + "&orderid=" + Session["orderid"].ToString() + "&PPid=" + Request.QueryString["PPid"] + "&SPid=" + serverppid + "&subTypeid=14&type=";//14=Srvice
                   
                   //  ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
                     Response.Redirect("" + link + ""); 
                 }
                 else
                 {  

                     string links = "Ordernow.aspx?orderid=" + Session["orderid"].ToString() + "&pid=" + Request.QueryString["PPid"] + "";//14=Srvice                                             
                     Response.Redirect("" + links + "");  
                 }
            
         }


    }
       
   
    ////---INSERT ORDER DETAIL MASTRER    

    protected void InsertOrderMasterDetail(string maxorderid, string priceplanid, string amt, Boolean IsHostingServer, Boolean IsServices, Boolean IsWebBasedApplications)
    {
        //------------------
        try
        {
            SqlCommand cmdsq = new SqlCommand("OrderMasterDetail_AddDelUpdtSelect", con);
            cmdsq.CommandType = CommandType.StoredProcedure;
            cmdsq.Parameters.AddWithValue("@StatementType", "Insert");
            cmdsq.Parameters.AddWithValue("@ordermasterid", maxorderid);
            cmdsq.Parameters.AddWithValue("@Priceplanid", priceplanid);
            cmdsq.Parameters.AddWithValue("@Amt", amt);
            cmdsq.Parameters.AddWithValue("@Date", DateTime.Now);
            cmdsq.Parameters.AddWithValue("@IsHostingServer", IsHostingServer);
            cmdsq.Parameters.AddWithValue("@IsServices", IsServices);
            cmdsq.Parameters.AddWithValue("@IsWebBasedApplications", IsWebBasedApplications);

            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdsq.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception ex)
        {
            con.Close();
        }

    }

}
