using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Account_UserControl_UControlWizardpanel1 : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["pnl"] != null)
        {
            ButtonColor(Convert.ToInt16(Session["pnl"]));
        }
        else
        {
            Session["pnl"] = "1";
            ButtonColor(Convert.ToInt16(Session["pnl"]));
        }
    }
    public string Test()
    {
        String s = null;
        Response.Write("This is Test Message.");
        s = "Test";
        return s;
    }
    protected void ButtonColor(int ButtonId)
    {
        switch (ButtonId)
        {

            case 1:
                btnStep1.ImageUrl = "~/Account/images/BtnStep1sel.jpg";
                //              btnStep1.Width    = System.ui "100px";
                BtnStep2.ImageUrl = "~/Account/images/BtnStep2.jpg";
                BtnStep3.ImageUrl = "~/Account/images/BtnStep3.jpg";
                btnStep4.ImageUrl = "~/Account/images/BtnStep4.jpg";
                btnStep5.ImageUrl = "~/Account/images/BtnStep5.jpg";
                btnstep6.ImageUrl = "~/Account/images/BtnStep6.jpg";
                btnstep7.ImageUrl = "~/Account/images/BtnStep7.jpg";
                btnstep8.ImageUrl = "~/Account/images/BtnStep8.jpg";
                btnstep9.ImageUrl = "~/Account/images/BtnStep9.jpg";
                btnstep10.ImageUrl = "~/Account/images/BtnStep10.jpg";
                btnstep11.ImageUrl = "~/Account/images/BtnStep11.jpg";
                btnstep12.ImageUrl = "~/Account/images/step122.jpg";
                btnStep13.ImageUrl = "~/Account/images/BtnStep13.jpg";
                //btnStep13.ImageUrl = "~/Account/images/BtnStep13.jpg";
                btnStep14.ImageUrl = "~/Account/images/BtnStep14.jpg";
                break;
            case 2:
                btnStep1.ImageUrl = "~/Account/images/BtnStep1.jpg";
                //              btnStep1.Width    = System.ui "100px";
                BtnStep2.ImageUrl = "~/Account/images/BtnStep2sel.jpg";
                BtnStep3.ImageUrl = "~/Account/images/BtnStep3.jpg";
                btnStep4.ImageUrl = "~/Account/images/BtnStep4.jpg";
                btnStep5.ImageUrl = "~/Account/images/BtnStep5.jpg";
                btnstep6.ImageUrl = "~/Account/images/BtnStep6.jpg";
                btnstep7.ImageUrl = "~/Account/images/BtnStep7.jpg";
                btnstep8.ImageUrl = "~/Account/images/BtnStep8.jpg";
                btnstep9.ImageUrl = "~/Account/images/BtnStep9.jpg";
                btnstep10.ImageUrl = "~/Account/images/BtnStep10.jpg";
                btnstep11.ImageUrl = "~/Account/images/BtnStep11.jpg";
                btnstep12.ImageUrl = "~/Account/images/step122.jpg";
                btnStep13.ImageUrl = "~/Account/images/BtnStep13.jpg";
                btnStep14.ImageUrl = "~/Account/images/BtnStep14.jpg";
                break;
            case 3:
                btnStep1.ImageUrl = "~/Account/images/BtnStep1.jpg";
                //              btnStep1.Width    = System.ui "100px";
                BtnStep2.ImageUrl = "~/Account/images/BtnStep2.jpg";
                BtnStep3.ImageUrl = "~/Account/images/BtnStep3sel.jpg";
                btnStep4.ImageUrl = "~/Account/images/BtnStep4.jpg";
                btnStep5.ImageUrl = "~/Account/images/BtnStep5.jpg";
                btnstep6.ImageUrl = "~/Account/images/BtnStep6.jpg";
                btnstep7.ImageUrl = "~/Account/images/BtnStep7.jpg";
                btnstep8.ImageUrl = "~/Account/images/BtnStep8.jpg";
                btnstep9.ImageUrl = "~/Account/images/BtnStep9.jpg";
                btnstep10.ImageUrl = "~/Account/images/BtnStep10.jpg";
                btnstep11.ImageUrl = "~/Account/images/BtnStep11.jpg";
                btnstep12.ImageUrl = "~/Account/images/step122.jpg";
                btnStep13.ImageUrl = "~/Account/images/BtnStep13.jpg";
                btnStep14.ImageUrl = "~/Account/images/BtnStep14.jpg";
                break;
            case 4:
                btnStep1.ImageUrl = "~/Account/images/BtnStep1.jpg";
                //              btnStep1.Width    = System.ui "100px";
                BtnStep2.ImageUrl = "~/Account/images/BtnStep2.jpg";
                BtnStep3.ImageUrl = "~/Account/images/BtnStep3.jpg";
                btnStep4.ImageUrl = "~/Account/images/BtnStep4sel.jpg";
                btnStep5.ImageUrl = "~/Account/images/BtnStep5.jpg";
                btnstep6.ImageUrl = "~/Account/images/BtnStep6.jpg";
                btnstep7.ImageUrl = "~/Account/images/BtnStep7.jpg";
                btnstep8.ImageUrl = "~/Account/images/BtnStep8.jpg";
                btnstep9.ImageUrl = "~/Account/images/BtnStep9.jpg";
                btnstep10.ImageUrl = "~/Account/images/BtnStep10.jpg";
                btnstep11.ImageUrl = "~/Account/images/BtnStep11.jpg";
                btnstep12.ImageUrl = "~/Account/images/step122.jpg";
                btnStep13.ImageUrl = "~/Account/images/BtnStep13.jpg";
                btnStep14.ImageUrl = "~/Account/images/BtnStep14.jpg";
                break;
            case 5:
                btnStep1.ImageUrl = "~/Account/images/BtnStep1.jpg";
                //              btnStep1.Width    = System.ui "100px";
                BtnStep2.ImageUrl = "~/Account/images/BtnStep2.jpg";
                BtnStep3.ImageUrl = "~/Account/images/BtnStep3.jpg";
                btnStep4.ImageUrl = "~/Account/images/BtnStep4.jpg";
                btnStep5.ImageUrl = "~/Account/images/BtnStep5sel.jpg";
                btnstep6.ImageUrl = "~/Account/images/BtnStep6.jpg";
                btnstep7.ImageUrl = "~/Account/images/BtnStep7.jpg";
                btnstep8.ImageUrl = "~/Account/images/BtnStep8.jpg";
                btnstep9.ImageUrl = "~/Account/images/BtnStep9.jpg";
                btnstep10.ImageUrl = "~/Account/images/BtnStep10.jpg";
                btnstep11.ImageUrl = "~/Account/images/BtnStep11.jpg";
                btnstep12.ImageUrl = "~/Account/images/step122.jpg";
                btnStep13.ImageUrl = "~/Account/images/BtnStep13.jpg";
                btnStep14.ImageUrl = "~/Account/images/BtnStep14.jpg";
                break;
            case 6:
                btnStep1.ImageUrl = "~/Account/images/BtnStep1.jpg";
                //              btnStep1.Width    = System.ui "100px";
                BtnStep2.ImageUrl = "~/Account/images/BtnStep2.jpg";
                BtnStep3.ImageUrl = "~/Account/images/BtnStep3.jpg";
                btnStep4.ImageUrl = "~/Account/images/BtnStep4.jpg";
                btnStep5.ImageUrl = "~/Account/images/BtnStep5.jpg";
                btnstep6.ImageUrl = "~/Account/images/BtnStep6sel.jpg";
                btnstep7.ImageUrl = "~/Account/images/BtnStep7.jpg";
                btnstep8.ImageUrl = "~/Account/images/BtnStep8.jpg";
                btnstep9.ImageUrl = "~/Account/images/BtnStep9.jpg";
                btnstep10.ImageUrl = "~/Account/images/BtnStep10.jpg";
                btnstep11.ImageUrl = "~/Account/images/BtnStep11.jpg";
                btnstep12.ImageUrl = "~/Account/images/step122.jpg";
                btnStep13.ImageUrl = "~/Account/images/BtnStep13.jpg";
                btnStep14.ImageUrl = "~/Account/images/BtnStep14.jpg";
                break;
            case 7:
                btnStep1.ImageUrl = "~/Account/images/BtnStep1.jpg";
                //              btnStep1.Width    = System.ui "100px";
                BtnStep2.ImageUrl = "~/Account/images/BtnStep2.jpg";
                BtnStep3.ImageUrl = "~/Account/images/BtnStep3.jpg";
                btnStep4.ImageUrl = "~/Account/images/BtnStep4.jpg";
                btnStep5.ImageUrl = "~/Account/images/BtnStep5.jpg";
                btnstep6.ImageUrl = "~/Account/images/BtnStep6.jpg";
                btnstep7.ImageUrl = "~/Account/images/BtnStep7sel.jpg";
                btnstep8.ImageUrl = "~/Account/images/BtnStep8.jpg";
                btnstep9.ImageUrl = "~/Account/images/BtnStep9.jpg";
                btnstep10.ImageUrl = "~/Account/images/BtnStep10.jpg";
                btnstep11.ImageUrl = "~/Account/images/BtnStep11.jpg";
                btnstep12.ImageUrl = "~/Account/images/step122.jpg";
                btnStep13.ImageUrl = "~/Account/images/BtnStep13.jpg";
                btnStep14.ImageUrl = "~/Account/images/BtnStep14.jpg";

                break;
            case 8:
                btnStep1.ImageUrl = "~/Account/images/BtnStep1.jpg";
                //              btnStep1.Width    = System.ui "100px";
                BtnStep2.ImageUrl = "~/Account/images/BtnStep2.jpg";
                BtnStep3.ImageUrl = "~/Account/images/BtnStep3.jpg";
                btnStep4.ImageUrl = "~/Account/images/BtnStep4.jpg";
                btnStep5.ImageUrl = "~/Account/images/BtnStep5.jpg";
                btnstep6.ImageUrl = "~/Account/images/BtnStep6.jpg";
                btnstep7.ImageUrl = "~/Account/images/BtnStep7.jpg";
                btnstep8.ImageUrl = "~/Account/images/BtnStep8sel.jpg";
                btnstep9.ImageUrl = "~/Account/images/BtnStep9.jpg";
                btnstep10.ImageUrl = "~/Account/images/BtnStep10.jpg";
                btnstep11.ImageUrl = "~/Account/images/BtnStep11.jpg";
                btnstep12.ImageUrl = "~/Account/images/step122.jpg";
                btnStep13.ImageUrl = "~/Account/images/BtnStep13.jpg";
                btnStep14.ImageUrl = "~/Account/images/BtnStep14.jpg";
                break;
            case 9:
                btnStep1.ImageUrl = "~/Account/images/BtnStep1.jpg";
                //              btnStep1.Width    = System.ui "100px";
                BtnStep2.ImageUrl = "~/Account/images/BtnStep2.jpg";
                BtnStep3.ImageUrl = "~/Account/images/BtnStep3.jpg";
                btnStep4.ImageUrl = "~/Account/images/BtnStep4.jpg";
                btnStep5.ImageUrl = "~/Account/images/BtnStep5.jpg";
                btnstep6.ImageUrl = "~/Account/images/BtnStep6.jpg";
                btnstep7.ImageUrl = "~/Account/images/BtnStep7.jpg";
                btnstep8.ImageUrl = "~/Account/images/BtnStep8.jpg";
                btnstep9.ImageUrl = "~/Account/images/BtnStep9sel.jpg";
                btnstep10.ImageUrl = "~/Account/images/BtnStep10.jpg";
                btnstep11.ImageUrl = "~/Account/images/BtnStep11.jpg";
                btnstep12.ImageUrl = "~/Account/images/step122.jpg";
                btnStep13.ImageUrl = "~/Account/images/BtnStep13.jpg";
                btnStep14.ImageUrl = "~/Account/images/BtnStep14.jpg";
                break;

            case 10:
                btnStep1.ImageUrl = "~/Account/images/BtnStep1.jpg";
                //              btnStep1.Width    = System.ui "100px";
                BtnStep2.ImageUrl = "~/Account/images/BtnStep2.jpg";
                BtnStep3.ImageUrl = "~/Account/images/BtnStep3.jpg";
                btnStep4.ImageUrl = "~/Account/images/BtnStep4.jpg";
                btnStep5.ImageUrl = "~/Account/images/BtnStep5.jpg";
                btnstep6.ImageUrl = "~/Account/images/BtnStep6.jpg";
                btnstep7.ImageUrl = "~/Account/images/BtnStep7.jpg";
                btnstep8.ImageUrl = "~/Account/images/BtnStep8.jpg";
                btnstep9.ImageUrl = "~/Account/images/BtnStep9.jpg";
                btnstep10.ImageUrl = "~/Account/images/BtnStep10sel.jpg";
                btnstep11.ImageUrl = "~/Account/images/BtnStep11.jpg";
                btnstep12.ImageUrl = "~/Account/images/step122.jpg";
                btnStep13.ImageUrl = "~/Account/images/BtnStep13.jpg";
                btnStep14.ImageUrl = "~/Account/images/BtnStep14.jpg";
                break;
            case 11:
                btnStep1.ImageUrl = "~/Account/images/BtnStep1.jpg";
                //              btnStep1.Width    = System.ui "100px";
                BtnStep2.ImageUrl = "~/Account/images/BtnStep2.jpg";
                BtnStep3.ImageUrl = "~/Account/images/BtnStep3.jpg";
                btnStep4.ImageUrl = "~/Account/images/BtnStep4.jpg";
                btnStep5.ImageUrl = "~/Account/images/BtnStep5.jpg";
                btnstep6.ImageUrl = "~/Account/images/BtnStep6.jpg";
                btnstep7.ImageUrl = "~/Account/images/BtnStep7.jpg";
                btnstep8.ImageUrl = "~/Account/images/BtnStep8.jpg";
                btnstep9.ImageUrl = "~/Account/images/BtnStep9.jpg";
                btnstep10.ImageUrl = "~/Account/images/BtnStep10.jpg";
                btnstep11.ImageUrl = "~/Account/images/BtnStep11sel.jpg";
                btnstep12.ImageUrl = "~/Account/images/step122.jpg";
                btnStep13.ImageUrl = "~/Account/images/BtnStep13.jpg";
                btnStep14.ImageUrl = "~/Account/images/BtnStep14.jpg";
                break;
            case 12:
                btnStep1.ImageUrl = "~/Account/images/BtnStep1.jpg";
                //              btnStep1.Width    = System.ui "100px";
                BtnStep2.ImageUrl = "~/Account/images/BtnStep2.jpg";
                BtnStep3.ImageUrl = "~/Account/images/BtnStep3.jpg";
                btnStep4.ImageUrl = "~/Account/images/BtnStep4.jpg";
                btnStep5.ImageUrl = "~/Account/images/BtnStep5.jpg";
                btnstep6.ImageUrl = "~/Account/images/BtnStep6.jpg";
                btnstep7.ImageUrl = "~/Account/images/BtnStep7.jpg";
                btnstep8.ImageUrl = "~/Account/images/BtnStep8.jpg";
                btnstep9.ImageUrl = "~/Account/images/BtnStep9.jpg";
                btnstep10.ImageUrl = "~/Account/images/BtnStep10.jpg";
                btnstep11.ImageUrl = "~/Account/images/BtnStep11.jpg";
                btnstep11.ImageUrl = "~/Account/images/BtnStep11.jpg";
                btnstep12.ImageUrl = "~/Account/images/BtnStep12sel.jpg";
                btnStep13.ImageUrl = "~/Account/images/BtnStep13.jpg";
                btnStep14.ImageUrl = "~/Account/images/BtnStep14.jpg";
                break;
            case 13:
                btnStep1.ImageUrl = "~/Account/images/BtnStep1.jpg";
                //              btnStep1.Width    = System.ui "100px";
                BtnStep2.ImageUrl = "~/Account/images/BtnStep2.jpg";
                BtnStep3.ImageUrl = "~/Account/images/BtnStep3.jpg";
                btnStep4.ImageUrl = "~/Account/images/BtnStep4.jpg";
                btnStep5.ImageUrl = "~/Account/images/BtnStep5.jpg";
                btnstep6.ImageUrl = "~/Account/images/BtnStep6.jpg";
                btnstep7.ImageUrl = "~/Account/images/BtnStep7.jpg";
                btnstep8.ImageUrl = "~/Account/images/BtnStep8.jpg";
                btnstep9.ImageUrl = "~/Account/images/BtnStep9.jpg";
                btnstep10.ImageUrl = "~/Account/images/BtnStep10.jpg";
                btnstep11.ImageUrl = "~/Account/images/BtnStep11.jpg";
                btnstep11.ImageUrl = "~/Account/images/BtnStep11.jpg";
                btnstep12.ImageUrl = "~/Account/images/step122.jpg";
                btnStep13.ImageUrl = "~/Account/images/BtnStep13sel.jpg";
                btnStep14.ImageUrl = "~/Account/images/BtnStep14.jpg";
                break;
            case 14:
                btnStep1.ImageUrl = "~/Account/images/BtnStep1.jpg";
                //              btnStep1.Width    = System.ui "100px";
                BtnStep2.ImageUrl = "~/Account/images/BtnStep2.jpg";
                BtnStep3.ImageUrl = "~/Account/images/BtnStep3.jpg";
                btnStep4.ImageUrl = "~/Account/images/BtnStep4.jpg";
                btnStep5.ImageUrl = "~/Account/images/BtnStep5.jpg";
                btnstep6.ImageUrl = "~/Account/images/BtnStep6.jpg";
                btnstep7.ImageUrl = "~/Account/images/BtnStep7.jpg";
                btnstep8.ImageUrl = "~/Account/images/BtnStep8.jpg";
                btnstep9.ImageUrl = "~/Account/images/BtnStep9.jpg";
                btnstep10.ImageUrl = "~/Account/images/BtnStep10.jpg";
                btnstep11.ImageUrl = "~/Account/images/BtnStep11.jpg";
                btnstep11.ImageUrl = "~/Account/images/BtnStep11.jpg";
                btnstep12.ImageUrl = "~/Account/images/step122.jpg";
                btnStep13.ImageUrl = "~/Account/images/BtnStep13.jpg";
                btnStep14.ImageUrl = "~/Account/images/BtnStep14sel.jpg";
                break;
        }
    }
    protected void btnStep1_Click(object sender, ImageClickEventArgs e)
    {
        Session["pnl"] = "1";
        ButtonColor(Convert.ToInt16(Session["pnl"]));
        Response.Redirect("~/Account/enteryourcompanyinformation.aspx");

    }
    protected void BtnStep2_Click(object sender, ImageClickEventArgs e)
    {
        Session["pnl"] = "2";
        ButtonColor(Convert.ToInt16(Session["pnl"]));
        Response.Redirect("~/Account/wizardMasterDepartmentManagement.aspx");

    }
    protected void BtnStep3_Click(object sender, ImageClickEventArgs e)
    {
        Session["pnl"] = "3";
        ButtonColor(Convert.ToInt16(Session["pnl"]));
        Response.Redirect("~/Account/WizardMasterDesignationManagement.aspx");
    }
    protected void btnStep4_Click(object sender, ImageClickEventArgs e)
    {
        Session["pnl"] = "4";
        ButtonColor(Convert.ToInt16(Session["pnl"]));
        Response.Redirect("~/Account/WizardDocumentCabinet.aspx");
    }
    protected void btnStep5_Click(object sender, ImageClickEventArgs e)
    {
        Session["pnl"] = "5";
        ButtonColor(Convert.ToInt16(Session["pnl"]));
        Response.Redirect("~/Account/wizardEmployeeAddNew.aspx");
    }
    protected void btnstep6_Click(object sender, ImageClickEventArgs e)
    {
        Session["pnl"] = "6";
        ButtonColor(Convert.ToInt16(Session["pnl"]));

        Response.Redirect("~/Account/wizardMasterPartyMaster.aspx");

    }
    protected void btnstep7_Click(object sender, ImageClickEventArgs e)
    {
        Session["pnl"] = "7";
        ButtonColor(Convert.ToInt16(Session["pnl"]));
        Response.Redirect("~/Account/wizardMasterAssignAccessrightMaster.aspx");
    }
    protected void btnstep8_Click(object sender, ImageClickEventArgs e)
    {
        Session["pnl"] = "8";
        ButtonColor(Convert.ToInt16(Session["pnl"]));
        Response.Redirect("~/Account/wizardDocumentAccesRight.aspx");
    }
    protected void btnstep9_Click(object sender, ImageClickEventArgs e)
    {
        Session["pnl"] = "9";
        ButtonColor(Convert.ToInt16(Session["pnl"]));
        Response.Redirect("~/Account/WizardDocumentApproveTypeMaster.aspx");
    }
    protected void btnstep10_Click(object sender, ImageClickEventArgs e)
    {
        Session["pnl"] = "10";
        ButtonColor(Convert.ToInt16(Session["pnl"]));
        Response.Redirect("~/Account/wizardforIPStep.aspx");
    }
    protected void btnstep11_Click(object sender, ImageClickEventArgs e)
    {
        Session["pnl"] = "11";
        ButtonColor(Convert.ToInt16(Session["pnl"]));
        Response.Redirect("~/Account/wizardDocumentEmailDownload.aspx");
    }
    protected void btnstep12_Click(object sender, ImageClickEventArgs e)
    {
        Session["pnl"] = "12";
        ButtonColor(Convert.ToInt16(Session["pnl"]));
        Response.Redirect("~/Account/WizardDocumentFTPDownload.aspx");
    }
    //protected void btnStep13_Click(object sender, ImageClickEventArgs e)
    //{
    //    Session["pnl"] = "13";
    //    ButtonColor(Convert.ToInt16(Session["pnl"]));
    //    Response.Redirect("~/Account/wizardDocumentEmailDownload.aspx");
    //}
    //protected void btnStep14_Click(object sender, ImageClickEventArgs e)
    //{
    //    Session["pnl"] = "14";
    //    ButtonColor(Convert.ToInt16(Session["pnl"]));
    //    Response.Redirect("~/Account/WizardDocumentFTPDownload.aspx");
    //}


    protected void btnstep13_Click(object sender, ImageClickEventArgs e)
    {
        Session["pnl"] = "13";
        ButtonColor(Convert.ToInt16(Session["pnl"]));
        Response.Redirect("~/Account/WizardAutoAllocation.aspx");
    }
    protected void btnStep14_Click(object sender, ImageClickEventArgs e)
    {
        Session["pnl"] = "14";
        ButtonColor(Convert.ToInt16(Session["pnl"]));
        Response.Redirect("~/Account/DocumentAutoAllocationFolder.aspx");
    }
}
