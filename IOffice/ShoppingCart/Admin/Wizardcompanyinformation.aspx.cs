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

public partial class ShoppingCart_Admin_Wizardcompanyinformation : System.Web.UI.Page
{
    SqlConnection con;
    CompanyWizard clsCompany = new CompanyWizard();
    
    string newcompany;
    string stre1;
    string existingcompany;
    string onlyproduct;
    string Display;
    string onlyService;
    string both;
    // string normalsatrtdateofyear;
    // string normalstartmonthofyear;


    protected void Page_Load(object sender, EventArgs e)
    {

        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();

        Page.Title = pg.getPageTitle(page);
        //Session["pagename"] = "Wizardcompanyinformation.aspx";

        if (!IsPostBack)
        {

           // FillCountry();
            pnllogo.Visible = true;
            ware();
           // ddlwarehouse_SelectedIndexChanged(sender, e);
            LoadData();
            DataTable dtda = (DataTable)select("select Distinct Report_period_confirm.Id as conid, ReportPeriod.Report_Period_Id,Convert(nvarchar,StartDate,101) as StartDate, Convert(nvarchar,EndDate,101) as EndDate from [ReportPeriod] left join Report_period_confirm on Report_period_confirm.Report_Period_Id=ReportPeriod.Report_Period_Id where Compid = '" + Session["comid"] + "' and Whid='" + ddlbus.SelectedValue + "' and Active='1' ");
            if (dtda.Rows.Count > 0)
            {

                string fday = Convert.ToDateTime(dtda.Rows[0]["StartDate"]).Day.ToString();
                string fmonth = Convert.ToDateTime(dtda.Rows[0]["StartDate"]).Month.ToString();
                string fyear = Convert.ToDateTime(dtda.Rows[0]["StartDate"]).Year.ToString();
                lblfysdate.Text = Convert.ToString(dtda.Rows[0]["StartDate"]);
                lblfyedate.Text = Convert.ToString(dtda.Rows[0]["EndDate"]);
                for (int k = 1; k <= 31; k++)
                {
                    ddlacday.Items.Insert(k - 1, k.ToString());
                }
                int index = 0;
                ddlacday.SelectedIndex = ddlacday.Items.IndexOf(ddlacday.Items.FindByText(fday));
                ddlacday_SelectedIndexChanged(sender, e);
                ddlacmonth.SelectedIndex = ddlacmonth.Items.IndexOf(ddlacmonth.Items.FindByText(fmonth));
                for (int k = 1; k <= 50; k++)
                {
                    int cye = (DateTime.Now.Year) + 50;
                    ddlacyear.Items.Insert(index, (cye - k).ToString());

                }
                for (int k = 1; k <= 15; k++)
                {
                    int cye = DateTime.Now.Year - 15;
                    ddlacyear.Items.Insert(index, (DateTime.Now.Year - k).ToString());
                }

                ddlacyear.SelectedIndex = ddlacyear.Items.IndexOf(ddlacyear.Items.FindByText(fyear));
                if (Convert.ToString(dtda.Rows[0]["conid"]) == "")
                {
                    lblyearcacc.Text = "Select your First Accounting year for your business";

                    pnlaccy.Visible = true;
                    //lblyearcacc.Text = "Select your First Accounting year for your business";
                    lblopenaccy.Visible = false;
                    contr(true);
                }
                else
                {

                    //ddlacmonth.SelectedIndex = ddlacmonth.Items.IndexOf(ddlacmonth.Items.FindByText(DateTime.Now.Year.ToString()));
                    contr(false);
                    pnlaccy.Visible = true;
                    //      lblyearcacc.Text = "Accounting year for your business ";
                    //      lblopenaccy.Text = Convert.ToString(dtda.Rows[0]["StartDate"]) + "-" + Convert.ToString(dtda.Rows[0]["EndDate"]);
                    lblopenaccy.Visible = true;
                    //lblopenaccy.NavigateUrl = "~/ShoppingCart/Admin/AccountYearChange.aspx?Whid=" + ddlbus.SelectedValue + "";
                }
            }
        }

    }
    protected void ware()
    {
        //string str1 = "select * from WarehouseMaster where comid='" + Session["comid"] + "' and WareHouseMaster.Status='1' order by Name";

        //DataTable ds1 = new DataTable();
        //SqlDataAdapter da = new SqlDataAdapter(str1, con);
        //da.Fill(ds1);
        DataTable ds = ClsStore.SelectStorename();
        if (ds.Rows.Count > 0)
        {
            

            ddlbus.DataSource = ds;
            ddlbus.DataTextField = "Name";
            ddlbus.DataValueField = "WarehouseId";
            ddlbus.DataBind();

        }
        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlbus.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }

    }
    //protected void Fillacco()
    //{
    //    listip.Items.Clear();
    //    ddlnsm.Items.Clear();
    //    string str1 = "select Name,Report_Period_Id,convert(nvarchar,StartDate,101) as StartDate ,convert(nvarchar,EndDate,101) as EndDate,convert(nvarchar,FistStartDate,101) as FistStartDate from ReportPeriod where Compid = '" + Session["comid"] + "' and Whid='" + ddlwarehouse.SelectedValue + "' and Report_Period_Id not in (select min(Report_Period_Id)  from ReportPeriod where  Whid='" + ddlwarehouse.SelectedValue + "')  Order by Active Desc";
    //    SqlCommand cmd1 = new SqlCommand(str1);
    //    cmd1.Connection = con;
    //    SqlDataAdapter da1 = new SqlDataAdapter();
    //    da1.SelectCommand = cmd1;
    //    DataTable ds1 = new DataTable();
    //    da1.Fill(ds1);
    //    if (ds1.Rows.Count > 0)
    //    {
    //        ddlnsm.DataSource = ds1;
    //        ddlnsm.DataTextField = "Name";
    //        ddlnsm.DataValueField = "Report_Period_Id";


    //        ddlnsm.DataBind();
    //        lbldateval.Text = "Start Date of the accounting year,will not be allowed to be change in future";
    //        txtAcStartDate.Text = ds1.Rows[0]["StartDate"].ToString();
    //        txtAcEndDAte.Text = ds1.Rows[0]["EndDate"].ToString();
    //        txtFirstDate.Text = ds1.Rows[0]["FistStartDate"].ToString();
    //        foreach (DataRow dr in ds1.Rows)
    //        {
    //            listip.Items.Add(dr["Name"].ToString());
    //        }
    //        if (ds1.Rows.Count > 1)
    //        {
    //            btnchange1.Visible = false;
    //            btnup.Visible = true;
    //        }
    //        else
    //        {
    //            btnchange1.Visible = true;
    //            btnup.Visible = false;
    //        }

    //    }
    //}

    protected void controlenable(bool t)
    {
        // txtAcEndDAte.Enabled = t;
        //  txtAcStartDate.Enabled = t;
        //  txtFirstDate.Enabled = t;
        txtaddress1.Enabled = t;
        txtAddress2.Enabled = t;
        //txtcity.Enabled = t;
       
        txtcompanyname.Enabled = t;
        txtcontactpersondesi.Enabled = t;
        txtcontactpersonname.Enabled = t;
        
       
    
        txtPaypalId.Enabled = t;
     
     
      
        txtwebsite1.Enabled = t;
    
       
        // ImageButton1.Enabled = t;
        // ImageButton2.Enabled = t;
        // ImageButton3.Enabled = t;
       
     
        //DropDownList1.Enabled = t;
        
    }
    protected void LoadData()
    {
        try
        {
            DataTable dtseluseras = new DataTable();
            dtseluseras = (DataTable)select("Select * from AssociateLoginInfoTbl where UserId ='" + Session["userid"].ToString() + "'");
            if (dtseluseras.Rows.Count > 0)
            {
                //CheckBox1.Checked = true;
                // panelifile.Visible = true;
                // Textusername.Text = Convert.ToString(dtseluseras.Rows[0]["AssociatesiteUID"]);
                //Textcompanyid.Text = Convert.ToString(dtseluseras.Rows[0]["AssociateSiteCompanyId"]);
                // Textpassword.Text = Convert.ToString(dtseluseras.Rows[0]["AssociatesitePWD"]);
                // ftpurlip.Text = Convert.ToString(dtseluseras.Rows[0]["FtpUrlIp"]);
                // txtftpport.Text = Convert.ToString(dtseluseras.Rows[0]["Ftpport"]);
                // txtftpusername.Text = Convert.ToString(dtseluseras.Rows[0]["FtpUserIdUpload"]);
                // txtftppass.Text = Convert.ToString(dtseluseras.Rows[0]["FtpPasswordUpload"]);
            }
            DataTable dt = new DataTable();
            string cmpid = Session["Comid"].ToString();
            dt = clsCompany.SelectCompanyInfo(cmpid);
            if (dt.Rows.Count > 0)
            {
                //controlenable(false);
                //ImgBtnSubmit.Visible = false;
                //Imgbtnedit.Visible = true;
                ViewState["comid"] = dt.Rows[0]["CompanyId"].ToString();
                txtcompanyname.Text = dt.Rows[0]["CompanyName"].ToString();
                txtcontactpersondesi.Text = dt.Rows[0]["ContactPersonDesignation"].ToString();
                txtcontactpersonname.Text = dt.Rows[0]["ContactPersonNAme"].ToString();
                txtPaypalId.Text = dt.Rows[0]["PaypalEmailId"].ToString();
                txtwebsite1.Text = dt.Rows[0]["WebSite"].ToString();
                lblcountry.Text = dt.Rows[0]["Country"].ToString();
                //ddlcountry.SelectedIndex = ddlcountry.Items.IndexOf(ddlcountry.Items.FindByValue(dt.Rows[0]["Country"].ToString()));
                // txtAcEndDAte.Text = Convert.ToDateTime(dt.Rows[0]["YearEndingDate"]).ToShortDateString();
                // txtAcStartDate.Text = Convert.ToDateTime(dt.Rows[0]["StartDateOfAccountYear"]).ToShortDateString();
                //  txtFirstDate.Text = Convert.ToDateTime(dt.Rows[0]["FirstYearStartDate"]).ToShortDateString();
                txtaddress1.Text = dt.Rows[0]["Address1"].ToString();
                txtAddress2.Text = dt.Rows[0]["Address2"].ToString();

                lblemail.Text = dt.Rows[0]["Email"].ToString();
                lblfax.Text = dt.Rows[0]["Fax"].ToString();
                lblphone.Text = dt.Rows[0]["Phone"].ToString();
                lblpincode.Text = dt.Rows[0]["PinCode"].ToString();

                string str = "select CountryName from CountryMaster where CountryId='"+ Convert.ToInt32(dt.Rows[0]["Country"].ToString()) +"'";
                SqlCommand cmd = new SqlCommand(str,con);                
                SqlDataAdapter da = new SqlDataAdapter(cmd);                
                DataSet ds = new DataSet();
                da.Fill(ds);
                lblcountry.Text = ds.Tables[0].Rows[0]["CountryName"].ToString();
                //txtemail.Text = dt.Rows[0]["Email"].ToString();
                //txtfax.Text = dt.Rows[0]["Fax"].ToString();
                //txtIRsno.Text = dt.Rows[0]["IRSNumber"].ToString();

                //txtphone.Text = dt.Rows[0]["Phone"].ToString();
                //txtpincode.Text = dt.Rows[0]["PinCode"].ToString();
                //txtstatetaxno.Text = dt.Rows[0]["StateTaxNumber"].ToString();

                //ddlcountry.SelectedValue = dt.Rows[0]["Country"].ToString();
                string str1 = "select StateName from StateMasterTbl where StateId = '" + Convert.ToInt32(dt.Rows[0]["State"].ToString()) + "'";
                SqlCommand cmd1 = new SqlCommand(str1,con);                
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);                
                DataSet ds1 = new DataSet();
                da1.Fill(ds1);
                lblstate.Text = ds1.Tables[0].Rows[0]["StateName"].ToString();

                string str2 = "select CityName from CityMasterTbl where CityId = '" + Convert.ToInt32(dt.Rows[0]["City"].ToString()) + "'";
                SqlCommand cmd2 = new SqlCommand(str2,con);                
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);                
                DataSet ds2 = new DataSet();
                da2.Fill(ds2);
                lblcity.Text = ds2.Tables[0].Rows[0]["CityName"].ToString();
                //object sen = new object();
                //EventArgs ert = new EventArgs();
                //ddlcountry_SelectedIndexChanged(sen, ert);
                //ddlstate.SelectedIndex = ddlstate.Items.IndexOf(ddlstate.Items.FindByValue(dt.Rows[0]["State"].ToString()));
                //object sen1 = new object();
                //EventArgs ert1 = new EventArgs();
                //ddlstate_SelectedIndexChanged(sen1, ert1);
                //ddlcity.SelectedIndex = ddlcity.Items.IndexOf(ddlcity.Items.FindByValue(dt.Rows[0]["City"].ToString()));
                imgLogo.ImageUrl = "~/ShoppingCart/images/" + dt.Rows[0]["CompanyLogo"].ToString();
            }
            else
            {
                FileUpload1.Visible = true;
                 pnllogo.Visible = false;
            }
        }
        catch
        {
        }
        // SqlCommand cmd = new SqlCommand(str,con);
        lblwebsite.Text = "http://www.eplaza.com/ShoppingCart" + " /default.aspx?Cid=" + Session["comid"] ;
        lbldomain.Text = txtwebsite1.Text;
        lblcid.Text = Session["comid"].ToString();
    }
    

   

  
    //protected void imgbSubmit_Click(object sender, ImageClickEventArgs e)
    //{
    //    //Response.Redirect("WizardCompanyWebsitMaster.aspx");
    //}
    protected void imgbReset_Click(object sender, ImageClickEventArgs e)
    {
        //controlenable(true);

        //txtcompanyname.Text = "";
        //txtaddress1.Text = "";
        //txtAddress2.Text = "";
       
        //txtcontactpersonname.Text = "";
        //txtcontactpersondesi.Text = "";
       
        //txtwebsite1.Text = "";
        //txtPaypalId.Text = "";
       
        // DropDownList1.SelectedIndex = 0;
        //  panelifile.Visible = false;

      

    }
    

    protected void ImgBtnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        //if (Convert.ToDateTime(txtFirstDate.Text) < Convert.ToDateTime(txtAcStartDate.Text))
        //{
        //    lblmsg.Text = "";
        //    lblmsg.Text = "Account year startdate cannot be greater than First year startdate";
        //}
        //else
        //{
        //    string logoname = FileUpload1.FileName;

        //    //**** Radhika Solanki
        //    //int i = clsCompany.CompanyMaster(txtcompanyname.Text, txtstatetaxno.Text, txtIRsno.Text, Convert.ToDateTime(txtAcEndDAte.Text), Convert.ToDateTime(txtFirstDate.Text), Convert.ToDateTime(txtAcStartDate.Text), txtPaypalId.Text, logoname.ToString(), txtcontactpersonname.Text, txtaddress1.Text, txtAddress2.Text, txtphone.Text, ddlcountry.SelectedValue, ddlcity.SelectedValue, ddlstate.SelectedValue, txtpincode.Text, txtemail.Text, txtfax.Text, txtcontactpersondesi.Text, txtwebsite1.Text);

        //    //**********
        //    string cmpid = Session["Comid"].ToString();
        //    int i = clsCompany.CompanyMaster(txtcompanyname.Text, txtstatetaxno.Text, txtIRsno.Text, Convert.ToDateTime(txtAcEndDAte.Text), Convert.ToDateTime(txtFirstDate.Text), Convert.ToDateTime(txtAcStartDate.Text), txtPaypalId.Text, logoname.ToString(), txtcontactpersonname.Text, txtaddress1.Text, txtAddress2.Text, txtphone.Text, ddlcountry.SelectedValue, ddlcity.SelectedValue, ddlstate.SelectedValue, txtpincode.Text, txtemail.Text, txtfax.Text, txtcontactpersondesi.Text, txtwebsite1.Text, cmpid);
        //    if (i == 1)
        //    {
        //        lblmsg.Text = "";
        //        lblmsg.Text = "Record Added Successfully";
        //    }
        //    LoadData();
        //}

    }
    protected void imgBtnImageUpdate_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            try
            {
                FileUpload1.SaveAs(Server.MapPath("~/ShoppingCart/images/") + FileUpload1.FileName.ToString());
                string logoname = FileUpload1.FileName;
                //int i = clsCompany1.LOGOUPDATE_CompanyMaster(logoname.ToString());

                LoadData();
                //************CHNAGES codes
                con.Close();
                con.Open();

                //*************Radhika Chnages
                //  string updatelogoname = "update CompanyMaster set CompanyLogo='" + logoname + "' where CompanyId='" + ViewState["comid"].ToString() + "' ";
                //*************************
                string updatelogoname = "update CompanyMaster set CompanyLogo='" + logoname + "' where CompanyId='" + ViewState["comid"].ToString() + "' and Compid='" + Session["Comid"].ToString() + "' ";

                SqlCommand cmdlogo = new SqlCommand(updatelogoname, con);
                cmdlogo.ExecuteNonQuery();
                con.Close();


                //*************


                imgLogo.ImageUrl = "~/ShoppingCart/images/" + FileUpload1.FileName.ToString();
                pnllogo.Visible = true;
                FileUpload1.Visible = false;
                pnllogo.Visible = true;
                //btnChange.Visible = false;
                imgBtnImageUpdate.Visible = false;
                ImgBtncancel.Visible = false;
                Label1.Visible = false;
            }
            catch
            {

            }
        }

    }
    protected void Imgbtnedit_Click(object sender, ImageClickEventArgs e)
    {
        //Imgbtnedit.Visible = false;
        //Imgbtnupdate.Visible = true;
        //controlenable(true);
    }
    protected void Imgbtnupdate_Click(object sender, ImageClickEventArgs e)
    {

        //if (Convert.ToDateTime(txtFirstDate.Text) < Convert.ToDateTime(txtAcStartDate.Text))
        //{
        //    lblmsg.Text = "";
        //    lblmsg.Text = "Account year startdate cannot be greater than First year startdate";
        //}
        //else
        //{

        //    if (RadioButtonList1.SelectedIndex == 0)
        //    {
        //        newcompany = "True";
        //        existingcompany = "False";
        //    }
        //    else if (RadioButtonList1.SelectedIndex == 1)
        //    {
        //        existingcompany = "True";
        //        newcompany = "False";
        //    }
        //    if (RadioButtonList2.SelectedIndex == 0)
        //    {
        //        onlyproduct = "True";
        //        onlyService = "False";
        //        both = "False";
        //    }
        //    else if (RadioButtonList2.SelectedIndex == 1)
        //    {
        //        onlyproduct = "False";
        //        onlyService = "True";
        //        both = "False";
        //        Display = "Service";
        //    }
        //    else if (RadioButtonList2.SelectedIndex == 2)
        //    {
        //        onlyproduct = "False";
        //        onlyService = "False";
        //        both = "True";
        //    }
        //    if (both == "True")
        //    {
        //        if (RadioButtonList3.SelectedIndex == 0)
        //        {
        //            Display = "Products";

        //        }
        //        else if (RadioButtonList3.SelectedIndex == 1)
        //        {
        //            Display = "Service";

        //        }
        //    }
        //    stre1 = "select * from CompanyBusinessInfo Where comid='" + Session["Comid"] + "'";
        //    SqlCommand cmde = new SqlCommand(stre1, con);
        //    SqlDataAdapter adpe = new SqlDataAdapter(cmde);
        //    DataTable dte = new DataTable();
        //    adpe.Fill(dte);
        //    string ins;
        //    if (dte.Rows.Count > 0)
        //    {
        //        ins = "Update CompanyBusinessInfo Set newcompany='" + newcompany + "',existingcompany='" + existingcompany + "',onlyproduct='" + onlyproduct + "',onlyService='" + onlyService + "',both='" + both + "',normalsatrtdateofyear='" + ddlnsm.SelectedItem.Text + "',normalstartmonthofyear='4',Display='" + Display + "' Where  comid='" + Session["comid"] + "'";


        //    }
        //    else
        //    {
        //        ins = "Insert into CompanyBusinessInfo(comid,newcompany,existingcompany,onlyproduct,onlyService,both,normalsatrtdateofyear,normalstartmonthofyear,Display) Values " +
        //             "('" + Session["comid"] + "','" + newcompany + "','" + existingcompany + "','" + onlyproduct + "','" + onlyService + "','" + both + "','" + ddlnsm.SelectedItem.Text + "','4','" + Display + "')";

        //    }
        //    SqlCommand cmdin = new SqlCommand(ins, con);
        //    if (con.State.ToString() == "Open")
        //    {
        //        con.Close();
        //    }
        //    con.Open();
        //    cmdin.ExecuteNonQuery();
        //    con.Close();
        //    string cnid = Session["Comid"].ToString();
        //    int u = clsCompany.updatecompanymaster(txtcompanyname.Text, txtstatetaxno.Text, txtIRsno.Text, Convert.ToDateTime(txtAcEndDAte.Text), Convert.ToDateTime(txtFirstDate.Text), Convert.ToDateTime(txtAcStartDate.Text), txtPaypalId.Text, txtcontactpersonname.Text, txtaddress1.Text, txtAddress2.Text, txtphone.Text, ddlcountry.SelectedValue, ddlcity.SelectedValue, ddlstate.SelectedValue, txtpincode.Text, txtemail.Text, txtfax.Text, txtcontactpersondesi.Text, txtwebsite1.Text, ViewState["comid"].ToString(), cnid);
        //    if (u == 1)
        //    {
        //        lblmsg.Text = "Record Updated Successfully";
        //        Imgbtnupdate.Visible = false;
        //        controlenable(false);
        //        Imgbtnedit.Visible = true;

        //    }


        //}
        //string insert = "";
        //DataTable dtseluseras = new DataTable();
        //dtseluseras = (DataTable)select("Select * from AssociateLoginInfoTbl where UserId ='" + Session["userid"].ToString() + "'");
        //if (dtseluseras.Rows.Count <= 0)
        //{


        //    //insert = "Update ifilecabiientinfo Set companyIDifile='" + Textcompanyid.Text.Trim() + "',username='" + Textusername.Text.Trim() + "',password='" + Textpassword.Text.Trim() + "' where CID='" + Session["comid"] + "'";
        //    insert = "insert into AssociateLoginInfoTbl(CID,Associateadminlogininfoid,AssociatesiteUID,AssociatesitePWD,UserId,AssociateSiteCompanyId,AssociatesiteId,FtpUrlIp,Ftpport,FtpUserIdUpload,FtpPasswordUpload)values('" + Session["comid"] + "','" + Session["userid"] + "','" + Textusername.Text.Trim() + "','" + Textpassword.Text.Trim() + "','" + Session["userid"] + "','" + Textcompanyid.Text.Trim() + "','4','" + ftpurlip.Text + "','" + txtftpport.Text + "',,'" + txtftpusername.Text + "','" + txtftppass.Text + "')";

        //}
        //else
        //{
        //    //insert = "insert into ifilecabiientinfo(companyIDifile,username,password,CID)values('" + Textcompanyid.Text.Trim() + "','" + Textusername.Text.Trim() + "','" + Textpassword.Text.Trim() + "','" + Session["comid"] + "')";
        //    insert = "Update AssociateLoginInfoTbl set AssociatesiteUID='" + Textusername.Text.Trim() + "',AssociatesitePWD='" + Textpassword.Text.Trim() + "',AssociateSiteCompanyId='" + Textcompanyid.Text.Trim() + "',FtpUrlIp='" + ftpurlip.Text + "',Ftpport='" + txtftpport.Text + "',FtpUserIdUpload='" + txtftpusername.Text + "',FtpPasswordUpload='" + txtftppass.Text + "' Where UserId='" + Session["userid"] + "'";

        //}
        //if (con.State.ToString() == "Open")
        //{
        //    con.Close();
        //}
        //con.Open();
        //SqlCommand cmd = new SqlCommand(insert, con);
        //cmd.ExecuteNonQuery();
        //con.Close();
    }
   
    protected void ImgBtncancel_Click(object sender, EventArgs e)
    {
        FileUpload1.Visible = false;
        RequiredFieldValidator11.Visible = false;
        pnllogo.Visible = true;
        //btnChange.Visible = false;
        imgBtnImageUpdate.Visible = false;
        ImgBtncancel.Visible = false;
        Label1.Visible = false;
    }
    //protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (CheckBox1.Checked == true)
    //    {
    //        panelifile.Visible = true;
    //        //panelifile1.Visible = true;
    //    }
    //    else
    //    {
    //        panelifile.Visible = false;
    //        // panelifile1.Visible = false;
    //        Textusername.Text = "";
    //        Textcompanyid.Text = "";
    //        Textpassword.Text = "";
    //        ftpurlip.Text = "";
    //        txtftpport.Text = "";
    //        txtftpusername.Text = "";
    //        txtftppass.Text = "";
    //    }
    //}

    protected void lblacc_Click(object sender, EventArgs e)
    {

    }
    protected void RadioButtonList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList2.SelectedIndex == 2)
        {
            RadioButtonList3.Visible = true;
            lblboth.Visible = true;
        }
        else
        {
            RadioButtonList3.Visible = false;
            lblboth.Visible = false;
        }
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        //int dd = Convert.ToInt32(DateTime.Now.Year.ToString());

        //int d1 = dd - 1;
        //SqlCommand cmdd = new SqlCommand("Insert into ReportPeriod(Name,StartDate,EndDate,FistStartDate,Active,Compid,Whid)Values('" + d1 + " TO " + dd + "','','3/31/" + dd + "','','0','" + Session["comid"] + "','" + Convert.ToInt32(ddlwarehouse.SelectedValue) + "') ", con);

        //con.Open();
        //cmdd.ExecuteNonQuery();
        //con.Close();
        //SqlCommand cmsa11 = new SqlCommand("SELECT max(Report_Period_Id) from ReportPeriod where Compid='" + Session["comid"] + "' and Whid='" + Convert.ToInt32(ddlwarehouse.SelectedValue) + "'", con);
        //con.Open();
        //object xde21 = cmsa11.ExecuteScalar();
        //con.Close();
        //string str5t1 = "select Id from AccountMaster where Whid='" + ddlwarehouse.SelectedValue.ToString() + "'";
        //SqlCommand cmd32t1 = new SqlCommand(str5t1, con);
        //SqlDataAdapter adp15t1 = new SqlDataAdapter(cmd32t1);
        //DataTable dtlogin14t1 = new DataTable();
        //adp15t1.Fill(dtlogin14t1);
        //foreach (DataRow dr in dtlogin14t1.Rows)
        //{
        //    SqlCommand cmdtr13 = new SqlCommand("Insert into AccountBalance(AccountMasterId,Balance,Report_Period_Id)Values('" + dr["Id"] + "','0','" + xde21 + "') ", con);

        //    con.Open();
        //    cmdtr13.ExecuteNonQuery();
        //    con.Close();
        //}

        //int ddt11 = Convert.ToInt32(DateTime.Now.Year.ToString());
        //int dde11 = ddt11 + 1;
        //SqlCommand cmdtr2 = new SqlCommand("Insert into ReportPeriod(Name,StartDate,EndDate,FistStartDate,Active,Compid,Whid)Values('" + ddt11 + " TO " + dde11 + "','4/1/" + ddt11 + "','3/31/" + dde11 + "','" + DateTime.Now.ToShortDateString() + "','1','" + Session["comid"] + "','" + ddlwarehouse.SelectedValue + "') ", con);

        //con.Open();
        //cmdtr2.ExecuteNonQuery();
        //con.Close();
        //SqlCommand cmsa1 = new SqlCommand("SELECT max(Report_Period_Id) from ReportPeriod where Compid='" + Session["comid"] + "' and Whid='" + ddlwarehouse.SelectedValue + "'", con);
        //con.Open();
        //object xde2 = cmsa1.ExecuteScalar();
        //con.Close();
        //string str5t = "select Id from AccountMaster where Whid='" + ddlwarehouse.SelectedValue + "'";
        //SqlCommand cmd32t = new SqlCommand(str5t, con);
        //SqlDataAdapter adp15t = new SqlDataAdapter(cmd32t);
        //DataTable dtlogin14t = new DataTable();
        //adp15t.Fill(dtlogin14t);
        //foreach (DataRow dr in dtlogin14t.Rows)
        //{
        //    SqlCommand cmdtr13 = new SqlCommand("Insert into AccountBalance(AccountMasterId,Balance,Report_Period_Id)Values('" + dr["Id"] + "','0','" + xde2 + "') ", con);

        //    con.Open();
        //    cmdtr13.ExecuteNonQuery();
        //    con.Close();
        //}


        //string str1 = "select Name,Report_Period_Id,StartDate,EndDate from ReportPeriod where Compid = '" + Session["comid"] + "' and Whid='" + ddlwarehouse.SelectedValue + "' order by Report_Period_Id Desc";
        //SqlCommand cmd1 = new SqlCommand(str1);
        //cmd1.Connection = con;
        //SqlDataAdapter da1 = new SqlDataAdapter();
        //da1.SelectCommand = cmd1;
        //DataTable ds1 = new DataTable();
        //da1.Fill(ds1);

        //if (ds1.Rows.Count >= 1)
        //{

        //    string[] separator1 = new string[] { " TO " };
        //    string[] strSplitArr1 = ds1.Rows[0]["Name"].ToString().Split(separator1, StringSplitOptions.RemoveEmptyEntries);
        //    int i111 = Convert.ToInt32(strSplitArr1.Length);
        //    if (i111 == 2)
        //    {
        //        int fr = Convert.ToInt32(strSplitArr1[0].ToString());
        //        int sec = Convert.ToInt32(strSplitArr1[1].ToString());


        //        int ddt1 = fr + 1;
        //        int dde1 = sec + 1;
        //        string addsdate = Convert.ToDateTime(ds1.Rows[0]["StartDate"]).AddMonths(12).ToShortDateString();
        //        string addedate = Convert.ToDateTime(ds1.Rows[0]["EndDate"]).AddMonths(12).ToShortDateString();
        //        SqlCommand cmdtr = new SqlCommand("Insert into ReportPeriod(Name,StartDate,EndDate,FistStartDate,Active,Compid,Whid)Values('" + ddt1 + " TO " + dde1 + "','" + addsdate + "','" + addedate + "','" + DateTime.Now.ToShortDateString() + "','0','" + Session["comid"] + "','" + ddlwarehouse.SelectedValue + "') ", con);
        //        if (con.State.ToString() != "Open")
        //        {
        //            con.Open();
        //        }
        //        cmdtr.ExecuteNonQuery();
        //        con.Close();
        //        SqlCommand cmsa = new SqlCommand("SELECT max(Report_Period_Id) from ReportPeriod where Compid='" + Session["comid"] + "' and Whid='" + ddlwarehouse.SelectedValue + "'", con);
        //        con.Open();
        //        object xde = cmsa.ExecuteScalar();
        //        con.Close();
        //        string str5t = "select Id from AccountMaster where Whid='" + ddlwarehouse.SelectedValue + "'";
        //        SqlCommand cmd32t = new SqlCommand(str5t, con);
        //        SqlDataAdapter adp15t = new SqlDataAdapter(cmd32t);
        //        DataTable dtlogin14t = new DataTable();
        //        adp15t.Fill(dtlogin14t);
        //        foreach (DataRow dr in dtlogin14t.Rows)
        //        {
        //            SqlCommand cmdtr1 = new SqlCommand("Insert into AccountBalance(AccountMasterId,Balance,Report_Period_Id)Values('" + dr["Id"] + "','0','" + xde + "') ", con);

        //            con.Open();
        //            cmdtr1.ExecuteNonQuery();
        //            con.Close();
        //        }
        //        listip.Items.Add(ddt1 + " TO " + dde1);
        //        btnup.Visible = true;
        //        lblmsg.Text = "Add Account Year Sucessfully";
        //        lblmsg.Visible = true;
        //    }
        //}
    }
    //protected void btnchange1_Click(object sender, EventArgs e)
    //{
    //    DateTime accounty = Convert.ToDateTime(txtAcStartDate.Text);
    //    DateTime fstar = Convert.ToDateTime(txtFirstDate.Text);
    //    if (accounty <= fstar)
    //    {

    //        if (btnchange1.Text == "Change")
    //        {
    //            txtAcStartDate.Enabled = true;
    //            ImageButton1.Enabled = true;
    //            btnchange1.Text = "Calculate";
    //        }
    //        else if (btnchange1.Text == "Calculate")
    //        {

    //            DateTime daaa = Convert.ToDateTime(txtAcStartDate.Text).AddDays(364);
    //            txtAcEndDAte.Text = daaa.ToShortDateString();
    //            btnchange1.Text = "Change";
    //            btnup.Visible = true;
    //            btnup.Text = "Update";

    //        }
    //    }
    //    else
    //    {
    //        lblmsg.Text = "Account Year Start Date Less then first year start date";
    //        lblmsg.Visible = true;
    //    }


    //}
    //protected void btnup_Click(object sender, EventArgs e)
    //{
    //    if (listip.SelectedIndex != -1)
    //    {
    //        if (btnup.Text == "Edit")
    //        {
    //            string str1 = "select Name,Report_Period_Id,StartDate,EndDate,FistStartDate from ReportPeriod where Compid = '" + Session["comid"] + "' and Whid='" + ddlwarehouse.SelectedValue + "' and Name='" + listip.SelectedItem.Text + "'";
    //            SqlCommand cmd1 = new SqlCommand(str1);
    //            cmd1.Connection = con;
    //            SqlDataAdapter da1 = new SqlDataAdapter();
    //            da1.SelectCommand = cmd1;
    //            DataTable ds1 = new DataTable();
    //            da1.Fill(ds1);
    //            if (ds1.Rows.Count > 0)
    //            {
    //                ddlnsm.SelectedIndex = ddlnsm.Items.IndexOf(ddlnsm.Items.FindByValue(ds1.Rows[0]["Report_Period_Id"].ToString()));
    //                txtAcStartDate.Text = Convert.ToDateTime(ds1.Rows[0]["StartDate"]).ToShortDateString();
    //                txtAcEndDAte.Text = Convert.ToDateTime(ds1.Rows[0]["EndDate"]).ToShortDateString();
    //                txtFirstDate.Text = Convert.ToDateTime(ds1.Rows[0]["FistStartDate"]).ToShortDateString();
    //                btnup.Text = "Active";
    //                btnadd.Visible = false;
    //            }
    //        }

    //        else if (btnup.Text == "Active")
    //        {
    //            fillamount();
    //            SqlCommand cmdtr = new SqlCommand("Update ReportPeriod set StartDate='" + txtAcStartDate.Text + "',EndDate='" + txtAcEndDAte.Text + "',Active='1' where  Name='" + ddlnsm.SelectedItem.Text + "' and Compid= '" + Session["comid"] + "' and Whid='" + ddlwarehouse.SelectedValue + "'  ", con);

    //            con.Open();
    //            cmdtr.ExecuteNonQuery();
    //            con.Close();

    //            btnup.Text = "Edit";
    //            btnup.Visible = true;
    //            btnadd.Visible = true;
    //            Fillacco();
    //            lblmsg.Text = "Change Active Account Year Sucessfully";
    //            lblmsg.Visible = true;
    //        }
    //    }
    //    if (btnup.Text == "Update")
    //    {

    //        string stary = Convert.ToDateTime(txtAcStartDate.Text).Year.ToString();
    //        string enduea = (Convert.ToInt32(stary) + 1).ToString();
    //        string aaa = stary + " TO " + enduea;
    //        string staryS = (Convert.ToInt32(stary) - 1).ToString();
    //        string endueaS = (Convert.ToInt32(enduea) - 1).ToString();
    //        string aaaS = staryS + " TO " + endueaS;
    //        //enduea=  "3/31/" + enduea  ;
    //        //stary = "4/01/" + stary;
    //        enduea = Convert.ToDateTime(txtAcEndDAte.Text).ToShortDateString();
    //        stary = Convert.ToDateTime(txtAcStartDate.Text).ToShortDateString();
    //        SqlCommand cmdtrw = new SqlCommand("Update ReportPeriod set Name='" + aaaS + "' where Report_Period_Id=(Select Top(1) Report_Period_Id from ReportPeriod WHERE Active<>'1' AND  Whid='" + ddlwarehouse.SelectedValue + "')", con);
    //        if (con.State.ToString() != "Open")
    //        {
    //            con.Open();
    //        }
    //        cmdtrw.ExecuteNonQuery();
    //        con.Close();
    //        SqlCommand cmdtr = new SqlCommand("Update ReportPeriod set StartDate='" + stary + "', EndDate='" + enduea + "',Name='" + aaa + "'  where  Name='" + ddlnsm.SelectedItem.Text + "' and Compid= '" + Session["comid"] + "' and Whid='" + ddlwarehouse.SelectedValue + "'  ", con);

    //        con.Open();
    //        cmdtr.ExecuteNonQuery();
    //        con.Close();
    //        btnup.Text = "Edit";

    //        btnup.Visible = false;
    //    }


    //}
    protected DataTable select(string str)
    {
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);

        return dt;

    }
    protected void addtxformula(string emp, string year)
    {
        string str = "Insert Into TaxFormulaVariableTbl(EmployeeID,TaxYearId,A,A1,B,B1,C,D,D1,E,E1,F,F1,F2,F3,F4,G,HD,I,I1,IE,K,KP,K1,K1P,K2,K2P,K2Q,K3,K3P,K4,K4P,L,LCF,LCP,M,M1,P,P1,PR,R,S,S1,T,T1,T2,T3,T4,TB,TC,TCP,U1,V,V1,V2,Y,YTD,T)values('" + emp + "','" + year + "','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0')";
        SqlCommand cmd = new SqlCommand(str, con);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
    }
    protected void fillamount()
    {


        ////  double opnbal1 = 0;
        //string str1 = "select Whid, Name,Report_Period_Id,Cast(StartDate as Date) as StartDate,Cast(EndDate as Date)  as EndDate,FistStartDate,Name from ReportPeriod where Compid = '" + Session["comid"] + "' and Whid='" + ddlwarehouse.SelectedValue + "' and Active='1' order by Report_Period_Id Desc";
        //SqlCommand cmd12 = new SqlCommand(str1);
        //cmd12.Connection = con;
        //SqlDataAdapter da12 = new SqlDataAdapter();
        //da12.SelectCommand = cmd12;
        //DataTable ds12 = new DataTable();
        //da12.Fill(ds12);
        ////string bal_cal = "";
        //string str145 = "select ReportPeriod.Report_Period_Id from ReportPeriod  where Compid = '" + Session["comid"] + "' and Whid='" + ddlwarehouse.SelectedValue + "' and ReportPeriod.Report_Period_Id<'" + ds12.Rows[0]["Report_Period_Id"] + "' order by ReportPeriod.Report_Period_Id Desc ";
        //SqlCommand cmd121 = new SqlCommand(str145);
        //cmd121.Connection = con;
        //SqlDataAdapter da121 = new SqlDataAdapter();
        //da121.SelectCommand = cmd121;
        //DataTable ds121 = new DataTable();
        //da121.Fill(ds121);
        //int k = 0;
        //if (ds121.Rows.Count > 1)
        //{
        //    // opnbal1 = Convert.ToDouble(opnbal1) + (Convert.ToDouble(ds121.Rows[0]["Balance"].ToString()));
        //    // bal_cal = opnbal1.ToString();
        //}
        //else
        //{
        //    k += 1;
        //}
        //SqlCommand cmddeltm = new SqlCommand("delete TranctionMaster where [Tranction_Master_Id] in(Select Trans_Id from TemporyDataTbl where periodId='" + ds12.Rows[0]["Report_Period_Id"] + "') ", con);
        //if (con.State.ToString() != "Open")
        //{
        //    con.Open();
        //}
        //cmddeltm.ExecuteNonQuery();

        //con.Close();
        //SqlCommand cmddeltd = new SqlCommand("delete [Tranction_Details] where [Tranction_Master_Id] in(Select Trans_Id from TemporyDataTbl where periodId='" + ds12.Rows[0]["Report_Period_Id"] + "') ", con);

        //con.Open();
        //cmddeltd.ExecuteNonQuery();

        //con.Close();
        //SqlCommand cmddelte = new SqlCommand("delete  from TemporyDataTbl where periodId='" + ds12.Rows[0]["Report_Period_Id"] + "' ", con);

        //con.Open();
        //cmddelte.ExecuteNonQuery();

        //con.Close();
        //DataTable dtaa = new DataTable();
        //dtaa = (DataTable)select("Select Id from AccountMaster where Id not in(select AccountMasterId  from AccountBalance where Report_Period_Id='" + ds12.Rows[0]["Report_Period_Id"] + "')  and Whid='" + ddlwarehouse.SelectedValue + "'");
        //if (dtaa.Rows.Count > 0)
        //{
        //    SqlCommand cmdtr11 = new SqlCommand("Insert into AccountBalance(AccountMasterId,Balance,Report_Period_Id)Values('" + dtaa.Rows[0]["Id"] + "','0','" + ds121.Rows[0]["Report_Period_Id"] + "') ", con);

        //    con.Open();
        //    cmdtr11.ExecuteNonQuery();
        //    con.Close();
        //    SqlCommand cmdtr1 = new SqlCommand("Insert into AccountBalance(AccountMasterId,Balance,Report_Period_Id)Values('" + dtaa.Rows[0]["Id"] + "','0','" + ds12.Rows[0]["Report_Period_Id"] + "') ", con);

        //    con.Open();
        //    cmdtr1.ExecuteNonQuery();
        //    con.Close();
        //}

        //if (ds12.Rows.Count > 0)
        //{




        //    DataTable dtamtc = new DataTable();
        //    dtamtc = (DataTable)select("Select  AccountMaster.AccountId,AccountBalance.Balance,AccountBalance.Account_Balance_Id from ReportPeriod inner join AccountBalance on AccountBalance.Report_Period_Id=ReportPeriod.Report_Period_Id inner join AccountMaster on AccountMaster.Id=AccountBalance.AccountMasterId  where ReportPeriod.Report_Period_Id='" + ds12.Rows[0]["Report_Period_Id"] + "'");

        //    foreach (DataRow gdr5 in dtamtc.Rows)
        //    {
        //        double totcrdit = 0;
        //        double totdebit = 0;
        //        double balance = 0;
        //        DataTable dtcredit = new DataTable();
        //        dtcredit = (DataTable)select("Select sum(AmountCredit) as AmountCredit from Tranction_Details inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=Tranction_Details.Tranction_Master_Id where Tranction_Details.Whid='" + ds12.Rows[0]["Whid"] + "' and TranctionMaster.Whid='" + ds12.Rows[0]["Whid"] + "' and AccountCredit='" + gdr5["AccountId"] + "' and DateTimeOfTransaction between '" + Convert.ToDateTime(ds12.Rows[0]["StartDate"]) + "' and '" + Convert.ToDateTime(ds12.Rows[0]["EndDate"]) + "'");
        //        if (dtcredit.Rows.Count > 0)
        //        {
        //            if (dtcredit.Rows[0]["AmountCredit"].ToString() != "")
        //            {

        //                totcrdit = Convert.ToDouble(dtcredit.Rows[0]["AmountCredit"].ToString());

        //            }
        //        }
        //        DataTable dtamtd1 = new DataTable();
        //        dtamtd1 = (DataTable)select("Select sum(AmountDebit) as AmountDebit from Tranction_Details inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=Tranction_Details.Tranction_Master_Id where Tranction_Details.Whid='" + ds12.Rows[0]["Whid"] + "' and TranctionMaster.Whid='" + ds12.Rows[0]["Whid"] + "' and AccountDebit='" + gdr5["AccountId"] + "' and DateTimeOfTransaction between '" + Convert.ToDateTime(ds12.Rows[0]["StartDate"]) + "' and '" + Convert.ToDateTime(ds12.Rows[0]["EndDate"]) + "' ");
        //        if (dtamtd1.Rows.Count > 0)
        //        {
        //            if (dtamtd1.Rows[0]["AmountDebit"].ToString() != "")
        //            {

        //                totdebit = Convert.ToDouble(dtamtd1.Rows[0]["AmountDebit"].ToString());
        //            }
        //        }
        //        if (k > 0)
        //        {
        //            DataTable dta = new DataTable();
        //            dta = (DataTable)select("Select  AccountMaster.AccountId,AccountBalance.Balance,AccountBalance.Account_Balance_Id from ReportPeriod inner join AccountBalance on AccountBalance.Report_Period_Id=ReportPeriod.Report_Period_Id inner join AccountMaster on AccountMaster.Id=AccountBalance.AccountMasterId  where AccountId='" + gdr5["AccountId"] + "' and ReportPeriod.Report_Period_Id='" + ds121.Rows[0]["Report_Period_Id"] + "'");
        //            if (dta.Rows.Count > 0)
        //            {

        //                if (dta.Rows[0]["Balance"].ToString() == "")
        //                {
        //                    balance = totdebit - totcrdit;
        //                }
        //                else
        //                {
        //                    balance = (Convert.ToDouble(dta.Rows[0]["Balance"]) + totdebit - totcrdit);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            balance = totdebit - totcrdit;
        //        }
        //        SqlCommand cmdtr1 = new SqlCommand("Update AccountBalance Set Balance='" + balance + "' where Account_Balance_Id='" + gdr5["Account_Balance_Id"] + "' ", con);

        //        con.Open();
        //        cmdtr1.ExecuteNonQuery();
        //        con.Close();
        //    }
        //    double revdif = 0;

        //    DataTable dttotac = new DataTable();
        //    dttotac = (DataTable)select("select distinct AccountId from ClassMaster inner join GroupMaster on GroupMaster.ClassId=ClassMaster.ClassId inner join  AccountMaster on AccountMaster.GroupId=GroupMaster.GroupId inner join Tranction_Details on " +
        //    "(Tranction_Details.AccountDebit=AccountMaster.AccountId or Tranction_Details.AccountCredit=AccountMaster.AccountId ) where  Tranction_Details.whid='" + ds12.Rows[0]["Whid"] + "' " +
        //    "and  AccountMaster.Whid='" + ds12.Rows[0]["Whid"] + "' and   AccountMaster.compid='" + Session["comid"] + "'  and ClassMaster.ClassTypeId In('15','19')");
        //    if (dttotac.Rows.Count > 0)
        //    {
        //        int jno = 0;
        //        DataTable dtj = new DataTable();
        //        dtj = (DataTable)select("  Select Max(EntryNumber) as EntryNumber from TranctionMaster where EntryTypeId='3' and Whid='" + ddlwarehouse.SelectedValue + "'");
        //        if (dtj.Rows.Count > 0)
        //        {
        //            if (Convert.ToString(dtj.Rows[0]["EntryNumber"]) != "")
        //            {
        //                jno = Convert.ToInt32(dtj.Rows[0]["EntryNumber"]);
        //            }
        //        }
        //        foreach (DataRow dts in dttotac.Rows)
        //        {
        //            double actotcr = 0;
        //            double actotde = 0;

        //            double crdif = 0;
        //            double dedif = 0;
        //            DataTable dtsdebit = new DataTable();
        //            dtsdebit = (DataTable)select(" select sum(AmountDebit) as AmountDebit from ClassMaster inner join GroupMaster on GroupMaster.ClassId=ClassMaster.ClassId inner join  AccountMaster on AccountMaster.GroupId=GroupMaster.GroupId inner join Tranction_Details on " +
        //            "Tranction_Details.AccountDebit=AccountMaster.AccountId  where  Tranction_Details.whid='" + ds12.Rows[0]["Whid"] + "' " +
        //           " and  AccountMaster.Whid='" + ds12.Rows[0]["Whid"] + "' and   AccountMaster.compid='" + Session["comid"] + "'  and ClassMaster.ClassTypeId In('15','19') and AccountDebit='" + dts["AccountId"] + "' and DateTimeOfTransaction between '" + Convert.ToDateTime(ds12.Rows[0]["StartDate"]) + "' and '" + Convert.ToDateTime(ds12.Rows[0]["EndDate"]) + "'");

        //            if (dtsdebit.Rows[0]["AmountDebit"].ToString() != "")
        //            {

        //                actotde = Convert.ToDouble(dtsdebit.Rows[0]["AmountDebit"].ToString());

        //            }

        //            DataTable dtscredit = new DataTable();
        //            dtscredit = (DataTable)select(" select sum(AmountCredit) as AmountCredit from ClassMaster inner join GroupMaster on GroupMaster.ClassId=ClassMaster.ClassId inner join  AccountMaster on AccountMaster.GroupId=GroupMaster.GroupId inner join Tranction_Details on " +
        //            "Tranction_Details.AccountCredit=AccountMaster.AccountId  where  Tranction_Details.whid='" + ds12.Rows[0]["Whid"] + "' " +
        //           " and  AccountMaster.Whid='" + ds12.Rows[0]["Whid"] + "' and   AccountMaster.compid='" + Session["comid"] + "'  and ClassMaster.ClassTypeId In('15','19') and AccountCredit='" + dts["AccountId"] + "' and DateTimeOfTransaction between '" + Convert.ToDateTime(ds12.Rows[0]["StartDate"]) + "' and '" + Convert.ToDateTime(ds12.Rows[0]["EndDate"]) + "'");


        //            if (dtscredit.Rows[0]["AmountCredit"].ToString() != "")
        //            {

        //                actotcr = Convert.ToDouble(dtscredit.Rows[0]["AmountCredit"].ToString());
        //            }
        //            if (actotde > actotcr)
        //            {

        //                dedif = actotde - actotcr;
        //                revdif += dedif;
        //                jno += 1;
        //                SqlCommand cmdtr11 = new SqlCommand("Insert into TranctionMaster(Date,EntryNumber,EntryTypeId,UserId,Tranction_Amount,compid,Whid)Values('" + DateTime.Now.ToShortDateString() + "','" + jno + "','3','" + Session["userid"] + "','" + dedif + "','" + Session["comid"] + "','" + ds12.Rows[0]["Whid"] + "') ", con);

        //                con.Open();
        //                cmdtr11.ExecuteNonQuery();

        //                con.Close();
        //                DataTable dttrid = new DataTable();
        //                dttrid = (DataTable)select("Select  Max(Tranction_Master_Id) as Tranction_Master_Id from TranctionMaster  where Whid='" + ds12.Rows[0]["Whid"] + "'");
        //                if (dttrid.Rows.Count > 0)
        //                {
        //                    SqlCommand cmdtrd = new SqlCommand("Insert into Tranction_Details(AccountDebit,AccountCredit,AmountDebit,AmountCredit,Tranction_Master_Id,Memo,DateTimeOfTransaction,compid,whid)Values('0','" + dts["AccountId"] + "','0','" + dedif + "','" + dttrid.Rows[0]["Tranction_Master_Id"] + "','ClosingEntry','" + DateTime.Now.ToShortDateString() + "','" + Session["comid"] + "','" + ds12.Rows[0]["Whid"] + "') ", con);

        //                    con.Open();
        //                    cmdtrd.ExecuteNonQuery();
        //                    con.Close();
        //                }
        //                SqlCommand cmdtem = new SqlCommand("Insert into TemporyDataTbl(Trans_Id,periodId)Values('" + dttrid.Rows[0]["Tranction_Master_Id"] + "','" + ds12.Rows[0]["Report_Period_Id"] + "') ", con);

        //                con.Open();
        //                cmdtem.ExecuteNonQuery();
        //                con.Close();

        //            }
        //            else if (actotde < actotcr)
        //            {
        //                crdif = actotcr - actotde;
        //                revdif += crdif;
        //                jno += 1;
        //                SqlCommand cmdtr11 = new SqlCommand("Insert into TranctionMaster(Date,EntryNumber,EntryTypeId,UserId,Tranction_Amount,compid,Whid)Values('" + DateTime.Now.ToShortDateString() + "','" + jno + "','3','" + Session["userid"] + "','" + crdif + "','" + Session["comid"] + "','" + ds12.Rows[0]["Whid"] + "')", con);

        //                con.Open();
        //                cmdtr11.ExecuteNonQuery();

        //                con.Close();
        //                DataTable dttrid = new DataTable();
        //                dttrid = (DataTable)select("Select  Max(Tranction_Master_Id)  as Tranction_Master_Id  from TranctionMaster  where Whid='" + ds12.Rows[0]["Whid"] + "'");
        //                if (dttrid.Rows.Count > 0)
        //                {
        //                    SqlCommand cmdtrd = new SqlCommand("Insert into Tranction_Details(AccountDebit,AccountCredit,AmountDebit,AmountCredit,Tranction_Master_Id,Memo,DateTimeOfTransaction,compid,whid)Values('" + dts["AccountId"] + "','0','" + crdif + "','0','" + dttrid.Rows[0]["Tranction_Master_Id"] + "','ClosingEntry','" + DateTime.Now.ToShortDateString() + "','" + Session["comid"] + "','" + ds12.Rows[0]["Whid"] + "') ", con);

        //                    con.Open();
        //                    cmdtrd.ExecuteNonQuery();
        //                    con.Close();
        //                }
        //                SqlCommand cmdtem = new SqlCommand("Insert into TemporyDataTbl(Trans_Id,periodId)Values('" + dttrid.Rows[0]["Tranction_Master_Id"] + "','" + ds12.Rows[0]["Report_Period_Id"] + "') ", con);

        //                con.Open();
        //                cmdtem.ExecuteNonQuery();
        //                con.Close();
        //            }
        //        }

        //        DataTable dtbal = new DataTable();
        //        dtbal = (DataTable)select("Select AccountBalance.Balance,Account_Balance_Id from AccountBalance  inner join AccountMaster on AccountMaster.Id=AccountBalance.AccountMasterId where AccountId='4700' and Report_Period_Id='" + ds12.Rows[0]["Report_Period_Id"] + "'  and Whid='" + ds12.Rows[0]["Whid"] + "'");
        //        if (dtbal.Rows.Count > 0)
        //        {
        //            revdif += Convert.ToDouble(dtbal.Rows[0]["Balance"]);
        //            SqlCommand cmdtr1 = new SqlCommand("Update AccountBalance Set Balance='" + revdif + "' where Account_Balance_Id='" + dtbal.Rows[0]["Account_Balance_Id"] + "' ", con);

        //            con.Open();
        //            cmdtr1.ExecuteNonQuery();
        //            con.Close();
        //        }
        //    }
        //}
        //SqlCommand cmdtr = new SqlCommand("Update ReportPeriod set Active='0'  where  Report_Period_Id='" + ds12.Rows[0]["Report_Period_Id"] + "' ", con);

        //con.Open();
        //cmdtr.ExecuteNonQuery();
        //con.Close();
        //string[] separator1 = new string[] { "TO" };
        //string[] strSplitArr1 = ds12.Rows[0]["Name"].ToString().Split(separator1, StringSplitOptions.RemoveEmptyEntries);
        //string yearofstart = strSplitArr1[0].ToString().Replace(" ", "");
        //string yearofend = strSplitArr1[1].ToString().Replace(" ", "");
        //DataTable dtyear = new DataTable();
        //dtyear = (DataTable)select("Select EmployeeMasterID  from EmployeeMaster where Whid='" + ddlwarehouse.SelectedValue + "'");
        //foreach (DataRow dtpsr in dtyear.Rows)
        //{
        //    DataTable dtyearcheckor = new DataTable();
        //    dtyearcheckor = (DataTable)select("Select * from  Tax_Year WHERE TaxYear_Name='" + yearofstart + "'");
        //    if (dtyearcheckor.Rows.Count < 0)
        //    {
        //        string strtax = "Insert Into Tax_Year(TaxYear_Name,StartDate,EndDate,Active)values'" + yearofstart + "','01/01/" + yearofstart + "','12/31/" + yearofstart + "'.'1')";
        //        SqlCommand cmdtax = new SqlCommand(strtax, con);
        //        con.Open();
        //        cmdtax.ExecuteNonQuery();
        //        con.Close();


        //    }

        //    DataTable dtyearcheck = new DataTable();

        //    dtyearcheck = (DataTable)select("Select * from   TaxFormulaVariableTbl inner join Tax_Year on Tax_Year.TaxYear_Id=TaxFormulaVariableTbl.TaxYearId where Tax_Year.TaxYear_Name='" + yearofstart + "' and TaxFormulaVariableTbl.EmployeeID='" + dtpsr["EmployeeMasterID"] + "'");
        //    if (dtyearcheck.Rows.Count < 0)
        //    {

        //        addtxformula(Convert.ToString(dtpsr["EmployeeMasterID"]), yearofstart);
        //    }

        //    DataTable dtyearcheckor1 = new DataTable();
        //    dtyearcheckor1 = (DataTable)select("Select * from Tax_Year WHERE TaxYear_Name='" + yearofend + "'");
        //    if (dtyearcheckor1.Rows.Count < 0)
        //    {
        //        string strtax = "Insert Into Tax_Year(TaxYear_Name,StartDate,EndDate,Active)values'" + yearofend + "','01/01/" + yearofend + "','12/31/" + yearofend + "'.'1')";
        //        SqlCommand cmdtax = new SqlCommand(strtax, con);
        //        con.Open();
        //        cmdtax.ExecuteNonQuery();
        //        con.Close();


        //    }
        //    DataTable dtyearcheck1 = new DataTable();
        //    dtyearcheck1 = (DataTable)select("Select * from   TaxFormulaVariableTbl inner join Tax_Year on Tax_Year.TaxYear_Id=TaxFormulaVariableTbl.TaxYearId where Tax_Year.TaxYear_Name='" + yearofend + "' and TaxFormulaVariableTbl.EmployeeID='" + dtpsr["EmployeeMasterID"] + "'");
        //    if (dtyearcheck1.Rows.Count < 0)
        //    {
        //        addtxformula(Convert.ToString(dtpsr["EmployeeMasterID"]), yearofend);
        //    }
        //}

    }
    protected void fillcloseentry()
    {

    }
    public double isnumSelf(string ck)
    {
        double i = 0;
        try
        {
            i = Convert.ToDouble(ck);
        }
        catch (Exception)
        {
            i = 0;
        }
        return i;
    }
    
    protected void txtAddress2_TextChanged(object sender, EventArgs e)
    {

    }
    protected void ddlacday_SelectedIndexChanged(object sender, EventArgs e)
    {
        string se = "";
        if (ddlacmonth.SelectedIndex != -1)
        {
            se = ddlacmonth.SelectedItem.Text;
        }
        ddlacmonth.Items.Clear();
        if (Convert.ToInt32(ddlacday.SelectedItem.Text) <= 29)
        {
            ddlacmonth.Items.Insert(0, "1");
            ddlacmonth.Items.Insert(1, "2");
            ddlacmonth.Items.Insert(2, "3");
            ddlacmonth.Items.Insert(3, "4");
            ddlacmonth.Items.Insert(4, "5");
            ddlacmonth.Items.Insert(5, "6");
            ddlacmonth.Items.Insert(6, "7");
            ddlacmonth.Items.Insert(7, "8");
            ddlacmonth.Items.Insert(8, "9");
            ddlacmonth.Items.Insert(9, "10");
            ddlacmonth.Items.Insert(10, "11");
            ddlacmonth.Items.Insert(11, "12");
        }
        else if (Convert.ToInt32(ddlacday.SelectedItem.Text) <= 30)
        {
            ddlacmonth.Items.Insert(0, "1");
            // ddlacmonth.Items.Insert(0, "2");
            ddlacmonth.Items.Insert(1, "3");
            ddlacmonth.Items.Insert(2, "4");
            ddlacmonth.Items.Insert(3, "5");
            ddlacmonth.Items.Insert(4, "6");
            ddlacmonth.Items.Insert(5, "7");
            ddlacmonth.Items.Insert(6, "8");
            ddlacmonth.Items.Insert(7, "9");
            ddlacmonth.Items.Insert(8, "10");
            ddlacmonth.Items.Insert(9, "11");
            ddlacmonth.Items.Insert(10, "12");

        }

        else if (Convert.ToInt32(ddlacday.SelectedItem.Text) <= 31)
        {
            ddlacmonth.Items.Insert(0, "1");

            ddlacmonth.Items.Insert(1, "3");

            ddlacmonth.Items.Insert(2, "5");

            ddlacmonth.Items.Insert(3, "7");
            ddlacmonth.Items.Insert(4, "8");

            ddlacmonth.Items.Insert(5, "10");

            ddlacmonth.Items.Insert(6, "12");
        }
        if (se != "")
        {
            ddlacmonth.SelectedIndex = ddlacmonth.Items.IndexOf(ddlacmonth.Items.FindByText(se));
            lblfysdate.Text = ddlacmonth.SelectedItem.Text + "/" + ddlacday.SelectedItem.Text + "/" + ddlacyear.SelectedItem.Text;
            lblfyedate.Text = Convert.ToDateTime(lblfysdate.Text).AddYears(1).ToShortDateString();
            lblfyedate.Text = Convert.ToDateTime(lblfyedate.Text).AddDays(-1).ToShortDateString();
        }

    }
    protected void btnconf_Click(object sender, EventArgs e)
    {
        ModalPopupExtender122.Show();
    }
    protected void ddlbus_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtda = (DataTable)select("select Distinct Report_period_confirm.Id as conid, ReportPeriod.Report_Period_Id,Convert(nvarchar,StartDate,101) as StartDate, Convert(nvarchar,EndDate,101) as EndDate from [ReportPeriod] left join Report_period_confirm on Report_period_confirm.Report_Period_Id=ReportPeriod.Report_Period_Id where Compid = '" + Session["comid"] + "' and Whid='" + ddlbus.SelectedValue + "' and Active='1' ");
        if (dtda.Rows.Count > 0)
        {
            lblyearcacc.Text = "Select your First Accounting year for your business";
            if (Convert.ToString(dtda.Rows[0]["conid"]) == "")
            {
                pnlaccy.Visible = true;
                //lblyearcacc.Text = "Select your First Accounting year for your business";
                lblopenaccy.Visible = false;
                string fday = Convert.ToDateTime(dtda.Rows[0]["StartDate"]).Day.ToString();
                string fmonth = Convert.ToDateTime(dtda.Rows[0]["StartDate"]).Month.ToString();
                string fyear = Convert.ToDateTime(dtda.Rows[0]["StartDate"]).Year.ToString();
                lblfysdate.Text = Convert.ToString(dtda.Rows[0]["StartDate"]);
                lblfyedate.Text = Convert.ToString(dtda.Rows[0]["EndDate"]);
                //for (int k = 1; k <= 31; k++)
                //{
                //    ddlacday.Items.Insert(k - 1, k.ToString());
                //}
                //int index = 0;
                ddlacday.SelectedIndex = ddlacday.Items.IndexOf(ddlacday.Items.FindByText(fday));
                ddlacyear.SelectedIndex = ddlacyear.Items.IndexOf(ddlacyear.Items.FindByText(fyear));
                ddlacday_SelectedIndexChanged(sender, e);
                ddlacmonth.SelectedIndex = ddlacmonth.Items.IndexOf(ddlacmonth.Items.FindByText(fmonth));
                //for (int k = 1; k <= 50; k++)
                //{
                //    int cye = (DateTime.Now.Year) + 50;
                //    ddlacyear.Items.Insert(index, (cye - k).ToString());

                //}
                //for (int k = 0; k < 15; k++)
                //{
                //    int cye = DateTime.Now.Year - 15;
                //    ddlacyear.Items.Insert(index, (cye - k).ToString());
                //}
                contr(true);

            }
            else
            {
                lblopenaccy.NavigateUrl = "~/ShoppingCart/Admin/AccountYearChange.aspx?Whid=" + ddlbus.SelectedValue + "";

                pnlaccy.Visible = true;
                contr(false);
                //lblyearcacc.Text = "Accounting year for your business ";
                // lblopenaccy.Text = Convert.ToString(dtda.Rows[0]["StartDate"]) + "-" + Convert.ToString(dtda.Rows[0]["EndDate"]);
                //lblopenaccy.Visible = true;
            }

        }
    }
    protected void contr(Boolean t)
    {
        ddlacday.Enabled = t;
        ddlacmonth.Enabled = t;
        ddlacyear.Enabled = t;
        ddlacmonth.Enabled = t;
        btnconf.Enabled = t;

    }
    protected void ddlacmonth_SelectedIndexChanged(object sender, EventArgs e)
    {

        lblfysdate.Text = ddlacmonth.SelectedItem.Text + "/" + ddlacday.SelectedItem.Text + "/" + ddlacyear.SelectedItem.Text;


        lblfyedate.Text = Convert.ToDateTime(lblfysdate.Text).AddYears(1).ToShortDateString(); ;
        lblfyedate.Text = Convert.ToDateTime(lblfyedate.Text).AddDays(-1).ToShortDateString();
    }
    protected void ddlacyear_SelectedIndexChanged(object sender, EventArgs e)
    {

        lblfysdate.Text = ddlacmonth.SelectedItem.Text + "/" + ddlacday.SelectedItem.Text + "/" + ddlacyear.SelectedItem.Text;


        lblfyedate.Text = Convert.ToDateTime(lblfysdate.Text).AddYears(1).ToShortDateString();
        lblfyedate.Text = Convert.ToDateTime(lblfyedate.Text).AddDays(-1).ToShortDateString();
    }
    protected void btnapd_Click(object sender, EventArgs e)
    {
        int Currentyearorg = 0;
        int Currentyear = 0;
        DataTable dtda = (DataTable)select("select Report_Period_Id,Convert(nvarchar,StartDate,101) as StartDate, Convert(nvarchar,EndDate,101) as EndDate from [ReportPeriod] where Compid = '" + Session["comid"] + "' and Whid='" + ddlbus.SelectedValue + "' and Active='1' ");
        if (dtda.Rows.Count > 0)
        {
            DataTable dtdaopb = new DataTable();
            dtdaopb = (DataTable)select("select top(1)Report_Period_Id from [ReportPeriod] where ReportPeriod.Report_Period_Id<'" + dtda.Rows[0]["Report_Period_Id"] + "' and  Whid='" + ddlbus.SelectedValue + "'  order by Report_Period_Id Desc");
            if (dtdaopb.Rows.Count > 0)
            {
                string inse = "Insert into Report_period_confirm(Report_Period_Id,conform)values('" + dtda.Rows[0]["Report_Period_Id"] + "','1')";
                SqlCommand cmd = new SqlCommand(inse, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                con.Close();
                Currentyear = Convert.ToInt32(ddlacyear.SelectedItem.Text) + 1;
                Currentyearorg = DateTime.Now.Year + 1;
                int yem = Convert.ToInt32(ddlacyear.SelectedItem.Text) + 1;
                string yname = ddlacyear.SelectedItem.Text + " TO " + yem.ToString();
                string upd = "Update ReportPeriod set Name='" + yname + "',StartDate='" + Convert.ToDateTime(lblfysdate.Text) + "',EndDate='" + Convert.ToDateTime(lblfyedate.Text) + "',FistStartDate='" + Convert.ToDateTime(lblfysdate.Text) + "' where Report_Period_Id='" + dtda.Rows[0]["Report_Period_Id"] + "'";
                SqlCommand cmd1 = new SqlCommand(upd, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd1.ExecuteNonQuery();
                con.Close();


                int yem1 = Convert.ToInt32(ddlacyear.SelectedItem.Text) - 1;
                string yname1 = yem1.ToString() + " TO " + ddlacyear.SelectedItem.Text;
                string upd1 = "Update ReportPeriod set Name='" + yname1 + "',StartDate='" + Convert.ToDateTime(lblfysdate.Text).AddYears(-1).ToString() + "',EndDate='" + Convert.ToDateTime(lblfyedate.Text).AddYears(-1).ToString() + "',FistStartDate='" + Convert.ToDateTime(lblfysdate.Text) + "' where Report_Period_Id='" + dtdaopb.Rows[0]["Report_Period_Id"] + "'";
                SqlCommand cmd11 = new SqlCommand(upd1, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd11.ExecuteNonQuery();
                con.Close();
                int k = 1;
                for (int i = Currentyear; i < Currentyearorg; i++)
                {
                    DateTime fye = Convert.ToDateTime(lblfysdate.Text).AddYears(k);
                    DateTime eye = Convert.ToDateTime(lblfyedate.Text).AddYears(k);
                    int sy = Convert.ToInt32(ddlacyear.SelectedItem.Text) + k;
                    int ey = Convert.ToInt32(ddlacyear.SelectedItem.Text) + (k + 1);
                    string nam = sy.ToString() + " TO " + ey.ToString();
                    SqlCommand cmdtr2 = new SqlCommand("Insert into ReportPeriod(Name,StartDate,EndDate,FistStartDate,Active,Compid,Whid)Values('" + nam + "','" + fye + "','" + eye + "','" + fye + "','0','" + Session["Comid"] + "','" + ddlbus.SelectedValue + "') ", con);

                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdtr2.ExecuteNonQuery();
                    con.Close();
                    k += 1;
                    SqlCommand cmsa = new SqlCommand("SELECT max(Report_Period_Id) from ReportPeriod where Compid='" + Session["comid"] + "' and Whid='" + ddlbus.SelectedValue + "'", con);
                    con.Open();
                    object rig = cmsa.ExecuteScalar();
                    con.Close();
                    string str5t1 = "select Id from AccountMaster where Whid='" + ddlbus.SelectedValue + "'";
                    SqlCommand cmd32t1 = new SqlCommand(str5t1, con);
                    SqlDataAdapter adp15t1 = new SqlDataAdapter(cmd32t1);
                    DataTable dtlogin14t1 = new DataTable();
                    adp15t1.Fill(dtlogin14t1);
                    foreach (DataRow dr in dtlogin14t1.Rows)
                    {
                        SqlCommand cmdtr13 = new SqlCommand("Insert into AccountBalance(AccountMasterId,Balance,Report_Period_Id)Values('" + dr["Id"] + "','0','" + rig + "') ", con);

                        con.Open();
                        cmdtr13.ExecuteNonQuery();
                        con.Close();
                    }
                }
                contr(false);
            }
        }
    }

    protected void btns_Click(object sender, EventArgs e)
    {

    }

    protected void btnChange_Click(object sender, EventArgs e)
    {
        FileUpload1.Visible = true;
        RequiredFieldValidator11.Visible = true;
        pnllogo.Visible = false;
        //btnChange.Visible = false;
        imgBtnImageUpdate.Visible = true;
        ImgBtncancel.Visible = true;
        Label1.Visible = true;
    }
    protected void btnmanage_Click(object sender, EventArgs e)
    {
        //string te = "http://members.busiwiz.com/login.aspx?comid=" + Session["Comid"];
        string te = "http://members.busiwiz.com/CompanyProfileForEdit.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }

   
}
