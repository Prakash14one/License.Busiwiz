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


public partial class ProductUpdateLastt : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    SqlConnection conn;
    SqlConnection connserver;
    SqlConnection connNewDB = new SqlConnection();

    SqlConnection conmaster = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    public static string encstr = "";
   
    protected void Page_Load(object sender, EventArgs e)
    {
        //*************
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!IsPostBack)
        {
           
            ViewState["sortOrder"] = "";
            if (Request.QueryString["ClientId"] == null )
            {
                //Session["ClientId"] = Request.QueryString["Clid"];              
            }
           
               
                FillGrid();

               
        }
    }
  
  




    //-***********Website Grid web
    //-***********Website Grid web
    protected void FillGrid()
    {
        DataTable dtsvr = selectSer(" Select * From ProductMasterCodeonsatelliteserverTbl  ");
        GridView1.DataSource = dtsvr;
        GridView1.DataBind();
    }
   
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
       
    }
    protected void GridView2_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
       
    }
    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
   
    //-***************************
   
    //-***************************
    //---
   
    //***********************
    //Filters
    
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;

    }
    
    protected DataTable select(string str)
    {
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);
        return dt;
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
    protected DataTable selectSer(string str)
    {
        SqlConnection connCompserver = new SqlConnection();
                string strr = " SELECT  * From Servermastertbl Where id=5";
                SqlCommand cmd = new SqlCommand(strr, con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                adp.Fill(ds);
                if (ds.Rows.Count > 0)
                {
                    string Comp_Ser_Masterpath = ds.Rows[0]["folderpathformastercode"].ToString();
                    string Comp_Ser_iispath = ds.Rows[0]["serverdefaultpathforiis"].ToString();

                    //Start--------Company's Server Connectionstring
                    string Comp_ServEnckey = ds.Rows[0]["Enckey"].ToString();
                    string Comp_serversqlserverip = ds.Rows[0]["sqlurl"].ToString();
                    string Comp_serversqlinstancename = ds.Rows[0]["DefaultsqlInstance"].ToString();
                    string Comp_serversqlport = ds.Rows[0]["port"].ToString();
                    string Comp_serversqldbname = ds.Rows[0]["DefaultDatabaseName"].ToString();
                    string Comp_serversqlpwd = ds.Rows[0]["Sapassword"].ToString();
                    string Comp_serverweburl = ds.Rows[0]["Busiwizsatellitesiteurl"].ToString();
                    connCompserver.ConnectionString = @"Data Source =" + Comp_serversqlserverip + "\\" + "\\" + Comp_serversqlinstancename + "," + Comp_serversqlport + "; Initial Catalog=" + Comp_serversqldbname + "; User ID=Sa; Password=" + PageMgmt.Decrypted(Comp_serversqlpwd) + "; Persist Security Info=true;";
                }
                SqlCommand cmdclnccdweb = new SqlCommand(str, connCompserver);
        DataTable dtclnccdweb = new DataTable();
        SqlDataAdapter adpclnccdweb = new SqlDataAdapter(cmdclnccdweb);
        adpclnccdweb.Fill(dtclnccdweb);
        return dtclnccdweb;
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
    public static string GetLast3String(string source, int last)
    {
        return last >= source.Length ? source : source.Substring(source.Length - last);
    }


   
}
