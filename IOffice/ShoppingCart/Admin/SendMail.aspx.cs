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

public partial class ShoppingCart_SendMail : System.Web.UI.Page
{
    string body;
    string finalmessage;
    Int32 typeid;
    Int32 warehouseid, companymasterid;
   // SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString"].ConnectionString);
    SqlConnection con;
    string compid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        }
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();

        compid = Session["Comid"].ToString();
        Page.Title = pg.getPageTitle(page);
       
        lblmsg.Visible = false;
        
        if (!IsPostBack)
        {
           
            ViewState["sortOrder"] = "";
           
            ViewState["attachment"] = "no attachmnet";
            

            string strwh = "SELECT WareHouseId,Name,Address,CurrencyId FROM WareHouseMaster where comid='" + compid + "' order by name";
            SqlCommand cmdwh = new SqlCommand(strwh, con);
            SqlDataAdapter adpwh = new SqlDataAdapter(cmdwh);
            DataTable dtwh = new DataTable();
            adpwh.Fill(dtwh);

            ddlWarehouse.DataSource = dtwh;
            ddlWarehouse.DataTextField = "Name";
            ddlWarehouse.DataValueField = "WareHouseId";
            ddlWarehouse.DataBind();
            ddlWarehouse_SelectedIndexChanged(sender, e);
            RadioButtonList1_SelectedIndexChanged(sender, e);
        }
    }
    protected void FillEmailType()
    {
        string sr123 = " SELECT     EmailTypeMaster.EmailTypeId, EmailTypeMaster.Name as EmailTypeName, EmailContentMaster.EmailContentMasterId, EmailContentMaster.EmailContent,    EmailContentMaster.EntryDate  FROM      EmailContentMaster  Right OUTER JOIN   EmailTypeMaster ON EmailContentMaster.EmailTypeId = EmailTypeMaster.EmailTypeId   where EmailTypeMaster.[Whid]='" + ddlWarehouse.SelectedValue + "'";
        SqlCommand cm123 = new SqlCommand(sr123, con);
        cm123.CommandType = CommandType.Text;
        SqlDataAdapter da123 = new SqlDataAdapter(cm123);
        DataTable ds123 = new DataTable();
        da123.Fill(ds123);
        if (ds123.Rows.Count > 0)
        {
            ddlEmailType.DataSource = ds123;
            ddlEmailType.DataTextField = "EmailTypeName";
            ddlEmailType.DataValueField = "EmailTypeId";
            ddlEmailType.DataBind();

            ViewState["EmailDetailString"] = sr123;
        }
        else
        {
            ddlEmailType.Items.Insert(0, "-Select-");
            ddlEmailType.Items[0].Value = "0";
        }
    }
    public StringBuilder getSiteAddressWithWh()
    {
        //DataSet ds544 = (DataSet)getProdcutDetail();
        int whid = Convert.ToInt32(ddlWarehouse.SelectedValue);
        string masterid = "select CompanyWebsiteMasterId from CompanyWebsitMaster left outer join CompanyMaster on CompanyWebsitMaster.CompanyId=CompanyMaster.CompanyId where WHId=" + whid + " and CompanyMaster.Compid='" + compid + "'";
        DataSet ds3 = new DataSet();
        SqlDataAdapter dt3 = new SqlDataAdapter(masterid, con);
        dt3.Fill(ds3);
        if (ds3.Tables[0].Rows.Count > 0)
        {
            companymasterid = Convert.ToInt32(ds3.Tables[0].Rows[0]["CompanyWebsiteMasterId"].ToString());
        }
        SqlCommand cmd = new SqlCommand("Sp_select_SiteaddressWithWH", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("Whid", whid);
        //cmd.Parameters.AddWithValue("compid", companymasterid);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        //return ds;
        StringBuilder strAddress = new StringBuilder();
        if (ds.Rows.Count > 0)
        {
           

            strAddress.Append("<table width=\"100%\"> ");

           

            strAddress.Append("<tr><td> <img src=\"../images/" + ds.Rows[0]["CompanyLogo"].ToString() + "\" \"border=\"0\" Width=\"176px\" Height=\"106px\" / > </td><td align=\"center\"><b><span style=\"color: #996600\">" + ds.Rows[0]["Sitename"].ToString() + "</span></b><Br><b>" + ds.Rows[0]["CompanyName"].ToString() + "</b><Br>" + ds.Rows[0]["Address1"].ToString() + "<Br><b>TollFree:</b>" + ds.Rows[0]["TollFree1"].ToString() + "," + ds.Rows[0]["TollFree2"].ToString() + "<Br><b>Phone:</b>" + ds.Rows[0]["Phone1"].ToString() + "," + ds.Rows[0]["Phone2"].ToString() + "<Br><b>Fax:</b>" + ds.Rows[0]["Fax"].ToString() + "<Br><b>Email:</b>" + ds.Rows[0]["Email"].ToString() + "<Br><b>Website:</b>" + ds.Rows[0]["SiteUrl"].ToString() + " </td></tr>  ");

            strAddress.Append("</table> ");
            ViewState["sitename"] = ds.Rows[0]["Sitename"].ToString();

        }
        return strAddress;
    }
    public void sendmail(string Toaddress,string username)
    {
        int i = 0;
        try
        {
            

            string hhhg = "SELECT      Login_master.username, Login_master.password, " +
                " Login_master.department, Login_master.accesslevel, Login_master.deptid, Login_master.accessid, " +
                      " User_master.Name, Login_master.UserID " +
            " FROM         Login_master LEFT OUTER JOIN " +
                      " User_master ON Login_master.UserID = User_master.UserID left outer join DepartmentmasterMNC on Login_master.department=DepartmentmasterMNC.Id " +
                      " Where User_master.Name = '" + username.ToString() + "' and  User_master.EmailID='" + Toaddress + "'";
            SqlCommand cm = new SqlCommand(hhhg, con);
            cm.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataTable ds = new DataTable();
            da.Fill(ds);

            //StringBuilder HeadingTable = new StringBuilder();
            //HeadingTable = (StringBuilder)getSiteAddress();
            StringBuilder HeadingTable = new StringBuilder();
            HeadingTable = (StringBuilder)getSiteAddressWithWh();
            //lblHeading.Text = HeadingTable.ToString();
            //lblHeading.Visible = true;
            // string body = txtBody.Text;

            string AccountInfo = "";
            if (ds.Rows.Count > 0)
            {
                AccountInfo = " <b><span style=\"color: #996600\">ACCOUNT INFORMATION</span></b><br><b>Username:</b>" + Toaddress.ToString() + "<br><b>Password:</b>" + ClsEncDesc.Decrypted(ds.Rows[0]["password"].ToString()) + " ";
            }

            if (RadioButtonList1.SelectedValue == "0")
            {
                lblmsg.Text = " Mail Sent ";
                string welcometext = getWelcometext();
                lblmsg.Text = " Mail Sent ";
                finalmessage = txtBody.Text;
                body = "" + HeadingTable + "<br><br> Dear " + ds.Rows[0]["Name"].ToString() + ",<br><br>" + welcometext.ToString() + ",<br><br>" + finalmessage.ToString() + " <br>" + AccountInfo.ToString() + "<br><br><br><br><b> Shopping Cart</b> " +
                     "<br><b>Customer Support</b> " +
                      " <br><br> " +
                      " <span style=\"font-size: 10pt; color: #000000; font-family: Arial\"> " +
                      "Sincerely,</span><br><strong><span style=\"color: #996600\"> " + ViewState["sitename"] + " " +
                      " Team</span></strong>";
            }
            else
            {
                finalmessage = txtBody.Text;
                body = "" + HeadingTable + "<br><br> Dear " + ds.Rows[0]["Name"].ToString() + ",<br><br>" + finalmessage.ToString() + " <br>" + AccountInfo.ToString() + "<br><br><br><br><b> Shopping Cart</b> " +
                   "<br><b>Customer Support</b> " +
                    " <br><br> " +
                    " <span style=\"font-size: 10pt; color: #000000; font-family: Arial\"> " +
                    "Sincerely,</span><br><strong><span style=\"color: #996600\"> " + ViewState["sitename"] + " " +
                    " Team</span></strong>";
            }
            string strmal = "  SELECT     OutGoingMailServer,WebMasterEmail, EmailMasterLoginPassword, AdminEmail, WHId " +
                " FROM         CompanyWebsitMaster left outer join CompanyMaster on CompanyWebsitMaster.CompanyId=CompanyMaster.CompanyId WHERE     (WHId = " + Convert.ToInt32(ddlWarehouse.SelectedValue) + ") and CompanyMaster.Compid='" + compid + "' ";
            SqlCommand cmdma = new SqlCommand(strmal, con);
            SqlDataAdapter adpma = new SqlDataAdapter(cmdma);
            DataTable dtma = new DataTable();
            adpma.Fill(dtma);
            if (dtma.Rows.Count > 0)
            {
                string AdminEmail = dtma.Rows[0]["WebMasterEmail"].ToString();// TextAdminEmail.Text;
                //string AdminEmail = txtusmail.Text;
                String Password = dtma.Rows[0]["EmailMasterLoginPassword"].ToString();// TextEmailMasterLoginPassword.Text;

                //string body = "Test Mail Server - TestIwebshop";
                MailAddress to = new MailAddress(Toaddress);
                MailAddress from = new MailAddress(AdminEmail);

                MailMessage objEmail = new MailMessage(from, to);
                objEmail.Subject = txtSubject.Text;

                // if (RadioButtonList1.SelectedValue == "0")
                {
                    objEmail.Body = body.ToString();
                    objEmail.IsBodyHtml = true;

                }
                

                objEmail.Priority = MailPriority.High;
                if (ViewState["flag"] != null)
                {
                    Attachment attachFile = new Attachment(Server.MapPath("..\\Attachment\\") + lblfilename.Text);
                    objEmail.Attachments.Add(attachFile);
                }
                SmtpClient client = new SmtpClient();

                client.Credentials = new NetworkCredential(AdminEmail, Password);
                client.Host = dtma.Rows[0]["OutGoingMailServer"].ToString();

                //client.Credentials = new NetworkCredential(dtma.Rows[0]["AdminEmail"].ToString(), dtma.Rows[0]["EmailMasterLoginPassword"].ToString());//("test1@busiwiz.com", "test");//
                //client.Host = dtma.Rows[0]["OutGoingMailServer"].ToString();
                client.Send(objEmail);
                i += 1;

                lblmsg.Visible = true;
                lblmsg.Text = i + " Mail Sent ";
            }
            else
            {
                lblmsg.Visible = true;
                lblmsg.Text = "No Mail Server Available for this Location ";
            }
           
        }
        catch (Exception tr)
        {
            lblmsg.Visible = true;
            lblmsg.Text = i + " Mail Sent - " + tr.Message;
        }
       
    }

    public StringBuilder getSiteAddress()
    {
        
        SqlCommand cmd = new SqlCommand("Sp_select_Siteaddress", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("compid", compid);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        //return ds;
        // string path = Server.MapPath(@"../ShoppingCart/images/logo.gif"); 
        StringBuilder strAddress = new StringBuilder();
        strAddress.Append(" ");
        if (ds.Rows.Count > 0)
        {
            strAddress.Append("<table width=\"100%\"> ");
            //ImageUrl="~/ShoppingCart/images/logo.png"
            strAddress.Append("<tr><td>  </td><td align=\"center\"><b><span style=\"color: #996600\">" + ds.Rows[0]["Sitename"].ToString() + "</span></b><Br><b>" + ds.Rows[0]["CompanyName"].ToString() + "</b><Br>" + ds.Rows[0]["Address1"].ToString() + "<Br><b>TollFree:</b>" + ds.Rows[0]["TollFree1"].ToString() + "," + ds.Rows[0]["TollFree2"].ToString() + "<Br><b>Phone:</b>" + ds.Rows[0]["Phone1"].ToString() + "," + ds.Rows[0]["Phone2"].ToString() + "<Br><b>Fax:</b>" + ds.Rows[0]["Fax"].ToString() + "<Br><b>Email:</b>" + ds.Rows[0]["Email"].ToString() + "<Br><b>Website:</b>" + ds.Rows[0]["SiteUrl"].ToString() + " </td></tr>  ");

            strAddress.Append("</table> ");
            ViewState["sitename"] = ds.Rows[0]["Sitename"].ToString();
            return strAddress;
        }
        return strAddress;
    }
    public void insertMail(int userid)
    {
    }
    public string getWelcometext()
    {
        string fetchty = "select * from  EmailTypeMaster where Name='" + ddlEmailType.SelectedItem.ToString() + "' and EmailTypeMaster.Compid='" + compid + "'";
        DataSet ds1 = new DataSet();
        SqlDataAdapter dt1 = new SqlDataAdapter(fetchty, con);
        dt1.Fill(ds1);
        if (ds1.Tables[0].Rows.Count > 0)
        {
            typeid = Convert.ToInt32(ds1.Tables[0].Rows[0]["EmailTypeId"].ToString());
        }
        string fetchwarehouse = "select * from WareHouseMaster where Name='" + ddlWarehouse.SelectedItem.ToString() + "' and WareHouseMaster.comid='" + compid + "' ";
        DataSet ds2 = new DataSet();
        SqlDataAdapter dt2 = new SqlDataAdapter(fetchwarehouse, con);
        dt2.Fill(ds2);
        if (ds2.Tables[0].Rows.Count > 0)
        {
            warehouseid = Convert.ToInt32(ds2.Tables[0].Rows[0]["WareHouseId"].ToString());
        }
        string masterid = "select CompanyWebsiteMasterId from CompanyWebsitMaster left outer join CompanyMaster on CompanyWebsitMaster.CompanyId=CompanyMaster.CompanyId where WHId=" + warehouseid + " and CompanyMaster.Compid='" + compid + "'";
        DataSet ds3 = new DataSet();
        SqlDataAdapter dt3 = new SqlDataAdapter(masterid, con);
        dt3.Fill(ds3);
        if (ds3.Tables[0].Rows.Count > 0)
        {
            companymasterid = Convert.ToInt32(ds3.Tables[0].Rows[0]["CompanyWebsiteMasterId"].ToString());
        }
        string str =" SELECT EmailContentMaster.EmailContent, EmailContentMaster.EntryDate, CompanyWebsitMaster.SiteUrl, EmailTypeMaster.EmailTypeId " +
                    " FROM CompanyWebsitMaster INNER JOIN " +
                    " EmailContentMaster ON CompanyWebsitMaster.CompanyWebsiteMasterId = EmailContentMaster.CompanyWebsiteMasterId INNER JOIN " +
                    " EmailTypeMaster ON EmailContentMaster.EmailTypeId = EmailTypeMaster.EmailTypeId left outer join CompanyMaster on CompanyWebsitMaster.CompanyId=CompanyMaster.CompanyId" +
                    " WHERE (EmailTypeMaster.EmailTypeId = " + typeid + ")  and (CompanyMaster.Compid='" + compid + "' )";

        //  " ORDER BY EmailContentMaster.EntryDate DESC ";
        SqlDataAdapter adp = new SqlDataAdapter(str, con);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        string welcometext = "";
        if (ds.Rows.Count > 0)
        {
            welcometext = ds.Rows[0]["EmailContent"].ToString();
        }
        return welcometext;

       
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {

           
              
            SqlCommand cmd = new SqlCommand("insert into EmailCircularMaster(Subject,Message,Attachmentpath) values('" + txtSubject.Text + "','" + txtBody.Text + "','" + ViewState["attachment"].ToString() + "')", con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
           
            cmd.ExecuteNonQuery();
            con.Close();
            SqlDataAdapter adp = new SqlDataAdapter("select max(EmailCircularMasterId) as masterid from EmailCircularMaster", con);
            DataTable ds = new DataTable();
            adp.Fill(ds);
            int masterid = Convert.ToInt32(ds.Rows[0]["masterid"]);
            if (GridView1.Rows.Count > 0)
            {
                int ik = 0;
                foreach (GridViewRow gdr in GridView1.Rows)
                {
                    string usermailid = gdr.Cells[3].Text.Trim();
                    string usernm = gdr.Cells[2].Text.Trim();
                    CheckBox chk = (CheckBox)gdr.Cells[1].FindControl("CheckBox1");
                    if (chk.Checked == true)
                    {
                        int key = Convert.ToInt32(GridView1.DataKeys[gdr.RowIndex].Value.ToString());
                        if (usermailid != "" || usermailid != " " || usermailid != "&nbsp;")
                        {
                            try
                            {

                                sendmail(usermailid, usernm);
                                SqlCommand cmd2 = new SqlCommand("insert into EmailCircularDetail(EmailCircularMasterId,UserID) values('" + masterid + "','" + key + "')", con);
                                if (con.State.ToString() != "Open")
                                {
                                    con.Open();
                                }
                                cmd2.ExecuteNonQuery();
                                con.Close();

                                lblmsg.Visible = true;
                                ik = ik + 1;
                                //lblmsg.Text = ik +"Mail sent Successfully";
                                //txtBody.Text = "";
                                //txtSubject.Text = "";
                                //GridView1.DataSource = null;
                                //GridView1.DataBind();
                                //TextBox1.Text = "";
                                imgbtnreset_Click(sender, e);

                            }
                            catch (Exception ex)
                            {
                                lblmsg.Visible = true;
                               lblmsg.Text = ik + "Error:" + ex.Message.ToString();
                                return;
                            }
                        }
                    }
                }
            }
        }
        catch (Exception tr)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Error :" + tr.Message;
        }
   
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        string filepath = "";
        String filename = "";
        if (FileUpload1.HasFile)
        {
            filename = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "_" + FileUpload1.FileName;
            FileUpload1.SaveAs(Server.MapPath("..\\Attachment\\") + filename);
            string fileimg = "/Attachment/" + filename;
                // FileUpload1.PostedFile.SaveAs(Server.MapPath("..\\Attachment\\") + FileUpload1.FileName);
                //filepath = "/Attachment/" + FileUpload1.FileName;
                //fpdsm.SaveAs(Server.MapPath("..\\Thumbnail\\") + fpdsm.FileName.ToString());
                //  string fileSmallName = "Thumbnail/" + fpdsm.FileName.ToString();
                if (FileUpload1.HasFile != false)
                {

                    string strimg = "select Attachmentpath from EmailCircularMaster where Attachmentpath='" + fileimg.ToString() + "'";

                    SqlCommand cmdimg = new SqlCommand(strimg, con);
                    SqlDataAdapter adpimg = new SqlDataAdapter(cmdimg);
                    DataTable dtimg = new DataTable();
                    adpimg.Fill(dtimg);

                    if (dtimg.Rows.Count > 0)
                    {
                        FileUpload1.PostedFile.SaveAs(Server.MapPath("..\\Attachment\\") + 1 + filename);
                        filepath = "/Attachment/" + 1 + filename;
                        lblfilename.Text = +1 + filename;

                    }
                    else
                    {
                        FileUpload1.PostedFile.SaveAs(Server.MapPath("..\\Attachment\\") + filename);
                        filepath = "/Attachment/" + filename;
                        lblfilename.Text = filename;
                    }
                }



            ViewState["filename"] = filename;
            lblfilename.Visible = true;
            
            FileUpload1.Visible = false;
            ImageButton2.Visible = true;
            ViewState["attachment"] = filepath.ToString();
            ViewState["flag"] = "1";
        }
        else
        {
            ViewState["attachment"] = "No Attachment";
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Sort")
        {
            return;
        }
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        
        ImageClickEventArgs eererer = new ImageClickEventArgs(1, 1);
        ImageButton3_Click(sender, eererer);
        
    }
    public string sortOrder
    {
        get
        {
            if (ViewState["sortOrder"].ToString() == "desc")
            {
                ViewState["sortOrder"] = "asc";
            }
            else
            {
                ViewState["sortOrder"] = "desc";
            }
            return ViewState["sortOrder"].ToString();
        }
        set
        {
            ViewState["sortOrder"] = value;
        }
    }
    protected void fillPartytype()
    {
        string strg = " SELECT PartyTypeId ,PartType  FROM PartytTypeMaster where compid='" + compid + "' ";
        SqlCommand cmdg = new SqlCommand(strg, con);
        //cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter adpg = new SqlDataAdapter(cmdg);
        DataTable dsg = new DataTable();
        adpg.Fill(dsg);
        if (dsg.Rows.Count > 0)
        {
            ddlPartyType.DataSource = dsg;
            ddlPartyType.DataTextField = "PartType";
            ddlPartyType.DataValueField = "PartyTypeId";
            ddlPartyType.DataBind();
            ddlPartyType.Items.Insert(0, "All");
            ddlPartyType.Items[0].Value = "0";

        }
    }

    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedValue == "0")
        {
            Panel3.Visible = true;
        }
        else if (RadioButtonList1.SelectedValue == "1")
        {
            Panel3.Visible = false;
        }
    }
    protected void ddlEmailType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEmailType.SelectedIndex > 0)
        {
            string emaildetail = "";
            if (ViewState["EmailDetailString"] != null)
            {
                emaildetail = ViewState["EmailDetailString"].ToString() + " and EmailTypeMaster.EmailTypeId='" + ddlEmailType.SelectedValue + "' ";
                SqlCommand cmdw = new SqlCommand(emaildetail, con);
                SqlDataAdapter adpw = new SqlDataAdapter(cmdw);
                DataTable dtw = new DataTable();
                adpw.Fill(dtw);
                if (dtw.Rows.Count > 0)
                {
                    txtBody.Text =Convert.ToString(dtw.Rows[0]["EmailContent"]);

                }
            }
        }
    }
    protected void imgbtnreset_Click(object sender, EventArgs e)
    {
        TextBox1.Text = "";
        
        ddlPartyType.SelectedIndex = 0;
        ddlstatus.SelectedIndex = 0;
        lblfilename.Text = "";
        FileUpload1.Visible = true;
        ddlEmailType.SelectedIndex = -1;
        txtSubject.Text = "";
        txtBody.Text = "";
    }
    protected void imgbtncancel_Click(object sender, EventArgs e)
    {
        txtBody.Text = "";
        txtSubject.Text = "";
        ddlEmailType.SelectedIndex = -1;
        //ddlPartyType.SelectedIndex = 0;
        //ddlstatus.SelectedIndex = 0;
        lblfilename.Text = "";
        FileUpload1.Visible = true;
    }
    protected void ddlWarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillEmailType();
        fillPartytype();
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
      
        lblmsg.Visible = false;

        string str2 = " SELECT User_master.Name, User_master.UserID, User_master.zipcode, User_master.Phoneno, User_master.EmailID AS Emailid, User_master.Username,User_master.Active,  " +
                      " StateMasterTbl.StateName, Party_master.Country, CountryMaster.CountryName, User_master.City, PartytTypeMaster.PartType,PartytTypeMaster.PartyTypeId " +
                      " FROM Party_master LEFT OUTER JOIN " +
                      " PartytTypeMaster ON Party_master.PartyTypeId = PartytTypeMaster.PartyTypeId RIGHT OUTER JOIN " +
                      " StateMasterTbl RIGHT OUTER JOIN " +
                      " User_master LEFT OUTER JOIN " +
                      " CountryMaster ON User_master.Country = CountryMaster.CountryId ON StateMasterTbl.StateId = User_master.State ON Party_master.PartyID = User_master.PartyID  where PartytTypeMaster.compid='" + compid + "' and Party_master.whid='"+ddlWarehouse.SelectedValue+"' ";
        if (ddlPartyType.SelectedIndex > 0 && TextBox1.Text.Length > 0)
        {
            str2 += "  and PartytTypeMaster.PartyTypeId ='" + ddlPartyType.SelectedValue + "' " +
                    " and User_master.Name like '%" + TextBox1.Text + "%' or User_master.Phoneno='" + TextBox1.Text + "' ";
        }
        else if (TextBox1.Text.Length > 0 && ddlPartyType.SelectedIndex <= 0)
        {
            str2 += " and User_master.Name like '%" + TextBox1.Text + "%' or User_master.Phoneno='" + TextBox1.Text + "' ";
        }
        else if (TextBox1.Text.Length <= 0 && ddlPartyType.SelectedIndex > 0)
        {
            str2 += " and PartytTypeMaster.PartyTypeId ='" + ddlPartyType.SelectedValue + "' ";
        }
        else
        {
        }
        if (ddlstatus.SelectedItem.Text == "Active")
        {
            str2 += " and User_master.Active='1'";
        }
        if (ddlstatus.SelectedItem.Text == "InActive")
        {
            str2 += " and User_master.Active='0'";
        }
        //else 
        SqlCommand cmd2 = new SqlCommand(str2, con);
        //cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd2);
        DataTable ds = new DataTable();
        da.Fill(ds);

        if (ds.Rows.Count > 0)
        {
            GridView1.DataSource = ds;

            DataView myDataView = new DataView();
            myDataView = ds.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }
            //GridView1.DataSource = myDataView;
            //GridView1.DataBind();
            GridView1.DataBind();
            lblmsg.Visible = false;
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();

            
        }
       
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //try
        //{
        //    if (GridView1.Rows.Count > 0)
        //    {
        //        CheckBox cbHeader = (CheckBox)GridView1.HeaderRow.FindControl("chkAll");
        //        cbHeader.Attributes["onclick"] = "ChangeAllCheckBoxStates(this.checked);";
        //        List<string> ArrayValues = new List<string>();
        //        ArrayValues.Add(string.Concat("'", cbHeader.ClientID, "'"));
        //        foreach (GridViewRow gvr in GridView1.Rows)
        //        {
        //            CheckBox cb = (CheckBox)gvr.FindControl("CheckBox1");
        //            cb.Attributes["onclick"] = "ChangeHeaderAsNeeded();";
        //            ArrayValues.Add(string.Concat("'", cb.ClientID, "'"));
        //        }
        //        CheckBoxIDsArray.Text = "<script type='text/javascript'>" + "\n" + "<!--" + "\n" + String.Concat("var CheckBoxIDs =  new Array(", String.Join(",", ArrayValues.ToArray()), ");") + "\n // -->" + "\n" + "</script>";

        //    }
        //    else
        //    {
        //    }
        //}
        //catch (Exception ex)
        //{
        //    lblmsg.Visible = true;
        //    lblmsg.Text = "Error in databound : " + ex.Message.ToString();
        //}
    }
}
