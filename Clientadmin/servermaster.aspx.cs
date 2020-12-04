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
 SqlConnection conn =new SqlConnection();
    //con = new SqlConnection();
    //= @"Data Source =" + serversqlserverip + "," + serversqlport + "; Initial Catalog=" + serversqldbname + "; User ID='" + serveruserid + "'; Password=" + serversqlpwd + "; Persist Security Info=true;";


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

            filldatebyperiod();
           
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

    
    protected void chk_ServerMonthlyExclusiveLease_CheckedChanged(object sender, EventArgs e)
    {
        if (chk_ServerMonthlyExclusiveLease.Checked == true)
        {              
        }
        else
        {  
        }
        if (chk_ServerMonthlySharedLease.Checked == true)
        {
            pnlshared.Visible = true;  
        }
        else
        {           
            pnlshared.Visible = false;
            txtNoofcompanycanuse.Text = "";
        }
        if (chk_ServersforSell.Checked == true)
        {
           
        }
        else
        {
            
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


    protected DataTable select(string qu)
    {
        SqlCommand cmd = new SqlCommand(qu, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        return dt;
    }



    protected void fillgrid1()
    {
        string str = " select *,case when (ServerMasterTbl.Status='1') then 'Active' else 'Inactive' End as Statuslabel from ServerMasterTbl where Id<>''  ";

        string status = "";
        string search = "";

        if (DropDownList1.SelectedValue != "2")
        {
            status = " and Status='" + DropDownList1.SelectedValue + "' ";
        }
        if (ddlServerType.SelectedValue == "1")
        {
            status = " and ISCommonServer='1' ";
        }
        if (ddlServerType.SelectedValue == "2")
        {
            status = " and IsLeaseServer='1' ";
        }
        if (ddlServerType.SelectedValue == "3")
        {
            status = " and IsSharedServer='1' ";
        }
        if (ddlServerType.SelectedValue == "4")
        {
            status = " and ISSaleServer='1' ";
        }
        if (ddlServerType.SelectedValue == "5")
        {
            status = " and IsOwnServer='1' ";
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
       
        //for (int rowindex = 0; rowindex < ds.Rows.Count; rowindex++)        
        //{
        //    DataTable dateyu = select("select ServerMasterTbl.Ipaddress,ServerMasterTbl.FTPforMastercode,ServerMasterTbl.FTPuseridforDefaultIISpath,ServerMasterTbl.FtpPasswordforDefaultIISpath,ServerMasterTbl.FTPportfordefaultIISpath,ServerMasterTbl.MDF_FTPUrl,ServerMasterTbl.MDF_FTPUserId,ServerMasterTbl.MDF_FTPPassword,ServerMasterTbl.MDF_FTPPort,ServerMasterTbl.LDF_FTPUrl,ServerMasterTbl.LDF_FTPUserId,ServerMasterTbl.LDF_FTPPassword,ServerMasterTbl.LDF_FTPPort,ServerMasterTbl.Default_FTPUrl,ServerMasterTbl.Default_FTPUserId,ServerMasterTbl.Default_FTPPassword,ServerMasterTbl.Default_FTPPort,ServerMasterTbl.FTPurl,ServerMasterTbl.FTPUserId,ServerMasterTbl.FTPPassword,ServerMasterTbl.FTPPort from ServerMasterTbl where ServerMasterTbl.Id=" + ds.Rows[rowindex]["Id"] + "");
        //    if (dateyu.Rows.Count > 0)
        //    {
        //        string sdr = "";
        //        for (int i = 0; i < dateyu.Rows.Count; i++)
        //        {
        //            try
        //            {                       
        //                bool gg = isValidConnection(dateyu.Rows[0]["FTPforMastercode"].ToString(), dateyu.Rows[0]["FTPuseridforDefaultIISpath"].ToString(), dateyu.Rows[0]["FtpPasswordforDefaultIISpath"].ToString(), dateyu.Rows[0]["FTPportfordefaultIISpath"].ToString());
        //                bool gg1 = isValidConnection(dateyu.Rows[0]["MDF_FTPUrl"].ToString(), dateyu.Rows[0]["MDF_FTPUserId"].ToString(), dateyu.Rows[0]["MDF_FTPPassword"].ToString(), dateyu.Rows[0]["MDF_FTPPort"].ToString());
        //                bool gg2 = isValidConnection(dateyu.Rows[0]["LDF_FTPUrl"].ToString(), dateyu.Rows[0]["LDF_FTPUserId"].ToString(), dateyu.Rows[0]["LDF_FTPPassword"].ToString(), dateyu.Rows[0]["LDF_FTPPort"].ToString());
        //                bool gg3 = isValidConnection(dateyu.Rows[0]["Default_FTPUrl"].ToString(), dateyu.Rows[0]["Default_FTPUserId"].ToString(), dateyu.Rows[0]["Default_FTPPassword"].ToString(), dateyu.Rows[0]["Default_FTPPort"].ToString());
        //                bool gg4 = isValidConnection(dateyu.Rows[0]["FTPurl"].ToString(), dateyu.Rows[0]["FTPUserId"].ToString(), dateyu.Rows[0]["FTPPassword"].ToString(), dateyu.Rows[0]["FTPPort"].ToString());                        
        //                sdr="Is Working";
        //                ds.Rows[rowindex]["Default_FTPUrl"]=sdr;
        //            }
        //            catch 
        //            {
        //               sdr = "Is Not Working";
        //                ds.Rows[rowindex]["Default_FTPUrl"] = sdr;
        //            }
        //            try
        //            {
        //                var ping = new System.Net.NetworkInformation.Ping();
        //                var result = ping.Send("" + dateyu.Rows[0]["FTPforMastercode"].ToString() + "");
        //                if (result.Status != System.Net.NetworkInformation.IPStatus.Success)
        //                {
        //                    ds.Rows[rowindex]["Default_FTPUserId"] = "Server is On";
        //                    //lbl_weburlconnection.ForeColor = System.Drawing.Color.Green;
        //                }
        //                else
        //                {
        //                    ds.Rows[rowindex]["Default_FTPUserId"] = "Server is On";
        //                    //lbl_weburlconnection.ForeColor = System.Drawing.Color.Red;
        //                }
        //            }
        //            catch
        //            {
        //                ds.Rows[rowindex]["Default_FTPUserId"] = "Server is Off";
        //                // lbl_weburlconnection.ForeColor = System.Drawing.Color.Red;  
        //            }                   
        //        }
        //        ds.Rows[rowindex]["Default_FTPUrl"] = sdr;
        //    }

        //}
       
    }
    protected void chk_uploadcode_CheckedChanged(object sender, EventArgs e)
    {
        if (chk_conn.Checked == true)
        {
            CheckServerConn();
        }
    }

    protected void CheckServerConn()
    {
        foreach (GridViewRow item in GridView2.Rows)
        {
            Label lblserID = (Label)(item.FindControl("lblserID"));
            Label lblftpstatus = (Label)(item.FindControl("lblftpstatus"));
            Label lblserverstatus = (Label)(item.FindControl("lblserverstatus"));
            Label Label521 = (Label)(item.FindControl("lblserverstatus"));

            DataTable dateyu = select("select ServerMasterTbl.Ipaddress,ServerMasterTbl.FTPforMastercode,ServerMasterTbl.FTPuseridforDefaultIISpath,ServerMasterTbl.FtpPasswordforDefaultIISpath,ServerMasterTbl.FTPportfordefaultIISpath,ServerMasterTbl.MDF_FTPUrl,ServerMasterTbl.MDF_FTPUserId,ServerMasterTbl.MDF_FTPPassword,ServerMasterTbl.MDF_FTPPort,ServerMasterTbl.LDF_FTPUrl,ServerMasterTbl.LDF_FTPUserId,ServerMasterTbl.LDF_FTPPassword,ServerMasterTbl.LDF_FTPPort,ServerMasterTbl.Default_FTPUrl,ServerMasterTbl.Default_FTPUserId,ServerMasterTbl.Default_FTPPassword,ServerMasterTbl.Default_FTPPort,ServerMasterTbl.FTPurl,ServerMasterTbl.FTPUserId,ServerMasterTbl.FTPPassword,ServerMasterTbl.FTPPort from ServerMasterTbl where ServerMasterTbl.Id=" + lblserID.Text + "");
            if (dateyu.Rows.Count > 0)
            {
                string sdr = "";
                for (int i = 0; i < dateyu.Rows.Count; i++)
                {
                    try
                    {
                        bool gg = isValidConnection(dateyu.Rows[0]["FTPforMastercode"].ToString(), dateyu.Rows[0]["FTPuseridforDefaultIISpath"].ToString(), dateyu.Rows[0]["FtpPasswordforDefaultIISpath"].ToString(), dateyu.Rows[0]["FTPportfordefaultIISpath"].ToString());
                        bool gg1 = isValidConnection(dateyu.Rows[0]["MDF_FTPUrl"].ToString(), dateyu.Rows[0]["MDF_FTPUserId"].ToString(), dateyu.Rows[0]["MDF_FTPPassword"].ToString(), dateyu.Rows[0]["MDF_FTPPort"].ToString());
                        bool gg2 = isValidConnection(dateyu.Rows[0]["LDF_FTPUrl"].ToString(), dateyu.Rows[0]["LDF_FTPUserId"].ToString(), dateyu.Rows[0]["LDF_FTPPassword"].ToString(), dateyu.Rows[0]["LDF_FTPPort"].ToString());
                        bool gg3 = isValidConnection(dateyu.Rows[0]["Default_FTPUrl"].ToString(), dateyu.Rows[0]["Default_FTPUserId"].ToString(), dateyu.Rows[0]["Default_FTPPassword"].ToString(), dateyu.Rows[0]["Default_FTPPort"].ToString());
                        bool gg4 = isValidConnection(dateyu.Rows[0]["FTPurl"].ToString(), dateyu.Rows[0]["FTPUserId"].ToString(), dateyu.Rows[0]["FTPPassword"].ToString(), dateyu.Rows[0]["FTPPort"].ToString());
                        sdr = "Is Working";
                        lblftpstatus.Text = sdr;
                    }
                    catch
                    {
                        sdr = "Is Not Working";
                        lblftpstatus.Text = sdr;
                    }
                    try
                    {
                        var ping = new System.Net.NetworkInformation.Ping();
                        var result = ping.Send("" + dateyu.Rows[0]["FTPforMastercode"].ToString() + "");
                        if (result.Status != System.Net.NetworkInformation.IPStatus.Success)
                        {
                            lblserverstatus.Text = "Server is On";
                            //lbl_weburlconnection.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            lblserverstatus.Text = "Server is On";
                            //lbl_weburlconnection.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    catch
                    {
                        lblserverstatus.Text = "Server is Off";
                        Label521.ToolTip = "site is down please check the site";
                        // lbl_weburlconnection.ForeColor = System.Drawing.Color.Red;  
                    }
                }
                lblftpstatus.Text = sdr;
            }
        }
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
            SqlCommand com = new SqlCommand(" update ServerMasterTbl set  ServerName='" + txtServerName.Text + "',serverloction='" + txtserverloction.Text + "',serverdetail='" + txtserverdetail.Text + "',Ipaddress='" + txtIpaddress.Text + "',port='" + txtport.Text + "',serverdefaultpathforiis='" + TextBox4.Text + "',serverdefaultpathformdf='" + TextBox9.Text + "',serverdefaultpathforfdf='" + TextBox14.Text + "',folderpathformastercode='" + txtDatabaseName.Text + "',Busiwizsatellitesiteurl='" + txtBusiwizsatellitesiteurl.Text + "' ,Sqlinstancename='" + txtSqlinstancename.Text + "',Sapassword='" + PageMgmt.Encrypted(txtSapassword.Text) + "',BusicontrolUserId='" + txtUserId.Text + "',BusicontrolPassword= '" + PageMgmt.Encrypted(txtwindowpassword.Text) + "',PublicIp='" + txtpubip.Text + "' ,ServerComputerFullName='" + txtservercomputerfullname.Text + "',BusiwizsimpleUserUserID='" + txtsimplebusiwizuser.Text + "',BusiwizsimpleUserPassword='" + PageMgmt.Encrypted(txtsimpleuserpassword.Text) + "',DefaultMdfpath='" + txtdefaultdatabasemdfpath.Text + "',DefaultLdfpath='" + txtdefaultdatabaseldfpath.Text + "',DefaultDatabaseName='" + txtdefaultdatabasename.Text + "',Status='" + ddlstatus.SelectedValue + "',DefaultsqlInstance='" + txtdefaultsqlinstance.Text + "',MacAddress='" + txtmacaddress.Text + "',ComputerName='" + txtcomputername.Text + "',InDomain='" + RadioButtonList2.SelectedValue + "',DomainName='" + txtdomainname.Text + "',DomainGroupName='" + txtdomaingrpname.Text + "',FTPurl='" + txtftpurl.Text + "',FTPPort='" + txtftpport.Text + "',FTPUserId='" + txtftpuserid.Text + "',FTPPassword='" + PageMgmt.Encrypted(txtftppassword.Text) + "',Name='" + txtname.Text + "',HomePhone='" + txthomephone.Text + "',MobileName='" + txtmobilephoneadmin.Text + "',Email='" + txtadminemail.Text + "',CountryID='" + ddlcountry.SelectedValue + "',CarrierID='" + ddlcarriername.SelectedValue + "',PortforCompanymastersqlistance = '" + txtcompnayport.Text + "',SqlServerName='" + txtdefaultsqlinstance.Text + "' ,ProductMasterindividualID='" + DDLProductMasterindividual.SelectedValue + "' , IsLeaseServer='" + chk_ServerMonthlyExclusiveLease.Checked + "', MaxCompSharing='" + txtNoofcompanycanuse.Text + "'   ,IsSharedServer='" + chk_ServerMonthlySharedLease.Checked + "',ISSaleServer='" + chk_ServersforSell.Checked + "' ,ServerType='" + RblServerType.SelectedValue + "' , MaxCommonCompanyShared='" + txtMaxNoOfCompany.Text + "',FTPforMastercode='" + txtDatabaseServerurl.Text + "',FTPuseridforDefaultIISpath='" + txtDBUserId.Text + "',FtpPasswordforDefaultIISpath='" + txtDBPassword.Text + "',FTPportfordefaultIISpath='" + txtDatabaseAccessPort.Text + "',MDF_FTPUrl='" + TextBox10.Text + "', MDF_FTPPort='" + TextBox13.Text + "',MDF_FTPUserId='" + TextBox11.Text + "',MDF_FTPPassword='" + TextBox12.Text + "',LDF_FTPUrl='" + TextBox15.Text + "',LDF_FTPPort='" + TextBox18.Text + "',LDF_FTPUserId='" + TextBox16.Text + "',LDF_FTPPassword='" + TextBox17.Text + "',Default_FTPUrl='" + TextBox5.Text + "',Default_FTPUserId='" + TextBox6.Text + "',Default_FTPPassword='" + TextBox7.Text + "',Default_FTPPort='" + TextBox8 .Text+ "',DNS='"+CheckBox1.Checked+"' where Id='" + ViewState["mm1"].ToString() + "' ", con); 
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            com.ExecuteNonQuery();
            con.Close();


            string st2 = " Delete from Server_clientBackupFTP where serverid=" + ViewState["mm1"].ToString()+"";
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
            string strServerstatusmaster = " Delete From Serverstatusmaster Where Id='" + ViewState["mm1"] + "' ";
            SqlCommand cmd = new SqlCommand(strServerstatusmaster, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();
            //*************************************************************************************
            string id = ViewState["mm1"].ToString();
            int own = 0;
            int common = 0;
            int lease = 0;
            int Shared = 0;
            int Sell = 0;
            if (RblServerType.SelectedValue == "0")
            {
                common = 1015;
            }
            else
            {
                common = 1018;
            }
             strServerstatusmaster = " Insert Into Serverstatusmaster (SatelliteserverID,DateandTIme,Serversdtatusmasterid)  values ('" + id + "','" + DateTime.Now.ToShortDateString() + "','" + common + "')";
             cmd = new SqlCommand(strServerstatusmaster, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();
            //*****************************************************--------------------------
            //******************************************************------------------
          
           
            if (RblServerType.SelectedValue == "2")
            {
                own = 1016;
            }
            else
            {
                own = 1017;
            }
            strServerstatusmaster = " Insert Into Serverstatusmaster (SatelliteserverID,DateandTIme,Serversdtatusmasterid)  values ('" + id + "','" + DateTime.Now.ToShortDateString() + "','" + own + "')";
            cmd = new SqlCommand(strServerstatusmaster, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();
            //*****************************************************--------------------------
            //******************************************************------------------
            if (chk_ServerMonthlyExclusiveLease.Checked == true)
            {
                lease = 5;
            }
            else
            {
                lease = 1013;
            }
            strServerstatusmaster = " Insert Into Serverstatusmaster (SatelliteserverID,DateandTIme,Serversdtatusmasterid)  values ('" + id + "','" + DateTime.Now.ToShortDateString() + "','" + lease + "')";
            cmd = new SqlCommand(strServerstatusmaster, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();
            //*****************************************************--------------------------
            //******************************************************------------------
            if (chk_ServerMonthlySharedLease.Checked == true)
            {
                Shared = 4;
            }
            else
            {
                Shared = 1012;
            }

            strServerstatusmaster = " Insert Into Serverstatusmaster (SatelliteserverID ,DateandTIme,Serversdtatusmasterid)  values ('" + id + "','" + DateTime.Now.ToShortDateString() + "','" + Shared + "')";
            cmd = new SqlCommand(strServerstatusmaster, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();
            //*****************************************************--------------------------
            //******************************************************------------------
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
            //*****************************************************--------------------------
            //******************************************************------------------


            //string st23 = " Delete from Securitykeyforsilentpages where serverid=" + ViewState["mm1"].ToString()+"";
            //SqlCommand cmd23 = new SqlCommand(st23, con);
            //con.Open();
            //cmd23.ExecuteNonQuery();
            //con.Close();

            

            string strstate = "  select ClientMaster.ServerId from ClientMaster where ClientMasterId='35'";
            SqlCommand cmdstate = new SqlCommand(strstate, con);
            DataTable dtstate = new DataTable();
            SqlDataAdapter adpstate = new SqlDataAdapter(cmdstate);
            adpstate.Fill(dtstate);
            {
                string stre = dtstate.Rows[0]["ServerId"].ToString();


//                DataTable dtconn = select(@"SELECT ServerMasterTbl.ServerName,ServerMasterTbl.port,ServerMasterTbl.sqlserveruserid,ServerMasterTbl.Sapassword,Instancename 
//                                     FROM ServerMasterTbl INNER JOIN ClientMaster ON ServerMasterTbl.Id = ClientMaster.ServerId inner join ProductMaster on ProductMaster.ClientID=ClientMaster.ClientMasterId
//                                     inner join VersionInfoMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId inner join CodeTypeTbl on CodeTypeTbl.ProductVersionId=VersionInfoMaster.VersionInfoId  where ServerMasterTbl.Id='" + stre + "'");
//                if (dtconn.Rows.Count > 0)
//                    conn = new SqlConnection();

//                conn.ConnectionString = ServerWizard.ServerDatabaseFromInstanceTCP(stre.ToString(), dtconn.Rows[0]["Instancename"].ToString(), dtconn.Rows[0]["DefaultsqlInstance"].ToString());

                   // conn = new SqlConnection();
                // conn.ConnectionString = @"Data Source =" + dtconn.Rows[0]["SqlServerName"] + "\\" + dtconn.Rows[0]["instancename"] + "," + dtconn.Rows[0]["port"] + "; Initial Catalog=" + DropDownList1.SelectedItem.Text + "; User ID='" +  dtconn.Rows[0]["sqlserveruserid"] + "'; Password=" +  dtconn.Rows[0]["sqlserverpassword"] + "; Persist Security Info=true;";
                //conn1 = new SqlConnection();
              //  conn.ConnectionString = @"Data Source =" + dtconn.Rows[0]["ServerName"] + "\\" + dtconn.Rows[0]["Instancename"] + "," + dtconn.Rows[0]["port"] + "; Initial Catalog=C3SATELLITESERVER; User ID=sa; Password=" + PageMgmt.Decrypted(dtconn.Rows[0]["Sapassword"].ToString()) + "; Persist Security Info=true;";

                // conn1.ConnectionString = @"Data Source =TCP:192.168.9.120,30000; Initial Catalog=Licensejobcenter.OADB; User ID=TVMDeveloper; Password=Om2015++; Persist Security Info=true;";
                // conn1 = new SqlConnection(strConn);

                conn = new SqlConnection();

                conn = ServerWizard.ServerDefaultInstanceConnetionTCP_Serverid(ViewState["mm1"].ToString());


                conn.Open();
                //ConnectionState conState = conn.State;
                //string st23w = " Delete from Securitykeyforsilentpages where serverid=" + ViewState["mm1"].ToString() + "";
                //SqlCommand cmd23w = new SqlCommand(st23w, conn);
                
                //cmd23w.ExecuteNonQuery();
                //string satsrvencryky = RandomeIntnumber(20);

                try
                {
                    SqlCommand cmdsecuritykey = new SqlCommand("update Securitykeyforsilentpages set  Securitykey1='" + TextBox19.Text + "' ,Securitykey2='" + TextBox20.Text + "',Securitykey3='" + TextBox21.Text + "', Securitykey4='" + TextBox22.Text + "' ,Securitykey5='" + TextBox23.Text + "',Securitykey6='" + TextBox24.Text + "',Securitykey7='" + TextBox25.Text + "',Securitykey8='" + TextBox26.Text + "',Securitykey9='" + TextBox27.Text + "',Securitykey10='" + TextBox28.Text + "' where serverid='" + ViewState["mm1"].ToString() + "' ", conn);
                    if (conn.State.ToString() != "Open")
                    {
                        // conn.Open();
                    }
                    cmdsecuritykey.ExecuteNonQuery();
                    conn.Close();

                    SqlCommand cmdsecuritykey1 = new SqlCommand(" update Securitykeyforsilentpages set  Securitykey1='" + TextBox19.Text + "' ,Securitykey2='" + TextBox20.Text + "',Securitykey3='" + TextBox21.Text + "', Securitykey4='" + TextBox22.Text + "' ,Securitykey5='" + TextBox23.Text + "',Securitykey6='" + TextBox24.Text + "',Securitykey7='" + TextBox25.Text + "',Securitykey8='" + TextBox26.Text + "',Securitykey9='" + TextBox27.Text + "',Securitykey10='" + TextBox28.Text + "' where serverid='" + ViewState["mm1"].ToString() + "' ", con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdsecuritykey1.ExecuteNonQuery();
                    con.Close();



                    //SqlCommand cmdsecuritykey = new SqlCommand("insertsecuritykey", conn);
                    //cmdsecuritykey.CommandType = CommandType.StoredProcedure;
                    //cmdsecuritykey.Parameters.AddWithValue("@satsrvencryky", satsrvencryky.ToString());
                    //cmdsecuritykey.Parameters.AddWithValue("@Securitykey1", TextBox19.Text);
                    //cmdsecuritykey.Parameters.AddWithValue("@Dynamiccontent1", DropDownList2.SelectedValue);
                    //cmdsecuritykey.Parameters.AddWithValue("@Date1", DropDownList3.SelectedValue);
                    //cmdsecuritykey.Parameters.AddWithValue("@Securitykey2", TextBox20.Text);
                    //cmdsecuritykey.Parameters.AddWithValue("@Dynamiccontent2", DropDownList4.SelectedValue);
                    //cmdsecuritykey.Parameters.AddWithValue("@Date2", DropDownList5.SelectedValue);
                    //cmdsecuritykey.Parameters.AddWithValue("@Securitykey3", TextBox21.Text);
                    //cmdsecuritykey.Parameters.AddWithValue("@Dynamiccontent3", DropDownList6.SelectedValue);
                    //cmdsecuritykey.Parameters.AddWithValue("@Date3", DropDownList7.SelectedValue);
                    //cmdsecuritykey.Parameters.AddWithValue("@Securitykey4", TextBox22.Text);
                    //cmdsecuritykey.Parameters.AddWithValue("@Dynamiccontent4", DropDownList8.SelectedValue);
                    //cmdsecuritykey.Parameters.AddWithValue("@Date4", DropDownList9.SelectedValue);
                    //cmdsecuritykey.Parameters.AddWithValue("@Securitykey5", TextBox23.Text);
                    //cmdsecuritykey.Parameters.AddWithValue("@Dynamiccontent5", DropDownList10.SelectedValue);
                    //cmdsecuritykey.Parameters.AddWithValue("@Date5", DropDownList11.SelectedValue);
                    //cmdsecuritykey.Parameters.AddWithValue("@Securitykey6", TextBox24.Text);
                    //cmdsecuritykey.Parameters.AddWithValue("@Dynamiccontent6", DropDownList12.SelectedValue);
                    //cmdsecuritykey.Parameters.AddWithValue("@Date6", DropDownList13.SelectedValue);
                    //cmdsecuritykey.Parameters.AddWithValue("@Securitykey7", TextBox25.Text);
                    //cmdsecuritykey.Parameters.AddWithValue("@Dynamiccontent7", DropDownList14.SelectedValue);
                    //cmdsecuritykey.Parameters.AddWithValue("@Date7", DropDownList15.SelectedValue);
                    //cmdsecuritykey.Parameters.AddWithValue("@Securitykey8", TextBox26.Text);
                    //cmdsecuritykey.Parameters.AddWithValue("@Dynamiccontent8", DropDownList16.SelectedValue);
                    //cmdsecuritykey.Parameters.AddWithValue("@Date8", DropDownList17.SelectedValue);
                    //cmdsecuritykey.Parameters.AddWithValue("@Securitykey9", TextBox27.Text);
                    //cmdsecuritykey.Parameters.AddWithValue("@Dynamiccontent9", DropDownList18.SelectedValue);
                    //cmdsecuritykey.Parameters.AddWithValue("@Date9", DropDownList19.SelectedValue);
                    //cmdsecuritykey.Parameters.AddWithValue("@Securitykey10", TextBox28.Text);
                    //cmdsecuritykey.Parameters.AddWithValue("@Dynamiccontent10", DropDownList20.SelectedValue);
                    //cmdsecuritykey.Parameters.AddWithValue("@Date10", DropDownList21.SelectedValue);
                    //cmdsecuritykey.Parameters.AddWithValue("@serverid", ViewState["mm1"].ToString());
                    //cmdsecuritykey.ExecuteNonQuery();
                    //conn.Close();

                    //con.Open();
                    //SqlCommand cmdsecuritykey1 = new SqlCommand("insertsecuritykey", con);
                    //cmdsecuritykey1.CommandType = CommandType.StoredProcedure;
                    //cmdsecuritykey1.Parameters.AddWithValue("@satsrvencryky", satsrvencryky.ToString());
                    //cmdsecuritykey1.Parameters.AddWithValue("@Securitykey1", TextBox19.Text);
                    //cmdsecuritykey1.Parameters.AddWithValue("@Dynamiccontent1", DropDownList2.SelectedValue);
                    //cmdsecuritykey1.Parameters.AddWithValue("@Date1", DropDownList3.SelectedValue);
                    //cmdsecuritykey1.Parameters.AddWithValue("@Securitykey2", TextBox20.Text);
                    //cmdsecuritykey1.Parameters.AddWithValue("@Dynamiccontent2", DropDownList4.SelectedValue);
                    //cmdsecuritykey1.Parameters.AddWithValue("@Date2", DropDownList5.SelectedValue);
                    //cmdsecuritykey1.Parameters.AddWithValue("@Securitykey3", TextBox21.Text);
                    //cmdsecuritykey1.Parameters.AddWithValue("@Dynamiccontent3", DropDownList6.SelectedValue);
                    //cmdsecuritykey1.Parameters.AddWithValue("@Date3", DropDownList7.SelectedValue);
                    //cmdsecuritykey1.Parameters.AddWithValue("@Securitykey4", TextBox22.Text);
                    //cmdsecuritykey1.Parameters.AddWithValue("@Dynamiccontent4", DropDownList8.SelectedValue);
                    //cmdsecuritykey1.Parameters.AddWithValue("@Date4", DropDownList9.SelectedValue);
                    //cmdsecuritykey1.Parameters.AddWithValue("@Securitykey5", TextBox23.Text);
                    //cmdsecuritykey1.Parameters.AddWithValue("@Dynamiccontent5", DropDownList10.SelectedValue);
                    //cmdsecuritykey1.Parameters.AddWithValue("@Date5", DropDownList11.SelectedValue);
                    //cmdsecuritykey1.Parameters.AddWithValue("@Securitykey6", TextBox24.Text);
                    //cmdsecuritykey1.Parameters.AddWithValue("@Dynamiccontent6", DropDownList12.SelectedValue);
                    //cmdsecuritykey1.Parameters.AddWithValue("@Date6", DropDownList13.SelectedValue);
                    //cmdsecuritykey1.Parameters.AddWithValue("@Securitykey7", TextBox25.Text);
                    //cmdsecuritykey1.Parameters.AddWithValue("@Dynamiccontent7", DropDownList14.SelectedValue);
                    //cmdsecuritykey1.Parameters.AddWithValue("@Date7", DropDownList15.SelectedValue);
                    //cmdsecuritykey1.Parameters.AddWithValue("@Securitykey8", TextBox26.Text);
                    //cmdsecuritykey1.Parameters.AddWithValue("@Dynamiccontent8", DropDownList16.SelectedValue);
                    //cmdsecuritykey1.Parameters.AddWithValue("@Date8", DropDownList17.SelectedValue);
                    //cmdsecuritykey1.Parameters.AddWithValue("@Securitykey9", TextBox27.Text);
                    //cmdsecuritykey1.Parameters.AddWithValue("@Dynamiccontent9", DropDownList18.SelectedValue);
                    //cmdsecuritykey1.Parameters.AddWithValue("@Date9", DropDownList19.SelectedValue);
                    //cmdsecuritykey1.Parameters.AddWithValue("@Securitykey10", TextBox28.Text);
                    //cmdsecuritykey1.Parameters.AddWithValue("@Dynamiccontent10", DropDownList20.SelectedValue);
                    //cmdsecuritykey1.Parameters.AddWithValue("@Date10", DropDownList21.SelectedValue);
                    //cmdsecuritykey1.Parameters.AddWithValue("@serverid", ViewState["mm1"].ToString());
                    //cmdsecuritykey1.ExecuteNonQuery();
                    //con.Close();

                    lblmsg.Visible = true;
                    lblmsg.Text = "Record updated Successfully";


                }
                catch (Exception ex)
                {
                    lblmsg.Visible = true;
                    lblmsg.Text =ex.ToString();


                }


            }


            FillProductMasterindividual();
            fillgrid1();
            clear();
            addnewpanel.Visible = true;
            pnladdnew.Visible = false;
        }

    }
    public static string RandomeIntnumber(int passwordLength)
    {
        string allowedChars = "1234567890QWERTYUIOPLKJHGFDSAZXCVBNM";

        char[] chars = new char[passwordLength];
        Random rd = new Random();
        for (int i = 0; i < passwordLength; i++)
        {
            chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
        }
        return new string(chars);
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
        TextBox4.Text = "";
        TextBox9.Text = "";
        TextBox14.Text = "";
        txtDatabaseName.Text = "";
        txtBusiwizsatellitesiteurl.Text = "";
        txtSqlinstancename.Text = "";
        txtDatabaseServerurl.Text = "";
        txtDBUserId.Text = "";
        txtDBPassword.Text = "";
        txtDatabaseAccessPort.Text = "";
        TextBox5.Text = "";
        TextBox6.Text = "";
        TextBox8.Text = "";
        TextBox10.Text = "";
        TextBox11.Text = "";
        TextBox12.Text = "";
        TextBox13.Text = "";
        TextBox15.Text = "";
        TextBox16.Text = "";
        TextBox17.Text = "";
        TextBox18.Text = "";
        pnladdnew.Visible = false;
         
        txtNoofcompanycanuse.Text ="0";

        chk_ServerMonthlyExclusiveLease.Checked = false; 
        chk_ServerMonthlySharedLease.Checked =false;
        chk_ServersforSell.Checked = false;
       
    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "viewplan")
        {
            int mm1 = Convert.ToInt32(e.CommandArgument);
            SqlDataAdapter da1 = new SqlDataAdapter("select * from ServerMasterTbl where ID=" + mm1 + "", con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                lbl_servername.Text ="List of Company that  register in "+ dt1.Rows[0]["ServerName"].ToString()+" Server";
                ViewState["serid"] = mm1;
                fillPortal();
                fillnewgrid();
            }           
           
        }
        if (e.CommandName == "Delete")
        {
            int mm1 = Convert.ToInt32(e.CommandArgument);
            SqlCommand cmdd1 = new SqlCommand("delete from ServerMasterTbl where Id=" + mm1 +"", con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdd1.ExecuteNonQuery();
            con.Close();


            string st23 = " Delete from Securitykeyforsilentpages where serverid=" + mm1+"";
            SqlCommand cmd23 = new SqlCommand(st23, con);
            con.Open();
            cmd23.ExecuteNonQuery();
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
                txtDatabaseName.Text = dt1.Rows[0]["folderpathformastercode"].ToString();
                TextBox4.Text = dt1.Rows[0]["serverdefaultpathforiis"].ToString();
                TextBox9.Text = dt1.Rows[0]["serverdefaultpathformdf"].ToString();
                TextBox14.Text = dt1.Rows[0]["serverdefaultpathforfdf"].ToString();

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


               // txtDatabaseName.Text = dt1.Rows[0]["Sqlinstancename"].ToString();
               // txtBusiwizsatellitesiteurl.Text = dt1.Rows[0]["FTPforMastercode"].ToString();
                //txtSqlinstancename.Text = dt1.Rows[0]["Sqlinstancename"].ToString();
                txtDatabaseServerurl.Text = dt1.Rows[0]["FTPforMastercode"].ToString();
                txtDBUserId.Text = dt1.Rows[0]["FTPuseridforDefaultIISpath"].ToString();
                txtDBPassword.Text =PageMgmt.Decrypted( dt1.Rows[0]["FtpPasswordforDefaultIISpath"].ToString());
                string strqa2 = txtDBPassword.Text;
                txtDBPassword.Attributes.Add("Value", strqa2);
                txtDatabaseAccessPort.Text = dt1.Rows[0]["FTPportfordefaultIISpath"].ToString();
                TextBox5.Text = dt1.Rows[0]["Default_FTPUrl"].ToString();
                TextBox6.Text = dt1.Rows[0]["Default_FTPUserId"].ToString();
                TextBox7.Text =PageMgmt.Decrypted( dt1.Rows[0]["Default_FTPPassword"].ToString());
                string strqa4 = TextBox7.Text;
                TextBox7.Attributes.Add("Value", strqa4);
                TextBox8.Text = dt1.Rows[0]["Default_FTPPort"].ToString();
                TextBox10.Text = dt1.Rows[0]["MDF_FTPUrl"].ToString();
                TextBox11.Text = dt1.Rows[0]["MDF_FTPUserId"].ToString();
                TextBox12.Text =PageMgmt.Decrypted( dt1.Rows[0]["MDF_FTPPassword"].ToString());
                string strqa5 = TextBox12.Text;
                TextBox12.Attributes.Add("Value", strqa5);
                TextBox13.Text = dt1.Rows[0]["MDF_FTPPort"].ToString();
                TextBox15.Text = dt1.Rows[0]["LDF_FTPUrl"].ToString();
                TextBox16.Text = dt1.Rows[0]["LDF_FTPUserId"].ToString();
                TextBox17.Text =PageMgmt.Decrypted( dt1.Rows[0]["LDF_FTPPassword"].ToString());
                string strqa6 = TextBox17.Text;
                TextBox17.Attributes.Add("Value", strqa6);
                TextBox18.Text = dt1.Rows[0]["LDF_FTPPort"].ToString();


                if (dt1.Rows[0]["Status"] == "1")
                {
                    ckbactive.Checked = true;

                }
                else
                {
                    ckbactive.Checked = false;

                }


                if (dt1.Rows[0]["DNS"] == "False")
                {
                    CheckBox1.Checked = false;

                }
                else
                {
                    CheckBox1.Checked = true;

                }






                SqlDataAdapter da2 = new SqlDataAdapter("select * from Securitykeyforsilentpages where serverid=" + ViewState["mm1"] + "", con);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                if(dt2.Rows.Count>0)
                {
                    TextBox19.Text = dt2.Rows[0]["Securitykey1"].ToString();
                    TextBox20.Text = dt2.Rows[0]["Securitykey2"].ToString();
                    TextBox21.Text = dt2.Rows[0]["Securitykey3"].ToString();
                    TextBox22.Text = dt2.Rows[0]["Securitykey4"].ToString();
                    TextBox23.Text = dt2.Rows[0]["Securitykey5"].ToString();
                    TextBox24.Text = dt2.Rows[0]["Securitykey6"].ToString();
                    TextBox25.Text = dt2.Rows[0]["Securitykey7"].ToString();
                    TextBox26.Text = dt2.Rows[0]["Securitykey8"].ToString();
                    TextBox27.Text = dt2.Rows[0]["Securitykey9"].ToString();
                    TextBox28.Text = dt2.Rows[0]["Securitykey10"].ToString();
                    DropDownList2.SelectedValue = dt2.Rows[0]["Dynamiccontent1"].ToString();
                    DropDownList3.SelectedValue = dt2.Rows[0]["Date1"].ToString();
                    DropDownList4.SelectedValue = dt2.Rows[0]["Dynamiccontent2"].ToString();
                    DropDownList5.SelectedValue = dt2.Rows[0]["Date2"].ToString();
                    DropDownList6.SelectedValue = dt2.Rows[0]["Dynamiccontent3"].ToString();
                    DropDownList7.SelectedValue = dt2.Rows[0]["Date3"].ToString();
                    DropDownList8.SelectedValue = dt2.Rows[0]["Dynamiccontent4"].ToString();
                    DropDownList9.SelectedValue = dt2.Rows[0]["Date4"].ToString();
                    DropDownList10.SelectedValue = dt2.Rows[0]["Dynamiccontent5"].ToString();
                    DropDownList11.SelectedValue = dt2.Rows[0]["Date5"].ToString();
                    DropDownList12.SelectedValue = dt2.Rows[0]["Dynamiccontent6"].ToString();
                    DropDownList13.SelectedValue = dt2.Rows[0]["Date6"].ToString();
                    DropDownList14.SelectedValue = dt2.Rows[0]["Dynamiccontent7"].ToString();
                    DropDownList15.SelectedValue = dt2.Rows[0]["Date7"].ToString();

                    DropDownList16.SelectedValue = dt2.Rows[0]["Dynamiccontent8"].ToString();
                    DropDownList17.SelectedValue = dt2.Rows[0]["Date8"].ToString();
                    DropDownList18.SelectedValue = dt2.Rows[0]["Dynamiccontent9"].ToString();
                    DropDownList19.SelectedValue = dt2.Rows[0]["Date9"].ToString();
                    DropDownList20.SelectedValue = dt2.Rows[0]["Dynamiccontent10"].ToString();
                    DropDownList21.SelectedValue = dt2.Rows[0]["Date10"].ToString();
                }


              

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
                  RblServerType.SelectedValue  = dt1.Rows[0]["ServerType"].ToString();
                   
                }
                catch (Exception ex)
                {
                   
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
    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }
    protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
    {
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblserID = (Label)e.Row.FindControl("lblserID");
            LinkButton linl_totalnoofcompany=(LinkButton)e.Row .FindControl("linl_totalnoofcompany"); 
            SqlDataAdapter da1 = new SqlDataAdapter("select count(CompanyId) as totalcompany From CompanyMaster Where Active=1 and ServerId=" + lblserID.Text + "", con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
                linl_totalnoofcompany.Text = dt1.Rows[0]["totalcompany"].ToString();
            
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
            //encstr = CreateLicenceKey(out HashKey);
            encstr = MyCommonfile.RandomeIntnumber(20);
            // string satelliteurul = txtBusiwizsatellitesiteurl.Text + ".safestserver.com";
            string satelliteurul = txtBusiwizsatellitesiteurl.Text;

            string sqlsatelliteurul = txtcomputername.Text + "_SQL.safestserver.com";

            string SubMenuInsert = "Insert Into ServerMasterTbl (ServerName,ServerComputerFullName,serverloction,serverdetail,Busiwizsatellitesiteurl,folderpathformastercode,serverdefaultpathforiis,serverdefaultpathformdf,serverdefaultpathforfdf,BusicontrolUserId,BusicontrolPassword,PublicIp,Ipaddress,Sqlinstancename,port,Sapassword,DefaultCreated,Status,BusiwizsimpleUserUserID,BusiwizsimpleUserPassword,DefaultMdfpath,DefaultLdfpath,DefaultDatabaseName,DefaultsqlInstance,MacAddress,ComputerName,InDomain,DomainName,DomainGroupName,Enckey,sqlurl,FTPurl,FTPPort,FTPUserId,FTPPassword,Name,HomePhone,MobileName,Email,CountryID,CarrierID,DateCreated,PortforCompanymastersqlistance,SqlServerName,ProductMasterindividualID ,IsLeaseServer,MaxCompSharing,IsSharedServer,ISSaleServer ,ServerType ,MaxCommonCompanyShared,FTPforMastercode,FTPuseridforDefaultIISpath,FtpPasswordforDefaultIISpath,FTPportfordefaultIISpath,MDF_FTPUrl,MDF_FTPUserId,MDF_FTPPassword,MDF_FTPPort,LDF_FTPUrl,LDF_FTPUserId,LDF_FTPPassword,LDF_FTPPort,Default_FTPUrl,Default_FTPUserId,Default_FTPPassword,Default_FTPPort,DNS) " +
            " values ('" + txtServerName.Text + "','" + txtservercomputerfullname.Text + "','" + txtserverloction.Text + "','" + txtserverdetail.Text + "','" + satelliteurul + "','" + txtDatabaseName.Text + "','" + TextBox4.Text + "','" + TextBox9.Text + "','" + TextBox14.Text + "','" + txtUserId.Text + "','" + PageMgmt.Encrypted(txtwindowpassword.Text) + "','" + txtpubip.Text + "','" + txtIpaddress.Text + "','" + txtSqlinstancename.Text + "','" + txtport.Text + "','" + PageMgmt.Encrypted(txtSapassword.Text) + "','" + RadioButtonList1.SelectedValue + "','" + ddlstatus.SelectedValue + "','" + txtsimplebusiwizuser.Text + "','" + PageMgmt.Encrypted(txtsimpleuserpassword.Text) + "','" + TextBox9.Text + "','" + TextBox14.Text + "','" + txtdefaultdatabasename.Text + "','" + txtdefaultsqlinstance.Text + "','" + txtmacaddress.Text + "','" + txtcomputername.Text + "','" + RadioButtonList2.SelectedValue + "','" + txtdomainname.Text + "','" + txtdomaingrpname.Text + "','" + encstr + "','" + sqlsatelliteurul + "','" + txtftpurl.Text + "','" + txtftpport.Text + "','" + txtftpuserid.Text + "','" + PageMgmt.Encrypted(txtftppassword.Text) + "','" + txtname.Text + "','" + txthomephone.Text + "','" + txtmobilephoneadmin.Text + "','" + txtadminemail.Text + "','" + ddlcountry.SelectedValue + "','" + ddlcarriername.SelectedValue + "','" + DateTime.Now.ToShortDateString() + "','" + txtcompnayport.Text + "','" + txtdefaultsqlinstance.Text + "' ,'" + DDLProductMasterindividual.SelectedValue + "' " +
            " ,'" + chk_ServerMonthlyExclusiveLease.Checked + "','" + txtNoofcompanycanuse.Text + "','" + chk_ServerMonthlySharedLease.Checked + "', '" + chk_ServersforSell.Checked + "','" + RblServerType.SelectedValue + "','" + txtMaxNoOfCompany.Text + "','" + txtDatabaseServerurl.Text + "','" + txtDBUserId.Text + "','" + txtDBPassword.Text + "','" + txtDatabaseAccessPort.Text + "','" + TextBox10.Text + "','" + TextBox11.Text + "','" + TextBox12.Text + "','" + TextBox13.Text + "','" + TextBox15.Text + "','" + TextBox16.Text + "','" + TextBox17.Text + "','" + TextBox18.Text + "','" + TextBox5.Text + "','" + TextBox6.Text + "','" + TextBox7.Text + "','" + TextBox8.Text + "','" + CheckBox1.Checked + "')";
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
            int own = 0;
            int common = 0;
            int lease = 0;
            int Shared = 0;
            int Sell = 0;
            if (RblServerType.SelectedValue == "0")
            {
                common = 1015;
            }
            else
            {
                common = 1018;
            }
            string strServerstatusmaster = " Insert Into Serverstatusmaster (SatelliteserverID,DateandTIme,Serversdtatusmasterid)  values ('" + id + "','" + DateTime.Now.ToShortDateString() + "','" + common + "')";
            cmd = new SqlCommand(strServerstatusmaster, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();
            //*****************************************************--------------------------
            //******************************************************------------------
            if (RblServerType.SelectedValue == "2")
            {
                own = 1016;
            }
            else
            {
                own = 1017;
            }
            strServerstatusmaster = " Insert Into Serverstatusmaster (SatelliteserverID,DateandTIme,Serversdtatusmasterid)  values ('" + id + "','" + DateTime.Now.ToShortDateString() + "','" + own + "')";
            cmd = new SqlCommand(strServerstatusmaster, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();
            //*****************************************************--------------------------
            //******************************************************------------------
            if (chk_ServerMonthlyExclusiveLease.Checked == true)
            {
                lease = 5;
            }
            else
            {
                lease = 1013;
            }
            strServerstatusmaster = " Insert Into Serverstatusmaster (SatelliteserverID,DateandTIme,Serversdtatusmasterid)  values ('" + id + "','" + DateTime.Now.ToShortDateString() + "','" + lease + "')";
            cmd = new SqlCommand(strServerstatusmaster, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();
            //*****************************************************--------------------------
            //******************************************************------------------
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
            //*****************************************************--------------------------
            //******************************************************------------------
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
            //*****************************************************--------------------------
            //******************************************************------------------
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


                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
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

            string satsrvencryky = RandomeIntnumber(20);
            string strstate = "  select ClientMaster.ServerId from ClientMaster where ClientMasterId='" + Session["Comid"].ToString()+ "'";
            SqlCommand cmdstate = new SqlCommand(strstate, con);
            DataTable dtstate = new DataTable();
            SqlDataAdapter adpstate = new SqlDataAdapter(cmdstate);
            adpstate.Fill(dtstate);
            {
                string stre = dtstate.Rows[0]["ServerId"].ToString();
                try
                {
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    SqlCommand cmdsecuritykey1 = new SqlCommand("insertsecuritykey", con);
                    cmdsecuritykey1.CommandType = CommandType.StoredProcedure;
                    cmdsecuritykey1.Parameters.AddWithValue("@satsrvencryky", satsrvencryky.ToString());
                    cmdsecuritykey1.Parameters.AddWithValue("@Securitykey1", TextBox19.Text);
                    cmdsecuritykey1.Parameters.AddWithValue("@Dynamiccontent1", DropDownList2.SelectedValue);
                    cmdsecuritykey1.Parameters.AddWithValue("@Date1", DropDownList3.SelectedValue);
                    cmdsecuritykey1.Parameters.AddWithValue("@Securitykey2", TextBox20.Text);
                    cmdsecuritykey1.Parameters.AddWithValue("@Dynamiccontent2", DropDownList4.SelectedValue);
                    cmdsecuritykey1.Parameters.AddWithValue("@Date2", DropDownList5.SelectedValue);
                    cmdsecuritykey1.Parameters.AddWithValue("@Securitykey3", TextBox21.Text);
                    cmdsecuritykey1.Parameters.AddWithValue("@Dynamiccontent3", DropDownList6.SelectedValue);
                    cmdsecuritykey1.Parameters.AddWithValue("@Date3", DropDownList7.SelectedValue);
                    cmdsecuritykey1.Parameters.AddWithValue("@Securitykey4", TextBox22.Text);
                    cmdsecuritykey1.Parameters.AddWithValue("@Dynamiccontent4", DropDownList8.SelectedValue);
                    cmdsecuritykey1.Parameters.AddWithValue("@Date4", DropDownList9.SelectedValue);
                    cmdsecuritykey1.Parameters.AddWithValue("@Securitykey5", TextBox23.Text);
                    cmdsecuritykey1.Parameters.AddWithValue("@Dynamiccontent5", DropDownList10.SelectedValue);
                    cmdsecuritykey1.Parameters.AddWithValue("@Date5", DropDownList11.SelectedValue);
                    cmdsecuritykey1.Parameters.AddWithValue("@Securitykey6", TextBox24.Text);
                    cmdsecuritykey1.Parameters.AddWithValue("@Dynamiccontent6", DropDownList12.SelectedValue);
                    cmdsecuritykey1.Parameters.AddWithValue("@Date6", DropDownList13.SelectedValue);
                    cmdsecuritykey1.Parameters.AddWithValue("@Securitykey7", TextBox25.Text);
                    cmdsecuritykey1.Parameters.AddWithValue("@Dynamiccontent7", DropDownList14.SelectedValue);
                    cmdsecuritykey1.Parameters.AddWithValue("@Date7", DropDownList15.SelectedValue);
                    cmdsecuritykey1.Parameters.AddWithValue("@Securitykey8", TextBox26.Text);
                    cmdsecuritykey1.Parameters.AddWithValue("@Dynamiccontent8", DropDownList16.SelectedValue);
                    cmdsecuritykey1.Parameters.AddWithValue("@Date8", DropDownList17.SelectedValue);
                    cmdsecuritykey1.Parameters.AddWithValue("@Securitykey9", TextBox27.Text);
                    cmdsecuritykey1.Parameters.AddWithValue("@Dynamiccontent9", DropDownList18.SelectedValue);
                    cmdsecuritykey1.Parameters.AddWithValue("@Date9", DropDownList19.SelectedValue);
                    cmdsecuritykey1.Parameters.AddWithValue("@Securitykey10", TextBox28.Text);
                    cmdsecuritykey1.Parameters.AddWithValue("@Dynamiccontent10", DropDownList20.SelectedValue);
                    cmdsecuritykey1.Parameters.AddWithValue("@Date10", DropDownList21.SelectedValue);
                    cmdsecuritykey1.Parameters.AddWithValue("@serverid", id);
                    cmdsecuritykey1.ExecuteNonQuery();
                    con.Close();
                }
                catch
                {
                }
                ConnectionState conState = conn.State;
                try
                {
                    //                    DataTable dtconn = select(@"SELECT ServerMasterTbl.ServerName,ServerMasterTbl.port,ServerMasterTbl.sqlserveruserid,ServerMasterTbl.Sapassword,Instancename 
                    //                                     FROM ServerMasterTbl INNER JOIN ClientMaster ON ServerMasterTbl.Id = ClientMaster.ServerId inner join ProductMaster on ProductMaster.ClientID=ClientMaster.ClientMasterId
                    //                                     inner join VersionInfoMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId inner join CodeTypeTbl on CodeTypeTbl.ProductVersionId=VersionInfoMaster.VersionInfoId  where ServerMasterTbl.Id='" + stre + "'");
                    //                    if (dtconn.Rows.Count > 0)
                    conn = new SqlConnection();

                    conn = ServerWizard.ServerDefaultInstanceConnetionTCP_Serverid(id.ToString());

                    //conn.ConnectionString = @"Data Source =" + dtconn.Rows[0]["ServerName"] + "\\" + dtconn.Rows[0]["Instancename"] + "," + dtconn.Rows[0]["port"] + "; Initial Catalog=C3SATELLITESERVER; User ID=sa; Password=" + PageMgmt.Decrypted(dtconn.Rows[0]["Sapassword"].ToString()) + "; Persist Security Info=true;";
                    //conn.ConnectionString = @"Data Source =TCP:" + dtconn.Rows[0]["ipaddress"] + "," + dtconn.Rows[0]["port"] + "; Initial Catalog=" + dtconn.Rows[0]["DefaultsqlInstance"] + "; User ID=sa; Password=" + PageMgmt.Decrypted(dtconn.Rows[0]["Sapassword"].ToString()) + "; Persist Security Info=true;";                   
                    //if (conn.State.ToString() != "Open")
                    {
                        conn.Open();
                    }
                    SqlCommand cmdsecuritykey = new SqlCommand("insertsecuritykey", conn);
                    cmdsecuritykey.CommandType = CommandType.StoredProcedure;
                    cmdsecuritykey.Parameters.AddWithValue("@satsrvencryky", satsrvencryky.ToString());
                    cmdsecuritykey.Parameters.AddWithValue("@Securitykey1", TextBox19.Text);
                    cmdsecuritykey.Parameters.AddWithValue("@Dynamiccontent1", DropDownList2.SelectedValue);
                    cmdsecuritykey.Parameters.AddWithValue("@Date1", DropDownList3.SelectedValue);
                    cmdsecuritykey.Parameters.AddWithValue("@Securitykey2", TextBox20.Text);
                    cmdsecuritykey.Parameters.AddWithValue("@Dynamiccontent2", DropDownList4.SelectedValue);
                    cmdsecuritykey.Parameters.AddWithValue("@Date2", DropDownList5.SelectedValue);
                    cmdsecuritykey.Parameters.AddWithValue("@Securitykey3", TextBox21.Text);
                    cmdsecuritykey.Parameters.AddWithValue("@Dynamiccontent3", DropDownList6.SelectedValue);
                    cmdsecuritykey.Parameters.AddWithValue("@Date3", DropDownList7.SelectedValue);
                    cmdsecuritykey.Parameters.AddWithValue("@Securitykey4", TextBox22.Text);
                    cmdsecuritykey.Parameters.AddWithValue("@Dynamiccontent4", DropDownList8.SelectedValue);
                    cmdsecuritykey.Parameters.AddWithValue("@Date4", DropDownList9.SelectedValue);
                    cmdsecuritykey.Parameters.AddWithValue("@Securitykey5", TextBox23.Text);
                    cmdsecuritykey.Parameters.AddWithValue("@Dynamiccontent5", DropDownList10.SelectedValue);
                    cmdsecuritykey.Parameters.AddWithValue("@Date5", DropDownList11.SelectedValue);
                    cmdsecuritykey.Parameters.AddWithValue("@Securitykey6", TextBox24.Text);
                    cmdsecuritykey.Parameters.AddWithValue("@Dynamiccontent6", DropDownList12.SelectedValue);
                    cmdsecuritykey.Parameters.AddWithValue("@Date6", DropDownList13.SelectedValue);
                    cmdsecuritykey.Parameters.AddWithValue("@Securitykey7", TextBox25.Text);
                    cmdsecuritykey.Parameters.AddWithValue("@Dynamiccontent7", DropDownList14.SelectedValue);
                    cmdsecuritykey.Parameters.AddWithValue("@Date7", DropDownList15.SelectedValue);
                    cmdsecuritykey.Parameters.AddWithValue("@Securitykey8", TextBox26.Text);
                    cmdsecuritykey.Parameters.AddWithValue("@Dynamiccontent8", DropDownList16.SelectedValue);
                    cmdsecuritykey.Parameters.AddWithValue("@Date8", DropDownList17.SelectedValue);
                    cmdsecuritykey.Parameters.AddWithValue("@Securitykey9", TextBox27.Text);
                    cmdsecuritykey.Parameters.AddWithValue("@Dynamiccontent9", DropDownList18.SelectedValue);
                    cmdsecuritykey.Parameters.AddWithValue("@Date9", DropDownList19.SelectedValue);
                    cmdsecuritykey.Parameters.AddWithValue("@Securitykey10", TextBox28.Text);
                    cmdsecuritykey.Parameters.AddWithValue("@Dynamiccontent10", DropDownList20.SelectedValue);
                    cmdsecuritykey.Parameters.AddWithValue("@Date10", DropDownList21.SelectedValue);
                    cmdsecuritykey.Parameters.AddWithValue("@serverid", id);
                    cmdsecuritykey.ExecuteNonQuery();
                    conn.Close();
                    lblmsg.Visible = true;

                }
                catch
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "Record not Inserted Successfully";
                }
            }
            lblmsg.Text = "Record Inserted Successfully";
            //lblmsg.Visible = true;
            //lblmsg.Text = "Record Inserted Successfully";
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
        }
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
        if (txtftpurl.Text != "")
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
                    pnlshared.Visible = true;
                }
                else
                {                  
                    pnlshared.Visible = false;
                    txtNoofcompanycanuse.Text = "0";
                }
                if (lblroleid.Text == "1")
                {
                  
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
            txtMaxNoOfCompany.Text = "0";
            txtMaxNoOfCompany.Text = "0";
        }
        else if (RblServerType.SelectedValue == "0")
        {
            txtMaxNoOfCompany.Text = "0";
            txtMaxNoOfCompany.Text = "0";
            txtMaxNoOfCompany.Visible = true;
            pnlLeasSharedSale.Visible = false;
            chk_ServerMonthlyExclusiveLease.Checked = false;
            chk_ServerMonthlySharedLease.Checked = false;
            chk_ServersforSell.Checked = false;
        }
        else
        {
            txtMaxNoOfCompany.Visible = false;
            
            txtMaxNoOfCompany.Text = "";
            txtMaxNoOfCompany.Text = "";
            pnlLeasSharedSale.Visible = false;
            chk_ServerMonthlyExclusiveLease.Checked = false;
            chk_ServerMonthlySharedLease.Checked = false;
            chk_ServersforSell.Checked = false;
        }
    }


    //---------Fill Popup Company active List
    public void fillPortal()
    {
        //Status
        string status = "";
        if (ddlfilters.SelectedIndex > 0)
        {
            status = " and Status=" + ddlfilters.SelectedValue + " ";
        }
        string str = " select * from PortalMasterTbl Where PortalMasterTbl.PortalName !='' " + status + " ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        if (ds.Rows.Count > 0)
        {
            Hashtable hTable = new Hashtable();
            ArrayList duplicateList = new ArrayList();
            foreach (DataRow dtRow in ds.Rows)
            {
                if (hTable.Contains(dtRow["PortalName"]))
                    duplicateList.Add(dtRow);
                else
                    hTable.Add(dtRow["PortalName"], string.Empty);
            }
            foreach (DataRow dtRow in duplicateList)
                ds.Rows.Remove(dtRow);


            if (ds.Rows.Count > 0)
            {
                ddlportal.DataSource = ds;
                ddlportal.DataTextField = "PortalName";
                ddlportal.DataValueField = "Id";
                ddlportal.DataBind();
                ddlportal.Items.Insert(0, "---Select All---");
            }
        }
    }
    protected void ddlActive_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillnewgrid();
    }
    protected void ddlportal_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillPricePlan();
        fillnewgrid(); 
    }
    public void fillPricePlan()
    {
        // CompanyMaster.active='1' and PricePlanMaster.active='1' 
        string status = "";
        if (ddlfilters.SelectedIndex > 0)
        {
            status = " and PricePlanMaster.active=" + ddlfilters.SelectedValue + " ";
        }
        if (ddlportal.SelectedIndex > 0)
        {
            ddlsortPlan.Items.Clear();

            string str22 = " select  CompanyMaster.*,CompanyMaster.active,PricePlanMaster.active,CompanyMaster.PricePlanId,PricePlanMaster.PricePlanName AS planname,PricePlanMaster.PricePlanId,PortalMasterTbl.PortalName,PortalMasterTbl.ProductId,ProductMaster.ProductId, CompanyMaster.OrderId ,CompanyMaster.CompanyId ,PricePlanMaster.PricePlanName,LicenseMaster.LicenseKey," + "CASE CompanyMaster.HostId WHEN 1 THEN 'User Server' ELSE 'busiwiz Server' END AS HostName" + "  from CompanyMaster inner join ProductMaster on ProductMaster.ProductId = CompanyMaster.ProductId inner join PortalMasterTbl on PortalMasterTbl.ProductId = ProductMaster.ProductId inner join PricePlanMaster on PricePlanMaster.pricePlanId = CompanyMaster.PricePlanId inner join OrderMaster on OrderMaster.OrderId  =CompanyMaster.OrderId inner join LicenseMaster on LicenseMasterId=CompanyMaster.CompanyId Where   PortalMasterTbl.Id ='" + ddlportal.SelectedValue.ToString() + "' " + status + " ";
            str22 = "select * from PricePlanMaster  inner join PortalMasterTbl on PortalMasterTbl.Id= PortalMasterId1  Where PortalMasterTbl.Id ='" + ddlportal.SelectedValue.ToString() + "'  " + status + "";
            SqlCommand cmd = new SqlCommand(str22, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            adp.Fill(ds);
            if (ds.Rows.Count > 0)
            {
                Hashtable hTable = new Hashtable();
                ArrayList duplicateList = new ArrayList();
                foreach (DataRow dtRow in ds.Rows)
                {
                    if (hTable.Contains(dtRow["PricePlanName"]))
                        duplicateList.Add(dtRow);
                    else
                        hTable.Add(dtRow["PricePlanName"], string.Empty);
                }
                foreach (DataRow dtRow in duplicateList)
                    ds.Rows.Remove(dtRow);
                if (ds.Rows.Count > 0)
                {
                    ddlsortPlan.DataSource = ds;
                    ddlsortPlan.DataTextField = "PricePlanName";
                    ddlsortPlan.DataValueField = "PricePlanId";
                    ddlsortPlan.DataBind();
                    ddlsortPlan.Items.Insert(0, "---Select All---");
                }
            }
        }
        else
        {
            ddlsortPlan.Items.Clear();
            ddlsortPlan.Items.Insert(0, "---Select All---");
        }
    }
  
    protected void ddllicensestart_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddllicensestart.SelectedValue == "5")
        {
            pnllicensedate.Visible = true;
        }
        else
        {
            pnllicensedate.Visible = false;
            fillnewgrid();
        }
        ModalPopupExtender1.Show();
    }
    public void fillnewgrid()
    {
        ModalPopupExtender1.Show();
        string st = "";
        string status = "";
        if (ddlfilters.SelectedIndex > 0)
        {
            st += " and CompanyMaster.active=" + ddlfilters.SelectedValue + " ";
        }

        if (ddlportal.SelectedIndex > 0)
        {
            st += " and PortalMasterTbl.Id ='" + ddlportal.SelectedValue.ToString() + "' ";
        }

        if (ddlsortPlan.SelectedIndex > 0)
        {
            st += " and PricePlanMaster.PricePlanId='" + ddlsortPlan.SelectedValue + "' ";
        }
       
        if (txtsortsearch.Text != "")
        {
            st += " and ( CompanyMaster.CompanyName LIKE '%" + txtsortsearch.Text + "%' OR  CompanyMaster.CompanyLoginId LIKE '%" + txtsortsearch.Text + "%' ) ";
        }
      

        //-----------------License Date----------------------------------------------------------

        if (ddllicensestart.SelectedItem.Text == "Today")
        {
            st += " and dbo.LicenseMaster.LicenseDate = '" + System.DateTime.Now.ToShortDateString() + "'";
        }
        if (ddllicensestart.SelectedItem.Text == "This Week")
        {
            st += " and dbo.LicenseMaster.LicenseDate between '" + ViewState["thisweekstart"] + "' and '" + ViewState["thisweekend"] + "'";
        }
        if (ddllicensestart.SelectedItem.Text == "This Month")
        {
            st += " and dbo.LicenseMaster.LicenseDate between '" + ViewState["thismonthstartdate"] + "' and '" + ViewState["thismonthenddate"] + "'";
        }
        if (ddllicensestart.SelectedItem.Text == "This Year")
        {
            st += " and dbo.LicenseMaster.LicenseDate between '" + ViewState["thisyearstartdate"] + "' and '" + ViewState["thisyearenddate"] + "'";
        }
        //---------------------------------------------------------------------------
        if (TextBox1.Text != "" && TextBox2.Text != "")
        {

            st += " and (dbo.LicenseMaster.LicenseDate between '" + Convert.ToDateTime(TextBox2.Text).ToShortDateString() + "' and '" + Convert.ToDateTime(TextBox3.Text).ToShortDateString() + "')";
        }

        if (ddlActive.SelectedIndex > 0)
        {
            st += " and CompanyMaster.active='" + ddlActive.SelectedValue + "' ";
        }
        st += "  and  CompanyMaster.ServerId='" + ViewState["serid"] + "' ";
       
        string str = "  "; // ";
        str = @"  SELECT        dbo.ProductMaster.ProductId, dbo.ProductMaster.ClientMasterId, dbo.ProductMaster.ProductName, CompanyMaster.ServerId,dbo.PricePlanMaster.PricePlanId,  dbo.PricePlanMaster.PricePlanName, dbo.PricePlanMaster.active, dbo.PricePlanMaster.amountperOrder, dbo.CompanyMaster.CompanyName,  dbo.CompanyMaster.Email, dbo.CompanyMaster.CompanyId, dbo.CompanyMaster.CompanyLoginId,dbo.CompanyMaster.ContactPerson,dbo.CompanyMaster.phone, dbo.PortalMasterTbl.Id, dbo.PortalMasterTbl.PortalName, dbo.LicenseMaster.LicenseDate, dbo.LicenseMaster.LicensePeriod  FROM dbo.CompanyMaster INNER JOIN dbo.ProductMaster INNER JOIN dbo.PricePlanMaster ON dbo.ProductMaster.ProductId = dbo.PricePlanMaster.ProductId ON  dbo.CompanyMaster.PricePlanId = dbo.PricePlanMaster.PricePlanId INNER JOIN dbo.PortalMasterTbl ON dbo.PricePlanMaster.PortalMasterId1 = dbo.PortalMasterTbl.Id INNER JOIN dbo.LicenseMaster ON dbo.CompanyMaster.CompanyId = dbo.LicenseMaster.CompanyId where dbo.CompanyMaster.CompanyLoginId !='' " + st + " and dbo.PortalMasterTbl.CompanyCreationOption='1'  ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        GV_companyshow.DataSource = ds;
        GV_companyshow.DataBind();

    }
    protected void GV_companyshow_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GV_companyshow_RowEditing1(object sender, GridViewEditEventArgs e)
    {

    }
    protected void GV_companyshow_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
           
        }
      

        if (e.CommandName == "ReceivePayment")
        {
            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            int CompanyLoginId = Convert.ToInt32(GridView1.SelectedDataKey.Value);
            // Label Orderid = (Label)GridView1.Rows[GridView1.SelectedIndex].FindControl("lblOrderId");
            // Session["Orderid"] = Orderid.Text;
            //   DeletewebsiteAttach(Request.QueryString["cid"].ToString());   
        }
    }
    protected void GV_companyshow_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_noofday = (Label)e.Row.FindControl("lbl_noofday");
                Label lbllicensedathide = (Label)e.Row.FindControl("lbllicensedathide");
                Label lbl_enddate = (Label)e.Row.FindControl("lbl_enddate");
                DateTime dt = Convert.ToDateTime(lbllicensedathide.Text);
                // As String-----------------------------
                int noofday = Convert.ToInt32(lbl_noofday.Text);
                lbl_enddate.Text = dt.AddDays(noofday).ToString("MM/dd/yyyy");
                string todaydate = System.DateTime.Now.ToShortDateString();
                if (Convert.ToDateTime(lbl_enddate.Text) < Convert.ToDateTime(todaydate))
                {
                    // lbl_enddate.CssClass = "btnFillGreen";
                    lbl_enddate.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

    }
    protected void GV_companyshow_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GV_companyshow.PageIndex = e.NewPageIndex;
        fillnewgrid();
    }
    protected void GV_companyshow_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        // fillgrid();
        fillnewgrid();
    }
 
    protected void filldatebyperiod()
    {
        string Today, Yesterday, ThisYear;
        Today = Convert.ToString(System.DateTime.Today.ToShortDateString());
        Yesterday = Convert.ToString(System.DateTime.Today.AddDays(-1).ToShortDateString());
        ThisYear = Convert.ToString(System.DateTime.Today.Year.ToString());


        DateTime d1, d2, d3, d4, d5, d6, d7;
        DateTime weekstart, weekend;
        d1 = Convert.ToDateTime(System.DateTime.Today.ToShortDateString());
        d2 = Convert.ToDateTime(System.DateTime.Today.AddDays(-1).ToShortDateString());
        d3 = Convert.ToDateTime(System.DateTime.Today.AddDays(-2).ToShortDateString());
        d4 = Convert.ToDateTime(System.DateTime.Today.AddDays(-3).ToShortDateString());
        d5 = Convert.ToDateTime(System.DateTime.Today.AddDays(-4).ToShortDateString());
        d6 = Convert.ToDateTime(System.DateTime.Today.AddDays(-5).ToShortDateString());
        d7 = Convert.ToDateTime(System.DateTime.Today.AddDays(-6).ToShortDateString());

        string ThisWeek = (System.DateTime.Today.DayOfWeek.ToString());
        if (ThisWeek.ToString() == "Monday")
        {
            weekstart = d1;
            weekend = weekstart.Date.AddDays(+6);
        }
        else if (Convert.ToString(ThisWeek) == "Tuesday")
        {
            weekstart = d2;
            weekend = weekstart.Date.AddDays(+6);
        }
        else if (ThisWeek.ToString() == "Wednesday")
        {
            weekstart = d3;
            weekend = weekstart.Date.AddDays(+6);
        }
        else if (ThisWeek.ToString() == "Thursday")
        {
            weekstart = d4;
            weekend = weekstart.Date.AddDays(+6);
        }
        else if (ThisWeek.ToString() == "Friday")
        {
            weekstart = d5;
            weekend = weekstart.Date.AddDays(+6);
        }
        else if (ThisWeek.ToString() == "Saturday")
        {
            weekstart = d6;
            weekend = weekstart.Date.AddDays(+6);

        }
        else
        {
            weekstart = d7;
            weekend = weekstart.Date.AddDays(+6);
        }
        string thisweekstart = weekstart.ToShortDateString();
        ViewState["thisweekstart"] = thisweekstart;
        string thisweekend = weekend.ToShortDateString();
        ViewState["thisweekend"] = thisweekend;

        //.................this week .....................


        DateTime d17, d8, d9, d10, d11, d12, d13;
        DateTime lastweekstart, lastweekend;

        d17 = Convert.ToDateTime(System.DateTime.Today.AddDays(-7).ToShortDateString());
        d8 = Convert.ToDateTime(System.DateTime.Today.AddDays(-8).ToShortDateString());
        d9 = Convert.ToDateTime(System.DateTime.Today.AddDays(-9).ToShortDateString());
        d10 = Convert.ToDateTime(System.DateTime.Today.AddDays(-10).ToShortDateString());
        d11 = Convert.ToDateTime(System.DateTime.Today.AddDays(-11).ToShortDateString());
        d12 = Convert.ToDateTime(System.DateTime.Today.AddDays(-12).ToShortDateString());
        d13 = Convert.ToDateTime(System.DateTime.Today.AddDays(-13).ToShortDateString());
        string thisday = (System.DateTime.Today.DayOfWeek.ToString());
        if (thisday.ToString() == "Monday")
        {
            lastweekstart = d17;
            lastweekend = lastweekstart.Date.AddDays(+6);
        }
        else if (Convert.ToString(thisday) == "Tuesday")
        {
            lastweekstart = d8;
            lastweekend = lastweekstart.Date.AddDays(+6);
        }
        else if (thisday.ToString() == "Wednesday")
        {
            lastweekstart = d9;
            lastweekend = lastweekstart.Date.AddDays(+6);
        }
        else if (thisday.ToString() == "Thursday")
        {
            lastweekstart = d10;
            lastweekend = lastweekstart.Date.AddDays(+6);
        }
        else if (thisday.ToString() == "Friday")
        {
            lastweekstart = d11;
            lastweekend = lastweekstart.Date.AddDays(+6);
        }
        else if (thisday.ToString() == "Saturday")
        {
            lastweekstart = d12;
            lastweekend = lastweekstart.Date.AddDays(+6);

        }
        else
        {
            lastweekstart = d13;
            lastweekend = lastweekstart.Date.AddDays(+6);
        }
        string lastweekstartdate = lastweekstart.ToShortDateString();
        ViewState["lastweekstart"] = lastweekstartdate;
        string lastweekenddate = lastweekend.ToShortDateString();
        ViewState["lastweekend"] = lastweekenddate;

        //.................last week .....................

        DateTime d14, d15, d16, d171, d18, d19, d20;
        DateTime last2weekstart, last2weekend;

        d14 = Convert.ToDateTime(System.DateTime.Today.AddDays(-14).ToShortDateString());
        d15 = Convert.ToDateTime(System.DateTime.Today.AddDays(-15).ToShortDateString());
        d16 = Convert.ToDateTime(System.DateTime.Today.AddDays(-16).ToShortDateString());
        d171 = Convert.ToDateTime(System.DateTime.Today.AddDays(-17).ToShortDateString());
        d18 = Convert.ToDateTime(System.DateTime.Today.AddDays(-18).ToShortDateString());
        d19 = Convert.ToDateTime(System.DateTime.Today.AddDays(-19).ToShortDateString());
        d20 = Convert.ToDateTime(System.DateTime.Today.AddDays(-20).ToShortDateString());

        //string thisday = (System.DateTime.Today.DayOfWeek.ToString());
        if (thisday.ToString() == "Monday")
        {
            last2weekstart = d14;
            last2weekend = last2weekstart.Date.AddDays(+6);
        }
        else if (Convert.ToString(thisday) == "Tuesday")
        {
            last2weekstart = d15;
            last2weekend = last2weekstart.Date.AddDays(+6);
        }
        else if (thisday.ToString() == "Wednesday")
        {
            last2weekstart = d16;
            last2weekend = last2weekstart.Date.AddDays(+6);
        }
        else if (thisday.ToString() == "Thursday")
        {
            last2weekstart = d171;
            last2weekend = last2weekstart.Date.AddDays(+6);
        }
        else if (thisday.ToString() == "Friday")
        {
            last2weekstart = d18;
            last2weekend = last2weekstart.Date.AddDays(+6);
        }
        else if (thisday.ToString() == "Saturday")
        {
            last2weekstart = d19;
            last2weekend = last2weekstart.Date.AddDays(+6);

        }
        else
        {
            last2weekstart = d20;
            last2weekend = last2weekstart.Date.AddDays(+6);
        }

        string last2weekstartdate = last2weekstart.ToShortDateString();
        ViewState["last2weekstart"] = last2weekstartdate;
        //string last2weekenddate = last2weekend.ToShortDateString();
        //ViewState["last2week"] = last2weekenddate;



        //------------------this month period-----------------

        DateTime thismonthstart = Convert.ToDateTime(System.DateTime.Now.Month.ToString() + "/1/" + System.DateTime.Now.Year.ToString());
        string thismonthstartdate = thismonthstart.ToShortDateString();
        ViewState["thismonthstartdate"] = thismonthstartdate;
        string thismonthenddate = Today.ToString();
        ViewState["thismonthenddate"] = thismonthenddate;

        //------------------this month period end................



        //-----------------last month period start ---------------

        // int last2monthno = Convert.ToInt32(thismonthstart.Month.ToString()) - 2;



        int lastmonthno = Convert.ToInt32(thismonthstart.Month.ToString()) - 1;
        string lastyesr = ThisYear.ToString();
        if (lastmonthno == 0)
        {
            lastmonthno = 1;
            lastyesr = System.DateTime.Today.AddYears(-1).Year.ToString();
        }
        string lastmonthNumber = Convert.ToString(lastmonthno.ToString());
        DateTime lastmonth = Convert.ToDateTime(lastmonthNumber.ToString() + "/1/" + lastyesr);
        string lastmonthstart = lastmonth.ToShortDateString();
        string lastmonthend = "";
        

        if (lastmonthNumber == "1" || lastmonthNumber == "3" || lastmonthNumber == "5" || lastmonthNumber == "7" || lastmonthNumber == "8" || lastmonthNumber == "10" || lastmonthNumber == "12")
        {
            lastmonthend = lastmonthNumber + "/31/" + ThisYear.ToString();
        }
        else if (lastmonthNumber == "4" || lastmonthNumber == "6" || lastmonthNumber == "9" || lastmonthNumber == "11")
        {
            lastmonthend = lastmonthNumber + "/30/" + ThisYear.ToString();
        }
        else
        {
            if (System.DateTime.IsLeapYear(Convert.ToInt32(ThisYear.ToString())))
            {
                lastmonthend = lastmonthNumber + "/29/" + ThisYear.ToString();
            }
            else
            {
                lastmonthend = lastmonthNumber + "/28/" + ThisYear.ToString();
            }
        }

        string lastmonthstartdate = lastmonthstart.ToString();
        ViewState["lastmonthstartdate"] = lastmonthstartdate;
        string lastmonthenddate = lastmonthend.ToString();
        ViewState["lastmonthenddate"] = lastmonthenddate;

        //-----------------last month period end -----------------------

        int last2monthno = Convert.ToInt32(thismonthstart.Month.ToString()) - 2;
         lastyesr = ThisYear.ToString();
         if (last2monthno == 0)
        {
            last2monthno = 12;
            lastyesr = System.DateTime.Today.AddYears(-1).Year.ToString();
        }
         if (last2monthno == -1)
        {
            last2monthno = 11;
            lastyesr = System.DateTime.Today.AddYears(-1).Year.ToString();
        }
        string last2monthNumber = Convert.ToString(last2monthno.ToString());
        DateTime last2month = Convert.ToDateTime(last2monthNumber.ToString() + "/1/" + lastyesr);
        string last2monthstart = last2month.ToShortDateString();
        ViewState["last2monthstart"] = last2monthstart;

        //-----------------last 2 month period end -----------------------


        //--------------this year period start----------------------


        DateTime thisyearstart = Convert.ToDateTime("1/1/" + ThisYear.ToString());
        DateTime thisyearend = Convert.ToDateTime("12/31/" + ThisYear.ToString());
        string thisyearstartdate = thisyearstart.ToShortDateString();
        ViewState["thisyearstartdate"] = thisyearstartdate;
        string thisyearenddate = thisyearend.ToShortDateString();
        ViewState["thisyearenddate"] = thisyearenddate;

        //---------------this year period end-------------------



        //--------------last year period start----------------------
        DateTime lastyearstart = Convert.ToDateTime("1/1/" + System.DateTime.Today.AddYears(-1).Year.ToString());
        DateTime lastyearend = Convert.ToDateTime("12/31/" + System.DateTime.Today.AddYears(-1).Year.ToString());
        string lastyearstartdate = lastyearstart.ToShortDateString();
        ViewState["lastyearstartdate"] = lastyearstartdate;
        string lastyearenddate = lastyearend.ToShortDateString();
        ViewState["lastyearenddate"] = lastyearenddate;
        //---------------last year period end-------------------
        DateTime last2yearstart = Convert.ToDateTime("1/1/" + System.DateTime.Today.AddYears(-2).Year.ToString());
        string last2yearstartdate = last2yearstart.ToShortDateString();
        ViewState["last2yearstartdate"] = last2yearstartdate;

        //---------------last 2 year period -------------------
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        bool gg = isValidConnection(txtDatabaseServerurl.Text, txtDBUserId.Text, txtDBPassword.Text, txtDatabaseAccessPort.Text);
        if (gg)
        {
           // lblmsg.Visible = true;
            Label9.Text = "Connected";
        }
        else
        {
          //  lblmsg.Visible = true;
            Label9.Text = "Not Connected";
        }

    }

    protected void Button7_Click(object sender, EventArgs e)
    {
        bool gg = isValidConnection(TextBox5.Text, TextBox6.Text, TextBox7.Text, TextBox8.Text);
        if (gg)
        {
            //lblmsg.Visible = true;
            Label10.Text = "Connected";
        }
        else
        {
           // lblmsg.Visible = true;
            Label10.Text = "Not Connected";
        }

    }
    protected void Button8_Click(object sender, EventArgs e)
    {
        bool gg = isValidConnection(TextBox10.Text, TextBox11.Text, TextBox12.Text, TextBox13.Text);
        if (gg)
        {
           // lblmsg.Visible = true;
            Label11.Text = "Connected";
        }
        else
        {
           // lblmsg.Visible = true;
            Label11.Text = "Not Connected";
        }
    }
    protected void Button9_Click(object sender, EventArgs e)
    {
        bool gg = isValidConnection(TextBox15.Text, TextBox16.Text, TextBox17.Text, TextBox18.Text);
        if (gg)
        {
           // lblmsg.Visible = true;
            Label12.Text = "Connected";
        }
        else
        {
          //  lblmsg.Visible = true;
            Label12.Text = " Not Connected";
        }

    }


    
}
