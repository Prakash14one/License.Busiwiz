﻿using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Data.Common;


/// <summary>
/// Summary description for PageConn
/// </summary>
public class PageConn
{
    // SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString1"].ConnectionString);

    //SqlConnection con;
    public static string connnn;
    public SqlConnection dynconn;
    public static string strEnc = "";
    public static string bidname = "";
    public static string busdatabase = "";
    public static string Userrnamedb = "";
    public static string intmsg11 = "";
    public static string extmsg11 = "";
   // public SqlConnection servermasterconn;

    public PageConn()
    {
        dynconn = new SqlConnection();
       
        string strcln = "Select * from CompanyDatabaseDetailTbl where CompanyLoginId='" + Convert.ToString(HttpContext.Current.Session["Comid"]) + "' and CodeType='OADB' ";
        SqlCommand cmdcln = new SqlCommand(strcln, serverconn());
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);

        if (dtcln.Rows.Count > 0)
        {
            //OADB_Developer
        }
        dynconn.ConnectionString = "Data Source =TCP:72.38.84.230,30000; Initial Catalog = jobcenter.OADB_Developer; User ID=sa; Password=06De1963++; Persist Security Info=true;";
        dynconn.ConnectionString = "Data Source =TCP:192.168.10.42,40000; Initial Catalog = jobcenter.OADB_Developer; User ID=AMDDeveloper; Password=Om2012++; Persist Security Info=true;";
        dynconn.ConnectionString = "Data Source =TCP:72.38.84.230,30000; Initial Catalog = jobcenter.OADB; User ID=sa; Password=06De1963++; Persist Security Info=true;";  
        connnn = dynconn.ConnectionString.ToString();

        busdatabase = "[" + dynconn.Database + "]";
        HttpContext.Current.Session["dyconn"] = dynconn;
        intmsg();
        extmsg();

    }

    public static SqlConnection licenseconn()
    { 
        SqlConnection liceco = new SqlConnection();
      liceco.ConnectionString = "Data Source=TCP:72.38.84.230,2810;Initial Catalog=License.BusiwizDeveloper; User ID=sa; Password=06De1963++; Persist Security Info=true; ";
      liceco.ConnectionString = "Data Source =TCP:192.168.10.42,40000; Initial Catalog = License.BusiwizDeveloper; User ID=AMDDeveloper; Password=Om2012++; Persist Security Info=true;";
      liceco.ConnectionString = "Data Source=TCP:72.38.84.230,2810;Initial Catalog=License.Busiwiz; User ID=sa; Password=06De1963++; Persist Security Info=true; ";
        return liceco;
    }

    public static SqlConnection serverconn()
    {
        SqlConnection servermasterconn = new SqlConnection();

        string strcomp = "select ServerMasterTbl.*,CompanyMaster.Encryptkeycomp from CompanyMaster inner join ServerMasterTbl on ServerMasterTbl.Id=CompanyMaster.ServerId where CompanyMaster.CompanyLoginId='" + Convert.ToString(HttpContext.Current.Session["Comid"]) + "'  ";
        SqlCommand cmdcomp = new SqlCommand(strcomp, licenseconn());
        DataTable dtcomp = new DataTable();
        SqlDataAdapter adpcomp = new SqlDataAdapter(cmdcomp);
        adpcomp.Fill(dtcomp);

        if (dtcomp.Rows.Count > 0)
        {

         
            strEnc = Convert.ToString(dtcomp.Rows[0]["Encryptkeycomp"]);

        }
        //C3SATELLITESERVER_Developer
        servermasterconn.ConnectionString = @"Data Source =TCP:72.38.84.230,30000; Initial Catalog = C3SATELLITESERVER_Developer; User ID=sa; Password=06De1963++; Persist Security Info=true;";
       servermasterconn.ConnectionString =   "Data Source =TCP:192.168.10.42,40000; Initial Catalog = C3SATELLITESERVER_Developer; User ID=AMDDeveloper; Password=Om2012++; Persist Security Info=true;";
       servermasterconn.ConnectionString =  @"Data Source =TCP:72.38.84.230,30000; Initial Catalog = C3SATELLITESERVER; User ID=sa; Password=06De1963++; Persist Security Info=true;";
                                            //Data Source =C3\C3SERVERMASTER,30000; Initial Catalog = C3SATELLITESERVER; User ID=sa; Password=06De1963++; Persist Security Info=true; 
        return servermasterconn;
    }


    public static SqlConnection busclient()
    {
        SqlConnection bus = new SqlConnection();

        

        string strcomid = " Select * from CompanyDatabaseDetailTbl where CompanyLoginId='" + Convert.ToString(HttpContext.Current.Session["Comid"]) + "' and CodeType='BUSICONTROLDB' ";
        SqlDataAdapter adcheck1 = new SqlDataAdapter(strcomid, serverconn());
        DataTable dtcheck1 = new DataTable();
        adcheck1.Fill(dtcheck1);
        //_Developer
        if (dtcheck1.Rows.Count > 0)
        {
            bus = new SqlConnection();
            bus.ConnectionString = @"Data Source =TCP:72.38.84.230,30000; Initial Catalog = jobcenter.BUSICONTROLDB; User ID=sa; Password=06De1963++; Persist Security Info=true;";
            bus.ConnectionString = @"Data Source =TCP:72.38.84.230,30000; Initial Catalog = jobcenter.BUSICONTROLDB; User ID=sa; Password=06De1963++; Persist Security Info=true;";
            bidname = "[" + Convert.ToString(dtcheck1.Rows[0]["DatabaseName"]) + "]";
           
        }
          bus.ConnectionString = @"Data Source =TCP:72.38.84.230,30000; Initial Catalog = jobcenter.BUSICONTROLDB_Developer; User ID=sa; Password=06De1963++; Persist Security Info=true;";
          bus.ConnectionString = "Data Source =TCP:192.168.10.42,40000; Initial Catalog = jobcenter.BUSICONTROLDB_Developer; User ID=AMDDeveloper; Password=Om2012++; Persist Security Info=true;";
          bus.ConnectionString = @"Data Source =TCP:72.38.84.230,30000; Initial Catalog = jobcenter.BUSICONTROLDB; User ID=sa; Password=06De1963++; Persist Security Info=true;";
        return bus;
    }
    public static SqlConnection UserLog()
    {
        SqlConnection userlog = new SqlConnection();

        string strcln = "Select * from CompanyDatabaseDetailTbl where CompanyLoginId='" + Convert.ToString(HttpContext.Current.Session["Comid"]) + "' and CodeType='USERLOGDB' ";
        SqlCommand cmdcln = new SqlCommand(strcln, serverconn());
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        if (dtcln.Rows.Count > 0)
        {
            //userlog.ConnectionString = @"Data Source =" + dtcln.Rows[0]["SqlServerName"] + "\\" + dtcln.Rows[0]["SqlInstanceName"] + "," + dtcln.Rows[0]["Sqlport"] + "; Initial Catalog = " + dtcln.Rows[0]["DatabaseName"] + "; Integrated Security = true";
          //  userlog.ConnectionString = @"Data Source =C3\C3SERVERMASTER,30000; Initial Catalog = jobcenter.USERLOGDB_Developer; User ID=sa; Password=06De1963++; Persist Security Info=true;";

       }
        //USERLOGDB_Developer
       userlog.ConnectionString = "Data Source =192.168.1.219\\C3SERVERMASTER,30000; Initial Catalog = jobcenter.USERLOGDB_Developer; User ID=sa; Password=06De1963++; Persist Security Info=true;";
       userlog.ConnectionString = "Data Source =TCP:192.168.10.42,40000; Initial Catalog = jobcenter.USERLOGDB_Developer; User ID=AMDDeveloper; Password=Om2012++; Persist Security Info=true;";
       userlog.ConnectionString = "Data Source =192.168.1.219\\C3SERVERMASTER,30000; Initial Catalog = jobcenter.USERLOGDB; User ID=sa; Password=06De1963++; Persist Security Info=true;";

        Userrnamedb = userlog.Database;
        return userlog;
    }
    public static SqlConnection intmsg()
    {
        SqlConnection msgint = new SqlConnection();


        string strcln = "Select * from CompanyDatabaseDetailTbl where CompanyLoginId='" + Convert.ToString(HttpContext.Current.Session["Comid"]) + "' and CodeType='INTMSGDB' ";
        SqlCommand cmdcln = new SqlCommand(strcln, serverconn());
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        if (dtcln.Rows.Count > 0)
        {
            //msgint.ConnectionString = @"Data Source =" + dtcln.Rows[0]["SqlServerName"] + "\\" + dtcln.Rows[0]["SqlInstanceName"] + "," + dtcln.Rows[0]["Sqlport"] + "; Initial Catalog = " + dtcln.Rows[0]["DatabaseName"] + "; Integrated Security = true";
          //  msgint.ConnectionString = @"Data Source =C3\C3SERVERMASTER,30000; Initial Catalog = jobcenter.INTMSGDB_Developer; User ID=sa; Password=06De1963++; Persist Security Info=true;";

        }
        msgint.ConnectionString = "Data Source =TCP:192.168.10.42,40000; Initial Catalog = jobcenter.INTMSGDB_Developer; User ID=AMDDeveloper; Password=Om2012++; Persist Security Info=true;";
        msgint.ConnectionString = @"Data Source =192.168.1.219\\C3SERVERMASTER,30000; Initial Catalog = jobcenter.INTMSGDB_Developer; User ID=Sa; Password=06De1963++; Persist Security Info=true;";
        intmsg11 = msgint.Database;
        if (intmsg11=="")
       {
           intmsg11 = "jobcenter.INTMSGDB_Developer";
       }
        return msgint;
    }

    public static SqlConnection extmsg()
    {
        SqlConnection msgext = new SqlConnection();


        string strcln = "Select * from CompanyDatabaseDetailTbl where CompanyLoginId='" + Convert.ToString(HttpContext.Current.Session["Comid"]) + "' and CodeType='EXTMSGDB' ";
        SqlCommand cmdcln = new SqlCommand(strcln, serverconn());
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);

        if (dtcln.Rows.Count > 0)
        {

            msgext.ConnectionString = @"Data Source =C3\C3SERVERMASTER,30000; Initial Catalog = jobcenter.EXTMSGDB_Developer; User ID=sa; Password=06De1963++; Persist Security Info=true;";
            msgext.ConnectionString = @"Data Source =TCP:72.38.84.230,30000; Initial Catalog = jobcenter.EXTMSGDB_Developer; User ID=IndiaDeveloper; Password=Om2012++; Persist Security Info=true;";
        }
        msgext.ConnectionString = @"Data Source =TCP:192.168.10.42,40000; Initial Catalog = jobcenter.EXTMSGDB_Developer; User ID=AMDDeveloper; Password=Om2012++; Persist Security Info=true;";
        msgext.ConnectionString = @"Data Source =C3\C3SERVERMASTER,30000; Initial Catalog = jobcenter.EXTMSGDB_Developer; User ID=sa; Password=06De1963++; Persist Security Info=true;";
        extmsg11 = msgext.Database;
        return msgext;
    }

}
