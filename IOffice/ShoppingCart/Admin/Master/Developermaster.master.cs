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
using AjaxControlToolkit;
using System.Data.SqlClient;
using Microsoft.SqlServer.Server;
using Microsoft.SqlServer.Management.Common;
using System.Xml;
using System.IO;
using Microsoft.SqlServer.Management.Smo;
using System.Security.Cryptography;

public partial class Developermaster : System.Web.UI.MasterPage
{
    // SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString"].ToString());
    // SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString1"].ToString());
    SqlConnection con=new SqlConnection(PageConn.connnn);
    SqlConnection con1;
    private Control myC;
    protected string priceid;
    protected string verid;
    string pageiddd;
    //HttpCookieCollection cook;
    protected void Page_Load(object sender, EventArgs e)
    {
       
        int mast = 0;
        //PageConn pgcon = new PageConn();
        //con = pgcon.dynconn;
        if (Convert.ToString(con.ConnectionString) == "")
        {
            PageConn pgcon = new PageConn();
            con = pgcon.dynconn;
        }
        if (Convert.ToString(PageConn.busdatabase) == "")
        {
            PageConn.licenseconn();
        }
        con1 = PageConn.licenseconn();
        
        if (Convert.ToString(PageConn.bidname) == "")
        {
            PageConn.busclient();
        }
     
        string strData = Request.Url.LocalPath.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();
        Session["pagename"] = page.ToString();

      

        String pageurl = Request.Url.AbsoluteUri;
        if (Convert.ToString(Session["Devl"]) == "yes")
        {
            if (!IsPostBack)
            {
                FillLogos();

                HttpContext.Current.Response.Cache.SetAllowResponseInBrowserHistory(false);
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Cache.SetNoStore();
                Response.Cache.SetExpires(DateTime.Now.AddSeconds(60));
                Response.Cache.SetValidUntilExpires(true);

            }
        }
        else
        {
            Response.Redirect("~/ShoppingCart/Developer/DeveloperLogin.aspx");
        }
              
    }
  
    string GetBackground()
    {
        return Request.Cookies["Background"].Value;
    }
    protected DataTable select(string qu)
    {
        SqlCommand cmd = new SqlCommand(qu, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        return dt;

    }
    protected DataTable selectbusdy(string qu)
    {
        SqlCommand cmd = new SqlCommand(qu, PageConn.busclient());
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        return dt;

    }
   
   
    protected void FillLogos()
    {


        string strMainRedirect = "select warehousemaster.warehouseid,employeemaster.EmployeeName from warehousemaster inner join employeemaster on employeemaster.whid=warehousemaster.warehouseid where employeemaster.employeemasterid='" + Session["EmployeeId"] + "'";
        SqlDataAdapter adpRedirect = new SqlDataAdapter(strMainRedirect, con);
        DataTable dtRedirect = new DataTable();
        adpRedirect.Fill(dtRedirect);

        if (dtRedirect.Rows.Count > 0)
        {
            SqlDataAdapter dafff = new SqlDataAdapter("select LogoUrl,SiteUrl from CompanyWebsitMaster where whid='" + Convert.ToString(dtRedirect.Rows[0]["warehouseid"]) + "'", con);
            DataTable dtfff = new DataTable();
            dafff.Fill(dtfff);

            if (dtfff.Rows.Count > 0)
            {
                mainloginlogo.ImageUrl = "~/ShoppingCart/images/" + dtfff.Rows[0]["LogoUrl"].ToString();
            }
            else
            {
                mainloginlogo.ImageUrl = imgsitel.ImageUrl;
             //   mainloginlogo.ImageUrl = "~/ShoppingCart/images/timekeeperlogo.jpg";
            }

            Label2.Text = "Login as : " + Convert.ToString(dtRedirect.Rows[0]["EmployeeName"]);
            string straddr = "select Distinct CompanyWebsiteAddressMaster.*,WareHouseMaster.Name as BName,CityMasterTbl.CityName,StateMasterTbl.Statename,CountryMaster.CountryName from CompanyWebsiteAddressMaster inner join WareHouseMaster on WareHouseMaster.WareHouseId=CompanyWebsiteAddressMaster.CompanyWebsiteMasterId inner join AddressTypeMaster on AddressTypeMaster.AddressTypeMasterId=CompanyWebsiteAddressMaster.AddressTypeMasterId inner join CountryMaster on " +
                   "CountryMaster.CountryId=CompanyWebsiteAddressMaster.Country inner join StateMasterTbl on " +
                   "StateMasterTbl.StateId=CompanyWebsiteAddressMaster.State inner join CityMasterTbl on " +
                   "CityMasterTbl.CityId=CompanyWebsiteAddressMaster.City where CompanyWebsiteAddressMaster.CompanyWebsiteMasterId='" + Convert.ToString(dtRedirect.Rows[0]["warehouseid"]) + "' and AddressTypeMaster.Name='Business Address' ";
            SqlDataAdapter adpaddr = new SqlDataAdapter(straddr, con);
            DataTable dtaddr = new DataTable();
            adpaddr.Fill(dtaddr);

            if (dtaddr.Rows.Count > 0)
            {
                busn.Text = Convert.ToString(dtaddr.Rows[0]["BName"]);
                lbladdr.Text = Convert.ToString(dtaddr.Rows[0]["BName"]) + ", " + Convert.ToString(dtaddr.Rows[0]["Address1"]) + ", " + Convert.ToString(dtaddr.Rows[0]["CityName"]) + ", " + Convert.ToString(dtaddr.Rows[0]["Statename"]) + ", " + Convert.ToString(dtaddr.Rows[0]["CountryName"]);
                      
                if (Convert.ToString(dtaddr.Rows[0]["Zip"]) != "")
                {
                    lbladdr.Text = lbladdr.Text + ", " + Convert.ToString(dtaddr.Rows[0]["Zip"]);
                }

                if (Convert.ToString(dtaddr.Rows[0]["Phone1"]) != "")
                {
                    lbladdr.Text = lbladdr.Text + ", " + Convert.ToString(dtaddr.Rows[0]["Phone1"]);
                }
                if (Convert.ToString(dtaddr.Rows[0]["Email"]) != "")
                {
                    lbladdr.Text = lbladdr.Text + ", " + Convert.ToString(dtaddr.Rows[0]["Email"]);
                }
                if (Convert.ToString(dtfff.Rows[0]["SiteUrl"]) != "")
                {
                    lbladdr.Text = lbladdr.Text + ", " + Convert.ToString(dtfff.Rows[0]["SiteUrl"]);
                }
            }

            
        }
    }
    public void GetControls(Control c, string FindControl)
    {


        foreach (Control cc in c.Controls)
        {
            
            if (cc.ID == FindControl)
            {
                myC = cc;
                break;

            }

            if (cc.Controls.Count > 0)
                GetControls(cc, FindControl);


        }

    }
    private string Decrypt(string strText, string strEncrypt)
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
            throw ex;
        }
    }
   
  



  
    protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
    {

    }

    
  
    protected void Menu1_MenuItemClick1(object sender, MenuEventArgs e)
    {
        if (Menu1.Items.Count > 0)
        {
            if (Menu1.SelectedItem.Text == "Home")
            {
                if (Session["Pnori"] == null)
                {
                    Response.Redirect("~/ShoppingCart/Admin/frmafterloginforSuper.aspx");
                }
                else
                {
                    Response.Redirect("~/ShoppingCart/Admin/" + Session["Pnori"] + "");
                }
            }
            else if (Menu1.SelectedItem.Text == "Log Out")
            {
                FormsAuthentication.SignOut();
                Session.Clear();
                Session.Abandon();
                Response.AddHeader("Pragma", "no-cache");
                Response.Cache.SetAllowResponseInBrowserHistory(false);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetNoStore();
                Response.Expires = -1;
                Response.Redirect("~/ShoppingCart/Admin/ShoppingCartLogin.aspx");
            }
        }

    }
}

