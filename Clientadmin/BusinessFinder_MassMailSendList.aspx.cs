using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Text;

public partial class Admin_BusinessFinder : System.Web.UI.Page
{
    //SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conn21"].ToString());
        
    //SqlConnection jobcenterconn = new SqlConnection(ConfigurationManager.ConnectionStrings["jobconn1"].ToString());
    //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);

    SqlConnection con = new SqlConnection();
    SqlConnection connection = new SqlConnection();
    SqlConnection jobcenterconn = new SqlConnection();
    SqlConnection Livelocal = new SqlConnection();
    string mail = "";
    string Notsendmailmail = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        con = PageConn.licenseconn();
        //Livelocal = PageConn.livelocal();
        //connection = PageConn.livelocal();
        PageConn pgcon = new PageConn();
        jobcenterconn = pgcon.dynconn;
        lblVersion.Text = "This PageVersion Is V1  Date:27-Oct-2015 Develop By @Pk";
        if (!IsPostBack)
        {
            FillMailType();
            fillgrid();
           
        }
       // fillgrid();
    }


    protected void FillMailType()
    {
        ddlMailType.Items.Clear();
        DataTable dtv = new DataTable();
       
            SqlDataAdapter dav = new SqlDataAdapter("select StandardMaiLFOrmatTYpeID as EmailTypeId ,Name from StandardMailFormatType where Active=1", jobcenterconn);
            dav.Fill(dtv);
       
        if (dtv.Rows.Count > 0)
        {
            ddlMailType.DataSource = dtv;
            ddlMailType.DataTextField = "Name";
            ddlMailType.DataValueField = "EmailTypeId";
            ddlMailType.DataBind();
        }
        ddlMailType.Items.Insert(0, "-Select-");
        ddlMailType.Items[0].Value = "0";
    }
    public void fillgrid()
    {
        string str1 = "";
        string st = "";
        if (ddlMailType.SelectedIndex > 0)
        {
            
            st +=  " and  dbo.MassemailMaster.Mail_message_FormatName ='"+ ddlMailType.SelectedItem.Text  +"'";
        }
        if (TextBox1.Text == "" || TextBox2.Text == "")
        {

        }
        else
        {
            st += " and  dbo.MassemailMaster.date between '" + TextBox1.Text + "' and '" + TextBox2.Text + "'";
        }
        if (txt_business.Text != "")
        {
            st += "and dbo.MassEmailDetail.BusinessName like '%" + txt_business.Text.Replace("'", "''") + "%'";
        }
       
                string str = "SELECT        dbo.MassemailMaster.MassemailMasterID, dbo.MassemailMaster.Portal_Id, dbo.MassemailMaster.Email_ID, dbo.MassemailMaster.MasterEmailSentSubject, dbo.MassemailMaster.AttachmentURL, dbo.MassemailMaster.Message, dbo.MassemailMaster.date, dbo.MassemailMaster.Mail_message_FormatName, dbo.MassEmailDetail.BusinessEmailID, dbo.MassEmailDetail.BusinessName " +
                             "       FROM            dbo.MassEmailDetail INNER JOIN dbo.MassemailMaster ON dbo.MassEmailDetail.MassemailMasterID = dbo.MassemailMaster.MassemailMasterID where dbo.MassemailMaster.MassemailMasterID !=1  " + st + "";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter da = new SqlDataAdapter(str, con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "newpro")
        {
            int mkl1 = Convert.ToInt32(e.CommandArgument);
            string te = "BusinessProfile.aspx?id=" + mkl1;
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate) && (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header))
        {
            e.Row.Cells[0].Visible = false;
            //e.Row.Cells[7].Visible = false;
            //e.Row.Cells[6].Visible = false;

        }
    }
   
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        mainfun();
    }

    public void mainfun()
    {
        connection.Close();
        connection.Open();
        fillgrid();
       /*
        string str1 = "";
        
        string str = @"SELECT top 40 EmployeeDataEntry.businessdataId, CountryMaster.CountryName, StateMaster.StateName, CityMaster.CityName, EmployeeDataEntry.BusinessName,  EmployeeDataEntry.Phone, EmployeeDataEntry.mobile, EmployeeDataEntry.Email, EmployeeDataEntry.BusinessDetails, Forum.ForumOrder FROM [livelocalservice.com].[dbo].CountryMaster INNER JOIN  [livelocalservice.com].[dbo].EmployeeDataEntry ON CountryMaster.ID = EmployeeDataEntry.Country INNER JOIN [livelocalservice.com].[dbo].StateMaster ON EmployeeDataEntry.State = StateMaster.ID INNER JOIN  [livelocalservice.com].[dbo].CityMaster ON EmployeeDataEntry.City = CityMaster.ID INNER JOIN [livelocalservice.com].[dbo]. Forum ON EmployeeDataEntry.Category = Forum.ForumOrderID where  EmployeeDataEntry.Active='yes' " + str1 + " ";
        SqlCommand command1 = new SqlCommand(str, connection);
        SqlDataAdapter clientadp = new SqlDataAdapter();
        clientadp.SelectCommand = command1;
        DataSet dataset = new DataSet();
        clientadp.Fill(dataset, "EmployeeDataEntry");
        GridView1.DataSource = dataset.Tables["EmployeeDataEntry"].DefaultView;
        GridView1.DataBind();*/
        connection.Close();

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string str = string.Empty;
        string strname = string.Empty;
        string email="", name="", country="", state="", city="";
        DataTable dt = new DataTable("productinfo");
        dt.Columns.Add("BusinessEmailID", typeof(string));
        dt.Columns.Add("BusinessName", typeof(string));
        dt.Columns.Add("Country", typeof(string));
        dt.Columns.Add("State", typeof(string));
        dt.Columns.Add("City", typeof(string));
        foreach (GridViewRow gvrow in GridView1.Rows)
        {
            LinkButton lk = (LinkButton)gvrow.FindControl("LinkButton2");
            CheckBox chk = (CheckBox)gvrow.FindControl("chkSelect");
            if (chk != null & chk.Checked)
            {
              email= gvrow.Cells[6].Text;
              name= lk.Text ;
              country= gvrow.Cells[1].Text ;
              state= gvrow.Cells[2].Text ;
              city= gvrow.Cells[3].Text;
              dt.Rows.Add(email, name, country, state, city);
            }
          
        }
        Session["BusinessFinder"] = dt;
        Response.Redirect("MassEmails.aspx");
    }

public void SendMail()
    {
        try
        {
            string str = "select [PortalMasterTbl].[EmailId],PortalMasterTbl.EmailDisplayname,PortalMasterTbl.Password,PortalMasterTbl.Mailserverurl from [PortalMasterTbl] inner join [CompanyMaster] on [PortalMasterTbl].[ProductId]=[CompanyMaster].[ProductId]  where dbo.PortalMasterTbl.Id=7 ";

            SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                string email = Convert.ToString(dt.Rows[0]["EmailId"]);
                string displayname = Convert.ToString(dt.Rows[0]["EmailDisplayname"]);
                string password = Convert.ToString(dt.Rows[0]["Password"]);
                string outgo = Convert.ToString(dt.Rows[0]["Mailserverurl"]);
                string body =Convert.ToString(Session["lbl_Message"]);
                string Subject = Convert.ToString(Session["lbl_Master_EmailSentSubject"]);;

                MailAddress to = new MailAddress(mail);
                MailAddress from = new MailAddress(email, "busiwiz.com");
                MailMessage objEmail = new MailMessage(from, to);
                objEmail.Subject = Subject.ToString();
                objEmail.Body = body;
                objEmail.IsBodyHtml = true;
                objEmail.Priority = MailPriority.High;
                SmtpClient client = new SmtpClient();
                client.Credentials = new NetworkCredential(email, password);
                client.Host = outgo;
                client.Send(objEmail);
            }
            else
            {
                // lblmsg.Text = "No Portal Email Available.";
            }
        }
        catch (Exception ex)
        {
            Notsendmailmail += mail;
        }
       


    }
protected void Button1_Click1(object sender, EventArgs e)
{
    Response.Redirect("BusinessFinder.aspx");
}
protected void Button2_Click11(object sender, EventArgs e)
{
    fillgrid(); 
}
    protected void Button2_Click(object sender, EventArgs e)
{
    string str1 = "";
    con.Close();
    con.Open();
    string str = " SELECT DISTINCT COUNT(dbo.MassEmailDetail.BusinessEmailID) AS Expr1, dbo.MassEmailDetail.BusinessEmailID FROM dbo.MassEmailDetail INNER JOIN  dbo.MassemailMaster ON dbo.MassEmailDetail.MassemailMasterID = dbo.MassemailMaster.MassemailMasterID GROUP BY dbo.MassEmailDetail.BusinessEmailID ";
    str = "SELECT DISTINCT COUNT(dbo.MassEmailDetail.BusinessEmailID) AS Expr1, dbo.MassEmailDetail.BusinessEmailID, dbo.MassemailMaster.date, dbo.MassemailMaster.Message, dbo.MassemailMaster.AttachmentURL, dbo.MassemailMaster.MasterEmailSentSubject, dbo.MassemailMaster.Mail_message_FormatName FROM            dbo.MassEmailDetail INNER JOIN    dbo.MassemailMaster ON dbo.MassEmailDetail.MassemailMasterID = dbo.MassemailMaster.MassemailMasterID GROUP BY dbo.MassEmailDetail.BusinessEmailID, dbo.MassemailMaster.date, dbo.MassemailMaster.Message, dbo.MassemailMaster.AttachmentURL, dbo.MassemailMaster.MasterEmailSentSubject, dbo.MassemailMaster.Mail_message_FormatName";
    SqlCommand cmd = new SqlCommand(str, con);
    SqlDataAdapter da = new SqlDataAdapter(str, con);
    DataTable dt = new DataTable();
    da.Fill(dt);
    GridView2.DataSource = dt;
    GridView2.DataBind();  
  
    con.Close();
    con.Open();
    foreach (GridViewRow GR in GridView1.Rows)
    {
        CheckBox chk = (CheckBox)GR.FindControl("chkSelect");
        Label BusinessEmailID = (Label)GR.FindControl("lbl_email");
        Label lbl_Message = (Label)GR.FindControl("lbl_Message");
        Label lbl_Master_EmailSentSubject = (Label)GR.FindControl("lbl_Master_EmailSentSubject");
        if (chk.Checked == true)
        {
            Session["lbl_Message"] = lbl_Message.Text;
            Session["lbl_Master_EmailSentSubject"] = lbl_Master_EmailSentSubject.Text;
            mail = BusinessEmailID.Text;
            SendMail();
            con.Close();
            con.Open();
        }
       
    }

    lbl_msg.Text = "Mail Send Successfully"; 

}
protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
{
    fillgrid(); 
}
protected void ddlMailTypeselectedindex(object sender, EventArgs e)
{
    fillgrid(); 
}
}