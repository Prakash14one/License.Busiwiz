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
using System.IO;
using System.Text;
using System.Data.Common;
using System.ServiceProcess;
using System.Diagnostics;
using System.Windows;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text;
using System.Text;
using System.Net;
using System.Net.Mail;


public partial class ShoppingCart_Admin_Master_Default : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter adp;
    DataSet ds;
    string strconn;
    string lblempno = "";
    int groupid = 0;
    string accid = "";
    int classid = 0;

    object paramMissing = Type.Missing;
    public string errormessage;
    private bool wordavailable = false;
    private bool checkedword = false;
    public static string abcdy = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;

        strconn = pgcon.dynconn.ConnectionString;

        if (!IsPostBack)
        {            
            Label50.Text = DateTime.Now.ToShortDateString();
            Label51.Text = DateTime.Now.ToShortDateString();

            Label2.Text = DateTime.Now.ToShortTimeString().ToString();
            lblsystemtime.Text = DateTime.Now.ToShortTimeString().ToString();
            lblsystemtime.Text = DateTime.Now.ToShortTimeString().ToString();

            panel11.Visible = true;
            panel12.Visible = true;
            panel13.Visible = true;
            panel14.Visible = true;

            userid.Visible = true;

            efname.Visible = false;
            elname.Visible = false;
            ephoneno.Visible = false;
            eemailid.Visible = false;

            Label1.Visible = true;

            Panel4.Visible = true;
            Panel1.Visible = true;
            Panel2.Visible = true;
            Panel3.Visible = true;
            Panel1.Visible = true;
            Panel6.Visible = false;

            lblempno = number(4).ToString();
            userid.Text = lblempno.Substring(0, 4);

            btnEdit.Visible = false;
            btnUpdate.Visible = false;

            RadioButtonList2_SelectedIndexChanged(sender, e);

            drop_request_SelectedIndexChanged(sender, e);
            fillstorewithfilter();

            SqlDataAdapter daza = new SqlDataAdapter("select city,state,country from [CompanyWebsiteAddressMaster] where [CompanyWebsiteMasterId]='" + dropbusiness.SelectedValue + "'", con);
            DataTable dtza = new DataTable();
            daza.Fill(dtza);

            if (dtza.Rows.Count > 0)
            {
                ViewState["Country"] = dtza.Rows[0]["country"].ToString();
                ViewState["State"] = dtza.Rows[0]["state"].ToString();
                ViewState["City"] = dtza.Rows[0]["city"].ToString();
            }

            fillcarrier();

            dropbusiness_SelectedIndexChanged(sender, e);
            dropemployee.Items.Insert(0, "-Select-");
            dropemployee.Items[0].Value = "0";

            fillparties();
        }

    }
    protected void dropvisitorname_SelectedIndexChanged(object sender, EventArgs e)
    {
        Panel2.Visible = true;
        btnUpdate.Visible = false;
    }

    protected void fillemployee()
    {
        dropemployee.Items.Clear();

        string str = "Select * from EmployeeMaster  Where DesignationMasterId = '" + dropdepartment.SelectedValue + "' order by EmployeeName ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();

        adp.Fill(ds);
        dropemployee.DataSource = ds;
        dropemployee.DataTextField = "EmployeeName";
        dropemployee.DataValueField = "EmployeeMasterID";
        dropemployee.DataBind();

        dropemployee.Items.Insert(0, "-Select-");
        dropemployee.Items[0].Value = "0";
        con.Close();
    }
    protected void dropdepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillemployee();
    }

    protected void filldeprdesg1()
    {
        dropdepartment.Items.Clear();

        string str1 = "select Employeemaster.EmployeeMasterID,Employeemaster.EmployeeName + ':' + DesignationMaster.DesignationName + ':' + DepartmentmasterMNC.Departmentname as name FROM EmployeeMaster inner join DesignationMaster on DesignationMaster.DesignationMasterId=EmployeeMaster.DesignationMasterId inner join DepartmentmasterMNC on DesignationMaster.DeptID = DepartmentmasterMNC.id where Companyid='" + Session["Comid"].ToString() + "' and Employeemaster.Whid='" + dropbusiness.SelectedValue + "' ORDER BY name";
        DataTable ds1 = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(str1, con);
        da.Fill(ds1);
        if (ds1.Rows.Count > 0)
        {
            dropdepartment.DataSource = ds1;
            dropdepartment.DataTextField = "name";
            dropdepartment.DataValueField = "EmployeeMasterID";
            dropdepartment.DataBind();
        }
        dropdepartment.Items.Insert(0, "-Select-");
        dropdepartment.SelectedItem.Value = "0";
        con.Close();
    }
    protected void dropbusiness_SelectedIndexChanged(object sender, EventArgs e)
    {
        filldeprdesg1();

    }
    protected void fillstorewithfilter()
    {
        dropbusiness.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ViewState["cdf"] = "1";
        dropbusiness.DataSource = ds;
        dropbusiness.DataTextField = "Name";
        dropbusiness.DataValueField = "WareHouseId";
        dropbusiness.DataBind();
        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            dropbusiness.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }

    }
    public static Int64 number(int s)
    {
        Random rn = new Random((int)DateTime.Now.Ticks);
        StringBuilder bl = new StringBuilder();
        string ass;
        for (int i = 0; i < s; i++)
        {
            ass = Convert.ToString(Convert.ToInt32(Math.Floor(26 * rn.NextDouble() + 65)));
            bl.Append(ass);
        }
        return Convert.ToInt64(bl.ToString());
    }

    protected void drop_request_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblsystemtime.Text = DateTime.Now.ToShortTimeString().ToString();
        Label2.Text = DateTime.Now.ToShortTimeString().ToString();

        if (drop_request.SelectedValue == "1")
        {
            Panel9.Visible = true;
            Panel10.Visible = true;
            RadioButtonList2_SelectedIndexChanged(sender, e);

            RadioButtonList1.SelectedValue = "1";

            txtfname.Text = "";
            txtlastname.Text = "";
            txtphone.Text = "";
            txtemailid.Text = "";
            txtpurpose.Text = "";

            panel11.Visible = true;
            panel12.Visible = true;
            panel13.Visible = true;
            panel14.Visible = true;

            statuslable.Text = "";
            Panel4.Visible = true;
            Panel1.Visible = true;
            Panel2.Visible = true;
            Panel3.Visible = true;
            Panel1.Visible = true;
            Panel6.Visible = false;
            Panel5.Visible = false;

            userid.Visible = true;
            lblempno = number(4).ToString();
            userid.Text = lblempno.Substring(0, 4);

            btnEdit.Visible = false;
            btnUpdate.Visible = false;
            //lbl1.Visible = true;
            //lbl2.Visible = true;
            //lbl3.Visible = true;
            elname.Visible = false;
            efname.Visible = false;
            eemailid.Visible = false;
            ephoneno.Visible = false;
            btnsubmit.Visible = true;


        }
        if (drop_request.SelectedValue == "2")
        {
            statuslable.Text = "";
            Panel5.Visible = true;
            Panel4.Visible = false;
            Panel10.Visible = false;
            Panel1.Visible = false;
            Panel2.Visible = false;
            Panel3.Visible = false;
            Panel4.Visible = false;
            Panel7.Visible = false;
            Panel6.Visible = false;
            btnsubmit.Visible = false;

            SqlDataAdapter da = new SqlDataAdapter("Select FirstName + ' : ' + LastName as name,ID From VisitorProfile where ExitTime IS Null order by name", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                dropvisitor.DataSource = dt;
                dropvisitor.DataTextField = "name";
                dropvisitor.DataValueField = "ID";
                dropvisitor.DataBind();
            }
            dropvisitor.Items.Insert(0, "-Select-");
            dropvisitor.SelectedItem.Value = "0";
        }
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedItem.ToString() == "New Visitor")
        {
            Panel9.Visible = true;
            RadioButtonList2_SelectedIndexChanged(sender, e);

        }
        else if (RadioButtonList1.SelectedItem.ToString() == "Existing Visitor")
        {
            string str1 = "select distinct Party_master.Compname + ' : ' + VisitorProfile.FirstName + ' ' +  VisitorProfile.LastName as Name,VisitorProfile.ID from Party_master inner join VisitorProfile on VisitorProfile.PartyID=Party_master.PartyID order by Name asc";

            DataTable ds1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(str1, con);
            da.Fill(ds1);

            if (ds1.Rows.Count > 0)
            {
                ddname.DataSource = ds1;
                ddname.DataTextField = "Name";
                ddname.DataValueField = "ID";
                ddname.DataBind();
            }
            ddname.Items.Insert(0, "-Select-");
            ddname.Items[0].Value = "0";

            Panel9.Visible = false;

            txtvisiotrID.Text = "";
            statuslable.Text = "";
            Panel1.Visible = true;
            Panel4.Visible = true;
            Panel6.Visible = true;
            Panel3.Visible = false;
            Panel2.Visible = false;

            btnEdit.Visible = true;
            btnUpdate.Visible = false;

            userid.Visible = false;

            efname.Visible = true;
            elname.Visible = true;
            ephoneno.Visible = true;
            eemailid.Visible = true;

            Label2.Visible = true;
            Panel8.Visible = true;
            btnsubmit.Visible = false;
        }
    }


    protected void btnsubmit_Click1(object sender, EventArgs e)
    {
      
        if (RadioButtonList1.SelectedItem.ToString() == "New Visitor")
        {

            Label45.Visible = false;
            statuslable.Text = "";

            string str11 = "Select * From VisitorProfile Where(LastName='" + txtlastname.Text + "' and ContactNumber='" + txtphone.Text + "') OR (LastName='" + txtlastname.Text + "' and EmaiID='" + txtemailid.Text + "')";

            SqlDataAdapter ad = new SqlDataAdapter(str11, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            DataTable dt = new DataTable();
            ad.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                statuslable.Text = "Record already exist";
                txtfname.Text = "";
                txtlastname.Text = "";
                txtphone.Text = "";
                txtemailid.Text = "";
                txtpurpose.Text = "";
                dropdepartment.SelectedIndex = 0;
                dropemployee.SelectedIndex = 0;
                con.Close();
            }
            else
            {
                if (RadioButtonList2.SelectedValue == "0")
                {

                    DataTable dtdesg = select("select EmployeeMaster.DesignationMasterId from EmployeeMaster where EmployeeMasterID='" + dropdepartment.SelectedValue + "'");

                    string str2 = "Insert into VisitorProfile(FirstName,LastName,ContactNumber,EmaiID,Meetingwith_Bus,Meetingwith_Dep,Meetingwith_Emp,PurposeOfMeeting,EntryTime,ID,partyid,entry_date) Values('" + txtfname.Text + "','" + txtlastname.Text + "','" + txtphone.Text + "','" + txtemailid.Text + "','" + dropbusiness.SelectedValue + "','" + dtdesg.Rows[0]["DesignationMasterId"].ToString() + "','" + dropdepartment.SelectedValue + "','" + txtpurpose.Text + "','" + Label2.Text + "','" + userid.Text + "','" + ddparty.SelectedValue + "','" + DateTime.Now.ToShortDateString() + "')";
                    SqlCommand cmd = new SqlCommand(str2, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd.ExecuteNonQuery();

                    string str2par = "Insert into PartyMoreUserTbl(PartyID,VisitorID,FirstName,LastName,Mobile,Carrier,Phone,Ext,Email) Values('" + ddparty.SelectedValue + "','" + userid.Text + "','" + txtfname.Text + "','" + txtlastname.Text + "','" + txtphone.Text + "','" + ddlcarrier.SelectedValue + "','" + TextBox2.Text + "','" + TextBox3.Text + "','" + txtemailid.Text + "')";
                    SqlCommand cmdpar = new SqlCommand(str2par, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdpar.ExecuteNonQuery();

                    SqlDataAdapter davis = new SqlDataAdapter("select SmallmesstypeId from SmallMessageType where Company_id='" + Session["Comid"].ToString() + "' and Smallmesstype='In Visit'", con);
                    DataTable dtvis = new DataTable();
                    davis.Fill(dtvis);

                    string str = "insert into CommunicationDetail (CommTypeId,CapmanPartyTypeId,CommWith,CommFor,Date,Time,Description,Phoneno,ReminderDate,RelatedBusiness,RelatedWeekGoal,Project,Task,Flag) values ('" + dtvis.Rows[0]["SmallmesstypeId"].ToString() + "','7','" + ddparty.SelectedValue + "','" + dropdepartment.SelectedValue + "','" + System.DateTime.Now.ToShortDateString() + "','" + System.DateTime.Now.ToShortTimeString() + "','" + txtpurpose.Text + "','" + txtphone.Text + "','" + System.DateTime.Now.ToShortDateString() + "','" + dropbusiness.SelectedValue + "','0','0','0','0')";
                    SqlCommand cmd123 = new SqlCommand(str, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd123.ExecuteNonQuery();

                    statuslable.Visible = true;
                    statuslable.Text = "You have successfully entered. Your entry time is " + Label2.Text;

                    if (CheckBox1.Checked == true)
                    {
                        fillpass();
                        printpass.Visible = true;
                        paneluper.Visible = false;
                        Button2.Visible = true;
                    }

                    txtfname.Text = "";
                    txtlastname.Text = "";
                    txtphone.Text = "";
                    txtemailid.Text = "";
                    txtpurpose.Text = "";
                    TextBox1.Text = "";
                    TextBox2.Text = "";
                    TextBox3.Text = "";
                    ddlcarrier.SelectedIndex = 0;
                    ddparty.SelectedIndex = 0;
                    dropdepartment.SelectedIndex = 0;
                    dropemployee.SelectedIndex = 0;

                    dropbusiness.SelectedIndex = 0;
                    txtvisiotrID.Text = "";
                    efname.Visible = false;
                    elname.Visible = false;
                    eemailid.Visible = false;
                    ephoneno.Visible = false;
                    userid.Visible = true;
                    ddname.Items.Clear();
                    lblempno = number(4).ToString();
                    userid.Text = lblempno.Substring(0, 4);
                    con.Close();
                }
                else
                {
                    fillparty();

                    DataTable dtdesg = select("select EmployeeMaster.DesignationMasterId from EmployeeMaster where EmployeeMasterID='" + dropdepartment.SelectedValue + "'");

                    string str2 = "Insert into VisitorProfile(FirstName,LastName,ContactNumber,EmaiID,Meetingwith_Bus,Meetingwith_Dep,Meetingwith_Emp,PurposeOfMeeting,EntryTime,ID,partyid,entry_date) Values('" + txtfname.Text + "','" + txtlastname.Text + "','" + txtphone.Text + "','" + txtemailid.Text + "','" + dropbusiness.SelectedValue + "','" + dtdesg.Rows[0]["DesignationMasterId"].ToString() + "','" + dropdepartment.SelectedValue + "','" + txtpurpose.Text + "','" + Label2.Text + "','" + userid.Text + "','" + ViewState["PartyMasterId"] + "','" + DateTime.Now.ToShortDateString() + "')";
                    SqlCommand cmd = new SqlCommand(str2, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd.ExecuteNonQuery();

                    string str2par = "Insert into PartyMoreUserTbl(PartyID,VisitorID,FirstName,LastName,Mobile,Carrier,Phone,Ext,Email) Values('" + ViewState["PartyMasterId"] + "','" + userid.Text + "','" + txtfname.Text + "','" + txtlastname.Text + "','" + txtphone.Text + "','" + ddlcarrier.SelectedValue + "','" + TextBox2.Text + "','" + TextBox3.Text + "','" + txtemailid.Text + "')";
                    SqlCommand cmdpar = new SqlCommand(str2par, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdpar.ExecuteNonQuery();

                    SqlDataAdapter davis = new SqlDataAdapter("select SmallmesstypeId from SmallMessageType where Company_id='" + Session["Comid"].ToString() + "' and Smallmesstype='In Visit'", con);
                    DataTable dtvis = new DataTable();
                    davis.Fill(dtvis);

                    string str = "insert into CommunicationDetail (CommTypeId,CapmanPartyTypeId,CommWith,CommFor,Date,Time,Description,Phoneno,ReminderDate,RelatedBusiness,RelatedWeekGoal,Project,Task,Flag) values ('" + dtvis.Rows[0]["SmallmesstypeId"].ToString() + "','7','" + ViewState["PartyMasterId"] + "','" + dropdepartment.SelectedValue + "','" + System.DateTime.Now.ToShortDateString() + "','" + System.DateTime.Now.ToShortTimeString() + "','" + txtpurpose.Text + "','" + txtphone.Text + "','" + System.DateTime.Now.ToShortDateString() + "','" + dropbusiness.SelectedValue + "','0','0','0','0')";
                    SqlCommand cmd123 = new SqlCommand(str, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd123.ExecuteNonQuery();

                    statuslable.Visible = true;
                    statuslable.Text = "You have successfully entered. Your entry time is " + Label2.Text;

                    if (CheckBox1.Checked == true)
                    {
                        fillpass();
                        printpass.Visible = true;
                        paneluper.Visible = false;
                        Button2.Visible = true;
                    }

                    txtfname.Text = "";
                    txtlastname.Text = "";
                    txtphone.Text = "";
                    txtemailid.Text = "";
                    txtpurpose.Text = "";
                    TextBox1.Text = "";
                    TextBox2.Text = "";
                    TextBox3.Text = "";
                    ddlcarrier.SelectedIndex = 0;
                    ddparty.SelectedIndex = 0;
                    dropdepartment.SelectedIndex = 0;
                    dropemployee.SelectedIndex = 0;

                    dropbusiness.SelectedIndex = 0;
                    txtvisiotrID.Text = "";
                    efname.Visible = false;
                    elname.Visible = false;
                    eemailid.Visible = false;
                    ephoneno.Visible = false;
                    userid.Visible = true;
                    ddname.Items.Clear();
                    lblempno = number(4).ToString();
                    userid.Text = lblempno.Substring(0, 4);
                    con.Close();
                }

            }
        }
        else
        {

            SqlDataAdapter das = new SqlDataAdapter("select * from visitorprofile where ID = '" + userid.Text + "' and Exittime IS NULL", con);
            DataTable dts = new DataTable();
            das.Fill(dts);

            if (dts.Rows.Count > 0)
            {
                statuslable.Text = "Please Exit First, Your entry time is " + dts.Rows[0]["EntryTime"].ToString();
            }
            else
            {
                ddname.Items.Clear();
                statuslable.Text = "";

                DataTable dtsfsf = select("select * from visitorprofile where ID ='" + userid.Text + "' ");

                DataTable dtdesg = select("select EmployeeMaster.DesignationMasterId from EmployeeMaster where EmployeeMasterID='" + dropdepartment.SelectedValue + "'");

                string str3 = "Insert into VisitorProfile(FirstName,LastName,ContactNumber,EmaiID,Meetingwith_Bus,Meetingwith_Dep,Meetingwith_Emp,PurposeOfMeeting,EntryTime,ID,partyid,entry_date) Values('" + efname.Text + "','" + elname.Text + "','" + ephoneno.Text + "','" + eemailid.Text + "','" + dropbusiness.SelectedValue + "','" + dtdesg.Rows[0]["DesignationMasterId"].ToString() + "','" + dropdepartment.SelectedValue + "','" + txtpurpose.Text + "','" + Label2.Text + "','" + userid.Text + "','" + dtsfsf.Rows[0]["partyid"].ToString() + "','" + DateTime.Now.ToShortDateString() + "') ";
                SqlCommand cmd = new SqlCommand(str3, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();

                statuslable.Visible = true;
                statuslable.Text = "You have successfully entered. Your entry time is " + Label2.Text;
                con.Close();

                if (CheckBox1.Checked == true)
                {
                    fillpass();
                    printpass.Visible = true;
                    paneluper.Visible = false;
                    Button2.Visible = true;
                }

                txtfname.Text = "";
                txtlastname.Text = "";
                txtphone.Text = "";
                txtemailid.Text = "";
                txtpurpose.Text = "";
                dropdepartment.SelectedIndex = 0;
                dropemployee.SelectedIndex = 0;

                dropbusiness.SelectedIndex = 0;
                txtvisiotrID.Text = "";
                efname.Visible = false;
                elname.Visible = false;
                eemailid.Visible = false;
                ephoneno.Visible = false;
                userid.Visible = true;
                ddname.Items.Clear();
                lblempno = number(4).ToString();
                userid.Text = lblempno.Substring(0, 4);
                con.Close();
            }
        }

        Label45.Visible = false;
        Label2.Text = DateTime.Now.ToShortTimeString().ToString();

        fillparties();
    }

    protected void fillpass()
    {
        SqlDataAdapter da11 = new SqlDataAdapter("select LogoUrl from CompanyWebsitMaster where whid='" + dropbusiness.SelectedValue + "'", con);
        DataTable dt11 = new DataTable();
        da11.Fill(dt11);

        if (dt11.Rows.Count > 0)
        {
            Img1.ImageUrl = "~/ShoppingCart/images/" + dt11.Rows[0]["LogoUrl"].ToString();
        }

        SqlDataAdapter daza = new SqlDataAdapter("select warehousemaster.Name,CityMasterTbl.CityName,StateMasterTbl.StateName,CountryMaster.CountryName,CompanyWebsiteAddressMaster.Address1,CompanyWebsiteAddressMaster.Zip,CompanyWebsiteAddressMaster.Address2,CompanyWebsiteAddressMaster.Phone1,CompanyWebsiteAddressMaster.Phone2,CompanyWebsiteAddressMaster.Email from CompanyWebsiteAddressMaster inner join warehousemaster on warehousemaster.warehouseid=CompanyWebsiteAddressMaster.CompanyWebsiteMasterId inner join CityMasterTbl on CityMasterTbl.CityId=CompanyWebsiteAddressMaster.City inner join StateMasterTbl on StateMasterTbl.StateId=CompanyWebsiteAddressMaster.State inner join CountryMaster on CountryMaster.CountryId=CompanyWebsiteAddressMaster.Country where CompanyWebsiteAddressMaster.CompanyWebsiteMasterId='" + dropbusiness.SelectedValue + "'", con);
        DataTable dtza = new DataTable();
        daza.Fill(dtza);

        if (dtza.Rows.Count > 0)
        {
            lblcompanyname.Text = dtza.Rows[0]["Name"].ToString();
            lbladdress1.Text = dtza.Rows[0]["Address1"].ToString();
            lblcs.Text = dtza.Rows[0]["CityName"].ToString() + "," + dtza.Rows[0]["StateName"].ToString() + "," + dtza.Rows[0]["CountryName"].ToString() + "," + dtza.Rows[0]["Zip"].ToString();
            //  lbltollfreeno.Text = dtza.Rows[0]["Address2"].ToString();
            lblphoneno.Text = dtza.Rows[0]["Phone1"].ToString();
            lblemail.Text = dtza.Rows[0]["Email"].ToString();

        }

        lblentrytt.Text = Label2.Text;
        lblfirsttt.Text = txtfname.Text;
        lbllasttt.Text = txtlastname.Text;
        lblcontactnoo.Text = txtphone.Text;

        lblmeetingwith.Text = dropdepartment.SelectedItem.Text;

        DataTable dftg = select("select DepartmentmasterMNC.Departmentname,DesignationMaster.DesignationName from DepartmentmasterMNC inner join DesignationMaster on DesignationMaster.DeptID=DepartmentmasterMNC.id inner join EmployeeMaster on EmployeeMaster.DesignationMasterId=DesignationMaster.DesignationMasterId where EmployeeMaster.EmployeeMasterID='" + dropdepartment.SelectedValue + "'");

        lblmeetingwith.Text += " - " + dftg.Rows[0]["DesignationName"].ToString();

        lblmeetingwith.Text += " - " + dftg.Rows[0]["Departmentname"].ToString();

        Label30.Text = txtpurpose.Text;
    }

    protected void btn_exitsubmit_Click(object sender, EventArgs e)
    {
        statuslable.Text = "";

        string str3 = "update VisitorProfile SET ExitTime ='" + lblsystemtime.Text + "' where ID='" + dropvisitor.SelectedValue + "'";
        SqlCommand cmd = new SqlCommand(str3, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd.ExecuteNonQuery();
        con.Close();

        statuslable.Visible = true;
        statuslable.Text = "Thanks For Visit.";

        dropvisitor.Items.Clear();

        string stff = "Select FirstName + ' : ' + LastName as name,ID From VisitorProfile where ExitTime IS Null ";
        SqlDataAdapter da = new SqlDataAdapter(stff, con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            dropvisitor.DataSource = dt;
            dropvisitor.DataTextField = "name";
            dropvisitor.DataValueField = "ID";
            dropvisitor.DataBind();
        }

        dropvisitor.Items.Insert(0, "-Select-");
        dropvisitor.Items[0].Value = "0";

        Label45.Visible = false;
        dropvisitor.SelectedIndex = 0;

        pnlsds.Visible = false;

    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {


        string str11 = "Select * From VisitorProfile Where ((LastName='" + txtlastname.Text + "' and ContactNumber='" + txtphone.Text + "') OR (LastName='" + txtlastname.Text + "' and EmaiID='" + txtemailid.Text + "')) and ID<>'" + userid.Text + "'";
        SqlDataAdapter ad = new SqlDataAdapter(str11, con);
        DataTable dt = new DataTable();
        ad.Fill(dt);

        if (con.State.ToString() != "Open")
        {
            con.Open();
        }

        if (dt.Rows.Count > 0)
        {
            statuslable.Visible = true;
            statuslable.Text = "Record already exist.";
            con.Close();
        }
        else
        {
            con.Close();
            statuslable.Text = "";

            string str4 = "update VisitorProfile set FirstName='" + txtfname.Text + "',LastName='" + txtlastname.Text + "',ContactNumber='" + txtphone.Text + "',EmaiID='" + txtemailid.Text + "',entry_date='" + DateTime.Now.ToShortDateString() + "' where ID='" + userid.Text + "'";
            SqlCommand cmd = new SqlCommand(str4, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();

            string str4par = "update PartyMoreUserTbl set FirstName='" + txtfname.Text + "',Carrier='" + ddlcarrier.SelectedValue + "',Phone='" + TextBox2.Text + "',Ext='" + TextBox3.Text + "',LastName='" + txtlastname.Text + "',Mobile='" + txtphone.Text + "',Email='" + txtemailid.Text + "' where VisitorID='" + userid.Text + "'";
            SqlCommand cmdpar = new SqlCommand(str4par, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdpar.ExecuteNonQuery();
            con.Close();

            SqlDataAdapter dafind = new SqlDataAdapter("select partyid from VisitorProfile where ID='" + userid.Text + "'", con);
            DataTable dtfind = new DataTable();
            dafind.Fill(dtfind);

            SqlConnection conn3 = new SqlConnection(strconn);

            string ins1 = "update Party_master set Contactperson='" + txtfname.Text + ' ' + txtlastname.Text + "',Email='" + txtemailid.Text + "',Phoneno='" + txtphone.Text + "' where partyid='" + dtfind.Rows[0]["partyid"].ToString() + "' ";

            SqlCommand cmd3 = new SqlCommand(ins1);
            cmd3.Connection = conn3;
            conn3.Open();
            cmd3.ExecuteNonQuery();

            SqlConnection conn6 = new SqlConnection(strconn);

            string ins6 = "update User_master set Name='" + txtfname.Text + ' ' + txtlastname.Text + "',Phoneno='" + txtphone.Text + "',EmailID='" + txtemailid.Text + "' where partyid='" + dtfind.Rows[0]["partyid"].ToString() + "' ";
            SqlCommand cmd6 = new SqlCommand(ins6);
            cmd6.Connection = conn6;
            conn6.Open();
            cmd6.ExecuteNonQuery();

            statuslable.Text = "Record updated successfully.";

            btnEdit.Visible = true;
            btnUpdate.Visible = false;

            btnsubmit.Visible = true;

            panel11.Visible = false;
            panel12.Visible = false;
            panel13.Visible = false;
            panel14.Visible = false;
            panel17.Visible = false;
            TextBox3.Visible = false;

            pnlmobcar.Visible = false;
            Panel19.Visible = true;

            Label42.Visible = true;
            Label47.Visible = true;
            //Label49.Visible = true;

            Panel3.Visible = true;

            efname.Visible = true;
            elname.Visible = true;
            ephoneno.Visible = true;

            eemailid.Visible = true;

            efname.Text = txtfname.Text;
            elname.Text = txtlastname.Text;
            ephoneno.Text = txtphone.Text;
            eemailid.Text = txtemailid.Text;
            Label53.Text = ddlcarrier.SelectedItem.Text;

            Label47.Text = TextBox2.Text;
            Label42.Text = TextBox3.Text;

            con.Close();

        }
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }

        SqlDataAdapter ad1 = new SqlDataAdapter("Select * From VisitorProfile Where ID='" + txtvisiotrID.Text + "'", con);
        DataTable dt1 = new DataTable();
        ad1.Fill(dt1);
        if (dt1.Rows.Count > 0)
        {

            txtvisiotrID.Text = dt1.Rows[0]["ID"].ToString();
            Panel4.Visible = true;
            Panel2.Visible = true;
            userid.Visible = true;

            efname.Visible = true;
            elname.Visible = true;
            ephoneno.Visible = true;
            eemailid.Visible = true;


            efname.Text = txtfname.Text;
            elname.Text = txtlastname.Text;
            ephoneno.Text = txtphone.Text;
            eemailid.Text = txtemailid.Text;

        }


    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        panel11.Visible = false;
        panel12.Visible = false;
        panel13.Visible = false;
        panel14.Visible = false;

        txtname.Text = "";
        txtfname.Text = "";
        txtlastname.Text = "";
        txtphone.Text = "";
        txtemailid.Text = "";

        //txtfname.Visible = false;
        //txtlastname.Visible = false;
        //txtemailid.Visible = false;
        //txtphone.Visible = false;

        panel11.Visible = false;
        panel12.Visible = false;
        panel13.Visible = false;
        panel14.Visible = false;

        statuslable.Text = "";
        btnUpdate.Visible = false;

        //lbl1.Visible = false;
        //lbl2.Visible = false;
        //lbl3.Visible = false;
        //    Label8.Visible = false;
        //   Label20.Visible = false;
        //    Label17.Visible = false;
        //    Label5.Visible = false;
        //    Label9.Visible = false;
        //    Label40.Visible = false;
        //  Label41.Visible = false;

        elname.Visible = true;
        efname.Visible = true;
        ephoneno.Visible = true;
        eemailid.Visible = true;

        btnEdit.Visible = true;

        try
        {


            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("Select * From VisitorProfile Where ID='" + txtvisiotrID.Text + "'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                txtvisiotrID.Text = dt.Rows[0]["ID"].ToString();
                Panel4.Visible = true;
                Panel2.Visible = true;
                userid.Visible = true;

                efname.Visible = true;
                elname.Visible = true;
                ephoneno.Visible = true;
                eemailid.Visible = true;

                efname.Text = dt.Rows[0]["FirstName"].ToString();
                elname.Text = dt.Rows[0]["LastName"].ToString();
                ephoneno.Text = dt.Rows[0]["ContactNumber"].ToString();
                eemailid.Text = dt.Rows[0]["EmaiID"].ToString();
                userid.Text = dt.Rows[0]["ID"].ToString();
                btnsubmit.Visible = true;
                Panel3.Visible = true;
            }

            else
            {
                statuslable.Text = "Please Enter valid User ID";
                Panel2.Visible = false;
                Panel3.Visible = false;
                con.Close();

            }
        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
    }


    protected void dropvisitor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dropvisitor.SelectedIndex > 0)
        {
            pnlsds.Visible = true;

            SqlDataAdapter da = new SqlDataAdapter("Select * From VisitorProfile Where ID='" + dropvisitor.SelectedValue + "'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                Label45.Visible = true;
                Label45.Text = dt.Rows[0]["EntryTime"].ToString();
                Label52.Text = Convert.ToDateTime(dt.Rows[0]["entry_date"].ToString()).ToShortDateString();
            }
        }
        else
        {
            pnlsds.Visible = false;
        }
    }

    protected void txtname_TextChanged(object sender, EventArgs e)
    {
        ddname.Items.Clear();
        statuslable.Text = "";
    }
    protected void ddselectvisitor_SelectedIndexChanged(object sender, EventArgs e)
    {
        statuslable.Text = "";


        if (ddselectvisitor.SelectedValue == "1")
        {
            ddname.Items.Clear();
            Panel7.Visible = true;
            Panel8.Visible = false;
            Panel2.Visible = false;
            Panel3.Visible = false;
            btnsubmit.Visible = false;

        }
        else
        {
            //ddname.Items.Clear();
            Panel7.Visible = false;
            Panel8.Visible = true;
            Panel2.Visible = false;
            Panel3.Visible = false;
            btnsubmit.Visible = false;
        }
    }
    protected void btngoo_Click(object sender, EventArgs e)
    {
        panel11.Visible = false;
        panel12.Visible = false;
        panel13.Visible = false;
        panel14.Visible = false;
        panel17.Visible = false;
        TextBox3.Visible = false;

        Panel15.Visible = false;
        Panel16.Visible = false;

        txtvisiotrID.Text = "";
        txtname.Text = "";
        txtfname.Text = "";
        txtlastname.Text = "";
        txtphone.Text = "";
        txtemailid.Text = "";

        panel11.Visible = false;
        panel12.Visible = false;
        panel13.Visible = false;
        panel14.Visible = false;
        pnlmobcar.Visible = false;
        Panel19.Visible = true;

        btnUpdate.Visible = false;

        btnEdit.Visible = true;
        statuslable.Text = "";

        try
        {
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("Select * From VisitorProfile inner join PartyMoreUserTbl on VisitorProfile.ID=PartyMoreUserTbl.VisitorID  Where VisitorProfile.ID='" + ddname.SelectedValue + "'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {

                fillcarrier();

                ddlcarrier.SelectedIndex = ddlcarrier.Items.IndexOf(ddlcarrier.Items.FindByValue(dt.Rows[0]["Carrier"].ToString()));
                Label53.Text = ddlcarrier.SelectedItem.Text;

                Panel4.Visible = true;
                Panel2.Visible = true;
                userid.Visible = true;
                efname.Visible = true;
                elname.Visible = true;
                ephoneno.Visible = true;
                eemailid.Visible = true;
                userid.Visible = true;
                Label47.Visible = true;
                Label42.Visible = true;
                //Label49.Visible = true;

                efname.Text = dt.Rows[0]["FirstName"].ToString();
                elname.Text = dt.Rows[0]["LastName"].ToString();
                ephoneno.Text = dt.Rows[0]["ContactNumber"].ToString();
                eemailid.Text = dt.Rows[0]["EmaiID"].ToString();
                userid.Text = dt.Rows[0]["ID"].ToString();
                Label47.Text = dt.Rows[0]["Phone"].ToString();
                Label42.Text = dt.Rows[0]["Ext"].ToString();

                txtfname.Text = dt.Rows[0]["FirstName"].ToString();
                txtlastname.Text = dt.Rows[0]["LastName"].ToString();
                txtphone.Text = dt.Rows[0]["ContactNumber"].ToString();
                txtemailid.Text = dt.Rows[0]["EmaiID"].ToString();
                userid.Text = dt.Rows[0]["ID"].ToString();

                TextBox2.Text = dt.Rows[0]["Phone"].ToString();
                TextBox3.Text = dt.Rows[0]["Ext"].ToString();

                btnsubmit.Visible = true;
                Panel3.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }

    }
    protected void ddname_SelectedIndexChanged(object sender, EventArgs e)
    {
        statuslable.Text = "";
    }

    protected void dropemployee_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        pnlmobcar.Visible = true;
        Panel19.Visible = false;

        panel11.Visible = true;
        panel12.Visible = true;
        panel13.Visible = true;
        panel14.Visible = true;
        panel17.Visible = true;
        TextBox3.Visible = true;

        statuslable.Text = "";
        btnEdit.Visible = false;
        btnUpdate.Visible = true;

        Label42.Visible = false;
        Label47.Visible = false;
        //Label49.Visible = false;

        userid.Visible = true;

        efname.Visible = false;
        elname.Visible = false;
        ephoneno.Visible = false;
        eemailid.Visible = false;

        btnsubmit.Visible = false;
        Panel3.Visible = false;

        try
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * From VisitorProfile inner join PartyMoreUserTbl on VisitorProfile.ID=PartyMoreUserTbl.VisitorID  Where VisitorProfile.ID='" + txtvisiotrID.Text + "'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                txtvisiotrID.Text = dt.Rows[0]["ID"].ToString();
                Panel4.Visible = true;
                Panel2.Visible = true;

                txtfname.Text = dt.Rows[0]["FirstName"].ToString();
                txtlastname.Text = dt.Rows[0]["LastName"].ToString();
                txtphone.Text = dt.Rows[0]["ContactNumber"].ToString();
                txtemailid.Text = dt.Rows[0]["EmaiID"].ToString();
                userid.Text = dt.Rows[0]["ID"].ToString();

                TextBox2.Text = dt.Rows[0]["Phone"].ToString();
                TextBox3.Text = dt.Rows[0]["Ext"].ToString();

                btnsubmit.Visible = false;
                Panel3.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
    }

    protected void fillparty()
    {
        int acce = 0;

        SqlCommand cmdec = new SqlCommand("Select Email from Party_master where Email='" + txtemailid.Text + "'", con);
        SqlDataAdapter adpec = new SqlDataAdapter(cmdec);
        DataTable dsec = new DataTable();
        adpec.Fill(dsec);

        if (dsec.Rows.Count == 0)
        {
            acce = 0;
        }
        else
        {
            if (txtemailid.Text.Length > 0)
            {
                acce = 2;
                txtemailid.Focus();
            }
            else
            {
                acce = 0;
            }
        }
        if (acce == 0)
        {
            statuslable.Text = "";

            string date = "select Convert(nvarchar,StartDate,101) as StartDate,Convert(nvarchar,EndDate,101) EndDate from [ReportPeriod] where Compid = '" + Session["Comid"].ToString() + "' and Whid='" + dropbusiness.SelectedValue + "' and Active='1'";
            SqlCommand cmd1111111 = new SqlCommand(date, con);
            SqlDataAdapter adp1111 = new SqlDataAdapter(cmd1111111);
            DataTable dt1111 = new DataTable();
            adp1111.Fill(dt1111);

            if (dt1111.Rows.Count > 0)
            {
                groupclass();

                SqlCommand cmddate = new SqlCommand("select StartDateOfAccountYear from CompanyMaster where Compid='" + Session["Comid"].ToString() + "'", con);
                SqlDataAdapter dtpdate = new SqlDataAdapter(cmddate);
                DataTable dtdate = new DataTable();
                dtpdate.Fill(dtdate);

                if (dtdate.Rows.Count > 0)
                {
                    if (dtdate.Rows[0]["StartDateOfAccountYear"].ToString() != "")
                    {

                        //if (Convert.ToDateTime(txtdate.Text) < Convert.ToDateTime(dtdate.Rows[0]["StartDateOfAccountYear"].ToString()))
                        //{
                        //    lblmsg.Text = "";
                        //    lblmsg.Text = "Start date can not be earlier then the " + Convert.ToDateTime(dtdate.Rows[0]["StartDateOfAccountYear"].ToString()).ToShortDateString() + "";

                        //}
                        //else
                        //{
                        //bool access = UserAccess.Usercon("Party_Master", lblpno.Text, "PartyId", "", "", "id", "Party_Master");
                        //if (access == true)
                        //{

                        string qryStr = " insert into AccountMaster(ClassId,AccountId,GroupId,AccountName,Description,Balance,Date,InventoryFlag,BalanceOfLastYear,DateTimeOfLastUpdatedBalance,Status,compid,Whid) " +
                                 " values ('" + classid + "','" + accid + "','" + groupid + "','" + TextBox1.Text + "','New Party','0'," + System.DateTime.Now.ToShortDateString() + ",'0','0','" + System.DateTime.Now.ToShortDateString() + "','1','" + Session["Comid"].ToString() + "','" + dropbusiness.SelectedValue.ToString() + "')";
                        SqlCommand cm = new SqlCommand(qryStr, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();

                        }

                        cm.ExecuteNonQuery();
                        con.Close();

                        string str1113 = "select max(Id) as Aid from AccountMaster";
                        SqlCommand cmd1113 = new SqlCommand(str1113, con);
                        SqlDataAdapter adp1113 = new SqlDataAdapter(cmd1113);
                        DataTable ds1113 = new DataTable();
                        adp1113.Fill(ds1113);
                        Session["maxaid"] = ds1113.Rows[0]["Aid"].ToString();



                        string st153 = "select Report_Period_Id  from ReportPeriod where Compid='" + Session["Comid"].ToString() + "' and Whid='" + dropbusiness.SelectedValue + "' and Active='1'";
                        SqlCommand cmd153 = new SqlCommand(st153, con);
                        SqlDataAdapter adp153 = new SqlDataAdapter(cmd153);
                        DataTable ds153 = new DataTable();
                        adp153.Fill(ds153);
                        Session["reportid"] = ds153.Rows[0]["Report_Period_Id"].ToString();


                        string st1531 = "select Report_Period_Id from [ReportPeriod] where ReportPeriod.Report_Period_Id<'" + Session["reportid"] + "' and  Whid='" + dropbusiness.SelectedValue + "'  order by Report_Period_Id Desc";
                        SqlCommand cmd1531 = new SqlCommand(st1531, con);
                        SqlDataAdapter adp1531 = new SqlDataAdapter(cmd1531);
                        DataTable ds1531 = new DataTable();
                        adp1531.Fill(ds1531);
                        Session["reportid1"] = ds1531.Rows[0]["Report_Period_Id"].ToString();

                        string str4562 = "insert into AccountBalance(AccountMasterId,Balance,Report_Period_Id) values ('" + Session["maxaid"].ToString() + "','" + 0 + "','" + Session["reportid1"] + "')";
                        SqlCommand cmd4562 = new SqlCommand(str4562, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmd4562.ExecuteNonQuery();
                        con.Close();

                        string str456 = "insert into AccountBalance(AccountMasterId,Balance,Report_Period_Id) values ('" + Session["maxaid"].ToString() + "','" + 0 + "','" + Session["reportid"].ToString() + "')";
                        SqlCommand cmd456 = new SqlCommand(str456, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmd456.ExecuteNonQuery();
                        con.Close();

                        string str111 = "insert into AccountBalanceLimit(AccountId,BalanceLimitTypeId,BalancelimitAmount,DateTime,Whid) " +
                              " values('" + accid + "','1','0.00','" + System.DateTime.Now.ToShortDateString() + "','" + dropbusiness.SelectedValue + "')";
                        SqlCommand cmd111 = new SqlCommand(str111, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmd111.ExecuteNonQuery();
                        con.Close();

                        SqlDataAdapter ad = new SqlDataAdapter("select max(AccountBalanceLimitId) as balid from AccountBalanceLimit where Whid='" + dropbusiness.SelectedValue + "'", con);
                        DataSet ds112 = new DataSet();
                        ad.Fill(ds112);
                        if (ds112.Tables[0].Rows.Count > 0)
                        {
                            ViewState["balid"] = ds112.Tables[0].Rows[0]["balid"].ToString();
                        }

                        SqlConnection conn3 = new SqlConnection(strconn);

                        string stffg = "select partytypeid from PartytTypeMaster  where PartyCategoryId='7' and  compid='" + Session["Comid"].ToString() + "' and PartType not in('Employee') order by PartType";
                        SqlDataAdapter daffg = new SqlDataAdapter(stffg, conn3);
                        DataTable dtffg = new DataTable();
                        daffg.Fill(dtffg);


                        string ins1 = "insert into Party_master(Account,Compname,Contactperson,Address,City,State,Country,Website,GSTno,Incometaxno,Email,Phoneno,DataopID, " +
                        " PartyTypeId,AssignedAccountManagerId,AssignedRecevingDepartmentInchargeId,AssignedPurchaseDepartmentInchargeId,AssignedShippingDepartmentInchargeId, " +
                        " AssignedSalesDepartmentIncharge,StatusMasterId,Fax,AccountnameID,AccountBalanceLimitId,id,Whid,Zipcode,PartyTypeCategoryNo) " +
                        " values ( '" + accid + "','" + TextBox1.Text + "','" + txtfname.Text + ' ' + txtlastname.Text + "', " +
                        "'','" + ViewState["City"].ToString() + "','" + ViewState["State"].ToString() + "','" + ViewState["Country"].ToString() + "','', " +
                        " '' ,'','" + txtemailid.Text + "','" + txtphone.Text + "','1', '" + dtffg.Rows[0]["partytypeid"].ToString() + "' ,'" + "' ,'', " +
                        " '' , '' , '' ,'1' , '' ,'1','" + ViewState["balid"] + "','" + Session["Comid"].ToString() + "','" + dropbusiness.SelectedValue.ToString() + "','','7')";
                        SqlCommand cmd3 = new SqlCommand(ins1);
                        cmd3.Connection = conn3;
                        conn3.Open();
                        cmd3.ExecuteNonQuery();

                        SqlConnection conn5 = new SqlConnection(strconn);

                        string sel = "select max(PartyID) as PartyID from Party_master";
                        SqlCommand cmd5 = new SqlCommand(sel);
                        cmd5.Connection = conn5;
                        SqlDataAdapter da5 = new SqlDataAdapter();
                        da5.SelectCommand = cmd5;
                        DataSet ds5 = new DataSet();
                        da5.Fill(ds5, "Party_master");

                        ViewState["PartyMasterId"] = Convert.ToInt32(ds5.Tables[0].Rows[0]["PartyID"]);

                        string strgetusername = "select * from Party_master where PartyID='" + Convert.ToInt32(ds5.Tables[0].Rows[0]["PartyID"]) + "'";
                        SqlCommand cmdusername = new SqlCommand(strgetusername, con);
                        SqlDataAdapter adpusername = new SqlDataAdapter(cmdusername);
                        DataTable dsusername = new DataTable();
                        adpusername.Fill(dsusername);

                        string yearactive = "select LEFT(Name,4) as Name  from ReportPeriod where Compid='" + Session["Comid"].ToString() + "' and Active='1' and Whid='" + dropbusiness.SelectedValue + "'";
                        SqlCommand cmdactive = new SqlCommand(yearactive, con);
                        SqlDataAdapter adpactive = new SqlDataAdapter(cmdactive);
                        DataTable dsactive = new DataTable();
                        adpactive.Fill(dsactive);


                        string username = txtfname.Text + dsusername.Rows[0]["PartyID"].ToString();
                        string Password = "Party" + dsactive.Rows[0]["Name"].ToString() + "++";

                        Session["PartyUser"] = username.ToString();
                        Session["PartyPassword"] = ClsEncDesc.Decrypted(Password.ToString());

                        SqlConnection conn6 = new SqlConnection(strconn);

                        DataTable dtdesg = select("select EmployeeMaster.DesignationMasterId from EmployeeMaster where EmployeeMasterID='" + dropdepartment.SelectedValue + "'");

                        SqlDataAdapter dapart = new SqlDataAdapter("select [DeptID],[RoleId] from [DesignationMaster] where [DesignationMasterId]='" + dtdesg.Rows[0]["DesignationMasterId"].ToString() + "'", con);
                        DataTable dtpart = new DataTable();
                        dapart.Fill(dtpart);

                        string ins6 = "insert into User_master(Name,Address ,City,State,Country,Phoneno,EmailID ,Username,Department,Accesslevel,PartyID,DesigantionMasterId,Photo,Active,Extention,zipcode)" +
                                                      "values ('" + txtfname.Text + ' ' + txtlastname.Text + "','','" + ViewState["City"].ToString() + "','" + ViewState["State"].ToString() + "','" + ViewState["Country"].ToString() + "','" + txtphone.Text + "','" + txtemailid.Text + "','" + username + "','" + dtpart.Rows[0]["DeptID"].ToString() + "','1','" + Convert.ToInt32(ds5.Tables[0].Rows[0]["PartyId"]) + "','" + dtdesg.Rows[0]["DesignationMasterId"].ToString() + "','' ,'1','','')";
                        SqlCommand cmd6 = new SqlCommand(ins6);
                        cmd6.Connection = conn6;
                        conn6.Open();
                        cmd6.ExecuteNonQuery();

                        SqlConnection conn10 = new SqlConnection(strconn);
                        string sel11 = "select max(UserID) as UserID from User_master";
                        SqlCommand cmd10 = new SqlCommand(sel11);
                        cmd10.Connection = conn10;
                        SqlDataAdapter da10 = new SqlDataAdapter();
                        da10.SelectCommand = cmd10;
                        DataSet ds10 = new DataSet();
                        da10.Fill(ds10, "User_master");

                        string instrole = "insert into User_Role(User_id,Role_id,ActiveDeactive) values ('" + Convert.ToInt32(ds10.Tables[0].Rows[0]["UserID"]) + "','" + dtpart.Rows[0]["RoleId"].ToString() + "','1')";
                        SqlCommand cmdid = new SqlCommand(instrole, con);
                        cmdid.Connection = con;
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmdid.ExecuteNonQuery();
                        con.Close();

                        SqlConnection conn = new SqlConnection(strconn);

                        string str11 = "select max(UserID) as UserID from User_master";
                        SqlCommand cmd11 = new SqlCommand(str11, conn);
                        SqlDataAdapter adp11 = new SqlDataAdapter(cmd11);
                        DataTable ds11 = new DataTable();
                        adp11.Fill(ds11);


                        Session["userid"] = ds11.Rows[0]["UserID"].ToString();
                        Session["username"] = txtfname.Text + ' ' + txtlastname.Text;

                        statuslable.Text = "";
                        statuslable.Text = "Record inserted successfully";

                    }
                }
            }

        }
        else if (acce == 2)
        {
            statuslable.Visible = true;
            statuslable.Text = "This email ID is already in use.";
        }
    }

    protected void groupclass()
    {
        groupid = 15;
        DataTable dtt = new DataTable();
        dtt = (DataTable)select("Select Max(AccountId) as aid from AccountMaster where GroupId='15' and Whid='" + dropbusiness.SelectedValue + "'");
        if (dtt.Rows.Count > 0)
        {
            if (dtt.Rows[0]["aid"].ToString() != "")
            {
                //if(Convert.ToInt32(dtt.Rows[0]["aid"].ToString() >  )
                //{

                //}
                int gid = Convert.ToInt32(dtt.Rows[0]["aid"].ToString()) + 1;
                accid = Convert.ToString(gid);
            }
            else
            {
                accid = Convert.ToString(30000);
            }
        }

        if (groupid == 15)
        {
            classid = 5;
        }
        else if (groupid == 2)
        {
            classid = 1;
        }
        else if (groupid == 5)
        {
            classid = 1;
        }
        else if (groupid == 20)
        {
            classid = 5;
        }
    }

    protected void acccc(string accgenid)
    {
        int act = Convert.ToInt32(accgenid) + 1;
        DataTable dtrs = select("select AccountId from AccountMaster where AccountId='" + act + "' and Whid='" + dropbusiness.SelectedValue + "'");
        if (dtrs.Rows.Count > 0)
        {
            accid += 1;
            acccc(accid);
        }
    }

    protected DataTable select(string qu)
    {
        SqlCommand cmd = new SqlCommand(qu, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        return dt;
    }

    protected void fillpasss()
    {
        //SqlDataAdapter da11 = new SqlDataAdapter("select LogoUrl from CompanyWebsitMaster where whid='" + dropbusiness.SelectedValue + "'", con);
        //DataTable dt11 = new DataTable();
        //da11.Fill(dt11);

        //if (dt11.Rows.Count > 0)
        //{
        //    Img1.ImageUrl = "~/ShoppingCart/images/" + dt11.Rows[0]["LogoUrl"].ToString();
        //}

        //SqlDataAdapter daza = new SqlDataAdapter("select warehousemaster.Name,CityMasterTbl.CityName,StateMasterTbl.StateName,CountryMaster.CountryName,CompanyWebsiteAddressMaster.Address1,CompanyWebsiteAddressMaster.Zip,CompanyWebsiteAddressMaster.Address2,CompanyWebsiteAddressMaster.Phone1,CompanyWebsiteAddressMaster.Phone2,CompanyWebsiteAddressMaster.Email from CompanyWebsiteAddressMaster inner join warehousemaster on warehousemaster.warehouseid=CompanyWebsiteAddressMaster.CompanyWebsiteMasterId inner join CityMasterTbl on CityMasterTbl.CityId=CompanyWebsiteAddressMaster.City inner join StateMasterTbl on StateMasterTbl.StateId=CompanyWebsiteAddressMaster.State inner join CountryMaster on CountryMaster.CountryId=CompanyWebsiteAddressMaster.Country where CompanyWebsiteAddressMaster.CompanyWebsiteMasterId='" + dropbusiness.SelectedValue + "'", con);
        //DataTable dtza = new DataTable();
        //daza.Fill(dtza);

        //if (dtza.Rows.Count > 0)
        //{
        //    lblcompanyname.Text = dtza.Rows[0]["Name"].ToString();
        //    lbladdress1.Text = dtza.Rows[0]["Address1"].ToString();            
        //    lblphoneno.Text = dtza.Rows[0]["Phone1"].ToString();
        //    lblemail.Text = dtza.Rows[0]["Email"].ToString();            
        //}

        //lblentrytt.Text = Label2.Text;
        //lblfirsttt.Text = txtfname.Text;
        //lbllasttt.Text = txtlastname.Text;
        //lblcontactnoo.Text = txtphone.Text;
        //lblmeetingwith.Text = dropemployee.SelectedItem.Text;

        System.IO.MemoryStream msReport = new System.IO.MemoryStream();

        Document document = new Document(PageSize.A6, 0f, 0f, 30f, 30f);

        PdfWriter writer = PdfWriter.GetInstance(document, msReport);

        this.EnableViewState = false;

        Response.Charset = string.Empty;

        document.AddSubject("Export to PDF");
        document.Open();

        iTextSharp.text.Table datatable4 = new iTextSharp.text.Table(3);

        datatable4.Padding = 2;
        datatable4.Spacing = 1;
        datatable4.Width = 90;

        float[] headerwidths4 = new float[3];

        headerwidths4[0] = 10;
        headerwidths4[1] = 80;
        headerwidths4[2] = 10;
        datatable4.Widths = headerwidths4;

        Cell cell = new Cell(new Phrase(Img1.ImageUrl));
        cell.HorizontalAlignment = Element.ALIGN_LEFT;
        cell.Colspan = 3;
        cell.Border = Rectangle.NO_BORDER;

        cell = new Cell(new Phrase(lblcompanyname.Text + " \n " + lbladdress1.Text + " \n " + lblphoneno.Text + " \n " + lblemail.Text, FontFactory.GetFont(FontFactory.HELVETICA, 12)));
        cell.HorizontalAlignment = Element.ALIGN_RIGHT;
        cell.Colspan = 3;
        cell.Border = Rectangle.NO_BORDER;

        datatable4.AddCell(cell);

        datatable4.DefaultCellBorderWidth = 1;
        datatable4.DefaultHorizontalAlignment = 1;


        Cell cell31 = new Cell(new Phrase("Entry Time : " + lblentrytt.Text, FontFactory.GetFont(FontFactory.HELVETICA, 10)));
        cell31.HorizontalAlignment = Element.ALIGN_LEFT;

        cell31.Colspan = 3;
        cell31.Border = Rectangle.NO_BORDER;

        datatable4.AddCell(cell31);

        Cell cell310 = new Cell(new Phrase("First Name : " + lblfirsttt.Text, FontFactory.GetFont(FontFactory.HELVETICA, 10)));
        cell310.HorizontalAlignment = Element.ALIGN_LEFT;

        cell310.Colspan = 3;
        cell310.Border = Rectangle.NO_BORDER;

        datatable4.AddCell(cell310);

        Cell cell311 = new Cell(new Phrase("Last Name : " + lbllasttt.Text, FontFactory.GetFont(FontFactory.HELVETICA, 10)));
        cell311.HorizontalAlignment = Element.ALIGN_LEFT;

        cell311.Colspan = 3;
        cell311.Border = Rectangle.NO_BORDER;

        datatable4.AddCell(cell311);

        Cell cell312 = new Cell(new Phrase("Contact No : " + lblcontactnoo.Text, FontFactory.GetFont(FontFactory.HELVETICA, 10)));
        cell312.HorizontalAlignment = Element.ALIGN_LEFT;

        cell312.Colspan = 3;
        cell312.Border = Rectangle.NO_BORDER;

        datatable4.AddCell(cell312);

        Cell cell313 = new Cell(new Phrase("Meeting with : " + lblmeetingwith.Text, FontFactory.GetFont(FontFactory.HELVETICA, 10)));
        cell313.HorizontalAlignment = Element.ALIGN_LEFT;

        cell313.Colspan = 3;
        cell313.Border = Rectangle.NO_BORDER;

        datatable4.AddCell(cell313);
        document.Add(datatable4);
        document.Close();

        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=VisitorPass.pdf");
        Response.ContentType = "application/pdf";
        Response.BinaryWrite(msReport.ToArray());

        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        fillpasss();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        RadioButtonList2.SelectedValue = "0";
        RadioButtonList2_SelectedIndexChanged(sender, e);

        Button2.Visible = false;
        printpass.Visible = false;
        paneluper.Visible = true;

        Label2.Text = DateTime.Now.ToShortTimeString().ToString();

        lblsystemtime.Text = DateTime.Now.ToShortTimeString().ToString();

        panel11.Visible = true;
        panel12.Visible = true;
        panel13.Visible = true;
        panel14.Visible = true;

        userid.Visible = true;

        efname.Visible = false;
        elname.Visible = false;
        ephoneno.Visible = false;
        eemailid.Visible = false;

        Label1.Visible = true;

        Panel4.Visible = true;
        Panel1.Visible = true;
        Panel2.Visible = true;
        Panel3.Visible = true;
        Panel1.Visible = true;
        Panel6.Visible = false;

        lblempno = number(4).ToString();
        userid.Text = lblempno.Substring(0, 4);
        btnEdit.Visible = false;
        btnUpdate.Visible = false;

        drop_request_SelectedIndexChanged(sender, e);
        fillstorewithfilter();
        dropbusiness_SelectedIndexChanged(sender, e);
        dropemployee.Items.Insert(0, "-Select-");
        dropemployee.Items[0].Value = "0";

        fillparties();

    }
    protected void RadioButtonList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlmobcar.Visible = true;
        Panel19.Visible = false;

        if (RadioButtonList2.SelectedValue == "0")
        {
            Panel15.Visible = false;
            Panel16.Visible = true;
        }
        else
        {
            Panel15.Visible = true;
            Panel16.Visible = false;
        }

        panel11.Visible = true;
        panel12.Visible = true;
        panel13.Visible = true;
        panel14.Visible = true;
        panel17.Visible = true;
        TextBox3.Visible = true;

        Label42.Visible = false;
        Label47.Visible = false;
        //Label49.Visible = false;

        txtfname.Text = "";
        txtlastname.Text = "";
        txtphone.Text = "";
        txtemailid.Text = "";

        TextBox2.Text = "";
        TextBox3.Text = "";

        btnUpdate.Visible = false;

        statuslable.Text = "";
        Panel4.Visible = true;
        Panel2.Visible = true;
        Panel3.Visible = true;
        Panel1.Visible = true;
        Panel6.Visible = false;

        userid.Visible = true;

        btnEdit.Visible = false;
        efname.Visible = false;
        elname.Visible = false;
        ephoneno.Visible = false;
        eemailid.Visible = false;

        btnsubmit.Visible = true;
    }

    protected void fillparties()
    {
        DataTable dtpart = select("select Party_master.Compname,Party_master.PartyID from Party_master where Party_master.id='" + Session["Comid"].ToString() + "' and Party_master.StatusMasterId='1' and Party_master.PartyTypeCategoryNo in (select PartyMasterCategoryNo from PartyMasterCategory where Name in('Customer','Vendor','Visitor')) order by Compname asc");

        ddparty.DataSource = dtpart;
        ddparty.DataTextField = "Compname";
        ddparty.DataValueField = "PartyID";
        ddparty.DataBind();

        ddparty.Items.Insert(0, "-Select-");
        ddparty.Items[0].Value = "0";
    }

    protected void fillcarrier()
    {
        ddlcarrier.Items.Clear();

        string str = "select ID,CarrirName from SMSCarrirMaster where Country='" + ViewState["Country"].ToString() + "'";

        SqlDataAdapter da = new SqlDataAdapter(str, con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            ddlcarrier.DataSource = dt;
            ddlcarrier.DataTextField = "CarrirName";
            ddlcarrier.DataValueField = "ID";
            ddlcarrier.DataBind();
        }

        ddlcarrier.Items.Insert(0, "-Select-");
        ddlcarrier.Items[0].Value = "0";
    }

}





