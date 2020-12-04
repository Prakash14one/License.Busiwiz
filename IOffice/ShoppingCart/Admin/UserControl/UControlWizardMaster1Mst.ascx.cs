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

public partial class ShoppingCart_Admin_UserControl_UControlWizardMaster1Mst : System.Web.UI.UserControl
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
            case 11:
                ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/companyInfoSele.png";
                ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/Companywebsite.png";
                ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/Websiteadd.png";
                ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/FAQcat.png";
                ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/Askquton.png";
                ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/E-mailtype.png";
                ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/Emailcnct.png";
                ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/Departmnt.png";
                ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/Designation.png";
                ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/Partymastr.png";
                ImgBtnStep1_11.ImageUrl = "~/ShoppingCart/images/Employeemastr.png";
                ImgBtnStep1_12.ImageUrl = "~/ShoppingCart/images/Partyallction.png";
               // ImgBtnStep1_13.ImageUrl = "~/ShoppingCart/images/1.13.png";
               // ImageButton1_14.ImageUrl = "~/ShoppingCart/images/1.10.png";
                break;
            case 12:
                ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/companyinfo.png";
                ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/Companywebsitesele.png";
                ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/Websiteadd.png";
                ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/FAQcat.png";
                ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/Askquton.png";
                ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/E-mailtype.png";
                ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/Emailcnct.png";
                ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/Departmnt.png";
                ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/Designation.png";
                ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/Partymastr.png";
                ImgBtnStep1_11.ImageUrl = "~/ShoppingCart/images/Employeemastr.png";
                ImgBtnStep1_12.ImageUrl = "~/ShoppingCart/images/Partyallction.png";
             //   ImgBtnStep1_13.ImageUrl = "~/ShoppingCart/images/1.13.png";
             //   ImageButton1_14.ImageUrl = "~/ShoppingCart/images/1.10.png";
                break;
            case 13:
                ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/companyinfo.png";
                ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/Companywebsite.png";
                ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/Websiteaddsele.png";
                ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/FAQcat.png";
                ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/Askquton.png";
                ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/E-mailtype.png";
                ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/Emailcnct.png";
                ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/Departmnt.png";
                ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/Designation.png";
                ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/Partymastr.png";
                ImgBtnStep1_11.ImageUrl = "~/ShoppingCart/images/Employeemastr.png";
                ImgBtnStep1_12.ImageUrl = "~/ShoppingCart/images/Partyallction.png";
             //   ImgBtnStep1_13.ImageUrl = "~/ShoppingCart/images/1.13.png";
              //  ImageButton1_14.ImageUrl = "~/ShoppingCart/images/1.10.png";

                break;
            case 14:
                ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/companyinfo.png";
                ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/Companywebsite.png";
                ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/Websiteadd.png";
                ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/FAQcatsele.png";
                ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/Askquton.png";
                ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/E-mailtype.png";
                ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/Emailcnct.png";
                ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/Departmnt.png";
                ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/Designation.png";
                ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/Partymastr.png";
                ImgBtnStep1_11.ImageUrl = "~/ShoppingCart/images/Employeemastr.png";
                ImgBtnStep1_12.ImageUrl = "~/ShoppingCart/images/Partyallction.png";
              //  ImgBtnStep1_13.ImageUrl = "~/ShoppingCart/images/1.13.png";
               // ImageButton1_14.ImageUrl = "~/ShoppingCart/images/1.10.png";
                break;
            case 15:
                ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/companyinfo.png";
                ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/Companywebsite.png";
                ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/Websiteadd.png";
                ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/FAQcat.png";
                ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/Askqutonsele.png";
                ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/E-mailtype.png";
                ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/Emailcnct.png";
                ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/Departmnt.png";
                ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/Designation.png";
                ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/Partymastr.png";
                ImgBtnStep1_11.ImageUrl = "~/ShoppingCart/images/Employeemastr.png";
                ImgBtnStep1_12.ImageUrl = "~/ShoppingCart/images/Partyallction.png";
              //  ImgBtnStep1_13.ImageUrl = "~/ShoppingCart/images/1.13.png";
             //   ImageButton1_14.ImageUrl = "~/ShoppingCart/images/1.10.png";
                break;
            case 16:
                ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/companyinfo.png";
                ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/Companywebsite.png";
                ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/Websiteadd.png";
                ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/FAQcat.png";
                ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/Askquton.png";
                ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/E-mailtypesele.png";
                ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/Emailcnct.png";
                ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/Departmnt.png";
                ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/Designation.png";
                ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/Partymastr.png";
                ImgBtnStep1_11.ImageUrl = "~/ShoppingCart/images/Employeemastr.png";
                ImgBtnStep1_12.ImageUrl = "~/ShoppingCart/images/Partyallction.png";
              //  ImgBtnStep1_13.ImageUrl = "~/ShoppingCart/images/1.13.png";
               // ImageButton1_14.ImageUrl = "~/ShoppingCart/images/1.10.png";
                break;
            case 17:
                ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/companyinfo.png";
                ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/Companywebsite.png";
                ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/Websiteadd.png";
                ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/FAQcat.png";
                ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/Askquton.png";
                ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/E-mailtype.png";
                ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/Emailcnctsele.png";
                ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/Departmnt.png";
                ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/Designation.png";
                ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/Partymastr.png";
                ImgBtnStep1_11.ImageUrl = "~/ShoppingCart/images/Employeemastr.png";
                ImgBtnStep1_12.ImageUrl = "~/ShoppingCart/images/Partyallction.png";
               // ImgBtnStep1_13.ImageUrl = "~/ShoppingCart/images/1.13.png";
              //  ImageButton1_14.ImageUrl = "~/ShoppingCart/images/1.10.png";
                break;
            case 18:
                ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/companyinfo.png";
                ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/Companywebsite.png";
                ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/Websiteadd.png";
                ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/FAQcat.png";
                ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/Askquton.png";
                ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/E-mailtype.png";
                ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/Emailcnct.png";
                ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/Departmntsele.png";
                ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/Designation.png";
                ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/Partymastr.png";
                ImgBtnStep1_11.ImageUrl = "~/ShoppingCart/images/Employeemastr.png";
                ImgBtnStep1_12.ImageUrl = "~/ShoppingCart/images/Partyallction.png";
               // ImgBtnStep1_13.ImageUrl = "~/ShoppingCart/images/1.13.png";
                //ImageButton1_14.ImageUrl = "~/ShoppingCart/images/1.10.png";
                break;
            case 19:
                ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/companyinfo.png";
                ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/Companywebsite.png";
                ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/Websiteadd.png";
                ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/FAQcat.png";
                ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/Askquton.png";
                ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/E-mailtype.png";
                ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/Emailcnct.png";
                ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/Departmnt.png";
                ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/Designationsele.png";
                ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/Partymastr.png";
                ImgBtnStep1_11.ImageUrl = "~/ShoppingCart/images/Employeemastr.png";
                ImgBtnStep1_12.ImageUrl = "~/ShoppingCart/images/Partyallction.png";
                //ImgBtnStep1_13.ImageUrl = "~/ShoppingCart/images/1.13.png";
                //ImageButton1_14.ImageUrl = "~/ShoppingCart/images/1.10.png";
                break;

            case 110:
                ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/companyinfo.png";
                ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/Companywebsite.png";
                ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/Websiteadd.png";
                ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/FAQcat.png";
                ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/Askquton.png";
                ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/E-mailtype.png";
                ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/Emailcnct.png";
                ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/Departmnt.png";
                ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/Designation.png";
                ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/Partymastrsele.png";
                ImgBtnStep1_11.ImageUrl = "~/ShoppingCart/images/Employeemastr.png";
                ImgBtnStep1_12.ImageUrl = "~/ShoppingCart/images/Partyallction.png";
              //  ImgBtnStep1_13.ImageUrl = "~/ShoppingCart/images/1.13.png";
              //  ImageButton1_14.ImageUrl = "~/ShoppingCart/images/1.10.png";
                break;
            case 111:
                ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/companyinfo.png";
                ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/Companywebsite.png";
                ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/Websiteadd.png";
                ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/FAQcat.png";
                ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/Askquton.png";
                ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/E-mailtype.png";
                ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/Emailcnct.png";
                ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/Departmnt.png";
                ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/Designation.png";
                ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/Partymastr.png";
                ImgBtnStep1_11.ImageUrl = "~/ShoppingCart/images/Employeemastrsele.png";
                ImgBtnStep1_12.ImageUrl = "~/ShoppingCart/images/Partyallction.png";
                //ImgBtnStep1_13.ImageUrl = "~/ShoppingCart/images/1.13.png";
              //  ImageButton1_14.ImageUrl = "~/ShoppingCart/images/1.10.png";
                break;
            case 112:
                ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/companyinfo.png";
                ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/Companywebsite.png";
                ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/Websiteadd.png";
                ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/FAQcat.png";
                ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/Askquton.png";
                ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/E-mailtype.png";
                ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/Emailcnct.png";
                ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/Departmnt.png";
                ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/Designation.png";
                ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/Partymastr.png";
                ImgBtnStep1_11.ImageUrl = "~/ShoppingCart/images/Employeemastr.png";
                ImgBtnStep1_12.ImageUrl = "~/ShoppingCart/images/Partyallctionsele.png";
               // ImgBtnStep1_13.ImageUrl = "~/ShoppingCart/images/1.13.png";
               // ImageButton1_14.ImageUrl = "~/ShoppingCart/images/1.10.png";
                break;
            case 113:
                ImgBtnStep1_1.ImageUrl = "~/ShoppingCart/images/companyinfo.png";
                ImgBtnStep1_2.ImageUrl = "~/ShoppingCart/images/Companywebsite.png";
                ImgBtnStep1_3.ImageUrl = "~/ShoppingCart/images/Websiteadd.png";
                ImgBtnStep1_4.ImageUrl = "~/ShoppingCart/images/FAQcat.png";
                ImgBtnStep1_5.ImageUrl = "~/ShoppingCart/images/Askquton.png";
                ImgBtnStep1_6.ImageUrl = "~/ShoppingCart/images/E-mailtype.png";
                ImgBtnStep1_7.ImageUrl = "~/ShoppingCart/images/Emailcnct.png";
                ImgBtnStep1_8.ImageUrl = "~/ShoppingCart/images/Departmnt.png";
                ImgBtnStep1_9.ImageUrl = "~/ShoppingCart/images/Designation.png";
                ImgBtnStep1_10.ImageUrl = "~/ShoppingCart/images/Partymastr.png";
                ImgBtnStep1_11.ImageUrl = "~/ShoppingCart/images/Employeemastr.png";
                ImgBtnStep1_12.ImageUrl = "~/ShoppingCart/images/1Partyallction.png";
               // ImgBtnStep1_13.ImageUrl = "~/ShoppingCart/images/1.13hover.png";
               // ImageButton1_14.ImageUrl = "~/ShoppingCart/images/1.10.png";
                break;


               // shipping panel















               // account panel

           

        }
    }





   //protected void ImgBtnStep01_1_Click(object sender, ImageClickEventArgs e)
   //{
   //    //Session["pnlM"] = "1";
   //    //ButtonColor(Convert.ToInt16(Session["pnlM"]));
   //    //Response.Redirect("~/ShoppingCart/Admin/Wizardcompanyinformation.aspx");
   //    Session["pnl1"]="11";
   //    ButtonColor(Convert.ToInt16(Session["pnl1"]));
   //}
   //protected void ImgBtnStep01_2_Click(object sender, ImageClickEventArgs e)
   //{
   //    //Session["pnlM"] = "2";
   //    //ButtonColor(Convert.ToInt16(Session["pnlM"]));
   //    //Response.Redirect("~/ShoppingCart/Admin/wzShippingManager.aspx");
   //    Session["pnl1"] = "12";
   //    ButtonColor(Convert.ToInt16(Session["pnl1"]));
   //}
   //protected void ImgBtnStep01_3_Click(object sender, ImageClickEventArgs e)
   //{
   //    //Session["pnlM"] = "3";
   //    //ButtonColor(Convert.ToInt16(Session["pnlM"]));
   //    //Response.Redirect("~/ShoppingCart/Admin/wzInventorySiteMasterTbl.aspx");
   //    Session["pnl1"] = "13";
   //    ButtonColor(Convert.ToInt16(Session["pnl1"]));
   //}

   //protected void ImgBtnStep01_4_Click(object sender, ImageClickEventArgs e)
   //{
   //    //Session["pnlM"] = "4";
   //    //ButtonColor(Convert.ToInt16(Session["pnlM"]));
   //    //Response.Redirect("~/ShoppingCart/Admin/wzInventorySiteMasterTbl.aspx");
   //    Session["pnl1"] = "14";
   //    ButtonColor(Convert.ToInt16(Session["pnl1"]));
   //}
   //protected void ImgBtnStep01_5_Click(object sender, ImageClickEventArgs e)
   //{
   //    //Session["pnlM"] = "5";
   //    //ButtonColor(Convert.ToInt16(Session["pnlM"]));
   //    //Response.Redirect("~/ShoppingCart/Admin/ApplyVolumeDiscount.aspx");
   //    Session["pnl1"] = "15";
   //    ButtonColor(Convert.ToInt16(Session["pnl1"]));
   //}

   










    protected void ImgBtnStep1_1_Click(object sender, ImageClickEventArgs e)
    {
        Session["pnl1"] = "11";
        ButtonColor(Convert.ToInt16(Session["pnl1"]));
        Response.Redirect("~/ShoppingCart/Admin/Wizardcompanyinformation.aspx");

    }
    protected void ImgBtnStep1_2_Click(object sender, ImageClickEventArgs e)
    {
        Session["pnl1"] = "12";
        ButtonColor(Convert.ToInt16(Session["pnl1"]));
        Response.Redirect("~/ShoppingCart/Admin/WizardCompanyWebsitMaster.aspx");

    }
    protected void ImgBtnStep1_3_Click(object sender, ImageClickEventArgs e)
    {
        Session["pnl1"] = "13";
        ButtonColor(Convert.ToInt16(Session["pnl1"]));
        Response.Redirect("~/ShoppingCart/Admin/WizardCompanyWebsiteAddressMaster.aspx");
    }
   
    protected void ImgBtnStep1_4_Click(object sender, ImageClickEventArgs e)
    {
        Session["pnl1"] = "14";
        ButtonColor(Convert.ToInt16(Session["pnl1"]));
        Response.Redirect("~/ShoppingCart/Admin/WzFaqCategoryMaster.aspx");
    }
    protected void ImgBtnStep1_5_Click(object sender, ImageClickEventArgs e)
    {
        Session["pnl1"] = "15";
        ButtonColor(Convert.ToInt16(Session["pnl1"]));
        Response.Redirect("~/ShoppingCart/Admin/WzFAQMaster.aspx");
    }
    protected void ImgBtnStep1_6_Click(object sender, ImageClickEventArgs e)
    {
        Session["pnl1"] = "16";
        ButtonColor(Convert.ToInt16(Session["pnl1"]));
       // Response.Redirect("~/ShoppingCart/Admin/wzWebContentEntry.aspx");
         Response.Redirect("~/ShoppingCart/Admin/wzEmailTypeMaster.aspx");
    }
    protected void ImgBtnStep1_7_Click(object sender, ImageClickEventArgs e)
    {
        Session["pnl1"] = "17";
        ButtonColor(Convert.ToInt16(Session["pnl1"]));
       // Response.Redirect("~/ShoppingCart/Admin/wzEmailTypeMaster.aspx");
        Response.Redirect("~/ShoppingCart/Admin/wzEmailContentMaster.aspx");
    }
    protected void ImgBtnStep1_8_Click(object sender, ImageClickEventArgs e)
    {
        Session["pnl1"] = "18";
        ButtonColor(Convert.ToInt16(Session["pnl1"]));
        //Response.Redirect("~/ShoppingCart/Admin/WizardInventoryCategoryMaster.aspx");
        //Response.Redirect("~/ShoppingCart/Admin/wzEmailContentMaster.aspx");
        Response.Redirect("~/ShoppingCart/Admin/wzdepartmentmaster.aspx");
    }
    protected void ImgBtnStep1_9_Click(object sender, ImageClickEventArgs e)
    {
        Session["pnl1"] = "19";
        ButtonColor(Convert.ToInt16(Session["pnl1"]));
      //  Response.Redirect("~/ShoppingCart/Admin/wzTaxMasterAndMoreInfo_New.aspx");
        Response.Redirect("~/ShoppingCart/Admin/wzDesignationmaster.aspx");
    }
   
    protected void ImgBtnStep1_10_Click(object sender, ImageClickEventArgs e)
    {
        Session["pnl1"] = "110";
        ButtonColor(Convert.ToInt16(Session["pnl1"]));
        Response.Redirect("~/ShoppingCart/Admin/wzPartyMaster.aspx");
    }
    protected void ImgBtnStep1_11_Click(object sender, ImageClickEventArgs e)
    {
        Session["pnl1"] = "111";
        ButtonColor(Convert.ToInt16(Session["pnl1"]));
        Response.Redirect("~/ShoppingCart/Admin/wzEmployeeMaster.aspx");
    }
    protected void ImgBtnStep1_12_Click(object sender, ImageClickEventArgs e)
    {
        Session["pnl1"] = "112";
        ButtonColor(Convert.ToInt16(Session["pnl1"]));
        Response.Redirect("~/ShoppingCart/Admin/wzpartyautoallocationofmanagers.aspx");
    }
    //protected void ImgBtnStep1_13_Click(object sender, ImageClickEventArgs e)
    //{
    //    Session["pnl1"] = "113";
    //    ButtonColor(Convert.ToInt16(Session["pnl1"]));
       
    //}
    //protected void ImgBtnStep1_14_Click(object sender, ImageClickEventArgs e)
    //{
    //    Session["pnl1"] = "114";
    //    ButtonColor(Convert.ToInt16(Session["pnl1"]));
    //    Response.Redirect("~/ShoppingCart/Admin/WizardforIPStep.aspx");
    //}
     //<td>
     //           <asp:ImageButton    ID="ImageButton5" runat="server" 
    // ImageUrl="~/ShoppingCart/images/1(5).png" OnClick="ImgBtnStep11_5_Click" />
                                           
     //       </td>
}
