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

public partial class GeneralShipSetting : System.Web.UI.Page
{
    SqlConnection con;
    int i;
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
            ViewState["sortOrder"] = "";
            Warehouse();
            ddlware_SelectedIndexChanged(sender, e);
            fillgrid12();
            
           
        }
    }
    protected void fillcountry()
    {
        ddlcountry.DataSource = (DataSet)fillddl();
        ddlcountry.DataTextField = "Country_Code";
        ddlcountry.DataValueField = "CountryId";
        ddlcountry.DataBind();

         
       
    }
   
    public void Warehouse()
    {
      
        DataTable dswh = ClsStore.SelectStorename();
        ddlware.DataSource = dswh;
        ddlware.DataTextField = "Name";
        ddlware.DataValueField = "WareHouseId";
        ddlware.DataBind();
        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();
        if (dteeed.Rows.Count > 0)
        {
            ddlware.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }
    }
    public DataSet fillddl()
    {

        string str22 = "SELECT  CountryId,CountryName as Country_Code  FROM  CountryMaster order by CountryName ";
        SqlCommand cmd = new SqlCommand(str22, con);


        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;

    }

    public DataSet fillddl2()
    {
        SqlCommand cmd = new SqlCommand("Sp_select_statemaster2", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@CountryId", ddlcountry.SelectedValue);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;

    }
    public DataSet fillddl3()
    {
        SqlCommand cmd = new SqlCommand("Sp_select_cityMaster2", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@StateId", ddlstate.SelectedValue);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;

    }
    protected void ddlware_SelectedIndexChanged(object sender, EventArgs e)
    {

        string str="Shipping Address";
        SqlDataAdapter adpt = new SqlDataAdapter("select * from CompanyWebsiteAddressMaster inner join AddressTypeMaster on AddressTypeMaster.AddressTypeMasterId=CompanyWebsiteAddressMaster.AddressTypeMasterId where CompanyWebsiteAddressMaster.CompanyWebsiteMasterId = '" + ddlware.SelectedValue + "' and AddressTypeMaster.Name= '" + str + "'", con);
        DataTable dtins = new DataTable();
        adpt.Fill(dtins);
        if (dtins.Rows.Count > 0)
        {
            ViewState["CompanyWebsiteAddressMasterId"] = dtins.Rows[0]["CompanyWebsiteAddressMasterId"].ToString();
        }

        if (dtins.Rows.Count > 0)
        {
            fillcountry();
            ddlcountry.SelectedIndex = ddlcountry.Items.IndexOf(ddlcountry.Items.FindByValue(dtins.Rows[0]["Country"].ToString()));
            if (ddlcountry.SelectedIndex > -1)
            {
                lblcountry.Text = ddlcountry.SelectedItem.Text;
            }

            ddlcountry_SelectedIndexChanged(sender, e);
            ddlstate.SelectedIndex = ddlstate.Items.IndexOf(ddlstate.Items.FindByValue(dtins.Rows[0]["State"].ToString()));
            if (ddlstate.SelectedIndex > -1)
            {
                lblstate.Text = ddlstate.SelectedItem.Text;
            }

            ddlstate_SelectedIndexChanged(sender, e);
            ddlcity.SelectedIndex = ddlcity.Items.IndexOf(ddlcity.Items.FindByValue(dtins.Rows[0]["City"].ToString()));
            if (ddlcity.SelectedIndex > -1)
            {
                lblcity.Text = ddlcity.SelectedItem.Text;
            }
            txtEndzip.Text = dtins.Rows[0]["Zip"].ToString();
            lblzip.Text = txtEndzip.Text;

            pnlchangeaddress.Visible = false;
            Panel2.Visible = true;
            Button2.Visible = true;
            Button2.Text = "Change";
                  
            
        }
        else
        {
            pnlchangeaddress.Visible = true;
            Panel2.Visible = false;
            lblzip.Text = "";
            fillcountry();
            ddlcountry_SelectedIndexChanged(sender, e);
            ddlstate_SelectedIndexChanged(sender, e);
            Button2.Visible = false;
            txtEndzip.Text = "";
            ViewState["CompanyWebsiteAddressMasterId"] = "";
           
        }
    }
    protected void ddlcountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlstate.DataSource = (DataSet)fillddl2();
        ddlstate.DataTextField = "StateName";
        ddlstate.DataValueField = "StateId";
        ddlstate.DataBind();

        ddlstate_SelectedIndexChanged(sender, e);

        

                 
    }
    protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlcity.DataSource = (DataSet)fillddl3();
        ddlcity.DataTextField = "CityName";
        ddlcity.DataValueField = "CityId";
        ddlcity.DataBind();
      
       
        
    }

    public void clean()
    {
        
       
        txtHndlingText.Text = "";
        txtHndlingText0.Text = "";
        CheckBox1.Checked = false;
        CheckBox2.Checked = false;
        chkhandlingcharge.Checked = false;
        pnlhandlingtext.Visible = false;
        
    }

    protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (ddlcountry.SelectedIndex == 0)
        {
            i = 1;
        }
        else
        { i = 0; }
    }
    protected void CustomValidator4_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (ddlstate.SelectedIndex == 0)
        {
            i = 1;
        }
        else
        { i = 0; }
    }
    protected void CustomValidator2_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (ddlcity.SelectedIndex == 0)
        {
            i = 1;
        }
        else
        { i = 0; }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        {
            try
            {
                if (ddlstate.SelectedIndex > -1)
                {

                    if (ddlcity.SelectedIndex > -1)
                    {
                        string str111 = "Select handling_text from General_Ship_Settings where  Whid='" + ddlware.SelectedValue + "'";
                        SqlCommand cmdstr111 = new SqlCommand(str111, con);
                        SqlDataAdapter dastr111 = new SqlDataAdapter(cmdstr111);
                        DataTable dtstr111 = new DataTable();
                        dastr111.Fill(dtstr111);
                        if (dtstr111.Rows.Count == 0)
                        {



                            string sqlin = "INSERT INTO  General_Ship_Settings" +
                            " (allow_different_shipping_addr " +
                            " ,allow_international_shipping_addr " +
                            " ,charge_per_order " +
                            " ,show_handling_text " +
                            " ,handling_text " +
                            " ,select_shipping_option " +
                            " ,shipping_orgin_city " +
                            " ,shipping_orgin_zip_code " +
                            " ,shipping_orgin_state " +
                            " ,shipping_orgin_country " +
                            " ,Country_ID " +
                            " ,State_ID,compid,Whid) " +
                            " VALUES " +
                            "   ('" + Convert.ToBoolean(CheckBox1.Checked) + "','" + Convert.ToBoolean(CheckBox2.Checked) + "', " +
                            "   '" + Convert.ToDecimal(txtHndlingText0.Text) + "','" + Convert.ToBoolean(chkhandlingcharge.Checked) + "', " +
                            "   '" + txtHndlingText.Text + "','1', " +
                            "   '" + ddlcity.SelectedValue + "','" + txtEndzip.Text + "', '" + ddlstate.SelectedValue + "','" + ddlcountry.SelectedValue + "', " +
                            "   '" + ddlcountry.SelectedValue + "','" + ddlstate.SelectedValue + "','" + Session["comid"] + "','" + ddlware.SelectedValue + "')";

                            SqlCommand cmd = new SqlCommand(sqlin, con);

                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmd.ExecuteNonQuery();
                            con.Close();
                            string spc1 = "select * from ShippingGeneralSetting where Whid='" + ddlware.SelectedValue + "'";
                            SqlDataAdapter ad = new SqlDataAdapter(spc1, con);
                            DataTable dtp = new DataTable();
                            ad.Fill(dtp);
                            if (dtp.Rows.Count == 0)
                            {
                                string genesett = "INSERT INTO ShippingGeneralSetting(Whid,allow_different_shipping_addr,allow_international_shipping_addr)values('" + ddlware.SelectedValue + "','" + CheckBox1.Checked + "','" + CheckBox2.Checked + "')";
                                SqlCommand cmdgen = new SqlCommand(genesett, con);

                                if (con.State.ToString() != "Open")
                                {
                                    con.Open();
                                }
                                cmdgen.ExecuteNonQuery();
                                con.Close();
                            }
                            Label1.Visible = true;
                            Label1.Text = "Record inserted successfully";
                            updateaddress();
                            clean();
                            pnladd.Visible = false;
                            lbladd.Text = "";
                            btnadd.Visible = true;
                            Warehouse();
                            ddlware_SelectedIndexChanged(sender, e);
                        }
                        else
                        {
                            Label1.Visible = true;
                            Label1.Text = "Record already exists";
                        }
                    }
                    else
                    {
                        Label1.Visible = true;
                        Label1.Text = "Please select city name";


                    }

                }
                else
                {
                    Label1.Visible = true;
                    Label1.Text = "Please select state name";

                }
               

            }
            catch (Exception ex)
            {

                Label1.Visible = true;
                Label1.Text = "Error:" + ex + " ";

            }
            finally { 
                fillgrid12(); 
                }
        }
    }


    protected void fillgrid12()
    {
        string strgir = "SELECT distinct Warehousemaster.Name,General_Ship_Settings.*,case when(General_Ship_Settings.allow_different_shipping_addr='1') then 'Active' else 'Inactive' end as Shipaddresslbl,case when(General_Ship_Settings.allow_international_shipping_addr='1') then 'Active' else 'Inactive' end as Internationaddresslbl,CountryMaster.CountryName,  StateMasterTbl.StateName,CityMasterTbl.CityName	FROM General_Ship_Settings inner join Warehousemaster on Warehousemaster.WarehouseId=General_Ship_Settings.Whid inner join CountryMaster on General_Ship_Settings.shipping_orgin_country = cast(CountryMaster.CountryId as nvarchar)   inner join StateMasterTbl on General_Ship_Settings.shipping_orgin_state = cast(StateMasterTbl.StateId as nvarchar)  inner join CityMasterTbl on General_Ship_Settings.shipping_orgin_city = cast(CityMasterTbl.CityId as nvarchar) where General_Ship_Settings.compid='" + Session["comid"] + "'and Warehousemaster.Status='" + 1 + "' order by Warehousemaster.Name,CountryMaster.CountryName,StateMasterTbl.StateName,CityMasterTbl.CityName,General_Ship_Settings.shipping_orgin_zip_code,General_Ship_Settings.handling_text";
       
        SqlCommand cmdgir = new SqlCommand(strgir, con);
        SqlDataAdapter adpgir = new SqlDataAdapter(cmdgir);
        DataTable dtgir = new DataTable();
        adpgir.Fill(dtgir);
        if (dtgir.Rows.Count > 0)
        {

            DataView myDataView = new DataView();
            myDataView = dtgir.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }
            grdTempData.DataSource = dtgir;
            grdTempData.DataBind();
        }
        else
        {
            grdTempData.DataSource = null;
            grdTempData.DataBind();
        }
        lblcompany.Text = Session["Cname"].ToString();
        lblbusiness.Text = ddlware.SelectedItem.Text;
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

   
    protected void Button2_Click(object sender, EventArgs e)
    {
        clean();    
    
        Button1.Visible = false;
        ImageButton1.Visible = true;
        pnladd.Visible = false;
        lbladd.Text = "";
        btnadd.Visible = true;
        Panel2.Visible = true;
        Warehouse();
        ddlware_SelectedIndexChanged(sender, e);
      

        
    }
    protected void FillGrid()
    {
        string strGrd = "   SELECT ShippingMaster.Id, ShippingMaster.EndZip, ShippingMaster.StartZip, ShippingMaster.MinWeight, ShippingMaster.MaxWeight, ShippingMaster.WeightUnit,  " +
                        "   ShippingMaster.MinVolume, ShippingMaster.MaxVolume, ShippingMaster.VolUnit, ShippingMaster.Volumerate, ShippingMaster.WeightRate, ShippingMaster.FlatRate,  " +
                        "   CountryMaster.CountryName, StateMasterTbl.StateName, CityMasterTbl.CityName, ShippingMaster.StateId, ShippingMaster.CityId, ShippingMaster.CountryId " +
                        "   FROM  ShippingMaster LEFT OUTER JOIN " +
                        "   CityMasterTbl ON ShippingMaster.CityId = CityMasterTbl.CityId LEFT OUTER JOIN " +
                        "   StateMasterTbl ON ShippingMaster.StateId = StateMasterTbl.StateId LEFT OUTER JOIN " +
                        "   CountryMaster ON ShippingMaster.CountryId = CountryMaster.CountryId where ShippingMaster.compid = '" + compid + "' ";
        SqlCommand cmdGrd = new SqlCommand(strGrd, con);
        SqlDataAdapter adpGrd = new SqlDataAdapter(cmdGrd);
        DataTable dtGrd = new DataTable();
        adpGrd.Fill(dtGrd);
        if (dtGrd.Rows.Count > 0)
        {
            grdTempData.DataSource = dtGrd;
            grdTempData.DataBind();

        }
        else
        {
            grdTempData.DataSource = null;
            grdTempData.DataBind();
        }

        if (grdTempData.Rows.Count > 0)
        {
            foreach (GridViewRow bn in grdTempData.Rows)
            {
                Label lblid = (Label)bn.FindControl("lblid");
                Label lblminwht = (Label)bn.FindControl("lblMinWht");
                Label lblmaxwht = (Label)bn.FindControl("lblMaxWht");
                Label lblminvlm = (Label)bn.FindControl("lblMinVlm");
                Label lblmaxvlm = (Label)bn.FindControl("lblMaxVlm");
                Label lblwhtrate = (Label)bn.FindControl("lblWhtRate");
                Label lblvlmrate = (Label)bn.FindControl("lblVlmRate");
                Label lblflaterate = (Label)bn.FindControl("lblFlateRate");
                Label lblstrzp = (Label)bn.FindControl("lblstrzp");
                Label lblendzp = (Label)bn.FindControl("lblendzp");
                TextBox txtminwht = (TextBox)bn.FindControl("txtMinWht");
                TextBox txtmaxwht = (TextBox)bn.FindControl("txtMaxWht");
                TextBox txtminvlm = (TextBox)bn.FindControl("txtMinVlm");
                TextBox txtmaxvlm = (TextBox)bn.FindControl("txtMaxVlm");
                TextBox txtwhtrate = (TextBox)bn.FindControl("txtWhtRate");
                TextBox txtvlmrate = (TextBox)bn.FindControl("txtVlmRate");
                TextBox txtflaterate = (TextBox)bn.FindControl("txtFlateRate");
                TextBox txtstzp = (TextBox)bn.FindControl("txtstrzp");
                TextBox txtenzp = (TextBox)bn.FindControl("txtendzp");
                DropDownList ddlCountry = (DropDownList)bn.FindControl("ddlGrdCountry");
                DropDownList ddlState = (DropDownList)bn.FindControl("ddlGrdState");
                DropDownList ddlcity = (DropDownList)bn.FindControl("ddlGrdCity");
                DropDownList ddlwhtunit = (DropDownList)bn.FindControl("ddlWhtUnit");
                DropDownList ddlvlmunit = (DropDownList)bn.FindControl("ddlVlmUnit");
                SqlCommand cmdGrd1 = new SqlCommand(strGrd + " Where ShippingMaster.Id='" + lblid.Text + "' ", con);
                SqlDataAdapter adpGrd1 = new SqlDataAdapter(cmdGrd1);
                DataTable dtGrd1 = new DataTable();
                adpGrd1.Fill(dtGrd1);
                ddlCountry.Items.Clear();
                ddlState.Items.Clear();
                ddlcity.Items.Clear();

                string str = "Select CountryName,CountryId from CountryMaster order by CountryName";
                SqlCommand cmd = new SqlCommand(str);
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataSet ds = new DataSet();

                da.Fill(ds, "CountryMaster");


                ddlCountry.DataSource = ds;
                ddlCountry.DataTextField = "CountryName";
                ddlCountry.DataValueField = "CountryId";
                ddlCountry.DataBind();
                ddlCountry.Items.Insert(0, "All");
                ddlCountry.SelectedItem.Value = "0";
                ddlState.Items.Insert(0, "All");
                ddlState.SelectedItem.Value = "0";
                ddlcity.Items.Insert(0, "All");
                ddlcity.SelectedItem.Value = "0";
                if (dtGrd1.Rows[0]["CountryId"].ToString() != "")
                {
                    ddlCountry.SelectedIndex = ddlCountry.Items.IndexOf(ddlCountry.Items.FindByValue(dtGrd1.Rows[0]["CountryId"].ToString()));
                }

                if (ddlCountry.SelectedIndex > 0)
                {

                    ddlState.Items.Clear();
                    ddlcity.Items.Clear();

                    string str45090 = "SELECT     StateName  ,StateId " +
                            " FROM  StateMasterTbl where CountryId='" + ddlCountry.SelectedValue + "' " +
                            " Order By StateName";



                    //"Select StateName,StateId from StateMasterTbl";
                    SqlCommand cmd4590 = new SqlCommand(str45090, con);

                    SqlDataAdapter da90 = new SqlDataAdapter(cmd4590);
                    //da.SelectCommand = cmd;
                    DataTable ds90 = new DataTable();

                    da90.Fill(ds90);
                    ddlState.DataSource = ds90;
                    ddlState.DataTextField = "StateName";
                    ddlState.DataValueField = "StateId";
                    ddlState.DataBind();

                    ddlState.Items.Insert(0, "-Select-");
                    ddlState.SelectedItem.Value = "0";
                    ddlcity.Items.Insert(0, "-Select-");
                    ddlcity.SelectedItem.Value = "0";
                    if (dtGrd1.Rows[0]["StateId"].ToString() != "")
                    {
                        ddlState.SelectedIndex = ddlState.Items.IndexOf(ddlState.Items.FindByValue(dtGrd1.Rows[0]["StateId"].ToString()));
                    }
                    //ddlstatechange_SelectedIndexChanged(sender, e);
                    if (ddlState.SelectedIndex > 0)
                    {

                        ddlcity.Items.Clear();


                        string str455 = "SELECT     CityName  ,CityId " +
                                " FROM  CityMasterTbl where StateId='" + ddlState.SelectedValue + "' " +
                                " Order By CityName";

                        //[CityName] ='" + cityname.Text + "' ,  " +
                        //" [StateId] = '" + ddlcountrystate.SelectedValue + "' WHERE [CityId] ='" + dk.ToString() + "'";


                        //"Select StateName,StateId from StateMasterTbl";
                        SqlCommand cmd45555 = new SqlCommand(str455, con);

                        SqlDataAdapter da5 = new SqlDataAdapter(cmd45555);
                        //da.SelectCommand = cmd;
                        DataTable ds5 = new DataTable();

                        da5.Fill(ds5);
                        ddlcity.DataSource = ds5;
                        ddlcity.DataTextField = "CityName";
                        ddlcity.DataValueField = "CityId";
                        ddlcity.DataBind();

                        ddlcity.Items.Insert(0, "-Select-");
                        ddlcity.SelectedItem.Value = "0";
                        if (dtGrd1.Rows[0]["CityId"].ToString() != "")
                        {
                            ddlcity.SelectedIndex = ddlcity.Items.IndexOf(ddlcity.Items.FindByValue(dtGrd1.Rows[0]["CityId"].ToString()));

                        }
                    }

                }
                else
                {
                    ddlState.Items.Clear();
                    ddlState.Items.Insert(0, "-Select-");
                    ddlState.SelectedItem.Value = "0";


                    ddlcity.Items.Clear();
                    ddlcity.Items.Insert(0, "-Select-");
                    ddlcity.SelectedItem.Value = "0";
                    //fillgrid();
                }

                string strUnit = "SELECT [UnitTypeId]      ,[Name]      ,[Status]  from [UnitTypeMaster]";
                SqlCommand cmdUnit = new SqlCommand(strUnit, con);

                SqlDataAdapter apdUnit = new SqlDataAdapter(cmdUnit);

                DataTable dtUnit = new DataTable();

                apdUnit.Fill(dtUnit);
                ddlwhtunit.DataSource = dtUnit;
                ddlwhtunit.DataTextField = "Name";
                ddlwhtunit.DataValueField = "UnitTypeId";
                ddlwhtunit.DataBind();

                if (dtGrd1.Rows[0]["WeightUnit"].ToString() != "")
                {
                    ddlwhtunit.SelectedIndex = ddlwhtunit.Items.IndexOf(ddlwhtunit.Items.FindByValue(dtGrd1.Rows[0]["WeightUnit"].ToString()));

                }


                string strVolume = "SELECT [VolumeUnitId]      ,[VolumeUnitName]  FROM  [VolumeUnitMaster]";
                SqlCommand cmdVolume = new SqlCommand(strVolume, con);

                SqlDataAdapter apdVolume = new SqlDataAdapter(cmdVolume);

                DataTable dtVolume = new DataTable();

                apdVolume.Fill(dtVolume);
                ddlvlmunit.DataSource = dtVolume;
                ddlvlmunit.DataTextField = "VolumeUnitName";
                ddlvlmunit.DataValueField = "VolumeUnitId";
                ddlvlmunit.DataBind();

                if (dtGrd1.Rows[0]["VolUnit"].ToString() != "")
                {
                    ddlvlmunit.SelectedIndex = ddlvlmunit.Items.IndexOf(ddlvlmunit.Items.FindByValue(dtGrd1.Rows[0]["VolUnit"].ToString()));

                }



            }

        }
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

        ddlSt.Items.Insert(0, "All");
        ddlSt.SelectedItem.Value = "0";


    }
    protected void FillgrdSTct(DropDownList ddlSt, DropDownList ddlCty)
    {
        if (ddlSt.SelectedIndex > 0)
        {
            string str455 = "SELECT     CityName  ,CityId " +
                       " FROM  CityMasterTbl where StateId='" + ddlSt.SelectedValue + "' " +
                       " Order By CityName";


            SqlCommand cmd45555 = new SqlCommand(str455, con);

            SqlDataAdapter da5 = new SqlDataAdapter(cmd45555);

            DataTable ds5 = new DataTable();

            da5.Fill(ds5);
            ddlCty.DataSource = ds5;
            ddlCty.DataTextField = "CityName";
            ddlCty.DataValueField = "CityId";
            ddlCty.DataBind();
        }
        ddlCty.Items.Insert(0, "All");
        ddlCty.SelectedItem.Value = "0";


    }

    protected void ddlGrdCountry_SelectedIndexChanged1(object sender, EventArgs e)
    {


        int ihk = grdTempData.EditIndex;
        DropDownList ddcountry = (DropDownList)grdTempData.Rows[grdTempData.EditIndex].FindControl("ddlGrdCountry");

        DropDownList ddstate = (DropDownList)grdTempData.Rows[grdTempData.EditIndex].FindControl("ddlGrdState");
        DropDownList ddcity = (DropDownList)grdTempData.Rows[grdTempData.EditIndex].FindControl("ddlGrdCity");
        FillGrdCntST(ddcountry, ddstate);

       

    }
    protected void ddlGrdState_SelectedIndexChanged132(object sender, EventArgs e)
    {


        DropDownList ddcountry = (DropDownList)grdTempData.Rows[grdTempData.EditIndex].FindControl("ddlGrdCountry");

        DropDownList ddstate = (DropDownList)grdTempData.Rows[grdTempData.EditIndex].FindControl("ddlGrdState");
        DropDownList ddcity = (DropDownList)grdTempData.Rows[grdTempData.EditIndex].FindControl("ddlGrdCity");
        FillgrdSTct(ddstate, ddcity);

        
    }
    protected void grdTempData_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Label1.Text = "";
        grdTempData.SelectedIndex = Convert.ToInt32(grdTempData.DataKeys[e.NewEditIndex].Value.ToString());

        ViewState["editid"] = grdTempData.DataKeys[e.NewEditIndex].Value.ToString();

        string str = "select * from General_Ship_Settings where general_ship_settings_id='"+ grdTempData.SelectedIndex +"'";
        SqlCommand cmd = new SqlCommand(str,con);
        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            pnladd.Visible = true;
            lbladd.Text = "Edit Handling Charge and General Shipping Option";
            btnadd.Visible = false;
            Panel2.Visible = true;

            Button2.Text = "Change";

            pnlchangeaddress.Visible = false;
          
            Warehouse();

            ddlware.SelectedIndex = ddlware.Items.IndexOf(ddlware.Items.FindByValue(dt.Rows[0]["Whid"].ToString()));
            fillcountry();

            ddlcountry.SelectedIndex = ddlcountry.Items.IndexOf(ddlcountry.Items.FindByValue(dt.Rows[0]["Country_ID"].ToString()));

            lblcity.Text = ddlcountry.SelectedItem.Text;

            ddlcountry_SelectedIndexChanged(sender, e);
            ddlstate.SelectedIndex = ddlstate.Items.IndexOf(ddlstate.Items.FindByValue(dt.Rows[0]["State_ID"].ToString()));

            lblstate.Text = ddlstate.SelectedItem.Text;

            ddlstate_SelectedIndexChanged(sender, e);
            ddlcity.SelectedIndex = ddlcity.Items.IndexOf(ddlcity.Items.FindByValue(dt.Rows[0]["shipping_orgin_city"].ToString()));

            lblcity.Text = ddlcity.SelectedItem.Text;
           

            if (Convert.ToBoolean(dt.Rows[0]["show_handling_text"].ToString()) == true)
            {
                pnlhandlingtext.Visible = true;
                txtHndlingText.Text = dt.Rows[0]["handling_text"].ToString();
            }
            txtHndlingText0.Text = dt.Rows[0]["charge_per_order"].ToString();

            txtEndzip.Text = dt.Rows[0]["shipping_orgin_zip_code"].ToString();

            lblzip.Text = txtEndzip.Text;

            CheckBox1.Checked = Convert.ToBoolean(dt.Rows[0]["allow_different_shipping_addr"].ToString());
            CheckBox2.Checked = Convert.ToBoolean(dt.Rows[0]["allow_international_shipping_addr"].ToString());
            chkhandlingcharge.Checked = Convert.ToBoolean(dt.Rows[0]["show_handling_text"].ToString());
            ImageButton1.Visible = false;
            Button1.Visible = true;

        }
       
    }
    protected void grdTempData_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        fillgrid12();
    }
    protected void grdTempData_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //yes_Click(sender, e);
        ViewState["deleid"] = grdTempData.DataKeys[e.RowIndex].Value.ToString();
        string strdel = "delete from General_Ship_Settings where general_ship_settings_id='" + ViewState["deleid"] + "' ";
            SqlCommand cmddel = new SqlCommand(strdel, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmddel.ExecuteNonQuery();
            con.Close();

            grdTempData.EditIndex = -1;
            fillgrid12();
            Label1.Visible = true;
            Label1.Text = "Record deleted successfully";
    }
    
  
    protected void ImageButton7_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/ShoppingCart/Admin/wzFreeShippingRuleAddManage.aspx");
    }
    
    protected void yes_Click(object sender, EventArgs e)
    {
        
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
       

    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {


       
    }
    
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
       
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
       

    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        try
        {
            if (ddlstate.SelectedIndex > -1)
            {
                if (ddlcity.SelectedIndex > -1)
                {
                    string str111 = "Select handling_text from General_Ship_Settings where handling_text='" + txtHndlingText.Text + "'  and Whid='" + ddlware.SelectedValue + "' and general_ship_settings_id<>'" + ViewState["editid"] + "' ";
                    SqlCommand cmdstr111 = new SqlCommand(str111, con);
                    SqlDataAdapter dastr111 = new SqlDataAdapter(cmdstr111);
                    DataTable dtstr111 = new DataTable();
                    dastr111.Fill(dtstr111);
                    if (dtstr111.Rows.Count == 0)
                    {

                        string strupdate = " UPDATE [General_Ship_Settings] " +
                            "  SET allow_different_shipping_addr = '" + Convert.ToBoolean(CheckBox1.Checked) + "'  " +
                            "  ,allow_international_shipping_addr ='" + Convert.ToBoolean(CheckBox2.Checked) + "' " +
                            "  ,charge_per_order ='" + txtHndlingText0.Text + "' " +
                            "  ,show_handling_text = '" + Convert.ToBoolean(chkhandlingcharge.Checked) + "' " +
                            "  ,handling_text = '" + txtHndlingText.Text + "' " +
                            "  ,select_shipping_option = '1'  " +
                            "  ,shipping_orgin_city = '" + ddlcity.SelectedValue + "' " +
                            "  ,shipping_orgin_zip_code = '" + txtEndzip.Text + "'  " +
                            "  ,shipping_orgin_state = '" + ddlstate.SelectedValue + "' " +
                            "  ,shipping_orgin_country ='" + ddlcountry.SelectedValue + "'  " +
                            "  ,Country_ID = '" + ddlcountry.SelectedValue + "' " +
                            "  ,State_ID = '" + ddlstate.SelectedValue + "' ,Whid='" + ddlware.SelectedValue + "' " +
                            "  WHERE general_ship_settings_id='" + ViewState["editid"] + "' ";
                        SqlCommand cmdupadate = new SqlCommand(strupdate, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmdupadate.ExecuteNonQuery();
                        con.Close();

                        updateaddress();
                        Label1.Visible = true;
                        Label1.Text = "Record updated successfully";

                        grdTempData.EditIndex = -1;
                        fillgrid12();

                        clean();
                        ImageButton1.Visible = true;
                        Button1.Visible = false;
                        pnladd.Visible = false;
                        lbladd.Text = "";
                        btnadd.Visible = true;
                        Warehouse();
                        ddlware_SelectedIndexChanged(sender, e);
                    }
                    else
                    {
                        Label1.Visible = true;
                        Label1.Text = "Record already exists";
                    }
                }
                else
                {
                    Label1.Visible = true;
                    Label1.Text = "please select city name";

                }
            }
            else
            {
                Label1.Visible = true;
                Label1.Text = "please select state name";
            }
           
        }
        catch (Exception ty)
        {
            Label1.Visible = true;
            Label1.Text = "Error ;" + ty.Message; ;
        }
    }
    protected void grdTempData_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 1;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Shipping Origin Address";
            HeaderCell.ColumnSpan = 4;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.BackColor = System.Drawing.Color.FromArgb(65, 98, 113);
            HeaderCell.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 12;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 9;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;


            HeaderGridRow.Cells.Add(HeaderCell);

            grdTempData.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }

    protected void chkhandlingcharge_CheckedChanged(object sender, EventArgs e)
    {
        if (chkhandlingcharge.Checked == true)
        {
            pnlhandlingtext.Visible = true;
        }
        else
        {
            pnlhandlingtext.Visible = false;
            txtHndlingText.Text = "";
        }
    }
    protected void Button2_Click1(object sender, EventArgs e)
    {
        if (Button2.Text == "Change")
        {
            pnlchangeaddress.Visible = true;
            Panel2.Visible = false;

            Button2.Text = "Update";
        }
        else if (Button2.Text == "Update")
        {
           
            

            if (ViewState["CompanyWebsiteAddressMasterId"]!=null)
            {
                if (ddlstate.SelectedIndex > -1)
                {
                    if (ddlcity.SelectedIndex > -1)
                    {
                        string strup = "Update CompanyWebsiteAddressMaster set City = '" + ddlcity.SelectedValue + "',State = '" + ddlstate.SelectedValue + "',Country = '" + ddlcountry.SelectedValue + "' ,Zip = '" + txtEndzip.Text + "' where CompanyWebsiteAddressMasterId = '" + ViewState["CompanyWebsiteAddressMasterId"] + "'";
                        SqlCommand cmd = new SqlCommand(strup, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmd.ExecuteNonQuery();
                        con.Close();

                        ddlware_SelectedIndexChanged(sender, e);

                        pnlchangeaddress.Visible = false;
                        Panel2.Visible = true;
                        Button2.Text = "Change";
                    }
                    else
                    {
                        Label1.Visible = true;
                        Label1.Text = "Please select city name";
                    }
                }
                else
                {
                    Label1.Visible = true;
                    Label1.Text = "Please select state name";

                }
               
            }
           
        }
        
    }

    protected void btncancel0_Click(object sender, EventArgs e)
    {
        if (btncancel0.Text == "Printable Version")
        {
            Panel1.ScrollBars = ScrollBars.None;
            Panel1.Height = new Unit("100%");

            btncancel0.Text = "Hide Printable Version";
            Button7.Visible = true;
            if (grdTempData.Columns[12].Visible == true)
            {
                ViewState["editHide"] = "tt";
                grdTempData.Columns[12].Visible = false;
            }
            if (grdTempData.Columns[13].Visible == true)
            {
                ViewState["deleteHide"] = "tt";
                grdTempData.Columns[13].Visible = false;
            }
        }
        else
        {
            //pnlgrid.ScrollBars = ScrollBars.Vertical;
            //pnlgrid.Height = new Unit(150);

            btncancel0.Text = "Printable Version";
            Button7.Visible = false;
            if (ViewState["editHide"] != null)
            {
                grdTempData.Columns[12].Visible = true;
            }
            if (ViewState["deleteHide"] != null)
            {
                grdTempData.Columns[13].Visible = true;
            }
        }
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        if (pnladd.Visible == false)
        {
            pnladd.Visible = true;
            lbladd.Text = "Add Handling Charge and General Shipping Option";
            btnadd.Visible = false;
        }
        else
        {
            pnladd.Visible = false;
            lbladd.Text = "";
            btnadd.Visible = true;
        }
        Label1.Text = "";
       
        
        chkhandlingcharge.Checked = false;
        pnlhandlingtext.Visible = false;
        
    }
    protected void ddlcity_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblcity.Text = ddlcity.SelectedItem.Text;
    }
    protected void updateaddress()
    {
        
            string str = "Shipping Address";
            SqlDataAdapter adpt = new SqlDataAdapter("select * from CompanyWebsiteAddressMaster inner join AddressTypeMaster on AddressTypeMaster.AddressTypeMasterId=CompanyWebsiteAddressMaster.AddressTypeMasterId where CompanyWebsiteAddressMaster.CompanyWebsiteMasterId = '" + ddlware.SelectedValue + "' and AddressTypeMaster.Name= '" + str + "'", con);
            DataTable dtins = new DataTable();
            adpt.Fill(dtins);
            if (dtins.Rows.Count > 0)
            {
                ViewState["CompanyWebsiteAddressMasterId"] = dtins.Rows[0]["CompanyWebsiteAddressMasterId"].ToString();
            }


            if (ViewState["CompanyWebsiteAddressMasterId"] != null)
            {
                string strup = "Update CompanyWebsiteAddressMaster set City = '" + ddlcity.SelectedValue + "',State = '" + ddlstate.SelectedValue + "',Country = '" + ddlcountry.SelectedValue + "' ,Zip = '" + txtEndzip.Text + "' where CompanyWebsiteAddressMasterId = '" + ViewState["CompanyWebsiteAddressMasterId"] + "' ";
                SqlCommand cmd = new SqlCommand(strup, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                con.Close();
              
               
            }

        
    }
}
