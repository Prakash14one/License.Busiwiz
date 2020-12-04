
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

public partial class Master_Main : System.Web.UI.MasterPage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    public string cssdyn="Registrationstyle.css";
    public string VBF = "";
    protected void Page_PreInit(object sender, EventArgs e)
    {
        
    }
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            string PrId = "";
            if (Request.QueryString["id"] != null || Request.QueryString["view"] != null)
            {
                if (Request.QueryString["id"] != null)
                {
                    Session["ProductId"] = Request.QueryString["id"];
                }
                string str = " select distinct PortalMasterTbl.*,ClientMaster.CompanyName ,StateMasterTbl.StateName,CountryMaster.CountryName from  ClientMaster inner join ProductMaster on ProductMaster.ClientMasterId=ClientMaster.ClientMasterId inner join VersionInfoMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId inner join PortalMasterTbl on PortalMasterTbl.ProductId=ProductMaster.ProductId inner join StateMasterTbl on StateMasterTbl.StateId=PortalMasterTbl.StateId inner join CountryMaster on StateMasterTbl.CountryId=CountryMaster.CountryId  where Upper(PortalMasterTbl.PortalName)='" + Session["PortalName"] + "' and VersionInfoId='" + Session["ProductId"] + "' ";//3-15 
              //  string str = " select ClientMaster.*,ProductMaster.ProductId,PortalMasterTbl.LogoPath  from  ClientMaster inner join ProductMaster on ProductMaster.ClientMasterId=ClientMaster.ClientMasterId where ProductMaster.ProductId='" + Request.QueryString["id"] + "' ";
                SqlCommand cmd = new SqlCommand(str, con);
                 SqlDataAdapter adp = new SqlDataAdapter(cmd);
                  DataTable dt = new DataTable();
                  adp.Fill(dt);

                  if (dt.Rows.Count > 0)
                  {
                      Session["ProductId"] = dt.Rows[0]["ProductId"].ToString();
                      ImageButton1.ImageUrl = "~/images/" + dt.Rows[0]["LogoPath"].ToString() + " ";
                      lblcompanyname.Text = dt.Rows[0]["CompanyName"].ToString();
                      Session["Clientname"] = dt.Rows[0]["CompanyName"].ToString();
                      lblportalname.Text = dt.Rows[0]["PortalName"].ToString();
                      lbladresss.Text = dt.Rows[0]["Address1"].ToString();
                      Session["PrId"] = dt.Rows[0]["ProductId"].ToString();
                      string ext = "";
                      string tfext = "";
                      if (Convert.ToString(dt.Rows[0]["Supportteamphonenoext"].ToString()) != "" && Convert.ToString(dt.Rows[0]["Supportteamphonenoext"].ToString()) != null)
                      {
                          ext = " ext " + dt.Rows[0]["Supportteamphonenoext"].ToString();
                      }

                      if (Convert.ToString(dt.Rows[0]["Tollfreeext"].ToString()) != "" && Convert.ToString(dt.Rows[0]["Tollfreeext"].ToString()) != null)
                      {
                          tfext = " ext " + dt.Rows[0]["Tollfreeext"].ToString();
                      }
                      lblphoneno.Text = dt.Rows[0]["Supportteamphoneno"].ToString() + ext;
                      lbltollfree.Text = dt.Rows[0]["Tollfree"].ToString() + tfext;
                      lblemail.Text = dt.Rows[0]["Supportteamemailid"].ToString();
                      lblcity.Text = dt.Rows[0]["City"].ToString();
                      lblstate.Text = dt.Rows[0]["StateName"].ToString();
                      lblcountry.Text = dt.Rows[0]["CountryName"].ToString();
                      LinkButton1.Text = dt.Rows[0]["Portalmarketingwebsitename"].ToString();
                    
                      
                  }

             }
             if (Request.QueryString["cid"] != null)
             {
                 string str = " select distinct PortalMasterTbl.*,ClientMaster.CompanyName ,StateMasterTbl.StateName,CountryMaster.CountryName from  ClientMaster inner join ProductMaster on ProductMaster.ClientMasterId=ClientMaster.ClientMasterId inner join VersionInfoMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId inner join PortalMasterTbl on PortalMasterTbl.ProductId=ProductMaster.ProductId inner join StateMasterTbl on StateMasterTbl.StateId=PortalMasterTbl.StateId inner join CountryMaster on StateMasterTbl.CountryId=CountryMaster.CountryId  where Upper(PortalMasterTbl.PortalName)='" + Session["PortalName"] + "' and VersionInfoId='" + Session["pverid"] + "' "; //" + Request.QueryString["id"] + "
                 //string str = " select ClientMaster.*,ProductMaster.ProductId,PortalMasterTbl.LogoPath  from  ClientMaster inner join ProductMaster on ProductMaster.ClientMasterId=ClientMaster.ClientMasterId where ProductMaster.ProductId='" + Request.QueryString["id"] + "' ";
                 SqlCommand cmd = new SqlCommand(str, con);
                 SqlDataAdapter adp = new SqlDataAdapter(cmd);
                 DataTable dt = new DataTable();
                 adp.Fill(dt);

                 if (dt.Rows.Count > 0)
                 {
                     Session["ProductId"] = dt.Rows[0]["ProductId"].ToString();
                     ImageButton1.ImageUrl = "~/images/" + dt.Rows[0]["LogoPath"].ToString() + " ";
                     lblcompanyname.Text = dt.Rows[0]["CompanyName"].ToString();
                     lblportalname.Text = dt.Rows[0]["PortalName"].ToString();
                     lbladresss.Text = dt.Rows[0]["Address1"].ToString();
                     Session["PrId"] = dt.Rows[0]["ProductId"].ToString();
                     string ext = "";
                     string tfext = "";
                     if (Convert.ToString(dt.Rows[0]["Supportteamphonenoext"].ToString()) != "" && Convert.ToString(dt.Rows[0]["Supportteamphonenoext"].ToString()) != null)
                     {
                         ext = " ext " + dt.Rows[0]["Supportteamphonenoext"].ToString();
                     }

                     if (Convert.ToString(dt.Rows[0]["Tollfreeext"].ToString()) != "" && Convert.ToString(dt.Rows[0]["Tollfreeext"].ToString()) != null)
                     {
                         tfext = " ext " + dt.Rows[0]["Tollfreeext"].ToString();
                     }
                     lblphoneno.Text = dt.Rows[0]["Supportteamphoneno"].ToString() + ext;
                     lbltollfree.Text = dt.Rows[0]["Tollfree"].ToString() + tfext;
                     lblemail.Text = dt.Rows[0]["Supportteamemailid"].ToString();
                     lblcity.Text = dt.Rows[0]["City"].ToString();
                     lblstate.Text = dt.Rows[0]["StateName"].ToString();
                     lblcountry.Text = dt.Rows[0]["CountryName"].ToString();
                     LinkButton1.Text = dt.Rows[0]["Portalmarketingwebsitename"].ToString();


                 }

             }

             if (Request.QueryString["pid"] != null)
             {
                 Session["ProductId"] = Request.QueryString["pid"];

                 string str = " select PortalMasterTbl.*,ClientMaster.CompanyName ,StateMasterTbl.StateName,CountryMaster.CountryName from  ClientMaster inner join ProductMaster on ProductMaster.ClientMasterId=ClientMaster.ClientMasterId  inner join PricePlanMaster on PricePlanMaster.ProductId=ProductMaster.ProductId inner join Priceplancategory on Priceplancategory.ID=PricePlanMaster.PriceplancatId inner join PortalMasterTbl on PortalMasterTbl.Id=Priceplancategory.PortalId inner join StateMasterTbl on StateMasterTbl.StateId=PortalMasterTbl.StateId inner join CountryMaster on StateMasterTbl.CountryId=CountryMaster.CountryId   where PricePlanMaster.PricePlanId='" + Session["ProductId"] + "' ";
                 SqlCommand cmd = new SqlCommand(str, con);
                 SqlDataAdapter adp = new SqlDataAdapter(cmd);
                 DataTable dt = new DataTable();
                 adp.Fill(dt);

                 if (dt.Rows.Count > 0)
                 {
                     Session["PrId"] = dt.Rows[0]["ProductId"].ToString();
                     ImageButton1.ImageUrl = "~/images/" + dt.Rows[0]["LogoPath"].ToString() + " ";
                     lblcompanyname.Text = dt.Rows[0]["CompanyName"].ToString();
                     lblportalname.Text = dt.Rows[0]["PortalName"].ToString();
                     lbladresss.Text = dt.Rows[0]["Address1"].ToString();

                     string ext = "";
                     string tfext = "";
                     if (Convert.ToString(dt.Rows[0]["Supportteamphonenoext"].ToString()) != "" && Convert.ToString(dt.Rows[0]["Supportteamphonenoext"].ToString()) != null)
                     {
                         ext = " ext " + dt.Rows[0]["Supportteamphonenoext"].ToString();
                     }

                     if (Convert.ToString(dt.Rows[0]["Tollfreeext"].ToString()) != "" && Convert.ToString(dt.Rows[0]["Tollfreeext"].ToString()) != null)
                     {
                         tfext = " ext " + dt.Rows[0]["Tollfreeext"].ToString();
                     }
                     lblphoneno.Text = dt.Rows[0]["Supportteamphoneno"].ToString() + ext;
                     lbltollfree.Text = dt.Rows[0]["Tollfree"].ToString() + tfext;
                     lblemail.Text = dt.Rows[0]["Supportteamemailid"].ToString();
                     lblcity.Text = dt.Rows[0]["City"].ToString();
                     lblstate.Text = dt.Rows[0]["StateName"].ToString();
                     lblcountry.Text = dt.Rows[0]["CountryName"].ToString();
                     LinkButton1.Text = dt.Rows[0]["Portalmarketingwebsitename"].ToString();
                  
                 }
                

             }

             if (Request.QueryString["ProductId"] != null)
             {
                 Session["ProductId"] = Request.QueryString["ProductId"];

                 string str = " select ClientMaster.* from  ClientMaster inner join ProductMaster on ProductMaster.ClientMasterId=ClientMaster.ClientMasterId where ProductMaster.ProductId='" + Session["ProductId"] + "' ";
                 SqlCommand cmd = new SqlCommand(str, con);
                 SqlDataAdapter adp = new SqlDataAdapter(cmd);
                 DataTable dt = new DataTable();
                 adp.Fill(dt);
                 if (dt.Rows.Count > 0)
                 {                     
                     ImageButton1.ImageUrl = "~/images/" + dt.Rows[0]["CompanyLogo"].ToString() + " ";                     
                     lblcompanyname.Text = dt.Rows[0]["CompanyName"].ToString();
                     lbladresss.Text = dt.Rows[0]["Address1"].ToString();
                     lblphoneno.Text = dt.Rows[0]["Phone1"].ToString();
                     lblemail.Text = dt.Rows[0]["EmailID"].ToString();
                 }
                 GNCLS();
             }
        }
        if (Convert.ToString(Session["VBF"]) != "")
        {
            VBF = Convert.ToString(Session["VBF"]);
        }
        else
        {
            GNCLS();
        }

    }
    protected void GNCLS()
    { if (Convert.ToString(Session["PortalName"]) != "")
             {
                 string str = " select Colorportal  from  PortalMasterTbl  where Upper(PortalMasterTbl.PortalName)='" + Session["PortalName"] + "' and ProductId='" + Session["PrId"] + "' ";

                 //  string str = " select ClientMaster.*,ProductMaster.ProductId,PortalMasterTbl.LogoPath  from  ClientMaster inner join ProductMaster on ProductMaster.ClientMasterId=ClientMaster.ClientMasterId where ProductMaster.ProductId='" + Request.QueryString["id"] + "' ";
                 SqlCommand cmd = new SqlCommand(str, con);
                 SqlDataAdapter adp = new SqlDataAdapter(cmd);
                 DataTable dt = new DataTable();
                 adp.Fill(dt);
                 if (dt.Rows.Count > 0)
                 {
                     VBF = Convert.ToString(dt.Rows[0]["Colorportal"]);
                     Session["VBF"] = VBF;
                     //VBF = "#d4f8ba";
                 }
                 else
                 {
                     Session["VBF"] = "#65bcd0";
                 }
             }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        string te = "http://" + LinkButton1.Text;

        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        string te = "http://" + LinkButton1.Text;
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void lblportalname_Click(object sender, EventArgs e)
    {
        string te = "http://" + LinkButton1.Text;
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
}
