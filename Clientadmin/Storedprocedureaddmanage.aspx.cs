using System.IO.Compression;
using System.Security.Cryptography;
using Microsoft.Win32;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Text;
using System.Data;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System.Collections.Specialized;
using Microsoft.SqlServer.Management.Smo;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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
using System.Data.SqlClient;
using System.DirectoryServices;
using System.IO.Compression;
using System.IO;
using Ionic.Zip;
using System.Net;
using System.Security.Cryptography;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

public partial class procedureadd : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    SqlConnection conn1 = new SqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    {     
        if (!IsPostBack)
        {
            ModalPopupExtender2.Hide(); 
            FillTXT();
            Session[" userid"] = Convert.ToInt32(Session["Id"]); 
            FillProduct();
            fillwebsite();

            FillProductSearch();
            fillgrid();
                      
            Panel4.Visible = false;
            pnl_addPROCEDURE.Visible = false;  
            ViewState["sortOrder"] = "";
        }
    }
    protected void addnewpanel_Click(object sender, EventArgs e)
    {
        ModernpopSync.Show();      
    }
    protected void btn_selecoption_Click(object sender, EventArgs e)
    {
        btn_addnewreco.Visible = false;
        pnl_addPROCEDURE.Visible = true;
        lblmsg.Text = "";
        ModernpopSync.Hide();
        if (rb_option.SelectedValue == "0")
        {
            txtprocedure.Enabled = true;            
        }
        else if (rb_option.SelectedValue == "1")
        {
            txtprocedure.Enabled = false;
        }
    }
    protected void FillTXT()
    {
       
    }
    protected void Clr()
    {
        FillProduct();
        fillwebsite();

        txtname.Text = "";
        txtdescription.Text = "";
        txtprocedure.Text = "";
        txtsearch.Text = "";
      
        txtpagesearch.Text = "";
        
        gv_selectprocedure.DataSource = null;
        gv_selectprocedure.DataBind();

        GridView1.DataSource = null;
        GridView2.DataBind();
    }
    protected void fillwebsite()
    {
        DataTable dtCodeVersionID = MyCommonfile.selectBZ("SELECT dbo.WebsiteMaster.ID, dbo.WebsiteMaster.WebsiteName From Websitemaster Where  dbo.WebsiteMaster.VersionInfoId='" + ddlProductname.SelectedValue + "'  order  by WebsiteName ");
        ddlwebsite.DataSource = dtCodeVersionID;
        ddlwebsite.DataValueField = "ID";
        ddlwebsite.DataTextField = "WebsiteName";
        ddlwebsite.DataBind();
        ddlwebsite.Items.Insert(0, "--Select--");
        ddlwebsite.Items[0].Value = "0";
        ddlwebsite.SelectedIndex = 0; 
    }
    protected void FillProduct()
    {  
        string strcln = "SELECT  distinct  WebsiteMaster.ID ,VersionInfoMaster.VersionInfoId,WebsiteSection.WebsiteSectionId,  ProductMaster.ProductName + ':' +  VersionInfoMaster.VersionInfoName + ' : ' + WebsiteMaster.WebsiteName + ':' +   WebsiteSection.SectionName  as productversion  FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId inner join WebsiteMaster on WebsiteMaster.VersionInfoId=VersionInfoMaster.VersionInfoId inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.ID where ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "' and VersionInfoMaster.Active ='True' and ProductDetail.Active='1' order  by productversion asc";
        DataTable dtcln =MyCommonfile.selectBZ("SELECT distinct ProductMaster.ProductId, VersionInfoMaster.VersionInfoId,ProductMaster.ProductName + ' : ' + VersionInfoMaster.VersionInfoName as productversion,ProductMaster.ProductName FROM  dbo.ProductMaster INNER JOIN dbo.VersionInfoMaster ON dbo.ProductMaster.ProductId = dbo.VersionInfoMaster.ProductId INNER JOIN dbo.ProductDetail ON dbo.ProductMaster.ProductId = dbo.ProductDetail.ProductId AND dbo.VersionInfoMaster.VersionInfoName = dbo.ProductDetail.VersionNo where ClientMasterId=" + Session["ClientId"].ToString() + " and ProductDetail.Active ='True' and ProductDetail.Active='True' and VersionInfoMaster.Active='True'  order  by productversion");       
        ddlProductname.DataSource = dtcln;
        ddlProductname.DataValueField = "VersionInfoId";
        ddlProductname.DataTextField = "ProductName";
        ddlProductname.DataBind();
        
    }
    protected void ddlProductname_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillwebsite();
        fillproductid();
        fillCodetype();

        filldatalist();
        Filllistbox();      
    }

    protected void fillproductid()
    {
        DataTable dtcln = MyCommonfile.selectBZ("SELECT distinct ProductMaster.ProductId,  dbo.ClientMaster.ServerId  ,ProductMaster.Description,dbo.VersionInfoMaster.ServerMasterCodeSourceIISWebsitePath  FROM  dbo.ProductMaster INNER JOIN dbo.VersionInfoMaster ON dbo.ProductMaster.ProductId = dbo.VersionInfoMaster.ProductId INNER JOIN dbo.ProductDetail ON dbo.ProductDetail.VersionNo = dbo.VersionInfoMaster.VersionInfoName INNER JOIN dbo.ClientMaster ON dbo.ProductMaster.ClientMasterId = dbo.ClientMaster.ClientMasterId  where ProductMaster.ClientMasterId=" + Session["ClientId"].ToString() + " and ProductDetail.Active ='True' and VersionInfoMaster.VersionInfoId='" + ddlProductname.SelectedValue + "' ");
        if (dtcln.Rows.Count > 0)
        {            
            lbl_serverid.Text = dtcln.Rows[0]["ServerId"].ToString();
        }
    }  
    protected void fillCodetype()
    {
        DataTable dtcln = MyCommonfile.selectBZ(" SELECT DISTINCT TOP (100) PERCENT dbo.ProductCodeDetailTbl.Id, dbo.ProductCodeDetailTbl.ProductId, dbo.ProductCodeDetailTbl.CodeTypeName,dbo.CodeTypeTbl.CodeTypeCategoryId FROM dbo.CodeTypeTbl INNER JOIN dbo.CodeTypeCategory ON dbo.CodeTypeCategory.CodeMasterNo = dbo.CodeTypeTbl.CodeTypeCategoryId INNER JOIN dbo.ProductCodeDetailTbl ON dbo.CodeTypeTbl.ProductCodeDetailId = dbo.ProductCodeDetailTbl.Id  where  ProductCodeDetailTbl.Active='1' and CodeTypeTbl.ProductVersionId='" + ddlProductname.SelectedValue + "' and CodeTypeCategory.Id='2' and ProductCodeDetailTbl.Active=1 order  by dbo.ProductCodeDetailTbl.CodeTypeName ");
        ddlcodetype.DataSource = dtcln;
        ddlcodetype.DataValueField = "Id";
        ddlcodetype.DataTextField = "CodeTypeName";
        ddlcodetype.DataBind();
        ddlcodetype.Items.Insert(0, "--Select--");
        ddlcodetype.Items[0].Value = "0";
        ddlcodetype.SelectedIndex = 0; 
    }
    protected void ddlcodetype_SelectedIndexChanged(object sender, EventArgs e)
    {     
    }


    protected void txtname_TextChanged(object sender, EventArgs e)
    {
        lbl_errortxtname.Text = "";
        txtprocedure.Text ="";
        pnl_showgidproc.Visible = false;
        Int64 StorproTypeExisting=Convert.ToInt64(rb_option.SelectedValue); 
        SqlConnection conn = new SqlConnection();
        Boolean checkstore = true;
        Boolean servConn = true;
        DataTable dtcln = MyCommonfile.selectBZ(" SELECT dbo.CodeTypeTbl.Instancename, dbo.CodeTypeTbl.ID, dbo.CodeTypeTbl.ProductCodeDetailId, dbo.ClientMaster.ServerId FROM dbo.CodeTypeTbl INNER JOIN dbo.ClientMaster INNER JOIN dbo.ProductMaster ON dbo.ClientMaster.ClientMasterId = dbo.ProductMaster.ClientMasterId INNER JOIN dbo.VersionInfoMaster ON dbo.ProductMaster.ProductId = dbo.VersionInfoMaster.ProductId ON dbo.CodeTypeTbl.ProductVersionId = dbo.VersionInfoMaster.VersionInfoId where dbo.CodeTypeTbl.ProductCodeDetailId='" + ddlcodetype.SelectedValue + "' ");
        if (dtcln.Rows.Count > 0)
        {
            string serversqlinstancename = dtcln.Rows[0]["Instancename"].ToString();
            conn = ServerWizard.ServerDatabaseFromInstanceTCP(lbl_serverid.Text, serversqlinstancename, ddlcodetype.SelectedItem.Text);
            try
            {
                if (conn.State.ToString() != "Open")
                {
                    conn.Open();
                }
            }
            catch
            {
                servConn = false;
            }
            if (servConn == true)
            {
                int inv = 0;                
                string strnew = " Select name from sys.procedures  Where (name = '" + txtname.Text + "') ";
                SqlCommand cmdnew = new SqlCommand(strnew, conn);
                SqlDataAdapter danew = new SqlDataAdapter(cmdnew);
                DataTable dtnew = new DataTable();
                danew.Fill(dtnew);
                if (dtnew.Rows.Count == 0 && StorproTypeExisting == 1)
                {
                    DataTable dt = MyCommonfile.selectBZ("SELECT DISTINCT dbo.Proceduremaster.Name as name FROM  dbo.Proceduremaster INNER JOIN dbo.ProductCodeDetailTbl ON dbo.Proceduremaster.databaseid = dbo.ProductCodeDetailTbl.Id LEFT OUTER JOIN dbo.EmployeeMaster ON dbo.Proceduremaster.User_id = dbo.EmployeeMaster.Id WHERE Proceduremaster.proce_id !=0 and Proceduremaster.databaseid='" + ddlcodetype.SelectedValue + "'");
                    string currenttable = "";
                    string str1="";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        currenttable +="'"+ dt.Rows[i]["name"].ToString()+"'" +",";
                    }
                    if (currenttable.Length > 0)
                    {
                        currenttable = currenttable.Remove(currenttable.Length - 1);
                        str1 = " and name NOT IN(" + currenttable + ")";
                    }
                    strnew = " Select name from sys.procedures  Where (name like '%" + txtname.Text + "%') " + str1 + " ";
                    cmdnew = new SqlCommand(strnew, conn);
                    danew = new SqlDataAdapter(cmdnew);
                    dtnew = new DataTable();
                    danew.Fill(dtnew);
                    if (dtnew.Rows.Count > 0)
                    {
                        gv_selectprocedure.DataSource = dtnew;
                        gv_selectprocedure.DataBind();
                        pnl_showgidproc.Visible = true;
                    }
                    lbl_errortxtname.Text = "";
                }
                else if (StorproTypeExisting == 1)
                {
                    GridView5.DataSource = null;
                    GridView5.DataBind();
                    lbl_errortxtname.Text = "Name matching successfully";
                    lbl_errortxtname.ForeColor = Color.Green;
                    GetProcedureName(txtname.Text);   
                }
                else if (StorproTypeExisting == 0)
                {
                    string str1 = "";
                    str1 += " and Proceduremaster.databaseid='" + ddlcodetype.SelectedValue + "'";
                    str1 += " and dbo.Proceduremaster.Name='" + txtname.Text + "' ";
                    DataTable dt = MyCommonfile.selectBZ("SELECT DISTINCT dbo.Proceduremaster.Type, dbo.Proceduremaster.Name, dbo.Proceduremaster.Date, dbo.EmployeeMaster.Name AS employeename, dbo.Proceduremaster.proce_id AS id, dbo.ProductCodeDetailTbl.CodeTypeName FROM  dbo.Proceduremaster INNER JOIN dbo.ProductCodeDetailTbl ON dbo.Proceduremaster.databaseid = dbo.ProductCodeDetailTbl.Id LEFT OUTER JOIN dbo.EmployeeMaster ON dbo.Proceduremaster.User_id = dbo.EmployeeMaster.Id WHERE Proceduremaster.proce_id !=0 " + str1 + "");

                    if (dtnew.Rows.Count > 0 && dt.Rows.Count > 0)
                    {
                        lbl_errortxtname.Text = "Allredy exist this procedure in database (SQL Server " + ddlcodetype.SelectedItem.Text + " database) and also Proceduremaster table";
                        lbl_errortxtname.ForeColor = Color.Red;
                    }
                    else  if(dt.Rows.Count > 0)
                    {
                        lbl_errortxtname.Text = "Allredy exist this procedure in database (SQL Server " + ddlcodetype.SelectedItem.Text + " database)";
                        lbl_errortxtname.ForeColor = Color.Red;
                      
                    }
                   
                  
                   
                }
                else
                {
                }
            }
        }     
    }
    protected void lnk_selectstoreproce_Click(object sender, EventArgs e)
    {
        LinkButton lnkbtn = (LinkButton)sender;
        GridViewRow item = (GridViewRow)lnkbtn.NamingContainer;
        int i = Convert.ToInt32(item.RowIndex);
        Label lblproc = ((Label)gv_selectprocedure.Rows[i].FindControl("lbl_name"));
        GetProcedureName(lblproc.Text);       
    }
    protected void gv_selectprocedure_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void GetProcedureName(string procedurename)
    {
        SqlConnection conn = new SqlConnection();
        Boolean checkstore = true;
        Boolean servConn = true;
        DataTable dtcln = MyCommonfile.selectBZ(" SELECT dbo.CodeTypeTbl.Instancename, dbo.CodeTypeTbl.ID, dbo.CodeTypeTbl.ProductCodeDetailId, dbo.ClientMaster.ServerId FROM dbo.CodeTypeTbl INNER JOIN dbo.ClientMaster INNER JOIN dbo.ProductMaster ON dbo.ClientMaster.ClientMasterId = dbo.ProductMaster.ClientMasterId INNER JOIN dbo.VersionInfoMaster ON dbo.ProductMaster.ProductId = dbo.VersionInfoMaster.ProductId ON dbo.CodeTypeTbl.ProductVersionId = dbo.VersionInfoMaster.VersionInfoId where dbo.CodeTypeTbl.ProductCodeDetailId='" + ddlcodetype.SelectedValue + "' ");
        if (dtcln.Rows.Count > 0)
        {
            string serversqlinstancename = dtcln.Rows[0]["Instancename"].ToString();
            conn = ServerWizard.ServerDatabaseFromInstanceTCP(lbl_serverid.Text, serversqlinstancename, ddlcodetype.SelectedItem.Text);
            try
            {
                if (conn.State.ToString() != "Open")
                {
                    conn.Open();
                }
            }
            catch
            {
                servConn = false;
            }
            if (servConn == true)
            {
                string strnew = " Select name from sys.procedures  Where (name='" + procedurename + "') ";
                SqlCommand cmdnew = new SqlCommand(strnew, conn);
                SqlDataAdapter danew = new SqlDataAdapter(cmdnew);
                DataTable dtnew = new DataTable();
                danew.Fill(dtnew);
                if (dtnew.Rows.Count > 0)
                {
                    txtname.Text = dtnew.Rows[0]["name"].ToString();
                    //string srtr = " SELECT DISTINCT  o.id, o.name AS 'Procedure_Name' , oo.name AS 'Table_Name', d.depid FROM sysdepends d, sysobjects o, sysobjects oo WHERE    o.id=d.id  AND o.name= '" + txtname.Text + "'  AND oo.id=d.depid  ORDER BY o.name,oo.name ";
                    //SqlCommand cmdr = new SqlCommand(srtr, conn);
                    //SqlDataAdapter dar = new SqlDataAdapter(cmdr);
                    //DataTable dtr = new DataTable();
                    //dar.Fill(dtr);
                    string strda = " SELECT Object_definition(object_id) as procedurecode FROM   sys.procedures WHERE  name = '" + txtname.Text + "'";
                    SqlCommand cmdda = new SqlCommand(strda, conn);
                    SqlDataAdapter daas = new SqlDataAdapter(cmdda);
                    DataTable dts = new DataTable();
                    daas.Fill(dts);
                    if (dts.Rows.Count > 0)
                    {
                        txtprocedure.Text = dts.Rows[0]["procedurecode"].ToString();
                    }
                }
            }
        }
    }









    protected void txtsearch_TextChanged(object sender, EventArgs e)
    {    
    
    }
    protected void chkimg_CheckedChanged(object sender, EventArgs e)
    {
        if (chk_desc.Checked == true)
        {
            txtdescription.Visible = true;
        }
        else
        {
            txtdescription.Visible = false;
        }
    }
    protected void Filllistbox()    
    {
        string str = "";
        if (txtsearch.Text != "")
        {
            str += " and (ClientProductTableMaster.TableName like '%" + txtsearch.Text.Replace("'", "''") + "%')";
        }
        DataTable dtcln = MyCommonfile.selectBZ(" select distinct TableName, Id,VersionInfoId from ClientProductTableMaster where ClientProductTableMaster.Active=1 and Databaseid='" + ddlcodetype.SelectedValue + "' " + str + "  order by TableName  ");
        if (dtcln.Rows.Count > 0)
        {
            GridView5.DataSource = dtcln;
            GridView5.DataBind();
        }
        else
        {
            GridView5.DataSource = null;
            GridView5.DataBind();        
        }
    }  
    protected void Button2_Click(object sender, EventArgs e)
    {
      
                Button lnkbtn = (Button)sender;
                GridViewRow item = (GridViewRow)lnkbtn.NamingContainer;
                int i = Convert.ToInt32(item.RowIndex);
                Label itemid = ((Label)GridView6.Rows[i].FindControl("lbl_id"));
                Label lbl = ((Label)GridView6.Rows[i].FindControl("lnk21"));
                string lcl = lbl.Text;
                DataTable dt = new DataTable();
                if (Convert.ToString(ViewState["application1"]) == "")
                {

                    dt.Columns.Add("id");
                    dt.Columns.Add("PageName");
                }
                else
                {
                    dt = (DataTable)ViewState["application1"];
                }
                DataRow dr = dt.NewRow();
                dr["id"] = itemid.Text;
                dr["PageName"] = lcl.ToString();

                dt.Rows.Add(dr);
                GridView2.DataSource = dt;
                GridView2.DataBind();
                ViewState["application1"] = dt;

    }
    protected void txtpagesearch_TextChanged(object sender, EventArgs e)
    {
    }
    protected void filldatalist()
    {
        string str1 = "";

        if (txtpagesearch.Text != "")
        {
            str1 += " and ((PageMaster.PageName like '%" + txtpagesearch.Text.Replace("'", "''") + "%') )";
        }
        if (ddlwebsite.SelectedIndex > 0)
        {
            str1 += " and   dbo.WebsiteMaster.ID='" + ddlwebsite.SelectedValue + "' ";
        }
        str1 = " SELECT DISTINCT dbo.PageMaster.PageName, dbo.PageMaster.PageId, dbo.WebsiteSection.WebsiteSectionId, dbo.MasterPageMaster.MasterPageId, dbo.MainMenuMaster.MasterPage_Id, dbo.WebsiteMaster.WebsiteName FROM dbo.PageMaster INNER JOIN dbo.VersionInfoMaster ON dbo.PageMaster.VersionInfoMasterId = dbo.VersionInfoMaster.VersionInfoId INNER JOIN dbo.ProductMaster ON dbo.ProductMaster.ProductId = dbo.VersionInfoMaster.ProductId INNER JOIN dbo.MainMenuMaster ON dbo.PageMaster.MainMenuId = dbo.MainMenuMaster.MainMenuId INNER JOIN dbo.MasterPageMaster ON dbo.MainMenuMaster.MasterPage_Id = dbo.MasterPageMaster.MasterPageId INNER JOIN dbo.WebsiteSection ON dbo.MasterPageMaster.WebsiteSectionId = dbo.WebsiteSection.WebsiteSectionId INNER JOIN dbo.WebsiteMaster ON dbo.WebsiteSection.WebsiteMasterId = dbo.WebsiteMaster.ID where dbo.VersionInfoMaster.VersionInfoId='" + ddlProductname.SelectedValue + "' " + str1 + "";
        SqlCommand cmdcln = new SqlCommand(str1, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        if (dtcln.Rows.Count > 0)
        {
            GridView6.DataSource = dtcln;
            GridView6.DataBind();
        }
        else
        {
            GridView6.DataSource = null;
            GridView6.DataBind();        
        }
    }

    protected void lnk20_Click(object sender, EventArgs e)
    {
        
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
      
    }
    protected void ClassFileCreating(string appcodepath)
    {
        string HashKey = "";
        //encstr = CreateLicenceKey(out HashKey);
        string fileLoc = appcodepath + "\\sqlfile.sql";

        using (StreamWriter sw = new StreamWriter(fileLoc))
            sw.Write
                (@"" + txtprocedure.Text + "");
    }
   
   
  
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Button12.Visible = true;
        btnSubmit.Visible = false;
        LinkButton lnkbtn = (LinkButton)sender;
        GridViewRow item = (GridViewRow)lnkbtn.NamingContainer;
        int i = Convert.ToInt32(item.RowIndex);
        Label itemid = ((Label)GridView3.Rows[i].FindControl("Lael32"));
        LinkButton lbl = ((LinkButton)GridView3.Rows[i].FindControl("LinkButton1"));
      //  string te = "Viewstoredprocedureprofile.aspx?id='"+itemid.Text+"'";
        Response.Redirect("Viewstoredprocedureprofile.aspx?id=" +itemid.Text+ "");
        //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        btnSubmit.Visible = true;
        Button12.Visible = false;
        GridView3.PageIndex = e.NewPageIndex;
        fillgrid();
    }
   


    protected void Button1_Click1(object sender, EventArgs e)
    {
        GridView1.DataSource = null;
        GridView1.DataBind();
        ViewState["application"] = null; 
        DataTable dt = new DataTable();
        if (Convert.ToString(ViewState["application"]) == "")
        {
            dt.Columns.Add("id");
            dt.Columns.Add("Name");
        }
        else
        {
            dt = (DataTable)ViewState["application"];
        }
        foreach (GridViewRow gr5 in GridView5.Rows)
        {          
            Label itemid = ((Label)gr5.FindControl("lblid"));
            LinkButton lbl = ((LinkButton)gr5.FindControl("lnk20"));
            string lcl = lbl.Text;
            CheckBox chk = ((CheckBox)gr5.FindControl("CheckBox1"));         
            if (chk.Checked == true)
            {
                DataRow dr = dt.NewRow();
                dr["id"] = itemid.Text;
                dr["Name"] = lcl.ToString();
                dt.Rows.Add(dr);
                ViewState["application"] = dt;           
            }              
        }
        GridView1.DataSource = dt;
        GridView1.DataBind();
        //GridView5.Visible = false;
        //Panel5.Visible = false;

    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        if (txtsearch.Text != "")
        {          
            Button1.Visible = true;
            Filllistbox();
        }
        else 
        {        
        }
    }
    protected void Button7_Click(object sender, EventArgs e)
    {
       
        Button1.Visible = true;
        Filllistbox(); 

    }
   
    //
    protected void lnk21_Click(object sender, EventArgs e)
    {
       
    }
    protected void Button2_Click1(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        if (Convert.ToString(ViewState["application1"]) == "")
        {
            dt.Columns.Add("id");
            dt.Columns.Add("PageName");
        }
        else
        {
            dt = (DataTable)ViewState["application1"];
        }
        foreach (GridViewRow gr5 in GridView6.Rows)
        {
            Label itemid = ((Label)gr5.FindControl("lbl_id"));
            LinkButton lbl = ((LinkButton)gr5.FindControl("lnk21"));
            string lcl = lbl.Text;
            CheckBox chk = ((CheckBox)gr5.FindControl("CheckBox2"));
            if (chk.Checked == true)
            {
                DataRow dr = dt.NewRow();
                dr["id"] = itemid.Text;
                dr["PageName"] = lcl.ToString();
                dt.Rows.Add(dr);
            }
        }       
        GridView2.DataSource = dt;
        GridView2.DataBind();
        ViewState["application1"] = dt;
        //GridView6.Visible = false;
        //Panel4.Visible = false;
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
 
  
    protected void Button10_Click(object sender, EventArgs e)
    {
        if (txtpagesearch.Text != "" || ddlwebsite.SelectedIndex >0)
        {
            filldatalist();
            Button2.Visible = true;
            Panel4.Visible = true;
        }      
    }
    protected void Button11_Click(object sender, EventArgs e)
    {
        filldatalist();
        Button2.Visible = true;
        Panel4.Visible = true;
    }
    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView5_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView6_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
   
 
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
   

    //Search
    protected void BtnGo_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void FillProductSearch()
    {
        DataTable dtcln = MyCommonfile.selectBZ("SELECT distinct ProductMaster.ProductId, VersionInfoMaster.VersionInfoId,ProductMaster.ProductName + ' : ' + VersionInfoMaster.VersionInfoName as productversion,ProductMaster.ProductName FROM  dbo.ProductMaster INNER JOIN dbo.VersionInfoMaster ON dbo.ProductMaster.ProductId = dbo.VersionInfoMaster.ProductId INNER JOIN dbo.ProductDetail ON dbo.ProductMaster.ProductId = dbo.ProductDetail.ProductId AND dbo.VersionInfoMaster.VersionInfoName = dbo.ProductDetail.VersionNo where ClientMasterId=" + Session["ClientId"].ToString() + " and ProductDetail.Active ='True' and ProductDetail.Active='True' and VersionInfoMaster.Active='True'  order  by productversion");     
        DropDownList1.DataSource = dtcln;
        DropDownList1.DataValueField = "VersionInfoId";
        DropDownList1.DataTextField = "ProductName";
        DropDownList1.DataBind();
        FillCodettypeSearch();
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCodettypeSearch();
        fillgrid();
    }
    protected void FillCodettypeSearch()
    {
        DataTable dtcln = MyCommonfile.selectBZ(" SELECT DISTINCT TOP (100) PERCENT dbo.ProductCodeDetailTbl.Id, dbo.ProductCodeDetailTbl.ProductId, dbo.ProductCodeDetailTbl.CodeTypeName,dbo.CodeTypeTbl.CodeTypeCategoryId FROM dbo.CodeTypeTbl INNER JOIN dbo.CodeTypeCategory ON dbo.CodeTypeCategory.CodeMasterNo = dbo.CodeTypeTbl.CodeTypeCategoryId INNER JOIN dbo.ProductCodeDetailTbl ON dbo.CodeTypeTbl.ProductCodeDetailId = dbo.ProductCodeDetailTbl.Id  where  ProductCodeDetailTbl.Active='1' and CodeTypeTbl.ProductVersionId='" + DropDownList1.SelectedValue + "' and CodeTypeCategory.Id='2' and ProductCodeDetailTbl.Active=1 order  by dbo.ProductCodeDetailTbl.CodeTypeName ");
        ddlcodetypesearch.DataSource = dtcln;
        ddlcodetypesearch.DataValueField = "Id";
        ddlcodetypesearch.DataTextField = "CodeTypeName";
        ddlcodetypesearch.DataBind();
        if (dtcln.Rows.Count > 0)
        {
            Boolean DbConnection = CheckDbConn();          
        }
        else
        {
            img_dbconn.Visible = false;
        }
        //ddlcodetypesearch.Items.Insert(0, "--Select--");
        //ddlcodetypesearch.Items[0].Value = "0";
       // ddlcodetypesearch.SelectedIndex = 0;
    }
    protected void ddlcodetypesearch_SelectedIndexChanged(object sender, EventArgs e)
    {      
        fillgrid();
    }
    protected void txt_searchname_TextChanged(object sender, EventArgs e)
    {
    }
    protected void fillgrid()
    {
        string str1 = "";
        int id = Convert.ToInt32(ViewState["itemid"]);

        if (txt_searchname.Text != null)
        {
            str1 += " and ( dbo.Proceduremaster.Name like '%" + txtname.Text + "%' or dbo.Proceduremaster.Proce_code like '%" + txtname.Text + "%' or dbo.Proceduremaster.Description like '%" + txtname.Text + "%')";
        }
        str1 += " and Proceduremaster.ProductVersionID='" + DropDownList1.SelectedValue + "'";
        //if (ddlcodetypesearch.SelectedIndex > 0)
        //{
            str1 += " and Proceduremaster.databaseid='" + ddlcodetypesearch.SelectedValue + "'";
       // }
        //string str = " select distinct Proceduremaster.Type,Proceduremaster.Name,Proceduremaster.Date,EmployeeMaster.Name as employeename,Proceduremaster.proce_id as id from Proceduremaster left join EmployeeMaster on Proceduremaster.User_id=EmployeeMaster.Id left join storeproc_usingpage on storeproc_usingpage.Proce_id=Proceduremaster.proce_id left join storeproc_usingtable on storeproc_usingtable.Proce_id=Proceduremaster.proce_id WHERE Proceduremaster.proce_id !=0 " + str1 + "";
        DataTable dt = MyCommonfile.selectBZ(" SELECT DISTINCT dbo.Proceduremaster.Type, dbo.Proceduremaster.Name, dbo.Proceduremaster.Date, dbo.EmployeeMaster.Name AS employeename, dbo.Proceduremaster.proce_id AS id, dbo.ProductCodeDetailTbl.CodeTypeName FROM  dbo.Proceduremaster INNER JOIN dbo.ProductCodeDetailTbl ON dbo.Proceduremaster.databaseid = dbo.ProductCodeDetailTbl.Id LEFT OUTER JOIN dbo.EmployeeMaster ON dbo.Proceduremaster.User_id = dbo.EmployeeMaster.Id WHERE Proceduremaster.proce_id !=0 " + str1 + ""); 
        if (dt.Rows.Count > 0)
        {
            GridView3.DataSource = dt;
            GridView3.DataBind();
        }
        else
        {
            GridView3.DataSource = null;
            GridView3.DataBind();
            ViewState.Clear();
        }
    }

    protected void GridView3_SelectedIndexChanged(object sender, EventArgs e)
    {        
    }
    protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        btnSubmit.Visible = true;
        Button12.Visible = false;
        if (e.CommandName == "Delete")
        {
            ImageButton lnkBtn = (ImageButton)e.CommandSource;    // the button
            GridViewRow myRow = (GridViewRow)lnkBtn.Parent.Parent;  // the row
            GridView myGrid = (GridView)sender; // the gridview
            string i = myGrid.DataKeys[myRow.RowIndex].Value.ToString();
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("Proceduremaster_AddDelUpdtSelect", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StatementType", "Delete");
            cmd.Parameters.AddWithValue("@proce_id", i);           
            object maxprocID = new object();
            maxprocID = cmd.ExecuteScalar();
            fillgrid();
            Response.Redirect("Storedprocedureaddmanage.aspx");  
        }
        if (e.CommandName == "Edit1")
        {
            btnSubmit.Visible = false;

            Button12.Visible = true;
            pnl_addPROCEDURE.Visible = true;
            ImageButton lnkBtn = (ImageButton)e.CommandSource;    // the button
            GridViewRow myRow = (GridViewRow)lnkBtn.Parent.Parent;  // the row
            GridView myGrid = (GridView)sender; // the gridview
            string i = myGrid.DataKeys[myRow.RowIndex].Value.ToString();

            Session["prod_id"] = i;
            //string str = " select distinct Proceduremaster.proce_id as id ,Proceduremaster.databaseid ,Proceduremaster.Type,Proceduremaster.Name,Proceduremaster.Description,Proceduremaster.Proce_code,Proceduremaster.ProductVersionID  from dbo.Proceduremaster LEFT OUTER JOIN dbo.storeproc_usingtable ON dbo.storeproc_usingtable.Proce_id = dbo.Proceduremaster.proce_id INNER JOIN dbo.ClientProductTableMaster ON dbo.ClientProductTableMaster.Id = dbo.storeproc_usingtable.table_id INNER JOIN dbo.VersionInfoMaster ON dbo.ClientProductTableMaster.VersionInfoId = dbo.VersionInfoMaster.VersionInfoId  WHERE Proceduremaster.proce_id='" + i + "'";
            //SqlCommand cmd = new SqlCommand(str, con);
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            DataTable dt = MyCommonfile.selectBZ("SELECT DISTINCT dbo.Proceduremaster.Type,  dbo.Proceduremaster.Proce_code,dbo.Proceduremaster.ProductVersionID, dbo.Proceduremaster.Description ,dbo.Proceduremaster.databaseid,dbo.Proceduremaster.Name, dbo.Proceduremaster.Date, dbo.EmployeeMaster.Name AS employeename, dbo.Proceduremaster.proce_id AS id, dbo.ProductCodeDetailTbl.CodeTypeName FROM  dbo.Proceduremaster INNER JOIN dbo.ProductCodeDetailTbl ON dbo.Proceduremaster.databaseid = dbo.ProductCodeDetailTbl.Id LEFT OUTER JOIN dbo.EmployeeMaster ON dbo.Proceduremaster.User_id = dbo.EmployeeMaster.Id WHERE Proceduremaster.proce_id =" + i + " "); 
            if (dt.Rows.Count > 0)
            {
                FillProduct();
                ddlProductname.SelectedValue = dt.Rows[0]["ProductVersionID"].ToString();
                fillwebsite();
                fillproductid();
                fillCodetype();
                filldatalist();
                Filllistbox(); 
                ddlcodetype.SelectedValue = dt.Rows[0]["databaseid"].ToString();                
                txtname.Text = dt.Rows[0]["Name"].ToString();
                if (dt.Rows[0]["Type"].ToString() == "Insert-Select-Delete-Update" || dt.Rows[0]["Type"].ToString() == "Update" || dt.Rows[0]["Type"].ToString() == "Insert" || dt.Rows[0]["Type"].ToString() == "Select" || dt.Rows[0]["Type"].ToString() == "Delete")
                {
                    ddltype.SelectedItem.Text = dt.Rows[0]["Type"].ToString();
                }
                else
                {
                    ddltype.SelectedItem.Text = "Insert-Select-Delete-Update";
                }
                txtdescription.Text = dt.Rows[0]["Description"].ToString();
                txtprocedure.Text = dt.Rows[0]["Proce_code"].ToString();
                txtprocedure.Text = txtprocedure.Text.Replace("CREATE PROCEDURE", "ALTER PROCEDURE");
            }
            string str1 = "select storeproc_usingtable.Proce_id,storeproc_usingtable.table_id as id,ClientProductTableMaster.TableName as Name from  storeproc_usingtable inner join ClientProductTableMaster on ClientProductTableMaster.Id= storeproc_usingtable.table_id where storeproc_usingtable.Proce_id='" + i + "'";
            SqlCommand cmd1 = new SqlCommand(str1, con);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                ViewState["application"] = dt1;
                GridView1.DataSource = dt1;
                GridView1.DataBind();
            }
            string str2 = "select storeproc_usingpage.Proce_id,storeproc_usingpage.page_id as id,PageMaster.PageName  from  storeproc_usingpage inner join PageMaster on PageMaster.PageId= storeproc_usingpage.page_id where storeproc_usingpage.Proce_id='" + i + "'";
            SqlCommand cmd2 = new SqlCommand(str2, con);
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            if (dt2.Rows.Count > 0)
            {
                ViewState["application1"] = dt2;
                GridView2.DataSource = dt2;
                GridView2.DataBind();
            }
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["application"];
            dt.Rows[Convert.ToInt32(GridView1.SelectedIndex.ToString())].Delete();
            dt.AcceptChanges();            
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }



    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            GridView2.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["application1"];
            dt.Rows[Convert.ToInt32(GridView2.SelectedIndex.ToString())].Delete();
            dt.AcceptChanges();
            GridView2.DataSource = dt;
            GridView2.DataBind();
        }
    }
   //Cloase Search 





    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection();
        DataTable dtcln = MyCommonfile.selectBZ(" SELECT dbo.CodeTypeTbl.Instancename, dbo.CodeTypeTbl.ID, dbo.CodeTypeTbl.ProductCodeDetailId, dbo.ClientMaster.ServerId FROM dbo.CodeTypeTbl INNER JOIN dbo.ClientMaster INNER JOIN dbo.ProductMaster ON dbo.ClientMaster.ClientMasterId = dbo.ProductMaster.ClientMasterId INNER JOIN dbo.VersionInfoMaster ON dbo.ProductMaster.ProductId = dbo.VersionInfoMaster.ProductId ON dbo.CodeTypeTbl.ProductVersionId = dbo.VersionInfoMaster.VersionInfoId where dbo.CodeTypeTbl.ProductCodeDetailId='" + ddlcodetype.SelectedValue + "' ");
        if (dtcln.Rows.Count > 0)
        {
            Boolean checkstore = true;
            Boolean servConn = true;
                //Server server = new Server(new ServerConnection(conn));
                    //server.ConnectionContext.ExecuteNonQuery(script);
                    
                    //string connectionString = conn.ConnectionString;
                    //using (var connection = new SqlConnection(connectionString))
                    //{
                    //    var server = new Server(new ServerConnection(connection));
                    //    server.ConnectionContext.ExecuteNonQuery(script);
                    //}
                    if (rb_option.SelectedValue == "0")
                    {
                        string serversqlinstancename = dtcln.Rows[0]["Instancename"].ToString();
                        string filepath = Server.MapPath("");
                        ClassFileCreating(filepath);
                        txtprocedure.Text = txtprocedure.Text.Replace("ALTER PROCEDURE", "CREATE PROCEDURE");
                        string script = txtprocedure.Text;
                        script = File.ReadAllText(filepath + "\\sqlfile.sql");
                        conn = ServerWizard.ServerDatabaseFromInstanceTCP(lbl_serverid.Text, serversqlinstancename, ddlcodetype.SelectedItem.Text);
                        try
                        {
                            if (conn.State.ToString() != "Open")
                            {
                                conn.Open();
                            }
                        }
                        catch
                        {
                            servConn = false;
                        }
                        if (servConn == true)
                        {
                            try
                            {
                                SqlCommand command = new SqlCommand(script, conn);
                                command.ExecuteNonQuery();
                                conn.Close();
                            }
                            catch (Exception ex)
                            {
                                checkstore = false;
                                lblmsg.Text = ex.ToString();
                            }
                        }
                    }
                    else if (rb_option.SelectedValue == "1")
                    {
                        // txtprocedure.Enabled = false;                      
                        checkstore = true;
                        checkstore = true;
                    }
           
            if (checkstore == true && checkstore == true)
            {
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                string dt = System.DateTime.Now.ToString();
                SqlCommand cmd = new SqlCommand("Proceduremaster_AddDelUpdtSelect", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StatementType", "Insert");
                cmd.Parameters.AddWithValue("@ProductVersionID", ddlProductname.SelectedValue);
                cmd.Parameters.AddWithValue("@databaseid", ddlcodetype.SelectedValue);
                cmd.Parameters.AddWithValue("@Name", txtname.Text);
                cmd.Parameters.AddWithValue("@Type", ddltype.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Description", txtdescription.Text);
                cmd.Parameters.AddWithValue("@Proce_code", txtprocedure.Text);
                cmd.Parameters.AddWithValue("@Date", dt.ToString());
                cmd.Parameters.AddWithValue("@User_id", Convert.ToInt32(Session[" userid"]));
                object maxprocID = new object();
                maxprocID = cmd.ExecuteScalar();
                foreach (GridViewRow gr5 in GridView1.Rows)
                {
                    Label Label511 = (Label)gr5.FindControl("Label11");
                    Label lblid = (Label)gr5.FindControl("lbid");
                    SqlCommand cmd1 = new SqlCommand("insert_usingtable", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@Proce_id", maxprocID);
                    cmd1.Parameters.AddWithValue("@table_id", lblid.Text);
                    cmd1.ExecuteNonQuery();
                }
                foreach (GridViewRow gr5 in GridView2.Rows)
                {
                    Label Label521 = (Label)gr5.FindControl("Lael11");
                    Label Label522 = (Label)gr5.FindControl("lb_id");
                    SqlCommand cmd2 = new SqlCommand("insert_usingpage", con);
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.AddWithValue("@Proce_id", maxprocID);
                    cmd2.Parameters.AddWithValue("@page_id", Label522.Text);
                    cmd2.ExecuteNonQuery();
                }
                lblmsg.Text = "Record inserted successfully";
               
                fillgrid();
                Clr();
                pnl_addPROCEDURE.Visible = false;
                btn_addnewreco.Visible = true;
            }
            else 
            {
                if (checkstore == false)
                {
                    lblmsg.Text += "Some problem in insert store procedure please check procedure is currect or not or procedure related table and field available in SQL server database or not" ;
                }
                if (servConn == false)
                {
                    lblmsg.Text += "" + conn.ConnectionString;
                }
            }
        }
    }
    protected void Button12_Click(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection();       
        Boolean checkstore = true;
        Boolean servConn = true;        
        try
        {
            DataTable dtcln = MyCommonfile.selectBZ(" SELECT dbo.CodeTypeTbl.Instancename, dbo.CodeTypeTbl.ID, dbo.CodeTypeTbl.ProductCodeDetailId, dbo.ClientMaster.ServerId FROM dbo.CodeTypeTbl INNER JOIN dbo.ClientMaster INNER JOIN dbo.ProductMaster ON dbo.ClientMaster.ClientMasterId = dbo.ProductMaster.ClientMasterId INNER JOIN dbo.VersionInfoMaster ON dbo.ProductMaster.ProductId = dbo.VersionInfoMaster.ProductId ON dbo.CodeTypeTbl.ProductVersionId = dbo.VersionInfoMaster.VersionInfoId where dbo.CodeTypeTbl.ProductCodeDetailId='" + ddlcodetype.SelectedValue + "' ");
            string serversqlinstancename = dtcln.Rows[0]["Instancename"].ToString();
            string serverid = dtcln.Rows[0]["Instancename"].ToString();
          
            string filepath = Server.MapPath("");
            ClassFileCreating(filepath);
            txtprocedure.Text = txtprocedure.Text.Replace("CREATE PROCEDURE", "ALTER PROCEDURE");
            string script = txtprocedure.Text;
            script = File.ReadAllText(filepath + "\\sqlfile.sql");
            conn = ServerWizard.ServerDatabaseFromInstanceTCP(lbl_serverid.Text, serversqlinstancename, ddlcodetype.SelectedItem.Text);
            string connectionString = conn.ConnectionString;
            try
            {
                if (conn.State.ToString() != "Open")
                {
                    conn.Open();
                }
            }
            catch
            {
                servConn = false;
            }
            SqlCommand command = new SqlCommand(script, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }
        catch
        {
            checkstore = false;
        }
        if (checkstore == true)
        {
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            string dt = System.DateTime.Now.ToString();
            SqlCommand cmd = new SqlCommand("Proceduremaster_AddDelUpdtSelect", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StatementType", "Update");
            cmd.Parameters.AddWithValue("@ProductVersionID", ddlProductname.SelectedValue);
            cmd.Parameters.AddWithValue("@databaseid", ddlcodetype.SelectedValue);
            cmd.Parameters.AddWithValue("@Name", txtname.Text);
            cmd.Parameters.AddWithValue("@Type", ddltype.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@Description", txtdescription.Text);
            cmd.Parameters.AddWithValue("@Proce_code", txtprocedure.Text);
            cmd.Parameters.AddWithValue("@Date", dt.ToString());
            cmd.Parameters.AddWithValue("@User_id", Convert.ToInt32(Session[" userid"]));
            cmd.Parameters.AddWithValue("@Proce_id", Session["prod_id"]);
            cmd.ExecuteNonQuery();


            string strdelete = "delete  from storeproc_usingtable where storeproc_usingtable.Proce_id='" + Session["prod_id"] + "' ";
            SqlCommand cmddelete = new SqlCommand(strdelete, con);
            cmddelete.ExecuteNonQuery();
            foreach (GridViewRow gr5 in GridView1.Rows)
            {
                SqlCommand cmd1 = new SqlCommand("insert_usingtable", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Proce_id", Session["prod_id"]);
                Label Label511 = (Label)gr5.FindControl("Label11");
                Label Label521 = (Label)gr5.FindControl("lbid");
                cmd1.Parameters.AddWithValue("@table_id", Label521.Text);
                cmd1.ExecuteNonQuery();
            }
            string strdelete1 = "delete  from storeproc_usingpage where storeproc_usingpage.Proce_id='" + Session["prod_id"] + "' ";
            SqlCommand cmddelete1 = new SqlCommand(strdelete1, con);
            cmddelete1.ExecuteNonQuery();
            foreach (GridViewRow gr5 in GridView2.Rows)
            {
                Label Label521 = (Label)gr5.FindControl("Lael11");
                Label Label522 = (Label)gr5.FindControl("lb_id");
                SqlCommand cmd2 = new SqlCommand("insert_usingpage", con);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.AddWithValue("@Proce_id", Session["prod_id"]);
                cmd2.Parameters.AddWithValue("@page_id", Label522.Text);
                cmd2.ExecuteNonQuery();
            }
            lblmsg.Text = "Record updated successfully";
            con.Close();
            fillgrid();
            Clr();
            pnl_addPROCEDURE.Visible = false;
            btn_addnewreco.Visible = true;
        }
        else
        {
            lblmsg.Text = "Some problem in updating store procedure please check procedure is currect or not or procedure related table and field available in SQL server database or not " + conn;
        }
    }
  
    protected void Button13_Click(object sender, EventArgs e)
    {
        Response.Redirect("Storedprocedureaddmanage.aspx");
        Clr(); 
        pnl_addPROCEDURE.Visible = false;
    }
   






    //---------------------------------
    protected void btnsynTables(object sender, EventArgs e)
    {
       // ModalPopupExtender2.Show();
        ModalPopupExtender1.Show(); 
    }
    public Boolean CheckDbConn()
    {
        lnk_syncdb.Visible = false;
        gv_selectprocedure.DataSource = null;
        gv_selectprocedure.DataBind();
        Boolean status = false;
        DataTable dtdatabaseins = MyCommonfile.selectBZ(@"SELECT dbo.CodeTypeTbl.Instancename, dbo.CodeTypeTbl.ID, dbo.CodeTypeTbl.ProductCodeDetailId, dbo.ClientMaster.ServerId FROM dbo.CodeTypeTbl INNER JOIN dbo.ClientMaster INNER JOIN dbo.ProductMaster ON dbo.ClientMaster.ClientMasterId = dbo.ProductMaster.ClientMasterId INNER JOIN dbo.VersionInfoMaster ON dbo.ProductMaster.ProductId = dbo.VersionInfoMaster.ProductId ON dbo.CodeTypeTbl.ProductVersionId = dbo.VersionInfoMaster.VersionInfoId where dbo.CodeTypeTbl.ProductCodeDetailId='" + ddlcodetypesearch.SelectedValue + "'");
        if (dtdatabaseins.Rows.Count > 0)
        {
            string serverid = dtdatabaseins.Rows[0]["ServerId"].ToString();
            string serversqlinstancename = dtdatabaseins.Rows[0]["Instancename"].ToString();
             conn1 = new SqlConnection();
             conn1 = ServerWizard.ServerDatabaseFromInstanceTCP(serverid, serversqlinstancename, ddlcodetypesearch.SelectedItem.Text);
            try
            {
                if (conn1.State.ToString() != "Open")
                {
                    conn1.Open();
                    status = true;
                    img_dbconn.ImageUrl = "~/images/DatabaseConnection/DatabaseConnTrue.png";
                    img_dbconn.Visible = true;

                    DataTable dt = MyCommonfile.selectBZ("SELECT DISTINCT dbo.Proceduremaster.Name as name FROM  dbo.Proceduremaster INNER JOIN dbo.ProductCodeDetailTbl ON dbo.Proceduremaster.databaseid = dbo.ProductCodeDetailTbl.Id LEFT OUTER JOIN dbo.EmployeeMaster ON dbo.Proceduremaster.User_id = dbo.EmployeeMaster.Id WHERE Proceduremaster.proce_id !=0 and Proceduremaster.databaseid='" + ddlcodetype.SelectedValue + "'");
                    string currenttable = "";
                    string str1 = "";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        currenttable += "'" + dt.Rows[i]["name"].ToString() + "'" + ",";
                    }
                    if (currenttable.Length > 0)
                    {
                        currenttable = currenttable.Remove(currenttable.Length - 1);
                        str1 = " and name NOT IN(" + currenttable + ")";
                    }
                    string strnew = " Select Count(Object_id) as Count from sys.procedures where object_id != '' " + str1 + "";
                    SqlCommand cmdnew = new SqlCommand(strnew, conn1);
                    SqlDataAdapter danew = new SqlDataAdapter(cmdnew);
                    DataTable dtnew = new DataTable();
                    danew.Fill(dtnew);
                    string noofRow = dtnew.Rows[0]["Count"].ToString();
                    Int64 IntnoofRow = Convert.ToInt64(noofRow);
                    if (IntnoofRow > 0)
                    {
                        Lbl_text2.Text = "There are " + dtnew.Rows[0]["Count"].ToString() + " store prcedures which are not imported from Sql server " + ddlcodetypesearch.SelectedItem.Text + " database. Would you like to import them in this system from sql server ?";
                        lnk_syncdb.Visible = true;

                        strnew = " SELECT  TOP (10) Object_definition(object_id) AS procedurecode, name FROM sys.procedures where object_id != '' " + str1 + "";
                        cmdnew = new SqlCommand(strnew, conn1);
                        danew = new SqlDataAdapter(cmdnew);
                        dtnew = new DataTable();
                        danew.Fill(dtnew);                      
                        if (dtnew.Rows.Count > 0)
                        {
                            GridView4.DataSource = dtnew;
                            GridView4.DataBind();
                            pnl_showgidproc.Visible = true;
                        }
                    }
                    conn1.Close();
                }
            }
            catch
            {
                img_dbconn.ImageUrl = "~/images/DatabaseConnection/DatabaseConnFalse.png";
                img_dbconn.Visible = true;
            }
        }
        return status;
    }
    protected void GridView4_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void lnk_viewselectstoreproce_Click(object sender, EventArgs e)
    {
        LinkButton lnkbtn = (LinkButton)sender;
        GridViewRow item = (GridViewRow)lnkbtn.NamingContainer;
        int i = Convert.ToInt32(item.RowIndex);
        Label lbl_code = ((Label)GridView4.Rows[i].FindControl("lbl_code"));
        //GetProcedureName(lbl_code.Text);
        lbl_syntax.Text = "View Procedure code";
        txtpricedure.Text = lbl_code.Text;
        Image11235.Visible = false;
        ModalPopupExtender1.Show(); 
        //
    }
    protected void Checkprocedure()
    {
        lbl_errortxtname.Text = "";
        txtprocedure.Text = "";
        pnl_showgidproc.Visible = false;
        Int64 StorproTypeExisting = Convert.ToInt64(rb_option.SelectedValue);
        SqlConnection conn = new SqlConnection();
        Boolean checkstore = true;
        Boolean servConn = true;
        DataTable dtcln = MyCommonfile.selectBZ(" SELECT dbo.CodeTypeTbl.Instancename, dbo.CodeTypeTbl.ID, dbo.CodeTypeTbl.ProductCodeDetailId, dbo.ClientMaster.ServerId FROM dbo.CodeTypeTbl INNER JOIN dbo.ClientMaster INNER JOIN dbo.ProductMaster ON dbo.ClientMaster.ClientMasterId = dbo.ProductMaster.ClientMasterId INNER JOIN dbo.VersionInfoMaster ON dbo.ProductMaster.ProductId = dbo.VersionInfoMaster.ProductId ON dbo.CodeTypeTbl.ProductVersionId = dbo.VersionInfoMaster.VersionInfoId where dbo.CodeTypeTbl.ProductCodeDetailId='" + ddlcodetype.SelectedValue + "' ");
        if (dtcln.Rows.Count > 0)
        {
            string serversqlinstancename = dtcln.Rows[0]["Instancename"].ToString();
            conn = ServerWizard.ServerDatabaseFromInstanceTCP(lbl_serverid.Text, serversqlinstancename, ddlcodetype.SelectedItem.Text);
            try
            {
                if (conn.State.ToString() != "Open")
                {
                    conn.Open();
                }
            }
            catch
            {
                servConn = false;
            }
            if (servConn == true)
            {
                int inv = 0;
                string strnew = " Select name from sys.procedures  Where (name = '" + txtname.Text + "') ";
                SqlCommand cmdnew = new SqlCommand(strnew, conn);
                SqlDataAdapter danew = new SqlDataAdapter(cmdnew);
                DataTable dtnew = new DataTable();
                danew.Fill(dtnew);
                if (dtnew.Rows.Count == 0 && StorproTypeExisting == 1)
                {
                    DataTable dt = MyCommonfile.selectBZ("SELECT DISTINCT dbo.Proceduremaster.Name as name FROM  dbo.Proceduremaster INNER JOIN dbo.ProductCodeDetailTbl ON dbo.Proceduremaster.databaseid = dbo.ProductCodeDetailTbl.Id LEFT OUTER JOIN dbo.EmployeeMaster ON dbo.Proceduremaster.User_id = dbo.EmployeeMaster.Id WHERE Proceduremaster.proce_id !=0 and Proceduremaster.databaseid='" + ddlcodetype.SelectedValue + "'");
                    string currenttable = "";
                    string str1 = "";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        currenttable += "'" + dt.Rows[i]["name"].ToString() + "'" + ",";
                    }
                    if (currenttable.Length > 0)
                    {
                        currenttable = currenttable.Remove(currenttable.Length - 1);
                        str1 = " and name NOT IN(" + currenttable + ")";
                    }
                    strnew = " Select name from sys.procedures  Where (name like '%" + txtname.Text + "%') " + str1 + " ";
                    cmdnew = new SqlCommand(strnew, conn);
                    danew = new SqlDataAdapter(cmdnew);
                    dtnew = new DataTable();
                    danew.Fill(dtnew);
                    if (dtnew.Rows.Count > 0)
                    {
                        gv_selectprocedure.DataSource = dtnew;
                        gv_selectprocedure.DataBind();
                        pnl_showgidproc.Visible = true;
                    }
                    lbl_errortxtname.Text = "";
                }
                else if (StorproTypeExisting == 1)
                {
                    GridView5.DataSource = null;
                    GridView5.DataBind();
                    lbl_errortxtname.Text = "Name matching successfully";
                    lbl_errortxtname.ForeColor = Color.Green;
                    GetProcedureName(txtname.Text);
                }
                else if (StorproTypeExisting == 0)
                {
                    string str1 = "";
                    str1 += " and Proceduremaster.databaseid='" + ddlcodetype.SelectedValue + "'";
                    str1 += " and dbo.Proceduremaster.Name='" + txtname.Text + "' ";
                    DataTable dt = MyCommonfile.selectBZ("SELECT DISTINCT dbo.Proceduremaster.Type, dbo.Proceduremaster.Name, dbo.Proceduremaster.Date, dbo.EmployeeMaster.Name AS employeename, dbo.Proceduremaster.proce_id AS id, dbo.ProductCodeDetailTbl.CodeTypeName FROM  dbo.Proceduremaster INNER JOIN dbo.ProductCodeDetailTbl ON dbo.Proceduremaster.databaseid = dbo.ProductCodeDetailTbl.Id LEFT OUTER JOIN dbo.EmployeeMaster ON dbo.Proceduremaster.User_id = dbo.EmployeeMaster.Id WHERE Proceduremaster.proce_id !=0 " + str1 + "");

                    if (dtnew.Rows.Count > 0 && dt.Rows.Count > 0)
                    {
                        lbl_errortxtname.Text = "Allredy exist this procedure in database (SQL Server " + ddlcodetype.SelectedItem.Text + " database) and also Proceduremaster table";
                        lbl_errortxtname.ForeColor = Color.Red;
                    }
                    else if (dt.Rows.Count > 0)
                    {
                        lbl_errortxtname.Text = "Allredy exist this procedure in database (SQL Server " + ddlcodetype.SelectedItem.Text + " database)";
                        lbl_errortxtname.ForeColor = Color.Red;
                    }
                }
                else
                {
                }
            }
        }     
   
    }

   
}

