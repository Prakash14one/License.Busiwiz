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
using System.Collections.Generic;
using System.Data.SqlClient;

public partial class WebSite1_referenceafterlogin : System.Web.UI.Page
{
    SqlConnection con;
    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        if (!IsPostBack)
        {
            string select = " select Party_master.Email from Party_master where PartyID='" + Session["PartyId"].ToString() + "'";
            SqlDataAdapter da = new SqlDataAdapter(select,con);
            DataTable dt = new DataTable();
            da.Fill(dt);
           
            ViewState["email"] = dt.Rows[0][0].ToString();
            Panel1.Visible = true;
            panel23.Visible = true;
            request();
          
        }
       
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        Panel2.Visible = false;
        Panel4.Visible = false;
        Panel3.Visible = false;
        request();
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        Panel1.Visible = false;
        Panel2.Visible = false;
        Panel4.Visible = true;
        Panel3.Visible = false;
        referenceprovided();
        
    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        Panel1.Visible = false;
        Panel2.Visible = true;
        Panel4.Visible = false;
        Panel3.Visible = false;
        fillcountry();
        fillstate();
        fillcity();
        profile();
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        Panel1.Visible = false;
        Panel2.Visible = false;
        Panel4.Visible = false;
        Panel3.Visible = true;
        logindetails();
    }
    public void logindetails()
    {
        string login = "select Login_master.username,Login_master.password from  Login_master inner join User_master ON User_master.UserID = Login_master.UserID where PartyID='" + Session["PartyId"].ToString() + "'";
        SqlCommand cmd3 = new SqlCommand(login, con);
        SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
        DataTable dt3 = new DataTable();
        da3.Fill(dt3);
        TextBox12.Text = dt3.Rows[0]["username"].ToString();
        TextBox11.Text = ClsEncDesc.Decrypted(dt3.Rows[0]["password"].ToString());
    }
    public void profile()
    {
        string prfl = "select Party_master.Compname as name,Party_master.Address,Party_master.City,Party_master.State,Party_master.Country,Party_master.Email, Party_master.Phoneno,MyRefenceTbl.designation,MyRefenceTbl.company from MyRefenceTbl inner join Party_master on Party_master.Email=MyRefenceTbl.email    where PartyID='" + Session["PartyId"].ToString() + "'";
        SqlCommand cmd1 = new SqlCommand(prfl, con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataTable dt = new DataTable();
        da1.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            txtname.Text = dt.Rows[0]["name"].ToString();
            txtdesig.Text = dt.Rows[0]["designation"].ToString();
            txtcom.Text = dt.Rows[0]["company"].ToString();
            txtph.Text = dt.Rows[0]["Phoneno"].ToString();
            txtaddress.Text = dt.Rows[0]["Address"].ToString();
            txtemail.Text = dt.Rows[0]["Email"].ToString();
            txtemail.Enabled = false;
            ddlcountry.SelectedValue = dt.Rows[0]["Country"].ToString();
            ddlstate.SelectedValue = dt.Rows[0]["State"].ToString();
            ddlcity.SelectedValue = dt.Rows[0]["City"].ToString();

        }

    }
    public void fillcountry()
    {
        string qryStr = "select CountryId,CountryName from CountryMaster order by CountryName";
        SqlCommand cmd1 = new SqlCommand(qryStr, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable ds1 = new DataTable();
        adp1.Fill(ds1);
        DataRow dr = ds1.NewRow();
        dr["CountryId"] = "0";
        dr["CountryName"] = "--Select--";
        ds1.Rows.InsertAt(dr, 0);
        ddlcountry.DataSource = ds1;
        ddlcountry.DataBind();



    }
    public void fillstate()
    {
        string str2 = "select StateId,StateName from StateMasterTbl ";
        SqlCommand cmd1 = new SqlCommand(str2, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable ds1 = new DataTable();
        adp1.Fill(ds1);
        DataRow dr = ds1.NewRow();
        dr["StateId"] = "0";
        dr["StateName"] = "--Select--";
        ds1.Rows.InsertAt(dr, 0);
        ddlstate.DataSource = ds1;
        ddlstate.DataBind();



    }
    public void fillcity()
    {
        string str2 = "select CityId,CityName from CityMasterTbl ";
        SqlCommand cmd1 = new SqlCommand(str2, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable ds1 = new DataTable();
        adp1.Fill(ds1);
        DataRow dr = ds1.NewRow();
        dr["CityId"] = "0";
        dr["CityName"] = "--Select--";
        ds1.Rows.InsertAt(dr, 0);
        ddlcity.DataSource = ds1;
        ddlcity.DataBind();

    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        con.Open();
        string upda = "update MyRefenceTbl set name='" + txtname.Text + "',designation='" + txtdesig.Text + "',company='" + txtcom.Text + "',contactno='" + txtph.Text + "',address='" + txtaddress.Text + "',countryid='" + ddlcountry.SelectedValue + "',stateid='" + ddlstate.SelectedValue + "',cityid='" + ddlcity.SelectedValue + "' where email='" + txtemail.Text + "'";
        SqlCommand cmd2 = new SqlCommand(upda,con);
        cmd2.ExecuteNonQuery();

        string partymaster = "update Party_master set Compname='" + txtname.Text + "',Address='" + txtaddress.Text + "',City='" + ddlcity.SelectedValue + "',State='" + ddlstate.SelectedValue + "',Country='" + ddlcountry.SelectedValue + "',Phoneno='" + txtph.Text + "' where  PartyID='" + Session["PartyId"].ToString() + "' ";
        SqlCommand cmd3 = new SqlCommand(partymaster, con);
        cmd3.ExecuteNonQuery();

        string usermaster = " update User_master set  Name='" + txtname.Text + "',Address='" + txtaddress.Text + "' ,City='" + ddlcity.SelectedValue + "',State='" + ddlstate.SelectedValue + "',Country='" + ddlcountry.SelectedValue + "',Phoneno='" + txtph.Text + "' where  PartyID='" + Session["PartyId"].ToString() + "' ";
        SqlCommand cmd4 = new SqlCommand(usermaster, con);
        cmd4.ExecuteNonQuery();

        string employeemaster = "update EmployeeMaster set Address='" + txtaddress.Text + "',CountryId='" + ddlcountry.SelectedValue + "',StateId='" + ddlstate.SelectedValue + "',City='" + ddlcity.SelectedValue + "',ContactNo='" + txtph.Text + "',EmployeeName='" + txtname.Text + "' where  PartyID='" + Session["PartyId"].ToString() + "' ";
        SqlCommand cmd5 = new SqlCommand(employeemaster, con);
        cmd5.ExecuteNonQuery();

        string candmaster = "update  CandidateMaster set CountryId='" + ddlcountry.SelectedValue + "',StateId='" + ddlstate.SelectedValue + "',City='" + ddlcity.SelectedValue + "',LastName='" + txtname.Text + "',FirstName='" + txtname.Text + "',MiddleName='" + txtname.Text + "',Address='" + txtaddress.Text + "',ContactNo='" + txtph.Text + "' where  PartyID='" + Session["PartyId"].ToString() + "' ";
        SqlCommand cmd6 = new SqlCommand(candmaster, con);
        cmd6.ExecuteNonQuery();

        string party = "update  PartyAddressTbl set  Address='" + txtaddress.Text + "',Country='" + ddlcountry.SelectedValue + "',State='" + ddlstate.SelectedValue + "',City='" + ddlcity.SelectedValue + "',Phone='" + txtph.Text + "' where  PartyMasterId='" + Session["PartyId"].ToString() + "' ";
        SqlCommand cmd7 = new SqlCommand(party, con);
        cmd7.ExecuteNonQuery();

        con.Close();

        Label16.Text = "Profile Updated Successfully";
    }
    protected void ddlcountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        string str2 = "select StateId,StateName from StateMasterTbl where CountryId=" + ddlcountry.SelectedValue + " order by StateName";
        SqlCommand cmd1 = new SqlCommand(str2, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable ds1 = new DataTable();
        adp1.Fill(ds1);
        DataRow dr = ds1.NewRow();
        dr["StateId"] = "0";
        dr["StateName"] = "--Select--";
        ds1.Rows.InsertAt(dr, 0);
        ddlstate.DataSource = ds1;
        ddlstate.DataBind();
    
      
    
    }
    protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        string str2 = "select CityId,CityName from CityMasterTbl where StateId=" + ddlstate.SelectedValue + " order by CityName";
        SqlCommand cmd1 = new SqlCommand(str2, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable ds1 = new DataTable();
        adp1.Fill(ds1);
        DataRow dr = ds1.NewRow();
        dr["CityId"] = "0";
        dr["CityName"] = "--Select--";
        ds1.Rows.InsertAt(dr, 0);
        ddlcity.DataSource = ds1;
        ddlcity.DataBind();
        
    }
    public void request()
    {
        string request = " select  distinct CandidateMaster.CandidatePhotoPath,CandidateMaster.LastName,CandidateMaster.FirstName, CandidateMaster.CandidateId,MyRefenceTbl.refernceid "+
                          " from CandidateMaster inner join MyRefenceTbl on CandidateMaster.CandidateId= MyRefenceTbl.candidateid    inner join Refernce_inputTbl on Refernce_inputTbl.reference_id=MyRefenceTbl.refernceid " +
                          " where MyRefenceTbl.email='" + ViewState["email"] + "'and Refernce_inputTbl.Response='No' ";
        SqlCommand cmd2 = new SqlCommand(request,con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataTable dt = new DataTable();
        da2.Fill(dt);
        
        if (dt.Rows.Count > 0)
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = "";
            GridView1.DataBind();
        }

    }
    protected void LinkButton6_Click(object sender, EventArgs e)
    {
        LinkButton lnkbtn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lnkbtn.NamingContainer;
        int j = Convert.ToInt32(row.RowIndex);
        string refrenceid = GridView1.Rows[j].Cells[5].Text;
        string te = "referenceinput.aspx?referenceid="+refrenceid;
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
       
    }
    public void referenceprovided()
    {
        Label16.Text = "";
        string str1 = "";
        if (txtfromdate.Text != " " && txttodate.Text!="")
        {
            str1 += " and Refernce_inputTbl.Dateandtime between '" + txtfromdate.Text + "' and '" + txttodate.Text + "'";
        }
        
        if (TextBox10.Text!="")
        {
            str1 += " and  candidatemaster.FirstName  like '%" + TextBox10.Text.Replace("'", "''") + "%'";
        }
        string provided = "  select distinct CandidateMaster.CandidatePhotoPath,CandidateMaster.LastName,CandidateMaster.FirstName,Party_master.PartyID," +
                              "CandidateMaster.CandidateId,MyRefenceTbl.refernceid,Party_master.Email,CityMasterTbl.CityName,  CountryMaster.CountryName,CandidateMaster.MobileNo,Refernce_inputTbl.Dateandtime"+
                              " from CandidateMaster    inner join  [jobcenter.OADB_Developer].[dbo].Party_master  on Party_master.PartyID=CandidateMaster.PartyID "+
                               "inner join CountryMaster on CountryMaster.CountryId= CandidateMaster.CountryId   inner join CityMasterTbl on CityMasterTbl.CityId=CandidateMaster.City "+
                                "inner join MyRefenceTbl on CandidateMaster.CandidateId= MyRefenceTbl.candidateid  inner join Refernce_inputTbl on  MyRefenceTbl.refernceid=Refernce_inputTbl.reference_id "+
                                 "where Refernce_inputTbl.Response='Yes' and MyRefenceTbl.status='1' and MyRefenceTbl.email='" + ViewState["email"] + "' "+str1+"";
        SqlCommand cmd3 = new SqlCommand(provided, con);
        SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
        DataTable dt3 = new DataTable();
        da3.Fill(dt3);
        if (dt3.Rows.Count > 0)
        {
            GridView2.DataSource = dt3;
            GridView2.DataBind();
        }
        else
        {
            GridView2.DataSource = "";
            GridView2.DataBind();
        }
    }
    protected void LinkButton5_Click(object sender, EventArgs e)
    {
        LinkButton lnkbtn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lnkbtn.NamingContainer;
        int j = Convert.ToInt32(row.RowIndex);
        string candaidteid = GridView2.Rows[j].Cells[11].Text;
        string te = "CandidateJobProfile.aspx?ID=" + candaidteid;
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void LinkButton7_Click(object sender, EventArgs e)
    {
        LinkButton lnkbtn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lnkbtn.NamingContainer;
        int j = Convert.ToInt32(row.RowIndex);
        string candaidteid = GridView2.Rows[j].Cells[11].Text;
        string te = "CandidateJobProfile.aspx?ID=" + candaidteid;
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void LinkButton5_Click1(object sender, EventArgs e)
    {
        LinkButton lnkbtn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lnkbtn.NamingContainer;
        int j = Convert.ToInt32(row.RowIndex);
        string refernceid = GridView2.Rows[j].Cells[10].Text;
        string te = "viewcandidatereferences.aspx?refernceid=" + refernceid;
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate) && (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header))
        {
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[11].Visible = false;


        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate) && (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header))
        {
            e.Row.Cells[5].Visible = false;
            


        }
    }
    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton lnkbtn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)lnkbtn.NamingContainer;
        int j = Convert.ToInt32(row.RowIndex);
        string refernceid = GridView2.Rows[j].Cells[10].Text;
        con.Open();
        string del = "update MyRefenceTbl set status='2' where refernceid='" + refernceid + "' ";
        SqlCommand cmd2 = new SqlCommand(del,con);
        cmd2.ExecuteNonQuery();
        con.Close();
        referenceprovided();

    }
    protected void txttodate_TextChanged(object sender, EventArgs e)
    {
        referenceprovided();
    }
    protected void TextBox10_TextChanged(object sender, EventArgs e)
    {
        referenceprovided();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("ResetPasswordUser.aspx");
    }
    #region mail
    //public void sendmail2()
    //{




    //    string cand = "select CandidateMaster.CandidatePhotoPath,CandidateMaster.LastName + ' ' + CandidateMaster.FirstName + ' ' + CandidateMaster.MiddleName as canname,CandidateMaster.MobileNo,Party_master.Email from CandidateMaster inner join Party_master on Party_master.PartyID=CandidateMaster.PartyID inner join MyRefenceTbl on CandidateMaster.CandidateId=MyRefenceTbl.candidateid where MyRefenceTbl.candidateid='" + ViewState["candidateid"] + "'";
    //    SqlCommand cmd2 = new SqlCommand(cand, con);
    //    SqlDataAdapter adp2 = new SqlDataAdapter(cmd2);
    //    DataTable dt2 = new DataTable();
    //    adp2.Fill(dt2);



    //    StringBuilder strplan = new StringBuilder();

    //    string file = "job-center-logo.jpg";
    //    strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
    //    strplan.Append("<tr><td align=\"left\"> <img src=\"http://" + Request.Url.Host.ToString() + "/images/" + file + "\" \"border=\"0\" Width=\"90\" Height=\"80\" / > </td ></tr>  ");
    //    strplan.Append("<br></table> ");

    //    strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
    //    strplan.Append("<tr><td align=\"left\">Dear " + txtname.Text + ",</td></tr>  ");
    //    strplan.Append("<tr><td align=\"left\"><br></td></tr>  ");
    //    strplan.Append("</table> ");

    //    strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
    //    strplan.Append("<tr><td align=\"left\">The following candidate has requested that you provide her/him a reference to use in her/his ijobcenter.com job search.</td></tr> ");
    //    strplan.Append("</table> ");



    //    strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
    //    strplan.Append("<tr><td align=\"left\"> <img src=\"http://www.ijobcenter.com/images/" + dt2.Rows[0]["CandidatePhotoPath"].ToString() + "\" \"border=\"0\" Width=\"90\" Height=\"80\" / > </td ></tr>  ");

    //    strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Candidate Name :</td><td align=\"left\" style=\"width: 80%\">" + dt2.Rows[0]["canname"].ToString() + "</td></tr>");
    //    strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Candidate Phone No :</td><td align=\"left\" style=\"width: 80%\">" + dt2.Rows[0]["MobileNo"].ToString() + "</td></tr>");
    //    strplan.Append("<tr><td align=\"left\" style=\"width: 20%\">Candidate Email ID :</td><td align=\"left\" style=\"width: 80%\">" + dt2.Rows[0]["Email"].ToString() + "</td></tr>");

    //    strplan.Append("</table> ");

    //    strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; font-weight:bold; color: #000000; font-family: Arial\"> ");
    //    strplan.Append("<tr><td align=\"left\"></td></tr>  ");
    //    strplan.Append("<tr><td align=\"left\"><br></td></tr>  ");
    //    strplan.Append("</table> ");


    //    strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
    //    strplan.Append("<tr><td align=\"left\">References such as the one requested of you play an instrumental role in a candidate's job search. </td></tr> ");
    //    strplan.Append("</table> ");

    //    strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
    //    strplan.Append("<tr><td align=\"left\">Please note that you have to configure your account before you can start using this product.</td></tr> ");
    //    strplan.Append("</table> ");

    //    strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
    //    strplan.Append("<tr><td align=\"left\">Your login information is as under:</td></tr>");
    //    strplan.Append("<tr><td align=\"left\">Login Information:</td></tr> ");
    //    strplan.Append("<tr><td align=\"left\">User ID: " + ViewState["username"] + "</td></tr> ");
    //    strplan.Append("<tr><td align=\"left\">Password: " + ViewState["password"] + "</td></tr> ");
    //    strplan.Append("<tr><td align=\"left\">That being said, if you wish oblige this request, please click <a href=http://members.ijobcenter.com/ target=_blank >here</a>, or copy and paste the URL below into your internet browser. Please rest assured that information you choose to provide here will not be shared with the candidate in question.  </td></tr> ");
    //    strplan.Append("<tr><td align=\"left\"> http://members.ijobcenter.com/ </td></tr> ");
    //    strplan.Append("</table> ");



    //    strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");
    //    strplan.Append("<tr><td align=\"left\">With gratitude, </td></tr>  ");



    //    strplan.Append("<table width=\"100%\" style=\"font-size: 10pt; color: #000000; font-family: Arial\"> ");

    //    strplan.Append("<tr><td align=\"left\"><br> </td></tr> ");

    //    strplan.Append("<tr><td align=\"left\">Irene Walsh- Support Manager</td></tr> ");
    //    strplan.Append("<tr><td align=\"left\">JobCenter.com </td></tr> ");
    //    strplan.Append("<tr><td align=\"left\">079-26462230 ext 221 </td></tr> ");
    //    strplan.Append("<tr><td align=\"left\">18001800180 ext 222 </td></tr> ");
    //    strplan.Append("<tr><td align=\"left\">www.ijobcenter.com </td></tr> ");
    //    strplan.Append("<tr><td align=\"left\">Herrykem House, Nr. Acharya Travels, Nr. Havmore Railway Track </td></tr> ");
    //    strplan.Append("<tr><td align=\"left\">Ahmedabad Gujarat India 380014</td></tr> ");


    //    string bodyformate = "" + strplan + "";

    //    try
    //    {
    //        string str = "select [PortalMasterTbl].[EmailId],PortalMasterTbl.EmailDisplayname,PortalMasterTbl.Password,PortalMasterTbl.Mailserverurl from [PortalMasterTbl] inner join [CompanyMaster] on [PortalMasterTbl].[ProductId]=[CompanyMaster].[ProductId]  where  dbo.PortalMasterTbl.Id=7";

    //        SqlDataAdapter da = new SqlDataAdapter(str, PageConn.licenseconn());
    //        DataTable dt = new DataTable();
    //        da.Fill(dt);
    //        if (dt.Rows.Count > 0)
    //        {
    //            string email = Convert.ToString("info@ijobcenter.com");
    //            string displayname = Convert.ToString(dt.Rows[0]["EmailDisplayname"]);
    //            string password = Convert.ToString(dt.Rows[0]["Password"]);
    //            string outgo = Convert.ToString(dt.Rows[0]["Mailserverurl"]);

    //            MailAddress to = new MailAddress(txtemail.Text);
    //            MailAddress from = new MailAddress(email, "Admin");
    //            MailMessage objEmail = new MailMessage(from, to);
    //            objEmail.Subject = "Reference for candidate";  //Subject.ToString();
    //            objEmail.Body = bodyformate;


    //            objEmail.IsBodyHtml = true;
    //            objEmail.Priority = MailPriority.High;
    //            SmtpClient client = new SmtpClient();
    //            client.Credentials = new NetworkCredential("info@ijobcenter.com", "Om2012++");
    //            client.Host = outgo;
    //            client.Send(objEmail);
    //        }
    //        else
    //        {
    //            // lblmsg.Text = "No Portal Email Available.";
    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //        //Notsendmailmail += mail;
    //    }

    //}
    #endregion
    protected void ImageButton6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton lnkbtn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)lnkbtn.NamingContainer;
        int j = Convert.ToInt32(row.RowIndex);
        string refernceid = GridView1.Rows[j].Cells[5].Text;
        con.Open();
        string del = "delete from  MyRefenceTbl where refernceid='" + refernceid + "' ";
        SqlCommand cmd2 = new SqlCommand(del, con);
        cmd2.ExecuteNonQuery();
        con.Close();
        request();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
}