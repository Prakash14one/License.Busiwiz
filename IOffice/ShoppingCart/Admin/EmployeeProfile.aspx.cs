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
using System.Text;
using System.Net;
using System.Net.Mail;

public partial class ShoppingCart_Admin_EmployeeProfile : System.Web.UI.Page
{
    SqlConnection con;
    SqlConnection conLicense = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    string compid;
    String qryStr;
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
        if (!Page.IsPostBack)
        {
            Pagecontrol.dypcontrol(Page, page);
            Label20.Text = "";
            ViewState["sortOrder"] = "";
            if (Request.QueryString["EmployeeMasterID"] != null)
            {
                int id = Convert.ToInt32(Request.QueryString["EmployeeMasterID"]);

                ViewState["Newemployeeid"] = id;

                existinglogininfo.Visible = true;

                string newuserid = " select userid from user_master inner join employeemaster on employeemaster.partyid=user_master.partyid where employeemaster.employeemasterid='" + id + "'";
                SqlDataAdapter danew = new SqlDataAdapter(newuserid, con);
                DataTable dtnew = new DataTable();
                danew.Fill(dtnew);

                ViewState["NewUserID"] = dtnew.Rows[0]["userid"].ToString();

                SqlDataAdapter exisda = new SqlDataAdapter("select username,password from login_master where UserID='" + ViewState["NewUserID"] + "'", con);
                DataTable exisdt = new DataTable();
                exisda.Fill(exisdt);

                lblexistuid.Text = exisdt.Rows[0]["username"].ToString();
                string existpwd = exisdt.Rows[0]["password"].ToString();
                lblexistpwd.Text=ClsEncDesc.Decrypted(existpwd);
               
                SqlDataAdapter da = new SqlDataAdapter("select Answer from SecurityQuestion inner join User_master on User_master.userid=SecurityQuestion.userid  inner join employeemaster on employeemaster.partyid=User_master.partyid   where employeemaster.employeemasterid='" + id + "'", con);
                DataTable dtanswer = new DataTable();
                da.Fill(dtanswer);

                if (dtanswer.Rows.Count > 0)
                {
                    pnlsecurityque.Visible = true;
                }
                else
                {
                    Pnlsecurityblank.Visible = true;
                }

                fillquestion();
                fillempflm123();
                txtcontrol(false);
                fillgrid123();
                EmergencyContactInfo123();
                fillemployee123();
                ddluidwd_SelectedIndexChanged(sender, e);
            }
            else
            {
                fillcountry();
                fillstate();
                fillcity();
                SqlDataAdapter da = new SqlDataAdapter("select Answer from SecurityQuestion where UserId='" + Session["userid"] + "'", con);
                DataTable dtanswer = new DataTable();
                da.Fill(dtanswer);

                if (dtanswer.Rows.Count > 0)
                {
                    pnlsecurityque.Visible = true;
                }
                else
                {
                    Pnlsecurityblank.Visible = true;
                }
                fillquestion();
                fillemployee();
                txtcontrol(false);
                fillgrid();
                EmergencyContactInfo();
                fillempflm();

                ddluidwd_SelectedIndexChanged(sender, e);
            }
        }
    }
    public DataSet fillddl(String qry)
    {
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;
    }
    public void fillddlOther(DropDownList ddl, String dtf, String dvf)
    {
        ddl.DataTextField = dtf;
        ddl.DataValueField = dvf;
        ddl.DataBind();

    }
    protected void fillcountry()
    {

        qryStr = "select CountryId,CountryName from CountryMaster order by CountryName";
        ddlcountry.DataSource = (DataSet)fillddl(qryStr);
        fillddlOther(ddlcountry, "CountryName", "CountryId");

        ddlcountry.Items.Insert(0, "-Select-");
        ddlcountry.Items[0].Value = "0";

        ddlstates.Items.Insert(0, "-Select-");
        ddlstates.Items[0].Value = "0";
        ddlcity.Items.Insert(0, "-Select-");
        ddlcity.Items[0].Value = "0";
    }
    protected void fillstate()
    {
        ddlstates.Items.Clear();
        qryStr = "select StateId,StateName from StateMasterTbl where CountryId=" + ddlcountry.SelectedValue + " order by StateName";
        ddlstates.DataSource = (DataSet)fillddl(qryStr);
        fillddlOther(ddlstates, "StateName", "StateId");
        ddlstates.Items.Insert(0, "-Select-");
        ddlstates.Items[0].Value = "0";
    }
    protected void fillcity()
    {
        ddlcity.Items.Clear();
        qryStr = "select CityId,CityName from CityMasterTbl where StateId=" + ddlstates.SelectedValue + " order by CityName";
        ddlcity.DataSource = (DataSet)fillddl(qryStr);
        fillddlOther(ddlcity, "CityName", "CityId");
        ddlcity.Items.Insert(0, "-Select-");
        ddlcity.Items[0].Value = "0";
    }
    protected void fillemployee()
    
    {
        string str = "select Login_master.UserID,Login_master.username,User_master.UserID,User_master.PartyID," +
                    " EmployeeMaster.EmployeeMasterID,Party_master.PartyID,Party_master.id,EmployeeMaster.SuprviserId " +
                    " from Login_master inner join User_master on Login_master.UserID=User_master.UserID  " +
                    " inner join Party_master on User_master.PartyID=Party_master.PartyID inner join " +
                    " EmployeeMaster on User_master.PartyID=EmployeeMaster.PartyID where Login_master.UserID='" + Session["userid"] + "' and Party_master.id='" + compid + "'";
        DataSet ds = (DataSet)fillddl(str);
        ViewState["empid"] = ds.Tables[0].Rows[0]["EmployeeMasterID"].ToString();
        ViewState["partyid"] = ds.Tables[0].Rows[0]["PartyID"].ToString();
        ViewState["userid"] = ds.Tables[0].Rows[0]["UserID"].ToString();

        string str1 = "Select EmployeeMaster.*,Party_master.Zipcode,Party_master.Fax,Party_master.Website,User_master.Extention from EmployeeMaster inner join Party_master on EmployeeMaster.PartyID = Party_master.PartyID inner join User_master on User_master.PartyID = Party_master.PartyID where EmployeeMasterID='" + ViewState["empid"] + "'";
        DataSet ds1 = (DataSet)fillddl(str1);
        if (ds1.Tables[0].Rows.Count > 0)
        {
            lblempname.Text = ds1.Tables[0].Rows[0]["EmployeeName"].ToString();
            lblempedit.Text = ds1.Tables[0].Rows[0]["EmployeeName"].ToString();
            lblem.Text = "List of  " + ds1.Tables[0].Rows[0]["EmployeeName"].ToString() + "'s Addresses";
            lbladdress.Text = ds1.Tables[0].Rows[0]["Address"].ToString();
            txtaddress.Text = ds1.Tables[0].Rows[0]["Address"].ToString();

            string strcountry = "select CountryName from CountryMaster where CountryId='" + ds1.Tables[0].Rows[0]["CountryId"] + "'";
            DataSet dscountry = (DataSet)fillddl(strcountry);
            if (dscountry.Tables[0].Rows.Count > 0)
            {
                lblcountry.Text = dscountry.Tables[0].Rows[0]["CountryName"].ToString();
            }
            fillcountry();
            ddlcountry.SelectedIndex = ddlcountry.Items.IndexOf(ddlcountry.Items.FindByValue(ds1.Tables[0].Rows[0]["CountryId"].ToString()));


            string strstate = "select StateName from StateMasterTbl where StateId='" + ds1.Tables[0].Rows[0]["StateId"] + "'";
            DataSet dsstate = (DataSet)fillddl(strstate);
            if (dsstate.Tables[0].Rows.Count > 0)
            {
                lblstate.Text = dsstate.Tables[0].Rows[0]["StateName"].ToString();
            }
            fillstate();
            ddlstates.SelectedIndex = ddlstates.Items.IndexOf(ddlstates.Items.FindByValue(ds1.Tables[0].Rows[0]["StateId"].ToString()));

            string strcity = "select CityName from CityMasterTbl where CityId='" + ds1.Tables[0].Rows[0]["City"] + "'";
            DataSet dscity = (DataSet)fillddl(strcity);
            if (dscity.Tables[0].Rows.Count > 0)
            {
                lblcity.Text = dscity.Tables[0].Rows[0]["CityName"].ToString();
            }
            fillcity();
            ddlcity.SelectedIndex = ddlcity.Items.IndexOf(ddlcity.Items.FindByValue(ds1.Tables[0].Rows[0]["City"].ToString()));
            if (ds1.Tables[0].Rows[0]["Zipcode"].ToString() != "")
            {
                lblzipcode.Text = ds1.Tables[0].Rows[0]["Zipcode"].ToString();
                txtzipcode.Text = ds1.Tables[0].Rows[0]["Zipcode"].ToString();
            }
            if (ds1.Tables[0].Rows[0]["ContactNo"].ToString() != "")
            {
                lblphone.Text = ds1.Tables[0].Rows[0]["ContactNo"].ToString();
                txtphone.Text = ds1.Tables[0].Rows[0]["ContactNo"].ToString();
            }
            //lblext.Text = ds1.Tables[0].Rows[0]["Extention"].ToString();
            //txtext.Text = ds1.Tables[0].Rows[0]["Extention"].ToString();
            if (ds1.Tables[0].Rows[0]["Fax"].ToString() != "")
            {
                lblfax.Text = ds1.Tables[0].Rows[0]["Fax"].ToString();
                txtfax.Text = ds1.Tables[0].Rows[0]["Fax"].ToString();
            }
            if (ds1.Tables[0].Rows[0]["Email"].ToString() != "")
            {
                lblemail.Text = ds1.Tables[0].Rows[0]["Email"].ToString();
                txtemail.Text = ds1.Tables[0].Rows[0]["Email"].ToString();
            }
            //lblwebsite.Text = ds1.Tables[0].Rows[0]["Website"].ToString();
            //txtwebsite.Text = ds1.Tables[0].Rows[0]["Website"].ToString();

            //lbldesc.Text = ds1.Tables[0].Rows[0]["Description"].ToString();
            //txtdescs.Text = ds1.Tables[0].Rows[0]["Description"].ToString();
        }
        fillempquestion();

    }


    protected void fillemployee123()
    {

        string str = "select Login_master.UserID,Login_master.username,User_master.UserID,User_master.PartyID," +
                   " EmployeeMaster.EmployeeMasterID,Party_master.PartyID,Party_master.id,EmployeeMaster.SuprviserId " +
                   " from Login_master inner join User_master on Login_master.UserID=User_master.UserID  " +
                   " inner join Party_master on User_master.PartyID=Party_master.PartyID inner join " +
                   " EmployeeMaster on User_master.PartyID=EmployeeMaster.PartyID where Login_master.UserID='" + ViewState["NewUserID"] + "' and Party_master.id='" + compid + "'";
        DataSet ds = (DataSet)fillddl(str);
        //ViewState["empid"] = ds.Tables[0].Rows[0]["EmployeeMasterID"].ToString();
        ViewState["NewPartyID"] = ds.Tables[0].Rows[0]["PartyID"].ToString();
        //ViewState["userid"] = ds.Tables[0].Rows[0]["UserID"].ToString();

        string str1 = "Select EmployeeMaster.*,Party_master.Zipcode,Party_master.Fax,Party_master.Website,User_master.Extention from EmployeeMaster inner join Party_master on EmployeeMaster.PartyID = Party_master.PartyID inner join User_master on User_master.PartyID = Party_master.PartyID where EmployeeMasterID='" + ViewState["Newemployeeid"] + "'";
        DataSet ds1 = (DataSet)fillddl(str1);
        if (ds1.Tables[0].Rows.Count > 0)
        {
            lblempname.Text = ds1.Tables[0].Rows[0]["EmployeeName"].ToString();
            lblempedit.Text = ds1.Tables[0].Rows[0]["EmployeeName"].ToString();
            lblem.Text = "List of  " + ds1.Tables[0].Rows[0]["EmployeeName"].ToString() + "'s Addresses";
            lbladdress.Text = ds1.Tables[0].Rows[0]["Address"].ToString();
            txtaddress.Text = ds1.Tables[0].Rows[0]["Address"].ToString();

            string strcountry = "select CountryName from CountryMaster where CountryId='" + ds1.Tables[0].Rows[0]["CountryId"] + "'";
            DataSet dscountry = (DataSet)fillddl(strcountry);
            if (dscountry.Tables[0].Rows.Count > 0)
            {
                lblcountry.Text = dscountry.Tables[0].Rows[0]["CountryName"].ToString();
            }
            fillcountry();
            ddlcountry.SelectedIndex = ddlcountry.Items.IndexOf(ddlcountry.Items.FindByValue(ds1.Tables[0].Rows[0]["CountryId"].ToString()));


            string strstate = "select StateName from StateMasterTbl where StateId='" + ds1.Tables[0].Rows[0]["StateId"] + "'";
            DataSet dsstate = (DataSet)fillddl(strstate);
            if (dsstate.Tables[0].Rows.Count > 0)
            {
                lblstate.Text = dsstate.Tables[0].Rows[0]["StateName"].ToString();
            }
            fillstate();
            ddlstates.SelectedIndex = ddlstates.Items.IndexOf(ddlstates.Items.FindByValue(ds1.Tables[0].Rows[0]["StateId"].ToString()));

            string strcity = "select CityName from CityMasterTbl where CityId='" + ds1.Tables[0].Rows[0]["City"] + "'";
            DataSet dscity = (DataSet)fillddl(strcity);
            if (dscity.Tables[0].Rows.Count > 0)
            {
                lblcity.Text = dscity.Tables[0].Rows[0]["CityName"].ToString();
            }
            fillcity();
            ddlcity.SelectedIndex = ddlcity.Items.IndexOf(ddlcity.Items.FindByValue(ds1.Tables[0].Rows[0]["City"].ToString()));
            if (ds1.Tables[0].Rows[0]["Zipcode"].ToString() != "")
            {
                lblzipcode.Text = ds1.Tables[0].Rows[0]["Zipcode"].ToString();
                txtzipcode.Text = ds1.Tables[0].Rows[0]["Zipcode"].ToString();
            }
            if (ds1.Tables[0].Rows[0]["ContactNo"].ToString() != "")
            {
                lblphone.Text = ds1.Tables[0].Rows[0]["ContactNo"].ToString();
                txtphone.Text = ds1.Tables[0].Rows[0]["ContactNo"].ToString();
            }

            if (ds1.Tables[0].Rows[0]["Fax"].ToString() != "")
            {
                lblfax.Text = ds1.Tables[0].Rows[0]["Fax"].ToString();
                txtfax.Text = ds1.Tables[0].Rows[0]["Fax"].ToString();
            }
            if (ds1.Tables[0].Rows[0]["Email"].ToString() != "")
            {
                lblemail.Text = ds1.Tables[0].Rows[0]["Email"].ToString();
                txtemail.Text = ds1.Tables[0].Rows[0]["Email"].ToString();
            }

        }
        fillempquestion123();
    }

    protected void fillempquestion()
    {
        string strquestion = "Select SecurityQuestionMaster.*,SecurityQuestion.UserId,SecurityQuestion.Answer from SecurityQuestionMaster inner join SecurityQuestion on SecurityQuestionMaster.id = SecurityQuestion.SequrityQueId where SecurityQuestion.UserId = '" + Session["userid"] + "'";
        DataSet dsquestion = (DataSet)fillddl(strquestion);
        if (dsquestion.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dsquestion.Tables[0].Rows.Count; i++)
            {
                if (i == 0)
                {
                    lblque1.Text = dsquestion.Tables[0].Rows[0]["QueName"].ToString();
                    lblans1.Text = dsquestion.Tables[0].Rows[0]["Answer"].ToString();
                    txtans1.Text = dsquestion.Tables[0].Rows[0]["Answer"].ToString();
                    ddlque1.SelectedIndex = ddlque1.Items.IndexOf(ddlque1.Items.FindByValue(dsquestion.Tables[0].Rows[0]["id"].ToString()));
                }

                if (i == 1)
                {
                    lblque2.Text = dsquestion.Tables[0].Rows[1]["QueName"].ToString();
                    lblans2.Text = dsquestion.Tables[0].Rows[1]["Answer"].ToString();
                    txtans2.Text = dsquestion.Tables[0].Rows[1]["Answer"].ToString();
                    ddlque2.SelectedIndex = ddlque2.Items.IndexOf(ddlque2.Items.FindByValue(dsquestion.Tables[0].Rows[1]["id"].ToString()));
                }

                if (i == 2)
                {
                    lblque3.Text = dsquestion.Tables[0].Rows[2]["QueName"].ToString();
                    lblans3.Text = dsquestion.Tables[0].Rows[2]["Answer"].ToString();
                    txtans3.Text = dsquestion.Tables[0].Rows[2]["Answer"].ToString();
                    ddlque3.SelectedIndex = ddlque3.Items.IndexOf(ddlque3.Items.FindByValue(dsquestion.Tables[0].Rows[2]["id"].ToString()));
                }
            }
        }
    }


    protected void fillempquestion123()
    {
        string strquestion = "Select SecurityQuestionMaster.*,SecurityQuestion.UserId,SecurityQuestion.Answer from SecurityQuestionMaster inner join SecurityQuestion on SecurityQuestionMaster.id = SecurityQuestion.SequrityQueId where SecurityQuestion.UserId = '" + ViewState["NewUserID"] + "'";
        DataSet dsquestion = (DataSet)fillddl(strquestion);
        if (dsquestion.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dsquestion.Tables[0].Rows.Count; i++)
            {
                if (i == 0)
                {
                    lblque1.Text = dsquestion.Tables[0].Rows[0]["QueName"].ToString();
                    lblans1.Text = dsquestion.Tables[0].Rows[0]["Answer"].ToString();
                    txtans1.Text = dsquestion.Tables[0].Rows[0]["Answer"].ToString();
                    ddlque1.SelectedIndex = ddlque1.Items.IndexOf(ddlque1.Items.FindByValue(dsquestion.Tables[0].Rows[0]["id"].ToString()));
                }

                if (i == 1)
                {
                    lblque2.Text = dsquestion.Tables[0].Rows[1]["QueName"].ToString();
                    lblans2.Text = dsquestion.Tables[0].Rows[1]["Answer"].ToString();
                    txtans2.Text = dsquestion.Tables[0].Rows[1]["Answer"].ToString();
                    ddlque2.SelectedIndex = ddlque2.Items.IndexOf(ddlque2.Items.FindByValue(dsquestion.Tables[0].Rows[1]["id"].ToString()));
                }

                if (i == 2)
                {
                    lblque3.Text = dsquestion.Tables[0].Rows[2]["QueName"].ToString();
                    lblans3.Text = dsquestion.Tables[0].Rows[2]["Answer"].ToString();
                    txtans3.Text = dsquestion.Tables[0].Rows[2]["Answer"].ToString();
                    ddlque3.SelectedIndex = ddlque3.Items.IndexOf(ddlque3.Items.FindByValue(dsquestion.Tables[0].Rows[2]["id"].ToString()));
                }
            }
        }
    }

    protected void txtcontrol(Boolean t)
    {

        txtaddress.Visible = t;
        ddlcountry.Visible = t;
        ddlstates.Visible = t;
        ddlcity.Visible = t;
        txtzipcode.Visible = t;
        txtphone.Visible = t;
        //txtext.Visible = t;
        txtemail.Visible = t;
        //txtwebsite.Visible = t;
        txtfax.Visible = t;
        //txtdescs.Visible = t;                                   
    }
    protected void lblcontrol(Boolean t)
    {

        lbladdress.Visible = t;
        lblcountry.Visible = t;
        lblstate.Visible = t;
        lblcity.Visible = t;
        lblzipcode.Visible = t;
        lblphone.Visible = t;
        //lblext.Visible = t;
        lblemail.Visible = t;
        //lblwebsite.Visible = t;
        lblfax.Visible = t;
        //lbldesc.Visible = t;

    }

    protected void fillquestion()
    {
        

        string str = "Select * from SecurityQuestionMaster where Active='1'";
        ddlque1.DataSource = (DataSet)fillddl(str);
        fillddlOther(ddlque1, "QueName", "id");
        ddlque1.Items.Insert(0, "-Select any question-");
        ddlque1.Items[0].Value = "0";

        ddlque2.DataSource = (DataSet)fillddl(str);
        fillddlOther(ddlque2, "QueName", "id");
        ddlque2.Items.Insert(0, "-Select any question-");
        ddlque2.Items[0].Value = "0";

        ddlque3.DataSource = (DataSet)fillddl(str);
        fillddlOther(ddlque3, "QueName", "id");
        ddlque3.Items.Insert(0, "-Select any questions-");
        ddlque3.Items[0].Value = "0";
    }
    protected void btnchange_Click(object sender, EventArgs e)
    {
        lblcontrol(false);
        txtcontrol(true);
        Panel2.Visible = false;
        Panel3.Visible = true;
        btnupdate.Visible = true;
        btncancel.Visible = true;
        btnchange.Visible = false;
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["EmployeeMasterID"] != null)
        {
            int id = Convert.ToInt32(Request.QueryString["EmployeeMasterID"]);

           // ViewState["NewUserID"]
           //     ViewState["NewPartyID"]


            string strpartyaddupdate = "update PartyAddressTbl set AddressActiveStatus='0' where UserId='" + ViewState["NewUserID"] + "'";
            SqlCommand cmdaddupdate = new SqlCommand(strpartyaddupdate, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdaddupdate.ExecuteNonQuery();
            con.Close();

             string strpartyadd = "insert into PartyAddressTbl(PartyMasterId,Address,Country,State,City,email,Phone,fax,UserId,datetime,AddressActiveStatus,zipcode,verified) values ('" + ViewState["NewPartyID"] + "','" + txtaddress.Text + "','" + ddlcountry.SelectedValue + "','" + ddlstates.SelectedValue + "','" + ddlcity.SelectedValue + "','" + txtemail.Text + "','" + txtphone.Text + "','" + txtfax.Text + "','" + ViewState["NewUserID"] + "','" + System.DateTime.Now.ToString() + "','1','" + txtzipcode.Text + "','0')";
            SqlCommand cmdpartyadd = new SqlCommand(strpartyadd, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdpartyadd.ExecuteNonQuery();
            con.Close();

            string strempupdate = "update EmployeeMaster set Address='" + txtaddress.Text + "',CountryId='" + ddlcountry.SelectedValue + "',StateId='" + ddlstates.SelectedValue + "',City='" + ddlcity.SelectedValue + "',ContactNo='" + txtphone.Text + "',Email='" + txtemail.Text + "' where EmployeeMasterID = " + id;
            SqlCommand cmd = new SqlCommand(strempupdate, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();

            if (ViewState["NewPartyID"] != "")
            {
                string strpartyupdate = "update Party_master set Address='" + txtaddress.Text + "',City='" + ddlcity.SelectedValue + "',State='" + ddlstates.SelectedValue + "',Country='" + ddlcountry.SelectedValue + "',Email='" + txtemail.Text + "',Phoneno='" + txtphone.Text + "',Fax='" + txtfax.Text + "',Zipcode='" + txtzipcode.Text + "' where PartyID='" + ViewState["NewPartyID"] + "'";
                SqlCommand cmd1 = new SqlCommand(strpartyupdate, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd1.ExecuteNonQuery();
                con.Close();
            }
            if (ViewState["NewUserID"] != "")
            {
                string struserupdate = "update User_master set Address='" + txtaddress.Text + "',City='" + ddlcity.SelectedValue + "',State='" + ddlstates.SelectedValue + "',Country='" + ddlcountry.SelectedValue + "',Phoneno='" + txtphone.Text + "',EmailID='" + txtemail.Text + "',zipcode='" + txtzipcode.Text + "' where UserID='" + ViewState["NewUserID"] + "'";
                SqlCommand cmd2 = new SqlCommand(struserupdate, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd2.ExecuteNonQuery();
                con.Close();
            }

            lblmsg.Visible = true;
            lblmsg.Text = "Record updated successfully";
            fillgrid123();
            fillemployee123();
            lblcontrol(true);
            txtcontrol(false);
            Panel2.Visible = true;
            Panel3.Visible = false;
            btnupdate.Visible = false;
            btncancel.Visible = false;
            btnchange.Visible = true;
        }
        else
        {
            string strpartyaddupdate = "update PartyAddressTbl set AddressActiveStatus='0' where UserId='" + ViewState["userid"] + "'";
            SqlCommand cmdaddupdate = new SqlCommand(strpartyaddupdate, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdaddupdate.ExecuteNonQuery();
            con.Close();
            string strpartyadd = "insert into PartyAddressTbl(PartyMasterId,Address,Country,State,City,email,Phone,fax,UserId,datetime,AddressActiveStatus,zipcode,verified) values ('" + ViewState["partyid"] + "','" + txtaddress.Text + "','" + ddlcountry.SelectedValue + "','" + ddlstates.SelectedValue + "','" + ddlcity.SelectedValue + "','" + txtemail.Text + "','" + txtphone.Text + "','" + txtfax.Text + "','" + ViewState["userid"] + "','" + System.DateTime.Now.ToString() + "','1','" + txtzipcode.Text + "','0')";
            SqlCommand cmdpartyadd = new SqlCommand(strpartyadd, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdpartyadd.ExecuteNonQuery();
            con.Close();
            string strempupdate = "update EmployeeMaster set Address='" + txtaddress.Text + "',CountryId='" + ddlcountry.SelectedValue + "',StateId='" + ddlstates.SelectedValue + "',City='" + ddlcity.SelectedValue + "',ContactNo='" + txtphone.Text + "',Email='" + txtemail.Text + "' where EmployeeMasterID = '" + ViewState["empid"] + "'";
            SqlCommand cmd = new SqlCommand(strempupdate, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();
            if (ViewState["partyid"] != "")
            {
                string strpartyupdate = "update Party_master set Address='" + txtaddress.Text + "',City='" + ddlcity.SelectedValue + "',State='" + ddlstates.SelectedValue + "',Country='" + ddlcountry.SelectedValue + "',Email='" + txtemail.Text + "',Phoneno='" + txtphone.Text + "',Fax='" + txtfax.Text + "',Zipcode='" + txtzipcode.Text + "' where PartyID='" + ViewState["partyid"] + "'";
                SqlCommand cmd1 = new SqlCommand(strpartyupdate, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd1.ExecuteNonQuery();
                con.Close();
            }
            if (ViewState["userid"] != "")
            {
                string struserupdate = "update User_master set Address='" + txtaddress.Text + "',City='" + ddlcity.SelectedValue + "',State='" + ddlstates.SelectedValue + "',Country='" + ddlcountry.SelectedValue + "',Phoneno='" + txtphone.Text + "',EmailID='" + txtemail.Text + "',zipcode='" + txtzipcode.Text + "' where UserID='" + ViewState["userid"] + "'";
                SqlCommand cmd2 = new SqlCommand(struserupdate, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd2.ExecuteNonQuery();
                con.Close();
            }

            lblmsg.Visible = true;
            lblmsg.Text = "Record updated successfully";
            fillgrid();
            fillemployee();
            lblcontrol(true);
            txtcontrol(false);
            Panel2.Visible = true;
            Panel3.Visible = false;
            btnupdate.Visible = false;
            btncancel.Visible = false;
            btnchange.Visible = true;
        }
    }
    protected void btnque1_Click(object sender, EventArgs e)
    {
        lblque1.Visible = false;
        lblans1.Visible = false;
        lblque2.Visible = false;
        lblans2.Visible = false;
        lblque3.Visible = false;
        lblans3.Visible = false;
        btnupque1.Visible = true;
        btnreset1.Visible = true;
        btnque1.Visible = false;
        ddlque1.Visible = true;
        txtans1.Visible = true;
        Label40.Visible = true;
        ddlque2.Visible = true;
        txtans2.Visible = true;
        ddlque3.Visible = true;
        txtans3.Visible = true;
    }
    protected void chkactive_chachedChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((CheckBox)sender).Parent.Parent as GridViewRow;

        int rinrow = row.RowIndex;

        ViewState["id"] = Convert.ToString(grd.DataKeys[rinrow].Value);

        ModalPopupExtender122.Show();
    }
    protected void btnupque1_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["EmployeeMasterID"] != null)
        {

            int ques1 = 0;
            ques1 = Convert.ToInt32(ddlque1.SelectedValue);
            int ques2 = 0;
            ques2 = Convert.ToInt32(ddlque2.SelectedValue);
            int ques3 = 0;
            ques3 = Convert.ToInt32(ddlque3.SelectedValue);
            if (ques1 == ques2)
            {
                Label12.Text = "This question is already selected";
            }

            else if (ques2 == ques3)
            {
                Label12.Text = "This question is already selected";
            }
            else if (ques3 == ques1)
            {
                Label12.Text = "This question is already selected";
            }
            else
            {
                Label12.Text = " ";
                //ViewState["NewUserID"]
                string strquestion = "Select SecurityQuestionMaster.*,SecurityQuestion.Id as ansid,SecurityQuestion.UserId,SecurityQuestion.Answer from SecurityQuestionMaster inner join SecurityQuestion on SecurityQuestionMaster.id = SecurityQuestion.SequrityQueId where SecurityQuestion.UserId = '" + ViewState["NewUserID"] + "'";
                DataSet dsquestion = (DataSet)fillddl(strquestion);
                string updateque = "";
                Int32 row = 0;
                if (row < dsquestion.Tables[0].Rows.Count)
                {
                    // if (dsquestion.Tables[0].Rows[0][""].ToString())
                    if (txtans1.Text != "")
                    {
                        updateque = "update SecurityQuestion set SequrityQueId='" + ddlque1.SelectedValue + "',Answer='" + txtans1.Text + "' where UserId='" + ViewState["NewUserID"] + "' and Id='" + dsquestion.Tables[0].Rows[0]["ansid"] + "'";
                        SqlCommand cmdupdateque = new SqlCommand(updateque, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmdupdateque.ExecuteNonQuery();
                        con.Close();
                    }
                }
                else
                {
                    updateque = "insert into SecurityQuestion(SequrityQueId,UserId,Answer) values ('" + ddlque1.SelectedValue + "','" + ViewState["NewUserID"] + "','" + txtans1.Text + "')";
                    SqlCommand cmdupdateque = new SqlCommand(updateque, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdupdateque.ExecuteNonQuery();
                    con.Close();
                }

                row = row + 1;
                string updateque1 = "";
                if (row < dsquestion.Tables[0].Rows.Count)
                {
                    if (txtans2.Text != "")
                    {
                        updateque1 = "update SecurityQuestion set SequrityQueId='" + ddlque2.SelectedValue + "',Answer='" + txtans2.Text + "' where UserId='" + ViewState["NewUserID"] + "' and Id='" + dsquestion.Tables[0].Rows[1]["ansid"] + "'";
                        SqlCommand cmdupdateque1 = new SqlCommand(updateque1, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmdupdateque1.ExecuteNonQuery();
                        con.Close();
                    }
                }
                else
                {
                    if (txtans2.Text != "")
                    {
                        updateque1 = "insert into SecurityQuestion(SequrityQueId,UserId,Answer) values ('" + ddlque2.SelectedValue + "','" + ViewState["NewUserID"] + "','" + txtans2.Text + "')";
                        SqlCommand cmdupdateque1 = new SqlCommand(updateque1, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmdupdateque1.ExecuteNonQuery();
                        con.Close();
                    }
                }

                row = row + 1;
                string updateque2 = "";
                if (row < dsquestion.Tables[0].Rows.Count)
                {
                    if (txtans3.Text != "")
                    {
                        updateque2 = "update SecurityQuestion set SequrityQueId='" + ddlque3.SelectedValue + "',Answer='" + txtans3.Text + "' where UserId='" + ViewState["NewUserID"] + "'  and Id='" + dsquestion.Tables[0].Rows[2]["ansid"] + "'";
                        SqlCommand cmdupdateque2 = new SqlCommand(updateque2, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmdupdateque2.ExecuteNonQuery();
                        con.Close();
                    }
                }
                else
                {
                    if (txtans3.Text != "")
                    {
                        updateque2 = "insert into SecurityQuestion(SequrityQueId,UserId,Answer) values ('" + ddlque3.SelectedValue + "','" + ViewState["NewUserID"] + "','" + txtans3.Text + "')";
                        SqlCommand cmdupdateque2 = new SqlCommand(updateque2, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmdupdateque2.ExecuteNonQuery();
                        con.Close();
                    }
                }


                btnupque1.Visible = false;
                btnreset1.Visible = false;
                btnque1.Visible = true;
                fillemployee123();
                lblque1.Visible = true;
                lblans1.Visible = true;
                lblque2.Visible = true;
                lblans2.Visible = true;
                lblque3.Visible = true;
                lblans3.Visible = true;
                ddlque1.Visible = false;
                txtans1.Visible = false;
                Label40.Visible = false;
                ddlque2.Visible = false;
                txtans2.Visible = false;
                ddlque3.Visible = false;
                txtans3.Visible = false;
            }
        }
        else
        {


            int ques1 = 0;
            ques1 = Convert.ToInt32(ddlque1.SelectedValue);
            int ques2 = 0;
            ques2 = Convert.ToInt32(ddlque2.SelectedValue);
            int ques3 = 0;
            ques3 = Convert.ToInt32(ddlque3.SelectedValue);
            if (ques1 == ques2)
            {
                Label12.Text = "This question is already selected";
            }

            else if (ques2 == ques3)
            {
                Label12.Text = "This question is already selected";
            }
            else if (ques3 == ques1)
            {
                Label12.Text = "This question is already selected";
            }
            else
            {
                Label12.Text = " ";

                string strquestion = "Select SecurityQuestionMaster.*,SecurityQuestion.Id as ansid,SecurityQuestion.UserId,SecurityQuestion.Answer from SecurityQuestionMaster inner join SecurityQuestion on SecurityQuestionMaster.id = SecurityQuestion.SequrityQueId where SecurityQuestion.UserId = '" + Session["userid"] + "'";
                DataSet dsquestion = (DataSet)fillddl(strquestion);
                string updateque = "";
                Int32 row = 0;
                if (row < dsquestion.Tables[0].Rows.Count)
                {
                    // if (dsquestion.Tables[0].Rows[0][""].ToString())
                    if (txtans1.Text != "")
                    {
                        updateque = "update SecurityQuestion set SequrityQueId='" + ddlque1.SelectedValue + "',Answer='" + txtans1.Text + "' where UserId='" + Session["userid"] + "' and Id='" + dsquestion.Tables[0].Rows[0]["ansid"] + "'";
                        SqlCommand cmdupdateque = new SqlCommand(updateque, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmdupdateque.ExecuteNonQuery();
                        con.Close();
                    }
                }
                else
                {
                    updateque = "insert into SecurityQuestion(SequrityQueId,UserId,Answer) values ('" + ddlque1.SelectedValue + "','" + Session["userid"] + "','" + txtans1.Text + "')";
                    SqlCommand cmdupdateque = new SqlCommand(updateque, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdupdateque.ExecuteNonQuery();
                    con.Close();
                }

                row = row + 1;
                string updateque1 = "";
                if (row < dsquestion.Tables[0].Rows.Count)
                {
                    if (txtans2.Text != "")
                    {
                        updateque1 = "update SecurityQuestion set SequrityQueId='" + ddlque2.SelectedValue + "',Answer='" + txtans2.Text + "' where UserId='" + Session["userid"] + "' and Id='" + dsquestion.Tables[0].Rows[1]["ansid"] + "'";
                        SqlCommand cmdupdateque1 = new SqlCommand(updateque1, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmdupdateque1.ExecuteNonQuery();
                        con.Close();
                    }
                }
                else
                {
                    if (txtans2.Text != "")
                    {
                        updateque1 = "insert into SecurityQuestion(SequrityQueId,UserId,Answer) values ('" + ddlque2.SelectedValue + "','" + Session["userid"] + "','" + txtans2.Text + "')";
                        SqlCommand cmdupdateque1 = new SqlCommand(updateque1, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmdupdateque1.ExecuteNonQuery();
                        con.Close();
                    }
                }

                row = row + 1;
                string updateque2 = "";
                if (row < dsquestion.Tables[0].Rows.Count)
                {
                    if (txtans3.Text != "")
                    {
                        updateque2 = "update SecurityQuestion set SequrityQueId='" + ddlque3.SelectedValue + "',Answer='" + txtans3.Text + "' where UserId='" + Session["userid"] + "'  and Id='" + dsquestion.Tables[0].Rows[2]["ansid"] + "'";
                        SqlCommand cmdupdateque2 = new SqlCommand(updateque2, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmdupdateque2.ExecuteNonQuery();
                        con.Close();
                    }
                }
                else
                {
                    if (txtans3.Text != "")
                    {
                        updateque2 = "insert into SecurityQuestion(SequrityQueId,UserId,Answer) values ('" + ddlque3.SelectedValue + "','" + Session["userid"] + "','" + txtans3.Text + "')";
                        SqlCommand cmdupdateque2 = new SqlCommand(updateque2, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmdupdateque2.ExecuteNonQuery();
                        con.Close();
                    }
                }
                btnupque1.Visible = false;
                btnreset1.Visible = false;
                btnque1.Visible = true;
                fillemployee();
                lblque1.Visible = true;
                lblans1.Visible = true;
                lblque2.Visible = true;
                lblans2.Visible = true;
                lblque3.Visible = true;
                lblans3.Visible = true;
                ddlque1.Visible = false;
                txtans1.Visible = false;
                Label40.Visible = false;
                ddlque2.Visible = false;
                txtans2.Visible = false;
                ddlque3.Visible = false;
                txtans3.Visible = false;
            }
        }
    }
    protected void btnapd_Click(object sender, EventArgs e)
    {

        if (Request.QueryString["EmployeeMasterID"] != null)
        {
            string str = "select Id,AddressActiveStatus from  PartyAddressTbl where UserId='" + ViewState["NewUserID"] + "'";
            SqlCommand cmdup = new SqlCommand(str, con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmdup);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            string strupdate = "";
            int id = Convert.ToInt32(ViewState["id"].ToString());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["Id"].ToString() == ViewState["id"].ToString())
                {
                    strupdate = "update PartyAddressTbl set AddressActiveStatus='1' where Id='" + ViewState["id"] + "'";
                }
                else
                {
                    strupdate = "update PartyAddressTbl set AddressActiveStatus='0'  where UserId='" + ViewState["NewUserID"] + "' and Id!='" + ViewState["id"] + "'";
                }
                SqlCommand cmd = new SqlCommand(strupdate, con);

                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                con.Close();
            }
            string strup = "select * from  PartyAddressTbl where AddressActiveStatus='1' and  UserId='" + ViewState["NewUserID"] + "'";
            SqlCommand cmdpat = new SqlCommand(strup, con);
            SqlDataAdapter adparty = new SqlDataAdapter(cmdpat);
            DataTable dtparty = new DataTable();
            adparty.Fill(dtparty);
            if (dtparty.Rows.Count > 0)
            {
                string strempupdate = "update EmployeeMaster set Address='" + dtparty.Rows[0]["Address"].ToString() + "',CountryId='" + dtparty.Rows[0]["Country"] + "',StateId='" + dtparty.Rows[0]["State"] + "',City='" + dtparty.Rows[0]["City"] + "',ContactNo='" + dtparty.Rows[0]["Phone"] + "',Email='" + dtparty.Rows[0]["email"] + "' where PartyID = '" + dtparty.Rows[0]["PartyMasterId"] + "'";
                SqlCommand cmd = new SqlCommand(strempupdate, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                con.Close();

                string strpartyupdate = "update Party_master set Address='" + dtparty.Rows[0]["Address"].ToString() + "',City='" + dtparty.Rows[0]["City"] + "',State='" + dtparty.Rows[0]["State"] + "',Country='" + dtparty.Rows[0]["Country"] + "',Email='" + dtparty.Rows[0]["email"] + "',Phoneno='" + dtparty.Rows[0]["Phone"] + "',Fax='" + dtparty.Rows[0]["fax"] + "',Zipcode='" + dtparty.Rows[0]["zipcode"] + "' where PartyID='" + dtparty.Rows[0]["PartyMasterId"] + "'";
                SqlCommand cmd1 = new SqlCommand(strpartyupdate, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd1.ExecuteNonQuery();
                con.Close();

                string struserupdate = "update User_master set Address='" + dtparty.Rows[0]["Address"].ToString() + "',City='" + dtparty.Rows[0]["City"] + "',State='" + dtparty.Rows[0]["State"] + "',Country='" + dtparty.Rows[0]["Country"] + "',Phoneno='" + dtparty.Rows[0]["Phone"] + "',EmailID='" + dtparty.Rows[0]["email"] + "',zipcode='" + dtparty.Rows[0]["zipcode"] + "' where UserID='" + dtparty.Rows[0]["UserId"] + "'";
                SqlCommand cmd2 = new SqlCommand(struserupdate, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd2.ExecuteNonQuery();
                con.Close();
            }
            fillgrid123();
        }
        else
        {

            string str = "select Id,AddressActiveStatus from  PartyAddressTbl where UserId='" + Session["userid"] + "'";
            SqlCommand cmdup = new SqlCommand(str, con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmdup);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            string strupdate = "";
            int id = Convert.ToInt32(ViewState["id"].ToString());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["Id"].ToString() == ViewState["id"].ToString())
                {
                    strupdate = "update PartyAddressTbl set AddressActiveStatus='1' where Id='" + ViewState["id"] + "'";
                }
                else
                {
                    strupdate = "update PartyAddressTbl set AddressActiveStatus='0'  where UserId='" + Session["userid"] + "' and Id!='" + ViewState["id"] + "'";
                }
                SqlCommand cmd = new SqlCommand(strupdate, con);

                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                con.Close();
            }
            string strup = "select * from  PartyAddressTbl where AddressActiveStatus='1' and  UserId='" + Session["userid"] + "'";
            SqlCommand cmdpat = new SqlCommand(strup, con);
            SqlDataAdapter adparty = new SqlDataAdapter(cmdpat);
            DataTable dtparty = new DataTable();
            adparty.Fill(dtparty);
            if (dtparty.Rows.Count > 0)
            {
                string strempupdate = "update EmployeeMaster set Address='" + dtparty.Rows[0]["Address"].ToString() + "',CountryId='" + dtparty.Rows[0]["Country"] + "',StateId='" + dtparty.Rows[0]["State"] + "',City='" + dtparty.Rows[0]["City"] + "',ContactNo='" + dtparty.Rows[0]["Phone"] + "',Email='" + dtparty.Rows[0]["email"] + "' where PartyID = '" + dtparty.Rows[0]["PartyMasterId"] + "'";
                SqlCommand cmd = new SqlCommand(strempupdate, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                con.Close();

                string strpartyupdate = "update Party_master set Address='" + dtparty.Rows[0]["Address"].ToString() + "',City='" + dtparty.Rows[0]["City"] + "',State='" + dtparty.Rows[0]["State"] + "',Country='" + dtparty.Rows[0]["Country"] + "',Email='" + dtparty.Rows[0]["email"] + "',Phoneno='" + dtparty.Rows[0]["Phone"] + "',Fax='" + dtparty.Rows[0]["fax"] + "',Zipcode='" + dtparty.Rows[0]["zipcode"] + "' where PartyID='" + dtparty.Rows[0]["PartyMasterId"] + "'";
                SqlCommand cmd1 = new SqlCommand(strpartyupdate, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd1.ExecuteNonQuery();
                con.Close();

                string struserupdate = "update User_master set Address='" + dtparty.Rows[0]["Address"].ToString() + "',City='" + dtparty.Rows[0]["City"] + "',State='" + dtparty.Rows[0]["State"] + "',Country='" + dtparty.Rows[0]["Country"] + "',Phoneno='" + dtparty.Rows[0]["Phone"] + "',EmailID='" + dtparty.Rows[0]["email"] + "',zipcode='" + dtparty.Rows[0]["zipcode"] + "' where UserID='" + dtparty.Rows[0]["UserId"] + "'";
                SqlCommand cmd2 = new SqlCommand(struserupdate, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd2.ExecuteNonQuery();
                con.Close();
            }
            fillgrid();
        }
    }
    protected void btns_Click(object sender, EventArgs e)
    {

    }
    protected void fillgrid()
    {
        string filldata = "select PartyAddressTbl.*,CityMasterTbl.CityName,StateMasterTbl.StateName,CountryMaster.CountryName from PartyAddressTbl inner join  CityMasterTbl on PartyAddressTbl.City = CityMasterTbl.CityId inner join StateMasterTbl on PartyAddressTbl.State = StateMasterTbl.StateId inner join CountryMaster on PartyAddressTbl.Country = CountryMaster.CountryId  where UserId='" + Session["userid"] + "' order by PartyAddressTbl.AddressActiveStatus desc";
        SqlCommand cmd = new SqlCommand(filldata, con);
        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adpt.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            DataView myDataView = new DataView();
            myDataView = dt.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }

            grd.DataSource = myDataView;
            grd.DataBind();
        }
        else
        {
            grd.DataSource = null;
            grd.DataBind();
        }
        lblcompany.Text = Session["Cname"].ToString();
        
    }

    protected void fillgrid123()
    {
        string filldata = "select PartyAddressTbl.*,CityMasterTbl.CityName,StateMasterTbl.StateName,CountryMaster.CountryName from PartyAddressTbl inner join  CityMasterTbl on PartyAddressTbl.City = CityMasterTbl.CityId inner join StateMasterTbl on PartyAddressTbl.State = StateMasterTbl.StateId inner join CountryMaster on PartyAddressTbl.Country = CountryMaster.CountryId  where UserId='" + ViewState["NewUserID"] + "' order by PartyAddressTbl.AddressActiveStatus desc";
        SqlCommand cmd = new SqlCommand(filldata, con);
        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adpt.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            DataView myDataView = new DataView();
            myDataView = dt.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }

            grd.DataSource = myDataView;
            grd.DataBind();

        }
        else
        {
            grd.DataSource = null;
            grd.DataBind();

        }
        lblcompany.Text = Session["Cname"].ToString();
    }

    protected void grid_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (Request.QueryString["EmployeeMasterID"] != null)
        {
            hdnsortExp.Value = e.SortExpression.ToString();
            hdnsortDir.Value = sortOrder; // sortOrder;
            fillgrid123();
        }
        else
        {
            hdnsortExp.Value = e.SortExpression.ToString();
            hdnsortDir.Value = sortOrder; // sortOrder;
            fillgrid();
        }
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

    protected void btnreset1_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["EmployeeMasterID"] != null)
        {
            fillempquestion123();
        }
        else
        {
            fillempquestion();    
        }

        Label12.Text = "";
        
        lblque1.Visible = true;
        lblans1.Visible = true;
        lblque2.Visible = true;
        lblans2.Visible = true;
        lblque3.Visible = true;
        lblans3.Visible = true;
        btnupque1.Visible = false;
        btnreset1.Visible = false;
        btnque1.Visible = true;

        ddlque1.Visible = false;
        txtans1.Visible = false;
        Label40.Visible = false;
        ddlque2.Visible = false;
        txtans2.Visible = false;
        ddlque3.Visible = false;
        txtans3.Visible = false;
    }
    protected void btnupdatepass_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["EmployeeMasterID"] != null)
        {


            Label1.Text = "";
            string str = "Select password from Login_master where UserID = '" + ViewState["NewUserID"] + "'";
            DataSet dsquestion = (DataSet)fillddl(str);
            string oldpass = dsquestion.Tables[0].Rows[0]["password"].ToString();
            if (panelforpassword.Visible == true)
            {
                if (ClsEncDesc.Decrypted(oldpass) == txtoldpass.Text)
                {
                    if (txtnewpass.Text == txtconfirmpass.Text)
                    {
                        string newpass = "update Login_master set password='" + PageMgmt.Encrypted(txtnewpass.Text) + "' where UserID = '" + ViewState["NewUserID"] + "'";
                        SqlCommand cmd = new SqlCommand(newpass, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmd.ExecuteNonQuery();
                        con.Close();
                        Label20.Text = "Password updated successfully";
                    }
                    else
                    {
                        Label20.Text = "Password does not match";
                    }
                }
                else
                {
                    Label20.Text = "Your old password is incorrect";
                }
            }
            SqlDataAdapter dauid = new SqlDataAdapter("Select username from Login_master where UserID = '" + ViewState["NewUserID"] + "'", con);
            DataTable dtuid = new DataTable();
            dauid.Fill(dtuid);
            string olduid = dtuid.Rows[0]["username"].ToString();
            if (panelforuserid.Visible == true)
            {
                if (olduid == TextBox1.Text)
                {
                    SqlDataAdapter daava = new SqlDataAdapter("select * from Login_master where username='" + TextBox2.Text + "'", con);
                    DataTable dtava = new DataTable();
                    daava.Fill(dtava);

                    if (dtava.Rows.Count > 0)
                    {
                        lbl1234.Text = "This Username is already used.";

                    }
                    else
                    {
                        SqlCommand cmdaca = new SqlCommand("update Login_master set username='" + TextBox2.Text + "' where UserID = '" + ViewState["NewUserID"] + "'");
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmdaca.ExecuteNonQuery();
                        con.Close();
                        lbl1234.Text = "Username updated successfully";
                    }
                }
                else
                {
                    lbl1234.Text = "Your old User ID is incorrect";
                }
            }

        }
        else
        {

            Label1.Text = "";
            string str = "Select password from Login_master where UserID = '" + Session["userid"] + "'";
            DataSet dsquestion = (DataSet)fillddl(str);
            string oldpass = dsquestion.Tables[0].Rows[0]["password"].ToString();
            if (panelforpassword.Visible == true)
            {
                if (ClsEncDesc.Decrypted(oldpass) == txtoldpass.Text)
                {
                    if (txtnewpass.Text == txtconfirmpass.Text)
                    {
                        string newpass = "update Login_master set password='" + ClsEncDesc.Encrypted(txtnewpass.Text) + "' where UserID = '" + Session["userid"] + "'";
                        SqlCommand cmd = new SqlCommand(newpass, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmd.ExecuteNonQuery();
                        con.Close();
                        
                        string ClientInsert = " update clientLoginMaster set Password='" + PageMgmt.Encrypted(txtnewpass.Text) + "'  where UserId='" + oldpass + "' ";
                        SqlCommand cmd1123 = new SqlCommand(ClientInsert, conLicense);
                        conLicense.Open();
                        cmd1123.ExecuteNonQuery();
                        conLicense.Close();
                        conLicense.Close();
                        panelforpassword.Visible = false;  
                        Label20.Text = "Password updated successfully";
                    }
                    else
                    {
                        Label20.Text = "Password does not match";
                    }
                }
                else
                {
                    Label20.Text = "Your old password is incorrect";
                }
            }
            SqlDataAdapter dauid = new SqlDataAdapter("Select username from Login_master where UserID = '" + Session["userid"] + "'", con);
            DataTable dtuid = new DataTable();
            dauid.Fill(dtuid);
            string olduid = dtuid.Rows[0]["username"].ToString();
            if (panelforuserid.Visible == true)
            {
                if (olduid == TextBox1.Text)
                {
                    SqlDataAdapter daava = new SqlDataAdapter("select * from Login_master where username='" + TextBox2.Text + "'", con);
                    DataTable dtava = new DataTable();
                    daava.Fill(dtava);

                    if (dtava.Rows.Count > 0)
                    {
                        lbl1234.Text = "This Username is already used.";

                    }
                    else
                    {
                        SqlCommand cmdaca = new SqlCommand("update Login_master set username='" + TextBox2.Text + "' where UserID = '" + Session["userid"] + "'",con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmdaca.ExecuteNonQuery();
                        con.Close();

                        string sr51 = (" update EmployeeMaster set  UserId='" + TextBox2.Text + "'  where UserId='" + olduid + "' ");
                        SqlCommand cmd801 = new SqlCommand(sr51, conLicense);
                        conLicense.Open();
                        cmd801.ExecuteNonQuery();
                        conLicense.Close();

                        string ClientInsert = " update clientLoginMaster set  UserId='" + TextBox2.Text + "'  where UserId='" + olduid + "' ";
                        SqlCommand cmd1123 = new SqlCommand(ClientInsert, conLicense);
                        conLicense.Open();
                        cmd1123.ExecuteNonQuery();
                        conLicense.Close();
                        conLicense.Close();
                        panelforuserid.Visible = false; 
                        lbl1234.Text = "Username updated successfully";



                    }
                }
                else
                {
                    lbl1234.Text = "Your old User ID is incorrect";
                }
            }
        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        lblcontrol(true);
        txtcontrol(false);
        Panel2.Visible = true;
        Panel3.Visible = false;
        btnupdate.Visible = false;
        btncancel.Visible = false;
        btnchange.Visible = true;
    }
    protected void btnresetpass_Click(object sender, EventArgs e)
    {

        btnupdatepass.Visible = false;
        btnresetpass.Visible = false;
        Label69.Visible = false;
        ddluidwd.Visible = false;
        Label63.Visible = false;
        TextBox1.Visible = false;
        Label64.Visible = false;
        Label65.Visible = false;
        TextBox2.Visible = false;

        txtoldpass.Text = "";
        txtnewpass.Text = "";
        txtconfirmpass.Text = "";
        TextBox1.Text = "";
        TextBox2.Text = "";
        Label20.Text = "";
        lbl1234.Text = "";
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");

            Button1.Text = "Hide Printable Version";
            Button7.Visible = true;

            btnchange.Visible = false;
            btnchange1.Visible = false;
            btnque1.Visible = false;
            Button2.Visible = false;
           
            //B. Contact Info.
            lblcontrol(true);
            txtcontrol(false);
            Panel2.Visible = true;
            Panel3.Visible = false;
            btnupdate.Visible = false;
            btncancel.Visible = false;
            btnchange.Visible = false;

            //C.imergency contact info            
            lblemerconnam1.Text = TextBox5.Text;
            lblemerconnam2.Text = TextBox6.Text;
            lblemerrel1.Text = TextBox7.Text;
            lblemerrel2.Text = TextBox8.Text;
            lblemerphhom1.Text = TextBox9.Text;
            lblemerphhom2.Text = TextBox10.Text;
            lblemerphcel1.Text = TextBox11.Text;
            lblemerphcel2.Text = TextBox12.Text;
            lblemerphwk1.Text = TextBox13.Text;
            lblemerphwk2.Text = TextBox14.Text;
            lblemeremail1.Text = TextBox15.Text;
            lblemeremail2.Text = TextBox16.Text;
            pnlemerconinfolbl.Visible = true;
            pnlemercontinfo.Visible = false;
            btnupdate1.Visible = false;
            btncancel1.Visible = false;  
         
            //D. Security Question
            lblque1.Visible = true;
            lblans1.Visible = true;
            lblque2.Visible = true;
            lblans2.Visible = true;
            lblque3.Visible = true;
            lblans3.Visible = true;
            btnupque1.Visible = false;
            btnreset1.Visible = false;           
            ddlque1.Visible = false;
            txtans1.Visible = false;
            Label40.Visible = false;
            ddlque2.Visible = false;
            txtans2.Visible = false;
            ddlque3.Visible = false;
            txtans3.Visible = false;

            //E.login Info
            btnupdatepass.Visible = false;
            btnresetpass.Visible = false;
            Label69.Visible = false;
            ddluidwd.Visible = false;
            Label63.Visible = false;
            TextBox1.Visible = false;
            Label64.Visible = false;
            Label65.Visible = false;
            TextBox2.Visible = false;

        }
        else
        {

            //pnlgrid.ScrollBars = ScrollBars.Vertical;
            //pnlgrid.Height = new Unit(400);

            Button1.Text = "Printable Version";
            Button7.Visible = false;

            btnchange.Visible = true;
            btnchange1.Visible = true;
            btnque1.Visible = true;
            Button2.Visible = true;            

        }
    }
    protected void ddlcountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillstate();
    }
    protected void ddlstates_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillcity();
    }

    protected void EmergencyContactInfo()
    {
        string stremer = "select * from EmployeeEmergencyContactTbl where EmployeeMasterID='" + Convert.ToInt32(Session["EmployeeId"]) + "' ";
        //string stremer = "select * from EmployeeEmergencyContactTbl where EmployeeMasterID='5317' ";
        SqlDataAdapter daemer = new SqlDataAdapter(stremer,con);
        DataTable dtemer = new DataTable();
        daemer.Fill(dtemer);
        
        if (dtemer.Rows.Count > 0)
        {

            lblemerconnam1.Text = dtemer.Rows[0]["FirstEmergencyContactName"].ToString();
            lblemerconnam2.Text = dtemer.Rows[0]["SecondEmergencyContactName"].ToString();
            lblemerrel1.Text = dtemer.Rows[0]["FirstEmergencyRelationship"].ToString();
            lblemerrel2.Text = dtemer.Rows[0]["SecondEmergencyRelationship"].ToString();
            lblemerphhom1.Text = dtemer.Rows[0]["FirstEmergencyPhoneNumberhome"].ToString();
            lblemerphhom2.Text = dtemer.Rows[0]["SecondEmergencyPhoneNumberhome"].ToString();
            lblemerphcel1.Text = dtemer.Rows[0]["FirstEmergencyPhoneNumbercell"].ToString();
            lblemerphcel2.Text = dtemer.Rows[0]["SecondEmergencyPhoneNumbercell"].ToString();
            lblemerphwk1.Text = dtemer.Rows[0]["FirstEmergencyPhoneNumberWork"].ToString();
            lblemerphwk2.Text = dtemer.Rows[0]["SecondEmergencyPhoneNumberWork"].ToString();
            lblemeremail1.Text = dtemer.Rows[0]["FirstEmergencyEmail"].ToString();
            lblemeremail2.Text = dtemer.Rows[0]["SecondEmergencyEmail"].ToString();
        }
    }
    protected void EmergencyContactInfo123()
    {
        string stremer = "select * from EmployeeEmergencyContactTbl where EmployeeMasterID='" + ViewState["Newemployeeid"] + "'";
        //string stremer = "select * from EmployeeEmergencyContactTbl where EmployeeMasterID='5317' ";
        SqlDataAdapter daemer = new SqlDataAdapter(stremer, con);
        DataTable dtemer = new DataTable();
        daemer.Fill(dtemer);

        if (dtemer.Rows.Count > 0)
        {

            lblemerconnam1.Text = dtemer.Rows[0]["FirstEmergencyContactName"].ToString();
            lblemerconnam2.Text = dtemer.Rows[0]["SecondEmergencyContactName"].ToString();
            lblemerrel1.Text = dtemer.Rows[0]["FirstEmergencyRelationship"].ToString();
            lblemerrel2.Text = dtemer.Rows[0]["SecondEmergencyRelationship"].ToString();
            lblemerphhom1.Text = dtemer.Rows[0]["FirstEmergencyPhoneNumberhome"].ToString();
            lblemerphhom2.Text = dtemer.Rows[0]["SecondEmergencyPhoneNumberhome"].ToString();
            lblemerphcel1.Text = dtemer.Rows[0]["FirstEmergencyPhoneNumbercell"].ToString();
            lblemerphcel2.Text = dtemer.Rows[0]["SecondEmergencyPhoneNumbercell"].ToString();
            lblemerphwk1.Text = dtemer.Rows[0]["FirstEmergencyPhoneNumberWork"].ToString();
            lblemerphwk2.Text = dtemer.Rows[0]["SecondEmergencyPhoneNumberWork"].ToString();
            lblemeremail1.Text = dtemer.Rows[0]["FirstEmergencyEmail"].ToString();
            lblemeremail2.Text = dtemer.Rows[0]["SecondEmergencyEmail"].ToString();
        }
    }
    protected void btnchange1_Click(object sender, EventArgs e)
    {
        //lblcontrol(false);
        //txtcontrol(true);

        TextBox5.Text = lblemerconnam1.Text;
        TextBox6.Text = lblemerconnam2.Text;
        TextBox7.Text = lblemerrel1.Text;
        TextBox8.Text = lblemerrel2.Text;
        TextBox9.Text = lblemerphhom1.Text;
        TextBox10.Text = lblemerphhom2.Text;
        TextBox11.Text = lblemerphcel1.Text;
        TextBox12.Text = lblemerphcel2.Text;
        TextBox13.Text = lblemerphwk1.Text;
        TextBox14.Text = lblemerphwk2.Text;
        TextBox15.Text = lblemeremail1.Text;
        TextBox16.Text = lblemeremail2.Text;

        pnlemerconinfolbl.Visible = false;
        pnlemercontinfo.Visible = true;
        btnupdate1.Visible = true;
        btncancel1.Visible = true;
        btnchange1.Visible = false;
    }
    protected void btncancel1_Click(object sender, EventArgs e)
    {
     //   lblcontrol(true);
     //   txtcontrol(false);
        pnlemerconinfolbl.Visible = true;
        pnlemercontinfo.Visible = false;
        btnupdate1.Visible = false;
        btncancel1.Visible = false;
        btnchange1.Visible = true;
    }
    protected void btnupdate1_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["EmployeeMasterID"] != null)
        {
            int id = Convert.ToInt32(Request.QueryString["EmployeeMasterID"]);

            SqlCommand cmdemerupd = new SqlCommand("update EmployeeEmergencyContactTbl set FirstEmergencyContactName='" + TextBox5.Text + "',FirstEmergencyRelationship='" + TextBox7.Text + "',FirstEmergencyPhoneNumberhome='" + TextBox9.Text + "',FirstEmergencyPhoneNumbercell='" + TextBox11.Text + "',FirstEmergencyPhoneNumberWork='" + TextBox13.Text + "',FirstEmergencyEmail='" + TextBox15.Text + "',SecondEmergencyContactName='" + TextBox6.Text + "',SecondEmergencyRelationship='" + TextBox8.Text + "',SecondEmergencyPhoneNumberhome='" + TextBox10.Text + "',SecondEmergencyPhoneNumbercell='" + TextBox12.Text + "',SecondEmergencyPhoneNumberWork='" + TextBox14.Text + "',SecondEmergencyEmail='" + TextBox16.Text + "' where EmployeeMasterID='" + id + "'", con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdemerupd.ExecuteNonQuery();
            con.Close();

            lblmsg.Visible = true;
            lblmsg.Text = "Record updated successfully";

            EmergencyContactInfo123();
            pnlemerconinfolbl.Visible = true;
            pnlemercontinfo.Visible = false;
            btnupdate1.Visible = false;
            btncancel1.Visible = false;
            btnchange1.Visible = true;
        }
        else
        {

            SqlCommand cmdemerupd = new SqlCommand("update EmployeeEmergencyContactTbl set FirstEmergencyContactName='" + TextBox5.Text + "',FirstEmergencyRelationship='" + TextBox7.Text + "',FirstEmergencyPhoneNumberhome='" + TextBox9.Text + "',FirstEmergencyPhoneNumbercell='" + TextBox11.Text + "',FirstEmergencyPhoneNumberWork='" + TextBox13.Text + "',FirstEmergencyEmail='" + TextBox15.Text + "',SecondEmergencyContactName='" + TextBox6.Text + "',SecondEmergencyRelationship='" + TextBox8.Text + "',SecondEmergencyPhoneNumberhome='" + TextBox10.Text + "',SecondEmergencyPhoneNumbercell='" + TextBox12.Text + "',SecondEmergencyPhoneNumberWork='" + TextBox14.Text + "',SecondEmergencyEmail='" + TextBox16.Text + "' where EmployeeMasterID='" + Convert.ToInt32(Session["EmployeeId"]) + "'", con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdemerupd.ExecuteNonQuery();
            con.Close();

            lblmsg.Visible = true;
            lblmsg.Text = "Record updated successfully";

            EmergencyContactInfo();
            pnlemerconinfolbl.Visible = true;
            pnlemercontinfo.Visible = false;
            btnupdate1.Visible = false;
            btncancel1.Visible = false;
            btnchange1.Visible = true;
        }
    }
    protected void fillempflm()
    {
        SqlDataAdapter da = new SqlDataAdapter("select LastName,FirstName,Intials,EmployeeNo,DateOfBirth,SocialSecurityNo from EmployeePayrollMaster where EmpId='" + Convert.ToInt32(Session["EmployeeId"]) + "'", con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        SqlDataAdapter da1 = new SqlDataAdapter("select Employeecode from EmployeeBarcodeMaster where Employee_Id='" + Convert.ToInt32(Session["EmployeeId"]) + "'", con);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);

        SqlDataAdapter dddf = new SqlDataAdapter("select Photo from user_master where userid='" + Session["userid"] + "'",con);
        DataTable dtdf = new DataTable();
        dddf.Fill(dtdf);

        SqlDataAdapter da11 = new SqlDataAdapter("select Sex from EmployeeMaster where EmployeeMasterID='" + Convert.ToInt32(Session["EmployeeId"]) + "'", con);
        DataTable dt11 = new DataTable();
        da11.Fill(dt11);

        if (dtdf.Rows.Count > 0)
        {
            string logoname = dtdf.Rows[0]["Photo"].ToString();

            imgLogo.ImageUrl = "~/ShoppingCart/images/" + logoname;
        }

        if (dt.Rows.Count > 0)
        {

            lbllastnam.Text = dt.Rows[0]["LastName"].ToString();
            lblfirstnam.Text = dt.Rows[0]["FirstName"].ToString();
            lblmiddl.Text = dt.Rows[0]["Intials"].ToString();
            string dates = dt.Rows[0]["DateOfBirth"].ToString();
            lblDOB.Text = Convert.ToDateTime(dates).ToString("dd/MM/yyyy");
            lblsocsecno.Text = dt.Rows[0]["SocialSecurityNo"].ToString();
            lblempno.Text = dt.Rows[0]["EmployeeNo"].ToString();

            if (Convert.ToInt32(dt11.Rows[0]["Sex"]) == 1)
            {
                lblsex.Text = "Female";
            }
            else
            {
                lblsex.Text = "Male";
            }
            
        }
        if (dt1.Rows.Count > 0)
        {
            lnlseccode.Text = dt1.Rows[0]["Employeecode"].ToString();
        }
    }

    protected void fillempflm123()
    {
        SqlDataAdapter da3 = new SqlDataAdapter("select LastName,FirstName,Intials,EmployeeNo,DateOfBirth,SocialSecurityNo from EmployeePayrollMaster where EmpId='" + ViewState["Newemployeeid"] + "'", con);
        DataTable dt3 = new DataTable();
        da3.Fill(dt3);

        SqlDataAdapter da1 = new SqlDataAdapter("select Employeecode from EmployeeBarcodeMaster where Employee_Id='" + ViewState["Newemployeeid"] + "'", con);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);

        SqlDataAdapter dddf = new SqlDataAdapter("select Photo from user_master inner join employeemaster on employeemaster.partyid=user_master.partyid where employeemasterid='" + ViewState["Newemployeeid"] + "'", con);
        DataTable dtdf = new DataTable();
        dddf.Fill(dtdf);

        SqlDataAdapter da11 = new SqlDataAdapter("select Sex from EmployeeMaster where EmployeeMasterID='" + ViewState["Newemployeeid"] + "'", con);
        DataTable dt11 = new DataTable();
        da11.Fill(dt11);

        if (dtdf.Rows.Count > 0)
        {
            string logoname = dtdf.Rows[0]["Photo"].ToString();

            imgLogo.ImageUrl = "~/ShoppingCart/images/" + logoname;
        }

        if (dt3.Rows.Count > 0)
        {

            lbllastnam.Text = dt3.Rows[0]["LastName"].ToString();
            lblfirstnam.Text = dt3.Rows[0]["FirstName"].ToString();
            lblmiddl.Text = dt3.Rows[0]["Intials"].ToString();
            string dates = dt3.Rows[0]["DateOfBirth"].ToString();
            lblDOB.Text = Convert.ToDateTime(dates).ToString("dd/MM/yyyy");
            lblsocsecno.Text = dt3.Rows[0]["SocialSecurityNo"].ToString();
            lblempno.Text = dt3.Rows[0]["EmployeeNo"].ToString();

            if (Convert.ToInt32(dt11.Rows[0]["Sex"]) == 1)
            {
                lblsex.Text = "Female";
            }
            else
            {
                lblsex.Text = "Male";
            }
        }
        if (dt1.Rows.Count > 0)
        {
            lnlseccode.Text = dt1.Rows[0]["Employeecode"].ToString();
        }
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox1.Checked == true)
        {
            pnlsecurityque.Visible = true;
        }
        if (CheckBox1.Checked == false)
        {
            pnlsecurityque.Visible = false;
        }
    }
    //protected void btnuidpwd_Click(object sender, EventArgs e)
    //{

    //    if (ddluidwd.SelectedValue == "1")
    //    {
    //        panelforuserid.Visible = true;
    //        panelforpassword.Visible = false;
    //        lbl1234.Text = "";
    //        Label20.Text = "";
    //    }
    //    if (ddluidwd.SelectedValue == "2")
    //    {
    //        panelforuserid.Visible = false;
    //        panelforpassword.Visible = true;
    //        lbl1234.Text = "";
    //        Label20.Text = "";
    //    }
    //    if (ddluidwd.SelectedValue == "3")
    //    {
    //        panelforuserid.Visible = true;
    //        panelforpassword.Visible = true;
    //        lbl1234.Text = "";
    //        Label20.Text = "";
    //    }
    //}
    protected void ddluidwd_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddluidwd.SelectedValue == "1")
        {
            panelforuserid.Visible = true;
            panelforpassword.Visible = false;
            lbl1234.Text = "";
            Label20.Text = "";
        }
        if (ddluidwd.SelectedValue == "2")
        {
            panelforuserid.Visible = false;
            panelforpassword.Visible = true;
            lbl1234.Text = "";
            Label20.Text = "";
        }
        if (ddluidwd.SelectedValue == "3")
        {
            panelforuserid.Visible = true;
            panelforpassword.Visible = true;
            lbl1234.Text = "";
            Label20.Text = "";
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        btnupdatepass.Visible = true;
        btnresetpass.Visible = true;
        Label69.Visible = true;
        ddluidwd.Visible = true;
        Label63.Visible = true;
        TextBox1.Visible = true;
        Label64.Visible = true;
        Label65.Visible = true;
        TextBox2.Visible = true;
    }
}
