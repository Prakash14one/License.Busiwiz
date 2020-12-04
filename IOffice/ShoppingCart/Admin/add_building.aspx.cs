using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class add_building : System.Web.UI.Page
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
        ViewState["cd"] = "1";
        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            //   ddlschool.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
            //   filterschool.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }
    }
    protected void clear()
    {

        filterschool.SelectedIndex = 0;
        ddlschool.SelectedIndex = 0;
      
        txtBuilding.Text = "";
        chkNavigation.Checked = false;
        btnadddd.Visible = true;
        pnladdd.Visible = false;
        lbllegend.Text = "";
        btnsave.Visible = true;
        Button3.Visible = false;
    }
    protected void bindgrid()
    {
        string gra = "";


        if (filterschool.SelectedIndex > 0)
        {

            gra += " where BusinessID='" + filterschool.SelectedValue + "'";

        }


        SqlCommand cmdc = new SqlCommand("SELECT tblBuilding.BuildingName,tblBuilding.ID,tblBuilding.BusinessID,WareHouseMaster.Name as School FROM tblBuilding inner join WareHouseMaster on tblBuilding.BusinessID=WareHouseMaster.WareHouseId" + gra + "", con);
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


            SqlCommand cmd = new SqlCommand("spaddbuilding", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@BusinessID", SqlDbType.Int).Value = ddlschool.SelectedValue;
            cmd.Parameters.Add("@BuildingName", SqlDbType.VarChar).Value = txtBuilding.Text;

           
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

       
            SqlCommand cmd = new SqlCommand("spupdatebuilding", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ViewState["ID"];
            cmd.Parameters.Add("@BusinessID", SqlDbType.Int).Value = ddlschool.SelectedValue;
            cmd.Parameters.Add("@BuildingName", SqlDbType.VarChar).Value = txtBuilding.Text;


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

    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        if (!IsPostBack)
        {

            fillstore();
            bindgrid();
        }
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;

        SqlCommand cmdstr = new SqlCommand("Select * from tblBuilding where   BusinessID = '" + ddlschool.SelectedValue + "'  and BuildingName= '" + txtBuilding.Text + "'", con);
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
            SqlCommand cmd = new SqlCommand("Select ID from tblBuilding  where BusinessId = '" + ddlschool.SelectedValue + "' and BuildingName= '" + txtBuilding.Text + "'", con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                pnladdd.Visible = false;
                lbllegend.Visible = false;

                string te = "add_floor.aspx?buildingid=" + dt.Rows[0]["ID"];
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

        SqlCommand cmdstr = new SqlCommand("Select * from tblBuilding where   BusinessID = '" + ddlschool.SelectedValue + "'and BuildingName= '" + txtBuilding.Text + "'", con);
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


            if (GVC.Columns[2].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GVC.Columns[2].Visible = false;
            }
            if (GVC.Columns[3].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                GVC.Columns[3].Visible = false;
            }
        }
        else
        {
            Button1.Text = "Printable Version";
            Button2.Visible = false;

            if (ViewState["editHide"] != null)
            {
                GVC.Columns[2].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                GVC.Columns[3].Visible = true;
            }
        }
    }
    protected void filterschool_SelectedIndexChanged(object sender, EventArgs e)
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
            lbllegend.Text = "Edit Building";
            lblmsg.Text = "";

            int mm = Convert.ToInt32(e.CommandArgument);

            ViewState["ID"] = mm;

            SqlDataAdapter da = new SqlDataAdapter("select * from tblBuilding where ID='" + mm + "'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            //   ddlGradeName.SelectedValue = dt.Rows[0]["GradeID"].ToString();
            txtBuilding.Text = dt.Rows[0]["BuildingName"].ToString();
           
            ddlschool.SelectedValue = dt.Rows[0]["BusinessId"].ToString();
          
        }

        if (e.CommandName == "Delete")
        {
            int mm1 = Convert.ToInt32(e.CommandArgument);

            SqlDataAdapter daf = new SqlDataAdapter("select * from tblFloor where Buildingid='" + mm1 + "'", con);
            DataTable dtf = new DataTable();
            daf.Fill(dtf);

            if (dtf.Rows.Count > 0)
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Sorry, you are not able to delete this record as child record exist using this record.";
            }
            else
            {
            string str1 = "Delete  From tblBuilding Where ID= " + mm1 + " ";
                SqlCommand cmd1 = new SqlCommand(str1, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd1.ExecuteNonQuery();
                con.Close();

                lblmsg.Visible = true;
                lblmsg.Text = "Record deleted successfully.";
            }
            bindgrid();
        }
    }
    protected void GVC_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GVC_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void btnadddd_Click(object sender, EventArgs e)
    {
        pnladdd.Visible = true;
        btnadddd.Visible = false;
        lbllegend.Text = "Add New Building";
        lblmsg.Text = "";
    }
    protected void ddlschool_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
    protected void txtBuilding_TextChanged(object sender, EventArgs e)
    {

    }
    protected void chkNavigation_CheckedChanged(object sender, EventArgs e)
    {

    }
}