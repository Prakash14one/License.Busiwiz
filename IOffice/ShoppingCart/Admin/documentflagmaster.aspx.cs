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

public partial class ShoppingCart_Admin_documentflagmaster : System.Web.UI.Page
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
        //string qry = "Select * from Mydocumentflagstbl";       
        SqlCommand cmd = new SqlCommand("openalldtMydocumentflagstbl", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        da.Fill(dt);
        grddocumentflagmaster.DataSource = dt;
        grddocumentflagmaster.DataBind();
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {       

        //string qry = "select * from Mydocumentflagstbl where documentflagname='" + txtdocumentflagname.Text + "'";     
        SqlCommand cmd1 = new SqlCommand("checkdtMydocumentflagstbl", con);
        cmd1.CommandType = CommandType.StoredProcedure;
        cmd1.Parameters.AddWithValue("@documentflagname", txtdocumentflagname.Text);
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
            txtdocumentflagname.Text = "";
        }
        else
        {

          // string qry1 = "insert into Mydocumentflagstbl(CID,documentflagname) values('" + Session["Comid"].ToString() + "','" + txtdocumentflagname.Text + "')";
           SqlCommand cmd = new SqlCommand("insertMydocumentflagstbl", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CID", Session["Comid"].ToString());
            cmd.Parameters.AddWithValue("@documentflagname", txtdocumentflagname.Text);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            lblmsg.Text = "Record inserted successfully..";
            txtdocumentflagname.Text = "";           
            Bindgrid();
        }
    }
    protected void btnadddd_Click(object sender, EventArgs e)
    {
        pnladdd.Visible = true;
        lblmsg.Text = "";
        lbllegend.Text = "Add New Document Flag Name";
        btnadddd.Visible = false;
        btnsubmit.Visible = true;
        btnupdate.Visible = false;
       txtdocumentflagname.Text = "";      
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        pnladdd.Visible = false;
        lblmsg.Text = "";
        lbllegend.Text = "";
        btnadddd.Visible = true;
        txtdocumentflagname.Text = "";       

    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {        
        SqlCommand cmd = new SqlCommand("updateMydocumentflagstbl", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@documentflagname", txtdocumentflagname.Text);
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
        txtdocumentflagname.Text = "";       
       
        if (e.CommandName == "edit")
        {

           // string qry = "Select * from Mydocumentflagstbl where id='" + e.CommandArgument.ToString() + "'";        
            SqlCommand cmd = new SqlCommand("editdtMydocumentflagstbl", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", e.CommandArgument.ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            da.Fill(dt);
            if ((dt.Rows[0]["documentflagname"].ToString() == "Read") || (dt.Rows[0]["documentflagname"].ToString() == "Unread") || (dt.Rows[0]["documentflagname"].ToString() == "Important") || (dt.Rows[0]["documentflagname"].ToString() == "Remind Me Later") || (dt.Rows[0]["documentflagname"].ToString() == "Discuss"))
            {
                lbllegend.Text = "";
                pnladdd.Visible = false;
                btnupdate.Visible = false;
                btnsubmit.Visible = false;
                btnadddd.Visible = true;
                lblmsg.Text = "This is a system created default document flag name and can not be edited..";
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
                txtdocumentflagname.Text = dt.Rows[0]["documentflagname"].ToString();                
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
           // string qry1 = "Select * from Mydocumentflagstbl where id='" + e.CommandArgument.ToString() + "'";       
            SqlCommand cmd1 = new SqlCommand("editdtMydocumentflagstbl", con);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@id", e.CommandArgument.ToString());
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            da1.Fill(dt1);
            if ((dt1.Rows[0]["documentflagname"].ToString() == "Read") || (dt1.Rows[0]["documentflagname"].ToString() == "Unread") || (dt1.Rows[0]["documentflagname"].ToString() == "Important") || (dt1.Rows[0]["documentflagname"].ToString() == "Remind Me Later") || (dt1.Rows[0]["documentflagname"].ToString() == "Discuss"))
            {

                lblmsg.Text = "This is a system created default document flag name and can not be deleted..";
            }
            else
            {
                SqlCommand cmd = new SqlCommand("deleteMydocumentflagstbl", con);
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

            if (grddocumentflagmaster.Columns[2].Visible == true)
            {
                ViewState["editHide"] = "tt";
                grddocumentflagmaster.Columns[2].Visible = false;
            }
            if (grddocumentflagmaster.Columns[1].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                grddocumentflagmaster.Columns[1].Visible = false;
            }
        }
        else
        {
            Button1.Text = "Printable Version";
            Button2.Visible = false;

            if (ViewState["editHide"] != null)
            {
                grddocumentflagmaster.Columns[2].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                grddocumentflagmaster.Columns[1].Visible = true;
            }
        }
    } 

}
