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

public partial class Master_Login : System.Web.UI.MasterPage
{
    SqlConnection con;
    SqlConnection busiclient;
    protected void Page_Load(object sender, EventArgs e)
    {
        //Session["Comid"] = "1133";
        con = new SqlConnection(PageConn.connnn);

        if (!IsPostBack)
        {
            string[] separator1 = new string[] { "." };
            string[] strSplitArr1 = Request.Url.Host.Split(separator1, StringSplitOptions.RemoveEmptyEntries);

           //if(strSplitArr1.Length>0)
           //{
             
           //     if (con != null)
           //     {
           //         if (Convert.ToString(con.ConnectionString) == "")
           //         {
           //             Session["Comid"] = strSplitArr1[0].ToString();
           //             PageConn cnd = new PageConn();
           //             con = new SqlConnection(PageConn.connnn);
           //         }
           //     }
           //     else
           //     {
           //        // Session["Comid"] = "1133";
           //         Session["Comid"] = strSplitArr1[0].ToString();
           //         PageConn cnd = new PageConn();
           //         con = new SqlConnection(PageConn.connnn);
           //     }

           //   //  SqlDataAdapter dafff = new SqlDataAdapter("select Top(1) LogoUrl,WHId,SiteUrl from CompanyMaster inner join  CompanyWebsitMaster on CompanyWebsitMaster.CompanyId=CompanyMaster.CompanyId where CompanyMaster.Compid='1133' Order by CompanyWebsiteMasterId", con);
           //     SqlDataAdapter dafff = new SqlDataAdapter("select Top(1) LogoUrl,WHId,SiteUrl from CompanyMaster inner join  CompanyWebsitMaster on CompanyWebsitMaster.CompanyId=CompanyMaster.CompanyId where CompanyMaster.Compid='" + Convert.ToString(Session["Comid"]) + "' Order by CompanyWebsiteMasterId", con);
           //     DataTable dtfff = new DataTable();
           //     dafff.Fill(dtfff);

           //     if (dtfff.Rows.Count > 0)
           //     {
           //         mainloginlogo.ImageUrl = "~/ShoppingCart/images/" + dtfff.Rows[0]["LogoUrl"].ToString();


           //         string straddr = "select CompanyWebsiteAddressMaster.*,WareHouseMaster.Name as BName,CityMasterTbl.CityName,StateMasterTbl.Statename,CountryMaster.CountryName from CompanyWebsiteAddressMaster inner join WareHouseMaster on WareHouseMaster.WareHouseId=CompanyWebsiteAddressMaster.CompanyWebsiteMasterId inner join AddressTypeMaster on AddressTypeMaster.AddressTypeMasterId=CompanyWebsiteAddressMaster.AddressTypeMasterId inner join CountryMaster on " +
           //        "CountryMaster.CountryId=CompanyWebsiteAddressMaster.Country inner join StateMasterTbl on " +
           //        "StateMasterTbl.StateId=CompanyWebsiteAddressMaster.State inner join CityMasterTbl on " +
           //        "CityMasterTbl.CityId=CompanyWebsiteAddressMaster.City where WareHouseMaster.WareHouseId='" + dtfff.Rows[0]["WHId"] + "' and AddressTypeMaster.Name='Business Address' ";
           //         SqlDataAdapter adpaddr = new SqlDataAdapter(straddr, con);
           //         DataTable dtaddr = new DataTable();
           //         adpaddr.Fill(dtaddr);

           //         if (dtaddr.Rows.Count > 0)
           //         {
           //             busn.Text = Convert.ToString(dtaddr.Rows[0]["BName"]);
           //             lbladdr.Text = Convert.ToString(dtaddr.Rows[0]["BName"]) + ", " + Convert.ToString(dtaddr.Rows[0]["Address1"]) + ", " + Convert.ToString(dtaddr.Rows[0]["CityName"]) + ", " + Convert.ToString(dtaddr.Rows[0]["Statename"]) + ", " + Convert.ToString(dtaddr.Rows[0]["CountryName"]);
           //             if (Convert.ToString(dtaddr.Rows[0]["Zip"]) != "")
           //                {
           //                    lbladdr.Text = lbladdr.Text + ", " + Convert.ToString(dtaddr.Rows[0]["Zip"]);
           //                }

           //             if (Convert.ToString(dtaddr.Rows[0]["Phone1"]) != "")
           //                {
           //                    lbladdr.Text = lbladdr.Text + ", " + Convert.ToString(dtaddr.Rows[0]["Phone1"]);
           //                }
           //             if (Convert.ToString(dtaddr.Rows[0]["Email"]) != "")
           //                {
           //                    lbladdr.Text = lbladdr.Text+ ", " + Convert.ToString(dtaddr.Rows[0]["Email"]);
           //                }
           //             if(Convert.ToString(dtfff.Rows[0]["SiteUrl"])!="")
           //                {
           //                     lbladdr.Text = lbladdr.Text +", "+  Convert.ToString(dtfff.Rows[0]["SiteUrl"]);
           //                }
           //         }

                  
           //     }
               
           // }
          
           if (strSplitArr1.Length > 1)
           
            {
              
                if (strSplitArr1[1].ToString() == "onlineaccounts")
                {
                    imgsitel.ImageUrl = "~/images/onlineaccounts.jpg";
                   
                }
                else if (strSplitArr1[1].ToString() == "ifilecabinet")
                {
                    imgsitel.ImageUrl = "~/images/ifilecabinet.jpg";
                   
                }
                else if (strSplitArr1[1].ToString() == "eplaza.us")
                {
                    mainloginlogo.ImageUrl = "~/images/eplaza.jpg";
                 
                }
                else if (strSplitArr1[1].ToString() == "iofficemanager")
                {
                    imgsitel.ImageUrl = "~/images/iofficemanager.jpg";
                
                }
                else if (strSplitArr1[1].ToString() == "itimekeeper")
                {
                    imgsitel.ImageUrl = "~/images/itimekeeper.jpg";
                    
                }
                else if (strSplitArr1[1].ToString() == "ipayrollmanager")
                {
                    imgsitel.ImageUrl = "~/images/ipayrollmanager.jpg";
                  
                }
                else if (strSplitArr1[1].ToString() == "onlinemanagers")
                {
                    imgsitel.ImageUrl = "~/images/onlinemanagers.jpg";

                }
                else if (strSplitArr1[1].ToString() == "busiwiz")
                {
                    imgsitel.ImageUrl = "~/images/busiwiz.jpg";
                   
                }
                //else
                //{
                //    imgsitel.ImageUrl = "~/images/onlineaccounts.jpg";
                //    //imgsitel.ImageUrl = "~/images/itimekeeper.jpg";
           
                //}
                if (Convert.ToString(imgsitel.ImageUrl) == "")
                {
                    imgsitel.ImageUrl = "~/images/onlineaccounts.jpg";
                }
               
            }
           if (imgsitel.ImageUrl == "")
           {

               imgsitel.ImageUrl = "~/images/onlineaccounts.jpg";
              
           }
           Session["ownur"] = imgsitel.ImageUrl;

           if (Convert.ToString(mainloginlogo.ImageUrl) == "")
           {
               mainloginlogo.ImageUrl = "~/images/OALOGO.jpg";
           }
        }

    }
   
}
