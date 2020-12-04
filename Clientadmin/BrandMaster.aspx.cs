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
using System.Net;
using System.IO;
using System.Drawing;
public partial class BrandMaster : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        lblVersion.Text = "v1 ";
        if (!IsPostBack)
        {
           
            fillgrid();
            Panel1.Visible = false;
            Panel2.Visible = true;
        }
    }
    public void fillgrid()
    {
        string strcln = " ";
       
            if (txtsearch.Text != "")
            {
                strcln += "  where  (BrandMasterTbl.Name Like '%" + txtsearch.Text + "%' OR BrandMasterTbl.WebsiteName Like '%" + txtsearch.Text + "%'  )";
            }
            string finalstr = "Select * from  BrandMasterTbl " + strcln + " order by ID DESC";
        SqlCommand cmdcln = new SqlCommand(finalstr, con);
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        DataTable dtcln = new DataTable();
        adpcln.Fill(dtcln);
        if (dtcln.Rows.Count > 0)
        {
            GridView1.DataSource = dtcln;
            GridView1.DataBind();

        }

    }
    protected void Clr()
    {
        txtname.Text = "";
        txtwebsite.Text="";  
        chkboxActiveDeactive.Checked = false;
        Panel1.Visible = false;
    }  
    protected void Btnsave_Click(object sender, EventArgs e)
    {
        

             if (con.State.ToString() != "Open")
        {
            con.Open();
        }
             SqlCommand cmd = new SqlCommand("BrandMasterTbl_AddDelUpdtSelect", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@StatementType", "Insert");
        cmd.Parameters.AddWithValue("@Name", txtname.Text);
        cmd.Parameters.AddWithValue("@WebsiteName", txtwebsite.Text);
        cmd.Parameters.AddWithValue("@Active", chkboxActiveDeactive.Checked);       
        cmd.ExecuteNonQuery();
        con.Close();
        lblmsg.Visible = true;
        lblmsg.Text = "Record Inserted Successfully";
        Clr();
        fillgrid();
        //string finalstr = "insert into BrandMasterTbl(Name,WebsiteName,Active)values('" + txtname.Text + "','" + txtwebsite.Text + "','" + chkboxActiveDeactive.Checked + "')";
        //SqlCommand cmdcln = new SqlCommand(finalstr, con);
        //con.Open();
        //cmdcln.ExecuteNonQuery();
        //con.Close();
        //Label11.Text = "Record Inserted Sucessfully";
        //Panel1.Visible = false;
        //Panel2.Visible = true;
        //Btnupdate.Visible = false;
     
        //chkboxActiveDeactive.Checked = false;
        //Panel2.Visible = true;
        //fillgrid();
    }

    protected void Btncancel_Click(object sender, EventArgs e)
    {
        Panel1.Visible = false;
        Panel2.Visible = true;
    }
    protected void Btnupdate_Click(object sender, EventArgs e)
    {
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
            SqlCommand cmd = new SqlCommand("BrandMasterTbl_AddDelUpdtSelect", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StatementType", "Update");
            cmd.Parameters.AddWithValue("@ID", ViewState["id"]);
            cmd.Parameters.AddWithValue("@Name", txtname.Text);
            cmd.Parameters.AddWithValue("@WebsiteName", txtwebsite.Text);
            cmd.Parameters.AddWithValue("@Active", chkboxActiveDeactive.Checked);
            cmd.ExecuteNonQuery();
            con.Close();
            lblmsg.Visible = true;
            lblmsg.Text = "Record updated successfully.";
            Clr();
            fillgrid();
                
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
          ImageButton lnkbtn = (ImageButton)sender;

        GridViewRow row = (GridViewRow)lnkbtn.NamingContainer;
        int j = Convert.ToInt32(row.RowIndex);

        Label id = (Label)GridView1.Rows[j].FindControl("Label15");
        string finalstr = "Select * from  BrandMasterTbl where ID='" + id.Text + "'";
        SqlCommand cmdcln = new SqlCommand(finalstr, con);
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        DataTable dtcln = new DataTable();
        adpcln.Fill(dtcln);
        ViewState["id"]= id.Text;
        if (dtcln.Rows.Count > 0)
        {
            txtname.Text = dtcln.Rows[0]["Name"].ToString();
            txtwebsite.Text = dtcln.Rows[0]["WebsiteName"].ToString();
            try
            {
                chkboxActiveDeactive.Checked = Convert.ToBoolean(dtcln.Rows[0]["Active"].ToString());
            }
            catch (Exception ex)
            {
            }                 
            Panel1.Visible = true;
            Btnsave.Visible = false;
            Btnupdate.Visible = true;
        }
    }
    protected void imgdelete_Click(object sender, ImageClickEventArgs e)
    {

        ImageButton lnkbtn = (ImageButton)sender;

        GridViewRow row = (GridViewRow)lnkbtn.NamingContainer;
        int j = Convert.ToInt32(row.RowIndex);

        Label id = (Label)GridView1.Rows[j].FindControl("Label15");
        string finalstr = "Select * from  Product_MasterIndividual where BrandID='" + id.Text + "'";
        SqlCommand cmdcln = new SqlCommand(finalstr, con);
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        DataTable dtcln = new DataTable();
        adpcln.Fill(dtcln);
        if (dtcln.Rows.Count == 0)
        {
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("BrandMasterTbl_AddDelUpdtSelect", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StatementType", "Delete");
            cmd.Parameters.AddWithValue("@ID", id.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            lblmsg.Visible = true;
            lblmsg.Text = "Record Deleted Sucessfully";
            fillgrid();
            Panel1.Visible = false;
            Panel2.Visible = true;

        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Sorry, You are not allow delete this record,first delete chield record.";
        }
       
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        Btnsave.Visible = true;
        Btnupdate.Visible = false;
        lblmsg.Text = "";
        txtname.Text = "";
        txtwebsite.Text = "";
        chkboxActiveDeactive.Checked = false;
        
        Panel2.Visible = true;
    }
    protected void BtnGo_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
}