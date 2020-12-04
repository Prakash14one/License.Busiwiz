using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
//using System.DirectoryServices;
using System.IO.Compression;
using System.IO;
using System.IO;
using Ionic.Zip;
using System.Net;
using System.Security.Cryptography;
using Microsoft.Win32;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Text;
using System.Configuration;
using System.Data;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System.Data.SqlClient;
using System.Collections.Specialized;
using Microsoft.SqlServer.Management.Smo;




public partial class AccessRight : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    public static string Serverencstr = "";
    public static string verid = "";
    public static string compid = "";
    public static string Encryptkeycompsss = "";
    public static string encstr = "";
    SqlConnection condefaultinstance = new SqlConnection();
    SqlConnection conser;
    public static double size = 0;
    int StepId = 1;
    SqlConnection connMasterserver;
    SqlConnection connCompserver = new SqlConnection();
    string allstring = "";
    SqlConnection conn;
    protected void Page_Load(object sender, EventArgs e)
    {
        //5  10090  xxxxaa   sid=kdQMwcj0lE8=&versionid=v6GFOsFGdZ8=&compid=XV/A1eFDsSQ=
      //  Response.Redirect("http://license.busiwiz.com/Silent_CompanyCodeCreateTransfer.aspkx?sid=" + PageMgmt.Encrypted("5") + "&versionid=" + PageMgmt.Encrypted("10090") + "&compid=" + PageMgmt.Encrypted("xxxxaa"));
        if (!IsPostBack)
        {

            if (Request.QueryString["sid"] != null && Request.QueryString["comid"] != null)
            {
                string sid = PageMgmt.Decrypted(Request.QueryString["sid"].ToString());
                string compid = PageMgmt.Decrypted(Request.QueryString["comid"].ToString());
                //-----------------------------------------------------------------------------------------------------------
                string str = " SELECT dbo.CompanyMaster.CompanyLoginId, dbo.ServerMasterTbl.Id, dbo.PricePlanMaster.PricePlanId, dbo.PricePlanMaster.VersionInfoMasterId FROM dbo.CompanyMaster INNER JOIN dbo.PricePlanMaster ON dbo.CompanyMaster.PricePlanId = dbo.PricePlanMaster.PricePlanId INNER JOIN dbo.ServerMasterTbl ON dbo.CompanyMaster.ServerId = dbo.ServerMasterTbl.Id  where CompanyMaster.CompanyLoginId='" + compid + "' and dbo.ServerMasterTbl.Id='" + sid + "' ";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                adp.Fill(ds);
                if (ds.Rows.Count > 0)
                {
                    string versionid = ds.Rows[0]["VersionInfoMasterId"].ToString();
                    FristtimeSyncroTable_Detail(sid, versionid);
                    GenerateGrid(sid, versionid);
                    SeprateDatabase(sid, versionid);
                    // string Comp_ServEnckey = ds.Rows[0]["Enckey"].ToString();                  
                    //  string url = "http://" + lbl_serverurl.Text + "/ServerNewCompanyCreationPageNEW.aspx?cid=" + compid + "&sid=" + sid + "";
                    //  Response.Redirect("" + url + "");                     
                }
                else
                {
                    lblmsg.Text = "No record available for  ";
                }
                Response.Redirect("http://members.busiwiz.com/Companyconfigureinfo.aspx?comid=" + Request.QueryString["comid"].ToString() + "");
            }
            else
            {
                Response.Redirect("~/Silent_NewServerTable_Data_Transfer.aspx?sid=" + PageMgmt.Encrypted("5") + "&comid=" + PageMgmt.Encrypted("N666"));
            }
        }
    }
    protected void btnsync_Click(object sender, EventArgs e)
    {
       
    }
    protected void FristtimeSyncroTable_Detail(string sid,string  versionid)
    {
        int transf = 0;
        DataTable dt1 = selectBZ(" SELECT DISTINCT SatelliteSyncronisationrequiringTablesMaster.Id FROM ClientProductTableMaster INNER JOIN SatelliteSyncronisationrequiringTablesMaster ON ClientProductTableMaster.Id = SatelliteSyncronisationrequiringTablesMaster.TableID where SatelliteSyncronisationrequiringTablesMaster.Status='1' ");
        if (dt1.Rows.Count > 0)
        {
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                string datetim = DateTime.Now.ToString();
                string arqid = dt1.Rows[i]["Id"].ToString();

                string str22 = " Insert Into SyncronisationrequiredTbl(SatelliteSyncronisationrequiringTablesMasterID,DateandTime) Values ('" + arqid + "','" + Convert.ToDateTime(datetim) + "')";
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                SqlCommand cmn = new SqlCommand(str22, con);
                cmn.ExecuteNonQuery();
                con.Close();

                DataTable dt121 = selectBZ(" SELECT Max(ID) as ID from SyncronisationrequiredTbl where SatelliteSyncronisationrequiringTablesMasterID='" + arqid + "'");
                if (Convert.ToString(dt121.Rows[0]["ID"]) != "")
                {
                    DataTable dtcln = selectBZ(" SELECT Distinct ServerMasterTbl.Id FROM ServerMasterTbl inner join ServerAssignmentMasterTbl on ServerAssignmentMasterTbl.ServerId=ServerMasterTbl.Id inner join  PricePlanMaster on PricePlanMaster.PricePlanId=ServerAssignmentMasterTbl.PricePlanId    where ServerMasterTbl.id='" + sid + "' and ServerMasterTbl.Status='1' and ServerAssignmentMasterTbl.Active='1' and PricePlanMaster.active='1' ");
                    for (int j = 0; j < dtcln.Rows.Count; j++)
                    {
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        string str223 = " Insert Into SateliteServerRequiringSynchronisationMasterTbl (SyncronisationrequiredTBlID,[servermasterID],[SynchronisationSuccessful],[SynchronisationSuccessfulDatetime]) Values ('" + dt121.Rows[0]["ID"] + "','" + dtcln.Rows[j]["Id"] + "','0','" + DateTime.Now.ToString() + "')";
                        SqlCommand cmn3 = new SqlCommand(str223, con);
                        cmn3.ExecuteNonQuery();
                        con.Close();
                       // transf = Convert.ToInt32(rdsync.SelectedValue);
                    }
                }
            }
        }
        else
        {

        }
    }
    protected void GenerateGrid(string serid,string versionid)
    {
        int totnoc = 0;
        string portid = "";
        string pcateid = "";
        string serId = "";
        serId = " and ServerMasterTbl.Id='" + serid + "'";

        string tablefil = ""; string datefil = "";
        DataTable dtTemp = new DataTable();
        dtTemp = CreateData();
        int kl = 1;

        conn = new SqlConnection();
        DataTable dtcln = new DataTable();

        dtcln = selectBZ("SELECT Distinct ServerMasterTbl.* FROM CompanyMaster inner join  ServerMasterTbl on ServerMasterTbl.Id=CompanyMaster.ServerId inner join ServerAssignmentMasterTbl on ServerAssignmentMasterTbl.ServerId=ServerMasterTbl.Id inner join  PricePlanMaster on PricePlanMaster.PricePlanId=ServerAssignmentMasterTbl.PricePlanId inner join Priceplancategory on Priceplancategory.Id=PricePlanMaster.PriceplancatId   where ServerMasterTbl.Status='1' and ServerAssignmentMasterTbl.Active='1' and PricePlanMaster.active='1' and  PricePlanMaster.VersionInfoMasterId='" + versionid + "' and Priceplancategory.PortalId in" +
            "(Select Distinct Id from PortalMasterTbl where ProductId In( Select distinct ProductMaster.ProductId from  ProductMaster  inner join VersionInfoMaster on VersionInfoMaster.productId=ProductMaster.ProductId where CompanyMaster.active='1' and VersionInfoId = '" + versionid + "' ) " + portid + ") " + pcateid + serId + "");
        foreach (DataRow item in dtcln.Rows)
        {
            DataTable dtfindtab = selectBZ("select Distinct ClientProductTableMaster.Id as TableId from ClientProductTableMaster inner join SatelliteSyncronisationrequiringTablesMaster on SatelliteSyncronisationrequiringTablesMaster.TableID=ClientProductTableMaster.Id inner join SyncronisationrequiredTbl on SyncronisationrequiredTbl.SatelliteSyncronisationrequiringTablesMasterID=SatelliteSyncronisationrequiringTablesMaster.Id inner join " +
               " SateliteServerRequiringSynchronisationMasterTbl on SateliteServerRequiringSynchronisationMasterTbl.SyncronisationrequiredTBlID=SyncronisationrequiredTbl.Id inner join ServerMasterTbl on ServerMasterTbl.Id=SateliteServerRequiringSynchronisationMasterTbl.servermasterID where SatelliteSyncronisationrequiringTablesMaster.Status='1' and  SateliteServerRequiringSynchronisationMasterTbl.SynchronisationSuccessful='0' and SateliteServerRequiringSynchronisationMasterTbl.servermasterID='" + item["Id"] + "' and SatelliteSyncronisationrequiringTablesMaster.ProductVersionID='" + versionid + "'" + tablefil);
            foreach (DataRow igb in dtfindtab.Rows)
            {
                DataTable dtsedata = selectBZ(" select Distinct top(1) Failmsg,FailAttemp,Faildatetime, ClientProductTableMaster.TableName, ClientProductTableMaster.Id as TableId, SatelliteSyncronisationrequiringTablesMaster.Name as tabdesname,ServerMasterTbl.ServerName,ServerMasterTbl.serverloction,SyncronisationrequiredTbl.DateandTime,SateliteServerRequiringSynchronisationMasterTbl.servermasterID,SateliteServerRequiringSynchronisationMasterTbl.Id from ClientProductTableMaster inner join SatelliteSyncronisationrequiringTablesMaster on SatelliteSyncronisationrequiringTablesMaster.TableID=ClientProductTableMaster.Id inner join SyncronisationrequiredTbl on SyncronisationrequiredTbl.SatelliteSyncronisationrequiringTablesMasterID=SatelliteSyncronisationrequiringTablesMaster.Id inner join " +
               " SateliteServerRequiringSynchronisationMasterTbl on SateliteServerRequiringSynchronisationMasterTbl.SyncronisationrequiredTBlID=SyncronisationrequiredTbl.Id inner join ServerMasterTbl on ServerMasterTbl.Id=SateliteServerRequiringSynchronisationMasterTbl.servermasterID where SatelliteSyncronisationrequiringTablesMaster.Status='1' and  SateliteServerRequiringSynchronisationMasterTbl.SynchronisationSuccessful='0' and SateliteServerRequiringSynchronisationMasterTbl.servermasterID='" + item["Id"] + "' and SatelliteSyncronisationrequiringTablesMaster.TableID='" + igb["TableId"] + "' and  SatelliteSyncronisationrequiringTablesMaster.ProductVersionID='" + versionid + "' " + datefil + " order by SateliteServerRequiringSynchronisationMasterTbl.Id Desc");
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
                    dtr2["TableId"] = Convert.ToString(dtsedata.Rows[0]["TableId"]);
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
    protected void SeprateDatabase(string serid, string verid)
    {
        int totnoc = 0;
        string portid = "";
        string pcateid = "";
        string serId = "";
        serId = " and ServerMasterTbl.Id='" + serid + "'";
        conn = new SqlConnection();
        DataTable dtcln = new DataTable();
        foreach (GridViewRow item in grdserver.Rows)
        {
            string syncreqid = grdserver.DataKeys[item.RowIndex].Value.ToString();
            Label lbltabname = (Label)item.FindControl("lbltabname");
            Label lblseid = (Label)item.FindControl("lblseid");
            Label lblattempt = (Label)item.FindControl("lblattempt");
            CheckBox cbItem = (CheckBox)item.FindControl("cbItem");
            Label lbl_TableId = (Label)item.FindControl("lbl_TableId");
            
           // string verid = ddlProductname.SelectedValue;
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
                        conn.ConnectionString = @"Data Source =" + serversqlserverip + "\\" + serversqlinstancename + "," + serversqlport + "; Initial Catalog=" + serversqldbname + "; User ID=Sa; Password=" + PageMgmt.Decrypted(serversqlpwd) + "; Persist Security Info=true;";
                         //conn = new SqlConnection(@"Data Source =TCP:192.168.2.100,40000; Initial Catalog=C3SATELLITESERVER; User ID=Sa; Password=06De1963++; Persist Security Info=true;");
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
                        //tableins("PayPaltest1");
                        DynamicalyTable(tablename); 
                        lblmsg.Text = " Sync successfully";
                        if (inv == 1)
                        {
                            SatelliteSyncronisationrequiringTablesMaster(verid, lbl_TableId.Text);
                            SyncronisationrequiredTbl(verid, lbl_TableId.Text);
                            string insser = "insert into SateliteServerRequiringSynchronisationDetailTbl(SateliteServerRequiringSynchronisationMasterTblID,SynchronisationAttemptDatetime,SynchronisationSuccessful,SynchronisationAttemptErromMessage) Values" +
                                " ('" + syncreqid  + "','" + DateTime.Now.ToString() + "','" + inv + "','" + lblmsg.Text + "') ";
                            SqlCommand cmdin = new SqlCommand(insser, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            int appd = Convert.ToInt32(cmdin.ExecuteNonQuery());
                            con.Close();
                            if (appd > 0)
                            {
                                string inupd = " Update SateliteServerRequiringSynchronisationMasterTbl set SynchronisationSuccessful='" + inv + "' where SyncronisationrequiredTBlID='"+lbl_TableId.Text +"' and Id='" + syncreqid + "'";
                                SqlCommand cmup = new SqlCommand(inupd, con);
                                if (con.State.ToString() != "Open")
                                {
                                    con.Open();
                                }
                                cmup.ExecuteNonQuery();
                                con.Close();
                                DataTable dtcup = selectBZ(" Select * from SateliteServerRequiringSynchronisationMasterTbl where Id='" + syncreqid + "' and SyncronisationrequiredTBlID='" + lbl_TableId.Text + "'  ");
                                if (dtcup.Rows.Count > 0)
                                {
                                    string str223 = " Insert Into SateliteServerRequiringSynchronisationMasterTbl(Id,SyncronisationrequiredTBlID,[servermasterID],[SynchronisationSuccessful],[SynchronisationSuccessfulDatetime])Values('" + syncreqid + "'," +
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
                                                "('" + dtcupx.Rows[0]["Id"] + "','" + syncreqid + "','" + dtcupx.Rows[0]["SynchronisationAttemptDatetime"] + "','" + inv + "','" + lblmsg.Text + "')";
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
                        string inupd = "Update SateliteServerRequiringSynchronisationMasterTbl set SynchronisationSuccessful='0',Failmsg='Fail',FailAttemp='" + noatt + "',Faildatetime='" + DateTime.Now.ToString() + "' where Id='" + syncreqid + "' and SyncronisationrequiredTBlID='" + lbl_TableId.Text + "' ";
                        SqlCommand cmup = new SqlCommand(inupd, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmup.ExecuteNonQuery();
                        con.Close();
                        lblmsg.Text = e1.ToString();
                    }

                }
            }
        }
        if (totnoc == 0)
        {
            lblmsg.Text = "Record not founds.";
        }
        else
        {   
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
        DataTable dts = selectSer("select column_name,data_type,CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.COLUMNS where table_name='" + tablename + "'");
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
            DataTable DTBC = selectSer("select column_name,data_type,CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.COLUMNS where table_name='" + tablename + "'");
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
                cccd += "'" + PageMgmt.Encrypted(Convert.ToString(itm["" + dtsccc.Rows[k]["column_name"] + ""])) + "' ,";
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
                    string tempstr = Temp2 + Temp3val;
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
    protected void SatelliteSyncronisationrequiringTablesMaster(string verid,string TableID)
    {
        SqlCommand cmdrX = new SqlCommand(" Delete  from  SatelliteSyncronisationrequiringTablesMaster where TableID='" + TableID + "'", conn);
        conn.Open();
        cmdrX.ExecuteNonQuery();
        conn.Close();
        string Temp3val = "";
        string temp = "";
        string Temp2 = "INSERT INTO SatelliteSyncronisationrequiringTablesMaster(Id,ProductVersionID,TableID,Name,Status,TableName)Values ";
        DataTable dtr = selectBZ(" Select Distinct * from SatelliteSyncronisationrequiringTablesMaster where status='1' and ProductVersionID='" + verid + "'");
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
    protected void SyncronisationrequiredTbl(string verid, string TableId)
    {
        SqlCommand cmdrX = new SqlCommand(" Delete  from  SyncronisationrequiredTbl where SatelliteSyncronisationrequiringTablesMasterID='"+TableId+"' ", conn);
        conn.Open();
        cmdrX.ExecuteNonQuery();
        conn.Close();
        string Temp3val = "";
        string temp = "";
        string Temp2 = " INSERT INTO SyncronisationrequiredTbl (ID,SatelliteSyncronisationrequiringTablesMasterID,DateandTime) Values ";
        DataTable dtr = selectBZ(" Select Distinct SyncronisationrequiredTbl.*   from  SyncronisationrequiredTbl inner join SatelliteSyncronisationrequiringTablesMaster on SatelliteSyncronisationrequiringTablesMaster.Id=SyncronisationrequiredTbl.SatelliteSyncronisationrequiringTablesMasterID where ProductVersionID='" + verid + "' and SyncronisationrequiredTbl.SatelliteSyncronisationrequiringTablesMasterID='"+TableId+"' ");
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



    public DataTable CreateData()
    {
        DataTable dtTemp = new DataTable();
        DataColumn prd1 = new DataColumn();
        prd1.ColumnName = "Id";
        prd1.DataType = System.Type.GetType("System.String");
        prd1.AllowDBNull = true;
        dtTemp.Columns.Add(prd1);

         DataColumn prd1tid = new DataColumn();
         prd1tid.ColumnName = "TableId";
         prd1tid.DataType = System.Type.GetType("System.String");
         prd1tid.AllowDBNull = true;
         dtTemp.Columns.Add(prd1tid);
        


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




    protected DataTable selectSer(string str)
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

   
}
