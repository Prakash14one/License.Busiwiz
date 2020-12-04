using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class add_floor : System.Web.UI.Page
{
    SqlConnection con;


    protected void fillstore()
    {
        ddlschool.Items.Clear();
        filterschool.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlschool.DataSource = ds;
        ddlschool.DataTextField = "Name";
        ddlschool.DataValueField = "WareHouseId";

        ddlschool.DataBind();


        filterschool.DataSource = ds;
        filterschool.DataTextField = "Name";
        filterschool.DataValueField = "WareHouseId";

        filterschool.DataBind();
        //ddlStore.Items.Insert(0, "Select");
        filterschool.Items.Insert(0, "All");
        filterbuilding.Items.Insert(0, "All");
        ViewState["cd"] = "1";
        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        //if (dteeed.Rows.Count > 0)
        //{
        //    ddlschool.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        //    filterschool.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        //}
    }

    private void fn_bind_ddlbuilding()
    {
        SqlCommand cmd = new SqlCommand("select ID,BuildingName from tblBuilding where BusinessID = '" + ddlschool.SelectedValue + "'", con);

        con.Open();
        DataTable dt = new DataTable();

        dt.Load(cmd.ExecuteReader());
        con.Close();


        ddlbuildingName.DataSource = dt;
        ddlbuildingName.DataTextField = "BuildingName";
        ddlbuildingName.DataValueField = "ID";
        ddlbuildingName.DataBind();

        ddlbuildingName.Items.Insert(0, "-Select-");
        ddlbuildingName.Items[0].Value = "0";
        //ddlbuildingName.Items.Insert(0, new ListItem("--", "0"));
    }
    protected void clear()
    {

        ddlschool.SelectedIndex = 0;
        filterschool.SelectedIndex = 0;
        ddlbuildingName.SelectedIndex = 0;
        //  filterbuilding.SelectedIndex = 0;
        txtfloor.Text = "";
        chkNavigation.Checked = false;
        btnadddd.Visible = true;
        pnladdd.Visible = false;
        lbllegend.Text = "";
        btnsave.Visible = true;
        Button3.Visible = false;
    }
    protected void bindgrid()
    {
        string gra = "", sta = "";


        if (filterschool.SelectedIndex > 0)
        {

            gra += " where tblFloor.BusinessID='" + filterschool.SelectedValue + "'";

        }
        if (filterbuilding.SelectedIndex > 0)
        {

            sta += " and tblFloor.Buildingid='" + filterbuilding.SelectedValue + "'";

        }

        SqlCommand cmdc = new SqlCommand("SELECT tblFloor.floorname,tblFloor.ID,tblFloor.Buildingid,WareHouseMaster.Name as School,tblBuilding.BuildingName FROM tblFloor inner join WareHouseMaster on tblFloor.BusinessID=WareHouseMaster.WareHouseId inner join tblBuilding on  tblBuilding.ID=tblFloor.Buildingid" + gra + sta + "", con);
        SqlDataAdapter dac = new SqlDataAdapter(cmdc);
        DataTable dtc = new DataTable();
        dac.Fill(dtc);

        if (dtc.Rows.Count > 0)
        {
            GVC.DataSource = dtc;
            GVC.DataBind();
        }
        else
        {
            GVC.DataSource = null;
            GVC.DataBind();
        }

    }

    private void fn_SaveGrade()
    {
        try
        {


            SqlCommand cmd = new SqlCommand("spaddfloor", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.Add("@floorname", SqlDbType.VarChar).Value = txtfloor.Text;
            cmd.Parameters.Add("@BusinessID", SqlDbType.Int).Value = ddlschool.SelectedValue;
            cmd.Parameters.Add("@Buildingid", SqlDbType.Int).Value = ddlbuildingName.SelectedValue;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            lblmsg.Visible = true;
            lblmsg.Text = "Record inserted successfully.";
        }
        catch
        {
            lblmsg.Text = "Record not inserted.";
        }


    }
    private void fn_update()
    {
        try
        {


            SqlCommand cmd = new SqlCommand("spupdatefloor", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ViewState["ID"];
            cmd.Parameters.Add("@floorname", SqlDbType.VarChar).Value = txtfloor.Text;
            cmd.Parameters.Add("@BusinessID", SqlDbType.Int).Value = ddlschool.SelectedValue;
            cmd.Parameters.Add("@Buildingid", SqlDbType.VarChar).Value = ddlbuildingName.SelectedValue;


            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            lblmsg.Visible = true;
            lblmsg.Text = "Record updated successfully.";
        }
        catch
        {
            lblmsg.Text = "Record not updated.";
        }
        bindgrid();

    }

    private void fn_ddlfilter()
    {
        filterbuilding.Items.Clear();

        if (filterschool.SelectedIndex > 0)
        {
            SqlCommand cmd = new SqlCommand("select ID,BuildingName from tblBuilding where BusinessID = '" + filterschool.SelectedValue + "'", con);
            con.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            con.Close();

            filterbuilding.DataSource = dt;
            filterbuilding.DataTextField = "BuildingName";
            filterbuilding.DataValueField = "ID";
            filterbuilding.DataBind();

        }
        filterbuilding.Items.Insert(0, new ListItem("All", "0"));

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;


        if (!IsPostBack)
        {
            bindgrid();
            fillstore();
            fn_bind_ddlbuilding();

            if (Request.QueryString["buildingid"] != null)
            {
                pnladdd.Visible = true;
                btnadddd.Visible = false;
                string buildingid = Request.QueryString["buildingid"];
                SqlCommand cmdstr = new SqlCommand("Select * from tblBuilding where ID = '" + buildingid + "'", con);
                SqlDataAdapter adp = new SqlDataAdapter(cmdstr);
                DataTable ds = new DataTable();
                adp.Fill(ds);
                if (ds.Rows.Count > 0)
                {

                    ddlschool.SelectedValue = ds.Rows[0]["BusinessID"].ToString();
                    fn_bind_ddlbuilding();
                    ddlbuildingName.SelectedValue = ds.Rows[0]["ID"].ToString();

                }

            }

        }
    }
    protected void btnadddd_Click(object sender, EventArgs e)
    {
        pnladdd.Visible = true;
        btnadddd.Visible = false;
        lbllegend.Text = "Add New Floor";
        lblmsg.Text = "";
    }
    protected void ddlschool_SelectedIndexChanged(object sender, EventArgs e)
    {
        fn_bind_ddlbuilding();
    }
    protected void ddlbuildingName_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void txtfloor_TextChanged(object sender, EventArgs e)
    {

    }
    protected void chkNavigation_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;

        SqlCommand cmdstr = new SqlCommand("Select * from tblFloor where   BusinessID = '" + ddlschool.SelectedValue + "'  and Buildingid = '" + ddlbuildingName.SelectedValue + "'  and floorname= '" + txtfloor.Text + "'", con);
        SqlDataAdapter adp = new SqlDataAdapter(cmdstr);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        if (ds.Rows.Count > 0)
        {
            lblmsg.Text = "Record already exist.";
        }
        else
        {
            fn_SaveGrade();

        }


        if (chkNavigation.Checked == true)
        {
            SqlCommand cmd = new SqlCommand("Select ID from tblFloor  where BusinessId = '" + ddlschool.SelectedValue + "' and Buildingid = '" + ddlbuildingName.SelectedValue + "'  and floorname= '" + txtfloor.Text + "'", con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                pnladdd.Visible = false;
                lbllegend.Visible = false;



                string te = "addRoom.aspx?floorID=" + dt.Rows[0]["ID"];
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
            }
        }
        clear();

        bindgrid();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;

        SqlCommand cmdstr = new SqlCommand("Select * from tblFloor where    BusinessId = '" + ddlschool.SelectedValue + "' and Buildingid = '" + ddlbuildingName.SelectedValue + "'  and floorname= '" + txtfloor.Text + "'", con);
        SqlDataAdapter adp = new SqlDataAdapter(cmdstr);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        if (ds.Rows.Count > 0)
        {
            string currentsection = ViewState["ID"].ToString();
            if (ds.Rows[0]["ID"].ToString() == currentsection)
            {

                lblmsg.Text = "Record updated successfully.";
            }
            else
            {
                lblmsg.Text = "Record already exist.";
            }
        }
        else
        {
            fn_update();

        }



        clear();
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {

        clear();
    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            Button1.Text = "Hide Printable Version";
            Button2.Visible = true;


            if (GVC.Columns[3].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GVC.Columns[3].Visible = false;
            }
            if (GVC.Columns[4].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                GVC.Columns[4].Visible = false;
            }
        }
        else
        {
            Button1.Text = "Printable Version";
            Button2.Visible = false;

            if (ViewState["editHide"] != null)
            {
                GVC.Columns[3].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                GVC.Columns[4].Visible = true;
            }
        }
    }
    protected void filterschool_SelectedIndexChanged(object sender, EventArgs e)
    {
        fn_ddlfilter();
        bindgrid();
    }
    protected void filterbuilding_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindgrid();
    }
    protected void GVC_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVC.PageIndex = e.NewPageIndex;
        bindgrid();
    }
    protected void GVC_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            btnsave.Visible = false;
            Button3.Visible = true;
            pnladdd.Visible = true;
            btnadddd.Visible = false;
            lbllegend.Text = "Edit Floor";
            lblmsg.Text = "";

            int mm = Convert.ToInt32(e.CommandArgument);

            ViewState["ID"] = mm;

            SqlDataAdapter da = new SqlDataAdapter("select * from tblFloor where ID='" + mm + "'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            txtfloor.Text = dt.Rows[0]["floorname"].ToString();

            ddlschool.SelectedValue = dt.Rows[0]["BusinessId"].ToString();
            fn_bind_ddlbuilding();
            ddlbuildingName.SelectedValue = dt.Rows[0]["Buildingid"].ToString();
        }

        if (e.CommandName == "Delete")
        {
            int mm1 = Convert.ToInt32(e.CommandArgument);

            //SqlDataAdapter daf = new SqlDataAdapter("select * from RoomMasterTbl where ID='" + mm1 + "'", con);
            //DataTable dtf = new DataTable();
            //daf.Fill(dtf);

            //if (dtf.Rows.Count > 0)
            //{
            //    lblmsg.Visible = true;
            //    lblmsg.Text = "Sorry, you are not able to delete this record as child record exist using this record.";
            //}
            //else
            //{
            string str1 = "Delete  From tblFloor Where ID= " + mm1 + " ";
            SqlCommand cmd1 = new SqlCommand(str1, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd1.ExecuteNonQuery();
            con.Close();

            lblmsg.Visible = true;
            lblmsg.Text = "Record deleted successfully.";
            //  }
            bindgrid();
        }
    }
    protected void GVC_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GVC_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
}