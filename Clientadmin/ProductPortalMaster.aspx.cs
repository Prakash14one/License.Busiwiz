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
using System.IO;
//using iTextSharp.text.pdf;
//using iTextSharp.text.html.simpleparser;
//using iTextSharp.text;
using System.Text;
using System.Net;
using System.Net.Mail;



public partial class SetupWizardDetail1 : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    SqlConnection condefaultinstance = new SqlConnection();

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");

        lblVersion.Text = "";       
        string strData = Request.Url.ToString();
        string pass1 = TextEmailMasterLoginPassword.Text;
        TextEmailMasterLoginPassword.Attributes.Add("Value", pass1);

        string pass2 = TextBox3.Text;
        TextBox3.Attributes.Add("Value", pass2);

        char[] separator = new char[] { '/' };
        //compid = Session["Comid"].ToString();
        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();
        if (!IsPostBack)
        {
            if (Session["Login"] != null)
            {
                if (Session["Login"].ToString() == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }

            //--35Company Name
            chkBusiwizServer.Text = "Allowed to be hosted on Clients's (for eg: " + Session["Clientname"].ToString()  + ") server";
            chkLeaseServer.Text = "Host on your exclusive separate secured ( physical not virtual) server in server farm of " + Session["Clientname"].ToString() + ".  ";
            chkSharedServer.Text = "Host on your Limited shared secured ( physical not virtual) server in server farm of " + Session["Clientname"].ToString() + ".";
            chksale.Text = "Hosted on Clients Own server in " + Session["Clientname"].ToString() + " Server farm.";
            //--
            FillProduct();
            dllportal();           
            fillcompany();
            fillddlcountry();
            fillddlstate();
            portal();         
            fillgriddata();          
            //          
            //
            ddlserchstatus_SelectedIndexChanged(sender, e);          
        }
    }

    string str = "";
    string compid;
    string temp;
    protected void Clear()
    {
        HiddenField1.Value = "0";
        lblmsg.Text = "";
        BtnUpdate.Visible = false;
        btnSubmit.Visible = true;
        ddlstatus.SelectedIndex = 0;
        pnladdnew.Visible = false;
        addnewpanel.Visible = true;
        con.Close();
        fillgriddata();
        ddpname.SelectedIndex = 0;
        txtportname.Text = "";
        ddlstate.SelectedIndex = 0;
        ddlcountry.SelectedIndex = 0;
        imglogo.Visible = false;
        txtmemailname.Text = "";
        txtaspxpagename.Text = "";
        txtemail.Text = "";
        txtuserid.Text = "";
        TextIncomingMailServer.Text = "";
        TextBox3.Attributes.Clear();
        TextEmailMasterLoginPassword.Attributes.Clear();
        txtsupptememail.Text = "";
        txtspno.Text = "";
        txtmanagername.Text = "";
        txtSiteName.Text = "";
        txtaddress2.Text = "";
        txtAddress1.Text = "";
        ddlcountry.SelectedIndex = 0;
        ddlstate.SelectedIndex = 0;
        txtcity.Text = "";
        TextPhone1.Text = "";
        txtfax.Text = "";
        txtex.Text = "";
        txttollfree.Text = "";
        txtextension.Text = "";
        TextZip.Text = "";
        lbltestdata.BackColor = System.Drawing.Color.Empty;
        txtcolour.Text = "";
        BtnUpdate.Visible = false;
        btnSubmit.Visible = true;
        addnewpanel.Visible = true;
        pnladdnew.Visible = false;
        imglogo.AlternateText = "";
        imglogo.AlternateText = "";
        TextIncomingMailServerport.Text = "";
    }
    protected void FillVersion()
    {
        //string strcln = " SELECT distinct ProductMaster.ProductId, VersionInfoMaster.VersionInfoId,ProductMaster.ProductName + ' : ' + VersionInfoMaster.VersionInfoName as productversion FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.VersionNo=VersionInfoMaster.VersionInfoName    where ClientMasterId=" + Session["ClientId"].ToString() + " and ProductDetail.Active='1' and ProductDetail.Active ='True' order  by productversion";
        string activestr = "";
        string strwhere = "";
        if (CheckBox1.Checked == true)
        {
            activestr = " and VersionInfoMaster.Active=1 ";
            strwhere = " where dbo.ProductDetail.Active = '1' ";
        }
        if (ddlProductname.SelectedIndex > 0)
        {
            activestr += " and ProductId="+ ddlProductname.SelectedValue  +" ";
        }
        string strcln = " ";
        strcln = " Select * From VersionInfoMaster Where  ProductId IN(Select DISTINCT dbo.ProductDetail.ProductId From dbo.ProductDetail " + strwhere + ") " + activestr + " ";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        ddlversion.DataSource = dtcln;
        ddlversion.DataValueField = "Versioninfoid";
        ddlversion.DataTextField = "VersioninfoName";
        ddlversion.DataBind();
        ddlversion.Items.Insert(0, "-Select-");
        ddlversion.Items[0].Value = "0";
    }
    protected void ddpname_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillcompany();
        lbl_websiteoption.Text = "The product " + ddpname.SelectedItem.Text  + " has below of websites available, which website / would you like to create for subscribers";
        FillWebsiteGrid();
        FillDatabaseGrid();
    }
    protected void FillProduct()
    {
        //string strcln = " SELECT distinct ProductMaster.ProductId, VersionInfoMaster.VersionInfoId,ProductMaster.ProductName + ' : ' + VersionInfoMaster.VersionInfoName as productversion FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.VersionNo=VersionInfoMaster.VersionInfoName    where ClientMasterId=" + Session["ClientId"].ToString() + " and ProductDetail.Active='1' and ProductDetail.Active ='True' order  by productversion";
        string activestr = "";
        if (CheckBox1.Checked == true)
        {
           // activestr = " and VersionInfoMaster.Active=1";
            activestr = " and (ProductId IN (SELECT ProductId  FROM dbo.ProductDetail WHERE   (Active = 1))) and (ProductId IN (SELECT ProductId  FROM dbo.VersionInfoMaster WHERE (Active = 1))) ";
        }
        string strcln = " SELECT distinct ProductMaster.ProductId,ProductDetail.Active,VersionInfoMaster.VersionInfoId,ProductMaster.ProductName + ' : ' + VersionInfoMaster.VersionInfoName as productversion FROM ProductMaster inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId where ClientMasterId=" + Session["ClientId"].ToString() + " and ProductDetail.Active='1'  order  by productversion";
        strcln = " SELECT DISTINCT TOP (100) PERCENT ProductId, ProductName FROM dbo.ProductMaster WHERE  ClientMasterId= " + Session["ClientId"].ToString() + " "+ activestr +" ORDER BY ProductName ";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        ddlProductname.DataSource = dtcln;
        ddlProductname.DataValueField = "ProductId";
        ddlProductname.DataTextField = "ProductName";
        ddlProductname.DataBind();
        ddlProductname.Items.Insert(0, "-Select-");
        ddlProductname.Items[0].Value = "0";

        ddpname.DataSource = dtcln;
        ddpname.DataValueField = "ProductId";
        ddpname.DataTextField = "ProductName";
        ddpname.DataBind();
        ddpname.Items.Insert(0, "-Select-");
        ddpname.Items[0].Value = "0";       
    }
    protected void fillgriddata()
    {
        String st1 = "";
        string sgggg = "";
        if (ddlsrechportal.SelectedIndex > 0)
        {
            st1 += " and PortalMasterTbl.PortalName ='" + ddlsrechportal.SelectedValue + "'";
        }
        if (TextBox1.Text != "")
        {
            st1 += " and ( ProductMaster.ProductName like '%" + TextBox1.Text.Replace("'", "''") + "%'  OR PortalMasterTbl.PortalName like '%" + TextBox1.Text.Replace("'", "''") + "%'   OR  PortalMasterTbl.EmailDisplayname like '%" + TextBox1.Text.Replace("'", "''") + "%'  OR PortalMasterTbl.Supportteamemailid like '%" + TextBox1.Text.Replace("'", "''") + "%'  " +
                " OR PortalMasterTbl.Supportteamphoneno like '%" + TextBox1.Text.Replace("'", "''") + "%' OR PortalMasterTbl.Supportteammanagername like '%" + TextBox1.Text.Replace("'", "''") + "%' OR  PortalMasterTbl.Portalmarketingwebsitename like '%" + TextBox1.Text.Replace("'", "''") + "%' )  ";

            //ProductName   PortalName   EmailDisplayname   Supportteamemailid   Supportteamphoneno   Supportteammanagername  Portalmarketingwebsitename
        }
        if (ddlProductname.SelectedIndex > 0)
        {
            st1 += " and PortalMasterTbl.ProductId = '" + ddlProductname.SelectedValue + "'  ";
        }
        if (ddlversion.SelectedIndex > 0)
        {
           // st1 += " and PortalMasterTbl.ProductId In( Select distinct ProductMaster.ProductId from  ProductMaster  inner join VersionInfoMaster on VersionInfoMaster.productId=ProductMaster.ProductId where VersionInfoId = '" + ddlversion.SelectedValue + "' ) "; 
            st1 += " and VersionInfoMaster.VersionInfoId = '" + ddlversion.SelectedValue + "' ";
        }
        if (CheckBox1.Checked == true)
        {
         sgggg = "SELECT PortalMasterTbl.*, CountryMaster.CountryId AS Expr1, CountryMaster.CountryName, ProductMaster.ProductId AS Expr2, ProductMaster.ProductName, StateMasterTbl.StateId AS Expr3, StateMasterTbl.StateName FROM " + 
                    " StateMasterTbl INNER JOIN CountryMaster INNER JOIN ProductMaster INNER JOIN  PortalMasterTbl ON ProductMaster.ProductId = PortalMasterTbl.ProductId ON CountryMaster.CountryId = PortalMasterTbl.CountryId ON StateMasterTbl.StateId = PortalMasterTbl.StateId " +
                   // " dbo.StateMasterTbl INNER JOIN dbo.CountryMaster INNER JOIN dbo.ProductMaster INNER JOIN dbo.PortalMasterTbl ON dbo.ProductMaster.ProductId = dbo.PortalMasterTbl.ProductId ON dbo.CountryMaster.CountryId = dbo.PortalMasterTbl.CountryId ON dbo.StateMasterTbl.StateId = dbo.PortalMasterTbl.StateId INNER JOIN dbo.VersionInfoMaster ON dbo.ProductMaster.ProductId = dbo.VersionInfoMaster.ProductId " +
                    " where  PortalMasterTbl.Status=1 " + st1 + " ";
        }
        else
        {
             sgggg = "SELECT PortalMasterTbl.*, CountryMaster.CountryId AS Expr1, CountryMaster.CountryName, ProductMaster.ProductId AS Expr2, ProductMaster.ProductName, StateMasterTbl.StateId AS Expr3, StateMasterTbl.StateName FROM  " +
                    " StateMasterTbl INNER JOIN CountryMaster INNER JOIN ProductMaster INNER JOIN  PortalMasterTbl ON ProductMaster.ProductId = PortalMasterTbl.ProductId ON CountryMaster.CountryId = PortalMasterTbl.CountryId ON StateMasterTbl.StateId = PortalMasterTbl.StateId "+
                   // " dbo.StateMasterTbl INNER JOIN dbo.CountryMaster INNER JOIN dbo.ProductMaster INNER JOIN dbo.PortalMasterTbl ON dbo.ProductMaster.ProductId = dbo.PortalMasterTbl.ProductId ON dbo.CountryMaster.CountryId = dbo.PortalMasterTbl.CountryId ON dbo.StateMasterTbl.StateId = dbo.PortalMasterTbl.StateId INNER JOIN dbo.VersionInfoMaster ON dbo.ProductMaster.ProductId = dbo.VersionInfoMaster.ProductId "+
                    " where  PortalMasterTbl.Status in(1,0) " + st1 + " ";
        }
        SqlCommand cmdgrid = new SqlCommand(sgggg, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        //cmdgrid.CommandType = CommandType.StoredProcedure;
        //cmdgrid.CommandText = "selectportalfillgriddata";
        SqlDataAdapter dtpgrid = new SqlDataAdapter(cmdgrid);
        DataTable dtgrid = new DataTable();
        dtpgrid.Fill(dtgrid);
        if (dtgrid.Rows.Count > 0)
        {            
            GridView1.DataSource = dtgrid;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
    }
    private void dllportal()
    {
        string strcondi = "";
        if (ddlProductname.SelectedIndex >0)
        {

            ddlsrechportal.Items.Clear();
            string strcln22v = "Select * from PortalMasterTbl where PortalMasterTbl.ProductId In( Select distinct ProductMaster.ProductId from  ProductMaster  inner join VersionInfoMaster on VersionInfoMaster.productId=ProductMaster.ProductId where VersionInfoId = '" + ddlProductname.SelectedValue + "' ) and PortalMasterTbl.Status = '1'  order by PortalName"; //and PortalMasterTbl.Status = '1' add on 32615
            
            SqlCommand cmdcln22v = new SqlCommand(strcln22v, con);
            DataTable dtcln22v = new DataTable();
            SqlDataAdapter adpcln22v = new SqlDataAdapter(cmdcln22v);
            adpcln22v.Fill(dtcln22v);
            ddlsrechportal.DataSource = dtcln22v;
            ddlsrechportal.DataValueField = "Id";
            ddlsrechportal.DataTextField = "PortalName";
            ddlsrechportal.DataBind();
            ddlsrechportal.Items.Insert(0, "All");
            ddlsrechportal.Items[0].Value = "0";
        }
       

    }
    protected void ddlversion_SelectedIndexChanged(object sender, EventArgs e)
    {
          
        fillgriddata();
         
    }
    protected void ddlProductname_SelectedIndexChanged(object sender, EventArgs e)
    {
        //   dllportal();
        //  ddlsrechportal_SelectedIndexChanged(sender, e);
       // FillVersion();
        fillgriddata();

    }
    protected void fillddlcountry()
    {
        //SqlCommand cmd = new SqlCommand("Select distinct CountryId,CountryName from CountryMaster order by CountryName", con);
        SqlCommand cmd = new SqlCommand("Selectfillddlcountry", con);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "Selectfillddlcountry";
        DataTable dt = new DataTable();
        dtp.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ddlcountry.DataSource = dt;
            ddlcountry.DataTextField = "CountryName";
            ddlcountry.DataValueField = "CountryId";
            ddlcountry.DataBind();

        }
        ddlcountry.Items.Insert(0, "-Select-");
        ddlcountry.Items[0].Value = "0";
        ddlstate.Items.Insert(0, "-Select-");
        ddlstate.Items[0].Value = "0";

    }
    protected void fillddlstate()
    {
        ddlstate.Items.Clear();

        SqlCommand cmd = new SqlCommand("Select * from StateMasterTbl where CountryId='" + ddlcountry.SelectedValue + "' order by StateName", con);
        //SqlCommand cmd = new SqlCommand("Selectfillddlstate12", con);
        //cmd.CommandType = CommandType.StoredProcedure;
        //cmd.Parameters.AddWithValue("@CountryId",ddlcountry.SelectedValue);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ddlstate.DataSource = dt;
            ddlstate.DataTextField = "StateName";
            ddlstate.DataValueField = "StateId";
            ddlstate.DataBind();
            // ddlstate.Items.Insert(0, "--Select--");
        }
        ddlstate.Items.Insert(0, "-Select-");
        ddlstate.Items[0].Value = "0";

    }
    //-***********Website Grid web
    protected void FillWebsiteGrid()
    {
        string strsearch = "";
        strsearch = " ";
        string strcln = " SELECT distinct ID, WebsiteName,Case when(ID IS NULL) then  cast ('1' as bit) else  cast('0' as bit) end as chk From dbo.WebsiteMaster INNER JOIN dbo.VersionInfoMaster ON dbo.WebsiteMaster.VersionInfoId = dbo.VersionInfoMaster.VersionInfoId  Where dbo.VersionInfoMaster.Active=1 " + strsearch + "  ";
        strcln = "  SELECT  dbo.WebsiteMaster.WebsiteName + '-' + dbo.CodeTypeTbl.Name AS CodeTypeName, dbo.WebsiteMaster.WebsiteName, dbo.WebsiteMaster.WebsiteUrl, CASE WHEN (WebsiteID IS NULL) THEN CAST('1' AS bit) ELSE CAST('0' AS bit) END AS chk, dbo.CodeTypeTbl.Name, dbo.CodeTypeTbl.CodeTypeCategoryId, dbo.CodeTypeTbl.ProductCodeDetailId, dbo.CodeTypeTbl.WebsiteID, dbo.CodeTypeTbl.ID, dbo.ProductMaster.ProductId FROM dbo.WebsiteMaster INNER JOIN dbo.CodeTypeTbl ON dbo.WebsiteMaster.ID = dbo.CodeTypeTbl.WebsiteID INNER JOIN dbo.ProductCodeDetailTbl ON dbo.CodeTypeTbl.ProductCodeDetailId = dbo.ProductCodeDetailTbl.Id INNER JOIN dbo.VersionInfoMaster ON dbo.WebsiteMaster.VersionInfoId = dbo.VersionInfoMaster.VersionInfoId INNER JOIN dbo.ProductMaster ON dbo.VersionInfoMaster.ProductId = dbo.ProductMaster.ProductId Where ProductCodeDetailTbl.Active='1' and dbo.ProductMaster.ProductId=" + ddpname.SelectedValue + "  order  by  WebsiteID  ";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        GridView3.DataSource = dtcln;
        GridView3.DataBind();
    }
    protected void ch1_chachedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow item in GridView3.Rows)
        {
            CheckBox cbItem1 = (CheckBox)item.FindControl("cbItem");
            cbItem1.Checked = ((CheckBox)sender).Checked;
        }
    }
    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //VersionDate
            Label lbl_PortalMasterTblID = (Label)e.Row.FindControl("lbl_PortalMasterTblID");
            Label lblwebid = (Label)e.Row.FindControl("lblwebid");
            Label lblcheck = (Label)e.Row.FindControl("lblcheck");

            CheckBox cbItem = (CheckBox)(e.Row.FindControl("cbItem"));
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            string strwebsite = " SELECT   TOP (1) * From PortalMasterTbl_AllowWebsite Where PortalMasterTblID='" + HiddenField1.Value + "' and WebsiteID='" + lblwebid.Text + "' ";
            SqlCommand cmd12web = new SqlCommand(strwebsite, con);
            SqlDataAdapter adp12web = new SqlDataAdapter(cmd12web);
            DataTable ds12web = new DataTable();
            adp12web.Fill(ds12web);
            if (ds12web.Rows.Count > 0)
            { 
                cbItem.Checked = true;                       
            }
            else
            {               
                cbItem.Checked = false;               
            }
        }
    }
    protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }
    protected void GridView3_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView3.PageIndex = e.NewPageIndex;
        FillWebsiteGrid();
    }
    protected void GridView3_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        FillWebsiteGrid();
    }
    protected void GridView3_SelectedIndexChanged(object sender, EventArgs e)
    {
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
    //-***************************
    //-***********Website Grid Database
    protected void FillDatabaseGrid()
    {
        string strsearch = "";
        strsearch = " ";
        string strcln = "  ";
        strcln = "  SELECT DISTINCT CASE WHEN CompanyDefaultData = '1' THEN cast ('1' as bit) ELSE cast ('0' as bit) END AS chk, dbo.ProductCodeDetailTbl.CodeTypeName, dbo.ProductCodeDatabasDetailWithWebsiteID.ProductCodeDatabasDetailID, dbo.ProductCodeDetailTbl.CompanyDefaultData FROM dbo.ProductCodeDatabasDetailWithWebsiteID INNER JOIN dbo.CodeTypeTbl INNER JOIN dbo.ProductCodeDetailTbl ON dbo.CodeTypeTbl.ProductCodeDetailId = dbo.ProductCodeDetailTbl.Id ON  dbo.ProductCodeDatabasDetailWithWebsiteID.ProductCodeDatabasDetailID = dbo.ProductCodeDetailTbl.Id INNER JOIN dbo.ProductMaster INNER JOIN dbo.VersionInfoMaster ON dbo.ProductMaster.ProductId = dbo.VersionInfoMaster.ProductId ON dbo.CodeTypeTbl.ProductVersionId = dbo.VersionInfoMaster.VersionInfoId WHERE (dbo.ProductCodeDetailTbl.Active = '1') and dbo.ProductMaster.ProductId=" + ddpname.SelectedValue + "  order  by  ProductCodeDatabasDetailID  ";
       
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        GridView4.DataSource = dtcln;
        GridView4.DataBind();
    }
    protected void ch4_chachedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow item in GridView4.Rows)
        {
            CheckBox cbItem1 = (CheckBox)item.FindControl("cbItem");
            cbItem1.Checked = ((CheckBox)sender).Checked;
        }
    }
    protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //VersionDate
            Label lbl_codedetailid = (Label)e.Row.FindControl("lbl_codedetailid");            
            Label lblcheck = (Label)e.Row.FindControl("lblcheck");
            CheckBox cbItem = (CheckBox)(e.Row.FindControl("cbItem"));
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            string strwebsite = " SELECT   TOP (1) * From ProductCodeDetailTbl Where Id='" + lbl_codedetailid.Text + "' and dbo.ProductCodeDetailTbl.CompanyDefaultData='1' ";
            SqlCommand cmd12web = new SqlCommand(strwebsite, con);
            SqlDataAdapter adp12web = new SqlDataAdapter(cmd12web);
            DataTable ds12web = new DataTable();
            adp12web.Fill(ds12web);
            if (ds12web.Rows.Count > 0)
            {
                cbItem.Checked = true;
            }
            else
            {
                cbItem.Checked = false;
            }
        }
    }
    protected void GridView4_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }
    protected void GridView4_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView4.PageIndex = e.NewPageIndex;
        FillDatabaseGrid();
    }
    protected void GridView4_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        FillDatabaseGrid();
    }
    protected void GridView4_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
  
    //-***************************
  
    
    protected void btnSubmit_ClickGo(object sender, EventArgs e)
    {
        fillgriddata();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        //int flag = 0;

        //if (RadioButtonList1.SelectedValue == "1")
        //{
        //    flag = 1;
        //}
        //if (RadioButtonList1.SelectedValue == "0")
        //{
        //    if (Label97.Text == "1")
        //    {
        //        flag = 1;
        //    }
        //    else
        //    {
        //        lblmsg.Visible = true;
        //        lblmsg.Text = "Please check your database connection details and try again..";
        //    }
        //}

        //if (flag == 1)
        //{

            // String str1 = "Select * from  PortalMasterTbl where PortalName='" + txtportname.Text + "'";

            SqlCommand cmd = new SqlCommand("Selectinsertchek", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PortalName", txtportname.Text);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                lblmsg.Text = "Record Already Exist.";
                Panel1.Visible = false;
            }
            else
            {
                // string str = "Insert Into PortalMasterTbl (ProductId,PortalName,DefaultPagename,LogoPath,EmailDisplayname,EmailId,UserIdtosendmail,Password,Mailserverurl,Supportteamemailid,Supportteamphoneno,Supportteammanagername,Portalmarketingwebsitename,Address1,Address2,CountryId,StateId,City,Zip,PhoneNo,Fax,Status) values ('" + ddpname.SelectedValue + "','" + txtportname.Text + "','"+txtaspxpagename.text+"','" + imglogo.ImageUrl + "','" + txtmemailname.Text + "','" + txtemail.Text + "','" + txtuserid.Text + "','" + TextEmailMasterLoginPassword.Text + "','" + TextIncomingMailServer.Text + "','" + txtsupptememail.Text + "','" + txtspno.Text + "','" + txtmanagername.Text + "','" + txtSiteName.Text + "','" + txtAddress1.Text + "','" + txtaddress2.Text + "','" + ddlcountry.SelectedValue + "','" + ddlstate.SelectedValue + "','" + txtcity.Text + "','" + TextZip.Text + "','" + TextPhone1.Text + "','" + txtfax.Text + "','" + ddlstatus.SelectedValue + "')";
                SqlCommand cmd1 = new SqlCommand("PortalMasterTbl_AddDeleteUpdate", con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@StatementType", "Insert");
                cmd1.Parameters.AddWithValue("@ProductId", ddpname.SelectedValue);
                cmd1.Parameters.AddWithValue("@PortalName", txtportname.Text);
                cmd1.Parameters.AddWithValue("@DefaultPagename", txtaspxpagename.Text);
                cmd1.Parameters.AddWithValue("@LogoPath", imglogo.AlternateText);
                cmd1.Parameters.AddWithValue("@EmailDisplayname", txtmemailname.Text);
                cmd1.Parameters.AddWithValue("@EmailId", txtemail.Text);
                cmd1.Parameters.AddWithValue("@UserIdtosendmail", txtuserid.Text);
                cmd1.Parameters.AddWithValue("@Password", TextEmailMasterLoginPassword.Text);
                cmd1.Parameters.AddWithValue("@Mailserverurl", TextIncomingMailServer.Text);
                cmd1.Parameters.AddWithValue("@Supportteamemailid", txtsupptememail.Text);
                cmd1.Parameters.AddWithValue("@Supportteamphoneno", txtspno.Text);
                cmd1.Parameters.AddWithValue("@Supportteammanagername", txtmanagername.Text);
                cmd1.Parameters.AddWithValue("@Portalmarketingwebsitename", txtSiteName.Text);
                cmd1.Parameters.AddWithValue("@Address1", txtAddress1.Text);
                cmd1.Parameters.AddWithValue("@Address2", txtaddress2.Text);
                cmd1.Parameters.AddWithValue("@CountryId", ddlcountry.SelectedValue);
                cmd1.Parameters.AddWithValue("@StateId", ddlstate.SelectedValue);
                cmd1.Parameters.AddWithValue("@City", txtcity.Text);
                cmd1.Parameters.AddWithValue("@Zip", TextZip.Text);
                cmd1.Parameters.AddWithValue("@PhoneNo", TextPhone1.Text);
                cmd1.Parameters.AddWithValue("@Fax", txtfax.Text);
                cmd1.Parameters.AddWithValue("@Status", ddlstatus.SelectedValue);
                cmd1.Parameters.AddWithValue("@Supportteamphonenoext", txtextension.Text);
                cmd1.Parameters.AddWithValue("@Tollfree", txttollfree.Text);
                cmd1.Parameters.AddWithValue("@Tollfreeext", txtex.Text);
                cmd1.Parameters.AddWithValue("@CompanyCreationOption", RadioButtonList1.SelectedValue);
                cmd1.Parameters.AddWithValue("@PortalRunningCompanyID", DropDownList1.SelectedValue);
                cmd1.Parameters.AddWithValue("@Colorportal", txtcolour.Text);
                cmd1.Parameters.AddWithValue("@PortNo",TextIncomingMailServerport.Text);              
                cmd1.Parameters.AddWithValue("@OwnServerAllow",chk_OwnServer.Checked);
                cmd1.Parameters.AddWithValue("@IsWebBasedApplications", Chk_IsWebBasedApplications.Checked);
                cmd1.Parameters.AddWithValue("@CommonServerAllow", chbbusiServer.Checked);
                cmd1.Parameters.AddWithValue("@SharedServerAllow", chkSharedServer.Checked);
                cmd1.Parameters.AddWithValue("@LeaseServerAllow", chkLeaseServer.Checked);
                cmd1.Parameters.AddWithValue("@SaleServerAllow", chksale.Checked);
                cmd1.Parameters.AddWithValue("@IsHostingServer", Chk_portalIsforServer.Checked);
                cmd.Parameters.AddWithValue("@IsDownloadableSoftware", ChkDoenlodeblesw.Checked);
                SqlDataAdapter da2 = new SqlDataAdapter(cmd1);
                DataTable dt2 = new DataTable();
                cmd1.ExecuteNonQuery();
                con.Close();
                //----------------------------------
                //-------------------------------------------------------
                string strmax = " Select Max(ID) as ID from PortalMasterTbl";
                SqlCommand cmdmax = new SqlCommand(strmax, con);
                DataTable dtmax = new DataTable();
                SqlDataAdapter adpmax = new SqlDataAdapter(cmdmax);
                adpmax.Fill(dtmax);
                string id = "";
                if (dtmax.Rows.Count > 0)
                {
                    HiddenField1.Value = dtmax.Rows[0]["ID"].ToString();
                }
                //------------------------------------------------------------------------------------
                //------------------------------------------------------------------------------------
                //-----------------------Priority
                if (ViewState["rankduplicate"] == "")
                {
                    string update = " delete  from PortalMaster_CommonServerPriorty where PortalID='" + HiddenField1.Value + "' ";
                    cmd = new SqlCommand(update, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd.ExecuteNonQuery();
                    con.Close();
                    foreach (GridViewRow ggg in GridView2.Rows)
                    {
                        TextBox res = (TextBox)ggg.FindControl("txtgrid_Priority");
                        Label lblserverid = (Label)ggg.FindControl("lblserverid");
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        SqlCommand cmd21 = new SqlCommand("PortalMaster_CommonServerPriorty_AddDeleteUpdate", con);
                        cmd21.CommandType = CommandType.StoredProcedure;
                        cmd21.Parameters.AddWithValue("@StatementType", "Insert");
                        cmd21.Parameters.AddWithValue("@PortalID", HiddenField1.Value);
                        cmd21.Parameters.AddWithValue("@ServerID", lblserverid.Text);
                        cmd21.Parameters.AddWithValue("@Priority", res.Text);
                        cmd21.Parameters.AddWithValue("@Availability", 1);
                        cmd21.ExecuteNonQuery();
                    }
                }
                 foreach (GridViewRow ggg in GridView3.Rows)
                    {                       
                        Label lblwebid = (Label)ggg.FindControl("lblwebid");
                        CheckBox cbItem = (CheckBox)ggg.FindControl("cbItem");
                        if (cbItem.Checked == true)
                        {
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            SqlCommand cmd21 = new SqlCommand("PortalMasterTbl_AllowWebsite_AddDeleteUpdate", con);
                            cmd21.CommandType = CommandType.StoredProcedure;
                            cmd21.Parameters.AddWithValue("@StatementType", "Insert");
                            cmd21.Parameters.AddWithValue("@PortalMasterTblID", HiddenField1.Value);
                            cmd21.Parameters.AddWithValue("@WebsiteID", lblwebid.Text);
                            cmd21.Parameters.AddWithValue("@ProductID", ddpname.SelectedValue);
                            cmd21.ExecuteNonQuery();
                        }
                    }
                
                //-----------------------
                Clear();
                lblmsg.Visible = true;
                lblmsg.Text = "Record Inserted sucessfully.";
                //btndosyncro_Click(sender, e);
            }
        //}
        //else
        //{
            
        //}

    }
    protected void ddlcountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcountry.SelectedIndex > 0)
        {
            fillddlstate();
        }
    }
    protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
    {

    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        lbltestdata.BackColor = System.Drawing.Color.Empty;
        lbladd.Text = "";
        Panel1.Visible = true;
        ddpname.SelectedIndex = 0;
        txtportname.Text = "";
        ddlstate.SelectedIndex = 0;
        ddlcountry.SelectedIndex = 0;
        imglogo.Visible = false;
        txtmemailname.Text = "";
        txtemail.Text = "";
        txtaspxpagename.Text = "";
        txtex.Text = "";
        txtextension.Text = "";
        txttollfree.Text = "";
        txtuserid.Text = "";
        TextIncomingMailServer.Text = "";
        TextBox3.Attributes.Clear();
        TextEmailMasterLoginPassword.Attributes.Clear();
        txtsupptememail.Text = "";
        txtspno.Text = "";
        txtmanagername.Text = "";
        txtSiteName.Text = "";
        txtaddress2.Text = "";
        txtAddress1.Text = "";
        ddlcountry.SelectedIndex = 0;
        ddlstatus.SelectedIndex = 0;
        ddlstate.SelectedIndex = 0;
        txtcity.Text = "";
        TextPhone1.Text = "";
        txtfax.Text = "";
        TextZip.Text = "";
        BtnUpdate.Visible = false;
        btnSubmit.Visible = true;
        addnewpanel.Visible = true;
        pnladdnew.Visible = false;
        lblmsg.Text = "";
        txtcolour.Text = "";
        imglogo.AlternateText = "";

    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");

            Button1.Text = "Hide Printable Version";
            Button4.Visible = true;
            if (GridView1.Columns[8].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[8].Visible = false;
            }
            if (GridView1.Columns[9].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                GridView1.Columns[9].Visible = false;
            }
        }
        else
        {

            Button1.Text = "Printable Version";
            Button4.Visible = false;
            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[8].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                GridView1.Columns[9].Visible = true;
            }
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgriddata();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        lblmsg.Text = "";
        if (e.CommandName == "edit")
        {

            lbladd.Text = "Edit Product Portal Details";
            addnewpanel.Visible = false;
            pnladdnew.Visible = true;
            BtnUpdate.Visible = true;
            btnSubmit.Visible = false;

            //SqlCommand cmd = new SqlCommand("SELECT * from PortalMasterTbl where Id='" + e.CommandArgument.ToString() + "'", con);
            SqlCommand cmd = new SqlCommand("PortalMasterTbl_AddDeleteUpdate", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StatementType", "Select");
            cmd.Parameters.AddWithValue("@Id", e.CommandArgument.ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtbn = new DataTable();
            da.Fill(dtbn);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
             if (dtbn.Rows.Count > 0)
            {

                fillgriddata();
                ddpname.SelectedIndex = ddpname.Items.IndexOf(ddpname.Items.FindByValue(dtbn.Rows[0]["ProductId"].ToString()));
                imglogo.Visible = true;
                HiddenField1.Value = dtbn.Rows[0]["Id"].ToString();
                ddpname.SelectedValue = dtbn.Rows[0]["ProductId"].ToString();
                txtportname.Text = dtbn.Rows[0]["PortalName"].ToString();
                txtaspxpagename.Text = dtbn.Rows[0]["DefaultPagename"].ToString();              
              
                imglogo.ImageUrl ="~/images/"+ dtbn.Rows[0]["LogoPath"].ToString();
                imglogo.AlternateText = dtbn.Rows[0]["LogoPath"].ToString();
                txtmemailname.Text = dtbn.Rows[0]["EmailDisplayname"].ToString();
                txtemail.Text = dtbn.Rows[0]["EmailId"].ToString();
                txtuserid.Text = dtbn.Rows[0]["UserIdtosendmail"].ToString();
                TextIncomingMailServer.Text = dtbn.Rows[0]["Mailserverurl"].ToString();
                txtcolour.Text = Convert.ToString(dtbn.Rows[0]["Colorportal"]);

                TextEmailMasterLoginPassword.Text = dtbn.Rows[0]["Password"].ToString();
                string strqa3 = TextEmailMasterLoginPassword.Text;
                TextEmailMasterLoginPassword.Attributes.Add("Value", strqa3);

                TextBox3.Text = dtbn.Rows[0]["Password"].ToString();
                string strqa4 = TextBox3.Text;
                TextBox3.Attributes.Add("Value", strqa4);

                txtsupptememail.Text = dtbn.Rows[0]["Supportteamemailid"].ToString();
                txtspno.Text = dtbn.Rows[0]["Supportteamphoneno"].ToString();
                txtmanagername.Text = dtbn.Rows[0]["Supportteammanagername"].ToString();
                txtSiteName.Text = dtbn.Rows[0]["Portalmarketingwebsitename"].ToString();
                txtAddress1.Text = dtbn.Rows[0]["Address1"].ToString();
                txtaddress2.Text = dtbn.Rows[0]["Address2"].ToString();
                ddlcountry.SelectedValue = dtbn.Rows[0]["CountryId"].ToString();
                fillddlstate();
                ddlstate.SelectedValue = dtbn.Rows[0]["StateId"].ToString();
                txtcity.Text = dtbn.Rows[0]["City"].ToString();
                TextZip.Text = dtbn.Rows[0]["Zip"].ToString();

                TextPhone1.Text = dtbn.Rows[0]["PhoneNo"].ToString();
                txtfax.Text = dtbn.Rows[0]["Fax"].ToString();
                txtextension.Text = dtbn.Rows[0]["Supportteamphonenoext"].ToString();
                txttollfree.Text = dtbn.Rows[0]["Tollfree"].ToString();
                txtex.Text = dtbn.Rows[0]["Tollfreeext"].ToString();
                TextIncomingMailServerport.Text = dtbn.Rows[0]["PortNo"].ToString();

                string st = dtbn.Rows[0]["Status"].ToString();
                if (st == "True")
                {
                    ddlstatus.SelectedValue = "1";
                }
                if (st == "False")
                {
                    ddlstatus.SelectedValue = "0";
                }

                        chkClr();
                        RbselectPortalUse_SelectedIndexChanged(sender, e);
                        chkBusiwizServer_CheckedChanged(sender, e);       
                    try
                    {
                        Boolean Bolle_IsHosting = Convert.ToBoolean(dtbn.Rows[0]["IsHostingServer"].ToString());
                        Boolean Bolle_IsDownSoftware = Convert.ToBoolean(dtbn.Rows[0]["IsDownloadableSoftware"].ToString());
                        Boolean Bolle_IsWebBasedApplications = Convert.ToBoolean(dtbn.Rows[0]["IsWebBasedApplications"].ToString());

                        if(Bolle_IsHosting==true)
                        {
                                Chk_portalIsforServer.Checked = true;                          
                                pnlServerForPortal.Visible = false;
                                pnlwebsitecretion.Visible = false;
                                RbselectPortalUse.SelectedValue = "2";
                              
                        }
                        else if (Bolle_IsDownSoftware == true)
                        {
                                ChkDoenlodeblesw.Checked = true;                           
                                pnlServerForPortal.Visible = false;
                                pnlwebsitecretion.Visible = false;
                                RbselectPortalUse.SelectedValue = "1";
                            
                        }
                        else 
                        {
                            Chk_IsWebBasedApplications.Checked = true;
                            RbselectPortalUse.SelectedValue = "3";

                            pnlwebsitecretion.Visible = true;

                            string companyoption = dtbn.Rows[0]["CompanyCreationOption"].ToString();
                            if (companyoption == "0")
                            {
                                RadioButtonList1.SelectedValue = companyoption;
                                Panel6.Visible = true;
                                pnlServerForPortal.Visible = false;
                                fillcompany();
                                DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByValue(dtbn.Rows[0]["PortalRunningCompanyID"].ToString()));
                                
                            }
                            if (companyoption == "1")
                            {
                                RadioButtonList1.SelectedValue = companyoption;
                                Panel6.Visible = false;
                                pnlServerForPortal.Visible = true;
                                int any1true = 0;
                                try
                                {
                                    chk_OwnServer.Checked = Convert.ToBoolean(dtbn.Rows[0]["OwnServerAllow"].ToString());                                  
                                }
                                catch (Exception ex)
                                {
                                    chk_OwnServer.Checked = false;
                                }                              
                                try
                                {
                                    chkSharedServer.Checked = Convert.ToBoolean(dtbn.Rows[0]["SharedServerAllow"].ToString());
                                    any1true = 1;
                                }
                                catch (Exception ex)
                                {
                                    chkSharedServer.Checked = false;
                                }
                                try
                                {
                                    chkLeaseServer.Checked = Convert.ToBoolean(dtbn.Rows[0]["LeaseServerAllow"].ToString());
                                    any1true = 1;
                                }
                                catch (Exception ex)
                                {
                                    chkLeaseServer.Checked = false;
                                }
                                try
                                {
                                    chksale.Checked = Convert.ToBoolean(dtbn.Rows[0]["SaleServerAllow"].ToString());
                                    any1true = 1;
                                }
                                catch (Exception ex)
                                {
                                    chksale.Checked = false;
                                }
                                try
                                {
                                    chbbusiServer.Checked = Convert.ToBoolean(dtbn.Rows[0]["CommonServerAllow"].ToString());
                                    any1true = 1;                                   
                                }
                                catch (Exception ex)
                                {
                                    chbbusiServer.Checked = false;                                   
                                }
                                if (any1true == 1)
                                {
                                    chkBusiwizServer.Checked = true; 
                                }
                                RadioButtonList1_SelectedIndexChanged(sender, e);
                                chkBusiwizServer_CheckedChanged(sender, e);
                            }
                            FillWebsiteGrid();
                            FillDatabaseGrid();
                        }
                        
  

                    }
                    catch (Exception ex)
                    {                       
                    }
                  //  RbselectPortalUse_SelectedIndexChanged(sender ,e);
                con.Close();
            }

        }

        if (e.CommandName == "Delete")
        {

            //SqlCommand cmd1 = new SqlCommand("Delete  From PortalMasterTbl  where Id='" + e.CommandArgument.ToString() + "'", con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("PortalMasterTbl_AddDeleteUpdate", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StatementType", "Delete");
            cmd.Parameters.AddWithValue("@Id", e.CommandArgument.ToString());
            cmd.ExecuteNonQuery();
            lblmsg.Text = "Record deleted successfully.";
            fillgriddata();
            con.Close();

            ddpname.SelectedIndex = 0;
            btnSubmit.Visible = true;
            BtnUpdate.Visible = false;
            fillgriddata();


        }

    }
    protected void btnUpload_Click1(object sender, EventArgs e)
    {
        if (imglogo.ImageUrl == "")
        {
            lblmsg.Text = " Please Select Logo. ";
        }
        if (FileUpload1.HasFile)
        {
            imglogo.Visible = true;
            lblmsg.Text = "";
            imglogo.ImageUrl = "";
            string str = Path.GetExtension(FileUpload1.PostedFile.FileName);
            switch (str.ToLower())
            {
                case ".bmp":
                case ".gif":
                case ".jpg":
                case ".jpeg":
                case ".png":
                    break;
            }
            if (str == ".bmp" || str == ".gif" || str == ".jpg" || str == ".jpeg" || str == ".png")
            {
                lblmsg.Text = "";
                string filename = Path.GetFileName(FileUpload1.FileName);
                FileUpload1.SaveAs(Server.MapPath("~\\images\\") + filename);
                temp = filename;
                imglogo.ImageUrl = "~/images/" + temp;
                imglogo.AlternateText = temp;
            }
            else
            {
                imglogo.ImageUrl = "";
                lblmsg.Text = "Invalid file extension.";
            }
        }
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void GridView1_RowEditing1(object sender, GridViewEditEventArgs e)
    {

    }
    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        //string str11 = "Select * From PortalMasterTbl Where  PortalName ='" +txtportname.Text +"' and Id<>'" + HiddenField1.Value + "'";
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        SqlCommand cmd1 = new SqlCommand("selectupdateportal", con);
        SqlDataAdapter ad = new SqlDataAdapter(cmd1);
        cmd1.CommandType = CommandType.StoredProcedure;
        cmd1.Parameters.AddWithValue("@Id", HiddenField1.Value);
        cmd1.Parameters.AddWithValue("@PortalName", txtportname.Text);

        cmd1.ExecuteNonQuery();
        DataTable dt = new DataTable();

        ad.Fill(dt);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }


        if (dt.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Record already exist.";
            con.Close();
            Panel1.Visible = false;

            BtnUpdate.Visible = true;
            btnSubmit.Visible = false;
        }

        else
        {

            //string qry = "update  PortalMasterTbl set ProductId='" + ddpname.SelectedValue + "',PortalName='" + txtportname.Text + "',LogoPath='" + imglogo.ImageUrl + "',EmailDisplayname='" + txtmemailname.Text + "',EmailId='" + txtemail.Text + "',UserIdtosendmail='" + txtuserid.Text + "',Password='" + TextEmailMasterLoginPassword.Text + "',Mailserverurl='" + TextIncomingMailServer.Text + "',Supportteamemailid='" + txtsupptememail.Text + "',Supportteamphoneno='" + txtspno.Text + "',Supportteammanagername='" + txtmanagername.Text + "',Portalmarketingwebsitename='" + txtSiteName.Text + "',Address1='" + txtAddress1.Text + "',Address2='" + txtaddress2.Text + "',CountryId='" + ddlcountry.SelectedValue + "',StateId='" + ddlstate.SelectedValue + "',City='" + txtcity.Text + "',Zip='" + TextZip.Text + "',PhoneNo='" + TextPhone1.Text + "',Fax='" + txtfax.Text + "',Status='"+ ddlstatus.SelectedValue + +"' where Id='"+HiddenField1.Value+"'";
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("PortalMasterTbl_AddDeleteUpdate", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StatementType", "Update");
            cmd.Parameters.AddWithValue("@Id", HiddenField1.Value);
            cmd.Parameters.AddWithValue("@ProductId", ddpname.SelectedValue);
            cmd.Parameters.AddWithValue("@PortalName", txtportname.Text);
            cmd.Parameters.AddWithValue("@DefaultPagename", txtaspxpagename.Text);
            cmd.Parameters.AddWithValue("@LogoPath", imglogo.AlternateText);
            cmd.Parameters.AddWithValue("@EmailDisplayname", txtmemailname.Text);
            cmd.Parameters.AddWithValue("@EmailId", txtemail.Text);
            cmd.Parameters.AddWithValue("@UserIdtosendmail", txtuserid.Text);
            cmd.Parameters.AddWithValue("@Password", TextEmailMasterLoginPassword.Text);
            cmd.Parameters.AddWithValue("@Mailserverurl", TextIncomingMailServer.Text);
            cmd.Parameters.AddWithValue("@Supportteamemailid", txtsupptememail.Text);
            cmd.Parameters.AddWithValue("@Supportteamphoneno", txtspno.Text);
            cmd.Parameters.AddWithValue("@Supportteammanagername", txtmanagername.Text);
            cmd.Parameters.AddWithValue("@Portalmarketingwebsitename", txtSiteName.Text);
            cmd.Parameters.AddWithValue("@Address1", txtAddress1.Text);
            cmd.Parameters.AddWithValue("@Address2", txtaddress2.Text);
            cmd.Parameters.AddWithValue("@CountryId", ddlcountry.SelectedValue);
            cmd.Parameters.AddWithValue("@StateId", ddlstate.SelectedValue);
            cmd.Parameters.AddWithValue("@City", txtcity.Text);
            cmd.Parameters.AddWithValue("@Zip", TextZip.Text);
            cmd.Parameters.AddWithValue("@PhoneNo", TextPhone1.Text);
            cmd.Parameters.AddWithValue("@Fax", txtfax.Text);
            cmd.Parameters.AddWithValue("@Supportteamphonenoext", txtextension.Text);
            cmd.Parameters.AddWithValue("@Tollfree", txttollfree.Text);
            cmd.Parameters.AddWithValue("@Tollfreeext", txtex.Text);
            cmd.Parameters.AddWithValue("@Status", ddlstatus.SelectedValue);
            cmd.Parameters.AddWithValue("@CompanyCreationOption", RadioButtonList1.SelectedValue);
            cmd.Parameters.AddWithValue("@PortalRunningCompanyID", DropDownList1.SelectedValue);
            cmd.Parameters.AddWithValue("@Colorportal", txtcolour.Text);
            cmd.Parameters.AddWithValue("@PortNo", TextIncomingMailServerport.Text);
            cmd.Parameters.AddWithValue("@OwnServerAllow", chk_OwnServer.Checked);
            cmd.Parameters.AddWithValue("@IsWebBasedApplications", Chk_IsWebBasedApplications.Checked);
            cmd.Parameters.AddWithValue("@CommonServerAllow", chbbusiServer.Checked);
            cmd.Parameters.AddWithValue("@SharedServerAllow", chkSharedServer.Checked);
            cmd.Parameters.AddWithValue("@LeaseServerAllow", chkLeaseServer.Checked);
            cmd.Parameters.AddWithValue("@SaleServerAllow", chksale.Checked);
            cmd.Parameters.AddWithValue("@IsHostingServer", Chk_portalIsforServer.Checked);
            cmd.Parameters.AddWithValue("@IsDownloadableSoftware", ChkDoenlodeblesw.Checked);
            
            cmd.ExecuteNonQuery();

            //--Insert Priority------------------------
            if (ViewState["rankduplicate"] == "")
            {
                string update = " delete  from PortalMaster_CommonServerPriorty where PortalID='" + HiddenField1.Value + "' ";
                cmd = new SqlCommand(update, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                con.Close();
                foreach (GridViewRow ggg in GridView2.Rows)
                {
                    TextBox res = (TextBox)ggg.FindControl("txtgrid_Priority");
                    Label lblserverid = (Label)ggg.FindControl("lblserverid");

                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    SqlCommand cmd21 = new SqlCommand("PortalMaster_CommonServerPriorty_AddDeleteUpdate", con);
                    cmd21.CommandType = CommandType.StoredProcedure;
                    cmd21.Parameters.AddWithValue("@StatementType", "Insert");
                    cmd21.Parameters.AddWithValue("@PortalID", HiddenField1.Value);
                    cmd21.Parameters.AddWithValue("@ServerID", lblserverid.Text);
                    cmd21.Parameters.AddWithValue("@Priority", res.Text);
                    cmd21.Parameters.AddWithValue("@Availability", 1);                   
                    cmd21.ExecuteNonQuery();
                } 
            }
            
            //----------
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            SqlCommand cmd2121 = new SqlCommand("PortalMasterTbl_AllowWebsite_AddDeleteUpdate", con);
            cmd2121.CommandType = CommandType.StoredProcedure;
            cmd2121.Parameters.AddWithValue("@StatementType", "DeleteWithportal");
            cmd2121.Parameters.AddWithValue("@PortalMasterTblID", HiddenField1.Value);           
            cmd2121.ExecuteNonQuery();
            foreach (GridViewRow ggg in GridView3.Rows)
            {
               
                Label lblwebid = (Label)ggg.FindControl("lblwebid");
                CheckBox cbItem = (CheckBox)ggg.FindControl("cbItem");
                if (cbItem.Checked == true)
                {

                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    SqlCommand cmd21 = new SqlCommand("PortalMasterTbl_AllowWebsite_AddDeleteUpdate", con);
                    cmd21.CommandType = CommandType.StoredProcedure;
                    cmd21.Parameters.AddWithValue("@StatementType", "Insert");
                    cmd21.Parameters.AddWithValue("@PortalMasterTblID", HiddenField1.Value);
                    cmd21.Parameters.AddWithValue("@WebsiteID", lblwebid.Text);
                    cmd21.Parameters.AddWithValue("@ProductID", ddpname.SelectedValue);
                    cmd21.ExecuteNonQuery();
                }
            }
            Clear();
            lblmsg.Visible = true;
            lblmsg.Text = "Record updated successfully";           
            btndosyncro_Click(sender, e);
        }
    }
   

    protected void addnewpanel_Click(object sender, EventArgs e)
    {

        lblmsg.Text = "";

        pnladdnew.Visible = true;
        Label1.Visible = true;
        addnewpanel.Visible = false;
        lbladd.Text = "Add Portal";
        ddlstatus.SelectedIndex = 0;
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl1 = (Label)e.Row.FindControl("lblstatus");
                Label lbl2 = (Label)e.Row.FindControl("lblproductid2");
                Label lbl3 = (Label)e.Row.FindControl("lblproductid3");
                if (lbl1.Text == "True")
                {
                    lbl2.Visible = true;
                    lbl3.Visible = false;
                }
                if (lbl1.Text == "False")
                {
                    lbl3.Visible = true;
                    lbl2.Visible = false;
                }
            }
        }

    }
    protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgriddata();
    }
    protected void ddlsrechportal_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgriddata();



    }
    private void portal()
    {
        SqlCommand cmd;
       //SqlCommand cmd = new SqlCommand("Select  distinct ProductId,ProductName from ProductMaster order by ProductName", con);
        if (CheckBox1.Checked == true)
        {
             cmd = new SqlCommand("Select Distinct PortalName From PortalMasterTbl where PortalMasterTbl.Status=1  Order by PortalName", con);
        }
        else
        {
             cmd = new SqlCommand("Select Distinct PortalName From PortalMasterTbl  Order by PortalName", con);
     
        }
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }

        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ddlsrechportal.DataSource = dt;
            ddlsrechportal.DataTextField = "PortalName";
            //ddpname.DataValueField = "ProductId";
            ddlsrechportal.DataBind();

        }
        ddlsrechportal.Items.Insert(0, "All");
        ddlsrechportal.Items[0].Value = "0";

    }

    protected void ddlserchstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgriddata();
    }
    protected void RbselectPortalUse_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RbselectPortalUse.SelectedValue == "1")
        {
            Chk_portalIsforServer.Checked = false;            
            ChkDoenlodeblesw.Checked = true;
            pnlwebsitecretion.Visible = false;
            Chk_IsWebBasedApplications.Checked = false;
            Chk_portalIsforServer_CheckedChanged(sender, e);
        }
        else if (RbselectPortalUse.SelectedValue == "2")
        {
            Chk_portalIsforServer.Checked = true;
            pnlwebsitecretion.Visible = false;
            ChkDoenlodeblesw.Checked = false;
            Chk_IsWebBasedApplications.Checked = false;
            Chk_portalIsforServer_CheckedChanged(sender, e);
        }
        else if (RbselectPortalUse.SelectedValue == "3")
        {
            Chk_portalIsforServer.Checked = false;
            pnlwebsitecretion.Visible = true;
            ChkDoenlodeblesw.Checked = false;
            Chk_IsWebBasedApplications.Checked = true;
            Chk_portalIsforServer_CheckedChanged(sender, e);
            RadioButtonList1_SelectedIndexChanged(sender, e);
        }
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedValue == "0")
        {
            Panel6.Visible = true;
            pnlServerForPortal.Visible = false;
        }
        else
        {
            Panel6.Visible = false;
            pnlServerForPortal.Visible = true;
           
        }
    }
   

    protected void fillcompany()
    {
        DropDownList1.Items.Clear();
        string sgggg = " SELECT *, CompanyLoginId  +':'+ CompanyName as CompanyNamenew  from CompanyMaster where ProductId='" + ddpname.SelectedValue + "'  order By CompanyLoginId  ";
        SqlCommand cmdgrid = new SqlCommand(sgggg, con);      
        SqlDataAdapter dtpgrid = new SqlDataAdapter(cmdgrid);
        DataTable dtgrid = new DataTable();
        dtpgrid.Fill(dtgrid);


        DropDownList1.DataSource = dtgrid;
        DropDownList1.DataTextField = "CompanyNamenew";
        DropDownList1.DataValueField = "CompanyLoginId";
        DropDownList1.DataBind();
        DropDownList1.Items.Insert(0, "-Select-");
        DropDownList1.Items[0].Value = "0";
    }
   
    protected void btntest_Click(object sender, EventArgs e)
    {
        lbltestdata.BackColor = System.Drawing.Color.Empty;
        if (txtcolour.Text.Length > 0)
        {
            try
            {
                lbltestdata.BackColor = System.Drawing.ColorTranslator.FromHtml(txtcolour.Text);
            }
            catch
            {
                lblmsg.Text = "Colour not found.";
            }
        }
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        FillProduct();
       // FillVersion(); 
        fillgriddata();       
        
    }
    protected void CheckBox10_CheckedChanged(object sender, EventArgs e)
    {
        
    }
    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
        fillgriddata();
    }


    protected DataTable select(string str)
    {
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);

        return dt;

    }

    protected void btndosyncro_Clickpop(object sender, EventArgs e)
    {
       // ModernpopSync.Show();
    }
    protected void btndosyncro_Click(object sender, EventArgs e)
    {
        int transf = 0;


        DataTable dt1 = select("SELECT DISTINCT SatelliteSyncronisationrequiringTablesMaster.Id FROM ClientProductTableMaster INNER JOIN SatelliteSyncronisationrequiringTablesMaster ON ClientProductTableMaster.Id = SatelliteSyncronisationrequiringTablesMaster.TableID where SatelliteSyncronisationrequiringTablesMaster.Status='1' and (ClientProductTableMaster.TableName='PortalMasterTbl' OR ClientProductTableMaster.TableName='PortaldesignationTbl'  )  ");
        if (dt1.Rows.Count > 0)
        {
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                string datetim = DateTime.Now.ToString();
                string arqid = dt1.Rows[i]["Id"].ToString();

                string str22 = "Insert Into SyncronisationrequiredTbl(SatelliteSyncronisationrequiringTablesMasterID,DateandTime)Values('" + arqid + "','" + Convert.ToDateTime(datetim) + "')";
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                SqlCommand cmn = new SqlCommand(str22, con);
                cmn.ExecuteNonQuery();
                con.Close();

                DataTable dt121 = select("SELECT Max(ID) as ID from SyncronisationrequiredTbl where SatelliteSyncronisationrequiringTablesMasterID='" + arqid + "'");

                if (Convert.ToString(dt121.Rows[0]["ID"]) != "")
                {
                    DataTable dtcln = select("SELECT Distinct ServerMasterTbl.Id FROM ServerMasterTbl inner join ServerAssignmentMasterTbl on ServerAssignmentMasterTbl.ServerId=ServerMasterTbl.Id inner join  PricePlanMaster on PricePlanMaster.PricePlanId=ServerAssignmentMasterTbl.PricePlanId    where ServerMasterTbl.Status='1' and ServerAssignmentMasterTbl.Active='1' and PricePlanMaster.active='1' ");

                    for (int j = 0; j < dtcln.Rows.Count; j++)
                    {
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }

                        string str223 = "Insert Into SateliteServerRequiringSynchronisationMasterTbl(SyncronisationrequiredTBlID,[servermasterID],[SynchronisationSuccessful],[SynchronisationSuccessfulDatetime])Values('" + dt121.Rows[0]["ID"] + "','" + dtcln.Rows[j]["Id"] + "','0','" + DateTime.Now.ToString() + "')";
                        SqlCommand cmn3 = new SqlCommand(str223, con);
                        cmn3.ExecuteNonQuery();
                        con.Close();
                        transf = Convert.ToInt32(rdsync.SelectedValue);
                    }
                }


            }

        }


        else
        {

        }
        if (transf > 0)
        {
            string te = "SyncData.aspx";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

        }
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
   
  
  
   
   protected void  chkClr()
   {
       chk_OwnServer.Checked = false;
       chbbusiServer.Checked = false;
       chkLeaseServer.Checked = false;
       chkSharedServer.Checked = false;
       chksale.Checked = false;
       pnlcommon.Visible = false;
       
        ChkDoenlodeblesw.Checked = false;
        Chk_portalIsforServer.Checked = false;
        Chk_IsWebBasedApplications.Checked = false;

        pnlwebsitecretion.Visible = false;
        Panel6.Visible = false;
        pnlServerForPortal.Visible = false;
        chk_OwnServer.Checked = false;
        chkBusiwizServer.Checked = false;
        pnlbusiwizserver.Visible = false; 
       
        
                             
                               
   }
    

    //---Registretion Company Server 
    protected void Chk_portalIsforServer_CheckedChanged(object sender, EventArgs e)
    {
        if (Chk_portalIsforServer.Checked == true || ChkDoenlodeblesw.Checked ==true )
        {
            pnlServerForPortal.Visible = false;
            pnlwebsitecretion.Visible = false;           
        }
        else if(Chk_IsWebBasedApplications.Checked ==true)
        {            
            pnlwebsitecretion.Visible = true;
        }
    }

    protected void chk_OwnServer_CheckedChanged(object sender, EventArgs e)
    {
      
    }

    protected void chkBusiwizServer_CheckedChanged(object sender, EventArgs e)
    {
        if (chkBusiwizServer.Checked == true)
        {
            pnlbusiwizserver.Visible = true;
            serverCommonFill();

        }
        else
        {
            pnlbusiwizserver.Visible = false;
            chbbusiServer.Checked = false;
            chkLeaseServer.Checked = false;
            chkSharedServer.Checked = false;
            chksale.Checked = false;
            pnlcommon.Visible = false; 
        }
    }


    //New Server Selection Option

    protected void chbbusiServer_CheckedChanged(object sender, EventArgs e)
    {
        //pnlcommon
        if (chbbusiServer.Checked == true)
        {
            pnlcommon.Visible = true;
        }
        else
        {
            pnlcommon.Visible = false;            
        }
    }
    protected void btnbutProductmodul_Click(object sender, EventArgs e)
    {
       
    }
    protected void chkLeaseServer_CheckedChanged(object sender, EventArgs e)
    {
      
    }
    protected void chkSharedServer_CheckedChanged(object sender, EventArgs e)
    {
       
    }
    protected void chksale_CheckedChanged(object sender, EventArgs e)
    {
      
    }

    //-----------------------------
    protected void serverCommonFill()
    {
        //string strcln = " SELECT  distinct Top(10)  MaxCompSharing,ServerName,Id FROM ServerMasterTbl where Status='1'  order  by ServerName ";//and MaxCommonCompanyShared='1'
        //SqlCommand cmdcln = new SqlCommand(strcln, con);
        //DataTable dtcln = new DataTable();
        //SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        //adpcln.Fill(dtcln);
        ////--
        //GridView2.DataSource = dtcln;
        //GridView2.DataBind();
        //---

        string dd = "";
        dd = " SELECT  distinct Top(10) Id ,MaxCommonCompanyShared as MaxCompSharing,ServerName FROM ServerMasterTbl where Status='1' and ServerType='0' and MaxCommonCompanyShared>0 order  by ServerName ";

        SqlDataAdapter da1 = new SqlDataAdapter(dd, con);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);
        if (dt1.Rows.Count > 0)
        {
            DataTable dt_s = new DataTable();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                string ff = " SELECT  * From PortalMaster_CommonServerPriorty Where PortalID='" + HiddenField1.Value + "' and ServerID='" + dt1.Rows[i][0].ToString() + "' ";
                SqlDataAdapter da = new SqlDataAdapter(ff, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt_s.Rows.Count < 1)
                {
                    dt_s.Columns.Add("Id");
                    dt_s.Columns.Add("MaxCompSharing");
                    dt_s.Columns.Add("ServerName");
                    dt_s.Columns.Add("Priority");                  
                }
                DataRow dr = dt_s.NewRow();
                dr["Id"] = dt1.Rows[i]["Id"].ToString();
                dr["MaxCompSharing"] = dt1.Rows[i]["MaxCompSharing"].ToString();
                dr["ServerName"] = dt1.Rows[i]["ServerName"].ToString();
                
                if (dt.Rows.Count > 0)
                {
                    dr["Priority"] = dt.Rows[0]["Priority"].ToString();
                }
                else
                {
                    dr["Priority"] = "";
                }
                dt_s.Rows.Add(dr);
            }

            GridView2.DataSource = dt_s;
            GridView2.DataBind();           
            chbbusiServer.Visible = true;
            chbbusiServer.Checked = true;
            pnlcommon.Visible = true;  
        }
        else
        {
            chbbusiServer.Visible = false; 
            GridView2.DataSource = null;
            pnlcommon.Visible = false;
            GridView2.DataBind();           
        }
    }
    protected void Btnchangerank_Click(object sender, EventArgs e)
    {
        if (Button3.Text == "Change Rank")
        {
            foreach (GridViewRow ggg in GridView2.Rows)
            {
                TextBox res = (TextBox)ggg.FindControl("txtgrid_Priority");
                res.Enabled = true;
            }
            Button2.Text = "Update";
        }
        else if (Button3.Text == "Update")
        {

            if (ViewState["rankduplicate"] == "")
            {
                foreach (GridViewRow ggg in GridView2.Rows)
                {
                    TextBox res = (TextBox)ggg.FindControl("txtgrid_Priority");
                    Label lbl = (Label)ggg.FindControl("Label22");

                    string update = "update PortalMaster_CommonServerPriorty set Priority='" + res.Text + "' where PortalID='" + lbl.Text + "' ";
                    SqlCommand cmd = new SqlCommand(update, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    res.Enabled = false;

                }
                Button2.Text = "Change Rank";
                fillgriddata();
            }
            else
            {
                lblmsg.Text = "Duplicate values are not permitted!! ";
            }



        }
    }
    protected void txtgrid_Priority_TextChanged(object sender, EventArgs e)
    {
        lblmsg.Text = " ";
        ViewState["rankduplicate"] = "";
        TextBox lnkbtn = (TextBox)sender;
        GridViewRow row = (GridViewRow)lnkbtn.NamingContainer;
        int j = Convert.ToInt32(row.RowIndex);
        TextBox rank = (TextBox)GridView2.Rows[j].FindControl("txtgrid_Priority");
        int count3 = Convert.ToInt16(rank.Text);
        int count1 = GridView1.Rows.Count + 1;
        if (count3 <= count1)
        {
            foreach (GridViewRow ggg in GridView2.Rows)
            {
                int count = ggg.RowIndex;

                TextBox res = (TextBox)ggg.FindControl("txtgrid_Priority");

                if (j != count)
                {

                    if (res.Text == rank.Text)
                    {
                        lblmsg.Text = "Duplicate values are not permitted!! ";
                        ViewState["rankduplicate"] = 1;
                    }
                    else
                    {

                    }
                }
            }
        }
        else
        {
            ViewState["rankduplicate"] = 1;
            lblmsg.Text = "Please Enter the correct order from the list";
        }
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton lnkbtn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)lnkbtn.NamingContainer;
        int j = Convert.ToInt32(row.RowIndex);
        Label id = (Label)GridView2.Rows[j].FindControl("lblserverid");

        string update = " delete  from PortalMaster_CommonServerPriorty where ID='" + id.Text + "' ";
        SqlCommand cmd = new SqlCommand(update, con);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        fillgriddata();
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate) && (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header))
        {
            e.Row.Cells[0].Visible = false;

        }
    }
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        fillgriddata();
    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "View1")
        {
            int hh = Convert.ToInt32(e.CommandArgument);

            string te = "PageMasterNew.aspx?id=" + hh;
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        }
    }
    //
 

}
