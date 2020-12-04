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
public partial class ShoppingCart_Admin_UserControl_UControlWizardMaster1 : System.Web.UI.UserControl
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
      
            string stre = "select * from CompanyBusinessInfo Where comid='" + Session["Comid"] + "'";
            SqlCommand cmde = new SqlCommand(stre, con);
            SqlDataAdapter adpe = new SqlDataAdapter(cmde);
            DataTable dte = new DataTable();
            adpe.Fill(dte);
            if (dte.Rows.Count > 0)
            {
              
                if (dte.Rows[0]["both"].ToString() == "True")
                {
                    ImageButton2.Visible = true;
                    ImageButton4.Visible = true;
                    ImageButton6.Visible = true;
                }
                else if (dte.Rows[0]["onlyproduct"].ToString() == "True")
                {
                    ImageButton2.Visible = false;
                    ImageButton4.Visible = true; 
                    ImageButton6.Visible = false;
                }
                else if (dte.Rows[0]["onlyService"].ToString() == "True")
                {
                    ImageButton2.Visible = false;
                    ImageButton4.Visible = false;
                    ImageButton6.Visible = true;
                }
               
               
            }
            
        
        if (Session["pnlM"] != null)
        {
            
            ButtonColor(Convert.ToInt16(Session["pnlM"]));
          
        }
        else
        {
            Session["pnlM"] = "1";
            ButtonColor(Convert.ToInt16(Session["pnlM"]));
           
        }

    }
   protected void   ButtonColor( int  ButtonId)
    {
       switch (ButtonId)
        {
         

           case 1:
                ImageButton1.ImageUrl = "~/ShoppingCart/images/masterhover.png";
                ImageButton2.ImageUrl = "~/ShoppingCart/images/shipping2.png";
                ImageButton3.ImageUrl = "~/ShoppingCart/images/Account1.png";
                ImageButton4.ImageUrl = "~/ShoppingCart/images/Inventory1.png";
                ImageButton5.ImageUrl = "~/ShoppingCart/images/salespur1.png";
                ImageButton6.ImageUrl = "~/ShoppingCart/images/Services.png";
               break;


           case 2:
               ImageButton1.ImageUrl = "~/ShoppingCart/images/Master1.png";
               ImageButton2.ImageUrl = "~/ShoppingCart/images/shippinghover.png";
               ImageButton3.ImageUrl = "~/ShoppingCart/images/Account1.png";
               ImageButton4.ImageUrl = "~/ShoppingCart/images/Inventory1.png";
               ImageButton5.ImageUrl = "~/ShoppingCart/images/salespur1.png";
               ImageButton6.ImageUrl = "~/ShoppingCart/images/Services.png";
               break;


           case 3:
               ImageButton1.ImageUrl = "~/ShoppingCart/images/Master1.png";
               ImageButton2.ImageUrl = "~/ShoppingCart/images/shipping2.png";
               ImageButton3.ImageUrl = "~/ShoppingCart/images/accounthover.png";
               ImageButton4.ImageUrl = "~/ShoppingCart/images/Inventory1.png";
               ImageButton5.ImageUrl = "~/ShoppingCart/images/salespur1.png";
               ImageButton6.ImageUrl = "~/ShoppingCart/images/Services.png";
               break;


           case 4:
               ImageButton1.ImageUrl = "~/ShoppingCart/images/Master1.png";
               ImageButton2.ImageUrl = "~/ShoppingCart/images/shipping2.png";
               ImageButton3.ImageUrl = "~/ShoppingCart/images/Account1.png";
               ImageButton4.ImageUrl = "~/ShoppingCart/images/inventoryhover.png";
               ImageButton5.ImageUrl = "~/ShoppingCart/images/salespur1.png";
               ImageButton6.ImageUrl = "~/ShoppingCart/images/Services.png";
          
               break;


           case 5:
               ImageButton1.ImageUrl = "~/ShoppingCart/images/Master1.png";
               ImageButton2.ImageUrl = "~/ShoppingCart/images/shipping2.png";
               ImageButton3.ImageUrl = "~/ShoppingCart/images/Account1.png";
               ImageButton4.ImageUrl = "~/ShoppingCart/images/Inventory1.png";
               ImageButton5.ImageUrl = "~/ShoppingCart/images/salespurhover.png";
               ImageButton6.ImageUrl = "~/ShoppingCart/images/Services.png";
              
               break;

           case 6:
               ImageButton1.ImageUrl = "~/ShoppingCart/images/Master1.png";
               ImageButton2.ImageUrl = "~/ShoppingCart/images/shipping2.png";
               ImageButton3.ImageUrl = "~/ShoppingCart/images/Account1.png";
               ImageButton4.ImageUrl = "~/ShoppingCart/images/Inventory1.png";
               ImageButton5.ImageUrl = "~/ShoppingCart/images/salespur.png";
               ImageButton6.ImageUrl = "~/ShoppingCart/images/Serviceshover.png";
              
               break;
        }
    }





   protected void ImgBtnStep01_1_Click(object sender, ImageClickEventArgs e)
   {
       Session["pnlM"] = "1";
       ButtonColor(Convert.ToInt16(Session["pnlM"]));
       Response.Redirect("~/ShoppingCart/Admin/Wizardcompanyinformation.aspx");
     
    
   }
   protected void ImgBtnStep01_2_Click(object sender, ImageClickEventArgs e)
   {
       Session["pnlM"] = "2";
       ButtonColor(Convert.ToInt16(Session["pnlM"]));
       Response.Redirect("~/ShoppingCart/Admin/wzShippingManager.aspx");
       //Session["pnl1"] = "21";
       //ButtonColor(Convert.ToInt16(Session["pnl1"]));
   }
   protected void ImgBtnStep01_3_Click(object sender, ImageClickEventArgs e)
   {
       Session["pnlM"] = "3";
       ButtonColor(Convert.ToInt16(Session["pnlM"]));
       Response.Redirect("~/ShoppingCart/Admin/wzClassType.aspx");
       //Session["pnl1"] = "31";
       //ButtonColor(Convert.ToInt16(Session["pnl1"]));
   }

   protected void ImgBtnStep01_4_Click(object sender, ImageClickEventArgs e)
   {
       ImageButton2.Visible = false;
       ImageButton4.Visible = false;
       Session["pnlM"] = "4";
       ButtonColor(Convert.ToInt16(Session["pnlM"]));
       Response.Redirect("~/ShoppingCart/Admin/wzInventorySiteMasterTbl.aspx");
       //Session["pnl1"] = "41";
       //ButtonColor(Convert.ToInt16(Session["pnl1"]));
   }
   protected void ImgBtnStep01_5_Click(object sender, ImageClickEventArgs e)
   {
       Session["pnlM"] = "5";
       ButtonColor(Convert.ToInt16(Session["pnlM"]));
       Response.Redirect("~/ShoppingCart/Admin/wzApplyVolumeDiscount.aspx");
       ////Session["pnl1"] = "51";
       ////ButtonColor(Convert.ToInt16(Session["pnl1"]));
   }
   protected void ImgBtnStep01_6_Click(object sender, ImageClickEventArgs e)
   {
      
       Session["pnlM"] = "6";
       ButtonColor(Convert.ToInt16(Session["pnlM"]));
       Response.Redirect("~/ShoppingCart/Admin/wzServicecategory.aspx");
     
   }

   










}
