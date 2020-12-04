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

public partial class Add_Employee_Payroll_Master : System.Web.UI.Page
{
    SqlConnection con;

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

        Page.Title = pg.getPageTitle(page);

        Label1.Visible = false;
        if (!Page.IsPostBack)
        {
            Pagecontrol.dypcontrol(Page, page);
            lblCompany.Text = Session["Cname"].ToString();
            ViewState["sortOrder"] = "";
            fillstore();
            fillemployee();
            statusselect();
            fillpaymentcycle();
            fillpaymentMethod();
            btnupdate.Visible = false;
            btnsubmit.Visible = true;
            filteremp();
            filldata();
            
        }
    }

    protected void fillstore()
    {
        ////string str1 = " select Id,AccountName,AccountId from AccountMaster where  AccountMaster.Status=1 and  ClassId=13 and AccountMaster.compid = '" + Session["Comid"].ToString() + "' and AccountMaster.Whid='" + ddlstrname.SelectedValue + "'  order by AccountName asc";
        //string str = "select WareHouseId,Name from WareHouseMaster WHERE comid='" + Session["Comid"].ToString() + "' and Status='1' order by Name";
        //SqlCommand cmd = new SqlCommand(str, con);
        //SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //DataSet ds = new DataSet();
        //adp.Fill(ds);

        //ddlStore.DataSource = ds;
        //ddlStore.DataTextField = "Name";
        //ddlStore.DataValueField = "WareHouseId";
        //ddlStore.DataBind();
        //ddlStore.Items.Insert(0, "--Select--");
        //ddlStore.Items[0].Value = "0";

        ddlStore.Items.Clear();
        
        DataTable ds = ClsStore.SelectStorename();
        ddlStore.DataSource = ds;
        ddlStore.DataTextField = "Name";
        ddlStore.DataValueField = "WareHouseId";
        ddlStore.DataBind();



        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlStore.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }

        ddlfilterbusiness.Items.Clear();
        ddlfilterbusiness.DataSource = ds;
        ddlfilterbusiness.DataTextField = "Name";
        ddlfilterbusiness.DataValueField = "WareHouseId";
        ddlfilterbusiness.DataBind();
        ddlfilterbusiness.Items.Insert(0, "All");
        ddlfilterbusiness.Items[0].Value = "0";
        //if (dteeed.Rows.Count > 0)
        //{
        //    ddlfilterbusiness.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        //}
    }
    protected void fillemployee()
    {
        string str = "  select [EmployeeMaster].[EmployeeMasterID],[EmployeeMaster].[EmployeeName] +':'+DesignationMaster.DesignationName+':'+Cast([EmployeeMaster].EmployeeMasterID as nvarchar(50)) as [EmployeeName]  from [EmployeeMaster] inner join DesignationMaster on DesignationMaster.DesignationMasterId=[EmployeeMaster].DesignationMasterId WHERE EmployeeMaster.Whid='" + ddlStore.SelectedValue + "' and EmployeeMaster.Active='1' order by EmployeeName";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlemployee.DataSource = ds;
            ddlemployee.DataTextField = "EmployeeName";
            ddlemployee.DataValueField = "EmployeeMasterID";
            ddlemployee.DataBind();
        }
        else
        {
            ddlemployee.Items.Insert(0, "-Select-");
            ddlemployee.Items[0].Value = "0";
        }
    }
    protected void filteremp()
    {
        ddlfilteremployee.Items.Clear();
        string str = "  select [EmployeeMaster].[EmployeeMasterID],[EmployeeMaster].[EmployeeName] +':'+DesignationMaster.DesignationName+':'+Cast([EmployeeMaster].EmployeeMasterID as nvarchar(50)) as [EmployeeName]  from [EmployeeMaster] inner join DesignationMaster on DesignationMaster.DesignationMasterId=[EmployeeMaster].DesignationMasterId WHERE EmployeeMaster.Whid='" + ddlfilterbusiness.SelectedValue + "' and EmployeeMaster.Active='1' order by EmployeeName";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlfilteremployee.DataSource = ds;
            ddlfilteremployee.DataTextField = "EmployeeName";
            ddlfilteremployee.DataValueField = "EmployeeMasterID";
            ddlfilteremployee.DataBind();
            ddlfilteremployee.Items.Insert(0, "All");
            ddlfilteremployee.Items[0].Value = "0";
        }
        else
        {
            ddlfilteremployee.Items.Insert(0, "All");
            ddlfilteremployee.Items[0].Value = "0";
        }
    }
    protected void fillpaymentMethod()
    {
        //string str1 = " select Id,AccountName,AccountId from AccountMaster where  AccountMaster.Status=1 and  ClassId=13 and AccountMaster.compid = '" + Session["Comid"].ToString() + "' and AccountMaster.Whid='" + ddlstrname.SelectedValue + "'  order by AccountName asc";
        string str = "Select * from PaymentMethod order by PaymentMethodName";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);

        ddlPaymentMethod.DataSource = ds;
        ddlPaymentMethod.DataTextField = "PaymentMethodName";
        ddlPaymentMethod.DataValueField = "PaymentMethodId";
        ddlPaymentMethod.DataBind();
        ddlPaymentMethod.Items.Insert(0, "-Select-");
        ddlPaymentMethod.Items[0].Value = "0";

    }


    protected void fillpaymentcycle()
    {
        //string str1 = " select Id,AccountName,AccountId from AccountMaster where  AccountMaster.Status=1 and  ClassId=13 and AccountMaster.compid = '" + Session["Comid"].ToString() + "' and AccountMaster.Whid='" + ddlstrname.SelectedValue + "'  order by AccountName asc";
        string str = "Select * from Payperiodtype where active='true' order by Name";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);

        ddlPaymentCycle.DataSource = ds;
        ddlPaymentCycle.DataTextField = "Name";
        ddlPaymentCycle.DataValueField = "ID";
        ddlPaymentCycle.DataBind();
        ddlPaymentCycle.Items.Insert(0, "-Select-");
        ddlPaymentCycle.Items[0].Value = "0";

    }
    protected void ddlfilterbusiness_SelectedIndexChanged(object sender, EventArgs e)
    {
        filteremp();
        filldata();
    }
    protected void ddlfilteremployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        filldata();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        ddpnl.Visible = false;
       
        cleartext();
        filldata();
        Label1.Text = "";
        btnsubmit.Visible = true;
        btnupdate.Visible = false;
        pnlparty.Visible = false;
        btnshowparty.Visible = true;
        btnupdate.Enabled = true;
        ddlStore.Enabled = true;
        ddlemployee.Enabled = true;
        ddlstatus.Enabled = true;
        RadioButtonList1.Enabled = true;
        txtlastname.Enabled = true;
        txtfirstname.Enabled = true;
        txtintialis.Enabled = true;
        txtemployeeno.Enabled = true;
        txtdateofbirth.Enabled = true;
        txtsecurityno.Enabled = true;
        ddlPaymentMethod.Enabled = true;
        ddlPaymentCycle.Enabled = true;
        txtPaymentReceivedNameOf.Enabled = true;
        txtPaypalId.Enabled = true;
        txtPaymentEmailId.Enabled = true;
        txtDirectDepositBankName.Enabled = true;
        txtDirectDepositBankCode.Enabled = true;
        txtDirectDepositBankBranchName.Enabled = true;
        txtDirectDepositBankBranchNumber.Enabled = true;
        txtDirectDepositTransitNumber.Enabled = true;
        txtDirectDepositAccountHolderName.Enabled = true;
        ddlDirectDepositBankAccountType.Enabled = true;
        txtDirectDepositBankAccountNumber.Enabled = true;
        ImageButton50.Visible = true;
        ImageButton51.Visible = true;
        clearall();
        lbladd.Text = "";
    }

    public void filldata()
    {
        string sg90 = " Select EmployeePayrollMaster.* ,payperiodtype.Name as PaymentCycle ," +
                  " PaymentMethod.PaymentMethodName,PaymentMethod.PaymentMethodId,[EmployeeMaster].[EmployeeMasterID],[EmployeeMaster].[EmployeeName]  as [EmployeeName],[WareHouseMaster].[Name] as StoreName from EmployeePayrollMaster inner  Join [EmployeeMaster] on " +
            //" PaymentMethod.PaymentMethodName,PaymentMethod.PaymentMethodId,[EmployeeMaster].[EmployeeMasterID],[EmployeeMaster].[EmployeeName] +':'+DesignationMaster.DesignationName+':'+Cast([EmployeeMaster].EmployeeMasterID as nvarchar(50)) as [EmployeeName],[WareHouseMaster].[Name] as StoreName from EmployeePayrollMaster inner  Join [EmployeeMaster] on " +
                  " [EmployeeMaster].[EmployeeMasterID]= EmployeePayrollMaster.EmpId inner join DesignationMaster on DesignationMaster.DesignationMasterId=[EmployeeMaster].DesignationMasterId inner join WareHouseMaster  on [WareHouseMaster].[WareHouseId] = EmployeePayrollMaster.Whid inner join PaymentMethod on PaymentMethod.PaymentMethodId= EmployeePayrollMaster.PaymentMethodId inner  join payperiodtype on payperiodtype.ID=EmployeePayrollMaster.PayPeriodMasterId where [WareHouseMaster].comid='" + Session["comid"] + "' ";
        if (ddlfilterbusiness.SelectedIndex > 0)
        {
            sg90 += "and EmployeePayrollMaster.Whid='" + ddlfilterbusiness.SelectedValue + "'";
        }
        if (ddlfilteremployee.SelectedIndex > 0)
        {
            sg90 += "and EmployeePayrollMaster.EmpId='" + ddlfilteremployee.SelectedValue + "'";
        }
        lblbusiness.Text = ddlfilterbusiness.SelectedItem.Text;
        lblfilteremp.Text = ddlfilteremployee.SelectedItem.Text;
        sg90 += " order by StoreName, EmployeeName, EmployeeNo";
        SqlCommand cmd23490 = new SqlCommand(sg90, con);
        SqlDataAdapter adp23490 = new SqlDataAdapter(cmd23490);
        DataTable dt23490 = new DataTable();

        adp23490.Fill(dt23490);
        //GridView1.DataSource = dt23490;

        DataView myDataView = new DataView();
        myDataView = dt23490.DefaultView;
        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }
        GridView1.DataSource = myDataView;
        GridView1.DataBind();


        foreach (GridViewRow gdr in GridView1.Rows)
        {
            Label lblemppaidasperdesignation = (Label)gdr.FindControl("lblemppaidasperdesignation");
            RadioButtonList RadioButtonList123 = (RadioButtonList)gdr.FindControl("RadioButtonList123");




            if (Convert.ToInt32(lblemppaidasperdesignation.Text.ToString()) == 0)
            {
                RadioButtonList123.SelectedValue = "0";

            }
            if (Convert.ToInt32(lblemppaidasperdesignation.Text.ToString()) == 1)
            {
                RadioButtonList123.SelectedValue = "1";

            }

        }


    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string sg90 = "Select  EmployeePayrollMaster.*  from EmployeePayrollMaster  where EmployeePayrollMaster.EmpId='" + ddlemployee.SelectedValue + "' and PayPeriodMasterId='" + ddlPaymentCycle.SelectedValue + "' and EmployeePayrollMaster.Whid ='" + ddlStore.SelectedValue + "'";
        SqlCommand cmd23490 = new SqlCommand(sg90, con);
        SqlDataAdapter adp23490 = new SqlDataAdapter(cmd23490);
        DataTable dt23490 = new DataTable();
        adp23490.Fill(dt23490);
        if (dt23490.Rows.Count > 0)
        {
            Label1.Visible = true;
            Label1.Text = "Record already exists";

        }
        else
        {
            if (ddlPaymentMethod.SelectedItem.Text == "Demand Draft" || ddlPaymentMethod.SelectedItem.Text == "Cheque")
            {
                string str = "Insert Into EmployeePayrollMaster(EmpId,EmployeePaidAsPerDesignation,PaymentMethodId,PaymentReceivedNameOf,Whid,Compid,PayPeriodMasterId,LastName,FirstName,Intials,EmployeeNo,DateOfBirth,SocialSecurityNo)values('" + ddlemployee.SelectedValue + "','" + RadioButtonList1.SelectedValue + "','" + ddlPaymentMethod.SelectedValue + "','" + txtPaymentReceivedNameOf.Text + "','" + ddlStore.SelectedValue + "','" + Session["comid"] + "','" + ddlPaymentCycle.SelectedValue + "','" + txtlastname.Text + "','" + txtfirstname.Text + "','" + txtintialis.Text + "','" + txtemployeeno.Text + "','" + txtdateofbirth.Text + "','" + txtsecurityno.Text + "')";
                SqlCommand cmd = new SqlCommand(str, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                con.Close();
                Label1.Visible = true;
                Label1.Text = "Record inserted successfully";
                filldata();

                ddlemployee.SelectedIndex = 0;
                // RadioButtonList1.SelectedValue.Checked=false;
                txtPaymentReceivedNameOf.Text = "";
                ddlStore.SelectedIndex = 0;
                ddlPaymentMethod.SelectedIndex = 0;
                ddlPaymentCycle.SelectedIndex = 0;

            }
            else if (ddlPaymentMethod.SelectedItem.Text == "Paypal")
            {
                string str = "Insert Into EmployeePayrollMaster(EmpId,EmployeePaidAsPerDesignation,PaymentMethodId,PaypalId,Whid,Compid,PayPeriodMasterId,LastName,FirstName,Intials,EmployeeNo,DateOfBirth,SocialSecurityNo)values('" + ddlemployee.SelectedValue + "','" + RadioButtonList1.SelectedValue + "','" + ddlPaymentMethod.SelectedValue + "','" + txtPaypalId.Text + "','" + ddlStore.SelectedValue + "','" + Session["comid"] + "','" + ddlPaymentCycle.SelectedValue + "','" + txtlastname.Text + "','" + txtfirstname.Text + "','" + txtintialis.Text + "','" + txtemployeeno.Text + "','" + txtdateofbirth.Text + "','" + txtsecurityno.Text + "')";
                SqlCommand cmd = new SqlCommand(str, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                con.Close();
                Label1.Visible = true;
                Label1.Text = "Record inserted successfully";
                filldata();

                ddlemployee.SelectedIndex = 0;
                //RadioButtonList1.SelectedValue.Checked = false;
                txtPaypalId.Text = "";
                ddlStore.SelectedIndex = 0;
                ddlPaymentMethod.SelectedIndex = 0;
            }
            else if (ddlPaymentMethod.SelectedItem.Text == "By Email")
            {
                string str = "Insert Into EmployeePayrollMaster(EmpId,EmployeePaidAsPerDesignation,PaymentMethodId,PaymentEmailId,Whid,Compid,PayPeriodMasterId,LastName,FirstName,Intials,EmployeeNo,DateOfBirth,SocialSecurityNo)values('" + ddlemployee.SelectedValue + "','" + RadioButtonList1.SelectedValue + "','" + ddlPaymentMethod.SelectedValue + "','" + txtPaymentEmailId.Text + "','" + ddlStore.SelectedValue + "','" + Session["comid"] + "','" + ddlPaymentCycle.SelectedValue + "','" + txtlastname.Text + "','" + txtfirstname.Text + "','" + txtintialis.Text + "','" + txtemployeeno.Text + "','" + txtdateofbirth.Text + "','" + txtsecurityno.Text + "')";
                SqlCommand cmd = new SqlCommand(str, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                con.Close();
                Label1.Visible = true;
                Label1.Text = "Record inserted successfully";
                filldata();

                ddlemployee.SelectedIndex = 0;
                // RadioButtonList1.SelectedValue.Checked = false;
                txtPaymentEmailId.Text = "";
                ddlStore.SelectedIndex = 0;
                ddlPaymentMethod.SelectedIndex = 0;
            }

            else if (ddlPaymentMethod.SelectedItem.Text == "Direct Deposit")
            {
                string str = "Insert Into EmployeePayrollMaster(EmpId,EmployeePaidAsPerDesignation,PaymentMethodId,DirectDepositBankName,DirectDepositBankCode,DirectDepositBankBranchName,DirectDepositBankBranchNumber,DirectDepositTransitNumber,DirectDepositAccountHolderName,DirectDepositBankAccountType,DirectDepositBankAccountNumber,Whid,Compid,PayPeriodMasterId,LastName,FirstName,Intials,EmployeeNo,DateOfBirth,SocialSecurityNo)values('" + ddlemployee.SelectedValue + "','" + RadioButtonList1.SelectedValue + "','" + ddlPaymentMethod.SelectedValue + "','" + txtDirectDepositBankName.Text + "','" + txtDirectDepositBankCode.Text + "','" + txtDirectDepositBankBranchName.Text + "','" + txtDirectDepositBankBranchNumber.Text + "','" + txtDirectDepositTransitNumber.Text + "','" + txtDirectDepositAccountHolderName.Text + "','" + ddlDirectDepositBankAccountType.SelectedValue + "','" + txtDirectDepositBankAccountNumber.Text + "','" + ddlStore.SelectedValue + "','" + Session["comid"] + "','" + ddlPaymentCycle.SelectedValue + "','" + txtlastname.Text + "','" + txtfirstname.Text + "','" + txtintialis.Text + "','" + txtemployeeno.Text + "','" + txtdateofbirth.Text + "','" + txtsecurityno.Text + "')";
                SqlCommand cmd = new SqlCommand(str, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                con.Close();
                Label1.Visible = true;
                Label1.Text = "Record inserted successfully";
                filldata();

                ddlemployee.SelectedIndex = 0;
                // RadioButtonList1.SelectedValue.Checked = false;
                txtDirectDepositAccountHolderName.Text = "";
                txtDirectDepositBankAccountNumber.Text = "";


                txtDirectDepositBankBranchNumber.Text = "";
                txtDirectDepositBankBranchName.Text = "";
                txtDirectDepositBankCode.Text = "";
                txtDirectDepositBankName.Text = "";

                txtDirectDepositTransitNumber.Text = "";

                ddlDirectDepositBankAccountType.SelectedIndex = 0;
                ddlStore.SelectedIndex = 0;
                ddlPaymentMethod.SelectedIndex = 0;
            }
            else if (ddlPaymentMethod.SelectedItem.Text == "Cash")
            {
                string str = "Insert Into EmployeePayrollMaster(EmpId,EmployeePaidAsPerDesignation,PaymentMethodId,Whid,Compid,PayPeriodMasterId,LastName,FirstName,Intials,EmployeeNo,DateOfBirth,SocialSecurityNo)values('" + ddlemployee.SelectedValue + "','" + RadioButtonList1.SelectedValue + "','" + ddlPaymentMethod.SelectedValue + "','" + ddlStore.SelectedValue + "','" + Session["comid"] + "','" + ddlPaymentCycle.SelectedValue + "','" + txtlastname.Text + "','" + txtfirstname.Text + "','" + txtintialis.Text + "','" + txtemployeeno.Text + "','" + txtdateofbirth.Text + "','" + txtsecurityno.Text + "')";
                SqlCommand cmd = new SqlCommand(str, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                con.Close();
                Label1.Visible = true;
                Label1.Text = "Record inserted successfully";
                filldata();

                ddlemployee.SelectedIndex = 0;
                //RadioButtonList1.SelectedValue.Checked = false;

                ddlStore.SelectedIndex = 0;
                ddlPaymentMethod.SelectedIndex = 0;
            }

            string empupdate = " update EmployeeMaster set Active='" + ddlstatus.SelectedValue + "' ";
            SqlCommand cmdempup = new SqlCommand(empupdate, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdempup.ExecuteNonQuery();
            con.Close();
            lbladd.Text = "";

            pnlparty.Visible = false;
            btnshowparty.Visible = true;
            clearall();

        }
    }
    public void cleartext()
    {
        ddlDirectDepositBankAccountType.SelectedIndex = 0;
        txtPaymentReceivedNameOf.Text = "";
        txtPaypalId.Text = "";
        txtPaymentEmailId.Text = "";
        ddlemployee.SelectedIndex = 0;
        //  RadioButtonList1.SelectedValue.Checked = false;
        txtDirectDepositAccountHolderName.Text = "";
        txtDirectDepositBankAccountNumber.Text = "";


        txtDirectDepositBankBranchNumber.Text = "";
        txtDirectDepositBankBranchName.Text = "";
        txtDirectDepositBankCode.Text = "";
        txtDirectDepositBankName.Text = "";

        txtDirectDepositTransitNumber.Text = "";

        ddlDirectDepositBankAccountType.SelectedIndex = 0;
        ddlStore.SelectedIndex = 0;
        ddlPaymentMethod.SelectedIndex = 0;

    }


    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        filldata();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Sort")
        {
            return;

        }
        if (e.CommandName == "Edit")
        {
            pnlparty.Visible = true;
            btnshowparty.Visible = false;
            lbladd.Text = "Edit Employee Payroll";
            Label1.Text = "";
            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            ViewState["Id"] = GridView1.SelectedIndex;


            ddlStore.Enabled = true;
            ddlemployee.Enabled = true;
            ddlstatus.Enabled = true;
            RadioButtonList1.Enabled = true;
            txtlastname.Enabled = true;
            txtfirstname.Enabled = true;
            txtintialis.Enabled = true;
            txtemployeeno.Enabled = true;
            txtdateofbirth.Enabled = true;
            txtsecurityno.Enabled = true;
            ddlPaymentMethod.Enabled = true;
            ddlPaymentCycle.Enabled = true;
            txtPaymentReceivedNameOf.Enabled = true;
            txtPaypalId.Enabled = true;
            txtPaymentEmailId.Enabled = true;
            txtDirectDepositBankName.Enabled = true;
            txtDirectDepositBankCode.Enabled = true;
            txtDirectDepositBankBranchName.Enabled = true;
            txtDirectDepositBankBranchNumber.Enabled = true;
            txtDirectDepositTransitNumber.Enabled = true;
            txtDirectDepositAccountHolderName.Enabled = true;
            ddlDirectDepositBankAccountType.Enabled = true;
            txtDirectDepositBankAccountNumber.Enabled = true;

            btnsubmit.Visible = false;
            btnupdate.Visible = true;


            edit();


        }
        else if (e.CommandName == "View")
        {
            pnlparty.Visible = true;
            btnshowparty.Visible = false;
            lbladd.Text = "View Employee Payroll";
            Label1.Text = "";
            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            ViewState["Id"] = GridView1.SelectedIndex;



            ddlStore.Enabled = false;
            ddlemployee.Enabled = false;
            ddlstatus.Enabled = false;
            RadioButtonList1.Enabled = false;
            txtlastname.Enabled = false;
            txtfirstname.Enabled = false;
            txtintialis.Enabled = false;
            txtemployeeno.Enabled = false;
            txtdateofbirth.Enabled = false;
            txtsecurityno.Enabled = false;
            ddlPaymentMethod.Enabled = false;
            ddlPaymentCycle.Enabled = false;
            txtPaymentReceivedNameOf.Enabled = false;
            txtPaypalId.Enabled = false;
            txtPaymentEmailId.Enabled = false;
            txtDirectDepositBankName.Enabled = false;
            txtDirectDepositBankCode.Enabled = false;
            txtDirectDepositBankBranchName.Enabled = false;
            txtDirectDepositBankBranchNumber.Enabled = false;
            txtDirectDepositTransitNumber.Enabled = false;
            txtDirectDepositAccountHolderName.Enabled = false;
            ddlDirectDepositBankAccountType.Enabled = false;
            txtDirectDepositBankAccountNumber.Enabled = false;
            ImageButton50.Visible = false;
            ImageButton51.Visible = false;
            btnsubmit.Visible = false;
            btnupdate.Visible = false;

            edit();


        }



        //else if (e.CommandName == "Delete")
        //{
            //Label1.Text = "";
            //GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            //ViewState["DId"] = GridView1.SelectedDataKey.Value;

        //}
    }




    protected void edit()
    {



        string str1 = " Select Payperiodtype.ID,EmployeePayrollMaster.* , " +
                  " PaymentMethod.PaymentMethodName,PaymentMethod.PaymentMethodId,[EmployeeMaster].[EmployeeName],[WareHouseMaster].[Name] as StoreName , [EmployeeMaster].[EmployeeMasterID],[EmployeeMaster].[EmployeeName] +':'+DesignationMaster.DesignationName+':'+Cast([EmployeeMaster].EmployeeMasterID as nvarchar(50)) as [EmployeeName] from EmployeePayrollMaster inner  Join [EmployeeMaster] on " +
                  " [EmployeeMaster].[EmployeeMasterID]= EmployeePayrollMaster.EmpId inner join DesignationMaster on DesignationMaster.DesignationMasterId=[EmployeeMaster].DesignationMasterId inner join WareHouseMaster  on [WareHouseMaster].[WareHouseId] = EmployeePayrollMaster.Whid inner join PaymentMethod on PaymentMethod.PaymentMethodId= EmployeePayrollMaster.PaymentMethodId inner  join Payperiodtype on Payperiodtype.ID=EmployeePayrollMaster.PayPeriodMasterId where [WareHouseMaster].comid='" + Session["comid"] + "' and EmployeePayrollMaster.EmployeePayrollMaster_Id='" + ViewState["Id"] + "'";

        SqlCommand cmd = new SqlCommand(str1, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        adp.Fill(dt);
        fillpaymentcycle();
        fillstore();

        ddlStore.SelectedIndex = Convert.ToInt32(ddlStore.Items.IndexOf(ddlStore.Items.FindByValue(dt.Rows[0]["Whid"].ToString())));

        if (ddlStore.SelectedIndex > 0)
        {
            ddlemployee.Items.Clear();
            string str22 = "  select [EmployeeMaster].[EmployeeMasterID],[EmployeeMaster].[EmployeeName] +':'+DesignationMaster.DesignationName+':'+Cast([EmployeeMaster].EmployeeMasterID as nvarchar(50)) as [EmployeeName]  from [EmployeeMaster] inner join DesignationMaster on DesignationMaster.DesignationMasterId=[EmployeeMaster].DesignationMasterId WHERE [Whid]='" + ddlStore.SelectedValue + "'  order by EmployeeName";
            SqlCommand cmd22 = new SqlCommand(str22, con);
            SqlDataAdapter adp22 = new SqlDataAdapter(cmd22);
            DataSet ds22 = new DataSet();
            adp22.Fill(ds22);

            ddlemployee.DataSource = ds22;
            ddlemployee.DataTextField = "EmployeeName";
            ddlemployee.DataValueField = "EmployeeMasterID";
            ddlemployee.DataBind();
            ddlemployee.Items.Insert(0, "-Select-");
            ddlemployee.Items[0].Value = "0";
        }
        else
        {
            //string str = "  select [EmployeeMaster].[EmployeeMasterID],[EmployeeMaster].[EmployeeName] +':'+DesignationMaster.DesignationName+':'+Cast([EmployeeMaster].EmployeeMasterID as nvarchar(50)) as [EmployeeName]  from [EmployeeMaster] inner join DesignationMaster on DesignationMaster.DesignationMasterId=[EmployeeMaster].DesignationMasterId WHERE [Whid]='" + dt.Rows[0]["Whid"] + "'  order by EmployeeName";
            //SqlCommand cmd22 = new SqlCommand(str, con);
            //SqlDataAdapter adp22 = new SqlDataAdapter(cmd);
            //DataSet ds22 = new DataSet();
            //adp.Fill(ds22);

            ddlemployee.DataSource = null;
            ddlemployee.DataTextField = "EmployeeName";
            ddlemployee.DataValueField = "EmployeeMasterID";
            ddlemployee.DataBind();
            ddlemployee.Items.Insert(0, "-Select-");
            ddlemployee.Items[0].Value = "0";
        }

        ddlStore.SelectedValue = dt.Rows[0]["Whid"].ToString();
        ddlemployee.SelectedIndex = ddlemployee.Items.IndexOf(ddlemployee.Items.FindByValue(dt.Rows[0]["EmpId"].ToString()));

        txtlastname.Text = dt.Rows[0]["LastName"].ToString();
        txtfirstname.Text = dt.Rows[0]["FirstName"].ToString();
        txtintialis.Text = dt.Rows[0]["Intials"].ToString();
        txtemployeeno.Text = dt.Rows[0]["EmployeeNo"].ToString();

        DateTime t1 = Convert.ToDateTime(dt.Rows[0]["DateOfBirth"].ToString());
        txtdateofbirth.Text = t1.ToShortDateString();
        txtsecurityno.Text = dt.Rows[0]["SocialSecurityNo"].ToString();

        ddlPaymentCycle.SelectedValue = dt.Rows[0]["ID"].ToString();
        RadioButtonList1.SelectedValue = dt.Rows[0]["EmployeePaidAsPerDesignation"].ToString();
        RadioButtonList1.SelectedValue = dt.Rows[0]["EmployeePaidAsPerDesignation"].ToString();
        ddlPaymentMethod.SelectedValue = dt.Rows[0]["PaymentMethodId"].ToString();

        //txtStartDate.Text = dt.Rows[0]["EffectiveStartDate"].ToString();
        //txtEndDate.Text = dt.Rows[0]["EffectiveEndDate"].ToString();
        //txtNotes.Text = dt.Rows[0]["Notes"].ToString();
        //txttaxDetailName.Text = dt.Rows[0]["TaxDetailName"].ToString();


        if (ddlPaymentMethod.SelectedIndex > 0)
        {
            if (ddlPaymentMethod.SelectedItem.Text == "Demand Draft" || ddlPaymentMethod.SelectedItem.Text == "Cheque")
            {
                ddpnl.Visible = false;
                pnlreceivepayment.Visible = true;
              //  lblPaymentReceivedNameOf.Visible = true;
               // txtPaymentReceivedNameOf.Visible = true;
                
               
                txtPaymentReceivedNameOf.Text = dt.Rows[0]["PaymentReceivedNameOf"].ToString();
            }
            else
            {
                ddpnl.Visible = false;
                pnlreceivepayment.Visible = false;
                //lblPaymentReceivedNameOf.Visible = false;
               // txtPaymentReceivedNameOf.Visible = false;
            }

            if (ddlPaymentMethod.SelectedItem.Text == "Paypal")
            {
                ddpnl.Visible = false;
                pnlpaypalid.Visible = true;
                //lblPaypalId.Visible = true;
                //txtPaypalId.Visible = true;
                txtPaypalId.Text = dt.Rows[0]["PaypalId"].ToString();
            }
            else
            {
                ddpnl.Visible = false;
                pnlpaypalid.Visible = false;
               // lblPaypalId.Visible = false;
               // txtPaypalId.Visible = false;
            }

            if (ddlPaymentMethod.SelectedItem.Text == "By Email")
            {
                ddpnl.Visible = false;
                pnlpaymentemail.Visible = true;
                //lblPaymentEmailId.Visible = true;
                //txtPaymentEmailId.Visible = true;
                txtPaymentEmailId.Text = dt.Rows[0]["PaymentEmailId"].ToString();
            }
            else
            {
                ddpnl.Visible = false;
                pnlpaymentemail.Visible = false;
                //lblPaymentEmailId.Visible = false;
                //txtPaymentEmailId.Visible = false;
            }

            if (ddlPaymentMethod.SelectedItem.Text == "Direct Deposit")
            {
                ddpnl.Visible = true;
                //lblDirectDepositAccountHolderName.Visible = true;
               // txtDirectDepositAccountHolderName.Visible = true;

                txtDirectDepositAccountHolderName.Text = dt.Rows[0]["DirectDepositAccountHolderName"].ToString();

                //lblDirectDepositBankAccountNumber.Visible = true;
                //txtDirectDepositBankAccountNumber.Visible = true;

                txtDirectDepositBankAccountNumber.Text = dt.Rows[0]["DirectDepositBankAccountNumber"].ToString();

               // lblDirectDepositBankAccountType.Visible = true;
               // ddlDirectDepositBankAccountType.Visible = true;

                ddlDirectDepositBankAccountType.SelectedItem.Text = dt.Rows[0]["DirectDepositBankAccountType"].ToString();


               // lblDirectDepositBankBranchName.Visible = true;
               // txtDirectDepositBankBranchNumber.Visible = true;

                txtDirectDepositBankBranchName.Text = dt.Rows[0]["DirectDepositBankBranchName"].ToString();


               // lblDirectDepositBankCode.Visible = true;
               // txtDirectDepositBankCode.Visible = true;

                txtDirectDepositBankCode.Text = dt.Rows[0]["DirectDepositBankCode"].ToString();



               // lblDirectDepositBankName.Visible = true;
               // txtDirectDepositBankName.Visible = true;

                txtDirectDepositBankName.Text = dt.Rows[0]["DirectDepositBankName"].ToString();

               // lblDirectDepositBankBranchNumber.Visible = true;
               // txtDirectDepositBankBranchNumber.Visible = true;

                txtDirectDepositBankBranchNumber.Text = dt.Rows[0]["DirectDepositBankBranchNumber"].ToString();

               // lblDirectDepositTransitNumber.Visible = true;
               // txtDirectDepositTransitNumber.Visible = true;


                txtDirectDepositTransitNumber.Text = dt.Rows[0]["DirectDepositTransitNumber"].ToString();


            }
            else
            {
                ddpnl.Visible = false;
                //lblDirectDepositAccountHolderName.Visible = false;
                //txtDirectDepositAccountHolderName.Visible = false;

                //lblDirectDepositBankAccountNumber.Visible = false;
                //txtDirectDepositBankAccountNumber.Visible = false;

                //lblDirectDepositBankAccountType.Visible = false;
                //ddlDirectDepositBankAccountType.Visible = false;

                //lblDirectDepositBankBranchName.Visible = false;
                //txtDirectDepositBankBranchNumber.Visible = false;

                //lblDirectDepositBankCode.Visible = false;
                //txtDirectDepositBankCode.Visible = false;

                //lblDirectDepositBankName.Visible = false;
                //txtDirectDepositBankName.Visible = false;

                //lblDirectDepositBankBranchNumber.Visible = false;
                //txtDirectDepositBankBranchNumber.Visible = false;

                //lblDirectDepositTransitNumber.Visible = false;
                //txtDirectDepositTransitNumber.Visible = false;
            }
        }
        
        // btnupdate.Visible = true;
        //  btnsubmit.Visible = false;

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


    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string sg90 = "Select  EmployeePayrollMaster.*  from EmployeePayrollMaster  where EmployeePayrollMaster.EmpId='" + ddlemployee.SelectedValue + "' and PayPeriodMasterId='" + ddlPaymentCycle.SelectedValue + "'  and EmployeePayrollMaster.Whid ='" + ddlStore.SelectedValue + "' and EmployeePayrollMaster.EmployeePayrollMaster_Id<>'" + ViewState["Id"] + "' ";
        SqlCommand cmd23490 = new SqlCommand(sg90, con);
        SqlDataAdapter adp23490 = new SqlDataAdapter(cmd23490);
        DataTable dt23490 = new DataTable();
        adp23490.Fill(dt23490);
        if (dt23490.Rows.Count > 0)
        {
            Label1.Visible = true;
            Label1.Text = "Record already exists";

        }
        else
        {
            if (ddlPaymentMethod.SelectedItem.Text == "Demand Draft" || ddlPaymentMethod.SelectedItem.Text == "Cheque")
            {
                string str = "Update  EmployeePayrollMaster Set  EmpId='" + ddlemployee.SelectedValue + "',EmployeePaidAsPerDesignation='" + RadioButtonList1.SelectedValue + "',PaymentMethodId='" + ddlPaymentMethod.SelectedValue + "',PaymentReceivedNameOf='" + txtPaymentReceivedNameOf.Text + "',Whid='" + ddlStore.SelectedValue + "',PayPeriodMasterId='" + ddlPaymentCycle.SelectedValue + "',LastName='" + txtlastname.Text + "',FirstName='" + txtfirstname.Text + "',Intials='" + txtintialis.Text + "',EmployeeNo='" + txtemployeeno.Text + "',DateOfBirth='" + txtdateofbirth.Text + "',SocialSecurityNo='" + txtsecurityno.Text + "' where EmployeePayrollMaster_Id='" + ViewState["Id"] + "'";
                SqlCommand cmd = new SqlCommand(str, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                con.Close();
                Label1.Visible = true;
                Label1.Text = "Record updated successfully";
                filldata();

                ddlemployee.SelectedIndex = 0;
                // RadioButtonList1.SelectedValue.Checked = false;
                txtPaymentReceivedNameOf.Text = "";
                ddlStore.SelectedIndex = 0;
                ddlPaymentMethod.SelectedIndex = 0;
                ddlPaymentCycle.SelectedIndex = 0;
                btnsubmit.Visible = true;
                btnupdate.Visible = false;

            }
            else if (ddlPaymentMethod.SelectedItem.Text == "Paypal")
            {
                //string str = "Insert Into EmployeePayrollMaster(EmpId,EmployeePaidAsPerDesignation,PaymentMethodId,PaypalId,Whid,Compid)values('" + ddlemployee.SelectedValue + "','" + RadioButtonList1.SelectedValue.Checked + "','" + ddlPaymentMethod.SelectedValue + "','" + txtPaypalId.Text + "','" + ddlStore.SelectedValue + "','" + Session["comid"] + "')";
                string str = "Update  EmployeePayrollMaster Set  EmpId='" + ddlemployee.SelectedValue + "',EmployeePaidAsPerDesignation='" + RadioButtonList1.SelectedValue + "',PaymentMethodId='" + ddlPaymentMethod.SelectedValue + "',PaypalId='" + ddlPaymentMethod.SelectedValue + "',Whid='" + ddlStore.SelectedValue + "',PayPeriodMasterId='" + ddlPaymentCycle.SelectedValue + "',LastName='" + txtlastname.Text + "',FirstName='" + txtfirstname.Text + "',Intials='" + txtintialis.Text + "',EmployeeNo='" + txtemployeeno.Text + "',DateOfBirth='" + txtdateofbirth.Text + "',SocialSecurityNo='" + txtsecurityno.Text + "' where EmployeePayrollMaster_Id='" + ViewState["Id"] + "'";
                SqlCommand cmd = new SqlCommand(str, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                con.Close();
                Label1.Visible = true;
                Label1.Text = "Record updated successfully";
                filldata();

                ddlemployee.SelectedIndex = 0;
                // RadioButtonList1.SelectedValue = false;
                txtPaypalId.Text = "";
                ddlStore.SelectedIndex = 0;
                ddlPaymentMethod.SelectedIndex = 0;
                ddlPaymentCycle.SelectedIndex = 0;
                btnsubmit.Visible = true;
                btnupdate.Visible = false;
            }
            else if (ddlPaymentMethod.SelectedItem.Text == "By Email")
            {
                string str = "Update  EmployeePayrollMaster Set  EmpId='" + ddlemployee.SelectedValue + "',EmployeePaidAsPerDesignation='" + RadioButtonList1.SelectedValue + "',PaymentMethodId='" + ddlPaymentMethod.SelectedValue + "',PaymentEmailId='" + txtPaymentEmailId.Text + "',Whid='" + ddlStore.SelectedValue + "',PayPeriodMasterId='" + ddlPaymentCycle.SelectedValue + "',LastName='" + txtlastname.Text + "',FirstName='" + txtfirstname.Text + "',Intials='" + txtintialis.Text + "',EmployeeNo='" + txtemployeeno.Text + "',DateOfBirth='" + txtdateofbirth.Text + "',SocialSecurityNo='" + txtsecurityno.Text + "' where EmployeePayrollMaster_Id='" + ViewState["Id"] + "'";
                // string str = "Insert Into EmployeePayrollMaster(EmpId,EmployeePaidAsPerDesignation,PaymentMethodId,PaymentEmailId,Whid,Compid)values('" + ddlemployee.SelectedValue + "','" + RadioButtonList1.SelectedValue.Checked + "','" + ddlPaymentMethod.SelectedValue + "','" + txtPaymentEmailId.Text + "','" + ddlStore.SelectedValue + "','" + Session["comid"] + "')";
                SqlCommand cmd = new SqlCommand(str, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                con.Close();
                Label1.Visible = true;
                Label1.Text = "Record updated successfully";
                filldata();

                ddlemployee.SelectedIndex = 0;
                //  RadioButtonList1.SelectedValue.Checked = false;
                txtPaymentEmailId.Text = "";
                ddlStore.SelectedIndex = 0;
                ddlPaymentMethod.SelectedIndex = 0;
                ddlPaymentCycle.SelectedIndex = 0;
                btnsubmit.Visible = true;
                btnupdate.Visible = false;
            }

            else if (ddlPaymentMethod.SelectedItem.Text == "Direct Deposit")
            {
                string str = "Update  EmployeePayrollMaster Set  EmpId='" + ddlemployee.SelectedValue + "',EmployeePaidAsPerDesignation='" + RadioButtonList1.SelectedValue + "',PaymentMethodId='" + ddlPaymentMethod.SelectedValue + "'," +
                   "DirectDepositBankName='" + txtDirectDepositBankName.Text + "',DirectDepositBankCode='" + txtDirectDepositBankCode.Text + "', " +
                   "  DirectDepositBankBranchName='" + txtDirectDepositBankBranchName.Text + "', " +
                   " DirectDepositBankBranchNumber= '" + txtDirectDepositBankBranchNumber.Text + "'," +
                   " DirectDepositTransitNumber='" + txtDirectDepositTransitNumber.Text + "',DirectDepositAccountHolderName='" + txtDirectDepositAccountHolderName.Text + "', " +
                   " DirectDepositBankAccountType='" + ddlDirectDepositBankAccountType.SelectedItem.Text + "',DirectDepositBankAccountNumber='" + txtDirectDepositBankAccountNumber.Text + "',PayPeriodMasterId='" + ddlPaymentCycle.SelectedValue + "',LastName='" + txtlastname.Text + "',FirstName='" + txtfirstname.Text + "',Intials='" + txtintialis.Text + "',EmployeeNo='" + txtemployeeno.Text + "',DateOfBirth='" + txtdateofbirth.Text + "',SocialSecurityNo='" + txtsecurityno.Text + "' where EmployeePayrollMaster_Id='" + ViewState["Id"] + "' ";

                SqlCommand cmd = new SqlCommand(str, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                con.Close();
                Label1.Visible = true;
                Label1.Text = "Record updated successfully";
                filldata();

                ddlemployee.SelectedIndex = 0;
                // RadioButtonList1.SelectedValue.Checked = false;
                txtDirectDepositAccountHolderName.Text = "";
                txtDirectDepositBankAccountNumber.Text = "";


                txtDirectDepositBankBranchNumber.Text = "";
                txtDirectDepositBankBranchName.Text = "";
                txtDirectDepositBankCode.Text = "";
                txtDirectDepositBankName.Text = "";

                txtDirectDepositTransitNumber.Text = "";

                ddlDirectDepositBankAccountType.SelectedIndex = 0;
                ddlStore.SelectedIndex = 0;
                ddlPaymentMethod.SelectedIndex = 0;
                ddlPaymentCycle.SelectedIndex = 0;
                btnsubmit.Visible = true;
                btnupdate.Visible = false;
                ddpnl.Visible = false;
                
            }
            else if (ddlPaymentMethod.SelectedItem.Text == "Cash")
            {
                string str = "Update  EmployeePayrollMaster Set  EmpId='" + ddlemployee.SelectedValue + "',EmployeePaidAsPerDesignation='" + RadioButtonList1.SelectedValue + "',PaymentMethodId='" + ddlPaymentMethod.SelectedValue + "',Whid='" + ddlStore.SelectedValue + "',PayPeriodMasterId='" + ddlPaymentCycle.SelectedValue + "',LastName='" + txtlastname.Text + "',FirstName='" + txtfirstname.Text + "',Intials='" + txtintialis.Text + "',EmployeeNo='" + txtemployeeno.Text + "',DateOfBirth='" + txtdateofbirth.Text + "',SocialSecurityNo='" + txtsecurityno.Text + "' where EmployeePayrollMaster_Id='" + ViewState["Id"] + "'";
                SqlCommand cmd = new SqlCommand(str, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                con.Close();
                Label1.Visible = true;
                Label1.Text = "Record updated successfully";
                filldata();

                ddlemployee.SelectedIndex = 0;
                // RadioButtonList1.SelectedValue.Checked = false;

                ddlStore.SelectedIndex = 0;
                ddlPaymentMethod.SelectedIndex = 0;
                btnsubmit.Visible = true;
                btnupdate.Visible = false;
            }

            string empupdate = " update EmployeeMaster set Active='" + ddlstatus.SelectedValue + "' ";
            SqlCommand cmdempup = new SqlCommand(empupdate, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdempup.ExecuteNonQuery();
            con.Close();
            pnlparty.Visible = false;
            btnshowparty.Visible = true;
            clearall();
            lbladd.Text = "";

        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ViewState["DId"] = GridView1.DataKeys[e.RowIndex].Value.ToString();
        string st2 = "Delete from EmployeePayrollMaster  where EmployeePayrollMaster_Id='" + ViewState["DId"] + "' ";
        SqlCommand cmd2 = new SqlCommand(st2, con);
        
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
        cmd2.ExecuteNonQuery();
        con.Close();

        filldata();
        Label1.Visible = true;
        Label1.Text = "Record deleted successfully";
    }


    protected void ddlStore_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlStore.SelectedIndex > 0)
        //{
        //    string str = "  select [EmployeeMaster].[EmployeeMasterID],[EmployeeMaster].[EmployeeName] +':'+DesignationMaster.DesignationName+':'+Cast([EmployeeMaster].EmployeeMasterID as nvarchar(50)) as [EmployeeName]  from [EmployeeMaster] inner join DesignationMaster on DesignationMaster.DesignationMasterId=[EmployeeMaster].DesignationMasterId WHERE [Whid]='" + ddlStore.SelectedValue + "'  order by EmployeeName";
        //    SqlCommand cmd = new SqlCommand(str, con);
        //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //    DataSet ds = new DataSet();
        //    adp.Fill(ds);

        //    ddlemployee.DataSource = ds;
        //    ddlemployee.DataTextField = "EmployeeName";
        //    ddlemployee.DataValueField = "EmployeeMasterID";
        //    ddlemployee.DataBind();
        //    ddlemployee.Items.Insert(0, "--Select--");
        //    ddlemployee.Items[0].Value = "0";
        //}
        //else
        //{
        //    ddlemployee.DataSource = null;
        //    ddlemployee.DataTextField = "EmployeeName";
        //    ddlemployee.DataValueField = "EmployeeMasterID";
        //    ddlemployee.DataBind();
        //    ddlemployee.Items.Insert(0, "--Select--");
        //    ddlemployee.Items[0].Value = "0";
        //}
        
        fillemployee();
    }
    protected void ddlPaymentMethod_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPaymentMethod.SelectedItem.Text == "Demand Draft" || ddlPaymentMethod.SelectedItem.Text == "Cheque")
        {
            pnlreceivepayment.Visible = true;
            ddpnl.Visible = false;
            
            
           // lblPaymentReceivedNameOf.Visible = true;
           // txtPaymentReceivedNameOf.Visible = true;


        }
        else
        {
            pnlreceivepayment.Visible = false;
            ddpnl.Visible = false;
            

            //lblPaymentReceivedNameOf.Visible = false;
           // txtPaymentReceivedNameOf.Visible = false;
        }

        if (ddlPaymentMethod.SelectedItem.Text == "Paypal")
        {
            pnlpaypalid.Visible = true;
            ddpnl.Visible = false;
            
           // lblPaypalId.Visible = true;
           // txtPaypalId.Visible = true;
        }
        else
        {
            pnlpaypalid.Visible = false;
            ddpnl.Visible = false;
           
           // lblPaypalId.Visible = false;
           // txtPaypalId.Visible = false;
        }

        if (ddlPaymentMethod.SelectedItem.Text == "By Email")
        {
            pnlpaymentemail.Visible = true;
            
            ddpnl.Visible = false;
            

            
           // lblPaymentEmailId.Visible = true;
           // txtPaymentEmailId.Visible = true;
        }
        else
        {
            pnlpaymentemail.Visible = false;
            
            ddpnl.Visible = false;
            

            
           // lblPaymentEmailId.Visible = false;
            //txtPaymentEmailId.Visible = false;
        }

        if (ddlPaymentMethod.SelectedItem.Text == "Direct Deposit")
        {
            ddpnl.Visible = true;
            
            //lblDirectDepositAccountHolderName.Visible = true;
            //txtDirectDepositAccountHolderName.Visible = true;

            //lblDirectDepositBankAccountNumber.Visible = true;
            //txtDirectDepositBankAccountNumber.Visible = true;

            //lblDirectDepositBankAccountType.Visible = true;
            //ddlDirectDepositBankAccountType.Visible = true;

            //lblDirectDepositBankBranchName.Visible = true;
            //txtDirectDepositBankBranchName.Visible = true;

            //lblDirectDepositBankCode.Visible = true;
            //txtDirectDepositBankCode.Visible = true;

            //lblDirectDepositBankName.Visible = true;
            //txtDirectDepositBankName.Visible = true;

            //lblDirectDepositBankBranchNumber.Visible = true;
            //txtDirectDepositBankBranchNumber.Visible = true;

            //lblDirectDepositTransitNumber.Visible = true;
            //txtDirectDepositTransitNumber.Visible = true;



        }
        else
        {
            ddpnl.Visible = false;
            
            //lblDirectDepositAccountHolderName.Visible = false;
            //txtDirectDepositAccountHolderName.Visible = false;

            //lblDirectDepositBankAccountNumber.Visible = false;
            //txtDirectDepositBankAccountNumber.Visible = false;

            //lblDirectDepositBankAccountType.Visible = false;
            //ddlDirectDepositBankAccountType.Visible = false;

            //lblDirectDepositBankBranchName.Visible = false;
            //txtDirectDepositBankBranchNumber.Visible = false;

            //lblDirectDepositBankCode.Visible = false;
            //txtDirectDepositBankCode.Visible = false;

            //lblDirectDepositBankName.Visible = false;
            //txtDirectDepositBankName.Visible = false;

            //lblDirectDepositBankBranchNumber.Visible = false;
            //txtDirectDepositBankBranchNumber.Visible = false;

            //lblDirectDepositTransitNumber.Visible = false;
            //txtDirectDepositTransitNumber.Visible = false;
        }



    }


    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void clearall()
    {
        txtlastname.Text = "";
        txtfirstname.Text = "";
        txtintialis.Text = "";
        txtemployeeno.Text = "";
        txtdateofbirth.Text = "";
        txtsecurityno.Text = "";
        ddlPaymentCycle.SelectedIndex = 0;
        RadioButtonList1.SelectedIndex = 0;
        ddpnl.Visible = false;
        pnlpaymentemail.Visible = false;
        pnlpaypalid.Visible = false;
        pnlreceivepayment.Visible = false;
    }
    
    protected void ddlemployee_SelectedIndexChanged(object sender, EventArgs e)
    {

        statusselect();


    }
    protected void statusselect()
    {
        string str = "  select * from EmployeeMaster where EmployeeMasterID='" + ddlemployee.SelectedValue + "'";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);

        if (ds.Rows.Count > 0)
        {
            if (ds.Rows[0]["Active"].ToString() == "True")
            {
                ddlstatus.SelectedValue = "1";
            }
            else
            {
                ddlstatus.SelectedValue = "0";
            }
        }

    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        filldata();
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        if (Button5.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");

            Button5.Text = "Hide Printable Version";
            Button3.Visible = true;
            if (GridView1.Columns[6].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[6].Visible = false;
            }
            if (GridView1.Columns[7].Visible == true)
            {
                ViewState["deleteHide"] = "tt";
                GridView1.Columns[7].Visible = false;
            }
            if (GridView1.Columns[8].Visible == true)
            {
                ViewState["viewHide"] = "tt";
                GridView1.Columns[8].Visible = false;
            }
        }
        else
        {
           

            Button5.Text = "Printable Version";
            Button3.Visible = false;
            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[6].Visible = true;
            }
            if (ViewState["deleteHide"] != null)
            {
                GridView1.Columns[7].Visible = true;
            }
            if (ViewState["viewHide"] != null)
            {
                GridView1.Columns[8].Visible = true;
            }
        }
    }

    protected void btnshowparty_Click(object sender, EventArgs e)
    {
        pnlparty.Visible = true;
        btnshowparty.Visible = false;
        lbladd.Text = "Add Employee Payroll";
    }

    protected void ImageButton50_Click(object sender, ImageClickEventArgs e)
    {
        string te = "EmployeeMaster.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void ImageButton51_Click(object sender, ImageClickEventArgs e)
    {
        fillemployee();
    }
}