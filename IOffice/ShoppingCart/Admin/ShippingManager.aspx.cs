
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



public partial class ShoppingCart_Admin_ShippingManager : System.Web.UI.Page
{
    string compid;
  //  SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString"].ConnectionString);
    SqlConnection con=new SqlConnection(PageConn.connnn);
    DBCommands1 dbss1 = new DBCommands1();
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

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();

       // compid = Session["Comid"].ToString();
        Page.Title = pg.getPageTitle(page);
       
        Label2.Text = "";
    
        if (!IsPostBack)
        {
            ViewState["sortOrder"] = "";
            ddlcountry1.DataSource = (DataSet)fillcountry();
            ddlcountry1.DataTextField = "CountryName";
            ddlcountry1.DataValueField = "CountryId";
            ddlcountry1.DataBind();
            ddlcountry1.Items.Insert(0, "-Select-");
            ddlcountry1.Items[0].Value = "0";
            //DataSet ggggg = (DataSet)fillstate();
            //ddlState.DataSource = ggggg;
            //if (ggggg.Tables.Count > 0)
            //{
            //    if (ggggg.Tables[0].Rows.Count > 0)
            //    {
            //        ddlState.DataTextField = "StateName";
            //        ddlState.DataValueField = "StateId";
            //        ddlState.DataBind();
            //    }
            //}

            ddlState.Items.Insert(0, "--Select--");

            ddlState.Items[0].Value = "0";

            FillShipCarrier();
            FillShipMethods();
            FillGridView1();

            lblCompany.Text = Session["Cname"].ToString();
        }
    }
    public DataSet fillcountry()
    {
        SqlCommand cmd = new SqlCommand("Sp_select_CountryMaster", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;

    }

    public DataSet fillstate()
    {
        DataSet ds = new DataSet();
        if (ddlcountry1.SelectedIndex > 0)
        {
            SqlCommand cmd = new SqlCommand("Sp_Select_StateMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CountryId", ddlcountry1.SelectedValue);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);

            adp.Fill(ds);
            return ds;
        }
        return ds;

    }
    protected void FillShipCarrier()
    {
        //string sss = "SELECT ShippingManagerID  ,CarrierMethod FROM ShippingManager";
        //SqlCommand cmd222 = new SqlCommand(sss, con);
        ////cmd.CommandType = CommandType.StoredProcedure;

        //SqlDataAdapter adp222 = new SqlDataAdapter(cmd222);
        //DataTable ds222 = new DataTable();
        //adp222.Fill(ds222);
        //if (ds222.Rows.Count > 0)
        //{
        //    ddlcarrierMethod.DataSource = ds222;
        //    ddlcarrierMethod.DataTextField = "CarrierMethod";
        //    ddlcarrierMethod.DataValueField = "ShippingManagerID";
        //    ddlcarrierMethod.DataBind();
        //    ddlcarrierMethod.Items.Insert(0, "-Select-");
        //    ddlcarrierMethod.Items[0].Value = "0";
        //}
    }
    protected void fillcount()
    {
        string count="SELECT CountryId ,CountryName FROM CountryMaster ";
        SqlCommand cmmm = new SqlCommand(count,con);
        SqlDataAdapter ddaaa = new SqlDataAdapter(cmmm);
        DataTable dttt = new DataTable();
        ddaaa.Fill(dttt);

        if (dttt.Rows.Count > 0)
        {
            ddlcountry1.DataSource = dttt;
            ddlcountry1.DataTextField = "CountryName";
            ddlcountry1.DataValueField = "CountryId";
            ddlcountry1.DataBind();
            ddlcountry1.Items.Insert(0, "--Select--");
            ddlcountry1.Items[0].Value = "0";
        }
    }
    //protected void fillstat1()
    //{
    //    string count = "SELECT StateName, StateId, CountryId  FROM StateMasterTbl Where CountryId ='"+ddlcountry1.SelectedValue+"'  ";
    //    SqlCommand cmmm = new SqlCommand(count, con);
    //    SqlDataAdapter ddaaa = new SqlDataAdapter(cmmm);
    //    DataTable dttt = new DataTable();
    //    ddaaa.Fill(dttt);

    //    if (dttt.Rows.Count > 0)
    //    {
    //        ddlState1.DataSource = dttt;
    //        ddlState1.DataTextField = "StateName";
    //        ddlState1.DataValueField = "StateId";
    //        ddlState1.DataBind();
    //        ddlState1.Items.Insert(0, "--Select--");
    //        ddlState1.Items[0].Value = "0";
    //    }
    //}


    protected void FillShipMethods()
    {
        string sss2 = "SELECT ShippingMethodId  ,ShippingMethod    FROM ShippingMethod";
        SqlCommand cmd2222 = new SqlCommand(sss2, con);
        //cmd.CommandType = CommandType.StoredProcedure;

        SqlDataAdapter adp2222 = new SqlDataAdapter(cmd2222);
        DataTable ds2222 = new DataTable();
        adp2222.Fill(ds2222);
        if (ds2222.Rows.Count > 0)
        {
            DropDownList1.DataSource = ds2222;
            DropDownList1.DataTextField = "ShippingMethod";
            DropDownList1.DataValueField = "ShippingMethodId";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, "-Select-");
            DropDownList1.Items[0].Value = "0";
        }
    }

    //protected void fillcountry()
    //{
    //    string qryStr = "select CountryId,CountryName from CountryMaster order by CountryName";
    //    ddlCountry.DataSource = (DataSet)fillddl(qryStr);
    //    fillddlOther(ddlCountry, "CountryName", "CountryId");

    //    ddlCountry.Items.Insert(0, "-Select-");
    //    ddlCountry.Items[0].Value = "0";
    //}
    public void fillddlOther(DropDownList ddl, String dtf, String dvf)
    {
        ddl.DataTextField = dtf;
        ddl.DataValueField = dvf;
        ddl.DataBind();       
    }
    protected void ddlcarrierMethod_SelectedIndexChanged(object sender, EventArgs e)
    {


    }
    protected void ddlcountry_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlcountry_SelectedIndexChanged1(object sender, EventArgs e)
    {
        ddlState.Items.Clear();
        if (ddlcountry.SelectedIndex > 0)
        {
            ddlState.DataSource = (DataSet)fillstate();
            ddlState.DataTextField = "StateName";
            ddlState.DataValueField = "StateId";
            ddlState.DataBind();
        }
        else
        {

        }
        ddlState.Items.Insert(0, "-Select-");
        ddlState.Items[0].Value = "0";
    }

    public void cancel()
    {
        ddlcarrierMethod.Text = "";
        rbUserYes.Checked = true;
        rbShopCartyes.Checked = true;
        rbMarkYes.Checked = true;
        rbHideYes.Checked = true;
        rbCarrierYes.Checked = true;
        rbbyPriceYes.Checked = true;
        rbCarrierunitYes.Checked = true;
        rbShopCartyes.Checked = true;
        txtCustome.Text = "";
        txtMaxWeight.Text = "0";
        txtMinWeight.Text = "0";
        ddlcarrierMethod.Text = "";
       // ddlcountry1.SelectedIndex = 0;
        //ddlcountry_SelectedIndexChanged(ddlcountry, 0);
   //     ddlState1.SelectedIndex = 0;
        txtNotes.Text = "";
       // DropDownList1.SelectedIndex = 0;
        //Label2.Text = "";
      //  DropDownList2.SelectedIndex = 0;
        TextBox1.Text = "";
        panelgridstate.Visible = false;


    }
    protected void FillGridView1()
    {

        string Fillgrd1 = " SELECT   Distinct  ShippingManager.ShippingManagerID, ShippingManager.CarrierMethod, ShippingManager.State_ID, ShippingManager.Country_ID, ShippingManager.MinWeight,  " +
                    "   ShippingManager.MaxWeight, ShippingMethod.ShippingMethod as ShippingTypeName ,ShippingManager.MarkUp, ShippingManager.Carrierunit, ShippingManager.Ispercent, ShippingManager.Exclude, " +
                    "  ShippingManager.CarrierHide, ShippingManager.ShippingType, ShippingManager.Hide, ShippingManager.ByPrice, ShippingManager.ShopCart, " +
                    "  ShippingManager.UseRules, ShippingManager.Notes, ShippingManager.Custom,Case When( CountryMaster.CountryName IS NULL) then 'ALL' else CountryMaster.CountryName End as CountryName, StateMasterTbl.StateName " +
                     " FROM         ShippingManager LEFT OUTER JOIN " +
                    "  StateMasterTbl ON ShippingManager.State_ID = StateMasterTbl.StateId LEFT OUTER JOIN " +
                    "  CountryMaster ON ShippingManager.Country_ID = CountryMaster.CountryId inner join ShippingMethod on ShippingMethod.ShippingMethodId=ShippingManager.ShippingType  order by CarrierMethod,CountryName";
        DataTable dtfillgrd1 = dbss1.cmdSelect(Fillgrd1);
       
            GridView1.DataSource = dtfillgrd1;
            DataView myDataView = new DataView();
            myDataView = dtfillgrd1.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }
            GridView1.DataBind();
         
        
       


        foreach (GridViewRow gdr in GridView1.Rows)
        {
            Label lblshipmngrid = (Label)gdr.FindControl("lblShipMangrId");

            Label lblCntId = (Label)gdr.FindControl("lblCntId");
            Label lblstid = (Label)gdr.FindControl("lblstid");


            SqlDataAdapter dat = new SqlDataAdapter("select StateMasterTbl.StateName,ShippingmanagerStateDetail.StateID from ShippingmanagerStateDetail inner join StateMasterTbl on ShippingmanagerStateDetail.StateID=StateMasterTbl.StateId where ShippingmanagerStateDetail.ShippingManagerID='" + lblshipmngrid.Text + "' order by StateName", con);
            DataTable dtts = new DataTable();
            dat.Fill(dtts);
            string state1 = "";
            if (dtts.Rows.Count > 0)
            {
               // state1 = "";
                foreach (DataRow dr in dtts.Rows)
                {
                    string state = Convert.ToString(dr["StateName"]);

                    if (state != "")
                    {                        
                        state1 = state1 + state + ",";
                    }
                    
                }
                Label lblstName = (Label)gdr.FindControl("lblstName");
                lblstName.Text = state1;
              string st=  lblstName.Text.Substring(0, lblstName.Text.Length - 1);
              lblstName.Text = st;
               // lblstName.Text.Length;
                //lblstName.Text.Replace("lblstName.Text.Length", " ");
            }
            else
            {
                Label lblstName = (Label)gdr.FindControl("lblstName");
                lblstName.Text = "ALL";
            }
            
        }


    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        decimal c1 = Convert.ToDecimal(isdecimalornot(txtMaxWeight.Text));
        decimal b1 = Convert.ToDecimal(isdecimalornot(txtMinWeight.Text));
        if (b1 > c1)
        {

            lblmsg.Visible = true;

            lblmsg.Text = "Minimum Weight Must Be Less Than Maximum Weight";

        }

        else
        {
            try
            {

                int a, b, c, d, f, g, h, i, j, m;

                if (rbCarrierNo.Checked == true)
                {
                    b = 0;
                }
                else { b = 1; }

                if (rbIsperNo.Checked == true)
                {
                    c = 0;
                }
                else { c = 1; }


                if (rbCarrierunitNo.Checked == true)
                {
                    d = 0;
                }
                else { d = 1; }


                if (rbHideNo.Checked == true)
                {
                    f = 0;
                }
                else { f = 1; }


                if (rbExcludeNo.Checked == true)
                {
                    g = 0;
                }
                else { g = 1; }


                if (rbbyPriceNo.Checked == true)
                {
                    h = 0;
                }
                else { h = 1; }


                if (rbMarkupNo.Checked == true)
                {
                    i = 0;
                }
                else { i = 1; }


                if (rbShopCartNo.Checked == true)
                {
                    j = 0;
                }
                else { j = 1; }

                if (rbUserno.Checked == true)
                {
                    m = 0;
                }
                else { m = 1; }
                string strmaster = "SELECT * from ShippingManager  where CarrierMethod='" + ddlcarrierMethod.Text + "' ";
                SqlCommand cmdmaster = new SqlCommand(strmaster, con);
                SqlDataAdapter adpMaster = new SqlDataAdapter(cmdmaster);
                DataTable dtmaster = new DataTable();
                adpMaster.Fill(dtmaster);
                if (dtmaster.Rows.Count == 0)
                {
                    string str = "insert into ShippingManager(CarrierMethod,State_ID,Country_ID,MinWeight,MaxWeight," +
                        " MarkUp,Carrierunit,Ispercent,Exclude,CarrierHide,ShippingType,Hide,ByPrice,ShopCart,UseRules,Notes,Custom,TrackingURL,Protocol)" +
                        " values('" + ddlcarrierMethod.Text + "'," + ddlState.SelectedValue + "," + ddlcountry1.SelectedValue + "," + txtMinWeight.Text + "," + txtMaxWeight.Text + "," +
                        " " + i + "," + d + "," + c + "," + g + "," + b + ",2," + f + "," + h + "," + j + "," +
                        " " + m + ",'" + txtNotes.Text + "','" + txtCustome.Text + "','"+TextBox1.Text+"','"+DropDownList2.SelectedValue+"')";

                    SqlCommand cmd = new SqlCommand(str, con);
                    if(cmd.Connection.State.ToString()!="Open")
                    {
                        con.Open();
                    }
                    cmd.ExecuteNonQuery();
                    con.Close();
                    SqlDataAdapter dag = new SqlDataAdapter("select max(ShippingManagerID) as ShippingManagerID from ShippingManager", con);
                    DataTable dtg = new DataTable();
                    dag.Fill(dtg);

                    ViewState["shipid"] = dtg.Rows[0]["ShippingManagerID"].ToString();
                    Boolean bul = false;
                    foreach (GridViewRow gdr in GridView2.Rows)
                    {
                        Label labelstate = (Label)gdr.FindControl("Label53");
                        CheckBox checkstate = (CheckBox)gdr.FindControl("CheckBox1");

                        if (checkstate.Checked == true)
                        {
                            bul = true;
                            SqlCommand cmdg = new SqlCommand("insert into ShippingmanagerStateDetail(ShippingManagerID,CountryID,StateID) values('" + ViewState["shipid"].ToString() + "','" + ddlcountry1.SelectedValue + "','" + labelstate.Text + "')", con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmdg.ExecuteNonQuery();
                            con.Close();
                            string st = "Select State,Country,StateName,CountryName,CompanyWebsitMaster.Whid,comid from CountryMaster inner join CompanyWebsiteAddressMaster on CompanyWebsiteAddressMaster.Country=CountryMaster.CountryId inner join StateMasterTbl on StateMasterTbl.StateId=CompanyWebsiteAddressMaster.State  inner join CompanyWebsitMaster on CompanyWebsitMaster.CompanyWebsiteMasterId=CompanyWebsiteAddressMaster.CompanyWebsiteMasterId  inner join WarehouseMaster on WarehouseMaster.WarehouseId=CompanyWebsitMaster.Whid where State='" + labelstate.Text + "'";
                            DataTable dtst = dbss1.cmdSelect(st);
                            foreach (DataRow item in dtst.Rows)
                            {


                                string strsh = "Insert Into ShippersMaster(ShippersName,shippingDocNo,compid,Whid,DocumentName,TrackingUrl,Protocol,ShippingMethodId)values('" + ddlcarrierMethod.Text + "','0','" + item["comid"] + "','" + item["Whid"] + "','','" + TextBox1.Text + "','" + DropDownList2.SelectedValue + "','2')";
                                SqlCommand cmdsh = new SqlCommand(strsh, con);
                                if (con.State.ToString() != "Open")
                                {
                                    con.Open();
                                }
                                cmdsh.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                        
                    }
                    if (bul == false)
                    {
                        string st = "Select State,Country,StateName,CountryName,CompanyWebsitMaster.Whid,comid from CountryMaster inner join CompanyWebsiteAddressMaster on CompanyWebsiteAddressMaster.Country=CountryMaster.CountryId inner join StateMasterTbl on StateMasterTbl.StateId=CompanyWebsiteAddressMaster.State  inner join CompanyWebsitMaster on CompanyWebsitMaster.CompanyWebsiteMasterId=CompanyWebsiteAddressMaster.CompanyWebsiteMasterId  inner join WarehouseMaster on WarehouseMaster.WarehouseId=CompanyWebsitMaster.Whid where Country='" + ddlcountry1.SelectedValue + "'";
                        DataTable dtst = dbss1.cmdSelect(st);
                        foreach (DataRow item in dtst.Rows)
                        {


                            string strsh = "Insert Into ShippersMaster(ShippersName,shippingDocNo,compid,Whid,DocumentName,TrackingUrl,Protocol,ShippingMethodId)values('" + ddlcarrierMethod.Text + "','0','" + item["comid"] + "','" + item["Whid"] + "','','" + TextBox1.Text + "','" + DropDownList2.SelectedValue + "','2')";
                            SqlCommand cmdsh = new SqlCommand(strsh, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmdsh.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                    Label2.Visible = true;
                    Label2.Text = "Record inserted successfully";
                    cancel();
                    FillGridView1();

                    addinventoryroom.Visible = false;
                    lbllegend.Text = "";
                    btnaddroom.Visible = true;

                    
                }
                else
                {
                    Label2.Visible = true;
                    Label2.Text = "Record alredy exists";
                }

                
                }
        


            catch (Exception ex)
            {
                Label2.Visible = true;
                Label2.Text = ex.ToString();
            }
            finally { }
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
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (Button2.Visible = true)
        {
            Button2.Visible = false;
            ImageButton1.Visible = true;
        }
        cancel();
        addinventoryroom.Visible = false;
        lbllegend.Text = "";
        btnaddroom.Visible = true;
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

      
       


        Button2.Visible = true;
        ImageButton1.Visible = false;
        addinventoryroom.Visible = true;
        lbllegend.Visible = true;
        panelgridstate.Visible = true;
        lbllegend.Text = "Edit Shipping Company";
        btnaddroom.Visible = false;
        GridView1.SelectedIndex = Convert.ToInt32(GridView1.DataKeys[e.NewEditIndex].Value.ToString());
        int h = Convert.ToInt32(GridView1.SelectedIndex);
        //int dk1 = Convert.ToInt32(GridView1.DataKeys[e.NewEditIndex].Value);
        int dk1 = h;
        ViewState["Id"] = h.ToString();

        string eeed = " select * from ShippingManager  where ShippingManagerID='" + dk1 + "'  ";
        SqlCommand cmdeeed = new SqlCommand(eeed, con);
        SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
        DataTable dteeed = new DataTable();
        adpeeed.Fill(dteeed);

        //fillstore();
        FillShipMethods();
        DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByValue(Convert.ToInt32(dteeed.Rows[0]["ShippingType"]).ToString()));
        fillcount();
        ddlcountry1.SelectedIndex = ddlcountry1.Items.IndexOf(ddlcountry1.Items.FindByValue(dteeed.Rows[0]["Country_ID"].ToString()));
      //  fillstat1();      
      //  ddlState1.SelectedIndex = ddlState1.Items.IndexOf(ddlState1.Items.FindByValue(dteeed.Rows[0]["State_ID"].ToString()));

        SqlCommand cmdst = new SqlCommand("SELECT StateName, StateId, CountryId  FROM StateMasterTbl Where CountryId ='" + ddlcountry1.SelectedValue + "'", con);
        SqlDataAdapter dast = new SqlDataAdapter(cmdst);
        DataTable dtst = new DataTable();
        dast.Fill(dtst);

        GridView2.DataSource = dtst;
        GridView2.DataBind();
        SqlCommand cmdstate = new SqlCommand("select * from ShippingmanagerStateDetail where ShippingManagerID='" + dk1 + "' and CountryID='" + dteeed.Rows[0]["Country_ID"].ToString() + "'",con);
        SqlDataAdapter dastate = new SqlDataAdapter(cmdstate);
        DataTable dtstate = new DataTable();
        dastate.Fill(dtstate);
        if (dtstate.Rows.Count > 0)
        {

            for (int i = 0; i < dtstate.Rows.Count; i++)
            {
                foreach (GridViewRow gdr in GridView2.Rows)
                {
                    CheckBox checkstate = (CheckBox)gdr.FindControl("CheckBox1");
                    Label Label53 = (Label)gdr.FindControl("Label53");
                    // int n = Convert.ToInt32(dr.ToString());
                    if (Label53.Text == dtstate.Rows[i]["StateID"].ToString())
                    {
                        checkstate.Checked = true;
                    }
                }
            }
          
        }
        if (GridView2.Rows.Count > 0)
        {
            panelgridstate.Visible = true;
        }
        else
        {
            panelgridstate.Visible = false;
        }
        ddlcarrierMethod.Text =Convert.ToString(dteeed.Rows[0]["CarrierMethod"]);
        TextBox1.Text = Convert.ToString(dteeed.Rows[0]["TrackingURL"]);
    //    DropDownList2.Text = dteeed.Rows[0]["Protocol"].ToString();
        try
        {
            DropDownList2.SelectedValue = Convert.ToString(dteeed.Rows[0]["Protocol"]);
        }
        catch
        {
        }
     //   DropDownList2.SelectedItem.Text = dteeed.Rows[0]["Protocol"].ToString();

    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //Label lblid = (Label)GridView1.Rows[e.RowIndex].FindControl("lblShipMangrId");
        //ViewState["lblid"] = lblid.Text.ToString();

        //GridView1.SelectedIndex = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
        //ViewState["lblid"] = GridView1.SelectedIndex;


        //SqlCommand cmd = new SqlCommand("SELECT ShippingManagerId  FROM  ShippingAccountInfo where ShippingManagerId='" + ViewState["lblid"] + "' ", con);
        //SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //DataTable ddt = new DataTable();
        //adp.Fill(ddt);
        //if (ddt.Rows.Count > 0)
        //{

        //    Label2.Visible = true;
        //    Label2.Text = "Sorry edit not allowed.";
        //}
        //else
        //{
        //   // ModalPopupExtender1222.Show();
        //    yes_Click(sender, e);
        //}

    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        FillGridView1();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        //DropDownList ddlc = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddlcountry");
        //DropDownList ddls = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddlstate");
        //Label lblshipmgrId = (Label)GridView1.Rows[e.RowIndex].FindControl("lblShipMangrId");
        //Label lblcid = (Label)GridView1.Rows[e.RowIndex].FindControl("lblCntId");
        //Label lblsid = (Label)GridView1.Rows[e.RowIndex].FindControl("lblstid");

        ////CheckBox ckmark = (CheckBox)GridView1.Rows[e.RowIndex].FindControl("lblMarkUp");
        ////CheckBox ckcariunit = (CheckBox)GridView1.Rows[e.RowIndex].FindControl("lblCarrierunit");
        ////CheckBox ckisper = (CheckBox)GridView1.Rows[e.RowIndex].FindControl("lblIspercent");
        //CheckBox ckexcl = (CheckBox)GridView1.Rows[e.RowIndex].FindControl("lblExclude");
        //CheckBox ckcrhd = (CheckBox)GridView1.Rows[e.RowIndex].FindControl("lblCarrierHide");
        //CheckBox ckhide = (CheckBox)GridView1.Rows[e.RowIndex].FindControl("lblHide");
        //CheckBox ckbyprc = (CheckBox)GridView1.Rows[e.RowIndex].FindControl("lblByPrice");
        //CheckBox ckshpct = (CheckBox)GridView1.Rows[e.RowIndex].FindControl("lblShopCart");
        //CheckBox ckusrl = (CheckBox)GridView1.Rows[e.RowIndex].FindControl("lblUseRules");


        //TextBox txtcarier = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtcarier");
        //TextBox txtminwgt = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtMinWeight");
        //TextBox txtmaxwgt = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtMaxWeight");
        //TextBox txtshipmethod = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtlShipMethod");
        //TextBox txtcustm = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtCustom");
        //TextBox txtnote = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtNotes");

        //decimal c1 = Convert.ToDecimal(isdecimalornot(txtmaxwgt.Text));
        //decimal b1 = Convert.ToDecimal(isdecimalornot(txtminwgt.Text));
        //if (b1 > c1)
        //{

        //    lblmsg.Visible = true;

        //    lblmsg.Text = "Minimum Weight Must Be Less Than Maximum Weight";

        //}

        //else
        //{
        //    try
        //    {
        //        string strmaster = "SELECT * from ShippingManager  where CarrierMethod='" + txtcarier.Text + "' and ShippingManagerID<>'" + lblshipmgrId.Text + "' ";
        //        SqlCommand cmdmaster = new SqlCommand(strmaster, con);
        //        SqlDataAdapter adpMaster = new SqlDataAdapter(cmdmaster);
        //        DataTable dtmaster = new DataTable();
        //        adpMaster.Fill(dtmaster);
        //        if (dtmaster.Rows.Count == 0)
        //        {
        //            string updateshimgr = "update  ShippingManager      set " +
        //                    " CarrierMethod='" + txtcarier.Text + "' , " +
        //                    " State_ID='" + ddls.SelectedValue + "' , " +
        //                    " Country_ID='" + ddlc.SelectedValue + "' , " +
        //                    " MinWeight='" + txtminwgt.Text + "' , " +
        //                    " MaxWeight='" + txtmaxwgt.Text + "' , " +
        //                //" MarkUp='" + ckmark.Checked + "' , " +
        //                //" Carrierunit='" + ckcariunit.Checked + "' , " +
        //                //" Ispercent='" + ckisper.Checked + "' , " +
        //                    " Exclude='" + ckexcl.Checked + "' , " +
        //                    " CarrierHide='" + ckcrhd.Checked + "' , " +
        //                     " ShippingType='" + txtshipmethod.Text + "' , " +
        //                    " Hide='" + ckhide.Checked + "' , " +
        //                    " ByPrice='" + ckbyprc.Checked + "' , " +
        //                    " ShopCart='" + ckshpct.Checked + "' , " +
        //                    " UseRules='" + ckusrl.Checked + "' , " +
        //                    " Notes='" + txtnote.Text + "' , " +
        //                    " Custom='" + txtcustm.Text + "'  " +
        //                    " where  ShippingManagerID='" + lblshipmgrId.Text + "' ";
        //            SqlCommand cmdupadate = new SqlCommand(updateshimgr, con);
        //            con.Open();
        //            cmdupadate.ExecuteNonQuery();
        //            con.Close();
        //            Label2.Visible = true;
        //            Label2.Text = "Record Update Successfully";

        //            GridView1.EditIndex = -1;
        //            FillGridView1();
        //        }
        //        else
        //        {
        //            Label2.Visible = true;
        //            Label2.Text = "Record Already used";
        //        }
        //    }
        //    catch (Exception a)
        //    {
        //        Label2.Visible = true;
        //        Label2.Text = "Error :" + a.Message;
        //    }

        //}


    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            //GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            // ViewState["id"] = GridView1.SelectedDataKey.Value;

            int m = Convert.ToInt32(e.CommandArgument);

            SqlCommand cmddel = new SqlCommand("delete from ShippingManager where ShippingManagerID="+m, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmddel.ExecuteNonQuery();
            con.Close();

            SqlCommand cmddel1 = new SqlCommand("delete from ShippingmanagerStateDetail where ShippingManagerID=" + m, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmddel1.ExecuteNonQuery();
            con.Close();

            Label2.Visible = true;
            Label2.Text = "Record deleted successfully";
            FillGridView1();
        }
    }
    protected void ddlcountry_SelectedIndexChanged2(object sender, EventArgs e)
    {
        DropDownList ddlCountry = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("ddlcountry");
        DropDownList ddlState = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("ddlstate");
        FillGrdCntST(ddlCountry, ddlState);
    }
    protected void FillGrdCntST(DropDownList ddlCnt, DropDownList ddlSt)
    {
        ddlSt.Items.Clear();
        if (ddlCnt.SelectedIndex > 0)
        {
            string str45 = "SELECT     StateName  ,StateId " +
                   " FROM  StateMasterTbl where CountryId='" + ddlCnt.SelectedValue + "' " +
                   " Order By StateName";

            SqlCommand cmd45 = new SqlCommand(str45, con);

            SqlDataAdapter da = new SqlDataAdapter(cmd45);

            DataTable ds = new DataTable();

            da.Fill(ds);
            ddlSt.DataSource = ds;
            ddlSt.DataTextField = "StateName";
            ddlSt.DataValueField = "StateId";
            ddlSt.DataBind();

        }

        ddlSt.Items.Insert(0, "ALL");
        //ddlcountry.Items[0].Value = "0";
        ddlSt.Items[0].Value = "0";
        //ddlcity.Items[0].Value = "0";




    }
    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/ShoppingCart/Admin/wzShippingMethod.aspx");
    }
    protected void yes_Click(object sender, EventArgs e)
    {
        try
        {

            string strdel = " delete ShippingManager where ShippingManagerID='" + ViewState["lblid"] + "' ";
            SqlCommand cmddel = new SqlCommand(strdel, con);
            if (cmddel.Connection.State.ToString() != "Open")
            {
                con.Open();
            }
            cmddel.ExecuteNonQuery();
            con.Close();
            Label2.Visible = true;
            Label2.Text = "Record delete successfully";
            GridView1.EditIndex = -1;
            FillGridView1();
          //  ModalPopupExtender1222.Hide();

        }
        catch (Exception ty1)
        {
            Label2.Visible = true;
            Label2.Text = "Error ;" + ty1.Message;
            //ModalPopupExtender1222.Hide();
        }
    }
    protected void ImageButton6_Click(object sender, ImageClickEventArgs e)
    {
        ModalPopupExtender1222.Hide();
    }
    protected void btnaddroom_Click(object sender, EventArgs e)
    {
        if (addinventoryroom.Visible == false)
        {
            addinventoryroom.Visible = true;
            lbllegend.Visible = true;
            lbllegend.Text = "Add Shipping Company";
        }
        else if (addinventoryroom.Visible == true)
        {
            addinventoryroom.Visible = false;

            lbllegend.Text = "";
        }
       lblmsg.Text = "";
        btnaddroom.Visible = false;
    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");

            Button1.Text = "Hide Printable Version";
            Button7.Visible = true;
            if (GridView1.Columns[14].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[14].Visible = false;
            }
            if (GridView1.Columns[15].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                GridView1.Columns[15].Visible = false;
            }

        }
        else
        {

            pnlgrid.ScrollBars = ScrollBars.Vertical;
            pnlgrid.Height = new Unit(250);

            Button1.Text = "Printable Version";
            Button7.Visible = false;
            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[14].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                GridView1.Columns[15].Visible = true;
            }

        }
    }
    protected void Button2_Click1(object sender, EventArgs e)
    {
        try
        {
            string strmaster = "SELECT * from ShippingManager  where CarrierMethod='" + ddlcarrierMethod.Text + "' and ShippingManagerID<>'" + ViewState["Id"] + "' ";
            SqlCommand cmdmaster = new SqlCommand(strmaster, con);
            SqlDataAdapter adpMaster = new SqlDataAdapter(cmdmaster);
            DataTable dtmaster = new DataTable();
            adpMaster.Fill(dtmaster);
            if (dtmaster.Rows.Count == 0)
            {
                string updateshimgr = "update  ShippingManager  set CarrierMethod='" + ddlcarrierMethod.Text + "' ,ShippingType='" + DropDownList1.SelectedValue + "',Country_ID='"+ddlcountry1.SelectedValue+"',State_ID='"+ddlState.SelectedValue+"',TrackingURL='"+TextBox1.Text+"',Protocol='"+DropDownList2.SelectedValue+"' where ShippingManagerID='" + ViewState["Id"] + "'   ";
                       
                SqlCommand cmdupadate = new SqlCommand(updateshimgr, con);
                if (cmdupadate.Connection.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdupadate.ExecuteNonQuery();
                con.Close();

                SqlDataAdapter dag1 = new SqlDataAdapter("select ShippingManagerID from ShippingManager where ShippingManagerID='" + ViewState["Id"] + "'", con);
                DataTable dtg1 = new DataTable();
                dag1.Fill(dtg1);
                ViewState["shipid"] = dtg1.Rows[0]["ShippingManagerID"].ToString();

                SqlCommand cmddelete = new SqlCommand("delete from ShippingmanagerStateDetail where ShippingManagerID='" + ViewState["shipid"].ToString() + "' ", con);
                if (cmddelete.Connection.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmddelete.ExecuteNonQuery();
                con.Close();

                foreach (GridViewRow gdr in GridView2.Rows)
                {
                    Label labelstate = (Label)gdr.FindControl("Label53");
                    CheckBox checkstate = (CheckBox)gdr.FindControl("CheckBox1");

                    if (checkstate.Checked == true)
                    {
                        SqlCommand cmdg = new SqlCommand("insert into ShippingmanagerStateDetail(ShippingManagerID,CountryID,StateID) values ('" + ViewState["shipid"].ToString() + "','" + ddlcountry1.SelectedValue + "','" + labelstate.Text + "')", con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmdg.ExecuteNonQuery();
                        con.Close();
                    }

                  
                }


                Label2.Visible = true;
                Label2.Text = "Record updated successfully";
                addinventoryroom.Visible = false;
                lbllegend.Text = "";
                btnaddroom.Visible = true;
                cancel();
                Button2.Visible = false;
                ImageButton1.Visible = true;
                GridView1.EditIndex = -1;
                FillGridView1();
            }
            else
            {
                Label2.Visible = true;
                Label2.Text = "Record already exists";
            }
        }
        catch (Exception a)
        {
            Label2.Visible = true;
            Label2.Text = "Error :" + a.Message;
        }
    }
    protected void ddlcountry1_SelectedIndexChanged(object sender, EventArgs e)
    {
      
      //  ddlState1.Items.Clear();

        

        if (ddlcountry1.SelectedIndex > 0)
        {
            SqlCommand cmdst = new SqlCommand("SELECT StateName, StateId, CountryId  FROM StateMasterTbl Where CountryId ='" + ddlcountry1.SelectedValue + "' Order by StateName ", con);
            SqlDataAdapter dast = new SqlDataAdapter(cmdst);
            DataTable dtst = new DataTable();
            dast.Fill(dtst);

            GridView2.DataSource = dtst;
            GridView2.DataBind();

            //ddlState1.DataSource = (DataSet)fillstate();
            //ddlState1.DataTextField = "StateName";
            //ddlState1.DataValueField = "StateId";
            //ddlState1.DataBind();
        }
        else
        {

        }
        if (GridView2.Rows.Count > 0)
        {
            panelgridstate.Visible = true;
        }
        else
        {
            panelgridstate.Visible = false;
        }
        //ddlState1.Items.Insert(0, "-Select-");
        //ddlState1.Items[0].Value = "0";
    }
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        GridView2.DataBind();
    }

    protected void HeaderChkbox1_CheckedChanged1(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)GridView2.HeaderRow.Cells[0].Controls[1];
        for (int i = 0; i < GridView2.Rows.Count; i++)
        {
            CheckBox ch = (CheckBox)GridView2.Rows[i].Cells[0].Controls[1];
            ch.Checked = chk.Checked;
        }
    }

    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        FillGridView1();
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
    
}
