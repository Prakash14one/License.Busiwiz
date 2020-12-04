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
public partial class Admin_VaultDeviceMaster : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        if (!IsPostBack)
        {
           
            
            VaultDeviceType();
            fillgrid();
        }
    }
    
    protected void VaultDeviceType()
    {
        string strcln = "SELECT * from VaultDeviceType order by name";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        ddldivicetype.DataSource = dtcln;
        ddldivicetype.DataValueField = "Id";
        ddldivicetype.DataTextField = "Name";
        ddldivicetype.DataBind();
        ddldivicetype.Items.Insert(0, "-Select-");
        ddldivicetype.Items[0].Value = "0";
    }
    protected void fillgrid()
    {
        try
        {
            GridView1.DataSource = null;


            string strcln = "Select VaultDeviceMaster.*,VaultDeviceType.Name as name1 from VaultDeviceMaster inner join  VaultDeviceType on VaultDeviceMaster.DeviceTypeID=VaultDeviceType.Id order by VaultDeviceMaster.name";
            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);
            GridView1.DataSource = dtcln;

            GridView1.DataBind();
        }
        catch(Exception ex)
        {
            Response.Write(ex);
        }
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        string str = "";
        try
        {
            if (ddldivicetype.SelectedItem.Text != "-Select-")
            {
                if (btnadd.Text == "Update")
                {
                    str = "Update VaultDeviceMaster Set name='" + Txtaddname.Text.Trim() + "',SrNumber='" + txtsrnu.Text.Trim() + "',DeviceTypeID='" + ddldivicetype.SelectedValue + "',Active='" + Chkactive.Checked + "',StartDate='" + txtStartdate.Text.Trim() + "',SeedNumber='" + txtseedno.Text.Trim() + "',Arithmaticoperator='" + txtaritho.Text.Trim() + "',HopNumber='"+txthopno.Text.Trim()+"', activetimeofnumbermnts='" + txtatm.Text.Trim() + "' where ID='" + ViewState["ID"] + "'";

                    lblmsg.Text = "Record Updated Successfully.";
                }
                else
                {

                    str = "INSERT INTO VaultDeviceMaster(Name,SrNumber,DeviceTypeID,Active,StartDate,SeedNumber,Arithmaticoperator,HopNumber,activetimeofnumbermnts)values('" + Txtaddname.Text.Trim() + "', " +
                    " '" + txtsrnu.Text.Trim() + "','" + ddldivicetype.SelectedValue + "','" + Chkactive.Checked + "','" + txtStartdate.Text.Trim() + "','" + txtseedno.Text.Trim() + "','"+txtaritho.Text.Trim()+"','"+txthopno.Text.Trim()+"','" + txtatm.Text.Trim() + "')";
                    lblmsg.Text = "Record insert Successfully.";
                }



                SqlCommand cmd = new SqlCommand(str, con);
                DataTable dt = new DataTable();
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                fillgrid();
                lblmsg.Visible = true;
                btnadd.Text = "Submit";
                clearall();
                
            }
            else
            {
                lblmsg.Text = "Please select vaultdivice name";
                lblmsg.Visible = true;
            }
            
        }
            
        catch
        {
        }
     
    }
    protected void clearall()
    {
        Txtaddname.Text = "";
        Chkactive.Checked = true;
        ddldivicetype.SelectedIndex = 0;
       // ddlpv.SelectedIndex = 0;
        txtsrnu.Text = "";
        txtStartdate.Text = "";
        txtseedno.Text = "";
        txtaritho.SelectedIndex = 0;
        txthopno.Text = "";
        txtatm.Text = "";
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgrid();
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
                string strcln1 = "";

                strcln1 = "Select * from VaultDeviceMaster where Id='" + i.ToString() + "'";
                SqlCommand cmdcln1 = new SqlCommand(strcln1, con);
                DataTable dtcln1 = new DataTable();
                SqlDataAdapter adpcln1 = new SqlDataAdapter(cmdcln1);
                adpcln1.Fill(dtcln1);
                if (dtcln1.Rows.Count > 0)
                    Txtaddname.Text = dtcln1.Rows[0]["name"].ToString();
                 Chkactive.Checked = Convert.ToBoolean(dtcln1.Rows[0]["Active"].ToString());
               

                 ddldivicetype.SelectedIndex = ddldivicetype.Items.IndexOf(ddldivicetype.Items.FindByValue(dtcln1.Rows[0]["DeviceTypeID"].ToString()));
                 txtsrnu.Text = dtcln1.Rows[0]["SrNumber"].ToString();
                 txtStartdate.Text = dtcln1.Rows[0]["Startdate"].ToString();
                 txtseedno.Text = dtcln1.Rows[0]["SeedNumber"].ToString();
                 txtaritho.SelectedIndex = txtaritho.Items.IndexOf(txtaritho.Items.FindByValue(dtcln1.Rows[0]["Arithmaticoperator"].ToString()));
          
                 txthopno.Text = dtcln1.Rows[0]["HopNumber"].ToString();
                 txtatm.Text = dtcln1.Rows[0]["activetimeofnumbermnts"].ToString();
                btnadd.Text = "Update";
            }
        }

        catch
        {
        }
    }
    protected void btnCheckCompany_Click(object sender, EventArgs e)
    {
       
        int i = 0;
        string str = "SELECT  * FROM   VaultDeviceMaster WHERE     (SrNumber = '" + txtsrnu.Text + "')";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            // lblmsg.Visible = true;
            lblCompanyIDAVl.Text = "Sorry This srnumber is already exist.Please try another.";
            lblCompanyIDAVl.ForeColor = System.Drawing.Color.Red;
            txtsrnu.Text = "";

        }
        else
        {
            lblCompanyIDAVl.Text = "Available for you.";
            lblCompanyIDAVl.ForeColor = System.Drawing.Color.Green;
        }

    }
    protected void txtsrnu_TextChanged(object sender, EventArgs e)
    {

    }
}
