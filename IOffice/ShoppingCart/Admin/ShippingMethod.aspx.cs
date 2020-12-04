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

public partial class ShoppingCart_Admin_ShippingMethod : System.Web.UI.Page
{
    string compid;
   // SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString"].ConnectionString);
    SqlConnection con=new SqlConnection(PageConn.connnn);
    public  static string countryId = "0";
    public static string stateId = "0";
    public static int Idno = 0;
    int i;
    protected void Page_Load(object sender, EventArgs e)
    {
        //PageConn pgcon = new PageConn();
        //con = pgcon.dynconn;
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();
        compid = Session["Comid"].ToString();
        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();

      
        Page.Title = pg.getPageTitle(page);
     
        Label1.Text = "";
        Label1.Visible = false;
        if (!IsPostBack)
        {
            lblCompany.Text = Convert.ToString(Session["Cname"]);
            lblCn.Text = Convert.ToString(Session["Cname"]);
            DataTable ds = ClsStore.SelectStorename();
            ddlWarehouse.DataSource = ds;
            ddlWarehouse.DataTextField = "Name";
            ddlWarehouse.DataValueField = "WareHouseId";
            ddlWarehouse.DataBind();


            DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

            if (dteeed.Rows.Count > 0)
            {
                ddlWarehouse.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
            }
            string spc11 = "select * from ShippingMethodNew where WarehouseId='" + ddlWarehouse.SelectedValue + "'";
            SqlDataAdapter ads = new SqlDataAdapter(spc11, con);
            DataTable dtps = new DataTable();
            ads.Fill(dtps);
            string str11 = "";

            if (dtps.Rows.Count == 0)
            {
                str11 = " INSERT INTO  ShippingMethodNew ([WarehouseId]," +
              " [RealTimeShipping],[CustomShipping])    " +
                         " VALUES('" + ddlWarehouse.SelectedValue + "','1', " +
                         " '0') ";
                SqlCommand cmd11 = new SqlCommand(str11, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd11.ExecuteNonQuery();

                con.Close();


            }
            else
            {
                if (Convert.ToString(dtps.Rows[0]["CustomShipping"]) == "True")
                {
                    RadioButtonList1.SelectedIndex = 1;
                }
                else
                {
                    RadioButtonList1.SelectedIndex = 0;
                }
            }
          RadioButtonList1_SelectedIndexChanged2(sender,e);

        }
    }
    protected void btnAddFreeShippingRule_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((Button)sender).Parent.Parent as GridViewRow;
        Idno = row.RowIndex;
        string shipmenid = grdTempData.DataKeys[Idno].Value.ToString();
        CheckBox chkbus = (CheckBox)grdTempData.Rows[Idno].FindControl("chkbus");
        Label lblid = (Label)grdTempData.Rows[Idno].FindControl("lblid");
        TextBox lblaccesskey = (TextBox)grdTempData.Rows[Idno].FindControl("lblaccesskey");
        TextBox lblUsername = (TextBox)grdTempData.Rows[Idno].FindControl("lblUsername");
        TextBox lblpassword = (TextBox)grdTempData.Rows[Idno].FindControl("lblpassword");
        TextBox lblmerchant = (TextBox)grdTempData.Rows[Idno].FindControl("lblmerchant");
        TextBox lblaccname = (TextBox)grdTempData.Rows[Idno].FindControl("lblaccname");
        Button btnadd = (Button)grdTempData.Rows[Idno].FindControl("btnadd");

        RequiredFieldValidator Reqfieldusername = (RequiredFieldValidator)grdTempData.Rows[Idno].FindControl("Reqfieldusername");
        RequiredFieldValidator Reqfieldpass = (RequiredFieldValidator)grdTempData.Rows[Idno].FindControl("Reqfieldpass");

        RequiredFieldValidator ReqfieldMerc = (RequiredFieldValidator)grdTempData.Rows[Idno].FindControl("ReqfieldMerc");

        RequiredFieldValidator ReqfieldAcc = (RequiredFieldValidator)grdTempData.Rows[Idno].FindControl("ReqfieldAcc");

        RequiredFieldValidator ReqfieldAccesskey = (RequiredFieldValidator)grdTempData.Rows[Idno].FindControl("ReqfieldAccesskey");
     
        if (btnadd.Text == "Edit")
        {
            btnadd.Text = "Save";
            chkbus.Enabled = true;
            lblaccesskey.Enabled = true;
            lblpassword.Enabled = true;
            lblmerchant.Enabled = true;
            lblaccname.Enabled = true;
            lblUsername.Enabled = true;
            Reqfieldusername.Visible = true;
            Reqfieldpass.Visible = true;
            ReqfieldMerc.Visible = true;
            ReqfieldAcc.Visible = true;
            ReqfieldAccesskey.Visible = true;
        }
        else if (btnadd.Text == "Save")
        {
            if (lblid.Text == "")
            {
                if (chkbus.Checked == true)
                {
                   
                    string str = "insert into ShippingAccountInfo(ShippingManagerId,UserID,Password,MerchantId,AccountNo,AccessKey,Whid)values('" + shipmenid + "','" + lblUsername.Text + "','" + lblpassword.Text + "','" + lblmerchant.Text + "','" + lblaccname.Text + "','" + lblaccesskey.Text + "','" + ddlWarehouse.SelectedValue + "')";
                        SqlCommand cmd = new SqlCommand(str, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmd.ExecuteNonQuery();

                        con.Close();
                        Label1.Text = "Record inserted successfully";
                        Label1.Visible = true;


                }
            }
            else
            {
                string update = "";
                if (chkbus.Checked == true)
                {
                     update = " update ShippingAccountInfo set UserID='" + lblUsername.Text + "' , Password='" + lblpassword.Text + "', " +
                    " AccessKey='" + lblaccesskey.Text + "', MerchantId='" + lblmerchant.Text + "', AccountNo='" + lblaccname.Text + "' " +
                    " where ShippingAccountInfo='" + lblid.Text + "' ";
                }
                else
                {
                    update = "Delete from ShippingAccountInfo where ShippingAccountInfo='" + lblid.Text + "' ";
                }
                SqlCommand cmdup = new SqlCommand(update, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdup.ExecuteNonQuery();
                con.Close();
                Label1.Text = "Record updated successfully";
                Label1.Visible = true;
            }
            FillGriddata();
        }
       
    }
    protected void chkedit_chachedChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((CheckBox)sender).Parent.Parent as GridViewRow;

        Idno = row.RowIndex;

        CheckBox chkbus=(CheckBox)grdTempData.Rows[Idno].FindControl("chkbus");
        Label lblid = (Label)grdTempData.Rows[Idno].FindControl("lblid");
        TextBox lblaccesskey = (TextBox)grdTempData.Rows[Idno].FindControl("lblaccesskey");
        TextBox lblUsername = (TextBox)grdTempData.Rows[Idno].FindControl("lblUsername");
        TextBox lblpassword = (TextBox)grdTempData.Rows[Idno].FindControl("lblpassword");
        TextBox lblmerchant = (TextBox)grdTempData.Rows[Idno].FindControl("lblmerchant");
        TextBox lblaccname = (TextBox)grdTempData.Rows[Idno].FindControl("lblaccname");
        Button btnadd = (Button)grdTempData.Rows[Idno].FindControl("btnadd");
       
        RequiredFieldValidator Reqfieldusername = (RequiredFieldValidator)grdTempData.Rows[Idno].FindControl("Reqfieldusername");
        RequiredFieldValidator Reqfieldpass = (RequiredFieldValidator)grdTempData.Rows[Idno].FindControl("Reqfieldpass");

        RequiredFieldValidator ReqfieldMerc = (RequiredFieldValidator)grdTempData.Rows[Idno].FindControl("ReqfieldMerc");

        RequiredFieldValidator ReqfieldAcc = (RequiredFieldValidator)grdTempData.Rows[Idno].FindControl("ReqfieldAcc");

        RequiredFieldValidator ReqfieldAccesskey = (RequiredFieldValidator)grdTempData.Rows[Idno].FindControl("ReqfieldAccesskey");
     
        if (chkbus.Checked == false)
        {
            if (lblid.Text != "")
            {
                mod1.Show();

            }
            else
            {
                if (lblid.Text == "")
                {
                    ReqfieldMerc.Visible = false;
                    ReqfieldAccesskey.Visible = false;
                    ReqfieldAcc.Visible = false;
                    Reqfieldpass.Visible = false;
                    Reqfieldusername.Visible = false;
                    btnadd.Enabled = false;
                    lblaccesskey.Text = "";
                    lblpassword.Text = "";
                    lblmerchant.Text = "";
                    lblaccname.Text = "";
                    lblUsername.Text = "";
                    lblaccesskey.Enabled = false;
                    lblpassword.Enabled = false;
                    lblmerchant.Enabled = false;
                    lblaccname.Enabled = false;
                    lblUsername.Enabled = false;
                }
            }
        }
        else
        {
            ReqfieldMerc.Visible = true;
            ReqfieldAccesskey.Visible = true;
            ReqfieldAcc.Visible = true;
            Reqfieldpass.Visible = true;
            Reqfieldusername.Visible = true;
                 btnadd.Enabled = true;
                lblaccesskey.Enabled = true;
                lblpassword.Enabled = true;
                lblmerchant.Enabled = true;
                lblaccname.Enabled = true;
                lblUsername.Enabled = true;
            
        }
    }
    protected void FillGriddata()
    {



        DataTable dt1 = select(" Select Distinct Case When(ShippingAccountInfo IS NULL) then Cast('0' as bit) Else Cast('1' as bit) End as chk, ShippingManager.CarrierMethod,ShippingAccountInfo, ShippingManager.ShippingManagerID,UserID,Password,MerchantId,AccountNo,AccessKey from ShippingmanagerStateDetail Right join ShippingManager on ShippingManager.ShippingManagerID=ShippingmanagerStateDetail.ShippingManagerID and (StateID='" + stateId + "' or StateID IS NULL) Left join ShippingAccountInfo on ShippingAccountInfo.ShippingManagerId=ShippingManager.ShippingManagerID and  ShippingAccountInfo.Whid='" + ddlWarehouse.SelectedValue + "' " +
      " Left join ShippingMethodNew on ShippingMethodNew.WarehouseId=ShippingAccountInfo.Whid  where Country_ID='" + countryId + "'  and ShippingType='2'");

        if (dt1.Rows.Count > 0)
        {
            grdTempData.DataSource = dt1;
            grdTempData.DataBind();
        }
        else
        {
            grdTempData.DataSource = null;
            grdTempData.DataBind();
        }
        foreach (GridViewRow item in grdTempData.Rows)
        {
            CheckBox chkbus = (CheckBox)item.FindControl("chkbus");
            Label lblid = (Label)item.FindControl("lblid");
            TextBox lblaccesskey = (TextBox)item.FindControl("lblaccesskey");
            TextBox lblUsername = (TextBox)item.FindControl("lblUsername");
            TextBox lblpassword = (TextBox)item.FindControl("lblpassword");
            TextBox lblmerchant = (TextBox)item.FindControl("lblmerchant");
            TextBox lblaccname = (TextBox)item.FindControl("lblaccname");
            Button btnadd = (Button)item.FindControl("btnadd");
            Label lblppp = (Label)item.FindControl("lblppp");

           
            if (lblid.Text != "")
            {
                btnadd.Enabled = true;
                btnadd.Text = "Edit";
                chkbus.Enabled = false;
                string strqa = lblppp.Text;

                lblpassword.Attributes.Add("Value", strqa);
            }
            else
            {
                btnadd.Enabled = false;
                btnadd.Text = "Save";
                chkbus.Enabled = true;
            }
        }


    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string pera = "";
        string cois = "";
            int countory = 1;
        if (rdcountry.Checked == true)
        {
            countory = 1;
            cois = ddlcountry.SelectedValue;
            if (ddlcountry.SelectedIndex > 0)
            {
                pera = " and ShippingMaster.CountryId='" + ddlcountry.SelectedValue + "'";
            }
            if (ddlstate.SelectedIndex > 0)
            {
                pera += " and ShippingMaster.CityId='" + ddlstate.SelectedValue + "'";
            }
            if (ddlcity.SelectedIndex > 0)
            {
                pera += " and ShippingMaster.CountryId='" + ddlcity.SelectedValue + "'";
            }
        }
        else
        {
            countory = 2;
            cois = ddlpostalcountry.SelectedValue;
            if (ddlpostalcountry.SelectedIndex > 0)
            {
                pera = " and ShippingMaster.CountryId='" + ddlpostalcountry.SelectedValue + "'";
            }
            pera += " and ShippingMaster.StartZip='" + txtStartzip.Text + "'";
            pera += " and ShippingMaster.EndZip='" + txtEndzip.Text + "'";
        }
        try
        {
            DataTable dtstate = select("Select * from ShippingMaster where Whid='" + ddlWarehouse.SelectedValue + "'" + pera);
            if (dtstate.Rows.Count == 0)
            {

                int flag = 0;
                int Ratetype = 1;
                if (rdrate.Checked == true)
                {
                    Ratetype = 1;
                }
                else if (rdratevalume.Checked == true)
                {
                    Ratetype = 2;
                }
                else if (rdperorder.Checked == true)
                {

                    if (rdperorder.Checked == true)
                    {
                        if (rdflatamt.SelectedValue == "0")
                        {

                        }
                        else
                        {
                            if (Convert.ToDecimal(txtFlatRate.Text) > Convert.ToDecimal(100))
                            {
                                flag = 1;
                            }
                        }
                    }

                    Ratetype = 3;
                }
                if (flag == 0)
                {
                    SqlCommand cmdsp = new SqlCommand("Sp_Insert_ShippingMaster", con);
                    cmdsp.CommandType = CommandType.StoredProcedure;

                    cmdsp.Parameters.AddWithValue("@CountryId", cois);
                    cmdsp.Parameters.AddWithValue("@StateId", ddlstate.SelectedValue);
                    cmdsp.Parameters.AddWithValue("@CityId", ddlcity.SelectedValue);
                    cmdsp.Parameters.AddWithValue("@EndZip", txtEndzip.Text);
                    cmdsp.Parameters.AddWithValue("@StartZip", txtStartzip.Text);
                    cmdsp.Parameters.AddWithValue("@MinWeight", txtMinWeight.Text);
                    cmdsp.Parameters.AddWithValue("@MaxWeight", txtMaxWeight.Text);
                    cmdsp.Parameters.AddWithValue("@WeightUnit", ddlUnitType.SelectedValue);
                    cmdsp.Parameters.AddWithValue("@MinVolume", txtMinVolume.Text);
                    cmdsp.Parameters.AddWithValue("@MaxVolume", txtMaxVolume.Text);
                    cmdsp.Parameters.AddWithValue("@VolUnit", ddlVolumeUnit.SelectedValue);
                    cmdsp.Parameters.AddWithValue("@Volumerate", txtVolumerate.Text);
                    cmdsp.Parameters.AddWithValue("@WeightRate", txtWeightRate.Text);
                    cmdsp.Parameters.AddWithValue("@FlatRate", txtFlatRate.Text);
                    cmdsp.Parameters.AddWithValue("@compid", compid);
                    cmdsp.Parameters.AddWithValue("@Whid", ddlWarehouse.SelectedValue);
                    cmdsp.Parameters.AddWithValue("@Countryoption", countory);
                    cmdsp.Parameters.AddWithValue("@Rateoption", Ratetype);
                    cmdsp.Parameters.AddWithValue("@VolUnitVol1", ddlvolunit.SelectedValue);
                    cmdsp.Parameters.AddWithValue("@WeightUnit1", ddlunittype1.SelectedValue);
                    cmdsp.Parameters.AddWithValue("@PerorderType", rdflatamt.SelectedValue);

                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdsp.ExecuteNonQuery();
                    con.Close();

                    clean();




                    Label1.Visible = true;
                    Label1.Text = "Record inserted successfully";
                    FillGridGridView2();
                    masterpnl.Visible = false;
                    pnlbtnshow.Visible = false;
                    btnnew.Visible = true;
                }
                else
                {
                    Label1.Visible = true;
                    Label1.Text = "You cannot input a flat rate percentage greater then 100";
                }
            }
            else
            {
                Label1.Visible = true;
                Label1.Text = "Record already exists";

            }

           
        }


        catch (Exception ty)
        {
            Label1.Visible = true;
            Label1.Text = "Error : " + ty.Message;
        }

        finally
        {
            FillGriddata();
         
            FillGridGridView2();
        }
    }

 
    protected void ddlWarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label1.Text = "";
        fillcdata();
        RadioButtonList1_SelectedIndexChanged2(sender, e);
    }
    protected void fillcdata()
    {
        if (RadioButtonList1.SelectedIndex == 0)
        {
            DataTable dtstate = select("Select State,Country,StateName,CountryName from CountryMaster inner join CompanyWebsiteAddressMaster on CompanyWebsiteAddressMaster.Country=CountryMaster.CountryId inner join StateMasterTbl on StateMasterTbl.StateId=CompanyWebsiteAddressMaster.State  inner join CompanyWebsitMaster on CompanyWebsitMaster.CompanyWebsiteMasterId=CompanyWebsiteAddressMaster.CompanyWebsiteMasterId  inner join WarehouseMaster on WarehouseMaster.WarehouseId=CompanyWebsitMaster.Whid where CompanyWebsitMaster.Whid='" + ddlWarehouse.SelectedValue + "'");
            if (dtstate.Rows.Count > 0)
            {
                countryId = Convert.ToString(dtstate.Rows[0]["Country"]);
                stateId = Convert.ToString(dtstate.Rows[0]["State"]);
                //lblco.Text = "<" + Convert.ToString(dtstate.Rows[0]["CountryName"]) + ">";
                //lblst.Text = " <" + Convert.ToString(dtstate.Rows[0]["StateName"]) + ">";
                lblco.Text =  Convert.ToString(dtstate.Rows[0]["CountryName"]);
                lblst.Text =", "+ Convert.ToString(dtstate.Rows[0]["StateName"]);
   
            }
        }
        else
        {
            countryId = "0";
            stateId = "0";
        }
    }
    protected DataTable select(string str)
    {
        SqlDataAdapter ad = new SqlDataAdapter(str, con);
        DataTable dtp = new DataTable();
        ad.Fill(dtp);
        return dtp;
    }
    protected DataTable GrdTempData()
    {
        DataTable dtTemp = new DataTable();


        DataColumn prd = new DataColumn();
        prd.ColumnName = "Whid";
        prd.DataType = System.Type.GetType("System.String");
        prd.AllowDBNull = true;
        dtTemp.Columns.Add(prd);

        DataColumn ssCatId = new DataColumn();
        ssCatId.ColumnName = "Whname";
        ssCatId.DataType = System.Type.GetType("System.String");
        ssCatId.AllowDBNull = true;
        dtTemp.Columns.Add(ssCatId);

        //Category
        DataColumn catname = new DataColumn();
        catname.ColumnName = "ShipMethod";
        catname.DataType = System.Type.GetType("System.String");
        catname.AllowDBNull = true;
        dtTemp.Columns.Add(catname);


        DataColumn InvName = new DataColumn();
        InvName.ColumnName = "ShippingMethodNewId";
        InvName.DataType = System.Type.GetType("System.String");
        InvName.AllowDBNull = true;
        dtTemp.Columns.Add(InvName);


        return dtTemp;

    }
    protected void grdTempData_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdTempData.EditIndex = -1;
        FillGriddata();

    }
    protected void grdTempData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            grdTempData.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            // ViewState["id"] = GridView1.SelectedDataKey.Value;
        }
    }
    protected void grdTempData_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        FillGriddata();
        Label mid = (Label)grdTempData.Rows[e.RowIndex].FindControl("lblid");
        ViewState["id"] = mid.Text;
       // ModalPopupExtender1222.Show();
       
    }
 
  
    protected void grdTempData_Sorting(object sender, GridViewSortEventArgs e)
    {

    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void yes_Click(object sender, ImageClickEventArgs e)
    {


        string str = "Delete ShippingMethodNew where  ShippingMethodNewId='" + ViewState["id"] + "'  ";

        SqlCommand cmd = new SqlCommand(str, con);

        con.Open();
        cmd.ExecuteNonQuery();

        con.Close();
        Label1.Visible = true;
        Label1.Text = "Record Deleted successfully";


        FillGriddata();
       
    }
   
    protected void grdTempData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdTempData.PageIndex = e.NewPageIndex;

        FillGriddata();
    }
    protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/ShoppingCart/Admin/wzShippingManager.aspx");
    }
    protected void ImageButton6_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/ShoppingCart/Admin/wzShippingAccountInfo.aspx");
    }

    protected void RadioButtonList1_SelectedIndexChanged2(object sender, EventArgs e)
    {
        string optreal = "1";
        string optcust = "0";

        Label1.Text = "";
       
            if ( RadioButtonList1.SelectedIndex == 1)
            {

               optreal = "0";
                optcust = "1";
            }
            else
           
            {
                optreal = "1";
                optcust = "0";
            }
           
            
           string str11 = " Update ShippingMethodNew set RealTimeShipping='" + optreal + "',CustomShipping= '" + optcust + "' where   [WarehouseId]= '" + ddlWarehouse.SelectedValue + "'";

        
        SqlCommand cmd11 = new SqlCommand(str11, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd11.ExecuteNonQuery();

        con.Close();
        if (RadioButtonList1.SelectedIndex ==1)
        {
            pnl1.Visible = false;


          
         
            pnlop2.Visible = true;

            ddlcountry.Items.Clear();
            ddlcountry.DataSource = (DataSet)fillddl1();
            ddlcountry.DataTextField = "CountryName";
            ddlcountry.DataValueField = "CountryId";
            ddlcountry.DataBind();
            ddlcountry.Items.Insert(0, "All");
            ddlpostalcountry.Items.Clear();
            ddlpostalcountry.DataSource = (DataSet)fillddl1();
            ddlpostalcountry.DataTextField = "CountryName";
            ddlpostalcountry.DataValueField = "CountryId";
            ddlpostalcountry.DataBind();
            ddlpostalcountry.Items.Insert(0, "All");
            ddlpostalcountry.Items[0].Value = "0";
            ddlfiltercountory.Items.Clear();
            ddlfiltercountory.DataSource = (DataSet)fillddl1();
            ddlfiltercountory.DataTextField = "CountryName";
            ddlfiltercountory.DataValueField = "CountryId";
            ddlfiltercountory.DataBind();
            ddlfiltercountory.Items.Insert(0, "All");

            ddlstate.Items.Clear();
            ddlstate.Items.Insert(0, "All");
            ddlcity.Items.Clear();
            ddlcity.Items.Insert(0, "All");
            ddlcountry.Items[0].Value = "0";
            ddlstate.Items[0].Value = "0";
            ddlcity.Items[0].Value = "0";
            ddlfilterstate.Items.Clear();
            ddlfiltercity.Items.Clear();
            ddlfilterstate.Items.Insert(0, "All");
            ddlfiltercity.Items.Insert(0, "All");
            ddlfiltercountory.Items[0].Value = "0";
         
            ddlfilterstate.Items[0].Value = "0";
            ddlfiltercity.Items[0].Value = "0";
            string strVoltype = "SELECT VolumeUnitId ,VolumeUnitName FROM VolumeUnitMaster ";

            SqlCommand cmdVoltype = new SqlCommand(strVoltype, con);
            SqlDataAdapter adpVoltype = new SqlDataAdapter(cmdVoltype);
            DataTable dtVoltype = new DataTable();
            adpVoltype.Fill(dtVoltype);
            ddlVolumeUnit.DataSource = dtVoltype;
            ddlVolumeUnit.DataTextField = "VolumeUnitName";
            ddlVolumeUnit.DataValueField = "VolumeUnitId";
            ddlVolumeUnit.DataBind();

            ddlvolunit.DataSource = dtVoltype;
            ddlvolunit.DataTextField = "VolumeUnitName";
            ddlvolunit.DataValueField = "VolumeUnitId";
            ddlvolunit.DataBind();

            String strunitytpe = " SELECT UnitTypeId      ,Name  FROM UnitTypeMaster where UnitTypeId in ('1','2','3','4') ";
            SqlCommand cmdunitytpe = new SqlCommand(strunitytpe, con);
            SqlDataAdapter adpunitytpe = new SqlDataAdapter(cmdunitytpe);
            DataTable dtunitytpe = new DataTable();
            adpunitytpe.Fill(dtunitytpe);
            ddlUnitType.DataSource = dtunitytpe;
            ddlUnitType.DataTextField = "Name";
            ddlUnitType.DataValueField = "UnitTypeId";
            ddlUnitType.DataBind();

            ddlunittype1.DataSource = dtunitytpe;
            ddlunittype1.DataTextField = "Name";
            ddlunittype1.DataValueField = "UnitTypeId";
            ddlunittype1.DataBind();
            FillGridGridView2();
        }
        else
        {
            fillcdata();
            FillGriddata();
            pnl1.Visible = true;
            pnlop2.Visible = false;
           
          
        
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
  
    protected void ddlcountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlstate.Items.Clear();
        ddlstate.DataSource = (DataSet)fillddl2();
        ddlstate.DataTextField = "StateName";
        ddlstate.DataValueField = "StateId";
        ddlstate.DataBind();
        ddlstate.Items.Insert(0, "ALL");
        //ddlcountry.Items[0].Value = "0";
        ddlstate.Items[0].Value = "0";
        ddlstate_SelectedIndexChanged(sender, e);

    }
    protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlcity.Items.Clear();
        ddlcity.DataSource = (DataSet)fillddl3();
        ddlcity.DataTextField = "CityName";
        ddlcity.DataValueField = "CityId";
        ddlcity.DataBind();
        ddlcity.Items.Insert(0, "ALL");
        // ddlcountry.Items[0].Value = "0";
        //  ddlstate.Items[0].Value = "0";
        ddlcity.Items[0].Value = "0";
    }
    public DataSet fillddl1()
    {
        SqlCommand cmd = new SqlCommand("Sp_select_CountryMaster", con);
        cmd.CommandType = CommandType.StoredProcedure;

        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;

    }
    public DataSet fillddlco()
    {
        SqlCommand cmd = new SqlCommand("Sp_select_statemaster2", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@CountryId", ddlfiltercountory.SelectedValue);
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
    public DataSet fillddl3sta()
    {
        SqlCommand cmd = new SqlCommand("Sp_select_cityMaster2", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@StateId", ddlfilterstate.SelectedValue);
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
    public void clean()
    {
        ddlcity.SelectedIndex = 0;
        ddlcountry.SelectedIndex = 0;
        ddlstate.SelectedIndex = 0;
        txtEndzip.Text = "0";
        txtFlatRate.Text = "0";
        txtMaxVolume.Text = "0";
        txtMaxWeight.Text = "0";
        txtMinVolume.Text = "0";
        txtMinWeight.Text = "0";
        txtStartzip.Text = "0";
        txtVolumerate.Text = "0";
            
        txtWeightRate.Text = "0";
            


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
    protected void FillGridGridView2()
    {
        lblbusiness.Text = ddlWarehouse.SelectedItem.Text;
        string pera = "";
        if (ddlfiltercountory.SelectedIndex > 0)
        {
            pera = " and ShippingMaster.CountryId='" + ddlfiltercountory.SelectedValue + "'";
        }
        if (ddlfilterstate.SelectedIndex > 0)
        {
            pera += " and ShippingMaster.StateId='" + ddlfilterstate.SelectedValue + "'";
        }
        if (ddlfiltercity.SelectedIndex > 0)
        {
            pera += " and ShippingMaster.CityId='" + ddlfiltercity.SelectedValue + "'";
        }
        string strGrd = " SELECT distinct  ShippingMaster.Id, ShippingMaster.EndZip, ShippingMaster.StartZip, ShippingMaster.MinWeight, ShippingMaster.MaxWeight, ShippingMaster.WeightUnit,  " +
                  "     ShippingMaster.MinVolume, ShippingMaster.MaxVolume, ShippingMaster.VolUnit, ShippingMaster.Volumerate, ShippingMaster.WeightRate,Case When(PerorderType='0') then '$' else '' End+''+ ShippingMaster.FlatRate+''+Case When(PerorderType='1') then '%' else '' End as FlatRate, " +
                  "   Case When (CountryMaster.CountryName IS NULL) then'All' else  CountryMaster.CountryName End CountryName ,Case When ( StateMasterTbl.StateName IS NULL) Then 'All' else StateMasterTbl.StateName end StateName,Warehousemaster.name,ShippingMaster.Whid,Case When (CityMasterTbl.CityName IS NULL) then 'All' Else CityMasterTbl.CityName End CityName, ShippingMaster.StateId, ShippingMaster.CityId, ShippingMaster.CountryId " +
                 " FROM    Warehousemaster inner join ShippingMaster on Warehousemaster.WarehouseId=ShippingMaster.Whid LEFT OUTER JOIN " +
                     " CityMasterTbl ON ShippingMaster.CityId = CityMasterTbl.CityId LEFT OUTER JOIN " +
                     " StateMasterTbl ON ShippingMaster.StateId = StateMasterTbl.StateId LEFT join " +
                     " CountryMaster ON ShippingMaster.CountryId = CountryMaster.CountryId where ShippingMaster.compid='" + compid + "'and [ShippingMaster].Whid='" + ddlWarehouse.SelectedValue + "'  " + pera;
        SqlCommand cmdGrd = new SqlCommand(strGrd, con);
        SqlDataAdapter adpGrd = new SqlDataAdapter(cmdGrd);
        DataTable dtGrd = new DataTable();
        adpGrd.Fill(dtGrd);
        if (dtGrd.Rows.Count > 0)
        {
            GridView2.DataSource = dtGrd;
            GridView2.DataBind();

        }
        else
        {
            GridView2.DataSource = null;
            GridView2.DataBind();
        }

        
    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit" || e.CommandName == "View")
        {
            if (e.CommandName == "Edit")
            {
                masterpnl.Enabled = true;
                imgupdate.Visible = true;
                ImageButton1.Visible = false;
            }
            else if (e.CommandName == "View")
            {
                imgupdate.Visible = false;
                ImageButton1.Visible = false;
                masterpnl.Enabled = false;
            }
            string idd = Convert.ToString(e.CommandArgument);
            ViewState["smid"] = idd;
            string strwh = "SELECT  * FROM ShippingMaster where Id='" + idd + "' ";
        SqlCommand cmdwh = new SqlCommand(strwh, con);
        SqlDataAdapter adpwh = new SqlDataAdapter(cmdwh);
        DataTable dtwh = new DataTable();
        adpwh.Fill(dtwh);
        if (dtwh.Rows.Count > 0)
        {
            rdrate.Checked = false;
            rdratevalume.Checked = false;
            rdperorder.Checked = false;
            masterpnl.Visible = true;
            pnlbtnshow.Visible = true;
            rdzippostal.Checked = false;
            rdcountry.Checked = false;
            if (Convert.ToString(dtwh.Rows[0]["Countryoption"]) == "1")
            {
                btnnew.Visible = false;
                rdcountry.Checked = true;
               ddlcountry.SelectedIndex = ddlcountry.Items.IndexOf(ddlcountry.Items.FindByValue(Convert.ToString(dtwh.Rows[0]["CountryId"])));
               ddlcountry_SelectedIndexChanged(sender, e);
                ddlstate.SelectedIndex = ddlstate.Items.IndexOf(ddlstate.Items.FindByValue(Convert.ToString(dtwh.Rows[0]["StateId"])));
                ddlstate_SelectedIndexChanged(sender, e);
               ddlcity.SelectedIndex = ddlcity.Items.IndexOf(ddlcity.Items.FindByValue(Convert.ToString(dtwh.Rows[0]["CityId"])));

            }
            else
            {
                rdzippostal.Checked = true;
                ddlcountry.SelectedIndex = ddlcountry.Items.IndexOf(ddlcountry.Items.FindByValue(Convert.ToString(dtwh.Rows[0]["CountryId"])));
                txtEndzip.Text = Convert.ToString(dtwh.Rows[0]["EndZip"]);
                txtStartzip.Text = Convert.ToString(dtwh.Rows[0]["StartZip"]);
              
             
               
            }
            rdcountry_CheckedChanged(sender, e);
            rdrate.Checked = false;
            rdperorder.Checked = false;
            rdratevalume.Checked = false;
            txtMinWeight.Text = "";
            txtMaxVolume.Text = "";
            txtMaxWeight.Text = "";
            txtMinVolume.Text = "";
            txtVolumerate.Text = "";
            txtWeightRate.Text = "";
            txtFlatRate.Text = "";
            if (Convert.ToString(dtwh.Rows[0]["Rateoption"]) == "3")
            {
                txtFlatRate.Text = Convert.ToString(dtwh.Rows[0]["FlatRate"]);
                rdflatamt.SelectedValue = Convert.ToString(dtwh.Rows[0]["PerorderType"]);
                rdperorder.Checked = true;

            }
            else if (Convert.ToString(dtwh.Rows[0]["Rateoption"]) == "2")
            {
                txtMinVolume.Text = Convert.ToString(dtwh.Rows[0]["MinVolume"]);
                txtMaxVolume.Text = Convert.ToString(dtwh.Rows[0]["MaxVolume"]);
                txtVolumerate.Text = Convert.ToString(dtwh.Rows[0]["Volumerate"]);
                ddlVolumeUnit.SelectedIndex = ddlVolumeUnit.Items.IndexOf(ddlVolumeUnit.Items.FindByValue(Convert.ToString(dtwh.Rows[0]["VolUnit"])));
                ddlvolunit.SelectedIndex = ddlvolunit.Items.IndexOf(ddlvolunit.Items.FindByValue(Convert.ToString(dtwh.Rows[0]["VolUnitVol1"])));
            
                rdratevalume.Checked = true;
            
            }
            else if (Convert.ToString(dtwh.Rows[0]["Rateoption"]) == "1")
            {
                txtMinWeight.Text = Convert.ToString(dtwh.Rows[0]["MinWeight"]);
                txtMaxWeight.Text = Convert.ToString(dtwh.Rows[0]["MaxWeight"]);
                txtWeightRate.Text = Convert.ToString(dtwh.Rows[0]["WeightRate"]);
                ddlUnitType.SelectedIndex = ddlUnitType.Items.IndexOf(ddlUnitType.Items.FindByValue(Convert.ToString(dtwh.Rows[0]["WeightUnit"])));
                ddlunittype1.SelectedIndex = ddlunittype1.Items.IndexOf(ddlunittype1.Items.FindByValue(Convert.ToString(dtwh.Rows[0]["WeightUnit1"])));
            
                rdrate.Checked = true;

            }
            RadioButton1_CheckedChanged(sender, e);
        }
       
       
          
        }
        else  if (e.CommandName == "Delete")
        {
            //grdTempData.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            // ViewState["id"] = GridView1.SelectedDataKey.Value;
        }
    }
    protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
    {
       
    }
    protected void GridView2_Sorting(object sender, GridViewSortEventArgs e)
    {

    }
    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
       
        try
        {
           
            string strdel = " delete ShippingMaster where Id='" + GridView2.DataKeys[e.RowIndex].Value + "' ";
            SqlCommand cmddel = new SqlCommand(strdel, con);
            con.Open();
            cmddel.ExecuteNonQuery();
            con.Close();

            GridView2.EditIndex = -1;
            FillGridGridView2();
            Label1.Visible = true;
            Label1.Text = "Record Deleted Successfully";
        }
        catch (Exception ty1)
        {
            Label1.Visible = true;
            Label1.Text = "Error ;" + ty1.Message; ;
        }
    }
   
 
    protected void ddlGrdCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlCountry = (DropDownList)GridView2.Rows[GridView2.EditIndex].FindControl("ddlGrdCountry");
        DropDownList ddlState = (DropDownList)GridView2.Rows[GridView2.EditIndex].FindControl("ddlGrdState");
        DropDownList ddlcity = (DropDownList)GridView2.Rows[GridView2.EditIndex].FindControl("ddlGrdCity");
        FillGrdCntST(ddlCountry, ddlState);
    }
    protected void ddlGrdState_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlCountry = (DropDownList)GridView2.Rows[GridView2.EditIndex].FindControl("ddlGrdCountry");
        DropDownList ddlState = (DropDownList)GridView2.Rows[GridView2.EditIndex].FindControl("ddlGrdState");
        DropDownList ddlcity = (DropDownList)GridView2.Rows[GridView2.EditIndex].FindControl("ddlGrdCity");
        FillgrdSTct(ddlState, ddlcity);

    }
    protected void FillGrdCntST(DropDownList ddlCnt, DropDownList ddlSt)
    {
        ddlSt.Items.Clear();
        if (ddlCnt.SelectedIndex > 0)
        {
            string str45 = "SELECT  distinct   StateId,StateName   " +
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
    protected void FillgrdSTct(DropDownList ddlSt, DropDownList ddlCty)
    {
        if (ddlSt.SelectedIndex > 0)
        {
            string str455 = "SELECT  distinct CityId , CityName  " +
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
        ddlCty.Items.Insert(0, "ALL");
        ddlCty.Items[0].Value = "0";


    }

    protected void btnchkcon_Click(object sender, EventArgs e)
    {
        Label lblid = (Label)grdTempData.Rows[Idno].FindControl("lblid");
        RequiredFieldValidator Reqfieldusername = (RequiredFieldValidator)grdTempData.Rows[Idno].FindControl("Reqfieldusername");
        RequiredFieldValidator Reqfieldpass = (RequiredFieldValidator)grdTempData.Rows[Idno].FindControl("Reqfieldpass");

        RequiredFieldValidator ReqfieldMerc = (RequiredFieldValidator)grdTempData.Rows[Idno].FindControl("ReqfieldMerc");

        RequiredFieldValidator ReqfieldAcc = (RequiredFieldValidator)grdTempData.Rows[Idno].FindControl("ReqfieldAcc");

        RequiredFieldValidator ReqfieldAccesskey = (RequiredFieldValidator)grdTempData.Rows[Idno].FindControl("ReqfieldAccesskey");

        if (lblid.Text == "")
        {
            TextBox lblaccesskey = (TextBox)grdTempData.Rows[Idno].FindControl("lblaccesskey");
            TextBox lblUsername = (TextBox)grdTempData.Rows[Idno].FindControl("lblUsername");
            TextBox lblpassword = (TextBox)grdTempData.Rows[Idno].FindControl("lblpassword");
            TextBox lblmerchant = (TextBox)grdTempData.Rows[Idno].FindControl("lblmerchant");
            TextBox lblaccname = (TextBox)grdTempData.Rows[Idno].FindControl("lblaccname");
            Button btnadd = (Button)grdTempData.Rows[Idno].FindControl("btnadd");

          

        
           
        }
        ReqfieldMerc.Visible = true;
        ReqfieldAccesskey.Visible = true;
        ReqfieldAcc.Visible = true;
        Reqfieldpass.Visible = true;
        Reqfieldusername.Visible = true;
        
    }
    protected void btnchkcan_Click(object sender, EventArgs e)
    {

        CheckBox chkbus = (CheckBox)grdTempData.Rows[Idno].FindControl("chkbus");

        chkbus.Checked =true;
    }
    protected void rdcountry_CheckedChanged(object sender, EventArgs e)
    {
        if (rdcountry.Checked == true)
        {
            pnlcountry.Visible = true;
            pnlcounpostal.Visible = false;
            ddlpostalcountry.SelectedIndex = 0;
            txtStartzip.Text = "0";
            txtEndzip.Text = "0";
        }
        else
        {
            ddlcountry.SelectedIndex = 0;
            ddlcountry_SelectedIndexChanged(sender, e);
            ddlstate.SelectedIndex = 0;
            ddlstate_SelectedIndexChanged(sender, e);
            ddlcity.SelectedIndex = 0;
            pnlcountry.Visible = false;
            pnlcounpostal.Visible = true;
        }
    }
    protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
    {
        if (rdrate.Checked == true)
        {
            pnlforweight.Visible = true;
            pnlvolume.Visible = false;
            pnlperorder.Visible = false;
            txtMinVolume.Text = "0";
            txtMaxVolume.Text = "0";
            txtVolumerate.Text = "0";
            txtFlatRate.Text = "0";
        }
        else if (rdratevalume.Checked == true)
        {
            pnlforweight.Visible = false;
            pnlvolume.Visible = true;
            pnlperorder.Visible = false;
            txtFlatRate.Text = "0";
            txtMaxWeight.Text = "0";
            txtMinWeight.Text = "0";
            txtWeightRate.Text = "0";
        }
        else if (rdperorder.Checked == true)
        {
            pnlforweight.Visible = false;
            pnlvolume.Visible = false;
            pnlperorder.Visible = true;
            txtMaxWeight.Text = "0";
            txtMinWeight.Text = "0";
            txtWeightRate.Text = "0";
            txtMinVolume.Text = "0";
            txtMaxVolume.Text = "0";
            txtVolumerate.Text = "0";
        }
    }
   
    protected void imgupdate_Click(object sender, EventArgs e)
    {
        string pera = "";
        string cois = "";
        int countory = 1;
        if (rdcountry.Checked == true)
        {
            countory = 1;
            cois = ddlcountry.SelectedValue;
            if (ddlcountry.SelectedIndex > 0)
            {
                pera = " and ShippingMaster.CountryId='" + ddlcountry.SelectedValue + "'";
            }
            if (ddlstate.SelectedIndex > 0)
            {
                pera += " and ShippingMaster.CityId='" + ddlstate.SelectedValue + "'";
            }
            if (ddlcity.SelectedIndex > 0)
            {
                pera += " and ShippingMaster.CountryId='" + ddlcity.SelectedValue + "'";
            }
        }
        else
        {
            countory = 2;
            cois = ddlpostalcountry.SelectedValue;
            if (ddlpostalcountry.SelectedIndex > 0)
            {
                pera = " and ShippingMaster.CountryId='" + ddlpostalcountry.SelectedValue + "'";
            }
            pera += " and ShippingMaster.StartZip='" + txtStartzip.Text + "'";
            pera += " and ShippingMaster.EndZip='" + txtEndzip.Text + "'";
        }
        try
        {
            DataTable dtstate = select("Select * from ShippingMaster where Whid='" + ddlWarehouse.SelectedValue + "' and Id<>'" + ViewState["smid"] + "'" + pera);
            if (dtstate.Rows.Count == 0)
            {
                int flag = 0;
                int Ratetype = 1;
                if (rdrate.Checked == true)
                {
                    Ratetype = 1;
                }
                else if (rdratevalume.Checked == true)
                {
                    Ratetype = 2;
                }
                else if (rdperorder.Checked == true)
                {
                  
                        if (rdflatamt.SelectedValue == "0")
                        {

                        }
                        else
                        {
                            if (Convert.ToDecimal(txtFlatRate.Text) > Convert.ToDecimal(100))
                            {
                                flag = 1;
                            }
                        }
                   

                    Ratetype = 3;
                }
                if (flag == 0)
                {
                    string strupdate = "UPDATE  [ShippingMaster] " +
                   " SET [CountryId] = '" + cois + "' " +
                 " ,[StateId] = '" + ddlstate.SelectedValue + "' " +
                 " ,[CityId] = '" + ddlcity.SelectedValue + "' " +
                 " ,[EndZip] = '" + txtEndzip.Text + "' " +
                 " ,[StartZip] = '" + txtStartzip.Text + "' " +
                 " ,[MinWeight] = '" + txtMinWeight.Text + "' " +
                 " ,[MaxWeight] = '" + txtMaxWeight.Text + "' " +
                 " ,[WeightUnit] = '" + ddlUnitType.SelectedValue + "' " +
                 " ,[MinVolume] = '" + txtMinVolume.Text + "' " +
                 " ,[MaxVolume] = '" + txtMinVolume.Text + "'" +
                 " ,[VolUnit] ='" + ddlVolumeUnit.SelectedValue + "' " +
                 " ,[Volumerate] = '" + txtVolumerate.Text + "' " +
                 " ,[WeightRate] = '" + txtWeightRate.Text + "' " +
                 " ,[FlatRate] = '" + txtFlatRate.Text + "' " +
                 " ,[Countryoption] = '" + countory + "' " +
                   " ,[Rateoption] = '" + Ratetype + "' " +
                     " ,[VolUnitVol1] = '" + ddlvolunit.SelectedValue + "' " +
                       " ,[WeightUnit1] = '" + ddlunittype1.SelectedValue + "',PerorderType='" + rdflatamt.SelectedValue + "' " +
                    " WHERE  id='" + ViewState["smid"] + "'";
                    SqlCommand cmdupadate = new SqlCommand(strupdate, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdupadate.ExecuteNonQuery();
                    con.Close();

                    FillGridGridView2();
                    Label1.Visible = true;
                    Label1.Text = "Record updated successfully";
                    ImageButton1.Visible = true;
                    imgupdate.Visible = false;
                    clean();
                    masterpnl.Visible = false;
                    pnlbtnshow.Visible = false;
                    btnnew.Visible = true;

                }
                else
                {
                    Label1.Visible = true;
                    Label1.Text = "You cannot input a flatrate percentage greater then 100";
                }
            }
            else
            {
                Label1.Visible = true;
                Label1.Text = "Record already exists";
            }
        }
        catch (Exception ty)
        {
            Label1.Visible = true;
            Label1.Text = "Error ;" + ty.Message; ;
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        masterpnl.Enabled = true;
        btnnew.Visible = true;
        Label1.Text = "";
        ImageButton1.Visible = true;
        imgupdate.Visible = false;
        clean();
        masterpnl.Visible = false;
        pnlbtnshow.Visible = false;
    }
    protected void ddlVolumeUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlvolunit.SelectedIndex = ddlVolumeUnit.SelectedIndex;
    }
    protected void ddlUnitType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlunittype1.SelectedIndex = ddlUnitType.SelectedIndex;
    }
    protected void btnnew_Click(object sender, EventArgs e)
    {
        masterpnl.Visible = true;
        btnnew.Visible = false;
        masterpnl.Visible = true;
        pnlbtnshow.Visible = true;
    }
    protected void ddlfiltercountory_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlfilterstate.Items.Clear();
        ddlfilterstate.DataSource = (DataSet)fillddlco();
        ddlfilterstate.DataTextField = "StateName";
        ddlfilterstate.DataValueField = "StateId";
        ddlfilterstate.DataBind();
        ddlfilterstate.Items.Insert(0, "All");
        ddlfilterstate.Items[0].Value = "0";
        ddlfilterstate_SelectedIndexChanged(sender, e);
       // FillGridGridView2();
    }
    protected void ddlfilterstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlfiltercity.Items.Clear();
        ddlfiltercity.DataSource = (DataSet)fillddl3sta();
        ddlfiltercity.DataTextField = "CityName";
        ddlfiltercity.DataValueField = "CityId";
        ddlfiltercity.DataBind();
        ddlfiltercity.Items.Insert(0, "All");

        ddlfiltercity.Items[0].Value = "0";
        //FillGridGridView2();
        ddlfiltercity_SelectedIndexChanged(sender, e);
    }
   

    protected void btnprinta_Click(object sender, EventArgs e)
    {
        if (btnprinta.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");

            btnprinta.Text = "Hide Printable Version";
            Button75.Visible = true;
            if (GridView2.Columns[9].Visible == true)
            {
                ViewState["viewhide"] = "tt";
                GridView2.Columns[9].Visible = false;
            }
            if (GridView2.Columns[10].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView2.Columns[10].Visible = false;
            }
            if (GridView2.Columns[11].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                GridView2.Columns[11].Visible = false;
            }

        }
        else
        {

            pnlgrid.ScrollBars = ScrollBars.Both;
            //pnlgrid.Height = new Unit(250);

            btnprinta.Text = "Printable Version";
            Button75.Visible = false;
            if (ViewState["viewhide"] != null)
            {
                GridView2.Columns[9].Visible = true;
            }
            if (ViewState["editHide"] != null)
            {
                GridView2.Columns[10].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                GridView2.Columns[11].Visible = true;
            }

        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        if (Button4.Text == "Printable Version")
        {
            lblbn.Text = ddlWarehouse.SelectedItem.Text;
           // pnlreal.ScrollBars = ScrollBars.None;
          //  pnlreal.Height = new Unit("100%");

            Button4.Text = "Hide Printable Version";
            Button2.Visible = true;
          
              
                grdTempData.Columns[8].Visible = false;
          
        }
        else
        {

            //pnlreal.ScrollBars = ScrollBars.Both;
            //pnlgrid.Height = new Unit(250);

            Button4.Text = "Printable Version";
            Button2.Visible = false;

            grdTempData.Columns[8].Visible = true;
           

        }
    }

    protected void ddlfiltercity_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGridGridView2();
    }
}
