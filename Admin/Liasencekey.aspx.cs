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

public partial class Admin_Liasencekey : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    string today;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        clearlabel();
        today = System.DateTime.Now.ToShortDateString();
        if (!IsPostBack)
        {
           
            SqlDataAdapter adp = new SqlDataAdapter("select CompanyId,CompanyName from CompanyMaster", con);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            ddlcompanyid.DataSource = ds;
            ddlcompanyid.DataTextField = "CompanyName";
            ddlcompanyid.DataValueField = "CompanyId";
            ddlcompanyid.DataBind();

            SqlDataAdapter adplkey = new SqlDataAdapter(@"select LicenseId,CompanyLicensekey from LicensekeyList where Issed='n'", con);
            DataSet dslkey = new DataSet();
            ddllickey.Items.Clear();
            adplkey.Fill(dslkey);
            ddllickey.DataSource = dslkey;
            ddllickey.DataTextField = "CompanyLicensekey";
            ddllickey.DataValueField = "LicenseId";
            ddllickey.DataBind();
            
            
        }
    }
    protected void ddlcompanyid_SelectedIndexChanged(object sender, EventArgs e)
    {
        //lacompanyname.Text = "";
        clearlabel();
        con.Open();
        SqlCommand cmd = new SqlCommand("select c.CompanyName,c.Address,c.Phone,c.Websiteurl,p.ProductName,pr.PricePlanName,l.LicenseKey from LicenseMaster l,CompanyMaster c, ProductMaster p, PricePlanMaster pr where c.ProductId=p.ProductId and c.PricePlanId=pr.PricePlanId and c.CompanyId=l.CompanyId and c.CompanyId=" + ddlcompanyid.SelectedItem.Value + "", con);
        SqlDataReader dr = cmd.ExecuteReader();
        
        if (dr.HasRows)
        {
            panforlickey.Visible = false ;
            dr.Read();
            //lacompanyname.Text = dr["CompanyName"].ToString();
            labcompadd.Text = dr["Address"].ToString();
            labcompphoneno.Text = dr["phone"].ToString();
            lbproductname.Text = dr["ProductName"].ToString();
            lbproductprizeplan.Text = dr["PricePlanName"].ToString();
            lbliacencekey.Text = dr["LicenseKey"].ToString();
            lburl.Text = dr["Websiteurl"].ToString();
            //hlurl.Text = dr["Websiteurl"].ToString();
            //hlurl.NavigateUrl = dr["Websiteurl"].ToString();
        }
        else 
        {
            
            panforlickey.Visible = true;
            
        }
        dr.Close();
    
    }
    protected void btnkeyissued_Click(object sender, EventArgs e)
    {
        /*check wether key is given or not*/
        con.Open();
        SqlCommand cmddrkey = new SqlCommand("select CompanyId,CompanyName from CompanyMaster  where CompanyId not in (select CompanyId from LicenseMaster)",con);
        SqlDataReader drkey = cmddrkey.ExecuteReader();
       
        if (drkey.HasRows)
        {
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO LicenseMaster(CompanyId,LicenseKey,LicenseDate) VALUES ('" + ddlcompanyid.SelectedItem.Value + "','" + ddllickey.SelectedItem.Text + "','" + today + "') ", con);
            cmd.ExecuteNonQuery();
            SqlCommand cmdupdateliclist = new SqlCommand("UPDATE LicensekeyList set Issed='y' where LicenseId='" + ddllickey.SelectedItem.Value + "'", con);
            cmdupdateliclist.ExecuteNonQuery();
            SqlDataAdapter adplkey = new SqlDataAdapter(@"select LicenseId,CompanyLicensekey from LicensekeyList where Issed='n'", con);
            DataSet dslkey = new DataSet();
            ddllickey.Items.Clear();
            adplkey.Fill(dslkey);
            ddllickey.DataSource = dslkey;
            ddllickey.DataTextField = "CompanyLicensekey";
            ddllickey.DataValueField = "LicenseId";
            ddllickey.DataBind();
            panforlickey.Visible = false;

            lblmsg.Text = "LicenseKey Issued";
        }
        else 
        {
 
        }
            drkey.Close();

       
           

    }
   
    protected void ddllickey_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void cblcompany_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
    protected void rbcompanylicense_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbcompanylicense.SelectedItem.Text == "ALL")
        {
            clearlabel();
            SqlDataAdapter adp = new SqlDataAdapter("select CompanyId,CompanyName from CompanyMaster", con);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            ddlcompanyid.DataSource = ds;
            ddlcompanyid.DataTextField = "CompanyName";
            ddlcompanyid.DataValueField = "CompanyId";
            ddlcompanyid.DataBind();
            panforlickey.Visible = false;
        }
        else if (rbcompanylicense.SelectedItem.Text == "LicenceKeyRemaing")
        {
            clearlabel();
            SqlDataAdapter adp = new SqlDataAdapter("select CompanyId,CompanyName from CompanyMaster  where CompanyId not in (select CompanyId from LicenseMaster) ", con);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            ddlcompanyid.DataSource = ds;
            ddlcompanyid.DataTextField = "CompanyName";
            ddlcompanyid.DataValueField = "CompanyId";
            ddlcompanyid.DataBind();
            panforlickey.Visible = true;
        }
        else if (rbcompanylicense.SelectedItem.Text == "LicencekeyGiven") 
        {
            clearlabel();
            SqlDataAdapter adp = new SqlDataAdapter("select CompanyId,CompanyName from CompanyMaster  where CompanyId in (select CompanyId from LicenseMaster) ", con);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            ddlcompanyid.DataSource = ds;
            ddlcompanyid.DataTextField = "CompanyName";
            ddlcompanyid.DataValueField = "CompanyId";
            ddlcompanyid.DataBind();
            panforlickey.Visible = false;
        }
    }

    public void clearlabel()
    {
        labcompadd.Text = "";
        labcompphoneno.Text = "";
        lbproductname.Text = "";
        lbproductprizeplan.Text = "";
        lbliacencekey.Text = "";
        lburl.Text = "";

    }
}
