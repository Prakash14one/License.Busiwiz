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
using System.IO;
using System.Runtime.InteropServices;
using System.Net;
using System.Web.Configuration;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Net.Mail;

public partial class MyPayroll : System.Web.UI.Page
{
    SqlConnection con;
    string li = "[" + PageConn.licenseconn().Database + "]";
    public static int Gindex = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        }
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        if (!IsPostBack)
        {
            fillstore();
            ddlstproductname_SelectedIndexChanged(sender, e);

            DataTable dtc = select("select distinct EmployeeName from EmployeeMaster where EmployeeMaster.EmployeeMasterId='" + Session["EmployeeId"] + "'");
            if (dtc.Rows.Count > 0)
            {
                lblempn.Text = Convert.ToString(dtc.Rows[0]["EmployeeName"]);
            }
        }
    }
    //public void FillProduct()
    //{
    //    string strcln = " SELECT distinct ProductMaster.ProductId,ProductDetail.Active,VersionInfoMaster.VersionInfoId,ProductMaster.ProductName + ' : ' + VersionInfoMaster.VersionInfoName as productversion FROM ProductMaster inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId inner join VersionInfoMaster on VersionInfoMaster.ProductId= ProductMaster.ProductId where ProductDetail.Active='1'  order  by productversion";
    //    SqlCommand cmdcln = new SqlCommand(strcln, PageConn.licenseconn());
    //    DataTable dtcln = new DataTable();
    //    SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
    //    adpcln.Fill(dtcln);
    //    ddlstproductname.DataSource = dtcln;
    //    ddlstproductname.DataValueField = "VersionInfoId";
    //    ddlstproductname.DataTextField = "productversion";
    //    ddlstproductname.DataBind();
    //    ddlstproductname.Items.Insert(0, "All");
    //    ddlstproductname.Items[0].Value = "0";
    //}
    protected void fillstore()
    {
        DataTable ds1 = ClsStore.SelectStorename();
        if (ds1.Rows.Count > 0)
        {
            ddlstproductname.DataSource = ds1;
            ddlstproductname.DataTextField = "Name";
            ddlstproductname.DataValueField = "WarehouseId";
            ddlstproductname.DataBind();
            //ddlsearchByStore.Items.Insert(0, "All");
            //ddlsearchByStore.Items[0].Value = "0";
            DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

            if (dteeed.Rows.Count > 0)
            {
                ddlstproductname.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
            }

        }

    }
    protected void ddlstproductname_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        lblproduct.Text = ddlstproductname.SelectedItem.ToString();
        if (ddlstproductname.SelectedIndex >= 0)
        {
            string st = "select distinct  CountryMaster.CountryName,CountryMaster.CountryId,StateMasterTbl.Statename from EmployeeMaster" +
               " inner join CountryMaster on  CountryMaster.CountryId=EmployeeMaster.CountryId  inner join StateMasterTbl on " +
                                 "StateMasterTbl.StateId=EmployeeMaster.StateId" +
               " where EmployeeMaster.Whid='" + ddlstproductname.SelectedValue + "' and EmployeeMaster.EmployeeMasterId='" + Session["EmployeeId"] + "'";
            SqlCommand cmd = new SqlCommand(st, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            ViewState["countryid"] = "";
            lblheadmessage.Text = "Payroll related government forms";
            if (dt.Rows.Count > 0)
            {

                lblheadmessage.Text = "Payroll related government forms (Applicable to " + Convert.ToString(dt.Rows[0]["Statename"]) + "/" + Convert.ToString(dt.Rows[0]["CountryName"]) + ")";
                ViewState["countryid"] = dt.Rows[0]["CountryId"].ToString();
            }
        }
        fill_grid();
    }
    public void fill_grid()
    {
        GridView1.DataSource = null;
        GridView1.DataBind();
        string str1 = "select distinct " + PageConn.busdatabase + " .dbo.CountryPayrollpagesmasterTBL.ID, " + PageConn.busdatabase + " .dbo.CountryPayrollpagesmasterTBL.CountryID," +
         "" + PageConn.busdatabase + ".dbo.CountryPayrollpagesmasterTBL.PageName," + PageConn.busdatabase + ".dbo.CountryPayrollpagesmasterTBL.Pagetitle," + PageConn.busdatabase + ".dbo.CountryPayrollpagesmasterTBL.Section," +
         "" + PageConn.busdatabase + ".dbo.CountryPayrollpagesmasterTBL.VersionInfoID," +
         "" + PageConn.busdatabase + ".dbo.CountryMaster.CountryId," + PageConn.busdatabase + ".dbo.CountryMaster.CountryName," +

         "" + li + ".dbo.ProductMaster.ProductId," + li + ".dbo.VersionInfoMaster.ProductId," +
         "" + li + ".dbo.VersionInfoMaster.VersionInfoId," + li + ".dbo.VersionInfoMaster.VersionInfoName," +
         "" + li + ".dbo.ProductMaster.ProductName + ' : ' + " + li + ".dbo.VersionInfoMaster.VersionInfoName as productversion," +
         "" + li + ".dbo.PageMaster.PageId," + li + ".dbo.PageMaster.PageName as pgnm," +
         "" + li + ".dbo.PageMaster.PageTitle as pgtit, " + li + ".dbo.PageMaster.PageDescription as pgdesc  from " +
         "" + PageConn.busdatabase + ".dbo.CountryPayrollpagesmasterTBL " +
         " inner join  " + PageConn.busdatabase + ".dbo.CountryMaster on " + PageConn.busdatabase + ".dbo.CountryPayrollpagesmasterTBL.CountryID=" + PageConn.busdatabase + ".dbo.CountryMaster.CountryId" +

         " inner join" + li + ".dbo.VersionInfoMaster ON " + li + ".dbo.VersionInfoMaster.VersionInfoId=" + PageConn.busdatabase + ".dbo.CountryPayrollpagesmasterTBL.VersionInfoID" +
         " inner join" + li + ".dbo.ProductMaster ON " + li + ".dbo.ProductMaster.ProductId=" + li + ".dbo.VersionInfoMaster.ProductId" +


         " inner join " + li + ".dbo.PageMaster on " + PageConn.busdatabase + ".dbo.CountryPayrollpagesmasterTBL.PageName=" + li + ".dbo.PageMaster.PageId where CountryPayrollpagesmasterTBL.Section='0' ";

        //string str1 = "select distinct  " + PageConn.busdatabase + " .dbo. CompanyWebsiteAddressMaster.CompanyWebsiteMasterId," + PageConn.busdatabase + " .dbo. CompanyWebsiteAddressMaster.Country," +
        //    " " + PageConn.busdatabase + " .dbo.WareHouseMaster.Name as BName," +
        //" " + PageConn.busdatabase + " .dbo.CountryPayrollpagesmasterTBL.ID, " + PageConn.busdatabase + " .dbo.CountryPayrollpagesmasterTBL.CountryID," +
        // "" + PageConn.busdatabase + ".dbo.CountryPayrollpagesmasterTBL.PageName," + PageConn.busdatabase + ".dbo.CountryPayrollpagesmasterTBL.Pagetitle," + PageConn.busdatabase + ".dbo.CountryPayrollpagesmasterTBL.Section," +
        // "" + PageConn.busdatabase + ".dbo.CountryPayrollpagesmasterTBL.VersionInfoID," +
        // "" + PageConn.busdatabase + ".dbo.CountryMaster.CountryId," + PageConn.busdatabase + ".dbo.CountryMaster.CountryName," +

        // "" + li + ".dbo.ProductMaster.ProductId," + li + ".dbo.VersionInfoMaster.ProductId," +
        // "" + li + ".dbo.VersionInfoMaster.VersionInfoId," + li + ".dbo.VersionInfoMaster.VersionInfoName," +
        // "" + li + ".dbo.ProductMaster.ProductName + ' : ' + " + li + ".dbo.VersionInfoMaster.VersionInfoName as productversion," +
        // "" + li + ".dbo.PageMaster.PageId," + li + ".dbo.PageMaster.PageName as pgnm," +
        // "" + li + ".dbo.PageMaster.PageTitle as pgtit, " + li + ".dbo.PageMaster.PageDescription as pgdesc  from " +
        // "" + PageConn.busdatabase + ".dbo.CountryPayrollpagesmasterTBL " +
        // " inner join  " + PageConn.busdatabase + ".dbo.CountryMaster on " + PageConn.busdatabase + ".dbo.CountryPayrollpagesmasterTBL.CountryID=" + PageConn.busdatabase + ".dbo.CountryMaster.CountryId" +

        // " inner join" + li + ".dbo.VersionInfoMaster ON " + li + ".dbo.VersionInfoMaster.VersionInfoId=" + PageConn.busdatabase + ".dbo.CountryPayrollpagesmasterTBL.VersionInfoID" +
        // " inner join" + li + ".dbo.ProductMaster ON " + li + ".dbo.ProductMaster.ProductId=" + li + ".dbo.VersionInfoMaster.ProductId" +


        // " inner join " + li + ".dbo.PageMaster on " + PageConn.busdatabase + ".dbo.CountryPayrollpagesmasterTBL.PageName=" + li + ".dbo.PageMaster.PageId" +


        //" inner join " + PageConn.busdatabase + " .dbo. CompanyWebsiteAddressMaster on" +
        //"" + PageConn.busdatabase + ".dbo.CompanyWebsiteAddressMaster.Country =" + PageConn.busdatabase + ".dbo.CountryPayrollpagesmasterTBL.CountryID" +
        //" inner join  "+ PageConn.busdatabase + ".dbo.WareHouseMaster on" +
        //"" + PageConn.busdatabase + ".dbo.WareHouseMaster.WareHouseId=" + PageConn.busdatabase + ".dbo.CompanyWebsiteAddressMaster.CompanyWebsiteMasterId ";






        string str2 = "";
        //if (ddlstproductname.SelectedIndex > 0)
        //{
        //    str2 = " and " + PageConn.busdatabase + ".dbo.CountryPayrollpagesmasterTBL.VersionInfoID='" + ddlstproductname.SelectedValue + "'";
        //}
        if (ddlstproductname.SelectedIndex >= 0)
        {
            if (ViewState["countryid"].ToString() == "" || ViewState["countryid"].ToString() == null)
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
            else
            {
                str2 = " and " + PageConn.busdatabase + ".dbo.CountryPayrollpagesmasterTBL.CountryID='" + ViewState["countryid"].ToString() + "'";
            }
        }
        string st = str1 + str2;
        SqlDataAdapter adpcln = new SqlDataAdapter(st, con);
        DataTable dtcln = new DataTable();
        adpcln.Fill(dtcln);
        if (dtcln.Rows.Count > 0)
        {
            GridView1.DataSource = dtcln;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
        if (ddlstproductname.SelectedIndex >= 0)
        {
            if (ViewState["countryid"].ToString() == "" || ViewState["countryid"].ToString() == null)
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //    Label lblim = (Label)e.Row.FindControl("lblidm");
            //    Label lblStatename = (Label)e.Row.FindControl("lblStatename");
            //    string st = "select distinct CountrypayrollPageMasterTbl_Sub.provinceID, StateMasterTbl.StateName  from StateMasterTbl inner join CountrypayrollPageMasterTbl_Sub on CountrypayrollPageMasterTbl_Sub.provinceID= StateMasterTbl.StateId where CountrypayrollPageMasterTbl_Sub.CountrypayrollpageMasterTBL_ID='" + lblim.Text + "'";
            //    SqlCommand cm = new SqlCommand(st, con);
            //    SqlDataAdapter dta = new SqlDataAdapter(cm);
            //    DataTable dt = new DataTable();
            //    dta.Fill(dt);
            //    if (dt.Rows.Count > 0)
            //    {
            //        for (int i = 0; i < dt.Rows.Count; i++)
            //        {
            //            if (lblStatename.Text != "")
            //            {
            //                lblStatename.Text = lblStatename.Text + "," + dt.Rows[i]["StateName"].ToString();
            //            }
            //            else
            //            {
            //                lblStatename.Text = lblStatename.Text + dt.Rows[i]["StateName"].ToString();
            //            }
            //        }

            //    }
            //    else
            //    {
            //        lblStatename.Text = "All";
            //    }
            Label lblpn = (Label)e.Row.FindControl("lblpagename");
            LinkButton lnkbtnpgnm1 = (LinkButton)e.Row.FindControl("lnkbtnpgnm1");
            string pnu = lblpn.Text.ToUpper();
            DataTable dtc = select("Select distinct TaxYear_Name,TaxYear_Id from  Tax_Year  where CountryId='" + Convert.ToString(ViewState["countryid"]) + "' and TaxYear_Name='" + DateTime.Now.Year + "' and Active='1'");
            if (dtc.Rows.Count > 0)
            {
                string Datat = "select distinct CountryMaster.CountryId from EmployeeMaster" +
               " inner join CountryMaster on  CountryMaster.CountryId=EmployeeMaster.CountryId  inner join StateMasterTbl on " +
                                 "StateMasterTbl.StateId=EmployeeMaster.StateId" +
               " where EmployeeMaster.Whid='" + ddlstproductname.SelectedValue + "' and EmployeeMaster.EmployeeMasterId='" + Session["EmployeeId"] + "'";
                string Datatz = "select distinct StateMasterTbl.StateId from EmployeeMaster" +
             " inner join CountryMaster on  CountryMaster.CountryId=EmployeeMaster.CountryId  inner join StateMasterTbl on " +
                               "StateMasterTbl.StateId=EmployeeMaster.StateId" +
             " where EmployeeMaster.Whid='" + ddlstproductname.SelectedValue + "' and EmployeeMaster.EmployeeMasterId='" + Session["EmployeeId"] + "'";

                DataTable dtcDD = select("Select distinct Filename,RelatedPagename,PayrolltaxdetailId from Tax_Year inner join [PayRollTaxDetail] on PayRollTaxDetail.TaxYearId=Tax_Year.TaxYear_Id inner join PayrolltaxMaster on PayrolltaxMaster.Payrolltax_id=PayRollTaxDetail.Payrolltaxmasterid  inner join Payrolltaxmasterwithstate on Payrolltaxmasterwithstate.PayrolltaxMasterId=PayrolltaxMaster.Payrolltax_id inner join PayrolltaxdetailforRelatedPage on PayrolltaxdetailforRelatedPage.PayrolltaxdetailId=PayRollTaxDetail.Id where Upper(RelatedPagename)='" + pnu + "' and Payrolltaxmasterwithstate.StateId in (" + Datatz + ")and Tax_Year.TaxYear_Id='" + dtc.Rows[0]["TaxYear_Id"] + "' ");
                if (dtcDD.Rows.Count == 0)
                {
                    dtcDD = select("Select distinct Filename,RelatedPagename,PayrolltaxdetailId from Tax_Year inner join [PayRollTaxDetail] on PayRollTaxDetail.TaxYearId=Tax_Year.TaxYear_Id inner join PayrolltaxMaster on PayrolltaxMaster.Payrolltax_id=PayRollTaxDetail.Payrolltaxmasterid  inner join PayrolltaxdetailforRelatedPage on PayrolltaxdetailforRelatedPage.PayrolltaxdetailId=PayRollTaxDetail.Id where  Upper(RelatedPagename)='" + pnu + "' and  PayrolltaxMaster.Country_id in (" + Datat + ") and Tax_Year.TaxYear_Id='" + dtc.Rows[0]["TaxYear_Id"] + "'");

                }
                if (dtcDD.Rows.Count > 0)
                {
                    string filepath = Server.MapPath("~\\ShoppingCart\\Admin\\Files\\" + Convert.ToString(dtcDD.Rows[0]["Filename"]));

                    FileInfo file = new FileInfo(filepath);
                    if (!file.Exists)
                    {
                        filepath = "1133.busiwiz.com\\ShoppingCart\\Admin\\Files\\" + Convert.ToString(dtcDD.Rows[0]["Filename"]);
                    }
                    lnkbtnpgnm1.Text = "Download Form";
                    lnkbtnpgnm1.Enabled = true;
                    //  lnkbtnpgnm1.PostBackUrl = filepath;
                }
                else
                {
                    lnkbtnpgnm1.Text = "Not Available";
                    lnkbtnpgnm1.Enabled = false;
                }
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            lblproduct.Text = ddlstproductname.SelectedItem.ToString();
            Button1.Text = "Hide Printable Version";
            Button4.Visible = true;
        }
        else
        {
            Button1.Text = "Printable Version";
            Button4.Visible = false;
        }
    }
    protected void lblpen_Click11(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        GridViewRow iten = ((LinkButton)sender).Parent.Parent as GridViewRow;
        int rinrow = iten.RowIndex;
        Gindex = rinrow;

        if (Convert.ToString(ViewState["countryid"]) != "")
        {
            Label lblpn = (Label)GridView1.Rows[Gindex].FindControl("lblpagename");
            string pnu = lblpn.Text.ToUpper();
            DataTable dtc = select("Select distinct TaxYear_Name,TaxYear_Id from  Tax_Year  where CountryId='" + Convert.ToString(ViewState["countryid"]) + "' and TaxYear_Name='" + DateTime.Now.Year + "' and Active='1'");
            if (dtc.Rows.Count > 0)
            {
                string Datat = "select distinct CountryMaster.CountryId from EmployeeMaster" +
            " inner join CountryMaster on  CountryMaster.CountryId=EmployeeMaster.CountryId  inner join StateMasterTbl on " +
                              "StateMasterTbl.StateId=EmployeeMaster.StateId" +
            " where EmployeeMaster.Whid='" + ddlstproductname.SelectedValue + "' and EmployeeMaster.EmployeeMasterId='" + Session["EmployeeId"] + "'";
                string Datatz = "select distinct StateMasterTbl.StateId from EmployeeMaster" +
             " inner join CountryMaster on  CountryMaster.CountryId=EmployeeMaster.CountryId  inner join StateMasterTbl on " +
                               "StateMasterTbl.StateId=EmployeeMaster.StateId" +
             " where EmployeeMaster.Whid='" + ddlstproductname.SelectedValue + "' and EmployeeMaster.EmployeeMasterId='" + Session["EmployeeId"] + "'";

                DataTable dtcDD = select("Select distinct Filename,RelatedPagename,PayrolltaxdetailId from Tax_Year inner join [PayRollTaxDetail] on PayRollTaxDetail.TaxYearId=Tax_Year.TaxYear_Id inner join PayrolltaxMaster on PayrolltaxMaster.Payrolltax_id=PayRollTaxDetail.Payrolltaxmasterid  inner join Payrolltaxmasterwithstate on Payrolltaxmasterwithstate.PayrolltaxMasterId=PayrolltaxMaster.Payrolltax_id inner join PayrolltaxdetailforRelatedPage on PayrolltaxdetailforRelatedPage.PayrolltaxdetailId=PayRollTaxDetail.Id where Upper(RelatedPagename)='" + pnu + "' and Payrolltaxmasterwithstate.StateId in (" + Datatz + ")and Tax_Year.TaxYear_Id='" + dtc.Rows[0]["TaxYear_Id"] + "' ");
                if (dtcDD.Rows.Count == 0)
                {
                    dtcDD = select("Select distinct Filename,RelatedPagename,PayrolltaxdetailId from Tax_Year inner join [PayRollTaxDetail] on PayRollTaxDetail.TaxYearId=Tax_Year.TaxYear_Id inner join PayrolltaxMaster on PayrolltaxMaster.Payrolltax_id=PayRollTaxDetail.Payrolltaxmasterid  inner join PayrolltaxdetailforRelatedPage on PayrolltaxdetailforRelatedPage.PayrolltaxdetailId=PayRollTaxDetail.Id where  Upper(RelatedPagename)='" + pnu + "' and  PayrolltaxMaster.Country_id in (" + Datat + ") and Tax_Year.TaxYear_Id='" + dtc.Rows[0]["TaxYear_Id"] + "'");

                }
                if (dtcDD.Rows.Count > 0)
                {
                    string filepath = Server.MapPath("~\\ShoppingCart\\Admin\\Files\\" + Convert.ToString(dtcDD.Rows[0]["Filename"]));

                    FileInfo file = new FileInfo(filepath);
                    if (!file.Exists)
                    {
                        filepath = "1133.busiwiz.com\\ShoppingCart\\Admin\\Files\\" + Convert.ToString(dtcDD.Rows[0]["Filename"]);
                    }

                    //if (file.Exists)
                    //{
                    Response.ClearContent();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + Convert.ToString(dtcDD.Rows[0]["Filename"]));
                    //Response.AddHeader("Content-Length", file.Length.ToString());
                    Response.ContentType = "application/pdf";
                    Response.TransmitFile(filepath);

                    Response.End();
                }
                else
                {
                    lblmsg.Text = "No file found.";
                }
            }
        }
    }

    protected DataTable select(string qu)
    {
        SqlCommand cmd = new SqlCommand(qu, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        return dt;

    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "pg")
        {
            String strcln = " SELECT PageId,PageName From PageMaster where PageId='" + e.CommandArgument.ToString() + "'";
            SqlCommand cmdcln = new SqlCommand(strcln, PageConn.licenseconn());
            SqlDataAdapter da = new SqlDataAdapter(cmdcln);
            DataTable dt = new DataTable();
            da.Fill(dt);
            string nm = dt.Rows[0]["PageName"].ToString();

            string te = nm.ToString();
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        }
    }
}





