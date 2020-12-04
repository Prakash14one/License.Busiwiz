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

public partial class BusinessCategory : System.Web.UI.Page
{
    SqlConnection con;

    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;

        if (!IsPostBack)
        {
            fillcountry1();
            fillstate1();
            fillcity1();

            fillcomlogo();

            fillgrid();
        }
    }

    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            Button1.Text = "Hide Printable Version";
            Button2.Visible = true;

            if (Gridview2.Columns[4].Visible == true)
            {
                ViewState["editHide"] = "tt";
                Gridview2.Columns[4].Visible = false;
            }
            if (Gridview2.Columns[5].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                Gridview2.Columns[5].Visible = false;
            }
        }
        else
        {
            Button1.Text = "Printable Version";
            Button2.Visible = false;

            if (ViewState["editHide"] != null)
            {
                Gridview2.Columns[4].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                Gridview2.Columns[5].Visible = true;
            }
        }
    }

    public void fillddlOther(DropDownList ddl, String dtf, String dvf)
    {
        ddl.DataTextField = dtf;
        ddl.DataValueField = dvf;
        ddl.DataBind();

    }

    protected DataTable select(string qu)
    {
        //con = new SqlConnection(PageConn.connnn);

        SqlCommand cmd = new SqlCommand(qu, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        return dt;
    }

    protected void fillcountry1()
    {
        string qryStr = "select CountryId,CountryName from CountryMaster order by CountryName";

        ddlcountry1.DataSource = (DataTable)select(qryStr);
        fillddlOther(ddlcountry1, "CountryName", "CountryId");
        ddlcountry1.Items.Insert(0, "All");
        ddlcountry1.Items[0].Value = "0";
    }

    protected void fillstate1()
    {
        ddlstate1.Items.Clear();

        if (ddlcountry1.SelectedIndex > 0)
        {
            string qryStr = "select StateId,StateName from StateMasterTbl where CountryId=" + ddlcountry1.SelectedValue + " order by StateName";
            ddlstate1.DataSource = (DataTable)select(qryStr);
            fillddlOther(ddlstate1, "StateName", "StateId");
            ddlstate1.Items.Insert(0, "All");
            ddlstate1.Items[0].Value = "0";
        }
        else
        {
            ddlstate1.Items.Insert(0, "All");
            ddlstate1.Items[0].Value = "0";
        }
    }

    protected void fillcity1()
    {
        ddlcity1.Items.Clear();

        if (ddlstate1.SelectedIndex > 0)
        {
            string qryStr = "select CityId,CityName from CityMasterTbl where StateId=" + ddlstate1.SelectedValue + " order by CityName";
            ddlcity1.DataSource = (DataTable)select(qryStr);
            fillddlOther(ddlcity1, "CityName", "CityId");
            ddlcity1.Items.Insert(0, "All");
            ddlcity1.Items[0].Value = "0";
        }
        else
        {
            ddlcity1.Items.Insert(0, "All");
            ddlcity1.Items[0].Value = "0";
        }
    }

    protected void ddlcountry1_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillstate1();
    }
    protected void ddlstate1_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillcity1();
    }

    public void fillcomlogo()
    {
        DataTable s1 = select("select distinct [CompanyId] from [CompanyWebsitMaster]");

        DataTable s11 = new DataTable();
        DataTable s12 = new DataTable();

        for (int i = 0; i < s1.Rows.Count; i++)
        {
            string st1 = "";

            if (ddlcountry1.SelectedIndex > 0)
            {
                st1 += " and CompanyWebsiteAddressMaster.Country='" + ddlcountry1.SelectedValue + "'";
            }
            if (ddlstate1.SelectedIndex > 0)
            {
                st1 += " and CompanyWebsiteAddressMaster.State='" + ddlstate1.SelectedValue + "'";
            }
            if (ddlcity1.SelectedIndex > 0)
            {
                st1 += " and CompanyWebsiteAddressMaster.City='" + ddlcity1.SelectedValue + "'";
            }
            if (TextBox1.Text != "")
            {
                st1 += " and ((CompanyWebsitMaster.Sitename like '%" + TextBox1.Text.Replace("'", "''") + "%') or (CountryMaster.CountryName like '%" + TextBox1.Text.Replace("'", "''") + "%') or (StateMasterTbl.StateName like '%" + TextBox1.Text.Replace("'", "''") + "%') or (CityMasterTbl.CityName like '%" + TextBox1.Text.Replace("'", "''") + "%'))";
            }

            s11 = select("select TOP(1) CompanyWebsitMaster.CompanyId,CountryMaster.CountryId,StateMasterTbl.StateId,CityMasterTbl.CityId,CompanyWebsitMaster.logourl,CompanyWebsitMaster.Sitename,CountryMaster.CountryName,StateMasterTbl.StateName,CityMasterTbl.CityName from CompanyWebsitMaster inner join CompanyWebsiteAddressMaster on CompanyWebsiteAddressMaster.CompanyWebsiteMasterId=CompanyWebsitMaster.Whid inner join CountryMaster on CountryMaster.CountryId=CompanyWebsiteAddressMaster.Country inner join StateMasterTbl on StateMasterTbl.StateId=CompanyWebsiteAddressMaster.State inner join CityMasterTbl on CityMasterTbl.CityId=CompanyWebsiteAddressMaster.City where CompanyWebsitMaster.CompanyId='" + s1.Rows[i]["CompanyId"].ToString() + "' " + st1 + " order by CompanyWebsitMaster.CompanyWebsiteMasterId asc");

            s12.Merge(s11);
        }

        //string str = "select CompanyWebsitMaster.logourl,CompanyWebsitMaster.Sitename from CompanyWebsitMaster inner join CompanyWebsiteAddressMaster on CompanyWebsiteAddressMaster.CompanyWebsiteMasterId=CompanyWebsitMaster.Whid where CompanyWebsiteAddressMaster.AddressTypeMasterId=1 and CompanyWebsiteAddressMaster.whid=1 " + st1 + "";

        //SqlDataAdapter da = new SqlDataAdapter(str, con);
        //DataTable dt = new DataTable();
        //da.Fill(dt);

        GridView1.DataSource = s12;
        GridView1.DataBind();

        foreach (GridViewRow dtg in GridView1.Rows)
        {
            Image image1 = (Image)dtg.FindControl("Image100");

            image1.ImageUrl = "~/ShoppingCart/images/" + image1.ImageUrl;
        }
    }

    protected void btnadddd_Click(object sender, EventArgs e)
    {
        pnladdd.Visible = true;
        btnadddd.Visible = false;
        lbllegend.Text = "Add Company Logo to Display on Homepage";
        lblmsg.Text = "";
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        fillcomlogo();
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        DataTable dtcount = select("select count(ID) as ID from CompanyLogoDisplay");

        if (Convert.ToInt32(dtcount.Rows[0]["ID"]) < 51)
        {
            foreach (GridViewRow ggg in GridView1.Rows)
            {
                DropDownList DropDownList1 = (DropDownList)ggg.FindControl("DropDownList1");
                Label lblcomid = (Label)ggg.FindControl("lblcomid");
                Label lblcountryid = (Label)ggg.FindControl("lblcountryid");
                Label lblstateid = (Label)ggg.FindControl("lblstateid");
                Label lblcityid = (Label)ggg.FindControl("lblcityid");

                if (DropDownList1.SelectedValue != "0")
                {
                    string strd = "";

                    if (DropDownList1.SelectedValue == "1")
                    {
                        DataTable dtcount1 = select("select count(ID) as ID from CompanyLogoDisplay");

                        if (Convert.ToInt32(dtcount1.Rows[0]["ID"]) < 51)
                        {
                            DataTable dtmatch = select("select CompanyID from CompanyLogoDisplay where CompanyID='" + lblcomid.Text + "'");

                            if (dtmatch.Rows.Count > 0)
                            {
                                strd = "update CompanyLogoDisplay set CompanyID='" + lblcomid.Text + "',EverywhereDisplay='1',CountryID='0',StateID='0',CityID='0' where CompanyID='" + lblcomid.Text + "'";
                            }
                            else
                            {
                                strd = "insert into CompanyLogoDisplay values('" + lblcomid.Text + "','1','0','0','0')";
                            }
                            SqlCommand cmd = new SqlCommand(strd, con);
                            if (con.State.ToString() != "")
                            {
                                con.Open();
                            }
                            cmd.ExecuteNonQuery();
                            con.Close();
                            lblmsg.Text = "Recod inserted successfully.";
                        }
                        else
                        {
                            lblmsg.Text = "You are exceeding the limit for selection of company logo.Only 50 records are allowed for display everywhere or in entire country or entire state or entire city.Please remove another record to insert this new selection and try again.";
                        }
                    }
                    if (DropDownList1.SelectedValue == "2")
                    {
                        DataTable dtcount1 = select("select count(ID) as ID from CompanyLogoDisplay");

                        if (Convert.ToInt32(dtcount1.Rows[0]["ID"]) < 51)
                        {
                            DataTable dtmatch = select("select CompanyID from CompanyLogoDisplay where CompanyID='" + lblcomid.Text + "'");

                            if (dtmatch.Rows.Count > 0)
                            {
                                strd = "update CompanyLogoDisplay set CompanyID='" + lblcomid.Text + "',EverywhereDisplay='0',CountryID='" + lblcountryid.Text + "',StateID='0',CityID='0' where CompanyID='" + lblcomid.Text + "'";
                            }
                            else
                            {
                                strd = "insert into CompanyLogoDisplay values('" + lblcomid.Text + "','0','" + lblcountryid.Text + "','0','0')";
                            }
                            SqlCommand cmd = new SqlCommand(strd, con);
                            if (con.State.ToString() != "")
                            {
                                con.Open();
                            }
                            cmd.ExecuteNonQuery();
                            con.Close();
                            lblmsg.Text = "Recod inserted successfully.";
                        }
                        else
                        {
                            lblmsg.Text = "You are exceeding the limit for selection of company logo.Only 50 records are allowed for display everywhere or in entire country or entire state or entire city.Please remove another record to insert this new selection and try again.";
                        }
                    }
                    if (DropDownList1.SelectedValue == "3")
                    {
                        DataTable dtcount1 = select("select count(ID) as ID from CompanyLogoDisplay");

                        if (Convert.ToInt32(dtcount1.Rows[0]["ID"]) < 51)
                        {
                            DataTable dtmatch = select("select CompanyID from CompanyLogoDisplay where CompanyID='" + lblcomid.Text + "'");

                            if (dtmatch.Rows.Count > 0)
                            {
                                strd = "update CompanyLogoDisplay set CompanyID='" + lblcomid.Text + "',EverywhereDisplay='0',CountryID='0',StateID='" + lblstateid.Text + "',CityID='0' where CompanyID='" + lblcomid.Text + "'";
                            }
                            else
                            {
                                strd = "insert into CompanyLogoDisplay values('" + lblcomid.Text + "','0','0','" + lblstateid.Text + "','0')";
                            }
                            SqlCommand cmd = new SqlCommand(strd, con);
                            if (con.State.ToString() != "")
                            {
                                con.Open();
                            }
                            cmd.ExecuteNonQuery();
                            con.Close();
                            lblmsg.Text = "Recod inserted successfully.";
                        }
                        else
                        {
                            lblmsg.Text = "You are exceeding the limit for selection of company logo.Only 50 records are allowed for display everywhere or in entire country or entire state or entire city.Please remove another record to insert this new selection and try again.";
                        }
                    }
                    if (DropDownList1.SelectedValue == "4")
                    {
                        DataTable dtcount1 = select("select count(ID) as ID from CompanyLogoDisplay");

                        if (Convert.ToInt32(dtcount1.Rows[0]["ID"]) < 51)
                        {
                            DataTable dtmatch = select("select CompanyID from CompanyLogoDisplay where CompanyID='" + lblcomid.Text + "'");

                            if (dtmatch.Rows.Count > 0)
                            {
                                strd = "update CompanyLogoDisplay set CompanyID='" + lblcomid.Text + "',EverywhereDisplay='0',CountryID='0',StateID='0',CityID='" + lblcityid.Text + "' where CompanyID='" + lblcomid.Text + "'";
                            }
                            else
                            {
                                strd = "insert into CompanyLogoDisplay values('" + lblcomid.Text + "','0','0','0','" + lblcityid.Text + "')";
                            }
                            SqlCommand cmd = new SqlCommand(strd, con);
                            if (con.State.ToString() != "")
                            {
                                con.Open();
                            }
                            cmd.ExecuteNonQuery();
                            con.Close();
                            lblmsg.Text = "Recod inserted successfully.";
                        }
                        else
                        {
                            lblmsg.Text = "You are exceeding the limit for selection of company logo.Only 50 records are allowed for display everywhere or in entire country or entire state or entire city.Please remove another record to insert this new selection and try again.";
                        }
                    }                    
                }
                if (lblmsg.Text == "Recod inserted successfully.")
                {

                }
                else
                {
                    lblmsg.Text = "Please select atleast one specific Location.";
                }                
            }
            fillgrid();
            if (lblmsg.Text == "Recod inserted successfully.")
            {
                clear();
            }
            else
            {
                lblmsg.Text = "Please select atleast one specific Location.";
            }        
        }
        else
        {
            lblmsg.Text = "You are exceeding the limit for selection of company logo.Only 50 records are allowed for display everywhere or in entire country or entire state or entire city.Please remove another record to insert this new selection and try again.";
        }
        
    }

    public void clear()
    {
        foreach (GridViewRow gfb in GridView1.Rows)
        {
            DropDownList DropDownList1 = (DropDownList)gfb.FindControl("DropDownList1");

            DropDownList1.SelectedValue = "0";
        }

        TextBox1.Text = "";
        fillcountry1();
        ddlcountry1.SelectedIndex = 0;
        fillstate1();
        ddlstate1.SelectedIndex = 0;
        fillcity1();
        ddlcity1.SelectedIndex = 0;
        btnadddd.Visible = true;
        pnladdd.Visible = false;
        lbllegend.Text = "";
    }

    public void fillgrid()
    {
        DataTable dtmycount = select("select distinct [CompanyId] from CompanyLogoDisplay");

        DataTable s11 = new DataTable();
        DataTable s12 = new DataTable();

        if (dtmycount.Rows.Count > 0)
        {
            for (int i = 0; i < dtmycount.Rows.Count; i++)
            {
                s11 = select("select TOP(1) CompanyLogoDisplay.ID,CompanyWebsitMaster.CompanyId,CompanyWebsitMaster.logourl,CompanyWebsitMaster.Sitename,CompanyWebsitMaster.Sitename as location,CompanyLogoDisplay.EverywhereDisplay,CompanyLogoDisplay.CountryID,CompanyLogoDisplay.StateID,CompanyLogoDisplay.CityID,CountryMaster.CountryName,StateMasterTbl.StateName,CityMasterTbl.CityName,CountryMaster.CountryId as Country1,StateMasterTbl.StateId as State1,CityMasterTbl.CityId as City1 from CompanyWebsitMaster inner join CompanyWebsiteAddressMaster on CompanyWebsiteAddressMaster.CompanyWebsiteMasterId=CompanyWebsitMaster.Whid inner join CountryMaster on CountryMaster.CountryId=CompanyWebsiteAddressMaster.Country inner join StateMasterTbl on StateMasterTbl.StateId=CompanyWebsiteAddressMaster.State inner join CityMasterTbl on CityMasterTbl.CityId=CompanyWebsiteAddressMaster.City inner join CompanyLogoDisplay on CompanyLogoDisplay.CompanyID=CompanyWebsitMaster.CompanyId where CompanyLogoDisplay.CompanyID='" + dtmycount.Rows[i]["CompanyId"].ToString() + "'  order by CompanyWebsitMaster.CompanyWebsiteMasterId asc");

                s12.Merge(s11);
            }

            Gridview2.DataSource = s12;
            Gridview2.DataBind();

            foreach (GridViewRow ggg in Gridview2.Rows)
            {
                Label lbllocation = (Label)ggg.FindControl("lbllocation");

                Label lbllogevry = (Label)ggg.FindControl("lbllogevry");
                Label lbllogcoun = (Label)ggg.FindControl("lbllogcoun");
                Label lbllogstat = (Label)ggg.FindControl("lbllogstat");
                Label lbllogcity = (Label)ggg.FindControl("lbllogcity");

                Image image1 = (Image)ggg.FindControl("Image1001");

                image1.ImageUrl = "~/ShoppingCart/images/" + image1.ImageUrl;

                if (lbllogevry == null)
                {

                }
                else
                {
                    if (lbllogevry.Text == "1")
                    {
                        lbllocation.Text = "Everywhere";
                    }
                }
                if (lbllogcoun == null)
                {

                }
                else
                {
                    if (lbllogcoun.Text != "0")
                    {
                        lbllocation.Text = "Entire Country";
                    }
                }
                if (lbllogstat == null)
                {

                }
                else
                {
                    if (lbllogstat.Text != "0")
                    {
                        lbllocation.Text = "Entire State";
                    }
                }
                if (lbllogcity == null)
                {

                }
                else
                {
                    if (lbllogcity.Text != "0")
                    {
                        lbllocation.Text = "Entire City";
                    }
                }
            }
        }
        else
        {
            Gridview2.DataSource = null;
            Gridview2.DataBind();
        }
    }
    protected void Gridview2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            int i = Convert.ToInt32(e.CommandArgument);

            SqlCommand cmd = new SqlCommand("delete from CompanyLogoDisplay where ID='" + i + "'", con);
            if (con.State.ToString() != "")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();

            lblmsg.Text = "Record deleted successfully.";
            fillgrid();
        }

        if (e.CommandName == "Edit")
        {
            int m = Convert.ToInt32(e.CommandArgument);


        }
    }
    protected void Gridview2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void Gridview2_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Gridview2.EditIndex = e.NewEditIndex;

        fillgrid();

        DropDownList DropDownList11 = (DropDownList)(Gridview2.Rows[Gridview2.EditIndex].FindControl("DropDownList11"));
    }
    protected void Gridview2_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        DropDownList DropDownList11 = (DropDownList)(Gridview2.Rows[e.RowIndex].FindControl("DropDownList11"));

        Label lblcomid1 = (Label)(Gridview2.Rows[e.RowIndex].FindControl("lblcomid1"));

        Label lbllogcoun = (Label)(Gridview2.Rows[e.RowIndex].FindControl("lblcountryid1"));
        Label lbllogstat = (Label)(Gridview2.Rows[e.RowIndex].FindControl("lblstateid1"));
        Label lbllogcity = (Label)(Gridview2.Rows[e.RowIndex].FindControl("lblcityid1"));

        if (DropDownList11.SelectedValue != "0")
        {
            string strd = "";

            if (DropDownList11.SelectedValue == "1")
            {
                strd = "update CompanyLogoDisplay set CompanyID='" + lblcomid1.Text + "',EverywhereDisplay='1',CountryID='0',StateID='0',CityID='0' where CompanyID='" + lblcomid1.Text + "'";
            }
            if (DropDownList11.SelectedValue == "2")
            {
                strd = "update CompanyLogoDisplay set CompanyID='" + lblcomid1.Text + "',EverywhereDisplay='0',CountryID='" + lbllogcoun.Text + "',StateID='0',CityID='0' where CompanyID='" + lblcomid1.Text + "'";
            }
            if (DropDownList11.SelectedValue == "3")
            {
                strd = "update CompanyLogoDisplay set CompanyID='" + lblcomid1.Text + "',EverywhereDisplay='0',CountryID='0',StateID='" + lbllogstat.Text + "',CityID='0' where CompanyID='" + lblcomid1.Text + "'";
            }
            if (DropDownList11.SelectedValue == "4")
            {
                strd = "update CompanyLogoDisplay set CompanyID='" + lblcomid1.Text + "',EverywhereDisplay='0',CountryID='0',StateID='0',CityID='" + lbllogcity.Text + "' where CompanyID='" + lblcomid1.Text + "'";
            }

            SqlCommand cmd = new SqlCommand(strd, con);
            if (con.State.ToString() != "")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();

            lblmsg.Text = "Record updated successfully.";
        }
        else
        {
            lblmsg.Text = "Please select atleast one specific Location.";
        }

        Gridview2.EditIndex = -1;
        fillgrid();
    }
    protected void Gridview2_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        Gridview2.EditIndex = -1;
        fillgrid();
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        clear();
        lblmsg.Text = "";
    }
}
