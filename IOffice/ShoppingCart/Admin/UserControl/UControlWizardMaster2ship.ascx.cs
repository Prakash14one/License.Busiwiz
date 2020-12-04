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

public partial class ShoppingCart_Admin_UserControl_UControlWizardMaster2ship : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

       
                ButtonColor(Convert.ToInt16(Session["pnl2"]));
            

    }
   protected void   ButtonColor( int  ButtonId)
    {
       switch (ButtonId)
        {
           // master panel
            
            case 21:
                ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/Companynamesele.png";
                ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/Selectmethod.png";
                ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/Freerule.png";
                ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/Handlngchrg.png";
              //  ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/2.5.png";
             //   ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/2.6.png";
                
                break;
            case 22:
                ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/Companyname.png";
                ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/Selectmethodsele.png";
                ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/Freerule.png";
                ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/Handlngchrg.png";
              //  ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/2.5.png";
              // ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/2.6.png";
                
                break;
            case 23:
                ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/Companyname.png";
                ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/Selectmethod.png";
                ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/Freerulesele.png";
                ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/Handlngchrg.png";
               // ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/2.5.png";
              //  ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/2.6.png";
                break;
            case 24:
                ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/Companyname.png";
                ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/Selectmethod.png";
                ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/Freerule.png";
                ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/Handlngchrgsele.png";
               // ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/2.5.png";
              //  ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/2.6.png";
                break;
                
            //case 25:
            //    ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/2.1.png";
            //    ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/2.2.png";
            //    ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/2.3.png";
            //    ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/2.4.png";
            //   // ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/2.5hover.png";
            //   // ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/2.6.png";
            //    break;
            //case 26:
            //    ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/2.1.png";
            //    ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/2.2.png";
            //    ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/2.3.png";
            //    ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/2.4.png";
            //   // ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/2.5.png";
            //  //ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/2.6hover.png";
            //    break;

            //   //Inventory panel

           


        }
    }





   
    protected void ImgBtnStep1_1_Click(object sender, ImageClickEventArgs e)
    {
        Session["pnl2"] = "21";
        ButtonColor(Convert.ToInt16(Session["pnl2"]));
        Response.Redirect("~/ShoppingCart/Admin/wzShippingManager.aspx");

    }
    protected void ImgBtnStep1_2_Click(object sender, ImageClickEventArgs e)
    {
        Session["pnl2"] = "22";
        ButtonColor(Convert.ToInt16(Session["pnl2"]));
        Response.Redirect("~/ShoppingCart/Admin/wzShippingMethod.aspx");

    }
    protected void ImgBtnStep1_3_Click(object sender, ImageClickEventArgs e)
    {
        Session["pnl2"] = "23";
        ButtonColor(Convert.ToInt16(Session["pnl2"]));
       // Response.Redirect("~/ShoppingCart/Admin/wzShippingAccountInfo.aspx");
        Response.Redirect("~/ShoppingCart/Admin/wzFreeShippingRuleAddManage.aspx");
    }
   
    protected void ImgBtnStep1_4_Click(object sender, ImageClickEventArgs e)
    {
        Session["pnl2"] = "24";
        ButtonColor(Convert.ToInt16(Session["pnl2"]));
       // Response.Redirect("~/ShoppingCart/Admin/wzShippingMaster.aspx");
        Response.Redirect("~/ShoppingCart/Admin/wzGeneralShipSetting.aspx");
    }
    //protected void ImgBtnStep1_5_Click(object sender, ImageClickEventArgs e)
    //{
    //    Session["pnl2"] = "25";
    //    ButtonColor(Convert.ToInt16(Session["pnl2"]));
    //    Response.Redirect("~/ShoppingCart/Admin/wzFreeShippingRuleAddManage.aspx");
    //}
    //protected void ImgBtnStep1_6_Click(object sender, ImageClickEventArgs e)
    //{
    //    Session["pnl2"] = "26";
    //    ButtonColor(Convert.ToInt16(Session["pnl2"]));
    //    Response.Redirect("~/ShoppingCart/Admin/wzGeneralShipSetting.aspx");
       
    //}
    
}
