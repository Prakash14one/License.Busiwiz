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


     

        if (!IsPostBack)
        {
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
    protected void fillpriceplancate()
    {
        
        //if (gvCustomers.Rows.Count > 0)
        //{
        discat=0;
        gvCustomers.Columns[2].Visible = false;
        gvCustomers.Columns[3].Visible = false;
        gvCustomers.Columns[4].Visible = false;
        gvCustomers.Columns[5].Visible = false;
        gvCustomers.Columns[6].Visible = false;
        gvCustomers.Columns[7].Visible = false;
        gvCustomers.Columns[8].Visible = false;

        DataTable dtcln = select(" SELECT * FROM Priceplancategory where status=1 and  PortalId='" + ddlportal.SelectedValue + "' order  by Id ");
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
    protected void FillGrid()
    {
        gvCustomers.DataSource = null;
        gvCustomers.DataBind();
          DataTable dtclnv = select(" SELECT * FROM Priceplancategory where PortalId='"+ddlportal.SelectedValue+"' order  by Id ");
          if (dtclnv.Rows.Count > 0)
          {
              discat = dtclnv.Rows.Count;
              Cond = "0";
              DataTable dtTemp = new DataTable();
              dtTemp = CreateData();


              //        string strcln = "";

              strcln = "SELECT Distinct productfeature,Productfeaturetbl.Id FROM Productfeaturetbl inner join Priceplanfeatureproduct on Priceplanfeatureproduct.productfeatureid=Productfeaturetbl.Id  where portalid='"+ddlportal.SelectedValue+"' and  Productfeaturetbl.productversionid='" + VerId + "' ";

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
                  dtr2["Name"] = "Price";

                  dtTemp.Rows.Add(dtr2);
                  kb += 1;
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
                          dtr2["Name"] = "Customise Order";
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
    protected void Description_click(object sender, EventArgs e)
    {
        GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
        int rowindex = row.RowIndex;
        Response.Redirect("ViewPricePlan.aspx?id12='" + gvCustomers.Rows[rowindex].Cells[0].Text + "'");

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string pid;
        //   strcln = "SELECT PricePlanMaster.PricePlanId AS Expr2,PricePlanMaster.PricePlanName,PricePlanMaster.PricePlanDesc,PricePlanMaster.active,PricePlanMaster.StartDate,PricePlanMaster.EndDate,PricePlanMaster.PricePlanAmount,PricePlanMaster.ProductId,PricePlanMaster.DurationMonth,PricePlanMaster.AllowIPTrack,PricePlanMaster.GBUsage,PricePlanMaster.MaxUser,PricePlanMaster.TrafficinGB,PricePlanMaster.TotalMail,CASE WHEN PricePlanMaster.AllowIPTrack IS NULL THEN 'No' ELSE 'Yes' END AS GBUSage1, ProductMaster.ProductId AS Expr1,  ProductMaster.ClientMasterId, ProductMaster.ProductName, ProductMaster.ProductURL, ProductMaster.PricePlanURL,Versioninfomaster.versioninfoname, SetupMaster.*  FROM PricePlanMaster LEFT OUTER JOIN SetupMaster ON PricePlanMaster.PricePlanId = SetupMaster.PricePlanId LEFT OUTER JOIN VersionInfoMaster on VersionInfoMaster.VersionInfoId=PricePlanMaster.VersionInfoMasterId LEFT OUTER JOIN  ProductMaster ON PricePlanMaster.ProductId = ProductMaster.ProductId  where   ProductMaster.ProductId='12' and SetupMaster.ProductSetup is not null";
        strcln = "select ProductId from PricePlanMaster";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        lblproductid.Text = dtcln.Rows[0]["ProductId"].ToString();
        Session["pid"] = lblproductid.Text;
        Response.Write("<script>window.open('Viewdescription.aspx','_blank');</script>");
        //  Response.Redirect("Viewdescription.aspx");
    }
    protected void LinkButton11_Click(object sender, EventArgs e)
    {

        GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;

        int rinrow = row.RowIndex;
        Label ctrl = (Label)gvCustomers.Rows[rinrow].FindControl("Labellink1");
        Response.Write("<script>window.open('Viewdescription.aspx?id=" + ctrl.Text + "','_blank');</script>");
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
                    DataTable dtcln = select("Select Distinct Top(1) PricePlanMaster.PricePlanId, PricePlanMaster.PricePlanAmount,PricePlanMaster.PricePlanId,Plancatedefault from PriceplanrestrictionTbl inner join Priceplanrestrecordallowtbl on Priceplanrestrecordallowtbl.PriceplanRestrictiontblId=PriceplanrestrictionTbl.Id inner join PricePlanMaster on PricePlanMaster.PricePlanId=Priceplanrestrecordallowtbl.PricePlanId where  PriceplancatId='" + lblhead1id.Text + "' and  PricePlanMaster.VersionInfoMasterId='" + ddlproduct.SelectedValue + "' and  PriceplanrestrictionTbl.portalid='" + ddlportal.SelectedValue + "' order by Plancatedefault Desc");

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
                    btnBuySelectSer.Visible = true;  

                    Label lbla1 = (Label)e.Row.FindControl("lbla" + i);
                    lbla1.CssClass = "BTNTorder";
                    HR1.Visible = false;
                    lbla1.Visible = true;
                    HR1.HRef = "";

                    DataTable dtcln = select("Select Distinct Top(1) PricePlanMaster.PricePlanAmount,PricePlanMaster.PricePlanId,Plancatedefault from PriceplanrestrictionTbl inner join Priceplanrestrecordallowtbl on Priceplanrestrecordallowtbl.PriceplanRestrictiontblId=PriceplanrestrictionTbl.Id inner join PricePlanMaster on PricePlanMaster.PricePlanId=Priceplanrestrecordallowtbl.PricePlanId where PriceplancatId='" + lblhead1id.Text + "' and  PricePlanMaster.VersionInfoMasterId='" + ddlproduct.SelectedValue + "' and  PriceplanrestrictionTbl.portalid='" + ddlportal.SelectedValue + "' order by Plancatedefault Desc");
                    if (dtcln.Rows.Count > 0)
                    {

                        HR1.HRef = "Ordernow.aspx?Pid=" + Convert.ToString(dtcln.Rows[0]["PricePlanId"]) + "";
                        HR1.Name = Convert.ToString(dtcln.Rows[0]["PricePlanId"]);
                        HR11.Name = Convert.ToString(dtcln.Rows[0]["PricePlanId"]);


                    }

                }
            }
            else if (lblfeid.Text == "f2")
            {
                lblcb.Text = "";
                for (int i = 1; i <= discat; i++)
                {
                    Button ddlhead1 = (Button)e.Row.FindControl("ddlhead" + i);
                    ddlhead1.Visible = true;

                    Button Button1 = (Button)e.Row.FindControl("Button" + i);
                    Button1.Visible = true;
                }
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
       //  pnlData.Visible = false;
       // Label lblhead1id = (Label)gvCustomers.HeaderRow.FindControl("lblhead1id");
       // catid = lblhead1id.Text;
       // //for (int mn = 1; mn <= discat; mn++)
       // //{
       //     //Label lbla = (Label)gvCustomers.Rows[rinrow - 1].FindControl("lbla" + mn);
       //     //Button btndata = (Button)gvCustomers.Rows[rinrow].FindControl("ddlhead" + mn);
       //     //if (mn == 1)
       //     //{
       //     //    lbla.Enabled = true;
       //     //    btndata.BackColor = System.Drawing.Color.Aquamarine;
       //     //}
       //     //else
       //     //{
       //     //    lbla.Enabled = false;
       //     //    btndata.BackColor = new System.Drawing.Color();
       //     //}
       //// }
       // cellcolourchange(1);
       // Panel pnlA = gvCustomers.Rows[gvCustomers.Rows.Count - 1].FindControl("pnlA") as Panel;

       // Label lblcb = gvCustomers.Rows[gvCustomers.Rows.Count - 1].FindControl("lblcb") as Label;
       // pnlA.Visible = false;
       // lblcb.Visible = false;
       // selcol = "1";
       // filldefpanel();

       // fillddldata(rinrow); ;

        GridViewRow row = ((Button)sender).Parent.Parent as GridViewRow;
        tempdata = "";
        int rinrow = row.RowIndex;
        pnlcustomise.Visible = true;
        
        pnlData.Visible = false;
        Label lblhead1id = (Label)gvCustomers.HeaderRow.FindControl("lblhead1id");
        catid = lblhead1id.Text;
        //for (int mn = 1; mn <= discat; mn++)
        //{
        //Label lbla = (Label)gvCustomers.Rows[rinrow - 1].FindControl("lbla" + mn);
        //Button btndata = (Button)gvCustomers.Rows[rinrow].FindControl("ddlhead" + mn);
        //if (mn == 1)
        //{
        //    lbla.Enabled = true;
        //    btndata.BackColor = System.Drawing.Color.Aquamarine;
        //}
        //else
        //{
        //    lbla.Enabled = false;
        //    btndata.BackColor = new System.Drawing.Color();
        //}
        // }
        cellcolourchange(1);
        Panel pnlA = gvCustomers.Rows[gvCustomers.Rows.Count - 1].FindControl("pnlA") as Panel;

        Label lblcb = gvCustomers.Rows[gvCustomers.Rows.Count - 1].FindControl("lblcb") as Label;
        pnlA.Visible = false;
        lblcb.Visible = false;
        selcol = "1";
        filldefpanel();

        fillddldata(rinrow); ; 
    }

    protected void ddlhead2_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((Button)sender).Parent.Parent as GridViewRow;
        tempdata = "";
        int rinrow = row.RowIndex;
        pnlcustomise.Visible = true;
        pnlData.Visible = false;
        Label lblhead2id = (Label)gvCustomers.HeaderRow.FindControl("lblhead2id");
        catid = lblhead2id.Text;
        //for (int mn = 1; mn <= discat; mn++)
        //{
        //    Label lbla = (Label)gvCustomers.Rows[rinrow - 1].FindControl("lbla" + mn);
        //    Button btndata = (Button)gvCustomers.Rows[rinrow].FindControl("ddlhead" + mn);
        //    if (mn == 2)
        //    {
        //        lbla.Enabled = true;
        //        btndata.BackColor = System.Drawing.Color.Aquamarine;
        //    }
        //    else
        //    {
        //        lbla.Enabled = false;
        //        btndata.BackColor = new System.Drawing.Color();
        //    }
        //}
        cellcolourchange(2);
        Panel pnlA = gvCustomers.Rows[gvCustomers.Rows.Count - 1].FindControl("pnlA") as Panel;
    
        Label lblcb = gvCustomers.Rows[gvCustomers.Rows.Count - 1].FindControl("lblcb") as Label;
        pnlA.Visible = false;
        lblcb.Visible = false;
        selcol = "2";
        filldefpanel();

        fillddldata(rinrow);;
    }
    protected void ddlhead3_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((Button)sender).Parent.Parent as GridViewRow;
        tempdata = "";
        int rinrow = row.RowIndex;
        pnlcustomise.Visible = true;
        pnlData.Visible = false;
        Label lblhead3id = (Label)gvCustomers.HeaderRow.FindControl("lblhead3id");
        catid = lblhead3id.Text;
        //for (int mn = 1; mn <= discat; mn++)
        //{
        //    Label lbla = (Label)gvCustomers.Rows[rinrow - 1].FindControl("lbla" + mn);
        //    Button btndata = (Button)gvCustomers.Rows[rinrow].FindControl("ddlhead" + mn);
        //    if (mn == 3)
        //    {
        //        lbla.Enabled = true;
        //        btndata.BackColor = System.Drawing.Color.Aquamarine;
        //    }
        //    else
        //    {
        //        lbla.Enabled = false;
        //        btndata.BackColor = new System.Drawing.Color();
        //    }
        //}
        cellcolourchange(3);
        Panel pnlA = gvCustomers.Rows[gvCustomers.Rows.Count - 1].FindControl("pnlA") as Panel;

        Label lblcb = gvCustomers.Rows[gvCustomers.Rows.Count - 1].FindControl("lblcb") as Label;
        pnlA.Visible = false;
        lblcb.Visible = false;
        selcol = "3";
        filldefpanel();

        fillddldata(rinrow);;
    }

    protected void ddlhead4_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((Button)sender).Parent.Parent as GridViewRow;
        tempdata = "";
        int rinrow = row.RowIndex;
        pnlcustomise.Visible = true;
        pnlData.Visible = false;
        Label lblhead4id = (Label)gvCustomers.HeaderRow.FindControl("lblhead4id");
        catid = lblhead4id.Text;
        //for (int mn = 1; mn <= discat; mn++)
        //{
        //    Label lbla = (Label)gvCustomers.Rows[rinrow - 1].FindControl("lbla" + mn);
        //    Button btndata = (Button)gvCustomers.Rows[rinrow].FindControl("ddlhead" + mn);
        //    if (mn == 4)
        //    {
        //        lbla.Enabled = true;
        //        btndata.BackColor = System.Drawing.Color.Aquamarine;
        //    }
        //    else
        //    {
        //        lbla.Enabled = false;
        //        btndata.BackColor = new System.Drawing.Color();
        //    }
        //}
        cellcolourchange(4);
        selcol = "4";
        filldefpanel();
        Panel pnlA = gvCustomers.Rows[gvCustomers.Rows.Count - 1].FindControl("pnlA") as Panel;

        Label lblcb = gvCustomers.Rows[gvCustomers.Rows.Count - 1].FindControl("lblcb") as Label;
        pnlA.Visible = false;
        lblcb.Visible = false;
        fillddldata(rinrow);;
    }

    protected void ddlhead5_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((Button)sender).Parent.Parent as GridViewRow;
        tempdata = "";
        int rinrow = row.RowIndex;
        pnlcustomise.Visible = true;
        pnlData.Visible = false;
        Label lblhead5id = (Label)gvCustomers.HeaderRow.FindControl("lblhead5id");
        catid = lblhead5id.Text;
        //for (int mn = 1; mn <= discat; mn++)
        //{
        //    Label lbla = (Label)gvCustomers.Rows[rinrow - 1].FindControl("lbla" + mn);
        //    Button btndata = (Button)gvCustomers.Rows[rinrow].FindControl("ddlhead" + mn);
        //    if (mn == 5)
        //    {
        //        lbla.Enabled = true;
        //        btndata.BackColor = System.Drawing.Color.Aquamarine;
        //    }
        //    else
        //    {
        //        lbla.Enabled = false;
        //        btndata.BackColor = new System.Drawing.Color();
        //    }
        //}
        cellcolourchange(5);
        selcol = "5";
        filldefpanel();
        Panel pnlA = gvCustomers.Rows[gvCustomers.Rows.Count - 1].FindControl("pnlA") as Panel;

        Label lblcb = gvCustomers.Rows[gvCustomers.Rows.Count - 1].FindControl("lblcb") as Label;
        pnlA.Visible = false;
        lblcb.Visible = false;
        fillddldata(rinrow);;
    }
    protected void ddlhead6_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((Button)sender).Parent.Parent as GridViewRow;
        tempdata = "";
        int rinrow = row.RowIndex;
        pnlcustomise.Visible = true;
        pnlData.Visible = false;
        Label lblhead6id = (Label)gvCustomers.HeaderRow.FindControl("lblhead6id");
        catid = lblhead6id.Text;
        //for (int mn = 1; mn <= discat; mn++)
        //{
        //    Label lbla = (Label)gvCustomers.Rows[rinrow - 1].FindControl("lbla" + mn);
        //    Button btndata = (Button)gvCustomers.Rows[rinrow].FindControl("ddlhead" + mn);
        //    if (mn == 6)
        //    {
        //        lbla.Enabled = true;
        //        btndata.BackColor = System.Drawing.Color.Aquamarine;
        //    }
        //    else
        //    {
        //        lbla.Enabled = false;
        //        btndata.BackColor = new System.Drawing.Color();
        //    }
        //}
        cellcolourchange(6);
        Panel pnlA = gvCustomers.Rows[gvCustomers.Rows.Count - 1].FindControl("pnlA") as Panel;

        Label lblcb = gvCustomers.Rows[gvCustomers.Rows.Count - 1].FindControl("lblcb") as Label;
        pnlA.Visible = false;
        lblcb.Visible = false;
        selcol = "6";
        filldefpanel();

        fillddldata(rinrow);;
    }
    protected void ddlhead7_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((Button)sender).Parent.Parent as GridViewRow;
        tempdata = "";
        int rinrow = row.RowIndex;
        pnlcustomise.Visible = true;
        pnlData.Visible = false;
        Label lblhead7id = (Label)gvCustomers.HeaderRow.FindControl("lblhead7id");
        catid = lblhead7id.Text;
        //for (int mn = 1; mn <= discat; mn++)
        //{
        //    Label lbla = (Label)gvCustomers.Rows[rinrow - 1].FindControl("lbla" + mn);
        //    Button btndata = (Button)gvCustomers.Rows[rinrow].FindControl("ddlhead" + mn);
        //    if (mn == 7)
        //    {
        //        lbla.Enabled = true;
        //        btndata.BackColor = System.Drawing.Color.Aquamarine;
        //    }
        //    else
        //    {
        //        lbla.Enabled = false;
        //        btndata.BackColor = new System.Drawing.Color();
        //    }
        //}
        cellcolourchange(7);
        Panel pnlA = gvCustomers.Rows[gvCustomers.Rows.Count - 1].FindControl("pnlA") as Panel;

        Label lblcb = gvCustomers.Rows[gvCustomers.Rows.Count - 1].FindControl("lblcb") as Label;
        pnlA.Visible = false;
        lblcb.Visible = false;
        selcol = "7";
        filldefpanel();

        fillddldata(rinrow);
    }


    protected void btnmoreinfo1_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((Button)sender).Parent.Parent as GridViewRow;
        tempdata = "";
        int rinrow = row.RowIndex;
        pnlcustomise.Visible = false;

        pnlData.Visible = false;
        Label lblhead1id = (Label)gvCustomers.HeaderRow.FindControl("lblhead1id");
        catid = lblhead1id.Text;
       
        cellcolourchange(1);
        Panel pnlA = gvCustomers.Rows[gvCustomers.Rows.Count - 1].FindControl("pnlA") as Panel;

        Label lblcb = gvCustomers.Rows[gvCustomers.Rows.Count - 1].FindControl("lblcb") as Label;
        pnlA.Visible = false;
        lblcb.Visible = false;
        selcol = "1";
        filldefpanel();

        fillddldataPopup(rinrow); ; 

        pnlpopup.Visible = true;
    }
    protected void btnmoreinfo2_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((Button)sender).Parent.Parent as GridViewRow;
        tempdata = "";
        int rinrow = row.RowIndex;
        pnlcustomise.Visible = false;
        pnlData.Visible = false;
        Label lblhead1id = (Label)gvCustomers.HeaderRow.FindControl("lblhead2id");
        catid = lblhead1id.Text;
        cellcolourchange(2);
        Panel pnlA = gvCustomers.Rows[gvCustomers.Rows.Count - 1].FindControl("pnlA") as Panel;
        Label lblcb = gvCustomers.Rows[gvCustomers.Rows.Count - 1].FindControl("lblcb") as Label;
        pnlA.Visible = false;
        lblcb.Visible = false;
        selcol = "2";
        filldefpanel();
        fillddldataPopup(rinrow); ; 
        pnlpopup.Visible = true;

       
       
      
      
    }
    protected void btnmoreinfo3_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((Button)sender).Parent.Parent as GridViewRow;
        tempdata = "";
        int rinrow = row.RowIndex;
        pnlcustomise.Visible = false;

        pnlData.Visible = false;
        Label lblhead1id = (Label)gvCustomers.HeaderRow.FindControl("lblhead3id");
        catid = lblhead1id.Text;

        cellcolourchange(3);
        Panel pnlA = gvCustomers.Rows[gvCustomers.Rows.Count - 1].FindControl("pnlA") as Panel;

        Label lblcb = gvCustomers.Rows[gvCustomers.Rows.Count - 1].FindControl("lblcb") as Label;
        pnlA.Visible = false;
        lblcb.Visible = false;
        selcol = "3";
        filldefpanel();

        fillddldataPopup(rinrow); ; 
        pnlpopup.Visible = true;
    }
    protected void btnmoreinfo4_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((Button)sender).Parent.Parent as GridViewRow;
        tempdata = "";
        int rinrow = row.RowIndex;
        pnlcustomise.Visible = false;

        pnlData.Visible = false;
        Label lblhead1id = (Label)gvCustomers.HeaderRow.FindControl("lblhead4id");
        catid = lblhead1id.Text;

        cellcolourchange(4);
        Panel pnlA = gvCustomers.Rows[gvCustomers.Rows.Count - 1].FindControl("pnlA") as Panel;

        Label lblcb = gvCustomers.Rows[gvCustomers.Rows.Count - 1].FindControl("lblcb") as Label;
        pnlA.Visible = false;
        lblcb.Visible = false;
        selcol = "4";
        filldefpanel();

        fillddldataPopup(rinrow); ; 
        pnlpopup.Visible = true;
    }
    protected void btnmoreinfo5_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((Button)sender).Parent.Parent as GridViewRow;
        tempdata = "";
        int rinrow = row.RowIndex;
        pnlcustomise.Visible = false;

        pnlData.Visible = false;
        Label lblhead1id = (Label)gvCustomers.HeaderRow.FindControl("lblhead5id");
        catid = lblhead1id.Text;

        cellcolourchange(5);
        Panel pnlA = gvCustomers.Rows[gvCustomers.Rows.Count - 1].FindControl("pnlA") as Panel;

        Label lblcb = gvCustomers.Rows[gvCustomers.Rows.Count - 1].FindControl("lblcb") as Label;
        pnlA.Visible = false;
        lblcb.Visible = false;
        selcol = "5";
        filldefpanel();

        fillddldataPopup(rinrow); ; 
        pnlpopup.Visible = true;
    }
    protected void btnmoreinfo6_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((Button)sender).Parent.Parent as GridViewRow;
        tempdata = "";
        int rinrow = row.RowIndex;
        pnlcustomise.Visible = false;

        pnlData.Visible = false;
        Label lblhead1id = (Label)gvCustomers.HeaderRow.FindControl("lblhead6id");
        catid = lblhead1id.Text;

        cellcolourchange(6);
        Panel pnlA = gvCustomers.Rows[gvCustomers.Rows.Count - 1].FindControl("pnlA") as Panel;

        Label lblcb = gvCustomers.Rows[gvCustomers.Rows.Count - 1].FindControl("lblcb") as Label;
        pnlA.Visible = false;
        lblcb.Visible = false;
        selcol = "6";
        filldefpanel();

        fillddldataPopup(rinrow); ; 
        pnlpopup.Visible = true;
    }
    protected void btnmoreinfo7_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((Button)sender).Parent.Parent as GridViewRow;
        tempdata = "";
        int rinrow = row.RowIndex;
        pnlcustomise.Visible = false;

        pnlData.Visible = false;
        Label lblhead1id = (Label)gvCustomers.HeaderRow.FindControl("lblhead7id");
        catid = lblhead1id.Text;

        cellcolourchange(7);
        Panel pnlA = gvCustomers.Rows[gvCustomers.Rows.Count - 1].FindControl("pnlA") as Panel;

        Label lblcb = gvCustomers.Rows[gvCustomers.Rows.Count - 1].FindControl("lblcb") as Label;
        pnlA.Visible = false;
        lblcb.Visible = false;
        selcol = "7";
        filldefpanel();

        fillddldataPopup(rinrow); ; 
        pnlpopup.Visible = true;
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
    protected void filldefpanel()
    {
        //DataTable dtcln = select("Select Distinct RecordsAllowed from PriceplanrestrictionTbl inner join Priceplanrestrecordallowtbl on Priceplanrestrecordallowtbl.PriceplanRestrictiontblId=PriceplanrestrictionTbl.Id inner join PricePlanMaster on PricePlanMaster.PricePlanId=Priceplanrestrecordallowtbl.PricePlanId where PriceplancatId='" + catid + "' and  PricePlanMaster.VersionInfoMasterId='" + ddlproduct.SelectedValue + "' and  PriceplanrestrictionTbl.portalid='" + ddlportal.SelectedValue + "'");
        //ddlrecords.DataSource = dtcln;
        //ddlrecords.DataTextField = "RecordsAllowed";
        //ddlrecords.DataValueField = "RecordsAllowed";
        //ddlrecords.DataBind();
        // stepover();
    }
    protected void fillddldata(int rimin)
    {
        HtmlAnchor HR1 = (HtmlAnchor)gvCustomers.Rows[(rimin-1)].FindControl("HR" + (selcol));
        datalp1.DataSource = null;
        datalp1.DataBind();
        DataTable dtcln = select(" Select Distinct PriceplanrestrictionTbl.Id, NameofRest+'\n - '+Priceplanrestrecordallowtbl.RecordsAllowed as NameofRest,TextofQueinSelection,Priceaddingroup,Restingroup ,PriceplancatId from PriceplanrestrictionTbl left join Priceplanrestrecordallowtbl on Priceplanrestrecordallowtbl.PriceplanRestrictiontblId=PriceplanrestrictionTbl.Id and Priceplanrestrecordallowtbl.PricePlanId='" + HR1.Name + "' inner join PricePlanMaster on PricePlanMaster.PricePlanId=Priceplanrestrecordallowtbl.PricePlanId where  PricePlanMaster.VersionInfoMasterId='" + ddlproduct.SelectedValue + "' and  PriceplanrestrictionTbl.portalid='" + ddlportal.SelectedValue + "' and PriceplancatId='" + catid + "' Order by PriceplanrestrictionTbl.Id ASC ");
        datalp1.DataSource = dtcln;
        datalp1.DataBind();
    }
    protected void fillddldataPopup(int rimin)
    {
        HtmlAnchor HR1 = (HtmlAnchor)gvCustomers.Rows[(rimin - 1)].FindControl("HR" + (selcol));
        gvpopup.DataSource = null;
        gvpopup.DataBind();
        DataTable dtcln = select(" Select Distinct PriceplanrestrictionTbl.Id, NameofRest +'\n Maximum up to '+ Priceplanrestrecordallowtbl.RecordsAllowed as NameofRest,Priceplanrestrecordallowtbl.RecordsAllowed  ,NameofRest as NameofRest1, TextofQueinSelection,Priceaddingroup,Restingroup,  PriceplanrestrictionTbl.portalid ,PriceplanrestrictionTbl.id from PriceplanrestrictionTbl left join Priceplanrestrecordallowtbl on Priceplanrestrecordallowtbl.PriceplanRestrictiontblId=PriceplanrestrictionTbl.Id and Priceplanrestrecordallowtbl.PricePlanId='" + HR1.Name + "' inner join PricePlanMaster on PricePlanMaster.PricePlanId=Priceplanrestrecordallowtbl.PricePlanId where  PricePlanMaster.VersionInfoMasterId='" + ddlproduct.SelectedValue + "' and  PriceplanrestrictionTbl.portalid='" + ddlportal.SelectedValue + "' and PriceplancatId='" + catid + "' Order by PriceplanrestrictionTbl.Id ASC ");
        gvpopup.DataSource = dtcln;
        gvpopup.DataBind();
        gvpopup.Visible = true;
        gvallrest.Visible = false; 
    }
    protected void lblworkho_Click1(object sender, EventArgs e)
    {
        DataListItem iten = ((Button)sender).Parent.Parent as DataListItem;

        int rinrow = iten.ItemIndex;
        fillgridlist(rinrow);
        // btndata.Enabled = false;
    }
  
    protected void ddlportal_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlData.Visible = false;
        pnlcustomise.Visible = false;
        btnnext.Visible = true;
        btngo.Visible = false;
        FillGrid();
        fillpriceplancate();
      

    }
    protected void btnnext_Click(object sender, EventArgs e)
    {
        if (datalp1.Items.Count > 0)
        {
            Label lblnorec = (Label)datalp1.Items[btnstep].FindControl("lblnorec");
            lblnorec.Text = ddlrecords.SelectedItem.Text;
            btnstep += 1;
            if ((datalp1.Items.Count - 1) != btnstep)
            {
                btnnext.Visible = true;
                btngo.Visible = false;
            }
            else
            {
                btnnext.Visible = false;
                btngo.Visible = true;
            }
           // stepover();
            fillgridlist(btnstep);

        }
    }
    protected void stepover()
    {
        string strclnid = "";
        foreach (DataListItem item in datalp1.Items)
        {
            string RestId = datalp1.DataKeys[item.ItemIndex].ToString();
            Label lblnorec = (Label)item.FindControl("lblnorec");
            Button btndata = (Button)item.FindControl("btndata");
            //if (lblnorec.Text.Length > 0)
            //{

            //}
            if (btndata.BackColor == System.Drawing.ColorTranslator.FromHtml("#298AA8"))
            {
                btndata.BackColor = System.Drawing.ColorTranslator.FromHtml("#415241");
                //btndata.BackColor = System.Drawing.Color.Aquamarine;
            }
            else
            {
               
            }
            if (btnstep == item.ItemIndex)
            {
                btndata.BackColor = System.Drawing.ColorTranslator.FromHtml("#298AA8");
                //btndata.BackColor = System.Drawing.Color.Red;
                strclnid = strclnid + "  and (PriceplanrestrictionTbl.Id='" + RestId + "')";

            }

        }
        if (tempdata != "")
        {
            strclnid = strclnid + "  and (Priceplanrestrecordallowtbl.PricePlanId in(" + tempdata + "))";
        }
        ddlrecords.Items.Clear();

        DataTable dtcln = select(" Select Distinct RecordsAllowed from PriceplanrestrictionTbl inner join Priceplanrestrecordallowtbl on Priceplanrestrecordallowtbl.PriceplanRestrictiontblId=PriceplanrestrictionTbl.Id inner join PricePlanMaster on PricePlanMaster.PricePlanId=Priceplanrestrecordallowtbl.PricePlanId where PriceplancatId='" + catid + "' and  PricePlanMaster.VersionInfoMasterId='" + ddlproduct.SelectedValue + "' and  PriceplanrestrictionTbl.portalid='" + ddlportal.SelectedValue + "'" + strclnid);
        ddlrecords.DataSource = dtcln;
        ddlrecords.DataTextField = "RecordsAllowed";
        ddlrecords.DataValueField = "RecordsAllowed";
        ddlrecords.DataBind();
        tempdata = "";
        if (ddlrecords.Items.Count > 0)
        {
            DataTable dtc = select("Select Distinct PricePlanMaster.PricePlanId from PriceplanrestrictionTbl inner join Priceplanrestrecordallowtbl on Priceplanrestrecordallowtbl.PriceplanRestrictiontblId=PriceplanrestrictionTbl.Id inner join PricePlanMaster on PricePlanMaster.PricePlanId=Priceplanrestrecordallowtbl.PricePlanId where PriceplancatId='" + catid + "' and  PricePlanMaster.VersionInfoMasterId='" + ddlproduct.SelectedValue + "' and  PriceplanrestrictionTbl.portalid='" + ddlportal.SelectedValue + "'" + strclnid);
            foreach (DataRow item in dtc.Rows)
            {
                if (tempdata == "")
                {
                    tempdata = "'" + item["PricePlanId"] + "'";
                }
                else
                {
                    tempdata = tempdata + ",'" + item["PricePlanId"] + "'";
                }
            }
        }
    }
    protected void fillgridlist(int rinrow)
    {
        btnstep = rinrow;
        string restid = datalp1.DataKeys[rinrow].ToString();
        Button btndata = (Button)datalp1.Items[rinrow].FindControl("btndata");
        pnlData.Visible = true;
        ddlrecords.Visible = true;


        stepover();
        DataTable dtcln = select("Select PriceplanrestrictionTbl.Id,NameofRest,TextofQueinSelection,Priceaddingroup,Restingroup from PriceplanrestrictionTbl where PriceplanrestrictionTbl.Id='" + restid + "'");
        if (dtcln.Rows.Count > 0)
        {
            lbltextmsg.Text = Convert.ToString(dtcln.Rows[0]["TextofQueinSelection"]);
        }

        //rinrow += 1;
        if ((datalp1.Items.Count - 1) != rinrow)
        {
            btnnext.Visible = true;
            btngo.Visible = false;
        }
        else
        {
            btnnext.Visible = false;
            btngo.Visible = true;
        }
        //btndata.BackColor = System.Drawing.Color.Aquamarine;
    }
    protected void btngo_Click(object sender, EventArgs e)
    {
        if (ddlrecords.Items.Count > 0)
        {
            pnlData.Visible = false;
            pnlcustomise.Visible = false;
            btnnext.Visible = true;
            btngo.Visible = false;
            string RestId = datalp1.DataKeys[btnstep].ToString();
            Label lblnorec = (Label)datalp1.Items[btnstep].FindControl("lblnorec");
            Button btndata = (Button)datalp1.Items[btnstep].FindControl("btndata");
            lblnorec.Text = ddlrecords.SelectedItem.Text;
            GridView grdnoof = gvCustomers.Rows[gvCustomers.Rows.Count - 1].FindControl("grdnoof") as GridView;
            Panel pnlA = gvCustomers.Rows[gvCustomers.Rows.Count - 1].FindControl("pnlA") as Panel;
            Label lblfeid = gvCustomers.Rows[gvCustomers.Rows.Count - 1].FindControl("lblfeid") as Label;
            Label lblcb = gvCustomers.Rows[gvCustomers.Rows.Count - 1].FindControl("lblcb") as Label;
            pnlA.Visible = true;
            lblcb.Visible = true;

            grdnoof.Columns[1].Visible = false;
            grdnoof.Columns[2].Visible = false;
            grdnoof.Columns[3].Visible = false;
            grdnoof.Columns[4].Visible = false;
            grdnoof.Columns[5].Visible = false;
            grdnoof.Columns[6].Visible = false;
            grdnoof.Columns[7].Visible = false;
           
            for (int mn = 1; mn <= discat; mn++)
            {
                grdnoof.Columns[mn].Visible = true;
            }
            DataTable dtcln = select("Select Distinct PriceplanrestrictionTbl.Id,NameofRest,TextofQueinSelection,Priceaddingroup,Restingroup from PriceplanrestrictionTbl inner join Priceplanrestrecordallowtbl on Priceplanrestrecordallowtbl.PriceplanRestrictiontblId=PriceplanrestrictionTbl.Id inner join PricePlanMaster on PricePlanMaster.PricePlanId=Priceplanrestrecordallowtbl.PricePlanId where PricePlanMaster.VersionInfoMasterId='" + ddlproduct.SelectedValue + "' and  PriceplanrestrictionTbl.portalid='" + ddlportal.SelectedValue + "' and PriceplancatId='" + catid + "'" + "  Order by PriceplanrestrictionTbl.Id ASC");
            grdnoof.DataSource = dtcln;
            grdnoof.DataBind();

            foreach (GridViewRow item in grdnoof.Rows)
            {
                Label lbl = (Label)item.FindControl("lbl" + selcol);
                Label lblnorec1 = (Label)datalp1.Items[item.RowIndex].FindControl("lblnorec");
                lbl.Text = lblnorec1.Text;
            }

            if (tempdata != "")
            {
                DataTable dtx = select("select * from PricePlanMaster where PricePlanId In (" + tempdata + ")");
                if (dtx.Rows.Count > 0)
                {
                    foreach (GridViewRow ite in gvCustomers.Rows)
                    {
                        HtmlAnchor HR1 = (HtmlAnchor)ite.FindControl("HR" +selcol);
                        Label lbla1 = (Label)ite.FindControl("lbla" + selcol);
                        Label lblfe = ite.FindControl("lblfeid") as Label;

                        Label lblpid = ite.FindControl("lblpid"+selcol) as Label;
                        

                        if (lblfe.Text == "f1")
                        {
                            HR1.Visible = false;
                           
                            lbla1.Visible = true;
                            HR1.HRef = "";
                            //lbla1.Text = Convert.ToString(dtx.Rows[0]["PricePlanId"]);
                            HR1.HRef = "Ordernow.aspx?Pid=" + Convert.ToString(dtx.Rows[0]["PricePlanId"]) + "";
                            HR1.Name = Convert.ToString(dtx.Rows[0]["PricePlanId"]);
                            lblpid.Text = Convert.ToString(dtx.Rows[0]["PricePlanId"]);
                        }
                        else if (lblfe.Text == "P1")
                        {
                            lbla1.Text = Convert.ToString(dtx.Rows[0]["PricePlanAmount"]);
                        }
                        else if (lblfe.Text == "P2")
                        {
                            lbla1.Text = Convert.ToString(dtx.Rows[0]["DurationMonth"]);
                        }
                    }
                }
            }
        }

    }

    protected void datalp1databinding(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item)
        {
            Label lblpricecatid = (Label)e.Item.FindControl("lblpricecatid");
            DataTable dtcln = select(" Select Distinct RecordsAllowed from PriceplanrestrictionTbl inner join Priceplanrestrecordallowtbl on Priceplanrestrecordallowtbl.PriceplanRestrictiontblId=PriceplanrestrictionTbl.Id inner join PricePlanMaster on PricePlanMaster.PricePlanId=Priceplanrestrecordallowtbl.PricePlanId where PriceplancatId='" + lblpricecatid.Text + "' and  PricePlanMaster.VersionInfoMasterId='" + ddlproduct.SelectedValue + "' ");
        
            
        }
    }

    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        pnlpopup.Visible = false;
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        gvpopup.Visible = true;
        gvallrest.Visible = false;
        ImageButton1.Visible = false;
    }


   

    protected void gvpopup_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "GetRow")
        {
            GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
            //Here i am assuming you are having label inside your gridview
            Label lbl = (Label)row.FindControl("lblid");
            Label Label1 = (Label)row.FindControl("Label1");
            Label lblcid = (Label)row.FindControl("lblcid");
            
            lbl.Text = lbl.Text;
            gvpopup.Visible = false;
            gvallrest.Visible = true;
            gvallrest.DataSource = null;
            gvallrest.DataBind();
           // DataTable dtcln = select(" SELECT DISTINCT TOP (100) PERCENT dbo.PricePlanMaster.PricePlanName, CONVERT(int, dbo.Priceplanrestrecordallowtbl.RecordsAllowed) AS RecordsAllowed  ,dbo.PricePlanMaster.PricePlanId as pid, dbo.PricePlanMaster.PricePlanAmount, dbo.PriceplanrestrictionTbl.Id, dbo.Priceplancategory.ID AS cid,  dbo.PriceplanrestrictionTbl.NameofRest, dbo.PriceplanrestrictionTbl.TextofQueinSelection, dbo.PriceplanrestrictionTbl.Restingroup, dbo.PriceplanrestrictionTbl.PortalId, dbo.PricePlanMaster.PricePlanId, dbo.Priceplanrestrecordallowtbl.PriceplanRestrictiontblId, dbo.PricePlanMaster.PriceplancatId, dbo.PricePlanMaster.PortalMasterId1, dbo.Priceplancategory.CategoryName FROM  dbo.PriceplanrestrictionTbl LEFT OUTER JOIN dbo.Priceplanrestrecordallowtbl ON dbo.Priceplanrestrecordallowtbl.PriceplanRestrictiontblId = dbo.PriceplanrestrictionTbl.Id INNER JOIN dbo.PricePlanMaster ON dbo.PricePlanMaster.PricePlanId = dbo.Priceplanrestrecordallowtbl.PricePlanId INNER JOIN dbo.Priceplancategory ON dbo.PricePlanMaster.PriceplancatId = dbo.Priceplancategory.ID WHERE    (dbo.PriceplanrestrictionTbl.PortalId = '" + ddlportal.SelectedValue + "') AND dbo.PriceplanrestrictionTbl.Id='" + lbl.Text + "' AND dbo.Priceplancategory.CategoryType='" + Request.QueryString["type"] + "' AND (dbo.PricePlanMaster.active = '1') AND (dbo.Priceplancategory.Status = '1') AND dbo.Priceplanrestrecordallowtbl.RecordsAllowed > " + Label1.Text + "  ORDER BY dbo.Priceplancategory.CategoryName DESC, dbo.PricePlanMaster.PricePlanName DESC, RecordsAllowed asc");
            fillpriceplancateNew();

            string category="";
            if (ddlpriceplancatagory.SelectedIndex > 0)
            {
                category = " AND ID='" + ddlpriceplancatagory.SelectedValue + "'";
            }

            DataTable dtcln = select(" SELECT CategoryName, ID FROM  dbo.Priceplancategory WHERE PortalID='" + ddlportal.SelectedValue + "' and (CategoryType = '" + Request.QueryString["type"] + "') AND (Status = '1') " + category + " ORDER BY CategoryName DESC ");
            
            gvallrest.DataSource = dtcln;
            gvallrest.DataBind();
            ImageButton1.Visible = true;
            pnl1search.Visible = true; 

            //****
             
        }
    }

    protected void gvallrest_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //GetRow
        if (e.CommandName == "GetRow")
        {
            GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
            //Here i am assuming you are having label inside your gridview
            Label lblpid = (Label)row.FindControl("lblpid");
            Response.Redirect("Ordernow.aspx?Pid=" + lblpid.Text + "");
        }

    }
    protected void gvOrdersPRICEPLN_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //GetRow
        if (e.CommandName == "GetRowBuy")
        {
            GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
            //Here i am assuming you are having label inside your gridview
            Label lblpid = (Label)row.FindControl("lblpid");
            Response.Redirect("Ordernow.aspx?Pid=" + lblpid.Text + "");
        }

    }
    
    protected void gvallrest_RowCommand(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
                     //int customerId = Convert.ToInt32(gvCustomers.DataKeys[e.Row.RowIndex].Value.ToString());
            Label lblcb = (Label)e.Row.FindControl("lblcid");        
            
            GridView gvOrdersPRICEPLN = e.Row.FindControl("gvOrdersPRICEPLN") as GridView;
          
            //****************************************************************
            string strmain = "";

           
            string stry = "";
            string andor = "AND";
            if (DDLRest1.SelectedIndex > 0 && (txtrestam1.Text != "" || txtrestam1.Text != ""))
            {
                andor = "AND";
                stry = " AND dbo.Priceplanrestrecordallowtbl.RecordsAllowed " + DDLrescomp1.SelectedValue + " " + txtrestam1.Text + "";
              
            }
            if (DDLrest2.SelectedIndex > 0 && (txtrestam2.Text != "" || txtrestam2.Text != null))
            {

                stry += " " + andor + " dbo.Priceplanrestrecordallowtbl.RecordsAllowed " + DDLrescomp1.SelectedValue + " " + txtrestam2.Text + "";
                
            }
            if (DDLrest3.SelectedIndex > 0 && (txtrestam3.Text != "" || txtrestam3.Text != null))
            {
                stry += " " + andor + " dbo.Priceplanrestrecordallowtbl.RecordsAllowed " + DDLrescomp1.SelectedValue + " " + txtrestam3.Text + "";
              
            }
            if (DDLrest4.SelectedIndex > 0 && (txtrestam4.Text != "" || txtrestam4.Text != null))
            {
                stry += " " + andor + " dbo.Priceplanrestrecordallowtbl.RecordsAllowed " + DDLrescomp1.SelectedValue + " " + txtrestam4.Text + "";
                
            }
            if (stry != "")
            {
                strmain = " AND  dbo.PricePlanMaster.PricePlanId IN (SELECT dbo.PricePlanMaster.PricePlanId as PricePlanId FROM dbo.PriceplanrestrictionTbl LEFT OUTER JOIN dbo.Priceplanrestrecordallowtbl ON dbo.Priceplanrestrecordallowtbl.PriceplanRestrictiontblId = dbo.PriceplanrestrictionTbl.Id INNER JOIN dbo.PricePlanMaster ON dbo.PricePlanMaster.PricePlanId = dbo.Priceplanrestrecordallowtbl.PricePlanId WHERE (dbo.PriceplanrestrictionTbl.PortalId = " + ddlportal.SelectedValue + ") AND (dbo.PricePlanMaster.active = '1') " + stry + ")  ";
            }
            //****************************************************************
            DataTable dtsd = select(" SELECT DISTINCT dbo.PricePlanMaster.PricePlanName, dbo.PricePlanMaster.PricePlanId AS pid,dbo.PricePlanMaster.PricePlanId AS id, dbo.PricePlanMaster.PricePlanAmount, dbo.Priceplancategory.ID AS cid, dbo.PricePlanMaster.PricePlanId, dbo.PricePlanMaster.PriceplancatId, dbo.PricePlanMaster.PortalMasterId1,  dbo.Priceplancategory.CategoryName FROM dbo.PricePlanMaster INNER JOIN dbo.Priceplancategory ON dbo.PricePlanMaster.PriceplancatId = dbo.Priceplancategory.ID WHERE (dbo.Priceplancategory.CategoryType = 'Customer') AND dbo.Priceplancategory.ID='" + lblcb.Text + "' " + strmain + "  ORDER BY dbo.Priceplancategory.CategoryName DESC, dbo.PricePlanMaster.PricePlanName DESC ");
            gvOrdersPRICEPLN.DataSource = dtsd;
            gvOrdersPRICEPLN.DataBind();
            
        }
    }
    protected void gvOrdersPRICEPLN_RowCommand(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //int customerId = Convert.ToInt32(gvCustomers.DataKeys[e.Row.RowIndex].Value.ToString());
            Label lblcb = (Label)e.Row.FindControl("lblpid");

            GridView gvOrdersRESTRIC = e.Row.FindControl("gvOrdersRESTRIC") as GridView;

           
           // ID IN (SELECT dbo.PricePlanMaster.PriceplancatId as ID FROM dbo.PriceplanrestrictionTbl LEFT OUTER JOIN dbo.Priceplanrestrecordallowtbl ON dbo.Priceplanrestrecordallowtbl.PriceplanRestrictiontblId = dbo.PriceplanrestrictionTbl.Id INNER JOIN dbo.PricePlanMaster ON dbo.PricePlanMaster.PricePlanId = dbo.Priceplanrestrecordallowtbl.PricePlanId WHERE (dbo.PriceplanrestrictionTbl.PortalId = " + ddlportal.SelectedValue + ") AND (dbo.PricePlanMaster.active = '1') " + stry + ") 

            DataTable dtsd = select(" SELECT DISTINCT TOP (100) PERCENT dbo.PricePlanMaster.PricePlanName, CONVERT(int, dbo.Priceplanrestrecordallowtbl.RecordsAllowed) AS RecordsAllowed  ,dbo.PricePlanMaster.PricePlanId as pid, dbo.PricePlanMaster.PricePlanAmount,  dbo.Priceplancategory.ID AS cid ,dbo.PriceplanrestrictionTbl.ID,  dbo.PriceplanrestrictionTbl.NameofRest, dbo.PriceplanrestrictionTbl.TextofQueinSelection, dbo.PriceplanrestrictionTbl.Restingroup, dbo.PriceplanrestrictionTbl.PortalId, dbo.PricePlanMaster.PricePlanId, dbo.Priceplanrestrecordallowtbl.PriceplanRestrictiontblId, dbo.PricePlanMaster.PriceplancatId, dbo.PricePlanMaster.PortalMasterId1, dbo.Priceplancategory.CategoryName FROM  dbo.PriceplanrestrictionTbl LEFT OUTER JOIN dbo.Priceplanrestrecordallowtbl ON dbo.Priceplanrestrecordallowtbl.PriceplanRestrictiontblId = dbo.PriceplanrestrictionTbl.Id INNER JOIN dbo.PricePlanMaster ON dbo.PricePlanMaster.PricePlanId = dbo.Priceplanrestrecordallowtbl.PricePlanId INNER JOIN dbo.Priceplancategory ON dbo.PricePlanMaster.PriceplancatId = dbo.Priceplancategory.ID WHERE    (dbo.PriceplanrestrictionTbl.PortalId = '" + ddlportal.SelectedValue + "')  AND dbo.Priceplancategory.CategoryType='" + Request.QueryString["type"] + "' AND (dbo.PricePlanMaster.active = '1') AND (dbo.Priceplancategory.Status = '1') AND dbo.PricePlanMaster.PricePlanId='" + lblcb.Text + "'    ORDER BY dbo.Priceplancategory.CategoryName DESC, dbo.PricePlanMaster.PricePlanName DESC, RecordsAllowed asc ");
            gvOrdersRESTRIC.DataSource = dtsd;
            gvOrdersRESTRIC.DataBind();

        }
    }

    protected void gvOrdersRESTRIC_RowCommand(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {          
            Label lblcb = (Label)e.Row.FindControl("lblpid");
            GridView gvOrdersRESTRIC = e.Row.FindControl("gvOrdersRESTRIC") as GridView;
            DataTable dtsd = select(" SELECT DISTINCT TOP (100) PERCENT dbo.PricePlanMaster.PricePlanName, CONVERT(int, dbo.Priceplanrestrecordallowtbl.RecordsAllowed) AS RecordsAllowed  ,dbo.PricePlanMaster.PricePlanId as pid, dbo.PricePlanMaster.PricePlanAmount, dbo.PriceplanrestrictionTbl.Id, dbo.Priceplancategory.ID AS cid,  dbo.PriceplanrestrictionTbl.NameofRest, dbo.PriceplanrestrictionTbl.TextofQueinSelection, dbo.PriceplanrestrictionTbl.Restingroup, dbo.PriceplanrestrictionTbl.PortalId, dbo.PricePlanMaster.PricePlanId, dbo.Priceplanrestrecordallowtbl.PriceplanRestrictiontblId, dbo.PricePlanMaster.PriceplancatId, dbo.PricePlanMaster.PortalMasterId1, dbo.Priceplancategory.CategoryName FROM  dbo.PriceplanrestrictionTbl LEFT OUTER JOIN dbo.Priceplanrestrecordallowtbl ON dbo.Priceplanrestrecordallowtbl.PriceplanRestrictiontblId = dbo.PriceplanrestrictionTbl.Id INNER JOIN dbo.PricePlanMaster ON dbo.PricePlanMaster.PricePlanId = dbo.Priceplanrestrecordallowtbl.PricePlanId INNER JOIN dbo.Priceplancategory ON dbo.PricePlanMaster.PriceplancatId = dbo.Priceplancategory.ID WHERE    (dbo.PriceplanrestrictionTbl.PortalId = '" + ddlportal.SelectedValue + "')  AND dbo.Priceplancategory.CategoryType='" + Request.QueryString["type"] + "' AND (dbo.PricePlanMaster.active = '1') AND (dbo.Priceplancategory.Status = '1') AND dbo.PricePlanMaster.id='" + lblcb.Text + "'   ORDER BY dbo.Priceplancategory.CategoryName DESC, dbo.PricePlanMaster.PricePlanName DESC, RecordsAllowed asc ");
            gvOrdersRESTRIC.DataSource = dtsd;
            gvOrdersRESTRIC.DataBind();
            
        }
    }
    protected void fillpriceplancateNew()
    {
        ddlpriceplancatagory.Items.Clear();
        string strcln = " SELECT distinct * FROM Priceplancategory where  PortalId='" + ddlportal.SelectedValue + "' and (CategoryType = '" + Request.QueryString["type"] + "') AND (Status = '1') order  by CategoryName";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        ddlpriceplancatagory.DataSource = dtcln;
        ddlpriceplancatagory.DataTextField = "CategoryName";
        ddlpriceplancatagory.DataValueField = "Id";
        ddlpriceplancatagory.DataBind();
        ddlpriceplancatagory.Items.Insert(0, "Select All");
        ddlpriceplancatagory.Items[0].Value = "0";
                                  
       // DataTable dtsd = select(" SELECT DISTINCT TOP (100) PERCENT dbo.PricePlanMaster.PricePlanName, CONVERT(int, dbo.Priceplanrestrecordallowtbl.RecordsAllowed) AS RecordsAllowed  ,dbo.PricePlanMaster.PricePlanId as pid, dbo.PricePlanMaster.PricePlanAmount, dbo.PriceplanrestrictionTbl.Id, dbo.Priceplancategory.ID AS cid,  dbo.PriceplanrestrictionTbl.NameofRest, dbo.PriceplanrestrictionTbl.TextofQueinSelection, dbo.PriceplanrestrictionTbl.Restingroup, dbo.PriceplanrestrictionTbl.PortalId, dbo.PricePlanMaster.PricePlanId, dbo.Priceplanrestrecordallowtbl.PriceplanRestrictiontblId, dbo.PricePlanMaster.PriceplancatId, dbo.PricePlanMaster.PortalMasterId1, dbo.Priceplancategory.CategoryName FROM  dbo.PriceplanrestrictionTbl LEFT OUTER JOIN dbo.Priceplanrestrecordallowtbl ON dbo.Priceplanrestrecordallowtbl.PriceplanRestrictiontblId = dbo.PriceplanrestrictionTbl.Id INNER JOIN dbo.PricePlanMaster ON dbo.PricePlanMaster.PricePlanId = dbo.Priceplanrestrecordallowtbl.PricePlanId INNER JOIN dbo.Priceplancategory ON dbo.PricePlanMaster.PriceplancatId = dbo.Priceplancategory.ID WHERE    (dbo.PriceplanrestrictionTbl.PortalId = '" + ddlportal.SelectedValue + "')  AND dbo.Priceplancategory.CategoryType='" + Request.QueryString["type"] + "' AND (dbo.PricePlanMaster.active = '1') AND (dbo.Priceplancategory.Status = '1')    ORDER BY dbo.Priceplancategory.CategoryName DESC, dbo.PricePlanMaster.PricePlanName DESC, RecordsAllowed asc  ");
        DataTable dtsd = select(" SELECT DISTINCT TOP (100) PERCENT Id, NameofRest, TextofQueinSelection, Restingroup, PortalId FROM dbo.PriceplanrestrictionTbl WHERE        (PortalId = '" + ddlportal.SelectedValue + "')  ");
        
        DDLRest1.DataSource = dtsd;
        DDLRest1.DataTextField = "NameofRest";
        DDLRest1.DataValueField = "Id";
        DDLRest1.DataBind();
        DDLRest1.Items.Insert(0, "Select Restriction");
        DDLRest1.Items[0].Value = "0";

        DDLrest2.DataSource = dtsd;
        DDLrest2.DataTextField = "NameofRest";
        DDLrest2.DataValueField = "Id";
        DDLrest2.DataBind();
        DDLrest2.Items.Insert(0, "Select Restriction");
        DDLrest2.Items[0].Value = "0";

        DDLrest3.DataSource = dtsd;
        DDLrest3.DataTextField = "NameofRest";
        DDLrest3.DataValueField = "Id";
        DDLrest3.DataBind();
        DDLrest3.Items.Insert(0, "Select Restriction");
        DDLrest3.Items[0].Value = "0";

        DDLrest4.DataSource = dtsd;
        DDLrest4.DataTextField = "NameofRest";
        DDLrest4.DataValueField = "Id";
        DDLrest4.DataBind();
        DDLrest4.Items.Insert(0, "Select Restriction");
        DDLrest4.Items[0].Value = "0";
    }
    protected void ddlpriceplancatagory_SelectedIndexChanged(object sender, EventArgs e)
    {
        string category = "";
        if (ddlpriceplancatagory.SelectedIndex > 0)
        {
            category = " AND ID='" + ddlpriceplancatagory.SelectedValue + "'";
        }

        DataTable dtcln = select(" SELECT CategoryName, ID FROM  dbo.Priceplancategory WHERE PortalID='" + ddlportal.SelectedValue + "' and (CategoryType = '" + Request.QueryString["type"] + "') AND (Status = '1') " + category + " ORDER BY CategoryName DESC    ");

        gvallrest.DataSource = dtcln;
        gvallrest.DataBind();
        ImageButton1.Visible = true;   
    }

    protected void txtPlanName_TextChanged(object sender, EventArgs e)
    {
        
    }

    protected void btnnext8_Click(object sender, EventArgs e)
    {
        string category = "";
        if (ddlpriceplancatagory.SelectedIndex > 0)
        {
            category = " AND ID='" + ddlpriceplancatagory.SelectedValue + "'";
        }

        string stry = "";
        string andor = "AND";
        if (DDLRest1.SelectedIndex > 0 && (txtrestam1.Text != "" || txtrestam1.Text != ""))
        {
            andor = "AND";
            stry = " AND dbo.Priceplanrestrecordallowtbl.RecordsAllowed " + DDLrescomp1.SelectedValue + " " + txtrestam1.Text + "";
           
        }
        if (DDLrest2.SelectedIndex > 0 && (txtrestam2.Text != "" || txtrestam2.Text != null))
        {

            stry += " " + andor + " dbo.Priceplanrestrecordallowtbl.RecordsAllowed " + DDLrescomp1.SelectedValue + " " + txtrestam2.Text + "";
            
        }
        if (DDLrest3.SelectedIndex > 0 && (txtrestam3.Text != "" || txtrestam3.Text != null))
        {
            stry += " " + andor + " dbo.Priceplanrestrecordallowtbl.RecordsAllowed " + DDLrescomp1.SelectedValue + " " + txtrestam3.Text + "";
           
        }
        if (DDLrest4.SelectedIndex > 0 && (txtrestam4.Text != "" || txtrestam4.Text != null))
        {
            stry += " " + andor + " dbo.Priceplanrestrecordallowtbl.RecordsAllowed " + DDLrescomp1.SelectedValue + " " + txtrestam4.Text + "";
          
        }

        DataTable dtcln = select(" SELECT CategoryName, ID FROM  dbo.Priceplancategory WHERE  ID IN (SELECT dbo.PricePlanMaster.PriceplancatId as ID FROM dbo.PriceplanrestrictionTbl LEFT OUTER JOIN dbo.Priceplanrestrecordallowtbl ON dbo.Priceplanrestrecordallowtbl.PriceplanRestrictiontblId = dbo.PriceplanrestrictionTbl.Id INNER JOIN dbo.PricePlanMaster ON dbo.PricePlanMaster.PricePlanId = dbo.Priceplanrestrecordallowtbl.PricePlanId WHERE (dbo.PriceplanrestrictionTbl.PortalId = " + ddlportal.SelectedValue + ") AND (dbo.PricePlanMaster.active = '1') " + stry + ")     AND  PortalID='" + ddlportal.SelectedValue + "' and (CategoryType = '" + Request.QueryString["type"] + "') AND (Status = '1') " + category + " ORDER BY CategoryName DESC    ");

        gvallrest.DataSource = dtcln;
        gvallrest.DataBind();
        ImageButton1.Visible = true;   
    }

    protected void btnnext8sea_Click(object sender, EventArgs e)
    {
        pnl2search.Visible = true;  
        
    }

    protected void btnBuySelectSer1_Click(object sender, EventArgs e)
    {   
        GridViewRow row = ((Button)sender).Parent.Parent as GridViewRow;
        tempdata = "";
        int rinrow = row.RowIndex;
        selcol = "1";
        Label lblpid = (Label)gvCustomers.Rows[(rinrow - 1)].FindControl("lblpid" + (selcol));
        lblpopupPPID.Text = lblpid.Text;
     //   lblpopupPPID.Text = "8433";
        ServercheckboxAllow(lblpopupPPID.Text);
    }
    protected void btnBuySelectSer2_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((Button)sender).Parent.Parent as GridViewRow;
        tempdata = "";
        int rinrow = row.RowIndex;
        selcol = "2";
        Label lblpid = (Label)gvCustomers.Rows[(rinrow - 1)].FindControl("lblpid" + (selcol));
        lblpopupPPID.Text = lblpid.Text;
        //   lblpopupPPID.Text = "8433";

        ServercheckboxAllow(lblpopupPPID.Text); 
    }
    protected void btnBuySelectSer3_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((Button)sender).Parent.Parent as GridViewRow;
        tempdata = "";
        int rinrow = row.RowIndex;
        selcol = "3";
        Label lblpid = (Label)gvCustomers.Rows[(rinrow - 1)].FindControl("lblpid" + (selcol));
        lblpopupPPID.Text = lblpid.Text;
        ServercheckboxAllow(lblpopupPPID.Text);  
    }
    protected void btnBuySelectSer4_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((Button)sender).Parent.Parent as GridViewRow;
        tempdata = "";
        int rinrow = row.RowIndex;
        selcol = "4";
        Label lblpid = (Label)gvCustomers.Rows[(rinrow - 1)].FindControl("lblpid" + (selcol));
        lblpopupPPID.Text = lblpid.Text;
        ServercheckboxAllow(lblpopupPPID.Text); 
    }
    protected void btnBuySelectSer5_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((Button)sender).Parent.Parent as GridViewRow;
        tempdata = "";
        int rinrow = row.RowIndex;
        selcol = "5";
        Label lblpid = (Label)gvCustomers.Rows[(rinrow - 1)].FindControl("lblpid" + (selcol));
        lblpopupPPID.Text = lblpid.Text;
        ServercheckboxAllow(lblpopupPPID.Text);
    }
    protected void btnBuySelectSer6_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((Button)sender).Parent.Parent as GridViewRow;
        tempdata = "";
        int rinrow = row.RowIndex;
        selcol = "6";
        Label lblpid = (Label)gvCustomers.Rows[(rinrow - 1)].FindControl("lblpid" + (selcol));
        lblpopupPPID.Text = lblpid.Text;
        ServercheckboxAllow(lblpopupPPID.Text);     
    }
    protected void btnBuySelectSer7_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((Button)sender).Parent.Parent as GridViewRow;
        tempdata = "";
        int rinrow = row.RowIndex;
        selcol = "7";
        Label lblpid = (Label)gvCustomers.Rows[(rinrow - 1)].FindControl("lblpid" + (selcol));
        lblpopupPPID.Text = lblpid.Text;
        ServercheckboxAllow(lblpopupPPID.Text);
    }
    protected void btnBuySelectSer8_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((Button)sender).Parent.Parent as GridViewRow;
        tempdata = "";
        int rinrow = row.RowIndex;
        selcol = "8";
        Label lblpid = (Label)gvCustomers.Rows[(rinrow - 1)].FindControl("lblpid" + (selcol));
        lblpopupPPID.Text = lblpid.Text;
        ServercheckboxAllow(lblpopupPPID.Text);
    }
    //------Modul Project--------------------------------------
    //---------------------------------------------
    public void ServercheckboxAllow(string popupPPID)
    {
         DataTable dtgridm = MyCommonfile.selectBZ(" SELECT dbo.ProductMaster.ProductId, dbo.PortalMasterTbl.Id, dbo.PortalMasterTbl.PortalName FROM dbo.ProductMaster INNER JOIN dbo.PortalMasterTbl ON dbo.ProductMaster.ProductId = dbo.PortalMasterTbl.ProductId where  PortalMasterTbl.Status=1 and IsHostingServer=1 ");
         if (dtgridm.Rows.Count == 1)//if onlly 1 portal for Host
         {
             lbl_hostingportalid.Text = dtgridm.Rows[0]["Id"].ToString();
             lbl_portalname.Text = dtgridm.Rows[0]["PortalName"].ToString();
             lbl_hosting_productid.Text = dtgridm.Rows[0]["ProductId"].ToString();
            // lblportalCat_subTypeid.Text = dtgridm.Rows[0]["ProductId"].ToString();
            // string linkk = "ServerPricePlanComparision.aspx?orderid=" + Session["orderid"].ToString() + "&PPid=" + lblpopupPPID.Text + "&Id=" + lbl_hosting_productid.Text + "&PN=" + lbl_portalname.Text + "&subTypeid=" + lblportalCat_subTypeid.Text + "&type=";
            // linkk = lbl_hostingPortalLink.Text;
             DataTable dtcln = MyCommonfile.selectBZ(" Select dbo.PortalMasterTbl.ProductId, dbo.PortalMasterTbl.PortalName, dbo.PortalMasterTbl.CompanyCreationOption, dbo.PortalMasterTbl.CommonServerAllow,PortalMasterTbl.OwnServerAllow, dbo.PortalMasterTbl.IsWebBasedApplications ,dbo.PortalMasterTbl.LeaseServerAllow ,dbo.PortalMasterTbl.SharedServerAllow , dbo.PortalMasterTbl.SaleServerAllow,dbo.PricePlanMaster.PricePlanId, dbo.PricePlanMaster.PricePlanName, dbo.PricePlanMaster.PricePlanAmount FROM  dbo.PricePlanMaster INNER JOIN dbo.PortalMasterTbl ON dbo.PricePlanMaster.PortalMasterId1 = dbo.PortalMasterTbl.Id Where PricePlanId='" + popupPPID + "' and IsWebBasedApplications=1 ");//and Producthostclientserver='0'
             if (dtcln.Rows.Count > 0)
             {
                 lblPPname.Text = "Priceplan Name : " + dtcln.Rows[0]["PricePlanName"].ToString();
                 lblPPamt.Text = "Price Plan Amt. : " + dtcln.Rows[0]["PricePlanAmount"].ToString();
                 lblportal.Text = "Portal Name : " + dtcln.Rows[0]["PortalName"].ToString();
                 string ProductId = dtcln.Rows[0]["ProductId"].ToString();
                 string PortalName = dtcln.Rows[0]["PortalName"].ToString();

                 //----------------------------------------------------------
                 //Insertas a OrderMaster-----Main PP ID-------------------------------
                 InsertOrderMaster(popupPPID, dtcln.Rows[0]["PricePlanAmount"].ToString(), false, false, true);
                 //-----------------------------------------------------------
                 int int_CompanyCreationOption = 0;
                 try
                 {
                     int_CompanyCreationOption = Convert.ToInt32(dtcln.Rows[0]["CompanyCreationOption"].ToString());
                 }
                 catch (Exception ex)
                 {
                 }
                 if (int_CompanyCreationOption == 1)
                 {
                     pnl_step1.Visible = false;
                     pnl_step2.Visible = true;
                     pnlChkServerType.Visible = true;
                     pnlserverportal.Visible = false;
                     //----------Available Server Type for This portal Status Check---------------------------------------
                     // Own Server Allow
                     #region    1 Own Server
                     try
                     {
                         Boolean Boole_Chk_OwnServer = Convert.ToBoolean(dtcln.Rows[0]["OwnServerAllow"].ToString());
                         if (Boole_Chk_OwnServer == true)
                         {
                             DataTable dtclnv1 = MyCommonfile.selectBZ(" SELECT TOP (1) dbo.PricePlanMaster.PricePlanAmount, dbo.Priceplancategory.ID, dbo.PricePlanMaster.PricePlanId,dbo.PricePlanMaster.DurationMonth,  dbo.PriceplanCategorySubType.Name, dbo.PriceplanCategorySubType.Description FROM dbo.Priceplancategory INNER JOIN dbo.PricePlanMaster ON dbo.Priceplancategory.ID = dbo.PricePlanMaster.PriceplancatId INNER JOIN dbo.PriceplanCategorySubType ON dbo.Priceplancategory.CategoryTypeSubID = dbo.PriceplanCategorySubType.ID where PortalId='" + lbl_hostingportalid.Text + "' and CategoryTypeSubID=5  ORDER BY dbo.PricePlanMaster.PricePlanAmount ");// order  by Id
                             if (dtclnv1.Rows.Count > 0)
                             {
                                 lbl_ownserver1.Visible = true;
                                 chk_ownserver.Visible = true;
                                 Panel1.Visible = true;
                                 //Description
                                 Linkbtn_ownserver1.Text = "Price plan starts from $ " + dtclnv1.Rows[0]["PricePlanAmount"].ToString() + " per " + dtclnv1.Rows[0]["DurationMonth"].ToString() + " days";
                                 lbl_ownserver1.Text = dtclnv1.Rows[0]["Description"].ToString();
                             }
                             else
                             {
                                 lbl_ownserver1.Visible = false;
                                 chk_ownserver.Visible = false;
                                 Panel1.Visible = false;
                             }
                             lbl_ownserver1.Visible = true;
                             chk_ownserver.Visible = true;
                             Panel1.Visible = true;
                         }
                         else
                         {
                             lbl_ownserver1.Visible = false;
                             chk_ownserver.Visible = false;
                             Panel1.Visible = false;
                         }
                     }
                     catch (Exception ex)
                     {
                         lbl_ownserver1.Visible = false;
                         chk_ownserver.Visible = false;
                         Panel1.Visible = false;
                     }
                     #endregion
                     #region  2  Common Server 
                     try
                     {
                         try
                         {
                             Boolean Boole_CommonServerAllow = Convert.ToBoolean(dtcln.Rows[0]["CommonServerAllow"].ToString());
                             if (Boole_CommonServerAllow == true)
                             {
                                 //DataTable dtclnv = select(" SELECT Top(1) * FROM Priceplancategory where PortalId='" + ddlportal.SelectedValue + "' and CategoryTypeSubID=4 order  by Id ");
                                 DataTable dtclnv2 = MyCommonfile.selectBZ(" SELECT TOP (1) dbo.PricePlanMaster.PricePlanAmount, dbo.Priceplancategory.ID, dbo.PricePlanMaster.PricePlanId,dbo.PricePlanMaster.DurationMonth,  dbo.PriceplanCategorySubType.Name, dbo.PriceplanCategorySubType.Description FROM dbo.Priceplancategory INNER JOIN dbo.PricePlanMaster ON dbo.Priceplancategory.ID = dbo.PricePlanMaster.PriceplancatId INNER JOIN dbo.PriceplanCategorySubType ON dbo.Priceplancategory.CategoryTypeSubID = dbo.PriceplanCategorySubType.ID where PortalId='" + lbl_hostingportalid.Text + "' and CategoryTypeSubID=4  ORDER BY dbo.PricePlanMaster.PricePlanAmount ");// order  by Id
                                 if (dtclnv2.Rows.Count > 0)
                                 {
                                     Linkbtn_CommonServerAllow2.Text = "Price plan starts from $ " + dtclnv2.Rows[0]["PricePlanAmount"].ToString() + " per " + dtclnv2.Rows[0]["DurationMonth"].ToString() + " days";
                                     lbl_CommonServerAllow2.Text = dtclnv2.Rows[0]["Description"].ToString();
                                 }
                                 else
                                 {
                                     lbl_CommonServerAllow2.Visible = false;
                                     chk_CommonServerAllow.Visible = false;
                                     Panel2.Visible = false;
                                 }
                                 DataTable dt1018_1015 = select(" select Distinct top(1) ServerMasterTbl.* from dbo.ServerMasterTbl INNER JOIN dbo.Serverstatusmaster ON dbo.ServerMasterTbl.Id = dbo.Serverstatusmaster.SatelliteserverID  Where dbo.Serverstatusmaster.Serversdtatusmasterid='1015' and  (IsSharedServer='1' OR IsSharedServer='0') and MaxCommonCompanyShared >0 and Status=1  ");
                                 if (dt1018_1015.Rows.Count > 0)
                                 {
                                     lbl_CommonServerAllow2.Visible = true;
                                     chk_CommonServerAllow.Visible = true;
                                     Panel2.Visible = true;
                                     //lblportalCat_subTypeid.Text = "4";
                                     //lblportalCat_subType.Text = "Hosting Type :  Common Server";
                                 }
                                 else
                                 {
                                     lbl_CommonServerAllow2.Visible = false;
                                     chk_CommonServerAllow.Visible = false;
                                     Panel2.Visible = false;
                                 }
                             }
                             else
                             {
                                 lbl_CommonServerAllow2.Visible = false;
                                 chk_CommonServerAllow.Visible = false;
                                 Panel2.Visible = false;
                             }
                         }
                         catch (Exception ex)
                         {
                             lbl_CommonServerAllow2.Visible = false;
                             chk_CommonServerAllow.Visible = false;
                             Panel2.Visible = false;
                         }
                     }
                     catch (Exception ex)
                     {
                         lbl_CommonServerAllow2.Visible = false;
                         chk_CommonServerAllow.Visible = false;
                         Panel2.Visible = false;
                     }
                     #endregion
                     #region    3
                     try
                     {
                         Boolean Bool_LeaseServerAllow = Convert.ToBoolean(dtcln.Rows[0]["LeaseServerAllow"].ToString());
                         if (Bool_LeaseServerAllow == true)
                         {
                             //DataTable dtclnv = select(" SELECT Top(1) * FROM Priceplancategory where PortalId='" + ddlportal.SelectedValue + "' and CategoryTypeSubID=2 order  by Id ");
                             DataTable dtclnv3 = MyCommonfile.selectBZ(" SELECT TOP (1) dbo.PricePlanMaster.PricePlanAmount, dbo.Priceplancategory.ID, dbo.PricePlanMaster.PricePlanId,dbo.PricePlanMaster.DurationMonth,  dbo.PriceplanCategorySubType.Name, dbo.PriceplanCategorySubType.Description FROM dbo.Priceplancategory INNER JOIN dbo.PricePlanMaster ON dbo.Priceplancategory.ID = dbo.PricePlanMaster.PriceplancatId INNER JOIN dbo.PriceplanCategorySubType ON dbo.Priceplancategory.CategoryTypeSubID = dbo.PriceplanCategorySubType.ID where PortalId='" + lbl_hostingportalid.Text + "' and CategoryTypeSubID=2  ORDER BY dbo.PricePlanMaster.PricePlanAmount ");// order  by Id
                             if (dtclnv3.Rows.Count > 0)
                             {
                                 Linkbtn_LeaseServerAllow3.Text = "Price plan starts from $ " + dtclnv3.Rows[0]["PricePlanAmount"].ToString() + " per " + dtclnv3.Rows[0]["DurationMonth"].ToString() + " days";
                                 lbl_LeaseServerAllow3.Text = dtclnv3.Rows[0]["Description"].ToString();
                             }
                             else
                             {
                                 lbl_LeaseServerAllow3.Visible = false;                                                         
                                 chk_LeaseServerAllow.Visible = false;
                                 Panel3.Visible = false;
                             }
                             DataTable dt1013_5 = select(" select Distinct top(1) ServerMasterTbl.* from dbo.ServerMasterTbl INNER JOIN dbo.Serverstatusmaster ON dbo.ServerMasterTbl.Id = dbo.Serverstatusmaster.SatelliteserverID  Where dbo.Serverstatusmaster.Serversdtatusmasterid='5' and  IsLeaseServer='1'  and Status=1   ");
                             if (dt1013_5.Rows.Count > 0)
                             {
                               
                                 lbl_LeaseServerAllow3.Visible = true;  
                                 chk_LeaseServerAllow.Visible = true;
                                 Panel3.Visible = true;
                             }
                             else
                             {
                                 lbl_LeaseServerAllow3.Visible = false;
                                
                                 chk_LeaseServerAllow.Visible = false;
                                 Panel3.Visible = false;
                             }
                         }
                         else
                         {
                             lbl_LeaseServerAllow3.Visible = false;                            
                             chk_LeaseServerAllow.Visible = false;
                             Panel3.Visible = false;
                         }
                     }
                     catch (Exception ex)
                     {                        
                         lbl_LeaseServerAllow3.Visible = false;                        
                         chk_LeaseServerAllow.Visible = false;
                         Panel3.Visible = false;
                     }
                     #endregion
                     #region     4
                     try
                     {
                         Boolean Bool_SharedServerAllow = Convert.ToBoolean(dtcln.Rows[0]["SharedServerAllow"].ToString());
                         if (Bool_SharedServerAllow == true)
                         {
                             //DataTable dtclnv = select(" SELECT Top(1) * FROM Priceplancategory where PortalId='" + ddlportal.SelectedValue + "' and CategoryTypeSubID=3 order  by Id ");
                             DataTable dtclnv4 = MyCommonfile.selectBZ(" SELECT TOP (1) dbo.PricePlanMaster.PricePlanAmount, dbo.Priceplancategory.ID, dbo.PricePlanMaster.PricePlanId,dbo.PricePlanMaster.DurationMonth,  dbo.PriceplanCategorySubType.Name, dbo.PriceplanCategorySubType.Description FROM dbo.Priceplancategory INNER JOIN dbo.PricePlanMaster ON dbo.Priceplancategory.ID = dbo.PricePlanMaster.PriceplancatId INNER JOIN dbo.PriceplanCategorySubType ON dbo.Priceplancategory.CategoryTypeSubID = dbo.PriceplanCategorySubType.ID where PortalId='" + lbl_hostingportalid.Text + "' and CategoryTypeSubID=1  ORDER BY dbo.PricePlanMaster.PricePlanAmount ");// order  by Id
                             if (dtclnv4.Rows.Count > 0)
                             {
                                 Linkbtn_SharedServerAllow4.Text = "Price plan starts from $ " + dtclnv4.Rows[0]["PricePlanAmount"].ToString() + " per " + dtclnv4.Rows[0]["DurationMonth"].ToString() + " days";
                                 lbl_SharedServerAllow4.Text = dtclnv4.Rows[0]["Description"].ToString();
                             }
                             else
                             {
                                 lbl_SharedServerAllow4.Visible = false;
                                 Chk_SaleServerAllow.Visible = false;
                                 Panel4.Visible = false;
                             }
                             DataTable dt1011_6 = select(" select Distinct top(1) ServerMasterTbl.* from dbo.ServerMasterTbl INNER JOIN dbo.Serverstatusmaster ON dbo.ServerMasterTbl.Id = dbo.Serverstatusmaster.SatelliteserverID  Where dbo.Serverstatusmaster.Serversdtatusmasterid='6' and  ISSaleServer='1'  and Status=1  ");
                             if (dt1011_6.Rows.Count > 0)
                             {
                                 lbl_SharedServerAllow4.Visible = true;
                                 Chk_SaleServerAllow.Visible = true;
                                 Panel4.Visible = true;
                                 //lblportalCat_subTypeid.Text = "3";
                                 //lblportalCat_subType.Text = "Hosting Type :  Server For Sale";                                   
                                 //lblsalaeserver.Text = "Hosting on your own server placed in " + Session["Clientname"].ToString() + "server farm";
                             }
                             else
                             {
                                 lbl_SharedServerAllow4.Visible = false;
                                 Chk_SaleServerAllow.Visible = false;
                                 Panel4.Visible = false;
                             }
                         }
                         else
                         {
                             lbl_SharedServerAllow4.Visible = false;
                             Chk_SaleServerAllow.Visible = false;
                             Panel4.Visible = false;
                         }
                     }
                     catch (Exception ex)
                     {
                         lbl_SharedServerAllow4.Visible = false;
                         Chk_SaleServerAllow.Visible = false;
                         Panel4.Visible = false;
                     }
                     #endregion
                     #region  5 Shared Priceplan For Server For Shared Lease
                     try
                     {
                            Boolean Bool_SaleServerAllow = Convert.ToBoolean(dtcln.Rows[0]["SaleServerAllow"].ToString());
                         if (Bool_SaleServerAllow == true)
                         {                         
                             //DataTable dtclnv5 = select(" SELECT Top(1) * FROM Priceplancategory where PortalId='" + ddlportal.SelectedValue + "' and CategoryTypeSubID=1 order  by Id ");
                             DataTable dtclnv5 = MyCommonfile.selectBZ(" SELECT TOP (1) dbo.PricePlanMaster.PricePlanAmount, dbo.Priceplancategory.ID, dbo.PricePlanMaster.PricePlanId,dbo.PricePlanMaster.DurationMonth,  dbo.PriceplanCategorySubType.Name, dbo.PriceplanCategorySubType.Description FROM dbo.Priceplancategory INNER JOIN dbo.PricePlanMaster ON dbo.Priceplancategory.ID = dbo.PricePlanMaster.PriceplancatId INNER JOIN dbo.PriceplanCategorySubType ON dbo.Priceplancategory.CategoryTypeSubID = dbo.PriceplanCategorySubType.ID where PortalId='" + lbl_hostingportalid.Text + "' and CategoryTypeSubID=3  ORDER BY dbo.PricePlanMaster.PricePlanAmount ");// order  by Id
                             if (dtclnv5.Rows.Count > 0)
                             {
                                 Linkbtn_SaleServerAllow5.Text = "Price plan starts from $ " + dtclnv5.Rows[0]["PricePlanAmount"].ToString() + " per " + dtclnv5.Rows[0]["DurationMonth"].ToString() + " days";
                                 lbl_SaleServerAllow5.Text = dtclnv5.Rows[0]["Description"].ToString();
                             }
                             else
                             {
                                 lbl_SaleServerAllow5.Visible = false;                                
                                 Chk_SharedServerAllow.Visible = false;
                                 Panel5.Visible = false;
                             }
                             DataTable dt1014_4 = select(" select Distinct top(1) ServerMasterTbl.* from dbo.ServerMasterTbl INNER JOIN dbo.Serverstatusmaster ON dbo.ServerMasterTbl.Id = dbo.Serverstatusmaster.SatelliteserverID  Where ( dbo.Serverstatusmaster.Serversdtatusmasterid='4' OR dbo.Serverstatusmaster.Serversdtatusmasterid='1014') and  IsSharedServer='1' and MaxCompSharing >0 and Status=1  ");
                             if (dt1014_4.Rows.Count > 0)
                             {
                                 lbl_SaleServerAllow5.Visible = true;                                
                                 Chk_SharedServerAllow.Visible = true;                                
                                 Panel5.Visible = true;
                             }
                             else
                             {
                                 lbl_SaleServerAllow5.Visible = false;                               
                                 Chk_SharedServerAllow.Visible = false;
                                 Panel5.Visible = false;
                             }
                         }
                         else
                         {
                             lbl_SaleServerAllow5.Visible = false;                             
                             Chk_SharedServerAllow.Visible = false;
                             Panel5.Visible = false;
                         }
                     }
                     catch (Exception ex)
                     {
                         lbl_SaleServerAllow5.Visible = false;                         
                         Chk_SharedServerAllow.Visible = false;
                         Panel5.Visible = false;
                     }
                     #endregion

                     lbl_ownserver1.Visible = true;
                     chk_ownserver.Visible = true;
                     Panel1.Visible = true;

                     lbl_CommonServerAllow2.Visible = true;
                     chk_CommonServerAllow.Visible = true;
                     Panel2.Visible = true;

                     lbl_SharedServerAllow4.Visible = true;
                     Chk_SaleServerAllow.Visible = true;
                     Panel3.Visible = true;

                     lbl_LeaseServerAllow3.Visible = true;                   
                     chk_LeaseServerAllow.Visible = true;
                     Panel4.Visible = true;

                     lbl_SaleServerAllow5.Visible = true;                    
                     Chk_SharedServerAllow.Visible = true;
                     Panel5.Visible = true;
        }
        else if (int_CompanyCreationOption == 0)
        {
                     string sgggg = "";
                     //Any Hosting Sever Portal Available
                     sgggg = " SELECT PortalMasterTbl.* FROM StateMasterTbl INNER JOIN CountryMaster INNER JOIN ProductMaster INNER JOIN  PortalMasterTbl ON ProductMaster.ProductId = PortalMasterTbl.ProductId ON CountryMaster.CountryId = PortalMasterTbl.CountryId ON StateMasterTbl.StateId = PortalMasterTbl.StateId " +
                                " where  PortalMasterTbl.Status=1 and IsHostingServer=1 ";
                     SqlCommand cmdgrid = new SqlCommand(sgggg, con);
                     if (con.State.ToString() != "Open")
                     {
                         con.Open();
                     }
                     SqlDataAdapter dtpgrid = new SqlDataAdapter(cmdgrid);
                     DataTable dtgrid = new DataTable();
                     dtpgrid.Fill(dtgrid);

                     if (dtgrid.Rows.Count == 1)
                     {
                         string portal = dtgrid.Rows[0]["PortalName"].ToString();
                         string strProductId = dtgrid.Rows[0]["ProductId"].ToString();
                         DataTable dtservice = select("SELECT dbo.PriceplanCategorySubType.UniqueID, dbo.PriceplanCategorySubType.Name as CategorySubTypeName, dbo.PriceplanCategorySubType.Description FROM dbo.Priceplancategory INNER JOIN dbo.PriceplanCategoryType ON dbo.Priceplancategory.CategoryTypeID = dbo.PriceplanCategoryType.ID INNER JOIN dbo.PriceplanCategorySubType ON dbo.Priceplancategory.CategoryTypeSubID = dbo.PriceplanCategorySubType.UniqueID INNER JOIN dbo.PricePlanMaster ON dbo.Priceplancategory.ID = dbo.PricePlanMaster.PriceplancatId where PortalId='" + dtgrid.Rows[0]["id"].ToString() + "' and CategoryTypeID='14' order  by dbo.PriceplanCategorySubType.Name ");
                         if (dtservice.Rows.Count > 0)
                         {
                             string link = "ServicePricePlanComparision.aspx?Id=" + strProductId + "&PN=" + portal + "&orderid=" + Session["orderid"].ToString() + "&Ppid=" + popupPPID + "&SPid=0&Ssid=0&subTypeid=14&type=";//14=Srvice
                             Response.Redirect("" + link + "");
                             //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
                         }
                         else
                         {
                             string link = "Ordernow.aspx?orderid=" + Session["orderid"].ToString() + "&pid=" + popupPPID + "";//14=Srvice                                             
                             Response.Redirect("" + link + "");
                             //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
                         }
                     }
                     if (dtgrid.Rows.Count > 0)
                     {
                         Gv_ServerHostPortal.DataSource = dtgrid;
                         Gv_ServerHostPortal.DataBind();
                         Gv_ServerHostPortal.Visible = true;
                     }

                 }
             }
             else
             {
                 //IS Downloadeble Software
                 DataTable dt_IsDownloadableSoftware = select(" Select  dbo.PortalMasterTbl.ProductId, dbo.PortalMasterTbl.PortalName,dbo.PortalMasterTbl.CommonServerAllow,PortalMasterTbl.OwnServerAllow, dbo.PortalMasterTbl.IsWebBasedApplications ,dbo.PortalMasterTbl.LeaseServerAllow ,dbo.PortalMasterTbl.SharedServerAllow , dbo.PortalMasterTbl.SaleServerAllow,dbo.PricePlanMaster.PricePlanId, dbo.PricePlanMaster.PricePlanName, dbo.PricePlanMaster.PricePlanAmount FROM            dbo.PricePlanMaster INNER JOIN dbo.PortalMasterTbl ON dbo.PricePlanMaster.PortalMasterId1 = dbo.PortalMasterTbl.Id Where PricePlanId='" + popupPPID + "' and IsDownloadableSoftware=1 ");//and Producthostclientserver='0'

                 if (dt_IsDownloadableSoftware.Rows.Count > 0)
                 {
                     string ProductId = dt_IsDownloadableSoftware.Rows[0]["ProductId"].ToString();
                     string PortalName = dt_IsDownloadableSoftware.Rows[0]["PortalName"].ToString();

                     InsertOrderMaster(popupPPID, dt_IsDownloadableSoftware.Rows[0]["PricePlanAmount"].ToString(), false, true, false);
                     string sgggg = "";
                     //Any Hosting Sever Portal Available
                     sgggg = " SELECT PortalMasterTbl.* FROM StateMasterTbl INNER JOIN CountryMaster INNER JOIN ProductMaster INNER JOIN  PortalMasterTbl ON ProductMaster.ProductId = PortalMasterTbl.ProductId ON CountryMaster.CountryId = PortalMasterTbl.CountryId ON StateMasterTbl.StateId = PortalMasterTbl.StateId " +
                                " where  PortalMasterTbl.Status=1 and IsHostingServer=1 ";
                     SqlCommand cmdgrid = new SqlCommand(sgggg, con);
                     if (con.State.ToString() != "Open")
                     {
                         con.Open();
                     }
                     SqlDataAdapter dtpgrid = new SqlDataAdapter(cmdgrid);
                     DataTable dtgrid = new DataTable();
                     dtpgrid.Fill(dtgrid);
                     if (dtgrid.Rows.Count == 1)
                     {
                         //Service Category No14 Available
                         DataTable dtservice = select("SELECT dbo.PriceplanCategorySubType.UniqueID, dbo.PriceplanCategorySubType.Name as CategorySubTypeName, dbo.PriceplanCategorySubType.Description FROM dbo.Priceplancategory INNER JOIN dbo.PriceplanCategoryType ON dbo.Priceplancategory.CategoryTypeID = dbo.PriceplanCategoryType.ID INNER JOIN dbo.PriceplanCategorySubType ON dbo.Priceplancategory.CategoryTypeSubID = dbo.PriceplanCategorySubType.UniqueID INNER JOIN dbo.PricePlanMaster ON dbo.Priceplancategory.ID = dbo.PricePlanMaster.PriceplancatId where PortalId='" + dtgrid.Rows[0]["id"].ToString() + "' and CategoryTypeID='14' order  by dbo.PriceplanCategorySubType.Name ");
                         if (dtservice.Rows.Count > 0)
                         {
                             string portal = dtgrid.Rows[0]["PortalName"].ToString();
                             string strProductId = dtgrid.Rows[0]["ProductId"].ToString();

                             string link = "ServicePricePlanComparision.aspx?Id=" + strProductId + "&PN=" + portal + "&orderid=" + Session["orderid"].ToString() + "&Ppid=" + popupPPID + "&SPid=0&Ssid=0&subTypeid=14&type=";//14=Srvice
                             Response.Redirect("" + link + "");
                             //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
                         }
                     }
                     if (dtgrid.Rows.Count > 0)
                     {
                         Gv_ServerHostPortal.DataSource = dtgrid;
                         Gv_ServerHostPortal.DataBind();
                         Gv_ServerHostPortal.Visible = true;
                     }
                     string links = "Ordernow.aspx?orderid=" + Session["orderid"].ToString() + "&pid=" + popupPPID + "";//14=Srvice                                             
                     Response.Redirect("" + links + "");
                 }
                 else
                 {
                     lblmsg.Text = "For This portal Not allows download software application Or web based applications ";
                 }
             }
         }
         else if (dtgridm.Rows.Count > 0)
         {
             lblmsg.Text = " More Then 1 portal provides server hosting service to other portals of the company ";
         }
         else
         {
             lblmsg.Text = " No any portal provides server hosting service to other portals of the company ";
         }
    }
    //-----------------------------------------------


    protected void FiveServerTypeSelect1_CheckedChanged(object sender, EventArgs e)
    {
        if (chk_ownserver.Checked == true)
        {
            chk_ownserver.Checked = true;
            chk_CommonServerAllow.Checked = false;
            chk_LeaseServerAllow.Checked = false;
            Chk_SharedServerAllow.Checked = false;
            Chk_SaleServerAllow.Checked = false;
           
            Btn_ownserver.Visible = true;
            Btn_CommonServerAllow.Visible = false;
            Btn_LeaseServerAllow.Visible = false;
            Btn_SharedServerAllow.Visible = false;
            Btn_SaleServerAllow.Visible = false;
        }
        else
        {
            Btn_ownserver.Visible = false;
            Btn_CommonServerAllow.Visible = false;
            Btn_LeaseServerAllow.Visible = false;
            Btn_SharedServerAllow.Visible = false;
            Btn_SaleServerAllow.Visible = false;
        }
    }
    protected void FiveServerTypeSelect2_CheckedChanged(object sender, EventArgs e)
    {
        if (chk_CommonServerAllow.Checked == true)
        {
            chk_ownserver.Checked = false;
            chk_CommonServerAllow.Checked = true;
            chk_LeaseServerAllow.Checked = false;
            Chk_SharedServerAllow.Checked = false;
            Chk_SaleServerAllow.Checked = false;
          
            Btn_ownserver.Visible = false;
            Btn_CommonServerAllow.Visible = true;
            Btn_LeaseServerAllow.Visible = false;
            Btn_SharedServerAllow.Visible = false;
            Btn_SaleServerAllow.Visible = false;
        }
        else
        {
            Btn_ownserver.Visible = false;
            Btn_CommonServerAllow.Visible = false;
            Btn_LeaseServerAllow.Visible = false;
            Btn_SharedServerAllow.Visible = false;
            Btn_SaleServerAllow.Visible = false;
        }
    }
    protected void FiveServerTypeSelect3_CheckedChanged(object sender, EventArgs e)
    {
        if (chk_LeaseServerAllow.Checked == true)
        {
            chk_ownserver.Checked = false;
            chk_CommonServerAllow.Checked = false;
            chk_LeaseServerAllow.Checked = true;
            Chk_SharedServerAllow.Checked = false;
            Chk_SaleServerAllow.Checked = false;

            Btn_ownserver.Visible = false;
            Btn_CommonServerAllow.Visible = false;
            Btn_LeaseServerAllow.Visible = true;
            Btn_SharedServerAllow.Visible = false;
            Btn_SaleServerAllow.Visible = false;
        }
        else
        {
            Btn_LeaseServerAllow.Visible = false;
        }
    }
    protected void FiveServerTypeSelect4_CheckedChanged(object sender, EventArgs e)
    {
        if (Chk_SharedServerAllow.Checked == true)
        {
            chk_ownserver.Checked = false;
            chk_CommonServerAllow.Checked = false;
            chk_LeaseServerAllow.Checked = false;
            Chk_SharedServerAllow.Checked = true;
            Chk_SaleServerAllow.Checked = false;
           
            Btn_ownserver.Visible = false;
            Btn_CommonServerAllow.Visible = false;
            Btn_LeaseServerAllow.Visible = false;
            Btn_SharedServerAllow.Visible = true;
            Btn_SaleServerAllow.Visible = false;
        }
        else
        {
            Btn_ownserver.Visible = false;
            Btn_CommonServerAllow.Visible = false;
            Btn_LeaseServerAllow.Visible = false;
            Btn_SharedServerAllow.Visible = false;
            Btn_SaleServerAllow.Visible = false;
        }
    }
    protected void FiveServerTypeSelect5_CheckedChanged(object sender, EventArgs e)
    {
        if (Chk_SaleServerAllow.Checked == true)
        {
            chk_ownserver.Checked = false;
            chk_CommonServerAllow.Checked = false;
            chk_LeaseServerAllow.Checked = false;
            Chk_SharedServerAllow.Checked = false;
            Chk_SaleServerAllow.Checked = true;

            Btn_ownserver.Visible = false;
            Btn_CommonServerAllow.Visible = false;
            Btn_LeaseServerAllow.Visible = false;
            Btn_SharedServerAllow.Visible = false;
            Btn_SaleServerAllow.Visible = true;
            Panel6.Visible = true;
            serverdetails();
        }
        else
        {
            Btn_ownserver.Visible = false;
            Btn_CommonServerAllow.Visible = false;
            Btn_LeaseServerAllow.Visible = false;
            Btn_SharedServerAllow.Visible = false;
            Btn_SaleServerAllow.Visible = false;
            Panel6.Visible = false;
        }
    }

    public void serverdetails()
    {
        filldddlsub();
        filldddlsubsub();
        DDLProductModelName();
        franchisee();
        country();
        fillproductbatch();
        ModalPopupExtender3.Show();
    }
    public void country()
    {
        string sdf = @"select CountryId,CountryName from  CountryMaster";
        SqlCommand cmd = new SqlCommand(sdf, con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ddlcountry.DataSource = dt;
            ddlcountry.DataTextField = "CountryName";
            ddlcountry.DataValueField = "CountryId";
            ddlcountry.DataBind();
        }
    }
    public void fillstate()
    {
        string sdf = @"select StateId,StateName  from StateMasterTbl where  CountryId='" + ddlcountry.SelectedValue+ "'";
        SqlCommand cmd = new SqlCommand(sdf, con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ddlstate.DataSource = dt;
            ddlstate.DataTextField = "CountryName";
            ddlstate.DataValueField = "CountryId";
            ddlstate.DataBind();
        }
    }
    public void franchisee()
    {
        string sdf = @"SELECT  dbo.CompanyMaster.CompanyId, dbo.CompanyMaster.CompanyName   FROM    dbo.CompanyMaster inner join  dbo.PricePlanMaster   ON dbo.PricePlanMaster.PricePlanId = dbo.CompanyMaster.PricePlanId 
                       INNER JOIN dbo.Priceplancategory ON dbo.PricePlanMaster.PriceplancatId = dbo.Priceplancategory.ID   inner join dbo.PortalMasterTbl ON dbo.PortalMasterTbl.Id = dbo.Priceplancategory.PortalId where PortalMasterTbl.Id=5026";
        SqlCommand cmd = new SqlCommand(sdf, con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ddlfranchisee.DataSource = dt;
            ddlfranchisee.DataTextField = "CompanyName";
            ddlfranchisee.DataValueField = "CompanyId";
            ddlfranchisee.DataBind();
        }
    }
    public void fillproductbatch()
    {
        string sty = "";
        if (subtype.SelectedIndex > 0)
        {
            sty += " and Product_SubTypeTbl.ID='" + subtype.SelectedValue + "' ";
        }

        if (subsubtype.SelectedIndex > 0)
        {
            sty += "and Product_Model.ProductSubsubTypeId='" + subsubtype.SelectedValue + "'";
        }
        if (ddlfranchisee.SelectedIndex > 0)
        {
            sty += "and Product_BatchMaster.SupplierID='" + subsubtype.SelectedValue + "'";
        }
        if (ddlcountry.SelectedIndex > 0)
        {
            sty += "and Product_MasterIndividual.country='" + ddlcountry.SelectedValue + "'";
        }
        if (ddlstate.SelectedIndex > 0)
        {
            sty += "and Product_MasterIndividual.state='" + ddlstate.SelectedValue + "'";
        }
        if (productmodel.SelectedIndex > 0)
        {
            sty += "and Product_Model.ID='" + subsubtype.SelectedValue + "'";
        }
        if (txtsearch.Text != "")
        {
            sty += "and ((Product_Model.ProductModelDesc like '%" + txtsearch.Text.Replace("'", "''") + "%') or(Product_Model.ProductModelSpecification like '%" + txtsearch.Text.Replace("'", "''") + "%') or(Product_Model.ProductModelName like '%" + txtsearch.Text.Replace("'", "''") + "%')  or(Product_BatchMaster.BatchName like '%" + txtsearch.Text.Replace("'", "''") + "%') or(Product_BatchMaster.BAtchSpecification like '%" + txtsearch.Text.Replace("'", "''") + "%')) ";

        }
        if (txtprice.Text != "")
        {
            sty += "and Product_BtchPriceSalePriceDetail.SalePrice < " + txtprice.Text + "";
        }
        string sdf = @"SELECT  distinct Product_Model.ProductModelName +'- '+ Product_BatchMaster.BatchName as pname,Product_ModelImgTbl.ImageSmallFront,Product_BatchMaster.ProductBAtchMasterID, Product_BtchPriceSalePriceDetail.SalePrice,Product_BtchPriceSalePriceDetail.ProductBAtchMasterID,Product_Model.ID   FROM  Product_Model   INNER JOIN Product_ModelImgTbl ON Product_Model.ID = Product_ModelImgTbl.ProductModelID 
                       INNER JOIN Product_BatchMaster ON Product_Model.ID = Product_BatchMaster.ProductModelID   INNER JOIN Product_BtchPriceSalePriceDetail ON Product_BatchMaster.ProductBAtchMasterID = Product_BtchPriceSalePriceDetail.ProductBAtchMasterID   INNER JOIN Product_MasterIndividual ON Product_BtchPriceSalePriceDetail.ProductBAtchMasterID = Product_MasterIndividual.ProductBAtchMasterID  inner join Product_SubSubTypeTbl on Product_SubSubTypeTbl.ID=Product_Model.ProductSubsubTypeId  inner join Product_SubTypeTbl on Product_SubTypeTbl.ID=Product_SubSubTypeTbl.ProductSubTypeTblID where Product_BatchMaster.Active='true' and Product_Model.Active='true' and Product_MasterIndividual.Active='true' "+sty+"";
        SqlCommand cmd = new SqlCommand(sdf, con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            DataList2.DataSource = dt;
            DataList2.DataBind();
        }
    }
    protected void DDLProductModelName()
    {
         string str = "";
        if (subsubtype.SelectedIndex > 0)
        {
            str = "and ProductSubsubTypeId='" + subsubtype .SelectedValue+ "'";
        }
        string data = "select * from Product_Model Where ProductModelName !='' and Active='1' "+str+" order by ProductModelName ";
        SqlCommand cmd = new SqlCommand(data, con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            productmodel.DataSource = dt;
            productmodel.DataTextField = "ProductModelName";
            productmodel.DataValueField = "ID";
            productmodel.DataBind();
        }
        productmodel.Items.Insert(0, "All");
        productmodel.Items[0].Value = "0";
    }
    protected void filldddlsub()
    {
        string data = "select * from Product_SubTypeTbl Where  Active='1'  and Name !='' order by Name ";
        SqlCommand cmd = new SqlCommand(data, con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sda.Fill(dt);

        subtype.DataSource = dt;
        subtype.DataTextField = "Name";
        subtype.DataValueField = "ID";

        subtype.DataBind();
        subtype.Items.Insert(0, "All");
        subtype.Items[0].Value = "0";
    }
    protected void filldddlsubsub()
    {
        string str = "";
        if (subtype.SelectedIndex > 0)
        {
            str = " and ProductSubTypeTblID='" + subtype.SelectedValue + "' ";
        }
        string data = "select * from Product_SubSubTypeTbl Where Name !='' and Active='1' " + str + " order by Name ";
        SqlCommand cmd = new SqlCommand(data, con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        subsubtype.DataSource = dt;
        subsubtype.DataTextField = "Name";
        subsubtype.DataValueField = "ID";
        subsubtype.DataBind();
        subsubtype.Items.Insert(0, "All");
        subsubtype.Items[0].Value = "0";
    }
    ///////////-------------------------------
    protected void Linkbtn1_OnClick(object sender, EventArgs e)
    {
        //string te = "http://license.busiwiz.com/Silent_Sync_RequirDailyUpdationTable.aspx?sid=kdQMwcj0lE8=";
        //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void Linkbtn2_OnClick(object sender, EventArgs e)
    {
        //string te = "http://license.busiwiz.com/Silent_Sync_RequirDailyUpdationTable.aspx?sid=kdQMwcj0lE8=";
        //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void Linkbtn3_OnClick(object sender, EventArgs e)
    {
        //string te = "http://license.busiwiz.com/Silent_Sync_RequirDailyUpdationTable.aspx?sid=kdQMwcj0lE8=";
        //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void Linkbtn4_OnClick(object sender, EventArgs e)
    {
        //string te = "http://license.busiwiz.com/Silent_Sync_RequirDailyUpdationTable.aspx?sid=kdQMwcj0lE8=";
        //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void Linkbtn5_OnClick(object sender, EventArgs e)
    {
        //string te = "http://license.busiwiz.com/Silent_Sync_RequirDailyUpdationTable.aspx?sid=kdQMwcj0lE8=";
        //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }


    //Buy New

    protected void Btn_ownserver_Click(object sender, EventArgs e)
    {      
        lblportalCat_subTypeid.Text = "5";
        lblportalCat_subType.Text = "Hosting Type :  Own Server";
        FillHosting_Service_Portal();
        LinkHosting();     
    }
    protected void btn_Chk_CommonServerAllow_Click(object sender, EventArgs e)
    {
        lblportalCat_subTypeid.Text = "4";
        lblportalCat_subType.Text = "Hosting Type :  Common Server";
        FillHosting_Service_Portal();
        LinkHosting();
    }
    protected void Btn_LeaseServerAllow_Click(object sender, EventArgs e)
    {
        lblportalCat_subTypeid.Text = "2";
        lblportalCat_subType.Text = "Hosting Type :  For Exclusive Lease";
        FillHosting_Service_Portal();
        LinkHosting();
    }
    protected void Btn_SharedServerAllow_Click(object sender, EventArgs e)
    {
        lblportalCat_subTypeid.Text = "1";
        lblportalCat_subType.Text = "Hosting Type :  Server For Shared Lease";
        FillHosting_Service_Portal();
        LinkHosting();
    }
    protected void Btn_SaleServerAllow_Click(object sender, EventArgs e)
    {
        lblportalCat_subTypeid.Text = "3";
        lblportalCat_subType.Text = "Hosting Type :  Server For Sale";
        FillHosting_Service_Portal();
        LinkHosting();
    }
    protected void LinkHosting()
    {
        //te += Convert.ToString(link);
        // ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        Response.Redirect("" + lbl_hostingPortalLink.Text + "");
    }
    //---------------------Portal Host Gridview
    protected void FillHosting_Service_Portal()
    {

        DataTable dtgrid = MyCommonfile.selectBZ(" SELECT PortalMasterTbl.*, CountryMaster.CountryId AS Expr1, CountryMaster.CountryName, ProductMaster.ProductId AS Expr2, ProductMaster.ProductName, StateMasterTbl.StateId AS Expr3, StateMasterTbl.StateName FROM StateMasterTbl INNER JOIN CountryMaster INNER JOIN ProductMaster INNER JOIN  PortalMasterTbl ON ProductMaster.ProductId = PortalMasterTbl.ProductId ON CountryMaster.CountryId = PortalMasterTbl.CountryId ON StateMasterTbl.StateId = PortalMasterTbl.StateId where  PortalMasterTbl.Status=1 and IsHostingServer=1 ");
        if (dtgrid.Rows.Count == 1)//if onlly 1 portal for Host
        {
            lbl_hostingportalid.Text= dtgrid.Rows[0]["Id"].ToString();
            string portal = dtgrid.Rows[0]["PortalName"].ToString();
            string strProductId = dtgrid.Rows[0]["ProductId"].ToString();
            string link = "ServerPricePlanComparision.aspx?orderid=" + Session["orderid"].ToString() + "&PPid=" + lblpopupPPID.Text + "&Id=" + strProductId + "&PN=" + portal + "&subTypeid=" + lblportalCat_subTypeid.Text + "&type=";
            lbl_hostingPortalLink.Text = link;
            string te = "";//http://           
        }
        else if (dtgrid.Rows.Count > 0)
        {
            lblmsg.Text = " More Then 1 portal provides server hosting service to other portals of the company ";
        }
        else
        {
            lblmsg.Text = " No any portal provides server hosting service to other portals of the company "; 
        }
        //if (dtgrid.Rows.Count > 0)
        //{
        //    Gv_ServerHostPortal.DataSource = dtgrid;
        //    Gv_ServerHostPortal.DataBind();
        //    Gv_ServerHostPortal.Visible = true;
        //    pnlserverportal.Visible = true;
        //    pnl_step2.Visible = false;
        //    pnl_step1.Visible = false;
        //}
        //else
        //{
        //    Gv_ServerHostPortal.DataSource = null;
        //    Gv_ServerHostPortal.DataBind();
        //}
    }
    protected void Gv_ServerHostPortal_RowEditing1(object sender, GridViewEditEventArgs e)
    {

    }
    protected void Gv_ServerHostPortal_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Gv_ServerHostPortal.PageIndex = e.NewPageIndex;
        FillHosting_Service_Portal();
    }
    protected void Gv_ServerHostPortal_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void Gv_ServerHostPortal_RowCommand(object sender, GridViewCommandEventArgs e)
    {        
        if (e.CommandName == "MarketingURL")
        {
            string mm = Convert.ToString(e.CommandArgument);
            string te = "http://";
            te += Convert.ToString(mm);
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

        }
        if (e.CommandName == "Go")
        {
            int mm = Convert.ToInt32(e.CommandArgument);

           string strportal = " SELECT PortalMasterTbl.* FROM  " +
                  "  dbo.ProductMaster INNER JOIN dbo.PortalMasterTbl ON dbo.ProductMaster.ProductId = dbo.PortalMasterTbl.ProductId " +
                  " where  PortalMasterTbl.Status=1 and Id='" + mm + "' ";
           SqlCommand cmdgrid = new SqlCommand(strportal, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            SqlDataAdapter dtpgrid = new SqlDataAdapter(cmdgrid);
            DataTable dtgrid = new DataTable();
            dtpgrid.Fill(dtgrid);
            if (dtgrid.Rows.Count > 0)
            {
                string portal = dtgrid.Rows[0]["PortalName"].ToString();
                string strProductId = dtgrid.Rows[0]["ProductId"].ToString();
                string link = "ServerPricePlanComparision.aspx?Id=" + strProductId + "&PN=" + portal + "&PPid=" + lblpopupPPID.Text + "&subTypeid=" + lblportalCat_subTypeid.Text + "&type=";
                Response.Redirect("" + link + ""); 
               
               // ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
            }
            
        }
    }
    protected void Gv_ServerHostPortal_RowDataBound(object sender, GridViewRowEventArgs e)
    {

      
    }
    //---INSERT ORDERMASTER AND DETAIL MASTRER    
    protected void InsertOrderMaster(string priceplanid, string amt, Boolean IsHostingServer, Boolean IsServices, Boolean IsWebBasedApplications)
    {
        try
        {
            SqlCommand cmdsq = new SqlCommand("OrderMaster_AddDelUpdtSelect", con);
            cmdsq.CommandType = CommandType.StoredProcedure;
            cmdsq.Parameters.AddWithValue("@StatementType", "Insert");
            cmdsq.Parameters.AddWithValue("@PlanId", priceplanid);
            cmdsq.Parameters.AddWithValue("@Status",false);           
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
        //-MAX

        string s = " select max(OrderId) as OrderId  from OrderMaster ";
        SqlCommand c = new SqlCommand(s, con);
        SqlDataAdapter a = new SqlDataAdapter(c);
        DataTable d = new DataTable();
        a.Fill(d);
        int maxorderid = 0;
        if (d.Rows.Count > 0)
        {
            maxorderid = Convert.ToInt32(d.Rows[0]["orderid"]);
            Session["orderid"] = Convert.ToInt32(d.Rows[0]["orderid"]);
        }
        //------------------
        InsertOrderMasterDetail(Session["orderid"].ToString(), priceplanid, amt, IsHostingServer, IsServices, IsWebBasedApplications,false,"");       

    }
    protected void InsertOrderMasterDetail(string maxorderid, string priceplanid, string amt, Boolean IsHostingServer, Boolean IsServices, Boolean IsWebBasedApplications,Boolean purchase,string invidualid)
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
    protected void subtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        filldddlsubsub();
    }
    protected void subsubtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        DDLProductModelName();
    }
    protected void Button10_Click(object sender, EventArgs e)
    {
        fillproductbatch();
        ModalPopupExtender3.Show();
    }
    protected void btnnext_Click1(object sender, EventArgs e)
    {
        Button lnkbtn = (Button)sender;
        DataListItem item = (DataListItem)lnkbtn.NamingContainer;
        int i = Convert.ToInt32(item.ItemIndex);
        Label id = ((Label)DataList2.Items[i].FindControl("Label8"));
        Label pr = ((Label)DataList2.Items[i].FindControl("lblprice"));
        string s = "select ID from Product_MasterIndividual where Product_MasterIndividual.ProductBAtchMasterID='"+id.Text+"' and Product_MasterIndividual.Active='true'";
        SqlCommand c = new SqlCommand(s, con);
        SqlDataAdapter a = new SqlDataAdapter(c);
        DataTable d = new DataTable();
        a.Fill(d);
        if (d.Rows.Count > 0)
        {
            string dd=d.Rows[0][0].ToString();
            SqlCommand cmd = new SqlCommand("update Product_MasterIndividual set Product_MasterIndividual.Active='false' where Product_MasterIndividual.ID='"+dd.ToString()+"'", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            InsertOrderMasterDetail(Session["orderid"].ToString(), "", pr.Text, false, false, true, true, dd.ToString());

            string sdf = @"SELECT distinct Product_Model.ProductModelName,Product_BatchMaster.BatchName, Product_ModelImgTbl.ImageSmallFront,Product_BatchMaster.ProductBAtchMasterID, 
Product_BtchPriceSalePriceDetail.SalePrice
FROM   Product_Model 
INNER JOIN Product_ModelImgTbl ON Product_Model.ID = Product_ModelImgTbl.ProductModelID 
INNER JOIN Product_BatchMaster ON Product_Model.ID = Product_BatchMaster.ProductModelID 
INNER JOIN Product_BtchPriceSalePriceDetail ON Product_BatchMaster.ProductBAtchMasterID = Product_BtchPriceSalePriceDetail.ProductBAtchMasterID
where Product_BatchMaster.ProductBAtchMasterID='" + id.Text+"'";
            SqlCommand c1 = new SqlCommand(sdf, con);
            SqlDataAdapter a1 = new SqlDataAdapter(c1);
            DataTable d1 = new DataTable();
            a1.Fill(d1);
            if (d1.Rows.Count > 0)
            {
                Image1.ImageUrl = "~/images/" + d1.Rows[0]["ImageSmallFront"].ToString() + "";
                Label53.Text = d1.Rows[0]["ProductModelName"].ToString();
                Label54.Text = d1.Rows[0]["BatchName"].ToString();
                Label55.Text ="$"+ d1.Rows[0]["SalePrice"].ToString();
                Label56.Text = dd.ToString();
                Panel10.Visible = true;
            }
        }
    }
    protected void Button11_Click(object sender, EventArgs e)
    {
        serverdetails();
        
    }
    protected void Button12_Click(object sender, EventArgs e)
    {
       

        SqlCommand cmd1 = new SqlCommand("update Product_MasterIndividual set Product_MasterIndividual.Active='true' where Product_MasterIndividual.ID='" + Label56.Text + "'", con);
        con.Open();
        cmd1.ExecuteNonQuery();
        con.Close();

        SqlCommand cmd = new SqlCommand("delete from OrderMasterDetail where IndvidualId='" + Label56.Text + "'", con);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        serverdetails();
        Panel10.Visible = false;
    }
    protected void ddlcountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillstate();
    }
}
