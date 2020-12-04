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
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Diagnostics;
using System.Text.RegularExpressions;

public partial class ShoppingCart_Admin_CompanySiteAboutUsInput : System.Web.UI.Page
{
    SqlConnection con;
   // Session["Comid"]="";
    protected void Page_Load(object sender, EventArgs e)
    {
        TextBox1.Attributes.Add("onkeypress", "return tbLimit();");
        TextBox1.Attributes.Add("onkeyup", "return tbCount(" + Label1.ClientID + ");");
        TextBox1.Attributes.Add("maxLength", "500");

        PageConn pgcon = new PageConn();
        con = pgcon.dynconn; 

        //geting comapany name from partymastertable..
        string str1 = "select * from CompanyMaster where Compid='" + Session["Comid"].ToString() + "'";
        SqlCommand cmd = new SqlCommand(str1, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        if (ds.Rows.Count > 0)
        {
          ViewState["companyname"] = ds.Rows[0]["CompanyName"].ToString();
        }
        //closed



    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("CompanySiteAboutUsInput.aspx");
    }
    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
        Panel13.Visible = true;
    }
    protected void Button7_Click(object sender, EventArgs e)
    {
        
        FileUpload1.Visible = true;
        Button18.Visible = false;
        Button20.Visible = true;
    }

    public bool ext111(string filename)
    {
        string[] validFileTypes = { "jpeg", "bmp", "gif", "png", "jpg" };

        string ext = System.IO.Path.GetExtension(filename);

        bool isValidFile = false;

        for (int i = 0; i < validFileTypes.Length; i++)
        {

            if (ext == "." + validFileTypes[i])
            {

                isValidFile = true;

                break;

            }

        }
        return isValidFile;
    }
    protected void Button8_Click(object sender, EventArgs e)
    {
        Button20.Visible = false;
        Button18.Visible = true;
        Label17.Visible = true;
         bool valid = ext111(FileUpload1.FileName);

         if (valid == true)
         {

               if (FileUpload1.HasFile == true)
            {
                if (Directory.Exists(Server.MapPath("~\\ShoppingCart\\images\\")) == false)
                {
                    Directory.CreateDirectory(Server.MapPath("~\\ShoppingCart\\images\\"));
                }
                FileUpload1.PostedFile.SaveAs(Server.MapPath("~\\ShoppingCart\\images\\") + FileUpload1.FileName);
                   cphMain_ucAboutUs_imgITimeKeeperLogo.Height= 160 ;
                   cphMain_ucAboutUs_imgITimeKeeperLogo.Width = 225;
                   cphMain_ucAboutUs_imgITimeKeeperLogo.ImageUrl = "~\\ShoppingCart\\images\\"+ FileUpload1.FileName ;
                   ViewState["path"] = FileUpload1.FileName.ToString();
              //string fn = FileUpload1.FileName.ToString();
              //Session["fn"] = fn;
            }
             
               Label17.Text = "image uploaded";

              
             
        }
        else
        {
      Label17.Text = "Invalid File Type.Please upload file in one of the following formats:jpeg, bmp, gif, png, jpg";
        }
         FileUpload1.Visible = false;

       // ---------------image url-------------------------------------------------


         //con.Open();
         //string cmd1 = "select * from CompanyAboutUsInputTBL where companyid=3 ";
         //SqlDataAdapter da = new SqlDataAdapter(cmd1, con);
         //DataTable dt = new DataTable();
         //da.Fill(dt);


         //string path = dt.Rows[0][3].ToString();
         //cphMain_ucAboutUs_imgITimeKeeperLogo.ImageUrl = path;// "~/images/aboutus.jpg";
         //con.Close();



    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        Panel13.Visible = true;

        TextBox1.Visible = true;
        Button4.Visible = true;
    }
   
    protected void submit(object sender, EventArgs e)
    {
        Label17.Text = "Your request to make changes has been submitted and is pending authorization";
        inserttable();
        getcompid();
       // unapprovedstatus();
          sendmailtoAdmin();
       
       



    }
    public void getcompid()//on submit
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("select MAX(ID)from CompanyAboutUsInputTBL where CompanyId='" + Session["comid"] + "'", con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ViewState["id"] = Convert.ToInt32(dt.Rows[0][0]);
           // ViewState["companyid"] = Convert.ToInt32(dt.Rows[0]["ID"].ToString());// coloumn not taken while passing in quates 
        }
        con.Close();
    }
    public void inserttable()//on submit

    {
               con.Open();
               SqlCommand cmd = new SqlCommand("insert into CompanyAboutUsInputTBL (Companyid,Aboutus,Aboutusimage,Dateandtime,Status ) values('" + Session["Comid"] + "','" + TextBox1.Text + "','" + ViewState["path"] + "','" + DateTime.Now.ToString() + "','Unapproved')", con);
        cmd.ExecuteNonQuery();
        con.Close();
        string tb1 = TextBox1.Text.ToString();
        Session["tb1"] = tb1;
    }
    public void unapprovedstatus()//on submit

    {
        con.Open();
        SqlCommand cmd = new SqlCommand("update CompanyAboutUsInputTBL set status='Unapproved' where Companyid='herrykem12'",con);
        cmd.ExecuteNonQuery();
        con.Close();
    }
    public void sendmailtoAdmin() // mail send to support@ijobcenter.com admin
    {
        // Session["Comid"] = "d1989";
        string ADDRESSEX = "";
        string logg = "";
        string business = "";

        if (Request.QueryString["Id"] != null)
        {
            ADDRESSEX = "SELECT distinct CompanyMaster.CompanyLogo, CompanyMaster.CompanyName,CompanyWebsitMaster.Sitename,CompanyWebsitMaster.MasterEmailId,CompanyWebsitMaster.EmailMasterLoginPassword,CompanyWebsitMaster.OutGoingMailServer, CompanyWebsitMaster.EmailSentDisplayName,CompanyWebsitMaster.SiteUrl,CompanyWebsiteAddressMaster.Address1,CompanyWebsiteAddressMaster.Address2,CompanyWebsiteAddressMaster.Phone1, CompanyWebsiteAddressMaster.Phone2, CompanyWebsiteAddressMaster.TollFree1, CompanyWebsiteAddressMaster.Fax,CompanyWebsiteAddressMaster.Email,CompanyMaster.CompanyId,CompanyWebsitMaster.WHid FROM  CompanyMaster LEFT OUTER JOIN AddressTypeMaster RIGHT OUTER JOIN CompanyWebsiteAddressMaster ON AddressTypeMaster.AddressTypeMasterId = CompanyWebsiteAddressMaster.AddressTypeMasterId RIGHT OUTER JOIN CompanyWebsitMaster ON CompanyWebsiteAddressMaster.CompanyWebsiteMasterId = CompanyWebsitMaster.CompanyWebsiteMasterId ON CompanyMaster.CompanyId = CompanyWebsitMaster.CompanyId where  WHId='1'";
            ADDRESSEX = "SELECT Id, ProductId, PortalName, DefaultPagename, LogoPath, EmailDisplayname, EmailId, UserIdtosendmail, Password, Mailserverurl, Supportteamemailid, Supportteamphoneno, Supportteammanagername, Portalmarketingwebsitename, Address1, Address2, CountryId, StateId, City, Zip, PhoneNo, Fax, Status, Supportteamphonenoext, Tollfree, Tollfreeext, CompanyCreationOption, DatabaseURL, DatabaseName, PortNo, UserID, UserPassword, PortalRunningCompanyID, Colorportal FROM   dbo.PortalMasterTbl WHERE (ProductId = '2056') AND(Id = '7')";
            logg = "select LogoUrl from CompanyWebsitMaster where whid='1'";

            business = "select Warehousemaster.Name,CompanyWebsiteAddressMaster.Address1,CompanyWebsiteAddressMaster.Address2,CompanyWebsiteAddressMaster.Phone1,CompanyWebsiteAddressMaster.Email From Warehousemaster inner join CompanyWebsiteAddressMaster on CompanyWebsiteAddressMaster.CompanyWebsiteMasterId=Warehousemaster.WarehouseID where WarehouseID='1'";
        }
        else
        {
            ADDRESSEX = "SELECT distinct CompanyMaster.CompanyLogo, CompanyMaster.CompanyName,CompanyWebsitMaster.Sitename,CompanyWebsitMaster.MasterEmailId,CompanyWebsitMaster.EmailMasterLoginPassword,CompanyWebsitMaster.OutGoingMailServer, CompanyWebsitMaster.EmailSentDisplayName,CompanyWebsitMaster.SiteUrl,CompanyWebsiteAddressMaster.Address1,CompanyWebsiteAddressMaster.Address2,CompanyWebsiteAddressMaster.Phone1, CompanyWebsiteAddressMaster.Phone2, CompanyWebsiteAddressMaster.TollFree1, CompanyWebsiteAddressMaster.Fax,CompanyWebsiteAddressMaster.Email,CompanyMaster.CompanyId,CompanyWebsitMaster.WHid FROM  CompanyMaster LEFT OUTER JOIN AddressTypeMaster RIGHT OUTER JOIN CompanyWebsiteAddressMaster ON AddressTypeMaster.AddressTypeMasterId = CompanyWebsiteAddressMaster.AddressTypeMasterId RIGHT OUTER JOIN CompanyWebsitMaster ON CompanyWebsiteAddressMaster.CompanyWebsiteMasterId = CompanyWebsitMaster.CompanyWebsiteMasterId ON CompanyMaster.CompanyId = CompanyWebsitMaster.CompanyId where WHId='1'";
            ADDRESSEX = " SELECT Id, ProductId, PortalName, DefaultPagename, LogoPath, EmailDisplayname, EmailId, UserIdtosendmail, Password, Mailserverurl, Supportteamemailid, Supportteamphoneno, Supportteammanagername, Portalmarketingwebsitename, Address1, Address2, CountryId, StateId, City, Zip, PhoneNo, Fax, Status, Supportteamphonenoext, Tollfree, Tollfreeext, CompanyCreationOption, DatabaseURL, DatabaseName, PortNo, UserID, UserPassword, PortalRunningCompanyID, Colorportal FROM   dbo.PortalMasterTbl WHERE (ProductId = '2056') AND(Id = '7')";

            logg = "select LogoUrl from CompanyWebsitMaster where whid='1'";

            business = "select Warehousemaster.Name,CompanyWebsiteAddressMaster.Address1,CompanyWebsiteAddressMaster.Address2,CompanyWebsiteAddressMaster.Phone1,CompanyWebsiteAddressMaster.Email From Warehousemaster inner join CompanyWebsiteAddressMaster on CompanyWebsiteAddressMaster.CompanyWebsiteMasterId=Warehousemaster.WarehouseID where WarehouseID='1'";
        }


        SqlCommand cmd = new SqlCommand(ADDRESSEX, PageConn.licenseconn());
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);

        SqlDataAdapter dalog = new SqlDataAdapter(logg, con);
        DataTable dtlog = new DataTable();
        dalog.Fill(dtlog);

        SqlDataAdapter dabus = new SqlDataAdapter(business, con);
        DataTable dtbus = new DataTable();
        dabus.Fill(dtbus);

        StringBuilder HeadingTable = new StringBuilder();
        HeadingTable.Append("<table width=\"100%\"> ");

        string file = "job-center-logo-changes 33.png";

        HeadingTable.Append("<tr><td width=\"50%\" style=\" align=\"left\" > <img src=\"http://" + Request.Url.Host.ToString() + "/images/" + file + "\" \"border=\"0\" Width=\"90\" Height=\"80\" / > </td></tr>  ");

        HeadingTable.Append("</table> ");


    //    string loginurl = "";//Request.Url.Host.ToString() + "/Shoppingcart/Admin/ResetPasswordUser.aspx";
        //string accountdetailofparty = "SELECT DISTINCT max(ID)  dbo.VacancyMasterTbl.ID, dbo.VacancyMasterTbl.BusinessID, dbo.VacancyMasterTbl.DesignationID, dbo.VacancyMasterTbl.vacancypositiontypeid, dbo.VacancyMasterTbl.vacancypositiontitleid,                          dbo.VacancyMasterTbl.noofvacancy, dbo.VacancyMasterTbl.startdate, dbo.VacancyMasterTbl.enddate, dbo.VacancyMasterTbl.currencyid, dbo.VacancyMasterTbl.salaryamount,                          dbo.VacancyMasterTbl.salaryperperiodid, dbo.VacancyMasterTbl.worktimings, dbo.VacancyMasterTbl.hours, dbo.VacancyMasterTbl.hoursperperiodid, dbo.VacancyMasterTbl.vacancyftptid,                          dbo.VacancyMasterTbl.status, dbo.VacancyMasterTbl.contactname, dbo.VacancyMasterTbl.contactEmail, dbo.VacancyMasterTbl.contactphoneno, dbo.VacancyMasterTbl.contactAddress,                          dbo.VacancyMasterTbl.applybyemail, dbo.VacancyMasterTbl.applybyphone, dbo.VacancyMasterTbl.applybyvisit, dbo.VacancyMasterTbl.applyonline, dbo.VacancyMasterTbl.countryid,                          dbo.VacancyMasterTbl.stateid, dbo.VacancyMasterTbl.cityid, dbo.VacancyMasterTbl.vacancyduration, dbo.VacancyMasterTbl.comid, dbo.VacancyFTPT.Name AS Term, dbo.VacancyMasterTbl.ID AS candidate,                          dbo.VacancyMasterTbl.ID AS Expr1, CASE WHEN (VacancyMasterTbl.status = '1') THEN 'Active By Ijob' WHEN (VacancyMasterTbl.status = '2') THEN 'Active' ELSE 'Inactive' END AS Statuslabel, CAST(dbo.VacancyMasterTbl.salaryamount AS float) AS salary,                          dbo.SalaryPerPeriodMaster.Name AS sss, LEFT(dbo.VacancyDetailTbl.JobFunction, 100) AS JobFunction, LEFT(dbo.VacancyDetailTbl.QualificationRequirements, 25) AS QualificationRequirements,                          dbo.VacancyDetailTbl.TermsandConditions, dbo.CurrencyMaster.CurrencyName AS ccc, dbo.VacancyFTPT.Name AS vvv, dbo.WareHouseMaster.Name AS wname,                          dbo.DepartmentmasterMNC.Departmentname + ':' + dbo.DesignationMaster.DesignationName AS dname, dbo.VacancyTypeMaster.Name AS vname, dbo.DesignationMaster.DesignationName,                          dbo.DepartmentmasterMNC.Departmentname, dbo.CityMasterTbl.CityName FROM            dbo.VacancyMasterTbl INNER JOIN                          dbo.WareHouseMaster ON dbo.WareHouseMaster.WareHouseId = dbo.VacancyMasterTbl.BusinessID INNER JOIN                         dbo.DesignationMaster ON dbo.DesignationMaster.DesignationMasterId = dbo.VacancyMasterTbl.DesignationID INNER JOIN                         dbo.DepartmentmasterMNC ON dbo.DesignationMaster.DeptID = dbo.DepartmentmasterMNC.id INNER JOIN                         dbo.VacancyTypeMaster ON dbo.VacancyTypeMaster.ID = dbo.VacancyMasterTbl.vacancypositiontypeid INNER JOIN                         dbo.CurrencyMaster ON dbo.CurrencyMaster.CurrencyId = dbo.VacancyMasterTbl.currencyid INNER JOIN                         dbo.VacancyFTPT ON dbo.VacancyFTPT.Id = dbo.VacancyMasterTbl.vacancyftptid INNER JOIN                         dbo.SalaryPerPeriodMaster ON dbo.SalaryPerPeriodMaster.ID = dbo.VacancyMasterTbl.salaryperperiodid INNER JOIN                         dbo.VacancyDetailTbl ON dbo.VacancyMasterTbl.ID = dbo.VacancyDetailTbl.vacancymasterid INNER JOIN                         dbo.CityMasterTbl ON dbo.VacancyMasterTbl.cityid = dbo.CityMasterTbl.CityId where VacancyMasterTbl.comid='" + Session["Comid"] + "' ";
        //SqlCommand cmdpartydetail = new SqlCommand(accountdetailofparty, con);
        //SqlDataAdapter adppartydetail = new SqlDataAdapter(cmdpartydetail);
        //DataTable dspartydetail = new DataTable();
       
        //adppartydetail.Fill(dspartydetail);

        //string AccountInfo;
        //string Accountdetail = "<br>";

        //AccountInfo = ""; //"The following company has posted a Freelance Project in ijobcenter.com<br> <br><br><b>Vacancy Information: </b><br><br>Company Id: " + Session["comid"] + "<br>Department : " +""+ "<br>Project Name: " +"fgyugfy"+ "<br>Project Duration: " + "uiuhyi9" + "<br> Expected Maximum Project Hours: " + "ftyf" + "<br>";
        //for the position of - " + Label11.Text + <br>You can login to our system to see the status of your job application as well as to contact us.<br>

        //Accountdetail12 = "You may view this listing  click <a href=http://" + Request.Url.Host.ToString() + "/Shoppingcart/admin/VacancyDetail.aspx?id=" + Session["vac_MaxId"] + " target=_blank > here </a> Alternatively, you may copy and paste the following URL into your browser <br><br>http://" + Request.Url.Host.ToString() + "/Shoppingcart/admin/VacancyDetail.aspx?id=" + Session["vac_MaxId"] + "<br><br><br> ";
       
        //Accountdetail12 += " Do you wish to Approve this Freelance Project and activate this listing on www.ijobcenter.com?  If yes, click <a href=http://" + Request.Url.Host.ToString() + "/Shoppingcart/admin/ApprovalFreelance.aspx?FreelanceIDAcept=" + ClsEncDesc.Encrypted(ViewState["promaxid"].ToString()) + "  target=_blank > Yes </a> Alternatively, Or, " +
        //    " <br> copy and paste the following URL in your browser http://" + Request.Url.Host.ToString() + "/Shoppingcart/admin/ApprovalFreelance.aspx?FreelanceIDAcept=" + ClsEncDesc.Encrypted(ViewState["promaxid"].ToString()) + "  ";

        //Accountdetail12 += " <br><br>Do you wish to Reject this Freelance Project If yes, click <a href=http://" + Request.Url.Host.ToString() + "/Shoppingcart/admin/ApprovalFreelance.aspx?FreelanceIDReject=" + ClsEncDesc.Encrypted(ViewState["promaxid"].ToString()) + "  target=_blank > Yes </a> Alternatively, Or, " +
        //    " copy and paste the following URL in your browser http://" + Request.Url.Host.ToString() + "/Shoppingcart/admin/ApprovalFreelance.aspx?FreelanceIDReject=" + ClsEncDesc.Encrypted(ViewState["promaxid"].ToString()) + "  ";
       
        
        string Accountdetail12 = "";
        Accountdetail12 += " " + ViewState["companyname"] + " is requesting that changes be made to their About Us page. You can review the changes and decide whether to approve or reject the requested changes by clicking  <a href=http://www.ijobcenter.com/CompanySiteAboutUsViewforApproval.aspx?id1=" + ClsEncDesc.Encrypted(ViewState["id"].ToString()) + " target=_blank > here </a> or by copy and pasting the following URL into your internet browser.<br/> <br/> www.ijobcenter.com/CompanySiteAboutUsViewforApproval.aspx?id1=" + ClsEncDesc.Encrypted(ViewState["id"].ToString()) + " <br/> </br> <br/> </br> ";
        
        
        string ext = "ext " + ds.Rows[0]["Supportteamphonenoext"].ToString();
        string tollfreeext = "ext " + ds.Rows[0]["Tollfreeext"].ToString();
        string tollfree = ds.Rows[0]["Tollfree"].ToString();
        string aa = "" + ds.Rows[0]["Supportteammanagername"].ToString() + "- Support Manager";
        string bb = "" + ds.Rows[0]["PortalName"].ToString() + " ";
        string cc = "" + ds.Rows[0]["Supportteamphoneno"].ToString() + "  " + ext + " ";
        string dd = "" + ds + " " + tollfreeext + " ";
        string ee = "" + ds.Rows[0]["Portalmarketingwebsitename"].ToString() + "";

        string body = "<br>" + HeadingTable + " Dear <strong><span style=\"color: #996600\"> Admin </span></strong>,<br><br> " + Accountdetail12 + " <br> <span style=\"font-size: 10pt; color: #000000; font-family: Arial\"> " +
            //   " <br>Thanking you,<br>Sincerely,</span><br><br>Admin Team<br><b>" + dtbus.Rows[0]["Name"].ToString() + "</b><br>" + dtbus.Rows[0]["Address1"].ToString() + "<br>" + dtbus.Rows[0]["Address2"].ToString() + "";
            " <br>Thanking you, <br>" + aa + "<br>" + bb + "<br>" + cc + "<br>" + ee + "";

        if (ds.Rows[0]["UserIdtosendmail"].ToString() != "" && ds.Rows[0]["Password"].ToString() != "")
        {
            try
            {
                MailAddress to = new MailAddress("" + ds.Rows[0]["Supportteamemailid"].ToString() + "");
                MailAddress from = new MailAddress("" + ds.Rows[0]["UserIdtosendmail"] + "", "" + ds.Rows[0]["EmailDisplayname"] + "");
                MailMessage objEmail = new MailMessage(from, to);

                objEmail.Subject = "Company Site About Us Changes Requested";

                objEmail.Body = body.ToString();
                objEmail.IsBodyHtml = true;
                objEmail.Priority = MailPriority.Normal;
                SmtpClient client = new SmtpClient();
                client.Credentials = new NetworkCredential("" + ds.Rows[0]["UserIdtosendmail"] + "", "" + ds.Rows[0]["Password"] + "");
                client.Host = ds.Rows[0]["Mailserverurl"].ToString();

                client.Send(objEmail);
            }
            catch { }



        }
        else
        {


        }





    }

    protected void Button7(object sender, EventArgs e)
    {

    }
    protected void Button19_Click(object sender, EventArgs e)
    {
        Response.Redirect("CompanySiteAboutUs.aspx");
    }
}
