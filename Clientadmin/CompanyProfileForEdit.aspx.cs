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

using System.Net;
using System.IO;
using System.Collections.Generic;
public partial class CompanyProfileForEdit : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();

    SqlConnection conMemberBusiwiz = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    SqlConnection conLicenseBusiwiz = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);



    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            
            if (Session["CompanyId"] != "" || Session["CompanyId"] != null)
            {
                ViewState["comid"] = Session["CompanyId"];
                filldata();

                if (Request.QueryString["type"] == "upgrad")
                {
                    btnplanchange_Clickall(sender, e);
                }
                if (Request.QueryString["type"] == "portalselect")
                {
                    //Priceplancomparision.aspx?type=portalselect&portid=" + lblpid.Text + "
                    btnplanchange_ClickallOtherPortal(sender, e);
                }
                
                if (Request.QueryString["type"] == "renew")
                {
                    //CompanyProfileForEdit.aspx?type=renew&ppid=" + lblppid.Text + "
                    btnplanchange_ClickCurrent(sender, e);
                   
                }
                if (Request.QueryString["type"] == "detail")
                {
                    btnplanchange_ClickallFree(sender, e);
                }
                if (Request.QueryString["type"] == "change")
                {
                    fillPortal();
                }
            }       
        }
        catch (Exception ex)
        {

        }
          
        
    }

    protected void btnEditProfile_Click(object sender, EventArgs e)
    {
        //con.ConnectionString = Session["DynamicConnecationString"].ToString();
        filldata();
    }
    protected void filldata()
    {

        string str1 = "SELECT CompanyMaster.companyloginid, CompanyMaster.ProductId,CompanyMaster.PriceplanId, CompanyMaster.Websiteurl,CompanyMaster.CompanyId, CompanyMaster.CompanyName, CompanyMaster.ContactPerson, CompanyMaster.ContactPersonDesignation, " +
                     " CompanyMaster.CompanyWebsite, CompanyMaster.date, CompanyMaster.PlanId, CompanyMaster.Address, CompanyMaster.Email, CompanyMaster.pincode, " +
                     " CompanyMaster.phone, CompanyMaster.city,CompanyMaster.fax, CompanyMaster.CompanyLoginId, CompanyMaster.AdminId, CompanyMaster.Password, CompanyMaster.redirect, " +
                     " CompanyMaster.active, CompanyMaster.deactiveReason, CompanyMaster.PlanActive, CompanyMaster.HostId, CompanyMaster.StateId, StateMasterTbl.StateName,  " +
                     " CountryMaster.CountryName,LicenseMaster.Licensekey, StateMasterTbl.StateId, CountryMaster.CountryId " +
                      "  FROM         CountryMaster RIGHT OUTER JOIN " +
                     " StateMasterTbl ON CountryMaster.CountryId = StateMasterTbl.CountryId RIGHT OUTER JOIN " +
                     " CompanyMaster ON StateMasterTbl.StateId = CompanyMaster.StateId inner join LicenseMaster on LicenseMaster.Companyid=CompanyMaster.CompanyId where  CompanyMaster.CompanyId = '" + Session["CompanyId"].ToString() + "' ";

        SqlCommand cmd1 = new SqlCommand(str1, conLicenseBusiwiz);
        DataTable dt1 = new DataTable();
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        da1.Fill(dt1);
        if (dt1.Rows.Count > 0)
        {
            Session["ProductId"] = dt1.Rows[0]["ProductId"].ToString();

           
            txtadd.Text = dt1.Rows[0]["Address"].ToString();
            ViewState["comid"] = dt1.Rows[0]["companyloginid"].ToString();
          
            string chkaper = "select ProductMaster.Download,PricePlanMaster.PayperOrderPlan,PricePlanMaster.PricePlanAmount, PricePlanMaster.EndDate, PricePlanMaster.priceplanname,PaypalordercustomerbalanceTbl.Balance,PaypalordercustomerbalanceTbl.Subdate FROM PricePlanMaster inner join PaypalordercustomerbalanceTbl on PaypalordercustomerbalanceTbl.PricePlanID=PricePlanMaster.PricePlanId inner join  ProductMaster on ProductMaster.ProductId=PricePlanMaster.ProductId WHERE PricePlanMaster.PricePlanId='" + dt1.Rows[0]["PlanId"].ToString() + "' and amountperOrder is NOT NULL and amountperOrder NOT IN('') and PayperOrderPlan='True' and PaypalordercustomerbalanceTbl.CustomerID='" + dt1.Rows[0]["Companyloginid"].ToString() + "'";
            SqlDataAdapter adcheck = new SqlDataAdapter(chkaper, conLicenseBusiwiz);
            DataTable dtcheck = new DataTable();
            adcheck.Fill(dtcheck);
            if (dtcheck.Rows.Count > 0)
            {
                lblplanname.Text = dtcheck.Rows[0]["priceplanname"].ToString();
                lblsubdate.Text = dtcheck.Rows[0]["Subdate"].ToString();
                lblvaliddate.Text = dtcheck.Rows[0]["EndDate"].ToString();
                lblppob.Text = dtcheck.Rows[0]["Balance"].ToString();
                ViewState["pamt"] = dtcheck.Rows[0]["PricePlanAmount"].ToString();

                if (Convert.ToString(dtcheck.Rows[0]["PricePlanAmount"]) == "0.0" || Convert.ToString(dtcheck.Rows[0]["PricePlanAmount"]) == "0.00" || Convert.ToString(dtcheck.Rows[0]["PricePlanAmount"]) == "00.00" || Convert.ToString(dtcheck.Rows[0]["PricePlanAmount"]) == "" || Convert.ToString(dtcheck.Rows[0]["PricePlanAmount"]) == "0" || Convert.ToString(dtcheck.Rows[0]["PricePlanAmount"]) == "00.00" || Convert.ToString(dtcheck.Rows[0]["PricePlanAmount"]) == "0.0" || Convert.ToString(dtcheck.Rows[0]["PricePlanAmount"]) == "00.0" || Convert.ToString(dtcheck.Rows[0]["PricePlanAmount"]) == "0.00")
                {
                    btnplanrenue.Enabled = false;
                    btnplanrenue.ToolTip = "Sorry your existing plan amounts to $ 0 , So this plan can not be renewed. Please change your plan.";  
                }
                else
                {
                    btnplanrenue.Enabled = true;
                     btnplanrenue.ToolTip = "";
                }
            }


            string chkapedr = "select PricePlanAmount,ProductMaster.Download,Convert(nvarchar, LicenseMaster.LicenseDate,101) as LicenseDate,LicenseMaster.LicensePeriod,PricePlanMaster.PayperOrderPlan,PricePlanMaster.PricePlanAmount, PricePlanMaster.EndDate, PricePlanMaster.priceplanname FROM LicenseMaster inner join CompanyMaster on CompanyMaster.CompanyId=LicenseMaster.CompanyId inner join  PricePlanMaster on PricePlanMaster.PricePlanId=CompanyMaster.PricePlanId inner join  ProductMaster on ProductMaster.ProductId=PricePlanMaster.ProductId WHERE PricePlanMaster.PricePlanId='" + dt1.Rows[0]["PlanId"].ToString() + "' and CompanyMaster.CompanyLoginId='" + dt1.Rows[0]["Companyloginid"].ToString() + "'";
            SqlDataAdapter adchecdk = new SqlDataAdapter(chkapedr, conLicenseBusiwiz);
            DataTable dtchecdk = new DataTable();
            adchecdk.Fill(dtchecdk);
            if (dtchecdk.Rows.Count > 0)
            {

                if (Convert.ToString(dtchecdk.Rows[0]["PricePlanAmount"]) == "" || Convert.ToString(dtchecdk.Rows[0]["PricePlanAmount"]) == "0" || Convert.ToString(dtchecdk.Rows[0]["PricePlanAmount"]) == "00.00" || Convert.ToString(dtchecdk.Rows[0]["PricePlanAmount"]) == "0.00" || Convert.ToString(dtchecdk.Rows[0]["PricePlanAmount"]) == "00.0")
                {
                    btnplanrenue.Enabled = false;
                    btnplanrenue.ToolTip = "Sorry your existing plan amounts to $ 0 , So this plan can not be renewed. Please change your plan.";
                }
                else
                {
                    btnplanrenue.Enabled = true;
                    btnplanrenue.ToolTip = "";
                }
                if (Convert.ToBoolean(dtchecdk.Rows[0]["PayperOrderPlan"]) == true)
                {
                    btnmoneytobalane.Visible = true;
                }
                else
                {
                    btnmoneytobalane.Visible = false;
                    lblplanname.Text = dtchecdk.Rows[0]["priceplanname"].ToString();
                    lblsubdate.Text = dtchecdk.Rows[0]["LIcenseDate"].ToString();
                    lblvaliddate.Text = (Convert.ToDateTime(dtchecdk.Rows[0]["LIcenseDate"]).AddDays(Convert.ToInt32(dtchecdk.Rows[0]["LicensePeriod"]))).ToShortDateString(); ;

                    lblppob.Text = dtchecdk.Rows[0]["PricePlanAmount"].ToString();
                    ViewState["pamt"] = dtchecdk.Rows[0]["PricePlanAmount"].ToString();

                }
                if (Convert.ToBoolean(dtchecdk.Rows[0]["Download"]) == true)
                {
                    pnldownload.Visible = true;
                }
                else
                {
                    pnldownload.Visible = false;
                }
            }
            string str1d = " Select Distinct SetupMaster.* from CompanyMaster inner join PricePlanMaster on PricePlanMaster.PricePlanId=CompanyMaster.PricePlanId Right OUTER JOIN  SetupMaster ON PricePlanMaster.PricePlanId = SetupMaster.PricePlanId inner join  ProductDetail on ProductDetail.ProductDetailId=SetupMaster.ProductDetailId inner join  ProductMaster ON ProductMaster.ProductId = ProductDetail.ProductId where CompanyMaster.CompanyId = '" + Session["CompanyId"].ToString() + "' and CompanyMaster.PricePlanId='" + dt1.Rows[0]["PricePlanId"].ToString() + "'";


            SqlCommand cmd1d = new SqlCommand(str1d, conLicenseBusiwiz);
            DataTable dt1d = new DataTable();
            SqlDataAdapter da1d = new SqlDataAdapter(cmd1d);
            da1d.Fill(dt1d);
            if (dt1d.Rows.Count > 0)
            {
               dtbv(dt1d);
            }
            else
            {
                string str1dd = "Select SetupMaster.* from SetupMaster  inner join  ProductDetail on ProductDetail.ProductDetailId=SetupMaster.ProductDetailId inner join  ProductMaster ON ProductMaster.ProductId = ProductDetail.ProductId inner join CompanyMaster on ProductMaster.ProductId=CompanyMaster.ProductId  where CompanyMaster.CompanyId = '" + Session["CompanyId"].ToString() + "' and SetupMaster.PricePlanId='0' and CompanyMaster.ProductId='" + dt1.Rows[0]["ProductId"].ToString() + "'";

                SqlCommand cmd1dd = new SqlCommand(str1dd, conLicenseBusiwiz);
                DataTable dt1dd = new DataTable();
                SqlDataAdapter da1dd = new SqlDataAdapter(cmd1dd);
                da1dd.Fill(dt1dd);
                if (dt1dd.Rows.Count > 0)
                {
                  dtbv(dt1dd);
                }
            }

        }
        
    }

    protected void dtbv(DataTable db)
    {
        string name = "";
        string ftp1 = "ftp://" + db.Rows[0]["FtpUrlIp"] + ":" + db.Rows[0]["Ftpport"] + "/";
        //  string ftp1 = "ftp://192.168.1.219:29/";

        string username = db.Rows[0]["FtpUserIdUpload"].ToString();
        string password = db.Rows[0]["FtpPasswordUpload"].ToString();
        FtpWebRequest oFTP = (FtpWebRequest)FtpWebRequest.Create(ftp1);
        //oFTP.Method = WebRequestMethods.Ftp.DownloadFile;
        oFTP.Credentials = new NetworkCredential(username, password);
        //oFTP.UseBinary = false;
        //oFTP.UsePassive = false;
        oFTP.UseBinary = false;
        oFTP.KeepAlive = true;
        oFTP.UsePassive = true;
        oFTP.Method = WebRequestMethods.Ftp.ListDirectory;

        FtpWebResponse response = (FtpWebResponse)oFTP.GetResponse();
        StreamReader sr = new StreamReader(response.GetResponseStream());
        string str = sr.ReadLine();
        List<string> oList = new List<string>();
        while (str != null)
        {
            if (str.Length > 3)
            {
                name = str.Trim();
                string extension = name.Substring(name.Length - 3);

                oList.Add(str);

            }
            str = sr.ReadLine();
        }
        sr.Close();
        response.Close();
        oFTP = null;
        int j = oList.Count;
        for (int i = 0; i < j; i++)
        {
            string filename = oList[i].ToString();
            string location = Server.MapPath("//Setupdata//");

            string destpath = location.ToString() + filename.ToString();
            if (Convert.ToString(db.Rows[0]["ProductSetup"]) == oList[i].ToString())
            {
                if (!File.Exists(destpath))
                {
                    GetFile(ftp1.ToString(), oList[i].ToString(), destpath.ToString(), username.ToString(), password.ToString());
                }
                //  HyperLink1.NavigateUrl = "~/Setupdata/ProductMainCode.zip";
                HyperLink1.NavigateUrl = "~/Setupdata/" + oList[i].ToString();


            }
            else if (Convert.ToString(db.Rows[0]["ProductDB"]) == oList[i].ToString())
            {
                if (!File.Exists(destpath))
                {
                    GetFile(ftp1.ToString(), oList[i].ToString(), destpath.ToString(), username.ToString(), password.ToString());
                }
                HyperLink2.NavigateUrl = "~/Setupdata/" + oList[i].ToString();


            }
            else if (Convert.ToString(db.Rows[0]["ProductExtra"]) == oList[i].ToString())
            {
                if (!File.Exists(destpath))
                {
                    GetFile(ftp1.ToString(), oList[i].ToString(), destpath.ToString(), username.ToString(), password.ToString());
                }
                HyperLink5.NavigateUrl = "~/Setupdata/" + oList[i].ToString();

            }
            else if (Convert.ToString(db.Rows[0]["BusicontrolerSetup"]) == oList[i].ToString())
            {
                if (!File.Exists(destpath))
                {
                    GetFile(ftp1.ToString(), oList[i].ToString(), destpath.ToString(), username.ToString(), password.ToString());
                }
                HyperLink3.NavigateUrl = "~/Setupdata/" + oList[i].ToString();

            }
            else if (Convert.ToString(db.Rows[0]["BusicontrolerDB"]) == oList[i].ToString())
            {
                if (!File.Exists(destpath))
                {
                    GetFile(ftp1.ToString(), oList[i].ToString(), destpath.ToString(), username.ToString(), password.ToString());
                }
                HyperLink4.NavigateUrl = "~/Setupdata/" + oList[i].ToString();

            }



        }



    }
    public bool GetFile(string ftp, string filename, string Destpath, string username, string password)
    {
        FtpWebRequest oFTP = (FtpWebRequest)FtpWebRequest.
           Create(ftp.ToString() + filename.ToString());

        oFTP.Credentials = new NetworkCredential(username.ToString(), password.ToString());
        //oFTP.UseBinary = false;
        //oFTP.UsePassive = false;
        oFTP.UseBinary = false;
        oFTP.UsePassive = true;
       
        oFTP.Method = WebRequestMethods.Ftp.DownloadFile;


        FtpWebResponse response =
           (FtpWebResponse)oFTP.GetResponse();
        Stream responseStream = response.GetResponseStream();

        FileStream fs = new FileStream(Destpath, FileMode.CreateNew);
        Byte[] buffer = new Byte[2047];
        int read = 1;
        while (read != 0)
        {
            read = responseStream.Read(buffer, 0, buffer.Length);
            fs.Write(buffer, 0, read);
        }

        responseStream.Close();
        fs.Flush();
        fs.Close();
        responseStream.Close();
        response.Close();

        oFTP = null;
        return true;
    }

    protected void btnmoneytobalane_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Refillplan.aspx");
    }
    protected void btnplanchange_Click(object sender, EventArgs e)
    {
        string PortalMasterId1;
        string str1dd = "select distinct PricePlanMaster.VersionInfoMasterId, Convert(nvarchar, LicenseMaster.LicenseDate,101) as LicenseDate ,LicenseMaster.LicensePeriod,PricePlanMaster.* from LicenseMaster inner join CompanyMaster on CompanyMaster.CompanyId=LicenseMaster.CompanyId inner join PricePlanMaster on PricePlanMaster.PricePlanId=CompanyMaster.PricePlanId  inner join VersionInfoMaster on PricePlanMaster.VersionInfoMasterId=VersionInfoMaster.VersionInfoId where CompanyMaster.CompanyId = '" + Session["CompanyId"].ToString() + "'";

        DataTable dtc = datafill(str1dd);
        if (dtc.Rows.Count > 0)
        {
            lblexistpamt.Text = Convert.ToString(dtc.Rows[0]["PricePlanAmount"]);
            lblplid.Text = Convert.ToString(dtc.Rows[0]["PricePlanId"]);
            lblexistplanname.Text = Convert.ToString(dtc.Rows[0]["PricePlanName"]);
            lbldesctext.Text = Convert.ToString(dtc.Rows[0]["PricePlanDesc"]);
            lblvalidityperiod.Text = Convert.ToString(dtc.Rows[0]["DurationMonth"]);
            lblsubcrubdate.Text = Convert.ToString(dtc.Rows[0]["LicenseDate"]);
            lblexdate.Text = (Convert.ToDateTime(dtc.Rows[0]["LicenseDate"]).AddDays(Convert.ToDouble(lblvalidityperiod.Text)).ToShortDateString()).ToString();
            PortalMasterId1= Convert.ToString(dtc.Rows[0]["PortalMasterId1"]);
            string VersionInfoMasterId = Convert.ToString(dtc.Rows[0]["VersionInfoMasterId"]);
            string activestr = "";
            
            ddlportal.Items.Clear();
            string strcln22v = "Select * from PortalMasterTbl where ProductId In( Select distinct ProductMaster.ProductId from  ProductMaster  inner join VersionInfoMaster on VersionInfoMaster.productId=ProductMaster.ProductId where VersionInfoId = '" + VersionInfoMasterId + "' ) and PortalMasterTbl.Id <>'" + PortalMasterId1 + "' and PortalMasterTbl.Status=1 order by PortalName ";
            //SqlCommand cmdcln22v = new SqlCommand(strcln22v, con);
            //DataTable dtcln22v = new DataTable();
           //SqlDataAdapter adpcln22v = new SqlDataAdapter(cmdcln22v);
           // adpcln22v.Fill(dtcln22v); 
            DataTable dtcln22v = datafill(strcln22v);
           
            ddlportal.DataSource = dtcln22v;

            ddlportal.DataValueField = "Id";
            ddlportal.DataTextField = "PortalName";
            ddlportal.DataBind();


            ddlportal.Items.Insert(0, "-Select-");
            ddlportal.Items[0].Value = "0";

            ddlportal.Visible = true;
            Label8.Visible = true;
            btngo.Visible = true; 
        }
        //string str1dd1 = "select * from PricePlanMaster  where VersionInfoMasterId='" + dtc.Rows[0]["VersionInfoMasterId"] + "' and PricePlanId<>'" + dtc.Rows[0]["PricePlanId"] + "' and ( PricePlanAmount IS NOT NULL  and PricePlanAmount<>'' and  PricePlanAmount<>'0.0' and  PricePlanAmount<>'00.0'  and PricePlanAmount<>'' and PricePlanAmount<>'00.00'  and  PricePlanAmount<>'0.00')";
        //DataTable dtc1 = datafill(str1dd1);
        //if (dtc1.Rows.Count > 0)
        //{
        //    GridView1.DataSource = dtc1;
        //    GridView1.DataBind();
        //}
        //else
        //{
        //    GridView1.DataSource = null;
        //    GridView1.DataBind();
        //}
        //ModalPopupExtender3.Show();
    }

    protected void btnplanchange_ClickCurrent(object sender, EventArgs e)
    {
        string PortalMasterId1;
        string str1dd = @" SELECT DISTINCT dbo.PricePlanMaster.VersionInfoMasterId, CONVERT(nvarchar, dbo.LicenseMaster.LicenseDate, 101) AS LicenseDate, dbo.LicenseMaster.LicensePeriod,  dbo.PricePlanMaster.PricePlanId, dbo.PricePlanMaster.PricePlanName, dbo.PricePlanMaster.PricePlanDesc, dbo.PricePlanMaster.active,  dbo.PricePlanMaster.StartDate, dbo.PricePlanMaster.EndDate, dbo.PricePlanMaster.PricePlanAmount, dbo.PricePlanMaster.ProductId,  dbo.PricePlanMaster.DurationMonth, dbo.PricePlanMaster.AllowIPTrack, dbo.PricePlanMaster.GBUsage, dbo.PricePlanMaster.MaxUser,  dbo.PricePlanMaster.TrafficinGB, dbo.PricePlanMaster.TotalMail, dbo.PricePlanMaster.CheckIntervalDays, dbo.PricePlanMaster.GraceDays, dbo.PricePlanMaster.VersionInfoMasterId AS Expr1, dbo.PricePlanMaster.PayperOrderPlan, dbo.PricePlanMaster.amountperOrder,  dbo.PricePlanMaster.FreeIntialOrders, dbo.PricePlanMaster.MinimumDeposite, dbo.PricePlanMaster.Maxamount, dbo.PricePlanMaster.FoldersizeMB, dbo.PricePlanMaster.PriceplancatId, dbo.PricePlanMaster.PortalMasterId1, dbo.PricePlanMaster.Producthostclientserver, dbo.PricePlanMaster.IsItFreeTryOutPlan, dbo.PricePlanMaster.CompanyPrefix, dbo.PricePlanMaster.ConfigurationText, dbo.PricePlanMaster.EmailContent, dbo.PricePlanMaster.BasePrice,  dbo.PricePlanMaster.Plancatedefault, dbo.PortalMasterTbl.PortalName
                             FROM            dbo.LicenseMaster INNER JOIN  dbo.CompanyMaster ON dbo.CompanyMaster.CompanyId = dbo.LicenseMaster.CompanyId INNER JOIN   dbo.PricePlanMaster ON dbo.PricePlanMaster.PricePlanId = dbo.CompanyMaster.PricePlanId INNER JOIN    dbo.VersionInfoMaster ON dbo.PricePlanMaster.VersionInfoMasterId = dbo.VersionInfoMaster.VersionInfoId INNER JOIN  dbo.PortalMasterTbl ON dbo.PricePlanMaster.PortalMasterId1 = dbo.PortalMasterTbl.Id where CompanyMaster.CompanyId = '" + Session["CompanyId"].ToString() + "'";

        DataTable dtc = datafill(str1dd);
        if (dtc.Rows.Count > 0)
        {
            Session["ProductId"]=Convert.ToString(dtc.Rows[0]["VersionInfoMasterId"]);
            lblexistpamt.Text = Convert.ToString(dtc.Rows[0]["PricePlanAmount"]);
            lblplid.Text = Convert.ToString(dtc.Rows[0]["PricePlanId"]);
            lblexistplanname.Text = Convert.ToString(dtc.Rows[0]["PricePlanName"]);
            lbldesctext.Text = Convert.ToString(dtc.Rows[0]["PricePlanDesc"]);
            lblvalidityperiod.Text = Convert.ToString(dtc.Rows[0]["DurationMonth"]);
            lblsubcrubdate.Text = Convert.ToString(dtc.Rows[0]["LicenseDate"]);
            lblexdate.Text = (Convert.ToDateTime(dtc.Rows[0]["LicenseDate"]).AddDays(Convert.ToDouble(lblvalidityperiod.Text)).ToShortDateString()).ToString();
            PortalMasterId1 = Convert.ToString(dtc.Rows[0]["PortalMasterId1"]);
            Llbproductid.Text = dtc.Rows[0]["VersionInfoMasterId"].ToString();
            lblportal.Text = dtc.Rows[0]["PortalName"].ToString();
            
            //  Response.Redirect("http://license.busiwiz.com/priceplancomparisionRenew.aspx?Id=" + VersionInfoMasterId + "&PN=" + PortalMastername + "");  

        }
        string str1dd1 = "select * from PricePlanMaster  where VersionInfoMasterId='" + dtc.Rows[0]["VersionInfoMasterId"] + "' and PricePlanId ='" + dtc.Rows[0]["PricePlanId"] + "'  ";
        DataTable dtc1 = datafill(str1dd1);
        if (dtc1.Rows.Count > 0)
        {
            GridView1.DataSource = dtc1;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
        ModalPopupExtender3.Show();
    }
    protected void btnplanchange_Clickall(object sender, EventArgs e)
    {
        string PortalMasterId1;
        string str1dd = @" SELECT DISTINCT dbo.PricePlanMaster.VersionInfoMasterId, CONVERT(nvarchar, dbo.LicenseMaster.LicenseDate, 101) AS LicenseDate, dbo.LicenseMaster.LicensePeriod,  dbo.PricePlanMaster.PricePlanId, dbo.PricePlanMaster.PricePlanName, dbo.PricePlanMaster.PricePlanDesc, dbo.PricePlanMaster.active,  dbo.PricePlanMaster.StartDate, dbo.PricePlanMaster.EndDate, dbo.PricePlanMaster.PricePlanAmount, dbo.PricePlanMaster.ProductId,  dbo.PricePlanMaster.DurationMonth, dbo.PricePlanMaster.AllowIPTrack, dbo.PricePlanMaster.GBUsage, dbo.PricePlanMaster.MaxUser,  dbo.PricePlanMaster.TrafficinGB, dbo.PricePlanMaster.TotalMail, dbo.PricePlanMaster.CheckIntervalDays, dbo.PricePlanMaster.GraceDays, dbo.PricePlanMaster.VersionInfoMasterId AS Expr1, dbo.PricePlanMaster.PayperOrderPlan, dbo.PricePlanMaster.amountperOrder,  dbo.PricePlanMaster.FreeIntialOrders, dbo.PricePlanMaster.MinimumDeposite, dbo.PricePlanMaster.Maxamount, dbo.PricePlanMaster.FoldersizeMB, dbo.PricePlanMaster.PriceplancatId, dbo.PricePlanMaster.PortalMasterId1, dbo.PricePlanMaster.Producthostclientserver, dbo.PricePlanMaster.IsItFreeTryOutPlan, dbo.PricePlanMaster.CompanyPrefix, dbo.PricePlanMaster.ConfigurationText, dbo.PricePlanMaster.EmailContent, dbo.PricePlanMaster.BasePrice,  dbo.PricePlanMaster.Plancatedefault, dbo.PortalMasterTbl.PortalName
                             FROM            dbo.LicenseMaster INNER JOIN  dbo.CompanyMaster ON dbo.CompanyMaster.CompanyId = dbo.LicenseMaster.CompanyId INNER JOIN   dbo.PricePlanMaster ON dbo.PricePlanMaster.PricePlanId = dbo.CompanyMaster.PricePlanId INNER JOIN    dbo.VersionInfoMaster ON dbo.PricePlanMaster.VersionInfoMasterId = dbo.VersionInfoMaster.VersionInfoId INNER JOIN  dbo.PortalMasterTbl ON dbo.PricePlanMaster.PortalMasterId1 = dbo.PortalMasterTbl.Id where CompanyMaster.CompanyId = '" + Session["CompanyId"].ToString() + "'";

        DataTable dtc = datafill(str1dd);
        if (dtc.Rows.Count > 0)
        {
            lblexistpamt.Text = Convert.ToString(dtc.Rows[0]["PricePlanAmount"]);
            lblplid.Text = Convert.ToString(dtc.Rows[0]["PricePlanId"]);
            lblexistplanname.Text = Convert.ToString(dtc.Rows[0]["PricePlanName"]);
            lbldesctext.Text = Convert.ToString(dtc.Rows[0]["PricePlanDesc"]);
            lblvalidityperiod.Text = Convert.ToString(dtc.Rows[0]["DurationMonth"]);
            lblsubcrubdate.Text = Convert.ToString(dtc.Rows[0]["LicenseDate"]);
            lblexdate.Text = (Convert.ToDateTime(dtc.Rows[0]["LicenseDate"]).AddDays(Convert.ToDouble(lblvalidityperiod.Text)).ToShortDateString()).ToString();
            PortalMasterId1 = Convert.ToString(dtc.Rows[0]["PortalMasterId1"]);
            string PortalMastername = Convert.ToString(dtc.Rows[0]["PortalName"]);
            string VersionInfoMasterId = Convert.ToString(dtc.Rows[0]["VersionInfoMasterId"]);

          //  Response.Redirect("http://license.busiwiz.com/priceplancomparisionRenew.aspx?Id=" + VersionInfoMasterId + "&PN=" + PortalMastername + "");  

        }
        string str1dd1 = "select * from PricePlanMaster  where VersionInfoMasterId='" + dtc.Rows[0]["VersionInfoMasterId"] + "' and PricePlanId<>'" + dtc.Rows[0]["PricePlanId"] + "' and (PricePlanAmount IS NOT NULL  and PricePlanAmount<>'' and  PricePlanAmount<>'0.0' and  PricePlanAmount<>'00.0'  and PricePlanAmount<>'' and PricePlanAmount<>'00.00'  and  PricePlanAmount<>'0.00' and PortalMasterId1='" + dtc.Rows[0]["PortalMasterId1"] + "' )";
        DataTable dtc1 = datafill(str1dd1);
        if (dtc1.Rows.Count > 0)
        {
            GridView1.DataSource = dtc1;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
        ModalPopupExtender3.Show();
    }

    protected void btnplanchange_ClickallOtherPortal(object sender, EventArgs e)
    {
        string PortalMasterId1;
        string str1dd = @" SELECT DISTINCT dbo.PricePlanMaster.VersionInfoMasterId, CONVERT(nvarchar, dbo.LicenseMaster.LicenseDate, 101) AS LicenseDate, dbo.LicenseMaster.LicensePeriod,  dbo.PricePlanMaster.PricePlanId, dbo.PricePlanMaster.PricePlanName, dbo.PricePlanMaster.PricePlanDesc, dbo.PricePlanMaster.active,  dbo.PricePlanMaster.StartDate, dbo.PricePlanMaster.EndDate, dbo.PricePlanMaster.PricePlanAmount, dbo.PricePlanMaster.ProductId,  dbo.PricePlanMaster.DurationMonth, dbo.PricePlanMaster.AllowIPTrack, dbo.PricePlanMaster.GBUsage, dbo.PricePlanMaster.MaxUser,  dbo.PricePlanMaster.TrafficinGB, dbo.PricePlanMaster.TotalMail, dbo.PricePlanMaster.CheckIntervalDays, dbo.PricePlanMaster.GraceDays, dbo.PricePlanMaster.VersionInfoMasterId AS Expr1, dbo.PricePlanMaster.PayperOrderPlan, dbo.PricePlanMaster.amountperOrder,  dbo.PricePlanMaster.FreeIntialOrders, dbo.PricePlanMaster.MinimumDeposite, dbo.PricePlanMaster.Maxamount, dbo.PricePlanMaster.FoldersizeMB, dbo.PricePlanMaster.PriceplancatId, dbo.PricePlanMaster.PortalMasterId1, dbo.PricePlanMaster.Producthostclientserver, dbo.PricePlanMaster.IsItFreeTryOutPlan, dbo.PricePlanMaster.CompanyPrefix, dbo.PricePlanMaster.ConfigurationText, dbo.PricePlanMaster.EmailContent, dbo.PricePlanMaster.BasePrice,  dbo.PricePlanMaster.Plancatedefault, dbo.PortalMasterTbl.PortalName
                             FROM            dbo.LicenseMaster INNER JOIN  dbo.CompanyMaster ON dbo.CompanyMaster.CompanyId = dbo.LicenseMaster.CompanyId INNER JOIN   dbo.PricePlanMaster ON dbo.PricePlanMaster.PricePlanId = dbo.CompanyMaster.PricePlanId INNER JOIN    dbo.VersionInfoMaster ON dbo.PricePlanMaster.VersionInfoMasterId = dbo.VersionInfoMaster.VersionInfoId INNER JOIN  dbo.PortalMasterTbl ON dbo.PricePlanMaster.PortalMasterId1 = dbo.PortalMasterTbl.Id where CompanyMaster.CompanyId = '" + Session["CompanyId"].ToString() + "'";

        DataTable dtc = datafill(str1dd);
        if (dtc.Rows.Count > 0)
        {
            lblexistpamt.Text = Convert.ToString(dtc.Rows[0]["PricePlanAmount"]);
            lblplid.Text = Convert.ToString(dtc.Rows[0]["PricePlanId"]);
            lblexistplanname.Text = Convert.ToString(dtc.Rows[0]["PricePlanName"]);
            lbldesctext.Text = Convert.ToString(dtc.Rows[0]["PricePlanDesc"]);
            lblvalidityperiod.Text = Convert.ToString(dtc.Rows[0]["DurationMonth"]);
            lblsubcrubdate.Text = Convert.ToString(dtc.Rows[0]["LicenseDate"]);
            lblexdate.Text = (Convert.ToDateTime(dtc.Rows[0]["LicenseDate"]).AddDays(Convert.ToDouble(lblvalidityperiod.Text)).ToShortDateString()).ToString();
            PortalMasterId1 = Convert.ToString(dtc.Rows[0]["PortalMasterId1"]);
            string PortalMastername = Convert.ToString(dtc.Rows[0]["PortalName"]);
            string VersionInfoMasterId = Convert.ToString(dtc.Rows[0]["VersionInfoMasterId"]);

            //  Response.Redirect("http://license.busiwiz.com/priceplancomparisionRenew.aspx?Id=" + VersionInfoMasterId + "&PN=" + PortalMastername + "");  

        }
        string str1dd1 = "select * from PricePlanMaster  where VersionInfoMasterId='" + dtc.Rows[0]["VersionInfoMasterId"] + "' and PricePlanId<>'" + dtc.Rows[0]["PricePlanId"] + "' and (PricePlanAmount IS NOT NULL  and PricePlanAmount<>'' and  PricePlanAmount<>'0.0' and  PricePlanAmount<>'00.0'  and PricePlanAmount<>'' and PricePlanAmount<>'00.00'  and  PricePlanAmount<>'0.00' and PortalMasterId1='" + Request.QueryString["portid"] + "' )";
        DataTable dtc1 = datafill(str1dd1);
        if (dtc1.Rows.Count > 0)
        {
            GridView1.DataSource = dtc1;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
        ModalPopupExtender3.Show();
    }

    protected void btnplanchange_ClickGo(object sender, EventArgs e)
    {
        if (ddlportal.SelectedIndex > 0)
        {
            string PortalMasterId1;
            string str1dd = "select distinct PricePlanMaster.VersionInfoMasterId, Convert(nvarchar, LicenseMaster.LicenseDate,101) as LicenseDate ,LicenseMaster.LicensePeriod,PricePlanMaster.* from LicenseMaster inner join CompanyMaster on CompanyMaster.CompanyId=LicenseMaster.CompanyId inner join PricePlanMaster on PricePlanMaster.PricePlanId=CompanyMaster.PricePlanId  inner join VersionInfoMaster on PricePlanMaster.VersionInfoMasterId=VersionInfoMaster.VersionInfoId where CompanyMaster.CompanyId = '" + Session["CompanyId"].ToString() + "'";

            DataTable dtc = datafill(str1dd);
            if (dtc.Rows.Count > 0)
            {
                lblexistpamt.Text = Convert.ToString(dtc.Rows[0]["PricePlanAmount"]);
                lblplid.Text = Convert.ToString(dtc.Rows[0]["PricePlanId"]);
                lblexistplanname.Text = Convert.ToString(dtc.Rows[0]["PricePlanName"]);
                lbldesctext.Text = Convert.ToString(dtc.Rows[0]["PricePlanDesc"]);
                lblvalidityperiod.Text = Convert.ToString(dtc.Rows[0]["DurationMonth"]);
                lblsubcrubdate.Text = Convert.ToString(dtc.Rows[0]["LicenseDate"]);
                lblexdate.Text = (Convert.ToDateTime(dtc.Rows[0]["LicenseDate"]).AddDays(Convert.ToDouble(lblvalidityperiod.Text)).ToShortDateString()).ToString();
                PortalMasterId1 = Convert.ToString(dtc.Rows[0]["PortalMasterId1"]);
                string VersionInfoMasterId = Convert.ToString(dtc.Rows[0]["VersionInfoMasterId"]);
                string activestr = "";

                string str1dd1 = "select * from PricePlanMaster  where VersionInfoMasterId='" + dtc.Rows[0]["VersionInfoMasterId"] + "' and PricePlanId<>'" + dtc.Rows[0]["PricePlanId"] + "' and (PricePlanAmount IS NOT NULL  and PricePlanAmount<>'' and  PricePlanAmount<>'0.0' and  PricePlanAmount<>'00.0'  and PricePlanAmount<>'' and PricePlanAmount<>'00.00'  and  PricePlanAmount<>'0.00' and PortalMasterId1='" + ddlportal.SelectedValue + "' )";
                DataTable dtc1 = datafill(str1dd1);
                if (dtc1.Rows.Count > 0)
                {
                    GridView1.DataSource = dtc1;
                    GridView1.DataBind();
                }
                else
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                }
                ModalPopupExtender3.Show();
                //  Response.Redirect("http://license.busiwiz.com/priceplancomparisionRenew.aspx?Id=" + VersionInfoMasterId + "&PN=" + ddlportal.SelectedItem.Text + "");



            }
        }

    }

    protected void btnplanchange_ClickallFree(object sender, EventArgs e)
    {
        string PortalMasterId1;
        string str1dd = @" SELECT DISTINCT dbo.PricePlanMaster.VersionInfoMasterId, CONVERT(nvarchar, dbo.LicenseMaster.LicenseDate, 101) AS LicenseDate, dbo.LicenseMaster.LicensePeriod,  dbo.PricePlanMaster.PricePlanId, dbo.PricePlanMaster.PricePlanName, dbo.PricePlanMaster.PricePlanDesc, dbo.PricePlanMaster.active,  dbo.PricePlanMaster.StartDate, dbo.PricePlanMaster.EndDate, dbo.PricePlanMaster.PricePlanAmount, dbo.PricePlanMaster.ProductId,  dbo.PricePlanMaster.DurationMonth, dbo.PricePlanMaster.AllowIPTrack, dbo.PricePlanMaster.GBUsage, dbo.PricePlanMaster.MaxUser,  dbo.PricePlanMaster.TrafficinGB, dbo.PricePlanMaster.TotalMail, dbo.PricePlanMaster.CheckIntervalDays, dbo.PricePlanMaster.GraceDays, dbo.PricePlanMaster.VersionInfoMasterId AS Expr1, dbo.PricePlanMaster.PayperOrderPlan, dbo.PricePlanMaster.amountperOrder,  dbo.PricePlanMaster.FreeIntialOrders, dbo.PricePlanMaster.MinimumDeposite, dbo.PricePlanMaster.Maxamount, dbo.PricePlanMaster.FoldersizeMB, dbo.PricePlanMaster.PriceplancatId, dbo.PricePlanMaster.PortalMasterId1, dbo.PricePlanMaster.Producthostclientserver, dbo.PricePlanMaster.IsItFreeTryOutPlan, dbo.PricePlanMaster.CompanyPrefix, dbo.PricePlanMaster.ConfigurationText, dbo.PricePlanMaster.EmailContent, dbo.PricePlanMaster.BasePrice,  dbo.PricePlanMaster.Plancatedefault, dbo.PortalMasterTbl.PortalName
                             FROM            dbo.LicenseMaster INNER JOIN  dbo.CompanyMaster ON dbo.CompanyMaster.CompanyId = dbo.LicenseMaster.CompanyId INNER JOIN   dbo.PricePlanMaster ON dbo.PricePlanMaster.PricePlanId = dbo.CompanyMaster.PricePlanId INNER JOIN    dbo.VersionInfoMaster ON dbo.PricePlanMaster.VersionInfoMasterId = dbo.VersionInfoMaster.VersionInfoId INNER JOIN  dbo.PortalMasterTbl ON dbo.PricePlanMaster.PortalMasterId1 = dbo.PortalMasterTbl.Id where CompanyMaster.CompanyId = '" + Session["CompanyId"].ToString() + "' AND dbo.PricePlanMaster.PricePlanId=" + Request.QueryString["ppid"] + " ";

        DataTable dtc = datafill(str1dd);
        if (dtc.Rows.Count > 0)
        {
            lblexistpamt.Text = Convert.ToString(dtc.Rows[0]["PricePlanAmount"]);
            lblplid.Text = Convert.ToString(dtc.Rows[0]["PricePlanId"]);
            lblexistplanname.Text = Convert.ToString(dtc.Rows[0]["PricePlanName"]);
            lbldesctext.Text = Convert.ToString(dtc.Rows[0]["PricePlanDesc"]);
            lblvalidityperiod.Text = Convert.ToString(dtc.Rows[0]["DurationMonth"]);
            lblsubcrubdate.Text = Convert.ToString(dtc.Rows[0]["LicenseDate"]);
            lblexdate.Text = (Convert.ToDateTime(dtc.Rows[0]["LicenseDate"]).AddDays(Convert.ToDouble(lblvalidityperiod.Text)).ToShortDateString()).ToString();
            PortalMasterId1 = Convert.ToString(dtc.Rows[0]["PortalMasterId1"]);
            string PortalMastername = Convert.ToString(dtc.Rows[0]["PortalName"]);
            string VersionInfoMasterId = Convert.ToString(dtc.Rows[0]["VersionInfoMasterId"]);

            //  Response.Redirect("http://license.busiwiz.com/priceplancomparisionRenew.aspx?Id=" + VersionInfoMasterId + "&PN=" + PortalMastername + "");  

        }
        string str1dd1 = "select * from PricePlanMaster  where VersionInfoMasterId='" + dtc.Rows[0]["VersionInfoMasterId"] + "' AND dbo.PricePlanMaster.PricePlanId=" + Request.QueryString["ppid"] + " AND PortalMasterId1='" + dtc.Rows[0]["PortalMasterId1"] + "' ";
        DataTable dtc1 = datafill(str1dd1);
        if (dtc1.Rows.Count > 0)
        {
            GridView1.DataSource = dtc1;
            GridView1.DataBind();
            GridView1.Columns[5].Visible = false;
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
        ModalPopupExtender3.Show();
    }
    protected void ddlportal_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }

    protected DataTable datafill(string scr)
    {
         
         SqlCommand cmd1dd = new SqlCommand(scr, conLicenseBusiwiz);
                DataTable dt1dd = new DataTable();
                SqlDataAdapter da1dd = new SqlDataAdapter(cmd1dd);
                da1dd.Fill(dt1dd);
                return dt1dd;
    }
    protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
    {
        ModalPopupExtender3.Hide();
    }
    protected void btnpchange_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((Button)sender).Parent.Parent as GridViewRow;

        int rinrow = row.RowIndex;
        //Label lblportal = (Label)GridView1.Rows[rinrow].FindControl("lblportal");

      //  Response.Redirect("http://license.busiwiz.com/Priceplancomparision.aspx?Id=" + Llbproductid.Text + "&PN=" + lblportal.Text + "&type=Customer");


        int remainday = 0;
        double payamt = 0;
        double lpanunusedamt = 0;
        remainday = Convert.ToDateTime(lblexdate.Text).Subtract(DateTime.Now).Days;
       
        Label lblpid = (Label)GridView1.Rows[rinrow].FindControl("lblpid");
        Label lblpamt = (Label)GridView1.Rows[rinrow].FindControl("lblpamt");
        if (remainday > 0)
        {
            lpanunusedamt = Convert.ToDouble(lblexistpamt.Text) * remainday / Convert.ToDouble(lblvalidityperiod.Text);
            lpanunusedamt = (Math.Round(lpanunusedamt, 2));
            payamt = Convert.ToDouble(lblpamt.Text) - lpanunusedamt;
            payamt = (Math.Round(payamt, 2));
        }
        else
        {
            payamt = Convert.ToDouble(lblpamt.Text);
        }
        string str1dd1 = "select Top(1) * from  OrderMaster  where CompanyLoginId='" + ViewState["comid"] + "' and Status='1' order by OrderId Desc";
        DataTable dtcorder = datafill(str1dd1);
        int onrderno = 0;
        if (dtcorder.Rows.Count > 0)
        {
            string sxx = "INSERT INTO OrderMaster(PlanId,CompanyLoginId,AdminId, Password,Status,PriviousActiveorderId,Amount,UnusedAmtPriviousorder,OrderType)values " +
                " ('" + lblpid.Text + "','" + ViewState["comid"] + "','" + dtcorder.Rows[0]["AdminId"] + "','" + dtcorder.Rows[0]["Password"] + "','0','" + dtcorder.Rows[0]["OrderId"] + "','" + String.Format("{0:0.00} ", payamt) + "','" + String.Format("{0:0.00} ", lpanunusedamt) + "','Change')";
            SqlCommand cmd = new SqlCommand(sxx, conLicenseBusiwiz);
            conLicenseBusiwiz.Open();
            cmd.ExecuteNonQuery();
            conLicenseBusiwiz.Close();
            string str1dd123 = "select Max(OrderId) as OrderId from  OrderMaster  where CompanyLoginId='" + ViewState["comid"] + "'";
            DataTable dtcorder23 = datafill(str1dd123);

            if (dtcorder23.Rows.Count > 0)
            {
                onrderno = Convert.ToInt32(dtcorder23.Rows[0]["OrderId"]);
            }


        }

        string strpaypalAtmp1 = "update paymentinfo set amount='" + String.Format("{0:0.00} ", payamt) + "',ordermasterid='" + onrderno + "' where compid='" + ViewState["comid"] + "' and salesorderid IS NULL ";
        SqlCommand cmdpapalatmp1 = new SqlCommand(strpaypalAtmp1, conLicenseBusiwiz);
        conLicenseBusiwiz.Open();
        cmdpapalatmp1.ExecuteNonQuery();
        conLicenseBusiwiz.Close();
        string str5 = "SELECT paymentinfoid FROM  paymentinfo where compid='" + ViewState["comid"] + "' and salesorderid IS NULL ";
        SqlCommand cmd5 = new SqlCommand(str5, conLicenseBusiwiz);
        conLicenseBusiwiz.Open();
        int i = Convert.ToInt32(cmd5.ExecuteScalar());
        conLicenseBusiwiz.Close();
        Response.Redirect("http://paymentgateway.safestserver.com/paymentnow.aspx?payid=" + i + "");

            
    }
    protected void btnplanrenue_Click(object sender, EventArgs e)
    {
        string str1dd1 = "select Top(1) * from  OrderMaster  where CompanyLoginId='" + ViewState["comid"] + "' and Status='1' order by OrderId Desc";
        DataTable dtcorder = datafill(str1dd1);
        int onrderno = 0;
        if (dtcorder.Rows.Count > 0)
        {
            string sxx = "INSERT INTO OrderMaster(PlanId,CompanyLoginId,AdminId, Password,Status,PriviousActiveorderId,Amount,UnusedAmtPriviousorder,OrderType)values " +
                " ('" + dtcorder.Rows[0]["PlanId"] + "','" + ViewState["comid"] + "','" + dtcorder.Rows[0]["AdminId"] + "','" + dtcorder.Rows[0]["Password"] + "','0','" + dtcorder.Rows[0]["OrderId"] + "','" + String.Format("{0:0.00} ", ViewState["pamt"]) + "','','Renue')";
            SqlCommand cmd = new SqlCommand(sxx, conLicenseBusiwiz);
            conLicenseBusiwiz.Open();
            cmd.ExecuteNonQuery();
            conLicenseBusiwiz.Close();
            string str1dd123 = "select Max(OrderId) as OrderId from  OrderMaster  where CompanyLoginId='" + ViewState["comid"] + "' order by OrderId Desc ";
            DataTable dtcorder23 = datafill(str1dd123);

            if (dtcorder23.Rows.Count > 0)
            {
                onrderno = Convert.ToInt32(dtcorder23.Rows[0]["OrderId"]);
            }


        }
        string strpaypalAtmp1 = "update paymentinfo set amount='" + String.Format("{0:0.00} ", ViewState["pamt"]) + "',ordermasterid='" + onrderno + "' where compid='" + ViewState["comid"] + "' and salesorderid IS NULL ";
        SqlCommand cmdpapalatmp1 = new SqlCommand(strpaypalAtmp1, conLicenseBusiwiz);
        conLicenseBusiwiz.Open();
        cmdpapalatmp1.ExecuteNonQuery();
        conLicenseBusiwiz.Close();
        string str5 = "SELECT paymentinfoid FROM  paymentinfo where compid='" + ViewState["comid"] + "' and salesorderid IS NULL ";
        SqlCommand cmd5 = new SqlCommand(str5, conLicenseBusiwiz);
        conLicenseBusiwiz.Open();
        int i = Convert.ToInt32(cmd5.ExecuteScalar());
        conLicenseBusiwiz.Close();
        //Response.Redirect("http://paymentgateway.safestserver.com/paymentnow.aspx?payid=" + i + "");
        //Response.Redirect("Conform_Payment.aspx?paymentinfoid=" + i + "&user=Client&paymode=" + drppaymode.SelectedValue + "&paymodename=" + drppaymode.SelectedItem.Text + "");
        Response.Redirect("http://license.busiwiz.com/Conform_Payment.aspx?paymentinfoid=" + i + "&user=Client");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Companymoreinfo.aspx");
    }

    //--------------------------------------------------------------------------------------------
    //------------------------------
    protected DataTable select(string str)
    {
        SqlCommand cmd = new SqlCommand(str, conLicenseBusiwiz);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);

        return dt;

    }
    protected void fillPortal()
    {
        string porid = "";
        if (Request.QueryString["poid"] != "")
        {
            porid = " and id != "+Request.QueryString["poid"]+"";
        }
        DataTable dtcln = select(" SELECT dbo.PortalMasterTbl.* FROM dbo.PortalMasterTbl Where Status=1 AND ProductId=" + Session["ProductId"] + " "+ porid +" ");
        gvallrest.DataSource = dtcln;
        gvallrest.DataBind();
       
        pnlpopup.Visible = true; 
    }


    protected void gvallrest_RowCommand(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //int customerId = Convert.ToInt32(gvCustomers.DataKeys[e.Row.RowIndex].Value.ToString());
            Label lblcb = (Label)e.Row.FindControl("lblcid");
            Label lbllogo = (Label)e.Row.FindControl("lbllogo");
            Image img_logo = (Image)e.Row.FindControl("img_logo");
            //if (File.Exists == "~//images//" + lbllogo.Text + "")
            if (File.Exists("~//images//" + lbllogo.Text + ""))
            {
                img_logo.ImageUrl = "~//images//" + lbllogo.Text + "";
            }
            else
            {
              //  img_logo.Visible = false;
                img_logo.ImageUrl = "~//images//LogoDefault.png"; 
            }
            Label lblportal = (Label)e.Row.FindControl("lblportal");
            Label lblpid = (Label)e.Row.FindControl("lblpid");
            
            HtmlAnchor HR11 = (HtmlAnchor)e.Row.FindControl("HR11");
            //HR11.HRef = "http://license.busiwiz.com/Priceplancomparision.aspx?Id=" + Session["ProductId"] + "&PN=" + lblportal.Text + "&type=Customer";
            HR11.HRef = "CompanyProfileForEdit.aspx?type=portalselect&portid=" + lblpid.Text + "";
            
            GridView gvOrdersPRICEPLN = e.Row.FindControl("gvOrdersPRICEPLN") as GridView;


            DataTable dtsd = select(" SELECT * From Productfeaturetbl Where portalid=" + lblpid.Text + " ");
            gvOrdersPRICEPLN.DataSource = dtsd;
            gvOrdersPRICEPLN.DataBind();

        }
    }
  
    //////////////////////////////////////////////////
    //////////////////////////////////////////////////
  
     
   


}
