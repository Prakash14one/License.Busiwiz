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
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
// developed by dineshbabu-->db
// this page is approval page for companyaboutusinput.aspx act as admin for approval
public partial class ShoppingCart_Admin_CompanySiteAboutUsViewforApproval : System.Web.UI.Page
{
   
    SqlConnection con;
    string compid = "";

    protected void Page_Load(object sender, EventArgs e)
    {
         //Making textbox unable to edit
        TextBox1.Attributes.Add("readonly", "readonly");
        TextBox2.Attributes.Add("readonly", "readonly");
//------------------------------------------------------
        PageConn pgcon = new PageConn();
        PageConn.strEnc = "3d70f5cff23ed17ip8H9";
        con = pgcon.dynconn;
        if (!IsPostBack)
        {
            if (Request.QueryString["id1"] != null)
            {
                string id = Request.QueryString["id1"].ToString().Replace(" ", "+");
                string idcomp = ClsEncDesc.Decrypted(id.ToString());
                ViewState["id2"] = idcomp.ToString();
                          
                SqlCommand cmd = new SqlCommand("select * from  CompanyAboutUsInputTBL where ID ='" + ViewState["id2"] + "' ", con);
                                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {

                    ViewState["compid"] = dt.Rows[0]["CompanyId"].ToString();

                   // Existingpage();// code for displaying existing company details on textbox1
                    //Proposedpage();// code for displaying existing company details on textbox1
                }
                //con.Close();
                Existingpage();
                Proposedpage();
            }


           


        }
    }
    protected void Proposedpage()
    {
        con.Open();
        //Image2.ImageUrl = "";
        Image2.Width = 225;
        Image2.Height = 150;
        SqlCommand cmd = new SqlCommand("select * from  CompanyAboutUsInputTBL where Companyid ='" + ViewState["compid"] + "' and Status='Unapproved'", con);
        //SqlCommand cmd = new SqlCommand("select top(1)* from  CompanyAboutUsInputTBL where  Status='Unapproved' ", con);
         SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        if (dt.Rows.Count > 0)
        {

           // Image2.ImageUrl = @"~\\ShoppingCart\\images\\"+dt.Rows[0]["Aboutusimage"].ToString();
            Image2.ImageUrl = "http://members.ijobcenter.com/ShoppingCart/images/" + dt.Rows[0]["Aboutusimage"].ToString();
            
            TextBox2.Text = dt.Rows[0]["Aboutus"].ToString();
           
        }
        con.Close();
    }
    protected void Existingpage()
    {
        con.Open();
     //Image1.ImageUrl = "";
        Image1.Width = 225;
        Image1.Height = 150;

        SqlCommand cmd = new SqlCommand("select * from  CompanyAboutUsInputTBL where Companyid ='" + ViewState["compid"] + "' and Status='Approved'", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt1 = new DataTable();
        sda.Fill(dt1);
        if (dt1.Rows.Count > 0)
        {
            Image1.ImageUrl = "http://members.ijobcenter.com/ShoppingCart/images/" + dt1.Rows[0]["Aboutusimage"].ToString();
             //Image1.ImageUrl = @"~\\ShoppingCart\\images\\" + dt1.Rows[0]["Aboutusimage"].ToString();
            TextBox1.Text = dt1.Rows[0]["Aboutus"].ToString();
          
                             
        }
        con.Close();
    }
    protected void btnreject(object sender, EventArgs e)
    {
        popup.Visible = true;
        ModalPopupExtender1.Show();

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        popup.Visible = true;
        ModalPopupExtender1.Show();
    }
    protected void Button3_Click(object sender, EventArgs e) // submit 
    {
       Sendmail2();
        rejectedstatus();
    }
    //public void Companyid()
    //{
    //    con.Open();
    //    SqlCommand cmd = new SqlCommand("select Companyid from CompanyAboutUSInputTBL where ID ='" + ViewState["id"] + "'", con);

    //}
    public void Sendmail2()// admin to comp
    {
       
        try
        {
            //string str2 = "select Party_master.id,Email,CompanyAboutUSInputTBL.Companyid,CompanyMaster.CompanyName  from CompanyMaster inner join Party_master on Party_master.id=CompanyMaster.Compid inner join CompanyAboutUsInputTBL on CompanyAboutUsInputTBL.Companyid=CompanyMaster.Compid where CompanyAboutUsInputTBL.Companyid='" + ViewState["id"] + "'";
            string str2 = "select Party_master.id,Email,CompanyAboutUSInputTBL.Companyid,CompanyMaster.CompanyName  from CompanyMaster inner join Party_master on Party_master.id=CompanyMaster.Compid inner join CompanyAboutUsInputTBL on CompanyAboutUsInputTBL.Companyid=CompanyMaster.Compid where Party_master.id=compid ";
            //string str2 = "select CompanyMaster.CompanyName,Email,VacancyPositionTitle from CompanyMaster inner join Party_master on  Party_master.id=CompanyMaster.Compid inner join VacancyPositionTitleMaster on VacancyPositionTitleMaster.ID=VacancyMasterTbl.vacancypositiontitleid where VacancyMasterTbl.ID='" + Session["id"] + "'";
            SqlCommand cmd1 = new SqlCommand(str2, con);
            SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
            DataTable dt11 = new DataTable();
            adp1.Fill(dt11);

            //// string str3 = "  select comid,VacancyTypeMaster.Name,VacancyPositionTitleMaster.VacancyPositionTitle,noofvacancy,vacancyduration,QualificationRequirements from VacancyMasterTbl inner join VacancyTypeMaster on VacancyTypeMaster.ID =VacancyMasterTbl.vacancypositiontypeid  inner join VacancyPositionTitleMaster on VacancyPositionTitleMaster.ID=vacancypositiontitleid inner join VacancyDetailTbl on VacancyMasterTbl.ID=VacancyDetailTbl.vacancymasterid where VacancyMasterTbl.ID=" + Session["id"] + "";
            // SqlCommand cmd3 = new SqlCommand(str3, con);
            // SqlDataAdapter adp = new SqlDataAdapter(cmd3);
            // DataTable dt35 = new DataTable();
            // adp.Fill(dt35);


            string str21 = "  select distinct  PortalMasterTbl.* from  CompanyMaster inner join PricePlanMaster  on CompanyMaster.PricePlanId=PricePlanMaster.PricePlanId inner join VersionInfoMaster on VersionInfoMaster.VersionInfoId=PricePlanMaster.VersionInfoMasterId inner join ProductMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId inner join ClientMaster on ProductMaster.ClientMasterId=ClientMaster.ClientMasterId inner join Priceplancategory on Priceplancategory.ID=PricePlanMaster.PriceplancatId inner join PortalMasterTbl on PortalMasterTbl.Id=Priceplancategory.PortalId   inner join OrderMaster on CompanyMaster.CompanyLoginId=OrderMaster.CompanyLoginId inner join  OrderPaymentSatus on OrderMaster.OrderId=OrderPaymentSatus.OrderId inner join StateMasterTbl on StateMasterTbl.StateId=PortalMasterTbl.StateId inner join CountryMaster on StateMasterTbl.CountryId=CountryMaster.CountryId  WHERE(PortalMasterTbl.Id=7)  ";
            SqlCommand cmd45 = new SqlCommand(str21, PageConn.licenseconn());
            SqlDataAdapter adp45 = new SqlDataAdapter(cmd45);
            DataTable dt21 = new DataTable();
            adp45.Fill(dt21);

            string aa = "";
            string bb = "";
            string cc = "";
            string ff = "";
            string ee = "";
            string dd = "";
            string ext = "";
            string tollfree = "";
            string tollfreeext = "";
            if (dt21.Rows.Count > 0)
            {

                if (Convert.ToString(dt21.Rows[0]["Supportteamphonenoext"].ToString()) != "" && Convert.ToString(dt21.Rows[0]["Supportteamphonenoext"].ToString()) != null)
                {
                    ext = "ext " + dt21.Rows[0]["Supportteamphonenoext"].ToString();
                }

                if (Convert.ToString(dt21.Rows[0]["Tollfree"].ToString()) != "" && Convert.ToString(dt21.Rows[0]["Tollfree"].ToString()) != null)
                {
                    tollfree = dt21.Rows[0]["Tollfree"].ToString();
                }

                if (Convert.ToString(dt21.Rows[0]["Tollfree"].ToString()) != "" && Convert.ToString(dt21.Rows[0]["Tollfree"].ToString()) != null)
                {
                    tollfreeext = "ext " + dt21.Rows[0]["Tollfreeext"].ToString();
                }


                aa = "" + dt21.Rows[0]["Supportteammanagername"].ToString() + "- Support Manager";
                bb = "" + dt21.Rows[0]["PortalName"].ToString() + " ";
                cc = "" + dt21.Rows[0]["Supportteamphoneno"].ToString() + "  " + ext + " ";
                dd = "" + tollfree + " " + tollfreeext + " ";
                ee = "" + dt21.Rows[0]["Portalmarketingwebsitename"].ToString() + "";
                // ff = "" + dt21.Rows[0]["City"].ToString() + " " + dt21.Rows[0]["StateName"].ToString() + " " + dt21.Rows[0]["CountryName"].ToString() + " " + dt21.Rows[0]["Zip"].ToString() + " ";
            }
            string tomail = dt11.Rows[0]["Email"].ToString();
            if (dt21.Rows.Count > 0)
            {
                string file = "job-center-logo.jpg";
                //string body1 =   "Dear " + dt11.Rows[0]["CompanyName"].ToString() + " <br><br> The altrations you are requestedto be made to our company's ijobcenter.com " + " website About Us have been rejected for following reasons: <br>" 
                //   +TextBox3.Text+ " <br> <br>" + "Please make the appropraite changes to the alterations you are requesting and submit the request again.<br><br> " + "Thank you,<br><br>" ;

                string email = Convert.ToString(dt21.Rows[0]["UserIdtosendmail"]);
                string displayname = Convert.ToString("IJobCenter");
                string password = Convert.ToString(dt21.Rows[0]["Password"]);
                string outgo = Convert.ToString(dt21.Rows[0]["Mailserverurl"]);
                string body = "Dear " + dt11.Rows[0]["CompanyName"].ToString() + " <br><br> The altrations you are requested to be made to our company's ijobcenter.com <br/>" + " website have been rejected for following reasons: <br/><br/>"
                   + TextBox3.Text + " <br> <br><br> <br>" + "Please make the appropraite changes to the alterations you are requesting and submit the request again.<br><br> " + "Thank you,<br><br>"; //body1;
                string Subject = "About Us Rejected";


                MailAddress to = new MailAddress(tomail);//info@ijobcenter.com("company12@safestmail.net");//
                MailAddress from = new MailAddress(email, displayname);
                MailMessage objEmail = new MailMessage(from, to);
                objEmail.Subject = Subject.ToString();
                objEmail.Body = body.ToString();


                //string path = "http://members.ijobcenter.com/Account/jobcenter/UploadedDocuments/"+dt15.Rows[0][0].ToString()+"";
                //System.Net.Mail.Attachment attachment;
                //attachment = new System.Net.Mail.Attachment(path);
                //objEmail.Attachments.Add(attachment);

                objEmail.IsBodyHtml = true;
                objEmail.Priority = MailPriority.High;
                SmtpClient client = new SmtpClient();
                client.Credentials = new NetworkCredential(email, password);
                client.Host = outgo;
                client.Send(objEmail);
            }
        }
        catch
        {

        }

    }

    public void rejectedstatus()
    {
        con.Open();
       
        SqlCommand cmd = new SqlCommand("update CompanyAboutUsInputTBL set status='Rejected' where ID='" + ViewState["id2"] + "' and Status='Unapproved' ", con);
        cmd.ExecuteNonQuery();
        con.Close();
    }
    public void approvedstatus()
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("update CompanyAboutUsInputTBL set status='Approved' where ID='"+ViewState["id2"]+"' and status='Unapproved'", con);
        cmd.ExecuteNonQuery();
        con.Close();
    }
    public void delstatus()
    {
        con.Open();
        SqlCommand cmd1 = new SqlCommand("select * from  CompanyAboutUsInputTBL  where Status='Approved' and Companyid ='"+ ViewState["compid"]+"'", con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd1);
        DataTable dt3 = new DataTable();
        sda.Fill(dt3);
        if (dt3.Rows.Count > 1)
        {
            SqlCommand cmd2 = new SqlCommand("select MIN(ID) from CompanyAboutUsInputTBL  where Status='Approved' and Companyid ='" + ViewState["compid"] + "'", con);
            SqlDataAdapter sda22 = new SqlDataAdapter(cmd2);
            DataTable dt22 = new DataTable();
            sda22.Fill(dt22);
            if (dt22.Rows.Count > 0)
            {

                SqlCommand cmd = new SqlCommand("delete  from CompanyAboutUsInputTBL  where Companyid ='" + ViewState["compid"] + "' and ID='" + dt22.Rows[0][0].ToString() + "' and status='Approved'", con);
                SqlDataAdapter sda1 = new SqlDataAdapter(cmd);
                cmd.ExecuteNonQuery();
            }
        }
        con.Close();
        //{
        //    con.Open();
        //    SqlCommand cmd = new SqlCommand("delete from CompanyAboutUsInputTBL  where Companyid ='"+ ViewState["compid"]+"' and ID='" + ViewState["minid"] + "' and status='Approved'", con);
        //    cmd.ExecuteNonQuery();
        //    con.Close();
        //}
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        popup.Visible = false;
        TextBox3.Text = "";
    }
   
    protected void Approve(object sender, EventArgs e)
    {
        approvedstatus();
        delstatus();
       
    }
}