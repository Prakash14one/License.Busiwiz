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
using System.IO;
using System.Text;
using System.Net;
using System.Data.SqlClient;
using System.Globalization;
using System.Net.Mail;
using System.Security.Cryptography;

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

    string UserLoginLogID ="";
    string MacAddress = "";
    string Ipaddress = "";
    string computerip = "";
    string computername = "";
    string companyid = "";
    string banname = "";
    string employee = "";
    string employeeid = "";
    string employeename = "";
    string Departmentname = "";
    string DesignetionName = "";
    string banid = "";


    string FailedAttempt = "";
    string FailedAttemptMinute = "";
    string TemporarilyBanMacAddressMInute = "";
    string SameComputerMacAddressFailedAttempt = "";
    string SameComputerMacAddressFailedAttemptMinute = "";
    string SameComputerIpAddressFailedAttempt = "";
    string SameComputerIpAddressFailedAttemptMinute = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
          
            if (Request.QueryString["UserLoginLogID"] != null)//Permenataly ban ipadress and Sync
            {              
                //UserLoginLogID  comid MacAddress Ipaddress computerip computername Departmentname DesignetionName FailedAttempt FailedAttemptMinute TemporarilyBanMacAddressMInute SameComputerMacAddressFailedAttempt SameComputerIpAddressFailedAttempt SameComputerIpAddressFailedAttempt SameComputerIpAddressFailedAttemptMinute
                createwebsiteandattach(Request.QueryString["comid"]);
                companyid = Request.QueryString["comid"];
                Session["CompId"] = Request.QueryString["comid"];

                UserLoginLogID = DecryptStringServer(Request.QueryString["UserLoginLogID"]);//UserLoginLogID
                MacAddress = DecryptStringServer(Request.QueryString["MacAddress"]);
                Ipaddress = Request.QueryString["Ipaddress"].ToString();
                Ipaddress = Ipaddress.Replace(' ', '+');
                Ipaddress = DecryptStringServer(Ipaddress);
                computerip = DecryptStringServer(Request.QueryString["computerip"]);
                computername = DecryptStringServer(Request.QueryString["computername"]);
                employeeid= DecryptStringServer(Request.QueryString["employeeid"]);
                employeename= DecryptStringServer(Request.QueryString["employeename"]);
                Departmentname= DecryptStringServer(Request.QueryString["Departmentname"]);
                DesignetionName= DecryptStringServer(Request.QueryString["DesignetionName"]);
                //----------------------------------------------------------------------------------------------
                FailedAttempt = DecryptStringServer(Request.QueryString["FailedAttempt"]);
                FailedAttemptMinute = DecryptStringServer(Request.QueryString["FailedAttemptMinute"]);
                TemporarilyBanMacAddressMInute = DecryptStringServer(Request.QueryString["TemporarilyBanMacAddressMInute"]);
                SameComputerMacAddressFailedAttempt = DecryptStringServer(Request.QueryString["SameComputerMacAddressFailedAttempt"]);
                SameComputerMacAddressFailedAttemptMinute = DecryptStringServer(Request.QueryString["SameComputerMacAddressFailedAttemptMinute"]);
                SameComputerIpAddressFailedAttempt = DecryptStringServer(Request.QueryString["SameComputerIpAddressFailedAttempt"]);
                SameComputerIpAddressFailedAttemptMinute = DecryptStringServer(Request.QueryString["SameComputerIpAddressFailedAttemptMinute"]);
               // string te = "http://license.busiwiz.com/InsertPermenatalybanSilent.aspx?UserLoginLogID=" + ClsEncDesc.Encrypted("" + txtuname.Text + "") + "&MacAddress="++""&publicip=" + ClsEncDesc.Encrypted("" + Session["publicip"].ToString() + "") + "&Ipaddress=" + ClsEncDesc.Encrypted("" + Session["ipaddress"].ToString() + "") + "&computerip=" + ClsEncDesc.Encrypted("" + Session["computeripadd"].ToString() + "") + "&5=" + ClsEncDesc.Encrypted("" + Session["computeripadd"].ToString() + "") + "&6=" + ssttuff + "&comid=" + ssttuff + "";
                // 
                //-------------------------------------------------------------------------------------------------
                string ipbanYesNo = "0";
                string makebanYesNo = "0";
                if (Request.QueryString["ipbanyesORno"] == "yes")//Permenataly ban ipadress and Sync ipbanyesORno
                {
                    banname = " IP " + Ipaddress;
                    Session["banname"] = banname;  
                    ipbanYesNo = "1";
                }
                else
                {
                    banname = " MacAddress " + MacAddress;
                    Session["banname"] = banname;  
                    makebanYesNo = "1";                  
                }
                
                try
                {
                   // if (con.State.ToString() != "Open")
                   // {
                   //     con.Open();
                   // }
                   // SqlCommand cmd = new SqlCommand("Permenatalybanmacaddressandipadress_AddDelUpdtSelect", con);
                   // cmd.CommandType = CommandType.StoredProcedure;

                   // cmd.Parameters.AddWithValue("@StatementType", "Insert");
                   //// cmd.Parameters.AddWithValue("@ProductModelID", ViewState["ID"]);
                   // cmd.Parameters.AddWithValue("@UserLoginLogID", UserLoginLogID);
                   // cmd.Parameters.AddWithValue("@MacAddress", MacAddress);
                   // cmd.Parameters.AddWithValue("@Ipaddress", Ipaddress);
                   // cmd.Parameters.AddWithValue("@computerip", computerip);
                   // cmd.Parameters.AddWithValue("@computername", computername);
                   // cmd.Parameters.AddWithValue("@bannedmacaddress", makebanYesNo);
                   // cmd.Parameters.AddWithValue("@bannedipaddress", ipbanYesNo);
                   // cmd.Parameters.AddWithValue("@compid",companyid );
                   // cmd.Parameters.AddWithValue("@DateTime", DateTime.Now.ToString("yyyy/MM/dd"));
                   // cmd.Parameters.AddWithValue("@UserNameLastAccess", "");
                   // cmd.Parameters.AddWithValue("@BanIsActive", "");
                   // cmd.Parameters.AddWithValue("@portalid", Session["portlID"]);
                   // cmd.Parameters.AddWithValue("@companyloinid", companyid);
                   // cmd.Parameters.AddWithValue("@Employeeid", employeeid);
                   // cmd.Parameters.AddWithValue("@EmpName", employeename);
                   // cmd.Parameters.AddWithValue("@Designetionid", "");
                   // cmd.Parameters.AddWithValue("@Departmentid","");
                   // cmd.Parameters.AddWithValue("@Departmentname", Departmentname);
                   // cmd.Parameters.AddWithValue("@DesignetionName", DesignetionName);   
                   // cmd.ExecuteNonQuery();


                    string insertpermenant = "insert into Permenatalybanmacaddressandipadress (UserLoginLogID,MacAddress,Ipaddress,computerip,computername,bannedmacaddress, " +
                        " bannedipaddress,DateTime,portalid,companyloinid,Employeeid,EmpName,Departmentname,DesignetionName) values " +
                        "('" + UserLoginLogID + "','" + MacAddress + "','" + Ipaddress + "','" + computerip + "','" + computername + "','" + makebanYesNo + "','" + ipbanYesNo + "','" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + Session["portlID"] + "','" + companyid + "','" + employeeid + "','" + employeename + "','" + Departmentname + "','" + DesignetionName + "') ";
                    SqlCommand cmdpermenant = new SqlCommand(insertpermenant, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdpermenant.ExecuteNonQuery();
                    con.Close();


                    string selectlogin = "select max(ID) as Bannid from Permenatalybanmacaddressandipadress ";
                    SqlCommand cmdselect = new SqlCommand(selectlogin, con);
                    SqlDataAdapter sdaselect = new SqlDataAdapter(cmdselect);
                    DataTable dtlogin = new DataTable();
                    sdaselect.Fill(dtlogin);
                    Session["Bannid"] = dtlogin.Rows[0]["Bannid"].ToString();//
                    try
                    {
                        sendmailtoclientforcompanyactivation();                        
                    }
                    catch (Exception ex)
                    {
                    }
                    try
                    {
                        sendmailtoClient();
                    }
                    catch (Exception ex)
                    {
                    }
                    syncro();
                    string te = "InsertPermenatalybanSilent.aspx?success=yes&ipban='" + Request.QueryString["ipbanyesORno"] + "'";
                    Response.Redirect(te); 

                }
                catch (Exception ex)
                {
                    string te = "InsertPermenatalybanSilent.aspx?success1=no&ipban='" + Request.QueryString["ipbanyesORno"] + "'";
                    Response.Redirect(te); 
                }
                               
            }
            if (Request.QueryString["success"] != null)
            {

                Label1.Text = "Unfortunately your " + Session["banname"] + " Address has been permanently banned. You may contact your company administrator to remove the ban.";  
            }
            if (Request.QueryString["success1"] != null)
            {

                Label1.Text = "Unfortunately your " + Session["banname"] + " Address has been permanently banned. You may contact your company administrator to remove the ban.";
            }
            if (Request.QueryString["SendeRelReq"] != null)//Send 
            {               
                Pnl1.Visible = true;                
            }
           
            if (Request.QueryString["Banrelease"] != null)
            {               
                companyid = (Request.QueryString["CompId"]);
                Session["CompId"] = (Request.QueryString["CompId"]);
                createwebsiteandattach(companyid);
                Ipaddress = Request.QueryString["Banrelease"].ToString();
                Ipaddress = Ipaddress.Replace(' ', '+');
                Ipaddress = DecryptStringServer(Ipaddress);
                banid = Request.QueryString["banid"].ToString();
                banid = banid.Replace(' ', '+');
                banid = DecryptStringServer(banid);
                Session["Bannid"] = banid; 
                //            
               

                string selectlogin = " select * from Permenatalybanmacaddressandipadress where id=" + banid + " ";
                SqlCommand cmdselect = new SqlCommand(selectlogin, con);
                SqlDataAdapter sdaselect = new SqlDataAdapter(cmdselect);
                DataTable dtlogin = new DataTable();
                sdaselect.Fill(dtlogin);
                if (dtlogin.Rows.Count > 0)
                {
                    Ipaddress = dtlogin.Rows[0]["Ipaddress"].ToString();//
                    computername = dtlogin.Rows[0]["computername"].ToString();//
                    employeename = dtlogin.Rows[0]["EmpName"].ToString();//
                    UserLoginLogID = dtlogin.Rows[0]["UserLoginLogID"].ToString();//
                    MacAddress = dtlogin.Rows[0]["MacAddress"].ToString();//
                    if (Request.QueryString["ipbanyesORno"] == "yes")//Permenataly ban ipadress and Sync
                    {
                        banname = " IP " + Ipaddress;
                        Session["banname"] = banname;
                    }
                    else
                    {
                        banname = " MacAddress " + MacAddress;
                        Session["banname"] = banname;
                    }

                    removeiprestriction();
                    SendMailToCompanyAdmin_Release();
                    syncro();
                }
                string te = "InsertPermenatalybanSilent.aspx?success2=yes";
                Response.Redirect(te);              
            }
            if (Request.QueryString["success2"] != null)
            {
                Label1.Text = " Successfully Release " + Session["banname"] + " .";
            }
            
        }
    }
    protected void createwebsiteandattach(string compid)
    {
        int totnoc = 0;
        string str = " SELECT CompanyMaster.*,PortalMasterTbl.id as portlID, PortalMasterTbl.PortalName,PricePlanMaster.VersionInfoMasterId,PricePlanMaster.Producthostclientserver from CompanyMaster inner join PricePlanMaster on PricePlanMaster.PricePlanId=CompanyMaster.PricePlanId inner join PortalMasterTbl on PricePlanMaster.PortalMasterId1 = PortalMasterTbl.id where CompanyLoginId='" + compid + "' ";
        SqlCommand cmd = new SqlCommand(str, con);        
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        if (ds.Rows.Count > 0)
        {
            //string websitename = compid;
            //verid = ds.Rows[0]["VersionInfoMasterId"].ToString();
            //string priceplanid = ds.Rows[0]["PricePlanId"].ToString();
            //string productid = ds.Rows[0]["ProductId"].ToString();
            Session["PortalName"] = ds.Rows[0]["PortalName"].ToString();
            Session["ProductId"] = ds.Rows[0]["VersionInfoMasterId"].ToString();
            string versionid = ds.Rows[0]["VersionInfoMasterId"].ToString();
            string Serverid = ds.Rows[0]["Serverid"].ToString();
            Encryptkeycompsss = ds.Rows[0]["Encryptkeycomp"].ToString();
            Session["portlID"] = ds.Rows[0]["portlID"].ToString();

            DataTable dtre = selectBZ("select * from ServerMasterTbl where Id='" + Serverid + "'");
            encstr = "&%#@?,:*";
            string serversqlserverip = dtre.Rows[0]["sqlurl"].ToString();
            string serversqlinstancename = dtre.Rows[0]["DefaultsqlInstance"].ToString();
            string serversqldbname = dtre.Rows[0]["DefaultDatabaseName"].ToString();
            string serversqlpwd = dtre.Rows[0]["Sapassword"].ToString();
            string serversqlport = dtre.Rows[0]["port"].ToString();


            try
            {
                totnoc = 1;
                conser = new SqlConnection();
                conser.ConnectionString = @"Data Source =" + serversqlserverip + "\\" + serversqlinstancename + "," + serversqlport + "; Initial Catalog=" + serversqldbname + "; User ID=Sa; Password=" + PageMgmt.Decrypted(serversqlpwd) + "; Persist Security Info=true;";
              //  conser.ConnectionString = @" Data Source =TCP:192.168.6.80,40000; Initial Catalog=C3SATELLITESERVER; User ID=Sa; Password=06De1963++; Persist Security Info=true;";
                    //           Data Source =q444_SQL.safestserver.com\C3SERVERMASTER,30000; Initial Catalog = master; User ID=sa; Password=06De1962++; Persist Security Info=true; 
                if (conser.State.ToString() != "Open")
                {
                    conser.Open();
                }
                conser.Close();
                encstr = "";
                encstr = Convert.ToString(dtre.Rows[0]["Enckey"]);
            }
            catch (Exception e1)
            {

            }
        }

    }

    protected void tableins(string tablename)
    {
        string st1 = " CREATE TABLE " + tablename + "(";

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
            SqlCommand cmdr = new SqlCommand(st1, conser);
            conser.Open();
            cmdr.ExecuteNonQuery();
            conser.Close();
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
                SqlCommand cmdrX = new SqlCommand("Drop table " + tablename, conser);
                conser.Open();
                cmdrX.ExecuteNonQuery();
                conser.Close();
                SqlCommand cmdr = new SqlCommand(st1, conser);
                conser.Open();
                cmdr.ExecuteNonQuery();
                conser.Close();
              
            }
            else
            {
                SqlCommand cmdrX = new SqlCommand("Delete  from  " + tablename, conser);
                conser.Open();
                cmdrX.ExecuteNonQuery();
                conser.Close();
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

                cccd += "'" + (Convert.ToString(itm["" + dtsccc.Rows[k]["column_name"] + ""])) + "' ,";//Encrypted

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
                    SqlCommand ccm = new SqlCommand(tempstr, conser);
                    conser.Open();
                    ccm.ExecuteNonQuery();
                    conser.Close();
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
            SqlCommand ccm = new SqlCommand(Temp2, conser);
            conser.Open();
            ccm.ExecuteNonQuery();
            conser.Close();
        }
    }
    protected void syncro()
    {
        try
        {
            int inv = 0;
            string tablename = "Permenatalybanmacaddressandipadress";
            inv = 1;
            tableins("" + tablename + "");
            //tableins("PayPaltest1");
            DynamicalyTable(tablename);
        }
        catch (Exception ex)
        {
        }
        

        
        
    }
    protected void DynamicalyTable1(string tanlename)
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
                    string tempstr = Temp2 + Temp3val;
                    SqlCommand ccm = new SqlCommand(tempstr, conser);
                    conser.Open();
                    ccm.ExecuteNonQuery();
                    conser.Close();
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
            SqlCommand ccm = new SqlCommand(Temp2, conser);
            conser.Open();
            ccm.ExecuteNonQuery();
            conser.Close();
        }
    }
    protected DataTable select(string str)
    {
        SqlCommand cmdclnccdweb = new SqlCommand(str, conser);
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
    public void sendmailtoclientforcompanyactivation()
    {
        string str = " select distinct Priceplancategory.CategoryName,CompanyMaster.CompanyLoginId,dbo.ClientMaster.EmailID AS clientEmail,  dbo.PricePlanMaster.EndDate, dbo.CompanyMaster.AdminId , dbo.CompanyMaster.ContactPersonDesignation,CompanyMaster.phone,CompanyMaster.Email,CompanyMaster.pincode,CompanyMaster.CompanyName,CompanyMaster.ContactPerson,CompanyMaster.city,CompanyMaster.Address,StateMasterTbl.StateName,CountryMaster.CountryName, PortalMasterTbl.*,PricePlanMaster.PricePlanAmount,PricePlanMaster.PricePlanName,PricePlanMaster.PricePlanId,OrderPaymentSatus.orderdate,OrderPaymentSatus.TransactionID from  CompanyMaster inner join PricePlanMaster  on CompanyMaster.PricePlanId=PricePlanMaster.PricePlanId inner join VersionInfoMaster on VersionInfoMaster.VersionInfoId=PricePlanMaster.VersionInfoMasterId inner join ProductMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId inner join ClientMaster on ProductMaster.ClientMasterId=ClientMaster.ClientMasterId inner join Priceplancategory on Priceplancategory.ID=PricePlanMaster.PriceplancatId inner join PortalMasterTbl on PortalMasterTbl.Id=Priceplancategory.PortalId   inner join OrderMaster on CompanyMaster.CompanyLoginId=OrderMaster.CompanyLoginId inner join  OrderPaymentSatus on OrderMaster.OrderId=OrderPaymentSatus.OrderId   inner join StateMasterTbl on StateMasterTbl.StateId=CompanyMaster.StateId inner join CountryMaster on StateMasterTbl.CountryId=CountryMaster.CountryId  WHERE (CompanyMaster.CompanyLoginId = '" + companyid + "') ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            StringBuilder strplan = new StringBuilder();
            strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
         //   strplan.Append("<tr><td align=\"left\"> <img src=\"http://" + Request.Url.Host.ToString() + "/Shoppingcart/images/" + dssecadmin.Rows[0]["logourl"].ToString() + "\" \"border=\"0\" Width=\"90\" Height=\"80\" / > </td ></tr>  ");
            strplan.Append("<br></table> ");

            strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
            // strplan.Append("<tr><td align=\"left\">Dear " + dssecadmin.Rows[0]["EmployeeName"].ToString() + ",</td></tr>  ");
            strplan.Append("<tr><td align=\"left\">Dear Admin,</td></tr>  ");
            strplan.Append("<tr><td align=\"left\"><br></td></tr>  ");
            strplan.Append("</table> ");
            DateTime now = DateTime.Now;
            string date = now.GetDateTimeFormats('d')[0];
            string time = now.GetDateTimeFormats('t')[0];
            

            strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
            //strplan.Append("<tr><td align=\"left\"> On " + date + " at " + time + " following company " + companyid + " from IP " + Ipaddress + " have done unsuccessful login attempts which crossed the permissible limit hence the IP " + Ipaddress + " is banned globally for further login.</td></tr> ");
            strplan.Append("<tr><td align=\"left\">There was a potential hacking incident occurred for company " + companyid + ",   user name " + UserLoginLogID + ", </td></tr> ");
            
            strplan.Append("<tr><td align=\"left\">employee " + employeename + ", " + banname + ", On " + date + " at " + time + ". </td></tr> ");
            strplan.Append("<tr><td align=\"left\"><br>" + date + " at " + time + " globally banned  </td></tr> ");            
            strplan.Append("</table> ");


            strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
            strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Department and Designation:  </td><td align=\"left\" style=\"width: 80%\">" + Departmentname + ", " + DesignetionName + "</td></tr>");
            strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">User ID: </td><td align=\"left\" style=\"width: 80%\">" + UserLoginLogID + "</td></tr>");
            strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Employee name: </td><td align=\"left\" style=\"width: 80%\">" + employeename + "</td></tr>");
            strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Computer name: </td><td align=\"left\" style=\"width: 80%\">" + computername + "</td></tr>");
            strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">IP: </td><td align=\"left\" style=\"width: 80%\">" + Ipaddress + "</td></tr>");
            strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Phone No: </td><td align=\"left\" style=\"width: 80%\">" + dt.Rows[0]["phone"].ToString() + "</td></tr>");
            strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Email: </td><td align=\"left\" style=\"width: 80%\">" + dt.Rows[0]["Email"].ToString() + "</td></tr>");
            strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Address: </td><td align=\"left\" style=\"width: 80%\">" + dt.Rows[0]["Address"].ToString() + ", " + dt.Rows[0]["city"].ToString() + ", " + dt.Rows[0]["StateName"].ToString() + ", " + dt.Rows[0]["CountryName"].ToString() + ", " + dt.Rows[0]["pincode"].ToString() + "</td></tr>");
            strplan.Append("</table> ");


            strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
            strplan.Append("<tr><td align=\"left\"> " + banname + " have done unsuccessful login attempts which crossed the permissible limit and hence the " + banname + " is banned globally for further login. </td></tr> ");
            strplan.Append("<tr><td align=\"left\"><br> </td></tr> ");

            //strplan.Append("<tr><td align=\"left\"> If you wish to see the temporary " + banname + " ban history please   <a href=http://" + Request.QueryString["url"].ToString() + "/LoginErrorLogBeforlogin.aspx?banid=" + Encrypted(Session["Bannid"].ToString()) + "&view=" + Encrypted(Ipaddress.ToString()) + "&CompId=" + Session["CompId"].ToString() + "&ipban=" + Request.QueryString["ipbanyesORno"] + " >here </a>  OR copy paste following link in your browser. </td></tr> ");
            //strplan.Append("<tr><td align=\"left\">http://" + Request.QueryString["url"].ToString() + "/LoginErrorLogBeforlogin.aspx?banid=" + Encrypted(Session["Bannid"].ToString()) + "&view=" + Encrypted(Ipaddress.ToString()) + "&CompId=" + Session["CompId"].ToString() + "&ipban="+Request.QueryString["ipbanyesORno"]+"</td></tr> ");

            strplan.Append("<tr><td align=\"left\"><br> If you wish to see the permanent globally " + banname + " ban history please <a href=http://" + Request.Url.Host.ToString() + "/Banreleaseview.aspx?banid=" + Encrypted(Session["Bannid"].ToString()) + "&view=" + Encrypted(Ipaddress.ToString()) + "&CompId=" + Session["CompId"].ToString() + "&ipban=" + Request.QueryString["ipbanyesORno"] + " >here </a>  OR copy paste following link in your browser. </td></tr> ");
            strplan.Append("<tr><td align=\"left\">http://" + Request.Url.Host.ToString() + "/Banreleaseview.aspx?banid=" + Encrypted(Session["Bannid"].ToString()) + "&view=" + Encrypted(Ipaddress.ToString()) + "&CompId=" + Session["CompId"].ToString() + "&ipban=" + Request.QueryString["ipbanyesORno"] + "</td></tr> ");
            strplan.Append("<tr><td align=\"left\"><br> </td></tr> ");

            

            //strplan.Append("<tr><td align=\"left\"> As per web security rule set your IP will be banned on following conditions </td></tr> ");
            //strplan.Append("<tr><td align=\"left\">     1.  If any user makes " + FailedAttempt + " failed attempts in " + FailedAttemptMinute + " minutes then the computer will be banned for login for the next " + TemporarilyBanMacAddressMInute + " minutes. </td></tr> ");
            //strplan.Append("<tr><td align=\"left\">    	2.  If the same computer violates Rule #1 " + SameComputerMacAddressFailedAttempt + " times within " + SameComputerMacAddressFailedAttemptMinute + " minutes then the computer will be banned globally (for any other company) and permanently. </td></tr> ");
            //strplan.Append("<tr><td align=\"left\">     3.  If the same IP address violates Rule #1 " + SameComputerIpAddressFailedAttempt + " times within " + SameComputerIpAddressFailedAttemptMinute + " minutes then the computer will be banned globally (for any other company) and permanently. </td></tr> ");


            //strplan.Append("<tr><td align=\"left\"><br>If you wish to communicate to employee please click here</td></tr> ");

            strplan.Append("<tr><td align=\"left\"> <br>If you are certain that the failed login attempt is due to unintentional error by one of your user and it is not a hacking attempt, <br>you may click  <a href=http://" + Request.Url.Host.ToString() + "/InsertPermenatalybanSilent.aspx?banid=" + Encrypted(Session["Bannid"].ToString()) + "&SendeRelReq=" + Encrypted(Ipaddress.ToString()) + "&CompId=" + Session["CompId"].ToString() + "&url=" + Request.QueryString["url"] + "&ipban=" + Request.QueryString["ipbanyesORno"] + ">here </a> for sending request to reactivate the banned IP. OR copy paste following link in your browser.	</td></tr> ");
            strplan.Append("<tr><td align=\"left\">http://" + Request.Url.Host.ToString() + "/InsertPermenatalybanSilent.aspx?banid=" + Encrypted(Session["Bannid"].ToString()) + "&SendeRelReq=" + Encrypted(Ipaddress) + "&CompId=" + Session["CompId"].ToString() + "&url=" + Request.QueryString["url"] + "&ipban=" + Request.QueryString["ipbanyesORno"] + " </td></tr> ");
            
            
            strplan.Append("</table> ");
            strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
            strplan.Append("<tr><td align=\"left\"><br><br><br>Thank you,</td></tr>  ");
            //strplan.Append("<tr><td align=\"left\">Sincerely ,</td></tr>  ");
            strplan.Append("<tr><td align=\"left\">Sincerely. </td></tr>  ");
            strplan.Append("</table> ");

                    
                string bodyformate = "" + strplan + "";
                //try
                //{
                MailAddress to = new MailAddress(dt.Rows[0]["Email"].ToString());// 
                    MailAddress from = new MailAddress(dt.Rows[0]["UserIdtosendmail"].ToString(), dt.Rows[0]["EmailDisplayname"].ToString());//donot_reply@ijobcenter.com  **Sales Team IJobCenter 
                    MailMessage objEmail = new MailMessage(from, to);
                    objEmail.Subject = " " + banname + " banned globally for company " + companyid + "  ";
                    //IP Banned due to failed login attempt by company<company>
                    objEmail.Body = bodyformate.ToString();
                    objEmail.IsBodyHtml = true;
                    objEmail.Priority = MailPriority.High;
                    SmtpClient client = new SmtpClient();
                    client.Credentials = new NetworkCredential(dt.Rows[0]["UserIdtosendmail"].ToString(), dt.Rows[0]["Password"].ToString()); //donot_reply@ijobcenter.com  **Om2012++
                    client.Host = dt.Rows[0]["Mailserverurl"].ToString();
                    //client.Port = 587;
                    client.Send(objEmail);
                //}
                //catch (Exception ex)
                //{
                //}
        }
    }
    public void sendmailtoClient()
    {
        string str = " select distinct Priceplancategory.CategoryName,CompanyMaster.CompanyLoginId,  dbo.PricePlanMaster.EndDate, dbo.CompanyMaster.AdminId , dbo.CompanyMaster.ContactPersonDesignation,CompanyMaster.phone,CompanyMaster.Email,CompanyMaster.pincode,CompanyMaster.CompanyName,CompanyMaster.ContactPerson,CompanyMaster.city,CompanyMaster.Address,StateMasterTbl.StateName,CountryMaster.CountryName, PortalMasterTbl.*,PricePlanMaster.PricePlanAmount,PricePlanMaster.PricePlanName,PricePlanMaster.PricePlanId,OrderPaymentSatus.orderdate,OrderPaymentSatus.TransactionID from  CompanyMaster inner join PricePlanMaster  on CompanyMaster.PricePlanId=PricePlanMaster.PricePlanId inner join VersionInfoMaster on VersionInfoMaster.VersionInfoId=PricePlanMaster.VersionInfoMasterId inner join ProductMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId inner join ClientMaster on ProductMaster.ClientMasterId=ClientMaster.ClientMasterId inner join Priceplancategory on Priceplancategory.ID=PricePlanMaster.PriceplancatId inner join PortalMasterTbl on PortalMasterTbl.Id=Priceplancategory.PortalId   inner join OrderMaster on CompanyMaster.CompanyLoginId=OrderMaster.CompanyLoginId inner join  OrderPaymentSatus on OrderMaster.OrderId=OrderPaymentSatus.OrderId   inner join StateMasterTbl on StateMasterTbl.StateId=CompanyMaster.StateId inner join CountryMaster on StateMasterTbl.CountryId=CountryMaster.CountryId  WHERE (CompanyMaster.CompanyLoginId = '" + companyid + "') ";
         str = " select distinct Priceplancategory.CategoryName,CompanyMaster.CompanyLoginId,dbo.ClientMaster.EmailID AS clientEmail,  dbo.PricePlanMaster.EndDate, dbo.CompanyMaster.AdminId , dbo.CompanyMaster.ContactPersonDesignation,CompanyMaster.phone,CompanyMaster.Email,CompanyMaster.pincode,CompanyMaster.CompanyName,CompanyMaster.ContactPerson,CompanyMaster.city,CompanyMaster.Address,StateMasterTbl.StateName,CountryMaster.CountryName, PortalMasterTbl.*,PricePlanMaster.PricePlanAmount,PricePlanMaster.PricePlanName,PricePlanMaster.PricePlanId,OrderPaymentSatus.orderdate,OrderPaymentSatus.TransactionID from  CompanyMaster inner join PricePlanMaster  on CompanyMaster.PricePlanId=PricePlanMaster.PricePlanId inner join VersionInfoMaster on VersionInfoMaster.VersionInfoId=PricePlanMaster.VersionInfoMasterId inner join ProductMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId inner join ClientMaster on ProductMaster.ClientMasterId=ClientMaster.ClientMasterId inner join Priceplancategory on Priceplancategory.ID=PricePlanMaster.PriceplancatId inner join PortalMasterTbl on PortalMasterTbl.Id=Priceplancategory.PortalId   inner join OrderMaster on CompanyMaster.CompanyLoginId=OrderMaster.CompanyLoginId inner join  OrderPaymentSatus on OrderMaster.OrderId=OrderPaymentSatus.OrderId   inner join StateMasterTbl on StateMasterTbl.StateId=CompanyMaster.StateId inner join CountryMaster on StateMasterTbl.CountryId=CountryMaster.CountryId  WHERE (CompanyMaster.CompanyLoginId = '" + companyid + "') ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            DateTime now = DateTime.Now;
            string date = now.GetDateTimeFormats('d')[0];
            string time = now.GetDateTimeFormats('t')[0];

            StringBuilder strplan = new StringBuilder();


            strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
            //   strplan.Append("<tr><td align=\"left\"> <img src=\"http://" + Request.Url.Host.ToString() + "/Shoppingcart/images/" + dssecadmin.Rows[0]["logourl"].ToString() + "\" \"border=\"0\" Width=\"90\" Height=\"80\" / > </td ></tr>  ");
            strplan.Append("<br></table> ");

            strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
            // strplan.Append("<tr><td align=\"left\">Dear " + dssecadmin.Rows[0]["EmployeeName"].ToString() + ",</td></tr>  ");
            strplan.Append("<tr><td align=\"left\">Dear Master Admin,</td></tr>  ");
            strplan.Append("<tr><td align=\"left\"><br></td></tr>  ");
            strplan.Append("</table> ");

            strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
            strplan.Append("<tr><td align=\"left\">There is a potential hacking incident occurred and the " + banname + " is globally banned permanently  </td></tr> ");
            strplan.Append("<tr><td align=\"left\">The details of the company are as follows  </td></tr> ");
            strplan.Append("<tr><td align=\"left\" style=\"width: 20%\"><br>" + date + " at " + time + " globally banned  </td></tr>"); 
            strplan.Append("</table> ");

            strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
          
            strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Portal name:  </td><td align=\"left\" style=\"width: 80%\">" + dt.Rows[0]["PortalName"].ToString() + "</td></tr>");
            strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Price plan: </td><td align=\"left\" style=\"width: 80%\">" + dt.Rows[0]["PricePlanName"].ToString() + "</td></tr>");
            strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Price plan validity till: </td><td align=\"left\" style=\"width: 80%\">" + dt.Rows[0]["EndDate"].ToString() + "</td></tr>");
            strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Company name: </td><td align=\"left\" style=\"width: 80%\">" + dt.Rows[0]["CompanyName"].ToString() + "</td></tr>");
            strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Phone No: </td><td align=\"left\" style=\"width: 80%\">" + dt.Rows[0]["phone"].ToString() + "</td></tr>");
            strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">User ID: </td><td align=\"left\" style=\"width: 80%\">" + UserLoginLogID + "</td></tr>");
            strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">User name: </td><td align=\"left\" style=\"width: 80%\">" + UserLoginLogID + "</td></tr>");
            strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Computer name: </td><td align=\"left\" style=\"width: 80%\">" + computername + "</td></tr>");
            strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">IP: </td><td align=\"left\" style=\"width: 80%\">" + Ipaddress + "</td></tr>");
            strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Admin name: </td><td align=\"left\" style=\"width: 80%\">" + dt.Rows[0]["ContactPersonDesignation"].ToString() + "</td></tr>");
            strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Email: </td><td align=\"left\" style=\"width: 80%\">" + dt.Rows[0]["Email"].ToString() + "</td></tr>");
            strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Address: </td><td align=\"left\" style=\"width: 80%\">" + dt.Rows[0]["Address"].ToString() + ", " + dt.Rows[0]["city"].ToString() + ", " + dt.Rows[0]["StateName"].ToString() + ", " + dt.Rows[0]["CountryName"].ToString() + ", " + dt.Rows[0]["pincode"].ToString() + "</td></tr>");
            strplan.Append("</table> ");
            //Request.Url.Host.ToString()

            //strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
            //strplan.Append("<tr><td align=\"left\"><br><br> If you wish to see the temporary " + banname + " ban history please   <a href=http://" + Request.QueryString["url"].ToString() + "/LoginErrorLogBeforlogin.aspx?banid=" + Encrypted(Session["Bannid"].ToString()) + "&view=" + Encrypted(Ipaddress.ToString()) + "&CompId=" + Session["CompId"].ToString() + "&ipban=" + Request.QueryString["ipbanyesORno"] + " >here </a>  OR copy paste following link in your browser. </td></tr> ");
            //strplan.Append("<tr><td align=\"left\">http://" + Request.QueryString["url"].ToString() + "/LoginErrorLogBeforlogin.aspx?banid=" + Encrypted(Session["Bannid"].ToString()) + "&view=" + Encrypted(Ipaddress.ToString()) + "&CompId=" + Session["CompId"].ToString() + "&ipban=" + Request.QueryString["ipbanyesORno"] + "</td></tr> ");

            strplan.Append("<tr><td align=\"left\"><br> If you wish to see the permanent globally " + banname + " ban history please   <a href=http://" + Request.Url.Host.ToString() + "/Banreleaseview.aspx?banid=" + Encrypted(Session["Bannid"].ToString()) + "&view=" + Encrypted(Ipaddress.ToString()) + "&CompId=" + Session["CompId"].ToString() + "&ipban=" + Request.QueryString["ipbanyesORno"] + " >here </a>  OR copy paste following link in your browser. </td></tr> ");
            strplan.Append("<tr><td align=\"left\">http://" + Request.Url.Host.ToString() + "/Banreleaseview.aspx?banid=" + Encrypted(Session["Bannid"].ToString()) + "&view=" + Encrypted(Ipaddress.ToString()) + "&CompId=" + Session["CompId"].ToString() + "&ipban=" + Request.QueryString["ipbanyesORno"] + "</td></tr> ");
            strplan.Append("<tr><td align=\"left\"><br><br> </td></tr> ");


            //strplan.Append("<tr><td align=\"left\">If you wish to communicate to Company please click <a href=http://" + Request.QueryString["url"].ToString() + "/shoppingCart/Admin/MessageSentext.aspx?banid=" + Encrypted(Session["Bannid"].ToString()) + "&SendeRelReq=" + Encrypted(Ipaddress.ToString()) + "&CompId=" + Session["CompId"].ToString() + "&ipban=" + Request.QueryString["ipbanyesORno"] + "&ipban=" + Request.QueryString["ipbanyesORno"] + " >here </a> . OR copy paste following link in your browser.	</td></tr> ");
            //strplan.Append("<tr><td align=\"left\">http://" + Request.QueryString["url"].ToString() + "/shoppingCart/Admin/MessageSentext.aspx?banid=" + Encrypted(Session["Bannid"].ToString()) + "&SendeRelReq=" + Encrypted(Ipaddress) + "&CompId=" + Session["CompId"].ToString() + "&ipban=" + Request.QueryString["ipbanyesORno"] + "&ipban=" + Request.QueryString["ipbanyesORno"] + " </td></tr> ");

            strplan.Append("<tr><td align=\"left\"><br>If you wish release the " + banname + " banned globally please click <a href=http://" + Request.Url.Host.ToString() + "/InsertPermenatalybanSilent.aspx?banid=" + Encrypted(Session["Bannid"].ToString()) + "&Banrelease=" + Encrypted(Ipaddress.ToString()) + "&CompId=" + Session["CompId"].ToString() + "&ipban=" + Request.QueryString["ipbanyesORno"] + "&ipban=" + Request.QueryString["ipbanyesORno"] + " > here </a> . OR copy paste following link in your browser </td></tr> ");
            strplan.Append("<tr><td align=\"left\">http://" + Request.Url.Host.ToString() + "/InsertPermenatalybanSilent.aspx?banid=" + Encrypted(Session["Bannid"].ToString()) + "&Banrelease=" + Encrypted(Ipaddress.ToString()) + "&CompId=" + Session["CompId"].ToString() + "&ipban=" + Request.QueryString["ipbanyesORno"] + " </td></tr> ");
            //strplan.Append("<tr><td align=\"left\"><br> The failed login attempt from " + banname + " is an unintentional error made by our user and it is not related to hacking.  </td></tr> ");
            //strplan.Append("<tr><td align=\"left\"> In future such failed login attempts will not cross the allowed limit. </td></tr> ");
            //strplan.Append("<tr><td align=\"left\"> <br> Please click <a href=http://" + Request.Url.Host.ToString() + "/Banreleaseview.aspx?view=" + Encrypted(Ipaddress.ToString()) + "&CompId=" + Session["CompId"].ToString() + " >here </a> to View IP Ban History OR copy paste following link in your browser.</td></tr> ");
            //strplan.Append("<tr><td align=\"left\">http://" + Request.Url.Host.ToString() + "/Banreleaseview.aspx?view=" + Encrypted(Ipaddress.ToString()) + "&CompId=" + Session["CompId"].ToString() + "</td></tr> ");     
           
            strplan.Append("</table> ");
            strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
            strplan.Append("<tr><td align=\"left\"><br><br><br>Thanking you,</td></tr>  ");
            //strplan.Append("<tr><td align=\"left\">Sincerely ,</td></tr>  ");
            strplan.Append("<tr><td align=\"left\">Sincerely.</td></tr>  ");
            strplan.Append("</table> ");

            //strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
            //strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Your IP "+ Ipaddress +" is banned for further login due to number of unsuccessful login attempts crossed the permissible limit. If you are sure that the failed login attempt is due to unintentional error by one of your user and it is not a hacking attempt, you may click here for request to reactivate the banned IP.</td><td align=\"left\" style=\"width: 80%\"></td></tr>");
            //strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">User Name :</td><td align=\"left\" style=\"width: 80%\">" + username + " </td></tr>");
            //strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Portal Name :</td><td align=\"left\" style=\"width: 80%\">" + portalname + "</td></tr>");
            //strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Computer Name :</td><td align=\"left\" style=\"width: 80%\">" + computername + "</td></tr>");
            //strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Public IP :</td><td align=\"left\" style=\"width: 80%\">" + publicip + "</td></tr>");
            //strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Local IP :</td><td align=\"left\" style=\"width: 80%\">" + localip + " </td></tr>");
            //strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Last Attempt Date and Time  :</td><td align=\"left\" style=\"width: 80%\">" + DateTime.Now.ToString() + "</td></tr>");
            //strplan.Append("</table> ");
            string bodyformate = "" + strplan + "";
            //try
            //{

            MailAddress to = new MailAddress(dt.Rows[0]["clientEmail"].ToString());// dt.Rows[0]["dt.Rows[0]["clientEmail"].ToString()"].ToString()
                MailAddress from = new MailAddress(dt.Rows[0]["UserIdtosendmail"].ToString(), dt.Rows[0]["EmailDisplayname"].ToString());// dt.Rows[0]["EmailDisplayname"].ToString() donot_reply@ijobcenter.com  **Sales Team IJobCenter 
                MailMessage objEmail = new MailMessage(from, to);
                objEmail.Subject = " "+banname+" banned globally for company "+companyid+"  ";
                objEmail.Body = bodyformate.ToString();
                objEmail.IsBodyHtml = true;
                objEmail.Priority = MailPriority.High;
                SmtpClient client = new SmtpClient();
                client.Credentials = new NetworkCredential(dt.Rows[0]["UserIdtosendmail"].ToString(), dt.Rows[0]["Password"].ToString()); //donot_reply@ijobcenter.com  **Om2012++
                client.Host = dt.Rows[0]["Mailserverurl"].ToString();
                //client.Port = 587;
                client.Send(objEmail);
            //}
            //catch (Exception ex)
            //{
            //}

        }
    }

    public void SendreleaseRequest()
    {
        string str = " select distinct Priceplancategory.CategoryName,CompanyMaster.CompanyLoginId,  dbo.PricePlanMaster.EndDate, dbo.CompanyMaster.AdminId , dbo.CompanyMaster.ContactPersonDesignation,CompanyMaster.phone,CompanyMaster.Email,CompanyMaster.pincode,CompanyMaster.CompanyName,CompanyMaster.ContactPerson,CompanyMaster.city,CompanyMaster.Address,StateMasterTbl.StateName,CountryMaster.CountryName, PortalMasterTbl.*,PricePlanMaster.PricePlanAmount,PricePlanMaster.PricePlanName,PricePlanMaster.PricePlanId,OrderPaymentSatus.orderdate,OrderPaymentSatus.TransactionID from  CompanyMaster inner join PricePlanMaster  on CompanyMaster.PricePlanId=PricePlanMaster.PricePlanId inner join VersionInfoMaster on VersionInfoMaster.VersionInfoId=PricePlanMaster.VersionInfoMasterId inner join ProductMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId inner join ClientMaster on ProductMaster.ClientMasterId=ClientMaster.ClientMasterId inner join Priceplancategory on Priceplancategory.ID=PricePlanMaster.PriceplancatId inner join PortalMasterTbl on PortalMasterTbl.Id=Priceplancategory.PortalId   inner join OrderMaster on CompanyMaster.CompanyLoginId=OrderMaster.CompanyLoginId inner join  OrderPaymentSatus on OrderMaster.OrderId=OrderPaymentSatus.OrderId   inner join StateMasterTbl on StateMasterTbl.StateId=CompanyMaster.StateId inner join CountryMaster on StateMasterTbl.CountryId=CountryMaster.CountryId  WHERE (CompanyMaster.CompanyLoginId = '" + companyid + "') ";
         str = " select distinct Priceplancategory.CategoryName,CompanyMaster.CompanyLoginId,dbo.ClientMaster.EmailID AS clientEmail,  dbo.PricePlanMaster.EndDate, dbo.CompanyMaster.AdminId , dbo.CompanyMaster.ContactPersonDesignation,CompanyMaster.phone,CompanyMaster.Email,CompanyMaster.pincode,CompanyMaster.CompanyName,CompanyMaster.ContactPerson,CompanyMaster.city,CompanyMaster.Address,StateMasterTbl.StateName,CountryMaster.CountryName, PortalMasterTbl.*,PricePlanMaster.PricePlanAmount,PricePlanMaster.PricePlanName,PricePlanMaster.PricePlanId,OrderPaymentSatus.orderdate,OrderPaymentSatus.TransactionID from  CompanyMaster inner join PricePlanMaster  on CompanyMaster.PricePlanId=PricePlanMaster.PricePlanId inner join VersionInfoMaster on VersionInfoMaster.VersionInfoId=PricePlanMaster.VersionInfoMasterId inner join ProductMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId inner join ClientMaster on ProductMaster.ClientMasterId=ClientMaster.ClientMasterId inner join Priceplancategory on Priceplancategory.ID=PricePlanMaster.PriceplancatId inner join PortalMasterTbl on PortalMasterTbl.Id=Priceplancategory.PortalId   inner join OrderMaster on CompanyMaster.CompanyLoginId=OrderMaster.CompanyLoginId inner join  OrderPaymentSatus on OrderMaster.OrderId=OrderPaymentSatus.OrderId   inner join StateMasterTbl on StateMasterTbl.StateId=CompanyMaster.StateId inner join CountryMaster on StateMasterTbl.CountryId=CountryMaster.CountryId  WHERE (CompanyMaster.CompanyLoginId = '" + companyid + "') ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            try
            {
            DateTime now = DateTime.Now;
            string date = now.GetDateTimeFormats('d')[0];
            string time = now.GetDateTimeFormats('t')[0];

            StringBuilder strplan = new StringBuilder();
            strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
            //   strplan.Append("<tr><td align=\"left\"> <img src=\"http://" + Request.Url.Host.ToString() + "/Shoppingcart/images/" + dssecadmin.Rows[0]["logourl"].ToString() + "\" \"border=\"0\" Width=\"90\" Height=\"80\" / > </td ></tr>  ");
            strplan.Append("<br></table> ");

            strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
            // strplan.Append("<tr><td align=\"left\">Dear " + dssecadmin.Rows[0]["EmployeeName"].ToString() + ",</td></tr>  ");
            strplan.Append("<tr><td align=\"left\">Dear Master Admin,</td></tr>  ");
            strplan.Append("<tr><td align=\"left\"><br></td></tr>  ");
            strplan.Append("</table> ");

            strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
            strplan.Append("<tr><td align=\"left\">There was a potential hacking incident occurred for company " + companyid + ",   user name " + UserLoginLogID + " </td></tr> ");
            strplan.Append("<tr><td align=\"left\">employee " + employeename + " " + banname + " On " + date + " at " + time + ". </td></tr> ");
            strplan.Append("<tr><td align=\"left\"><br>" + date + " at " + time + "  globally banned</td></tr> ");
            strplan.Append("</table> ");

            strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
            strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Department and Designation:  </td><td align=\"left\" style=\"width: 80%\">" + dt.Rows[0]["PortalName"].ToString() + "</td></tr>");
            strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">User ID: </td><td align=\"left\" style=\"width: 80%\">" + UserLoginLogID + "</td></tr>");
            strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Employee Name: </td><td align=\"left\" style=\"width: 80%\">" + employeename + "</td></tr>");
            strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Computer name: </td><td align=\"left\" style=\"width: 80%\">" + computername + "</td></tr>");
            strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">IP: </td><td align=\"left\" style=\"width: 80%\">" + Ipaddress + "</td></tr>");
            strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Phone No: </td><td align=\"left\" style=\"width: 80%\">" + dt.Rows[0]["phone"].ToString() + "</td></tr>");
            strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Email: </td><td align=\"left\" style=\"width: 80%\">" + dt.Rows[0]["Email"].ToString() + "</td></tr>");
            strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Address: </td><td align=\"left\" style=\"width: 80%\">" + dt.Rows[0]["Address"].ToString() + ", " + dt.Rows[0]["city"].ToString() + ", " + dt.Rows[0]["StateName"].ToString() + ", " + dt.Rows[0]["CountryName"].ToString() + ", " + dt.Rows[0]["pincode"].ToString() + "</td></tr>");                        
            strplan.Append("</table> ");

            strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
            //strplan.Append("<tr><td align=\"left\"> " + banname + " have done unsuccessful login attempts which crossed the permissible limit and hence the " + banname + " is banned globally for further login. </td></tr> ");
            strplan.Append("<tr><td align=\"left\"><br> </td></tr> ");


            strplan.Append("<tr><td align=\"left\"> The failed login attempt from " + banname + " is an unintentional error made by our user and it is not related to hacking. In future such failed login attempts will not cross the allowed limit.  </td></tr> ");
            strplan.Append("<tr><td align=\"left\"> our explanation for the incident is as follows .  </td></tr> ");
            strplan.Append("<tr><td align=\"left\"> "+txtdescription.Text+"  </td></tr> ");

            //strplan.Append("<tr><td align=\"left\"><br><br> If you wish to see the temporary " + banname + " ban history please   <a href=http://" + Request.QueryString["url"].ToString() + "/LoginErrorLogBeforlogin.aspx?banid=" + Encrypted(Session["Bannid"].ToString()) + "&view=" + Encrypted(Ipaddress.ToString()) + "&CompId=" + Session["CompId"].ToString() + "&ipban=" + Request.QueryString["ipbanyesORno"] + " >here </a>  OR copy paste following link in your browser. </td></tr> ");
            //strplan.Append("<tr><td align=\"left\">http://" + Request.QueryString["url"].ToString() + "/LoginErrorLogBeforlogin.aspx?banid=" + Encrypted(Session["Bannid"].ToString()) + "&view=" + Encrypted(Ipaddress.ToString()) + "&CompId=" + Session["CompId"].ToString() + "&ipban=" + Request.QueryString["ipbanyesORno"] + "</td></tr> ");

            strplan.Append("<tr><td align=\"left\"><br> If you wish to see the permanent globally " + banname + " ban history please <a href=http://" + Request.Url.Host.ToString() + "/Banreleaseview.aspx?banid=" + Encrypted(Session["Bannid"].ToString()) + "&view=" + Encrypted(Ipaddress.ToString()) + "&CompId=" + Session["CompId"].ToString() + "&ipban=" + Request.QueryString["ipbanyesORno"] + " >here </a>  OR copy paste following link in your browser. </td></tr> ");
            strplan.Append("<tr><td align=\"left\">http://" + Request.Url.Host.ToString() + "/Banreleaseview.aspx?banid=" + Encrypted(Session["Bannid"].ToString()) + "&view=" + Encrypted(Ipaddress.ToString()) + "&CompId=" + Session["CompId"].ToString() + "&ipban=" + Request.QueryString["ipbanyesORno"] + "</td></tr> ");

          
            //strplan.Append("<tr><td align=\"left\"><br> </td></tr> ");

            //strplan.Append("<tr><td align=\"left\"><br>If you wish to communicate with Company Admin please click <a href=http://" + Request.QueryString["url"].ToString() + "/shoppingCart/Admin/MessageSentext.aspx?banid=" + Encrypted(Session["Bannid"].ToString()) + "&SendeRelReq=" + Encrypted(Ipaddress.ToString()) + "&CompId=" + Session["CompId"].ToString() + "&ipban=" + Request.QueryString["ipbanyesORno"] + " >here </a> . OR copy paste following link in your browser.	</td></tr> ");
            //strplan.Append("<tr><td align=\"left\">http://" + Request.QueryString["url"].ToString() + "/shoppingCart/Admin/MessageSentext.aspx?banid=" + Encrypted(Session["Bannid"].ToString()) + "&SendeRelReq=" + Encrypted(Ipaddress) + "&CompId=" + Session["CompId"].ToString() + "&ipban=" + Request.QueryString["ipbanyesORno"] + " </td></tr> ");


            strplan.Append("<tr><td align=\"left\"><br>If you wish release the " + banname + " banned globally please click <a href=http://" + Request.Url.Host.ToString() + "/InsertPermenatalybanSilent.aspx?banid=" + Encrypted(Session["Bannid"].ToString()) + "&Banrelease=" + Encrypted(Ipaddress.ToString()) + "&CompId=" + Session["CompId"].ToString() + "&ipban=" + Request.QueryString["ipbanyesORno"] + " > here </a> . OR copy paste following link in your browser </td></tr> ");
            strplan.Append("<tr><td align=\"left\">http://" + Request.Url.Host.ToString() + "/InsertPermenatalybanSilent.aspx?banid=" + Encrypted(Session["Bannid"].ToString()) + "&Banrelease=" + Encrypted(Ipaddress.ToString()) + "&CompId=" + Session["CompId"].ToString() + "&ipban=" + Request.QueryString["ipbanyesORno"] + " </td></tr> ");

            // strplan.Append("<tr><td align=\"left\"> The failed login attempt from IP "+Ipaddress+" is an unintentional error made by one of our user and it is not related to hacking. In future such failed login attempts will not cross the allowed limit. </td></tr> ");
            //strplan.Append("<tr><td align=\"left\"> You are requested to reactivate the banned IP.</td></tr> ");
            //strplan.Append("<tr><td align=\"left\">If you like to Release Ban From List of bann Ipaddress <a href=http://" + Request.Url.Host.ToString() + "/Banrelease.aspx?a=" + ClsEncDesc.Encrypted(Ipaddress.ToString()) + " >here </a> OR copy paste following link in your browser.</td></tr> ");
            //strplan.Append("<tr><td align=\"left\">http://" + Request.Url.Host.ToString() + "/Banrelease.aspx?a=" + ClsEncDesc.Encrypted(Ipaddress.ToString()) + "</td></tr> ");
            //strplan.Append("</table> ");


            //strplan.Append("<tr><td align=\"left\"> The failed login attempt from IP "+Ipaddress+" is an unintentional error made by one of our user and it is not related to hacking. In future such failed login attempts will not cross the allowed limit. </td></tr> ");
            //strplan.Append("<tr><td align=\"left\"> You are requested to reactivate the banned IP.</td></tr> ");
            //strplan.Append("<tr><td align=\"left\">If you like to Release Ban From List of bann Ipaddress <a href=http://" + Request.Url.Host.ToString() + "/Banrelease.aspx?a=" + ClsEncDesc.Encrypted(Ipaddress.ToString()) + " >here </a> OR copy paste following link in your browser.</td></tr> ");
            //strplan.Append("<tr><td align=\"left\">http://" + Request.Url.Host.ToString() + "/Banrelease.aspx?a=" + ClsEncDesc.Encrypted(Ipaddress.ToString()) + "</td></tr> ");

            strplan.Append("</table> ");
            strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
            strplan.Append("<tr><td align=\"left\"><br><br><br>Thank you,</td></tr>  ");
            //strplan.Append("<tr><td align=\"left\">Sincerely ,</td></tr>  ");
            strplan.Append("<tr><td align=\"left\">Sincerely</td></tr>  ");
            strplan.Append("</table> ");

            //strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
            //strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Your IP "+ Ipaddress +" is banned for further login due to number of unsuccessful login attempts crossed the permissible limit. If you are sure that the failed login attempt is due to unintentional error by one of your user and it is not a hacking attempt, you may click here for request to reactivate the banned IP.</td><td align=\"left\" style=\"width: 80%\"></td></tr>");
            //strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">User Name :</td><td align=\"left\" style=\"width: 80%\">" + username + " </td></tr>");
            //strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Portal Name :</td><td align=\"left\" style=\"width: 80%\">" + portalname + "</td></tr>");
            //strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Computer Name :</td><td align=\"left\" style=\"width: 80%\">" + computername + "</td></tr>");
            //strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Public IP :</td><td align=\"left\" style=\"width: 80%\">" + publicip + "</td></tr>");
            //strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Local IP :</td><td align=\"left\" style=\"width: 80%\">" + localip + " </td></tr>");
            //strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Last Attempt Date and Time  :</td><td align=\"left\" style=\"width: 80%\">" + DateTime.Now.ToString() + "</td></tr>");
            //strplan.Append("</table> ");
            string bodyformate = "" + strplan + "";
          

            MailAddress to = new MailAddress(dt.Rows[0]["clientEmail"].ToString());// dt.Rows[0]["clientEmail"].ToString()
            MailAddress from = new MailAddress(dt.Rows[0]["UserIdtosendmail"].ToString(), "Release "+banname+" Request");//donot_reply@ijobcenter.com  **Sales Team IJobCenter 
            MailMessage objEmail = new MailMessage(from, to);
            objEmail.Subject = " " + banname + " banned globally for company " + companyid + " ";
            objEmail.Body = bodyformate.ToString();
            objEmail.IsBodyHtml = true;
            objEmail.Priority = MailPriority.High;
            SmtpClient client = new SmtpClient();
            client.Credentials = new NetworkCredential(dt.Rows[0]["UserIdtosendmail"].ToString(), dt.Rows[0]["Password"].ToString()); //donot_reply@ijobcenter.com  **Om2012++
            client.Host = dt.Rows[0]["Mailserverurl"].ToString();
            //client.Port = 587;
            client.Send(objEmail);
            }
            catch (Exception ex)
            {
            }

        }
    }

    protected void removeiprestriction()
    {
        try
        {
            string strupdate = "  update Permenatalybanmacaddressandipadress set bannedmacaddress='0',bannedipaddress='0' where Ipaddress='" + Ipaddress + "' ";// 
            SqlCommand cmdupdate = new SqlCommand(strupdate, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdupdate.ExecuteNonQuery();
            con.Close();
        }
        catch
        {

        }
    }

    public void SendMailToCompanyAdmin_Release()
    {
        string str = " select distinct Priceplancategory.CategoryName,CompanyMaster.CompanyLoginId,CompanyMaster.phone,CompanyMaster.Email,CompanyMaster.pincode,CompanyMaster.CompanyName,CompanyMaster.ContactPerson,CompanyMaster.city,CompanyMaster.Address,StateMasterTbl.StateName,CountryMaster.CountryName, PortalMasterTbl.*,PricePlanMaster.PricePlanAmount,PricePlanMaster.PricePlanName,PricePlanMaster.PricePlanId,OrderPaymentSatus.orderdate,OrderPaymentSatus.TransactionID from  CompanyMaster inner join PricePlanMaster  on CompanyMaster.PricePlanId=PricePlanMaster.PricePlanId inner join VersionInfoMaster on VersionInfoMaster.VersionInfoId=PricePlanMaster.VersionInfoMasterId inner join ProductMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId inner join ClientMaster on ProductMaster.ClientMasterId=ClientMaster.ClientMasterId inner join Priceplancategory on Priceplancategory.ID=PricePlanMaster.PriceplancatId inner join PortalMasterTbl on PortalMasterTbl.Id=Priceplancategory.PortalId   inner join OrderMaster on CompanyMaster.CompanyLoginId=OrderMaster.CompanyLoginId inner join  OrderPaymentSatus on OrderMaster.OrderId=OrderPaymentSatus.OrderId   inner join StateMasterTbl on StateMasterTbl.StateId=CompanyMaster.StateId inner join CountryMaster on StateMasterTbl.CountryId=CountryMaster.CountryId  WHERE (CompanyMaster.CompanyLoginId = '" + Session["CompId"].ToString()  + "') ";
         str = " select distinct Priceplancategory.CategoryName,CompanyMaster.CompanyLoginId,dbo.ClientMaster.EmailID AS clientEmail,  dbo.PricePlanMaster.EndDate, dbo.CompanyMaster.AdminId , dbo.CompanyMaster.ContactPersonDesignation,CompanyMaster.phone,CompanyMaster.Email,CompanyMaster.pincode,CompanyMaster.CompanyName,CompanyMaster.ContactPerson,CompanyMaster.city,CompanyMaster.Address,StateMasterTbl.StateName,CountryMaster.CountryName, PortalMasterTbl.*,PricePlanMaster.PricePlanAmount,PricePlanMaster.PricePlanName,PricePlanMaster.PricePlanId,OrderPaymentSatus.orderdate,OrderPaymentSatus.TransactionID from  CompanyMaster inner join PricePlanMaster  on CompanyMaster.PricePlanId=PricePlanMaster.PricePlanId inner join VersionInfoMaster on VersionInfoMaster.VersionInfoId=PricePlanMaster.VersionInfoMasterId inner join ProductMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId inner join ClientMaster on ProductMaster.ClientMasterId=ClientMaster.ClientMasterId inner join Priceplancategory on Priceplancategory.ID=PricePlanMaster.PriceplancatId inner join PortalMasterTbl on PortalMasterTbl.Id=Priceplancategory.PortalId   inner join OrderMaster on CompanyMaster.CompanyLoginId=OrderMaster.CompanyLoginId inner join  OrderPaymentSatus on OrderMaster.OrderId=OrderPaymentSatus.OrderId   inner join StateMasterTbl on StateMasterTbl.StateId=CompanyMaster.StateId inner join CountryMaster on StateMasterTbl.CountryId=CountryMaster.CountryId  WHERE (CompanyMaster.CompanyLoginId = '" + companyid + "') ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            StringBuilder strplan = new StringBuilder();
            strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
            //   strplan.Append("<tr><td align=\"left\"> <img src=\"http://" + Request.Url.Host.ToString() + "/Shoppingcart/images/" + dssecadmin.Rows[0]["logourl"].ToString() + "\" \"border=\"0\" Width=\"90\" Height=\"80\" / > </td ></tr>  ");
            strplan.Append("<br></table> ");

            strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
            // strplan.Append("<tr><td align=\"left\">Dear " + dssecadmin.Rows[0]["EmployeeName"].ToString() + ",</td></tr>  ");
            strplan.Append("<tr><td align=\"left\">Dear Admin,</td></tr>  ");
            strplan.Append("<tr><td align=\"left\"><br></td></tr>  ");
            strplan.Append("</table> ");
            DateTime now = DateTime.Now;
            string date = now.GetDateTimeFormats('d')[0];
            string time = now.GetDateTimeFormats('t')[0];


            strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
            strplan.Append("<tr><td align=\"left\"><br>" + date + " at " + time + "  globally banned</td></tr> ");
            strplan.Append("</table> ");

            strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
            strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Department and Designation:  </td><td align=\"left\" style=\"width: 80%\">" + dt.Rows[0]["PortalName"].ToString() + "</td></tr>");
            strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">User ID: </td><td align=\"left\" style=\"width: 80%\">" + UserLoginLogID + "</td></tr>");
            strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Employee Name: </td><td align=\"left\" style=\"width: 80%\">" + employeename + "</td></tr>");
            strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Computer name: </td><td align=\"left\" style=\"width: 80%\">" + computername + "</td></tr>");
            strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">IP: </td><td align=\"left\" style=\"width: 80%\">" + Ipaddress + "</td></tr>");
            strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Phone No: </td><td align=\"left\" style=\"width: 80%\">" + dt.Rows[0]["phone"].ToString() + "</td></tr>");
            strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Email: </td><td align=\"left\" style=\"width: 80%\">" + dt.Rows[0]["Email"].ToString() + "</td></tr>");
            strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Address: </td><td align=\"left\" style=\"width: 80%\">" + dt.Rows[0]["Address"].ToString() + ", " + dt.Rows[0]["city"].ToString() + ", " + dt.Rows[0]["StateName"].ToString() + ", " + dt.Rows[0]["CountryName"].ToString() + ", " + dt.Rows[0]["pincode"].ToString() + "</td></tr>");
            strplan.Append("</table> ");



            strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");           

            //strplan.Append("<tr><td align=\"left\"> Your Company "+companyid +" "+banname+" banned globally On < Date> at <Time> due to unsuccessful login attempts  </td></tr> ");
            strplan.Append("<tr><td align=\"left\"><br> Your Company " + companyid + " " + banname + " banned globally On " + date + " at " + time + " due to unsuccessful login attempts which crossed the permissible limit is released based on your request received. </td></tr> ");
            strplan.Append("<tr><td align=\"left\"> Please login with correct username/ password to avoid permanent ban of "+banname+" globally.	</td></tr> ");            
            strplan.Append("</table> ");
            strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
            strplan.Append("<tr><td align=\"left\"><br><br><br>Thank you,</td></tr>  ");
            //strplan.Append("<tr><td align=\"left\">Sincerely ,</td></tr>  ");
            strplan.Append("<tr><td align=\"left\">Sincerely </td></tr>  ");
            strplan.Append("</table> ");

                     
            string bodyformate = "" + strplan + "";
            //try
            //{

            MailAddress to = new MailAddress(dt.Rows[0]["Email"].ToString());// dt.Rows[0]["Email"].ToString()
                MailAddress from = new MailAddress(dt.Rows[0]["UserIdtosendmail"].ToString(), "Master Admin");//donot_reply@ijobcenter.com  **Sales Team IJobCenter 
                MailMessage objEmail = new MailMessage(from, to);
                objEmail.Subject = " Lift of ban on Globally banned "+banname+" ";
                //IP Banned due to failed login attempt by company<company>
                objEmail.Body = bodyformate.ToString();
                objEmail.IsBodyHtml = true;
                objEmail.Priority = MailPriority.High;
                SmtpClient client = new SmtpClient();
                client.Credentials = new NetworkCredential(dt.Rows[0]["UserIdtosendmail"].ToString(), dt.Rows[0]["Password"].ToString()); //donot_reply@ijobcenter.com  **Om2012++
                client.Host = dt.Rows[0]["Mailserverurl"].ToString();
                //client.Port = 587;
                client.Send(objEmail);
            //}
            //catch (Exception ex)
            //{
            //}
        }
    }
    //--------------------------------------------------------

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (txtdescription.Text != null)
        {
            companyid = (Request.QueryString["CompId"]);
            Session["CompId"] = (Request.QueryString["CompId"]);
            createwebsiteandattach(companyid);   

            companyid = (Request.QueryString["CompId"]);
            Ipaddress = Request.QueryString["SendeRelReq"].ToString();
            Ipaddress = Ipaddress.Replace(' ', '+');
            Ipaddress = DecryptStringServer(Ipaddress);
            banid = (Request.QueryString["banid"].ToString());
            banid = banid.Replace(' ', '+');
            banid = (DecryptStringServer(banid));            
            Session["Bannid"] = banid;
          

            string selectlogin = "select * from Permenatalybanmacaddressandipadress where id=" + Session["Bannid"] + " ";
            SqlCommand cmdselect = new SqlCommand(selectlogin, con);
            SqlDataAdapter sdaselect = new SqlDataAdapter(cmdselect);
            DataTable dtlogin = new DataTable();
            sdaselect.Fill(dtlogin);
            Ipaddress = dtlogin.Rows[0]["Ipaddress"].ToString();//
            computername = dtlogin.Rows[0]["computername"].ToString();//
            employeename = dtlogin.Rows[0]["EmpName"].ToString();//
            UserLoginLogID = dtlogin.Rows[0]["UserLoginLogID"].ToString();//
            MacAddress = dtlogin.Rows[0]["MacAddress"].ToString();//
            if (Request.QueryString["ipbanyesORno"] == "yes")//Permenataly ban ipadress and Sync
            {
                banname = " IP " + Ipaddress;
                Session["banname"] = banname;
            }
            else
            {
                banname = " MacAddress " + MacAddress;
                Session["banname"] = banname;
            }


            string insertpermenant = "Update Permenatalybanmacaddressandipadress SET BanReason='" + txtdescription.Text + "' Where Id=" + Session["Bannid"] + "";
            SqlCommand cmdpermenant = new SqlCommand(insertpermenant, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdpermenant.ExecuteNonQuery();


            Pnl1.Visible = false;
            SendreleaseRequest();
            Label1.Text = " Sending Release " + Session["banname"] + " address Request to Master Admin ";  
        }
    }
    public static string Encrypted(string strText)
    {
        return Encrypt(strText, Encryptkeycompsss);      
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
    public static string DecryptStringServer(string str)
    {
        return DecryptServServer(str, Encryptkeycompsss);
    }
    private static string DecryptServServer(string strText, string strEncrypt)
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
            return "";
        }

    }
    public static string serverEncrypted(string strText)
    {
        return Encrypt(strText, Serverencstr);
    }
}
