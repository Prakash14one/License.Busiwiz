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
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Security.Cryptography;
public partial class Companymoreinfo : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    //SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["infinal"].ConnectionString);
   // SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["infinal"].ConnectionString);
    SqlConnection conn;
    DataTable dt;
    SqlCommand cmd1;
    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        conn = pgcon.dynconn; 
        lblmsg.Text = "";
        if (!IsPostBack)
        {
          
            Fillddlcountry();
           
            BusinessType();

            Businesscatmaster();
            Businesssubcatmaster();
            Businesssubsubcatmaster();
            Fillddlstatelist();
            if (Request.QueryString["comid"] != null)
            {
                
               string spp = "select * from CompanyMaster where  CompanyLoginId='" + Request.QueryString["comid"] + "'";
            datafilled(spp);
            }
            else
            {
                ViewState["mcid"] = Session["CompanyId"].ToString();
              
                lblcname.Text = Session["CompanyName"].ToString();
                lbl_com.Text = Session["CompanyName"].ToString();

                string spp = "select * from CompanyMaster where CompanyId='" + Session["CompanyId"] + "'";
                datafilled(spp);
                filldata();
            }
            string strcln = "Select Sqldatabasename,SqlconnurlIp,SqlPort,SqlUserId,SqlPassword from CompanyMaster inner join PricePlanMaster on PricePlanMaster.PricePlanId=CompanyMaster.PricePlanId inner join ProductMaster on ProductMaster.ProductId= PricePlanMaster.ProductId where CompanyMaster.CompanyId='" + ViewState["mcid"] + "' and ProductMaster.Download='1'";
            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);
            if (dtcln.Rows.Count > 0)
            {
                pnlsqlconn.Visible = true;
                txtsqlserurlip.Text = dtcln.Rows[0]["SqlconnurlIp"].ToString();
                txtsqldbname.Text = dtcln.Rows[0]["Sqldatabasename"].ToString();
                txtport.Text = dtcln.Rows[0]["SqlPort"].ToString();
                txtuserid.Text = dtcln.Rows[0]["SqlUserId"].ToString();
                txtpass.Text = dtcln.Rows[0]["SqlPassword"].ToString();
                string strclnt = "Select BusiControllerApplicationURL,DatabaseName,DatabaseServerNameOrIp,Port,UserID,Password,compnaymasterid from BusiControllerMasterTBl where compnaymasterid='" + ViewState["mcid"] + "'";
                SqlCommand cmdclnt = new SqlCommand(strclnt, con);
                DataTable dtclnt = new DataTable();
                SqlDataAdapter adpclnt = new SqlDataAdapter(cmdclnt);
                adpclnt.Fill(dtclnt);
                if (dtclnt.Rows.Count > 0)
                {
                    txtbusisiteurl.Text = dtclnt.Rows[0]["BusiControllerApplicationURL"].ToString();
                    txtbusiurlip.Text = dtclnt.Rows[0]["DatabaseServerNameOrIp"].ToString();
                    txtbusidatabasevabe.Text = dtclnt.Rows[0]["DatabaseName"].ToString();
                    txtbusiport.Text = dtclnt.Rows[0]["Port"].ToString();
                    txtbusiuserid.Text = dtclnt.Rows[0]["UserID"].ToString();
                    txtbusipass.Text = dtclnt.Rows[0]["Password"].ToString();

                }

            }
            else
            {
                pnlsqlconn.Visible = false;
            }
            change_Click(sender, e);
        }
    }

    public void sendmail1()
    {
       

      

        string str21 = "  select distinct  PortalMasterTbl.* from  CompanyMaster inner join PricePlanMaster  on CompanyMaster.PricePlanId=PricePlanMaster.PricePlanId inner join VersionInfoMaster on VersionInfoMaster.VersionInfoId=PricePlanMaster.VersionInfoMasterId inner join ProductMaster on VersionInfoMaster.ProductId=ProductMaster.ProductId inner join ClientMaster on ProductMaster.ClientMasterId=ClientMaster.ClientMasterId inner join Priceplancategory on Priceplancategory.ID=PricePlanMaster.PriceplancatId inner join PortalMasterTbl on PortalMasterTbl.Id=Priceplancategory.PortalId   inner join OrderMaster on CompanyMaster.CompanyLoginId=OrderMaster.CompanyLoginId inner join  OrderPaymentSatus on OrderMaster.OrderId=OrderPaymentSatus.OrderId inner join StateMasterTbl on StateMasterTbl.StateId=PortalMasterTbl.StateId inner join CountryMaster on StateMasterTbl.CountryId=CountryMaster.CountryId  WHERE (PortalMasterTbl.Id=7)  ";
        SqlCommand cmd45 = new SqlCommand(str21, con);
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
        // string tomail = //txtEmail.Text;
        if (dt21.Rows.Count > 0)
        {

            // string body1 = "<br>Dear " + dt1.Rows[0][0].ToString() + " <br><br> " + txtmsg.Text + "<br><br> Your security code is: <br> Candidate Secuirity code :" + dt1.Rows[0][2].ToString() + " <br> Test Center Code: " + dt1.Rows[0][3].ToString() + " <br><br> With Regards,<br> IJobcenter ";
            //priya string body1 = "<br>Dear Admin <br><br><u>Comapany Information </u> <br><br>Company Name: " + TextBox1.Text + "           Company ID:" + Label7.Text + " <br><br> Location -       Country: " + ddlcountry.SelectedItem.Text + "           State: " + ddlstate.SelectedItem.Text + "             City: " + ddlcity.SelectedItem.Text + " <br><br> Email Address: " + txtEmail.Text + " <br><br> Confirm Address: " + txtEmail.Text + " <br><br> <u>Tell us about the vacancy </u> <br><br> Vacancy Category :" + ddlvacancytype.SelectedItem.Text + " <br><br>Vacancy Position: " + ddlvacancyposition.SelectedItem.Text + " <br><br>Location-            Country: " + ddlcountry1.SelectedItem.Text + "                State: " + ddlstate1.SelectedItem.Text + "                 City: " + ddlcity1.SelectedItem.Text + " <br><br> Description : " + txtdescription.Text + " <br><br> If you wish to approve this registration and vacancy posted by the company, Please click <a href=http://www.ijobcenter.com/companyapproval.aspx?comid=" + ClsEncDesc.Encrypted(Label7.Text) + "&id=" + dsmaxid.Rows[0]["Id"].ToString() + " target=_blank >here </a>. If the the link does not open please copy and paste the following link http://www.ijobcenter.com/companyapproval.aspx?comid=" + ClsEncDesc.Encrypted(Label7.Text) + "&id=" + ClsEncDesc.Encrypted(dsmaxid.Rows[0]["Id"].ToString()) + " <br><br> If you wish to reject  this registration and vacancy posted by the company, Please click <a href=http://www.ijobcenter.com/companyautoregistrationrejection.aspx?comid=" + ClsEncDesc.Encrypted(Label7.Text) + "&id=" + ClsEncDesc.Encrypted(dsmaxid.Rows[0]["Id"].ToString()) + " target=_blank >here </a>. If the the link does not open please copy and paste the following link http://www.ijobcenter.com/companyautoregistrationrejection.aspx?comid=" + ClsEncDesc.Encrypted(Label7.Text) + "&id=" + ClsEncDesc.Encrypted(dsmaxid.Rows[0]["Id"].ToString()) + " <br><br>Thank you,<br><br>" + aa + "<br>" + bb + "<br>" + cc + "<br>" + ee + "<br>";
            string body1 = "<br>Dear Admin <br><br><u>The following changes have been requested: </u> <br><br> " +
                " Existing Company Name:  " + lbl_com.Text + "  " +
                " Requested Company Name: " + lblcname.Text + " <br><br> " +
                " Requested Changes to About Us: " + txtbusdesc.Text + " <br><br> " +
                " Requested Changes to Products/Services: " + txtserviceprovided.Text + "  " +

                " If you wish to view " + lbl_com.Text + " current Company Site click here, <a href=http://members.busiwiz.com/companyapproval.aspx?comid=" + ViewState["comid"] + " target=_blank >here </a>. " +
                " <br> If the the link does not open please copy and paste the following link http://members.busiwiz.com/companyapproval.aspx?comid=" + ViewState["comid"] + " <br> " +
                " To approve the requested changes, click here <a href=http://www.ijobcenter.com/companyautoregistrationrejection.aspx?comidapp=" + ViewState["comid"] + " target=_blank >here </a>." +
                " If the the link does not open please copy and paste the following link http://www.ijobcenter.com/companyautoregistrationrejection.aspx?comid=" + ViewState["comid"] + " <br><br>Thank you,<br><br>" + aa + "<br>" + bb + "<br>" + cc + "<br>" + ee + "<br>";

            string email = Convert.ToString(dt21.Rows[0]["UserIdtosendmail"]);
            string displayname = Convert.ToString("IJobCenter");
            string password = Convert.ToString(dt21.Rows[0]["Password"]);
            string outgo = Convert.ToString(dt21.Rows[0]["Mailserverurl"]);
            string body = body1;
            string Subject = "Approval for new  registration and vacancy posted by the company ";


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
    protected void datafilled(string spp)
    {
        
        SqlCommand cmd2sel = new SqlCommand(spp, con);
        SqlDataAdapter adp2sel = new SqlDataAdapter(cmd2sel);
        DataTable ds2sel = new DataTable();
        adp2sel.Fill(ds2sel);

        if (ds2sel.Rows.Count > 0)
        {
            txtcity.Text = ds2sel.Rows[0]["city"].ToString();
            imgdisplay.ImageUrl = "~/images/" + ds2sel.Rows[0]["companylogo"].ToString() + "";
            ImageButton1.ImageUrl = "~/images/" + ds2sel.Rows[0]["companylogo"].ToString() + ""; 
            
            txtbusdesc.Text = ds2sel.Rows[0]["BusinessDescription"].ToString();
            lbl_busdesc.Text = ds2sel.Rows[0]["BusinessDescription"].ToString();
            txtcontactper.Text = ds2sel.Rows[0]["ContactPerson"].ToString();
            txtcodesi.Text = ds2sel.Rows[0]["ContactPersonDesignation"].ToString();
            lblcname.Text = ds2sel.Rows[0]["Companyname"].ToString();
            txthostedsite.Text = ds2sel.Rows[0]["CompanyWebsite"].ToString();
            txtadd.Text = ds2sel.Rows[0]["Address"].ToString();
            txtemail.Text = ds2sel.Rows[0]["Email"].ToString();
            ViewState["comid"] = ds2sel.Rows[0]["CompanyLoginId"].ToString();
            chkcheck.Checked = Convert.ToBoolean(ds2sel.Rows[0]["Active"].ToString());
            txtphn.Text = ds2sel.Rows[0]["phone"].ToString();
            txtzipcode.Text = ds2sel.Rows[0]["pincode"].ToString();
            
            txtfax.Text = ds2sel.Rows[0]["fax"].ToString();
            ddlcountry.SelectedIndex = ddlcountry.Items.IndexOf(ddlcountry.Items.FindByValue(ds2sel.Rows[0]["countryId"].ToString()));
            ddlstate.SelectedIndex = ddlstate.Items.IndexOf(ddlstate.Items.FindByValue(ds2sel.Rows[0]["stateid"].ToString()));
            ddlbustype.SelectedIndex = ddlbustype.Items.IndexOf(ddlbustype.Items.FindByValue(ds2sel.Rows[0]["Businesstypeid"].ToString()));
            ddlbuscategory.SelectedIndex = ddlbuscategory.Items.IndexOf(ddlbuscategory.Items.FindByValue(ds2sel.Rows[0]["Businessscatid"].ToString()));
            ddlbussubcat.SelectedIndex = ddlbussubcat.Items.IndexOf(ddlbussubcat.Items.FindByValue(ds2sel.Rows[0]["Businesssubcateid"].ToString()));
            ddlsubsubcat.SelectedIndex = ddlsubsubcat.Items.IndexOf(ddlsubsubcat.Items.FindByValue(ds2sel.Rows[0]["Businesssubsubcatid"].ToString()));
            txtcity.Text = ds2sel.Rows[0]["city"].ToString();

            txtphonecc.Text = ds2sel.Rows[0]["Phonecc"].ToString();
            txtmobilecc.Text = ds2sel.Rows[0]["Mobilecc"].ToString();
            txtmobileno.Text = ds2sel.Rows[0]["MobileNo"].ToString();
            txtserviceprovided.Text = ds2sel.Rows[0]["ServiceProvided"].ToString();
            lbl_serviceprovided.Text = ds2sel.Rows[0]["ServiceProvided"].ToString();

            Textpaypal.Text = ds2sel.Rows[0]["paypalid"].ToString();
            Txtmastereid.Text = ds2sel.Rows[0]["MasterEmailid"].ToString();
            txtincomingserver.Text = ds2sel.Rows[0]["IncomingServerPOP"].ToString(); ;
            txtincomingserveruserid.Text = ds2sel.Rows[0]["IncomingServerUSerID"].ToString();
            incomingserverpassword.Text = ds2sel.Rows[0]["IncolingServerPasword"].ToString();
            txtoutgoingserver.Text = ds2sel.Rows[0]["OurgoingServerSMTP"].ToString();
            txtoutgoingserveruserid.Text = ds2sel.Rows[0]["OutgoingServerUserID"].ToString();
            outgoingserverpassword.Text = ds2sel.Rows[0]["OutgoingServerPassword"].ToString();
            urltext.Text = ds2sel.Rows[0]["Domainurl"].ToString();
            
        }
    }

  
    protected void BusinessType()
    {
        string strcln = "SELECT * from Businesstypetbl order by bustypename";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        ddlbustype.DataSource = dtcln;
        ddlbustype.DataValueField = "Id";
        ddlbustype.DataTextField = "bustypename";
        ddlbustype.DataBind();
        ddlbustype.Items.Insert(0, "-Select-");
        ddlbustype.Items[0].Value = "0";
    }
    protected void Businesscatmaster()
    {
        string strcln = "SELECT * from BusinessCategorytbl order by businesscategoryname";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        ddlbuscategory.DataSource = dtcln;
        ddlbuscategory.DataValueField = "Id";
        ddlbuscategory.DataTextField = "businesscategoryname";
        ddlbuscategory.DataBind();
        ddlbuscategory.Items.Insert(0, "-Select-");
        ddlbuscategory.Items[0].Value = "0";
    }
         protected void Businesssubcatmaster()
    {
        string strcln = "SELECT * from Subbusinesscategorytbl order by subcategoryname";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        ddlbussubcat.DataSource = dtcln;
        ddlbussubcat.DataValueField = "Id";
        ddlbussubcat.DataTextField = "subcategoryname";
        ddlbussubcat.DataBind();
        ddlbussubcat.Items.Insert(0, "-Select-");
        ddlbussubcat.Items[0].Value = "0";
    }
     protected void Businesssubsubcatmaster()
    {
        string strcln = "SELECT * from SubSubbusinesscategorytbl order by subsubcategoryname";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        ddlsubsubcat.DataSource = dtcln;
        ddlsubsubcat.DataValueField = "Id";
        ddlsubsubcat.DataTextField = "subsubcategoryname";
        ddlsubsubcat.DataBind();
        ddlsubsubcat.Items.Insert(0, "-Select-");
        ddlsubsubcat.Items[0].Value = "0";
    }
    
    protected void filldata()
    {
        txtbusdesc.ReadOnly = true;
        lblcname.ReadOnly = true;
        txtcontactper.ReadOnly = true;
        txtcodesi.ReadOnly = true;
        txtcity.ReadOnly = true;
   
        txthostedsite.ReadOnly = true;
        txtadd.ReadOnly = true;
        txtemail.ReadOnly = true;
        txtphn.ReadOnly = true;
        txtfax.ReadOnly = true;
        txtzipcode.ReadOnly = true;
        chkcheck.Enabled = false;
        Textpaypal.ReadOnly = true;
        Txtmastereid.ReadOnly = true;
        txtincomingserver.ReadOnly = true;
        txtincomingserveruserid.ReadOnly = true;
        incomingserverpassword.ReadOnly = true;
        txtoutgoingserver.ReadOnly = true;
        txtoutgoingserveruserid.ReadOnly = true;
        outgoingserverpassword.ReadOnly = true;
        urltext.ReadOnly = true;
        btnsubmited.Visible = false;
        Change.Visible = true;

        txtsqlserurlip.ReadOnly = true;
        txtsqldbname.ReadOnly = true;
        txtport.ReadOnly = true;
        txtuserid.ReadOnly = true;
        txtpass.ReadOnly = true;
        txtbusisiteurl.ReadOnly = true;
        txtbusiurlip.ReadOnly = true;
        txtbusidatabasevabe.ReadOnly = true;
        txtbusiport.ReadOnly = true;
        txtbusiuserid.ReadOnly = true;
        txtbusipass.ReadOnly = true;
}
    protected void change_Click(object sender, EventArgs e)
    {
        //txtbusdesc.ReadOnly = false;
        //lblcname.ReadOnly = false;
        //txtcontactper.ReadOnly = false;
        //txtcodesi.ReadOnly = false;
        btnsubmited.Visible = true;
        btnsubmited.Text = "Update";
        Change.Visible = false;
        //txtcity.ReadOnly = false;
        //chkcheck.Enabled = true;
        //txthostedsite.ReadOnly = false;
        //txtadd.ReadOnly = false;
        txtemail.ReadOnly = false;
        txtphn.ReadOnly = false;
        //txtfax.ReadOnly = false;
        //txtzipcode.ReadOnly = false;
    
        //Textpaypal.ReadOnly = false;
        //Txtmastereid.ReadOnly = false;
        //txtincomingserver.ReadOnly = false;
        //txtincomingserveruserid.ReadOnly = false;
        //incomingserverpassword.ReadOnly = false;
        //txtoutgoingserver.ReadOnly = false;
        //txtoutgoingserveruserid.ReadOnly = false;
        //outgoingserverpassword.ReadOnly = false;
        //lblcname.ReadOnly = false;
        //txtcontactper.ReadOnly = false;
        //txtcodesi.ReadOnly = false;
        //urltext.ReadOnly = false;
        //txtsqlserurlip.ReadOnly = false;
        //txtsqldbname.ReadOnly = false;
        //txtport.ReadOnly = false;
        //txtuserid.ReadOnly = false;
        //txtpass.ReadOnly = false;
        //txtbusisiteurl.ReadOnly = false;
        //txtbusiurlip.ReadOnly = false;
        //txtbusidatabasevabe.ReadOnly = false;
        //txtbusiport.ReadOnly = false;
        //txtbusiuserid.ReadOnly = false;
        //txtbusipass.ReadOnly = false;
    }
    protected void Butsubmit_Clickchange(object sender, EventArgs e)
    {
        string te = "http://members.ijobcenter.com/ShoppingCart/Admin/Businesslogo.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void Butsubmit_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {

            FileUpload1.PostedFile.SaveAs(Server.MapPath("images\\") + FileUpload1.FileName);
            ViewState["upfile"] = FileUpload1.FileName;
            imgdisplay.ImageUrl = "~/images/" + FileUpload1.FileName + ""; 
            lblimage.Text = "Upload Image Successfully.";
        }
    }
    protected void btnsubmited_Click(object sender, EventArgs e)
    {
        if (txtbusdesc.Text != lbl_busdesc.Text || txtserviceprovided.Text != lbl_serviceprovided.Text || lblcname.Text != lbl_com.Text)
        {
        
        string str1 = "Insert Into CompanyMaster_Approval (CompanyName='" + lblcname.Text + "' and ServiceProvided='" + txtserviceprovided.Text + "' and BusinessDescription='" + txtbusdesc.Text + "' and CompanyLoginId='" + ViewState["comid"] + "')";
        SqlCommand cmd1 = new SqlCommand(str1, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd1.ExecuteNonQuery();
        con.Close();
        }

        //string str = "Update CompanyMaster set CompanyName='" + lblcname.Text + "',ContactPerson='" + txtcontactper.Text + "',ContactPersonDesignation='" + txtcodesi.Text + "', CompanyWebsite='" + txthostedsite.Text + "',date='" + DateTime.Now.ToShortDateString() + "',Address='" + txtadd.Text + "',Email='" + txtemail.Text + "',phone='" + txtphn.Text + "', fax='" + txtfax.Text + "', " +
        //              "websiteurl='" + txthostedsite.Text + "',Active='" + chkcheck.Checked + "', redirect='True',StateId='" + ddlstate.SelectedValue.ToString() + "',companylogo='" + ViewState["upfile"] + "',paypalid='" + Textpaypal.Text + "',MasterEmailid='" + Txtmastereid.Text + "',IncomingServerPOP='" + txtincomingserver.Text + "',IncomingServerUSerID='" + txtincomingserveruserid.Text + "',IncolingServerPasword='" + incomingserverpassword.Text + "',OurgoingServerSMTP='" + txtoutgoingserver.Text + "',OutgoingServerUserID='" + txtoutgoingserveruserid.Text + "',OutgoingServerPassword='" + outgoingserverpassword.Text + "',city='" + txtcity.Text + "',Businesstypeid='" + ddlbustype.SelectedValue + "',Businessscatid='" + ddlbuscategory.SelectedValue + "',Businesssubcateid='" + ddlbussubcat.SelectedValue + "',Businesssubsubcatid='" + ddlsubsubcat.SelectedValue + "',Countryid='" + ddlcountry.SelectedValue + "',BusinessDescription='" + txtbusdesc.Text + "',Domainurl='" + urltext.Text + "',Sqldatabasename='" + txtsqldbname.Text + "',SqlconnurlIp='" + txtsqlserurlip.Text + "',SqlPort='" + txtport.Text + "',SqlUserId='" + txtuserid.Text + "',SqlPassword='" + txtpass.Text + "' where CompanyLoginId='" + ViewState["comid"] + "'";
        string str = "Update CompanyMaster set CompanyName='" + lbl_com.Text + "',ContactPerson='" + txtcontactper.Text + "',ContactPersonDesignation='" + txtcodesi.Text + "', CompanyWebsite='" + txthostedsite.Text + "',date='" + DateTime.Now.ToShortDateString() + "',Address='" + txtadd.Text + "',Email='" + txtemail.Text + "',phone='" + txtphn.Text + "', fax='" + txtfax.Text + "',pincode='" + txtzipcode.Text + "', " +
                      "websiteurl='" + txthostedsite.Text + "',Active='" + chkcheck.Checked + "', redirect='True',StateId='" + ddlstate.SelectedValue.ToString() + "',companylogo='" + ViewState["upfile"] + "',paypalid='" + Textpaypal.Text + "',MasterEmailid='" + Txtmastereid.Text + "',IncomingServerPOP='" + txtincomingserver.Text + "',IncomingServerUSerID='" + txtincomingserveruserid.Text + "',IncolingServerPasword='" + incomingserverpassword.Text + "',OurgoingServerSMTP='" + txtoutgoingserver.Text + "',OutgoingServerUserID='" + txtoutgoingserveruserid.Text + "',OutgoingServerPassword='" + outgoingserverpassword.Text + "',city='" + txtcity.Text + "',Businesstypeid='" + ddlbustype.SelectedValue + "',Businessscatid='" + ddlbuscategory.SelectedValue + "',Businesssubcateid='" + ddlbussubcat.SelectedValue + "',Businesssubsubcatid='" + ddlsubsubcat.SelectedValue + "',Countryid='" + ddlcountry.SelectedValue + "',BusinessDescription='" + lbl_busdesc.Text + "',Domainurl='" + urltext.Text + "',Sqldatabasename='" + txtsqldbname.Text + "',SqlconnurlIp='" + txtsqlserurlip.Text + "',SqlPort='" + txtport.Text + "',SqlUserId='" + txtuserid.Text + "',SqlPassword='" + txtpass.Text + "',Phonecc='" + txtphonecc.Text + "',Mobilecc='" + txtmobilecc.Text + "',MobileNo='" + txtmobileno.Text + "',ServiceProvided='" + lbl_serviceprovided.Text + "' where CompanyLoginId='" + ViewState["comid"] + "'";

        SqlCommand cmd = new SqlCommand(str, con);
        con.Close();
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        if (pnlsqlconn.Visible == true)
        {
            string strb = "Update BusiControllerMasterTBl set BusiControllerApplicationURL='" + txtbusisiteurl.Text + "',DatabaseName='" + txtbusidatabasevabe.Text + "',DatabaseServerNameOrIp='" + txtbusiurlip.Text + "',Port='" + txtbusiport.Text + "',UserID='" + txtbusiuserid.Text + "',Password='" + txtbusipass.Text + "' where compnaymasterid='" + ViewState["mcid"] + "'";
            SqlCommand cmdb = new SqlCommand(strb, con);
            con.Open();
            cmdb.ExecuteNonQuery();
            con.Close();
            //string strb = "Insert into BusiControllerMasterTBl(BusiControllerApplicationURL,DatabaseName,DatabaseServerNameOrIp,Port,UserID,Password,compnaymasterid)Values('" + txtbusisiteurl.Text + "','" + txtbusidatabasevabe.Text + "','" + txtbusiurlip.Text + "','" + txtbusiport.Text + "','" + txtbusiuserid.Text + "','" + txtbusipass.Text + "' ,'" + ViewState["mcid"] + "')";
            //SqlCommand cmdb = new SqlCommand(strb, con);
            //con.Open();
            //cmdb.ExecuteNonQuery();
            //con.Close();
        }
        //string strs = "Select CompanyLoginId from CompanyMaster where  CompanyId='" + Session["CompanyId"] + "'";
        //SqlCommand cmds = new SqlCommand(strs, con);
        //con.Open();
        // object cid=cmds.ExecuteScalar();
        //con.Close();
        string stro = "Update OrderMaster set Address='" + txtadd.Text + "',Email='" + txtemail.Text + "',phone='" + txtphn.Text + "', fax='" + txtfax.Text + "', " +
                       " companylogo='" + ViewState["upfile"] + "',paypalid='" + Textpaypal.Text + "' where CompanyLoginId='" + ViewState["comid"].ToString() + "'";
        SqlCommand cmdo = new SqlCommand(stro, con);
        con.Open();
        cmdo.ExecuteNonQuery();
        con.Close();
        string strcln = "SELECT * from CompanyMaster where Compid='" + ViewState["comid"].ToString() + "'";
        SqlCommand cmdcln = new SqlCommand(strcln, conn);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);


        string strcitistate = "SELECT * from CompanyMaster where CompanyLoginId='" + ViewState["comid"] + "'";
        SqlCommand cmdcitystate = new SqlCommand(strcitistate, con);
        DataTable dtcitystate = new DataTable();
        SqlDataAdapter adpcitystate = new SqlDataAdapter(cmdcitystate);
        adpcitystate.Fill(dtcitystate);
        string onlinecountyid = "";

        string onlinestateid = "";
        string onlinecityid = "";
        string licencestatename = "";

        string licensestateid = "";
        string eplazastateid = "";
        string licensecity = "";
        string eplazacityid = "";

        if (dtcitystate.Rows.Count > 0)
        {
            string strcountry = dtcitystate.Rows[0]["countryId"].ToString(); //licence country id
            licensecity = dtcitystate.Rows[0]["city"].ToString(); //licence city name
            if (strcountry != "")
            {
                string strcounrty = "SELECT * from CountryMaster where CountryId='" + strcountry + "' ";
                SqlCommand cmdcountry = new SqlCommand(strcounrty, con);
                DataTable dtcountry = new DataTable();
                SqlDataAdapter adpcountry = new SqlDataAdapter(cmdcountry);
                adpcountry.Fill(dtcountry);
                if (dtcountry.Rows.Count > 0)
                {

                    if (dtcountry.Rows[0]["CountryName"].ToString() != "")
                    {
                        string stronlinecounrty = "SELECT * from CountryMaster where CountryName='" + dtcountry.Rows[0]["CountryName"].ToString() + "' ";
                        SqlCommand cmdonlinecountry = new SqlCommand(stronlinecounrty, conn);
                        DataTable dtonlinecountry = new DataTable();
                        SqlDataAdapter adponlinecountry = new SqlDataAdapter(cmdonlinecountry);
                        adponlinecountry.Fill(dtonlinecountry);

                        if (dtonlinecountry.Rows.Count > 0)
                        {
                            if (dtonlinecountry.Rows[0]["CountryId"].ToString() != "")
                            {
                                onlinecountyid = dtonlinecountry.Rows[0]["CountryId"].ToString(); //eplaza country id


                                if (dtcitystate.Rows[0]["countryId"].ToString() != "")
                                {
                                    licensestateid = dtcitystate.Rows[0]["StateId"].ToString();



                                    string strstate12 = "SELECT * from StateMasterTbl where CountryId='" + strcountry + "' and StateId='" + licensestateid + "' ";
                                    SqlCommand cmdstate12 = new SqlCommand(strstate12, con);
                                    DataTable dtstate12 = new DataTable();
                                    SqlDataAdapter adpstate12 = new SqlDataAdapter(cmdstate12);
                                    adpstate12.Fill(dtstate12);

                                    if (dtstate12.Rows.Count > 0)
                                    {
                                        licencestatename = dtstate12.Rows[0]["StateName"].ToString();

                                        string strstate12121 = "SELECT * from StateMasterTbl where CountryId='" + onlinecountyid + "' and StateName='" + licencestatename + "' ";
                                        SqlCommand cmdstate12121 = new SqlCommand(strstate12121, conn);
                                        DataTable dtstate12121 = new DataTable();
                                        SqlDataAdapter adpstate12121 = new SqlDataAdapter(cmdstate12121);
                                        adpstate12121.Fill(dtstate12121);

                                        if (dtstate12121.Rows.Count > 0)
                                        {
                                            eplazastateid = dtstate12121.Rows[0]["StateId"].ToString();// Eplaza State Id

                                            string strstate12121789 = "SELECT * from CityMasterTbl where StateId='" + eplazastateid + "' and CityName='" + licensecity + "' ";
                                            SqlCommand cmdstate12121789 = new SqlCommand(strstate12121789, conn);
                                            DataTable dtstate12121789 = new DataTable();
                                            SqlDataAdapter adpstate12121789 = new SqlDataAdapter(cmdstate12121789);
                                            adpstate12121789.Fill(dtstate12121789);

                                            if (dtstate12121789.Rows.Count > 0)
                                            {
                                                eplazacityid = dtstate12121789.Rows[0]["CityId"].ToString();

                                            }

                                        }

                                    }

                                }





                            }

                        }


                    }
                }



            }

        }




        if (dtcln.Rows.Count > 0)
        {
            string strin = "Update CompanyMaster set CompanyName='" + lblcname.Text + "',CompanyMainWebsite='"+urltext.Text+"', PaypalEmailId='" + Textpaypal.Text + "' where Compid='" + ViewState["comid"].ToString() + "'";
            SqlCommand cmdoin = new SqlCommand(strin, conn);
            conn.Open();
            cmdoin.ExecuteNonQuery();
            conn.Close();
            string strs = "Select CompanyId from CompanyMaster where   Compid='" + ViewState["comid"].ToString() + "'";
            SqlCommand cmds = new SqlCommand(strs, conn);
            conn.Open();
            object cid = cmds.ExecuteScalar();
            conn.Close();
            string strinn = "Update CompanyAddressMaster set CompanyName='" + lblcname.Text + "',ContactPersonNAme='" + txtcontactper.Text + "',ContactPersonDesignation='" + txtcodesi.Text + "',WebSite='" + txthostedsite.Text + "',Address1='" + txtadd.Text + "',Phone='" + txtphn.Text + "',Country='" + onlinecountyid + "',State='" + eplazastateid + "',City='" + eplazacityid + "',Email='" + txtemail.Text + "',Fax='" + txtfax.Text + "',PinCode='" + txtzipcode.Text + "' where CompanyMasterId='" + cid.ToString() + "'";
            SqlCommand cmdoinn = new SqlCommand(strinn, conn);
            conn.Open();
            cmdoinn.ExecuteNonQuery();
            conn.Close();
        }
        else
        {
            int ddt = Convert.ToInt32(DateTime.Now.Year.ToString());
            int dde = ddt + 1;
            string strin = "insert into CompanyMaster(CompanyName,AdminName,PaypalEmailId,Compid,YearEndingDate,FirstYearStartDate,StartDateOfAccountYear,CompanyMainWebsite)values('" + lblcname.Text + "','" + txtcontactper.Text + "','" + Textpaypal.Text + "', '" + ViewState["comid"].ToString() + "','3/31/" + dde + "','" + DateTime.Now.ToShortDateString() + "','4/1/" + ddt + "','"+urltext.Text+"')";
            SqlCommand cmdoin = new SqlCommand(strin, conn);
            conn.Open();
            cmdoin.ExecuteNonQuery();
            conn.Close();
            string strs = "Select CompanyId from CompanyMaster where   Compid='" + ViewState["comid"].ToString() + "'";
            SqlCommand cmds = new SqlCommand(strs, conn);
            conn.Open();
            object cid = cmds.ExecuteScalar();
            conn.Close();

            string strinn = "insert into CompanyAddressMaster(CompanyName,ContactPersonNAme,ContactPersonDesignation,WebSite,CompanyMasterId,Address1,Phone,Country,State,Email,Fax,City,PinCode)values('" + lblcname.Text + "','" + txtcontactper.Text + "','" + txtcodesi.Text + "','" + txthostedsite.Text + "', '" + cid.ToString() + "','" + txtadd.Text + "','" + txtphn.Text + "','" + onlinecountyid + "','" + eplazastateid + "','" + txtemail.Text + "','" + txtfax.Text + "','" + eplazacityid + "','" + txtzipcode.Text + "')";
            SqlCommand cmdoinn = new SqlCommand(strinn, conn);
            conn.Open();
            cmdoinn.ExecuteNonQuery();
            conn.Close();
        }
        lblmsg.Text = "Record Updated successfully.";
        filldata();
    }
    protected void Clear()
    {
        txtcity.Text = "";
        ddlcountry.SelectedIndex = 0;
        txthostedsite.Text = "";
        txtadd.Text = "";
        txtemail.Text = "";
        txtphn.Text = "";
        txtfax.Text = "";
      ddlstate.SelectedIndex=0;
      Textpaypal.Text = "";
      Txtmastereid.Text = "";
      txtzipcode.Text = "";
      txtincomingserver.Text = "";
      txtincomingserveruserid.Text = "";
      incomingserverpassword.Text = "";
      txtoutgoingserver.Text = "";
      txtoutgoingserveruserid.Text = "";
      outgoingserverpassword.Text = "";
      txtbusdesc.Text = "";
      txtcodesi.Text = "";
      txtcontactper.Text = "";
      lblcname.Text = "";
      ddlbuscategory.SelectedIndex = 0;
            ddlbussubcat.SelectedIndex=0;
            ddlbustype.SelectedIndex = 0;
            ddlsubsubcat.SelectedIndex = 0;
            urltext.Text = "";
    }
    protected void ddlcountry_SelectedIndexChanged(object sender, EventArgs e)
    {
      
        Fillddlstatelist();
    }
    protected void Fillddlstatelist()
    { 
        SqlCommand cmd = new SqlCommand();
        if(ddlcountry.SelectedIndex>0)
        {
        cmd.CommandText = "SELECT StateMasterTbl.StateId, StateMasterTbl.StateName, StateMasterTbl.State_Code, CountryMaster.CountryId AS CountryId, CountryMaster.CountryName, CountryMaster.Country_Code FROM StateMasterTbl LEFT OUTER JOIN  CountryMaster ON StateMasterTbl.CountryId = CountryMaster.CountryId WHERE (CountryMaster.CountryId =" + ddlcountry.SelectedValue + ")";
        
        }
        else
        {
              cmd.CommandText = "SELECT StateMasterTbl.StateId, StateMasterTbl.StateName, StateMasterTbl.State_Code, CountryMaster.CountryId AS CountryId, CountryMaster.CountryName, CountryMaster.Country_Code FROM StateMasterTbl LEFT OUTER JOIN  CountryMaster ON StateMasterTbl.CountryId = CountryMaster.CountryId";
        
        }
        cmd.CommandType = CommandType.Text;
        DataTable dt = new DataTable();
        cmd.Connection = con;
       SqlDataAdapter adp = new SqlDataAdapter(cmd);
        adp.Fill(dt);
           ddlstate.DataSource = dt;
            ddlstate.DataBind();
            ddlstate.Items.Insert(0, "-Select-");

            ddlstate.Items[0].Value = "0";
        }
    protected void Fillddlcountry()
    {
        dt = new DataTable();
        dt = SelectCountryMaster();
        ddlcountry.DataSource = dt;
        ddlcountry.DataBind();
        ddlcountry.Items.Insert(0, "-Select-");
        ddlcountry.Items[0].Value = "0";
    }
    protected DataTable SelectCountryMaster()
    {
        SqlCommand cmd = new SqlCommand();
        dt = new DataTable();
        cmd.Connection = con;
        cmd.CommandText = "SelectCountryMaster";
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        adp.Fill(dt);
        return dt;
    }
    protected void ddlbustype_SelectedIndexChanged(object sender, EventArgs e)
    {
        Businesscatmaster();
        Businesssubcatmaster();
        Businesssubsubcatmaster();
    }
    protected void ddlbuscategory_SelectedIndexChanged(object sender, EventArgs e)
    {
       // Businesscatmaster();
        Businesssubcatmaster();
        Businesssubsubcatmaster();
    }
   
    protected void ddlbussubcat_SelectedIndexChanged(object sender, EventArgs e)
    {
        Businesssubsubcatmaster();
    }
}
