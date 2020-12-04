using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Default : System.Web.UI.Page
{
    SqlConnection con;

    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        if (!IsPostBack)
        {
            fillstore();
            bindgrid();
            // fillschoolfilter();
        }
    }
    //protected void fillschoolfilter()
    //{
    //    filterschool.Items.Clear();
    //    DataTable ds = ClsStore.SelectStorename();
    //    ddlschool.DataSource = ds;
    //    filterschool.DataTextField = "Name";
    //    filterschool.DataValueField = "WareHouseId";
    //    filterschool.DataBind();
    //    //ddlStore.Items.Insert(0, "Select");

    //  //  ViewState["cd"] = "1";
    //    DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

    //    if (dteeed.Rows.Count > 0)
    //    {
    //        filterschool.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
    //    }
    //}
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

    private void fn_SaveGrade()
    {
        try
        {

            string gradename = string.Empty;

            SqlCommand cmd = new SqlCommand("spAddGrade", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@GradeName", SqlDbType.VarChar).Value = txtGrade.Text;
            cmd.Parameters.Add("@Status", SqlDbType.Bit).Value = Convert.ToInt32(ddlActiveInactive.SelectedValue);

            cmd.Parameters.Add("@BusinessId", SqlDbType.Int).Value = ddlschool.SelectedValue;



            // ViewState["gid"] = txtGrade.Text;
            //  insertCmd.Parameters["@AccountType"].Value = StaffType_LB.SelectedItem.Text;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            lblmsg.Text = "Record inserted successfully.";

        }
        catch
        {
            lblmsg.Text = "Record insertion Failed.";
        }


    }
    private void bindgrid()
    {
        string st1 = "", st2 = "";

        st2 += " where GradeNameTbl.Status='" + ddlfilter.SelectedValue + "'";

        if (filterschool.SelectedIndex > 0)
        {
            st1 += " and BusinessId='" + filterschool.SelectedValue + "'";
        }
        SqlCommand cmd = new SqlCommand("select GradeName,Id,BusinessId,WareHouseMaster.Name as school,CASE(GradeNameTbl.Status)  WHEN 1 THEN 'Active' ELSE 'Inactive'  end as Status From  GradeNameTbl inner join WareHouseMaster on GradeNameTbl.BusinessId=WareHouseMaster.WareHouseId" + st2 + st1 + "", con);
        SqlDataAdapter sqlAdp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sqlAdp.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }

    }
    protected void chkActiveInactive_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void txtGrade_TextChanged(object sender, EventArgs e)
    {
        ViewState["gradename"] = Server.HtmlEncode(txtGrade.Text);

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;

        SqlCommand cmdstr = new SqlCommand("Select * from GradeNameTbl  where BusinessId = '" + ddlschool.SelectedValue + "' and GradeName = '" + txtGrade.Text + "'", con);
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
            lblmsg.Visible = true;


        }

        if (chkNavigation.Checked == true)
        {
            SqlCommand cmd = new SqlCommand("Select Id from GradeNameTbl  where BusinessId = '" + ddlschool.SelectedValue + "' and GradeName = '" + txtGrade.Text + "'", con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                pnladdd.Visible = false;
                lbllegend.Visible = false;
                //  btnadddiv.Visible = true;
                string te = "add_section.aspx?gid=" + dt.Rows[0]["Id"];
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
            }



        }

        bindgrid();
        clear();



    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {

            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            btnUpdate.Visible = true;
            btnSave.Visible = false;
            btnCancel.Visible = true;
            lbllegend.Visible = true;
            lbllegend.Text = "Edit Grade";
            pnladdd.Visible = true;
            btnadddiv.Visible = false;
            lblmsg.Visible = false;
            int mm = Convert.ToInt32(e.CommandArgument);

            ViewState["ID"] = mm;

            SqlCommand cmd = new SqlCommand("select * from GradeNameTbl  where Id='" + mm + "'", con);
            SqlDataAdapter sqlAdp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlAdp.Fill(dt);
            txtGrade.Text = dt.Rows[0]["GradeName"].ToString();
            string status = dt.Rows[0]["Status"].ToString();
            if (status == "True")
            {
                ddlActiveInactive.SelectedValue = "1";
            }
            else
            {
                ddlActiveInactive.SelectedValue = "0";
            }
            //  cmd.Parameters.Add("@SchoolName", SqlDbType.VarChar).Value = ddlschool.SelectedItem.ToString();
            ddlschool.SelectedValue = dt.Rows[0]["BusinessId"].ToString();

            con.Close();


            //   chkStatus.Checked = Convert.ToBoolean(dt.Rows[0]["Status"].ToString());
        }

        if (e.CommandName == "Delete")
            //{
            //    int mm1 = Convert.ToInt32(e.CommandArgument);

            //    string str1 = "Delete  From GradeNameTbl Where Id= " + mm1 + " ";
            //    SqlCommand cmd1 = new SqlCommand(str1, con);
            //    if (con.State.ToString() != "Open")
            //    {
            //        con.Open();
            //    }
            //    cmd1.ExecuteNonQuery();
            //    con.Close();

            //    //lblMsg.Visible = true;
            //    //lblMsg.Text = "Record deleted successfully.";


            //}
            if (e.CommandName == "Delete")
            {
                int mm1 = Convert.ToInt32(e.CommandArgument);

                SqlDataAdapter daf = new SqlDataAdapter("select * from SectionMaster where GradeID='" + mm1 + "'", con);
                DataTable dtf = new DataTable();
                daf.Fill(dtf);

                if (dtf.Rows.Count > 0)
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "Sorry, you are not able to delete this record as child record exist using this record.";
                }
                else
                {
                    string str1 = "Delete  From GradeNameTbl Where Id= " + mm1 + " ";
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
            }
        bindgrid();

    }



    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }


    protected void btnUpdate_Click(object sender, EventArgs e)
    {

        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        DataTable dt1 = new DataTable();

        SqlCommand cmdstr = new SqlCommand("Select * from GradeNameTbl  where BusinessId = '" + ddlschool.SelectedValue + "' and Status = '" + ddlActiveInactive.SelectedValue + "' and  GradeName = '" + txtGrade.Text + "'", con);
        SqlDataAdapter adp = new SqlDataAdapter(cmdstr);

        adp.Fill(dt1);
        if (dt1.Rows.Count > 0)
        {
            string currentgrade = ViewState["ID"].ToString();
            if (dt1.Rows[0]["Id"].ToString() == currentgrade)
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Record updated successfully.";
            }
            else
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Record already exist.";
            }
            //  lblmsg.Text = "Record already exist.";
        }
        else
        {
            fn_UpdateGrade();
            lblmsg.Visible = true;
            lblmsg.Text = "Record updated successfully.";
        }



        bindgrid();
        clear();
    }
    private void fn_UpdateGrade()
    {
        try
        {
            string gradename = string.Empty;


            string id = ViewState["ID"].ToString();

            SqlCommand cmd = new SqlCommand("update GradeNameTbl set GradeName='" + txtGrade.Text + "',Status='" + ddlActiveInactive.SelectedValue + "',BusinessId='" + ddlschool.SelectedValue + "' where id ='" + id + "'", con);
            SqlDataAdapter sqlAdp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlAdp.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }

        catch
        {

        }


    }
    protected void btnadddiv_Click(object sender, EventArgs e)
    {
        txtGrade.Text = "";
        ddlschool.SelectedIndex = 0;
        lbllegend.Text = "Add Grade";
        pnladdd.Visible = true;
        btnSave.Visible = true;
        btnUpdate.Visible = false;
        btnCancel.Visible = true;
        btnadddiv.Visible = false;
        ddlActiveInactive.SelectedIndex = 0;
        lblmsg.Text = "";
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();

        lblmsg.Text = "";
    }
    protected void clear()
    {
        //filterschool.SelectedIndex = 0;
        ddlfilter.SelectedIndex = 0;
        ddlschool.SelectedIndex = 0;
        chkNavigation.Checked = false;
        ddlfilter.SelectedValue = "1";
        txtGrade.Text = "";
        ddlActiveInactive.SelectedValue = "1";
        btnadddiv.Visible = true;
        pnladdd.Visible = false;
        lbllegend.Text = "";
        btnSave.Visible = true;
        btnUpdate.Visible = false;
    }

    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            Button1.Text = "Hide Printable Version";
            Button2.Visible = true;

            if (GridView1.Columns[3].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[3].Visible = false;
            }
            if (GridView1.Columns[4].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                GridView1.Columns[4].Visible = false;
            }
        }
        else
        {
            Button1.Text = "Printable Version";
            Button2.Visible = false;

            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[3].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                GridView1.Columns[4].Visible = true;
            }
        }
    }
    protected void ddlActiveInactive_SelectedIndexChanged(object sender, EventArgs e)
    {
        //  ViewState["status"] = ddlActiveInactive.SelectedValue;
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        bindgrid();
    }


    protected void ddlfilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindgrid();
    }
    protected void ddlschool_SelectedIndexChanged(object sender, EventArgs e)
    {
        //  ViewState["schoolna"] = ddlschool.SelectedItem.ToString();
    }
    protected void chkNavigation_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void filterschool_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindgrid();
    }
}