using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Default2 : System.Web.UI.Page
{
    SqlConnection con;

    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;

        if (!IsPostBack)
        {
            //   Response.Write("Hi");
            
            fillstore();

            gvExamNameFill();
            clear();
        }
    }

    protected void fillstore()
    {
        ddlSchoolName.SelectedIndex = 0;
        ddlSchoolName.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlSchoolName.DataSource = ds;
        ddlSchoolName.DataTextField = "Name";
        ddlSchoolName.DataValueField = "WareHouseId";
        ddlSchoolName.DataBind();
        //ddlStore.Items.Insert(0, "Select");

        ddlSchoolName_Search.Items.Clear();
        DataTable ds1 = ClsStore.SelectStorename();
        ddlSchoolName_Search.DataSource = ds;
        ddlSchoolName_Search.DataTextField = "Name";
        ddlSchoolName_Search.DataValueField = "WareHouseId";
        ddlSchoolName_Search.DataBind();

        ddlSchoolName_Search.Items.Insert(0, "All");
        ddlSchoolName_Search.Items[0].Value = "0";

        // ViewState["cd"] = "1";
        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        //if (dteeed.Rows.Count > 0)
        //{
        //    ddlSchoolName.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        //}
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        duplicatedChk();
    }

    protected void duplicatedChk()
    {
        SqlCommand cmd1 = new SqlCommand("Select * from tblExamName where ExamName = '" + txtExamName.Text + "' and BusinessID= '" + ddlSchoolName.SelectedItem.Value + "'", con);
        SqlDataAdapter sqlAdp = new SqlDataAdapter(cmd1);
        DataTable Dt = new DataTable();
        sqlAdp.Fill(Dt);

        if (Dt.Rows.Count > 0)
        {
                lblMsg.Text = "Record already exist.";                       
        }
        else
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("spExamName", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ExamName", SqlDbType.VarChar).Value = txtExamName.Text;
            cmd.Parameters.Add("@BusinessID", SqlDbType.Int).Value = Convert.ToInt32(ddlSchoolName.SelectedValue);
            cmd.Parameters.Add("@Status", SqlDbType.Bit).Value = Convert.ToInt32(ddlStatusEN.SelectedValue);
            cmd.ExecuteNonQuery();

            gvExamNameFill();
            pnladdd.Visible = false;
            btnAddBusCat.Visible = true;
            lblMsg.Text = "Record Inserted Successfully";
            lbllegend.Text = "";
            txtExamName.Text = "";
        }
    }

    protected void clear()
    {
        txtExamName.Text = "";
        pnladdd.Visible = false;
        btnAddBusCat.Visible = true;
        btnsave.Visible = true;
        Button3.Visible = false;
        lbllegend.Text = "";   
    }


    protected void gvExamNameFill()
    {
        string st1 = "", st2 = "";
        lblstat.Text = "All";
        lblstat.Text = ddlStatusENActive.SelectedItem.Text;
        lblSchoolName.Text = ddlSchoolName_Search.SelectedItem.Text;

        if (ddlSchoolName_Search.SelectedIndex > 0)
        {
            st2 += " and T1.BusinessID='" + ddlSchoolName_Search.SelectedValue + "'";
        }

        if (ddlStatusENActive.SelectedIndex > 0)
        {
            st1 += " and T1.Status='" + ddlStatusENActive.SelectedItem.Value + "'";
        }

        // SqlCommand cmd = new SqlCommand("select ID,ExamName,BusinessID,Status,case(Status) when 1 then 'Active' else 'Inactive' end as StatusMode from tblExamName" + st1 + "", con);

        SqlCommand cmd = new SqlCommand("select T2.Name as School,T1.ID,T1.ExamName,T1.BusinessID,T1.Status,case(T1.Status) when 1 then 'Active' else 'Inactive' end as StatusMode from [dbo].[tblExamName] as T1 join [WareHouseMaster] as T2 on T1.BusinessID = T2.WareHouseId where T2.Status='1' " + st2 + st1 + "", con);
        SqlDataAdapter sqlAdp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sqlAdp.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            gvExamName.DataSource = dt;
            gvExamName.DataBind();
        }
        else
        {
            gvExamName.DataSource = null;
            gvExamName.DataBind();
        }

    }

    protected void gvExamName_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            btnsave.Visible = false;
            Button3.Visible = true;
            pnladdd.Visible = true;
            lbllegend.Text = "Edit Exam Name";
            lblMsg.Text = "";

            int mm = Convert.ToInt32(e.CommandArgument);

            ViewState["ID"] = mm;

            SqlDataAdapter da = new SqlDataAdapter("select * from tblExamName  where ID='" + mm + "'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlSchoolName.SelectedValue = dt.Rows[0]["BusinessID"].ToString();
            txtExamName.Text = dt.Rows[0]["ExamName"].ToString();

            string status = dt.Rows[0]["Status"].ToString();

            if (status == "True")
            {
                ddlStatusEN.SelectedValue = "1";
                ddlStatusEN.SelectedItem.Text = "Active";
            }
            else
            {
                ddlStatusEN.SelectedValue = "0";
                ddlStatusEN.SelectedItem.Text = "Inactive";
            }            

        }

        if (e.CommandName == "Delete")
        {
            int mm1 = Convert.ToInt32(e.CommandArgument);

            string str1 = "Delete  From tblExamName Where ID= " + mm1 + " ";
            SqlCommand cmd1 = new SqlCommand(str1, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd1.ExecuteNonQuery();
            con.Close();

            lblMsg.Visible = true;
            lblMsg.Text = "Record deleted successfully.";

            gvExamNameFill();
        }

    }
    protected void gvExamName_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvExamName.PageIndex = e.NewPageIndex;
        gvExamNameFill();
    }
    protected void gvExamName_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void gvExamName_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void Button3_Click(object sender, EventArgs e)
    {      
        DataTable dt = new DataTable();
        SqlCommand cmdStr = new SqlCommand("select * from tblExamName where ExamName='" + txtExamName.Text + "' and BusinessID='" + Convert.ToInt32(ddlSchoolName.SelectedValue) + "' and Status='" + Convert.ToInt32(ddlStatusEN.SelectedValue) + "'", con);
        SqlDataAdapter sqlAdp = new SqlDataAdapter(cmdStr);

        sqlAdp.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            string currentgrade = ViewState["ID"].ToString();
            
            if (dt.Rows[0]["Id"].ToString() == currentgrade)
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Record updated successfully.";
            }
            else
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Record already exist.";
            }
        }
        else
        {
            string str = "update tblExamName set ExamName='" + txtExamName.Text + "',BusinessID='" + Convert.ToInt32(ddlSchoolName.SelectedValue) + "',Status='" + Convert.ToInt32(ddlStatusEN.SelectedValue) + "' where ID= '" + ViewState["ID"] + "'";
            SqlCommand cmd1 = new SqlCommand(str, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd1.ExecuteNonQuery();
            con.Close();            
            lblMsg.Text = "Record Updated Successfully";
        }
        gvExamNameFill();      
        clear();

    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        clear();
        lblMsg.Text = "";
    }

    protected void btnAddBusCat_Click(object sender, EventArgs e)
    {
        ddlSchoolName.SelectedIndex = 0;
        ddlStatusEN.SelectedIndex = 0;
        btnAddBusCat.Visible = false;
        pnladdd.Visible = true;
        lbllegend.Text = "Add New Exam";
        lblMsg.Text = "";
        txtExamName.Text = "";
    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            Button1.Text = "Hide Printable Version";
            Button2.Visible = true;

            if (gvExamName.Columns[3].Visible == true)
            {
                ViewState["editHide"] = "tt";
                gvExamName.Columns[3].Visible = false;
            }
            if (gvExamName.Columns[4].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                gvExamName.Columns[4].Visible = false;
            }
        }
        else
        {
            Button1.Text = "Printable Version";
            Button2.Visible = false;

            if (ViewState["editHide"] != null)
            {
                gvExamName.Columns[3].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                gvExamName.Columns[4].Visible = true;
            }
        }
    }
    protected void ddlStatusENActive_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvExamNameFill();
    }
    protected void ddlSchoolName_Search_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvExamNameFill();
    }
}