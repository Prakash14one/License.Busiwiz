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

public partial class ShoppingCart_Admin_UserControl_UControlWizardpanel : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["pnl1"] != null)
        {
            //ButtonColor(Convert.ToInt16(Session["pnlM"]));
            //if (Session["pnl1"] != null)
            //{
                ButtonColor(Convert.ToInt16(Session["pnl1"]));
            //}
            //else
            //{
            //    Session["pnlM"] = "1";
            //    ButtonColor(Convert.ToInt16(Session["pnlM"]));
            //    Session["pnl1"] = "11";
            //    ButtonColor(Convert.ToInt16(Session["pnl1"]));
            //}
        }
        else
        {
            
        }

    }
   protected void   ButtonColor( int  ButtonId)
    {
       switch (ButtonId)
        {
           // master panel
            //case 11:
            //    ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/1.1hover.png";            
            //    ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/1.2.png";
            //    ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/1.3.png";
            //    ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/1.4.png";
            //    ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/1.5.png";
            //    ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/1.6.png";
            //    ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/1.7.png";
            //    ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/1.8.png";
            //    ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/1.9.png";
            //    ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/1.10.png";
            //    ImgBtnStep1_11.ImageUrl = "~/ShoppingCart/images/1.11.png";
            //    ImgBtnStep1_12.ImageUrl = "~/ShoppingCart/images/1.12.png";
            //    ImgBtnStep1_13.ImageUrl = "~/ShoppingCart/images/1.13.png";
            //    break;
            //case 12:
            //    ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/1.1.png";
            //    ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/1.2hover.png";
            //    ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/1.3.png";
            //    ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/1.4.png";
            //    ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/1.5.png";
            //    ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/1.6.png";
            //    ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/1.7.png";
            //    ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/1.8.png";
            //    ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/1.9.png";
            //    ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/1.10.png";
            //    ImgBtnStep1_11.ImageUrl = "~/ShoppingCart/images/1.11.png";
            //    ImgBtnStep1_12.ImageUrl = "~/ShoppingCart/images/1.12.png";
            //    ImgBtnStep1_13.ImageUrl = "~/ShoppingCart/images/1.13.png";
            //    break;
            //case 13:
            //    ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/1.1.png";
            //    ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/1.2.png";
            //    ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/1.3hover.png";
            //    ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/1.4.png";
            //    ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/1.5.png";
            //    ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/1.6.png";
            //    ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/1.7.png";
            //    ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/1.8.png";
            //    ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/1.9.png";
            //    ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/1.10.png";
            //    ImgBtnStep1_11.ImageUrl = "~/ShoppingCart/images/1.11.png";
            //    ImgBtnStep1_12.ImageUrl = "~/ShoppingCart/images/1.12.png";
            //    ImgBtnStep1_13.ImageUrl = "~/ShoppingCart/images/1.13.png";
            //    break;
            //case 14:
            //    ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/1.1.png";
            //    ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/1.2.png";
            //    ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/1.3.png";
            //    ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/1.4hover.png";
            //    ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/1.5.png";
            //    ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/1.6.png";
            //    ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/1.7.png";
            //    ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/1.8.png";
            //    ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/1.9.png";
            //    ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/1.10.png";
            //    ImgBtnStep1_11.ImageUrl = "~/ShoppingCart/images/1.11.png";
            //    ImgBtnStep1_12.ImageUrl = "~/ShoppingCart/images/1.12.png";
            //    ImgBtnStep1_13.ImageUrl = "~/ShoppingCart/images/1.13.png";
            //    break;
            //case 15:
            //    ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/1.1.png";
            //    ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/1.2.png";
            //    ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/1.3.png";
            //    ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/1.4.png";
            //    ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/1.5hover.png";
            //    ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/1.6.png";
            //    ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/1.7.png";
            //    ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/1.8.png";
            //    ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/1.9.png";
            //    ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/1.10.png";
            //    ImgBtnStep1_11.ImageUrl = "~/ShoppingCart/images/1.11.png";
            //    ImgBtnStep1_12.ImageUrl = "~/ShoppingCart/images/1.12.png";
            //    ImgBtnStep1_13.ImageUrl = "~/ShoppingCart/images/1.13.png";
            //    break;
            //case 16:
            //    ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/1.1.png";
            //    ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/1.2.png";
            //    ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/1.3.png";
            //    ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/1.4.png";
            //    ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/1.5.png";
            //    ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/1.6hover.png";
            //    ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/1.7.png";
            //    ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/1.8.png";
            //    ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/1.9.png";
            //    ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/1.10.png";
            //    ImgBtnStep1_11.ImageUrl = "~/ShoppingCart/images/1.11.png";
            //    ImgBtnStep1_12.ImageUrl = "~/ShoppingCart/images/1.12.png";
            //    ImgBtnStep1_13.ImageUrl = "~/ShoppingCart/images/1.13.png";
            //    break;
            //case 17:
            //    ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/1.1.png";
            //    ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/1.2.png";
            //    ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/1.3.png";
            //    ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/1.4.png";
            //    ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/1.5.png";
            //    ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/1.6.png";
            //    ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/1.7hover.png";
            //    ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/1.8.png";
            //    ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/1.9.png";
            //    ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/1.10.png";
            //    ImgBtnStep1_11.ImageUrl = "~/ShoppingCart/images/1.11.png";
            //    ImgBtnStep1_12.ImageUrl = "~/ShoppingCart/images/1.12.png";
            //    ImgBtnStep1_13.ImageUrl = "~/ShoppingCart/images/1.13.png";
            //    break;
            //case 18:
            //    ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/1.1.png";
            //    ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/1.2.png";
            //    ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/1.3.png";
            //    ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/1.4.png";
            //    ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/1.5.png";
            //    ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/1.6.png";
            //    ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/1.7.png";
            //    ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/1.8hover.png";
            //    ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/1.9.png";
            //    ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/1.10.png";
            //    ImgBtnStep1_11.ImageUrl = "~/ShoppingCart/images/1.11.png";
            //    ImgBtnStep1_12.ImageUrl = "~/ShoppingCart/images/1.12.png";
            //    ImgBtnStep1_13.ImageUrl = "~/ShoppingCart/images/1.13.png";
            //    break;
            //case 19:
            //    ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/1.1.png";
            //    ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/1.2.png";
            //    ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/1.3.png";
            //    ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/1.4.png";
            //    ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/1.5.png";
            //    ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/1.6.png";
            //    ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/1.7.png";
            //    ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/1.8.png";
            //    ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/1.9hover.png";
            //    ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/1.10.png";
            //    ImgBtnStep1_11.ImageUrl = "~/ShoppingCart/images/1.11.png";
            //    ImgBtnStep1_12.ImageUrl = "~/ShoppingCart/images/1.12.png";
            //    ImgBtnStep1_13.ImageUrl = "~/ShoppingCart/images/1.13.png";
            //    break;
             
            //case 110:
            //    ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/1.1.png";
            //    ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/1.2.png";
            //    ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/1.3.png";
            //    ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/1.4.png";
            //    ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/1.5.png";
            //    ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/1.6.png";
            //    ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/1.7.png";
            //    ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/1.8.png";
            //    ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/1.9.png";
            //    ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/1.10hover.png";
            //    ImgBtnStep1_11.ImageUrl = "~/ShoppingCart/images/1.11.png";
            //    ImgBtnStep1_12.ImageUrl = "~/ShoppingCart/images/1.12.png";
            //    ImgBtnStep1_13.ImageUrl = "~/ShoppingCart/images/1.13.png";
            //    break;
            //case 111:
            //    ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/1.1.png";
            //    ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/1.2.png";
            //    ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/1.3.png";
            //    ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/1.4.png";
            //    ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/1.5.png";
            //    ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/1.6.png";
            //    ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/1.7.png";
            //    ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/1.8.png";
            //    ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/1.9.png";
            //    ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/1.10.png";
            //    ImgBtnStep1_11.ImageUrl = "~/ShoppingCart/images/1.11hover.png";
            //    ImgBtnStep1_12.ImageUrl = "~/ShoppingCart/images/1.12.png";
            //    ImgBtnStep1_13.ImageUrl = "~/ShoppingCart/images/1.13.png";
            //    break;
            //case 112:
            //    ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/1.1.png";
            //    ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/1.2.png";
            //    ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/1.3.png";
            //    ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/1.4.png";
            //    ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/1.5.png";
            //    ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/1.6.png";
            //    ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/1.7.png";
            //    ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/1.8.png";
            //    ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/1.9.png";
            //    ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/1.10.png";
            //    ImgBtnStep1_11.ImageUrl = "~/ShoppingCart/images/1.11.png";
            //    ImgBtnStep1_12.ImageUrl = "~/ShoppingCart/images/1.12hover.png";
            //    ImgBtnStep1_13.ImageUrl = "~/ShoppingCart/images/1.13.png"; 
            //    break;
            //case 113:
            //    ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/1.1.png";
            //    ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/1.2.png";
            //    ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/1.3.png";
            //    ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/1.4.png";
            //    ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/1.5.png";
            //    ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/1.6.png";
            //    ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/1.7.png";
            //    ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/1.8.png";
            //    ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/1.9.png";
            //    ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/1.10.png";
            //    ImgBtnStep1_11.ImageUrl = "~/ShoppingCart/images/1.11.png";
            //    ImgBtnStep1_12.ImageUrl = "~/ShoppingCart/images/1.12.png";
            //    ImgBtnStep1_13.ImageUrl = "~/ShoppingCart/images/1.13hover.png";
            //    break;


            //   // shipping panel















            //   // account panel

            //case 31:
            //    ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/3(1)hover.png";
            //    ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/3(2).png";
            //    ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/3(3).png";
            //    ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/3(4).png";
            //    ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/3(5).png";
            //    ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/3(6).png";
            //    ImgBtnStep1_7.Visible = false;// "~/ShoppingCart/images/1(7).png";
            //    ImgBtnStep1_8.Visible = false; //"~/ShoppingCart/images/1(8).png";
            //    ImgBtnStep1_9.Visible = false; //"~/ShoppingCart/images/1(9).png";
            //    ImgBtnStep1_10.Visible = false; //"~/ShoppingCart/images/1(10).png";
            //    ImgBtnStep1_11.Visible = false; // "~/ShoppingCart/images/1(11).png";
            //    ImgBtnStep1_12.Visible = false; // "~/ShoppingCart/images/1(12).png";
            //    ImgBtnStep1_13.Visible = false; // "~/ShoppingCart/images/1(13).png";
            //    break;
            //case 32:
            //    ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/3(1).png";
            //    ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/3(2)hover.png";
            //    ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/3(3).png";
            //    ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/3(4).png";
            //    ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/3(5).png";
            //    ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/3(6).png";
            //    ImgBtnStep1_7.Visible = false;// "~/ShoppingCart/images/1(7).png";
            //    ImgBtnStep1_8.Visible = false; //"~/ShoppingCart/images/1(8).png";
            //    ImgBtnStep1_9.Visible = false; //"~/ShoppingCart/images/1(9).png";
            //    ImgBtnStep1_10.Visible = false; //"~/ShoppingCart/images/1(10).png";
            //    ImgBtnStep1_11.Visible = false; // "~/ShoppingCart/images/1(11).png";
            //    ImgBtnStep1_12.Visible = false; // "~/ShoppingCart/images/1(12).png";
            //    ImgBtnStep1_13.Visible = false; // "~/ShoppingCart/images/1(13).png";
            //    break;
            //case 33:
            //    ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/3(1).png";
            //    ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/3(2).png";
            //    ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/3(3)hover.png";
            //    ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/3(4).png";
            //    ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/3(5).png";
            //    ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/3(6).png";
            //    ImgBtnStep1_7.Visible = false;// "~/ShoppingCart/images/1(7).png";
            //    ImgBtnStep1_8.Visible = false; //"~/ShoppingCart/images/1(8).png";
            //    ImgBtnStep1_9.Visible = false; //"~/ShoppingCart/images/1(9).png";
            //    ImgBtnStep1_10.Visible = false; //"~/ShoppingCart/images/1(10).png";
            //    ImgBtnStep1_11.Visible = false; // "~/ShoppingCart/images/1(11).png";
            //    ImgBtnStep1_12.Visible = false; // "~/ShoppingCart/images/1(12).png";
            //    ImgBtnStep1_13.Visible = false; // "~/ShoppingCart/images/1(13).png";
            //    break;
            //case 34:
            //    ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/3(1).png";
            //    ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/3(2).png";
            //    ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/3(3).png";
            //    ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/3(4)hover.png";
            //    ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/3(5).png";
            //    ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/3(6).png";
            //    ImgBtnStep1_7.Visible = false;// "~/ShoppingCart/images/1(7).png";
            //    ImgBtnStep1_8.Visible = false; //"~/ShoppingCart/images/1(8).png";
            //    ImgBtnStep1_9.Visible = false; //"~/ShoppingCart/images/1(9).png";
            //    ImgBtnStep1_10.Visible = false; //"~/ShoppingCart/images/1(10).png";
            //    ImgBtnStep1_11.Visible = false; // "~/ShoppingCart/images/1(11).png";
            //    ImgBtnStep1_12.Visible = false; // "~/ShoppingCart/images/1(12).png";
            //    ImgBtnStep1_13.Visible = false; // "~/ShoppingCart/images/1(13).png";
            //    break;
            //case 35:
            //    ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/3(1).png";
            //    ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/3(2).png";
            //    ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/3(3).png";
            //    ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/3(4).png";
            //    ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/3(5)hover.png";
            //    ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/3(6).png";
            //    ImgBtnStep1_7.Visible = false;// "~/ShoppingCart/images/1(7).png";
            //    ImgBtnStep1_8.Visible = false; //"~/ShoppingCart/images/1(8).png";
            //    ImgBtnStep1_9.Visible = false; //"~/ShoppingCart/images/1(9).png";
            //    ImgBtnStep1_10.Visible = false; //"~/ShoppingCart/images/1(10).png";
            //    ImgBtnStep1_11.Visible = false; // "~/ShoppingCart/images/1(11).png";
            //    ImgBtnStep1_12.Visible = false; // "~/ShoppingCart/images/1(12).png";
            //    ImgBtnStep1_13.Visible = false; // "~/ShoppingCart/images/1(13).png";
            //    break;
            //case 36:
            //    ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/3(1).png";
            //    ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/3(2).png";
            //    ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/3(3).png";
            //    ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/3(4).png";
            //    ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/3(5).png";
            //    ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/3(6)hover.png";
            //    ImgBtnStep1_7.Visible = false;// "~/ShoppingCart/images/1(7).png";
            //    ImgBtnStep1_8.Visible = false; //"~/ShoppingCart/images/1(8).png";
            //    ImgBtnStep1_9.Visible = false; //"~/ShoppingCart/images/1(9).png";
            //    ImgBtnStep1_10.Visible = false; //"~/ShoppingCart/images/1(10).png";
            //    ImgBtnStep1_11.Visible = false; // "~/ShoppingCart/images/1(11).png";
            //    ImgBtnStep1_12.Visible = false; // "~/ShoppingCart/images/1(12).png";
            //    ImgBtnStep1_13.Visible = false; // "~/ShoppingCart/images/1(13).png";
            //    break;

            //   //Inventory panel

            //case 41:
            //    ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/4(1)hover.png";
            //    ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/4(2).png";
            //    ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/4(3).png";
            //    ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/4(4).png";
            //    ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/4(5).png";
            //    ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/4(6).png";
            //    ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/4(7).png";
            //    ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/4(8).png";
            //    ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/4(9).png";
            //    ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/4(10).png";
            //    ImgBtnStep1_11.Visible=false;// = "~/ShoppingCart/images/4(11).png";
            //    ImgBtnStep1_12.Visible = false;// "~/ShoppingCart/images/4(12).png";
            //    ImgBtnStep1_13.Visible = false;//"~/ShoppingCart/images/4(13).png";
            //    break;
            //case 42:
            //    ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/4(1).png";
            //    ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/4(2)hover.png";
            //    ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/4(3).png";
            //    ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/4(4).png";
            //    ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/4(5).png";
            //    ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/4(6).png";
            //    ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/4(7).png";
            //    ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/4(8).png";
            //    ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/4(9).png";
            //    ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/4(10).png";
            //    ImgBtnStep1_11.Visible = false;// = "~/ShoppingCart/images/4(11).png";
            //    ImgBtnStep1_12.Visible = false;// "~/ShoppingCart/images/4(12).png";
            //    ImgBtnStep1_13.Visible = false;//"~/ShoppingCart/images/4(13).png";
            //    break;
            //case 43:
            //    ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/4(1).png";
            //    ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/4(2).png";
            //    ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/4(3)hover.png";
            //    ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/4(4).png";
            //    ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/4(5).png";
            //    ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/4(6).png";
            //    ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/4(7).png";
            //    ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/4(8).png";
            //    ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/4(9).png";
            //    ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/4(10).png";
            //    ImgBtnStep1_11.Visible = false;// = "~/ShoppingCart/images/4(11).png";
            //    ImgBtnStep1_12.Visible = false;// "~/ShoppingCart/images/4(12).png";
            //    ImgBtnStep1_13.Visible = false;//"~/ShoppingCart/images/4(13).png";
            //    break;
                
            //case 44:
            //    ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/4(1).png";
            //    ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/4(2).png";
            //    ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/4(3).png";
            //    ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/4(4)hover.png";
            //    ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/4(5).png";
            //    ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/4(6).png";
            //    ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/4(7).png";
            //    ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/4(8).png";
            //    ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/4(9).png";
            //    ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/4(10).png";
            //    ImgBtnStep1_11.Visible = false;// = "~/ShoppingCart/images/4(11).png";
            //    ImgBtnStep1_12.Visible = false;// "~/ShoppingCart/images/4(12).png";
            //    ImgBtnStep1_13.Visible = false;//"~/ShoppingCart/images/4(13).png";
            //    break;
            //case 45:
            //    ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/4(1).png";
            //    ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/4(2).png";
            //    ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/4(3).png";
            //    ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/4(4).png";
            //    ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/4(5)hover.png";
            //    ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/4(6).png";
            //    ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/4(7).png";
            //    ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/4(8).png";
            //    ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/4(9).png";
            //    ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/4(10).png";
            //    ImgBtnStep1_11.Visible = false;// = "~/ShoppingCart/images/4(11).png";
            //    ImgBtnStep1_12.Visible = false;// "~/ShoppingCart/images/4(12).png";
            //    ImgBtnStep1_13.Visible = false;//"~/ShoppingCart/images/4(13).png";
            //    break;
            //case 46:
            //    ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/4(1).png";
            //    ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/4(2).png";
            //    ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/4(3).png";
            //    ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/4(4).png";
            //    ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/4(5).png";
            //    ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/4(6)hover.png";
            //    ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/4(7).png";
            //    ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/4(8).png";
            //    ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/4(9).png";
            //    ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/4(10).png";
            //    ImgBtnStep1_11.Visible = false;// = "~/ShoppingCart/images/4(11).png";
            //    ImgBtnStep1_12.Visible = false;// "~/ShoppingCart/images/4(12).png";
            //    ImgBtnStep1_13.Visible = false;//"~/ShoppingCart/images/4(13).png";
            //    break;
            //case 47:
            //    ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/4(1).png";
            //    ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/4(2).png";
            //    ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/4(3).png";
            //    ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/4(4).png";
            //    ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/4(5).png";
            //    ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/4(6).png";
            //    ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/4(7)hover.png";
            //    ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/4(8).png";
            //    ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/4(9).png";
            //    ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/4(10).png";
            //    ImgBtnStep1_11.Visible = false;// = "~/ShoppingCart/images/4(11).png";
            //    ImgBtnStep1_12.Visible = false;// "~/ShoppingCart/images/4(12).png";
            //    ImgBtnStep1_13.Visible = false;//"~/ShoppingCart/images/4(13).png";
            //    break;
            //case 48:
            //    ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/4(1).png";
            //    ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/4(2).png";
            //    ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/4(3).png";
            //    ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/4(4).png";
            //    ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/4(5).png";
            //    ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/4(6).png";
            //    ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/4(7).png";
            //    ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/4(8)hover.png";
            //    ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/4(9).png";
            //    ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/4(10).png";
            //    ImgBtnStep1_11.Visible = false;// = "~/ShoppingCart/images/4(11).png";
            //    ImgBtnStep1_12.Visible = false;// "~/ShoppingCart/images/4(12).png";
            //    ImgBtnStep1_13.Visible = false;//"~/ShoppingCart/images/4(13).png";
            //    break;
            //case 49:
            //    ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/4(1).png";
            //    ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/4(2).png";
            //    ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/4(3).png";
            //    ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/4(4).png";
            //    ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/4(5).png";
            //    ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/4(6).png";
            //    ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/4(7).png";
            //    ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/4(8).png";
            //    ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/4(9)hover.png";
            //    ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/4(10).png";
            //    ImgBtnStep1_11.Visible = false;// = "~/ShoppingCart/images/4(11).png";
            //    ImgBtnStep1_12.Visible = false;// "~/ShoppingCart/images/4(12).png";
            //    ImgBtnStep1_13.Visible = false;//"~/ShoppingCart/images/4(13).png";
            //    break;

            //case 410:
            //    ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/4(1).png";
            //    ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/4(2).png";
            //    ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/4(3).png";
            //    ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/4(4).png";
            //    ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/4(5).png";
            //    ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/4(6).png";
            //    ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/4(7).png";
            //    ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/4(8).png";
            //    ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/4(9).png";
            //    ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/4(10)hover.png";
            //    ImgBtnStep1_11.Visible = false;// = "~/ShoppingCart/images/4(11).png";
            //    ImgBtnStep1_12.Visible = false;// "~/ShoppingCart/images/4(12).png";
            //    ImgBtnStep1_13.Visible = false;//"~/ShoppingCart/images/4(13).png";
            //    break;







            //   //sales and purchase
            //case 51:
            //    ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/5(1).png";
            //    ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/5(2).png";
            //    ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/5(3).png";
            //    ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/5(4).png";
            //    ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/5(5).png";
            //    ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/5(6)hover.png";
            //    ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/5(7).png";
            //    ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/5(8).png";
            //    ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/5(9).png";
            //    ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/5(10).png";
            //    ImgBtnStep1_11.Visible = false;// = "~/ShoppingCart/images/4(11).png";
            //    ImgBtnStep1_12.Visible = false;// "~/ShoppingCart/images/4(12).png";
            //    ImgBtnStep1_13.Visible = false;//"~/ShoppingCart/images/4(13).png";
            //    break;
            //case 52:
            //    ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/5(1).png";
            //    ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/5(2).png";
            //    ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/5(3).png";
            //    ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/5(4).png";
            //    ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/5(5).png";
            //    ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/5(6)hover.png";
            //    ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/5(7).png";
            //    ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/5(8).png";
            //    ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/5(9).png";
            //    ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/5(10).png";
            //    ImgBtnStep1_11.Visible = false;// = "~/ShoppingCart/images/4(11).png";
            //    ImgBtnStep1_12.Visible = false;// "~/ShoppingCart/images/4(12).png";
            //    ImgBtnStep1_13.Visible = false;//"~/ShoppingCart/images/4(13).png";
            //    break;
            //case 53:
            //    ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/5(1).png";
            //    ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/5(2).png";
            //    ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/5(3).png";
            //    ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/5(4).png";
            //    ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/5(5).png";
            //    ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/5(6)hover.png";
            //    ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/5(7).png";
            //    ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/5(8).png";
            //    ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/5(9).png";
            //    ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/5(10).png";
            //    ImgBtnStep1_11.Visible = false;// = "~/ShoppingCart/images/4(11).png";
            //    ImgBtnStep1_12.Visible = false;// "~/ShoppingCart/images/4(12).png";
            //    ImgBtnStep1_13.Visible = false;//"~/ShoppingCart/images/4(13).png";
            //    break;

            //case 54:
            //    ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/5(1).png";
            //    ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/5(2).png";
            //    ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/5(3).png";
            //    ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/5(4).png";
            //    ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/5(5).png";
            //    ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/5(6)hover.png";
            //    ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/5(7).png";
            //    ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/5(8).png";
            //    ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/5(9).png";
            //    ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/5(10).png";
            //    ImgBtnStep1_11.Visible = false;// = "~/ShoppingCart/images/4(11).png";
            //    ImgBtnStep1_12.Visible = false;// "~/ShoppingCart/images/4(12).png";
            //    ImgBtnStep1_13.Visible = false;//"~/ShoppingCart/images/4(13).png";
            //    break;
            //case 55:
            //    ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/5(1).png";
            //    ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/5(2).png";
            //    ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/5(3).png";
            //    ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/5(4).png";
            //    ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/5(5).png";
            //    ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/5(6)hover.png";
            //    ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/5(7).png";
            //    ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/5(8).png";
            //    ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/5(9).png";
            //    ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/5(10).png";
            //    ImgBtnStep1_11.Visible = false;// = "~/ShoppingCart/images/4(11).png";
            //    ImgBtnStep1_12.Visible = false;// "~/ShoppingCart/images/4(12).png";
            //    ImgBtnStep1_13.Visible = false;//"~/ShoppingCart/images/4(13).png";
            //    break;
            //case 56:
            //    ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/5(1).png";
            //    ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/5(2).png";
            //    ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/5(3).png";
            //    ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/5(4).png";
            //    ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/5(5).png";
            //    ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/5(6)hover.png";
            //    ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/5(7).png";
            //    ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/5(8).png";
            //    ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/5(9).png";
            //    ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/5(10).png";
            //    ImgBtnStep1_11.Visible = false;// = "~/ShoppingCart/images/4(11).png";
            //    ImgBtnStep1_12.Visible = false;// "~/ShoppingCart/images/4(12).png";
            //    ImgBtnStep1_13.Visible = false;//"~/ShoppingCart/images/4(13).png";
            //    break;
            //case 57:
            //    ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/5(1).png";
            //    ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/5(2).png";
            //    ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/5(3).png";
            //    ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/5(4).png";
            //    ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/5(5).png";
            //    ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/5(6).png";
            //    ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/5(7)hover.png";
            //    ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/5(8).png";
            //    ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/5(9).png";
            //    ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/5(10).png";
            //    ImgBtnStep1_11.Visible = false;// = "~/ShoppingCart/images/4(11).png";
            //    ImgBtnStep1_12.Visible = false;// "~/ShoppingCart/images/4(12).png";
            //    ImgBtnStep1_13.Visible = false;//"~/ShoppingCart/images/4(13).png";
            //    break;
            //case 58:
            //    ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/5(1).png";
            //    ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/5(2).png";
            //    ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/5(3).png";
            //    ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/5(4).png";
            //    ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/5(5).png";
            //    ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/5(6).png";
            //    ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/5(7).png";
            //    ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/5(8)hover.png";
            //    ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/5(9).png";
            //    ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/5(10).png";
            //    ImgBtnStep1_11.Visible = false;// = "~/ShoppingCart/images/4(11).png";
            //    ImgBtnStep1_12.Visible = false;// "~/ShoppingCart/images/4(12).png";
            //    ImgBtnStep1_13.Visible = false;//"~/ShoppingCart/images/4(13).png";
            //    break;
            //case 59:
            //    ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/5(1).png";
            //    ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/5(2).png";
            //    ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/5(3).png";
            //    ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/5(4).png";
            //    ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/5(5).png";
            //    ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/5(6).png";
            //    ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/5(7).png";
            //    ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/5(8).png";
            //    ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/5(9)hover.png";
            //    ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/5(10).png";
            //    ImgBtnStep1_11.Visible = false;// = "~/ShoppingCart/images/4(11).png";
            //    ImgBtnStep1_12.Visible = false;// "~/ShoppingCart/images/4(12).png";
            //    ImgBtnStep1_13.Visible = false;//"~/ShoppingCart/images/4(13).png";
            //    break;

            //case 510:
            //    ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/5(1).png";
            //    ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/5(2).png";
            //    ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/5(3).png";
            //    ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/5(4).png";
            //    ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/5(5).png";
            //    ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/5(6).png";
            //    ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/5(7).png";
            //    ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/5(8).png";
            //    ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/5(9).png";
            //    ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/5(10)hover.png";
            //    ImgBtnStep1_11.Visible = false;// = "~/ShoppingCart/images/4(11).png";
            //    ImgBtnStep1_12.Visible = false;// "~/ShoppingCart/images/4(12).png";
            //    ImgBtnStep1_13.Visible = false;//"~/ShoppingCart/images/4(13).png";
            //    break;














        }
    }










    //protected void ImgBtnStep1_1_Click(object sender, ImageClickEventArgs e)
    //{
    //    Session["pnl1"] = "11";
    //    ButtonColor(Convert.ToInt16(Session["pnl1"]));
    //    Response.Redirect("~/ShoppingCart/Admin/Wizardcompanyinformation.aspx");

    //}
    //protected void ImgBtnStep1_2_Click(object sender, ImageClickEventArgs e)
    //{
    //    Session["pnl1"] = "12";
    //    ButtonColor(Convert.ToInt16(Session["pnl1"]));
    //    Response.Redirect("~/ShoppingCart/Admin/WizardCompanyWebsitMaster.aspx");

    //}
    //protected void ImgBtnStep1_3_Click(object sender, ImageClickEventArgs e)
    //{
    //    Session["pnl1"] = "13";
    //    ButtonColor(Convert.ToInt16(Session["pnl1"]));
    //    Response.Redirect("~/ShoppingCart/Admin/WizardCompanyWebsiteAddressMaster.aspx");
    //}
   
    //protected void ImgBtnStep1_4_Click(object sender, ImageClickEventArgs e)
    //{
    //    Session["pnl1"] = "14";
    //    ButtonColor(Convert.ToInt16(Session["pnl1"]));
    //    Response.Redirect("~/ShoppingCart/Admin/WizardFaqCategoryMaster.aspx");
    //}
    //protected void ImgBtnStep1_5_Click(object sender, ImageClickEventArgs e)
    //{
    //    Session["pnl1"] = "15";
    //    ButtonColor(Convert.ToInt16(Session["pnl1"]));
    //    Response.Redirect("~/ShoppingCart/Admin/WizardFAQMaster.aspx");
    //}
    //protected void ImgBtnStep1_6_Click(object sender, ImageClickEventArgs e)
    //{
    //    Session["pnl1"] = "16";
    //    ButtonColor(Convert.ToInt16(Session["pnl1"]));
    //    Response.Redirect("~/ShoppingCart/Admin/wzWebContentEntry.aspx");
    //    //Response.Redirect("~/ShoppingCart/Admin/WizardEmailContentMaster.aspx");
    //}
    //protected void ImgBtnStep1_7_Click(object sender, ImageClickEventArgs e)
    //{
    //    Session["pnl1"] = "17";
    //    ButtonColor(Convert.ToInt16(Session["pnl1"]));
    //    Response.Redirect("~/ShoppingCart/Admin/wzEmailTypeMaster.aspx");
    //}
    //protected void ImgBtnStep1_8_Click(object sender, ImageClickEventArgs e)
    //{
    //    Session["pnl1"] = "18";
    //    ButtonColor(Convert.ToInt16(Session["pnl1"]));
    //    //Response.Redirect("~/ShoppingCart/Admin/WizardInventoryCategoryMaster.aspx");
    //    Response.Redirect("~/ShoppingCart/Admin/WizardEmailContentMaster.aspx");
    //}
    //protected void ImgBtnStep1_9_Click(object sender, ImageClickEventArgs e)
    //{
    //    Session["pnl1"] = "19";
    //    ButtonColor(Convert.ToInt16(Session["pnl1"]));
    //    Response.Redirect("~/ShoppingCart/Admin/wzTaxTypeMaster.aspx");
    //}
   
    //protected void ImgBtnStep1_10_Click(object sender, ImageClickEventArgs e)
    //{
    //    Session["pnl1"] = "110";
    //    ButtonColor(Convert.ToInt16(Session["pnl1"]));
    //    Response.Redirect("~/ShoppingCart/Admin/wzTaxTypeMasterMoreInfo.aspx");
    //}
    //protected void ImgBtnStep1_11_Click(object sender, ImageClickEventArgs e)
    //{
    //    Session["pnl1"] = "111";
    //    ButtonColor(Convert.ToInt16(Session["pnl1"]));
    //    Response.Redirect("~/ShoppingCart/Admin/wzPartyMaster.aspx");
    //}
    //protected void ImgBtnStep1_12_Click(object sender, ImageClickEventArgs e)
    //{
    //    Session["pnl1"] = "112";
    //    ButtonColor(Convert.ToInt16(Session["pnl1"]));
    //    Response.Redirect("~/ShoppingCart/Admin/wzEmployeeMaster.aspx");
    //}
    //protected void ImgBtnStep1_13_Click(object sender, ImageClickEventArgs e)
    //{
    //    Session["pnl1"] = "113";
    //    ButtonColor(Convert.ToInt16(Session["pnl1"]));
    //    Response.Redirect("~/ShoppingCart/Admin/wzpartyautoallocationofmanagers.aspx");
    //}
     //<td>
     //           <asp:ImageButton    ID="ImageButton5" runat="server" 
    // ImageUrl="~/ShoppingCart/images/1(5).png" OnClick="ImgBtnStep11_5_Click" />
                                           
     //       </td>
}
