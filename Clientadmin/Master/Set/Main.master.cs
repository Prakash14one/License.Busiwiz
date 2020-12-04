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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
             if (Request.QueryString["id"] != "")
            {
                Session["ProductId"] = Request.QueryString["id"];
                 


                string str = " select ClientMaster.* from  ClientMaster inner join ProductMaster on ProductMaster.ClientMasterId=ClientMaster.ClientMasterId where ProductMaster.ProductId='" + Session["ProductId"] + "' ";
                SqlCommand cmd = new SqlCommand(str, con);
                 SqlDataAdapter adp = new SqlDataAdapter(cmd);
                  DataTable dt = new DataTable();
                  adp.Fill(dt);

                  if (dt.Rows.Count > 0)
                  {
                     
                      ImageButton1.ImageUrl="~/images/" + dt.Rows[0]["CompanyLogo"].ToString()+" ";                     
                      lblcompanyname.Text = dt.Rows[0]["CompanyName"].ToString();
                      lbladresss.Text = dt.Rows[0]["Address1"].ToString();
                      lblphoneno.Text = dt.Rows[0]["Phone1"].ToString();
                      lblemail.Text = dt.Rows[0]["EmailID"].ToString();
                     
                      
                  }

             }
             if (Request.QueryString["pid"] != "")
             {
                 Session["ProductId"] = Request.QueryString["pid"];

                 string str = " select PortalMasterTbl.*,ClientMaster.CompanyName ,StateMasterTbl.StateName,CountryMaster.CountryName from  ClientMaster inner join ProductMaster on ProductMaster.ClientMasterId=ClientMaster.ClientMasterId  inner join PricePlanMaster on PricePlanMaster.ProductId=ProductMaster.ProductId inner join Priceplancategory on Priceplancategory.ID=PricePlanMaster.PriceplancatId inner join PortalMasterTbl on PortalMasterTbl.Id=Priceplancategory.PortalId inner join StateMasterTbl on StateMasterTbl.StateId=PortalMasterTbl.StateId inner join CountryMaster on StateMasterTbl.CountryId=CountryMaster.CountryId   where PricePlanMaster.PricePlanId='" + Session["ProductId"] + "' ";
                 SqlCommand cmd = new SqlCommand(str, con);
                 SqlDataAdapter adp = new SqlDataAdapter(cmd);
                 DataTable dt = new DataTable();
                 adp.Fill(dt);

                 if (dt.Rows.Count > 0)
                 {

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

             if (Request.QueryString["ProductId"] != "")
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

             }


           

        }

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        string te = "http://" + LinkButton1.Text;

        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
}
