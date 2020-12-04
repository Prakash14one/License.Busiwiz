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
using System.Collections.Generic;
using System.Data.SqlClient;

public partial class ShoppingCart_Admin_CandidateApplicationStatus : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(PageConn.connnn);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        }
        PageConn pgconn = new PageConn();
        con = pgconn.dynconn;
        if (!IsPostBack)
        {
            Bindgrid();
        }
    }
   
 
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        string qry = "select * from CandidateApplicationStatusMaster where PageStatusName='" + txtdocumenttype.Text + "'";
        SqlCommand cmd1 = new SqlCommand(qry, con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        da1.Fill(dt1);
        if (dt1.Rows.Count > 0)
        {
            lblmsg.Text = "Record already exist.";           
        }
        else
        {

           // string qry1 = "insert into CandidateApplicationStatusMaster(PageStatusName,Active) values('" + txtdocumenttype.Text + "','" + chkactive.Checked + "')";
           // SqlCommand cmd = new SqlCommand(qry1, con);           
            SqlCommand cmd = new SqlCommand("insertCandidateApplicationStatusMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PageStatusName", txtdocumenttype.Text);
            cmd.Parameters.AddWithValue("@StatusShortName", txtshortname.Text);
            cmd.Parameters.AddWithValue("@Active", ddlstactive.SelectedValue);
            cmd.Parameters.AddWithValue("@CompID", Session["Comid"].ToString());
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            lblmsg.Text = "Record inserted successfully.";
            txtdocumenttype.Text = "";
            ddlstactive.SelectedIndex = 0;
            pnladdd.Visible = false;           
            lbllegend.Text = "";
            btnadddd.Visible = true;
            Bindgrid();
        }
    }
    protected void btnadddd_Click(object sender, EventArgs e)
    {
        pnladdd.Visible = true;
        lblmsg.Text = "";
        lbllegend.Text = "Add New Candidate Application Status";
        btnadddd.Visible = false;
        btnsubmit.Visible = true;
        btnupdate.Visible = false;
        txtdocumenttype.Text = "";
        ddlstactive.SelectedIndex = 0;
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        pnladdd.Visible = false;
        lblmsg.Text = "";
        lbllegend.Text = "";
        btnadddd.Visible = true;
        txtdocumenttype.Text = "";
        ddlstactive.SelectedIndex = 0;

    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string qry = "select * from CandidateApplicationStatusMaster where PageStatusName='" + txtdocumenttype.Text + "' and  id <> '" + ViewState["id"].ToString() + "'";
        SqlCommand cmd1 = new SqlCommand(qry, con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        da1.Fill(dt1);
        if (dt1.Rows.Count > 0)
        {
            lblmsg.Text = "Record already exist.";
        }
        else
        {

            //string qry = "update CandidateApplicationStatusMaster set PageStatusName='" + txtdocumenttype.Text + "', Active='" + chkactive.Checked + "' where id='" + ViewState["id"].ToString() + "'";
            //SqlCommand cmd = new SqlCommand(qry, con);
            SqlCommand cmd = new SqlCommand("updateCandidateApplicationStatusMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PageStatusName", txtdocumenttype.Text);
            cmd.Parameters.AddWithValue("@StatusShortName", txtshortname.Text);
            cmd.Parameters.AddWithValue("@Active", ddlstactive.SelectedValue);
            cmd.Parameters.AddWithValue("@CompID", Session["Comid"].ToString());
            cmd.Parameters.AddWithValue("@id", ViewState["id"].ToString());
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            lblmsg.Text = "Record updated successfully.";
            Bindgrid();
            pnladdd.Visible = false;
            btnadddd.Visible = true;
            lbllegend.Text = "";
            pnladdd.Visible = false;
            lbllegend.Text = "";
            btnadddd.Visible = true;
            ddlstactive.SelectedIndex = 0;
        }
    }
    protected void grdsecquestuion_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblmsg.Text = "";
        txtdocumenttype.Text = "";
        ddlstactive.SelectedIndex = 0;
        if (e.CommandName == "edit")
        {

            string qry = "Select * from CandidateApplicationStatusMaster where id='" + e.CommandArgument.ToString() + "'";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            da.Fill(dt);
            if ((dt.Rows[0]["PageStatusName"].ToString() == "Application received") || (dt.Rows[0]["PageStatusName"].ToString() == "Call for first interview") || (dt.Rows[0]["PageStatusName"].ToString() == "First interview done and under review") || (dt.Rows[0]["PageStatusName"].ToString() == "Call For second interview") || (dt.Rows[0]["PageStatusName"].ToString() == "Second interview done and under review") || (dt.Rows[0]["PageStatusName"].ToString() == "Call for third interview") || (dt.Rows[0]["PageStatusName"].ToString() == "Third interview done and under review") || (dt.Rows[0]["PageStatusName"].ToString() == "Primary short listed") || (dt.Rows[0]["PageStatusName"].ToString() == "Final short listed") || (dt.Rows[0]["PageStatusName"].ToString() == "Awaiting references") || (dt.Rows[0]["PageStatusName"].ToString() == "Rejected") || (dt.Rows[0]["PageStatusName"].ToString() == "Hired"))
            {
                lbllegend.Text = "";
                pnladdd.Visible = false;
                btnupdate.Visible = false;
                btnsubmit.Visible = false;
                btnadddd.Visible = true;
                lblmsg.Text = "This is a system created default applications status and can not be edited..";
            }
            else
            {
                lbllegend.Text = "Edit Candidate Application Status";
                pnladdd.Visible = true;
                btnupdate.Visible = true;
                btnsubmit.Visible = false;
                btnadddd.Visible = false;
                lblmsg.Text = "";
               
                ViewState["id"] = dt.Rows[0]["id"].ToString();
                txtdocumenttype.Text = dt.Rows[0]["PageStatusName"].ToString();
                txtshortname.Text = dt.Rows[0]["StatusShortName"].ToString();
                if (dt.Rows[0]["Active"].ToString() == "True")
                {
                    //chkactive.Checked = true;
                    ddlstactive.SelectedValue = "1";
                }
                else
                {
                    //chkactive.Checked = false;
                    ddlstactive.SelectedValue = "0";
                }
                con.Close();
            }
        }
        if (e.CommandName == "delete")
        {
            lbllegend.Text = "";
            lblmsg.Text = "";
            lbllegend.Text = "";
            pnladdd.Visible = false;
            btnupdate.Visible = false;
            btnsubmit.Visible = false;
            btnadddd.Visible = true;
            string qry1 = "Select * from CandidateApplicationStatusMaster where id='" + e.CommandArgument.ToString() + "'";
            SqlCommand cmd1 = new SqlCommand(qry1, con);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            da1.Fill(dt1);
            if ((dt1.Rows[0]["PageStatusName"].ToString() == "Application received") || (dt1.Rows[0]["PageStatusName"].ToString() == "Call for first interview") || (dt1.Rows[0]["PageStatusName"].ToString() == "First interview done and under review") || (dt1.Rows[0]["PageStatusName"].ToString() == "Call For second interview") || (dt1.Rows[0]["PageStatusName"].ToString() == "Second interview done and under review") || (dt1.Rows[0]["PageStatusName"].ToString() == "Call for third interview") || (dt1.Rows[0]["PageStatusName"].ToString() == "Third interview done and under review") || (dt1.Rows[0]["PageStatusName"].ToString() == "Primary short listed") || (dt1.Rows[0]["PageStatusName"].ToString() == "Final short listed") || (dt1.Rows[0]["PageStatusName"].ToString() == "Awaiting references") || (dt1.Rows[0]["PageStatusName"].ToString() == "Rejected") || (dt1.Rows[0]["PageStatusName"].ToString() == "Hired"))
            {

                lblmsg.Text = "This is a system created default applications status and can not be deleted..";
            }
            else
            {

                //string qry = "delete from CandidateApplicationStatusMaster where id='" + e.CommandArgument.ToString() + "'";
                //SqlCommand cmd = new SqlCommand(qry, con);
                SqlCommand cmd = new SqlCommand("deleteCandidateApplicationStatusMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", e.CommandArgument.ToString());
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                lblmsg.Text = "Record deleted successfully.";
                pnladdd.Visible = false;
                btnadddd.Visible = true;
                Bindgrid();
            }
        }
    }
    protected void grdsecquestuion_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void grdsecquestuion_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        if (Button1.Text == "Printable Version")
        {
            Button1.Text = "Hide Printable Version";
            Button2.Visible = true;

            if (grdsecquestuion.Columns[3].Visible == true)
            {
                ViewState["editHide"] = "tt";
                grdsecquestuion.Columns[3].Visible = false;
            }
            if (grdsecquestuion.Columns[2].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                grdsecquestuion.Columns[2].Visible = false;
            }
        }
        else
        {
            Button1.Text = "Printable Version";
            Button2.Visible = false;

            if (ViewState["editHide"] != null)
            {
                grdsecquestuion.Columns[3].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                grdsecquestuion.Columns[2].Visible = true;
            }
        }
    }
    protected void grdsecquestuion_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbl1 = (Label)e.Row.FindControl("lblproductid1");
            Label lbl2 = (Label)e.Row.FindControl("lblproductid2");
            Label lbl3 = (Label)e.Row.FindControl("lblproductid3");
            if (lbl1.Text == "True")
            {
                lbl2.Visible = true;
                lbl3.Visible = false;
            }
            else
            {
                lbl3.Visible = true;
                lbl2.Visible = false;
            }
        }
    }
    public void Bindgrid()
    {
        if (ddlstapplicationstatus.SelectedValue == "All")
        {
            ViewState["str"] = "Select * from CandidateApplicationStatusMaster";
        }
        if (ddlstapplicationstatus.SelectedValue == "Active")
        {
            ViewState["str"] = "Select * from CandidateApplicationStatusMaster where Active='True'";
        }
        if (ddlstapplicationstatus.SelectedValue == "Inactive")
        {
            ViewState["str"] = "Select * from CandidateApplicationStatusMaster where Active='False'";
        }

        SqlCommand cmd = new SqlCommand(ViewState["str"].ToString(), con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            grdsecquestuion.DataSource = dt;
            grdsecquestuion.DataBind();
        }
        else
        {
            grdsecquestuion.DataSource = null;
            grdsecquestuion.DataBind();
        }
    }

    protected void ddlstapplicationstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bindgrid();        
    }
}
