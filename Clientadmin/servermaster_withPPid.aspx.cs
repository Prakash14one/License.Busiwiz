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
using System.Management;
using System.IO;
using Microsoft.SqlServer.Server;
using System.Net;
using System.Security.Cryptography;
using System.Text;




public partial class Master_Default : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    public static string encstr = "";
    //Boolean islease = false;
    //Boolean isshared = false;
    //Boolean issale = false;
    protected void Page_Load(object sender, EventArgs e)
    {     

        if (!IsPostBack)
        {

            Session["GridFileAttach1"] = null;
            txtUserId.Text = "BusiwizAdmin";
            string windowpassword = BusiwizCreateRandomPassword(16);
            txtwindowpassword.Attributes.Add("Value", windowpassword.ToString());

            txtsimplebusiwizuser.Text = "BusiwizUser";
            string simpleuserpassword = OtherCreateRandomPassword(16);
            txtsimpleuserpassword.Attributes.Add("Value", simpleuserpassword.ToString());

            fillportal1Lease();
            fillportal2Shared();
            fillportal3sell();
            FillPriceplanName1Lease();
            FillPriceplanName2shared();
            FillPriceplanName3sell();
           
            Fillddlcountry();
            carrirfill();
            fillsoftwaremaster();
            fillsoftwaremasterlicensekeys();

            //---
            FillProductMasterindividual();
            //--
            fillgrid1();

        }
    }


    protected void fillportal1Lease()
    {
        string activestr = "";
        ddlportalLease.Items.Clear();     
        string strcln22v = " Select * from PortalMasterTbl where ProductId In( Select distinct ProductMaster.ProductId from  ProductMaster  inner join VersionInfoMaster on VersionInfoMaster.productId=ProductMaster.ProductId )  and PortalMasterTbl.Status=1  and PortalMasterTbl.IsHostingServer=1 order by PortalName ";
        SqlCommand cmdcln22v = new SqlCommand(strcln22v, con);
        DataTable dtcln22v = new DataTable();
        SqlDataAdapter adpcln22v = new SqlDataAdapter(cmdcln22v);
        adpcln22v.Fill(dtcln22v);

        ddlportalLease.DataSource = dtcln22v;
        ddlportalLease.DataValueField = "Id";
        ddlportalLease.DataTextField = "PortalName";
        ddlportalLease.DataBind();
        ddlportalLease.Items.Insert(0, "-Select-");
        ddlportalLease.Items[0].Value = "0";
    
    }
    protected void fillportal2Shared()
    {
        string activestr = "";
        ddlportalShared.Items.Clear();      
        string strcln22v = " Select * from PortalMasterTbl where ProductId In( Select distinct ProductMaster.ProductId from  ProductMaster  inner join VersionInfoMaster on VersionInfoMaster.productId=ProductMaster.ProductId )  and PortalMasterTbl.Status=1  and PortalMasterTbl.IsHostingServer=1 order by PortalName ";
        SqlCommand cmdcln22v = new SqlCommand(strcln22v, con);
        DataTable dtcln22v = new DataTable();
        SqlDataAdapter adpcln22v = new SqlDataAdapter(cmdcln22v);
        adpcln22v.Fill(dtcln22v);
        ddlportalShared.DataSource = dtcln22v;
        ddlportalShared.DataValueField = "Id";
        ddlportalShared.DataTextField = "PortalName";
        ddlportalShared.DataBind();
        ddlportalShared.Items.Insert(0, "-Select-");
        ddlportalShared.Items[0].Value = "0";   
      

    }
    protected void fillportal3sell()
    {
        string activestr = "";      
        ddlportalanSell.Items.Clear();
        string strcln22v = " Select * from PortalMasterTbl where ProductId In( Select distinct ProductMaster.ProductId from  ProductMaster  inner join VersionInfoMaster on VersionInfoMaster.productId=ProductMaster.ProductId )  and PortalMasterTbl.Status=1  and PortalMasterTbl.IsHostingServer=1 order by PortalName ";
        SqlCommand cmdcln22v = new SqlCommand(strcln22v, con);
        DataTable dtcln22v = new DataTable();
        SqlDataAdapter adpcln22v = new SqlDataAdapter(cmdcln22v);
        adpcln22v.Fill(dtcln22v);  

        ddlportalanSell.DataSource = dtcln22v;
        ddlportalanSell.DataValueField = "Id";
        ddlportalanSell.DataTextField = "PortalName";
        ddlportalanSell.DataBind();
        ddlportalanSell.Items.Insert(0, "-Select-");
        ddlportalanSell.Items[0].Value = "0";

    }
    protected void ddlportalShared_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillPriceplanName2shared();
    }
    protected void ddlportalLease_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillPriceplanName1Lease();
    }
    protected void ddlportalSell_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillPriceplanName3sell();
    }
    protected void FillPriceplanName1Lease()
    {
        try
        {
            ddlpriceplanLease.DataSource = null;
            string po2 = "";

            if (ddlportalLease.SelectedIndex > 0)
            {
                po2 = " and  PricePlanMaster.PortalMasterId1='" + ddlportalLease.SelectedValue + "'";
            }
            string strcln1 = " SELECT   dbo.Priceplancategory.CategoryName + ' -- ' + dbo.PricePlanMaster.PricePlanName AS PricePlanName,  dbo.PricePlanMaster.PricePlanId FROM dbo.ProductMaster INNER JOIN dbo.VersionInfoMaster ON dbo.VersionInfoMaster.ProductId = dbo.ProductMaster.ProductId INNER JOIN dbo.PricePlanMaster ON dbo.PricePlanMaster.VersionInfoMasterId = dbo.VersionInfoMaster.VersionInfoId INNER JOIN dbo.Priceplancategory ON dbo.PricePlanMaster.PriceplancatId = dbo.Priceplancategory.ID INNER JOIN dbo.PortalMasterTbl ON dbo.Priceplancategory.PortalId = dbo.PortalMasterTbl.Id  where   dbo.Priceplancategory.CategoryTypeID='13' and PricePlanMaster.active='True' " + po2 + " and PortalMasterTbl.IsHostingServer=1 ";

            SqlCommand cmdcln = new SqlCommand(strcln1, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);

            ddlpriceplanLease.DataSource = dtcln;
            ddlpriceplanLease.DataValueField = "PricePlanId";
            ddlpriceplanLease.DataTextField = "PricePlanName";
            ddlpriceplanLease.DataBind();
            ddlpriceplanLease.Items.Insert(0, "--Select--");
            ddlpriceplanLease.Items[0].Value = "0";

        }
        catch
        {
        }
    }
    protected void FillPriceplanName2shared()
    {
        try
        {
            ddlpriceplanShared.DataSource = null;

            string po1 = "";
            string po2 = "";
            string po3 = "";
            if (ddlportalShared.SelectedIndex > 0)
            {
                po1 = " and  PricePlanMaster.PortalMasterId1='" + ddlportalShared.SelectedValue + "'";
            }
            string strcln1 = " SELECT   dbo.Priceplancategory.CategoryName + ' -- ' + dbo.PricePlanMaster.PricePlanName AS PricePlanName,  dbo.PricePlanMaster.PricePlanId FROM dbo.ProductMaster INNER JOIN dbo.VersionInfoMaster ON dbo.VersionInfoMaster.ProductId = dbo.ProductMaster.ProductId INNER JOIN dbo.PricePlanMaster ON dbo.PricePlanMaster.VersionInfoMasterId = dbo.VersionInfoMaster.VersionInfoId INNER JOIN dbo.Priceplancategory ON dbo.PricePlanMaster.PriceplancatId = dbo.Priceplancategory.ID INNER JOIN dbo.PortalMasterTbl ON dbo.Priceplancategory.PortalId = dbo.PortalMasterTbl.Id  where   dbo.Priceplancategory.CategoryTypeID='13' and PricePlanMaster.active='True' and PortalMasterTbl.IsHostingServer=1 " + po1 + "";

            SqlCommand cmdcln = new SqlCommand(strcln1, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);

            ddlpriceplanShared.DataSource = dtcln;
            ddlpriceplanShared.DataValueField = "PricePlanId";
            ddlpriceplanShared.DataTextField = "PricePlanName";
            ddlpriceplanShared.DataBind();
            ddlpriceplanShared.Items.Insert(0, "--Select--");
            ddlpriceplanShared.Items[0].Value = "0";
        }
        catch
        {
        }
    }  
    protected void FillPriceplanName3sell()
    {
        try
        {          
            ddlpriceplanSell.DataSource = null;
            string po1="";
            string po2="";
            string po3="";

            if (ddlportalanSell.SelectedIndex > 0)
            {
                po3 = " and  PricePlanMaster.PortalMasterId1='" + ddlportalanSell.SelectedValue + "'";
            }
            string strcln1 = " SELECT   dbo.Priceplancategory.CategoryName + ' -- ' + dbo.PricePlanMaster.PricePlanName AS PricePlanName,  dbo.PricePlanMaster.PricePlanId FROM dbo.ProductMaster INNER JOIN dbo.VersionInfoMaster ON dbo.VersionInfoMaster.ProductId = dbo.ProductMaster.ProductId INNER JOIN dbo.PricePlanMaster ON dbo.PricePlanMaster.VersionInfoMasterId = dbo.VersionInfoMaster.VersionInfoId INNER JOIN dbo.Priceplancategory ON dbo.PricePlanMaster.PriceplancatId = dbo.Priceplancategory.ID INNER JOIN dbo.PortalMasterTbl ON dbo.Priceplancategory.PortalId = dbo.PortalMasterTbl.Id  where   dbo.Priceplancategory.CategoryTypeID='13' and PricePlanMaster.active='True' " + po3 + " and PortalMasterTbl.IsHostingServer=1 ";

            SqlCommand cmdcln = new SqlCommand(strcln1, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);
            ddlpriceplanSell.DataSource = dtcln;
            ddlpriceplanSell.DataValueField = "PricePlanId";
            ddlpriceplanSell.DataTextField = "PricePlanName";
            ddlpriceplanSell.DataBind();
            ddlpriceplanSell.Items.Insert(0, "--Select--");
            ddlpriceplanSell.Items[0].Value = "0";  
          
        }
        catch
        {
        }       
    }
   
    
    protected void chk_ServerMonthlyExclusiveLease_CheckedChanged(object sender, EventArgs e)
    {
        if (chk_ServerMonthlyExclusiveLease.Checked == true)
        {          
            ddlpriceplanLease.Visible = true;
            lblpriceplanLease.Visible = true;

            lblpriceplanLease1.Visible = true;
            ddlportalLease.Visible = true;
            
        }
        else
        {
            ddlpriceplanLease.Visible = false;
            ddlpriceplanLease.SelectedIndex = 0;
            lblpriceplanLease.Visible = false;

            lblpriceplanLease1.Visible = false;
            ddlportalLease.Visible = false;
            ddlportalLease.SelectedIndex = 0;
           
        }
        if (chk_ServerMonthlySharedLease.Checked == true)
        {
            ddlpriceplanShared.Visible = true;
            lblpriceplanShared.Visible = true;

            lblpriceplanShared1.Visible = true;
            ddlportalShared.Visible = true;

            pnlshared.Visible = true;  
        }
        else
        {
            ddlpriceplanShared.Visible = false;
            ddlpriceplanShared.SelectedIndex = 0;
            lblpriceplanShared.Visible = false;

            lblpriceplanShared1.Visible = false;
            ddlportalShared.Visible = false;
            ddlportalShared.SelectedIndex = 0;

            pnlshared.Visible = false;
            txtNoofcompanycanuse.Text = "";
        }
        if (chk_ServersforSell.Checked == true)
        {
            ddlpriceplanSell.Visible = true;
            lblpriceplanSell.Visible = true;

            lblpriceplanSell1.Visible = true;
            ddlportalanSell.Visible = true; 
        }
        else
        {
            ddlpriceplanSell.Visible = false;
            ddlpriceplanSell.SelectedIndex = 0;
            lblpriceplanSell.Visible = false;

            lblpriceplanSell1.Visible = false;
            ddlportalanSell.Visible = false;
            ddlportalanSell.SelectedIndex = 0;
        }
    }
   
  
    protected void ch1_chachedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow item in GvRoleName.Rows)
        {
            CheckBox cbItem1 = (CheckBox)item.FindControl("cbItem");
            cbItem1.Checked = ((CheckBox)sender).Checked;
        }
    }
    protected void cbItem_chachedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow item in GvRoleName.Rows)
        {
            CheckBox cbItem = (CheckBox)item.FindControl("cbItem");
            Label lblroleid = (Label)item.FindControl("lblroleid");
            if (lblroleid.Text == "3")
            {
                if (cbItem.Checked == true)
                {
                    pnlshared.Visible = true;
                }
                else
                {
                    pnlshared.Visible = false;
                    txtNoofcompanycanuse.Text = "0";
                }
            }
            
        }
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


    protected void fillgrid1()
    {
        string str = " select *,case when (ServerMasterTbl.Status='1') then 'Active' else 'Inactive' End as Statuslabel from ServerMasterTbl where Id<>'' ";

        string status = "";
        string search = "";

        if (DropDownList1.SelectedValue != "2")
        {
            status = " and Status='" + DropDownList1.SelectedValue + "' ";
        }

        if (TextBox1.Text.Length > 0 && TextBox1.Text != "")
        {
            search = " and ( (ServerName like '%" + TextBox1.Text.Replace("'", "''") + "%') or (ServerComputerFullName like '%" + TextBox1.Text.Replace("'", "''") + "%') or (serverloction like '%" + TextBox1.Text.Replace("'", "''") + "%') or (PublicIp like '%" + TextBox1.Text.Replace("'", "''") + "%') or (Ipaddress  like '%" + TextBox1.Text.Replace("'", "''") + "%') or (Sqlinstancename like '%" + TextBox1.Text.Replace("'", "''") + "%') or (port like '%" + TextBox1.Text.Replace("'", "''") + "%')or (Busiwizsatellitesiteurl like '%" + TextBox1.Text.Replace("'", "''") + "%') or (DateCreated like '%" + TextBox1.Text.Replace("'", "''") + "%')   )";
        }
        string ord = " order by DateCreated desc";

        string finalstr = str + status + search + ord;

        SqlDataAdapter adp = new SqlDataAdapter(finalstr, con);
        DataTable ds = new DataTable();
        adp.Fill(ds);

        GridView2.DataSource = ds;
        GridView2.DataBind();

    }

    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string str1 = "select * from ServerMasterTbl where ServerName='" + txtServerName.Text + "' and ServerComputerFullName='" + txtservercomputerfullname.Text + "' and Id <> '" + ViewState["mm1"].ToString() + "'";
        SqlCommand cmd1 = new SqlCommand(str1, con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);

        if (dt1.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Record already exists";
        }
        else
        {

            //string str1c = "select * from ServerMasterTbl  where Id='" + ViewState["mm1"].ToString() + "'";
            //SqlCommand cmd1c = new SqlCommand(str1c, con);
            //SqlDataAdapter da1c = new SqlDataAdapter(cmd1c);
            //DataTable dt1c = new DataTable();
            //da1c.Fill(dt1c);
            //string HashKey = "";
            //string geren = "";
            //if (dt1c.Rows.Count > 0)
            //{
            //    //if (Convert.ToString(dt1c.Rows[0][""]) == "")
            //    //{

            //    //    encstr = CreateLicenceKey(out HashKey);
            //    //    geren = " Enckey='" + encstr + "', ";
            //    //}
            //    lblmsg.Visible = true;
            //    lblmsg.Text = "Record already exist";
            //}
            SqlCommand com = new SqlCommand(" update ServerMasterTbl set  ServerName='" + txtServerName.Text + "',serverloction='" + txtserverloction.Text + "',serverdetail='" + txtserverdetail.Text + "',Ipaddress='" + txtIpaddress.Text + "',port='" + txtport.Text + "',serverdefaultpathforiis='" + txtserverdefaultpathforiis.Text + "',serverdefaultpathformdf='" + txtserverdefaultpathformdf.Text + "',serverdefaultpathforfdf='" + txtserverdefaultpathforfdf.Text + "',folderpathformastercode='" + txtfolderpathformastercode.Text + "',Busiwizsatellitesiteurl='" + txtBusiwizsatellitesiteurl.Text + "' ,Sqlinstancename='" + txtSqlinstancename.Text + "',Sapassword='" + PageMgmt.Encrypted(txtSapassword.Text) + "',BusicontrolUserId='" + txtUserId.Text + "',BusicontrolPassword= '" + PageMgmt.Encrypted(txtwindowpassword.Text) + "',PublicIp='" + txtpubip.Text + "' ,ServerComputerFullName='" + txtservercomputerfullname.Text + "',BusiwizsimpleUserUserID='" + txtsimplebusiwizuser.Text + "',BusiwizsimpleUserPassword='" + PageMgmt.Encrypted(txtsimpleuserpassword.Text) + "',DefaultMdfpath='" + txtdefaultdatabasemdfpath.Text + "',DefaultLdfpath='" + txtdefaultdatabaseldfpath.Text + "',DefaultDatabaseName='" + txtdefaultdatabasename.Text + "',Status='" + ddlstatus.SelectedValue + "',DefaultsqlInstance='" + txtdefaultsqlinstance.Text + "',MacAddress='" + txtmacaddress.Text + "',ComputerName='" + txtcomputername.Text + "',InDomain='" + RadioButtonList2.SelectedValue + "',DomainName='" + txtdomainname.Text + "',DomainGroupName='" + txtdomaingrpname.Text + "',FTPurl='" + txtftpurl.Text + "',FTPPort='" + txtftpport.Text + "',FTPUserId='" + txtftpuserid.Text + "',FTPPassword='" + PageMgmt.Encrypted(txtftppassword.Text) + "',Name='" + txtname.Text + "',HomePhone='" + txthomephone.Text + "',MobileName='" + txtmobilephoneadmin.Text + "',Email='" + txtadminemail.Text + "',CountryID='" + ddlcountry.SelectedValue + "',CarrierID='" + ddlcarriername.SelectedValue + "',PortforCompanymastersqlistance = '" + txtcompnayport.Text + "',SqlServerName='" + txtdefaultsqlinstance.Text + "' ,ProductMasterindividualID='" + DDLProductMasterindividual.SelectedValue + "' , IsLeaseServer='" + chk_ServerMonthlyExclusiveLease.Checked + "', MaxCompSharing='" + txtNoofcompanycanuse.Text + "'   ,IsSharedServer='" + chk_ServerMonthlySharedLease.Checked + "',ISSaleServer='" + chk_ServersforSell.Checked + "' ,IsLeaseServerPPlanID='" + ddlpriceplanLease.SelectedValue + "',IsSharedServerPPlanID='" + ddlpriceplanShared.SelectedValue + "',ISSaleServerPPlanID='" + ddlpriceplanSell.SelectedValue + "' ,RadioISCommonServer='"+RblServerType.SelectedValue +"' , MaxCommonCompanyShared='"+txtMaxNoOfCompany.Text+"' where Id='" + ViewState["mm1"].ToString() + "' ", con); 
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            com.ExecuteNonQuery();
            con.Close();


            string st2 = " Delete from Server_clientBackupFTP where serverid=" + ViewState["mm1"].ToString();
            SqlCommand cmd2 = new SqlCommand(st2, con);
            con.Open();
            cmd2.ExecuteNonQuery();
            con.Close();
            foreach (GridViewRow gdr in gridFileAttach.Rows)
            {
                Label FTPurl = (Label)gdr.FindControl("Label1");
                Label lblFTPPort = (Label)gdr.FindControl("lblFTPPort");
                Label lblusrid = (Label)gdr.FindControl("lblusrid");
                Label lbllocation = (Label)gdr.FindControl("lbllocation");

                Label lblselectdefauly = (Label)gdr.FindControl("lblselectdefauly");
                Label lblactive = (Label)gdr.FindControl("lblactive");
                Label lbldesc = (Label)gdr.FindControl("lbldesc");
                Label lblpass = (Label)gdr.FindControl("lblpass");
                Label lblFTPfolder = (Label)gdr.FindControl("lblFTPfolder");
                

                con.Open();
                SqlCommand cmdFTP = new SqlCommand("Insert_Server_clientBackupFTP", con);
                cmdFTP.CommandType = CommandType.StoredProcedure;
                cmdFTP.Parameters.AddWithValue("@serverid", ViewState["mm1"].ToString());
                cmdFTP.Parameters.AddWithValue("@FTPurl", FTPurl.Text);
                cmdFTP.Parameters.AddWithValue("@FTPPort", lblFTPPort.Text);
                cmdFTP.Parameters.AddWithValue("@FTPUserId", lblusrid.Text);
                cmdFTP.Parameters.AddWithValue("@FTPPassword", lblpass.Text);
                cmdFTP.Parameters.AddWithValue("@Description", lbldesc.Text);
                cmdFTP.Parameters.AddWithValue("@location", lblusrid.Text);
                cmdFTP.Parameters.AddWithValue("@active", lblactive.Text);
                cmdFTP.Parameters.AddWithValue("@selectdefauly", lblselectdefauly.Text);
                cmdFTP.Parameters.AddWithValue("@FTPfolder", lblFTPfolder.Text);
                cmdFTP.ExecuteNonQuery();
                con.Close();
            }
            lblmsg.Visible = true;
            lblmsg.Text = "Record Updated successfully";

            FillProductMasterindividual();
            fillgrid1();
            clear();
            addnewpanel.Visible = true;
            pnladdnew.Visible = false;
        }

    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        clear();
        addnewpanel.Visible = true;
    }
    protected void clear()
    {
        txtServerName.Text = "";
        txtserverloction.Text = "";
        txtserverdetail.Text = "";
        txtIpaddress.Text = "";
        txtport.Text = "";
        txtSapassword.Text = "";
        txtSapassword.Attributes.Clear();
        txtserverdefaultpathforiis.Text = "";
        txtserverdefaultpathformdf.Text = "";
        txtserverdefaultpathforfdf.Text = "";
        txtfolderpathformastercode.Text = "";
        txtBusiwizsatellitesiteurl.Text = "";
        txtSqlinstancename.Text = "";
        pnladdnew.Visible = false;
         
        txtNoofcompanycanuse.Text ="0";

        chk_ServerMonthlyExclusiveLease.Checked = false; 
        chk_ServerMonthlySharedLease.Checked =false;
        chk_ServersforSell.Checked = false;
       
    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            int mm1 = Convert.ToInt32(e.CommandArgument);
            SqlCommand cmdd1 = new SqlCommand("delete from ServerMasterTbl where Id=" + mm1, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdd1.ExecuteNonQuery();
            con.Close();

            fillgrid1();
            lblmsg.Text = "Record deleted successfully";


        }


        if (e.CommandName == "Edit")
        {
            btnupdate.Visible = true;
            btnadd.Visible = false;
            Label5.Text = "Edit Server";
            lblmsg.Text = "";
            int mm = Convert.ToInt32(e.CommandArgument);

          

            ViewState["mm1"] = mm;
            SqlDataAdapter da1 = new SqlDataAdapter("select * from ServerMasterTbl where ID=" + mm + "", con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);


            if (dt1.Rows.Count > 0)
            {
                RadioButtonList1.SelectedValue = dt1.Rows[0]["DefaultCreated"].ToString();
                RadioButtonList1.Enabled = false;

                if (RadioButtonList1.SelectedValue == "1")
                {
                    pnlsqldetail.Visible = true;
                    pnlsqldetail.Enabled = true;

                }
                if (RadioButtonList1.SelectedValue == "0")
                {
                    pnlsqldetail.Visible = true;
                    pnlsqldetail.Enabled = false;

                }

                RadioButtonList2.SelectedValue = dt1.Rows[0]["InDomain"].ToString();


                if (RadioButtonList2.SelectedValue == "1")
                {
                    Panel1.Visible = true;
                    txtdomainname.Text = dt1.Rows[0]["DomainName"].ToString();
                    txtdomaingrpname.Text = dt1.Rows[0]["DomainGroupName"].ToString();
                }
                if (RadioButtonList2.SelectedValue == "0")
                {

                    Panel1.Visible = false;
                }

                txtServerName.Text = dt1.Rows[0]["ServerName"].ToString();
                txtservercomputerfullname.Text = dt1.Rows[0]["ServerComputerFullName"].ToString();
                txtserverloction.Text = dt1.Rows[0]["serverloction"].ToString();
                txtserverdetail.Text = dt1.Rows[0]["serverdetail"].ToString();
                txtcomputername.Text = dt1.Rows[0]["ComputerName"].ToString();
                txtmacaddress.Text = dt1.Rows[0]["MacAddress"].ToString();


                txtBusiwizsatellitesiteurl.Text = dt1.Rows[0]["Busiwizsatellitesiteurl"].ToString();
                txtfolderpathformastercode.Text = dt1.Rows[0]["folderpathformastercode"].ToString();
                txtserverdefaultpathforiis.Text = dt1.Rows[0]["serverdefaultpathforiis"].ToString();
                txtserverdefaultpathformdf.Text = dt1.Rows[0]["serverdefaultpathformdf"].ToString();
                txtserverdefaultpathforfdf.Text = dt1.Rows[0]["serverdefaultpathforfdf"].ToString();

                txtUserId.Text = dt1.Rows[0]["BusicontrolUserId"].ToString();
                txtwindowpassword.Text = PageMgmt.Decrypted(dt1.Rows[0]["BusicontrolPassword"].ToString());
                string strqa3 = txtwindowpassword.Text;

                txtwindowpassword.Attributes.Add("Value", strqa3);
                txtsimplebusiwizuser.Text = dt1.Rows[0]["BusiwizsimpleUserUserID"].ToString();
                txtsimpleuserpassword.Text = PageMgmt.Decrypted(dt1.Rows[0]["BusiwizsimpleUserPassword"].ToString());
                string strsimpleuserpwd = txtsimpleuserpassword.Text;
                txtsimpleuserpassword.Attributes.Add("Value", strsimpleuserpwd);

                txtpubip.Text = dt1.Rows[0]["PublicIp"].ToString();
                txtIpaddress.Text = dt1.Rows[0]["Ipaddress"].ToString();


                txtdefaultdatabasemdfpath.Text = dt1.Rows[0]["DefaultMdfpath"].ToString();
                txtdefaultdatabaseldfpath.Text = dt1.Rows[0]["DefaultLdfpath"].ToString();
                txtdefaultdatabasename.Text = dt1.Rows[0]["DefaultDatabaseName"].ToString();

                ddlstatus.SelectedIndex = ddlstatus.Items.IndexOf(ddlstatus.Items.FindByValue(dt1.Rows[0]["Status"].ToString()));


                txtSqlinstancename.Text = dt1.Rows[0]["Sqlinstancename"].ToString();
                txtdefaultsqlinstance.Text = dt1.Rows[0]["DefaultsqlInstance"].ToString();
                txtport.Text = dt1.Rows[0]["port"].ToString();
                txtSapassword.Text = PageMgmt.Decrypted(dt1.Rows[0]["Sapassword"].ToString());
                string strqa1 = txtSapassword.Text;
                txtSapassword.Attributes.Add("Value", strqa1);
                
              

                string stpageall = " SELECT * From  Server_clientBackupFTP Where  serverid=" + ViewState["mm1"] + "";
                SqlCommand cmall = new SqlCommand(stpageall, con);
                DataTable dtall = new DataTable();
                SqlDataAdapter adpall = new SqlDataAdapter(cmall);
                adpall.Fill(dtall);                
                    Session["GridFileAttach1"] = dtall;
                    gridFileAttach.DataSource = dtall;
                    gridFileAttach.DataBind();
                


               
                string strqa3ftpdetail = txtftppassword.Text;
                txtftppassword.Attributes.Add("Value", strqa3ftpdetail);


                txtname.Text = dt1.Rows[0]["Name"].ToString();
                txthomephone.Text = dt1.Rows[0]["HomePhone"].ToString();
                txtadminemail.Text = dt1.Rows[0]["Email"].ToString();
                txtmobilephoneadmin.Text = dt1.Rows[0]["MobileName"].ToString();
                txtcompnayport.Text = dt1.Rows[0]["PortforCompanymastersqlistance"].ToString();

                Fillddlcountry();
                ddlcountry.SelectedIndex = ddlcountry.Items.IndexOf(ddlcountry.Items.FindByValue(dt1.Rows[0]["CountryID"].ToString()));

                carrirfill();
                ddlcarriername.SelectedIndex = ddlcarriername.Items.IndexOf(ddlcarriername.Items.FindByValue(dt1.Rows[0]["CarrierID"].ToString()));



                //----Checked
              
                //---------------------Radio--------
                try
                {
                    
                    FillProductMasterindividualEDIT(dt1.Rows[0]["ProductMasterindividualID"].ToString());
                    DDLProductMasterindividual.SelectedValue = dt1.Rows[0]["ProductMasterindividualID"].ToString();
                }
                catch (Exception ex)
                {
                }
                try
                {                    
                    RblServerType.SelectedValue =dt1.Rows[0]["RadioISCommonServer"].ToString();                   
                }
                catch (Exception ex)
                {
                    RblServerType.SelectedValue = "0";
                }
                RblServerType_SelectedIndexChanged(sender, e);
                //-----------------------
                
                //txtmonthlyleaserate.Text = dt1.Rows[0]["LeaseRate"].ToString();
                //txtSetupAmount.Text = dt1.Rows[0]["InitialSetupAmt"].ToString();
               //--------------------------------
                if (RblServerType.SelectedValue == "0")
                {
                    txtMaxNoOfCompany.Text = dt1.Rows[0]["MaxCommonCompanyShared"].ToString();
                    txtMaxNoOfCompany.Visible = true;
                  
                }
                else
                {
                    txtMaxNoOfCompany.Visible = false;
                   
                }
                if (RblServerType.SelectedValue == "1")
                {
                    try
                    {
                        try
                        {
                            chk_ServerMonthlyExclusiveLease.Checked = Convert.ToBoolean(dt1.Rows[0]["IsLeaseServer"].ToString());
                        }
                        catch (Exception ex)
                        {
                        }                       
                        fillportal1Lease();                       
                        string strcln1 = " SELECT   dbo.PricePlanMaster.PortalMasterId1 as PortalMasterId1 FROM dbo.ProductMaster INNER JOIN dbo.VersionInfoMaster ON dbo.VersionInfoMaster.ProductId = dbo.ProductMaster.ProductId INNER JOIN dbo.PricePlanMaster ON dbo.PricePlanMaster.VersionInfoMasterId = dbo.VersionInfoMaster.VersionInfoId INNER JOIN dbo.Priceplancategory ON dbo.PricePlanMaster.PriceplancatId = dbo.Priceplancategory.ID INNER JOIN dbo.PortalMasterTbl ON dbo.Priceplancategory.PortalId = dbo.PortalMasterTbl.Id  where dbo.PricePlanMaster.PricePlanId='" + dt1.Rows[0]["IsLeaseServerPPlanID"].ToString() + "' ";
                        SqlCommand cmdcln = new SqlCommand(strcln1, con);
                        DataTable dtcln = new DataTable();
                        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
                        adpcln.Fill(dtcln);
                        if(dtcln.Rows.Count>0)
                        {
                            ddlportalLease.SelectedIndex = ddlportalLease.Items.IndexOf(ddlportalLease.Items.FindByValue(dtcln.Rows[0]["PortalMasterId1"].ToString()));                               
                        }

                        FillPriceplanName1Lease();
                        ddlpriceplanLease.SelectedIndex = ddlpriceplanLease.Items.IndexOf(ddlpriceplanLease.Items.FindByValue(dt1.Rows[0]["IsLeaseServerPPlanID"].ToString()));
                    }
                    catch (Exception ex)
                    {
                    }
                    try
                    {
                        try
                        {
                            chk_ServerMonthlySharedLease.Checked = Convert.ToBoolean(dt1.Rows[0]["IsSharedServer"].ToString());
                            txtNoofcompanycanuse.Text = dt1.Rows[0]["MaxCompSharing"].ToString();
                        }
                        catch (Exception ex)
                        {
                        }                        
                        fillportal2Shared();
                        string strcln1 = " SELECT   dbo.PricePlanMaster.PortalMasterId1 as PortalMasterId1 FROM dbo.ProductMaster INNER JOIN dbo.VersionInfoMaster ON dbo.VersionInfoMaster.ProductId = dbo.ProductMaster.ProductId INNER JOIN dbo.PricePlanMaster ON dbo.PricePlanMaster.VersionInfoMasterId = dbo.VersionInfoMaster.VersionInfoId INNER JOIN dbo.Priceplancategory ON dbo.PricePlanMaster.PriceplancatId = dbo.Priceplancategory.ID INNER JOIN dbo.PortalMasterTbl ON dbo.Priceplancategory.PortalId = dbo.PortalMasterTbl.Id  where dbo.PricePlanMaster.PricePlanId='" + dt1.Rows[0]["IsSharedServerPPlanID"].ToString() + "' ";
                        SqlCommand cmdcln = new SqlCommand(strcln1, con);
                        DataTable dtcln = new DataTable();
                        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
                        adpcln.Fill(dtcln);
                        if (dtcln.Rows.Count > 0)
                        {
                            ddlportalShared.SelectedIndex = ddlportalShared.Items.IndexOf(ddlportalShared.Items.FindByValue(dtcln.Rows[0]["PortalMasterId1"].ToString()));
                        }

                        FillPriceplanName2shared();
                        ddlpriceplanShared.SelectedIndex = ddlpriceplanShared.Items.IndexOf(ddlpriceplanShared.Items.FindByValue(dt1.Rows[0]["IsSharedServerPPlanID"].ToString()));
                    }
                    catch (Exception ex)
                    {
                    }
                    try
                    {
                        try
                        {
                            chk_ServersforSell.Checked = Convert.ToBoolean(dt1.Rows[0]["ISSaleServer"].ToString());
                        }
                        catch (Exception ex)
                        {
                        }  
                        fillportal3sell();
                        string strcln1 = " SELECT   dbo.PricePlanMaster.PortalMasterId1 as PortalMasterId1 FROM dbo.ProductMaster INNER JOIN dbo.VersionInfoMaster ON dbo.VersionInfoMaster.ProductId = dbo.ProductMaster.ProductId INNER JOIN dbo.PricePlanMaster ON dbo.PricePlanMaster.VersionInfoMasterId = dbo.VersionInfoMaster.VersionInfoId INNER JOIN dbo.Priceplancategory ON dbo.PricePlanMaster.PriceplancatId = dbo.Priceplancategory.ID INNER JOIN dbo.PortalMasterTbl ON dbo.Priceplancategory.PortalId = dbo.PortalMasterTbl.Id  where dbo.PricePlanMaster.PricePlanId='" + dt1.Rows[0]["ISSaleServerPPlanID"].ToString() + "' ";
                        SqlCommand cmdcln = new SqlCommand(strcln1, con);
                        DataTable dtcln = new DataTable();
                        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
                        adpcln.Fill(dtcln);
                        if (dtcln.Rows.Count > 0)
                        {
                            ddlportalanSell.SelectedIndex = ddlportalanSell.Items.IndexOf(ddlportalanSell.Items.FindByValue(dtcln.Rows[0]["PortalMasterId1"].ToString()));
                        }

                        FillPriceplanName3sell();
                        ddlpriceplanSell.SelectedIndex = ddlpriceplanSell.Items.IndexOf(ddlpriceplanSell.Items.FindByValue(dt1.Rows[0]["ISSaleServerPPlanID"].ToString()));
                    }
                    catch (Exception ex)
                    {
                    }
                }
             
                chk_ServerMonthlyExclusiveLease_CheckedChanged(sender, e);
                pnladdnew.Visible = true;
                addnewpanel.Visible = false;


            }

        }

    }
    public void CheckDublicateFullnam()
    {
        string str1 = "select * from ServerMasterTbl where ServerComputerFullName='" + txtservercomputerfullname.Text + "'";
        SqlCommand cmd1 = new SqlCommand(str1, con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);

        if (dt1.Rows.Count > 0)
        {
            lbl_dublicat.Visible = true;
            lbl_dublicat.Text = "Record already exists";
            return; 
        }
        else
        {
            lbl_dublicat.Visible = false;
            lbl_dublicat.Text = "";
        }
    }
    protected void txtcomputername_TextChanged1(object sender, EventArgs e)
    {
        //05March2015 txtBusiwizsatellitesiteurl.Text = txtcomputername.Text + ".safestserver.com";
        CheckDublicateFullnam();

    }
    public void CheckDublicateURl()
    {
        string str1 = "select * from ServerMasterTbl where Busiwizsatellitesiteurl='" + txtBusiwizsatellitesiteurl.Text + "'";
        SqlCommand cmd1 = new SqlCommand(str1, con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);

        if (dt1.Rows.Count > 0)
        {
            lbl_dublicaturl.Visible = true;
            lbl_dublicaturl.Text = "Record already exists";
            return;
        }
        else
        {
            lbl_dublicaturl.Visible = false;
            lbl_dublicaturl.Text = "";
        }
    }


    protected void txtcomputername_TextChanged2(object sender, EventArgs e)
    {
        //05March2015 txtBusiwizsatellitesiteurl.Text = txtcomputername.Text + ".safestserver.com";
        CheckDublicateURl();
    }

    public void CheckDublicateIP()
    {
        string str1 = "select * from ServerMasterTbl where Busiwizsatellitesiteurl='" + txtBusiwizsatellitesiteurl.Text + "'";
        SqlCommand cmd1 = new SqlCommand(str1, con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);

        if (dt1.Rows.Count > 0)
        {
            lbl_dublicaturl.Visible = true;
            lbl_dublicaturl.Text = "Record already exists";
            return;
        }
        else
        {
            lbl_dublicaturl.Visible = false;
            lbl_dublicaturl.Text = "";
        }
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        CheckDublicateFullnam();
        CheckDublicateURl();
        string str1 = " select * from ServerMasterTbl where MacAddress='" + txtmacaddress.Text + "' and PublicIp='" + txtpubip.Text + "' and Ipaddress='" + txtIpaddress.Text + "'  ";
        SqlCommand cmd1 = new SqlCommand(str1, con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);

        if (dt1.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Record already exists";
        }
        else
        {
            string HashKey = "";
            encstr = CreateLicenceKey(out HashKey);

            // string satelliteurul = txtBusiwizsatellitesiteurl.Text + ".safestserver.com";
            string satelliteurul = txtBusiwizsatellitesiteurl.Text;

            string sqlsatelliteurul = txtcomputername.Text + "_SQL.safestserver.com";

            string SubMenuInsert = "Insert Into ServerMasterTbl (ServerName,ServerComputerFullName,serverloction,serverdetail,Busiwizsatellitesiteurl,folderpathformastercode,serverdefaultpathforiis,serverdefaultpathformdf,serverdefaultpathforfdf,BusicontrolUserId,BusicontrolPassword,PublicIp,Ipaddress,Sqlinstancename,port,Sapassword,DefaultCreated,Status,BusiwizsimpleUserUserID,BusiwizsimpleUserPassword,DefaultMdfpath,DefaultLdfpath,DefaultDatabaseName,DefaultsqlInstance,MacAddress,ComputerName,InDomain,DomainName,DomainGroupName,Enckey,sqlurl,FTPurl,FTPPort,FTPUserId,FTPPassword,Name,HomePhone,MobileName,Email,CountryID,CarrierID,DateCreated,PortforCompanymastersqlistance,SqlServerName,ProductMasterindividualID ,IsLeaseServer,MaxCompSharing,IsSharedServer,ISSaleServer, IsLeaseServerPPlanID,IsSharedServerPPlanID,ISSaleServerPPlanID ,RadioISCommonServer ,MaxCommonCompanyShared) " +
            " values ('" + txtServerName.Text + "','" + txtservercomputerfullname.Text + "','" + txtserverloction.Text + "','" + txtserverdetail.Text + "','" + satelliteurul + "','" + txtfolderpathformastercode.Text + "','" + txtserverdefaultpathforiis.Text + "','" + txtserverdefaultpathformdf.Text + "','" + txtserverdefaultpathforfdf.Text + "','" + txtUserId.Text + "','" + PageMgmt.Encrypted(txtwindowpassword.Text) + "','" + txtpubip.Text + "','" + txtIpaddress.Text + "','" + txtSqlinstancename.Text + "','" + txtport.Text + "','" + PageMgmt.Encrypted(txtSapassword.Text) + "','" + RadioButtonList1.SelectedValue + "','" + ddlstatus.SelectedValue + "','" + txtsimplebusiwizuser.Text + "','" + PageMgmt.Encrypted(txtsimpleuserpassword.Text) + "','" + txtserverdefaultpathformdf.Text + "','" + txtserverdefaultpathforfdf.Text + "','" + txtdefaultdatabasename.Text + "','" + txtdefaultsqlinstance.Text + "','" + txtmacaddress.Text + "','" + txtcomputername.Text + "','" + RadioButtonList2.SelectedValue + "','" + txtdomainname.Text + "','" + txtdomaingrpname.Text + "','" + encstr + "','" + sqlsatelliteurul + "','" + txtftpurl.Text + "','" + txtftpport.Text + "','" + txtftpuserid.Text + "','" + PageMgmt.Encrypted(txtftppassword.Text) + "','" + txtname.Text + "','" + txthomephone.Text + "','" + txtmobilephoneadmin.Text + "','" + txtadminemail.Text + "','" + ddlcountry.SelectedValue + "','" + ddlcarriername.SelectedValue + "','"+DateTime.Now.ToShortDateString()+"','"+ txtcompnayport.Text +"','"+ txtdefaultsqlinstance.Text +"' ,'"+DDLProductMasterindividual.SelectedValue+"' " +
            " ,'" + chk_ServerMonthlyExclusiveLease.Checked + "','" + txtNoofcompanycanuse.Text + "','" + chk_ServerMonthlySharedLease.Checked + "', '" + chk_ServersforSell.Checked + "','" + ddlpriceplanLease.SelectedValue + "' ,'" + ddlpriceplanShared.SelectedValue + "' ,'" + ddlpriceplanSell.SelectedValue + "','" + RblServerType.SelectedValue + "','" + txtMaxNoOfCompany.Text + "')";
            SqlCommand cmd = new SqlCommand(SubMenuInsert, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();          

            string strmax = " Select Max(Id) as Id from ServerMasterTbl";
            SqlCommand cmdmax = new SqlCommand(strmax, con);
            DataTable dtmax = new DataTable();
            SqlDataAdapter adpmax = new SqlDataAdapter(cmdmax);
            adpmax.Fill(dtmax);
            string id = "";
            if (dtmax.Rows.Count > 0)
            {
                id = dtmax.Rows[0]["Id"].ToString();
            }

            //-----------------------------------------
            int lease = 0;
            int Shared = 0;
            int Sell = 0;
            if (chk_ServerMonthlyExclusiveLease.Checked == true)
            {
                lease = 5;
            }
            else
            {
                lease = 1013;
            }
            string strServerstatusmaster = " Insert Into Serverstatusmaster (SatelliteserverID,DateandTIme,Serversdtatusmasterid)  values ('" + id + "','" + DateTime.Now.ToShortDateString() + "','" + lease + "1')";
            cmd = new SqlCommand(strServerstatusmaster, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                con.Close();

                if (chk_ServerMonthlySharedLease.Checked == true)
                {
                    Shared = 4;
                }
                else
                {
                    Shared = 1012;
                }
            strServerstatusmaster = " Insert Into Serverstatusmaster (SatelliteserverID,DateandTIme,Serversdtatusmasterid)  values ('" + id + "','" + DateTime.Now.ToShortDateString() + "','" + Shared + "')";
            cmd = new SqlCommand(strServerstatusmaster, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                con.Close();
            
            if (chk_ServersforSell.Checked == true)
            {
                Sell = 6;
            }
            else
            {
                Sell = 1014;
            }
            strServerstatusmaster = " Insert Into Serverstatusmaster (SatelliteserverID,DateandTIme,Serversdtatusmasterid)  values ('" + id + "','" + DateTime.Now.ToShortDateString() + "','" + Sell + "')";
            cmd = new SqlCommand(strServerstatusmaster, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                con.Close();
            
            //------------------------------------------
            //---------------------------------------   
            foreach (GridViewRow gdr in gridFileAttach.Rows)
            {
                Label FTPurl = (Label)gdr.FindControl("Label1");
                Label lblFTPPort = (Label)gdr.FindControl("lblFTPPort");
                Label lblusrid = (Label)gdr.FindControl("lblusrid");
                Label lbllocation = (Label)gdr.FindControl("lbllocation");

                Label lblselectdefauly = (Label)gdr.FindControl("lblselectdefauly");
                Label lblactive = (Label)gdr.FindControl("lblactive");
                Label lbldesc = (Label)gdr.FindControl("lbldesc");
                Label lblpass = (Label)gdr.FindControl("lblpass");
                Label lblFTPfolder = (Label)gdr.FindControl("lblFTPfolder");
            
                                       
                con.Open();
                SqlCommand cmdFTP = new SqlCommand("Insert_Server_clientBackupFTP", con);
                cmdFTP.CommandType = CommandType.StoredProcedure;
                cmdFTP.Parameters.AddWithValue("@serverid", id);
                cmdFTP.Parameters.AddWithValue("@FTPurl", FTPurl.Text);
                cmdFTP.Parameters.AddWithValue("@FTPPort", lblFTPPort.Text);
                cmdFTP.Parameters.AddWithValue("@FTPUserId", lblusrid.Text);
                cmdFTP.Parameters.AddWithValue("@FTPPassword", lblpass.Text);
                cmdFTP.Parameters.AddWithValue("@Description", lbldesc.Text);
                cmdFTP.Parameters.AddWithValue("@location", lblusrid.Text);
                cmdFTP.Parameters.AddWithValue("@active", lblactive.Text);
                cmdFTP.Parameters.AddWithValue("@selectdefauly", lblselectdefauly.Text);
                cmdFTP.Parameters.AddWithValue("@FTPfolder", lblFTPfolder.Text);
                
                cmdFTP.ExecuteNonQuery();
                con.Close();
            }
               
            //----------------------------------------

            string dnsentryservername = "c3.safestserver.com";
            string dnsServerName = Environment.MachineName;
            string ipaddress = txtpubip.Text;

            try
            {

                AddARecord(txtcomputername.Text, "safestserver.com", ipaddress, dnsServerName, dnsentryservername);
                AddARecord(txtcomputername.Text + "_SQL", "safestserver.com", ipaddress, dnsServerName, dnsentryservername);
            }
            catch
            {

            }
            lblmsg.Visible = true;
            lblmsg.Text = "Record Inserted Successfully";
            FillProductMasterindividual();
            fillgrid1();
            clear();
            addnewpanel.Visible = true;
            pnladdnew.Visible = false;
        }
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
    protected void addnewpanel_Click(object sender, EventArgs e)
    {
        addnewpanel.Visible = false;

        pnladdnew.Visible = true;

        btnupdate.Visible = false;

        btnadd.Visible = true;

        Label5.Text = "Add Server";

        lblmsg.Text = "";



    }
    protected void btnprint_Click(object sender, EventArgs e)
    {

        if (btnprint.Text == "Printable Version")
        {

            btnprint.Text = "Hide Printable Version";
            Button5.Visible = true;

            GridView2.AllowPaging = false;
            GridView2.PageSize = 1000;

            fillgrid1();

            if (GridView2.Columns[11].Visible == true)
            {
                ViewState["docth"] = "tt";
                GridView2.Columns[11].Visible = false;
            }
            //if (GridView2.Columns[11].Visible == true)
            //{
            //    ViewState["edith"] = "tt";
            //    GridView2.Columns[11].Visible = false;
            //}


        }
        else
        {

            btnprint.Text = "Printable Version";
            Button5.Visible = false;

            GridView2.AllowPaging = true;
            GridView2.PageSize = 25;

            fillgrid1();

            if (ViewState["docth"] != null)
            {
                GridView2.Columns[11].Visible = true;
            }
            //if (ViewState["edith"] != null)
            //{
            //    GridView2.Columns[11].Visible = true;
            //}


        }



    }
    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void pnladdnew_click(object sender, EventArgs e)
    {
        pnladdnew.Visible = true;

        addnewpanel.Visible = false;

    }
    protected void Button1_Click1(object sender, EventArgs e)
    {

    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedValue == "1")
        {
            pnlsqldetail.Visible = true;
            pnlsqldetail.Enabled = true;

        }
        if (RadioButtonList1.SelectedValue == "0")
        {
            pnlsqldetail.Visible = true;
            pnlsqldetail.Enabled = false;

            string sapassword = CreateRandomPassword(16);
            txtSapassword.Attributes.Add("Value", sapassword.ToString());

            string busicontrolpassword = OtherCreateRandomPassword(16);
            txtport.Text = "2810";
            txtSqlinstancename.Text = "BUZSQL";
            txtdefaultsqlinstance.Text = "DEFAULTBUZ";
            txtdefaultdatabasename.Text = "ServerMaster";
            txtcompnayport.Text = "2811";

        }


    }
    private static string CreateRandomPassword(int passwordLength)
    {
        string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$?_-";

        char[] chars = new char[passwordLength];
        Random rd = new Random();
        for (int i = 0; i < passwordLength; i++)
        {
            chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
        }
        return new string(chars);
    }
    private static string OtherCreateRandomPassword(int passwordLength)
    {
        string allowedChars = "ABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$?_-abcdefghijkmnopqrstuvwxyz";

        char[] chars = new char[passwordLength];
        Random rd = new Random();
        for (int i = 0; i < passwordLength; i++)
        {
            chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
        }
        return new string(chars);
    }
    private static string BusiwizCreateRandomPassword(int passwordLength)
    {
        string allowedChars = "0123456789!@$?_ABCDEFGHJKLMNOPQRSTUVWXYZ-abcdefghijkmnopqrstuvwxyz";

        char[] chars = new char[passwordLength];
        Random rd = new Random();
        for (int i = 0; i < passwordLength; i++)
        {
            chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
        }
        return new string(chars);
    }

    public void AddARecord(string hostName, string zone, string iPAddress, string dnsServerName, string ServerName)
    {

        //ManagementScope scope = new ManagementScope(@"\\" + dnsServerName + "\\root\\MicrosoftDNS");
        //scope.Connect();
        //ManagementClass cmiClass = new ManagementClass(scope, new ManagementPath("MicrosoftDNS_AType"), null);
        //ManagementBaseObject inParams = cmiClass.GetMethodParameters("CreateInstanceFromPropertyData");
        //inParams["DnsServerName"] = ServerName;
        //inParams["ContainerName"] = zone;
        //inParams["OwnerName"] = hostName + "." + zone;
        //inParams["IPAddress"] = iPAddress;
        //cmiClass.InvokeMethod("CreateInstanceFromPropertyData", inParams, null);


        
    }



    protected void RadioButtonList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList2.SelectedValue == "0")
        {
            Panel1.Visible = false;
        }
        else
        {
            Panel1.Visible = true;
        }
       
        serverfullname(); 
    }

    protected void fillsoftwaremaster()
    {
        string str1 = "select * from Redistributed_Software_Mst where clientid='" + Session["ClientId"].ToString() + "' ";
        SqlCommand cmd1 = new SqlCommand(str1, con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);

        if (dt1.Rows.Count > 0)
        {
            ddldistributedsoftwaremaster.DataSource = dt1;
            ddldistributedsoftwaremaster.DataTextField = "redistributed_software_name";
            ddldistributedsoftwaremaster.DataValueField = "id";
            ddldistributedsoftwaremaster.DataBind();

        }
    }
    protected void fillsoftwaremasterlicensekeys()
    {
        string str1 = "select * from RedistributedSoftwareLicenseKeysTbl where RedistrubtedSoftwareMasterID='" + ddldistributedsoftwaremaster.SelectedValue + "' ";
        SqlCommand cmd1 = new SqlCommand(str1, con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);

        if (dt1.Rows.Count > 0)
        {
            ddllicensekeymaster.DataSource = dt1;
            ddllicensekeymaster.DataTextField = "redistributed_software_name";
            ddllicensekeymaster.DataValueField = "id";
            ddllicensekeymaster.DataBind();

        }
    }
    protected void ddldistributedsoftwaremaster_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillsoftwaremasterlicensekeys();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //DataTable dt = new DataTable();

        //if (ViewState["data"] == null)
        //{

        //    dt = CreateDatatable();
        //    DataRow Drow = dt.NewRow();



        //    Drow["ID"] = filaname.ToString();
        //    Drow["redistributed_software_name"] = ddlDocType.SelectedValue;
        //    Drow["Licensekey"] = "Not Uploaded";

        //    dt.Rows.Add(Drow);

        //    ViewState["data"] = dt;
        //    Gridreqinfo.DataSource = dt;
        //    Gridreqinfo.DataBind();





        //}
        //else
        //{
        //    dt = (DataTable)ViewState["data"];



        //    DataRow Drow = dt.NewRow();
        //    Drow["documentname"] = filaname.ToString();
        //    Drow["documenttype"] = ddlDocType.SelectedValue;
        //    Drow["status"] = "Not Uploaded";

        //    dt.Rows.Add(Drow);

        //    ViewState["data"] = dt;
        //    Gridreqinfo.DataSource = dt;
        //    Gridreqinfo.DataBind();

        //}



    }
    public DataTable CreateDatatable()
    {
        DataTable dt = new DataTable();

        DataColumn Dcom = new DataColumn();
        Dcom.DataType = System.Type.GetType("System.String");
        Dcom.ColumnName = "ID";
        Dcom.AllowDBNull = true;
        Dcom.Unique = false;
        Dcom.ReadOnly = false;

        DataColumn Dcom1 = new DataColumn();
        Dcom1.DataType = System.Type.GetType("System.String");
        Dcom1.ColumnName = "redistributed_software_name";
        Dcom1.AllowDBNull = true;
        Dcom1.Unique = false;
        Dcom1.ReadOnly = false;

        DataColumn Dcom2 = new DataColumn();
        Dcom2.DataType = System.Type.GetType("System.String");
        Dcom2.ColumnName = "Licensekey";
        Dcom2.AllowDBNull = true;
        Dcom2.Unique = false;
        Dcom2.ReadOnly = false;



        dt.Columns.Add(Dcom);
        dt.Columns.Add(Dcom1);
        dt.Columns.Add(Dcom2);

        return dt;
    }
    protected void Fillddlcountry()
    {
        DataTable dt = new DataTable();
        dt = SelectCountryMaster();
        ddlcountry.DataSource = dt;
        ddlcountry.DataBind();
        ddlcountry.Items.Insert(0, "-Select-");
        ddlcountry.Items[0].Value = "0";
    }
    protected DataTable SelectCountryMaster()
    {
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        cmd.Connection = con;
        cmd.CommandText = "SelectCountryMaster";
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        adp.Fill(dt);
        return dt;
    }
    protected void ddlcountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        carrirfill();
    }
    protected void carrirfill()
    {
        ddlcarriername.Items.Clear();

        string strcarrier = "select * from SMSCarrirMaster where Country='" + ddlcountry.SelectedValue + "' ";
        SqlCommand cmdcarrier = new SqlCommand(strcarrier, con);
        SqlDataAdapter adpcarrier = new SqlDataAdapter(cmdcarrier);
        DataTable dscarrier = new DataTable();
        adpcarrier.Fill(dscarrier);

        if (dscarrier.Rows.Count > 0)
        {

            ddlcarriername.DataSource = dscarrier;
            ddlcarriername.DataTextField = "CarrirName";
            ddlcarriername.DataValueField = "ID";
            ddlcarriername.DataBind();
        }
        else
        {
            ddlcarriername.Items.Insert(0, "-Select-");
            ddlcarriername.Items[0].Value = "0";

        }
    }
    public void serverfullname()
    {
        txtservercomputerfullname.Text = "";
        if (RadioButtonList2.SelectedValue == "0")
        {
            txtservercomputerfullname.Text = txtcomputername.Text;
        }
        else if (RadioButtonList2.SelectedValue == "1")
        {
            txtservercomputerfullname.Text = txtcomputername.Text + "." + txtdomainname.Text + "." + txtdomaingrpname.Text ;
        }

    }


    protected void txtcomputername_TextChanged(object sender, EventArgs e)
    {
       //05March2015 txtBusiwizsatellitesiteurl.Text = txtcomputername.Text + ".safestserver.com";
       serverfullname();
       
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        bool gg = isValidConnection(txtftpurl.Text, txtftpuserid.Text, txtftppassword.Text,txtport.Text );
        if (gg)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Read and Write Successful";
        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Read and Write not Successful";
        }
    }
    private bool isValidConnection(string url, string user, string password,string port)
    {
        try
        {
            string[] separator1 = new string[] { "/" };
            string[] strSplitArr1 = url.Split(separator1, StringSplitOptions.RemoveEmptyEntries);

            String productno = strSplitArr1[0].ToString();
            string ftpurl = "";

            if (productno == "FTP:" || productno == "ftp:")
            {
                if (strSplitArr1.Length >= 3)
                {
                    ftpurl = strSplitArr1[0].ToString() + "//" + strSplitArr1[1].ToString() + ":" + Convert.ToString(port);
                    for (int i = 2; i < strSplitArr1.Length; i++)
                    {
                        ftpurl += "/" + strSplitArr1[i].ToString();
                    }
                }
                else
                {
                    ftpurl = strSplitArr1[0].ToString() + "//" + strSplitArr1[1].ToString() + ":" + Convert.ToString(port);

                }
            }
            else
            {
                if (strSplitArr1.Length >= 2)
                {
                    ftpurl = "ftp://" + strSplitArr1[0].ToString() + ":" + Convert.ToString(port);
                    for (int i = 1; i < strSplitArr1.Length; i++)
                    {
                        ftpurl += "/" + strSplitArr1[i].ToString();
                    }
                }
                else
                {
                    ftpurl = "ftp://" + strSplitArr1[0].ToString() + ":" + Convert.ToString(port);

                }

            }
            string ftphost = ftpurl;



            // FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftphost);
            request.Method = WebRequestMethods.Ftp.ListDirectory;
            request.Credentials = new NetworkCredential(user, password);
            request.GetResponse();
        }
        catch (WebException ex)
        {
            return false;
        }
        return true;
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        fillgrid1();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        string filepath = "D:\\exefordownload\\setup.zip";
        FileInfo file = new FileInfo(filepath);

        if (file.Exists)
        {
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
            Response.AddHeader("Content-Length", file.Length.ToString());
            Response.ContentType = "application/octet-stream";
            Response.WriteFile(file.FullName);
            Response.End();

        }
    }
    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void txtcompnayport_TextChanged(object sender, EventArgs e)
    {

    }


    //FTP

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
        if (e.CommandName == "Edite")
        {
          //  gridFileAttach.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
            
            Label FTPurl = row.FindControl("Label1") as Label;
            Label lblFTPPort = row.FindControl("lblFTPPort") as Label;
            Label lblusrid = row.FindControl("lblusrid") as Label;
            Label lbllocation = row.FindControl("lbllocation") as Label;

            Label lblselectdefauly = row.FindControl("lblselectdefauly") as Label;
            Label lblactive = row.FindControl("lblactive") as Label;
            Label lbldesc = row.FindControl("lbldesc") as Label;
            Label lblpass = row.FindControl("lblpass") as Label;

            txtftpurl.Text = FTPurl.Text;
            txtftpport.Text = lblFTPPort.Text;
            txtftpuserid.Text = lblusrid.Text;
            txtLocation.Text = lbllocation.Text;
            txtftppassword.Text = lblpass.Text;
            lbldesc.Text = lblselectdefauly.Text;
            
            Boolean active = false;
            Boolean ckdefulta = false;

            try
            {
                active = Convert.ToBoolean(lblactive.Text);
                ckdefulta = Convert.ToBoolean(lblselectdefauly.Text); 
            }
            catch (Exception ex)
            {
            }
            active = ckbactive.Checked;
            ckdefulta = ckdefult.Checked;

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

        if (e.CommandName == "Test")
        {
            GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);

            Label FTPurl = row.FindControl("Label1") as Label;
            Label lblFTPPort = row.FindControl("lblFTPPort") as Label;
            Label lblusrid = row.FindControl("lblusrid") as Label;
            Label lbllocation = row.FindControl("lbllocation") as Label;

            Label lblselectdefauly = row.FindControl("lblselectdefauly") as Label;
            Label lblactive = row.FindControl("lblactive") as Label;
            Label lbldesc = row.FindControl("lbldesc") as Label;
            Label lblpass = row.FindControl("lblpass") as Label;
            bool gg = isValidConnection(FTPurl.Text, lblusrid.Text, lblpass.Text, lblFTPPort.Text );
            if (gg)
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Read and Write Successful";
            }
            else
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Read and Write not Successful";
            }
        }
        if (e.CommandName == "set")
        {
            GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
            Label lblserverid = row.FindControl("lblserverid") as Label;
            Label lblid = row.FindControl("lblid") as Label;
           
            string st2 = " update Server_clientBackupFTP set selectdefauly=0 Where  serverid='" + lblserverid.Text + "'";
            SqlCommand cmd2 = new SqlCommand(st2, con);
            con.Open();
            cmd2.ExecuteNonQuery();
            con.Close();

            st2 = " update Server_clientBackupFTP set selectdefauly=1 Where  id='" + lblid.Text + "'";
             cmd2 = new SqlCommand(st2, con);
            con.Open();
            cmd2.ExecuteNonQuery();
            con.Close();

            string stpageall = " SELECT * From  Server_clientBackupFTP Where  serverid=" + lblserverid.Text + "";
            SqlCommand cmall = new SqlCommand(stpageall, con);
            DataTable dtall = new DataTable();
            SqlDataAdapter adpall = new SqlDataAdapter(cmall);
            adpall.Fill(dtall);

            Session["GridFileAttach1"] = dtall;
            gridFileAttach.DataSource = dtall;
            gridFileAttach.DataBind();
                
        }
    }
    protected void Button2_Clickadd(object sender, EventArgs e)
    {
       // lblempmsg.Visible = false;
        if (txtftpurl.Text !="")
        {
           
            String filename = "";
            string audiofile = "";
            DataTable dt = new DataTable();

            int add = 1;
            if (ckdefult.Checked == true)
            {
                if (Session["GridFileAttach1"] != null)
                {
                    DataTable dslan = new DataTable();
                    dslan = (DataTable)Session["GridFileAttach1"];
                    foreach (DataRow row in dslan.Rows)
                    {
                        row["selectdefauly"] = "False";
                    }
                    Session["GridFileAttach1"]=dslan ;
                }
                //foreach (GridViewRow gdr in gridFileAttach.Rows)
                //{
                //    Label lblselectdefauly = (Label)gdr.FindControl("lblselectdefauly");
                //    Boolean active = Convert.ToBoolean(lblselectdefauly.Text); 
                //    if (active ==true)
                //    {
                       
                //    }
                //}
            }
            
            if (add == 1)
            {
                if (Session["GridFileAttach1"] == null)
                {
                  //  lblempmsg.Visible = false;
                    DataColumn dtcom2 = new DataColumn();
                    dtcom2.DataType = System.Type.GetType("System.String");
                    dtcom2.ColumnName = "FTPurl";
                    dtcom2.ReadOnly = false;
                    dtcom2.Unique = false;
                    dtcom2.AllowDBNull = true;
                    dt.Columns.Add(dtcom2);

                    DataColumn dtcom3 = new DataColumn();
                    dtcom3.DataType = System.Type.GetType("System.String");
                    dtcom3.ColumnName = "FTPPort";
                    dtcom3.ReadOnly = false;
                    dtcom3.Unique = false;
                    dtcom3.AllowDBNull = true;
                    dt.Columns.Add(dtcom3);

                    DataColumn dtcom4 = new DataColumn();
                    dtcom4.DataType = System.Type.GetType("System.String");
                    dtcom4.ColumnName = "FTPUserId";
                    dtcom4.ReadOnly = false;
                    dtcom4.Unique = false;
                    dtcom4.AllowDBNull = true;
                    dt.Columns.Add(dtcom4);

                    DataColumn dtcom5 = new DataColumn();
                    dtcom5.DataType = System.Type.GetType("System.String");
                    dtcom5.ColumnName = "location";
                    dtcom5.ReadOnly = false;
                    dtcom5.Unique = false;
                    dtcom5.AllowDBNull = true;
                    dt.Columns.Add(dtcom5);

                    DataColumn dtcom6 = new DataColumn();
                    dtcom6.DataType = System.Type.GetType("System.String");
                    dtcom6.ColumnName = "Description";
                    dtcom6.ReadOnly = false;
                    dtcom6.Unique = false;
                    dtcom6.AllowDBNull = true;
                    dt.Columns.Add(dtcom6);

                    DataColumn dtcom7 = new DataColumn();
                    dtcom7.DataType = System.Type.GetType("System.String");
                    dtcom7.ColumnName = "selectdefauly";
                    dtcom7.ReadOnly = false;
                    dtcom7.Unique = false;
                    dtcom7.AllowDBNull = true;
                    dt.Columns.Add(dtcom7);

                    DataColumn dtcom8 = new DataColumn();
                    dtcom8.DataType = System.Type.GetType("System.String");
                    dtcom8.ColumnName = "active";
                    dtcom8.ReadOnly = false;
                    dtcom8.Unique = false;
                    dtcom8.AllowDBNull = true;
                    dt.Columns.Add(dtcom8);

                    DataColumn dtcom9 = new DataColumn();
                    dtcom9.DataType = System.Type.GetType("System.String");
                    dtcom9.ColumnName = "FTPPassword";
                    dtcom9.ReadOnly = false;
                    dtcom9.Unique = false;
                    dtcom9.AllowDBNull = true;
                    dt.Columns.Add(dtcom9);

                    DataColumn dtcom10 = new DataColumn();
                    dtcom10.DataType = System.Type.GetType("System.String");
                    dtcom10.ColumnName = "FTPfolder";
                    dtcom10.ReadOnly = false;
                    dtcom10.Unique = false;
                    dtcom10.AllowDBNull = true;
                    dt.Columns.Add(dtcom10);

                    
                }
                else
                {
                    dt = (DataTable)Session["GridFileAttach1"];
                }
                DataRow dtrow = dt.NewRow();
                dtrow["FTPurl"] = txtftpurl.Text ;
                dtrow["FTPPort"] =txtftpport.Text;
                dtrow["FTPUserId"] = txtftpuserid.Text;
                dtrow["location"] = txtLocation.Text;
                dtrow["selectdefauly"] = ckdefult.Checked;
                dtrow["active"] = ckbactive.Checked; 
                dtrow["Description"] = txtdesc.Text;
                dtrow["FTPPassword"] = txtftppassword.Text;
                dtrow["FTPfolder"] = txtftpfolder.Text;                
                
                // dtrow["FileNameChanged"] = hdnFileName.Value;
                dt.Rows.Add(dtrow);
                Session["GridFileAttach1"] = dt;
                if (Session["GridFileAttach1"] != null)
                {
                    gridFileAttach.DataSource = dt;
                    gridFileAttach.DataBind();
                }
            }
            else
            {
              //  lblempmsg.Visible = true;
            }

              txtftpurl.Text="";
                 txtftpport.Text="";
                 txtftpuserid.Text="";
                txtLocation.Text="";
                 ckdefult.Checked=false;
                ckbactive.Checked=false;
                 txtdesc.Text="";

        }
    }

    //---------
    
      protected void FillProductMasterindividual()
    {
        string cmdstr = " Select * From Product_MasterIndividual Where PriceplanCategoryTypeID='13' and Active='1' and ID NOT IN ( Select ProductMasterindividualID as ID From ServerMasterTbl Where (ProductMasterindividualID != Null OR ProductMasterindividualID != '') ) ";
                SqlCommand cmdcln = new SqlCommand(cmdstr, con);
                DataTable dtcln = new DataTable();
                SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
                adpcln.Fill(dtcln);        
                DDLProductMasterindividual.DataSource = dtcln;
                DDLProductMasterindividual.DataValueField = "ID";
                DDLProductMasterindividual.DataTextField = "ProductName";
                DDLProductMasterindividual.DataBind();
                DDLProductMasterindividual.Items.Insert(0, "--Select--");
                DDLProductMasterindividual.Items[0].Value = "0";

    }               
    protected void FillProductMasterindividualEDIT(string id)
    {
        string cmdstr = " Select * From Product_MasterIndividual Where PriceplanCategoryTypeID='13' and Active='1' and (ID='" + id + "' OR ID NOT IN ( Select ProductMasterindividualID as ID From ServerMasterTbl Where (ProductMasterindividualID != Null OR ProductMasterindividualID != '') ) ) ";
        SqlCommand cmdcln = new SqlCommand(cmdstr, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        DDLProductMasterindividual.DataSource = dtcln;
        DDLProductMasterindividual.DataValueField = "ID";
        DDLProductMasterindividual.DataTextField = "ProductName";
        DDLProductMasterindividual.DataBind();
        DDLProductMasterindividual.Items.Insert(0, "--Select--");
        DDLProductMasterindividual.Items[0].Value = "0";

    }
    protected void ProductMasterindividual_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }

    //Lease hared Server

    //protected void rblistservertype_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (rblistservertype.SelectedValue == "0")
    //    {
    //        pnlshared.Visible = true;
    //    }
    //    else
    //    {
    //        pnlshared.Visible = false;
    //        txtNoofcompanycanuse.Text = "0";
    //    }       
    //}

    protected void FillGridServerType()
    {
        string strcln1 = " SELECT ID,Name,Active,Decription FROM ComputerOrPartsorServicePricePlancategoryType where Active=1  order by Name ";

        SqlCommand cmdcln = new SqlCommand(strcln1, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        GvRoleName.DataSource = dtcln;
        DataView myDataView = new DataView();
        myDataView = dtcln.DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }
        GvRoleName.DataBind();
    }
    protected void GvRoleName_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvRoleName.PageIndex = e.NewPageIndex;
        FillGridServerType();
    }
    protected void GvRoleName_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        FillGridServerType();
    }
   
    protected void GvRoleName_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Restore")
        {
            GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
            CheckBox cbItem = row.FindControl("cbItem") as CheckBox;
            Label lblroleid = row.FindControl("lblroleid") as Label;
            if (cbItem.Checked == true)
            {
                if (lblroleid.Text == "3")
                {
                    //ServersforSell 1
                    //ServerMonthlyExclusiveLease  2
                    //ServerMonthlySharedLease 3                    
                   // isshared = true;
                    pnlshared.Visible = true;
                }
                else
                {
                   // isshared = false;
                    pnlshared.Visible = false;
                    txtNoofcompanycanuse.Text = "0";
                }
                if (lblroleid.Text == "1")
                {
                   // issale = true;
                }
                else
                {
                   // issale = false;
                }
                if (lblroleid.Text == "2")
                {
                 //   islease = true;
                }
                else
                {
                  //  islease = false;
                }

            }
        }
    }

    //

    protected void RblServerType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RblServerType.SelectedValue == "1")
        {
            pnlLeasSharedSale.Visible = true;
          
            txtMaxNoOfCompany.Visible = false;
            txtMaxNoOfCompany.Text = "";
        }
        else
        {
          
            txtMaxNoOfCompany.Visible = true;

            pnlLeasSharedSale.Visible = false;

            chk_ServerMonthlyExclusiveLease.Checked = false;
            chk_ServerMonthlySharedLease.Checked = false;
            chk_ServersforSell.Checked = false; 
        }
    }
}
