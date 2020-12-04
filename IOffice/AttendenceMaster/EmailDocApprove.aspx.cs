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

public partial class EmailDocApprove : System.Web.UI.Page
{
    //public SqlConnection dynconn;
    public SqlConnection dynconn;
    public StringBuilder strplan1 = new StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
         dynconn = pgcon.dynconn;
       // dynconn = new SqlConnection();
        if (!IsPostBack)
        {

            if (Request.QueryString["cid"] != null)
            {
                HttpContext.Current.Session["Comid"] = Request.QueryString["cid"];
            }
            //string strcln = "Select Sqldatabasename,SqlconnurlIp,SqlPort,SqlUserId,SqlPassword from CompanyMaster inner join PricePlanMaster on PricePlanMaster.PricePlanId=CompanyMaster.PricePlanId inner join ProductMaster on ProductMaster.ProductId= PricePlanMaster.ProductId where CompanyMaster.CompanyLoginId='" + Convert.ToString(HttpContext.Current.Session["Comid"]) + "' and ProductMaster.Download='1'";
            //SqlCommand cmdcln = new SqlCommand(strcln, PageConn.licenseconn());
            //DataTable dtcln = new DataTable();
            //SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            //adpcln.Fill(dtcln);
            //if (dtcln.Rows.Count > 0)
            //{
            //    if (Convert.ToString(dtcln.Rows[0]["SqlconnurlIp"]) != "")
            //    {

            //        string scot = dtcln.Rows[0]["SqlconnurlIp"].ToString().Substring(0, 4);
            //        string scot1 = dtcln.Rows[0]["SqlconnurlIp"].ToString().Substring(0, 2);
            //        if (scot != "tcp:" || scot1 != "19")
            //        {
            //            dynconn.ConnectionString = @"Data Source=" + dtcln.Rows[0]["SqlconnurlIp"].ToString() + "," + dtcln.Rows[0]["SqlPort"].ToString() + ";Initial Catalog=" + dtcln.Rows[0]["Sqldatabasename"].ToString() + ";Persist Security Info=True;User ID=" + dtcln.Rows[0]["SqlUserId"].ToString() + ";Password=" + dtcln.Rows[0]["SqlPassword"].ToString() + "";

            //        }
            //        else
            //        {
            //            dynconn.ConnectionString = @"Data Source=" + dtcln.Rows[0]["SqlconnurlIp"].ToString() + "," + dtcln.Rows[0]["SqlPort"].ToString() + ";Initial Catalog=" + dtcln.Rows[0]["Sqldatabasename"].ToString() + ";Persist Security Info=True;User ID=" + dtcln.Rows[0]["SqlUserId"].ToString() + ";Password=" + dtcln.Rows[0]["SqlPassword"].ToString() + "";

            //        }
            //    }
            //    else
            //    {
            //        //dynconn.ConnectionString = @"Data Source=tcp:192.168.6.49,2800;Initial Catalog=ePlaza3-8-2012;Integrated Security=True";
            //        dynconn.ConnectionString = @"Data Source=tcp:192.168.1.219,2800;Initial Catalog=ePlaza;Integrated Security=True";

            //    }
            //}
            //else
            //{
            //   // dynconn.ConnectionString = @"Data Source=tcp:192.168.6.49,2800;Initial Catalog=ePlaza3-8-2012;Integrated Security=True";
            //    dynconn.ConnectionString = @"Data Source=tcp:192.168.1.219,2810;Initial Catalog=ePlaza;Integrated Security=True";

            //}
            fillaccess();
        }
    }
    protected void fillaccess()
    {
        DataTable dts = select("Select EmailApproval.* from  EmailApproval  Where ControlNo='" + Request.QueryString["cn"] + "'");

        Session["EmployeeId"] = dts.Rows[0]["UserId"];
       
        DocumentCls1 clsDocument = new DocumentCls1();

        DataTable dte = select("Select distinct RuleDetail.RuleDetailId from RuleDetail inner join RuleProcessMaster on RuleDetail.RuleDetailId=RuleProcessMaster.RuleDetailId where RuleDetail.RuleDetailId='" + Convert.ToInt32(Request.QueryString["rdt"]) + "' and RuleProcessMaster.DocumentId='" + dts.Rows[0]["DocumentId"] + "' and RuleProcessMaster.EmployeeId='" + Session["EmployeeId"] + "' ");
        if (dte.Rows.Count <= 0)
        {
            lblmsg.Text = "Your Document approval has been successfully recorded.";
            if (Request.QueryString["cn"] != null && Request.QueryString["ap"] != "ync")
            {

                bool success = clsDocument.InsertRuleProcess(Convert.ToInt32(dts.Rows[0]["DocumentId"]), Convert.ToInt32(Request.QueryString["rdt"]), "Email Approval", Convert.ToBoolean(true));
                success = true;
                string str = "Update EmailApproval Set AnswerReceived='True', DatetimeInventorySend='" + DateTime.Now.ToString() + "' where ControlNo='" + Request.QueryString["cn"] + "'";
                SqlCommand cmd = new SqlCommand(str, dynconn);
                if (dynconn.State.ToString() != "Open")
                {
                    dynconn.Open();
                }
                cmd.ExecuteNonQuery();
                dynconn.Close();
                sendmail(dts);


            }
            else if (Request.QueryString["ap"] == "ync")
            {
                bool success = clsDocument.InsertRuleProcess(Convert.ToInt32(dts.Rows[0]["DocumentId"]), Convert.ToInt32(Request.QueryString["rdt"]), "Email Rejected", Convert.ToBoolean(false));
                success = true;
                string str = "Update EmailApproval Set AnswerReceived='True',ApprovalReject='True', DatetimeInventorySend='" + DateTime.Now.ToString() + "' where ControlNo='" + Request.QueryString["cn"] + "'";
                SqlCommand cmd = new SqlCommand(str, dynconn);
                if (dynconn.State.ToString() != "Open")
                {
                    dynconn.Open();
                }
                cmd.ExecuteNonQuery();
                dynconn.Close();
                sendmail(dts);
            }

        }
        else
        {
            lblmsg.Text = "This Document already approved before";
        }

        string str1 = " Select Distinct CompanyMaster.CompanyName, EmployeeMaster.EmployeeName, OutGoingMailServer,WebMasterEmail, EmailMasterLoginPassword, AdminEmail, CompanyMaster.CompanyLogo,WarehouseMaster.Name as Wname,CityMasterTbl.CityName,StateMasterTbl.Statename,CountryMaster.CountryName from EmployeeMaster inner join WarehouseMaster on WarehouseMaster.WarehouseId=EmployeeMaster.Whid" +
        " inner join CompanyWebsitMaster on  CompanyWebsitMaster.Whid= WarehouseMaster.WarehouseId inner join " +
        " CompanyMaster on CompanyMaster.CompanyId=CompanyWebsitMaster.CompanyId inner join CompanyAddressMaster" +
        " on CompanyAddressMaster.CompanyMasterId=CompanyMaster.CompanyId inner join CountryMaster on " +
         "CountryMaster.CountryId=CompanyAddressMaster.Country inner join StateMasterTbl on " +
         "StateMasterTbl.StateId=CompanyAddressMaster.State inner join CityMasterTbl on " +
         "CityMasterTbl.CityId=CompanyAddressMaster.City where CompanyWebsitMaster.Whid='" + dts.Rows[0]["Whid"] + "' and EmployeeMaster.EmployeeMasterId='" + dts.Rows[0]["UserId"] + "'";
        SqlCommand cmdsq = new SqlCommand(str1, dynconn);
        SqlDataAdapter adp = new SqlDataAdapter(cmdsq);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            Wname.Text = Convert.ToString(dt.Rows[0]["Wname"]);
            lblciti.Text = Convert.ToString(dt.Rows[0]["CityName"]);
            lblstate.Text = Convert.ToString(dt.Rows[0]["Statename"]);
            lblcountry.Text = Convert.ToString(dt.Rows[0]["CountryName"]);
            lblcname.Text = Convert.ToString(dt.Rows[0]["CompanyName"]);
            img1.ImageUrl = "~/ShoppingCart/images/" + Convert.ToString(dt.Rows[0]["CompanyLogo"]);
        }
        DataTable dtsaa = select("Select DocumentMaster.DocumentTitle, EmployeeMaster.EmployeeName,RuleProcessMaster.* from RuleMaster inner join RuleDetail on RuleDetail.RuleId=RuleMaster.RuleId inner join RuleProcessMaster on RuleProcessMaster.RuleDetailId=RuleDetail.RuleDetailId inner join EmployeeMaster on EmployeeMaster.EmployeeMasterId=  RuleProcessMaster.EmployeeId inner join DocumentMaster on DocumentMaster.DocumentId=RuleProcessMaster.DocumentId  where RuleProcessMaster.DocumentId='" + dts.Rows[0]["DocumentId"] + "' and RuleProcessMaster.EmployeeId='" + dts.Rows[0]["UserId"] + "'");
        if (dtsaa.Rows.Count > 0)
        {
            
            lblDocId.Text = Convert.ToString(dts.Rows[0]["DocumentId"]);
            lbldoctitle.Text = Convert.ToString(dtsaa.Rows[0]["DocumentTitle"]);
            lblemployeename.Text = Convert.ToString(dtsaa.Rows[0]["EmployeeName"]);
            lblapprovaldatetime.Text = Convert.ToString(dtsaa.Rows[0]["RuleProcessDate"]);
        }
    }
    
    protected DataTable select(string std)
    {
        SqlDataAdapter cidco = new SqlDataAdapter(std, dynconn);
        DataTable dts = new DataTable();
        cidco.Fill(dts);
        return dts;


    }
    public void sendmail(DataTable dtsc)
    {
        DataTable dte = select("Select distinct RuleDetail.RuleApproveTypeId, RuleMaster.RuleId, RuleDetail.EmployeeId,ConditionTypeId, EmployeeMaster.Email, RuleApproveTypeMaster.RuleApproveTypeName, RuleMaster.RuleDate,StepId,EmployeeMaster.EmployeeName, RuleMaster.RuleTitle,RuleDetail.RuleDetailId from RuleMaster inner join " +
                  "RuleDetail on RuleDetail.RuleId=RuleMaster.RuleId inner join EmployeeMaster on  EmployeeMaster.EmployeeMasterId= " +
                   " RuleDetail.EmployeeId inner join RuleApproveTypeMaster on RuleApproveTypeMaster.RuleApproveTypeId=RuleDetail.RuleApproveTypeId where RuleDetail.RuleDetailId not in(Select RuleProcessMaster.RuleDetailId from RuleProcessMaster inner join  RuleDetail on RuleDetail.RuleDetailId=RuleProcessMaster.RuleDetailId where RuleDetail.RuleId= '" + dtsc.Rows[0]["RuleId"] + "' and DocumentId=  '" + dtsc.Rows[0]["DocumentId"] + "') and RuleDetail.RuleId= '" + dtsc.Rows[0]["RuleId"] + "' order by RuleDetailId ASC");
        if (dte.Rows.Count > 0)
        {
            if (Convert.ToInt32(dte.Rows[0]["ConditionTypeId"]) == 1)
            {

                string str = "Select Distinct logourl,MasterEmailId,  CompanyMaster.CompanyName, EmployeeMaster.EmployeeName, OutGoingMailServer,WebMasterEmail, EmailMasterLoginPassword, AdminEmail, CompanyMaster.CompanyLogo,WarehouseMaster.Name as Wname,CityMasterTbl.CityName,StateMasterTbl.Statename,CountryMaster.CountryName from EmployeeMaster inner join WarehouseMaster on WarehouseMaster.WarehouseId=EmployeeMaster.Whid" +
         " inner join CompanyWebsitMaster on  CompanyWebsitMaster.Whid= WarehouseMaster.WarehouseId inner join " +
         " CompanyMaster on CompanyMaster.CompanyId=CompanyWebsitMaster.CompanyId inner join CompanyAddressMaster" +
         " on CompanyAddressMaster.CompanyMasterId=CompanyMaster.CompanyId inner join CountryMaster on " +
          "CountryMaster.CountryId=CompanyAddressMaster.Country inner join StateMasterTbl on " +
          "StateMasterTbl.StateId=CompanyAddressMaster.State inner join CityMasterTbl on " +
          "CityMasterTbl.CityId=CompanyAddressMaster.City where CompanyWebsitMaster.Whid='" + dtsc.Rows[0]["Whid"] + "' and EmployeeMaster.EmployeeMasterId='" + dte.Rows[0]["EmployeeId"] + "'";
                SqlCommand cmd = new SqlCommand(str, dynconn);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    SqlCommand cmdxx = new SqlCommand();
                    cmdxx.CommandText = "InsertEmailApproval";
                    cmdxx.CommandType = CommandType.StoredProcedure;

                    cmdxx.Parameters.Add(new SqlParameter("@Whid", SqlDbType.Int));
                    cmdxx.Parameters["@Whid"].Value = dtsc.Rows[0]["Whid"];
                    cmdxx.Parameters.Add(new SqlParameter("@DocumentId", SqlDbType.Int));
                    cmdxx.Parameters["@DocumentId"].Value = dtsc.Rows[0]["DocumentId"];
                    cmdxx.Parameters.Add(new SqlParameter("@RuleId", SqlDbType.Int));
                    cmdxx.Parameters["@RuleId"].Value = dte.Rows[0]["RuleId"];
                    cmdxx.Parameters.Add(new SqlParameter("@UserId", SqlDbType.NVarChar));
                    cmdxx.Parameters["@UserId"].Value = dte.Rows[0]["EmployeeId"];
                    cmdxx.Parameters.Add(new SqlParameter("@EmailSend", SqlDbType.NVarChar));
                    cmdxx.Parameters["@EmailSend"].Value = true;
                    cmdxx.Parameters.Add(new SqlParameter("@AnswerReceived", SqlDbType.NVarChar));
                    cmdxx.Parameters["@AnswerReceived"].Value = false;
                    cmdxx.Parameters.Add(new SqlParameter("@ApprovalReject", SqlDbType.NVarChar));
                    cmdxx.Parameters["@ApprovalReject"].Value = false;

                    cmdxx.Parameters.Add(new SqlParameter("@DocApprovalType", SqlDbType.NVarChar));
                    cmdxx.Parameters["@DocApprovalType"].Value = dte.Rows[0]["RuleApproveTypeId"];
                    cmdxx.Parameters.Add(new SqlParameter("@DatetimeEmailSend", SqlDbType.NVarChar));
                    cmdxx.Parameters["@DatetimeEmailSend"].Value = DateTime.Now.ToString();
                    cmdxx.Parameters.Add(new SqlParameter("@DatetimeInventorySend", SqlDbType.NVarChar));
                    cmdxx.Parameters["@DatetimeInventorySend"].Value = DateTime.Now.ToString();
                    cmdxx.Parameters.Add(new SqlParameter("@ControlNo", SqlDbType.Int));
                    cmdxx.Parameters["@ControlNo"].Direction = ParameterDirection.Output;
                    cmdxx.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
                    cmdxx.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
                    Int32 result = DatabaseCls1.ExecuteNonQueryep(cmdxx);
                    result = Convert.ToInt32(cmdxx.Parameters["@ControlNo"].Value);

                    StringBuilder strhead = new StringBuilder();
                    strhead.Append("<table width=\"100%\"> ");
                    //strhead.Append("<tr><td align=\"left\"> <img src=" + Request.Url.Host.ToString() + "/ShoppingCart/images/" + Convert.ToString(dt.Rows[0]["CompanyLogo"]) + "\" \"border=\"0\" Width=\"176px\" Height=\"106px\" / > </td><td align=\"right\"><b><span style=\"color: #996600\">" + Convert.ToString(dt.Rows[0]["Wname"]) + "</span></b><BR>" + Convert.ToString(dt.Rows[0]["CityName"]) + "<Br>" + Convert.ToString(dt.Rows[0]["Statename"]) + "<Br>" + Convert.ToString(dt.Rows[0]["CountryName"]) + "</td></tr>  ");
                    strhead.Append("<tr><td Width=\"80%\" align=\"left\"> <img src=\"http://" + Request.Url.Host.ToString() + "/Shoppingcart/images/" + Convert.ToString(dt.Rows[0]["logourl"]) + "\" \"border=\"0\" Width=\"176px\" Height=\"106px\" / > </td><td align=\"left\"><b><span style=\"color: #996600\">" + Convert.ToString(dt.Rows[0]["Wname"]) + "</span></b><BR>" + Convert.ToString(dt.Rows[0]["CityName"]) + "<Br>" + Convert.ToString(dt.Rows[0]["Statename"]) + "<Br>" + Convert.ToString(dt.Rows[0]["CountryName"]) + "</td></tr>  ");
              
                    strhead.Append("<tr><td><br></td></tr>");
                    strhead.Append("<tr><td><b>Dear " + Convert.ToString(dt.Rows[0]["EmployeeName"]) + ",</b></td></tr>");
                    strhead.Append("<tr><td><br></td></tr>");
                    strhead.Append("<tr><td align=\"left\"><b> The following company has send you the following document for your approval kindly do need full in the matter.</b></td></tr>");
                    strhead.Append("<tr><td><table width=\"100%\">");
                    DataTable dteap = select("Select DocumentName, DocumentTitle,DocumentMainType.DocumentMainType+':'+DocumentSubType.DocumentSubType+':'+DocumentType.DocumentType as docmane from DocumentMaster inner join DocumentType on DocumentType.DocumentTypeId=DocumentMaster.DocumentTypeId " +
    " inner join DocumentSubType on DocumentSubType.DocumentSubTypeId=DocumentType.DocumentSubTypeId inner join " +
    " DocumentMainType on DocumentMainType.DocumentMainTypeId=DocumentSubType.DocumentMainTypeId Where DocumentId='" + dtsc.Rows[0]["DocumentId"] + "'");
                    if (dteap.Rows.Count > 0)
                    {
                        strhead.Append("<tr><td> Document Title :</td><td>" + dteap.Rows[0]["DocumentTitle"] + "</td></tr>");
                        strhead.Append("<tr><td> Cabinet-Drower-Folder :</td><td>" + dteap.Rows[0]["docmane"] + "</td></tr>");
                    }
                    strhead.Append("<tr><td> Document Approval Rule Type :</td><td>" + dte.Rows[0]["RuleApproveTypeName"] + "</td></tr>");
                    strhead.Append("<tr><td>  Document Approval Rule Name :</td><td>" + dte.Rows[0]["RuleTitle"] + "</td></tr>");
                    strhead.Append("</table></td></tr> ");
                    DataTable dt2 = select(" Select EmployeeMaster.EmployeeName, RuleDetail.RuleDetailId,DesignationMaster.DesignationName,DepartmentmasterMNC.Departmentname,RuleProcessDate from RuleMaster inner join RuleDetail on RuleDetail.RuleId=RuleMaster.RuleId " +
    " inner join RuleProcessMaster on RuleProcessMaster.RuleDetailId=RuleDetail.RuleDetailId" +
    " inner join EmployeeMaster on  EmployeeMaster.EmployeeMasterId=  RuleProcessMaster.EmployeeId inner join DesignationMaster" +
    " on DesignationMaster.DesignationMasterId=EmployeeMaster.DesignationMasterId inner join DepartmentmasterMNC on DepartmentmasterMNC.id=DesignationMaster.DeptID where RuleProcessMaster.DocumentId='" + dtsc.Rows[0]["DocumentId"] + "' and RuleMaster.RuleId='" + dtsc.Rows[0]["RuleId"] + "'");
                    int i = 0;
                    foreach (DataRow dx in dt2.Rows)
                    {
                        if (i == 0)
                        {
                            strhead.Append("<tr><td><table width=\"100% BorderColor=Black BorderStyle=Solid\">");
                            strhead.Append("<tr><td colsplan=\"4\"><b>This Document as already approved by </b> </td>");
                            strhead.Append("<tr><td align=left>Employee Name </td><td  align=left>Designation Name</td> <td  align=left>Department Name</td>  <td  align=left>Aproval DateTime</td></tr>");
                        }

                        strhead.Append("<tr><td align=left>" + dx["EmployeeName"] + " </td><td  align=left>" + dx["DesignationName"] + "</td> <td  align=left>" + dx["Departmentname"] + "</td>  <td  align=left>" + dx["RuleProcessDate"] + "</td></tr>");
                        i = i + 1;

                    }
                    if (i > 0)
                    {
                        strhead.Append("</table></td></tr> ");

                    }
                    strhead.Append("<tr><td><br></td></tr>");
                    strhead.Append("<tr><td align=\"left\"><b>if you want to Approve/Reject above document,you can do it from here bellow. </b></td></tr>");
                    strhead.Append("<tr><td align=\"left\"><b><a href=http://" + Request.Url.Host.ToString() + "/EmailDocApprove.aspx?cn=" + result + "&rdt=" + dte.Rows[0]["RuleDetailId"] + "&cid=" + Session["Comid"] + ">Approve</a></b></td></tr>");
                    strhead.Append("<tr><td align=\"left\"><b><a href=http://" + Request.Url.Host.ToString() + "/EmailDocApprove.aspx?ap=ync&cn=" + result + "&rdt=" + dte.Rows[0]["RuleDetailId"] + "&cid=" + Session["Comid"] + ">Reject</a></b></td></tr>");

                   
                    strhead.Append("<tr><td><br></td></tr>");
                    strhead.Append("<tr><td><b>Thanking You</b></td></tr>");
                    strhead.Append("<tr><td><b>Sincerely</b></td></tr>");
                    strhead.Append("<tr><td><br></td></tr>");
                    strhead.Append("<tr><td><b>For,</b></td></tr>");
                    strhead.Append("<tr><td><b> " + Convert.ToString(dt.Rows[0]["CompanyName"]) + "</b></td></tr>");
                    strhead.Append("</table> ");

                    string AdminEmail = "";
                    if (Convert.ToString(dt.Rows[0]["WebMasterEmail"]) != "")
                    {
                        AdminEmail = dt.Rows[0]["WebMasterEmail"].ToString();// TextAdminEmail.Text;
                    }
                    else
                    {
                        AdminEmail = dt.Rows[0]["MasterEmailId"].ToString();// TextAdminEmail.Text;
                    }
                    //string AdminEmail = txtusmail.Text;
                    String Password = dt.Rows[0]["EmailMasterLoginPassword"].ToString();// TextEmailMasterLoginPassword.Text;

                    //string body = "Test Mail Server - TestIwebshop";
                    MailAddress to = new MailAddress(dte.Rows[0]["Email"].ToString());
                  //  MailAddress to = new MailAddress("maheshsorathiya500@gmail.com");
                    MailAddress from = new MailAddress(AdminEmail);

                    MailMessage objEmail = new MailMessage(from, to);
                    objEmail.Subject = "Document Approved by " + Convert.ToString(dt.Rows[0]["EmployeeName"]);

                    // if (RadioButtonList1.SelectedValue == "0")
                    {
                        objEmail.Body = strhead.ToString();
                        objEmail.IsBodyHtml = true;

                    }


                    objEmail.Priority = MailPriority.High;
                    string path2 = Server.MapPath("~\\Account\\" + Session["comid"] + "\\UploadedDocuments\\" + dteap.Rows[0]["DocumentName"].ToString());

                    Attachment attachFile = new Attachment(path2);
                    objEmail.Attachments.Add(attachFile);

                    SmtpClient client = new SmtpClient();

                    client.Credentials = new NetworkCredential(AdminEmail, Password);
                    client.Host = dt.Rows[0]["OutGoingMailServer"].ToString();


                    client.Send(objEmail);



                }

            }

        }
    }
}
