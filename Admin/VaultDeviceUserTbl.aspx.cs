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

public partial class Admin_VaultDeviceUserTbl : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
      
        lblmsg.Text = "";
        if (!IsPostBack)
        {
            fillclient();
            fillcustomer();
            fillproductv();
            fillsrno();
            VaultDeviceusertbl();
            fillgrid();

        }
    }
    protected void fillsrno()
    {
        string strcln = "SELECT * from VaultDeviceMaster order by SrNumber";

        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        ddlsrno.DataSource = dtcln;
        ddlsrno.DataValueField = "SrNumber";
        ddlsrno.DataTextField = "SrNumber";
        ddlsrno.DataBind();
        ddlsrno.Items.Insert(0, "-Select-");
        ddlsrno.Items[0].Value = "0";
    }
    protected void fillclient()
    {
        string strcln = "SELECT distinct BusiControllerMasterTBl.ClinetMasterID,BusiControllerMasterTBl.ClinetMasterID + ' : ' + ClientMaster.CompanyName as clientidname FROM BusiControllerMasterTBl inner join ClientMaster on BusiControllerMasterTBl.ClinetMasterID=ClientMaster.ClientMasterId order by clientidname";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        ddlclientid.DataSource = dtcln;
        ddlclientid.DataValueField = "ClinetMasterID";
        ddlclientid.DataTextField = "clientidname";
        ddlclientid.DataBind();
        ddlclientid.Items.Insert(0, "-Select-");
        ddlclientid.Items[0].Value = "0";
    }
    protected void fillcustomer()
    {
        string strcln = "SELECT distinct BusiControllerMasterTBl.compnaymasterid,BusiControllerMasterTBl.compnaymasterid + ' : ' + CompanyMaster.CompanyName as comidname FROM BusiControllerMasterTBl inner join CompanyMaster on BusiControllerMasterTBl.compnaymasterid=CompanyMaster.CompanyId  order  by comidname";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        ddlcompanyid.DataSource = dtcln;
        ddlcompanyid.DataValueField = "compnaymasterid";
        ddlcompanyid.DataTextField = "comidname";
        ddlcompanyid.DataBind();
        ddlcompanyid.Items.Insert(0, "-Select-");
        ddlcompanyid.Items[0].Value = "0";
    }
    protected void fillproductv()
    {
        string strcln = "SELECT distinct productMaster.ProductId, VersionInfoMaster.VersionInfoId,ProductMaster.ProductName + ' : ' + VersionInfoMaster.VersionInfoName as productversion FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join BusiControllerMasterTBl on BusiControllerMasterTBl.versioninfomasterid=VersionInfoMaster.VersionInfoId order  by productversion";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        ddlpv.DataSource = dtcln;
        ddlpv.DataValueField = "VersionInfoId";
        ddlpv.DataTextField = "productversion";
        ddlpv.DataBind();
        ddlpv.Items.Insert(0, "-Select-");
        ddlpv.Items[0].Value = "0";
    }
    protected void VaultDeviceusertbl()
    {
        try
        {
            string st = "";
            string strcln1 = "SELECT * from VaultDeviceUserTbl";
            SqlCommand cmdcln1 = new SqlCommand(strcln1, con);
            DataTable dtcln1 = new DataTable();
            SqlDataAdapter adpcln1 = new SqlDataAdapter(cmdcln1);
            adpcln1.Fill(dtcln1);
            for (int i = 0; i < dtcln1.Rows.Count; i++)
            {
                st += "'" + dtcln1.Rows[i]["vaultdevicemasterid"].ToString() + "',";
            }
            if (st.Length > 0)
            {
                st = st.Remove(st.Length - 1);
            }
            else
            {
                st = "0";
            }
            
            string strcln = "SELECT * from VaultDeviceMaster where Id not in(" + st + ") and Active='True' order by name";
            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);
           
            ddldvm.DataSource = dtcln;
            ddldvm.DataValueField = "Id";
            ddldvm.DataTextField = "Name";
            ddldvm.DataBind();
            ddldvm.Items.Insert(0, "-Select-");
            ddldvm.Items[0].Value = "0";
        }
        catch
        {
        }
    }
    protected void fillgrid()
    {
        try
        {
            GridView1.DataSource = null;


            string strcln = "Select VaultDeviceUserTbl.*,VaultDeviceMaster.Name,VaultDeviceMaster.Active from VaultDeviceMaster  inner join VaultDeviceUserTbl  on VaultDeviceMaster.Id=VaultDeviceUserTbl.vaultdevicemasterid where Active='True' order by VaultDeviceUserTbl.Userid";
            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);
            GridView1.DataSource = dtcln;

            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        string str = "";
        try
        {
            if (ddldvm.SelectedItem.Text != "-Select-")
            {
                if (btnadd.Text == "Update")
                {
                    str = "Update VaultDeviceUserTbl Set clientid='" + ddlclientid.SelectedValue + "',ProductVersionID='" + ddlpv.SelectedValue + "',companyid='" + ddlcompanyid.SelectedValue + "',SrNumber='"+ddlsrno.SelectedValue+"',userid='" + txtuserid.Text.Trim() + "',vaultdevicemasterid='" + ddldvm.SelectedValue + "' where Id='" + ViewState["ID"] + "'";

                    lblmsg.Text = "Record Updated Successfully.";
                }
                else
                {

                    str = "INSERT INTO VaultDeviceUserTbl(clientid,ProductVersionID,companyid,SrNumber,userid,vaultdevicemasterid)values( " +
                    " '" + ddlclientid.SelectedValue + "','"+ddlpv.SelectedValue+"','"+ddlsrno.SelectedValue+"','" + ddlcompanyid.SelectedValue + "','" + txtuserid.Text.Trim() + "','" + ddldvm.SelectedValue + "')";
                    lblmsg.Text = "Record insert Successfully.";
                }
                SqlCommand cmd = new SqlCommand(str, con);
                DataTable dt = new DataTable();
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                fillgrid();
                btnadd.Text = "Submit";
                lblmsg.Visible = true;
                clearall();
                VaultDeviceusertbl();
            }
            else
            {
                lblmsg.Text = "Please select vault Device Master Name";
                lblmsg.Visible = true;
            }
            
        }
            
        catch
        {
        }
     
    }
    protected void clearall()
    {
        ddlpv.SelectedIndex = 0;
        ddlcompanyid.SelectedIndex = 0;
        ddlclientid.SelectedIndex = 0;
        txtuserid.Text = "";
        ddldvm.SelectedIndex = 0;
        ddlsrno.SelectedIndex = 0;
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

                strcln1 = "Select * from VaultDeviceUserTbl where Id='" + i.ToString() + "'";
                SqlCommand cmdcln1 = new SqlCommand(strcln1, con);
                DataTable dtcln1 = new DataTable();
                SqlDataAdapter adpcln1 = new SqlDataAdapter(cmdcln1);
                adpcln1.Fill(dtcln1);
                if (dtcln1.Rows.Count > 0)
                    ddlclientid.SelectedIndex = ddlclientid.Items.IndexOf(ddlclientid.Items.FindByValue(dtcln1.Rows[0]["clientid"].ToString()));
                ddlcompanyid.SelectedIndex = ddlcompanyid.Items.IndexOf(ddlcompanyid.Items.FindByValue(dtcln1.Rows[0]["companyid"].ToString()));
                ddlsrno.SelectedIndex = ddlsrno.Items.IndexOf(ddlsrno.Items.FindByValue(dtcln1.Rows[0]["SrNumber"].ToString()));
                txtuserid.Text = dtcln1.Rows[0]["userid"].ToString();
                    ddldvm.SelectedIndex = ddldvm.Items.IndexOf(ddldvm.Items.FindByValue(dtcln1.Rows[0]["vaultdevicemasterid"].ToString()));
                    ddlpv.SelectedIndex = ddlpv.Items.IndexOf(ddlpv.Items.FindByValue(dtcln1.Rows[0]["ProductVersionID"].ToString()));
                
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
        string str = "SELECT  * FROM   VaultDeviceUserTbl  WHERE     (userid = '" + txtuserid.Text.Trim() + "')";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            string update = "UPDATE VaultDeviceMaster Set Active='False' where SrNumber='" + ddlsrno.SelectedValue + "'";
            SqlCommand cmd1 = new SqlCommand(update, con);
            con.Open();
            cmd1.ExecuteNonQuery();
            con.Close();
            lblCompanyIDAVl.Text = "Your old device inoperative.";
            lblCompanyIDAVl.ForeColor = System.Drawing.Color.Red;
            fillgrid();

        }
        else
        {
            lblCompanyIDAVl.Text = " Not Available for old Device.";
            lblCompanyIDAVl.ForeColor = System.Drawing.Color.Green;
        }
    }
}
