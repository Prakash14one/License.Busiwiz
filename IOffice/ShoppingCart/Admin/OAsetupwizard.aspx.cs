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
using System.Data.SqlClient;


public partial class OAsetupwizard : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(PageConn.connnn);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        }

        if (!IsPostBack)
        {
            business();
           // fillstep1business();
          //  datacheck();
           // othercheck();

            // cabinet means accounts year
            // drawer means  opening balance
            // folder means setup taxes
            // email rule payment option
            // ftp rule mean volume discount
            // folder rule mean customer discount

            

        }
    }

    // Business
    protected void business()
    {
        string business = "select WareHouseMaster.Name,WareHouseMaster.WareHouseId from WareHouseMaster inner join employeemaster on employeemaster.whid=WareHouseMaster.WarehouseId where employeemaster.employeemasterid='" + Session["EmployeeId"] + "'";
        SqlDataAdapter da = new SqlDataAdapter(business, con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            lblbusiness.Text = dt.Rows[0]["Name"].ToString();

            string whid = dt.Rows[0]["WareHouseId"].ToString();
            departments(whid);

            designation(whid);
            fillaccountyearofbusiness(whid);


        }
    }
    protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CheckBoxList1.SelectedValue == "1")
        {
            string te = "Wizardcompanywebsitmaster.aspx";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
            ModalPopupExtender1.Show();

        }
        if (CheckBoxList1.SelectedValue == "2")
        {
            pnldepartment.Visible = true;
            pnlnotrequire.Visible = true;
            pnldone.Visible = false;
            pnlyesno.Visible = false;
            pnltryagain.Visible = false;

            // 1 Pending
            // 2 Done
            // 3 Not Require

            //DataTable dt = fillstep1business();
            //if (dt.Rows.Count > 0)
            //{
            //    insertdate("Business", "3");

            //}



        }
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        string te = "Wizardcompanywebsitmaster.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        ModalPopupExtender1.Show();

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        pnldepartment.Visible = true;
        pnlyesno.Visible = false;
        pnldone.Visible = true;
        pnlnotrequire.Visible = false;
        pnltryagain.Visible = false;

        //DataTable dt = fillstep1business();
        //if (dt.Rows.Count > 0)
        //{
        //    insertdate("Business", "2");

        //}

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        pnldepartment.Visible = false;
        pnlnotrequire.Visible = false;
        pnldone.Visible = false;
        pnlyesno.Visible = false;
        pnltryagain.Visible = true;


    }

    // End Business

    // Department
    protected void CheckBoxList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CheckBoxList2.SelectedValue == "1")
        {
            string te = "departmentaddmanage.aspx";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
            ModalPopupExtender12.Show();

        }
        if (CheckBoxList2.SelectedValue == "2")
        {
            pnldesignation.Visible = true;
            pnldeptyesno.Visible = false;
            pnldeptdone.Visible = false;
            pnldeptnotrequire.Visible = true;
            pnldepttryagain.Visible = false;

            //DataTable dt = fillstep1business();
            //if (dt.Rows.Count > 0)
            //{
            //    insertdate("Department", "3");

            //}

        }
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        string te = "departmentaddmanage.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        ModalPopupExtender12.Show();

    }
    protected void LinkButton15_Click(object sender, EventArgs e)
    {
        
        ModalPopupExtenderpnldeptgrid.Show();

    }
    protected void btnpnl2_Click(object sender, EventArgs e)
    {
        pnldesignation.Visible = true;
        pnldeptyesno.Visible = false;
        pnldeptdone.Visible = true;
        pnldeptnotrequire.Visible = false;
        pnldepttryagain.Visible = false;

        //DataTable dt = fillstep1business();
        //if (dt.Rows.Count > 0)
        //{
        //    insertdate("Department", "2");

        //}
    }
    protected void btnpnl22_Click(object sender, EventArgs e)
    {
        pnldesignation.Visible = false;
        pnldeptyesno.Visible = false;
        pnldeptdone.Visible = false;
        pnldeptnotrequire.Visible = false;
        pnldepttryagain.Visible = true;
    }
    // End Department

    //Designation
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        ModalPopupExtender2.Show();
    }
    protected void CheckBoxList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CheckBoxList3.SelectedValue == "1")
        {
            string te = "designationaddmanage.aspx";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

            ModalPopupExtender13.Show();
        }
        if (CheckBoxList3.SelectedValue == "2")
        {
            pnlemployee.Visible = true;
            pnldegnyesno.Visible = false;
            pnldegndone.Visible = false;
            pnldegnnotrequire.Visible = true;
            pnldegntryagain.Visible = false;

            //DataTable dt = fillstep1business();
            //if (dt.Rows.Count > 0)
            //{
            //    insertdate("Designation", "3");

            //}
        }
    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        string te = "designationaddmanage.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

        ModalPopupExtender13.Show();
    }
    protected void btnpnl3_Click(object sender, EventArgs e)
    {

        pnlemployee.Visible = true;
        pnldegnyesno.Visible = false;
        pnldegndone.Visible = true;
        pnldegnnotrequire.Visible = false;
        pnldegntryagain.Visible = false;
        //DataTable dt = fillstep1business();
        //if (dt.Rows.Count > 0)
        //{
        //    insertdate("Designation", "2");

        //}

    }
    protected void btnpnl33_Click(object sender, EventArgs e)
    {
        pnlemployee.Visible = false;
        pnldegnyesno.Visible = false;
        pnldegndone.Visible = false;
        pnldegnnotrequire.Visible = false;
        pnldegntryagain.Visible = true;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        ModalPopupExtender2.Hide();
    }

    // End Designation

    // Employee
    protected void CheckBoxList5_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CheckBoxList5.SelectedValue == "1")
        {
            string te = "EmployeeMaster.aspx";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
            Modalemployeepopup.Show();

        }
        if (CheckBoxList5.SelectedValue == "2")
        {
            pnlcabinet.Visible = true;
            pnlempyesno.Visible = false;
            pnlempdone.Visible = false;
            pnlempnotrequire.Visible = true;
            pnlemptryagain.Visible = false;

            //DataTable dt = fillstep1business();
            //if (dt.Rows.Count > 0)
            //{
            //    insertdate("Employee", "3");

            //}


        }
    }
    protected void LinkButton6_Click(object sender, EventArgs e)
    {
        string te = "EmployeeMaster.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        Modalemployeepopup.Show();

    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        pnlcabinet.Visible = true;
        pnlempyesno.Visible = false;
        pnlempdone.Visible = true;
        pnlempnotrequire.Visible = false;
        pnlemptryagain.Visible = false;

        //DataTable dt = fillstep1business();
        //if (dt.Rows.Count > 0)
        //{
        //    insertdate("Employee", "2");

        //}


    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        pnlcabinet.Visible = false;
        pnlempyesno.Visible = false;
        pnlempdone.Visible = false;
        pnlempnotrequire.Visible = false;
        pnlemptryagain.Visible = true;

    }
    // End Employee

    // Account year
    protected void CheckBoxList4_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CheckBoxList4.SelectedValue == "1")
        {
            string te = "AccountYearChange.aspx";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
            ModalPopupExtendercabinetpopup.Show();

        }
        if (CheckBoxList4.SelectedValue == "2")
        {
            pnldrawer.Visible = true;
            pnlcabinetyesno.Visible = false;
            pnlcabinetdone.Visible = false;
            pnlcabinetnotrequire.Visible = true;
            pnlcabinettryagain.Visible = false;

            //DataTable dt = fillstep1business();
            //if (dt.Rows.Count > 0)
            //{
            //    insertdate("Cabinet", "3");

            //}

        }

    }
    protected void Button7_Click(object sender, EventArgs e)
    {
        business();
        pnldrawer.Visible = true;
        pnlcabinetyesno.Visible = false;
        pnlcabinetdone.Visible = true;
        pnlcabinetnotrequire.Visible = false;
        pnlcabinettryagain.Visible = false;

        //DataTable dt = fillstep1business();
        //if (dt.Rows.Count > 0)
        //{
        //    insertdate("Cabinet", "2");

        //}
    }
    protected void Button8_Click(object sender, EventArgs e)
    {
        pnldrawer.Visible = false;
        pnlcabinetyesno.Visible = false;
        pnlcabinetdone.Visible = false;
        pnlcabinetnotrequire.Visible = false;
        pnlcabinettryagain.Visible = true;
    }
    protected void LinkButton5_Click(object sender, EventArgs e)
    {
        string te = "AccountYearChange.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        ModalPopupExtendercabinetpopup.Show();


    }

    // End Account year

    // Opening Balance
    protected void CheckBoxList6_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CheckBoxList6.SelectedValue == "1")
        {
            string te = "Opening_Balance.aspx";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
            ModalPopupExtenderdrawerpopup.Show();

        }
        if (CheckBoxList6.SelectedValue == "2")
        {
            pnlfolder.Visible = true;
            pnldraweryesno.Visible = false;
            pnldrawerdone.Visible = false;
            pnldrawernotrequire.Visible = true;
            pnldrawertryagain.Visible = false;

            //DataTable dt = fillstep1business();
            //if (dt.Rows.Count > 0)
            //{
            //    insertdate("Drawer", "3");

            //}
        }

    }
    protected void LinkButton7_Click(object sender, EventArgs e)
    {
        string te = "Opening_Balance.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        ModalPopupExtenderdrawerpopup.Show();


    }
    protected void Button10_Click(object sender, EventArgs e)
    {
        pnlfolder.Visible = true;
        pnldraweryesno.Visible = false;
        pnldrawerdone.Visible = true;
        pnldrawernotrequire.Visible = false;
        pnldrawertryagain.Visible = false;

        //DataTable dt = fillstep1business();
        //if (dt.Rows.Count > 0)
        //{
        //    insertdate("Drawer", "2");

        //}

    }
    protected void Button11_Click(object sender, EventArgs e)
    {
        pnlfolder.Visible = false;
        pnldraweryesno.Visible = false;
        pnldrawerdone.Visible = false;
        pnldrawernotrequire.Visible = false;
        pnldrawertryagain.Visible = true;
    }
    // End Opening Balance

    //  setup taxes
    protected void CheckBoxList7_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CheckBoxList7.SelectedValue == "1")
        {
            string te = "StoreTaxMethodTBL.aspx";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
            ModalPopupExtenderfolderpopup.Show();

        }
        if (CheckBoxList7.SelectedValue == "2")
        {
            pnlemailrule.Visible = true;
            pnlfolderyesno.Visible = false;
            pnlfolderdone.Visible = false;
            pnlfoldernotrequire.Visible = true;
            pnlfoldertryagain.Visible = false;

            //DataTable dt = fillstep1business();
            //if (dt.Rows.Count > 0)
            //{
            //    insertdate("Folder", "3");

            //}
        }

    }
    protected void Button13_Click(object sender, EventArgs e)
    {
        pnlemailrule.Visible = true;
        pnlfolderyesno.Visible = false;
        pnlfolderdone.Visible = true;
        pnlfoldernotrequire.Visible = false;
        pnlfoldertryagain.Visible = false;

        //DataTable dt = fillstep1business();
        //if (dt.Rows.Count > 0)
        //{
        //    insertdate("Folder", "2");

        //}

    }
    protected void Button14_Click(object sender, EventArgs e)
    {
        pnlemailrule.Visible = false;
        pnlfolderyesno.Visible = false;
        pnlfolderdone.Visible = false;
        pnlfoldernotrequire.Visible = false;
        pnlfoldertryagain.Visible = true;
    }
    protected void LinkButton8_Click(object sender, EventArgs e)
    {
        string te = "StoreTaxMethodTBL.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        ModalPopupExtenderfolderpopup.Show();
    }

    // End  setup taxes

    // payment option
    protected void CheckBoxList8_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CheckBoxList8.SelectedValue == "1")
        {
            string te = "PaymnentOption.aspx";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
            ModalPopupExtenderemailrulepopup.Show();

        }
        if (CheckBoxList8.SelectedValue == "2")
        {
            pnlftprule.Visible = true;
            pnlemailruleyesno.Visible = false;
            pnlemailruledone.Visible = false;
            pnlemailrulenotrequire.Visible = true;
            pnlemailruletryagain.Visible = false;

            //DataTable dt = fillstep1business();
            //if (dt.Rows.Count > 0)
            //{
            //    insertdate("Emailrule", "3");

            //}
        }

    }
    protected void LinkButton9_Click(object sender, EventArgs e)
    {
        string te = "PaymnentOption.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        ModalPopupExtenderemailrulepopup.Show();

    }
    protected void Button16_Click(object sender, EventArgs e)
    {
        pnlftprule.Visible = true;
        pnlemailruleyesno.Visible = false;
        pnlemailruledone.Visible = true;
        pnlemailrulenotrequire.Visible = false;
        pnlemailruletryagain.Visible = false;

        //DataTable dt = fillstep1business();
        //if (dt.Rows.Count > 0)
        //{
        //    insertdate("Emailrule", "2");

        //}
    }
    protected void Button17_Click(object sender, EventArgs e)
    {
        pnlftprule.Visible = false;
        pnlemailruleyesno.Visible = false;
        pnlemailruledone.Visible = false;
        pnlemailrulenotrequire.Visible = false;
        pnlemailruletryagain.Visible = true;

    }

    // End payment option

    // volume discount
    protected void CheckBoxList9_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CheckBoxList9.SelectedValue == "1")
        {
            string te = "ApplyVolumeDiscount.aspx";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
            ModalPopupExtenderftprulepopup.Show();

        }
        if (CheckBoxList9.SelectedValue == "2")
        {
            pnlfolderrule.Visible = true;
            pnlftpruleyesno.Visible = false;
            pnlftpruledone.Visible = false;
            pnlftprulenotrequire.Visible = true;
            pnlftpruletryagain.Visible = false;

            //DataTable dt = fillstep1business();
            //if (dt.Rows.Count > 0)
            //{
            //    insertdate("FtpRule", "3");

            //}

        }

    }
    protected void Button19_Click(object sender, EventArgs e)
    {
        pnlfolderrule.Visible = true;
        pnlftpruleyesno.Visible = false;
        pnlftpruledone.Visible = true;
        pnlftprulenotrequire.Visible = false;
        pnlftpruletryagain.Visible = false;

        //DataTable dt = fillstep1business();
        //if (dt.Rows.Count > 0)
        //{
        //    insertdate("FtpRule", "2");

        //}
    }
    protected void Button20_Click(object sender, EventArgs e)
    {
        pnlfolderrule.Visible = false;
        pnlftpruleyesno.Visible = false;
        pnlftpruledone.Visible = false;
        pnlftprulenotrequire.Visible = false;
        pnlftpruletryagain.Visible = true;
    }
    protected void LinkButton10_Click(object sender, EventArgs e)
    {
        string te = "ApplyVolumeDiscount.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        ModalPopupExtenderftprulepopup.Show();

    }

    // End volume discount

    // discount for your customers
    protected void CheckBoxList10_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CheckBoxList10.SelectedValue == "1")
        {
            string te = "OrderValueDiscountMaster.aspx";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
            ModalPopupExtenderfolderrulepopup.Show();

        }
        if (CheckBoxList10.SelectedValue == "2")
        {
            pnlfinish.Visible = true;
            pnlfolderruleyesno.Visible = false;
            pnlfolderruledone.Visible = false;
            pnlfolderrulenotrequire.Visible = true;
            pnlfolderruletryagain.Visible = false;

            //DataTable dt = fillstep1business();
            //if (dt.Rows.Count > 0)
            //{
            //    insertdate("FolderRule", "3");

            //}
        }
    }
    protected void LinkButton11_Click(object sender, EventArgs e)
    {
        string te = "OrderValueDiscountMaster.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        ModalPopupExtenderfolderrulepopup.Show();

    }
    protected void Button22_Click(object sender, EventArgs e)
    {
          pnlfinish.Visible = true;
        pnlfolderruleyesno.Visible = false;
        pnlfolderruledone.Visible = true;
        pnlfolderrulenotrequire.Visible = false;
        pnlfolderruletryagain.Visible = false;

        //DataTable dt = fillstep1business();
        //if (dt.Rows.Count > 0)
        //{
        //    insertdate("FolderRule", "2");

        //}
    }
    protected void Button23_Click(object sender, EventArgs e)
    {
        pnlfinish.Visible = false;
        pnlfolderruleyesno.Visible = false;
        pnlfolderruledone.Visible = false;
        pnlfolderrulenotrequire.Visible = false;
        pnlfolderruletryagain.Visible = true;
    }
    // End discount for your customers

    //// File Storage Rule
    //protected void CheckBoxList11_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (CheckBoxList11.SelectedValue == "1")
    //    {
    //        string te = "FileStorage.aspx";
    //        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    //        ModalPopupExtenderfilestoragepopup.Show();

    //    }
    //    if (CheckBoxList11.SelectedValue == "2")
    //    {
    //        pnlfillingdeskrule.Visible = true;
    //        pnlfilestorageruleyesno.Visible = false;
    //        pnlfilestorageruledone.Visible = false;
    //        pnlfilestoragerulenotrequire.Visible = true;
    //        pnlfilestorageruletryagain.Visible = false;

    //        //DataTable dt = fillstep1business();
    //        //if (dt.Rows.Count > 0)
    //        //{
    //        //    insertdate("FileStorageRule", "3");

    //        //}
    //    }

    //}
    //protected void Button25_Click(object sender, EventArgs e)
    //{
    //    pnlfillingdeskrule.Visible = true;
    //    pnlfilestorageruleyesno.Visible = false;
    //    pnlfilestorageruledone.Visible = true;
    //    pnlfilestoragerulenotrequire.Visible = false;
    //    pnlfilestorageruletryagain.Visible = false;

    //    //DataTable dt = fillstep1business();
    //    //if (dt.Rows.Count > 0)
    //    //{
    //    //    insertdate("FileStorageRule", "2");

    //    //}
    //}
    //protected void Button26_Click(object sender, EventArgs e)
    //{
    //    pnlfillingdeskrule.Visible = false;
    //    pnlfilestorageruleyesno.Visible = false;
    //    pnlfilestorageruledone.Visible = false;
    //    pnlfilestoragerulenotrequire.Visible = false;
    //    pnlfilestorageruletryagain.Visible = true;
    //}
    //protected void LinkButton12_Click(object sender, EventArgs e)
    //{
    //    string te = "FileStorage.aspx";
    //    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    //    ModalPopupExtenderfilestoragepopup.Show();

    //}
    //// End File Storage Rule

    //// Wizard Auto Allocation Rule
    //protected void CheckBoxList12_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (CheckBoxList12.SelectedValue == "1")
    //    {
    //        string te = "WizardAutoAllocation.aspx";
    //        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    //        ModalPopupExtenderfillingdeskrule.Show();

    //    }
    //    if (CheckBoxList12.SelectedValue == "2")
    //    {
    //        pnldocumentflowrule.Visible = true;
    //        pnlfillingdeskruleyesno.Visible = false;
    //        pnlfillingdeskruledone.Visible = false;
    //        pnlfillingdeskrulenotrequire.Visible = true;
    //        pnlfillingdeskruletryagain.Visible = false;

    //        //DataTable dt = fillstep1business();
    //        //if (dt.Rows.Count > 0)
    //        //{
    //        //    insertdate("FillingDeskrule", "3");

    //        //}

    //    }
    //}
    //protected void LinkButton13_Click(object sender, EventArgs e)
    //{
    //    string te = "WizardAutoAllocation.aspx";
    //    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    //    ModalPopupExtenderfillingdeskrule.Show();
    //}
    //protected void Button28_Click(object sender, EventArgs e)
    //{
    //    pnldocumentflowrule.Visible = true;
    //    pnlfillingdeskruleyesno.Visible = false;
    //    pnlfillingdeskruledone.Visible = true;
    //    pnlfillingdeskrulenotrequire.Visible = false;
    //    pnlfillingdeskruletryagain.Visible = false;

    //    //DataTable dt = fillstep1business();
    //    //if (dt.Rows.Count > 0)
    //    //{
    //    //    insertdate("FillingDeskrule", "2");

    //    //}

    //}
    //protected void Button29_Click(object sender, EventArgs e)
    //{
    //    pnldocumentflowrule.Visible = false;
    //    pnlfillingdeskruleyesno.Visible = false;
    //    pnlfillingdeskruledone.Visible = false;
    //    pnlfillingdeskrulenotrequire.Visible = false;
    //    pnlfillingdeskruletryagain.Visible = true;

    //}
    //// End Wizard Auto Allocation Rule

    //// Document Flow Rule Rule
    //protected void CheckBoxList13_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (CheckBoxList13.SelectedValue == "1")
    //    {
    //        string te = "BusinessProcessRules.aspx";
    //        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    //        ModalPopupExtenderdocumentflowpopup.Show();

    //    }
    //    if (CheckBoxList13.SelectedValue == "2")
    //    {
    //        pnlfinish.Visible = true;
    //        pnldocumentflowruleyesno.Visible = false;
    //        pnldocumentflowruledone.Visible = false;
    //        pnldocumentflowrulenotrequire.Visible = true;
    //        pnldocumentflowruletryagain.Visible = false;

    //        //DataTable dt = fillstep1business();
    //        //if (dt.Rows.Count > 0)
    //        //{
    //        //    insertdate("DocumentFlowRule", "3");

    //        //    insertdate("SetupWizardDone", "1");


    //        //}
    //    }

    //}
    //protected void Button31_Click(object sender, EventArgs e)
    //{
    //    pnlfinish.Visible = true;
    //    pnldocumentflowruleyesno.Visible = false;
    //    pnldocumentflowruledone.Visible = true;
    //    pnldocumentflowrulenotrequire.Visible = false;
    //    pnldocumentflowruletryagain.Visible = false;

    //    //DataTable dt = fillstep1business();
    //    //if (dt.Rows.Count > 0)
    //    //{
    //    //    insertdate("DocumentFlowRule", "2");
    //    //    insertdate("SetupWizardDone", "1");

    //    //}
    //}
    //protected void Button32_Click(object sender, EventArgs e)
    //{
    //    pnlfinish.Visible = false;
    //    pnldocumentflowruleyesno.Visible = false;
    //    pnldocumentflowruledone.Visible = false;
    //    pnldocumentflowrulenotrequire.Visible = false;
    //    pnldocumentflowruletryagain.Visible = true;
    //}
    //protected void LinkButton14_Click(object sender, EventArgs e)
    //{
    //    string te = "BusinessProcessRules.aspx";
    //    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    //    ModalPopupExtenderdocumentflowpopup.Show();
    //}
    //// End Document Flow Rule Rule

    //protected DataTable fillstep1business()
    //{
    //    string str = "select * from Ifilesetupwizard where compid='" + Session["Comid"].ToString() + "' ";
    //    SqlCommand cmd = new SqlCommand(str, con);
    //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
    //    DataTable dt = new DataTable();
    //    adp.Fill(dt);
    //    if (dt.Rows.Count == 0)
    //    {
    //        string str1 = "Insert into Ifilesetupwizard (Business,Department,Designation,Employee,Cabinet,Drawer,Folder,Emailrule,FtpRule,FolderRule,FileStorageRule,FillingDeskrule,DocumentFlowRule,SetupWizardDone,compid) values ('1','1','1','1','1','1','1','1','1','1','1','1','1','0','" + Session["Comid"].ToString() + "') ";
    //        SqlCommand cmd1 = new SqlCommand(str1, con);
    //        con.Open();
    //        cmd1.ExecuteNonQuery();
    //        con.Close();
    //    }

    //    return dt;



    //}
    //public void insertdate(String filedname, string data)
    //{

    //    string str1 = "Update Ifilesetupwizard set " + filedname.ToString() + "='" + data + "' where compid='" + Session["Comid"].ToString() + "' ";
    //    SqlCommand cmd1 = new SqlCommand(str1, con);
    //    con.Open();
    //    cmd1.ExecuteNonQuery();
    //    con.Close();

    //}

    //protected void datacheck()
    //{
    //     DataTable dt = fillstep1business();
    //     if (dt.Rows.Count > 0)
    //     {
    //         // Business
    //         if (dt.Rows[0]["Business"].ToString()=="1")
    //         {
    //             pnldepartment.Visible = true;
    //             pnlyesno.Visible = true;
    //             pnldone.Visible = false;
    //             pnlnotrequire.Visible = false;
    //             pnltryagain.Visible = false;
                
                
    //         }
    //         if (dt.Rows[0]["Business"].ToString() == "2")
    //         {
    //             pnlyesno.Visible = false;
    //             pnldone.Visible = true;
    //             pnlnotrequire.Visible = false;
    //             pnltryagain.Visible = false;

    //         }
    //         if (dt.Rows[0]["Business"].ToString() == "3")
    //         {
    //             pnlyesno.Visible = false;
    //             pnldone.Visible = false;
    //             pnlnotrequire.Visible = true;
    //             pnltryagain.Visible = false;

    //         }
    //         // end business

    //         // Department
    //         if (dt.Rows[0]["Department"].ToString() == "1")
    //         {
    //            // pnldesignation.Visible = true; 
    //             pnldeptyesno.Visible = true;
    //             pnldeptdone.Visible = false;
    //             pnldeptnotrequire.Visible = false;
    //             pnldepttryagain.Visible = false;
    //         }
    //         if (dt.Rows[0]["Department"].ToString() == "2")
    //         {
    //             pnldepartment.Visible = true;
    //             pnldeptyesno.Visible = false;
    //             pnldeptdone.Visible = true;
    //             pnldeptnotrequire.Visible = false;
    //             pnldepttryagain.Visible = false;
    //         }
    //         if (dt.Rows[0]["Department"].ToString() == "3")
    //         {
    //             pnldepartment.Visible = true;
    //             pnldeptyesno.Visible = false;
    //             pnldeptdone.Visible = false;
    //             pnldeptnotrequire.Visible = true;
    //             pnldepttryagain.Visible = false;

    //         }
    //         // end Department

    //         //designation

    //         if (dt.Rows[0]["Designation"].ToString() == "1")
    //         {
    //           //  pnlemployee.Visible = true;
    //             pnldegnyesno.Visible = true;
    //             pnldegndone.Visible = false;
    //             pnldegnnotrequire.Visible = false;
    //             pnldegntryagain.Visible = false;
    //         }
    //         if (dt.Rows[0]["Designation"].ToString() == "2")
    //         {
    //             pnldesignation.Visible = true;
    //             pnldegnyesno.Visible = false;
    //             pnldegndone.Visible = true;
    //             pnldegnnotrequire.Visible = false;
    //             pnldegntryagain.Visible = false;
    //         }
    //         if (dt.Rows[0]["Designation"].ToString() == "3")
    //         {
    //             pnldesignation.Visible = true;
    //             pnldegnyesno.Visible = false;
    //             pnldegndone.Visible = false;
    //             pnldegnnotrequire.Visible = true;
    //             pnldegntryagain.Visible = false;

    //         }

    //         // end designation

    //         //Employee

    //         if (dt.Rows[0]["Employee"].ToString() == "1")
    //         {
    //          // pnlcabinet.Visible = true;
    //             pnlempyesno.Visible = true;
    //             pnlempdone.Visible = false;
    //             pnlempnotrequire.Visible = false;
    //             pnlemptryagain.Visible = false;

    //         }
    //         if (dt.Rows[0]["Employee"].ToString() == "2")
    //         {
    //             pnlemployee.Visible = true;
    //             pnlempyesno.Visible = false;
    //             pnlempdone.Visible = true;
    //             pnlempnotrequire.Visible = false;
    //             pnlemptryagain.Visible = false;

                
    //         }
    //         if (dt.Rows[0]["Employee"].ToString() == "3")
    //         {
    //             pnlemployee.Visible = true;
    //             pnlempyesno.Visible = false;
    //             pnlempdone.Visible = false;
    //             pnlempnotrequire.Visible = true;
    //             pnlemptryagain.Visible = false;

    //         }

    //         // end Employee

    //         // Cabinet

    //         if (dt.Rows[0]["Cabinet"].ToString() == "1")
    //         {
    //          //   pnldrawer.Visible = true;
    //             pnlcabinetyesno.Visible = true;
    //             pnlcabinetdone.Visible = false;
    //             pnlcabinetnotrequire.Visible = false;
    //             pnlcabinettryagain.Visible = false;


    //         }
    //         if (dt.Rows[0]["Cabinet"].ToString() == "2")
    //         {
    //             pnlcabinet.Visible = true;
    //             pnlcabinetyesno.Visible = false;
    //             pnlcabinetdone.Visible = true;
    //             pnlcabinetnotrequire.Visible = false;
    //             pnlcabinettryagain.Visible = false;



    //         }
    //         if (dt.Rows[0]["Cabinet"].ToString() == "3")
    //         {
    //             pnlcabinet.Visible = true;
    //             pnlcabinetyesno.Visible = false;
    //             pnlcabinetdone.Visible = false;
    //             pnlcabinetnotrequire.Visible = true;
    //             pnlcabinettryagain.Visible = false;


    //         }

    //         // End Cabinet

    //         // Drawer

    //         if (dt.Rows[0]["Drawer"].ToString() == "1")
    //         {
    //            //  pnlfolder.Visible = true;
    //             pnldraweryesno.Visible = true;
    //             pnldrawerdone.Visible = false;
    //             pnldrawernotrequire.Visible = false;
    //             pnldrawertryagain.Visible = false;

    //         }
    //         if (dt.Rows[0]["Drawer"].ToString() == "2")
    //         {
    //             pnldrawer.Visible = true;
    //             pnldraweryesno.Visible = false;
    //             pnldrawerdone.Visible = true;
    //             pnldrawernotrequire.Visible = false;
    //             pnldrawertryagain.Visible = false;

    //         }
    //         if (dt.Rows[0]["Drawer"].ToString() == "3")
    //         {
    //             pnldrawer.Visible = true;
    //             pnldraweryesno.Visible = false;
    //             pnldrawerdone.Visible = false;
    //             pnldrawernotrequire.Visible = true;
    //             pnldrawertryagain.Visible = false;
                
    //         }

    //         // End Drawer

    //         // Folder

    //         if (dt.Rows[0]["Folder"].ToString() == "1")
    //         {
    //            //  pnlemailrule.Visible = true;
    //             pnlfolderyesno.Visible = true;
    //             pnlfolderdone.Visible = false;
    //             pnlfoldernotrequire.Visible = false;
    //             pnlfoldertryagain.Visible = false;
                 
    //         }
    //         if (dt.Rows[0]["Folder"].ToString() == "2")
    //         {
    //             pnlfolder.Visible = true;
    //             pnlfolderyesno.Visible = false;
    //             pnlfolderdone.Visible = true;
    //             pnlfoldernotrequire.Visible = false;
    //             pnlfoldertryagain.Visible = false;
    //         }
    //         if (dt.Rows[0]["Folder"].ToString() == "3")
    //         {
    //             pnlfolder.Visible = true;
    //             pnlfolderyesno.Visible = false;
    //             pnlfolderdone.Visible = false;
    //             pnlfoldernotrequire.Visible = true;
    //             pnlfoldertryagain.Visible = false;
    //         }

    //         // End Folder

    //         // Emailrule

    //         if (dt.Rows[0]["Emailrule"].ToString() == "1")
    //         {
    //            //  pnlftprule.Visible = true;
    //             pnlemailruleyesno.Visible = true;
    //             pnlemailruledone.Visible = false;
    //             pnlemailrulenotrequire.Visible = false;
    //             pnlemailruletryagain.Visible = false;

    //         }
    //         if (dt.Rows[0]["Emailrule"].ToString() == "2")
    //         {
    //             pnlemailrule.Visible = true;
    //             pnlemailruleyesno.Visible = false;
    //             pnlemailruledone.Visible = true;
    //             pnlemailrulenotrequire.Visible = false;
    //             pnlemailruletryagain.Visible = false;
    //         }
    //         if (dt.Rows[0]["Emailrule"].ToString() == "3")
    //         {
    //             pnlemailrule.Visible = true;
    //             pnlemailruleyesno.Visible = false;
    //             pnlemailruledone.Visible = false;
    //             pnlemailrulenotrequire.Visible = true;
    //             pnlemailruletryagain.Visible = false;
    //         }

    //         // End Emailrule

    //         // FtpRule

    //         if (dt.Rows[0]["FtpRule"].ToString() == "1")
    //         {
    //           //  pnlfolderrule.Visible = true;
    //             pnlftpruleyesno.Visible = true;
    //             pnlftpruledone.Visible = false;
    //             pnlftprulenotrequire.Visible = false;
    //             pnlftpruletryagain.Visible = false;

    //         }
    //         if (dt.Rows[0]["FtpRule"].ToString() == "2")
    //         {
    //             pnlftprule.Visible = true;
    //             pnlftpruleyesno.Visible = false;
    //             pnlftpruledone.Visible = true;
    //             pnlftprulenotrequire.Visible = false;
    //             pnlftpruletryagain.Visible = false;

    //         }
    //         if (dt.Rows[0]["FtpRule"].ToString() == "3")
    //         {
    //             pnlftprule.Visible = true;
    //             pnlftpruleyesno.Visible = false;
    //             pnlftpruledone.Visible = false;
    //             pnlftprulenotrequire.Visible = true;
    //             pnlftpruletryagain.Visible = false;

    //         }

    //         // End FtpRule

    //         // FolderRule

    //         if (dt.Rows[0]["FolderRule"].ToString() == "1")
    //         {
    //          //    pnlfilestoragerule.Visible = true;
    //             pnlfolderruleyesno.Visible = true;
    //             pnlfolderruledone.Visible = false;
    //             pnlfolderrulenotrequire.Visible = false;
    //             pnlfolderruletryagain.Visible = false;
    //         }
    //         if (dt.Rows[0]["FolderRule"].ToString() == "2")
    //         {
    //             pnlfolderrule.Visible = true;
    //             pnlfolderruleyesno.Visible = false;
    //             pnlfolderruledone.Visible = true;
    //             pnlfolderrulenotrequire.Visible = false;
    //             pnlfolderruletryagain.Visible = false;
    //         }
    //         if (dt.Rows[0]["FolderRule"].ToString() == "3")
    //         {
    //             pnlfolderrule.Visible = true;
    //             pnlfolderruleyesno.Visible = false;
    //             pnlfolderruledone.Visible = false;
    //             pnlfolderrulenotrequire.Visible = true;
    //             pnlfolderruletryagain.Visible = false;
    //         }

    //         // End FolderRule

    //         // FileStorageRule

    //         if (dt.Rows[0]["FileStorageRule"].ToString() == "1")
    //         {
    //           //   pnlfillingdeskrule.Visible = true;
    //             pnlfilestorageruleyesno.Visible = true;
    //             pnlfilestorageruledone.Visible = false;
    //             pnlfilestoragerulenotrequire.Visible = false;
    //             pnlfilestorageruletryagain.Visible = false;
    //         }
    //         if (dt.Rows[0]["FileStorageRule"].ToString() == "2")
    //         {
    //             pnlfilestoragerule.Visible = true;
    //             pnlfilestorageruleyesno.Visible = false;
    //             pnlfilestorageruledone.Visible = true;
    //             pnlfilestoragerulenotrequire.Visible = false;
    //             pnlfilestorageruletryagain.Visible = false;
    //         }
    //         if (dt.Rows[0]["FileStorageRule"].ToString() == "3")
    //         {
    //             pnlfilestoragerule.Visible = true;
    //             pnlfilestorageruleyesno.Visible = false;
    //             pnlfilestorageruledone.Visible = false;
    //             pnlfilestoragerulenotrequire.Visible = true;
    //             pnlfilestorageruletryagain.Visible = false;
    //         }

    //         // End FileStorageRule

    //         // FillingDeskrule

    //         if (dt.Rows[0]["FillingDeskrule"].ToString() == "1")
    //         {
    //           //   pnldocumentflowrule.Visible = true;
    //             pnlfillingdeskruleyesno.Visible = true;
    //             pnlfillingdeskruledone.Visible = false;
    //             pnlfillingdeskrulenotrequire.Visible = false;
    //             pnlfillingdeskruletryagain.Visible = false;
    //         }
    //         if (dt.Rows[0]["FillingDeskrule"].ToString() == "2")
    //         {
    //             pnlfillingdeskrule.Visible = true;
    //             pnlfillingdeskruleyesno.Visible = false;
    //             pnlfillingdeskruledone.Visible = true;
    //             pnlfillingdeskrulenotrequire.Visible = false;
    //             pnlfillingdeskruletryagain.Visible = false;
    //         }
    //         if (dt.Rows[0]["FillingDeskrule"].ToString() == "3")
    //         {
    //             pnlfillingdeskrule.Visible = true;
    //             pnlfillingdeskruleyesno.Visible = false;
    //             pnlfillingdeskruledone.Visible = false;
    //             pnlfillingdeskrulenotrequire.Visible = true;
    //             pnlfillingdeskruletryagain.Visible = false;
    //         }

    //         // End FillingDeskrule

    //         // DocumentFlowRule

    //         if (dt.Rows[0]["DocumentFlowRule"].ToString() == "1")
    //         {
               
    //             pnldocumentflowruleyesno.Visible = true;
    //             pnldocumentflowruledone.Visible = false;
    //             pnldocumentflowrulenotrequire.Visible = false;
    //             pnldocumentflowruletryagain.Visible = false;

    //         }
    //         if (dt.Rows[0]["DocumentFlowRule"].ToString() == "2")
    //         {
    //             pnldocumentflowrule.Visible = true;
    //             pnldocumentflowruleyesno.Visible = false;
    //             pnldocumentflowruledone.Visible = true;
    //             pnldocumentflowrulenotrequire.Visible = false;
    //             pnldocumentflowruletryagain.Visible = false;

    //         }
    //         if (dt.Rows[0]["DocumentFlowRule"].ToString() == "3")
    //         {
    //             pnldocumentflowrule.Visible = true;
    //             pnldocumentflowruleyesno.Visible = false;
    //             pnldocumentflowruledone.Visible = false;
    //             pnldocumentflowrulenotrequire.Visible = true;
    //             pnldocumentflowruletryagain.Visible = false;

    //         }

    //         // End DocumentFlowRule
    //     }
    //}
    //protected void othercheck()
    //{
    //     DataTable dt = fillstep1business();
    //     if (dt.Rows.Count > 0)
    //     {

    //         if ((dt.Rows[0]["Department"].ToString() == "2" || dt.Rows[0]["Department"].ToString() == "3") && dt.Rows[0]["Designation"].ToString() == "1")
    //         {
    //             pnldesignation.Visible = true;
                 
    //         }
    //         if ((dt.Rows[0]["Designation"].ToString() == "2" || dt.Rows[0]["Designation"].ToString() == "3") && dt.Rows[0]["Employee"].ToString() == "1")
    //         {
    //             pnlemployee.Visible = true;
    //         }
    //         if ((dt.Rows[0]["Employee"].ToString() == "2" || dt.Rows[0]["Employee"].ToString() == "3") && dt.Rows[0]["Cabinet"].ToString() == "1")
    //         {
    //             pnlcabinet.Visible = true;
    //         }
    //         if ((dt.Rows[0]["Cabinet"].ToString() == "2" || dt.Rows[0]["Cabinet"].ToString() == "3") && dt.Rows[0]["Drawer"].ToString() == "1")
    //         {
    //             pnldrawer.Visible = true;
    //         }
    //         if ((dt.Rows[0]["Drawer"].ToString() == "2" || dt.Rows[0]["Drawer"].ToString() == "3") && dt.Rows[0]["Folder"].ToString() == "1")
    //         {
    //             pnlfolder.Visible = true;
    //         }

    //         if ((dt.Rows[0]["Folder"].ToString() == "2" || dt.Rows[0]["Folder"].ToString() == "3") && dt.Rows[0]["Emailrule"].ToString() == "1")
    //         {
    //             pnlemailrule.Visible = true;
    //         }
    //         if ((dt.Rows[0]["Emailrule"].ToString() == "2" || dt.Rows[0]["Emailrule"].ToString() == "3") && dt.Rows[0]["FtpRule"].ToString() == "1")
    //         {
    //             pnlftprule.Visible = true;
    //         }
    //         if ((dt.Rows[0]["FtpRule"].ToString() == "2" || dt.Rows[0]["FtpRule"].ToString() == "3") && dt.Rows[0]["FolderRule"].ToString() == "1")
    //         {
    //             pnlfolderrule.Visible = true;
    //         }
    //         if ((dt.Rows[0]["FolderRule"].ToString() == "2" || dt.Rows[0]["FolderRule"].ToString() == "3") && dt.Rows[0]["FileStorageRule"].ToString() == "1")
    //         {
    //             pnlfilestoragerule.Visible = true;
    //         }

    //         if ((dt.Rows[0]["FileStorageRule"].ToString() == "2" || dt.Rows[0]["FileStorageRule"].ToString() == "3") && dt.Rows[0]["FillingDeskrule"].ToString() == "1")
    //         {
    //             pnlfillingdeskrule.Visible = true;
    //         }
    //         if ((dt.Rows[0]["FillingDeskrule"].ToString() == "2" || dt.Rows[0]["FillingDeskrule"].ToString() == "3") && dt.Rows[0]["DocumentFlowRule"].ToString() == "1")
    //         {
    //             pnldocumentflowrule.Visible = true;
    //         }
    //         if ((dt.Rows[0]["DocumentFlowRule"].ToString() == "2" || dt.Rows[0]["DocumentFlowRule"].ToString() == "3") )
    //         {
    //             pnlfinish.Visible = true;
    //         }
             
    //     }
    //}

    protected void departments(string whid)
    {
        string str = "select * from DepartmentmasterMNC where Companyid='" + Session["Comid"].ToString() + "' and Whid='" + whid + "' order by Departmentname ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);


        griddocmaintype.DataSource = dt;
        griddocmaintype.DataBind();
        
    }
    protected void designation(string whid)
    {
        string str = "select DepartmentmasterMNC.Departmentname,DesignationMaster.DesignationName from DepartmentmasterMNC inner join DesignationMaster on DesignationMaster.DeptID=DepartmentmasterMNC.id where Companyid='" + Session["Comid"].ToString() + "' and Whid='" + whid + "' order by Departmentname,DesignationName ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);


        GridView1.DataSource = dt;
        GridView1.DataBind();

    }

    protected void Button34_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmafterloginforsuper.aspx");
    }
    protected void fillaccountyearofbusiness(string whid)
    {
        DataTable dtda = new DataTable();

        dtda = (DataTable)select("select Report_Period_Id,Convert(nvarchar,StartDate,101) as StartDate, Convert(nvarchar,EndDate,101) as EndDate from [ReportPeriod] where Compid = '" + Session["comid"] + "' and Whid='" + whid + "' and Active='1' ");
       
        Label37.Text = Convert.ToDateTime(dtda.Rows[0]["StartDate"]).ToShortDateString().ToString();
        Label38.Text= Convert.ToDateTime(dtda.Rows[0]["EndDate"]).ToShortDateString().ToString();

    }
    protected DataTable select(string str)
    {
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);

        return dt;

    }
}
