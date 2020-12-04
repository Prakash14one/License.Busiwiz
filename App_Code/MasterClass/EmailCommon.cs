using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Data;
using DS.Win.BO;

/// <summary>
/// Summary description for EmailCommon
/// </summary>
public class EmailCommon
{

    public static string TO = string.Empty;
    public static string BCC = string.Empty;
    public static string CC = string.Empty;
    public static string UserName = string.Empty;
    public static string DomainPass = string.Empty;
    public static string EMailFrom = string.Empty;
    public static string MailServer = string.Empty;
    public static bool SSLEnable = true;
    public static string Subject = string.Empty;



    public static void initializeData()
    {
        DataTable dtGetData = BOGeneral.GetDataset("SELECT Key1,Key2,ParamValue FROM allparam WHERE Key2='Email'").Tables[0];
        if (dtGetData.Rows.Count > 0)
        {
            foreach (DataRow row in dtGetData.Rows)
            {
                switch (Convert.ToString(row["Key1"]).ToUpper())
                {
                    case "ETO":
                        TO = Convert.ToString(row["ParamValue"]);
                        break;
                    case "ECC":
                        CC = Convert.ToString(row["ParamValue"]);
                        break;
                    case "EBCC":
                        BCC = Convert.ToString(row["ParamValue"]);
                        break;
                    case "EUSERNAME":
                        UserName = Convert.ToString(row["ParamValue"]);
                        break;
                    case "EPASSWORD":
                        DomainPass = Convert.ToString(row["ParamValue"]);
                        break;
                    case "EFROM":
                        EMailFrom = Convert.ToString(row["ParamValue"]);
                        break;
                    case "ESSLENABLE":
                        SSLEnable = Convert.ToBoolean(Convert.ToString(row["ParamValue"]));
                        break;
                    case "EMAILSERVR":
                        MailServer = Convert.ToString(row["ParamValue"]);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public static bool SendEmailUsingDotNet(string To, string Subject, string BodyMessage, bool IsHTML, string From, string CC, string BCC, string LocalFilePathForAttachment)
    {
        string mailServerName = EmailCommon.MailServer;
        try
        {
            
            using (MailMessage message = new MailMessage(From, To, Subject, BodyMessage))
            {
                message.IsBodyHtml = true;

                if (CC != "")
                {
                    message.CC.Add(CC);
                }
                if (BCC != "")
                {
                    message.Bcc.Add(BCC);
                }
                if(To!="")
                {
                    message.To.Add(To);
                }
                if (LocalFilePathForAttachment.Trim() != "")
                {
                    Attachment data = new Attachment(LocalFilePathForAttachment);
                    //// Add time stamp information for the file.
                    //ContentDisposition disposition = data.ContentDisposition;
                    //disposition.CreationDate = System.IO.File.GetCreationTime(file);
                    //disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
                    //disposition.ReadDate = System.IO.File.GetLastAccessTime(file);
                    // Add the file attachment to this e-mail message.
                    message.Attachments.Add(data);
                }
                SmtpClient mailClient = new SmtpClient(mailServerName);
                //mailClient.Host = mailServerName;
                
                string UserName = EmailCommon.UserName;
                string DomainPassword = EmailCommon.DomainPass;
                
                mailClient.Credentials = new System.Net.NetworkCredential(UserName, DomainPassword);
                //mailClient.UseDefaultCredentials = true;
                mailClient.EnableSsl = EmailCommon.SSLEnable;
                mailClient.Send(message);
                return true;

            }

        }
        catch (Exception ex)
        {
            return false;
            //throw ex;
        }
    }
	public EmailCommon()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}