﻿using System;
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
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Text.RegularExpressions;
using System.Drawing;
public partial class ShoppingCart_Admin_CompanySiteProductsandServicesInput : System.Web.UI.Page
{
    SqlConnection con;
    protected void Page_Load(object sender, EventArgs e)
    {
       // Session["Comid"] = "herrykem12";
        lblVersion.Text = "This version 2 is updated by Mejila on 24/2/2016";
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        if (!IsPostBack)
        {
            string str = " select * from CompanyMaster where Compid='" + Session["Comid"].ToString() + "'";
            SqlCommand cmd = new SqlCommand(str,con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                ViewState["COMPANY"] = dt.Rows[0]["CompanyName"].ToString();
            }
            Panel2.Visible = true;
            fillpage();

            Button1.Visible = true;
            TextBox1.Attributes.Add("onkeypress", "return tbLimit();");
            TextBox1.Attributes.Add("onkeyup", "return tbCount(" + Label1.ClientID + ");");
            TextBox1.Attributes.Add("maxLength", "40");

            TextBox2.Attributes.Add("onkeypress", "return tbLimit();");
            TextBox2.Attributes.Add("onkeyup", "return tbCount(" + Label2.ClientID + ");");
            TextBox2.Attributes.Add("maxLength", "40");
            TextBox3.Attributes.Add("onkeypress", "return tbLimit();");
            TextBox3.Attributes.Add("onkeyup", "return tbCount(" + Label3.ClientID + ");");
            TextBox3.Attributes.Add("maxLength", "40");
            TextBox4.Attributes.Add("onkeypress", "return tbLimit();");
            TextBox4.Attributes.Add("onkeyup", "return tbCount(" + Label4.ClientID + ");");
            TextBox4.Attributes.Add("maxLength", "40");
        }
    }
    protected void edit1_Click(object sender, EventArgs e)
    {
        Button3.Visible = true;
        FileUpload1.Visible = true;
        edit1.Visible = false;
        submit1.Visible = true;
        Button1.Visible = true;
        
    }
    protected void edit2_Click(object sender, EventArgs e)
    {
        Button5.Visible = true;
        FileUpload2.Visible = true;
        edit2.Visible = false;
        submit2.Visible = true;
        Button1.Visible = true;
    }
    protected void edit3_Click(object sender, EventArgs e)
    {
        Button7.Visible = true;
        FileUpload3.Visible = true;
        edit3.Visible = false;
        submit3.Visible = true;
        Button1.Visible = true;
    }
    protected void edit4_Click(object sender, EventArgs e)
    {
        Button8.Visible = true;
        FileUpload4.Visible = true;
        edit4.Visible = false;
        submit4.Visible = true;
        Button1.Visible = true;
        
    }
    protected void edit11_Click(object sender, EventArgs e)
    {
        TextBox1.Enabled = true;
        Button9.Visible = true;
        edit11.Visible = false;
        Button1.Visible = true;
       
    }
    protected void edit21_Click(object sender, EventArgs e)
    {
        TextBox2.Enabled = true;
        Button10.Visible = true;
        edit21.Visible = false;
        Button1.Visible = true;
    }
    protected void edit31_Click(object sender, EventArgs e)
    {
        TextBox3.Enabled = true;
        Button11.Visible = true;
        edit31.Visible = false;
        Button1.Visible = true;
        Button1.Visible = true;
       
    }
    protected void edit41_Click(object sender, EventArgs e)
    {
        TextBox4.Enabled = true;
        Button12.Visible = true;
        edit41.Visible = false;
        Button1.Visible = true;
        Button1.Visible = true;
        
    }
    public void fillpage()
    {
        //Image1.ImgeUrl = "";
        //Image2.ImgeUrl = "";
        //Image3.ImgeUrl = "";
        //Image4.ImgeUrl = "";

        string str1 = "Select * from  CompanyProductsAndServicesInputTBL where CompanyID='" + Session["Comid"] + "'  and Active='Approved'";
        SqlCommand cmd1 = new SqlCommand(str1, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd1);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        if (ds.Rows.Count > 0)
        {
            lbimage1.Text = ds.Rows[0]["ProductsandservicesimageURL1"].ToString();
            lbimage2.Text = ds.Rows[0]["ProductsandservicesimageURL2"].ToString();
            lbimage3.Text = ds.Rows[0]["ProductsandservicesimageURL3"].ToString();
            lbimage4.Text = ds.Rows[0]["ProductsandservicesimageURL4"].ToString();
            TextBox1.Enabled = false;
            TextBox2.Enabled = false;
            TextBox3.Enabled = false;
            TextBox4.Enabled = false;
            TextBox1.Text = ds.Rows[0]["Productsandservicestext1"].ToString();
            TextBox2.Text = ds.Rows[0]["Productsandservicestext2"].ToString();
            TextBox3.Text = ds.Rows[0]["Productsandservicestext3"].ToString();
            TextBox4.Text = ds.Rows[0]["Productsandservicestext4"].ToString();
            //  string path = ds.Rows[0]["Image1URL"].ToString();
            Image1.ImageUrl = "~\\ShoppingCart\\images\\" + ds.Rows[0]["ProductsandservicesimageURL1"].ToString();
            Image2.ImageUrl = "~\\ShoppingCart\\images\\" + ds.Rows[0]["ProductsandservicesimageURL2"].ToString();
            Image3.ImageUrl = "~\\ShoppingCart\\images\\" + ds.Rows[0]["ProductsandservicesimageURL3"].ToString();
            Image4.ImageUrl = "~\\ShoppingCart\\images\\" + ds.Rows[0]["ProductsandservicesimageURL4"].ToString();
        }
        else
        {
            string str2 = "Select * from  CompanyProductsAndServicesInputTBL where CompanyID='" + Session["Comid"] + "'  and Active='Unapproved'";
            SqlCommand cmd2 = new SqlCommand(str2, con);
            SqlDataAdapter adp2 = new SqlDataAdapter(cmd2);
            DataTable ds2 = new DataTable();
            adp2.Fill(ds2);
            if (ds2.Rows.Count > 0)
            {
                lbimage1.Text = ds2.Rows[0]["ProductsandservicesimageURL1"].ToString();
                lbimage2.Text = ds2.Rows[0]["ProductsandservicesimageURL2"].ToString();
                lbimage3.Text = ds2.Rows[0]["ProductsandservicesimageURL3"].ToString();
                lbimage4.Text = ds2.Rows[0]["ProductsandservicesimageURL4"].ToString();
                TextBox1.Enabled = false;
                TextBox2.Enabled = false;
                TextBox3.Enabled = false;
                TextBox4.Enabled = false;
                TextBox1.Text = ds2.Rows[0]["Productsandservicestext1"].ToString();
                TextBox2.Text = ds2.Rows[0]["Productsandservicestext2"].ToString();
                TextBox3.Text = ds2.Rows[0]["Productsandservicestext3"].ToString();
                TextBox4.Text = ds2.Rows[0]["Productsandservicestext4"].ToString();
                //  string path = ds.Rows[0]["Image1URL"].ToString();
                Image1.ImageUrl = "~\\ShoppingCart\\images\\" + ds2.Rows[0]["ProductsandservicesimageURL1"].ToString();
                Image2.ImageUrl = "~\\ShoppingCart\\images\\" + ds2.Rows[0]["ProductsandservicesimageURL2"].ToString();
                Image3.ImageUrl = "~\\ShoppingCart\\images\\" + ds2.Rows[0]["ProductsandservicesimageURL3"].ToString();
                Image4.ImageUrl = "~\\ShoppingCart\\images\\" + ds2.Rows[0]["ProductsandservicesimageURL4"].ToString();
            }
            else
            {
                TextBox1.Enabled = false;
                TextBox2.Enabled = false;
                TextBox3.Enabled = false;
                TextBox4.Enabled = false;
            }
        }
            edit1.Visible = true;
            edit2.Visible = true;
            edit3.Visible = true;
            edit4.Visible = true;
            edit11.Visible = true;
            edit21.Visible = true;
            edit31.Visible = true;
            edit41.Visible = true;
        
    }
    public bool ext111(string filename)
    {
        string[] validFileTypes = { "jpeg", "bmp", "gif", "png", "jpg" };

        string ext = System.IO.Path.GetExtension(filename);

        bool isValidFile = true;

        for (int i = 0; i < validFileTypes.Length; i++)
        {

            if (ext == "." + filename)
            {

                isValidFile = true;

                break;

            }

        }
        return isValidFile;
    }
    protected void submit1_Click(object sender, EventArgs e)
    {
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
                Image1.Height = 220;
                Image1.Width = 642;
                Image1.ImageUrl = "~\\ShoppingCart\\images\\" + FileUpload1.FileName;
            }
            lblmsg.Visible = true;
            lblmsg.Text = "Image uploaded.";
            lbl1.Text = FileUpload1.FileName;
            lbimage1.Text = FileUpload1.FileName;
        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Invalid File Type.Please upload file in one of the following formats:jpeg, bmp, gif, png, jpg,";
        }
        FileUpload1.Visible = false;
        submit1.Visible = false;
        Button1.Visible = true;
    }

    protected void submit2_Click(object sender, EventArgs e)
    {

        bool valid = ext111(FileUpload2.FileName);

        if (valid == true)
        {

            if (FileUpload2.HasFile == true)
            {
                if (Directory.Exists(Server.MapPath("~\\ShoppingCart\\images\\")) == false)
                {
                    Directory.CreateDirectory(Server.MapPath("~\\ShoppingCart\\images\\"));
                }
                FileUpload2.PostedFile.SaveAs(Server.MapPath("~\\ShoppingCart\\images\\") + FileUpload2.FileName);
                Image2.Height = 220;
                Image2.Width = 642;
                Image2.ImageUrl = "~\\ShoppingCart\\images\\" + FileUpload2.FileName;
            }
            lblmsg.Visible = true;
            lblmsg.Text = "Image uploaded.";
            lbl2.Text = FileUpload2.FileName;
            lbimage2.Text = FileUpload2.FileName;

        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Invalid File Type.Please upload file in one of the following formats:jpeg, bmp, gif, png, jpg";
        }


        FileUpload2.Visible = false;
        submit2.Visible = false;
        Button1.Visible = true;
    }

    protected void submit3_Click(object sender, EventArgs e)
    {
        bool valid = ext111(FileUpload3.FileName);

        if (valid == true)
        {

            if (FileUpload3.HasFile == true)
            {
                if (Directory.Exists(Server.MapPath("~\\ShoppingCart\\images\\")) == false)
                {
                    Directory.CreateDirectory(Server.MapPath("~\\ShoppingCart\\images\\"));
                }
                FileUpload3.PostedFile.SaveAs(Server.MapPath("~\\ShoppingCart\\images\\") + FileUpload3.FileName);
                Image3.Height = 220;
                Image3.Width = 642;
                Image3.ImageUrl = "~\\ShoppingCart\\images\\" + FileUpload3.FileName;
            }
            lblmsg.Text = "Image uploaded.";
            lbl3.Text = FileUpload3.FileName;
            lbimage3.Text = FileUpload3.FileName;

        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Invalid File Type.Please upload file in one of the following formats:jpeg, bmp, gif, png, jpg";
        }

        FileUpload3.Visible = false;
        submit3.Visible = false;
        Button1.Visible = true;
    }

    protected void submit4_Click(object sender, EventArgs e)
    {
        bool valid = ext111(FileUpload4.FileName);

        if (valid == true)
        {

            if (FileUpload4.HasFile == true)
            {
                if (Directory.Exists(Server.MapPath("~\\ShoppingCart\\images\\")) == false)
                {
                    Directory.CreateDirectory(Server.MapPath("~\\ShoppingCart\\images\\"));
                }
                FileUpload4.PostedFile.SaveAs(Server.MapPath("~\\ShoppingCart\\images\\") + FileUpload4.FileName);
                Image4.Height = 220;
                Image4.Width = 642;
                Image4.ImageUrl = "~\\ShoppingCart\\images\\" + FileUpload4.FileName;
            }
            lblmsg.Visible = true;
            lblmsg.Text = "Image uploaded";
            lbl4.Text = FileUpload4.FileName;
            lbimage4.Text = FileUpload4.FileName;

        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Invalid File Type.Please upload file in one of the following formats:jpeg, bmp, gif, png, jpg";
        }

        FileUpload4.Visible = false;
        submit4.Visible = false;
        Button1.Visible = true;


    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string str2 = "Select * from  CompanyProductsAndServicesInputTBL where CompanyID='" + Session["Comid"] + "'  and Active='Unapproved'";
        SqlCommand cmd2 = new SqlCommand(str2, con);
        SqlDataAdapter adp2 = new SqlDataAdapter(cmd2);
        DataTable ds2 = new DataTable();
        adp2.Fill(ds2);
        if (ds2.Rows.Count > 0)
        {
            string str1 = "update CompanyProductsAndServicesInputTBL set ProductsandservicesimageURL1= '" + lbl1.Text + "',ProductsandservicesimageURL2='" + lbl2.Text + "',ProductsandservicesimageURL3= '" + lbl3.Text + "',ProductsandservicesimageURL4 ='" + lbl4.Text + "',Productsandservicestext1='" + TextBox1.Text + "',Productsandservicestext2= '" + TextBox2.Text + "', Productsandservicestext3='" + TextBox3.Text + "',Productsandservicestext4= '" + TextBox4.Text + "',DateandTime= '" + DateTime.Now.ToString() + "' where CompanyID='" + Session["Comid"] + "' and Active='Unapproved'";
            SqlCommand cmd = new SqlCommand(str1, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        else
        {
            string str1 = "insert into CompanyProductsAndServicesInputTBL (CompanyID,ProductsandservicesimageURL1,ProductsandservicesimageURL2,ProductsandservicesimageURL3,ProductsandservicesimageURL4,Productsandservicestext1 ,Productsandservicestext2,Productsandservicestext3,Productsandservicestext4,Active,DateandTime)values('" + Session["Comid"].ToString() + "','" + lbl1.Text + "','" + lbl2.Text + "','" + lbl3.Text + "','" + lbl4.Text + "','" + TextBox1.Text + "','" + TextBox2.Text + "','" + TextBox3.Text + "','" + TextBox4.Text + "','Unapproved','" + DateTime.Now.ToString() + "')";
            SqlCommand cmd = new SqlCommand(str1, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();


        }
        string str3 = "select max(ID) as ID from CompanyProductsAndServicesInputTBL where CompanyID='" + Session["Comid"].ToString() + "' and Active='Unapproved' ";
        SqlCommand cmd22 = new SqlCommand(str3, con);
        SqlDataAdapter adp21 = new SqlDataAdapter(cmd22);
        DataTable ds21 = new DataTable();
        adp21.Fill(ds21);
        if (ds21.Rows.Count > 0)
        {
            ViewState["productid"] = ds21.Rows[0]["ID"].ToString();
        }
        sendmailtoadmin();
        Label6.Visible = true;
        Label5.Visible = false;
        lblmsg.Text = "";
        lblmsg.Visible = false;
        Label6.Text = "Your requested changes have been submitted and are pending authorization.";
   
    }
    public void sendmailtoadmin()
    {
        string str2 = "Select * from  CompanyProductsAndServicesInputTBL where CompanyID='" + Session["Comid"] + "'  and Active='Unapproved'";
        SqlCommand cmd2 = new SqlCommand(str2, con);
        SqlDataAdapter adp2 = new SqlDataAdapter(cmd2);
        DataTable ds2 = new DataTable();
        adp2.Fill(ds2);
        string str3 = "Select * from  CompanyMaster where Compid='" + Session["Comid"] + "' ";
        SqlCommand cmd3 = new SqlCommand(str3, con);
        SqlDataAdapter adp3 = new SqlDataAdapter(cmd3);
        DataTable ds3 = new DataTable();
        adp3.Fill(ds3);
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
        // string tomail = //txtConfirmEmail.Text;
        if (dt21.Rows.Count > 0)
        {
            string file = "job-center-logo.jpg";
            // string body1 = "<br>Dear " + dt1.Rows[0][0].ToString() + " <br><br> " + txtmsg.Text + "<br><br> Your security code is: <br> Candidate Secuirity code :" + dt1.Rows[0][2].ToString() + " <br> Test Center Code: " + dt1.Rows[0][3].ToString() + " <br><br> With Regards,<br> IJobcenter ";
            string body1 = "<br>  <img src=\"http://members.ijobcenter.com/images/" + file + "\" \"border=\"0\" Width=\"90\" Height=\"80\" / > <br>Dear Admin, <br><br>" + ViewState["COMPANY"] + " is requesting that changes be made to their Products and Services Input page. You can review the changes and decide whether to approve or reject the requested changes by clicking <a href=http://www.ijobcenter.com/CompanySiteProductandServicesApproval.aspx?comid=" + ClsEncDesc.Encrypted(Session["Comid"].ToString()) + "&id=" + ClsEncDesc.Encrypted(ViewState["productid"].ToString()) + " target=_blank >here </a> or by copy and pasting the following URL into your internet browser.<br><br>http://www.ijobcenter.com/CompanySiteProductandServicesApproval.aspx?comid=" + ClsEncDesc.Encrypted(Session["Comid"].ToString()) + "&id=" + ClsEncDesc.Encrypted(ViewState["productid"].ToString()) + " <br><br><br><br>Thank you,<br>" + aa + "<br>" + bb + "<br>" + cc + "<br>" + ee + "<br>";

            string email = Convert.ToString(dt21.Rows[0]["UserIdtosendmail"]);
            string displayname = Convert.ToString("IJobCenter");
            string password = Convert.ToString(dt21.Rows[0]["Password"]);
            string outgo = Convert.ToString(dt21.Rows[0]["Mailserverurl"]);
            string body = body1;
            string Subject = "Approval for new Products and Services details ";


            MailAddress to = new MailAddress("support@ijobcenter.com");//info@ijobcenter.com("company12@safestmail.net");//
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

    protected void TextBox4_TextChanged(object sender, System.EventArgs e)
    {
        Label10.Visible = true;
    }
    protected void TextBox1_TextChanged(object sender, System.EventArgs e)
    {
        Label7.Visible = true;

    }
    protected void TextBox2_TextChanged(object sender, System.EventArgs e)
    {
        Label8.Visible = true;

    }
    protected void TextBox3_TextChanged(object sender, System.EventArgs e)
    {
        Label9.Visible = true;
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        if (DropDownList1.SelectedValue == "0")
        {
            Panel2.Visible = true;
            Panel3.Visible = false;
            Panel4.Visible = false;
            Panel5.Visible = false;

            lbimage1.Visible = true;

            fillpage();
            lblmsg.Text = "";
            Label5.Text = "";
            Label6.Text = "";
            Button1.Visible = true;
        }
        else if (DropDownList1.SelectedValue == "1")
        {
            Panel3.Visible = true;
            Panel2.Visible = false;
            Panel4.Visible = false;
            Panel5.Visible = false;

            lbimage2.Visible = true;


            fillpage();
            lblmsg.Text = "";
            Label5.Text = "";
            Label6.Text = "";
            Button1.Visible = true;
        }
        else if (DropDownList1.SelectedValue == "2")
        {
            Panel4.Visible = true;
            Panel3.Visible = false;
            Panel2.Visible = false;
            Panel5.Visible = false;

            lbimage3.Visible = true;


            fillpage();
            lblmsg.Text = "";
            Label5.Text = "";
            Label6.Text = "";
            Button1.Visible = true;

        }
        else
        {
            Panel5.Visible = true;
            Panel3.Visible = false;
            Panel4.Visible = false;
            Panel2.Visible = false;

            lbimage4.Visible = true;


            fillpage();
            lblmsg.Text = "";
            Label5.Text = "";
            Label6.Text = "";
            Button1.Visible = true;
        }

    }
    protected void Button8_Click(object sender, System.EventArgs e)
    {
        FileUpload4.Visible = false;
        submit4.Visible = false;
        edit4.Visible = true;
        Button8.Visible = false;
    }
    protected void Button7_Click1(object sender, System.EventArgs e)
    {
        FileUpload3.Visible = false;
        submit3.Visible = false;
        edit3.Visible = true;
        Button7.Visible = false;
    }
    protected void Button5_Click(object sender, System.EventArgs e)
    {
        FileUpload2.Visible = false;
        submit2.Visible = false;
        edit2.Visible = true;
        Button5.Visible = false;
    }
    protected void Button3_Click(object sender, System.EventArgs e)
    {
        FileUpload1.Visible = false;
        submit1.Visible = false;
        edit1.Visible = true;
        Button3.Visible = false;
    }
    protected void Button9_Click(object sender, System.EventArgs e)
    {
        TextBox1.Enabled = false;
        Button9.Visible = false;
        edit11.Visible = true;
    }
    protected void Button10_Click(object sender, System.EventArgs e)
    {
        TextBox2.Enabled = false;
        Button10.Visible = false;
        edit21.Visible = true;
    }
    protected void Button11_Click(object sender, System.EventArgs e)
    {
        TextBox3.Enabled = false;
        Button11.Visible = false;
        edit31.Visible = true;
    }
    protected void Button12_Click(object sender, System.EventArgs e)
    {
        TextBox4.Enabled = false;
        Button12.Visible = false;
        edit41.Visible = true;
    }
}