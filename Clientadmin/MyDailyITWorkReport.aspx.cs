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

using System.Net;
using System.IO;
using System.Text;
using System.Globalization;
using System.Net.Mail;
using System.Security.Cryptography;

public partial class MyDailyWorkReport : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    public static SqlConnection epconn;
    protected void Page_Load(object sender, EventArgs e)
    {
        // Session["ClientId"] = "35";
        PageConn pgcon = new PageConn();
        if (!IsPostBack)
        {
            txttargetdatedeve.Text = System.DateTime.Now.ToShortDateString();

            fillemployee();
            costofdeveloper();
            fillpagename();
            fillbudgetdhour();
            maintaskbudhourcost();
            todaytaskbudhourcost();


            //  TextBox1.Text = System.DateTime.Now.ToShortDateString();
            DateTime dm = DateTime.Now;
         //new DateTime(3057, 3, 14).ToString("d");
            //dm = dm.AddMonths(-1);
            TextBox1.Text = Convert.ToString(dm);  
           
            DateTime d = DateTime.Now;
            TextBox2.Text  = Convert.ToString(d);
         
            string s_today = d.ToString("MM/dd/yyyy"); // As String
            TextBox2.Text  = s_today;
            TextBox1.Text = s_today;


            fillemployeeSearch();
            fillproduct();
            filltype();
            fillgrid();



        }
        

    }
    protected void Button1_ClickSearch(object sender, EventArgs e)
    {
       // columndisplay();
        fillgrid();

    }
    protected void filltype()
    {

        ddlpagename.Items.Clear();
        if (ddlwebsite.SelectedIndex > 1)
        {

            string strcln = "";
            strcln = "SELECT distinct MainMenuMaster.*,PageMaster.PageId,PageMaster.PageName +'-'+PageMaster.PageTitle+'-'+MainMenuMaster.MainMenuName+'-'+SubMenuMaster.SubMenuName as Page_Name from   PageMaster    inner  join  MainMenuMaster  on PageMaster.MainMenuId=MainMenuMaster.MainMenuId   left outer join SubMenuMaster on SubMenuMaster.SubMenuId=PageMaster.SubMenuId   inner join MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id   inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId 	inner join WebsiteMaster   on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId inner join VersionInfoMaster    on VersionInfoMaster.VersionInfoId = WebsiteMaster.VersionInfoId  inner join ProductMaster   on VersionInfoMaster.ProductId=ProductMaster.ProductId   where    ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "' and MasterPageMaster.MasterPageId='" + ddlwebsite.SelectedValue + "'   and ( MainMenuMaster.MainMenuName  <> '' and SubMenuMaster.SubMenuName  <> '' and  PageMaster.PageTitle  <> '')   ";

            string orderby = "order by Page_Name";

            string finalstr = strcln + orderby;

            SqlCommand cmdcln = new SqlCommand(finalstr, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);

            ddlpagename.DataSource = dtcln;
            ddlpagename.DataValueField = "PageId";
            ddlpagename.DataTextField = "Page_Name";
            ddlpagename.DataBind();
            ddlpagename.Items.Insert(0, "All");
            ddlpagename.Items[0].Value = "0";

        }
        else
        {
            ddlpagename.DataSource = null;
            ddlpagename.DataValueField = "PageId";
            ddlpagename.DataTextField = "Page_Name";
            ddlpagename.DataBind();
            ddlpagename.Items.Insert(0, "All");
            ddlpagename.Items[0].Value = "0";
        }
    }
    protected void fillemployeeSearch()
    {
        string strcln = " SELECT * from EmployeeMaster where ClientId='" + Session["ClientId"] + "' and Id='" + Session["id"] + "'   ";// and  UserId='" + Session["UserId"] + "'
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);

        ddl_empsearch.DataSource = dtcln;
        ddl_empsearch.DataValueField = "Id";
        ddl_empsearch.DataTextField = "Name";
        ddl_empsearch.DataBind();

        //ddlemployee.Items.Insert(0, "-Select-");
        //ddlemployee.Items[0].Value = "0";
    }
    protected void fillproduct()
    {


        string strcln = "SELECT  distinct  VersionInfoMaster.VersionInfoId,WebsiteSection.WebsiteSectionId, MasterPageMaster.MasterPageId, ProductMaster.ProductName + '-' +   VersionInfoMaster.VersionInfoName  + ' - ' + WebsiteMaster.WebsiteName  + ' - ' +  WebsiteSection.SectionName   + ' - ' +  MasterPageMaster.MasterPageName as productversion  FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId inner join WebsiteMaster on WebsiteMaster.VersionInfoId=VersionInfoMaster.VersionInfoId inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.ID inner join MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId where ProductMaster.ClientMasterId='" + Session["ClientId"] + "' and VersionInfoMaster.Active ='True' and ProductDetail.Active='1' order  by productversion ";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);

        ddlwebsite.DataSource = dtcln;
        ddlwebsite.DataValueField = "MasterPageId";
        ddlwebsite.DataTextField = "productversion";
        ddlwebsite.DataBind();
        ddlwebsite.Items.Insert(0, "All");
        ddlwebsite.Items[0].Value = "0";

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
            "  where  MyDailyWorkReport.PageworkTblid IS NOT NULL AND  MyDailyWorkReport.EmployeeId='" + ddlemployee.SelectedValue + "'";

        if (ddlwebsite.SelectedIndex > 0)
        {
            product = " and MasterPageMaster.MasterPageId='" + ddlwebsite.SelectedValue + "' ";
        }
        //if (ddltodaytaskstatus.SelectedValue == "0")
        //{
        //    todaystatus = "and (MyDailyWorkReport.WorkDone='" + ddltodaytaskstatus.SelectedValue + "' Or MyDailyWorkReport.WorkDone IS NULL  ) ";
        //}
        //if (ddltodaytaskstatus.SelectedValue == "1")
        //{
        //    todaystatus = "and MyDailyWorkReport.WorkDone='" + ddltodaytaskstatus.SelectedValue + "'";
        //}
        

        if (ddlmaintaskstatus.SelectedValue == "1")
        {
            allstatus = " and ((PageWorkTbl.EpmloyeeID_AssignedDeveloper='" + ddl_empsearch.SelectedValue + "' and PageWorkTbl.DevelopmentDone='1')or(PageWorkTbl.EpmloyeeID_AssignedTester='" + ddl_empsearch.SelectedValue + "' and PageWorkTbl.TestingDone='1')or (PageWorkTbl.EpmloyeeID_AssignedSupervisor='" + ddl_empsearch.SelectedValue + "' and PageWorkTbl.SupervisorCheckingDone='1'))";
            allstatus = " and (MyDailyWorkReport.WorkDone ='1') ";
        }

        if (ddlmaintaskstatus.SelectedValue == "0")
        {
            allstatus = " and ((PageWorkTbl.EpmloyeeID_AssignedDeveloper='" + ddl_empsearch.SelectedValue + "' and PageWorkTbl.DevelopmentDone='0')or(PageWorkTbl.EpmloyeeID_AssignedTester='" + ddl_empsearch.SelectedValue + "' and PageWorkTbl.TestingDone='0')or (PageWorkTbl.EpmloyeeID_AssignedSupervisor='" + ddl_empsearch.SelectedValue + "' and PageWorkTbl.SupervisorCheckingDone='0'))";
            allstatus = " and ( MyDailyWorkReport.WorkDone='0' Or MyDailyWorkReport.WorkDone='') ";
        }
        if (ddlpagename.SelectedIndex > 0)
        {
            pagename = " and PageMaster.PageId='" + ddlpagename.SelectedValue + "' ";
        }
        if (TextBox1.Text.Trim() != "" && TextBox2.Text.Trim() != "")
        {
            allstatus += " and MyDailyWorkReport.workallocationdate between '" + TextBox1.Text + "' and '" + TextBox2.Text + "' ";
           // allstatus += " and MyDailyWorkReport.workallocationdate between '" + TextBox1.Text.Trim() + "' and '" + TextBox2.Text.Trim() + "' ";
        }
        if (TextBox5.Text != "")
        {
            allstatus += "  and (PageMaster.PageName Like '%" + TextBox5.Text + "%' OR PageMaster.PageTitle Like '%" + TextBox5.Text + "%' ) ";
        }

        string orderby = " order by  dbo.MyDailyWorkReport.EmployeeId, dbo.MyDailyWorkReport.WorkDone desc ";

        string finalstr = strcln + product + todaystatus + allstatus + pagename + orderby;


        
        
        
SqlCommand cmdcln = new SqlCommand(finalstr, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);




        if (dtcln.Rows.Count > 0)
        {
            DataView myDataView = new DataView();
            myDataView = dtcln.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
             //   myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }

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
                lbl_totaloffer.Text =Convert.ToString(sumTotal);
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
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            
            txtworkdonereport.Enabled = true;
            txthourspent.Enabled = true;
            RadioButtonList1.Enabled = true; 
            // Label lbl_shortlist = (Label)GR.FindControl("lbl_shortlist");
            Session["lbl_shortlist"] = Convert.ToInt32(e.CommandArgument);//(GridView1.SelectedRow.FindControl("lbl_shortlist") as Label).Text;

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

                txt_mainwork.Text = dtfff.Rows[0]["WorkRequirementTitle"].ToString();

                if (dtfff.Rows[0]["maininc"] != "" || dtfff.Rows[0]["maininc"] != null )
                {
                    lbl_incentiveAct.Text = dtfff.Rows[0]["maininc"].ToString();
                    if (lbl_incentiveAct.Text == "")
                    {
                        lbl_incentiveAct.Text = "00.0";
                    }
                }
                else
                {
                    lbl_incentiveAct.Text = "00.0";
                }
                txthourspent.Text = dtfff.Rows[0]["HourSpent"].ToString();
                
                    fillbudgetdhour();
                    maintaskbudhourcost();
                    todaytaskbudhourcost();

                    try
                    {
                        string work = dtfff.Rows[0]["WorkDone"].ToString();
                        if (work == "True" || work == "true")
                        {
                            RadioButtonList1.SelectedValue = "1";    
                        }
                        RadioButtonList1_SelectedIndexChanged(sender, e);
                        if (DropDownList1.SelectedValue == "1")
                        {
                            CheckBox1.Checked = Convert.ToBoolean(dtfff.Rows[0]["WorkDone"].ToString());
                        }
                        else if (DropDownList1.SelectedValue == "2")
                        {
                            CheckBox1.Checked = Convert.ToBoolean(dtfff.Rows[0]["TestingDone"].ToString());

                        }
                        else if (DropDownList1.SelectedValue == "3")
                        {
                            CheckBox1.Checked = Convert.ToBoolean(dtfff.Rows[0]["SupervisorCheckingDone"].ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                pnl_grid.Visible = false;   
                pnl_add.Visible = true;
            }



            RadioButtonList1_SelectedIndexChanged(sender, e);
        }

        if (e.CommandName == "Update")
        {
            //Button1.Text = "Edit";
            //txtworkdonereport.Enabled = false;
            //txthourspent.Enabled = false;
            // Label lbl_shortlist = (Label)GR.FindControl("lbl_shortlist");
            Session["lbl_shortlist"] = Convert.ToInt32(e.CommandArgument);//(GridView1.SelectedRow.FindControl("lbl_shortlist") as Label).Text;

            string stdd = "select Top(1) MyDailyWorkReport.*,PageWorkTbl.WorkRequirementTitle,PageMaster.PageName as PageTitle ,EmployeeMaster.Name as EmployeeName,EmployeeMaster.EffectiveRate ,ProductMaster.ProductName,MyDailyWorkReport.Typeofwork,PageVersionTbl.VersionNo, case when (MyDailyWorkReport.Typeofwork='1') then 'Developer' else  (case when (MyDailyWorkReport.Typeofwork='2') then 'Tester' else (case when (MyDailyWorkReport.Typeofwork='3') then 'Supervisor' else '' End) End   )  End  as Statuslabel, case when (MyDailyWorkReport.WorkDone='0' or MyDailyWorkReport.WorkDone IS NULL) then 'Pending' else 'Completed' End as TodayStatus from MyDailyWorkReport  left outer join ProductMaster on ProductMaster.ProductId=MyDailyWorkReport.ProductId left outer join VersionInfoMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId left outer join WebsiteMaster on WebsiteMaster.VersionInfoId=VersionInfoMaster.VersionInfoId left outer join   WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.ID left outer join   MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId left outer join PageMaster on PageMaster.PageId=MyDailyWorkReport.PageId left outer join   PageWorkTbl on MyDailyWorkReport.PageWorkTblId=PageWorkTbl.Id  left outer join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId    inner join  EmployeeMaster on EmployeeMaster.Id=MyDailyWorkReport.EmployeeId   " +
            "   where MyDailyWorkReport.ID=" + Session["lbl_shortlist"] + " ";

            SqlDataAdapter dafff = new SqlDataAdapter(stdd, con);
            DataTable dtfff = new DataTable();
            dafff.Fill(dtfff);

            if (dtfff.Rows.Count > 0)
            {
                Session["ID"] = dtfff.Rows[0]["ID"].ToString();
                ddlworktitle.SelectedValue = Convert.ToString(dtfff.Rows[0]["ID"]);

                fillbudgetdhour();
                maintaskbudhourcost();
                todaytaskbudhourcost();


                pnl_grid.Visible = false;
                pnl_add.Visible = true;
            }




        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
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
    protected void GridView2_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        fillgrid();
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
     
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

           
      


            Label lblmasterId = (Label)e.Row.FindControl("lblmasterId");
            Label lblworktypeid = (Label)e.Row.FindControl("lblworktypeid");
            Label lblworktobedonsse = (Label)e.Row.FindControl("lblworktobedonsse");
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
            string strpageworkall = "select * from DailyWorkGuideUploadTbl where MyWorktblid='" + lbl_workid.Text + "' ";

            SqlCommand cmdpageworkall = new SqlCommand(strpageworkall, con);
            DataTable dtpageworkall = new DataTable();
            SqlDataAdapter adppageworkall = new SqlDataAdapter(cmdpageworkall);
            adppageworkall.Fill(dtpageworkall);

            if (dtpageworkall.Rows.Count > 0)
            {
                imageBTN_IWork.Visible = true;
                lbl_noattach.Visible = false;
            }
            else
            {
                imageBTN_IWork.Visible = false;
                lbl_noattach.Visible = true;
            }
            

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
                    LinkButton2Show.Visible = true;
                    LinkButton5Edit.Visible = false;
                }
                else
                {
                    lblmaintaskstatus.Text = "Pending";
                    LinkButton2Show.Visible = false;
                    LinkButton5Edit.Visible = true;
                }
                if (lblworktobedonsse.Text == "" || lblworktobedonsse.Text == null)
                {
                    LinkButton2Show.Visible = false;
                    LinkButton5Edit.Visible = true;
                }
                else
                {
                    LinkButton2Show.Visible = true;
                    LinkButton5Edit.Visible = false;
                }

                string strhrspent = " select * from MyDailyWorkReport  where PageWorkTblId='" + lblmasterId.Text + "'  and MyDailyWorkReport.Typeofwork='" + lblworktypeid.Text + "' ";
                SqlCommand cmdhrspent = new SqlCommand(strhrspent, con);
                DataTable dthrspent = new DataTable();
                SqlDataAdapter adphrspent = new SqlDataAdapter(cmdhrspent);
                adphrspent.Fill(dthrspent);

                if (dthrspent.Rows.Count > 0)
                {

                string checkwork=    dthrspent.Rows[0]["WorkDone"].ToString();

                if (checkwork == "False")
                {
                  
                    //LinkButton2Show.Visible = false;
                   // LinkButton5Edit.Visible = true;
                }
                else
                {
                  
                 //   LinkButton2Show.Visible = true;
                  //  LinkButton5Edit.Visible = false;
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

                    //double cost1 = totalhr * Convert.ToDouble(lblempeffectiverate.Text);

                    //double cost2 = ((totalminute * 60) * (Convert.ToDouble(lblempeffectiverate.Text)));

                    //double FinalCost = cost1 + cost2;
                    //FinalCost = Math.Round(FinalCost, 2);

                    //lblspenthourcost.Text = FinalCost.ToString();
                }

            }

           
        }
        //if (e.Row.RowType = DataControlRowType.Footer)
        //{

        //}
    }

    protected void columndisplay()
    {
        //if (chkdate.Checked == true)
        //{
        //    GridView2.Columns[0].Visible = true;
        //}
        //else
        //{
        //    GridView2.Columns[0].Visible = false;
        //}

        //if (chkemployee.Checked == true)
        //{
        //    GridView2.Columns[1].Visible = true;
        //}
        //else
        //{
        //    GridView2.Columns[1].Visible = false;

        //}
        //if (chkproduct.Checked == true)
        //{
        //    GridView2.Columns[2].Visible = true;
        //}
        //else
        //{
        //    GridView2.Columns[2].Visible = false;
        //}

        //if (chkworktype.Checked == true)
        //{
        //    GridView2.Columns[3].Visible = true;
        //}
        //else
        //{
        //    GridView2.Columns[3].Visible = false;
        //}

        //if (chkworktitle.Checked == true)
        //{
        //    GridView2.Columns[4].Visible = true;
        //}
        //else
        //{
        //    GridView2.Columns[4].Visible = false;
        //}


        //if (chkpagename.Checked == true)
        //{

        //    GridView2.Columns[5].Visible = true;
        //}
        //else
        //{
        //    GridView2.Columns[5].Visible = false;
        //}

        //if (chkverno.Checked == true)
        //{

        //    GridView2.Columns[6].Visible = true;
        //}
        //else
        //{
        //    GridView2.Columns[6].Visible = false;
        //}

        //if (chkbudmaintask.Checked == true)
        //{
        //    GridView2.Columns[7].Visible = true;
        //}
        //else
        //{
        //    GridView2.Columns[7].Visible = false;
        //}

        //if (chkacthrmaintask.Checked == true)
        //{
        //    GridView2.Columns[8].Visible = true;

        //}
        //else
        //{
        //    GridView2.Columns[8].Visible = false;
        //}


        //if (chktodaybudhr.Checked == true)
        //{
        //    GridView2.Columns[9].Visible = true;
        //}
        //else
        //{
        //    GridView2.Columns[9].Visible = false;
        //}

        //if (chktodayacthr.Checked == true)
        //{
        //    GridView2.Columns[10].Visible = true;
        //}
        //else
        //{
        //    GridView2.Columns[10].Visible = false;
        //}

        //if (chktodayworktitle.Checked == true)
        //{
        //    GridView2.Columns[11].Visible = true;
        //}
        //else
        //{
        //    GridView2.Columns[11].Visible = false;
        //}
        //if (chktodayworkreport.Checked == true)
        //{
        //    GridView2.Columns[12].Visible = true;

        //}
        //else
        //{
        //    GridView2.Columns[12].Visible = false;
        //}

        //if (chkmaintaskstatus.Checked == true)
        //{
        //    GridView2.Columns[13].Visible = true;

        //}
        //else
        //{
        //    GridView2.Columns[13].Visible = false;
        //}

        //if (chktodaytaskstatus.Checked == true)
        //{
        //    GridView2.Columns[14].Visible = true;

        //}
        //else
        //{
        //    GridView2.Columns[14].Visible = false;
        //}





    }
    protected void ddlwebsite_SelectedIndexChanged(object sender, EventArgs e)
    {
        filltype();
    }
    protected void link1_Click(object sender, EventArgs e)
    {
        lblpaged.Visible = true;
        lblpaged.Text = "";
      //  GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
        GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;


        int rinrow = row.RowIndex;

        Label lblmasterId = (Label)GridView2.Rows[rinrow].FindControl("lblmasterId");
        Label lblmyworktblid = (Label)GridView2.Rows[rinrow].FindControl("lblmyworktblid");



        string strcount = "select PageWorkTbl.*,WebsiteMaster.WebsiteName,PageMaster.PageTitle,PageVersionTbl.PageName as NewVersionName,PageVersionTbl.VersionNo from PageWorkTbl  inner join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId   inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId    inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId    inner join  MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id    inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId    inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where PageWorkTbl.Id='" + lblmasterId.Text + "'";

        SqlCommand cmdcount = new SqlCommand(strcount, con);
        DataTable dtcount = new DataTable();
        SqlDataAdapter adpcount = new SqlDataAdapter(cmdcount);
        adpcount.Fill(dtcount);
        if (dtcount.Rows.Count > 0)
        {
            lblwebsitenamedetail.Text = dtcount.Rows[0]["WebsiteName"].ToString();
            lblpagenamedetail.Text = dtcount.Rows[0]["PageTitle"].ToString();
            lblworktitledetail.Text = dtcount.Rows[0]["WorkRequirementTitle"].ToString();

            lblnewpageversion.Text = dtcount.Rows[0]["VersionNo"].ToString();
            lblnewpagedetaildetail.Text = dtcount.Rows[0]["NewVersionName"].ToString();
            lblworkdescriptiondetail.Text = dtcount.Rows[0]["WorkRequirementDescription"].ToString();

            lblbudgetedhourdetail.Text = Convert.ToString(dtcount.Rows[0]["BudgetedHourDevelopment"]);

            lbltargatedatedetail.Text = Convert.ToString(dtcount.Rows[0]["TargetDateDeveloper"]);


            string str1231 = " select * from PageWorkGuideUploadTbl where PageWorkTblId='" + lblmasterId.Text + "'";

            SqlCommand cmd1231 = new SqlCommand(str1231, con);
            DataTable dt1231 = new DataTable();
            SqlDataAdapter adp123 = new SqlDataAdapter(cmd1231);
            adp123.Fill(dt1231);

            if (dt1231.Rows.Count > 0)
            {
                GridView1.DataSource = dt1231;
                GridView1.DataBind();
            }

            string strcount2 = " select PageFinishWorkUploadTbl.Id,Convert(nvarchar,PageFinishWorkUploadTbl.Date,101) as Date, PageFinishWorkUploadTbl.FileName as upfile,PageMaster.FolderName,PageMaster.PageTitle,PageVersionTbl.VersionNo,PageVersionTbl.PageName from PageFinishWorkUploadTbl inner join PageWorkTbl on PageWorkTbl.Id=PageFinishWorkUploadTbl.PageWorkTblId inner join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId  inner join  MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id  inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId  inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where PageWorkTbl.Id='" + lblmasterId.Text + "' order by PageFinishWorkUploadTbl.Id Desc";
            SqlCommand cmdcount2 = new SqlCommand(strcount2, con);
            DataTable dtcount2 = new DataTable();
            SqlDataAdapter adpcount2 = new SqlDataAdapter(cmdcount2);
            adpcount2.Fill(dtcount2);
            if (dtcount2.Rows.Count > 0)
            {

                grdsourcefile.DataSource = dtcount2;
                grdsourcefile.DataBind();
            }
        }

        string strpageworkall = "select * from DailyWorkGuideUploadTbl where MyworktblId='" + lblmyworktblid.Text + "' ";

        SqlCommand cmdpageworkall = new SqlCommand(strpageworkall, con);
        DataTable dtpageworkall = new DataTable();
        SqlDataAdapter adppageworkall = new SqlDataAdapter(cmdpageworkall);
        adppageworkall.Fill(dtpageworkall);

        if (dtpageworkall.Rows.Count > 0)
        {
            GridView3.DataSource = dtpageworkall;
            GridView3.DataBind();
        }


        ModalPopupExtender1.Show();





    }

    protected void link312_Click(object sender, EventArgs e)
    {

        //GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;

        //int rinrow = row.RowIndex;
        //string pv = GridView2.DataKeys[rinrow].Value.ToString();

        //Label lblmasterId = (Label)GridView2.Rows[rinrow].FindControl("lblmasterId");


        //lblpageworkmasterId.Text = lblmasterId.Text;


        //string strcount = " select WebsiteMaster.*,PageMaster.FolderName,PageVersionTbl.DeveloperOK from MyDailyWorkReport inner join  PageWorkTbl on MyDailyWorkReport.PageWorkTblId=PageWorkTbl.Id inner join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId  inner join  MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id  inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId  inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where MyDailyWorkReport.Id='" + pv + "'";
        //SqlCommand cmdcount = new SqlCommand(strcount, con);
        //DataTable dtcount = new DataTable();
        //SqlDataAdapter adpcount = new SqlDataAdapter(cmdcount);
        //adpcount.Fill(dtcount);
        //if (dtcount.Rows.Count > 0)
        //{
        //    lblftpurl123.Text = dtcount.Rows[0]["FTP_Url"].ToString();
        //    lblftpport123.Text = dtcount.Rows[0]["FTP_Port"].ToString();
        //    lblftpuserid.Text = dtcount.Rows[0]["FTP_UserId"].ToString();
        //    lblftppassword123.Text = PageMgmt.Decrypted(dtcount.Rows[0]["FTP_Password"].ToString());
        //    ViewState["folder"] = dtcount.Rows[0]["FolderName"].ToString();
        //}
        //else
        //{
        //    lblftpurl123.Text = "";
        //    lblftpport123.Text = "";
        //    lblftpuserid.Text = "";
        //    lblftppassword123.Text = "";
        //    ViewState["folder"] = "";
        //}
        //Session["GridFileAttach1"] = null;
        //gridFileAttach.DataSource = null;
        //gridFileAttach.DataBind();

        //if (dtcount.Rows.Count > 0)
        //{

        //    if (Convert.ToString(dtcount.Rows[0]["DeveloperOK"]) != "True")
        //    {
        //        ModalPopupExtender3.Show();
        //    }
        //    else
        //    {
        //        lblmsg.Text = "Sorry,You don't permitted change Record after certify";
        //        lblmsg.Visible = true;
        //    }

        //}
        //else
        //{
        //    lblmsg.Text = "Work allocation with page version is not done for this task.";
        //    lblmsg.Visible = true;
        //}



    }
    protected void linkdow1_Click(object sender, EventArgs e)
    {
    }
    protected void linkdow_Click(object sender, EventArgs e)
    {
        lblpaged.Visible = true;
        lblpaged.Text = "";
        GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;

        int rinrow = row.RowIndex;

        int data = Convert.ToInt32(grdsourcefile.DataKeys[rinrow].Value);




        string strcount = " select PageFinishWorkUploadTbl.FileName,PageFinishWorkUploadTbl.PageWorkTblId as PageWorkMasterId, WebsiteMaster.*,PageMaster.FolderName from PageFinishWorkUploadTbl inner join  PageWorkTbl  on PageWorkTbl.id=PageFinishWorkUploadTbl.PageWorkTblId inner join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId  inner join  MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id  inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId  inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where PageFinishWorkUploadTbl.Id='" + data + "'";
        SqlCommand cmdcount = new SqlCommand(strcount, con);
        DataTable dtcount = new DataTable();
        SqlDataAdapter adpcount = new SqlDataAdapter(cmdcount);
        adpcount.Fill(dtcount);



        if (dtcount.Rows.Count > 0)
        {

            string strpageversion = "select  PageVersionTbl.* from PageWorkTbl inner join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId where PageWorkTbl.Id='" + dtcount.Rows[0]["PageWorkMasterId"].ToString() + "'";
            SqlCommand cmdpageversion = new SqlCommand(strpageversion, con);
            DataTable dtpageversion = new DataTable();
            SqlDataAdapter adppageversion = new SqlDataAdapter(cmdpageversion);
            adppageversion.Fill(dtpageversion);

            if (dtpageversion.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dtpageversion.Rows[0]["TesterOk"]) == true && Convert.ToBoolean(dtpageversion.Rows[0]["SupervisorOk"]) == true)
                {
                    lblpaged.Visible = true;
                    lblpaged.Text = "Sorry ,After Certification of Supervisior you can not download this file.";
                    ModalPopupExtender1.Show();

                }
                else
                {

                    //lblftpurl123.Text = dtcount.Rows[0]["FTP_Url"].ToString();
                    //lblftpport123.Text = dtcount.Rows[0]["FTP_Port"].ToString();
                    //lblftpuserid.Text = dtcount.Rows[0]["FTP_UserId"].ToString();
                    //lblftppassword123.Text = dtcount.Rows[0]["FTP_Password"].ToString();
                    //ViewState["folder"] = dtcount.Rows[0]["FolderName"].ToString();

                    //string[] separator1 = new string[] { "/" };
                    //string[] strSplitArr1 = lblftpurl123.Text.Split(separator1, StringSplitOptions.RemoveEmptyEntries);

                    //String productno = strSplitArr1[0].ToString();
                    //string ftpurl = "";

                    //if (productno == "FTP:" || productno == "ftp:")
                    //{
                    //    if (strSplitArr1.Length >= 3)
                    //    {
                    //        ftpurl = strSplitArr1[0].ToString() + "//" + strSplitArr1[1].ToString() + ":" + lblftpport123.Text;
                    //        for (int i = 2; i < strSplitArr1.Length; i++)
                    //        {
                    //            ftpurl += "/" + strSplitArr1[i].ToString();
                    //        }
                    //    }
                    //    else
                    //    {
                    //        ftpurl = strSplitArr1[0].ToString() + "//" + strSplitArr1[1].ToString() + ":" + lblftpport123.Text;

                    //    }
                    //}
                    //else
                    //{
                    //    if (strSplitArr1.Length >= 2)
                    //    {
                    //        ftpurl = "ftp://" + strSplitArr1[0].ToString() + ":" + lblftpport123.Text;
                    //        for (int i = 1; i < strSplitArr1.Length; i++)
                    //        {
                    //            ftpurl += "/" + strSplitArr1[i].ToString();
                    //        }
                    //    }
                    //    else
                    //    {
                    //        ftpurl = "ftp://" + strSplitArr1[0].ToString() + ":" + lblftpport123.Text;

                    //    }

                    //}


                    //if (lblftpurl123.Text.Length > 0)
                    //{

                    //string ftphost = ftpurl + "/" + ViewState["folder"] + "/";
                    string fnname = dtcount.Rows[0]["FileName"].ToString();
                    string despath = Server.MapPath("~\\Attachment\\") + fnname.ToString();
                    //FileInfo filec = new FileInfo(despath);
                    //try
                    //{
                    //    if (!filec.Exists)
                    //    {
                    //        GetFile(ftphost, fnname, despath, lblftpuserid.Text, lblftppassword123.Text);
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    lblpaged.Text = ex.ToString();
                    //}



                    FileInfo file = new FileInfo(despath);
                    if (file.Exists)
                    {
                        Response.Clear();
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                        Response.AddHeader("Content-Length", file.Length.ToString());
                        Response.ContentType = "application/octet-stream";
                        Response.WriteFile(file.FullName);
                        Response.End();
                    }


                    //  }

                }
            }



        }

    }
    protected void linkdow1dailywork_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;

        int rinrow = row.RowIndex;

        int data = Convert.ToInt32(GridView3.DataKeys[rinrow].Value);




       
        //string strcount = " select PageWorkGuideUploadTbl.WorkRequirementPdfFilename,PageWorkGuideUploadTbl.WorkRequirementAudioFileName,WebsiteMaster.* from PageWorkGuideUploadTbl inner join PageWorkTbl on PageWorkTbl.Id=PageWorkGuideUploadTbl.PageWorkTblId inner join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId inner join MainMenuMaster on MainMenuMaster.MainMenuId=PageMaster.MainMenuId  inner join  MasterPageMaster on MasterPageMaster.MasterPageId=MainMenuMaster.MasterPage_Id  inner join WebsiteSection on WebsiteSection.WebsiteSectionId=MasterPageMaster.WebsiteSectionId  inner join WebsiteMaster on WebsiteMaster.ID=WebsiteSection.WebsiteMasterId where PageWorkGuideUploadTbl.Id='" + data + "' ";
        string strcount = " select  DailyWorkGuideUploadTbl.WorkRequirementPdfFilename,DailyWorkGuideUploadTbl.WorkRequirementAudioFileName,WebsiteMaster.* from DailyWorkGuideUploadTbl inner join MyDailyWorkReport on MyDailyWorkReport.Id=DailyWorkGuideUploadTbl.MyworktblId inner join ProductMaster on ProductMaster.ProductId=MyDailyWorkReport.ProductId inner join VersionInfoMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId inner join WebsiteMaster on WebsiteMaster.VersionInfoId=VersionInfoMaster.VersionInfoId where DailyWorkGuideUploadTbl.Id='" + data + "' ";
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


            string despath = Server.MapPath("~\\Attachment\\") + fnname.ToString();
            FileInfo file = new FileInfo(despath);
            despath = "~\\Uploads\\" + fnname.ToString();
            despath = "http://license.busiwiz.com/Attachment/" + fnname.ToString();
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + despath + "');", true);
            //FileInfo file = new FileInfo(despath);
            //if (file.Exists)
            //{
            //    Response.Clear();
            //    Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
            //    Response.AddHeader("Content-Length", file.Length.ToString());
            //    Response.ContentType = "application/octet-stream";
            //    Response.WriteFile(file.FullName);
            //    Response.End();
            //}
        }
      
    }
    //******Searching End

    protected void fillemployee()
    {

        string strcln = " SELECT * from EmployeeMaster where ClientId='" + Session["ClientId"] + "'  ";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);

        ddlemployee.DataSource = dtcln;
        ddlemployee.DataValueField = "Id";
        ddlemployee.DataTextField = "Name";
        ddlemployee.DataBind();

        if (dtcln.Rows.Count > 0)
        {


            string strclnuser = " SELECT * from EmployeeMaster where ClientId='" + Session["ClientId"] + "' and Id='" + Session["id"] + "'  ";//UserId='" +Session["UserId"]+"'
            SqlCommand cmdclnuser = new SqlCommand(strclnuser, con);
            DataTable dtclnuser = new DataTable();
            SqlDataAdapter adpclnuser = new SqlDataAdapter(cmdclnuser);
            adpclnuser.Fill(dtclnuser);
            if (dtclnuser.Rows.Count > 0)
            {
                ddlemployee.Enabled = false;
                ddlemployee.SelectedValue = dtclnuser.Rows[0]["Id"].ToString();

            }
            else
            {
                ddlemployee.Enabled = true;

            }

        }

    }
    protected void fillpagename()
    {

        string file1 = "";


        if (DropDownList1.SelectedValue == "1")
        {
            file1 = "+'- Main Hr :'+  PageWorkTbl.BudgetedHourDevelopment +'- Today Hr :'+ MyDailyWorkReport.budgetedhour  ";
        }
        if (DropDownList1.SelectedValue == "2")
        {
            file1 = "+'- Main Hr :'+ PageWorkTbl.BudgetedHourTesting +'- Today Hr :'+ MyDailyWorkReport.budgetedhour ";
        }
        if (DropDownList1.SelectedValue == "3")
        {
            file1 = "+'- Main Hr :'+ PageWorkTbl.BudgetedHourSupervisorChecking +'- Today Hr :'+ MyDailyWorkReport.budgetedhour  ";
        }

      //new  string strcln = "select  MyDailyWorkReport.Id,MyDailyWorkReport.worktobedone +'-'+ PageMaster.PageName  As WorkName from MyDailyWorkReport inner join PageMaster on PageMaster.PageId=MyDailyWorkReport.PageId where EmployeeId='" + ddlemployee.SelectedValue + "' and workallocationdate='" + txttargetdatedeve.Text + "' and Typeofwork='" + DropDownList1.SelectedValue + "'order by WorkName ";//and Typeofwork='" + DropDownList1.SelectedValue + "' order by WorkName
        string strcln = "select MyDailyWorkReport.Id,MyDailyWorkReport.worktobedone,PageMaster.PageName +'-'+ PageVersionTbl.VersionNo +'-'+Left(PageWorkTbl.WorkRequirementTitle,20)+'-'+ MyDailyWorkReport.worktobedone " + file1 + " as WorkName from  MyDailyWorkReport inner join PageWorkTbl on PageWorkTbl.Id=MyDailyWorkReport.PageWorkTblId inner join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId where  EmployeeId='" + ddlemployee.SelectedValue + "'  and Typeofwork='" + DropDownList1.SelectedValue + "' order by workallocationdate Desc";//and workallocationdate='" + txttargetdatedeve.Text + "'

        //old  string strcln = "select MyDailyWorkReport.Id,MyDailyWorkReport.worktobedone,PageMaster.PageName +'-'+ PageVersionTbl.VersionNo +'-'+Left(PageWorkTbl.WorkRequirementTitle,20)+'-'+ MyDailyWorkReport.worktobedone " + file1 + " as WorkName from  MyDailyWorkReport inner join PageWorkTbl on PageWorkTbl.Id=MyDailyWorkReport.PageWorkTblId inner join PageVersionTbl on PageVersionTbl.Id=PageWorkTbl.PageVersionTblId inner join PageMaster on PageMaster.PageId=PageVersionTbl.PageMasterId where  EmployeeId='" + ddlemployee.SelectedValue + "' and workallocationdate='" + txttargetdatedeve.Text + "' and Typeofwork='" + DropDownList1.SelectedValue + "' order by WorkName";

        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        
        if (dtcln.Rows.Count > 0)
        {
            ddlworktitle.DataSource = dtcln;
            ddlworktitle.DataValueField = "Id";
            ddlworktitle.DataTextField = "WorkName";
            ddlworktitle.DataBind();
        }
       
        if (dtcln.Rows.Count <= 0)
        {
           // string strcln1 = "select MyDailyWorkReport.Id, MyDailyWorkReport.worktobedone from MyDailyWorkReport where  EmployeeId='" + ddlemployee.SelectedValue + "' and workallocationdate='" + txttargetdatedeve.Text + "' and Typeofwork='" + DropDownList1.SelectedValue + "' ";//and Typeofwork='" + DropDownList1.SelectedValue + "' order by WorkName
            string str = "select MyDailyWorkReport.worktobedone ,MyDailyWorkReport.Id from MyDailyWorkReport  where  EmployeeId='" + ddlemployee.SelectedValue + "' and workallocationdate='" + txttargetdatedeve.Text + "' and Typeofwork='" + DropDownList1.SelectedValue + "' ";//and Typeofwork='" + DropDownList1.SelectedValue + "' order by WorkName
            SqlCommand cmd = new SqlCommand(str, con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            ddlworktitle.DataSource = dt;
            ddlworktitle.DataValueField = "Id";
            ddlworktitle.DataTextField = "worktobedone";
            ddlworktitle.DataBind();
        }


      
        //  ddlworktitle.DataTextField = "WorkName";
       


    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button1.Text == "Edit")
        {
            Button1.Text = "Update";
            txthourspent.Enabled = true;
            txtworkdonereport.Enabled = true;
            RadioButtonList1.Enabled = true;
        }
        else
        {

            //if (ddlworktitle.SelectedIndex > -1)
            //{
                DateTime d = DateTime.Now;
                string  i = "0";
               if(Convert.ToDateTime(txttargetdatedeve.Text) >= d)
                {
                    i = txt_incentive.Text; 
                }
                string UpdatePagework = "";
                if (RadioButtonList1.SelectedValue == "0")
                {
                    UpdatePagework = "Update MyDailyWorkReport set HourSpent='" + txthourspent.Text + "',WorkDoneReport='" + txtworkdonereport.Text + "',WorkDone='" + RadioButtonList1.SelectedValue + "',EmpRequestHour='" + txtactualhourrequired.Text + "', Earn_Amount='0' where Id='" + Session["ID"] + "'  ";//ddlworktitle.SelectedValue
                }
                if (RadioButtonList1.SelectedValue == "1")
                {
                    UpdatePagework = "Update MyDailyWorkReport set HourSpent='" + txthourspent.Text + "',WorkDoneReport='" + txtworkdonereport.Text + "',WorkDone='" + RadioButtonList1.SelectedValue + "',EmpRequestHour='00:00',  Earn_Amount='" + txt_incentive.Text + "' where Id='" + Session["ID"] + "'  ";//ddlworktitle.SelectedValue
                }



                SqlCommand cmdupdate = new SqlCommand(UpdatePagework, con);
                con.Open();
                cmdupdate.ExecuteNonQuery();
                con.Close();

                string pageworkupdate = "";

                if (RadioButtonList1.SelectedValue == "1" && CheckBox1.Checked == true)
                {
                    string strcln = " select * from MyDailyWorkReport  where Id='" + Session["id"] + " ' ";//" + ddlworktitle.SelectedValue + "
                    SqlCommand cmdcln = new SqlCommand(strcln, con);
                    DataTable dtcln = new DataTable();
                    SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
                    adpcln.Fill(dtcln);

                    string typework = dtcln.Rows[0]["Typeofwork"].ToString();

                    if (DropDownList1.SelectedValue  == "1")
                    {
                        pageworkupdate = "update PageWorkTbl set DevelopmentDone='1' where Id='" + dtcln.Rows[0]["PageWorkTblId"].ToString() + "'";
                    }
                    else if (DropDownList1.SelectedValue == "2")
                    {
                        pageworkupdate = "update PageWorkTbl set TestingDone='1' where Id='" + dtcln.Rows[0]["PageWorkTblId"].ToString() + "'";
                    }
                    else if (DropDownList1.SelectedValue == "3")
                    {
                        pageworkupdate = "update PageWorkTbl set SupervisorCheckingDone='1' where Id='" + dtcln.Rows[0]["PageWorkTblId"].ToString() + "'";
                    }

                    SqlCommand cmdpageworkupdate = new SqlCommand(pageworkupdate, con);
                    con.Open();
                    cmdpageworkupdate.ExecuteNonQuery();
                    con.Close();


                }
                else
                {
                    string strcln = " select * from MyDailyWorkReport  where Id='" + Session["id"] + "' ";
                    SqlCommand cmdcln = new SqlCommand(strcln, con);
                    DataTable dtcln = new DataTable();
                    SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
                    adpcln.Fill(dtcln);

                    string typework = dtcln.Rows[0]["Typeofwork"].ToString();

                    if (DropDownList1.SelectedValue == "1")
                    {
                        pageworkupdate = "update PageWorkTbl set DevelopmentDone='0' where Id='" + dtcln.Rows[0]["PageWorkTblId"].ToString() + "'";
                    }
                    else if (DropDownList1.SelectedValue == "2")
                    {
                        pageworkupdate = "update PageWorkTbl set TestingDone='0' where Id='" + dtcln.Rows[0]["PageWorkTblId"].ToString() + "'";
                    }
                    else if (DropDownList1.SelectedValue == "3")
                    {
                        pageworkupdate = "update PageWorkTbl set SupervisorCheckingDone='0' where Id='" + dtcln.Rows[0]["PageWorkTblId"].ToString() + "'";
                    }

                    SqlCommand cmdpageworkupdate = new SqlCommand(pageworkupdate, con);
                    con.Open();
                    cmdpageworkupdate.ExecuteNonQuery();
                    con.Close();


                }

                lblmsg.Visible = true;
                lblmsg.Text = "Report Done Succesfully";





         //   }
            //else
            //{
            //    lblmsg.Visible = true;

            //    lblmsg.Text = "Please select Work title";

            //}

            // ddlemployee.SelectedIndex = 0;
            //ddlworktitle.SelectedIndex = 0;
            txtworkdonereport.Text = "";
            txthourspent.Text = "";

            pnl_add.Visible = false;
            pnl_grid.Visible = true;
            fillgrid();
        }

    }
    protected void ddlemployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        costofdeveloper();
        fillpagename();
        fillbudgetdhour();
        maintaskbudhourcost();
        todaytaskbudhourcost();

    }
    protected void txttargetdatedeve_TextChanged(object sender, EventArgs e)
    {
        if (txttargetdatedeve.Text.ToString() == "")
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Please Select date";

        }
        else
        {
            fillpagename();
            fillbudgetdhour();
            maintaskbudhourcost();
            todaytaskbudhourcost();

        }
    }
    protected void fillbudgetdhour()
    {
        string strcln = " select * from MyDailyWorkReport  where Id='" + Session["ID"] + "' ";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        DataTable dtcln = new DataTable();
        adpcln.Fill(dtcln);

        if (dtcln.Rows.Count > 0)
        {
            Label2.Text = dtcln.Rows[0]["budgetedhour"].ToString();
            txthourspent.Text = dtcln.Rows[0]["budgetedhour"].ToString();
            txt_title.Text = dtcln.Rows[0]["worktobedone"].ToString();
            txtworkdonereport.Text = dtcln.Rows[0]["WorkDoneReport"].ToString();
            string checkwork = dtcln.Rows[0]["WorkDone"].ToString();
            if (dtcln.Rows[0]["Offer_Amount"] != "" || dtcln.Rows[0]["Offer_Amount"] != null)
            {
                txt_incentive.Text = dtcln.Rows[0]["Offer_Amount"].ToString();
                if (txt_incentive.Text == "")
                {
                    txt_incentive.Text = "00.0";
                }
            }
            else
            {
                txt_incentive.Text = "00.0";
            }
         
            txttargetdatedeve.Text  =dtcln.Rows[0]["workallocationdate"].ToString();
            
            
            if (checkwork == "False")
            {
                Button1.Text = "Submit";
                txthourspent.Enabled = true;
                txtworkdonereport.Enabled = true;
                RadioButtonList1.Enabled = true;
                 
            }
            else
            {
                //Button1.Text = "Edit";
                //txthourspent.Enabled = false;
                //txtworkdonereport.Enabled = false;
                
                //RadioButtonList1.SelectedValue = "1";
                //RadioButtonList1.Enabled = false;
                Button1.Text = "Submit";
                txthourspent.Enabled = true;
                txtworkdonereport.Enabled = true;
                RadioButtonList1.Enabled = true;
            }


            string strhrspent11 = "  select * from MyDailyWorkReport where PageWorkTblId='" + dtcln.Rows[0]["PageWorkTblId"].ToString() + "' and WorkDone='0' order by Id desc ";
            SqlCommand cmdhrspent11 = new SqlCommand(strhrspent11, con);
            DataTable dthrspent11 = new DataTable();
            SqlDataAdapter adphrspent11 = new SqlDataAdapter(cmdhrspent11);
            adphrspent11.Fill(dthrspent11);

            if (dthrspent11.Rows.Count > 0)
            {
                lblemprequestedhour.Text = dthrspent11.Rows[0]["EmpRequestHour"].ToString();
               
              
             //   txthourspent.Text = dthrspent11.Rows[0]["HourSpent"].ToString();
             

            }
            else
            {
                lblemprequestedhour.Text = "00:00";

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

                double cost1 = main1 * Convert.ToDouble(lblempeffectiverate.Text);

                double cost2 = ((main2 / 60) * (Convert.ToDouble(lblempeffectiverate.Text)));
                double FinalCost = cost1 + cost2;
                FinalCost = Math.Round(FinalCost, 2);

                lblrequestedempcost.Text = FinalCost.ToString();
            }





            string strmaintaskhr = " select * from PageWorkTbl where Id='" + dtcln.Rows[0]["PageWorkTblId"].ToString() + "' ";
            SqlCommand cmdmaintaskhr = new SqlCommand(strmaintaskhr, con);
            DataTable dtmaintaskhr = new DataTable();
            SqlDataAdapter adpmaintaskhr = new SqlDataAdapter(cmdmaintaskhr);
            adpmaintaskhr.Fill(dtmaintaskhr);

            string typework = dtcln.Rows[0]["Typeofwork"].ToString();

            if (typework == "1")
            {
                if (dtmaintaskhr.Rows.Count > 0)//add this line(10/16/'14)
                {
                    lblmaintaskbudhour.Text = dtmaintaskhr.Rows[0]["BudgetedHourDevelopment"].ToString();
                }
            }
            else if (typework == "2")
            {
                if (dtmaintaskhr.Rows.Count > 0)//add this line(10/16/'14)
                {
                    lblmaintaskbudhour.Text = dtmaintaskhr.Rows[0]["BudgetedHourTesting"].ToString();
                }
            }
            else if (typework == "3")
            {
                if (dtmaintaskhr.Rows.Count > 0)//add this line(10/16/'14)
                {
                    lblmaintaskbudhour.Text = dtmaintaskhr.Rows[0]["BudgetedHourSupervisorChecking"].ToString();
                }
            }

            //17-10  string strhrspent = " select * from MyDailyWorkReport  where PageWorkTblId='" + dtcln.Rows[0]["PageWorkTblId"].ToString() + "' and MyDailyWorkReport.workallocationdate<'" + txttargetdatedeve.Text + "' and MyDailyWorkReport.Typeofwork='" + DropDownList1.SelectedValue + "' ";
            //SqlCommand cmdhrspent = new SqlCommand(strhrspent, con);
            //DataTable dthrspent = new DataTable();
            //SqlDataAdapter adphrspent = new SqlDataAdapter(cmdhrspent);
            //adphrspent.Fill(dthrspent);

            //if (dthrspent.Rows.Count > 0)
            //{
            //    int totalhr = 0;
            //    int totalminute = 0;
            //    string FinalTime = "";
            //     foreach (DataRow dr in dthrspent.Rows)
            //     {
            //         string time = "";
            //         string temp12 = "";
            //         string temp123 = "";
            //         string outdifftime = "";

            //         time = dthrspent.Rows[0]["HourSpent"].ToString();
            //        outdifftime = Convert.ToDateTime(time).ToString("HH:mm");

            //         temp12 = Convert.ToDateTime(time).ToString("HH");
            //         temp123 = Convert.ToDateTime(time).ToString("mm");

            //         int main1 = Convert.ToInt32(temp12);
            //        int main2 = Convert.ToInt32(temp123);

            //         totalhr += main1;
            //         totalminute += main2;

            //         FinalTime = totalhr + ":" + totalminute ;

            //         lbltotalhourspentonmaintask.Text = FinalTime.ToString();


            //     }

            //     double cost1 = totalhr * Convert.ToDouble(lblempeffectiverate.Text);

            //     double cost2 = ((totalminute * 60) * (Convert.ToDouble(lblempeffectiverate.Text)));

            //     double FinalCost = cost1 + cost2;
            //     FinalCost = Math.Round(FinalCost, 2);

            //     lblspenthourcost.Text = FinalCost.ToString();
            //   17-10}


        }



    }
    protected void ddlworktitle_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillbudgetdhour();
        maintaskbudhourcost();
        todaytaskbudhourcost();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        ddlemployee.SelectedIndex = 0;
        fillpagename();

        pnl_grid.Visible = true;
        pnl_add.Visible = false;

    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillpagename();
        fillbudgetdhour();
        maintaskbudhourcost();
        todaytaskbudhourcost();

    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedValue == "0")
        {
            pnltest.Visible = true;
            pnlmaintaskstatus.Visible = false;

        }
        else
        {
            pnltest.Visible = false;
            pnlmaintaskstatus.Visible = true;
        }
    }
    protected void requestedhour()
    {
        string strhrspent = "  select * from MyDailyWorkReport where Id='" + ddlworktitle.SelectedValue + "' and WorkDone='0' order by Id desc ";
        SqlCommand cmdhrspent = new SqlCommand(strhrspent, con);
        DataTable dthrspent = new DataTable();
        SqlDataAdapter adphrspent = new SqlDataAdapter(cmdhrspent);
        adphrspent.Fill(dthrspent);

        if (dthrspent.Rows.Count > 0)
        {
            lblemprequestedhour.Text = dthrspent.Rows[0]["EmpRequestHour"].ToString();

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

            lblempeffectiverate.Text = ViewState["EffectiveRate1"].ToString();

        }
        else
        {
            lblempeffectiverate.Text = "0";

        }




    }

    protected void maintaskbudhourcost()
    {
        if (lblmaintaskbudhour.Text.Length > 0)
        {
            string time = "";
            string outdifftime = "";
            string temp12 = "";
            string temp123 = "";
            TimeSpan t4 = TimeSpan.Parse(lblmaintaskbudhour.Text);
            time = t4.ToString();


            outdifftime = Convert.ToDateTime(time).ToString("HH:mm");
            temp12 = Convert.ToDateTime(time).ToString("HH");
            temp123 = Convert.ToDateTime(time).ToString("mm");

            double main1 = Convert.ToDouble(temp12);
            double main2 = Convert.ToDouble(temp123);

            double cost1 = main1 * Convert.ToDouble(lblempeffectiverate.Text);

            double cost2 = ((main2 / 60) * (Convert.ToDouble(lblempeffectiverate.Text)));
            double FinalCost = cost1 + cost2;
            FinalCost = Math.Round(FinalCost, 2);

            lblmaintaskcost.Text = FinalCost.ToString();
        }
    }
    protected void todaytaskbudhourcost()
    {
        if (Label2.Text.Length > 0)
        {
            string time = "";
            string outdifftime = "";
            string temp12 = "";
            string temp123 = "";
            TimeSpan t4 = TimeSpan.Parse(Label2.Text);
            time = t4.ToString();


            outdifftime = Convert.ToDateTime(time).ToString("HH:mm");
            temp12 = Convert.ToDateTime(time).ToString("HH");
            temp123 = Convert.ToDateTime(time).ToString("mm");

            double main1 = Convert.ToDouble(temp12);
            double main2 = Convert.ToDouble(temp123);

            double cost1 = main1 * Convert.ToDouble(lblempeffectiverate.Text);

            double cost2 = ((main2 / 60) * (Convert.ToDouble(lblempeffectiverate.Text)));
            double FinalCost = cost1 + cost2;
            FinalCost = Math.Round(FinalCost, 2);

            lbltodaytaskcost.Text = FinalCost.ToString();
        }
    }

    protected void SendMailtoadmin(object sender, EventArgs e)
    {
        DateTime now = DateTime.Now;
        string date = now.GetDateTimeFormats('d')[0];
        string time = now.GetDateTimeFormats('t')[0];
        txttargetdatedeve.Text = date + " 00:00";
        TextBox1.Text = date + " 00:00";
        TextBox2.Text = date + " 23:59";


        fillgrid();
        if (GridView2.Rows.Count > 0)
        {
            string str1 = "select distinct PortalMasterTbl.EmailDisplayname,PortalMasterTbl.UserIdtosendmail,PortalMasterTbl.Password,PortalMasterTbl.Mailserverurl, PortalMasterTbl.LogoPath,PortalMasterTbl.Supportteammanagername,PortalMasterTbl.City,PortalMasterTbl.Zip,PortalMasterTbl.Supportteamemailid,PortalMasterTbl.PortalName,PortalMasterTbl.Portalmarketingwebsitename,PortalMasterTbl.Address1,PortalMasterTbl.Supportteamphoneno,PortalMasterTbl.Supportteamphonenoext,PortalMasterTbl.Tollfree,PortalMasterTbl.Tollfreeext,PortalMasterTbl.Fax, ProductMaster.ProductURL, ProductMaster.loginurlforuser, ClientMaster.*,PricePlanMaster.PricePlanName,CompanyMaster.PlanId,CompanyMaster.Adminid,CompanyMaster.Password,CompanyMaster.CompanyLoginId,StateMasterTbl.StateName,CountryMaster.CountryName  from  CompanyMaster inner join PricePlanMaster  on CompanyMaster.PricePlanId=PricePlanMaster.PricePlanId inner join VersionInfoMaster on VersionInfoMaster.VersionInfoId=PricePlanMaster.VersionInfoMasterId inner join ProductMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId inner join ClientMaster on ProductMaster.ClientMasterId=ClientMaster.ClientMasterId  inner join Priceplancategory on Priceplancategory.ID=PricePlanMaster.PriceplancatId inner join PortalMasterTbl on PortalMasterTbl.Id=Priceplancategory.PortalId inner join StateMasterTbl on StateMasterTbl.StateId=PortalMasterTbl.StateId inner join CountryMaster on StateMasterTbl.CountryId=CountryMaster.CountryId WHERE (CompanyLoginId = '" + Session["Comid"] + "') ";
            SqlCommand cmd1 = new SqlCommand(str1, con);
            SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            adp1.Fill(dt1);
            string imgpath = "";
            if (dt1.Rows.Count > 0)
            {
                imgpath = dt1.Rows[0]["LogoPath"].ToString();
            }
            else
            {

            }
            PageConn pgcon = new PageConn();
            epconn = pgcon.dynconn;
            SqlDataAdapter dafff = new SqlDataAdapter("select LogoUrl,SiteUrl from CompanyWebsitMaster where whid='" + Session["whid"] + "'", epconn);
            DataTable dtfff = new DataTable();
            dafff.Fill(dtfff);

            if (dtfff.Rows.Count > 0)
            {
                imgpath= dtfff.Rows[0]["LogoUrl"].ToString();//"~/ShoppingCart/images/" + dtfff.Rows[0]["LogoUrl"].ToString();
                if (imgpath == "")
                {
                    imgpath = "e1.PNG";
                }
            }
            else
            {
                imgpath = "e1.PNG";             
            }
            
          
       
        string str = " select distinct dbo.ClientMaster.Email2 AS VSirEmail,ContactPersonName ,OutgoingServerUserID,OutgoingServerPassword,OurgoingServerSMTP,CompanyLogo From   ClientMaster where ClientMasterId='" + Session["ClientId"] + "' ";
      //  str = " SELECT ID, IncomingEmailServer AS ServerName, EmailName, InEmailID AS EmailId, InPassword AS Password, LastDownloadedTime, LastDownloadIndex, OutgoingEmailServer, OutEmailID AS Email, OutPassword, Whid FROM dbo.InOutCompanyEmail Where Whid=" + Session["whid"] + " ";
        SqlCommand cmd = new SqlCommand(str, epconn);
           
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            StringBuilder strplan = new StringBuilder();
            

            //" 
            strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
            strplan.Append("<tr><td align=\"left\"> <img src=\"http://license.busiwiz.com/images/" + imgpath + "\" \"border=\"0\" Width=\"90\" Height=\"80\" / > </td ></tr>  ");
            //strplan.Append("<br></table> ");
            strplan.Append("<tr><td align=\"left\" style=\"width: 20%\"><br><br><b> Dear Admin, </b> <br><br> </td></tr>");//" + dt.Rows[0]["ContactPersonName"].ToString() + "
            strplan.Append("<tr><td align=\"left\" style=\"width: 20%\"> Following are IT Department work report of all employee for  " + date + " At " + time + "  <br></td></tr>");
            strplan.Append("</table> ");
            foreach (GridViewRow gdr in GridView2.Rows)
            {
                StringBuilder strplanold = new StringBuilder();
                strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
                strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">----------------------------------------------------------------</td></tr>");//" + dt.Rows[0]["ContactPersonName"].ToString() + "
                strplan.Append("</table> ");
                //Product				
                //Work Title		
                //Bud Hrs		
                //Actual Hrs Spent Sofar		
                //Work Status		
                //Work Report	
                Label lbl_emp = (Label)gdr.FindControl("lblempname");
                Label lblworktype = (Label)gdr.FindControl("lblworktype");                
                Label lblproduct = (Label)gdr.FindControl("lblprodect");
                Label lbltitle = (Label)gdr.FindControl("lblworktobedone");
                Label lbl_status = (Label)gdr.FindControl("lbltodaytaskstatus");
                Label lbl_reporttoday = (Label)gdr.FindControl("lblworkreport");
                Label lbl_maintitle = (Label)gdr.FindControl("lblworktitle12345");
               
                Label lbl_todayActHour = (Label)gdr.FindControl("lblactualhour");
                Label lbl_todayBufHour = (Label)gdr.FindControl("lblbudgetd132");
                Label lbl_projectBufHour = (Label)gdr.FindControl("lblbudhrmaintask");

                Label lbl_mainid = (Label)gdr.FindControl("lbl_mainid");



                Label lblmasterId = (Label)gdr.FindControl("lblmasterId");
                Label lblempid = (Label)gdr.FindControl("lblempid");
                string strold = " select * From MyDailyWorkReport Where id !='" + lbl_mainid.Text + "' and PageWorkTblId='" + lblmasterId.Text + "' and EmployeeId='" + lblempid.Text + "'";
                SqlCommand cmdold = new SqlCommand(strold, con);
                SqlDataAdapter adpold = new SqlDataAdapter(cmdold);
                DataTable dtold = new DataTable();
                adpold.Fill(dtold);
                if (dtold.Rows.Count > 0)
                {

                    foreach (DataRow dr in dtold.Rows)
                    {
                        string dateold = "";
                        string workold = "";
                        try
                        {

                            dateold = dr["workallocationdate"].ToString();
                            DateTime nowold = Convert.ToDateTime(dr["workallocationdate"].ToString());
                            dateold = nowold.GetDateTimeFormats('d')[0];
                            workold = dr["WorkDoneReport"].ToString();
                        }
                        catch (Exception ex)
                        {
                        }
                        if (dateold != "")
                        {
                            strplanold.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
                            strplanold.Append("<tr><td align=\"left\" style=\"width: 20%\"><b>Report(" + dateold + "): </b>" + workold + "  </td></tr>");//" + dt.Rows[0]["ContactPersonName"].ToString() + "
                            strplanold.Append("</table> ");
                        }
                    }
                }
                string bodyformateold = "" + strplanold + "";

                string str_todayActHour = "00.00";
                try
                {
                    str_todayActHour = lbl_todayActHour.Text;
                }
                catch (Exception ex)
                {
                }

                string str_todayBufHour = "00.00";
                try
                {
                    str_todayBufHour = lbl_todayBufHour.Text;
                }
                catch (Exception ex)
                {
                }

                string str_reporttoday="00.00";                
                try
                {
                    str_reporttoday = lbl_reporttoday.Text;
                }
                catch (Exception ex)
                {
                }

                string str_projectBufHour = "00.00";
                try
                {
                    str_projectBufHour = lbl_projectBufHour.Text;
                }
                catch (Exception ex)
                {
                }

                Label lbl_projActHour = (Label)gdr.FindControl("lblactualhrmaintask");
                string str_projActHour = "00.00";
                try
                {
                    str_projActHour = lbl_projActHour.Text;
                }
                catch (Exception ex)
                {
                }
                //strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
                //strplan.Append("<tr><td align=\"left\" style=\"width: 50%\"><b>Name Of Employee :</b>  </td><td align=\"left\" style=\"width: 70%\">" + lbl_emp.Text + "</td></tr>");
                //strplan.Append("<tr><td align=\"left\" style=\"width: 30%\"><br><b>Product Name:<b> </td><td align=\"left\" style=\"width: 70%\">" + lblproduct.Text + "</td></tr>");

                //strplan.Append("<tr><td align=\"left\" style=\"width: 30%\"><b> Main Work </b>  </td><td align=\"left\" style=\"width: 70%\"></td></tr>");
                //strplan.Append("<tr><td align=\"left\" style=\"width: 30%\">Title : </td><td align=\"left\" style=\"width: 70%\">" + lbl_maintitle.Text + "</td></tr>");
                //strplan.Append("<tr><td align=\"left\" style=\"width: 30%\">Bug. Hour :" + str_projectBufHour + " </td><td align=\"left\" style=\"width: 70%\">Actual Hrs Spent Sofar: " + str_projActHour + "</td></tr>");
                //strplan.Append("<tr><td align=\"left\" style=\"width: 30%\"></td><td align=\"left\" style=\"width: 70%\"></td></tr>");

                //strplan.Append("<tr><td align=\"left\" style=\"width: 30%\"><b> Today's Work </b>  </td><td align=\"left\" style=\"width: 70%\"></td></tr>");
                //strplan.Append("<tr><td align=\"left\" style=\"width: 30%\"><b>Work Title : </b></td><td align=\"left\" style=\"width: 70%\">" + lbltitle.Text + "</td></tr>");
                //strplan.Append("<tr><td align=\"left\" style=\"width: 30%\"><b>Bug. Hour :</b> " + lbl_todayBufHour.Text + "</td><td align=\"left\" style=\"width: 70%\"><b>Act Hrs Spent:</b>  " + lbl_todayActHour.Text + "</td></tr>");
                //strplan.Append("<tr><td align=\"left\" style=\"width: 30%\"> </td><td align=\"left\" style=\"width: 70%\"></td></tr>");
                //strplan.Append("<tr><td align=\"left\" style=\"width: 30%\"><b>Work Status :</b> </td><td align=\"left\" style=\"width: 70%\">" + lbl_status.Text + "</td></tr>");
                //strplan.Append("<tr><td align=\"left\" style=\"width: 30%\"><b>Work Report :</b> </td><td align=\"left\" style=\"width: 70%\">" + lbl_reporttoday.Text + "</td></tr>");
                //strplan.Append("<tr><td align=\"left\" style=\"width: 20%\"><br></td></tr>");
                //strplan.Append("</table> ");
                strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> " +
                 "<tr><td align=\"left\" style=\"width: 100%\" colspan='2'><b>Name of Employee :</b> " + lbl_emp.Text + " (" + lblworktype .Text+ ")</td></tr> " +
                 "<tr><td align=\"left\" style=\"width: 100%\" colspan='2' valign='top'><b> Product Name:</b>" + lblproduct.Text + "</td></tr> " +
                 "<tr><td align=\"left\" style=\"width: 100%\" colspan='2'><br> <b><u> Main Work</u></b>  </td></tr> " +
                 "<tr><td align=\"left\" style=\"width: 100%\" colspan='2'><strong>Title :</strong> " + lbl_maintitle.Text + "</td></tr> " +
                 "<tr><td align=\"left\" style=\"width: 100%\" colspan='2'><b>Bug. Hour :</b>" + str_projectBufHour + "  <b>Act Hrs Spent:</b> " + str_projActHour + "</td></tr> " +
                 "<tr><td align=\"left\" style=\"width: 100%\" colspan='2'><b></b> " + bodyformateold + "</td></tr> " +
                 "<tr><td align=\"left\" style=\"width: 100%\" colspan='2'></td></tr> " +
                 "<tr><td align=\"left\" style=\"width: 100%\" colspan='2'> " +
                 "<b><u> Today's Work</u> </b>  </td></tr> " +
                 "<tr><td align=\"left\" style=\"width: 100%\" colspan='2'><b>Work Title : </b>" + lbltitle.Text + "</td></tr> " +
                 "<tr><td align=\"left\" style=\"width: 100%\"  colspan='2'><b>Bug. Hour :</b> " + str_todayBufHour + " <b>Act Hrs Spent:</b>  " + str_todayActHour + "</td></tr> " +
                 "<tr><td align=\"left\" style=\"width: 100%\" colspan='2'> </td></tr> " +
                 "<tr><td align=\"left\" style=\"width: 100%\" colspan='2'><b>Work Status :</b> " + lbl_status.Text + "</td></tr> " +
                 "<tr><td align=\"left\" style=\"width: 100%\" colspan='2'><b>Work Report :</b> " + lbl_reporttoday.Text + "</td></tr> " +
                 "<tr><td align=\"left\" style=\"width: 100%\"><br></td></tr> " +
                 " </table> ");
            }

            string bodyformate = "" + strplan + "";
            //try
            //{


            MailAddress to = new MailAddress("" + dt.Rows[0]["Emailid"].ToString() + "");//acharyaandassociate@gmail.com operationsmanager@herrykem.com  dt.Rows[0]["VSirEmail"].ToString()
            MailAddress from = new MailAddress(dt.Rows[0]["Email"].ToString(), "Daily IT work report");//donot_reply@ijobcenter.com  **Sales Team IJobCenter 
            MailMessage objEmail = new MailMessage(from, to);
            objEmail.Subject = " Daily IT work status report " + date + " : " + time + " ";
            //IP Banned due to failed login attempt by company<company>
            objEmail.Body = bodyformate.ToString();
            objEmail.IsBodyHtml = true;
            objEmail.Priority = MailPriority.High;
            SmtpClient client = new SmtpClient();
            client.Credentials = new NetworkCredential(dt.Rows[0]["EmailID"].ToString(), dt.Rows[0]["Password"].ToString()); //dt.Rows[0]["UserIdtosendmail"].ToString(), dt.Rows[0]["Password"].ToString()donot_reply@ijobcenter.com  **Om2012++
            //client.Host = dt.Rows[0]["Mailserverurl"].ToString();
            client.Host = dt.Rows[0]["ServerName"].ToString();
            //client.Port = 587;
            client.Send(objEmail);
            lblmsg.Text = "Mail Send Sucessfully";
            lblmsg.Visible = true;   
        }
        else
        {
            lblmsg.Text = "no Any Data for send mail to admin";
                lblmsg.Visible = true;
            }
    }
    }
}
