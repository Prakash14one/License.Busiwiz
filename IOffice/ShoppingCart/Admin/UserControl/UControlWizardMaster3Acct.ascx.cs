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

public partial class ShoppingCart_Admin_UserControl_UControlWizardMaster3Acct : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

        //if (Session["pnlM"] != null)
        //{
        //    ButtonColor(Convert.ToInt16(Session["pnlM"]));
            if (Session["pnl3"] != null)
            {
                ButtonColor(Convert.ToInt16(Session["pnl3"]));
            }
        //    else
        //    {
        //        Session["pnlM"] = "1";
        //        ButtonColor(Convert.ToInt16(Session["pnlM"]));
        //        Session["pnl1"] = "11";
        //        ButtonColor(Convert.ToInt16(Session["pnl1"]));
        //    }
        //}
        //else
        //{
        //    Session["pnlM"] = "1";
        //    ButtonColor(Convert.ToInt16(Session["pnlM"]));
        //    Session["pnl1"] = "11";
        //    ButtonColor(Convert.ToInt16(Session["pnl1"]));
        //}

    }
   protected void   ButtonColor( int  ButtonId)
    {
       switch (ButtonId)
        {
           // master panel
            

            case 31:
                ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/Classtypesele.png";
                ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/Classmastr.png";
                ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/Groupmaster.png";
                ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/Balncelmit.png";
                ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/Accountmastr.png";
                ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/Opngbal.png";
                
                break;
            case 32:
                ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/Classtype.png";
                ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/Classmastrsele.png";
                ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/Groupmaster.png";
                ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/Balncelmit.png";
                ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/Accountmastr.png";
                ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/Opngbal.png";
                break;
            case 33:
                ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/Classtype.png";
                ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/Classmastr.png";
                ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/Groupmastersele.png";
                ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/Balncelmit.png";
                ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/Accountmastr.png";
                ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/Opngbal.png";
                break;
            case 34:
                ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/Classtype.png";
                ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/Classmastr.png";
                ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/Groupmaster.png";
                ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/Balncelmitsele.png";
                ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/Accountmastr.png";
                ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/Opngbal.png";
                break;
            case 35:
                ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/Classtype.png";
                ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/Classmastr.png";
                ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/Groupmaster.png";
                ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/Balncelmit.png";
                ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/Accountmastrsele.png";
                ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/Opngbal.png";
                break;
            case 36:
                ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/Classtype.png";
                ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/Classmastr.png";
                ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/Groupmaster.png";
                ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/Balncelmit.png";
                ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/Accountmastr.png";
                ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/Opngbalsele.png";
                break;


        }
    }








    protected void ImgBtnStep1_1_Click(object sender, ImageClickEventArgs e)
    {
        Session["pnl3"] = "31";
        ButtonColor(Convert.ToInt16(Session["pnl3"]));
        Response.Redirect("~/ShoppingCart/Admin/wzClassType.aspx");

    }
    protected void ImgBtnStep1_2_Click(object sender, ImageClickEventArgs e)
    {
        Session["pnl3"] = "32";
        ButtonColor(Convert.ToInt16(Session["pnl3"]));
        Response.Redirect("~/ShoppingCart/Admin/wzClassMaster.aspx");

    }
    protected void ImgBtnStep1_3_Click(object sender, ImageClickEventArgs e)
    {
        Session["pnl3"] = "33";
        ButtonColor(Convert.ToInt16(Session["pnl3"]));
        Response.Redirect("~/ShoppingCart/Admin/wzGroupMaster.aspx");
    }
   
    protected void ImgBtnStep1_4_Click(object sender, ImageClickEventArgs e)
    {
        Session["pnl3"] = "34";
        ButtonColor(Convert.ToInt16(Session["pnl3"]));
        Response.Redirect("~/ShoppingCart/Admin/wzBalanceLimitType.aspx");
    }
    protected void ImgBtnStep1_5_Click(object sender, ImageClickEventArgs e)
    {
        Session["pnl3"] = "35";
        ButtonColor(Convert.ToInt16(Session["pnl3"]));
        Response.Redirect("~/ShoppingCart/Admin/wzAccountMaster.aspx");
    }
    protected void ImgBtnStep1_6_Click(object sender, ImageClickEventArgs e)
    {
        Session["pnl3"] = "36";
        ButtonColor(Convert.ToInt16(Session["pnl3"]));
        Response.Redirect("~/ShoppingCart/Admin/wzOpening_Balance.aspx");
        //Response.Redirect("~/ShoppingCart/Admin/WizardEmailContentMaster.aspx");
    }
    //protected void ImgBtnStep1_7_Click(object sender, ImageClickEventArgs e)
    //{
    //    Session["pnl1"] = "17";
    //    ButtonColor(Convert.ToInt16(Session["pnl3"]));
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
    // //<td>
     //           <asp:ImageButton    ID="ImageButton5" runat="server" 
    // ImageUrl="~/ShoppingCart/images/1(5).png" OnClick="ImgBtnStep11_5_Click" />
                                           
     //       </td>
}
