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

public partial class ShoppingCart_Admin_AddNewServiceCall : System.Web.UI.Page
{
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

       

        if (!IsPostBack)
        {
            fillstore();

            FillMainStatus();

            ddlProbType.DataSource = (DataSet)fillddl();
            ddlProbType.DataTextField = "ProblemName";
            ddlProbType.DataValueField = "ProblemTypeId";
            ddlProbType.DataBind();
            ddlProbType.Items.Insert(0, "All");
            ddlProbType.Items[0].Value = "0";

            Label2.Text = System.DateTime.Now.ToString();




            filluser();
            Fillcomplaint();
            ddlcustomercomplanit_SelectedIndexChanged(sender, e);

            lblmsg.Text = "";
        }
    }


    //public DataSet fillddl()
    //{
    //    SqlCommand cmd = new SqlCommand("Sp_Select_Problemtype", con);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
    //    DataSet ds = new DataSet();
    //    adp.Fill(ds);
    //    return ds;
    //}
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
        // SqlCommand cmd = new SqlCommand("SELECT UserID,[Name]+':'+User_master.Phoneno AS Uname FROM dbo.User_master inner join Party_master ON User_master.PartyID = Party_master.PartyID where Party_master.id='" + Session["comid"] + "' and Party_master.Whid='" + ddlWarehouse.SelectedValue+ "' order by User_master.Name,User_master.Phoneno ", con);
        SqlCommand cmd = new SqlCommand("SELECT UserID, EmployeeMaster.EmployeeName as Uname FROM dbo.User_master inner join Party_master ON User_master.PartyID = Party_master.PartyID inner join PartytTypeMaster on PartytTypeMaster.PartyTypeId=Party_master.PartyTypeId inner join EmployeeMaster on EmployeeMaster.PartyID=Party_master.PartyID where EmployeeMaster.Whid='" + ddlWarehouse.SelectedValue + "' and Party_master.id='" + Session["comid"] + "' and Party_master.Whid='" + ddlWarehouse.SelectedValue + "' order by EmployeeMaster.EmployeeName ", con);


        cmd.CommandType = CommandType.Text;
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);


        ddlUserMaster.DataSource = ds;
        ddlUserMaster.DataTextField = "Uname";
        ddlUserMaster.DataValueField = "UserID";
        ddlUserMaster.DataBind();

        if (Session["userid"].ToString()!="")
        {
            ddlUserMaster.SelectedValue = Convert.ToString(Session["userid"]);
        }
        

    }

    protected void ddlWarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        filluser();
        Fillcomplaint();
        ddlcustomercomplanit_SelectedIndexChanged(sender, e);
    }

    protected void ddlProbType_SelectedIndexChanged(object sender, EventArgs e)
    {
        Fillcomplaint();
        ddlcustomercomplanit_SelectedIndexChanged(sender, e);
        

    }
    protected void ddlUserMaster_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Fillcomplaint()
    {
        //SqlDataAdapter da1 = new SqlDataAdapter("select CustomerServiceCallMaster.CustomerServiceCallMasterId,cast(CustomerServiceCallMaster.CustomerServiceCallMasterId as nvarchar)+' : '+User_Master.Name as Pname from CustomerServiceCallMaster inner join user_master on CustomerServiceCallMaster.[CustomerId]=user_master.[UserID] inner join party_master on [User_master].[PartyID]=party_master.[PartyID] where party_master.Whid='" + ddlWarehouse.SelectedValue + "' and CustomerServiceCallMaster.ServiceStatusId='" + ddlMainStatus.SelectedValue + "'", con);


        string str = "select CustomerServiceCallMaster.CustomerServiceCallMasterId,cast(CustomerServiceCallMaster.CustomerServiceCallMasterId as nvarchar)+':'+ Convert(nvarchar,CustomerServiceCallMaster.Entrydate,101)+' : '+Left(party_master.Contactperson,10)+':'+ Left(CustomerServiceCallMaster.ProblemTitle,30) as  Pname  from CustomerServiceCallMaster inner join user_master on CustomerServiceCallMaster.[CustomerId]=user_master.[UserID] inner join party_master on [User_master].[PartyID]=party_master.[PartyID] where party_master.Whid='" + ddlWarehouse.SelectedValue + "' and CustomerServiceCallMaster.ServiceStatusId='" + ddlcomplaintstatus.SelectedValue + "'  ";
        string str1="";

        if (ddlProbType.SelectedIndex > 0)
        {
            str1 = "and CustomerServiceCallMaster.ProblemTypeId='" + ddlProbType.SelectedValue + "' ";
        }
        string finalstr = str + str1;


        SqlCommand cmd = new SqlCommand(finalstr, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);

        ddlcustomercomplanit.DataSource = dt;
        ddlcustomercomplanit.DataTextField = "Pname";
        ddlcustomercomplanit.DataValueField = "CustomerServiceCallMasterId";
        ddlcustomercomplanit.DataBind();


    }
    public void FillMainStatus()
    {
        string str = "SELECT     StatusId, StatusName " +
                    " FROM         ServiceStatusMaster";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);

        ddlcomplaintstatus.DataSource = dt;
        ddlcomplaintstatus.DataTextField = "StatusName";
        ddlcomplaintstatus.DataValueField = "StatusId";
        ddlcomplaintstatus.DataBind();


    }
    protected void ddlcomplaintstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        Fillcomplaint();
        ddlcustomercomplanit_SelectedIndexChanged(sender, e);
    }
    protected void ddlcustomercomplanit_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcustomercomplanit.SelectedIndex > -1)
        {

            SqlCommand cmd = new SqlCommand("select CustomerServiceCallMaster.CustomerServiceCallMasterId,Party_master.Contactperson +' : '+Party_master.Compname as Name ,CustomerServiceCallMaster.[Entrydate],CustomerServiceCallMaster.[ProblemTitle],CustomerServiceCallMaster.[ProblemDescription],case when (CustomerServiceCallMaster.ServiceStatusId = 1) then 'Active' else 'Inactive' end as status from CustomerServiceCallMaster inner join user_master on CustomerServiceCallMaster.[CustomerId]=user_master.[UserID] inner join party_master on [User_master].[PartyID]=party_master.[PartyID] where CustomerServiceCallMaster.CustomerServiceCallMasterId='" + ddlcustomercomplanit.SelectedValue + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            da.Fill(dt1);


            lblprintcomplaintdate.Text = Convert.ToDateTime(dt1.Rows[0]["Entrydate"].ToString()).ToShortDateString();
            lblprintpartyname.Text = dt1.Rows[0]["Name"].ToString();
            lblprintcomplainttitle.Text = dt1.Rows[0]["ProblemTitle"].ToString();
            lblcomplaintdescription.Text = dt1.Rows[0]["ProblemDescription"].ToString();



        }
        else
        {
            lblprintcomplaintdate.Text = "";
            lblprintpartyname.Text = "";
            lblprintcomplainttitle.Text = "";
            lblcomplaintdescription.Text = "";

        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (ddlcustomercomplanit.SelectedIndex == -1)
        {
            lblmsg.Text = "Please select complaint title";
        }
        else
        {

            string str = " insert into ServiceCallDetailTbl (ServiceCallId,AssignedUserId,ServiceProvidedDate,ServiceDoneNote) values ('" + ddlcustomercomplanit.SelectedValue + "','" + ddlUserMaster.SelectedValue + "','" + Label2.Text + "','" + TextBox1.Text + "')";

            SqlCommand cm = new SqlCommand(str, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cm.ExecuteNonQuery();
            con.Close();

            if (CheckBox1.Checked == true)
            {

                string strupdate = " Update CustomerServiceCallMaster set ServiceStatusId='" + 2 + "' where CustomerServiceCallMasterId='" + ddlcustomercomplanit.SelectedValue + "' ";

                SqlCommand cmdupdate = new SqlCommand(strupdate, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdupdate.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                string strupdate = " Update CustomerServiceCallMaster set ServiceStatusId='" + 1 + "' where CustomerServiceCallMasterId='" + ddlcustomercomplanit.SelectedValue + "' ";

                SqlCommand cmdupdate = new SqlCommand(strupdate, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdupdate.ExecuteNonQuery();
                con.Close();
            }

            lblmsg.Text = "Service note inserted successfully";

            TextBox1.Text = "";
            CheckBox1.Checked = false;
            Fillcomplaint();
            ddlcustomercomplanit_SelectedIndexChanged(sender, e);

        }
       
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        TextBox1.Text = "";
        CheckBox1.Checked = false;
        lblmsg.Text = "";
    }
}
