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
using System.Net.Security;
using System.Diagnostics;

using System.Text.RegularExpressions;
using System.Net;
using System.Web.Configuration;
using System.Text;
using System.Collections.Generic;
using System.IO;

public partial class WorkAllocationForEmployee : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    string mainid = "";
       SqlConnection conioffce;
   
      
    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        conioffce = pgcon.dynconn;
        lblVersion.Text = "This is V2 Updated on 1Aprl2016 by ";
        if (!IsPostBack)
        {
           
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            // FillProduct();

            if (Request.QueryString["Id"] != null)
            {
                Session["mainidq"] = Request.QueryString["Id"].ToString();
                mainid = Request.QueryString["Id"].ToString();

                ddlworktitle.SelectedIndex = ddlworktitle.Items.IndexOf(ddlworktitle.Items.FindByValue(mainid));

                ddlworktitle_SelectedIndexChanged(sender, e);


                string strcount = "Select * From MyDailyWorkReport where Id='" + mainid + "'";

                SqlCommand cmdcount = new SqlCommand(strcount, con);
                DataTable dtcount = new DataTable();
                SqlDataAdapter adpcount = new SqlDataAdapter(cmdcount);
                adpcount.Fill(dtcount);
                if (dtcount.Rows.Count > 0)
                {
                    string file1 = "";

                    try
                    {
                        if (DropDownList1.SelectedValue == "1")
                        {
                            file1 = "+'- Main Hr :'+  PageWorkTbl.BudgetedHourDevelopment ";
                        }
                        if (DropDownList1.SelectedValue == "2")
                        {
                            file1 = "+'- Main Hr :'+ PageWorkTbl.BudgetedHourTesting ";
                        }
                        if (DropDownList1.SelectedValue == "3")
                        {
                            file1 = "+'- Main Hr :'+ PageWorkTbl.BudgetedHourSupervisorChecking ";
                        }
                        string strcln = " select PageMaster.PageName +'-'+PageVersionTbl.VersionNo +'-'+ PageWorkTbl.WorkRequirementTitle " + file1 + " as WorkTitle, PageWorkTbl.*  from PageWorkTbl inner join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId  inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId inner join MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId  inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId inner join VersionInfoMaster    on VersionInfoMaster.VersionInfoId = WebsiteMaster.VersionInfoId  inner join ProductMaster   on VersionInfoMaster.ProductId=ProductMaster.ProductId where   PageMaster.PageName !='' and Employeeid="+ dtcount.Rows[0]["EmployeeId"].ToString() +"";
                        SqlCommand cmdcln = new SqlCommand(strcln, con);
                        DataTable dtcln = new DataTable();
                        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
                        adpcln.Fill(dtcln);

                        ddlworktitle.DataSource = dtcln;
                        ddlworktitle.DataValueField = "Id";
                        ddlworktitle.DataTextField = "WorkTitle";
                        ddlworktitle.DataBind();

                        ddlworktitle.Items.Insert(0, "-Select-");
                        ddlworktitle.Items[0].Value = "0";


                        ddlworktitle.SelectedValue = dtcount.Rows[0]["PageWorkTblId"].ToString();

                    }
                    catch (Exception ex)
                    {
                    }
                    fillemployee();
                    ddlemployee.SelectedValue = dtcount.Rows[0]["EmployeeId"].ToString();
                    costofdeveloper();
                    filltodayhour();

                   // Session["EmpSelect"] = ddlemployee.SelectedIndex;
                    txttodaybudgetdhour.Text = dtcount.Rows[0]["budgetedhour"].ToString();
                    txttodaybudgetdhour_TextChanged(sender, e);
                    txttargetdatedeve.Text = dtcount.Rows[0]["workallocationdate"].ToString();
                    txtworktobedone.Text = dtcount.Rows[0]["worktobedone"].ToString();

                    DropDownList1.SelectedValue = dtcount.Rows[0]["Typeofwork"].ToString();
                    DropDownList1_SelectedIndexChanged(sender, e);

                    ddlemployee.SelectedValue = dtcount.Rows[0]["Employeeid"].ToString();
                    txtincentive.Text = dtcount.Rows[0]["Offer_Amount"].ToString();
                    ddl_priority.SelectedValue = dtcount.Rows[0]["Priority"].ToString();
                    FillProduct();

                    string strcln12 = " SELECT  distinct   MasterPageMaster.MasterPageId   FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId inner join WebsiteMaster on WebsiteMaster.VersionInfoId=VersionInfoMaster.VersionInfoId inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.ID inner join MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId where ProductMaster.ClientMasterId='" + Session["ClientId"] + "' and ProductMaster.ProductId='" + dtcount.Rows[0]["ProductId"].ToString() + "' and VersionInfoMaster.Active ='True' and ProductDetail.Active='1'  ";
                    SqlCommand cmdcln12 = new SqlCommand(strcln12, con);
                    DataTable dtcln12 = new DataTable();
                    SqlDataAdapter adpcln12 = new SqlDataAdapter(cmdcln12);
                    adpcln12.Fill(dtcln12);

                    if (dtcln12.Rows.Count > 0)
                    {
                        Session["MasterPageId"] = dtcln12.Rows[0]["MasterPageId"].ToString();
                    }
                    else
                    {
                        Session["MasterPageId"] = "0";
                    }
                    string asdfg = Convert.ToString(Session["MasterPageId"]);
                    ddlproductname.SelectedValue = asdfg;
                  //  ddlproductname_SelectedIndexChanged(sender, e);
                    filltype();
                    ddlpage.SelectedValue = dtcount.Rows[0]["PageId"].ToString();
                   // ddlpage_SelectedIndexChanged(sender, e);
                    /* MyDailyWorkReport(PageWorkTblId,
                     * EmployeeId,budgetedhour,workallocationdate,worktobedone,Typeofwork,PageId,ProductId, Offer_Amount, Priority) values
                     * ('" + ddlworktitle.SelectedValue + "','" + ddlemployee.SelectedValue + "','" + txttodaybudgetdhour.Text + "',
                     * '" + txttargetdatedeve.Text + "','" + txtworktobedone.Text + "','" + DropDownList1.SelectedValue + "','" + ddlpage.SelectedValue + "','" + Session["ProductId"].ToString() + "', '" + txtincentive.Text + "' , '" + ddl_priority.SelectedItem.Text + "')";*/
                    ddlworktitle.SelectedValue = dtcount.Rows[0]["PageWorkTblId"].ToString();
                  //  ddlworktitle_SelectedIndexChanged(sender, e);


                    Button1.Text = "Update"; 
                    string str1231 = " Select  MyworktblId, WorkRequirementPdfFilename as  PDFURL, WorkRequirementAudioFileName as AudioURL, FileTitle as  Title, Date  From DailyWorkGuideUploadTbl where MyworktblId='" + mainid + "'";

                    SqlCommand cmd1231 = new SqlCommand(str1231, con);
                    DataTable dt1231 = new DataTable();
                    SqlDataAdapter adp123 = new SqlDataAdapter(cmd1231);
                    adp123.Fill(dt1231);
                    Session["GridFileAttach1"]=dt1231;


                    if (dt1231.Rows.Count > 0)
                    {
                        gridFileAttach.DataSource = dt1231;
                        gridFileAttach.DataBind();
                        pnlup.Visible = true;
                        chkupload.Checked =true;

                    }
                    else
                    {
                        pnlup.Visible = false;
                    //    Label7.Visible = false;
                        //chkupload.Visible = false;
                    }
                }


            }

            else
            {
                FillProduct();
                fillproductid();
                filltype();

               txttargetdatedeve.Text = System.DateTime.Now.ToShortDateString();
                TextBox1.Text = System.DateTime.Now.ToShortDateString();
                TextBox2.Text = "Planning " + TextBox1.Text;
                txttitlename.Text = TextBox2.Text; 
                DateTime dto = DateTime.Now;
                dto = dto.AddDays(1);
                txttargetdatedeve.Text = Convert.ToString(dto.ToShortDateString());



                DDLwarehous();
                fillddldepartment();

                fillemployee();



                if (Request.QueryString["Id"] != null && Request.QueryString["EmpDevid"] != null)
                {

                    string empid = Request.QueryString["EmpDevid"].ToString();
                    ddlemployee.SelectedIndex = ddlemployee.Items.IndexOf(ddlemployee.Items.FindByValue(empid));
                    DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByValue("1"));
                }
                if (Request.QueryString["Id"] != null && Request.QueryString["EmpTestid"] != null)
                {
                    string empid = Request.QueryString["EmpTestid"].ToString();
                    ddlemployee.SelectedIndex = ddlemployee.Items.IndexOf(ddlemployee.Items.FindByValue(empid));
                    DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByValue("1"));
                }
                if (Request.QueryString["Id"] != null && Request.QueryString["EmpSupid"] != null)
                {
                    string empid = Request.QueryString["EmpSupid"].ToString();
                    string masid = Request.QueryString["ID"].ToString();
                    ddlemployee.SelectedIndex = ddlemployee.Items.IndexOf(ddlemployee.Items.FindByValue(empid));
                    DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByValue("1"));
                }
                costofdeveloper();
                fillpagename();
                fillmasterhour();
                filltodayhour();
                                //
                if (Session["EmpSelect"] != "")
                {
                    ddlemployee.SelectedIndex = Convert.ToInt32(Session["EmpSelect"]);
                }
                if (Session["EmpType"] != "")
                {
                    DropDownList1.SelectedIndex = Convert.ToInt32(Session["EmpType"]);
                }

                if (Session["ProductSel"] != "")
                {
                    ddlproductname.SelectedIndex = Convert.ToInt32(Session["ProductSel"]);
                    ddlproductname_SelectedIndexChanged(sender ,e);
                }


               
               
                DDLwarehousFilter();
                fillddldepartmentFilter();
                addnewpanel_Click1(sender, e);
        }
       

            
        }
    }
    //DDL
    public void DDLwarehous()
    {
        string finalstr = " SELECT DISTINCT  dbo.WareHouseMaster.Name, dbo.WareHouseMaster.WareHouseId, dbo.WareHouseMaster.comid FROM dbo.DepartmentmasterMNC INNER JOIN dbo.WareHouseMaster ON dbo.WareHouseMaster.WareHouseId = dbo.DepartmentmasterMNC.Whid WHERE (dbo.WareHouseMaster.Status = 1) AND (dbo.DepartmentmasterMNC.Active = 1) AND dbo.WareHouseMaster.comid='" + Session["comid"] + "' ORDER BY dbo.WareHouseMaster.Name ";
        SqlCommand cmdcln = new SqlCommand(finalstr, conioffce);
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        DataTable dtcln = new DataTable();
        adpcln.Fill(dtcln);
        ddlstore.DataSource = dtcln;
        ddlstore.DataValueField = "WareHouseId";
        ddlstore.DataTextField = "Name";
        ddlstore.DataBind();
        ddlstore.Items.Insert(0, "--Select--");
        ddlstore.Items[0].Value = "0";

    }
    protected void ddlstore_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillddldepartment();
        fillemployee();
    }
    public void fillddldepartment()
    {
        string strfillgrid1 = "SELECT Departmentname as Departmentname,ID from  DepartmentmasterMNC inner join WareHouseMaster on WareHouseMaster.WareHouseId=DepartmentmasterMNC.Whid where Companyid='" + Session["comid"] + "' and DepartmentmasterMNC.Whid='" + ddlstore.SelectedValue + "' and  [WareHouseMaster].status = '1' order by Departmentname";
        SqlCommand cmdfillgrid1 = new SqlCommand(strfillgrid1, conioffce);
        SqlDataAdapter adpfillgrid1 = new SqlDataAdapter(cmdfillgrid1);
        DataTable dtfill1 = new DataTable();
        adpfillgrid1.Fill(dtfill1);
        ddldesignation.DataSource = dtfill1;
        ddldesignation.DataValueField = "ID";
        ddldesignation.DataTextField = "Departmentname";
        ddldesignation.DataBind();
        ddldesignation.Items.Insert(0, "-Select-");
        ddldesignation.SelectedItem.Value = "0";
    }
    protected void ddldesignation_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillemployee();
    }
    protected void fillemployee()
    {
        //string empid = "";
        //string strcon = "";
        //string depart = "";
        //string strdepa = "";

        //string allemp = "";
        //string strall = "";
        //if (ddldesignation.SelectedIndex > 0)
        //{

        //    string finalstr = " SELECT dbo.Syncr_LicenseEmployee_With_JobcenterId.License_Emp_id, dbo.DepartmentmasterMNC.id FROM  dbo.EmployeeMaster INNER JOIN dbo.Syncr_LicenseEmployee_With_JobcenterId ON  dbo.EmployeeMaster.EmployeeMasterID = dbo.Syncr_LicenseEmployee_With_JobcenterId.Jobcenter_Emp_id INNER JOIN dbo.DepartmentmasterMNC ON dbo.EmployeeMaster.DeptID = dbo.DepartmentmasterMNC.id  Where   dbo.DepartmentmasterMNC.id='" + ddldesignation.SelectedValue + "' and EmployeeMaster.Active=1 ";
        //    SqlCommand cmdclnn = new SqlCommand(finalstr, conioffce);
        //    SqlDataAdapter adpclnn = new SqlDataAdapter(cmdclnn);
        //    DataTable dtclnn = new DataTable();
        //    adpclnn.Fill(dtclnn);
        //    if (dtclnn.Rows.Count > 0)
        //    {
        //        foreach (DataRow dts in dtclnn.Rows)
        //        {
        //            depart += "'" + dts["License_Emp_id"] + "',";
        //        }
        //        if (depart.Length > 0)
        //        {
        //            depart = depart.Remove(depart.Length - 1);
        //            strdepa = " And Id IN (" + depart + ") ";
        //        }
        //    }
        //}
        //if (ddlstore.SelectedIndex > 0)
        //{

        //    string finalstr = " SELECT dbo.Syncr_LicenseEmployee_With_JobcenterId.License_Emp_id, dbo.EmployeeMaster.Whid  FROM  dbo.EmployeeMaster INNER JOIN dbo.Syncr_LicenseEmployee_With_JobcenterId ON  dbo.EmployeeMaster.EmployeeMasterID = dbo.Syncr_LicenseEmployee_With_JobcenterId.Jobcenter_Emp_id INNER JOIN dbo.DepartmentmasterMNC ON dbo.EmployeeMaster.DeptID = dbo.DepartmentmasterMNC.id  Where  dbo.EmployeeMaster.Whid='" + ddlstore.SelectedValue + "' and EmployeeMaster.Active=1 ";
        //    SqlCommand cmdclnn = new SqlCommand(finalstr, conioffce);
        //    SqlDataAdapter adpclnn = new SqlDataAdapter(cmdclnn);
        //    DataTable dtclnn = new DataTable();
        //    adpclnn.Fill(dtclnn);
        //    if (dtclnn.Rows.Count > 0)
        //    {
        //        foreach (DataRow dts in dtclnn.Rows)
        //        {
        //            empid += "'" + dts["License_Emp_id"] + "',";
        //        }
        //        if (empid.Length > 0)
        //        {
        //            empid = empid.Remove(empid.Length - 1);
        //            strcon = " And Id IN (" + empid + ") ";
        //        }
        //    }
        //}
        //else
        //{
        //    string finalstr = " SELECT dbo.Syncr_LicenseEmployee_With_JobcenterId.License_Emp_id, dbo.DepartmentmasterMNC.id FROM  dbo.EmployeeMaster INNER JOIN dbo.Syncr_LicenseEmployee_With_JobcenterId ON  dbo.EmployeeMaster.EmployeeMasterID = dbo.Syncr_LicenseEmployee_With_JobcenterId.Jobcenter_Emp_id INNER JOIN dbo.DepartmentmasterMNC ON dbo.EmployeeMaster.DeptID = dbo.DepartmentmasterMNC.id  Where   EmployeeMaster.Active=1 ";
        //    SqlCommand cmdclnn = new SqlCommand(finalstr, conioffce);
        //    SqlDataAdapter adpclnn = new SqlDataAdapter(cmdclnn);
        //    DataTable dtclnn = new DataTable();
        //    adpclnn.Fill(dtclnn);
        //    if (dtclnn.Rows.Count > 0)
        //    {
        //        foreach (DataRow dts in dtclnn.Rows)
        //        {
        //            allemp += "'" + dts["License_Emp_id"] + "',";
        //        }
        //        if (allemp.Length > 0)
        //        {
        //            allemp = allemp.Remove(allemp.Length - 1);
        //            strall = " And Id IN (" + allemp + ") ";
        //        }
        //    }
        //}
        string strwhid = "";
        if (ddlstore.SelectedIndex > 0)
        {
            strwhid += " and dbo.EmployeeMaster.Whid='" + ddlstore.SelectedValue + "' ";
        }
        if (ddldesignation.SelectedIndex > 0)
        {
            strwhid += " and dbo.EmployeeMaster.DeptID='" + ddldesignation.SelectedValue + "' ";
        }

        //SELECT dbo.Syncr_LicenseEmployee_With_JobcenterId.License_Emp_id, dbo.EmployeeMaster.EmployeeName, dbo.EmployeeMaster.DeptID, dbo.EmployeeMaster.Whid FROM dbo.EmployeeMaster INNER JOIN dbo.Syncr_LicenseEmployee_With_JobcenterId ON  dbo.EmployeeMaster.EmployeeMasterID = dbo.Syncr_LicenseEmployee_With_JobcenterId.Jobcenter_Emp_id WHERE (dbo.EmployeeMaster.Active = 1)
        //string strcln = " SELECT * from EmployeeMaster where ClientId='" + Session["ClientId"] + "'" + strall + " " + strcon + "" + strdepa + " order by Name  ";
        string strcln = " SELECT  dbo.Syncr_LicenseEmployee_With_JobcenterId.License_Emp_id, dbo.EmployeeMaster.EmployeeName, dbo.EmployeeMaster.Whid ,  dbo.EmployeeMaster.DeptID FROM  dbo.EmployeeMaster INNER JOIN dbo.Syncr_LicenseEmployee_With_JobcenterId ON  dbo.EmployeeMaster.EmployeeMasterID = dbo.Syncr_LicenseEmployee_With_JobcenterId.Jobcenter_Emp_id INNER JOIN dbo.DepartmentmasterMNC ON dbo.EmployeeMaster.DeptID = dbo.DepartmentmasterMNC.id  Where   EmployeeMaster.Active=1 " + strwhid + " ";
        SqlCommand cmdcln = new SqlCommand(strcln, conioffce);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);

        //string strcln = " SELECT * from EmployeeMaster where ClientId='" + Session["ClientId"] + "' " + strall + " " + strcon + "" + strdepa + " order by Name  ";
        //SqlCommand cmdcln = new SqlCommand(strcln, con);
        //DataTable dtcln = new DataTable();
        //SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        //adpcln.Fill(dtcln);

        ddlemployee.DataSource = dtcln;
        ddlemployee.DataValueField = "License_Emp_id";
        ddlemployee.DataTextField = "EmployeeName";
        ddlemployee.DataBind();

    }
    protected void ddlemployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        costofdeveloper();
        fillpagename();
        filltodayhour();

        Session["EmpSelect"] = ddlemployee.SelectedIndex;
    }


    //-----Filter------------------
    public void DDLwarehousFilter()
    {
        string finalstr = " SELECT DISTINCT  dbo.WareHouseMaster.Name, dbo.WareHouseMaster.WareHouseId, dbo.WareHouseMaster.comid FROM dbo.DepartmentmasterMNC INNER JOIN dbo.WareHouseMaster ON dbo.WareHouseMaster.WareHouseId = dbo.DepartmentmasterMNC.Whid WHERE (dbo.WareHouseMaster.Status = 1) AND (dbo.DepartmentmasterMNC.Active = 1) AND dbo.WareHouseMaster.comid='" + Session["comid"] + "' ORDER BY dbo.WareHouseMaster.Name ";
        SqlCommand cmdcln = new SqlCommand(finalstr, conioffce);
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        DataTable dtcln = new DataTable();
        adpcln.Fill(dtcln);
        ddlbusinessfilter.DataSource = dtcln;
        ddlbusinessfilter.DataValueField = "WareHouseId";
        ddlbusinessfilter.DataTextField = "Name";
        ddlbusinessfilter.DataBind();
        ddlbusinessfilter.Items.Insert(0, "--Select--");
        ddlbusinessfilter.Items[0].Value = "0";
    }
    protected void ddlbusinessfilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillddldepartmentFilter();
        EmployeeFilter();
    }
    public void fillddldepartmentFilter()
    {
        string strfillgrid1 = "SELECT Departmentname as Departmentname,ID from  DepartmentmasterMNC inner join WareHouseMaster on WareHouseMaster.WareHouseId=DepartmentmasterMNC.Whid where Companyid='" + Session["comid"] + "' and DepartmentmasterMNC.Whid='" + ddlbusinessfilter.SelectedValue + "' and  [WareHouseMaster].status = '1' order by Departmentname";
        SqlCommand cmdfillgrid1 = new SqlCommand(strfillgrid1, conioffce);
        SqlDataAdapter adpfillgrid1 = new SqlDataAdapter(cmdfillgrid1);
        DataTable dtfill1 = new DataTable();
        adpfillgrid1.Fill(dtfill1);
        ddldepartmentfilter.DataSource = dtfill1;
        ddldepartmentfilter.DataValueField = "ID";
        ddldepartmentfilter.DataTextField = "Departmentname";
        ddldepartmentfilter.DataBind();
        ddldepartmentfilter.Items.Insert(0, "-Select-");
        ddldepartmentfilter.SelectedItem.Value = "0";
    }
    protected void ddldepartmentfilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        EmployeeFilter();
    }
    protected void EmployeeFilter()
    {
        //string empid = "";
        //string strcon = "";
        //string depart = "";
        //string strdepa = "";
        //string allemp = "";
        //string strall = "";
        //if (ddldepartmentfilter.SelectedIndex > 0)
        //{
        //    string finalstr = " SELECT dbo.Syncr_LicenseEmployee_With_JobcenterId.License_Emp_id, dbo.DepartmentmasterMNC.id FROM  dbo.EmployeeMaster INNER JOIN dbo.Syncr_LicenseEmployee_With_JobcenterId ON  dbo.EmployeeMaster.EmployeeMasterID = dbo.Syncr_LicenseEmployee_With_JobcenterId.Jobcenter_Emp_id INNER JOIN dbo.DepartmentmasterMNC ON dbo.EmployeeMaster.DeptID = dbo.DepartmentmasterMNC.id  Where   dbo.DepartmentmasterMNC.id='" + ddldepartmentfilter.SelectedValue + "'";
        //    SqlCommand cmdclnn = new SqlCommand(finalstr, conioffce);
        //    SqlDataAdapter adpclnn = new SqlDataAdapter(cmdclnn);
        //    DataTable dtclnn = new DataTable();
        //    adpclnn.Fill(dtclnn);
        //    if (dtclnn.Rows.Count > 0)
        //    {
        //        foreach (DataRow dts in dtclnn.Rows)
        //        {
        //            depart += "'" + dts["License_Emp_id"] + "',";
        //        }
        //        if (depart.Length > 0)
        //        {
        //            depart = depart.Remove(depart.Length - 1);
        //            strdepa = " And Id IN (" + depart + ") ";
        //        }
        //    }
        //}
        //if (ddlbusinessfilter.SelectedIndex > 0)
        //{

        //    string finalstr = " SELECT dbo.Syncr_LicenseEmployee_With_JobcenterId.License_Emp_id, dbo.EmployeeMaster.Whid FROM  dbo.EmployeeMaster INNER JOIN dbo.Syncr_LicenseEmployee_With_JobcenterId ON  dbo.EmployeeMaster.EmployeeMasterID = dbo.Syncr_LicenseEmployee_With_JobcenterId.Jobcenter_Emp_id INNER JOIN dbo.DepartmentmasterMNC ON dbo.EmployeeMaster.DeptID = dbo.DepartmentmasterMNC.id  Where  dbo.EmployeeMaster.Whid='" + ddlbusinessfilter.SelectedValue + "'";
        //    SqlCommand cmdclnn = new SqlCommand(finalstr, conioffce);
        //    SqlDataAdapter adpclnn = new SqlDataAdapter(cmdclnn);
        //    DataTable dtclnn = new DataTable();
        //    adpclnn.Fill(dtclnn);
        //    if (dtclnn.Rows.Count > 0)
        //    {
        //        foreach (DataRow dts in dtclnn.Rows)
        //        {
        //            empid += "'" + dts["License_Emp_id"] + "',";
        //        }
        //        if (empid.Length > 0)
        //        {
        //            empid = empid.Remove(empid.Length - 1);
        //            strcon = " And Id IN (" + empid + ") ";
        //        }
        //    }
        //}
        //else
        //{
        //    string finalstr = " SELECT dbo.Syncr_LicenseEmployee_With_JobcenterId.License_Emp_id, dbo.DepartmentmasterMNC.id FROM  dbo.EmployeeMaster INNER JOIN dbo.Syncr_LicenseEmployee_With_JobcenterId ON  dbo.EmployeeMaster.EmployeeMasterID = dbo.Syncr_LicenseEmployee_With_JobcenterId.Jobcenter_Emp_id INNER JOIN dbo.DepartmentmasterMNC ON dbo.EmployeeMaster.DeptID = dbo.DepartmentmasterMNC.id  Where   EmployeeMaster.Active=1 ";
        //    SqlCommand cmdclnn = new SqlCommand(finalstr, conioffce);
        //    SqlDataAdapter adpclnn = new SqlDataAdapter(cmdclnn);
        //    DataTable dtclnn = new DataTable();
        //    adpclnn.Fill(dtclnn);
        //    if (dtclnn.Rows.Count > 0)
        //    {
        //        foreach (DataRow dts in dtclnn.Rows)
        //        {
        //            allemp += "'" + dts["License_Emp_id"] + "',";
        //        }
        //        if (allemp.Length > 0)
        //        {
        //            allemp = allemp.Remove(allemp.Length - 1);
        //            strall = " And Id IN (" + allemp + ") ";
        //        }
        //    }
        //}

        string strwhid = "";
        if (ddlbusinessfilter.SelectedIndex > 0)
        {
            strwhid += " and dbo.EmployeeMaster.Whid='" + ddlbusinessfilter.SelectedValue + "' ";
        }
        if (ddldepartmentfilter.SelectedIndex > 0)
        {
            strwhid += " and dbo.EmployeeMaster.DeptID='" + ddldepartmentfilter.SelectedValue + "' ";
        }

        //SELECT dbo.Syncr_LicenseEmployee_With_JobcenterId.License_Emp_id, dbo.EmployeeMaster.EmployeeName, dbo.EmployeeMaster.DeptID, dbo.EmployeeMaster.Whid FROM dbo.EmployeeMaster INNER JOIN dbo.Syncr_LicenseEmployee_With_JobcenterId ON  dbo.EmployeeMaster.EmployeeMasterID = dbo.Syncr_LicenseEmployee_With_JobcenterId.Jobcenter_Emp_id WHERE (dbo.EmployeeMaster.Active = 1)
        //string strcln = " SELECT * from EmployeeMaster where ClientId='" + Session["ClientId"] + "'" + strall + " " + strcon + "" + strdepa + " order by Name  ";
        string strcln = " SELECT  dbo.Syncr_LicenseEmployee_With_JobcenterId.License_Emp_id, dbo.EmployeeMaster.EmployeeName, dbo.EmployeeMaster.Whid ,  dbo.EmployeeMaster.DeptID FROM  dbo.EmployeeMaster INNER JOIN dbo.Syncr_LicenseEmployee_With_JobcenterId ON  dbo.EmployeeMaster.EmployeeMasterID = dbo.Syncr_LicenseEmployee_With_JobcenterId.Jobcenter_Emp_id INNER JOIN dbo.DepartmentmasterMNC ON dbo.EmployeeMaster.DeptID = dbo.DepartmentmasterMNC.id  Where   EmployeeMaster.Active=1 " + strwhid + " ";
        SqlCommand cmdcln = new SqlCommand(strcln, conioffce);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);

        //string strcln = " SELECT * from EmployeeMaster where ClientId='" + Session["ClientId"] + "' " + strall + " " + strcon + "" + strdepa + " Order By Name ";
        //SqlCommand cmdcln = new SqlCommand(strcln, con);
        //DataTable dtcln = new DataTable();
        //SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        //adpcln.Fill(dtcln);

        ddl_empsearch.DataSource = dtcln;
        ddl_empsearch.DataValueField = "License_Emp_id";
        ddl_empsearch.DataTextField = "EmployeeName";
        ddl_empsearch.DataBind();

        ddl_empsearch.Items.Insert(0, "-Select-");
        //ddlemployee.Items[0].Value = "0";
    }

    //-------------------------------------------------
    protected void ddlemployee_SelectedIndexChanged1(object sender, EventArgs e)
    {
        //FillHours();
        Button1_ClickSearch(sender, e);

    }
    protected void fillpagename()
    {
        string file1 = "";
       

        if (DropDownList1.SelectedValue == "1")
        {
            file1 = "+'- Main Hr :'+  PageWorkTbl.BudgetedHourDevelopment ";
        }
        if (DropDownList1.SelectedValue == "2")
        {
            file1 = "+'- Main Hr :'+ PageWorkTbl.BudgetedHourTesting ";
        }
        if (DropDownList1.SelectedValue == "3")
        {
            file1 = "+'- Main Hr :'+ PageWorkTbl.BudgetedHourSupervisorChecking ";
        }

        string str1 = "";
        string str2 = "";
        string str3 = "";
        string pageid = "";


        string strcln = " select Top(100) PageMaster.PageName +'-'+PageVersionTbl.VersionNo +'-'+ PageWorkTbl.WorkRequirementTitle " + file1 + " as WorkTitle, PageWorkTbl.*  from PageWorkTbl inner join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId  inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId inner join MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId  inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId inner join VersionInfoMaster    on VersionInfoMaster.VersionInfoId = WebsiteMaster.VersionInfoId  inner join ProductMaster   on VersionInfoMaster.ProductId=ProductMaster.ProductId where   PageMaster.PageName !='' ";
        
        if (DropDownList1.SelectedValue == "1")
        {

            str1 = " and  PageWorkTbl.EpmloyeeID_AssignedDeveloper='" + ddlemployee.SelectedValue + "' and (dbo.PageVersionTbl.DeveloperOK='0' Or dbo.PageVersionTbl.TesterOk='0' OR dbo.PageVersionTbl.SupervisorOk='0')";
        }
        if (DropDownList1.SelectedValue == "2")
        {
            str2 = " and PageWorkTbl.EpmloyeeID_AssignedTester='" + ddlemployee.SelectedValue + "' and (dbo.PageVersionTbl.DeveloperOK='0' Or dbo.PageVersionTbl.TesterOk='0' OR dbo.PageVersionTbl.SupervisorOk='0') ";
        }
        if (DropDownList1.SelectedValue == "3")
        {
            str3 = " and  PageWorkTbl.EpmloyeeID_AssignedSupervisor='" + ddlemployee.SelectedValue + "' and (dbo.PageVersionTbl.DeveloperOK='0' Or dbo.PageVersionTbl.TesterOk='0' OR dbo.PageVersionTbl.SupervisorOk='0') ";
        }
        if (ddlpage.SelectedIndex > 0)
        {
            pageid = " and PageMaster.PageId='" + ddlpage.SelectedValue + "' ";
        }

        try
        {
            string orderby = " order by WebsiteMaster.WebsiteName,PageMaster.PageName,PageWorkTbl.WorkRequirementTitle";

            string finalstr = strcln + str1 + str2 + str3 + pageid + orderby;

            SqlCommand cmdcln = new SqlCommand(finalstr, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);

            ddlworktitle.DataSource = dtcln;
            ddlworktitle.DataValueField = "Id";
            ddlworktitle.DataTextField = "WorkTitle";
            ddlworktitle.DataBind();

            ddlworktitle.Items.Insert(0, "-Select-");
            ddlworktitle.Items[0].Value = "0";
        }
        catch (Exception ex)
        {
        }
        
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        string Insert = "";
        if (Button1.Text == "Submit")
        {
            if (ddlworktitle.SelectedIndex > 0)
            {
                Insert = "Insert into MyDailyWorkReport(PageWorkTblId,EmployeeId,budgetedhour,workallocationdate,worktobedone,Typeofwork,PageId,ProductId, Offer_Amount, Priority, WorkDone) values ('" + ddlworktitle.SelectedValue + "','" + ddlemployee.SelectedValue + "','" + txttodaybudgetdhour.Text + "','" + txttargetdatedeve.Text + "','" + txtworktobedone.Text + "','" + DropDownList1.SelectedValue + "','" + ddlpage.SelectedValue + "','" + Session["ProductId"].ToString() + "', '" + txtincentive.Text + "' , '" + ddl_priority.SelectedItem.Text + "', '0')";

            }
            else
            {
                Insert = "Insert into MyDailyWorkReport(EmployeeId,budgetedhour,workallocationdate,worktobedone,Typeofwork,PageId,ProductId, Offer_Amount, Priority ,WorkDone) values ('" + ddlemployee.SelectedValue + "','" + txttodaybudgetdhour.Text + "','" + txttargetdatedeve.Text + "','" + txtworktobedone.Text + "','" + DropDownList1.SelectedValue + "','" + ddlpage.SelectedValue + "','" + Session["ProductId"].ToString() + "' , '" + txtincentive.Text + "', '" + ddl_priority.SelectedItem.Text + "', '0')";

            }


            SqlCommand cmdinsert = new SqlCommand(Insert, con);
            con.Open();
            cmdinsert.ExecuteNonQuery();
            con.Close();

            string str1113 = "select Max(Id) as Id from MyDailyWorkReport  ";
            SqlCommand cmd1113 = new SqlCommand(str1113, con);
            SqlDataAdapter adp1113 = new SqlDataAdapter(cmd1113);
            DataTable ds1113 = new DataTable();
            adp1113.Fill(ds1113);

            if (ds1113.Rows.Count > 0)
            {
                ViewState["MaxId"] = ds1113.Rows[0]["Id"].ToString();

                foreach (GridViewRow gdr in gridFileAttach.Rows)
                {
                    Label lbltitle = (Label)gdr.FindControl("lbltitle");
                    Label lblpdfurl = (Label)gdr.FindControl("lblpdfurl");
                    Label lblaudiourl = (Label)gdr.FindControl("lblaudiourl");

                    string strftpinsert = "INSERT INTO DailyWorkGuideUploadTbl (MyworktblId,WorkRequirementPdfFilename,WorkRequirementAudioFileName,FileTitle,Date) values('" + ViewState["MaxId"].ToString() + "','" + lblpdfurl.Text + "','" + lblaudiourl.Text + "','" + lbltitle.Text + "','" + System.DateTime.Now.ToShortDateString() + "')";

                    SqlCommand cmdftpinsert = new SqlCommand(strftpinsert, con);
                    con.Open();
                    cmdftpinsert.ExecuteNonQuery();
                    con.Close();
                    if (lblpdfurl.Text != "")
                    {
                        //  ftpfile(lblpdfurl.Text.ToString(), Server.MapPath("~\\Attach\\") + lblpdfurl.Text.ToString());

                    }
                    if (lblaudiourl.Text != "")
                    {
                        //    ftpfile(lblaudiourl.Text.ToString(), Server.MapPath("~\\Attach\\") + lblaudiourl.Text.ToString());

                    }

                }
            }
            Session["GridFileAttach1"] = null;
            gridFileAttach.DataSource = null;
            gridFileAttach.DataBind();


            lblmsg.Visible = true;
            lblmsg.Text = "Record Inserted Successfully";
            txttodaybudgetdhour.Text = "00:00";

            filltodayhour();
        }
        else
        {
              string str;
              string query = Request.QueryString["Id"];  
            if (ddlworktitle.SelectedIndex > 0)
            {
                str = "update   MyDailyWorkReport set PageWorkTblId='" + ddlworktitle.SelectedValue + "',EmployeeId='" + ddlemployee.SelectedValue + "' ,budgetedhour='" + txttodaybudgetdhour.Text + "' ,workallocationdate='" + txttargetdatedeve.Text + "' ,worktobedone='" + txtworktobedone.Text + "',Typeofwork='" + DropDownList1.SelectedValue + "' ,PageId='" + ddlpage.SelectedValue + "',ProductId='" + Session["ProductId"].ToString() + "' , Offer_Amount='" + txtincentive.Text + "', Priority='" + ddl_priority.SelectedItem.Text + "' where id='" + query + "' ";
                

            }
            else
            {
                str = "update   MyDailyWorkReport set EmployeeId='" + ddlemployee.SelectedValue + "', workallocationdate='" + txttargetdatedeve.Text + "',  budgetedhour='" + txttodaybudgetdhour.Text + "', worktobedone='" + txtworktobedone.Text + "',Typeofwork='" + DropDownList1.SelectedValue + "',PageId='" + ddlpage.SelectedValue + "',ProductId='" + Session["ProductId"].ToString() + "', Offer_Amount='" + txtincentive.Text + "', Priority='" + ddl_priority.SelectedItem.Text + "'  where id='" + query + "' ";
                
            }
           //= "update   PageMaster set   LanguageId = '" + ddlanguage.SelectedValue + "',PageName= '" + txtpagename.Text + "' ,PageTitle='" + txtpagetitle.Text + "', PageDescription='" + txtpagedescriptin.Text + "',pageIndex='" + txtpageindex.Text + "',VersionInfoMasterId='" + ViewState["VersionInfoId"].ToString() + "',MainMenuId='" + ddlMainMenu.SelectedValue + "',FolderName='" + txtFolderName.Text + "',Active='" + chkactive.Checked + "',SubMenuId='" + ddlSubmenu.SelectedValue + "',ManuAccess='" + chkmanuaccess.Checked + "' where pageid='" + ViewState["pageid"] + "'";
            SqlCommand cmd = new SqlCommand(str, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            string strftpinsert1 = "Delete From DailyWorkGuideUploadTbl where MyworktblId=" + query + "";
            con.Close();
            con.Open();
            SqlCommand cmdinsert1 = new SqlCommand(strftpinsert1, con);
            cmdinsert1.ExecuteNonQuery();
            foreach (GridViewRow gdr in gridFileAttach.Rows)
            {
                Label lbltitle = (Label)gdr.FindControl("lbltitle");
                Label lblpdfurl = (Label)gdr.FindControl("lblpdfurl");
                Label lblaudiourl = (Label)gdr.FindControl("lblaudiourl");

              

                con.Close();

                string strftpinsert = "INSERT INTO DailyWorkGuideUploadTbl (MyworktblId,WorkRequirementPdfFilename,WorkRequirementAudioFileName,FileTitle,Date) values('" + query + "','" + lblpdfurl.Text + "','" + lblaudiourl.Text + "','" + lbltitle.Text + "','" + System.DateTime.Now.ToShortDateString() + "')";

                SqlCommand cmdinsert = new SqlCommand(strftpinsert, con);
                con.Open();
                cmdinsert.ExecuteNonQuery();
                con.Close();

            }
            lblmsg.Visible = true;
            lblmsg.Text = "Record Update Successfully";
            txtworktobedone.Text = "";
            txttodaybudgetdhour.Text = "";

            filltodayhour();
        }
      

    }
    protected void FillHours()
    {

        try
        {
            string strcln = " select sum(datepart(hour,convert(datetime,budgetedhour)))  AS TotalHours,sum(datepart(minute,convert(datetime,budgetedhour))) AS TotalMinutes from MyDailyWorkReport where EmployeeId='" + ddl_empsearch.SelectedValue + "' and workallocationdate='" + TextBox1.Text + "'";
            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);

            if (dtcln.Rows.Count > 0)
            {
                string TotalHour = dtcln.Rows[0]["TotalHours"].ToString();
                string TotalMinutes = dtcln.Rows[0]["TotalMinutes"].ToString();

                if (TotalHour == "" && TotalMinutes == "")
                {
                    TotalHour = "0";
                    TotalMinutes = "0";

                }

                lbl_totalhours.Text = TotalHour + ":" + TotalMinutes;
            }
            else
            {
                lbl_totalhours.Text = "0.0";

            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void fillgrid()
    {
        // lblfromdate.Text = txttargetdatedeve.Text + " 00:00";
        //  lbltodate.Text = TextBox1.Text + " 23:59";
        //   lblwebsitename.Text = ddlwebsite.SelectedItem.Text;
        //    lblempname.Text = ddl_empsearch.SelectedItem.Text;


        string strcln = "";
        string product = "";
        string todaystatus = "";
        string allstatus = "";
        string pagename = "";



        // strcln = "select MyDailyWorkReport.*,PageWorkTbl.WorkRequirementTitle,WebsiteMaster.WebsiteName,PageMaster.PageName as PageTitle ,EmployeeMaster.Name as EmployeeName,EmployeeMaster.EffectiveRate ,ProductMaster.ProductName,MyDailyWorkReport.Typeofwork,PageVersionTbl.VersionNo, case when (MyDailyWorkReport.Typeofwork='1') then 'Developer' else  (case when (MyDailyWorkReport.Typeofwork='2') then 'Tester' else (case when (MyDailyWorkReport.Typeofwork='3') then 'Supervisor' else '' End) End   )  End  as Statuslabel, case when (MyDailyWorkReport.WorkDone='0' or MyDailyWorkReport.WorkDone IS NULL) then 'Pending' else 'Completed' End as TodayStatus from MyDailyWorkReport  left outer join   PageWorkTbl on MyDailyWorkReport.PageWorkTblId=PageWorkTbl.Id   left outer join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId   left outer join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId  left outer join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId   left outer join  MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id   left outer join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId     left outer join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId 	left outer join VersionInfoMaster    on VersionInfoMaster.VersionInfoId = WebsiteMaster.VersionInfoId  	left outer join ProductMaster   on VersionInfoMaster.ProductId=ProductMaster.ProductId 	inner join  EmployeeMaster on EmployeeMaster.Id=MyDailyWorkReport.EmployeeId where MyDailyWorkReport.workallocationdate between '" + txttargetdatedeve.Text + "' and '" + TextBox1.Text + "' and MyDailyWorkReport.EmployeeId='" + ddlemployee.SelectedValue + "'";

        strcln = " select Distinct Top(200) MyDailyWorkReport.*,PageWorkTbl.WorkRequirementTitle,PageMaster.PageName as PageTitle ,EmployeeMaster.Name as EmployeeName,EmployeeMaster.EffectiveRate ,ProductMaster.ProductName,MyDailyWorkReport.Typeofwork,PageVersionTbl.VersionNo, case when (MyDailyWorkReport.Typeofwork='1') then 'Developer' else  (case when (MyDailyWorkReport.Typeofwork='2') then 'Tester' else (case when (MyDailyWorkReport.Typeofwork='3') then 'Supervisor' else '' End) End   )  End  as Statuslabel, case when (MyDailyWorkReport.WorkDone='0' or MyDailyWorkReport.WorkDone IS NULL) then 'Pending' else 'Completed' End as TodayStatus from MyDailyWorkReport  left outer join ProductMaster on ProductMaster.ProductId=MyDailyWorkReport.ProductId left outer join VersionInfoMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId left outer join WebsiteMaster on WebsiteMaster.VersionInfoId=VersionInfoMaster.VersionInfoId left outer join   WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.ID left outer join   MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId left outer join PageMaster on PageMaster.PageId=MyDailyWorkReport.PageId Left Outer join   PageWorkTbl on MyDailyWorkReport.PageWorkTblId=PageWorkTbl.Id  left outer join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId      Left OUter join  EmployeeMaster on EmployeeMaster.Id=MyDailyWorkReport.EmployeeId   " +
            "  where  MyDailyWorkReport.EmployeeId IS NOT NULL ";
        //MyDailyWorkReport.PageworkTblid IS NOT NULL ";

        //strcln = " SELECT        MyDailyWorkReport.Id, MyDailyWorkReport.PageWorkTblId, MyDailyWorkReport.Date, MyDailyWorkReport.HourSpent, MyDailyWorkReport.WorkDoneReport, MyDailyWorkReport.EmployeeId, MyDailyWorkReport.WorkDone, MyDailyWorkReport.budgetedhour, MyDailyWorkReport.workallocationdate, MyDailyWorkReport.worktobedone, MyDailyWorkReport.Typeofwork, MyDailyWorkReport.EmpRequestHour, MyDailyWorkReport.PageId, MyDailyWorkReport.ProductId, MyDailyWorkReport.Offer_Amount, MyDailyWorkReport.Earn_Amount, MyDailyWorkReport.Priority, dbo.PageWorkTbl.WorkRequirementTitle, dbo.PageVersionTbl.VersionNo, dbo.EmployeeMaster.Name as EmployeeName, dbo.EmployeeMaster.EffectiveRate, dbo.PageMaster.PageName, dbo.ProductMaster.ProductName " +
        //    " ,case when (MyDailyWorkReport.Typeofwork='1') then 'Developer' else  (case when (MyDailyWorkReport.Typeofwork='2') then 'Tester' else (case when (MyDailyWorkReport.Typeofwork='3') then 'Supervisor' else '' End) End   )  End  as Statuslabel ,case when (MyDailyWorkReport.WorkDone='0' or MyDailyWorkReport.WorkDone IS NULL) then 'Pending' else 'Completed' End as TodayStatus " +      
        //    " FROM  dbo.MyDailyWorkReport AS MyDailyWorkReport INNER JOIN (SELECT        PageId, MAX(PageWorkTblId) AS maxid FROM dbo.MyDailyWorkReport AS MyDailyWorkReport_1 GROUP BY PageId) AS MyDailyWorkReportb ON MyDailyWorkReport.PageWorkTblId = MyDailyWorkReportb.maxid INNER JOIN dbo.PageWorkTbl ON MyDailyWorkReport.PageWorkTblId = dbo.PageWorkTbl.Id INNER JOIN dbo.PageVersionTbl ON dbo.PageWorkTbl.PageVersionTblId = dbo.PageVersionTbl.Id INNER JOIN dbo.EmployeeMaster ON MyDailyWorkReport.EmployeeId = dbo.EmployeeMaster.Id INNER JOIN dbo.PageMaster ON MyDailyWorkReport.PageId = dbo.PageMaster.PageId INNER JOIN dbo.ProductMaster ON MyDailyWorkReport.ProductId = dbo.ProductMaster.ProductId INNER JOIN dbo.VersionInfoMaster ON dbo.ProductMaster.ProductId = dbo.VersionInfoMaster.ProductId INNER JOIN dbo.WebsiteMaster ON dbo.VersionInfoMaster.VersionInfoId = dbo.WebsiteMaster.VersionInfoId INNER JOIN dbo.WebsiteSection ON dbo.WebsiteMaster.ID = dbo.WebsiteSection.WebsiteMasterId INNER JOIN dbo.MasterPageMaster ON dbo.WebsiteSection.WebsiteSectionId = dbo.MasterPageMaster.WebsiteSectionId " +
        //              "  where  MyDailyWorkReport.PageworkTblid IS NOT NULL ";


       // allstatus = " and ( MyDailyWorkReport.WorkDone='0' Or MyDailyWorkReport.WorkDone='' Or MyDailyWorkReport.WorkDone IS NULL) ";
       if(ddl_empsearch.SelectedIndex >0 )
       {
           allstatus +=" AND  MyDailyWorkReport.EmployeeId='" + ddl_empsearch.SelectedValue + "' ";
       }
       
        if (TextBox1.Text.Trim() != "" )
        {
            allstatus += " and MyDailyWorkReport.workallocationdate = '" + TextBox1.Text + "'  ";
            // allstatus += " and MyDailyWorkReport.workallocationdate between '" + TextBox1.Text.Trim() + "' and '" + TextBox2.Text.Trim() + "' ";
        }

        if (DropDownList2.SelectedIndex > 0)
        {

        }
        if (DropDownList2.SelectedValue =="1")
        {
            strcln += " and MyDailyWorkReport.Typeofwork='1'";
        }

        if (DropDownList2.SelectedValue =="2")
        {
            strcln += " and MyDailyWorkReport.Typeofwork='2'";
        }

        if (DropDownList2.SelectedValue =="3")
        {
            strcln += " and MyDailyWorkReport.Typeofwork='3'";
        }
        string orderby = " order by MyDailyWorkReport.WorkallocationDate desc ";

        string finalstr = strcln + product + todaystatus + allstatus + pagename + orderby;





        SqlCommand cmdcln = new SqlCommand(finalstr, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);




        if (dtcln.Rows.Count > 0)
        {
            DataView myDataView = new DataView();
            myDataView = dtcln.DefaultView;

            //if (hdnsortExp.Value != string.Empty)
            //{
            //    //   myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            //}

            GridView2.DataSource = myDataView;
            GridView2.DataBind();

            decimal sumTotal = 0;
            decimal sumEarn = 0;


            Double Sum_BugHourToday = 0;
            Double Sum_lblbudhrmaintask = 0;
            Double Sum_lblActualHrMaintask = 0;
            Double Sum_lblactualhourToday = 0;
            Double Sum_lblactualhourmain = 0;
            Double Sum_lblBudgeted_Cost = 0;
            foreach (GridViewRow gdr in GridView2.Rows)
            {
                string lbl_inceoffer = ((Label)gdr.FindControl("lbl_inceoffer")).Text;
                //Label lblftrtodaybudhrtotal = (Label)ft.FindControl("lblftrtodaybudhrtotal");
                //   string lbl_totaloffer = ((Label)e.Row.FindControl("lbl_totaloffer")).Text;

                string lbl_earn = ((Label)gdr.FindControl("lbl_earn")).Text;
                //     string lbl_TotalEarnoffer = ((Label)e.Row.FindControl("lbl_TotalEarnoffer")).Text;
                Label String_product = (Label)gdr.FindControl("lblwebsitename12345");
                TextBox txtNewtargetdatedeve = (TextBox)gdr.FindControl("txtNewtargetdatedeve");
                Label lbl_totalhrsforday = (Label)gdr.FindControl("lbl_totalhrsforday");
                txtNewtargetdatedeve.Text = txttargetdatedeve.Text;



                

                try
                {
                    string strclnp = " select sum(datepart(hour,convert(datetime,budgetedhour)))  AS TotalHours,sum(datepart(minute,convert(datetime,budgetedhour))) AS TotalMinutes from MyDailyWorkReport where EmployeeId='" + ddl_empsearch.SelectedValue + "' and workallocationdate='" + txtNewtargetdatedeve.Text + "'";
                    SqlCommand cmdclnp = new SqlCommand(strclnp, con);
                    DataTable dtclnp = new DataTable();
                    SqlDataAdapter adpclnp = new SqlDataAdapter(cmdclnp);
                    adpclnp.Fill(dtclnp);

                    if (dtclnp.Rows.Count > 0)
                    {
                        string TotalHour = dtclnp.Rows[0]["TotalHours"].ToString();
                        string TotalMinutes = dtclnp.Rows[0]["TotalMinutes"].ToString();

                        if (TotalHour == "" && TotalMinutes == "")
                        {
                            TotalHour = "0";
                            TotalMinutes = "0";

                        }

                        lbl_totalhrsforday.Text = TotalHour + ":" + TotalMinutes;
                    }
                    else
                    {
                        lbl_totalhrsforday.Text = "0.0";

                    }
                }
                catch (Exception ex)
                {

                }





                try
                {
                    if (String_product.Text.ToString().Length > 4)
                    {
                        String_product.Text = String_product.Text.ToString().Substring(0, 12) + "...";
                    }


                }
                catch (Exception ex)
                {
                }
                Label lblpagename123 = (Label)gdr.FindControl("lblpagename123");
                try
                {
                    if (lblpagename123.Text.ToString().Length > 4)
                    {
                        lblpagename123.Text = String_product.Text.ToString().Substring(0, 32) + "...";
                    }


                }
                catch (Exception ex)
                {
                }
                try
                {

                    decimal totalvalue = Convert.ToDecimal(lbl_inceoffer);
                    sumTotal += totalvalue;
                }
                catch (Exception ex)
                {
                }

                try
                {
                    decimal totallbl_earn = Convert.ToDecimal(lbl_earn);
                    sumEarn += totallbl_earn;
                }
                catch (Exception ex)
                {
                }

                GridViewRow ft = (GridViewRow)(GridView2.FooterRow);
                Label lbl_totaloffer = (Label)ft.FindControl("lbl_totaloffer");
                Label lbl_TotalEarnoffer = (Label)ft.FindControl("lbl_TotalEarnoffer");
                lbl_totaloffer.Text = Convert.ToString(sumTotal);
                lbl_TotalEarnoffer.Text = Convert.ToString(sumEarn);







                //5---Act Hr Daily Task-----------------------------------------

                Label lblactualhour = (Label)gdr.FindControl("lblactualhour");
                lblactualhour.Text = lblactualhour.Text.Replace(":", ".");
                Double total_lblActHrMainTask = 0;
                try
                {
                    total_lblActHrMainTask = Convert.ToDouble(lblactualhour.Text);
                }
                catch (Exception ex)
                {
                    lblactualhour.Text = "00.00";
                    total_lblActHrMainTask = Convert.ToDouble(lblactualhour.Text);

                }
                Sum_lblactualhourmain = total_lblActHrMainTask + Sum_lblactualhourmain;
                Label lblftractualtodaytask = (Label)ft.FindControl("lblftractualtodaytask");
                lblftractualtodaytask.Text = Convert.ToString(Sum_lblactualhourmain);



                //3N-------------bug Hr Main Task-------------------------------------------------
                Label lblbudhrmaintask = (Label)gdr.FindControl("lblbudhrmaintask");
                lblbudhrmaintask.Text = lblbudhrmaintask.Text.Replace(":", ".");
                Double totallbl_lblbudhrmaintask = 0;
                try
                {
                    totallbl_lblbudhrmaintask = Convert.ToDouble(lblbudhrmaintask.Text);
                    Sum_lblbudhrmaintask = totallbl_lblbudhrmaintask + Sum_lblbudhrmaintask;
                }
                catch (Exception ex)
                {
                    lblbudhrmaintask.Text = "00.00";
                    totallbl_lblbudhrmaintask = Convert.ToDouble(lblbudhrmaintask.Text);
                    Sum_lblbudhrmaintask = totallbl_lblbudhrmaintask + Sum_lblbudhrmaintask;

                }

                Label lbl_budghurfootr = (Label)ft.FindControl("lbl_budghurfootr");
                lbl_budghurfootr.Text = Convert.ToString(Sum_lblbudhrmaintask);

            }

        }

        else
        {
            GridView2.DataSource = null;
            GridView2.DataBind();



        }






        //}
    }

    protected void Button1_ClickSearch(object sender, EventArgs e)
    {
        // columndisplay();
        fillgrid();

    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            
             Session["lbl_shortlist"] = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
            TextBox txt_newHrs = row.FindControl("txt_newHrs") as TextBox;
            TextBox txtNewtargetdatedeve = row.FindControl("txtNewtargetdatedeve") as TextBox;
            LinkButton LinkButton5Edit = row.FindControl("LinkButton5Edit") as LinkButton;
            LinkButton LinkButton2Show = row.FindControl("LinkButton2Show") as LinkButton;
            Label lbl_totalhrsforday = row.FindControl("lbl_totalhrsforday") as Label;
            Label lbl_pageid = row.FindControl("lbl_pageid") as Label;


            string strpageworkall = "select * from DailyWorkGuideUploadTbl where MyworktblId='" + lbl_pageid.Text  + "' ";
            strpageworkall = " SELECT          dbo.DailyWorkGuideUploadTbl.MyworktblId AS Expr2, dbo.DailyWorkGuideUploadTbl.WorkRequirementPdfFilename AS Expr3, dbo.DailyWorkGuideUploadTbl.Id AS Expr1, dbo.MyDailyWorkReport.PageId, dbo.MyDailyWorkReport.PageWorkTblId, dbo.DailyWorkGuideUploadTbl.* FROM            dbo.MyDailyWorkReport INNER JOIN " +
                                     "  dbo.DailyWorkGuideUploadTbl ON dbo.MyDailyWorkReport.Id = dbo.DailyWorkGuideUploadTbl.MyworktblId Where  dbo.MyDailyWorkReport.PageId='" + lbl_pageid.Text  + "'";

            SqlCommand cmdpageworkall = new SqlCommand(strpageworkall, con);
            DataTable dtpageworkall = new DataTable();
            SqlDataAdapter adppageworkall = new SqlDataAdapter(cmdpageworkall);
            adppageworkall.Fill(dtpageworkall);

            if (dtpageworkall.Rows.Count > 0)
            {
                GridView3.DataSource = dtpageworkall;
                GridView3.DataBind();
            }

            string HorsNew = txt_newHrs.Text;
            string DateNew = txtNewtargetdatedeve.Text;

            if (ddl_empsearch.SelectedIndex > 0 && HorsNew != "00:00" && DateNew != "")
            {
                try
                {
                    string strcln = " select sum(datepart(hour,convert(datetime,budgetedhour)))  AS TotalHours,sum(datepart(minute,convert(datetime,budgetedhour))) AS TotalMinutes from MyDailyWorkReport where EmployeeId='" + ddl_empsearch.SelectedValue + "' and workallocationdate='" + DateNew + "'";
                    SqlCommand cmdcln = new SqlCommand(strcln, con);
                    DataTable dtcln = new DataTable();
                    SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
                    adpcln.Fill(dtcln);

                    if (dtcln.Rows.Count > 0)
                    {
                        string TotalHour = dtcln.Rows[0]["TotalHours"].ToString();
                        string TotalMinutes = dtcln.Rows[0]["TotalMinutes"].ToString();

                        lbl_totalhrsforday.Text = TotalHour + ":" + TotalMinutes;

                        if (TotalHour == "" && TotalMinutes == "")
                        {
                            TotalHour = "00";
                            TotalMinutes = "00";

                        }
                        double contohour;
                        try
                        {
                            string time = "";
                            string outdifftime = "";
                            string temp12 = "";
                            string temp123 = "";
                            TimeSpan t4 = TimeSpan.Parse(HorsNew);
                            time = t4.ToString();


                            outdifftime = Convert.ToDateTime(time).ToString("HH:mm");
                            temp12 = Convert.ToDateTime(time).ToString("HH");
                            temp123 = Convert.ToDateTime(time).ToString("mm");

                            double main1 = Convert.ToDouble(temp12);
                            main1 = main1 * 60;
                            double main2 = Convert.ToDouble(temp123);
                            main1 = main1 + main2;


                            contohour = Convert.ToInt32(TotalHour);
                            contohour = contohour * 60;
                            contohour = contohour + main1;
                            TotalHour = Convert.ToString(contohour);
                        }
                        catch (Exception ex)
                        {

                        }

                        if (Convert.ToInt32(TotalHour) > 540)
                        {
                            lbl_gverror.Visible = true;
                            lbl_gverror.Text = " Insufficient Time Available on this Day for Developer. Select Another Date ";
                        }
                        else
                        {
                            lbl_gverror.Visible = false;
                            lbl_gverror.Text = "";
                            string stdddd = "select Top(1) MyDailyWorkReport.*,PageWorkTbl.WorkRequirementTitle,dbo.PageWorkTbl.Incentive as maininc, dbo.PageWorkTbl.WorkRequirementDescription, PageMaster.PageName as PageTitle ,EmployeeMaster.Name as EmployeeName,EmployeeMaster.EffectiveRate ,ProductMaster.ProductName,MyDailyWorkReport.Typeofwork,PageVersionTbl.VersionNo, case when (MyDailyWorkReport.Typeofwork='1') then 'Developer' else  (case when (MyDailyWorkReport.Typeofwork='2') then 'Tester' else (case when (MyDailyWorkReport.Typeofwork='3') then 'Supervisor' else '' End) End   )  End  as Statuslabel, case when (MyDailyWorkReport.WorkDone='0' or MyDailyWorkReport.WorkDone IS NULL) then 'Pending' else 'Completed' End as TodayStatus from MyDailyWorkReport  left outer join ProductMaster on ProductMaster.ProductId=MyDailyWorkReport.ProductId left outer join VersionInfoMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId left outer join WebsiteMaster on WebsiteMaster.VersionInfoId=VersionInfoMaster.VersionInfoId left outer join   WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.ID left outer join   MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId left outer join PageMaster on PageMaster.PageId=MyDailyWorkReport.PageId left outer join   PageWorkTbl on MyDailyWorkReport.PageWorkTblId=PageWorkTbl.Id  left outer join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId     left outer join  EmployeeMaster on EmployeeMaster.Id=MyDailyWorkReport.EmployeeId   " +
                                                 "   where MyDailyWorkReport.ID=" + Session["lbl_shortlist"] + "  Order By MyDailyWorkReport.ID Desc  ";

                            SqlDataAdapter dafffd = new SqlDataAdapter(stdddd, con);
                            DataTable dtfffd = new DataTable();
                            dafffd.Fill(dtfffd);

                            if (dtfffd.Rows.Count > 0)
                            {
                                Session["ID"] = dtfffd.Rows[0]["ID"].ToString();



                                String Insert = "Insert into MyDailyWorkReport(PageWorkTblId,EmployeeId,budgetedhour,workallocationdate,worktobedone,Typeofwork,PageId,ProductId, Offer_Amount, Priority, WorkDone) values "
                                    + " ('" + dtfffd.Rows[0]["PageWorkTblId"].ToString() + "','" + ddl_empsearch.SelectedValue + "','" + HorsNew + "','" + DateNew + "','" + dtfffd.Rows[0]["worktobedone"].ToString() + "','" + dtfffd.Rows[0]["Typeofwork"].ToString() + "','" + dtfffd.Rows[0]["PageId"].ToString() + "','" + dtfffd.Rows[0]["ProductId"].ToString() + "', '" + dtfffd.Rows[0]["Offer_Amount"].ToString() + "' , '" + dtfffd.Rows[0]["Priority"].ToString() + "' , '0')";
                                //Insert = "Insert into MyDailyWorkReport(EmployeeId,budgetedhour,workallocationdate,worktobedone,Typeofwork,PageId,ProductId, Offer_Amount, Priority) values ('" + ddlemployee.SelectedValue + "','" + txttodaybudgetdhour.Text + "','" + txttargetdatedeve.Text + "','" + txtworktobedone.Text + "','" + DropDownList1.SelectedValue + "','" + ddlpage.SelectedValue + "','" + Session["ProductId"].ToString() + "' , '" + txtincentive.Text + "', '" + ddl_priority.SelectedItem.Text + "')";
                                SqlCommand cmdinsert = new SqlCommand(Insert, con);
                                con.Open();
                                cmdinsert.ExecuteNonQuery();
                                con.Close();

                                string str1113 = "select Max(Id) as Id from MyDailyWorkReport  ";
                                SqlCommand cmd1113 = new SqlCommand(str1113, con);
                                SqlDataAdapter adp1113 = new SqlDataAdapter(cmd1113);
                                DataTable ds1113 = new DataTable();
                                adp1113.Fill(ds1113);

                                if (ds1113.Rows.Count > 0)
                                {
                                    ViewState["MaxId"] = ds1113.Rows[0]["Id"].ToString();

                                    foreach (GridViewRow gdr in GridView1Attach.Rows)
                                    {
                                        Label lbltitle = (Label)gdr.FindControl("lbltitle");
                                        Label lblpdfurl = (Label)gdr.FindControl("lblpdfurl");
                                        Label lblaudiourl = (Label)gdr.FindControl("lblaudiourl");

                                        string strftpinsert = "INSERT INTO DailyWorkGuideUploadTbl (MyworktblId,WorkRequirementPdfFilename,WorkRequirementAudioFileName,FileTitle,Date) values('" + ViewState["MaxId"].ToString() + "','" + lblpdfurl.Text + "','" + lblaudiourl.Text + "','" + lbltitle.Text + "','" + System.DateTime.Now.ToShortDateString() + "')";

                                        SqlCommand cmdftpinsert = new SqlCommand(strftpinsert, con);
                                        con.Open();
                                        cmdftpinsert.ExecuteNonQuery();
                                        con.Close();
                                        if (lblpdfurl.Text != "")
                                        {
                                            //  ftpfile(lblpdfurl.Text.ToString(), Server.MapPath("~\\Attach\\") + lblpdfurl.Text.ToString());

                                        }
                                        if (lblaudiourl.Text != "")
                                        {
                                            //    ftpfile(lblaudiourl.Text.ToString(), Server.MapPath("~\\Attach\\") + lblaudiourl.Text.ToString());

                                        }

                                    }


                                    foreach (GridViewRow gdr in GridView3.Rows)
                                    {
                                        Label lbltitle1 = (Label)gdr.FindControl("lbltitledailywork");
                                        Label lblpdfurl1 = (Label)gdr.FindControl("lblpdfurldailywork");
                                        Label lblaudiourl1 = (Label)gdr.FindControl("lblaudiourldailywork");

                                        string strftpinsert = "INSERT INTO DailyWorkGuideUploadTbl (MyworktblId,WorkRequirementPdfFilename,WorkRequirementAudioFileName,FileTitle,Date) values('" + ViewState["MaxId"].ToString() + "','" + lblpdfurl1.Text + "','" + lblaudiourl1.Text + "','" + lbltitle1.Text + "','" + System.DateTime.Now.ToShortDateString() + "')";

                                        SqlCommand cmdftpinsert = new SqlCommand(strftpinsert, con);
                                        con.Open();
                                        cmdftpinsert.ExecuteNonQuery();
                                        con.Close();
                                        if (lblaudiourl1.Text != "")
                                        {
                                            //  ftpfile(lblpdfurl.Text.ToString(), Server.MapPath("~\\Attach\\") + lblpdfurl.Text.ToString());

                                        }
                                        if (lblaudiourl1.Text != "")
                                        {
                                            //    ftpfile(lblaudiourl.Text.ToString(), Server.MapPath("~\\Attach\\") + lblaudiourl.Text.ToString());

                                        }

                                    }
                                }
                                Session["GridFileAttach1n"] = null;
                                GridView1Attach.DataSource = null;
                                GridView1Attach.DataBind();




                                LinkButton2Show.Visible = true;
                                LinkButton2Show.Text = "Reallocated for " + DateNew;
                                LinkButton5Edit.Visible = false;  
                            }

                        }

                    }
                    else
                    {
                        lbltotalworkallocatedtoday.Text = "";

                    }
                }
                catch (Exception ex)
                {

                }


                decimal sumTotal = 0;
                decimal sumEarn = 0;


                Double Sum_BugHourToday = 0;
                Double Sum_lblbudhrmaintask = 0;
                Double Sum_lblActualHrMaintask = 0;
                Double Sum_lblactualhourToday = 0;
                Double Sum_lblactualhourmain = 0;
                Double Sum_lblBudgeted_Cost = 0;
                foreach (GridViewRow gdr in GridView2.Rows)
                {
                    string lbl_inceoffer = ((Label)gdr.FindControl("lbl_inceoffer")).Text;
                    //Label lblftrtodaybudhrtotal = (Label)ft.FindControl("lblftrtodaybudhrtotal");
                    //   string lbl_totaloffer = ((Label)e.Row.FindControl("lbl_totaloffer")).Text;

                    string lbl_earn = ((Label)gdr.FindControl("lbl_earn")).Text;
                    //     string lbl_TotalEarnoffer = ((Label)e.Row.FindControl("lbl_TotalEarnoffer")).Text;
                    Label String_product = (Label)gdr.FindControl("lblwebsitename12345");
                    Label lbl_totalhrsforday1 = (Label)gdr.FindControl("lbl_totalhrsforday");
                   


                    try
                    {
                        string strclnp = " select sum(datepart(hour,convert(datetime,budgetedhour)))  AS TotalHours,sum(datepart(minute,convert(datetime,budgetedhour))) AS TotalMinutes from MyDailyWorkReport where EmployeeId='" + ddl_empsearch.SelectedValue + "' and workallocationdate='" + txtNewtargetdatedeve.Text + "'";
                        SqlCommand cmdclnp = new SqlCommand(strclnp, con);
                        DataTable dtclnp = new DataTable();
                        SqlDataAdapter adpclnp = new SqlDataAdapter(cmdclnp);
                        adpclnp.Fill(dtclnp);

                        if (dtclnp.Rows.Count > 0)
                        {
                            string TotalHour = dtclnp.Rows[0]["TotalHours"].ToString();
                            string TotalMinutes = dtclnp.Rows[0]["TotalMinutes"].ToString();

                            if (TotalHour == "" && TotalMinutes == "")
                            {
                                TotalHour = "0";
                                TotalMinutes = "0";

                            }

                            lbl_totalhrsforday1.Text = TotalHour + ":" + TotalMinutes;
                        }
                        else
                        {
                            lbl_totalhrsforday.Text = "0.0";

                        }
                    }
                    catch (Exception ex)
                    {

                    }



                }

            }
            else
            {
                lbl_gverror.Text = "Employee Name / Allocation Date / Allocation Hours Must Select";
                lbl_gverror.Visible = true; 
            }
            
            string stdd = "select Top(1) MyDailyWorkReport.*,PageWorkTbl.WorkRequirementTitle,dbo.PageWorkTbl.Incentive as maininc, dbo.PageWorkTbl.WorkRequirementDescription, PageMaster.PageName as PageTitle ,EmployeeMaster.Name as EmployeeName,EmployeeMaster.EffectiveRate ,ProductMaster.ProductName,MyDailyWorkReport.Typeofwork,PageVersionTbl.VersionNo, case when (MyDailyWorkReport.Typeofwork='1') then 'Developer' else  (case when (MyDailyWorkReport.Typeofwork='2') then 'Tester' else (case when (MyDailyWorkReport.Typeofwork='3') then 'Supervisor' else '' End) End   )  End  as Statuslabel, case when (MyDailyWorkReport.WorkDone='0' or MyDailyWorkReport.WorkDone IS NULL) then 'Pending' else 'Completed' End as TodayStatus from MyDailyWorkReport  left outer join ProductMaster on ProductMaster.ProductId=MyDailyWorkReport.ProductId left outer join VersionInfoMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId left outer join WebsiteMaster on WebsiteMaster.VersionInfoId=VersionInfoMaster.VersionInfoId left outer join   WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.ID left outer join   MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId left outer join PageMaster on PageMaster.PageId=MyDailyWorkReport.PageId left outer join   PageWorkTbl on MyDailyWorkReport.PageWorkTblId=PageWorkTbl.Id  left outer join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId     left outer join  EmployeeMaster on EmployeeMaster.Id=MyDailyWorkReport.EmployeeId   " +
            "   where MyDailyWorkReport.ID=" + Session["lbl_shortlist"] + "  Order By MyDailyWorkReport.ID Desc  ";

            SqlDataAdapter dafff = new SqlDataAdapter(stdd, con);
            DataTable dtfff = new DataTable();
            dafff.Fill(dtfff);

            if (dtfff.Rows.Count > 0)
            {
                Session["ID"] = dtfff.Rows[0]["ID"].ToString();
                try
                {
                    ddlworktitle.SelectedValue = Convert.ToString(dtfff.Rows[0]["ID"]);
                }
                catch (Exception Ex)
                {
                }
                try
                {
                    DropDownList1.SelectedValue = Convert.ToString(dtfff.Rows[0]["Typeofwork"]);
                }
                catch (Exception Ex)
                {
                }

               

               
                
            }



           
        }

        if (e.CommandName == "Attach")
        {
            int lblmasterId = Convert.ToInt32(e.CommandArgument);
           
            string strpageworkall = "select * from DailyWorkGuideUploadTbl where MyworktblId='" + lblmasterId + "' ";
            strpageworkall = " SELECT          dbo.DailyWorkGuideUploadTbl.MyworktblId AS Expr2, dbo.DailyWorkGuideUploadTbl.WorkRequirementPdfFilename AS Expr3, dbo.DailyWorkGuideUploadTbl.Id AS Expr1, dbo.MyDailyWorkReport.PageId, dbo.MyDailyWorkReport.PageWorkTblId, dbo.DailyWorkGuideUploadTbl.* FROM            dbo.MyDailyWorkReport INNER JOIN " +
                                     "  dbo.DailyWorkGuideUploadTbl ON dbo.MyDailyWorkReport.Id = dbo.DailyWorkGuideUploadTbl.MyworktblId Where  dbo.MyDailyWorkReport.PageId='" + lblmasterId + "'";

            SqlCommand cmdpageworkall = new SqlCommand(strpageworkall, con);
            DataTable dtpageworkall = new DataTable();
            SqlDataAdapter adppageworkall = new SqlDataAdapter(cmdpageworkall);
            adppageworkall.Fill(dtpageworkall);

            if (dtpageworkall.Rows.Count > 0)
            {
                GridView3.DataSource = dtpageworkall;
                GridView3.DataBind();
            }


         //   ModalPopupExtender1.Show();
        }
    }

    protected void linkdow1dailywork_Click(object sender, EventArgs e)
    {

        GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;

        int rinrow = row.RowIndex;

        int data = Convert.ToInt32(GridView3.DataKeys[rinrow].Value);

        string strcount;
        strcount = " Select * From DailyWorkGuideUploadTbl where id='" + data + "' ";
        SqlCommand cmdcount = new SqlCommand(strcount, con);
        DataTable dtcount = new DataTable();
        SqlDataAdapter adpcount = new SqlDataAdapter(cmdcount);
        adpcount.Fill(dtcount);

        if (dtcount.Rows.Count > 0)
        {

            string fnname = "";
            if (dtcount.Rows[0]["WorkRequirementPdfFilename"].ToString() != "")
            {
                fnname = dtcount.Rows[0]["WorkRequirementPdfFilename"].ToString();
            }
            else
            {
                fnname = dtcount.Rows[0]["WorkRequirementAudioFileName"].ToString();

            }


            string despath = Server.MapPath("~\\Attach\\") + fnname.ToString();
            //FileInfo file = new FileInfo(despath);
            //despath = "~\\Uploads\\" + fnname.ToString();
            despath = "http://license.busiwiz.com/Attachment/" + fnname.ToString();


            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + despath + "');", true);//'" + "Attachment/" + fnname.ToString() + "'
        
        }






    }
    protected void filltodayhourDEV()
    {
        // string strcln = " SELECT * from MyDailyWorkReport where EmployeeId='" + ddlemployee.SelectedValue + "' and workallocationdate='" + txttargetdatedeve.Text + "'  ";
      



    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    protected void GridView2_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        fillgrid();
    }
    public string sortOrder
    {
        get
        {
            if (ViewState["sortOrder"].ToString() == "desc")
            {
                ViewState["sortOrder"] = "asc";
            }
            else
            {
                ViewState["sortOrder"] = "desc";
            }

            return ViewState["sortOrder"].ToString();
        }
        set
        {
            ViewState["sortOrder"] = value;
        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lblmasterId = (Label)e.Row.FindControl("lblmasterId");
            Label lblworktypeid = (Label)e.Row.FindControl("lblworktypeid");
           
            Label lblbudhrmaintask = (Label)e.Row.FindControl("lblbudhrmaintask");
            Label lblactualhrmaintask = (Label)e.Row.FindControl("lblactualhrmaintask");
            Label lblmaintaskstatus = (Label)e.Row.FindControl("lblmaintaskstatus");
            LinkButton LinkButton2Show = (LinkButton)e.Row.FindControl("LinkButton2Show");
            LinkButton LinkButton5Edit = (LinkButton)e.Row.FindControl("LinkButton5Edit");
            Label lblwebsitenameWorkDone = (Label)e.Row.FindControl("lblwebsitenameWorkDone");

            Label lbl_noattach = (Label)e.Row.FindControl("lbl_noattach");
            Label lbl_workid = (Label)e.Row.FindControl("lbl_workid");

            ImageButton imageBTN_IWork = (ImageButton)e.Row.FindControl("imageBTN_IWork");
            if (lblwebsitenameWorkDone.Text == "True")
            {
                lblwebsitenameWorkDone.Text = "Completed";
            }
            else
            {
                lblwebsitenameWorkDone.Text = "Pending";
            }
            //**********************************************--------------------------------
            //string strpageworkall = "select * from DailyWorkGuideUploadTbl where MyWorktblid='" + lbl_workid.Text + "' ";

            //SqlCommand cmdpageworkall = new SqlCommand(strpageworkall, con);
            //DataTable dtpageworkall = new DataTable();
            //SqlDataAdapter adppageworkall = new SqlDataAdapter(cmdpageworkall);
            //adppageworkall.Fill(dtpageworkall);

            //if (dtpageworkall.Rows.Count > 0)
            //{
            //    imageBTN_IWork.Visible = true;
            //    lbl_noattach.Visible = false;
            //}
            //else
            //{
            //    imageBTN_IWork.Visible = false;
            //    lbl_noattach.Visible = true;
            //}


            string strcln = " SELECT  * from PageWorkTbl where Id='" + lblmasterId.Text + "' ";
            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);

            if (dtcln.Rows.Count > 0)
            {

                if (lblworktypeid.Text == "1")
                {
                    lblbudhrmaintask.Text = dtcln.Rows[0]["BudgetedHourDevelopment"].ToString();
                    lblmaintaskstatus.Text = dtcln.Rows[0]["DevelopmentDone"].ToString();



                }
                if (lblworktypeid.Text == "2")
                {
                    lblbudhrmaintask.Text = dtcln.Rows[0]["BudgetedHourTesting"].ToString();
                    lblmaintaskstatus.Text = dtcln.Rows[0]["TestingDone"].ToString();

                }
                if (lblworktypeid.Text == "3")
                {
                    lblbudhrmaintask.Text = dtcln.Rows[0]["BudgetedHourSupervisorChecking"].ToString();
                    lblmaintaskstatus.Text = dtcln.Rows[0]["SupervisorCheckingDone"].ToString();

                }
                if (lblmaintaskstatus.Text == "True")
                {
                    lblmaintaskstatus.Text = "Completed";
                    //LinkButton2Show.Visible = true;
                    //LinkButton5Edit.Visible = false;
                }
                else
                {
                    lblmaintaskstatus.Text = "Pending";
                    //LinkButton2Show.Visible = false;
                    //LinkButton5Edit.Visible = true;
                }
               
                string strhrspent = " select * from MyDailyWorkReport  where PageWorkTblId='" + lblmasterId.Text + "'  and MyDailyWorkReport.Typeofwork='" + lblworktypeid.Text + "' ";
                SqlCommand cmdhrspent = new SqlCommand(strhrspent, con);
                DataTable dthrspent = new DataTable();
                SqlDataAdapter adphrspent = new SqlDataAdapter(cmdhrspent);
                adphrspent.Fill(dthrspent);

                if (dthrspent.Rows.Count > 0)
                {

                    string checkwork = dthrspent.Rows[0]["WorkDone"].ToString();

                    if (checkwork == "False")
                    {

                        
                    }
                    else
                    {

                       
                    }


                    int totalhr = 0;
                    int totalminute = 0;
                    string FinalTime = "";
                    foreach (DataRow dr in dthrspent.Rows)
                    {
                        string time = "";
                        string temp12 = "";
                        string temp123 = "";
                        string outdifftime = "";

                        time = dr["HourSpent"].ToString();
                        if (time != "")
                        {
                            outdifftime = Convert.ToDateTime(time).ToString("HH:mm");

                            temp12 = Convert.ToDateTime(time).ToString("HH");
                            temp123 = Convert.ToDateTime(time).ToString("mm");

                            int main1 = Convert.ToInt32(temp12);
                            int main2 = Convert.ToInt32(temp123);

                            totalhr += main1;
                            totalminute += main2;

                            FinalTime = totalhr + ":" + totalminute;

                            lblactualhrmaintask.Text = FinalTime.ToString();
                        }

                    }

                  
                }

            }


        }
      
    }

    
   

    protected void ddlemployee_SelectedIndexChangedGrid(object sender, EventArgs e)
    {
      
        DataTable dt_s = new DataTable();
        for (int i = 0; i < GridView2.Rows.Count; i++)
        {

            TextBox txtNewtargetdatedeve = (TextBox)GridView2.Rows[i].FindControl("txtNewtargetdatedeve");
            Label lbl_totalhrsforday = (Label)GridView2.Rows[i].FindControl("lbl_totalhrsforday");
            lbl_totalhrsforday.Text = "";
            if (txtNewtargetdatedeve.Text != "" || txtNewtargetdatedeve.Text != null)
            {
                try
                {
                    string strcln = " select sum(datepart(hour,convert(datetime,budgetedhour)))  AS TotalHours,sum(datepart(minute,convert(datetime,budgetedhour))) AS TotalMinutes from MyDailyWorkReport where EmployeeId='" + ddl_empsearch.SelectedValue + "' and workallocationdate='" + txtNewtargetdatedeve.Text + "'";
                    SqlCommand cmdcln = new SqlCommand(strcln, con);
                    DataTable dtcln = new DataTable();
                    SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
                    adpcln.Fill(dtcln);

                    if (dtcln.Rows.Count > 0)
                    {
                        string TotalHour = dtcln.Rows[0]["TotalHours"].ToString();
                        string TotalMinutes = dtcln.Rows[0]["TotalMinutes"].ToString();
                        lbl_totalhrsforday.Text = TotalHour + ":" + TotalMinutes;
                        if (TotalHour == "" && TotalMinutes == "")
                        {
                            TotalHour = "0";
                            TotalMinutes = "0";

                        }

                        lbl_totalhours.Text = TotalHour + ":" + TotalMinutes;
                    }
                    else
                    {
                        lbl_totalhours.Text = "0.0";
                        lbl_totalhrsforday.Text = "0.0";

                    }
                }
                catch (Exception ex)
                {
                    lbl_totalhours.Text = "0.0";
                    lbl_totalhrsforday.Text = "0.0";
                }


            }

        }
    }

    protected void filltodayhour()
    {
        try
        {
            // string strcln = " SELECT * from MyDailyWorkReport where EmployeeId='" + ddlemployee.SelectedValue + "' and workallocationdate='" + txttargetdatedeve.Text + "'  ";
            string strcln = " select sum(datepart(hour,convert(datetime,budgetedhour)))  AS TotalHours,sum(datepart(minute,convert(datetime,budgetedhour))) AS TotalMinutes from MyDailyWorkReport where EmployeeId='" + ddlemployee.SelectedValue + "' and workallocationdate='" + txttargetdatedeve.Text + "'";
            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);

            if (dtcln.Rows.Count > 0)
            {
                string TotalHour = dtcln.Rows[0]["TotalHours"].ToString();
                string TotalMinutes = dtcln.Rows[0]["TotalMinutes"].ToString();

                if (TotalHour == "" && TotalMinutes == "")
                {
                    TotalHour = "0";
                    TotalMinutes = "0";

                }

                lbltotalworkallocatedtoday.Text = TotalHour + ":" + TotalMinutes;
            }
            else
            {
                lbltotalworkallocatedtoday.Text = "";

            }


        }
        catch (Exception ex)
        {
        }
               
    }

    protected void addnewpanel_Click(object sender, EventArgs e)
    {
        pnladdnew.Visible = true;
        Panel1.Visible = false;
        Upradio_SelectedIndexChanged(sender, e);
       // ModalPopupExtender1.Hide();  
    }

    protected void addnewpanel_Click1(object sender, EventArgs e)
    {
        EmployeeFilter();       
        fillgrid();
        pnladdnew.Visible = false;
        Panel1.Visible = true;       
    }   
    //protected void txttargetdatedeve_TextChanged(object sender, EventArgs e)
    //{
    //if (txttargetdatedeve.Text.ToString() == "")
    //{
    //    lblmsg.Visible = true;
    //    lblmsg.Text = "Please Select date";

    //}
    //else
    //{
    //    fillpagename();
    //    filltodayhour();

    //}
    //}
    protected void ddlworktitle_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlworktitle.SelectedIndex > 0)
        {
            fillmasterhour();

            tilldatehourcalculation();
            employeerequestedhour();

            string strcln = " SELECT * from PageWorkTbl where Id='" + ddlworktitle.SelectedValue + "' ";

            strcln = " SELECT        TOP (1) dbo.PageVersionTbl.VersionName, dbo.PageVersionTbl.VersionNo, dbo.PageVersionTbl.PageName, dbo.PageVersionTbl.Date, dbo.PageVersionTbl.Active,  dbo.PageVersionTbl.FolderName, dbo.PageWorkTbl.Id, dbo.EmployeeMaster.Name FROM            dbo.PageWorkTbl INNER JOIN dbo.PageVersionTbl ON dbo.PageWorkTbl.PageVersionTblId = dbo.PageVersionTbl.Id INNER JOIN dbo.EmployeeMaster ON dbo.PageWorkTbl.EpmloyeeID_AssignedDeveloper = dbo.EmployeeMaster.Id " +
                            " Where PageWorkTbl.id='" + ddlworktitle.SelectedValue + "' ";
            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);

            if (dtcln.Rows.Count > 0)
            {
              //  txtworktobedone.Text = dtcln.Rows[0]["WorkRequirementTitle"].ToString();
                
                if (DropDownList1.SelectedValue == "1")
                {
                    txtworktobedone.Text = "Development work for " + dtcln.Rows[0]["PageName"].ToString() + "-" + dtcln.Rows[0]["VersionNo"].ToString() + "";
                }
                if (DropDownList1.SelectedValue == "2")
                {
                    txtworktobedone.Text = "Testing of " + dtcln.Rows[0]["PageName"].ToString() + "-" + dtcln.Rows[0]["VersionNo"].ToString() + "completed by  " + dtcln.Rows[0]["Name"].ToString() + " ";
                }
                if (DropDownList1.SelectedValue == "3")
                {
                    txtworktobedone.Text = "Demo and Certification of " + dtcln.Rows[0]["PageName"].ToString() + "-" + dtcln.Rows[0]["VersionNo"].ToString() + "";
                }
                if (DropDownList1.SelectedValue == "4")
                {
                    txtworktobedone.Text = "";
                }
                if (DropDownList1.SelectedValue == "5")
                {
                    txtworktobedone.Text = "";
                }
            }


        }
       

    }

    protected void costofdeveloper()
    {
        string str = "select * from EmployeeMaster where Id='" + ddlemployee.SelectedValue + "'";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable da = new DataTable();
        adp.Fill(da);

        if (da.Rows.Count > 0)
        {
            ViewState["EffectiveRate1"] = da.Rows[0]["EffectiveRate"].ToString();

            lblmasterbudhrrate.Text = ViewState["EffectiveRate1"].ToString();
            lbltodayhrrate.Text = ViewState["EffectiveRate1"].ToString();
            lblactualrate.Text = ViewState["EffectiveRate1"].ToString();
            lblemprequestedrate.Text = ViewState["EffectiveRate1"].ToString();
        }
        else
        {
            lblmasterbudhrrate.Text = "0";
            lbltodayhrrate.Text = "0";
            lblactualrate.Text = "0";
            lblemprequestedrate.Text = "0";
        } 


       

    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        fillpagename();
        filltodayhour();
        Session["EmpType"] = DropDownList1.SelectedIndex;

        
    }

    protected void fillmasterhour()
    {
        string strmasterhr = " SELECT * from PageWorkTbl where Id='" + ddlworktitle.SelectedValue + "' ";
        SqlCommand cmdmasterhr = new SqlCommand(strmasterhr, con);
        DataTable dtmasterhr = new DataTable();
        SqlDataAdapter adpmasterhr = new SqlDataAdapter(cmdmasterhr);
        adpmasterhr.Fill(dtmasterhr);

        if (dtmasterhr.Rows.Count > 0)
        {
            if (DropDownList1.SelectedValue == "1")
            {
                lblmasterbudgetedhour.Text = dtmasterhr.Rows[0]["BudgetedHourDevelopment"].ToString();
                txttodaybudgetdhour.Text = lblmasterbudgetedhour.Text;
                
            }
            if (DropDownList1.SelectedValue == "2")
            {
                lblmasterbudgetedhour.Text = dtmasterhr.Rows[0]["BudgetedHourTesting"].ToString();
                txttodaybudgetdhour.Text = lblmasterbudgetedhour.Text;
            }
            if (DropDownList1.SelectedValue == "3")
            {
                lblmasterbudgetedhour.Text = dtmasterhr.Rows[0]["BudgetedHourSupervisorChecking"].ToString();

                txttodaybudgetdhour.Text = lblmasterbudgetedhour.Text;
            }


        }
        else
        {
            lblmasterbudgetedhour.Text = "00:00";
            txttodaybudgetdhour.Text = "00:00";
        }

       

        if (lblmasterbudgetedhour.Text.Length > 0)
        {
            string time = "";
            string outdifftime = "";
            string temp12 = "";
            string temp123 = "";
            TimeSpan t4 = TimeSpan.Parse(lblmasterbudgetedhour.Text);
            time = t4.ToString();


            outdifftime = Convert.ToDateTime(time).ToString("HH:mm");
            temp12 = Convert.ToDateTime(time).ToString("HH");
            temp123 = Convert.ToDateTime(time).ToString("mm");

            double main1 = Convert.ToDouble(temp12);
            double main2 = Convert.ToDouble(temp123);

            double cost1 = main1 * Convert.ToDouble(lblmasterbudhrrate.Text);

            double cost2 = ((main2 / 60) * (Convert.ToDouble(lblmasterbudhrrate.Text)));
            double FinalCost = cost1 + cost2;
            FinalCost = Math.Round(FinalCost, 2);
          
            lblmasterbudcost.Text = FinalCost.ToString();
        }

        todaysratecostcalculation();

    }

    protected void todaysratecostcalculation()
    {
        if (txttodaybudgetdhour.Text.Length > 0)
        {
            string time = "";
            string outdifftime = "";
            string temp12 = "";
            string temp123 = "";
            TimeSpan t4 = TimeSpan.Parse(txttodaybudgetdhour.Text);
            time = t4.ToString();


            outdifftime = Convert.ToDateTime(time).ToString("HH:mm");
            temp12 = Convert.ToDateTime(time).ToString("HH");
            temp123 = Convert.ToDateTime(time).ToString("mm");

            double main1 = Convert.ToDouble(temp12);
            double main2 = Convert.ToDouble(temp123);

            double cost1 = main1 * Convert.ToDouble(lbltodayhrrate.Text);

            double cost2 = ((main2 / 60) * (Convert.ToDouble(lbltodayhrrate.Text)));

            double FinalCost = cost1 + cost2;
            FinalCost = Math.Round(FinalCost, 2);

            lbltodayscost.Text = FinalCost.ToString();
        }
        
    }
    protected void txttodaybudgetdhour_TextChanged(object sender, EventArgs e)
    {
        todaysratecostcalculation();
    }

    protected void tilldatehourcalculation()
    {
        string strhrspent = " select * from MyDailyWorkReport  where PageWorkTblId='" + ddlworktitle.SelectedValue + "'  and MyDailyWorkReport.Typeofwork='" + DropDownList1.SelectedValue + "' ";
        SqlCommand cmdhrspent = new SqlCommand(strhrspent, con);
        DataTable dthrspent = new DataTable();
        SqlDataAdapter adphrspent = new SqlDataAdapter(cmdhrspent);
        adphrspent.Fill(dthrspent);

        if (dthrspent.Rows.Count > 0)
        {
            int totalhr = 0;
            int totalminute = 0;
            string FinalTime = "";
            foreach (DataRow dr in dthrspent.Rows)
            {
                string time = "";
                string temp12 = "";
                string temp123 = "";
                string outdifftime = "";

                time = dr["HourSpent"].ToString();
                if (time != "")
                {

                    outdifftime = Convert.ToDateTime(time).ToString("HH:mm");

                    temp12 = Convert.ToDateTime(time).ToString("HH");
                    temp123 = Convert.ToDateTime(time).ToString("mm");

                    int main1 = Convert.ToInt32(temp12);
                    int main2 = Convert.ToInt32(temp123);

                    totalhr += main1;
                    totalminute += main2;

                    FinalTime = totalhr + ":" + totalminute;

                    lblactualhourtillyesterday.Text = FinalTime.ToString();
                }
                

            }

            double cost1 = totalhr * Convert.ToDouble(lbltodayhrrate.Text);

            double cost2 = ((totalminute * 60) * (Convert.ToDouble(lbltodayhrrate.Text)));

            double FinalCost = cost1 + cost2;
            FinalCost = Math.Round(FinalCost, 2);

            lblactualcost.Text = FinalCost.ToString();
        }
    }
    protected void employeerequestedhour()
    {


        string strhrspent = "  select * from MyDailyWorkReport where PageWorkTblId='" + ddlworktitle.SelectedValue + "' and WorkDone='0' order by Id desc ";
        SqlCommand cmdhrspent = new SqlCommand(strhrspent, con);
        DataTable dthrspent = new DataTable();
        SqlDataAdapter adphrspent = new SqlDataAdapter(cmdhrspent);
        adphrspent.Fill(dthrspent);

        if (dthrspent.Rows.Count > 0)
        {
            lblemprequestedhour.Text = dthrspent.Rows[0]["EmpRequestHour"].ToString();
           // pnlrequestedhr.Visible =true ;
        }
        else
        {
            lblemprequestedhour.Text = "00:00";
           // pnlrequestedhr.Visible = false;
        }

        if (lblemprequestedhour.Text.Length > 0)
        {
            string time = "";
            string outdifftime = "";
            string temp12 = "";
            string temp123 = "";
            TimeSpan t4 = TimeSpan.Parse(lblemprequestedhour.Text);
            time = t4.ToString();


            outdifftime = Convert.ToDateTime(time).ToString("HH:mm");
            temp12 = Convert.ToDateTime(time).ToString("HH");
            temp123 = Convert.ToDateTime(time).ToString("mm");

            double main1 = Convert.ToDouble(temp12);
            double main2 = Convert.ToDouble(temp123);

            double cost1 = main1 * Convert.ToDouble(lblemprequestedrate.Text);

            double cost2 = ((main2 / 60) * (Convert.ToDouble(lblemprequestedrate.Text)));
            double FinalCost = cost1 + cost2;
            FinalCost = Math.Round(FinalCost, 2);

            lblrequestedcost.Text = FinalCost.ToString();
        }

    }
    protected void FillProduct()
    {


        string strcln = " SELECT  distinct  VersionInfoMaster.VersionInfoId,WebsiteSection.WebsiteSectionId, MasterPageMaster.MasterPageId, ProductMaster.ProductName + '-' +   VersionInfoMaster.VersionInfoName  + ' - ' + WebsiteMaster.WebsiteName  + ' - ' +  WebsiteSection.SectionName   + ' - ' +  MasterPageMaster.MasterPageName as productversion  FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId inner join WebsiteMaster on WebsiteMaster.VersionInfoId=VersionInfoMaster.VersionInfoId inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.ID inner join MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId where ProductMaster.ClientMasterId='" + Session["ClientId"] + "' and VersionInfoMaster.Active ='True' and ProductDetail.Active='1' order  by productversion ";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);

        ddlproductname.DataSource = dtcln;
        ddlproductname.DataValueField = "MasterPageId";
        ddlproductname.DataTextField = "productversion";
        ddlproductname.DataBind();


    }
    protected void filltype()
    {

        ddlpage.Items.Clear();
        if (ddlproductname.SelectedIndex >-1)
        {

            string strcln = "";
            string str1 = "";
            string str2 = "";


            strcln = "SELECT distinct MainMenuMaster.*,PageMaster.PageId,PageMaster.PageName +'-'+PageMaster.PageTitle+'-'+MainMenuMaster.MainMenuName as Page_Name from   PageMaster    inner  join  MainMenuMaster  on PageMaster.MainMenuId=MainMenuMaster.MainMenuId   left outer join SubMenuMaster on SubMenuMaster.SubMenuId=PageMaster.SubMenuId   inner join MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id   inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId 	inner join WebsiteMaster   on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId inner join VersionInfoMaster    on VersionInfoMaster.VersionInfoId = WebsiteMaster.VersionInfoId  inner join ProductMaster   on VersionInfoMaster.ProductId=ProductMaster.ProductId   where  pageMaster.Active=1 and  ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "' and MasterPageMaster.MasterPageId='" + ddlproductname.SelectedValue + "'   and ( MainMenuMaster.MainMenuName  <> ''  and  PageMaster.PageTitle  <> ''  )   ";

           

            string orderby = "order by Page_Name";

            string finalstr = strcln + orderby;

            SqlCommand cmdcln = new SqlCommand(finalstr, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);

            ddlpage.DataSource = dtcln;
            ddlpage.DataValueField = "PageId";
            ddlpage.DataTextField = "Page_Name";
            ddlpage.DataBind();
            ddlpage.Items.Insert(0, "-Select-");
            ddlpage.Items[0].Value = "0";

        }
    }
    protected void ddlproductname_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillproductid();
        filltype();
        fillpagename();
        Session["ProductSel"] = ddlproductname.SelectedIndex;  
    }
    protected void ddlpage_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillpagename();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {

        string ext = "";
        string[] validFileTypes = { "bmp", "gif", "png", ".doc", "jpg", "jpeg", "doc", "xls", "xlsx", "docx", "aspx", "cs", "zip", "pdf", "PDF", "wma", "html", "css", "rar", "zip", "rpt" };
        string[] validFileTypes1 = { "MP3", "MP4", "mp3", ".mp3", "mp3", ".mp4", ".MP3", ".m4a", "m4a", ".M4A", ".wav", "wav" };
        bool isValidFile = false;
        if (Upradio.SelectedValue == "1")
        {

            ext = System.IO.Path.GetExtension(fileuploadaudio.PostedFile.FileName);
            for (int i = 0; i < validFileTypes1.Length; i++)
            {

                if (ext == "." + validFileTypes1[i])
                {

                    isValidFile = true;
                    lblmsg.Text = "";
                    break;

                }

            }
        }
        else
        {
            if (fileuploadadattachment.HasFile == true)
            {

                ext = System.IO.Path.GetExtension(fileuploadadattachment.PostedFile.FileName);
                for (int i = 0; i < validFileTypes.Length; i++)
                {

                    if (ext == "." + validFileTypes[i])
                    {

                        isValidFile = true;
                        lblmsg.Text = "";
                        break;

                    }

                }
            }
        }


        if (!isValidFile)
        {

            lblmsg.Visible = true;
            if (Upradio.SelectedValue == "1")
            {
                lblmsg.Text = "Invalid File. Please upload a File with extension " +

                               string.Join(",", validFileTypes1);
            }
            else
            {
                lblmsg.Text = "Invalid File. Please upload a File with extension " +

                              string.Join(",", validFileTypes);
            }

        }

        else
        {

            String filename = "";
            string audiofile = "";
            //PnlFileAttachLbl.Visible = true;
            if (fileuploadadattachment.HasFile || fileuploadaudio.HasFile)
            {
                if (fileuploadadattachment.HasFile)
                {
                    filename = fileuploadadattachment.FileName;
                    fileuploadadattachment.PostedFile.SaveAs(Server.MapPath("~\\Attach\\") + filename);
                    fileuploadadattachment.PostedFile.SaveAs(Server.MapPath("~\\Attachment\\") + filename);

                    //fileuploadadattachment.PostedFile.SaveAs(Server.MapPath("http://license.busiwiz.com/Attach/") + filename);
                    //fileuploadadattachment.PostedFile.SaveAs(Server.MapPath("http://license.busiwiz.com/Attachment/") + filename);
                }
                if (fileuploadaudio.HasFile)
                {
                    audiofile = fileuploadaudio.FileName;
                    fileuploadaudio.PostedFile.SaveAs(Server.MapPath("~\\Clientadmin\\Attach\\") + audiofile);
                    fileuploadaudio.PostedFile.SaveAs(Server.MapPath("~\\Clientadmin\\Attachment\\") + audiofile);
                    //fileuploadadattachment.PostedFile.SaveAs(Server.MapPath("http://license.busiwiz.com/Attach/") + filename);
                    //fileuploadadattachment.PostedFile.SaveAs(Server.MapPath("http://license.busiwiz.com/Attachment/") + filename);
                    
                }
                //hdnFileName.Value = filename;
                DataTable dt = new DataTable();
                if (Session["GridFileAttach1"] == null)
                {
                    DataColumn dtcom2 = new DataColumn();
                    dtcom2.DataType = System.Type.GetType("System.String");
                    dtcom2.ColumnName = "PDFURL";
                    dtcom2.ReadOnly = false;
                    dtcom2.Unique = false;
                    dtcom2.AllowDBNull = true;
                    dt.Columns.Add(dtcom2);

                    DataColumn dtcom3 = new DataColumn();
                    dtcom3.DataType = System.Type.GetType("System.String");
                    dtcom3.ColumnName = "Title";
                    dtcom3.ReadOnly = false;
                    dtcom3.Unique = false;
                    dtcom3.AllowDBNull = true;
                    dt.Columns.Add(dtcom3);

                    DataColumn dtcom4 = new DataColumn();
                    dtcom4.DataType = System.Type.GetType("System.String");
                    dtcom4.ColumnName = "AudioURL";
                    dtcom4.ReadOnly = false;
                    dtcom4.Unique = false;
                    dtcom4.AllowDBNull = true;
                    dt.Columns.Add(dtcom4);

                }
                else
                {
                    dt = (DataTable)Session["GridFileAttach1"];
                }
                DataRow dtrow = dt.NewRow();
                dtrow["PDFURL"] = filename;
                dtrow["Title"] = txttitlename.Text;
                dtrow["AudioURL"] = audiofile;

                // dtrow["FileNameChanged"] = hdnFileName.Value;
                dt.Rows.Add(dtrow);
                Session["GridFileAttach1"] = dt;
                gridFileAttach.DataSource = dt;


                gridFileAttach.DataBind();
                txttitlename.Text = "";
            }
            else
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Please Attach File to Upload.";
                return;
            }
        }
    }

    protected void Button2_ClickAdd(object sender, EventArgs e)
    {

        string ext = "";
        string[] validFileTypes = { "bmp", "gif", "png", "jpg", "jpeg", "doc", "xls", "xlsx", "doc", "docx", "aspx", "cs", "zip", "pdf", "PDF", "MP3", "MP4", "wma", "html", "css", "rar", "zip", "rpt", "MP3", "mp3", "Mp3", "mP3", "MP4", "mp4", "mP4", "Mp4", "pdf", "PDF", "wma", "html", "css", "rar", "zip", "rpt", "xls", "xlsx" };
        string[] validFileTypes1 = { "bmp", "gif", "png", "jpg", "jpeg", "doc", "xls", "xlsx", "doc", "docx", "aspx", "cs", "zip", "pdf", "PDF", "MP3", "MP4", "wma", "html", "css", "rar", "zip", "rpt", "MP3", "mp3", "Mp3", "mP3", "MP4", "mp4", "mP4", "Mp4", "pdf", "PDF", "wma", "html", "css", "rar", "zip", "rpt", "xls", "xlsx" };
        bool isValidFile = false;
        if (Upradio.SelectedValue == "1")
        {

            ext = System.IO.Path.GetExtension(fileupload2audio.PostedFile.FileName);
            for (int i = 0; i < validFileTypes1.Length; i++)
            {

                if (ext == "." + validFileTypes1[i])
                {

                    isValidFile = true;
                    lblmsg.Text = "";
                    break;

                }

            }
        }
        else
        {
            if (fileupload1other.HasFile == true)
            {

                ext = System.IO.Path.GetExtension(fileupload1other.PostedFile.FileName);
                for (int i = 0; i < validFileTypes.Length; i++)
                {

                    if (ext == "." + validFileTypes[i])
                    {

                        isValidFile = true;
                        lblmsg.Text = "";
                        break;

                    }

                }
            }
        }


        if (!isValidFile)
        {

            lblmsg.Visible = true;
            if (Upradio.SelectedValue == "1")
            {
                lblmsg.Text = "Invalid File. Please upload a File with extension " +

                               string.Join(",", validFileTypes1);
            }
            else
            {
                lblmsg.Text = "Invalid File. Please upload a File with extension " +

                              string.Join(",", validFileTypes);
            }

        }

        else
        {

            String filename = "";
            string audiofile = "";
            //PnlFileAttachLbl.Visible = true;
            if (fileupload1other.HasFile || fileupload2audio.HasFile)
            {
                if (fileupload1other.HasFile)
                {
                    filename = fileupload1other.FileName;
                   fileupload1other.PostedFile.SaveAs(Server.MapPath("~\\Attach\\") + filename);
                    fileupload1other.PostedFile.SaveAs(Server.MapPath("~\\Attachment\\") + filename);
                 //   fileuploadadattachment.PostedFile.SaveAs(Server.MapPath("http://license.busiwiz.com/Attachment/") + filename);
                  //  fileuploadadattachment.PostedFile.SaveAs(Server.MapPath("http://license.busiwiz.com/Attach/") + filename);

                }
                if (fileupload2audio.HasFile)
                {
                    audiofile = fileupload2audio.FileName;
                    fileupload2audio.PostedFile.SaveAs(Server.MapPath("~\\Attach\\") + audiofile);
                        fileupload2audio.PostedFile.SaveAs(Server.MapPath("~\\Attachment\\") + audiofile);
                    //    fileuploadadattachment.PostedFile.SaveAs(Server.MapPath("http://license.busiwiz.com/Attachment/") + filename);
                      //  fileuploadadattachment.PostedFile.SaveAs(Server.MapPath("http://license.busiwiz.com/Attach/") + filename);
                }
                //hdnFileName.Value = filename;
                DataTable dt = new DataTable();
                if (Session["GridFileAttach1n"] == null)
                {
                    DataColumn dtcom2 = new DataColumn();
                    dtcom2.DataType = System.Type.GetType("System.String");
                    dtcom2.ColumnName = "PDFURL";
                    dtcom2.ReadOnly = false;
                    dtcom2.Unique = false;
                    dtcom2.AllowDBNull = true;
                    dt.Columns.Add(dtcom2);

                    DataColumn dtcom3 = new DataColumn();
                    dtcom3.DataType = System.Type.GetType("System.String");
                    dtcom3.ColumnName = "Title";
                    dtcom3.ReadOnly = false;
                    dtcom3.Unique = false;
                    dtcom3.AllowDBNull = true;
                    dt.Columns.Add(dtcom3);

                    DataColumn dtcom4 = new DataColumn();
                    dtcom4.DataType = System.Type.GetType("System.String");
                    dtcom4.ColumnName = "AudioURL";
                    dtcom4.ReadOnly = false;
                    dtcom4.Unique = false;
                    dtcom4.AllowDBNull = true;
                    dt.Columns.Add(dtcom4);

                }
                else
                {
                    dt = (DataTable)Session["GridFileAttach1n"];
                }
                DataRow dtrow = dt.NewRow();
                dtrow["PDFURL"] = filename;
                dtrow["Title"] = TextBox2.Text;
                dtrow["AudioURL"] = audiofile;

                // dtrow["FileNameChanged"] = hdnFileName.Value;
                dt.Rows.Add(dtrow);
                Session["GridFileAttach1n"] = dt;
                GridView1Attach.DataSource = dt;


                GridView1Attach.DataBind();
               
            }
            else
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Please Attach File to Upload.";
                return;
            }
        }
      //  ModalPopupExtender1.Show(); 
    }
    protected void Upradio_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Upradio.SelectedValue == "1")
        {
            pnladio.Visible = true;
            pnlpdfup.Visible = false;
        }
        else
        {
            pnladio.Visible = false;
            pnlpdfup.Visible = true;
        }

        if (RadioButtonList1.SelectedValue == "1")
        {
            Panel5.Visible = true;
            Panel4.Visible = false;
         //   ModalPopupExtender1.Show(); 
        }
        else
        {
            Panel5.Visible = false;
            Panel4.Visible = true;
           // ModalPopupExtender1.Show();
        }
    }
    protected void gridFileAttach_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete1")
        {
            gridFileAttach.SelectedIndex = Convert.ToInt32(e.CommandArgument);

            DataTable dt = new DataTable();
            if (Session["GridFileAttach1"] != null)
            {
                if (gridFileAttach.Rows.Count > 0)
                {
                    dt = (DataTable)Session["GridFileAttach1"];

                    dt.Rows.Remove(dt.Rows[gridFileAttach.SelectedIndex]);


                    gridFileAttach.DataSource = dt;
                    gridFileAttach.DataBind();
                    Session["GridFileAttach1"] = dt;
                }
            }

        }
    }

    protected void gridFileAttach_RowCommandn(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete1")
        {
            GridView1Attach.SelectedIndex = Convert.ToInt32(e.CommandArgument);

            DataTable dt = new DataTable();
            if (Session["GridFileAttach1n"] != null)
            {
                if (GridView1Attach.Rows.Count > 0)
                {
                    dt = (DataTable)Session["GridFileAttach1n"];

                    dt.Rows.Remove(dt.Rows[GridView1Attach.SelectedIndex]);


                    GridView1Attach.DataSource = dt;
                    GridView1Attach.DataBind();
                    Session["GridFileAttach1n"] = dt;
                }
            }
           // ModalPopupExtender1.Show(); 
        }
    }
    protected void chkupload_CheckedChanged(object sender, EventArgs e)
    {
        if (chkupload.Checked == true)
        {
            pnlup.Visible = true;
        }
        else
        {
            pnlup.Visible = false;
        }

    }
    public void ftpfile(string inputfilepath, string filename)
    {
        try
        {
            string strcount = " select WebsiteMaster.* from ProductMaster inner join VersionInfoMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId inner join WebsiteMaster on WebsiteMaster.VersionInfoId=VersionInfoMaster.VersionInfoId where ProductMaster.ProductId='" + Session["ProductId"].ToString() + "'";
            SqlCommand cmdcount = new SqlCommand(strcount, con);
            DataTable dtcount = new DataTable();
            SqlDataAdapter adpcount = new SqlDataAdapter(cmdcount);
            adpcount.Fill(dtcount);
            if (dtcount.Rows.Count > 0)
            {
                string[] separator1 = new string[] { "/" };
                string[] strSplitArr1 = dtcount.Rows[0]["FTPWorkGuideUrl"].ToString().Split(separator1, StringSplitOptions.RemoveEmptyEntries);

                String productno = strSplitArr1[0].ToString();
                string ftpurl = "";

                if (productno == "FTP:" || productno == "ftp:")
                {
                    if (strSplitArr1.Length >= 3)
                    {
                        ftpurl = strSplitArr1[0].ToString() + "//" + strSplitArr1[1].ToString() + ":" + Convert.ToString(dtcount.Rows[0]["FTPWorkGuidePort"]);
                        for (int i = 2; i < strSplitArr1.Length; i++)
                        {
                            ftpurl += "/" + strSplitArr1[i].ToString();
                        }
                    }
                    else
                    {
                        ftpurl = strSplitArr1[0].ToString() + "//" + strSplitArr1[1].ToString() + ":" + Convert.ToString(dtcount.Rows[0]["FTPWorkGuidePort"]);

                    }
                }
                else
                {
                    if (strSplitArr1.Length >= 2)
                    {
                        ftpurl = "ftp://" + strSplitArr1[0].ToString() + ":" + Convert.ToString(dtcount.Rows[0]["FTPWorkGuidePort"]);
                        for (int i = 1; i < strSplitArr1.Length; i++)
                        {
                            ftpurl += "/" + strSplitArr1[i].ToString();
                        }
                    }
                    else
                    {
                        ftpurl = "ftp://" + strSplitArr1[0].ToString() + ":" + Convert.ToString(dtcount.Rows[0]["FTPWorkGuidePort"]);

                    }

                }
                string ftphost = ftpurl + "/" + inputfilepath;
                // string ftphost = Convert.ToString( dtcount.Rows[0]["FTPWorkGuideUrl"]) + "/" + inputfilepath;
                FtpWebRequest FTP = (FtpWebRequest)FtpWebRequest.Create(ftphost);
                FTP.Credentials = new NetworkCredential(Convert.ToString(dtcount.Rows[0]["FTPWorkGuideUserId"]), PageMgmt.Decrypted(Convert.ToString(dtcount.Rows[0]["FTPWorkGuidePW"])));
                FTP.UseBinary = false;
                FTP.KeepAlive = true;
                FTP.UsePassive = true;


                FTP.Method = WebRequestMethods.Ftp.UploadFile;
                FileStream fs = File.OpenRead(filename);
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
                fs.Close();
                Stream ftpstream = FTP.GetRequestStream();
                ftpstream.Write(buffer, 0, buffer.Length);
                ftpstream.Close();

            }
            System.IO.File.Delete(filename);
        }
        catch (Exception ex)
        {
        }

       
    }

    protected void fillproductid()
    {
        string strcln = " SELECT  distinct   ProductMaster.ProductId   FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId inner join WebsiteMaster on WebsiteMaster.VersionInfoId=VersionInfoMaster.VersionInfoId inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.ID inner join MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId where ProductMaster.ClientMasterId='" + Session["ClientId"] + "' and MasterPageMaster.MasterPageId='" + ddlproductname.SelectedValue + "' and VersionInfoMaster.Active ='True' and ProductDetail.Active='1'  ";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);

        if (dtcln.Rows.Count > 0)
        {
            Session["ProductId"] = dtcln.Rows[0]["ProductId"].ToString();
        }
        else
        {
            Session["ProductId"] = "0";
        }
    }
}
