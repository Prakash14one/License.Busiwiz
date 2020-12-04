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
public partial class ManageCutomerDiscountCategory : System.Web.UI.Page
{

    SqlConnection con;

    string compid;
    protected void Page_Load(object sender, EventArgs e)
    {
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
        if (!IsPostBack)
        {
            lblCompany.Text = Session["Cname"].ToString();

            if (Request.QueryString["Id"] != null)
            {
                int id = Convert.ToInt32(Request.QueryString["Id"]);

                SqlCommand cmdmax = new SqlCommand("select * from PartyTypeCategoryMasterTbl where PartyTypeCategoryMasterId='" + id.ToString() + "' ", con);
                SqlDataAdapter adpmax = new SqlDataAdapter(cmdmax);
                DataTable dsmax = new DataTable();
                adpmax.Fill(dsmax);

                if (dsmax.Rows.Count > 0)
                {
                    fillwarehouse();
                    ddlSearchByStore.SelectedIndex = ddlSearchByStore.Items.IndexOf(ddlSearchByStore.Items.FindByValue(dsmax.Rows[0]["Whid"].ToString()));
                    filldiscountcategory();
                    ddldiscountcategory.SelectedIndex = ddldiscountcategory.Items.IndexOf(ddldiscountcategory.Items.FindByValue(dsmax.Rows[0]["PartyTypeCategoryMasterId"].ToString()));
                    fillfilterwarehouse();
                    fillfiltertdiscountcategory();
                    fillcountry();
                    fillstatebyfilter();
                    fillcity();
                    RadioButtonList1_SelectedIndexChanged(sender, e);
                }
            }
            else
            {
                fillwarehouse();
                filldiscountcategory();

                fillfilterwarehouse();
                fillfiltertdiscountcategory();
                fillcountry();
                fillstatebyfilter();
                fillcity();
                RadioButtonList1_SelectedIndexChanged(sender, e);
                
            }
            

           
        }



    }
    protected void fillwarehouse()
    {

        ddlSearchByStore.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlSearchByStore.DataSource = ds;
        ddlSearchByStore.DataTextField = "Name";
        ddlSearchByStore.DataValueField = "WareHouseId";
        ddlSearchByStore.DataBind();

        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlSearchByStore.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }
    }
    protected void fillfilterwarehouse()
    {
        ddlfilterbybusiness.Items.Clear();

        ddlfilterbybusiness.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlfilterbybusiness.DataSource = ds;
        ddlfilterbybusiness.DataTextField = "Name";
        ddlfilterbybusiness.DataValueField = "WareHouseId";
        ddlfilterbybusiness.DataBind();

        ddlfilterbybusiness.Items.Insert(0, "All");
        ddlfilterbybusiness.Items[0].Value = "0";


    }

    protected void fillcountry()
    {
        ddlfiltercountry.Items.Clear();

        string str = "  SELECT     CountryId, CountryName FROM     CountryMaster order by CountryName";
        DataTable ds = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(str, con);
        da.Fill(ds);
        if (ds.Rows.Count > 0)
        {
            ddlfiltercountry.DataSource = ds;
            ddlfiltercountry.DataTextField = "CountryName";
            ddlfiltercountry.DataValueField = "CountryId";
            ddlfiltercountry.DataBind();
            ddlfiltercountry.Items.Insert(0, "All");
            ddlfiltercountry.Items[0].Value = "0";
        }
        else
        {
            ddlfiltercountry.DataSource = null;
            ddlfiltercountry.DataTextField = "CountryName";
            ddlfiltercountry.DataValueField = "CountryId";
            ddlfiltercountry.DataBind();
            ddlfiltercountry.Items.Insert(0, "All");
            ddlfiltercountry.Items[0].Value = "0";

        }







    }
    protected void fillstatebyfilter()
    {


        ddlflterstate.Items.Clear();

        string str45 = "SELECT     StateName  ,StateId FROM  StateMasterTbl where CountryId='" + ddlfiltercountry.SelectedValue + "' Order By StateName ";
        SqlCommand cmd45 = new SqlCommand(str45, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd45);
        DataTable ds = new DataTable();
        da.Fill(ds);

        if (ds.Rows.Count > 0)
        {
            ddlflterstate.DataSource = ds;
            ddlflterstate.DataTextField = "StateName";
            ddlflterstate.DataValueField = "StateId";
            ddlflterstate.DataBind();
            ddlflterstate.Items.Insert(0, "All");
            ddlflterstate.Items[0].Value = "0";
        }
        else
        {
            ddlflterstate.DataSource = null;
            ddlflterstate.DataTextField = "StateName";
            ddlflterstate.DataValueField = "StateId";
            ddlflterstate.DataBind();
            ddlflterstate.Items.Insert(0, "All");
            ddlflterstate.Items[0].Value = "0";

        }





    }
    protected void fillcity()
    {
        ddlfiltercity.Items.Clear();

        string str455 = "SELECT     CityName  ,CityId FROM  CityMasterTbl where StateId='" + ddlflterstate.SelectedValue + "' Order By CityName ";
        SqlCommand cmd45555 = new SqlCommand(str455, con);
        SqlDataAdapter da5 = new SqlDataAdapter(cmd45555);
        DataTable ds5 = new DataTable();
        da5.Fill(ds5);
        if (ds5.Rows.Count > 0)
        {
            ddlfiltercity.DataSource = ds5;
            ddlfiltercity.DataTextField = "CityName";
            ddlfiltercity.DataValueField = "CityId";
            ddlfiltercity.DataBind();
            ddlfiltercity.Items.Insert(0, "All");
            ddlfiltercity.Items[0].Value = "0";
        }
        else
        {
            ddlfiltercity.DataSource = null;
            ddlfiltercity.DataTextField = "CityName";
            ddlfiltercity.DataValueField = "CityId";
            ddlfiltercity.DataBind();
            ddlfiltercity.Items.Insert(0, "All");
            ddlfiltercity.Items[0].Value = "0";

        }









    }
    protected void filldiscountcategory()
    {
        string strpartytype = "  SELECT   WarehouseMaster.Name,PartyCategoryName,  PartyTypeCategoryMasterId FROM PartyTypeCategoryMasterTbl inner join WarehouseMaster on WarehouseMaster.WarehouseId=PartyTypeCategoryMasterTbl.Whid where PartyTypeCategoryMasterTbl.Whid='" + ddlSearchByStore.SelectedValue + "' and (PartyTypeCategoryMasterTbl.Active = 1)  order by PartyCategoryName";
        SqlCommand cmdpartytype = new SqlCommand(strpartytype, con);
        SqlDataAdapter adppartytype = new SqlDataAdapter(cmdpartytype);
        DataTable dtpartytype = new DataTable();
        adppartytype.Fill(dtpartytype);

        ddldiscountcategory.DataSource = dtpartytype;
        ddldiscountcategory.DataTextField = "PartyCategoryName";
        ddldiscountcategory.DataValueField = "PartyTypeCategoryMasterId";
        ddldiscountcategory.DataBind();


    }
    protected void fillfiltertdiscountcategory()
    {
        ddlfilterbydiscountcategory.Items.Clear();
        string strpartytype = "  SELECT   WarehouseMaster.Name,PartyCategoryName,  PartyTypeCategoryMasterId FROM PartyTypeCategoryMasterTbl inner join WarehouseMaster on WarehouseMaster.WarehouseId=PartyTypeCategoryMasterTbl.Whid where PartyTypeCategoryMasterTbl.Whid='" + ddlfilterbybusiness.SelectedValue + "' and (PartyTypeCategoryMasterTbl.Active = 1)  order by PartyCategoryName";
        SqlCommand cmdpartytype = new SqlCommand(strpartytype, con);
        SqlDataAdapter adppartytype = new SqlDataAdapter(cmdpartytype);
        DataTable dtpartytype = new DataTable();
        adppartytype.Fill(dtpartytype);

        if (dtpartytype.Rows.Count > 0)
        {

            ddlfilterbydiscountcategory.DataSource = dtpartytype;
            ddlfilterbydiscountcategory.DataTextField = "PartyCategoryName";
            ddlfilterbydiscountcategory.DataValueField = "PartyTypeCategoryMasterId";
            ddlfilterbydiscountcategory.DataBind();
            ddlfilterbydiscountcategory.Items.Insert(0, "All");
            ddlfilterbydiscountcategory.Items[0].Value = "0";

        }
        else
        {
            ddlfilterbydiscountcategory.DataSource = null;
            ddlfilterbydiscountcategory.DataTextField = "PartyCategoryName";
            ddlfilterbydiscountcategory.DataValueField = "PartyTypeCategoryMasterId";
            ddlfilterbydiscountcategory.DataBind();
            ddlfilterbydiscountcategory.Items.Insert(0, "All");
            ddlfilterbydiscountcategory.Items[0].Value = "0";

        }


    }
    protected void fillgrid()
    {
        lblBusiness.Text = ddlfilterbybusiness.SelectedItem.Text;
        lblcutomercategoryprint.Text = ddlfilterbydiscountcategory.SelectedItem.Text;


        if (RadioButtonList1.SelectedValue == "1")
        {
            Panel1.Visible = true;
            GridView1.Columns[0].Visible = true;

        }
        if (RadioButtonList1.SelectedValue == "0")
        {
            Panel1.Visible = false;
            GridView1.Columns[0].Visible = false;
        }

        string st1 = "";
        string st2 = "";
        string st3 = "";
        string st4 = "";
        string st5 = "";
        string strname = "";


        string strgrd = "select distinct PartyTypeDetailTbl.PartyTypeDetailId,PartyTypeDetailTbl.PartyTypeCategoryMasterId,WarehouseMaster.WarehouseId,WarehouseMaster.Name,Party_master.PartyID,Party_master.Compname,Party_master.Contactperson, Party_master.Phoneno,Party_master.Country,Party_master.State,Party_master.City,  CountryMaster.CountryName,CASE WHEN (CountryMaster.CountryName Is Null or CountryMaster.CountryName='0') THEN '-NA-' ELSE CountryMaster.CountryName END AS CountryName, CASE WHEN (StateMasterTbl.StateName Is Null or StateMasterTbl.StateName='0') THEN '-NA-' ELSE StateMasterTbl.StateName END AS StateName,CASE WHEN (CityMasterTbl.CityName Is Null or CityMasterTbl.CityName='0') THEN '-NA-' ELSE CityMasterTbl.CityName END AS CityName,CASE WHEN (PartyTypeCategoryMasterTbl.PartyCategoryName Is Null ) THEN '-NA-' ELSE PartyTypeCategoryMasterTbl.PartyCategoryName END AS PartyCategoryName  from Party_master inner join WarehouseMaster on WarehouseMaster.WarehouseId=Party_master.Whid  inner join PartytTypeMaster on PartytTypeMaster.PartyTypeId=Party_master.PartyTypeId left outer join PartyTypeDetailTbl on PartyTypeDetailTbl.PartyID=Party_master.PartyID  left outer join PartyTypeCategoryMasterTbl on PartyTypeCategoryMasterTbl.PartyTypeCategoryMasterId=PartyTypeDetailTbl.PartyTypeCategoryMasterId  left outer join CountryMaster on  CountryMaster.CountryId = Party_master.Country  left outer join StateMasterTbl ON Party_master.State = StateMasterTbl.StateId left outer join CityMasterTbl on Party_master.City = CityMasterTbl.CityId  where Party_master.id='" + Session["comid"] + "' and PartytTypeMaster.PartType='Customer' ";


        if (ddlfilterbybusiness.SelectedIndex > 0)
        {
            st1 = " and WarehouseMaster.WarehouseId='" + ddlfilterbybusiness.SelectedValue + "'";
        }

        if (ddlfilterbydiscountcategory.SelectedIndex > 0)
        {
            st2 = " and PartyTypeDetailTbl.PartyTypeCategoryMasterId='" + ddlfilterbydiscountcategory.SelectedValue + "'";
        }
        if (ddlfiltercountry.SelectedIndex > 0)
        {

            st3 = " and  Party_master.Country='" + ddlfiltercountry.SelectedValue + "'";
        }
        if (ddlflterstate.SelectedIndex > 0)
        {
            st4 = " and Party_master.State='" + ddlflterstate.SelectedValue + "'";
        }
        if (ddlfiltercity.SelectedIndex > 0)
        {
            st5 = " and Party_master.City='" + ddlfiltercity.SelectedValue + "'";
        }
        if (txtsearchby.Text.Length > 0)
        {
            strname = " and ((Party_master.Compname like '%" + txtsearchby.Text.Replace("'", "''") + "%') or (Party_master.Contactperson like '%" + txtsearchby.Text.Replace("'", "''") + "%') or (Party_master.Phoneno like '%" + txtsearchby.Text.Replace("'", "''") + "%') or (Party_master.Email = '" + txtsearchby.Text.Replace("'", "''") + "') )";
        }



        string strfinal = strgrd + st1 + st2 + st3 + st4 + st5 + strname;

        SqlCommand cmdgrd = new SqlCommand(strfinal, con);
        SqlDataAdapter adpgrd = new SqlDataAdapter(cmdgrd);
        DataTable dtgrd1 = new DataTable();
        adpgrd.Fill(dtgrd1);

        GridView1.DataSource = dtgrd1;
        GridView1.DataBind();

        if (GridView1.Rows.Count > 0)
        {

            Button4.Visible = true;
        }
        else
        {
            Button4.Visible = false;
        }

    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button2.Text == "Printable Version")
        {

            Button2.Text = "Hide Printable Version";
            Button7.Visible = true;
            if (GridView1.Columns[9].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[9].Visible = false;
            }
           

        }
        else
        {

            Button2.Text = "Printable Version";
            Button7.Visible = false;
            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[9].Visible = true;
            }
            
        }

    }
    protected void chkboxcopy_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkmasterid = (CheckBox)GridView1.HeaderRow.FindControl("chkmasterid");
        if (chkmasterid.Checked == true)
        {

            foreach (GridViewRow grd in GridView1.Rows)
            {

                CheckBox chkpartyid = (CheckBox)grd.FindControl("chkpartyid");
                if (chkpartyid.Enabled == true)
                {
                    chkpartyid.Checked = true;
                }
            }
        }
        else
        {
            foreach (GridViewRow grd in GridView1.Rows)
            {
                CheckBox chkpartyid = (CheckBox)grd.FindControl("chkpartyid");
                chkpartyid.Checked = false;
            }
        }

    }
    protected void ddlSearchByStore_SelectedIndexChanged(object sender, EventArgs e)
    {
        filldiscountcategory();
        RadioButtonList1_SelectedIndexChanged(sender, e);
    }
    protected void ddlfilterbybusiness_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillfiltertdiscountcategory();
    }
    protected void ddlfiltercountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillstatebyfilter();
        fillcity();
    }
    protected void ddlflterstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillcity();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        

        if (RadioButtonList1.SelectedValue == "1")
        {
            Panel1.Visible = true;
            GridView1.Columns[8].Visible = true;
            GridView1.Columns[9].Visible = false;

            int a = Convert.ToInt32(ddlSearchByStore.SelectedValue.ToString());

            fillfilterwarehouse();
            ddlfilterbybusiness.SelectedIndex = ddlfilterbybusiness.Items.IndexOf(ddlfilterbybusiness.Items.FindByValue(a.ToString()));
            fillfiltertdiscountcategory();
            ddlfilterbybusiness.Enabled = false;
            Panel2.Visible = true;

            lblstep1.Text = "Step 1:";
            lblstep2.Text = "Step 2:";
            lblstep3.Text = "Step 3:";
           
            

           
        }
        if (RadioButtonList1.SelectedValue == "0")
        {
            Panel1.Visible = false;

            GridView1.Columns[8].Visible = false;
            GridView1.Columns[9].Visible = true;

            ddlfilterbybusiness.Enabled = true;
            Panel2.Visible = false;
            lblstep1.Text = "";
            lblstep2.Text = "";
            lblstep3.Text = "";

            
        }
        fillgrid();
    }

   

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int dk1 = Convert.ToInt32(GridView1.DataKeys[e.NewEditIndex].Value);
        GridView1.EditIndex = e.NewEditIndex;
        fillgrid();




        DropDownList ddlgrdcategory = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("ddlgrdcategory");

        Label lblwhid123 = (Label)GridView1.Rows[GridView1.EditIndex].FindControl("lblwhid123");

        Label lblpartydetailmasterid123 = (Label)GridView1.Rows[GridView1.EditIndex].FindControl("lblpartydetailmasterid123");
        Label lblpartytypemasterid123 = (Label)GridView1.Rows[GridView1.EditIndex].FindControl("lblpartytypemasterid123");



        string strpartytype = "  SELECT   WarehouseMaster.Name,PartyCategoryName,  PartyTypeCategoryMasterId FROM PartyTypeCategoryMasterTbl inner join WarehouseMaster on WarehouseMaster.WarehouseId=PartyTypeCategoryMasterTbl.Whid where PartyTypeCategoryMasterTbl.Whid='" + lblwhid123.Text + "' and (PartyTypeCategoryMasterTbl.Active = 1)  order by PartyCategoryName";
        SqlCommand cmdpartytype = new SqlCommand(strpartytype, con);
        SqlDataAdapter adppartytype = new SqlDataAdapter(cmdpartytype);
        DataTable dtpartytype = new DataTable();
        adppartytype.Fill(dtpartytype);

        ddlgrdcategory.DataSource = dtpartytype;
        ddlgrdcategory.DataTextField = "PartyCategoryName";
        ddlgrdcategory.DataValueField = "PartyTypeCategoryMasterId";
        ddlgrdcategory.DataBind();

        if (lblpartydetailmasterid123.Text != "" && lblpartytypemasterid123.Text != "")
        {
            ddlgrdcategory.SelectedIndex = ddlgrdcategory.Items.IndexOf(ddlgrdcategory.Items.FindByValue(lblpartytypemasterid123.Text));

        }



    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int dk = Convert.ToInt32(GridView1.DataKeys[GridView1.EditIndex].Value);

        DropDownList ddlgrdcategory = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("ddlgrdcategory");

        Label lblwhid123 = (Label)GridView1.Rows[GridView1.EditIndex].FindControl("lblwhid123");
        Label lblpartydetailmasterid123 = (Label)GridView1.Rows[GridView1.EditIndex].FindControl("lblpartydetailmasterid123");
        Label lblpartytypemasterid123 = (Label)GridView1.Rows[GridView1.EditIndex].FindControl("lblpartytypemasterid123");
        Label lblpartyid123 = (Label)GridView1.Rows[GridView1.EditIndex].FindControl("lblpartyid123");

        if (lblpartydetailmasterid123.Text == "" && lblpartytypemasterid123.Text == "")
        {
            string strinsert = "Insert into PartyTypeDetailTbl (PartyTypeCategoryMasterId,PartyID) values ('" + ddlgrdcategory.SelectedValue + "','" + lblpartyid123.Text + "')";
            SqlCommand cmd1 = new SqlCommand(strinsert, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();

            }
            cmd1.ExecuteNonQuery();
            con.Close();

        }
        else
        {
            string strinsert = " Update PartyTypeDetailTbl set PartyTypeCategoryMasterId='" + ddlgrdcategory.SelectedValue + "',PartyID='" + lblpartyid123.Text + "' where PartyTypeDetailId='" + lblpartydetailmasterid123.Text + "' ";
            SqlCommand cmd1 = new SqlCommand(strinsert, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();

            }
            cmd1.ExecuteNonQuery();
            con.Close();

        }
        GridView1.EditIndex = -1;
        fillgrid();

        statuslable.Visible = true;
        statuslable.Text = "Record updated successfully";



    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gdr in GridView1.Rows)
        {
            Label lblpartydetailmasterid = (Label)gdr.FindControl("lblpartydetailmasterid");
            Label lblpartytypemasterid = (Label)gdr.FindControl("lblpartytypemasterid");
            Label lblpartyid = (Label)gdr.FindControl("lblpartyid");
            CheckBox chkpartyid = (CheckBox)gdr.FindControl("chkpartyid");

            if (chkpartyid.Checked == true)
            {

                if (lblpartydetailmasterid.Text == "" && lblpartytypemasterid.Text == "")
                {
                    string strinsert = "Insert into PartyTypeDetailTbl (PartyTypeCategoryMasterId,PartyID) values ('" + ddldiscountcategory.SelectedValue + "','" + lblpartyid.Text + "')";
                    SqlCommand cmd1 = new SqlCommand(strinsert, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();

                    }
                    cmd1.ExecuteNonQuery();
                    con.Close();

                }
                else
                {
                    string strinsert = " Update PartyTypeDetailTbl set PartyTypeCategoryMasterId='" + ddldiscountcategory.SelectedValue + "',PartyID='" + lblpartyid.Text + "' where PartyTypeDetailId='" + lblpartydetailmasterid.Text + "' ";
                    SqlCommand cmd1 = new SqlCommand(strinsert, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();

                    }
                    cmd1.ExecuteNonQuery();
                    con.Close();
                }
            }
            fillgrid();

            statuslable.Visible = true;
            statuslable.Text = "Record updated successfully";

        }
    }

    protected void LinkButton97666667_Click(object sender, ImageClickEventArgs e)
    {

        string te = "PartyTypeCategoryMasterTbl.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);


    }
    protected void LinkButton13_Click(object sender, EventArgs e)
    {
        filldiscountcategory();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        fillgrid();
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox1.Checked == true)
        {
            pnlfilter.Visible = true;
        }
        else
        {
            pnlfilter.Visible = false;
        }
    }
}
