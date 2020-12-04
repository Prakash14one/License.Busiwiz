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

public partial class ShoppingCart_Admin_DocumentType : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(PageConn.connnn);
    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgconn = new PageConn();
        con = pgconn.dynconn;
        if (!IsPostBack)
        {
            Bindgrid();
        }
    }
    public void Bindgrid()
    {
        string qry = "Select * from DocumentTypenm";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        da.Fill(dt);
        grdsecquestuion.DataSource = dt;
        grdsecquestuion.DataBind();
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        string qry = "select * from DocumentTypenm where name='" + txtdocumenttype.Text + "'";
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
            lblmsg.Text = "Record already exist..";
            txtdocumenttype.Text = "";
            chkactive.Checked = false;
        }
        else
        {

           // string qry1 = "insert into DocumentTypenm(name,active) values('" + txtdocumenttype.Text + "','" + chkactive.Checked + "')";
            //SqlCommand cmd = new SqlCommand(qry1, con);           
            SqlCommand cmd = new SqlCommand("insertDocumentTypenm", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@name", txtdocumenttype.Text);
            cmd.Parameters.AddWithValue("@active", chkactive.Checked);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            lblmsg.Text = "Record inserted successfully..";
            txtdocumenttype.Text = "";
            chkactive.Checked = false;
            Bindgrid();
        }
    }
    protected void btnadddd_Click(object sender, EventArgs e)
    {
        pnladdd.Visible = true;
        lblmsg.Text = "";
        lbllegend.Text = "Add New Document Type";
        btnadddd.Visible = false;
        btnsubmit.Visible = true;
        btnupdate.Visible = false;
        txtdocumenttype.Text = "";
        chkactive.Checked = false;
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        pnladdd.Visible = false;
        lblmsg.Text = "";
        lbllegend.Text = "";
        btnadddd.Visible = true;
        txtdocumenttype.Text = "";
        chkactive.Checked = false;

    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
       // string qry = "update DocumentTypenm set name='" + txtdocumenttype.Text + "', active='" + chkactive.Checked + "' where id='" + lblid.Text + "'";
       // SqlCommand cmd = new SqlCommand(qry, con);
        SqlCommand cmd = new SqlCommand("updateDocumentTypenm", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@name", txtdocumenttype.Text);
        cmd.Parameters.AddWithValue("@active", chkactive.Checked);
        cmd.Parameters.AddWithValue("@id", lblid.Text);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd.ExecuteNonQuery();
        lblmsg.Text = "Record updated successfully..";
        Bindgrid();
        pnladdd.Visible = false;
        btnadddd.Visible = true;
        lbllegend.Text = "";
    }
    protected void grdsecquestuion_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblmsg.Text = "";
        txtdocumenttype.Text = "";
        chkactive.Checked = false;
       
        if (e.CommandName == "edit")
        {
            
            string qry = "Select * from DocumentTypenm where id='" + e.CommandArgument.ToString() + "'";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            da.Fill(dt);
            if ((dt.Rows[0]["name"].ToString() == "Resume") || (dt.Rows[0]["name"].ToString() == "Credit Invoice") || (dt.Rows[0]["name"].ToString() == "Cash Invoice") || (dt.Rows[0]["name"].ToString() == "Bank Statement") || (dt.Rows[0]["name"].ToString() == "Cash Voucher") || (dt.Rows[0]["name"].ToString() == "Credit Voucher") || (dt.Rows[0]["name"].ToString() == "Document") || (dt.Rows[0]["name"].ToString() == "Statement") || (dt.Rows[0]["name"].ToString() == "Note") || (dt.Rows[0]["name"].ToString() == "Letter") || (dt.Rows[0]["name"].ToString() == "Agreement") || (dt.Rows[0]["name"].ToString() == "Email"))
            {
                lbllegend.Text = "";
                pnladdd.Visible = false;
                btnupdate.Visible = false;
                btnsubmit.Visible = false;
                btnadddd.Visible = true;
                lblmsg.Text = "This is a system created default document type and can not be edited..";
            }
            else
            {
                lbllegend.Text = "Edit Document Type";
                pnladdd.Visible = true;
                btnupdate.Visible = true;
                btnsubmit.Visible = false;
                btnadddd.Visible = false;
                lblmsg.Text = "";
                lblid.Text = dt.Rows[0]["id"].ToString();
                txtdocumenttype.Text = dt.Rows[0]["name"].ToString();
                if (dt.Rows[0]["active"].ToString() == "True")
                {
                    chkactive.Checked = true;
                }
                else
                {
                    chkactive.Checked = false;
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
            string qry1 = "Select * from DocumentTypenm where id='" + e.CommandArgument.ToString() + "'";
            SqlCommand cmd1 = new SqlCommand(qry1, con);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            da1.Fill(dt1);         
            if ((dt1.Rows[0]["name"].ToString() == "Resume") || (dt1.Rows[0]["name"].ToString() == "Credit Invoice") || (dt1.Rows[0]["name"].ToString() == "Cash Invoice") || (dt1.Rows[0]["name"].ToString() == "Bank Statement") || (dt1.Rows[0]["name"].ToString() == "Cash Voucher") || (dt1.Rows[0]["name"].ToString() == "Credit Voucher") || (dt1.Rows[0]["name"].ToString() == "Document") || (dt1.Rows[0]["name"].ToString() == "Statement") || (dt1.Rows[0]["name"].ToString() == "Note") || (dt1.Rows[0]["name"].ToString() == "Letter") || (dt1.Rows[0]["name"].ToString() == "Agreement") || (dt1.Rows[0]["name"].ToString() == "Email"))
            {
               
                lblmsg.Text = "This is a system created default document type and can not be deleted..";
            }
            else
            {
                
              //  string qry = "delete from DocumentTypenm where id='" + e.CommandArgument.ToString() + "'";
               // SqlCommand cmd = new SqlCommand(qry, con);
                SqlCommand cmd = new SqlCommand("deleteDocumentTypenm", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", e.CommandArgument.ToString());
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                lblmsg.Text = "Record deleted successfully..";
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

}
