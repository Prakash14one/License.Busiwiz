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

public partial class ShoppingCart_Admin_CustomerNewServiceCall : System.Web.UI.Page
{
    int  empid=0;
    //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString"].ToString());
    //string strconn = ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString"].ConnectionString;
    SqlConnection con;
        string strconn;
    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        strconn = pgcon.dynconn.ConnectionString;
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();


        Page.Title = pg.getPageTitle(page);
        afterLogin clsafterlogin = new afterLogin();

        valuecounter();
       
        try
        {
            if (!IsPostBack)
            {
                TxtEntryDate.Text = System.DateTime.Now.ToShortDateString();

                Label1.Visible = false;
                
                fillstore();
                fillproblemtype();
                filluser();
                
                fillcountry();
                fillddlstore();
            }

        }
        catch (Exception ex)
        {

            Label1.Visible = true;
            Label1.Text = "Error On Page :" + ex.Message;


        }
        finally { }

    }
    

    public DataSet fillddl()
    {
        SqlCommand cmd = new SqlCommand("Sp_Select_Problemtype", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@compid", Session["comid"]);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;
    }
    protected void ImageButton10_Click(object sender, ImageClickEventArgs e)
    {
        if (txtemail.Text == "")
        {
            Label1.Text = "Insert Your Email id for check";
            Label1.ForeColor = System.Drawing.Color.Red;
            return;
        }

        string str4 = "select Email, PartyId from Party_master where Email='" + txtemail.Text + "'";
        SqlConnection conn7 = new SqlConnection(strconn);
        SqlCommand cmd7 = new SqlCommand(str4);
        cmd7.Connection = conn7;
        SqlDataAdapter da7 = new SqlDataAdapter();
        da7.SelectCommand = cmd7;
        DataSet ds7 = new DataSet();
        da7.Fill(ds7);

        int i1 = ds7.Tables[0].Rows.Count;
        if (i1 == 0)
        {
            Label1.Visible = true;
            Label1.Text = "Available";
            Label1.ForeColor = System.Drawing.Color.Green;
            ViewState["not"] = null;
            Label6.Visible = false;
            Label7.Visible = false;
            LinkButton4.Visible = false;
        }
        else
        {
            Label1.Visible = false;
            Label6.Visible = true;
            Label7.Visible = true;
            LinkButton4.Visible = true;
            //Label1.Text = "This email id is already registered with us as a member if u have forgotten your password  click here or you may try your 10 digit phone no as password (ex-1234567890) ";
            //Label1.ForeColor = System.Drawing.Color.Red;
            ViewState["not"] = 1;
        }



        string str3 = "select UserID,EmailID from User_master where EmailID='" + txtemail.Text + "'";
        SqlConnection conn8 = new SqlConnection(strconn);
        SqlCommand cmd8 = new SqlCommand(str3);
        cmd8.Connection = conn8;
        SqlDataAdapter da8 = new SqlDataAdapter();
        da8.SelectCommand = cmd8;
        DataSet ds8 = new DataSet();
        da8.Fill(ds8);

        int i = ds8.Tables[0].Rows.Count;
        if (i == 0)
        {
            Label1.Text = "Available";
            Label1.ForeColor = System.Drawing.Color.Green;
            ViewState["not"] = null;
        }
        else
        {
            Label1.Text = "Not Available Please select another.";
            Label1.ForeColor = System.Drawing.Color.Red;
            ViewState["not"] = 1;
        }
        ModalPopupExtender5.Show();
    }
    protected void ddlcountry_SelectedIndexChanged1(object sender, EventArgs e)
    {
        SqlCommand cmdst = new SqlCommand("select * from StateMasterTbl where CountryId = '" + Convert.ToInt32(ddlcountry.SelectedValue) + "'", con);
        SqlDataAdapter dtpst = new SqlDataAdapter(cmdst);
        DataSet dsst = new DataSet();

        dtpst.Fill(dsst);

        ddlstate.DataSource = dsst;
        ddlstate.DataTextField = "StateName";
        ddlstate.DataValueField = "StateId";
        ddlstate.DataBind();

        ddlstate.Items.Insert(0, "--Select--");
        ModalPopupExtender5.Show();
    }
    protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlCommand cmdcity = new SqlCommand("select * from CityMasterTbl where StateId = '" + Convert.ToInt32(ddlstate.SelectedValue) + "'", con);
        SqlDataAdapter dtpcity = new SqlDataAdapter(cmdcity);
        DataSet dscity = new DataSet();
        dtpcity.Fill(dscity);

        ddlcity.DataSource = dscity;
        ddlcity.DataTextField = "CityName";
        ddlcity.DataValueField = "CityId";
        ddlcity.DataBind();

        ddlcity.Items.Insert(0, "--Select--");
        ModalPopupExtender5.Show();
       // ddlcity.Items.Clear();
        //if (ddlstate.SelectedIndex > 0)
        //{
        //    SqlConnection conn2 = new SqlConnection(strconn);
        //    string str2 = "select distinct  CityId,CityName from CityMasterTbl where StateId = '" + Convert.ToInt32(ddlstate.SelectedValue) + "' order by CityName ASC";
        //    SqlCommand cmd2 = new SqlCommand(str2, conn2);
        //    cmd2.Connection = conn2;
        //    SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        //    //da2.SelectCommand = cmd2;
        //    DataTable ds2 = new DataTable();
        //    da2.Fill(ds2);
        //    if (ds2.Rows.Count > 0)
        //    {
        //        ddlcity.DataSource = ds2;
        //        ddlcity.DataTextField = "CityName";
        //        ddlcity.DataValueField = "CityId";

        //        ddlcity.DataBind();
        //    }
        //    ddlcity.Items.Insert(0, "--Select--");
        //    ddlcity.Items[0].Value = "0";
        //    ddlcity_SelectedIndexChanged(sender, e);
        //    //addcity.Visible = true;
        //}
        //else
        //{
        //    //ddlcity.Items.Clear();
        //    ddlcity_SelectedIndexChanged(sender, e);
        //}
        ModalPopupExtender5.Show();
    }
    protected void RegisterBtn_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlcountry.SelectedIndex == 0)
        {
            Label2.Text = "Please select country";
            return;
        }
        if (ddlstate.SelectedIndex == 0)
        {
            Label2.Text = "Please select state";
            return;
        }
        if (ddlcity.SelectedIndex == 0)
        {
            Label2.Text = "Please select City";
            return;
        }
        Label2.Text = "";
        ImageButton10_Click(sender, e);
        if (Convert.ToInt32(ViewState["not"]) == 1)
        {
            return;
        }
        if (CheckBox1.Checked == true)
        {
            int accid = 10000;
            SqlDataAdapter ad5 = new SqlDataAdapter("SELECT     AccountId " +
               "   FROM         AccountMaster " +
                //  " WHERE     (AccountId < 30000 AND AccountId > 10000) AND Whid='"+Session["WH"]+"' order by AccountId desc", con);
            " WHERE     (AccountId < 30000 AND AccountId > 10000) AND Whid='" + ddlstorename.SelectedValue + "' order by AccountId desc", con);
            DataTable ds1125 = new DataTable();
            ad5.Fill(ds1125);
            if (ds1125.Rows.Count > 0)
            {
                accid = Convert.ToInt32(ds1125.Rows[0]["AccountId"]) + 1;
            }
            else
            {

            }

            string st = " insert into AccountMaster(ClassId,GroupId,AccountName,Description,Balance,Date,InventoryFlag,BalanceOfLastYear,DateTimeOfLastUpdatedBalance,Status,compid,Whid) " +
                       " values ('1','10','New Customer','new Order From Online Shopping','0','" + System.DateTime.Now.ToShortDateString() + "','0','0','" + System.DateTime.Now.ToShortDateString() + "',1,'" + Session["comid"] + "','" + ddlstorename.SelectedValue + "')";
            SqlCommand cm = new SqlCommand(st, con);
            con.Open();
            cm.ExecuteNonQuery();
            con.Close();


            //SqlDataAdapter ad = new SqlDataAdapter("select max(AccountId) as account from AccountMaster where Whid='" + ddlstorename.SelectedValue + "'", con);
            SqlDataAdapter ad = new SqlDataAdapter("select max(AccountId) as AccountId from AccountMaster where Whid='" + ddlstorename.SelectedValue + "'", con);
            DataSet ds112 = new DataSet();
            ad.Fill(ds112);
            

            ViewState["AccountId"] = ds112.Tables[0].Rows[0]["AccountId"].ToString();

            string stbllim = " INSERT INTO AccountBalanceLimit (AccountId ,BalanceLimitTypeId  ,BalancelimitAmount,DateTime,Whid) " +
                     " VALUES(" + accid + ",'2','500','" + System.DateTime.Today.ToString("MM/dd/yyyy") + "','" + ddlstorename.SelectedValue + "' )   ";
            SqlCommand cmbllim = new SqlCommand(stbllim, con);
            con.Open();
            cmbllim.ExecuteNonQuery();
            con.Close();



            SqlConnection conn3 = new SqlConnection(strconn);
            string ins1 = "insert into Party_master(Account,Compname,Contactperson,Address,City,State,Country,Website,GSTno,Incometaxno,Email,Phoneno,DataopID,PartyTypeId,AssignedAccountManagerId,AssignedRecevingDepartmentInchargeId,AssignedPurchaseDepartmentInchargeId,AssignedShippingDepartmentInchargeId,AssignedSalesDepartmentIncharge,StatusMasterId,Fax,AccountnameID,id,Whid) values ( '" + Convert.ToInt32(ds112.Tables[0].Rows[0]["AccountId"]) + "','" + txtfirstname.Text + "','" + txtfirstname.Text + "','" + txtaddress.Text + "','" + ddlcity.Text + "','" + ddlstate.SelectedValue + "','" + ddlcountry.SelectedValue + "','indiaauthentic', '1' ,'1','" + txtemail.Text + "','" + txtphoneno.Text + "','1', '2' ,'1' ,'1' , '1' , '1' , '1' ,'5' , '1' , '1','" + Session["comid"] + "','" + ddlstorename.SelectedValue + "' )";
            SqlCommand cmd3 = new SqlCommand(ins1);
            cmd3.Connection = conn3;
            conn3.Open();
            cmd3.ExecuteNonQuery();

            SqlConnection conn5 = new SqlConnection(strconn);
            string sel = "select max(PartyID) as PartyID from Party_master where Whid='" + ddlstorename.SelectedValue + "'";
            SqlCommand cmd5 = new SqlCommand(sel);
            cmd5.Connection = conn5;
            SqlDataAdapter da5 = new SqlDataAdapter();
            da5.SelectCommand = cmd5;
            DataSet ds5 = new DataSet();
            da5.Fill(ds5, "Party_master");

            SqlConnection conn6 = new SqlConnection(strconn);
            string ins6 = "insert into User_master(Name,Address,City,State,Country,Phoneno,EmailID,Username,Department,Accesslevel,PartyID,DesigantionMasterId,Photo,Active,Extention,zipcode) values ('" + txtlastname.Text + " " + txtfirstname.Text + "','" + txtaddress.Text + "','" + ddlcity.SelectedValue + "','" + ddlstate.SelectedValue + "','" + ddlcountry.SelectedValue + "','" + txtphoneno.Text + "','" + txtemail.Text + "','" + txtemail.Text + "','1','1','" + Convert.ToInt32(ds5.Tables[0].Rows[0]["PartyId"]) + "','57','PP','true','1','" + txtzip.Text + "')";
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


            SqlConnection conn9 = new SqlConnection(strconn);
            string ins7 = "insert into Login_master(UserID,username,password,department,accesslevel,deptid,accessid) values ('" + Convert.ToInt32(ds10.Tables[0].Rows[0]["UserID"]) + "','" + txtemail.Text + "','" + txtpassword.Text + "','1','1','57','1')";
            SqlCommand cmd9 = new SqlCommand(ins7);
            cmd9.Connection = conn9;
            conn9.Open();
            cmd9.ExecuteNonQuery();
            //sendmail(txtemail.Text);

            SqlConnection conn = new SqlConnection(strconn);

            string str11 = "select max(UserID) as userid from Login_master";
            SqlCommand cmd11 = new SqlCommand(str11, conn);
            //cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adp11 = new SqlDataAdapter(cmd11);
            DataTable ds11 = new DataTable();
            adp11.Fill(ds11);

            Session["userid"] = ds11.Rows[0]["userid"].ToString();
            Session["username"] = txtemail.Text;

            

            SqlCommand cmd = new SqlCommand("SELECT UserID,Name FROM dbo.User_master inner join Party_master ON User_master.PartyID = Party_master.PartyID where Party_master.id='" + Session["comid"] + "' and Party_master.Whid='" + ddlstorename.SelectedValue + "' order by Name", con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);

            ddlUserMaster.DataSource = ds;
            ddlUserMaster.DataTextField = "Name";
            ddlUserMaster.DataValueField = "UserID";
            ddlUserMaster.DataBind();
            ddlUserMaster.Items.Insert(0, "--Select--");
            ddlUserMaster.SelectedItem.Value = "0";
        }
        else
        {
            Label2.Text = "Please accept The terms & conditions.";
        }
        ModalPopupExtender5.Hide();
        Label1.Text = "";
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void imgbtn4_Click(object sender, ImageClickEventArgs e)
    {
        ModalPopupExtender5.Hide();
    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {

    }
    protected void ddlProbType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void imgbtnAddNewUser_Click(object sender, ImageClickEventArgs e)
    {
        ModalPopupExtender5.Show();
    }

    protected void ImageButton48_Click(object sender, EventArgs e)
    {
        try
        {

            string partyid = Session["PartyId"].ToString();
            string fetchempid = "select EmployeeMasterID from EmployeeMaster where PartyID='" + partyid + "'";
            DataSet ds56 = new DataSet();
            SqlDataAdapter ft = new SqlDataAdapter(fetchempid, con);
            ft.Fill(ds56);
            if (ds56.Tables[0].Rows.Count > 0)
            {
                empid = Convert.ToInt16(ds56.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                empid = 0;
            }




            SqlCommand cmd = new SqlCommand("Sp_Insert_CusServiceCallMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Entrydate", Convert.ToDateTime(TxtEntryDate.Text));
            cmd.Parameters.AddWithValue("@ProblemTitle", txtProTitle.Text);
            cmd.Parameters.AddWithValue("@AssigendEmployeeID", empid);
            cmd.Parameters.AddWithValue("@ProblemDescription", txtProbDesc.Text);
            cmd.Parameters.AddWithValue("@ProblemType", DBNull.Value);
            cmd.Parameters.AddWithValue("@CustomerId", Convert.ToInt32(ddlUserMaster.SelectedValue));
            cmd.Parameters.AddWithValue("@ServiceDateandTime", DBNull.Value);
            cmd.Parameters.AddWithValue("@ServiceNotes", DBNull.Value);
            cmd.Parameters.AddWithValue("@ServiceStatusId", 1);
            cmd.Parameters.AddWithValue("@Email", DBNull.Value);
            cmd.Parameters.AddWithValue("@ProblemTypeId", Convert.ToInt32(ddlProbType.SelectedValue));
            SqlDataAdapter adp511 = new SqlDataAdapter(cmd);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();

           


          

            Label1.Visible = true;
            Label1.Text = "Record inserted successfully";
            txtProbDesc.Text = "";
            txtProTitle.Text = "";
            valuecounter();

        }
        catch (Exception ex)
        {
            Label1.Visible = true;
            Label1.Text = "Error : " + ex.Message;

        }
        finally
        {
        }
    }
    public void fillcountry()
    {
        SqlCommand cmd = new SqlCommand("Select * from CountryMaster", con);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataSet dscou = new DataSet();
        dtp.Fill(dscou);

        ddlcountry.DataSource = dscou;
        ddlcountry.DataTextField = "CountryName";
        ddlcountry.DataValueField = "CountryId";
        ddlcountry.DataBind();

        ddlcountry.Items.Insert(0, "--Select--");
    }


    protected void fillddlstore()
    {
        string str = "SELECT  WareHouseId,Name ,Address ,CurrencyId FROM  WareHouseMaster where comid='" + Session["comid"] + "'and [WareHouseMaster].Status='" + 1 + "'";
        SqlCommand cmdSL = new SqlCommand(str, con);
        SqlDataAdapter adpSL = new SqlDataAdapter(cmdSL);
        DataTable dtSL = new DataTable();
        adpSL.Fill(dtSL);

        ddlstorename.DataSource = dtSL;
        ddlstorename.DataTextField = "Name";
        ddlstorename.DataValueField = "WareHouseId";

        ddlstorename.DataBind();
    }
    protected void ddlcity_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void fillstore()
    {
        ddlWarehouse.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlWarehouse.DataSource = ds;
        ddlWarehouse.DataTextField = "Name";
        ddlWarehouse.DataValueField = "WareHouseId";
        ddlWarehouse.DataBind();


       
        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlWarehouse.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }


    }
    protected void filluser()
    {
       
        SqlCommand cmd = new SqlCommand("SELECT UserID, PartytTypeMaster.PartType+':'+Party_master.Contactperson AS Uname FROM dbo.User_master inner join Party_master ON User_master.PartyID = Party_master.PartyID inner join PartytTypeMaster on PartytTypeMaster.PartyTypeId=Party_master.PartyTypeId where Party_master.id='" + Session["comid"] + "' and Party_master.Whid='" + ddlWarehouse.SelectedValue + "' and PartytTypeMaster.PartType In ('Customer') order by User_master.Name,User_master.Phoneno ", con);

        
        cmd.CommandType = CommandType.Text;
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);


        ddlUserMaster.DataSource = ds;
        ddlUserMaster.DataTextField = "Uname";
        ddlUserMaster.DataValueField = "UserID";
        ddlUserMaster.DataBind();
        ddlUserMaster.Items.Insert(0, "--Select--");
        ddlUserMaster.SelectedItem.Value = "0";
    }
    protected void ddlWarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        filluser();
    }
    protected void btnviewservicelog_Click(object sender, EventArgs e)
    {
        string te = "CustomerServiceCall.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void LinkButton97666667_Click(object sender, ImageClickEventArgs e)
    {

        string te = "CustomersPartyRegister.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);


    }
    protected void LinkButton13_Click(object sender, ImageClickEventArgs e)
    {
        filluser();

    }
    protected void LinkButtonproblemtypeaddnew_Click(object sender, ImageClickEventArgs e)
    {

        string te = "ProblemType.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);


    }
    protected void LinkButtonproblemtyperefreshs_Click(object sender, ImageClickEventArgs e)
    {
        fillproblemtype();

    }
    protected void fillproblemtype()
    {
        ddlProbType.DataSource = (DataSet)fillddl();
        ddlProbType.DataTextField = "ProblemName";
        ddlProbType.DataValueField = "ProblemTypeId";
        ddlProbType.DataBind();
        ddlProbType.Items.Insert(0, "--Select--");
        ddlProbType.SelectedItem.Value = "0";
    }

    protected void valuecounter()
    {
        SqlCommand cm = new SqlCommand("SELECT     MAX(CustomerServiceCallMasterId) AS Id FROM         CustomerServiceCallMaster", con);
        cm.CommandType = CommandType.Text;
        SqlDataAdapter sadp = new SqlDataAdapter(cm);
        DataSet ds2 = new DataSet();
        sadp.Fill(ds2);

        int s = 1;

        if (ds2.Tables[0].Rows[0]["Id"].ToString() != "")
        {
            s = Convert.ToInt32(ds2.Tables[0].Rows[0]["Id"]) + 1;
        }
        else
        {
            s = 1;
        }
        TextBox1.Text = s.ToString();



    }

}
