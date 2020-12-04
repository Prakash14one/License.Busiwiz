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


public partial class ShoppingCart_Admin_UserControl_UControlWizardMaster6Ser : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

        ButtonColor(Convert.ToInt16(Session["pnl6"]));
       

    }
    protected void ButtonColor(int ButtonId)
    {
        switch (ButtonId)
        {

            //sales and purchase
            case 61:
                ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/scatsel.png";
                ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/sscat.png";
                ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/ssscat.png";
                ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/ser.png";
                ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/sslo.png";
                ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/serimage.png";
                ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/batchma.png";
                ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/batchtma.png";
                ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/batchworkin.png";
                ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/empman.png";
                ImgBtnStep1_11.ImageUrl = "~/ShoppingCart/images/serimage.png";
                ImgBtnStep1_12.ImageUrl = "~/ShoppingCart/images/serprma.png"; 
            
                break;
            case 62:
                ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/scat.png";
                ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/sscatsel.png";
                ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/ssscat.png";
                ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/ser.png";
                ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/sslo.png";
                ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/serimage.png";
                ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/batchma.png";
                ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/batchtma.png";
                ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/batchworkin.png";
                ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/empman.png";
                ImgBtnStep1_11.ImageUrl = "~/ShoppingCart/images/promas.png";
                ImgBtnStep1_12.ImageUrl = "~/ShoppingCart/images/serprma.png"; 
                //ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/5.10.png";
                break;
            case 63:
                ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/scat.png";
                ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/sscat.png";
                ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/ssscatsel.png";
                ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/ser.png";
                ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/sslo.png";
                ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/serimage.png";
                ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/batchma.png";
                ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/batchtma.png";
                ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/batchworkin.png";
                ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/empman.png";
                ImgBtnStep1_11.ImageUrl = "~/ShoppingCart/images/promas.png";
                ImgBtnStep1_12.ImageUrl = "~/ShoppingCart/images/serprma.png"; 
                //ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/5.10.png";
                break;

            case 64:
                ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/scat.png";
                ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/sscat.png";
                ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/ssscat.png";
                ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/sersel.png";
                ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/sslo.png";
                ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/serimage.png";
                ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/batchma.png";
                ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/batchtma.png";
                ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/batchworkin.png";
                ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/empman.png";
                ImgBtnStep1_11.ImageUrl = "~/ShoppingCart/images/promasserimage.png";
                ImgBtnStep1_12.ImageUrl = "~/ShoppingCart/images/serprma.png"; 
                break;
            case 65:
                ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/scat.png";
                ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/sscat.png";
                ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/ssscat.png";
                ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/ser.png";
                ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/sslosel.png";
                ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/serimage.png";
                ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/batchma.png";
                ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/batchtma.png";
                ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/batchworkin.png";
                ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/empman.png";
                ImgBtnStep1_11.ImageUrl = "~/ShoppingCart/images/promas.png";
                ImgBtnStep1_12.ImageUrl = "~/ShoppingCart/images/serprma.png"; 
                break;
            case 66:
                ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/scat.png";
                ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/sscat.png";
                ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/ssscat.png";
                ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/ser.png";
                ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/ssloselsel.png";
                ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/serimage.png";
                ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/batchma.png";
                ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/batchtma.png";
                ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/batchworkin.png";
                ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/empman.png";
                ImgBtnStep1_11.ImageUrl = "~/ShoppingCart/images/promas.png";
                ImgBtnStep1_12.ImageUrl = "~/ShoppingCart/images/serprma.png"; 
                break;
            case 67:
                ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/scat.png";
                ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/sscat.png";
                ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/ssscat.png";
                ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/ser.png";
                ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/sslo.png";
                ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/serimage.png";
                ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/batchmasel.png";
                ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/batchtma.png";
                ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/batchworkin.png";
                ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/empman.png";
                ImgBtnStep1_11.ImageUrl = "~/ShoppingCart/images/promas.png";
                ImgBtnStep1_12.ImageUrl = "~/ShoppingCart/images/serprma.png";
                break;
            case 68:
                ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/scat.png";
                ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/sscat.png";
                ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/ssscat.png";
                ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/ser.png";
                ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/sslo.png";
                ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/serimage.png";
                ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/batchma.png";
                ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/batchtmasel.png";
                ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/batchworkin.png";
                ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/empman.png";
                ImgBtnStep1_11.ImageUrl = "~/ShoppingCart/images/promas.png";
                ImgBtnStep1_12.ImageUrl = "~/ShoppingCart/images/serprma.png";
                break;
            case 69:
                ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/scat.png";
                ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/sscat.png";
                ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/ssscat.png";
                ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/ser.png";
                ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/sslo.png";
                ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/serimage.png";
                ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/batchma.png";
                ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/batchtma.png";
                ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/batchworkinsel.png";
                ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/empman.png";
                ImgBtnStep1_11.ImageUrl = "~/ShoppingCart/images/promas.png";
                ImgBtnStep1_12.ImageUrl = "~/ShoppingCart/images/serprma.png";
                break;
            case 610:
                ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/scat.png";
                ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/sscat.png";
                ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/ssscat.png";
                ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/ser.png";
                ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/sslo.png";
                ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/serimage.png";
                ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/batchma.png";
                ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/batchtma.png";
                ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/batchworkin.png";
                ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/empmansel.png";
                ImgBtnStep1_11.ImageUrl = "~/ShoppingCart/images/promas.png";
                ImgBtnStep1_12.ImageUrl = "~/ShoppingCart/images/serprma.png";
                break;
            case 611:
                ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/scat.png";
                ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/sscat.png";
                ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/ssscat.png";
                ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/ser.png";
                ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/sslo.png";
                ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/serimage.png";
                ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/batchma.png";
                ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/batchtma.png";
                ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/batchworkin.png";
                ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/empman.png";
                ImgBtnStep1_11.ImageUrl = "~/ShoppingCart/images/promassel.png";
                ImgBtnStep1_12.ImageUrl = "~/ShoppingCart/images/serprma.png";
                break;
            case 612:
                ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/scat.png";
                ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/sscat.png";
                ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/ssscat.png";
                ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/ser.png";
                ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/sslo.png";
                ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/serimage.png";
                ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/batchma.png";
                ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/batchtma.png";
                ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/batchworkin.png";
                ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/empman.png";
                ImgBtnStep1_11.ImageUrl = "~/ShoppingCart/images/promas.png";
                ImgBtnStep1_12.ImageUrl = "~/ShoppingCart/images/serprmasel.png";
                break;

          default:
                ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/scatsel.png";
                ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/sscat.png";
                ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/ssscat.png";
                ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/ser.png";
                ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/sslo.png";
                ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/serimage.png";
                ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/batchma.png";
                ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/batchtma.png";
                ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/batchworkin.png";
                ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/empman.png";
                ImgBtnStep1_11.ImageUrl = "~/ShoppingCart/images/serimage.png";
                ImgBtnStep1_12.ImageUrl = "~/ShoppingCart/images/serprma.png";

                break;
        }
    }

    protected void ImgBtnStep1_1_Click(object sender, ImageClickEventArgs e)
    {
        Session["pnl6"] = "61";
        ButtonColor(Convert.ToInt16(Session["pnl6"]));
        Response.Redirect("~/ShoppingCart/Admin/wzServicecategory.aspx");

    }
    protected void ImgBtnStep1_2_Click(object sender, ImageClickEventArgs e)
    {
        Session["pnl6"] = "62";
        ButtonColor(Convert.ToInt16(Session["pnl6"]));
        Response.Redirect("~/ShoppingCart/Admin/wzServiceInSubCatMaster.aspx");

    }
    protected void ImgBtnStep1_3_Click(object sender, ImageClickEventArgs e)
    {
        Session["pnl6"] = "63";
        ButtonColor(Convert.ToInt16(Session["pnl6"]));
        Response.Redirect("~/ShoppingCart/Admin/ServiceSubSubInCatMaster.aspx");
    }

    protected void ImgBtnStep1_4_Click(object sender, ImageClickEventArgs e)
    {
        Session["pnl6"] = "64";
        ButtonColor(Convert.ToInt16(Session["pnl6"]));
        Response.Redirect("~/ShoppingCart/Admin/wzservicesInventorymaster.aspx");
    }
    protected void ImgBtnStep1_5_Click(object sender, ImageClickEventArgs e)
    {
        Session["pnl6"] = "65";
        ButtonColor(Convert.ToInt16(Session["pnl6"]));
        Response.Redirect("~/ShoppingCart/Admin/wzservicestoreselection.aspx");
    }
    protected void ImgBtnStep1_6_Click(object sender, ImageClickEventArgs e)
    {
        Session["pnl6"] = "66";
        ButtonColor(Convert.ToInt16(Session["pnl6"]));
        Response.Redirect("~/ShoppingCart/Admin/wzserviceproductimg.aspx");
    }
    protected void ImgBtnStep1_7_Click(object sender, ImageClickEventArgs e)
    {
        Session["pnl6"] = "67";
        ButtonColor(Convert.ToInt16(Session["pnl6"]));
        Response.Redirect("~/ShoppingCart/Admin/wzBatchMaster.aspx");
    }
    protected void ImgBtnStep1_8_Click(object sender, ImageClickEventArgs e)
    {
        Session["pnl6"] = "68";
        ButtonColor(Convert.ToInt16(Session["pnl6"]));
        Response.Redirect("~/ShoppingCart/Admin/wzBatchTimingManage.aspx");
    }
    protected void ImgBtnStep1_9_Click(object sender, ImageClickEventArgs e)
    {
        Session["pnl6"] = "69";
        ButtonColor(Convert.ToInt16(Session["pnl6"]));
        Response.Redirect("~/ShoppingCart/Admin/wzBatchworkingDay.aspx");
    }
    protected void ImgBtnStep1_10_Click(object sender, ImageClickEventArgs e)
    {
        Session["pnl6"] = "610";
        ButtonColor(Convert.ToInt16(Session["pnl6"]));
        Response.Redirect("~/ShoppingCart/Admin/wzEmployeeBatchManage.aspx");
    }
    protected void ImgBtnStep1_11_Click(object sender, ImageClickEventArgs e)
    {
        Session["pnl6"] = "611";
        ButtonColor(Convert.ToInt16(Session["pnl6"]));
        Response.Redirect("~/ShoppingCart/Admin/wzProviderMaster.aspx");
    }
    protected void ImgBtnStep1_12_Click(object sender, ImageClickEventArgs e)
    {
        Session["pnl6"] = "612";
        ButtonColor(Convert.ToInt16(Session["pnl6"]));
        Response.Redirect("~/ShoppingCart/Admin/wzServiceProviderManager.aspx");
    }
}
