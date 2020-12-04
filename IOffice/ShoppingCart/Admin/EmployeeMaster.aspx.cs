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


public partial class ShoppingCart_Admin_Default : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(PageConn.connnn);
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    int groupid = 0;
    string accid = "";
    int classid = 0;
    string lblempno = "";
    DocumentCls1 clsDocument = new DocumentCls1();
    CompanyWizard clsCompany = new CompanyWizard();
    EmployeeCls clsEmployee = new EmployeeCls();
    string logoname1 = "";
    string todaydate = "";
    string dateafterthreeyear = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Login.aspx");
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
        string pass1 = tbPassword.Text;
        tbPassword.Attributes.Add("Value", pass1);
        string pass2 = tbConPassword.Text;
        tbConPassword.Attributes.Add("Value", pass2);

        todaydate = System.DateTime.Now.ToString();
        dateafterthreeyear = System.DateTime.Now.AddYears(3).ToString();

        if (!IsPostBack)
        {
            Pagecontrol.dypcontrol(Page, page);

            MultiView1.ActiveViewIndex = 0;

            ViewState["sortOrder"] = "";
            string str = "Select Id, Name From PartyMasterCategory where Name not in('Customer') order by Name ";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable datat = new DataTable();
            adp.Fill(datat);
            if (datat.Rows.Count > 0)
            {
                ddlpartycate.DataSource = datat;
                ddlpartycate.DataTextField = "Name";
                ddlpartycate.DataValueField = "Id";
                ddlpartycate.DataBind();
            }
            string qryStr = "select GroupId,GroupName from GroupMaster where GroupId in('2','5','15','20') order by GroupName";
            ddlGroup.DataSource = (DataTable)select(qryStr);
            fillddlOther(ddlGroup, "GroupName", "GroupId");

            Page.Form.Attributes.Add("enctype", "multipart/form-data");


            fillspecialsub();

            flaganddoc();

            if (Request.QueryString["Id"] == "1")
            {
                SqlDataAdapter daparty = new SqlDataAdapter("select max(PartyID) as PartyID from party_master", con);
                DataTable dtparty = new DataTable();
                daparty.Fill(dtparty);

                ViewState["partyidddd"] = dtparty.Rows[0]["PartyID"].ToString();

                SqlDataAdapter daparty1 = new SqlDataAdapter("select country,state,city from party_master where PartyID='" + ViewState["partyidddd"] + "'", con);
                DataTable dtparty1 = new DataTable();
                daparty1.Fill(dtparty1);

                fillstore();

                FillGridView1();

                filterstore();
                fillpaymentcycle();
                fillpaymentMethod();
                filleducationquali();
                //fillspecialsub();
                filllastjobposition();


                filterfilleducationquali();
                filterfillspecialsub();
                filterfilllastjobposition();

                fillpartytype();

                SqlDataAdapter daparty2 = new SqlDataAdapter("select partytypeid from PartytTypeMaster where compid='" + Session["Comid"] + "' and PartType='Employee'", con);
                DataTable dtparty2 = new DataTable();
                daparty2.Fill(dtparty2);

                ddlPartyType.SelectedIndex = ddlPartyType.Items.IndexOf(ddlPartyType.Items.FindByValue(dtparty2.Rows[0]["partytypeid"].ToString()));

                //   ddlPartyType.SelectedItem.Text = "Employee";

                fillrole();
                fillemployeetype();

                SqlDataAdapter daparty6 = new SqlDataAdapter("select employeetypeid from EmployeeType where CID='" + Session["Comid"] + "' and EmployeeTypeName='Permanent'", con);
                DataTable dtparty6 = new DataTable();
                daparty6.Fill(dtparty6);

                ddlemptype.SelectedIndex = ddlemptype.Items.IndexOf(ddlemptype.Items.FindByValue(dtparty6.Rows[0]["employeetypeid"].ToString()));

                fillcountry();
                ddlCountry.SelectedIndex = ddlCountry.Items.IndexOf(ddlCountry.Items.FindByValue(dtparty1.Rows[0]["country"].ToString()));
                ddlCountry_SelectedIndexChanged(sender, e);

                fillstate();
                ddlState.SelectedIndex = ddlState.Items.IndexOf(ddlState.Items.FindByValue(dtparty1.Rows[0]["state"].ToString()));
                ddlState_SelectedIndexChanged(sender, e);

                fillcity();
                ddlCity.SelectedIndex = ddlCity.Items.IndexOf(ddlCity.Items.FindByValue(dtparty1.Rows[0]["city"].ToString()));

                fillbankcountry();
                fillbankstate();
                fillbankcity();

                tbPassword.Attributes.Add("Value", "Default");
                tbConPassword.Attributes.Add("Value", "Default");


                txtjoindate.Text = System.DateTime.Now.ToShortDateString();
                txtstartdate.Text = System.DateTime.Now.ToShortDateString();
                string securitycode = alphanum();
                txtsecuritycode.Attributes.Add("Value", securitycode);

                //lblempno = number(8).ToString();
                //TextBox1.Text = lblempno.Substring(0, 4);

                //TextBox2.Text = lblempno.Substring(4, 4);
                //TextBox3.Text = lblempno.Substring(8, 4);
                //TextBox4.Text = lblempno.Substring(12, 4);
                ddlwarehouse_SelectedIndexChanged(sender, e);

                SqlDataAdapter daparty3 = new SqlDataAdapter("select max(EmployeeMasterID) as EmployeeMasterID from employeemaster", con);
                DataTable dtparty3 = new DataTable();
                daparty3.Fill(dtparty3);

                ViewState["employeemasteridd"] = dtparty3.Rows[0]["EmployeeMasterID"].ToString();

                SqlDataAdapter daparty4 = new SqlDataAdapter("select SuprviserId,DeptID,DesignationMasterId from employeemaster where employeemasterid='" + ViewState["employeemasteridd"] + "'", con);
                DataTable dtparty4 = new DataTable();
                daparty4.Fill(dtparty4);

                ddldept.SelectedIndex = ddldept.Items.IndexOf(ddldept.Items.FindByValue(dtparty4.Rows[0]["DeptID"].ToString()));
                ddldept_SelectedIndexChanged(sender, e);
                ddldesignation.SelectedIndex = ddldesignation.Items.IndexOf(ddldesignation.Items.FindByValue(dtparty4.Rows[0]["DesignationMasterId"].ToString()));

                ddlemp.SelectedIndex = ddlemp.Items.IndexOf(ddlemp.Items.FindByValue(dtparty4.Rows[0]["SuprviserId"].ToString()));

                SqlDataAdapter daparty9 = new SqlDataAdapter("select ID from batchmaster where CompanyId='" + Session["Comid"] + "' and Name='Reg 9 to 17:30 EST'", con);
                DataTable dtparty9 = new DataTable();
                daparty9.Fill(dtparty9);

                if (dtparty9.Rows.Count > 0)
                {

                    ddlbatch.SelectedIndex = ddlbatch.Items.IndexOf(ddlbatch.Items.FindByValue(dtparty9.Rows[0]["ID"].ToString()));

                }

                paybleper();
                paybleper111();
                Fillaccount();
                fillfilterdepartment();
                fillfilterdesignation();
                fillfilterbatch();
                fillfiltersupervisor();
                fillgriddata();

                chkempbarcode.Checked = true;
                chkempbarcode_CheckedChanged(sender, e);
                chkemppayroll_CheckedChanged(sender, e);
                ddlPaymentMethod.SelectedValue = "2";
                ddlPaymentMethod_SelectedIndexChanged(sender, e);
                ddlPaymentCycle.SelectedIndex = 1;

                SqlDataAdapter daremun = new SqlDataAdapter("select id from remunerationmaster where whid='" + ddlwarehouse.SelectedValue + "' and remunerationname='Office Salary'", con);
                DataTable dtremun = new DataTable();
                daremun.Fill(dtremun);

                if (dtremun.Rows.Count > 0)
                {
                    ddlremuneration.SelectedIndex = ddlremuneration.Items.IndexOf(ddlremuneration.Items.FindByValue(dtremun.Rows[0]["id"].ToString()));
                }
                txtamount.Text = "00";
                ddlpaybleper.SelectedValue = "2";
                DropDownList1.SelectedValue = "2";
            }
            else
            {

                fillstore();

                FillGridView1();

                filterstore();
                fillpaymentcycle();
                fillpaymentMethod();

                filleducationquali();
                //fillspecialsub();
                filllastjobposition();


                filterfilleducationquali();
                filterfillspecialsub();
                filterfilllastjobposition();

                fillpartytype();
                fillrole();
                fillemployeetype();
                fillcountry();
                fillstate();
                fillcity();
                fillbankcountry();
                fillbankstate();
                fillbankcity();
                fillstatusgrid();
                fillstatus();
                //   tbUserName.Text = txtfirstname.Text txtintialis.Text txtlastname.Text;
                tbPassword.Attributes.Add("Value", "Default");
                tbConPassword.Attributes.Add("Value", "Default");
                txtjoindate.Text = System.DateTime.Now.ToShortDateString();
                txtstartdate.Text = System.DateTime.Now.ToShortDateString();
                string securitycode = alphanum();
                txtsecuritycode.Attributes.Add("Value", securitycode);

                //lblempno = number(8).ToString();
                //TextBox1.Text = lblempno.Substring(0, 4);

                //TextBox2.Text = lblempno.Substring(4, 4);
                //TextBox3.Text = lblempno.Substring(8, 4);
                //TextBox4.Text = lblempno.Substring(12, 4);
                ddlwarehouse_SelectedIndexChanged(sender, e);

                fillfilterdepartment();
                fillfilterdesignation();
                fillfilterbatch();
                fillfiltersupervisor();
                fillgriddata();

                paybleper();
                paybleper111();
                Fillaccount();

                chkempbarcode.Checked = true;
                chkempbarcode_CheckedChanged(sender, e);
                chkemppayroll_CheckedChanged(sender, e);
                ddlPaymentMethod.SelectedValue = "2";
                ddlPaymentMethod_SelectedIndexChanged(sender, e);
                ddlPaymentCycle.SelectedIndex = 1;

                SqlDataAdapter daremun = new SqlDataAdapter("select id from remunerationmaster where whid='" + ddlwarehouse.SelectedValue + "' and remunerationname='Office Salary'", con);
                DataTable dtremun = new DataTable();
                daremun.Fill(dtremun);

                if (dtremun.Rows.Count > 0)
                {
                    ddlremuneration.SelectedIndex = ddlremuneration.Items.IndexOf(ddlremuneration.Items.FindByValue(dtremun.Rows[0]["id"].ToString()));
                }
                txtamount.Text = "00";
                ddlpaybleper.SelectedValue = "2";
                DropDownList1.SelectedValue = "2";
            }

            int i1 = 0;

            for (i1 = 1000; i1 <= 9999; i1++)
            {
                string strusercheck = " select EmployeeNo from EmployeePayrollMaster where Compid='" + Session["Comid"].ToString() + "' and EmployeeNo='" + i1 + "'";
                SqlCommand cmdusercheck = new SqlCommand(strusercheck, con);
                SqlDataAdapter adpusercheck = new SqlDataAdapter(cmdusercheck);
                DataTable dsusercheck = new DataTable();
                adpusercheck.Fill(dsusercheck);

                if (dsusercheck.Rows.Count > 0)
                {
                    TextBox1.Text = (i1 + 1).ToString();
                }
                else
                {
                    TextBox1.Text = i1.ToString();
                    break;
                }

            }
        }
    }

    protected void filleducationquali()
    {
        ddleduquali.Items.Clear();

        // string str = "select AreaofStudiesTbl.ID,AreaofStudiesTbl.Name,EducationDegrees.DegreeName,AreaofStudiesTbl.Name + ' : ' + EducationDegrees.DegreeName as Education from EducationDegrees inner join AreaofStudiesTbl on EducationDegrees.AreaofStudyID=AreaofStudiesTbl.ID where AreaofStudiesTbl.Active='1' order by AreaofStudiesTbl.Name,EducationDegrees.DegreeName asc";
        string str = "select AreaofStudiesTbl.ID,AreaofStudiesTbl.Name + ' : ' + LevelofEducationTBL.Name + ' : ' + EducationDegrees.DegreeName as Namee  from AreaofStudiesTbl inner join LevelofEducationTBL on LevelofEducationTBL.AreaofStudyID=AreaofStudiesTbl.Id inner join EducationDegrees on EducationDegrees.LevelofEducationTblID=LevelofEducationTBL.Id where AreaofStudiesTbl.Active='1' order by Namee asc";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            ddleduquali.DataSource = dt;
            ddleduquali.DataTextField = "Namee";
            ddleduquali.DataValueField = "ID";
            ddleduquali.DataBind();
        }
        ddleduquali.Items.Insert(0, "-Select-");
        ddleduquali.Items[0].Value = "0";
    }

    protected void fillstore()
    {
        ddlwarehouse.Items.Clear();

        DataTable ds = ClsStore.SelectStorename();
        ddlwarehouse.DataSource = ds;
        ddlwarehouse.DataTextField = "Name";
        ddlwarehouse.DataValueField = "WareHouseId";
        ddlwarehouse.DataBind();



        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
          //  ddlwarehouse.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }


    }

    protected void filterstore()
    {
        filterwarehouse.Items.Clear();

        DataTable ds = ClsStore.SelectStorename();
        ddlwarehouse.DataSource = ds;
        //ddlwarehouse.DataTextField = "Name";
        //ddlwarehouse.DataValueField = "WareHouseId";
     
        //ddlwarehouse.DataBind();

        filterwarehouse.DataSource = ds;
        filterwarehouse.DataTextField = "Name";
        filterwarehouse.DataValueField = "WarehouseId";
        filterwarehouse.DataBind();
        filterwarehouse.Items.Insert(0, "All");
        filterwarehouse.Items[0].Value = "0";
    }
    protected void fillpaymentMethod()
    {

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
        //  ddlPaymentMethod.SelectedItem.Text == "Cheque";
    }
    protected void fillpaymentcycle()
    {

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
        // ddlPaymentCycle.SelectedItem.Text = "Bi-Week";
    }
    protected void ddlPaymentMethod_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPaymentMethod.SelectedItem.Text == "Demand Draft" || ddlPaymentMethod.SelectedItem.Text == "Cheque")
        {
            pnlreceivepayment.Visible = true;
            ddpnl.Visible = false;
        }
        else
        {
            pnlreceivepayment.Visible = false;
            ddpnl.Visible = false;
        }

        if (ddlPaymentMethod.SelectedItem.Text == "Paypal")
        {
            pnlpaypalid.Visible = true;
            ddpnl.Visible = false;
        }
        else
        {
            pnlpaypalid.Visible = false;
            ddpnl.Visible = false;
        }

        if (ddlPaymentMethod.SelectedItem.Text == "By Email")
        {
            pnlpaymentemail.Visible = true;
            ddpnl.Visible = false;
        }
        else
        {
            pnlpaymentemail.Visible = false;
            ddpnl.Visible = false;
        }

        if (ddlPaymentMethod.SelectedItem.Text == "Direct Deposit")
        {
            ddpnl.Visible = true;
        }
        else
        {
            ddpnl.Visible = false;
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
    public void fillddlOther(DropDownList ddl, String dtf, String dvf)
    {
        ddl.DataTextField = dtf;
        ddl.DataValueField = dvf;
        ddl.DataBind();

    }
    protected void filldept()
    {
        ddldept.Items.Clear();
        ddldesignation.Items.Clear();
        String qryStr1 = "Select id,Departmentname from DepartmentmasterMNC  where DepartmentmasterMNC.Active=1 And Companyid='" + Session["Comid"] + "' and DepartmentmasterMNC.Whid='" + ddlwarehouse.SelectedValue + "'  order by Departmentname";
        ddldept.DataSource = (DataTable)select(qryStr1);
        fillddlOther(ddldept, "Departmentname", "id");
        ddldept.Items.Insert(0, "-Select-");
        ddldept.Items[0].Value = "0";
    }
    protected void filldesignation()
    {
        ddldesignation.Items.Clear();

        if (ddldept.SelectedIndex > 0)
        {
            string str = "SELECT DesignationMasterId,DesignationName,DeptID  FROM DesignationMaster where RoleId!=''and  DesignationMaster.Active=1 AND  DeptID=" + ddldept.SelectedValue + " order by DesignationName";
            ddldesignation.DataSource = (DataTable)select(str);
            fillddlOther(ddldesignation, "DesignationName", "DesignationMasterId");
            ddldesignation.Items.Insert(0, "-Select-");
            ddldesignation.Items[0].Value = "0";
        }
        else
        {
            ddldesignation.Items.Insert(0, "-Select-");
            ddldesignation.Items[0].Value = "0";
        }
    }
    protected void fillfilterdepartment()
    {
        ddlfilterdept.Items.Clear();

        string str = "select * from  DepartmentmasterMNC where DepartmentmasterMNC.Active=1 and Whid='" + filterwarehouse.SelectedValue + "' order by Departmentname";
        SqlCommand cmdfilterdept = new SqlCommand(str, con);
        SqlDataAdapter adpfilterdept = new SqlDataAdapter(cmdfilterdept);
        DataTable dtfilterdept = new DataTable();
        adpfilterdept.Fill(dtfilterdept);

        if (dtfilterdept.Rows.Count > 0)
        {
            ddlfilterdept.DataSource = dtfilterdept;
            ddlfilterdept.DataTextField = "Departmentname";
            ddlfilterdept.DataValueField = "id";
            ddlfilterdept.DataBind();
        }
        ddlfilterdept.Items.Insert(0, "All");
        ddlfilterdept.Items[0].Value = "0";

    }
    protected void fillfilterdesignation()
    {
        ddlfilterdesig.Items.Clear();

        string str = "select * from  DesignationMaster where DesignationMaster.Active=1 and DeptID='" + ddlfilterdept.SelectedValue + "' order by DesignationName";
        SqlCommand cmdfilterdept = new SqlCommand(str, con);
        SqlDataAdapter adpfilterdept = new SqlDataAdapter(cmdfilterdept);
        DataTable dtfilterdept = new DataTable();
        adpfilterdept.Fill(dtfilterdept);

        if (dtfilterdept.Rows.Count > 0)
        {
            ddlfilterdesig.DataSource = dtfilterdept;
            ddlfilterdesig.DataTextField = "DesignationName";
            ddlfilterdesig.DataValueField = "DesignationMasterId";
            ddlfilterdesig.DataBind();
        }
        ddlfilterdesig.Items.Insert(0, "All");
        ddlfilterdesig.Items[0].Value = "0";

    }
    protected void fillbankcountry()
    {
        string qryStr = "select CountryId,CountryName from CountryMaster order by CountryName";
        ddlDirectDepositBankBranchcountry.DataSource = (DataTable)select(qryStr);
        fillddlOther(ddlDirectDepositBankBranchcountry, "CountryName", "CountryId");
        ddlDirectDepositBankBranchcountry.Items.Insert(0, "-Select-");
        ddlDirectDepositBankBranchcountry.Items[0].Value = "0";
    }
    protected void fillbankstate()
    {
        ddlDirectDepositBankBranchstate.Items.Clear();
        if (ddlDirectDepositBankBranchcountry.SelectedIndex > 0)
        {
            string qryStr = "select StateId,StateName from StateMasterTbl where CountryId=" + ddlDirectDepositBankBranchcountry.SelectedValue + " order by StateName";
            ddlDirectDepositBankBranchstate.DataSource = (DataTable)select(qryStr);
            fillddlOther(ddlDirectDepositBankBranchstate, "StateName", "StateId");
            ddlDirectDepositBankBranchstate.Items.Insert(0, "-Select-");
            ddlDirectDepositBankBranchstate.Items[0].Value = "0";
        }
        else
        {
            ddlDirectDepositBankBranchstate.Items.Insert(0, "-Select-");
            ddlDirectDepositBankBranchstate.Items[0].Value = "0";
        }
    }


    protected void Fillaccount()
    {

        string str1 = "select * from RemunerationMaster where Whid='" + ddlwarehouse.SelectedValue + "' and Active=1 order by RemunerationName";
        SqlCommand cmd1 = new SqlCommand(str1, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataSet ds1 = new DataSet();
        adp1.Fill(ds1);
        ddlremuneration.DataSource = ds1;
        ddlremuneration.DataTextField = "RemunerationName";
        ddlremuneration.DataValueField = "Id";
        ddlremuneration.DataBind();


    }
    protected void paybleper()
    {
        string str1 = "select * from PeriodMaster12 where Id In ('2','3','4','7') order by Period_name ";
        SqlCommand cmd1 = new SqlCommand(str1, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataSet ds1 = new DataSet();
        adp1.Fill(ds1);
        ddlpaybleper.DataSource = ds1;
        ddlpaybleper.DataTextField = "Period_name";
        ddlpaybleper.DataValueField = "Id";
        ddlpaybleper.DataBind();

    }
    protected void fillbankcity()
    {
        ddlDirectDepositBankBranchcity.Items.Clear();
        if (ddlDirectDepositBankBranchstate.SelectedIndex > 0)
        {

            string qryStr = "select CityId,CityName from CityMasterTbl where StateId=" + ddlDirectDepositBankBranchstate.SelectedValue + " order by CityName";
            ddlDirectDepositBankBranchcity.DataSource = (DataTable)select(qryStr);
            fillddlOther(ddlDirectDepositBankBranchcity, "CityName", "CityId");
            ddlDirectDepositBankBranchcity.Items.Insert(0, "-Select-");
            ddlDirectDepositBankBranchcity.Items[0].Value = "0";
        }
        else
        {
            ddlDirectDepositBankBranchcity.Items.Insert(0, "-Select-");
            ddlDirectDepositBankBranchcity.Items[0].Value = "0";
        }
    }
    protected void fillcountry()
    {
        string qryStr = "select CountryId,CountryName from CountryMaster order by CountryName";
        ddlCountry.DataSource = (DataTable)select(qryStr);
        fillddlOther(ddlCountry, "CountryName", "CountryId");
        ddlCountry.Items.Insert(0, "-Select-");
        ddlCountry.Items[0].Value = "0";
    }
    protected void fillstate()
    {
        ddlState.Items.Clear();
        if (ddlCountry.SelectedIndex > 0)
        {
            string qryStr = "select StateId,StateName from StateMasterTbl where CountryId=" + ddlCountry.SelectedValue + " order by StateName";
            ddlState.DataSource = (DataTable)select(qryStr);
            fillddlOther(ddlState, "StateName", "StateId");
            ddlState.Items.Insert(0, "-Select-");
            ddlState.Items[0].Value = "0";
        }
        else
        {
            ddlState.Items.Insert(0, "-Select-");
            ddlState.Items[0].Value = "0";
        }
    }
    protected void fillcity()
    {
        ddlCity.Items.Clear();
        if (ddlState.SelectedIndex > 0)
        {

            string qryStr = "select CityId,CityName from CityMasterTbl where StateId=" + ddlState.SelectedValue + " order by CityName";
            ddlCity.DataSource = (DataTable)select(qryStr);
            fillddlOther(ddlCity, "CityName", "CityId");
            ddlCity.Items.Insert(0, "-Select-");
            ddlCity.Items[0].Value = "0";
        }
        else
        {
            ddlCity.Items.Insert(0, "-Select-");
            ddlCity.Items[0].Value = "0";
        }
    }
    protected void fillpartytype()
    {
        string qryStr = "select * from PartytTypeMaster  where  compid='" + Session["Comid"] + "' and PartType in('Admin','Employee') order by PartType";
        ddlPartyType.DataSource = (DataTable)select(qryStr);
        fillddlOther(ddlPartyType, "PartType", "PartyTypeId");
        ddlPartyType.Items.Insert(0, "-Select-");
        ddlPartyType.Items[0].Value = "0";
    }
    protected void fillrole()
    {
        int aid = 0;
        string emprole1 = " select Party_master.Account from User_master inner join Party_master on Party_master.PartyID=User_master.PartyID where Party_master.id='" + Session["Comid"] + "'  and User_master.UserID='" + Session["userid"] + "' ";
        SqlCommand cmdrole1 = new SqlCommand(emprole1, con);
        SqlDataAdapter darole1 = new SqlDataAdapter(cmdrole1);
        DataTable dtrole1 = new DataTable();
        darole1.Fill(dtrole1);
        if (dtrole1.Rows.Count > 0)
        {
            aid = Convert.ToInt32(dtrole1.Rows[0]["Account"].ToString());
        }

        if (aid == 30000)
        {
            string emprole = "SELECT [Role_id],[Role_name],[ActiveDeactive] FROM [RoleMaster] where compid='" + Session["Comid"] + "' order by Role_name";
            SqlCommand cmdrole = new SqlCommand(emprole, con1);
            SqlDataAdapter darole = new SqlDataAdapter(cmdrole);
            DataTable dtrole = new DataTable();

            darole.Fill(dtrole);
            ddlemprole.DataSource = dtrole;
            ddlemprole.DataTextField = "Role_name";
            ddlemprole.DataValueField = "Role_id";
            ddlemprole.DataBind();
        }
        else
        {
            string emprole = "SELECT [Role_id],[Role_name],[ActiveDeactive] FROM [RoleMaster] where compid='" + Session["Comid"] + "' and Role_name<>'Admin' order by Role_name";
            SqlCommand cmdrole = new SqlCommand(emprole, con1);
            SqlDataAdapter darole = new SqlDataAdapter(cmdrole);
            DataTable dtrole = new DataTable();

            darole.Fill(dtrole);
            ddlemprole.DataSource = dtrole;
            ddlemprole.DataTextField = "Role_name";
            ddlemprole.DataValueField = "Role_id";
            ddlemprole.DataBind();

        }
    }
    protected void fillbatch()
    {
        ddlbatch.Items.Clear();
        string str = "select * from BatchMaster  where WHID='" + ddlwarehouse.SelectedValue + "' order by Name ";

        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ddlbatch.DataSource = dt;
            ddlbatch.DataTextField = "Name";
            ddlbatch.DataValueField = "ID";
            ddlbatch.DataBind();
        }
        else
        {
            ddlbatch.Items.Insert(0, "-Select-");
            ddlbatch.Items[0].Value = "0";
        }


    }
    protected void fillfilterbatch()
    {
        ddlfilterbatch.Items.Clear();
        string str = "select * from BatchMaster  where WHID='" + filterwarehouse.SelectedValue + "' order by Name ";

        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ddlfilterbatch.DataSource = dt;
            ddlfilterbatch.DataTextField = "Name";
            ddlfilterbatch.DataValueField = "ID";
            ddlfilterbatch.DataBind();
        }
        ddlfilterbatch.Items.Insert(0, "All");
        ddlfilterbatch.Items[0].Value = "0";

    }
    protected void fillsupervisor()
    {
        ddlemp.Items.Clear();

        string st11 = "select ParentDesignationid from Designationmastertemp where Designationid='" + ddldesignation.SelectedValue + "'";
        SqlDataAdapter da1 = new SqlDataAdapter(st11, con);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);

        if (dt1.Rows.Count > 0)
        {
            string ssd = " Select EmployeeName,EmployeeMasterID from EmployeeMaster inner join Syncr_LicenseEmployee_With_JobcenterId   on Syncr_LicenseEmployee_With_JobcenterId.Jobcenter_Emp_id=EmployeeMaster.EmployeeMasterID where whid='" + ddlwarehouse.SelectedValue + "' and (DesignationMasterID='" + dt1.Rows[0]["ParentDesignationid"].ToString() + "' OR Description='Admin')";
            ssd = "Select EmployeeName,EmployeeMasterID from EmployeeMaster where Whid='" + ddlwarehouse.SelectedValue + "' AND EmployeeMaster.Active=1 ";//and Description='Admin'
            SqlDataAdapter da2 = new SqlDataAdapter(ssd, con);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);

            if (dt2.Rows.Count > 0)
            {
                ddlemp.DataSource = dt2;
                ddlemp.DataTextField = "EmployeeName";
                ddlemp.DataValueField = "EmployeeMasterID";
                ddlemp.DataBind();
                ddlemp.Items.Insert(0, "-Select-");
                ddlemp.Items[0].Value = "0";
            }
            else
            {
                ssd = "Select EmployeeName,EmployeeMasterID from EmployeeMaster where Whid='" + ddlwarehouse.SelectedValue + "' AND EmployeeMaster.Active=1 ";//and Description='Admin'

                 da2 = new SqlDataAdapter(ssd, con);
                 dt2 = new DataTable();
                da2.Fill(dt2);

                ddlemp.DataSource = dt2;
                ddlemp.DataTextField = "EmployeeName";
                ddlemp.DataValueField = "EmployeeMasterID";
                ddlemp.DataBind();
                ddlemp.Items.Insert(0, "-Select-");
                ddlemp.Items[0].Value = "0";
            }
        }
        else
        {
            string ssd = "Select EmployeeName,EmployeeMasterID from EmployeeMaster where Whid='" + ddlwarehouse.SelectedValue + "'  AND EmployeeMaster.Active=1 ";//and Description='Admin'

            SqlDataAdapter da2 = new SqlDataAdapter(ssd, con);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);

            ddlemp.DataSource = dt2;
            ddlemp.DataTextField = "EmployeeName";
            ddlemp.DataValueField = "EmployeeMasterID";
            ddlemp.DataBind();
            ddlemp.Items.Insert(0, "-Select-");
            ddlemp.Items[0].Value = "0";
        }
        
        //ddlemp.Items.Clear();
        //string str = "select EmployeeMasterID,DeptID,DesignationMasterId,EmployeeName from EmployeeMaster " +
        //                "where Whid='" + ddlwarehouse.SelectedValue + "' and Active=1 order by EmployeeName";
        //SqlCommand cmd = new SqlCommand(str, con);
        //SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //DataTable dt = new DataTable();
        //adp.Fill(dt);


    }
    protected void fillfiltersupervisor()
    {
        ddlsupervisor.Items.Clear();

        string st11 = "select ParentDesignationid from Designationmastertemp where Designationid='" + ddlfilterdesig.SelectedValue + "'";
        SqlDataAdapter da1 = new SqlDataAdapter(st11, con);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);

        if (dt1.Rows.Count > 0)
        {
            SqlDataAdapter da2 = new SqlDataAdapter("Select EmployeeName,EmployeeMasterID from EmployeeMaster where whid='" + filterwarehouse.SelectedValue + "' and (DesignationMasterID='" + dt1.Rows[0]["ParentDesignationid"].ToString() + "' OR Description='Admin')", con);
            DataTable dt = new DataTable();
            da2.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                ddlsupervisor.DataSource = dt;
                ddlsupervisor.DataTextField = "EmployeeName";
                ddlsupervisor.DataValueField = "EmployeeMasterID";
                ddlsupervisor.DataBind();
            }
            ddlsupervisor.Items.Insert(0, "All");
            ddlsupervisor.Items[0].Value = "0";
        }
        else
        {
            string ssd = "Select EmployeeName,EmployeeMasterID from EmployeeMaster where Whid='" + filterwarehouse.SelectedValue + "' and Description='Admin'";

            SqlDataAdapter da2 = new SqlDataAdapter(ssd, con);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);

            ddlsupervisor.DataSource = dt2;
            ddlsupervisor.DataTextField = "EmployeeName";
            ddlsupervisor.DataValueField = "EmployeeMasterID";
            ddlsupervisor.DataBind();
            ddlsupervisor.Items.Insert(0, "All");
            ddlsupervisor.Items[0].Value = "0";
        }



        //string str = "select EmployeeMasterID,DeptID,DesignationMasterId,EmployeeName from EmployeeMaster " +
        //                "where Whid='" + filterwarehouse.SelectedValue + "' and Active=1 order by EmployeeName";
        //SqlCommand cmd = new SqlCommand(str, con);
        //SqlDataAdapter adp = new SqlDataAdapter(cmd);




    }
    protected void fillemployeetype()
    {
        string qryStr = "SELECT   EmployeeTypeId    ,EmployeeTypeName  FROM EmployeeType where CID='" + Session["Comid"] + "' order by  EmployeeTypeName ";
        ddlemptype.DataSource = (DataTable)select(qryStr);
        fillddlOther(ddlemptype, "EmployeeTypeName", "EmployeeTypeId");
    }

    protected void fillemployeedesg()
    {
        string str = " select RemunerationByDesignation.*,DesignationMaster.DesignationMasterId ,DepartmentmasterMNC.Departmentname+':'+DesignationMaster.DesignationName  as DesignationName ,p2.Period_name as Period_name1,rm1.RemunerationName as RemunerationName,rm2.RemunerationName as RemunerationName1, " +
                   " '$'+ RemunerationByDesignation.Amount + ' Per ' + p2.Period_name as Amountpay, RemunerationByDesignation.Percentage +'% of '+ rm2.RemunerationName as Percentofsale, " +
                   " RemunerationByDesignation.SalesPercentage +'% of Sales of '  + (case  RemunerationByDesignation.PercentageOfSalesId when 0 then 'Employee himself' when 1 then 'Employee and his subbordinates' " +
                   " when 2 then 'entire business' else cast(RemunerationByDesignation.PercentageOfSalesId as nvarchar) end) as PercentageOfSalesemp, " +
                   " payperiodtype.Name as Paytypename,WareHouseMaster.Name from RemunerationByDesignation " +
                   " inner join DesignationMaster  on RemunerationByDesignation.DesignationId=DesignationMaster.DesignationMasterId " +
                   " inner join DepartmentmasterMNC on DesignationMaster.DeptID=DepartmentmasterMNC.id " +
                   " inner join RemunerationMaster as rm1 on rm1.Id=RemunerationByDesignation.Remuneration_Id " +
                   " left outer join RemunerationMaster as rm2 on rm2.Id=RemunerationByDesignation.IsPercentRemunerationId " +
                   " left outer join PeriodMaster12 as p2 on p2.Id=RemunerationByDesignation.PayablePer_PeriodMasterId left outer join payperiodtype on payperiodtype.Id=RemunerationByDesignation.PayableOfSales inner join Warehousemaster on Warehousemaster.WareHouseId = RemunerationByDesignation.Whid " +
                   " where RemunerationByDesignation.compid='" + Session["Comid"].ToString() + "' " +
                    " and RemunerationByDesignation.Whid='" + ddlwarehouse.SelectedValue + "' " +
                    " and RemunerationByDesignation.DesignationId='" + ddldesignation.SelectedValue + "' order by Name, DesignationName, RemunerationName";

        SqlCommand cmd1 = new SqlCommand(str, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable ds1 = new DataTable();
        adp1.Fill(ds1);

        GridView3.DataSource = ds1;
        GridView3.DataBind();

        if (GridView3.Rows.Count > 0)
        {
            RadioButtonList1.Items[0].Enabled = true;
            Button8.Visible = false;
            ImageButton1.Visible = false;
        }
        else
        {
            RadioButtonList1.Items[0].Enabled = false;
            Button8.Visible = true;
            ImageButton1.Visible = true;
        }
    }

    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedValue == "0")
        {
            Panel2.Visible = false;
            panlam.Visible = true;
            panel2nd.Visible = true;
        }
        if (RadioButtonList1.SelectedValue == "1")
        {
            fillemployeedesg();
            costcalcu();
            Panel2.Visible = true;
            panlam.Visible = false;
            panel2nd.Visible = false;
            panel3rd.Visible = false;
        }
    }

    protected void GridView4_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Views")
        {
            string te = "Employeesalarymaster.aspx";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        }
    }

    protected decimal costcalcu()
    {
        int ress = 0;

        SqlDataAdapter dacount = new SqlDataAdapter("select count(Amount) as me from RemunerationByDesignation where DesignationId='" + ddldesignation.SelectedValue + "' and IsPercent_IsAmount='0'", con);
        DataTable dtcount = new DataTable();
        dacount.Fill(dtcount);

        if (dtcount.Rows.Count > 0)
        {
            if (Convert.ToString(dtcount.Rows[0]["me"]) != "")
            {
                ress = Convert.ToInt32(dtcount.Rows[0]["me"]);
            }
        }


        double d1 = 0;
        double d2 = 0;
        double d3 = 0;
        double d4 = 0;
        double d5 = 0;

        SqlDataAdapter da1 = new SqlDataAdapter("select sum(cast(Amount as float)) as Amount1 from RemunerationByDesignation where DesignationId='" + ddldesignation.SelectedValue + "' and PayablePer_PeriodMasterId='2'", con);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);

        int workday0 = 0;
        decimal empsalamt0 = 0;

        int workday = 0;
        decimal empsalamt = 0;

        int workday1 = 0;
        decimal empsalamt1 = 0;

        int workday2 = 0;
        decimal empsalamt2 = 0;

        int workday3 = 0;
        decimal empsalamt3 = 0;

        if (dt1.Rows.Count > 0)
        {
            if (Convert.ToString(dt1.Rows[0]["Amount1"]) != "")
            {
                d1 = Convert.ToDouble(dt1.Rows[0]["Amount1"]);

                DataTable dtr0 = select("select * from BatchWorkingDay where BatchMasterId='" + ddlbatch.SelectedValue + "'  and Whid='" + ddlwarehouse.SelectedValue + "' ");

                if (dtr0.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dtr0.Rows[0]["Monday"]) == 1)
                    {

                        DataTable dtr00 = select("select * from BatchTiming where Active='1' and BatchMasterId='" + ddlbatch.SelectedValue + "'  and TimeScheduleMasterId='" + dtr0.Rows[0]["MondayScheduleId"] + "' ");

                        if (dtr00.Rows.Count > 0)
                        {
                            workday0 += 1;
                            empsalamt0 += salcal(dtr00);

                        }
                    }
                    if (Convert.ToInt32(dtr0.Rows[0]["Tuesday"]) == 1)
                    {

                        DataTable dtr00 = select("select * from BatchTiming where Active='1' and BatchMasterId='" + ddlbatch.SelectedValue + "'  and TimeScheduleMasterId='" + dtr0.Rows[0]["TuesdayScheduleId"] + "' ");

                        if (dtr00.Rows.Count > 0)
                        {
                            workday0 += 1;
                            empsalamt0 += salcal(dtr00);

                        }
                    }
                    if (Convert.ToInt32(dtr0.Rows[0]["Wednesday"]) == 1)
                    {

                        DataTable dtr00 = select("select * from BatchTiming where Active='1' and BatchMasterId='" + ddlbatch.SelectedValue + "'  and TimeScheduleMasterId='" + dtr0.Rows[0]["WednesdayScheduleId"] + "' ");

                        if (dtr00.Rows.Count > 0)
                        {
                            workday0 += 1;
                            empsalamt0 += salcal(dtr00);

                        }
                    }
                    if (Convert.ToInt32(dtr0.Rows[0]["Thursday"]) == 1)
                    {

                        DataTable dtr00 = select("select * from BatchTiming where Active='1' and BatchMasterId='" + ddlbatch.SelectedValue + "'  and TimeScheduleMasterId='" + dtr0.Rows[0]["ThursdayScheduleId"] + "' ");

                        if (dtr00.Rows.Count > 0)
                        {
                            workday0 += 1;
                            empsalamt0 += salcal(dtr00);

                        }
                    }
                    if (Convert.ToInt32(dtr0.Rows[0]["Friday"]) == 1)
                    {

                        DataTable dtr00 = select("select * from BatchTiming where Active='1' and BatchMasterId='" + ddlbatch.SelectedValue + "'  and TimeScheduleMasterId='" + dtr0.Rows[0]["FridayScheduleId"] + "' ");

                        if (dtr00.Rows.Count > 0)
                        {
                            workday0 += 1;
                            empsalamt0 += salcal(dtr00);

                        }
                    }
                    if (Convert.ToInt32(dtr0.Rows[0]["Saturday"]) == 1)
                    {
                        DataTable dtr00 = select("select * from BatchTiming where Active='1' and BatchMasterId='" + ddlbatch.SelectedValue + "'  and TimeScheduleMasterId='" + dtr0.Rows[0]["SaturdayScheduleId"] + "' ");

                        if (dtr00.Rows.Count > 0)
                        {
                            workday0 += 1;
                            empsalamt0 += salcal(dtr00);

                        }
                    }
                    if (Convert.ToInt32(dtr0.Rows[0]["Sunday"]) == 1)
                    {
                        DataTable dtr00 = select("select * from BatchTiming where Active='1' and BatchMasterId='" + ddlbatch.SelectedValue + "'  and TimeScheduleMasterId='" + dtr0.Rows[0]["SundayScheduleId"] + "' ");

                        if (dtr00.Rows.Count > 0)
                        {
                            workday0 += 1;
                            empsalamt0 += salcal(dtr00);

                        }
                    }
                }
                else
                {
                    empsalamt0 = Convert.ToDecimal(d1);
                    empsalamt0 = Math.Round(empsalamt0, 2);
                }

                if (empsalamt0 > 0)
                {
                    empsalamt0 = Convert.ToDecimal(d1);
                    empsalamt0 = Math.Round(empsalamt0, 2);
                }
                else
                {
                    empsalamt0 = 0;
                }

            }


        }


        SqlDataAdapter da2 = new SqlDataAdapter("select sum(cast(Amount as float)) as Amount2 from RemunerationByDesignation where DesignationId='" + ddldesignation.SelectedValue + "' and PayablePer_PeriodMasterId='3'", con);
        DataTable dt2 = new DataTable();
        da2.Fill(dt2);

        if (dt2.Rows.Count > 0)
        {
            if (Convert.ToString(dt2.Rows[0]["Amount2"]) != "")
            {
                d2 = Convert.ToDouble(dt2.Rows[0]["Amount2"]);


                DataTable dtr1 = select("select * from BatchWorkingDay where BatchMasterId='" + ddlbatch.SelectedValue + "'  and Whid='" + ddlwarehouse.SelectedValue + "' ");

                if (dtr1.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dtr1.Rows[0]["Monday"]) == 1)
                    {

                        DataTable dtr11 = select("select * from BatchTiming where Active='1' and BatchMasterId='" + ddlbatch.SelectedValue + "'  and TimeScheduleMasterId='" + dtr1.Rows[0]["MondayScheduleId"] + "' ");

                        if (dtr11.Rows.Count > 0)
                        {
                            workday += 1;
                            empsalamt += salcal(dtr11);

                        }
                    }
                    if (Convert.ToInt32(dtr1.Rows[0]["Tuesday"]) == 1)
                    {

                        DataTable dtr11 = select("select * from BatchTiming where Active='1' and BatchMasterId='" + ddlbatch.SelectedValue + "'  and TimeScheduleMasterId='" + dtr1.Rows[0]["TuesdayScheduleId"] + "' ");

                        if (dtr11.Rows.Count > 0)
                        {
                            workday += 1;
                            empsalamt += salcal(dtr11);

                        }
                    }
                    if (Convert.ToInt32(dtr1.Rows[0]["Wednesday"]) == 1)
                    {

                        DataTable dtr11 = select("select * from BatchTiming where Active='1' and BatchMasterId='" + ddlbatch.SelectedValue + "'  and TimeScheduleMasterId='" + dtr1.Rows[0]["WednesdayScheduleId"] + "' ");

                        if (dtr11.Rows.Count > 0)
                        {
                            workday += 1;
                            empsalamt += salcal(dtr11);

                        }
                    }
                    if (Convert.ToInt32(dtr1.Rows[0]["Thursday"]) == 1)
                    {

                        DataTable dtr11 = select("select * from BatchTiming where Active='1' and BatchMasterId='" + ddlbatch.SelectedValue + "'  and TimeScheduleMasterId='" + dtr1.Rows[0]["ThursdayScheduleId"] + "' ");

                        if (dtr11.Rows.Count > 0)
                        {
                            workday += 1;
                            empsalamt += salcal(dtr11);

                        }
                    }
                    if (Convert.ToInt32(dtr1.Rows[0]["Friday"]) == 1)
                    {

                        DataTable dtr11 = select("select * from BatchTiming where Active='1' and BatchMasterId='" + ddlbatch.SelectedValue + "'  and TimeScheduleMasterId='" + dtr1.Rows[0]["FridayScheduleId"] + "' ");

                        if (dtr11.Rows.Count > 0)
                        {
                            workday += 1;
                            empsalamt += salcal(dtr11);

                        }
                    }
                    if (Convert.ToInt32(dtr1.Rows[0]["Saturday"]) == 1)
                    {
                        DataTable dtr11 = select("select * from BatchTiming where Active='1' and BatchMasterId='" + ddlbatch.SelectedValue + "'  and TimeScheduleMasterId='" + dtr1.Rows[0]["SaturdayScheduleId"] + "' ");

                        if (dtr11.Rows.Count > 0)
                        {
                            workday += 1;
                            empsalamt += salcal(dtr11);

                        }
                    }
                    if (Convert.ToInt32(dtr1.Rows[0]["Sunday"]) == 1)
                    {
                        DataTable dtr11 = select("select * from BatchTiming where Active='1' and BatchMasterId='" + ddlbatch.SelectedValue + "'  and TimeScheduleMasterId='" + dtr1.Rows[0]["SundayScheduleId"] + "' ");

                        if (dtr11.Rows.Count > 0)
                        {
                            workday += 1;
                            empsalamt += salcal(dtr11);

                        }
                    }
                }
                else
                {
                    empsalamt = Convert.ToDecimal(d2);
                    empsalamt = Math.Round(empsalamt, 2);
                }

                if (empsalamt > 0 && workday != 0)
                {
                    decimal dayhour = empsalamt / workday;
                    empsalamt = Convert.ToDecimal(d2) / dayhour;
                    empsalamt = Math.Round(empsalamt, 2);
                }
                else
                {
                    empsalamt = 0;
                }
            }
        }

        SqlDataAdapter da3 = new SqlDataAdapter("select sum(cast(Amount as float)) as Amount3 from RemunerationByDesignation where DesignationId='" + ddldesignation.SelectedValue + "' and PayablePer_PeriodMasterId='4'", con);
        DataTable dt3 = new DataTable();
        da3.Fill(dt3);

        if (dt3.Rows.Count > 0)
        {
            if (Convert.ToString(dt3.Rows[0]["Amount3"]) != "")
            {

                d3 = Convert.ToDouble(dt3.Rows[0]["Amount3"]);

                DataTable dtr2 = select("select * from BatchWorkingDay where BatchMasterId='" + ddlbatch.SelectedValue + "'  and Whid='" + ddlwarehouse.SelectedValue + "' ");

                if (dtr2.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dtr2.Rows[0]["Monday"]) == 1)
                    {

                        DataTable dtr22 = select("select * from BatchTiming where Active='1' and BatchMasterId='" + ddlbatch.SelectedValue + "'  and TimeScheduleMasterId='" + dtr2.Rows[0]["MondayScheduleId"] + "' ");

                        if (dtr22.Rows.Count > 0)
                        {
                            workday1 += 1;
                            empsalamt1 += salcal(dtr22);

                        }
                    }
                    if (Convert.ToInt32(dtr2.Rows[0]["Tuesday"]) == 1)
                    {

                        DataTable dtr22 = select("select * from BatchTiming where Active='1' and BatchMasterId='" + ddlbatch.SelectedValue + "'  and TimeScheduleMasterId='" + dtr2.Rows[0]["TuesdayScheduleId"] + "' ");

                        if (dtr22.Rows.Count > 0)
                        {
                            workday1 += 1;
                            empsalamt1 += salcal(dtr22);

                        }
                    }
                    if (Convert.ToInt32(dtr2.Rows[0]["Wednesday"]) == 1)
                    {

                        DataTable dtr22 = select("select * from BatchTiming where Active='1' and BatchMasterId='" + ddlbatch.SelectedValue + "'  and TimeScheduleMasterId='" + dtr2.Rows[0]["WednesdayScheduleId"] + "' ");

                        if (dtr22.Rows.Count > 0)
                        {
                            workday1 += 1;
                            empsalamt1 += salcal(dtr22);

                        }
                    }
                    if (Convert.ToInt32(dtr2.Rows[0]["Thursday"]) == 1)
                    {

                        DataTable dtr22 = select("select * from BatchTiming where Active='1' and BatchMasterId='" + ddlbatch.SelectedValue + "'  and TimeScheduleMasterId='" + dtr2.Rows[0]["ThursdayScheduleId"] + "' ");

                        if (dtr22.Rows.Count > 0)
                        {
                            workday1 += 1;
                            empsalamt1 += salcal(dtr22);

                        }
                    }
                    if (Convert.ToInt32(dtr2.Rows[0]["Friday"]) == 1)
                    {

                        DataTable dtr22 = select("select * from BatchTiming where Active='1' and BatchMasterId='" + ddlbatch.SelectedValue + "'  and TimeScheduleMasterId='" + dtr2.Rows[0]["FridayScheduleId"] + "' ");

                        if (dtr22.Rows.Count > 0)
                        {
                            workday1 += 1;
                            empsalamt1 += salcal(dtr22);

                        }
                    }
                    if (Convert.ToInt32(dtr2.Rows[0]["Saturday"]) == 1)
                    {
                        DataTable dtr22 = select("select * from BatchTiming where Active='1' and BatchMasterId='" + ddlbatch.SelectedValue + "'  and TimeScheduleMasterId='" + dtr2.Rows[0]["SaturdayScheduleId"] + "' ");

                        if (dtr22.Rows.Count > 0)
                        {
                            workday1 += 1;
                            empsalamt1 += salcal(dtr22);

                        }
                    }
                    if (Convert.ToInt32(dtr2.Rows[0]["Sunday"]) == 1)
                    {
                        DataTable dtr22 = select("select * from BatchTiming where Active='1' and BatchMasterId='" + ddlbatch.SelectedValue + "'  and TimeScheduleMasterId='" + dtr2.Rows[0]["SundayScheduleId"] + "' ");

                        if (dtr22.Rows.Count > 0)
                        {
                            workday1 += 1;
                            empsalamt1 += salcal(dtr22);

                        }
                    }
                }
                else
                {
                    empsalamt1 = Convert.ToDecimal(d3);
                    empsalamt1 = Math.Round(empsalamt1, 2);
                }

                if (empsalamt1 > 0 && workday1 != 0)
                {
                    decimal dayhour1 = empsalamt1;
                    empsalamt1 = Convert.ToDecimal(d3) / dayhour1;
                    empsalamt1 = Math.Round(empsalamt1, 2);
                }
                else
                {
                    empsalamt1 = 0;
                }
            }
        }

        SqlDataAdapter da4 = new SqlDataAdapter("select sum(cast(Amount as float)) as Amount4 from RemunerationByDesignation where DesignationId='" + ddldesignation.SelectedValue + "' and PayablePer_PeriodMasterId='7'", con);
        DataTable dt4 = new DataTable();
        da4.Fill(dt4);

        if (dt4.Rows.Count > 0)
        {
            if (Convert.ToString(dt4.Rows[0]["Amount4"]) != "")
            {

                d4 = Convert.ToDouble(dt4.Rows[0]["Amount4"]);

                DataTable dtr3 = select("select * from BatchWorkingDay where BatchMasterId='" + ddlbatch.SelectedValue + "'  and Whid='" + ddlwarehouse.SelectedValue + "' ");

                if (dtr3.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dtr3.Rows[0]["Monday"]) == 1)
                    {

                        DataTable dtr33 = select("select * from BatchTiming where Active='1' and BatchMasterId='" + ddlbatch.SelectedValue + "'  and TimeScheduleMasterId='" + dtr3.Rows[0]["MondayScheduleId"] + "' ");

                        if (dtr33.Rows.Count > 0)
                        {
                            workday2 += 1;
                            empsalamt2 += salcal(dtr33);

                        }
                    }
                    if (Convert.ToInt32(dtr3.Rows[0]["Tuesday"]) == 1)
                    {

                        DataTable dtr33 = select("select * from BatchTiming where Active='1' and BatchMasterId='" + ddlbatch.SelectedValue + "'  and TimeScheduleMasterId='" + dtr3.Rows[0]["TuesdayScheduleId"] + "' ");

                        if (dtr33.Rows.Count > 0)
                        {
                            workday2 += 1;
                            empsalamt2 += salcal(dtr33);

                        }
                    }
                    if (Convert.ToInt32(dtr3.Rows[0]["Wednesday"]) == 1)
                    {

                        DataTable dtr33 = select("select * from BatchTiming where Active='1' and BatchMasterId='" + ddlbatch.SelectedValue + "'  and TimeScheduleMasterId='" + dtr3.Rows[0]["WednesdayScheduleId"] + "' ");

                        if (dtr33.Rows.Count > 0)
                        {
                            workday2 += 1;
                            empsalamt2 += salcal(dtr33);

                        }
                    }
                    if (Convert.ToInt32(dtr3.Rows[0]["Thursday"]) == 1)
                    {

                        DataTable dtr33 = select("select * from BatchTiming where Active='1' and BatchMasterId='" + ddlbatch.SelectedValue + "'  and TimeScheduleMasterId='" + dtr3.Rows[0]["ThursdayScheduleId"] + "' ");

                        if (dtr33.Rows.Count > 0)
                        {
                            workday2 += 1;
                            empsalamt2 += salcal(dtr33);

                        }
                    }
                    if (Convert.ToInt32(dtr3.Rows[0]["Friday"]) == 1)
                    {

                        DataTable dtr33 = select("select * from BatchTiming where Active='1' and BatchMasterId='" + ddlbatch.SelectedValue + "'  and TimeScheduleMasterId='" + dtr3.Rows[0]["FridayScheduleId"] + "' ");

                        if (dtr33.Rows.Count > 0)
                        {
                            workday2 += 1;
                            empsalamt2 += salcal(dtr33);

                        }
                    }
                    if (Convert.ToInt32(dtr3.Rows[0]["Saturday"]) == 1)
                    {
                        DataTable dtr33 = select("select * from BatchTiming where Active='1' and BatchMasterId='" + ddlbatch.SelectedValue + "'  and TimeScheduleMasterId='" + dtr3.Rows[0]["SaturdayScheduleId"] + "' ");

                        if (dtr33.Rows.Count > 0)
                        {
                            workday2 += 1;
                            empsalamt2 += salcal(dtr33);

                        }
                    }
                    if (Convert.ToInt32(dtr3.Rows[0]["Sunday"]) == 1)
                    {
                        DataTable dtr33 = select("select * from BatchTiming where Active='1' and BatchMasterId='" + ddlbatch.SelectedValue + "'  and TimeScheduleMasterId='" + dtr3.Rows[0]["SundayScheduleId"] + "' ");

                        if (dtr33.Rows.Count > 0)
                        {
                            workday2 += 1;
                            empsalamt2 += salcal(dtr33);

                        }
                    }
                }
                else
                {
                    empsalamt2 = Convert.ToDecimal(d4);
                    empsalamt2 = Math.Round(empsalamt2, 2);
                }

                if (empsalamt > 0 && workday2 != 0)
                {
                    decimal caldata2 = Convert.ToDecimal(d4) * 12 / 52;
                    decimal dayhour2 = empsalamt2;
                    empsalamt2 = Convert.ToDecimal(caldata2) / dayhour2;
                    empsalamt2 = Math.Round(empsalamt2, 2);
                }
                else
                {
                    empsalamt2 = 0;
                }
            }
        }

        SqlDataAdapter da5 = new SqlDataAdapter("select sum(cast(Amount as float)) as Amount5 from RemunerationByDesignation where DesignationId='" + ddldesignation.SelectedValue + "' and PayablePer_PeriodMasterId='14'", con);
        DataTable dt5 = new DataTable();
        da5.Fill(dt5);

        if (dt5.Rows.Count > 0)
        {
            if (Convert.ToString(dt5.Rows[0]["Amount5"]) != "")
            {
                d5 = Convert.ToDouble(dt5.Rows[0]["Amount5"]);

                DataTable dtr4 = select("select * from BatchWorkingDay where BatchMasterId='" + ddlbatch.SelectedValue + "'  and Whid='" + ddlwarehouse.SelectedValue + "' ");

                if (dtr4.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dtr4.Rows[0]["Monday"]) == 1)
                    {

                        DataTable dtr44 = select("select * from BatchTiming where Active='1' and BatchMasterId='" + ddlbatch.SelectedValue + "'  and TimeScheduleMasterId='" + dtr4.Rows[0]["MondayScheduleId"] + "' ");

                        if (dtr44.Rows.Count > 0)
                        {
                            workday3 += 1;
                            empsalamt3 += salcal(dtr44);

                        }
                    }
                    if (Convert.ToInt32(dtr4.Rows[0]["Tuesday"]) == 1)
                    {

                        DataTable dtr44 = select("select * from BatchTiming where Active='1' and BatchMasterId='" + ddlbatch.SelectedValue + "'  and TimeScheduleMasterId='" + dtr4.Rows[0]["TuesdayScheduleId"] + "' ");

                        if (dtr44.Rows.Count > 0)
                        {
                            workday3 += 1;
                            empsalamt3 += salcal(dtr44);

                        }
                    }
                    if (Convert.ToInt32(dtr4.Rows[0]["Wednesday"]) == 1)
                    {

                        DataTable dtr44 = select("select * from BatchTiming where Active='1' and BatchMasterId='" + ddlbatch.SelectedValue + "'  and TimeScheduleMasterId='" + dtr4.Rows[0]["WednesdayScheduleId"] + "' ");

                        if (dtr44.Rows.Count > 0)
                        {
                            workday3 += 1;
                            empsalamt3 += salcal(dtr44);

                        }
                    }
                    if (Convert.ToInt32(dtr4.Rows[0]["Thursday"]) == 1)
                    {

                        DataTable dtr44 = select("select * from BatchTiming where Active='1' and BatchMasterId='" + ddlbatch.SelectedValue + "'  and TimeScheduleMasterId='" + dtr4.Rows[0]["ThursdayScheduleId"] + "' ");

                        if (dtr44.Rows.Count > 0)
                        {
                            workday3 += 1;
                            empsalamt3 += salcal(dtr44);

                        }
                    }
                    if (Convert.ToInt32(dtr4.Rows[0]["Friday"]) == 1)
                    {

                        DataTable dtr44 = select("select * from BatchTiming where Active='1' and BatchMasterId='" + ddlbatch.SelectedValue + "'  and TimeScheduleMasterId='" + dtr4.Rows[0]["FridayScheduleId"] + "' ");

                        if (dtr44.Rows.Count > 0)
                        {
                            workday3 += 1;
                            empsalamt3 += salcal(dtr44);

                        }
                    }
                    if (Convert.ToInt32(dtr4.Rows[0]["Saturday"]) == 1)
                    {
                        DataTable dtr44 = select("select * from BatchTiming where Active='1' and BatchMasterId='" + ddlbatch.SelectedValue + "'  and TimeScheduleMasterId='" + dtr4.Rows[0]["SaturdayScheduleId"] + "' ");

                        if (dtr44.Rows.Count > 0)
                        {
                            workday3 += 1;
                            empsalamt3 += salcal(dtr44);

                        }
                    }
                    if (Convert.ToInt32(dtr4.Rows[0]["Sunday"]) == 1)
                    {
                        DataTable dtr44 = select("select * from BatchTiming where Active='1' and BatchMasterId='" + ddlbatch.SelectedValue + "'  and TimeScheduleMasterId='" + dtr4.Rows[0]["SundayScheduleId"] + "' ");

                        if (dtr44.Rows.Count > 0)
                        {
                            workday3 += 1;
                            empsalamt3 += salcal(dtr44);

                        }
                    }
                }
                else
                {
                    empsalamt3 = Convert.ToDecimal(d5);
                    empsalamt3 = Math.Round(empsalamt3, 2);
                }

                if (empsalamt3 > 0 && workday3 != 0)
                {
                    decimal caldata3 = Convert.ToDecimal(d5) / 52;
                    decimal dayhour3 = empsalamt3;
                    empsalamt3 = Convert.ToDecimal(caldata3) / dayhour3;
                    empsalamt3 = Math.Round(empsalamt3, 2);
                }
                else
                {
                    empsalamt3 = 0;
                }
            }
        }
        decimal salary = empsalamt0 + empsalamt + empsalamt1 + empsalamt2 + empsalamt3;

        decimal finalsal = 0;

        if (ress != 0)
        {
            finalsal = salary / Convert.ToDecimal(ress);
        }
        else
        {
            finalsal = 0;
        }
        finalsal = Math.Round(finalsal, 2);

        return finalsal;
    }

    protected void ddlwarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkaccessright.Checked = true;
        chkaccessright_CheckedChanged(sender, e);

        FillGridView1();
        Fillaccount();
        filldept();
        filldesignation();
        fillbatch();
        fillsupervisor();
        fillemployeedesg();
        RadioButtonList1_SelectedIndexChanged(sender, e);
    }
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDirectDepositBankBranchcountry.SelectedValue = ddlCountry.SelectedValue;
        ddlDirectDepositBankBranchcountry_SelectedIndexChanged(sender, e);
        fillstate();
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDirectDepositBankBranchstate.SelectedValue = ddlState.SelectedValue;
        ddlDirectDepositBankBranchstate_SelectedIndexChanged(sender, e);
        fillcity();
    }
    protected void ddldept_SelectedIndexChanged(object sender, EventArgs e)
    {
        filldesignation();
    }
    protected void ddldesignation_SelectedIndexChanged1(object sender, EventArgs e)
    {
        
      
            fillsupervisor();
            fillemployeedesg();
            RadioButtonList1_SelectedIndexChanged(sender, e);
            string str = "Select distinct BatchId From BatchByDefault where DesignationId='" + ddldesignation.SelectedValue + "' ";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable datat = new DataTable();
            adp.Fill(datat);
            if (datat.Rows.Count > 0)
            {
                ddlbatch.SelectedIndex = ddlbatch.Items.IndexOf(ddlbatch.Items.FindByValue(datat.Rows[0]["BatchId"].ToString()));
            }
            if (ddldesignation.SelectedIndex > 0)
            {
                string str1 = "Select RoleId from DesignationMaster where DesignationMasterId='" + ddldesignation.SelectedValue + "' ";
                SqlCommand cmd1 = new SqlCommand(str1, con);
                SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                DataTable datat1 = new DataTable();
                adp1.Fill(datat1);
                if (datat1.Rows.Count > 0)
                {
                    ddlemprole.SelectedIndex = ddlemprole.Items.IndexOf(ddlemprole.Items.FindByValue(datat1.Rows[0]["RoleId"].ToString()));
                }
            }
       
    }
    protected void ddlPartyType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblpno.Text = "0";
        if (ddlPartyType.SelectedIndex > 0)
        {
            string str = "Select PartytTypeMaster.*,PartyMasterCategory.PartyMasterCategoryNo From PartytTypeMaster inner join PartyMasterCategory on PartyMasterCategory.ID=PartytTypeMaster.PartyCategoryId where  PartType='" + ddlPartyType.SelectedItem.Text + "' and compid='" + Session["comid"] + "' order by Name ";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable datat = new DataTable();
            adp.Fill(datat);
            if (datat.Rows.Count > 0)
            {
                lblpno.Text = Convert.ToString(datat.Rows[0]["PartyMasterCategoryNo"]);
            }
            //ddlAssAccManagerId.Visible = false;
            //ddlAssPurDeptId.Visible = false;
            //ddlAssRecieveDeptId.Visible = false;
            //ddlAssSalDeptId.Visible = false;
            //ddlAssShipDeptId.Visible = false;
            //if (ddlPartyType.SelectedItem.Text == "Employee")
            //{
            ddlAssAccManagerId.Enabled = false;
            ddlAssPurDeptId.Enabled = false;
            ddlAssRecieveDeptId.Enabled = false;
            ddlAssSalDeptId.Enabled = false;
            ddlAssShipDeptId.Enabled = false;
            ddlAssAccManagerId.Visible = false;
            ddlAssPurDeptId.Visible = false;
            ddlAssRecieveDeptId.Visible = false;
            ddlAssSalDeptId.Visible = false;
            ddlAssShipDeptId.Visible = false;

            string qryStr = "SELECT     EmployeeMaster.EmployeeMasterID, EmployeeMaster.EmployeeName, EmployeeMaster.PartyID, User_master.Active " +
                " FROM         EmployeeMaster LEFT OUTER JOIN " +
                " User_master ON EmployeeMaster.PartyID = User_master.PartyID LEFT OUTER JOIN DepartmentmasterMNC on   " +
                "    DepartmentmasterMNC.Departmentid=User_master.Department " +
                "  where User_master.Active = 1 and  DepartmentmasterMNC.Companyid ='" + Session["Comid"] + "' order by EmployeeMaster.EmployeeName";

            ddlAssAccManagerId.DataSource = (DataTable)select(qryStr);
            fillddlOther(ddlAssAccManagerId, "EmployeeName", "EmployeeMasterID");
            ddlAssAccManagerId.Items.Insert(0, "All");
            ddlAssAccManagerId.Items[0].Value = "0";

            ddlAssPurDeptId.DataSource = (DataTable)select(qryStr);
            fillddlOther(ddlAssPurDeptId, "EmployeeName", "EmployeeMasterID");
            ddlAssPurDeptId.Items.Insert(0, "All");
            ddlAssPurDeptId.Items[0].Value = "0";

            ddlAssRecieveDeptId.DataSource = (DataTable)select(qryStr);
            fillddlOther(ddlAssRecieveDeptId, "EmployeeName", "EmployeeMasterID");
            ddlAssRecieveDeptId.Items.Insert(0, "All");
            ddlAssRecieveDeptId.Items[0].Value = "0";

            ddlAssSalDeptId.DataSource = (DataTable)select(qryStr);
            fillddlOther(ddlAssSalDeptId, "EmployeeName", "EmployeeMasterID");
            ddlAssSalDeptId.Items.Insert(0, "All");
            ddlAssSalDeptId.Items[0].Value = "0";

            ddlAssShipDeptId.DataSource = (DataTable)select(qryStr);
            fillddlOther(ddlAssShipDeptId, "EmployeeName", "EmployeeMasterID");
            ddlAssShipDeptId.Items.Insert(0, "All");
            ddlAssShipDeptId.Items[0].Value = "0";

            // }
        }
    }
    protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCity.SelectedIndex > 0)
        {
            ddlDirectDepositBankBranchcity.SelectedValue = ddlCity.SelectedValue;

            if (ddlState.SelectedIndex > 0)
            {
                if (ddlCountry.SelectedIndex > 0)
                {
                    string str = "SELECT     AssignedAccountManager, AssignedRecievingDept, AssignedPurchseDept, AssignedSalesDept, AssignedShippingDept " +
                         " FROM         PartyAutoAllocationManager " +
                         "where ((Country=" + ddlCountry.SelectedValue + ") And (State=" + ddlState.SelectedValue + ") And (City=" + ddlCity.SelectedValue + ") and (compid='" + Session["Comid"] + "')) ";
                    SqlCommand cmd = new SqlCommand(str, con);
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        ddlAssAccManagerId.SelectedIndex = ddlAssAccManagerId.Items.IndexOf(ddlAssAccManagerId.Items.FindByValue(dt.Rows[0]["AssignedAccountManager"].ToString()));
                        ddlAssPurDeptId.SelectedIndex = ddlAssPurDeptId.Items.IndexOf(ddlAssPurDeptId.Items.FindByValue(dt.Rows[0]["AssignedPurchseDept"].ToString()));
                        ddlAssRecieveDeptId.SelectedIndex = ddlAssRecieveDeptId.Items.IndexOf(ddlAssRecieveDeptId.Items.FindByValue(dt.Rows[0]["AssignedRecievingDept"].ToString()));
                        ddlAssSalDeptId.SelectedIndex = ddlAssSalDeptId.Items.IndexOf(ddlAssSalDeptId.Items.FindByValue(dt.Rows[0]["AssignedSalesDept"].ToString()));
                        ddlAssShipDeptId.SelectedIndex = ddlAssShipDeptId.Items.IndexOf(ddlAssShipDeptId.Items.FindByValue(dt.Rows[0]["AssignedShippingDept"].ToString()));

                    }
                    else
                    {
                        string str1 = "SELECT     AssignedAccountManager, AssignedRecievingDept, AssignedPurchseDept, AssignedSalesDept, AssignedShippingDept " +
                         " FROM         PartyAutoAllocationManager " +
                         "where ((Country=" + ddlCountry.SelectedValue + ") And (State=" + ddlState.SelectedValue + ") and (compid='" + Session["Comid"] + "')) ";
                        SqlCommand cmd1 = new SqlCommand(str1, con);
                        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                        DataTable dt1 = new DataTable();
                        adp1.Fill(dt1);
                        if (dt1.Rows.Count > 0)
                        {
                            ddlAssAccManagerId.SelectedIndex = ddlAssAccManagerId.Items.IndexOf(ddlAssAccManagerId.Items.FindByValue(dt1.Rows[0]["AssignedAccountManager"].ToString()));
                            ddlAssPurDeptId.SelectedIndex = ddlAssPurDeptId.Items.IndexOf(ddlAssPurDeptId.Items.FindByValue(dt1.Rows[0]["AssignedPurchseDept"].ToString()));
                            ddlAssRecieveDeptId.SelectedIndex = ddlAssRecieveDeptId.Items.IndexOf(ddlAssRecieveDeptId.Items.FindByValue(dt1.Rows[0]["AssignedRecievingDept"].ToString()));
                            ddlAssSalDeptId.SelectedIndex = ddlAssSalDeptId.Items.IndexOf(ddlAssSalDeptId.Items.FindByValue(dt1.Rows[0]["AssignedSalesDept"].ToString()));
                            ddlAssShipDeptId.SelectedIndex = ddlAssShipDeptId.Items.IndexOf(ddlAssShipDeptId.Items.FindByValue(dt1.Rows[0]["AssignedShippingDept"].ToString()));

                        }
                        else
                        {
                            string strcou = "SELECT     AssignedAccountManager, AssignedRecievingDept, AssignedPurchseDept, AssignedSalesDept, AssignedShippingDept " +
                         " FROM         PartyAutoAllocationManager " +
                         "where (Country=" + ddlCountry.SelectedValue + ") and (compid='" + Session["Comid"] + "')";
                            SqlCommand cmdco = new SqlCommand(strcou, con);
                            SqlDataAdapter adpco = new SqlDataAdapter(cmdco);
                            DataTable dtco = new DataTable();
                            adpco.Fill(dtco);
                            if (dtco.Rows.Count > 0)
                            {
                                ddlAssAccManagerId.SelectedIndex = ddlAssAccManagerId.Items.IndexOf(ddlAssAccManagerId.Items.FindByValue(dtco.Rows[0]["AssignedAccountManager"].ToString()));
                                ddlAssPurDeptId.SelectedIndex = ddlAssPurDeptId.Items.IndexOf(ddlAssPurDeptId.Items.FindByValue(dtco.Rows[0]["AssignedPurchseDept"].ToString()));
                                ddlAssRecieveDeptId.SelectedIndex = ddlAssRecieveDeptId.Items.IndexOf(ddlAssRecieveDeptId.Items.FindByValue(dtco.Rows[0]["AssignedRecievingDept"].ToString()));
                                ddlAssSalDeptId.SelectedIndex = ddlAssSalDeptId.Items.IndexOf(ddlAssSalDeptId.Items.FindByValue(dtco.Rows[0]["AssignedSalesDept"].ToString()));
                                ddlAssShipDeptId.SelectedIndex = ddlAssShipDeptId.Items.IndexOf(ddlAssShipDeptId.Items.FindByValue(dtco.Rows[0]["AssignedShippingDept"].ToString()));

                            }
                            else
                            {
                                string str12 = "SELECT     AssignedAccountManager, AssignedRecievingDept, AssignedPurchseDept, AssignedSalesDept, AssignedShippingDept " +
                         " FROM   PartyAutoAllocationManager " +
                         " where [All]='1' and compid='" + Session["Comid"] + "'";
                                SqlCommand cmd12 = new SqlCommand(str12, con);
                                SqlDataAdapter adp12 = new SqlDataAdapter(cmd12);
                                DataTable dt12 = new DataTable();
                                adp12.Fill(dt12);

                                if (dt12.Rows.Count > 0)
                                {
                                    ddlAssAccManagerId.SelectedIndex = ddlAssAccManagerId.Items.IndexOf(ddlAssAccManagerId.Items.FindByValue(dt12.Rows[0]["AssignedAccountManager"].ToString()));
                                    ddlAssPurDeptId.SelectedIndex = ddlAssPurDeptId.Items.IndexOf(ddlAssPurDeptId.Items.FindByValue(dt12.Rows[0]["AssignedPurchseDept"].ToString()));
                                    ddlAssRecieveDeptId.SelectedIndex = ddlAssRecieveDeptId.Items.IndexOf(ddlAssRecieveDeptId.Items.FindByValue(dt12.Rows[0]["AssignedRecievingDept"].ToString()));
                                    ddlAssSalDeptId.SelectedIndex = ddlAssSalDeptId.Items.IndexOf(ddlAssSalDeptId.Items.FindByValue(dt12.Rows[0]["AssignedSalesDept"].ToString()));
                                    ddlAssShipDeptId.SelectedIndex = ddlAssShipDeptId.Items.IndexOf(ddlAssShipDeptId.Items.FindByValue(dt12.Rows[0]["AssignedShippingDept"].ToString()));
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    protected void FillGridView1()
    {
        string str = "select  distinct WareHouseId as Id, WareHouseMaster.WareHouseId,WareHouseMaster.Name, CASE  When (WareHouseId IS NULL)  Then Cast( '0' as bit) ELSE Cast( '0' as bit) end AS AccessAllowed from WareHouseMaster where comid='" + Session["Comid"] + "' and Status='1' order by Name";
        SqlCommand cmdfillgrid = new SqlCommand(str, con);
        SqlDataAdapter adpfillgrid = new SqlDataAdapter(cmdfillgrid);
        DataTable dtfill = new DataTable();
        adpfillgrid.Fill(dtfill);
        gridAccess.DataSource = dtfill;
        gridAccess.DataBind();

    }
    protected void tbUserName_TextChanged(object sender, EventArgs e)
    {
        string str = " select * from User_master where Username='" + tbUserName.Text + "'";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);

        if (ds.Rows.Count > 0)
        {
            lblusernameavailableornot.Visible = true;
            lblusernameavailableornot.Text = "This Username is already used.";
        }
        else
        {
            lblusernameavailableornot.Visible = true;
            lblusernameavailableornot.Text = "Username Available";
        }
    }


    public bool ext(string filename)
    {
        string[] validFileTypes = { "bmp", "gif", "png", "jpg", "jpeg", "ipg", "BMP", "GIF", "PNG", "JPG", "JPEG", "IPG", "JFIF", "jfif", "TIFF", "tiff", "WEBP", "webp" };

        string ext = System.IO.Path.GetExtension(filename);

        bool isValidFile = false;

        for (int i = 0; i < validFileTypes.Length; i++)
        {

            if (ext == "." + validFileTypes[i])
            {

                isValidFile = true;

                break;

            }

        }
        return isValidFile;
    }

    //protected void btnupload_Click(object sender, EventArgs e)
    //{

    //    //string filename = "";
    //    //filename = FileUpload1.FileName;

    ////    string path = "images/" + FileUpload1.FileName;

    ////    FileUpload1.SaveAs(Server.MapPath(path));

    ////    //filename = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "_" + FileUpload1.FileName;
    //// //   filename = FileUpload1.PostedFile.FileName;
    //// //   string path = Server.MapPath(FileUpload1.PostedFile.FileName);
    //////    string path = Server.MapPath("~/Account/" + Session["Comid"] + "/EmpPhoto/") + filename;
    //////    FileUpload1.SaveAs(path);

    //// //   imgLogo.Visible = true;
    ////    imgLogo.ImageUrl = "~/" + path;
    // //   imgLogo.ImageUrl = "~/Account/" + Session["Comid"] + "/EmpPhoto/" + filename;


    //    FileUpload1.PostedFile.SaveAs(Server.MapPath("~\\ShoppingCart\\images\\") + FileUpload1.FileName);
    //    string logoname = FileUpload1.FileName.ToString();

    //    imgLogo.ImageUrl = "~/ShoppingCart/images/" + FileUpload1.FileName.ToString();


    //}



    protected void imgBtnImageUpdate_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {

            bool valid = ext(FileUpload1.FileName);
            if (valid == true)
            {
                FileUpload1.PostedFile.SaveAs(Server.MapPath("~\\ShoppingCart\\images\\") + FileUpload1.FileName);
                string logoname = FileUpload1.FileName.ToString();


                //LoadData();             


                imgLogo.ImageUrl = "~/ShoppingCart/images/" + FileUpload1.FileName.ToString();
                //pnllogo.Visible = true;
                //Panel1.Visible = false;
                //lblmsg.Text = "Record updated successfully";
                Session["phofile"] = FileUpload1.FileName.ToString();
            }
            else
            {
                lblmsg.Text = "Invalid File Type. Please upload an image file in one of the following formats: bmp, gif, png, jpg, jpeg, ipg, jfif, tiff, webp";
            }

        }

    }

    protected void fillstatusgrid()
    {
        ddlstatuscategory.Items.Clear();

        string qryStr = " SELECT * FROM StatusCategory WHERE compid = '" + Session["Comid"] + "' and StatusCategory='Employee Category' order by StatusCategory ";

        SqlCommand cmd = new SqlCommand(qryStr, con);
        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adpt.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            ddlstatuscategory.DataSource = dt;
            ddlstatuscategory.DataTextField = "StatusCategory";
            ddlstatuscategory.DataValueField = "StatusCategoryMasterId";
            ddlstatuscategory.DataBind();
        }
        //ddlstatuscategory.Items.Insert(0, "-Select-");
        //ddlstatuscategory.Items[0].Value = "0";

    }
    protected void fillstatus()
    {
        ddlstatusname.Items.Clear();
        string str = "Select * from StatusMaster where StatusCategoryMasterId = '" + ddlstatuscategory.SelectedValue + "' order by StatusName";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ddlstatusname.DataSource = dt;
            ddlstatusname.DataTextField = "StatusName";
            ddlstatusname.DataValueField = "StatusId";
            ddlstatusname.DataBind();
        }
        //ddlstatusname.Items.Insert(0, "-Select-");
        //ddlstatusname.Items[0].Value = "0";
    }
    protected void chkaccessright_CheckedChanged(object sender, EventArgs e)
    {
        if (chkaccessright.Checked == true)
        {
            pnlaccess.Visible = true;
        }
        else
        {
            pnlaccess.Visible = false;
        }
    }
    //protected void chkemplogin_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (chkemplogin.Checked == true)
    //    {
    //        pnlemployeedate.Visible = true;
    //    }
    //    else
    //    {
    //        pnlemployeedate.Visible = false;
    //    }
    //}
    protected void chkemppayroll_CheckedChanged(object sender, EventArgs e)
    {
        if (chkemppayroll.Checked == true)
        {
            Pnlemppayroll.Visible = true;
        }
        else
        {
            Pnlemppayroll.Visible = false;
        }
    }
    protected void chkempbarcode_CheckedChanged(object sender, EventArgs e)
    {
        if (chkempbarcode.Checked == true)
        {
            pnlempbarcode.Visible = true;

        }
        else
        {
            pnlempbarcode.Visible = true;
        }
    }
    protected void acccc(string accgenid)
    {
        //int act = Convert.ToInt32(accgenid) + 1;
        DataTable dtrs = select("select AccountId from AccountMaster where AccountId='" + accgenid + "' and Whid='" + ddlwarehouse.SelectedValue + "'");
        if (dtrs.Rows.Count > 0)
        {
            accid = Convert.ToString(Convert.ToInt32(accid) + 1);
            acccc(accid);
        }

    }
    protected void groupclass()
    {
        if (ddlPartyType.SelectedItem.Text == "Vendor")
        {
            groupid = 15;

            DataTable dtt = new DataTable();
            dtt = (DataTable)select("Select Max(AccountId) as aid from AccountMaster where GroupId='15' and Whid='" + ddlwarehouse.SelectedValue + "'");
            if (dtt.Rows.Count > 0)
            {

                accid = dtt.Rows[0]["aid"].ToString();
                acccc(dtt.Rows[0]["aid"].ToString());
                int gid = Convert.ToInt32(accid) + 1;
                accid = gid.ToString();
            }
            else
            {
                accid = Convert.ToString(30000);
                acccc(accid);
            }

        }
        else if (ddlPartyType.SelectedItem.Text == "Customer")
        {
            groupid = 2;
            DataTable dtt = new DataTable();
            dtt = (DataTable)select("Select Max(AccountId) as aid from AccountMaster where GroupId='2' and Whid='" + ddlwarehouse.SelectedValue + "'");
            if (dtt.Rows.Count > 0)
            {
                accid = dtt.Rows[0]["aid"].ToString();
                acccc(dtt.Rows[0]["aid"].ToString());

                if (Convert.ToInt32(accid) >= 29999)
                {
                    if (Convert.ToInt32(accid) >= 100000)
                    {
                        accid = (Convert.ToInt32(accid) + 1).ToString();
                    }
                    else
                    {
                        accid = "100000";
                        acccc(accid);
                    }
                }
                else
                {
                    if (Convert.ToInt32(accid) == 0)
                    {
                        accid = "10000";
                        acccc(accid);
                    }
                    else
                    {
                        accid = (Convert.ToInt32(accid) + 1).ToString();
                    }

                }
            }
            else
            {
                accid = Convert.ToString(10000);
                acccc(accid);
            }

        }
        else if (ddlPartyType.SelectedItem.Text == "Employee")
        {
            groupid = 15;
            DataTable dtt = new DataTable();
            dtt = (DataTable)select("Select Max(AccountId) as aid from AccountMaster where GroupId='15' and Whid='" + ddlwarehouse.SelectedValue + "'");
            if (dtt.Rows.Count > 0)
            {

                accid = dtt.Rows[0]["aid"].ToString();
                acccc(dtt.Rows[0]["aid"].ToString());
                int gid = Convert.ToInt32(accid) + 1;

            }
            else
            {
                accid = Convert.ToString(30000);
                acccc(accid);
            }
        }
        else if (ddlPartyType.SelectedItem.Text == "Other")
        {
            groupid = 15;
            DataTable dtt = new DataTable();
            dtt = (DataTable)select("Select Max(AccountId) as aid from AccountMaster where GroupId='15' and Whid='" + ddlwarehouse.SelectedValue + "'");
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
        }
        else if (ddlPartyType.SelectedItem.Text == "Credit Card Company")
        {
            groupid = 20;
            DataTable dtt = new DataTable();
            dtt = (DataTable)select("Select Max(AccountId) as aid from AccountMaster where GroupId='20' and Whid='" + ddlwarehouse.SelectedValue + "'");
            if (dtt.Rows.Count > 0)
            {
                if (dtt.Rows[0]["aid"].ToString() != "")
                {
                    if (Convert.ToInt32(dtt.Rows[0]["aid"].ToString()) >= 3999)
                    {
                        accid = Convert.ToString(33000);
                    }
                    else
                    {
                        int gid = Convert.ToInt32(dtt.Rows[0]["aid"].ToString()) + 1;
                        accid = Convert.ToString(gid);
                    }
                }
                else
                {
                    accid = Convert.ToString(3300);
                }
            }
        }
        else if (ddlPartyType.SelectedItem.Text == "Employee - CustomerSupport")
        {
            groupid = 15;
            DataTable dtt = new DataTable();
            dtt = (DataTable)select("Select Max(AccountId) as aid from AccountMaster where GroupId='15' and Whid='" + ddlwarehouse.SelectedValue + "'");
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
        }
        else if (ddlPartyType.SelectedItem.Text == "Admin")
        {
            groupid = 15;
            DataTable dtt = new DataTable();
            dtt = (DataTable)select("Select Max(AccountId) as aid from AccountMaster where GroupId='15' and Whid='" + ddlwarehouse.SelectedValue + "'");
            if (dtt.Rows.Count > 0)
            {

                accid = dtt.Rows[0]["aid"].ToString();
                acccc(dtt.Rows[0]["aid"].ToString());
                int gid = Convert.ToInt32(accid) + 1;

            }
            else
            {
                accid = Convert.ToString(30000);
                acccc(accid);
            }
        }
        else if (ddlPartyType.SelectedItem.Text == "Employee - Sale")
        {
            groupid = 15;
            DataTable dtt = new DataTable();
            dtt = (DataTable)select("Select Max(AccountId) as aid from AccountMaster where GroupId='15' and Whid='" + ddlwarehouse.SelectedValue + "'");
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
        }
        else if (ddlPartyType.SelectedItem.Text == "Employee - OnlineManagement")
        {
            groupid = 15;
            DataTable dtt = new DataTable();
            dtt = (DataTable)select("Select Max(AccountId) as aid from AccountMaster where GroupId='15' and Whid='" + ddlwarehouse.SelectedValue + "'");
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
        }
        else if (ddlPartyType.SelectedItem.Text == "Employee - OnlinetechSupport")
        {
            groupid = 15;
            DataTable dtt = new DataTable();
            dtt = (DataTable)select("Select Max(AccountId) as aid from AccountMaster where GroupId='15' and Whid='" + ddlwarehouse.SelectedValue + "'");
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
        }
        else if (ddlPartyType.SelectedItem.Text == "Employee - Warehouse")
        {
            groupid = 15;
            DataTable dtt = new DataTable();
            dtt = (DataTable)select("Select Max(AccountId) as aid from AccountMaster where GroupId='15' and Whid='" + ddlwarehouse.SelectedValue + "'");
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
        }
        else if (ddlPartyType.SelectedItem.Text == "Credit Card Company")
        {
            groupid = 5;
            DataTable dtt = new DataTable();
            dtt = (DataTable)select("Select Max(AccountId) as aid from AccountMaster where GroupId='5' and Whid='" + ddlwarehouse.SelectedValue + "'");
            if (dtt.Rows.Count > 0)
            {
                if (dtt.Rows[0]["aid"].ToString() != "")
                {
                    if (Convert.ToInt32(dtt.Rows[0]["aid"].ToString()) >= 1999)
                    {
                        accid = Convert.ToString(17000);
                    }
                    else
                    {
                        int gid = Convert.ToInt32(dtt.Rows[0]["aid"].ToString()) + 1;
                        accid = Convert.ToString(gid);
                    }
                }
                else
                {
                    accid = Convert.ToString(1700);
                }
            }
        }
        else if (ddlPartyType.SelectedItem.Text == "client")
        {
            groupid = 2;
            DataTable dtt = new DataTable();
            dtt = (DataTable)select("Select Max(AccountId) as aid from AccountMaster where GroupId='2' and Whid='" + ddlwarehouse.SelectedValue + "'");
            if (dtt.Rows.Count > 0)
            {
                if (dtt.Rows[0]["aid"].ToString() != "")
                {
                    if (Convert.ToInt32(dtt.Rows[0]["aid"].ToString()) >= 29999)
                    {
                        accid = Convert.ToString(100000);
                    }
                    else
                    {
                        int gid = Convert.ToInt32(dtt.Rows[0]["aid"].ToString()) + 1;
                        accid = Convert.ToString(gid);
                    }
                }
                else
                {
                    accid = Convert.ToString(10000);
                }
            }
        }
        //}


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


    protected decimal salcal(DataTable dt)
    {
        decimal amt = 0;
        string time1 = "";
        string outdifftime1 = "";

        string time2 = "";
        string outdifftime2 = "";

        string time3 = "";
        string outdifftime3 = "";


        string temp1 = "";
        string temp2 = "";

        string temp3 = "";
        string temp4 = "";

        string temp5 = "";
        string temp6 = "";
        if (dt.Rows.Count > 0)
        {
            TimeSpan stime = TimeSpan.Parse(dt.Rows[0]["StartTime"].ToString());
            TimeSpan etime = TimeSpan.Parse(dt.Rows[0]["EndTime"].ToString());
            TimeSpan fbreakstarttime;
            TimeSpan fbreakendtime;
            TimeSpan sbreakstarttime;
            TimeSpan sbreakendtime;
            if (Convert.ToString(dt.Rows[0]["FirstBreakStartTime"]) != "" && Convert.ToString(dt.Rows[0]["FirstBreakEndTime"]) != "")
            {
                fbreakstarttime = TimeSpan.Parse(Convert.ToString(dt.Rows[0]["FirstBreakStartTime"]));
                fbreakendtime = TimeSpan.Parse(Convert.ToString(dt.Rows[0]["FirstBreakEndTime"]));
            }
            else
            {
                fbreakstarttime = TimeSpan.Parse("00:00");
                fbreakendtime = TimeSpan.Parse("00:00");
            }
            if (Convert.ToString(dt.Rows[0]["SecondBreakStartTime"]) != "" && Convert.ToString(dt.Rows[0]["SecondBreakEndTime"]) != "")
            {
                sbreakstarttime = TimeSpan.Parse(Convert.ToString(dt.Rows[0]["FirstBreakStartTime"]));
                sbreakendtime = TimeSpan.Parse(Convert.ToString(dt.Rows[0]["FirstBreakEndTime"]));
            }
            else
            {
                sbreakstarttime = TimeSpan.Parse("00:00");
                sbreakendtime = TimeSpan.Parse("00:00");
            }
            time1 = etime.Subtract(stime).ToString();

            outdifftime1 = Convert.ToDateTime(time1).ToString("HH:MM");

            temp1 = Convert.ToDateTime(time1).ToString("HH");
            temp2 = Convert.ToDateTime(time1).ToString("mm");

            double main1 = Convert.ToDouble(temp1);
            double main2 = Convert.ToDouble(temp2);


            time2 = fbreakendtime.Subtract(fbreakstarttime).ToString();
            outdifftime2 = Convert.ToDateTime(time2).ToString("HH:mm");

            temp3 = Convert.ToDateTime(time2).ToString("HH");
            temp4 = Convert.ToDateTime(time2).ToString("mm");
            double main3 = Convert.ToDouble(temp3);
            double main4 = Convert.ToDouble(temp4);

            time3 = sbreakendtime.Subtract(sbreakstarttime).ToString();

            outdifftime3 = Convert.ToDateTime(time3).ToString("HH:mm");



            temp5 = Convert.ToDateTime(time3).ToString("HH");
            temp6 = Convert.ToDateTime(time3).ToString("mm");

            double main5 = Convert.ToDouble(temp5);
            double main6 = Convert.ToDouble(temp6);


            TimeSpan C1 = TimeSpan.Parse(time1);
            TimeSpan C2 = TimeSpan.Parse(time2);
            TimeSpan C3 = TimeSpan.Parse(time3);

            string diff1 = C3.Add(C2).ToString();

            TimeSpan C4 = TimeSpan.Parse(diff1);

            string Finalcal = C1.Subtract(C4).ToString();
            string finalvalue = Convert.ToDateTime(Finalcal).ToString("HH:mm");
            string lblnoofhour = finalvalue;


            string[] separbm = new string[] { ":" };
            string[] strSplitArrbm = lblnoofhour.Split(separbm, StringSplitOptions.RemoveEmptyEntries);
            amt = ((Convert.ToDecimal(strSplitArrbm[0]) + (Convert.ToDecimal(strSplitArrbm[1]) / 60)));



        }
        return amt;
    }

    protected decimal EmpAvg()
    {
        int workday = 0;
        decimal empsalamt = 0;
        if (ddlpaybleper.SelectedValue != "2")
        {
            DataTable ds123 = select("select * from BatchWorkingDay where BatchMasterId='" + ddlbatch.SelectedValue + "'  and Whid='" + ddlwarehouse.SelectedValue + "' ");

            if (ds123.Rows.Count > 0)
            {
                if (Convert.ToInt32(ds123.Rows[0]["Monday"]) == 1)
                {

                    DataTable ds1231 = select("select * from BatchTiming where Active='1' and BatchMasterId='" + ddlbatch.SelectedValue + "'  and TimeScheduleMasterId='" + ds123.Rows[0]["MondayScheduleId"] + "' ");

                    if (ds1231.Rows.Count > 0)
                    {
                        workday += 1;
                        empsalamt += salcal(ds1231);

                    }
                }
                if (Convert.ToInt32(ds123.Rows[0]["Tuesday"]) == 1)
                {

                    DataTable ds1231 = select("select * from BatchTiming where Active='1' and BatchMasterId='" + ddlbatch.SelectedValue + "'  and TimeScheduleMasterId='" + ds123.Rows[0]["TuesdayScheduleId"] + "' ");

                    if (ds1231.Rows.Count > 0)
                    {
                        workday += 1;
                        empsalamt += salcal(ds1231);

                    }
                }
                if (Convert.ToInt32(ds123.Rows[0]["Wednesday"]) == 1)
                {

                    DataTable ds1231 = select("select * from BatchTiming where Active='1' and BatchMasterId='" + ddlbatch.SelectedValue + "'  and TimeScheduleMasterId='" + ds123.Rows[0]["WednesdayScheduleId"] + "' ");

                    if (ds1231.Rows.Count > 0)
                    {
                        workday += 1;
                        empsalamt += salcal(ds1231);

                    }
                }
                if (Convert.ToInt32(ds123.Rows[0]["Thursday"]) == 1)
                {

                    DataTable ds1231 = select("select * from BatchTiming where Active='1' and BatchMasterId='" + ddlbatch.SelectedValue + "'  and TimeScheduleMasterId='" + ds123.Rows[0]["ThursdayScheduleId"] + "' ");

                    if (ds1231.Rows.Count > 0)
                    {
                        workday += 1;
                        empsalamt += salcal(ds1231);

                    }
                }
                if (Convert.ToInt32(ds123.Rows[0]["Friday"]) == 1)
                {

                    DataTable ds1231 = select("select * from BatchTiming where Active='1' and BatchMasterId='" + ddlbatch.SelectedValue + "'  and TimeScheduleMasterId='" + ds123.Rows[0]["FridayScheduleId"] + "' ");

                    if (ds1231.Rows.Count > 0)
                    {
                        workday += 1;
                        empsalamt += salcal(ds1231);

                    }
                }
                if (Convert.ToInt32(ds123.Rows[0]["Saturday"]) == 1)
                {
                    DataTable ds1231 = select("select * from BatchTiming where Active='1' and BatchMasterId='" + ddlbatch.SelectedValue + "'  and TimeScheduleMasterId='" + ds123.Rows[0]["SaturdayScheduleId"] + "' ");

                    if (ds1231.Rows.Count > 0)
                    {
                        workday += 1;
                        empsalamt += salcal(ds1231);

                    }
                }
                if (Convert.ToInt32(ds123.Rows[0]["Sunday"]) == 1)
                {
                    DataTable ds1231 = select("select * from BatchTiming where Active='1' and BatchMasterId='" + ddlbatch.SelectedValue + "'  and TimeScheduleMasterId='" + ds123.Rows[0]["SundayScheduleId"] + "' ");

                    if (ds1231.Rows.Count > 0)
                    {
                        workday += 1;
                        empsalamt += salcal(ds1231);

                    }
                }
            }
        }
        else
        {
            empsalamt = Convert.ToDecimal(txtamount.Text);
        }
        if (empsalamt > 0)
        {
            if (ddlpaybleper.SelectedValue == "3")
            {
                decimal dayhour = empsalamt / workday;
                empsalamt = Convert.ToDecimal(txtamount.Text) / dayhour;
            }
            if (ddlpaybleper.SelectedValue == "4")
            {
                decimal dayhour = empsalamt;
                empsalamt = Convert.ToDecimal(txtamount.Text) / dayhour;
            }

            else if (ddlpaybleper.SelectedValue == "7")
            {
                decimal caldata = Convert.ToDecimal(txtamount.Text) * 12 / 52;
                decimal dayhour = empsalamt;
                empsalamt = Convert.ToDecimal(caldata) / dayhour;

            }

            empsalamt = Math.Round(empsalamt, 2);
        }
        else
        {
            empsalamt = 0;
        }
        return empsalamt;
    }



    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (RadioButtonList2.SelectedValue == "0")
        {
            ViewState["tbPassword"] = tbPassword.Text;
        }
         string strgetusername = "select max(PartyID) as PartyID from Party_master ";
        SqlCommand cmdusername = new SqlCommand(strgetusername, con);
        SqlDataAdapter adpusername = new SqlDataAdapter(cmdusername);
        DataTable dsusername = new DataTable();
        adpusername.Fill(dsusername);

        if (tbEmail.Text != "" && RadioButtonList2.SelectedValue == "1")
        {
            pnlemployeedate.Visible = false;

            string username;
            string Password;

            int i = 0;

            for (i = 0; ; i++)
            {
                username = txtfirstname.Text + dsusername.Rows[0]["PartyID"].ToString() + i;
                Password = "Employee" + dsusername.Rows[0]["PartyID"].ToString() + "++" + i;

                string strusercheck = " select * from User_master where Username='" + username + "'";
                SqlCommand cmdusercheck = new SqlCommand(strusercheck, con);
                SqlDataAdapter adpusercheck = new SqlDataAdapter(cmdusercheck);
                DataTable dsusercheck = new DataTable();
                adpusercheck.Fill(dsusercheck);

                if (dsusercheck.Rows.Count > 0)
                {

                }
                else
                {
                    username = txtfirstname.Text + dsusername.Rows[0]["PartyID"].ToString() + i;
                    Password = "Employee" + dsusername.Rows[0]["PartyID"].ToString() + "++" + i;
                    break;
                }

            }
            tbUserName.Text = username;
            tbPassword.Text = Password;
        }


        if (tbEmail.Text != "" && RadioButtonList2.SelectedValue == "1")
        {
            string usercode;

            int i = 0;


            for (i = 0; ; i++)
            {

                usercode = dsusername.Rows[0]["PartyID"].ToString() + i;

                string strusercheck = " select * from EmployeeBarcodeMaster inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID=EmployeeBarcodeMaster.Employee_Id inner join Party_master on Party_master.PartyID=EmployeeMaster.PartyID where EmployeeBarcodeMaster.Employeecode='" + usercode + "' and Party_master.id='" + Session["comid"] + "'";
                SqlCommand cmdusercheck = new SqlCommand(strusercheck, con);
                SqlDataAdapter adpusercheck = new SqlDataAdapter(cmdusercheck);
                DataTable dsusercheck = new DataTable();
                adpusercheck.Fill(dsusercheck);

                if (dsusercheck.Rows.Count > 0)
                {


                }
                else
                {
                    usercode = txtfirstname.Text + dsusername.Rows[0]["PartyID"].ToString() + i;
                    break;
                }
            }
            // txtemployeecode.Text = usercode;
        }

        //string strusercodecheck = " select * from EmployeeBarcodeMaster inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID=EmployeeBarcodeMaster.Employee_Id inner join Party_master on Party_master.PartyID=EmployeeMaster.PartyID where EmployeeBarcodeMaster.Employeecode='" + txtsecuritycode.Text + "' and Party_master.id='" + Session["comid"] + "'";
        //SqlCommand cmdusercodecheck = new SqlCommand(strusercodecheck, con);
        //SqlDataAdapter adpusercodecheck = new SqlDataAdapter(cmdusercodecheck);
        //DataTable dsusercodecheck = new DataTable();
        //adpusercodecheck.Fill(dsusercodecheck);

        //if (dsusercodecheck.Rows.Count > 0)
        //{
        //    //lblmsg.Visible = true;
        //    //lblmsg.Text = "This employee code is already used.";
        //}

        //else
        //{
        int flag = 0;
        string strusernaemchk = " select * from User_master where Username='" + tbUserName.Text + "'";
        SqlCommand cmdusernaemchk = new SqlCommand(strusernaemchk, con);
        SqlDataAdapter adpusernaemchk = new SqlDataAdapter(cmdusernaemchk);
        DataTable dsusernaemchk = new DataTable();
        adpusernaemchk.Fill(dsusernaemchk);

        if (dsusernaemchk.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "This Username is already used.";
        }
        else
        {
           
            if (txtsecuritycode.Text != "")
            {
                string strbluetoothno = "Select * from EmployeeBarcodeMaster where Employeecode='" + txtsecuritycode.Text + "'";
                SqlCommand cmdbluetoothno = new SqlCommand(strbluetoothno, con);
                SqlDataAdapter adpbluetoothno = new SqlDataAdapter(cmdbluetoothno);
                DataTable dsbluetoothno = new DataTable();
                adpbluetoothno.Fill(dsbluetoothno);
                if (dsbluetoothno.Rows.Count > 0)
                {
                    flag = 1;
                    lblmsg.Visible = true;
                    lblmsg.Text = "This Security Code is already in use.";

                }
            }
            if (txtbarcode.Text != "")
            {
                string strbarcode = "Select * from EmployeeBarcodeMaster where Barcode='" + txtbarcode.Text + "'";
                SqlCommand cmdbarcode = new SqlCommand(strbarcode, con);
                SqlDataAdapter adpbarcode = new SqlDataAdapter(cmdbarcode);
                DataTable dsbarcode = new DataTable();
                adpbarcode.Fill(dsbarcode);
                if (dsbarcode.Rows.Count > 0)
                {
                    flag = 1;
                    lblmsg.Visible = true;
                    lblmsg.Text = "This Barcode is already in use.";

                }
            }
            if (flag == 0)
            {
                int acce = 0;
                if (RadioButtonList2.SelectedValue == "1" && tbEmail.Text == "")
                {
                    lblmsg.Text = "Please fill email address";
                    tbEmail.Focus();
                }
                else
                {
                    string str1 = "Select RoleId as DefaultRole from DesignationMaster where DesignationMasterId='" + ddldesignation.SelectedValue + "' ";
                    SqlCommand cmd1Role = new SqlCommand(str1, con);
                    SqlDataAdapter adp1 = new SqlDataAdapter(cmd1Role);
                    DataTable dty = new DataTable();
                    adp1.Fill(dty);
                    if (dty.Rows.Count > 0)
                    {
                        // dty.Rows[0]["DefaultRole"].ToString();
                    }
                    else
                    {
                        lblmsg.Visible = true;
                        lblmsg.Text = " No role Available for Selected Designetion ";
                        return;
                    }

                    string st789 = "select * from EmployeeMaster where  ClientId='" + Session["ClientId"].ToString() + "' and UserId='" + tbUserName.Text + "'   ";

                    SqlCommand cmd789 = new SqlCommand(st789, PageConn.licenseconn());
                    SqlDataAdapter ds789 = new SqlDataAdapter(cmd789);
                    DataTable dt789 = new DataTable();
                    ds789.Fill(dt789);
                    if (dt789.Rows.Count > 0 || dty.Rows.Count == 0)
                    {
                        lblmsg.Visible = true;
                        lblmsg.Text = "Sorry, This User Id Is Not Available";

                    }
                    else
                    {
                      //  DataTable dty = select("select DefaultRole from RoleMaster where Role_id='" + ddlemprole.SelectedValue + "'");
                       
                        string str111 = "select * from EmployeeMaster where Name='" + txtfirstname.Text + "'  and ClientId='" + Session["ClientId"].ToString() + "' and UserId='" + tbUserName.Text + "'   ";
                        SqlCommand cmd111 = new SqlCommand(str111, PageConn.licenseconn());
                        SqlDataAdapter da111 = new SqlDataAdapter(cmd111);
                        DataTable dt111 = new DataTable();
                        da111.Fill(dt111);
                        if (dt111.Rows.Count > 0)
                        {
                            lblmsg.Visible = true;
                            lblmsg.Text = "Record already exists";
                        }
                        else
                        {
                            //  SqlConnection con1 = PageConn.licenseconn();
                            string SubMenuInsert = "Insert Into EmployeeMaster (Name,FTPServerURL,FTPPort,FTPUserId,FTPPassword,SupervisorId,DesignationId,UserId,Password,Active,ClientId,PhoneNo,PhoneExtension,MobileNo,CountryId,StateId,City,Email,Zipcode,RoleId,EffectiveRate) values ('" + txtfirstname.Text + "','','','','" + PageMgmt.Encrypted(ViewState["tbPassword"].ToString()) + "','" + ddlsupervisor.SelectedValue + "','','" + tbUserName.Text + "','" + PageMgmt.Encrypted(ViewState["tbPassword"].ToString()) + "','" + ddlActive.SelectedValue + "','" + Session["comid"].ToString() + "','" + tbPhone.Text + "','" + workext.Text + "','" + tbExtension.Text + "','','','','" + tbEmail.Text + "','" + tbZipCode.Text + "','" + dty.Rows[0][0].ToString() + "','00.00')";
                            SqlCommand cmd = new SqlCommand(SubMenuInsert, con1);
                            con1.Open();
                            cmd.ExecuteNonQuery();
                            con1.Close();
                            string str2 = " select MAX(Id) as EmpID from EmployeeMaster where EmployeeMaster.ClientId='" + Session["ClientId"].ToString() + "'";

                            SqlCommand cmd2 = new SqlCommand(str2, con1);
                            SqlDataAdapter adp2 = new SqlDataAdapter(cmd2);
                            DataSet ds2 = new DataSet();
                             adp2.Fill(ds2);
                              ViewState["eId"] = ds2.Tables[0].Rows[0]["EmpID"].ToString(); 
                             int i = 0;
                                
                                string ClientInsert = "Insert Into clientLoginMaster (clientId,UserId,Password) values ('" + Session["ClientId"].ToString() + "','" + tbUserName.Text + "','" + PageMgmt.Encrypted(ViewState["tbPassword"].ToString()) + "') ";
                                SqlCommand cmd1123 = new SqlCommand(ClientInsert, con1);
                                con1.Open();
                                cmd1123.ExecuteNonQuery();
                                con1.Close();
                                con1.Close();

                                con1.Open();
                                string insertdatabase = "insert into User_Role (User_id,Role_id,ActiveDeactive)values(" + ViewState["eId"] + ",'" + dty.Rows[0][0].ToString()  + "','1')";
                                SqlCommand cmdRole = new SqlCommand(insertdatabase, con1);
                                cmdRole.ExecuteNonQuery();
                                con1.Close();

                    // SqlCommand cmdec = new SqlCommand("Select Email from Party_master where Email='" + tbEmail.Text + "'", con);
                    SqlCommand cmdec = new SqlCommand("Select Email from Party_master where PartyID='10000000' ", con);
                    SqlDataAdapter adpec = new SqlDataAdapter(cmdec);
                    DataTable dsec = new DataTable();
                    adpec.Fill(dsec);

                    if (dsec.Rows.Count == 0)
                    {

                        acce = 0;
                    }
                    else
                    {
                        if (tbEmail.Text.Length > 0)
                        {
                            acce = 2;

                        }
                        else
                        {
                            acce = 0;
                        }
                    }
                    #region acce
                    if (acce == 0)
                    {

                        SqlCommand cmd1 = new SqlCommand("SELECT Party_master.* FROM  Party_master inner join EmployeeMaster on EmployeeMaster.PartyID=Party_master.PartyID " +
                                     "where Party_master.Whid='" + ddlwarehouse.SelectedValue + "' and EmployeeName='" + txtlastname.Text + ' ' + txtfirstname.Text + ' ' + txtintialis.Text + "' ", con);
                        SqlDataAdapter adp = new SqlDataAdapter(cmd1);
                        DataTable ds = new DataTable();
                        adp.Fill(ds);

                        if (ds.Rows.Count > 0)
                        {
                            lblmsg.Visible = true;
                            lblmsg.Text = "Record already exist";
                        }
                        else
                        {

                            groupclass();


                            bool access = UserAccess.Usercon("Party_Master", lblpno.Text, "PartyId", "", "", "id", "Party_Master");
                            if (access == true)
                            {
                                string qryStr = " insert into AccountMaster(ClassId,AccountId,GroupId,AccountName,Description,Balance,Date,InventoryFlag,BalanceOfLastYear,DateTimeOfLastUpdatedBalance,Status,compid,Whid) " +
                                              " values ('" + classid + "','" + accid + "','" + groupid + "','" + txtfirstname.Text + "','New Party','0'," + System.DateTime.Now.ToShortDateString() + ",'0','0','" + System.DateTime.Now.ToShortDateString() + "','" + ddlActive.SelectedValue + "','" + Session["comid"].ToString() + "','" + ddlwarehouse.SelectedValue + "')";
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

                                string st153 = "select Report_Period_Id  from ReportPeriod where Compid='" + Session["Comid"].ToString() + "' and Whid='" + ddlwarehouse.SelectedValue + "' and Active='1'";
                                SqlCommand cmd153 = new SqlCommand(st153, con);
                                SqlDataAdapter adp153 = new SqlDataAdapter(cmd153);
                                DataTable ds153 = new DataTable();
                                adp153.Fill(ds153);
                                Session["reportid"] = ds153.Rows[0]["Report_Period_Id"].ToString();

                                string st1531 = "select Report_Period_Id from [ReportPeriod] where ReportPeriod.Report_Period_Id<'" + Session["reportid"] + "' and  Whid='" + ddlwarehouse.SelectedValue + "'  order by Report_Period_Id Desc";
                                SqlCommand cmd1531 = new SqlCommand(st1531, con);
                                SqlDataAdapter adp1531 = new SqlDataAdapter(cmd1531);
                                DataTable ds1531 = new DataTable();
                                adp1531.Fill(ds1531);
                                Session["reportid1"] = ds1531.Rows[0]["Report_Period_Id"].ToString();

                                string str4562 = "insert into AccountBalance(AccountMasterId,Balance,Report_Period_Id) values ('" + Session["maxaid"].ToString() + "','" + 0 + "','" + Session["reportid1"] + "')";
                                SqlCommand cmd4562 = new SqlCommand(str4562, con);
                                con.Open();
                                cmd4562.ExecuteNonQuery();
                                con.Close();

                                string str456 = "insert into AccountBalance(AccountMasterId,Balance,Report_Period_Id) values ('" + Session["maxaid"].ToString() + "','" + 0 + "','" + Session["reportid"].ToString() + "')";
                                SqlCommand cmd456 = new SqlCommand(str456, con);
                                con.Open();
                                cmd456.ExecuteNonQuery();
                                con.Close();

                                SqlDataAdapter ad = new SqlDataAdapter("select max(AccountBalanceLimitId) as balid from AccountBalanceLimit where Whid='" + ddlwarehouse.SelectedValue + "'", con);
                                DataSet ds112 = new DataSet();
                                ad.Fill(ds112);
                                if (ds112.Tables[0].Rows.Count > 0)
                                {
                                    ViewState["balid"] = ds112.Tables[0].Rows[0]["balid"].ToString();
                                }
                                string ins1 = "insert into Party_master(Account,Compname,Contactperson,Address,City,State,Country,Website,GSTno,Incometaxno,Email,Phoneno,DataopID, " +
                                " PartyTypeId,AssignedAccountManagerId,AssignedRecevingDepartmentInchargeId,AssignedPurchaseDepartmentInchargeId,AssignedShippingDepartmentInchargeId, " +
                                " AssignedSalesDepartmentIncharge,StatusMasterId,Fax,AccountnameID,AccountBalanceLimitId,id,Whid,PartyTypeCategoryNo,Zipcode) " +
                                " values ( '" + accid + "','" + txtlastname.Text + ' ' + txtfirstname.Text + ' ' + txtintialis.Text + "','', " +
                                "'" + tbAddress.Text + "','" + ddlCity.SelectedValue + "','" + ddlState.SelectedValue + "','" + ddlCountry.SelectedValue + "','', " +
                                " '' ,'','" + tbEmail.Text + "','" + tbPhone.Text + "','1', '" + ddlPartyType.SelectedValue + "' ,'" + ddlAssAccManagerId.SelectedValue + "' ,'" + ddlAssPurDeptId.SelectedValue + "', " +
                                " '" + ddlAssRecieveDeptId.SelectedValue + "' , '" + ddlAssSalDeptId.SelectedValue + "' , '" + ddlAssShipDeptId.SelectedValue + "' ,'" + ddlActive.SelectedValue + "' , '' ,'1','" + ViewState["balid"] + "','" + Session["comid"] + "','" + ddlwarehouse.SelectedValue + "','" + lblpno.Text + "','" + tbZipCode.Text + "')";
                                SqlCommand cmd3 = new SqlCommand(ins1, con);

                                con.Open();
                                cmd3.ExecuteNonQuery();
                                con.Close();

                                string sel = "select max(PartyID) as PartyID from Party_master where Id='" + Session["comid"].ToString() + "' and Whid='" + ddlwarehouse.SelectedValue + "'";
                                SqlCommand cmd5 = new SqlCommand(sel, con);
                                SqlDataAdapter da5 = new SqlDataAdapter(cmd5);
                                DataSet ds5 = new DataSet();
                                da5.Fill(ds5);
                                string phofile = "";

                                phofile = Convert.ToString(Session["phofile"]);


                                ViewState["partyidforemail"] = Convert.ToInt32(ds5.Tables[0].Rows[0]["PartyId"].ToString());

                                string ins6 = "insert into User_master(Name,Address ,City,State,Country,Phoneno,EmailID ,Username,Department,Accesslevel,PartyID,DesigantionMasterId,Photo,Active,Extention,zipcode) " +
                                                              "values ('" + txtlastname.Text + ' ' + txtfirstname.Text + ' ' + txtintialis.Text + "','" + tbAddress.Text + "','" + ddlCity.SelectedValue + "','" + ddlState.SelectedValue + "','" + ddlCountry.SelectedValue + "','" + tbPhone.Text + "','" + tbEmail.Text + "','" + tbUserName.Text + "','" + ddldept.SelectedValue + "','1','" + Convert.ToInt32(ds5.Tables[0].Rows[0]["PartyId"].ToString()) + "','" + ddldesignation.SelectedValue + "','" + phofile + "' ,'" + ddlActive.SelectedValue + "','" + tbExtension.Text + "','" + tbZipCode.Text + "')";
                                SqlCommand cmd6 = new SqlCommand(ins6, con);

                                con.Open();
                                cmd6.ExecuteNonQuery();
                                con.Close();

                                string sel11 = "select max(UserID) as UserID from User_master";
                                SqlCommand cmd10 = new SqlCommand(sel11, con);

                                SqlDataAdapter da10 = new SqlDataAdapter(cmd10);

                                DataSet ds10 = new DataSet();
                                da10.Fill(ds10);

                                ViewState["userid"] = ds10.Tables[0].Rows[0]["UserID"].ToString();

                                string ins7 = "insert into Login_master(UserID,username,password,department,accesslevel,deptid,accessid) values ('" + Convert.ToInt32(ds10.Tables[0].Rows[0]["UserID"]) + "','" + tbUserName.Text + "','" + ClsEncDesc.Encrypted(Convert.ToString(ViewState["tbPassword"])) + "','" + ddldept.SelectedValue + "','1','" + ddldesignation.SelectedValue + "','1')";
                                SqlCommand cmd9 = new SqlCommand(ins7, con);
                                cmd9.Connection = con;
                                con.Open();
                                cmd9.ExecuteNonQuery();
                                con.Close();

                                ViewState["username"] = tbUserName.Text;
                                ViewState["password"] = tbPassword.Text;
                                // ViewState["empcode"] = txtemployeecode.Text;

                                if (chksunday.Checked == true)
                                {
                                    foreach (GridViewRow item in grdstatuscategory.Rows)
                                    {
                                        Label lblstatusid = (Label)item.FindControl("lblstatusid");
                                        string inststatuscontrol = "insert into StatusControl(StatusMasterId,Datetime,UserMasterId) values ('" + lblstatusid.Text + "','" + DateTime.Now.ToShortDateString() + "','" + Convert.ToInt32(ds10.Tables[0].Rows[0]["UserID"]) + "')";
                                        SqlCommand cmdstaus = new SqlCommand(inststatuscontrol, con);
                                        cmdstaus.Connection = con;
                                        if (con.State.ToString() != "Open")
                                        {
                                            con.Open();
                                        }
                                        cmdstaus.ExecuteNonQuery();
                                        con.Close();

                                    }
                                }

                                string inststatuscontrol1 = "insert into StatusControl(StatusMasterId,Datetime,UserMasterId) values ('" + ddlstatusname.SelectedValue + "','" + DateTime.Now.ToShortDateString() + "','" + Convert.ToInt32(ds10.Tables[0].Rows[0]["UserID"]) + "')";
                                SqlCommand cmdstaus1 = new SqlCommand(inststatuscontrol1, con);
                                cmdstaus1.Connection = con;
                                if (con.State.ToString() != "Open")
                                {
                                    con.Open();
                                }
                                cmdstaus1.ExecuteNonQuery();
                                con.Close();


                                string instrole = "insert into User_Role(User_id,Role_id,ActiveDeactive) values ('" + Convert.ToInt32(ds10.Tables[0].Rows[0]["UserID"]) + "','" + ddlemprole.SelectedValue + "','1')";
                                SqlCommand cmdid = new SqlCommand(instrole, con);
                                cmdid.Connection = con;
                                con.Open();
                                cmdid.ExecuteNonQuery();
                                con.Close();

                                string str11 = "select max(UserID) as userid from Login_master";
                                SqlCommand cmd11 = new SqlCommand(str11, con);

                                SqlDataAdapter adp11 = new SqlDataAdapter(cmd11);
                                DataTable ds11 = new DataTable();
                                adp11.Fill(ds11);
                                int id = 0;
                                DataTable dtid = new DataTable();
                                dtid = (DataTable)select("Select Id from AccountMaster where AccountId='" + accid + "' and Whid='" + ddlwarehouse.SelectedValue + "'");
                                if (dtid.Rows.Count > 0)
                                {
                                    id = Convert.ToInt32(dtid.Rows[0]["Id"].ToString());
                                }

                                string stremp = "";

                                if (chkeduquali.Checked == true && chkspecialsub.Checked == false && chkjobposition.Checked == false)
                                {
                                    SqlCommand cmd112 = new SqlCommand("insert into AreaofStudiesTbl values('" + txteduquali.Text + "',0)", con);
                                    if (con.State.ToString() != "Open")
                                    {
                                        con.Open();
                                    }
                                    cmd112.ExecuteNonQuery();
                                    con.Close();

                                    SqlDataAdapter da1 = new SqlDataAdapter("select max(ID) as ID from AreaofStudiesTbl", con);
                                    DataTable dt1 = new DataTable();
                                    da1.Fill(dt1);

                                    if (dt1.Rows.Count > 0)
                                    {

                                        stremp = "Insert into EmployeeMaster(PartyID,DeptID,DesignationMasterId,StatusMasterId,EmployeeTypeId, " +
                                    " DateOfJoin,Address,CountryId,StateId,City,ContactNo,Email,AccountId,AccountNo,EmployeeName,Whid,Description,SuprviserId,Active,WorkPhone,WorkExt,WorkEmail,EducationqualificationID,SpecialSubjectID,yearofexperience,Jobpositionid,sex) values('" + Convert.ToInt32(ds5.Tables[0].Rows[0]["PartyId"].ToString()) + "','" + ddldept.SelectedValue + "', " +
                                    " '" + ddldesignation.SelectedValue + "','','" + ddlemptype.SelectedValue + "','" + txtjoindate.Text + "','" + tbAddress.Text + "', " +
                                        " '" + ddlCountry.SelectedValue + "','" + ddlState.SelectedValue + "','" + ddlCity.SelectedValue + "','" + tbPhone.Text + "','" + tbEmail.Text + "','" + id + "','" + accid + "','" + txtlastname.Text + ' ' + txtfirstname.Text + ' ' + txtintialis.Text + "','" + ddlwarehouse.SelectedValue + "','','" + ddlemp.SelectedValue + "','" + ddlActive.SelectedValue + "','" + workphone.Text + "','" + workext.Text + "','" + txtworkemail.Text + "','" + dt1.Rows[0]["ID"].ToString() + "','" + ddlspecialsub.SelectedValue + "','" + txtyearexpr.Text + "','" + ddljobposition.SelectedValue + "','" + Radiogender.SelectedValue + "')";

                                    }
                                }


                                else if (chkeduquali.Checked == false && chkspecialsub.Checked == true && chkjobposition.Checked == false)
                                {
                                    SqlCommand cmd13 = new SqlCommand("insert into SpecialisedSubjectTBL(SubjectName,Status,AreaofStudiesId,compid) values('" + txtspecialsub.Text + "',0,'" + ddleduquali.SelectedValue + "','" + Session["Comid"] + "')", con);
                                    if (con.State.ToString() != "Open")
                                    {
                                        con.Open();
                                    }
                                    cmd13.ExecuteNonQuery();
                                    con.Close();

                                    SqlDataAdapter da1 = new SqlDataAdapter("select max(ID) as ID from SpecialisedSubjectTBL", con);
                                    DataTable dt1 = new DataTable();
                                    da1.Fill(dt1);

                                    if (dt1.Rows.Count > 0)
                                    {

                                        stremp = "Insert into EmployeeMaster(PartyID,DeptID,DesignationMasterId,StatusMasterId,EmployeeTypeId, " +
                                    " DateOfJoin,Address,CountryId,StateId,City,ContactNo,Email,AccountId,AccountNo,EmployeeName,Whid,Description,SuprviserId,Active,WorkPhone,WorkExt,WorkEmail,EducationqualificationID,SpecialSubjectID,yearofexperience,Jobpositionid,sex) values('" + Convert.ToInt32(ds5.Tables[0].Rows[0]["PartyId"].ToString()) + "','" + ddldept.SelectedValue + "', " +
                                    " '" + ddldesignation.SelectedValue + "','','" + ddlemptype.SelectedValue + "','" + txtjoindate.Text + "','" + tbAddress.Text + "', " +
                                        " '" + ddlCountry.SelectedValue + "','" + ddlState.SelectedValue + "','" + ddlCity.SelectedValue + "','" + tbPhone.Text + "','" + tbEmail.Text + "','" + id + "','" + accid + "','" + txtlastname.Text + ' ' + txtfirstname.Text + ' ' + txtintialis.Text + "','" + ddlwarehouse.SelectedValue + "','','" + ddlemp.SelectedValue + "','" + ddlActive.SelectedValue + "','" + workphone.Text + "','" + workext.Text + "','" + txtworkemail.Text + "','" + ddleduquali.SelectedValue + "','" + dt1.Rows[0]["ID"].ToString() + "','" + txtyearexpr.Text + "','" + ddljobposition.SelectedValue + "','" + Radiogender.SelectedValue + "')";

                                    }
                                }

                                else if (chkeduquali.Checked == false && chkspecialsub.Checked == false && chkjobposition.Checked == true)
                                {
                                    SqlDataAdapter da11 = new SqlDataAdapter("select ID from VacancyTypeMaster where Name='Other'", con);
                                    DataTable dt11 = new DataTable();
                                    da11.Fill(dt11);

                                    SqlCommand cmd13 = new SqlCommand("insert into VacancyPositionTitleMaster values('" + dt11.Rows[0]["ID"].ToString() + "','" + txtjobposition.Text + "',0)", con);
                                    if (con.State.ToString() != "Open")
                                    {
                                        con.Open();
                                    }
                                    cmd13.ExecuteNonQuery();
                                    con.Close();

                                    SqlDataAdapter da1 = new SqlDataAdapter("select max(ID) as ID from VacancyPositionTitleMaster", con);
                                    DataTable dt1 = new DataTable();
                                    da1.Fill(dt1);

                                    if (dt1.Rows.Count > 0)
                                    {

                                        stremp = "Insert into EmployeeMaster(PartyID,DeptID,DesignationMasterId,StatusMasterId,EmployeeTypeId, " +
                                    " DateOfJoin,Address,CountryId,StateId,City,ContactNo,Email,AccountId,AccountNo,EmployeeName,Whid,Description,SuprviserId,Active,WorkPhone,WorkExt,WorkEmail,EducationqualificationID,SpecialSubjectID,yearofexperience,Jobpositionid,sex) values('" + Convert.ToInt32(ds5.Tables[0].Rows[0]["PartyId"].ToString()) + "','" + ddldept.SelectedValue + "', " +
                                    " '" + ddldesignation.SelectedValue + "','','" + ddlemptype.SelectedValue + "','" + txtjoindate.Text + "','" + tbAddress.Text + "', " +
                                        " '" + ddlCountry.SelectedValue + "','" + ddlState.SelectedValue + "','" + ddlCity.SelectedValue + "','" + tbPhone.Text + "','" + tbEmail.Text + "','" + id + "','" + accid + "','" + txtlastname.Text + ' ' + txtfirstname.Text + ' ' + txtintialis.Text + "','" + ddlwarehouse.SelectedValue + "','','" + ddlemp.SelectedValue + "','" + ddlActive.SelectedValue + "','" + workphone.Text + "','" + workext.Text + "','" + txtworkemail.Text + "','" + ddleduquali.SelectedValue + "','" + ddlspecialsub.SelectedValue + "','" + txtyearexpr.Text + "','" + dt1.Rows[0]["ID"].ToString() + "','" + Radiogender.SelectedValue + "')";

                                    }
                                }

                                else if (chkeduquali.Checked == true && chkspecialsub.Checked == true && chkjobposition.Checked == true)
                                {
                                    SqlDataAdapter da11 = new SqlDataAdapter("select ID from VacancyTypeMaster where Name='Other'", con);
                                    DataTable dt11 = new DataTable();
                                    da11.Fill(dt11);

                                    SqlCommand cmd13 = new SqlCommand("insert into VacancyPositionTitleMaster values('" + dt11.Rows[0]["ID"].ToString() + "','" + txtjobposition.Text + "',0)", con);
                                    if (con.State.ToString() != "Open")
                                    {
                                        con.Open();
                                    }
                                    cmd13.ExecuteNonQuery();
                                    con.Close();

                                    SqlDataAdapter da1 = new SqlDataAdapter("select max(ID) as ID from VacancyPositionTitleMaster", con);
                                    DataTable dt1 = new DataTable();
                                    da1.Fill(dt1);

                                    SqlCommand cmd100 = new SqlCommand("insert into AreaofStudiesTbl values('" + txteduquali.Text + "',0)", con);
                                    if (con.State.ToString() != "Open")
                                    {
                                        con.Open();
                                    }
                                    cmd100.ExecuteNonQuery();
                                    con.Close();

                                    SqlDataAdapter dass = new SqlDataAdapter("select max(ID) as ID from AreaofStudiesTbl", con);
                                    DataTable dtss = new DataTable();
                                    dass.Fill(dtss);

                                    SqlCommand cmd101 = new SqlCommand("insert into SpecialisedSubjectTBL(SubjectName,Status,AreaofStudiesId,compid) values('" + txtspecialsub.Text + "',0,'" + dtss.Rows[0]["ID"].ToString() + "','" + Session["Comid"] + "')", con);
                                    if (con.State.ToString() != "Open")
                                    {
                                        con.Open();
                                    }
                                    cmd101.ExecuteNonQuery();
                                    con.Close();

                                    SqlDataAdapter dass1 = new SqlDataAdapter("select max(ID) as ID from SpecialisedSubjectTBL", con);
                                    DataTable dtss1 = new DataTable();
                                    dass1.Fill(dtss1);



                                    //if (dt1.Rows.Count > 0)
                                    //{

                                    stremp = "Insert into EmployeeMaster(PartyID,DeptID,DesignationMasterId,StatusMasterId,EmployeeTypeId, " +
                                " DateOfJoin,Address,CountryId,StateId,City,ContactNo,Email,AccountId,AccountNo,EmployeeName,Whid,Description,SuprviserId,Active,WorkPhone,WorkExt,WorkEmail,EducationqualificationID,SpecialSubjectID,yearofexperience,Jobpositionid,sex) values('" + Convert.ToInt32(ds5.Tables[0].Rows[0]["PartyId"].ToString()) + "','" + ddldept.SelectedValue + "', " +
                                " '" + ddldesignation.SelectedValue + "','','" + ddlemptype.SelectedValue + "','" + txtjoindate.Text + "','" + tbAddress.Text + "', " +
                                    " '" + ddlCountry.SelectedValue + "','" + ddlState.SelectedValue + "','" + ddlCity.SelectedValue + "','" + tbPhone.Text + "','" + tbEmail.Text + "','" + id + "','" + accid + "','" + txtlastname.Text + ' ' + txtfirstname.Text + ' ' + txtintialis.Text + "','" + ddlwarehouse.SelectedValue + "','','" + ddlemp.SelectedValue + "','" + ddlActive.SelectedValue + "','" + workphone.Text + "','" + workext.Text + "','" + txtworkemail.Text + "','" + dtss.Rows[0]["ID"].ToString() + "','" + dtss1.Rows[0]["ID"].ToString() + "','" + txtyearexpr.Text + "','" + dt1.Rows[0]["ID"].ToString() + "','" + Radiogender.SelectedValue + "')";

                                    // }
                                }


                                else if (chkeduquali.Checked == true && chkspecialsub.Checked == true && chkjobposition.Checked == false)
                                {
                                    //SqlDataAdapter da11 = new SqlDataAdapter("select ID from VacancyTypeMaster where Name='Other'", con);
                                    //DataTable dt11 = new DataTable();
                                    //da11.Fill(dt11);

                                    //SqlCommand cmd1 = new SqlCommand("insert into VacancyPositionTitleMaster values('" + dt11.Rows[0]["ID"].ToString() + "','" + txtjobposition.Text + "',0)", con);
                                    //if (con.State.ToString() != "Open")
                                    //{
                                    //    con.Open();
                                    //}
                                    //cmd1.ExecuteNonQuery();
                                    //con.Close();

                                    //SqlDataAdapter da1 = new SqlDataAdapter("select max(ID) as ID from VacancyPositionTitleMaster", con);
                                    //DataTable dt1 = new DataTable();
                                    //da1.Fill(dt1);

                                    SqlCommand cmd100 = new SqlCommand("insert into AreaofStudiesTbl values('" + txteduquali.Text + "',0)", con);
                                    if (con.State.ToString() != "Open")
                                    {
                                        con.Open();
                                    }
                                    cmd100.ExecuteNonQuery();
                                    con.Close();

                                    SqlDataAdapter dass = new SqlDataAdapter("select max(ID) as ID from AreaofStudiesTbl", con);
                                    DataTable dtss = new DataTable();
                                    dass.Fill(dtss);

                                    SqlCommand cmd101 = new SqlCommand("insert into SpecialisedSubjectTBL(SubjectName,Status,AreaofStudiesId,compid) values('" + txtspecialsub.Text + "',0,'" + dtss.Rows[0]["ID"].ToString() + "','" + Session["Comid"] + "')", con);
                                    if (con.State.ToString() != "Open")
                                    {
                                        con.Open();
                                    }
                                    cmd101.ExecuteNonQuery();
                                    con.Close();

                                    SqlDataAdapter dass1 = new SqlDataAdapter("select max(ID) as ID from SpecialisedSubjectTBL", con);
                                    DataTable dtss1 = new DataTable();
                                    dass1.Fill(dtss1);

                                    //if (dt1.Rows.Count > 0)
                                    //{

                                    stremp = "Insert into EmployeeMaster(PartyID,DeptID,DesignationMasterId,StatusMasterId,EmployeeTypeId, " +
                                " DateOfJoin,Address,CountryId,StateId,City,ContactNo,Email,AccountId,AccountNo,EmployeeName,Whid,Description,SuprviserId,Active,WorkPhone,WorkExt,WorkEmail,EducationqualificationID,SpecialSubjectID,yearofexperience,Jobpositionid,sex) values('" + Convert.ToInt32(ds5.Tables[0].Rows[0]["PartyId"].ToString()) + "','" + ddldept.SelectedValue + "', " +
                                " '" + ddldesignation.SelectedValue + "','','" + ddlemptype.SelectedValue + "','" + txtjoindate.Text + "','" + tbAddress.Text + "', " +
                                    " '" + ddlCountry.SelectedValue + "','" + ddlState.SelectedValue + "','" + ddlCity.SelectedValue + "','" + tbPhone.Text + "','" + tbEmail.Text + "','" + id + "','" + accid + "','" + txtlastname.Text + ' ' + txtfirstname.Text + ' ' + txtintialis.Text + "','" + ddlwarehouse.SelectedValue + "','','" + ddlemp.SelectedValue + "','" + ddlActive.SelectedValue + "','" + workphone.Text + "','" + workext.Text + "','" + txtworkemail.Text + "','" + dtss.Rows[0]["ID"].ToString() + "','" + dtss1.Rows[0]["ID"].ToString() + "','" + txtyearexpr.Text + "','" + ddljobposition.SelectedValue + "','" + Radiogender.SelectedValue + "')";

                                    // }
                                }



                                else if (chkeduquali.Checked == false && chkspecialsub.Checked == true && chkjobposition.Checked == true)
                                {
                                    SqlDataAdapter da11 = new SqlDataAdapter("select ID from VacancyTypeMaster where Name='Other'", con);
                                    DataTable dt11 = new DataTable();
                                    da11.Fill(dt11);

                                    SqlCommand cmd13 = new SqlCommand("insert into VacancyPositionTitleMaster values('" + dt11.Rows[0]["ID"].ToString() + "','" + txtjobposition.Text + "',0)", con);
                                    if (con.State.ToString() != "Open")
                                    {
                                        con.Open();
                                    }
                                    cmd13.ExecuteNonQuery();
                                    con.Close();

                                    SqlDataAdapter da1 = new SqlDataAdapter("select max(ID) as ID from VacancyPositionTitleMaster", con);
                                    DataTable dt1 = new DataTable();
                                    da1.Fill(dt1);

                                    //SqlCommand cmd100 = new SqlCommand("insert into AreaofStudiesTbl values('" + txteduquali.Text + "',0)", con);
                                    //if (con.State.ToString() != "Open")
                                    //{
                                    //    con.Open();
                                    //}
                                    //cmd100.ExecuteNonQuery();
                                    //con.Close();

                                    //SqlDataAdapter dass = new SqlDataAdapter("select max(ID) as ID from AreaofStudiesTbl", con);
                                    //DataTable dtss = new DataTable();
                                    //dass.Fill(dtss);

                                    SqlCommand cmd101 = new SqlCommand("insert into SpecialisedSubjectTBL(SubjectName,Status,AreaofStudiesId,compid) values('" + txtspecialsub.Text + "',0,'" + ddleduquali.SelectedValue + "','" + Session["Comid"] + "')", con);
                                    if (con.State.ToString() != "Open")
                                    {
                                        con.Open();
                                    }
                                    cmd101.ExecuteNonQuery();
                                    con.Close();

                                    SqlDataAdapter dass1 = new SqlDataAdapter("select max(ID) as ID from SpecialisedSubjectTBL", con);
                                    DataTable dtss1 = new DataTable();
                                    dass1.Fill(dtss1);

                                    //if (dt1.Rows.Count > 0)
                                    //{

                                    stremp = "Insert into EmployeeMaster(PartyID,DeptID,DesignationMasterId,StatusMasterId,EmployeeTypeId, " +
                                " DateOfJoin,Address,CountryId,StateId,City,ContactNo,Email,AccountId,AccountNo,EmployeeName,Whid,Description,SuprviserId,Active,WorkPhone,WorkExt,WorkEmail,EducationqualificationID,SpecialSubjectID,yearofexperience,Jobpositionid,sex) values('" + Convert.ToInt32(ds5.Tables[0].Rows[0]["PartyId"].ToString()) + "','" + ddldept.SelectedValue + "', " +
                                " '" + ddldesignation.SelectedValue + "','','" + ddlemptype.SelectedValue + "','" + txtjoindate.Text + "','" + tbAddress.Text + "', " +
                                    " '" + ddlCountry.SelectedValue + "','" + ddlState.SelectedValue + "','" + ddlCity.SelectedValue + "','" + tbPhone.Text + "','" + tbEmail.Text + "','" + id + "','" + accid + "','" + txtlastname.Text + ' ' + txtfirstname.Text + ' ' + txtintialis.Text + "','" + ddlwarehouse.SelectedValue + "','','" + ddlemp.SelectedValue + "','" + ddlActive.SelectedValue + "','" + workphone.Text + "','" + workext.Text + "','" + txtworkemail.Text + "','" + ddleduquali.SelectedValue + "','" + dtss1.Rows[0]["ID"].ToString() + "','" + txtyearexpr.Text + "','" + dt1.Rows[0]["ID"].ToString() + "','" + Radiogender.SelectedValue + "')";

                                    // }
                                }


                                else if (chkeduquali.Checked == true && chkspecialsub.Checked == false && chkjobposition.Checked == true)
                                {
                                    SqlDataAdapter da11 = new SqlDataAdapter("select ID from VacancyTypeMaster where Name='Other'", con);
                                    DataTable dt11 = new DataTable();
                                    da11.Fill(dt11);

                                    SqlCommand cmd13 = new SqlCommand("insert into VacancyPositionTitleMaster values('" + dt11.Rows[0]["ID"].ToString() + "','" + txtjobposition.Text + "',0)", con);
                                    if (con.State.ToString() != "Open")
                                    {
                                        con.Open();
                                    }
                                    cmd13.ExecuteNonQuery();
                                    con.Close();

                                    SqlDataAdapter da1 = new SqlDataAdapter("select max(ID) as ID from VacancyPositionTitleMaster", con);
                                    DataTable dt1 = new DataTable();
                                    da1.Fill(dt1);

                                    SqlCommand cmd100 = new SqlCommand("insert into AreaofStudiesTbl values('" + txteduquali.Text + "',0)", con);
                                    if (con.State.ToString() != "Open")
                                    {
                                        con.Open();
                                    }
                                    cmd100.ExecuteNonQuery();
                                    con.Close();

                                    SqlDataAdapter dass = new SqlDataAdapter("select max(ID) as ID from AreaofStudiesTbl", con);
                                    DataTable dtss = new DataTable();
                                    dass.Fill(dtss);

                                    //SqlCommand cmd10 = new SqlCommand("insert into LevelofEducationTBL values('" + txtspecialsub.Text + "',0,'" + ddleduquali.SelectedValue + "')", con);
                                    //if (con.State.ToString() != "Open")
                                    //{
                                    //    con.Open();
                                    //}
                                    //cmd10.ExecuteNonQuery();
                                    //con.Close();

                                    //SqlDataAdapter dass1 = new SqlDataAdapter("select max(ID) as ID from LevelofEducationTBL", con);
                                    //DataTable dtss1 = new DataTable();
                                    //dass1.Fill(dtss1);

                                    //if (dt1.Rows.Count > 0)
                                    //{

                                    stremp = "Insert into EmployeeMaster(PartyID,DeptID,DesignationMasterId,StatusMasterId,EmployeeTypeId, " +
                                " DateOfJoin,Address,CountryId,StateId,City,ContactNo,Email,AccountId,AccountNo,EmployeeName,Whid,Description,SuprviserId,Active,WorkPhone,WorkExt,WorkEmail,EducationqualificationID,SpecialSubjectID,yearofexperience,Jobpositionid,sex) values('" + Convert.ToInt32(ds5.Tables[0].Rows[0]["PartyId"].ToString()) + "','" + ddldept.SelectedValue + "', " +
                                " '" + ddldesignation.SelectedValue + "','','" + ddlemptype.SelectedValue + "','" + txtjoindate.Text + "','" + tbAddress.Text + "', " +
                                    " '" + ddlCountry.SelectedValue + "','" + ddlState.SelectedValue + "','" + ddlCity.SelectedValue + "','" + tbPhone.Text + "','" + tbEmail.Text + "','" + id + "','" + accid + "','" + txtlastname.Text + ' ' + txtfirstname.Text + ' ' + txtintialis.Text + "','" + ddlwarehouse.SelectedValue + "','','" + ddlemp.SelectedValue + "','" + ddlActive.SelectedValue + "','" + workphone.Text + "','" + workext.Text + "','" + txtworkemail.Text + "','" + dtss.Rows[0]["ID"].ToString() + "','" + ddlspecialsub.SelectedValue + "','" + txtyearexpr.Text + "','" + dt1.Rows[0]["ID"].ToString() + "','" + Radiogender.SelectedValue + "')";

                                    // }
                                }


                                else
                                {

                                    stremp = "Insert into EmployeeMaster(PartyID,DeptID,DesignationMasterId,StatusMasterId,EmployeeTypeId, " +
                                        " DateOfJoin,Address,CountryId,StateId,City,ContactNo,Email,AccountId,AccountNo,EmployeeName,Whid,Description,SuprviserId,Active,WorkPhone,WorkExt,WorkEmail,EducationqualificationID,SpecialSubjectID,yearofexperience,Jobpositionid,sex) values('" + Convert.ToInt32(ds5.Tables[0].Rows[0]["PartyId"].ToString()) + "','" + ddldept.SelectedValue + "', " +
                                        " '" + ddldesignation.SelectedValue + "','','" + ddlemptype.SelectedValue + "','" + txtjoindate.Text + "','" + tbAddress.Text + "', " +
                                            " '" + ddlCountry.SelectedValue + "','" + ddlState.SelectedValue + "','" + ddlCity.SelectedValue + "','" + tbPhone.Text + "','" + tbEmail.Text + "','" + id + "','" + accid + "','" + txtlastname.Text + ' ' + txtfirstname.Text + ' ' + txtintialis.Text + "','" + ddlwarehouse.SelectedValue + "','','" + ddlemp.SelectedValue + "','" + ddlActive.SelectedValue + "','" + workphone.Text + "','" + workext.Text + "','" + txtworkemail.Text + "','" + ddleduquali.SelectedValue + "','" + ddlspecialsub.SelectedValue + "','" + txtyearexpr.Text + "','" + ddljobposition.SelectedValue + "','" + Radiogender.SelectedValue + "')";

                                }
                                SqlCommand cmdemp = new SqlCommand(stremp, con);
                                con.Open();
                                cmdemp.ExecuteNonQuery();
                                con.Close();


                                ViewState["PartyMasterId"] = ds5.Tables[0].Rows[0]["PartyId"].ToString();
                                Session["userid"] = ds11.Rows[0]["userid"].ToString();
                                Session["username"] = tbUserName.Text;
                                //insert partyaddresstbl
                                string partyaddress = "Insert into PartyAddressTbl(PartyMasterId,Address,Country,State,City,email,Phone,fax,zipcode,UserId,datetime,AddressActiveStatus) " +
                                    " values ('" + Convert.ToInt32(ds5.Tables[0].Rows[0]["PartyId"].ToString()) + "','" + tbAddress.Text + "','" + ddlCountry.SelectedValue + "','" + ddlState.SelectedValue + "','" + ddlCity.SelectedValue + "', " +
                                    " '" + tbEmail.Text + "','" + tbPhone.Text + "','','" + tbZipCode.Text + "','" + Session["userid"] + "','" + System.DateTime.Now.ToString() + "','1')";
                                SqlCommand cmdpartyaddress = new SqlCommand(partyaddress, con);
                                if (con.State.ToString() != "Open")
                                {
                                    con.Open();
                                }
                                cmdpartyaddress.ExecuteNonQuery();
                                con.Close();
                                //end 
                                //  Response.Redirect("Default.aspx");

                                string str121 = "select max(EmployeeMasterID) as EmployeeMasterID from EmployeeMaster";
                                SqlCommand cmd121 = new SqlCommand(str121, con);

                                SqlDataAdapter adp121 = new SqlDataAdapter(cmd121);
                                DataTable ds121 = new DataTable();
                                adp121.Fill(ds121);

                                if (ds121.Rows.Count > 0)
                                {
                                    ViewState["EmerEMID"] = ds121.Rows[0]["EmployeeMasterID"].ToString();

                                    if (chk111.Checked == true)
                                    {
                                        string te = "AddDocMaster.aspx?employeemm=" + ViewState["EmerEMID"].ToString() + "&storeid=" + ddlwarehouse.SelectedValue + "";
                                        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
                                    }


                                    if (Request.QueryString["Id"] == "1")
                                    {
                                        SqlCommand cmdemer = new SqlCommand("insert into EmployeeEmergencyContactTbl values('" + Session["Comid"].ToString() + "','" + ddlwarehouse.SelectedValue + "','" + ViewState["EmerEMID"] + "','','','','','','','','','','','','')", con);
                                        if (con.State.ToString() != "Open")
                                        {
                                            con.Open();
                                        }
                                        cmdemer.ExecuteNonQuery();
                                        con.Close();
                                    }
                                    else
                                    {
                                        SqlCommand cmdemer = new SqlCommand("insert into EmployeeEmergencyContactTbl values('" + Session["Comid"].ToString() + "','" + ddlwarehouse.SelectedValue + "','" + ViewState["EmerEMID"] + "','" + TextBox5.Text + "','" + TextBox7.Text + "','" + TextBox9.Text + "','" + TextBox11.Text + "','" + TextBox13.Text + "','" + TextBox15.Text + "','" + TextBox6.Text + "','" + TextBox8.Text + "','" + TextBox10.Text + "','" + TextBox12.Text + "','" + TextBox14.Text + "','" + TextBox16.Text + "')", con);
                                        if (con.State.ToString() != "Open")
                                        {
                                            con.Open();
                                        }
                                        cmdemer.ExecuteNonQuery();
                                        con.Close();
                                    }
                                }

                                if (RadioButtonList1.SelectedValue == "0")
                                {

                                    string strsalary = "insert into EmployeeSalaryMaster (EmployeeId,Remuneration_Id,Amount,PayablePer_PeriodMasterId,EffectiveStartDate,EffectiveEndDate,Whid,compid) values('" + ds121.Rows[0]["EmployeeMasterID"].ToString() + "','" + ddlremuneration.SelectedValue + "','" + txtamount.Text + "','" + ddlpaybleper.SelectedValue + "','" + txtjoindate.Text + "','" + dateafterthreeyear + "','" + ddlwarehouse.SelectedValue + "','" + Session["Comid"].ToString() + "')";
                                    SqlCommand cmdsalary = new SqlCommand(strsalary, con);
                                    if (con.State.ToString() != "Open")
                                    {
                                        con.Open();
                                    }
                                    cmdsalary.ExecuteNonQuery();
                                    con.Close();

                                    Decimal dr = EmpAvg();

                                    string strsalary1 = "insert into EmployeeAvgSalaryMaster values('" + ds121.Rows[0]["EmployeeMasterID"].ToString() + "','" + dr + "',0,'" + ddlwarehouse.SelectedValue + "','" + Session["Comid"].ToString() + "')";
                                    SqlCommand cmdsalary1 = new SqlCommand(strsalary1, con);
                                    if (con.State.ToString() != "Open")
                                    {
                                        con.Open();
                                    }
                                    cmdsalary1.ExecuteNonQuery();
                                    con.Close();
                                }

                                if (RadioButtonList1.SelectedValue == "1")
                                {
                                    Decimal dr = costcalcu();

                                    string strsalary1 = "insert into EmployeeAvgSalaryMaster values('" + ds121.Rows[0]["EmployeeMasterID"].ToString() + "','" + dr + "',0,'" + ddlwarehouse.SelectedValue + "','" + Session["Comid"].ToString() + "')";
                                    SqlCommand cmdsalary1 = new SqlCommand(strsalary1, con);
                                    if (con.State.ToString() != "Open")
                                    {
                                        con.Open();
                                    }
                                    cmdsalary1.ExecuteNonQuery();
                                    con.Close();
                                }


                                if (chkaccessright.Checked == true)
                                {
                                    for (int j = 0; j < gridAccess.Rows.Count; j++)
                                    {
                                        Label Whid = (Label)gridAccess.Rows[j].FindControl("lblWh");
                                        CheckBox chk = (CheckBox)(gridAccess.Rows[j].FindControl("ChkAess"));
                                        int ch = 0;
                                        if (chk.Checked)
                                        {
                                            ch = 1;
                                        }
                                        else
                                        {
                                            ch = 0;
                                        }
                                        string str = "Insert  into EmployeeWarehouseRights (EmployeeId,Whid,AccessAllowed)values('" + ds121.Rows[0]["EmployeeMasterID"] + "','" + Whid.Text + "','" + ch + "')";
                                        SqlCommand cmd13 = new SqlCommand(str, con);
                                        con.Open();
                                        cmd13.ExecuteNonQuery();
                                        con.Close();
                                    }
                                }
                                Int32 empid = Convert.ToInt32(ds121.Rows[0]["EmployeeMasterID"].ToString());

                                string insertbatch = "Insert into EmployeeBatchMaster(Whid,Employeeid,Batchmasterid) values ('" + ddlwarehouse.SelectedValue + "','" + empid + "','" + ddlbatch.SelectedValue + "')";
                                SqlCommand cmdbatch = new SqlCommand(insertbatch, con);
                                con.Open();
                                cmdbatch.ExecuteNonQuery();
                                con.Close();


                                SqlDataAdapter dalsde = new SqlDataAdapter("select Whid from employeemaster where employeemasterid='" + ViewState["EmerEMID"].ToString() + "'", con);
                                DataTable dtlsde = new DataTable();
                                dalsde.Fill(dtlsde);

                                if (dtlsde.Rows.Count > 0)
                                {
                                    ViewState["veve"] = Convert.ToString(dtlsde.Rows[0]["Whid"]);

                                    SqlDataAdapter dalsde1 = new SqlDataAdapter("select top(1) [ID],[BusinessID],[AccountID],[Rate],[DateCalculated] from OverheadAbsorbtionMasterTbl where BusinessID='" + ViewState["veve"].ToString() + "' order by ID desc", con);
                                    DataTable dtlsde1 = new DataTable();
                                    dalsde1.Fill(dtlsde1);

                                    if (dtlsde1.Rows.Count > 0)
                                    {
                                        SqlDataAdapter daf100 = new SqlDataAdapter("select [StartDate],[EndDate] from [ReportPeriod] where [Report_Period_Id]='" + Convert.ToString(dtlsde1.Rows[0]["AccountID"]) + "'", con);
                                        DataTable dtf100 = new DataTable();
                                        daf100.Fill(dtf100);

                                        if (dtf100.Rows.Count > 0)
                                        {
                                            ViewState["stdate"] = Convert.ToDateTime(dtf100.Rows[0]["StartDate"].ToString()).ToShortDateString();
                                            ViewState["endate"] = Convert.ToDateTime(dtf100.Rows[0]["EndDate"].ToString()).ToShortDateString();
                                        }

                                        SqlDataAdapter da1 = new SqlDataAdapter("select EmployeeName,EmployeeMasterID,EmployeeBatchMaster.Batchmasterid from EmployeeMaster inner join party_master on party_master.partyid=employeemaster.partyid inner join employeebatchmaster on  EmployeeMaster.EmployeeMasterID=EmployeeBatchMaster.Employeeid  where party_master.id='" + Session["Comid"].ToString() + "' and employeemaster.whid='" + Convert.ToString(dtlsde1.Rows[0]["BusinessID"]) + "' and employeemaster.Active='1'  and employeemaster.dateofjoin between '" + ViewState["stdate"].ToString() + "' and '" + ViewState["endate"].ToString() + "' ", con);
                                        DataTable dt1 = new DataTable();
                                        da1.Fill(dt1);

                                        GridView6.DataSource = dt1;
                                        GridView6.DataBind();

                                        decimal d9 = 0;

                                        foreach (GridViewRow grd in GridView6.Rows)
                                        {
                                            Label Label10var = (Label)grd.FindControl("Label10var");

                                            //for (int rowindex = 0; rowindex < dt1.Rows.Count; rowindex++)
                                            //{
                                            string str12 = "select EmployeeBatchMaster.* from EmployeeBatchMaster inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID=EmployeeBatchMaster.Employeeid where EmployeeMaster.EmployeeMasterID='" + Label10var.Text + "' and EmployeeMaster.Whid='" + Convert.ToString(dtlsde1.Rows[0]["BusinessID"]) + "'";
                                            DataTable ds12 = new DataTable();
                                            SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
                                            da12.Fill(ds12);
                                            string outdifftime1 = "00:00";

                                            if (ds12.Rows.Count > 0)
                                            {
                                                ViewState["BatchId"] = ds12.Rows[0]["Batchmasterid"].ToString();

                                                SqlDataAdapter daf = new SqlDataAdapter("select [StartDate],[EndDate] from [ReportPeriod] where [Report_Period_Id]='" + Convert.ToString(dtlsde1.Rows[0]["AccountID"]) + "'", con);
                                                DataTable dtf = new DataTable();
                                                daf.Fill(dtf);

                                                if (dtf.Rows.Count > 0)
                                                {
                                                    ViewState["std"] = Convert.ToDateTime(dtf.Rows[0]["StartDate"].ToString()).ToShortDateString();
                                                    ViewState["end"] = Convert.ToDateTime(dtf.Rows[0]["EndDate"].ToString()).ToShortDateString();
                                                }


                                                string str12311 = "select count(BatchWorkingDaysId) as BatchWorkingDays from BatchWorkingDays inner join DateMasterTbl on DateMasterTbl.DateId=BatchWorkingDays.DateMasterID where BatchWorkingDays.BatchID='" + ViewState["BatchId"] + "' and DateMasterTbl.Date between '" + ViewState["std"] + "' and '" + ViewState["end"] + "'";
                                                //and DateMasterTbl.Date between '" + ViewState["std"] + "' and '" + ViewState["end"] + "'";
                                                DataTable ds12311 = new DataTable();
                                                SqlDataAdapter da12311 = new SqlDataAdapter(str12311, con);
                                                da12311.Fill(ds12311);

                                                if (Convert.ToString(ds12311.Rows[0]["BatchWorkingDays"]) != "")
                                                {
                                                    ViewState["count"] = Convert.ToString(ds12311.Rows[0]["BatchWorkingDays"]);
                                                }

                                                string str123 = "select * from BatchWorkingDays inner join DateMasterTbl on DateMasterTbl.DateId=BatchWorkingDays.DateMasterID where BatchWorkingDays.BatchID='" + ViewState["BatchId"] + "' and DateMasterTbl.Date between '" + ViewState["std"] + "' and '" + ViewState["end"] + "'";
                                                //and DateMasterTbl.Date between '" + ViewState["std"] + "' and '" + ViewState["end"] + "'";
                                                DataTable ds123 = new DataTable();
                                                SqlDataAdapter da123 = new SqlDataAdapter(str123, con);
                                                da123.Fill(ds123);

                                                if (ds123.Rows.Count > 0)
                                                {
                                                    DataTable dsday = new DataTable();
                                                    //if (ds123.Rows[0]["day"].ToString() == "Monday")
                                                    //{



                                                    string TotalMinutes = "00:00";
                                                    string TotalMinutes1 = "00:00";
                                                    string TotalMinutes2 = "00:00";
                                                    string TotalMinutes3 = "00:00";
                                                    string TotalMinutes4 = "00:00";
                                                    string TotalMinutes5 = "00:00";
                                                    string TotalMinutes6 = "00:00";

                                                    string outdifftime12 = "00:00";
                                                    string outdifftime13 = "00:00";
                                                    string outdifftime14 = "00:00";
                                                    string outdifftime15 = "00:00";
                                                    string outdifftime16 = "00:00";
                                                    string outdifftime17 = "00:00";

                                                    string s1 = "";
                                                    string s11 = "";
                                                    string s2 = "";
                                                    string s22 = "";
                                                    string s3 = "";
                                                    string s33 = "";
                                                    string s4 = "";
                                                    string s44 = "";
                                                    string s5 = "";
                                                    string s55 = "";
                                                    string s6 = "";
                                                    string s66 = "";
                                                    string s7 = "";
                                                    string s77 = "";



                                                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.MondayScheduleId=BatchTiming.ID  inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where BatchTiming.Whid='" + Convert.ToString(dtlsde1.Rows[0]["BusinessID"]) + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";

                                                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                                                    daday.Fill(dsday);

                                                    if (dsday.Rows.Count > 0)
                                                    {
                                                        if (Convert.ToString(dsday.Rows[0]["totalhours"]) != "")
                                                        {
                                                            TotalMinutes = Convert.ToString(dsday.Rows[0]["totalhours"]);
                                                            outdifftime1 = Convert.ToDateTime(TotalMinutes).ToString("HH:mm");

                                                            string strValues = outdifftime1;
                                                            string[] strArray = strValues.Split(':');

                                                            s1 = strArray[0];
                                                            s11 = strArray[1];
                                                        }
                                                        else
                                                        {
                                                            s1 = "0";
                                                            s11 = "0";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        s1 = "0";
                                                        s11 = "0";
                                                    }


                                                    DataTable dsday1 = new DataTable();
                                                    string strday1 = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.TuesdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + Convert.ToString(dtlsde1.Rows[0]["BusinessID"]) + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";

                                                    SqlDataAdapter daday1 = new SqlDataAdapter(strday1, con);
                                                    daday1.Fill(dsday1);

                                                    if (dsday1.Rows.Count > 0)
                                                    {
                                                        if (Convert.ToString(dsday1.Rows[0]["totalhours"]) != "")
                                                        {
                                                            TotalMinutes1 = Convert.ToString(dsday1.Rows[0]["totalhours"]);
                                                            outdifftime12 = Convert.ToDateTime(TotalMinutes1).ToString("HH:mm");

                                                            string strValues1 = outdifftime12;
                                                            string[] strArray1 = strValues1.Split(':');

                                                            s2 = strArray1[0];
                                                            s22 = strArray1[1];
                                                        }
                                                        else
                                                        {
                                                            s2 = "0";
                                                            s22 = "0";
                                                        }

                                                    }
                                                    else
                                                    {
                                                        s2 = "0";
                                                        s22 = "0";
                                                    }


                                                    DataTable dsday2 = new DataTable();
                                                    string strday2 = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.WednesdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + Convert.ToString(dtlsde1.Rows[0]["BusinessID"]) + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";

                                                    SqlDataAdapter daday2 = new SqlDataAdapter(strday2, con);
                                                    daday2.Fill(dsday2);


                                                    if (dsday2.Rows.Count > 0)
                                                    {
                                                        if (Convert.ToString(dsday2.Rows[0]["totalhours"]) != "")
                                                        {
                                                            TotalMinutes2 = Convert.ToString(dsday2.Rows[0]["totalhours"]);
                                                            outdifftime13 = Convert.ToDateTime(TotalMinutes2).ToString("HH:mm");

                                                            string strValues2 = outdifftime13;
                                                            string[] strArray2 = strValues2.Split(':');

                                                            s3 = strArray2[0];
                                                            s33 = strArray2[1];
                                                        }
                                                        else
                                                        {
                                                            s3 = "0";
                                                            s33 = "0";
                                                        }

                                                    }
                                                    else
                                                    {
                                                        s3 = "0";
                                                        s33 = "0";
                                                    }



                                                    DataTable dsday3 = new DataTable();
                                                    string strday3 = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.ThursdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + Convert.ToString(dtlsde1.Rows[0]["BusinessID"]) + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";

                                                    SqlDataAdapter daday3 = new SqlDataAdapter(strday3, con);
                                                    daday3.Fill(dsday3);


                                                    if (dsday3.Rows.Count > 0)
                                                    {
                                                        if (Convert.ToString(dsday3.Rows[0]["totalhours"]) != "")
                                                        {
                                                            TotalMinutes3 = Convert.ToString(dsday3.Rows[0]["totalhours"]);
                                                            outdifftime14 = Convert.ToDateTime(TotalMinutes3).ToString("HH:mm");

                                                            string strValues3 = outdifftime14;
                                                            string[] strArray3 = strValues3.Split(':');

                                                            s4 = strArray3[0];
                                                            s44 = strArray3[1];
                                                        }
                                                        else
                                                        {
                                                            s4 = "0";
                                                            s44 = "0";
                                                        }

                                                    }
                                                    else
                                                    {
                                                        s4 = "0";
                                                        s44 = "0";
                                                    }



                                                    DataTable dsday4 = new DataTable();
                                                    string strday4 = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.FridayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + Convert.ToString(dtlsde1.Rows[0]["BusinessID"]) + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";

                                                    SqlDataAdapter daday4 = new SqlDataAdapter(strday4, con);
                                                    daday4.Fill(dsday4);

                                                    if (dsday4.Rows.Count > 0)
                                                    {
                                                        if (Convert.ToString(dsday4.Rows[0]["totalhours"]) != "")
                                                        {
                                                            TotalMinutes4 = Convert.ToString(dsday4.Rows[0]["totalhours"]);
                                                            outdifftime15 = Convert.ToDateTime(TotalMinutes4).ToString("HH:mm");


                                                            string strValues4 = outdifftime15;
                                                            string[] strArray4 = strValues4.Split(':');

                                                            s5 = strArray4[0];
                                                            s55 = strArray4[1];
                                                        }
                                                        else
                                                        {
                                                            s5 = "0";
                                                            s55 = "0";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        s5 = "0";
                                                        s55 = "0";
                                                    }



                                                    DataTable dsday5 = new DataTable();
                                                    string strday5 = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.SaturdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + Convert.ToString(dtlsde1.Rows[0]["BusinessID"]) + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";

                                                    SqlDataAdapter daday5 = new SqlDataAdapter(strday5, con);
                                                    daday5.Fill(dsday5);

                                                    if (dsday5.Rows.Count > 0)
                                                    {
                                                        if (Convert.ToString(dsday5.Rows[0]["totalhours"]) != "")
                                                        {
                                                            TotalMinutes5 = Convert.ToString(dsday5.Rows[0]["totalhours"]);
                                                            outdifftime16 = Convert.ToDateTime(TotalMinutes5).ToString("HH:mm");

                                                            string strValues5 = outdifftime16;
                                                            string[] strArray5 = strValues5.Split(':');

                                                            s6 = strArray5[0];
                                                            s66 = strArray5[1];
                                                        }
                                                        else
                                                        {
                                                            s6 = "0";
                                                            s66 = "0";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        s6 = "0";
                                                        s66 = "0";
                                                    }



                                                    DataTable dsday6 = new DataTable();
                                                    string strday6 = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.SundayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + Convert.ToString(dtlsde1.Rows[0]["BusinessID"]) + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";

                                                    SqlDataAdapter daday6 = new SqlDataAdapter(strday6, con);
                                                    daday6.Fill(dsday6);


                                                    if (dsday6.Rows.Count > 0)
                                                    {
                                                        if (Convert.ToString(dsday6.Rows[0]["totalhours"]) != "")
                                                        {
                                                            TotalMinutes6 = Convert.ToString(dsday6.Rows[0]["totalhours"]);
                                                            outdifftime17 = Convert.ToDateTime(TotalMinutes6).ToString("HH:mm");

                                                            string strValues6 = outdifftime17;
                                                            string[] strArray6 = strValues6.Split(':');

                                                            s7 = strArray6[0];
                                                            s77 = strArray6[1];
                                                        }
                                                        else
                                                        {
                                                            s7 = "0";
                                                            s77 = "0";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        s7 = "0";
                                                        s77 = "0";
                                                    }


                                                    int hourss = Convert.ToInt32(s1) + Convert.ToInt32(s2) + Convert.ToInt32(s3) + Convert.ToInt32(s4) + Convert.ToInt32(s5) + Convert.ToInt32(s6) + Convert.ToInt32(s7);

                                                    int min1 = hourss * 60;

                                                    int minss = Convert.ToInt32(s11) + Convert.ToInt32(s22) + Convert.ToInt32(s33) + Convert.ToInt32(s44) + Convert.ToInt32(s55) + Convert.ToInt32(s66) + Convert.ToInt32(s77);

                                                    int finalmin = min1 + minss;

                                                    int finhr1 = (finalmin / 60);
                                                    int finhr2 = (finalmin % 60);

                                                    string finaltime = "";

                                                    if (finhr2 == 0)
                                                    {
                                                        finaltime = finhr1.ToString();
                                                    }
                                                    else
                                                    {
                                                        finaltime = finhr1.ToString() + "." + finhr2.ToString();
                                                    }

                                                    decimal myfinal = 0;

                                                    if (dsday6.Rows.Count == 0)
                                                    {
                                                        myfinal = Convert.ToDecimal(finaltime) / 6;
                                                    }
                                                    else if (dsday6.Rows.Count == 0 && dsday5.Rows.Count == 0)
                                                    {
                                                        myfinal = Convert.ToDecimal(finaltime) / 5;
                                                    }
                                                    else if (dsday6.Rows.Count == 0 && dsday5.Rows.Count == 0 && dsday4.Rows.Count == 0)
                                                    {
                                                        myfinal = Convert.ToDecimal(finaltime) / 4;
                                                    }
                                                    else if (dsday6.Rows.Count == 0 && dsday5.Rows.Count == 0 && dsday4.Rows.Count == 0 && dsday3.Rows.Count == 0)
                                                    {
                                                        myfinal = Convert.ToDecimal(finaltime) / 3;
                                                    }
                                                    else if (dsday6.Rows.Count == 0 && dsday5.Rows.Count == 0 && dsday4.Rows.Count == 0 && dsday3.Rows.Count == 0 && dsday2.Rows.Count == 0)
                                                    {
                                                        myfinal = Convert.ToDecimal(finaltime) / 2;
                                                    }
                                                    else if (dsday6.Rows.Count == 0 && dsday5.Rows.Count == 0 && dsday4.Rows.Count == 0 && dsday3.Rows.Count == 0 && dsday2.Rows.Count == 0 && dsday1.Rows.Count == 0)
                                                    {
                                                        myfinal = Convert.ToDecimal(finaltime) / 1;
                                                    }
                                                    else
                                                    {
                                                        myfinal = Convert.ToDecimal(finaltime) / 7;
                                                    }

                                                    myfinal = Math.Round(myfinal, 2);

                                                    //  string temp1 = Convert.ToDateTime(finaltime).ToString("HH:mm");

                                                    Label Labelsdfsdd159 = (Label)grd.FindControl("Labelsdfsdd159");
                                                    Labelsdfsdd159.Text = myfinal.ToString();

                                                    Label Labxxx158 = (Label)grd.FindControl("Labxxx158");
                                                    Labxxx158.Text = ViewState["count"].ToString();

                                                    //dt1.Rows[rowindex]["dailyhr"] = Convert.ToInt32(finaltime);
                                                    //dt1.Rows[rowindex]["totald"] = ViewState["count"].ToString();



                                                    double dec = Convert.ToDouble(myfinal) * Convert.ToDouble(Labxxx158.Text);

                                                    Label Labtttt59 = (Label)grd.FindControl("Labtttt59");
                                                    Labtttt59.Text = dec.ToString("###,###.##");


                                                    string st9 = Labtttt59.Text;
                                                    if (st9 == "")
                                                    {
                                                        st9 = "0";
                                                    }
                                                    d9 += Convert.ToDecimal(st9);
                                                }
                                            }
                                        }
                                        Label15.Text = d9.ToString("###,###.##");

                                        lbl2rate.Text = Label15.Text;


                                        SqlDataAdapter dalsde2 = new SqlDataAdapter("select * from OverheadAbsorbtionDetailTbl where OverheadAbsorbtionMasterID='" + Convert.ToString(dtlsde1.Rows[0]["ID"]) + "'", con);
                                        DataTable dtlsde2 = new DataTable();
                                        dalsde2.Fill(dtlsde2);

                                        GridView5.DataSource = dtlsde2;
                                        GridView5.DataBind();

                                        decimal d33 = 0;

                                        foreach (GridViewRow gr in GridView5.Rows)
                                        {
                                            Label Label15911 = (Label)gr.FindControl("Label15911");

                                            string d3 = Label15911.Text;
                                            d33 += Convert.ToDecimal(d3);
                                        }

                                        lbl1rate.Text = d33.ToString("###,###.##");

                                        double div = Convert.ToDouble(lbl1rate.Text) / Convert.ToDouble(lbl2rate.Text);
                                        lbl3rate.Text = div.ToString("###,###.##");


                                        string stryy1 = "insert into EmplAvgCostperhourmaster values('" + Convert.ToString(dtlsde1.Rows[0]["BusinessID"]) + "','" + txtjoindate.Text + ' ' + System.DateTime.Now.TimeOfDay.ToString().Substring(0, 8) + "','" + lbl3rate.Text + "')";

                                        SqlCommand cmdyy1 = new SqlCommand(stryy1, con);
                                        if (con.State.ToString() != "Open")
                                        {
                                            con.Open();
                                        }
                                        cmdyy1.ExecuteNonQuery();
                                        con.Close();

                                        SqlDataAdapter daoy1 = new SqlDataAdapter("select max(ID) as ID from EmplAvgCostperhourmaster", con);
                                        DataTable dtoy1 = new DataTable();
                                        daoy1.Fill(dtoy1);

                                        if (dtoy1.Rows.Count > 0)
                                        {
                                            ViewState["tbl"] = Convert.ToString(dtoy1.Rows[0]["ID"]);

                                            foreach (GridViewRow gt in GridView6.Rows)
                                            {
                                                Label Label10var = (Label)gt.FindControl("Label10var");
                                                Label Labesdfsdl158 = (Label)gt.FindControl("Labesdfsdl158");


                                                SqlDataAdapter dak = new SqlDataAdapter("select AvgRate from EmployeeAvgSalaryMaster where EmployeeId='" + Label10var.Text + "'", con);
                                                DataTable dtk = new DataTable();
                                                dak.Fill(dtk);

                                                if (dtk.Rows.Count > 0)
                                                {
                                                    if (Convert.ToString(dtk.Rows[0]["AvgRate"]) != "")
                                                    {
                                                        ViewState["ret"] = dtk.Rows[0]["AvgRate"].ToString();
                                                    }
                                                    else
                                                    {
                                                        ViewState["ret"] = "0";
                                                    }
                                                }
                                                else
                                                {
                                                    ViewState["ret"] = "0";
                                                }

                                                double db = Convert.ToDouble(lbl3rate.Text) + Convert.ToDouble(ViewState["ret"].ToString());
                                                string money = db.ToString();

                                                SqlCommand cmmm = new SqlCommand("insert into EmplAvgcostperhourdetail values ('" + ViewState["tbl"].ToString() + "','" + Label10var.Text + "','" + money + "','" + ViewState["ret"].ToString() + "')", con);
                                                if (con.State.ToString() != "Open")
                                                {
                                                    con.Open();
                                                }
                                                cmmm.ExecuteNonQuery();
                                                con.Close();

                                            }
                                        }

                                        string strins = "insert into OverheadAbsorbtionMasterTbl values('" + Convert.ToString(dtlsde1.Rows[0]["BusinessID"]) + "','" + Convert.ToString(dtlsde1.Rows[0]["AccountID"]) + "','" + lbl3rate.Text + "','" + txtjoindate.Text + ' ' + System.DateTime.Now.TimeOfDay.ToString().Substring(0, 8) + "')";

                                        SqlCommand cmdcmd = new SqlCommand(strins, con);
                                        if (con.State.ToString() != "Open")
                                        {
                                            con.Open();
                                        }
                                        cmdcmd.ExecuteNonQuery();
                                        con.Close();

                                        SqlDataAdapter dao = new SqlDataAdapter("select max(ID) as ID from OverheadAbsorbtionMasterTbl", con);
                                        DataTable dto = new DataTable();
                                        dao.Fill(dto);

                                        if (dto.Rows.Count > 0)
                                        {
                                            ViewState["store"] = Convert.ToString(dto.Rows[0]["ID"]);

                                            foreach (GridViewRow gdr in GridView5.Rows)
                                            {
                                                Label Label15811 = (Label)gdr.FindControl("Label15811");
                                                Label Label15911 = (Label)gdr.FindControl("Label15911");

                                                SqlCommand cmd13 = new SqlCommand("insert into OverheadAbsorbtionDetailTbl values('" + ViewState["store"].ToString() + "','" + Label15811.Text + "','" + Label15911.Text + "')", con);
                                                if (con.State.ToString() != "Open")
                                                {
                                                    con.Open();
                                                }
                                                cmd13.ExecuteNonQuery();
                                                con.Close();
                                            }
                                        }

                                    }
                                }


                                // --- insert data in employee barcode --- ///
                                chkempbarcode.Checked = true;
                                if (chkempbarcode.Checked == true)
                                {
                                    string insertempcode = "Insert Into EmployeeBarcodeMaster(Employee_Id,Barcode,Active,Whid,Employeecode,Biometricno,blutoothid)values('" + empid + "','" + txtbarcode.Text + "','" + ddlActive.SelectedValue + "','" + ddlwarehouse.SelectedValue + "','" + txtsecuritycode.Text + "','" + txtbiometricid.Text + "','" + txtbluetoothid.Text + "')";
                                    SqlCommand cmdempcode = new SqlCommand(insertempcode, con);
                                    con.Open();
                                    cmdempcode.ExecuteNonQuery();
                                    con.Close();
                                }

                                if (Request.QueryString["Id"] == "1")
                                {
                                    string insertempcode = "Insert Into EmployeeBarcodeMaster(Employee_Id,Barcode,Active,Whid,Employeecode,Biometricno,blutoothid)values('" + empid + "','',1,'" + ddlwarehouse.SelectedValue + "','','','')";
                                    SqlCommand cmdempcode = new SqlCommand(insertempcode, con);
                                    con.Open();
                                    cmdempcode.ExecuteNonQuery();
                                    con.Close();
                                }

                                // --- end inserting in employee barcode ---///



                                //---insert data in employee payroll ----////
                                if (chkemppayroll.Checked == true)
                                {
                                    if (ddlPaymentMethod.SelectedItem.Text == "Demand Draft" || ddlPaymentMethod.SelectedItem.Text == "Cheque")
                                    {
                                        string str = "Insert Into EmployeePayrollMaster(EmpId,EmployeePaidAsPerDesignation,PaymentMethodId,PaymentReceivedNameOf,Whid,Compid,PayPeriodMasterId,LastName,FirstName,Intials,EmployeeNo,DateOfBirth,SocialSecurityNo)values('" + empid + "','" + RadioButtonList1.SelectedValue + "','" + ddlPaymentMethod.SelectedValue + "','" + txtPaymentReceivedNameOf.Text + "','" + ddlwarehouse.SelectedValue + "','" + Session["comid"] + "','" + ddlPaymentCycle.SelectedValue + "','" + txtlastname.Text + "','" + txtfirstname.Text + "','" + txtintialis.Text + "','" + TextBox1.Text.Trim() + "','" + txtdateofbirth.Text + "','" + txtsecurityno.Text + "')";
                                        SqlCommand cmddemand = new SqlCommand(str, con);
                                        if (con.State.ToString() != "Open")
                                        {
                                            con.Open();
                                        }
                                        cmddemand.ExecuteNonQuery();
                                        con.Close();


                                    }
                                    else if (ddlPaymentMethod.SelectedItem.Text == "Paypal")
                                    {
                                        string str = "Insert Into EmployeePayrollMaster(EmpId,EmployeePaidAsPerDesignation,PaymentMethodId,PaypalId,Whid,Compid,PayPeriodMasterId,LastName,FirstName,Intials,EmployeeNo,DateOfBirth,SocialSecurityNo)values('" + empid + "','" + RadioButtonList1.SelectedValue + "','" + ddlPaymentMethod.SelectedValue + "','" + txtPaypalId.Text + "','" + ddlwarehouse.SelectedValue + "','" + Session["comid"] + "','" + ddlPaymentCycle.SelectedValue + "','" + txtlastname.Text + "','" + txtfirstname.Text + "','" + txtintialis.Text + "','" + TextBox1.Text.Trim() + "','" + txtdateofbirth.Text + "','" + txtsecurityno.Text + "')";
                                        SqlCommand cmdpaypal = new SqlCommand(str, con);
                                        if (con.State.ToString() != "Open")
                                        {
                                            con.Open();
                                        }
                                        cmdpaypal.ExecuteNonQuery();
                                        con.Close();


                                    }
                                    else if (ddlPaymentMethod.SelectedItem.Text == "By Email")
                                    {
                                        string str = "Insert Into EmployeePayrollMaster(EmpId,EmployeePaidAsPerDesignation,PaymentMethodId,PaymentEmailId,Whid,Compid,PayPeriodMasterId,LastName,FirstName,Intials,EmployeeNo,DateOfBirth,SocialSecurityNo)values('" + empid + "','" + RadioButtonList1.SelectedValue + "','" + ddlPaymentMethod.SelectedValue + "','" + txtPaymentEmailId.Text + "','" + ddlwarehouse.SelectedValue + "','" + Session["comid"] + "','" + ddlPaymentCycle.SelectedValue + "','" + txtlastname.Text + "','" + txtfirstname.Text + "','" + txtintialis.Text + "','" + TextBox1.Text.Trim() + "','" + txtdateofbirth.Text + "','" + txtsecurityno.Text + "')";
                                        SqlCommand cmdemail = new SqlCommand(str, con);
                                        if (con.State.ToString() != "Open")
                                        {
                                            con.Open();
                                        }
                                        cmdemail.ExecuteNonQuery();
                                        con.Close();

                                    }

                                    else if (ddlPaymentMethod.SelectedItem.Text == "Direct Deposit")
                                    {
                                        string str = "Insert Into EmployeePayrollMaster(EmpId,EmployeePaidAsPerDesignation,PaymentMethodId,DirectDepositBankName,DirectDepositBankBranchName,DirectDepositTransitNumber,DirectDepositAccountHolderName,DirectDepositBankAccountType,DirectDepositBankAccountNumber,DirectDepositBankBranchAddress,DirectDepositBankBranchcity,DirectDepositBankBranchstate,DirectDepositBankBranchcountry,DirectDepositBankBranchzipcode,DirectDepositBankIFCNumber,DirectDepositBankSwiftNumber,DirectDepositBankEmployeeEmail,Whid,Compid,PayPeriodMasterId,LastName,FirstName,Intials,EmployeeNo,DateOfBirth,SocialSecurityNo,RegisterBankAddress)values('" + empid + "','" + RadioButtonList1.SelectedValue + "','" + ddlPaymentMethod.SelectedValue + "','" + txtDirectDepositBankName.Text + "','" + txtDirectDepositBankBranchName.Text + "','" + txtDirectDepositTransitNumber.Text + "','" + txtDirectDepositAccountHolderName.Text + "','" + ddlDirectDepositBankAccountType.SelectedValue + "','" + txtDirectDepositBankAccountNumber.Text + "','" + txtDirectDepositBranchAddress.Text + "','" + ddlDirectDepositBankBranchcity.SelectedValue + "','" + ddlDirectDepositBankBranchstate.SelectedValue + "','" + ddlDirectDepositBankBranchcountry.SelectedValue + "','" + ddlDirectDepositBankBranchzipcode.Text + "','" + txtDirectDepositifscnumber.Text + "','" + txtDirectDepositswiftnumber.Text + "','" + txtdirectdipositemployeeemail.Text + "','" + ddlwarehouse.SelectedValue + "','" + Session["comid"] + "','" + ddlPaymentCycle.SelectedValue + "','" + txtlastname.Text + "','" + txtfirstname.Text + "','" + txtintialis.Text + "','" + TextBox1.Text.Trim() + "','" + txtdateofbirth.Text + "','" + txtsecurityno.Text + "','" + TextBox2.Text + "')";
                                        SqlCommand cmddeposit = new SqlCommand(str, con);
                                        if (con.State.ToString() != "Open")
                                        {
                                            con.Open();
                                        }
                                        cmddeposit.ExecuteNonQuery();
                                        con.Close();

                                    }
                                    else if (ddlPaymentMethod.SelectedItem.Text == "Cash")
                                    {
                                        string str = "Insert Into EmployeePayrollMaster(EmpId,EmployeePaidAsPerDesignation,PaymentMethodId,Whid,Compid,PayPeriodMasterId,LastName,FirstName,Intials,EmployeeNo,DateOfBirth,SocialSecurityNo)values('" + empid + "','" + RadioButtonList1.SelectedValue + "','" + ddlPaymentMethod.SelectedValue + "','" + ddlwarehouse.SelectedValue + "','" + Session["comid"] + "','" + ddlPaymentCycle.SelectedValue + "','" + txtlastname.Text + "','" + txtfirstname.Text + "','" + txtintialis.Text + "','" + TextBox1.Text.Trim() + "','" + txtdateofbirth.Text + "','" + txtsecurityno.Text + "')";
                                        SqlCommand cmdcash = new SqlCommand(str, con);
                                        if (con.State.ToString() != "Open")
                                        {
                                            con.Open();
                                        }
                                        cmdcash.ExecuteNonQuery();
                                        con.Close();

                                    }
                                }


                                //---end inserting in employee payroll -----///

                                ////-----Start for Add Cabinet Drawer Folder----/////

                                string candcab = "select DocumentMainType,DocumentMainTypeId from DocumentMainType where CID='" + Session["Comid"].ToString() + "' and Whid='" + ddlwarehouse.SelectedValue + "' and DocumentMainType='Employee'";

                                SqlDataAdapter dacancab = new SqlDataAdapter(candcab, con);
                                DataTable dtcancab = new DataTable();
                                dacancab.Fill(dtcancab);

                                if (dtcancab.Rows.Count > 0)
                                {
                                    SqlCommand cmdcancab = new SqlCommand("insert into DocumentSubType(DocumentMainTypeId,DocumentSubType,CID) values('" + dtcancab.Rows[0]["DocumentMainTypeId"].ToString() + "','" + txtlastname.Text + ' ' + txtfirstname.Text + ' ' + txtintialis.Text + "','" + Session["Comid"].ToString() + "')", con);
                                    if (con.State.ToString() != "Open")
                                    {
                                        con.Open();
                                    }
                                    cmdcancab.ExecuteNonQuery();
                                    con.Close();

                                    SqlDataAdapter dafindfol = new SqlDataAdapter("select max(DocumentSubTypeId) as DocumentSubTypeId from DocumentSubType", con);
                                    DataTable dtfindfol = new DataTable();
                                    dafindfol.Fill(dtfindfol);

                                    if (dtfindfol.Rows.Count > 0)
                                    {
                                        SqlDataAdapter dadraw = new SqlDataAdapter("select * from DrawerAccessRightsMaster where DrawerId='" + dtfindfol.Rows[0]["DocumentSubTypeId"].ToString() + "' and DesignationId='" + ddldesignation.SelectedValue + "' ", con);
                                        DataTable dtdraw = new DataTable();
                                        dadraw.Fill(dtdraw);

                                        if (dtdraw.Rows.Count > 0)
                                        {
                                            SqlCommand cmddraw = new SqlCommand("update DrawerAccessRightsMaster set ViewAccess='1' where DrawerId='" + dtfindfol.Rows[0]["DocumentSubTypeId"].ToString() + "' and DesignationId='" + ddldesignation.SelectedValue + "'", con);
                                            if (con.State.ToString() != "Open")
                                            {
                                                con.Open();
                                            }
                                            cmddraw.ExecuteNonQuery();
                                            con.Close();
                                        }
                                        else
                                        {
                                            SqlCommand cmddraw1 = new SqlCommand("insert into DrawerAccessRightsMaster ([DrawerId],[DesignationId],[ViewAccess],[DeleteAccess],[SaveAccess],[EditAccess],[EmailAccess],[FaxAccess],[PrintAccess],[MessageAccess]) values ('" + dtfindfol.Rows[0]["DocumentSubTypeId"].ToString() + "','" + ddldesignation.SelectedValue + "','1','0','0','0','0','1','1','0')", con);
                                            if (con.State.ToString() != "Open")
                                            {
                                                con.Open();
                                            }
                                            cmddraw1.ExecuteNonQuery();
                                            con.Close();
                                        }

                                        SqlCommand cmdcanres = new SqlCommand("insert into DocumentType(DocumentSubTypeId,DocumentType,CID) values('" + dtfindfol.Rows[0]["DocumentSubTypeId"].ToString() + "','Contract','" + Session["Comid"].ToString() + "')", con);
                                        if (con.State.ToString() != "Open")
                                        {
                                            con.Open();
                                        }
                                        cmdcanres.ExecuteNonQuery();
                                        con.Close();

                                        SqlDataAdapter daid1 = new SqlDataAdapter("select max(DocumentTypeId) as DocumentTypeId from DocumentType where DocumentSubTypeId='" + dtfindfol.Rows[0]["DocumentSubTypeId"].ToString() + "' and DocumentType='Contract'", con);
                                        DataTable dtid1 = new DataTable();
                                        daid1.Fill(dtid1);

                                        if (dtid1.Rows.Count > 0)
                                        {
                                            ViewState["upload1"] = Convert.ToString(dtid1.Rows[0]["DocumentTypeId"]);
                                        }

                                        SqlCommand cmddraw123 = new SqlCommand("insert into DocumentAccessRightMaster ([DocumentTypeId],[DesignationId],[ViewAccess],[DeleteAccess],[SaveAccess],[EditAccess],[EmailAccess],[FaxAccess],[PrintAccess],[MessageAccess],[CID]) values ('" + ViewState["upload1"] + "','" + ddldesignation.SelectedValue + "','1','0','0','0','0','1','1','0','" + Session["Comid"].ToString() + "')", con);
                                        if (con.State.ToString() != "Open")
                                        {
                                            con.Open();
                                        }
                                        cmddraw123.ExecuteNonQuery();
                                        con.Close();






                                        SqlCommand cmdcanid = new SqlCommand("insert into DocumentType(DocumentSubTypeId,DocumentType,CID) values('" + dtfindfol.Rows[0]["DocumentSubTypeId"].ToString() + "','Resume','" + Session["Comid"].ToString() + "')", con);
                                        if (con.State.ToString() != "Open")
                                        {
                                            con.Open();
                                        }
                                        cmdcanid.ExecuteNonQuery();
                                        con.Close();

                                        SqlDataAdapter daid11 = new SqlDataAdapter("select max(DocumentTypeId) as DocumentTypeId from DocumentType where DocumentSubTypeId='" + dtfindfol.Rows[0]["DocumentSubTypeId"].ToString() + "' and DocumentType='Resume'", con);
                                        DataTable dtid11 = new DataTable();
                                        daid11.Fill(dtid11);

                                        if (dtid11.Rows.Count > 0)
                                        {
                                            ViewState["upload2"] = Convert.ToString(dtid11.Rows[0]["DocumentTypeId"]);
                                        }


                                        SqlCommand cmddraw223 = new SqlCommand("insert into DocumentAccessRightMaster ([DocumentTypeId],[DesignationId],[ViewAccess],[DeleteAccess],[SaveAccess],[EditAccess],[EmailAccess],[FaxAccess],[PrintAccess],[MessageAccess],[CID]) values ('" + ViewState["upload2"] + "','" + ddldesignation.SelectedValue + "','1','0','0','0','0','1','1','0','" + Session["Comid"].ToString() + "')", con);
                                        if (con.State.ToString() != "Open")
                                        {
                                            con.Open();
                                        }
                                        cmddraw223.ExecuteNonQuery();
                                        con.Close();








                                        SqlCommand cmdcanmydoc = new SqlCommand("insert into DocumentType(DocumentSubTypeId,DocumentType,CID) values('" + dtfindfol.Rows[0]["DocumentSubTypeId"].ToString() + "','Employee ID','" + Session["Comid"].ToString() + "')", con);
                                        if (con.State.ToString() != "Open")
                                        {
                                            con.Open();
                                        }
                                        cmdcanmydoc.ExecuteNonQuery();
                                        con.Close();

                                        SqlDataAdapter daid1231 = new SqlDataAdapter("select max(DocumentTypeId) as DocumentTypeId from DocumentType where DocumentSubTypeId='" + dtfindfol.Rows[0]["DocumentSubTypeId"].ToString() + "' and DocumentType='Employee ID'", con);
                                        DataTable dtid1231 = new DataTable();
                                        daid1231.Fill(dtid1231);

                                        if (dtid1231.Rows.Count > 0)
                                        {
                                            ViewState["upload3"] = Convert.ToString(dtid1231.Rows[0]["DocumentTypeId"]);
                                        }


                                        SqlCommand cmddraw323 = new SqlCommand("insert into DocumentAccessRightMaster ([DocumentTypeId],[DesignationId],[ViewAccess],[DeleteAccess],[SaveAccess],[EditAccess],[EmailAccess],[FaxAccess],[PrintAccess],[MessageAccess],[CID]) values ('" + ViewState["upload3"] + "','" + ddldesignation.SelectedValue + "','1','0','0','0','0','1','1','0','" + Session["Comid"].ToString() + "')", con);
                                        if (con.State.ToString() != "Open")
                                        {
                                            con.Open();
                                        }
                                        cmddraw323.ExecuteNonQuery();
                                        con.Close();




                                        SqlCommand cmdcanmydoc1 = new SqlCommand("insert into DocumentType(DocumentSubTypeId,DocumentType,CID) values('" + dtfindfol.Rows[0]["DocumentSubTypeId"].ToString() + "','Salary Slip','" + Session["Comid"].ToString() + "')", con);
                                        if (con.State.ToString() != "Open")
                                        {
                                            con.Open();
                                        }
                                        cmdcanmydoc1.ExecuteNonQuery();
                                        con.Close();

                                        SqlDataAdapter daid12311 = new SqlDataAdapter("select max(DocumentTypeId) as DocumentTypeId from DocumentType where DocumentSubTypeId='" + dtfindfol.Rows[0]["DocumentSubTypeId"].ToString() + "' and DocumentType='Salary Slip'", con);
                                        DataTable dtid12311 = new DataTable();
                                        daid12311.Fill(dtid12311);

                                        if (dtid12311.Rows.Count > 0)
                                        {
                                            ViewState["upload4"] = Convert.ToString(dtid12311.Rows[0]["DocumentTypeId"]);
                                        }


                                        SqlCommand cmddraw3231 = new SqlCommand("insert into DocumentAccessRightMaster ([DocumentTypeId],[DesignationId],[ViewAccess],[DeleteAccess],[SaveAccess],[EditAccess],[EmailAccess],[FaxAccess],[PrintAccess],[MessageAccess],[CID]) values ('" + ViewState["upload4"] + "','" + ddldesignation.SelectedValue + "','1','0','0','0','0','1','1','0','" + Session["Comid"].ToString() + "')", con);
                                        if (con.State.ToString() != "Open")
                                        {
                                            con.Open();
                                        }
                                        cmddraw3231.ExecuteNonQuery();
                                        con.Close();






                                        SqlCommand cmdcanmydoc12 = new SqlCommand("insert into DocumentType(DocumentSubTypeId,DocumentType,CID) values('" + dtfindfol.Rows[0]["DocumentSubTypeId"].ToString() + "','Tax Document','" + Session["Comid"].ToString() + "')", con);
                                        if (con.State.ToString() != "Open")
                                        {
                                            con.Open();
                                        }
                                        cmdcanmydoc12.ExecuteNonQuery();
                                        con.Close();

                                        SqlDataAdapter daid123112 = new SqlDataAdapter("select max(DocumentTypeId) as DocumentTypeId from DocumentType where DocumentSubTypeId='" + dtfindfol.Rows[0]["DocumentSubTypeId"].ToString() + "' and DocumentType='Tax Document'", con);
                                        DataTable dtid123112 = new DataTable();
                                        daid123112.Fill(dtid123112);

                                        if (dtid123112.Rows.Count > 0)
                                        {
                                            ViewState["upload5"] = Convert.ToString(dtid123112.Rows[0]["DocumentTypeId"]);
                                        }


                                        SqlCommand cmddraw32312 = new SqlCommand("insert into DocumentAccessRightMaster ([DocumentTypeId],[DesignationId],[ViewAccess],[DeleteAccess],[SaveAccess],[EditAccess],[EmailAccess],[FaxAccess],[PrintAccess],[MessageAccess],[CID]) values ('" + ViewState["upload5"] + "','" + ddldesignation.SelectedValue + "','1','0','0','0','0','1','1','0','" + Session["Comid"].ToString() + "')", con);
                                        if (con.State.ToString() != "Open")
                                        {
                                            con.Open();
                                        }
                                        cmddraw32312.ExecuteNonQuery();
                                        con.Close();





                                        SqlCommand cmdcanmydoc121 = new SqlCommand("insert into DocumentType(DocumentSubTypeId,DocumentType,CID) values('" + dtfindfol.Rows[0]["DocumentSubTypeId"].ToString() + "','document from employee','" + Session["Comid"].ToString() + "')", con);
                                        if (con.State.ToString() != "Open")
                                        {
                                            con.Open();
                                        }
                                        cmdcanmydoc121.ExecuteNonQuery();
                                        con.Close();

                                        SqlDataAdapter daid1231121 = new SqlDataAdapter("select max(DocumentTypeId) as DocumentTypeId from DocumentType where DocumentSubTypeId='" + dtfindfol.Rows[0]["DocumentSubTypeId"].ToString() + "' and DocumentType='document from employee'", con);
                                        DataTable dtid1231121 = new DataTable();
                                        daid1231121.Fill(dtid1231121);

                                        if (dtid1231121.Rows.Count > 0)
                                        {
                                            ViewState["upload6"] = Convert.ToString(dtid1231121.Rows[0]["DocumentTypeId"]);
                                        }


                                        SqlCommand cmddraw323121 = new SqlCommand("insert into DocumentAccessRightMaster ([DocumentTypeId],[DesignationId],[ViewAccess],[DeleteAccess],[SaveAccess],[EditAccess],[EmailAccess],[FaxAccess],[PrintAccess],[MessageAccess],[CID]) values ('" + ViewState["upload6"] + "','" + ddldesignation.SelectedValue + "','1','0','0','0','0','1','1','0','" + Session["Comid"].ToString() + "')", con);
                                        if (con.State.ToString() != "Open")
                                        {
                                            con.Open();
                                        }
                                        cmddraw323121.ExecuteNonQuery();
                                        con.Close();









                                        SqlCommand cmdcanmydoc1212 = new SqlCommand("insert into DocumentType(DocumentSubTypeId,DocumentType,CID) values('" + dtfindfol.Rows[0]["DocumentSubTypeId"].ToString() + "','document for approval','" + Session["Comid"].ToString() + "')", con);
                                        if (con.State.ToString() != "Open")
                                        {
                                            con.Open();
                                        }
                                        cmdcanmydoc1212.ExecuteNonQuery();
                                        con.Close();

                                        SqlDataAdapter daid12311212 = new SqlDataAdapter("select max(DocumentTypeId) as DocumentTypeId from DocumentType where DocumentSubTypeId='" + dtfindfol.Rows[0]["DocumentSubTypeId"].ToString() + "' and DocumentType='document for approval'", con);
                                        DataTable dtid12311212 = new DataTable();
                                        daid12311212.Fill(dtid12311212);

                                        if (dtid12311212.Rows.Count > 0)
                                        {
                                            ViewState["upload7"] = Convert.ToString(dtid12311212.Rows[0]["DocumentTypeId"]);
                                        }


                                        SqlCommand cmddraw3231212 = new SqlCommand("insert into DocumentAccessRightMaster ([DocumentTypeId],[DesignationId],[ViewAccess],[DeleteAccess],[SaveAccess],[EditAccess],[EmailAccess],[FaxAccess],[PrintAccess],[MessageAccess],[CID]) values ('" + ViewState["upload7"] + "','" + ddldesignation.SelectedValue + "','1','0','0','0','0','1','1','0','" + Session["Comid"].ToString() + "')", con);
                                        if (con.State.ToString() != "Open")
                                        {
                                            con.Open();
                                        }
                                        cmddraw3231212.ExecuteNonQuery();
                                        con.Close();





                                        SqlCommand cmdcanmydoc1212a = new SqlCommand("insert into DocumentType(DocumentSubTypeId,DocumentType,CID) values('" + dtfindfol.Rows[0]["DocumentSubTypeId"].ToString() + "','Alimony Maintenance Court Orders','" + Session["Comid"].ToString() + "')", con);
                                        if (con.State.ToString() != "Open")
                                        {
                                            con.Open();
                                        }
                                        cmdcanmydoc1212a.ExecuteNonQuery();
                                        con.Close();

                                        SqlDataAdapter daid12311212a = new SqlDataAdapter("select max(DocumentTypeId) as DocumentTypeId from DocumentType where DocumentSubTypeId='" + dtfindfol.Rows[0]["DocumentSubTypeId"].ToString() + "' and DocumentType='Alimony Maintenance Court Orders'", con);
                                        DataTable dtid12311212a = new DataTable();
                                        daid12311212a.Fill(dtid12311212a);

                                        if (dtid12311212a.Rows.Count > 0)
                                        {
                                            ViewState["upload8"] = Convert.ToString(dtid12311212a.Rows[0]["DocumentTypeId"]);
                                        }

                                        SqlCommand cmddraw3231212a = new SqlCommand("insert into DocumentAccessRightMaster ([DocumentTypeId],[DesignationId],[ViewAccess],[DeleteAccess],[SaveAccess],[EditAccess],[EmailAccess],[FaxAccess],[PrintAccess],[MessageAccess],[CID]) values ('" + ViewState["upload8"] + "','" + ddldesignation.SelectedValue + "','1','0','0','0','0','1','1','0','" + Session["Comid"].ToString() + "')", con);
                                        if (con.State.ToString() != "Open")
                                        {
                                            con.Open();
                                        }
                                        cmddraw3231212a.ExecuteNonQuery();
                                        con.Close();



                                        SqlCommand cmdcanmydoc1212ab = new SqlCommand("insert into DocumentType(DocumentSubTypeId,DocumentType,CID) values('" + dtfindfol.Rows[0]["DocumentSubTypeId"].ToString() + "','Confidential Records','" + Session["Comid"].ToString() + "')", con);
                                        if (con.State.ToString() != "Open")
                                        {
                                            con.Open();
                                        }
                                        cmdcanmydoc1212ab.ExecuteNonQuery();
                                        con.Close();

                                        SqlDataAdapter daid12311212ab = new SqlDataAdapter("select max(DocumentTypeId) as DocumentTypeId from DocumentType where DocumentSubTypeId='" + dtfindfol.Rows[0]["DocumentSubTypeId"].ToString() + "' and DocumentType='Confidential Records'", con);
                                        DataTable dtid12311212ab = new DataTable();
                                        daid12311212ab.Fill(dtid12311212ab);

                                        if (dtid12311212ab.Rows.Count > 0)
                                        {
                                            ViewState["upload9"] = Convert.ToString(dtid12311212ab.Rows[0]["DocumentTypeId"]);
                                        }

                                        SqlCommand cmddraw3231212ab = new SqlCommand("insert into DocumentAccessRightMaster ([DocumentTypeId],[DesignationId],[ViewAccess],[DeleteAccess],[SaveAccess],[EditAccess],[EmailAccess],[FaxAccess],[PrintAccess],[MessageAccess],[CID]) values ('" + ViewState["upload9"] + "','" + ddldesignation.SelectedValue + "','0','0','0','0','0','1','1','0','" + Session["Comid"].ToString() + "')", con);
                                        if (con.State.ToString() != "Open")
                                        {
                                            con.Open();
                                        }
                                        cmddraw3231212ab.ExecuteNonQuery();
                                        con.Close();
                                    }
                                }
                                else
                                {
                                    SqlCommand cmdcancab = new SqlCommand("insert into DocumentMainType(DocumentMainType,CID,Whid) values('Employee','" + Session["Comid"].ToString() + "','" + ddlwarehouse.SelectedValue + "')", con);
                                    if (con.State.ToString() != "Open")
                                    {
                                        con.Open();
                                    }
                                    cmdcancab.ExecuteNonQuery();
                                    con.Close();

                                    SqlDataAdapter dasdsdsdsd = new SqlDataAdapter("select max(DocumentMainTypeId) as DocumentMainTypeId from DocumentMainType", con);
                                    DataTable dtsdsd = new DataTable();
                                    dasdsdsdsd.Fill(dtsdsd);

                                    if (dtsdsd.Rows.Count > 0)
                                    {
                                        SqlCommand cmdcancab1 = new SqlCommand("insert into DocumentSubType(DocumentMainTypeId,DocumentSubType,CID) values('" + dtsdsd.Rows[0]["DocumentMainTypeId"].ToString() + "','" + txtlastname.Text + ' ' + txtfirstname.Text + ' ' + txtintialis.Text + "','" + Session["Comid"].ToString() + "')", con);
                                        if (con.State.ToString() != "Open")
                                        {
                                            con.Open();
                                        }
                                        cmdcancab1.ExecuteNonQuery();
                                        con.Close();

                                        SqlDataAdapter dafindfol1 = new SqlDataAdapter("select max(DocumentSubTypeId) as DocumentSubTypeId from DocumentSubType", con);
                                        DataTable dtfindfol1 = new DataTable();
                                        dafindfol1.Fill(dtfindfol1);

                                        if (dtfindfol1.Rows.Count > 0)
                                        {
                                            SqlDataAdapter dadraw = new SqlDataAdapter("select * from DrawerAccessRightsMaster where DrawerId='" + dtfindfol1.Rows[0]["DocumentSubTypeId"].ToString() + "' and DesignationId='" + ddldesignation.SelectedValue + "' ", con);
                                            DataTable dtdraw = new DataTable();
                                            dadraw.Fill(dtdraw);

                                            if (dtdraw.Rows.Count > 0)
                                            {
                                                SqlCommand cmddraw = new SqlCommand("update DrawerAccessRightsMaster set ViewAccess='1' where DrawerId='" + dtfindfol1.Rows[0]["DocumentSubTypeId"].ToString() + "' and DesignationId='" + ddldesignation.SelectedValue + "'", con);
                                                if (con.State.ToString() != "Open")
                                                {
                                                    con.Open();
                                                }
                                                cmddraw.ExecuteNonQuery();
                                                con.Close();
                                            }
                                            else
                                            {
                                                SqlCommand cmddraw1 = new SqlCommand("insert into DrawerAccessRightsMaster ([DrawerId],[DesignationId],[ViewAccess],[DeleteAccess],[SaveAccess],[EditAccess],[EmailAccess],[FaxAccess],[PrintAccess],[MessageAccess]) values ('" + dtfindfol1.Rows[0]["DocumentSubTypeId"].ToString() + "','" + ddldesignation.SelectedValue + "','1','0','0','0','0','1','1','0')", con);
                                                if (con.State.ToString() != "Open")
                                                {
                                                    con.Open();
                                                }
                                                cmddraw1.ExecuteNonQuery();
                                                con.Close();
                                            }

                                            SqlCommand cmdcanres = new SqlCommand("insert into DocumentType(DocumentSubTypeId,DocumentType,CID) values('" + dtfindfol1.Rows[0]["DocumentSubTypeId"].ToString() + "','Contract','" + Session["Comid"].ToString() + "')", con);
                                            if (con.State.ToString() != "Open")
                                            {
                                                con.Open();
                                            }
                                            cmdcanres.ExecuteNonQuery();
                                            con.Close();

                                            SqlDataAdapter daid1 = new SqlDataAdapter("select max(DocumentTypeId) as DocumentTypeId from DocumentType where DocumentSubTypeId='" + dtfindfol1.Rows[0]["DocumentSubTypeId"].ToString() + "' and DocumentType='Contract'", con);
                                            DataTable dtid1 = new DataTable();
                                            daid1.Fill(dtid1);

                                            if (dtid1.Rows.Count > 0)
                                            {
                                                ViewState["upload1"] = Convert.ToString(dtid1.Rows[0]["DocumentTypeId"]);
                                            }

                                            SqlCommand cmddraw123 = new SqlCommand("insert into DocumentAccessRightMaster ([DocumentTypeId],[DesignationId],[ViewAccess],[DeleteAccess],[SaveAccess],[EditAccess],[EmailAccess],[FaxAccess],[PrintAccess],[MessageAccess],[CID]) values ('" + ViewState["upload1"] + "','" + ddldesignation.SelectedValue + "','1','0','0','0','0','1','1','0','" + Session["Comid"].ToString() + "')", con);
                                            if (con.State.ToString() != "Open")
                                            {
                                                con.Open();
                                            }
                                            cmddraw123.ExecuteNonQuery();
                                            con.Close();


                                            SqlCommand cmdcanid = new SqlCommand("insert into DocumentType(DocumentSubTypeId,DocumentType,CID) values('" + dtfindfol1.Rows[0]["DocumentSubTypeId"].ToString() + "','Resume','" + Session["Comid"].ToString() + "')", con);
                                            if (con.State.ToString() != "Open")
                                            {
                                                con.Open();
                                            }
                                            cmdcanid.ExecuteNonQuery();
                                            con.Close();

                                            SqlDataAdapter daid11 = new SqlDataAdapter("select max(DocumentTypeId) as DocumentTypeId from DocumentType where DocumentSubTypeId='" + dtfindfol1.Rows[0]["DocumentSubTypeId"].ToString() + "' and DocumentType='Resume'", con);
                                            DataTable dtid11 = new DataTable();
                                            daid11.Fill(dtid11);

                                            if (dtid11.Rows.Count > 0)
                                            {
                                                ViewState["upload2"] = Convert.ToString(dtid11.Rows[0]["DocumentTypeId"]);
                                            }


                                            SqlCommand cmddraw223 = new SqlCommand("insert into DocumentAccessRightMaster ([DocumentTypeId],[DesignationId],[ViewAccess],[DeleteAccess],[SaveAccess],[EditAccess],[EmailAccess],[FaxAccess],[PrintAccess],[MessageAccess],[CID]) values ('" + ViewState["upload2"] + "','" + ddldesignation.SelectedValue + "','1','0','0','0','0','1','1','0','" + Session["Comid"].ToString() + "')", con);
                                            if (con.State.ToString() != "Open")
                                            {
                                                con.Open();
                                            }
                                            cmddraw223.ExecuteNonQuery();
                                            con.Close();



                                            SqlCommand cmdcanmydoc = new SqlCommand("insert into DocumentType(DocumentSubTypeId,DocumentType,CID) values('" + dtfindfol1.Rows[0]["DocumentSubTypeId"].ToString() + "','Employee ID','" + Session["Comid"].ToString() + "')", con);
                                            if (con.State.ToString() != "Open")
                                            {
                                                con.Open();
                                            }
                                            cmdcanmydoc.ExecuteNonQuery();
                                            con.Close();

                                            SqlDataAdapter daid1231 = new SqlDataAdapter("select max(DocumentTypeId) as DocumentTypeId from DocumentType where DocumentSubTypeId='" + dtfindfol1.Rows[0]["DocumentSubTypeId"].ToString() + "' and DocumentType='Employee ID'", con);
                                            DataTable dtid1231 = new DataTable();
                                            daid1231.Fill(dtid1231);

                                            if (dtid1231.Rows.Count > 0)
                                            {
                                                ViewState["upload3"] = Convert.ToString(dtid1231.Rows[0]["DocumentTypeId"]);
                                            }


                                            SqlCommand cmddraw323 = new SqlCommand("insert into DocumentAccessRightMaster ([DocumentTypeId],[DesignationId],[ViewAccess],[DeleteAccess],[SaveAccess],[EditAccess],[EmailAccess],[FaxAccess],[PrintAccess],[MessageAccess],[CID]) values ('" + ViewState["upload3"] + "','" + ddldesignation.SelectedValue + "','1','0','0','0','0','1','1','0','" + Session["Comid"].ToString() + "')", con);
                                            if (con.State.ToString() != "Open")
                                            {
                                                con.Open();
                                            }
                                            cmddraw323.ExecuteNonQuery();
                                            con.Close();


                                            SqlCommand cmdcanmydoc1 = new SqlCommand("insert into DocumentType(DocumentSubTypeId,DocumentType,CID) values('" + dtfindfol1.Rows[0]["DocumentSubTypeId"].ToString() + "','Salary Slip','" + Session["Comid"].ToString() + "')", con);
                                            if (con.State.ToString() != "Open")
                                            {
                                                con.Open();
                                            }
                                            cmdcanmydoc1.ExecuteNonQuery();
                                            con.Close();

                                            SqlDataAdapter daid12311 = new SqlDataAdapter("select max(DocumentTypeId) as DocumentTypeId from DocumentType where DocumentSubTypeId='" + dtfindfol1.Rows[0]["DocumentSubTypeId"].ToString() + "' and DocumentType='Salary Slip'", con);
                                            DataTable dtid12311 = new DataTable();
                                            daid12311.Fill(dtid12311);

                                            if (dtid12311.Rows.Count > 0)
                                            {
                                                ViewState["upload4"] = Convert.ToString(dtid12311.Rows[0]["DocumentTypeId"]);
                                            }


                                            SqlCommand cmddraw3231 = new SqlCommand("insert into DocumentAccessRightMaster ([DocumentTypeId],[DesignationId],[ViewAccess],[DeleteAccess],[SaveAccess],[EditAccess],[EmailAccess],[FaxAccess],[PrintAccess],[MessageAccess],[CID]) values ('" + ViewState["upload4"] + "','" + ddldesignation.SelectedValue + "','1','0','0','0','0','1','1','0','" + Session["Comid"].ToString() + "')", con);
                                            if (con.State.ToString() != "Open")
                                            {
                                                con.Open();
                                            }
                                            cmddraw3231.ExecuteNonQuery();
                                            con.Close();






                                            SqlCommand cmdcanmydoc12 = new SqlCommand("insert into DocumentType(DocumentSubTypeId,DocumentType,CID) values('" + dtfindfol1.Rows[0]["DocumentSubTypeId"].ToString() + "','Tax Document','" + Session["Comid"].ToString() + "')", con);
                                            if (con.State.ToString() != "Open")
                                            {
                                                con.Open();
                                            }
                                            cmdcanmydoc12.ExecuteNonQuery();
                                            con.Close();

                                            SqlDataAdapter daid123112 = new SqlDataAdapter("select max(DocumentTypeId) as DocumentTypeId from DocumentType where DocumentSubTypeId='" + dtfindfol1.Rows[0]["DocumentSubTypeId"].ToString() + "' and DocumentType='Tax Document'", con);
                                            DataTable dtid123112 = new DataTable();
                                            daid123112.Fill(dtid123112);

                                            if (dtid123112.Rows.Count > 0)
                                            {
                                                ViewState["upload5"] = Convert.ToString(dtid123112.Rows[0]["DocumentTypeId"]);
                                            }


                                            SqlCommand cmddraw32312 = new SqlCommand("insert into DocumentAccessRightMaster ([DocumentTypeId],[DesignationId],[ViewAccess],[DeleteAccess],[SaveAccess],[EditAccess],[EmailAccess],[FaxAccess],[PrintAccess],[MessageAccess],[CID]) values ('" + ViewState["upload5"] + "','" + ddldesignation.SelectedValue + "','1','0','0','0','0','1','1','0','" + Session["Comid"].ToString() + "')", con);
                                            if (con.State.ToString() != "Open")
                                            {
                                                con.Open();
                                            }
                                            cmddraw32312.ExecuteNonQuery();
                                            con.Close();





                                            SqlCommand cmdcanmydoc121 = new SqlCommand("insert into DocumentType(DocumentSubTypeId,DocumentType,CID) values('" + dtfindfol1.Rows[0]["DocumentSubTypeId"].ToString() + "','document from employee','" + Session["Comid"].ToString() + "')", con);
                                            if (con.State.ToString() != "Open")
                                            {
                                                con.Open();
                                            }
                                            cmdcanmydoc121.ExecuteNonQuery();
                                            con.Close();

                                            SqlDataAdapter daid1231121 = new SqlDataAdapter("select max(DocumentTypeId) as DocumentTypeId from DocumentType where DocumentSubTypeId='" + dtfindfol1.Rows[0]["DocumentSubTypeId"].ToString() + "' and DocumentType='document from employee'", con);
                                            DataTable dtid1231121 = new DataTable();
                                            daid1231121.Fill(dtid1231121);

                                            if (dtid1231121.Rows.Count > 0)
                                            {
                                                ViewState["upload6"] = Convert.ToString(dtid1231121.Rows[0]["DocumentTypeId"]);
                                            }


                                            SqlCommand cmddraw323121 = new SqlCommand("insert into DocumentAccessRightMaster ([DocumentTypeId],[DesignationId],[ViewAccess],[DeleteAccess],[SaveAccess],[EditAccess],[EmailAccess],[FaxAccess],[PrintAccess],[MessageAccess],[CID]) values ('" + ViewState["upload6"] + "','" + ddldesignation.SelectedValue + "','1','0','0','0','0','1','1','0','" + Session["Comid"].ToString() + "')", con);
                                            if (con.State.ToString() != "Open")
                                            {
                                                con.Open();
                                            }
                                            cmddraw323121.ExecuteNonQuery();
                                            con.Close();









                                            SqlCommand cmdcanmydoc1212 = new SqlCommand("insert into DocumentType(DocumentSubTypeId,DocumentType,CID) values('" + dtfindfol1.Rows[0]["DocumentSubTypeId"].ToString() + "','document for approval','" + Session["Comid"].ToString() + "')", con);
                                            if (con.State.ToString() != "Open")
                                            {
                                                con.Open();
                                            }
                                            cmdcanmydoc1212.ExecuteNonQuery();
                                            con.Close();

                                            SqlDataAdapter daid12311212 = new SqlDataAdapter("select max(DocumentTypeId) as DocumentTypeId from DocumentType where DocumentSubTypeId='" + dtfindfol1.Rows[0]["DocumentSubTypeId"].ToString() + "' and DocumentType='document for approval'", con);
                                            DataTable dtid12311212 = new DataTable();
                                            daid12311212.Fill(dtid12311212);

                                            if (dtid12311212.Rows.Count > 0)
                                            {
                                                ViewState["upload7"] = Convert.ToString(dtid12311212.Rows[0]["DocumentTypeId"]);
                                            }


                                            SqlCommand cmddraw3231212 = new SqlCommand("insert into DocumentAccessRightMaster ([DocumentTypeId],[DesignationId],[ViewAccess],[DeleteAccess],[SaveAccess],[EditAccess],[EmailAccess],[FaxAccess],[PrintAccess],[MessageAccess],[CID]) values ('" + ViewState["upload7"] + "','" + ddldesignation.SelectedValue + "','1','0','0','0','0','1','1','0','" + Session["Comid"].ToString() + "')", con);
                                            if (con.State.ToString() != "Open")
                                            {
                                                con.Open();
                                            }
                                            cmddraw3231212.ExecuteNonQuery();
                                            con.Close();



                                            SqlCommand cmdcanmydoc1212a = new SqlCommand("insert into DocumentType(DocumentSubTypeId,DocumentType,CID) values('" + dtfindfol1.Rows[0]["DocumentSubTypeId"].ToString() + "','Alimony Maintenance Court Orders','" + Session["Comid"].ToString() + "')", con);
                                            if (con.State.ToString() != "Open")
                                            {
                                                con.Open();
                                            }
                                            cmdcanmydoc1212a.ExecuteNonQuery();
                                            con.Close();

                                            SqlDataAdapter daid12311212a = new SqlDataAdapter("select max(DocumentTypeId) as DocumentTypeId from DocumentType where DocumentSubTypeId='" + dtfindfol1.Rows[0]["DocumentSubTypeId"].ToString() + "' and DocumentType='Alimony Maintenance Court Orders'", con);
                                            DataTable dtid12311212a = new DataTable();
                                            daid12311212a.Fill(dtid12311212a);

                                            if (dtid12311212a.Rows.Count > 0)
                                            {
                                                ViewState["upload8"] = Convert.ToString(dtid12311212a.Rows[0]["DocumentTypeId"]);
                                            }

                                            SqlCommand cmddraw3231212a = new SqlCommand("insert into DocumentAccessRightMaster ([DocumentTypeId],[DesignationId],[ViewAccess],[DeleteAccess],[SaveAccess],[EditAccess],[EmailAccess],[FaxAccess],[PrintAccess],[MessageAccess],[CID]) values ('" + ViewState["upload8"] + "','" + ddldesignation.SelectedValue + "','1','0','0','0','0','1','1','0','" + Session["Comid"].ToString() + "')", con);
                                            if (con.State.ToString() != "Open")
                                            {
                                                con.Open();
                                            }
                                            cmddraw3231212a.ExecuteNonQuery();
                                            con.Close();



                                            SqlCommand cmdcanmydoc1212ab = new SqlCommand("insert into DocumentType(DocumentSubTypeId,DocumentType,CID) values('" + dtfindfol1.Rows[0]["DocumentSubTypeId"].ToString() + "','Confidential Records','" + Session["Comid"].ToString() + "')", con);
                                            if (con.State.ToString() != "Open")
                                            {
                                                con.Open();
                                            }
                                            cmdcanmydoc1212ab.ExecuteNonQuery();
                                            con.Close();

                                            SqlDataAdapter daid12311212ab = new SqlDataAdapter("select max(DocumentTypeId) as DocumentTypeId from DocumentType where DocumentSubTypeId='" + dtfindfol1.Rows[0]["DocumentSubTypeId"].ToString() + "' and DocumentType='Confidential Records'", con);
                                            DataTable dtid12311212ab = new DataTable();
                                            daid12311212ab.Fill(dtid12311212ab);

                                            if (dtid12311212ab.Rows.Count > 0)
                                            {
                                                ViewState["upload9"] = Convert.ToString(dtid12311212ab.Rows[0]["DocumentTypeId"]);
                                            }

                                            SqlCommand cmddraw3231212ab = new SqlCommand("insert into DocumentAccessRightMaster ([DocumentTypeId],[DesignationId],[ViewAccess],[DeleteAccess],[SaveAccess],[EditAccess],[EmailAccess],[FaxAccess],[PrintAccess],[MessageAccess],[CID]) values ('" + ViewState["upload9"] + "','" + ddldesignation.SelectedValue + "','0','0','0','0','0','1','1','0','" + Session["Comid"].ToString() + "')", con);
                                            if (con.State.ToString() != "Open")
                                            {
                                                con.Open();
                                            }
                                            cmddraw3231212ab.ExecuteNonQuery();
                                            con.Close();
                                        }
                                    }
                                }

                                ////-----End for Add Cabinet Drawer Folder----/////

                                ////-----Add Rule For Employee------////




                                string str1112 = "SELECT * FROM RuleTypeMaster where RuleType = 'Employee Rule' and Whid='" + ddlwarehouse.SelectedValue + "' ";

                                SqlCommand cmd1111 = new SqlCommand(str1112, con);
                                cmd1111.CommandType = CommandType.Text;
                                SqlDataAdapter da1111 = new SqlDataAdapter(cmd1111);
                                DataTable dt1111 = new DataTable();
                                da1111.Fill(dt1111);

                                if (dt1111.Rows.Count > 0)
                                {
                                    DataTable dsdsds1 = select("select [DocumentSubTypeId] from [DocumentType] where [DocumentTypeId]='" + ViewState["upload7"].ToString() + "'");

                                    DataTable dsdsds12 = select("select [DocumentMainTypeId] from [DocumentSubType] where [DocumentSubTypeId]='" + dsdsds1.Rows[0]["DocumentSubTypeId"].ToString() + "'");

                                    SqlCommand cmdrulet11 = new SqlCommand("insert into [RuleMaster] ([RuleTypeId],[DocumentTypeId],[RuleDate],[RuleTitle],[ConditionTypeId],[CID],[DocumentMainId],[DocumentSubId] ,[Whid],[Approvemail],[Active]) values('" + dt1111.Rows[0]["RuleTypeId"].ToString() + "','" + ViewState["upload7"].ToString() + "','" + DateTime.Now.ToShortDateString() + "','Approval Required from Employee - " + txtfirstname.Text.Substring(0, 1).ToString() + ' ' + txtintialis.Text + ' ' + txtlastname.Text + "','1','" + Session["Comid"].ToString() + "', '" + dsdsds12.Rows[0]["DocumentMainTypeId"].ToString() + "', '" + dsdsds1.Rows[0]["DocumentSubTypeId"].ToString() + "','" + ddlwarehouse.SelectedValue + "','0','1')", con);
                                    if (con.State.ToString() != "Open")
                                    {
                                        con.Open();
                                    }
                                    cmdrulet11.ExecuteNonQuery();
                                    con.Close();

                                    DataTable dtrulede = select("select max(RuleId) as RuleId from RuleMaster");

                                    DataTable dtruleapprove = select("select * from RuleApproveTypeMaster where RuleApproveTypeName='Employee Acceptance of Document Content' and CID='" + Session["Comid"].ToString() + "' and Whid='" + ddlwarehouse.SelectedValue + "'");

                                    if (dtruleapprove.Rows.Count > 0)
                                    {
                                        SqlCommand cmdruletapprove = new SqlCommand("insert into [RuleDetail] ([RuleId],[EmployeeId],[RuleApproveTypeId],[StepId],[Days]) values('" + dtrulede.Rows[0]["RuleId"].ToString() + "','" + ViewState["EmerEMID"].ToString() + "','" + dtruleapprove.Rows[0]["RuleApproveTypeId"].ToString() + "','1','1')", con);
                                        if (con.State.ToString() != "Open")
                                        {
                                            con.Open();
                                        }
                                        cmdruletapprove.ExecuteNonQuery();
                                        con.Close();
                                    }
                                    else
                                    {
                                        SqlCommand cmdruletapprovetype = new SqlCommand("insert into [RuleApproveTypeMaster] ([RuleApproveTypeName],[CID],[Description],[Whid]) values('Employee Acceptance of Document Content','" + Session["Comid"].ToString() + "','Employee Acceptance of Document Content','" + ddlwarehouse.SelectedValue + "')", con);
                                        if (con.State.ToString() != "Open")
                                        {
                                            con.Open();
                                        }
                                        cmdruletapprovetype.ExecuteNonQuery();
                                        con.Close();

                                        DataTable dtfvcv = select("select max(RuleApproveTypeId) as RuleApproveTypeId from RuleApproveTypeMaster");

                                        SqlCommand cmdruletapprove11 = new SqlCommand("insert into [RuleDetail] ([RuleId],[EmployeeId],[RuleApproveTypeId],[StepId],[Days]) values('" + dtrulede.Rows[0]["RuleId"].ToString() + "','" + ViewState["EmerEMID"].ToString() + "','" + dtfvcv.Rows[0]["RuleApproveTypeId"].ToString() + "','1','1')", con);
                                        if (con.State.ToString() != "Open")
                                        {
                                            con.Open();
                                        }
                                        cmdruletapprove11.ExecuteNonQuery();
                                        con.Close();
                                    }
                                }
                                else
                                {
                                    SqlCommand cmdrulet = new SqlCommand("insert into [RuleTypeMaster] ([RuleType],[CID],[Whid]) values('Employee Rule','" + Session["Comid"].ToString() + "','" + ddlwarehouse.SelectedValue + "')", con);
                                    if (con.State.ToString() != "Open")
                                    {
                                        con.Open();
                                    }
                                    cmdrulet.ExecuteNonQuery();
                                    con.Close();

                                    DataTable dabang = select("select max(RuleTypeId) as RuleTypeId from RuleTypeMaster");

                                    if (dabang.Rows.Count > 0)
                                    {
                                        DataTable dsdsds1 = select("select [DocumentSubTypeId] from [DocumentType] where [DocumentTypeId]='" + ViewState["upload7"].ToString() + "'");

                                        DataTable dsdsds12 = select("select [DocumentMainTypeId] from [DocumentSubType] where [DocumentSubTypeId]='" + dsdsds1.Rows[0]["DocumentSubTypeId"].ToString() + "'");

                                        SqlCommand cmdrulet11 = new SqlCommand("insert into [RuleMaster] ([RuleTypeId],[DocumentTypeId],[RuleDate],[RuleTitle],[ConditionTypeId],[CID],[DocumentMainId],[DocumentSubId] ,[Whid],[Approvemail],[Active]) values('" + dabang.Rows[0]["RuleTypeId"].ToString() + "','" + ViewState["upload7"].ToString() + "','" + DateTime.Now.ToShortDateString() + "','Approval Required from Employee - " + txtfirstname.Text.Substring(0, 1).ToString() + ' ' + txtintialis.Text + ' ' + txtlastname.Text + "','1','" + Session["Comid"].ToString() + "', '" + dsdsds12.Rows[0]["DocumentMainTypeId"].ToString() + "', '" + dsdsds1.Rows[0]["DocumentSubTypeId"].ToString() + "','" + ddlwarehouse.SelectedValue + "','0','1')", con);
                                        if (con.State.ToString() != "Open")
                                        {
                                            con.Open();
                                        }
                                        cmdrulet11.ExecuteNonQuery();
                                        con.Close();

                                        DataTable dtrulede = select("select max(RuleId) as RuleId from RuleMaster");

                                        DataTable dtruleapprove = select("select * from RuleApproveTypeMaster where RuleApproveTypeName='Employee Acceptance of Document Content' and CID='" + Session["Comid"].ToString() + "' and Whid='" + ddlwarehouse.SelectedValue + "'");

                                        if (dtruleapprove.Rows.Count > 0)
                                        {
                                            SqlCommand cmdruletapprove = new SqlCommand("insert into [RuleDetail] ([RuleId],[EmployeeId],[RuleApproveTypeId],[StepId],[Days]) values('" + dtrulede.Rows[0]["RuleId"].ToString() + "','" + ViewState["EmerEMID"].ToString() + "','" + dtruleapprove.Rows[0]["RuleApproveTypeId"].ToString() + "','1','1')", con);
                                            if (con.State.ToString() != "Open")
                                            {
                                                con.Open();
                                            }
                                            cmdruletapprove.ExecuteNonQuery();
                                            con.Close();
                                        }
                                        else
                                        {
                                            SqlCommand cmdruletapprovetype = new SqlCommand("insert into [RuleApproveTypeMaster] ([RuleApproveTypeName],[CID],[Description],[Whid]) values('Employee Acceptance of Document Content','" + Session["Comid"].ToString() + "','Employee Acceptance of Document Content','" + ddlwarehouse.SelectedValue + "')", con);
                                            if (con.State.ToString() != "Open")
                                            {
                                                con.Open();
                                            }
                                            cmdruletapprovetype.ExecuteNonQuery();
                                            con.Close();

                                            DataTable dtfvcv = select("select max(RuleApproveTypeId) as RuleApproveTypeId from RuleApproveTypeMaster");

                                            SqlCommand cmdruletapprove11 = new SqlCommand("insert into [RuleDetail] ([RuleId],[EmployeeId],[RuleApproveTypeId],[StepId],[Days]) values('" + dtrulede.Rows[0]["RuleId"].ToString() + "','" + ViewState["EmerEMID"].ToString() + "','" + dtfvcv.Rows[0]["RuleApproveTypeId"].ToString() + "','1','1')", con);
                                            if (con.State.ToString() != "Open")
                                            {
                                                con.Open();
                                            }
                                            cmdruletapprove11.ExecuteNonQuery();
                                            con.Close();
                                        }
                                    }
                                }


                              //  int ff = ViewState["eId"];
                                string insertSyncr = "insert into Syncr_LicenseEmployee_With_JobcenterId(License_Emp_id,Jobcenter_Emp_id)values('" + ViewState["eId"].ToString() + "','" + ViewState["EmerEMID"].ToString() + "')";
                                SqlCommand nisync = new SqlCommand(insertSyncr, con);
                                if (con.State.ToString() != "Open")
                                {
                                    con.Open();
                                }
                                nisync.ExecuteNonQuery();
                                con.Close();

                                string ind = "insert into TBLUserLoginIpRestrictionPreference(compid,userid,MakeIPRestriction)values('" + Session["comid"] + "','" + ViewState["userid"] + "',0)";
                                SqlCommand ind1 = new SqlCommand(ind, con);
                                if (con.State.ToString() != "Open")
                                {
                                    con.Open();
                                }
                                ind1.ExecuteNonQuery();
                                con.Close();




                                /////-------End for Add Rule------//////

                                UploadDocumets();

                                if (RadioButtonList2.SelectedValue == "1" && tbEmail.Text != "")
                                {
                                    bool success = sendmail(tbEmail.Text);
                                    if (success == true)
                                    {
                                        lblmsg.Text = "";
                                        lblmsg.Text = "Record inserted successfully";
                                    }
                                }
                                else
                                {
                                    lblmsg.Text = "";
                                    lblmsg.Text = "Record inserted successfully";
                                }
                                fillgriddata();
                                clear();
                                pnllabeladdress.Visible = false;
                                addnewaddrpnl.Visible = true;
                                addnewaddr.Visible = false;
                                pnllastaddress.Visible = false;

                                Panel6.Visible = false;

                                //lbladd.Text = "";
                                pnlparty.Visible = false;

                                btnshowparty.Visible = true;
                                MultiView1.ActiveViewIndex = 0;
                                lblmsg.Text = "";
                                lblmsg.Text = "Record inserted successfully";
                            }
                            else
                            {
                                lblmsg.Text = "";
                                lblmsg.Text = "Sorry, You don't permited greter record to priceplan";
                            }
                        
                        }
                    }
                    else if (acce == 2)
                    {
                        lblmsg.Visible = true;
                        lblmsg.Text = "Sorry,Email Id is already used";
                    }
#endregion
        //////////    }
            }
        }
                }
            }
        }
    }
    
    
    //////////}
        //////////        }
        //////////    }


        //int i1 = 0;

        //for (i1 = 1000; i1 <= 9999; i1++)
        //{
        //    string strusercheck = " select EmployeeNo from EmployeePayrollMaster where Compid='" + Session["Comid"].ToString() + "' and EmployeeNo='" + i1 + "'";
        //    SqlCommand cmdusercheck = new SqlCommand(strusercheck, con);
        //    SqlDataAdapter adpusercheck = new SqlDataAdapter(cmdusercheck);
        //    DataTable dsusercheck = new DataTable();
        //    adpusercheck.Fill(dsusercheck);

        //    if (dsusercheck.Rows.Count > 0)
        //    {
        //        TextBox1.Text = (i1 + 1).ToString();
        //    }
        //    else
        //    {
        //        TextBox1.Text = i1.ToString();
        //        break;
        //    }

        //}
       /////// }

    protected void imgbtnupdate_Click(object sender, EventArgs e)
    {       
        //string strusercodecheck = " select * from EmployeeBarcodeMaster inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID=EmployeeBarcodeMaster.Employee_Id inner join Party_master on Party_master.PartyID=EmployeeMaster.PartyID where EmployeeBarcodeMaster.Employeecode='" + txtsecuritycode.Text + "' and Party_master.id='" + Session["comid"] + "' and EmployeeBarcodeMaster.Employee_Id <>'" + ViewState["editid"] + "'";
        //SqlCommand cmdusercodecheck = new SqlCommand(strusercodecheck, con);
        //SqlDataAdapter adpusercodecheck = new SqlDataAdapter(cmdusercodecheck);
        //DataTable dsusercodecheck = new DataTable();
        //adpusercodecheck.Fill(dsusercodecheck);

        //if (dsusercodecheck.Rows.Count > 0)
        //{
        //    lblmsg.Visible = true;
        //    lblmsg.Text = "This employee code is already used.";
        //}
        //else
        //{
        int flag = 0;
        string strusernaemchk = " select * from User_master inner join Party_master on User_master.PartyID = party_master.PartyID inner join EmployeeMaster on party_master.PartyID = EmployeeMaster.PartyID where Username ='" + tbUserName.Text + "' and EmployeeMaster.EmployeeMasterID <>'" + ViewState["editid"] + "'";
        SqlCommand cmdusernaemchk = new SqlCommand(strusernaemchk, con);
        SqlDataAdapter adpusernaemchk = new SqlDataAdapter(cmdusernaemchk);
        DataTable dsusernaemchk = new DataTable();
        adpusernaemchk.Fill(dsusernaemchk);

        if (dsusernaemchk.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "This Username is already used.";
        }
        else
        {
            //if (txtbiometricid.Text != "")
            //{

            //    string strbiometricno = "Select * from EmployeeBarcodeMaster where Biometricno='" + txtbiometricid.Text + "' and Employee_Id<> '" + ViewState["editid"] + "'";
            //    SqlCommand cmdbiometricno = new SqlCommand(strbiometricno, con);
            //    SqlDataAdapter adpbiometricno = new SqlDataAdapter(cmdbiometricno);
            //    DataTable dsbiometricno = new DataTable();
            //    adpbiometricno.Fill(dsbiometricno);
            //    if (dsbiometricno.Rows.Count > 0)
            //    {
            //        flag = 1;
            //        lblmsg.Visible = true;
            //        lblmsg.Text = "This Bio-metric Id is already in use";

            //    }
            //}
            if (txtsecuritycode.Text != "")
            {
                string strbluetoothno = "Select * from EmployeeBarcodeMaster where Employeecode='" + txtsecuritycode.Text + "' and Employee_Id<> '" + ViewState["editid"] + "'";
                SqlCommand cmdbluetoothno = new SqlCommand(strbluetoothno, con);
                SqlDataAdapter adpbluetoothno = new SqlDataAdapter(cmdbluetoothno);
                DataTable dsbluetoothno = new DataTable();
                adpbluetoothno.Fill(dsbluetoothno);
                if (dsbluetoothno.Rows.Count > 0)
                {
                    flag = 1;
                    lblmsg.Visible = true;
                    lblmsg.Text = "This Security Code is already in use.";

                }
            }
            if (txtbarcode.Text != "")
            {
                string strbluetoothno = "Select * from EmployeeBarcodeMaster where Barcode='" + txtbarcode.Text + "' and Employee_Id<> '" + ViewState["editid"] + "'";
                SqlCommand cmdbluetoothno = new SqlCommand(strbluetoothno, con);
                SqlDataAdapter adpbluetoothno = new SqlDataAdapter(cmdbluetoothno);
                DataTable dsbluetoothno = new DataTable();
                adpbluetoothno.Fill(dsbluetoothno);
                if (dsbluetoothno.Rows.Count > 0)
                {
                    flag = 1;
                    lblmsg.Visible = true;
                    lblmsg.Text = "This Barcode is already in use";

                }
            }
            if (flag == 0)
            {


                int acce = 0;
                SqlCommand cmd = new SqlCommand("SELECT Party_master.* FROM  Party_master inner join EmployeeMaster on EmployeeMaster.PartyID=Party_master.PartyID " +
                             "where Party_master.Whid='" + ddlwarehouse.SelectedValue + "' and EmployeeName='" + txtlastname.Text + ' ' + txtfirstname.Text + ' ' + txtintialis.Text + "'  and EmployeeMaster.EmployeeMasterID<>'" + ViewState["editid"] + "' ", con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                adp.Fill(ds);

                if (ds.Rows.Count > 0)
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "Record already exist";
                }
                else
                {
                    bool access = UserAccess.Usercon("Party_Master", lblpno.Text, "PartyId", "", "", "id", "Party_Master");
                    if (access == true)
                    {
                        //SqlCommand cmdec = new SqlCommand("Select Email from Party_master where Email='" + tbEmail.Text + "' and PartyID<>'" + ViewState["partyid"] + "' ", con);

                        SqlCommand cmdec = new SqlCommand("Select Email from Party_master where PartyID='10000000' ", con);

                        SqlDataAdapter adpec = new SqlDataAdapter(cmdec);
                        DataTable dsec = new DataTable();
                        adpec.Fill(dsec);

                        if (dsec.Rows.Count == 0)
                        {
                            if (dsec.Rows.Count == 0)
                            {

                                acce = 0;
                            }
                            else
                            {
                                if (tbEmail.Text.Length > 0)
                                {
                                    acce = 2;

                                }
                                else
                                {
                                    acce = 0;
                                }
                            }
                            if (acce == 0)
                            {
                                //btnCancel.Enabled = false;
                                //qryStr = " insert into AccountMaster(ClassId,AccountId,GroupId,AccountName,Description,Balance,Date,InventoryFlag,BalanceOfLastYear,DateTimeOfLastUpdatedBalance,Status) " +
                                //                              " values ('" + classid + "','" + accid + "','" + groupid + "','" + tbName.Text + "','New Party','0'," + System.DateTime.Now.ToShortDateString() + ",'0','0','" + System.DateTime.Now.ToShortDateString() + "','" + ddlActive.SelectedValue + "')";



                                string stremp = "";

                                if (chkeduquali.Checked == true && chkspecialsub.Checked == false && chkjobposition.Checked == false)
                                {
                                    SqlCommand cmd1 = new SqlCommand("insert into AreaofStudiesTbl values('" + txteduquali.Text + "',0)", con);
                                    if (con.State.ToString() != "Open")
                                    {
                                        con.Open();
                                    }
                                    cmd1.ExecuteNonQuery();
                                    con.Close();

                                    SqlDataAdapter da1 = new SqlDataAdapter("select max(ID) as ID from AreaofStudiesTbl", con);
                                    DataTable dt1 = new DataTable();
                                    da1.Fill(dt1);

                                    if (dt1.Rows.Count > 0)
                                    {
                                        stremp = " Update EmployeeMaster set DeptID='" + ddldept.SelectedValue + "',DesignationMasterId='" + ddldesignation.SelectedValue + "',EmployeeTypeId='" + ddlemptype.SelectedValue + "', " +
                                        " DateOfJoin='" + txtjoindate.Text + "',Address='" + tbAddress.Text + "',CountryId='" + ddlCountry.SelectedValue + "',StateId='" + ddlState.SelectedValue + "',City='" + ddlCity.SelectedValue + "',ContactNo='" + tbPhone.Text + "',Email='" + tbEmail.Text + "',EmployeeName='" + txtlastname.Text + ' ' + txtfirstname.Text + ' ' + txtintialis.Text + "',Whid='" + ddlwarehouse.SelectedValue + "',SuprviserId='" + ddlemp.SelectedValue + "',Active='" + ddlActive.SelectedValue + "',WorkPhone = '" + workphone.Text + "',WorkExt='" + workext.Text + "',WorkEmail='" + txtworkemail.Text + "',EducationqualificationID='" + dt1.Rows[0]["ID"].ToString() + "',SpecialSubjectID='" + ddlspecialsub.SelectedValue + "',yearofexperience='" + txtyearexpr.Text + "',Jobpositionid='" + ddljobposition.SelectedValue + "',sex='" + Radiogender.SelectedValue + "' where EmployeeMasterID='" + ViewState["editid"] + "'";


                                    }
                                }


                                else if (chkeduquali.Checked == false && chkspecialsub.Checked == true && chkjobposition.Checked == false)
                                {
                                    SqlCommand cmd1 = new SqlCommand("insert into SpecialisedSubjectTBL(SubjectName,Status,AreaofStudiesId,compid) values('" + txtspecialsub.Text + "',0,'" + ddleduquali.SelectedValue + "','" + Session["Comid"] + "')", con);
                                    if (con.State.ToString() != "Open")
                                    {
                                        con.Open();
                                    }
                                    cmd1.ExecuteNonQuery();
                                    con.Close();

                                    SqlDataAdapter da1 = new SqlDataAdapter("select max(ID) as ID from SpecialisedSubjectTBL", con);
                                    DataTable dt1 = new DataTable();
                                    da1.Fill(dt1);

                                    if (dt1.Rows.Count > 0)
                                    {

                                        stremp = "Update EmployeeMaster set DeptID='" + ddldept.SelectedValue + "',DesignationMasterId='" + ddldesignation.SelectedValue + "',EmployeeTypeId='" + ddlemptype.SelectedValue + "', " +
                                        " DateOfJoin='" + txtjoindate.Text + "',Address='" + tbAddress.Text + "',CountryId='" + ddlCountry.SelectedValue + "',StateId='" + ddlState.SelectedValue + "',City='" + ddlCity.SelectedValue + "',ContactNo='" + tbPhone.Text + "',Email='" + tbEmail.Text + "',EmployeeName='" + txtlastname.Text + ' ' + txtfirstname.Text + ' ' + txtintialis.Text + "',Whid='" + ddlwarehouse.SelectedValue + "',SuprviserId='" + ddlemp.SelectedValue + "',Active='" + ddlActive.SelectedValue + "',WorkPhone = '" + workphone.Text + "',WorkExt='" + workext.Text + "',WorkEmail='" + txtworkemail.Text + "',EducationqualificationID='" + ddleduquali.SelectedValue + "',SpecialSubjectID='" + dt1.Rows[0]["ID"].ToString() + "',yearofexperience='" + txtyearexpr.Text + "',Jobpositionid='" + ddljobposition.SelectedValue + "',sex='" + Radiogender.SelectedValue + "' where EmployeeMasterID='" + ViewState["editid"] + "'";


                                    }
                                }

                                else if (chkeduquali.Checked == false && chkspecialsub.Checked == false && chkjobposition.Checked == true)
                                {
                                    SqlDataAdapter da11 = new SqlDataAdapter("select ID from VacancyTypeMaster where Name='Other'", con);
                                    DataTable dt11 = new DataTable();
                                    da11.Fill(dt11);

                                    SqlCommand cmd1 = new SqlCommand("insert into VacancyPositionTitleMaster values('" + dt11.Rows[0]["ID"].ToString() + "','" + txtjobposition.Text + "',0)", con);
                                    if (con.State.ToString() != "Open")
                                    {
                                        con.Open();
                                    }
                                    cmd1.ExecuteNonQuery();
                                    con.Close();

                                    SqlDataAdapter da1 = new SqlDataAdapter("select max(ID) as ID from VacancyPositionTitleMaster", con);
                                    DataTable dt1 = new DataTable();
                                    da1.Fill(dt1);

                                    if (dt1.Rows.Count > 0)
                                    {

                                        stremp = "Update EmployeeMaster set DeptID='" + ddldept.SelectedValue + "',DesignationMasterId='" + ddldesignation.SelectedValue + "',EmployeeTypeId='" + ddlemptype.SelectedValue + "', " +
                                        " DateOfJoin='" + txtjoindate.Text + "',Address='" + tbAddress.Text + "',CountryId='" + ddlCountry.SelectedValue + "',StateId='" + ddlState.SelectedValue + "',City='" + ddlCity.SelectedValue + "',ContactNo='" + tbPhone.Text + "',Email='" + tbEmail.Text + "',EmployeeName='" + txtlastname.Text + ' ' + txtfirstname.Text + ' ' + txtintialis.Text + "',Whid='" + ddlwarehouse.SelectedValue + "',SuprviserId='" + ddlemp.SelectedValue + "',Active='" + ddlActive.SelectedValue + "',WorkPhone = '" + workphone.Text + "',WorkExt='" + workext.Text + "',WorkEmail='" + txtworkemail.Text + "',EducationqualificationID='" + ddleduquali.SelectedValue + "',SpecialSubjectID='" + ddlspecialsub.SelectedValue + "',yearofexperience='" + txtyearexpr.Text + "',Jobpositionid='" + dt1.Rows[0]["ID"].ToString() + "',sex='" + Radiogender.SelectedValue + "' where EmployeeMasterID='" + ViewState["editid"] + "'";


                                    }
                                }

                                else if (chkeduquali.Checked == true && chkspecialsub.Checked == true && chkjobposition.Checked == true)
                                {
                                    SqlDataAdapter da11 = new SqlDataAdapter("select ID from VacancyTypeMaster where Name='Other'", con);
                                    DataTable dt11 = new DataTable();
                                    da11.Fill(dt11);

                                    SqlCommand cmd1 = new SqlCommand("insert into VacancyPositionTitleMaster values('" + dt11.Rows[0]["ID"].ToString() + "','" + txtjobposition.Text + "',0)", con);
                                    if (con.State.ToString() != "Open")
                                    {
                                        con.Open();
                                    }
                                    cmd1.ExecuteNonQuery();
                                    con.Close();

                                    SqlDataAdapter da1 = new SqlDataAdapter("select max(ID) as ID from VacancyPositionTitleMaster", con);
                                    DataTable dt1 = new DataTable();
                                    da1.Fill(dt1);

                                    SqlCommand cmd100 = new SqlCommand("insert into AreaofStudiesTbl values('" + txteduquali.Text + "',0)", con);
                                    if (con.State.ToString() != "Open")
                                    {
                                        con.Open();
                                    }
                                    cmd100.ExecuteNonQuery();
                                    con.Close();

                                    SqlDataAdapter dass = new SqlDataAdapter("select max(ID) as ID from AreaofStudiesTbl", con);
                                    DataTable dtss = new DataTable();
                                    dass.Fill(dtss);

                                    SqlCommand cmd101 = new SqlCommand("insert into SpecialisedSubjectTBL(SubjectName,Status,AreaofStudiesId,compid) values('" + txtspecialsub.Text + "',0,'" + dtss.Rows[0]["ID"].ToString() + "','" + Session["Comid"] + "')", con);
                                    if (con.State.ToString() != "Open")
                                    {
                                        con.Open();
                                    }
                                    cmd101.ExecuteNonQuery();
                                    con.Close();

                                    SqlDataAdapter dass1 = new SqlDataAdapter("select max(ID) as ID from SpecialisedSubjectTBL", con);
                                    DataTable dtss1 = new DataTable();
                                    dass1.Fill(dtss1);


                                    stremp = "Update EmployeeMaster set DeptID='" + ddldept.SelectedValue + "',DesignationMasterId='" + ddldesignation.SelectedValue + "',EmployeeTypeId='" + ddlemptype.SelectedValue + "', " +
                                        " DateOfJoin='" + txtjoindate.Text + "',Address='" + tbAddress.Text + "',CountryId='" + ddlCountry.SelectedValue + "',StateId='" + ddlState.SelectedValue + "',City='" + ddlCity.SelectedValue + "',ContactNo='" + tbPhone.Text + "',Email='" + tbEmail.Text + "',EmployeeName='" + txtlastname.Text + ' ' + txtfirstname.Text + ' ' + txtintialis.Text + "',Whid='" + ddlwarehouse.SelectedValue + "',SuprviserId='" + ddlemp.SelectedValue + "',Active='" + ddlActive.SelectedValue + "',WorkPhone = '" + workphone.Text + "',WorkExt='" + workext.Text + "',WorkEmail='" + txtworkemail.Text + "',EducationqualificationID='" + dtss.Rows[0]["ID"].ToString() + "',SpecialSubjectID='" + dtss1.Rows[0]["ID"].ToString() + "',yearofexperience='" + txtyearexpr.Text + "',Jobpositionid='" + dt1.Rows[0]["ID"].ToString() + "',sex='" + Radiogender.SelectedValue + "' where EmployeeMasterID='" + ViewState["editid"] + "'";


                                }


                                else if (chkeduquali.Checked == true && chkspecialsub.Checked == true && chkjobposition.Checked == false)
                                {


                                    SqlCommand cmd100 = new SqlCommand("insert into AreaofStudiesTbl values('" + txteduquali.Text + "',0)", con);
                                    if (con.State.ToString() != "Open")
                                    {
                                        con.Open();
                                    }
                                    cmd100.ExecuteNonQuery();
                                    con.Close();

                                    SqlDataAdapter dass = new SqlDataAdapter("select max(ID) as ID from AreaofStudiesTbl", con);
                                    DataTable dtss = new DataTable();
                                    dass.Fill(dtss);

                                    SqlCommand cmd101 = new SqlCommand("insert into SpecialisedSubjectTBL(SubjectName,Status,AreaofStudiesId,compid) values('" + txtspecialsub.Text + "',0,'" + dtss.Rows[0]["ID"].ToString() + "','" + Session["Comid"] + "')", con);
                                    if (con.State.ToString() != "Open")
                                    {
                                        con.Open();
                                    }
                                    cmd101.ExecuteNonQuery();
                                    con.Close();

                                    SqlDataAdapter dass1 = new SqlDataAdapter("select max(ID) as ID from SpecialisedSubjectTBL", con);
                                    DataTable dtss1 = new DataTable();
                                    dass1.Fill(dtss1);


                                    stremp = "Update EmployeeMaster set DeptID='" + ddldept.SelectedValue + "',DesignationMasterId='" + ddldesignation.SelectedValue + "',EmployeeTypeId='" + ddlemptype.SelectedValue + "', " +
                                        " DateOfJoin='" + txtjoindate.Text + "',Address='" + tbAddress.Text + "',CountryId='" + ddlCountry.SelectedValue + "',StateId='" + ddlState.SelectedValue + "',City='" + ddlCity.SelectedValue + "',ContactNo='" + tbPhone.Text + "',Email='" + tbEmail.Text + "',EmployeeName='" + txtlastname.Text + ' ' + txtfirstname.Text + ' ' + txtintialis.Text + "',Whid='" + ddlwarehouse.SelectedValue + "',SuprviserId='" + ddlemp.SelectedValue + "',Active='" + ddlActive.SelectedValue + "',WorkPhone = '" + workphone.Text + "',WorkExt='" + workext.Text + "',WorkEmail='" + txtworkemail.Text + "',EducationqualificationID='" + dtss.Rows[0]["ID"].ToString() + "',SpecialSubjectID='" + dtss1.Rows[0]["ID"].ToString() + "',yearofexperience='" + txtyearexpr.Text + "',Jobpositionid='" + ddljobposition.SelectedValue + "',sex='" + Radiogender.SelectedValue + "' where EmployeeMasterID='" + ViewState["editid"] + "'";



                                }
                                else if (chkeduquali.Checked == false && chkspecialsub.Checked == true && chkjobposition.Checked == true)
                                {
                                    SqlDataAdapter da11 = new SqlDataAdapter("select ID from VacancyTypeMaster where Name='Other'", con);
                                    DataTable dt11 = new DataTable();
                                    da11.Fill(dt11);

                                    SqlCommand cmd1 = new SqlCommand("insert into VacancyPositionTitleMaster values('" + dt11.Rows[0]["ID"].ToString() + "','" + txtjobposition.Text + "',0)", con);
                                    if (con.State.ToString() != "Open")
                                    {
                                        con.Open();
                                    }
                                    cmd1.ExecuteNonQuery();
                                    con.Close();

                                    SqlDataAdapter da1 = new SqlDataAdapter("select max(ID) as ID from VacancyPositionTitleMaster", con);
                                    DataTable dt1 = new DataTable();
                                    da1.Fill(dt1);



                                    SqlCommand cmd101 = new SqlCommand("insert into SpecialisedSubjectTBL(SubjectName,Status,AreaofStudiesId,compid) values('" + txtspecialsub.Text + "',0,'" + ddleduquali.SelectedValue + "','" + Session["Comid"] + "')", con);
                                    if (con.State.ToString() != "Open")
                                    {
                                        con.Open();
                                    }
                                    cmd101.ExecuteNonQuery();
                                    con.Close();

                                    SqlDataAdapter dass1 = new SqlDataAdapter("select max(ID) as ID from SpecialisedSubjectTBL", con);
                                    DataTable dtss1 = new DataTable();
                                    dass1.Fill(dtss1);


                                    stremp = "Update EmployeeMaster set DeptID='" + ddldept.SelectedValue + "',DesignationMasterId='" + ddldesignation.SelectedValue + "',EmployeeTypeId='" + ddlemptype.SelectedValue + "', " +
                                        " DateOfJoin='" + txtjoindate.Text + "',Address='" + tbAddress.Text + "',CountryId='" + ddlCountry.SelectedValue + "',StateId='" + ddlState.SelectedValue + "',City='" + ddlCity.SelectedValue + "',ContactNo='" + tbPhone.Text + "',Email='" + tbEmail.Text + "',EmployeeName='" + txtlastname.Text + ' ' + txtfirstname.Text + ' ' + txtintialis.Text + "',Whid='" + ddlwarehouse.SelectedValue + "',SuprviserId='" + ddlemp.SelectedValue + "',Active='" + ddlActive.SelectedValue + "',WorkPhone = '" + workphone.Text + "',WorkExt='" + workext.Text + "',WorkEmail='" + txtworkemail.Text + "',EducationqualificationID='" + ddleduquali.SelectedValue + "',SpecialSubjectID='" + dtss1.Rows[0]["ID"].ToString() + "',yearofexperience='" + txtyearexpr.Text + "',Jobpositionid='" + dt1.Rows[0]["ID"].ToString() + "',sex='" + Radiogender.SelectedValue + "' where EmployeeMasterID='" + ViewState["editid"] + "'";


                                }


                                else if (chkeduquali.Checked == true && chkspecialsub.Checked == false && chkjobposition.Checked == true)
                                {
                                    SqlDataAdapter da11 = new SqlDataAdapter("select ID from VacancyTypeMaster where Name='Other'", con);
                                    DataTable dt11 = new DataTable();
                                    da11.Fill(dt11);

                                    SqlCommand cmd1 = new SqlCommand("insert into VacancyPositionTitleMaster values('" + dt11.Rows[0]["ID"].ToString() + "','" + txtjobposition.Text + "',0)", con);
                                    if (con.State.ToString() != "Open")
                                    {
                                        con.Open();
                                    }
                                    cmd1.ExecuteNonQuery();
                                    con.Close();

                                    SqlDataAdapter da1 = new SqlDataAdapter("select max(ID) as ID from VacancyPositionTitleMaster", con);
                                    DataTable dt1 = new DataTable();
                                    da1.Fill(dt1);

                                    SqlCommand cmd100 = new SqlCommand("insert into AreaofStudiesTbl values('" + txteduquali.Text + "',0)", con);
                                    if (con.State.ToString() != "Open")
                                    {
                                        con.Open();
                                    }
                                    cmd100.ExecuteNonQuery();
                                    con.Close();

                                    SqlDataAdapter dass = new SqlDataAdapter("select max(ID) as ID from AreaofStudiesTbl", con);
                                    DataTable dtss = new DataTable();
                                    dass.Fill(dtss);

                                    stremp = "Update EmployeeMaster set DeptID='" + ddldept.SelectedValue + "',DesignationMasterId='" + ddldesignation.SelectedValue + "',EmployeeTypeId='" + ddlemptype.SelectedValue + "', " +
                                       " DateOfJoin='" + txtjoindate.Text + "',Address='" + tbAddress.Text + "',CountryId='" + ddlCountry.SelectedValue + "',StateId='" + ddlState.SelectedValue + "',City='" + ddlCity.SelectedValue + "',ContactNo='" + tbPhone.Text + "',Email='" + tbEmail.Text + "',EmployeeName='" + txtlastname.Text + ' ' + txtfirstname.Text + ' ' + txtintialis.Text + "',Whid='" + ddlwarehouse.SelectedValue + "',SuprviserId='" + ddlemp.SelectedValue + "',Active='" + ddlActive.SelectedValue + "',WorkPhone = '" + workphone.Text + "',WorkExt='" + workext.Text + "',WorkEmail='" + txtworkemail.Text + "',EducationqualificationID='" + dtss.Rows[0]["ID"].ToString() + "',SpecialSubjectID='" + ddlspecialsub.SelectedValue + "',yearofexperience='" + txtyearexpr.Text + "',Jobpositionid='" + dt1.Rows[0]["ID"].ToString() + "',sex='" + Radiogender.SelectedValue + "' where EmployeeMasterID='" + ViewState["editid"] + "'";


                                }
                                else
                                {
                                    stremp = "Update EmployeeMaster set DeptID='" + ddldept.SelectedValue + "',DesignationMasterId='" + ddldesignation.SelectedValue + "',EmployeeTypeId='" + ddlemptype.SelectedValue + "', " +
                                        " DateOfJoin='" + txtjoindate.Text + "',Address='" + tbAddress.Text + "',CountryId='" + ddlCountry.SelectedValue + "',StateId='" + ddlState.SelectedValue + "',City='" + ddlCity.SelectedValue + "',ContactNo='" + tbPhone.Text + "',Email='" + tbEmail.Text + "',EmployeeName='" + txtlastname.Text + ' ' + txtfirstname.Text + ' ' + txtintialis.Text + "',Whid='" + ddlwarehouse.SelectedValue + "',SuprviserId='" + ddlemp.SelectedValue + "',Active='" + ddlActive.SelectedValue + "',WorkPhone = '" + workphone.Text + "',WorkExt='" + workext.Text + "',WorkEmail='" + txtworkemail.Text + "',EducationqualificationID='" + ddleduquali.SelectedValue + "',SpecialSubjectID='" + ddlspecialsub.SelectedValue + "',yearofexperience='" + txtyearexpr.Text + "',Jobpositionid='" + ddljobposition.SelectedValue + "',sex='" + Radiogender.SelectedValue + "' where EmployeeMasterID='" + ViewState["editid"] + "'";
                                }
                                SqlCommand cmdupemp = new SqlCommand(stremp, con);
                                if (con.State.ToString() != "Open")
                                {
                                    con.Open();

                                }

                                cmdupemp.ExecuteNonQuery();
                                con.Close();

                                if (chk111.Checked == true)
                                {
                                    string te = "AddDocMaster.aspx?employeemm=" + ViewState["editid"].ToString() + "&storeid=" + ddlwarehouse.SelectedValue + "";
                                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
                                }

                                SqlCommand cmdupemer = new SqlCommand("update EmployeeEmergencyContactTbl set WarehouseID='" + ddlwarehouse.SelectedValue + "', FirstEmergencyContactName='" + TextBox5.Text + "',FirstEmergencyRelationship='" + TextBox7.Text + "',FirstEmergencyPhoneNumberhome='" + TextBox9.Text + "',FirstEmergencyPhoneNumbercell='" + TextBox11.Text + "',FirstEmergencyPhoneNumberWork='" + TextBox13.Text + "',FirstEmergencyEmail='" + TextBox15.Text + "',SecondEmergencyContactName='" + TextBox6.Text + "',SecondEmergencyRelationship='" + TextBox8.Text + "',SecondEmergencyPhoneNumberhome='" + TextBox10.Text + "',SecondEmergencyPhoneNumbercell='" + TextBox12.Text + "',SecondEmergencyPhoneNumberWork='" + TextBox14.Text + "',SecondEmergencyEmail='" + TextBox16.Text + "' where EmployeeMasterID='" + ViewState["editid"] + "'", con);
                                if (con.State.ToString() != "Open")
                                {
                                    con.Open();
                                }
                                cmdupemer.ExecuteNonQuery();
                                con.Close();

                                if (RadioButtonList1.SelectedValue == "0")
                                {
                                    SqlDataAdapter daf100 = new SqlDataAdapter("select * from [employeesalarymaster] where [EmployeeId]='" + ViewState["editid"] + "'", con);
                                    DataTable dtf100 = new DataTable();
                                    daf100.Fill(dtf100);
                                    if (dtf100.Rows.Count > 0)
                                    {
                                        SqlCommand cmdsalaries = new SqlCommand("update employeesalarymaster set EmployeeId='" + ViewState["editid"] + "',Remuneration_Id='" + ddlremuneration.SelectedValue + "',Amount='" + txtamount.Text + "',PayablePer_PeriodMasterId='" + ddlpaybleper.SelectedValue + "',EffectiveStartDate='" + txtjoindate.Text + "',EffectiveEndDate='" + dateafterthreeyear + "',Whid='" + ddlwarehouse.SelectedValue + "',compid='" + Session["Comid"].ToString() + "' where EmployeeId='" + ViewState["editid"] + "'", con);
                                        if (con.State.ToString() != "Open")
                                        {
                                            con.Open();
                                        }
                                        cmdsalaries.ExecuteNonQuery();
                                        con.Close();
                                    }
                                    else
                                    {
                                        string strsalary = "insert into EmployeeSalaryMaster (EmployeeId,Remuneration_Id,Amount,PayablePer_PeriodMasterId,EffectiveStartDate,EffectiveEndDate,Whid,compid) values('" + ViewState["editid"] + "','" + ddlremuneration.SelectedValue + "','" + txtamount.Text + "','" + ddlpaybleper.SelectedValue + "','" + txtjoindate.Text + "','" + dateafterthreeyear + "','" + ddlwarehouse.SelectedValue + "','" + Session["Comid"].ToString() + "')";
                                        SqlCommand cmdsalary = new SqlCommand(strsalary, con);
                                        if (con.State.ToString() != "Open")
                                        {
                                            con.Open();
                                        }
                                        cmdsalary.ExecuteNonQuery();
                                        con.Close();
                                    }

                                    Decimal dr = EmpAvg();
                                    daf100 = new SqlDataAdapter("select * from [EmployeeAvgSalaryMaster] where [EmployeeId]='" + ViewState["editid"] + "'", con);
                                     dtf100 = new DataTable();
                                    daf100.Fill(dtf100);
                                    if (dtf100.Rows.Count > 0)
                                    {
                                        SqlCommand cmdsalaries1 = new SqlCommand("update EmployeeAvgSalaryMaster set EmployeeId='" + ViewState["editid"] + "',AvgRate='" + dr + "',PayPeriodMasterId=0,Whid='" + ddlwarehouse.SelectedValue + "',compid='" + Session["Comid"].ToString() + "' where EmployeeId='" + ViewState["editid"] + "' and PayPeriodMasterId=0", con);
                                        if (con.State.ToString() != "Open")
                                        {
                                            con.Open();
                                        }
                                        cmdsalaries1.ExecuteNonQuery();
                                        con.Close();
                                    }
                                    else
                                    {
                                        string strsalary1 = "insert into EmployeeAvgSalaryMaster values('" + ViewState["editid"] + "','" + dr + "',0,'" + ddlwarehouse.SelectedValue + "','" + Session["Comid"].ToString() + "')";
                                        SqlCommand cmdsalary1 = new SqlCommand(strsalary1, con);
                                        if (con.State.ToString() != "Open")
                                        {
                                            con.Open();
                                        }
                                        cmdsalary1.ExecuteNonQuery();
                                        con.Close();     

                                    }
                                }

                                if (RadioButtonList1.SelectedValue == "1")
                                {
                                    Decimal dr = costcalcu();

                                    string strsalary1 = "update EmployeeAvgSalaryMaster set EmployeeId='" + ViewState["editid"] + "',AvgRate='" + dr + "',PayPeriodMasterId=0,Whid='" + ddlwarehouse.SelectedValue + "',compid='" + Session["Comid"].ToString() + "' where EmployeeId='" + ViewState["editid"] + "' and PayPeriodMasterId=0";
                                    SqlCommand cmdsalary1 = new SqlCommand(strsalary1, con);
                                    if (con.State.ToString() != "Open")
                                    {
                                        con.Open();
                                    }
                                    cmdsalary1.ExecuteNonQuery();
                                    con.Close();
                                }


                                string strbatch = "";
                                if (Convert.ToString(ViewState["EmpBid"]) != "" && ddlbatch.SelectedIndex != -1)
                                {
                                    strbatch = "Update EmployeeBatchMaster set Batchmasterid='" + ddlbatch.SelectedValue + "', Whid='" + ddlwarehouse.SelectedValue + "' where Employeeid='" + ViewState["editid"] + "'";
                                    if (con.State.ToString() != "Open")
                                    {
                                        con.Open();
                                    }
                                    SqlCommand cmdbatch = new SqlCommand(strbatch, con);
                                    cmdbatch.ExecuteNonQuery();
                                    con.Close();
                                }

                                else
                                {
                                    string insertbatch = "Insert into EmployeeBatchMaster(Whid,Employeeid,Batchmasterid) values ('" + ddlwarehouse.SelectedValue + "','" + ViewState["editid"] + "','" + ddlbatch.SelectedValue + "')";
                                    SqlCommand cmdbatch = new SqlCommand(insertbatch, con);
                                    con.Open();
                                    cmdbatch.ExecuteNonQuery();
                                    con.Close();
                                }


                                SqlDataAdapter dalsde = new SqlDataAdapter("select Whid from employeemaster where employeemasterid='" + ViewState["editid"].ToString() + "'", con);
                                DataTable dtlsde = new DataTable();
                                dalsde.Fill(dtlsde);

                                if (dtlsde.Rows.Count > 0)
                                {
                                    ViewState["veve1"] = Convert.ToString(dtlsde.Rows[0]["Whid"]);

                                    SqlDataAdapter dalsde1 = new SqlDataAdapter("select top(1) [ID],[BusinessID],[AccountID],[Rate],[DateCalculated] from OverheadAbsorbtionMasterTbl where BusinessID='" + ViewState["veve1"].ToString() + "' order by ID desc", con);
                                    DataTable dtlsde1 = new DataTable();
                                    dalsde1.Fill(dtlsde1);

                                    if (dtlsde1.Rows.Count > 0)
                                    {
                                        SqlDataAdapter daf100 = new SqlDataAdapter("select [StartDate],[EndDate] from [ReportPeriod] where [Report_Period_Id]='" + Convert.ToString(dtlsde1.Rows[0]["AccountID"]) + "'", con);
                                        DataTable dtf100 = new DataTable();
                                        daf100.Fill(dtf100);

                                        if (dtf100.Rows.Count > 0)
                                        {
                                            ViewState["stdate"] = Convert.ToDateTime(dtf100.Rows[0]["StartDate"].ToString()).ToShortDateString();
                                            ViewState["endate"] = Convert.ToDateTime(dtf100.Rows[0]["EndDate"].ToString()).ToShortDateString();
                                        }

                                        SqlDataAdapter da1 = new SqlDataAdapter("select EmployeeName,EmployeeMasterID,EmployeeBatchMaster.Batchmasterid from EmployeeMaster inner join party_master on party_master.partyid=employeemaster.partyid inner join employeebatchmaster on  EmployeeMaster.EmployeeMasterID=EmployeeBatchMaster.Employeeid  where party_master.id='" + Session["Comid"].ToString() + "' and employeemaster.whid='" + Convert.ToString(dtlsde1.Rows[0]["BusinessID"]) + "' and employeemaster.Active='1'  and employeemaster.dateofjoin between '" + ViewState["stdate"].ToString() + "' and '" + ViewState["endate"].ToString() + "' ", con);
                                        DataTable dt1 = new DataTable();
                                        da1.Fill(dt1);

                                        GridView6.DataSource = dt1;
                                        GridView6.DataBind();

                                        decimal d9 = 0;

                                        foreach (GridViewRow grd in GridView6.Rows)
                                        {
                                            Label Label10var = (Label)grd.FindControl("Label10var");

                                            //for (int rowindex = 0; rowindex < dt1.Rows.Count; rowindex++)
                                            //{
                                            string str12 = "select EmployeeBatchMaster.* from EmployeeBatchMaster inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID=EmployeeBatchMaster.Employeeid where EmployeeMaster.EmployeeMasterID='" + Label10var.Text + "' and EmployeeMaster.Whid='" + Convert.ToString(dtlsde1.Rows[0]["BusinessID"]) + "'";
                                            DataTable ds12 = new DataTable();
                                            SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
                                            da12.Fill(ds12);
                                            string outdifftime1 = "00:00";

                                            if (ds12.Rows.Count > 0)
                                            {
                                                ViewState["BatchId"] = ds12.Rows[0]["Batchmasterid"].ToString();

                                                SqlDataAdapter daf = new SqlDataAdapter("select [StartDate],[EndDate] from [ReportPeriod] where [Report_Period_Id]='" + Convert.ToString(dtlsde1.Rows[0]["AccountID"]) + "'", con);
                                                DataTable dtf = new DataTable();
                                                daf.Fill(dtf);

                                                if (dtf.Rows.Count > 0)
                                                {
                                                    ViewState["std"] = Convert.ToDateTime(dtf.Rows[0]["StartDate"].ToString()).ToShortDateString();
                                                    ViewState["end"] = Convert.ToDateTime(dtf.Rows[0]["EndDate"].ToString()).ToShortDateString();
                                                }


                                                string str12311 = "select count(BatchWorkingDaysId) as BatchWorkingDays from BatchWorkingDays inner join DateMasterTbl on DateMasterTbl.DateId=BatchWorkingDays.DateMasterID where BatchWorkingDays.BatchID='" + ViewState["BatchId"] + "' and DateMasterTbl.Date between '" + ViewState["std"] + "' and '" + ViewState["end"] + "'";
                                                //and DateMasterTbl.Date between '" + ViewState["std"] + "' and '" + ViewState["end"] + "'";
                                                DataTable ds12311 = new DataTable();
                                                SqlDataAdapter da12311 = new SqlDataAdapter(str12311, con);
                                                da12311.Fill(ds12311);

                                                if (Convert.ToString(ds12311.Rows[0]["BatchWorkingDays"]) != "")
                                                {
                                                    ViewState["count"] = Convert.ToString(ds12311.Rows[0]["BatchWorkingDays"]);
                                                }

                                                string str123 = "select * from BatchWorkingDays inner join DateMasterTbl on DateMasterTbl.DateId=BatchWorkingDays.DateMasterID where BatchWorkingDays.BatchID='" + ViewState["BatchId"] + "' and DateMasterTbl.Date between '" + ViewState["std"] + "' and '" + ViewState["end"] + "'";
                                                //and DateMasterTbl.Date between '" + ViewState["std"] + "' and '" + ViewState["end"] + "'";
                                                DataTable ds123 = new DataTable();
                                                SqlDataAdapter da123 = new SqlDataAdapter(str123, con);
                                                da123.Fill(ds123);

                                                if (ds123.Rows.Count > 0)
                                                {
                                                    DataTable dsday = new DataTable();
                                                    //if (ds123.Rows[0]["day"].ToString() == "Monday")
                                                    //{



                                                    string TotalMinutes = "00:00";
                                                    string TotalMinutes1 = "00:00";
                                                    string TotalMinutes2 = "00:00";
                                                    string TotalMinutes3 = "00:00";
                                                    string TotalMinutes4 = "00:00";
                                                    string TotalMinutes5 = "00:00";
                                                    string TotalMinutes6 = "00:00";

                                                    string outdifftime12 = "00:00";
                                                    string outdifftime13 = "00:00";
                                                    string outdifftime14 = "00:00";
                                                    string outdifftime15 = "00:00";
                                                    string outdifftime16 = "00:00";
                                                    string outdifftime17 = "00:00";

                                                    string s1 = "";
                                                    string s11 = "";
                                                    string s2 = "";
                                                    string s22 = "";
                                                    string s3 = "";
                                                    string s33 = "";
                                                    string s4 = "";
                                                    string s44 = "";
                                                    string s5 = "";
                                                    string s55 = "";
                                                    string s6 = "";
                                                    string s66 = "";
                                                    string s7 = "";
                                                    string s77 = "";



                                                    string strday = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.MondayScheduleId=BatchTiming.ID  inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where BatchTiming.Whid='" + Convert.ToString(dtlsde1.Rows[0]["BusinessID"]) + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";

                                                    SqlDataAdapter daday = new SqlDataAdapter(strday, con);
                                                    daday.Fill(dsday);

                                                    if (dsday.Rows.Count > 0)
                                                    {
                                                        if (Convert.ToString(dsday.Rows[0]["totalhours"]) != "")
                                                        {
                                                            TotalMinutes = Convert.ToString(dsday.Rows[0]["totalhours"]);
                                                            outdifftime1 = Convert.ToDateTime(TotalMinutes).ToString("HH:mm");

                                                            string strValues = outdifftime1;
                                                            string[] strArray = strValues.Split(':');

                                                            s1 = strArray[0];
                                                            s11 = strArray[1];
                                                        }
                                                        else
                                                        {
                                                            s1 = "0";
                                                            s11 = "0";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        s1 = "0";
                                                        s11 = "0";
                                                    }


                                                    DataTable dsday1 = new DataTable();
                                                    string strday1 = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.TuesdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + Convert.ToString(dtlsde1.Rows[0]["BusinessID"]) + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";

                                                    SqlDataAdapter daday1 = new SqlDataAdapter(strday1, con);
                                                    daday1.Fill(dsday1);

                                                    if (dsday1.Rows.Count > 0)
                                                    {
                                                        if (Convert.ToString(dsday1.Rows[0]["totalhours"]) != "")
                                                        {
                                                            TotalMinutes1 = Convert.ToString(dsday1.Rows[0]["totalhours"]);
                                                            outdifftime12 = Convert.ToDateTime(TotalMinutes1).ToString("HH:mm");

                                                            string strValues1 = outdifftime12;
                                                            string[] strArray1 = strValues1.Split(':');

                                                            s2 = strArray1[0];
                                                            s22 = strArray1[1];
                                                        }
                                                        else
                                                        {
                                                            s2 = "0";
                                                            s22 = "0";
                                                        }

                                                    }
                                                    else
                                                    {
                                                        s2 = "0";
                                                        s22 = "0";
                                                    }


                                                    DataTable dsday2 = new DataTable();
                                                    string strday2 = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.WednesdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + Convert.ToString(dtlsde1.Rows[0]["BusinessID"]) + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";

                                                    SqlDataAdapter daday2 = new SqlDataAdapter(strday2, con);
                                                    daday2.Fill(dsday2);


                                                    if (dsday2.Rows.Count > 0)
                                                    {
                                                        if (Convert.ToString(dsday2.Rows[0]["totalhours"]) != "")
                                                        {
                                                            TotalMinutes2 = Convert.ToString(dsday2.Rows[0]["totalhours"]);
                                                            outdifftime13 = Convert.ToDateTime(TotalMinutes2).ToString("HH:mm");

                                                            string strValues2 = outdifftime13;
                                                            string[] strArray2 = strValues2.Split(':');

                                                            s3 = strArray2[0];
                                                            s33 = strArray2[1];
                                                        }
                                                        else
                                                        {
                                                            s3 = "0";
                                                            s33 = "0";
                                                        }

                                                    }
                                                    else
                                                    {
                                                        s3 = "0";
                                                        s33 = "0";
                                                    }



                                                    DataTable dsday3 = new DataTable();
                                                    string strday3 = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.ThursdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + Convert.ToString(dtlsde1.Rows[0]["BusinessID"]) + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";

                                                    SqlDataAdapter daday3 = new SqlDataAdapter(strday3, con);
                                                    daday3.Fill(dsday3);


                                                    if (dsday3.Rows.Count > 0)
                                                    {
                                                        if (Convert.ToString(dsday3.Rows[0]["totalhours"]) != "")
                                                        {
                                                            TotalMinutes3 = Convert.ToString(dsday3.Rows[0]["totalhours"]);
                                                            outdifftime14 = Convert.ToDateTime(TotalMinutes3).ToString("HH:mm");

                                                            string strValues3 = outdifftime14;
                                                            string[] strArray3 = strValues3.Split(':');

                                                            s4 = strArray3[0];
                                                            s44 = strArray3[1];
                                                        }
                                                        else
                                                        {
                                                            s4 = "0";
                                                            s44 = "0";
                                                        }

                                                    }
                                                    else
                                                    {
                                                        s4 = "0";
                                                        s44 = "0";
                                                    }



                                                    DataTable dsday4 = new DataTable();
                                                    string strday4 = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.FridayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + Convert.ToString(dtlsde1.Rows[0]["BusinessID"]) + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";

                                                    SqlDataAdapter daday4 = new SqlDataAdapter(strday4, con);
                                                    daday4.Fill(dsday4);

                                                    if (dsday4.Rows.Count > 0)
                                                    {
                                                        if (Convert.ToString(dsday4.Rows[0]["totalhours"]) != "")
                                                        {
                                                            TotalMinutes4 = Convert.ToString(dsday4.Rows[0]["totalhours"]);
                                                            outdifftime15 = Convert.ToDateTime(TotalMinutes4).ToString("HH:mm");


                                                            string strValues4 = outdifftime15;
                                                            string[] strArray4 = strValues4.Split(':');

                                                            s5 = strArray4[0];
                                                            s55 = strArray4[1];
                                                        }
                                                        else
                                                        {
                                                            s5 = "0";
                                                            s55 = "0";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        s5 = "0";
                                                        s55 = "0";
                                                    }



                                                    DataTable dsday5 = new DataTable();
                                                    string strday5 = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.SaturdayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + Convert.ToString(dtlsde1.Rows[0]["BusinessID"]) + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";

                                                    SqlDataAdapter daday5 = new SqlDataAdapter(strday5, con);
                                                    daday5.Fill(dsday5);

                                                    if (dsday5.Rows.Count > 0)
                                                    {
                                                        if (Convert.ToString(dsday5.Rows[0]["totalhours"]) != "")
                                                        {
                                                            TotalMinutes5 = Convert.ToString(dsday5.Rows[0]["totalhours"]);
                                                            outdifftime16 = Convert.ToDateTime(TotalMinutes5).ToString("HH:mm");

                                                            string strValues5 = outdifftime16;
                                                            string[] strArray5 = strValues5.Split(':');

                                                            s6 = strArray5[0];
                                                            s66 = strArray5[1];
                                                        }
                                                        else
                                                        {
                                                            s6 = "0";
                                                            s66 = "0";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        s6 = "0";
                                                        s66 = "0";
                                                    }



                                                    DataTable dsday6 = new DataTable();
                                                    string strday6 = "select BatchTiming.totalhours,TimeSchedulMaster.SchedulName from BatchWorkingDay inner join BatchTiming on BatchWorkingDay.SundayScheduleId=BatchTiming.ID inner join TimeSchedulMaster on TimeSchedulMaster.id=BatchTiming.TimeScheduleMasterId where  BatchTiming.Whid='" + Convert.ToString(dtlsde1.Rows[0]["BusinessID"]) + "' and BatchTiming.BatchMasterId='" + ViewState["BatchId"] + "' ";

                                                    SqlDataAdapter daday6 = new SqlDataAdapter(strday6, con);
                                                    daday6.Fill(dsday6);


                                                    if (dsday6.Rows.Count > 0)
                                                    {
                                                        if (Convert.ToString(dsday6.Rows[0]["totalhours"]) != "")
                                                        {
                                                            TotalMinutes6 = Convert.ToString(dsday6.Rows[0]["totalhours"]);
                                                            outdifftime17 = Convert.ToDateTime(TotalMinutes6).ToString("HH:mm");

                                                            string strValues6 = outdifftime17;
                                                            string[] strArray6 = strValues6.Split(':');

                                                            s7 = strArray6[0];
                                                            s77 = strArray6[1];
                                                        }
                                                        else
                                                        {
                                                            s7 = "0";
                                                            s77 = "0";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        s7 = "0";
                                                        s77 = "0";
                                                    }


                                                    int hourss = Convert.ToInt32(s1) + Convert.ToInt32(s2) + Convert.ToInt32(s3) + Convert.ToInt32(s4) + Convert.ToInt32(s5) + Convert.ToInt32(s6) + Convert.ToInt32(s7);

                                                    int min1 = hourss * 60;

                                                    int minss = Convert.ToInt32(s11) + Convert.ToInt32(s22) + Convert.ToInt32(s33) + Convert.ToInt32(s44) + Convert.ToInt32(s55) + Convert.ToInt32(s66) + Convert.ToInt32(s77);

                                                    int finalmin = min1 + minss;

                                                    int finhr1 = (finalmin / 60);
                                                    int finhr2 = (finalmin % 60);

                                                    string finaltime = "";

                                                    if (finhr2 == 0)
                                                    {
                                                        finaltime = finhr1.ToString();
                                                    }
                                                    else
                                                    {
                                                        finaltime = finhr1.ToString() + "." + finhr2.ToString();
                                                    }

                                                    decimal myfinal = 0;

                                                    if (dsday6.Rows.Count == 0)
                                                    {
                                                        myfinal = Convert.ToDecimal(finaltime) / 6;
                                                    }
                                                    else if (dsday6.Rows.Count == 0 && dsday5.Rows.Count == 0)
                                                    {
                                                        myfinal = Convert.ToDecimal(finaltime) / 5;
                                                    }
                                                    else if (dsday6.Rows.Count == 0 && dsday5.Rows.Count == 0 && dsday4.Rows.Count == 0)
                                                    {
                                                        myfinal = Convert.ToDecimal(finaltime) / 4;
                                                    }
                                                    else if (dsday6.Rows.Count == 0 && dsday5.Rows.Count == 0 && dsday4.Rows.Count == 0 && dsday3.Rows.Count == 0)
                                                    {
                                                        myfinal = Convert.ToDecimal(finaltime) / 3;
                                                    }
                                                    else if (dsday6.Rows.Count == 0 && dsday5.Rows.Count == 0 && dsday4.Rows.Count == 0 && dsday3.Rows.Count == 0 && dsday2.Rows.Count == 0)
                                                    {
                                                        myfinal = Convert.ToDecimal(finaltime) / 2;
                                                    }
                                                    else if (dsday6.Rows.Count == 0 && dsday5.Rows.Count == 0 && dsday4.Rows.Count == 0 && dsday3.Rows.Count == 0 && dsday2.Rows.Count == 0 && dsday1.Rows.Count == 0)
                                                    {
                                                        myfinal = Convert.ToDecimal(finaltime) / 1;
                                                    }
                                                    else
                                                    {
                                                        myfinal = Convert.ToDecimal(finaltime) / 7;
                                                    }

                                                    myfinal = Math.Round(myfinal, 2);


                                                    //  string temp1 = Convert.ToDateTime(finaltime).ToString("HH:mm");

                                                    Label Labelsdfsdd159 = (Label)grd.FindControl("Labelsdfsdd159");
                                                    Labelsdfsdd159.Text = myfinal.ToString();

                                                    Label Labxxx158 = (Label)grd.FindControl("Labxxx158");
                                                    Labxxx158.Text = ViewState["count"].ToString();

                                                    //dt1.Rows[rowindex]["dailyhr"] = Convert.ToInt32(finaltime);
                                                    //dt1.Rows[rowindex]["totald"] = ViewState["count"].ToString();



                                                    double dec = Convert.ToDouble(myfinal) * Convert.ToDouble(Labxxx158.Text);

                                                    Label Labtttt59 = (Label)grd.FindControl("Labtttt59");
                                                    Labtttt59.Text = dec.ToString("###,###.##");


                                                    string st9 = Labtttt59.Text;
                                                    if (st9 == "")
                                                    {
                                                        st9 = "0";
                                                    }
                                                    d9 += Convert.ToDecimal(st9);
                                                }
                                            }
                                        }
                                        Label15.Text = d9.ToString("###,###.##");

                                        lbl2rate.Text = Label15.Text;


                                        SqlDataAdapter dalsde2 = new SqlDataAdapter("select * from OverheadAbsorbtionDetailTbl where OverheadAbsorbtionMasterID='" + Convert.ToString(dtlsde1.Rows[0]["ID"]) + "'", con);
                                        DataTable dtlsde2 = new DataTable();
                                        dalsde2.Fill(dtlsde2);

                                        GridView5.DataSource = dtlsde2;
                                        GridView5.DataBind();

                                        decimal d33 = 0;

                                        foreach (GridViewRow gr in GridView5.Rows)
                                        {
                                            Label Label15911 = (Label)gr.FindControl("Label15911");

                                            string d3 = Label15911.Text;
                                            d33 += Convert.ToDecimal(d3);
                                        }

                                        lbl1rate.Text = d33.ToString("###,###.##");

                                        double div = Convert.ToDouble(lbl1rate.Text) / Convert.ToDouble(lbl2rate.Text);
                                        lbl3rate.Text = div.ToString("###,###.##");


                                        string stryy1 = "insert into EmplAvgCostperhourmaster values('" + Convert.ToString(dtlsde1.Rows[0]["BusinessID"]) + "','" + txtjoindate.Text + ' ' + System.DateTime.Now.TimeOfDay.ToString().Substring(0, 8) + "','" + lbl3rate.Text + "')";

                                        SqlCommand cmdyy1 = new SqlCommand(stryy1, con);
                                        if (con.State.ToString() != "Open")
                                        {
                                            con.Open();
                                        }
                                        cmdyy1.ExecuteNonQuery();
                                        con.Close();

                                        SqlDataAdapter daoy1 = new SqlDataAdapter("select max(ID) as ID from EmplAvgCostperhourmaster", con);
                                        DataTable dtoy1 = new DataTable();
                                        daoy1.Fill(dtoy1);

                                        if (dtoy1.Rows.Count > 0)
                                        {
                                            ViewState["tbl"] = Convert.ToString(dtoy1.Rows[0]["ID"]);

                                            foreach (GridViewRow gt in GridView6.Rows)
                                            {
                                                Label Label10var = (Label)gt.FindControl("Label10var");
                                                Label Labesdfsdl158 = (Label)gt.FindControl("Labesdfsdl158");


                                                SqlDataAdapter dak = new SqlDataAdapter("select AvgRate from EmployeeAvgSalaryMaster where EmployeeId='" + Label10var.Text + "'", con);
                                                DataTable dtk = new DataTable();
                                                dak.Fill(dtk);

                                                if (dtk.Rows.Count > 0)
                                                {
                                                    if (Convert.ToString(dtk.Rows[0]["AvgRate"]) != "")
                                                    {
                                                        ViewState["ret"] = dtk.Rows[0]["AvgRate"].ToString();
                                                    }
                                                    else
                                                    {
                                                        ViewState["ret"] = "0";
                                                    }
                                                }
                                                else
                                                {
                                                    ViewState["ret"] = "0";
                                                }

                                                double db = Convert.ToDouble(lbl3rate.Text) + Convert.ToDouble(ViewState["ret"].ToString());
                                                string money = db.ToString();

                                                SqlCommand cmmm = new SqlCommand("insert into EmplAvgcostperhourdetail values ('" + ViewState["tbl"].ToString() + "','" + Label10var.Text + "','" + money + "','" + ViewState["ret"].ToString() + "')", con);
                                                if (con.State.ToString() != "Open")
                                                {
                                                    con.Open();
                                                }
                                                cmmm.ExecuteNonQuery();
                                                con.Close();

                                            }
                                        }

                                        string strins = "insert into OverheadAbsorbtionMasterTbl values('" + Convert.ToString(dtlsde1.Rows[0]["BusinessID"]) + "','" + Convert.ToString(dtlsde1.Rows[0]["AccountID"]) + "','" + lbl3rate.Text + "','" + txtjoindate.Text + ' ' + System.DateTime.Now.TimeOfDay.ToString().Substring(0, 8) + "')";

                                        SqlCommand cmdcmd = new SqlCommand(strins, con);
                                        if (con.State.ToString() != "Open")
                                        {
                                            con.Open();
                                        }
                                        cmdcmd.ExecuteNonQuery();
                                        con.Close();

                                        SqlDataAdapter dao = new SqlDataAdapter("select max(ID) as ID from OverheadAbsorbtionMasterTbl", con);
                                        DataTable dto = new DataTable();
                                        dao.Fill(dto);

                                        if (dto.Rows.Count > 0)
                                        {
                                            ViewState["store"] = Convert.ToString(dto.Rows[0]["ID"]);

                                            foreach (GridViewRow gdr in GridView5.Rows)
                                            {
                                                Label Label15811 = (Label)gdr.FindControl("Label15811");
                                                Label Label15911 = (Label)gdr.FindControl("Label15911");

                                                SqlCommand cmd1 = new SqlCommand("insert into OverheadAbsorbtionDetailTbl values('" + ViewState["store"].ToString() + "','" + Label15811.Text + "','" + Label15911.Text + "')", con);
                                                if (con.State.ToString() != "Open")
                                                {
                                                    con.Open();
                                                }
                                                cmd1.ExecuteNonQuery();
                                                con.Close();
                                            }
                                        }

                                    }
                                }






                                string qryStr = "Update AccountMaster set AccountName='" + txtfirstname.Text + "',Status='" + ddlActive.SelectedValue + "'where Whid='" + ddlwarehouse.SelectedValue + "' and AccountId='" + ViewState["accoutid"] + "' ";
                                SqlCommand cm = new SqlCommand(qryStr, con);
                                con.Open();
                                cm.ExecuteNonQuery();
                                con.Close();




                                //}
                                string up = "Update Party_master set Compname='" + txtlastname.Text + ' ' + txtfirstname.Text + ' ' + txtintialis.Text + "',Address='" + tbAddress.Text + "', " +
                                    " City='" + ddlCity.SelectedValue + "',State='" + ddlState.SelectedValue + "',Country='" + ddlCountry.SelectedValue + "', " +
                                    " Email='" + tbEmail.Text + "',Phoneno='" + tbPhone.Text + "',PartyTypeId='" + ddlPartyType.SelectedValue + "', " +
                                    " AssignedAccountManagerId='" + ddlAssAccManagerId.SelectedValue + "',AssignedRecevingDepartmentInchargeId='" + ddlAssRecieveDeptId.SelectedValue + "' ,AssignedPurchaseDepartmentInchargeId='" + ddlAssPurDeptId.SelectedValue + "', " +
                                    " AssignedSalesDepartmentIncharge='" + ddlAssSalDeptId.SelectedValue + "' ,AssignedShippingDepartmentInchargeId='" + ddlAssShipDeptId.SelectedValue + "',StatusMasterId='" + ddlActive.SelectedValue + "' ,Whid='" + ddlwarehouse.SelectedValue + "',PartyTypeCategoryNo='" + lblpno.Text + "' where PartyID='" + ViewState["partyid"] + "' ";
                                SqlCommand cmd3 = new SqlCommand(up, con);

                                cmd3.Connection = con;
                                if (con.State.ToString() != "Open")
                                {
                                    con.Open();

                                }
                                //con.Open();
                                cmd3.ExecuteNonQuery();
                                con.Close();


                                string phofile = "";
                                if (Convert.ToString(Session["phofile"]) != "")
                                {
                                    logoname1 = Session["phofile"].ToString();
                                }
                                //if (FileUpload1.HasFile)
                                //{
                                //    FileUpload1.PostedFile.SaveAs(Server.MapPath("~\\ShoppingCart\\images\\") + FileUpload1.FileName);
                                //    logoname1 = FileUpload1.FileName.ToString();

                                //    imgLogo.ImageUrl = "~/ShoppingCart/images/" + FileUpload1.FileName.ToString();

                                //    //   Session["phofile"] = FileUpload1.FileName.ToString();
                                //    //   phofile = FileUpload1.FileName.ToString();
                                //}
                                else
                                {
                                    SqlCommand cmdphotofile = new SqlCommand("select Photo from User_master where PartyID='" + ViewState["partyid"] + "'", con);
                                    SqlDataAdapter dtpphofile = new SqlDataAdapter(cmdphotofile);
                                    DataTable dtphofile = new DataTable();
                                    dtpphofile.Fill(dtphofile);
                                    if (dtphofile.Rows.Count > 0)
                                    {
                                        logoname1 = dtphofile.Rows[0]["Photo"].ToString();
                                    }
                                }
                                //SqlConnection conn6 = new SqlConnection(strconn);
                                //string ins6 = "insert into User_master(Name,Address ,City,State,Country,Phoneno,EmailID ,Username,Department,Accesslevel,PartyID,DesigantionMasterId,Photo,Active,Extention,zipcode)" +
                                //                              "values ('" + tbName.Text + "','" + tbAddress.Text + "','" + ddlCity.SelectedValue + "','" + ddlState.SelectedValue + "','" + ddlCountry.SelectedValue + "','" + tbPhone.Text + "','" + tbEmail.Text + "','" + tbUserName.Text + "','" + ddldept.SelectedValue + "','1','" + Convert.ToInt32(ds5.Tables[0].Rows[0]["PartyId"]) + "','" + ddldesignation.SelectedValue + "','' ,'" + ddlActive.SelectedValue + "','" + tbExtension.Text + "','" + tbZipCode.Text + "')";
                                string up6 = "Update User_master set Name='" + txtlastname.Text + ' ' + txtfirstname.Text + ' ' + txtintialis.Text + "',Address='" + tbAddress.Text + "',City='" + ddlCity.SelectedValue + "', " +
                                    " State='" + ddlState.SelectedValue + "',Country='" + ddlCountry.SelectedValue + "',Phoneno='" + tbPhone.Text + "',Active='" + ddlActive.SelectedValue + "', " +
                                    " EmailID='" + tbEmail.Text + "',Username='" + tbUserName.Text + "',Department='" + ddldept.SelectedValue + "',DesigantionMasterId='" + ddldesignation.SelectedValue + "',Extention='" + tbExtension.Text + "',zipcode='" + tbZipCode.Text + "',Photo='" + logoname1 + "' where PartyID='" + ViewState["partyid"] + "'";
                                SqlCommand cmd6 = new SqlCommand(up6, con);
                                cmd6.Connection = con;
                                if (con.State.ToString() != "Open")
                                {
                                    con.Open();

                                }
                                // con.Open();
                                cmd6.ExecuteNonQuery();
                                con.Close();

                                //SqlConnection conn10 = new SqlConnection(strconn);
                                string sel11 = "SELECT  UserID FROM User_master where PartyID='" + ViewState["partyid"] + "'";
                                SqlCommand cmd10 = new SqlCommand(sel11, con);
                                //cmd10.Connection = con;
                                SqlDataAdapter da10 = new SqlDataAdapter(cmd10);
                                //da10.SelectCommand = cmd10;
                                DataSet ds10 = new DataSet();
                                da10.Fill(ds10);
                                //UPDATE//
                                if (addnewaddrpnl.Visible == true)
                                {
                                    string strpartyaddupdate = "update PartyAddressTbl set AddressActiveStatus='0' where UserId='" + ds10.Tables[0].Rows[0]["UserID"] + "'";
                                    SqlCommand cmdaddupdate = new SqlCommand(strpartyaddupdate, con);
                                    if (con.State.ToString() != "Open")
                                    {
                                        con.Open();
                                    }
                                    cmdaddupdate.ExecuteNonQuery();
                                    con.Close();
                                    //insert//
                                    string strpartyadd = "Insert into PartyAddressTbl(PartyMasterId,Address,Country,State,City,email,Phone,zipcode,UserId,datetime,AddressActiveStatus) " +
                                                    " values ('" + ViewState["partyid"] + "','" + tbAddress.Text + "','" + ddlCountry.SelectedValue + "','" + ddlState.SelectedValue + "','" + ddlCity.SelectedValue + "', " +
                                                    " '" + tbEmail.Text + "','" + tbPhone.Text + "','" + tbZipCode.Text + "','" + Session["userid"] + "','" + System.DateTime.Now.ToString() + "','1')";
                                    SqlCommand cmdpartyadd = new SqlCommand(strpartyadd, con);
                                    if (con.State.ToString() != "Open")
                                    {
                                        con.Open();
                                    }
                                    cmdpartyadd.ExecuteNonQuery();
                                    con.Close();
                                }

                                //SqlConnection conn9 = new SqlConnection(strconn);
                                //string ins7 = "insert into Login_master(UserID,username,password,department,accesslevel,deptid,accessid) values ('" + Convert.ToInt32(ds10.Tables[0].Rows[0]["UserID"]) + "','" + tbUserName.Text + "','" + tbPassword.Text + "','" + ddldept.SelectedValue + "','1','" + ddldesignation.SelectedValue + "','1')";
                                string up7 = "Update Login_master set username='" + tbUserName.Text + "',department='" + ddldept.SelectedValue + "',deptid='" + ddldesignation.SelectedValue + "',password='" + PageMgmt.Encrypted(tbPassword.Text) + "' where UserID='" + ds10.Tables[0].Rows[0]["UserID"].ToString() + "'";
                                SqlCommand cmd9 = new SqlCommand(up7, con);
                                cmd9.Connection = con;
                                con.Open();
                                cmd9.ExecuteNonQuery();
                                con.Close();

                                string inststatuscontrol1 = "update StatusControl set StatusMasterId='" + ddlstatusname.SelectedValue + "',Datetime='" + DateTime.Now.ToShortDateString() + "' where UserMasterId='" + Convert.ToInt32(ds10.Tables[0].Rows[0]["UserID"]) + "'";
                                SqlCommand cmdstaus1 = new SqlCommand(inststatuscontrol1, con);
                                cmdstaus1.Connection = con;
                                if (con.State.ToString() != "Open")
                                {
                                    con.Open();
                                }
                                cmdstaus1.ExecuteNonQuery();
                                con.Close();


                                string uprole = "Update User_Role set Role_id='" + ddlemprole.SelectedValue + "' where User_id='" + ds10.Tables[0].Rows[0]["UserID"].ToString() + "'";
                                SqlCommand cmduprl = new SqlCommand(uprole, con);
                                cmduprl.Connection = con;
                                con.Open();
                                cmduprl.ExecuteNonQuery();
                                con.Close();

                                if (chkaccessright.Checked == true)
                                {
                                    for (int i = 0; i < gridAccess.Rows.Count; i++)
                                    {
                                        Label Whid = (Label)gridAccess.Rows[i].FindControl("lblWh");
                                        CheckBox chk = (CheckBox)(gridAccess.Rows[i].FindControl("ChkAess"));
                                        Label lblwaid = (Label)gridAccess.Rows[i].FindControl("lblwaid");
                                        string str = "";

                                        DataTable dtwarerights = select("select * from EmployeeWarehouseRights where EmployeeId='" + ViewState["editid"] + "'");

                                        if (dtwarerights.Rows.Count > 0)
                                        {
                                            str = "Update  EmployeeWarehouseRights Set AccessAllowed='" + chk.Checked + "' where Id='" + lblwaid.Text + "'";
                                            SqlCommand cmd1 = new SqlCommand(str, con);
                                            con.Open();
                                            cmd1.ExecuteNonQuery();
                                            con.Close();
                                        }
                                        else
                                        {
                                            if (chk.Checked == true)
                                            {
                                                str = "Insert  into EmployeeWarehouseRights (EmployeeId,Whid,AccessAllowed)values('" + ViewState["editid"] + "','" + Whid.Text + "','" + chk.Checked + "')";

                                                SqlCommand cmd1 = new SqlCommand(str, con);
                                                con.Open();
                                                cmd1.ExecuteNonQuery();
                                                con.Close();
                                            }
                                        }


                                    }
                                }

                                if (chkempbarcode.Checked == true)
                                {
                                    SqlDataAdapter dabar1 = new SqlDataAdapter("select * from EmployeeBarcodeMaster where Employee_Id ='" + ViewState["editid"] + "'", con);
                                    DataTable dtbar1 = new DataTable();
                                    dabar1.Fill(dtbar1);

                                    if (dtbar1.Rows.Count > 0)
                                    {
                                        string str = "Update EmployeeBarcodeMaster set Whid='" + ddlwarehouse.SelectedValue + "',Barcode='" + txtbarcode.Text + "',[Active]='" + ddlActive.SelectedValue + "',Employeecode='" + txtsecuritycode.Text + "',Biometricno='" + txtbiometricid.Text + "',blutoothid='" + txtbluetoothid.Text + "' where Employee_Id ='" + ViewState["editid"] + "'";
                                        SqlCommand cmd1 = new SqlCommand(str, con);
                                        if (con.State.ToString() != "Open")
                                        {
                                            con.Open();
                                        }
                                        cmd1.ExecuteNonQuery();
                                        con.Close();
                                    }
                                    else
                                    {
                                        string str = "insert into EmployeeBarcodeMaster (Whid,Barcode,Active,Employeecode,Biometricno,blutoothid,Employee_Id) values('" + ddlwarehouse.SelectedValue + "','" + txtbarcode.Text + "','" + ddlActive.SelectedValue + "','" + txtsecuritycode.Text + "','" + txtbiometricid.Text + "','" + txtbluetoothid.Text + "','" + ViewState["editid"] + "')";
                                        SqlCommand cmd1 = new SqlCommand(str, con);
                                        if (con.State.ToString() != "Open")
                                        {
                                            con.Open();
                                        }
                                        cmd1.ExecuteNonQuery();
                                        con.Close();
                                    }

                                }

                                DataTable dtmypayroll = select("select * from EmployeePayrollMaster where Empid='" + ViewState["editid"] + "'");

                                if (dtmypayroll.Rows.Count > 0)
                                {
                                    if (chkemppayroll.Checked == true)
                                    {
                                        if (ddlPaymentMethod.SelectedItem.Text == "Demand Draft" || ddlPaymentMethod.SelectedItem.Text == "Cheque")
                                        {
                                            string str = "Update  EmployeePayrollMaster Set  EmployeePaidAsPerDesignation='" + RadioButtonList1.SelectedValue + "',PaymentMethodId='" + ddlPaymentMethod.SelectedValue + "',PaymentReceivedNameOf='" + txtPaymentReceivedNameOf.Text + "',Whid='" + ddlwarehouse.SelectedValue + "',PayPeriodMasterId='" + ddlPaymentCycle.SelectedValue + "',LastName='" + txtlastname.Text + "',FirstName='" + txtfirstname.Text + "',Intials='" + txtintialis.Text + "',DateOfBirth='" + txtdateofbirth.Text + "',SocialSecurityNo='" + txtsecurityno.Text + "' where EmpId='" + ViewState["editid"] + "'";
                                            SqlCommand cmd12 = new SqlCommand(str, con);
                                            if (con.State.ToString() != "Open")
                                            {
                                                con.Open();
                                            }
                                            cmd12.ExecuteNonQuery();
                                            con.Close();


                                        }
                                        else if (ddlPaymentMethod.SelectedItem.Text == "Paypal")
                                        {
                                            //string str = "Insert Into EmployeePayrollMaster(EmpId,EmployeePaidAsPerDesignation,PaymentMethodId,PaypalId,Whid,Compid)values('" + ddlemployee.SelectedValue + "','" + RadioButtonList1.SelectedValue.Checked + "','" + ddlPaymentMethod.SelectedValue + "','" + txtPaypalId.Text + "','" + ddlStore.SelectedValue + "','" + Session["comid"] + "')";
                                            string str = "Update  EmployeePayrollMaster Set  EmployeePaidAsPerDesignation='" + RadioButtonList1.SelectedValue + "',PaymentMethodId='" + ddlPaymentMethod.SelectedValue + "',PaypalId='" + ddlPaymentMethod.SelectedValue + "',Whid='" + ddlwarehouse.SelectedValue + "',PayPeriodMasterId='" + ddlPaymentCycle.SelectedValue + "',LastName='" + txtlastname.Text + "',FirstName='" + txtfirstname.Text + "',Intials='" + txtintialis.Text + "',DateOfBirth='" + txtdateofbirth.Text + "',SocialSecurityNo='" + txtsecurityno.Text + "' where EmpId='" + ViewState["editid"] + "'";
                                            SqlCommand cmd12 = new SqlCommand(str, con);
                                            if (con.State.ToString() != "Open")
                                            {
                                                con.Open();
                                            }
                                            cmd12.ExecuteNonQuery();
                                            con.Close();


                                        }
                                        else if (ddlPaymentMethod.SelectedItem.Text == "By Email")
                                        {
                                            string str = "Update  EmployeePayrollMaster Set  EmployeePaidAsPerDesignation='" + RadioButtonList1.SelectedValue + "',PaymentMethodId='" + ddlPaymentMethod.SelectedValue + "',PaymentEmailId='" + txtPaymentEmailId.Text + "',Whid='" + ddlwarehouse.SelectedValue + "',PayPeriodMasterId='" + ddlPaymentCycle.SelectedValue + "',LastName='" + txtlastname.Text + "',FirstName='" + txtfirstname.Text + "',Intials='" + txtintialis.Text + "',DateOfBirth='" + txtdateofbirth.Text + "',SocialSecurityNo='" + txtsecurityno.Text + "' where EmpId='" + ViewState["editid"] + "'";
                                            // string str = "Insert Into EmployeePayrollMaster(EmpId,EmployeePaidAsPerDesignation,PaymentMethodId,PaymentEmailId,Whid,Compid)values('" + ddlemployee.SelectedValue + "','" + RadioButtonList1.SelectedValue.Checked + "','" + ddlPaymentMethod.SelectedValue + "','" + txtPaymentEmailId.Text + "','" + ddlStore.SelectedValue + "','" + Session["comid"] + "')";
                                            SqlCommand cmd12 = new SqlCommand(str, con);
                                            if (con.State.ToString() != "Open")
                                            {
                                                con.Open();
                                            }
                                            cmd12.ExecuteNonQuery();
                                            con.Close();

                                        }

                                        else if (ddlPaymentMethod.SelectedItem.Text == "Direct Deposit")
                                        {
                                            string str = "Update  EmployeePayrollMaster Set  EmployeePaidAsPerDesignation='" + RadioButtonList1.SelectedValue + "',PaymentMethodId='" + ddlPaymentMethod.SelectedValue + "'," +
                                               "DirectDepositBankName='" + txtDirectDepositBankName.Text + "'," +
                                               "  DirectDepositBankBranchName='" + txtDirectDepositBankBranchName.Text + "', " +
                                               " DirectDepositBankBranchAddress = '" + txtDirectDepositBranchAddress.Text + "', " +
                                               " DirectDepositBankBranchcity = '" + ddlDirectDepositBankBranchcity.SelectedValue + "', " +
                                               " DirectDepositBankBranchstate = '" + ddlDirectDepositBankBranchstate.SelectedValue + "', " +
                                               " DirectDepositBankBranchcountry = '" + ddlDirectDepositBankBranchcountry.SelectedValue + "', " +
                                               " DirectDepositBankBranchzipcode = '" + ddlDirectDepositBankBranchzipcode.Text + "', " +
                                               " DirectDepositBankIFCNumber = '" + txtDirectDepositifscnumber.Text + "', " +
                                               " DirectDepositBankSwiftNumber = '" + txtDirectDepositswiftnumber.Text + "', " +
                                               " DirectDepositBankEmployeeEmail = '" + txtdirectdipositemployeeemail.Text + "', " +
                                               " DirectDepositTransitNumber='" + txtDirectDepositTransitNumber.Text + "',DirectDepositAccountHolderName='" + txtDirectDepositAccountHolderName.Text + "', " +
                                               " DirectDepositBankAccountType='" + ddlDirectDepositBankAccountType.SelectedItem.Text + "',DirectDepositBankAccountNumber='" + txtDirectDepositBankAccountNumber.Text + "',Whid='" + ddlwarehouse.SelectedValue + "',PayPeriodMasterId='" + ddlPaymentCycle.SelectedValue + "',LastName='" + txtlastname.Text + "',FirstName='" + txtfirstname.Text + "',Intials='" + txtintialis.Text + "',DateOfBirth='" + txtdateofbirth.Text + "',SocialSecurityNo='" + txtsecurityno.Text + "',RegisterBankAddress='" + TextBox2.Text + "' where EmpId='" + ViewState["editid"] + "' ";

                                            SqlCommand cmd12 = new SqlCommand(str, con);
                                            if (con.State.ToString() != "Open")
                                            {
                                                con.Open();
                                            }
                                            cmd12.ExecuteNonQuery();
                                            con.Close();



                                        }
                                        else if (ddlPaymentMethod.SelectedItem.Text == "Cash")
                                        {
                                            string str = "Update  EmployeePayrollMaster Set  EmployeePaidAsPerDesignation='" + RadioButtonList1.SelectedValue + "',PaymentMethodId='" + ddlPaymentMethod.SelectedValue + "',Whid='" + ddlwarehouse.SelectedValue + "',PayPeriodMasterId='" + ddlPaymentCycle.SelectedValue + "',LastName='" + txtlastname.Text + "',FirstName='" + txtfirstname.Text + "',Intials='" + txtintialis.Text + "',DateOfBirth='" + txtdateofbirth.Text + "',SocialSecurityNo='" + txtsecurityno.Text + "' where EmpId='" + ViewState["editid"] + "'";
                                            SqlCommand cmd12 = new SqlCommand(str, con);
                                            if (con.State.ToString() != "Open")
                                            {
                                                con.Open();
                                            }
                                            cmd12.ExecuteNonQuery();
                                            con.Close();

                                        }
                                    }
                                }
                                else
                                {
                                    if (chkemppayroll.Checked == true)
                                    {
                                        if (ddlPaymentMethod.SelectedItem.Text == "Demand Draft" || ddlPaymentMethod.SelectedItem.Text == "Cheque")
                                        {
                                            string str = "Insert Into EmployeePayrollMaster(EmpId,EmployeePaidAsPerDesignation,PaymentMethodId,PaymentReceivedNameOf,Whid,Compid,PayPeriodMasterId,LastName,FirstName,Intials,EmployeeNo,DateOfBirth,SocialSecurityNo)values('" + ViewState["editid"] + "','" + RadioButtonList1.SelectedValue + "','" + ddlPaymentMethod.SelectedValue + "','" + txtPaymentReceivedNameOf.Text + "','" + ddlwarehouse.SelectedValue + "','" + Session["comid"] + "','" + ddlPaymentCycle.SelectedValue + "','" + txtlastname.Text + "','" + txtfirstname.Text + "','" + txtintialis.Text + "','" + TextBox1.Text.Trim() + "','" + txtdateofbirth.Text + "','" + txtsecurityno.Text + "')";
                                            SqlCommand cmddemand = new SqlCommand(str, con);
                                            if (con.State.ToString() != "Open")
                                            {
                                                con.Open();
                                            }
                                            cmddemand.ExecuteNonQuery();
                                            con.Close();


                                        }
                                        else if (ddlPaymentMethod.SelectedItem.Text == "Paypal")
                                        {
                                            string str = "Insert Into EmployeePayrollMaster(EmpId,EmployeePaidAsPerDesignation,PaymentMethodId,PaypalId,Whid,Compid,PayPeriodMasterId,LastName,FirstName,Intials,EmployeeNo,DateOfBirth,SocialSecurityNo)values('" + ViewState["editid"] + "','" + RadioButtonList1.SelectedValue + "','" + ddlPaymentMethod.SelectedValue + "','" + txtPaypalId.Text + "','" + ddlwarehouse.SelectedValue + "','" + Session["comid"] + "','" + ddlPaymentCycle.SelectedValue + "','" + txtlastname.Text + "','" + txtfirstname.Text + "','" + txtintialis.Text + "','" + TextBox1.Text.Trim() + "','" + txtdateofbirth.Text + "','" + txtsecurityno.Text + "')";
                                            SqlCommand cmdpaypal = new SqlCommand(str, con);
                                            if (con.State.ToString() != "Open")
                                            {
                                                con.Open();
                                            }
                                            cmdpaypal.ExecuteNonQuery();
                                            con.Close();


                                        }
                                        else if (ddlPaymentMethod.SelectedItem.Text == "By Email")
                                        {
                                            string str = "Insert Into EmployeePayrollMaster(EmpId,EmployeePaidAsPerDesignation,PaymentMethodId,PaymentEmailId,Whid,Compid,PayPeriodMasterId,LastName,FirstName,Intials,EmployeeNo,DateOfBirth,SocialSecurityNo)values('" + ViewState["editid"] + "','" + RadioButtonList1.SelectedValue + "','" + ddlPaymentMethod.SelectedValue + "','" + txtPaymentEmailId.Text + "','" + ddlwarehouse.SelectedValue + "','" + Session["comid"] + "','" + ddlPaymentCycle.SelectedValue + "','" + txtlastname.Text + "','" + txtfirstname.Text + "','" + txtintialis.Text + "','" + TextBox1.Text.Trim() + "','" + txtdateofbirth.Text + "','" + txtsecurityno.Text + "')";
                                            SqlCommand cmdemail = new SqlCommand(str, con);
                                            if (con.State.ToString() != "Open")
                                            {
                                                con.Open();
                                            }
                                            cmdemail.ExecuteNonQuery();
                                            con.Close();

                                        }

                                        else if (ddlPaymentMethod.SelectedItem.Text == "Direct Deposit")
                                        {
                                            string str = "Insert Into EmployeePayrollMaster(EmpId,EmployeePaidAsPerDesignation,PaymentMethodId,DirectDepositBankName,DirectDepositBankBranchName,DirectDepositTransitNumber,DirectDepositAccountHolderName,DirectDepositBankAccountType,DirectDepositBankAccountNumber,DirectDepositBankBranchAddress,DirectDepositBankBranchcity,DirectDepositBankBranchstate,DirectDepositBankBranchcountry,DirectDepositBankBranchzipcode,DirectDepositBankIFCNumber,DirectDepositBankSwiftNumber,DirectDepositBankEmployeeEmail,Whid,Compid,PayPeriodMasterId,LastName,FirstName,Intials,EmployeeNo,DateOfBirth,SocialSecurityNo)values('" + ViewState["editid"] + "','" + RadioButtonList1.SelectedValue + "','" + ddlPaymentMethod.SelectedValue + "','" + txtDirectDepositBankName.Text + "','" + txtDirectDepositBankBranchName.Text + "','" + txtDirectDepositTransitNumber.Text + "','" + txtDirectDepositAccountHolderName.Text + "','" + ddlDirectDepositBankAccountType.SelectedValue + "','" + txtDirectDepositBankAccountNumber.Text + "','" + txtDirectDepositBranchAddress.Text + "','" + ddlDirectDepositBankBranchcity.SelectedValue + "','" + ddlDirectDepositBankBranchstate.SelectedValue + "','" + ddlDirectDepositBankBranchcountry.SelectedValue + "','" + ddlDirectDepositBankBranchzipcode.Text + "','" + txtDirectDepositifscnumber.Text + "','" + txtDirectDepositswiftnumber.Text + "','" + txtdirectdipositemployeeemail.Text + "','" + ddlwarehouse.SelectedValue + "','" + Session["comid"] + "','" + ddlPaymentCycle.SelectedValue + "','" + txtlastname.Text + "','" + txtfirstname.Text + "','" + txtintialis.Text + "','" + TextBox1.Text.Trim() + "','" + txtdateofbirth.Text + "','" + txtsecurityno.Text + "')";
                                            SqlCommand cmddeposit = new SqlCommand(str, con);
                                            if (con.State.ToString() != "Open")
                                            {
                                                con.Open();
                                            }
                                            cmddeposit.ExecuteNonQuery();
                                            con.Close();

                                        }
                                        else if (ddlPaymentMethod.SelectedItem.Text == "Cash")
                                        {
                                            string str = "Insert Into EmployeePayrollMaster(EmpId,EmployeePaidAsPerDesignation,PaymentMethodId,Whid,Compid,PayPeriodMasterId,LastName,FirstName,Intials,EmployeeNo,DateOfBirth,SocialSecurityNo)values('" + ViewState["editid"] + "','" + RadioButtonList1.SelectedValue + "','" + ddlPaymentMethod.SelectedValue + "','" + ddlwarehouse.SelectedValue + "','" + Session["comid"] + "','" + ddlPaymentCycle.SelectedValue + "','" + txtlastname.Text + "','" + txtfirstname.Text + "','" + txtintialis.Text + "','" + TextBox1.Text.Trim() + "','" + txtdateofbirth.Text + "','" + txtsecurityno.Text + "')";
                                            SqlCommand cmdcash = new SqlCommand(str, con);
                                            if (con.State.ToString() != "Open")
                                            {
                                                con.Open();
                                            }
                                            cmdcash.ExecuteNonQuery();
                                            con.Close();

                                        }
                                    }
                                }


                                //--------------------
                                string ssd = " Select License_Emp_id,Jobcenter_Emp_id from  Syncr_LicenseEmployee_With_JobcenterId  Where  (Jobcenter_Emp_id='" + ViewState["editid"].ToString()  + "' ) ";
                                SqlDataAdapter da2 = new SqlDataAdapter(ssd, con);
                                DataTable dt2 = new DataTable();
                                da2.Fill(dt2);
                                string licenseEmplID = "";
                                if (dt2.Rows.Count > 0)
                                {
                                    licenseEmplID = dt2.Rows[0]["License_Emp_id"].ToString();
                                    string licenseRole_FromIoffice = "";
                                    //Iffice
                                    string str1R = "Select RoleId as RoleId from DesignationMaster where DesignationMasterId='" + ddldesignation.SelectedValue + "' ";
                                    SqlCommand cmd1R = new SqlCommand(str1R, con);
                                    SqlDataAdapter adp1R = new SqlDataAdapter(cmd1R);
                                    DataTable datat1R = new DataTable();
                                    adp1R.Fill(datat1R);
                                    if (datat1R.Rows.Count > 0)
                                    {
                                        licenseRole_FromIoffice = datat1R.Rows[0]["RoleId"].ToString();

                                        //License 
                                        String licenseUserId = "";
                                        string str1 = "Select UserId as UserId from EmployeeMaster where id='" + licenseEmplID + "' ";
                                        SqlCommand cmd1 = new SqlCommand(str1, con1);
                                        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                                        DataTable datat1 = new DataTable();
                                        adp1.Fill(datat1);
                                        if (datat1.Rows.Count > 0)
                                        {
                                            licenseUserId = datat1.Rows[0]["UserId"].ToString();
                                            //License 
                                            string sr51 = (" update EmployeeMaster set Name='" + txtfirstname.Text + "',Active='" + ddlActive.SelectedValue + "', UserId='" + tbUserName.Text + "' , PhoneNo='" + tbPhone.Text + "',PhoneExtension='" + workext.Text + "',MobileNo='" + tbExtension.Text + "' ,Email='" + tbEmail.Text + "',Zipcode='" + tbZipCode.Text + "',RoleId='" + licenseRole_FromIoffice + "'  where id='" + licenseEmplID + "' ");
                                            SqlCommand cmd801 = new SqlCommand(sr51, con1);
                                            con1.Open();
                                            cmd801.ExecuteNonQuery();
                                            con1.Close();


                                            string ClientInsert = " update clientLoginMaster set  clientId='" + Session["ClientId"].ToString() + "',UserId='" + tbUserName.Text + "',Password='" + PageMgmt.Encrypted(tbPassword.Text) + "'  where UserId='" + licenseUserId + "' ";
                                            SqlCommand cmd1123 = new SqlCommand(ClientInsert, con1);
                                            con1.Open();
                                            cmd1123.ExecuteNonQuery();
                                            con1.Close();
                                            con1.Close();

                                            con1.Open();
                                            string insertdatabase = "update User_Role set  Role_id='" + licenseRole_FromIoffice + "' Where User_id='" + licenseEmplID + "'";//ViewState["eId"]
                                            SqlCommand cmdRole = new SqlCommand(insertdatabase, con1);
                                            cmdRole.ExecuteNonQuery();
                                            con1.Close();
                                        }
                                        else
                                        {
                                            string SubMenuInsert = " Insert Into EmployeeMaster (Name,FTPServerURL,FTPPort,FTPUserId,FTPPassword,SupervisorId,DesignationId,UserId,Password,Active,ClientId,PhoneNo,PhoneExtension,MobileNo,CountryId,StateId,City,Email,Zipcode,RoleId,EffectiveRate) values ('" + txtfirstname.Text + "','','','','" + tbPassword.Text + "','" + ddlsupervisor.SelectedValue + "','','" + tbUserName.Text + "','" + PageMgmt.Encrypted(tbPassword.Text) + "','" + ddlActive.SelectedValue + "','" + Session["comid"].ToString() + "','" + tbPhone.Text + "','" + workext.Text + "','" + tbExtension.Text + "','','','','" + tbEmail.Text + "','" + tbZipCode.Text + "','" + licenseRole_FromIoffice + "','00.00')";
                                            cmd = new SqlCommand(SubMenuInsert, con1);
                                            con1.Open();
                                            cmd.ExecuteNonQuery();
                                            con1.Close();
                                            string str2 = " select MAX(Id) as EmpID from EmployeeMaster where EmployeeMaster.ClientId='" + Session["ClientId"].ToString() + "'";

                                            SqlCommand cmd2 = new SqlCommand(str2, con1);
                                            SqlDataAdapter adp2 = new SqlDataAdapter(cmd2);
                                            DataSet ds2 = new DataSet();
                                            adp2.Fill(ds2);
                                            ViewState["eId"] = ds2.Tables[0].Rows[0]["EmpID"].ToString();
                                            int i = 0;

                                            string ClientInsert = " Insert Into clientLoginMaster (clientId,UserId,Password) values ('" + Session["ClientId"].ToString() + "','" + tbUserName.Text + "','" + PageMgmt.Encrypted(tbPassword.Text) + "') ";
                                            SqlCommand cmd1123 = new SqlCommand(ClientInsert, con1);
                                            con1.Open();
                                            cmd1123.ExecuteNonQuery();
                                            con1.Close();
                                            con1.Close();

                                            con1.Open();
                                            string insertdatabase = "insert into User_Role (User_id,Role_id,ActiveDeactive)values(" + ViewState["eId"] + ",'" + licenseRole_FromIoffice + "','1')";
                                            SqlCommand cmdRole = new SqlCommand(insertdatabase, con1);
                                            cmdRole.ExecuteNonQuery();
                                            con1.Close();

                                            con.Open();
                                            insertdatabase = " Update Syncr_LicenseEmployee_With_JobcenterId  set  License_Emp_id='" + ViewState["eId"] + "' Where Jobcenter_Emp_id='" + ViewState["editid"].ToString() + "'";//ViewState["eId"]
                                            cmdRole = new SqlCommand(insertdatabase, con);
                                            cmdRole.ExecuteNonQuery();
                                            con.Close();
                                        }
                                    }
                                }
                                else
                                {
                                    //string insertSyncr = "insert into Syncr_LicenseEmployee_With_JobcenterId(License_Emp_id,Jobcenter_Emp_id)values('" + ViewState["eId"].ToString() + "','" + ViewState["EmerEMID"].ToString() + "')";
                                    //SqlCommand nisync = new SqlCommand(insertSyncr, con);
                                    //if (con.State.ToString() != "Open")
                                    //{
                                    //    con.Open();
                                    //}
                                    //nisync.ExecuteNonQuery();
                                    //con.Close();
                                }
                                //----------------------------

                               


                                //UploadDocumets();

                                fillgriddata();
                                clear();

                                Panel6.Visible = false;

                                pnlparty.Visible = false;

                                btnshowparty.Visible = true;
                                //lbladd.Text = "";

                                MultiView1.ActiveViewIndex = 0;

                                //pnlparty.Visible = false;

                                //btnshowparty.Visible = true;

                                imgbtnupdate.Visible = false;
                                btnSubmit.Visible = true;

                                pnllabeladdress.Visible = false;
                                addnewaddrpnl.Visible = true;
                                addnewaddr.Visible = false;
                                pnllastaddress.Visible = false;
                                lblmsg.Text = "Record updated successfully";
                            }
                            else if (acce == 2)
                            {
                                lblmsg.Visible = true;
                                lblmsg.Text = "Sorry,Email Id is already used";
                            }

                        }
                        else
                        {
                            lblmsg.Visible = true;
                            lblmsg.Text = "Sorry,Email Id is already used";
                        }
                    }
                    else
                    {
                        lblmsg.Text = "";
                        lblmsg.Text = "Sorry, You don't permited greter record to priceplan";
                    }
                }
            }
            //}
        }

        //int i1 = 0;

        //for (i1 = 1000; i1 <= 9999; i1++)
        //{
        //    string strusercheck = " select EmployeeNo from EmployeePayrollMaster where Compid='" + Session["Comid"].ToString() + "' and EmployeeNo='" + i1 + "'";
        //    SqlCommand cmdusercheck = new SqlCommand(strusercheck, con);
        //    SqlDataAdapter adpusercheck = new SqlDataAdapter(cmdusercheck);
        //    DataTable dsusercheck = new DataTable();
        //    adpusercheck.Fill(dsusercheck);

        //    if (dsusercheck.Rows.Count > 0)
        //    {
        //        TextBox1.Text = (i1 + 1).ToString();
        //    }
        //    else
        //    {
        //        TextBox1.Text = i1.ToString();
        //        break;
        //    }

        //}
    }

    //protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (tbEmail.Text != "")
    //    {
    //        if (CheckBox2.Checked == true)
    //        {
    //            pnlemployeedate.Visible = false;

    //        }
    //        else
    //        {
    //            pnlemployeedate.Visible = true;
    //        }



    //    }
    //    else
    //    {
    //        lblmsg.Text = "please fill email address.";
    //    }
    //}
    public string alphanum()
    {
        Guid g = Guid.NewGuid();
        string r = g.ToString();
        string alpha = r.Substring(0, 6);
        return alpha;
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
    public Boolean sendmail(string To)
    {
        // Session["Comid"] = "d1989";
        string ADDRESSEX = "SELECT distinct CompanyMaster.CompanyLogo, CompanyMaster.CompanyName,CompanyWebsitMaster.Sitename,CompanyWebsitMaster.MasterEmailId,CompanyWebsitMaster.EmailMasterLoginPassword,CompanyWebsitMaster.OutGoingMailServer, CompanyWebsitMaster.EmailSentDisplayName,CompanyWebsitMaster.SiteUrl,CompanyWebsiteAddressMaster.Address1,CompanyWebsiteAddressMaster.Address2,CompanyWebsiteAddressMaster.Phone1, CompanyWebsiteAddressMaster.Phone2, CompanyWebsiteAddressMaster.TollFree1, CompanyWebsiteAddressMaster.Fax,CompanyWebsiteAddressMaster.Email,CompanyMaster.CompanyId,CompanyWebsitMaster.WHid FROM  CompanyMaster LEFT OUTER JOIN AddressTypeMaster RIGHT OUTER JOIN CompanyWebsiteAddressMaster ON AddressTypeMaster.AddressTypeMasterId = CompanyWebsiteAddressMaster.AddressTypeMasterId RIGHT OUTER JOIN CompanyWebsitMaster ON CompanyWebsiteAddressMaster.CompanyWebsiteMasterId = CompanyWebsitMaster.CompanyWebsiteMasterId ON CompanyMaster.CompanyId = CompanyWebsitMaster.CompanyId where CompanyMaster.Compid='" + Session["comid"] + "' and WHId='" + ddlwarehouse.SelectedValue + "'";
        SqlCommand cmd = new SqlCommand(ADDRESSEX, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        StringBuilder HeadingTable = new StringBuilder();
        HeadingTable.Append("<table width=\"100%\"> ");

        SqlDataAdapter dalogo = new SqlDataAdapter("select logourl from CompanyWebsitMaster where whid='" + ddlwarehouse.SelectedValue + "'", con);
        DataTable dtlogo = new DataTable();
        dalogo.Fill(dtlogo);

        HeadingTable.Append("<tr><td width=\"50%\" style=\"padding-left:10px\" align=\"left\" > <img src=\"http://" + Request.Url.Host.ToString() + "/Shoppingcart/images/" + dtlogo.Rows[0]["logourl"].ToString() + "\" border=\"0\" Width=\"200px\" Height=\"125px\" / > </td><td style=\"padding-left:100px\" width=\"50%\" align=\"left\"><b><span style=\"color: #996600\">" + ds.Rows[0]["CompanyName"].ToString() + "</span></b><Br>" + ds.Rows[0]["Address1"].ToString() + "<Br>" + ds.Rows[0]["Address2"].ToString() + "<Br><b>Toll Free:</b>" + ds.Rows[0]["TollFree1"].ToString() + "<Br><b>Phone :</b>" + ds.Rows[0]["Phone1"].ToString() + "<Br><b>Fax :</b>" + ds.Rows[0]["Fax"].ToString() + "<br><b>Email:</b>" + ds.Rows[0]["Email"].ToString() + "<Br><b>Website:</b>" + ds.Rows[0]["SiteUrl"].ToString() + " </td></tr>  ");


        HeadingTable.Append("</table> ");


        string welcometext = getWelcometext();

        string loginurl = Request.Url.Host.ToString() + "/Shoppingcart/Admin/ResetPasswordUser.aspx";

        string AccountInfo = " Your new account has been successfully created and the following is your temporary login and account information:<br><br><b>Temporary Login Information:</b><br><br>Company Name: " + ds.Rows[0]["CompanyName"].ToString() + "<br>Company ID: " + Session["comid"] + "<br>Temporary User ID: " + ViewState["username"] + "<br>Temporary Password: " + ViewState["password"] + " ";

        string Accountdetail = "";
        //<br><br>You can login now to your account and setup your own userid & password.


        string accountdetailofparty = "select Party_master.*,CountryMaster.CountryName,StateMasterTbl.StateName,CityMasterTbl.CityName from Party_master  left outer join CountryMaster on CountryMaster.CountryId=Party_master.Country left outer join StateMasterTbl on StateMasterTbl.StateId=Party_master.State left outer join CityMasterTbl on CityMasterTbl.CityId=Party_master.City inner join User_master on User_master.PartyID=Party_master.PartyID where Party_master.id='" + Session["comid"] + "' and Party_master.PartyID='" + ViewState["partyidforemail"] + "' ";
        SqlCommand cmdpartydetail = new SqlCommand(accountdetailofparty, con);
        SqlDataAdapter adppartydetail = new SqlDataAdapter(cmdpartydetail);
        DataTable dspartydetail = new DataTable();
        string Accountdetail12 = "";
        adppartydetail.Fill(dspartydetail);
        if (dspartydetail.Rows.Count > 0)
        {
            Accountdetail12 = "<br><b>Account Information:</b><br><br>Employee Name: " + txtlastname.Text + ' ' + txtfirstname.Text + ' ' + txtintialis.Text + "<br>Employee Number: " + TextBox1.Text + "<br>Employee Code: " + txtsecuritycode.Text + " <br>Address: " + dspartydetail.Rows[0]["Address"].ToString() + " <br>City, State/Province, Country: " + dspartydetail.Rows[0]["CityName"].ToString() + "," + dspartydetail.Rows[0]["StateName"].ToString() + "," + dspartydetail.Rows[0]["CountryName"].ToString() + "<br>ZIP/Postal Code: " + dspartydetail.Rows[0]["Zipcode"].ToString() + "<br>Phone Number: " + dspartydetail.Rows[0]["Phoneno"].ToString() + "<br>Email: " + dspartydetail.Rows[0]["Email"].ToString() + "<br><br>To edit your contact details,login and make changes from the 'My Personal Profile' page.<br><br>Please ensure that you change your user ID and password as soon as possiblr for your account security.  Please click <a href=http://" + Request.Url.Host.ToString() + "/Shoppingcart/admin/Resetpassworduser.aspx target=_blank > here</a> to change your information, if the link does not open please copy and paste the following link:<br><br><a href=http://" + Request.Url.Host.ToString() + "/Shoppingcart/admin/Resetpassworduser.aspx target=_blank > ";
            //<strong><span style=\"color: #996600\"> </span></strong> 
        }

        string body = "<br>" + HeadingTable + "<br><br> Dear <strong><span style=\"color: #996600\"> " + txtlastname.Text + ' ' + txtfirstname.Text + ' ' + txtintialis.Text + " </span></strong>,<br><br>" + welcometext.ToString() + " <br>" + AccountInfo.ToString() + "<br> " + Accountdetail.ToString() + " <br>" + Accountdetail12 + " <br><br> <span style=\"font-size: 10pt; color: #000000; font-family: Arial\"><br>Thank you,</span><br><strong><span style=\"color: #996600\"><br>Admin Team<br></span></strong>" + ds.Rows[0]["CompanyName"].ToString() + "<br>" + ds.Rows[0]["Address1"].ToString() + "<br>" + ds.Rows[0]["Address2"].ToString() + "Toll Free:" + ds.Rows[0]["TollFree1"].ToString() + "<br>Phone:" + ds.Rows[0]["Phone1"].ToString() + "<br>Fax:" + ds.Rows[0]["Fax"].ToString() + "<br>Email:" + ds.Rows[0]["Email"].ToString() + "<br>Website:" + ds.Rows[0]["SiteUrl"].ToString() + "";
        if (ds.Rows[0]["MasterEmailId"].ToString() != "" && ds.Rows[0]["EmailSentDisplayName"].ToString() != "")
        {
            MailAddress to = new MailAddress(To);

            MailAddress from = new MailAddress("" + ds.Rows[0]["MasterEmailId"] + "", "" + ds.Rows[0]["EmailSentDisplayName"] + "");


            MailMessage objEmail = new MailMessage(from, to);


            SqlDataAdapter daffff = new SqlDataAdapter("select warehousemaster.name from warehousemaster where comid='" + Session["Comid"] + "' and WareHouseId='" + ddlwarehouse.SelectedValue + "'", con);
            DataTable dtfff = new DataTable();
            daffff.Fill(dtfff);

            objEmail.Subject = "New Employee Account Creation- " + dtfff.Rows[0]["name"].ToString() + "";


            objEmail.Body = body.ToString();
            objEmail.IsBodyHtml = true;


            objEmail.Priority = MailPriority.Normal;


            SmtpClient client = new SmtpClient();

            client.Credentials = new NetworkCredential("" + ds.Rows[0]["MasterEmailId"] + "", "" + ds.Rows[0]["EmailMasterLoginPassword"] + "");
            client.Host = ds.Rows[0]["OutGoingMailServer"].ToString();


            client.Send(objEmail);
            return true;
        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Please, First Set Master Email Server";
            return false;
        }
    }
    public string getWelcometext()
    {


        string str = "SELECT EmailContentMaster.EmailContent, EmailContentMaster.EntryDate, CompanyWebsitMaster.SiteUrl, EmailTypeMaster.EmailTypeId " +
                    " FROM CompanyWebsitMaster INNER JOIN " +
                      " EmailContentMaster ON CompanyWebsitMaster.CompanyWebsiteMasterId = EmailContentMaster.CompanyWebsiteMasterId INNER JOIN " +
                      " EmailTypeMaster ON EmailContentMaster.EmailTypeId = EmailTypeMaster.EmailTypeId " +
                    " WHERE     (EmailTypeMaster.EmailTypeId = 1)  and (EmailTypeMaster.Compid='" + Session["Comid"].ToString() + "')" +
                    " ORDER BY EmailContentMaster.EntryDate DESC ";
        SqlCommand cmd = new SqlCommand(str, con);

        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        string welcometext = "";
        if (ds.Rows.Count > 0)
        {
            welcometext = ds.Rows[0]["EmailContent"].ToString();

        } return welcometext;

    }
    protected void fillgriddata()
    {
        string str1 = "";
        lblCompany.Text = Session["Cname"].ToString();
        lblBusiness.Text = filterwarehouse.SelectedItem.Text;
        lbldepartmentprint.Text = "Department Name :" + ddlfilterdept.SelectedItem.Text;
        lbldesignationprint.Text = "Designation Name :" + ddlfilterdesig.SelectedItem.Text;
        lblstatusname.Text = "Status Name :" + ddlfilterbyactive.SelectedItem.Text;
        lblfilbatchname.Text = "Batch Name :" + ddlfilterbatch.SelectedItem.Text;
        lblsupervisorname.Text = "Supervisor Name :" + ddlsupervisor.SelectedItem.Text;
        lblserchkey.Text = "";

        if (filterwarehouse.SelectedIndex > 0)
        {
            str1 = " and EmployeeMaster.Whid='" + filterwarehouse.SelectedValue + "' ";
        }
        if (ddlfilterdept.SelectedIndex > 0)
        {
            str1 += " and DepartmentmasterMNC.id='" + ddlfilterdept.SelectedValue + "' ";

        }
        if (ddlfilterdesig.SelectedIndex > 0)
        {
            str1 += " and DesignationMaster.DesignationMasterId='" + ddlfilterdesig.SelectedValue + "' ";

        }
        if (ddlfilterbyactive.SelectedValue != "2")
        {
            str1 += " and EmployeeMaster.Active='" + ddlfilterbyactive.SelectedValue + "'";
        }
        if (ddlfilterbatch.SelectedIndex > 0)
        {
            str1 += " and EmployeeBatchMaster.Batchmasterid='" + ddlfilterbatch.SelectedValue + "'";
        }
        if (ddlsupervisor.SelectedIndex > 0)
        {
            str1 += " and EmployeeMaster.suprviserid = '" + ddlsupervisor.SelectedValue + "'";
        }
        if (txtsearch.Text != "")
        {
            lblserchkey.Text = "Search By :" + txtsearch.Text;
            str1 += " and ((EmployeeMaster.EmployeeName like '%" + txtsearch.Text.Replace("'", "''") + "%') or (EmployeeMaster.ContactNo like '%" + txtsearch.Text.Replace("'", "''") + "%') or (EmployeeMaster.Email like '%" + txtsearch.Text.Replace("'", "''") + "%') or (EmployeeMaster.Address like '%" + txtsearch.Text.Replace("'", "''") + "%') or (Party_master.Zipcode like '%" + txtsearch.Text.Replace("'", "''") + "%'))";
        }

        if (ddlfilterposition.SelectedIndex > 0)
        {
            str1 += " and EmployeeMaster.Jobpositionid = '" + ddlfilterposition.SelectedValue + "'";
        }

        if (ddlfilterqualification.SelectedIndex > 0)
        {
            str1 += " and EmployeeMaster.EducationqualificationID = '" + ddlfilterqualification.SelectedValue + "'";
        }

        if (ddlfiltersubject.SelectedIndex > 0)
        {
            str1 += " and EmployeeMaster.SpecialSubjectID = '" + ddlfiltersubject.SelectedValue + "'";
        }

        if (txtfilterexpr.Text != "")
        {
            str1 += " and EmployeeMaster.yearofexperience >= '" + txtfilterexpr.Text + "'";
        }


        string str = "  WarehouseMaster.Name as wname,EmployeeMaster.EmployeeMasterID as DocumentId,Party_Master.Zipcode,EmployeeMaster.EmployeeMasterID,s1.EmployeeName as  supervisor,EmployeeMaster.PartyID, EmployeeMaster.DeptID, EmployeeMaster.DesignationMasterId, EmployeeMaster.EmployeeTypeId,case when(EmployeeMaster.Active = '1') then 'Active' else 'Inactive' end as Active, EmployeeMaster.DateOfJoin, EmployeeMaster.Email, EmployeeMaster.ContactNo, EmployeeMaster.AccountNo, EmployeeMaster.EmployeeName, DepartmentmasterMNC.Departmentname, DesignationMaster.DesignationName, EmployeeType.EmployeeTypeName,CityMasterTbl.CityName,EmployeeBatchMaster.Batchmasterid,BatchMaster.ID,BatchMaster.Name FROM    WarehouseMaster Inner Join EmployeeMaster on EmployeeMaster.Whid=WarehouseMaster.WarehouseId inner join Party_master on Party_master.PartyID = EmployeeMaster.PartyID left outer JOIN  EmployeeType ON EmployeeMaster.EmployeeTypeId = EmployeeType.EmployeeTypeId left outer join EmployeeBatchMaster on EmployeeBatchMaster.Employeeid = EmployeeMaster.EmployeeMasterID left outer join BatchMaster on BatchMaster.ID = EmployeeBatchMaster.Batchmasterid inner JOIN  DesignationMaster ON EmployeeMaster.DesignationMasterId = DesignationMaster.DesignationMasterId inner JOIN DepartmentmasterMNC on DepartmentmasterMNC.id=DesignationMaster.DeptID left join  CityMasterTbl on CityMasterTbl.CityId=EmployeeMaster.City left join StateMasterTbl on StateMasterTbl.StateId=EmployeeMaster.StateId left join CountryMaster on CountryMaster.CountryId=EmployeeMaster.CountryId left join EmployeeMaster as s1 on s1.EmployeeMasterId = EmployeeMaster.suprviserid where  DepartmentmasterMNC.Companyid='" + Session["comid"] + "' and WarehouseMaster.Status='1' " + str1 + "";
        //order by  wname,EmployeeName,supervisor,Departmentname,DesignationName";

        string str2 = " select count(EmployeeMaster.EmployeeMasterID) as ci FROM    WarehouseMaster Inner Join EmployeeMaster on EmployeeMaster.Whid=WarehouseMaster.WarehouseId inner join Party_master on Party_master.PartyID = EmployeeMaster.PartyID left outer JOIN  EmployeeType ON EmployeeMaster.EmployeeTypeId = EmployeeType.EmployeeTypeId left outer join EmployeeBatchMaster on EmployeeBatchMaster.Employeeid = EmployeeMaster.EmployeeMasterID left outer join BatchMaster on BatchMaster.ID = EmployeeBatchMaster.Batchmasterid inner JOIN  DesignationMaster ON EmployeeMaster.DesignationMasterId = DesignationMaster.DesignationMasterId  Left Outer JOIN DepartmentmasterMNC on DepartmentmasterMNC.id=DesignationMaster.DeptID left join  CityMasterTbl on CityMasterTbl.CityId=EmployeeMaster.City left join StateMasterTbl on StateMasterTbl.StateId=EmployeeMaster.StateId left join CountryMaster on CountryMaster.CountryId=EmployeeMaster.CountryId left join EmployeeMaster as s1 on s1.EmployeeMasterId = EmployeeMaster.suprviserid where  DepartmentmasterMNC.Companyid='" + Session["comid"] + "' and WarehouseMaster.Status='1' " + str1 + "";

        GridView1.VirtualItemCount = GetRowCount(str2);

        string sortExpression = " WareHouseMaster.Name,EmployeeMaster.EmployeeName,DepartmentmasterMNC.Departmentname,DesignationMaster.DesignationName asc";

        if (Convert.ToInt32(ViewState["count"]) > 0)
        {
            DataTable ds = GetDataPage(GridView1.PageIndex, GridView1.PageSize, sortExpression, str);

            for (int rowindex = 0; rowindex < ds.Rows.Count; rowindex++)
            {
                DataTable dtcrNew11 = clsDocument.SelectOfficeManagerDocuments1(Convert.ToString(ds.Rows[rowindex]["EmployeeMasterID"]));

                ds.Rows[rowindex]["DocumentId"] = dtcrNew11.Rows[0]["DocumentCount"];

            }

            if (ds.Rows.Count > 0)
            {
                DataView myDataView = new DataView();
                myDataView = ds.DefaultView;

                if (hdnsortExp.Value != string.Empty)
                {
                    myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
                }
                GridView1.DataSource = myDataView;
                GridView1.DataBind();
            }
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }

        //SqlCommand cmd = new SqlCommand(str, con);
        //SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //DataTable ds = new DataTable();
        //adp.Fill(ds);
    }

    private int GetRowCount(string str)
    {
        int count = 0;
        DataTable dte = new DataTable();
        dte = select(str);
        if (dte.Rows.Count > 0)
        {
            count += Convert.ToInt32(dte.Rows[0]["ci"]);
        }
        ViewState["count"] = count;
        return count;

    }

    private DataTable GetDataPage(int pageIndex, int pageSize, string sortExpression, string query)
    {
        DataTable dt = select(string.Format("SELECT * FROM (select TOP {0} ROW_NUMBER() OVER (ORDER BY {1}) as ROW_NUM,   " + " {2} ORDER BY ROW_NUM) innerSelect WHERE ROW_NUM > {3}", ((pageIndex + 1) * pageSize), sortExpression, query, (pageIndex * pageSize)));

        dt.Columns.Remove("ROW_NUM");

        return dt;

        ViewState["dt"] = dt;
    }
    protected void filterwarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {

        fillfilterdepartment();
        fillfilterdesignation();
        fillfilterbatch();
        fillfiltersupervisor();
        fillgriddata();
    }
    protected void ddlfilterdept_SelectedIndexChanged(object sender, EventArgs e)
    {

        fillfilterdesignation();
        fillgriddata();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            //pnlgrid.ScrollBars = ScrollBars.None;
            //pnlgrid.Height = new Unit("100%");

            Button1.Text = "Hide Printable Version";
            Button7.Visible = true;

            GridView1.AllowPaging = false;
            GridView1.PageSize = 10000;
            fillgriddata();

            if (GridView1.Columns[12].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[12].Visible = false;
            }
            if (GridView1.Columns[13].Visible == true)
            {
                ViewState["delHide"] = "tt";
                GridView1.Columns[13].Visible = false;
            }
            if (GridView1.Columns[14].Visible == true)
            {
                ViewState["viewhide"] = "tt";
                GridView1.Columns[14].Visible = false;
            }
            if (GridView1.Columns[15].Visible == true)
            {
                ViewState["viewhideas"] = "tt";
                GridView1.Columns[15].Visible = false;
            }
        }
        else
        {

            //pnlgrid.ScrollBars = ScrollBars.Vertical;
            //pnlgrid.Height = new Unit(400);

            Button1.Text = "Printable Version";
            Button7.Visible = false;

            GridView1.AllowPaging = true;
            GridView1.PageSize = 30;
            fillgriddata();

            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[12].Visible = true;
            }
            if (ViewState["delHide"] != null)
            {
                GridView1.Columns[13].Visible = true;
            }
            if (ViewState["viewhide"] != null)
            {
                GridView1.Columns[14].Visible = true;
            }
            if (ViewState["viewhideas"] != null)
            {
                GridView1.Columns[15].Visible = true;
            }
        }
    }
    protected void ddlfilterbyactive_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgriddata();
    }
    protected void ddlfilterdesig_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillfiltersupervisor();
        fillgriddata();
    }

    protected void ImageButton3_Click1(object sender, EventArgs e)
    {
        ModalPopupExtenderAddnew.Hide();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Send")
        {
            //grid.SelectedIndex = 
            ////ViewState["MissionId"] = grid.SelectedDataKey.Value;

            //int index = grid.SelectedIndex;

            //Label MId = (Label)grid.Rows[index].FindControl("lblMasterId");
            ViewState["employeemmid"] = Convert.ToInt32(e.CommandArgument);

            // DataTable dtcrNew11 = clsDocument.SelectOfficedocwithmissionId(Convert.ToString(ViewState["MissionId"]));
            DataTable dtcrNew11 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["employeemmid"]), 500);
            GridView2.DataSource = dtcrNew11;
            GridView2.DataBind();
            ModalPopupExtenderAddnew.Show();



        }

        if (e.CommandName == "vi")
        {
           // Pnl_login.Visible = false;

            CheckBox6.Enabled = false;
            Panel6.Visible = true;
            RadioButtonList2.SelectedValue = "0";

            ImageButton9.Visible = false;
            ImageButton22.Visible = true;

            ImageButton29.Visible = true;
            ImageButton30.Visible = false;

            MultiView1.ActiveViewIndex = 0;

            panelemergency.Visible = true;
            Label37.Visible = true;
            chkempbarcode.Visible = false;
            Label28.Visible = true;
            chksunday.Visible = true;
            Label29.Visible = true;
            //chkaccessright.Visible = true;

            chkeduquali.Checked = false;
            chkeduquali_CheckedChanged(sender, e);
            chkspecialsub.Checked = false;
            chkspecialsub_CheckedChanged(sender, e);
            chkjobposition.Checked = false;
            chkjobposition_CheckedChanged(sender, e);

            lblmsg.Text = "";
            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument.ToString());
            ViewState["editid"] = GridView1.SelectedIndex;

            RadioButtonList3.Items[1].Enabled = true;

            fillemployeeownsetup();


            panel3rd.Visible = false;

            pnlparty.Visible = true;
            // lbladd.Text = "Edit Employee";
            btnshowparty.Visible = false;

            chkaccessright.Checked = true;
            pnlaccess.Visible = true;

            pnlemployeedate.Visible = true;
            chkemppayroll.Checked = true;
            Pnlemppayroll.Visible = true;
            chkempbarcode.Checked = true;
            pnlempbarcode.Visible = true;

            SqlDataAdapter daemer = new SqlDataAdapter("select * from EmployeeEmergencyContactTbl where EmployeeMasterID='" + ViewState["editid"] + "'", con);
            DataTable dtemer = new DataTable();
            daemer.Fill(dtemer);

            if (dtemer.Rows.Count > 0)
            {

                TextBox5.Text = dtemer.Rows[0]["FirstEmergencyContactName"].ToString();
                TextBox7.Text = dtemer.Rows[0]["FirstEmergencyRelationship"].ToString();
                TextBox9.Text = dtemer.Rows[0]["FirstEmergencyPhoneNumberhome"].ToString();
                TextBox11.Text = dtemer.Rows[0]["FirstEmergencyPhoneNumbercell"].ToString();
                TextBox13.Text = dtemer.Rows[0]["FirstEmergencyPhoneNumberWork"].ToString();
                TextBox15.Text = dtemer.Rows[0]["FirstEmergencyEmail"].ToString();
                TextBox6.Text = dtemer.Rows[0]["SecondEmergencyContactName"].ToString();
                TextBox8.Text = dtemer.Rows[0]["SecondEmergencyRelationship"].ToString();
                TextBox10.Text = dtemer.Rows[0]["SecondEmergencyPhoneNumberhome"].ToString();
                TextBox12.Text = dtemer.Rows[0]["SecondEmergencyPhoneNumbercell"].ToString();
                TextBox14.Text = dtemer.Rows[0]["SecondEmergencyPhoneNumberWork"].ToString();
                TextBox16.Text = dtemer.Rows[0]["SecondEmergencyEmail"].ToString();
            }

            SqlDataAdapter adptemp = new SqlDataAdapter("Select EmployeeMaster.*,Party_master.PartyTypeCategoryNo,EmployeePayrollMaster.EmployeeNo,Party_master.Zipcode,Party_master.PartyTypeId,EmployeeBarcodeMaster.Barcode,EmployeeBarcodeMaster.Employeecode,EmployeeBarcodeMaster.Biometricno,EmployeeBarcodeMaster.blutoothid,EmployeePayrollMaster.PaymentMethodId,EmployeePayrollMaster.PaymentReceivedNameOf,EmployeePayrollMaster.PaypalId,EmployeePayrollMaster.PaymentEmailId,EmployeePayrollMaster.DirectDepositBankName,EmployeePayrollMaster.DirectDepositBankCode,EmployeePayrollMaster.DirectDepositBankBranchAddress,EmployeePayrollMaster.DirectDepositBankBranchcity,EmployeePayrollMaster.DirectDepositBankBranchstate,EmployeePayrollMaster.DirectDepositBankBranchcountry,EmployeePayrollMaster.DirectDepositBankBranchzipcode,EmployeePayrollMaster.DirectDepositBankIFCNumber,EmployeePayrollMaster.DirectDepositBankSwiftNumber,EmployeePayrollMaster.DirectDepositBankEmployeeEmail,EmployeePayrollMaster.DirectDepositBankBranchName,EmployeePayrollMaster.DirectDepositBankBranchNumber,EmployeePayrollMaster.DirectDepositTransitNumber,EmployeePayrollMaster.DirectDepositAccountHolderName,EmployeePayrollMaster.DirectDepositBankAccountType,EmployeePayrollMaster.DirectDepositBankAccountNumber,EmployeePayrollMaster.PayPeriodMasterId,EmployeePayrollMaster.LastName,EmployeePayrollMaster.FirstName,EmployeePayrollMaster.Intials,EmployeePayrollMaster.EmployeeNo,EmployeePayrollMaster.DateOfBirth,EmployeePayrollMaster.SocialSecurityNo,EmployeePayrollMaster.EmployeePaidAsPerDesignation,EmployeePayrollMaster.RegisterBankAddress  from EmployeeMaster left join EmployeePayrollMaster on EmployeeMaster.EmployeeMasterID = EmployeePayrollMaster.EmpId  left join EmployeeBarcodeMaster on  EmployeePayrollMaster.EmpId = EmployeeBarcodeMaster.Employee_Id inner join Party_master on EmployeeMaster.PartyID = Party_master.PartyID where EmployeeMaster.EmployeeMasterID = '" + ViewState["editid"] + "'", con);
            DataTable dt = new DataTable();
            adptemp.Fill(dt);


                ViewState["partyid"] = dt.Rows[0]["PartyID"].ToString();             
                //IOffice Employee Master Edit
                ddlwarehouse.SelectedIndex = ddlwarehouse.Items.IndexOf(ddlwarehouse.Items.FindByValue(dt.Rows[0]["Whid"].ToString()));
                filldept();
                ddldept.SelectedIndex = ddldept.Items.IndexOf(ddldept.Items.FindByValue(dt.Rows[0]["DeptID"].ToString()));
                object obu = new object();
                EventArgs evtu = new EventArgs();
                ddldept_SelectedIndexChanged(obu, evtu);
                ddldesignation.SelectedIndex = ddldesignation.Items.IndexOf(ddldesignation.Items.FindByValue(dt.Rows[0]["DesignationMasterId"].ToString()));
               ddlActive.SelectedIndex = ddlActive.Items.IndexOf(ddlActive.Items.FindByValue(dt.Rows[0]["StatusMasterId"].ToString()));
                ddlemptype.SelectedIndex = ddlemptype.Items.IndexOf(ddlemptype.Items.FindByValue(dt.Rows[0]["EmployeeTypeId"].ToString()));            
                txtjoindate.Text = Convert.ToDateTime(dt.Rows[0]["DateOfJoin"]).ToShortDateString();
                tbAddress.Text=dt.Rows[0]["Address"].ToString();
                lblstreetadd.Text = dt.Rows[0]["Address"].ToString();

                ddlCountry.SelectedIndex = ddlCountry.Items.IndexOf(ddlCountry.Items.FindByValue(dt.Rows[0]["CountryId"].ToString()));
                object obs = new object();
                EventArgs evts = new EventArgs();
                ddlCountry_SelectedIndexChanged(obs, evts);
                lblcountry.Text = ddlCountry.SelectedItem.Text;

                ddlState.SelectedIndex = ddlState.Items.IndexOf(ddlState.Items.FindByValue(dt.Rows[0]["StateId"].ToString()));
                object obc = new object();
                EventArgs evtc = new EventArgs();
                ddlState_SelectedIndexChanged(obc, evtc);
                lblstate.Text = ddlState.SelectedItem.Text;

                ddlCity.SelectedIndex = ddlCity.Items.IndexOf(ddlCity.Items.FindByValue(dt.Rows[0]["City"].ToString()));
                object o = new object();
                EventArgs ev = new EventArgs();
                ddlCity_SelectedIndexChanged(o, ev);
                lblcity.Text = ddlCity.SelectedItem.Text;

                tbPhone.Text = dt.Rows[0]["ContactNo"].ToString();
                lblphone.Text = dt.Rows[0]["ContactNo"].ToString();
                tbEmail.Text = dt.Rows[0]["Email"].ToString();
                lblemail.Text = dt.Rows[0]["Email"].ToString();
                
                string id = dt.Rows[0]["AccountId"].ToString();//
                accid = dt.Rows[0]["AccountNo"].ToString();//

                txtlastname.Text = dt.Rows[0]["EmployeeName"].ToString();
               
            

              
               // Description=dt.Rows[0]["Description"].ToString();
                fillsupervisor();
                ddlemp.SelectedIndex = ddlemp.Items.IndexOf(ddlemp.Items.FindByValue(dt.Rows[0]["SuprviserId"].ToString()));
                if (Convert.ToBoolean(dt.Rows[0]["Active"].ToString()) == true)
                {
                    ddlActive.SelectedValue = "1";
                }
                else
                {
                    ddlActive.SelectedValue = "0";
                }
                workphone.Text = dt.Rows[0]["WorkPhone"].ToString();
                workext.Text = dt.Rows[0]["Workext"].ToString();
                txtworkemail.Text = dt.Rows[0]["WorkEmail"].ToString();
                filleducationquali();
                ddleduquali.SelectedIndex = ddleduquali.Items.IndexOf(ddleduquali.Items.FindByValue(dt.Rows[0]["EducationqualificationID"].ToString())); 
                fillspecialsub();
                ddlspecialsub.SelectedIndex = ddlspecialsub.Items.IndexOf(ddlspecialsub.Items.FindByValue(dt.Rows[0]["SpecialSubjectID"].ToString()));           
                txtyearexpr.Text = dt.Rows[0]["yearofexperience"].ToString();            
                filllastjobposition();
                ddljobposition.SelectedIndex = ddljobposition.Items.IndexOf(ddljobposition.Items.FindByValue(dt.Rows[0]["Jobpositionid"].ToString()));

                if (Convert.ToString(dt.Rows[0]["sex"]) != "")
                {
                    Radiogender.SelectedValue = dt.Rows[0]["sex"].ToString();
                }
                 TextBox17.Text = dt.Rows[0]["Amount"].ToString();
                 DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByValue(dt.Rows[0]["Payper"].ToString()));
            
         //      ,[Description]
         //,[SuprviserId]
      //

            //Ioffice Party Master Edit
            //Party_master.PartyTypeCategoryNo,
              string str = "Select Id From PartyMasterCategory where PartyMasterCategoryNo='" + dt.Rows[0]["PartyTypeCategoryNo"].ToString() + "' order by Name ";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable datat = new DataTable();
            adp.Fill(datat);
            if (datat.Rows.Count > 0)
            {
                ddlpartycate.SelectedIndex = ddlpartycate.Items.IndexOf(ddlpartycate.Items.FindByValue(datat.Rows[0]["Id"].ToString()));
            }
            //Party_master.PartyTypeId,
            ddlPartyType.SelectedIndex = ddlPartyType.Items.IndexOf(ddlPartyType.Items.FindByValue(dt.Rows[0]["PartyTypeId"].ToString()));
            object ob = new object();
            EventArgs evt = new EventArgs();
            ddlPartyType_SelectedIndexChanged(ob, evt);
            
            //EmployeePayrollMaster Edit

            TextBox1.Text = dt.Rows[0]["EmployeeNo"].ToString(); 
             txtbarcode.Text = dt.Rows[0]["Barcode"].ToString();
            txtsecuritycode.Text = dt.Rows[0]["Employeecode"].ToString();
            txtbiometricid.Text = dt.Rows[0]["Biometricno"].ToString();
            txtbluetoothid.Text = dt.Rows[0]["blutoothid"].ToString();
            
            txtlastname.Text = dt.Rows[0]["LastName"].ToString();
            txtfirstname.Text = dt.Rows[0]["FirstName"].ToString();
            txtintialis.Text = dt.Rows[0]["Intials"].ToString();
            

            if (dt.Rows[0]["DateOfBirth"].ToString() != "")
            {
                DateTime t1 = Convert.ToDateTime(dt.Rows[0]["DateOfBirth"].ToString());
                txtdateofbirth.Text = t1.ToShortDateString();
            }
            if (dt.Rows[0]["SocialSecurityNo"].ToString() != "")
            {
                txtsecurityno.Text = dt.Rows[0]["SocialSecurityNo"].ToString();
            }
            if (dt.Rows[0]["PayPeriodMasterId"].ToString() != "")
            {
                ddlPaymentCycle.SelectedValue = dt.Rows[0]["PayPeriodMasterId"].ToString();
            }


            

            SqlDataAdapter dasalaryy = new SqlDataAdapter("select * from employeesalarymaster where EmployeeId='" + ViewState["editid"] + "'", con);
            DataTable dtsalaryy = new DataTable();
            dasalaryy.Fill(dtsalaryy);

            if (dtsalaryy.Rows.Count > 0)
            {
                Fillaccount();
                ddlremuneration.SelectedIndex = ddlremuneration.Items.IndexOf(ddlremuneration.Items.FindByValue(dtsalaryy.Rows[0]["Remuneration_Id"].ToString()));
                txtamount.Text = dtsalaryy.Rows[0]["Amount"].ToString();
                ddlpaybleper.SelectedIndex = ddlpaybleper.Items.IndexOf(ddlpaybleper.Items.FindByValue(dtsalaryy.Rows[0]["PayablePer_PeriodMasterId"].ToString()));
            }

            //Login Detail  Edit
            DataTable dtseluser = new DataTable();
            dtseluser = (DataTable)select(" Select Login_master.password, User_master.* from User_master inner join Login_master on  Login_master.UserID=User_master.UserID where PartyID='" + dt.Rows[0]["PartyID"].ToString() + "'");
            if (dtseluser.Rows.Count > 0)
            {
                string logoname = dtseluser.Rows[0]["Photo"].ToString();

                imgLogo.ImageUrl = "~/ShoppingCart/images/" + logoname;

                tbUserName.Text = dtseluser.Rows[0]["Username"].ToString();

                string strqa = PageMgmt.Decrypted(dtseluser.Rows[0]["password"].ToString());

                ViewState["newpass"] = strqa;

                tbPassword.Attributes.Add("Value", strqa);
                tbConPassword.Attributes.Add("Value", strqa);
                tbPassword.Text = PageMgmt.Decrypted(dtseluser.Rows[0]["password"].ToString());
                tbConPassword.Text = PageMgmt.Decrypted(dtseluser.Rows[0]["password"].ToString());

                ViewState["passd"] = "1";

                

                if (dt.Rows[0]["EmployeePaidAsPerDesignation"].ToString() != "")
                {
                    RadioButtonList1.SelectedValue = dt.Rows[0]["EmployeePaidAsPerDesignation"].ToString();
                    RadioButtonList1_SelectedIndexChanged(sender, e);
                }
               
                 if (dt.Rows[0]["PaymentMethodId"].ToString() != "")
                {
                    ddlPaymentMethod.SelectedValue = dt.Rows[0]["PaymentMethodId"].ToString();
                }
                if (ddlPaymentMethod.SelectedIndex > 0)
                {
                    if (ddlPaymentMethod.SelectedItem.Text == "Demand Draft" || ddlPaymentMethod.SelectedItem.Text == "Cheque")
                    {
                        ddpnl.Visible = false;
                        pnlreceivepayment.Visible = true;
                        txtPaymentReceivedNameOf.Text = dt.Rows[0]["PaymentReceivedNameOf"].ToString();
                    }
                    else
                    {
                        ddpnl.Visible = false;
                        pnlreceivepayment.Visible = false;
                    }
                    if (ddlPaymentMethod.SelectedItem.Text == "Paypal")
                    {
                        ddpnl.Visible = false;
                        pnlpaypalid.Visible = true;
                        txtPaypalId.Text = dt.Rows[0]["PaypalId"].ToString();
                    }
                    else
                    {
                        ddpnl.Visible = false;
                        pnlpaypalid.Visible = false;
                    }
                    if (ddlPaymentMethod.SelectedItem.Text == "By Email")
                    {
                        ddpnl.Visible = false;
                        pnlpaymentemail.Visible = true;
                        txtPaymentEmailId.Text = dt.Rows[0]["PaymentEmailId"].ToString();
                    }
                    else
                    {
                        ddpnl.Visible = false;
                        pnlpaymentemail.Visible = false;

                    }

                    if (ddlPaymentMethod.SelectedItem.Text == "Direct Deposit")
                    {
                        ddpnl.Visible = true;
                        txtDirectDepositAccountHolderName.Text = dt.Rows[0]["DirectDepositAccountHolderName"].ToString();
                        txtDirectDepositBankAccountNumber.Text = dt.Rows[0]["DirectDepositBankAccountNumber"].ToString();
                        ddlDirectDepositBankAccountType.SelectedItem.Text = dt.Rows[0]["DirectDepositBankAccountType"].ToString();
                        txtDirectDepositBankBranchName.Text = dt.Rows[0]["DirectDepositBankBranchName"].ToString();
                        //txtDirectDepositBankCode.Text = dt.Rows[0]["DirectDepositBankCode"].ToString();
                        txtDirectDepositBankName.Text = dt.Rows[0]["DirectDepositBankName"].ToString();
                        //txtDirectDepositBankBranchNumber.Text = dt.Rows[0]["DirectDepositBankBranchNumber"].ToString();
                        txtDirectDepositTransitNumber.Text = dt.Rows[0]["DirectDepositTransitNumber"].ToString();
                        txtDirectDepositBranchAddress.Text = dt.Rows[0]["DirectDepositBankBranchAddress"].ToString();

                        TextBox2.Text = dt.Rows[0]["RegisterBankAddress"].ToString();

                        ddlDirectDepositBankBranchcountry.SelectedIndex = ddlDirectDepositBankBranchcountry.Items.IndexOf(ddlDirectDepositBankBranchcountry.Items.FindByValue(dt.Rows[0]["DirectDepositBankBranchcountry"].ToString()));

                        object obs1 = new object();
                        EventArgs evts1 = new EventArgs();
                        ddlDirectDepositBankBranchcountry_SelectedIndexChanged(obs1, evts1);
                        ddlDirectDepositBankBranchstate.SelectedIndex = ddlDirectDepositBankBranchstate.Items.IndexOf(ddlDirectDepositBankBranchstate.Items.FindByValue(dt.Rows[0]["DirectDepositBankBranchstate"].ToString()));

                        object obc2 = new object();
                        EventArgs evtc2 = new EventArgs();
                        ddlDirectDepositBankBranchstate_SelectedIndexChanged(obc2, evtc2);
                        ddlDirectDepositBankBranchcity.SelectedIndex = ddlDirectDepositBankBranchcity.Items.IndexOf(ddlDirectDepositBankBranchcity.Items.FindByValue(dt.Rows[0]["DirectDepositBankBranchcity"].ToString()));

                        ddlDirectDepositBankBranchzipcode.Text = dt.Rows[0]["DirectDepositBankBranchzipcode"].ToString();
                        txtDirectDepositifscnumber.Text = dt.Rows[0]["DirectDepositBankIFCNumber"].ToString();
                        txtDirectDepositswiftnumber.Text = dt.Rows[0]["DirectDepositBankSwiftNumber"].ToString();
                        txtdirectdipositemployeeemail.Text = dt.Rows[0]["DirectDepositBankEmployeeEmail"].ToString();
                    }
                    else
                    {
                        ddpnl.Visible = false;
                    }
                }
                fillemployeedesg();


                tbExtension.Text = dtseluser.Rows[0]["Extention"].ToString();
                lblext.Text = dtseluser.Rows[0]["Extention"].ToString();

                tbZipCode.Text = dtseluser.Rows[0]["zipcode"].ToString();
                lblzipcode.Text = dtseluser.Rows[0]["zipcode"].ToString();

                DataTable dtstatus = new DataTable();

                dtstatus = (DataTable)select("select StatusControl.statusmasterid,statuscategory.statuscategory,statuscategory.statuscategorymasterid,statusmaster.statusname,statusmaster.statusId from StatusControl inner join statusmaster on StatusControl.statusmasterid=statusmaster.statusId inner join statuscategory on statusmaster.statuscategorymasterid = statuscategory.statuscategorymasterid where StatusControl.UserMasterId = '" + dtseluser.Rows[0]["UserId"].ToString() + "' and TranctionMasterId IS NULL and SalesOrderId IS NULL and SalesChallanMasterId IS NULL");

                if (dtstatus.Rows.Count > 0)
                {
                    ddlstatusname.SelectedIndex = ddlstatusname.Items.IndexOf(ddlstatusname.Items.FindByValue(dtstatus.Rows[0]["statusId"].ToString()));

                    chksunday.Checked = true;
                    Panel3.Visible = true;
                    grdstatuscategory.Visible = true;
                    grdstatuscategory.DataSource = dtstatus;
                    grdstatuscategory.DataBind();
                }
                // imgempphoto.ImageUrl = "~/Account/" + Session["comid"] + "/EmpPhoto/" + dtseluser.Rows[0]["Photo"].ToString();

            }


            DataTable dtbatch = new DataTable();
            dtbatch = (DataTable)select("Select * From EmployeeBatchMaster where Employeeid='" + dt.Rows[0]["EmployeeMasterID"].ToString() + "'");
            fillbatch();
            if (dtbatch.Rows.Count > 0)
            {
                ViewState["EmpBid"] = dtbatch.Rows[0]["Id"].ToString();
                ddlbatch.SelectedIndex = ddlbatch.Items.IndexOf(ddlbatch.Items.FindByValue(dtbatch.Rows[0]["Batchmasterid"].ToString()));
            }
            gridAccess.DataSource = null;

            gridAccess.DataBind();
            if (dt.Rows[0]["EmployeeMasterID"].ToString() != "" && dt.Rows[0]["EmployeeMasterID"].ToString() != null)
            {

                pnlaccess.Visible = true;
                string str1 = "select  distinct  CASE  When (EmployeeWarehouseRights.Id IS NULL) Then '0' ELSE EmployeeWarehouseRights.Id End as Id, WareHouseMaster.WareHouseId,WareHouseMaster.Name,EmployeeWarehouseRights.EmployeeId, CASE  When (EmployeeWarehouseRights.AccessAllowed IS NULL)  Then  Cast( '0' as bit) ELSE EmployeeWarehouseRights.AccessAllowed end AS AccessAllowed from EmployeeWarehouseRights Right join WareHouseMaster on WareHouseMaster.WareHouseId= EmployeeWarehouseRights.Whid and EmployeeWarehouseRights.EmployeeId='" + dt.Rows[0]["EmployeeMasterID"].ToString() + "' where  comid='" + Session["Comid"] + "' and Status='1'  order by WareHouseMaster.Name ";

                SqlCommand cmdfillgrid = new SqlCommand(str1, con);
                SqlDataAdapter adpfillgrid = new SqlDataAdapter(cmdfillgrid);
                DataTable dtfill = new DataTable();
                adpfillgrid.Fill(dtfill);

                DataView myDataView = new DataView();
                myDataView = dtfill.DefaultView;

                if (hdnsortExp.Value != string.Empty)
                {
                    myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
                }

                gridAccess.DataSource = myDataView;
                gridAccess.DataBind();


            }
            //string ststatus = "select StatusMasterId from StatusControl where UserMasterId='" + Convert.ToInt32(dtseluser.Rows[0]["UserID"].ToString()) + "' and TranctionMasterId IS NULL and SalesOrderId IS NULL and SalesChallanMasterId IS NULL";
            //SqlCommand cmstatus = new SqlCommand(ststatus, con);
            //SqlDataAdapter dastatus = new SqlDataAdapter(cmstatus);
            //DataTable dtstatus = new DataTable();
            //dastatus.Fill(dtstatus);

            //foreach (GridViewRow grd in grdstatuscategory.Rows)
            //{
            //    Label lblstatusid = (Label)grd.FindControl("lblstatusid");
            //    CheckBox Chkstatus = (CheckBox)grd.FindControl("Chkstatus");

            //    if (lblstatusid.Text == dtstatus.Rows[0]["StatusMasterId"].ToString())
            //    {
            //        Chkstatus.Checked = true;
            //    }
            //}
            string strole1 = "select Role_id from User_Role where User_id='" + Convert.ToInt32(dtseluser.Rows[0]["UserID"]).ToString() + "'";
            SqlCommand cmrl1 = new SqlCommand(strole1, con);
            SqlDataAdapter dauprole1 = new SqlDataAdapter(cmrl1);
            DataTable dtrl1 = new DataTable();
            dauprole1.Fill(dtrl1);
            ddlemprole.SelectedIndex = ddlemprole.Items.IndexOf(ddlemprole.Items.FindByValue(dtrl1.Rows[0]["Role_id"].ToString()));

            pnllabeladdress.Visible = true;
            addnewaddrpnl.Visible = false;
            addnewaddr.Visible = true;
            pnllastaddress.Visible = true;

            SqlDataAdapter adptaddress = new SqlDataAdapter("Select PartyAddressTbl.*,PartyAddressTbl.Address +' : '+ convert(nvarchar,PartyAddressTbl.datetime,101) as lastaddress,convert(nvarchar,PartyAddressTbl.datetime,101) as new from PartyAddressTbl where PartyMasterId='" + dt.Rows[0]["PartyID"].ToString() + "' order by Id desc", con);
            DataTable dtaddress = new DataTable();
            adptaddress.Fill(dtaddress);
            if (dtaddress.Rows.Count > 0)
            {
                ddlpreviousaddress.DataSource = dtaddress;
                ddlpreviousaddress.DataTextField = "lastaddress";
                ddlpreviousaddress.DataValueField = "Id";
                ddlpreviousaddress.DataBind();
                if (dtaddress.Rows[0]["new"].ToString() != "")
                {
                    lbldate.Text = dtaddress.Rows[0]["new"].ToString();
                }
            }

            imgbtnupdate.Visible = true;
            btnSubmit.Visible = false;
            if (ddlemptype.SelectedItem.Text == "Admin")
            {
                Button5.Visible = true;
                Button6.Visible = true;
            }
            else
            {
                Button5.Visible = false;
                Button6.Visible = false;
            }

        }
        if (e.CommandName == "del")
        {
            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            ViewState["empid"] = GridView1.SelectedIndex;

            DataTable dtempidsel = new DataTable();
            dtempidsel = (DataTable)select("Select PartyID from EmployeeMaster where EmployeeMasterID='" + ViewState["empid"] + "'");
            if (dtempidsel.Rows.Count > 0)
            {
                ViewState["partyid"] = dtempidsel.Rows[0]["PartyID"].ToString();

                DataTable dtaccidsel = new DataTable();
                //  Radhika Chnages

                //dtaccidsel = (DataTable)select("Select Account from  Party_master where PartyID = '" + ViewState["partyid"].ToString() + "'");

                dtaccidsel = (DataTable)select("Select Account from  Party_master,PartytTypeMaster where PartyID = '" + ViewState["partyid"].ToString() + "'  and PartytTypeMaster.PartyTypeId= Party_master.PartyTypeId and PartytTypeMaster.compid='" + Session["comid"] + "'");


                if (dtaccidsel.Rows.Count > 0)
                {
                    SqlCommand cmddel = new SqlCommand("SELECT  AccountMasterId, DeleteAllowed FROM Fixeddata where AccountMasterId='" + dtaccidsel.Rows[0]["Account"].ToString() + "'", con);
                    SqlDataAdapter dtpdel = new SqlDataAdapter(cmddel);
                    DataTable dtdel = new DataTable();
                    dtpdel.Fill(dtdel);
                    if (dtdel.Rows.Count > 0)
                    {
                        if (dtdel.Rows[0]["DeleteAllowed"].ToString() == "0")
                        {
                            ModalPopupExtender145.Show();
                        }
                        else
                        {
                            ModalPopupExtender2.Show();
                        }
                    }
                    else
                    {
                        ModalPopupExtender145.Show();
                    }
                }
            }
        }

        if (e.CommandName == "View")
        {
            //GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            //ViewState["empidview"] = GridView1.SelectedIndex;

            int dk = Convert.ToInt32(e.CommandArgument);
            string te = "Employeeprofile.aspx?EmployeeMasterID=" + dk;
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

        }
    }
    protected void clear()
    {
        ViewState["data"] = null;

        GridView7.DataSource = null;
        GridView7.DataBind();

        txtdoctitle.Text = "";
        TxtDocDate.Text = "";
        panelforresume.Visible = false;

        ViewState["tbPassword"] = "";

        duplicateworkemail.Text = "";
        duplicatetbemail.Text = "";
        lblusernameavailableornot.Text = "";
        txtfirstname.Text = "";
        txtlastname.Text = "";
        txtintialis.Text = "";
        txtdateofbirth.Text = "";
        txtsecurityno.Text = "";
        tbAddress.Text = "";
        ddlCountry.SelectedIndex = 0;
        ddlState.SelectedIndex = 0;
        ddlCity.SelectedIndex = 0;
        tbZipCode.Text = "";
        ddlwarehouse.SelectedIndex = 0;
        ddlPartyType.SelectedIndex = 0;
        ddlpartycate.SelectedIndex = 0;
        ddlemprole.SelectedIndex = 0;
        ddldept.SelectedIndex = 0;
        ddldesignation.SelectedIndex = 0;
        ddlbatch.SelectedIndex = 0;
        ddlemp.SelectedIndex = 0;
        ddlemptype.SelectedIndex = 0;
        tbPhone.Text = "";
        tbExtension.Text = "";
        tbEmail.Text = "";
        txtjoindate.Text = System.DateTime.Now.ToShortDateString();
        ddlActive.SelectedIndex = 0;
        tbUserName.Text = "";
        tbPassword.Text = "";
        tbConPassword.Text = "";
        ddlPaymentMethod.SelectedIndex = 0;
        ddlPaymentCycle.SelectedIndex = 0;
        txtPaymentReceivedNameOf.Text = "";
        txtPaymentEmailId.Text = "";
        txtPaypalId.Text = "";
        txtDirectDepositAccountHolderName.Text = "";
        txtDirectDepositBankAccountNumber.Text = "";
        txtDirectDepositBankBranchName.Text = "";
        txtDirectDepositBankName.Text = "";
        txtDirectDepositTransitNumber.Text = "";
        ddlDirectDepositBankAccountType.SelectedIndex = 0;
        txtDirectDepositBranchAddress.Text = "";
        ddlDirectDepositBankBranchcity.SelectedIndex = 0;
        ddlDirectDepositBankBranchstate.SelectedIndex = 0;
        ddlDirectDepositBankBranchcountry.SelectedIndex = 0;
        ddlDirectDepositBankBranchzipcode.Text = "";
        txtDirectDepositifscnumber.Text = "";
        txtDirectDepositswiftnumber.Text = "";
        txtdirectdipositemployeeemail.Text = "";
        txtbarcode.Text = "";
        txtbiometricid.Text = "";
        txtbluetoothid.Text = "";
        TextBox5.Text = "";
        TextBox6.Text = "";
        TextBox7.Text = "";
        TextBox8.Text = "";
        TextBox9.Text = "";
        TextBox10.Text = "";
        TextBox11.Text = "";
        TextBox12.Text = "";
        TextBox13.Text = "";
        TextBox14.Text = "";
        TextBox15.Text = "";
        TextBox16.Text = "";
        imgLogo.ImageUrl = "";
        chk111.Checked = false;

        chkeduquali.Checked = false;
        pnleduqauli.Visible = false;
        //chkeduquali_CheckedChanged(sender, e);
        chkspecialsub.Checked = false;
        pnlspecialsub.Visible = false;
        //chkspecialsub_CheckedChanged(sender, e);
        chkjobposition.Checked = false;
        pnljobposition.Visible = false;
        //chkjobposition_CheckedChanged(sender, e);



        ddleduquali.SelectedIndex = 0;
        ddlspecialsub.SelectedIndex = 0;
        ddljobposition.SelectedIndex = 0;
        txtyearexpr.Text = "";
        txteduquali.Text = "";
        txtspecialsub.Text = "";
        txtjobposition.Text = "";

        string pass1 = tbPassword.Text;
        tbPassword.Attributes.Clear();
        string pass2 = tbConPassword.Text;
        tbConPassword.Attributes.Clear();
        chksunday.Checked = false;
        Panel3.Visible = false;
        chkaccessright.Checked = false;
        pnlaccess.Visible = false;
        //   chkemplogin.Checked = false;
        pnlemployeedate.Visible = false;
        //   chkemppayroll.Checked = false;
        Pnlemppayroll.Visible = false;
        chkempbarcode.Checked = true;
        pnlempbarcode.Visible = true;
        grdstatuscategory.DataSource = null;
        grdstatuscategory.DataBind();
        ViewState["dt"] = null;
        grdstatuscategory.Visible = false;
        if (Request.QueryString["Id"] == null)
        {
            ddlstatuscategory.SelectedIndex = 0;
            ddlstatusname.SelectedIndex = 0;
        }
        foreach (GridViewRow gdr in gridAccess.Rows)
        {
            CheckBox ChkAess = (CheckBox)gdr.FindControl("ChkAess");
            ChkAess.Checked = false;
        }
        txtworkemail.Text = "";
        workphone.Text = "";
        workext.Text = "";
        ddpnl.Visible = false;
        string securitycode = alphanum();
        txtsecuritycode.Attributes.Add("Value", securitycode);

        //lblempno = number(8).ToString();
        //TextBox1.Text = lblempno.Substring(0, 4);

        //TextBox2.Text = lblempno.Substring(4, 4);
        //TextBox3.Text = lblempno.Substring(8, 4);
        //TextBox4.Text = lblempno.Substring(12, 4);
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        clear();
        lblmsg.Text = "";
        imgbtnupdate.Visible = false;
        btnSubmit.Visible = true;

        pnllabeladdress.Visible = false;
        addnewaddrpnl.Visible = true;
        addnewaddr.Visible = false;
        pnllastaddress.Visible = false;

        pnlparty.Visible = false;
        // lbladd.Text = "";
        btnshowparty.Visible = true;
        Panel6.Visible = false;

        //int i1 = 0;

        //for (i1 = 1000; i1 <= 9999; i1++)
        //{
        //    string strusercheck = " select EmployeeNo from EmployeePayrollMaster where Compid='" + Session["Comid"].ToString() + "' and EmployeeNo='" + i1 + "'";
        //    SqlCommand cmdusercheck = new SqlCommand(strusercheck, con);
        //    SqlDataAdapter adpusercheck = new SqlDataAdapter(cmdusercheck);
        //    DataTable dsusercheck = new DataTable();
        //    adpusercheck.Fill(dsusercheck);

        //    if (dsusercheck.Rows.Count > 0)
        //    {
        //        TextBox1.Text = (i1 + 1).ToString();
        //    }
        //    else
        //    {
        //        TextBox1.Text = i1.ToString();
        //        break;
        //    }

        //}

        //MultiView1.ActiveViewIndex = 0;

    }
    protected void chksunday_CheckedChanged(object sender, EventArgs e)
    {
        if (chksunday.Checked == true)
        {
            Panel3.Visible = true;
        }
        else
        {
            Panel3.Visible = false;
        }
    }
    protected void ddlfilterbatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgriddata();
    }
    protected void ddlsupervisor_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgriddata();
    }
    protected void txtsearch_TextChanged(object sender, EventArgs e)
    {
        fillgriddata();
    }


    protected void ddlstatuscategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillstatus();
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        if (ViewState["dt"] == null)
        {
            DataTable dt = new DataTable();

            DataColumn dtcom = new DataColumn();
            dtcom.DataType = System.Type.GetType("System.String");
            dtcom.ColumnName = "StatusCategory";
            dtcom.ReadOnly = false;
            dtcom.Unique = false;
            dtcom.AllowDBNull = true;

            dt.Columns.Add(dtcom);

            DataColumn dtcomid = new DataColumn();
            dtcomid.DataType = System.Type.GetType("System.String");
            dtcomid.ColumnName = "StatusCategoryMasterId";
            dtcomid.ReadOnly = false;
            dtcomid.Unique = false;
            dtcomid.AllowDBNull = true;

            dt.Columns.Add(dtcomid);

            DataColumn dtcomd = new DataColumn();
            dtcomd.DataType = System.Type.GetType("System.String");
            dtcomd.ColumnName = "StatusName";
            dtcomd.ReadOnly = false;
            dtcomd.Unique = false;
            dtcomd.AllowDBNull = true;

            dt.Columns.Add(dtcomd);


            DataColumn dtcom12 = new DataColumn();
            dtcom12.DataType = System.Type.GetType("System.String");
            dtcom12.ColumnName = "StatusId";
            dtcom12.ReadOnly = false;
            dtcom12.Unique = false;
            dtcom12.AllowDBNull = true;

            dt.Columns.Add(dtcom12);



            DataRow dtrow = dt.NewRow();

            dtrow["StatusCategory"] = ddlstatuscategory.SelectedItem.Text;
            dtrow["StatusCategoryMasterId"] = ddlstatuscategory.SelectedValue;
            dtrow["StatusName"] = ddlstatusname.SelectedItem.Text;
            dtrow["StatusId"] = ddlstatusname.SelectedValue;

            dt.Rows.Add(dtrow);
            ViewState["dt"] = dt;
            grdstatuscategory.Visible = true;
            grdstatuscategory.DataSource = dt;
            grdstatuscategory.DataBind();
        }
        else
        {
            int flag = 0;
            foreach (GridViewRow item in grdstatuscategory.Rows)
            {
                //string StatusId = item.Cells[0].Text;
                Label lblstatusid = (Label)item.FindControl("lblstatusid");
                if (lblstatusid.Text == ddlstatusname.SelectedValue)
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "Record already exists";
                    flag = 1;
                    break;
                }
            }
            if (flag == 0)
            {
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["dt"];

                DataRow dtrow = dt.NewRow();
                dtrow["StatusCategory"] = ddlstatuscategory.SelectedItem.Text;
                dtrow["StatusCategoryMasterId"] = ddlstatuscategory.SelectedValue;
                dtrow["StatusName"] = ddlstatusname.SelectedItem.Text;
                dtrow["StatusId"] = ddlstatusname.SelectedValue;


                dt.Rows.Add(dtrow);
                ViewState["dt"] = dt;

                grdstatuscategory.DataSource = dt;
                grdstatuscategory.DataBind();
                grdstatuscategory.Visible = true;
            }
        }
    }
    protected void grdstatuscategory_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        grdstatuscategory.SelectedIndex = Convert.ToInt32(grdstatuscategory.DataKeys[e.RowIndex].Value.ToString());
        DataTable dt = new DataTable();
        dt = (DataTable)ViewState["dt"];
        dt.Rows.Remove(dt.Rows[e.RowIndex]);

        if (dt.Rows.Count == 0)
        {
            grdstatuscategory.DataSource = null;
            grdstatuscategory.DataBind();
        }
        else
        {
            grdstatuscategory.DataSource = dt;
            grdstatuscategory.DataBind();
        }

        ViewState["dt"] = dt;
    }
    protected void grdstatuscategory_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdstatuscategory.SelectedIndex = Convert.ToInt32(grdstatuscategory.DataKeys[e.NewEditIndex].Value.ToString());
        DataTable dt = new DataTable();
        dt = (DataTable)ViewState["dt"];
        dt.Rows.Contains(dt.Rows[grdstatuscategory.SelectedIndex]);
        ddlstatuscategory.SelectedIndex = ddlstatuscategory.Items.IndexOf(ddlstatuscategory.Items.FindByValue(dt.Rows[0]["StatusCategoryMasterId"].ToString()));
        ddlstatusname.SelectedIndex = ddlstatusname.Items.IndexOf(ddlstatusname.Items.FindByValue(dt.Rows[0]["StatusId"].ToString()));
    }
    protected void grdstatuscategory_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void ddlpreviousaddress_SelectedIndexChanged(object sender, EventArgs e)
    {

        SqlDataAdapter adptaddress = new SqlDataAdapter(" Select PartyAddressTbl.*,convert(nvarchar,PartyAddressTbl.datetime,101) as new from PartyAddressTbl where Id = '" + ddlpreviousaddress.SelectedValue + "'", con);
        DataTable dtaddress = new DataTable();
        adptaddress.Fill(dtaddress);
        if (dtaddress.Rows.Count > 0)
        {
            pnllabeladdress.Visible = true;
            addnewaddrpnl.Visible = false;
            addnewaddr.Visible = true;
            pnllastaddress.Visible = true;
            Button5.Visible = true;
            Button6.Visible = true;
            ddlCountry.SelectedIndex = ddlCountry.Items.IndexOf(ddlCountry.Items.FindByValue(dtaddress.Rows[0]["Country"].ToString()));

            ddlCountry_SelectedIndexChanged(sender, e);
            lblcountry.Text = ddlCountry.SelectedItem.Text;

            ddlState.SelectedIndex = ddlState.Items.IndexOf(ddlState.Items.FindByValue(dtaddress.Rows[0]["State"].ToString()));

            ddlState_SelectedIndexChanged(sender, e);
            lblstate.Text = ddlState.SelectedItem.Text;

            ddlCity.SelectedIndex = ddlCity.Items.IndexOf(ddlCity.Items.FindByValue(dtaddress.Rows[0]["City"].ToString()));

            ddlCity_SelectedIndexChanged(sender, e);
            lblcity.Text = ddlCity.SelectedItem.Text;

            lblstreetadd.Text = dtaddress.Rows[0]["Address"].ToString();
            lblzipcode.Text = dtaddress.Rows[0]["zipcode"].ToString();
            lblphone.Text = dtaddress.Rows[0]["Phone"].ToString();
            lblemail.Text = dtaddress.Rows[0]["email"].ToString();
            lbldate.Text = dtaddress.Rows[0]["new"].ToString();
        }
        else
        {
            pnllabeladdress.Visible = false;
            addnewaddrpnl.Visible = true;
            addnewaddr.Visible = false;
            pnllastaddress.Visible = false;
            Button6.Visible = false;
            Button5.Visible = false;
        }

    }
    protected void addnewaddr_Click(object sender, EventArgs e)
    {
        pnllabeladdress.Visible = false;
        addnewaddrpnl.Visible = true;
        addnewaddr.Visible = false;
        pnllastaddress.Visible = false;
        Button5.Visible = false;
        Button6.Visible = false;

        tbAddress.Text = "";
        ddlCountry.SelectedIndex = 0;
        ddlState.SelectedIndex = 0;
        ddlCity.SelectedIndex = 0;
        lbldate.Text = System.DateTime.Now.ToShortDateString();
        tbZipCode.Text = "";
        tbPhone.Text = "";
        tbEmail.Text = "";
        tbExtension.Text = "";

    }
    protected void btnshowparty_Click(object sender, EventArgs e)
    {
        fillspecialsub();

        Panel6.Visible = true;
        MultiView1.ActiveViewIndex = 0;
        chksunday_CheckedChanged(sender, e);
        ImageButton9.Visible = false;
        ImageButton22.Visible = true;

        ImageButton29.Visible = true;
        ImageButton30.Visible = false;

        if (Request.QueryString["Id"] == "1")
        {
            panelemergency.Visible = false;
            Label37.Visible = false;
            chkempbarcode.Visible = false;
            Label28.Visible = false;
            chksunday.Visible = false;
            Label29.Visible = false;
            chkaccessright.Visible = false;
            Button5.Visible = false;
            Button6.Visible = false;

            lblmsg.Text = "";
            pnlparty.Visible = true;

            fillstore();
            ddlwarehouse_SelectedIndexChanged(sender, e);

            chkempbarcode.Checked = true;
            chkempbarcode_CheckedChanged(sender, e);
            chkemppayroll_CheckedChanged(sender, e);
            if (ddlPaymentMethod.SelectedIndex > 0)
            {
                ddlPaymentMethod.SelectedValue = "2";
            }
            if (ddlPaymentCycle.SelectedIndex > 0)
            {
                ddlPaymentCycle.SelectedIndex = 1;
            }
            RadioButtonList2.SelectedValue = "1";
            //  lbladd.Text = "Add New Employee";
            btnshowparty.Visible = false;

            //chkeduquali.Checked = false;
            //chkeduquali_CheckedChanged(sender, e);
            //chkspecialsub.Checked = false;
            //chkspecialsub_CheckedChanged(sender, e);
            //chkjobposition.Checked = false;
            //chkjobposition_CheckedChanged(sender, e);
        }

        else
        {
            panelemergency.Visible = true;
            Label37.Visible = true;
            chkempbarcode.Visible = false;
            Label28.Visible = true;
            chksunday.Visible = true;
            Label29.Visible = true;
            //chkaccessright.Visible = true;
            Button5.Visible = false;
            Button6.Visible = false;

            lblmsg.Text = "";
            pnlparty.Visible = true;

            fillstore();
            ddlwarehouse_SelectedIndexChanged(sender, e);

            chkempbarcode.Checked = true;
            chkempbarcode_CheckedChanged(sender, e);

            chkemppayroll_CheckedChanged(sender, e);
            if (ddlPaymentMethod.SelectedIndex > 0)
            {
                ddlPaymentMethod.SelectedValue = "2";
            }
            if (ddlPaymentCycle.SelectedIndex > 0)
            {
                ddlPaymentCycle.SelectedIndex = 1;
            }
            RadioButtonList2.SelectedValue = "1";
            //   lbladd.Text = "Add New Employee";
            btnshowparty.Visible = false;

            //chkeduquali.Checked = false;
            //chkeduquali_CheckedChanged(sender, e);
            //chkspecialsub.Checked = false;
            //chkspecialsub_CheckedChanged(sender, e);
            //chkjobposition.Checked = false;
            //chkjobposition_CheckedChanged(sender, e);
        }

        int i = 0;

        for (i = 1000; i <= 9999; i++)
        {
            string strusercheck = " select EmployeeNo from EmployeePayrollMaster where Compid='" + Session["Comid"].ToString() + "' and EmployeeNo='" + i + "'";
            SqlCommand cmdusercheck = new SqlCommand(strusercheck, con);
            SqlDataAdapter adpusercheck = new SqlDataAdapter(cmdusercheck);
            DataTable dsusercheck = new DataTable();
            adpusercheck.Fill(dsusercheck);

            if (dsusercheck.Rows.Count > 0)
            {
                TextBox1.Text = (i + 1).ToString();
            }
            else
            {
                TextBox1.Text = i.ToString();
                break;
            }

        }
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtable = new DataTable();
            dtable = (DataTable)select("select UserID from User_master where PartyID='" + ViewState["partyid"] + "'");
            if (con.State.ToString() == "Open")
            {
                con.Close();

            }
            else
            {
                con.Open();
            }
            if (dtable.Rows.Count > 0)
            {
                Delete("Delete from Login_master Where UserID='" + dtable.Rows[0]["UserID"].ToString() + "'");
                Delete("delete from User_master where PartyID = '" + ViewState["partyid"] + "'");
                Delete("Delete From SalesChallanMaster where PartyID='" + ViewState["partyid"] + "'");
                DataTable dtsalodsup = new DataTable();
                dtsalodsup = (DataTable)select("select SalesOrderId from SalesOrderMaster where PartyId='" + ViewState["partyid"] + "'");
                if (dtsalodsup.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtsalodsup.Rows)
                    {
                        Delete("delete from SalesOrderSuppliment where SalesOrderMasterId = '" + dr["SalesOrderId"].ToString() + "'");
                    }
                }

                Delete("delete from SalesOrderMaster where PartyId = '" + ViewState["partyid"] + "'");
                Delete("Delete from EmployeeEmergencyContactTbl where EmployeeMasterID='" + ViewState["empid"] + "'");
                Delete("Delete from EmployeeMaster where PartyID='" + ViewState["partyid"] + "'");
                Delete("Delete from EmployeeBatchMaster where Employeeid='" + ViewState["empid"] + "' ");
                Delete("Delete from EmployeePayrollMaster where EmpId = '" + ViewState["empid"] + "'");
                Delete("Delete from EmployeeBarcodeMaster where Employee_Id = '" + ViewState["empid"] + "'");
                DataTable dttid = new DataTable();
                dttid = (DataTable)select("Select Account,Whid from Party_master where PartyID='" + ViewState["partyid"] + "'");
                Delete("delete from Party_master where PartyID = '" + ViewState["partyid"] + "'");




                if (dttid.Rows.Count > 0)
                {
                    Delete("Delete from AccountBalanceLimit where AccountId = '" + dttid.Rows[0]["Account"].ToString() + "' and  Whid='" + dttid.Rows[0]["Whid"].ToString() + "'");
                    Delete("Delete from AccountMaster where AccountId='" + dttid.Rows[0]["Account"].ToString() + "' and  Whid='" + dttid.Rows[0]["Whid"].ToString() + "'");
                    DataTable dtt = new DataTable();
                    dtt = (DataTable)select("Select TranctionMaster.Tranction_Master_Id from TranctionMaster  inner join Tranction_Details on Tranction_Details.Tranction_Master_Id=TranctionMaster.Tranction_Master_Id where AccountDebit='" + dttid.Rows[0]["Account"].ToString() + "' and TranctionMaster.Whid='" + dttid.Rows[0]["Whid"].ToString() + "'");
                    if (dtt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtt.Rows)
                        {
                            Delete("Delete from Tranction_Details where Tranction_Master_Id = '" + dr["Tranction_Master_Id"].ToString() + "'");
                            Delete("Delete from TranctionMaster where Tranction_Master_Id = '" + dr["Tranction_Master_Id"].ToString() + "'");
                            Delete("Delete from TranctionMasterSuppliment where Tranction_Master_Id = '" + dr["Tranction_Master_Id"].ToString() + "'");
                            Delete("Delete From PaymentApplicationTbl where TranMIdPayReceived = '" + dr["Tranction_Master_Id"].ToString() + "'");
                            Delete("Delete From PaymentApplicationTbl where TranMIdAmtApplied = '" + dr["Tranction_Master_Id"].ToString() + "'");
                        }

                    }
                    DataTable dtt1 = new DataTable();
                    dtt1 = (DataTable)select("Select TranctionMaster.Tranction_Master_Id from TranctionMaster inner join Tranction_Details on Tranction_Details.Tranction_Master_Id=TranctionMaster.Tranction_Master_Id where AccountCredit='" + dttid.Rows[0]["Account"].ToString() + "' and TranctionMaster.Whid='" + dttid.Rows[0]["Whid"].ToString() + "'");
                    if (dtt1.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtt1.Rows)
                        {
                            Delete("Delete from Tranction_Details where Tranction_Master_Id = '" + dr["Tranction_Master_Id"].ToString() + "'");
                            Delete("Delete from TranctionMaster where Tranction_Master_Id = '" + dr["Tranction_Master_Id"].ToString() + "'");
                            Delete("Delete from TranctionMasterSuppliment where Tranction_Master_Id = '" + dr["Tranction_Master_Id"].ToString() + "'");
                            Delete("Delete From PaymentApplicationTbl where TranMIdPayReceived = '" + dr["Tranction_Master_Id"].ToString() + "'");
                            Delete("Delete From PaymentApplicationTbl where TranMIdAmtApplied = '" + dr["Tranction_Master_Id"].ToString() + "'");
                        }

                    }
                }

            }
            fillgriddata();
        }
        catch
        {
            throw;
        }
        finally
        {
            //ModalPopupExtender1xz.Show();
            ModalPopupExtender145.Hide();
            lblmsg.Text = "";
            lblmsg.Text = "Record deleted successfully";
            // fillgriddata();

        }
    }
    protected void ImageButton5_Click(object sender, EventArgs e)
    {
        ModalPopupExtender145.Hide();
    }
    protected void ImageButton8_Click(object sender, EventArgs e)
    {
        ModalPopupExtender2.Hide();
    }
    protected void Delete(string query)
    {
        SqlCommand cmd = new SqlCommand(query, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd.ExecuteNonQuery();
        con.Close();
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        pnllabeladdress.Visible = false;
        addnewaddrpnl.Visible = true;
        addnewaddr.Visible = true;
        pnllastaddress.Visible = false;
        Button5.Visible = false;

    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        Delete("Delete from PartyAddressTbl where Id = '" + ddlpreviousaddress.SelectedValue + "'");

        SqlDataAdapter adptaddress = new SqlDataAdapter(" Select PartyAddressTbl.*,PartyAddressTbl.Address +' : '+ convert(nvarchar,PartyAddressTbl.datetime,101) as lastaddress,convert(nvarchar,PartyAddressTbl.datetime,101) as new from PartyAddressTbl where PartyMasterId='" + ViewState["partyid"] + "' order by Id desc", con);
        DataTable dtaddress = new DataTable();
        adptaddress.Fill(dtaddress);
        if (dtaddress.Rows.Count > 0)
        {
            ddlpreviousaddress.DataSource = dtaddress;
            ddlpreviousaddress.DataTextField = "lastaddress";
            ddlpreviousaddress.DataValueField = "Id";
            ddlpreviousaddress.DataBind();
            if (dtaddress.Rows[0]["new"].ToString() != "")
            {
                lbldate.Text = dtaddress.Rows[0]["new"].ToString();
            }
        }
        ddlpreviousaddress_SelectedIndexChanged(sender, e);
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

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
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        //hdnsortExp.Value = e.SortExpression.ToString();
        //hdnsortDir.Value = sortOrder; // sortOrder;
        //fillgriddata();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgriddata();
    }
    protected void ddlDirectDepositBankBranchcountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillbankstate();
    }
    protected void ddlDirectDepositBankBranchstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillbankcity();
    }
    //protected void txtlastname_TextChanged(object sender, EventArgs e)
    //{
    // //   string lastname = txtlastname.Text;
    //}
    //protected void txtfirstname_TextChanged(object sender, EventArgs e)
    //{
    ////    string firstname = txtfirstname.Text;
    //}
    protected void txtintialis_TextChanged(object sender, EventArgs e)
    {
        string lastname = txtlastname.Text;
        string firstname = txtfirstname.Text;
        string initials = txtintialis.Text;

        string summ = firstname + " " + initials + " " + lastname;
        txtPaymentReceivedNameOf.Text = summ;

        string summ1 = lastname + "" + firstname.Substring(0, 1);
        tbUserName.Text = summ1;
        tbUserName_TextChanged(sender, e);
    }
    protected void imgadddstacat_Click(object sender, ImageClickEventArgs e)
    {
        string te = "statuscategoryaddmanage.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void imgrefreshstacat_Click(object sender, ImageClickEventArgs e)
    {
        fillstatusgrid();
    }
    protected void imgadddstaname_Click(object sender, ImageClickEventArgs e)
    {
        string te = "statusmasteraddmanage.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void imgrefreshstaname_Click(object sender, ImageClickEventArgs e)
    {
        fillstatus();
    }

    protected void imgadddepart_Click(object sender, ImageClickEventArgs e)
    {
        string te = "departmentaddmanage.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void imgrefreshdepart_Click(object sender, ImageClickEventArgs e)
    {
        filldept();
    }
    protected void imgadddesign_Click(object sender, ImageClickEventArgs e)
    {
        string te = "designationaddmanage.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void imgrefreshdesign_Click(object sender, ImageClickEventArgs e)
    {
        filldesignation();
    }
    protected void imgaddbatch_Click(object sender, ImageClickEventArgs e)
    {
        string te = "batchmaster.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void imgrefreshbatch_Click(object sender, ImageClickEventArgs e)
    {
        fillbatch();
    }
    protected void imgaddemptype_Click(object sender, ImageClickEventArgs e)
    {
        string te = "employeetype.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void imgrefreshemptype_Click(object sender, ImageClickEventArgs e)
    {
        fillemployeetype();
    }
    protected void imgadddrolema_Click(object sender, ImageClickEventArgs e)
    {
        string te = "rolemaster.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void imgrefreshrolema_Click(object sender, ImageClickEventArgs e)
    {
        fillrole();
    }
    protected void btnvwaccessright_Click(object sender, EventArgs e)
    {
        if (ddldesignation.SelectedIndex > 0)
        {
            string str1 = "Select RoleId as DefaultRole from DesignationMaster where DesignationMasterId='" + ddldesignation.SelectedValue + "' ";
            SqlCommand cmd1Role = new SqlCommand(str1, con);
            SqlDataAdapter adp1 = new SqlDataAdapter(cmd1Role);
            DataTable dty = new DataTable();
            adp1.Fill(dty);
            if (dty.Rows.Count > 0)
            {
                // dty.Rows[0]["DefaultRole"].ToString();
            }
            string te = "http://license.busiwiz.com/admin/Page_role_AccessShow.aspx?roleid=" + dty.Rows[0]["DefaultRole"].ToString()+"&clientid="+ Session["ClientId"].ToString(); 
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        }
    }
    //protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (RadioButton2.Checked == true)
    //    {

    //    }
    //    if (RadioButton2.Checked == false)
    //    {
    //        pnlemployeedate.Visible = false;
    //    }
    //}
    protected void RadioButtonList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList2.SelectedValue == "0")
        {
            pnlemployeedate.Visible = true;
        }
        if (RadioButtonList2.SelectedValue == "1")
        {
            pnlemployeedate.Visible = false;
        }
    }
    protected void tbEmail_TextChanged(object sender, EventArgs e)
    {
        string str = " select Email from EmployeeMaster where Email='" + tbEmail.Text + "'";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);

        if (ds.Rows.Count > 0)
        {
            duplicatetbemail.Visible = true;
            duplicatetbemail.Text = "This Email is already used.";
        }
        else
        {
            duplicatetbemail.Visible = true;
            duplicatetbemail.Text = "Email Available";
        }
    }
    protected void txtworkemail_TextChanged(object sender, EventArgs e)
    {
        string str = " select WorkEmail from EmployeeMaster where WorkEmail='" + txtworkemail.Text + "'";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);

        if (ds.Rows.Count > 0)
        {
            duplicateworkemail.Visible = true;
            duplicateworkemail.Text = "This Email is already used.";
        }
        else
        {
            duplicateworkemail.Visible = true;
            duplicateworkemail.Text = "Email Available";
        }
    }
    protected void ddleduquali_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillspecialsub();
    }

    protected void fillspecialsub()
    {
        ddlspecialsub.Items.Clear();

        string str11 = "select SpecialisedSubjectTBL.Id,SpecialisedSubjectTBL.SubjectName as Name from SpecialisedSubjectTBL where SpecialisedSubjectTBL.compid='" + Session["Comid"] + "' and SpecialisedSubjectTBL.Status='1' order by SpecialisedSubjectTBL.SubjectName asc";
        SqlCommand cmd1 = new SqlCommand(str11, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        adp1.Fill(dt1);

        if (dt1.Rows.Count > 0)
        {
            ddlspecialsub.DataSource = dt1;
            ddlspecialsub.DataTextField = "Name";
            ddlspecialsub.DataValueField = "Id";
            ddlspecialsub.DataBind();
        }
        ddlspecialsub.Items.Insert(0, "-Select-");
        ddlspecialsub.Items[0].Value = "0";
    }

    protected void filllastjobposition()
    {
        ddljobposition.Items.Clear();

        string str11 = "select VacancyPositionTitle,ID from VacancyPositionTitleMaster where Active='1' order by VacancyPositionTitle asc";
        SqlCommand cmd11 = new SqlCommand(str11, con);
        SqlDataAdapter adp11 = new SqlDataAdapter(cmd11);
        DataTable dt11 = new DataTable();
        adp11.Fill(dt11);

        if (dt11.Rows.Count > 0)
        {
            ddljobposition.DataSource = dt11;
            ddljobposition.DataTextField = "VacancyPositionTitle";
            ddljobposition.DataValueField = "ID";
            ddljobposition.DataBind();
        }
        ddljobposition.Items.Insert(0, "-Select-");
        ddljobposition.Items[0].Value = "0";
    }
    protected void chkeduquali_CheckedChanged(object sender, EventArgs e)
    {
        if (chkeduquali.Checked == true)
        {
            ddleduquali.Visible = false;
            pnleduqauli.Visible = true;
        }
        if (chkeduquali.Checked == false)
        {
            pnleduqauli.Visible = false;
            ddleduquali.Visible = true;
        }
    }
    protected void chkspecialsub_CheckedChanged(object sender, EventArgs e)
    {
        if (chkspecialsub.Checked == true)
        {
            ddlspecialsub.Visible = false;
            pnlspecialsub.Visible = true;
        }
        if (chkspecialsub.Checked == false)
        {
            pnlspecialsub.Visible = false;
            ddlspecialsub.Visible = true;
        }
    }
    protected void chkjobposition_CheckedChanged(object sender, EventArgs e)
    {
        if (chkjobposition.Checked == true)
        {
            ddljobposition.Visible = false;
            pnljobposition.Visible = true;
        }
        if (chkjobposition.Checked == false)
        {
            pnljobposition.Visible = false;
            ddljobposition.Visible = true;
        }
    }

    private string ReturnExtension(string fileExtension)
    {
        switch (fileExtension)
        {
            case ".htm":
            case ".html":
            case ".log":
                return "text/HTML";
            case ".txt":
                return "text/plain";
            case ".doc":
                return "application/ms-word";
            case ".tiff":
            case ".tif":
                return "image/tiff";
            case ".asf":
                return "video/x-ms-asf";
            case ".avi":
                return "video/avi";
            case ".zip":
                return "application/zip";
            case ".xls":
            case ".csv":
                return "application/vnd.ms-excel";
            case ".gif":
                return "image/gif";
            case ".jpg":
            case "jpeg":
                return "image/jpeg";
            case ".bmp":
                return "image/bmp";
            case ".wav":
                return "audio/wav";
            case ".mp3":
                return "audio/mpeg3";
            case ".mpg":
            case "mpeg":
                return "video/mpeg";
            case ".rtf":
                return "application/rtf";
            case ".asp":
                return "text/asp";
            case ".pdf":
                return "application/pdf";
            case ".fdf":
                return "application/vnd.fdf";
            case ".ppt":
                return "application/mspowerpoint";
            case ".dwg":
                return "image/vnd.dwg";
            case ".msg":
                return "application/msoutlook";
            case ".xml":
            case ".sdxl":
                return "application/xml";
            case ".xdp":
                return "application/vnd.adobe.xdp+xml";
            default:
                return "application/octet-stream";
        }
    }

    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            DataTable dt = new DataTable();
            dt = clsDocument.SelectDoucmentMasterByIDwithobj(Convert.ToInt32(e.CommandArgument));

            string docname = dt.Rows[0]["DocumentName"].ToString();
            string filepath = Server.MapPath("~//Account//" + Session["comid"] + "//UploadedDocuments//" + docname);
            string name = docname.Trim();
            string extension = name.Substring(name.Length - 3);
            //if (Convert.ToString(extension) == "pdf")
            //{
            Session["ABCDE"] = "ABCDE";

            //                    string popupScript = "<script language='javascript'>" +
            //"newWindow=window.open('ViewDocument.aspx?id='" + e.CommandArgument + ", 'welcome','fullscreen=yes,status=yes,top=0,left=0,menubar=no,status=no')" + "</script>";
            int docid = 0;
            docid = Convert.ToInt32(e.CommandArgument);

            //                    Page.RegisterClientScriptBlock("newWindow", popupScript);
            //LinkButton lnkbtn = (LinkButton)Gridreqinfo.FindControl("LinkButton1");
            //lnkbtn.Attributes.Add("onclick", "window.open('ViewDocument.aspx?id='" + e.CommandArgument + ",, 'welcome','fullscreen=yes,status=yes,top=0,left=0,menubar=no,status=no')");


            String temp = "ViewDocument.aspx?id=" + docid + "&Siddd=VHDS";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + temp + "');", true);


            //    Response.Redirect("ViewDocument.aspx?id=" + docid + "&Siddd=VHDS");
            //}
            //else
            //{
            //    FileInfo file = new FileInfo(filepath);

            //    if (file.Exists)
            //    {
            //        Response.ClearContent();
            //        Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
            //        Response.AddHeader("Content-Length", file.Length.ToString());
            //        Response.ContentType = ReturnExtension(file.Extension.ToLower());
            //        Response.TransmitFile(file.FullName);

            //        Response.End();

            //    }
            //}
        }
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox1.Checked == true)
        {
            panelforfilters.Visible = true;
        }
        if (CheckBox1.Checked == false)
        {
            panelforfilters.Visible = false;
        }
    }

    protected void filterfilleducationquali()
    {
        ddlfilterqualification.Items.Clear();

        // string str = "select AreaofStudiesTbl.ID,AreaofStudiesTbl.Name,EducationDegrees.DegreeName,AreaofStudiesTbl.Name + ' : ' + EducationDegrees.DegreeName as Education from EducationDegrees inner join AreaofStudiesTbl on EducationDegrees.AreaofStudyID=AreaofStudiesTbl.ID where AreaofStudiesTbl.Active='1' order by AreaofStudiesTbl.Name,EducationDegrees.DegreeName asc";
        string str = "select AreaofStudiesTbl.ID,AreaofStudiesTbl.Name + ' : ' + LevelofEducationTBL.Name + ' : ' + EducationDegrees.DegreeName as Namee  from AreaofStudiesTbl inner join LevelofEducationTBL on LevelofEducationTBL.AreaofStudyID=AreaofStudiesTbl.Id inner join EducationDegrees on EducationDegrees.LevelofEducationTblID=LevelofEducationTBL.Id where AreaofStudiesTbl.Active='1' order by Namee asc";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            ddlfilterqualification.DataSource = dt;
            ddlfilterqualification.DataTextField = "Namee";
            ddlfilterqualification.DataValueField = "ID";
            ddlfilterqualification.DataBind();
        }
        ddlfilterqualification.Items.Insert(0, "All");
        ddlfilterqualification.Items[0].Value = "0";
    }

    protected void filterfillspecialsub()
    {
        ddlfiltersubject.Items.Clear();

        string str1 = "select SpecialisedSubjectTBL.Id,SpecialisedSubjectTBL.SubjectName as Name from SpecialisedSubjectTBL inner join AreaofStudiesTbl on AreaofStudiesTbl.ID=SpecialisedSubjectTBL.AreaofStudiesId where SpecialisedSubjectTBL.compid='" + Session["Comid"] + "' and SpecialisedSubjectTBL.AreaofStudiesId='" + ddlfilterqualification.SelectedValue + "' and SpecialisedSubjectTBL.Status='1' order by SpecialisedSubjectTBL.SubjectName asc";
        SqlCommand cmd1 = new SqlCommand(str1, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        adp1.Fill(dt1);

        if (dt1.Rows.Count > 0)
        {
            ddlfiltersubject.DataSource = dt1;
            ddlfiltersubject.DataTextField = "Name";
            ddlfiltersubject.DataValueField = "Id";
            ddlfiltersubject.DataBind();
        }
        ddlfiltersubject.Items.Insert(0, "All");
        ddlfiltersubject.Items[0].Value = "0";
    }

    protected void filterfilllastjobposition()
    {
        ddlfilterposition.Items.Clear();

        string str11 = "select VacancyPositionTitle,ID from VacancyPositionTitleMaster where Active='1' order by VacancyPositionTitle asc";
        SqlCommand cmd11 = new SqlCommand(str11, con);
        SqlDataAdapter adp11 = new SqlDataAdapter(cmd11);
        DataTable dt11 = new DataTable();
        adp11.Fill(dt11);

        if (dt11.Rows.Count > 0)
        {
            ddlfilterposition.DataSource = dt11;
            ddlfilterposition.DataTextField = "VacancyPositionTitle";
            ddlfilterposition.DataValueField = "ID";
            ddlfilterposition.DataBind();
        }
        ddlfilterposition.Items.Insert(0, "All");
        ddlfilterposition.Items[0].Value = "0";
    }
    protected void ddlfilterposition_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgriddata();
    }
    protected void ddlfilterqualification_SelectedIndexChanged(object sender, EventArgs e)
    {
        filterfillspecialsub();
        fillgriddata();
    }
    protected void ddlfiltersubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgriddata();
    }
    protected void txtfilterexpr_TextChanged(object sender, EventArgs e)
    {
        fillgriddata();
    }

    protected void chkoverhead_CheckedChanged(object sender, EventArgs e)
    {
        if (chkoverhead.Checked == true)
        {
            pnloverhead.Visible = true;
        }
        if (chkoverhead.Checked == false)
        {
            pnloverhead.Visible = false;
        }
    }

    protected void paybleper111()
    {
        string str1 = "select * from PeriodMaster12 where Id In ('2','3','4','7') order by Period_name ";
        SqlCommand cmd1 = new SqlCommand(str1, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataSet ds1 = new DataSet();
        adp1.Fill(ds1);
        DropDownList1.DataSource = ds1;
        DropDownList1.DataTextField = "Period_name";
        DropDownList1.DataValueField = "Id";
        DropDownList1.DataBind();

    }
    protected void RadioButtonList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList3.SelectedValue == "0")
        {
            panel2nd.Visible = true;
            panel3rd.Visible = false;
        }
        if (RadioButtonList3.SelectedValue == "1")
        {
            panel2nd.Visible = false;
            panel3rd.Visible = true;
        }
    }

    protected void fillemployeeownsetup()
    {
        string str = " select EmployeeSalaryMaster.*,EmployeeMaster.EmployeeMasterID,EmployeeMaster.EmployeeName+':'+DesignationMaster.DesignationName+':'+cast(EmployeeMaster.EmployeeMasterID as Nvarchar(50)) as EmployeeName, DesignationMaster.DesignationMasterId ,p2.Period_name as Period_name1,rm1.RemunerationName as RemunerationName,rm2.RemunerationName as RemunerationName1, " +
                   " '$'+ EmployeeSalaryMaster.Amount + ' Per ' + p2.Period_name as Amountpay, EmployeeSalaryMaster.Percentage +'% of '+ rm2.RemunerationName as Percentofsale, " +
                   " EmployeeSalaryMaster.SalesPercentage +'% of Sales of '  + (case  EmployeeSalaryMaster.PercentageOfSalesId when 0 then 'Employee himself' when 1 then 'Employee and his subbordinates' " +
                   " when 2 then 'entire business' else cast(EmployeeSalaryMaster.PercentageOfSalesId as nvarchar) end) as PercentageOfSalesemp, " +
                   " payperiodtype.Name as Paytypename,WareHouseMaster.Name from EmployeeSalaryMaster " +
                   " inner join EmployeeMaster  on EmployeeSalaryMaster.EmployeeId=EmployeeMaster.EmployeeMasterID  " +
                   " inner join DesignationMaster on EmployeeMaster.DesignationMasterId=DesignationMaster.DesignationMasterId " +
                   " inner join RemunerationMaster as rm1 on rm1.Id=EmployeeSalaryMaster.Remuneration_Id " +
                   " left outer join RemunerationMaster as rm2 on rm2.Id=EmployeeSalaryMaster.IsPercentRemunerationId " +
                   " left outer join PeriodMaster12 as p2 on p2.Id=EmployeeSalaryMaster.PayablePer_PeriodMasterId left outer join payperiodtype on payperiodtype.Id=EmployeeSalaryMaster.PayableOfSales inner join Warehousemaster on Warehousemaster.WareHouseId = EmployeeSalaryMaster.Whid " +
                   " where EmployeeSalaryMaster.compid='" + Session["Comid"].ToString() + "' " +
                   " and EmployeeSalaryMaster.Whid='" + ddlwarehouse.SelectedValue + "' " +
                   " and   EmployeeSalaryMaster.EmployeeId='" + ViewState["editid"].ToString() + "' " +
                   " order by Name, EmployeeName, RemunerationName";
        SqlCommand cmd1 = new SqlCommand(str, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable ds1 = new DataTable();
        adp1.Fill(ds1);

        GridView4.DataSource = ds1;
        GridView4.DataBind();
    }
    protected void Button8_Click(object sender, EventArgs e)
    {
        string te = "Remunerationbydesignation.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        fillemployeedesg();
    }
    protected void ImageButton6_Click(object sender, ImageClickEventArgs e)
    {
        string te = "Remunerationmaster.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void ImageButton7_Click(object sender, ImageClickEventArgs e)
    {
        Fillaccount();
    }
    protected void gridAccess_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblWh = (Label)(e.Row.FindControl("lblWh"));

            CheckBox ChkAess = (CheckBox)(e.Row.FindControl("ChkAess"));

            if (lblWh.Text == ddlwarehouse.SelectedValue)
            {
                ChkAess.Checked = true;
                ChkAess.Enabled = false;
            }
        }
    }
    protected void ImageButton9_Click(object sender, ImageClickEventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;

        ImageButton9.Visible = false;
        ImageButton22.Visible = true;

        ImageButton10.Visible = true;
        ImageButton11.Visible = true;
        ImageButton12.Visible = true;
        ImageButton13.Visible = true;
        ImageButton18.Visible = true;
        ImageButton19.Visible = true;

        ImageButton23.Visible = false;
        ImageButton24.Visible = false;
        ImageButton25.Visible = false;
        ImageButton26.Visible = false;
        ImageButton27.Visible = false;
        ImageButton28.Visible = false;

        ImageButton29.Visible = true;
        ImageButton30.Visible = false;
    }
    protected void ImageButton10_Click(object sender, ImageClickEventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;

        ImageButton10.Visible = false;
        ImageButton23.Visible = true;

        ImageButton29.Visible = true;
        ImageButton30.Visible = false;

        ImageButton9.Visible = true;
        ImageButton11.Visible = true;
        ImageButton12.Visible = true;
        ImageButton13.Visible = true;
        ImageButton18.Visible = true;
        ImageButton19.Visible = true;

        ImageButton22.Visible = false;
        ImageButton24.Visible = false;
        ImageButton25.Visible = false;
        ImageButton26.Visible = false;
        ImageButton27.Visible = false;
        ImageButton28.Visible = false;

    }
    protected void ImageButton11_Click(object sender, ImageClickEventArgs e)
    {
        MultiView1.ActiveViewIndex = 2;

        ImageButton11.Visible = false;
        ImageButton24.Visible = true;

        ImageButton9.Visible = true;
        ImageButton10.Visible = true;
        ImageButton12.Visible = true;
        ImageButton13.Visible = true;
        ImageButton18.Visible = true;
        ImageButton19.Visible = true;

        ImageButton22.Visible = false;
        ImageButton23.Visible = false;
        ImageButton25.Visible = false;
        ImageButton26.Visible = false;
        ImageButton27.Visible = false;
        ImageButton28.Visible = false;

        ImageButton29.Visible = true;
        ImageButton30.Visible = false;

        Label59.Text = ddlwarehouse.SelectedItem.Text;

        Label176.Text = ddldesignation.SelectedItem.Text;

        Label180.Text = Label176.Text;

        Label177.Text = Label59.Text;

        if (Radiogender.SelectedValue == "0")
        {
            Label178.Text = "He";
        }
        else
        {
            Label178.Text = "She";
        }

        if (gridAccess.Rows.Count > 1)
        {
            panelsdshhffg.Visible = true;
        }
    }
    protected void ImageButton12_Click(object sender, ImageClickEventArgs e)
    {
        MultiView1.ActiveViewIndex = 3;

        if (Convert.ToString(ViewState["passd"]) != "")
        {
            if (ViewState["passd"].ToString() == "1")
            {
                tbPassword.Attributes.Add("Value", ViewState["newpass"].ToString());
                tbConPassword.Attributes.Add("Value", ViewState["newpass"].ToString());
            }
        }

        ImageButton12.Visible = false;
        ImageButton25.Visible = true;

        ImageButton9.Visible = true;
        ImageButton10.Visible = true;
        ImageButton11.Visible = true;
        ImageButton13.Visible = true;
        ImageButton18.Visible = true;
        ImageButton19.Visible = true;

        ImageButton22.Visible = false;
        ImageButton23.Visible = false;
        ImageButton24.Visible = false;
        ImageButton26.Visible = false;
        ImageButton27.Visible = false;
        ImageButton28.Visible = false;

        ImageButton29.Visible = true;
        ImageButton30.Visible = false;
    }
    protected void ImageButton13_Click(object sender, ImageClickEventArgs e)
    {
        MultiView1.ActiveViewIndex = 4;

        ImageButton13.Visible = false;
        ImageButton26.Visible = true;

        ImageButton9.Visible = true;
        ImageButton10.Visible = true;
        ImageButton11.Visible = true;
        ImageButton12.Visible = true;
        ImageButton18.Visible = true;
        ImageButton19.Visible = true;

        ImageButton22.Visible = false;
        ImageButton23.Visible = false;
        ImageButton24.Visible = false;
        ImageButton25.Visible = false;
        ImageButton27.Visible = false;
        ImageButton28.Visible = false;

        ImageButton29.Visible = true;
        ImageButton30.Visible = false;

        Label59.Text = ddlwarehouse.SelectedItem.Text;

        Label176.Text = ddldesignation.SelectedItem.Text;

        Label180.Text = Label176.Text;

        Label177.Text = Label59.Text;
    }
    protected void ImageButton14_Click(object sender, ImageClickEventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;

        ImageButton10.Visible = false;
        ImageButton23.Visible = true;

        ImageButton9.Visible = true;
        ImageButton22.Visible = false;
    }
    protected void ImageButton15_Click(object sender, ImageClickEventArgs e)
    {
        MultiView1.ActiveViewIndex = 2;

        ImageButton10.Visible = true;
        ImageButton23.Visible = false;

        ImageButton11.Visible = false;
        ImageButton24.Visible = true;

        if (Convert.ToString(ViewState["passd"]) != "")
        {
            if (ViewState["passd"].ToString() == "1")
            {
                tbPassword.Attributes.Add("Value", ViewState["newpass"].ToString());
                tbConPassword.Attributes.Add("Value", ViewState["newpass"].ToString());
            }
        }

        Label59.Text = ddlwarehouse.SelectedItem.Text;

        Label176.Text = ddldesignation.SelectedItem.Text;

        Label180.Text = Label176.Text;

        Label177.Text = Label59.Text;

        if (Radiogender.SelectedValue == "0")
        {
            Label178.Text = "He";
        }
        else
        {
            Label178.Text = "She";
        }

        if (gridAccess.Rows.Count > 1)
        {
            panelsdshhffg.Visible = true;
        }
    }
    protected void ImageButton16_Click(object sender, ImageClickEventArgs e)
    {
        MultiView1.ActiveViewIndex = 3;

        if (RadioButtonList2.SelectedValue == "0")
        {
            ViewState["tbPassword"] = tbPassword.Text;            
        }

        ImageButton11.Visible = true;
        ImageButton24.Visible = false;

        ImageButton12.Visible = false;
        ImageButton25.Visible = true;
    }
    protected void ImageButton17_Click(object sender, ImageClickEventArgs e)
    {
        MultiView1.ActiveViewIndex = 4;

        ImageButton12.Visible = true;
        ImageButton25.Visible = false;

        ImageButton13.Visible = false;
        ImageButton26.Visible = true;

        if (tbPassword.Text != "")
        {
            ViewState["tbPassword"] = tbPassword.Text;
        }

        if (tbConPassword.Text != "")
        {
            ViewState["tbConPassword"] = tbPassword.Text;
        }
    }

    protected void ImageButton18_Click(object sender, ImageClickEventArgs e)
    {
        MultiView1.ActiveViewIndex = 5;

        ImageButton18.Visible = false;
        ImageButton27.Visible = true;

        ImageButton9.Visible = true;
        ImageButton10.Visible = true;
        ImageButton11.Visible = true;
        ImageButton12.Visible = true;
        ImageButton13.Visible = true;
        ImageButton19.Visible = true;

        ImageButton22.Visible = false;
        ImageButton23.Visible = false;
        ImageButton24.Visible = false;
        ImageButton25.Visible = false;
        ImageButton26.Visible = false;
        ImageButton28.Visible = false;

        ImageButton29.Visible = true;
        ImageButton30.Visible = false;
    }
    protected void ImageButton19_Click(object sender, ImageClickEventArgs e)
    {
        MultiView1.ActiveViewIndex = 6;

        ImageButton19.Visible = false;
        ImageButton28.Visible = true;

        ImageButton9.Visible = true;
        ImageButton10.Visible = true;
        ImageButton11.Visible = true;
        ImageButton12.Visible = true;
        ImageButton13.Visible = true;
        ImageButton18.Visible = true;
        ImageButton29.Visible = true;
        ImageButton30.Visible = false;

        ImageButton22.Visible = false;
        ImageButton23.Visible = false;
        ImageButton24.Visible = false;
        ImageButton25.Visible = false;
        ImageButton26.Visible = false;
        ImageButton27.Visible = false;
    }
    protected void ImageButton21_Click(object sender, ImageClickEventArgs e)
    {
        MultiView1.ActiveViewIndex = 5;

        ImageButton18.Visible = false;
        ImageButton27.Visible = true;

        ImageButton13.Visible = true;
        ImageButton26.Visible = false;
    }
    protected void ImageButton20_Click(object sender, ImageClickEventArgs e)
    {
        MultiView1.ActiveViewIndex = 6;

        ImageButton18.Visible = true;
        ImageButton27.Visible = false;

        ImageButton19.Visible = false;
        ImageButton28.Visible = true;
    }
    protected void ImageButton22_Click(object sender, ImageClickEventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
    }
    protected void ImageButton23_Click(object sender, ImageClickEventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
    }
    protected void ImageButton24_Click(object sender, ImageClickEventArgs e)
    {
        MultiView1.ActiveViewIndex = 2;
    }
    protected void ImageButton25_Click(object sender, ImageClickEventArgs e)
    {
        MultiView1.ActiveViewIndex = 3;
    }
    protected void ImageButton26_Click(object sender, ImageClickEventArgs e)
    {
        MultiView1.ActiveViewIndex = 4;
    }
    protected void ImageButton27_Click(object sender, ImageClickEventArgs e)
    {
        MultiView1.ActiveViewIndex = 5;
    }
    protected void ImageButton28_Click(object sender, ImageClickEventArgs e)
    {
        MultiView1.ActiveViewIndex = 6;
    }
    protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox2.Checked == true)
        {
            Panel9.Visible = true;
        }
        else
        {
            Panel9.Visible = false;
        }
    }
    protected void CheckBox3_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox3.Checked == true)
        {
            Panel8.Visible = true;
        }
        else
        {
            Panel8.Visible = false;
        }
    }
    protected void CheckBox4_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox4.Checked == true)
        {
            panelmainpanel.Visible = true;
        }
        else
        {
            panelmainpanel.Visible = false;
        }
    }
    protected void CheckBox5_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox5.Checked == true)
        {
            panelgetready.Visible = true;
        }
        else
        {
            panelgetready.Visible = false;
        }
    }
    protected void Button9_Click(object sender, EventArgs e)
    {
        FillGrid();
    }

    public DataTable CreateDatatable()
    {
        DataTable dt = new DataTable();
        DataColumn Dcom = new DataColumn();
        Dcom.DataType = System.Type.GetType("System.String");
        Dcom.ColumnName = "documentname";
        Dcom.AllowDBNull = true;
        Dcom.Unique = false;
        Dcom.ReadOnly = false;

        DataColumn Dcom1 = new DataColumn();
        Dcom1.DataType = System.Type.GetType("System.String");
        Dcom1.ColumnName = "documenttype";
        Dcom1.AllowDBNull = true;
        Dcom1.Unique = false;
        Dcom1.ReadOnly = false;

        DataColumn Dcom2 = new DataColumn();
        Dcom2.DataType = System.Type.GetType("System.String");
        Dcom2.ColumnName = "DocumentTitle";
        Dcom2.AllowDBNull = true;
        Dcom2.Unique = false;
        Dcom2.ReadOnly = false;
        DataColumn Dcom3 = new DataColumn();
        Dcom3.DataType = System.Type.GetType("System.String");
        Dcom3.ColumnName = "status";
        Dcom3.AllowDBNull = true;
        Dcom3.Unique = false;
        Dcom3.ReadOnly = false;

        DataColumn Dcom4 = new DataColumn();
        Dcom4.DataType = System.Type.GetType("System.String");
        Dcom4.ColumnName = "Businessname";
        Dcom4.AllowDBNull = true;
        Dcom4.Unique = false;
        Dcom4.ReadOnly = false;


        DataColumn Dcom5 = new DataColumn();
        Dcom5.DataType = System.Type.GetType("System.String");
        Dcom5.ColumnName = "PartyId";
        Dcom5.AllowDBNull = true;
        Dcom5.Unique = false;
        Dcom5.ReadOnly = false;


        DataColumn Dcom6 = new DataColumn();
        Dcom6.DataType = System.Type.GetType("System.String");
        Dcom6.ColumnName = "DocType";
        Dcom6.AllowDBNull = true;
        Dcom6.Unique = false;
        Dcom6.ReadOnly = false;

        DataColumn Dcom8 = new DataColumn();
        Dcom8.DataType = System.Type.GetType("System.String");
        Dcom8.ColumnName = "docdate";
        Dcom8.AllowDBNull = true;
        Dcom8.Unique = false;
        Dcom8.ReadOnly = false;


        DataColumn Dcom9 = new DataColumn();
        Dcom9.DataType = System.Type.GetType("System.String");
        Dcom9.ColumnName = "docrefno";
        Dcom9.AllowDBNull = true;
        Dcom9.Unique = false;
        Dcom9.ReadOnly = false;

        DataColumn Dcom10 = new DataColumn();
        Dcom10.DataType = System.Type.GetType("System.String");
        Dcom10.ColumnName = "docamt";
        Dcom10.AllowDBNull = true;
        Dcom10.Unique = false;
        Dcom10.ReadOnly = false;


        DataColumn Dcom4a = new DataColumn();
        Dcom4a.DataType = System.Type.GetType("System.String");
        Dcom4a.ColumnName = "Docty";
        Dcom4a.AllowDBNull = true;
        Dcom4a.Unique = false;
        Dcom4a.ReadOnly = false;

        DataColumn Dcom5a = new DataColumn();
        Dcom5a.DataType = System.Type.GetType("System.String");
        Dcom5a.ColumnName = "DoctyId";
        Dcom5a.AllowDBNull = true;
        Dcom5a.Unique = false;
        Dcom5a.ReadOnly = false;
        DataColumn Dcom6a = new DataColumn();
        Dcom6a.DataType = System.Type.GetType("System.String");
        Dcom6a.ColumnName = "PRN";
        Dcom6a.AllowDBNull = true;
        Dcom6a.Unique = false;
        Dcom6a.ReadOnly = false;

        dt.Columns.Add(Dcom);
        dt.Columns.Add(Dcom1);
        dt.Columns.Add(Dcom2);
        dt.Columns.Add(Dcom3);
        dt.Columns.Add(Dcom4);
        dt.Columns.Add(Dcom5);
        dt.Columns.Add(Dcom6);

        dt.Columns.Add(Dcom8);
        dt.Columns.Add(Dcom9);
        dt.Columns.Add(Dcom10);
        dt.Columns.Add(Dcom4a);
        dt.Columns.Add(Dcom5a);
        dt.Columns.Add(Dcom6a);
        return dt;
    }

    protected void FillGrid()
    {
        DataTable dt = new DataTable();
        if (Convert.ToString(ViewState["data"]) == "")
        {
            dt = CreateDatatable();
        }
        else
        {
            dt = (DataTable)ViewState["data"];
        }

        if (FileUpload2.HasFile == true)
        {
            DataRow Drow = dt.NewRow();
            Drow["documentname"] = FileUpload2.FileName;
            Drow["documenttype"] = "";
            Drow["status"] = "Not Uploaded";
            Drow["Businessname"] = ddlwarehouse.SelectedItem.Text;
            Drow["DocType"] = "Employee-Employee-Resume";
            Drow["DocumentTitle"] = txtdoctitle.Text;
            Drow["PartyId"] = '1';
            Drow["docdate"] = TxtDocDate.Text;
            Drow["docrefno"] = 0;
            Drow["docamt"] = 0;
            Drow["Docty"] = ddldt.SelectedItem.Text;
            Drow["DoctyId"] = ddldt.SelectedValue;
            Drow["PRN"] = 0;
            dt.Rows.Add(Drow);
        }

        ViewState["data"] = dt;

        GridView7.DataSource = dt;
        GridView7.DataBind();

        if (FileUpload2.HasFile == true)
        {
            if (Directory.Exists(Server.MapPath("~\\Account\\" + ViewState["comid"] + "\\TempDoc")) == false)
            {
                Directory.CreateDirectory(Server.MapPath("~\\Account\\" + ViewState["comid"] + "\\TempDoc"));
            }
            FileUpload2.PostedFile.SaveAs(Server.MapPath("~\\Account\\" + ViewState["comid"] + "\\TempDoc\\") + FileUpload2.FileName);
        }
    }

    protected void flaganddoc()
    {

        DataTable dts1 = select("select Id,name from DocumentTypenm where  active='1' Order by name");
        ddldt.DataSource = dts1;
        ddldt.DataTextField = "name";
        ddldt.DataValueField = "Id";
        ddldt.DataBind();
        ddldt.SelectedIndex = ddldt.Items.IndexOf(ddldt.Items.FindByText("Resume"));
        //ddldt.Items.Insert(0, "Select");
        //ddldt.Items[0].Value = "0";
    }

    protected void UploadDocumets()
    {
        int i1 = 0;

        if (GridView7.Rows.Count > 0)
        {
            do
            {
                string filename = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "_" + GridView7.Rows[i1].Cells[4].Text.Replace(" ", "_");

                string path1 = Server.MapPath("~\\Account\\" + Session["comid"] + "\\TempDoc\\" + GridView7.Rows[i1].Cells[4].Text);
                string path2 = Server.MapPath("~\\Account\\" + Session["comid"] + "\\UploadedDocuments\\" + filename.ToString());


                if (System.IO.File.Exists(path2))
                {
                }
                else
                {
                    File.Copy(path1, path2);
                }
                string filexten = Path.GetExtension(path2);

                Label lbldocdate = (Label)GridView7.Rows[i1].Cells[6].FindControl("lbldocdate");

                Label lbldocrefno = (Label)GridView7.Rows[i1].Cells[7].FindControl("lbldocrefno");
                Label lbldocamt = (Label)GridView7.Rows[i1].Cells[8].FindControl("lbldocamt");
                Label lblpid = (Label)GridView7.Rows[i1].Cells[1].FindControl("lblpid");
                Label lbldoctid = (Label)GridView7.Rows[i1].FindControl("lbldoctid");
                Label lblprn = (Label)GridView7.Rows[i1].FindControl("lblprn");

                //Label Label27 = (Label)GridView7.Rows[i1].FindControl("Label2700");

                Label lbldocumenttitle = (Label)GridView7.Rows[i1].FindControl("lbldocumenttitle");

                if (lbldocamt.Text == "")
                {
                    lbldocamt.Text = "0";
                }

                SqlCommand cmdinb = new SqlCommand("INSERT INTO DocumentMaster(DocumentTypeId,DocumentUploadTypeId,DocumentUploadDate,DocumentName,DocumentTitle,Description,PartyId,DocumentRefNo,DocumentAmount,EmployeeId,DocumentDate,FileExtensionType,CID,DocumentTypenmId,PartyDocrefno) VALUES ('" + ViewState["upload2"].ToString() + "',2,'" + Convert.ToDateTime(System.DateTime.Now.ToString()) + "','" + filename.ToString() + "','" + lbldocumenttitle.Text + "','', '" + Convert.ToInt32(lblpid.Text) + "','" + lbldocrefno.Text + "','" + Convert.ToDecimal(lbldocamt.Text) + "','" + ViewState["EmerEMID"].ToString() + "','" + Convert.ToDateTime(lbldocdate.Text) + "','" + filexten + "','" + Session["comid"] + "','" + lbldoctid.Text + "','" + lblprn.Text + "')", con);

                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdinb.ExecuteNonQuery();
                con.Close();

                SqlDataAdapter damax = new SqlDataAdapter("select max(DocumentId) as DocumentId from DocumentMaster", con);
                DataTable dtmax = new DataTable();
                damax.Fill(dtmax);

                Int32 rst = 0;

                if (dtmax.Rows.Count > 0)
                {
                    rst = Convert.ToInt32(dtmax.Rows[0]["DocumentId"]);
                }

                if (rst > 0)
                {

                    bool dcaprv = true;

                    SqlCommand cmdinb1 = new SqlCommand("INSERT INTO DocumentProcessing (DocumentId,EmployeeId,DocAllocateDate,Approve,ApproveDate,CID) VALUES ('" + rst + "','" + ViewState["EmerEMID"].ToString() + "','" + System.DateTime.Now.ToShortDateString() + "','" + dcaprv + "','" + System.DateTime.Now.ToShortDateString() + "','" + Session["comid"] + "')", con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdinb1.ExecuteNonQuery();
                    con.Close();

                    SqlCommand cmdinb12 = new SqlCommand("INSERT INTO DocumentLog (DocumentId, EmployeeId, Date, ViewLog, DeleteLog,SaveLog, EditLog, EmailLog, FaxLog, PrintLog, MessageLog,CID) VALUES ('" + rst + "', '" + ViewState["EmerEMID"].ToString() + "','" + Convert.ToDateTime(System.DateTime.Now) + "','" + false + "','" + false + "','" + true + "','" + false + "','" + false + "','" + false + "','" + false + "','" + false + "','" + Session["comid"] + "')", con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdinb12.ExecuteNonQuery();
                    con.Close();

                    string str12 = "Insert into OfficeManagerDocuments(DocumentId,StoreId,EmployeeID)" +
                                 "values('" + rst + "','" + ddlwarehouse.SelectedValue + "','" + ViewState["EmerEMID"] + "')";

                    SqlCommand cmd12 = new SqlCommand(str12, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd12.ExecuteNonQuery();
                    con.Close();

                }

                string Location = Server.MapPath(@"~/Account/" + Session["comid"] + "/UploadedDocuments/");
                string Location2 = Server.MapPath(@"~/Account/" + Session["comid"] + "/UploadedDocumentsTemp/");
                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Location);
                if (filexten == ".pdf")
                {
                    foreach (System.IO.FileInfo f in dir.GetFiles(filename))
                    {

                        string Location1 = Server.MapPath(@"~/Account/" + Session["comid"] + "/DocumentImage/");

                        string filepath = Server.MapPath("~//Account//pdftoimage.exe");
                        System.Diagnostics.ProcessStartInfo pti = new System.Diagnostics.ProcessStartInfo(filepath);

                        pti.Arguments = filepath + " -i UploadedDocuments//" + f.Name + " " + "-o" + " " + "DocumentImage//";
                        filepath += " " + "-r" + " " + "AGPVCWBRYBCDEPFD";


                        pti.WorkingDirectory = Server.MapPath("~//Account//" + ViewState["comid"] + "//");


                        pti.UseShellExecute = false;
                        pti.RedirectStandardOutput = true;
                        pti.RedirectStandardInput = true;
                        pti.RedirectStandardError = true;

                        System.Diagnostics.Process ps = Process.Start(pti);

                        if (System.IO.File.Exists(Location2 + f.Name))
                        {
                        }
                        else
                        {
                            System.IO.File.Copy(Location + f.Name, Location2 + f.Name);
                        }
                        System.IO.File.SetAttributes(Location2 + f.Name, System.IO.FileAttributes.Hidden);

                    }


                    int ii = 0;
                    string filepath1 = Server.MapPath("~//Account//" + Session["comid"] + "//UploadedDocuments//" + filename);

                    using (StreamReader st = new StreamReader(File.OpenRead(filepath1)))
                    {
                        Regex regex = new Regex(@"/Type\s*/Page[^s]");
                        MatchCollection match = regex.Matches(st.ReadToEnd());
                        ii = match.Count;
                    }

                    int length = filename.Length;
                    string docnameIn = filename.Substring(0, length - 4);


                    for (int kk = 1; kk <= ii; kk++)
                    {
                        string scpf = docnameIn;
                        if (kk >= 1 && kk < 10)
                        {
                            scpf = scpf + "0000" + kk + ".jpg";
                        }
                        else if (kk >= 10 && kk < 100)
                        {
                            scpf = scpf + "000" + kk + ".jpg";
                        }
                        else if (kk >= 100)
                        {
                            scpf = scpf + "00" + kk + ".jpg";
                        }

                        clsEmployee.InserDocumentImageMaster(rst, scpf);

                    }
                }

                lblmsg.Visible = true;
                //lblmsg.Text = "Message : All PDFs Are Converted Successfully!!!";

                i1 = i1 + 1;

            } while (i1 <= GridView7.Rows.Count - 1);
        }

        ViewState["data"] = null;

        //lblmsg.Text = "Documents uploaded successfully.";
    }
    protected void ImageButton29_Click(object sender, ImageClickEventArgs e)
    {
        MultiView1.ActiveViewIndex = 7;

        ImageButton29.Visible = false;
        ImageButton30.Visible = true;

        ImageButton9.Visible = true;
        ImageButton10.Visible = true;
        ImageButton11.Visible = true;
        ImageButton12.Visible = true;
        ImageButton13.Visible = true;
        ImageButton18.Visible = true;
        ImageButton19.Visible = true;

        ImageButton22.Visible = false;
        ImageButton23.Visible = false;
        ImageButton24.Visible = false;
        ImageButton25.Visible = false;
        ImageButton26.Visible = false;
        ImageButton27.Visible = false;
        ImageButton28.Visible = false;

    }
    protected void ImageButton30_Click(object sender, ImageClickEventArgs e)
    {
        MultiView1.ActiveViewIndex = 7;
    }

    protected void GridView7_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            GridView7.SelectedIndex = Convert.ToInt32(e.CommandArgument);

            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["data"];
            dt.Rows[Convert.ToInt32(GridView7.SelectedIndex.ToString())].Delete();
            dt.AcceptChanges();
            GridView7.DataSource = dt;
            GridView7.DataBind();
            ViewState["data"] = dt;

            lblmsg.Text = "Record deleted successfully.";
        }
    }
    protected void ImageButton31_Click(object sender, ImageClickEventArgs e)
    {
        MultiView1.ActiveViewIndex = 7;

        ImageButton19.Visible = true;
        ImageButton28.Visible = false;

        ImageButton29.Visible = false;
        ImageButton30.Visible = true;
    }
    protected void CheckBox6_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox6.Checked == true)
        {
            fillcandidate();
            panelexistingcandidate.Visible = true;
        }
        else
        {
            panelexistingcandidate.Visible = false;
        }
    }


    protected void fillcandidate()
    {
        string str1 = "select CandidateMaster.CandidateId,CandidateMaster.LastName + ' ' + CandidateMaster.FirstName + ' ' + CandidateMaster.MiddleName as name from CandidateMaster where compid='" + Session["Comid"].ToString() + "' and Active='1' order by name";
        SqlCommand cmd1 = new SqlCommand(str1, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataSet ds1 = new DataSet();
        adp1.Fill(ds1);

        if (ds1.Tables[0].Rows.Count > 0)
        {
            ddlcandidate.DataSource = ds1;
            ddlcandidate.DataTextField = "name";
            ddlcandidate.DataValueField = "CandidateId";
            ddlcandidate.DataBind();
        }

        ddlcandidate.Items.Insert(0, "-Select-");
        ddlcandidate.Items[0].Value = "0";
    }

    protected void ddlcandidate_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcandidate.SelectedIndex > 0)
        {
            DataTable dtfind = select("select partyid,LastName,FirstName,CandidatePhotoPath,MiddleName,DOB from CandidateMaster where CandidateId='" + ddlcandidate.SelectedValue + "'");

            DataTable dtemppp = select("select employeemasterid from employeemaster where partyid='" + dtfind.Rows[0]["partyid"].ToString() + "'");

            if (dtemppp.Rows.Count > 0)
            {
                ViewState["editid"] = dtemppp.Rows[0]["employeemasterid"].ToString();

                SqlDataAdapter adptemp = new SqlDataAdapter("Select EmployeeMaster.*,Party_master.PartyTypeCategoryNo,EmployeePayrollMaster.EmployeeNo,Party_master.Zipcode,Party_master.PartyTypeId,EmployeeBarcodeMaster.Barcode,EmployeeBarcodeMaster.Employeecode,EmployeeBarcodeMaster.Biometricno,EmployeeBarcodeMaster.blutoothid,EmployeePayrollMaster.PaymentMethodId,EmployeePayrollMaster.PaymentReceivedNameOf,EmployeePayrollMaster.PaypalId,EmployeePayrollMaster.PaymentEmailId,EmployeePayrollMaster.DirectDepositBankName,EmployeePayrollMaster.DirectDepositBankCode,EmployeePayrollMaster.DirectDepositBankBranchAddress,EmployeePayrollMaster.DirectDepositBankBranchcity,EmployeePayrollMaster.DirectDepositBankBranchstate,EmployeePayrollMaster.DirectDepositBankBranchcountry,EmployeePayrollMaster.DirectDepositBankBranchzipcode,EmployeePayrollMaster.DirectDepositBankIFCNumber,EmployeePayrollMaster.DirectDepositBankSwiftNumber,EmployeePayrollMaster.DirectDepositBankEmployeeEmail,EmployeePayrollMaster.DirectDepositBankBranchName,EmployeePayrollMaster.DirectDepositBankBranchNumber,EmployeePayrollMaster.DirectDepositTransitNumber,EmployeePayrollMaster.DirectDepositAccountHolderName,EmployeePayrollMaster.DirectDepositBankAccountType,EmployeePayrollMaster.DirectDepositBankAccountNumber,EmployeePayrollMaster.PayPeriodMasterId,EmployeePayrollMaster.LastName,EmployeePayrollMaster.FirstName,EmployeePayrollMaster.Intials,EmployeePayrollMaster.EmployeeNo,EmployeePayrollMaster.DateOfBirth,EmployeePayrollMaster.SocialSecurityNo,EmployeePayrollMaster.EmployeePaidAsPerDesignation  from EmployeeMaster left join EmployeePayrollMaster on EmployeeMaster.EmployeeMasterID = EmployeePayrollMaster.EmpId  left join EmployeeBarcodeMaster on  EmployeePayrollMaster.EmpId = EmployeeBarcodeMaster.Employee_Id inner join Party_master on EmployeeMaster.PartyID = Party_master.PartyID where EmployeeMaster.EmployeeMasterID = '" + dtemppp.Rows[0]["employeemasterid"].ToString() + "'", con);
                DataTable dt = new DataTable();
                adptemp.Fill(dt);

                btnSubmit.Visible = false;
                imgbtnupdate.Visible = true;

                if (Convert.ToString(dt.Rows[0]["sex"]) != "")
                {
                    Radiogender.SelectedValue = dt.Rows[0]["sex"].ToString();
                }

                filleducationquali();
                ddleduquali.SelectedIndex = ddleduquali.Items.IndexOf(ddleduquali.Items.FindByValue(dt.Rows[0]["EducationqualificationID"].ToString()));

                fillspecialsub();
                ddlspecialsub.SelectedIndex = ddlspecialsub.Items.IndexOf(ddlspecialsub.Items.FindByValue(dt.Rows[0]["SpecialSubjectID"].ToString()));

                txtyearexpr.Text = dt.Rows[0]["yearofexperience"].ToString();

                filllastjobposition();
                ddljobposition.SelectedIndex = ddljobposition.Items.IndexOf(ddljobposition.Items.FindByValue(dt.Rows[0]["Jobpositionid"].ToString()));

                txtlastname.Text = dtfind.Rows[0]["LastName"].ToString();
                txtfirstname.Text = dtfind.Rows[0]["FirstName"].ToString();
                txtintialis.Text = dtfind.Rows[0]["MiddleName"].ToString();

                DateTime t1 = Convert.ToDateTime(dtfind.Rows[0]["DOB"].ToString());
                txtdateofbirth.Text = t1.ToShortDateString();

                string logoname = dtfind.Rows[0]["CandidatePhotoPath"].ToString();

                imgLogo.ImageUrl = "~/ShoppingCart/images/" + logoname;

                //TextBox1.Text = dt.Rows[0]["EmployeeNo"].ToString();

                //if (dt.Rows[0]["DateOfBirth"].ToString() != "")
                //{
                //    DateTime t1 = Convert.ToDateTime(dt.Rows[0]["DateOfBirth"].ToString());
                //    txtdateofbirth.Text = t1.ToShortDateString();
                //}
                if (dt.Rows[0]["SocialSecurityNo"].ToString() != "")
                {
                    txtsecurityno.Text = dt.Rows[0]["SocialSecurityNo"].ToString();
                }
                if (dt.Rows[0]["PayPeriodMasterId"].ToString() != "")
                {
                    ddlPaymentCycle.SelectedValue = dt.Rows[0]["PayPeriodMasterId"].ToString();
                }

                txtbarcode.Text = dt.Rows[0]["Barcode"].ToString();
                txtsecuritycode.Text = dt.Rows[0]["Employeecode"].ToString();
                txtbiometricid.Text = dt.Rows[0]["Biometricno"].ToString();
                txtbluetoothid.Text = dt.Rows[0]["blutoothid"].ToString();
                TextBox17.Text = dt.Rows[0]["Amount"].ToString();
                DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByValue(dt.Rows[0]["Payper"].ToString()));

                ddlwarehouse.SelectedIndex = ddlwarehouse.Items.IndexOf(ddlwarehouse.Items.FindByValue(dt.Rows[0]["Whid"].ToString()));

                SqlDataAdapter dasalaryy = new SqlDataAdapter("select * from employeesalarymaster where EmployeeId='" + dtemppp.Rows[0]["employeemasterid"].ToString() + "'", con);
                DataTable dtsalaryy = new DataTable();
                dasalaryy.Fill(dtsalaryy);

                if (dtsalaryy.Rows.Count > 0)
                {
                    Fillaccount();
                    ddlremuneration.SelectedIndex = ddlremuneration.Items.IndexOf(ddlremuneration.Items.FindByValue(dtsalaryy.Rows[0]["Remuneration_Id"].ToString()));
                    txtamount.Text = dtsalaryy.Rows[0]["Amount"].ToString();
                    ddlpaybleper.SelectedIndex = ddlpaybleper.Items.IndexOf(ddlpaybleper.Items.FindByValue(dtsalaryy.Rows[0]["PayablePer_PeriodMasterId"].ToString()));
                }

                string str = "Select Id From PartyMasterCategory where PartyMasterCategoryNo='" + dt.Rows[0]["PartyTypeCategoryNo"].ToString() + "' order by Name ";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable datat = new DataTable();
                adp.Fill(datat);
                if (datat.Rows.Count > 0)
                {
                    ddlpartycate.SelectedIndex = ddlpartycate.Items.IndexOf(ddlpartycate.Items.FindByValue(datat.Rows[0]["Id"].ToString()));
                }

                ddlPartyType.SelectedIndex = ddlPartyType.Items.IndexOf(ddlPartyType.Items.FindByValue(dt.Rows[0]["PartyTypeId"].ToString()));
                object ob = new object();
                EventArgs evt = new EventArgs();
                ddlPartyType_SelectedIndexChanged(ob, evt);

                ddlCountry.SelectedIndex = ddlCountry.Items.IndexOf(ddlCountry.Items.FindByValue(dt.Rows[0]["CountryId"].ToString()));
                object obs = new object();
                EventArgs evts = new EventArgs();
                ddlCountry_SelectedIndexChanged(obs, evts);
                lblcountry.Text = ddlCountry.SelectedItem.Text;

                ddlState.SelectedIndex = ddlState.Items.IndexOf(ddlState.Items.FindByValue(dt.Rows[0]["StateId"].ToString()));
                object obc = new object();
                EventArgs evtc = new EventArgs();
                ddlState_SelectedIndexChanged(obc, evtc);
                lblstate.Text = ddlState.SelectedItem.Text;

                ddlCity.SelectedIndex = ddlCity.Items.IndexOf(ddlCity.Items.FindByValue(dt.Rows[0]["City"].ToString()));
                object o = new object();
                EventArgs ev = new EventArgs();
                ddlCity_SelectedIndexChanged(o, ev);
                lblcity.Text = ddlCity.SelectedItem.Text;

                txtworkemail.Text = dt.Rows[0]["WorkEmail"].ToString();
                workphone.Text = dt.Rows[0]["WorkPhone"].ToString();
                workext.Text = dt.Rows[0]["Workext"].ToString();

                ddlActive.SelectedIndex = ddlActive.Items.IndexOf(ddlActive.Items.FindByValue(dt.Rows[0]["StatusMasterId"].ToString()));

                tbEmail.Text = dt.Rows[0]["Email"].ToString();
                lblemail.Text = dt.Rows[0]["Email"].ToString();

                tbPhone.Text = dt.Rows[0]["ContactNo"].ToString();
                lblphone.Text = dt.Rows[0]["ContactNo"].ToString();

                tbAddress.Text = dt.Rows[0]["Address"].ToString();
                lblstreetadd.Text = dt.Rows[0]["Address"].ToString();

                ViewState["partyid"] = dt.Rows[0]["PartyID"].ToString();
                DataTable dtseluser = new DataTable();

                dtseluser = (DataTable)select(" Select Login_master.password, User_master.* from User_master inner join Login_master on  Login_master.UserID=User_master.UserID where PartyID='" + dtfind.Rows[0]["partyid"].ToString() + "'");

                if (dtseluser.Rows.Count > 0)
                {
                    //logoname = dtseluser.Rows[0]["Photo"].ToString();

                    //imgLogo.ImageUrl = "~/ShoppingCart/images/" + logoname;

                    tbUserName.Text = dtseluser.Rows[0]["Username"].ToString();

                    string strqa = ClsEncDesc.Decrypted(dtseluser.Rows[0]["password"].ToString());

                    ViewState["newpass"] = strqa;

                    tbPassword.Attributes.Add("Value", strqa);
                    tbConPassword.Attributes.Add("Value", strqa);
                    tbPassword.Text = strqa;
                    tbConPassword.Text = strqa;


                    ViewState["passd"] = "1";

                    filldept();
                    ddldept.SelectedIndex = ddldept.Items.IndexOf(ddldept.Items.FindByValue(dt.Rows[0]["DeptID"].ToString()));

                    object obu = new object();
                    EventArgs evtu = new EventArgs();
                    ddldept_SelectedIndexChanged(obu, evtu);

                    ddldesignation.SelectedIndex = ddldesignation.Items.IndexOf(ddldesignation.Items.FindByValue(dt.Rows[0]["DesignationMasterId"].ToString()));


                    if (dt.Rows[0]["EmployeePaidAsPerDesignation"].ToString() != "")
                    {
                        RadioButtonList1.SelectedValue = dt.Rows[0]["EmployeePaidAsPerDesignation"].ToString();
                        RadioButtonList1_SelectedIndexChanged(sender, e);
                    }
                    if (dt.Rows[0]["PaymentMethodId"].ToString() != "")
                    {
                        ddlPaymentMethod.SelectedValue = dt.Rows[0]["PaymentMethodId"].ToString();
                    }
                    if (ddlPaymentMethod.SelectedIndex > 0)
                    {
                        if (ddlPaymentMethod.SelectedItem.Text == "Demand Draft" || ddlPaymentMethod.SelectedItem.Text == "Cheque")
                        {
                            ddpnl.Visible = false;
                            pnlreceivepayment.Visible = true;
                            txtPaymentReceivedNameOf.Text = dt.Rows[0]["PaymentReceivedNameOf"].ToString();
                        }
                        else
                        {
                            ddpnl.Visible = false;
                            pnlreceivepayment.Visible = false;
                        }
                        if (ddlPaymentMethod.SelectedItem.Text == "Paypal")
                        {
                            ddpnl.Visible = false;
                            pnlpaypalid.Visible = true;
                            txtPaypalId.Text = dt.Rows[0]["PaypalId"].ToString();
                        }
                        else
                        {
                            ddpnl.Visible = false;
                            pnlpaypalid.Visible = false;
                        }
                        if (ddlPaymentMethod.SelectedItem.Text == "By Email")
                        {
                            ddpnl.Visible = false;
                            pnlpaymentemail.Visible = true;
                            txtPaymentEmailId.Text = dt.Rows[0]["PaymentEmailId"].ToString();
                        }
                        else
                        {
                            ddpnl.Visible = false;
                            pnlpaymentemail.Visible = false;

                        }

                        if (ddlPaymentMethod.SelectedItem.Text == "Direct Deposit")
                        {
                            ddpnl.Visible = true;
                            txtDirectDepositAccountHolderName.Text = dt.Rows[0]["DirectDepositAccountHolderName"].ToString();
                            txtDirectDepositBankAccountNumber.Text = dt.Rows[0]["DirectDepositBankAccountNumber"].ToString();
                            ddlDirectDepositBankAccountType.SelectedItem.Text = dt.Rows[0]["DirectDepositBankAccountType"].ToString();
                            txtDirectDepositBankBranchName.Text = dt.Rows[0]["DirectDepositBankBranchName"].ToString();
                            //txtDirectDepositBankCode.Text = dt.Rows[0]["DirectDepositBankCode"].ToString();
                            txtDirectDepositBankName.Text = dt.Rows[0]["DirectDepositBankName"].ToString();
                            //txtDirectDepositBankBranchNumber.Text = dt.Rows[0]["DirectDepositBankBranchNumber"].ToString();
                            txtDirectDepositTransitNumber.Text = dt.Rows[0]["DirectDepositTransitNumber"].ToString();
                            txtDirectDepositBranchAddress.Text = dt.Rows[0]["DirectDepositBankBranchAddress"].ToString();


                            ddlDirectDepositBankBranchcountry.SelectedIndex = ddlDirectDepositBankBranchcountry.Items.IndexOf(ddlDirectDepositBankBranchcountry.Items.FindByValue(dt.Rows[0]["DirectDepositBankBranchcountry"].ToString()));

                            object obs1 = new object();
                            EventArgs evts1 = new EventArgs();
                            ddlDirectDepositBankBranchcountry_SelectedIndexChanged(obs1, evts1);
                            ddlDirectDepositBankBranchstate.SelectedIndex = ddlDirectDepositBankBranchstate.Items.IndexOf(ddlDirectDepositBankBranchstate.Items.FindByValue(dt.Rows[0]["DirectDepositBankBranchstate"].ToString()));

                            object obc2 = new object();
                            EventArgs evtc2 = new EventArgs();
                            ddlDirectDepositBankBranchstate_SelectedIndexChanged(obc2, evtc2);
                            ddlDirectDepositBankBranchcity.SelectedIndex = ddlDirectDepositBankBranchcity.Items.IndexOf(ddlDirectDepositBankBranchcity.Items.FindByValue(dt.Rows[0]["DirectDepositBankBranchcity"].ToString()));

                            ddlDirectDepositBankBranchzipcode.Text = dt.Rows[0]["DirectDepositBankBranchzipcode"].ToString();
                            txtDirectDepositifscnumber.Text = dt.Rows[0]["DirectDepositBankIFCNumber"].ToString();
                            txtDirectDepositswiftnumber.Text = dt.Rows[0]["DirectDepositBankSwiftNumber"].ToString();
                            txtdirectdipositemployeeemail.Text = dt.Rows[0]["DirectDepositBankEmployeeEmail"].ToString();
                        }
                        else
                        {
                            ddpnl.Visible = false;
                        }
                    }
                }
            }
            else
            {
                lblmsg.Text = "No Record available.";
            }
        }
    }
    protected void ImageButton32_Click(object sender, ImageClickEventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;

        ImageButton10.Visible = true;
        ImageButton23.Visible = false;

        ImageButton9.Visible = false;
        ImageButton22.Visible = true;
    }
    protected void ImageButton33_Click(object sender, ImageClickEventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;

        ImageButton11.Visible = true;
        ImageButton24.Visible = false;

        ImageButton10.Visible = false;
        ImageButton23.Visible = true;
    }
    protected void ImageButton34_Click(object sender, ImageClickEventArgs e)
    {
        MultiView1.ActiveViewIndex = 2;

        ImageButton12.Visible = true;
        ImageButton25.Visible = false;

        ImageButton11.Visible = false;
        ImageButton24.Visible = true;
    }
    protected void ImageButton35_Click(object sender, ImageClickEventArgs e)
    {
        MultiView1.ActiveViewIndex = 3;

        ImageButton13.Visible = true;
        ImageButton26.Visible = false;

        ImageButton12.Visible = false;
        ImageButton25.Visible = true;
    }
    protected void ImageButton36_Click(object sender, ImageClickEventArgs e)
    {
        MultiView1.ActiveViewIndex = 4;

        ImageButton13.Visible = false;
        ImageButton26.Visible = true;

        ImageButton18.Visible = true;
        ImageButton27.Visible = false;
    }
    protected void ImageButton37_Click(object sender, ImageClickEventArgs e)
    {
        MultiView1.ActiveViewIndex = 5;

        ImageButton19.Visible = true;
        ImageButton28.Visible = false;

        ImageButton18.Visible = false;
        ImageButton27.Visible = true;
    }
    protected void ImageButton38_Click(object sender, ImageClickEventArgs e)
    {
        MultiView1.ActiveViewIndex = 6;

        ImageButton29.Visible = true;
        ImageButton30.Visible = false;

        ImageButton19.Visible = false;
        ImageButton28.Visible = true;
    }
}
