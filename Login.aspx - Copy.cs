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
using System.Data.SqlClient;
using System.Text;
using System.DirectoryServices;
using System.IO.Compression;
using Ionic.Zip;
using System.Security.Cryptography;
using Microsoft.Win32;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Xml;
using System.Net;
using System.Net.Mail;
using System.IO.Compression;
//using System.IO.Compression.ZipArchive;
public partial class Login : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();//(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
   // SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    SqlConnection con_Lice_Job = new SqlConnection(ConfigurationManager.ConnectionStrings["JobcenterLicense"].ConnectionString);
   
    protected void Page_Load(object sender, EventArgs e)
    {
        string assss="32";
        assss = PageMgmt.Encrypted("Inventorycategorymaster.aspx");
        assss = PageMgmt.Decrypted("lpSVW07ER8Q=");
       
        Session.Clear();
        if (!IsPostBack)
        {
            lblmsg.Visible = false;                                
        }




        con = PageConn.licenseconn(); 


//        string str = @" DECLARE @Pivot_Column [nvarchar](max);  
//                        DECLARE @Query [nvarchar](max); 
//                        SELECT @Pivot_Column= COALESCE(@Pivot_Column+',','')+ QUOTENAME(CategoryName) FROM  
//                        (SELECT DISTINCT [CategoryName] FROM PageAccessToPricePlanCategory Where CategoryName IS NOT NULL)Tab 
//                        SELECT @Query='SELECT PageId,PageName, '+@Pivot_Column+'FROM   
//                        (SELECT PageId,PageName, [CategoryName] , Id   FROM PageAccessToPricePlanCategory )Tab1  
//                        PIVOT  
//                        (  
//                        SUM(Id) FOR CategoryName IN ('+@Pivot_Column+')) AS Tab2  
//                        ORDER BY Tab2.PageName'  
//                        EXEC  sp_executesql  @Query ";//    
//        SqlCommand cmd = new SqlCommand(str, con);
//        SqlDataAdapter adp = new SqlDataAdapter(cmd);
//        DataTable dt = new DataTable();
//        adp.Fill(dt);



    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        con = PageConn.licenseconn(); 
        string ipaddress = "";
        ipaddress = Request.ServerVariables["REMOTE_ADDR"].ToString();
      //  ipaddress = "192.168.6.41";
        //Request.ServerVariables["REMOTE_ADDR"].ToString();

        Session["userloginname"] = txtUser.Text;

        //try
        //{
        string str = "   select clientLoginMaster.clientId,clientLoginMaster.Password,clientLoginMaster.UserId,ClientMaster.CompanyName from clientLoginMaster inner join  ClientMaster on ClientMaster.LoginName=clientLoginMaster.UserId " +
                 " where  clientLoginMaster.UserId='" + txtUser.Text + "' and clientLoginMaster.clientId='" + txtClientId.Text + "' " +
               " and clientLoginMaster.Password='" + PageMgmt.Encrypted(txtPassword.Text) + "' ";// 
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            Session["Login"] = txtUser.Text; // "admin";
            Session["ClientId"] = dt.Rows[0]["clientId"].ToString();
            Session["UserId"] = dt.Rows[0]["UserId"].ToString();
            string pass =PageMgmt.Encrypted(dt.Rows[0]["Password"].ToString());
            Session["ClientName"] = dt.Rows[0]["CompanyName"].ToString();
            Response.Redirect("Product_BatchMaster.aspx");
        }
        else
        {
            string str1 = "select distinct EmployeeMaster.Id as empid,clientLoginMaster.clientId,clientLoginMaster.Password,clientLoginMaster.UserId,ClientMaster.CompanyName from ClientMaster inner join clientLoginMaster on clientLoginMaster.ClientId=ClientMaster.ClientMasterId inner join EmployeeMaster on EmployeeMaster.UserId=clientLoginMaster.UserId " +
            " where  clientLoginMaster.UserId='" + txtUser.Text + "' and clientLoginMaster.clientId='" + txtClientId.Text + "' "+
           " and clientLoginMaster.Password='" + PageMgmt.Encrypted(txtPassword.Text) + "' ";// and clientLoginMaster.Password='" + PageMgmt.Encrypted(txtPassword.Text) + "'
            SqlCommand cmd1 = new SqlCommand(str1, con);
            SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            adp1.Fill(dt1);
          //  txtUser.Text = PageMgmt.Decrypted(dt1.Rows[0]["Password"].ToString());
            if (dt1.Rows.Count > 0)
            {
                Session["Clientname"] = dt1.Rows[0]["CompanyName"].ToString();
                string strIjob = " SELECT        dbo.EmployeeMaster.* FROM dbo.EmployeeMaster INNER JOIN dbo.Syncr_LicenseEmployee_With_JobcenterId ON dbo.EmployeeMaster.EmployeeMasterID = dbo.Syncr_LicenseEmployee_With_JobcenterId.Jobcenter_Emp_id  where dbo.Syncr_LicenseEmployee_With_JobcenterId.License_Emp_id='" + Convert.ToString(dt1.Rows[0]["empid"]) + "' ";
                strIjob = " SELECT        dbo.EmployeeMaster.EmployeeMasterID, dbo.EmployeeMaster.PartyID, dbo.EmployeeMaster.DeptID, dbo.EmployeeMaster.DesignationMasterId, dbo.EmployeeMaster.StatusMasterId, dbo.EmployeeMaster.EmployeeTypeId, dbo.EmployeeMaster.DateOfJoin, dbo.EmployeeMaster.Address, dbo.EmployeeMaster.CountryId, dbo.EmployeeMaster.StateId, dbo.EmployeeMaster.City, dbo.EmployeeMaster.ContactNo, dbo.EmployeeMaster.Email, dbo.EmployeeMaster.AccountId, dbo.EmployeeMaster.AccountNo, dbo.EmployeeMaster.EmployeeName, dbo.EmployeeMaster.Whid,  dbo.EmployeeMaster.Description, dbo.EmployeeMaster.SuprviserId, dbo.EmployeeMaster.Active, dbo.EmployeeMaster.WorkPhone, dbo.EmployeeMaster.WorkExt, dbo.EmployeeMaster.WorkEmail, dbo.EmployeeMaster.EducationqualificationID, dbo.EmployeeMaster.SpecialSubjectID, dbo.EmployeeMaster.yearofexperience,  dbo.EmployeeMaster.Jobpositionid, dbo.EmployeeMaster.Sex, dbo.EmployeeMaster.Amount, dbo.EmployeeMaster.Payper, dbo.User_master.UserID FROM            dbo.EmployeeMaster INNER JOIN dbo.Syncr_LicenseEmployee_With_JobcenterId ON dbo.EmployeeMaster.EmployeeMasterID = dbo.Syncr_LicenseEmployee_With_JobcenterId.Jobcenter_Emp_id INNER JOIN dbo.User_master ON dbo.EmployeeMaster.PartyID = dbo.User_master.PartyID    where dbo.Syncr_LicenseEmployee_With_JobcenterId.License_Emp_id='" + Convert.ToString(dt1.Rows[0]["empid"]) + "'  ";
                //and IpAddress ='" + ipaddress + "'
                SqlCommand cmdIjob = new SqlCommand(strIjob, con_Lice_Job);
                SqlDataAdapter adpIjob = new SqlDataAdapter(cmdIjob);
                DataTable dtIjob = new DataTable();
                adpIjob.Fill(dtIjob);
                if (dtIjob.Rows.Count > 0)
                {
                    Session["EmployeeId"] = Convert.ToString(dtIjob.Rows[0]["EmployeeMasterID"]);
                    Session["DesignationId"] = Convert.ToString(dtIjob.Rows[0]["DesignationMasterId"]);
                    Session["Cname"] = txtClientId.Text;
                    Session["Comid"] = txtClientId.Text;
                    Session["verId"] = "2056";
                    Session["PriceId"] = "10444";
                    Session["PartyId"]= Convert.ToString(dtIjob.Rows[0]["PartyId"]);
                    Session["UserId"] = dtIjob.Rows[0]["UserID"].ToString();
                    Session["whid"] = dtIjob.Rows[0]["Whid"].ToString();
                }

                SqlDataAdapter da4 = new SqlDataAdapter("SELECT DesignationName from DesignationMaster where DesignationMasterId='" + Session["DesignationId"] + "' ", con_Lice_Job);
               DataTable desi=new DataTable();
                da4.Fill(desi);

                string str11 = "Select * from EmployeeMaster  where EmployeeMaster.ClientId='" + txtClientId.Text + "' and  EmployeeMaster.Id='" + Convert.ToString(dt1.Rows[0]["empid"]) + "'  ";
                SqlCommand cmd111 = new SqlCommand(str11, con);
                SqlDataAdapter adp11 = new SqlDataAdapter(cmd111);
                DataTable dt11 = new DataTable();
                adp11.Fill(dt11);
                if (dt11.Rows.Count > 0)
                {

                    string pass = PageMgmt.Decrypted(dt1.Rows[0]["Password"].ToString());
                    Session["EmpId"] = Convert.ToString(dt1.Rows[0]["empid"]);
                    Session["Login"] = txtUser.Text; // "admin";
                    Session["ClientId"] = dt1.Rows[0]["clientId"].ToString();
                    //   Session["UserId"] = dt1.Rows[0]["UserId"].ToString();
                    Session["ClientName"] = dt1.Rows[0]["CompanyName"].ToString();
                    //Response.Redirect("afterLoginforClient.aspx");
                    Session["id"] = dt11.Rows[0]["Id"].ToString();
                    //if (dtIjob.Rows.Count > 0)
                    //{
                    //    Response.Redirect("afterLoginforClient.aspx");
                    Session["Login"] = txtUser.Text;
                    //}
                    //else
                    //{
                    //    Response.Redirect("afterLoginforClient.aspx");
                    //}

                    string strIjob1 = @"SELECT  dbo.TBLUserLoginIpRestrictionPreference.MakeIPRestriction
                                                      FROM   dbo.EmployeeMaster 
                                                     INNER JOIN   dbo.Syncr_LicenseEmployee_With_JobcenterId ON dbo.EmployeeMaster.EmployeeMasterID = dbo.Syncr_LicenseEmployee_With_JobcenterId.Jobcenter_Emp_id
                                                     INNER JOIN dbo.User_master  ON dbo.EmployeeMaster.PartyID = dbo.User_master.PartyID
                                                     INNER JOIN  dbo.TBLUserLoginIpRestrictionPreference ON dbo.TBLUserLoginIpRestrictionPreference.userid = dbo.User_master.UserID 
                                                     where dbo.Syncr_LicenseEmployee_With_JobcenterId.License_Emp_id='" + Convert.ToString(dt1.Rows[0]["empid"]) + "' ";
                    SqlCommand cmdIjob1 = new SqlCommand(strIjob1, con_Lice_Job);
                    SqlDataAdapter adpIjob1 = new SqlDataAdapter(cmdIjob1);
                    DataTable dtIjob1 = new DataTable();
                    adpIjob1.Fill(dtIjob1);
                    if (dtIjob1.Rows.Count > 0)
                    {
                        if (dtIjob1.Rows[0][0].ToString() == "False")
                        {
                            if (desi.Rows.Count > 0)
                            {
                                SqlDataAdapter databusc = new SqlDataAdapter("SELECT dbo.DefaultAfterloginForDefaultRolesTBL.id, dbo.DefaultAfterloginForDefaultRolesTBL.DefaultDesignationTbl, dbo.DefaultDesignationTbl.DesignationName, dbo.DefaultAfterloginForDefaultRolesTBL.PagemasterID, dbo.DefaultAfterloginForDefaultRolesTBL.pagename, dbo.DefaultAfterloginForDefaultRolesTBL.VersionId FROM  dbo.DefaultDesignationTbl INNER JOIN dbo.DefaultAfterloginForDefaultRolesTBL ON dbo.DefaultDesignationTbl.Id = dbo.DefaultAfterloginForDefaultRolesTBL.DefaultDesignationTbl Where dbo.DefaultDesignationTbl.DesignationName='" + desi.Rows[0][0].ToString() + "'", PageConn.licenseconn());
                                DataTable Datac = new DataTable();
                                databusc.Fill(Datac);

                                if (Datac.Rows.Count > 0)
                                {
                                    string pnameu = Convert.ToString(Datac.Rows[0]["pagename"]);
                                    string pp = "http://license.busiwiz.com/IOffice/ShoppingCart/Admin/" + pnameu + "";
                                    //Response.Redirect(pp);
                                    Response.Redirect("~/Clientadmin/AdminProjectMasterLB.aspx");
                                }
                                else
                                {
                                    //lblError.Text = "AfterLogin Page Is Not Available For Your Designetion";
                                    Response.Redirect("~/Clientadmin/AdminProjectMasterLB.aspx");
                                }
                            }
                            else
                            {
                                Response.Redirect("~/Clientadmin/AdminProjectMasterLB.aspx");
                            }
                        }
                        else
                        {
                            string dd = "  select * from IpControldetailtbl inner join IpControlMastertbl on IpControlMastertbl.IpcontrolId=IpControldetailtbl.IpcontrolId where  IpControlMastertbl.CID='" + Session["comid"] + "' and IpControldetailtbl.UserId='" + Session["UserId"] + "'  ";//and IpControldetailtbl.Ipaddress='" + ipaddress + "'
                            SqlCommand cmdIjob2 = new SqlCommand(dd, con_Lice_Job);
                            SqlDataAdapter adpIjob2 = new SqlDataAdapter(cmdIjob2);
                            DataTable dtIjob2 = new DataTable();
                            adpIjob2.Fill(dtIjob2);
                            if (dtIjob2.Rows.Count > 0)
                            {
                                SqlDataAdapter databusc = new SqlDataAdapter("SELECT dbo.DefaultAfterloginForDefaultRolesTBL.id, dbo.DefaultAfterloginForDefaultRolesTBL.DefaultDesignationTbl, dbo.DefaultDesignationTbl.DesignationName, dbo.DefaultAfterloginForDefaultRolesTBL.PagemasterID, dbo.DefaultAfterloginForDefaultRolesTBL.pagename, dbo.DefaultAfterloginForDefaultRolesTBL.VersionId FROM  dbo.DefaultDesignationTbl INNER JOIN dbo.DefaultAfterloginForDefaultRolesTBL ON dbo.DefaultDesignationTbl.Id = dbo.DefaultAfterloginForDefaultRolesTBL.DefaultDesignationTbl Where dbo.DefaultDesignationTbl.DesignationName='" + desi.Rows[0][0].ToString() + "'", PageConn.licenseconn());
                                DataTable Datac = new DataTable();
                                databusc.Fill(Datac);

                                if (Datac.Rows.Count > 0)
                                {
                                    string pnameu = Convert.ToString(Datac.Rows[0]["pagename"]);
                                    string pp = "http://license.busiwiz.com/IOffice/ShoppingCart/Admin/" + pnameu + "";
                                    Response.Redirect(pp);
                                }
                                else
                                {
                                    //lblError.Text = "AfterLogin Page Is Not Available For Your Designetion";

                                    Response.Redirect("~/Clientadmin/AdminProjectMasterLB.aspx");

                                }
                            }
                            else
                            {
                                lblmsg.Visible = true;
                                lblmsg.Text = "Invalid IP :" + ipaddress.ToString();
                            }



                        }
                    }
                    else
                    {
                        Response.Redirect("~/Clientadmin/AdminProjectMasterLB.aspx");
                    }
                }

              
            }

        }

        //}
        //catch (Exception e1)
        //{



        //}
        //finally
        //{

        //}

        //   if (txtUser.Text == "admin" && txtPassword.Text == "admin")
        //  {
        //}
        //else
        // {
        // }

    }


   



}
