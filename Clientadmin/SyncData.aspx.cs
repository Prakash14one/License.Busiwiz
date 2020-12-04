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
using System.Data.Sql;
using System.ServiceModel;
using System.Data.SqlTypes;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Security.Cryptography;
using System.IO;
using System.Text;

public partial class SyncData : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);

    DataSet dt;
    SqlConnection conn;
    public SqlConnection connweb;
    public static string encstr = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["ClientId"] = "35";

        if (!IsPostBack)
        {
            ViewState["sortOrder"] = "";
            txtStartdate.Text = DateTime.Now.ToShortDateString();
            txtfdate.Text = DateTime.Now.ToShortDateString();
            txttodate.Text = DateTime.Now.ToShortDateString();
            FillProduct();
             if (Request.QueryString["verId"] != null )
             {
                 chkall.Checked = true;
                 pnlsync.Visible = true;
                 ddlProductname.SelectedIndex = ddlProductname.Items.IndexOf(ddlProductname.Items.FindByValue(Request.QueryString["verId"].ToString()));
                 ddlProductname_SelectedIndexChanged(sender, e);
                 btnSubmit_Click(sender, e);
            }
            else if (Request.QueryString["Pvid"] != null && Request.QueryString["Pt"] != null && Request.QueryString["pcat"] != null)
            {
                ddlProductname.SelectedIndex = ddlProductname.Items.IndexOf(ddlProductname.Items.FindByValue(Request.QueryString["Pvid"].ToString()));
                fillportal();
                ddlportal.SelectedIndex = ddlportal.Items.IndexOf(ddlportal.Items.FindByValue(Request.QueryString["Pt"].ToString()));
                fillpriceplancate();
                ddlpriceplancatagory.SelectedIndex = ddlpriceplancatagory.Items.IndexOf(ddlpriceplancatagory.Items.FindByValue(Request.QueryString["pcat"].ToString()));
                ddlpriceplancatagory_SelectedIndexChanged(sender, e);
                //SeprateDatabase();
                Session["succc"] = "tn";
                Page.Response.Redirect("PageAccessUser.aspx");
            }
            else
            {
                ddlProductname_SelectedIndexChanged(sender, e);
            }
             ddlproductfilter_SelectedIndexChanged(sender, e);
             btnfilgo_Click(sender, e);
        }

    }
    protected void FillProduct()
    {

        //string strcln = " SELECT * from  ProductMaster where ClientMasterId= " + Session["ClientId"].ToString();

        string strcln = " SELECT distinct ProductMaster.ProductId, VersionInfoMaster.VersionInfoId,ProductMaster.ProductName + ' : ' + VersionInfoMaster.VersionInfoName as productversion FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.VersionNo=VersionInfoMaster.VersionInfoName    where ClientMasterId=" + Session["ClientId"].ToString() + " and ProductDetail.Active ='True' order  by productversion";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        ddlProductname.DataSource = dtcln;

        ddlProductname.DataValueField = "VersionInfoId";
        ddlProductname.DataTextField = "productversion";
        ddlProductname.DataBind();
        ddlProductname.Items.Insert(0, "-Select-");
        ddlProductname.Items[0].Value = "0";
        ddlproductfilter.DataSource = dtcln;

        ddlproductfilter.DataValueField = "VersionInfoId";
        ddlproductfilter.DataTextField = "productversion";
        ddlproductfilter.DataBind();
        //ddlproductfilter.Items.Insert(0, "All");
        //ddlproductfilter.Items[0].Value = "0";
    }

    protected void ddlProductname_SelectedIndexChanged(object sender, EventArgs e)
    {

        Label1.Text = "";
        fillportal();
        fillSynctableor();
        ddlpriceplan_SelectedIndexChanged(sender, e);
    }
    protected void fillportal()
    {
        ddlportal.Items.Clear();
        string strcln22v = "Select * from PortalMasterTbl where ProductId In( Select distinct ProductMaster.ProductId from  ProductMaster  inner join VersionInfoMaster on VersionInfoMaster.productId=ProductMaster.ProductId where VersionInfoId = '" + ddlProductname.SelectedValue + "' ) order by PortalName";
        SqlCommand cmdcln22v = new SqlCommand(strcln22v, con);
        DataTable dtcln22v = new DataTable();
        SqlDataAdapter adpcln22v = new SqlDataAdapter(cmdcln22v);
        adpcln22v.Fill(dtcln22v);
        ddlportal.DataSource = dtcln22v;

        ddlportal.DataValueField = "Id";
        ddlportal.DataTextField = "PortalName";
        ddlportal.DataBind();
        ddlportal.Items.Insert(0, "All");
        ddlportal.Items[0].Value = "0";
    }
    protected void fillportalfilter()
    {
        ddlportalfilter.Items.Clear();
        //if (ddlproductfilter.SelectedIndex > 0)
        //{
            string strcln22v = "Select * from PortalMasterTbl where ProductId In( Select distinct ProductMaster.ProductId from  ProductMaster  inner join VersionInfoMaster on VersionInfoMaster.productId=ProductMaster.ProductId where VersionInfoId = '" + ddlproductfilter.SelectedValue + "' ) order by PortalName";
            SqlCommand cmdcln22v = new SqlCommand(strcln22v, con);
            DataTable dtcln22v = new DataTable();
            SqlDataAdapter adpcln22v = new SqlDataAdapter(cmdcln22v);
            adpcln22v.Fill(dtcln22v);
            ddlportalfilter.DataSource = dtcln22v;

            ddlportalfilter.DataValueField = "Id";
            ddlportalfilter.DataTextField = "PortalName";
            ddlportalfilter.DataBind();
        //}
        ddlportalfilter.Items.Insert(0, "All");
        ddlportalfilter.Items[0].Value = "0";
    }
    protected void fillSynctableor()
    {
        ddlsynctable.Items.Clear();
        if (ddlProductname.SelectedIndex > 0)
        {
            string strcln22v = "Select  case when(TableName Is Null) then '-' else TableName End  +' : '+Name as TableName,Id from SatelliteSyncronisationrequiringTablesMaster where   ProductVersionID = '" + ddlProductname.SelectedValue + "'  order by TableName";
        SqlCommand cmdcln22v = new SqlCommand(strcln22v, con);
        DataTable dtcln22v = new DataTable();
        SqlDataAdapter adpcln22v = new SqlDataAdapter(cmdcln22v);
        adpcln22v.Fill(dtcln22v);
        ddlsynctable.DataSource = dtcln22v;

        ddlsynctable.DataValueField = "Id";
        ddlsynctable.DataTextField = "TableName";
        ddlsynctable.DataBind();
        }
        ddlsynctable.Items.Insert(0, "All");
        ddlsynctable.Items[0].Value = "0";
    }
    protected void fillSynctable()
    {
        ddlsyncreqtbl.Items.Clear();
        //if (ddlproductfilter.SelectedIndex > 0)
        //{
        string strcln22v = "Select  case when(TableName Is Null) then '-' else TableName End  +' : '+Name as TableName,Id from SatelliteSyncronisationrequiringTablesMaster where   ProductVersionID = '" + ddlproductfilter.SelectedValue + "'  order by TableName";
        SqlCommand cmdcln22v = new SqlCommand(strcln22v, con);
        DataTable dtcln22v = new DataTable();
        SqlDataAdapter adpcln22v = new SqlDataAdapter(cmdcln22v);
        adpcln22v.Fill(dtcln22v);
        ddlsyncreqtbl.DataSource = dtcln22v;

        ddlsyncreqtbl.DataValueField = "Id";
        ddlsyncreqtbl.DataTextField = "TableName";
        ddlsyncreqtbl.DataBind();
        //}
        ddlsyncreqtbl.Items.Insert(0, "All");
        ddlsyncreqtbl.Items[0].Value = "0";
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        grdserver.DataSource = null;
        grdserver.DataBind();
        pnltransst.Visible = false;
        /////////After Seprate Databasewise
        //string HashKey = "";
        //encstr = CreateLicenceKey(out HashKey);
        GenerateGrid();
       // fillgrid("");
        //if (grdhistory.Rows.Count > 0)
        //{
        //    pnlhist.Visible = true;
        //}
        //else
        //{
        //    pnlhist.Visible = false;
        //}
        // SeprateDatabase();


    }
    protected void fillgrid(string peram)
    {

        string serId = "";
        if (ddlportalfilter.SelectedIndex > 0)
        {
            serId = serId + " and PortalMasterTbl.Id='" + ddlportalfilter.SelectedValue + "'";
        }
        if (ddlcategoryfilter.SelectedIndex > 0)
        {
            serId = serId + " and Priceplancategory.Id='" + ddlcategoryfilter.SelectedValue + "'";
        }
        if (ddlserverfilter.SelectedIndex > 0)
        {
            serId = serId + " and ServerMasterTbl.Id='" + ddlserverfilter.SelectedValue + "'";
        }
        if (ddlsyncreqtbl.SelectedIndex > 0)
        {
            serId = serId + " and SatelliteSyncronisationrequiringTablesMaster.Id='" + ddlsyncreqtbl.SelectedValue + "'";
        }
        if (txtStartdate.Text.Length > 0)
        {
            serId = serId + " and  Cast(SynchronisationAttemptDatetime as Date)='" + txtStartdate.Text + "'";
        }
      
        string indv = "";
        if (ddlportalfilter.SelectedIndex > 0)
        {
            indv += indv + " and PortalMasterTbl.Id='" + ddlportalfilter.SelectedValue + "'";
        }
        if (ddlcategoryfilter.SelectedIndex > 0)
        {
            indv += indv + " and Priceplancategory.Id='" + ddlcategoryfilter.SelectedValue + "'";
        }
        string filt = "";
        if (Convert.ToString(hdnsortExp.Value) == "")
        {
            filt = " Order by SateliteServerRequiringSynchronisationDetailTbl.Id Desc";
        }
        
       
        DataTable dtsedata = selectBZ("select Distinct  top(60) SateliteServerRequiringSynchronisationMasterTbl.servermasterID,  SatelliteSyncronisationrequiringTablesMaster.Name as tabdesname,ServerMasterTbl.ServerName,ServerMasterTbl.serverloction,SyncronisationrequiredTbl.DateandTime,SateliteServerRequiringSynchronisationDetailTbl.* from  SatelliteSyncronisationrequiringTablesMaster inner join SyncronisationrequiredTbl on SyncronisationrequiredTbl.SatelliteSyncronisationrequiringTablesMasterID=SatelliteSyncronisationrequiringTablesMaster.Id inner join " +
                   " SateliteServerRequiringSynchronisationMasterTbl on SateliteServerRequiringSynchronisationMasterTbl.SyncronisationrequiredTBlID=SyncronisationrequiredTbl.Id inner join SateliteServerRequiringSynchronisationDetailTbl on " +
        " SateliteServerRequiringSynchronisationDetailTbl.SateliteServerRequiringSynchronisationMasterTblID=SateliteServerRequiringSynchronisationMasterTbl.Id inner join ServerMasterTbl on ServerMasterTbl.Id=SateliteServerRequiringSynchronisationMasterTbl.servermasterID inner join ServerAssignmentMasterTbl on  ServerAssignmentMasterTbl.ServerId=SateliteServerRequiringSynchronisationMasterTbl.servermasterID inner join PriceplanMaster on PriceplanMaster.PricePlanId " +
         " =ServerAssignmentMasterTbl.PricePlanId inner join Priceplancategory on Priceplancategory.Id=PriceplanMaster.PriceplancatId inner join PortalMasterTbl on " +
                "  PortalMasterTbl.Id =Priceplancategory.PortalId "+
                               " where  SatelliteSyncronisationrequiringTablesMaster.Status='1' and SatelliteSyncronisationrequiringTablesMaster.ProductVersionID='" + ddlproductfilter.SelectedValue + "' " + serId + peram + " order by SateliteServerRequiringSynchronisationDetailTbl.Id Desc");
        if (dtsedata.Rows.Count > 0)
        {
            DataTable dtTemp = new DataTable();
            dtTemp = CreateDataGD();
         
            foreach (DataRow item in dtsedata.Rows)
            {

                DataRow dtr2 = dtTemp.NewRow();
                dtr2["ServerName"] = Convert.ToString(item["ServerName"]);
                dtr2["SynchronisationAttemptDatetime"] = Convert.ToString(item["SynchronisationAttemptDatetime"]);
                dtr2["serverloction"] = Convert.ToString(item["serverloction"]);
                dtr2["Id"] = Convert.ToString(item["Id"]);
                dtr2["tabdesname"] = Convert.ToString(item["tabdesname"]);
                dtr2["DateandTime"] = Convert.ToString(item["DateandTime"]);
                dtr2["servermasterID"] = Convert.ToString(item["servermasterID"]);
                dtr2["SynchronisationSuccessful"] = Convert.ToString(item["SynchronisationSuccessful"]);
                dtr2["SynchronisationAttemptErromMessage"] = Convert.ToString(item["SynchronisationAttemptErromMessage"]);

                DataTable dtg = selectBZ("select Distinct   PortalMasterTbl.Id as PTID, PortalMasterTbl.PortalName,Priceplancategory.CategoryName,Priceplancategory.ID as PTCID from   PriceplanMaster inner join ServerAssignmentMasterTbl on PriceplanMaster.PricePlanId= " +
                " ServerAssignmentMasterTbl.PricePlanId inner join Priceplancategory on Priceplancategory.Id=PriceplanMaster.PriceplancatId inner join PortalMasterTbl on " +
                "  PortalMasterTbl.Id =Priceplancategory.PortalId where PriceplanMaster.active='1' and PriceplanMaster.VersionInfoMasterId='" + ddlproductfilter.SelectedValue + "' and ServerAssignmentMasterTbl.ServerId='" + Convert.ToString(item["servermasterID"]) + "'" + indv);
                if (dtg.Rows.Count > 0)
                {
                    dtr2["PortalName"] = Convert.ToString(dtg.Rows[0]["PortalName"]);
                    dtr2["CategoryName"] = Convert.ToString(dtg.Rows[0]["CategoryName"]);
                }
                else
                {
                    dtr2["PortalName"] = "";
                    dtr2["CategoryName"] = "";
                }
                dtTemp.Rows.Add(dtr2);

            }
            DataView myDataView = new DataView();
            myDataView = dtTemp.DefaultView;
            
            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }
           
            grdhistory.DataSource = myDataView;
            grdhistory.DataBind();
            ViewState["Data"] = dtTemp;
        }
        else
        {
            grdhistory.DataSource = null;
            grdhistory.DataBind();

        }
    }

    protected void GenerateGrid()
    {
        int totnoc = 0;
        string portid = "";
        string pcateid = "";
        string serId = "";
        if (ddlportal.SelectedIndex > 0)
        {
            portid = " and Id='" + ddlportal.SelectedValue + "'";
        }
        if (ddlpriceplancatagory.SelectedIndex > 0)
        {
            pcateid = " and Priceplancategory.Id='" + ddlpriceplancatagory.SelectedValue + "'";
        }
        if (ddlserver.SelectedIndex > 0)
        {
            serId = " and ServerMasterTbl.Id='" + ddlserver.SelectedValue + "'";
        }
        string tablefil = "";
        if (ddlsynctable.SelectedIndex > 0)
        {
            tablefil = tablefil + " and SatelliteSyncronisationrequiringTablesMaster.Id='" + ddlsynctable.SelectedValue + "'";
        } 
        string datefil = "";
        if (txtfdate.Text.Length > 0 && txttodate.Text.Length>0)
        {
            datefil = datefil + " and  Cast(DateandTime as Date) between '" + txtStartdate.Text + "' and '" + txttodate.Text + "' ";
        }
        DataTable dtTemp = new DataTable();
        dtTemp = CreateData();
        int kl = 1;
      
            conn = new SqlConnection();
            DataTable dtcln = new DataTable();

            dtcln = selectBZ("SELECT Distinct ServerMasterTbl.* FROM CompanyMaster inner join  ServerMasterTbl on ServerMasterTbl.Id=CompanyMaster.ServerId inner join ServerAssignmentMasterTbl on ServerAssignmentMasterTbl.ServerId=ServerMasterTbl.Id inner join  PricePlanMaster on PricePlanMaster.PricePlanId=ServerAssignmentMasterTbl.PricePlanId inner join Priceplancategory on Priceplancategory.Id=PricePlanMaster.PriceplancatId   where ServerMasterTbl.Status='1' and ServerAssignmentMasterTbl.Active='1' and PricePlanMaster.active='1' and  PricePlanMaster.VersionInfoMasterId='" + ddlProductname.SelectedValue + "' and Priceplancategory.PortalId in" +
                "(Select Distinct Id from PortalMasterTbl where ProductId In( Select distinct ProductMaster.ProductId from  ProductMaster  inner join VersionInfoMaster on VersionInfoMaster.productId=ProductMaster.ProductId where CompanyMaster.active='1' and VersionInfoId = '" + ddlProductname.SelectedValue + "' ) " + portid + ") " + pcateid + serId + "");
            foreach (DataRow item in dtcln.Rows)
            {
                DataTable dtfindtab = selectBZ("select Distinct ClientProductTableMaster.Id as TableId from ClientProductTableMaster inner join SatelliteSyncronisationrequiringTablesMaster on SatelliteSyncronisationrequiringTablesMaster.TableID=ClientProductTableMaster.Id inner join SyncronisationrequiredTbl on SyncronisationrequiredTbl.SatelliteSyncronisationrequiringTablesMasterID=SatelliteSyncronisationrequiringTablesMaster.Id inner join " +
                   " SateliteServerRequiringSynchronisationMasterTbl on SateliteServerRequiringSynchronisationMasterTbl.SyncronisationrequiredTBlID=SyncronisationrequiredTbl.Id inner join ServerMasterTbl on ServerMasterTbl.Id=SateliteServerRequiringSynchronisationMasterTbl.servermasterID where SatelliteSyncronisationrequiringTablesMaster.Status='1' and  SateliteServerRequiringSynchronisationMasterTbl.SynchronisationSuccessful='0' and SateliteServerRequiringSynchronisationMasterTbl.servermasterID='" + item["Id"] + "' and SatelliteSyncronisationrequiringTablesMaster.ProductVersionID='" + ddlProductname.SelectedValue + "'" + tablefil);
                foreach (DataRow igb in dtfindtab.Rows)
                {
                    DataTable dtsedata = selectBZ("select Distinct top(1) Failmsg,FailAttemp,Faildatetime, ClientProductTableMaster.TableName,SatelliteSyncronisationrequiringTablesMaster.Name as tabdesname,ServerMasterTbl.ServerName,ServerMasterTbl.serverloction,SyncronisationrequiredTbl.DateandTime,SateliteServerRequiringSynchronisationMasterTbl.servermasterID,SateliteServerRequiringSynchronisationMasterTbl.Id from ClientProductTableMaster inner join SatelliteSyncronisationrequiringTablesMaster on SatelliteSyncronisationrequiringTablesMaster.TableID=ClientProductTableMaster.Id inner join SyncronisationrequiredTbl on SyncronisationrequiredTbl.SatelliteSyncronisationrequiringTablesMasterID=SatelliteSyncronisationrequiringTablesMaster.Id inner join " +
                   " SateliteServerRequiringSynchronisationMasterTbl on SateliteServerRequiringSynchronisationMasterTbl.SyncronisationrequiredTBlID=SyncronisationrequiredTbl.Id inner join ServerMasterTbl on ServerMasterTbl.Id=SateliteServerRequiringSynchronisationMasterTbl.servermasterID where SatelliteSyncronisationrequiringTablesMaster.Status='1' and  SateliteServerRequiringSynchronisationMasterTbl.SynchronisationSuccessful='0' and SateliteServerRequiringSynchronisationMasterTbl.servermasterID='" + item["Id"] + "' and SatelliteSyncronisationrequiringTablesMaster.TableID='" + igb["TableId"] + "' and  SatelliteSyncronisationrequiringTablesMaster.ProductVersionID='" + ddlProductname.SelectedValue + "' "+datefil+" order by SateliteServerRequiringSynchronisationMasterTbl.Id Desc");
                    if (dtsedata.Rows.Count > 0)
                    {
                        DataRow dtr2 = dtTemp.NewRow();
                        dtr2["ServerName"] = Convert.ToString(dtsedata.Rows[0]["ServerName"]);
                        dtr2["TableName"] = Convert.ToString(dtsedata.Rows[0]["TableName"]);
                        dtr2["serverloction"] = Convert.ToString(dtsedata.Rows[0]["serverloction"]);
                        dtr2["Id"] = Convert.ToString(dtsedata.Rows[0]["Id"]);
                        dtr2["tabdesname"] = Convert.ToString(dtsedata.Rows[0]["tabdesname"]);
                        dtr2["DateandTime"] = Convert.ToString(dtsedata.Rows[0]["DateandTime"]);
                        dtr2["servermasterID"] = Convert.ToString(dtsedata.Rows[0]["servermasterID"]);
                        //dtr2["PortalId"] = Convert.ToString(impor["Id"]); ;
                        dtr2["PortalId"] = "";
                        if (Convert.ToString(dtsedata.Rows[0]["FailAttemp"]) != "")
                        {
                            dtr2["Attempt"] = Convert.ToString(dtsedata.Rows[0]["FailAttemp"]); 
                        }
                        else
                        {
                            dtr2["Attempt"] = "0";
                        }
                        dtr2["Msg"] = "";
                        if (Convert.ToString(dtsedata.Rows[0]["FailAttemp"]) != "" && Convert.ToString(dtsedata.Rows[0]["Faildatetime"]) != "")
                        {
                            dtr2["Msg"] = Convert.ToString(dtsedata.Rows[0]["FailAttemp"]) + " Fail - Last on " + Convert.ToString(dtsedata.Rows[0]["Faildatetime"]);
                        }
                        dtTemp.Rows.Add(dtr2);
                        kl = kl + 1;
                    }

                }
            }
        ViewState["Datac"] = dtTemp;
        DataView myDataView = new DataView();
        myDataView = dtTemp.DefaultView;

        if (hdnsortExp1.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp1.Value, hdnsortDir1.Value);
        }

        grdserver.DataSource = myDataView;
        grdserver.DataBind();
       
        //if (grdserver.Rows.Count > 0)
        //{
            pnltransst.Visible = true;
        //}
    }
    protected void ch1_chachedChanged(object sender, EventArgs e)
    {
        //GridViewRow row = ((CheckBox)sender).Parent.Parent as GridViewRow;

        //int rinrow = row.RowIndex;
        foreach (GridViewRow item in grdserver.Rows)
        {
            CheckBox cbItem1 = (CheckBox)item.FindControl("cbItem");
            cbItem1.Checked = ((CheckBox)sender).Checked;
        }
    }
    protected void SeprateDatabase()
    {
        int totnoc = 0;
        string portid = "";
        string pcateid = "";
        string serId = "";
        if (ddlportal.SelectedIndex > 0)
        {
            portid = " and Id='" + ddlportal.SelectedValue + "'";
        }
        if (ddlpriceplancatagory.SelectedIndex > 0)
        {
            pcateid = " and Priceplancategory.Id='" + ddlpriceplancatagory.SelectedValue + "'";
        }
        if (ddlserver.SelectedIndex > 0)
        {
            serId = " and ServerMasterTbl.Id='" + ddlpriceplancatagory.SelectedValue + "'";
        }

        conn = new SqlConnection();
        DataTable dtcln = new DataTable();

        foreach (GridViewRow item in grdserver.Rows)
        {
            string syncreqid = grdserver.DataKeys[item.RowIndex].Value.ToString();
            Label lbltabname = (Label)item.FindControl("lbltabname");
            Label lblseid = (Label)item.FindControl("lblseid");
            Label lblattempt = (Label)item.FindControl("lblattempt");
            CheckBox cbItem = (CheckBox)item.FindControl("cbItem");

            string verid = ddlProductname.SelectedValue;
            string CID = "";
            if (cbItem.Checked == true)
            {
                DataTable dtre = selectBZ("select * from ServerMasterTbl where Id='" + lblseid.Text + "'");
                if (dtre.Rows.Count > 0)
                {
                    encstr = "&%#@?,:*";
                    string serversqlserverip = dtre.Rows[0]["sqlurl"].ToString();
                    string serversqlinstancename = dtre.Rows[0]["DefaultsqlInstance"].ToString();
                    string serversqldbname = dtre.Rows[0]["DefaultDatabaseName"].ToString();
                    string serversqlpwd = dtre.Rows[0]["Sapassword"].ToString();
                    string serversqlport = dtre.Rows[0]["port"].ToString();
                    try
                    {
                        totnoc = 1;
                        conn = new SqlConnection();
                      //  conn.ConnectionString = @"Data Source =" + serversqlserverip + "\\" + serversqlinstancename + "," + serversqlport + "; Initial Catalog=" + serversqldbname + "; User ID=Sa; Password=" + PageMgmt.Decrypted(serversqlpwd) + "; Persist Security Info=true;";
                         //Data Source =C3\C3SERVERMASTER,30000; Initial Catalog=C3SATELLITESERVER; User ID=Sa; Password=06De1963++; Persist Security Info=true;
                         //conn.ConnectionString = @"Data Source =" + dtre.Rows[0]["PublicIp"] + "," + dtcheck1.Rows[0]["Port"] + "; Initial Catalog = " + dtcheck1.Rows[0]["DatabaseName"] + "; Integrated Security = true";
                         // conn = new SqlConnection(@"Data Source =" + Convert.ToString(dtre.Rows[0]["sqlurl"]) + "\\" + Convert.ToString(dtre.Rows[0]["DefaultsqlInstance"]) + "," + dtre.Rows[0]["port"] + "; Initial Catalog = " + Convert.ToString(dtre.Rows[0]["DefaultDatabaseName"]) + "; User ID=Sa; Password=" + Decrypted(Convert.ToString(dtre.Rows[0]["Sapassword"])) + "; Persist Security Info=true;");

                         //  conn = new SqlConnection(@"Data Source =" + Convert.ToString(dtre.Rows[0]["PublicIp"]) + "\\" + Convert.ToString(dtre.Rows[0]["DefaultsqlInstance"]) + "," + dtre.Rows[0]["port"] + "; Initial Catalog = " + Convert.ToString(dtre.Rows[0]["DefaultDatabaseName"]) + "; User ID=Sa; Password=" + Decrypted(Convert.ToString(dtre.Rows[0]["Sapassword"])) + "");
                          conn = new SqlConnection(@"Data Source =TCP:192.168.2.100,40000; Initial Catalog=C3SATELLITESERVER; User ID=Sa; Password=06De1963++; Persist Security Info=true;");
                        if (conn.State.ToString() != "Open")
                        {
                            conn.Open();
                        }
                        conn.Close();
                        encstr = "";
                        int inv = 0;
                        encstr = Convert.ToString(dtre.Rows[0]["Enckey"]);                       
                        //NEW CODE FOR DYNAMICALLY TABLE----------------------------------                      
                                string tablename = lbltabname.Text;
                                inv = 1;
                                tableins("" + tablename + "");                               
                                DynamicalyTable(tablename);  
                        Label1.Text = "Sync successfully";

                        if (inv == 1)
                        {
                            SatelliteSyncronisationrequiringTablesMaster(verid);
                            SyncronisationrequiredTbl(verid);
                            string insser = "insert into SateliteServerRequiringSynchronisationDetailTbl (SateliteServerRequiringSynchronisationMasterTblID,SynchronisationAttemptDatetime,SynchronisationSuccessful,SynchronisationAttemptErromMessage) Values" +
                                "('" + syncreqid + "','" + DateTime.Now.ToString() + "','" + inv + "','" + Label1.Text + "')";
                            SqlCommand cmdin = new SqlCommand(insser, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            int appd = Convert.ToInt32(cmdin.ExecuteNonQuery());
                            con.Close();
                            if (appd > 0)
                            {
                                string inupd = "Update SateliteServerRequiringSynchronisationMasterTbl set SynchronisationSuccessful='" + inv + "' where Id='" + syncreqid + "'";
                                SqlCommand cmup = new SqlCommand(inupd, con);
                                if (con.State.ToString() != "Open")
                                {
                                    con.Open();
                                }
                                cmup.ExecuteNonQuery();
                                con.Close();
                                DataTable dtcup = selectBZ("Select * from SateliteServerRequiringSynchronisationMasterTbl where Id='" + syncreqid + "'");
                                if (dtcup.Rows.Count > 0)
                                {
                                    string str223 = "Insert Into SateliteServerRequiringSynchronisationMasterTbl(Id,SyncronisationrequiredTBlID,[servermasterID],[SynchronisationSuccessful],[SynchronisationSuccessfulDatetime])Values('" + syncreqid + "'," +
                                    "'" + dtcup.Rows[0]["SyncronisationrequiredTBlID"] + "','" + dtcup.Rows[0]["servermasterID"] + "','" + dtcup.Rows[0]["SynchronisationSuccessful"] + "','" + dtcup.Rows[0]["SynchronisationSuccessfulDatetime"] + "')";
                                    SqlCommand cmsersync = new SqlCommand(str223, conn);
                                    if (conn.State.ToString() != "Open")
                                    {
                                        conn.Open();
                                    }
                                    int serversyn = Convert.ToInt32(cmsersync.ExecuteNonQuery());
                                    conn.Close();
                                    if (serversyn > 0)
                                    {
                                        DataTable dtcupx = selectBZ("Select top(1) * from SateliteServerRequiringSynchronisationDetailTbl where SateliteServerRequiringSynchronisationMasterTblID='" + syncreqid + "' order by Id Desc");
                                        if (dtcupx.Rows.Count > 0)
                                        {
                                            string insserb = "insert into SateliteServerRequiringSynchronisationDetailTbl(Id,SateliteServerRequiringSynchronisationMasterTblID,SynchronisationAttemptDatetime,SynchronisationSuccessful,SynchronisationAttemptErromMessage)Values" +
                                                "('" + dtcupx.Rows[0]["Id"] + "','" + syncreqid + "','" + dtcupx.Rows[0]["SynchronisationAttemptDatetime"] + "','" + inv + "','" + Label1.Text + "')";
                                            SqlCommand cmdinb = new SqlCommand(insserb, conn);
                                            if (conn.State.ToString() != "Open")
                                            {
                                                conn.Open();
                                            }
                                            cmdinb.ExecuteNonQuery();
                                            conn.Close();
                                        }
                                    }
                                }
                            }
                        }
                       
                    }

                    catch (Exception e1)
                    {
                        int noatt = Convert.ToInt32(lblattempt.Text) + 1;
                        string inupd = "Update SateliteServerRequiringSynchronisationMasterTbl set SynchronisationSuccessful='0',Failmsg='Fail',FailAttemp='" + noatt + "',Faildatetime='" + DateTime.Now.ToString() + "' where Id='" + syncreqid + "'";
                        SqlCommand cmup = new SqlCommand(inupd, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmup.ExecuteNonQuery();
                        con.Close();
                        Label1.Text = e1.ToString();
                    }

                }
            }
        }
        if (totnoc == 0)
        {
            Label1.Text = "Record not founds.";
        }
        else
        {
            GenerateGrid();
            fillgrid("");
        }

    }
    protected void tableins(string tablename)
    {
        string st1 = "CREATE TABLE " + tablename + "(";

        DataTable dts1 = selectBZ("select column_name,data_type,CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.COLUMNS where table_name='" + tablename + "'");
        for (int k = 0; k < dts1.Rows.Count; k++)
        {
            if (k == 0)
            {
                //st1 += ("" + dts1.Rows[k]["column_name"] + " int Identity(1,1),");
                st1 += ("" + dts1.Rows[k]["column_name"] + " nvarchar(500),");
            }
            else
            {
                st1 += ("" + dts1.Rows[k]["column_name"] + " " + dts1.Rows[k]["data_type"] + "(" + dts1.Rows[k]["CHARACTER_MAXIMUM_LENGTH"] + "),");

            }
        }
        st1 = st1.Remove(st1.Length - 1);
        st1 += ")";
        //st1 = st1.Replace("int()", "int");
        st1 = st1.Replace("bigint()", "nvarchar(500)");
        st1 = st1.Replace("int()", "nvarchar(500)");
        st1 = st1.Replace("(-1)", "(MAX)");
        st1 = st1.Replace("datetime()", "nvarchar(500)");
        st1 = st1.Replace("nvarchar(50)", "nvarchar(500)");
        st1 = st1.Replace("decimal()", "nvarchar(500)");
        st1 = st1.Replace("decimal", "nvarchar(500)");
        st1 = st1.Replace("bit()", "nvarchar(500)");//st1 = st1.Replace("bit()", "bit");
        DataTable dts = select("select column_name,data_type,CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.COLUMNS where table_name='" + tablename + "'");
        if (dts.Rows.Count <= 0)
        {
            SqlCommand cmdr = new SqlCommand(st1, conn);
            conn.Open();
            cmdr.ExecuteNonQuery();
            conn.Close();
        }
        else
        {
            string strBC = "CREATE TABLE " + tablename + "(";
            DataTable DTBC = select("select column_name,data_type,CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.COLUMNS where table_name='" + tablename + "'");
            for (int k = 0; k < DTBC.Rows.Count; k++)
            {
                if (k == 0)
                {
                    //  strBC += ("" + DTBC.Rows[k]["column_name"] + " int Identity(1,1),");
                    strBC += ("" + DTBC.Rows[k]["column_name"] + " nvarchar(500),");
                }
                else
                {
                    strBC += ("" + DTBC.Rows[k]["column_name"] + " " + DTBC.Rows[k]["data_type"] + "(" + DTBC.Rows[k]["CHARACTER_MAXIMUM_LENGTH"] + "),");

                }
            }
            strBC = strBC.Remove(strBC.Length - 1);
            strBC += ")";
            strBC = strBC.Replace("bigint()", "nvarchar(500)");
            strBC = strBC.Replace("int()", "nvarchar(500)");
            strBC = strBC.Replace("(-1)", "(MAX)");
            strBC = strBC.Replace("datetime()", "nvarchar(500)");
            st1 = st1.Replace("nvarchar(50)", "nvarchar(500)");
            st1 = st1.Replace("decimal()", "nvarchar(500)");
            st1 = st1.Replace("decimal", "nvarchar(500)");
            strBC = strBC.Replace("bit()", "bit");
            if (strBC != st1)
            {
                SqlCommand cmdrX = new SqlCommand("Drop table " + tablename, conn);
                conn.Open();
                cmdrX.ExecuteNonQuery();
                conn.Close();
                SqlCommand cmdr = new SqlCommand(st1, conn);
                conn.Open();
                cmdr.ExecuteNonQuery();
                conn.Close();
            }
            else
            {
                SqlCommand cmdrX = new SqlCommand("Delete  from  " + tablename, conn);
                conn.Open();
                cmdrX.ExecuteNonQuery();
                conn.Close();
            }
        }

    }
    protected void DynamicalyTable(string tanlename)
    {
        string Temp2 = "INSERT INTO " + tanlename + "(  ";
        string Temp3val = "";
        DataTable dts1 = selectBZ("select column_name,data_type,CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.COLUMNS where table_name='" + tanlename + "'");
        for (int k = 0; k < dts1.Rows.Count; k++)
        {

            Temp2 += ("" + dts1.Rows[k]["column_name"] + " ,");

        }
        Temp2 = Temp2.Remove(Temp2.Length - 1);
        Temp2 += ") VAlues";
        DataTable dtr = selectBZ(" Select * From " + tanlename + " ");
        int c = 0;
        foreach (DataRow itm in dtr.Rows)
        {            
            string cccd = "(";
            DataTable dtsccc = selectBZ("select column_name,data_type,CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.COLUMNS where table_name='" + tanlename + "'");
            for (int k = 0; k < dtsccc.Rows.Count; k++)
            {
                cccd += "'" + Encrypted(Convert.ToString(itm["" + dtsccc.Rows[k]["column_name"] + ""])) + "' ,";               
            }
            cccd = cccd.Remove(cccd.Length - 1);
            cccd += " )";
            c++;
            if (c == 199)
            {
                c = 0;
                if (Temp3val.Length > 0)
                {
                    Temp3val += ",";
                }
                Temp3val += cccd;
                if (Temp3val.Length > 0)
                {
                  string tempstr=  Temp2 + Temp3val;
                    SqlCommand ccm = new SqlCommand(tempstr, conn);
                    conn.Open();
                    ccm.ExecuteNonQuery();
                    conn.Close();
                }
                cccd = "";
                Temp3val = "";
            }
            if (Temp3val.Length > 0)
            {
                Temp3val += ",";
            }
            Temp3val += cccd;
            //Temp3val += "('" + Encrypted(Convert.ToString(itm["MasterPageId"])) + "','" + Encrypted(Convert.ToString(itm["MasterPageName"])) + "','" + Encrypted(Convert.ToString(itm["MasterPageDescription"])) + "','" + Encrypted(Convert.ToString(itm["WebsiteSectionId"])) + "')";
        }
        if (Temp3val.Length > 0)
        {
            Temp2 += Temp3val;
            SqlCommand ccm = new SqlCommand(Temp2, conn);
            conn.Open();
            ccm.ExecuteNonQuery();
            conn.Close();
        }
    }
    protected void WEBSITE(string verid, string cid)
    {
        string Temp3val = "";
        string Temp2 = "INSERT INTO WebsiteMaster(ID,WebsiteName,WebsiteUrl,WebsitePort,IISServerIpUrl,IISServerUserId,IISServerPassWord,DatabaseName," +
                " DatabaseServerIpUrl,DatabaseUserId,DatabasePassword,BusiControllerName,BusiControllerDatabaseName,BusiControllerSqlServerIpUrl,BusiControllerUserId,BusiControllerPassword,BusiControllerConnectionString," +
                " FTP_Url,FTP_Port,FTP_UserId,FTP_Password,VersionInfoId,IISAccessPort,DatabaseAccessPort,BusiControllerPort,compid,FTPWorkGuideUrl,FTPWorkGuidePort,FTPWorkGuideUserId,FTPWorkGuidePW)Values ";

        DataTable dtr = selectBZ("Select * from  WebsiteMaster  where [VersionInfoId]='" + verid + "' ");
        foreach (DataRow itm in dtr.Rows)
        {
            if (Temp3val.Length > 0)
            {
                Temp3val += ",";
            }

            Temp3val += "('" + Encrypted(Convert.ToString(itm["ID"])) + "','" + Encrypted(Convert.ToString(itm["WebsiteName"])) + "','" + Encrypted(Convert.ToString(itm["WebsiteUrl"])) + "','" + Encrypted(Convert.ToString(itm["WebsitePort"])) + "','" + Encrypted(Convert.ToString(itm["IISServerIpUrl"])) + "','" + Encrypted(Convert.ToString(itm["IISServerUserId"])) + "','" + Encrypted(Convert.ToString(itm["IISServerPassWord"])) + "'," +
        " '" + Encrypted(Convert.ToString(itm["DatabaseName"])) + "','" + Encrypted(Convert.ToString(itm["DatabaseServerIpUrl"])) + "','" + Encrypted(Convert.ToString(itm["DatabaseUserId"])) + "','" + Encrypted(Convert.ToString(itm["DatabasePassword"])) + "','" + Encrypted(Convert.ToString(itm["BusiControllerName"])) + "','" + Encrypted(Convert.ToString(itm["BusiControllerDatabaseName"])) + "','" + Encrypted(Convert.ToString(itm["BusiControllerSqlServerIpUrl"])) + "','" + Encrypted(Convert.ToString(itm["BusiControllerUserId"])) + "', " +
        " '" + Encrypted(Convert.ToString(itm["BusiControllerPassword"])) + "','" + Encrypted(Convert.ToString(itm["BusiControllerConnectionString"])) + "','" + Encrypted(Convert.ToString(itm["FTP_Url"])) + "','" + Encrypted(Convert.ToString(itm["FTP_Port"])) + "','" + Encrypted(Convert.ToString(itm["FTP_UserId"])) + "'," +
        " '" + Encrypted(Convert.ToString(itm["FTP_Password"])) + "','" + Encrypted(Convert.ToString(itm["VersionInfoId"])) + "','" + Encrypted(Convert.ToString(itm["IISAccessPort"])) + "','" + Encrypted(Convert.ToString(itm["DatabaseAccessPort"])) + "','" + Encrypted(Convert.ToString(itm["BusiControllerPort"])) + "','" + Encrypted(Convert.ToString(itm["compid"])) + "','" + Encrypted(Convert.ToString(itm["FTPWorkGuideUrl"])) + "','" + Encrypted(Convert.ToString(itm["FTPWorkGuidePort"])) + "','" + Encrypted(Convert.ToString(itm["FTPWorkGuideUserId"])) + "','" + Encrypted(Convert.ToString(itm["FTPWorkGuidePW"])) + "')";


        }
        if (Temp3val.Length > 0)
        {
            Temp2 += Temp3val;
            SqlCommand ccm = new SqlCommand(Temp2, conn);
            conn.Open();
            ccm.ExecuteNonQuery();
            conn.Close();
        }
    }

    protected void Section(string verid, string cid)
    {
        string Temp3val = "";
        string Temp2 = "INSERT INTO WebsiteSection(WebsiteSectionId,WebsiteMasterId,SectionName,AfterLoginDefaultPageId,LoginUrl,NormalUrl)Values ";
        DataTable dtr = selectBZ("Select Distinct WebsiteSection.*   from  WebsiteMaster inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.Id where WebsiteMaster.VersionInfoId = '" + verid + "' ");
        foreach (DataRow itm in dtr.Rows)
        {
            if (Temp3val.Length > 0)
            {
                Temp3val += ",";
            }

            Temp3val += "('" + Encrypted(Convert.ToString(itm["WebsiteSectionId"])) + "','" + Encrypted(Convert.ToString(itm["WebsiteMasterId"])) + "','" + Encrypted(Convert.ToString(itm["SectionName"])) + "','" + Encrypted(Convert.ToString(itm["AfterLoginDefaultPageId"])) + "','" + Encrypted(Convert.ToString(itm["LoginUrl"])) + "','" + Encrypted(Convert.ToString(itm["NormalUrl"])) + "')";


        }
        if (Temp3val.Length > 0)
        {
            Temp2 += Temp3val;
            SqlCommand ccm = new SqlCommand(Temp2, conn);
            conn.Open();
            ccm.ExecuteNonQuery();
            conn.Close();
        }
    }
    protected void Masterpage(string verid, string cid)
    {
        string Temp3val = "";
        string Temp2 = "INSERT INTO MasterPageMaster(MasterPageId,MasterPageName,MasterPageDescription,WebsiteSectionId)Values ";
        DataTable dtr = selectBZ("Select Distinct MasterPageMaster.*   from  WebsiteMaster inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.Id inner join MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId where WebsiteMaster.VersionInfoId = '" + verid + "' ");
        foreach (DataRow itm in dtr.Rows)
        {
            if (Temp3val.Length > 0)
            {
                Temp3val += ",";
            }

            Temp3val += "('" + Encrypted(Convert.ToString(itm["MasterPageId"])) + "','" + Encrypted(Convert.ToString(itm["MasterPageName"])) + "','" + Encrypted(Convert.ToString(itm["MasterPageDescription"])) + "','" + Encrypted(Convert.ToString(itm["WebsiteSectionId"])) + "')";


        }
        if (Temp3val.Length > 0)
        {
            Temp2 += Temp3val;
            SqlCommand ccm = new SqlCommand(Temp2, conn);
            conn.Open();
            ccm.ExecuteNonQuery();
            conn.Close();
        }
    }
    protected void MainManu(string verid, string cid)
    {
        string Temp3val = "";
        string Temp2 = "INSERT INTO MainMenuMaster(MainMenuId,MainMenuName,BackColour,MainMenuTitle,MasterPage_Id,MainMenuIndex,Active,LanguageId)Values ";
        DataTable dtr = selectBZ("Select Distinct MainMenuMaster.*   from  WebsiteMaster inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.Id inner join MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId inner join MainMenuMaster on MainMenuMaster.MasterPage_Id=MasterPageMaster.MasterPageId where WebsiteMaster.VersionInfoId = '" + verid + "' ");
        foreach (DataRow itm in dtr.Rows)
        {
            if (Temp3val.Length > 0)
            {
                Temp3val += ",";
            }

            Temp3val += "('" + Encrypted(Convert.ToString(itm["MainMenuId"])) + "','" + Encrypted(Convert.ToString(itm["MainMenuName"])) + "','" + Encrypted(Convert.ToString(itm["BackColour"])) + "','" + Encrypted(Convert.ToString(itm["MainMenuTitle"])) + "','" + Encrypted(Convert.ToString(itm["MasterPage_Id"])) + "','" + (itm["MainMenuIndex"]) + "','" + Convert.ToString(itm["Active"]) + "','" + Encrypted(Convert.ToString(itm["LanguageId"])) + "')";


        }
        if (Temp3val.Length > 0)
        {
            Temp2 += Temp3val;
            SqlCommand ccm = new SqlCommand(Temp2, conn);
            conn.Open();
            ccm.ExecuteNonQuery();
            conn.Close();
        }
    }
    protected void SubManu(string verid, string cid)
    {
        string Temp3val = "";
        string Temp2 = "INSERT INTO SubMenuMaster(SubMenuId,MainMenuId,SubMenuName,SubMenuIndex,Active,LanguageId)Values ";
        DataTable dtr = selectBZ("Select Distinct SubMenuMaster.*   from  WebsiteMaster inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.Id inner join MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId inner join MainMenuMaster on MainMenuMaster.MasterPage_Id=MasterPageMaster.MasterPageId inner join SubMenuMaster on SubMenuMaster.MainMenuId=MainMenuMaster.MainMenuId where WebsiteMaster.VersionInfoId = '" + verid + "' ");
        foreach (DataRow itm in dtr.Rows)
        {
            if (Temp3val.Length > 0)
            {
                Temp3val += ",";
            }

            Temp3val += "('" + Encrypted(Convert.ToString(itm["SubMenuId"])) + "','" + Encrypted(Convert.ToString(itm["MainMenuId"])) + "','" + Encrypted(Convert.ToString(itm["SubMenuName"])) + "','" + (Convert.ToString(itm["SubMenuIndex"])) + "','" + Convert.ToString(itm["Active"]) + "','" + Encrypted(Convert.ToString(itm["LanguageId"])) + "')";


        }
        if (Temp3val.Length > 0)
        {
            Temp2 += Temp3val;
            SqlCommand ccm = new SqlCommand(Temp2, conn);
            conn.Open();
            ccm.ExecuteNonQuery();
            conn.Close();
        }
    }
    protected void Pagename(string verid, string cid)
    {
        string Temp3val = "";
        string temp = "";
        string Temp2 = "INSERT INTO PageMaster(PageId,PageTypeId,PageName,PageTitle,PageDescription,PageIndex,VersionInfoMasterId,MainMenuId,FolderName,Active,SubMenuId,LanguageId,ManuAccess)Values ";
        DataTable dtr = selectBZ("Select Distinct PageMaster.*   from  WebsiteMaster inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.Id inner join MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId inner join MainMenuMaster on MainMenuMaster.MasterPage_Id=MasterPageMaster.MasterPageId inner join SubMenuMaster on SubMenuMaster.MainMenuId=MainMenuMaster.MainMenuId inner join PageMaster on PageMaster.MainMenuId=MainMenuMaster.MainMenuId where PageMaster.VersionInfoMasterId = '" + verid + "' ");
        temp = Temp3val;
        int jk = 0;
        foreach (DataRow itm in dtr.Rows)
        {

            if (Temp3val.Length > 0)
            {
                Temp3val += ",";
            }

            Temp3val += "('" + Encrypted(Convert.ToString(itm["PageId"])) + "','" + Encrypted(Convert.ToString(itm["PageTypeId"])) + "','" + Encrypted(Convert.ToString(itm["PageName"])) + "','" + Encrypted(Convert.ToString(itm["PageTitle"])) + "','" + Encrypted(Convert.ToString(itm["PageDescription"])) + "','" + Convert.ToString(itm["PageIndex"]) + "','" + Encrypted(Convert.ToString(itm["VersionInfoMasterId"])) + "','" + Encrypted(Convert.ToString(itm["MainMenuId"])) + "','" + Encrypted(Convert.ToString(itm["FolderName"])) + "','" + Convert.ToString(itm["Active"]) + "','" + Encrypted(Convert.ToString(itm["SubMenuId"])) + "','" + Encrypted(Convert.ToString(itm["LanguageId"])) + "','" + Convert.ToString(itm["ManuAccess"]) + "')";
            if (jk > 700)
            {

                temp = Temp2 + Temp3val;
                SqlCommand ccm = new SqlCommand(temp, conn);
                conn.Open();
                ccm.ExecuteNonQuery();
                conn.Close();
                Temp3val = "";
                temp = "";
                jk = 0;

            }
            else
            {
                jk += 1;
            }


        }
        if (Temp3val.Length > 0)
        {
            temp = Temp2 + Temp3val;
            SqlCommand ccm = new SqlCommand(temp, conn);
            conn.Open();
            ccm.ExecuteNonQuery();
            conn.Close();
        }
    }
    protected void Pageaccess(string verid, string cid)
    {

        string Temp3val = "";
        string temp = "";
        string Temp2 = "INSERT INTO pageplaneaccesstbl(Id,Pageid,Pagename,Priceplanid,pageaccess)Values ";
        DataTable dtr = selectBZ("Select Distinct pageplaneaccesstbl.*   from  WebsiteMaster inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.Id inner join MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId inner join MainMenuMaster on MainMenuMaster.MasterPage_Id=MasterPageMaster.MasterPageId inner join SubMenuMaster on SubMenuMaster.MainMenuId=MainMenuMaster.MainMenuId inner join PageMaster on PageMaster.MainMenuId=MainMenuMaster.MainMenuId inner join pageplaneaccesstbl on pageplaneaccesstbl.Pageid=PageMaster.PageId where  PageMaster.VersionInfoMasterId = '" + verid + "' ");
        temp = Temp3val;
        int jk = 0;
        foreach (DataRow itm in dtr.Rows)
        {

            if (Temp3val.Length > 0)
            {
                Temp3val += ",";
            }

            Temp3val += "('" + Encrypted(Convert.ToString(itm["Id"])) + "','" + Encrypted(Convert.ToString(itm["Pageid"])) + "','" + Encrypted(Convert.ToString(itm["Pagename"])) + "','" + Encrypted(Convert.ToString(itm["Priceplanid"])) + "','" + Convert.ToString(itm["pageaccess"]) + "')";
            if (jk > 700)
            {

                temp = Temp2 + Temp3val;
                SqlCommand ccm = new SqlCommand(temp, conn);
                conn.Open();
                ccm.ExecuteNonQuery();
                conn.Close();
                Temp3val = "";
                temp = "";
                jk = 0;

            }
            else
            {
                jk += 1;
            }


        }
        if (Temp3val.Length > 0)
        {
            temp = Temp2 + Temp3val;
            SqlCommand ccm = new SqlCommand(temp, conn);
            conn.Open();
            ccm.ExecuteNonQuery();
            conn.Close();
        }

    }

    protected void Controltype(string verid, string cid)
    {
        string Temp3val = "";
        string Temp2 = "INSERT INTO Control_type_Master(Type_id,Type_name)Values ";
        DataTable dtr = selectBZ("Select Distinct *   from  Control_type_Master ");
        foreach (DataRow itm in dtr.Rows)
        {
            if (Temp3val.Length > 0)
            {
                Temp3val += ",";
            }

            Temp3val += "('" + Encrypted(Convert.ToString(itm["Type_id"])) + "','" + Encrypted(Convert.ToString(itm["Type_name"])) + "')";


        }
        if (Temp3val.Length > 0)
        {
            Temp2 += Temp3val;
            SqlCommand ccm = new SqlCommand(Temp2, conn);
            conn.Open();
            ccm.ExecuteNonQuery();
            conn.Close();
        }
    }

    protected void PageControltype(string verid, string cid)
    {

        string Temp3val = "";
        string temp = "";
        string Temp2 = "INSERT INTO PageControlMaster(PageControl_id,Page_id,ControlTitle,ControlName,ControlType_id,ActiveDeactive,DefaultLabel)Values ";
        DataTable dtr = selectBZ("Select Distinct PageControlMaster.* from PageControlMaster inner join pageplaneaccesstbl on pageplaneaccesstbl.pageid=PageControlMaster.Page_id inner join PageMaster on PageMaster.PageId=PageControlMaster.Page_id where  PageMaster.VersionInfoMasterId = '" + verid + "'");
        temp = Temp3val;
        int jk = 0;
        foreach (DataRow itm in dtr.Rows)
        {

            if (Temp3val.Length > 0)
            {
                Temp3val += ",";
            }

            Temp3val += "('" + Encrypted(Convert.ToString(itm["PageControl_id"])) + "','" + Encrypted(Convert.ToString(itm["Page_id"])) + "','" + Encrypted(Convert.ToString(itm["ControlTitle"])) + "','" + Encrypted(Convert.ToString(itm["ControlName"])) + "','" + Encrypted(Convert.ToString(itm["ControlType_id"])) + "','" + Convert.ToString(itm["ActiveDeactive"]) + "','" + Encrypted(Convert.ToString(itm["DefaultLabel"])) + "')";
            if (jk > 700)
            {

                temp = Temp2 + Temp3val;
                SqlCommand ccm = new SqlCommand(temp, conn);
                conn.Open();
                ccm.ExecuteNonQuery();
                conn.Close();
                Temp3val = "";
                temp = "";
                jk = 0;

            }
            else
            {
                jk += 1;
            }


        }
        if (Temp3val.Length > 0)
        {
            temp = Temp2 + Temp3val;
            SqlCommand ccm = new SqlCommand(temp, conn);
            conn.Open();
            ccm.ExecuteNonQuery();
            conn.Close();
        }
    }
    protected void ClientProductTableMaster(string verid, string cid)
    {
        string Temp3val = "";
        string Temp2 = "INSERT INTO ClientProductTableMaster(Id,VersionInfoId,TableName,TableTitle)Values ";
        DataTable dtr = selectBZ("Select Distinct *   from  ClientProductTableMaster where VersionInfoId='" + verid + "'");
        foreach (DataRow itm in dtr.Rows)
        {
            if (Temp3val.Length > 0)
            {
                Temp3val += ",";
            }

            Temp3val += "('" + Encrypted(Convert.ToString(itm["Id"])) + "','" + Encrypted(Convert.ToString(itm["VersionInfoId"])) + "','" + Encrypted(Convert.ToString(itm["TableName"])) + "','" + Encrypted(Convert.ToString(itm["TableTitle"])) + "')";


        }
        if (Temp3val.Length > 0)
        {
            Temp2 += Temp3val;
            SqlCommand ccm = new SqlCommand(Temp2, conn);
            conn.Open();
            ccm.ExecuteNonQuery();
            conn.Close();
        }
    }

    protected void setupwizardquestion(string verid, string cid)
    {
        string Temp3val = "";
        string temp = "";
        string Temp2 = "INSERT INTO setupwizardquestion(id,SetupDetailID,questionname,answerhint,PageID,showanswerhint,Showanswertetbox,Showddwithhyperlink,YesHyperlinkdisplayPageID,screenshotURL,videoURL,powerpointURL,SetupQindex)Values ";
        DataTable dtr = selectBZ("Select Distinct setupwizardquestion.*   from setupwizardquestion inner join SetupwizardDetail on SetupwizardDetail.Id=setupwizardquestion.SetupDetailID inner join SetupWizardMaster on SetupWizardMaster.Id=SetupwizardDetail.SetupwizardMasterID   where SetupWizardMaster.ProductVersionId='" + verid + "'  ");
        temp = Temp3val;
        int jk = 0;
        foreach (DataRow itm in dtr.Rows)
        {
            if (Temp3val.Length > 0)
            {
                Temp3val += ",";
            }
            Temp3val += "('" + (Convert.ToString(itm["id"])) + "','" + (Convert.ToString(itm["SetupDetailID"])) + "','" + (Convert.ToString(itm["questionname"])) + "','" + (Convert.ToString(itm["answerhint"])) + "','" + (Convert.ToString(itm["PageID"])) + "','" + Convert.ToString(itm["showanswerhint"]) + "','" + (Convert.ToString(itm["Showanswertetbox"])) + "','" + (Convert.ToString(itm["Showddwithhyperlink"])) + "','" + (Convert.ToString(itm["YesHyperlinkdisplayPageID"])) + "','" + Convert.ToString(itm["screenshotURL"]) + "','" + (Convert.ToString(itm["videoURL"])) + "','" + (Convert.ToString(itm["powerpointURL"])) + "','" + (Convert.ToString(itm["SetupQindex"])) + "')";
            if (jk > 700)
            {

                temp = Temp2 + Temp3val;
                SqlCommand ccm = new SqlCommand(temp, conn);
                conn.Open();
                ccm.ExecuteNonQuery();
                conn.Close();
                Temp3val = "";
                temp = "";
                jk = 0;

            }
            else
            {
                jk += 1;
            }


        }
        if (Temp3val.Length > 0)
        {
            temp = Temp2 + Temp3val;
            SqlCommand ccm = new SqlCommand(temp, conn);
            conn.Open();
            ccm.ExecuteNonQuery();
            conn.Close();
        }
    }
    protected void setupwizardquestionPortal(string verid, string cid)
    {

        string Temp3val = "";
        string temp = "";


        string Temp2 = "INSERT INTO setupwizardquestionPortal(setupwizardquestionId,PortalId)Values ";
        //DataTable dtr = selectBZ("Select Distinct PageMaster.*   from  WebsiteMaster inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.Id inner join MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId inner join MainMenuMaster on MainMenuMaster.MasterPage_Id=MasterPageMaster.MasterPageId inner join SubMenuMaster on SubMenuMaster.MainMenuId=MainMenuMaster.MainMenuId inner join PageMaster on PageMaster.MainMenuId=MainMenuMaster.MainMenuId where PageMaster.VersionInfoMasterId = '" + verid + "' ");
        DataTable dtr = selectBZ("Select Distinct setupwizardquestionPortal.*   from SetupwizardMaster inner join  SetupwizardDetail on SetupwizardDetail.setupwizardmasterid=SetupwizardMaster.ID inner join setupwizardquestion on setupwizardquestion.SetupDetailID=SetupwizardDetail.Id  inner join setupwizardquestionPortal on setupwizardquestionPortal.setupwizardquestionId=setupwizardquestion.Id where ProductVersionId In( select ProductId from  VersionInfoMaster where VersionInfoId='" + verid + "')");
        temp = Temp3val;
        int jk = 0;
        foreach (DataRow itm in dtr.Rows)
        {

            if (Temp3val.Length > 0)
            {
                Temp3val += ",";
            }

            Temp3val += "('" + (Convert.ToString(itm["setupwizardquestionId"])) + "','" + (Convert.ToString(itm["PortalId"])) + "')";
            if (jk > 700)
            {

                temp = Temp2 + Temp3val;
                SqlCommand ccm = new SqlCommand(temp, conn);
                conn.Open();
                ccm.ExecuteNonQuery();
                conn.Close();
                Temp3val = "";
                temp = "";
                jk = 0;

            }
            else
            {
                jk += 1;
            }


        }
        if (Temp3val.Length > 0)
        {
            temp = Temp2 + Temp3val;
            SqlCommand ccm = new SqlCommand(temp, conn);
            conn.Open();
            ccm.ExecuteNonQuery();
            conn.Close();
        }
    }
    protected void SetupWizardMaster(string verid, string cid)
    {

        string Temp3val = "";
        string temp = "";


        string Temp2 = "INSERT INTO SetupWizardMaster(Id,Name,ProductVersionId,SetupMindex)Values ";
        //DataTable dtr = selectBZ("Select Distinct PageMaster.*   from  WebsiteMaster inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.Id inner join MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId inner join MainMenuMaster on MainMenuMaster.MasterPage_Id=MasterPageMaster.MasterPageId inner join SubMenuMaster on SubMenuMaster.MainMenuId=MainMenuMaster.MainMenuId inner join PageMaster on PageMaster.MainMenuId=MainMenuMaster.MainMenuId where PageMaster.VersionInfoMasterId = '" + verid + "' ");
        DataTable dtr = selectBZ("Select Distinct SetupWizardMaster.*   from SetupWizardMaster where ProductVersionId='" + verid + "'  ");
        temp = Temp3val;
        int jk = 0;
        foreach (DataRow itm in dtr.Rows)
        {

            if (Temp3val.Length > 0)
            {
                Temp3val += ",";
            }

            Temp3val += "('" + (Convert.ToString(itm["Id"])) + "','" + (Convert.ToString(itm["Name"])) + "','" + (Convert.ToString(itm["ProductVersionId"])) + "','" + (Convert.ToString(itm["SetupMindex"])) + "')";
            if (jk > 700)
            {

                temp = Temp2 + Temp3val;
                SqlCommand ccm = new SqlCommand(temp, conn);
                conn.Open();
                ccm.ExecuteNonQuery();
                conn.Close();
                Temp3val = "";
                temp = "";
                jk = 0;

            }
            else
            {
                jk += 1;
            }


        }
        if (Temp3val.Length > 0)
        {
            temp = Temp2 + Temp3val;
            SqlCommand ccm = new SqlCommand(temp, conn);
            conn.Open();
            ccm.ExecuteNonQuery();
            conn.Close();
        }
    }
    protected void SetupWizardMasterwithPortal(string verid, string cid)
    {

        string Temp3val = "";
        string temp = "";


        string Temp2 = "INSERT INTO SetupWizardMasterwithPortal(SetupWizardMasterId,PortalId)Values ";
        //DataTable dtr = selectBZ("Select Distinct PageMaster.*   from  WebsiteMaster inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.Id inner join MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId inner join MainMenuMaster on MainMenuMaster.MasterPage_Id=MasterPageMaster.MasterPageId inner join SubMenuMaster on SubMenuMaster.MainMenuId=MainMenuMaster.MainMenuId inner join PageMaster on PageMaster.MainMenuId=MainMenuMaster.MainMenuId where PageMaster.VersionInfoMasterId = '" + verid + "' ");
        DataTable dtr = selectBZ("Select Distinct SetupWizardMasterwithPortal.*   from SetupwizardMaster  inner join SetupWizardMasterwithPortal on SetupWizardMasterwithPortal.SetupWizardMasterId=SetupwizardMaster.Id  where ProductVersionId In( select ProductId from  VersionInfoMaster where VersionInfoId='" + verid + "')");
        temp = Temp3val;
        int jk = 0;
        foreach (DataRow itm in dtr.Rows)
        {

            if (Temp3val.Length > 0)
            {
                Temp3val += ",";
            }

            Temp3val += "('" + (Convert.ToString(itm["SetupWizardMasterId"])) + "','" + (Convert.ToString(itm["PortalId"])) + "')";
            if (jk > 700)
            {

                temp = Temp2 + Temp3val;
                SqlCommand ccm = new SqlCommand(temp, conn);
                conn.Open();
                ccm.ExecuteNonQuery();
                conn.Close();
                Temp3val = "";
                temp = "";
                jk = 0;

            }
            else
            {
                jk += 1;
            }


        }
        if (Temp3val.Length > 0)
        {
            temp = Temp2 + Temp3val;
            SqlCommand ccm = new SqlCommand(temp, conn);
            conn.Open();
            ccm.ExecuteNonQuery();
            conn.Close();
        }
    }
    protected void SetupwizardDetail(string verid, string cid)
    {

        string Temp3val = "";
        string temp = "";


        string Temp2 = "INSERT INTO SetupwizardDetail(Id,SetupwizardMasterID,SetupSubwizardTitle,SetupDMindex)Values ";
        DataTable dtr = selectBZ("Select Distinct SetupwizardDetail.*   from SetupwizardDetail inner join SetupWizardMaster on SetupWizardMaster.Id=SetupwizardDetail.SetupwizardMasterID   where SetupWizardMaster.ProductVersionId='" + verid + "'  ");
        temp = Temp3val;
        int jk = 0;
        foreach (DataRow itm in dtr.Rows)
        {

            if (Temp3val.Length > 0)
            {
                Temp3val += ",";
            }

            Temp3val += "('" + (Convert.ToString(itm["Id"])) + "','" + (Convert.ToString(itm["SetupwizardMasterID"])) + "','" + (Convert.ToString(itm["SetupSubwizardTitle"])) + "','" + (Convert.ToString(itm["SetupDMindex"])) + "')";
            if (jk > 700)
            {

                temp = Temp2 + Temp3val;
                SqlCommand ccm = new SqlCommand(temp, conn);
                conn.Open();
                ccm.ExecuteNonQuery();
                conn.Close();
                Temp3val = "";
                temp = "";
                jk = 0;

            }
            else
            {
                jk += 1;
            }


        }
        if (Temp3val.Length > 0)
        {
            temp = Temp2 + Temp3val;
            SqlCommand ccm = new SqlCommand(temp, conn);
            conn.Open();
            ccm.ExecuteNonQuery();
            conn.Close();
        }
    }
    protected void SetupWizardDetailwithPortal(string verid, string cid)
    {

        string Temp3val = "";
        string temp = "";


        string Temp2 = "INSERT INTO SetupWizardDetailwithPortal(SetupWizardDetailId,PortalId)Values ";
        //DataTable dtr = selectBZ("Select Distinct PageMaster.*   from  WebsiteMaster inner join WebsiteSection on WebsiteSection.WebsiteMasterId=WebsiteMaster.Id inner join MasterPageMaster on MasterPageMaster.WebsiteSectionId=WebsiteSection.WebsiteSectionId inner join MainMenuMaster on MainMenuMaster.MasterPage_Id=MasterPageMaster.MasterPageId inner join SubMenuMaster on SubMenuMaster.MainMenuId=MainMenuMaster.MainMenuId inner join PageMaster on PageMaster.MainMenuId=MainMenuMaster.MainMenuId where PageMaster.VersionInfoMasterId = '" + verid + "' ");
        DataTable dtr = selectBZ("Select Distinct SetupWizardDetailwithPortal.*   from SetupwizardMaster inner join  SetupwizardDetail on SetupwizardDetail.setupwizardmasterid=SetupwizardMaster.ID   inner join SetupWizardDetailwithPortal on SetupWizardDetailwithPortal.SetupWizardDetailId=SetupwizardDetail.Id where ProductVersionId In( select ProductId from  VersionInfoMaster where VersionInfoId='" + verid + "')");
        temp = Temp3val;
        int jk = 0;
        foreach (DataRow itm in dtr.Rows)
        {

            if (Temp3val.Length > 0)
            {
                Temp3val += ",";
            }

            Temp3val += "('" + (Convert.ToString(itm["SetupWizardDetailId"])) + "','" + (Convert.ToString(itm["PortalId"])) + "')";
            if (jk > 700)
            {

                temp = Temp2 + Temp3val;
                SqlCommand ccm = new SqlCommand(temp, conn);
                conn.Open();
                ccm.ExecuteNonQuery();
                conn.Close();
                Temp3val = "";
                temp = "";
                jk = 0;

            }
            else
            {
                jk += 1;
            }


        }
        if (Temp3val.Length > 0)
        {
            temp = Temp2 + Temp3val;
            SqlCommand ccm = new SqlCommand(temp, conn);
            conn.Open();
            ccm.ExecuteNonQuery();
            conn.Close();
        }
    }
    protected void PriceplanrestrictionTbl(string verid, string cid)
    {
        string Temp3val = "";
        string Temp2 = "INSERT INTO PriceplanrestrictionTbl(Id,ProductversionId,TableId,NameofRest,TextofQueinSelection,Restingroup,Priceaddingroup,PortalId,FieldrestrictionSet,RestrictfieldId)Values ";
        DataTable dtr = selectBZ("Select Distinct *   from  PriceplanrestrictionTbl where ProductversionId='" + verid + "'");
        foreach (DataRow itm in dtr.Rows)
        {
            if (Temp3val.Length > 0)
            {
                Temp3val += ",";
            }

            Temp3val += "('" + Encrypted(Convert.ToString(itm["Id"])) + "','" + Encrypted(Convert.ToString(itm["ProductversionId"])) + "','" + Encrypted(Convert.ToString(itm["TableId"])) + "','" + Encrypted(Convert.ToString(itm["NameofRest"])) + "'," +
            " '" + Encrypted(Convert.ToString(itm["TextofQueinSelection"])) + "','" + Encrypted(Convert.ToString(itm["Restingroup"])) + "','" + Encrypted(Convert.ToString(itm["Priceaddingroup"])) + "','" + Encrypted(Convert.ToString(itm["PortalId"])) + "'," +
            " '" + Encrypted(Convert.ToString(itm["FieldrestrictionSet"])) + "','" + Encrypted(Convert.ToString(itm["RestrictfieldId"])) + "' )";


        }
        if (Temp3val.Length > 0)
        {
            Temp2 += Temp3val;
            SqlCommand ccm = new SqlCommand(Temp2, conn);
            conn.Open();
            ccm.ExecuteNonQuery();
            conn.Close();
        }
    }


  
    protected void Priceplanrestrecordallowtbl(string verid, string cid)
    {

        string Temp3val = "";
        string temp = "";

        string Temp2 = "INSERT INTO Priceplanrestrecordallowtbl(Id,PriceplanRestrictiontblId,PricePlanId,RecordsAllowed)Values ";
        DataTable dtr = selectBZ("Select Distinct Priceplanrestrecordallowtbl.*   from Priceplanrestrecordallowtbl inner join  PriceplanrestrictionTbl on PriceplanrestrictionTbl.Id=Priceplanrestrecordallowtbl.PriceplanRestrictiontblId where PriceplanrestrictionTbl.ProductversionId='" + verid + "'");
        temp = Temp3val;
        int jk = 0;
        foreach (DataRow itm in dtr.Rows)
        {

            if (Temp3val.Length > 0)
            {
                Temp3val += ",";
            }

            Temp3val += "('" + Encrypted(Convert.ToString(itm["Id"])) + "','" + Encrypted(Convert.ToString(itm["PriceplanRestrictiontblId"])) + "','" + Encrypted(Convert.ToString(itm["PricePlanId"])) + "','" + Encrypted(Convert.ToString(itm["RecordsAllowed"])) + "')";
            if (jk > 700)
            {

                temp = Temp2 + Temp3val;
                SqlCommand ccm = new SqlCommand(temp, conn);
                conn.Open();
                ccm.ExecuteNonQuery();
                conn.Close();
                Temp3val = "";
                temp = "";
                jk = 0;

            }
            else
            {
                jk += 1;
            }


        }
        if (Temp3val.Length > 0)
        {
            temp = Temp2 + Temp3val;
            SqlCommand ccm = new SqlCommand(temp, conn);
            conn.Open();
            ccm.ExecuteNonQuery();
            conn.Close();
        }
    }
    protected void tablefielddetail(string verid, string cid)
    {

        string Temp3val = "";
        string temp = "";

        string Temp2 = "INSERT INTO tablefielddetail(Id,tableId,feildname,fieldtype,size,Isforeignkey,foreignkeytblid,foreignkeyfieldId)Values ";
        DataTable dtr = selectBZ("Select Distinct tablefielddetail.*   from tablefielddetail inner join  ClientProductTableMaster on ClientProductTableMaster.Id=tablefielddetail.tableId where ClientProductTableMaster.VersionInfoId='" + verid + "'");
        temp = Temp3val;
        int jk = 0;
        foreach (DataRow itm in dtr.Rows)
        {

            if (Temp3val.Length > 0)
            {
                Temp3val += ",";
            }

            Temp3val += "('" + Encrypted(Convert.ToString(itm["Id"])) + "','" + Encrypted(Convert.ToString(itm["tableId"])) + "','" + Encrypted(Convert.ToString(itm["feildname"])) + "','" + Encrypted(Convert.ToString(itm["fieldtype"])) + "'," +
            "'" + Encrypted(Convert.ToString(itm["size"])) + "','" + (Convert.ToString(itm["Isforeignkey"])) + "','" + Encrypted(Convert.ToString(itm["foreignkeytblid"])) + "','" + Encrypted(Convert.ToString(itm["foreignkeyfieldId"])) + "')";
            if (jk > 700)
            {

                temp = Temp2 + Temp3val;
                SqlCommand ccm = new SqlCommand(temp, conn);
                conn.Open();
                ccm.ExecuteNonQuery();
                conn.Close();
                Temp3val = "";
                temp = "";
                jk = 0;

            }
            else
            {
                jk += 1;
            }


        }
        if (Temp3val.Length > 0)
        {
            temp = Temp2 + Temp3val;
            SqlCommand ccm = new SqlCommand(temp, conn);
            conn.Open();
            ccm.ExecuteNonQuery();
            conn.Close();
        }
    }

    protected void SatelliteSyncronisationrequiringTablesMaster(string verid)
    {
        SqlCommand cmdrX = new SqlCommand("Delete  from  SatelliteSyncronisationrequiringTablesMaster", conn);
        conn.Open();
        cmdrX.ExecuteNonQuery();
        conn.Close();
        string Temp3val = "";
        string temp = "";

        string Temp2 = "INSERT INTO SatelliteSyncronisationrequiringTablesMaster(Id,ProductVersionID,TableID,Name,Status,TableName)Values ";
        DataTable dtr = selectBZ("Select Distinct *   from  SatelliteSyncronisationrequiringTablesMaster where ProductVersionID='" + verid + "'");
        temp = Temp3val;
        int jk = 0;
        foreach (DataRow itm in dtr.Rows)
        {

            if (Temp3val.Length > 0)
            {
                Temp3val += ",";
            }

            Temp3val += "('" + Convert.ToString(itm["Id"]) + "','" + Convert.ToString(itm["ProductVersionID"]) + "','" + Convert.ToString(itm["TableID"]) + "'," +
               " '" + Convert.ToString(itm["Name"]) + "','" + Convert.ToString(itm["Status"]) + "','" + Convert.ToString(itm["TableName"]) + "')";
            if (jk > 700)
            {

                temp = Temp2 + Temp3val;
                SqlCommand ccm = new SqlCommand(temp, conn);
                conn.Open();
                ccm.ExecuteNonQuery();
                conn.Close();
                Temp3val = "";
                temp = "";
                jk = 0;

            }
            else
            {
                jk += 1;
            }


        }
        if (Temp3val.Length > 0)
        {
            temp = Temp2 + Temp3val;
            SqlCommand ccm = new SqlCommand(temp, conn);
            conn.Open();
            ccm.ExecuteNonQuery();
            conn.Close();
        }
    }
    protected void SyncronisationrequiredTbl(string verid)
    {
        SqlCommand cmdrX = new SqlCommand("Delete  from  SyncronisationrequiredTbl", conn);
        conn.Open();
        cmdrX.ExecuteNonQuery();
        conn.Close();
        string Temp3val = "";
        string temp = "";

        string Temp2 = "INSERT INTO SyncronisationrequiredTbl(ID,SatelliteSyncronisationrequiringTablesMasterID,DateandTime)Values ";
        DataTable dtr = selectBZ("Select Distinct SyncronisationrequiredTbl.*   from  SyncronisationrequiredTbl inner join SatelliteSyncronisationrequiringTablesMaster on SatelliteSyncronisationrequiringTablesMaster.Id=SyncronisationrequiredTbl.SatelliteSyncronisationrequiringTablesMasterID where ProductVersionID='" + verid + "'");
        temp = Temp3val;
        int jk = 0;
        foreach (DataRow itm in dtr.Rows)
        {

            if (Temp3val.Length > 0)
            {
                Temp3val += ",";
            }

            Temp3val += "('" + Convert.ToString(itm["ID"]) + "','" + Convert.ToString(itm["SatelliteSyncronisationrequiringTablesMasterID"]) + "','" + Convert.ToString(itm["DateandTime"]) + "')";
            if (jk > 700)
            {

                temp = Temp2 + Temp3val;
                SqlCommand ccm = new SqlCommand(temp, conn);
                conn.Open();
                ccm.ExecuteNonQuery();
                conn.Close();
                Temp3val = "";
                temp = "";
                jk = 0;

            }
            else
            {
                jk += 1;
            }


        }
        if (Temp3val.Length > 0)
        {
            temp = Temp2 + Temp3val;
            SqlCommand ccm = new SqlCommand(temp, conn);
            conn.Open();
            ccm.ExecuteNonQuery();
            conn.Close();
        }
    }

    protected void PortaldesignationTbl(string verid, string cid)
    {

        string Temp3val = "";
        string temp = "";


        string Temp2 = "INSERT INTO PortaldesignationTbl(Id,ProductId,PortalId,Portalname,DefaultPageName)Values ";
        DataTable dtr = selectBZ("Select Distinct PortaldesignationTbl.*   from PortaldesignationTbl where ProductId In( Select Distinct ProductId from VersionInfoMaster where VersionInfoId='" + verid + "')");
        temp = Temp3val;
        int jk = 0;
        foreach (DataRow itm in dtr.Rows)
        {

            if (Temp3val.Length > 0)
            {
                Temp3val += ",";
            }

            Temp3val += "('" + (Convert.ToString(itm["Id"])) + "','" + (Convert.ToString(itm["ProductId"])) + "','" + (Convert.ToString(itm["PortalId"])) + "','" + (Convert.ToString(itm["Portalname"])) + "','" + (Convert.ToString(itm["DefaultPageName"])) + "')";
            if (jk > 700)
            {

                temp = Temp2 + Temp3val;
                SqlCommand ccm = new SqlCommand(temp, conn);
                conn.Open();
                ccm.ExecuteNonQuery();
                conn.Close();
                Temp3val = "";
                temp = "";
                jk = 0;

            }
            else
            {
                jk += 1;
            }


        }
        if (Temp3val.Length > 0)
        {
            temp = Temp2 + Temp3val;
            SqlCommand ccm = new SqlCommand(temp, conn);
            conn.Open();
            ccm.ExecuteNonQuery();
            conn.Close();
        }
    }
    protected void PortaldesignationDetailTbl(string verid, string cid)
    {

        string Temp3val = "";
        string temp = "";


        string Temp2 = "INSERT INTO PortaldesignationDetailTbl(PortaldesignationTblId,DesignationName,PageName)Values ";
        DataTable dtr = selectBZ("Select Distinct PortaldesignationDetailTbl.*   from PortaldesignationDetailTbl inner join PortaldesignationTbl on PortaldesignationTbl.Id=PortaldesignationDetailTbl.PortaldesignationTblId where PortaldesignationTbl.ProductId In( Select Distinct ProductId from VersionInfoMaster where VersionInfoId='" + verid + "')");
        temp = Temp3val;
        int jk = 0;
        foreach (DataRow itm in dtr.Rows)
        {

            if (Temp3val.Length > 0)
            {
                Temp3val += ",";
            }

            Temp3val += "('" + (Convert.ToString(itm["PortaldesignationTblId"])) + "','" + (Convert.ToString(itm["DesignationName"])) + "','" + (Convert.ToString(itm["PageName"])) + "')";
            if (jk > 700)
            {

                temp = Temp2 + Temp3val;
                SqlCommand ccm = new SqlCommand(temp, conn);
                conn.Open();
                ccm.ExecuteNonQuery();
                conn.Close();
                Temp3val = "";
                temp = "";
                jk = 0;

            }
            else
            {
                jk += 1;
            }


        }
        if (Temp3val.Length > 0)
        {
            temp = Temp2 + Temp3val;
            SqlCommand ccm = new SqlCommand(temp, conn);
            conn.Open();
            ccm.ExecuteNonQuery();
            conn.Close();
        }
    }
    private static string Encrypt(string strtxt, string strtoencrypt)
    {
        byte[] bykey = new byte[20];
        byte[] dv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        try
        {
            bykey = System.Text.Encoding.UTF8.GetBytes(strtoencrypt.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputArray = System.Text.Encoding.UTF8.GetBytes(strtxt);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(bykey, dv), CryptoStreamMode.Write);
            cs.Write(inputArray, 0, inputArray.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());
        }
        catch (Exception ex)
        {
            return strtxt;
            //  throw ex;
        }

    }
    public static string Encrypted(string strText)
    {

        return Encrypt(strText, encstr);

    }

    private static string Decrypt(string strText, string strEncrypt)
    {
        byte[] bKey = new byte[20];
        byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        try
        {
            bKey = System.Text.Encoding.UTF8.GetBytes(strEncrypt.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            Byte[] inputByteArray = inputByteArray = Convert.FromBase64String(strText);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(bKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            System.Text.Encoding encoding = System.Text.Encoding.UTF8;
            return encoding.GetString(ms.ToArray());
        }
        catch (Exception ex)
        {
            return strText;
            //throw ex;
        }
    }

    public static string Decrypted(string str)
    {

        return Decrypt(str, encstr);

    }

    protected DataTable select(string str)
    {
        SqlCommand cmdclnccdweb = new SqlCommand(str, conn);
        DataTable dtclnccdweb = new DataTable();
        SqlDataAdapter adpclnccdweb = new SqlDataAdapter(cmdclnccdweb);
        adpclnccdweb.Fill(dtclnccdweb);
        return dtclnccdweb;
    }
    protected DataTable selectBZ(string str)
    {
        SqlCommand cmdclnccdweb = new SqlCommand(str, con);
        DataTable dtclnccdweb = new DataTable();
        SqlDataAdapter adpclnccdweb = new SqlDataAdapter(cmdclnccdweb);
        adpclnccdweb.Fill(dtclnccdweb);
        return dtclnccdweb;
    }

    protected void ddlpriceplan_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillpriceplancate();
        ddlpriceplancatagory_SelectedIndexChanged(sender, e);
    }
    protected void fillpriceplancate()
    {
        ddlpriceplancatagory.Items.Clear();
        string strcln = " SELECT distinct * FROM Priceplancategory where PortalId='" + ddlportal.SelectedValue + "' order  by CategoryName";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        ddlpriceplancatagory.DataSource = dtcln;
        ddlpriceplancatagory.DataTextField = "CategoryName";
        ddlpriceplancatagory.DataValueField = "Id";
        ddlpriceplancatagory.DataBind();
        ddlpriceplancatagory.Items.Insert(0, "All");
        ddlpriceplancatagory.Items[0].Value = "0";
    }

    protected void fillcategoryfillter()
    {
        ddlcategoryfilter.Items.Clear();
        if (ddlportalfilter.SelectedIndex > 0)
        {
            string strcln = " SELECT distinct * FROM Priceplancategory where PortalId='" + ddlportalfilter.SelectedValue + "' order  by CategoryName";
            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);
            ddlcategoryfilter.DataSource = dtcln;
            ddlcategoryfilter.DataTextField = "CategoryName";
            ddlcategoryfilter.DataValueField = "Id";
            ddlcategoryfilter.DataBind();
        }
        ddlcategoryfilter.Items.Insert(0, "All");
        ddlcategoryfilter.Items[0].Value = "0";
    }

    protected void Encryptkey()
    {

    }
    public string CreateLicenceKey(out string HashKey)
    {
        string str = "";
        string s1 = "";
        string s2 = "";
        string s3 = "";
        string s4 = "";
        s1 = DateTime.Now.ToString().GetHashCode().ToString("x").ToString();
        if (s1.Length > 5)
        {
            s1 = s1.Substring(0, 5); //
        }
        else
        {
            s1 = s1 + "1";
        }
        s2 = DateTime.Now.Ticks.ToString("x").ToString();  //DateTime.Now.ToString().GetHashCode().ToString("x");
        if (s2.Length > 9)
        {
            s2 = s2.Substring(4, 5); //
        }
        s3 = Guid.NewGuid().ToString().GetHashCode().ToString("x").ToString(); //DateTime.Now.ToString().GetHashCode().ToString("x");
        if (s3.Length > 5)
        {
            s3 = s3.Substring(0, 5); //
        }
        s4 = RNGCharacterMask().ToString().Substring(0, 5); // DateTime.Now.ToString().GetHashCode().ToString("x");
        if (s4.Length > 5)
        {
            s4 = s4.Substring(0, 5); //
        }
        string hashcode = "";
        string s11 = "";
        string s22 = "";
        string s33 = "";
        string s44 = "";
        string s55 = "";
        s11 = DateTime.Now.ToString().GetHashCode().ToString("x").ToString();
        s22 = DateTime.Now.Ticks.ToString("x").ToString();  //DateTime.Now.ToString().GetHashCode().ToString("x");
        s33 = Guid.NewGuid().ToString().GetHashCode().ToString("x").ToString(); //DateTime.Now.ToString().GetHashCode().ToString("x");
        s44 = RNGCharacterMask().ToString(); // DateTime.Now.ToString().GetHashCode().ToString("x
        s55 = RNGCharacterMask().ToString(); // DateTime.Now.ToString().GetHashCode().ToString("x
        s11 = s11.Substring(s11.Length - 1, 1);
        s22 = s22.Substring(s22.Length - 1, 1);
        s33 = s33.Substring(s33.Length - 1, 1);
        s44 = s44.Substring(s44.Length - 1, 1);
        s55 = s55.Substring(s55.Length - 2, 1);
        hashcode = s11 + s22 + s33 + s44 + s55;
        str = s3.ToString() + "" + s2.ToString() + "" + s1.ToString() + "" + s4.ToString();
        HashKey = hashcode.ToUpper();
        return str;
    }
    private string RNGCharacterMask()
    {
        int maxSize = 12;
        int minSize = 10;
        char[] chars = new char[62];
        string a;
        a = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        chars = a.ToCharArray();
        int size = maxSize;
        byte[] data = new byte[1];
        RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
        crypto.GetNonZeroBytes(data);
        size = maxSize;
        data = new byte[size];
        crypto.GetNonZeroBytes(data);
        StringBuilder result = new StringBuilder(size);
        foreach (byte b in data)
        { result.Append(chars[b % (chars.Length - 1)]); }
        return result.ToString();
    }

    protected void ddlpriceplancatagory_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlserver.Items.Clear();
        DataTable dtpcate = selectBZ(" SELECT Distinct ServerMasterTbl.Id,ServerMasterTbl.ServerName FROM ServerMasterTbl inner join ServerAssignmentMasterTbl on ServerAssignmentMasterTbl.ServerId=ServerMasterTbl.Id inner join  PricePlanMaster on PricePlanMaster.PricePlanId=ServerAssignmentMasterTbl.PricePlanId inner join Priceplancategory on Priceplancategory.Id=PricePlanMaster.PriceplancatId   where ServerMasterTbl.Status='1' and ServerAssignmentMasterTbl.Active='1' and PricePlanMaster.active='1' and  PricePlanMaster.VersionInfoMasterId='" + ddlProductname.SelectedValue + "' and Priceplancategory.PortalId='" + ddlportal.SelectedValue + "' and Priceplancategory.Id='" + ddlpriceplancatagory.SelectedValue + "'");
        ddlserver.DataSource = dtpcate;
        ddlserver.DataTextField = "ServerName";
        ddlserver.DataValueField = "Id";
        ddlserver.DataBind();
        ddlserver.Items.Insert(0, "All");
        ddlserver.Items[0].Value = "0";
        grdserver.DataSource = null;
        grdserver.DataBind();
        pnltransst.Visible = false;
    }
    protected void serverfilter()
    {
        ddlserverfilter.Items.Clear();
        if (ddlportalfilter.SelectedIndex > 0)
        {
            DataTable dtpcate = selectBZ(" SELECT Distinct ServerMasterTbl.Id,ServerMasterTbl.ServerName FROM ServerMasterTbl inner join ServerAssignmentMasterTbl on ServerAssignmentMasterTbl.ServerId=ServerMasterTbl.Id inner join  PricePlanMaster on PricePlanMaster.PricePlanId=ServerAssignmentMasterTbl.PricePlanId inner join Priceplancategory on Priceplancategory.Id=PricePlanMaster.PriceplancatId   where ServerMasterTbl.Status='1' and ServerAssignmentMasterTbl.Active='1' and PricePlanMaster.active='1' and  PricePlanMaster.VersionInfoMasterId='" + ddlproductfilter.SelectedValue + "' and Priceplancategory.PortalId='" + ddlportalfilter.SelectedValue + "' and Priceplancategory.Id='" + ddlcategoryfilter.SelectedValue + "'");
            ddlserverfilter.DataSource = dtpcate;
            ddlserverfilter.DataTextField = "ServerName";
            ddlserverfilter.DataValueField = "Id";
            ddlserverfilter.DataBind();
        }
        ddlserverfilter.Items.Insert(0, "All");
        ddlserverfilter.Items[0].Value = "0";
       
    }
    public DataTable CreateData()
    {
        DataTable dtTemp = new DataTable();
        DataColumn prd1 = new DataColumn();
        prd1.ColumnName = "Id";
        prd1.DataType = System.Type.GetType("System.String");
        prd1.AllowDBNull = true;
        dtTemp.Columns.Add(prd1);

        DataColumn prd11 = new DataColumn();
        prd11.ColumnName = "ServerName";
        prd11.DataType = System.Type.GetType("System.String");
        prd11.AllowDBNull = true;
        dtTemp.Columns.Add(prd11);

        DataColumn prd111 = new DataColumn();
        prd111.ColumnName = "serverloction";
        prd111.DataType = System.Type.GetType("System.String");
        prd111.AllowDBNull = true;
        dtTemp.Columns.Add(prd111);

        DataColumn prd111c = new DataColumn();
        prd111c.ColumnName = "syncreq";
        prd111c.DataType = System.Type.GetType("System.String");
        prd111c.AllowDBNull = true;
        dtTemp.Columns.Add(prd111c);


        DataColumn prd111vx = new DataColumn();
        prd111vx.ColumnName = "servermasterID";
        prd111vx.DataType = System.Type.GetType("System.String");
        prd111vx.AllowDBNull = true;
        dtTemp.Columns.Add(prd111vx);
        DataColumn prd111v = new DataColumn();

        DataColumn prd1111v = new DataColumn();
        prd1111v.ColumnName = "DateandTime";
        prd1111v.DataType = System.Type.GetType("System.String");
        prd1111v.AllowDBNull = true;
        dtTemp.Columns.Add(prd1111v);
        DataColumn prd111vv = new DataColumn();
        prd111vv.ColumnName = "TableName";
        prd111vv.DataType = System.Type.GetType("System.String");
        prd111vv.AllowDBNull = true;
        dtTemp.Columns.Add(prd111vv);

        DataColumn ptd = new DataColumn();
        ptd.ColumnName = "PortalId";
        ptd.DataType = System.Type.GetType("System.String");
        ptd.AllowDBNull = true;
        dtTemp.Columns.Add(ptd);

        DataColumn ptdv = new DataColumn();
        ptdv.ColumnName = "tabdesname";
        ptdv.DataType = System.Type.GetType("System.String");
        ptdv.AllowDBNull = true;
        dtTemp.Columns.Add(ptdv);

        DataColumn ptdvv = new DataColumn();
        ptdvv.ColumnName = "Attempt";
        ptdvv.DataType = System.Type.GetType("System.String");
        ptdvv.AllowDBNull = true;
        dtTemp.Columns.Add(ptdvv);
        DataColumn ptdvv1 = new DataColumn();
        ptdvv1.ColumnName = "Msg";
        ptdvv1.DataType = System.Type.GetType("System.String");
        ptdvv1.AllowDBNull = true;
        dtTemp.Columns.Add(ptdvv1);
        return dtTemp;
    }

    public DataTable CreateDataGD()
    {
        DataTable dtTemp = new DataTable();
        DataColumn prd1 = new DataColumn();
        prd1.ColumnName = "Id";
        prd1.DataType = System.Type.GetType("System.String");
        prd1.AllowDBNull = true;
        dtTemp.Columns.Add(prd1);

        DataColumn prd11 = new DataColumn();
        prd11.ColumnName = "ServerName";
        prd11.DataType = System.Type.GetType("System.String");
        prd11.AllowDBNull = true;
        dtTemp.Columns.Add(prd11);

        DataColumn prd111 = new DataColumn();
        prd111.ColumnName = "serverloction";
        prd111.DataType = System.Type.GetType("System.String");
        prd111.AllowDBNull = true;
        dtTemp.Columns.Add(prd111);

        DataColumn prd111c = new DataColumn();
        prd111c.ColumnName = "SynchronisationSuccessful";
        prd111c.DataType = System.Type.GetType("System.String");
        prd111c.AllowDBNull = true;
        dtTemp.Columns.Add(prd111c);


        DataColumn prd111vx = new DataColumn();
        prd111vx.ColumnName = "servermasterID";
        prd111vx.DataType = System.Type.GetType("System.String");
        prd111vx.AllowDBNull = true;
        dtTemp.Columns.Add(prd111vx);
        DataColumn prd111v = new DataColumn();

        DataColumn prd1111v = new DataColumn();
        prd1111v.ColumnName = "DateandTime";
        prd1111v.DataType = System.Type.GetType("System.String");
        prd1111v.AllowDBNull = true;
        dtTemp.Columns.Add(prd1111v);
        DataColumn prd111vv = new DataColumn();
        prd111vv.ColumnName = "PortalName";
        prd111vv.DataType = System.Type.GetType("System.String");
        prd111vv.AllowDBNull = true;
        dtTemp.Columns.Add(prd111vv);



        DataColumn ptd = new DataColumn();
        ptd.ColumnName = "CategoryName";
        ptd.DataType = System.Type.GetType("System.String");
        ptd.AllowDBNull = true;
        dtTemp.Columns.Add(ptd);

        DataColumn ptdv = new DataColumn();
        ptdv.ColumnName = "tabdesname";
        ptdv.DataType = System.Type.GetType("System.String");
        ptdv.AllowDBNull = true;
        dtTemp.Columns.Add(ptdv);

        DataColumn ptdvz = new DataColumn();
        ptdvz.ColumnName = "SynchronisationAttemptErromMessage";
        ptdvz.DataType = System.Type.GetType("System.String");
        ptdvz.AllowDBNull = true;
        dtTemp.Columns.Add(ptdvz);

        DataColumn ptdvzx = new DataColumn();
        ptdvzx.ColumnName = "SynchronisationAttemptDatetime";
        ptdvzx.DataType = System.Type.GetType("System.String");
        ptdvzx.AllowDBNull = true;
        dtTemp.Columns.Add(ptdvzx);

        //DataColumn ptdvzxn = new DataColumn();
        //ptdvzxn.ColumnName = "CategoryName";
        //ptdvzxn.DataType = System.Type.GetType("System.String");
        //ptdvzxn.AllowDBNull = true;
        //dtTemp.Columns.Add(ptdvzxn);
        return dtTemp;
    }
    protected void btnsync_Click(object sender, EventArgs e)
    {
        SeprateDatabase();

    }
    protected void grdhistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    { //grdhistory.DataKeys[grdhistory.Rows.Count-1].Value.ToString();
        string strid = "";
        grdhistory.PageIndex = e.NewPageIndex;
        //strid = " and SateliteServerRequiringSynchronisationDetailTbl.Id<'" + strid+"'";
        fillgrid(strid);
       
    }
    protected void grdhistory_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        DataTable dtTemp = (DataTable)ViewState["Data"];
        grdhistory.DataSource = dtTemp;
        DataView myDataView = new DataView();
        myDataView = dtTemp.DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }


        grdhistory.DataBind();
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

    protected void grdserver_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdserver.PageIndex = e.NewPageIndex;
        DataTable dtTemp = (DataTable)ViewState["Datac"];
        grdserver.DataSource = dtTemp;
        DataView myDataView = new DataView();
        myDataView = dtTemp.DefaultView;

        if (hdnsortExp1.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp1.Value, hdnsortDir1.Value);
        }


        grdserver.DataBind();
    }
    protected void btnfilgo_Click(object sender, EventArgs e)
    {
        fillgrid("");
    }
    protected void ddlproductfilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillportalfilter();
        fillSynctable();
        ddlportalfilter_SelectedIndexChanged(sender, e);
    }
    protected void ddlportalfilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillcategoryfillter();
        ddlcategoryfilter_SelectedIndexChanged(sender, e);
    }
    protected void ddlcategoryfilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        serverfilter();
    }
    protected void grdserver_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp1.Value = e.SortExpression.ToString();
        hdnsortDir1.Value = sortOrder;
        DataTable dtTemp = (DataTable)ViewState["Datac"];
        grdserver.DataSource = dtTemp;
        DataView myDataView = new DataView();
        myDataView = dtTemp.DefaultView;

        if (hdnsortExp1.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp1.Value, hdnsortDir1.Value);
        }


        grdserver.DataBind();
    }
    protected void ddlsyncreqtbl_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid("");
    }
    protected void chkall_CheckedChanged(object sender, EventArgs e)
    {
        pnlsync.Visible = false;
        if (chkall.Checked == true)
        {
            pnlsync.Visible = true;
        }
    }
}
