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
public partial class Admin_VaultDeviceType : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        lblmsg.Visible = false;
        if (!IsPostBack)
        {
           fillgrid();
        }
    }
    protected void fillgrid()
    {
        try
        {
            GridView1.DataSource = null;


            string strcln = "Select * from VaultDeviceType order by name";
            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);
            GridView1.DataSource = dtcln;
           
            GridView1.DataBind();
        }
        catch
        {
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            if (e.CommandName == "edit1")
            {
                ViewState["ID"] = "";
                GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
                int i = Convert.ToInt32(GridView1.DataKeys[GridView1.SelectedIndex].Value.ToString());
                ViewState["ID"] = i.ToString();
                string strcln = "";
               
                strcln = "Select * from VaultDeviceType where Id='" + i.ToString() + "'";
                SqlCommand cmdcln = new SqlCommand(strcln, con);
                DataTable dtcln = new DataTable();
                SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
                adpcln.Fill(dtcln);
                if (dtcln.Rows.Count > 0)
                Txtaddname.Text = dtcln.Rows[0]["name"].ToString();
              // chkactive.Checked = Convert.ToBoolean(dtcln.Rows[0]["Active"].ToString());
                    //ddlcname.SelectedIndex = ddlcname.Items.IndexOf(ddlcname.Items.FindByValue(dtcln.Rows[0]["ClinetMasterID"].ToString()));
                btnadd.Text = "Update";
                }
            }
        
        catch
        {
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgrid();
    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        string str="";
        try
        {
            if (btnadd.Text == "Update")
            {
                str = "Update VaultDeviceType Set name='" + Txtaddname.Text.Trim() + "' where ID='" + ViewState["ID"] + "'";

                lblmsg.Text = "Record Updated Successfully.";
            }
            else
            {

                str = "INSERT INTO VaultDeviceType(Name)values('" + Txtaddname.Text.Trim() + "')";
                     lblmsg.Text = "Record insert Successfully.";
            }



            SqlCommand cmd = new SqlCommand(str, con);
            DataTable dt = new DataTable();
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            fillgrid();
            Txtaddname.Text = "";
            btnadd.Text = "Submit";
            lblmsg.Visible = true;
        }
        catch
        {
        }
               
    }
}
