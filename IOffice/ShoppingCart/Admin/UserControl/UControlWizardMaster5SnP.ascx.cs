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

public partial class ShoppingCart_Admin_UserControl_UControlWizardMaster5SnP : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

        //if (Session["pnlM"] != null)
        //{
            ButtonColor(Convert.ToInt16(Session["pnl5"]));
        //    if (Session["pnl5"] != null)
        //    {
        //        ButtonColor(Convert.ToInt16(Session["pnl5"]));
        //    }
        //    else
        //    {
        //        Session["pnlM"] = "1";
        //        ButtonColor(Convert.ToInt16(Session["pnlM"]));
        //        Session["pnl5"] = "11";
        //        ButtonColor(Convert.ToInt16(Session["pnl5"]));
        //    }
        //}
        //else
        //{
        //    Session["pnlM"] = "1";
        //    ButtonColor(Convert.ToInt16(Session["pnlM"]));
        //    Session["pnl5"] = "11";
        //    ButtonColor(Convert.ToInt16(Session["pnl5"]));
        //}

    }
   protected void   ButtonColor( int  ButtonId)
    {
       switch (ButtonId)
        {
           
               //sales and purchase
            case 51:
                ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/Applidiscsele.png";
                ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/Ordrdisc.png";
                ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/Handlngchrg.png";
                ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/Promotion.png";
                ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/Productdisc.png";
                ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/saledetmin.png";
                ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/pricedtmin.png";
                ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/orderqty.png";
                ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/Textinfo.png";
                //ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/5.10.png";
               
                break;
            case 52:
                ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/Applidisc.png";
                ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/Ordrdiscsele.png";
                ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/Handlngchrg.png";
                ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/Promotion.png";
                ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/Productdisc.png";
                ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/saledetmin.png";
                ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/pricedtmin.png";
                ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/orderqty.png";
                ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/Textinfo.png";
                //ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/5.10.png";
                break;
            case 53:
                ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/Applidisc.png";
                ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/Ordrdisc.png";
                ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/Handlngchrgsele.png";
                ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/Promotion.png";
                ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/Productdisc.png";
                ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/saledetmin.png";
                ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/pricedtmin.png";
                ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/orderqty.png";
                ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/Textinfo.png";
                //ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/5.10.png";
                break;

            case 54:
                ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/Applidisc.png";
                ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/Ordrdisc.png";
                ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/Handlngchrg.png";
                ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/Promotionsele.png";
                ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/Productdisc.png";
                ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/saledetmin.png";
                ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/pricedtmin.png";
                ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/orderqty.png";
                ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/Textinfo.png";
                //ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/5.10.png";
                break;
            case 55:
                ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/Applidisc.png";
                ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/Ordrdisc.png";
                ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/Handlngchrg.png";
                ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/Promotion.png";
                ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/Productdiscsele.png";
                ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/saledetmin.png";
                ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/pricedtmin.png";
                ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/orderqty.png";
                ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/Textinfo.png";
                //ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/5.10.png";
                break;
            case 56:
                ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/Applidisc.png";
                ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/Ordrdisc.png";
                ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/Handlngchrg.png";
                ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/Promotion.png";
                ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/Productdisc.png";
                ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/saledetminsele.png";
                ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/pricedtmin.png";
                ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/orderqty.png";
                ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/Textinfo.png";
                //ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/5.10.png";
                break;
            case 57:
                ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/5.1.png";
                ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/Ordrdisc.png";
                ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/Handlngchrg.png";
                ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/Promotion.png";
                ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/Productdisc.png";
                ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/saledetmin.png";
                ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/pricedtminsele.png";
                ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/orderqty.png";
                ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/Textinfo.png";
                //ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/5.10.png";
                break;
            case 58:
                ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/Applidisc.png";
                ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/Ordrdisc.png";
                ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/Handlngchrg.png";
                ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/Promotion.png";
                ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/Productdisc.png";
                ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/saledetmin.png";
                ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/pricedtmin.png";
                ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/Ordrdiscsele.png";
                ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/Textinfo.png";
                //ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/5.10.png";
                break;
            case 59:
                ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/Applidisc.png";
                ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/Ordrdisc.png";
                ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/Handlngchrg.png";
                ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/Promotion.png";
                ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/Productdisc.png";
                ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/saledetmin.png";
                ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/pricedtmin.png";
                ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/orderqty.png";
                ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/Textinfosele.png";
                //ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/5.10.png";
                break;
        }
    }

    protected void ImgBtnStep1_1_Click(object sender, ImageClickEventArgs e)
    {
        Session["pnl5"] = "51";
        ButtonColor(Convert.ToInt16(Session["pnl5"]));
        Response.Redirect("~/ShoppingCart/Admin/wzApplyVolumeDiscount.aspx");

    }
    protected void ImgBtnStep1_2_Click(object sender, ImageClickEventArgs e)
    {
        Session["pnl5"] = "52";
        ButtonColor(Convert.ToInt16(Session["pnl5"]));
        Response.Redirect("~/ShoppingCart/Admin/wzOrderValueDiscountMaster.aspx");

    }
    protected void ImgBtnStep1_3_Click(object sender, ImageClickEventArgs e)
    {
        Session["pnl5"] = "53";
        ButtonColor(Convert.ToInt16(Session["pnl5"]));
        Response.Redirect("~/ShoppingCart/Admin/wzPackingHandlingChargesMaster.aspx");
    }
   
    protected void ImgBtnStep1_4_Click(object sender, ImageClickEventArgs e)
    {
        Session["pnl5"] = "54";
        ButtonColor(Convert.ToInt16(Session["pnl5"]));
        Response.Redirect("~/ShoppingCart/Admin/wzPromotionDiscountDetail.aspx");
    }
    protected void ImgBtnStep1_5_Click(object sender, ImageClickEventArgs e)
    {
        Session["pnl5"] = "55";
        ButtonColor(Convert.ToInt16(Session["pnl5"]));
        Response.Redirect("~/ShoppingCart/Admin/wzFeatureProductsDiscountDetail.aspx");
    }
    protected void ImgBtnStep1_6_Click(object sender, ImageClickEventArgs e)
    {
        Session["pnl5"] = "56";
        ButtonColor(Convert.ToInt16(Session["pnl5"]));
        Response.Redirect("~/ShoppingCart/Admin/wzSalesRateDeterminatation.aspx");
        //Response.Redirect("~/ShoppingCart/Admin/WizardEmailContentMaster.aspx");
    }
    protected void ImgBtnStep1_7_Click(object sender, ImageClickEventArgs e)
    {
        Session["pnl5"] = "57";
        ButtonColor(Convert.ToInt16(Session["pnl5"]));
        Response.Redirect("~/ShoppingCart/Admin/wzPriceDetermination.aspx");
    }
    protected void ImgBtnStep1_8_Click(object sender, ImageClickEventArgs e)
    {
        Session["pnl5"] = "58";
        ButtonColor(Convert.ToInt16(Session["pnl5"]));
        //Response.Redirect("~/ShoppingCart/Admin/WizardInventoryCategoryMaster.aspx");
        Response.Redirect("~/ShoppingCart/Admin/wzOrderQtyRestrictionDetail.aspx");
    }
    protected void ImgBtnStep1_9_Click(object sender, ImageClickEventArgs e)
    {
        Session["pnl5"] = "59";
        ButtonColor(Convert.ToInt16(Session["pnl5"]));
        //Response.Redirect("~/ShoppingCart/Admin/WizardInventoryCategoryMaster.aspx");
        Response.Redirect("~/ShoppingCart/Admin/wzStoreTaxmethodtbl.aspx");
    }
    //protected void ImgBtnStep1_10_Click(object sender, ImageClickEventArgs e)
    //{
    //    Session["pnl5"] = "510";
    //    ButtonColor(Convert.ToInt16(Session["pnl5"]));
    //    //Response.Redirect("~/ShoppingCart/Admin/WizardInventoryCategoryMaster.aspx");
    //   // Response.Redirect("~/ShoppingCart/Admin/wzOrderQtyRestrictionDetail.aspx");
    //    Response.Redirect("~/ShoppingCart/Admin/wzTaxMasterWithInv.aspx");
    //}
}
